Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGHLUB
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal strCodart As String, _
                                      ByVal nFase As Integer, ByVal nMagaz As Integer, ByVal strDatreg As String, _
                                      ByRef dsHlub As DataSet) As Boolean
    Try
      Return GetData(strDitta, strCodart, nFase, nMagaz, strDatreg, dsHlub, False)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetData(ByVal strDitta As String, ByVal strCodart As String, _
                                      ByVal nFase As Integer, ByVal nMagaz As Integer, ByVal strDatreg As String, _
                                      ByRef dsHlub As DataSet, ByVal bAnaubic As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim lIITTStlocs As Integer = 0
    Dim dttTmp As New DataTable
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, nMagaz, strDatreg, dsHlub, bAnaubic})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlub = CType(oIn(5), DataSet)
        Return CBool(oOut)
      End If
      '----------------


      strSQL = "SELECT tb_dtulap FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      lIITTStlocs = GetTblInstId("TTSTLOCS", False)
      'leggo damovmag/keymag i saldi
      strSQL = "INSERT INTO TTSTLOCS (codditt, instid, tt_codart, tt_magaz, tt_ubicaz, tt_quant, tt_valore, tt_qtacar, tt_valcar, tt_fase)" & _
             " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocs.ToString & ", km_codart, km_magaz, km_ubicaz, sum(mm_quant * km_carscar), 0, Sum(mm_quant * km_carscar), 0, km_fase" & _
             " FROM testmag INNER JOIN (artico INNER JOIN (movmag INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND " & _
             " (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND " & _
             " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)) " & _
             " ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart) " & _
             " ON (testmag.codditt = movmag.codditt) AND (testmag.tm_numdoc = movmag.mm_numdoc) AND " & _
             " (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND " & _
             " (testmag.tm_tipork = movmag.mm_tipork)" & _
             " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
             " AND keymag.km_magaz = " & nMagaz & _
             " AND keymag.km_codart = " & CStrSQL(strCodart) & _
             " AND keymag.km_fase = " & nFase & _
             " AND keymag.km_aammgg > " & CDataSQL(NTSCDate(dttTmp.Rows(0)!tb_dtulap)) & _
             " AND artico.ar_gesubic = 'S'" & _
             " GROUP BY km_codart, km_fase, km_magaz, km_ubicaz"
      dttTmp.Clear()
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'aggiungo l'esistenza di lotcdef
      strSQL = "UPDATE TTSTLOCS SET tt_quant = tt_quant + ld_quant" & _
               " FROM TTSTLOCS INNER JOIN lotcdef ON TTSTLOCS.codditt = lotcdef.codditt AND " & _
               " TTSTLOCS.tt_codart = lotcdef.ld_codart AND TTSTLOCS.tt_magaz = lotcdef.ld_magaz AND " & _
               " TTSTLOCS.tt_ubicaz = lotcdef.ld_ubicaz AND TTSTLOCS.tt_fase = lotcdef.ld_fase " & _
               " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTStlocs
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'aggiungo l'esistenza di lotcdef per gli articoli non presenti sopra
      strSQL = "INSERT INTO TTSTLOCS (codditt, instid, tt_codart, tt_magaz, tt_ubicaz, tt_quant, tt_valore, tt_qtacar, tt_valcar, tt_fase )" & _
              " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocs & ", ld_codart, ld_magaz, ld_ubicaz, Sum(ld_quant), 0, 0, 0, ld_fase " & _
              " FROM lotcdef" & _
              " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
              " AND ld_ubicaz <> ' '" & _
              " AND ld_codart = " & CStrSQL(strCodart) & _
              " AND ld_magaz = " & nMagaz & _
              " AND ld_fase = " & nFase & _
              " AND NOT EXISTS (SELECT TOP 1 ttstlocs.codditt FROM ttstlocs " & _
              "                  WHERE lotcdef.codditt = ttstlocs.codditt " & _
              "                    AND tt_codart = ld_codart " & _
              "                    AND tt_fase = ld_fase " & _
              "                    AND tt_magaz = ld_magaz " & _
              "                    AND tt_ubicaz = ld_ubicaz " & _
              "                    AND instid = " & lIITTStlocs & ") " & _
              " GROUP BY ld_codart, ld_fase, ld_magaz, ld_ubicaz"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Copia la q.ta esistente nella disp. netta
      strSQL = "UPDATE TTSTLOCS SET tt_qtacar = tt_quant" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTStlocs
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Aggiorna il prenotato
      strSQL = "UPDATE TTSTLOCS SET tt_qtacar = tt_qtacar - ((mm_quant - mm_nprquaeva) * km_carscar)" & _
               " FROM TTSTLOCS INNER JOIN (movprb INNER JOIN keyprb ON (movprb.codditt = keyprb.codditt) AND " & _
               " (movprb.mm_riga = keyprb.km_riga) AND (movprb.mm_numdoc = keyprb.km_numdoc) AND " & _
               " (movprb.mm_serie = keyprb.km_serie) AND (movprb.mm_anno = keyprb.km_anno) AND " & _
               " (movprb.mm_tipork = keyprb.km_tipork)) ON TTSTLOCS.codditt = keyprb.codditt AND " & _
               " TTSTLOCS.tt_codart = keyprb.km_codart AND TTSTLOCS.tt_magaz = keyprb.km_magaz AND " & _
               " TTSTLOCS.tt_ubicaz = keyprb.km_ubicaz AND TTSTLOCS.tt_fase = keyprb.km_fase" & _
               " WHERE TTSTLOCS.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTStlocs
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Elimina i record a zero (per effetto dei calcolo floating-point sui double)
      strSQL = " DELETE FROM TTSTLOCS " & _
               " WHERE instid = " & lIITTStlocs & _
               " And codditt = " & CStrSQL(strDitta) & _
               " AND Round(tt_qtacar, 3) = 0 AND Round(tt_quant, 3) = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      If bAnaubic Then
        'Carica tutte le ubicazioni disponibili per quel magazzino
        strSQL = "INSERT INTO ttstlocs (codditt, instid, tt_codart, tt_magaz, tt_ubicaz, tt_quant, tt_valore, tt_qtacar, tt_valcar, tt_fase)" & _
                 " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocs & ", " & CStrSQL(strCodart) & ", " & nMagaz & ", au_ubicaz, 0, 0, 0, 0, " & nFase & _
                 " FROM anaubic " & _
                 "  WHERE codditt = " & CStrSQL(strDitta) & _
                 "    AND au_magaz = " & nMagaz & _
                 "    AND NOT EXISTS (SELECT TOP 1 1 FROM ttstlocs " & _
                 "                     WHERE codditt = " & CStrSQL(strDitta) & _
                 "                       AND tt_magaz = " & nMagaz & _
                 "                       AND instid = " & lIITTStlocs & _
                 "                       AND tt_ubicaz = au_ubicaz)"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      'carico l'elenco dei lotti aperti
      strSQL = "SELECT TTSTLOCS.*, au_bloccata FROM TTSTLOCS " & _
               " LEFT JOIN anaubic ON TTSTLOCS.codditt = anaubic.codditt AND au_ubicaz	= tt_ubicaz AND au_magaz = tt_magaz " & _
               " WHERE TTSTLOCS.codditt = " & CStrSQL(strDitta) & _
               "   AND TTSTLOCS.instid = " & lIITTStlocs 

      dsHlub = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "UBICAZ")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    Finally
      dttTmp.Clear()
      ResetTblInstId("TTSTLOCS", False, lIITTStlocs)
    End Try
  End Function

End Class
