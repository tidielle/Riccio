Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD

Public Class CLDORGSOR
  Inherits CLDMGDOCU

  '#Region "Variabili"
  '
  '  Public strTestordApri As String = "SELECT testord.*, tb_destpbf as xx_tipobf, tb_desvalu as xx_valuta, tb_despaga as xx_codpaga, " & _
  '                                   "co_descr1 as xx_commeca, tb_descfam as xx_codcfam, tb_descena as xx_codcena,tb_desbanc as xx_codbanc, " & _
  '                                   "tabvett.tb_desvett as xx_vettor,tabvett2.tb_desvett as xx_vettor2,tabmagai.tb_desmaga as xx_magimp, " & _
  '                                   "tabmaga2.tb_desmaga as xx_magaz2, tb_desciva as xx_codese, tabmaga.tb_desmaga as xx_magaz, " & _
  '                                   "tabcage.tb_descage as xx_codagen, tabcage2.tb_descage as xx_codagen2, " & _
  '                                   "tb_desport as xx_porto,tb_descaum as xx_caustra,destdiv.dd_nomdest as xx_coddest, destdiv2.dd_nomdest as xx_coddest2, " & _
  '                                   "'' as xx_conto, '' as xx_tipo, '' as xx_controp, '' as xx_contfatt, " & _
  '                                   " tb_desstag as xx_codstag " & _
  '                                   " FROM (((((((((((((((((((testord LEFT JOIN tabpaga ON testord.td_codpaga = tabpaga.tb_codpaga) " & _
  '                                   " LEFT JOIN tabvalu ON testord.td_valuta = tabvalu.tb_codvalu) " & _
  '                                   " LEFT JOIN tabciva ON testord.td_codese = tabciva.tb_codciva) " & _
  '                                   " LEFT JOIN tabcaum ON testord.td_caustra = tabcaum.tb_codcaum) " & _
  '                                   " LEFT JOIN tabtpbf ON testord.codditt = tabtpbf.codditt AND testord.td_tipobf = tabtpbf.tb_codtpbf) " & _
  '                                   " LEFT JOIN commess ON testord.codditt = commess.codditt AND testord.td_commeca = commess.co_comme) " & _
  '                                   " LEFT JOIN tabcfam ON testord.codditt = tabcfam.codditt AND testord.td_codcfam = tabcfam.tb_codcfam) " & _
  '                                   " LEFT JOIN tabcena ON testord.codditt = tabcena.codditt AND testord.td_codcena = tabcena.tb_codcena) " & _
  '                                   " LEFT JOIN tabbanc ON testord.codditt = tabbanc.codditt AND testord.td_codbanc = tabbanc.tb_codbanc) " & _
  '                                   " LEFT JOIN tabvett ON testord.codditt = tabvett.codditt AND testord.td_vettor = tabvett.tb_codvett) " & _
  '                                   " LEFT JOIN tabvett as tabvett2 ON testord.codditt = tabvett2.codditt AND testord.td_vettor2 = tabvett2.tb_codvett) " & _
  '                                   " LEFT JOIN tabmaga ON testord.codditt = tabmaga.codditt AND testord.td_magaz = tabmaga.tb_codmaga) " & _
  '                                   " LEFT JOIN tabmaga as tabmaga2 ON testord.codditt = tabmaga2.codditt AND testord.td_magaz2 = tabmaga2.tb_codmaga) " & _
  '                                   " LEFT JOIN tabmaga as tabmagai ON testord.codditt = tabmagai.codditt AND testord.td_magimp = tabmagai.tb_codmaga) " & _
  '                                   " LEFT JOIN tabcage ON testord.codditt = tabcage.codditt AND testord.td_codagen = tabcage.tb_codcage) " & _
  '                                   " LEFT JOIN tabcage as tabcage2 ON testord.codditt = tabcage2.codditt AND testord.td_codagen2 = tabcage2.tb_codcage) " & _
  '                                   " LEFT JOIN tabport ON testord.codditt = tabport.codditt AND testord.td_porto = tabport.tb_codport) " & _
  '                                   " LEFT JOIN tabstag ON testord.codditt = tabstag.codditt AND testord.td_codstag = tabstag.tb_codstag) " & _
  '                                   " LEFT JOIN destdiv ON testord.codditt = destdiv.codditt AND testord.td_conto = destdiv.dd_conto AND testord.td_coddest = destdiv.dd_coddest) " & _
  '                                   " LEFT JOIN destdiv as destdiv2 ON testord.codditt = destdiv2.codditt AND testord.td_conto = destdiv2.dd_conto AND testord.td_coddest2 = destdiv2.dd_coddest "

  '  Public strMovordApri As String = "SELECT movord.*, ar_codtagl as xxo_codtagl, tabmaga.tb_desmaga as xxo_magaz, tabmaga2.tb_desmaga as xxo_magaz2, tb_descove as xxo_controp, " & _
  '                                   " tb_desciva as xxo_codiva, tb_descfam as xxo_codcfam, co_descr1 as xxo_commeca, tb_descena as xxo_codcena, " & _
  '                                   " anagraconto.an_descr1 as xxo_contocon, anagra.an_descr1 as xxo_codclie, artico.ar_tipoopz as xxo_tipoopz, tb_deslavo as xxo_codlavo, " & _
  '                                   " tsk_descr as xxo_pmtaskid, af_descr as xxo_fase, ar_gescomm as xxo_gescomm, ar_gesfasi as xxo_gesfasi, ar_coddb as xxo_coddb, " & _
  '                                   " '' as xxo_codarfo, '' as xxo_darave, 0.0 as xxo_qtadaass, 'C' as xxo_flevasass, tb_destpro as xxo_codtpro, " & _
  '                                   " 0.0 as xxo_pmqtadisda, 0.0 as xxo_pmvaldisda, 0.0 as xxo_pmqtares, 0.0 as xxo_pmvalres, 0.0 as xxo_pmqtarim, 0.0 as xxo_pmvalrim, " & _
  '                                   " ' ' as xxo_destask, 'N' as xxo_prevgrup, '1' as xxo_gestcost, ar_geslotti as xxo_geslotti, ar_gesubic as xxo_gesubic, ar_gestmatr as xxo_gestmatr " & _
  '                                   " FROM (((((((((((((movord INNER JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga) " & _
  '                                   " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart) " & _
  '                                   " LEFT JOIN tabmaga as tabmaga2 ON movord.codditt = tabmaga2.codditt AND movord.mo_magaz2 = tabmaga2.tb_codmaga) " & _
  '                                   " LEFT JOIN tabcove ON movord.codditt = tabcove.codditt AND movord.mo_controp = tabcove.tb_codcove) " & _
  '                                   " LEFT JOIN tabciva ON movord.mo_codiva = tabciva.tb_codciva) " & _
  '                                   " LEFT JOIN tabcfam ON movord.codditt = tabcfam.codditt AND movord.mo_codcfam = tabcfam.tb_codcfam) " & _
  '                                   " LEFT JOIN commess ON movord.codditt = commess.codditt AND movord.mo_commeca = commess.co_comme) " & _
  '                                   " LEFT JOIN tabcena ON movord.codditt = tabcena.codditt AND movord.mo_codcena = tabcena.tb_codcena) " & _
  '                                   " LEFT JOIN anagra as anagraconto ON movord.codditt = anagraconto.codditt AND movord.mo_contocontr = anagraconto.an_conto) " & _
  '                                   " LEFT JOIN anagra ON movord.codditt = anagra.codditt AND movord.mo_codclie = anagra.an_conto) " & _
  '                                   " LEFT JOIN tablavo ON movord.codditt = tablavo.codditt AND movord.mo_codlavo = tablavo.tb_codlavo) " & _
  '                                   " LEFT JOIN tabtpro ON movord.codditt = tabtpro.codditt AND movord.mo_codtpro = tabtpro.tb_codtpro) " & _
  '                                   " LEFT JOIN tasks ON movord.codditt = tasks.codditt AND movord.mo_commeca = tasks.tsk_commeca AND movord.mo_pmtaskid = tasks.tsk_taskid) " & _
  '                                   " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_codart = artfasi.af_codart AND movord.mo_fase = artfasi.af_fase "
  '#End Region

  Public Overridable Function EsisteOrdine(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean

    'restituisce true se l'ordine esiste, diversamente false
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bResult As Boolean = False

    Try
      strSQL = "SELECT top 1 testord.td_numord FROM testord" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipoDoc) & _
               " AND td_anno = " & nAnno & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumdoc.ToString
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEST")
      If dsTmp.Tables("TEST").Rows.Count > 0 Then bResult = True
      dsTmp.Dispose()

      Return bResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                             ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                             ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                             ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String) As DataSet
    Try
      'obsoleta
      Return ZoomSeor(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, strDatConsDa, strDatConsA, strEvaso, _
                      strRiferim, strDitta, "*", "*", False, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                           ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String, _
                           ByVal strBlocco As String, ByVal strSospeso As String) As DataSet
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, _
                                             strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta, _
                                             strBlocco, strSospeso})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      'obsoleta
      Return ZoomSeor(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, strDatConsDa, strDatConsA, strEvaso, _
                      strRiferim, strDitta, strBlocco, strSospeso, False, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                             ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                             ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                             ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String, _
                             ByVal strBlocco As String, ByVal strSospeso As String, ByVal bModuloCRM As Boolean, _
                             ByVal bIsCRMUser As Boolean, ByVal nCodcageAccdito As Integer) As DataSet
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, _
                                             strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta, strBlocco, _
                                             strSospeso, bModuloCRM, bIsCRMUser, nCodcageAccdito})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      Return ZoomSeor(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, _
                      strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta, strBlocco, _
                      strSospeso, bModuloCRM, bIsCRMUser, nCodcageAccdito, _
                      "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                           ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String, _
                           ByVal strBlocco As String, ByVal strSospeso As String, ByVal bModuloCRM As Boolean, _
                           ByVal bIsCRMUser As Boolean, ByVal nCodcageAccdito As Integer, _
                           ByVal strWhereFiltriEstesi As String, ByVal strWhereFiltriEstesiRighe As String) As DataSet
    Dim dsTmp As DataSet = New DataSet
    Dim strSQL As String = ""
    Dim lIITtsubqcrm As Integer = 0

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, _
                                             strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta, strBlocco, _
                                             strSospeso, bModuloCRM, bIsCRMUser, nCodcageAccdito})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      'limitazioni per operatore CRM
      If bModuloCRM = True And bIsCRMUser = True Then
        If CBool(GetSettingBusDitt(strDitta, "Bsorgsor", "Opzioni", ".", "Zoom_ordini_applica_restrizioni_CRM", "0", " ", "0")) Then
          lIITtsubqcrm = GetTblInstId("TTSUBQCRM", False)
          If Not RiempiTmpTable(strDitta, lIITtsubqcrm) Then Return Nothing
        Else
          bModuloCRM = False
        End If
      End If

      strSQL = "SELECT testord.*, tmp.* " & _
               " FROM testord " & _
               " INNER JOIN (SELECT DISTINCT testord.codditt, td_tipork, td_anno, td_serie, td_numord, an_descr1 as xx_conto, dd_nomdest as xx_destin, " & _
               " td_commeca, co_descr1 as xx_commeca, tb_destpbf AS xx_tipobf, tabmaga.tb_desmaga AS xx_magaz, " & _
               " tabmaga1.tb_desmaga AS xx_magaz2, tabmaga2.tb_desmaga AS xx_magimp " & _
               " FROM testord " & _
               " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto " & _
               "  LEFT JOIN destdiv ON testord.codditt = destdiv.codditt AND testord.td_conto = destdiv.dd_conto AND testord.td_coddest = destdiv.dd_coddest " & _
               "  LEFT JOIN commess ON testord.codditt = commess.codditt AND testord.td_commeca = commess.co_comme " & _
               "  LEFT JOIN tabtpbf ON testord.codditt = tabtpbf.codditt AND td_tipobf = tb_codtpbf " & _
               "  LEFT JOIN tabmaga ON testord.codditt = tabmaga.codditt AND td_magaz = tabmaga.tb_codmaga " & _
               "  LEFT JOIN tabmaga AS tabmaga1 ON testord.codditt = tabmaga1.codditt AND td_magaz2 = tabmaga1.tb_codmaga " & _
               "  LEFT JOIN tabmaga AS tabmaga2 ON testord.codditt = tabmaga2.codditt AND td_magimp = tabmaga2.tb_codmaga "
      If bModuloCRM AndAlso bIsCRMUser Then
        strSQL &= " LEFT JOIN TTSUBQCRM ON TTSUBQCRM.codditt = testord.codditt " & _
                  "   AND TTSUBQCRM.sc_conto = testord.td_conto " & _
                  "   AND TTSUBQCRM.sc_coddest = testord.td_coddest "
      End If
      If strWhereFiltriEstesi.IndexOf("movord", StringComparison.CurrentCultureIgnoreCase) > -1 Then
        strSQL &= " LEFT JOIN movord ON movord.codditt = testord.codditt AND td_tipork = mo_tipork AND td_anno = mo_anno " & _
                  "                 AND td_serie = mo_serie AND td_numord = mo_numord "
      End If

      strSQL += "WHERE td_tipork = " & CStrSQL(strTipork) & " AND td_anno = " & nAnno & " AND testord.codditt = " & CStrSQL(strDitta)
      If strSerie <> "" Then strSQL += " AND td_serie = " & CStrSQL(strSerie)
      If lContoDa <> 0 Or lContoA <> 999999999 Then strSQL += " AND td_conto BETWEEN " & lContoDa.ToString & " AND " & lContoA.ToString
      If NTSCDate(strDataDa) <> New Date(1900, 1, 1) OrElse NTSCDate(strDataA) <> New Date(2099, 12, 31) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDataDa) & " AND " & CDataSQL(strDataA)
      End If
      If NTSCDate(strDatConsDa) <> New Date(1900, 1, 1) OrElse NTSCDate(strDatConsA) <> New Date(2099, 12, 31) Then
        strSQL += " AND td_datcons BETWEEN " & CDataSQL(strDatConsDa) & " AND " & CDataSQL(strDatConsA)
      End If
      If strEvaso <> "" Then strSQL += " AND td_flevas = " & CStrSQL(strEvaso)
      If strRiferim <> "" Then
        strRiferim = "%" & strRiferim.Replace("*", "%") & "%"
        strSQL += " AND td_riferim like " & CStrSQL(strRiferim.Replace("%%", "%"))
      End If
      If strBlocco <> "*" Then strSQL &= " AND td_blocco = " & CStrSQL(strBlocco)
      If strSospeso <> "*" Then strSQL &= " AND td_sospeso = " & CStrSQL(strSospeso)

      If bModuloCRM AndAlso bIsCRMUser Then
        If nCodcageAccdito <> 0 Then
          strSQL += " AND (td_codagen = " & nCodcageAccdito & " OR td_codagen2 = " & nCodcageAccdito & ")"
        End If
        strSQL += " AND TTSUBQCRM.instid = " & lIITtsubqcrm
      End If

      TraduciWhere(strWhereFiltriEstesi, strSQL)

      strSQL &= ") AS tmp ON tmp.codditt = testord.codditt AND tmp.td_tipork = testord.td_tipork AND tmp.td_anno = testord.td_anno " & _
                "        AND tmp.td_serie = testord.td_serie AND tmp.td_numord = testord.td_numord "

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SEOR")

      Return dsTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    Finally
      If lIITtsubqcrm <> 0 Then ResetTblInstId("TTSUBQCRM", False, lIITtsubqcrm)
    End Try
  End Function


  Public Overridable Function GetDatdocLastOrd(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetDatdocLastOrd = NTSCDate(IntSetDate("01/01/1900"))

      strSQL = "SELECT TOP 1 td_datord FROM testord WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipork) & _
               " AND td_anno = " & nAnno.ToString & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " ORDER BY td_numord DESC"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        GetDatdocLastOrd = NTSCDate(dttTmp.Rows(0)!td_datord)
        dttTmp.Clear()
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "APRI / SALVA / CANCELLA ORDINE"
  Public Overridable Function ApriOrdine(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Return ApriOrdine(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM, 0)
  End Function

  Public Overridable Function ApriOrdine(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean, _
                                         ByVal nOrdrig As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, _
                                             bModPM, nOrdrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(6), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '------------------------------------
      'testord
      'strSQL = strTestordApri
      'strSQL = strSQL & " WHERE testord.codditt = " & CStrSQL(strDitta) & _
      '                  " AND td_tipork = " & CStrSQL(strTipoDoc) & _
      '                  " AND td_anno = " & nAnno & _
      '                  " AND td_serie = " & CStrSQL(strSerie)
      'If bNew Then
      '  strSQL = strSQL & " AND td_numord = -1"
      'Else
      '  strSQL = strSQL & " AND td_numord = " & lNumdoc
      'End If
      strSQL = "bussp_bsorgsor_apritestord " & _
          CStrSQL(strDitta) & ", " & _
          CStrSQL(strTipoDoc) & ", " & _
          nAnno & ", " & _
          CStrSQL(strSerie) & ", " & _
          IIf(bNew, "-1", lNumdoc).ToString
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")

      'Aggiunge le colonne xx per la gestione delle promozioni
      For z As Integer = 1 To 6
        Dim strPost As String = z.ToString.Trim("1"c)
        ds.Tables("TESTORD").Columns.Add("xx_codtes" & strPost, GetType(String))
      Next
      ds.Tables("TESTORD").Columns.Add("xx_totpunti", GetType(Integer))

      If ds.Tables("TESTORD").Rows.Count > 0 Then
        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_contfatt) <> 0 Then
          strSQL = "SELECT an_descr1 FROM anagra " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND an_conto = " & NTSCInt(ds.Tables("TESTORD").Rows(0)!td_contfatt).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTORD").Rows(0)!xx_contfatt = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_codcli) <> 0 Then
          strSQL = "SELECT an_descr1 FROM anagra " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND an_conto = " & NTSCInt(ds.Tables("TESTORD").Rows(0)!td_codcli).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTORD").Rows(0)!xx_codcli = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_controp) <> 0 Then
          strSQL = "SELECT an_descr1 FROM tabcove " & _
                   "  INNER JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto " & _
                   " WHERE tabcove.codditt = " & CStrSQL(strDitta) & _
                   "   AND tb_codcove = " & NTSCInt(ds.Tables("TESTORD").Rows(0)!td_controp).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTORD").Rows(0)!xx_controp = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        If Not ds.Tables("TESTORD").Columns.Contains("xx_organig") Then ds.Tables("TESTORD").Columns.Add("xx_organig", GetType(String))
        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_organig) <> 0 Then
          strSQL = "SELECT og_descont, og_descont2 FROM organig " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND og_progr = " & NTSCInt(ds.Tables("TESTORD").Rows(0)!td_organig).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTORD").Rows(0)!xx_organig = (NTSCStr(dttTmp.Rows(0)!og_descont) & " " & NTSCStr(dttTmp.Rows(0)!og_descont2)).Trim
          dttTmp.Clear()
        End If

        For z As Integer = 1 To 6
          Dim strPost As String = z.ToString.Trim("1"c)
         
          If NTSCInt(ds.Tables("TESTORD").Rows(0)("td_codtes" & strPost)) <> 0 Then
            strSQL = "SELECT ret_destes FROM refites " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND ret_codtes = " & NTSCStr(ds.Tables("TESTORD").Rows(0)("td_codtes" & strPost))
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then ds.Tables("TESTORD").Rows(0)("xx_codtes" & strPost) = NTSCStr(dttTmp.Rows(0)!ret_destes)
            dttTmp.Clear()
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_coddest) <> 0 Then
          With ds.Tables("TESTORD").Rows(0)
            strSQL = "SELECT * FROM destdiv" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND dd_conto = " & NTSCInt(!td_conto).ToString & _
              " AND dd_coddest = " & NTSCInt(!td_coddest).ToString
          End With
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strSQL = ""
            With dttTmp.Rows(0)
              strSQL = NTSCStr(!dd_nomdest).Trim & _
                IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
                IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
            End With
            If strSQL.Trim <> "" Then ds.Tables("TESTORD").Rows(0)!xx_coddest = strSQL
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        If NTSCInt(ds.Tables("TESTORD").Rows(0)!td_coddest2) <> 0 Then
          With ds.Tables("TESTORD").Rows(0)
            strSQL = "SELECT * FROM destdiv" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND dd_conto = " & NTSCInt(!td_conto).ToString & _
              " AND dd_coddest = " & NTSCInt(!td_coddest2).ToString
          End With
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strSQL = ""
            With dttTmp.Rows(0)
              strSQL = NTSCStr(!dd_nomdest).Trim & _
                IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
                IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
            End With
            If strSQL.Trim <> "" Then ds.Tables("TESTORD").Rows(0)!xx_coddest2 = strSQL
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
      End If
      
      'movord (occhio: aggiungo la tabella ad dataset precedentemente creato con testord)
      'strSQL = strMovordApri
      'strSQL = strSQL & " WHERE movord.codditt = " & CStrSQL(strDitta) & _
      '                  " AND mo_tipork = " & CStrSQL(strTipoDoc) & _
      '                  " AND mo_anno = " & nAnno & _
      '                  " AND mo_serie = " & CStrSQL(strSerie)
      'If bNew Then
      '  strSQL = strSQL & " AND mo_numord = -1"
      'Else
      '  strSQL = strSQL & " AND mo_numord = " & lNumdoc
      'End If
      'strSQL += " ORDER BY "
      'Select Case nOrdrig
      '  Case 1
      '    strSQL = strSQL & "mo_codart"
      '  Case 2
      '    strSQL = strSQL & "mo_descr"
      '  Case 3
      '    strSQL = strSQL & "mo_commeca, mo_subcommeca, mo_codart"
      '  Case Else
      '    strSQL = strSQL & "mo_riga"
      'End Select
      'strSQL += " ASC "
      strSQL = "bussp_bsorgsor_aprimovord " & _
              CStrSQL(strDitta) & ", " & _
              CStrSQL(strTipoDoc) & ", " & _
              nAnno & ", " & _
              CStrSQL(strSerie) & ", " & _
              IIf(bNew, -1, lNumdoc).ToString & ", " & _
              nOrdrig.ToString
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD", ds)

      ds.Tables("MOVORD").Columns.Add("xxo_valoreiva", GetType(Decimal))

      '----------------------
      'aggiorno xxo_codarfo
      If ds.Tables("MOVORD").Rows.Count > 0 Then
        strSQL = "SELECT caf_codart, caf_codarfo FROM codarfo WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND caf_conto = " & ds.Tables("TESTORD").Rows(0)!td_conto.ToString & _
                 " AND caf_codart IN (SELECT mo_codart FROM movord " & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND mo_tipork = " & CStrSQL(strTipoDoc) & _
                 " AND mo_anno = " & nAnno & _
                 " AND mo_serie = " & CStrSQL(strSerie) & _
                 " GROUP BY mo_codart)"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i = 0 To ds.Tables("MOVORD").Rows.Count - 1
            dtrT = dttTmp.Select("caf_codart = " & CStrSQL(ds.Tables("MOVORD").Rows(i)!mo_codart))
            If dtrT.Length > 0 Then
              ds.Tables("MOVORD").Rows(i)!xxo_codarfo = NTSCStr(dtrT(0)!caf_codarfo)
            End If
          Next
        End If
        dttTmp.Clear()
      End If    'If ds.Tables("MOVORD").Rows.Count > 0 Then

      '----------------------
      If Not ApriOrdineProd(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM) Then Return False

      '----------------------
      If Not ApriOrdineTCO(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM) Then Return False

      If bModPM Or lNumdoc = 0 Then 'per evitare i problemi, in form_load riempio il datatable ugualmente ... non si sa mai dopo 
        strSQL = "SELECT * FROM proesec WHERE proesec.codditt = " & CStrSQL(strDitta) & " AND pes_commeca = -1"
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PROESEC", ds)
      End If

      '------------------------------------
      'schetrasp
      strSQL = "SELECT schetrasp.*, '' as xx_vettcod, '' as xx_commstat," & _
              " '' as xx_caristat, '' as xx_propstat," & _
              " '' as xx_lcstat, '' as xx_lsstat," & _
              " '' as xx_datcomp, '' as xx_riferim" & _
              " FROM schetrasp " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sct_tipork = " & CStrSQL(strTipoDoc) & _
              " AND sct_anno = " & nAnno & _
              " AND sct_serie = " & CStrSQL(strSerie)
      If bNew Then
        strSQL = strSQL & " AND sct_numdoc = -1"
      Else
        strSQL = strSQL & " AND sct_numdoc = " & lNumdoc
      End If
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCHETRASP", ds)


      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriOrdineGetOfaSalcon(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    '-----------------------------
    'con una join tra movord IC (o OF) e ICA preleva le mo_oasalcon di IC con valore diversi da mo_flevas di ICA
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movordIC.mo_riga, movordIC.mo_oasalcon, movord.mo_flevas " & _
               " FROM movord as movordIC INNER JOIN movord ON movordIC.codditt = movord.codditt " & _
               " AND movordIC.mo_oariga = movord.mo_riga AND movordIC.mo_oanum = movord.mo_numord " & _
               " AND movordIC.mo_oaserie = movord.mo_serie AND movordIC.mo_oaanno = movord.mo_anno " & _
               " AND movordIC.mo_oatipo = movord.mo_tipork " & _
               " WHERE movordIC.codditt = " & CStrSQL(strDitta) & _
               " AND movordIC.mo_tipork = " & CStrSQL(strTipoDoc) & _
               " AND movordIC.mo_anno = " & nAnno & _
               " AND movordIC.mo_serie = " & CStrSQL(strSerie) & _
               " AND movordIC.mo_numord = " & lNumdoc & _
               " AND movordIC.mo_oasalcon <> movord.mo_flevas " & _
               " GROUP BY movordIC.mo_riga, movordIC.mo_oasalcon, movord.mo_flevas "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ApriOrdineProd(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Dim strSQL As String = ""
    Try

      '------------------------------------
      'impegni collegati: testord è identico a testord ordine di produzione

      '------------------------------------
      'strSQL = strMovordApri
      'strSQL = strSQL & " WHERE movord.codditt = " & CStrSQL(strDitta) & _
      '            " AND mo_tipork = " & CStrSQL(IIf(strTipoDoc = "H", "Y", ".")) & _
      '            " AND mo_anno = " & nAnno & _
      '            " AND mo_serie = " & CStrSQL(strSerie)
      'If bNew Or strTipoDoc <> "H" Then
      '  strSQL = strSQL & " AND mo_numord = -1"
      'Else
      '  strSQL = strSQL & " AND mo_numord = " & lNumdoc
      'End If
      'strSQL += " ORDER BY mo_riga ASC "
      strSQL = "bussp_bsorgsor_aprimovord " & _
              CStrSQL(strDitta) & ", " & _
              CStrSQL(IIf(strTipoDoc = "H", "Y", ".")) & ", " & _
              nAnno & ", " & _
              CStrSQL(strSerie) & ", " & _
              IIf(bNew Or strTipoDoc <> "H", -1, lNumdoc).ToString & ", " & _
              "0"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORDIMP", ds)

      '------------------------------------
      strSQL = "SELECT attivit.*, ar_descr as xx_coddb, tb_deslavo as xx_codlavo, " & _
               " 0.0 as xx_tempattpr, 0.0 as xx_tempesepr, " & _
               " 0.0 as xx_tempattprm, 0.0 as xx_tempeseprm, " & _
               " 0.0 as xx_tempattees, 0.0 as xx_tempesees " & _
               " FROM (attivit INNER JOIN artico ON attivit.codditt = artico.codditt AND attivit.at_coddb = artico.ar_codart) " & _
               " LEFT JOIN tablavo ON attivit.codditt = tablavo.codditt AND attivit.at_codlavo = tablavo.tb_codlavo "
      strSQL += " WHERE attivit.codditt = " & CStrSQL(strDitta) & _
               " AND at_tipork = " & CStrSQL(strTipoDoc) & _
               " AND at_anno = " & nAnno & _
               " AND at_serie = " & CStrSQL(strSerie)
      If bNew Or strTipoDoc <> "H" Then
        strSQL = strSQL & " AND at_numord = -1"
      Else
        strSQL = strSQL & " AND at_numord = " & lNumdoc
      End If
      strSQL += " ORDER BY at_riga ASC, at_fase "
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ATTIVIT", ds)

      For Each dtrT As DataRow In ds.Tables("ATTIVIT").Rows
        dtrT!xx_tempattpr = ConvOra100Ora60(NTSCDec(dtrT!at_tempattpr))
        dtrT!xx_tempesepr = ConvOra100Ora60(NTSCDec(dtrT!at_tempesepr))
        dtrT!xx_tempattprm = ConvOra60Minuti(NTSCDec(dtrT!xx_tempattpr))
        dtrT!xx_tempeseprm = ConvOra60Minuti(NTSCDec(dtrT!xx_tempesepr))
        dtrT!xx_tempattees = ConvOra100Ora60(NTSCDec(dtrT!at_tempattees))
        dtrT!xx_tempesees = ConvOra100Ora60(NTSCDec(dtrT!at_tempesees))
      Next

      '------------------------------------
      strSQL = "SELECT assris.*, tb_descent as xx_codcent, tb_descove as xx_controp " & _
               " FROM (assris LEFT JOIN tabcent ON assris.codditt = tabcent.codditt AND assris.as_codcent = tabcent.tb_codcent) " & _
               " LEFT JOIN tabcove ON assris.codditt = tabcove.codditt AND assris.as_controp = tabcove.tb_codcove "
      strSQL += " WHERE assris.codditt = " & CStrSQL(strDitta) & _
            " AND as_tipork = " & CStrSQL(strTipoDoc) & _
            " AND as_anno = " & nAnno & _
            " AND as_serie = " & CStrSQL(strSerie)
      If bNew Or strTipoDoc <> "H" Then
        strSQL = strSQL & " AND as_numord = -1"
      Else
        strSQL = strSQL & " AND as_numord = " & lNumdoc
      End If
      strSQL += " ORDER BY as_riga ASC, as_fase "
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ASSRIS", ds)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriOrdineTCO(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim nTcIndTaglF As Integer
    Dim nRiga As Integer
    Dim dtrRow2 As DataRow()
    Dim dttDestagl As New DataTable
    Try
      '-----------------------------------
      If bModTCO Or lNumdoc = 0 Then 'per evitare i problemi, in form_load riempio il datatable ugualmente ... non si sa mai dopo 
        strSQL = "SELECT * FROM movordtc"
        strSQL = strSQL & " WHERE movordtc.codditt = " & CStrSQL(strDitta) & _
                          " AND mo_tipork = " & CStrSQL(strTipoDoc) & _
                          " AND mo_anno = " & nAnno & _
                          " AND mo_serie = " & CStrSQL(strSerie)
        If bNew Then
          strSQL = strSQL & " AND mo_numord = -1"
        Else
          strSQL = strSQL & " AND mo_numord = " & lNumdoc
        End If
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORDTC", ds)

        strSQL = "SELECT * FROM movordtc"
        strSQL = strSQL & " WHERE movordtc.codditt = " & CStrSQL(strDitta) & _
                    " AND mo_tipork = " & CStrSQL(IIf(strTipoDoc = "H", "Y", ".")) & _
                    " AND mo_anno = " & nAnno & _
                    " AND mo_serie = " & CStrSQL(strSerie)
        If bNew Or strTipoDoc <> "H" Then
          strSQL = strSQL & " AND mo_numord = -1"
        Else
          strSQL = strSQL & " AND mo_numord = " & lNumdoc
        End If
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORDIMPTC", ds)

        If bModTCO Then 'Riempimento campo di MOVORDIMP
          'xxo_tcindtaglf
          nTcIndTaglF = 0
          For Each dtrRow As DataRow In ds.Tables("MOVORDIMPTC").Rows
            nRiga = NTSCInt(dtrRow!mo_riga)
            If NTSCDec(dtrRow!mo_quant01) <> 0 Then
              nTcIndTaglF = 1
            ElseIf NTSCDec(dtrRow!mo_quant02) <> 0 Then
              nTcIndTaglF = 2
            ElseIf NTSCDec(dtrRow!mo_quant03) <> 0 Then
              nTcIndTaglF = 3
            ElseIf NTSCDec(dtrRow!mo_quant04) <> 0 Then
              nTcIndTaglF = 4
            ElseIf NTSCDec(dtrRow!mo_quant05) <> 0 Then
              nTcIndTaglF = 5
            ElseIf NTSCDec(dtrRow!mo_quant06) <> 0 Then
              nTcIndTaglF = 6
            ElseIf NTSCDec(dtrRow!mo_quant07) <> 0 Then
              nTcIndTaglF = 7
            ElseIf NTSCDec(dtrRow!mo_quant08) <> 0 Then
              nTcIndTaglF = 8
            ElseIf NTSCDec(dtrRow!mo_quant09) <> 0 Then
              nTcIndTaglF = 9
            ElseIf NTSCDec(dtrRow!mo_quant10) <> 0 Then
              nTcIndTaglF = 10
            ElseIf NTSCDec(dtrRow!mo_quant11) <> 0 Then
              nTcIndTaglF = 11
            ElseIf NTSCDec(dtrRow!mo_quant12) <> 0 Then
              nTcIndTaglF = 12
            ElseIf NTSCDec(dtrRow!mo_quant13) <> 0 Then
              nTcIndTaglF = 13
            ElseIf NTSCDec(dtrRow!mo_quant14) <> 0 Then
              nTcIndTaglF = 14
            ElseIf NTSCDec(dtrRow!mo_quant15) <> 0 Then
              nTcIndTaglF = 15
            ElseIf NTSCDec(dtrRow!mo_quant16) <> 0 Then
              nTcIndTaglF = 16
            ElseIf NTSCDec(dtrRow!mo_quant17) <> 0 Then
              nTcIndTaglF = 17
            ElseIf NTSCDec(dtrRow!mo_quant18) <> 0 Then
              nTcIndTaglF = 18
            ElseIf NTSCDec(dtrRow!mo_quant19) <> 0 Then
              nTcIndTaglF = 19
            ElseIf NTSCDec(dtrRow!mo_quant20) <> 0 Then
              nTcIndTaglF = 20
            ElseIf NTSCDec(dtrRow!mo_quant21) <> 0 Then
              nTcIndTaglF = 21
            ElseIf NTSCDec(dtrRow!mo_quant22) <> 0 Then
              nTcIndTaglF = 22
            ElseIf NTSCDec(dtrRow!mo_quant23) <> 0 Then
              nTcIndTaglF = 23
            ElseIf NTSCDec(dtrRow!mo_quant24) <> 0 Then
              nTcIndTaglF = 24
            End If

            dtrRow2 = ds.Tables("MOVORDIMP").Select("mo_riga = " & nRiga)
            If dtrRow2.Length > 0 Then
              dtrRow2(0)!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow2(0)!mo_tcindtagl, nTcIndTaglF))
              ValCodiceDb(NTSCInt(dtrRow2(0)!xxo_codtagl).ToString, strDitta, "TABTAGL", "N", "", dttDestagl)
              dtrRow2(0)!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow2(0)!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
              dtrRow2(0).AcceptChanges()
            End If
          Next
        End If

      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '------------------

  Public Overridable Function SalvaOrdine(ByRef ds As DataSet, ByVal strState As String, _
                                        ByVal bSetStatoOrdlist As Boolean, ByVal bModPM As Boolean, _
                                        ByVal bModRA As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                        ByVal bNuovoDaPrev As Boolean, ByVal strTipoPrev As String, _
                                        ByVal nAnnoPrev As Integer, ByVal strSeriePrev As String, _
                                        ByVal lNumPrev As Integer, ByVal bNuovoDaImportExport As Boolean, _
                                        ByRef strError As String) As Boolean
    Try
      'obsoleta
      Return SalvaOrdine(ds, strState, bSetStatoOrdlist, bModPM, bModRA, bModTCO, bProgrCambiato, _
                         bNuovoDaPrev, strTipoPrev, nAnnoPrev, strSeriePrev, lNumPrev, bNuovoDaImportExport, _
                         strError, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaOrdine(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bSetStatoOrdlist As Boolean, ByVal bModPM As Boolean, _
                                          ByVal bModRA As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaPrev As Boolean, ByVal strTipoPrev As String, _
                                          ByVal nAnnoPrev As Integer, ByVal strSeriePrev As String, _
                                          ByVal lNumPrev As Integer, ByVal bNuovoDaImportExport As Boolean, _
                                          ByRef strError As String, ByVal strNetProDb As String) As Boolean
    'bSetStatoOrdlist: se impostata a true cancellando l'ordine non verrà riaperta l'eventuale RDA collegata
    'strState: N = new, D = delete, U = update

    Dim strSQL As String
    Dim strSQLVal As String
    Dim lNewProgr As Integer

    'Dim factory As DbProviderFactory = Nothing
    'Dim sqlCmd As DbCommand
    Dim lResult As Integer

    Dim strDesogglog As String = ""
    Dim strSqlWhere As String = ""
    Dim strDesogglogY As String = ""
    Dim strSqlWhereY As String = ""

    Dim dbConn As DbConnection = Nothing
    Dim strNote As String = ""
    Dim dttFieldAlias As New DataTable

    Dim strNoteOrig As String = ""
    Dim dttTmp As New DataTable

    Dim lComme As Integer = 0
    Dim strCommessaEsistente As String = ""
    Dim strElencoCommesseEsistenti As String = ""

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bSetStatoOrdlist, bModPM, bModRA, bModTCO, bProgrCambiato, _
                                   bNuovoDaPrev, strTipoPrev, nAnnoPrev, strSeriePrev, lNumPrev, bNuovoDaImportExport, _
                                   strError, strNetProDb})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)        'alla funzione è passato ByRef !!!!
        strError = NTSCStr(oIn(13))
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------
      'serve per rinominare le colonne precedentemenre standardizzate
      dttFieldAlias.Columns.Add("datatable", GetType(String))
      dttFieldAlias.Columns.Add("database", GetType(String))
      dttFieldAlias.Rows.Add(New Object() {"et_datdoc", "td_datord"})
      dttFieldAlias.Rows.Add(New Object() {"et_numdoc", "td_numord"})
      dttFieldAlias.Rows.Add(New Object() {"ec_numdoc", "mo_numord"})
      dttFieldAlias.AcceptChanges()

      '---------------------------------
      'scrivo actlog
      Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString.ToUpper
        Case "R" : strDesogglog = "Impegno cliente"
        Case "O" : strDesogglog = "Ordine a fornitore"
        Case "Q" : strDesogglog = "Preventivo"
        Case "H" : strDesogglog = "Ordine di produzione"
        Case "Y" : strDesogglog = "Impegno di produzione"
        Case "X" : strDesogglog = "Impegno di trasferimento"
        Case "V" : strDesogglog = "Impegno cliente aperto"
        Case "$" : strDesogglog = "Ordine a fornitore aperto"
        Case "#" : strDesogglog = "Impegno di commessa"
      End Select
      strDesogglog += " n. " & NTSCStr(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
      If ds.Tables("TESTA").Rows(0)!et_serie.ToString.Trim <> "" Then
        strDesogglog += "/" & ds.Tables("TESTA").Rows(0)!et_serie.ToString
      End If
      strDesogglog += " del " & NTSCDate(ds.Tables("TESTA").Rows(0)("et_datdoc", DataRowVersion.Original)).ToShortDateString
      strNote = strDesogglog    'le note servono anche per i nuovi impegni da preventivo ...

      If strState <> "N" Then
        strDesogglog = IIf(strState = "D", "Cancella", "Modifica").ToString() & " " & strDesogglog
        strSqlWhere = "'" & ds.Tables("TESTA").Rows(0)!codditt.ToString & "', " & _
                      "'" & ds.Tables("TESTA").Rows(0)!et_tipork.ToString & "', " & _
                      ds.Tables("TESTA").Rows(0)!et_anno.ToString & ", " & _
                      "'" & ds.Tables("TESTA").Rows(0)!et_serie.ToString & "', " & _
                      ds.Tables("TESTA").Rows(0)!et_numdoc.ToString

        'per impegni di produzione
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString.ToUpper = "H" Then
          strSqlWhereY = "'" & ds.Tables("TESTA").Rows(0)!codditt.ToString & "','Y'," & _
                          ds.Tables("TESTA").Rows(0)!et_anno.ToString & ", " & _
                          "'" & ds.Tables("TESTA").Rows(0)!et_serie.ToString & "', " & _
                          ds.Tables("TESTA").Rows(0)!et_numdoc.ToString
          strDesogglogY = IIf(strState = "D", "Cancella", "Modifica").ToString()
          strDesogglogY += " Impegno di produzione" & " n. " & NTSCStr(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          If ds.Tables("TESTA").Rows(0)!et_serie.ToString.Trim <> "" Then
            strDesogglogY += "/" & ds.Tables("TESTA").Rows(0)!et_serie.ToString
          End If
          strDesogglogY += " del " & ds.Tables("TESTA").Rows(0)("et_datdoc", DataRowVersion.Original).ToString
        End If
      Else
        strDesogglog = ""
      End If    ' If strState <> "N" Then

      '---------------------------------
      'se è un nuovo documento da import/export (ovvero ho scritto a parte testord e movord e ora devo riaggiornare
      'lo tratto come se fosse una update, solo che in fase di cancellazione documento non storno artpro/artprox/keyord ma cancello solo testord e movord
      If bNuovoDaImportExport Then strState = "U"

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If strDesogglog <> "" Then ScriviActLog(ds.Tables("TESTA").Rows(0)!codditt.ToString, "BSORGSOR", "TESTORD", "TESTORD", strSqlWhere, IIf(strState = "D", "A", "M").ToString, "D", strDesogglog, False)
      If strDesogglogY <> "" Then ScriviActLog(ds.Tables("TESTA").Rows(0)!codditt.ToString, "BSORGSOR", "TESTORD", "TESTORD", strSqlWhereY, IIf(strState = "D", "A", "M").ToString, "D", strDesogglogY, False)

      '---------------------------------
      'per personalizzaz. 
      If Not SalvaOrdineBeforeDelete(ds, strState, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If

      '----------------------------------
      ' qui, adesso, sia per Access che per SQL Server aggiorna proeseb
      ' (ho bisogno qui di avere sia il contenuto di MOTRANS che il conteuto di MOVORD, documento precedente alla modifica...)
      If bModPM Then
        With ds.Tables("TESTA").Rows(0)
          If Not AggProesebMovord(dbConn, !codditt.ToString, !et_tipork.ToString, _
                                  NTSCInt(!et_anno), !et_serie.ToString, NTSCInt(!et_numdoc), _
                                  NTSCDate(!et_datdoc).ToShortDateString, ds.Tables("CORPO"), strState) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End With
      End If

      '---------------------------------
      'prima cancello il documento (non lo faccio se è una nuovo doc)
      If strState <> "N" Then
        If Not DeleteDoc(strState, ds, dbConn, bModPM, bModRA, bModTCO, bSetStatoOrdlist, bNuovoDaImportExport, strNetProDb) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

      If strState = "D" Then GoTo FINE

      '---------------------------------
      'aggiorno il numero del documento
      If bNuovoDaImportExport And bProgrCambiato = False Then
        'se non importo i documenti in ordine di numero documento non aggiorno la numerazione
        If NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) <> LegNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "V", "VV", ds.Tables("TESTA").Rows(0)!et_tipork.ToString).ToString, ds.Tables("TESTA").Rows(0)!et_serie.ToString, NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), False, dbConn) Then
          bProgrCambiato = True
        End If
      End If
      If (strState = "N" Or bNuovoDaImportExport) And bProgrCambiato = False Then
        lNewProgr = AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "V", "VV", ds.Tables("TESTA").Rows(0)!et_tipork.ToString).ToString, _
                ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                True, True, strError, dbConn)
        '----------------------------
        'si è verificato un errore
        If lNewProgr = 0 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(strError))
        End If

        If lNewProgr <> NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) Or strError <> "" Then
          If IsInTrans Then AnnullaTrans()
          '----------------------------
          'aggiorno il numero del documento con il nuovo numero, visto che quello che avevo impostato è già stato utilizzato
          strError = "*N*" & strError   'aggiungo il marcatore che permetterà all'entity di cambiare il num doc su tutti i datatable
          ds.Tables("TESTA").Rows(0)!et_numdoc = lNewProgr

          Return False
        End If

        '----------------------------
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
          AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                  "Y", _
                  ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                  NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                  NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                  False, False, strError, dbConn)
        End If
      End If

      '---------------------------------
      'insert into TESTA
      strSQL = "INSERT INTO TESTORD " & GetQueryInsertField(ds.Tables("TESTA"), "td_", "", "et_", dttFieldAlias)
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTA"), ds.Tables("TESTA").Rows(0), "td_", "", "et_")
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 127791221369687500, "Si è verificato un errore in fase di scrittura testata ordine: ordine non salvato")))
      End If

      '---------------------------------
      'aggiorno il preventivo se questo è un nuovo ordine/impegno da preventivo
      If strState = "N" AndAlso bNuovoDaPrev AndAlso ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" AndAlso strTipoPrev = "Q" Then
        strSQL = "SELECT td_note FROM testord" & _
          " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
          " AND td_tipork = 'Q'" & _
          " AND td_anno = " & nAnnoPrev.ToString & _
          " AND td_serie = " & CStrSQL(strSeriePrev) & _
          " AND td_numord = " & lNumPrev.ToString & _
          " AND td_note IS NOT NULL"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count <> 0 Then strNoteOrig = NTSCStr(dttTmp.Rows(0)!td_note).Trim
        strNoteOrig += IIf(strNoteOrig <> "", vbCrLf & vbCrLf, "").ToString & strNote
        dttTmp.Clear()
        dttTmp.Dispose()

        strSQL = "UPDATE testord SET td_flevas = 'S', td_note = " & CStrSQL(strNoteOrig) & _
                 " WHERE td_tipork = 'Q' " & _
                 " AND td_anno = " & nAnnoPrev.ToString & _
                 " AND td_serie = " & CStrSQL(strSeriePrev) & _
                 " AND td_numord = " & lNumPrev.ToString & _
                 " AND codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "UPDATE movord SET mo_flevas = 'S' " & _
                 " WHERE mo_tipork = 'Q' " & _
                 " AND mo_anno = " & nAnnoPrev.ToString & _
                 " AND mo_serie = " & CStrSQL(strSeriePrev) & _
                 " AND mo_numord = " & lNumPrev.ToString & _
                 " AND codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      '------------------------------------
      '--- Procede ora alla generazione dell'anagraficva commessa e la attribuisce alla riga, se:
      '----- il tipo Ordine/Impegno è 'R' o '#'
      '----- è attiva la generazione automatica della commessa
      '----- è attiva la generazione automatica della commessa ALLA FINE
      '----- l'articolo è gestito a commessa
      '----- il numero di commessa di riga è zero
      Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString.ToUpper
        Case "R", "#"
          If (CBool(GetSettingBusDitt(ds.Tables("TESTA").Rows(0)!codditt.ToString, "Bsorgsor", "Opzioni", ".", "GenNumCommecaAutR", "0", " ", "0")) = True) And _
             (CBool(GetSettingBusDitt(ds.Tables("TESTA").Rows(0)!codditt.ToString, "Bsorgsor", "Opzioni", ".", "GenNumCommecaAutR_AllaFine", "-1", " ", "-1")) = True) Then

            For Each dtrC As DataRow In ds.Tables("CORPO").Select("xxo_gescomm <> 'N' AND ec_commeca = 0", "ec_riga")
              If GeneraNumeroCommessa(ds.Tables("TESTA").Rows(0), dtrC, strError, strCommessaEsistente, lComme, dbConn) = False Then
                If IsInTrans Then AnnullaTrans()
                Return False
              Else
                'aggiorno anche il cod commessa in bus, diversamdnte non viene passato a netpro
                dtrC!ec_commeca = lComme
              End If
              If strCommessaEsistente.Trim <> "" Then strElencoCommesseEsistenti += strCommessaEsistente
            Next    'For Each dtrC As DataRow In ds.Tables("CORPO").Select("", "ec_riga")

            If strElencoCommesseEsistenti.Trim <> "" Then
              strError = oApp.Tr(Me, 129868988745471190, "display_info" & "Attenzione!" & vbCrLf & _
                "Opzione per la generazione automatica commesse al salvataggio del'ordine/impegno attiva." & vbCrLf & _
                "Il documento sarà, comunque, salvato." & vbCrLf & vbCrLf & _
                "Per le seguenti righe il numero di commessa risulta già esistente:") & vbCrLf & _
                strElencoCommesseEsistenti
            End If
          End If    'If (CBool(GetSettingBusDitt(ds.Tables("TESTA").Rows(0)!codditt.ToString
      End Select

      '---------------------------------
      'se serve aggiorno Net@Pro
      If Not SalvaOrdineAggNetPro(ds, strState, strNetProDb, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '---------------------------------
      If Not SalvaOrdineMovord(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
      '---------------------------------

      If bModTCO Then
        If Not SalvaOrdineMovordTC(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
      End If

      '---------------------------------
      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
        If Not SalvaOrdineTestordImp(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        If Not SalvaOrdineMovordImp(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        If Not SalvaOrdineAttivit(ds, strState, strError, dbConn) Then Return False
        If Not SalvaOrdineAssris(ds, strState, strError, dbConn) Then Return False
        If bModTCO Then
          If Not SalvaOrdineMovordImpTC(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        End If
      End If

      '---------------------------------
      'aggiorno td_ultagg
      strSQL = "UPDATE TESTORD SET td_ultagg = " & CDataOraSQL(DateTime.Now.ToString) & _
              " WHERE td_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
              " AND td_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
              " AND td_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
              " AND td_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & _
              " AND codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 127791221370000000, "Si è verificato un errore in fase aggiornamento data ultimo aggiornamento in testata ordine: ordine non salvato")))
      End If

      '---------------------------------
      'lancio la stored procedure per l'aggiornamento di keyord/artpro/artprox/...
      strSQL = "bussp_bsorgsor9_faggiorn2 " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                CDataSQL(DateTime.Now.ToShortDateString) & ", " & _
                CStrSQL(oApp.User.Nome)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128786602370252000, "Si è verificato un errore in fase di inserimento/modifica ordine (bussp_bsorgsor9_faggiorn2): ordine non aggiornato")))
      End If

      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
        strSQL = "bussp_bsorgsor9_faggiorn2 " & _
                "'Y', " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                CDataSQL(DateTime.Now.ToShortDateString) & ", " & _
                CStrSQL(oApp.User.Nome)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221370156250, "Si è verificato un errore in fase di inserimento/modifica ordine (bussp_bsorgsor9_faggiorn2 - impegni collegati): ordine non aggiornato")))
        End If
      End If


      '---------------------------------
      'TCO: AggArtproTC
      If bModTCO Then
        If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        If Not AggMovOrdApertoTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                                NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                                NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                                NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                                NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
          If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "Y", _
                                     NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                                     NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                                     NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If
      End If

      '---------------------------------
      'bnorgnor utilizza questa funzione per poter fare l'aggiornamento di ordlist/movrdo
      If Not SalvaOrdineAggiornaOrdlist(ds, strState, bSetStatoOrdlist, bModTCO, bModRA, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '---------------------------------
      'insert into SCHETRASP
      If ds.Tables("SCHETRASP").Rows.Count > 0 Then
        strSQL = "INSERT INTO schetrasp " & GetQueryInsertField(ds.Tables("SCHETRASP"), "sct_", "", "et_")
        strSQLVal = GetQueryInsertValue(ds.Tables("SCHETRASP"), ds.Tables("SCHETRASP").Rows(0), "sct_", "", "et_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128988829130475181, "Si è verificato un errore in fase di scrittura schetrasp Documento: Documento non salvato")))
        End If
      End If

FINE:

      '---------------------------------
      'per personalizzaz. 
      If Not SalvaOrdineBeforeChiudiTrans(ds, strState, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

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
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SalvaOrdineTestordImp(ByRef ds As DataSet, ByVal strState As String, _
                                                    ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                    ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0
    Dim nMagimp As Integer = 0

    Try
      '---------------------------------
      'cambio solo il tipo documento
      ds.Tables("TESTA").Rows(0).AcceptChanges()
      ds.Tables("TESTA").Rows(0)!et_tipork = "Y"
      strSQL = "INSERT INTO testord " & GetQueryInsertField(ds.Tables("TESTA"), "td_", "", "et_", dttFieldAlias)
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTA"), ds.Tables("TESTA").Rows(0), "td_", "", "et_")
      ds.Tables("TESTA").Rows(0).RejectChanges()
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128523812467798000, "Si è verificato un errore in fase di scrittura testata ordine impegno di produzione: ordine non salvato")))
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineMovord(ByRef ds As DataSet, ByVal strState As String, _
                                                ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movord " & GetQueryInsertField(ds.Tables("CORPO"), "mo_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPO").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPO"), ds.Tables("CORPO").Rows(i), "mo_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221369843750, "Si è verificato un errore in fase di scrittura riga ordine: ordine non salvato")))
        End If
        ds.Tables("CORPO").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineMovordImp(ByRef ds As DataSet, ByVal strState As String, _
                                                    ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                    ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movord " & GetQueryInsertField(ds.Tables("CORPOIMP"), "mo_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOIMP"), ds.Tables("CORPOIMP").Rows(i), "mo_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786078922000, "Si è verificato un errore in fase di scrittura riga ordine impegno di produzione: ordine non salvato")))
        End If
        ds.Tables("CORPOIMP").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineAssris(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO assris " & GetQueryInsertField(ds.Tables("ASSRIS"), "as_")
      For i = 0 To ds.Tables("ASSRIS").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ASSRIS"), ds.Tables("ASSRIS").Rows(i), "as_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523787362646000, "Si è verificato un errore in fase di scrittura riga centro associato all'attività: ordine non salvato")))
        End If
        ds.Tables("ASSRIS").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineAttivit(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO attivit " & GetQueryInsertField(ds.Tables("ATTIVIT"), "at_")
      For i = 0 To ds.Tables("ATTIVIT").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ATTIVIT"), ds.Tables("ATTIVIT").Rows(i), "at_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523787185586000, "Si è verificato un errore in fase di scrittura riga attività: ordine non salvato")))
        End If
        ds.Tables("ATTIVIT").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineMovordTC(ByRef ds As DataSet, ByVal strState As String, _
                                                  ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                  ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movordtc " & GetQueryInsertField(ds.Tables("CORPOTC"), "mo_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOTC").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOTC"), ds.Tables("CORPOTC").Rows(i), "mo_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786354886000, "Si è verificato un errore in fase di scrittura riga ordine TC: ordine non salvato")))
        End If
        ds.Tables("CORPOTC").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineMovordImpTC(ByRef ds As DataSet, ByVal strState As String, _
                                                  ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                  ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movordtc " & GetQueryInsertField(ds.Tables("CORPOIMPTC"), "mo_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOIMPTC").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOIMPTC"), ds.Tables("CORPOIMPTC").Rows(i), "mo_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786566422000, "Si è verificato un errore in fase di scrittura riga ordine TC impegno di produzione: ordine non salvato")))
        End If
        ds.Tables("CORPOIMPTC").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggProesebMovord(ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                             ByVal strTipork As String, ByVal nAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                             ByVal strDatdoc As String, ByRef dttCorpo As DataTable, _
                                             ByVal strState As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Dim strIdNew As String = ""     'sono le nuove righe di ttpriana che non c'erano in priana all'apertura della registrazione
    Try
      'se è una delete devo solo cancellare tutto !!!
      If strState = "D" Then
        strTmp = "'0'"
        strIdNew = ""
        GoTo CANCELLA
      End If

      ' cerca i record modificati
AGGIORNA:
      For Each dtrT As DataRow In dttCorpo.Rows
        strTmp += "'" & dtrT!codditt.ToString & "." & dtrT!ec_tipork.ToString & "." & dtrT!ec_anno.ToString & "." & _
                  dtrT!ec_serie.ToString & "." & dtrT!ec_numdoc.ToString & "." & dtrT!ec_riga.ToString & "', "   'mi servirà dopo ...

        strSQL = "SELECT td_datord, mo_commeca, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_quant, " & _
                 " mo_valore, mo_perqta, mo_pmtaskid, mo_pmsalcon, mo_pmqtadis, mo_pmvaldis " & _
                 " FROM " & strJoinTestordMovord & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND mo_tipork = " & CStrSQL(strTipork) & _
                 " AND mo_anno = " & nAnno.ToString & _
                 " AND mo_serie = " & CStrSQL(strSerie.ToString) & _
                 " AND mo_numord = " & lNumdoc.ToString & _
                 " AND mo_riga = " & dtrT!ec_riga.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn) 'ottengo priana di origine con ec_progr = a quello in memoria
        If dttTmp.Rows.Count = 0 Then strIdNew += "'" & dtrT!codditt.ToString & "." & dtrT!ec_tipork.ToString & "." & dtrT!ec_anno.ToString & "." & _
                                                   dtrT!ec_serie.ToString & "." & dtrT!ec_numdoc.ToString & "." & dtrT!ec_riga.ToString & "', "

        'se i dati di pm sono cambiati tra record di origine e quello in memoria, storno e riaggiorno PROESEB
        For Each dtrP As DataRow In dttTmp.Rows
          If (Not (NTSCInt(dtrT!ec_commeca) = NTSCInt(dtrP!mo_commeca) And _
                   NTSCInt(dtrT!ec_pmtaskid) = NTSCInt(dtrP!mo_pmtaskid) And _
                   NTSCDec(dtrT!ec_quant) = NTSCDec(dtrP!mo_quant) And _
                   NTSCDec(dtrT!ec_valore) = NTSCDec(dtrP!mo_valore) And _
                   NTSCDec(dtrT!ec_pmvaldis) = NTSCDec(dtrP!mo_pmvaldis) And _
                   NTSCStr(dtrT!ec_pmsalcon) = NTSCStr(dtrP!mo_pmsalcon) And _
                   NTSCDec(dtrT!ec_pmqtadis) = NTSCDec(dtrP!mo_pmqtadis))) And _
                  (NTSCInt(dtrT!ec_pmtaskid) <> 0 Or NTSCInt(dtrP!mo_pmtaskid) <> 0) Then
            ' ora crea proeseb storno
            If NTSCInt(dtrP!mo_pmtaskid) <> 0 Then
              AggProesebDaMovordDao(strDitta, 1, "MOVORD", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                                    NTSCInt(dtrP!mo_riga), NTSCDate(dtrP!td_datord).ToShortDateString, dbConn, Nothing)
            End If
            ' ora crea proeseb dati nuovi
            If NTSCInt(dtrT!ec_pmtaskid) <> 0 Then
              AggProesebDaMovordDao(strDitta, 0, "MOTRANS", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                                    NTSCInt(dtrP!mo_riga), NTSCDate(dtrP!td_datord).ToShortDateString, dbConn, dttCorpo)
            End If
          End If
        Next    'For Each dtrP As DataRow In dttPriaOrig.Rows

        dttTmp.Clear()
      Next    'For Each dtrT As DataRow In dttPriana.Rows
      If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 2)

      '--------------------------------------------
      'ora cancella i record in priana che non ci sono più in ttpriana
      'se dalla registraz. originaria di CG ho cancellato l'unica riga che rimandava alla CA,
      'dttPriana è vuota... devo comunque eseguire la delete 
