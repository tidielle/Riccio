Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGSTRL
  Inherits CLD__BASE


  Public Overridable Function GetMagazFromStab(ByVal strDitta As String, ByVal nCodStab As Integer, ByRef strMagaz As String) As Boolean
    'dato uno stabilimento, ritorno l'elenco dei magazzini associati separati da ','
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strMagaz = ""
      strSQL = "SELECT tb_codmaga FROM tabmaga " & _
                       " WHERE codditt = " & CStrSQL(strDitta) & _
                       " AND tb_codstab = " & nCodStab.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        strMagaz += dtrT!tb_codmaga.ToString & ", "
      Next
      If strMagaz.Length > 0 Then strMagaz = strMagaz.Substring(0, strMagaz.Length - 2)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function RiempiTTinventArticoli(ByVal strDitta As String, ByVal strWhere As String, _
                                                     ByVal nMagaz As Integer, ByVal lIITTinvent As Integer, _
                                                     ByVal bAdataUltimoagg As Boolean) As Boolean
    '----------------------------
    'inserisco in ttinvent tutti gli articoli da trattare, senza esistenza e valore
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato), -1 = merce altrui, -2 = tutti i magazzini
    Dim strSQL As String = ""
    Dim strMagaz As String = ""

    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      strSQL = "INSERT INTO ttinvent (codditt, instid, in_codart, in_desart, in_unmis," & _
               " in_giaini, in_vgiaini, in_incdec, in_costo, in_val, in_esist, in_vesist," & _
               " in_qtalif, in_vqtalif, in_ultcos, in_dtulcar, in_perqta, in_fase )" & _
               " SELECT " & CStrSQL(strDitta) & ", " & (lIITTinvent * -1).ToString & " , ar_codart, ar_descr, ar_unmis, " & _
               " 0, 0, 0, 0, Null, 0, 0, 0, 0, 0, 0, ar_perqta, " & _
               " CASE WHEN ar_gesfasi = 'S' and not af_fase IS NULL THEN af_fase ELSE 0 END"
      If bAdataUltimoagg Then
        strSQL += " FROM ((artico INNER JOIN artdef ON artico.codditt = artdef.codditt AND artico.ar_codart = artdef.ad_codart) " & _
                  " INNER JOIN tabmaga ON artdef.codditt = tabmaga.codditt AND artdef.ad_magaz = tabmaga.tb_codmaga) "
      Else
        strSQL += " FROM ((artico INNER JOIN artpro ON artico.codditt = artpro.codditt AND artico.ar_codart = artpro.ap_codart) " & _
                  " INNER JOIN tabmaga ON artpro.codditt = tabmaga.codditt AND artpro.ap_magaz = tabmaga.tb_codmaga) "
      End If
      strSQL += " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND " & _
                " artico.ar_codart = artfasi.af_codart" & _
                " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND ar_stainv = 'S'"

      Select Case nMagaz
        Case 0  'merce propria
          strSQL += " AND (tb_flclavo = ' ' OR tb_flclavo = 'F')"
        Case -1 'merce propria
          strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
        Case -2 'tutti i magazzini
          'non devo far enulla: va già bene così
        Case Else
          If bAdataUltimoagg Then
            strSQL += " AND ad_magaz IN (" & strMagaz & ")"
          Else
            strSQL += " AND ap_magaz IN (" & strMagaz & ")"
          End If
      End Select

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strWhere, strSQL)


      strSQL += " GROUP BY ar_codart, ar_descr, ar_unmis, ar_perqta, ar_gesfasi, CASE WHEN ar_gesfasi = 'S' and not af_fase IS NULL THEN af_fase ELSE 0 END"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "CALCOLO DELL'ESISTENZA, GIACENZA E VALORE GIACENZA INIZIALE"
  Public Overridable Function CalcolaEsistenza(ByVal strDitta As String, ByVal nMagaz As Integer, _
                                               ByVal lIITTinvent As Integer, ByVal bAdataUltimoagg As Boolean, _
                                               ByVal bElabAdata As Boolean) As Boolean
    '--------------------------------
    'calcolo l'esistenza, oltre alla quantità ed al valore della giacenza iniziale: 
    'in caso di elaborazione 'A DATA' poi dovrò aggiungere i movimenti da artdef/x in poi sia per esist che per giaini / vgiaini
    'prima appoggio i dati su ttinvent per instid negativo, poi faccio la update su i record definitivi
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato) se > 1000000 è un negozio, -1 = merce altrui, -2 = tutti i magazzini

    Dim strSQL As String = ""
    Dim strMagaz As String = ""
    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      strSQL = "INSERT INTO ttinvent (codditt, instid, in_codart, in_desart, in_unmis," & _
               " in_giaini, in_vgiaini, in_incdec, in_costo, in_val, in_esist, in_vesist," & _
               " in_qtalif, in_vqtalif, in_ultcos, in_dtulcar, in_perqta, in_fase )"
      If bAdataUltimoagg Then
        Select Case nMagaz
          Case 0 ' merce propria
            strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                     " adx_lgiaini, adx_lvgiaini, adx_lesist - adx_lgiaini, " & _
                     " in_costo, in_val, adx_lesist, in_vesist," & _
                     " adx_lqtalif, adx_lvqtalif, in_costo, in_dtulcar, in_perqta, in_fase " & _
                     " FROM (ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
                     " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase) "
          Case -2 ' tutti i magazzini
            strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                     " adx_giaini, adx_vgiaini, adx_esist - adx_giaini, " & _
                     " in_costo, in_val, adx_esist, in_vesist," & _
                     " adx_qtalif, adx_vqtalif, in_costo, in_dtulcar, in_perqta, in_fase " & _
                     " FROM (ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
                     " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase) "
          Case Else 'a data o un magazzino
            If nMagaz = -1 And bElabAdata Then
              'caso specifico: leggo da artdefx come differenza tra totale magazzini meno magazzini merce propria
              strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                       " min(adx_giaini - adx_lgiaini), min(adx_vgiaini - adx_lvgiaini), min((adx_esist - adx_lesist) - (adx_giaini - adx_lgiaini)), " & _
                       " in_costo, in_val, min(adx_esist - adx_lesist), in_vesist, " & _
                       " min(adx_qtalif - adx_lqtalif), min(adx_vqtalif - adx_lvqtalif), in_costo, in_dtulcar, in_perqta, in_fase "
            Else
              'caso generale: leggo come sommatoria di artdef per tipo magazzino
              strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                       " SUM(ad_giaini), SUM(ad_vgiaini), SUM(ad_esist - ad_giaini), " & _
                       " in_costo, in_val, SUM(ad_esist), in_vesist, " & _
                       " min(adx_qtalif), min(adx_vqtalif), in_costo, in_dtulcar, in_perqta, in_fase "
            End If
            strSQL += " FROM ((((ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
                       " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase) " & _
                       " INNER JOIN artdef ON ttinvent.codditt = artdef.codditt " & _
                       " AND ttinvent.in_codart = artdef.ad_codart AND ttinvent.in_fase = artdef.ad_fase) " & _
                       " INNER JOIN tabmaga ON artdef.codditt = tabmaga.codditt AND artdef.ad_magaz = tabmaga.tb_codmaga)) "
        End Select

      Else
        strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis,"
        If nMagaz = -2 Then 'tutti i magazzini leggo da artprox
          strSQL += " MIN(apx_giaini), MIN(apx_vgiaini), MIN(apx_esist - ap_giaini), in_costo, in_val, MIN(apx_esist), in_vesist,"
        Else
          strSQL += " SUM(ap_giaini), SUM(ap_vgiaini), SUM(ap_esist - ap_giaini), in_costo, in_val, SUM(ap_esist), in_vesist,"
        End If
        strSQL += " min(apx_qtalif), min(apx_vqtalif), in_ultcos, in_dtulcar, in_perqta, in_fase " & _
                 " FROM (((ttinvent INNER JOIN artprox ON ttinvent.codditt = artprox.codditt " & _
                 " AND ttinvent.in_codart = artprox.apx_codart AND ttinvent.in_fase = artprox.apx_fase) " & _
                 " INNER JOIN artpro ON ttinvent.codditt = artpro.codditt " & _
                 " AND ttinvent.in_codart = artpro.ap_codart AND ttinvent.in_fase = artpro.ap_fase) " & _
                 " INNER JOIN tabmaga ON artpro.codditt = tabmaga.codditt AND artpro.ap_magaz = tabmaga.tb_codmaga) "
      End If

      strSQL += " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinvent * -1).ToString
      Select Case nMagaz
        Case 0  'merce propria
          If bAdataUltimoagg = False Then strSQL += " AND (tb_flclavo = ' ' OR tb_flclavo = 'F')"
        Case -1 'merce altrui
          strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
        Case -2 'tutti i magazzini
          'non devo fare nulla: va già bene così
        Case Else
          If bAdataUltimoagg Then
            If nMagaz > 0 Then strSQL += " AND ad_magaz IN (" & strMagaz & ")"
          Else
            strSQL += " AND ap_magaz IN (" & strMagaz & ")"
          End If
      End Select
      If bAdataUltimoagg And (nMagaz = 0 Or nMagaz = -2) Then
        'non serve raggruppare (non c'è in join artdef / artpro)
      Else
        strSQL += " GROUP BY ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                 " in_costo, in_val, in_vesist, in_ultcos, in_dtulcar, in_perqta, in_fase "
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '----------------------------
      'se l'elaborazione è 'a data' devo mantenere gli articoli anche se non presenti in artdef: potrei averli movimentati dopo
      If bElabAdata Then
        strSQL = "UPDATE ttinvent SET instid = instid * -1 " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & (lIITTinvent * -1).ToString & _
                 " AND (in_codart + '.' + cast(in_fase as varchar(5))) NOT IN (" & _
                 " SELECT (in_codart + '.' + cast(in_fase as varchar(5))) FROM ttinvent " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      strSQL = "DELETE FROM ttinvent WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinvent * -1).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------
      'arrotondo le quantità
      strSQL = "UPDATE ttinvent SET in_giaini = round(cast (in_giaini as numeric(20, 3)), 3), " & _
               " in_qtalif = round(cast (in_qtalif as numeric(20, 3)), 3), " & _
               " in_esist = round(cast (in_esist as numeric(20, 3)), 3), " & _
               " in_incdec = round(cast (in_incdec as numeric(20, 3)), 3) " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaEsistenzaDettaglioTCO(ByVal strDitta As String, ByVal nMagaz As Integer, _
                                               ByVal lIITTinvent As Integer, ByVal bAdataUltimoagg As Boolean, _
                                               ByVal bElabAdata As Boolean, ByVal lIITTinventTC As Integer) As Boolean
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato) se > 1000000 è un negozio, -1 = merce altrui, -2 = tutti i magazzini

    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim strMagaz As String = ""
    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      strSQL = "INSERT INTO ttinventtc (codditt, instid, int_codart, int_fase, int_magaz," & _
                " int_esist01, int_esist02, int_esist03, int_esist04, int_esist05, int_esist06," & _
                " int_esist07, int_esist08, int_esist09, int_esist10, int_esist11, int_esist12," & _
                " int_esist13, int_esist14, int_esist15, int_esist16, int_esist17, int_esist18," & _
                " int_esist19, int_esist20, int_esist21, int_esist22, int_esist23, int_esist24)" & _
                " SELECT ttinvent.codditt, " & lIITTinventTC.ToString & ", in_codart, in_fase, 0,"

      If bAdataUltimoagg Then
        Select Case nMagaz
          Case 0 ' merce propria
            For i = 1 To 24
              strSQL += " round(adt_lesist" & i.ToString.PadLeft(2, "0"c) & ", 3) " & IIf(i < 24, ", ", " ").ToString
            Next
            strSQL += " FROM ttinvent INNER JOIN artdefxtc ON ttinvent.codditt = artdefxtc.codditt " & _
                     " AND ttinvent.in_codart = artdefxtc.adt_codart AND ttinvent.in_fase = artdefxtc.adt_fase "

          Case Else 'tutti i magazzini, merce altrui o un magazzino
            For i = 1 To 24
              strSQL += " round(Sum(adt_esist" & i.ToString.PadLeft(2, "0"c) & "), 3) " & IIf(i < 24, ", ", " ").ToString
            Next
            strSQL += " FROM (ttinvent INNER JOIN artdeftc ON ttinvent.codditt = artdeftc.codditt " & _
                     " AND ttinvent.in_codart = artdeftc.adt_codart AND ttinvent.in_fase = artdeftc.adt_fase) " & _
                     " INNER JOIN tabmaga ON artdeftc.codditt = tabmaga.codditt AND artdeftc.adt_magaz = tabmaga.tb_codmaga "
        End Select

      Else
        For i = 1 To 24
          strSQL += " round(Sum(apt_esist" & i.ToString.PadLeft(2, "0"c) & "), 3) " & IIf(i < 24, ", ", " ").ToString
        Next
        strSQL += " FROM (ttinvent INNER JOIN artprotc ON ttinvent.codditt = artprotc.codditt " & _
                 " AND ttinvent.in_codart = artprotc.apt_codart AND ttinvent.in_fase = artprotc.apt_fase) " & _
                 " INNER JOIN tabmaga ON artprotc.codditt = tabmaga.codditt AND artprotc.apt_magaz = tabmaga.tb_codmaga "
      End If

      strSQL += " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent.ToString
      Select Case nMagaz
        Case 0  'merce propria
          If bAdataUltimoagg = False Then strSQL += " AND (tb_flclavo = ' ' OR tb_flclavo = 'F')"
        Case -1 'merce altrui
          strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
        Case -2 'tutti i magazzini
          'non devo fare nulla: va già bene così
        Case Else
          If bAdataUltimoagg Then
            If nMagaz > 0 Then strSQL += " AND adt_magaz IN (" & strMagaz & ")"
          Else
            strSQL += " AND apt_magaz IN (" & strMagaz & ")"
          End If
      End Select
      If bAdataUltimoagg And nMagaz = 0 Then
        'non c'è in join artdeftc o artprotc: non serve raggruppare
      Else
        strSQL += " GROUP BY ttinvent.codditt, in_codart, in_fase"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '-----------------------------------------
      'se l'elaborazione è 'a data' devo aggiungere gli articoli anche se non presenti in artdeftc: potrei averli movimentati dopo
      If bElabAdata Then
        strSQL = "INSERT INTO ttinventtc (codditt, instid, int_codart, int_fase, int_magaz," & _
                  " int_esist01, int_esist02, int_esist03, int_esist04, int_esist05, int_esist06," & _
                  " int_esist07, int_esist08, int_esist09, int_esist10, int_esist11, int_esist12," & _
                  " int_esist13, int_esist14, int_esist15, int_esist16, int_esist17, int_esist18," & _
                  " int_esist19, int_esist20, int_esist21, int_esist22, int_esist23, int_esist24)" & _
                  " SELECT " & CStrSQL(strDitta) & ", " & lIITTinventTC.ToString & ", apt_codart, apt_fase, 0," & _
                  " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
                  " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 " & _
                  " FROM artproxtc INNER JOIN articotc ON artproxtc.codditt = articotc.codditt " & _
                  " AND artproxtc.apt_codart = articotc.art_codart " & _
                  " WHERE artproxtc.codditt = " & CStrSQL(strDitta) & _
                  " AND (apt_codart + '.' + cast(apt_fase as varchar(5))) NOT IN (" & _
                  " SELECT (int_codart + '.' + cast(int_fase as varchar(5))) FROM ttinventtc " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND instid = " & lIITTinventTC.ToString & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CalcolaEsistenzaECostiAData(ByVal strDitta As String, ByVal nMagaz As Integer, _
                                                    ByVal lIITTinvent As Integer, ByVal strDatini As String, _
                                                    ByVal strDatfin As String, ByVal bUsacostiGlob As Boolean, _
                                                    ByVal nValoriz As Integer) As Boolean
    '----------------------------------
    'aggiunge a ttinvent l'esistenza degli articoli dalla data ultimo aggiornamento alla data fine elaborazione
    'mi appoggio ad un instid temporaneo
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato) > 1000000 è un negozio, -1 = merce altrui, -2 = tutti i magazzini
    'nValoriz: = ultimo costo, -5 = ultico costo comprensivo di oneri accessori (devo considerare mm_nupac e mm_numpex)
    Dim strSQL As String = ""
    Dim strMagaz As String = ""
    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      '----------------------------------
      'se devo processare un magazzino solo al costo medio globale, se devo usare i costi di tutti i magazzini 
      'devo ricalcolare in_qtalif e in_vqtalif perchè fino ad ora erano calcolati 
      'sul singolo magazzino i elaborazione
      If nMagaz > 0 And bUsacostiGlob And nValoriz = -3 Then
        strSQL = "UPDATE ttinvent SET " & _
                 " in_qtalif = adx_qtalif, in_vqtalif = adx_vqtalif " & _
                 " FROM ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt AND " & _
                 " ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase " & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                 " AND ttinvent.instid = " & lIITTinvent.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      'ottengo i saldi raggruppati
      strSQL = "INSERT INTO ttinvent (codditt, instid, in_codart, in_desart, in_unmis," & _
        " in_giaini, in_vgiaini, in_incdec, in_costo, in_val, in_esist, in_vesist," & _
        " in_qtalif, in_vqtalif, in_ultcos, in_dtulcar, in_perqta, in_fase )"
      If nMagaz > 0 And bUsacostiGlob Then
        'occhio: prendo anche movimenti di magazzini che non centrano nulla: 
        'i campi relativi ad esistenza devono essere solo
        'quelli con magazzino uguale a quello scelto
        strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                  " Sum(mm_quant * CASE WHEN km_magaz = " & nMagaz & " THEN tb_giaini ELSE 0 END), " & _
                  " Sum((mm_valore + mm_numpac + mm_numpex) * CASE WHEN km_magaz = " & nMagaz & " THEN tb_vgiaini ELSE 0 END), " & _
                  " in_incdec, in_costo, in_val, " & _
                  " Sum(mm_quant * CASE WHEN km_magaz = " & nMagaz & " THEN km_carscar ELSE 0 END), " & _
                  " in_vesist," & _
                  " Sum(mm_quant * tb_valoriz), " & _
                  " Sum((mm_valore + mm_numpac + mm_numpex) * tb_vvaloriz), " & _
                  " in_ultcos, in_dtulcar, in_perqta, in_fase "
      Else
        'prenderò i movimenti dei magazzini interessati
        strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                  " Sum(mm_quant * tb_giaini), Sum((mm_valore + mm_numpac + mm_numpex) * tb_vgiaini), " & _
                  " in_incdec, in_costo, in_val, Sum(mm_quant * km_carscar), in_vesist," & _
                  " Sum(mm_quant * tb_valoriz), Sum((mm_valore + mm_numpac + mm_numpex) * tb_vvaloriz), " & _
                  " in_ultcos, in_dtulcar, in_perqta, in_fase "
      End If
      strSQL += " FROM busvw_bsmgstrl INNER JOIN ttinvent ON  " & _
                " busvw_bsmgstrl.codditt = ttinvent.codditt " & _
                " AND TTINVENT.in_codart = busvw_bsmgstrl.km_codart" & _
                " AND TTINVENT.in_fase = busvw_bsmgstrl.km_fase " & _
                " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                " AND ttinvent.instid = " & lIITTinvent & _
                " AND km_aammgg BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)

      Select Case nMagaz
        Case 0 'merce propria
          strSQL += " AND tb_flclavo <> 'C' AND  tb_flclavo <> 'X'"
        Case -1 'merce altrui
          strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
        Case -2 'tutti i magazzini
          'non devo fare nulla
        Case Else 'un magazzino
          If bUsacostiGlob = False Then strSQL += " AND km_magaz IN (" & strMagaz & ")"
      End Select

      strSQL += " GROUP BY ttinvent.codditt, instid, in_codart, in_desart, in_unmis, in_incdec, " & _
                " in_costo, in_val, in_vesist, in_ultcos, in_dtulcar, in_perqta, in_fase"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '----------------------------------
      'se devo processare un magazzino solo storno i valori iniziali (presi da artdef/x: sono relativi a costi presi da tutti i magazzini ...)
      If nMagaz > 0 Then
        If bUsacostiGlob = False Then
          strSQL = "UPDATE ttinvent SET " & _
                   " in_qtalif = 0, in_vqtalif = 0 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTinvent.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End If

      '----------------------------------
      'aggiungo i saldi al temporaneo di elaborazione
      strSQL = "UPDATE ttinvent SET " & _
              " ttinvent.in_esist = round(ttinvent.in_esist + t1.in_esist, 3), " & _
              " ttinvent.in_qtalif = round(ttinvent.in_qtalif + t1.in_qtalif, 3), " & _
              " ttinvent.in_vqtalif = round(ttinvent.in_vqtalif + t1.in_vqtalif, 3), " & _
              " ttinvent.in_giaini = round(ttinvent.in_giaini + t1.in_giaini, 3), " & _
              " ttinvent.in_vgiaini = round(ttinvent.in_vgiaini + t1.in_vgiaini, 3), " & _
              " ttinvent.in_incdec = round(round(ttinvent.in_esist + t1.in_esist, 3) - round(ttinvent.in_giaini + t1.in_giaini, 3), 3) " & _
              " FROM ttinvent INNER JOIN ttinvent as t1 ON " & _
              " ttinvent.codditt = t1.codditt AND ttinvent.in_codart = t1.in_codart " & _
              " AND ttinvent.in_fase = t1.in_fase " & _
              " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
              " AND ttinvent.instid = " & lIITTinvent.ToString & _
              " AND t1.instid = " & (lIITTinvent * -1).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM ttinvent WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinvent * -1).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'rimuovo tutti gli articoli non movimentati
      strSQL = "DELETE FROM ttinvent WHERE in_esist = 0 AND in_giaini = 0 and in_ultcos = -1 " & _
               " and codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'arrotondo le quantità
      strSQL = "UPDATE ttinvent SET in_ultcos = 0, " & _
               " in_giaini = round(cast (in_giaini as numeric(20, 3)), 3), " & _
               " in_qtalif = round(cast (in_qtalif as numeric(20, 3)), 3), " & _
               " in_esist = round(cast (in_esist as numeric(20, 3)), 3), " & _
               " in_incdec = round(cast (in_incdec as numeric(20, 3)), 3) " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaEsistenzaECostiADataDettaglioTCO(ByVal strDitta As String, ByVal nMagaz As Integer, _
                                                    ByVal lIITTinvent As Integer, ByVal strDatini As String, _
                                                    ByVal strDatfin As String, ByVal lIITTinventTC As Integer) As Boolean
    '----------------------------------
    'aggiunge a ttinvent l'esistenza degli articoli dalla data ultimo aggiornamento alla data fine elaborazione
    'mi appoggio ad un instid temporaneo
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato) > 1000000 è un negozio, -1 = merce altrui, -2 = tutti i magazzini

    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim strMagaz As String = ""
    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      strSQL = "INSERT INTO ttinventtc (codditt, instid, int_codart, int_fase, int_magaz," & _
          " int_esist01, int_esist02, int_esist03, int_esist04, int_esist05, int_esist06," & _
          " int_esist07, int_esist08, int_esist09, int_esist10, int_esist11, int_esist12," & _
          " int_esist13, int_esist14, int_esist15, int_esist16, int_esist17, int_esist18," & _
          " int_esist19, int_esist20, int_esist21, int_esist22, int_esist23, int_esist24)" & _
          " SELECT ttinvent.codditt, " & (lIITTinventTC * -1).ToString & ", in_codart, in_fase, 0, "
      For i = 1 To 24
        strSQL += "round(Sum(mm_quant" & i.ToString.PadLeft(2, "0"c) & " * km_carscar), 3) " & IIf(i < 24, ", ", "").ToString
      Next
      strSQL += " FROM ((busvw_bsmgstrl  INNER JOIN movmagtc " & _
                " ON busvw_bsmgstrl.codditt = movmagtc.codditt " & _
                " AND busvw_bsmgstrl.km_tipork = movmagtc.mm_tipork" & _
                " AND busvw_bsmgstrl.km_anno = movmagtc.mm_anno" & _
                " AND busvw_bsmgstrl.km_serie = movmagtc.mm_serie" & _
                " AND busvw_bsmgstrl.km_numdoc = movmagtc.mm_numdoc" & _
                " AND busvw_bsmgstrl.km_riga = movmagtc.mm_riga) " & _
                " INNER JOIN ttinvent ON ttinvent.codditt = busvw_bsmgstrl.codditt " & _
                " AND TTINVENT.in_codart = busvw_bsmgstrl.km_codart" & _
                " AND TTINVENT.in_fase = busvw_bsmgstrl.km_fase) " & _
                " INNER JOIN articotc ON ttinvent.codditt = articotc.codditt AND " & _
                " ttinvent.in_codart = articotc.art_codart " & _
                " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                " AND ttinvent.instid = " & lIITTinvent & _
                " AND km_aammgg BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)

      Select Case nMagaz
        Case 0 'merce propria
          strSQL += " AND tb_flclavo <> 'C' AND  tb_flclavo <> 'X'"
        Case -1 'merce altrui
          strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
        Case -2 'tutti i magazzini
          'non devo fare nulla
        Case Else 'un magazzino
          strSQL += " AND km_magaz IN (" & strMagaz & ")"
      End Select

      strSQL += " GROUP BY ttinvent.codditt, in_codart, in_fase"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '----------------------------------
      'aggiungo i saldi al temporaneo di elaborazione
      strSQL = "UPDATE ttinventtc SET "
      For i = 1 To 24
        strSQL += "ttinventtc.int_esist" & i.ToString.PadLeft(2, "0"c) & " = ttinventtc.int_esist" & i.ToString.PadLeft(2, "0"c) & " + t1.int_esist" & i.ToString.PadLeft(2, "0"c) & IIf(i < 24, ", ", "").ToString
      Next
      strSQL += " FROM ttinventtc INNER JOIN ttinventtc as t1 ON " & _
              " ttinventtc.codditt = t1.codditt AND ttinventtc.int_codart = t1.int_codart " & _
              " AND ttinventtc.int_fase = t1.int_fase " & _
              " WHERE ttinventtc.codditt = " & CStrSQL(strDitta) & _
              " AND ttinventtc.instid = " & lIITTinventTC.ToString & _
              " AND t1.instid = " & (lIITTinventTC * -1).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM ttinventtc WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinventTC * -1).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region


  Public Overridable Function RimuoviArticoliGiacenza(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByVal nGiacenze As Integer) As Boolean
    Try
      'obsoleta
      Return RimuoviArticoliGiacenza(strDitta, lIITTinvent, nGiacenze, False, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RimuoviArticoliGiacenza(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                      ByVal nGiacenze As Integer, ByVal bTcoEsitTaglia As Boolean, _
                                                      ByVal lIITTinventTC As Integer) As Boolean
    '------------------------------
    'rimuovo gli articoli con le giacenze finali diverse da quelle richieste
    'nGiacenze: 0 = > 0,     1 = < 0,    2 = <> 0,      3 = tutte,    4 = '= 0'

    'bTcoEsitTaglia: per gli articoli TCO, se true la giacenza deve essere determinata in base alla taglia, 
    '                e non prendere la giacenza indipendentemente dalla taglia!!!!
    '                è voluto che se per un articolo TCO una taglia è positiva ed una è negativa, se chiedo solo le giancenze
    '                < 0 per quell'articolo comunque vedo anche le taglie con giacenza > 0
    Dim strSQL As String = ""
    Dim i As Integer = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTinvent, nGiacenze, bTcoEsitTaglia})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If nGiacenze = 3 Then Return True

      'per determinale la giacenza uso la giacenza indipendentemente dalla taglia anche per gli articoli TCO
      strSQL = "DELETE ttinvent FROM ttinvent INNER JOIN artico ON ttinvent.codditt = artico.codditt " & _
               " AND ttinvent.in_codart = artico.ar_codart " & _
               " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent

      If bTcoEsitTaglia Then
        'per ora solo articoli non TCO
        strSQL += " AND ar_codtagl = 0"
      End If

      Select Case nGiacenze
        Case 0 : strSQL += " AND round(in_esist, 3) <= 0"
        Case 1 : strSQL += " AND round(in_esist, 3) >= 0"
        Case 2 : strSQL += " AND round(in_esist, 3) = 0"
        Case 4 : strSQL += " AND round(in_esist, 3) <> 0"
      End Select

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


      '---------------------------------
      If bTcoEsitTaglia Then
        'ora solo gli articoli TCO
        'scarto tutti quelli che non hanno almeno una taglia con giacenza uguale a quella chiesta
        strSQL = "DELETE ttinvent FROM ttinvent INNER JOIN artico ON ttinvent.codditt = artico.codditt " & _
                 " AND ttinvent.in_codart = artico.ar_codart " & _
                 " INNER JOIN ttinventtc ON ttinvent.codditt = ttinventtc.codditt " & _
                 " AND ttinvent.in_codart = ttinventtc.int_codart" & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                 " AND ttinvent.instid = " & lIITTinvent & _
                 " AND ttinventtc.instid = " & lIITTinventTC & _
                 " AND ar_codtagl <> 0"

        strSQL += " AND NOT ("
        For i = 1 To 24
          strSQL += IIf(i = 1, "", " OR ").ToString & " round(int_esist" & i.ToString("00") & ", 3) "
          Select Case nGiacenze
            Case 0 : strSQL += " > 0"
            Case 1 : strSQL += " < 0"
            Case 2 : strSQL += " <> 0"
            Case 4 : strSQL += " = 0"
          End Select
        Next
        strSQL += ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RimuoviArticoliGiacenzaTCO(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                        ByVal nGiacenze As Integer, ByVal lIITTinventTC As Integer) As Boolean
    '------------------------------
    'rimuovo gli articoli con le giacenze finali diverse da quelle richieste
    'nGiacenze: 0 = > 0,     1 = < 0,    2 = <> 0,      3 = tutte,    4 = '= 0'
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      If nGiacenze = 3 Then Return True

      strSQL = "DELETE FROM ttinventtc " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND instid = " & lIITTinventTC & _
               "   AND NOT EXISTS (SELECT TOP 1 in_codart FROM ttinvent " & _
               "                  WHERE codditt = " & CStrSQL(strDitta) & _
               "                    AND instid = " & lIITTinvent.ToString & _
               "                    AND in_codart = int_codart " & _
               "                    AND in_fase = int_fase)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiungiArticoliGiacenza(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                       ByVal strWhere As String) As Boolean
    '------------------------------
    'aggiungo gli articoli non movimentati
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO ttinvent (codditt, instid, in_codart, in_desart, in_unmis," & _
               " in_giaini, in_vgiaini, in_incdec, in_costo, in_val, in_esist, in_vesist," & _
               " in_qtalif, in_vqtalif, in_ultcos, in_dtulcar, in_perqta, in_fase )" & _
               " SELECT " & CStrSQL(strDitta) & ", " & lIITTinvent.ToString & " , ar_codart, ar_descr, ar_unmis, " & _
               " 0, 0, 0, 0, Null, 0, 0, 0, 0, 0, 0, ar_perqta, " & _
               " CASE WHEN ar_gesfasi = 'S' and not af_fase IS NULL THEN af_fase ELSE 0 END" & _
               " FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND " & _
               " artico.ar_codart = artfasi.af_codart" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND ar_stainv = 'S'" & _
               " AND ar_codart + cast(isnull(af_fase, 0) as varchar(4)) NOT IN " & _
               " (SELECT in_codart + cast(in_fase as varchar(4)) FROM ttinvent " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent.ToString & ")"

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strWhere, strSQL)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiungiArticoliGiacenzaTCO(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                          ByVal lIITTinventTC As Integer, ByVal strWhere As String) As Boolean
    '------------------------------
    'aggiungo gli articoli TCO non movimentati
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO ttinventtc (codditt, instid, int_codart, int_fase, int_magaz," & _
               " int_esist01, int_esist02, int_esist03, int_esist04, int_esist05, int_esist06," & _
               " int_esist07, int_esist08, int_esist09, int_esist10, int_esist11, int_esist12," & _
               " int_esist13, int_esist14, int_esist15, int_esist16, int_esist17, int_esist18," & _
               " int_esist19, int_esist20, int_esist21, int_esist22, int_esist23, int_esist24)" & _
               " SELECT " & CStrSQL(strDitta) & ", " & lIITTinventTC.ToString & " , ar_codart, isnull(af_fase, 0), 0, " & _
               " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
               " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 " & _
               " FROM (artico INNER JOIN articotc ON artico.codditt = articotc.codditt AND artico.ar_codart = articotc.art_codart ) " & _
               " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND ar_stainv = 'S'" & _
               " AND ar_codart + cast(isnull(af_fase, 0) as varchar(4)) NOT IN " & _
               " (SELECT int_codart + cast(int_fase as varchar(4)) FROM ttinventtc " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinventTC.ToString & ")"

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strWhere, strSQL)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "CALCOLO DEL COSTO"
  Public Overridable Function CalcolaCostoListino(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                  ByVal nListino As Integer, ByVal strDtElab As String, _
                                                  ByVal bUsacostiForn As Boolean, ByVal bElabAdata As Boolean) As Boolean
    '----------------------------------
    'calcolo il costo in base al listino passato ed alla data di elaborazione
    'se necessario utilizzo i listini fornitore
    Dim strSQL As String = ""
    Try

      '-------------------------------
      'costo di listino
      strSQL = "UPDATE ttinvent " & _
               " SET in_val = " & CStrSQL(Microsoft.VisualBasic.Left(nListino.ToString, 4)) & "," & _
               " in_costo = ISNULL((SELECT TOP 1 lc_prezzo " & _
               " FROM busvw_listini INNER JOIN artico ON busvw_listini.codditt = artico.codditt " & _
               " AND busvw_listini.ar_codart = artico.ar_codart " & _
               " WHERE busvw_listini.codditt = ttinvent.codditt " & _
               " AND busvw_listini.ar_codart = ttinvent.in_codart " & _
               " AND busvw_listini.lc_fase = ttinvent.in_fase " & _
               " AND busvw_listini.codditt = " & CStrSQL(strDitta) & _
               " AND (busvw_listini.lc_unmis = artico.ar_unmis OR busvw_listini.lc_unmis = ' ')" & _
               " AND lc_listino = " & nListino.ToString & _
               " AND lc_conto = 0" & _
               " AND lc_coddest = 0" & _
               " AND lc_codvalu = 0" & _
               " AND lc_codtpro = 0" & _
               " AND lc_codlavo = 0 " & _
               " AND lc_daquant <= 1 AND lc_aquant >= 1 " & _
               " AND lc_datagg <= " & CDataSQL(strDtElab) & " AND lc_datscad >= " & CDataSQL(strDtElab) & _
               " ORDER BY lc_datagg DESC, lc_daquant DESC), in_costo) " & _
               " FROM ttinvent " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '-------------------------------
      'aggiungo i costi fornitori
      If bUsacostiForn Then
        strSQL = "UPDATE ttinvent " & _
                 " SET in_val = " & CStrSQL(Microsoft.VisualBasic.Left(nListino.ToString, 4)) & ", in_costo = ISNULL((SELECT TOP 1 lc_prezzo " & _
                 " FROM busvw_listini INNER JOIN artico ON busvw_listini.codditt = artico.codditt " & _
                 " AND busvw_listini.ar_codart = artico.ar_codart " & _
                 " AND busvw_listini.lc_conto = artico.ar_forn " & _
                 " WHERE busvw_listini.codditt = ttinvent.codditt " & _
                 " AND busvw_listini.ar_codart = ttinvent.in_codart " & _
                 " AND busvw_listini.lc_fase = ttinvent.in_fase " & _
                 " AND busvw_listini.codditt = " & CStrSQL(strDitta) & _
                 " AND (busvw_listini.lc_unmis = artico.ar_unmis OR busvw_listini.lc_unmis = ' ')" & _
                 " AND lc_conto <> 0" & _
                 " AND lc_coddest = 0" & _
                 " AND lc_listino = 0" & _
                 " AND lc_codvalu = 0" & _
                 " AND lc_codtpro = 0" & _
                 " AND lc_codlavo = 0 " & _
                 " AND lc_daquant <= 1 AND lc_aquant >= 1 " & _
                 " AND lc_datagg <= " & CDataSQL(strDtElab) & " AND lc_datscad >= " & CDataSQL(strDtElab) & _
                 " ORDER BY lc_datagg DESC, lc_daquant DESC), in_costo) " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoInventarioFinale(ByVal strDitta As String, ByVal lIITTinvent As Integer) As Boolean
    '----------------------------------
    'Prelevo il costo 'come da inventario finale'
    'può essere solo 'a data ultimo aggiornamento' e 'magazzini merce propria'
    Dim strSQL As String = ""
    Try

      '-------------------------------
      'aggiungo i costi fornitori
      strSQL = "UPDATE ttinvent " & _
               " SET in_val = adx_tipval, in_costo = adx_lprezzo, in_vesist = adx_lvalesi " & _
               " FROM ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
               " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase " & _
               " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoUltimoCosto(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                      ByVal nTipoElab As Integer, ByVal nMagaz As Integer, _
                                                      ByVal strDtElab As String, ByVal bCostiacc As Boolean, _
                                                      ByVal bUsacostiGlob As Boolean, ByVal strDatini As String) As Boolean
    '---------------------------------
    'cerco l'ultimo costo
    'per elaborazione 'a data' prendo artdef/f, poi cerco eventuali costi da tabanaz.tb_dtulap fino a data fine elab
    'bCostiacc = ultico costo con spese accessorie
    'nTipoElab: 0 = attuale, 1 = a data, 2 = a data ultimo aggiornamento 
    'nMagaz: 0 = merce propria, > 0 un magazzino (quelli indicato) > 1000000 è un negozio, -1 = merce altrui, -2 = tutti i magazzini

    'SOLO SE ELABORAZIONE 'A DATA xx/xx/xx' e 1 MAGAZZINO DEVO GESTIRE 'USA COSTI GLOBALI'

    Dim strSQL As String = ""
    Dim strUltcos As String = ""
    Dim strValoriz As String = ""
    Dim strMagaz As String = ""

    Try
      'se devo elaborare uno stabilimento/filiale/negozio ottengo l'elenco dei magazzini ad esso associati
      If nMagaz > 0 Then
        If nMagaz > 1000000 Then
          If Not GetMagazFromStab(strDitta, nMagaz - 1000000, strMagaz) Then Return False
        Else
          strMagaz = nMagaz.ToString
        End If
      End If

      '------------------------------------
      'in base al fatto che devo valorizzare ad ultimo costo piuttosto che ultimo costo + spese acc
      'imposto le variabili del caso
      If bCostiacc = False Then
        'ultimo costo
        strValoriz = "U"
        If nTipoElab = 0 Then 'elaboraz attuale
          strUltcos = "apx_ultcos"
        Else
          If nMagaz = 0 Then  'magaz merce propria
            strUltcos = "adx_lultcos"
          Else
            strUltcos = "adx_ultcos"
          End If
        End If
      Else
        'ultimo costo con oneri accessori
        strValoriz = "Z"
        If nTipoElab = 0 Then 'elaboraz attuale
          strUltcos = "apx_peucos"
        Else
          If nMagaz = 0 Then  'magaz merce propria
            strUltcos = "adx_lpeucos"
          Else
            strUltcos = "adx_peucos"
          End If
        End If
      End If    'If bCostiacc = False Then

      '------------------------------------
      'solo nel caso di elaborazione 'a data' di un magazzino:
      'se non devo usarei i costi globali vuol dire che il csoto deve essere ricavato SOLO dai movimenti
      If bUsacostiGlob = False And nMagaz > 0 And nTipoElab = 1 Then GoTo PASSA

      If nTipoElab = 0 Then       'attuale: uguale per tutti i magazzini
        strSQL = "UPDATE ttinvent " & _
                 " SET in_val = '" & strValoriz & "', in_costo = " & strUltcos & _
                 " FROM ttinvent INNER JOIN artprox ON ttinvent.codditt = artprox.codditt " & _
                 " AND ttinvent.in_codart = artprox.apx_codart AND ttinvent.in_fase = artprox.apx_fase " & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString
      Else
        strSQL = "UPDATE ttinvent " & _
                 " SET in_val = '" & strValoriz & "', in_costo = " & strUltcos & _
                 " FROM ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
                 " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase " & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------
      'se elaborazione 'a data' cerco l'ultimo costo tra i movimenti
      'faccio prima una query massiva per trovare i dati dai movimenti, 
      'poi una update, sempre massiva, per aggiornare ttinvent
