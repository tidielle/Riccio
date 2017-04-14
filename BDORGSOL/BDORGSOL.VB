Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORGSOL
  Inherits CLDMGDOCU


  Public strMainQuery As String = "SELECT ordlist.*, anagra.an_descr1 as xxo_conto, tb_desvalu as xxo_codvalu, 'N' as xxo_seleziona, " & _
              " tabmaga.tb_desmaga as xxo_magaz, tabmaga2.tb_desmaga as xxo_magaz2, tabmaga3.tb_desmaga as xxo_magimp, " & _
              " anagra1.an_descr1 as xxo_codclie, tb_desciva as xxo_codiva, tb_descove as xxo_controp, " & _
              " anagra2.an_descr1 as xxo_contocon, af_descr as xxo_fase, tsk_descr as xxo_pmtaskid, " & _
              " tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, " & _
              " tb_deslavo as xxo_codlavo, ar_codtagl as xxo_codtagl, ar_gescomm as xxo_gescomm, " & _
              " CASE WHEN ol_fase = 0 THEN ar_livmindb ELSE af_livmindb END as xxo_livmindb, " & _
              " ar_gesfasi as xxo_gesfasi, ar_coddb as xxo_coddb, ar_geslotti as xxo_geslotti, ar_gesubic as xxo_gesubic, ar_gestmatr as xxo_gestmatr, " & _
              " 0 as ec_anno, ' ' as ec_serie, 0 as ec_numdoc, ' ' as xxo_darave, artico.ar_tipoopz as xxo_tipoopz, " & _
              " 'H' as ec_tiporkor, 0 as ec_annoor, ' ' as ec_serieor, 0 as ec_numordor, analotti.alo_lottox as xxo_lottox, 0 as xxo_tcindtaglf, ' ' as xxo_tctagliaf, " & _
              " ar_descr as xxo_descrart, 0 as xxo_listpromo, ar_gruppo AS xxo_gruppo, ar_sotgru AS xxo_sotgru " & _
              " FROM (((((((((((((((((ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart) " & _
              " LEFT JOIN artfasi ON ordlist.codditt = artfasi.codditt AND ordlist.ol_codart = artfasi.af_codart AND ordlist.ol_fase = artfasi.af_fase) " & _
              " LEFT JOIN anagra ON ordlist.codditt = anagra.codditt AND ordlist.ol_conto = anagra.an_conto) " & _
              " LEFT JOIN anagra AS anagra1 ON ordlist.codditt = anagra1.codditt AND ordlist.ol_codclie = anagra1.an_conto) " & _
              " LEFT JOIN anagra AS anagra2 ON ordlist.codditt = anagra2.codditt AND ordlist.ol_contocontr = anagra2.an_conto) " & _
              " LEFT JOIN tabvalu ON ordlist.ol_codvalu = tabvalu.tb_codvalu) " & _
              " LEFT JOIN tabmaga ON ordlist.codditt = tabmaga.codditt AND ordlist.ol_magaz = tabmaga.tb_codmaga) " & _
              " LEFT JOIN tabmaga AS tabmaga2 ON ordlist.codditt = tabmaga2.codditt AND ordlist.ol_magaz2 = tabmaga2.tb_codmaga) " & _
              " LEFT JOIN tabmaga AS tabmaga3 ON ordlist.codditt = tabmaga3.codditt AND ordlist.ol_magimp = tabmaga3.tb_codmaga) " & _
              " LEFT JOIN tabciva ON ordlist.ol_codiva = tabciva.tb_codciva) " & _
              " LEFT JOIN tabcove ON ordlist.codditt = tabcove.codditt AND ordlist.ol_controp = tabcove.tb_codcove) " & _
              " LEFT JOIN tabcfam ON ordlist.codditt = tabcfam.codditt AND ordlist.ol_codcfam = tabcfam.tb_codcfam) " & _
              " LEFT JOIN commess ON ordlist.codditt = commess.codditt AND ordlist.ol_commeca = commess.co_comme) " & _
              " LEFT JOIN tabcena ON ordlist.codditt = tabcena.codditt AND ordlist.ol_codcena = tabcena.tb_codcena) " & _
              " LEFT JOIN tablavo ON ordlist.codditt = tablavo.codditt AND ordlist.ol_codlavo = tablavo.tb_codlavo) " & _
              " LEFT JOIN tasks ON ordlist.codditt = tasks.codditt AND ordlist.ol_commeca = tasks.tsk_commeca AND ordlist.ol_pmtaskid = tasks.tsk_taskid) " & _
              " LEFT JOIN analotti ON ordlist.codditt = analotti.codditt AND ordlist.ol_codart = analotti.alo_codart AND ordlist.ol_lotto = analotti.alo_lotto) "

  Public Overridable Function GetFirstMagazMercePropria(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 tb_codmaga, tb_desmaga FROM tabmaga" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tb_flclavo = ' '" & _
              " ORDER BY tb_codmaga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then GetFirstMagazMercePropria = NTSCInt(dttTmp.Rows(0)!tb_codmaga)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabgamaFromConto(ByVal strDitta As String, ByVal lConto As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTabgamaFromConto = 0

      strSQL = "SELECT tb_codmaga FROM tabmaga" & _
               " WHERE codditt  =" & CStrSQL(strDitta) & _
               " AND tb_magconto = " & lConto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then GetTabgamaFromConto = NTSCInt(dttTmp.Rows(0)!tb_codmaga)
      dttTmp.Clear()

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrdlist(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                      ByVal strSQLArtico As String, ByVal lConto As Integer, _
                                      ByVal lCommeca As Integer, ByVal nMagaz As Integer, _
                                      ByVal strDatordini As String, ByVal strDatordfin As String, _
                                      ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                      ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                      ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                      ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                      ByVal bEmOrdine As Boolean, ByVal lProgr As Integer, _
                                      ByVal nCodStabilimento As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strOrdlistTipork, strSQLArtico, lConto, lCommeca, nMagaz, _
                                             strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                                             bEmOrdine, lProgr, nCodStabilimento, dsOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(19), DataSet)        'alla funzione è passato ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      Return GetOrdlist(strDitta, strOrdlistTipork, strSQLArtico, lConto, lCommeca, nMagaz, _
                                             strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                                             bEmOrdine, lProgr, nCodStabilimento, dsOut, 0)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrdlist(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                    ByVal strSQLArtico As String, ByVal lConto As Integer, _
                                    ByVal lCommeca As Integer, ByVal nMagaz As Integer, _
                                    ByVal strDatordini As String, ByVal strDatordfin As String, _
                                    ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                    ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                    ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                    ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                    ByVal bEmOrdine As Boolean, ByVal lProgr As Integer, _
                                    ByVal nCodStabilimento As Integer, ByRef dsOut As DataSet, _
                                    ByVal lOrdinamento As Integer) As Boolean
    '-------------------
    'ottengo l'elenco dei documenti da generare
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim dttImpTc As New DataTable

    Try
      strSQL = strMainQuery

      strWhere = " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
              " AND ordlist.ol_tipork = " & CStrSQL(strOrdlistTipork) & _
              " AND ordlist.ol_datcons BETWEEN " & CDataSQL(strDatordini) & " AND " & CDataSQL(strDatordfin) & _
              " AND ordlist.ol_datord BETWEEN " & CDataSQL(strDatconsini) & " AND " & CDataSQL(strDatconsfin)
      If strSQLArtico <> "" Then
        TraduciWhere(strSQLArtico, strWhere)
      End If
      If lConto <> 0 Then strWhere += " AND ordlist.ol_conto = " & lConto.ToString
      If lCommeca <> 0 Then strWhere += " AND ordlist.ol_commeca = " & lCommeca.ToString
      If nMagaz <> 0 Then strWhere += " AND ordlist.ol_magaz = " & nMagaz.ToString
      If bGenerato = False Then strWhere += " AND ordlist.ol_stato <> ' '"
      If bConfermato = False Then strWhere += " AND ordlist.ol_stato <> 'S'"
      If bCongelato = False Then strWhere += " AND ordlist.ol_stato <> 'F'"
      If bEmRDA = False Then strWhere += " AND ordlist.ol_stato <> 'P'"
      If bAppRDA = False Then strWhere += " AND ordlist.ol_stato <> 'Q'"
      If bEmRDO = False Then strWhere += " AND ordlist.ol_stato <> 'R'"
      If bEmOrdine = False Then strWhere += " AND ordlist.ol_stato <> 'T'"
      If lProgr <> 0 Then strWhere += " AND ordlist.ol_progr = " & lProgr
      If nCodStabilimento <> 0 Then strWhere += " AND ordlist.ol_commen = " & nCodStabilimento

      strSQL += strWhere & " ORDER BY "

      If lOrdinamento = 0 Then
        strSQL += "ol_progr"
      ElseIf lOrdinamento = 1 Then
        strSQL += "ol_codart, ol_fase"
      ElseIf lOrdinamento = 2 Then
        strSQL += "xxo_descrart, xxo_fase"
      ElseIf lOrdinamento = 3 Then
        strSQL += "ol_conto"
      ElseIf lOrdinamento = 4 Then
        strSQL += "xxo_conto"
      ElseIf lOrdinamento = 5 Then
        strSQL += "ol_datcons"
      ElseIf lOrdinamento = 6 Then
        strSQL += "ol_magaz"
      ElseIf lOrdinamento = 7 Then
        strSQL += "ol_codcfam"
      ElseIf lOrdinamento = 8 Then
        strSQL += "ol_codcena"
      End If
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDLIST")

      '-------------------------
      strSQL = "SELECT ordlisttc.*, ' ' as ec_tipork, 0 as ec_anno, ' ' as ec_serie, 0 as ec_numdoc " & _
               " FROM ordlisttc " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ol_progr IN (SELECT ol_progr FROM ordlist INNER JOIN artico ON " & _
               " ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart " & strWhere & ")"
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDLISTTC", dsOut)

      '-------------------------
      'testord serve per fare in modo che ordlist venga gestito tipo testord/movord
      'vedi prime righe di commento di BEORGSOL
      strSQL = "SELECT TOP 0 testord.* " & _
               " FROM testord " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD", dsOut)

      '-------------------------
      'per ordini di prod


      If Not GetOrdListImpegni(strDitta, -1, dsOut) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrdListImpegni(ByVal strDitta As String, ByVal lProgrH As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttImp As New DataTable
    Dim nTcIndTaglF As Integer
    Dim nRiga As Integer
    Dim dttTmp As New DataTable
    Dim dtrRow As DataRow
    Dim dttDestagl As New DataTable
    Try
      strSQL = strMainQuery
      strSQL += " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
               " AND ordlist.ol_tipork = 'Y'" & _
               " AND ordlist.ol_olprogr = " & lProgrH.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDLISTIMP", dsOut)

      strSQL = "SELECT ordlisttc.*, ' ' as ec_tipork, 0 as ec_anno, ' ' as ec_serie, 0 as ec_numdoc" & _
         " FROM ordlisttc INNER JOIN ordlist ON ordlist.codditt = ordlisttc.codditt AND ordlist.ol_progr = ordlisttc.ol_progr" & _
         " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
         " AND ordlist.ol_tipork = 'Y'" & _
         " AND ordlist.ol_olprogr = " & lProgrH.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDLISTIMPTC", dsOut)

      For Each dtrRow2 As DataRow In dsOut.Tables("ORDLISTIMP").Rows

        strSQL = " SELECT * FROM ordlisttc" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ol_progr = " & NTSCInt(dtrRow2!ol_progr)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dtrRow = dttTmp.Rows(0)

          'xxo_tcindtaglf
          nTcIndTaglF = 0

          nRiga = NTSCInt(dtrRow!ol_progr)
          If NTSCDec(dtrRow!ol_quant01) <> 0 Then
            nTcIndTaglF = 1
          ElseIf NTSCDec(dtrRow!ol_quant02) <> 0 Then
            nTcIndTaglF = 2
          ElseIf NTSCDec(dtrRow!ol_quant03) <> 0 Then
            nTcIndTaglF = 3
          ElseIf NTSCDec(dtrRow!ol_quant04) <> 0 Then
            nTcIndTaglF = 4
          ElseIf NTSCDec(dtrRow!ol_quant05) <> 0 Then
            nTcIndTaglF = 5
          ElseIf NTSCDec(dtrRow!ol_quant06) <> 0 Then
            nTcIndTaglF = 6
          ElseIf NTSCDec(dtrRow!ol_quant07) <> 0 Then
            nTcIndTaglF = 7
          ElseIf NTSCDec(dtrRow!ol_quant08) <> 0 Then
            nTcIndTaglF = 8
          ElseIf NTSCDec(dtrRow!ol_quant09) <> 0 Then
            nTcIndTaglF = 9
          ElseIf NTSCDec(dtrRow!ol_quant10) <> 0 Then
            nTcIndTaglF = 10
          ElseIf NTSCDec(dtrRow!ol_quant11) <> 0 Then
            nTcIndTaglF = 11
          ElseIf NTSCDec(dtrRow!ol_quant12) <> 0 Then
            nTcIndTaglF = 12
          ElseIf NTSCDec(dtrRow!ol_quant13) <> 0 Then
            nTcIndTaglF = 13
          ElseIf NTSCDec(dtrRow!ol_quant14) <> 0 Then
            nTcIndTaglF = 14
          ElseIf NTSCDec(dtrRow!ol_quant15) <> 0 Then
            nTcIndTaglF = 15
          ElseIf NTSCDec(dtrRow!ol_quant16) <> 0 Then
            nTcIndTaglF = 16
          ElseIf NTSCDec(dtrRow!ol_quant17) <> 0 Then
            nTcIndTaglF = 17
          ElseIf NTSCDec(dtrRow!ol_quant18) <> 0 Then
            nTcIndTaglF = 18
          ElseIf NTSCDec(dtrRow!ol_quant19) <> 0 Then
            nTcIndTaglF = 19
          ElseIf NTSCDec(dtrRow!ol_quant20) <> 0 Then
            nTcIndTaglF = 20
          ElseIf NTSCDec(dtrRow!ol_quant21) <> 0 Then
            nTcIndTaglF = 21
          ElseIf NTSCDec(dtrRow!ol_quant22) <> 0 Then
            nTcIndTaglF = 22
          ElseIf NTSCDec(dtrRow!ol_quant23) <> 0 Then
            nTcIndTaglF = 23
          ElseIf NTSCDec(dtrRow!ol_quant24) <> 0 Then
            nTcIndTaglF = 24
          End If

          dtrRow2!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow2!ol_tcindtagl, nTcIndTaglF))
          ValCodiceDb(NTSCInt(dtrRow2!xxo_codtagl).ToString, strDitta, "TABTAGL", "N", "", dttDestagl)
          dtrRow2!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow2!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
          dtrRow2.AcceptChanges()
        End If
      Next

      strSQL = "SELECT zzattivit.*, 'H' as at_tipork, 0 as at_anno, ' ' as at_serie, 0 as at_numord, " & _
               " ar_descr as xx_coddb, tb_deslavo as xx_codlavo, " & _
               " 0.0 as xx_tempattpr, 0.0 as xx_tempesepr, " & _
               " 0.0 as xx_tempattprm, 0.0 as xx_tempeseprm, " & _
               " 0.0 as xx_tempattees, 0.0 as xx_tempesees " & _
               " FROM (zzattivit INNER JOIN artico ON zzattivit.codditt = artico.codditt AND zzattivit.at_coddb = artico.ar_codart) " & _
               " LEFT JOIN tablavo ON zzattivit.codditt = tablavo.codditt AND zzattivit.at_codlavo = tablavo.tb_codlavo "
      strSQL += " WHERE zzattivit.codditt = " & CStrSQL(strDitta) & _
               " AND at_progr = " & lProgrH.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ATTIVIT", dsOut)

      For Each dtrT As DataRow In dsOut.Tables("ATTIVIT").Rows
        dtrT!xx_tempattpr = ConvOra100Ora60(NTSCDec(dtrT!at_tempattpr))
        dtrT!xx_tempesepr = ConvOra100Ora60(NTSCDec(dtrT!at_tempesepr))
        dtrT!xx_tempattprm = ConvOra60Minuti(NTSCDec(dtrT!xx_tempattpr))
        dtrT!xx_tempeseprm = ConvOra60Minuti(NTSCDec(dtrT!xx_tempesepr))
        dtrT!xx_tempattees = ConvOra100Ora60(NTSCDec(dtrT!at_tempattees))
        dtrT!xx_tempesees = ConvOra100Ora60(NTSCDec(dtrT!at_tempesees))
      Next

      strSQL = "SELECT zzassris.*, 'H' as as_tipork, 0 as as_anno, ' ' as as_serie, 0 as as_numord, " & _
               " tb_descent as xx_codcent, tb_descove as xx_controp " & _
               " FROM (zzassris LEFT JOIN tabcent ON zzassris.codditt = tabcent.codditt AND zzassris.as_codcent = tabcent.tb_codcent) " & _
               " LEFT JOIN tabcove ON zzassris.codditt = tabcove.codditt AND zzassris.as_controp = tabcove.tb_codcove "
      strSQL += " WHERE zzassris.codditt = " & CStrSQL(strDitta) & _
              " AND as_progr = " & lProgrH.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ASSRIS", dsOut)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdlist(ByVal strDitta As String, ByRef ds As DataSet, ByVal lProgr As Integer, _
                                           ByVal strTipork As String, ByVal strState As String, ByVal bModTCO As Boolean, _
                                           ByVal bRiscriviImpegni As Boolean) As Boolean
    'obsoleta
    Return SalvaOrdlist(strDitta, ds, lProgr, strTipork, strState, bModTCO, bRiscriviImpegni, False)
  End Function
  Public Overridable Function SalvaOrdlist(ByVal strDitta As String, ByRef ds As DataSet, ByVal lProgr As Integer, _
                                           ByVal strTipork As String, ByVal strState As String, ByVal bModTCO As Boolean, _
                                           ByVal bRiscriviImpegni As Boolean, ByVal bGestioneAbbinamentiTaglie As Boolean) As Boolean
    'bSetStatoOrdlist: se impostata a true cancellando l'ordine non verrà riaperta l'eventuale RDA collegata
    'strState: N = new, D = delete, U = update

    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim lResult As Integer = 0
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrTC() As DataRow = Nothing
    Dim j As Integer = 0

    Dim dbConn As DbConnection = Nothing
    Dim dttFieldAlias As New DataTable
    Dim dttFieldAliasTC As New DataTable
    Dim dttFieldAliasAtt As New DataTable
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lProgr, strTipork, strState, bModTCO, _
                                             bRiscriviImpegni, bGestioneAbbinamentiTaglie})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------
      'serve per rinominare le colonne precedentemenre standardizzate
      dttFieldAlias.Columns.Add("datatable", GetType(String))
      dttFieldAlias.Columns.Add("database", GetType(String))
      dttFieldAlias.Rows.Add(New Object() {"ec_riga", "ol_progr"})      'colonna da rinominare nella query
      dttFieldAlias.Rows.Add(New Object() {"ec_rigaor", "ol_olprogr"})  'colonna da rinominare nella query
      dttFieldAlias.Rows.Add(New Object() {"ec_anno", ""})              'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_serie", ""})             'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_numdoc", ""})            'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_tiporkor", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_annoor", ""})            'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_serieor", ""})           'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ec_numordor", ""})          'colonna da escludere dalla query
      dttFieldAlias.AcceptChanges()

      dttFieldAliasTC.Columns.Add("datatable", GetType(String))
      dttFieldAliasTC.Columns.Add("database", GetType(String))
      dttFieldAliasTC.Rows.Add(New Object() {"ec_riga", "ol_progr"})  'colonna da rinominare nella query
      dttFieldAliasTC.Rows.Add(New Object() {"ec_tipork", ""})        'colonna da escludere dalla query
      dttFieldAliasTC.Rows.Add(New Object() {"ec_anno", ""})          'colonna da escludere dalla query
      dttFieldAliasTC.Rows.Add(New Object() {"ec_serie", ""})         'colonna da escludere dalla query
      dttFieldAliasTC.Rows.Add(New Object() {"ec_numdoc", ""})        'colonna da escludere dalla query
      dttFieldAliasTC.AcceptChanges()

      dttFieldAliasAtt.Columns.Add("datatable", GetType(String))
      dttFieldAliasAtt.Columns.Add("database", GetType(String))
      dttFieldAliasAtt.Rows.Add(New Object() {"at_riga", "at_progr"})  'colonna da rinominare nella query
      dttFieldAliasAtt.Rows.Add(New Object() {"at_tipork", ""})        'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"at_anno", ""})          'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"at_serie", ""})         'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"at_numord", ""})        'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"as_riga", "as_progr"})  'colonna da rinominare nella query
      dttFieldAliasAtt.Rows.Add(New Object() {"as_tipork", ""})        'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"as_anno", ""})          'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"as_serie", ""})         'colonna da escludere dalla query
      dttFieldAliasAtt.Rows.Add(New Object() {"as_numord", ""})        'colonna da escludere dalla query
      dttFieldAliasAtt.AcceptChanges()

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      '---------------------------------
      'cancello
      If strState = "D" Then
        If strTipork = "H" Then
          If bModTCO Then
            strSQL = "DELETE FROM ordlisttc WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr IN (SELECT ol_progr FROM ordlist " & _
                                                                                                      " WHERE codditt = " & CStrSQL(strDitta) & " AND ol_olprogr = " & lProgr.ToString & ")"
            lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
          strSQL = "DELETE FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & " AND ol_olprogr = " & lProgr.ToString
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM zzassris WHERE codditt = " & CStrSQL(strDitta) & " AND as_progr = " & lProgr.ToString
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM zzattivit WHERE codditt = " & CStrSQL(strDitta) & " AND at_progr = " & lProgr.ToString
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strSQL = "DELETE FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & lProgr.ToString
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If bModTCO Then
          strSQL = "DELETE FROM ordlisttc WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & lProgr.ToString
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Else
        dtrT = ds.Tables("CORPO").Select("ec_riga = " & lProgr.ToString)

        '---------------------------------
        'inserisco
        If dtrT(0).RowState = DataRowState.Added Then
          strSQL = "INSERT INTO ordlist " & GetQueryInsertField(ds.Tables("CORPO"), "ol_", "", "ec_", dttFieldAlias)
          strSQLVal = GetQueryInsertValue(ds.Tables("CORPO"), dtrT(0), "ol_", "", "ec_", dttFieldAlias)
          lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
          If bModTCO Then
            dtrTC = ds.Tables("CORPOTC").Select("ec_riga = " & lProgr.ToString)
            If dtrTC.Length > 0 Then
              strSQL = "INSERT INTO ordlisttc " & GetQueryInsertField(ds.Tables("CORPOTC"), "ol_", "", "ec_", dttFieldAliasTC)
              strSQLVal = GetQueryInsertValue(ds.Tables("CORPOTC"), dtrTC(0), "ol_", "", "ec_", dttFieldAliasTC)
              lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          End If
        End If

        '---------------------------------
        'aggiorno
        If dtrT(0).RowState = DataRowState.Modified Then
          strSQL = "UPDATE ordlist SET " & _
                   GetQueryUpdate(ds.Tables("CORPO"), dtrT(0), "ol_", "", "ec_", dttFieldAlias) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & lProgr.ToString
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If bModTCO Then
            dtrTC = ds.Tables("CORPOTC").Select("ec_riga = " & lProgr.ToString)
            If dtrTC.Length > 0 Then
              strSQL = "UPDATE ordlisttc SET " & _
                       GetQueryUpdate(ds.Tables("CORPOTC"), dtrTC(0), "ol_", "", "ec_", dttFieldAliasTC) & _
                       " WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & lProgr.ToString
              lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          End If
        End If

        '----------------------------
        'aggiorno gli impegni / attivit / assris
        If strTipork = "H" And bRiscriviImpegni Then
          'cancello e reinserisco tutto degli impegni e scarichi
          If strTipork = "H" Then
            If bModTCO Then
              strSQL = "DELETE FROM ordlisttc WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr IN (SELECT ol_progr FROM ordlist " & _
                                                                                                        " WHERE codditt = " & CStrSQL(strDitta) & " AND ol_olprogr = " & dtrT(0)!ec_riga.ToString & ")"
              lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
            strSQL = "DELETE FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & " AND ol_olprogr = " & dtrT(0)!ec_riga.ToString
            lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM zzassris WHERE codditt = " & CStrSQL(strDitta) & " AND as_progr = " & dtrT(0)!ec_riga.ToString
            lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM zzattivit WHERE codditt = " & CStrSQL(strDitta) & " AND at_progr = " & dtrT(0)!ec_riga.ToString
            lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          'reinserisco le righe Y solo se è presente la riga H: è sucesso che reinserivo le righe Y anche senza riga H nel seguente caso:
          '2 sessioni aperte di business con gsol aperto su OP in contemporanea con 2 OP. su una sessione cancello la riga 1
          'sull'altra sessioen cancello la riga 2. esco da entrambe le istanze: uscendo dalla seconda cerca di risalvare la riga H rimasta in memoria
          '(non modificata): la riga H non viene risalvata perchè va in update, mentre le righe Y vengono risalvate perchè delete + insert!
          strSQL = "SELECT ol_progr FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & dtrT(0)!ec_riga.ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then

            'inserisco ordlist e ordlisttc
            dtrT = ds.Tables("CORPOIMP").Select("", "")
            If dtrT.Length > 0 Then
              For i = 0 To dtrT.Length - 1
                strSQL = "INSERT INTO ordlist " & GetQueryInsertField(ds.Tables("CORPOIMP"), "ol_", "", "ec_", dttFieldAlias)
                strSQLVal = GetQueryInsertValue(ds.Tables("CORPOIMP"), dtrT(i), "ol_", "", "ec_", dttFieldAlias)
                lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
                If bModTCO Then
                  dtrTC = ds.Tables("CORPOIMPTC").Select("ec_riga = " & dtrT(i)!ec_riga.ToString)
                  If dtrTC.Length > 0 Then
                    For j = 1 To 24
                      If dtrT(i).Table.Columns.Contains("xxo_tcindtaglf") Then
                        If dtrT(i)!xxo_tcindtaglf.Equals(System.DBNull.Value) Then
                          'Riga nuova
                          If j = NTSCInt(dtrT(i)!ec_tcindtagl) Then
                            dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrT(i)!ec_quant)
                          Else
                            dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = 0
                          End If
                        Else
                          'Riga già inserita
                          If j = NTSCInt(dtrT(i)!xxo_tcindtaglf) Then
                            dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrT(i)!ec_quant)
                          Else
                            dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = 0
                          End If
                        End If
                      Else
                        If j = NTSCInt(dtrT(i)!ec_tcindtagl) Then
                          dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrT(i)!ec_quant)
                        Else
                          dtrTC(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = 0
                        End If
                      End If
                    Next
                    strSQL = "INSERT INTO ordlisttc " & GetQueryInsertField(ds.Tables("CORPOIMPTC"), "ol_", "", "ec_", dttFieldAliasTC)
                    strSQLVal = GetQueryInsertValue(ds.Tables("CORPOTC"), dtrTC(0), "ol_", "", "ec_", dttFieldAliasTC)
                    lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
                  End If
                End If
              Next
            End If

            'inserisco zzattivit
            dtrT = ds.Tables("ATTIVIT").Select("", "")
            If dtrT.Length > 0 Then
              strSQL = "INSERT INTO zzattivit " & GetQueryInsertField(ds.Tables("ATTIVIT"), "at_", "", "at_", dttFieldAliasAtt)
              For i = 0 To dtrT.Length - 1
                strSQLVal = GetQueryInsertValue(ds.Tables("ATTIVIT"), dtrT(i), "at_", "", "at_", dttFieldAliasAtt)
                lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
              Next
            End If

            'inserisco zzassris
            dtrT = ds.Tables("ASSRIS").Select("", "")
            If dtrT.Length > 0 Then
              strSQL = "INSERT INTO zzassris " & GetQueryInsertField(ds.Tables("ASSRIS"), "as_", "", "as_", dttFieldAliasAtt)
              For i = 0 To dtrT.Length - 1
                strSQLVal = GetQueryInsertValue(ds.Tables("ASSRIS"), dtrT(i), "as_", "", "as_", dttFieldAliasAtt)
                lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
              Next
            End If

          End If    'If dttTmp.Rows.Count > 0 Then
          dttTmp.Clear()

        End If    'If strTipork = "H" And bRiscriviImpegni  Then

      End If    'If strState = "D" Then

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RiempiTmpTable(ByVal strDitta As String, ByVal strTipork As String, ByVal lIIAssris As Integer, _
                                            ByVal lIIAttivit As Integer, ByVal lIIOrlist As Integer, ByVal lIIOrdl As Integer, _
                                            ByVal lIIOrlisttc As Integer, ByVal strProgr As String, ByVal bModTCO As Boolean, _
                                            ByVal bSoloImpegni As Boolean, ByVal bSoloAttivita As Boolean) As Boolean
    'strProgr = ol_progr separati da ','
    Dim strSQL As String = ""
    Try

      '-----------------------------------------------------------------------------------------
      ResetTblInstId("TTORDL", False, lIIOrdl)
      ResetTblInstId("TTORLIST", False, lIIOrlist)
      ResetTblInstId("TTATTIVIT", False, lIIAttivit)
      ResetTblInstId("TTASSRIS", False, lIIAssris)
      ResetTblInstId("TTORLISTTC", False, lIIOrlisttc)
      '-----------------------------------------------------------------------------------------

      strSQL = "INSERT INTO TTORDL (codditt, instid, ol_progr, ol_tipork, ol_datord, ol_conto," & _
                    " ol_codart, ol_datcons, ol_magaz, ol_magaz2, ol_unmis, ol_descr, ol_colli, ol_quant," & _
                    " ol_prezzo, ol_scont1, ol_scont2, ol_scont3, ol_codiva, ol_prezvalc, ol_commen, ol_note," & _
                    " ol_controp, ol_stasino, ol_olprogr, ol_magimp, ol_prelist, ol_codcfam, ol_commeca," & _
                    " ol_subcommeca, ol_valore, ol_contocontr, ol_codcena, ol_desint, ol_codvuo, ol_ump," & _
                    " ol_codvalu, ol_stato, ol_ricimp, ol_codclie, ol_lotto, ol_misura1, ol_misura2, ol_misura3, ol_perqta," & _
                    " ol_valorev, ol_scont4, ol_scont5, ol_scont6, ol_fase, ol_datini, ol_datfin, ol_tctaglia, ol_tcindtagl)" & _
                    " SELECT " & CStrSQL(strDitta) & ", " & lIIOrdl & ", ol_progr, ol_tipork, ol_datord, ol_conto, ol_codart, ol_datcons," & _
                    " ol_magaz, ol_magaz2, ol_unmis, ol_descr, ol_colli, ol_quant, ol_prezzo, ol_scont1," & _
                    " ol_scont2, ol_scont3, ol_codiva, ol_prezvalc, ol_commen, ol_note, ol_controp, ol_stasino," & _
                    " ol_olprogr, ol_magimp, ol_prelist, ol_codcfam, ol_commeca, ol_subcommeca, ol_valore," & _
                    " ol_contocontr, ol_codcena, ol_desint, ol_codvuo, ol_ump, ol_codvalu, ol_stato, ol_ricimp," & _
                    " ol_codclie, ol_lotto, ol_misura1, ol_misura2, ol_misura3, ol_perqta, ol_valorev," & _
                    " ol_scont4, ol_scont5, ol_scont6, ol_fase, ol_datini, ol_datfin, ol_tctaglia, ol_tcindtagl" & _
                    " FROM ordlist" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND ol_tipork = '" & strTipork & "'" & _
                    " AND ol_progr IN (" & strProgr & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '---------------------------------------------------------------------------------------
      If bModTCO Then
        strSQL = "INSERT INTO TTORLISTTC (instid, codditt, ol_progr, " & _
                    " ol_quant01, ol_quant02, ol_quant03, ol_quant04, ol_quant05, ol_quant06," & _
                    " ol_quant07, ol_quant08, ol_quant09, ol_quant10, ol_quant11, ol_quant12," & _
                    " ol_quant13, ol_quant14, ol_quant15, ol_quant16, ol_quant17, ol_quant18," & _
                    " ol_quant19, ol_quant20, ol_quant21, ol_quant22, ol_quant23, ol_quant24)" & _
                    " SELECT " & lIIOrlisttc & ", " & CStrSQL(strDitta) & ", ordlisttc.ol_progr," & _
                    " ol_quant01, ol_quant02, ol_quant03, ol_quant04, ol_quant05, ol_quant06," & _
                    " ol_quant07, ol_quant08, ol_quant09, ol_quant10, ol_quant11, ol_quant12," & _
                    " ol_quant13, ol_quant14, ol_quant15, ol_quant16, ol_quant17, ol_quant18," & _
                    " ol_quant19, ol_quant20, ol_quant21, ol_quant22, ol_quant23, ol_quant24" & _
                    " FROM ordlisttc INNER JOIN ordlist ON ordlist.codditt = ordlisttc.codditt AND ordlist.ol_progr = ordlisttc.ol_progr" & _
                    " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
                    " AND ordlist.ol_tipork = " & CStrSQL(strTipork) & _
                    " AND ordlist.ol_progr IN (" & strProgr & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '---------------------------------------------------------------------------------------

      '-----------------------------------------------------------------------------------------
      If strTipork = "H" Then
        If bSoloImpegni = True Then
          strSQL = "INSERT INTO TTORLIST (codditt, instid, ol_progr, ol_tipork, ol_datord, ol_conto," & _
                    " ol_codart, ol_datcons, ol_magaz, ol_magaz2, ol_unmis, ol_descr, ol_colli, ol_quant," & _
                    " ol_prezzo, ol_scont1, ol_scont2, ol_scont3, ol_codiva, ol_prezvalc, ol_commen, ol_note," & _
                    " ol_controp, ol_stasino, ol_olprogr, ol_magimp, ol_prelist, ol_codcfam, ol_commeca," & _
                    " ol_subcommeca, ol_valore, ol_contocontr, ol_codcena, ol_desint, ol_codvuo, ol_ump," & _
                    " ol_codvalu, ol_stato, ol_ricimp, ol_codclie, ol_lotto, ol_misura1, ol_misura2, ol_misura3, ol_perqta," & _
                    " ol_valorev, ol_scont4, ol_scont5, ol_scont6, ol_fase, ol_datini, ol_datfin, ol_tctaglia, ol_tcindtagl)" & _
                    " SELECT " & CStrSQL(strDitta) & ", " & lIIOrlist & ", ol_progr, ol_tipork, ol_datord, ol_conto, ol_codart, ol_datcons," & _
                    " ol_magaz, ol_magaz2, ol_unmis, ol_descr, ol_colli, ol_quant, ol_prezzo, ol_scont1," & _
                    " ol_scont2, ol_scont3, ol_codiva, ol_prezvalc, ol_commen, ol_note, ol_controp, ol_stasino," & _
                    " ol_olprogr, ol_magimp, ol_prelist, ol_codcfam, ol_commeca, ol_subcommeca, ol_valore," & _
                    " ol_contocontr, ol_codcena, ol_desint, ol_codvuo, ol_ump, ol_codvalu, ol_stato, ol_ricimp," & _
                    " ol_codclie, ol_lotto, ol_misura1, ol_misura2, ol_misura3, ol_perqta, ol_valorev," & _
                    " ol_scont4, ol_scont5, ol_scont6, ol_fase, ol_datini, ol_datfin, ol_tctaglia, ol_tcindtagl" & _
                    " FROM ordlist" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND ol_tipork = 'Y'" & _
                    " AND ol_olprogr IN (" & strProgr & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          If bModTCO Then
            'ORDLISTTC "Y"
            strSQL = "INSERT INTO TTORLISTTC (instid, codditt, ol_progr," & _
                    " ol_quant01, ol_quant02, ol_quant03, ol_quant04, ol_quant05, ol_quant06," & _
                    " ol_quant07, ol_quant08, ol_quant09, ol_quant10, ol_quant11, ol_quant12," & _
                    " ol_quant13, ol_quant14, ol_quant15, ol_quant16, ol_quant17, ol_quant18," & _
                    " ol_quant19, ol_quant20, ol_quant21, ol_quant22, ol_quant23, ol_quant24)" & _
                    " SELECT " & lIIOrlisttc & ", " & CStrSQL(strDitta) & "," & _
                    " ordlisttc.ol_progr," & _
                    " ol_quant01, ol_quant02, ol_quant03, ol_quant04, ol_quant05, ol_quant06," & _
                    " ol_quant07, ol_quant08, ol_quant09, ol_quant10, ol_quant11, ol_quant12," & _
                    " ol_quant13, ol_quant14, ol_quant15, ol_quant16, ol_quant17, ol_quant18," & _
                    " ol_quant19, ol_quant20, ol_quant21, ol_quant22, ol_quant23, ol_quant24" & _
                    " FROM ordlisttc" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND ol_progr IN ( SELECT ol_progr FROM TTORLIST " & _
                                      " WHERE instid = " & lIIOrlist & _
                                      " AND codditt = " & CStrSQL(strDitta) & _
                                      " AND ol_tipork = 'Y')"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If

        End If
        If bSoloAttivita = True Then
          strSQL = "INSERT INTO TTATTIVIT (codditt, instid, at_progr, at_fase, at_coddb, at_codlavo," & _
                    " at_idproj, at_tempattpr, at_tempesepr, at_tempattees, at_tempesees, at_flevas, at_dtminima," & _
                    " at_suolprogr, at_sufase, at_relazsuc, at_qtapr, at_qtaes, at_datcons, at_terzista," & _
                    " at_sutipork, at_suanno, at_suserie, at_sunumord, at_suriga, at_note)" & _
                    " SELECT " & CStrSQL(strDitta) & ", " & lIIAttivit & ", at_progr, at_fase, at_coddb, at_codlavo, at_idproj, at_tempattpr," & _
                    " at_tempesepr, at_tempattees, at_tempesees, at_flevas, at_dtminima, at_suolprogr," & _
                    " at_sufase, at_relazsuc, at_qtapr, at_qtaes, at_datcons, at_terzista, at_sutipork," & _
                    " at_suanno, at_suserie, at_sunumord, at_suriga, at_note FROM zzattivit" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND at_progr IN (" & strProgr & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          strSQL = "INSERT INTO TTASSRIS (codditt, instid, as_progr, as_fase, as_codcent," & _
                    " as_percent, as_valore, as_valmo, as_controp, as_contocontr, as_codcena," & _
                    " as_cmacora, as_pagaora, as_pagaoratt, as_cmacoratt)" & _
                    " SELECT " & CStrSQL(strDitta) & ", " & lIIAssris & ", as_progr, as_fase, as_codcent, as_percent, as_valore," & _
                    " as_valmo, as_controp, as_contocontr, as_codcena, as_cmacora, as_pagaora," & _
                    " as_pagaoratt, as_cmacoratt FROM zzassris" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND as_progr IN (" & strProgr & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaLLC(ByVal dtrRow As DataRow, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If NTSCInt(dtrRow!ec_fase) = 0 Then
        strSQL = "SELECT ar_livmindb AS xx_livmindb FROM artico " & _
                 "WHERE ar_codart = " & CStrSQL(dtrRow!ec_codart) & _
                 " AND codditt = " & CStrSQL(dtrRow!codditt)
      Else
        strSQL = "SELECT af_livmindb AS xx_livmindb FROM artfasi " & _
                 "WHERE af_codart = " & CStrSQL(dtrRow!ec_codart) & _
                 " AND af_fase = " & CStrSQL(dtrRow!ec_fase) & _
                 " AND codditt = " & CStrSQL(dtrRow!codditt)
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetFornEMagazFromCicli(ByVal strDitta As String, ByVal strCoddb As String, _
                                                   ByVal nFase As Integer, ByVal strDtVal As String, _
                                                   ByRef lForn As Integer, ByRef nMagprod As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      lForn = 0
      nMagprod = 0
      strSQL = "SELECT dd_codcent, dd_magcent, dd_magtran, tabcent.tb_magterz, tabmaga.tb_magconto  " & _
               " FROM cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt " & _
               " AND cicli.dd_codcent = tabcent.tb_codcent " & _
               " INNER JOIN tabmaga ON tabcent.codditt = tabmaga.codditt " & _
               " AND tabcent.tb_magterz = tabmaga.tb_codmaga" & _
               " WHERE cicli.codditt = " & CStrSQL(strDitta) & _
               " AND dd_coddb = " & CStrSQL(strCoddb) & _
               " AND dd_fase = " & nFase.ToString & _
               " AND dd_dtinval <= " & CDataSQL(strDtVal) & _
               " AND dd_dtfival >= " & CDataSQL(strDtVal)
      'strDtVal
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        lForn = NTSCInt(dttTmp.Rows(0)!tb_magconto)
        If NTSCInt(dttTmp.Rows(0)!dd_magtran) = 0 Then
          nMagprod = NTSCInt(dttTmp.Rows(0)!dd_magcent)
        Else
          nMagprod = NTSCInt(dttTmp.Rows(0)!dd_magtran)
        End If
        If nMagprod = 0 Then nMagprod = NTSCInt(dttTmp.Rows(0)!tb_magterz)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
End Class