CANCELLA:
      strSQL = "SELECT mo_riga, td_datord FROM " & strJoinTestordMovord & _
               " WHERE movord.codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipork) & _
               " AND mo_anno = " & nAnno.ToString & _
               " AND mo_serie = " & CStrSQL(strSerie) & _
               " AND mo_numord = " & lNumdoc.ToString & _
               " AND mo_pmtaskid <> 0"
      If strTmp.Length > 0 Then strSQL += " AND (movord.codditt + '.' + mo_tipork + '.' + cast(mo_anno as varchar(4)) + '.' + " & _
                  "mo_serie + '.' + cast(mo_numord as varchar(9)) + '.' + cast(mo_riga as varchar(9))) NOT IN (" & strTmp & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      For Each dtrT As DataRow In dttTmp.Rows
        AggProesebDamovordDao(strDitta, 1, "movord", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                      NTSCInt(dtrT!mo_riga), NTSCDate(dtrT!td_datord).ToShortDateString, dbConn, Nothing)

      Next    'For Each dtrT As DataRow In dttPriana.Rows
      dttTmp.Clear()

INSERISCI:
      ' ora inserisce i record nuovi di ttpriana che non c'erano in priana
      If strIdNew.Length > 0 Then
        strIdNew = strIdNew.Substring(0, strIdNew.Length - 2)
        For Each dtrT As DataRow In dttCorpo.Select("ec_pmtaskid <> 0 AND (codditt + '.' + ec_tipork + '.' + ec_anno + '.' + " & _
                  "ec_serie + '.' + ec_numdoc + '.' + ec_riga) IN (" & strIdNew & ")")
          AggProesebDamovordDao(strDitta, 0, "MOTRANS", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                      NTSCInt(dtrT!ec_riga), strDatdoc, dbConn, dttCorpo)
        Next
      End If    'If strIdNew.Length > 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function




  '------------------

  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                      ByVal bModPM As Boolean, ByVal bModRA As Boolean, ByVal bModTCO As Boolean, _
                                      ByVal bSetStatoOrdlist As Boolean, ByVal bNuovoDaImportExport As Boolean) As Boolean
    Try
      'obosleta
      Return DeleteDoc(strState, ds, dbConn, bModPM, bModRA, bModTCO, bSetStatoOrdlist, bNuovoDaImportExport, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                        ByVal bModPM As Boolean, ByVal bModRA As Boolean, ByVal bModTCO As Boolean, _
                                        ByVal bSetStatoOrdlist As Boolean, ByVal bNuovoDaImportExport As Boolean, _
                                        ByVal strNetProDB As String) As Boolean
    'bSetStatoOrdlist: se impostata a true cancellando l'ordine non verrà riaperta l'eventuale RDA collegata
    'strState: N = new, D = delete, U = update
    Dim strSQL As String = ""
    'Dim factory As DbProviderFactory = Nothing
    'Dim sqlCmd As DbCommand = Nothing
    Dim lResult As Integer

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModRA, bModTCO, bSetStatoOrdlist, bNuovoDaImportExport, strNetProDB})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)        'alla funzione è passato ByRef !!!!
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      If Not DeletedocAggNetPro(ds, strState, strNetProDB, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '---------------------------
      If strState = "D" Then

        If bModRA And bSetStatoOrdlist Then
          'Aggiorna il campo di ORDLIST ol_stato da 'T' a 'S'
          strSQL = "UPDATE ordlist SET ol_stato = 'S' FROM testord, movord" & _
                  " WHERE testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork" & _
                  " AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno" & _
                  " AND testord.td_numord = movord.mo_numord AND ordlist.codditt = movord.codditt" & _
                  " AND ordlist.ol_orriga = movord.mo_riga AND ordlist.ol_ornum = movord.mo_numord" & _
                  " AND ordlist.ol_orserie = movord.mo_serie AND ordlist.ol_oranno = movord.mo_anno" & _
                  " AND ordlist.ol_ortipork = movord.mo_tipork" & _
                  " AND testord.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                  " AND td_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                  " AND td_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                  " AND td_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                  " AND td_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & _
                  " AND ol_stato = 'T'"
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If lResult = -1 Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Throw (New NTSException(oApp.Tr(Me, 128786602437644000, "Si è verificato un errore in fase di aggiornamento proposte d'ordine: ordine non salvato")))
          End If

          'Sgancia i campi collegati all'ordine in ORDLIST
          strSQL = "UPDATE ordlist SET ol_ortipork = 'O'," & _
                  " ol_oranno = 0," & _
                  " ol_orserie = ' '," & _
                  " ol_ornum = 0," & _
                  " ol_orriga = 0" & _
                  " FROM testord, movord" & _
                  " WHERE testord.codditt = movord.codditt" & _
                  " AND testord.td_tipork = movord.mo_tipork" & _
                  " AND testord.td_serie = movord.mo_serie" & _
                  " AND testord.td_anno = movord.mo_anno" & _
                  " AND testord.td_numord = movord.mo_numord" & _
                  " AND ordlist.codditt = movord.codditt" & _
                  " AND ordlist.ol_orriga = movord.mo_riga" & _
                  " AND ordlist.ol_ornum = movord.mo_numord" & _
                  " AND ordlist.ol_orserie = movord.mo_serie" & _
                  " AND ordlist.ol_oranno = movord.mo_anno" & _
                  " AND ordlist.ol_ortipork = movord.mo_tipork" & _
                  " AND testord.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                  " AND td_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                  " AND td_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                  " AND td_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                  " AND td_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If lResult = -1 Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Throw (New NTSException(oApp.Tr(Me, 128526316567336000, "Si è verificato un errore in fase di aggiornamento proposte d'ordine: ordine non salvato")))
          End If
        End If    'If bModRA And bSetStatoOrdlist Then

        '------------------------
        'Cancellazione eventuali records in ALLOLE
        If Not DeleteDocAllole(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                              ds.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                              ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), True, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        '------------------------
        'se posso porto il progressivo di tabnuma indietro di un numero
        'solo se il numero documento è uguale a quello di tabnuma ..)
        If Not DelNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                       IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "V", "VV", ds.Tables("TESTA").Rows(0)!et_tipork.ToString).ToString, _
                       ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                       NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                       NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                       dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
      End If    'If strState = "D" Then

      '---------------------------
      'cancello il documento con la Stored procedure
      If bNuovoDaImportExport Then
        'è un documento creato da import/export ed in fase di salvataggio
        'devo solo cancellare testord e movord, visto che nessuna ha mai scritto keyord o aggiornato artpro/artprox/ ...
        strSQL = "DELETE FROM movord WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mo_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mo_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mo_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mo_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM movordtc WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mo_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mo_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mo_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mo_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
          strSQL = "DELETE FROM movord WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mo_tipork = " & CStrSQL("Y") & _
                   " AND mo_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mo_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mo_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM movordtc WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mo_tipork = " & CStrSQL("Y") & _
                   " AND mo_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mo_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mo_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM attivit WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND at_tipork = " & CStrSQL("H") & _
                   " AND at_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND at_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND at_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM assris WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND as_tipork = " & CStrSQL("H") & _
                   " AND as_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND as_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND as_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM testord WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND td_tipork = " & CStrSQL("Y") & _
                   " AND td_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND td_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND td_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then

        strSQL = "DELETE FROM testord WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND td_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND td_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND td_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND td_numord = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        'cancellazione normale
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
          If bModTCO Then
            If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "Y", _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                              NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Return False
            End If
          End If

          strSQL = "bussp_bsorgsor9_fcancella " & _
                    CStrSQL("Y") & ", " & _
                    CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                    CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                    CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                    CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                    CStrSQL(IIf(bModTCO = True, "S", "N").ToString) & ", " & _
                    CDataSQL(DateTime.Now.ToShortDateString) & ", " & _
                    CStrSQL(oApp.User.Nome)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If lResult = -1 Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Throw (New NTSException(oApp.Tr(Me, 128526341060870000, "Si è verificato un errore in fase di cancellazione impegno di produzione: ordine non cancellato")))
          End If
        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then

        'vecchio sistema
        'sqlCmd = factory.CreateCommand()
        'sqlCmd.CommandType = CommandType.StoredProcedure
        'sqlCmd.CommandText = "bussp_bsorgsor9_fcancella"
        'sqlCmd.Parameters.Add(CreateParam(factory, "@tipodoc", DbType.AnsiStringFixedLength, 1, ds.Tables("TESTA").Rows(0)!et_tipork.ToString))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@anno", DbType.Int16, 0, ds.Tables("TESTA").Rows(0)!et_anno.ToString))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@serie", DbType.AnsiStringFixedLength, 1, ds.Tables("TESTA").Rows(0)!et_serie.ToString))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@numdoc", DbType.Int32, 0, ds.Tables("TESTA").Rows(0)!et_numdoc.ToString))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, ds.Tables("TESTA").Rows(0)!codditt.ToString))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@bModTCO", DbType.AnsiStringFixedLength, 1, "N"))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@dtData", DbType.DateTime, 0, CDataSQL(DateTime.Now.ToShortDateString).Replace("'", "")))
        'sqlCmd.Parameters.Add(CreateParam(factory, "@stropnome", DbType.AnsiStringFixedLength, 20, oApp.User.Nome))
        'lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)

        If bModTCO Then
          If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If

          If Not AggMovOrdApertoTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                                 NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                                 NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                                 NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                                 NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If

        strSQL = "bussp_bsorgsor9_fcancella " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & ", " & _
                  CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                  CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                  CStrSQL(IIf(bModTCO = True, "S", "N").ToString) & ", " & _
                  CDataSQL(DateTime.Now.ToShortDateString) & ", " & _
                  CStrSQL(oApp.User.Nome)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221369375000, "Si è verificato un errore in fase di cancellazione ordine: ordine non cancellato")))
        End If
      End If

      'cancello schetrasp
      strSQL = "DELETE FROM schetrasp WHERE" & _
         " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
         " AND sct_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
         " AND sct_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
         " AND sct_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
         " AND sct_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function SalvaOrdineBeforeDelete(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineAggiornaOrdlist(ByRef ds As DataSet, ByVal strState As String, _
                                      ByVal bSetStatoOrdlist As Boolean, ByVal bModTCO As Boolean, _
                                      ByVal bModRA As Boolean, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'utilizzata per poter eseguire operazioni prima della chiusura della transazione, esempio da bnorgnor
      'per aggiornare ordlist/movrdo

      If Not ds.Tables.Contains("ORDLISTTMP") Then Return True 'non sono stato lanciato da BEORGNOR

      'se ho degli ordlist passatimi e non ho il modulo RDA devo cancellare gli ordlist
      If bModRA = False Then
        For Each dtrT As DataRow In ds.Tables("ORDLISTTMP").Rows

          'se ordine di prod devo cancellare anche zzattivit, zzassris e impegni di prod (righe Y)
          If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
            strSQL = "DELETE FROM zzattivit WHERE at_progr = " & dtrT!ol_progr.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM zzassris WHERE as_progr = " & dtrT!ol_progr.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          'modulo TCO
          If bModTCO Then
            strSQL = "DELETE FROM ordlisttc WHERE ol_progr = " & dtrT!ol_progr.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          'cancello ordlist OF, IT e OP righe H e Y
          strSQL = "DELETE FROM ordlist WHERE ol_progr = " & dtrT!ol_progr.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        Next
      Else

        'a desso aggiorna se presente RDA/RDO , il rif. in ORDLIST alla riga di MOVORD generata
        'sia su ordlist righe H che su righe Y
        For Each dtrT As DataRow In ds.Tables("ORDLISTTMP").Rows
          strSQL = "UPDATE ordlist SET " & _
            " ol_stato = " & CStrSQL(dtrT!ol_stato.ToString) & ", " & _
            " ol_ortipork = " & CStrSQL(dtrT!ol_ortipork.ToString) & ", " & _
            " ol_oranno = " & dtrT!ol_oranno.ToString & ", " & _
            " ol_orserie = " & CStrSQL(dtrT!ol_orserie.ToString) & ", " & _
            " ol_ornum = " & dtrT!ol_ornum.ToString & ", " & _
            " ol_orriga = " & dtrT!ol_orriga.ToString & _
            " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
            " AND ol_progr = " & dtrT!ol_progr.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next

      End If      'If bModRA = False Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaOrdineBeforeChiudiTrans(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '------------------


  Public Overridable Sub AggiornaFlagStampato(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer)
    Dim strSQL As String = ""

    Try
      strSQL = "UPDATE testord SET td_flstam = 'S' " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipoDoc) & _
               " AND td_anno = " & nAnno.ToString & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumdoc.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal strTipoDoc As String, _
                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Dim bUsaKeyOrdWord As Boolean = CBool(GetSettingBusDitt(strDitta, "Bsorgsor", "Opzioni", ".", "UsaKeyordWord", "0", " ", "0"))

    Try
      Return " WHERE testord.codditt = " & CStrSQL(strDitta) & _
             " AND td_tipork = " & CStrSQL(strTipoDoc) & _
             " AND td_anno = " & nAnno & _
             " AND td_serie = " & CStrSQL(strSerie) & _
             " AND td_numord = " & lNumdoc.ToString & _
             IIf(bUsaKeyOrdWord = True, " AND ko_magaz = mo_magaz", "").ToString & " "

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryOrderStampaWord(ByVal nOrdin As Integer) As String
    Try
      Select Case nOrdin
        Case 0 : Return "ORDER BY mo_riga"
        Case 1 : Return "ORDER BY mo_codart"
        Case 2 : Return "ORDER BY mo_descr"
        Case 3 : Return "ORDER BY mo_commeca"
        Case Else : Return ""
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryStampaPdf(ByVal strDitta As String, ByVal strTipoDoc As String, _
                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Try
      Return " SELECT td_tipork as tipork, td_anno as anno, td_serie as serie, td_numord as numero, " & _
             "td_conto as conto, td_valuta as valuta, td_scorpo as scorpo, td_codagen as agente, " & _
             "td_coddest AS destin " & _
             " FROM testord " & _
             " WHERE testord.codditt = " & CStrSQL(strDitta) & _
             " AND td_tipork = " & CStrSQL(strTipoDoc) & _
             " AND td_anno = " & nAnno & _
             " AND td_serie = " & CStrSQL(strSerie) & _
             " AND td_numord = " & lNumdoc.ToString & _
             " ORDER BY td_valuta, td_scorpo, td_tipork, td_anno, td_serie, td_numord "

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetTebmagaTerzFormTabcent(ByVal strDitta As String, ByVal nCodcent As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT tabmaga.* " & _
               " FROM tabcent INNER JOIN TABMAGA ON tabcent.codditt = TABMAGA.codditt " & _
               " AND tabcent.tb_magterz = TABMAGA.tb_codmaga" & _
               " WHERE tabcent.codditt  =" & CStrSQL(strDitta) & _
               " AND tb_codcent = " & nCodcent.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function GetQuaevaOFA(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                           ByVal strTipoDocOFA As String, _
                                           ByVal nAnnoOFA As Integer, ByVal strSerieOFA As String, ByVal lNumdocOFA As Integer, _
                                           ByVal nRigaOFA As Integer, ByRef QuaEvasa As Decimal, _
                                           ByRef QuaDisEvasa As Decimal, ByRef ColDisEvasa As Decimal, _
                                           ByRef ValDisEvasa As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori aperti collegati all'ordine in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT mo_quant - mo_quaeva as QuaDiff, " & _
               " mo_colli - mo_coleva AS ColDiff, mo_valore, mo_flevas, " & _
               " mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga " & _
               " FROM movord WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipoDocOFA) & _
               " AND mo_anno = " & nAnnoOFA.ToString & _
               " AND mo_serie = " & CStrSQL(strSerieOFA) & _
               " AND mo_numord = " & lNumdocOFA.ToString & _
               " AND mo_riga = " & nRigaOFA.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un ordine in modifica determino quanta qta era stata evasa con questo ordine
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(mo_quant) AS QuaEvasa, Sum(mo_oaqtadis) AS QuaDisEvasa, " & _
              " Sum(mo_oacoldis) AS ColDisEvasa, Sum(mo_oavaldis) AS ValDisEvasa " & _
              " FROM movord" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mo_tipork = " & CStrSQL(strTipoDoc) & _
              " AND mo_anno = " & nAnno.ToString & _
              " AND mo_serie = " & CStrSQL(strSerie) & _
              " AND mo_numord = " & lNumdoc.ToString & _
              " AND mo_oatipo = " & CStrSQL(strTipoDocOFA) & _
              " AND mo_oaanno = " & nAnnoOFA.ToString & _
              " AND mo_oaserie = " & CStrSQL(strSerieOFA) & _
              " AND mo_oanum = " & lNumdocOFA.ToString & _
              " AND mo_oariga = " & nRigaOFA.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          QuaEvasa = NTSCDec(dttTmp.Rows(0)!QuaEvasa)
          QuaDisEvasa = NTSCDec(dttTmp.Rows(0)!QuaDisEvasa)
          ColDisEvasa = NTSCDec(dttTmp.Rows(0)!ColDisEvasa)
          ValDisEvasa = NTSCDec(dttTmp.Rows(0)!ValDisEvasa)
        End If
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

  Public Overridable Function GetQuaevaOFAtc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                           ByVal strTipoDocOFA As String, _
                                           ByVal nAnnoOFA As Integer, ByVal strSerieOFA As String, ByVal lNumdocOFA As Integer, _
                                           ByVal nRigaOFA As Integer, ByRef QuaEvasa() As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori aperti collegati all'ordine in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strTabella As String = ""
    Dim i As Integer = 0
    Try


      strTabella = "movord"

      strSQL = "SELECT mo_flevas AS Flevas"
      For i = 1 To 24
        strSQL += ", mo_quant" & i.ToString("00") & " - mo_quaeva" & i.ToString("00") & " as QuaDiff" & i.ToString("00")
      Next
      strSQL += " FROM movord INNER JOIN movordtc ON movordtc.codditt = movord.codditt " & _
                " AND movordtc.mo_tipork = movord.mo_tipork AND movordtc.mo_anno = movord.mo_anno " & _
                " AND movordtc.mo_serie = movord.mo_serie AND movordtc.mo_numord = movord.mo_numord " & _
                " AND movordtc.mo_riga = movord.mo_riga " & _
                " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                " AND movord.mo_tipork = " & CStrSQL(strTipoDocOFA) & _
                " AND movord.mo_anno = " & nAnnoOFA.ToString & _
                " AND movord.mo_serie = " & CStrSQL(strSerieOFA) & _
                " AND movord.mo_numord = " & lNumdocOFA.ToString & _
                " AND movord.mo_riga = " & nRigaOFA.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un ordine in modifica determino quanta qta era stata evasa con questo ordine
      If lNumdoc <> 0 Then
        strSQL = ""
        For i = 1 To 24
          strSQL += ", SUM(mo_qtadis" & i.ToString("00") & ") as QuaEvasa" & i.ToString("00")
        Next
        strSQL = " SELECT" & strSQL.Substring(1)
        strSQL += " FROM " & strTabella & " INNER JOIN movordtc ON " & strTabella & ".codditt = movordtc.codditt " & _
                  " AND " & strTabella & ".mo_tipork = movordtc.mo_tipork AND " & strTabella & ".mo_anno = movordtc.mo_anno " & _
                  " AND " & strTabella & ".mo_serie = movordtc.mo_serie AND " & strTabella & ".mo_numord = movordtc.mo_numord " & _
                  " AND " & strTabella & ".mo_riga = movordtc.mo_riga " & _
                  " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
                  " AND " & strTabella & ".mo_tipork = " & CStrSQL(strTipoDoc) & _
                  " AND " & strTabella & ".mo_anno = " & nAnno.ToString & _
                  " AND " & strTabella & ".mo_serie = " & CStrSQL(strSerie) & _
                  " AND " & strTabella & ".mo_numord = " & lNumdoc.ToString & _
                  " AND " & strTabella & ".mo_oatipo = " & CStrSQL(strTipoDocOFA) & _
                  " AND " & strTabella & ".mo_oaanno = " & nAnnoOFA.ToString & _
                  " AND " & strTabella & ".mo_oaserie = " & CStrSQL(strSerieOFA) & _
                  " AND " & strTabella & ".mo_oanum = " & lNumdocOFA.ToString & _
                  " AND " & strTabella & ".mo_oariga = " & nRigaOFA.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i = 1 To 24
            QuaEvasa(i) = NTSCDec(dttTmp.Rows(0)("QuaEvasa" & i.ToString("00")))
          Next
        End If
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

  Public Overridable Function GetTimeStampTestord(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTimeStampTestord = NTSCDate(IntSetDate("01/01/1900"))
      strSQL = "SELECT td_ultagg FROM testord" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(strTipoDoc) & _
              " AND td_anno = " & nAnno & _
              " AND td_serie = " & CStrSQL(strSerie) & _
              " AND td_numord = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then

        GetTimeStampTestord = NTSCDate(dttTmp.Rows(0)!td_ultagg)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckRDAcollegate(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer) As Boolean
    '-----------------------------
    'ritorna true se sono prenti RDA collegate all'ordine 
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      CheckRDAcollegate = False

      strSQL = "SELECT top 1 td_tipork" & _
              " FROM ordlist INNER JOIN (testord INNER JOIN movord ON (testord.codditt = movord.codditt) AND " & _
              " (testord.td_tipork = movord.mo_tipork) AND (testord.td_serie = movord.mo_serie) AND " & _
              " (testord.td_anno = movord.mo_anno) AND (testord.td_numord = movord.mo_numord)) ON " & _
              " (ordlist.codditt = movord.codditt) AND (ordlist.ol_orriga = movord.mo_riga) AND " & _
              " (ordlist.ol_ornum = movord.mo_numord) AND (ordlist.ol_orserie = movord.mo_serie) AND " & _
              " (ordlist.ol_oranno = movord.mo_anno) AND (ordlist.ol_ortipork = movord.mo_tipork)" & _
              " WHERE testord.codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(strTipoDoc) & _
              " AND td_anno = " & nAnno & _
              " AND td_serie = " & CStrSQL(strSerie) & _
              " AND td_numord = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function BloccaEvasione(ByVal strDitta As String, ByVal strOrtipo As String, ByVal nOranno As Integer, _
                                             ByVal strOrserie As String, ByVal lOrnum As Integer, ByVal lOrriga As Integer, _
                                             ByRef dttOut As DataTable) As Boolean
    '-------------------------------
    'restituisce gli estremi della nota di prelievo evasa in conto collegata alla riga d'ordine passata in input
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_anno, mm_serie, mm_numdoc, mm_riga FROM movprb" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND mm_ortipo = " & CStrSQL(strOrtipo) & _
                " AND mm_oranno = " & nOranno & _
                " AND mm_orserie = " & CStrSQL(strOrserie) & _
                " AND mm_ornum = " & lOrnum & _
                " AND mm_orriga = " & lOrriga & _
                " AND mm_nprflevas = 'C'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BloccaEvasioneY(ByVal strDitta As String, ByVal strOrtipo As String, ByVal nOranno As Integer, _
                                           ByVal strOrserie As String, ByVal lOrnum As Integer, ByVal lOrriga As Integer, _
                                           ByRef dttOut As DataTable) As Boolean
    '-------------------------------
    'restituisce gli estremi della nota di prelievo evasa in conto collegata alla riga degli scarichi collegati della riga d'ordine passata in input
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_anno, mm_serie, mm_numdoc, mm_riga" & _
              " FROM (movprb INNER JOIN movord ON movprb.codditt = movord.codditt AND movprb.mm_ortipo = movord.mo_tipork AND movprb.mm_oranno = movord.mo_anno AND movprb.mm_orserie = movord.mo_serie AND movprb.mm_ornum = movord.mo_numord AND movprb.mm_orriga = movord.mo_riga)" & _
              " INNER JOIN movord As movord_1 ON movord.codditt = movord_1.codditt AND movord.mo_tiporkor = movord_1.mo_tipork AND movord.mo_annoor = movord_1.mo_anno AND movord.mo_serieor = movord_1.mo_serie AND movord.mo_numordor = movord_1.mo_numord AND movord.mo_rigaor = movord_1.mo_riga" & _
              " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
              " AND movord.mo_rigaor = " & lOrriga & _
              " AND mm_ortipo = 'Y'" & _
              " AND mm_oranno = " & nOranno & _
              " AND mm_orserie = " & CStrSQL(strOrserie) & _
              " AND mm_ornum = " & lOrnum & _
              " AND mm_nprflevas = 'C'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ScriviMotransPerCreaNota(ByVal strDitta As String, ByVal lIIMotrans As Integer, ByVal dttEc As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0
    Dim dttMotrans As New DataTable
    Dim dttTmp As New DataTable

    Try
      '---------------------------------
      'riempio motrans solo per scrivere la nota di prelievo: una volta che bsorgnno verrà riscritto questa routine non servirà più
      dttMotrans = dttEc.Clone
      For i = 0 To dttEc.Rows.Count - 1
        dttMotrans.ImportRow(dttEc.Rows(i))
      Next

      'rimuovo le colonne non presenti su motrans
      strSQL = "SELECT top 0 * FROM motrans WHERE codditt = 'asdf' AND instid = 1234 AND mo_riga = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
RESTART:
      For Each oCol As DataColumn In dttMotrans.Columns
        If oCol.ColumnName.StartsWith("ec_") Then
          If dttTmp.Columns.Contains("mo" & oCol.ColumnName.Substring(2)) = False Then
            dttMotrans.Columns.Remove(oCol.ColumnName)
            GoTo RESTART
          End If
        End If
      Next
      dttTmp.Clear()
      dttTmp = Nothing


      dttMotrans.Columns("xxo_qtadaass").ColumnName = "ec_qtadaass"
      dttMotrans.Columns("xxo_flevasass").ColumnName = "ec_flevasass"

      strSQL = "INSERT INTO motrans " & GetQueryInsertField(dttMotrans, "mo_", "", "ec_")
      strSQL = strSQL.Trim.Substring(0, strSQL.Trim.Length - 1) & ", instid) "
      For i = 0 To dttMotrans.Rows.Count - 1
        strSQLVal = GetQueryInsertValue(dttMotrans, dttMotrans.Rows(i), "mo_", "", "ec_")
        strSQLVal = strSQLVal.Trim.Substring(0, strSQLVal.Trim.Length - 1) & ", " & lIIMotrans & ")"
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttMotrans.Clear()
      dttEc.AcceptChanges()
    End Try
  End Function

#Region "Import offerte / Ordini impegni aperti"
  Public Overridable Function GetWhereHlof(ByVal strDitta As String, ByVal lContoLead As Integer) As String
    Try
      Return GetWhereHlof(strDitta, lContoLead, 0)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetWhereHlof(ByVal strDitta As String, ByVal lContoLead As Integer, _
    ByVal nValuta As Integer) As String
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lContoLead, nValuta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      GetWhereHlof = ""
      '--------------------------------------------------------------------------------------------------------------
      strSQL = " td_tipork = '!'" & _
        " AND td_codlead = " & lContoLead.ToString & _
        " AND td_annull = 'N'" & _
        " AND td_rilasciato = 'S'" & _
        " AND td_valuta = " & nValuta
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetMovoff(ByVal strDitta As String, ByVal strTipork As String, _
                                        ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lNumord As Integer, ByVal nVers As Integer, _
                                        ByVal strSoloConfermate As String, _
                                        ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM movoff " & _
               " WHERE movoff.codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipork) & _
               " AND mo_anno = " & nAnno & _
               " AND mo_serie = " & CStrSQL(strSerie) & _
               " AND mo_numord = " & lNumord & _
               " AND mo_vers = " & nVers
      Select Case strSoloConfermate
        Case "0"
          strSQL = strSQL & " AND movoff.mo_abband = 'N' AND mo_flevas = 'C'"
        Case Else
          strSQL = strSQL & " AND movoff.mo_confermato = 'S' AND mo_flevas = 'C'"
      End Select
      strSQL = strSQL & " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_vers, mo_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHlmoOfa(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal bScorpo As Boolean, ByVal bSelContoDiv As Boolean, _
                                           ByVal lConto As Integer, ByVal nValuta As Integer, _
                                           ByVal bVisPrecOrd As Boolean) As String
    Dim strSQL As String = ""
    Dim strTipord As String = ""
    Dim strScorpo As String = ""
    Try
      GetWhereHlmoOfa = ""

      If bVisPrecOrd Then
        'per visualizza precedenti movimenti
        strSQL = "td_conto = " & lConto.ToString & " AND mo_flevas <> 'S'"
      Else
        'per selezione righe ordini


        'Scorporo
        If bScorpo Then strScorpo = "S" Else strScorpo = "N"
        'Costruisce la WHERE della query
        Select Case strTipork
          'Select Case strTmTipork
          Case "R", "#"
            strTipord = "td_tipork = 'V'"
          Case "O"
            strTipord = "td_tipork = '$'"
        End Select

        If Not bSelContoDiv Then
          strSQL = strTipord & " AND td_conto = " & lConto.ToString
        Else
          strSQL = strTipord
        End If
        strSQL = strSQL & " AND td_valuta = " & nValuta.ToString & " AND td_scorpo = '" & strScorpo & "' AND mo_flevas <> 'S'"
      End If


      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTestord(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumord As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testord " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipork) & _
               " AND td_anno = " & nAnno & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumord
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region


  Public Overridable Function GetFirstTabcove(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT TOP 1 * FROM tabcove WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      GetFirstTabcove = NTSCInt(dttTmp.Rows(0)!tb_codcove)
      dttTmp.Clear()

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetNnchiam(ByVal strDitta As String, ByVal nCodchia As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM nnchiam" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND op_codchia = " & nCodchia
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDettaglioEvasione(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                                    ByRef dttOut As DataTable) As Boolean
    '----------------------------------
    'restituisce con quali documenti è stato evaso l'ordine
    Dim strSQL As String = ""
    Try
      If strTipork = "$" Or strTipork = "V" Then
        'impegno/ordine aperto
        strSQL = "SELECT mo_tipork as mm_tipork, mo_anno as mm_anno, mo_serie as mm_serie, mo_numord as mm_numdoc, mo_riga as mm_riga, mo_quant as mm_quant, mo_oaqtadis as mm_qtadisimp " & _
                 " FROM " & strJoinTestordMovord & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND mo_oatipo = " & CStrSQL(strTipork) & _
                 " AND mo_oaanno = " & nAnno & _
                 " AND mo_oaserie = " & CStrSQL(strSerie) & _
                 " AND mo_oanum = " & lNumdoc.ToString & _
                 " AND mo_oariga = " & lRiga.ToString
      Else
        'altri 
        strSQL = "SELECT mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_quant, mm_qtadisimp " & _
                 " FROM movmag " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_ortipo = " & CStrSQL(strTipork) & _
                 " AND mm_oranno = " & nAnno & _
                 " AND mm_orserie = " & CStrSQL(strSerie) & _
                 " AND mm_ornum = " & lNumdoc.ToString & _
                 " AND mm_orriga = " & lRiga.ToString & _
                 "UNION SELECT mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_quant, mm_qtadisimp " & _
                 " FROM movprb " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_ortipo = " & CStrSQL(strTipork) & _
                 " AND mm_oranno = " & nAnno & _
                 " AND mm_orserie = " & CStrSQL(strSerie) & _
                 " AND mm_ornum = " & lNumdoc.ToString & _
                 " AND mm_orriga = " & lRiga.ToString
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "OrdStornaImportExport"
  Public Overridable Function CheckPM(ByVal strDitta As String, ByVal strtipoRk As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumDoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Count(*) As Records FROM movord" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strtipoRk) & _
               " AND mo_anno = " & nAnno & _
               " AND mo_serie = " & CStrSQL(strSerie) & _
               " AND mo_numord = " & lNumDoc & _
               " AND mo_pmtaskid <> 0"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function OrdStornaImportExport(ByVal strDitta As String, ByVal bModTco As Boolean, ByVal strTipoRk As String, ByVal nAnno As Integer, _
                                                    ByVal strSerie As String, ByVal lNumord As Integer, ByRef strMsg As String) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre la transazione
      '-----------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '-----------------------------------------------------------------------------------------
      '--- storna, MOVORD, ARTPRO
      '-----------------------------------------------------------------------------------------
      'Storna ARTPROTC
      If bModTco Then
        If Not AggArtproTC_AggDaMovord(strDitta, strTipoRk, nAnno, strSerie, lNumord, True, dbConn) Then
          strMsg = oApp.Tr(Me, 129381696883906250, "Si sono verificati degli errori nello storno di ARTPRO TC." & vbCrLf & _
                   "    Elaborazione non effettuata.")
          Return False
        End If
        'MOVORDTC
        If Not AggMovOrdApertoTC(strDitta, strTipoRk, nAnno, strSerie, lNumord, 1, dbConn) Then
          strMsg = oApp.Tr(Me, 129381110124017538, "Si sono verificati degli errori nello storno ordini aperti TC (AggMovOrdApertoTC)." & vbCrLf & _
                                                   "    Elaborazione non effettuata.")
          Return False
        End If
      End If
      strSQL = "bussp_aggproord9 " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumord & ", 1, " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "bussp_aggmovordoa9 " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumord & ", 1, " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'CRM
      strSQL = "bussp_aggmovoff " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumord & ", 1, " & _
      CStrSQL(strDitta) & ", " & CDataSQL(Now)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'CRM
      strSQL = "bussp_aggtestoff " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & _
               CStrSQL(strSerie) & ", " & lNumord & ", " & CStrSQL(strDitta) & ", " & _
               CDataSQL(Now) & ", " & CStrSQL(oApp.User.Nome)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '-----------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '-----------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
    End Try
  End Function
#End Region

  Public Overridable Function GetTipcenaCa2(ByVal strDitta As String, ByVal nCodcena As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTipcenaCa2 = "*"
      strSQL = "SELECT tb_ttipcena FROM tabcena INNER JOIN tabtcdc ON tabcena.tb_codtcdc = tabtcdc.tb_codtcdc " & _
               " WHERE tabcena.codditt = " & CStrSQL(strDitta) & " AND tb_codcena = " & nCodcena
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!tb_ttipcena).ToUpper

      Return "*"

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function SalvaDocumentoTestDataComp(ByVal strDitta As String, ByVal nControp As Integer, _
                                                         ByVal bCa As Boolean, ByRef bGestDateComp As Boolean) As Boolean
    '------------------
    'ritorna bGestDateComp = true se il conto do CG o il conto di CA gestiscono le date di competenza economica
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      bGestDateComp = False

      strSQL = "SELECT an_accperi, ac_accperi" & _
        " FROM (tabcove LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto)" & _
        " LEFT JOIN anagca ON tabcove.codditt = anagca.codditt AND tabcove.tb_concova = anagca.ac_conto" & _
        " WHERE tabcove.codditt = " & CStrSQL(strDitta) & _
        " AND tb_codcove = " & nControp & _
        " AND (an_accperi = 'S' " & IIf(bCa, "OR ac_accperi = 'S'", "").ToString & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!an_accperi.ToString = "S" Or dttTmp.Rows(0)!ac_accperi.ToString = "D" Then
          bGestDateComp = True
        End If
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


  Public Overridable Function GetMotransDaGestNuov(ByVal strDitta As String, ByVal lId As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM motrans WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lId
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Limitaizoni CRM"
  Public Overridable Function RitornaCodcageAccdito(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT opdi_codcage FROM accdito" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return 0 Else Return NTSCInt(dttTmp.Rows(0)!opdi_codcage)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RiempiTmpTable(ByVal strDitta As String, ByVal lIITtsubqcrm As Integer) As Boolean
    'limitazioni per CRM
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      'pulisco il temporaneo
      ResetTblInstId("TTSUBQCRM", False, lIITtsubqcrm)

      'se serve aggiungo anche i fornitori
      strSQL = "SELECT * FROM accdito WHERE codditt = " & CStrSQL(strDitta) & " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTSUBQCRM (codditt, instid, sc_conto, sc_coddest, sc_codlead)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", le_conto, le_coddest, le_codlead" & _
        " FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_codlead IN " & SubQueryFiltroLeadVis(strDitta)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!opdi_amm).ToUpper = "S" Then
          strSQL += " UNION select " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", an_conto, 0, 0 " & _
                    " FROM anagra " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & " AND an_tipo = 'F'"
          'se ci sono anche destinazioni diverse su fornitori ...
          strSQL += " UNION select " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", an_conto, dd_coddest, 0 " & _
                    " FROM anagra INNER JOIN destdiv ON anagra.codditt = destdiv.codditt AND " & _
                    " anagra.an_conto = destdiv.dd_conto " & _
                    " WHERE anagra.codditt = " & CStrSQL(strDitta) & " AND an_tipo = 'F'"
        End If
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
    End Try
  End Function
#End Region

  Public Overridable Function GeneraNumeroCommessa(ByVal dtrT As DataRow, ByVal dtrM As DataRow, _
    ByRef strErroreOut As String, ByRef strCommessaEsistente As String, ByRef lComme As Integer, _
    ByRef dbConn As DbConnection) As Boolean
    'Dim bTipoGen2New As Boolean = False
    Dim strTipoGen2New As String
    Dim i As Integer = 0
    Dim nRigacom As Integer = 0
    Dim nSecolo As Integer = 0
    Dim nAnnoa As Integer = 0
    Dim nSerie As Integer = 0
    Dim lNum As Integer = 0
    Dim strDescrArt As String = IIf(NTSCStr(dtrM!ec_descr) <> "", NTSCStr(dtrM!ec_descr), " ").ToString
    Dim strDescr As String = ""
    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strDescr3 As String = ""
    Dim strTotDescr As String = ""
    Dim strSubcomme As String = ""
    Dim strErr As String = ""
    Dim strGruppo As String = ""
    Dim strNumero As String = ""
    Dim strSQL As String = ""
    Dim strTipogen As String = GetSettingBusDitt(NTSCStr(dtrT!codditt), "BSORGSOR", "OPZIONI", ".", "TipoGenNumCommeca", "1", " ", "1")
    Dim strGenbudget As String = GetSettingBusDitt(NTSCStr(dtrT!codditt), "BSORGSOR", "OPZIONI", ".", "GenBudgetGenNumCommeca", "-1", " ", "-1")
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strTipogen = "2" Then
        strTipoGen2New = GetSettingBusDitt(NTSCStr(dtrT!codditt), "BSORGSOR", "OPZIONI", ".", "TipoGenNumCommeca2New", "0", " ", "0")
      End If

      '--------------------------
      If (strTipogen <> "2") Or (strTipogen = "2" And (strTipoGen2New <> "1")) Then
        nRigacom = NTSCInt(Microsoft.VisualBasic.Int(NTSCInt(dtrM!ec_riga) / 100))
        nRigacom = NTSCInt(dtrM!ec_riga) - nRigacom * 100
      Else
        nRigacom = NTSCInt(Microsoft.VisualBasic.Int(NTSCInt(dtrM!ec_riga) / 1000))
        nRigacom = NTSCInt(dtrM!ec_riga) - nRigacom * 1000
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina 2 numeri per l'anno
      '--------------------------------------------------------------------------------------------------------------
      nSecolo = NTSCInt(Microsoft.VisualBasic.Int(NTSCInt(dtrT!et_anno) / 100))
      nAnnoa = NTSCInt(dtrT!et_anno) - nSecolo * 100
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina 2 numeri per la serie (o un numero)
      '--------------------------------------------------------------------------------------------------------------
      If (strTipogen <> "2") Or (strTipogen = "2" And (strTipoGen2New = "0")) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Due numeri per la serie
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dtrT!et_serie) = " " Then
          nSerie = 0
        Else
          nSerie = NTSCInt(IIf(IsNumeric(NTSCStr(dtrT!et_serie)), Asc(NTSCStr(dtrT!et_serie)) - 48, Asc(NTSCStr(dtrT!et_serie)) - 64))
        End If
      Else
        If strTipoGen2New = "-1" Then ' 1 solo numero per la serie
          If NTSCStr(dtrT!et_serie) = " " Then
            nSerie = 0
          Else
            If Asc(NTSCStr(dtrT!et_serie)) < 74 Then   ' controllare
              nSerie = NTSCInt(IIf(IsNumeric(NTSCStr(dtrT!et_serie)), Asc(NTSCStr(dtrT!et_serie)) - 48, Asc(NTSCStr(dtrT!et_serie)) - 64))
            End If
          End If
        Else ' nessun numero per la serie
          'ok
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      strDescr = "Ordine n° " & NTSCStr(dtrT!et_numdoc) & " del " & NTSCDate(dtrT!et_datdoc).ToShortDateString & " " & NTSCStr(dtrM!ec_descr)
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora compone il numero
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipogen
        Case "1"
          lComme = nAnnoa * 10000000 + nSerie * 100000 + NTSCInt(dtrT!et_numdoc)
          strTotDescr = strDescr & " " & NTSCStr(dtrM!ec_desint) & " " & NTSCStr(dtrM!ec_note)
        Case "2"
          If strTipoGen2New = "-1" Then lComme = nAnnoa * 10000000 + nSerie * 1000000 + NTSCInt(dtrT!et_numdoc) * 100 + nRigacom
          If strTipoGen2New = "0" Then lComme = nAnnoa * 10000000 + nSerie * 100000 + NTSCInt(dtrT!et_numdoc) * 100 + nRigacom
          If strTipoGen2New = "1" Then lComme = nAnnoa * 10000000 + NTSCInt(dtrT!et_numdoc) * 1000 + nRigacom
          strTotDescr = strDescr & " Riga:" & NTSCStr(dtrM!ec_riga) & " " & NTSCStr(dtrM!ec_desint) & " " & NTSCStr(dtrM!ec_note)
        Case "3"
          lComme = nAnnoa * 10000000 + nSerie * 100000 + NTSCInt(dtrT!et_numdoc)
          strTotDescr = strDescr & " Riga:" & NTSCStr(dtrM!ec_riga) & " " & NTSCStr(dtrM!ec_desint) & " " & NTSCStr(dtrM!ec_note)
          strSubcomme = NTSCStr(dtrM!ec_riga).ToString.PadLeft(2, "0"c).Substring(0, 2)
        Case "4" ' numero progr. di commessa
          lNum = LegNuma(NTSCStr(dtrT!codditt), "CO", "", 0, True, dbConn)
          lNum = AggNuma(NTSCStr(dtrT!codditt), "CO", "", 0, lNum, True, False, strErr, dbConn)
          If strErr <> "" Then
            strErroreOut = strErr
            Return False
          End If
          lComme = lNum
          strTotDescr = strDescr & " Riga:" & NTSCStr(dtrM!ec_riga) & " " & NTSCStr(dtrM!ec_desint) & " " & NTSCStr(dtrM!ec_note)
        Case "5" ' sottogruppo merc. + numero progr. commessa
          ValCodiceDb(NTSCStr(dtrM!ec_codart), NTSCStr(dtrT!codditt), "ARTICO", "S", "", dttTmp)
          strGruppo = dttTmp.Rows(0)!ar_gruppo.ToString.PadLeft(2, "0"c).Substring(0, 2)
          dttTmp.Clear()
          dttTmp.Dispose()
          lNum = LegNuma(NTSCStr(dtrT!codditt), "CO", "", 0, True, dbConn)
          lNum = AggNuma(NTSCStr(dtrT!codditt), "CO", "", 0, lNum, True, False, strErr, dbConn)
          If strErr <> "" Then
            strErroreOut = strErr
            Return False
          End If
          If lNum > 9999999 Then Return False
          strNumero = lNum.ToString.PadLeft(7, "0"c).Substring(0, 7)
          lComme = NTSCInt(strGruppo & strNumero)
          strTotDescr = strDescr & " Riga:" & NTSCStr(dtrM!ec_riga) & " " & NTSCStr(dtrM!ec_desint) & " " & NTSCStr(dtrM!ec_note)
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If strTotDescr.Length > 40 Then
        strDescr1 = Microsoft.VisualBasic.Left(strTotDescr, 40)
      Else
        strDescr1 = Microsoft.VisualBasic.Left(strTotDescr, strTotDescr.Length)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strTotDescr.Length > 40 Then
        strDescr2 = Mid(strTotDescr, 41, 40)
        If strTotDescr.Length > 80 Then
          strDescr3 = Mid(strTotDescr, 81, strTotDescr.Length - 80)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Crea il record in COMMESS
      '--------------------------------------------------------------------------------------------------------------
      strCommessaEsistente = ""
      strSQL = "SELECT co_comme FROM commess" & _
          " WHERE co_comme = " & lComme & _
          " AND codditt = " & CStrSQL(dtrT!codditt) & _
          " ORDER BY co_comme"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        strCommessaEsistente = oApp.Tr(Me, 129868993248994360, "Commessa: |" & NTSCStr(lComme) & "|, riga: |" & NTSCStr(dtrM!ec_riga) & "|" & vbCrLf)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      If strCommessaEsistente = "" Then
        strSQL = "INSERT INTO commess (codditt, co_comme, co_conto, co_descr1, co_descr2, co_dtaper, co_dtagg," & _
          " co_dtchiu, co_dtscad, co_note, co_listmat, co_impricavi, co_impprov, co_impprovimp, co_impricaviimp," & _
          " co_percavlav, co_codseta, co_codarea, co_tipork, co_anno, co_serie, co_numord, co_riga, co_codprog," & _
          " co_stato, co_codtcom, co_ultagg, co_opnome, co_codtimp, co_coddest, co_sede, co_cartella, co_cqprogr," & _
          " co_respons, co_coninterm, co_congestore, co_dataggb, co_dataggc, co_apmod, co_opnomeap, co_notetecn," & _
          " co_notegest, co_notecomm, co_prior, co_codcena, co_coddica, co_coddicv, co_codtcdc)" & _
          " VALUES (" & CStrSQL(dtrT!codditt) & ", " & lComme & ", " & NTSCInt(dtrT!et_conto) & ", " & _
          CStrSQL(strDescr1) & ", " & CStrSQL(strDescr2) & ", " & CDataSQL(NTSCStr(dtrT!et_datdoc)) & ", " & _
          CDataSQL(DateTime.Now.ToShortDateString) & ", " & CDataSQL(IntSetDate("31/12/2099")) & ", " & _
          CDataSQL(IntSetDate("31/12/2099")) & ", " & CStrSQL(strDescr3) & ", 1, 0, 0, 0, 0, 0, 0, 0, " & _
          CStrSQL(dtrT!et_tipork) & ", " & NTSCInt(dtrT!et_anno) & ", " & CStrSQL(dtrT!et_serie) & ", " & _
          NTSCInt(dtrT!et_numdoc) & ", " & _
          NTSCInt(IIf(strTipogen = "3" Or strTipogen = "1", 0, NTSCInt(dtrM!ec_riga))) & "," & _
          " 0, '1', ' ', " & CDataOraSQL(Now) & ", " & CStrSQL(oApp.User.Nome) & ", 0, 0, ' ', NULL, 0, 0, 0, 0," & _
          " NULL, NULL , 'N', NULL, NULL, NULL, NULL, 0, 0, ' ', ' ', 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (strCommessaEsistente.Trim <> "") And (strTipogen = "3") Then strCommessaEsistente = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Crea il record in COMMESS
      '--------------------------------------------------------------------------------------------------------------
      If (strTipogen = "3") And (NTSCInt(dtrM!ec_riga) > 0) And (strCommessaEsistente = "") Then
        strSQL = "INSERT INTO subcomm (codditt, sco_commeca, sco_subcommeca, sco_descr, sco_note, sco_tipork," & _
            " sco_anno, sco_serie, sco_numord, sco_riga)" & _
            " VALUES (" & CStrSQL(dtrT!codditt) & ", " & lComme & ", " & CStrSQL(strSubcomme) & ", " & _
            CStrSQL(dtrM!ec_codart) & ", " & CStrSQL(strDescrArt) & ", " & CStrSQL(dtrT!et_tipork) & ", " & _
            NTSCInt(dtrT!et_anno) & ", " & CStrSQL(dtrT!et_serie) & ", " & NTSCInt(dtrT!et_numdoc) & ", " & _
            NTSCInt(dtrM!ec_riga) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
        '--------------------------------------------------------------------------------------------------------------
SaltaCreazioneCOMMESS:
        '--------------------------------------------------------------------------------------------------------------
        If strGenbudget <> "-1" Then Return True
        '--------------------------------------------------------------------------------------------------------------
        '--- Adesso crea il budget ricopiandolo dallo schema budget
        '--------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT pb_riga, pb_subcommeca, pb_conto, pb_origine, pb_darave, pb_unmis FROM probudget" & _
          " WHERE pb_tipork = 'C'" & _
          " AND codditt = " & CStrSQL(dtrT!codditt) & _
          " ORDER BY codditt, pb_tipork, pb_riga"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          i = 0
          For i = 0 To (dttTmp.Rows.Count - 1)
            With dttTmp.Rows(i)
              strSQL = "INSERT INTO budget (codditt, bu_tipork, bu_escomp, bu_commeca, bu_codcena, bu_codcfam," & _
                " bu_riga, bu_subcommeca, bu_conto, bu_origine, bu_darave, bu_unmis)" & _
                " VALUES (" & CStrSQL(dtrT!codditt) & " , 'C', 0, " & lComme & ", 0, ' ', " & _
              CDblSQL(NTSCDec(!pb_riga)) & ", " & CStrSQL(!pb_subcommeca) & ", " & NTSCInt(!pb_conto) & ", " & _
              CStrSQL(!pb_origine) & ", " & CStrSQL(!pb_darave) & ", " & CStrSQL(!pb_unmis) & ")"
            End With
            Try
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Catch
              '--- Ignora gli eventuali errori di chiave duplicata, perché in VB6, la gdb.Execute non scatenava erriri
            End Try
          Next
        End If
        '--------------------------------------------------------------------------------------------------------------
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT * FROM probudgetd" & _
          " WHERE pbd_tipork = 'C'" & _
          " AND codditt = " & CStrSQL(dtrT!codditt)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          i = 0
          For i = 0 To (dttTmp.Rows.Count - 1)
            With dttTmp.Rows(i)
              strSQL = "INSERT INTO budgetd (codditt, bud_tipork, bud_escomp, bud_commeca, bud_codcena, bud_codcfam," & _
                " bud_riga, bud_rigaa, bud_codart, bud_desart, bud_codlavo, bud_deslavo, bud_unmis)" & _
                " VALUES " & CStrSQL(dtrT!codditt) & ", 'C', 0, " & lComme & ", 0, ' ', " & _
                CDblSQL(NTSCDec(!pbd_riga)) & ", " & NTSCInt(!pbd_rigaa) & ", " & CStrSQL(!pbd_codart) & ", " & _
                CStrSQL(!pbd_desart) & ", " & NTSCInt(!pbd_codlavo) & ", " & CStrSQL(!pbd_deslavo) & ", " & _
                CStrSQL(!pbd_unmis) & ")"
            End With
            Try
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Catch
              '--- Ignora gli eventuali errori di chiave duplicata, perché in VB6, la gdb.Execute non scatenava erriri
            End Try
          Next
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------------------------------------
        Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function CheckOrdInAvlavp(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lce_progr, lce_orriga, lce_codart, lce_orfase, sum (lce_qtaes + lce_qtascart) as xx_qta " & _
                " FROM avlavp WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lce_ortipo = " & CStrSQL(strTipoDoc) & _
                " AND lce_oranno = " & nAnno & _
                " AND lce_orserie = " & CStrSQL(strSerie) & _
                " AND lce_ornum = " & lNumdoc & _
                " GROUP BY lce_progr, lce_orriga, lce_codart, lce_orfase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class

