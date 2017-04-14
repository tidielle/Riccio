Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLD__FLDO
  Inherits CLD__BASE


  Public Overridable Function GetTableFiltri(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "select cb_tipocampo as tipo, 'f' + cb_nomcampo as val, replace(cb_descampo, 'testmag', 'Fattura') as cod " & _
               " from campi " & _
               " where cb_nomtab = 'testmag'" & _
               " union all" & _
               " select cb_tipocampo as tipo, cb_nomcampo, replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(cb_descampo, 'testmag', 'Doc. testa'), 'movmag', 'Doc. corpo'), 'testord', 'Ord. testa'), 'movord', 'Ord. corpo'), 'anagra', 'Cli/forn'), 'leads', 'Lead'), 'artico', 'Articoli'), 'prinot', 'Prima nota'), 'scaden', 'Scadenze'), 'testoff', 'Off. testa'), 'movoff', 'Off. corpo'), 'testprb', 'Nota prel. testa'), 'movprb', 'Nota prel. corpo')" & _
               " from campi" & _
               " where cb_nomtab in ('testmag', 'movmag', 'testord', 'movord', 'testoff', 'movoff', 'prinot', 'scaden', 'anagra', 'leads', 'artico', 'testprb', 'movprb') " & _
               " and cb_tipocampo <> 11 and cb_tipocampo <> 12" & _
               " order by cod"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SpezzaFiltri(ByVal strQuery As String, _
                                          ByRef strFiltriTestoff As String, ByRef strFiltriMovoff As String, _
                                          ByRef strFiltriTestord As String, ByRef strFiltriMovord As String, _
                                          ByRef strFiltriTestprb As String, ByRef strFiltriMovprb As String, _
                                          ByRef strFiltriTestmag As String, ByRef strFiltriMovmag As String, _
                                          ByRef strFiltriTestmagf As String, ByRef strFiltriprinot As String, _
                                          ByRef strFiltriScaden As String, ByRef strFiltriAnagra As String, _
                                          ByRef strFiltriLeads As String, ByRef strFiltriArtico As String) As Boolean
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      strFiltriTestoff = ""
      strFiltriMovoff = ""
      strFiltriTestord = ""
      strFiltriMovord = ""
      strFiltriTestprb = ""
      strFiltriMovprb = ""
      strFiltriTestmag = ""
      strFiltriMovmag = ""
      strFiltriTestmagf = ""
      strFiltriprinot = ""
      strFiltriScaden = ""
      strFiltriAnagra = ""
      strFiltriLeads = ""
      strFiltriArtico = ""

      If strQuery = "" Then Return True

      strT = strQuery.Split("§"c)
      For i = 0 To strT.Length - 1
        If strT(i).Substring(0, 8) = "testoff." Then strFiltriTestoff += strT(i) & "§"
        If strT(i).Substring(0, 7) = "movoff." Then strFiltriMovoff += strT(i) & "§"
        If strT(i).Substring(0, 8) = "testord." Then strFiltriTestord += strT(i) & "§"
        If strT(i).Substring(0, 7) = "movord." Then strFiltriMovord += strT(i) & "§"
        If strT(i).Substring(0, 8) = "testprb." Then strFiltriTestprb += strT(i) & "§"
        If strT(i).Substring(0, 7) = "movprb." Then strFiltriMovprb += strT(i) & "§"
        If strT(i).Substring(0, 8) = "testmag." Then strFiltriTestmag += strT(i) & "§"
        If strT(i).Substring(0, 7) = "movmag." Then strFiltriMovmag += strT(i) & "§"
        If strT(i).Substring(0, 9) = "ftestmag." Then strFiltriTestmagf += strT(i).Substring(1) & "§"
        If strT(i).Substring(0, 7) = "prinot." Then strFiltriprinot += strT(i) & "§"
        If strT(i).Substring(0, 7) = "scaden." Then strFiltriScaden += strT(i) & "§"
        If strT(i).Substring(0, 7) = "anagra." Then strFiltriAnagra += strT(i) & "§"
        If strT(i).Substring(0, 6) = "leads." Then strFiltriLeads += strT(i) & "§"
        If strT(i).Substring(0, 7) = "artico." Then strFiltriArtico += strT(i) & "§"
      Next

      If strFiltriTestoff <> "" Then strFiltriTestoff = strFiltriTestoff.Substring(0, strFiltriTestoff.Length - 1)
      If strFiltriMovoff <> "" Then strFiltriMovoff = strFiltriMovoff.Substring(0, strFiltriMovoff.Length - 1)
      If strFiltriTestord <> "" Then strFiltriTestord = strFiltriTestord.Substring(0, strFiltriTestord.Length - 1)
      If strFiltriMovord <> "" Then strFiltriMovord = strFiltriMovord.Substring(0, strFiltriMovord.Length - 1)
      If strFiltriTestprb <> "" Then strFiltriTestprb = strFiltriTestprb.Substring(0, strFiltriTestprb.Length - 1)
      If strFiltriMovprb <> "" Then strFiltriMovprb = strFiltriMovprb.Substring(0, strFiltriMovprb.Length - 1)
      If strFiltriTestmag <> "" Then strFiltriTestmag = strFiltriTestmag.Substring(0, strFiltriTestmag.Length - 1)
      If strFiltriMovmag <> "" Then strFiltriMovmag = strFiltriMovmag.Substring(0, strFiltriMovmag.Length - 1)
      If strFiltriTestmagf <> "" Then strFiltriTestmagf = strFiltriTestmagf.Substring(0, strFiltriTestmagf.Length - 1)
      If strFiltriprinot <> "" Then strFiltriprinot = strFiltriprinot.Substring(0, strFiltriprinot.Length - 1)
      If strFiltriScaden <> "" Then strFiltriScaden = strFiltriScaden.Substring(0, strFiltriScaden.Length - 1)
      If strFiltriAnagra <> "" Then strFiltriAnagra = strFiltriAnagra.Substring(0, strFiltriAnagra.Length - 1)
      If strFiltriLeads <> "" Then strFiltriLeads = strFiltriLeads.Substring(0, strFiltriLeads.Length - 1)
      If strFiltriArtico <> "" Then strFiltriArtico = strFiltriArtico.Substring(0, strFiltriArtico.Length - 1)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataTesta(ByVal strDitta As String, ByVal bOfferte As Boolean, _
                                            ByVal bOrdini As Boolean, ByVal bNote As Boolean, _
                                            ByVal bMagaz As Boolean, ByVal bFatture As Boolean, _
                                            ByVal strQuery As String, ByRef dsGri As DataSet) As Boolean
    '----------------------------
    'compilo la griglia delle offerte
    Dim strSQL As String = ""
    Dim strSQLOK As String = ""
    Dim strWhere As String = ""
    Dim strFiltriTestoff As String = ""
    Dim strFiltriMovoff As String = ""
    Dim strFiltriTestord As String = ""
    Dim strFiltriMovord As String = ""
    Dim strFiltriTestprb As String = ""
    Dim strFiltriMovprb As String = ""
    Dim strFiltriTestmag As String = ""
    Dim strFiltriMovmag As String = ""
    Dim strFiltriTestmagf As String = ""
    Dim strFiltriprinot As String = ""
    Dim strFiltriScaden As String = ""
    Dim strFiltriAnagra As String = ""
    Dim strFiltriLeads As String = ""
    Dim strFiltriArtico As String = ""

    Const MAXCHAR As String = "255"
    Try
      '-------------------
      If Not SpezzaFiltri(strQuery, strFiltriTestoff, strFiltriMovoff, strFiltriTestord, strFiltriMovord, _
                          strFiltriTestprb, strFiltriMovprb, strFiltriTestmag, strFiltriMovmag, strFiltriTestmagf, _
                          strFiltriprinot, strFiltriScaden, strFiltriAnagra, strFiltriLeads, strFiltriArtico) Then Return False

      '-------------------
      If bOfferte Then
        strSQL = "SELECT distinct td_tipork as et_tipork, td_anno as et_anno, td_serie as et_serie, td_numord as et_numdoc, " & _
                 "td_vers as et_vers, td_codlead as et_codlead, td_datord as et_datdoc, td_riferim as et_riferim, " & _
                 "td_opinc as et_opinc, cast(td_oggetto as varchar(" & MAXCHAR & ")) as et_oggetto, td_tipobf as et_tipobf, td_datcons as et_datcons, " & _
                 "td_codpaga as et_codpaga, td_codagen as et_codagen, td_listino as et_listino, " & _
                 "td_scont1 as et_scont1, td_scont2 as et_scont2, td_scopag as et_scopag, td_valuta as et_valuta, td_cambio as et_cambio, " & _
                 "cast(td_note as varchar(" & MAXCHAR & ")) as et_note, td_flevas as et_flevas, td_confermato as et_confermato, td_rilasciato as et_rilasciato, " & _
                 "td_conto as et_conto, td_coddest as et_coddest, td_coddest2 as et_coddest2, td_totdoc as et_totdoc, " & _
                 "td_totdocv as et_totdocv, td_abi as et_abi, td_cab as et_cab, td_banc1 as et_banc1, td_banc2 as et_banc2, " & _
                 "td_opnome as et_opnome, td_contfatt as et_contfatt, 0 as et_codese, 0 as et_controp, " & _
                 "0 as et_magaz, 0 as et_magaz2, 0 as et_magimp, 0 as et_causale, 0 as et_numpar, null as et_datpar, " & _
                 "le_descr1 as xx_codlead, tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga, tb_descage as xx_codagen, tb_desvalu as xx_valuta, " & _
                 "an_descr1 as xx_conto, dd_nomdest as xx_coddest, '' as xx_codese, '' as xx_controp, " & _
                 "'' as xx_magaz, '' as xx_magaz2, '' as xx_causale " & _
                 " FROM (((((((((testoff INNER JOIN leads ON testoff.codditt = leads.codditt AND testoff.td_codlead = leads.le_codlead) " & _
                 " INNER JOIN movoff ON testoff.codditt = movoff.codditt AND testoff.td_tipork = movoff.mo_tipork " & _
                 " AND testoff.td_anno = movoff.mo_anno AND testoff.td_serie = movoff.mo_serie " & _
                 " AND testoff.td_numord = movoff.mo_numord AND testoff.td_vers = movoff.mo_vers) " & _
                 " INNER JOIN artico ON movoff.codditt = artico.codditt  AND movoff.mo_codart = artico.ar_codart) " & _
                 " LEFT JOIN anagra ON testoff.codditt = anagra.codditt AND testoff.td_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabtpbf ON testoff.codditt = tabtpbf.codditt AND testoff.td_tipobf = tabtpbf.tb_codtpbf) " & _
                 " LEFT JOIN tabpaga ON testoff.td_codpaga = tabpaga.tb_codpaga) " & _
                 " LEFT JOIN tabcage ON testoff.codditt = tabcage.codditt AND testoff.td_codagen = tabcage.tb_codcage) " & _
                 " LEFT JOIN tabvalu ON testoff.td_valuta = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN destdiv ON testoff.codditt = destdiv.codditt AND testoff.td_conto = destdiv.dd_conto AND testoff.td_coddest = destdiv.dd_coddest) " & _
                 " WHERE testoff.codditt = " & CStrSQL(strDitta)

        If strFiltriLeads <> "" Then TraduciWhere(strFiltriLeads, strWhere)
        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
        If strFiltriTestoff <> "" Then TraduciWhere(strFiltriTestoff, strWhere)
        If strFiltriMovoff <> "" Then TraduciWhere(strFiltriMovoff, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        strSQLOK = strSQL
      End If

      '-------------------
      If bOrdini Then
        strWhere = ""
        strSQL = ""
        strSQL = "SELECT distinct td_tipork as et_tipork, td_anno as et_anno, td_serie as et_serie, td_numord as et_numdoc, " & _
                 "0 as et_vers, 0 as et_codlead, td_datord as et_datdoc, td_riferim as et_riferim, " & _
                 "'' as et_opinc, '' as et_oggetto, td_tipobf as et_tipobf, td_datcons as et_datcons, " & _
                 "td_codpaga as et_codpaga, td_codagen as et_codagen, td_listino as et_listino, " & _
                 "td_scont1 as et_scont1, td_scont2 as et_scont2, td_scopag as et_scopag, td_valuta as et_valuta, td_cambio as et_cambio, " & _
                 "cast(td_note as varchar(" & MAXCHAR & ")) as et_note, td_flevas as et_flevas, td_confermato as et_confermato, td_rilasciato as et_rilasciato, " & _
                 "td_conto as et_conto, td_coddest as et_coddest, td_coddest2 as et_coddest2, td_totdoc as et_totdoc, " & _
                 "td_totdocv as et_totdocv, td_abi as et_abi, td_cab as et_cab, td_banc1 as et_banc1, td_banc2 as et_banc2, " & _
                 "td_opnome as et_opnome, td_contfatt as et_contfatt, td_codese as et_codese, td_controp as et_controp, " & _
                 "td_magaz as et_magaz, td_magaz2 as et_magaz2, td_magimp as et_magimp, 0 as et_causale, td_numpar as et_numpar, td_datpar as et_datpar, " & _
                 "'' as xx_codlead, tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga, tb_descage as xx_codagen, tb_desvalu as xx_valuta, " & _
                 "an_descr1 as xx_conto, dd_nomdest as xx_coddest, tb_desciva as xx_codese, tb_descove as xx_controp, " & _
                 "tabmaga.tb_desmaga as xx_magaz, tabmaga_1.tb_desmaga as xx_magaz2, '' as xx_causale " & _
                 " FROM ((((((((((testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork " & _
                 " AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie " & _
                 " AND testord.td_numord = movord.mo_numord) " & _
                 " INNER JOIN artico ON movord.codditt = artico.codditt  AND movord.mo_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabcove ON testord.codditt = tabcove.codditt AND testord.td_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON testord.td_codese = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabtpbf ON testord.codditt = tabtpbf.codditt AND testord.td_tipobf = tabtpbf.tb_codtpbf) " & _
                 " LEFT JOIN tabpaga ON testord.td_codpaga = tabpaga.tb_codpaga) " & _
                 " LEFT JOIN tabcage ON testord.codditt = tabcage.codditt AND testord.td_codagen = tabcage.tb_codcage) " & _
                 " LEFT JOIN tabvalu ON testord.td_valuta = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN destdiv ON testord.codditt = destdiv.codditt AND testord.td_conto = destdiv.dd_conto AND testord.td_coddest = destdiv.dd_coddest) " & _
                 " LEFT JOIN tabmaga ON testord.codditt = tabmaga.codditt AND testord.td_magaz = tabmaga.tb_codmaga" & _
                 " LEFT JOIN tabmaga AS tabmaga_1 ON testord.codditt = tabmaga_1.codditt AND testord.td_magaz2 = tabmaga_1.tb_codmaga" & _
                 " WHERE testord.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
        If strFiltriTestord <> "" Then TraduciWhere(strFiltriTestord, strWhere)
        If strFiltriMovord <> "" Then TraduciWhere(strFiltriMovord, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
      If bNote Then
        strWhere = ""
        strSQL = ""
        strSQL = "SELECT distinct tm_tipork as et_tipork, tm_anno as et_anno, tm_serie as et_serie, tm_numdoc as et_numdoc, " & _
                 "0 as et_vers, 0 as et_codlead, tm_datdoc as et_datdoc, tm_riferim as et_riferim, " & _
                 "'' as et_opinc, '' as et_oggetto, tm_tipobf as et_tipobf, '' as et_datcons, " & _
                 "tm_codpaga as et_codpaga, tm_codagen as et_codagen, tm_listino as et_listino, " & _
                 "tm_scont1 as et_scont1, tm_scont2 as et_scont2, tm_scopag as et_scopag, tm_valuta as et_valuta, tm_cambio as et_cambio, " & _
                 "cast(tm_note as varchar(" & MAXCHAR & ")) as et_note, tm_flevas as et_flevas, 'N' as et_confermato, 'N' as et_rilasciato, " & _
                 "tm_conto as et_conto, tm_coddest as et_coddest, tm_coddest2 as et_coddest2, tm_totdoc as et_totdoc, " & _
                 "tm_totdocv as et_totdocv, tm_abi as et_abi, tm_cab as et_cab, tm_banc1 as et_banc1, tm_banc2 as et_banc2, " & _
                 "tm_opnome as et_opnome, tm_contfatt as et_contfatt, tm_codese as et_codese, tm_controp as et_controp, " & _
                 "tm_magaz as et_magaz, tm_magaz2 as et_magaz2, tm_magimp as et_magimp, tm_causale as et_causale, " & _
                 "tm_numpar as et_numpar, tm_datpar as et_datpar, " & _
                 "'' as xx_codlead, tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga, tb_descage as xx_codagen, tb_desvalu as xx_valuta, " & _
                 "an_descr1 as xx_conto, dd_nomdest as xx_coddest, tb_desciva as xx_codese, tb_descove as xx_controp, " & _
                 "tabmaga.tb_desmaga as xx_magaz, tabmaga2.tb_desmaga as xx_magaz2, tb_descaum as xx_causale " & _
                 " FROM (((((((((((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork " & _
                 " AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_serie = movprb.mm_serie " & _
                 " AND testprb.tm_numdoc = movprb.mm_numdoc) " & _
                 " INNER JOIN artico ON movprb.codditt = artico.codditt  AND movprb.mm_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabmaga ON testprb.codditt = tabmaga.codditt AND testprb.tm_magaz = tabmaga.tb_codmaga) " & _
                 " LEFT JOIN tabmaga as tabmaga2 ON testprb.codditt = tabmaga2.codditt AND testprb.tm_magaz2 = tabmaga2.tb_codmaga) " & _
                 " LEFT JOIN tabcove ON testprb.codditt = tabcove.codditt AND testprb.tm_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON testprb.tm_codese = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabcaum ON testprb.tm_causale = tabcaum.tb_codcaum) " & _
                 " LEFT JOIN tabtpbf ON testprb.codditt = tabtpbf.codditt AND testprb.tm_tipobf = tabtpbf.tb_codtpbf) " & _
                 " LEFT JOIN tabpaga ON testprb.tm_codpaga = tabpaga.tb_codpaga) " & _
                 " LEFT JOIN tabcage ON testprb.codditt = tabcage.codditt AND testprb.tm_codagen = tabcage.tb_codcage) " & _
                 " LEFT JOIN tabvalu ON testprb.tm_valuta = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN destdiv ON testprb.codditt = destdiv.codditt AND testprb.tm_conto = destdiv.dd_conto AND testprb.tm_coddest = destdiv.dd_coddest) " & _
                 " WHERE testprb.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
        If strFiltriTestprb <> "" Then TraduciWhere(strFiltriTestprb, strWhere)
        If strFiltriMovprb <> "" Then TraduciWhere(strFiltriMovprb, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
      If bMagaz Then
        strWhere = ""
        strSQL = ""
        strSQL = "SELECT distinct tm_tipork as et_tipork, tm_anno as et_anno, tm_serie as et_serie, tm_numdoc as et_numdoc, " & _
                 "0 as et_vers, 0 as et_codlead, tm_datdoc as et_datdoc, tm_riferim as et_riferim, " & _
                 "'' as et_opinc, '' as et_oggetto, tm_tipobf as et_tipobf, '' as et_datcons, " & _
                 "tm_codpaga as et_codpaga, tm_codagen as et_codagen, tm_listino as et_listino, " & _
                 "tm_scont1 as et_scont1, tm_scont2 as et_scont2, tm_scopag as et_scopag, tm_valuta as et_valuta, tm_cambio as et_cambio, " & _
                 "cast(tm_note as varchar(" & MAXCHAR & ")) as et_note, tm_flevas as et_flevas, 'N' as et_confermato, 'N' as et_rilasciato, " & _
                 "tm_conto as et_conto, tm_coddest as et_coddest, tm_coddest2 as et_coddest2, tm_totdoc as et_totdoc, " & _
                 "tm_totdocv as et_totdocv, tm_abi as et_abi, tm_cab as et_cab, tm_banc1 as et_banc1, tm_banc2 as et_banc2, " & _
                 "tm_opnome as et_opnome, tm_contfatt as et_contfatt, tm_codese as et_codese, tm_controp as et_controp, " & _
                 "tm_magaz as et_magaz, tm_magaz2 as et_magaz2, tm_magimp as et_magimp, tm_causale as et_causale, " & _
                 "tm_numpar as et_numpar, tm_datpar as et_datpar, " & _
                 "'' as xx_codlead, tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga, tb_descage as xx_codagen, tb_desvalu as xx_valuta, " & _
                 "an_descr1 as xx_conto, dd_nomdest as xx_coddest, tb_desciva as xx_codese, tb_descove as xx_controp, " & _
                 "tabmaga.tb_desmaga as xx_magaz, tabmaga2.tb_desmaga as xx_magaz2, tb_descaum as xx_causale " & _
                 " FROM (((((((((((((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork " & _
                 " AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie " & _
                 " AND testmag.tm_numdoc = movmag.mm_numdoc) " & _
                 " INNER JOIN artico ON movmag.codditt = artico.codditt  AND movmag.mm_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabmaga ON testmag.codditt = tabmaga.codditt AND testmag.tm_magaz = tabmaga.tb_codmaga) " & _
                 " LEFT JOIN tabmaga as tabmaga2 ON testmag.codditt = tabmaga2.codditt AND testmag.tm_magaz2 = tabmaga2.tb_codmaga) " & _
                 " LEFT JOIN tabcove ON testmag.codditt = tabcove.codditt AND testmag.tm_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON testmag.tm_codese = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabcaum ON testmag.tm_causale = tabcaum.tb_codcaum) " & _
                 " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf) " & _
                 " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
                 " LEFT JOIN tabcage ON testmag.codditt = tabcage.codditt AND testmag.tm_codagen = tabcage.tb_codcage) " & _
                 " LEFT JOIN tabvalu ON testmag.tm_valuta = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN destdiv ON testmag.codditt = destdiv.codditt AND testmag.tm_conto = destdiv.dd_conto AND testmag.tm_coddest = destdiv.dd_coddest) " & _
                 " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND (testmag.tm_tipork not IN ('D', 'P', 'K', '£', '('))"

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
        If strFiltriTestmag <> "" Then TraduciWhere(strFiltriTestmag, strWhere)
        If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
      If bFatture Then
        strWhere = ""
        strSQL = ""
        strSQL = "SELECT distinct testmag.tm_tipork as et_tipork, testmag.tm_anno as et_anno," & _
                 " testmag.tm_serie as et_serie, testmag.tm_numdoc as et_numdoc, 0 as et_vers, 0 as et_codlead," & _
                 " testmag.tm_datdoc as et_datdoc, testmag.tm_riferim as et_riferim, '' as et_opinc," & _
                 " '' as et_oggetto, testmag.tm_tipobf as et_tipobf, '' as et_datcons," & _
                 " testmag.tm_codpaga as et_codpaga, testmag.tm_codagen as et_codagen," & _
                 " testmag.tm_listino as et_listino, testmag.tm_scont1 as et_scont1, testmag.tm_scont2 as et_scont2," & _
                 " testmag.tm_scopag as et_scopag, testmag.tm_valuta as et_valuta, testmag.tm_cambio as et_cambio," & _
                 " cast(testmag.tm_note as varchar(" & MAXCHAR & ")) as et_note, testmag.tm_flevas as et_flevas," & _
                 " 'N' as et_confermato, 'N' as et_rilasciato, testmag.tm_conto as et_conto," & _
                 " testmag.tm_coddest as et_coddest, testmag.tm_coddest2 as et_coddest2," & _
                 " testmag.tm_totdoc as et_totdoc, testmag.tm_totdocv as et_totdocv, testmag.tm_abi as et_abi," & _
                 " testmag.tm_cab as et_cab, testmag.tm_banc1 as et_banc1, testmag.tm_banc2 as et_banc2," & _
                 " testmag.tm_opnome as et_opnome, testmag.tm_contfatt as et_contfatt, testmag.tm_codese as et_codese," & _
                 " testmag.tm_controp as et_controp, testmag.tm_magaz as et_magaz, testmag.tm_magaz2 as et_magaz2," & _
                 " testmag.tm_magimp as et_magimp, testmag.tm_causale as et_causale, testmag.tm_numpar as et_numpar," & _
                 " testmag.tm_datpar as et_datpar, '' as xx_codlead, tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga," & _
                 " tb_descage as xx_codagen, tb_desvalu as xx_valuta, an_descr1 as xx_conto, dd_nomdest as xx_coddest," & _
                 " tb_desciva as xx_codese, tb_descove as xx_controp, '' as xx_magaz, '' as xx_magaz2," & _
                 " tb_descaum as xx_causale" & _
                 " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto" & _
                 " INNER JOIN testmag AS testmagf ON testmag.codditt = testmagf.codditt AND testmag.tm_tipork = testmagf.tm_tiporkfat AND testmag.tm_anno = testmagf.tm_annfat AND testmag.tm_serie = testmagf.tm_alffat AND testmag.tm_numdoc = testmagf.tm_numfat" & _
                 " INNER JOIN movmag ON testmagf.codditt = movmag.codditt AND testmagf.tm_tipork = movmag.mm_tipork AND testmagf.tm_anno = movmag.mm_anno AND testmagf.tm_serie = movmag.mm_serie AND testmagf.tm_numdoc = movmag.mm_numdoc" & _
                 " LEFT JOIN tabcove ON testmag.codditt = tabcove.codditt AND testmag.tm_controp = tabcove.tb_codcove" & _
                 " LEFT JOIN tabciva ON testmag.tm_codese = tabciva.tb_codciva" & _
                 " LEFT JOIN tabcaum ON testmag.tm_causale = tabcaum.tb_codcaum" & _
                 " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf" & _
                 " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga" & _
                 " LEFT JOIN tabcage ON testmag.codditt = tabcage.codditt AND testmag.tm_codagen = tabcage.tb_codcage" & _
                 " LEFT JOIN tabvalu ON testmag.tm_valuta = tabvalu.tb_codvalu" & _
                 " LEFT JOIN destdiv ON testmag.codditt = destdiv.codditt AND testmag.tm_conto = destdiv.dd_conto AND testmag.tm_coddest = destdiv.dd_coddest" & _
                 " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND (testmag.tm_tipork IN ('D', 'P', 'K', '£', '('))"

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriTestmag <> "" Then TraduciWhere(strFiltriTestmag, strWhere)
        If strFiltriTestmagf <> "" Then TraduciWhere(strFiltriTestmagf, strWhere)
        If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
      If strSQLOK.Trim <> "" Then
        strSQL = strSQLOK
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTA", dsGri)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataCorpo(ByVal strDitta As String, ByVal bOfferte As Boolean, _
    ByVal bOrdini As Boolean, ByVal bNote As Boolean, ByVal bMagaz As Boolean, _
    ByVal strQuery As String, ByVal bGriSoloUnDoc As Boolean, ByVal bGriSoloArtFiltri As Boolean, _
    ByRef dtrT As DataRow, ByRef dsGri As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetDataCorpo(strDitta, bOfferte, bOrdini, bNote, bMagaz, False, strQuery, bGriSoloUnDoc, _
        bGriSoloArtFiltri, dtrT, dsGri)
      '--------------------------------------------------------------------------------------------------------------      
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetDataCorpo(ByVal strDitta As String, ByVal bOfferte As Boolean, _
                                           ByVal bOrdini As Boolean, ByVal bNote As Boolean, _
                                           ByVal bMagaz As Boolean, ByVal bFatture As Boolean, _
                                           ByVal strQuery As String, ByVal bGriSoloUnDoc As Boolean, _
                                           ByVal bGriSoloArtFiltri As Boolean, ByRef dtrT As DataRow, _
                                           ByRef dsGri As DataSet) As Boolean
    '----------------------------
    'compilo la griglia delle offerte
    Dim strSQL As String = ""
    Dim strSQLOK As String = ""
    Dim strWhere As String = ""
    Dim strFiltriTestoff As String = ""
    Dim strFiltriMovoff As String = ""
    Dim strFiltriTestord As String = ""
    Dim strFiltriMovord As String = ""
    Dim strFiltriTestprb As String = ""
    Dim strFiltriMovprb As String = ""
    Dim strFiltriTestmag As String = ""
    Dim strFiltriMovmag As String = ""
    Dim strFiltriTestmagf As String = ""
    Dim strFiltriprinot As String = ""
    Dim strFiltriScaden As String = ""
    Dim strFiltriAnagra As String = ""
    Dim strFiltriLeads As String = ""
    Dim strFiltriArtico As String = ""
    Dim nTipoQuery As Integer = 0

    Const MAXCHAR As String = "255"
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bOfferte, bOrdini, bNote, bMagaz, bFatture, strQuery, _
        bGriSoloUnDoc, bGriSoloArtFiltri, dtrT, dsGri})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrT = CType(oIn(9), DataRow)
        dsGri = CType(oIn(10), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      'cosa devo mettere nella griglia (oltre alla bistecca ...)
      If bGriSoloUnDoc = True And bGriSoloArtFiltri = True Then
        nTipoQuery = 0 'solo un documento e solo un subset delle righe dell'interno documento (in base ai filtri su movxxx)
      End If
      If bGriSoloUnDoc = True And bGriSoloArtFiltri = False Then
        nTipoQuery = 1 'solo un documento, ma tutte le righe del corpo
      End If
      If bGriSoloUnDoc = False And bGriSoloArtFiltri = True Then
        nTipoQuery = 2 'tutti i documenti insieme ma solo un subset delle righe dell'interno documento (in base ai filtri su movxxx)
      End If
      If bGriSoloUnDoc = False And bGriSoloArtFiltri = False Then
        nTipoQuery = 3 'tutti i documenti e tutte le righe di tutti i documenti
      End If

      '-------------------
      'se non è stata passata la riga di griglia e devo visual solo le righe di un doc esco
      If dtrT Is Nothing And nTipoQuery < 2 Then Return True

      '-------------------
      If Not SpezzaFiltri(strQuery, strFiltriTestoff, strFiltriMovoff, strFiltriTestord, strFiltriMovord, _
                          strFiltriTestprb, strFiltriMovprb, strFiltriTestmag, strFiltriMovmag, strFiltriTestmagf, _
                          strFiltriprinot, strFiltriScaden, strFiltriAnagra, strFiltriLeads, strFiltriArtico) Then Return False

      '-------------------
      If bOfferte Then
        If nTipoQuery < 2 And GetDataCorpo_GetTipoGriglia(dtrT) <> "TESTOFF" Then GoTo Ordini

        strSQL = "SELECT td_conto as xxo_conto, td_codlead as xxo_codlead, mo_tipork as ec_tipork, mo_anno as ec_anno, mo_serie as ec_serie, mo_numord as ec_numdoc, " & _
                 " mo_vers as ec_vers, mo_codart as ec_codart, mo_descr as ec_descr, mo_desint as ec_desint, " & _
                 " mo_unmis as ec_unmis, mo_colli as ec_colli, mo_ump as ec_ump, mo_quant as ec_quant, " & _
                 " mo_prezzo as ec_prezzo, mo_prezvalc as ec_prezvalc, mo_preziva as ec_preziva, " & _
                 " mo_scont1 as ec_scont1, mo_scont2 as ec_scont2, mo_scont3 as ec_scont3, mo_scont4 as ec_scont4, " & _
                 " mo_scont5 as ec_scont5, mo_scont6 as ec_scont6, cast(mo_note as varchar(" & MAXCHAR & ")) as ec_note, " & _
                 " mo_datcons as ec_datcons, 0 as ec_magaz, 0 as ec_magaz2, 0 as ec_quaeva, " & _
                 " 0 as ec_quapre, mo_flevas as ec_flevas, 'C' as ec_flevapre, 0 as ec_provv, " & _
                 " 0 as ec_controp, mo_codiva as ec_codiva, mo_stasino as ec_stasino, mo_prelist as ec_prelist, " & _
                 " mo_codcfam as ec_codcfam, 0 as ec_commeca, '' as ec_subcommeca, 0 as ec_codcena, " & _
                 " 0 as ec_vprovv, mo_confermato as ec_confermato, mo_rilasciato as ec_rilasciato, 'N' as ec_aperto, " & _
                 " 0 as xx_lottox, mo_fase as ec_fase, mo_misura1 as ec_misura1, mo_misura2 as ec_misura2, mo_misura3 as ec_misura3, " & _
                 " null as ec_datini, null as ec_datfin, '' as ec_ubicaz, 0 as ec_causale, 0 as ec_causale2, mo_valore as ec_valore, " & _
                 " '' as ec_ortipo, 0 as ec_oranno, '' as ec_orserie, 0 as ec_ornum, 0 as ec_orriga, 'C' as ec_salcon, " & _
                 " '' as ec_nptipo, 0 as ec_npanno, '' as ec_npserie, 0 as ec_npnum, 0 as ec_npriga, 0 as ec_npvers, 'C' as ec_pnsalcon, " & _
                 " '' as xxo_magaz, '' as xxo_magaz2, '' as xxo_controp, " & _
                 " tb_desciva as xxo_codiva, tb_descfam as xxo_codcfam, '' as xxo_commeca, '' as xxo_codcena, " & _
                 " af_descr as xxo_fase, '' as xxo_causale " & _
                 " FROM ((((((testoff INNER JOIN movoff ON testoff.codditt = movoff.codditt AND testoff.td_tipork = movoff.mo_tipork " & _
                 " AND testoff.td_anno = movoff.mo_anno AND testoff.td_serie = movoff.mo_serie " & _
                 " AND testoff.td_numord = movoff.mo_numord AND testoff.td_vers = movoff.mo_vers) " & _
                 " INNER JOIN artico ON movoff.codditt = artico.codditt  AND movoff.mo_codart = artico.ar_codart) " & _
                 " LEFT JOIN anagra ON testoff.codditt = anagra.codditt AND testoff.td_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabciva ON movoff.mo_codiva = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabcfam ON movoff.codditt = tabcfam.codditt AND movoff.mo_codcfam = tabcfam.tb_codcfam) " & _
                 " LEFT JOIN artfasi ON movoff.codditt = artfasi.codditt AND movoff.mo_codart = artfasi.af_codart  AND movoff.mo_fase = artfasi.af_fase) " & _
                 " WHERE movoff.codditt = " & CStrSQL(strDitta)

        If strFiltriLeads <> "" Then TraduciWhere(strFiltriLeads, strWhere)
        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        Select Case nTipoQuery
          Case 0
            strWhere = " AND (movoff.mo_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movoff.mo_anno = " & dtrT!et_anno.ToString & _
                       " AND movoff.mo_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movoff.mo_numord = " & dtrT!et_numdoc.ToString & _
                       " AND movoff.mo_vers = " & dtrT!et_vers.ToString & ")"
            If strFiltriMovoff <> "" Then TraduciWhere(strFiltriMovoff, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 1
            strWhere = " AND (movoff.mo_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movoff.mo_anno = " & dtrT!et_anno.ToString & _
                       " AND movoff.mo_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movoff.mo_numord = " & dtrT!et_numdoc.ToString & _
                       " AND movoff.mo_vers = " & dtrT!et_vers.ToString & ")"
          Case 2
            If strFiltriTestoff <> "" Then TraduciWhere(strFiltriTestoff, strWhere)
            If strFiltriMovoff <> "" Then TraduciWhere(strFiltriMovoff, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 3
            If strFiltriTestoff <> "" Then TraduciWhere(strFiltriTestoff, strWhere)
        End Select

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        strSQLOK = strSQL
      End If

      '-------------------
Ordini:
      If bOrdini Then
        If nTipoQuery < 2 And GetDataCorpo_GetTipoGriglia(dtrT) <> "TESTORD" Then GoTo Note
        strWhere = ""
        strSQL = ""

        strSQL = "SELECT td_conto as xxo_conto, mo_tipork as ec_tipork, mo_anno as ec_anno, mo_serie as ec_serie, mo_numord as ec_numdoc, " & _
                 " 0 as ec_vers, mo_codart as ec_codart, mo_descr as ec_descr, mo_desint as ec_desint, " & _
                 " mo_unmis as ec_unmis, mo_colli as ec_colli, mo_ump as ec_ump, mo_quant as ec_quant, " & _
                 " mo_prezzo as ec_prezzo, mo_prezvalc as ec_prezvalc, mo_preziva as ec_preziva, " & _
                 " mo_scont1 as ec_scont1, mo_scont2 as ec_scont2, mo_scont3 as ec_scont3, mo_scont4 as ec_scont4, " & _
                 " mo_scont5 as ec_scont5, mo_scont6 as ec_scont6, cast(mo_note as varchar(" & MAXCHAR & ")) as ec_note, " & _
                 " mo_datcons as ec_datcons, mo_magaz as ec_magaz, mo_magaz2 as ec_magaz2, mo_quaeva as ec_quaeva, " & _
                 " mo_quapre as ec_quapre, mo_flevas as ec_flevas, mo_flevapre as ec_flevapre, mo_provv as ec_provv, " & _
                 " mo_controp as ec_controp, mo_codiva as ec_codiva, mo_stasino as ec_stasino, mo_prelist as ec_prelist, " & _
                 " mo_codcfam as ec_codcfam, mo_commeca as ec_commeca, mo_subcommeca as ec_subcommeca, mo_codcena as ec_codcena, " & _
                 " mo_vprovv as ec_vprovv, mo_confermato as ec_confermato, mo_rilasciato as ec_rilasciato, mo_aperto as ec_aperto, " & _
                 " alo_lottox as xx_lottox, mo_fase as ec_fase, mo_misura1 as ec_misura1, mo_misura2 as ec_misura2, mo_misura3 as ec_misura3, " & _
                 " mo_datini as ec_datini, mo_datfin as ec_datfin, mo_ubicaz as ec_ubicaz, 0 as ec_causale, 0 as ec_causale2, mo_valoremm as ec_valore, " & _
                 " mo_oatipo as ec_ortipo, mo_oaanno as ec_oranno, mo_oaserie as ec_orserie, mo_oanum as ec_ornum, mo_oariga as ec_orriga, mo_oasalcon as ec_salcon, " & _
                 " mo_oqtipo as ec_nptipo, mo_oqanno as ec_npanno, mo_oqserie as ec_npserie, mo_oqnum as ec_npnum, mo_oqriga as ec_npriga, mo_oqvers as ec_npvers, mo_oqsalcon as ec_pnsalcon, " & _
                 " tabmaga.tb_desmaga as xxo_magaz, tabmaga2.tb_desmaga as xxo_magaz2, tb_descove as xxo_controp, " & _
                 " tb_desciva as xxo_codiva, tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, " & _
                 " af_descr as xxo_fase, '' as xxo_causale " & _
                 " FROM ((((((((((((testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork " & _
                 " AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie " & _
                 " AND testord.td_numord = movord.mo_numord) " & _
                 " INNER JOIN artico ON movord.codditt = artico.codditt  AND movord.mo_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabcove ON movord.codditt = tabcove.codditt AND movord.mo_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON movord.mo_codiva = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga) " & _
                 " LEFT JOIN tabmaga as tabmaga2 ON movord.codditt = tabmaga2.codditt AND movord.mo_magaz2 = tabmaga2.tb_codmaga) " & _
                 " LEFT JOIN tabcfam ON movord.codditt = tabcfam.codditt AND movord.mo_codcfam = tabcfam.tb_codcfam) " & _
                 " LEFT JOIN commess ON movord.codditt = commess.codditt AND movord.mo_commeca = commess.co_comme) " & _
                 " LEFT JOIN tabcena ON movord.codditt = tabcena.codditt AND movord.mo_codcena = tabcena.tb_codcena) " & _
                 " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_codart = artfasi.af_codart AND movord.mo_fase = artfasi.af_fase) " & _
                 " LEFT JOIN analotti ON movord.codditt = analotti.codditt AND movord.mo_codart = analotti.alo_codart AND movord.mo_lotto = analotti.alo_lotto) " & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        Select Case nTipoQuery
          Case 0
            strWhere = " AND (movord.mo_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movord.mo_anno = " & dtrT!et_anno.ToString & _
                       " AND movord.mo_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movord.mo_numord = " & dtrT!et_numdoc.ToString & ")"
            If strFiltriMovord <> "" Then TraduciWhere(strFiltriMovord, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 1
            strWhere = " AND (movord.mo_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movord.mo_anno = " & dtrT!et_anno.ToString & _
                       " AND movord.mo_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movord.mo_numord = " & dtrT!et_numdoc.ToString & ")"
          Case 2
            If strFiltriTestord <> "" Then TraduciWhere(strFiltriTestord, strWhere)
            If strFiltriMovord <> "" Then TraduciWhere(strFiltriMovord, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 3
            If strFiltriTestord <> "" Then TraduciWhere(strFiltriTestord, strWhere)
        End Select

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
Note:
      If bNote Then
        If nTipoQuery < 2 And GetDataCorpo_GetTipoGriglia(dtrT) <> "TESTPRB" Then GoTo Magaz
        strWhere = ""
        strSQL = ""

        strSQL = "SELECT tm_conto as xxo_conto, mm_tipork as ec_tipork, mm_anno as ec_anno, mm_serie as ec_serie, mm_numdoc as ec_numdoc, " & _
                 " 0 as ec_vers, mm_codart as ec_codart, mm_descr as ec_descr, mm_desint as ec_desint, " & _
                 " mm_unmis as ec_unmis, mm_colli as ec_colli, mm_ump as ec_ump, mm_quant as ec_quant, " & _
                 " mm_prezzo as ec_prezzo, mm_prezvalc as ec_prezvalc, mm_preziva as ec_preziva, " & _
                 " mm_scont1 as ec_scont1, mm_scont2 as ec_scont2, mm_scont3 as ec_scont3, mm_scont4 as ec_scont4, " & _
                 " mm_scont5 as ec_scont5, mm_scont6 as ec_scont6, cast(mm_note as varchar(" & MAXCHAR & ")) as ec_note, " & _
                 " null as ec_datcons, mm_magaz as ec_magaz, mm_magaz2 as ec_magaz2, mm_nprquaeva as ec_quaeva, " & _
                 " 0 as ec_quapre, mm_nprflevas as ec_flevas, 'C' as ec_flevapre, mm_provv as ec_provv, " & _
                 " mm_controp as ec_controp, mm_codiva as ec_codiva, mm_stasino as ec_stasino, mm_prelist as ec_prelist, " & _
                 " mm_codcfam as ec_codcfam, mm_commeca as ec_commeca, mm_subcommeca as ec_subcommeca, mm_codcena as ec_codcena, " & _
                 " mm_vprovv as ec_vprovv, 'N' as ec_confermato, 'N' as ec_rilasciato, 'N' as ec_aperto, " & _
                 " alo_lottox as xx_lottox, mm_fase as ec_fase, mm_misura1 as ec_misura1, mm_misura2 as ec_misura2, mm_misura3 as ec_misura3, " & _
                 " mm_datini as ec_datini, mm_datfin as ec_datfin, mm_ubicaz as ec_ubicaz, mm_causale as ec_causale, mm_causale2 as ec_causale2, mm_valore as ec_valore, " & _
                 " mm_ortipo as ec_ortipo, mm_oranno as ec_oranno, mm_orserie as ec_orserie, mm_ornum as ec_ornum, mm_orriga as ec_orriga, mm_salcon as ec_salcon, " & _
                 " mm_nptipo as ec_nptipo, mm_npanno as ec_npanno, mm_npserie as ec_npserie, mm_npnum as ec_npnum, mm_npriga as ec_npriga, 0 as ec_npvers, mm_npsalcon as ec_pnsalcon, " & _
                 " tabmaga.tb_desmaga as xxo_magaz, tabmaga2.tb_desmaga as xxo_magaz2, tb_descove as xxo_controp, " & _
                 " tb_desciva as xxo_codiva, tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, " & _
                 " af_descr as xxo_fase, tb_descaum as xxo_causale " & _
                 " FROM (((((((((((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork " & _
                 " AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_serie = movprb.mm_serie " & _
                 " AND testprb.tm_numdoc = movprb.mm_numdoc) " & _
                 " INNER JOIN artico ON movprb.codditt = artico.codditt  AND movprb.mm_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabcove ON movprb.codditt = tabcove.codditt AND movprb.mm_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON movprb.mm_codiva = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabmaga ON movprb.codditt = tabmaga.codditt AND movprb.mm_magaz = tabmaga.tb_codmaga) " & _
                 " LEFT JOIN tabmaga as tabmaga2 ON movprb.codditt = tabmaga2.codditt AND movprb.mm_magaz2 = tabmaga2.tb_codmaga) " & _
                 " LEFT JOIN tabcfam ON movprb.codditt = tabcfam.codditt AND movprb.mm_codcfam = tabcfam.tb_codcfam) " & _
                 " LEFT JOIN commess ON movprb.codditt = commess.codditt AND movprb.mm_commeca = commess.co_comme) " & _
                 " LEFT JOIN tabcena ON movprb.codditt = tabcena.codditt AND movprb.mm_codcena = tabcena.tb_codcena) " & _
                 " LEFT JOIN artfasi ON movprb.codditt = artfasi.codditt AND movprb.mm_codart = artfasi.af_codart AND movprb.mm_fase = artfasi.af_fase) " & _
                 " LEFT JOIN tabcaum ON movprb.mm_causale = tabcaum.tb_codcaum) " & _
                 " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto) " & _
                 " WHERE movprb.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        Select Case nTipoQuery
          Case 0
            strWhere = " AND (movprb.mm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movprb.mm_anno = " & dtrT!et_anno.ToString & _
                       " AND movprb.mm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movprb.mm_numdoc = " & dtrT!et_numdoc.ToString & ")"
            If strFiltriMovprb <> "" Then TraduciWhere(strFiltriMovprb, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 1
            strWhere = " AND (movprb.mm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movprb.mm_anno = " & dtrT!et_anno.ToString & _
                       " AND movprb.mm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movprb.mm_numdoc = " & dtrT!et_numdoc.ToString & ")"
          Case 2
            If strFiltriTestprb <> "" Then TraduciWhere(strFiltriTestprb, strWhere)
            If strFiltriMovprb <> "" Then TraduciWhere(strFiltriMovprb, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 3
            If strFiltriTestprb <> "" Then TraduciWhere(strFiltriTestprb, strWhere)
        End Select

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '-------------------
Magaz:
      If bMagaz Then
        If nTipoQuery < 2 And GetDataCorpo_GetTipoGriglia(dtrT) <> "TESTMAG" Then GoTo Fatture
        strWhere = ""
        strSQL = ""
        strSQL = "SELECT tm_conto as xxo_conto, mm_tipork as ec_tipork, mm_anno as ec_anno, mm_serie as ec_serie, mm_numdoc as ec_numdoc, " & _
                 " 0 as ec_vers, mm_codart as ec_codart, mm_descr as ec_descr, mm_desint as ec_desint, " & _
                 " mm_unmis as ec_unmis, mm_colli as ec_colli, mm_ump as ec_ump, mm_quant as ec_quant, " & _
                 " mm_prezzo as ec_prezzo, mm_prezvalc as ec_prezvalc, mm_preziva as ec_preziva, " & _
                 " mm_scont1 as ec_scont1, mm_scont2 as ec_scont2, mm_scont3 as ec_scont3, mm_scont4 as ec_scont4, " & _
                 " mm_scont5 as ec_scont5, mm_scont6 as ec_scont6, cast(mm_note as varchar(" & MAXCHAR & ")) as ec_note, " & _
                 " null as ec_datcons, mm_magaz as ec_magaz, mm_magaz2 as ec_magaz2, mm_nprquaeva as ec_quaeva, " & _
                 " 0 as ec_quapre, mm_nprflevas as ec_flevas, 'C' as ec_flevapre, mm_provv as ec_provv, " & _
                 " mm_controp as ec_controp, mm_codiva as ec_codiva, mm_stasino as ec_stasino, mm_prelist as ec_prelist, " & _
                 " mm_codcfam as ec_codcfam, mm_commeca as ec_commeca, mm_subcommeca as ec_subcommeca, mm_codcena as ec_codcena, " & _
                 " mm_vprovv as ec_vprovv, 'N' as ec_confermato, 'N' as ec_rilasciato, 'N' as ec_aperto, " & _
                 " alo_lottox as xx_lottox, mm_fase as ec_fase, mm_misura1 as ec_misura1, mm_misura2 as ec_misura2, mm_misura3 as ec_misura3, " & _
                 " mm_datini as ec_datini, mm_datfin as ec_datfin, mm_ubicaz as ec_ubicaz, mm_causale as ec_causale, mm_causale2 as ec_causale2, mm_valore as ec_valore, " & _
                 " mm_ortipo as ec_ortipo, mm_oranno as ec_oranno, mm_orserie as ec_orserie, mm_ornum as ec_ornum, mm_orriga as ec_orriga, mm_salcon as ec_salcon, " & _
                 " mm_nptipo as ec_nptipo, mm_npanno as ec_npanno, mm_npserie as ec_npserie, mm_npnum as ec_npnum, mm_npriga as ec_npriga, 0 as ec_npvers, mm_npsalcon as ec_pnsalcon, " & _
                 " tabmaga.tb_desmaga as xxo_magaz, tabmaga2.tb_desmaga as xxo_magaz2, tb_descove as xxo_controp, " & _
                 " tb_desciva as xxo_codiva, tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, " & _
                 " af_descr as xxo_fase, tb_descaum as xxo_causale " & _
                 " FROM (((((((((((((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork " & _
                 " AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie " & _
                 " AND testmag.tm_numdoc = movmag.mm_numdoc) " & _
                 " INNER JOIN artico ON movmag.codditt = artico.codditt  AND movmag.mm_codart = artico.ar_codart) " & _
                 " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto) " & _
                 " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove) " & _
                 " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva) " & _
                 " LEFT JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) " & _
                 " LEFT JOIN tabmaga as tabmaga2 ON movmag.codditt = tabmaga2.codditt AND movmag.mm_magaz2 = tabmaga2.tb_codmaga) " & _
                 " LEFT JOIN tabcfam ON movmag.codditt = tabcfam.codditt AND movmag.mm_codcfam = tabcfam.tb_codcfam) " & _
                 " LEFT JOIN commess ON movmag.codditt = commess.codditt AND movmag.mm_commeca = commess.co_comme) " & _
                 " LEFT JOIN tabcena ON movmag.codditt = tabcena.codditt AND movmag.mm_codcena = tabcena.tb_codcena) " & _
                 " LEFT JOIN artfasi ON movmag.codditt = artfasi.codditt AND movmag.mm_codart = artfasi.af_codart  AND movmag.mm_fase = artfasi.af_fase) " & _
                 " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto) " & _
                 " LEFT JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum) " & _
                 " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND (testmag.tm_tipork not IN ('D', 'P', 'K', '£', '('))"

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        Select Case nTipoQuery
          Case 0
            strWhere = " AND (movmag.mm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movmag.mm_anno = " & dtrT!et_anno.ToString & _
                       " AND movmag.mm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movmag.mm_numdoc = " & dtrT!et_numdoc.ToString & ")"
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 1
            strWhere = " AND (movmag.mm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND movmag.mm_anno = " & dtrT!et_anno.ToString & _
                       " AND movmag.mm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND movmag.mm_numdoc = " & dtrT!et_numdoc.ToString & ")"
          Case 2
            If strFiltriTestmag <> "" Then TraduciWhere(strFiltriTestmag, strWhere)
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 3
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
        End Select

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If

      '--------------------------------------------------------------------------------------------------------------
Fatture:
      If bFatture = True Then
        If nTipoQuery < 2 And GetDataCorpo_GetTipoGriglia(dtrT) <> "TESTMAG" Then GoTo fine
        strWhere = "" : strSQL = ""
        strSQL = "SELECT testmag.tm_conto as xxo_conto, mm_tipork as ec_tipork, mm_anno as ec_anno," & _
          " mm_serie as ec_serie, mm_numdoc as ec_numdoc, 0 as ec_vers, mm_codart as ec_codart," & _
          " mm_descr as ec_descr, mm_desint as ec_desint, mm_unmis as ec_unmis, mm_colli as ec_colli," & _
          " mm_ump as ec_ump, mm_quant as ec_quant, mm_prezzo as ec_prezzo, mm_prezvalc as ec_prezvalc," & _
          " mm_preziva as ec_preziva, mm_scont1 as ec_scont1, mm_scont2 as ec_scont2, mm_scont3 as ec_scont3," & _
          " mm_scont4 as ec_scont4, mm_scont5 as ec_scont5, mm_scont6 as ec_scont6," & _
          " cast(mm_note as varchar(" & MAXCHAR & ")) as ec_note, null as ec_datcons, mm_magaz as ec_magaz," & _
          " mm_magaz2 as ec_magaz2, mm_nprquaeva as ec_quaeva, 0 as ec_quapre, mm_nprflevas as ec_flevas," & _
          " 'C' as ec_flevapre, mm_provv as ec_provv, mm_controp as ec_controp, mm_codiva as ec_codiva," & _
          " mm_stasino as ec_stasino, mm_prelist as ec_prelist, mm_codcfam as ec_codcfam, mm_commeca as ec_commeca," & _
          " mm_subcommeca as ec_subcommeca, mm_codcena as ec_codcena, mm_vprovv as ec_vprovv, 'N' as ec_confermato," & _
          " 'N' as ec_rilasciato, 'N' as ec_aperto, alo_lottox as xx_lottox, mm_fase as ec_fase," & _
          " mm_misura1 as ec_misura1, mm_misura2 as ec_misura2, mm_misura3 as ec_misura3, mm_datini as ec_datini," & _
          " mm_datfin as ec_datfin, mm_ubicaz as ec_ubicaz, mm_causale as ec_causale, mm_causale2 as ec_causale2," & _
          " mm_valore as ec_valore, mm_ortipo as ec_ortipo, mm_oranno as ec_oranno, mm_orserie as ec_orserie," & _
          " mm_ornum as ec_ornum, mm_orriga as ec_orriga, mm_salcon as ec_salcon, mm_nptipo as ec_nptipo," & _
          " mm_npanno as ec_npanno, mm_npserie as ec_npserie, mm_npnum as ec_npnum, mm_npriga as ec_npriga," & _
          " 0 as ec_npvers, mm_npsalcon as ec_pnsalcon, tabmaga.tb_desmaga as xxo_magaz," & _
          " tabmaga2.tb_desmaga as xxo_magaz2, tb_descove as xxo_controp, tb_desciva as xxo_codiva," & _
          " tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, af_descr as xxo_fase," & _
          " tb_descaum as xxo_causale" & _
          " FROM testmag INNER JOIN testmag AS testmagfatt ON testmag.codditt = testmagfatt.codditt AND testmag.tm_tipork = testmagfatt.tm_tiporkfat AND testmag.tm_anno = testmagfatt.tm_annfat AND testmag.tm_serie = testmagfatt.tm_alffat AND testmag.tm_numdoc = testmagfatt.tm_numfat" & _
          " INNER JOIN movmag ON testmagfatt.codditt = movmag.codditt AND testmagfatt.tm_tipork = movmag.mm_tipork AND testmagfatt.tm_anno = movmag.mm_anno AND testmagfatt.tm_serie = movmag.mm_serie AND testmagfatt.tm_numdoc = movmag.mm_numdoc" & _
          " INNER JOIN artico ON movmag.codditt = artico.codditt  AND movmag.mm_codart = artico.ar_codart" & _
          " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto" & _
          " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove" & _
          " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva" & _
          " LEFT JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga" & _
          " LEFT JOIN tabmaga as tabmaga2 ON movmag.codditt = tabmaga2.codditt AND movmag.mm_magaz2 = tabmaga2.tb_codmaga" & _
          " LEFT JOIN tabcfam ON movmag.codditt = tabcfam.codditt AND movmag.mm_codcfam = tabcfam.tb_codcfam" & _
          " LEFT JOIN commess ON movmag.codditt = commess.codditt AND movmag.mm_commeca = commess.co_comme" & _
          " LEFT JOIN tabcena ON movmag.codditt = tabcena.codditt AND movmag.mm_codcena = tabcena.tb_codcena" & _
          " LEFT JOIN artfasi ON movmag.codditt = artfasi.codditt AND movmag.mm_codart = artfasi.af_codart  AND movmag.mm_fase = artfasi.af_fase" & _
          " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto" & _
          " LEFT JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum" & _
          " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
          " AND testmag.tm_tipork IN ('D', 'P', 'K', '£', '(')"
        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        Select Case nTipoQuery
          Case 0
            strWhere = " AND (testmag.tm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND testmag.tm_anno = " & dtrT!et_anno.ToString & _
                       " AND testmag.tm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND testmag.tm_numdoc = " & dtrT!et_numdoc.ToString & ")"
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 1
            strWhere = " AND (testmag.tm_tipork = " & CStrSQL(dtrT!et_tipork.ToString) & _
                       " AND testmag.tm_anno = " & dtrT!et_anno.ToString & _
                       " AND testmag.tm_serie = " & CStrSQL(dtrT!et_serie.ToString) & _
                       " AND testmag.tm_numdoc = " & dtrT!et_numdoc.ToString & ")"
          Case 2
            If strFiltriTestmagf <> "" Then TraduciWhere(strFiltriTestmagf, strWhere)
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
            If strFiltriArtico <> "" Then TraduciWhere(strFiltriArtico, strWhere)
          Case 3
            If strFiltriMovmag <> "" Then TraduciWhere(strFiltriMovmag, strWhere)
        End Select
        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
        If strSQL <> "" Then
          If strSQLOK.Trim <> "" Then strSQLOK += "UNION ALL " & vbCrLf
          strSQLOK += strSQL
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
Fine:
      If strSQLOK.Trim <> "" Then
        strSQL = strSQLOK
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CORPO", dsGri)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDataCorpo_GetTipoGriglia(ByRef dtrT As DataRow) As String
    Try
      If dtrT Is Nothing Then Return ""
      Select Case dtrT!et_tipork.ToString
        Case "!" : Return "TESTOFF"
        Case "R", "O", "H", "X", "Q", "#", "V", "$", "Y" : Return "TESTORD"
        Case "W" : Return "TESTPRB"
        Case Else : Return "TESTMAG"
      End Select

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function GetDataPrinot(ByVal strDitta As String, ByVal bPrinot As Boolean, _
                                                 ByVal strQuery As String, ByRef dsGri As DataSet) As Boolean
    '----------------------------
    'compilo la griglia delle offerte
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strFiltriTestoff As String = ""
    Dim strFiltriMovoff As String = ""
    Dim strFiltriTestord As String = ""
    Dim strFiltriMovord As String = ""
    Dim strFiltriTestprb As String = ""
    Dim strFiltriMovprb As String = ""
    Dim strFiltriTestmag As String = ""
    Dim strFiltriMovmag As String = ""
    Dim strFiltriTestmagf As String = ""
    Dim strFiltriprinot As String = ""
    Dim strFiltriScaden As String = ""
    Dim strFiltriAnagra As String = ""
    Dim strFiltriLeads As String = ""
    Dim strFiltriArtico As String = ""

    Try
      '-------------------
      If Not SpezzaFiltri(strQuery, strFiltriTestoff, strFiltriMovoff, strFiltriTestord, strFiltriMovord, _
                          strFiltriTestprb, strFiltriMovprb, strFiltriTestmag, strFiltriMovmag, strFiltriTestmagf, _
                          strFiltriprinot, strFiltriScaden, strFiltriAnagra, strFiltriLeads, strFiltriArtico) Then Return False

      '-------------------
      If bPrinot Then
        strSQL = "SELECT pn_escomp, pn_datreg, pn_numreg, pn_riga, pn_causale, pn_descauc, pn_conto, pn_descr, pn_darave, " & _
                 " pn_dare, pn_avere, pn_importo, pn_codvalu, pn_cambio, pn_dareval, pn_avereval, pn_impval, " & _
                 " pn_datdoc, pn_alfdoc, pn_numdoc, pn_annpar, pn_alfpar, pn_numpar, pn_controp, pn_fllg, " & _
                 " pn_contocf, pn_tregiva, pn_nregiva, pn_codiva, pn_aliqiva, pn_indetr, pn_imponib, pn_imponibval, " & _
                 " pn_tipacq, pn_alfpro, pn_numpro, pn_integr, pn_dtcomiva, pn_dtvaluta, pn_dtcomplaf, pn_datini, pn_datfin, " & _
                 " pn_ultagg, pn_opnome, " & _
                 " tb_desvalu as xxp_codvalu, anagra.an_descr1 as xxp_conto, anagra2.an_descr1 as xxp_controp, " & _
                 " anagra3.an_descr1 as xxp_contocf, tb_desciva as xxp_codiva " & _
                 " FROM (((((prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_conto = anagra.an_conto) " & _
                 " LEFT JOIN anagra as anagra2 ON prinot.codditt = anagra2.codditt AND prinot.pn_controp = anagra2.an_conto) " & _
                 " LEFT JOIN anagra as anagra3 ON prinot.codditt = anagra3.codditt AND prinot.pn_contocf = anagra3.an_conto) " & _
                 " LEFT JOIN tabvalu ON prinot.pn_codvalu = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN tabciva ON prinot.pn_codiva = tabciva.tb_codciva) " & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriprinot <> "" Then TraduciWhere(strFiltriprinot, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
      End If

      '-------------------
      If strSQL.Trim <> "" Then
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PRIN", dsGri)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataScaden(ByVal strDitta As String, ByVal bScaden As Boolean, _
                                            ByVal strQuery As String, ByRef dsGri As DataSet) As Boolean
    '----------------------------
    'compilo la griglia delle offerte
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strFiltriTestoff As String = ""
    Dim strFiltriMovoff As String = ""
    Dim strFiltriTestord As String = ""
    Dim strFiltriMovord As String = ""
    Dim strFiltriTestprb As String = ""
    Dim strFiltriMovprb As String = ""
    Dim strFiltriTestmag As String = ""
    Dim strFiltriMovmag As String = ""
    Dim strFiltriTestmagf As String = ""
    Dim strFiltriprinot As String = ""
    Dim strFiltriScaden As String = ""
    Dim strFiltriAnagra As String = ""
    Dim strFiltriLeads As String = ""
    Dim strFiltriArtico As String = ""

    Try
      '-------------------
      If Not SpezzaFiltri(strQuery, strFiltriTestoff, strFiltriMovoff, strFiltriTestord, strFiltriMovord, _
                          strFiltriTestprb, strFiltriMovprb, strFiltriTestmag, strFiltriMovmag, strFiltriTestmagf, _
                          strFiltriprinot, strFiltriScaden, strFiltriAnagra, strFiltriLeads, strFiltriArtico) Then Return False

      '-------------------
      If bScaden Then
        strSQL = "SELECT sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, sc_datsca, sc_causale, " & _
                 " sc_descr, sc_darave, sc_importoda, sc_flsaldato, sc_codvalu, sc_cambio, sc_impvalda, sc_datdoc, " & _
                 " sc_alfdoc, sc_numdoc, sc_insolu, sc_codpaga, sc_tippaga, sc_codbanc, sc_codcage, " & _
                 " sc_controp, sc_abi, sc_cab, sc_banc1, sc_banc2, sc_numcc, sc_cin, sc_prefiban, sc_iban, " & _
                 " sc_fldis, sc_commeca, sc_subcommeca, sc_alfpro, sc_numprot, sc_swift, sc_tiposeqrid, sc_tiporid, sc_dtmandrid, " & _
                 " sc_datreg, sc_numreg, sc_dtsaldato, sc_rgsaldato, sc_integr, sc_dtdist, sc_numdist, sc_opdist, " & _
                 " anagra.an_descr1 as xxc_conto, anagra2.an_descr1 as xxc_controp, tb_descauc as xxc_causale,  " & _
                 " tb_desvalu as xxc_codvalu, tb_despaga as xxc_codpaga, tb_desbanc as xxc_codbanc, " & _
                 " tb_descage as xxc_codcage, co_descr1 as xxc_commeca " & _
                 " FROM ((((((((scaden INNER JOIN anagra ON scaden.codditt = anagra.codditt AND scaden.sc_conto = anagra.an_conto) " & _
                 " LEFT JOIN anagra as anagra2 ON scaden.codditt = anagra2.codditt AND scaden.sc_controp = anagra2.an_conto) " & _
                 " LEFT JOIN tabcauc ON scaden.sc_causale = tabcauc.tb_codcauc) " & _
                 " LEFT JOIN tabvalu ON scaden.sc_codvalu = tabvalu.tb_codvalu) " & _
                 " LEFT JOIN tabpaga ON scaden.sc_codpaga = tabpaga.tb_codpaga) " & _
                 " LEFT JOIN tabbanc ON scaden.codditt = tabbanc.codditt AND scaden.sc_codbanc = tabbanc.tb_codbanc) " & _
                 " LEFT JOIN tabcage ON scaden.codditt = tabcage.codditt AND scaden.sc_codcage = tabcage.tb_codcage) " & _
                 " LEFT JOIN commess ON scaden.codditt = commess.codditt AND scaden.sc_commeca = commess.co_comme) " & _
                 " WHERE scaden.codditt = " & CStrSQL(strDitta)

        If strFiltriAnagra <> "" Then TraduciWhere(strFiltriAnagra, strWhere)
        If strFiltriScaden <> "" Then TraduciWhere(strFiltriScaden, strWhere)

        If strWhere = "" Then strSQL = "" 'non ho filtri sulla where: non eseguo la query
        strSQL += strWhere
      End If

      '-------------------
      If strSQL.Trim <> "" Then
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCAD", dsGri)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
