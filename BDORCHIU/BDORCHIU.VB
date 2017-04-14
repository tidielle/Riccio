Imports System.IO
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class CLDORCHIU
  Inherits CLD__BASE

  Public Overridable Function AggiungiImpegniCollegati(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = (dttOut.Rows.Count - 1) To 0 Step -1
        With dttOut.Rows(i)
          If NTSCStr(!td_tipork) = "H" Then
            strSQL = "SELECT TOP 1 mo_tipork, mo_anno, mo_serie, mo_numord FROM movord" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mo_tiporkor = " & CStrSQL(NTSCStr(!td_tipork)) & _
              " AND mo_annoor = " & NTSCInt(!td_anno) & _
              " AND mo_serieor = " & CStrSQL(NTSCStr(!td_serie)) & _
              " AND mo_numordor = " & NTSCInt(!td_numord)
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then
              dttOut.Rows.InsertAt(dttOut.NewRow, i + 1)
              dttOut.Rows(i + 1).ItemArray = dttOut.Rows(i).ItemArray
              dttOut.AcceptChanges()
              dttOut.Rows(i + 1)!td_tipork = NTSCStr(dttTmp.Rows(0)!mo_tipork)
              dttOut.Rows(i + 1)!td_anno = NTSCInt(dttTmp.Rows(0)!mo_anno)
              dttOut.Rows(i + 1)!td_serie = NTSCStr(dttTmp.Rows(0)!mo_serie)
              dttOut.Rows(i + 1)!td_numord = NTSCInt(dttTmp.Rows(0)!mo_numord)
              dttOut.AcceptChanges()
            End If
            dttTmp.Clear()
            dttTmp.Dispose()
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SelezionaTestord(ByVal strDitta As String, _
    ByVal bNotaDiPrelievo As Boolean, ByVal strWhere As String, _
    ByVal lDanumord As Integer, ByVal lAnumord As Integer, _
    ByVal strDadatord As String, ByVal strAdatord As String, _
    ByVal strDadatcons As String, ByVal dtadatcons As String, _
    ByVal lCommecaIni As Integer, ByVal lCommecaFin As Integer, _
    ByVal lAnno As Integer, ByVal strSerie As String, _
    ByVal nCodagen As Integer, ByVal bParzEvasi As Boolean, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strTestata As String = IIf(bNotaDiPrelievo = True, "testprb", "testord").ToString
    Dim strCorpo As String = IIf(bNotaDiPrelievo = True, "movprb", "" & "movord" & "").ToString
    Dim strPrefT As String = IIf(bNotaDiPrelievo = True, "tm_", "td_").ToString
    Dim strPrefC As String = IIf(bNotaDiPrelievo = True, "mm_", "mo_").ToString

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT 'N' AS xx_sel, " & strPrefT & "tipork AS td_tipork, " & strPrefT & "anno AS td_anno, " & _
        strPrefT & "serie AS td_serie, " & _
        IIf(bNotaDiPrelievo = False, "td_numord", "tm_numdoc").ToString & " AS td_numord," & _
        " MIN(" & IIf(bNotaDiPrelievo = False, "td_datord", "tm_datdoc").ToString & ") AS td_datord," & _
        " MIN(" & strPrefT & "conto) AS td_conto, MIN(an_descr1) AS xx_conto," & _
        " MIN(" & strPrefT & "codagen) AS td_codagen, MIN(tb_descage) AS xx_codagen," & _
        " MIN(" & strPrefT & "tipobf) AS td_tipobf, MIN(tb_destpbf) AS xx_tipobf," & _
        " MIN(" & strPrefT & "coddest) AS td_coddest, MIN(dd_nomdest) AS xx_coddest," & _
        " MIN(" & strPrefT & "vettor) AS td_vettor, MIN(tb_desvett) AS xx_vettor," & _
        IIf(bNotaDiPrelievo = False, "0", "MIN(tm_idpick)").ToString & " AS tm_idpick" & _
        " FROM " & strTestata & " INNER JOIN " & strCorpo & " ON " & strTestata & ".codditt = " & strCorpo & ".codditt AND " & strTestata & "." & strPrefT & "tipork = " & strCorpo & "." & strPrefC & "tipork AND " & strTestata & "." & strPrefT & "anno = " & strCorpo & "." & strPrefC & "anno AND " & strTestata & "." & strPrefT & "serie = " & strCorpo & "." & strPrefC & "serie AND " & strTestata & "." & IIf(bNotaDiPrelievo = False, "td_numord", "tm_numdoc").ToString & " = " & strCorpo & "." & IIf(bNotaDiPrelievo = False, "mo_numord", "mm_numdoc").ToString & _
        " INNER JOIN anagra ON " & strTestata & ".codditt = anagra.codditt AND " & strTestata & "." & strPrefT & "conto = anagra.an_conto" & _
        " LEFT JOIN tabcage ON " & strTestata & ".codditt = tabcage.codditt AND " & strTestata & "." & strPrefT & "codagen = tabcage.tb_codcage" & _
        " LEFT JOIN tabtpbf ON " & strTestata & ".codditt = tabtpbf.codditt AND " & strTestata & "." & strPrefT & "tipobf = tabtpbf.tb_codtpbf" & _
        " LEFT JOIN destdiv ON " & strTestata & ".codditt = destdiv.codditt AND " & strTestata & "." & strPrefT & "conto = destdiv.dd_conto AND " & strTestata & "." & strPrefT & "coddest = destdiv.dd_coddest" & _
        " LEFT JOIN tabvett ON " & strTestata & ".codditt = tabvett.codditt AND " & strTestata & "." & strPrefT & "vettor = tabvett.tb_codvett" & _
        " WHERE " & strTestata & ".codditt = " & CStrSQL(strDitta) & _
        " AND " & strPrefT & "flevas <> 'S'" & _
        IIf(bNotaDiPrelievo = True, " AND tm_tipork = 'W'", " AND " & strWhere).ToString & _
        " AND " & strPrefT & "anno = " & lAnno & _
        IIf(strSerie <> "", " AND " & strPrefT & "serie = " & CStrSQL(strSerie), "").ToString & _
        IIf(nCodagen <> 0, " AND " & strPrefT & "codagen = " & nCodagen, "").ToString & _
        IIf(bParzEvasi = True, " AND " & IIf(bNotaDiPrelievo = False, "mo_quaeva", "mm_nprquaeva").ToString & " <> 0", "").ToString
      If (lDanumord <> 0) Or (lAnumord <> NTSCInt("".PadLeft(9, "9"c))) Then
        strSQL += " AND " & IIf(bNotaDiPrelievo = False, "td_numord", "tm_numdoc").ToString & " BETWEEN " & lDanumord & " AND " & lAnumord
      End If
      If (lCommecaIni <> 0) Or (lCommecaFin <> NTSCInt("".PadLeft(9, "9"c))) Then
        strSQL += " AND " & strPrefT & "commeca BETWEEN " & lCommecaIni & " AND " & lCommecaFin
      End If
      If (NTSCDate(strDadatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strAdatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND " & IIf(bNotaDiPrelievo = False, "td_datord", "tm_datdoc").ToString & " BETWEEN " & CDataSQL(strDadatord) & " AND " & CDataSQL(strAdatord)
      End If
      If bNotaDiPrelievo = False Then
        If (NTSCDate(strDadatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(dtadatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strSQL += " AND td_datcons BETWEEN " & CDataSQL(strDadatcons) & " AND " & CDataSQL(dtadatcons)
        End If
      End If
      strSQL += " GROUP BY " & strPrefT & "tipork, " & strPrefT & "anno, " & strPrefT & "serie, " & _
        IIf(bNotaDiPrelievo = False, "td_numord", "tm_numdoc").ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count > 0 Then
        For i As Integer = (dttOut.Rows.Count - 1) To 0 Step -1
          With dttOut.Rows(i)
            If NTSCStr(!td_tipork) = "Y" Then .Delete()
          End With
        Next
      End If
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiTestord(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal strWhere As String, _
                                                   ByVal lDanumord As Integer, ByVal lAnumord As Integer, ByVal strDadatord As String, _
                                                   ByVal strAdatord As String, ByVal strDadatcons As String, ByVal dtadatcons As String, _
                                                   ByVal lCommecaIni As Integer, ByVal lCommecaFin As Integer, ByVal lAnno As Integer, _
                                                   ByVal strSerie As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testord WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND td_flevas <> " & CStrSQL("S") & " AND " & strWhere & _
                 " AND td_numord BETWEEN " & lDanumord & " AND " & lAnumord & _
                 " AND td_datord BETWEEN " & CDataSQL(strDadatord) & " AND " & CDataSQL(strAdatord) & _
                 " AND td_datcons BETWEEN " & CDataSQL(strDadatcons) & " AND " & CDataSQL(dtadatcons) & _
                 " AND td_commeca BETWEEN " & lCommecaIni & " AND " & lCommecaFin & _
                 " AND td_anno = " & lAnno

      If strSerie <> "" Then strSQL = strSQL & " AND td_serie = " & CStrSQL(strSerie)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiMovord(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal dttTest As DataTable, _
                                                  ByVal i As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      With dttTest.Rows(i)
        strSQL = "SELECT TOP 1 mo_riga FROM movord" & _
                           " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                           " AND mo_tipork = " & CStrSQL(!td_tipork) & _
                           " AND mo_anno = " & NTSCInt(!td_anno) & _
                           " AND mo_serie = " & CStrSQL(!td_serie) & _
                           " AND mo_numord = " & NTSCInt(!td_numord) & _
                           " AND mo_quapre > 0"
      End With

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiMovord2(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal dttTest As DataTable, _
                                                   ByVal i As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      With dttTest.Rows(i)
        strSQL = "SELECT TOP 1 mo_tipork FROM movord" & _
                           " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                           " AND mo_tipork = " & CStrSQL("Y") & _
                           " AND mo_tiporkor = " & CStrSQL(!td_tipork) & _
                           " AND mo_annoor = " & NTSCInt(!td_anno) & _
                           " AND mo_serieor = " & CStrSQL(!td_serie) & _
                           " AND mo_numordor = " & NTSCInt(!td_numord) & _
                           " AND mo_quapre > 0"
      End With

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiMovordJoin(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal dttTest As DataTable, _
                                                      ByVal i As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_flevas," & _
               " mo_codart, mo_magaz, mo_magaz2, mo_valore, mo_quant, mo_quaeva," & _
               " (mo_quant - mo_quaeva) as QtaResidua, mo_fase, mo_commeca, mo_lotto," & _
               " mo_ubicaz, ar_gescomm, ar_geslotti" & _
               " FROM movord INNER JOIN artico ON movord.mo_codart = artico.ar_codart" & _
               " AND movord.codditt = artico.codditt " & _
               " WHERE movord.codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(dttTest.Rows(i)!td_tipork) & _
               " AND mo_anno = " & NTSCInt(dttTest.Rows(i)!td_anno) & _
               " AND mo_serie = " & CStrSQL(dttTest.Rows(i)!td_serie) & _
               " AND mo_numord = " & NTSCInt(dttTest.Rows(i)!td_numord) & _
               " AND mo_flevas = " & CStrSQL("C")

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiTestPRB(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal strSerie As String, _
                                                   ByVal lDanumord As Integer, ByVal lAnumord As Integer, ByVal strDadatord As String, _
                                                   ByVal strAdatord As String, ByVal lCommecaIni As Integer, ByVal lCommecaFin As Integer, _
                                                   ByVal lAnno As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testprb.* " & _
               " FROM testprb WHERE codditt = " & CStrSQL(strDitta) & _
               "  AND tm_tipork = 'W'" & _
               "  AND tm_anno = " & NTSCInt(lAnno) & _
               "  AND tm_numdoc BETWEEN " & NTSCInt(lDanumord) & " AND " & NTSCInt(lAnumord) & _
               "  AND tm_datdoc BETWEEN " & CDataSQL(strDadatord) & " AND " & CDataSQL(strAdatord) & _
               "  AND tm_commeca BETWEEN " & NTSCInt(lCommecaIni) & " AND " & NTSCInt(lCommecaFin) & _
               "  AND tm_flevas <> 'S' " & _
               "  AND tm_vistato IN ('N', 'S')"

      If strSerie <> "" Then strSQL = strSQL & " AND tm_serie = " & CStrSQL(strSerie)


      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiMovPRB(ByVal strDitta As String, ByRef dttOut As DataTable, ByVal dttTest As DataTable, _
                                                      ByVal i As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      With dttTest.Rows(i)
        strSQL = "SELECT mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_nprflevas, mm_codart, mm_magaz, " & _
                 "mm_nprvalore, mm_quant, mm_nprquaeva, mm_fase, mm_codart, mm_commeca, mm_lotto, mm_ubicaz, " & _
                 "(mm_quant - mm_nprquaeva) as QtaResidua FROM movprb" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_tipork = " & CStrSQL(!td_tipork) & _
                 " AND mm_anno = " & NTSCInt(!td_anno) & _
                 " AND mm_serie = " & CStrSQL(!td_serie) & _
                 " AND mm_numdoc = " & NTSCInt(!td_numord) & _
                 " AND mm_nprflevas = 'C'"
      End With

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function TransazioneOrdini(ByVal strDitta As String, ByVal strOpt As String, ByVal i As Integer, _
                                                 ByVal dttTest As DataTable, ByVal dttMov As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim k As Integer
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      For k = 0 To dttMov.Rows.Count - 1
        'Ordini di produzione, Ordine fornitore, Ordine fornitore aperto, Impegno di trasferimento
        If (strOpt = "OP" And NTSCStr(dttMov.Rows(k)!mo_tipork) = "H") _
            Or strOpt = "OF" Or strOpt = "OFA" Or strOpt = "IT" Then

          With dttMov.Rows(k)
            strSQL = "UPDATE artpro" & _
                     " SET ap_ordin = ap_ordin - " & NTSCInt(!QtaResidua) & "," & _
                     " ap_vordin = ap_vordin - " & NTSCInt(!mo_valore) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ap_codart = " & CStrSQL(!mo_codart) & _
                     " AND ap_magaz = " & NTSCInt(IIf(strOpt = "IT", !mo_magaz2, !mo_magaz)) & _
                     " AND ap_fase = " & NTSCInt(!mo_fase)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "UPDATE artprox" & _
                     " SET apx_ordin = apx_ordin - " & NTSCInt(!QtaResidua) & "," & _
                     " apx_vordin = apx_vordin - " & NTSCInt(!mo_valore) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND apx_codart = " & CStrSQL(!mo_codart) & _
                     " AND apx_fase = " & NTSCInt(!mo_fase)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "UPDATE lotcpro" & _
                     " SET lp_ordin = lp_ordin - " & NTSCInt(!QtaResidua) & "," & _
                     " lp_vordin = lp_vordin - " & NTSCInt(!mo_valore) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND lp_codart = " & CStrSQL(!mo_codart) & _
                     " AND lp_magaz = " & NTSCInt(IIf(strOpt = "IT", !mo_magaz2, !mo_magaz)) & _
                     " AND lp_commeca = " & NTSCInt(IIf(NTSCStr(!ar_gescomm) = "S", !mo_commeca, 0)) & _
                     " AND lp_lotto = " & NTSCInt(IIf(NTSCStr(!ar_geslotti) = "S", !mo_lotto, 0)) & _
                     " AND lp_fase = " & NTSCInt(!mo_fase) & _
                     " AND lp_ubicaz = " & CStrSQL(!mo_ubicaz)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End With
        End If

        'Impegno cliente, Impegno cliente aperto, Impegno di trasferimento
        If (strOpt = "OP" And NTSCStr(dttMov.Rows(k)!mo_tipork) = "Y") Or strOpt = "IC" Or strOpt = "ICA" Or strOpt = "IT" Then
          With dttMov.Rows(k)
            strSQL = "UPDATE artpro" & _
                    " SET ap_impeg = ap_impeg - " & NTSCInt(!QtaResidua) & "," & _
                    " ap_vimpeg = ap_vimpeg - " & NTSCInt(!mo_valore) & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND ap_codart = " & CStrSQL(!mo_codart) & _
                    " AND ap_magaz = " & NTSCInt(!mo_magaz) & _
                    " AND ap_fase = " & NTSCInt(!mo_fase)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "UPDATE artprox" & _
              " SET apx_impeg = apx_impeg - " & NTSCInt(!QtaResidua) & "," & _
              " apx_vimpeg = apx_vimpeg - " & NTSCInt(!mo_valore) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND apx_codart = " & CStrSQL(!mo_codart) & _
              " AND apx_fase = " & NTSCInt(!mo_fase)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "UPDATE lotcpro" & _
              " SET lp_impeg = lp_impeg - " & NTSCInt(!QtaResidua) & "," & _
              " lp_vimpeg = lp_vimpeg - " & NTSCInt(!mo_valore) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lp_codart = " & CStrSQL(!mo_codart) & _
              " AND lp_magaz = " & NTSCInt(!mo_magaz) & _
              " AND lp_commeca = " & NTSCInt(IIf(NTSCStr(!ar_gescomm) = "S", !mo_commeca, 0)) & _
              " AND lp_lotto = " & NTSCInt(IIf(NTSCStr(!ar_geslotti) = "S", !mo_lotto, 0)) & _
              " AND lp_fase = " & NTSCInt(!mo_fase) & _
              " AND lp_ubicaz = " & CStrSQL(!mo_ubicaz)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End With
        End If

        With dttTest.Rows(i)
          strSQL = "UPDATE movord SET mo_flevas = " & CStrSQL("S") & ", mo_valore = 0," & _
                   " mo_ultagg = " & CDataOraSQL(Now) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mo_tipork = " & CStrSQL(!td_tipork) & _
                   " AND mo_anno = " & NTSCInt(!td_anno) & _
                   " AND mo_serie = " & CStrSQL(!td_serie) & _
                   " AND mo_numord = " & NTSCInt(!td_numord) & _
                   " AND mo_riga =" & NTSCInt(dttMov.Rows(k)!mo_riga)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End With
      Next

      'Per gli ordini di produzione chiude le attività
      If strOpt = "OP" And NTSCStr(dttTest.Rows(i)!td_tipork) = "H" Then
        With dttTest.Rows(i)
          strSQL = "UPDATE attivit SET at_flevas = " & CStrSQL("S") & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND at_tipork = " & CStrSQL(!td_tipork) & _
                   " AND at_anno = " & NTSCInt(!td_anno) & _
                   " AND at_serie = " & CStrSQL(!td_serie) & _
                   " AND at_numord = " & NTSCInt(!td_numord)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End With
      End If

      With dttTest.Rows(i)
        strSQL = "UPDATE testord SET td_flevas = " & CStrSQL("S") & ", td_ultagg = " & CDataOraSQL(Now) & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND td_tipork = " & CStrSQL(!td_tipork) & _
                 " AND td_anno = " & NTSCInt(!td_anno) & _
                 " AND td_serie = " & CStrSQL(!td_serie) & _
                 " AND td_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TransazionePRB(ByVal strDitta As String, ByVal dttTest As DataTable, ByVal i As Integer, _
                                            ByVal dttMov As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim k As Integer
    Dim dttTmp As New DataTable()
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      For k = 0 To dttMov.Rows.Count - 1
        With dttMov.Rows(k)
          strSQL = "UPDATE artpro" & _
                   " SET ap_prenot = ap_prenot - " & NTSCInt(!QtaResidua) & "," & _
                   " ap_vprenot = ap_vprenot - " & NTSCInt(!mm_nprvalore) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ap_codart = " & CStrSQL(!mm_codart) & _
                   " AND ap_magaz = " & NTSCInt(!mm_magaz) & _
                   " AND ap_fase = " & NTSCInt(!mm_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "UPDATE artprox" & _
                   " SET apx_prenot = apx_prenot - " & NTSCInt(!QtaResidua) & "," & _
                   " apx_vprenot = apx_vprenot - " & NTSCInt(!mm_nprvalore) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND apx_codart = " & CStrSQL(!mm_codart) & _
                   " AND apx_fase = " & NTSCInt(!mm_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          ValCodiceDb(NTSCStr(!mm_codart), strDitta, "ARTICO", "S", "", dttTmp)

          strSQL = "UPDATE lotcpro" & _
                   " SET lp_prenot = lp_prenot - " & NTSCInt(!QtaResidua) & "," & _
                   " lp_vprenot = lp_vprenot - " & NTSCInt(!mm_nprvalore) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND lp_codart = " & CStrSQL(!mm_codart) & _
                   " AND lp_magaz = " & NTSCInt(!mm_magaz) & _
                   " AND lp_commeca = " & NTSCInt(IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) = "S", !mm_commeca, 0)) & _
                   " AND lp_lotto = " & NTSCInt(IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) = "S", !mm_lotto, 0)) & _
                   " AND lp_fase = " & NTSCInt(!mm_fase) & _
                   " AND lp_ubicaz = " & CStrSQL(!mm_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "UPDATE movprb SET mm_nprflevas = 'S', mm_valore = 0," & _
                   " mm_ultagg = " & CDataOraSQL(Now) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mm_tipork = " & CStrSQL(dttTest.Rows(i)!td_tipork) & _
                   " AND mm_anno = " & NTSCInt(dttTest.Rows(i)!td_anno) & _
                   " AND mm_serie = " & CStrSQL(dttTest.Rows(i)!td_serie) & _
                   " AND mm_numdoc = " & NTSCInt(dttTest.Rows(i)!td_numord) & _
                   " AND mm_riga =" & NTSCInt(!mm_riga)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End With
      Next

      With dttTest.Rows(i)
        strSQL = "UPDATE testprb SET tm_flevas = 'S', tm_ultagg = " & CDataOraSQL(Now) & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(!td_tipork) & _
                 " AND tm_anno = " & NTSCInt(!td_anno) & _
                 " AND tm_serie = " & CStrSQL(!td_serie) & _
                 " AND tm_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaPicking(ByVal strDitta As String, ByVal lIdPick As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM movpic " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mp_idpick = " & lIdPick

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM testpic " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tp_idpick = " & lIdPick

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tabpick " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codpick = " & lIdPick

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class