Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CLIE
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

  Public oCleClie As CLE__CLIE
  Public dsClie As DataSet
  Public oCallParams, oCallParamsOld As CLE__CLDP
  Public dcClie As BindingSource = New BindingSource
  Public bEntratoInAnaext As Boolean = False
  Public bNoTestNewAnaext As Boolean = False
  Public bGestAnaext As Boolean = False
  Public bAccmod As Boolean = False
  Public strVis_Note_Conto As String = "N"
  Public bChiudiAlSalvataggio As Boolean = False
  Public dtrOrganig As DataRow = Nothing

  Private components As System.ComponentModel.IContainer

#Region "Dichiarazione controlli"

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents lbAn_conto As NTSInformatica.NTSLabel
  Public WithEvents edAn_conto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_descr1 As NTSInformatica.NTSLabel
  Public WithEvents edAn_descr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_descr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents lbAn_persfg As NTSInformatica.NTSLabel
  Public WithEvents cbAn_persfg As NTSInformatica.NTSComboBox
  Public WithEvents ckAn_profes As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_siglaric As NTSInformatica.NTSLabel
  Public WithEvents edAn_siglaric As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckAn_soggresi As NTSInformatica.NTSCheckBox
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents tlbCalcolaCodFisc As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRitornaCodFisc As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents pnPag3 As NTSInformatica.NTSPanel
  Public WithEvents ckAn_condom As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codling As NTSInformatica.NTSLabel
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents lbAn_indir As NTSInformatica.NTSLabel
  Public WithEvents edAn_indir As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_cap As NTSInformatica.NTSLabel
  Public WithEvents edAn_cap As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_citta As NTSInformatica.NTSLabel
  Public WithEvents edAn_citta As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_prov As NTSInformatica.NTSLabel
  Public WithEvents edAn_prov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_stato As NTSInformatica.NTSLabel
  Public WithEvents edAn_stato As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codfis As NTSInformatica.NTSLabel
  Public WithEvents edAn_codfis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_pariva As NTSInformatica.NTSLabel
  Public WithEvents edAn_pariva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_telef As NTSInformatica.NTSLabel
  Public WithEvents edAn_telef As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_faxtlx As NTSInformatica.NTSLabel
  Public WithEvents edAn_faxtlx As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_destin As NTSInformatica.NTSLabel
  Public WithEvents edAn_destin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_destpag As NTSInformatica.NTSLabel
  Public WithEvents edAn_destpag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_email As NTSInformatica.NTSLabel
  Public WithEvents edAn_email As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_usaem As NTSInformatica.NTSLabel
  Public WithEvents cbAn_usaem As NTSInformatica.NTSComboBox
  Public WithEvents pnPag2 As NTSInformatica.NTSPanel
  Public WithEvents lbAn_pronasc As NTSInformatica.NTSLabel
  Public WithEvents edAn_pronasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_stanasc As NTSInformatica.NTSLabel
  Public WithEvents edAn_stanasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codfisest As NTSInformatica.NTSLabel
  Public WithEvents edAn_codfisest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_cognome As NTSInformatica.NTSLabel
  Public WithEvents edAn_cognome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_nome As NTSInformatica.NTSLabel
  Public WithEvents edAn_nome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codcomn As NTSInformatica.NTSLabel
  Public WithEvents edAn_codcomn As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_nazion1 As NTSInformatica.NTSLabel
  Public WithEvents edAn_nazion1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_nazion2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_nazion2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_estcodiso As NTSInformatica.NTSLabel
  Public WithEvents edAn_estcodiso As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_estpariva As NTSInformatica.NTSLabel
  Public WithEvents edAn_estpariva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codling As NTSInformatica.NTSLabel
  Public WithEvents edAn_codling As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmWeb As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_webpwd As NTSInformatica.NTSLabel
  Public WithEvents edAn_webpwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_website As NTSInformatica.NTSLabel
  Public WithEvents edAn_website As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_webuid As NTSInformatica.NTSLabel
  Public WithEvents edAn_webuid As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnPag1Dx As NTSInformatica.NTSPanel
  Public WithEvents pnPag1Sx As NTSInformatica.NTSPanel
  Public WithEvents pnPag1Bottom As NTSInformatica.NTSPanel
  Public WithEvents ckAn_omocodice As NTSInformatica.NTSCheckBox
  Public WithEvents cbAn_sesso As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_datnasc As NTSInformatica.NTSLabel
  Public WithEvents edAn_datnasc As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_citnasc As NTSInformatica.NTSLabel
  Public WithEvents edAn_citnasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_cell As NTSInformatica.NTSLabel
  Public WithEvents edAn_cell As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_codcomu As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codcomu As NTSInformatica.NTSLabel
  Public WithEvents edAn_codcomu As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_stato As NTSInformatica.NTSLabel
  Public WithEvents lbAn_tpsogiva As NTSInformatica.NTSLabel
  Public WithEvents lbAn_statofed As NTSInformatica.NTSLabel
  Public WithEvents edAn_statofed As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbAn_tpsogiva As NTSInformatica.NTSComboBox
  Public WithEvents fmIndirizzi As NTSInformatica.NTSGroupBox
  Public WithEvents pnIndirSx As NTSInformatica.NTSPanel
  Public WithEvents pnIndirDx As NTSInformatica.NTSPanel
  Public WithEvents cmdAltriIndir As NTSInformatica.NTSButton
  Public WithEvents lbXx_destpag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_destin As NTSInformatica.NTSLabel
  Public WithEvents cmdDestcorr As NTSInformatica.NTSButton
  Public WithEvents cmdDestresan As NTSInformatica.NTSButton
  Public WithEvents cmdDestsedel As NTSInformatica.NTSButton
  Public WithEvents cmdDestdomf As NTSInformatica.NTSButton
  Public WithEvents ckDestresan As NTSInformatica.NTSCheckBox
  Public WithEvents ckDestcorr As NTSInformatica.NTSCheckBox
  Public WithEvents ckDestsedel As NTSInformatica.NTSCheckBox
  Public WithEvents ckDestdomf As NTSInformatica.NTSCheckBox
  Public WithEvents fmNascita As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_stanasc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codcomn As NTSInformatica.NTSLabel
  Public WithEvents lbXx_nazion1 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_nazion2 As NTSInformatica.NTSLabel
  Public WithEvents fmNonresidenti As NTSInformatica.NTSGroupBox
  Public WithEvents fmPersfisica As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_sesso As NTSInformatica.NTSLabel
  Public WithEvents tsClie As NTSInformatica.NTSTabControl
  Public WithEvents tlbCli As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbForn As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbXx_codmast As NTSInformatica.NTSLabel
  Public WithEvents lbMastroLabel As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnDatiContabili As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnFornitura As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents pnExport As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage7 As NTSInformatica.NTSTabPage
  Public WithEvents pnNote As NTSInformatica.NTSPanel
  Public WithEvents lbAn_note As NTSInformatica.NTSLabel
  Public WithEvents edAn_note As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_note2 As NTSInformatica.NTSMemoBox
  Public WithEvents lbAn_note2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_titolo As NTSInformatica.NTSLabel
  Public WithEvents edAn_titolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdPartitario As NTSInformatica.NTSButton
  Public WithEvents pnDatiSx As NTSInformatica.NTSPanel
  Public WithEvents lbXx_categ As NTSInformatica.NTSLabel
  Public WithEvents edAn_categ As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_categ As NTSInformatica.NTSLabel
  Public WithEvents lbXx_agente As NTSInformatica.NTSLabel
  Public WithEvents edAn_agente As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_agente As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codcana As NTSInformatica.NTSLabel
  Public WithEvents edAn_codcana As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codcana As NTSInformatica.NTSLabel
  Public WithEvents lbXx_claprov As NTSInformatica.NTSLabel
  Public WithEvents edAn_claprov As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_claprov As NTSInformatica.NTSLabel
  Public WithEvents lbXx_clascon As NTSInformatica.NTSLabel
  Public WithEvents edAn_clascon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_clascon As NTSInformatica.NTSLabel
  Public WithEvents lbXx_agente2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_agente2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_agente2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_rating As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_rating As NTSInformatica.NTSLabel
  Public WithEvents fmAcquisizione As NTSInformatica.NTSGroupBox
  Public WithEvents edAn_contatt As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_contatt As NTSInformatica.NTSLabel
  Public WithEvents lbAn_dtaper As NTSInformatica.NTSLabel
  Public WithEvents edAn_dtaper As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_agcontrop As NTSInformatica.NTSLabel
  Public WithEvents edAn_agcontrop As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_contfatt As NTSInformatica.NTSLabel
  Public WithEvents edAn_contfatt As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAn_intragr As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_contfatt As NTSInformatica.NTSLabel
  Public WithEvents lbXx_zona As NTSInformatica.NTSLabel
  Public WithEvents edAn_zona As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_zona As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codbanc As NTSInformatica.NTSLabel
  Public WithEvents edAn_codbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codbanc As NTSInformatica.NTSLabel
  Public WithEvents cbAn_fatt As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_fatt As NTSInformatica.NTSLabel
  Public WithEvents fmRiclassificati As NTSInformatica.NTSGroupBox
  Public WithEvents cmdRiclassificazioni As NTSInformatica.NTSButton
  Public WithEvents lbRiclassif As NTSInformatica.NTSLabel
  Public WithEvents lbCee As NTSInformatica.NTSLabel
  Public WithEvents edAn_kpccee As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_kpccee2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_kpccee2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_kpccee As NTSInformatica.NTSLabel
  Public WithEvents lbAn_rifrica As NTSInformatica.NTSLabel
  Public WithEvents edAn_rifrica As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_rifricd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_rifricd As NTSInformatica.NTSLabel
  Public WithEvents ckAn_partite As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_scaden As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_controp As NTSInformatica.NTSLabel
  Public WithEvents lbAn_contropLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_controp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLead As NTSInformatica.NTSLabel
  Public WithEvents lbAnagen As NTSInformatica.NTSLabel
  Public WithEvents tlbApriLead As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbXx_listino As NTSInformatica.NTSLabel
  Public WithEvents edAn_listino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_listino As NTSInformatica.NTSLabel
  Public WithEvents pnDaticondSx As NTSInformatica.NTSPanel
  Public WithEvents fmPagamento As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_codpag As NTSInformatica.NTSLabel
  Public WithEvents edAn_giofiss As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAn_codpag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_giofiss As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codpag As NTSInformatica.NTSLabel
  Public WithEvents edAn_mesees2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_mesees As NTSInformatica.NTSLabel
  Public WithEvents edAn_mesees1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_colbil As NTSInformatica.NTSLabel
  Public WithEvents cbAn_colbil As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_codnscol As NTSInformatica.NTSLabel
  Public WithEvents edAn_codnscol As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_rifriba As NTSInformatica.NTSLabel
  Public WithEvents edAn_rifriba As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_banc2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_banc1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_cab As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAn_abi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_cab As NTSInformatica.NTSLabel
  Public WithEvents lbAn_abi As NTSInformatica.NTSLabel
  Public WithEvents lbAn_fido As NTSInformatica.NTSLabel
  Public WithEvents edAn_fido As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_vett2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_vett2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_vett2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vett As NTSInformatica.NTSLabel
  Public WithEvents edAn_vett As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_vett As NTSInformatica.NTSLabel
  Public WithEvents lbXx_porto As NTSInformatica.NTSLabel
  Public WithEvents edAn_porto As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_porto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents edAn_codtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents pnCondFornsx As NTSInformatica.NTSPanel
  Public WithEvents cbAn_blocco As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_blocco As NTSInformatica.NTSLabel
  Public WithEvents cbAn_status As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_status As NTSInformatica.NTSLabel
  Public WithEvents cbAn_gcons As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_gcons As NTSInformatica.NTSLabel
  Public WithEvents cbAn_perfatt As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_perfatt As NTSInformatica.NTSLabel
  Public WithEvents ckAn_bolli As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_spinc As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_vuoti As NTSInformatica.NTSCheckBox
  Public WithEvents fmDichIntento As NTSInformatica.NTSGroupBox
  Public WithEvents pnExportSx As NTSInformatica.NTSPanel
  Public WithEvents lbAn_codese As NTSInformatica.NTSLabel
  Public WithEvents edAn_maxdic As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_maxdic As NTSInformatica.NTSLabel
  Public WithEvents lbAn_numdic As NTSInformatica.NTSLabel
  Public WithEvents lbAn_scaddic As NTSInformatica.NTSLabel
  Public WithEvents edAn_scaddic As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_datdic As NTSInformatica.NTSLabel
  Public WithEvents edAn_datdic As NTSInformatica.NTSTextBoxData
  Public WithEvents lbXx_codese As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codntra As NTSInformatica.NTSLabel
  Public WithEvents edAn_codese As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAn_codntra As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codntra As NTSInformatica.NTSLabel
  Public WithEvents lbAn_datdicp As NTSInformatica.NTSLabel
  Public WithEvents edAn_datdicp As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_numdicp As NTSInformatica.NTSLabel
  Public WithEvents fmConai As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_perescon As NTSInformatica.NTSLabel
  Public WithEvents edAn_perescon As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAn_gescon As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_gescon As NTSInformatica.NTSLabel
  Public WithEvents lbAn_valuta As NTSInformatica.NTSLabel
  Public WithEvents edAn_valuta As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_valuta As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codrtac As NTSInformatica.NTSLabel
  Public WithEvents edAn_codrtac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codrtac As NTSInformatica.NTSLabel
  Public WithEvents pnRiclassificazioni As NTSInformatica.NTSPanel
  Public WithEvents cbAn_privacy As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_privacy As NTSInformatica.NTSLabel
  Public WithEvents pnCondForndx As NTSInformatica.NTSPanel
  Public WithEvents edAn_cin As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_iban As NTSInformatica.NTSLabel
  Public WithEvents edAn_iban As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmIbanitalia As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_prefiban As NTSInformatica.NTSLabel
  Public WithEvents edAn_prefiban As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsBarMenuItem3 As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbTipoBf As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAltreBanche As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbOrganizzazione As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbQualità As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbNote As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEstensioni As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbMovimenti As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbOrdini As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbOle As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsTabPage8 As NTSInformatica.NTSTabPage
  Public WithEvents pnListini As NTSInformatica.NTSPanel
  Public WithEvents ceListini As NTSInformatica.NTSXXLIST
  Public WithEvents NtsTabPage9 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage10 As NTSInformatica.NTSTabPage
  Public WithEvents pnSconti As NTSInformatica.NTSPanel
  Public WithEvents pnProvvigioni As NTSInformatica.NTSPanel
  Public WithEvents ceProvvig As NTSInformatica.NTSXXPROV
  Public WithEvents ceSconti As NTSInformatica.NTSXXSCON
  Public WithEvents tlbWeb As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbMail As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbGoogleMaps As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEmail As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGestioneSconti As NTSInformatica.NTSBarMenuItem
  Public WithEvents edAn_numdic As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_numdicp As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFocus As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbAn_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbXx_coddica As NTSInformatica.NTSLabel
  Public WithEvents lbAn_coddica As NTSInformatica.NTSLabel
  Public WithEvents fmCadc As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents edAn_codtcdc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents edAn_coddicv As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_coddica As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmTesoreria As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_trating As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codvfde As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codvfde As NTSInformatica.NTSLabel
  Public WithEvents cbAn_trating As NTSInformatica.NTSComboBox
  Public WithEvents edAn_codvfde As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbAn_privato As NTSInformatica.NTSComboBox
  Public WithEvents lbBarra As NTSInformatica.NTSLabel
  Public WithEvents tlbSottotipiPagamento As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbTpbfPerDocu As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmPosition As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_latitud As NTSInformatica.NTSLabel
  Public WithEvents edAn_latitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_longitud As NTSInformatica.NTSLabel
  Public WithEvents edAn_longitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbOption As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbApriUltimaRicerca As NTSInformatica.NTSBarMenuItem
  Public WithEvents ceColl As NTSInformatica.NTSXXCOLL
  Public WithEvents tlbSimula As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAn_swift As NTSInformatica.NTSLabel
  Public WithEvents edAn_swift As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_dtmandrid As NTSInformatica.NTSTextBoxData
  Public WithEvents cbAn_tiporid As NTSInformatica.NTSComboBox
  Public WithEvents lbRid As NTSInformatica.NTSLabel
  Public WithEvents lbAn_idmandrid As NTSInformatica.NTSLabel
  Public WithEvents edAn_idmandrid As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbSkype As NTSInformatica.NTSBarButtonItem
  Public WithEvents ckAn_webvis As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_codpaga2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpaga2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codpaga2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codpaga3 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpaga3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codpaga3 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpagscagl1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codpagscagl1 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpagscagl2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_codpagscagl2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_coduffpa As NTSInformatica.NTSLabel
  Public WithEvents edAn_coduffpa As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbContratti As NTSInformatica.NTSBarButtonItem
  Public WithEvents edAn_paepag As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_paepag As NTSInformatica.NTSLabel
  Public WithEvents lbAn_acuradi As NTSInformatica.NTSLabel
  Public WithEvents cbAn_acuradi As NTSInformatica.NTSComboBox
  Public WithEvents cmdConaiArt As NTSInformatica.NTSButton
#End Region



  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity
      Select Case e.TipoEvento
        Case "AggiornaColoreAbiCab" : ColoraCampoAbiCab()
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CLIE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbOption = New NTSInformatica.NTSBarSubItem
    Me.tlbApriUltimaRicerca = New NTSInformatica.NTSBarMenuItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarMenuItem
    Me.tlbTipoBf = New NTSInformatica.NTSBarMenuItem
    Me.tlbTpbfPerDocu = New NTSInformatica.NTSBarButtonItem
    Me.tlbAltreBanche = New NTSInformatica.NTSBarMenuItem
    Me.tlbSottotipiPagamento = New NTSInformatica.NTSBarButtonItem
    Me.tlbQualità = New NTSInformatica.NTSBarMenuItem
    Me.tlbApriLead = New NTSInformatica.NTSBarMenuItem
    Me.tlbMovimenti = New NTSInformatica.NTSBarMenuItem
    Me.tlbOrdini = New NTSInformatica.NTSBarMenuItem
    Me.tlbNote = New NTSInformatica.NTSBarMenuItem
    Me.tlbCalcolaCodFisc = New NTSInformatica.NTSBarMenuItem
    Me.tlbRitornaCodFisc = New NTSInformatica.NTSBarMenuItem
    Me.tlbWeb = New NTSInformatica.NTSBarMenuItem
    Me.tlbMail = New NTSInformatica.NTSBarMenuItem
    Me.tlbEmail = New NTSInformatica.NTSBarButtonItem
    Me.tlbSkype = New NTSInformatica.NTSBarButtonItem
    Me.tlbGoogleMaps = New NTSInformatica.NTSBarButtonItem
    Me.tlbEstensioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbGestioneSconti = New NTSInformatica.NTSBarMenuItem
    Me.tlbSimula = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrganizzazione = New NTSInformatica.NTSBarButtonItem
    Me.tlbContratti = New NTSInformatica.NTSBarButtonItem
    Me.tlbOle = New NTSInformatica.NTSBarButtonItem
    Me.tlbCli = New NTSInformatica.NTSBarButtonItem
    Me.tlbForn = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.NtsBarMenuItem3 = New NTSInformatica.NTSBarMenuItem
    Me.tsClie = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.pnPag1Dx = New NTSInformatica.NTSPanel
    Me.lbAn_cell = New NTSInformatica.NTSLabel
    Me.edAn_cell = New NTSInformatica.NTSTextBoxStr
    Me.ckAn_omocodice = New NTSInformatica.NTSCheckBox
    Me.cbAn_usaem = New NTSInformatica.NTSComboBox
    Me.lbAn_usaem = New NTSInformatica.NTSLabel
    Me.edAn_email = New NTSInformatica.NTSTextBoxStr
    Me.edAn_pariva = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_faxtlx = New NTSInformatica.NTSLabel
    Me.lbAn_email = New NTSInformatica.NTSLabel
    Me.edAn_faxtlx = New NTSInformatica.NTSTextBoxStr
    Me.edAn_telef = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_pariva = New NTSInformatica.NTSLabel
    Me.edAn_codfis = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codfis = New NTSInformatica.NTSLabel
    Me.lbAn_telef = New NTSInformatica.NTSLabel
    Me.pnPag1Sx = New NTSInformatica.NTSPanel
    Me.lbAn_tpsogiva = New NTSInformatica.NTSLabel
    Me.lbAn_statofed = New NTSInformatica.NTSLabel
    Me.edAn_statofed = New NTSInformatica.NTSTextBoxStr
    Me.cbAn_tpsogiva = New NTSInformatica.NTSComboBox
    Me.lbXx_stato = New NTSInformatica.NTSLabel
    Me.lbXx_codcomu = New NTSInformatica.NTSLabel
    Me.lbAn_codcomu = New NTSInformatica.NTSLabel
    Me.edAn_codcomu = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_citta = New NTSInformatica.NTSLabel
    Me.edAn_citta = New NTSInformatica.NTSTextBoxStr
    Me.edAn_indir = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_stato = New NTSInformatica.NTSLabel
    Me.lbAn_cap = New NTSInformatica.NTSLabel
    Me.edAn_stato = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_indir = New NTSInformatica.NTSLabel
    Me.edAn_cap = New NTSInformatica.NTSTextBoxStr
    Me.edAn_prov = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_prov = New NTSInformatica.NTSLabel
    Me.pnPag1Bottom = New NTSInformatica.NTSPanel
    Me.fmIndirizzi = New NTSInformatica.NTSGroupBox
    Me.pnIndirDx = New NTSInformatica.NTSPanel
    Me.cmdAltriIndir = New NTSInformatica.NTSButton
    Me.lbAn_destpag = New NTSInformatica.NTSLabel
    Me.lbXx_destpag = New NTSInformatica.NTSLabel
    Me.lbAn_destin = New NTSInformatica.NTSLabel
    Me.lbXx_destin = New NTSInformatica.NTSLabel
    Me.edAn_destpag = New NTSInformatica.NTSTextBoxNum
    Me.edAn_destin = New NTSInformatica.NTSTextBoxNum
    Me.pnIndirSx = New NTSInformatica.NTSPanel
    Me.ckDestresan = New NTSInformatica.NTSCheckBox
    Me.ckDestcorr = New NTSInformatica.NTSCheckBox
    Me.ckDestsedel = New NTSInformatica.NTSCheckBox
    Me.ckDestdomf = New NTSInformatica.NTSCheckBox
    Me.cmdDestcorr = New NTSInformatica.NTSButton
    Me.cmdDestresan = New NTSInformatica.NTSButton
    Me.cmdDestsedel = New NTSInformatica.NTSButton
    Me.cmdDestdomf = New NTSInformatica.NTSButton
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPag2 = New NTSInformatica.NTSPanel
    Me.fmPersfisica = New NTSInformatica.NTSGroupBox
    Me.edAn_titolo = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_titolo = New NTSInformatica.NTSLabel
    Me.lbAn_sesso = New NTSInformatica.NTSLabel
    Me.edAn_cognome = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_cognome = New NTSInformatica.NTSLabel
    Me.edAn_nome = New NTSInformatica.NTSTextBoxStr
    Me.cbAn_sesso = New NTSInformatica.NTSComboBox
    Me.lbAn_nome = New NTSInformatica.NTSLabel
    Me.fmNonresidenti = New NTSInformatica.NTSGroupBox
    Me.edAn_estcodiso = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_nazion1 = New NTSInformatica.NTSLabel
    Me.edAn_estpariva = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_nazion2 = New NTSInformatica.NTSLabel
    Me.lbAn_estpariva = New NTSInformatica.NTSLabel
    Me.lbAn_estcodiso = New NTSInformatica.NTSLabel
    Me.edAn_codfisest = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codfisest = New NTSInformatica.NTSLabel
    Me.lbAn_nazion1 = New NTSInformatica.NTSLabel
    Me.edAn_nazion2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_nazion2 = New NTSInformatica.NTSLabel
    Me.edAn_nazion1 = New NTSInformatica.NTSTextBoxStr
    Me.fmNascita = New NTSInformatica.NTSGroupBox
    Me.lbXx_stanasc = New NTSInformatica.NTSLabel
    Me.lbXx_codcomn = New NTSInformatica.NTSLabel
    Me.lbAn_datnasc = New NTSInformatica.NTSLabel
    Me.edAn_datnasc = New NTSInformatica.NTSTextBoxData
    Me.lbAn_pronasc = New NTSInformatica.NTSLabel
    Me.lbAn_citnasc = New NTSInformatica.NTSLabel
    Me.edAn_pronasc = New NTSInformatica.NTSTextBoxStr
    Me.edAn_citnasc = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codcomn = New NTSInformatica.NTSLabel
    Me.edAn_codcomn = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_stanasc = New NTSInformatica.NTSLabel
    Me.edAn_stanasc = New NTSInformatica.NTSTextBoxStr
    Me.ckAn_condom = New NTSInformatica.NTSCheckBox
    Me.ckAn_soggresi = New NTSInformatica.NTSCheckBox
    Me.ckAn_profes = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPag3 = New NTSInformatica.NTSPanel
    Me.fmPosition = New NTSInformatica.NTSGroupBox
    Me.lbAn_latitud = New NTSInformatica.NTSLabel
    Me.edAn_latitud = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_longitud = New NTSInformatica.NTSLabel
    Me.edAn_longitud = New NTSInformatica.NTSTextBoxStr
    Me.fmConai = New NTSInformatica.NTSGroupBox
    Me.cmdConaiArt = New NTSInformatica.NTSButton
    Me.lbAn_perescon = New NTSInformatica.NTSLabel
    Me.edAn_perescon = New NTSInformatica.NTSTextBoxNum
    Me.cbAn_gescon = New NTSInformatica.NTSComboBox
    Me.lbAn_gescon = New NTSInformatica.NTSLabel
    Me.fmAcquisizione = New NTSInformatica.NTSGroupBox
    Me.edAn_contatt = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_contatt = New NTSInformatica.NTSLabel
    Me.lbAn_dtaper = New NTSInformatica.NTSLabel
    Me.edAn_dtaper = New NTSInformatica.NTSTextBoxData
    Me.fmWeb = New NTSInformatica.NTSGroupBox
    Me.lbAn_webpwd = New NTSInformatica.NTSLabel
    Me.edAn_webpwd = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_website = New NTSInformatica.NTSLabel
    Me.edAn_website = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_webuid = New NTSInformatica.NTSLabel
    Me.edAn_webuid = New NTSInformatica.NTSTextBoxStr
    Me.pnDatiSx = New NTSInformatica.NTSPanel
    Me.fmTesoreria = New NTSInformatica.NTSGroupBox
    Me.lbXx_codvfde = New NTSInformatica.NTSLabel
    Me.cbAn_trating = New NTSInformatica.NTSComboBox
    Me.edAn_codvfde = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codvfde = New NTSInformatica.NTSLabel
    Me.lbAn_trating = New NTSInformatica.NTSLabel
    Me.edAn_rating = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_rating = New NTSInformatica.NTSLabel
    Me.lbAn_agcontrop = New NTSInformatica.NTSLabel
    Me.lbXx_zona = New NTSInformatica.NTSLabel
    Me.edAn_agcontrop = New NTSInformatica.NTSTextBoxNum
    Me.edAn_zona = New NTSInformatica.NTSTextBoxNum
    Me.edAn_codling = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_zona = New NTSInformatica.NTSLabel
    Me.lbAn_codling = New NTSInformatica.NTSLabel
    Me.lbXx_categ = New NTSInformatica.NTSLabel
    Me.lbXx_codling = New NTSInformatica.NTSLabel
    Me.edAn_categ = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_categ = New NTSInformatica.NTSLabel
    Me.lbXx_agente = New NTSInformatica.NTSLabel
    Me.edAn_agente = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_agente = New NTSInformatica.NTSLabel
    Me.lbAn_clascon = New NTSInformatica.NTSLabel
    Me.lbXx_codcana = New NTSInformatica.NTSLabel
    Me.edAn_clascon = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_clascon = New NTSInformatica.NTSLabel
    Me.lbXx_claprov = New NTSInformatica.NTSLabel
    Me.edAn_codcana = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_claprov = New NTSInformatica.NTSLabel
    Me.edAn_claprov = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_codcana = New NTSInformatica.NTSLabel
    Me.lbXx_agente2 = New NTSInformatica.NTSLabel
    Me.edAn_agente2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_agente2 = New NTSInformatica.NTSLabel
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnDatiContabili = New NTSInformatica.NTSPanel
    Me.fmCadc = New NTSInformatica.NTSGroupBox
    Me.edAn_coddicv = New NTSInformatica.NTSTextBoxStr
    Me.edAn_coddica = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codtcdc = New NTSInformatica.NTSLabel
    Me.edAn_codtcdc = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codtcdc = New NTSInformatica.NTSLabel
    Me.lbXx_coddicv = New NTSInformatica.NTSLabel
    Me.lbAn_coddica = New NTSInformatica.NTSLabel
    Me.lbAn_coddicv = New NTSInformatica.NTSLabel
    Me.lbXx_coddica = New NTSInformatica.NTSLabel
    Me.cbAn_privacy = New NTSInformatica.NTSComboBox
    Me.lbAn_privacy = New NTSInformatica.NTSLabel
    Me.lbAn_valuta = New NTSInformatica.NTSLabel
    Me.edAn_valuta = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_valuta = New NTSInformatica.NTSLabel
    Me.lbXx_codrtac = New NTSInformatica.NTSLabel
    Me.edAn_codrtac = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_codrtac = New NTSInformatica.NTSLabel
    Me.pnDaticondSx = New NTSInformatica.NTSPanel
    Me.ckAn_scaden = New NTSInformatica.NTSCheckBox
    Me.ckAn_partite = New NTSInformatica.NTSCheckBox
    Me.fmPagamento = New NTSInformatica.NTSGroupBox
    Me.edAn_codpagscagl2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_codpagscagl2 = New NTSInformatica.NTSLabel
    Me.edAn_codpagscagl1 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_codpagscagl1 = New NTSInformatica.NTSLabel
    Me.lbAn_codpaga3 = New NTSInformatica.NTSLabel
    Me.edAn_codpaga3 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpaga3 = New NTSInformatica.NTSLabel
    Me.lbAn_codpaga2 = New NTSInformatica.NTSLabel
    Me.edAn_codpaga2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpaga2 = New NTSInformatica.NTSLabel
    Me.lbAn_codpag = New NTSInformatica.NTSLabel
    Me.edAn_giofiss = New NTSInformatica.NTSTextBoxNum
    Me.edAn_codpag = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_giofiss = New NTSInformatica.NTSLabel
    Me.lbXx_codpag = New NTSInformatica.NTSLabel
    Me.edAn_mesees2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_mesees = New NTSInformatica.NTSLabel
    Me.edAn_mesees1 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_colbil = New NTSInformatica.NTSLabel
    Me.cbAn_colbil = New NTSInformatica.NTSComboBox
    Me.ckAn_intragr = New NTSInformatica.NTSCheckBox
    Me.lbAn_codnscol = New NTSInformatica.NTSLabel
    Me.edAn_codnscol = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbAn_contropLabel = New NTSInformatica.NTSLabel
    Me.edAn_controp = New NTSInformatica.NTSTextBoxNum
    Me.fmRiclassificati = New NTSInformatica.NTSGroupBox
    Me.pnRiclassificazioni = New NTSInformatica.NTSPanel
    Me.lbAn_kpccee = New NTSInformatica.NTSLabel
    Me.cmdRiclassificazioni = New NTSInformatica.NTSButton
    Me.lbAn_rifricd = New NTSInformatica.NTSLabel
    Me.lbRiclassif = New NTSInformatica.NTSLabel
    Me.edAn_rifricd = New NTSInformatica.NTSTextBoxStr
    Me.lbCee = New NTSInformatica.NTSLabel
    Me.edAn_rifrica = New NTSInformatica.NTSTextBoxStr
    Me.edAn_kpccee = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_rifrica = New NTSInformatica.NTSLabel
    Me.edAn_kpccee2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_kpccee2 = New NTSInformatica.NTSLabel
    Me.lbXx_contfatt = New NTSInformatica.NTSLabel
    Me.edAn_contfatt = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_contfatt = New NTSInformatica.NTSLabel
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnFornitura = New NTSInformatica.NTSPanel
    Me.lbAn_coduffpa = New NTSInformatica.NTSLabel
    Me.edAn_coduffpa = New NTSInformatica.NTSTextBoxStr
    Me.ckAn_webvis = New NTSInformatica.NTSCheckBox
    Me.lbAn_idmandrid = New NTSInformatica.NTSLabel
    Me.edAn_idmandrid = New NTSInformatica.NTSTextBoxStr
    Me.edAn_dtmandrid = New NTSInformatica.NTSTextBoxData
    Me.cbAn_tiporid = New NTSInformatica.NTSComboBox
    Me.lbRid = New NTSInformatica.NTSLabel
    Me.pnCondForndx = New NTSInformatica.NTSPanel
    Me.lbAn_fatt = New NTSInformatica.NTSLabel
    Me.cbAn_blocco = New NTSInformatica.NTSComboBox
    Me.cbAn_fatt = New NTSInformatica.NTSComboBox
    Me.lbAn_blocco = New NTSInformatica.NTSLabel
    Me.lbAn_perfatt = New NTSInformatica.NTSLabel
    Me.cbAn_status = New NTSInformatica.NTSComboBox
    Me.cbAn_perfatt = New NTSInformatica.NTSComboBox
    Me.lbAn_status = New NTSInformatica.NTSLabel
    Me.lbAn_gcons = New NTSInformatica.NTSLabel
    Me.cbAn_gcons = New NTSInformatica.NTSComboBox
    Me.lbAn_iban = New NTSInformatica.NTSLabel
    Me.edAn_iban = New NTSInformatica.NTSTextBoxStr
    Me.ckAn_bolli = New NTSInformatica.NTSCheckBox
    Me.ckAn_spinc = New NTSInformatica.NTSCheckBox
    Me.ckAn_vuoti = New NTSInformatica.NTSCheckBox
    Me.pnCondFornsx = New NTSInformatica.NTSPanel
    Me.lbAn_acuradi = New NTSInformatica.NTSLabel
    Me.cbAn_acuradi = New NTSInformatica.NTSComboBox
    Me.fmIbanitalia = New NTSInformatica.NTSGroupBox
    Me.lbAn_swift = New NTSInformatica.NTSLabel
    Me.edAn_swift = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_abi = New NTSInformatica.NTSLabel
    Me.lbAn_rifriba = New NTSInformatica.NTSLabel
    Me.lbAn_prefiban = New NTSInformatica.NTSLabel
    Me.edAn_rifriba = New NTSInformatica.NTSTextBoxStr
    Me.edAn_prefiban = New NTSInformatica.NTSTextBoxStr
    Me.edAn_banc2 = New NTSInformatica.NTSTextBoxStr
    Me.edAn_cin = New NTSInformatica.NTSTextBoxStr
    Me.edAn_banc1 = New NTSInformatica.NTSTextBoxStr
    Me.edAn_cab = New NTSInformatica.NTSTextBoxNum
    Me.edAn_abi = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_cab = New NTSInformatica.NTSLabel
    Me.lbXx_porto = New NTSInformatica.NTSLabel
    Me.edAn_codbanc = New NTSInformatica.NTSTextBoxNum
    Me.edAn_porto = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_porto = New NTSInformatica.NTSLabel
    Me.lbAn_codbanc = New NTSInformatica.NTSLabel
    Me.lbAn_codtpbf = New NTSInformatica.NTSLabel
    Me.edAn_codtpbf = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_vett2 = New NTSInformatica.NTSLabel
    Me.lbXx_codbanc = New NTSInformatica.NTSLabel
    Me.lbXx_codtpbf = New NTSInformatica.NTSLabel
    Me.edAn_vett2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_vett2 = New NTSInformatica.NTSLabel
    Me.lbAn_listino = New NTSInformatica.NTSLabel
    Me.lbXx_vett = New NTSInformatica.NTSLabel
    Me.edAn_vett = New NTSInformatica.NTSTextBoxNum
    Me.edAn_listino = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_vett = New NTSInformatica.NTSLabel
    Me.lbXx_listino = New NTSInformatica.NTSLabel
    Me.edAn_fido = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_fido = New NTSInformatica.NTSLabel
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnExport = New NTSInformatica.NTSPanel
    Me.pnExportSx = New NTSInformatica.NTSPanel
    Me.edAn_paepag = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_paepag = New NTSInformatica.NTSLabel
    Me.lbAn_codese = New NTSInformatica.NTSLabel
    Me.fmDichIntento = New NTSInformatica.NTSGroupBox
    Me.edAn_numdicp = New NTSInformatica.NTSTextBoxStr
    Me.edAn_numdic = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_numdicp = New NTSInformatica.NTSLabel
    Me.lbAn_datdicp = New NTSInformatica.NTSLabel
    Me.edAn_datdicp = New NTSInformatica.NTSTextBoxData
    Me.edAn_maxdic = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_maxdic = New NTSInformatica.NTSLabel
    Me.lbAn_numdic = New NTSInformatica.NTSLabel
    Me.lbAn_scaddic = New NTSInformatica.NTSLabel
    Me.edAn_scaddic = New NTSInformatica.NTSTextBoxData
    Me.lbAn_datdic = New NTSInformatica.NTSLabel
    Me.edAn_datdic = New NTSInformatica.NTSTextBoxData
    Me.lbXx_codese = New NTSInformatica.NTSLabel
    Me.lbAn_codntra = New NTSInformatica.NTSLabel
    Me.edAn_codese = New NTSInformatica.NTSTextBoxNum
    Me.edAn_codntra = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codntra = New NTSInformatica.NTSLabel
    Me.NtsTabPage7 = New NTSInformatica.NTSTabPage
    Me.pnNote = New NTSInformatica.NTSPanel
    Me.lbAn_note = New NTSInformatica.NTSLabel
    Me.edAn_note = New NTSInformatica.NTSTextBoxStr
    Me.edAn_note2 = New NTSInformatica.NTSMemoBox
    Me.lbAn_note2 = New NTSInformatica.NTSLabel
    Me.NtsTabPage8 = New NTSInformatica.NTSTabPage
    Me.pnListini = New NTSInformatica.NTSPanel
    Me.ceListini = New NTSInformatica.NTSXXLIST
    Me.NtsTabPage9 = New NTSInformatica.NTSTabPage
    Me.pnSconti = New NTSInformatica.NTSPanel
    Me.ceSconti = New NTSInformatica.NTSXXSCON
    Me.NtsTabPage10 = New NTSInformatica.NTSTabPage
    Me.pnProvvigioni = New NTSInformatica.NTSPanel
    Me.ceProvvig = New NTSInformatica.NTSXXPROV
    Me.lbAn_conto = New NTSInformatica.NTSLabel
    Me.edAn_conto = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_descr1 = New NTSInformatica.NTSLabel
    Me.edAn_descr1 = New NTSInformatica.NTSTextBoxStr
    Me.edAn_descr2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_persfg = New NTSInformatica.NTSLabel
    Me.cbAn_persfg = New NTSInformatica.NTSComboBox
    Me.lbAn_siglaric = New NTSInformatica.NTSLabel
    Me.edAn_siglaric = New NTSInformatica.NTSTextBoxStr
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.ceColl = New NTSInformatica.NTSXXCOLL
    Me.lbBarra = New NTSInformatica.NTSLabel
    Me.cbAn_privato = New NTSInformatica.NTSComboBox
    Me.lbLead = New NTSInformatica.NTSLabel
    Me.lbAnagen = New NTSInformatica.NTSLabel
    Me.cmdPartitario = New NTSInformatica.NTSButton
    Me.lbXx_codmast = New NTSInformatica.NTSLabel
    Me.lbMastroLabel = New NTSInformatica.NTSLabel
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.edFocus = New NTSInformatica.NTSTextBoxStr
    Me.lbDeteriorabili = New NTSInformatica.NTSLabel
    Me.edAn_codpagadet3 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpagadet3 = New NTSInformatica.NTSLabel
    Me.edAn_codpagadet2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpagadet2 = New NTSInformatica.NTSLabel
    Me.edAn_codpagadet = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpagadet = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsClie, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsClie.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.pnPag1Dx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Dx.SuspendLayout()
    CType(Me.edAn_cell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_omocodice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_usaem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_pariva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_faxtlx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_telef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codfis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag1Sx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Sx.SuspendLayout()
    CType(Me.edAn_statofed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_tpsogiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codcomu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_citta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_indir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_stato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_cap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_prov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag1Bottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Bottom.SuspendLayout()
    CType(Me.fmIndirizzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIndirizzi.SuspendLayout()
    CType(Me.pnIndirDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnIndirDx.SuspendLayout()
    CType(Me.edAn_destpag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_destin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnIndirSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnIndirSx.SuspendLayout()
    CType(Me.ckDestresan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDestcorr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDestsedel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDestdomf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2.SuspendLayout()
    CType(Me.fmPersfisica, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPersfisica.SuspendLayout()
    CType(Me.edAn_titolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_cognome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_nome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_sesso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmNonresidenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNonresidenti.SuspendLayout()
    CType(Me.edAn_estcodiso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_estpariva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codfisest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_nazion2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_nazion1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmNascita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNascita.SuspendLayout()
    CType(Me.edAn_datnasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_pronasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_citnasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codcomn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_stanasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_condom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_soggresi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_profes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag3.SuspendLayout()
    CType(Me.fmPosition, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPosition.SuspendLayout()
    CType(Me.edAn_latitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_longitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmConai, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmConai.SuspendLayout()
    CType(Me.edAn_perescon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_gescon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAcquisizione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAcquisizione.SuspendLayout()
    CType(Me.edAn_contatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_dtaper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmWeb.SuspendLayout()
    CType(Me.edAn_webpwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_website.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_webuid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDatiSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDatiSx.SuspendLayout()
    CType(Me.fmTesoreria, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTesoreria.SuspendLayout()
    CType(Me.cbAn_trating.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codvfde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rating.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_agcontrop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_zona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codling.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_categ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_agente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_clascon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codcana.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_claprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_agente2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnDatiContabili, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDatiContabili.SuspendLayout()
    CType(Me.fmCadc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCadc.SuspendLayout()
    CType(Me.edAn_coddicv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_coddica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codtcdc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_privacy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_valuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codrtac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDaticondSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDaticondSx.SuspendLayout()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPagamento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPagamento.SuspendLayout()
    CType(Me.edAn_codpagscagl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpagscagl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpaga3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpaga2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_giofiss.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_mesees2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_mesees1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_intragr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codnscol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRiclassificati.SuspendLayout()
    CType(Me.pnRiclassificazioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRiclassificazioni.SuspendLayout()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_contfatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnFornitura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFornitura.SuspendLayout()
    CType(Me.edAn_coduffpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_webvis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_idmandrid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_dtmandrid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_tiporid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCondForndx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCondForndx.SuspendLayout()
    CType(Me.cbAn_blocco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_fatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_perfatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_gcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_iban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_bolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_spinc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_vuoti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCondFornsx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCondFornsx.SuspendLayout()
    CType(Me.cbAn_acuradi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIbanitalia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIbanitalia.SuspendLayout()
    CType(Me.edAn_swift.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifriba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_prefiban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_banc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_cin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_banc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_cab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_abi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_porto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_vett2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_vett.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_listino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_fido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnExport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnExport.SuspendLayout()
    CType(Me.pnExportSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnExportSx.SuspendLayout()
    CType(Me.edAn_paepag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDichIntento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDichIntento.SuspendLayout()
    CType(Me.edAn_numdicp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_numdic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_datdicp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_maxdic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_scaddic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_datdic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codese.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codntra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage7.SuspendLayout()
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNote.SuspendLayout()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage8.SuspendLayout()
    CType(Me.pnListini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnListini.SuspendLayout()
    Me.NtsTabPage9.SuspendLayout()
    CType(Me.pnSconti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSconti.SuspendLayout()
    Me.NtsTabPage10.SuspendLayout()
    CType(Me.pnProvvigioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProvvigioni.SuspendLayout()
    CType(Me.edAn_conto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_descr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_persfg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_siglaric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.cbAn_privato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpagadet3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpagadet2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codpagadet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbApri, Me.tlbDuplica, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbCalcolaCodFisc, Me.tlbRitornaCodFisc, Me.tlbCli, Me.tlbForn, Me.tlbCambioDitta, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbApriLead, Me.tlbMovimenti, Me.tlbOrdini, Me.NtsBarMenuItem3, Me.tlbTipoBf, Me.tlbAltreBanche, Me.tlbOrganizzazione, Me.tlbEstensioni, Me.tlbNote, Me.tlbQualità, Me.tlbOle, Me.tlbWeb, Me.tlbMail, Me.tlbGoogleMaps, Me.tlbEmail, Me.tlbGestioneSconti, Me.tlbSottotipiPagamento, Me.tlbTpbfPerDocu, Me.tlbOption, Me.tlbApriUltimaRicerca, Me.tlbSimula, Me.tlbSkype, Me.tlbContratti})
    Me.NtsBarManager1.MaxItemId = 60
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOption), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrganizzazione), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbContratti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOle), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCli, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbForn), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 6
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 5
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbOption
    '
    Me.tlbOption.GlyphPath = ""
    Me.tlbOption.Id = 55
    Me.tlbOption.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriUltimaRicerca)})
    Me.tlbOption.Name = "tlbOption"
    Me.tlbOption.Visible = True
    '
    'tlbApriUltimaRicerca
    '
    Me.tlbApriUltimaRicerca.Caption = "Apri da ultima ricerca"
    Me.tlbApriUltimaRicerca.GlyphPath = ""
    Me.tlbApriUltimaRicerca.Id = 56
    Me.tlbApriUltimaRicerca.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F3))
    Me.tlbApriUltimaRicerca.Name = "tlbApriUltimaRicerca"
    Me.tlbApriUltimaRicerca.NTSIsCheckBox = False
    Me.tlbApriUltimaRicerca.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 31
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 32
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 33
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 34
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTipoBf, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTpbfPerDocu), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAltreBanche), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSottotipiPagamento), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbQualità), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriLead, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMovimenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNote, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCalcolaCodFisc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRitornaCodFisc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbWeb, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMail), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEmail), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSkype), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGoogleMaps), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEstensioni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGestioneSconti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSimula, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambio ditta"
    Me.tlbCambioDitta.GlyphPath = ""
    Me.tlbCambioDitta.Id = 30
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.NTSIsCheckBox = False
    Me.tlbCambioDitta.Visible = True
    '
    'tlbTipoBf
    '
    Me.tlbTipoBf.Caption = "Condizioni per tipo B/F"
    Me.tlbTipoBf.GlyphPath = ""
    Me.tlbTipoBf.Id = 40
    Me.tlbTipoBf.Name = "tlbTipoBf"
    Me.tlbTipoBf.NTSIsCheckBox = False
    Me.tlbTipoBf.Visible = True
    '
    'tlbTpbfPerDocu
    '
    Me.tlbTpbfPerDocu.Caption = "Tipi B\F per tipo documento"
    Me.tlbTpbfPerDocu.GlyphPath = ""
    Me.tlbTpbfPerDocu.Id = 54
    Me.tlbTpbfPerDocu.Name = "tlbTpbfPerDocu"
    Me.tlbTpbfPerDocu.Visible = True
    '
    'tlbAltreBanche
    '
    Me.tlbAltreBanche.Caption = "Altre banche"
    Me.tlbAltreBanche.GlyphPath = ""
    Me.tlbAltreBanche.Id = 41
    Me.tlbAltreBanche.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B))
    Me.tlbAltreBanche.Name = "tlbAltreBanche"
    Me.tlbAltreBanche.NTSIsCheckBox = False
    Me.tlbAltreBanche.Visible = True
    '
    'tlbSottotipiPagamento
    '
    Me.tlbSottotipiPagamento.Caption = "Sottotipi pagamento cli\for"
    Me.tlbSottotipiPagamento.GlyphPath = ""
    Me.tlbSottotipiPagamento.Id = 53
    Me.tlbSottotipiPagamento.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbSottotipiPagamento.Name = "tlbSottotipiPagamento"
    Me.tlbSottotipiPagamento.Visible = True
    '
    'tlbQualità
    '
    Me.tlbQualità.Caption = "Controlli qualità"
    Me.tlbQualità.GlyphPath = ""
    Me.tlbQualità.Id = 45
    Me.tlbQualità.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
    Me.tlbQualità.Name = "tlbQualità"
    Me.tlbQualità.NTSIsCheckBox = False
    Me.tlbQualità.Visible = True
    '
    'tlbApriLead
    '
    Me.tlbApriLead.Caption = "Apri lead associato"
    Me.tlbApriLead.GlyphPath = ""
    Me.tlbApriLead.Id = 36
    Me.tlbApriLead.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbApriLead.Name = "tlbApriLead"
    Me.tlbApriLead.NTSIsCheckBox = False
    Me.tlbApriLead.Visible = True
    '
    'tlbMovimenti
    '
    Me.tlbMovimenti.Caption = "Visualizza movimenti"
    Me.tlbMovimenti.GlyphPath = ""
    Me.tlbMovimenti.Id = 37
    Me.tlbMovimenti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbMovimenti.Name = "tlbMovimenti"
    Me.tlbMovimenti.NTSIsCheckBox = False
    Me.tlbMovimenti.Visible = True
    '
    'tlbOrdini
    '
    Me.tlbOrdini.Caption = "Visualizza righe ordini/impegni"
    Me.tlbOrdini.GlyphPath = ""
    Me.tlbOrdini.Id = 38
    Me.tlbOrdini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F9))
    Me.tlbOrdini.Name = "tlbOrdini"
    Me.tlbOrdini.NTSIsCheckBox = False
    Me.tlbOrdini.Visible = True
    '
    'tlbNote
    '
    Me.tlbNote.Caption = "Note"
    Me.tlbNote.GlyphPath = ""
    Me.tlbNote.Id = 44
    Me.tlbNote.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbNote.Name = "tlbNote"
    Me.tlbNote.NTSIsCheckBox = False
    Me.tlbNote.Visible = True
    '
    'tlbCalcolaCodFisc
    '
    Me.tlbCalcolaCodFisc.Caption = "Calcola codice fiscale"
    Me.tlbCalcolaCodFisc.GlyphPath = ""
    Me.tlbCalcolaCodFisc.Id = 26
    Me.tlbCalcolaCodFisc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbCalcolaCodFisc.Name = "tlbCalcolaCodFisc"
    Me.tlbCalcolaCodFisc.NTSIsCheckBox = False
    Me.tlbCalcolaCodFisc.Visible = True
    '
    'tlbRitornaCodFisc
    '
    Me.tlbRitornaCodFisc.Caption = "Ritorna codice fiscale"
    Me.tlbRitornaCodFisc.GlyphPath = ""
    Me.tlbRitornaCodFisc.Id = 27
    Me.tlbRitornaCodFisc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbRitornaCodFisc.Name = "tlbRitornaCodFisc"
    Me.tlbRitornaCodFisc.NTSIsCheckBox = False
    Me.tlbRitornaCodFisc.Visible = True
    '
    'tlbWeb
    '
    Me.tlbWeb.Caption = "Visita sito Web"
    Me.tlbWeb.GlyphPath = ""
    Me.tlbWeb.Id = 47
    Me.tlbWeb.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W))
    Me.tlbWeb.Name = "tlbWeb"
    Me.tlbWeb.NTSIsCheckBox = False
    Me.tlbWeb.Visible = True
    '
    'tlbMail
    '
    Me.tlbMail.Caption = "Richiama posta elettronica"
    Me.tlbMail.GlyphPath = ""
    Me.tlbMail.Id = 48
    Me.tlbMail.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
    Me.tlbMail.Name = "tlbMail"
    Me.tlbMail.NTSIsCheckBox = False
    Me.tlbMail.Visible = True
    '
    'tlbEmail
    '
    Me.tlbEmail.Caption = "Visualizza e-mail scambiate"
    Me.tlbEmail.GlyphPath = ""
    Me.tlbEmail.Id = 50
    Me.tlbEmail.Name = "tlbEmail"
    Me.tlbEmail.Visible = True
    '
    'tlbSkype
    '
    Me.tlbSkype.Caption = "Chiama con Skype"
    Me.tlbSkype.GlyphPath = ""
    Me.tlbSkype.Id = 58
    Me.tlbSkype.Name = "tlbSkype"
    Me.tlbSkype.Visible = True
    '
    'tlbGoogleMaps
    '
    Me.tlbGoogleMaps.Caption = "Localizza posizione"
    Me.tlbGoogleMaps.GlyphPath = ""
    Me.tlbGoogleMaps.Id = 49
    Me.tlbGoogleMaps.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbGoogleMaps.Name = "tlbGoogleMaps"
    Me.tlbGoogleMaps.Visible = True
    '
    'tlbEstensioni
    '
    Me.tlbEstensioni.Caption = "Estensioni anagrafiche"
    Me.tlbEstensioni.GlyphPath = ""
    Me.tlbEstensioni.Id = 43
    Me.tlbEstensioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbEstensioni.Name = "tlbEstensioni"
    Me.tlbEstensioni.Visible = True
    '
    'tlbGestioneSconti
    '
    Me.tlbGestioneSconti.Caption = "Gestione Sconti per Classe conto/Classe Articolo"
    Me.tlbGestioneSconti.GlyphPath = ""
    Me.tlbGestioneSconti.Id = 52
    Me.tlbGestioneSconti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5))
    Me.tlbGestioneSconti.Name = "tlbGestioneSconti"
    Me.tlbGestioneSconti.NTSIsCheckBox = False
    Me.tlbGestioneSconti.Visible = True
    '
    'tlbSimula
    '
    Me.tlbSimula.Caption = "Simulazione vendita"
    Me.tlbSimula.GlyphPath = ""
    Me.tlbSimula.Id = 57
    Me.tlbSimula.Name = "tlbSimula"
    Me.tlbSimula.Visible = True
    '
    'tlbOrganizzazione
    '
    Me.tlbOrganizzazione.Caption = "Organizzazione"
    Me.tlbOrganizzazione.Glyph = CType(resources.GetObject("tlbOrganizzazione.Glyph"), System.Drawing.Image)
    Me.tlbOrganizzazione.GlyphPath = ""
    Me.tlbOrganizzazione.Id = 42
    Me.tlbOrganizzazione.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbOrganizzazione.Name = "tlbOrganizzazione"
    Me.tlbOrganizzazione.Visible = True
    '
    'tlbContratti
    '
    Me.tlbContratti.Caption = "Contratti"
    Me.tlbContratti.Glyph = CType(resources.GetObject("tlbContratti.Glyph"), System.Drawing.Image)
    Me.tlbContratti.GlyphPath = ""
    Me.tlbContratti.Id = 59
    Me.tlbContratti.Name = "tlbContratti"
    Me.tlbContratti.Visible = True
    '
    'tlbOle
    '
    Me.tlbOle.Caption = "Oggetto OLE collegati"
    Me.tlbOle.Glyph = CType(resources.GetObject("tlbOle.Glyph"), System.Drawing.Image)
    Me.tlbOle.GlyphPath = ""
    Me.tlbOle.Id = 46
    Me.tlbOle.Name = "tlbOle"
    Me.tlbOle.Visible = True
    '
    'tlbCli
    '
    Me.tlbCli.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbCli.Caption = "Clienti"
    Me.tlbCli.Glyph = CType(resources.GetObject("tlbCli.Glyph"), System.Drawing.Image)
    Me.tlbCli.GlyphPath = ""
    Me.tlbCli.Id = 28
    Me.tlbCli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1))
    Me.tlbCli.Name = "tlbCli"
    Me.tlbCli.Visible = True
    '
    'tlbForn
    '
    Me.tlbForn.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbForn.Caption = "Fornitori"
    Me.tlbForn.Glyph = CType(resources.GetObject("tlbForn.Glyph"), System.Drawing.Image)
    Me.tlbForn.GlyphPath = ""
    Me.tlbForn.Id = 29
    Me.tlbForn.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
    Me.tlbForn.Name = "tlbForn"
    Me.tlbForn.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'NtsBarMenuItem3
    '
    Me.NtsBarMenuItem3.Caption = "Apri lead associato"
    Me.NtsBarMenuItem3.GlyphPath = ""
    Me.NtsBarMenuItem3.Id = 39
    Me.NtsBarMenuItem3.Name = "NtsBarMenuItem3"
    Me.NtsBarMenuItem3.NTSIsCheckBox = False
    Me.NtsBarMenuItem3.Visible = True
    '
    'tsClie
    '
    Me.tsClie.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsClie.Location = New System.Drawing.Point(0, 0)
    Me.tsClie.Name = "tsClie"
    Me.tsClie.SelectedTabPage = Me.NtsTabPage4
    Me.tsClie.Size = New System.Drawing.Size(780, 347)
    Me.tsClie.TabIndex = 4
    Me.tsClie.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6, Me.NtsTabPage7, Me.NtsTabPage8, Me.NtsTabPage9, Me.NtsTabPage10})
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage1.Text = "&1 - Generale"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.pnPag1Dx)
    Me.pnPag1.Controls.Add(Me.pnPag1Sx)
    Me.pnPag1.Controls.Add(Me.pnPag1Bottom)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(771, 317)
    Me.pnPag1.TabIndex = 0
    Me.pnPag1.Text = "NtsPanel1"
    '
    'pnPag1Dx
    '
    Me.pnPag1Dx.AllowDrop = True
    Me.pnPag1Dx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Dx.Appearance.Options.UseBackColor = True
    Me.pnPag1Dx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Dx.Controls.Add(Me.lbAn_cell)
    Me.pnPag1Dx.Controls.Add(Me.edAn_cell)
    Me.pnPag1Dx.Controls.Add(Me.ckAn_omocodice)
    Me.pnPag1Dx.Controls.Add(Me.cbAn_usaem)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_usaem)
    Me.pnPag1Dx.Controls.Add(Me.edAn_email)
    Me.pnPag1Dx.Controls.Add(Me.edAn_pariva)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_faxtlx)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_email)
    Me.pnPag1Dx.Controls.Add(Me.edAn_faxtlx)
    Me.pnPag1Dx.Controls.Add(Me.edAn_telef)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_pariva)
    Me.pnPag1Dx.Controls.Add(Me.edAn_codfis)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_codfis)
    Me.pnPag1Dx.Controls.Add(Me.lbAn_telef)
    Me.pnPag1Dx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Dx.Location = New System.Drawing.Point(364, 0)
    Me.pnPag1Dx.Name = "pnPag1Dx"
    Me.pnPag1Dx.NTSActiveTrasparency = True
    Me.pnPag1Dx.Size = New System.Drawing.Size(399, 192)
    Me.pnPag1Dx.TabIndex = 574
    Me.pnPag1Dx.Text = "NtsPanel1"
    '
    'lbAn_cell
    '
    Me.lbAn_cell.AutoSize = True
    Me.lbAn_cell.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cell.Location = New System.Drawing.Point(7, 89)
    Me.lbAn_cell.Name = "lbAn_cell"
    Me.lbAn_cell.NTSDbField = ""
    Me.lbAn_cell.Size = New System.Drawing.Size(48, 13)
    Me.lbAn_cell.TabIndex = 589
    Me.lbAn_cell.Text = "Cellulare"
    Me.lbAn_cell.Tooltip = ""
    Me.lbAn_cell.UseMnemonic = False
    '
    'edAn_cell
    '
    Me.edAn_cell.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_cell.EditValue = ""
    Me.edAn_cell.Location = New System.Drawing.Point(120, 86)
    Me.edAn_cell.Name = "edAn_cell"
    Me.edAn_cell.NTSDbField = ""
    Me.edAn_cell.NTSForzaVisZoom = False
    Me.edAn_cell.NTSOldValue = ""
    Me.edAn_cell.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_cell.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_cell.Properties.AutoHeight = False
    Me.edAn_cell.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_cell.Properties.MaxLength = 65536
    Me.edAn_cell.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_cell.Size = New System.Drawing.Size(194, 20)
    Me.edAn_cell.TabIndex = 590
    '
    'ckAn_omocodice
    '
    Me.ckAn_omocodice.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_omocodice.Location = New System.Drawing.Point(318, 8)
    Me.ckAn_omocodice.Name = "ckAn_omocodice"
    Me.ckAn_omocodice.NTSCheckValue = "S"
    Me.ckAn_omocodice.NTSUnCheckValue = "N"
    Me.ckAn_omocodice.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_omocodice.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_omocodice.Properties.AutoHeight = False
    Me.ckAn_omocodice.Properties.Caption = "Omocodice"
    Me.ckAn_omocodice.Size = New System.Drawing.Size(75, 19)
    Me.ckAn_omocodice.TabIndex = 588
    '
    'cbAn_usaem
    '
    Me.cbAn_usaem.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_usaem.DataSource = Nothing
    Me.cbAn_usaem.DisplayMember = ""
    Me.cbAn_usaem.Location = New System.Drawing.Point(120, 167)
    Me.cbAn_usaem.Name = "cbAn_usaem"
    Me.cbAn_usaem.NTSDbField = ""
    Me.cbAn_usaem.Properties.AutoHeight = False
    Me.cbAn_usaem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_usaem.Properties.DropDownRows = 30
    Me.cbAn_usaem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_usaem.SelectedValue = ""
    Me.cbAn_usaem.Size = New System.Drawing.Size(194, 20)
    Me.cbAn_usaem.TabIndex = 566
    Me.cbAn_usaem.ValueMember = ""
    '
    'lbAn_usaem
    '
    Me.lbAn_usaem.AutoSize = True
    Me.lbAn_usaem.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_usaem.Location = New System.Drawing.Point(7, 170)
    Me.lbAn_usaem.Name = "lbAn_usaem"
    Me.lbAn_usaem.NTSDbField = ""
    Me.lbAn_usaem.Size = New System.Drawing.Size(115, 13)
    Me.lbAn_usaem.TabIndex = 543
    Me.lbAn_usaem.Text = "Modalità di corrispond."
    Me.lbAn_usaem.Tooltip = ""
    Me.lbAn_usaem.UseMnemonic = False
    '
    'edAn_email
    '
    Me.edAn_email.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_email.EditValue = ""
    Me.edAn_email.Location = New System.Drawing.Point(120, 138)
    Me.edAn_email.Name = "edAn_email"
    Me.edAn_email.NTSDbField = ""
    Me.edAn_email.NTSForzaVisZoom = False
    Me.edAn_email.NTSOldValue = ""
    Me.edAn_email.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_email.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_email.Properties.AutoHeight = False
    Me.edAn_email.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_email.Properties.MaxLength = 65536
    Me.edAn_email.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_email.Size = New System.Drawing.Size(194, 20)
    Me.edAn_email.TabIndex = 564
    '
    'edAn_pariva
    '
    Me.edAn_pariva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_pariva.EditValue = ""
    Me.edAn_pariva.Location = New System.Drawing.Point(120, 34)
    Me.edAn_pariva.Name = "edAn_pariva"
    Me.edAn_pariva.NTSDbField = ""
    Me.edAn_pariva.NTSForzaVisZoom = False
    Me.edAn_pariva.NTSOldValue = ""
    Me.edAn_pariva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_pariva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_pariva.Properties.AutoHeight = False
    Me.edAn_pariva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_pariva.Properties.MaxLength = 65536
    Me.edAn_pariva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_pariva.Size = New System.Drawing.Size(194, 20)
    Me.edAn_pariva.TabIndex = 555
    '
    'lbAn_faxtlx
    '
    Me.lbAn_faxtlx.AutoSize = True
    Me.lbAn_faxtlx.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_faxtlx.Location = New System.Drawing.Point(7, 115)
    Me.lbAn_faxtlx.Name = "lbAn_faxtlx"
    Me.lbAn_faxtlx.NTSDbField = ""
    Me.lbAn_faxtlx.Size = New System.Drawing.Size(25, 13)
    Me.lbAn_faxtlx.TabIndex = 534
    Me.lbAn_faxtlx.Text = "Fax"
    Me.lbAn_faxtlx.Tooltip = ""
    Me.lbAn_faxtlx.UseMnemonic = False
    '
    'lbAn_email
    '
    Me.lbAn_email.AutoSize = True
    Me.lbAn_email.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_email.Location = New System.Drawing.Point(7, 141)
    Me.lbAn_email.Name = "lbAn_email"
    Me.lbAn_email.NTSDbField = ""
    Me.lbAn_email.Size = New System.Drawing.Size(35, 13)
    Me.lbAn_email.TabIndex = 541
    Me.lbAn_email.Text = "E-mail"
    Me.lbAn_email.Tooltip = ""
    Me.lbAn_email.UseMnemonic = False
    '
    'edAn_faxtlx
    '
    Me.edAn_faxtlx.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_faxtlx.EditValue = ""
    Me.edAn_faxtlx.Location = New System.Drawing.Point(120, 112)
    Me.edAn_faxtlx.Name = "edAn_faxtlx"
    Me.edAn_faxtlx.NTSDbField = ""
    Me.edAn_faxtlx.NTSForzaVisZoom = False
    Me.edAn_faxtlx.NTSOldValue = ""
    Me.edAn_faxtlx.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_faxtlx.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_faxtlx.Properties.AutoHeight = False
    Me.edAn_faxtlx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_faxtlx.Properties.MaxLength = 65536
    Me.edAn_faxtlx.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_faxtlx.Size = New System.Drawing.Size(194, 20)
    Me.edAn_faxtlx.TabIndex = 557
    '
    'edAn_telef
    '
    Me.edAn_telef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_telef.EditValue = ""
    Me.edAn_telef.Location = New System.Drawing.Point(120, 60)
    Me.edAn_telef.Name = "edAn_telef"
    Me.edAn_telef.NTSDbField = ""
    Me.edAn_telef.NTSForzaVisZoom = False
    Me.edAn_telef.NTSOldValue = ""
    Me.edAn_telef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_telef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_telef.Properties.AutoHeight = False
    Me.edAn_telef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_telef.Properties.MaxLength = 65536
    Me.edAn_telef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_telef.Size = New System.Drawing.Size(194, 20)
    Me.edAn_telef.TabIndex = 556
    '
    'lbAn_pariva
    '
    Me.lbAn_pariva.AutoSize = True
    Me.lbAn_pariva.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_pariva.Location = New System.Drawing.Point(7, 37)
    Me.lbAn_pariva.Name = "lbAn_pariva"
    Me.lbAn_pariva.NTSDbField = ""
    Me.lbAn_pariva.Size = New System.Drawing.Size(86, 13)
    Me.lbAn_pariva.TabIndex = 532
    Me.lbAn_pariva.Text = "Partita IVA Italia"
    Me.lbAn_pariva.Tooltip = ""
    Me.lbAn_pariva.UseMnemonic = False
    '
    'edAn_codfis
    '
    Me.edAn_codfis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codfis.EditValue = ""
    Me.edAn_codfis.Location = New System.Drawing.Point(120, 8)
    Me.edAn_codfis.Name = "edAn_codfis"
    Me.edAn_codfis.NTSDbField = ""
    Me.edAn_codfis.NTSForzaVisZoom = False
    Me.edAn_codfis.NTSOldValue = ""
    Me.edAn_codfis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codfis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codfis.Properties.AutoHeight = False
    Me.edAn_codfis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codfis.Properties.MaxLength = 65536
    Me.edAn_codfis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codfis.Size = New System.Drawing.Size(194, 20)
    Me.edAn_codfis.TabIndex = 554
    '
    'lbAn_codfis
    '
    Me.lbAn_codfis.AutoSize = True
    Me.lbAn_codfis.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codfis.Location = New System.Drawing.Point(7, 9)
    Me.lbAn_codfis.Name = "lbAn_codfis"
    Me.lbAn_codfis.NTSDbField = ""
    Me.lbAn_codfis.Size = New System.Drawing.Size(111, 13)
    Me.lbAn_codfis.TabIndex = 531
    Me.lbAn_codfis.Text = "Cod. fiscale/PI estera"
    Me.lbAn_codfis.Tooltip = ""
    Me.lbAn_codfis.UseMnemonic = False
    '
    'lbAn_telef
    '
    Me.lbAn_telef.AutoSize = True
    Me.lbAn_telef.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_telef.Location = New System.Drawing.Point(7, 63)
    Me.lbAn_telef.Name = "lbAn_telef"
    Me.lbAn_telef.NTSDbField = ""
    Me.lbAn_telef.Size = New System.Drawing.Size(49, 13)
    Me.lbAn_telef.TabIndex = 533
    Me.lbAn_telef.Text = "Telefono"
    Me.lbAn_telef.Tooltip = ""
    Me.lbAn_telef.UseMnemonic = False
    '
    'pnPag1Sx
    '
    Me.pnPag1Sx.AllowDrop = True
    Me.pnPag1Sx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Sx.Appearance.Options.UseBackColor = True
    Me.pnPag1Sx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Sx.Controls.Add(Me.lbAn_tpsogiva)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_statofed)
    Me.pnPag1Sx.Controls.Add(Me.edAn_statofed)
    Me.pnPag1Sx.Controls.Add(Me.cbAn_tpsogiva)
    Me.pnPag1Sx.Controls.Add(Me.lbXx_stato)
    Me.pnPag1Sx.Controls.Add(Me.lbXx_codcomu)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_codcomu)
    Me.pnPag1Sx.Controls.Add(Me.edAn_codcomu)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_citta)
    Me.pnPag1Sx.Controls.Add(Me.edAn_citta)
    Me.pnPag1Sx.Controls.Add(Me.edAn_indir)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_stato)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_cap)
    Me.pnPag1Sx.Controls.Add(Me.edAn_stato)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_indir)
    Me.pnPag1Sx.Controls.Add(Me.edAn_cap)
    Me.pnPag1Sx.Controls.Add(Me.edAn_prov)
    Me.pnPag1Sx.Controls.Add(Me.lbAn_prov)
    Me.pnPag1Sx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Sx.Location = New System.Drawing.Point(3, 0)
    Me.pnPag1Sx.Name = "pnPag1Sx"
    Me.pnPag1Sx.NTSActiveTrasparency = True
    Me.pnPag1Sx.Size = New System.Drawing.Size(358, 192)
    Me.pnPag1Sx.TabIndex = 573
    Me.pnPag1Sx.Text = "NtsPanel1"
    '
    'lbAn_tpsogiva
    '
    Me.lbAn_tpsogiva.AutoSize = True
    Me.lbAn_tpsogiva.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_tpsogiva.Location = New System.Drawing.Point(3, 170)
    Me.lbAn_tpsogiva.Name = "lbAn_tpsogiva"
    Me.lbAn_tpsogiva.NTSDbField = ""
    Me.lbAn_tpsogiva.Size = New System.Drawing.Size(77, 13)
    Me.lbAn_tpsogiva.TabIndex = 589
    Me.lbAn_tpsogiva.Text = "Tipo sogg. IVA"
    Me.lbAn_tpsogiva.Tooltip = ""
    Me.lbAn_tpsogiva.UseMnemonic = False
    '
    'lbAn_statofed
    '
    Me.lbAn_statofed.AutoSize = True
    Me.lbAn_statofed.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_statofed.Location = New System.Drawing.Point(3, 141)
    Me.lbAn_statofed.Name = "lbAn_statofed"
    Me.lbAn_statofed.NTSDbField = ""
    Me.lbAn_statofed.Size = New System.Drawing.Size(93, 13)
    Me.lbAn_statofed.TabIndex = 587
    Me.lbAn_statofed.Text = "Stato fed./contea"
    Me.lbAn_statofed.Tooltip = ""
    Me.lbAn_statofed.UseMnemonic = False
    '
    'edAn_statofed
    '
    Me.edAn_statofed.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_statofed.EditValue = ""
    Me.edAn_statofed.Location = New System.Drawing.Point(102, 138)
    Me.edAn_statofed.Name = "edAn_statofed"
    Me.edAn_statofed.NTSDbField = ""
    Me.edAn_statofed.NTSForzaVisZoom = False
    Me.edAn_statofed.NTSOldValue = ""
    Me.edAn_statofed.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_statofed.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_statofed.Properties.AutoHeight = False
    Me.edAn_statofed.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_statofed.Properties.MaxLength = 65536
    Me.edAn_statofed.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_statofed.Size = New System.Drawing.Size(253, 20)
    Me.edAn_statofed.TabIndex = 588
    '
    'cbAn_tpsogiva
    '
    Me.cbAn_tpsogiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_tpsogiva.DataSource = Nothing
    Me.cbAn_tpsogiva.DisplayMember = ""
    Me.cbAn_tpsogiva.Location = New System.Drawing.Point(102, 167)
    Me.cbAn_tpsogiva.Name = "cbAn_tpsogiva"
    Me.cbAn_tpsogiva.NTSDbField = ""
    Me.cbAn_tpsogiva.Properties.AutoHeight = False
    Me.cbAn_tpsogiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_tpsogiva.Properties.DropDownRows = 30
    Me.cbAn_tpsogiva.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_tpsogiva.SelectedValue = ""
    Me.cbAn_tpsogiva.Size = New System.Drawing.Size(253, 20)
    Me.cbAn_tpsogiva.TabIndex = 590
    Me.cbAn_tpsogiva.ValueMember = ""
    '
    'lbXx_stato
    '
    Me.lbXx_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_stato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_stato.Location = New System.Drawing.Point(171, 112)
    Me.lbXx_stato.Name = "lbXx_stato"
    Me.lbXx_stato.NTSDbField = ""
    Me.lbXx_stato.Size = New System.Drawing.Size(184, 20)
    Me.lbXx_stato.TabIndex = 580
    Me.lbXx_stato.Text = "xx_stato"
    Me.lbXx_stato.Tooltip = ""
    Me.lbXx_stato.UseMnemonic = False
    '
    'lbXx_codcomu
    '
    Me.lbXx_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcomu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcomu.Location = New System.Drawing.Point(171, 34)
    Me.lbXx_codcomu.Name = "lbXx_codcomu"
    Me.lbXx_codcomu.NTSDbField = ""
    Me.lbXx_codcomu.Size = New System.Drawing.Size(184, 20)
    Me.lbXx_codcomu.TabIndex = 579
    Me.lbXx_codcomu.Text = "xx_codcomu"
    Me.lbXx_codcomu.Tooltip = ""
    Me.lbXx_codcomu.UseMnemonic = False
    '
    'lbAn_codcomu
    '
    Me.lbAn_codcomu.AutoSize = True
    Me.lbAn_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codcomu.Location = New System.Drawing.Point(3, 37)
    Me.lbAn_codcomu.Name = "lbAn_codcomu"
    Me.lbAn_codcomu.NTSDbField = ""
    Me.lbAn_codcomu.Size = New System.Drawing.Size(70, 13)
    Me.lbAn_codcomu.TabIndex = 577
    Me.lbAn_codcomu.Text = "Cod. comune"
    Me.lbAn_codcomu.Tooltip = ""
    Me.lbAn_codcomu.UseMnemonic = False
    '
    'edAn_codcomu
    '
    Me.edAn_codcomu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codcomu.EditValue = ""
    Me.edAn_codcomu.Location = New System.Drawing.Point(102, 34)
    Me.edAn_codcomu.Name = "edAn_codcomu"
    Me.edAn_codcomu.NTSDbField = ""
    Me.edAn_codcomu.NTSForzaVisZoom = False
    Me.edAn_codcomu.NTSOldValue = ""
    Me.edAn_codcomu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codcomu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codcomu.Properties.AutoHeight = False
    Me.edAn_codcomu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codcomu.Properties.MaxLength = 65536
    Me.edAn_codcomu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codcomu.Size = New System.Drawing.Size(63, 20)
    Me.edAn_codcomu.TabIndex = 578
    '
    'lbAn_citta
    '
    Me.lbAn_citta.AutoSize = True
    Me.lbAn_citta.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_citta.Location = New System.Drawing.Point(3, 63)
    Me.lbAn_citta.Name = "lbAn_citta"
    Me.lbAn_citta.NTSDbField = ""
    Me.lbAn_citta.Size = New System.Drawing.Size(67, 13)
    Me.lbAn_citta.TabIndex = 528
    Me.lbAn_citta.Text = "Citta/località"
    Me.lbAn_citta.Tooltip = ""
    Me.lbAn_citta.UseMnemonic = False
    '
    'edAn_citta
    '
    Me.edAn_citta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_citta.EditValue = ""
    Me.edAn_citta.Location = New System.Drawing.Point(102, 60)
    Me.edAn_citta.Name = "edAn_citta"
    Me.edAn_citta.NTSDbField = ""
    Me.edAn_citta.NTSForzaVisZoom = False
    Me.edAn_citta.NTSOldValue = ""
    Me.edAn_citta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_citta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_citta.Properties.AutoHeight = False
    Me.edAn_citta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_citta.Properties.MaxLength = 65536
    Me.edAn_citta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_citta.Size = New System.Drawing.Size(253, 20)
    Me.edAn_citta.TabIndex = 551
    '
    'edAn_indir
    '
    Me.edAn_indir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_indir.EditValue = ""
    Me.edAn_indir.Location = New System.Drawing.Point(102, 7)
    Me.edAn_indir.Name = "edAn_indir"
    Me.edAn_indir.NTSDbField = ""
    Me.edAn_indir.NTSForzaVisZoom = False
    Me.edAn_indir.NTSOldValue = ""
    Me.edAn_indir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_indir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_indir.Properties.AutoHeight = False
    Me.edAn_indir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_indir.Properties.MaxLength = 65536
    Me.edAn_indir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_indir.Size = New System.Drawing.Size(253, 20)
    Me.edAn_indir.TabIndex = 549
    '
    'lbAn_stato
    '
    Me.lbAn_stato.AutoSize = True
    Me.lbAn_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_stato.Location = New System.Drawing.Point(3, 115)
    Me.lbAn_stato.Name = "lbAn_stato"
    Me.lbAn_stato.NTSDbField = ""
    Me.lbAn_stato.Size = New System.Drawing.Size(80, 13)
    Me.lbAn_stato.TabIndex = 530
    Me.lbAn_stato.Text = "Cod. stato est."
    Me.lbAn_stato.Tooltip = ""
    Me.lbAn_stato.UseMnemonic = False
    '
    'lbAn_cap
    '
    Me.lbAn_cap.AutoSize = True
    Me.lbAn_cap.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cap.Location = New System.Drawing.Point(3, 89)
    Me.lbAn_cap.Name = "lbAn_cap"
    Me.lbAn_cap.NTSDbField = ""
    Me.lbAn_cap.Size = New System.Drawing.Size(26, 13)
    Me.lbAn_cap.TabIndex = 527
    Me.lbAn_cap.Text = "Cap"
    Me.lbAn_cap.Tooltip = ""
    Me.lbAn_cap.UseMnemonic = False
    '
    'edAn_stato
    '
    Me.edAn_stato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_stato.EditValue = ""
    Me.edAn_stato.Location = New System.Drawing.Point(102, 112)
    Me.edAn_stato.Name = "edAn_stato"
    Me.edAn_stato.NTSDbField = ""
    Me.edAn_stato.NTSForzaVisZoom = False
    Me.edAn_stato.NTSOldValue = ""
    Me.edAn_stato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_stato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_stato.Properties.AutoHeight = False
    Me.edAn_stato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_stato.Properties.MaxLength = 65536
    Me.edAn_stato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_stato.Size = New System.Drawing.Size(63, 20)
    Me.edAn_stato.TabIndex = 553
    '
    'lbAn_indir
    '
    Me.lbAn_indir.AutoSize = True
    Me.lbAn_indir.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_indir.Location = New System.Drawing.Point(3, 10)
    Me.lbAn_indir.Name = "lbAn_indir"
    Me.lbAn_indir.NTSDbField = ""
    Me.lbAn_indir.Size = New System.Drawing.Size(47, 13)
    Me.lbAn_indir.TabIndex = 526
    Me.lbAn_indir.Text = "Indirizzo"
    Me.lbAn_indir.Tooltip = ""
    Me.lbAn_indir.UseMnemonic = False
    '
    'edAn_cap
    '
    Me.edAn_cap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_cap.EditValue = ""
    Me.edAn_cap.Location = New System.Drawing.Point(102, 86)
    Me.edAn_cap.Name = "edAn_cap"
    Me.edAn_cap.NTSDbField = ""
    Me.edAn_cap.NTSForzaVisZoom = False
    Me.edAn_cap.NTSOldValue = ""
    Me.edAn_cap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_cap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_cap.Properties.AutoHeight = False
    Me.edAn_cap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_cap.Properties.MaxLength = 65536
    Me.edAn_cap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_cap.Size = New System.Drawing.Size(63, 20)
    Me.edAn_cap.TabIndex = 550
    '
    'edAn_prov
    '
    Me.edAn_prov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_prov.EditValue = ""
    Me.edAn_prov.Location = New System.Drawing.Point(250, 86)
    Me.edAn_prov.Name = "edAn_prov"
    Me.edAn_prov.NTSDbField = ""
    Me.edAn_prov.NTSForzaVisZoom = False
    Me.edAn_prov.NTSOldValue = ""
    Me.edAn_prov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_prov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_prov.Properties.AutoHeight = False
    Me.edAn_prov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_prov.Properties.MaxLength = 65536
    Me.edAn_prov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_prov.Size = New System.Drawing.Size(45, 20)
    Me.edAn_prov.TabIndex = 552
    '
    'lbAn_prov
    '
    Me.lbAn_prov.AutoSize = True
    Me.lbAn_prov.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_prov.Location = New System.Drawing.Point(171, 89)
    Me.lbAn_prov.Name = "lbAn_prov"
    Me.lbAn_prov.NTSDbField = ""
    Me.lbAn_prov.Size = New System.Drawing.Size(50, 13)
    Me.lbAn_prov.TabIndex = 529
    Me.lbAn_prov.Text = "Provincia"
    Me.lbAn_prov.Tooltip = ""
    Me.lbAn_prov.UseMnemonic = False
    '
    'pnPag1Bottom
    '
    Me.pnPag1Bottom.AllowDrop = True
    Me.pnPag1Bottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Bottom.Appearance.Options.UseBackColor = True
    Me.pnPag1Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Bottom.Controls.Add(Me.fmIndirizzi)
    Me.pnPag1Bottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Bottom.Location = New System.Drawing.Point(0, 189)
    Me.pnPag1Bottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag1Bottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag1Bottom.Name = "pnPag1Bottom"
    Me.pnPag1Bottom.NTSActiveTrasparency = True
    Me.pnPag1Bottom.Size = New System.Drawing.Size(779, 128)
    Me.pnPag1Bottom.TabIndex = 572
    Me.pnPag1Bottom.Text = "NtsPanel1"
    '
    'fmIndirizzi
    '
    Me.fmIndirizzi.AllowDrop = True
    Me.fmIndirizzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIndirizzi.Appearance.Options.UseBackColor = True
    Me.fmIndirizzi.Controls.Add(Me.pnIndirDx)
    Me.fmIndirizzi.Controls.Add(Me.pnIndirSx)
    Me.fmIndirizzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIndirizzi.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmIndirizzi.Location = New System.Drawing.Point(0, 0)
    Me.fmIndirizzi.Name = "fmIndirizzi"
    Me.fmIndirizzi.Size = New System.Drawing.Size(779, 128)
    Me.fmIndirizzi.TabIndex = 562
    Me.fmIndirizzi.Text = "Indirizzi"
    '
    'pnIndirDx
    '
    Me.pnIndirDx.AllowDrop = True
    Me.pnIndirDx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnIndirDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnIndirDx.Appearance.Options.UseBackColor = True
    Me.pnIndirDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnIndirDx.Controls.Add(Me.cmdAltriIndir)
    Me.pnIndirDx.Controls.Add(Me.lbAn_destpag)
    Me.pnIndirDx.Controls.Add(Me.lbXx_destpag)
    Me.pnIndirDx.Controls.Add(Me.lbAn_destin)
    Me.pnIndirDx.Controls.Add(Me.lbXx_destin)
    Me.pnIndirDx.Controls.Add(Me.edAn_destpag)
    Me.pnIndirDx.Controls.Add(Me.edAn_destin)
    Me.pnIndirDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnIndirDx.Location = New System.Drawing.Point(364, 20)
    Me.pnIndirDx.Name = "pnIndirDx"
    Me.pnIndirDx.NTSActiveTrasparency = True
    Me.pnIndirDx.Size = New System.Drawing.Size(399, 106)
    Me.pnIndirDx.TabIndex = 563
    Me.pnIndirDx.Text = "NtsPanel1"
    '
    'cmdAltriIndir
    '
    Me.cmdAltriIndir.ImagePath = ""
    Me.cmdAltriIndir.ImageText = ""
    Me.cmdAltriIndir.Location = New System.Drawing.Point(293, 78)
    Me.cmdAltriIndir.Name = "cmdAltriIndir"
    Me.cmdAltriIndir.NTSContextMenu = Nothing
    Me.cmdAltriIndir.Size = New System.Drawing.Size(100, 24)
    Me.cmdAltriIndir.TabIndex = 550
    Me.cmdAltriIndir.Text = "Altri &indirizzi"
    '
    'lbAn_destpag
    '
    Me.lbAn_destpag.AutoSize = True
    Me.lbAn_destpag.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_destpag.Location = New System.Drawing.Point(7, 9)
    Me.lbAn_destpag.Name = "lbAn_destpag"
    Me.lbAn_destpag.NTSDbField = ""
    Me.lbAn_destpag.Size = New System.Drawing.Size(77, 13)
    Me.lbAn_destpag.TabIndex = 538
    Me.lbAn_destpag.Text = "Destin.pagam."
    Me.lbAn_destpag.Tooltip = ""
    Me.lbAn_destpag.UseMnemonic = False
    '
    'lbXx_destpag
    '
    Me.lbXx_destpag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_destpag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_destpag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_destpag.Location = New System.Drawing.Point(181, 5)
    Me.lbXx_destpag.Name = "lbXx_destpag"
    Me.lbXx_destpag.NTSDbField = ""
    Me.lbXx_destpag.Size = New System.Drawing.Size(214, 20)
    Me.lbXx_destpag.TabIndex = 578
    Me.lbXx_destpag.Text = "xx_destpag"
    Me.lbXx_destpag.Tooltip = ""
    Me.lbXx_destpag.UseMnemonic = False
    '
    'lbAn_destin
    '
    Me.lbAn_destin.AutoSize = True
    Me.lbAn_destin.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_destin.Location = New System.Drawing.Point(7, 33)
    Me.lbAn_destin.Name = "lbAn_destin"
    Me.lbAn_destin.NTSDbField = ""
    Me.lbAn_destin.Size = New System.Drawing.Size(73, 13)
    Me.lbAn_destin.TabIndex = 537
    Me.lbAn_destin.Text = "Destin. merce"
    Me.lbAn_destin.Tooltip = ""
    Me.lbAn_destin.UseMnemonic = False
    '
    'lbXx_destin
    '
    Me.lbXx_destin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_destin.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_destin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_destin.Location = New System.Drawing.Point(181, 29)
    Me.lbXx_destin.Name = "lbXx_destin"
    Me.lbXx_destin.NTSDbField = ""
    Me.lbXx_destin.Size = New System.Drawing.Size(214, 20)
    Me.lbXx_destin.TabIndex = 577
    Me.lbXx_destin.Text = "xx_destin"
    Me.lbXx_destin.Tooltip = ""
    Me.lbXx_destin.UseMnemonic = False
    '
    'edAn_destpag
    '
    Me.edAn_destpag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_destpag.EditValue = "0"
    Me.edAn_destpag.Location = New System.Drawing.Point(120, 5)
    Me.edAn_destpag.Name = "edAn_destpag"
    Me.edAn_destpag.NTSDbField = ""
    Me.edAn_destpag.NTSFormat = "0"
    Me.edAn_destpag.NTSForzaVisZoom = False
    Me.edAn_destpag.NTSOldValue = ""
    Me.edAn_destpag.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_destpag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_destpag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_destpag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_destpag.Properties.AutoHeight = False
    Me.edAn_destpag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_destpag.Properties.MaxLength = 65536
    Me.edAn_destpag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_destpag.Size = New System.Drawing.Size(55, 20)
    Me.edAn_destpag.TabIndex = 561
    '
    'edAn_destin
    '
    Me.edAn_destin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_destin.EditValue = "0"
    Me.edAn_destin.Location = New System.Drawing.Point(120, 29)
    Me.edAn_destin.Name = "edAn_destin"
    Me.edAn_destin.NTSDbField = ""
    Me.edAn_destin.NTSFormat = "0"
    Me.edAn_destin.NTSForzaVisZoom = False
    Me.edAn_destin.NTSOldValue = ""
    Me.edAn_destin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_destin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_destin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_destin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_destin.Properties.AutoHeight = False
    Me.edAn_destin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_destin.Properties.MaxLength = 65536
    Me.edAn_destin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_destin.Size = New System.Drawing.Size(55, 20)
    Me.edAn_destin.TabIndex = 560
    '
    'pnIndirSx
    '
    Me.pnIndirSx.AllowDrop = True
    Me.pnIndirSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnIndirSx.Appearance.Options.UseBackColor = True
    Me.pnIndirSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnIndirSx.Controls.Add(Me.ckDestresan)
    Me.pnIndirSx.Controls.Add(Me.ckDestcorr)
    Me.pnIndirSx.Controls.Add(Me.ckDestsedel)
    Me.pnIndirSx.Controls.Add(Me.ckDestdomf)
    Me.pnIndirSx.Controls.Add(Me.cmdDestcorr)
    Me.pnIndirSx.Controls.Add(Me.cmdDestresan)
    Me.pnIndirSx.Controls.Add(Me.cmdDestsedel)
    Me.pnIndirSx.Controls.Add(Me.cmdDestdomf)
    Me.pnIndirSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnIndirSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnIndirSx.Location = New System.Drawing.Point(2, 20)
    Me.pnIndirSx.Name = "pnIndirSx"
    Me.pnIndirSx.NTSActiveTrasparency = True
    Me.pnIndirSx.Size = New System.Drawing.Size(363, 106)
    Me.pnIndirSx.TabIndex = 562
    Me.pnIndirSx.Text = "NtsPanel1"
    '
    'ckDestresan
    '
    Me.ckDestresan.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDestresan.Enabled = False
    Me.ckDestresan.Location = New System.Drawing.Point(287, 54)
    Me.ckDestresan.Name = "ckDestresan"
    Me.ckDestresan.NTSCheckValue = "S"
    Me.ckDestresan.NTSUnCheckValue = "N"
    Me.ckDestresan.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDestresan.Properties.Appearance.Options.UseBackColor = True
    Me.ckDestresan.Properties.AutoHeight = False
    Me.ckDestresan.Properties.Caption = "Inserito"
    Me.ckDestresan.Size = New System.Drawing.Size(69, 19)
    Me.ckDestresan.TabIndex = 595
    '
    'ckDestcorr
    '
    Me.ckDestcorr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDestcorr.Enabled = False
    Me.ckDestcorr.Location = New System.Drawing.Point(287, 79)
    Me.ckDestcorr.Name = "ckDestcorr"
    Me.ckDestcorr.NTSCheckValue = "S"
    Me.ckDestcorr.NTSUnCheckValue = "N"
    Me.ckDestcorr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDestcorr.Properties.Appearance.Options.UseBackColor = True
    Me.ckDestcorr.Properties.AutoHeight = False
    Me.ckDestcorr.Properties.Caption = "Inserito"
    Me.ckDestcorr.Size = New System.Drawing.Size(69, 19)
    Me.ckDestcorr.TabIndex = 594
    '
    'ckDestsedel
    '
    Me.ckDestsedel.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckDestsedel.Enabled = False
    Me.ckDestsedel.Location = New System.Drawing.Point(287, 31)
    Me.ckDestsedel.Name = "ckDestsedel"
    Me.ckDestsedel.NTSCheckValue = "S"
    Me.ckDestsedel.NTSUnCheckValue = "N"
    Me.ckDestsedel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDestsedel.Properties.Appearance.Options.UseBackColor = True
    Me.ckDestsedel.Properties.AutoHeight = False
    Me.ckDestsedel.Properties.Caption = "Inserito"
    Me.ckDestsedel.Size = New System.Drawing.Size(69, 19)
    Me.ckDestsedel.TabIndex = 593
    '
    'ckDestdomf
    '
    Me.ckDestdomf.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDestdomf.Enabled = False
    Me.ckDestdomf.Location = New System.Drawing.Point(287, 6)
    Me.ckDestdomf.Name = "ckDestdomf"
    Me.ckDestdomf.NTSCheckValue = "S"
    Me.ckDestdomf.NTSUnCheckValue = "N"
    Me.ckDestdomf.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDestdomf.Properties.Appearance.Options.UseBackColor = True
    Me.ckDestdomf.Properties.AutoHeight = False
    Me.ckDestdomf.Properties.Caption = "Inserito"
    Me.ckDestdomf.Size = New System.Drawing.Size(69, 19)
    Me.ckDestdomf.TabIndex = 592
    '
    'cmdDestcorr
    '
    Me.cmdDestcorr.ImagePath = ""
    Me.cmdDestcorr.ImageText = ""
    Me.cmdDestcorr.Location = New System.Drawing.Point(3, 78)
    Me.cmdDestcorr.Name = "cmdDestcorr"
    Me.cmdDestcorr.NTSContextMenu = Nothing
    Me.cmdDestcorr.Size = New System.Drawing.Size(262, 24)
    Me.cmdDestcorr.TabIndex = 591
    Me.cmdDestcorr.Text = "&Luogo di esercizio attiv. all'estero"
    '
    'cmdDestresan
    '
    Me.cmdDestresan.ImagePath = ""
    Me.cmdDestresan.ImageText = ""
    Me.cmdDestresan.Location = New System.Drawing.Point(3, 53)
    Me.cmdDestresan.Name = "cmdDestresan"
    Me.cmdDestresan.NTSContextMenu = Nothing
    Me.cmdDestresan.Size = New System.Drawing.Size(262, 24)
    Me.cmdDestresan.TabIndex = 590
    Me.cmdDestresan.Text = "Resid&enza/Sede legale estera"
    '
    'cmdDestsedel
    '
    Me.cmdDestsedel.ImagePath = ""
    Me.cmdDestsedel.ImageText = ""
    Me.cmdDestsedel.Location = New System.Drawing.Point(3, 28)
    Me.cmdDestsedel.Name = "cmdDestsedel"
    Me.cmdDestsedel.NTSContextMenu = Nothing
    Me.cmdDestsedel.Size = New System.Drawing.Size(262, 24)
    Me.cmdDestsedel.TabIndex = 589
    Me.cmdDestsedel.Text = "Resid./Domic. fisc./Sede legale in I&talia"
    '
    'cmdDestdomf
    '
    Me.cmdDestdomf.ImagePath = ""
    Me.cmdDestdomf.ImageText = ""
    Me.cmdDestdomf.Location = New System.Drawing.Point(3, 3)
    Me.cmdDestdomf.Name = "cmdDestdomf"
    Me.cmdDestdomf.NTSContextMenu = Nothing
    Me.cmdDestdomf.Size = New System.Drawing.Size(262, 24)
    Me.cmdDestdomf.TabIndex = 588
    Me.cmdDestdomf.Text = "Do&micilio fiscale per provv. amministr."
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage2.Text = "&2 - Pers. fisica/giurid."
    '
    'pnPag2
    '
    Me.pnPag2.AllowDrop = True
    Me.pnPag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2.Appearance.Options.UseBackColor = True
    Me.pnPag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2.Controls.Add(Me.fmPersfisica)
    Me.pnPag2.Controls.Add(Me.fmNonresidenti)
    Me.pnPag2.Controls.Add(Me.fmNascita)
    Me.pnPag2.Controls.Add(Me.ckAn_condom)
    Me.pnPag2.Controls.Add(Me.ckAn_soggresi)
    Me.pnPag2.Controls.Add(Me.ckAn_profes)
    Me.pnPag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag2.Location = New System.Drawing.Point(0, 0)
    Me.pnPag2.Name = "pnPag2"
    Me.pnPag2.NTSActiveTrasparency = True
    Me.pnPag2.Size = New System.Drawing.Size(771, 317)
    Me.pnPag2.TabIndex = 0
    Me.pnPag2.Text = "NtsPanel1"
    '
    'fmPersfisica
    '
    Me.fmPersfisica.AllowDrop = True
    Me.fmPersfisica.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPersfisica.Appearance.Options.UseBackColor = True
    Me.fmPersfisica.Controls.Add(Me.edAn_titolo)
    Me.fmPersfisica.Controls.Add(Me.lbAn_titolo)
    Me.fmPersfisica.Controls.Add(Me.lbAn_sesso)
    Me.fmPersfisica.Controls.Add(Me.edAn_cognome)
    Me.fmPersfisica.Controls.Add(Me.lbAn_cognome)
    Me.fmPersfisica.Controls.Add(Me.edAn_nome)
    Me.fmPersfisica.Controls.Add(Me.cbAn_sesso)
    Me.fmPersfisica.Controls.Add(Me.lbAn_nome)
    Me.fmPersfisica.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPersfisica.Location = New System.Drawing.Point(3, 12)
    Me.fmPersfisica.Name = "fmPersfisica"
    Me.fmPersfisica.Size = New System.Drawing.Size(365, 109)
    Me.fmPersfisica.TabIndex = 600
    Me.fmPersfisica.Text = "Persona fisica"
    '
    'edAn_titolo
    '
    Me.edAn_titolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_titolo.EditValue = ""
    Me.edAn_titolo.Location = New System.Drawing.Point(238, 80)
    Me.edAn_titolo.Name = "edAn_titolo"
    Me.edAn_titolo.NTSDbField = ""
    Me.edAn_titolo.NTSForzaVisZoom = False
    Me.edAn_titolo.NTSOldValue = ""
    Me.edAn_titolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_titolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_titolo.Properties.AutoHeight = False
    Me.edAn_titolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_titolo.Properties.MaxLength = 65536
    Me.edAn_titolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_titolo.Size = New System.Drawing.Size(120, 20)
    Me.edAn_titolo.TabIndex = 596
    '
    'lbAn_titolo
    '
    Me.lbAn_titolo.AutoSize = True
    Me.lbAn_titolo.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_titolo.Location = New System.Drawing.Point(189, 83)
    Me.lbAn_titolo.Name = "lbAn_titolo"
    Me.lbAn_titolo.NTSDbField = ""
    Me.lbAn_titolo.Size = New System.Drawing.Size(33, 13)
    Me.lbAn_titolo.TabIndex = 595
    Me.lbAn_titolo.Text = "Titolo"
    Me.lbAn_titolo.Tooltip = ""
    Me.lbAn_titolo.UseMnemonic = False
    '
    'lbAn_sesso
    '
    Me.lbAn_sesso.AutoSize = True
    Me.lbAn_sesso.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_sesso.Location = New System.Drawing.Point(3, 83)
    Me.lbAn_sesso.Name = "lbAn_sesso"
    Me.lbAn_sesso.NTSDbField = ""
    Me.lbAn_sesso.Size = New System.Drawing.Size(35, 13)
    Me.lbAn_sesso.TabIndex = 594
    Me.lbAn_sesso.Text = "Sesso"
    Me.lbAn_sesso.Tooltip = ""
    Me.lbAn_sesso.UseMnemonic = False
    '
    'edAn_cognome
    '
    Me.edAn_cognome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_cognome.EditValue = ""
    Me.edAn_cognome.Location = New System.Drawing.Point(69, 28)
    Me.edAn_cognome.Name = "edAn_cognome"
    Me.edAn_cognome.NTSDbField = ""
    Me.edAn_cognome.NTSForzaVisZoom = False
    Me.edAn_cognome.NTSOldValue = ""
    Me.edAn_cognome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_cognome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_cognome.Properties.AutoHeight = False
    Me.edAn_cognome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_cognome.Properties.MaxLength = 65536
    Me.edAn_cognome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_cognome.Size = New System.Drawing.Size(289, 20)
    Me.edAn_cognome.TabIndex = 581
    '
    'lbAn_cognome
    '
    Me.lbAn_cognome.AutoSize = True
    Me.lbAn_cognome.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cognome.Location = New System.Drawing.Point(3, 31)
    Me.lbAn_cognome.Name = "lbAn_cognome"
    Me.lbAn_cognome.NTSDbField = ""
    Me.lbAn_cognome.Size = New System.Drawing.Size(52, 13)
    Me.lbAn_cognome.TabIndex = 562
    Me.lbAn_cognome.Text = "Cognome"
    Me.lbAn_cognome.Tooltip = ""
    Me.lbAn_cognome.UseMnemonic = False
    '
    'edAn_nome
    '
    Me.edAn_nome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_nome.EditValue = ""
    Me.edAn_nome.Location = New System.Drawing.Point(69, 54)
    Me.edAn_nome.Name = "edAn_nome"
    Me.edAn_nome.NTSDbField = ""
    Me.edAn_nome.NTSForzaVisZoom = False
    Me.edAn_nome.NTSOldValue = ""
    Me.edAn_nome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_nome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_nome.Properties.AutoHeight = False
    Me.edAn_nome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_nome.Properties.MaxLength = 65536
    Me.edAn_nome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_nome.Size = New System.Drawing.Size(289, 20)
    Me.edAn_nome.TabIndex = 582
    '
    'cbAn_sesso
    '
    Me.cbAn_sesso.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_sesso.DataSource = Nothing
    Me.cbAn_sesso.DisplayMember = ""
    Me.cbAn_sesso.Location = New System.Drawing.Point(69, 80)
    Me.cbAn_sesso.Name = "cbAn_sesso"
    Me.cbAn_sesso.NTSDbField = ""
    Me.cbAn_sesso.Properties.AutoHeight = False
    Me.cbAn_sesso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_sesso.Properties.DropDownRows = 30
    Me.cbAn_sesso.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_sesso.SelectedValue = ""
    Me.cbAn_sesso.Size = New System.Drawing.Size(100, 20)
    Me.cbAn_sesso.TabIndex = 593
    Me.cbAn_sesso.ValueMember = ""
    '
    'lbAn_nome
    '
    Me.lbAn_nome.AutoSize = True
    Me.lbAn_nome.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_nome.Location = New System.Drawing.Point(3, 57)
    Me.lbAn_nome.Name = "lbAn_nome"
    Me.lbAn_nome.NTSDbField = ""
    Me.lbAn_nome.Size = New System.Drawing.Size(34, 13)
    Me.lbAn_nome.TabIndex = 563
    Me.lbAn_nome.Text = "Nome"
    Me.lbAn_nome.Tooltip = ""
    Me.lbAn_nome.UseMnemonic = False
    '
    'fmNonresidenti
    '
    Me.fmNonresidenti.AllowDrop = True
    Me.fmNonresidenti.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmNonresidenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNonresidenti.Appearance.Options.UseBackColor = True
    Me.fmNonresidenti.Controls.Add(Me.edAn_estcodiso)
    Me.fmNonresidenti.Controls.Add(Me.lbXx_nazion1)
    Me.fmNonresidenti.Controls.Add(Me.edAn_estpariva)
    Me.fmNonresidenti.Controls.Add(Me.lbXx_nazion2)
    Me.fmNonresidenti.Controls.Add(Me.lbAn_estpariva)
    Me.fmNonresidenti.Controls.Add(Me.lbAn_estcodiso)
    Me.fmNonresidenti.Controls.Add(Me.edAn_codfisest)
    Me.fmNonresidenti.Controls.Add(Me.lbAn_codfisest)
    Me.fmNonresidenti.Controls.Add(Me.lbAn_nazion1)
    Me.fmNonresidenti.Controls.Add(Me.edAn_nazion2)
    Me.fmNonresidenti.Controls.Add(Me.lbAn_nazion2)
    Me.fmNonresidenti.Controls.Add(Me.edAn_nazion1)
    Me.fmNonresidenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNonresidenti.Location = New System.Drawing.Point(3, 190)
    Me.fmNonresidenti.Name = "fmNonresidenti"
    Me.fmNonresidenti.Size = New System.Drawing.Size(762, 125)
    Me.fmNonresidenti.TabIndex = 599
    Me.fmNonresidenti.Text = "Non residenti"
    '
    'edAn_estcodiso
    '
    Me.edAn_estcodiso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_estcodiso.EditValue = ""
    Me.edAn_estcodiso.Location = New System.Drawing.Point(171, 23)
    Me.edAn_estcodiso.Name = "edAn_estcodiso"
    Me.edAn_estcodiso.NTSDbField = ""
    Me.edAn_estcodiso.NTSForzaVisZoom = False
    Me.edAn_estcodiso.NTSOldValue = ""
    Me.edAn_estcodiso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_estcodiso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_estcodiso.Properties.AutoHeight = False
    Me.edAn_estcodiso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_estcodiso.Properties.MaxLength = 65536
    Me.edAn_estcodiso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_estcodiso.Size = New System.Drawing.Size(100, 20)
    Me.edAn_estcodiso.TabIndex = 588
    '
    'lbXx_nazion1
    '
    Me.lbXx_nazion1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_nazion1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_nazion1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_nazion1.Location = New System.Drawing.Point(277, 75)
    Me.lbXx_nazion1.Name = "lbXx_nazion1"
    Me.lbXx_nazion1.NTSDbField = ""
    Me.lbXx_nazion1.Size = New System.Drawing.Size(480, 20)
    Me.lbXx_nazion1.TabIndex = 597
    Me.lbXx_nazion1.Text = "xx_nazion1"
    Me.lbXx_nazion1.Tooltip = ""
    Me.lbXx_nazion1.UseMnemonic = False
    '
    'edAn_estpariva
    '
    Me.edAn_estpariva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_estpariva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_estpariva.EditValue = ""
    Me.edAn_estpariva.Location = New System.Drawing.Point(542, 23)
    Me.edAn_estpariva.Name = "edAn_estpariva"
    Me.edAn_estpariva.NTSDbField = ""
    Me.edAn_estpariva.NTSForzaVisZoom = False
    Me.edAn_estpariva.NTSOldValue = ""
    Me.edAn_estpariva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_estpariva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_estpariva.Properties.AutoHeight = False
    Me.edAn_estpariva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_estpariva.Properties.MaxLength = 65536
    Me.edAn_estpariva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_estpariva.Size = New System.Drawing.Size(215, 20)
    Me.edAn_estpariva.TabIndex = 589
    '
    'lbXx_nazion2
    '
    Me.lbXx_nazion2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_nazion2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_nazion2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_nazion2.Location = New System.Drawing.Point(277, 100)
    Me.lbXx_nazion2.Name = "lbXx_nazion2"
    Me.lbXx_nazion2.NTSDbField = ""
    Me.lbXx_nazion2.Size = New System.Drawing.Size(480, 20)
    Me.lbXx_nazion2.TabIndex = 598
    Me.lbXx_nazion2.Text = "xx_nazion2"
    Me.lbXx_nazion2.Tooltip = ""
    Me.lbXx_nazion2.UseMnemonic = False
    '
    'lbAn_estpariva
    '
    Me.lbAn_estpariva.AutoSize = True
    Me.lbAn_estpariva.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_estpariva.Location = New System.Drawing.Point(374, 26)
    Me.lbAn_estpariva.Name = "lbAn_estpariva"
    Me.lbAn_estpariva.NTSDbField = ""
    Me.lbAn_estpariva.Size = New System.Drawing.Size(103, 13)
    Me.lbAn_estpariva.TabIndex = 570
    Me.lbAn_estpariva.Text = "Id. IVA stato estero"
    Me.lbAn_estpariva.Tooltip = ""
    Me.lbAn_estpariva.UseMnemonic = False
    '
    'lbAn_estcodiso
    '
    Me.lbAn_estcodiso.AutoSize = True
    Me.lbAn_estcodiso.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_estcodiso.Location = New System.Drawing.Point(3, 26)
    Me.lbAn_estcodiso.Name = "lbAn_estcodiso"
    Me.lbAn_estcodiso.NTSDbField = ""
    Me.lbAn_estcodiso.Size = New System.Drawing.Size(110, 13)
    Me.lbAn_estcodiso.TabIndex = 569
    Me.lbAn_estcodiso.Text = "Cod.ISO stato estero"
    Me.lbAn_estcodiso.Tooltip = ""
    Me.lbAn_estcodiso.UseMnemonic = False
    '
    'edAn_codfisest
    '
    Me.edAn_codfisest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codfisest.EditValue = ""
    Me.edAn_codfisest.Location = New System.Drawing.Point(171, 49)
    Me.edAn_codfisest.Name = "edAn_codfisest"
    Me.edAn_codfisest.NTSDbField = ""
    Me.edAn_codfisest.NTSForzaVisZoom = False
    Me.edAn_codfisest.NTSOldValue = ""
    Me.edAn_codfisest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codfisest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codfisest.Properties.AutoHeight = False
    Me.edAn_codfisest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codfisest.Properties.MaxLength = 65536
    Me.edAn_codfisest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codfisest.Size = New System.Drawing.Size(233, 20)
    Me.edAn_codfisest.TabIndex = 573
    '
    'lbAn_codfisest
    '
    Me.lbAn_codfisest.AutoSize = True
    Me.lbAn_codfisest.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codfisest.Location = New System.Drawing.Point(3, 52)
    Me.lbAn_codfisest.Name = "lbAn_codfisest"
    Me.lbAn_codfisest.NTSDbField = ""
    Me.lbAn_codfisest.Size = New System.Drawing.Size(78, 13)
    Me.lbAn_codfisest.TabIndex = 554
    Me.lbAn_codfisest.Text = "Id. fisc. estero"
    Me.lbAn_codfisest.Tooltip = ""
    Me.lbAn_codfisest.UseMnemonic = False
    '
    'lbAn_nazion1
    '
    Me.lbAn_nazion1.AutoSize = True
    Me.lbAn_nazion1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_nazion1.Location = New System.Drawing.Point(3, 78)
    Me.lbAn_nazion1.Name = "lbAn_nazion1"
    Me.lbAn_nazion1.NTSDbField = ""
    Me.lbAn_nazion1.Size = New System.Drawing.Size(77, 13)
    Me.lbAn_nazion1.TabIndex = 565
    Me.lbAn_nazion1.Text = "Cod. nazion. 1"
    Me.lbAn_nazion1.Tooltip = ""
    Me.lbAn_nazion1.UseMnemonic = False
    '
    'edAn_nazion2
    '
    Me.edAn_nazion2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_nazion2.EditValue = ""
    Me.edAn_nazion2.Location = New System.Drawing.Point(171, 100)
    Me.edAn_nazion2.Name = "edAn_nazion2"
    Me.edAn_nazion2.NTSDbField = ""
    Me.edAn_nazion2.NTSForzaVisZoom = False
    Me.edAn_nazion2.NTSOldValue = ""
    Me.edAn_nazion2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_nazion2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_nazion2.Properties.AutoHeight = False
    Me.edAn_nazion2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_nazion2.Properties.MaxLength = 65536
    Me.edAn_nazion2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_nazion2.Size = New System.Drawing.Size(100, 20)
    Me.edAn_nazion2.TabIndex = 585
    '
    'lbAn_nazion2
    '
    Me.lbAn_nazion2.AutoSize = True
    Me.lbAn_nazion2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_nazion2.Location = New System.Drawing.Point(3, 103)
    Me.lbAn_nazion2.Name = "lbAn_nazion2"
    Me.lbAn_nazion2.NTSDbField = ""
    Me.lbAn_nazion2.Size = New System.Drawing.Size(77, 13)
    Me.lbAn_nazion2.TabIndex = 566
    Me.lbAn_nazion2.Text = "Cod. nazion. 2"
    Me.lbAn_nazion2.Tooltip = ""
    Me.lbAn_nazion2.UseMnemonic = False
    '
    'edAn_nazion1
    '
    Me.edAn_nazion1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_nazion1.EditValue = ""
    Me.edAn_nazion1.Location = New System.Drawing.Point(171, 75)
    Me.edAn_nazion1.Name = "edAn_nazion1"
    Me.edAn_nazion1.NTSDbField = ""
    Me.edAn_nazion1.NTSForzaVisZoom = False
    Me.edAn_nazion1.NTSOldValue = ""
    Me.edAn_nazion1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_nazion1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_nazion1.Properties.AutoHeight = False
    Me.edAn_nazion1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_nazion1.Properties.MaxLength = 65536
    Me.edAn_nazion1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_nazion1.Size = New System.Drawing.Size(100, 20)
    Me.edAn_nazion1.TabIndex = 584
    '
    'fmNascita
    '
    Me.fmNascita.AllowDrop = True
    Me.fmNascita.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmNascita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNascita.Appearance.Options.UseBackColor = True
    Me.fmNascita.Controls.Add(Me.lbXx_stanasc)
    Me.fmNascita.Controls.Add(Me.lbXx_codcomn)
    Me.fmNascita.Controls.Add(Me.lbAn_datnasc)
    Me.fmNascita.Controls.Add(Me.edAn_datnasc)
    Me.fmNascita.Controls.Add(Me.lbAn_pronasc)
    Me.fmNascita.Controls.Add(Me.lbAn_citnasc)
    Me.fmNascita.Controls.Add(Me.edAn_pronasc)
    Me.fmNascita.Controls.Add(Me.edAn_citnasc)
    Me.fmNascita.Controls.Add(Me.lbAn_codcomn)
    Me.fmNascita.Controls.Add(Me.edAn_codcomn)
    Me.fmNascita.Controls.Add(Me.lbAn_stanasc)
    Me.fmNascita.Controls.Add(Me.edAn_stanasc)
    Me.fmNascita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNascita.Location = New System.Drawing.Point(374, 12)
    Me.fmNascita.Name = "fmNascita"
    Me.fmNascita.Size = New System.Drawing.Size(391, 158)
    Me.fmNascita.TabIndex = 596
    Me.fmNascita.Text = "Estremi nascita/costituzione"
    '
    'lbXx_stanasc
    '
    Me.lbXx_stanasc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_stanasc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_stanasc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_stanasc.Location = New System.Drawing.Point(171, 80)
    Me.lbXx_stanasc.Name = "lbXx_stanasc"
    Me.lbXx_stanasc.NTSDbField = ""
    Me.lbXx_stanasc.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_stanasc.TabIndex = 596
    Me.lbXx_stanasc.Text = "xx_stanasc"
    Me.lbXx_stanasc.Tooltip = ""
    Me.lbXx_stanasc.UseMnemonic = False
    '
    'lbXx_codcomn
    '
    Me.lbXx_codcomn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_codcomn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcomn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcomn.Location = New System.Drawing.Point(171, 54)
    Me.lbXx_codcomn.Name = "lbXx_codcomn"
    Me.lbXx_codcomn.NTSDbField = ""
    Me.lbXx_codcomn.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codcomn.TabIndex = 595
    Me.lbXx_codcomn.Text = "xx_codcomn"
    Me.lbXx_codcomn.Tooltip = ""
    Me.lbXx_codcomn.UseMnemonic = False
    '
    'lbAn_datnasc
    '
    Me.lbAn_datnasc.AutoSize = True
    Me.lbAn_datnasc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_datnasc.Location = New System.Drawing.Point(3, 31)
    Me.lbAn_datnasc.Name = "lbAn_datnasc"
    Me.lbAn_datnasc.NTSDbField = ""
    Me.lbAn_datnasc.Size = New System.Drawing.Size(30, 13)
    Me.lbAn_datnasc.TabIndex = 591
    Me.lbAn_datnasc.Text = "Data"
    Me.lbAn_datnasc.Tooltip = ""
    Me.lbAn_datnasc.UseMnemonic = False
    '
    'edAn_datnasc
    '
    Me.edAn_datnasc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_datnasc.EditValue = "01/01/2000"
    Me.edAn_datnasc.Location = New System.Drawing.Point(102, 28)
    Me.edAn_datnasc.Name = "edAn_datnasc"
    Me.edAn_datnasc.NTSDbField = ""
    Me.edAn_datnasc.NTSForzaVisZoom = False
    Me.edAn_datnasc.NTSOldValue = ""
    Me.edAn_datnasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_datnasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_datnasc.Properties.AutoHeight = False
    Me.edAn_datnasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_datnasc.Properties.MaxLength = 65536
    Me.edAn_datnasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_datnasc.Size = New System.Drawing.Size(100, 20)
    Me.edAn_datnasc.TabIndex = 594
    '
    'lbAn_pronasc
    '
    Me.lbAn_pronasc.AutoSize = True
    Me.lbAn_pronasc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_pronasc.Location = New System.Drawing.Point(3, 134)
    Me.lbAn_pronasc.Name = "lbAn_pronasc"
    Me.lbAn_pronasc.NTSDbField = ""
    Me.lbAn_pronasc.Size = New System.Drawing.Size(50, 13)
    Me.lbAn_pronasc.TabIndex = 552
    Me.lbAn_pronasc.Text = "Provincia"
    Me.lbAn_pronasc.Tooltip = ""
    Me.lbAn_pronasc.UseMnemonic = False
    '
    'lbAn_citnasc
    '
    Me.lbAn_citnasc.AutoSize = True
    Me.lbAn_citnasc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_citnasc.Location = New System.Drawing.Point(3, 109)
    Me.lbAn_citnasc.Name = "lbAn_citnasc"
    Me.lbAn_citnasc.NTSDbField = ""
    Me.lbAn_citnasc.Size = New System.Drawing.Size(91, 13)
    Me.lbAn_citnasc.TabIndex = 592
    Me.lbAn_citnasc.Text = "Descr. città/stato"
    Me.lbAn_citnasc.Tooltip = ""
    Me.lbAn_citnasc.UseMnemonic = False
    '
    'edAn_pronasc
    '
    Me.edAn_pronasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_pronasc.EditValue = ""
    Me.edAn_pronasc.Location = New System.Drawing.Point(102, 132)
    Me.edAn_pronasc.Name = "edAn_pronasc"
    Me.edAn_pronasc.NTSDbField = ""
    Me.edAn_pronasc.NTSForzaVisZoom = False
    Me.edAn_pronasc.NTSOldValue = ""
    Me.edAn_pronasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_pronasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_pronasc.Properties.AutoHeight = False
    Me.edAn_pronasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_pronasc.Properties.MaxLength = 65536
    Me.edAn_pronasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_pronasc.Size = New System.Drawing.Size(63, 20)
    Me.edAn_pronasc.TabIndex = 571
    '
    'edAn_citnasc
    '
    Me.edAn_citnasc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_citnasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_citnasc.EditValue = ""
    Me.edAn_citnasc.Location = New System.Drawing.Point(102, 106)
    Me.edAn_citnasc.Name = "edAn_citnasc"
    Me.edAn_citnasc.NTSDbField = ""
    Me.edAn_citnasc.NTSForzaVisZoom = False
    Me.edAn_citnasc.NTSOldValue = ""
    Me.edAn_citnasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_citnasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_citnasc.Properties.AutoHeight = False
    Me.edAn_citnasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_citnasc.Properties.MaxLength = 65536
    Me.edAn_citnasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_citnasc.Size = New System.Drawing.Size(284, 20)
    Me.edAn_citnasc.TabIndex = 595
    '
    'lbAn_codcomn
    '
    Me.lbAn_codcomn.AutoSize = True
    Me.lbAn_codcomn.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codcomn.Location = New System.Drawing.Point(3, 57)
    Me.lbAn_codcomn.Name = "lbAn_codcomn"
    Me.lbAn_codcomn.NTSDbField = ""
    Me.lbAn_codcomn.Size = New System.Drawing.Size(70, 13)
    Me.lbAn_codcomn.TabIndex = 564
    Me.lbAn_codcomn.Text = "Cod. comune"
    Me.lbAn_codcomn.Tooltip = ""
    Me.lbAn_codcomn.UseMnemonic = False
    '
    'edAn_codcomn
    '
    Me.edAn_codcomn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codcomn.EditValue = ""
    Me.edAn_codcomn.Location = New System.Drawing.Point(102, 54)
    Me.edAn_codcomn.Name = "edAn_codcomn"
    Me.edAn_codcomn.NTSDbField = ""
    Me.edAn_codcomn.NTSForzaVisZoom = False
    Me.edAn_codcomn.NTSOldValue = ""
    Me.edAn_codcomn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codcomn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codcomn.Properties.AutoHeight = False
    Me.edAn_codcomn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codcomn.Properties.MaxLength = 65536
    Me.edAn_codcomn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codcomn.Size = New System.Drawing.Size(63, 20)
    Me.edAn_codcomn.TabIndex = 583
    '
    'lbAn_stanasc
    '
    Me.lbAn_stanasc.AutoSize = True
    Me.lbAn_stanasc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_stanasc.Location = New System.Drawing.Point(3, 83)
    Me.lbAn_stanasc.Name = "lbAn_stanasc"
    Me.lbAn_stanasc.NTSDbField = ""
    Me.lbAn_stanasc.Size = New System.Drawing.Size(92, 13)
    Me.lbAn_stanasc.TabIndex = 553
    Me.lbAn_stanasc.Text = "Cod. stato estero"
    Me.lbAn_stanasc.Tooltip = ""
    Me.lbAn_stanasc.UseMnemonic = False
    '
    'edAn_stanasc
    '
    Me.edAn_stanasc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_stanasc.EditValue = ""
    Me.edAn_stanasc.Location = New System.Drawing.Point(102, 80)
    Me.edAn_stanasc.Name = "edAn_stanasc"
    Me.edAn_stanasc.NTSDbField = ""
    Me.edAn_stanasc.NTSForzaVisZoom = False
    Me.edAn_stanasc.NTSOldValue = ""
    Me.edAn_stanasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_stanasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_stanasc.Properties.AutoHeight = False
    Me.edAn_stanasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_stanasc.Properties.MaxLength = 65536
    Me.edAn_stanasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_stanasc.Size = New System.Drawing.Size(63, 20)
    Me.edAn_stanasc.TabIndex = 572
    '
    'ckAn_condom
    '
    Me.ckAn_condom.Location = New System.Drawing.Point(195, 141)
    Me.ckAn_condom.Name = "ckAn_condom"
    Me.ckAn_condom.NTSCheckValue = "S"
    Me.ckAn_condom.NTSUnCheckValue = "N"
    Me.ckAn_condom.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_condom.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_condom.Properties.AutoHeight = False
    Me.ckAn_condom.Properties.Caption = "Condominio"
    Me.ckAn_condom.Size = New System.Drawing.Size(88, 19)
    Me.ckAn_condom.TabIndex = 590
    '
    'ckAn_soggresi
    '
    Me.ckAn_soggresi.Location = New System.Drawing.Point(6, 141)
    Me.ckAn_soggresi.Name = "ckAn_soggresi"
    Me.ckAn_soggresi.NTSCheckValue = "S"
    Me.ckAn_soggresi.NTSUnCheckValue = "N"
    Me.ckAn_soggresi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_soggresi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_soggresi.Properties.AutoHeight = False
    Me.ckAn_soggresi.Properties.Caption = "Residente"
    Me.ckAn_soggresi.Size = New System.Drawing.Size(78, 19)
    Me.ckAn_soggresi.TabIndex = 548
    '
    'ckAn_profes
    '
    Me.ckAn_profes.Location = New System.Drawing.Point(90, 141)
    Me.ckAn_profes.Name = "ckAn_profes"
    Me.ckAn_profes.NTSCheckValue = "S"
    Me.ckAn_profes.NTSUnCheckValue = "N"
    Me.ckAn_profes.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_profes.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_profes.Properties.AutoHeight = False
    Me.ckAn_profes.Properties.Caption = "Professionista"
    Me.ckAn_profes.Size = New System.Drawing.Size(100, 19)
    Me.ckAn_profes.TabIndex = 532
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage3.Text = "&3 - Altri dati"
    '
    'pnPag3
    '
    Me.pnPag3.AllowDrop = True
    Me.pnPag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag3.Appearance.Options.UseBackColor = True
    Me.pnPag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag3.Controls.Add(Me.fmPosition)
    Me.pnPag3.Controls.Add(Me.fmConai)
    Me.pnPag3.Controls.Add(Me.fmAcquisizione)
    Me.pnPag3.Controls.Add(Me.fmWeb)
    Me.pnPag3.Controls.Add(Me.pnDatiSx)
    Me.pnPag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag3.Location = New System.Drawing.Point(0, 0)
    Me.pnPag3.Name = "pnPag3"
    Me.pnPag3.NTSActiveTrasparency = True
    Me.pnPag3.Size = New System.Drawing.Size(771, 317)
    Me.pnPag3.TabIndex = 566
    Me.pnPag3.Text = "NtsPanel1"
    '
    'fmPosition
    '
    Me.fmPosition.AllowDrop = True
    Me.fmPosition.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPosition.Appearance.Options.UseBackColor = True
    Me.fmPosition.Controls.Add(Me.lbAn_latitud)
    Me.fmPosition.Controls.Add(Me.edAn_latitud)
    Me.fmPosition.Controls.Add(Me.lbAn_longitud)
    Me.fmPosition.Controls.Add(Me.edAn_longitud)
    Me.fmPosition.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPosition.Location = New System.Drawing.Point(411, 243)
    Me.fmPosition.Name = "fmPosition"
    Me.fmPosition.Size = New System.Drawing.Size(354, 71)
    Me.fmPosition.TabIndex = 632
    Me.fmPosition.Text = "Coordinate geografiche"
    '
    'lbAn_latitud
    '
    Me.lbAn_latitud.AutoSize = True
    Me.lbAn_latitud.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_latitud.Location = New System.Drawing.Point(5, 26)
    Me.lbAn_latitud.Name = "lbAn_latitud"
    Me.lbAn_latitud.NTSDbField = ""
    Me.lbAn_latitud.Size = New System.Drawing.Size(54, 13)
    Me.lbAn_latitud.TabIndex = 568
    Me.lbAn_latitud.Text = "Latitudine"
    Me.lbAn_latitud.Tooltip = ""
    Me.lbAn_latitud.UseMnemonic = False
    '
    'edAn_latitud
    '
    Me.edAn_latitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_latitud.EditValue = ""
    Me.edAn_latitud.Location = New System.Drawing.Point(137, 23)
    Me.edAn_latitud.Name = "edAn_latitud"
    Me.edAn_latitud.NTSDbField = ""
    Me.edAn_latitud.NTSForzaVisZoom = False
    Me.edAn_latitud.NTSOldValue = ""
    Me.edAn_latitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_latitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_latitud.Properties.AutoHeight = False
    Me.edAn_latitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_latitud.Properties.MaxLength = 65536
    Me.edAn_latitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_latitud.Size = New System.Drawing.Size(212, 20)
    Me.edAn_latitud.TabIndex = 570
    '
    'lbAn_longitud
    '
    Me.lbAn_longitud.AutoSize = True
    Me.lbAn_longitud.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_longitud.Location = New System.Drawing.Point(5, 52)
    Me.lbAn_longitud.Name = "lbAn_longitud"
    Me.lbAn_longitud.NTSDbField = ""
    Me.lbAn_longitud.Size = New System.Drawing.Size(62, 13)
    Me.lbAn_longitud.TabIndex = 569
    Me.lbAn_longitud.Text = "Longitudine"
    Me.lbAn_longitud.Tooltip = ""
    Me.lbAn_longitud.UseMnemonic = False
    '
    'edAn_longitud
    '
    Me.edAn_longitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_longitud.EditValue = ""
    Me.edAn_longitud.Location = New System.Drawing.Point(137, 48)
    Me.edAn_longitud.Name = "edAn_longitud"
    Me.edAn_longitud.NTSDbField = ""
    Me.edAn_longitud.NTSForzaVisZoom = False
    Me.edAn_longitud.NTSOldValue = ""
    Me.edAn_longitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_longitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_longitud.Properties.AutoHeight = False
    Me.edAn_longitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_longitud.Properties.MaxLength = 65536
    Me.edAn_longitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_longitud.Size = New System.Drawing.Size(212, 20)
    Me.edAn_longitud.TabIndex = 571
    '
    'fmConai
    '
    Me.fmConai.AllowDrop = True
    Me.fmConai.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmConai.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmConai.Appearance.Options.UseBackColor = True
    Me.fmConai.Controls.Add(Me.cmdConaiArt)
    Me.fmConai.Controls.Add(Me.lbAn_perescon)
    Me.fmConai.Controls.Add(Me.edAn_perescon)
    Me.fmConai.Controls.Add(Me.cbAn_gescon)
    Me.fmConai.Controls.Add(Me.lbAn_gescon)
    Me.fmConai.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmConai.Location = New System.Drawing.Point(411, 3)
    Me.fmConai.Name = "fmConai"
    Me.fmConai.Size = New System.Drawing.Size(354, 75)
    Me.fmConai.TabIndex = 631
    Me.fmConai.Text = "Conai"
    '
    'cmdConaiArt
    '
    Me.cmdConaiArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConaiArt.ImagePath = ""
    Me.cmdConaiArt.ImageText = ""
    Me.cmdConaiArt.Location = New System.Drawing.Point(196, 47)
    Me.cmdConaiArt.Name = "cmdConaiArt"
    Me.cmdConaiArt.NTSContextMenu = Nothing
    Me.cmdConaiArt.Size = New System.Drawing.Size(153, 22)
    Me.cmdConaiArt.TabIndex = 628
    Me.cmdConaiArt.Text = "% esenz. per tipo materiale"
    '
    'lbAn_perescon
    '
    Me.lbAn_perescon.AutoSize = True
    Me.lbAn_perescon.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_perescon.Location = New System.Drawing.Point(5, 50)
    Me.lbAn_perescon.Name = "lbAn_perescon"
    Me.lbAn_perescon.NTSDbField = ""
    Me.lbAn_perescon.Size = New System.Drawing.Size(126, 13)
    Me.lbAn_perescon.TabIndex = 626
    Me.lbAn_perescon.Text = "Percentuale di esenzione"
    Me.lbAn_perescon.Tooltip = ""
    Me.lbAn_perescon.UseMnemonic = False
    '
    'edAn_perescon
    '
    Me.edAn_perescon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_perescon.EditValue = "0"
    Me.edAn_perescon.Location = New System.Drawing.Point(137, 47)
    Me.edAn_perescon.Name = "edAn_perescon"
    Me.edAn_perescon.NTSDbField = ""
    Me.edAn_perescon.NTSFormat = "0"
    Me.edAn_perescon.NTSForzaVisZoom = False
    Me.edAn_perescon.NTSOldValue = ""
    Me.edAn_perescon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_perescon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_perescon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_perescon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_perescon.Properties.AutoHeight = False
    Me.edAn_perescon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_perescon.Properties.MaxLength = 65536
    Me.edAn_perescon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_perescon.Size = New System.Drawing.Size(53, 20)
    Me.edAn_perescon.TabIndex = 627
    '
    'cbAn_gescon
    '
    Me.cbAn_gescon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbAn_gescon.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_gescon.DataSource = Nothing
    Me.cbAn_gescon.DisplayMember = ""
    Me.cbAn_gescon.Location = New System.Drawing.Point(137, 23)
    Me.cbAn_gescon.Name = "cbAn_gescon"
    Me.cbAn_gescon.NTSDbField = ""
    Me.cbAn_gescon.Properties.AutoHeight = False
    Me.cbAn_gescon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_gescon.Properties.DropDownRows = 30
    Me.cbAn_gescon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_gescon.SelectedValue = ""
    Me.cbAn_gescon.Size = New System.Drawing.Size(212, 20)
    Me.cbAn_gescon.TabIndex = 606
    Me.cbAn_gescon.ValueMember = ""
    '
    'lbAn_gescon
    '
    Me.lbAn_gescon.AutoSize = True
    Me.lbAn_gescon.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_gescon.Location = New System.Drawing.Point(5, 26)
    Me.lbAn_gescon.Name = "lbAn_gescon"
    Me.lbAn_gescon.NTSDbField = ""
    Me.lbAn_gescon.Size = New System.Drawing.Size(66, 13)
    Me.lbAn_gescon.TabIndex = 605
    Me.lbAn_gescon.Text = "Applicazione"
    Me.lbAn_gescon.Tooltip = ""
    Me.lbAn_gescon.UseMnemonic = False
    '
    'fmAcquisizione
    '
    Me.fmAcquisizione.AllowDrop = True
    Me.fmAcquisizione.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmAcquisizione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAcquisizione.Appearance.Options.UseBackColor = True
    Me.fmAcquisizione.Controls.Add(Me.edAn_contatt)
    Me.fmAcquisizione.Controls.Add(Me.lbAn_contatt)
    Me.fmAcquisizione.Controls.Add(Me.lbAn_dtaper)
    Me.fmAcquisizione.Controls.Add(Me.edAn_dtaper)
    Me.fmAcquisizione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAcquisizione.Location = New System.Drawing.Point(411, 81)
    Me.fmAcquisizione.Name = "fmAcquisizione"
    Me.fmAcquisizione.Size = New System.Drawing.Size(354, 55)
    Me.fmAcquisizione.TabIndex = 603
    Me.fmAcquisizione.Text = "Acquisizione"
    '
    'edAn_contatt
    '
    Me.edAn_contatt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_contatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_contatt.EditValue = ""
    Me.edAn_contatt.Location = New System.Drawing.Point(228, 26)
    Me.edAn_contatt.Name = "edAn_contatt"
    Me.edAn_contatt.NTSDbField = ""
    Me.edAn_contatt.NTSForzaVisZoom = False
    Me.edAn_contatt.NTSOldValue = ""
    Me.edAn_contatt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_contatt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_contatt.Properties.AutoHeight = False
    Me.edAn_contatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_contatt.Properties.MaxLength = 65536
    Me.edAn_contatt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_contatt.Size = New System.Drawing.Size(121, 20)
    Me.edAn_contatt.TabIndex = 598
    '
    'lbAn_contatt
    '
    Me.lbAn_contatt.AutoSize = True
    Me.lbAn_contatt.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contatt.Location = New System.Drawing.Point(172, 29)
    Me.lbAn_contatt.Name = "lbAn_contatt"
    Me.lbAn_contatt.NTSDbField = ""
    Me.lbAn_contatt.Size = New System.Drawing.Size(50, 13)
    Me.lbAn_contatt.TabIndex = 597
    Me.lbAn_contatt.Text = "Contatto"
    Me.lbAn_contatt.Tooltip = ""
    Me.lbAn_contatt.UseMnemonic = False
    '
    'lbAn_dtaper
    '
    Me.lbAn_dtaper.AutoSize = True
    Me.lbAn_dtaper.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_dtaper.Location = New System.Drawing.Point(8, 29)
    Me.lbAn_dtaper.Name = "lbAn_dtaper"
    Me.lbAn_dtaper.NTSDbField = ""
    Me.lbAn_dtaper.Size = New System.Drawing.Size(30, 13)
    Me.lbAn_dtaper.TabIndex = 595
    Me.lbAn_dtaper.Text = "Data"
    Me.lbAn_dtaper.Tooltip = ""
    Me.lbAn_dtaper.UseMnemonic = False
    '
    'edAn_dtaper
    '
    Me.edAn_dtaper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_dtaper.EditValue = "01/01/2000"
    Me.edAn_dtaper.Location = New System.Drawing.Point(54, 26)
    Me.edAn_dtaper.Name = "edAn_dtaper"
    Me.edAn_dtaper.NTSDbField = ""
    Me.edAn_dtaper.NTSForzaVisZoom = False
    Me.edAn_dtaper.NTSOldValue = ""
    Me.edAn_dtaper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_dtaper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_dtaper.Properties.AutoHeight = False
    Me.edAn_dtaper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_dtaper.Properties.MaxLength = 65536
    Me.edAn_dtaper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_dtaper.Size = New System.Drawing.Size(100, 20)
    Me.edAn_dtaper.TabIndex = 596
    '
    'fmWeb
    '
    Me.fmWeb.AllowDrop = True
    Me.fmWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmWeb.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmWeb.Appearance.Options.UseBackColor = True
    Me.fmWeb.Controls.Add(Me.lbAn_webpwd)
    Me.fmWeb.Controls.Add(Me.edAn_webpwd)
    Me.fmWeb.Controls.Add(Me.lbAn_website)
    Me.fmWeb.Controls.Add(Me.edAn_website)
    Me.fmWeb.Controls.Add(Me.lbAn_webuid)
    Me.fmWeb.Controls.Add(Me.edAn_webuid)
    Me.fmWeb.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmWeb.Location = New System.Drawing.Point(411, 140)
    Me.fmWeb.Name = "fmWeb"
    Me.fmWeb.Size = New System.Drawing.Size(354, 100)
    Me.fmWeb.TabIndex = 596
    Me.fmWeb.Text = "Sito Web"
    '
    'lbAn_webpwd
    '
    Me.lbAn_webpwd.AutoSize = True
    Me.lbAn_webpwd.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_webpwd.Location = New System.Drawing.Point(1, 78)
    Me.lbAn_webpwd.Name = "lbAn_webpwd"
    Me.lbAn_webpwd.NTSDbField = ""
    Me.lbAn_webpwd.Size = New System.Drawing.Size(72, 13)
    Me.lbAn_webpwd.TabIndex = 572
    Me.lbAn_webpwd.Text = "Pwd sito Web"
    Me.lbAn_webpwd.Tooltip = ""
    Me.lbAn_webpwd.UseMnemonic = False
    '
    'edAn_webpwd
    '
    Me.edAn_webpwd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_webpwd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_webpwd.EditValue = ""
    Me.edAn_webpwd.Location = New System.Drawing.Point(137, 75)
    Me.edAn_webpwd.Name = "edAn_webpwd"
    Me.edAn_webpwd.NTSDbField = ""
    Me.edAn_webpwd.NTSForzaVisZoom = False
    Me.edAn_webpwd.NTSOldValue = ""
    Me.edAn_webpwd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_webpwd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_webpwd.Properties.AutoHeight = False
    Me.edAn_webpwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_webpwd.Properties.MaxLength = 65536
    Me.edAn_webpwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_webpwd.Size = New System.Drawing.Size(212, 20)
    Me.edAn_webpwd.TabIndex = 573
    '
    'lbAn_website
    '
    Me.lbAn_website.AutoSize = True
    Me.lbAn_website.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_website.Location = New System.Drawing.Point(1, 26)
    Me.lbAn_website.Name = "lbAn_website"
    Me.lbAn_website.NTSDbField = ""
    Me.lbAn_website.Size = New System.Drawing.Size(50, 13)
    Me.lbAn_website.TabIndex = 568
    Me.lbAn_website.Text = "Sito Web"
    Me.lbAn_website.Tooltip = ""
    Me.lbAn_website.UseMnemonic = False
    '
    'edAn_website
    '
    Me.edAn_website.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_website.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_website.EditValue = ""
    Me.edAn_website.Location = New System.Drawing.Point(137, 23)
    Me.edAn_website.Name = "edAn_website"
    Me.edAn_website.NTSDbField = ""
    Me.edAn_website.NTSForzaVisZoom = False
    Me.edAn_website.NTSOldValue = ""
    Me.edAn_website.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_website.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_website.Properties.AutoHeight = False
    Me.edAn_website.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_website.Properties.MaxLength = 65536
    Me.edAn_website.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_website.Size = New System.Drawing.Size(212, 20)
    Me.edAn_website.TabIndex = 570
    '
    'lbAn_webuid
    '
    Me.lbAn_webuid.AutoSize = True
    Me.lbAn_webuid.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_webuid.Location = New System.Drawing.Point(1, 52)
    Me.lbAn_webuid.Name = "lbAn_webuid"
    Me.lbAn_webuid.NTSDbField = ""
    Me.lbAn_webuid.Size = New System.Drawing.Size(85, 13)
    Me.lbAn_webuid.TabIndex = 569
    Me.lbAn_webuid.Text = "UserID sito Web"
    Me.lbAn_webuid.Tooltip = ""
    Me.lbAn_webuid.UseMnemonic = False
    '
    'edAn_webuid
    '
    Me.edAn_webuid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_webuid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_webuid.EditValue = ""
    Me.edAn_webuid.Location = New System.Drawing.Point(137, 49)
    Me.edAn_webuid.Name = "edAn_webuid"
    Me.edAn_webuid.NTSDbField = ""
    Me.edAn_webuid.NTSForzaVisZoom = False
    Me.edAn_webuid.NTSOldValue = ""
    Me.edAn_webuid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_webuid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_webuid.Properties.AutoHeight = False
    Me.edAn_webuid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_webuid.Properties.MaxLength = 65536
    Me.edAn_webuid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_webuid.Size = New System.Drawing.Size(212, 20)
    Me.edAn_webuid.TabIndex = 571
    '
    'pnDatiSx
    '
    Me.pnDatiSx.AllowDrop = True
    Me.pnDatiSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDatiSx.Appearance.Options.UseBackColor = True
    Me.pnDatiSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDatiSx.Controls.Add(Me.fmTesoreria)
    Me.pnDatiSx.Controls.Add(Me.edAn_rating)
    Me.pnDatiSx.Controls.Add(Me.lbAn_rating)
    Me.pnDatiSx.Controls.Add(Me.lbAn_agcontrop)
    Me.pnDatiSx.Controls.Add(Me.lbXx_zona)
    Me.pnDatiSx.Controls.Add(Me.edAn_agcontrop)
    Me.pnDatiSx.Controls.Add(Me.edAn_zona)
    Me.pnDatiSx.Controls.Add(Me.edAn_codling)
    Me.pnDatiSx.Controls.Add(Me.lbAn_zona)
    Me.pnDatiSx.Controls.Add(Me.lbAn_codling)
    Me.pnDatiSx.Controls.Add(Me.lbXx_categ)
    Me.pnDatiSx.Controls.Add(Me.lbXx_codling)
    Me.pnDatiSx.Controls.Add(Me.edAn_categ)
    Me.pnDatiSx.Controls.Add(Me.lbAn_categ)
    Me.pnDatiSx.Controls.Add(Me.lbXx_agente)
    Me.pnDatiSx.Controls.Add(Me.edAn_agente)
    Me.pnDatiSx.Controls.Add(Me.lbAn_agente)
    Me.pnDatiSx.Controls.Add(Me.lbAn_clascon)
    Me.pnDatiSx.Controls.Add(Me.lbXx_codcana)
    Me.pnDatiSx.Controls.Add(Me.edAn_clascon)
    Me.pnDatiSx.Controls.Add(Me.lbXx_clascon)
    Me.pnDatiSx.Controls.Add(Me.lbXx_claprov)
    Me.pnDatiSx.Controls.Add(Me.edAn_codcana)
    Me.pnDatiSx.Controls.Add(Me.lbAn_claprov)
    Me.pnDatiSx.Controls.Add(Me.edAn_claprov)
    Me.pnDatiSx.Controls.Add(Me.lbAn_codcana)
    Me.pnDatiSx.Controls.Add(Me.lbXx_agente2)
    Me.pnDatiSx.Controls.Add(Me.edAn_agente2)
    Me.pnDatiSx.Controls.Add(Me.lbAn_agente2)
    Me.pnDatiSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDatiSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnDatiSx.Location = New System.Drawing.Point(0, 0)
    Me.pnDatiSx.Name = "pnDatiSx"
    Me.pnDatiSx.NTSActiveTrasparency = True
    Me.pnDatiSx.Size = New System.Drawing.Size(409, 317)
    Me.pnDatiSx.TabIndex = 600
    Me.pnDatiSx.Text = "NtsPanel1"
    '
    'fmTesoreria
    '
    Me.fmTesoreria.AllowDrop = True
    Me.fmTesoreria.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTesoreria.Appearance.Options.UseBackColor = True
    Me.fmTesoreria.Controls.Add(Me.lbXx_codvfde)
    Me.fmTesoreria.Controls.Add(Me.cbAn_trating)
    Me.fmTesoreria.Controls.Add(Me.edAn_codvfde)
    Me.fmTesoreria.Controls.Add(Me.lbAn_codvfde)
    Me.fmTesoreria.Controls.Add(Me.lbAn_trating)
    Me.fmTesoreria.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTesoreria.Location = New System.Drawing.Point(4, 51)
    Me.fmTesoreria.Name = "fmTesoreria"
    Me.fmTesoreria.Size = New System.Drawing.Size(401, 68)
    Me.fmTesoreria.TabIndex = 629
    Me.fmTesoreria.Text = "Tesoreria e flussi finanziari"
    '
    'lbXx_codvfde
    '
    Me.lbXx_codvfde.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvfde.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvfde.Location = New System.Drawing.Point(201, 44)
    Me.lbXx_codvfde.Name = "lbXx_codvfde"
    Me.lbXx_codvfde.NTSDbField = ""
    Me.lbXx_codvfde.Size = New System.Drawing.Size(195, 20)
    Me.lbXx_codvfde.TabIndex = 622
    Me.lbXx_codvfde.Text = "lbXx_codvfde"
    Me.lbXx_codvfde.Tooltip = ""
    Me.lbXx_codvfde.UseMnemonic = False
    '
    'cbAn_trating
    '
    Me.cbAn_trating.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_trating.DataSource = Nothing
    Me.cbAn_trating.DisplayMember = ""
    Me.cbAn_trating.Location = New System.Drawing.Point(137, 22)
    Me.cbAn_trating.Name = "cbAn_trating"
    Me.cbAn_trating.NTSDbField = ""
    Me.cbAn_trating.Properties.AutoHeight = False
    Me.cbAn_trating.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_trating.Properties.DropDownRows = 30
    Me.cbAn_trating.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_trating.SelectedValue = ""
    Me.cbAn_trating.Size = New System.Drawing.Size(131, 20)
    Me.cbAn_trating.TabIndex = 3
    Me.cbAn_trating.ValueMember = ""
    '
    'edAn_codvfde
    '
    Me.edAn_codvfde.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codvfde.EditValue = ""
    Me.edAn_codvfde.Location = New System.Drawing.Point(137, 44)
    Me.edAn_codvfde.Name = "edAn_codvfde"
    Me.edAn_codvfde.NTSDbField = ""
    Me.edAn_codvfde.NTSForzaVisZoom = False
    Me.edAn_codvfde.NTSOldValue = ""
    Me.edAn_codvfde.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codvfde.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codvfde.Properties.AutoHeight = False
    Me.edAn_codvfde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codvfde.Properties.MaxLength = 65536
    Me.edAn_codvfde.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codvfde.Size = New System.Drawing.Size(61, 20)
    Me.edAn_codvfde.TabIndex = 2
    '
    'lbAn_codvfde
    '
    Me.lbAn_codvfde.AutoSize = True
    Me.lbAn_codvfde.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codvfde.Location = New System.Drawing.Point(3, 45)
    Me.lbAn_codvfde.Name = "lbAn_codvfde"
    Me.lbAn_codvfde.NTSDbField = ""
    Me.lbAn_codvfde.Size = New System.Drawing.Size(82, 13)
    Me.lbAn_codvfde.TabIndex = 1
    Me.lbAn_codvfde.Text = "Voce finanziaria"
    Me.lbAn_codvfde.Tooltip = ""
    Me.lbAn_codvfde.UseMnemonic = False
    '
    'lbAn_trating
    '
    Me.lbAn_trating.AutoSize = True
    Me.lbAn_trating.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_trating.Location = New System.Drawing.Point(4, 25)
    Me.lbAn_trating.Name = "lbAn_trating"
    Me.lbAn_trating.NTSDbField = ""
    Me.lbAn_trating.Size = New System.Drawing.Size(38, 13)
    Me.lbAn_trating.TabIndex = 0
    Me.lbAn_trating.Text = "Rating"
    Me.lbAn_trating.Tooltip = ""
    Me.lbAn_trating.UseMnemonic = False
    '
    'edAn_rating
    '
    Me.edAn_rating.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rating.EditValue = ""
    Me.edAn_rating.Location = New System.Drawing.Point(141, 4)
    Me.edAn_rating.Name = "edAn_rating"
    Me.edAn_rating.NTSDbField = ""
    Me.edAn_rating.NTSForzaVisZoom = False
    Me.edAn_rating.NTSOldValue = ""
    Me.edAn_rating.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_rating.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_rating.Properties.AutoHeight = False
    Me.edAn_rating.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_rating.Properties.MaxLength = 65536
    Me.edAn_rating.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_rating.Size = New System.Drawing.Size(53, 20)
    Me.edAn_rating.TabIndex = 605
    '
    'lbAn_rating
    '
    Me.lbAn_rating.AutoSize = True
    Me.lbAn_rating.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rating.Location = New System.Drawing.Point(3, 7)
    Me.lbAn_rating.Name = "lbAn_rating"
    Me.lbAn_rating.NTSDbField = ""
    Me.lbAn_rating.Size = New System.Drawing.Size(58, 13)
    Me.lbAn_rating.TabIndex = 604
    Me.lbAn_rating.Text = "Affidabilità"
    Me.lbAn_rating.Tooltip = ""
    Me.lbAn_rating.UseMnemonic = False
    '
    'lbAn_agcontrop
    '
    Me.lbAn_agcontrop.AutoSize = True
    Me.lbAn_agcontrop.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_agcontrop.Location = New System.Drawing.Point(3, 31)
    Me.lbAn_agcontrop.Name = "lbAn_agcontrop"
    Me.lbAn_agcontrop.NTSDbField = ""
    Me.lbAn_agcontrop.Size = New System.Drawing.Size(132, 13)
    Me.lbAn_agcontrop.TabIndex = 606
    Me.lbAn_agcontrop.Text = "Aggiunta controp. articolo"
    Me.lbAn_agcontrop.Tooltip = ""
    Me.lbAn_agcontrop.UseMnemonic = False
    '
    'lbXx_zona
    '
    Me.lbXx_zona.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_zona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_zona.Location = New System.Drawing.Point(197, 294)
    Me.lbXx_zona.Name = "lbXx_zona"
    Me.lbXx_zona.NTSDbField = ""
    Me.lbXx_zona.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_zona.TabIndex = 627
    Me.lbXx_zona.Text = "lbXx_zona"
    Me.lbXx_zona.Tooltip = ""
    Me.lbXx_zona.UseMnemonic = False
    '
    'edAn_agcontrop
    '
    Me.edAn_agcontrop.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_agcontrop.EditValue = "0"
    Me.edAn_agcontrop.Location = New System.Drawing.Point(141, 28)
    Me.edAn_agcontrop.Name = "edAn_agcontrop"
    Me.edAn_agcontrop.NTSDbField = ""
    Me.edAn_agcontrop.NTSFormat = "0"
    Me.edAn_agcontrop.NTSForzaVisZoom = False
    Me.edAn_agcontrop.NTSOldValue = ""
    Me.edAn_agcontrop.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_agcontrop.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_agcontrop.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_agcontrop.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_agcontrop.Properties.AutoHeight = False
    Me.edAn_agcontrop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_agcontrop.Properties.MaxLength = 65536
    Me.edAn_agcontrop.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_agcontrop.Size = New System.Drawing.Size(53, 20)
    Me.edAn_agcontrop.TabIndex = 607
    '
    'edAn_zona
    '
    Me.edAn_zona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_zona.EditValue = "0"
    Me.edAn_zona.Location = New System.Drawing.Point(141, 294)
    Me.edAn_zona.Name = "edAn_zona"
    Me.edAn_zona.NTSDbField = ""
    Me.edAn_zona.NTSFormat = "0"
    Me.edAn_zona.NTSForzaVisZoom = False
    Me.edAn_zona.NTSOldValue = ""
    Me.edAn_zona.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_zona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_zona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_zona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_zona.Properties.AutoHeight = False
    Me.edAn_zona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_zona.Properties.MaxLength = 65536
    Me.edAn_zona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_zona.Size = New System.Drawing.Size(53, 20)
    Me.edAn_zona.TabIndex = 628
    '
    'edAn_codling
    '
    Me.edAn_codling.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codling.EditValue = "0"
    Me.edAn_codling.Location = New System.Drawing.Point(141, 270)
    Me.edAn_codling.Name = "edAn_codling"
    Me.edAn_codling.NTSDbField = ""
    Me.edAn_codling.NTSFormat = "0"
    Me.edAn_codling.NTSForzaVisZoom = False
    Me.edAn_codling.NTSOldValue = ""
    Me.edAn_codling.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codling.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codling.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codling.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codling.Properties.AutoHeight = False
    Me.edAn_codling.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codling.Properties.MaxLength = 65536
    Me.edAn_codling.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codling.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codling.TabIndex = 595
    '
    'lbAn_zona
    '
    Me.lbAn_zona.AutoSize = True
    Me.lbAn_zona.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_zona.Location = New System.Drawing.Point(3, 297)
    Me.lbAn_zona.Name = "lbAn_zona"
    Me.lbAn_zona.NTSDbField = ""
    Me.lbAn_zona.Size = New System.Drawing.Size(31, 13)
    Me.lbAn_zona.TabIndex = 626
    Me.lbAn_zona.Text = "Zona"
    Me.lbAn_zona.Tooltip = ""
    Me.lbAn_zona.UseMnemonic = False
    '
    'lbAn_codling
    '
    Me.lbAn_codling.AutoSize = True
    Me.lbAn_codling.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codling.Location = New System.Drawing.Point(3, 273)
    Me.lbAn_codling.Name = "lbAn_codling"
    Me.lbAn_codling.NTSDbField = ""
    Me.lbAn_codling.Size = New System.Drawing.Size(38, 13)
    Me.lbAn_codling.TabIndex = 593
    Me.lbAn_codling.Text = "Lingua"
    Me.lbAn_codling.Tooltip = ""
    Me.lbAn_codling.UseMnemonic = False
    '
    'lbXx_categ
    '
    Me.lbXx_categ.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_categ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_categ.Location = New System.Drawing.Point(197, 173)
    Me.lbXx_categ.Name = "lbXx_categ"
    Me.lbXx_categ.NTSDbField = ""
    Me.lbXx_categ.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_categ.TabIndex = 624
    Me.lbXx_categ.Text = "lbXx_categ"
    Me.lbXx_categ.Tooltip = ""
    Me.lbXx_categ.UseMnemonic = False
    '
    'lbXx_codling
    '
    Me.lbXx_codling.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codling.Location = New System.Drawing.Point(197, 270)
    Me.lbXx_codling.Name = "lbXx_codling"
    Me.lbXx_codling.NTSDbField = ""
    Me.lbXx_codling.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codling.TabIndex = 567
    Me.lbXx_codling.Text = "xx_codling"
    Me.lbXx_codling.Tooltip = ""
    Me.lbXx_codling.UseMnemonic = False
    '
    'edAn_categ
    '
    Me.edAn_categ.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_categ.EditValue = "0"
    Me.edAn_categ.Location = New System.Drawing.Point(141, 173)
    Me.edAn_categ.Name = "edAn_categ"
    Me.edAn_categ.NTSDbField = ""
    Me.edAn_categ.NTSFormat = "0"
    Me.edAn_categ.NTSForzaVisZoom = False
    Me.edAn_categ.NTSOldValue = ""
    Me.edAn_categ.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_categ.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_categ.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_categ.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_categ.Properties.AutoHeight = False
    Me.edAn_categ.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_categ.Properties.MaxLength = 65536
    Me.edAn_categ.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_categ.Size = New System.Drawing.Size(53, 20)
    Me.edAn_categ.TabIndex = 625
    '
    'lbAn_categ
    '
    Me.lbAn_categ.AutoSize = True
    Me.lbAn_categ.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_categ.Location = New System.Drawing.Point(3, 174)
    Me.lbAn_categ.Name = "lbAn_categ"
    Me.lbAn_categ.NTSDbField = ""
    Me.lbAn_categ.Size = New System.Drawing.Size(54, 13)
    Me.lbAn_categ.TabIndex = 623
    Me.lbAn_categ.Text = "Categoria"
    Me.lbAn_categ.Tooltip = ""
    Me.lbAn_categ.UseMnemonic = False
    '
    'lbXx_agente
    '
    Me.lbXx_agente.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_agente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_agente.Location = New System.Drawing.Point(197, 122)
    Me.lbXx_agente.Name = "lbXx_agente"
    Me.lbXx_agente.NTSDbField = ""
    Me.lbXx_agente.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_agente.TabIndex = 621
    Me.lbXx_agente.Text = "lbXx_agente"
    Me.lbXx_agente.Tooltip = ""
    Me.lbXx_agente.UseMnemonic = False
    '
    'edAn_agente
    '
    Me.edAn_agente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_agente.EditValue = "0"
    Me.edAn_agente.Location = New System.Drawing.Point(141, 122)
    Me.edAn_agente.Name = "edAn_agente"
    Me.edAn_agente.NTSDbField = ""
    Me.edAn_agente.NTSFormat = "0"
    Me.edAn_agente.NTSForzaVisZoom = False
    Me.edAn_agente.NTSOldValue = ""
    Me.edAn_agente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_agente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_agente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_agente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_agente.Properties.AutoHeight = False
    Me.edAn_agente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_agente.Properties.MaxLength = 65536
    Me.edAn_agente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_agente.Size = New System.Drawing.Size(53, 20)
    Me.edAn_agente.TabIndex = 622
    '
    'lbAn_agente
    '
    Me.lbAn_agente.AutoSize = True
    Me.lbAn_agente.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_agente.Location = New System.Drawing.Point(3, 125)
    Me.lbAn_agente.Name = "lbAn_agente"
    Me.lbAn_agente.NTSDbField = ""
    Me.lbAn_agente.Size = New System.Drawing.Size(51, 13)
    Me.lbAn_agente.TabIndex = 620
    Me.lbAn_agente.Text = "Agente 1"
    Me.lbAn_agente.Tooltip = ""
    Me.lbAn_agente.UseMnemonic = False
    '
    'lbAn_clascon
    '
    Me.lbAn_clascon.AutoSize = True
    Me.lbAn_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_clascon.Location = New System.Drawing.Point(3, 200)
    Me.lbAn_clascon.Name = "lbAn_clascon"
    Me.lbAn_clascon.NTSDbField = ""
    Me.lbAn_clascon.Size = New System.Drawing.Size(84, 13)
    Me.lbAn_clascon.TabIndex = 602
    Me.lbAn_clascon.Text = "Classe di sconto"
    Me.lbAn_clascon.Tooltip = ""
    Me.lbAn_clascon.UseMnemonic = False
    '
    'lbXx_codcana
    '
    Me.lbXx_codcana.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcana.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcana.Location = New System.Drawing.Point(197, 246)
    Me.lbXx_codcana.Name = "lbXx_codcana"
    Me.lbXx_codcana.NTSDbField = ""
    Me.lbXx_codcana.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codcana.TabIndex = 612
    Me.lbXx_codcana.Text = "lbXx_codcana"
    Me.lbXx_codcana.Tooltip = ""
    Me.lbXx_codcana.UseMnemonic = False
    '
    'edAn_clascon
    '
    Me.edAn_clascon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_clascon.EditValue = "0"
    Me.edAn_clascon.Location = New System.Drawing.Point(141, 197)
    Me.edAn_clascon.Name = "edAn_clascon"
    Me.edAn_clascon.NTSDbField = ""
    Me.edAn_clascon.NTSFormat = "0"
    Me.edAn_clascon.NTSForzaVisZoom = False
    Me.edAn_clascon.NTSOldValue = ""
    Me.edAn_clascon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_clascon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_clascon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_clascon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_clascon.Properties.AutoHeight = False
    Me.edAn_clascon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_clascon.Properties.MaxLength = 65536
    Me.edAn_clascon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_clascon.Size = New System.Drawing.Size(53, 20)
    Me.edAn_clascon.TabIndex = 604
    '
    'lbXx_clascon
    '
    Me.lbXx_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_clascon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_clascon.Location = New System.Drawing.Point(197, 197)
    Me.lbXx_clascon.Name = "lbXx_clascon"
    Me.lbXx_clascon.NTSDbField = ""
    Me.lbXx_clascon.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_clascon.TabIndex = 603
    Me.lbXx_clascon.Text = "lbXx_clascon"
    Me.lbXx_clascon.Tooltip = ""
    Me.lbXx_clascon.UseMnemonic = False
    '
    'lbXx_claprov
    '
    Me.lbXx_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_claprov.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_claprov.Location = New System.Drawing.Point(197, 221)
    Me.lbXx_claprov.Name = "lbXx_claprov"
    Me.lbXx_claprov.NTSDbField = ""
    Me.lbXx_claprov.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_claprov.TabIndex = 606
    Me.lbXx_claprov.Text = "lbXx_claprov"
    Me.lbXx_claprov.Tooltip = ""
    Me.lbXx_claprov.UseMnemonic = False
    '
    'edAn_codcana
    '
    Me.edAn_codcana.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codcana.EditValue = "0"
    Me.edAn_codcana.Location = New System.Drawing.Point(141, 246)
    Me.edAn_codcana.Name = "edAn_codcana"
    Me.edAn_codcana.NTSDbField = ""
    Me.edAn_codcana.NTSFormat = "0"
    Me.edAn_codcana.NTSForzaVisZoom = False
    Me.edAn_codcana.NTSOldValue = ""
    Me.edAn_codcana.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codcana.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codcana.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codcana.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codcana.Properties.AutoHeight = False
    Me.edAn_codcana.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codcana.Properties.MaxLength = 65536
    Me.edAn_codcana.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codcana.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codcana.TabIndex = 613
    '
    'lbAn_claprov
    '
    Me.lbAn_claprov.AutoSize = True
    Me.lbAn_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_claprov.Location = New System.Drawing.Point(3, 224)
    Me.lbAn_claprov.Name = "lbAn_claprov"
    Me.lbAn_claprov.NTSDbField = ""
    Me.lbAn_claprov.Size = New System.Drawing.Size(93, 13)
    Me.lbAn_claprov.TabIndex = 605
    Me.lbAn_claprov.Text = "Classe provvigioni"
    Me.lbAn_claprov.Tooltip = ""
    Me.lbAn_claprov.UseMnemonic = False
    '
    'edAn_claprov
    '
    Me.edAn_claprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_claprov.EditValue = "0"
    Me.edAn_claprov.Location = New System.Drawing.Point(141, 221)
    Me.edAn_claprov.Name = "edAn_claprov"
    Me.edAn_claprov.NTSDbField = ""
    Me.edAn_claprov.NTSFormat = "0"
    Me.edAn_claprov.NTSForzaVisZoom = False
    Me.edAn_claprov.NTSOldValue = ""
    Me.edAn_claprov.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_claprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_claprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_claprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_claprov.Properties.AutoHeight = False
    Me.edAn_claprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_claprov.Properties.MaxLength = 65536
    Me.edAn_claprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_claprov.Size = New System.Drawing.Size(53, 20)
    Me.edAn_claprov.TabIndex = 607
    '
    'lbAn_codcana
    '
    Me.lbAn_codcana.AutoSize = True
    Me.lbAn_codcana.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codcana.Location = New System.Drawing.Point(3, 249)
    Me.lbAn_codcana.Name = "lbAn_codcana"
    Me.lbAn_codcana.NTSDbField = ""
    Me.lbAn_codcana.Size = New System.Drawing.Size(75, 13)
    Me.lbAn_codcana.TabIndex = 611
    Me.lbAn_codcana.Text = "Codice Canale"
    Me.lbAn_codcana.Tooltip = ""
    Me.lbAn_codcana.UseMnemonic = False
    '
    'lbXx_agente2
    '
    Me.lbXx_agente2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_agente2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_agente2.Location = New System.Drawing.Point(197, 148)
    Me.lbXx_agente2.Name = "lbXx_agente2"
    Me.lbXx_agente2.NTSDbField = ""
    Me.lbXx_agente2.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_agente2.TabIndex = 597
    Me.lbXx_agente2.Text = "lbXx_agente2"
    Me.lbXx_agente2.Tooltip = ""
    Me.lbXx_agente2.UseMnemonic = False
    '
    'edAn_agente2
    '
    Me.edAn_agente2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_agente2.EditValue = "0"
    Me.edAn_agente2.Location = New System.Drawing.Point(141, 148)
    Me.edAn_agente2.Name = "edAn_agente2"
    Me.edAn_agente2.NTSDbField = ""
    Me.edAn_agente2.NTSFormat = "0"
    Me.edAn_agente2.NTSForzaVisZoom = False
    Me.edAn_agente2.NTSOldValue = ""
    Me.edAn_agente2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_agente2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_agente2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_agente2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_agente2.Properties.AutoHeight = False
    Me.edAn_agente2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_agente2.Properties.MaxLength = 65536
    Me.edAn_agente2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_agente2.Size = New System.Drawing.Size(53, 20)
    Me.edAn_agente2.TabIndex = 598
    '
    'lbAn_agente2
    '
    Me.lbAn_agente2.AutoSize = True
    Me.lbAn_agente2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_agente2.Location = New System.Drawing.Point(3, 151)
    Me.lbAn_agente2.Name = "lbAn_agente2"
    Me.lbAn_agente2.NTSDbField = ""
    Me.lbAn_agente2.Size = New System.Drawing.Size(51, 13)
    Me.lbAn_agente2.TabIndex = 596
    Me.lbAn_agente2.Text = "Agente 2"
    Me.lbAn_agente2.Tooltip = ""
    Me.lbAn_agente2.UseMnemonic = False
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage4.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage4.Controls.Add(Me.pnDatiContabili)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage4.Text = "&4 - Dati contabili"
    '
    'pnDatiContabili
    '
    Me.pnDatiContabili.AllowDrop = True
    Me.pnDatiContabili.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDatiContabili.Appearance.Options.UseBackColor = True
    Me.pnDatiContabili.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDatiContabili.Controls.Add(Me.fmCadc)
    Me.pnDatiContabili.Controls.Add(Me.cbAn_privacy)
    Me.pnDatiContabili.Controls.Add(Me.lbAn_privacy)
    Me.pnDatiContabili.Controls.Add(Me.lbAn_valuta)
    Me.pnDatiContabili.Controls.Add(Me.edAn_valuta)
    Me.pnDatiContabili.Controls.Add(Me.lbXx_valuta)
    Me.pnDatiContabili.Controls.Add(Me.lbXx_codrtac)
    Me.pnDatiContabili.Controls.Add(Me.edAn_codrtac)
    Me.pnDatiContabili.Controls.Add(Me.lbAn_codrtac)
    Me.pnDatiContabili.Controls.Add(Me.pnDaticondSx)
    Me.pnDatiContabili.Controls.Add(Me.lbXx_controp)
    Me.pnDatiContabili.Controls.Add(Me.lbAn_contropLabel)
    Me.pnDatiContabili.Controls.Add(Me.edAn_controp)
    Me.pnDatiContabili.Controls.Add(Me.fmRiclassificati)
    Me.pnDatiContabili.Controls.Add(Me.lbXx_contfatt)
    Me.pnDatiContabili.Controls.Add(Me.edAn_contfatt)
    Me.pnDatiContabili.Controls.Add(Me.lbAn_contfatt)
    Me.pnDatiContabili.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDatiContabili.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDatiContabili.Location = New System.Drawing.Point(0, 0)
    Me.pnDatiContabili.Name = "pnDatiContabili"
    Me.pnDatiContabili.NTSActiveTrasparency = True
    Me.pnDatiContabili.Size = New System.Drawing.Size(771, 317)
    Me.pnDatiContabili.TabIndex = 1
    Me.pnDatiContabili.Text = "NtsPanel4"
    '
    'fmCadc
    '
    Me.fmCadc.AllowDrop = True
    Me.fmCadc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmCadc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCadc.Appearance.Options.UseBackColor = True
    Me.fmCadc.Controls.Add(Me.edAn_coddicv)
    Me.fmCadc.Controls.Add(Me.edAn_coddica)
    Me.fmCadc.Controls.Add(Me.lbAn_codtcdc)
    Me.fmCadc.Controls.Add(Me.edAn_codtcdc)
    Me.fmCadc.Controls.Add(Me.lbXx_codtcdc)
    Me.fmCadc.Controls.Add(Me.lbXx_coddicv)
    Me.fmCadc.Controls.Add(Me.lbAn_coddica)
    Me.fmCadc.Controls.Add(Me.lbAn_coddicv)
    Me.fmCadc.Controls.Add(Me.lbXx_coddica)
    Me.fmCadc.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCadc.Location = New System.Drawing.Point(378, 91)
    Me.fmCadc.Name = "fmCadc"
    Me.fmCadc.Size = New System.Drawing.Size(387, 90)
    Me.fmCadc.TabIndex = 661
    Me.fmCadc.Text = "Contabilità analitica duplice contabile"
    '
    'edAn_coddicv
    '
    Me.edAn_coddicv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_coddicv.EditValue = ""
    Me.edAn_coddicv.Location = New System.Drawing.Point(115, 65)
    Me.edAn_coddicv.Name = "edAn_coddicv"
    Me.edAn_coddicv.NTSDbField = ""
    Me.edAn_coddicv.NTSForzaVisZoom = False
    Me.edAn_coddicv.NTSOldValue = ""
    Me.edAn_coddicv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_coddicv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_coddicv.Properties.AutoHeight = False
    Me.edAn_coddicv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_coddicv.Properties.MaxLength = 65536
    Me.edAn_coddicv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_coddicv.Size = New System.Drawing.Size(88, 20)
    Me.edAn_coddicv.TabIndex = 665
    '
    'edAn_coddica
    '
    Me.edAn_coddica.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_coddica.EditValue = ""
    Me.edAn_coddica.Location = New System.Drawing.Point(115, 43)
    Me.edAn_coddica.Name = "edAn_coddica"
    Me.edAn_coddica.NTSDbField = ""
    Me.edAn_coddica.NTSForzaVisZoom = False
    Me.edAn_coddica.NTSOldValue = ""
    Me.edAn_coddica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_coddica.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_coddica.Properties.AutoHeight = False
    Me.edAn_coddica.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_coddica.Properties.MaxLength = 65536
    Me.edAn_coddica.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_coddica.Size = New System.Drawing.Size(88, 20)
    Me.edAn_coddica.TabIndex = 664
    '
    'lbAn_codtcdc
    '
    Me.lbAn_codtcdc.AutoSize = True
    Me.lbAn_codtcdc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codtcdc.Location = New System.Drawing.Point(3, 24)
    Me.lbAn_codtcdc.Name = "lbAn_codtcdc"
    Me.lbAn_codtcdc.NTSDbField = ""
    Me.lbAn_codtcdc.Size = New System.Drawing.Size(80, 13)
    Me.lbAn_codtcdc.TabIndex = 662
    Me.lbAn_codtcdc.Text = "Tipologia entità"
    Me.lbAn_codtcdc.Tooltip = ""
    Me.lbAn_codtcdc.UseMnemonic = False
    '
    'edAn_codtcdc
    '
    Me.edAn_codtcdc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codtcdc.EditValue = "0"
    Me.edAn_codtcdc.Location = New System.Drawing.Point(150, 21)
    Me.edAn_codtcdc.Name = "edAn_codtcdc"
    Me.edAn_codtcdc.NTSDbField = ""
    Me.edAn_codtcdc.NTSFormat = "0"
    Me.edAn_codtcdc.NTSForzaVisZoom = False
    Me.edAn_codtcdc.NTSOldValue = ""
    Me.edAn_codtcdc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codtcdc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codtcdc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codtcdc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codtcdc.Properties.AutoHeight = False
    Me.edAn_codtcdc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codtcdc.Properties.MaxLength = 65536
    Me.edAn_codtcdc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codtcdc.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codtcdc.TabIndex = 663
    '
    'lbXx_codtcdc
    '
    Me.lbXx_codtcdc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_codtcdc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtcdc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtcdc.Location = New System.Drawing.Point(209, 21)
    Me.lbXx_codtcdc.Name = "lbXx_codtcdc"
    Me.lbXx_codtcdc.NTSDbField = ""
    Me.lbXx_codtcdc.Size = New System.Drawing.Size(173, 20)
    Me.lbXx_codtcdc.TabIndex = 661
    Me.lbXx_codtcdc.Tooltip = ""
    Me.lbXx_codtcdc.UseMnemonic = False
    '
    'lbXx_coddicv
    '
    Me.lbXx_coddicv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_coddicv.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddicv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddicv.Location = New System.Drawing.Point(209, 64)
    Me.lbXx_coddicv.Name = "lbXx_coddicv"
    Me.lbXx_coddicv.NTSDbField = ""
    Me.lbXx_coddicv.Size = New System.Drawing.Size(173, 20)
    Me.lbXx_coddicv.TabIndex = 660
    Me.lbXx_coddicv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_coddicv.Tooltip = ""
    Me.lbXx_coddicv.UseMnemonic = False
    '
    'lbAn_coddica
    '
    Me.lbAn_coddica.AutoSize = True
    Me.lbAn_coddica.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_coddica.Location = New System.Drawing.Point(3, 46)
    Me.lbAn_coddica.Name = "lbAn_coddica"
    Me.lbAn_coddica.NTSDbField = ""
    Me.lbAn_coddica.Size = New System.Drawing.Size(110, 13)
    Me.lbAn_coddica.TabIndex = 655
    Me.lbAn_coddica.Text = "Aggregazione Budget"
    Me.lbAn_coddica.Tooltip = ""
    Me.lbAn_coddica.UseMnemonic = False
    '
    'lbAn_coddicv
    '
    Me.lbAn_coddicv.AutoSize = True
    Me.lbAn_coddicv.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_coddicv.Location = New System.Drawing.Point(3, 68)
    Me.lbAn_coddicv.Name = "lbAn_coddicv"
    Me.lbAn_coddicv.NTSDbField = ""
    Me.lbAn_coddicv.Size = New System.Drawing.Size(100, 13)
    Me.lbAn_coddicv.TabIndex = 658
    Me.lbAn_coddicv.Text = "Valore Agg. Budget"
    Me.lbAn_coddicv.Tooltip = ""
    Me.lbAn_coddicv.UseMnemonic = False
    '
    'lbXx_coddica
    '
    Me.lbXx_coddica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_coddica.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddica.Location = New System.Drawing.Point(209, 43)
    Me.lbXx_coddica.Name = "lbXx_coddica"
    Me.lbXx_coddica.NTSDbField = ""
    Me.lbXx_coddica.Size = New System.Drawing.Size(173, 20)
    Me.lbXx_coddica.TabIndex = 656
    Me.lbXx_coddica.Tooltip = ""
    Me.lbXx_coddica.UseMnemonic = False
    '
    'cbAn_privacy
    '
    Me.cbAn_privacy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbAn_privacy.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_privacy.DataSource = Nothing
    Me.cbAn_privacy.DisplayMember = ""
    Me.cbAn_privacy.Location = New System.Drawing.Point(528, 183)
    Me.cbAn_privacy.Name = "cbAn_privacy"
    Me.cbAn_privacy.NTSDbField = ""
    Me.cbAn_privacy.Properties.AutoHeight = False
    Me.cbAn_privacy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_privacy.Properties.DropDownRows = 30
    Me.cbAn_privacy.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_privacy.SelectedValue = ""
    Me.cbAn_privacy.Size = New System.Drawing.Size(237, 20)
    Me.cbAn_privacy.TabIndex = 654
    Me.cbAn_privacy.ValueMember = ""
    '
    'lbAn_privacy
    '
    Me.lbAn_privacy.AutoSize = True
    Me.lbAn_privacy.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_privacy.Location = New System.Drawing.Point(377, 186)
    Me.lbAn_privacy.Name = "lbAn_privacy"
    Me.lbAn_privacy.NTSDbField = ""
    Me.lbAn_privacy.Size = New System.Drawing.Size(115, 13)
    Me.lbAn_privacy.TabIndex = 653
    Me.lbAn_privacy.Text = "Autorizzazione privacy"
    Me.lbAn_privacy.Tooltip = ""
    Me.lbAn_privacy.UseMnemonic = False
    '
    'lbAn_valuta
    '
    Me.lbAn_valuta.AutoSize = True
    Me.lbAn_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_valuta.Location = New System.Drawing.Point(377, 72)
    Me.lbAn_valuta.Name = "lbAn_valuta"
    Me.lbAn_valuta.NTSDbField = ""
    Me.lbAn_valuta.Size = New System.Drawing.Size(37, 13)
    Me.lbAn_valuta.TabIndex = 651
    Me.lbAn_valuta.Text = "Valuta"
    Me.lbAn_valuta.Tooltip = ""
    Me.lbAn_valuta.UseMnemonic = False
    '
    'edAn_valuta
    '
    Me.edAn_valuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_valuta.EditValue = "0"
    Me.edAn_valuta.Location = New System.Drawing.Point(528, 69)
    Me.edAn_valuta.Name = "edAn_valuta"
    Me.edAn_valuta.NTSDbField = ""
    Me.edAn_valuta.NTSFormat = "0"
    Me.edAn_valuta.NTSForzaVisZoom = False
    Me.edAn_valuta.NTSOldValue = ""
    Me.edAn_valuta.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_valuta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_valuta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_valuta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_valuta.Properties.AutoHeight = False
    Me.edAn_valuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_valuta.Properties.MaxLength = 65536
    Me.edAn_valuta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_valuta.Size = New System.Drawing.Size(53, 20)
    Me.edAn_valuta.TabIndex = 652
    '
    'lbXx_valuta
    '
    Me.lbXx_valuta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_valuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_valuta.Location = New System.Drawing.Point(587, 72)
    Me.lbXx_valuta.Name = "lbXx_valuta"
    Me.lbXx_valuta.NTSDbField = ""
    Me.lbXx_valuta.Size = New System.Drawing.Size(178, 20)
    Me.lbXx_valuta.TabIndex = 648
    Me.lbXx_valuta.Text = "xx_valuta"
    Me.lbXx_valuta.Tooltip = ""
    Me.lbXx_valuta.UseMnemonic = False
    '
    'lbXx_codrtac
    '
    Me.lbXx_codrtac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_codrtac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codrtac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codrtac.Location = New System.Drawing.Point(587, 49)
    Me.lbXx_codrtac.Name = "lbXx_codrtac"
    Me.lbXx_codrtac.NTSDbField = ""
    Me.lbXx_codrtac.Size = New System.Drawing.Size(178, 20)
    Me.lbXx_codrtac.TabIndex = 649
    Me.lbXx_codrtac.Text = "lbXx_codrtac"
    Me.lbXx_codrtac.Tooltip = ""
    Me.lbXx_codrtac.UseMnemonic = False
    '
    'edAn_codrtac
    '
    Me.edAn_codrtac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codrtac.EditValue = "0"
    Me.edAn_codrtac.Location = New System.Drawing.Point(528, 47)
    Me.edAn_codrtac.Name = "edAn_codrtac"
    Me.edAn_codrtac.NTSDbField = ""
    Me.edAn_codrtac.NTSFormat = "0"
    Me.edAn_codrtac.NTSForzaVisZoom = False
    Me.edAn_codrtac.NTSOldValue = ""
    Me.edAn_codrtac.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codrtac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codrtac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codrtac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codrtac.Properties.AutoHeight = False
    Me.edAn_codrtac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codrtac.Properties.MaxLength = 65536
    Me.edAn_codrtac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codrtac.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codrtac.TabIndex = 650
    '
    'lbAn_codrtac
    '
    Me.lbAn_codrtac.AutoSize = True
    Me.lbAn_codrtac.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codrtac.Location = New System.Drawing.Point(377, 50)
    Me.lbAn_codrtac.Name = "lbAn_codrtac"
    Me.lbAn_codrtac.NTSDbField = ""
    Me.lbAn_codrtac.Size = New System.Drawing.Size(144, 13)
    Me.lbAn_codrtac.TabIndex = 647
    Me.lbAn_codrtac.Text = "Tipo assog. ritenuta acconto"
    Me.lbAn_codrtac.Tooltip = ""
    Me.lbAn_codrtac.UseMnemonic = False
    '
    'pnDaticondSx
    '
    Me.pnDaticondSx.AllowDrop = True
    Me.pnDaticondSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDaticondSx.Appearance.Options.UseBackColor = True
    Me.pnDaticondSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDaticondSx.Controls.Add(Me.ckAn_scaden)
    Me.pnDaticondSx.Controls.Add(Me.ckAn_partite)
    Me.pnDaticondSx.Controls.Add(Me.fmPagamento)
    Me.pnDaticondSx.Controls.Add(Me.lbAn_colbil)
    Me.pnDaticondSx.Controls.Add(Me.cbAn_colbil)
    Me.pnDaticondSx.Controls.Add(Me.ckAn_intragr)
    Me.pnDaticondSx.Controls.Add(Me.lbAn_codnscol)
    Me.pnDaticondSx.Controls.Add(Me.edAn_codnscol)
    Me.pnDaticondSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDaticondSx.Location = New System.Drawing.Point(3, 0)
    Me.pnDaticondSx.Name = "pnDaticondSx"
    Me.pnDaticondSx.NTSActiveTrasparency = True
    Me.pnDaticondSx.Size = New System.Drawing.Size(368, 314)
    Me.pnDaticondSx.TabIndex = 646
    Me.pnDaticondSx.Text = "NtsPanel1"
    '
    'ckAn_scaden
    '
    Me.ckAn_scaden.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_scaden.Location = New System.Drawing.Point(12, 228)
    Me.ckAn_scaden.Name = "ckAn_scaden"
    Me.ckAn_scaden.NTSCheckValue = "S"
    Me.ckAn_scaden.NTSUnCheckValue = "N"
    Me.ckAn_scaden.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_scaden.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_scaden.Properties.AutoHeight = False
    Me.ckAn_scaden.Properties.Caption = "Gestione &scadenze"
    Me.ckAn_scaden.Size = New System.Drawing.Size(122, 19)
    Me.ckAn_scaden.TabIndex = 550
    '
    'ckAn_partite
    '
    Me.ckAn_partite.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_partite.Location = New System.Drawing.Point(12, 210)
    Me.ckAn_partite.Name = "ckAn_partite"
    Me.ckAn_partite.NTSCheckValue = "S"
    Me.ckAn_partite.NTSUnCheckValue = "N"
    Me.ckAn_partite.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_partite.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_partite.Properties.AutoHeight = False
    Me.ckAn_partite.Properties.Caption = "Gestione &partite"
    Me.ckAn_partite.Size = New System.Drawing.Size(122, 19)
    Me.ckAn_partite.TabIndex = 549
    '
    'fmPagamento
    '
    Me.fmPagamento.AllowDrop = True
    Me.fmPagamento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPagamento.Appearance.Options.UseBackColor = True
    Me.fmPagamento.Controls.Add(Me.edAn_codpagadet3)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpagadet3)
    Me.fmPagamento.Controls.Add(Me.edAn_codpagadet2)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpagadet2)
    Me.fmPagamento.Controls.Add(Me.edAn_codpagadet)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpagadet)
    Me.fmPagamento.Controls.Add(Me.lbDeteriorabili)
    Me.fmPagamento.Controls.Add(Me.edAn_codpagscagl2)
    Me.fmPagamento.Controls.Add(Me.lbAn_codpagscagl2)
    Me.fmPagamento.Controls.Add(Me.edAn_codpagscagl1)
    Me.fmPagamento.Controls.Add(Me.lbAn_codpagscagl1)
    Me.fmPagamento.Controls.Add(Me.lbAn_codpaga3)
    Me.fmPagamento.Controls.Add(Me.edAn_codpaga3)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpaga3)
    Me.fmPagamento.Controls.Add(Me.lbAn_codpaga2)
    Me.fmPagamento.Controls.Add(Me.edAn_codpaga2)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpaga2)
    Me.fmPagamento.Controls.Add(Me.lbAn_codpag)
    Me.fmPagamento.Controls.Add(Me.edAn_giofiss)
    Me.fmPagamento.Controls.Add(Me.edAn_codpag)
    Me.fmPagamento.Controls.Add(Me.lbAn_giofiss)
    Me.fmPagamento.Controls.Add(Me.lbXx_codpag)
    Me.fmPagamento.Controls.Add(Me.edAn_mesees2)
    Me.fmPagamento.Controls.Add(Me.lbAn_mesees)
    Me.fmPagamento.Controls.Add(Me.edAn_mesees1)
    Me.fmPagamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPagamento.Location = New System.Drawing.Point(6, 3)
    Me.fmPagamento.Name = "fmPagamento"
    Me.fmPagamento.Size = New System.Drawing.Size(352, 206)
    Me.fmPagamento.TabIndex = 650
    Me.fmPagamento.Text = "Modalità di pagamento"
    '
    'edAn_codpagscagl2
    '
    Me.edAn_codpagscagl2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpagscagl2.EditValue = "0"
    Me.edAn_codpagscagl2.Location = New System.Drawing.Point(119, 161)
    Me.edAn_codpagscagl2.Name = "edAn_codpagscagl2"
    Me.edAn_codpagscagl2.NTSDbField = ""
    Me.edAn_codpagscagl2.NTSFormat = "0"
    Me.edAn_codpagscagl2.NTSForzaVisZoom = False
    Me.edAn_codpagscagl2.NTSOldValue = ""
    Me.edAn_codpagscagl2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpagscagl2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpagscagl2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpagscagl2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpagscagl2.Properties.AutoHeight = False
    Me.edAn_codpagscagl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpagscagl2.Properties.MaxLength = 65536
    Me.edAn_codpagscagl2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpagscagl2.Size = New System.Drawing.Size(67, 20)
    Me.edAn_codpagscagl2.TabIndex = 649
    '
    'lbAn_codpagscagl2
    '
    Me.lbAn_codpagscagl2.AutoSize = True
    Me.lbAn_codpagscagl2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpagscagl2.Location = New System.Drawing.Point(5, 164)
    Me.lbAn_codpagscagl2.Name = "lbAn_codpagscagl2"
    Me.lbAn_codpagscagl2.NTSDbField = ""
    Me.lbAn_codpagscagl2.Size = New System.Drawing.Size(102, 13)
    Me.lbAn_codpagscagl2.TabIndex = 648
    Me.lbAn_codpagscagl2.Text = "Limite grandi importi"
    Me.lbAn_codpagscagl2.Tooltip = ""
    Me.lbAn_codpagscagl2.UseMnemonic = False
    '
    'edAn_codpagscagl1
    '
    Me.edAn_codpagscagl1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpagscagl1.EditValue = "0"
    Me.edAn_codpagscagl1.Location = New System.Drawing.Point(119, 139)
    Me.edAn_codpagscagl1.Name = "edAn_codpagscagl1"
    Me.edAn_codpagscagl1.NTSDbField = ""
    Me.edAn_codpagscagl1.NTSFormat = "0"
    Me.edAn_codpagscagl1.NTSForzaVisZoom = False
    Me.edAn_codpagscagl1.NTSOldValue = ""
    Me.edAn_codpagscagl1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpagscagl1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpagscagl1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpagscagl1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpagscagl1.Properties.AutoHeight = False
    Me.edAn_codpagscagl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpagscagl1.Properties.MaxLength = 65536
    Me.edAn_codpagscagl1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpagscagl1.Size = New System.Drawing.Size(67, 20)
    Me.edAn_codpagscagl1.TabIndex = 647
    '
    'lbAn_codpagscagl1
    '
    Me.lbAn_codpagscagl1.AutoSize = True
    Me.lbAn_codpagscagl1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpagscagl1.Location = New System.Drawing.Point(5, 142)
    Me.lbAn_codpagscagl1.Name = "lbAn_codpagscagl1"
    Me.lbAn_codpagscagl1.NTSDbField = ""
    Me.lbAn_codpagscagl1.Size = New System.Drawing.Size(108, 13)
    Me.lbAn_codpagscagl1.TabIndex = 646
    Me.lbAn_codpagscagl1.Text = "Limite minimo importo"
    Me.lbAn_codpagscagl1.Tooltip = ""
    Me.lbAn_codpagscagl1.UseMnemonic = False
    '
    'lbAn_codpaga3
    '
    Me.lbAn_codpaga3.AutoSize = True
    Me.lbAn_codpaga3.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpaga3.Location = New System.Drawing.Point(5, 99)
    Me.lbAn_codpaga3.Name = "lbAn_codpaga3"
    Me.lbAn_codpaga3.NTSDbField = ""
    Me.lbAn_codpaga3.Size = New System.Drawing.Size(164, 13)
    Me.lbAn_codpaga3.TabIndex = 644
    Me.lbAn_codpaga3.Text = "Codice pagamento grandi importi"
    Me.lbAn_codpaga3.Tooltip = ""
    Me.lbAn_codpaga3.UseMnemonic = False
    '
    'edAn_codpaga3
    '
    Me.edAn_codpaga3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpaga3.EditValue = "0"
    Me.edAn_codpaga3.Location = New System.Drawing.Point(8, 115)
    Me.edAn_codpaga3.Name = "edAn_codpaga3"
    Me.edAn_codpaga3.NTSDbField = ""
    Me.edAn_codpaga3.NTSFormat = "0"
    Me.edAn_codpaga3.NTSForzaVisZoom = False
    Me.edAn_codpaga3.NTSOldValue = "0"
    Me.edAn_codpaga3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpaga3.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpaga3.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpaga3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpaga3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpaga3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpaga3.Properties.AutoHeight = False
    Me.edAn_codpaga3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpaga3.Properties.MaxLength = 65536
    Me.edAn_codpaga3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpaga3.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpaga3.TabIndex = 643
    '
    'lbXx_codpaga3
    '
    Me.lbXx_codpaga3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpaga3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpaga3.Location = New System.Drawing.Point(70, 115)
    Me.lbXx_codpaga3.Name = "lbXx_codpaga3"
    Me.lbXx_codpaga3.NTSDbField = ""
    Me.lbXx_codpaga3.Size = New System.Drawing.Size(116, 21)
    Me.lbXx_codpaga3.TabIndex = 645
    Me.lbXx_codpaga3.Tooltip = ""
    Me.lbXx_codpaga3.UseMnemonic = False
    '
    'lbAn_codpaga2
    '
    Me.lbAn_codpaga2.AutoSize = True
    Me.lbAn_codpaga2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpaga2.Location = New System.Drawing.Point(5, 60)
    Me.lbAn_codpaga2.Name = "lbAn_codpaga2"
    Me.lbAn_codpaga2.NTSDbField = ""
    Me.lbAn_codpaga2.Size = New System.Drawing.Size(162, 13)
    Me.lbAn_codpaga2.TabIndex = 641
    Me.lbAn_codpaga2.Text = "Codice pagamento importi minimi"
    Me.lbAn_codpaga2.Tooltip = ""
    Me.lbAn_codpaga2.UseMnemonic = False
    '
    'edAn_codpaga2
    '
    Me.edAn_codpaga2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpaga2.EditValue = "0"
    Me.edAn_codpaga2.Location = New System.Drawing.Point(8, 76)
    Me.edAn_codpaga2.Name = "edAn_codpaga2"
    Me.edAn_codpaga2.NTSDbField = ""
    Me.edAn_codpaga2.NTSFormat = "0"
    Me.edAn_codpaga2.NTSForzaVisZoom = False
    Me.edAn_codpaga2.NTSOldValue = "0"
    Me.edAn_codpaga2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpaga2.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpaga2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpaga2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpaga2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpaga2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpaga2.Properties.AutoHeight = False
    Me.edAn_codpaga2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpaga2.Properties.MaxLength = 65536
    Me.edAn_codpaga2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpaga2.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpaga2.TabIndex = 640
    '
    'lbXx_codpaga2
    '
    Me.lbXx_codpaga2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpaga2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpaga2.Location = New System.Drawing.Point(70, 76)
    Me.lbXx_codpaga2.Name = "lbXx_codpaga2"
    Me.lbXx_codpaga2.NTSDbField = ""
    Me.lbXx_codpaga2.Size = New System.Drawing.Size(116, 21)
    Me.lbXx_codpaga2.TabIndex = 642
    Me.lbXx_codpaga2.Tooltip = ""
    Me.lbXx_codpaga2.UseMnemonic = False
    '
    'lbAn_codpag
    '
    Me.lbAn_codpag.AutoSize = True
    Me.lbAn_codpag.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpag.Location = New System.Drawing.Point(5, 22)
    Me.lbAn_codpag.Name = "lbAn_codpag"
    Me.lbAn_codpag.NTSDbField = ""
    Me.lbAn_codpag.Size = New System.Drawing.Size(96, 13)
    Me.lbAn_codpag.TabIndex = 633
    Me.lbAn_codpag.Text = "Codice pagamento"
    Me.lbAn_codpag.Tooltip = ""
    Me.lbAn_codpag.UseMnemonic = False
    '
    'edAn_giofiss
    '
    Me.edAn_giofiss.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_giofiss.EditValue = "0"
    Me.edAn_giofiss.Location = New System.Drawing.Point(254, 183)
    Me.edAn_giofiss.Name = "edAn_giofiss"
    Me.edAn_giofiss.NTSDbField = ""
    Me.edAn_giofiss.NTSFormat = "0"
    Me.edAn_giofiss.NTSForzaVisZoom = False
    Me.edAn_giofiss.NTSOldValue = ""
    Me.edAn_giofiss.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_giofiss.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_giofiss.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_giofiss.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_giofiss.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_giofiss.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_giofiss.Properties.AutoHeight = False
    Me.edAn_giofiss.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_giofiss.Properties.MaxLength = 65536
    Me.edAn_giofiss.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_giofiss.Size = New System.Drawing.Size(39, 20)
    Me.edAn_giofiss.TabIndex = 637
    '
    'edAn_codpag
    '
    Me.edAn_codpag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpag.EditValue = "999"
    Me.edAn_codpag.Location = New System.Drawing.Point(8, 38)
    Me.edAn_codpag.Name = "edAn_codpag"
    Me.edAn_codpag.NTSDbField = ""
    Me.edAn_codpag.NTSFormat = "0"
    Me.edAn_codpag.NTSForzaVisZoom = False
    Me.edAn_codpag.NTSOldValue = "999"
    Me.edAn_codpag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpag.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpag.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpag.Properties.AutoHeight = False
    Me.edAn_codpag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpag.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpag.TabIndex = 632
    '
    'lbAn_giofiss
    '
    Me.lbAn_giofiss.AutoSize = True
    Me.lbAn_giofiss.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_giofiss.Location = New System.Drawing.Point(192, 186)
    Me.lbAn_giofiss.Name = "lbAn_giofiss"
    Me.lbAn_giofiss.NTSDbField = ""
    Me.lbAn_giofiss.Size = New System.Drawing.Size(63, 13)
    Me.lbAn_giofiss.TabIndex = 639
    Me.lbAn_giofiss.Tag = ""
    Me.lbAn_giofiss.Text = "Giorno fisso"
    Me.lbAn_giofiss.Tooltip = ""
    Me.lbAn_giofiss.UseMnemonic = False
    '
    'lbXx_codpag
    '
    Me.lbXx_codpag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpag.Location = New System.Drawing.Point(70, 38)
    Me.lbXx_codpag.Name = "lbXx_codpag"
    Me.lbXx_codpag.NTSDbField = ""
    Me.lbXx_codpag.Size = New System.Drawing.Size(116, 21)
    Me.lbXx_codpag.TabIndex = 634
    Me.lbXx_codpag.Tooltip = ""
    Me.lbXx_codpag.UseMnemonic = False
    '
    'edAn_mesees2
    '
    Me.edAn_mesees2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_mesees2.EditValue = "0"
    Me.edAn_mesees2.Location = New System.Drawing.Point(152, 183)
    Me.edAn_mesees2.Name = "edAn_mesees2"
    Me.edAn_mesees2.NTSDbField = ""
    Me.edAn_mesees2.NTSFormat = "0"
    Me.edAn_mesees2.NTSForzaVisZoom = False
    Me.edAn_mesees2.NTSOldValue = ""
    Me.edAn_mesees2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_mesees2.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_mesees2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_mesees2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_mesees2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_mesees2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_mesees2.Properties.AutoHeight = False
    Me.edAn_mesees2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_mesees2.Properties.MaxLength = 65536
    Me.edAn_mesees2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_mesees2.Size = New System.Drawing.Size(34, 20)
    Me.edAn_mesees2.TabIndex = 636
    '
    'lbAn_mesees
    '
    Me.lbAn_mesees.AutoSize = True
    Me.lbAn_mesees.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_mesees.Location = New System.Drawing.Point(5, 186)
    Me.lbAn_mesees.Name = "lbAn_mesees"
    Me.lbAn_mesees.NTSDbField = ""
    Me.lbAn_mesees.Size = New System.Drawing.Size(62, 13)
    Me.lbAn_mesees.TabIndex = 638
    Me.lbAn_mesees.Tag = ""
    Me.lbAn_mesees.Text = "Mesi esclusi"
    Me.lbAn_mesees.Tooltip = ""
    Me.lbAn_mesees.UseMnemonic = False
    '
    'edAn_mesees1
    '
    Me.edAn_mesees1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_mesees1.EditValue = "0"
    Me.edAn_mesees1.Location = New System.Drawing.Point(119, 183)
    Me.edAn_mesees1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.edAn_mesees1.Name = "edAn_mesees1"
    Me.edAn_mesees1.NTSDbField = ""
    Me.edAn_mesees1.NTSFormat = "0"
    Me.edAn_mesees1.NTSForzaVisZoom = False
    Me.edAn_mesees1.NTSOldValue = ""
    Me.edAn_mesees1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_mesees1.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_mesees1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_mesees1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_mesees1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_mesees1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_mesees1.Properties.AutoHeight = False
    Me.edAn_mesees1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_mesees1.Properties.MaxLength = 65536
    Me.edAn_mesees1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_mesees1.Size = New System.Drawing.Size(31, 20)
    Me.edAn_mesees1.TabIndex = 635
    '
    'lbAn_colbil
    '
    Me.lbAn_colbil.AutoSize = True
    Me.lbAn_colbil.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_colbil.Location = New System.Drawing.Point(9, 292)
    Me.lbAn_colbil.Name = "lbAn_colbil"
    Me.lbAn_colbil.NTSDbField = ""
    Me.lbAn_colbil.Size = New System.Drawing.Size(112, 13)
    Me.lbAn_colbil.TabIndex = 649
    Me.lbAn_colbil.Text = "Colonna in stampa bil."
    Me.lbAn_colbil.Tooltip = ""
    Me.lbAn_colbil.UseMnemonic = False
    '
    'cbAn_colbil
    '
    Me.cbAn_colbil.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_colbil.DataSource = Nothing
    Me.cbAn_colbil.DisplayMember = ""
    Me.cbAn_colbil.Location = New System.Drawing.Point(127, 289)
    Me.cbAn_colbil.Name = "cbAn_colbil"
    Me.cbAn_colbil.NTSDbField = ""
    Me.cbAn_colbil.Properties.AutoHeight = False
    Me.cbAn_colbil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_colbil.Properties.DropDownRows = 30
    Me.cbAn_colbil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_colbil.SelectedValue = ""
    Me.cbAn_colbil.Size = New System.Drawing.Size(216, 20)
    Me.cbAn_colbil.TabIndex = 648
    Me.cbAn_colbil.ValueMember = ""
    '
    'ckAn_intragr
    '
    Me.ckAn_intragr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_intragr.Location = New System.Drawing.Point(12, 246)
    Me.ckAn_intragr.Name = "ckAn_intragr"
    Me.ckAn_intragr.NTSCheckValue = "S"
    Me.ckAn_intragr.NTSUnCheckValue = "N"
    Me.ckAn_intragr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_intragr.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_intragr.Properties.AutoHeight = False
    Me.ckAn_intragr.Properties.Caption = "Cliente/fornitore intragruppo"
    Me.ckAn_intragr.Size = New System.Drawing.Size(169, 19)
    Me.ckAn_intragr.TabIndex = 620
    '
    'lbAn_codnscol
    '
    Me.lbAn_codnscol.AutoSize = True
    Me.lbAn_codnscol.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codnscol.Location = New System.Drawing.Point(11, 270)
    Me.lbAn_codnscol.Name = "lbAn_codnscol"
    Me.lbAn_codnscol.NTSDbField = ""
    Me.lbAn_codnscol.Size = New System.Drawing.Size(103, 13)
    Me.lbAn_codnscol.TabIndex = 647
    Me.lbAn_codnscol.Text = "Cod. nostro c/o loro"
    Me.lbAn_codnscol.Tooltip = ""
    Me.lbAn_codnscol.UseMnemonic = False
    '
    'edAn_codnscol
    '
    Me.edAn_codnscol.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codnscol.EditValue = ""
    Me.edAn_codnscol.Location = New System.Drawing.Point(127, 267)
    Me.edAn_codnscol.Name = "edAn_codnscol"
    Me.edAn_codnscol.NTSDbField = ""
    Me.edAn_codnscol.NTSForzaVisZoom = False
    Me.edAn_codnscol.NTSOldValue = ""
    Me.edAn_codnscol.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codnscol.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codnscol.Properties.AutoHeight = False
    Me.edAn_codnscol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codnscol.Properties.MaxLength = 65536
    Me.edAn_codnscol.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codnscol.Size = New System.Drawing.Size(216, 20)
    Me.edAn_codnscol.TabIndex = 646
    '
    'lbXx_controp
    '
    Me.lbXx_controp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(587, 24)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(178, 20)
    Me.lbXx_controp.TabIndex = 631
    Me.lbXx_controp.Text = "lbXx_controp"
    Me.lbXx_controp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbAn_contropLabel
    '
    Me.lbAn_contropLabel.AutoSize = True
    Me.lbAn_contropLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contropLabel.Location = New System.Drawing.Point(377, 28)
    Me.lbAn_contropLabel.Name = "lbAn_contropLabel"
    Me.lbAn_contropLabel.NTSDbField = ""
    Me.lbAn_contropLabel.Size = New System.Drawing.Size(113, 13)
    Me.lbAn_contropLabel.TabIndex = 629
    Me.lbAn_contropLabel.Text = "Contropartita abituale"
    Me.lbAn_contropLabel.Tooltip = ""
    Me.lbAn_contropLabel.UseMnemonic = False
    '
    'edAn_controp
    '
    Me.edAn_controp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_controp.EditValue = "0"
    Me.edAn_controp.Location = New System.Drawing.Point(493, 25)
    Me.edAn_controp.Name = "edAn_controp"
    Me.edAn_controp.NTSDbField = ""
    Me.edAn_controp.NTSFormat = "0"
    Me.edAn_controp.NTSForzaVisZoom = False
    Me.edAn_controp.NTSOldValue = ""
    Me.edAn_controp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_controp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_controp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_controp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_controp.Properties.AutoHeight = False
    Me.edAn_controp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_controp.Properties.MaxLength = 65536
    Me.edAn_controp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_controp.Size = New System.Drawing.Size(88, 20)
    Me.edAn_controp.TabIndex = 630
    '
    'fmRiclassificati
    '
    Me.fmRiclassificati.AllowDrop = True
    Me.fmRiclassificati.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmRiclassificati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRiclassificati.Appearance.Options.UseBackColor = True
    Me.fmRiclassificati.Controls.Add(Me.pnRiclassificazioni)
    Me.fmRiclassificati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRiclassificati.Location = New System.Drawing.Point(374, 205)
    Me.fmRiclassificati.Name = "fmRiclassificati"
    Me.fmRiclassificati.Size = New System.Drawing.Size(391, 109)
    Me.fmRiclassificati.TabIndex = 628
    Me.fmRiclassificati.Text = "Riclassificati su Excel"
    '
    'pnRiclassificazioni
    '
    Me.pnRiclassificazioni.AllowDrop = True
    Me.pnRiclassificazioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRiclassificazioni.Appearance.Options.UseBackColor = True
    Me.pnRiclassificazioni.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRiclassificazioni.Controls.Add(Me.lbAn_kpccee)
    Me.pnRiclassificazioni.Controls.Add(Me.cmdRiclassificazioni)
    Me.pnRiclassificazioni.Controls.Add(Me.lbAn_rifricd)
    Me.pnRiclassificazioni.Controls.Add(Me.lbRiclassif)
    Me.pnRiclassificazioni.Controls.Add(Me.edAn_rifricd)
    Me.pnRiclassificazioni.Controls.Add(Me.lbCee)
    Me.pnRiclassificazioni.Controls.Add(Me.edAn_rifrica)
    Me.pnRiclassificazioni.Controls.Add(Me.edAn_kpccee)
    Me.pnRiclassificazioni.Controls.Add(Me.lbAn_rifrica)
    Me.pnRiclassificazioni.Controls.Add(Me.edAn_kpccee2)
    Me.pnRiclassificazioni.Controls.Add(Me.lbAn_kpccee2)
    Me.pnRiclassificazioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRiclassificazioni.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnRiclassificazioni.Location = New System.Drawing.Point(2, 20)
    Me.pnRiclassificazioni.Name = "pnRiclassificazioni"
    Me.pnRiclassificazioni.NTSActiveTrasparency = True
    Me.pnRiclassificazioni.Size = New System.Drawing.Size(389, 87)
    Me.pnRiclassificazioni.TabIndex = 653
    Me.pnRiclassificazioni.Text = "NtsPanel1"
    '
    'lbAn_kpccee
    '
    Me.lbAn_kpccee.AutoSize = True
    Me.lbAn_kpccee.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_kpccee.Location = New System.Drawing.Point(8, 47)
    Me.lbAn_kpccee.Name = "lbAn_kpccee"
    Me.lbAn_kpccee.NTSDbField = ""
    Me.lbAn_kpccee.Size = New System.Drawing.Size(59, 13)
    Me.lbAn_kpccee.TabIndex = 532
    Me.lbAn_kpccee.Text = "Saldo Dare"
    Me.lbAn_kpccee.Tooltip = ""
    Me.lbAn_kpccee.UseMnemonic = False
    '
    'cmdRiclassificazioni
    '
    Me.cmdRiclassificazioni.ImagePath = ""
    Me.cmdRiclassificazioni.ImageText = ""
    Me.cmdRiclassificazioni.Location = New System.Drawing.Point(230, 0)
    Me.cmdRiclassificazioni.Name = "cmdRiclassificazioni"
    Me.cmdRiclassificazioni.NTSContextMenu = Nothing
    Me.cmdRiclassificazioni.Size = New System.Drawing.Size(159, 23)
    Me.cmdRiclassificazioni.TabIndex = 588
    Me.cmdRiclassificazioni.Text = "Altre riclassificazioni"
    '
    'lbAn_rifricd
    '
    Me.lbAn_rifricd.AutoSize = True
    Me.lbAn_rifricd.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rifricd.Location = New System.Drawing.Point(191, 47)
    Me.lbAn_rifricd.Name = "lbAn_rifricd"
    Me.lbAn_rifricd.NTSDbField = ""
    Me.lbAn_rifricd.Size = New System.Drawing.Size(59, 13)
    Me.lbAn_rifricd.TabIndex = 534
    Me.lbAn_rifricd.Text = "Saldo Dare"
    Me.lbAn_rifricd.Tooltip = ""
    Me.lbAn_rifricd.UseMnemonic = False
    '
    'lbRiclassif
    '
    Me.lbRiclassif.AutoSize = True
    Me.lbRiclassif.BackColor = System.Drawing.Color.Transparent
    Me.lbRiclassif.Location = New System.Drawing.Point(272, 26)
    Me.lbRiclassif.Name = "lbRiclassif"
    Me.lbRiclassif.NTSDbField = ""
    Me.lbRiclassif.Size = New System.Drawing.Size(68, 13)
    Me.lbRiclassif.TabIndex = 558
    Me.lbRiclassif.Text = "Riclassificato"
    Me.lbRiclassif.Tooltip = ""
    Me.lbRiclassif.UseMnemonic = False
    '
    'edAn_rifricd
    '
    Me.edAn_rifricd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rifricd.EditValue = ""
    Me.edAn_rifricd.Location = New System.Drawing.Point(265, 42)
    Me.edAn_rifricd.Name = "edAn_rifricd"
    Me.edAn_rifricd.NTSDbField = ""
    Me.edAn_rifricd.NTSForzaVisZoom = False
    Me.edAn_rifricd.NTSOldValue = ""
    Me.edAn_rifricd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_rifricd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_rifricd.Properties.AutoHeight = False
    Me.edAn_rifricd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_rifricd.Properties.MaxLength = 65536
    Me.edAn_rifricd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_rifricd.Size = New System.Drawing.Size(100, 20)
    Me.edAn_rifricd.TabIndex = 556
    '
    'lbCee
    '
    Me.lbCee.AutoSize = True
    Me.lbCee.BackColor = System.Drawing.Color.Transparent
    Me.lbCee.Location = New System.Drawing.Point(94, 26)
    Me.lbCee.Name = "lbCee"
    Me.lbCee.NTSDbField = ""
    Me.lbCee.Size = New System.Drawing.Size(64, 13)
    Me.lbCee.TabIndex = 556
    Me.lbCee.Text = "Bilancio CEE"
    Me.lbCee.Tooltip = ""
    Me.lbCee.UseMnemonic = False
    '
    'edAn_rifrica
    '
    Me.edAn_rifrica.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rifrica.EditValue = ""
    Me.edAn_rifrica.Location = New System.Drawing.Point(265, 64)
    Me.edAn_rifrica.Name = "edAn_rifrica"
    Me.edAn_rifrica.NTSDbField = ""
    Me.edAn_rifrica.NTSForzaVisZoom = False
    Me.edAn_rifrica.NTSOldValue = ""
    Me.edAn_rifrica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_rifrica.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_rifrica.Properties.AutoHeight = False
    Me.edAn_rifrica.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_rifrica.Properties.MaxLength = 65536
    Me.edAn_rifrica.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_rifrica.Size = New System.Drawing.Size(100, 20)
    Me.edAn_rifrica.TabIndex = 557
    '
    'edAn_kpccee
    '
    Me.edAn_kpccee.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_kpccee.EditValue = ""
    Me.edAn_kpccee.Location = New System.Drawing.Point(80, 42)
    Me.edAn_kpccee.Name = "edAn_kpccee"
    Me.edAn_kpccee.NTSDbField = ""
    Me.edAn_kpccee.NTSForzaVisZoom = False
    Me.edAn_kpccee.NTSOldValue = ""
    Me.edAn_kpccee.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_kpccee.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_kpccee.Properties.AutoHeight = False
    Me.edAn_kpccee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_kpccee.Properties.MaxLength = 65536
    Me.edAn_kpccee.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_kpccee.Size = New System.Drawing.Size(100, 20)
    Me.edAn_kpccee.TabIndex = 554
    '
    'lbAn_rifrica
    '
    Me.lbAn_rifrica.AutoSize = True
    Me.lbAn_rifrica.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rifrica.Location = New System.Drawing.Point(191, 67)
    Me.lbAn_rifrica.Name = "lbAn_rifrica"
    Me.lbAn_rifrica.NTSDbField = ""
    Me.lbAn_rifrica.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_rifrica.TabIndex = 535
    Me.lbAn_rifrica.Text = "Saldo Avere"
    Me.lbAn_rifrica.Tooltip = ""
    Me.lbAn_rifrica.UseMnemonic = False
    '
    'edAn_kpccee2
    '
    Me.edAn_kpccee2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_kpccee2.EditValue = ""
    Me.edAn_kpccee2.Location = New System.Drawing.Point(80, 64)
    Me.edAn_kpccee2.Name = "edAn_kpccee2"
    Me.edAn_kpccee2.NTSDbField = ""
    Me.edAn_kpccee2.NTSForzaVisZoom = False
    Me.edAn_kpccee2.NTSOldValue = ""
    Me.edAn_kpccee2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_kpccee2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_kpccee2.Properties.AutoHeight = False
    Me.edAn_kpccee2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_kpccee2.Properties.MaxLength = 65536
    Me.edAn_kpccee2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_kpccee2.Size = New System.Drawing.Size(100, 20)
    Me.edAn_kpccee2.TabIndex = 555
    '
    'lbAn_kpccee2
    '
    Me.lbAn_kpccee2.AutoSize = True
    Me.lbAn_kpccee2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_kpccee2.Location = New System.Drawing.Point(8, 67)
    Me.lbAn_kpccee2.Name = "lbAn_kpccee2"
    Me.lbAn_kpccee2.NTSDbField = ""
    Me.lbAn_kpccee2.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_kpccee2.TabIndex = 533
    Me.lbAn_kpccee2.Text = "Saldo Avere"
    Me.lbAn_kpccee2.Tooltip = ""
    Me.lbAn_kpccee2.UseMnemonic = False
    '
    'lbXx_contfatt
    '
    Me.lbXx_contfatt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_contfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_contfatt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_contfatt.Location = New System.Drawing.Point(587, 3)
    Me.lbXx_contfatt.Name = "lbXx_contfatt"
    Me.lbXx_contfatt.NTSDbField = ""
    Me.lbXx_contfatt.Size = New System.Drawing.Size(178, 20)
    Me.lbXx_contfatt.TabIndex = 625
    Me.lbXx_contfatt.Text = "lbXx_contfatt"
    Me.lbXx_contfatt.Tooltip = ""
    Me.lbXx_contfatt.UseMnemonic = False
    '
    'edAn_contfatt
    '
    Me.edAn_contfatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_contfatt.EditValue = "0"
    Me.edAn_contfatt.Location = New System.Drawing.Point(493, 3)
    Me.edAn_contfatt.Name = "edAn_contfatt"
    Me.edAn_contfatt.NTSDbField = ""
    Me.edAn_contfatt.NTSFormat = "0"
    Me.edAn_contfatt.NTSForzaVisZoom = False
    Me.edAn_contfatt.NTSOldValue = ""
    Me.edAn_contfatt.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_contfatt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_contfatt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_contfatt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_contfatt.Properties.AutoHeight = False
    Me.edAn_contfatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_contfatt.Properties.MaxLength = 65536
    Me.edAn_contfatt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_contfatt.Size = New System.Drawing.Size(88, 20)
    Me.edAn_contfatt.TabIndex = 626
    '
    'lbAn_contfatt
    '
    Me.lbAn_contfatt.AutoSize = True
    Me.lbAn_contfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contfatt.Location = New System.Drawing.Point(377, 6)
    Me.lbAn_contfatt.Name = "lbAn_contfatt"
    Me.lbAn_contfatt.NTSDbField = ""
    Me.lbAn_contfatt.Size = New System.Drawing.Size(98, 13)
    Me.lbAn_contfatt.TabIndex = 624
    Me.lbAn_contfatt.Text = "Conto fatturazione"
    Me.lbAn_contfatt.Tooltip = ""
    Me.lbAn_contfatt.UseMnemonic = False
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnFornitura)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage5.Text = "&5 - Cond. fornitura"
    '
    'pnFornitura
    '
    Me.pnFornitura.AllowDrop = True
    Me.pnFornitura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFornitura.Appearance.Options.UseBackColor = True
    Me.pnFornitura.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFornitura.Controls.Add(Me.lbAn_coduffpa)
    Me.pnFornitura.Controls.Add(Me.edAn_coduffpa)
    Me.pnFornitura.Controls.Add(Me.ckAn_webvis)
    Me.pnFornitura.Controls.Add(Me.lbAn_idmandrid)
    Me.pnFornitura.Controls.Add(Me.edAn_idmandrid)
    Me.pnFornitura.Controls.Add(Me.edAn_dtmandrid)
    Me.pnFornitura.Controls.Add(Me.cbAn_tiporid)
    Me.pnFornitura.Controls.Add(Me.lbRid)
    Me.pnFornitura.Controls.Add(Me.pnCondForndx)
    Me.pnFornitura.Controls.Add(Me.lbAn_iban)
    Me.pnFornitura.Controls.Add(Me.edAn_iban)
    Me.pnFornitura.Controls.Add(Me.ckAn_bolli)
    Me.pnFornitura.Controls.Add(Me.ckAn_spinc)
    Me.pnFornitura.Controls.Add(Me.ckAn_vuoti)
    Me.pnFornitura.Controls.Add(Me.pnCondFornsx)
    Me.pnFornitura.Controls.Add(Me.edAn_fido)
    Me.pnFornitura.Controls.Add(Me.lbAn_fido)
    Me.pnFornitura.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFornitura.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFornitura.Location = New System.Drawing.Point(0, 0)
    Me.pnFornitura.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnFornitura.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnFornitura.Name = "pnFornitura"
    Me.pnFornitura.NTSActiveTrasparency = True
    Me.pnFornitura.Size = New System.Drawing.Size(771, 317)
    Me.pnFornitura.TabIndex = 1
    Me.pnFornitura.Text = "NtsPanel3"
    '
    'lbAn_coduffpa
    '
    Me.lbAn_coduffpa.AutoSize = True
    Me.lbAn_coduffpa.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_coduffpa.Location = New System.Drawing.Point(424, 147)
    Me.lbAn_coduffpa.Name = "lbAn_coduffpa"
    Me.lbAn_coduffpa.NTSDbField = ""
    Me.lbAn_coduffpa.Size = New System.Drawing.Size(87, 13)
    Me.lbAn_coduffpa.TabIndex = 684
    Me.lbAn_coduffpa.Text = "Codice ufficio PA"
    Me.lbAn_coduffpa.Tooltip = ""
    Me.lbAn_coduffpa.UseMnemonic = False
    '
    'edAn_coduffpa
    '
    Me.edAn_coduffpa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_coduffpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_coduffpa.EditValue = ""
    Me.edAn_coduffpa.Location = New System.Drawing.Point(591, 144)
    Me.edAn_coduffpa.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_coduffpa.Name = "edAn_coduffpa"
    Me.edAn_coduffpa.NTSDbField = ""
    Me.edAn_coduffpa.NTSForzaVisZoom = False
    Me.edAn_coduffpa.NTSOldValue = ""
    Me.edAn_coduffpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_coduffpa.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_coduffpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_coduffpa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_coduffpa.Properties.AutoHeight = False
    Me.edAn_coduffpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_coduffpa.Properties.MaxLength = 65536
    Me.edAn_coduffpa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_coduffpa.Size = New System.Drawing.Size(174, 20)
    Me.edAn_coduffpa.TabIndex = 683
    '
    'ckAn_webvis
    '
    Me.ckAn_webvis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_webvis.Location = New System.Drawing.Point(427, 297)
    Me.ckAn_webvis.Name = "ckAn_webvis"
    Me.ckAn_webvis.NTSCheckValue = "S"
    Me.ckAn_webvis.NTSUnCheckValue = "N"
    Me.ckAn_webvis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_webvis.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_webvis.Properties.AutoHeight = False
    Me.ckAn_webvis.Properties.Caption = "Cliente/Fornitore visibile dall'applicazione esterna"
    Me.ckAn_webvis.Size = New System.Drawing.Size(271, 19)
    Me.ckAn_webvis.TabIndex = 682
    '
    'lbAn_idmandrid
    '
    Me.lbAn_idmandrid.AutoSize = True
    Me.lbAn_idmandrid.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_idmandrid.Location = New System.Drawing.Point(424, 194)
    Me.lbAn_idmandrid.Name = "lbAn_idmandrid"
    Me.lbAn_idmandrid.NTSDbField = ""
    Me.lbAn_idmandrid.Size = New System.Drawing.Size(88, 13)
    Me.lbAn_idmandrid.TabIndex = 681
    Me.lbAn_idmandrid.Text = "RID: ID mandato"
    Me.lbAn_idmandrid.Tooltip = ""
    Me.lbAn_idmandrid.UseMnemonic = False
    '
    'edAn_idmandrid
    '
    Me.edAn_idmandrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_idmandrid.EditValue = ""
    Me.edAn_idmandrid.Location = New System.Drawing.Point(591, 191)
    Me.edAn_idmandrid.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_idmandrid.Name = "edAn_idmandrid"
    Me.edAn_idmandrid.NTSDbField = ""
    Me.edAn_idmandrid.NTSForzaVisZoom = False
    Me.edAn_idmandrid.NTSOldValue = ""
    Me.edAn_idmandrid.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_idmandrid.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_idmandrid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_idmandrid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_idmandrid.Properties.AutoHeight = False
    Me.edAn_idmandrid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_idmandrid.Properties.MaxLength = 65536
    Me.edAn_idmandrid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_idmandrid.Size = New System.Drawing.Size(174, 20)
    Me.edAn_idmandrid.TabIndex = 680
    '
    'edAn_dtmandrid
    '
    Me.edAn_dtmandrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_dtmandrid.EditValue = "01/01/2000"
    Me.edAn_dtmandrid.Location = New System.Drawing.Point(684, 167)
    Me.edAn_dtmandrid.Name = "edAn_dtmandrid"
    Me.edAn_dtmandrid.NTSDbField = ""
    Me.edAn_dtmandrid.NTSForzaVisZoom = False
    Me.edAn_dtmandrid.NTSOldValue = ""
    Me.edAn_dtmandrid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_dtmandrid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_dtmandrid.Properties.AutoHeight = False
    Me.edAn_dtmandrid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_dtmandrid.Properties.MaxLength = 65536
    Me.edAn_dtmandrid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_dtmandrid.Size = New System.Drawing.Size(81, 20)
    Me.edAn_dtmandrid.TabIndex = 679
    '
    'cbAn_tiporid
    '
    Me.cbAn_tiporid.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_tiporid.DataSource = Nothing
    Me.cbAn_tiporid.DisplayMember = ""
    Me.cbAn_tiporid.Location = New System.Drawing.Point(591, 167)
    Me.cbAn_tiporid.Name = "cbAn_tiporid"
    Me.cbAn_tiporid.NTSDbField = ""
    Me.cbAn_tiporid.Properties.AutoHeight = False
    Me.cbAn_tiporid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_tiporid.Properties.DropDownRows = 30
    Me.cbAn_tiporid.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_tiporid.SelectedValue = ""
    Me.cbAn_tiporid.Size = New System.Drawing.Size(87, 20)
    Me.cbAn_tiporid.TabIndex = 678
    Me.cbAn_tiporid.ValueMember = ""
    '
    'lbRid
    '
    Me.lbRid.AutoSize = True
    Me.lbRid.BackColor = System.Drawing.Color.Transparent
    Me.lbRid.Location = New System.Drawing.Point(424, 170)
    Me.lbRid.Name = "lbRid"
    Me.lbRid.NTSDbField = ""
    Me.lbRid.Size = New System.Drawing.Size(169, 13)
    Me.lbRid.TabIndex = 677
    Me.lbRid.Text = "RID: tipo addebito/ data mandato"
    Me.lbRid.Tooltip = ""
    Me.lbRid.UseMnemonic = False
    '
    'pnCondForndx
    '
    Me.pnCondForndx.AllowDrop = True
    Me.pnCondForndx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCondForndx.Appearance.Options.UseBackColor = True
    Me.pnCondForndx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCondForndx.Controls.Add(Me.lbAn_fatt)
    Me.pnCondForndx.Controls.Add(Me.cbAn_blocco)
    Me.pnCondForndx.Controls.Add(Me.cbAn_fatt)
    Me.pnCondForndx.Controls.Add(Me.lbAn_blocco)
    Me.pnCondForndx.Controls.Add(Me.lbAn_perfatt)
    Me.pnCondForndx.Controls.Add(Me.cbAn_status)
    Me.pnCondForndx.Controls.Add(Me.cbAn_perfatt)
    Me.pnCondForndx.Controls.Add(Me.lbAn_status)
    Me.pnCondForndx.Controls.Add(Me.lbAn_gcons)
    Me.pnCondForndx.Controls.Add(Me.cbAn_gcons)
    Me.pnCondForndx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCondForndx.Location = New System.Drawing.Point(419, 1)
    Me.pnCondForndx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCondForndx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCondForndx.Name = "pnCondForndx"
    Me.pnCondForndx.NTSActiveTrasparency = True
    Me.pnCondForndx.Size = New System.Drawing.Size(349, 115)
    Me.pnCondForndx.TabIndex = 676
    Me.pnCondForndx.Text = "NtsPanel1"
    '
    'lbAn_fatt
    '
    Me.lbAn_fatt.AutoSize = True
    Me.lbAn_fatt.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_fatt.Location = New System.Drawing.Point(4, 6)
    Me.lbAn_fatt.Name = "lbAn_fatt"
    Me.lbAn_fatt.NTSDbField = ""
    Me.lbAn_fatt.Size = New System.Drawing.Size(89, 13)
    Me.lbAn_fatt.TabIndex = 603
    Me.lbAn_fatt.Text = "Tipo fatturazione"
    Me.lbAn_fatt.Tooltip = ""
    Me.lbAn_fatt.UseMnemonic = False
    '
    'cbAn_blocco
    '
    Me.cbAn_blocco.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_blocco.DataSource = Nothing
    Me.cbAn_blocco.DisplayMember = ""
    Me.cbAn_blocco.Location = New System.Drawing.Point(171, 93)
    Me.cbAn_blocco.Name = "cbAn_blocco"
    Me.cbAn_blocco.NTSDbField = ""
    Me.cbAn_blocco.Properties.AutoHeight = False
    Me.cbAn_blocco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_blocco.Properties.DropDownRows = 30
    Me.cbAn_blocco.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_blocco.SelectedValue = ""
    Me.cbAn_blocco.Size = New System.Drawing.Size(174, 20)
    Me.cbAn_blocco.TabIndex = 675
    Me.cbAn_blocco.ValueMember = ""
    '
    'cbAn_fatt
    '
    Me.cbAn_fatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_fatt.DataSource = Nothing
    Me.cbAn_fatt.DisplayMember = ""
    Me.cbAn_fatt.Location = New System.Drawing.Point(171, 3)
    Me.cbAn_fatt.Name = "cbAn_fatt"
    Me.cbAn_fatt.NTSDbField = ""
    Me.cbAn_fatt.Properties.AutoHeight = False
    Me.cbAn_fatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_fatt.Properties.DropDownRows = 30
    Me.cbAn_fatt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_fatt.SelectedValue = ""
    Me.cbAn_fatt.Size = New System.Drawing.Size(174, 20)
    Me.cbAn_fatt.TabIndex = 604
    Me.cbAn_fatt.ValueMember = ""
    '
    'lbAn_blocco
    '
    Me.lbAn_blocco.AutoSize = True
    Me.lbAn_blocco.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_blocco.Location = New System.Drawing.Point(4, 96)
    Me.lbAn_blocco.Name = "lbAn_blocco"
    Me.lbAn_blocco.NTSDbField = ""
    Me.lbAn_blocco.Size = New System.Drawing.Size(67, 13)
    Me.lbAn_blocco.TabIndex = 674
    Me.lbAn_blocco.Text = "Blocco conto"
    Me.lbAn_blocco.Tooltip = ""
    Me.lbAn_blocco.UseMnemonic = False
    '
    'lbAn_perfatt
    '
    Me.lbAn_perfatt.AutoSize = True
    Me.lbAn_perfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_perfatt.Location = New System.Drawing.Point(4, 28)
    Me.lbAn_perfatt.Name = "lbAn_perfatt"
    Me.lbAn_perfatt.NTSDbField = ""
    Me.lbAn_perfatt.Size = New System.Drawing.Size(105, 13)
    Me.lbAn_perfatt.TabIndex = 668
    Me.lbAn_perfatt.Text = "Periodo fatturazione"
    Me.lbAn_perfatt.Tooltip = ""
    Me.lbAn_perfatt.UseMnemonic = False
    '
    'cbAn_status
    '
    Me.cbAn_status.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_status.DataSource = Nothing
    Me.cbAn_status.DisplayMember = ""
    Me.cbAn_status.Location = New System.Drawing.Point(171, 71)
    Me.cbAn_status.Name = "cbAn_status"
    Me.cbAn_status.NTSDbField = ""
    Me.cbAn_status.Properties.AutoHeight = False
    Me.cbAn_status.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_status.Properties.DropDownRows = 30
    Me.cbAn_status.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_status.SelectedValue = ""
    Me.cbAn_status.Size = New System.Drawing.Size(174, 20)
    Me.cbAn_status.TabIndex = 673
    Me.cbAn_status.ValueMember = ""
    '
    'cbAn_perfatt
    '
    Me.cbAn_perfatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_perfatt.DataSource = Nothing
    Me.cbAn_perfatt.DisplayMember = ""
    Me.cbAn_perfatt.Location = New System.Drawing.Point(171, 25)
    Me.cbAn_perfatt.Name = "cbAn_perfatt"
    Me.cbAn_perfatt.NTSDbField = ""
    Me.cbAn_perfatt.Properties.AutoHeight = False
    Me.cbAn_perfatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_perfatt.Properties.DropDownRows = 30
    Me.cbAn_perfatt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_perfatt.SelectedValue = ""
    Me.cbAn_perfatt.Size = New System.Drawing.Size(174, 20)
    Me.cbAn_perfatt.TabIndex = 669
    Me.cbAn_perfatt.ValueMember = ""
    '
    'lbAn_status
    '
    Me.lbAn_status.AutoSize = True
    Me.lbAn_status.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_status.Location = New System.Drawing.Point(4, 74)
    Me.lbAn_status.Name = "lbAn_status"
    Me.lbAn_status.NTSDbField = ""
    Me.lbAn_status.Size = New System.Drawing.Size(38, 13)
    Me.lbAn_status.TabIndex = 672
    Me.lbAn_status.Text = "Status"
    Me.lbAn_status.Tooltip = ""
    Me.lbAn_status.UseMnemonic = False
    '
    'lbAn_gcons
    '
    Me.lbAn_gcons.AutoSize = True
    Me.lbAn_gcons.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_gcons.Location = New System.Drawing.Point(4, 51)
    Me.lbAn_gcons.Name = "lbAn_gcons"
    Me.lbAn_gcons.NTSDbField = ""
    Me.lbAn_gcons.Size = New System.Drawing.Size(98, 13)
    Me.lbAn_gcons.TabIndex = 670
    Me.lbAn_gcons.Text = "Giorno di consegna"
    Me.lbAn_gcons.Tooltip = ""
    Me.lbAn_gcons.UseMnemonic = False
    '
    'cbAn_gcons
    '
    Me.cbAn_gcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_gcons.DataSource = Nothing
    Me.cbAn_gcons.DisplayMember = ""
    Me.cbAn_gcons.Location = New System.Drawing.Point(171, 48)
    Me.cbAn_gcons.Name = "cbAn_gcons"
    Me.cbAn_gcons.NTSDbField = ""
    Me.cbAn_gcons.Properties.AutoHeight = False
    Me.cbAn_gcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_gcons.Properties.DropDownRows = 30
    Me.cbAn_gcons.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_gcons.SelectedValue = ""
    Me.cbAn_gcons.Size = New System.Drawing.Size(174, 20)
    Me.cbAn_gcons.TabIndex = 671
    Me.cbAn_gcons.ValueMember = ""
    '
    'lbAn_iban
    '
    Me.lbAn_iban.AutoSize = True
    Me.lbAn_iban.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_iban.Location = New System.Drawing.Point(424, 122)
    Me.lbAn_iban.Name = "lbAn_iban"
    Me.lbAn_iban.NTSDbField = ""
    Me.lbAn_iban.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_iban.TabIndex = 668
    Me.lbAn_iban.Text = "IBAN estero"
    Me.lbAn_iban.Tooltip = ""
    Me.lbAn_iban.UseMnemonic = False
    '
    'edAn_iban
    '
    Me.edAn_iban.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_iban.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_iban.EditValue = ""
    Me.edAn_iban.Location = New System.Drawing.Point(591, 119)
    Me.edAn_iban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_iban.Name = "edAn_iban"
    Me.edAn_iban.NTSDbField = ""
    Me.edAn_iban.NTSForzaVisZoom = False
    Me.edAn_iban.NTSOldValue = ""
    Me.edAn_iban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_iban.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_iban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_iban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_iban.Properties.AutoHeight = False
    Me.edAn_iban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_iban.Properties.MaxLength = 65536
    Me.edAn_iban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_iban.Size = New System.Drawing.Size(174, 20)
    Me.edAn_iban.TabIndex = 667
    '
    'ckAn_bolli
    '
    Me.ckAn_bolli.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckAn_bolli.Location = New System.Drawing.Point(427, 257)
    Me.ckAn_bolli.Name = "ckAn_bolli"
    Me.ckAn_bolli.NTSCheckValue = "S"
    Me.ckAn_bolli.NTSUnCheckValue = "N"
    Me.ckAn_bolli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_bolli.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_bolli.Properties.AutoHeight = False
    Me.ckAn_bolli.Properties.Caption = "Addebito &Bolli"
    Me.ckAn_bolli.Size = New System.Drawing.Size(86, 19)
    Me.ckAn_bolli.TabIndex = 667
    '
    'ckAn_spinc
    '
    Me.ckAn_spinc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_spinc.Location = New System.Drawing.Point(427, 237)
    Me.ckAn_spinc.Name = "ckAn_spinc"
    Me.ckAn_spinc.NTSCheckValue = "S"
    Me.ckAn_spinc.NTSUnCheckValue = "N"
    Me.ckAn_spinc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_spinc.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_spinc.Properties.AutoHeight = False
    Me.ckAn_spinc.Properties.Caption = "Addebito sp. &Incasso"
    Me.ckAn_spinc.Size = New System.Drawing.Size(128, 19)
    Me.ckAn_spinc.TabIndex = 666
    '
    'ckAn_vuoti
    '
    Me.ckAn_vuoti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_vuoti.Location = New System.Drawing.Point(427, 277)
    Me.ckAn_vuoti.Name = "ckAn_vuoti"
    Me.ckAn_vuoti.NTSCheckValue = "S"
    Me.ckAn_vuoti.NTSUnCheckValue = "N"
    Me.ckAn_vuoti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_vuoti.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_vuoti.Properties.AutoHeight = False
    Me.ckAn_vuoti.Properties.Caption = "Addebito &Cauzioni / spese generali"
    Me.ckAn_vuoti.Size = New System.Drawing.Size(192, 19)
    Me.ckAn_vuoti.TabIndex = 665
    '
    'pnCondFornsx
    '
    Me.pnCondFornsx.AllowDrop = True
    Me.pnCondFornsx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCondFornsx.Appearance.Options.UseBackColor = True
    Me.pnCondFornsx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCondFornsx.Controls.Add(Me.lbAn_acuradi)
    Me.pnCondFornsx.Controls.Add(Me.cbAn_acuradi)
    Me.pnCondFornsx.Controls.Add(Me.fmIbanitalia)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_porto)
    Me.pnCondFornsx.Controls.Add(Me.edAn_codbanc)
    Me.pnCondFornsx.Controls.Add(Me.edAn_porto)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_porto)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_codbanc)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_codtpbf)
    Me.pnCondFornsx.Controls.Add(Me.edAn_codtpbf)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_vett2)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_codbanc)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_codtpbf)
    Me.pnCondFornsx.Controls.Add(Me.edAn_vett2)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_vett2)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_listino)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_vett)
    Me.pnCondFornsx.Controls.Add(Me.edAn_vett)
    Me.pnCondFornsx.Controls.Add(Me.edAn_listino)
    Me.pnCondFornsx.Controls.Add(Me.lbAn_vett)
    Me.pnCondFornsx.Controls.Add(Me.lbXx_listino)
    Me.pnCondFornsx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCondFornsx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCondFornsx.Location = New System.Drawing.Point(0, 0)
    Me.pnCondFornsx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCondFornsx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCondFornsx.Name = "pnCondFornsx"
    Me.pnCondFornsx.NTSActiveTrasparency = True
    Me.pnCondFornsx.Size = New System.Drawing.Size(417, 317)
    Me.pnCondFornsx.TabIndex = 664
    Me.pnCondFornsx.Text = "NtsPanel1"
    '
    'lbAn_acuradi
    '
    Me.lbAn_acuradi.AutoSize = True
    Me.lbAn_acuradi.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_acuradi.Location = New System.Drawing.Point(8, 252)
    Me.lbAn_acuradi.Name = "lbAn_acuradi"
    Me.lbAn_acuradi.NTSDbField = ""
    Me.lbAn_acuradi.Size = New System.Drawing.Size(71, 13)
    Me.lbAn_acuradi.TabIndex = 679
    Me.lbAn_acuradi.Text = "Trasp. a cura"
    Me.lbAn_acuradi.Tooltip = ""
    Me.lbAn_acuradi.UseMnemonic = False
    '
    'cbAn_acuradi
    '
    Me.cbAn_acuradi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_acuradi.DataSource = Nothing
    Me.cbAn_acuradi.DisplayMember = ""
    Me.cbAn_acuradi.Location = New System.Drawing.Point(89, 249)
    Me.cbAn_acuradi.Name = "cbAn_acuradi"
    Me.cbAn_acuradi.NTSDbField = ""
    Me.cbAn_acuradi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbAn_acuradi.Properties.Appearance.Options.UseBackColor = True
    Me.cbAn_acuradi.Properties.AutoHeight = False
    Me.cbAn_acuradi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_acuradi.Properties.DropDownRows = 30
    Me.cbAn_acuradi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_acuradi.SelectedValue = ""
    Me.cbAn_acuradi.Size = New System.Drawing.Size(262, 20)
    Me.cbAn_acuradi.TabIndex = 678
    Me.cbAn_acuradi.ValueMember = ""
    '
    'fmIbanitalia
    '
    Me.fmIbanitalia.AllowDrop = True
    Me.fmIbanitalia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIbanitalia.Appearance.Options.UseBackColor = True
    Me.fmIbanitalia.Controls.Add(Me.lbAn_swift)
    Me.fmIbanitalia.Controls.Add(Me.edAn_swift)
    Me.fmIbanitalia.Controls.Add(Me.lbAn_abi)
    Me.fmIbanitalia.Controls.Add(Me.lbAn_rifriba)
    Me.fmIbanitalia.Controls.Add(Me.lbAn_prefiban)
    Me.fmIbanitalia.Controls.Add(Me.edAn_rifriba)
    Me.fmIbanitalia.Controls.Add(Me.edAn_prefiban)
    Me.fmIbanitalia.Controls.Add(Me.edAn_banc2)
    Me.fmIbanitalia.Controls.Add(Me.edAn_cin)
    Me.fmIbanitalia.Controls.Add(Me.edAn_banc1)
    Me.fmIbanitalia.Controls.Add(Me.edAn_cab)
    Me.fmIbanitalia.Controls.Add(Me.edAn_abi)
    Me.fmIbanitalia.Controls.Add(Me.lbAn_cab)
    Me.fmIbanitalia.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIbanitalia.Location = New System.Drawing.Point(3, 0)
    Me.fmIbanitalia.Name = "fmIbanitalia"
    Me.fmIbanitalia.Size = New System.Drawing.Size(410, 149)
    Me.fmIbanitalia.TabIndex = 677
    Me.fmIbanitalia.Text = "IBAN italia"
    '
    'lbAn_swift
    '
    Me.lbAn_swift.AutoSize = True
    Me.lbAn_swift.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_swift.Location = New System.Drawing.Point(5, 122)
    Me.lbAn_swift.Name = "lbAn_swift"
    Me.lbAn_swift.NTSDbField = ""
    Me.lbAn_swift.Size = New System.Drawing.Size(48, 13)
    Me.lbAn_swift.TabIndex = 670
    Me.lbAn_swift.Text = "Bic/Swift"
    Me.lbAn_swift.Tooltip = ""
    Me.lbAn_swift.UseMnemonic = False
    '
    'edAn_swift
    '
    Me.edAn_swift.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_swift.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_swift.EditValue = ""
    Me.edAn_swift.Location = New System.Drawing.Point(86, 119)
    Me.edAn_swift.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_swift.Name = "edAn_swift"
    Me.edAn_swift.NTSDbField = ""
    Me.edAn_swift.NTSForzaVisZoom = False
    Me.edAn_swift.NTSOldValue = ""
    Me.edAn_swift.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_swift.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_swift.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_swift.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_swift.Properties.AutoHeight = False
    Me.edAn_swift.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_swift.Properties.MaxLength = 65536
    Me.edAn_swift.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_swift.Size = New System.Drawing.Size(319, 20)
    Me.edAn_swift.TabIndex = 669
    '
    'lbAn_abi
    '
    Me.lbAn_abi.AutoSize = True
    Me.lbAn_abi.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_abi.Location = New System.Drawing.Point(5, 31)
    Me.lbAn_abi.Name = "lbAn_abi"
    Me.lbAn_abi.NTSDbField = ""
    Me.lbAn_abi.Size = New System.Drawing.Size(54, 13)
    Me.lbAn_abi.TabIndex = 5
    Me.lbAn_abi.Text = "Abi Banca"
    Me.lbAn_abi.Tooltip = ""
    Me.lbAn_abi.UseMnemonic = False
    '
    'lbAn_rifriba
    '
    Me.lbAn_rifriba.AutoSize = True
    Me.lbAn_rifriba.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rifriba.Location = New System.Drawing.Point(5, 76)
    Me.lbAn_rifriba.Name = "lbAn_rifriba"
    Me.lbAn_rifriba.NTSDbField = ""
    Me.lbAn_rifriba.Size = New System.Drawing.Size(68, 13)
    Me.lbAn_rifriba.TabIndex = 649
    Me.lbAn_rifriba.Text = "C/Corr. / Cin"
    Me.lbAn_rifriba.Tooltip = ""
    Me.lbAn_rifriba.UseMnemonic = False
    '
    'lbAn_prefiban
    '
    Me.lbAn_prefiban.AutoSize = True
    Me.lbAn_prefiban.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_prefiban.Location = New System.Drawing.Point(5, 99)
    Me.lbAn_prefiban.Name = "lbAn_prefiban"
    Me.lbAn_prefiban.NTSDbField = ""
    Me.lbAn_prefiban.Size = New System.Drawing.Size(72, 13)
    Me.lbAn_prefiban.TabIndex = 666
    Me.lbAn_prefiban.Text = "Prefisso IBAN"
    Me.lbAn_prefiban.Tooltip = ""
    Me.lbAn_prefiban.UseMnemonic = False
    '
    'edAn_rifriba
    '
    Me.edAn_rifriba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rifriba.EditValue = ""
    Me.edAn_rifriba.Location = New System.Drawing.Point(86, 73)
    Me.edAn_rifriba.Name = "edAn_rifriba"
    Me.edAn_rifriba.NTSDbField = ""
    Me.edAn_rifriba.NTSForzaVisZoom = False
    Me.edAn_rifriba.NTSOldValue = ""
    Me.edAn_rifriba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_rifriba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_rifriba.Properties.AutoHeight = False
    Me.edAn_rifriba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_rifriba.Properties.MaxLength = 65536
    Me.edAn_rifriba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_rifriba.Size = New System.Drawing.Size(289, 20)
    Me.edAn_rifriba.TabIndex = 648
    '
    'edAn_prefiban
    '
    Me.edAn_prefiban.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_prefiban.EditValue = ""
    Me.edAn_prefiban.Location = New System.Drawing.Point(86, 96)
    Me.edAn_prefiban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_prefiban.Name = "edAn_prefiban"
    Me.edAn_prefiban.NTSDbField = ""
    Me.edAn_prefiban.NTSForzaVisZoom = False
    Me.edAn_prefiban.NTSOldValue = ""
    Me.edAn_prefiban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_prefiban.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_prefiban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_prefiban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_prefiban.Properties.AutoHeight = False
    Me.edAn_prefiban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_prefiban.Properties.MaxLength = 65536
    Me.edAn_prefiban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_prefiban.Size = New System.Drawing.Size(120, 20)
    Me.edAn_prefiban.TabIndex = 665
    '
    'edAn_banc2
    '
    Me.edAn_banc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_banc2.EditValue = ""
    Me.edAn_banc2.Location = New System.Drawing.Point(175, 50)
    Me.edAn_banc2.Name = "edAn_banc2"
    Me.edAn_banc2.NTSDbField = ""
    Me.edAn_banc2.NTSForzaVisZoom = False
    Me.edAn_banc2.NTSOldValue = ""
    Me.edAn_banc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_banc2.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_banc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_banc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_banc2.Properties.AutoHeight = False
    Me.edAn_banc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_banc2.Properties.MaxLength = 65536
    Me.edAn_banc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_banc2.Size = New System.Drawing.Size(230, 20)
    Me.edAn_banc2.TabIndex = 50
    '
    'edAn_cin
    '
    Me.edAn_cin.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_cin.EditValue = ""
    Me.edAn_cin.Location = New System.Drawing.Point(381, 73)
    Me.edAn_cin.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edAn_cin.Name = "edAn_cin"
    Me.edAn_cin.NTSDbField = ""
    Me.edAn_cin.NTSForzaVisZoom = False
    Me.edAn_cin.NTSOldValue = ""
    Me.edAn_cin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_cin.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_cin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_cin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_cin.Properties.AutoHeight = False
    Me.edAn_cin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_cin.Properties.MaxLength = 65536
    Me.edAn_cin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_cin.Size = New System.Drawing.Size(24, 20)
    Me.edAn_cin.TabIndex = 664
    '
    'edAn_banc1
    '
    Me.edAn_banc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_banc1.EditValue = ""
    Me.edAn_banc1.Location = New System.Drawing.Point(175, 28)
    Me.edAn_banc1.Name = "edAn_banc1"
    Me.edAn_banc1.NTSDbField = ""
    Me.edAn_banc1.NTSForzaVisZoom = False
    Me.edAn_banc1.NTSOldValue = ""
    Me.edAn_banc1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_banc1.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_banc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_banc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_banc1.Properties.AutoHeight = False
    Me.edAn_banc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_banc1.Properties.MaxLength = 65536
    Me.edAn_banc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_banc1.Size = New System.Drawing.Size(230, 20)
    Me.edAn_banc1.TabIndex = 49
    '
    'edAn_cab
    '
    Me.edAn_cab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_cab.EditValue = "0"
    Me.edAn_cab.Location = New System.Drawing.Point(86, 50)
    Me.edAn_cab.Name = "edAn_cab"
    Me.edAn_cab.NTSDbField = ""
    Me.edAn_cab.NTSFormat = "0"
    Me.edAn_cab.NTSForzaVisZoom = False
    Me.edAn_cab.NTSOldValue = ""
    Me.edAn_cab.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_cab.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_cab.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_cab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_cab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_cab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_cab.Properties.AutoHeight = False
    Me.edAn_cab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_cab.Properties.MaxLength = 65536
    Me.edAn_cab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_cab.Size = New System.Drawing.Size(83, 20)
    Me.edAn_cab.TabIndex = 48
    '
    'edAn_abi
    '
    Me.edAn_abi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_abi.EditValue = "0"
    Me.edAn_abi.Location = New System.Drawing.Point(86, 28)
    Me.edAn_abi.Name = "edAn_abi"
    Me.edAn_abi.NTSDbField = ""
    Me.edAn_abi.NTSFormat = "0"
    Me.edAn_abi.NTSForzaVisZoom = False
    Me.edAn_abi.NTSOldValue = ""
    Me.edAn_abi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_abi.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_abi.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_abi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_abi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_abi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_abi.Properties.AutoHeight = False
    Me.edAn_abi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_abi.Properties.MaxLength = 65536
    Me.edAn_abi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_abi.Size = New System.Drawing.Size(83, 20)
    Me.edAn_abi.TabIndex = 47
    '
    'lbAn_cab
    '
    Me.lbAn_cab.AutoSize = True
    Me.lbAn_cab.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cab.Location = New System.Drawing.Point(5, 53)
    Me.lbAn_cab.Name = "lbAn_cab"
    Me.lbAn_cab.NTSDbField = ""
    Me.lbAn_cab.Size = New System.Drawing.Size(55, 13)
    Me.lbAn_cab.TabIndex = 6
    Me.lbAn_cab.Text = "Cab Filiale"
    Me.lbAn_cab.Tooltip = ""
    Me.lbAn_cab.UseMnemonic = False
    '
    'lbXx_porto
    '
    Me.lbXx_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_porto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_porto.Location = New System.Drawing.Point(146, 205)
    Me.lbXx_porto.Name = "lbXx_porto"
    Me.lbXx_porto.NTSDbField = ""
    Me.lbXx_porto.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_porto.TabIndex = 663
    Me.lbXx_porto.Text = "lbXx_porto"
    Me.lbXx_porto.Tooltip = ""
    Me.lbXx_porto.UseMnemonic = False
    '
    'edAn_codbanc
    '
    Me.edAn_codbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codbanc.EditValue = "0"
    Me.edAn_codbanc.Location = New System.Drawing.Point(89, 161)
    Me.edAn_codbanc.Name = "edAn_codbanc"
    Me.edAn_codbanc.NTSDbField = ""
    Me.edAn_codbanc.NTSFormat = "0"
    Me.edAn_codbanc.NTSForzaVisZoom = False
    Me.edAn_codbanc.NTSOldValue = ""
    Me.edAn_codbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codbanc.Properties.AutoHeight = False
    Me.edAn_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codbanc.Properties.MaxLength = 65536
    Me.edAn_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codbanc.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codbanc.TabIndex = 613
    '
    'edAn_porto
    '
    Me.edAn_porto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_porto.EditValue = ""
    Me.edAn_porto.Location = New System.Drawing.Point(89, 205)
    Me.edAn_porto.Name = "edAn_porto"
    Me.edAn_porto.NTSDbField = ""
    Me.edAn_porto.NTSForzaVisZoom = False
    Me.edAn_porto.NTSOldValue = ""
    Me.edAn_porto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_porto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_porto.Properties.AutoHeight = False
    Me.edAn_porto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_porto.Properties.MaxLength = 65536
    Me.edAn_porto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_porto.Size = New System.Drawing.Size(52, 20)
    Me.edAn_porto.TabIndex = 662
    '
    'lbAn_porto
    '
    Me.lbAn_porto.AutoSize = True
    Me.lbAn_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_porto.Location = New System.Drawing.Point(8, 208)
    Me.lbAn_porto.Name = "lbAn_porto"
    Me.lbAn_porto.NTSDbField = ""
    Me.lbAn_porto.Size = New System.Drawing.Size(33, 13)
    Me.lbAn_porto.TabIndex = 661
    Me.lbAn_porto.Text = "Porto"
    Me.lbAn_porto.Tooltip = ""
    Me.lbAn_porto.UseMnemonic = False
    '
    'lbAn_codbanc
    '
    Me.lbAn_codbanc.AutoSize = True
    Me.lbAn_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codbanc.Location = New System.Drawing.Point(7, 164)
    Me.lbAn_codbanc.Name = "lbAn_codbanc"
    Me.lbAn_codbanc.NTSDbField = ""
    Me.lbAn_codbanc.Size = New System.Drawing.Size(71, 13)
    Me.lbAn_codbanc.TabIndex = 611
    Me.lbAn_codbanc.Text = "Nostra banca"
    Me.lbAn_codbanc.Tooltip = ""
    Me.lbAn_codbanc.UseMnemonic = False
    '
    'lbAn_codtpbf
    '
    Me.lbAn_codtpbf.AutoSize = True
    Me.lbAn_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codtpbf.Location = New System.Drawing.Point(8, 230)
    Me.lbAn_codtpbf.Name = "lbAn_codtpbf"
    Me.lbAn_codtpbf.NTSDbField = ""
    Me.lbAn_codtpbf.Size = New System.Drawing.Size(78, 13)
    Me.lbAn_codtpbf.TabIndex = 658
    Me.lbAn_codtpbf.Text = "Tipo bolla/fatt."
    Me.lbAn_codtpbf.Tooltip = ""
    Me.lbAn_codtpbf.UseMnemonic = False
    '
    'edAn_codtpbf
    '
    Me.edAn_codtpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codtpbf.EditValue = "0"
    Me.edAn_codtpbf.Location = New System.Drawing.Point(89, 227)
    Me.edAn_codtpbf.Name = "edAn_codtpbf"
    Me.edAn_codtpbf.NTSDbField = ""
    Me.edAn_codtpbf.NTSFormat = "0"
    Me.edAn_codtpbf.NTSForzaVisZoom = False
    Me.edAn_codtpbf.NTSOldValue = ""
    Me.edAn_codtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codtpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codtpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codtpbf.Properties.AutoHeight = False
    Me.edAn_codtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codtpbf.Properties.MaxLength = 65536
    Me.edAn_codtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codtpbf.Size = New System.Drawing.Size(52, 20)
    Me.edAn_codtpbf.TabIndex = 660
    '
    'lbXx_vett2
    '
    Me.lbXx_vett2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vett2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vett2.Location = New System.Drawing.Point(146, 293)
    Me.lbXx_vett2.Name = "lbXx_vett2"
    Me.lbXx_vett2.NTSDbField = ""
    Me.lbXx_vett2.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_vett2.TabIndex = 656
    Me.lbXx_vett2.Text = "lbXx_vett2"
    Me.lbXx_vett2.Tooltip = ""
    Me.lbXx_vett2.UseMnemonic = False
    '
    'lbXx_codbanc
    '
    Me.lbXx_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codbanc.Location = New System.Drawing.Point(146, 161)
    Me.lbXx_codbanc.Name = "lbXx_codbanc"
    Me.lbXx_codbanc.NTSDbField = ""
    Me.lbXx_codbanc.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_codbanc.TabIndex = 612
    Me.lbXx_codbanc.Text = "lbXx_codbanc"
    Me.lbXx_codbanc.Tooltip = ""
    Me.lbXx_codbanc.UseMnemonic = False
    '
    'lbXx_codtpbf
    '
    Me.lbXx_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtpbf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtpbf.Location = New System.Drawing.Point(146, 227)
    Me.lbXx_codtpbf.Name = "lbXx_codtpbf"
    Me.lbXx_codtpbf.NTSDbField = ""
    Me.lbXx_codtpbf.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_codtpbf.TabIndex = 659
    Me.lbXx_codtpbf.Text = "lbXx_codtpbf"
    Me.lbXx_codtpbf.Tooltip = ""
    Me.lbXx_codtpbf.UseMnemonic = False
    '
    'edAn_vett2
    '
    Me.edAn_vett2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_vett2.EditValue = "0"
    Me.edAn_vett2.Location = New System.Drawing.Point(89, 293)
    Me.edAn_vett2.Name = "edAn_vett2"
    Me.edAn_vett2.NTSDbField = ""
    Me.edAn_vett2.NTSFormat = "0"
    Me.edAn_vett2.NTSForzaVisZoom = False
    Me.edAn_vett2.NTSOldValue = ""
    Me.edAn_vett2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_vett2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_vett2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_vett2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_vett2.Properties.AutoHeight = False
    Me.edAn_vett2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_vett2.Properties.MaxLength = 65536
    Me.edAn_vett2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_vett2.Size = New System.Drawing.Size(52, 20)
    Me.edAn_vett2.TabIndex = 657
    '
    'lbAn_vett2
    '
    Me.lbAn_vett2.AutoSize = True
    Me.lbAn_vett2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_vett2.Location = New System.Drawing.Point(8, 296)
    Me.lbAn_vett2.Name = "lbAn_vett2"
    Me.lbAn_vett2.NTSDbField = ""
    Me.lbAn_vett2.Size = New System.Drawing.Size(52, 13)
    Me.lbAn_vett2.TabIndex = 655
    Me.lbAn_vett2.Text = "Vettore 2"
    Me.lbAn_vett2.Tooltip = ""
    Me.lbAn_vett2.UseMnemonic = False
    '
    'lbAn_listino
    '
    Me.lbAn_listino.AutoSize = True
    Me.lbAn_listino.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_listino.Location = New System.Drawing.Point(8, 186)
    Me.lbAn_listino.Name = "lbAn_listino"
    Me.lbAn_listino.NTSDbField = ""
    Me.lbAn_listino.Size = New System.Drawing.Size(37, 13)
    Me.lbAn_listino.TabIndex = 614
    Me.lbAn_listino.Text = "Listino"
    Me.lbAn_listino.Tooltip = ""
    Me.lbAn_listino.UseMnemonic = False
    '
    'lbXx_vett
    '
    Me.lbXx_vett.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vett.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vett.Location = New System.Drawing.Point(146, 272)
    Me.lbXx_vett.Name = "lbXx_vett"
    Me.lbXx_vett.NTSDbField = ""
    Me.lbXx_vett.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_vett.TabIndex = 653
    Me.lbXx_vett.Text = "lbXx_vett"
    Me.lbXx_vett.Tooltip = ""
    Me.lbXx_vett.UseMnemonic = False
    '
    'edAn_vett
    '
    Me.edAn_vett.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_vett.EditValue = "0"
    Me.edAn_vett.Location = New System.Drawing.Point(89, 271)
    Me.edAn_vett.Name = "edAn_vett"
    Me.edAn_vett.NTSDbField = ""
    Me.edAn_vett.NTSFormat = "0"
    Me.edAn_vett.NTSForzaVisZoom = False
    Me.edAn_vett.NTSOldValue = ""
    Me.edAn_vett.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_vett.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_vett.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_vett.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_vett.Properties.AutoHeight = False
    Me.edAn_vett.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_vett.Properties.MaxLength = 65536
    Me.edAn_vett.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_vett.Size = New System.Drawing.Size(52, 20)
    Me.edAn_vett.TabIndex = 654
    '
    'edAn_listino
    '
    Me.edAn_listino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_listino.EditValue = "0"
    Me.edAn_listino.Location = New System.Drawing.Point(89, 183)
    Me.edAn_listino.Name = "edAn_listino"
    Me.edAn_listino.NTSDbField = ""
    Me.edAn_listino.NTSFormat = "0"
    Me.edAn_listino.NTSForzaVisZoom = False
    Me.edAn_listino.NTSOldValue = ""
    Me.edAn_listino.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_listino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_listino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_listino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_listino.Properties.AutoHeight = False
    Me.edAn_listino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_listino.Properties.MaxLength = 65536
    Me.edAn_listino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_listino.Size = New System.Drawing.Size(52, 20)
    Me.edAn_listino.TabIndex = 616
    '
    'lbAn_vett
    '
    Me.lbAn_vett.AutoSize = True
    Me.lbAn_vett.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_vett.Location = New System.Drawing.Point(8, 274)
    Me.lbAn_vett.Name = "lbAn_vett"
    Me.lbAn_vett.NTSDbField = ""
    Me.lbAn_vett.Size = New System.Drawing.Size(52, 13)
    Me.lbAn_vett.TabIndex = 652
    Me.lbAn_vett.Text = "Vettore 1"
    Me.lbAn_vett.Tooltip = ""
    Me.lbAn_vett.UseMnemonic = False
    '
    'lbXx_listino
    '
    Me.lbXx_listino.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_listino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_listino.Location = New System.Drawing.Point(146, 183)
    Me.lbXx_listino.Name = "lbXx_listino"
    Me.lbXx_listino.NTSDbField = ""
    Me.lbXx_listino.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_listino.TabIndex = 615
    Me.lbXx_listino.Text = "lbXx_listino"
    Me.lbXx_listino.Tooltip = ""
    Me.lbXx_listino.UseMnemonic = False
    '
    'edAn_fido
    '
    Me.edAn_fido.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_fido.EditValue = "0"
    Me.edAn_fido.Location = New System.Drawing.Point(591, 215)
    Me.edAn_fido.Name = "edAn_fido"
    Me.edAn_fido.NTSDbField = ""
    Me.edAn_fido.NTSFormat = "0"
    Me.edAn_fido.NTSForzaVisZoom = False
    Me.edAn_fido.NTSOldValue = ""
    Me.edAn_fido.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_fido.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_fido.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_fido.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_fido.Properties.AutoHeight = False
    Me.edAn_fido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_fido.Properties.MaxLength = 65536
    Me.edAn_fido.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_fido.Size = New System.Drawing.Size(174, 20)
    Me.edAn_fido.TabIndex = 650
    '
    'lbAn_fido
    '
    Me.lbAn_fido.AutoSize = True
    Me.lbAn_fido.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_fido.Location = New System.Drawing.Point(424, 218)
    Me.lbAn_fido.Name = "lbAn_fido"
    Me.lbAn_fido.NTSDbField = ""
    Me.lbAn_fido.Size = New System.Drawing.Size(27, 13)
    Me.lbAn_fido.TabIndex = 651
    Me.lbAn_fido.Text = "Fido"
    Me.lbAn_fido.Tooltip = ""
    Me.lbAn_fido.UseMnemonic = False
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnExport)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage6.Text = "&6 - Export"
    '
    'pnExport
    '
    Me.pnExport.AllowDrop = True
    Me.pnExport.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnExport.Appearance.Options.UseBackColor = True
    Me.pnExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnExport.Controls.Add(Me.pnExportSx)
    Me.pnExport.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnExport.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnExport.Location = New System.Drawing.Point(0, 0)
    Me.pnExport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnExport.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnExport.Name = "pnExport"
    Me.pnExport.NTSActiveTrasparency = True
    Me.pnExport.Size = New System.Drawing.Size(771, 317)
    Me.pnExport.TabIndex = 1
    Me.pnExport.Text = "NtsPanel2"
    '
    'pnExportSx
    '
    Me.pnExportSx.AllowDrop = True
    Me.pnExportSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnExportSx.Appearance.Options.UseBackColor = True
    Me.pnExportSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnExportSx.Controls.Add(Me.edAn_paepag)
    Me.pnExportSx.Controls.Add(Me.lbAn_paepag)
    Me.pnExportSx.Controls.Add(Me.lbAn_codese)
    Me.pnExportSx.Controls.Add(Me.fmDichIntento)
    Me.pnExportSx.Controls.Add(Me.lbXx_codese)
    Me.pnExportSx.Controls.Add(Me.lbAn_codntra)
    Me.pnExportSx.Controls.Add(Me.edAn_codese)
    Me.pnExportSx.Controls.Add(Me.edAn_codntra)
    Me.pnExportSx.Controls.Add(Me.lbXx_codntra)
    Me.pnExportSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnExportSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnExportSx.Location = New System.Drawing.Point(0, 0)
    Me.pnExportSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnExportSx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnExportSx.Name = "pnExportSx"
    Me.pnExportSx.NTSActiveTrasparency = True
    Me.pnExportSx.Size = New System.Drawing.Size(368, 317)
    Me.pnExportSx.TabIndex = 629
    Me.pnExportSx.Text = "NtsPanel1"
    '
    'edAn_paepag
    '
    Me.edAn_paepag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_paepag.EditValue = "  "
    Me.edAn_paepag.Location = New System.Drawing.Point(119, 61)
    Me.edAn_paepag.Name = "edAn_paepag"
    Me.edAn_paepag.NTSDbField = ""
    Me.edAn_paepag.NTSForzaVisZoom = False
    Me.edAn_paepag.NTSOldValue = "  "
    Me.edAn_paepag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_paepag.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_paepag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_paepag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_paepag.Properties.AutoHeight = False
    Me.edAn_paepag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_paepag.Properties.MaxLength = 65536
    Me.edAn_paepag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_paepag.Size = New System.Drawing.Size(53, 20)
    Me.edAn_paepag.TabIndex = 630
    '
    'lbAn_paepag
    '
    Me.lbAn_paepag.AutoSize = True
    Me.lbAn_paepag.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_paepag.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAn_paepag.Location = New System.Drawing.Point(9, 64)
    Me.lbAn_paepag.Name = "lbAn_paepag"
    Me.lbAn_paepag.NTSDbField = ""
    Me.lbAn_paepag.Size = New System.Drawing.Size(93, 13)
    Me.lbAn_paepag.TabIndex = 629
    Me.lbAn_paepag.Text = "Paese pagamento"
    Me.lbAn_paepag.Tooltip = ""
    Me.lbAn_paepag.UseMnemonic = False
    '
    'lbAn_codese
    '
    Me.lbAn_codese.AutoSize = True
    Me.lbAn_codese.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codese.Location = New System.Drawing.Point(9, 12)
    Me.lbAn_codese.Name = "lbAn_codese"
    Me.lbAn_codese.NTSDbField = ""
    Me.lbAn_codese.Size = New System.Drawing.Size(110, 13)
    Me.lbAn_codese.TabIndex = 626
    Me.lbAn_codese.Text = "Codice esenzione IVA"
    Me.lbAn_codese.Tooltip = ""
    Me.lbAn_codese.UseMnemonic = False
    '
    'fmDichIntento
    '
    Me.fmDichIntento.AllowDrop = True
    Me.fmDichIntento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDichIntento.Appearance.Options.UseBackColor = True
    Me.fmDichIntento.Controls.Add(Me.edAn_numdicp)
    Me.fmDichIntento.Controls.Add(Me.edAn_numdic)
    Me.fmDichIntento.Controls.Add(Me.lbAn_numdicp)
    Me.fmDichIntento.Controls.Add(Me.lbAn_datdicp)
    Me.fmDichIntento.Controls.Add(Me.edAn_datdicp)
    Me.fmDichIntento.Controls.Add(Me.edAn_maxdic)
    Me.fmDichIntento.Controls.Add(Me.lbAn_maxdic)
    Me.fmDichIntento.Controls.Add(Me.lbAn_numdic)
    Me.fmDichIntento.Controls.Add(Me.lbAn_scaddic)
    Me.fmDichIntento.Controls.Add(Me.edAn_scaddic)
    Me.fmDichIntento.Controls.Add(Me.lbAn_datdic)
    Me.fmDichIntento.Controls.Add(Me.edAn_datdic)
    Me.fmDichIntento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDichIntento.Location = New System.Drawing.Point(3, 111)
    Me.fmDichIntento.Name = "fmDichIntento"
    Me.fmDichIntento.Size = New System.Drawing.Size(358, 178)
    Me.fmDichIntento.TabIndex = 0
    Me.fmDichIntento.Text = "Dichiarazione d'intento"
    '
    'edAn_numdicp
    '
    Me.edAn_numdicp.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_numdicp.EditValue = ""
    Me.edAn_numdicp.Location = New System.Drawing.Point(175, 147)
    Me.edAn_numdicp.Name = "edAn_numdicp"
    Me.edAn_numdicp.NTSDbField = ""
    Me.edAn_numdicp.NTSForzaVisZoom = False
    Me.edAn_numdicp.NTSOldValue = ""
    Me.edAn_numdicp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_numdicp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_numdicp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_numdicp.Properties.AutoHeight = False
    Me.edAn_numdicp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_numdicp.Properties.MaxLength = 65536
    Me.edAn_numdicp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_numdicp.Size = New System.Drawing.Size(100, 20)
    Me.edAn_numdicp.TabIndex = 641
    '
    'edAn_numdic
    '
    Me.edAn_numdic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_numdic.EditValue = ""
    Me.edAn_numdic.Location = New System.Drawing.Point(175, 23)
    Me.edAn_numdic.Name = "edAn_numdic"
    Me.edAn_numdic.NTSDbField = ""
    Me.edAn_numdic.NTSForzaVisZoom = False
    Me.edAn_numdic.NTSOldValue = ""
    Me.edAn_numdic.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_numdic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_numdic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_numdic.Properties.AutoHeight = False
    Me.edAn_numdic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_numdic.Properties.MaxLength = 65536
    Me.edAn_numdic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_numdic.Size = New System.Drawing.Size(100, 20)
    Me.edAn_numdic.TabIndex = 640
    '
    'lbAn_numdicp
    '
    Me.lbAn_numdicp.AutoSize = True
    Me.lbAn_numdicp.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_numdicp.Location = New System.Drawing.Point(6, 150)
    Me.lbAn_numdicp.Name = "lbAn_numdicp"
    Me.lbAn_numdicp.NTSDbField = ""
    Me.lbAn_numdicp.Size = New System.Drawing.Size(94, 13)
    Me.lbAn_numdicp.TabIndex = 638
    Me.lbAn_numdicp.Text = "Numero protocollo"
    Me.lbAn_numdicp.Tooltip = ""
    Me.lbAn_numdicp.UseMnemonic = False
    '
    'lbAn_datdicp
    '
    Me.lbAn_datdicp.AutoSize = True
    Me.lbAn_datdicp.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_datdicp.Location = New System.Drawing.Point(6, 127)
    Me.lbAn_datdicp.Name = "lbAn_datdicp"
    Me.lbAn_datdicp.NTSDbField = ""
    Me.lbAn_datdicp.Size = New System.Drawing.Size(80, 13)
    Me.lbAn_datdicp.TabIndex = 636
    Me.lbAn_datdicp.Text = "Data protocollo"
    Me.lbAn_datdicp.Tooltip = ""
    Me.lbAn_datdicp.UseMnemonic = False
    '
    'edAn_datdicp
    '
    Me.edAn_datdicp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_datdicp.EditValue = "01/01/2000"
    Me.edAn_datdicp.Location = New System.Drawing.Point(175, 124)
    Me.edAn_datdicp.Name = "edAn_datdicp"
    Me.edAn_datdicp.NTSDbField = ""
    Me.edAn_datdicp.NTSForzaVisZoom = False
    Me.edAn_datdicp.NTSOldValue = ""
    Me.edAn_datdicp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_datdicp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_datdicp.Properties.AutoHeight = False
    Me.edAn_datdicp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_datdicp.Properties.MaxLength = 65536
    Me.edAn_datdicp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_datdicp.Size = New System.Drawing.Size(100, 20)
    Me.edAn_datdicp.TabIndex = 637
    '
    'edAn_maxdic
    '
    Me.edAn_maxdic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_maxdic.EditValue = "0"
    Me.edAn_maxdic.Location = New System.Drawing.Point(175, 91)
    Me.edAn_maxdic.Name = "edAn_maxdic"
    Me.edAn_maxdic.NTSDbField = ""
    Me.edAn_maxdic.NTSFormat = "0"
    Me.edAn_maxdic.NTSForzaVisZoom = False
    Me.edAn_maxdic.NTSOldValue = "0"
    Me.edAn_maxdic.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_maxdic.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_maxdic.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_maxdic.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_maxdic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_maxdic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_maxdic.Properties.AutoHeight = False
    Me.edAn_maxdic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_maxdic.Properties.MaxLength = 65536
    Me.edAn_maxdic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_maxdic.Size = New System.Drawing.Size(100, 20)
    Me.edAn_maxdic.TabIndex = 635
    '
    'lbAn_maxdic
    '
    Me.lbAn_maxdic.AutoSize = True
    Me.lbAn_maxdic.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_maxdic.Location = New System.Drawing.Point(5, 94)
    Me.lbAn_maxdic.Name = "lbAn_maxdic"
    Me.lbAn_maxdic.NTSDbField = ""
    Me.lbAn_maxdic.Size = New System.Drawing.Size(88, 13)
    Me.lbAn_maxdic.TabIndex = 634
    Me.lbAn_maxdic.Text = "Importo massimo"
    Me.lbAn_maxdic.Tooltip = ""
    Me.lbAn_maxdic.UseMnemonic = False
    '
    'lbAn_numdic
    '
    Me.lbAn_numdic.AutoSize = True
    Me.lbAn_numdic.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_numdic.Location = New System.Drawing.Point(6, 26)
    Me.lbAn_numdic.Name = "lbAn_numdic"
    Me.lbAn_numdic.NTSDbField = ""
    Me.lbAn_numdic.Size = New System.Drawing.Size(44, 13)
    Me.lbAn_numdic.TabIndex = 599
    Me.lbAn_numdic.Text = "Numero"
    Me.lbAn_numdic.Tooltip = ""
    Me.lbAn_numdic.UseMnemonic = False
    '
    'lbAn_scaddic
    '
    Me.lbAn_scaddic.AutoSize = True
    Me.lbAn_scaddic.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_scaddic.Location = New System.Drawing.Point(6, 71)
    Me.lbAn_scaddic.Name = "lbAn_scaddic"
    Me.lbAn_scaddic.NTSDbField = ""
    Me.lbAn_scaddic.Size = New System.Drawing.Size(78, 13)
    Me.lbAn_scaddic.TabIndex = 597
    Me.lbAn_scaddic.Text = "Data scadenza"
    Me.lbAn_scaddic.Tooltip = ""
    Me.lbAn_scaddic.UseMnemonic = False
    '
    'edAn_scaddic
    '
    Me.edAn_scaddic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_scaddic.EditValue = "01/01/2000"
    Me.edAn_scaddic.Location = New System.Drawing.Point(175, 68)
    Me.edAn_scaddic.Name = "edAn_scaddic"
    Me.edAn_scaddic.NTSDbField = ""
    Me.edAn_scaddic.NTSForzaVisZoom = False
    Me.edAn_scaddic.NTSOldValue = ""
    Me.edAn_scaddic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_scaddic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_scaddic.Properties.AutoHeight = False
    Me.edAn_scaddic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_scaddic.Properties.MaxLength = 65536
    Me.edAn_scaddic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_scaddic.Size = New System.Drawing.Size(100, 20)
    Me.edAn_scaddic.TabIndex = 598
    '
    'lbAn_datdic
    '
    Me.lbAn_datdic.AutoSize = True
    Me.lbAn_datdic.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_datdic.Location = New System.Drawing.Point(6, 48)
    Me.lbAn_datdic.Name = "lbAn_datdic"
    Me.lbAn_datdic.NTSDbField = ""
    Me.lbAn_datdic.Size = New System.Drawing.Size(30, 13)
    Me.lbAn_datdic.TabIndex = 595
    Me.lbAn_datdic.Text = "Data"
    Me.lbAn_datdic.Tooltip = ""
    Me.lbAn_datdic.UseMnemonic = False
    '
    'edAn_datdic
    '
    Me.edAn_datdic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_datdic.EditValue = "01/01/2000"
    Me.edAn_datdic.Location = New System.Drawing.Point(175, 45)
    Me.edAn_datdic.Name = "edAn_datdic"
    Me.edAn_datdic.NTSDbField = ""
    Me.edAn_datdic.NTSForzaVisZoom = False
    Me.edAn_datdic.NTSOldValue = ""
    Me.edAn_datdic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_datdic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_datdic.Properties.AutoHeight = False
    Me.edAn_datdic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_datdic.Properties.MaxLength = 65536
    Me.edAn_datdic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_datdic.Size = New System.Drawing.Size(100, 20)
    Me.edAn_datdic.TabIndex = 596
    '
    'lbXx_codese
    '
    Me.lbXx_codese.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codese.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codese.Location = New System.Drawing.Point(178, 9)
    Me.lbXx_codese.Name = "lbXx_codese"
    Me.lbXx_codese.NTSDbField = ""
    Me.lbXx_codese.Size = New System.Drawing.Size(183, 20)
    Me.lbXx_codese.TabIndex = 627
    Me.lbXx_codese.Text = "lbXx_codese"
    Me.lbXx_codese.Tooltip = ""
    Me.lbXx_codese.UseMnemonic = False
    '
    'lbAn_codntra
    '
    Me.lbAn_codntra.AutoSize = True
    Me.lbAn_codntra.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codntra.Location = New System.Drawing.Point(9, 38)
    Me.lbAn_codntra.Name = "lbAn_codntra"
    Me.lbAn_codntra.NTSDbField = ""
    Me.lbAn_codntra.Size = New System.Drawing.Size(99, 13)
    Me.lbAn_codntra.TabIndex = 623
    Me.lbAn_codntra.Text = "Natura transazione"
    Me.lbAn_codntra.Tooltip = ""
    Me.lbAn_codntra.UseMnemonic = False
    '
    'edAn_codese
    '
    Me.edAn_codese.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codese.EditValue = "0"
    Me.edAn_codese.Location = New System.Drawing.Point(119, 9)
    Me.edAn_codese.Name = "edAn_codese"
    Me.edAn_codese.NTSDbField = ""
    Me.edAn_codese.NTSFormat = "0"
    Me.edAn_codese.NTSForzaVisZoom = False
    Me.edAn_codese.NTSOldValue = ""
    Me.edAn_codese.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codese.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codese.Properties.AutoHeight = False
    Me.edAn_codese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codese.Properties.MaxLength = 65536
    Me.edAn_codese.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codese.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codese.TabIndex = 628
    '
    'edAn_codntra
    '
    Me.edAn_codntra.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_codntra.EditValue = "0"
    Me.edAn_codntra.Location = New System.Drawing.Point(119, 35)
    Me.edAn_codntra.Name = "edAn_codntra"
    Me.edAn_codntra.NTSDbField = ""
    Me.edAn_codntra.NTSFormat = "0"
    Me.edAn_codntra.NTSForzaVisZoom = False
    Me.edAn_codntra.NTSOldValue = ""
    Me.edAn_codntra.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codntra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codntra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codntra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codntra.Properties.AutoHeight = False
    Me.edAn_codntra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codntra.Properties.MaxLength = 65536
    Me.edAn_codntra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codntra.Size = New System.Drawing.Size(53, 20)
    Me.edAn_codntra.TabIndex = 625
    '
    'lbXx_codntra
    '
    Me.lbXx_codntra.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codntra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codntra.Location = New System.Drawing.Point(178, 35)
    Me.lbXx_codntra.Name = "lbXx_codntra"
    Me.lbXx_codntra.NTSDbField = ""
    Me.lbXx_codntra.Size = New System.Drawing.Size(183, 20)
    Me.lbXx_codntra.TabIndex = 624
    Me.lbXx_codntra.Text = "lbXx_codntra"
    Me.lbXx_codntra.Tooltip = ""
    Me.lbXx_codntra.UseMnemonic = False
    '
    'NtsTabPage7
    '
    Me.NtsTabPage7.AllowDrop = True
    Me.NtsTabPage7.Controls.Add(Me.pnNote)
    Me.NtsTabPage7.Enable = True
    Me.NtsTabPage7.Name = "NtsTabPage7"
    Me.NtsTabPage7.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage7.Text = "&7 - Note"
    '
    'pnNote
    '
    Me.pnNote.AllowDrop = True
    Me.pnNote.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNote.Appearance.Options.UseBackColor = True
    Me.pnNote.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNote.Controls.Add(Me.lbAn_note)
    Me.pnNote.Controls.Add(Me.edAn_note)
    Me.pnNote.Controls.Add(Me.edAn_note2)
    Me.pnNote.Controls.Add(Me.lbAn_note2)
    Me.pnNote.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNote.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnNote.Location = New System.Drawing.Point(0, 0)
    Me.pnNote.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnNote.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnNote.Name = "pnNote"
    Me.pnNote.NTSActiveTrasparency = True
    Me.pnNote.Size = New System.Drawing.Size(771, 317)
    Me.pnNote.TabIndex = 0
    Me.pnNote.Text = "NtsPanel1"
    '
    'lbAn_note
    '
    Me.lbAn_note.AutoSize = True
    Me.lbAn_note.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_note.Location = New System.Drawing.Point(3, 6)
    Me.lbAn_note.Name = "lbAn_note"
    Me.lbAn_note.NTSDbField = ""
    Me.lbAn_note.Size = New System.Drawing.Size(57, 13)
    Me.lbAn_note.TabIndex = 604
    Me.lbAn_note.Text = "Note brevi"
    Me.lbAn_note.Tooltip = ""
    Me.lbAn_note.UseMnemonic = False
    '
    'edAn_note
    '
    Me.edAn_note.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_note.EditValue = ""
    Me.edAn_note.Location = New System.Drawing.Point(73, 3)
    Me.edAn_note.Name = "edAn_note"
    Me.edAn_note.NTSDbField = ""
    Me.edAn_note.NTSForzaVisZoom = False
    Me.edAn_note.NTSOldValue = ""
    Me.edAn_note.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_note.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_note.Properties.AutoHeight = False
    Me.edAn_note.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_note.Properties.MaxLength = 65536
    Me.edAn_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_note.Size = New System.Drawing.Size(696, 20)
    Me.edAn_note.TabIndex = 605
    '
    'edAn_note2
    '
    Me.edAn_note2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAn_note2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_note2.Location = New System.Drawing.Point(73, 29)
    Me.edAn_note2.Name = "edAn_note2"
    Me.edAn_note2.NTSDbField = ""
    Me.edAn_note2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_note2.Size = New System.Drawing.Size(695, 285)
    Me.edAn_note2.TabIndex = 603
    '
    'lbAn_note2
    '
    Me.lbAn_note2.AutoSize = True
    Me.lbAn_note2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_note2.Location = New System.Drawing.Point(3, 31)
    Me.lbAn_note2.Name = "lbAn_note2"
    Me.lbAn_note2.NTSDbField = ""
    Me.lbAn_note2.Size = New System.Drawing.Size(30, 13)
    Me.lbAn_note2.TabIndex = 602
    Me.lbAn_note2.Text = "Note"
    Me.lbAn_note2.Tooltip = ""
    Me.lbAn_note2.UseMnemonic = False
    '
    'NtsTabPage8
    '
    Me.NtsTabPage8.AllowDrop = True
    Me.NtsTabPage8.Controls.Add(Me.pnListini)
    Me.NtsTabPage8.Enable = True
    Me.NtsTabPage8.Name = "NtsTabPage8"
    Me.NtsTabPage8.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage8.Text = "&8 - Listini"
    '
    'pnListini
    '
    Me.pnListini.AllowDrop = True
    Me.pnListini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnListini.Appearance.Options.UseBackColor = True
    Me.pnListini.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnListini.Controls.Add(Me.ceListini)
    Me.pnListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnListini.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnListini.Location = New System.Drawing.Point(0, 0)
    Me.pnListini.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnListini.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnListini.Name = "pnListini"
    Me.pnListini.NTSActiveTrasparency = True
    Me.pnListini.Size = New System.Drawing.Size(771, 317)
    Me.pnListini.TabIndex = 0
    Me.pnListini.Text = "NtsPanel1"
    '
    'ceListini
    '
    Me.ceListini.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceListini.LcCodart = ""
    Me.ceListini.LcCodartRoot = ""
    Me.ceListini.LcConto = 0
    Me.ceListini.LcFaseArticolo = 0
    Me.ceListini.LcTipo = ""
    Me.ceListini.Location = New System.Drawing.Point(0, 0)
    Me.ceListini.MinimumSize = New System.Drawing.Size(478, 260)
    Me.ceListini.Name = "ceListini"
    Me.ceListini.Size = New System.Drawing.Size(771, 317)
    Me.ceListini.strNomeCampo = ""
    Me.ceListini.TabIndex = 0
    '
    'NtsTabPage9
    '
    Me.NtsTabPage9.AllowDrop = True
    Me.NtsTabPage9.Controls.Add(Me.pnSconti)
    Me.NtsTabPage9.Enable = True
    Me.NtsTabPage9.Name = "NtsTabPage9"
    Me.NtsTabPage9.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage9.Text = "&9 - Sconti"
    '
    'pnSconti
    '
    Me.pnSconti.AllowDrop = True
    Me.pnSconti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSconti.Appearance.Options.UseBackColor = True
    Me.pnSconti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSconti.Controls.Add(Me.ceSconti)
    Me.pnSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSconti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSconti.Location = New System.Drawing.Point(0, 0)
    Me.pnSconti.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSconti.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSconti.Name = "pnSconti"
    Me.pnSconti.NTSActiveTrasparency = True
    Me.pnSconti.Size = New System.Drawing.Size(771, 317)
    Me.pnSconti.TabIndex = 0
    Me.pnSconti.Text = "NtsPanel1"
    '
    'ceSconti
    '
    Me.ceSconti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceSconti.GridColumn1_954_20 = Nothing
    Me.ceSconti.Location = New System.Drawing.Point(0, 0)
    Me.ceSconti.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceSconti.Name = "ceSconti"
    Me.ceSconti.Size = New System.Drawing.Size(771, 317)
    Me.ceSconti.SoClasseArt = 0
    Me.ceSconti.SoClasseCli = 0
    Me.ceSconti.SoCodart = ""
    Me.ceSconti.SoCodartRoot = ""
    Me.ceSconti.SoConto = 0
    Me.ceSconti.strNomeCampo = ""
    Me.ceSconti.TabIndex = 0
    Me.ceSconti.TipoSconto = 0
    '
    'NtsTabPage10
    '
    Me.NtsTabPage10.AllowDrop = True
    Me.NtsTabPage10.Controls.Add(Me.pnProvvigioni)
    Me.NtsTabPage10.Enable = True
    Me.NtsTabPage10.Name = "NtsTabPage10"
    Me.NtsTabPage10.Size = New System.Drawing.Size(771, 317)
    Me.NtsTabPage10.Text = "&10 - Provvig."
    '
    'pnProvvigioni
    '
    Me.pnProvvigioni.AllowDrop = True
    Me.pnProvvigioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProvvigioni.Appearance.Options.UseBackColor = True
    Me.pnProvvigioni.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProvvigioni.Controls.Add(Me.ceProvvig)
    Me.pnProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProvvigioni.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnProvvigioni.Location = New System.Drawing.Point(0, 0)
    Me.pnProvvigioni.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnProvvigioni.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnProvvigioni.Name = "pnProvvigioni"
    Me.pnProvvigioni.NTSActiveTrasparency = True
    Me.pnProvvigioni.Size = New System.Drawing.Size(771, 317)
    Me.pnProvvigioni.TabIndex = 0
    Me.pnProvvigioni.Text = "NtsPanel1"
    '
    'ceProvvig
    '
    Me.ceProvvig.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceProvvig.GridColumn1_465_17 = Nothing
    Me.ceProvvig.GridColumn1_715_16 = Nothing
    Me.ceProvvig.Location = New System.Drawing.Point(0, 0)
    Me.ceProvvig.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceProvvig.Name = "ceProvvig"
    Me.ceProvvig.PerClasseArt = 0
    Me.ceProvvig.PerClasseCli = 0
    Me.ceProvvig.PerCodart = ""
    Me.ceProvvig.PerCodartRoot = ""
    Me.ceProvvig.PerCodcage = 0
    Me.ceProvvig.PerConto = 0
    Me.ceProvvig.Size = New System.Drawing.Size(771, 317)
    Me.ceProvvig.strNomeCampo = ""
    Me.ceProvvig.TabIndex = 0
    Me.ceProvvig.TipoProvv = 0
    '
    'lbAn_conto
    '
    Me.lbAn_conto.AutoSize = True
    Me.lbAn_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_conto.Location = New System.Drawing.Point(9, 11)
    Me.lbAn_conto.Name = "lbAn_conto"
    Me.lbAn_conto.NTSDbField = ""
    Me.lbAn_conto.Size = New System.Drawing.Size(39, 13)
    Me.lbAn_conto.TabIndex = 10
    Me.lbAn_conto.Text = "Codice"
    Me.lbAn_conto.Tooltip = ""
    Me.lbAn_conto.UseMnemonic = False
    '
    'edAn_conto
    '
    Me.edAn_conto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_conto.EditValue = "0"
    Me.edAn_conto.Enabled = False
    Me.edAn_conto.Location = New System.Drawing.Point(75, 8)
    Me.edAn_conto.Name = "edAn_conto"
    Me.edAn_conto.NTSDbField = ""
    Me.edAn_conto.NTSFormat = "0"
    Me.edAn_conto.NTSForzaVisZoom = False
    Me.edAn_conto.NTSOldValue = ""
    Me.edAn_conto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_conto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_conto.Properties.AutoHeight = False
    Me.edAn_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_conto.Properties.MaxLength = 65536
    Me.edAn_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_conto.Size = New System.Drawing.Size(100, 20)
    Me.edAn_conto.TabIndex = 500
    '
    'lbAn_descr1
    '
    Me.lbAn_descr1.AutoSize = True
    Me.lbAn_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr1.Location = New System.Drawing.Point(9, 37)
    Me.lbAn_descr1.Name = "lbAn_descr1"
    Me.lbAn_descr1.NTSDbField = ""
    Me.lbAn_descr1.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_descr1.TabIndex = 11
    Me.lbAn_descr1.Text = "Rag. sociale"
    Me.lbAn_descr1.Tooltip = ""
    Me.lbAn_descr1.UseMnemonic = False
    '
    'edAn_descr1
    '
    Me.edAn_descr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_descr1.EditValue = ""
    Me.edAn_descr1.Location = New System.Drawing.Point(75, 34)
    Me.edAn_descr1.Name = "edAn_descr1"
    Me.edAn_descr1.NTSDbField = ""
    Me.edAn_descr1.NTSForzaVisZoom = False
    Me.edAn_descr1.NTSOldValue = ""
    Me.edAn_descr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_descr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_descr1.Properties.AutoHeight = False
    Me.edAn_descr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_descr1.Properties.MaxLength = 65536
    Me.edAn_descr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_descr1.Size = New System.Drawing.Size(226, 20)
    Me.edAn_descr1.TabIndex = 501
    '
    'edAn_descr2
    '
    Me.edAn_descr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_descr2.EditValue = ""
    Me.edAn_descr2.Location = New System.Drawing.Point(75, 60)
    Me.edAn_descr2.Name = "edAn_descr2"
    Me.edAn_descr2.NTSDbField = ""
    Me.edAn_descr2.NTSForzaVisZoom = False
    Me.edAn_descr2.NTSOldValue = ""
    Me.edAn_descr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_descr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_descr2.Properties.AutoHeight = False
    Me.edAn_descr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_descr2.Properties.MaxLength = 65536
    Me.edAn_descr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_descr2.Size = New System.Drawing.Size(226, 20)
    Me.edAn_descr2.TabIndex = 502
    '
    'lbAn_persfg
    '
    Me.lbAn_persfg.AutoSize = True
    Me.lbAn_persfg.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_persfg.Location = New System.Drawing.Point(307, 63)
    Me.lbAn_persfg.Name = "lbAn_persfg"
    Me.lbAn_persfg.NTSDbField = ""
    Me.lbAn_persfg.Size = New System.Drawing.Size(57, 13)
    Me.lbAn_persfg.TabIndex = 41
    Me.lbAn_persfg.Text = "Tipo sogg."
    Me.lbAn_persfg.Tooltip = ""
    Me.lbAn_persfg.UseMnemonic = False
    '
    'cbAn_persfg
    '
    Me.cbAn_persfg.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_persfg.DataSource = Nothing
    Me.cbAn_persfg.DisplayMember = ""
    Me.cbAn_persfg.Location = New System.Drawing.Point(377, 60)
    Me.cbAn_persfg.Name = "cbAn_persfg"
    Me.cbAn_persfg.NTSDbField = ""
    Me.cbAn_persfg.Properties.AutoHeight = False
    Me.cbAn_persfg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_persfg.Properties.DropDownRows = 30
    Me.cbAn_persfg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_persfg.SelectedValue = ""
    Me.cbAn_persfg.Size = New System.Drawing.Size(112, 20)
    Me.cbAn_persfg.TabIndex = 531
    Me.cbAn_persfg.ValueMember = ""
    '
    'lbAn_siglaric
    '
    Me.lbAn_siglaric.AutoSize = True
    Me.lbAn_siglaric.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_siglaric.Location = New System.Drawing.Point(307, 37)
    Me.lbAn_siglaric.Name = "lbAn_siglaric"
    Me.lbAn_siglaric.NTSDbField = ""
    Me.lbAn_siglaric.Size = New System.Drawing.Size(64, 13)
    Me.lbAn_siglaric.TabIndex = 50
    Me.lbAn_siglaric.Text = "Sigla ricerca"
    Me.lbAn_siglaric.Tooltip = ""
    Me.lbAn_siglaric.UseMnemonic = False
    '
    'edAn_siglaric
    '
    Me.edAn_siglaric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_siglaric.EditValue = ""
    Me.edAn_siglaric.Location = New System.Drawing.Point(377, 34)
    Me.edAn_siglaric.Name = "edAn_siglaric"
    Me.edAn_siglaric.NTSDbField = ""
    Me.edAn_siglaric.NTSForzaVisZoom = False
    Me.edAn_siglaric.NTSOldValue = ""
    Me.edAn_siglaric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_siglaric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_siglaric.Properties.AutoHeight = False
    Me.edAn_siglaric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_siglaric.Properties.MaxLength = 65536
    Me.edAn_siglaric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_siglaric.Size = New System.Drawing.Size(214, 20)
    Me.edAn_siglaric.TabIndex = 540
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.ceColl)
    Me.pnTop.Controls.Add(Me.lbBarra)
    Me.pnTop.Controls.Add(Me.cbAn_privato)
    Me.pnTop.Controls.Add(Me.lbLead)
    Me.pnTop.Controls.Add(Me.lbAnagen)
    Me.pnTop.Controls.Add(Me.cmdPartitario)
    Me.pnTop.Controls.Add(Me.lbXx_codmast)
    Me.pnTop.Controls.Add(Me.lbMastroLabel)
    Me.pnTop.Controls.Add(Me.edAn_descr2)
    Me.pnTop.Controls.Add(Me.lbAn_descr1)
    Me.pnTop.Controls.Add(Me.edAn_siglaric)
    Me.pnTop.Controls.Add(Me.lbAn_conto)
    Me.pnTop.Controls.Add(Me.lbAn_siglaric)
    Me.pnTop.Controls.Add(Me.edAn_descr1)
    Me.pnTop.Controls.Add(Me.edAn_conto)
    Me.pnTop.Controls.Add(Me.cbAn_persfg)
    Me.pnTop.Controls.Add(Me.lbAn_persfg)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(780, 88)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "NtsPanel1"
    '
    'ceColl
    '
    Me.ceColl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ceColl.Location = New System.Drawing.Point(678, 7)
    Me.ceColl.Name = "ceColl"
    Me.ceColl.Size = New System.Drawing.Size(97, 22)
    Me.ceColl.strNomeCampo = ""
    Me.ceColl.TabIndex = 596
    Me.ceColl.Visible = False
    '
    'lbBarra
    '
    Me.lbBarra.AutoSize = True
    Me.lbBarra.BackColor = System.Drawing.Color.Transparent
    Me.lbBarra.Location = New System.Drawing.Point(492, 63)
    Me.lbBarra.Name = "lbBarra"
    Me.lbBarra.NTSDbField = ""
    Me.lbBarra.Size = New System.Drawing.Size(11, 13)
    Me.lbBarra.TabIndex = 595
    Me.lbBarra.Text = "/"
    Me.lbBarra.Tooltip = ""
    Me.lbBarra.UseMnemonic = False
    '
    'cbAn_privato
    '
    Me.cbAn_privato.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_privato.DataSource = Nothing
    Me.cbAn_privato.DisplayMember = ""
    Me.cbAn_privato.Location = New System.Drawing.Point(505, 60)
    Me.cbAn_privato.Name = "cbAn_privato"
    Me.cbAn_privato.NTSDbField = ""
    Me.cbAn_privato.Properties.AutoHeight = False
    Me.cbAn_privato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_privato.Properties.DropDownRows = 30
    Me.cbAn_privato.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_privato.SelectedValue = ""
    Me.cbAn_privato.Size = New System.Drawing.Size(86, 20)
    Me.cbAn_privato.TabIndex = 594
    Me.cbAn_privato.ValueMember = ""
    '
    'lbLead
    '
    Me.lbLead.AutoSize = True
    Me.lbLead.BackColor = System.Drawing.Color.Transparent
    Me.lbLead.Location = New System.Drawing.Point(624, 60)
    Me.lbLead.Name = "lbLead"
    Me.lbLead.NTSDbField = ""
    Me.lbLead.Size = New System.Drawing.Size(57, 13)
    Me.lbLead.TabIndex = 593
    Me.lbLead.Text = "Cod.Lead:"
    Me.lbLead.Tooltip = ""
    Me.lbLead.UseMnemonic = False
    '
    'lbAnagen
    '
    Me.lbAnagen.AutoSize = True
    Me.lbAnagen.BackColor = System.Drawing.Color.Transparent
    Me.lbAnagen.Location = New System.Drawing.Point(624, 37)
    Me.lbAnagen.Name = "lbAnagen"
    Me.lbAnagen.NTSDbField = ""
    Me.lbAnagen.Size = New System.Drawing.Size(57, 13)
    Me.lbAnagen.TabIndex = 592
    Me.lbAnagen.Text = "An.Gen.  :"
    Me.lbAnagen.Tooltip = ""
    Me.lbAnagen.UseMnemonic = False
    '
    'cmdPartitario
    '
    Me.cmdPartitario.ImagePath = ""
    Me.cmdPartitario.ImageText = ""
    Me.cmdPartitario.Location = New System.Drawing.Point(594, 8)
    Me.cmdPartitario.Name = "cmdPartitario"
    Me.cmdPartitario.NTSContextMenu = Nothing
    Me.cmdPartitario.Size = New System.Drawing.Size(81, 20)
    Me.cmdPartitario.TabIndex = 591
    Me.cmdPartitario.Text = "&Partitario"
    '
    'lbXx_codmast
    '
    Me.lbXx_codmast.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codmast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codmast.Location = New System.Drawing.Point(377, 8)
    Me.lbXx_codmast.Name = "lbXx_codmast"
    Me.lbXx_codmast.NTSDbField = ""
    Me.lbXx_codmast.Size = New System.Drawing.Size(214, 20)
    Me.lbXx_codmast.TabIndex = 578
    Me.lbXx_codmast.Text = "lbXx_codmast"
    Me.lbXx_codmast.Tooltip = ""
    Me.lbXx_codmast.UseMnemonic = False
    '
    'lbMastroLabel
    '
    Me.lbMastroLabel.AutoSize = True
    Me.lbMastroLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbMastroLabel.Location = New System.Drawing.Point(307, 11)
    Me.lbMastroLabel.Name = "lbMastroLabel"
    Me.lbMastroLabel.NTSDbField = ""
    Me.lbMastroLabel.Size = New System.Drawing.Size(40, 13)
    Me.lbMastroLabel.TabIndex = 549
    Me.lbMastroLabel.Text = "Mastro"
    Me.lbMastroLabel.Tooltip = ""
    Me.lbMastroLabel.UseMnemonic = False
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsClie)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 118)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(780, 347)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel1"
    '
    'edFocus
    '
    Me.edFocus.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edFocus.EditValue = ""
    Me.edFocus.Location = New System.Drawing.Point(-10000, -10000)
    Me.edFocus.Name = "edFocus"
    Me.edFocus.NTSDbField = ""
    Me.edFocus.NTSForzaVisZoom = False
    Me.edFocus.NTSOldValue = ""
    Me.edFocus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFocus.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFocus.Properties.AutoHeight = False
    Me.edFocus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFocus.Properties.MaxLength = 65536
    Me.edFocus.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFocus.Size = New System.Drawing.Size(10, 20)
    Me.edFocus.TabIndex = 7
    '
    'lbDeteriorabili
    '
    Me.lbDeteriorabili.AutoSize = True
    Me.lbDeteriorabili.BackColor = System.Drawing.Color.Transparent
    Me.lbDeteriorabili.Location = New System.Drawing.Point(209, 24)
    Me.lbDeteriorabili.Name = "lbDeteriorabili"
    Me.lbDeteriorabili.NTSDbField = ""
    Me.lbDeteriorabili.Size = New System.Drawing.Size(116, 13)
    Me.lbDeteriorabili.TabIndex = 650
    Me.lbDeteriorabili.Text = "Per articoli deteriorabili"
    Me.lbDeteriorabili.Tooltip = ""
    Me.lbDeteriorabili.UseMnemonic = False
    '
    'edAn_codpagadet3
    '
    Me.edAn_codpagadet3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpagadet3.EditValue = "0"
    Me.edAn_codpagadet3.Location = New System.Drawing.Point(192, 115)
    Me.edAn_codpagadet3.Name = "edAn_codpagadet3"
    Me.edAn_codpagadet3.NTSDbField = ""
    Me.edAn_codpagadet3.NTSFormat = "0"
    Me.edAn_codpagadet3.NTSForzaVisZoom = False
    Me.edAn_codpagadet3.NTSOldValue = "0"
    Me.edAn_codpagadet3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpagadet3.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpagadet3.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpagadet3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpagadet3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpagadet3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpagadet3.Properties.AutoHeight = False
    Me.edAn_codpagadet3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpagadet3.Properties.MaxLength = 65536
    Me.edAn_codpagadet3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpagadet3.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpagadet3.TabIndex = 655
    '
    'lbXx_codpagadet3
    '
    Me.lbXx_codpagadet3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpagadet3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpagadet3.Location = New System.Drawing.Point(254, 115)
    Me.lbXx_codpagadet3.Name = "lbXx_codpagadet3"
    Me.lbXx_codpagadet3.NTSDbField = ""
    Me.lbXx_codpagadet3.Size = New System.Drawing.Size(93, 21)
    Me.lbXx_codpagadet3.TabIndex = 656
    Me.lbXx_codpagadet3.Tooltip = ""
    Me.lbXx_codpagadet3.UseMnemonic = False
    '
    'edAn_codpagadet2
    '
    Me.edAn_codpagadet2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpagadet2.EditValue = "0"
    Me.edAn_codpagadet2.Location = New System.Drawing.Point(192, 76)
    Me.edAn_codpagadet2.Name = "edAn_codpagadet2"
    Me.edAn_codpagadet2.NTSDbField = ""
    Me.edAn_codpagadet2.NTSFormat = "0"
    Me.edAn_codpagadet2.NTSForzaVisZoom = False
    Me.edAn_codpagadet2.NTSOldValue = "0"
    Me.edAn_codpagadet2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpagadet2.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpagadet2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpagadet2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpagadet2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpagadet2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpagadet2.Properties.AutoHeight = False
    Me.edAn_codpagadet2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpagadet2.Properties.MaxLength = 65536
    Me.edAn_codpagadet2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpagadet2.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpagadet2.TabIndex = 653
    '
    'lbXx_codpagadet2
    '
    Me.lbXx_codpagadet2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpagadet2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpagadet2.Location = New System.Drawing.Point(254, 76)
    Me.lbXx_codpagadet2.Name = "lbXx_codpagadet2"
    Me.lbXx_codpagadet2.NTSDbField = ""
    Me.lbXx_codpagadet2.Size = New System.Drawing.Size(93, 21)
    Me.lbXx_codpagadet2.TabIndex = 654
    Me.lbXx_codpagadet2.Tooltip = ""
    Me.lbXx_codpagadet2.UseMnemonic = False
    '
    'edAn_codpagadet
    '
    Me.edAn_codpagadet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codpagadet.EditValue = "0"
    Me.edAn_codpagadet.Location = New System.Drawing.Point(192, 38)
    Me.edAn_codpagadet.Name = "edAn_codpagadet"
    Me.edAn_codpagadet.NTSDbField = ""
    Me.edAn_codpagadet.NTSFormat = "0"
    Me.edAn_codpagadet.NTSForzaVisZoom = False
    Me.edAn_codpagadet.NTSOldValue = "0"
    Me.edAn_codpagadet.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAn_codpagadet.Properties.Appearance.Options.UseBackColor = True
    Me.edAn_codpagadet.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_codpagadet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_codpagadet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_codpagadet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_codpagadet.Properties.AutoHeight = False
    Me.edAn_codpagadet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_codpagadet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_codpagadet.Size = New System.Drawing.Size(59, 20)
    Me.edAn_codpagadet.TabIndex = 651
    '
    'lbXx_codpagadet
    '
    Me.lbXx_codpagadet.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpagadet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpagadet.Location = New System.Drawing.Point(254, 38)
    Me.lbXx_codpagadet.Name = "lbXx_codpagadet"
    Me.lbXx_codpagadet.NTSDbField = ""
    Me.lbXx_codpagadet.Size = New System.Drawing.Size(93, 21)
    Me.lbXx_codpagadet.TabIndex = 652
    Me.lbXx_codpagadet.Tooltip = ""
    Me.lbXx_codpagadet.UseMnemonic = False
    '
    'FRM__CLIE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(780, 465)
    Me.Controls.Add(Me.edFocus)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__CLIE"
    Me.Text = "ANAGRAFICA CLIENTI/FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsClie, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsClie.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    CType(Me.pnPag1Dx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Dx.ResumeLayout(False)
    Me.pnPag1Dx.PerformLayout()
    CType(Me.edAn_cell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_omocodice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_usaem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_pariva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_faxtlx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_telef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codfis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag1Sx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Sx.ResumeLayout(False)
    Me.pnPag1Sx.PerformLayout()
    CType(Me.edAn_statofed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_tpsogiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codcomu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_citta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_indir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_stato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_cap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_prov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag1Bottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Bottom.ResumeLayout(False)
    CType(Me.fmIndirizzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIndirizzi.ResumeLayout(False)
    CType(Me.pnIndirDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnIndirDx.ResumeLayout(False)
    Me.pnIndirDx.PerformLayout()
    CType(Me.edAn_destpag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_destin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnIndirSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnIndirSx.ResumeLayout(False)
    Me.pnIndirSx.PerformLayout()
    CType(Me.ckDestresan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDestcorr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDestsedel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDestdomf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2.ResumeLayout(False)
    Me.pnPag2.PerformLayout()
    CType(Me.fmPersfisica, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPersfisica.ResumeLayout(False)
    Me.fmPersfisica.PerformLayout()
    CType(Me.edAn_titolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_cognome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_nome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_sesso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmNonresidenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNonresidenti.ResumeLayout(False)
    Me.fmNonresidenti.PerformLayout()
    CType(Me.edAn_estcodiso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_estpariva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codfisest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_nazion2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_nazion1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmNascita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNascita.ResumeLayout(False)
    Me.fmNascita.PerformLayout()
    CType(Me.edAn_datnasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_pronasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_citnasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codcomn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_stanasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_condom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_soggresi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_profes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag3.ResumeLayout(False)
    CType(Me.fmPosition, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPosition.ResumeLayout(False)
    Me.fmPosition.PerformLayout()
    CType(Me.edAn_latitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_longitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmConai, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmConai.ResumeLayout(False)
    Me.fmConai.PerformLayout()
    CType(Me.edAn_perescon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_gescon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAcquisizione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAcquisizione.ResumeLayout(False)
    Me.fmAcquisizione.PerformLayout()
    CType(Me.edAn_contatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_dtaper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmWeb.ResumeLayout(False)
    Me.fmWeb.PerformLayout()
    CType(Me.edAn_webpwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_website.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_webuid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDatiSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDatiSx.ResumeLayout(False)
    Me.pnDatiSx.PerformLayout()
    CType(Me.fmTesoreria, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTesoreria.ResumeLayout(False)
    Me.fmTesoreria.PerformLayout()
    CType(Me.cbAn_trating.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codvfde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rating.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_agcontrop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_zona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codling.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_categ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_agente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_clascon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codcana.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_claprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_agente2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnDatiContabili, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDatiContabili.ResumeLayout(False)
    Me.pnDatiContabili.PerformLayout()
    CType(Me.fmCadc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCadc.ResumeLayout(False)
    Me.fmCadc.PerformLayout()
    CType(Me.edAn_coddicv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_coddica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codtcdc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_privacy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_valuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codrtac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDaticondSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDaticondSx.ResumeLayout(False)
    Me.pnDaticondSx.PerformLayout()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPagamento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPagamento.ResumeLayout(False)
    Me.fmPagamento.PerformLayout()
    CType(Me.edAn_codpagscagl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpagscagl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpaga3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpaga2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_giofiss.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_mesees2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_mesees1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_intragr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codnscol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRiclassificati.ResumeLayout(False)
    CType(Me.pnRiclassificazioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRiclassificazioni.ResumeLayout(False)
    Me.pnRiclassificazioni.PerformLayout()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_contfatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnFornitura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFornitura.ResumeLayout(False)
    Me.pnFornitura.PerformLayout()
    CType(Me.edAn_coduffpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_webvis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_idmandrid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_dtmandrid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_tiporid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCondForndx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCondForndx.ResumeLayout(False)
    Me.pnCondForndx.PerformLayout()
    CType(Me.cbAn_blocco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_fatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_perfatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_gcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_iban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_bolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_spinc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_vuoti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCondFornsx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCondFornsx.ResumeLayout(False)
    Me.pnCondFornsx.PerformLayout()
    CType(Me.cbAn_acuradi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIbanitalia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIbanitalia.ResumeLayout(False)
    Me.fmIbanitalia.PerformLayout()
    CType(Me.edAn_swift.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifriba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_prefiban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_banc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_cin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_banc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_cab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_abi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_porto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_vett2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_vett.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_listino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_fido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnExport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnExport.ResumeLayout(False)
    CType(Me.pnExportSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnExportSx.ResumeLayout(False)
    Me.pnExportSx.PerformLayout()
    CType(Me.edAn_paepag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDichIntento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDichIntento.ResumeLayout(False)
    Me.fmDichIntento.PerformLayout()
    CType(Me.edAn_numdicp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_numdic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_datdicp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_maxdic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_scaddic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_datdic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codese.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codntra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage7.ResumeLayout(False)
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNote.ResumeLayout(False)
    Me.pnNote.PerformLayout()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage8.ResumeLayout(False)
    CType(Me.pnListini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnListini.ResumeLayout(False)
    Me.NtsTabPage9.ResumeLayout(False)
    CType(Me.pnSconti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSconti.ResumeLayout(False)
    Me.NtsTabPage10.ResumeLayout(False)
    CType(Me.pnProvvigioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProvvigioni.ResumeLayout(False)
    CType(Me.edAn_conto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_descr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_persfg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_siglaric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.cbAn_privato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpagadet3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpagadet2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codpagadet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CLIE", "BE__CLIE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleClie = CType(oTmp, CLE__CLIE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CLIE", strRemoteServer, strRemotePort)
    AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleClie.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbOrganizzazione.GlyphPath = (oApp.ChildImageDir & "\selez_cli.gif")
        tlbOle.GlyphPath = (oApp.ChildImageDir & "\ole_1.gif")
        tlbCli.GlyphPath = (oApp.ChildImageDir & "\clie.gif")
        tlbForn.GlyphPath = (oApp.ChildImageDir & "\forn.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbContratti.GlyphPath = (oApp.ChildImageDir & "\anaext.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAn_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065537214000, "Codice"), tabanagen)
      edAn_descr1.NTSSetParam(oMenu, oApp.Tr(Me, 128738065550786000, "Rag. sociale"), 50, False)
      edAn_descr2.NTSSetParam(oMenu, oApp.Tr(Me, 128738065564202000, "Rag. sociale 2"), 50, True)
      edAn_indir.NTSSetParam(oMenu, oApp.Tr(Me, 128738065577462000, "Indirizzo"), 70, True)
      edAn_cap.NTSSetParam(oMenu, oApp.Tr(Me, 128738065590878000, "Cap"), 9, True)
      edAn_citta.NTSSetParam(oMenu, oApp.Tr(Me, 128738065605230000, "Citta/località"), 50, True)
      edAn_prov.NTSSetParam(oMenu, oApp.Tr(Me, 128738065618802000, "Prov"), 2, True)
      edAn_stato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065632062000, "Cod. stato estero"), tabstat, True)
      edAn_codfis.NTSSetParam(oMenu, oApp.Tr(Me, 128738065645166000, "Cod. fiscale"), 16, True)
      edAn_pariva.NTSSetParam(oMenu, oApp.Tr(Me, 128738065659674000, "Partita IVA"), 11, True)
      edAn_telef.NTSSetParam(oMenu, oApp.Tr(Me, 128738065672934000, "Telefono"), 18, True)
      edAn_faxtlx.NTSSetParam(oMenu, oApp.Tr(Me, 128738065686038000, "Fax"), 18, True)
      edAn_valuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065698830000, "Valuta"), tabvalu)
      edAn_codling.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065710686000, "Lingua"), tabling)
      edAn_destin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065720982000, "Destin. merce"), tabdestdiv)
      edAn_destpag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065731746000, "Destin.pagam."), tabdestdiv)
      edAn_note.NTSSetParam(oMenu, oApp.Tr(Me, 128738065743758000, "Note brevi"), 30, True)
      edAn_note2.NTSSetParam(oMenu, oApp.Tr(Me, 128738065784942000, "Note"), 0, True)
      edAn_email.NTSSetParam(oMenu, oApp.Tr(Me, 128738065801790000, "E-mail"), 100, True)
      edAn_website.NTSSetParam(oMenu, oApp.Tr(Me, 128738065812242000, "Sito Web"), 50, True)
      cbAn_usaem.NTSSetParam(oApp.Tr(Me, 128738065822382000, "Modalità di corrispondenza"))
      edAn_webuid.NTSSetParam(oMenu, oApp.Tr(Me, 128738065832834000, "UserID sito Web"), 20, True)
      edAn_webpwd.NTSSetParam(oMenu, oApp.Tr(Me, 128738065844534000, "Pwd sito Web"), 20, True)
      cbAn_sesso.NTSSetParam(oApp.Tr(Me, 128738065855298000, "Sesso"))
      edAn_datnasc.NTSSetParam(oMenu, oApp.Tr(Me, 128738065866218000, "Data nasc./costituz."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edAn_citnasc.NTSSetParam(oMenu, oApp.Tr(Me, 128738065876670000, "Città nasc."), 50, True)
      edAn_pronasc.NTSSetParam(oMenu, oApp.Tr(Me, 128738065902722000, "Prov. nasc."), 2, True)
      edAn_stanasc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065912862000, "Stato nasc."), tabstat, True)
      edAn_codfisest.NTSSetParam(oMenu, oApp.Tr(Me, 128738065922846000, "Id. fisc. estero"), 25, True)
      edAn_cell.NTSSetParam(oMenu, oApp.Tr(Me, 128738065932206000, "Cellulare"), 18, True)
      edAn_titolo.NTSSetParam(oMenu, oApp.Tr(Me, 128738065941410000, "Titolo"), 8, True)
      cbAn_persfg.NTSSetParam(oApp.Tr(Me, 128738065950770000, "Tipo sogg."))
      ckAn_profes.NTSSetParam(oMenu, oApp.Tr(Me, 128738065960286000, "Professionista"), "S", "N")
      ckAn_condom.NTSSetParam(oMenu, oApp.Tr(Me, 128738065970426000, "Condominio"), "S", "N")
      cbAn_tpsogiva.NTSSetParam(oApp.Tr(Me, 128738065979942000, "Tipo sogg. IVA"))
      edAn_codcomu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738065991174000, "Cod. comune"), tabcomuni, True)
      edAn_siglaric.NTSSetParam(oMenu, oApp.Tr(Me, 128738066001626000, "Sigla ricerca"), 20, True)
      edAn_cognome.NTSSetParam(oMenu, oApp.Tr(Me, 128738066013638000, "Cognome"), 30, True)
      edAn_nome.NTSSetParam(oMenu, oApp.Tr(Me, 128271029889414656, "Nome"), 30, True)
      edAn_codcomn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066054666000, "Cod. comune nasc."), tabcomuni, True)
      edAn_nazion1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066066834000, "Cod. nazion. 1"), tabstat, True)
      edAn_nazion2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066076818000, "Cod. nazion. 2"), tabstat, True)
      edAn_statofed.NTSSetParam(oMenu, oApp.Tr(Me, 128738066086958000, "Stato fed./contea"), 30, True)
      ckAn_soggresi.NTSSetParam(oMenu, oApp.Tr(Me, 128738066097254000, "Residente"), "S", "N")
      ckAn_omocodice.NTSSetParam(oMenu, oApp.Tr(Me, 128738066107238000, "Omocodice"), "S", "N")
      edAn_estcodiso.NTSSetParam(oMenu, oApp.Tr(Me, 128738066118470000, "Cod.ISO stato estero"), 3, True)
      edAn_estpariva.NTSSetParam(oMenu, oApp.Tr(Me, 128738066127986000, "Id. IVA stato estero"), 12, True)
      edAn_codrtac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066137658000, "Tipo di assoggettamento a ritenuta d'acconto"), tabrtac)
      ckDestresan.NTSSetParam(oMenu, oApp.Tr(Me, 128738066148578000, "Inserito"), "S", "N")
      ckDestcorr.NTSSetParam(oMenu, oApp.Tr(Me, 128738066159342000, "Inserito"), "S", "N")
      ckDestsedel.NTSSetParam(oMenu, oApp.Tr(Me, 128271366565228000, "Inserito"), "S", "N")
      ckDestdomf.NTSSetParam(oMenu, oApp.Tr(Me, 128271366565384000, "Inserito"), "S", "N")
      ckAn_intragr.NTSSetParam(oMenu, oApp.Tr(Me, 128365866643948000, "Intragruppo"), "S", "N")
      cbAn_privacy.NTSSetParam(oApp.Tr(Me, 128365866916636000, "Privacy"))
      edAn_cin.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751612000, "Cin"), 1, True)
      edAn_prefiban.NTSSetParam(oMenu, oApp.Tr(Me, 128738066363394000, "Prefisso IBAN italia"), 4, True)
      edAn_iban.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751924000, "IBAN estero"), 70, True)
      cbAn_trating.NTSSetParam(oApp.Tr(Me, 129525996340650134, "Rating"))
      edAn_codvfde.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129525996340806386, "Voce finanziaria"), tabvfde, True)
      cbAn_privato.NTSSetParam(oApp.Tr(Me, 129526012367944445, "Cliente privato"))

      edAn_contatt.NTSSetParam(oMenu, oApp.Tr(Me, 128365902166996000, "Contatto"), 30, True)
      edAn_dtaper.NTSSetParam(oMenu, oApp.Tr(Me, 128365902167464000, "Data acquisizione"), True)
      edAn_rating.NTSSetParam(oMenu, oApp.Tr(Me, 128365902168400000, "Affidabilità"), 3, True)
      edAn_agcontrop.NTSSetParam(oMenu, oApp.Tr(Me, 128365902169024000, "Aggiunta contropartita articolo"), "0", 4, 0, 9999)
      edAn_zona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902169492000, "Zona geografica"), tabzone)
      edAn_categ.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902170584000, "Categoria"), tabcate)
      edAn_agente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902171052000, "Agente"), tabcage)
      edAn_clascon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902171676000, "Classe di sconto"), tabcscl)
      edAn_claprov.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902172768000, "Classe provvigioni"), tabcpcl)
      edAn_codcana.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902172300000, "Canale di vendita"), tabcana)
      edAn_listino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902172612000, "Listino"), tablist)
      edAn_agente2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902173392000, "Agente 2"), tabcage)
      edAn_contfatt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902174172000, "Conto fatturazione"), tabanagrac)
      edAn_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960229616000, "Controp. ratei/risconti"), tabanagras)
      edAn_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128365902175108000, "Codice nostra banca"), tabbanc)
      cbAn_fatt.NTSSetParam(oApp.Tr(Me, 128365902175420000, "tipo fatturazione"))
      ckAn_partite.NTSSetParam(oMenu, oApp.Tr(Me, 128326960243166000, "Gestione partite"), "S", "N")
      ckAn_scaden.NTSSetParam(oMenu, oApp.Tr(Me, 128326960257806000, "Gestione scadenze"), "S", "N")
      edAn_kpccee.NTSSetParam(oMenu, oApp.Tr(Me, 128326960331886000, "Saldo dare CEE"), 8, True)
      edAn_kpccee2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960345646000, "Saldo avere CEE"), 8, True)
      edAn_rifricd.NTSSetParam(oMenu, oApp.Tr(Me, 128326960360916000, "Sado dare RICLASS."), 8, True)
      edAn_rifrica.NTSSetParam(oMenu, oApp.Tr(Me, 128326960384356000, "Saldo avere RICLASS"), 8, True)
      edAn_giofiss.NTSSetParam(oMenu, oApp.Tr(Me, 128370191677734000, "Giorno fisso di pagamento"), "0", 2, 0, 31)
      edAn_codpag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128370191677890000, "Codice pagamento"), tabpaga)
      edAn_codpaga2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130565523980544861, "Codice pagamento importi minimi"), tabpaga)
      edAn_codpaga3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130565524000427824, "Codice pagamento grandi importi"), tabpaga)
      edAn_codpagadet.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130952452437893644, "Codice pagamento articoli deteriorabili"), tabpaga)
      edAn_codpagadet2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130952452461257602, "Codice pagamento importi minimi articoli deteriorabili"), tabpaga)
      edAn_codpagadet3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130952452485727613, "Codice pagamento grandi importi articoli deteriorabili"), tabpaga)
      edAn_codpagscagl1.NTSSetParam(oMenu, oApp.Tr(Me, 130565524761428821, "Limite minimo importo"), oApp.FormatImporti, 12, 0, 999999999999)
      edAn_codpagscagl2.NTSSetParam(oMenu, oApp.Tr(Me, 130565525112349069, "Limite grandi importi"), oApp.FormatImporti, 12, 0, 999999999999)
      edAn_mesees2.NTSSetParam(oMenu, oApp.Tr(Me, 128370191678358000, "Secondo mese escluso"), "0", 2, 0, 12)
      edAn_mesees1.NTSSetParam(oMenu, oApp.Tr(Me, 128370191678670000, "Primo mese escluso"), "0", 2, 0, 12)
      cbAn_colbil.NTSSetParam(oApp.Tr(Me, 128370191678826000, "Colonna in stampa bilancio"))
      edAn_codnscol.NTSSetParam(oMenu, oApp.Tr(Me, 128370191679138000, "Codice nostro presso di loro"), 30, True)
      cbAn_blocco.NTSSetParam(oApp.Tr(Me, 128371005393820000, "Blocco"))
      cbAn_status.NTSSetParam(oApp.Tr(Me, 128371005394132000, "Status"))
      cbAn_gcons.NTSSetParam(oApp.Tr(Me, 128371005394444000, "Giorno di consegna"))
      cbAn_perfatt.NTSSetParam(oApp.Tr(Me, 128371005394756000, "Periodo fatturazione"))
      ckAn_bolli.NTSSetParam(oMenu, oApp.Tr(Me, 128371005395068000, "Addebito Bolli"), "S", "N")
      ckAn_spinc.NTSSetParam(oMenu, oApp.Tr(Me, 128371005395224000, "Addebito sp. Incasso"), "S", "N")
      ckAn_vuoti.NTSSetParam(oMenu, oApp.Tr(Me, 128371005395380000, "Addebito Cauzioni / spese generali"), "S", "N")
      edAn_porto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371005396316000, "Porto"), tabport, True)
      edAn_banc2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023232850179, "Descrizione Filiale"), 50)
      edAn_banc1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023233006352, "Descrizione Banca"), 50)
      edAn_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233162525, "Codice CAB"), tababicab)
      edAn_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233318698, "Codice ABI"), tababi)
      edAn_swift.NTSSetParam(oMenu, oApp.Tr(Me, 130420231454944538, "Codice BIC/SWIFT"), 14, True)
      edAn_dtmandrid.NTSSetParam(oMenu, oApp.Tr(Me, 130187048738861619, "Data sottoscrizione mandato RID"), True)
      edAn_idmandrid.NTSSetParam(oMenu, oApp.Tr(Me, 130190644318185364, "ID mandato RID"), 35, True)
      cbAn_tiporid.NTSSetParam(oApp.Tr(Me, 130187048596353602, "Tipo RID"))
      edAn_codtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371005397096000, "Tipo bolla/fattura"), tabtpbf)
      edAn_vett2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371005397564000, "Vettore 2"), tabvett)
      edAn_vett.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371005398500000, "Vettore"), tabvett)
      cbAn_acuradi.NTSSetParam(oApp.Tr(Me, 129055173884392804, "Tipo trasporto"))
      edAn_fido.NTSSetParam(oMenu, oApp.Tr(Me, 128371005399436000, "Importo fido"), oApp.FormatImporti, 12, 0, 999999999999)
      edAn_rifriba.NTSSetParam(oMenu, oApp.Tr(Me, 128371005399592000, "Numero C/C"), 70, True)
      edAn_perescon.NTSSetParam(oMenu, oApp.Tr(Me, 128371099083546000, "Percentuale di esenzione Conai"), oApp.FormatSconti, 6, 0, 100)
      cbAn_gescon.NTSSetParam(oApp.Tr(Me, 128371099083702000, "Applicazione Conai"))
      edAn_numdicp.NTSSetParam(oMenu, oApp.Tr(Me, 128371099106322000, "Numero protocollo dichiarazione d'intento"), 16, True)
      edAn_datdicp.NTSSetParam(oMenu, oApp.Tr(Me, 128371099106790000, "Data protocollo dichiarazione d'intento"), True)
      edAn_maxdic.NTSSetParam(oMenu, oApp.Tr(Me, 128371099106946000, "Importo massimo dichiarazione d'intento"), oApp.FormatImporti, 12, 0, 999999999999)
      edAn_numdic.NTSSetParam(oMenu, oApp.Tr(Me, 128371099107258000, "Numero dichiarazione d'intento"), 16)
      edAn_scaddic.NTSSetParam(oMenu, oApp.Tr(Me, 128371099107726000, "Data scadenza dichiarazione d'intento"), True)
      edAn_datdic.NTSSetParam(oMenu, oApp.Tr(Me, 128371099108038000, "Data dichiarazione d'intento"), True)
      edAn_codese.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371099108350000, "Codice esenzione IVA"), tabciva)
      edAn_codntra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371099108506000, "Natura transazione"), tabntra)
      edAn_paepag.NTSSetParam(oMenu, oApp.Tr(Me, 130693568128138703, "Paese di pagamento intrastat servizi"), 2, True)
      edAn_codtcdc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319672117777172, "Tipologia entità"), tabtcdc)
      edAn_coddica.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319672118089726, "Aggregazione budget"), tabdica, True)
      edAn_coddicv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319672118871111, "Valore di aggregazione budget"), tabdicv, True)
      edAn_latitud.NTSSetParam(oMenu, oApp.Tr(Me, 130420233615604309, "Latitudine"), 15, True)
      edAn_longitud.NTSSetParam(oMenu, oApp.Tr(Me, 130420233965336569, "Longitudine"), 15, True)
      ckAn_webvis.NTSSetParam(oMenu, oApp.Tr(Me, 130415294122244968, "Cliente/Fornitore visibile dall'applicazione esterna"), "S", "N")
      edAn_coduffpa.NTSSetParam(oMenu, oApp.Tr(Me, 130589028621694669, "Codice ufficio PA"), 50, True)

      ceListini.NTSSetParam(oMenu, "Listini", "BN__CLIE", Nothing)
      ceListini.tlbListEsci.Enabled = False
      ceListini.LcTipo = " "
      ceListini.LcCodart = ""
      ceListini.LcConto = 0
      AddHandler ceListini.VaiScontoCollegato, AddressOf ceListini_VaiScontoCollegato


      ceSconti.NTSSetParam(oMenu, "Sconti", "BN__CLIE", Nothing)
      ceSconti.tlbScontiEsci.Enabled = False
      ceSconti.TipoSconto = 0
      ceSconti.SoCodart = ""
      ceSconti.SoConto = 0
      ceSconti.SoClasseCli = 0
      ceSconti.SoClasseArt = 0
      AddHandler ceSconti.VaiListinoCollegato, AddressOf ceSconti_VaiListinoCollegato

      ceProvvig.NTSSetParam(oMenu, "Provvigioni", "BN__CLIE", Nothing)
      ceProvvig.tlbProvEsci.Enabled = False
      ceProvvig.TipoProvv = 1
      ceProvvig.PerCodart = ""
      ceProvvig.PerConto = 0
      ceProvvig.PerCodcage = 0
      ceProvvig.PerClasseCli = 0
      ceProvvig.PerClasseArt = 0

      edAn_conto.NTSForzaVisZoom = False
      edAn_citta.NTSForzaVisZoom = True
      edAn_cap.NTSForzaVisZoom = True
      edAn_descr1.NTSSetRichiesto()
      cbAn_tpsogiva.NTSSetRichiesto()

      edAn_kpccee.NTSSetParamZoom("ZOOMHLCE")
      edAn_kpccee2.NTSSetParamZoom("ZOOMHLCE")
      edAn_rifricd.NTSSetParamZoom("ZOOMHLCE")
      edAn_rifrica.NTSSetParamZoom("ZOOMHLCE")

      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupDII) Then edAn_numdic.NTSForzaVisZoom = True
      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttPersfg As New DataTable()
      dttPersfg.Columns.Add("cod", GetType(String))
      dttPersfg.Columns.Add("val", GetType(String))
      dttPersfg.Rows.Add(New Object() {"F", "Persona fisica"})
      dttPersfg.Rows.Add(New Object() {"G", "Persona giuridica"})
      dttPersfg.AcceptChanges()
      cbAn_persfg.DataSource = dttPersfg
      cbAn_persfg.ValueMember = "cod"
      cbAn_persfg.DisplayMember = "val"

      Dim dttSesso As New DataTable()
      dttSesso.Columns.Add("cod", GetType(String))
      dttSesso.Columns.Add("val", GetType(String))
      dttSesso.Rows.Add(New Object() {"M", "Maschio"})
      dttSesso.Rows.Add(New Object() {"F", "Femmina"})
      dttSesso.Rows.Add(New Object() {"S", "Pers.Giuridica"})
      dttSesso.AcceptChanges()
      cbAn_sesso.DataSource = dttSesso
      cbAn_sesso.ValueMember = "cod"
      cbAn_sesso.DisplayMember = "val"

      Dim dttSogiva As New DataTable()
      dttSogiva.Columns.Add("cod", GetType(String))
      dttSogiva.Columns.Add("val", GetType(String))
      dttSogiva.Rows.Add(New Object() {"N", "(Normale)"})
      dttSogiva.Rows.Add(New Object() {"L", "Escluso da IVA 11"})
      dttSogiva.Rows.Add(New Object() {"I", "Intra CEE"})
      dttSogiva.Rows.Add(New Object() {"E", "Extra CEE"})
      dttSogiva.Rows.Add(New Object() {"R", "R.S.M."})
      dttSogiva.Rows.Add(New Object() {"D", "Dogana"})
      dttSogiva.AcceptChanges()
      cbAn_tpsogiva.DataSource = dttSogiva
      cbAn_tpsogiva.ValueMember = "cod"
      cbAn_tpsogiva.DisplayMember = "val"

      Dim dttTipoSend As New DataTable()
      dttTipoSend.Columns.Add("cod", GetType(String))
      dttTipoSend.Columns.Add("val", GetType(String))
      dttTipoSend.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipoSend.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      'dttTipoSend.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"}) non più supportato
      dttTipoSend.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipoSend.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipoSend.Rows.Add(New Object() {"H", "HylaFAX"})
      dttTipoSend.AcceptChanges()
      cbAn_usaem.DataSource = dttTipoSend
      cbAn_usaem.ValueMember = "cod"
      cbAn_usaem.DisplayMember = "val"


      Dim dttPrivacy As New DataTable()
      dttPrivacy.Columns.Add("cod", GetType(String))
      dttPrivacy.Columns.Add("val", GetType(String))
      dttPrivacy.Rows.Add(New Object() {" ", "(Non definita)"})
      dttPrivacy.Rows.Add(New Object() {"S", "Concessa"})
      dttPrivacy.Rows.Add(New Object() {"N", "Non concessa"})
      dttPrivacy.AcceptChanges()
      cbAn_privacy.DataSource = dttPrivacy
      cbAn_privacy.ValueMember = "cod"
      cbAn_privacy.DisplayMember = "val"

      Dim dttTipfatt As New DataTable()
      dttTipfatt.Columns.Add("cod", GetType(String))
      dttTipfatt.Columns.Add("val", GetType(String))
      dttTipfatt.Rows.Add(New Object() {"R", "Riepilogativa"})
      dttTipfatt.Rows.Add(New Object() {"A", "Accompagnatoria"})
      dttTipfatt.Rows.Add(New Object() {"B", "Per bolla"})
      dttTipfatt.Rows.Add(New Object() {"D", "Riepilog. per destin."})
      dttTipfatt.AcceptChanges()
      cbAn_fatt.DataSource = dttTipfatt
      cbAn_fatt.ValueMember = "cod"
      cbAn_fatt.DisplayMember = "val"

      Dim dttColBil As New DataTable()
      dttColBil.Columns.Add("cod", GetType(String))
      dttColBil.Columns.Add("val", GetType(String))
      dttColBil.Rows.Add(New Object() {"0", "Non interessa"})
      dttColBil.Rows.Add(New Object() {"1", "Sezione opposta"})
      dttColBil.Rows.Add(New Object() {"2", "Dare"})
      dttColBil.Rows.Add(New Object() {"3", "Avere"})
      dttColBil.AcceptChanges()
      cbAn_colbil.DataSource = dttColBil
      cbAn_colbil.ValueMember = "cod"
      cbAn_colbil.DisplayMember = "val"

      Dim dttPerfatt As New DataTable()
      dttPerfatt.Columns.Add("cod", GetType(String))
      dttPerfatt.Columns.Add("val", GetType(String))
      dttPerfatt.Rows.Add(New Object() {" ", "Qualsiasi"})
      dttPerfatt.Rows.Add(New Object() {"M", "Mensile"})
      dttPerfatt.Rows.Add(New Object() {"S", "Settimanale"})
      dttPerfatt.Rows.Add(New Object() {"Q", "Quindicinale"})
      dttPerfatt.Rows.Add(New Object() {"G", "Giornaliero"})
      dttPerfatt.AcceptChanges()
      cbAn_perfatt.DataSource = dttPerfatt
      cbAn_perfatt.ValueMember = "cod"
      cbAn_perfatt.DisplayMember = "val"

      Dim dttGcons As New DataTable()
      dttGcons.Columns.Add("cod", GetType(Short))
      dttGcons.Columns.Add("val", GetType(String))
      dttGcons.Rows.Add(New Object() {1, "Lunedì"})
      dttGcons.Rows.Add(New Object() {2, "Martedì"})
      dttGcons.Rows.Add(New Object() {3, "Mercoledì"})
      dttGcons.Rows.Add(New Object() {4, "Giovedì"})
      dttGcons.Rows.Add(New Object() {5, "Venerdì"})
      dttGcons.Rows.Add(New Object() {6, "Sabato"})
      dttGcons.Rows.Add(New Object() {7, "Domenica"})
      dttGcons.Rows.Add(New Object() {8, "(Qualsiasi)"})
      dttGcons.AcceptChanges()
      cbAn_gcons.DataSource = dttGcons
      cbAn_gcons.ValueMember = "cod"
      cbAn_gcons.DisplayMember = "val"

      Dim dttStatus As New DataTable()
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {"A", "Attivo"})
      dttStatus.Rows.Add(New Object() {"I", "Inattivo"})
      dttStatus.Rows.Add(New Object() {"P", "Potenziale"})
      dttStatus.AcceptChanges()
      cbAn_status.DataSource = dttStatus
      cbAn_status.ValueMember = "cod"
      cbAn_status.DisplayMember = "val"

      Dim dttBlocco As New DataTable()
      dttBlocco.Columns.Add("cod", GetType(String))
      dttBlocco.Columns.Add("val", GetType(String))
      dttBlocco.Rows.Add(New Object() {"F", "Fuori fido"})
      dttBlocco.Rows.Add(New Object() {"I", "Insoluti"})
      dttBlocco.Rows.Add(New Object() {"N", "Nessuno"})
      dttBlocco.Rows.Add(New Object() {"R", "Rim. dirette scadute"})
      dttBlocco.Rows.Add(New Object() {"B", "Blocco fisso"})
      dttBlocco.AcceptChanges()
      cbAn_blocco.DataSource = dttBlocco
      cbAn_blocco.ValueMember = "cod"
      cbAn_blocco.DisplayMember = "val"

      Dim dttConai As New DataTable()
      dttConai.Columns.Add("cod", GetType(String))
      dttConai.Columns.Add("val", GetType(String))
      dttConai.Rows.Add(New Object() {"E", "Esente"})
      dttConai.Rows.Add(New Object() {"S", "Si"})
      dttConai.Rows.Add(New Object() {"N", "No"})
      dttConai.AcceptChanges()
      cbAn_gescon.DataSource = dttConai
      cbAn_gescon.ValueMember = "cod"
      cbAn_gescon.DisplayMember = "val"

      Dim dttRating As New DataTable()
      dttRating.Columns.Add("cod", GetType(String))
      dttRating.Columns.Add("val", GetType(String))
      dttRating.Rows.Add(New Object() {"1", "Inderogabile"})
      dttRating.Rows.Add(New Object() {"2", "Certo"})
      dttRating.Rows.Add(New Object() {"3", "Incerto"})
      dttRating.Rows.Add(New Object() {"4", "Inattendibile"})
      dttRating.Rows.Add(New Object() {" ", "Non gestito"})
      dttRating.AcceptChanges()
      cbAn_trating.DataSource = dttRating
      cbAn_trating.ValueMember = "cod"
      cbAn_trating.DisplayMember = "val"

      Dim dttPrivato As New DataTable()
      dttPrivato.Columns.Add("cod", GetType(String))
      dttPrivato.Columns.Add("val", GetType(String))
      dttPrivato.Rows.Add(New Object() {"N", "Azienda"})
      dttPrivato.Rows.Add(New Object() {"S", "Privato"})
      dttPrivato.AcceptChanges()
      cbAn_privato.DataSource = dttPrivato
      cbAn_privato.ValueMember = "cod"
      cbAn_privato.DisplayMember = "val"

      Dim dttTiporid As New DataTable()
      dttTiporid.Columns.Add("cod", GetType(String))
      dttTiporid.Columns.Add("val", GetType(String))
      dttTiporid.Rows.Add(New Object() {" ", "No RID"})
      dttTiporid.Rows.Add(New Object() {"C", "SDD CORE/RID ordinario"})
      dttTiporid.Rows.Add(New Object() {"B", "SDD B2B/RID veloce"})
      dttTiporid.AcceptChanges()
      cbAn_tiporid.DataSource = dttTiporid
      cbAn_tiporid.ValueMember = "cod"
      cbAn_tiporid.DisplayMember = "val"

      Dim dttTrasporto As New DataTable()
      dttTrasporto.Columns.Add("cod", GetType(String))
      dttTrasporto.Columns.Add("val", GetType(String))
      dttTrasporto.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttTrasporto.Rows.Add(New Object() {"D", "Destinatario"})
      dttTrasporto.Rows.Add(New Object() {"M", "Mittente"})
      dttTrasporto.Rows.Add(New Object() {"V", "Vettore"})
      dttTrasporto.Rows.Add(New Object() {"X", "Non proposto in ordini/documenti"})
      cbAn_acuradi.DataSource = dttTrasporto
      cbAn_acuradi.ValueMember = "cod"
      cbAn_acuradi.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAn_conto.NTSDbField = "ANAGRA.an_conto"
      edAn_descr1.NTSDbField = "ANAGRA.an_descr1"
      edAn_descr2.NTSDbField = "ANAGRA.an_descr2"
      edAn_indir.NTSDbField = "ANAGRA.an_indir"
      edAn_cap.NTSDbField = "ANAGRA.an_cap"
      edAn_citta.NTSDbField = "ANAGRA.an_citta"
      edAn_prov.NTSDbField = "ANAGRA.an_prov"
      edAn_stato.NTSDbField = "ANAGRA.an_stato"
      edAn_codfis.NTSDbField = "ANAGRA.an_codfis"
      edAn_pariva.NTSDbField = "ANAGRA.an_pariva"
      edAn_telef.NTSDbField = "ANAGRA.an_telef"
      edAn_faxtlx.NTSDbField = "ANAGRA.an_faxtlx"
      edAn_valuta.NTSDbField = "ANAGRA.an_valuta"
      edAn_codling.NTSDbField = "ANAGRA.an_codling"
      edAn_destin.NTSDbField = "ANAGRA.an_destin"
      edAn_destpag.NTSDbField = "ANAGRA.an_destpag"
      edAn_note.NTSDbField = "ANAGRA.an_note"
      edAn_note2.NTSDbField = "ANAGRA.an_note2"
      edAn_email.NTSDbField = "ANAGRA.an_email"
      edAn_website.NTSDbField = "ANAGRA.an_website"
      cbAn_usaem.NTSDbField = "ANAGRA.an_usaem"
      edAn_webuid.NTSDbField = "ANAGRA.an_webuid"
      edAn_webpwd.NTSDbField = "ANAGRA.an_webpwd"
      cbAn_sesso.NTSDbField = "ANAGRA.an_sesso"
      edAn_datnasc.NTSDbField = "ANAGRA.an_datnasc"
      edAn_citnasc.NTSDbField = "ANAGRA.an_citnasc"
      edAn_pronasc.NTSDbField = "ANAGRA.an_pronasc"
      edAn_stanasc.NTSDbField = "ANAGRA.an_stanasc"
      edAn_codfisest.NTSDbField = "ANAGRA.an_codfisest"
      edAn_cell.NTSDbField = "ANAGRA.an_cell"
      edAn_titolo.NTSDbField = "ANAGRA.an_titolo"
      cbAn_persfg.NTSDbField = "ANAGRA.an_persfg"
      ckAn_profes.NTSText.NTSDbField = "ANAGRA.an_profes"
      ckAn_condom.NTSText.NTSDbField = "ANAGRA.an_condom"
      cbAn_tpsogiva.NTSDbField = "ANAGRA.an_tpsogiva"
      edAn_codcomu.NTSDbField = "ANAGRA.an_codcomu"
      edAn_siglaric.NTSDbField = "ANAGRA.an_siglaric"
      edAn_cognome.NTSDbField = "ANAGRA.an_cognome"
      edAn_nome.NTSDbField = "ANAGRA.an_nome"
      edAn_codcomn.NTSDbField = "ANAGRA.an_codcomn"
      edAn_nazion1.NTSDbField = "ANAGRA.an_nazion1"
      edAn_nazion2.NTSDbField = "ANAGRA.an_nazion2"
      edAn_statofed.NTSDbField = "ANAGRA.an_statofed"
      ckAn_soggresi.NTSText.NTSDbField = "ANAGRA.an_soggresi"
      ckAn_omocodice.NTSText.NTSDbField = "ANAGRA.an_omocodice"
      edAn_estcodiso.NTSDbField = "ANAGRA.an_estcodiso"
      edAn_estpariva.NTSDbField = "ANAGRA.an_estpariva"
      edAn_codrtac.NTSDbField = "ANAGRA.an_codrtac"
      cbAn_privacy.NTSDbField = "ANAGRA.an_privacy"
      cbAn_fatt.NTSDbField = "ANAGRA.an_fatt"
      lbXx_codling.NTSDbField = "ANAGRA.xx_codling"
      lbXx_valuta.NTSDbField = "ANAGRA.xx_valuta"
      lbXx_codrtac.NTSDbField = "ANAGRA.xx_codrtac"
      lbXx_stato.NTSDbField = "ANAGRA.xx_stato"
      lbXx_codcomu.NTSDbField = "ANAGRA.xx_codcomu"
      lbXx_codcomn.NTSDbField = "ANAGRA.xx_codcomn"
      lbXx_stanasc.NTSDbField = "ANAGRA.xx_stanasc"
      lbXx_nazion1.NTSDbField = "ANAGRA.xx_nazion1"
      lbXx_nazion2.NTSDbField = "ANAGRA.xx_nazion2"
      lbXx_destin.NTSDbField = "ANAGRA.xx_destin"
      lbXx_destpag.NTSDbField = "ANAGRA.xx_destpag"
      lbXx_codmast.NTSDbField = "ANAGRA.xx_codmast"
      edAn_contatt.NTSDbField = "ANAGRA.an_contatt"
      edAn_dtaper.NTSDbField = "ANAGRA.an_dtaper"
      edAn_rating.NTSDbField = "ANAGRA.an_rating"
      lbXx_zona.NTSDbField = "ANAGRA.xx_zona"
      edAn_agcontrop.NTSDbField = "ANAGRA.an_agcontrop"
      edAn_controp.NTSDbField = "ANAGRA.an_controp"
      lbXx_controp.NTSDbField = "ANAGRA.xx_controp"
      edAn_zona.NTSDbField = "ANAGRA.an_zona"
      lbXx_categ.NTSDbField = "ANAGRA.xx_categ"
      edAn_categ.NTSDbField = "ANAGRA.an_categ"
      lbXx_agente.NTSDbField = "ANAGRA.xx_agente"
      edAn_agente.NTSDbField = "ANAGRA.an_agente"
      lbXx_codcana.NTSDbField = "ANAGRA.xx_codcana"
      edAn_clascon.NTSDbField = "ANAGRA.an_clascon"
      lbXx_listino.NTSDbField = "ANAGRA.xx_listino"
      lbXx_clascon.NTSDbField = "ANAGRA.xx_clascon"
      lbXx_claprov.NTSDbField = "ANAGRA.xx_claprov"
      edAn_codcana.NTSDbField = "ANAGRA.an_codcana"
      edAn_listino.NTSDbField = "ANAGRA.an_listino"
      edAn_claprov.NTSDbField = "ANAGRA.an_claprov"
      lbXx_agente2.NTSDbField = "ANAGRA.xx_agente2"
      edAn_agente2.NTSDbField = "ANAGRA.an_agente2"
      lbXx_contfatt.NTSDbField = "ANAGRA.xx_contfatt"
      edAn_contfatt.NTSDbField = "ANAGRA.an_contfatt"
      ckAn_intragr.NTSText.NTSDbField = "ANAGRA.an_intragr"
      lbXx_codbanc.NTSDbField = "ANAGRA.xx_codbanc"
      edAn_codbanc.NTSDbField = "ANAGRA.an_codbanc"
      ckAn_partite.NTSText.NTSDbField = "ANAGRA.an_partite"
      ckAn_scaden.NTSText.NTSDbField = "ANAGRA.an_scaden"
      edAn_kpccee.NTSDbField = "ANAGRA.an_kpccee"
      edAn_kpccee2.NTSDbField = "ANAGRA.an_kpccee2"
      edAn_rifricd.NTSDbField = "ANAGRA.an_rifricd"
      edAn_rifrica.NTSDbField = "ANAGRA.an_rifrica"
      edAn_giofiss.NTSDbField = "ANAGRA.an_giofiss"
      edAn_codpag.NTSDbField = "ANAGRA.an_codpag"
      lbXx_codpag.NTSDbField = "ANAGRA.xx_codpag"
      edAn_codpaga2.NTSDbField = "ANAGRA.an_codpaga2"
      lbXx_codpaga2.NTSDbField = "ANAGRA.xx_codpaga2"
      edAn_codpaga3.NTSDbField = "ANAGRA.an_codpaga3"
      lbXx_codpaga3.NTSDbField = "ANAGRA.xx_codpaga3"
      edAn_codpagadet.NTSDbField = "ANAGRA.an_codpagadet"
      lbXx_codpagadet.NTSDbField = "ANAGRA.xx_codpagadet"
      edAn_codpagadet2.NTSDbField = "ANAGRA.an_codpagadet2"
      lbXx_codpagadet2.NTSDbField = "ANAGRA.xx_codpagadet2"
      edAn_codpagadet3.NTSDbField = "ANAGRA.an_codpagadet3"
      lbXx_codpagadet3.NTSDbField = "ANAGRA.xx_codpagadet3"
      edAn_codpagscagl1.NTSDbField = "ANAGRA.an_codpagscagl1"
      edAn_codpagscagl2.NTSDbField = "ANAGRA.an_codpagscagl2"
      edAn_mesees2.NTSDbField = "ANAGRA.an_mesees2"
      edAn_mesees1.NTSDbField = "ANAGRA.an_mesees1"
      cbAn_colbil.NTSDbField = "ANAGRA.an_colbil"
      edAn_codnscol.NTSDbField = "ANAGRA.an_codnscol"
      cbAn_blocco.NTSDbField = "ANAGRA.an_blocco"
      cbAn_status.NTSDbField = "ANAGRA.an_status"
      cbAn_gcons.NTSDbField = "ANAGRA.an_gcons"
      cbAn_perfatt.NTSDbField = "ANAGRA.an_perfatt"
      ckAn_bolli.NTSText.NTSDbField = "ANAGRA.an_bolli"
      ckAn_spinc.NTSText.NTSDbField = "ANAGRA.an_spinc"
      ckAn_vuoti.NTSText.NTSDbField = "ANAGRA.an_vuoti"
      lbXx_porto.NTSDbField = "ANAGRA.xx_porto"
      edAn_porto.NTSDbField = "ANAGRA.an_porto"
      edAn_abi.NTSDbField = "ANAGRA.an_abi"
      edAn_codtpbf.NTSDbField = "ANAGRA.an_codtpbf"
      lbXx_vett2.NTSDbField = "ANAGRA.xx_vett2"
      lbXx_codtpbf.NTSDbField = "ANAGRA.xx_codtpbf"
      edAn_vett2.NTSDbField = "ANAGRA.an_vett2"
      edAn_cab.NTSDbField = "ANAGRA.an_cab"
      lbXx_vett.NTSDbField = "ANAGRA.xx_vett"
      edAn_banc1.NTSDbField = "ANAGRA.an_banc1"
      edAn_vett.NTSDbField = "ANAGRA.an_vett"
      cbAn_acuradi.NTSDbField = "ANAGRA.an_acuradi"
      edAn_banc2.NTSDbField = "ANAGRA.an_banc2"
      edAn_fido.NTSDbField = "ANAGRA.an_fido"
      edAn_rifriba.NTSDbField = "ANAGRA.an_rifriba"
      edAn_perescon.NTSDbField = "ANAGRA.an_perescon"
      cbAn_gescon.NTSDbField = "ANAGRA.an_gescon"
      edAn_numdicp.NTSDbField = "ANAGRA.an_numdicp"
      edAn_datdicp.NTSDbField = "ANAGRA.an_datdicp"
      edAn_maxdic.NTSDbField = "ANAGRA.an_maxdic"
      edAn_numdic.NTSDbField = "ANAGRA.an_numdic"
      edAn_scaddic.NTSDbField = "ANAGRA.an_scaddic"
      edAn_datdic.NTSDbField = "ANAGRA.an_datdic"
      lbXx_codese.NTSDbField = "ANAGRA.xx_codese"
      edAn_codese.NTSDbField = "ANAGRA.an_codese"
      edAn_codntra.NTSDbField = "ANAGRA.an_codntra"
      edAn_paepag.NTSDbField = "ANAGRA.an_paepag"
      lbXx_codntra.NTSDbField = "ANAGRA.xx_codntra"
      edAn_prefiban.NTSDbField = "ANAGRA.an_prefiban"
      edAn_cin.NTSDbField = "ANAGRA.an_cin"
      edAn_iban.NTSDbField = "ANAGRA.an_iban"
      edAn_codtcdc.NTSDbField = "ANAGRA.an_codtcdc"
      lbXx_codtcdc.NTSDbField = "ANAGRA.xx_codtcdc"
      edAn_coddica.NTSDbField = "ANAGRA.an_coddica"
      lbXx_coddicv.NTSDbField = "ANAGRA.xx_coddicv"
      lbXx_coddica.NTSDbField = "ANAGRA.xx_coddica"
      edAn_coddicv.NTSDbField = "ANAGRA.an_coddicv"
      lbXx_codvfde.NTSDbField = "ANAGRA.xx_codvfde"
      cbAn_trating.NTSDbField = "ANAGRA.an_trating"
      edAn_codvfde.NTSDbField = "ANAGRA.an_codvfde"
      cbAn_privato.NTSDbField = "ANAGRA.an_privato"
      edAn_latitud.NTSDbField = "ANAGRA.an_latitud"
      edAn_longitud.NTSDbField = "ANAGRA.an_longitud"
      edAn_swift.NTSDbField = "ANAGRA.an_swift"
      edAn_dtmandrid.NTSDbField = "ANAGRA.an_dtmandrid"
      edAn_idmandrid.NTSDbField = "ANAGRA.an_idmandrid"
      cbAn_tiporid.NTSDbField = "ANAGRA.an_tiporid"
      ckAn_webvis.NTSText.NTSDbField = "ANAGRA.an_webvis"
      edAn_coduffpa.NTSDbField = "ANAGRA.an_coduffpa"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcClie, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaColonneUnbound(ByRef dtrIn As DataRow)
    Dim bContoMovimentato As Boolean = False
    Try
      Me.Cursor = Cursors.WaitCursor

      oCleClie.nCurRow = dcClie.Position

      If oCleClie.bAnagen Then
        lbAnagen.Text = oApp.Tr(Me, 128366700576350000, "An.Gen.  : ") & CType(dcClie.Current, DataRowView).Row!an_codanag.ToString
      Else
        lbAnagen.Text = ""
      End If

      bEntratoInAnaext = False

      oCleClie.lAgenteFileApri = NTSCInt(CType(dcClie.Current, DataRowView).Row!an_agente)

      If oCleClie.strTipoConto = "C" Then
        If oCleClie.bNew Then
          'già caricato in precedenza
        Else
          If (oCleClie.bModuloCRM Or oCleClie.bModuloAS) Then
            oCleClie.lLead = oCleClie.CercaLeadDaConto(NTSCInt(CType(dcClie.Current, DataRowView).Row!an_conto), 0, _
                              oMenu.ModuliDittaDitt(DittaCorrente), oMenu.ModuliExtDittaDitt(DittaCorrente), _
                              oMenu.ModuliSupDittaDitt(DittaCorrente))
          Else
            oCleClie.lLead = 0
          End If
        End If
        If oCleClie.lLead <> 0 Then
          lbLead.Text = oApp.Tr(Me, 130420236771978139, "Cod.Lead: |" & oCleClie.lLead.ToString & "|")
        Else
          lbLead.Text = ""
        End If
      Else
        'fornitore
        oCleClie.lLead = 0
        lbLead.Text = ""
      End If

      '-------------------------------
      'se serve ricarico i listini
      If tsClie.SelectedTabPage.Equals(tsClie.TabPages(7)) Then
        If ceListini.LcConto <> NTSCInt(edAn_conto.Text) Then
          ceListini.LcConto = NTSCInt(edAn_conto.Text)
          ceListini.ApriListini()
        End If

        GctlSetVisEnab(ceListini, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente
          ceListini.grvList.Enabled = False
          ceListini.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceListini.grvList, False)
          ceListini.tlbMain.Visible = True
        End If
      End If

      '-------------------------------
      'se serve ricarico gli sconti
      If tsClie.SelectedTabPage.Equals(tsClie.TabPages(8)) Then
        If ceSconti.SoConto <> NTSCInt(edAn_conto.Text) Then
          ceSconti.SoConto = NTSCInt(edAn_conto.Text)
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente
          ceSconti.grvSconti.Enabled = False
          ceSconti.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceSconti.grvSconti, False)
          ceSconti.tlbMain.Visible = True
        End If
      End If

      '-------------------------------
      'se serve ricarico le provvigioni
      If tsClie.SelectedTabPage.Equals(tsClie.TabPages(9)) Then
        If ceProvvig.PerConto <> NTSCInt(edAn_conto.Text) Then
          ceProvvig.PerConto = NTSCInt(edAn_conto.Text)
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente 
          ceProvvig.grvProv.Enabled = False
          ceProvvig.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceProvvig.grvProv, False)
          ceProvvig.tlbMain.Visible = True
        End If
      End If

      If Not oCleClie.CaricaColonneUnbound(dtrIn, bContoMovimentato) Then
        Me.Close()
        Return
      End If
      If dtrIn.RowState <> DataRowState.Added Then dtrIn.AcceptChanges()

      '----------------------------------------------
      'se il record è movimentato in prinot non faccio modificare la gestione partite/scadenze
      If bContoMovimentato Then
        ckAn_partite.Enabled = False
        ckAn_scaden.Enabled = False
      Else
        GctlSetVisEnab(ckAn_partite, False)
        GctlSetVisEnab(ckAn_scaden, False)
      End If

      If oCleClie.lLead <> 0 And (oCleClie.bModuloCRM Or oCleClie.bModuloAS) Then
        GctlSetVisEnab(tlbApriLead, False)
      Else
        tlbApriLead.Enabled = False
      End If

      '----------------------------
      'CRM / CS: serifico se l'utente può modificare i dati
      bAccmod = True
      If oCleClie.bModuloCRM And oCleClie.bIsCRMUser And oCleClie.strTipoConto = "C" Then
        Try
          If Not oMenu.CercaAccessiCrmDaLead(DittaCorrente, oCleClie.lLead, bAccmod) Then
            If oCleClie.bNew = False Then
              SetStato(0)
              oApp.MsgBoxErr(oApp.Tr(Me, 128366770952828001, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi questo Cliente."))
              Return
            End If
          End If
        Catch ex As Exception
          oApp.MsgBoxErr(ex.Message)
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
        End Try

        If bAccmod = False And oCleClie.bNew = False Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128366770952828000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla modifica/cancellazione dei dati relativi questo Cliente."))
          tlbSalva.Enabled = False
          tlbCancella.Enabled = False
          tlbOle.Enabled = False
          tlbQualità.Enabled = False
        Else
          GctlSetVisEnab(tlbSalva, False)
          GctlSetVisEnab(tlbCancella, False)
          GctlSetVisEnab(tlbOle, False)
          GctlSetVisEnab(tlbQualità, False)
        End If

      Else
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbOle, False)
        GctlSetVisEnab(tlbQualità, False)
      End If    'If oCleClie.bModuloCRM And oCleClie.bIsCRMUser And oCleClie.strTipoConto = "C" Then


      ckDestcorr.Checked = False
      ckDestdomf.Checked = False
      ckDestresan.Checked = False
      ckDestsedel.Checked = False
      With dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)
        If NTSCInt(!an_destcorr) > 0 Then ckDestcorr.Checked = True
        If NTSCInt(!an_destdomf) > 0 Then ckDestdomf.Checked = True
        If NTSCInt(!an_destresan) > 0 Then ckDestresan.Checked = True
        If NTSCInt(!an_destsedel) > 0 Then ckDestsedel.Checked = True
      End With

      If oCleClie.bNew Then
        GctlSetVisEnab(ckAn_profes, False)
      Else
        If oCleClie.ProfessMovim Then
          ckAn_profes.Enabled = False
        Else
          GctlSetVisEnab(ckAn_profes, False)
        End If
      End If

      VisualizzaNoteconto()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


#Region "Eventi Form"
  Public Overridable Sub FRM__CLIE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      If Me.Modal = True Then Me.MinimizeBox = False

      If bPreload Then Return

      InitControls()


      CaricaCombo()

      oCleClie.strTestSalvaCfPiva = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "TestSalvaCfPiva", "N", " ", "N").ToString
      oCleClie.bNonProporreSiglaRic = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "NonProporreSiglaRic", "0", " ", "0"))
      oCleClie.strGeneraIdPswClienti = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GeneraIdPswClienti", "0", " ", "0").ToString
      oCleClie.strAnagenDtIniz = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "AnagenDtIniz", "0", " ", "0")
      bGestAnaext = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0"))
      oCleClie.bGesttabcont = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "GestTabcont", "0", " ", "0"))
      oCleClie.strBloccaavvertifido = oMenu.GetSettingBus("BSCGDCST", "OPZIONI", ".", "Bl_cliente_sup_fido", " ", " ", " ")
      oCleClie.strBloccainsolu = oMenu.GetSettingBus("BSCGDCST", "OPZIONI", ".", "Bl_cliente_per_insol", " ", " ", " ")
      oCleClie.strBloccaRDScadute = oMenu.GetSettingBus("BSCGDCST", "OPZIONI", ".", "Bl_cliente_RD_scadute", "N", " ", "N")
      oCleClie.bRiapriSuSalva = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "RiapriSuSalva", "0", " ", "0"))
      oCleClie.nCodpagaInAddNew = CInt(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Pagamento_Nuovo_Cliente", "0", " ", "0"))
      oCleClie.nListinoInAddNew = CInt(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Listino_Nuovo_Cliente", "1", " ", "1"))
      oCleClie.strDefaultUserCrm = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "UtentePredefinitoCrm", "Admin", " ", "Admin")
      If oCleClie.nListinoInAddNew = 0 Then oCleClie.nListinoInAddNew = 1
      oCleClie.strPasswbl = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Passwbl", "nts", " ", "nts")
      bNoTestNewAnaext = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "NoTestNewAnaext", "0", " ", "0"))
      oCleClie.bGestAlert = CBool(Val(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Abilita_Alert", "0", " ", "0")))
      strVis_Note_Conto = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Vis_Note_Conto", "N", " ", "N")
      oCleClie.strVoceFinClie = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "VoceFinanziariaCliente", "", ".", "")
      oCleClie.strVoceFinForn = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "VoceFinanziariaForn", "", ".", "")
      '--------------------------------------------------------------------------------------------------------------
      oCleClie.bSelCodiceNoApri = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "SelCodiceNoApri", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      '--- Lettura dell'instid su TTDESTDIV di appoggio, per la stampa degli Indirizzi dalla modale relativa
      '--------------------------------------------------------------------------------------------------------------
      oCleClie.lIITtdestdiv = oMenu.GetTblInstId("TTDESTDIV", False)
      '--------------------------------------------------------------------------------------------------------------

      SetStato(0)                       'altrimenti quando riapro la form che era stata ridimensionata da errore !!!

      tlbForn.Down = False
      tlbCli.Down = True
      oCleClie.strTipoConto = "C"
      GctlTipoDoc = "C"
      ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
      ceSconti.TipoSconto = 0
      ceProvvig.TipoProvv = 1

      If oCleClie.ArticoliDeteriorabili() Then
        GctlSetVisible(lbDeteriorabili)
        GctlSetVisible(edAn_codpagadet)
        GctlSetVisible(lbXx_codpagadet)
        GctlSetVisible(edAn_codpagadet2)
        GctlSetVisible(lbXx_codpagadet2)
        GctlSetVisible(edAn_codpagadet3)
        GctlSetVisible(lbXx_codpagadet3)
      Else
        lbDeteriorabili.Visible = False
        edAn_codpagadet.Visible = False
        lbXx_codpagadet.Visible = False
        edAn_codpagadet2.Visible = False
        lbXx_codpagadet2.Visible = False
        edAn_codpagadet3.Visible = False
        lbXx_codpagadet3.Visible = False
        lbXx_codpag.Width += edAn_codpagadet.Width + lbXx_codpagadet.Width + 5
        lbXx_codpaga2.Width = lbXx_codpag.Width
        lbXx_codpaga3.Width = lbXx_codpag.Width
      End If

      '-------------------------------------------------
      GctlSetRoules()
      GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma

      If oApp.oGvar.strSconClCliDaList = "S" Then
        'nasconto la classe di sconto cliente, visto che è stato impostato di prendere tale informazione dal listino
        lbAn_clascon.Visible = False
        edAn_clascon.Visible = False
        lbXx_clascon.Visible = False
      End If

      '''If CLN__STD.IsBis Then
      '''  tlbEmail.Visible = False
      '''  tlbWeb.Visible = False
      '''  tlbGoogleMaps.Visible = False
      '''End If

      If Not oCleClie.Apri(DittaCorrente, False, -1, "", dsClie) Then
        Me.Close()
        Return
      End If
      dcClie.DataSource = dsClie.Tables("ANAGRA")
      dsClie.AcceptChanges()
      Bindcontrols()

      SetStato(0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CLIE_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim bDestinazionePassata As Boolean = False
    Dim strTmp() As String = Nothing
    Dim dttPeca As New DataTable

    Try

      'Controllo che i campi delle destinazioni predefinite in Inizializzazioni comuni e globali siano diversi da 0
      If Not oCleClie.CheckInitGlobali() Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131038914186440394, "Attenzione!! Nelle 'Inizializzazioni comuni/globali' le destinazioni predefinite delle anagrafiche non sono state valorizzate."))
        Me.Close()
      End If

      If Not LeggiDatiDitta() Then
        Me.Close()
        Return
      End If

      If (oCleClie.bModuloCRM OrElse oCleClie.bModuloAS) AndAlso Not oCleClie.VerificaPresenzaLeads() Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130905931866407249, "Si è attivato il modulo CRM o Customer Service ma non sono ancora stati creati i Leads." & vbCrLf & _
                                                       "Avviare il programma 'Crea Leads da Clienti\Destinazioni' (V-F) per proseguire."))
        Me.Close()
        Return
      End If

      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCCC) Then tlbContratti.Visible = False

      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        oMenu.ValCodiceDb("1", DittaCorrente, "TABPECA", "N", , dttPeca)
        If dttPeca.Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129319677734956014, "Tabella delle Personalizzazioni CA-DC (Globale) non configurata. Imposibile continuare"))
          Me.Close()
          Return
        End If
        oCleClie.bCampiCAEAttivi = CBool(IIf(NTSCStr(dttPeca.Rows(0)!tb_richcli) = "S", True, False))
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
      Else
        ckAn_webvis.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          oCleClie.LeggiDatiDitta(DittaCorrente, bGestAnaext)
          '------------------------------------------------------------------------------------------------------------
          oCleClie.lCoddestDaGestioneZoom = 0
          strTmp = oCallParams.strParam.Split(";"c)
          Try
            If strTmp(3) <> "" Then bDestinazionePassata = True
          Catch ex As Exception
          End Try
          If bDestinazionePassata = True Then
            oCallParams.strParam = strTmp(0) & ";" & strTmp(1) & ";" & strTmp(2)
            oCleClie.lCoddestDaGestioneZoom = NTSCInt(strTmp(3))
          End If
          '------------------------------------------------------------------------------------------------------------
          If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
            If tlbNuovo.Enabled And tlbNuovo.Visible Then
              'Se 'Nuovo' non è abilitato, non permetto di creare una nuova anagrafica cliente/fornitore
              tlbNuovo_ItemClick(Me, Nothing)
            End If
          ElseIf NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 8)) <> 0 Then
            dttPeca.Clear()
            dttPeca.Dispose()
            If oMenu.ValCodiceDb(Microsoft.VisualBasic.Mid(oCallParams.strParam, 8), DittaCorrente, "ANAGRA", "N", "", dttPeca) = True Then
              If NTSCStr(dttPeca.Rows(0)!an_tipo) = "F" Then oCallParams.strParam = oCallParams.strParam.Replace("C", "F")
            End If
            dttPeca.Clear()
            dttPeca.Dispose()
            tlbApri_ItemClick(Nothing, Nothing)
          End If
        ElseIf oCallParams.strPar1 <> "" Then
          If oCallParams.strPar4 = "F" Then
            GctlTipoDoc = "F"
            oCleClie.strTipoConto = "F"
            tlbCli.Down = False
            tlbForn.Down = True
          Else
            GctlTipoDoc = "C"
            oCleClie.strTipoConto = "C"
            tlbCli.Down = True
            tlbForn.Down = False
          End If
          oCleClie.strOrderBy = oCallParams.strPar2
          ApriSelezioneCliente()
        ElseIf oCallParams.ctlPar1 IsNot Nothing Then
          dtrOrganig = CType(oCallParams.ctlPar1, DataRow)
          bChiudiAlSalvataggio = True
        End If
      End If    'If Not oCallParams Is Nothing Then

      oCallParamsOld = oCallParams
      oCallParams = Nothing 'in questo modo se vengo chiamato da bn_hlan, prima apro il cli scelto, poi se faccio ripristina posso scegliere un altro cliente/forn 
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.lCoddestDaGestioneZoom <> 0 Then
        Dim dttInsg As New DataTable
        If oMenu.ValCodiceDb("1", DittaCorrente, "TABINSG", "N", "", dttInsg) = True Then
          Select Case oCleClie.lCoddestDaGestioneZoom
            Case NTSCInt(dttInsg.Rows(0)!tb_destdomf) : cmdDestdomf_Click(Me, Nothing)
            Case NTSCInt(dttInsg.Rows(0)!tb_destsedel) : cmdDestsedel_Click(Me, Nothing)
            Case NTSCInt(dttInsg.Rows(0)!tb_destresan) : cmdDestresan_Click(Me, Nothing)
            Case NTSCInt(dttInsg.Rows(0)!tb_destcorr) : cmdDestcorr_Click(Me, Nothing)
            Case Else : cmdAltriIndir_Click(Me, Nothing)
          End Select
        Else
          cmdAltriIndir_Click(Me, Nothing)
        End If
        dttInsg.Clear()
        dttInsg.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CLIE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then
          e.Cancel = True
          Return
        End If
      End If
      oCleClie.Ripristina()
      SetStato(0)
      If tsClie.SelectedTabPage.Equals(tsClie.TabPages(7)) Then
        ceListini.Ripristina()
      ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(8)) Then
        ceSconti.Ripristina()
      ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(9)) Then
        ceProvvig.Ripristina()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__CLIE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTDESTDIV", False, oCleClie.lIITtdestdiv)
      '--------------------------------------------------------------------------------------------------------------
      dcClie.Dispose()
      dsClie.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
      End If

      If Not Apri(True, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
      End If

      If Not Apri(False, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApriUltimaRicerca_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriUltimaRicerca.ItemClick
    Dim oParam As New CLE__PATB
    Dim lContoTmp As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      If pnTop.Visible = True Then
        If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
          If Salva() = False Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edAn_descr1.Visible = True Then
        If Salva() = False Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleClie.bNuovoContoProposto = True
      '--------------------------------------------------------------------------------------------------------------
      oParam.strAlfpar = "BN__CLIE"
      oParam.bTipoProposto = False
      NTSZOOM.ZoomStrIn("ZOOMANAGRA" & oCleClie.strTipoConto.ToUpper, DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If oParam.strOut.Trim = "" Then Return
      lContoTmp = NTSCInt(oParam.strOut)
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBinding
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.Apri(DittaCorrente, False, lContoTmp, "", dsClie) = False Then
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      dcClie.DataSource = dsClie.Tables("ANAGRA")
      dsClie.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      dcClie.ResetBindings(False)
      dcClie.MoveFirst()

      NumeroPost()
      '--------------------------------------------------------------------------------------------------------------
      '--- Collego il BindingSource ai vari controlli 
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      SetStato(2)
      '--------------------------------------------------------------------------------------------------------------
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.strTipoConto = "C" Then
        GctlSetVisEnab(lbBarra, True)
        GctlSetVisEnab(cbAn_privato, True)
      Else
        lbBarra.Visible = False
        cbAn_privato.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Simulo il cambio di valore del combo così attiva i componenti corretti
      '--- (invece di lasciare quelli dell'ultimo cliente visto)
      '--------------------------------------------------------------------------------------------------------------
      cbAn_persfg_SelectedValueChanged(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      edAn_descr1.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Try
      If Not Apri(False, True) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Not oCleClie.RecordIsChanged Then
        If Not oCleClie.TestSalvaCfPiva() Then Return
      End If

      If Salva() Then
        'If oCleClie.bRiapriSuSalva = False Then tlbRipristina_ItemClick(tlbRipristina, Nothing)
        CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      If oCleClie.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128380294388916000, "Il conto non è mai stato salvato. Eventualmente ripristinare"))
        Return
      End If

      '-------------------------------------------------
      'cancello
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128271029890038656, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          Me.Cursor = Cursors.WaitCursor
          If Not oCleClie.Salva(True) Then Return
          tlbRipristina_ItemClick(Nothing, Nothing)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcClie, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'ripristino la forma di pagamento
      If oCleClie.bHasChanges = False Then
        oCleClie.Ripristina()
        SetStato(0)
        If tsClie.SelectedTabPage.Equals(tsClie.TabPages(7)) Then
          ceListini.Ripristina()
        ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(8)) Then
          ceSconti.Ripristina()
        ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(9)) Then
          ceProvvig.Ripristina()
        End If
        Return
      End If

      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128271029890194656, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleClie.Ripristina()

          If tsClie.SelectedTabPage.Equals(tsClie.TabPages(7)) Then
            ceListini.Ripristina()
          ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(8)) Then
            ceSconti.Ripristina()
          ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(9)) Then
            ceProvvig.Ripristina()
          End If

          SetStato(0)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcClie, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB
    Dim ds As New DataSet
    Dim dttDich As New DataTable
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return

      If edan_destin.ContainsFocus Or edan_destpag.ContainsFocus Then
        'non posso fare lo zoom standard, visto che potrei selez. una destinaz. diversa appena inserita e non ancora salvata ...
        'creo un dataset contenente tutte le destinazioni diverse che ho in memoria
        ds.Tables.Add(dsClie.Tables("DESTDIV").Clone)
        ds.Tables(0).TableName = "DESTDIV"
        For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
          ds.Tables(0).ImportRow(dsClie.Tables("DESTDIV").Rows(i))
        Next
        ds.Tables(0).AcceptChanges()
        NTSZOOM.strIn = IIf(edAn_destin.ContainsFocus, edAn_destin.Text, edAn_destpag.Text).ToString
        oParam.lContoCF = 1   'passo il conto cliente/fornitore solo per dire che non è zoom su anazul
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> IIf(edAn_destin.ContainsFocus, edAn_destin.Text, edAn_destpag.Text).ToString Then
          If edAn_destin.ContainsFocus Then
            edAn_destin.Text = NTSZOOM.strIn
          Else
            edAn_destpag.Text = NTSZOOM.strIn
          End If
        End If

      ElseIf edAn_citta.ContainsFocus Or edAn_cap.ContainsFocus Then
        '------------------------------------
        'zoom comune
        NTSZOOM.strIn = NTSCStr(edAn_codcomu.Text)
        NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_codcomu.Text Then edAn_codcomu.Text = NTSZOOM.strIn
        edAn_codcomu.Focus()

      ElseIf edAn_cab.ContainsFocus Then
        '------------------------------------
        'zoom cab
        SetFastZoom(edAn_cab.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = edAn_abi.Text   'passo il codice abi
        NTSZOOM.strIn = edAn_cab.Text
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_cab.Text Then edAn_cab.NTSTextDB = NTSZOOM.strIn

      ElseIf edAn_contfatt.ContainsFocus Then
        '------------------------------------
        'zoom conto fatturazione
        SetFastZoom(edAn_contfatt.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = edAn_contfatt.Text   'passo il codice cliente
        NTSZOOM.strIn = edAn_contfatt.Text
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA" & oCleClie.strTipoConto.ToUpper, DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_contfatt.Text Then edAn_contfatt.NTSTextDB = NTSZOOM.strIn

      ElseIf edAn_kpccee.Focused Then
        '------------------------------------
        'zoom riclass CEE dare
        SetFastZoom(edAn_kpccee.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_kpccee.Text)
        oParam.strTipoBil = "C"     'per CEE, R per RICLASSIFICATO
        oParam.nCodTipoRicl = 0
        oParam.strTipoSezione = "A"
        NTSZOOM.ZoomStrIn("ZOOMHLCE", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_kpccee.Text Then edAn_kpccee.Text = NTSZOOM.strIn

      ElseIf edAn_kpccee2.Focused Then
        '------------------------------------
        'zoom riclass CEE avere
        SetFastZoom(edAn_kpccee2.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_kpccee2.Text)
        oParam.strTipoBil = "C"     'per CEE, R per RICLASSIFICATO
        oParam.nCodTipoRicl = 0
        oParam.strTipoSezione = "P"
        NTSZOOM.ZoomStrIn("ZOOMHLCE", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_kpccee2.Text Then edAn_kpccee2.Text = NTSZOOM.strIn

      ElseIf edAn_rifricd.Focused Then
        '------------------------------------
        'zoom riclass dare
        SetFastZoom(edAn_rifricd.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_rifricd.Text)
        oParam.strTipoBil = "R"     'per CEE, R per RICLASSIFICATO
        oParam.nCodTipoRicl = 0
        oParam.strTipoSezione = "A"
        NTSZOOM.ZoomStrIn("ZOOMHLCE", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_rifricd.Text Then edAn_rifricd.Text = NTSZOOM.strIn

      ElseIf edAn_rifrica.Focused Then
        '------------------------------------
        'zoom riclass avere
        SetFastZoom(edAn_rifrica.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_rifrica.Text)
        oParam.strTipoBil = "R"     'per CEE, R per RICLASSIFICATO
        oParam.nCodTipoRicl = 0
        oParam.strTipoSezione = "P"
        NTSZOOM.ZoomStrIn("ZOOMHLCE", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_rifrica.Text Then edAn_rifrica.Text = NTSZOOM.strIn

      ElseIf ceListini.grList.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceListini.tlbListZoom_Click(ceListini.tlbListZoom, Nothing)

      ElseIf ceSconti.grSconti.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceSconti.tlbScontiZoom_Click(ceSconti.tlbScontiZoom, Nothing)

      ElseIf ceProvvig.grProv.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceProvvig.tlbProvZoom_Click(ceProvvig.tlbProvZoom, Nothing)
      ElseIf edAn_codtcdc.Focused Then
        SetFastZoom(edAn_codtcdc.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_codtcdc.Text)
        oParam.strTipo = "K"
        NTSZOOM.ZoomStrIn("ZOOMTABTCDC", "", oParam)
        If NTSZOOM.strIn <> NTSCStr(edAn_codtcdc.Text) Then edAn_codtcdc.Text = NTSZOOM.strIn
      ElseIf edAn_coddica.Focused Then
        SetFastZoom(edAn_coddica.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAn_coddica.Text)
        oParam.strTipo = "K"
        NTSZOOM.ZoomStrIn("ZOOMTABDICA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(edAn_coddica.Text) Then edAn_coddica.Text = NTSZOOM.strIn
      ElseIf edAn_coddicv.Focused Then
        If edAn_coddica.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129309316650792127, "Prima specificare il codice di Aggregazione Budget"))
          Return
        End If

        SetFastZoom(edAn_coddicv.Text, oParam)
        oParam.strCodice = edAn_coddica.Text
        NTSZOOM.strIn = edAn_coddicv.Text
        NTSZOOM.ZoomStrIn("ZOOMTABDICV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAn_coddicv.Text Then edAn_coddicv.Text = NTSZOOM.strIn
      ElseIf edAn_numdic.Focused Then
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupDII) Then
          SetFastZoom(edAn_numdic.Text, oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = "0"
          oParam.nAnnpar = 0
          oParam.lContoCF = NTSCInt(edAn_conto.Text)   'passo il conto cliente/fornitore
          oParam.strDatreg = DateTime.Now.ToShortDateString
          NTSZOOM.ZoomStrIn("ZOOMDICHINT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> "0" Or oParam.nAnnpar <> 0 Then
            oCleClie.LeggiDichint(oParam.nAnnpar, NTSCInt(NTSZOOM.strIn), dttDich)
            Select Case oCleClie.strTipoConto
              Case "C"
                edAn_numdic.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_clinum)
                edAn_datdic.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_clidata)
                edAn_numdicp.NTSTextDB = Strings.Right("0000" & NTSZOOM.strIn, 4) & "/" & Strings.Right(NTSCStr(oParam.nAnnpar), 2)
                edAn_datdicp.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_dataemis)
              Case "F"
                edAn_numdic.NTSTextDB = Strings.Right("0000" & NTSZOOM.strIn, 4) & "/" & Strings.Right(NTSCStr(oParam.nAnnpar), 2)
                edAn_datdic.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_dataemis)
            End Select
            edAn_scaddic.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_datascad)
            edAn_maxdic.NTSTextDB = NTSCStr(dttDich.Rows(0)!di_impmaxplaf)
          End If
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

      ctrlTmp.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      NTSFormClearDataBinding(Me)
      dcClie.MoveFirst()
      Bindcontrols()
      NumeroPost()
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      NTSFormClearDataBinding(Me)
      dcClie.MovePrevious()
      Bindcontrols()
      NumeroPost()
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      NTSFormClearDataBinding(Me)
      dcClie.MoveNext()
      Bindcontrols()
      NumeroPost()
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      NTSFormClearDataBinding(Me)
      dcClie.MoveLast()
      Bindcontrols()
      NumeroPost()
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Public Overridable Sub tlbWeb_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbWeb.ItemClick
    Dim strIndir As String = ""
    Try
      If edAn_website.Text.Trim.Length < 4 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128650711816250000, "Manca sito Web oppure nome sito non valido"))
        Return
      End If

      If edAn_website.Text.Substring(0, 4).ToLower = "http" Then
        strIndir = edAn_website.Text
      Else
        strIndir = "http://" & edAn_website.Text
      End If

      If CLN__STD.IsBis Then
        IS_ExecOnSbc("", strIndir)
      Else
        NTSProcessStart(strIndir, "")
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbMail_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMail.ItemClick
    Dim strIndir As String = ""
    Try
      If edAn_email.Text.Trim.Length < 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128650716759062500, "Manca indirizzo di e-mail oppure indirizzo  non valido"))
        Return
      End If

      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
        strIndir = "mailto:" & edAn_email.Text

        If CLN__STD.IsBis Then
          IS_ExecOnSbc("", strIndir)
        Else
          NTSProcessStart(strIndir, "")
        End If
      Else
        Dim oPar As New CLE__CLDP
        oPar.strPar3 = edAn_email.Text
        oPar.bPar1 = True
        oPar.bPar2 = True
        oPar.bPar3 = True
        oPar.bPar4 = True
        oPar.bPar5 = True

        oMenu.RunChild("NTSInformatica", "FRMEMWMAI", oApp.Tr(Me, 130154082081917624, "Invio e-mail"), DittaCorrente, "", "BNEMWMAI", oPar, "", False, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSkype_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSkype.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If NTSCInt(edAn_conto.Text) = 0 Then Return

      oPar.strPar1 = "C"
      oPar.dPar1 = NTSCInt(edAn_conto.Text)

      oMenu.RunChild("NTSInformatica", "FRM__SKYP", "", DittaCorrente, "", "BN__SKYP", oPar, "", False, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If tlbSalva.Enabled And tlbSalva.Visible Then
      If Not Salva() Then Return
    Else
      tlbRipristina_ItemClick(tlbRipristina, Nothing)
    End If
    Me.Close()
  End Sub


  Public Overridable Sub tlbEstensioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEstensioni.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      oPar.Ditta = DittaCorrente
      oPar.bAddNew = oCleClie.bNew
      oPar.strNomProg = "BN__CLIE"
      oPar.strPar1 = oCleClie.strTipoConto
      oPar.dPar1 = 0                                    'codice destinazione diversa
      oPar.ctlPar1 = dsClie.Tables("ANAEXT")
      oMenu.RunChild("NTSInformatica", "FRM__ANEX", "", DittaCorrente, "", "BN__ANEX", oPar, "", True, True)
      'restituisce dsClie.Tables("ANAEXT") aggiornato
      bEntratoInAnaext = True
      oCleClie.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbNote_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNote.ItemClick
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()

      strParam = "APRI;" & "000000000;" & NTSCInt(edAn_conto.Text).ToString.PadLeft(9, CChar("0")) & ";S;"
      oMenu.RunChild("BS__NOTE", "CLS__NOTE", "", DittaCorrente, "", "", Nothing, strParam, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdini.ItemClick
    Dim oPar As New CLE__PATB
    'Dim strParam As String = ""
    Try
      Me.ValidaLastControl()

      'strParam = NTSCInt(edAn_conto.Text).ToString.PadLeft(9, CChar("0")) & ";" & _
      '           NTSCInt(edAn_conto.Text).ToString.PadLeft(9, CChar("0")) & ";" & _
      '           "".PadLeft(18) & ";" & _
      '           "".PadLeft(18, "z"c) & ";" & _
      '           "§;" & _
      '           "1;" & _
      '           "01/01/1900;" & _
      '           "31/12/2099;"

      'oMenu.RunChild("BSORSCHO", "CLSORSCHO", "", DittaCorrente, "", "", Nothing, strParam, True, True)

      oPar.strDescr = oCleClie.GetWhereHlmo(NTSCInt(edAn_conto.Text))
      oPar.strTipo = " "
      oPar.lContoCF = NTSCInt(edAn_conto.Text)
      oPar.strCodart = ""
      oPar.nFase = 0
      oPar.dImporto = 0
      oPar.nTipologia = 0                     '0 solo visualizzaz, 2 = possibilità di selez le righe
      oPar.oParam = Nothing                   'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      oPar.nMastro = 1                        'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
      NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbMovimenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMovimenti.ItemClick
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()

      strParam = "APRI;" & New String(" "c, CodartMaxLen) & ";0000;" & NTSCInt(edAn_conto.Text).ToString.PadLeft(9, CChar("0")) & ";C"
      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", "", DittaCorrente, "", "", Nothing, strParam, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbApriLead_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriLead.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      Me.ValidaLastControl()

      If oCleClie.lLead = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128381303586594000, "Cliente/fornitore non collegato a nessun lead"))
        Return
      End If
      oPar.strParam = oCleClie.lLead.ToString.PadLeft(9, CChar("0"))
      oMenu.RunChild("NTSInformatica", "FRMCRLEAD", oApp.Tr(Me, 128986912997271979, "Gestione leads"), DittaCorrente, "", "BNCRLEAD", oPar, "", True, True)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbQualità_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbQualità.ItemClick
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()

      strParam = "APRI;" & NTSCInt(edAn_conto.Text).ToString & ";0;;0;0;0;0;" & DateTime.Now.ToShortDateString & ";A;"
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModSQ) Then
        oMenu.RunChild("NTSInformatica", "FRMSQCOQU", oApp.Tr(Me, 128580982589945330, "Controlli di Qualità"), DittaCorrente, "", "BNSQCOQU", Nothing, strParam, True, True)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128381290795188000, "Modulo Sistema Qualità non abilitato." & vbCrLf & "Impossibile continuare."))
        Exit Sub
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbOle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOle.ItemClick
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()

      If oCleClie.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130558728358781116, "Salvare il nuovo cliente/fornitore prima di passare alla gestione degli oggetti OLE."))
        Return
      End If
      If oCleClie.bHasChanges Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130558728663655120, "Salvare le modifiche apportate al cliente/fornitore prima di passare alla gestione degli oggetti OLE."))
        Return
      End If

      strParam = "APRI§C§0§" & edAn_conto.Text
      oMenu.RunChild("BS__AOLE", "CLS__AOLE", "", DittaCorrente, "", "", Nothing, strParam, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbOrganizzazione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrganizzazione.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      'Dim frmOrga As FRM__ORGA = nothing
      'frmOrga = CType(NTSNewFormModal("FRM__ORGA"), FRM__ORGA)
      'frmOrga.Init(oMenu, Nothing, DittaCorrente)
      'frmOrga.InitEntity(oCleClie)
      'frmOrga.ShowDialog()

      'oCallParams.Ditta = ditta di be__clie/be__anaz in analisi
      'oCallParams.strPar1 = strTipoConto (C= cliente, F= fornitore, '' = tabanaz)
      'oCallParams.ctlPar1 = dsShared di be__clie  o be__anaz (l'importante è che dentro ci sia la tabella ORGANIG
      'oCallParams.ctlPar2 = dttOrganigDeleted di be__clie
      'oCallParams.dPar1 = conto di be__clie in analisi
      'oCallParams.dPar2 = lead di be__clie in analisi

      oPar.Ditta = DittaCorrente
      oPar.strPar1 = oCleClie.strTipoConto        '(C= cliente, F= fornitore, '' = tabanaz)
      oPar.ctlPar1 = oCleClie.dsShared            'dsShared di be__clie  o be__anaz (l'importante è che dentro ci sia la tabella ORGANIG
      oPar.ctlPar2 = oCleClie.dttOrganigDeleted   'dttOrganigDeleted di be__clie
      oPar.dPar1 = NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_conto)   'conto di be__clie in analisi
      oPar.dPar2 = oCleClie.lLead                 'lead di be__clie in analisi
      oPar.strNomProg = "BN__CLIE"          'in alternativa BN__ANAZ'
      oMenu.RunChild("NTSInformatica", "FRM__ORGA", "", DittaCorrente, "", "BN__ORGA", oPar, "", True, True)

      oCleClie.bHasChanges = True
      oCleClie.bRiscriviOrganig = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbAltreBanche_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAltreBanche.ItemClick
    Dim frmBanc As FRM__BANC = Nothing
    Try
      Me.ValidaLastControl()

      frmBanc = CType(NTSNewFormModal("FRM__BANC"), FRM__BANC)
      frmBanc.Init(oMenu, Nothing, DittaCorrente)
      frmBanc.InitEntity(oCleClie)
      frmBanc.ShowDialog()
      frmBanc.Dispose()
      frmBanc = Nothing

      oCleClie.bHasChanges = True
      oCleClie.bRiscriviClibanc = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmBanc Is Nothing Then frmBanc.Dispose()
      frmBanc = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSottotipiPagamento_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSottotipiPagamento.ItemClick
    Dim frmStpg As FRM__STPG = Nothing
    Try
      Me.ValidaLastControl()

      frmStpg = CType(NTSNewFormModal("FRM__STPG"), FRM__STPG)
      frmStpg.Init(oMenu, Nothing, DittaCorrente)
      frmStpg.InitEntity(oCleClie)
      frmStpg.ShowDialog()
      frmStpg.Dispose()
      frmStpg = Nothing

      oCleClie.bHasChanges = True
      oCleClie.bRiscriviClistpg = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmStpg Is Nothing Then frmStpg.Dispose()
      frmStpg = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbTipoBf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTipoBf.ItemClick
    Dim frmTipb As FRM__TIPB = Nothing
    Try
      Me.ValidaLastControl()

      frmTipb = CType(NTSNewFormModal("FRM__TIPB"), FRM__TIPB)
      frmTipb.Init(oMenu, Nothing, DittaCorrente)
      frmTipb.InitEntity(oCleClie)
      frmTipb.ShowDialog()
      frmTipb.Dispose()
      frmTipb = Nothing

      oCleClie.bHasChanges = True
      oCleClie.bRiscriviClitipb = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmTipb Is Nothing Then frmTipb.Dispose()
      frmTipb = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCalcolaCodFisc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCalcolaCodFisc.ItemClick
    Try
      Me.ValidaLastControl()

      edAn_codfis.NTSTextDB = oMenu.CalcolaCodFiscEx(edAn_cognome.Text, edAn_nome.Text, cbAn_sesso.SelectedValue, _
                                                  edAn_datnasc.Text, edAn_codcomn.Text, lbXx_codcomn.Text, _
                                                  edAn_pronasc.Text, edAn_stanasc.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRitornaCodFisc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRitornaCodFisc.ItemClick
    Dim i As Integer = 0
    Dim strCodcomu As String = ""
    Dim strComune As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strSesso As String = ""
    Dim strDatnasc As String = ""
    Dim strCF As String = edAn_codfis.Text.ToUpper
    Try
      Me.ValidaLastControl()

      If oMenu.GetDatiFromCodFiscEx(strCF, strCodcomu, strComune, _
                                    strProv, strStato, strSesso, strDatnasc) Then
        edAn_codcomn.NTSTextDB = strCodcomu
        lbXx_codcomn.Text = strComune
        edAn_citnasc.NTSTextDB = strComune
        edAn_pronasc.NTSTextDB = strProv
        edAn_stanasc.NTSTextDB = strStato
        edAn_datnasc.NTSTextDB = strDatnasc
        cbAn_sesso.SelectedValue = strSesso
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_sesso = strSesso
      End If
      edAn_codfis.Text = strCF
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      If Not LeggiDatiDitta() Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbCli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCli.ItemClick
    Try
      If tlbCli.Down Then
        GctlSaveConfigGrid()             'salvo l'impostazione della griglia listini
        tlbForn.Down = False
        oCleClie.strTipoConto = "C"
        GctlTipoDoc = "C"
        GctlSetRoules()
        ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
        ceSconti.TipoSconto = 0
        ceProvvig.TipoProvv = 1
        GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
      Else
        If tlbForn.Down = False Then tlbCli.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbForn_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbForn.ItemClick
    Try
      If tlbForn.Down Then
        GctlSaveConfigGrid()             'salvo l'impostazione della griglia listini
        tlbCli.Down = False
        oCleClie.strTipoConto = "F"
        GctlTipoDoc = "F"
        GctlSetRoules()
        ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
        ceSconti.TipoSconto = 0
        ceProvvig.TipoProvv = 1
        GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
      Else
        If tlbCli.Down = False Then tlbForn.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub tlbGoogleMaps_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGoogleMaps.ItemClick
    Dim strQuery As String = ""
    Dim strTmp As String = ""
    Try
      Me.ValidaLastControl()

      If edAn_latitud.Text.Trim <> "" And edAn_longitud.Text.Trim <> "" Then
        strQuery = edAn_latitud.Text.Trim.Replace(",", ".") & "," & edAn_longitud.Text.Trim.Replace(",", ".")
      Else
        If edAn_citta.Text.Trim <> "" Then ' edAn_indir.Text.Trim <> "" And edAn_prov.Text.Trim <> "" And  
          strQuery = edAn_indir.Text.Replace(" ", "%20") & ","
          strQuery &= "%20" & edAn_citta.Text.Replace(" ", "%20") & ","
          strQuery &= "%20" & edAn_prov.Text.Replace(" ", "%20") & ","
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128907642952895815, "Specificare la città (oppure latitudine e longitudine) per poter procedere alla localizzazione"))
          Return
        End If
        If edAn_stato.Text = "" Then
          strQuery &= "%20Italia"
        Else
          If Not oMenu.ValCodiceDb(edAn_stato.Text, DittaCorrente, "TABSTAT", "S", strTmp) Then
            strQuery &= "%20Italia"
          Else
            strQuery &= "%20" & strTmp
          End If
        End If
      End If

      If CLN__STD.IsBis Then
        IS_ExecOnSbc("", "http://maps.google.it/maps?q=" & strQuery)
      Else
        NTSProcessStart("http://maps.google.it/maps?q=" & strQuery, "")
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbEmail_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEmail.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      oPar.strPar1 = edAn_conto.Text
      oPar.strPar3 = "Conto"

      oMenu.RunChild("NTSInformatica", "FRMEMCMAI", oApp.Tr(Me, 129048434199124914, "Console e-mail"), DittaCorrente, "", "BNEMCMAI", oPar)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGestioneSconti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGestioneSconti.ItemClick
    Dim strParam As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edAn_clascon.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129119245170997345, "Attenzione!" & vbCrLf & _
          "Indicare una classe di sconto valida prima di passare a:" & vbCrLf & _
          "Gestione Sconti per Classe conto/Classe Articolo."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      strParam = NTSCInt(edAn_clascon.Text).ToString.PadLeft(4, CChar("0"))
      oMenu.RunChild("BSVECLSC", "CLSVECLSC", "", DittaCorrente, "", "", Nothing, strParam, True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbContratti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbContratti.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      oPar.dPar1 = NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_conto)
      oPar.bPar1 = True ' Sblocca la griglia promozioni

      oMenu.RunChild("NTSInformatica", "FRMREGTES", "", DittaCorrente, "", "BNREGTES", oPar, "", True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

#Region "CommandButtons"
  Public Overridable Sub cmdAltriIndir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAltriIndir.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestdomf And _
            NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestsedel And _
            NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestresan And _
            NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestcorr Then
          ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
          dsClie.Tables("DESTDIV").Rows(i).Delete()
        End If
      Next
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)

      Dim oCallParamsTmp As New CLE__CLDP
      'oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdAltriIndir.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, 0)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdDestdomf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDestdomf.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing

    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = oCleClie.lDestdomf Then
          ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
          dsClie.Tables("DESTDIV").Rows(i).Delete()
        End If
      Next
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      Dim oCallParamsTmp As New CLE__CLDP
      'oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdDestdomf.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, oCleClie.lDestdomf)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      If ds.Tables("DESTDIV").Rows.Count > 0 Then
        ckDestdomf.Checked = True
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destdomf = oCleClie.lDestdomf
      Else
        ckDestdomf.Checked = False
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destdomf = 0
      End If
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdDestsedel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDestsedel.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = oCleClie.lDestsedel Then
          ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
          dsClie.Tables("DESTDIV").Rows(i).Delete()
        End If
      Next
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      Dim oCallParamsTmp As New CLE__CLDP
      'oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdDestsedel.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, oCleClie.lDestsedel)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      If ds.Tables("DESTDIV").Rows.Count > 0 Then
        ckDestsedel.Checked = True
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destsedel = oCleClie.lDestsedel
      Else
        ckDestsedel.Checked = False
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destsedel = 0
      End If
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdDestresan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDestresan.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = oCleClie.lDestresan Then
          ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
          dsClie.Tables("DESTDIV").Rows(i).Delete()
        End If
      Next
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      Dim oCallParamsTmp As New CLE__CLDP
      'oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdDestresan.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, oCleClie.lDestresan)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      If ds.Tables("DESTDIV").Rows.Count > 0 Then
        ckDestresan.Checked = True
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destresan = oCleClie.lDestresan
      Else
        ckDestresan.Checked = False
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destresan = 0
      End If
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdDestcorr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDestcorr.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = oCleClie.lDestcorr Then
          ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
          dsClie.Tables("DESTDIV").Rows(i).Delete()
        End If
      Next
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      Dim oCallParamsTmp As New CLE__CLDP
      'oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdDestcorr.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, oCleClie.lDestcorr)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      If ds.Tables("DESTDIV").Rows.Count > 0 Then
        ckDestcorr.Checked = True
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destcorr = oCleClie.lDestcorr
      Else
        ckDestcorr.Checked = False
        dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_destcorr = 0
      End If
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdPartitario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPartitario.Click
    Dim oParam As New CLE__PATB
    Try
      If Not Salva() Then Return

      oParam.lContoCF = NTSCInt(edAn_conto.Text)
      NTSZOOM.strIn = ""
      oParam.nEscomp = NTSCInt(oCleClie.dttAnaz.Rows(0)!tb_escomp)
      oParam.lNumpar = 0
      oParam.strAlfpar = ""
      oParam.nAnnpar = 0
      oParam.dImporto = 0
      oParam.dImportoval = 0
      oParam.strDatreg = ""
      oParam.lNumreg = 0
      oParam.nValuta = 0
      oParam.strIntegr = "N"
      oParam.bStanziamenti = False
      NTSZOOM.ZoomStrIn("ZOOMPARTITARI", DittaCorrente, oParam)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdRiclassificazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRiclassificazioni.Click
    'non posso inserire/modfiicare la griglia: 
    'i valori vengono gestiti da tabmast in modo che la modifica su tutti i cli/forn sia fatta da un porto solo 
    Dim oPar As New CLE__CLDP
    Try

      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If

      oPar.Ditta = DittaCorrente            'serve solo per BN__CLIE (anatric)
      oPar.strPar1 = ""        'serve solo per BN__SOTC  (anptric)
      oPar.dPar1 = NTSCInt(edAn_conto.Text)
      oPar.dPar2 = 0 'se <> 0 e programma BN__SOTC in griglia non si puoò fare insert e delete
      oPar.strNomProg = "BN__CLIE"          'in alternativa BN__CLIE'

      oMenu.RunChild("NTSInformatica", "FRMCGTRIC", "", DittaCorrente, "", "BNCGTRIC", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSimula_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSimula.ItemClick
    Dim frmSimu As FRM__SIMU = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      frmSimu = CType(NTSNewFormModal("FRM__SIMU"), FRM__SIMU)
      frmSimu.Init(oMenu, Nothing, DittaCorrente)
      frmSimu.oCleClie = oCleClie
      '--------------------------------------------------------------------------------------------------------------
      frmSimu.lConto = NTSCInt(edAn_conto.Text)
      frmSimu.strDescr1 = edAn_descr1.Text
      frmSimu.strDescr2 = edAn_descr2.Text
      frmSimu.strIndir = edAn_indir.Text
      frmSimu.strCap = edAn_cap.Text
      frmSimu.strCitta = edAn_citta.Text
      frmSimu.strProv = edAn_prov.Text
      frmSimu.strStato = edAn_stato.Text
      frmSimu.nAnClascon = NTSCInt(edAn_clascon.Text)
      frmSimu.nListino = NTSCInt(edAn_listino.Text)
      '--------------------------------------------------------------------------------------------------------------
      frmSimu.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmSimu Is Nothing Then frmSimu.Dispose()
      frmSimu = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdConaiArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConaiArt.Click
    Dim frmAnca As FRM__ANCA = Nothing
    Try
      frmAnca = CType(NTSNewFormModal("FRM__ANCA"), FRM__ANCA)
      frmAnca.Init(oMenu, Nothing, DittaCorrente)
      frmAnca.InitEntity(oCleClie)
      frmAnca.ShowDialog()
      oCleClie.bHasChanges = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmAnca Is Nothing Then frmAnca.Dispose()
      frmAnca = Nothing
    End Try
  End Sub
#End Region

  Public Overridable Function LeggiDatiDitta() As Boolean
    Dim bDbMultiDitta As Boolean = False
    Try
Riprova:
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BN__CLIE", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, oCleClie.bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Return False

      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModSQ) Then
        GctlSetVisEnab(tlbQualità, True)
      Else
        tlbQualità.Visible = False
      End If

      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModOR) Or CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModORCL) Then
        GctlSetVisEnab(tlbOrdini, True)
      Else
        tlbOrdini.Visible = False
      End If

      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModVE) Or CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModMG) Then
        GctlSetVisEnab(tlbMovimenti, True)
      Else
        tlbMovimenti.Visible = False
      End If

      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModVE) Or CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModMG) Or _
        CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModOR) Or CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModORCL) Then
        GctlSetVisEnab(tlbTipoBf, True)
      Else
        tlbTipoBf.Visible = False
      End If

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      oCleClie.InizializzaModuli
      If oCleClie.bModuloCRM Then
        GctlSetVisEnab(tlbApriLead, True)
        oCleClie.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleClie.bAmm, oCleClie.strAccvis, oCleClie.strAccmod, oCleClie.strRegvis, oCleClie.strRegmod)

        If oCleClie.bIsCRMUser Then
          oCleClie.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleClie.nCodcageoperat)
          If oCleClie.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return False
          End If
        End If
      Else
        If oCleClie.bModuloAS Then oCleClie.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleClie.nCodcageoperat)
        tlbApriLead.Visible = False
      End If    ' If bModuloCRM Then

      If oCleClie.bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      '-------------------------------------------------
      'leggo le informazioni relative alla ditta corrente
      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)
      If Not oCleClie.LeggiDatiDitta(DittaCorrente, bGestAnaext) Then
        If bDbMultiDitta Then
          GoTo Riprova
        Else
          Return False
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nStato As Integer)
    Try
      If nStato = 0 Then
        ceListini.LcConto = 0
        ceSconti.SoConto = 0
        ceProvvig.PerConto = 0

        '---------------------------------------
        'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
        'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
        'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
        'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida
        ceListini.Enabled = False
        ceSconti.Enabled = False
        ceProvvig.Enabled = False

        pnTop.Visible = False
        tsClie.Visible = False
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbApri, False)
        GctlSetVisEnab(tlbDuplica, False)
        tlbZoom.Enabled = False
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbCalcolaCodFisc.Enabled = False
        tlbRitornaCodFisc.Enabled = False
        tlbMovimenti.Enabled = False
        tlbOrdini.Enabled = False
        tlbNote.Enabled = False
        tlbOrganizzazione.Enabled = False
        tlbQualità.Enabled = False
        tlbEstensioni.Enabled = False
        tlbApriLead.Enabled = False
        tlbTipoBf.Enabled = False
        tlbTpbfPerDocu.Enabled = False
        If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupGPV) Then tlbTpbfPerDocu.Visible = False
        tlbAltreBanche.Enabled = False
        tlbSottotipiPagamento.Enabled = False
        GctlSetVisEnab(tlbCambioDitta, False)
        GctlSetVisEnab(tlbCli, False)
        GctlSetVisEnab(tlbForn, False)
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbOle.Enabled = False
        tlbWeb.Enabled = False
        tlbMail.Enabled = False
        tlbGoogleMaps.Enabled = False
        tlbGestioneSconti.Enabled = False
        If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
          tlbEmail.Visible = False
        End If
        tlbEmail.Enabled = False
        tlbContratti.Enabled = False
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbDuplica, False)
        tlbSkype.Enabled = False
        tlbSimula.Enabled = False

        'edFocus.Enabled = True
        'edFocus.Focus()
      Else
        tsClie.SelectedTabPageIndex = 0
        GctlSetVisEnab(pnTop, True)
        GctlSetVisEnab(tsClie, True)
        'tlbNuovo.Enabled = False
        'tlbApri.Enabled = False
        tlbDuplica.Enabled = False
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbSalva, False)
        If nStato > 1 Then GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbCalcolaCodFisc, False)
        GctlSetVisEnab(tlbRitornaCodFisc, False)
        GctlSetVisEnab(tlbMovimenti, False)
        GctlSetVisEnab(tlbOrdini, False)
        GctlSetVisEnab(tlbNote, False)
        GctlSetVisEnab(tlbOrganizzazione, False)
        GctlSetVisEnab(tlbQualità, False)
        GctlSetVisEnab(tlbEstensioni, False)
        GctlSetVisEnab(tlbTipoBf, False)
        GctlSetVisEnab(tlbAltreBanche, False)
        GctlSetVisEnab(tlbSottotipiPagamento, False)
        GctlSetVisEnab(tlbOle, False)
        GctlSetVisEnab(tlbWeb, False)
        GctlSetVisEnab(tlbMail, False)
        GctlSetVisEnab(tlbGoogleMaps, False)
        GctlSetVisEnab(tlbGestioneSconti, False)
        GctlSetVisEnab(tlbContratti, False)
        tlbCambioDitta.Enabled = False
        tlbCli.Enabled = False
        tlbForn.Enabled = False

        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
          GctlSetVisEnab(tlbEmail, False)
          GctlSetVisEnab(tlbEmail, True)
        End If

        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupGPV) Then
          GctlSetVisEnab(tlbTpbfPerDocu, False)
        Else
          tlbTpbfPerDocu.Visible = False
        End If

        If dsClie.Tables("ANAGRA").Rows.Count > 1 And nStato > 1 Then
          GctlSetVisEnab(tlbPrimo, False)
          GctlSetVisEnab(tlbPrecedente, False)
          GctlSetVisEnab(tlbSuccessivo, False)
          GctlSetVisEnab(tlbUltimo, False)
        End If
        If (oCleClie.bModuloCRM Or oCleClie.bModuloAS) And oCleClie.strTipoConto = "C" Then GctlSetVisEnab(tlbApriLead, False)

        If oCleClie.strTipoConto = "C" And oCleClie.bCampiCAEAttivi Then
          GctlSetVisEnab(fmCadc, True)
        Else
          fmCadc.Visible = False
        End If

        If oCleClie.strGestAnaext.IndexOf(oCleClie.strTipoConto) > -1 Then
          GctlSetVisEnab(tlbEstensioni, True)
          tlbEstensioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Else
          tlbEstensioni.Visible = False
          tlbEstensioni.ItemShortcut = Nothing
        End If
        GctlSetVisEnab(tlbSkype, False)
        GctlSetVisEnab(tlbSimula, False)
        'edFocus.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri(ByVal bNew As Boolean, ByVal bDuplica As Boolean) As Boolean
    Dim frmSean As FRM__SEAN = Nothing
    Dim frmNew As FRM__NUOV = Nothing
    Dim frmDuco As FRM__DUCO = Nothing
    Dim lContoTmp As Integer = 0
    Dim i As Integer = 0
    Dim strApriWhere As String = ""
    Dim strTipoContoOld As String = ""
    Try
      oCleClie.strOrderBy = ""

      If edAn_descr1.Visible Then
        If Not Salva() Then Return False
      End If

      oCleClie.bNuovoContoProposto = True
      If oCallParams IsNot Nothing AndAlso oCallParams.strParam <> "" Then
        'APRI/NUOVO DA GEST
        If Microsoft.VisualBasic.Mid(oCallParams.strParam, 6, 1) = "C" Then
          GctlTipoDoc = "C"
          oCleClie.strTipoConto = "C"
          tlbCli.Down = True
          tlbForn.Down = False
        Else
          GctlTipoDoc = "F"
          oCleClie.strTipoConto = "F"
          tlbCli.Down = False
          tlbForn.Down = True
        End If
        GctlSetRoules()
        ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
        ceSconti.TipoSconto = 0
        ceProvvig.TipoProvv = 1

        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          lContoTmp = 0
          GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
          GoTo PASSA
        Else
          lContoTmp = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 8))
        End If
      Else
PASSA:
        If bNew Then
          'NUOVO
          strApriWhere = ""
          lContoTmp = 0
          frmNew = CType(NTSNewFormModal("FRM__NUOV"), FRM__NUOV)
          frmNew.Init(oMenu, Nothing, DittaCorrente)
          frmNew.oCleClie = oCleClie
          frmNew.ShowDialog()
          If frmNew.bOk = False Then Return False 'annullato

          oCleClie.bNuovoContoProposto = frmNew.bNewCodProposto
          lContoTmp = (NTSCInt(frmNew.edMastro.Text) * oCleClie.lContoProgrMoltip) + NTSCInt(frmNew.edProgr.Text)
        Else
          'APRI
          frmSean = CType(NTSNewFormModal("FRM__SEAN"), FRM__SEAN)
          frmSean.Init(oMenu, Nothing, DittaCorrente)
          frmSean.edConto.Text = "0"
          frmSean.oCleClie = oCleClie
          If bDuplica Then frmSean.cmdSeleziona.Enabled = False
          frmSean.ShowDialog()
          If frmSean.bOk = False Then Return False 'annullato
          If frmSean.cbTipo.SelectedValue <> oCleClie.strTipoConto Then
            oCleClie.strTipoConto = frmSean.cbTipo.SelectedValue
            If oCleClie.strTipoConto = "C" Then
              GctlTipoDoc = "C"
              tlbCli.Down = True
              tlbForn.Down = False
            Else
              GctlTipoDoc = "F"
              tlbCli.Down = False
              tlbForn.Down = True
            End If
            GctlSetRoules()
            ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
            ceSconti.TipoSconto = 0
            ceProvvig.TipoProvv = 1
            GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
          End If
          oCleClie.bNuovoContoProposto = True
          lContoTmp = NTSCInt(frmSean.edConto.Text)
          strApriWhere = frmSean.strOut
        End If
      End If

      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      Me.Cursor = Cursors.WaitCursor
      If Not oCleClie.Apri(DittaCorrente, bNew, lContoTmp, strApriWhere, dsClie) Then
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        Return False
      End If

      If bNew Then
        'NUOVO DA LEAD
        oCleClie.lLead = NTSCInt(frmNew.edLead.Text)
        'If NTSCInt(frmNew.edLead.Text) <> 0 Then       'NUOVO DA LEAD
        'If NTSCInt(frmNew.edAnagen.Text) <> 0 Then     'NUOVO DA ANAGEN
        If Not oCleClie.NuovoAnagra(lContoTmp, NTSCInt(frmNew.edMastro.Text), NTSCInt(frmNew.edAnagen.Text), _
                                    oCleClie.lLead, frmNew.lbMastro.Text, dsClie.Tables("ANAGRA").Rows(0)) Then
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
          Return False
        End If
      End If

      dcClie.DataSource = dsClie.Tables("ANAGRA")
      dsClie.AcceptChanges()

      dcClie.ResetBindings(False)
      dcClie.MoveFirst()

      If bNew Then
        If dtrOrganig IsNot Nothing Then oCleClie.CopiaOrganizzazioneSuAnagra(dsClie.Tables("ANAGRA").Rows(0), dtrOrganig)
      Else
        NumeroPost()
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      If bNew Then
        SetStato(1)
      Else
        SetStato(2)
      End If

      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)

      '------------------------------------------------
      If bDuplica Then
        frmDuco = CType(NTSNewFormModal("FRM__DUCO"), FRM__DUCO)
        strApriWhere = ""
        lContoTmp = 0
        frmDuco.Init(oMenu, Nothing, DittaCorrente)
        frmDuco.oCleClie = oCleClie
        frmDuco.ShowDialog()
        If frmDuco.bOk = False Then
          'annullato
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
          Return False
        End If

        oCleClie.bNuovoContoProposto = frmDuco.bNewCodProposto
        lContoTmp = (NTSCInt(frmDuco.edMastro.Text) * oCleClie.lContoProgrMoltip) + NTSCInt(frmDuco.edProgr.Text)
        oCleClie.bNew = True
        SetStato(1)
        oCleClie.bRiscriviClibanc = True
        oCleClie.bRiscriviClitipb = True
        oCleClie.bRiscriviDestdiv = True
        oCleClie.bRiscriviOrganig = True
        oCleClie.bRiscriviCodarfo = True

        If frmDuco.cbTipo.SelectedValue <> oCleClie.strTipoConto Then
          strTipoContoOld = oCleClie.strTipoConto
          oCleClie.strTipoConto = frmDuco.cbTipo.SelectedValue
          If oCleClie.strTipoConto = "C" Then
            GctlTipoDoc = "C"
            tlbCli.Down = True
            tlbForn.Down = False
          Else
            GctlTipoDoc = "F"
            tlbCli.Down = False
            tlbForn.Down = True
          End If
          GctlSetRoules()
          ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
          ceSconti.TipoSconto = 0
          ceProvvig.TipoProvv = 1
          GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
        End If

        Me.Cursor = Cursors.WaitCursor
        If Not oCleClie.DuplicaConto(lContoTmp, NTSCInt(frmDuco.edMastro.Text), _
                                     frmDuco.ckAnagen.Checked, frmDuco.ckIndir.Checked, _
                                     frmDuco.ckListini.Checked, frmDuco.ckSconti.Checked, _
                                     frmDuco.ckProvvigioni.Checked, frmDuco.ckCodarfo.Checked, _
                                     strTipoContoOld, frmDuco.ckOrganig.Checked) Then
          'ripristino
          oCleClie.Ripristina()
          If tsClie.SelectedTabPage.Equals(tsClie.TabPages(7)) Then
            ceListini.Ripristina()
          ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(8)) Then
            ceSconti.Ripristina()
          ElseIf tsClie.SelectedTabPage.Equals(tsClie.TabPages(9)) Then
            ceProvvig.Ripristina()
          End If

          SetStato(0)

          Return False
        Else
          lbLead.Text = ""
          If Not frmDuco.ckAnagen.Checked Then lbAnagen.Text = ""
          If Not frmDuco.ckIndir.Checked Then
            ckDestcorr.Checked = False
            ckDestdomf.Checked = False
            ckDestresan.Checked = False
            ckDestsedel.Checked = False
          End If
          lbXx_codmast.Text = frmDuco.lbMastro.Text
        End If
        oApp.MsgBoxInfo(oApp.Tr(Me, 128473853444846662, "Duplicazione terminata"))
        CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)
      End If    'If bDuplica Then

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      If bNew And Not bDuplica Then
        oCleClie.bHasChanges = True
        Me.GctlApplicaDefaultValue() 'serve per far impostare i flag ed i combobox di listini/sconti/provvigioni alla prima apertura del programma
      End If

      If oCleClie.strTipoConto = "C" Then
        GctlSetVisEnab(lbBarra, True)
        GctlSetVisEnab(cbAn_privato, True)
      Else
        lbBarra.Visible = False
        cbAn_privato.Visible = False
      End If

      'Simulo il cambio di valore del combo così attiva i componenti corretti (invece di lasciare quelli dell'ultimo cliente visto)
      cbAn_persfg_SelectedValueChanged(Me, Nothing)

      edAn_descr1.Focus()

      ColoraCampoAbiCab()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSean Is Nothing Then frmSean.Dispose()
      If Not frmNew Is Nothing Then frmNew.Dispose()
      If Not frmDuco Is Nothing Then frmDuco.Dispose()
      frmSean = Nothing
      frmNew = Nothing
      frmDuco = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function ApriSelezioneCliente() As Boolean
    Try
      'leggo dal database i dati e collego il NTSBinding
      Me.Cursor = Cursors.WaitCursor
      If Not oCleClie.Apri(DittaCorrente, False, 0, oCallParams.strPar1, dsClie) Then
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        Return False
      End If

      dcClie = New BindingSource
      dcClie.DataSource = dsClie.Tables("ANAGRA")
      dsClie.AcceptChanges()

      dcClie.ResetBindings(False)
      dcClie.MoveFirst()

      If Not oCallParams Is Nothing AndAlso oCallParams.strPar3 <> "" Then
        'Cerca di posizionarsi sullo stesso articolo che è stato passato
        For z As Integer = 0 To dsClie.Tables("ANAGRA").Rows.Count - 1
          If NTSCStr(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_conto) = oCallParams.strPar3 Then Exit For
          dcClie.MoveNext()
        Next
      End If

      NumeroPost()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      SetStato(2)
      
      CaricaColonneUnbound(CType(dcClie.Current, DataRowView).Row)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      If tsClie.Visible = False Then Return True

      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleClie.bNew And bEntratoInAnaext = False And oCleClie.strGestAnaext.IndexOf(oCleClie.strTipoConto) > -1 And bNoTestNewAnaext = False Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128382297081336815, "Si sta salvando un cliente/fornitore senza essere entrati nelle estensioni anagrafiche: Aprire la form delle estensioni?")) = Windows.Forms.DialogResult.Yes Then
          tlbEstensioni_ItemClick(tlbEstensioni, Nothing)
        End If
      End If

      If oCleClie.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128271029890350656, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Me.Cursor = Cursors.WaitCursor
          If Not oCleClie.Salva(False) Then Return False
          'memorizzo il nuovo conto negli appunti, così se sono stato chiamato da ALT+F2 posso acquisire in automatico il nuovo conto
          oApp.NTSClipboard = "NTSNEW:ANAGRA:" & NTSCInt(edAn_conto.Text).ToString
          oCleClie.bNew = False
          SetStato(2)
          'Chiude il programma e ritorna il conto al programma chiamante (se presente)
          If bChiudiAlSalvataggio Then
            If oCallParamsOld IsNot Nothing Then oCallParamsOld.dPar1 = edAn_conto.TextInt
            Me.Close()
          End If
        End If
      End If

      '-------------------------------------------------
      'salvo i listini/sconti/provvigioni
      If Not ceListini.Salva Then Return False
      If Not ceSconti.Salva Then Return False
      If Not ceProvvig.Salva Then Return False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub cbAn_persfg_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAn_persfg.SelectedValueChanged
    Try

      If cbAn_persfg.SelectedValue.ToString = "G" Then
        edAn_cognome.Text = ""
        edAn_nome.Text = ""
        cbAn_sesso.SelectedValue = "S"

        edAn_cognome.Enabled = False
        edAn_nome.Enabled = False
        cbAn_sesso.Enabled = False
      Else
        If cbAn_sesso.SelectedValue = "S" Then cbAn_sesso.SelectedValue = "M"
        GctlSetVisEnab(edAn_cognome, False)
        GctlSetVisEnab(edAn_nome, False)
        GctlSetVisEnab(cbAn_sesso, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckAn_soggresi_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAn_soggresi.CheckStateChanged
    Try
      If ckAn_soggresi.Checked Then
        edAn_estcodiso.Text = ""
        edAn_estpariva.Text = ""
        edAn_codfisest.Text = ""
        edAn_nazion1.Text = ""
        lbXx_nazion1.Text = ""
        edAn_nazion2.Text = ""
        lbXx_nazion2.Text = ""
        edAn_estcodiso.Enabled = False
        edAn_estpariva.Enabled = False
        edAn_codfisest.Enabled = False
        edAn_nazion1.Enabled = False
        edAn_nazion2.Enabled = False
      Else
        GctlSetVisEnab(edAn_estcodiso, False)
        GctlSetVisEnab(edAn_estpariva, False)
        GctlSetVisEnab(edAn_codfisest, False)
        GctlSetVisEnab(edAn_nazion1, False)
        GctlSetVisEnab(edAn_nazion2, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tsClie_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsClie.SelectedPageChanged
    Dim i As Integer = 0
    Try
      '---------------------------------------
      'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
      'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
      'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
      'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida

      '-----------------------------
      'pagina dei listini
      If e.Page.Equals(tsClie.TabPages(7)) Then
        If oCleClie.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128466988465551088, "Salvare il nuovo cliente/fornitore prima di passare alla cartella LISTINI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If
        If oCleClie.bHasChanges Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129241810129854655, "Salvare le modifiche apportate al cliente/fornitore prima di passare alla cartella LISTINI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If

        If ceListini.LcConto <> NTSCInt(edAn_conto.Text) Then
          ceListini.LcConto = NTSCInt(edAn_conto.Text)
          ceListini.ApriListini()
        End If

        GctlSetVisEnab(ceListini, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente 
          ceListini.grvList.Enabled = False
          ceListini.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceListini.grvList, False)
          ceListini.tlbMain.Visible = True
        End If
      End If

      If e.PrevPage.Equals(tsClie.TabPages(7)) Then
        If Not ceListini.Salva Then
          tsClie.SelectedTabPageIndex = 7
        Else
          ceListini.Enabled = False
        End If
      End If



      '-----------------------------
      'pagina degli sconti
      If e.Page.Equals(tsClie.TabPages(8)) Then
        If oCleClie.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931444347256, "Salvare il nuovo cliente/fornitore prima di passare alla cartella SCONTI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If
        If oCleClie.bHasChanges Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129241810151573961, "Salvare le modifiche apportate al cliente/fornitore prima di passare alla cartella SCONTI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If

        If ceSconti.SoConto <> NTSCInt(edAn_conto.Text) Then
          ceSconti.SoConto = NTSCInt(edAn_conto.Text)
          i = ceSconti.TipoSconto
          ceSconti.TipoSconto = 0   'serve per far applicare le impostazioni di griglia e ricaricare gli sconti
          ceSconti.TipoSconto = i
        End If

        GctlSetVisEnab(ceSconti, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente
          ceSconti.grvSconti.Enabled = False
          ceSconti.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceSconti.grvSconti, False)
          ceSconti.tlbMain.Visible = True
        End If
      End If

      If e.PrevPage.Equals(tsClie.TabPages(8)) Then
        If Not ceSconti.Salva Then
          tsClie.SelectedTabPageIndex = 8
        Else
          ceSconti.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina delle provvigioni
      If e.Page.Equals(tsClie.TabPages(9)) Then
        If oCleClie.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931950723256, "Salvare il nuovo cliente/fornitore prima di passare alla cartella PROVVIGIONI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If
        If oCleClie.bHasChanges Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129241810167511869, "Salvare le modifiche apportate al cliente/fornitore prima di passare alla cartella PROVVIGIONI"))
          tsClie.SelectedTabPageIndex = 0
          Return
        End If

        If ceProvvig.PerConto <> NTSCInt(edAn_conto.Text) Then
          ceProvvig.PerConto = NTSCInt(edAn_conto.Text)
          i = ceProvvig.TipoProvv
          ceProvvig.TipoProvv = 0
          ceProvvig.TipoProvv = i   'serve per far applicare le impostazioni di griglia e ricaricare le provvigioni
        End If

        GctlSetVisEnab(ceProvvig, False)
        If oCleClie.strTipoConto = "C" And oCleClie.bModuloCRM = True And bAccmod = False Then
          'blocco se crm e cliente 
          ceProvvig.grvProv.Enabled = False
          ceProvvig.tlbMain.Visible = False
        Else
          GctlSetVisEnab(ceProvvig.grvProv, False)
          ceProvvig.tlbMain.Visible = True
        End If
      End If

      If e.PrevPage.Equals(tsClie.TabPages(9)) Then
        If Not ceProvvig.Salva Then
          tsClie.SelectedTabPageIndex = 9
        Else
          ceProvvig.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAn_note2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAn_note2.Enter
    Try
      Me.NTSDisableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAn_note2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAn_note2.Leave
    Try
      Me.NTSEnableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAn_dtaper_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAn_dtaper.Leave
    Try
      If edAn_dtaper.Text.Trim = "" Then edAn_dtaper.Text = Now.ToShortDateString
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  '--- Gestione tasti apri e nuovo zoom veloce destinazione
  Public Overridable Sub edAn_destpag_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAn_destpag.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If IsNumeric(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_destpag) Then
          nCodDestTmp = NTSCInt(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_destpag)
        End If
        oCallParamsTmp.strParam = "APRI;" & nCodDestTmp
      Else
        nCodDestTmp = 0
        oCallParamsTmp.strParam = "NUOV;0"
      End If

      oTmp.Text = NTSCStr(nCodDestTmp)

      oCallParamsTmp.strPar1 = "Altri indirizzi"
      nTipo = 0
      If nCodDestTmp > 0 Then
        For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
          If nCodDestTmp = NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) Then
            Select Case NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest)
              Case oCleClie.lDestdomf
                oCallParamsTmp.strPar1 = "Domicilio fiscale per provv. amministr."
                nTipo = oCleClie.lDestdomf
              Case oCleClie.lDestsedel
                oCallParamsTmp.strPar1 = "Resid./Domic. fisc./Sede legale in Italia"
                nTipo = oCleClie.lDestsedel
              Case oCleClie.lDestresan
                oCallParamsTmp.strPar1 = "Residenza/Sede legale estera"
                nTipo = oCleClie.lDestresan
              Case oCleClie.lDestcorr
                oCallParamsTmp.strPar1 = "Luogo di esercizio attiv. all'estero"
                nTipo = oCleClie.lDestcorr
            End Select
          End If
        Next
      End If

      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      Select Case nTipo
        Case oCleClie.lDestdomf, oCleClie.lDestsedel, oCleClie.lDestresan, oCleClie.lDestcorr
          For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = nTipo Then
              ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
              dsClie.Tables("DESTDIV").Rows(i).Delete()
            End If
          Next
        Case Else
          For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestdomf And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestsedel And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestresan And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestcorr Then
              ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
              dsClie.Tables("DESTDIV").Rows(i).Delete()
            End If
          Next
      End Select
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, nTipo)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
    End Try
  End Sub
  Public Overridable Sub edAn_destin_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAn_destin.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmDesg As FRM__DESG = Nothing
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If IsNumeric(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_destin) Then
          nCodDestTmp = NTSCInt(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_destin)
        End If
        oCallParamsTmp.strParam = "APRI;" & nCodDestTmp
      Else
        nCodDestTmp = 0
        oCallParamsTmp.strParam = "NUOV;0"
      End If

      oTmp.Text = NTSCStr(nCodDestTmp)

      oCallParamsTmp.strPar1 = "Altri indirizzi"
      nTipo = 0
      If nCodDestTmp > 0 Then
        For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
          If nCodDestTmp = NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) Then
            Select Case NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest)
              Case oCleClie.lDestdomf
                oCallParamsTmp.strPar1 = "Domicilio fiscale per provv. amministr."
                nTipo = oCleClie.lDestdomf
              Case oCleClie.lDestsedel
                oCallParamsTmp.strPar1 = "Resid./Domic. fisc./Sede legale in Italia"
                nTipo = oCleClie.lDestsedel
              Case oCleClie.lDestresan
                oCallParamsTmp.strPar1 = "Residenza/Sede legale estera"
                nTipo = oCleClie.lDestresan
              Case oCleClie.lDestcorr
                oCallParamsTmp.strPar1 = "Luogo di esercizio attiv. all'estero"
                nTipo = oCleClie.lDestcorr
            End Select
          End If
        Next
      End If

      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsClie.Tables("DESTDIV").Clone())
      Select Case nTipo
        Case oCleClie.lDestdomf, oCleClie.lDestsedel, oCleClie.lDestresan, oCleClie.lDestcorr
          For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) = nTipo Then
              ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
              dsClie.Tables("DESTDIV").Rows(i).Delete()
            End If
          Next
        Case Else
          For i = 0 To dsClie.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestdomf And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestsedel And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestresan And _
                NTSCInt(dsClie.Tables("DESTDIV").Rows(i)!dd_coddest) <> oCleClie.lDestcorr Then
              ds.Tables("DESTDIV").ImportRow(dsClie.Tables("DESTDIV").Rows(i))
              dsClie.Tables("DESTDIV").Rows(i).Delete()
            End If
          Next
      End Select
      dsClie.Tables("DESTDIV").AcceptChanges()

      frmDesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleClie, ds, nTipo)
      If NTSCInt(dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow)!an_codanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmDesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
        If ds.Tables("DESTDIV").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_coddest) > 0 Then
            dsClie.Tables("DESTDIV").ImportRow(ds.Tables("DESTDIV").Rows(i))
          Else
            ds.Tables("DESTDIV").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsClie.Tables("DESTDIV").AcceptChanges()
      oCleClie.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).RowState = DataRowState.Unchanged Then dsClie.Tables("ANAGRA").Rows(oCleClie.nCurRow).SetModified()
      oCleClie.bRiscriviDestdiv = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
    End Try
  End Sub
  '---

  Public Overridable Function VisualizzaNoteconto() As Boolean
    Try
      If oCleClie.bNew Then Return True
      If strVis_Note_Conto = "N" Then Return True
      If edAn_note2.Text.Trim = "" Then Return True
      Me.NTSBigBox(edAn_note2.Text.Trim, oApp.Tr(Me, 128686404046250000, "Note conto |" & edAn_conto.Text & "|"), 0, True, True, False)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub tlbTpbfPerDocu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTpbfPerDocu.ItemClick
    Dim frmTirk As FRM__TIRK = Nothing
    Try
      frmTirk = CType(NTSNewFormModal("FRM__TIRK"), FRM__TIRK)

      If Not frmTirk.Init(oMenu, Nothing, "") Then Return
      frmTirk.InitEntity(oCleClie)
      frmTirk.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmTirk Is Nothing Then frmTirk.Dispose()
      frmTirk = Nothing
    End Try
  End Sub


  Public Overridable Sub NumeroPost()
    Try
      ceColl.Visible = False 'Sempre invisibile, se è un utente social ci penserà lui ad apparire completato l'aggiornamento dei dati

      If oApp.User.SocialUser = "I" Then
        Dim oPar As New CLE__CLDP
        Dim dttDati As New DataTable
        dttDati.Columns.Add("codditt")
        dttDati.Columns.Add("spd_tipo")
        dttDati.Columns.Add("spd_conto")
        dttDati.Rows.Add(New Object() {DittaCorrente, "C", NTSCInt(dsClie.Tables("ANAGRA").Rows(dcClie.Position)!an_conto)})
        oPar.ctlPar1 = dttDati

        ceColl.NTSSetParam(oMenu, "", "BN__CLIE", oPar)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ceListini_VaiScontoCollegato(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      'mi posiziono sul tab 'sconti', imposto il combo su 'specifico cli/arti', 
      'setto la data validità, imposto la promozione e cerco di
      'posizionarmi sulla riga con sconto cli/arti con cli e arti = quello passato da bnxxlist
      strT = e.Message.Split("§"c)

      tsClie.SelectedTabPage = NtsTabPage9
      ceSconti.ckPromo.Checked = CBool(strT(2))
      ceSconti.opValDay.Checked = True
      ceSconti.edDtval.Text = strT(4)
      ceSconti.cbTiposconti.SelectedValue = "4"
      'mi devo posizionare sul record giusto
      For i = 0 To ceSconti.dcScon.List.Count - 1
        If CType(ceSconti.dcScon.Item(i), DataRowView)!so_conto.ToString = strT(0) And _
           CType(ceSconti.dcScon.Item(i), DataRowView)!so_codart.ToString = strT(1) And _
           NTSCDec(CType(ceSconti.dcScon.Item(i), DataRowView)!so_daquant) <= NTSCDec(strT(3)) And _
           NTSCDec(CType(ceSconti.dcScon.Item(i), DataRowView)!so_aquant) >= NTSCDec(strT(3)) Then
          ceSconti.dcScon.Position = i
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ceSconti_VaiListinoCollegato(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      'mi posiziono sul tab 'listini', imposto il combo su 'specifico cli/arti', 
      'setto la data validità, imposto la promozione, valuta fissa = 0 e cerco di
      'posizionarmi sulla riga con listino cli/arti con cli e arti = quello passato da bnxxscon
      strT = e.Message.Split("§"c)

      tsClie.SelectedTabPage = NtsTabPage8
      ceListini.ckPromo.Checked = CBool(strT(2))
      ceListini.opValDay.Checked = True
      ceListini.edDtval.Text = strT(4)
      ceListini.edCodvalu.Text = "0"
      ceListini.opValEur.Checked = True
      ceListini.cbTipolistino.SelectedValue = oCleClie.strTipoConto
      'mi devo posizionare sul record giusto
      For i = 0 To ceListini.dcList.List.Count - 1
        If CType(ceListini.dcList.Item(i), DataRowView)!lc_conto.ToString = strT(0) And _
           CType(ceListini.dcList.Item(i), DataRowView)!lc_codart.ToString = strT(1) And _
           NTSCDec(CType(ceListini.dcList.Item(i), DataRowView)!lc_daquant) <= NTSCDec(strT(3)) And _
           NTSCDec(CType(ceListini.dcList.Item(i), DataRowView)!lc_aquant) >= NTSCDec(strT(3)) Then
          ceListini.dcList.Position = i
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ColoraCampoAbiCab()
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(edAn_abi.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edAn_abi.BackColor = Color.Transparent
        edAn_banc1.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edAn_abi.Text, DittaCorrente, "ABI", "N", , dttTmp)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abiabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edAn_abi.BackColor = Color.Salmon
          edAn_banc1.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edAn_abi.BackColor = Color.Transparent
          edAn_banc1.BackColor = Color.Transparent
        End If
      End If

      If NTSCInt(edAn_cab.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edAn_cab.BackColor = Color.Transparent
        edAn_banc2.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edAn_cab.Text, DittaCorrente, "CAB", "N", , dttTmp, edAn_abi.Text)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abcabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edAn_cab.BackColor = Color.Salmon
          edAn_banc2.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edAn_cab.BackColor = Color.Transparent
          edAn_banc2.BackColor = Color.Transparent
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub cbAn_gescon_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAn_gescon.SelectedValueChanged
    Try
      If cbAn_gescon.SelectedValue = "E" Then
        edAn_perescon.Enabled = True
      Else
        edAn_perescon.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class

