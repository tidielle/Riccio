Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGLSAR
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tablsar.*, tabmaga.tb_desmaga as xx_codmagap FROM (tablsar" & _
        " LEFT JOIN tabmaga ON tablsar.codditt = tabmaga.codditt AND tablsar.tb_codmagap = tabmaga.tb_codmaga)" & _
        " WHERE tablsar.codditt = " & CStrSQL(strDitta) & _
        " ORDER BY tablsar.codditt, tb_codlsar"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABLSAR")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function DeleteListSar(ByVal strDitta As String, ByVal strCodLsar As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM listsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & strCodLsar

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ElsaGetData(ByVal strDitta As String, ByVal strCodlsar As String, _
                                      ByVal bOrdinaCodart As Boolean, ByRef ds As DataSet) As Boolean
    Try
      Return ElsaGetData(strDitta, strCodlsar, bOrdinaCodart, ds, -1)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElsaGetData(ByVal strDitta As String, ByVal strCodlsar As String, _
                                          ByVal bOrdinaCodart As Boolean, ByRef ds As DataSet, _
                                          ByVal nOrdin As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodlsar, bOrdinaCodart, ds, nOrdin})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(3), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT 'N' AS xx_sel, listsar.*, ar_descr as xx_descr, ar_desint as xx_desint, ar_unmis as xx_unmis, " & _
        " af_descr as xx_fase, co_descr1 as xx_commeca, alo_lottox as xx_lottox  " & _
        " FROM ((((listsar" & _
        " LEFT JOIN artico ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart)" & _
        " LEFT JOIN artfasi ON artfasi.codditt = listsar.codditt AND artfasi.af_codart = listsar.lsa_codart AND artfasi.af_fase = listsar.lsa_fase)" & _
        " LEFT JOIN commess ON commess.codditt = listsar.codditt AND commess.co_comme = listsar.lsa_commeca)" & _
        " LEFT JOIN analotti ON listsar.codditt = analotti.codditt AND listsar.lsa_codart = analotti.alo_codart AND listsar.lsa_lotto = analotti.alo_lotto)" & _
        " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & strCodlsar & _
        " AND lsa_codart IN (SELECT ar_codart FROM artico" & _
                       " WHERE artico.codditt = " & CStrSQL(strDitta) & ")"
      If nOrdin = -1 Then
        If bOrdinaCodart = False Then
          strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, listsar.lsa_riga"
        Else
          strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, listsar.lsa_codart"
        End If
      Else
        Select Case nOrdin
          Case 0 : strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, listsar.lsa_riga"
          Case 1 : strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, listsar.lsa_codart"
          Case 2 : strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, xx_descr"
          Case 3 : strSQL = strSQL & " ORDER BY listsar.codditt, listsar.lsa_codlsar, artico.ar_codalt"
        End Select
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSAR")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ImpostaTrattatoElsa(ByVal strDitta As String, ByVal strCodlsar As String, _
    ByVal strTrattato As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE listsar" & _
        " SET lsa_trattato = " & CStrSQL(strTrattato) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & strCodlsar
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function

  Public Overridable Function GetMaxListsar(ByVal strDitta As String, ByVal strCodlsar As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Max(lsa_riga) As Riga FROM listsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = " & strCodlsar

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSAR")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function Seleziona(ByVal strDitta As String, ByVal strCodlsar As String, _
                                     ByVal dMaxRiga As Decimal, ByVal strQuery As String, _
                                     ByVal btlbImpostaStato As Boolean) As Boolean

    Try
      'obsoleta
      Return Seleziona(strDitta, strCodlsar, dMaxRiga, strQuery, btlbImpostaStato, 1, False, False, False, False, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

  Public Overridable Function Seleziona(ByVal strDitta As String, ByVal strCodlsar As String, _
                                       ByVal dMaxRiga As Decimal, ByVal strQuery As String, _
                                       ByVal btlbImpostaStato As Boolean, ByVal nCodMagP As Integer) As Boolean

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodlsar, dMaxRiga, strQuery, btlbImpostaStato, nCodMagP})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return Seleziona(strDitta, strCodlsar, dMaxRiga, strQuery, btlbImpostaStato, 1, False, False, False, False, False, nCodMagP)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

  Public Overridable Function Seleziona(ByVal strDitta As String, ByVal strCodlsar As String, _
                                        ByVal dMaxRiga As Decimal, ByVal strQuery As String, _
                                        ByVal btlbImpostaStato As Boolean, ByVal nOrdin As Integer, _
                                        ByVal bMovimentazione As Boolean, ByVal bEsplComb As Boolean, _
                                        ByVal bEsistMagZero As Boolean, ByVal bEsistMinZero As Boolean, _
                                        ByVal bEsplFasi As Boolean, ByVal nCodMagP As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strSQLTmp As String = ""
    Dim strArtFasi As String = ""
    Dim strOrder As String = ""
    Dim i As Integer = 0
    Dim strNoteTmp As String = ""


    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodlsar, dMaxRiga, strQuery, btlbImpostaStato, nOrdin, bMovimentazione, bEsplComb, bEsistMagZero, bEsplFasi, nCodMagP})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQLTmp)

      'BS--HLAR BS--FXAR ordina a seconda del option selezionato, il default se non selezionato è ar_descr
      'BN__HLAR non restituisce l'ordinamento
      strOrder = "ar_codart"
      If nOrdin = 2 Then strOrder = "ar_descr"
      If nOrdin = 3 Then strOrder = "ar_codalt"

      '-----------------------------------------------------------------------------------------
      '--- restituisce il valore S o N se è gestito a fasi oppure no
      '--- tiene in considerazione come stringa da ricercare ar_gesfasi = '
      '-----------------------------------------------------------------------------------------
      If strQuery.IndexOf("ar_gesfasi = ") > -1 Then
        strArtFasi = strQuery.Substring(strQuery.IndexOf("ar_gesfasi = ") + 14, 1)
      End If

      '-----------------------------------------------------------------------------------------
      '--- Prende tutti gli articoli della selezione che non esistono già in LISTSAR...
      '-----------------------------------------------------------------------------------------
      'gli articoli fittizi vengono esclusi dalla selezione

      If bMovimentazione = True Then
        'Seleziona articoli movimentati in riferimento al magazzino associato alla lista (LISTSAR)
        If bEsplComb = True Then
          'Seleziona articoli con massimo dettaglio fase/lotto/commessa/ubicazione (LOTCPRO)
          strSQL = "SELECT ar_codart As lsa_codart, lp_fase As lsa_fase, lp_lotto As lsa_lotto, lp_commeca As lsa_commeca, lp_ubicaz As lsa_ubicaz, lp_esist AS lsa_note" & _
            " FROM artico JOIN lotcpro ON artico.codditt = lotcpro.codditt AND artico.ar_codart = lotcpro.lp_codart" & _
            " WHERE artico.codditt = " & CStrSQL(strDitta) & _
            " AND ar_tipoopz IN (' ', 'O')" & _
            " AND lp_magaz = " & nCodMagP & _
            " " & strSQLTmp
          If strArtFasi = "S" Then
            strSQL = strSQL & " AND ar_gesfasi = 'N'" '--- Solo quelli non a fasi, in questo primo step
          End If
          If bEsistMagZero = True And bEsistMinZero = False Then
            strSQL = strSQL & " AND lp_esist > 0.0000001" '--- Solo articoli con giacenza > 0
          ElseIf bEsistMinZero = True And bEsistMagZero = False Then
            strSQL = strSQL & " AND lp_esist < -0.0000001" '--- Solo articoli con giacenza < 0
          ElseIf bEsistMinZero = True And bEsistMagZero = True Then
            strSQL = strSQL & " AND ABS(lp_esist) > 0.0000001" '--- Solo articoli con giacenza <> 0
          End If
          strSQL = strSQL & _
            " AND (ar_codart+'.'+(CONVERT(varchar(5), lp_fase))+'.'+(CONVERT(varchar(10), lp_lotto))+'.'+(CONVERT(varchar(10), lp_commeca))+'.'+ lp_ubicaz)" & _
            " NOT IN (SELECT (lsa_codart+'.'+(CONVERT(varchar(5), lsa_fase))+'.'+(CONVERT(varchar(10), lsa_lotto))+'.'+(CONVERT(varchar(10), lsa_commeca))+'.'+ lsa_ubicaz)" & _
            " FROM listsar" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND lsa_codlsar = " & strCodlsar & ")" & _
            " ORDER BY " & strOrder

        Else
          'Seleziona articoli movimentati (ARTPRO)
          strSQL = "SELECT ar_codart As lsa_codart, ap_fase As lsa_fase, 0 As lsa_lotto, 0 As lsa_commeca, ' ' As lsa_ubicaz, ap_esist AS lsa_note" & _
            " FROM artico JOIN artpro ON artico.codditt = artpro.codditt AND artico.ar_codart = artpro.ap_codart" & _
            " WHERE artico.codditt = " & CStrSQL(strDitta) & _
            " AND ar_tipoopz IN (' ', 'O')" & _
            " AND ap_magaz = " & nCodMagP & _
            " " & strSQLTmp
          If strArtFasi = "S" Then
            strSQL = strSQL & " AND ar_gesfasi = 'N' " '--- Solo quelli non a fasi, in questo primo step
          End If
          If bEsistMagZero = True And bEsistMinZero = False Then
            strSQL = strSQL & " AND ap_esist > 0.0000001" '--- Solo articoli con giacenza > 0
          ElseIf bEsistMinZero = True And bEsistMagZero = False Then
            strSQL = strSQL & " AND ap_esist < -0.0000001" '--- Solo articoli con giacenza < 0
          ElseIf bEsistMinZero = True And bEsistMagZero = True Then
            strSQL = strSQL & " AND ABS(ap_esist) > 0.0000001" '--- Solo articoli con giacenza <> 0
          End If
          strSQL = strSQL & _
          " AND (ar_codart+'.'+(CONVERT(varchar(5), ap_fase)))" & _
          " NOT IN (SELECT (lsa_codart+'.'+(CONVERT(varchar(5), lsa_fase)))" & _
          " FROM listsar" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lsa_codlsar = " & strCodlsar & ")" & _
          " ORDER BY " & strOrder
        End If

      Else
        'Seleziona articoli NON movimentati
        If bEsplFasi = True Then
          'Esplodi le FASI 
          strSQL = "SELECT ar_codart As lsa_codart, ISNULL(af_fase,ar_ultfase) As lsa_fase, 0 As lsa_lotto, 0 As lsa_commeca, ' ' As lsa_ubicaz, '' AS lsa_note" & _
            " FROM artico LEFT JOIN artfasi ON artico.ar_codart = artfasi.af_codart AND artico.codditt = artfasi.codditt" & _
            " WHERE artico.codditt = " & CStrSQL(strDitta) & _
            " AND ar_tipoopz IN (' ', 'O')" & _
            " " & strSQLTmp
          If strArtFasi = "S" Then
            strSQL = strSQL & " AND ar_gesfasi = 'N' " '--- Solo quelli non a fasi, in questo primo step
          End If
          strSQL = strSQL & " AND (ar_codart+'.'+(CONVERT(varchar(5), ISNULL(af_fase, ar_ultfase))))" & _
          " NOT IN (SELECT (lsa_codart+'.'+(CONVERT(varchar(5), lsa_fase)))" & _
          " FROM listsar" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lsa_codlsar = " & strCodlsar & ")" & _
          " ORDER BY " & strOrder
        Else
          'Seleziona solo ultima fase
          strSQL = "SELECT ar_codart As lsa_codart, ar_ultfase As lsa_fase, 0 As lsa_lotto, 0 As lsa_commeca, ' ' As lsa_ubicaz, '' AS lsa_note" & _
            " FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_tipoopz IN (' ', 'O')" & _
            " " & strSQLTmp
          If strArtFasi = "S" Then
            strSQL = strSQL & " AND ar_gesfasi = 'N' " '--- Solo quelli non a fasi, in questo primo step
          End If
          strSQL = strSQL & " AND ar_codart NOT IN (SELECT lsa_codart FROM listsar" & _
                                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                                  " AND lsa_codlsar = " & strCodlsar & ")" & _
          " ORDER BY " & strOrder
        End If

      End If

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      'INSERT articoli nella listsar
      For i = 0 To dsTmp.Tables("ARTICO").Rows.Count - 1
        dMaxRiga = (dMaxRiga + 1)

        With dsTmp.Tables("ARTICO").Rows(i)

          strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_note, lsa_codlsar, lsa_riga," & _
            " lsa_flag, lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist," & _
            " lsa_trattato, lsa_int1)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & _
            CStrSQL(!lsa_codart) & ", " & _
            CStrSQL(IIf(NTSCStr(!lsa_note) <> "", "Giac.Contabile: " + NTSCDec(!lsa_note).ToString(oApp.FormatQta), "")) & ", " & _
            strCodlsar & ", " & _
            CDblSQL(dMaxRiga) & ", 'N', " & _
            CStrSQL(!lsa_fase) & ", " & _
            NTSCStr(!lsa_commeca) & ", " & _
            NTSCStr(!lsa_lotto) & ", " & _
            CStrSQL(!lsa_ubicaz) & ", NULL, 0, " & _
            CStrSQL(IIf(btlbImpostaStato = True, "S", "N")) & ", 0)"
        End With

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Next
      '-----------------------------------------------------------------------------------------
      '--- Adesso se è il caso gli articoli a fasi
      '-----------------------------------------------------------------------------------------
      If strArtFasi = "S" Then
        strSQL = "SELECT ar_codart, af_fase" & _
          " FROM artico INNER JOIN artfasi ON (artico.ar_codart = artfasi.af_codart) AND (artico.codditt = artfasi.codditt)" & _
          " WHERE artico.codditt = " & CStrSQL(strDitta) & _
          " " & strSQLTmp & _
          " AND ar_gesfasi = 'S'" & _
          " AND ar_codart NOT IN (SELECT lsa_codart FROM listsar" & _
                                " WHERE codditt = " & CStrSQL(strDitta) & _
                                " AND lsa_codlsar = " & strCodlsar & ")" & _
        " ORDER BY " & strOrder

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

        For i = 0 To dsTmp.Tables("ARTICO").Rows.Count - 1
          dMaxRiga = (dMaxRiga + 1)

          With dsTmp.Tables("ARTICO").Rows(i)
            strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_note, lsa_codlsar, lsa_riga," & _
              " lsa_flag, lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist," & _
              " lsa_trattato, lsa_int1)" & _
              " VALUES (" & CStrSQL(strDitta) & ", " & _
              CStrSQL(!ar_codart) & ", NULL, " & strCodlsar & ", " & _
              CDblSQL(dMaxRiga) & ", 'N', " & NTSCStr(!af_fase) & ", 0, 0, ' ', NULL, 0, " & _
              CStrSQL(IIf(btlbImpostaStato = True, "S", "N")) & ", 0)"
          End With

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        Next
      End If '--- Fine gstrartfasi = "S"

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RecordBlocca(ByVal strDitta As String, ByVal strCodlsar As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE artico " & _
         "SET ar_blocco = 'S' " & _
         "FROM artico INNER JOIN listsar ON (artico.ar_codart=lsa_codart) " & _
         "AND (artico.codditt = listsar.codditt) " & _
         "WHERE listsar.codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = " & strCodlsar

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RecordSblocca(ByVal strDitta As String, ByVal strCodlsar As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE artico " & _
         "SET ar_blocco = 'N' " & _
         "FROM artico INNER JOIN listsar ON (artico.ar_codart=lsa_codart) " & _
         "AND (artico.codditt = listsar.codditt) " & _
         "WHERE listsar.codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = " & strCodlsar

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetListsar(ByVal strDitta As String, ByVal strCodlsar As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 lsa_codart FROM listsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & strCodlsar

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSAR")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GeneraFileGetListSar(ByVal strDitta As String, ByVal strCodlsar As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lsa_codart, lsa_fase, ar_descr, af_descr, ar_geslotti, ar_gescomm," & _
        " ar_gestmatr, ar_gesubic, ar_gesfasi, lsa_codlsar, ar_unmis, ar_confez2, ar_qtacon2," & _
        " ar_unmis2, ar_conver" & _
        " FROM ((listsar INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart)" & _
        " LEFT JOIN barcode ON listsar.codditt = barcode.codditt AND listsar.lsa_codart = barcode.bc_codart AND listsar.lsa_fase = barcode.bc_fase)" & _
        " LEFT JOIN artfasi ON listsar.codditt = artfasi.codditt AND listsar.lsa_codart = artfasi.af_codart AND listsar.lsa_fase = artfasi.af_fase" & _
        " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & strCodlsar & _
        " GROUP BY lsa_codart, lsa_fase, ar_descr, af_descr, ar_geslotti, ar_gescomm," & _
        " ar_gestmatr, ar_gesubic, ar_gesfasi, lsa_codlsar, ar_unmis, ar_confez2," & _
        " ar_qtacon2, ar_unmis2, ar_conver"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArtPro(ByVal strDitta As String, ByVal strLsa_codart As String, ByVal strTb_codmagap As String, ByVal strLsa_fase As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ap_esist FROM artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strLsa_codart) & _
        " AND ap_magaz = " & strTb_codmagap & _
        " AND ap_fase = " & strLsa_fase

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetBarcode(ByVal strDitta As String, ByVal strCodlsar As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_codart, bc_code, bc_unmis, bc_quant, bc_fase FROM barcode" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND bc_codart IN (SELECT lsa_codart FROM listsar" & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND lsa_codlsar = " & strCodlsar & ")"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function UpdateTablsar(ByVal strDitta As String, ByVal strnomfileinv As String, ByVal nCodlsarTmp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE tablsar" & _
        " SET tb_nomfileinv = " & CStrSQL(strnomfileinv) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codlsar = " & nCodlsarTmp

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function InsertIntoListsar(ByVal strDitta As String, ByVal strLineFile() As String, ByVal dRiga As Decimal, ByVal strCodlsar As String) As Boolean
    Dim strSQL As String = ""
    Dim strCampiLISTSAR As String
    Try
      strCampiLISTSAR = "codditt, lsa_codart, lsa_note, lsa_codlsar, lsa_riga, lsa_flag," & _
        " lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato," & _
        " lsa_int1"

      strSQL = "INSERT INTO listsar (" & strCampiLISTSAR & ")" & _
         " VALUES (" & CStrSQL(strLineFile(1)) & ", " & _
         CStrSQL(strLineFile(2)) & ", NULL, " & strCodlsar & ", " & _
         CDblSQL(dRiga) & ", 'N', " & CInt(strLineFile(3)) & ", " & _
         CLng(strLineFile(22)) & ", " & CLng(strLineFile(20)) & ", " & _
         CStrSQL(IIf(strLineFile(23) = "", " ", strLineFile(23))) & ", " & _
         CStrSQL(strLineFile(21)) & ", " & CDblSQL(NTSCDec(strLineFile(19))) & ", " & _
         CStrSQL(strLineFile(24)) & ", 0)"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal strCodLSar As String) As String
    'prepara la query, ma non la esegue: la passerà a BN__WORD che estrarrà i dati e li stamperà
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try

      strWhere = ComponiWhereStampaWord(strDitta, strCodLSar)

      strSQL = "SELECT listsar.lsa_fase, artico.*, anagra.an_descr1 AS an_descr1, anagra_1.an_descr1 AS an_descr1_1," & _
        " tabciva.tb_desciva, tabgmer.tb_desgmer, tabsgme.tb_dessgme," & _
        " tabcove.tb_descove AS tb_descove, tabcpar.tb_descpar, tabcsar.tb_descsar," & _
        " tabcove_1.tb_descove AS tb_descove_1, tabcfam.tb_descfam, tabcena.tb_descena," & _
        " tabpdon.tb_despdon, tabcove_2.tb_descove AS tb_descove_2, tabappr.tb_desappr," & _
        " tabmarc.tb_desmarc"
      strSQL = strSQL & " FROM (((((((((((((((artico INNER JOIN listsar ON (artico.codditt = listsar.codditt)" & _
        " AND (artico.ar_codart = listsar.lsa_codart))" & _
        " LEFT JOIN anagra ON (artico.codditt = anagra.codditt) AND (artico.ar_forn = anagra.an_conto))" & _
        " LEFT JOIN anagra AS anagra_1 ON (artico.codditt = anagra_1.codditt) AND (artico.ar_forn2 = anagra_1.an_conto))" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva)" & _
        " LEFT JOIN tabgmer ON (artico.codditt = tabgmer.codditt) AND (artico.ar_gruppo = tabgmer.tb_codgmer))" & _
        " LEFT JOIN tabsgme ON (artico.codditt = tabsgme.codditt) AND (artico.ar_sotgru = tabsgme.tb_codsgme))" & _
        " LEFT JOIN tabcove ON (artico.codditt = tabcove.codditt) AND (artico.ar_controp = tabcove.tb_codcove))" & _
        " LEFT JOIN tabcpar ON (artico.codditt = tabcpar.codditt) AND (artico.ar_claprov = tabcpar.tb_codcpar))" & _
        " LEFT JOIN tabcsar ON (artico.codditt = tabcsar.codditt) AND (artico.ar_clascon = tabcsar.tb_codcsar))" & _
        " LEFT JOIN tabcove AS tabcove_1 ON (artico.codditt = tabcove_1.codditt) AND (artico.ar_controa = tabcove_1.tb_codcove))" & _
        " LEFT JOIN tabcfam ON (artico.codditt = tabcfam.codditt) AND (artico.ar_famprod = tabcfam.tb_codcfam))" & _
        " LEFT JOIN tabcena ON (artico.codditt = tabcena.codditt) AND (artico.ar_numecr = tabcena.tb_codcena))" & _
        " LEFT JOIN tabpdon ON (artico.codditt = tabpdon.codditt) AND (artico.ar_codpdon = tabpdon.tb_codpdon))" & _
        " LEFT JOIN tabcove AS tabcove_2 ON (artico.codditt = tabcove_2.codditt) AND (artico.ar_contros = tabcove_2.tb_codcove))" & _
        " LEFT JOIN tabappr ON (artico.codditt = tabappr.codditt) AND (artico.ar_codappr = tabappr.tb_codappr))" & _
        " LEFT JOIN tabmarc ON (artico.codditt = tabmarc.codditt) AND (artico.ar_codmarc = tabmarc.tb_codmarc)"

      strSQL += " WHERE  " & strWhere

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiWhereStampaWord(ByVal strDitta As String, ByVal strCodLSar As String) As String
    Dim strSQL As String = ""
    Try
      strSQL = " artico.codditt = " & CStrSQL(strDitta) & " And listsar.lsa_codlsar = " & strCodLSar


      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function ControllaFasLotUbiComMat(ByVal strDitta As String, ByVal bGesfasi As Boolean, _
                                                       ByVal bGeslotti As Boolean, ByVal bGesubic As Boolean, _
                                                       ByVal bGescomm As Boolean, ByVal bGestmatr As Boolean, _
                                                       ByVal strCodart As String, ByVal strCodlsar As String, _
                                                       ByVal strFase As String, ByVal strRiga As String, _
                                                       ByVal bMsgArticoloDuplicato As Boolean, ByVal strLotto As String, _
                                                       ByVal strUbicaz As String, ByVal strCommeca As String, _
                                                       ByVal strMatric As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lsa_codart FROM listsar" & _
        " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codart = " & CStrSQL(strCodart) & _
        " AND lsa_codlsar = " & strCodlsar & _
        " AND lsa_fase = " & strFase & _
        " AND lsa_riga <> " & CDblSQL(NTSCDec(strRiga))
      If bMsgArticoloDuplicato = True Then
        If bGeslotti = True Then
          strSQL = strSQL & " AND lsa_lotto = " & strLotto
        End If
        If bGesubic = True Then
          strSQL = strSQL & " AND lsa_ubicaz = " & CStrSQL(strUbicaz)
        End If
        If bGescomm = True Then
          strSQL = strSQL & " AND lsa_commeca = " & strCommeca
        End If
        If bGestmatr = True Then
          If Trim(strMatric) = "" Then
            strSQL = strSQL & " AND lsa_matric IS NULL"
          Else
            strSQL = strSQL & " AND lsa_matric = " & CStrSQL(strMatric)
          End If
        End If
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSAR")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckInPromozioni(ByVal strDitta As String, ByVal lCod As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codrepr, tb_desrepr FROM tabrepr " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND (tb_codlsar = " & lCod & _
               "   OR tb_lsaromg = " & lCod & ")"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Duplica(ByVal strDitta As String, ByVal strCodlistsel As String, _
                                      ByVal strNewCodlistsel As String) As Boolean
    Dim strSQL As String = ""
    Dim strCampiTABLSAR As String
    Dim strCampiLISTSAR As String
    Try
      strCampiTABLSAR = "codditt, tb_codlsar, tb_deslsar, tb_lsarnote, tb_dtcomp, tb_status," & _
        " tb_nomfileinv, tb_codmagap, tb_opnomest"
      strCampiLISTSAR = "codditt, lsa_codart, lsa_note, lsa_codlsar, lsa_riga, lsa_flag," & _
        " lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato," & _
        " lsa_int1"

      strSQL = "INSERT INTO tablsar (" & strCampiTABLSAR & ")" & _
         " SELECT " & strCampiTABLSAR.Replace("tb_codlsar", strNewCodlistsel) & _
         " FROM tablsar" & _
         " WHERE codditt = " & CStrSQL(strDitta) & _
         " AND tb_codlsar = " & strCodlistsel

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "INSERT INTO listsar (" & strCampiLISTSAR & ")" & _
               " SELECT " & strCampiLISTSAR.Replace("lsa_codlsar", strNewCodlistsel) & _
               " FROM listsar" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lsa_codlsar = " & strCodlistsel

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetImpostazioniFile(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT rp_liv2 FROM REGPROP" & _
        " WHERE rp_liv1 = 'BSMGLSAR'" & _
        " AND rp_liv2 LIKE 'Terminale%'" & _
        " AND LOWER(rp_liv2) <> 'terminale'" & _
        " GROUP BY rp_liv1, rp_liv2"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ImpostazioneFileGiaEsistente(ByVal strNome As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 rp_liv2 FROM REGPROP" & _
        " WHERE rp_liv1 = 'BSMGLSAR'" & _
        " AND LOWER(rp_liv2) = " & CStrSQL(strNome.ToLower) & _
        " GROUP BY rp_liv1, rp_liv2"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DeleteImpostazioniFile(ByVal strTmp As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM REGPROP" & _
        " WHERE UPPER(rp_liv1) = 'BSMGLSAR'" & _
        " AND LOWER(rp_liv2) = " & CStrSQL(strTmp.ToLower)
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM REGDIR" & _
        " WHERE UPPER(rd_liv1) = 'BSMGLSAR'" & _
        " AND LOWER(rd_liv2) = " & CStrSQL(strTmp.ToLower)
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