PASSA:
      If nTipoElab = 1 Then 'elab. a data
        If bCostiacc = False Then
          'ultimo costo
          strUltcos = " round((mm_valore / mm_quant) * mm_perqta, " & TrovaNdecSuPrzUn(0) & ") "
        Else
          'ultimo costo con oneri accessori
          strUltcos = " round(((mm_valore + mm_numpac + mm_numpex) / mm_quant) * mm_perqta, " & TrovaNdecSuPrzUn(0) & ") "
        End If
        strSQL = "UPDATE ttinvent " & _
                 " SET in_val = '" & strValoriz & "', in_costo = ISNULL((SELECT TOP 1 " & strUltcos & _
                 " FROM ((movmag INNER JOIN keymag ON movmag.codditt = keymag.codditt " & _
                 " AND movmag.mm_tipork = keymag.km_tipork" & _
                 " AND movmag.mm_anno = keymag.km_anno" & _
                 " AND movmag.mm_serie = keymag.km_serie" & _
                 " AND movmag.mm_numdoc = keymag.km_numdoc" & _
                 " AND movmag.mm_riga = keymag.km_riga) " & _
                 " INNER JOIN tabmaga ON tabmaga.codditt = keymag.codditt" & _
                 " AND tabmaga.tb_codmaga = keymag.km_magaz) " & _
                 " INNER JOIN tabcaum ON tabcaum.tb_codcaum = keymag.km_causale " & _
                 " WHERE keymag.codditt = ttinvent.codditt " & _
                 " AND keymag.km_codart = ttinvent.in_codart " & _
                 " AND keymag.km_fase = ttinvent.in_fase " & _
                 " AND keymag.codditt = " & CStrSQL(strDitta) & _
                 " AND km_aammgg BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDtElab) & _
                 " AND tb_ultcos = 'S'" & _
                 " AND mm_quant <> 0" & _
                 " AND mm_valore <> 0"
        Select Case nMagaz
          Case 0 'merce propria
            strSQL += " AND tb_flclavo <> 'C' AND  tb_flclavo <> 'X'"
          Case -1 'merce altrui
            strSQL += " AND (tb_flclavo = 'C' OR tb_flclavo = 'X')"
          Case -2 'tutti i magazzini
            'non devo fare nulla
          Case Else 'un magazzino
            If bUsacostiGlob = False Then strSQL += " AND km_magaz IN (" & strMagaz & ")"
        End Select
        strSQL += " ORDER BY keymag.km_aammgg DESC, km_tipork, km_anno, km_serie DESC, km_numdoc DESC, km_riga DESC), in_costo) " & _
                  " FROM ttinvent " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND instid = " & lIITTinvent.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      End If    'elab. a data

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoMedio(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                ByVal nTipoElab As Integer, ByVal nMagaz As Integer, _
                                                ByVal strDtElab As String, ByVal bCostoMedioGlobale As Boolean, _
                                                ByVal bUsacostiGlob As Boolean, ByVal strDatini As String, _
                                                ByVal strDatfin As String) As Boolean
    '---------------------------------
    'i valori di base sono già stati determinati in nelle routine che determinano l'esistenza

    Dim strSQL As String = ""
    Try
      If nMagaz > 0 And bUsacostiGlob Then
        'nel caso di costi da tutti i magazzini non devo prendere la giacenza iniziale è gli incrementi/decrementi del magazz corr 
        'ma quella di tutti i magazzini
        'ottengo i saldi raggruppati
        '... rileggo i movimenti senza distinz. magaz
        strSQL = "INSERT INTO ttinvent (codditt, instid, in_codart, in_desart, in_unmis," & _
                  " in_giaini, in_vgiaini, in_incdec, in_costo, in_val, in_esist, in_vesist," & _
                  " in_qtalif, in_vqtalif, in_ultcos, in_dtulcar, in_perqta, in_fase )"
        strSQL += " SELECT ttinvent.codditt, instid * -1, in_codart, in_desart, in_unmis," & _
                  " Sum(mm_quant * tb_giaini), " & _
                  " Sum((mm_valore + mm_numpac + mm_numpex) * tb_vgiaini), " & _
                  " in_incdec, in_costo, in_val, " & _
                  " Sum(mm_quant * km_carscar), " & _
                  " in_vesist," & _
                  " Sum(mm_quant * tb_valoriz), " & _
                  " Sum((mm_valore + mm_numpac + mm_numpex) * tb_vvaloriz), " & _
                  " in_ultcos, in_dtulcar, in_perqta, in_fase "
        strSQL += " FROM busvw_bsmgstrl INNER JOIN ttinvent ON  " & _
                  " busvw_bsmgstrl.codditt = ttinvent.codditt " & _
                  " AND TTINVENT.in_codart = busvw_bsmgstrl.km_codart" & _
                  " AND TTINVENT.in_fase = busvw_bsmgstrl.km_fase " & _
                  " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                  " AND ttinvent.instid = " & lIITTinvent & _
                  " AND km_aammgg BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)
        strSQL += " GROUP BY ttinvent.codditt, instid, in_codart, in_desart, in_unmis, in_incdec, " & _
                  " in_costo, in_val, in_vesist, in_ultcos, in_dtulcar, in_perqta, in_fase"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        '... aggiungo la giac inziale
        strSQL = "UPDATE ttinvent SET " & _
                 " in_giaini = in_giaini + adx_giaini, " & _
                 " in_vgiaini = in_vgiaini + adx_vgiaini, " & _
                 " in_qtalif = in_qtalif + adx_qtalif, " & _
                 " in_vqtalif = in_vqtalif + adx_vqtalif " & _
                 " FROM ttinvent INNER JOIN Artdefx ON ttinvent.codditt = artdefx.codditt AND " & _
                 " ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase " & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinvent * -1).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        '... calcolo il costo sul temporaneo
        If bCostoMedioGlobale Then
          strSQL = "UPDATE ttinvent SET in_val = CASE WHEN in_qtalif + in_giaini <> 0 THEN 'G' ELSE 'I' END, " & _
                   " in_costo = CASE WHEN Round(in_qtalif + in_giaini, " & TrovaNdecSuPrzUn(0) & ") <> 0 THEN Round(((in_vqtalif + in_vgiaini) / (in_qtalif + in_giaini)) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE 0 END " & _
                   " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & (lIITTinvent * -1).ToString
        Else
          strSQL = "UPDATE ttinvent SET in_val = CASE WHEN in_qtalif <> 0 THEN 'M' ELSE 'I' END, " & _
                   " in_costo = CASE WHEN Round(in_qtalif, " & TrovaNdecSuPrzUn(0) & ") <> 0 THEN Round((in_vqtalif / in_qtalif) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE 0 END " & _
                   " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & (lIITTinvent * -1).ToString
        End If
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        '... riverso il costo sul definitivo
        strSQL = "UPDATE ttinvent SET " & _
                " ttinvent.in_val = t1.in_val, " & _
                " ttinvent.in_costo = t1.in_costo " & _
                " FROM ttinvent INNER JOIN ttinvent as t1 ON " & _
                " ttinvent.codditt = t1.codditt AND ttinvent.in_codart = t1.in_codart " & _
                " AND ttinvent.in_fase = t1.in_fase " & _
                " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                " AND ttinvent.instid = " & lIITTinvent.ToString & _
                " AND t1.instid = " & (lIITTinvent * -1).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        '... svuoto il temporaneo
        strSQL = "DELETE FROM ttinvent WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & (lIITTinvent * -1).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        If bCostoMedioGlobale Then
          strSQL = "UPDATE ttinvent SET in_val = CASE WHEN in_qtalif + in_giaini <> 0 THEN 'G' ELSE 'I' END, " & _
                   " in_costo = CASE WHEN Round(in_qtalif + in_giaini, " & TrovaNdecSuPrzUn(0) & ") <> 0 THEN Round(((in_vqtalif + in_vgiaini) / (in_qtalif + in_giaini)) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE 0 END " & _
                   " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTinvent.ToString
        Else
          strSQL = "UPDATE ttinvent SET in_val = CASE WHEN in_qtalif <> 0 THEN 'M' ELSE 'I' END, " & _
                   " in_costo = CASE WHEN Round(in_qtalif, " & TrovaNdecSuPrzUn(0) & ") <> 0 THEN Round((in_vqtalif / in_qtalif) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE 0 END " & _
                   " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTinvent.ToString
        End If
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoFifo(ByVal strDitta As String, ByVal dsIn As DataSet) As Boolean
    '----------------------------------
    'il calcolo è già stato fatto nell'entity utilizzando la funzione di oclecomm: qui devo solo riversare i dati in ttinvent
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      For i = 0 To dsIn.Tables("TTINVENT").Rows.Count - 1
        With dsIn.Tables("TTINVENT").Rows(i)
          strSQL = "UPDATE ttinvent SET in_val = " & CStrSQL(NTSCStr(!in_val)) & ", " & _
                   " in_costo = " & CDblSQL(NTSCDec(!in_costo)) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & NTSCInt(!instid) & _
                   " AND in_codart = " & CStrSQL(NTSCStr(!in_codart)) & _
                   " AND in_fase = " & NTSCInt(!in_fase) & _
                   " AND in_magaz = " & NTSCInt(!in_magaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoLifo(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                          ByVal bElabAData As Boolean, ByRef dttArti As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '------------------------------------
      'assegno il costo: da artdef/x o da artpro/x as seconda del caso (viene compilato correttamente in 'CalcolaEsistenza')
      strSQL = "UPDATE ttinvent " & _
              " SET in_costo = CASE WHEN Round(in_qtalif, 3) > 0 THEN Round((in_vqtalif / in_qtalif) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE CASE WHEN Round(in_giaini, 3) > 0 THEN Round((in_vgiaini / in_giaini) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE 0 END END," & _
              " in_val = CASE WHEN in_qtalif > 0 THEN 'L' ELSE 'I' END," & _
              " in_incdec = Round((in_esist - in_giaini), 3) " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '------------------------------------
      strSQL = "UPDATE ttinvent " & _
              " SET in_vesist = round(CASE WHEN Round(in_esist, 3) > 0 THEN Round((in_esist * in_costo) / in_perqta, " & TrovaNdecSuPrzUn(0) & ") ELSE in_vesist END, " & TrovaNdec(0) & ")" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString & " and in_giaini = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


      '------------------------------------
      'incremento positivo o fine primo anno
      strSQL = "UPDATE ttinvent SET in_vesist = in_vgiaini" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString & _
              " AND in_incdec = 0" & _
              " AND in_giaini <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "UPDATE ttinvent " & _
              " SET in_vesist = round(in_vgiaini + ((in_incdec * in_costo) / in_perqta), " & TrovaNdec(0) & ")" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString & _
              " AND in_incdec > 0" & _
              " AND in_giaini <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '------------------------------------
      'Incremento negativo
      strSQL = "SELECT in_codart, in_fase, in_giaini, in_vgiaini, in_incdec, in_vesist, in_val, in_costo, in_esist " & _
              " FROM ttinvent " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString & _
              " AND in_incdec < 0" & _
              " ORDER BY in_codart, in_fase "
      dttArti = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoLifoFase2(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                        ByVal bElabAData As Boolean, ByRef dtrArti As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim dttLif As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dVesist As Decimal = 0
    Dim dVliforic As Decimal = 0
    Dim dIncdec As Decimal = 0
    Dim dValdato As Decimal = 0
    Dim dDatogl As Decimal = 0

    Try

      strSQL = "SELECT al_valdelta, al_qtadelta FROM ARTLIF" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND al_codart = " & CStrSQL(NTSCStr(dtrArti!in_codart)) & _
              " AND al_fase = " & NTSCInt(dtrArti!in_fase).ToString & _
              " ORDER BY al_codart, al_anno DESC"
      dttLif = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttLif.Rows.Count = 0 Then
        'Nessun record in ARTLIF
        If NTSCDec(dtrArti!in_giaini) > 0 Then
          strSQL = "UPDATE ttinvent SET in_vesist = round(in_esist * (in_vgiaini / in_giaini), " & TrovaNdec(0).ToString & ") " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTinvent.ToString & _
                   " AND in_codart = " & CStrSQL(NTSCStr(dtrArti!in_codart)) & _
                   " AND in_fase = " & NTSCInt(dtrArti!in_fase).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If

      Else
        'record in ARTLIF
        dVliforic = RicalcolaValoreLifo(strDitta, dttLif, NTSCDec(dtrArti!in_giaini), NTSCDec(dtrArti!in_vgiaini))

        dIncdec = Math.Abs(NTSCDec(dtrArti!in_incdec)) ' qta da togliere
        dValdato = 0 ' valore da togliere

        dVesist = 0
        dDatogl = 0
        l = 0
        Do
          If NTSCDec(dttLif.Rows(l)!al_qtadelta) > 0 Then ' scarta i record con qta delta = 0 o  < 0
            If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) <= dIncdec Then ' e se è negativo (qtadelta - ddatogl) ???
              If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) >= 0 Then
                dIncdec = dIncdec - (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl)
                dValdato = ArrDbl(dValdato + (NTSCDec(dttLif.Rows(l)!al_valdelta) / NTSCDec(dttLif.Rows(l)!al_qtadelta)) * (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl), TrovaNdec(0))
                dDatogl = 0
              Else ' novità ..
                dDatogl = dDatogl - NTSCDec(dttLif.Rows(l)!al_qtadelta)
              End If
            Else
              If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) >= 0 Then
                dValdato = ArrDbl(dValdato + dIncdec * (NTSCDec(dttLif.Rows(l)!al_valdelta) / NTSCDec(dttLif.Rows(l)!al_qtadelta)), TrovaNdec(0))
                dIncdec = 0
              Else ' novità ..
                dDatogl = dDatogl - NTSCDec(dttLif.Rows(l)!al_qtadelta)
              End If
            End If
          Else
            dDatogl = dDatogl + (NTSCDec(dttLif.Rows(l)!al_qtadelta) * -1)

          End If
          l += 1

        Loop Until (l = dttLif.Rows.Count) Or (dIncdec <= 0)
        dttLif.Clear()
        dVesist = ArrDbl(dVliforic - dValdato, TrovaNdec(0)) ' nuovo valore

        ' ora scrive il tipo valorizz. 
        strSQL = "UPDATE ttinvent SET in_vesist = " & CDblSQL(dVesist) & ", in_val = 'L', in_costo = 0 " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString & _
                 " AND in_codart = " & CStrSQL(NTSCStr(dtrArti!in_codart)) & _
                 " AND in_fase = " & NTSCInt(dtrArti!in_fase).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      End If
      dttLif.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaCostoLifoFase3(ByVal strDitta As String, ByVal lIITTinvent As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '------------------------------
      'ricalcoli il valore = costo * esistenza
      strSQL = "UPDATE ttinvent " & _
              " SET in_vesist = round((in_costo * in_esist) / in_perqta, " & TrovaNdec(0) & ")" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent.ToString & _
              " AND in_vesist = 0" & _
              " AND in_esist > 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Function


  Public Overridable Function RicalcolaValoreLifo(ByVal strDitta As String, ByVal dttLif As DataTable, ByVal dGiac As Decimal, _
                                                  ByVal dOldvesist As Decimal) As Decimal
    '----------------------------
    ' ricalcola il valore lifo teorico se applicato nel tempo un criterio diverso...
    ' in base a qta delta e valdelta
    Dim dCont As Decimal = 0
    Dim dIncdec As Decimal = 0
    Dim dValdato As Decimal = 0
    Dim dDatogl As Decimal = 0
    Dim l As Integer = 0
    Try

      '--------------------------
      'Nessun record in ARTLIF ---- attribuisce il valore passato ...
      If dttLif.Rows.Count = 0 Then
        RicalcolaValoreLifo = dOldvesist
        Exit Function
      End If

      l = 0
      dIncdec = dGiac
      dCont = 0
      dValdato = 0
      dDatogl = 0

      Do
        If NTSCDec(dttLif.Rows(l)!al_qtadelta) > 0 Then ' scarta i record con qta delta = 0 o  < 0
          If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) <= dIncdec Then ' e se è negativo (qtadelta - ddatogl) ???
            If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) >= 0 Then
              dIncdec = dIncdec - (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl)
              dValdato = ArrDbl(dValdato + (NTSCDec(dttLif.Rows(l)!al_valdelta) / NTSCDec(dttLif.Rows(l)!al_qtadelta)) * (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl), TrovaNdec(0))
              dDatogl = 0
            Else ' novità ..
              dDatogl = dDatogl - NTSCDec(dttLif.Rows(l)!al_qtadelta)
            End If
          Else
            If (NTSCDec(dttLif.Rows(l)!al_qtadelta) - dDatogl) >= 0 Then
              dValdato = ArrDbl(dValdato + dIncdec * (NTSCDec(dttLif.Rows(l)!al_valdelta) / NTSCDec(dttLif.Rows(l)!al_qtadelta)), TrovaNdec(0))
              dIncdec = 0
            Else ' novità ..
              dDatogl = dDatogl - NTSCDec(dttLif.Rows(l)!al_qtadelta)
            End If
          End If
        Else
          dDatogl = dDatogl + (NTSCDec(dttLif.Rows(l)!al_qtadelta) * -1)

        End If
        l += 1

      Loop Until (l = dttLif.Rows.Count) Or (dIncdec <= 0)

      RicalcolaValoreLifo = ArrDbl(dValdato, TrovaNdec(0))

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AzzeraValoreEsistenzaNegativa(ByVal strDitta As String, ByVal lIITTinvent As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------------
      'ricalcoli il valore = costo * esistenza
      strSQL = "UPDATE ttinvent" & _
               " SET in_vesist = 0" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent.ToString & _
               " AND in_esist <= 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function AggiungiCostoInventariale(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                        ByVal nValoriz As Integer, ByVal bUsacostiGlob As Boolean) As Boolean
    'alla fine della determinazione del costo (da listino, ultimo costo, ...) per i costi = 0 imposto come costo quello inventariale
    Dim strSQL As String = ""
    Try
      'se sto valorizzando un singolo magazzino ad ultimo costo, ultimo costo con oneri, costo medio o costo medio globale
      'ed ho spuntato di usare i costi globali devo rideterminare in_giani e in_vgiaini perchè sono stati calcolati sul singolo magazzino
      If (nValoriz = 0 Or nValoriz = -2 Or nValoriz = -3 Or nValoriz = -5) And bUsacostiGlob Then
        strSQL = "UPDATE ttinvent SET in_val = 'I', " & _
                 " in_costo = CASE WHEN Round(adx_giaini, " & TrovaNdecSuPrzUn(0) & ") <= 0 THEN 0 ELSE Round((adx_vgiaini / adx_giaini) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") END " & _
                 " FROM ttinvent INNER JOIN artdefx ON ttinvent.codditt = artdefx.codditt " & _
                 " AND ttinvent.in_codart = artdefx.adx_codart AND ttinvent.in_fase = artdefx.adx_fase " & _
                 " WHERE ttinvent.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString & _
                 " AND in_costo = 0 "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        'caso normale
        strSQL = "UPDATE ttinvent " & _
                 " SET in_val = 'I', in_costo = CASE WHEN Round(in_giaini, " & TrovaNdecSuPrzUn(0) & ") = 0 THEN 0 ELSE Round((in_vgiaini / in_giaini) * in_perqta, " & TrovaNdecSuPrzUn(0) & ") END " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTinvent.ToString & _
                 " AND in_costo = 0 "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaValore(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByVal bArrotonda As Boolean) As Boolean
    '-----------------------------
    'aggiorno il valore dell'esistenza

    Dim strSQL As String = ""
    Try

      If bArrotonda Then
        'solo se non è 'come da inventario finale' o 'data ultimo aggiornamento' ricalcolo il costo ed il valore con importi arrotondati,
        'diversamente possono verificarsi casi di costi/valori diversi tra elaborazioni 'a data' ed 'a situazione attuale'
        'non ricalcolo sui costi di articoli non movimentati
        strSQL = "UPDATE ttinvent SET in_costo = round(cast (in_costo as numeric(20, 4)), " & TrovaNdecSuPrzUn(0) & "), " & _
                " in_esist = round(cast (in_esist as numeric(20, 3)), 3)" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTinvent.ToString & _
                " AND in_val <> 'I'"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      'arrotondo, visto che la round con una operazione matematica non viene applicata in sql server ...
      'la round non funziona .... arrotondiamo nella CAST !!!!!!
      strSQL = "UPDATE ttinvent" & _
        " SET in_vesist = CASE WHEN in_costo = 0 OR in_esist = 0 THEN 0 ElSE round(cast (((in_costo * in_esist) / in_perqta) as numeric(20, " & TrovaNdecSuPrzUn(0).ToString & ")), " & TrovaNdec(0).ToString & ") END" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinvent.ToString & " AND in_costo <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CheckValoreEsistenzaMinoreZero(ByVal strDitta As String, ByVal lIITTinvent As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 * FROM ttinvent WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTinvent & " AND in_vesist < 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetDataTtinvent(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByRef dsOut As DataSet) As Boolean
    '------------------------------
    'leggo ttinvent fifo/salva listino
    'campi mirati altrimenti da errore in DB con oltre 2.000.000 di RK in bus non 64 bit
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT instid, in_codart, in_fase, in_unmis, in_magaz, in_val, in_giaini, in_vgiaini, in_esist, in_costo" & _
        " FROM TTINVENT " & _
        " WHERE TTINVENT.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinvent.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTINVENT")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiTtinvent(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByRef dsOut As DataSet) As Boolean
    '------------------------------
    'leggo ttinvent per la stampa su griglia
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TTINVENT.*, ar_desint AS in_desint" & _
        " FROM TTINVENT INNER JOIN artico ON TTINVENT.codditt = artico.codditt AND TTINVENT.in_codart = artico.ar_codart" & _
        " WHERE TTINVENT.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinvent.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTINVENT")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CountTtinvent(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByRef nRec As Integer, ByRef dTot As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      nRec = 0
      dTot = 0
      strSQL = "SELECT count(*) as nrec, sum(in_vesist) as tot " & _
               " FROM TTINVENT WHERE TTINVENT.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nRec = NTSCInt(dttTmp.Rows(0)!nrec)
        dTot = NTSCDec(dttTmp.Rows(0)!tot)
      End If
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function InventarioFinale(ByVal strDitta As String, ByVal lIITTinvent As Integer, ByVal strTipval As String) As Boolean
    '-----------------------------------
    'se selezionato 'inventario finale' devo aggiornare artdefx
    Dim strSQL As String = ""
    Try
      '-----------------------------------
      'aggiorna tipo valorizz., adx_lprezzo e adx_lvalesi
      strSQL = "UPDATE artdefx SET adx_tipval = " & CStrSQL(strTipval) & ", " & _
               "adx_lprezzo = ROUND(CASE WHEN in_costo IS NULL THEN 0 ELSE in_costo END, " & TrovaNdecSuPrzUn(0) & "), " & _
               "adx_lvalesi = ROUND(in_vesist, " & TrovaNdec(0) & ") " & _
               " FROM artdefx INNER JOIN ttinvent ON artdefx.codditt = ttinvent.codditt " & _
               " AND artdefx.adx_codart = ttinvent.in_codart " & _
               " AND artdefx.adx_fase = ttinvent.in_fase " & _
               " AND ttinvent.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTinvent.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------------------
      ' adesso però toglie iev. valori negativi da artdefx ...
      'Azzera le giacenze finali inferiori a zero...
      strSQL = "UPDATE ARTDEFX SET adx_lvalesi = 0" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " and adx_lesist <= 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CaricaDettaglioLifo(ByVal strDitta As String, ByVal lIITTinvent As Integer, _
                                                  ByVal lIITTinvent2 As Integer, ByVal bCheckArtlif As Boolean, _
                                                  ByRef strErr As String, ByRef dTotalemagazStoricoLifo As Decimal) As Boolean
    '------------------------------------
    'carico il temporaneo per la stampa del dettaglio Lifo
    Dim strSQL As String = ""
    Dim dttInv As New DataTable
    Dim dIncdec As Decimal = 0
    Dim dResiduo As Decimal = 0
    Dim dttTmp As New DataTable

    Dim dIn_residuo As Decimal = 0
    Dim dIn_vresiduo As Decimal = 0
    Dim bOk As Boolean = False

    Try
      strErr = ""
      strSQL = "SELECT in_codart, in_fase, in_incdec, in_giaini, in_esist FROM ttinvent " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTinvent & _
              " ORDER BY in_codart, in_fase "
      dttInv = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttInv.Rows.Count = 0 Then Return True

      For Each dtrT As DataRow In dttInv.Rows
        strSQL = "INSERT INTO ttinven2 (codditt, instid, in_codart, in_anno, in_esist, in_valore," & _
                " in_valdelta, in_qtadelta, in_tipval, in_residuo, in_vresiduo, in_fase)" & _
                " SELECT " & CStrSQL(strDitta) & ", " & lIITTinvent2.ToString & ", ar_codart," & _
                " CASE WHEN al_anno IS NULL THEN 0 ELSE al_anno END, al_esist, al_valore," & _
                " al_valdelta, al_qtadelta, al_tipval," & _
                " CASE WHEN al_qtadelta < 0 THEN 0 ELSE al_qtadelta END, al_valdelta," & _
                NTSCInt(dtrT!in_fase) & _
                " FROM artico LEFT JOIN artlif ON artico.codditt = artlif.codditt AND artico.ar_codart = artlif.al_codart" & _
                " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                " AND ar_codart = " & CStrSQL(NTSCStr(dtrT!in_codart)) & _
                " AND (al_fase = " & NTSCInt(dtrT!in_fase).ToString & " OR al_fase is null)" & _
                " ORDER BY ar_codart, al_fase, al_anno DESC"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        If NTSCDec(dtrT!in_incdec) < 0 Then
          dIncdec = NTSCDec(dtrT!in_incdec)                         '(dIncdec ha sepmre segno meno)
        Else
          dIncdec = 0
        End If
        dResiduo = NTSCDec(dtrT!in_esist)
        'apro un dyn su ttinvent2 pe rl'articolo in analisi ....
        strSQL = "SELECT * FROM ttinven2 " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND instid = " & lIITTinvent2.ToString & _
                  " AND in_codart = " & CStrSQL(NTSCStr(dtrT!in_codart)) & _
                  " AND in_fase = " & NTSCInt(dtrT!in_fase).ToString & _
                  " ORDER BY in_anno DESC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then GoTo NextRecord

        For Each dtrT2 As DataRow In dttTmp.Rows
          bOk = False
          If NTSCDec(dtrT2!in_qtadelta) <> 0 Then
            If dResiduo <> 0 Then                                            'se c'è ancora dell'esistenza iniziale da ripartire
              If NTSCDec(dtrT2!in_qtadelta) <= 0 Then
                dIncdec = dIncdec + NTSCDec(dtrT2!in_qtadelta)
              Else
                If NTSCDec(dtrT2!in_residuo) >= (dIncdec * -1) Then               'tolgo eventuali decrementi
                  dIn_residuo = NTSCDec(dtrT2!in_residuo)
                  dIn_vresiduo = NTSCDec(dtrT2!in_vresiduo)
                  If dIncdec <> 0 Then
                    bOk = True
                    dIn_residuo = ArrDbl(NTSCDec(dtrT2!in_residuo), 3) + ArrDbl(dIncdec, 3)
                    dIncdec = 0
                    dIn_vresiduo = dIn_residuo * (NTSCDec(dtrT2!in_valdelta) / NTSCDec(dtrT2!in_qtadelta))
                  End If
                  If ArrDbl(dIn_residuo - dResiduo, 3) >= 0 Then    'diminuisco la giacenza iniziale
                    dResiduo = 0
                  Else
                    dResiduo = ArrDbl(dResiduo - dIn_residuo, 3)
                  End If
                Else
                  bOk = True
                  dIncdec = ArrDbl(dIncdec + NTSCDec(dtrT2!in_residuo), 3)
                  dIn_residuo = 0
                  dIn_vresiduo = 0
                End If
              End If
            Else
              If NTSCDec(dtrT2!in_residuo) <> 0 Then                              'non dovrebbe mai succedere
                bOk = True
                dIn_residuo = 0
                dIn_vresiduo = 0
              End If
            End If
          End If

          If bOk Then
            strSQL = "UPDATE ttinven2 " & _
                    " SET in_residuo = " & CDblSQL(dIn_residuo) & ", in_vresiduo = " & CDblSQL(dIn_vresiduo) & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND instid = " & lIITTinvent2.ToString & _
                    " AND in_codart = " & CStrSQL(NTSCStr(dtrT!in_codart)) & _
                    " AND in_fase = " & NTSCInt(dtrT!in_fase).ToString & _
                    " AND in_anno = " & NTSCInt(dtrT2!in_anno).ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
        Next    'For Each dtrT2 As DataRow In dttTmp.Rows
        dttTmp.Clear()

        If bCheckArtlif Then
          If dIncdec < 0 Then
            strErr += oApp.Tr(Me, 128505009909214000, "Per l'articolo |" & NTSCStr(dtrT!in_codart) & "| fase |" & NTSCStr(dtrT!in_fase) & "| l'archivio storico LIFO non è corretto: " & vbCrLf & _
                  "non ci sono sufficienti carichi per soddisfare gli scarichi.")
          End If
          If dResiduo > 0 And NTSCDec(dtrT!in_incdec) < 0 Then
            strErr += oApp.Tr(Me, 128505010511530000, "Per l'articolo |" & NTSCStr(dtrT!in_codart) & "| fase |" & NTSCStr(dtrT!in_fase) & "| l'archivio storico LIFO non è corretto: " & vbCrLf & _
                   "non ci sono sufficienti carichi per soddisfare la giacenza iniziale.")
          End If
        End If

NextRecord:
      Next    'For Each dtrT As DataRow In dsTmp.Tables("TTINVENT").Rows

      dttTmp.Clear()
      strSQL = "SELECT Sum(in_vesist) As Totale FROM ttinvent " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTinvent & _
                " AND in_esist > 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then dTotalemagazStoricoLifo = NTSCDec(dttTmp.Rows(0)!Totale)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      dttTmp.Clear()
      dttInv.Clear()
    End Try
  End Function

  Public Overridable Function EscludiArticoliNonMovimentati(ByVal strDitta As String, _
    ByVal lIITTinvent As Integer, ByVal lIITTinvent2 As Integer, ByVal lIITTinventTC As Integer) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    '--- Se selezionato il CheckBox "Escludi articoli mai movimentati"
    '----------------------------------------------------------------------------------------------------------------
    Dim i As Integer = 0
    Dim y As Integer = 0
    Dim lInstid As Integer = 0
    Dim strSQL As String = ""
    Dim strTabella As String = ""
    Dim strCampoCodart As String = ""
    Dim strCampoFase As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      For y = 1 To 3
        Select Case y
          Case 1
            strTabella = "TTINVENT"
            lInstid = lIITTinvent
          Case 2
            strTabella = "TTINVEN2"
            lInstid = lIITTinvent2
          Case 3
            strTabella = "TTINVENTTC"
            lInstid = lIITTinventTC
        End Select
        '----------------------------------------------------------------------------------------------------------
        Select Case y
          Case 1, 2
            strCampoCodart = "in_codart"
            strCampoFase = "in_fase"
          Case 3
            strCampoCodart = "int_codart"
            strCampoFase = "int_fase"
        End Select
        '----------------------------------------------------------------------------------------------------------
        strSQL = "SELECT " & strCampoCodart & " As Codart, " & strCampoFase & " As Fase" & _
          " FROM " & strTabella & " LEFT JOIN artprox ON " & strTabella & ".codditt = artprox.codditt" & _
          " AND " & strTabella & "." & strCampoCodart & " = artprox.apx_codart" & _
          " AND " & strTabella & "." & strCampoFase & " = artprox.apx_fase" & _
          " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lInstid & _
          " AND apx_codart IS NULL" & _
          " ORDER BY " & strCampoCodart & ", " & strCampoFase
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        '----------------------------------------------------------------------------------------------------------
        If dttTmp.Rows.Count > 0 Then
          '----------------------------------------------------------------------------------------------------------
          For i = 0 To (dttTmp.Rows.Count - 1)
            strSQL = "DELETE FROM " & strTabella & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lInstid & _
              " AND " & strCampoCodart & " = " & CStrSQL(dttTmp.Rows(i)!Codart) & _
              " AND " & strCampoFase & " = " & NTSCInt(dttTmp.Rows(i)!Fase)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Next
        End If
        '------------------------------------------------------------------------------------------------------------
      Next y
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DeterminaCodiceListaSelezionata(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT Max(tb_codlsar) As Codlsar FROM tablsar" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then Return (NTSCInt(dttTmp.Rows(0)!Codlsar) + 1)
      '----------------------------------------------------------------------------------------------------------
      Return 1
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GeneraListaSelezionata(ByVal strDitta As String, ByVal nCodlsar As Integer, _
                                                     ByVal strDeslsar As String, ByVal lIITTinvent As Integer, ByVal lIITTinventTC As Integer, _
                                                     ByVal nCodmaga As Integer, ByVal strData As String, _
                                                     ByRef bTTINVENTVuoto As Boolean) As Boolean
    Try
      Return GeneraListaSelezionata(strDitta, nCodlsar, strDeslsar, lIITTinvent, lIITTinventTC, nCodmaga, strData, bTTINVENTVuoto, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GeneraListaSelezionata(ByVal strDitta As String, ByVal nCodlsar As Integer, _
                                                     ByVal strDeslsar As String, ByVal lIITTinvent As Integer, ByVal lIITTinventTC As Integer, _
                                                     ByVal nCodmaga As Integer, ByVal strData As String, _
                                                     ByRef bTTINVENTVuoto As Boolean, ByVal bQta0 As Boolean) As Boolean
    Dim i As Integer = 0
    Dim y As Integer = 0
    Dim dRiga As Decimal = 0
    Dim dEsistTC As Decimal = 0
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim strTaglia As String = ""
    Dim strCampiLISTSAR As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, nCodlsar, strDeslsar, lIITTinvent, lIITTinventTC, nCodmaga, strData, bTTINVENTVuoto, bQta0})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        bTTINVENTVuoto = CBool(oIn(7))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      bTTINVENTVuoto = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Prima di procedere con lo svuotamento/creazione della Lista Selezionata Articoli, controlla se le
      '--- tabelle temporanee TTINVENT/TTINVENTTC, contengono almeno un record,
      '--- nel caso non procede e avvisa (nell'Entity)
      '--- N.B. TTINVENT "NON" considera gli articoli a Taglie & Colori
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 in_codart" & _
        " FROM TTINVENT INNER JOIN artico ON TTINVENT.codditt = artico.codditt AND TTINVENT.in_codart = artico.ar_codart" & _
        " WHERE TTINVENT.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinvent & _
        " AND ar_codtagl = 0" & _
        " ORDER BY TTINVENT.codditt, instid, in_codart, in_fase, in_magaz"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bTTINVENTVuoto = False
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If bTTINVENTVuoto = True Then
        strSQL = "SELECT TOP 1 int_codart FROM TTINVENTTC" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTinventTC & _
          " AND ("
        For i = 1 To 24
          strSQL += "int_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & " <> 0" & _
            IIf(i < 24, " OR ", ")").ToString
        Next
        strSQL += " ORDER BY codditt, instid, int_codart, int_fase, int_magaz"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then bTTINVENTVuoto = False
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bTTINVENTVuoto = True Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strCampiLISTSAR = "codditt, lsa_codart, lsa_note, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase, lsa_commeca," & _
        " lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1, lsa_tctaglia, lsa_tcindtagl," & _
        " lsa_barcode"
      '--------------------------------------------------------------------------------------------------------------
      '--- Svuota l'eventuale lista selezionata esistente
      '--- (il test per determinare se è una lista cancellabile è stato fatto prima dell'elaborazione)
      '--------------------------------------------------------------------------------------------------------------
      '--- Svuota prima LISTSAR
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM listsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & nCodlsar
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Poi TABLSAR
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM tablsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codlsar = " & nCodlsar
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Crea la nuova lista selezionata
      '--------------------------------------------------------------------------------------------------------------
      '--- TABLSAR
      '--------------------------------------------------------------------------------------------------------------
      Dim nMagazzinoListaSelezionata As Integer = 0
      If nCodmaga > 1000000 Then
        'si tratta di un negozio e quindi la lista selezionata verrà inserita senza magazzino
      ElseIf nCodmaga < 0 Then
        'si tratta del magazzino 'merce altrui' oppure della opzione 'tutti magazzini'
        'anziche inserire i rispettivi valori -1, -2 nella lista selezionata 
        'quest'ultima verrà generata sempre con il magazzino=0
      Else
        nMagazzinoListaSelezionata = nCodmaga
      End If
      strSQL = "INSERT INTO tablsar (codditt, tb_codlsar, tb_deslsar, tb_lsarnote, tb_dtcomp, tb_status," & _
        " tb_nomfileinv, tb_codmagap, tb_opnomest)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & nCodlsar & ", " & CStrSQL(strDeslsar) & ", NULL, " & _
        CDataSQL(strData) & ", 'M', NULL, " & nMagazzinoListaSelezionata & ", ' ')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- LISTSAR da TTINVENT
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TTINVENT.*, ARTICO.* " & _
        " FROM TTINVENT INNER JOIN artico ON TTINVENT.codditt = artico.codditt AND TTINVENT.in_codart = artico.ar_codart" & _
        " WHERE TTINVENT.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinvent & _
        " AND ar_codtagl = 0" & _
        " ORDER BY TTINVENT.codditt, instid, in_codart, in_fase, in_magaz"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Dim bUbic As Boolean = CBool(GetSettingBus("BSMGSTRL", "OPZIONI", ".", "CreaListaConUbicazArticolo", "0", ".", "0"))

        For i = 0 To (dttTmp.Rows.Count - 1)
          '----------------------------------------------------------------------------------------------------------
          '--- Incrementa il contatore di riga
          '----------------------------------------------------------------------------------------------------------
          dRiga += 1
          '----------------------------------------------------------------------------------------------------------
          With dttTmp.Rows(i)
            '--------------------------------------------------------------------------------------------------------
            '--- Formatta l'esistenza con 8 spazi a sinistra e 3 zeri a destra, da mettere nelle note di riga
            '--------------------------------------------------------------------------------------------------------
            strNote = Microsoft.VisualBasic.Right(Format(NTSCDec(!in_esist.ToString), "".PadLeft(8) & "0.000"), 12).ToString
            '--------------------------------------------------------------------------------------------------------
            strSQL = "INSERT INTO listsar (" & strCampiLISTSAR & ")" & _
              " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(!in_codart) & ", " & CStrSQL(strNote) & ", " & _
              nCodlsar & ", " & CDblSQL(dRiga) & ", 'N', " & NTSCInt(!in_fase) & ", 0, 0, " & CStrSQL(IIf(bUbic, NTSCStr(!ar_ubicaz).Trim.PadLeft(1, " "c), " ")) & ", NULL, " & _
              CDblSQL(NTSCDec(IIf(bQta0, 0, !in_esist))) & ", 'N', 0, ' ', 0, NULL)"
          End With
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude il DataTabel su TTINVENT
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      i = 0
      '--------------------------------------------------------------------------------------------------------------
      '--- LISTSAR da TTINVENTTC
      '--- inserisce tante righe in LISTSAR per ogni esistenza taglia/colore diversa da zero
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TTINVENTTC.*, tabtagl.* FROM" & _
        " (TTINVENTTC INNER JOIN artico ON TTINVENTTC.codditt = artico.codditt AND TTINVENTTC.int_codart = artico.ar_codart)" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE TTINVENTTC.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTinventTC & _
        " AND ("
      For i = 1 To 24
        strSQL += "int_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & " <> 0" & _
          IIf(i < 24, " OR ", ")").ToString
      Next
      strSQL += " ORDER BY TTINVENTTC.codditt, instid, int_codart, int_fase, int_magaz"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      i = 0
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        For i = 0 To (dttTmp.Rows.Count - 1)
          For y = 1 To 24
            If NTSCDec(dttTmp.Rows(i)("int_esist" & Microsoft.VisualBasic.Right("0" & y.ToString, 2))) <> 0 Then
              '------------------------------------------------------------------------------------------------------
              '--- Incrementa il contatore di riga
              '------------------------------------------------------------------------------------------------------
              dRiga += 1
              '------------------------------------------------------------------------------------------------------
              With dttTmp.Rows(i)
                '----------------------------------------------------------------------------------------------------
                '--- Formatta l'esistenza con 8 spazi a sinistra e 3 zeri a destra, da mettere nelle note di riga
                '----------------------------------------------------------------------------------------------------
                dEsistTC = NTSCDec(dttTmp.Rows(i)("int_esist" & Microsoft.VisualBasic.Right("0" & y.ToString, 2)))
                strNote = Microsoft.VisualBasic.Right(Format(dEsistTC, "".PadLeft(8) & ".000"), 12).ToString
                '----------------------------------------------------------------------------------------------------
                '--- Determina la descrizione della taglia a seconda dell'indice di taglia
                '----------------------------------------------------------------------------------------------------
                strTaglia = dttTmp.Rows(i)("tb_dest" & Microsoft.VisualBasic.Right("0" & y.ToString, 2)).ToString
                '--------------------------------------------------------------------------------------------------------
                strSQL = "INSERT INTO listsar (" & strCampiLISTSAR & ")" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(!int_codart) & ", " & CStrSQL(strNote) & ", " & _
                  nCodlsar & ", " & CDblSQL(dRiga) & ", 'N', " & NTSCInt(!int_fase) & ", 0, 0, ' ', NULL, " & _
                  CDblSQL(NTSCDec(IIf(bQta0, 0, dEsistTC))) & ", 'N', 0, " & CStrSQL(strTaglia) & ", " & NTSCInt(!tb_codtagl) & ", NULL)"
              End With
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
              '------------------------------------------------------------------------------------------------------
            End If
          Next
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude il DataTabel su TTINVENTTC
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ProponiDescrizioneListaSelezionata(ByVal strDitta As String, ByVal nCodlsar As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_deslsar FROM tablsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codlsar = " & nCodlsar
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return "" Else Return NTSCStr(dttTmp.Rows(0)!tb_deslsar)
      '----------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  '@LM
  Public Overridable Function LoadWarehouse(ByVal pstrDitta As String, ByRef pdsOut As DataSet) As Boolean
    Dim bRetVal As Boolean = False
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT [tabmaga].*, 'N' AS xx_seleziona"
      strSQL += " FROM [tabmaga]"
      strSQL += " WHERE [tabmaga].codditt = " & CStrSQL(pstrDitta)

      pdsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "tabmaga")

      bRetVal = True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return bRetVal
  End Function
  '@

End Class
