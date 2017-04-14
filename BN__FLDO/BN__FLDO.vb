Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__FLDO
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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

  Public oCleFldo As CLE__FLDO
  Public oCallParams As CLE__CLDP
  Public dcTesta As BindingSource = New BindingSource()
  Public dcCorpo As BindingSource = New BindingSource()
  Public dcPrin As BindingSource = New BindingSource()
  Public dcScad As BindingSource = New BindingSource()
  Public dttTipi As New DataTable
  Public dttScenario As New DataTable
  Public dsFiltri As New DataSet
  Public dcFiltri As New BindingSource
  Public Const MAXROW As Integer = 24
  Public bInSalva As Boolean = False
  Public strScenarioCorrente As String = ""
  Public dsGri As New DataSet                 'dataset a cui sono collegate le griglie
  Public strLastGriFocus As String = ""
  Public bChiamatoDaChild As Boolean = False

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__FLDO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbApridoc = New NTSInformatica.NTSBarMenuItem
    Me.tlbCreaNewDoc = New NTSInformatica.NTSBarMenuItem
    Me.tlbGriTesta = New NTSInformatica.NTSBarMenuItem
    Me.tlbGriCorpo = New NTSInformatica.NTSBarMenuItem
    Me.tlbGriPrin = New NTSInformatica.NTSBarMenuItem
    Me.tlbGriScad = New NTSInformatica.NTSBarMenuItem
    Me.tlbGriReset = New NTSInformatica.NTSBarMenuItem
    Me.tlbNoModal = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbApriStampe = New NTSInformatica.NTSBarMenuItem
    Me.fmAnalisi = New NTSInformatica.NTSGroupBox
    Me.ckScad = New NTSInformatica.NTSCheckBox
    Me.ckContab = New NTSInformatica.NTSCheckBox
    Me.ckFatture = New NTSInformatica.NTSCheckBox
    Me.ckMagaz = New NTSInformatica.NTSCheckBox
    Me.ckNote = New NTSInformatica.NTSCheckBox
    Me.ckOrdini = New NTSInformatica.NTSCheckBox
    Me.ckOfferte = New NTSInformatica.NTSCheckBox
    Me.fmfiltriGlobali = New NTSInformatica.NTSGroupBox
    Me.edDataA = New NTSInformatica.NTSTextBoxData
    Me.edDataDa = New NTSInformatica.NTSTextBoxData
    Me.edCommessa = New NTSInformatica.NTSTextBoxNum
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.edLead = New NTSInformatica.NTSTextBoxNum
    Me.edClifor = New NTSInformatica.NTSTextBoxNum
    Me.edOperatore = New NTSInformatica.NTSTextBoxStr
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.lbOperatore = New NTSInformatica.NTSLabel
    Me.lbCommessa = New NTSInformatica.NTSLabel
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbDataA = New NTSInformatica.NTSLabel
    Me.lbDataDa = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbLead = New NTSInformatica.NTSLabel
    Me.lbClifor = New NTSInformatica.NTSLabel
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.grFiltri = New NTSInformatica.NTSGrid
    Me.grvFiltri = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.cbScenario = New NTSInformatica.NTSComboBox
    Me.lbScenario = New NTSInformatica.NTSLabel
    Me.ckGriSoloArtFiltri = New NTSInformatica.NTSCheckBox
    Me.ckGriSoloUnDoc = New NTSInformatica.NTSCheckBox
    Me.tsFldo = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnFiltri = New NTSInformatica.NTSPanel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.pnFiltriSx = New NTSInformatica.NTSPanel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnGriglie = New NTSInformatica.NTSPanel
    Me.grTesta = New NTSInformatica.NTSGrid
    Me.grvTesta = New NTSInformatica.NTSGridView
    Me.et_tipork = New NTSInformatica.NTSGridColumn
    Me.et_anno = New NTSInformatica.NTSGridColumn
    Me.et_serie = New NTSInformatica.NTSGridColumn
    Me.et_numdoc = New NTSInformatica.NTSGridColumn
    Me.et_datdoc = New NTSInformatica.NTSGridColumn
    Me.et_riferim = New NTSInformatica.NTSGridColumn
    Me.et_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.et_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.et_totdoc = New NTSInformatica.NTSGridColumn
    Me.et_totdocv = New NTSInformatica.NTSGridColumn
    Me.et_valuta = New NTSInformatica.NTSGridColumn
    Me.xx_valuta = New NTSInformatica.NTSGridColumn
    Me.et_cambio = New NTSInformatica.NTSGridColumn
    Me.et_flevas = New NTSInformatica.NTSGridColumn
    Me.et_rilasciato = New NTSInformatica.NTSGridColumn
    Me.et_confermato = New NTSInformatica.NTSGridColumn
    Me.et_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.et_causale = New NTSInformatica.NTSGridColumn
    Me.xx_causale = New NTSInformatica.NTSGridColumn
    Me.et_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.et_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xx_magaz2 = New NTSInformatica.NTSGridColumn
    Me.et_magimp = New NTSInformatica.NTSGridColumn
    Me.et_datcons = New NTSInformatica.NTSGridColumn
    Me.et_codagen = New NTSInformatica.NTSGridColumn
    Me.xx_codagen = New NTSInformatica.NTSGridColumn
    Me.et_listino = New NTSInformatica.NTSGridColumn
    Me.et_codese = New NTSInformatica.NTSGridColumn
    Me.xx_codese = New NTSInformatica.NTSGridColumn
    Me.et_controp = New NTSInformatica.NTSGridColumn
    Me.xx_controp = New NTSInformatica.NTSGridColumn
    Me.et_contfatt = New NTSInformatica.NTSGridColumn
    Me.et_scont1 = New NTSInformatica.NTSGridColumn
    Me.et_scont2 = New NTSInformatica.NTSGridColumn
    Me.et_scopag = New NTSInformatica.NTSGridColumn
    Me.et_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.et_abi = New NTSInformatica.NTSGridColumn
    Me.et_cab = New NTSInformatica.NTSGridColumn
    Me.et_banc1 = New NTSInformatica.NTSGridColumn
    Me.et_banc2 = New NTSInformatica.NTSGridColumn
    Me.et_numpar = New NTSInformatica.NTSGridColumn
    Me.et_datpar = New NTSInformatica.NTSGridColumn
    Me.et_opnome = New NTSInformatica.NTSGridColumn
    Me.et_note = New NTSInformatica.NTSGridColumn
    Me.et_opinc = New NTSInformatica.NTSGridColumn
    Me.et_coddest2 = New NTSInformatica.NTSGridColumn
    Me.et_oggetto = New NTSInformatica.NTSGridColumn
    Me.et_vers = New NTSInformatica.NTSGridColumn
    Me.et_codlead = New NTSInformatica.NTSGridColumn
    Me.xx_codlead = New NTSInformatica.NTSGridColumn
    Me.NtsSplitter1 = New NTSInformatica.NTSSplitter
    Me.grCorpo = New NTSInformatica.NTSGrid
    Me.grvCorpo = New NTSInformatica.NTSGridView
    Me.ec_tipork = New NTSInformatica.NTSGridColumn
    Me.ec_anno = New NTSInformatica.NTSGridColumn
    Me.ec_serie = New NTSInformatica.NTSGridColumn
    Me.ec_numdoc = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_desint = New NTSInformatica.NTSGridColumn
    Me.ec_unmis = New NTSInformatica.NTSGridColumn
    Me.ec_colli = New NTSInformatica.NTSGridColumn
    Me.ec_ump = New NTSInformatica.NTSGridColumn
    Me.ec_quant = New NTSInformatica.NTSGridColumn
    Me.ec_prezzo = New NTSInformatica.NTSGridColumn
    Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
    Me.ec_preziva = New NTSInformatica.NTSGridColumn
    Me.ec_valore = New NTSInformatica.NTSGridColumn
    Me.ec_scont1 = New NTSInformatica.NTSGridColumn
    Me.ec_scont2 = New NTSInformatica.NTSGridColumn
    Me.ec_scont3 = New NTSInformatica.NTSGridColumn
    Me.ec_scont4 = New NTSInformatica.NTSGridColumn
    Me.ec_scont5 = New NTSInformatica.NTSGridColumn
    Me.ec_scont6 = New NTSInformatica.NTSGridColumn
    Me.ec_note = New NTSInformatica.NTSGridColumn
    Me.ec_datcons = New NTSInformatica.NTSGridColumn
    Me.ec_magaz = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz = New NTSInformatica.NTSGridColumn
    Me.ec_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz2 = New NTSInformatica.NTSGridColumn
    Me.ec_quaeva = New NTSInformatica.NTSGridColumn
    Me.ec_quapre = New NTSInformatica.NTSGridColumn
    Me.ec_flevas = New NTSInformatica.NTSGridColumn
    Me.ec_flevapre = New NTSInformatica.NTSGridColumn
    Me.ec_provv = New NTSInformatica.NTSGridColumn
    Me.ec_vprovv = New NTSInformatica.NTSGridColumn
    Me.ec_controp = New NTSInformatica.NTSGridColumn
    Me.xxo_controp = New NTSInformatica.NTSGridColumn
    Me.ec_codiva = New NTSInformatica.NTSGridColumn
    Me.xxo_codiva = New NTSInformatica.NTSGridColumn
    Me.ec_stasino = New NTSInformatica.NTSGridColumn
    Me.ec_prelist = New NTSInformatica.NTSGridColumn
    Me.ec_codcfam = New NTSInformatica.NTSGridColumn
    Me.xxo_codcfam = New NTSInformatica.NTSGridColumn
    Me.ec_commeca = New NTSInformatica.NTSGridColumn
    Me.xxo_commeca = New NTSInformatica.NTSGridColumn
    Me.ec_subcommeca = New NTSInformatica.NTSGridColumn
    Me.ec_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_codcena = New NTSInformatica.NTSGridColumn
    Me.ec_confermato = New NTSInformatica.NTSGridColumn
    Me.ec_rilasciato = New NTSInformatica.NTSGridColumn
    Me.ec_aperto = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.ec_ubicaz = New NTSInformatica.NTSGridColumn
    Me.ec_causale = New NTSInformatica.NTSGridColumn
    Me.xxo_causale = New NTSInformatica.NTSGridColumn
    Me.ec_causale2 = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.xxo_fase = New NTSInformatica.NTSGridColumn
    Me.ec_misura1 = New NTSInformatica.NTSGridColumn
    Me.ec_misura2 = New NTSInformatica.NTSGridColumn
    Me.ec_misura3 = New NTSInformatica.NTSGridColumn
    Me.ec_datini = New NTSInformatica.NTSGridColumn
    Me.ec_datfin = New NTSInformatica.NTSGridColumn
    Me.ec_ortipo = New NTSInformatica.NTSGridColumn
    Me.ec_oranno = New NTSInformatica.NTSGridColumn
    Me.ec_orserie = New NTSInformatica.NTSGridColumn
    Me.ec_ornum = New NTSInformatica.NTSGridColumn
    Me.ec_orriga = New NTSInformatica.NTSGridColumn
    Me.ec_salcon = New NTSInformatica.NTSGridColumn
    Me.ec_nptipo = New NTSInformatica.NTSGridColumn
    Me.ec_npanno = New NTSInformatica.NTSGridColumn
    Me.ec_npserie = New NTSInformatica.NTSGridColumn
    Me.ec_npnum = New NTSInformatica.NTSGridColumn
    Me.ec_npvers = New NTSInformatica.NTSGridColumn
    Me.ec_npriga = New NTSInformatica.NTSGridColumn
    Me.ec_pnsalcon = New NTSInformatica.NTSGridColumn
    Me.ec_vers = New NTSInformatica.NTSGridColumn
    Me.xxo_conto = New NTSInformatica.NTSGridColumn
    Me.NtsSplitter2 = New NTSInformatica.NTSSplitter
    Me.grPrin = New NTSInformatica.NTSGrid
    Me.grvPrin = New NTSInformatica.NTSGridView
    Me.pn_datreg = New NTSInformatica.NTSGridColumn
    Me.pn_numreg = New NTSInformatica.NTSGridColumn
    Me.pn_riga = New NTSInformatica.NTSGridColumn
    Me.pn_causale = New NTSInformatica.NTSGridColumn
    Me.pn_descauc = New NTSInformatica.NTSGridColumn
    Me.pn_conto = New NTSInformatica.NTSGridColumn
    Me.xxp_conto = New NTSInformatica.NTSGridColumn
    Me.pn_descr = New NTSInformatica.NTSGridColumn
    Me.pn_darave = New NTSInformatica.NTSGridColumn
    Me.pn_importo = New NTSInformatica.NTSGridColumn
    Me.pn_dare = New NTSInformatica.NTSGridColumn
    Me.pn_avere = New NTSInformatica.NTSGridColumn
    Me.pn_datdoc = New NTSInformatica.NTSGridColumn
    Me.pn_numdoc = New NTSInformatica.NTSGridColumn
    Me.pn_alfdoc = New NTSInformatica.NTSGridColumn
    Me.pn_controp = New NTSInformatica.NTSGridColumn
    Me.xxp_controp = New NTSInformatica.NTSGridColumn
    Me.pn_annpar = New NTSInformatica.NTSGridColumn
    Me.pn_alfpar = New NTSInformatica.NTSGridColumn
    Me.pn_numpar = New NTSInformatica.NTSGridColumn
    Me.pn_codvalu = New NTSInformatica.NTSGridColumn
    Me.xxp_codvalu = New NTSInformatica.NTSGridColumn
    Me.pn_cambio = New NTSInformatica.NTSGridColumn
    Me.pn_impval = New NTSInformatica.NTSGridColumn
    Me.pn_dareval = New NTSInformatica.NTSGridColumn
    Me.pn_avereval = New NTSInformatica.NTSGridColumn
    Me.pn_tregiva = New NTSInformatica.NTSGridColumn
    Me.pn_nregiva = New NTSInformatica.NTSGridColumn
    Me.pn_codiva = New NTSInformatica.NTSGridColumn
    Me.xxp_codiva = New NTSInformatica.NTSGridColumn
    Me.pn_aliqiva = New NTSInformatica.NTSGridColumn
    Me.pn_indetr = New NTSInformatica.NTSGridColumn
    Me.pn_contocf = New NTSInformatica.NTSGridColumn
    Me.xxp_contocf = New NTSInformatica.NTSGridColumn
    Me.pn_imponib = New NTSInformatica.NTSGridColumn
    Me.pn_imponibval = New NTSInformatica.NTSGridColumn
    Me.pn_tipacq = New NTSInformatica.NTSGridColumn
    Me.pn_numpro = New NTSInformatica.NTSGridColumn
    Me.pn_alfpro = New NTSInformatica.NTSGridColumn
    Me.pn_integr = New NTSInformatica.NTSGridColumn
    Me.pn_fllg = New NTSInformatica.NTSGridColumn
    Me.pn_dtcomiva = New NTSInformatica.NTSGridColumn
    Me.pn_dtvaluta = New NTSInformatica.NTSGridColumn
    Me.pn_dtcomplaf = New NTSInformatica.NTSGridColumn
    Me.pn_datini = New NTSInformatica.NTSGridColumn
    Me.pn_datfin = New NTSInformatica.NTSGridColumn
    Me.pn_ultagg = New NTSInformatica.NTSGridColumn
    Me.pn_opnome = New NTSInformatica.NTSGridColumn
    Me.pn_escomp = New NTSInformatica.NTSGridColumn
    Me.NtsSplitter3 = New NTSInformatica.NTSSplitter
    Me.grScad = New NTSInformatica.NTSGrid
    Me.grvScad = New NTSInformatica.NTSGridView
    Me.sc_conto = New NTSInformatica.NTSGridColumn
    Me.xxc_conto = New NTSInformatica.NTSGridColumn
    Me.sc_datsca = New NTSInformatica.NTSGridColumn
    Me.sc_importoda = New NTSInformatica.NTSGridColumn
    Me.sc_impvalda = New NTSInformatica.NTSGridColumn
    Me.sc_annpar = New NTSInformatica.NTSGridColumn
    Me.sc_alfpar = New NTSInformatica.NTSGridColumn
    Me.sc_numpar = New NTSInformatica.NTSGridColumn
    Me.sc_numrata = New NTSInformatica.NTSGridColumn
    Me.sc_darave = New NTSInformatica.NTSGridColumn
    Me.sc_flsaldato = New NTSInformatica.NTSGridColumn
    Me.sc_datdoc = New NTSInformatica.NTSGridColumn
    Me.sc_alfdoc = New NTSInformatica.NTSGridColumn
    Me.sc_numdoc = New NTSInformatica.NTSGridColumn
    Me.sc_codpaga = New NTSInformatica.NTSGridColumn
    Me.xxc_codpaga = New NTSInformatica.NTSGridColumn
    Me.sc_tippaga = New NTSInformatica.NTSGridColumn
    Me.sc_descr = New NTSInformatica.NTSGridColumn
    Me.sc_causale = New NTSInformatica.NTSGridColumn
    Me.xxc_causale = New NTSInformatica.NTSGridColumn
    Me.sc_codvalu = New NTSInformatica.NTSGridColumn
    Me.xxc_codvalu = New NTSInformatica.NTSGridColumn
    Me.sc_cambio = New NTSInformatica.NTSGridColumn
    Me.sc_insolu = New NTSInformatica.NTSGridColumn
    Me.sc_codbanc = New NTSInformatica.NTSGridColumn
    Me.xxc_codbanc = New NTSInformatica.NTSGridColumn
    Me.sc_abi = New NTSInformatica.NTSGridColumn
    Me.sc_cab = New NTSInformatica.NTSGridColumn
    Me.sc_banc1 = New NTSInformatica.NTSGridColumn
    Me.sc_banc2 = New NTSInformatica.NTSGridColumn
    Me.sc_numcc = New NTSInformatica.NTSGridColumn
    Me.sc_cin = New NTSInformatica.NTSGridColumn
    Me.sc_prefiban = New NTSInformatica.NTSGridColumn
    Me.sc_iban = New NTSInformatica.NTSGridColumn
    Me.sc_alfpro = New NTSInformatica.NTSGridColumn
    Me.sc_numprot = New NTSInformatica.NTSGridColumn
    Me.sc_codcage = New NTSInformatica.NTSGridColumn
    Me.xxc_codcage = New NTSInformatica.NTSGridColumn
    Me.sc_controp = New NTSInformatica.NTSGridColumn
    Me.xxc_controp = New NTSInformatica.NTSGridColumn
    Me.sc_commeca = New NTSInformatica.NTSGridColumn
    Me.xxc_commeca = New NTSInformatica.NTSGridColumn
    Me.sc_subcommeca = New NTSInformatica.NTSGridColumn
    Me.sc_fldis = New NTSInformatica.NTSGridColumn
    Me.sc_dtdist = New NTSInformatica.NTSGridColumn
    Me.sc_numdist = New NTSInformatica.NTSGridColumn
    Me.sc_opdist = New NTSInformatica.NTSGridColumn
    Me.sc_datreg = New NTSInformatica.NTSGridColumn
    Me.sc_numreg = New NTSInformatica.NTSGridColumn
    Me.sc_dtsaldato = New NTSInformatica.NTSGridColumn
    Me.sc_rgsaldato = New NTSInformatica.NTSGridColumn
    Me.sc_integr = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnFlusso = New NTSInformatica.NTSPanel
    Me.ceFldo = New NTSInformatica.NTSXXFLDO
    Me.tlbLocalizzaGoogle = New NTSInformatica.NTSBarMenuItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAnalisi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAnalisi.SuspendLayout()
    CType(Me.ckScad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckContab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatture.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOrdini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOfferte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmfiltriGlobali, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmfiltriGlobali.SuspendLayout()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommessa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClifor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOperatore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.grFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbScenario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGriSoloArtFiltri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGriSoloUnDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsFldo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsFldo.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri.SuspendLayout()
    CType(Me.pnFiltriSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltriSx.SuspendLayout()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnGriglie, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglie.SuspendLayout()
    CType(Me.grTesta, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTesta, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grPrin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvPrin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grScad, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvScad, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnFlusso, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFlusso.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbGriTesta, Me.tlbGriCorpo, Me.tlbGriPrin, Me.tlbGriScad, Me.tlbGriReset, Me.tlbApridoc, Me.tlbApriStampe, Me.tlbNoModal, Me.tlbCreaNewDoc, Me.tlbLocalizzaGoogle})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 17
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApridoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCreaNewDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLocalizzaGoogle, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGriTesta, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGriCorpo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGriPrin), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGriScad), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGriReset), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoModal, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbApridoc
    '
    Me.tlbApridoc.Caption = "Apri documento/registrazione"
    Me.tlbApridoc.Id = 23
    Me.tlbApridoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A))
    Me.tlbApridoc.Name = "tlbApridoc"
    Me.tlbApridoc.NTSIsCheckBox = False
    Me.tlbApridoc.Visible = True
    '
    'tlbCreaNewDoc
    '
    Me.tlbCreaNewDoc.Caption = "Crea nuova Offerta/Ordine/Documento"
    Me.tlbCreaNewDoc.Id = 26
    Me.tlbCreaNewDoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.N))
    Me.tlbCreaNewDoc.Name = "tlbCreaNewDoc"
    Me.tlbCreaNewDoc.NTSIsCheckBox = False
    Me.tlbCreaNewDoc.Visible = True
    '
    'tlbGriTesta
    '
    Me.tlbGriTesta.Caption = "Visualizza/nascondi griglia testate doc."
    Me.tlbGriTesta.Id = 18
    Me.tlbGriTesta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Z))
    Me.tlbGriTesta.Name = "tlbGriTesta"
    Me.tlbGriTesta.NTSIsCheckBox = False
    Me.tlbGriTesta.Visible = True
    '
    'tlbGriCorpo
    '
    Me.tlbGriCorpo.Caption = "Visualizza/nascondi griglia corpo doc."
    Me.tlbGriCorpo.Id = 19
    Me.tlbGriCorpo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.X))
    Me.tlbGriCorpo.Name = "tlbGriCorpo"
    Me.tlbGriCorpo.NTSIsCheckBox = False
    Me.tlbGriCorpo.Visible = True
    '
    'tlbGriPrin
    '
    Me.tlbGriPrin.Caption = "Visualizza/nascondi griglia prima nota"
    Me.tlbGriPrin.Id = 20
    Me.tlbGriPrin.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C))
    Me.tlbGriPrin.Name = "tlbGriPrin"
    Me.tlbGriPrin.NTSIsCheckBox = False
    Me.tlbGriPrin.Visible = True
    '
    'tlbGriScad
    '
    Me.tlbGriScad.Caption = "Visualizza/nascondi griglia scadenze"
    Me.tlbGriScad.Id = 21
    Me.tlbGriScad.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.V))
    Me.tlbGriScad.Name = "tlbGriScad"
    Me.tlbGriScad.NTSIsCheckBox = False
    Me.tlbGriScad.Visible = True
    '
    'tlbGriReset
    '
    Me.tlbGriReset.Caption = "Reset dimensioni griglie"
    Me.tlbGriReset.Id = 22
    Me.tlbGriReset.Name = "tlbGriReset"
    Me.tlbGriReset.NTSIsCheckBox = False
    Me.tlbGriReset.Visible = True
    '
    'tlbNoModal
    '
    Me.tlbNoModal.Caption = "Apri progr. in modalita 'non modale'"
    Me.tlbNoModal.Id = 25
    Me.tlbNoModal.Name = "tlbNoModal"
    Me.tlbNoModal.NTSIsCheckBox = True
    Me.tlbNoModal.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbApriStampe
    '
    Me.tlbApriStampe.Caption = "Apri gestore di stampe"
    Me.tlbApriStampe.Id = 24
    Me.tlbApriStampe.Name = "tlbApriStampe"
    Me.tlbApriStampe.NTSIsCheckBox = False
    Me.tlbApriStampe.Visible = True
    '
    'fmAnalisi
    '
    Me.fmAnalisi.AllowDrop = True
    Me.fmAnalisi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAnalisi.Appearance.Options.UseBackColor = True
    Me.fmAnalisi.Controls.Add(Me.ckScad)
    Me.fmAnalisi.Controls.Add(Me.ckContab)
    Me.fmAnalisi.Controls.Add(Me.ckFatture)
    Me.fmAnalisi.Controls.Add(Me.ckMagaz)
    Me.fmAnalisi.Controls.Add(Me.ckNote)
    Me.fmAnalisi.Controls.Add(Me.ckOrdini)
    Me.fmAnalisi.Controls.Add(Me.ckOfferte)
    Me.fmAnalisi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAnalisi.Location = New System.Drawing.Point(12, 34)
    Me.fmAnalisi.Name = "fmAnalisi"
    Me.fmAnalisi.Size = New System.Drawing.Size(260, 107)
    Me.fmAnalisi.TabIndex = 4
    Me.fmAnalisi.Text = "Analisi su"
    '
    'ckScad
    '
    Me.ckScad.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckScad.Location = New System.Drawing.Point(133, 60)
    Me.ckScad.Name = "ckScad"
    Me.ckScad.NTSCheckValue = "S"
    Me.ckScad.NTSUnCheckValue = "N"
    Me.ckScad.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScad.Properties.Appearance.Options.UseBackColor = True
    Me.ckScad.Properties.Caption = "Scadenze"
    Me.ckScad.Size = New System.Drawing.Size(77, 19)
    Me.ckScad.TabIndex = 6
    '
    'ckContab
    '
    Me.ckContab.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContab.Location = New System.Drawing.Point(133, 40)
    Me.ckContab.Name = "ckContab"
    Me.ckContab.NTSCheckValue = "S"
    Me.ckContab.NTSUnCheckValue = "N"
    Me.ckContab.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContab.Properties.Appearance.Options.UseBackColor = True
    Me.ckContab.Properties.Caption = "Contabilità"
    Me.ckContab.Size = New System.Drawing.Size(77, 19)
    Me.ckContab.TabIndex = 5
    '
    'ckFatture
    '
    Me.ckFatture.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatture.Location = New System.Drawing.Point(133, 20)
    Me.ckFatture.Name = "ckFatture"
    Me.ckFatture.NTSCheckValue = "S"
    Me.ckFatture.NTSUnCheckValue = "N"
    Me.ckFatture.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatture.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatture.Properties.Caption = "Fatt./note acc. diff."
    Me.ckFatture.Size = New System.Drawing.Size(122, 19)
    Me.ckFatture.TabIndex = 4
    '
    'ckMagaz
    '
    Me.ckMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMagaz.Location = New System.Drawing.Point(15, 80)
    Me.ckMagaz.Name = "ckMagaz"
    Me.ckMagaz.NTSCheckValue = "S"
    Me.ckMagaz.NTSUnCheckValue = "N"
    Me.ckMagaz.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMagaz.Properties.Appearance.Options.UseBackColor = True
    Me.ckMagaz.Properties.Caption = "Movimenti di mag."
    Me.ckMagaz.Size = New System.Drawing.Size(113, 19)
    Me.ckMagaz.TabIndex = 3
    '
    'ckNote
    '
    Me.ckNote.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNote.Location = New System.Drawing.Point(15, 60)
    Me.ckNote.Name = "ckNote"
    Me.ckNote.NTSCheckValue = "S"
    Me.ckNote.NTSUnCheckValue = "N"
    Me.ckNote.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNote.Properties.Appearance.Options.UseBackColor = True
    Me.ckNote.Properties.Caption = "Note di prelievo"
    Me.ckNote.Size = New System.Drawing.Size(100, 19)
    Me.ckNote.TabIndex = 2
    '
    'ckOrdini
    '
    Me.ckOrdini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdini.Location = New System.Drawing.Point(15, 40)
    Me.ckOrdini.Name = "ckOrdini"
    Me.ckOrdini.NTSCheckValue = "S"
    Me.ckOrdini.NTSUnCheckValue = "N"
    Me.ckOrdini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdini.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdini.Properties.Caption = "Ordini"
    Me.ckOrdini.Size = New System.Drawing.Size(59, 19)
    Me.ckOrdini.TabIndex = 1
    '
    'ckOfferte
    '
    Me.ckOfferte.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOfferte.Location = New System.Drawing.Point(15, 20)
    Me.ckOfferte.Name = "ckOfferte"
    Me.ckOfferte.NTSCheckValue = "S"
    Me.ckOfferte.NTSUnCheckValue = "N"
    Me.ckOfferte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOfferte.Properties.Appearance.Options.UseBackColor = True
    Me.ckOfferte.Properties.Caption = "Offerte"
    Me.ckOfferte.Size = New System.Drawing.Size(68, 19)
    Me.ckOfferte.TabIndex = 0
    '
    'fmfiltriGlobali
    '
    Me.fmfiltriGlobali.AllowDrop = True
    Me.fmfiltriGlobali.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.fmfiltriGlobali.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmfiltriGlobali.Appearance.Options.UseBackColor = True
    Me.fmfiltriGlobali.Controls.Add(Me.edDataA)
    Me.fmfiltriGlobali.Controls.Add(Me.edDataDa)
    Me.fmfiltriGlobali.Controls.Add(Me.edCommessa)
    Me.fmfiltriGlobali.Controls.Add(Me.edEscomp)
    Me.fmfiltriGlobali.Controls.Add(Me.edAnno)
    Me.fmfiltriGlobali.Controls.Add(Me.edLead)
    Me.fmfiltriGlobali.Controls.Add(Me.edClifor)
    Me.fmfiltriGlobali.Controls.Add(Me.edOperatore)
    Me.fmfiltriGlobali.Controls.Add(Me.edArticolo)
    Me.fmfiltriGlobali.Controls.Add(Me.lbOperatore)
    Me.fmfiltriGlobali.Controls.Add(Me.lbCommessa)
    Me.fmfiltriGlobali.Controls.Add(Me.lbEscomp)
    Me.fmfiltriGlobali.Controls.Add(Me.lbAnno)
    Me.fmfiltriGlobali.Controls.Add(Me.lbDataA)
    Me.fmfiltriGlobali.Controls.Add(Me.lbDataDa)
    Me.fmfiltriGlobali.Controls.Add(Me.lbArticolo)
    Me.fmfiltriGlobali.Controls.Add(Me.lbLead)
    Me.fmfiltriGlobali.Controls.Add(Me.lbClifor)
    Me.fmfiltriGlobali.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmfiltriGlobali.Location = New System.Drawing.Point(12, 147)
    Me.fmfiltriGlobali.Name = "fmfiltriGlobali"
    Me.fmfiltriGlobali.Size = New System.Drawing.Size(260, 261)
    Me.fmfiltriGlobali.TabIndex = 5
    Me.fmfiltriGlobali.Text = "Filtri globali"
    '
    'edDataA
    '
    Me.edDataA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataA.EditValue = "31/12/2099"
    Me.edDataA.Location = New System.Drawing.Point(133, 132)
    Me.edDataA.Name = "edDataA"
    Me.edDataA.NTSDbField = ""
    Me.edDataA.NTSForzaVisZoom = False
    Me.edDataA.NTSOldValue = ""
    Me.edDataA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataA.Properties.MaxLength = 65536
    Me.edDataA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataA.Size = New System.Drawing.Size(112, 20)
    Me.edDataA.TabIndex = 26
    '
    'edDataDa
    '
    Me.edDataDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDa.EditValue = "01/01/1900"
    Me.edDataDa.Location = New System.Drawing.Point(133, 106)
    Me.edDataDa.Name = "edDataDa"
    Me.edDataDa.NTSDbField = ""
    Me.edDataDa.NTSForzaVisZoom = False
    Me.edDataDa.NTSOldValue = ""
    Me.edDataDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataDa.Properties.MaxLength = 65536
    Me.edDataDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataDa.Size = New System.Drawing.Size(112, 20)
    Me.edDataDa.TabIndex = 25
    '
    'edCommessa
    '
    Me.edCommessa.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCommessa.EditValue = "0"
    Me.edCommessa.Location = New System.Drawing.Point(133, 207)
    Me.edCommessa.Name = "edCommessa"
    Me.edCommessa.NTSDbField = ""
    Me.edCommessa.NTSFormat = "0"
    Me.edCommessa.NTSForzaVisZoom = False
    Me.edCommessa.NTSOldValue = ""
    Me.edCommessa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommessa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommessa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommessa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommessa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommessa.Properties.MaxLength = 65536
    Me.edCommessa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommessa.Size = New System.Drawing.Size(112, 20)
    Me.edCommessa.TabIndex = 24
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.EditValue = "0"
    Me.edEscomp.Location = New System.Drawing.Point(133, 182)
    Me.edEscomp.Name = "edEscomp"
    Me.edEscomp.NTSDbField = ""
    Me.edEscomp.NTSFormat = "0"
    Me.edEscomp.NTSForzaVisZoom = False
    Me.edEscomp.NTSOldValue = ""
    Me.edEscomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscomp.Properties.MaxLength = 65536
    Me.edEscomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscomp.Size = New System.Drawing.Size(112, 20)
    Me.edEscomp.TabIndex = 23
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "0"
    Me.edAnno.Location = New System.Drawing.Point(133, 158)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(112, 20)
    Me.edAnno.TabIndex = 22
    '
    'edLead
    '
    Me.edLead.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLead.EditValue = "0"
    Me.edLead.Location = New System.Drawing.Point(133, 55)
    Me.edLead.Name = "edLead"
    Me.edLead.NTSDbField = ""
    Me.edLead.NTSFormat = "0"
    Me.edLead.NTSForzaVisZoom = False
    Me.edLead.NTSOldValue = ""
    Me.edLead.Properties.Appearance.Options.UseTextOptions = True
    Me.edLead.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLead.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLead.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLead.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLead.Properties.MaxLength = 65536
    Me.edLead.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLead.Size = New System.Drawing.Size(112, 20)
    Me.edLead.TabIndex = 21
    '
    'edClifor
    '
    Me.edClifor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClifor.EditValue = "0"
    Me.edClifor.Location = New System.Drawing.Point(133, 31)
    Me.edClifor.Name = "edClifor"
    Me.edClifor.NTSDbField = ""
    Me.edClifor.NTSFormat = "0"
    Me.edClifor.NTSForzaVisZoom = False
    Me.edClifor.NTSOldValue = ""
    Me.edClifor.Properties.Appearance.Options.UseTextOptions = True
    Me.edClifor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClifor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClifor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClifor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClifor.Properties.MaxLength = 65536
    Me.edClifor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClifor.Size = New System.Drawing.Size(112, 20)
    Me.edClifor.TabIndex = 20
    '
    'edOperatore
    '
    Me.edOperatore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOperatore.Location = New System.Drawing.Point(133, 230)
    Me.edOperatore.Name = "edOperatore"
    Me.edOperatore.NTSDbField = ""
    Me.edOperatore.NTSForzaVisZoom = False
    Me.edOperatore.NTSOldValue = ""
    Me.edOperatore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOperatore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOperatore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOperatore.Properties.MaxLength = 65536
    Me.edOperatore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOperatore.Size = New System.Drawing.Size(112, 20)
    Me.edOperatore.TabIndex = 19
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(133, 80)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(112, 20)
    Me.edArticolo.TabIndex = 18
    '
    'lbOperatore
    '
    Me.lbOperatore.AutoSize = True
    Me.lbOperatore.BackColor = System.Drawing.Color.Transparent
    Me.lbOperatore.Location = New System.Drawing.Point(13, 233)
    Me.lbOperatore.Name = "lbOperatore"
    Me.lbOperatore.NTSDbField = ""
    Me.lbOperatore.Size = New System.Drawing.Size(57, 13)
    Me.lbOperatore.TabIndex = 17
    Me.lbOperatore.Text = "Operatore"
    Me.lbOperatore.Tooltip = ""
    Me.lbOperatore.UseMnemonic = False
    '
    'lbCommessa
    '
    Me.lbCommessa.AutoSize = True
    Me.lbCommessa.BackColor = System.Drawing.Color.Transparent
    Me.lbCommessa.Location = New System.Drawing.Point(12, 210)
    Me.lbCommessa.Name = "lbCommessa"
    Me.lbCommessa.NTSDbField = ""
    Me.lbCommessa.Size = New System.Drawing.Size(58, 13)
    Me.lbCommessa.TabIndex = 16
    Me.lbCommessa.Text = "Commessa"
    Me.lbCommessa.Tooltip = ""
    Me.lbCommessa.UseMnemonic = False
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(12, 185)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(80, 13)
    Me.lbEscomp.TabIndex = 15
    Me.lbEscomp.Text = "Esercizio comp."
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 161)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(70, 13)
    Me.lbAnno.TabIndex = 14
    Me.lbAnno.Text = "Anno docum."
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbDataA
    '
    Me.lbDataA.AutoSize = True
    Me.lbDataA.BackColor = System.Drawing.Color.Transparent
    Me.lbDataA.Location = New System.Drawing.Point(12, 135)
    Me.lbDataA.Name = "lbDataA"
    Me.lbDataA.NTSDbField = ""
    Me.lbDataA.Size = New System.Drawing.Size(39, 13)
    Me.lbDataA.TabIndex = 13
    Me.lbDataA.Text = "A data"
    Me.lbDataA.Tooltip = ""
    Me.lbDataA.UseMnemonic = False
    '
    'lbDataDa
    '
    Me.lbDataDa.AutoSize = True
    Me.lbDataDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDa.Location = New System.Drawing.Point(12, 109)
    Me.lbDataDa.Name = "lbDataDa"
    Me.lbDataDa.NTSDbField = ""
    Me.lbDataDa.Size = New System.Drawing.Size(45, 13)
    Me.lbDataDa.TabIndex = 12
    Me.lbDataDa.Text = "Da data"
    Me.lbDataDa.Tooltip = ""
    Me.lbDataDa.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(12, 83)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 11
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'lbLead
    '
    Me.lbLead.AutoSize = True
    Me.lbLead.BackColor = System.Drawing.Color.Transparent
    Me.lbLead.Location = New System.Drawing.Point(12, 58)
    Me.lbLead.Name = "lbLead"
    Me.lbLead.NTSDbField = ""
    Me.lbLead.Size = New System.Drawing.Size(30, 13)
    Me.lbLead.TabIndex = 10
    Me.lbLead.Text = "Lead"
    Me.lbLead.Tooltip = ""
    Me.lbLead.UseMnemonic = False
    '
    'lbClifor
    '
    Me.lbClifor.AutoSize = True
    Me.lbClifor.BackColor = System.Drawing.Color.Transparent
    Me.lbClifor.Location = New System.Drawing.Point(12, 34)
    Me.lbClifor.Name = "lbClifor"
    Me.lbClifor.NTSDbField = ""
    Me.lbClifor.Size = New System.Drawing.Size(86, 13)
    Me.lbClifor.TabIndex = 9
    Me.lbClifor.Text = "Cliente/fornitore"
    Me.lbClifor.Tooltip = ""
    Me.lbClifor.UseMnemonic = False
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.grFiltri)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(283, 34)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(496, 318)
    Me.fmFiltri.TabIndex = 6
    Me.fmFiltri.Text = "Filtri aggiuntivi"
    '
    'grFiltri
    '
    Me.grFiltri.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grFiltri.EmbeddedNavigator.Name = ""
    Me.grFiltri.Location = New System.Drawing.Point(2, 20)
    Me.grFiltri.MainView = Me.grvFiltri
    Me.grFiltri.Name = "grFiltri"
    Me.grFiltri.Size = New System.Drawing.Size(492, 296)
    Me.grFiltri.TabIndex = 6
    Me.grFiltri.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFiltri})
    '
    'grvFiltri
    '
    Me.grvFiltri.ActiveFilterEnabled = False
    Me.grvFiltri.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_valoreda, Me.xx_valorea})
    Me.grvFiltri.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFiltri.Enabled = True
    Me.grvFiltri.GridControl = Me.grFiltri
    Me.grvFiltri.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri.Name = "grvFiltri"
    Me.grvFiltri.NTSAllowDelete = True
    Me.grvFiltri.NTSAllowInsert = True
    Me.grvFiltri.NTSAllowUpdate = True
    Me.grvFiltri.NTSMenuContext = Nothing
    Me.grvFiltri.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri.OptionsFilter.AllowFilterEditor = False
    Me.grvFiltri.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri.OptionsNavigation.UseTabKey = False
    Me.grvFiltri.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFiltri.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri.OptionsView.ShowGroupPanel = False
    Me.grvFiltri.RowHeight = 16
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Caption = "Filtro"
    Me.xx_nome.Enabled = False
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowEdit = False
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nome.OptionsColumn.ReadOnly = True
    Me.xx_nome.OptionsFilter.AllowFilter = False
    Me.xx_nome.Visible = True
    Me.xx_nome.VisibleIndex = 0
    Me.xx_nome.Width = 140
    '
    'xx_valoreda
    '
    Me.xx_valoreda.AppearanceCell.Options.UseBackColor = True
    Me.xx_valoreda.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valoreda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valoreda.Caption = "Valore DA"
    Me.xx_valoreda.Enabled = True
    Me.xx_valoreda.FieldName = "xx_valoreda"
    Me.xx_valoreda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valoreda.Name = "xx_valoreda"
    Me.xx_valoreda.NTSRepositoryComboBox = Nothing
    Me.xx_valoreda.NTSRepositoryItemCheck = Nothing
    Me.xx_valoreda.NTSRepositoryItemMemo = Nothing
    Me.xx_valoreda.NTSRepositoryItemText = Nothing
    Me.xx_valoreda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valoreda.OptionsFilter.AllowFilter = False
    Me.xx_valoreda.Visible = True
    Me.xx_valoreda.VisibleIndex = 1
    Me.xx_valoreda.Width = 122
    '
    'xx_valorea
    '
    Me.xx_valorea.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea.Caption = "Valore A"
    Me.xx_valorea.Enabled = True
    Me.xx_valorea.FieldName = "xx_valorea"
    Me.xx_valorea.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea.Name = "xx_valorea"
    Me.xx_valorea.NTSRepositoryComboBox = Nothing
    Me.xx_valorea.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea.NTSRepositoryItemText = Nothing
    Me.xx_valorea.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valorea.OptionsFilter.AllowFilter = False
    Me.xx_valorea.Visible = True
    Me.xx_valorea.VisibleIndex = 2
    Me.xx_valorea.Width = 140
    '
    'cbScenario
    '
    Me.cbScenario.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbScenario.DataSource = Nothing
    Me.cbScenario.DisplayMember = ""
    Me.cbScenario.Location = New System.Drawing.Point(66, 8)
    Me.cbScenario.Name = "cbScenario"
    Me.cbScenario.NTSDbField = ""
    Me.cbScenario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbScenario.Properties.DropDownRows = 30
    Me.cbScenario.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbScenario.SelectedValue = ""
    Me.cbScenario.Size = New System.Drawing.Size(206, 20)
    Me.cbScenario.TabIndex = 7
    Me.cbScenario.ValueMember = ""
    '
    'lbScenario
    '
    Me.lbScenario.AutoSize = True
    Me.lbScenario.BackColor = System.Drawing.Color.Transparent
    Me.lbScenario.Location = New System.Drawing.Point(12, 11)
    Me.lbScenario.Name = "lbScenario"
    Me.lbScenario.NTSDbField = ""
    Me.lbScenario.Size = New System.Drawing.Size(48, 13)
    Me.lbScenario.TabIndex = 8
    Me.lbScenario.Text = "Scenario"
    Me.lbScenario.Tooltip = ""
    Me.lbScenario.UseMnemonic = False
    '
    'ckGriSoloArtFiltri
    '
    Me.ckGriSoloArtFiltri.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ckGriSoloArtFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGriSoloArtFiltri.EditValue = True
    Me.ckGriSoloArtFiltri.Location = New System.Drawing.Point(283, 380)
    Me.ckGriSoloArtFiltri.Name = "ckGriSoloArtFiltri"
    Me.ckGriSoloArtFiltri.NTSCheckValue = "S"
    Me.ckGriSoloArtFiltri.NTSUnCheckValue = "N"
    Me.ckGriSoloArtFiltri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGriSoloArtFiltri.Properties.Appearance.Options.UseBackColor = True
    Me.ckGriSoloArtFiltri.Properties.Caption = "Carica la griglia movimenti solo con le righe degli articoli impostati nei filtri" & _
        ""
    Me.ckGriSoloArtFiltri.Size = New System.Drawing.Size(375, 19)
    Me.ckGriSoloArtFiltri.TabIndex = 9
    '
    'ckGriSoloUnDoc
    '
    Me.ckGriSoloUnDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ckGriSoloUnDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGriSoloUnDoc.EditValue = True
    Me.ckGriSoloUnDoc.Location = New System.Drawing.Point(283, 357)
    Me.ckGriSoloUnDoc.Name = "ckGriSoloUnDoc"
    Me.ckGriSoloUnDoc.NTSCheckValue = "S"
    Me.ckGriSoloUnDoc.NTSUnCheckValue = "N"
    Me.ckGriSoloUnDoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGriSoloUnDoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckGriSoloUnDoc.Properties.Caption = "Carica la griglia movimenti solo con le righe del documento si cui si è posiziona" & _
        "ti nella griglia testate"
    Me.ckGriSoloUnDoc.Size = New System.Drawing.Size(489, 19)
    Me.ckGriSoloUnDoc.TabIndex = 10
    '
    'tsFldo
    '
    Me.tsFldo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsFldo.Location = New System.Drawing.Point(0, 30)
    Me.tsFldo.Name = "tsFldo"
    Me.tsFldo.SelectedTabPage = Me.NtsTabPage2
    Me.tsFldo.Size = New System.Drawing.Size(791, 452)
    Me.tsFldo.TabIndex = 11
    Me.tsFldo.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3})
    Me.tsFldo.Text = "tsFldo"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnFiltri)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(782, 422)
    Me.NtsTabPage1.Text = "&1 - Filtri"
    '
    'pnFiltri
    '
    Me.pnFiltri.AllowDrop = True
    Me.pnFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri.Appearance.Options.UseBackColor = True
    Me.pnFiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri.Controls.Add(Me.cmdLock)
    Me.pnFiltri.Controls.Add(Me.pnFiltriSx)
    Me.pnFiltri.Controls.Add(Me.fmFiltri)
    Me.pnFiltri.Controls.Add(Me.ckGriSoloUnDoc)
    Me.pnFiltri.Controls.Add(Me.ckGriSoloArtFiltri)
    Me.pnFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFiltri.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltri.Name = "pnFiltri"
    Me.pnFiltri.Size = New System.Drawing.Size(782, 422)
    Me.pnFiltri.TabIndex = 1
    Me.pnFiltri.Text = "NtsPanel1"
    '
    'cmdLock
    '
    Me.cmdLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(665, 399)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.Size = New System.Drawing.Size(111, 20)
    Me.cmdLock.TabIndex = 11
    Me.cmdLock.Text = "Blocca/sblocca filtri"
    '
    'pnFiltriSx
    '
    Me.pnFiltriSx.AllowDrop = True
    Me.pnFiltriSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltriSx.Appearance.Options.UseBackColor = True
    Me.pnFiltriSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltriSx.Controls.Add(Me.lbScenario)
    Me.pnFiltriSx.Controls.Add(Me.fmAnalisi)
    Me.pnFiltriSx.Controls.Add(Me.fmfiltriGlobali)
    Me.pnFiltriSx.Controls.Add(Me.cbScenario)
    Me.pnFiltriSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltriSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnFiltriSx.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltriSx.Name = "pnFiltriSx"
    Me.pnFiltriSx.Size = New System.Drawing.Size(282, 422)
    Me.pnFiltriSx.TabIndex = 0
    Me.pnFiltriSx.Text = "NtsPanel1"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage2.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage2.Controls.Add(Me.pnGriglie)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(782, 422)
    Me.NtsTabPage2.Text = "&2 - Griglie"
    '
    'pnGriglie
    '
    Me.pnGriglie.AllowDrop = True
    Me.pnGriglie.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGriglie.Appearance.Options.UseBackColor = True
    Me.pnGriglie.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGriglie.Controls.Add(Me.grTesta)
    Me.pnGriglie.Controls.Add(Me.NtsSplitter1)
    Me.pnGriglie.Controls.Add(Me.grCorpo)
    Me.pnGriglie.Controls.Add(Me.NtsSplitter2)
    Me.pnGriglie.Controls.Add(Me.grPrin)
    Me.pnGriglie.Controls.Add(Me.NtsSplitter3)
    Me.pnGriglie.Controls.Add(Me.grScad)
    Me.pnGriglie.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGriglie.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGriglie.Location = New System.Drawing.Point(0, 0)
    Me.pnGriglie.Name = "pnGriglie"
    Me.pnGriglie.Size = New System.Drawing.Size(782, 422)
    Me.pnGriglie.TabIndex = 1
    Me.pnGriglie.Text = "NtsPanel1"
    '
    'grTesta
    '
    Me.grTesta.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grTesta.EmbeddedNavigator.Name = ""
    Me.grTesta.Location = New System.Drawing.Point(0, 0)
    Me.grTesta.MainView = Me.grvTesta
    Me.grTesta.Name = "grTesta"
    Me.grTesta.Size = New System.Drawing.Size(782, 103)
    Me.grTesta.TabIndex = 0
    Me.grTesta.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTesta})
    '
    'grvTesta
    '
    Me.grvTesta.ActiveFilterEnabled = False
    Me.grvTesta.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.et_tipork, Me.et_anno, Me.et_serie, Me.et_numdoc, Me.et_datdoc, Me.et_riferim, Me.et_conto, Me.xx_conto, Me.et_coddest, Me.xx_coddest, Me.et_totdoc, Me.et_totdocv, Me.et_valuta, Me.xx_valuta, Me.et_cambio, Me.et_flevas, Me.et_rilasciato, Me.et_confermato, Me.et_tipobf, Me.xx_tipobf, Me.et_causale, Me.xx_causale, Me.et_magaz, Me.xx_magaz, Me.et_magaz2, Me.xx_magaz2, Me.et_magimp, Me.et_datcons, Me.et_codagen, Me.xx_codagen, Me.et_listino, Me.et_codese, Me.xx_codese, Me.et_controp, Me.xx_controp, Me.et_contfatt, Me.et_scont1, Me.et_scont2, Me.et_scopag, Me.et_codpaga, Me.xx_codpaga, Me.et_abi, Me.et_cab, Me.et_banc1, Me.et_banc2, Me.et_numpar, Me.et_datpar, Me.et_opnome, Me.et_note, Me.et_opinc, Me.et_coddest2, Me.et_oggetto, Me.et_vers, Me.et_codlead, Me.xx_codlead})
    Me.grvTesta.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTesta.Enabled = True
    Me.grvTesta.GridControl = Me.grTesta
    Me.grvTesta.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTesta.Name = "grvTesta"
    Me.grvTesta.NTSAllowDelete = True
    Me.grvTesta.NTSAllowInsert = True
    Me.grvTesta.NTSAllowUpdate = True
    Me.grvTesta.NTSMenuContext = Nothing
    Me.grvTesta.OptionsCustomization.AllowRowSizing = True
    Me.grvTesta.OptionsFilter.AllowFilterEditor = False
    Me.grvTesta.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTesta.OptionsNavigation.UseTabKey = False
    Me.grvTesta.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTesta.OptionsView.ColumnAutoWidth = False
    Me.grvTesta.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTesta.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTesta.OptionsView.ShowGroupPanel = False
    Me.grvTesta.RowHeight = 14
    '
    'et_tipork
    '
    Me.et_tipork.AppearanceCell.Options.UseBackColor = True
    Me.et_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.et_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_tipork.Caption = "Tipo"
    Me.et_tipork.Enabled = True
    Me.et_tipork.FieldName = "et_tipork"
    Me.et_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_tipork.Name = "et_tipork"
    Me.et_tipork.NTSRepositoryComboBox = Nothing
    Me.et_tipork.NTSRepositoryItemCheck = Nothing
    Me.et_tipork.NTSRepositoryItemMemo = Nothing
    Me.et_tipork.NTSRepositoryItemText = Nothing
    Me.et_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_tipork.OptionsFilter.AllowFilter = False
    Me.et_tipork.Visible = True
    Me.et_tipork.VisibleIndex = 0
    Me.et_tipork.Width = 70
    '
    'et_anno
    '
    Me.et_anno.AppearanceCell.Options.UseBackColor = True
    Me.et_anno.AppearanceCell.Options.UseTextOptions = True
    Me.et_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_anno.Caption = "Anno"
    Me.et_anno.Enabled = True
    Me.et_anno.FieldName = "et_anno"
    Me.et_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_anno.Name = "et_anno"
    Me.et_anno.NTSRepositoryComboBox = Nothing
    Me.et_anno.NTSRepositoryItemCheck = Nothing
    Me.et_anno.NTSRepositoryItemMemo = Nothing
    Me.et_anno.NTSRepositoryItemText = Nothing
    Me.et_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_anno.OptionsFilter.AllowFilter = False
    Me.et_anno.Visible = True
    Me.et_anno.VisibleIndex = 1
    Me.et_anno.Width = 70
    '
    'et_serie
    '
    Me.et_serie.AppearanceCell.Options.UseBackColor = True
    Me.et_serie.AppearanceCell.Options.UseTextOptions = True
    Me.et_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_serie.Caption = "Serie"
    Me.et_serie.Enabled = True
    Me.et_serie.FieldName = "et_serie"
    Me.et_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_serie.Name = "et_serie"
    Me.et_serie.NTSRepositoryComboBox = Nothing
    Me.et_serie.NTSRepositoryItemCheck = Nothing
    Me.et_serie.NTSRepositoryItemMemo = Nothing
    Me.et_serie.NTSRepositoryItemText = Nothing
    Me.et_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_serie.OptionsFilter.AllowFilter = False
    Me.et_serie.Visible = True
    Me.et_serie.VisibleIndex = 2
    '
    'et_numdoc
    '
    Me.et_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.et_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.et_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_numdoc.Caption = "Numero"
    Me.et_numdoc.Enabled = True
    Me.et_numdoc.FieldName = "et_numdoc"
    Me.et_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_numdoc.Name = "et_numdoc"
    Me.et_numdoc.NTSRepositoryComboBox = Nothing
    Me.et_numdoc.NTSRepositoryItemCheck = Nothing
    Me.et_numdoc.NTSRepositoryItemMemo = Nothing
    Me.et_numdoc.NTSRepositoryItemText = Nothing
    Me.et_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_numdoc.OptionsFilter.AllowFilter = False
    Me.et_numdoc.Visible = True
    Me.et_numdoc.VisibleIndex = 3
    '
    'et_datdoc
    '
    Me.et_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.et_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.et_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_datdoc.Caption = "Data doc"
    Me.et_datdoc.Enabled = True
    Me.et_datdoc.FieldName = "et_datdoc"
    Me.et_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_datdoc.Name = "et_datdoc"
    Me.et_datdoc.NTSRepositoryComboBox = Nothing
    Me.et_datdoc.NTSRepositoryItemCheck = Nothing
    Me.et_datdoc.NTSRepositoryItemMemo = Nothing
    Me.et_datdoc.NTSRepositoryItemText = Nothing
    Me.et_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_datdoc.OptionsFilter.AllowFilter = False
    Me.et_datdoc.Visible = True
    Me.et_datdoc.VisibleIndex = 4
    '
    'et_riferim
    '
    Me.et_riferim.AppearanceCell.Options.UseBackColor = True
    Me.et_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.et_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_riferim.Caption = "Riferim."
    Me.et_riferim.Enabled = True
    Me.et_riferim.FieldName = "et_riferim"
    Me.et_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_riferim.Name = "et_riferim"
    Me.et_riferim.NTSRepositoryComboBox = Nothing
    Me.et_riferim.NTSRepositoryItemCheck = Nothing
    Me.et_riferim.NTSRepositoryItemMemo = Nothing
    Me.et_riferim.NTSRepositoryItemText = Nothing
    Me.et_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_riferim.OptionsFilter.AllowFilter = False
    Me.et_riferim.Visible = True
    Me.et_riferim.VisibleIndex = 5
    '
    'et_conto
    '
    Me.et_conto.AppearanceCell.Options.UseBackColor = True
    Me.et_conto.AppearanceCell.Options.UseTextOptions = True
    Me.et_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_conto.Caption = "Conto"
    Me.et_conto.Enabled = True
    Me.et_conto.FieldName = "et_conto"
    Me.et_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_conto.Name = "et_conto"
    Me.et_conto.NTSRepositoryComboBox = Nothing
    Me.et_conto.NTSRepositoryItemCheck = Nothing
    Me.et_conto.NTSRepositoryItemMemo = Nothing
    Me.et_conto.NTSRepositoryItemText = Nothing
    Me.et_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_conto.OptionsFilter.AllowFilter = False
    Me.et_conto.Visible = True
    Me.et_conto.VisibleIndex = 6
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. conto"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 7
    '
    'et_coddest
    '
    Me.et_coddest.AppearanceCell.Options.UseBackColor = True
    Me.et_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.et_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_coddest.Caption = "Destin."
    Me.et_coddest.Enabled = True
    Me.et_coddest.FieldName = "et_coddest"
    Me.et_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_coddest.Name = "et_coddest"
    Me.et_coddest.NTSRepositoryComboBox = Nothing
    Me.et_coddest.NTSRepositoryItemCheck = Nothing
    Me.et_coddest.NTSRepositoryItemMemo = Nothing
    Me.et_coddest.NTSRepositoryItemText = Nothing
    Me.et_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_coddest.OptionsFilter.AllowFilter = False
    Me.et_coddest.Visible = True
    Me.et_coddest.VisibleIndex = 8
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destin."
    Me.xx_coddest.Enabled = True
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 9
    '
    'et_totdoc
    '
    Me.et_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.et_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.et_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_totdoc.Caption = "Tot. doc."
    Me.et_totdoc.Enabled = True
    Me.et_totdoc.FieldName = "et_totdoc"
    Me.et_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_totdoc.Name = "et_totdoc"
    Me.et_totdoc.NTSRepositoryComboBox = Nothing
    Me.et_totdoc.NTSRepositoryItemCheck = Nothing
    Me.et_totdoc.NTSRepositoryItemMemo = Nothing
    Me.et_totdoc.NTSRepositoryItemText = Nothing
    Me.et_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_totdoc.OptionsFilter.AllowFilter = False
    Me.et_totdoc.Visible = True
    Me.et_totdoc.VisibleIndex = 10
    '
    'et_totdocv
    '
    Me.et_totdocv.AppearanceCell.Options.UseBackColor = True
    Me.et_totdocv.AppearanceCell.Options.UseTextOptions = True
    Me.et_totdocv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_totdocv.Caption = "Tot. doc. Val."
    Me.et_totdocv.Enabled = True
    Me.et_totdocv.FieldName = "et_totdocv"
    Me.et_totdocv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_totdocv.Name = "et_totdocv"
    Me.et_totdocv.NTSRepositoryComboBox = Nothing
    Me.et_totdocv.NTSRepositoryItemCheck = Nothing
    Me.et_totdocv.NTSRepositoryItemMemo = Nothing
    Me.et_totdocv.NTSRepositoryItemText = Nothing
    Me.et_totdocv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_totdocv.OptionsFilter.AllowFilter = False
    Me.et_totdocv.Visible = True
    Me.et_totdocv.VisibleIndex = 11
    '
    'et_valuta
    '
    Me.et_valuta.AppearanceCell.Options.UseBackColor = True
    Me.et_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.et_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_valuta.Caption = "Valuta"
    Me.et_valuta.Enabled = True
    Me.et_valuta.FieldName = "et_valuta"
    Me.et_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_valuta.Name = "et_valuta"
    Me.et_valuta.NTSRepositoryComboBox = Nothing
    Me.et_valuta.NTSRepositoryItemCheck = Nothing
    Me.et_valuta.NTSRepositoryItemMemo = Nothing
    Me.et_valuta.NTSRepositoryItemText = Nothing
    Me.et_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_valuta.OptionsFilter.AllowFilter = False
    Me.et_valuta.Visible = True
    Me.et_valuta.VisibleIndex = 12
    '
    'xx_valuta
    '
    Me.xx_valuta.AppearanceCell.Options.UseBackColor = True
    Me.xx_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valuta.Caption = "Descr. valuta"
    Me.xx_valuta.Enabled = True
    Me.xx_valuta.FieldName = "xx_valuta"
    Me.xx_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valuta.Name = "xx_valuta"
    Me.xx_valuta.NTSRepositoryComboBox = Nothing
    Me.xx_valuta.NTSRepositoryItemCheck = Nothing
    Me.xx_valuta.NTSRepositoryItemMemo = Nothing
    Me.xx_valuta.NTSRepositoryItemText = Nothing
    Me.xx_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valuta.OptionsFilter.AllowFilter = False
    Me.xx_valuta.Visible = True
    Me.xx_valuta.VisibleIndex = 13
    '
    'et_cambio
    '
    Me.et_cambio.AppearanceCell.Options.UseBackColor = True
    Me.et_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.et_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_cambio.Caption = "Cambio"
    Me.et_cambio.Enabled = True
    Me.et_cambio.FieldName = "et_cambio"
    Me.et_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_cambio.Name = "et_cambio"
    Me.et_cambio.NTSRepositoryComboBox = Nothing
    Me.et_cambio.NTSRepositoryItemCheck = Nothing
    Me.et_cambio.NTSRepositoryItemMemo = Nothing
    Me.et_cambio.NTSRepositoryItemText = Nothing
    Me.et_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_cambio.OptionsFilter.AllowFilter = False
    Me.et_cambio.Visible = True
    Me.et_cambio.VisibleIndex = 14
    '
    'et_flevas
    '
    Me.et_flevas.AppearanceCell.Options.UseBackColor = True
    Me.et_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.et_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_flevas.Caption = "Evaso"
    Me.et_flevas.Enabled = True
    Me.et_flevas.FieldName = "et_flevas"
    Me.et_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_flevas.Name = "et_flevas"
    Me.et_flevas.NTSRepositoryComboBox = Nothing
    Me.et_flevas.NTSRepositoryItemCheck = Nothing
    Me.et_flevas.NTSRepositoryItemMemo = Nothing
    Me.et_flevas.NTSRepositoryItemText = Nothing
    Me.et_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_flevas.OptionsFilter.AllowFilter = False
    Me.et_flevas.Visible = True
    Me.et_flevas.VisibleIndex = 15
    '
    'et_rilasciato
    '
    Me.et_rilasciato.AppearanceCell.Options.UseBackColor = True
    Me.et_rilasciato.AppearanceCell.Options.UseTextOptions = True
    Me.et_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_rilasciato.Caption = "Rilasciato"
    Me.et_rilasciato.Enabled = True
    Me.et_rilasciato.FieldName = "et_rilasciato"
    Me.et_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_rilasciato.Name = "et_rilasciato"
    Me.et_rilasciato.NTSRepositoryComboBox = Nothing
    Me.et_rilasciato.NTSRepositoryItemCheck = Nothing
    Me.et_rilasciato.NTSRepositoryItemMemo = Nothing
    Me.et_rilasciato.NTSRepositoryItemText = Nothing
    Me.et_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_rilasciato.OptionsFilter.AllowFilter = False
    Me.et_rilasciato.Visible = True
    Me.et_rilasciato.VisibleIndex = 16
    '
    'et_confermato
    '
    Me.et_confermato.AppearanceCell.Options.UseBackColor = True
    Me.et_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.et_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_confermato.Caption = "Confermato"
    Me.et_confermato.Enabled = True
    Me.et_confermato.FieldName = "et_confermato"
    Me.et_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_confermato.Name = "et_confermato"
    Me.et_confermato.NTSRepositoryComboBox = Nothing
    Me.et_confermato.NTSRepositoryItemCheck = Nothing
    Me.et_confermato.NTSRepositoryItemMemo = Nothing
    Me.et_confermato.NTSRepositoryItemText = Nothing
    Me.et_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_confermato.OptionsFilter.AllowFilter = False
    Me.et_confermato.Visible = True
    Me.et_confermato.VisibleIndex = 17
    '
    'et_tipobf
    '
    Me.et_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.et_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.et_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_tipobf.Caption = "Tipo B/F"
    Me.et_tipobf.Enabled = True
    Me.et_tipobf.FieldName = "et_tipobf"
    Me.et_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_tipobf.Name = "et_tipobf"
    Me.et_tipobf.NTSRepositoryComboBox = Nothing
    Me.et_tipobf.NTSRepositoryItemCheck = Nothing
    Me.et_tipobf.NTSRepositoryItemMemo = Nothing
    Me.et_tipobf.NTSRepositoryItemText = Nothing
    Me.et_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_tipobf.OptionsFilter.AllowFilter = False
    Me.et_tipobf.Visible = True
    Me.et_tipobf.VisibleIndex = 18
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descr. tipo B/F"
    Me.xx_tipobf.Enabled = True
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipobf.OptionsFilter.AllowFilter = False
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 19
    '
    'et_causale
    '
    Me.et_causale.AppearanceCell.Options.UseBackColor = True
    Me.et_causale.AppearanceCell.Options.UseTextOptions = True
    Me.et_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_causale.Caption = "Causale"
    Me.et_causale.Enabled = True
    Me.et_causale.FieldName = "et_causale"
    Me.et_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_causale.Name = "et_causale"
    Me.et_causale.NTSRepositoryComboBox = Nothing
    Me.et_causale.NTSRepositoryItemCheck = Nothing
    Me.et_causale.NTSRepositoryItemMemo = Nothing
    Me.et_causale.NTSRepositoryItemText = Nothing
    Me.et_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_causale.OptionsFilter.AllowFilter = False
    Me.et_causale.Visible = True
    Me.et_causale.VisibleIndex = 20
    '
    'xx_causale
    '
    Me.xx_causale.AppearanceCell.Options.UseBackColor = True
    Me.xx_causale.AppearanceCell.Options.UseTextOptions = True
    Me.xx_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_causale.Caption = "Descr. causale"
    Me.xx_causale.Enabled = True
    Me.xx_causale.FieldName = "xx_causale"
    Me.xx_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_causale.Name = "xx_causale"
    Me.xx_causale.NTSRepositoryComboBox = Nothing
    Me.xx_causale.NTSRepositoryItemCheck = Nothing
    Me.xx_causale.NTSRepositoryItemMemo = Nothing
    Me.xx_causale.NTSRepositoryItemText = Nothing
    Me.xx_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_causale.OptionsFilter.AllowFilter = False
    Me.xx_causale.Visible = True
    Me.xx_causale.VisibleIndex = 21
    '
    'et_magaz
    '
    Me.et_magaz.AppearanceCell.Options.UseBackColor = True
    Me.et_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.et_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_magaz.Caption = "Magaz."
    Me.et_magaz.Enabled = True
    Me.et_magaz.FieldName = "et_magaz"
    Me.et_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_magaz.Name = "et_magaz"
    Me.et_magaz.NTSRepositoryComboBox = Nothing
    Me.et_magaz.NTSRepositoryItemCheck = Nothing
    Me.et_magaz.NTSRepositoryItemMemo = Nothing
    Me.et_magaz.NTSRepositoryItemText = Nothing
    Me.et_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_magaz.OptionsFilter.AllowFilter = False
    Me.et_magaz.Visible = True
    Me.et_magaz.VisibleIndex = 22
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Descr. magaz."
    Me.xx_magaz.Enabled = True
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz.OptionsFilter.AllowFilter = False
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 23
    '
    'et_magaz2
    '
    Me.et_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.et_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.et_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_magaz2.Caption = "Magaz. 2"
    Me.et_magaz2.Enabled = True
    Me.et_magaz2.FieldName = "et_magaz2"
    Me.et_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_magaz2.Name = "et_magaz2"
    Me.et_magaz2.NTSRepositoryComboBox = Nothing
    Me.et_magaz2.NTSRepositoryItemCheck = Nothing
    Me.et_magaz2.NTSRepositoryItemMemo = Nothing
    Me.et_magaz2.NTSRepositoryItemText = Nothing
    Me.et_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_magaz2.OptionsFilter.AllowFilter = False
    Me.et_magaz2.Visible = True
    Me.et_magaz2.VisibleIndex = 24
    '
    'xx_magaz2
    '
    Me.xx_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz2.Caption = "Descr. magaz. 2"
    Me.xx_magaz2.Enabled = True
    Me.xx_magaz2.FieldName = "xx_magaz2"
    Me.xx_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz2.Name = "xx_magaz2"
    Me.xx_magaz2.NTSRepositoryComboBox = Nothing
    Me.xx_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz2.NTSRepositoryItemText = Nothing
    Me.xx_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz2.OptionsFilter.AllowFilter = False
    Me.xx_magaz2.Visible = True
    Me.xx_magaz2.VisibleIndex = 25
    '
    'et_magimp
    '
    Me.et_magimp.AppearanceCell.Options.UseBackColor = True
    Me.et_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.et_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_magimp.Caption = "Mag. impegni"
    Me.et_magimp.Enabled = True
    Me.et_magimp.FieldName = "et_magimp"
    Me.et_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_magimp.Name = "et_magimp"
    Me.et_magimp.NTSRepositoryComboBox = Nothing
    Me.et_magimp.NTSRepositoryItemCheck = Nothing
    Me.et_magimp.NTSRepositoryItemMemo = Nothing
    Me.et_magimp.NTSRepositoryItemText = Nothing
    Me.et_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_magimp.OptionsFilter.AllowFilter = False
    Me.et_magimp.Visible = True
    Me.et_magimp.VisibleIndex = 26
    '
    'et_datcons
    '
    Me.et_datcons.AppearanceCell.Options.UseBackColor = True
    Me.et_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.et_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_datcons.Caption = "Data consegna"
    Me.et_datcons.Enabled = True
    Me.et_datcons.FieldName = "et_datcons"
    Me.et_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_datcons.Name = "et_datcons"
    Me.et_datcons.NTSRepositoryComboBox = Nothing
    Me.et_datcons.NTSRepositoryItemCheck = Nothing
    Me.et_datcons.NTSRepositoryItemMemo = Nothing
    Me.et_datcons.NTSRepositoryItemText = Nothing
    Me.et_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_datcons.OptionsFilter.AllowFilter = False
    Me.et_datcons.Visible = True
    Me.et_datcons.VisibleIndex = 27
    '
    'et_codagen
    '
    Me.et_codagen.AppearanceCell.Options.UseBackColor = True
    Me.et_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.et_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_codagen.Caption = "Agente"
    Me.et_codagen.Enabled = True
    Me.et_codagen.FieldName = "et_codagen"
    Me.et_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_codagen.Name = "et_codagen"
    Me.et_codagen.NTSRepositoryComboBox = Nothing
    Me.et_codagen.NTSRepositoryItemCheck = Nothing
    Me.et_codagen.NTSRepositoryItemMemo = Nothing
    Me.et_codagen.NTSRepositoryItemText = Nothing
    Me.et_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_codagen.OptionsFilter.AllowFilter = False
    Me.et_codagen.Visible = True
    Me.et_codagen.VisibleIndex = 28
    '
    'xx_codagen
    '
    Me.xx_codagen.AppearanceCell.Options.UseBackColor = True
    Me.xx_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codagen.Caption = "Descr. agente"
    Me.xx_codagen.Enabled = True
    Me.xx_codagen.FieldName = "xx_codagen"
    Me.xx_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codagen.Name = "xx_codagen"
    Me.xx_codagen.NTSRepositoryComboBox = Nothing
    Me.xx_codagen.NTSRepositoryItemCheck = Nothing
    Me.xx_codagen.NTSRepositoryItemMemo = Nothing
    Me.xx_codagen.NTSRepositoryItemText = Nothing
    Me.xx_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codagen.OptionsFilter.AllowFilter = False
    Me.xx_codagen.Visible = True
    Me.xx_codagen.VisibleIndex = 29
    '
    'et_listino
    '
    Me.et_listino.AppearanceCell.Options.UseBackColor = True
    Me.et_listino.AppearanceCell.Options.UseTextOptions = True
    Me.et_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_listino.Caption = "Listino"
    Me.et_listino.Enabled = True
    Me.et_listino.FieldName = "et_listino"
    Me.et_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_listino.Name = "et_listino"
    Me.et_listino.NTSRepositoryComboBox = Nothing
    Me.et_listino.NTSRepositoryItemCheck = Nothing
    Me.et_listino.NTSRepositoryItemMemo = Nothing
    Me.et_listino.NTSRepositoryItemText = Nothing
    Me.et_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_listino.OptionsFilter.AllowFilter = False
    Me.et_listino.Visible = True
    Me.et_listino.VisibleIndex = 30
    '
    'et_codese
    '
    Me.et_codese.AppearanceCell.Options.UseBackColor = True
    Me.et_codese.AppearanceCell.Options.UseTextOptions = True
    Me.et_codese.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_codese.Caption = "Cod. esenzione"
    Me.et_codese.Enabled = True
    Me.et_codese.FieldName = "et_codese"
    Me.et_codese.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_codese.Name = "et_codese"
    Me.et_codese.NTSRepositoryComboBox = Nothing
    Me.et_codese.NTSRepositoryItemCheck = Nothing
    Me.et_codese.NTSRepositoryItemMemo = Nothing
    Me.et_codese.NTSRepositoryItemText = Nothing
    Me.et_codese.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_codese.OptionsFilter.AllowFilter = False
    Me.et_codese.Visible = True
    Me.et_codese.VisibleIndex = 31
    '
    'xx_codese
    '
    Me.xx_codese.AppearanceCell.Options.UseBackColor = True
    Me.xx_codese.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codese.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codese.Caption = "Descr. esenzione"
    Me.xx_codese.Enabled = True
    Me.xx_codese.FieldName = "xx_codese"
    Me.xx_codese.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codese.Name = "xx_codese"
    Me.xx_codese.NTSRepositoryComboBox = Nothing
    Me.xx_codese.NTSRepositoryItemCheck = Nothing
    Me.xx_codese.NTSRepositoryItemMemo = Nothing
    Me.xx_codese.NTSRepositoryItemText = Nothing
    Me.xx_codese.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codese.OptionsFilter.AllowFilter = False
    Me.xx_codese.Visible = True
    Me.xx_codese.VisibleIndex = 32
    '
    'et_controp
    '
    Me.et_controp.AppearanceCell.Options.UseBackColor = True
    Me.et_controp.AppearanceCell.Options.UseTextOptions = True
    Me.et_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_controp.Caption = "Controp."
    Me.et_controp.Enabled = True
    Me.et_controp.FieldName = "et_controp"
    Me.et_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_controp.Name = "et_controp"
    Me.et_controp.NTSRepositoryComboBox = Nothing
    Me.et_controp.NTSRepositoryItemCheck = Nothing
    Me.et_controp.NTSRepositoryItemMemo = Nothing
    Me.et_controp.NTSRepositoryItemText = Nothing
    Me.et_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_controp.OptionsFilter.AllowFilter = False
    Me.et_controp.Visible = True
    Me.et_controp.VisibleIndex = 33
    '
    'xx_controp
    '
    Me.xx_controp.AppearanceCell.Options.UseBackColor = True
    Me.xx_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_controp.Caption = "Descr. controp."
    Me.xx_controp.Enabled = True
    Me.xx_controp.FieldName = "xx_controp"
    Me.xx_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_controp.Name = "xx_controp"
    Me.xx_controp.NTSRepositoryComboBox = Nothing
    Me.xx_controp.NTSRepositoryItemCheck = Nothing
    Me.xx_controp.NTSRepositoryItemMemo = Nothing
    Me.xx_controp.NTSRepositoryItemText = Nothing
    Me.xx_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_controp.OptionsFilter.AllowFilter = False
    Me.xx_controp.Visible = True
    Me.xx_controp.VisibleIndex = 34
    '
    'et_contfatt
    '
    Me.et_contfatt.AppearanceCell.Options.UseBackColor = True
    Me.et_contfatt.AppearanceCell.Options.UseTextOptions = True
    Me.et_contfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_contfatt.Caption = "Conto fatturaz."
    Me.et_contfatt.Enabled = True
    Me.et_contfatt.FieldName = "et_contfatt"
    Me.et_contfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_contfatt.Name = "et_contfatt"
    Me.et_contfatt.NTSRepositoryComboBox = Nothing
    Me.et_contfatt.NTSRepositoryItemCheck = Nothing
    Me.et_contfatt.NTSRepositoryItemMemo = Nothing
    Me.et_contfatt.NTSRepositoryItemText = Nothing
    Me.et_contfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_contfatt.OptionsFilter.AllowFilter = False
    Me.et_contfatt.Visible = True
    Me.et_contfatt.VisibleIndex = 35
    '
    'et_scont1
    '
    Me.et_scont1.AppearanceCell.Options.UseBackColor = True
    Me.et_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.et_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_scont1.Caption = "Sconto 1"
    Me.et_scont1.Enabled = True
    Me.et_scont1.FieldName = "et_scont1"
    Me.et_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_scont1.Name = "et_scont1"
    Me.et_scont1.NTSRepositoryComboBox = Nothing
    Me.et_scont1.NTSRepositoryItemCheck = Nothing
    Me.et_scont1.NTSRepositoryItemMemo = Nothing
    Me.et_scont1.NTSRepositoryItemText = Nothing
    Me.et_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_scont1.OptionsFilter.AllowFilter = False
    Me.et_scont1.Visible = True
    Me.et_scont1.VisibleIndex = 36
    '
    'et_scont2
    '
    Me.et_scont2.AppearanceCell.Options.UseBackColor = True
    Me.et_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.et_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_scont2.Caption = "Sconto 2"
    Me.et_scont2.Enabled = True
    Me.et_scont2.FieldName = "et_scont2"
    Me.et_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_scont2.Name = "et_scont2"
    Me.et_scont2.NTSRepositoryComboBox = Nothing
    Me.et_scont2.NTSRepositoryItemCheck = Nothing
    Me.et_scont2.NTSRepositoryItemMemo = Nothing
    Me.et_scont2.NTSRepositoryItemText = Nothing
    Me.et_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_scont2.OptionsFilter.AllowFilter = False
    Me.et_scont2.Visible = True
    Me.et_scont2.VisibleIndex = 37
    '
    'et_scopag
    '
    Me.et_scopag.AppearanceCell.Options.UseBackColor = True
    Me.et_scopag.AppearanceCell.Options.UseTextOptions = True
    Me.et_scopag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_scopag.Caption = "Sconto pag."
    Me.et_scopag.Enabled = True
    Me.et_scopag.FieldName = "et_scopag"
    Me.et_scopag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_scopag.Name = "et_scopag"
    Me.et_scopag.NTSRepositoryComboBox = Nothing
    Me.et_scopag.NTSRepositoryItemCheck = Nothing
    Me.et_scopag.NTSRepositoryItemMemo = Nothing
    Me.et_scopag.NTSRepositoryItemText = Nothing
    Me.et_scopag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_scopag.OptionsFilter.AllowFilter = False
    Me.et_scopag.Visible = True
    Me.et_scopag.VisibleIndex = 38
    '
    'et_codpaga
    '
    Me.et_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.et_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.et_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_codpaga.Caption = "Cod. pagam."
    Me.et_codpaga.Enabled = True
    Me.et_codpaga.FieldName = "et_codpaga"
    Me.et_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_codpaga.Name = "et_codpaga"
    Me.et_codpaga.NTSRepositoryComboBox = Nothing
    Me.et_codpaga.NTSRepositoryItemCheck = Nothing
    Me.et_codpaga.NTSRepositoryItemMemo = Nothing
    Me.et_codpaga.NTSRepositoryItemText = Nothing
    Me.et_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_codpaga.OptionsFilter.AllowFilter = False
    Me.et_codpaga.Visible = True
    Me.et_codpaga.VisibleIndex = 39
    '
    'xx_codpaga
    '
    Me.xx_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codpaga.Caption = "Descr. pagam."
    Me.xx_codpaga.Enabled = True
    Me.xx_codpaga.FieldName = "xx_codpaga"
    Me.xx_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpaga.Name = "xx_codpaga"
    Me.xx_codpaga.NTSRepositoryComboBox = Nothing
    Me.xx_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codpaga.NTSRepositoryItemMemo = Nothing
    Me.xx_codpaga.NTSRepositoryItemText = Nothing
    Me.xx_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpaga.OptionsFilter.AllowFilter = False
    Me.xx_codpaga.Visible = True
    Me.xx_codpaga.VisibleIndex = 40
    '
    'et_abi
    '
    Me.et_abi.AppearanceCell.Options.UseBackColor = True
    Me.et_abi.AppearanceCell.Options.UseTextOptions = True
    Me.et_abi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_abi.Caption = "Abi"
    Me.et_abi.Enabled = True
    Me.et_abi.FieldName = "et_abi"
    Me.et_abi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_abi.Name = "et_abi"
    Me.et_abi.NTSRepositoryComboBox = Nothing
    Me.et_abi.NTSRepositoryItemCheck = Nothing
    Me.et_abi.NTSRepositoryItemMemo = Nothing
    Me.et_abi.NTSRepositoryItemText = Nothing
    Me.et_abi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_abi.OptionsFilter.AllowFilter = False
    Me.et_abi.Visible = True
    Me.et_abi.VisibleIndex = 41
    '
    'et_cab
    '
    Me.et_cab.AppearanceCell.Options.UseBackColor = True
    Me.et_cab.AppearanceCell.Options.UseTextOptions = True
    Me.et_cab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_cab.Caption = "Cab"
    Me.et_cab.Enabled = True
    Me.et_cab.FieldName = "et_cab"
    Me.et_cab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_cab.Name = "et_cab"
    Me.et_cab.NTSRepositoryComboBox = Nothing
    Me.et_cab.NTSRepositoryItemCheck = Nothing
    Me.et_cab.NTSRepositoryItemMemo = Nothing
    Me.et_cab.NTSRepositoryItemText = Nothing
    Me.et_cab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_cab.OptionsFilter.AllowFilter = False
    Me.et_cab.Visible = True
    Me.et_cab.VisibleIndex = 42
    '
    'et_banc1
    '
    Me.et_banc1.AppearanceCell.Options.UseBackColor = True
    Me.et_banc1.AppearanceCell.Options.UseTextOptions = True
    Me.et_banc1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_banc1.Caption = "Banca"
    Me.et_banc1.Enabled = True
    Me.et_banc1.FieldName = "et_banc1"
    Me.et_banc1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_banc1.Name = "et_banc1"
    Me.et_banc1.NTSRepositoryComboBox = Nothing
    Me.et_banc1.NTSRepositoryItemCheck = Nothing
    Me.et_banc1.NTSRepositoryItemMemo = Nothing
    Me.et_banc1.NTSRepositoryItemText = Nothing
    Me.et_banc1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_banc1.OptionsFilter.AllowFilter = False
    Me.et_banc1.Visible = True
    Me.et_banc1.VisibleIndex = 43
    '
    'et_banc2
    '
    Me.et_banc2.AppearanceCell.Options.UseBackColor = True
    Me.et_banc2.AppearanceCell.Options.UseTextOptions = True
    Me.et_banc2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_banc2.Caption = "Filiale"
    Me.et_banc2.Enabled = True
    Me.et_banc2.FieldName = "et_banc2"
    Me.et_banc2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_banc2.Name = "et_banc2"
    Me.et_banc2.NTSRepositoryComboBox = Nothing
    Me.et_banc2.NTSRepositoryItemCheck = Nothing
    Me.et_banc2.NTSRepositoryItemMemo = Nothing
    Me.et_banc2.NTSRepositoryItemText = Nothing
    Me.et_banc2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_banc2.OptionsFilter.AllowFilter = False
    Me.et_banc2.Visible = True
    Me.et_banc2.VisibleIndex = 44
    '
    'et_numpar
    '
    Me.et_numpar.AppearanceCell.Options.UseBackColor = True
    Me.et_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.et_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_numpar.Caption = "Num. partita"
    Me.et_numpar.Enabled = True
    Me.et_numpar.FieldName = "et_numpar"
    Me.et_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_numpar.Name = "et_numpar"
    Me.et_numpar.NTSRepositoryComboBox = Nothing
    Me.et_numpar.NTSRepositoryItemCheck = Nothing
    Me.et_numpar.NTSRepositoryItemMemo = Nothing
    Me.et_numpar.NTSRepositoryItemText = Nothing
    Me.et_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_numpar.OptionsFilter.AllowFilter = False
    Me.et_numpar.Visible = True
    Me.et_numpar.VisibleIndex = 45
    '
    'et_datpar
    '
    Me.et_datpar.AppearanceCell.Options.UseBackColor = True
    Me.et_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.et_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_datpar.Caption = "Data partita"
    Me.et_datpar.Enabled = True
    Me.et_datpar.FieldName = "et_datpar"
    Me.et_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_datpar.Name = "et_datpar"
    Me.et_datpar.NTSRepositoryComboBox = Nothing
    Me.et_datpar.NTSRepositoryItemCheck = Nothing
    Me.et_datpar.NTSRepositoryItemMemo = Nothing
    Me.et_datpar.NTSRepositoryItemText = Nothing
    Me.et_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_datpar.OptionsFilter.AllowFilter = False
    Me.et_datpar.Visible = True
    Me.et_datpar.VisibleIndex = 46
    '
    'et_opnome
    '
    Me.et_opnome.AppearanceCell.Options.UseBackColor = True
    Me.et_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.et_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_opnome.Caption = "Operatore"
    Me.et_opnome.Enabled = True
    Me.et_opnome.FieldName = "et_opnome"
    Me.et_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_opnome.Name = "et_opnome"
    Me.et_opnome.NTSRepositoryComboBox = Nothing
    Me.et_opnome.NTSRepositoryItemCheck = Nothing
    Me.et_opnome.NTSRepositoryItemMemo = Nothing
    Me.et_opnome.NTSRepositoryItemText = Nothing
    Me.et_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_opnome.OptionsFilter.AllowFilter = False
    Me.et_opnome.Visible = True
    Me.et_opnome.VisibleIndex = 47
    '
    'et_note
    '
    Me.et_note.AppearanceCell.Options.UseBackColor = True
    Me.et_note.AppearanceCell.Options.UseTextOptions = True
    Me.et_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_note.Caption = "Note"
    Me.et_note.Enabled = True
    Me.et_note.FieldName = "et_note"
    Me.et_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_note.Name = "et_note"
    Me.et_note.NTSRepositoryComboBox = Nothing
    Me.et_note.NTSRepositoryItemCheck = Nothing
    Me.et_note.NTSRepositoryItemMemo = Nothing
    Me.et_note.NTSRepositoryItemText = Nothing
    Me.et_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_note.OptionsFilter.AllowFilter = False
    Me.et_note.Visible = True
    Me.et_note.VisibleIndex = 48
    '
    'et_opinc
    '
    Me.et_opinc.AppearanceCell.Options.UseBackColor = True
    Me.et_opinc.AppearanceCell.Options.UseTextOptions = True
    Me.et_opinc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_opinc.Caption = "Op. incaricato"
    Me.et_opinc.Enabled = True
    Me.et_opinc.FieldName = "et_opinc"
    Me.et_opinc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_opinc.Name = "et_opinc"
    Me.et_opinc.NTSRepositoryComboBox = Nothing
    Me.et_opinc.NTSRepositoryItemCheck = Nothing
    Me.et_opinc.NTSRepositoryItemMemo = Nothing
    Me.et_opinc.NTSRepositoryItemText = Nothing
    Me.et_opinc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_opinc.OptionsFilter.AllowFilter = False
    Me.et_opinc.Visible = True
    Me.et_opinc.VisibleIndex = 49
    '
    'et_coddest2
    '
    Me.et_coddest2.AppearanceCell.Options.UseBackColor = True
    Me.et_coddest2.AppearanceCell.Options.UseTextOptions = True
    Me.et_coddest2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_coddest2.Caption = "Destin. 2"
    Me.et_coddest2.Enabled = True
    Me.et_coddest2.FieldName = "et_coddest2"
    Me.et_coddest2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_coddest2.Name = "et_coddest2"
    Me.et_coddest2.NTSRepositoryComboBox = Nothing
    Me.et_coddest2.NTSRepositoryItemCheck = Nothing
    Me.et_coddest2.NTSRepositoryItemMemo = Nothing
    Me.et_coddest2.NTSRepositoryItemText = Nothing
    Me.et_coddest2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_coddest2.OptionsFilter.AllowFilter = False
    Me.et_coddest2.Visible = True
    Me.et_coddest2.VisibleIndex = 50
    '
    'et_oggetto
    '
    Me.et_oggetto.AppearanceCell.Options.UseBackColor = True
    Me.et_oggetto.AppearanceCell.Options.UseTextOptions = True
    Me.et_oggetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_oggetto.Caption = "Oggetto"
    Me.et_oggetto.Enabled = True
    Me.et_oggetto.FieldName = "et_oggetto"
    Me.et_oggetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_oggetto.Name = "et_oggetto"
    Me.et_oggetto.NTSRepositoryComboBox = Nothing
    Me.et_oggetto.NTSRepositoryItemCheck = Nothing
    Me.et_oggetto.NTSRepositoryItemMemo = Nothing
    Me.et_oggetto.NTSRepositoryItemText = Nothing
    Me.et_oggetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_oggetto.OptionsFilter.AllowFilter = False
    Me.et_oggetto.Visible = True
    Me.et_oggetto.VisibleIndex = 51
    '
    'et_vers
    '
    Me.et_vers.AppearanceCell.Options.UseBackColor = True
    Me.et_vers.AppearanceCell.Options.UseTextOptions = True
    Me.et_vers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_vers.Caption = "Vers."
    Me.et_vers.Enabled = True
    Me.et_vers.FieldName = "et_vers"
    Me.et_vers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_vers.Name = "et_vers"
    Me.et_vers.NTSRepositoryComboBox = Nothing
    Me.et_vers.NTSRepositoryItemCheck = Nothing
    Me.et_vers.NTSRepositoryItemMemo = Nothing
    Me.et_vers.NTSRepositoryItemText = Nothing
    Me.et_vers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_vers.OptionsFilter.AllowFilter = False
    Me.et_vers.Visible = True
    Me.et_vers.VisibleIndex = 52
    '
    'et_codlead
    '
    Me.et_codlead.AppearanceCell.Options.UseBackColor = True
    Me.et_codlead.AppearanceCell.Options.UseTextOptions = True
    Me.et_codlead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.et_codlead.Caption = "Lead"
    Me.et_codlead.Enabled = True
    Me.et_codlead.FieldName = "et_codlead"
    Me.et_codlead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.et_codlead.Name = "et_codlead"
    Me.et_codlead.NTSRepositoryComboBox = Nothing
    Me.et_codlead.NTSRepositoryItemCheck = Nothing
    Me.et_codlead.NTSRepositoryItemMemo = Nothing
    Me.et_codlead.NTSRepositoryItemText = Nothing
    Me.et_codlead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.et_codlead.OptionsFilter.AllowFilter = False
    Me.et_codlead.Visible = True
    Me.et_codlead.VisibleIndex = 53
    '
    'xx_codlead
    '
    Me.xx_codlead.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlead.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlead.Caption = "Descr. lead"
    Me.xx_codlead.Enabled = True
    Me.xx_codlead.FieldName = "xx_codlead"
    Me.xx_codlead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlead.Name = "xx_codlead"
    Me.xx_codlead.NTSRepositoryComboBox = Nothing
    Me.xx_codlead.NTSRepositoryItemCheck = Nothing
    Me.xx_codlead.NTSRepositoryItemMemo = Nothing
    Me.xx_codlead.NTSRepositoryItemText = Nothing
    Me.xx_codlead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlead.OptionsFilter.AllowFilter = False
    Me.xx_codlead.Visible = True
    Me.xx_codlead.VisibleIndex = 54
    '
    'NtsSplitter1
    '
    Me.NtsSplitter1.BackColor = System.Drawing.SystemColors.ActiveCaption
    Me.NtsSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.NtsSplitter1.Location = New System.Drawing.Point(0, 103)
    Me.NtsSplitter1.MinExtra = 50
    Me.NtsSplitter1.Name = "NtsSplitter1"
    Me.NtsSplitter1.Size = New System.Drawing.Size(782, 3)
    Me.NtsSplitter1.TabIndex = 1
    Me.NtsSplitter1.TabStop = False
    '
    'grCorpo
    '
    Me.grCorpo.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.grCorpo.EmbeddedNavigator.Name = ""
    Me.grCorpo.Location = New System.Drawing.Point(0, 106)
    Me.grCorpo.MainView = Me.grvCorpo
    Me.grCorpo.Name = "grCorpo"
    Me.grCorpo.Size = New System.Drawing.Size(782, 103)
    Me.grCorpo.TabIndex = 0
    Me.grCorpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCorpo})
    '
    'grvCorpo
    '
    Me.grvCorpo.ActiveFilterEnabled = False
    Me.grvCorpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_tipork, Me.ec_anno, Me.ec_serie, Me.ec_numdoc, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_ump, Me.ec_quant, Me.ec_prezzo, Me.ec_prezvalc, Me.ec_preziva, Me.ec_valore, Me.ec_scont1, Me.ec_scont2, Me.ec_scont3, Me.ec_scont4, Me.ec_scont5, Me.ec_scont6, Me.ec_note, Me.ec_datcons, Me.ec_magaz, Me.xxo_magaz, Me.ec_magaz2, Me.xxo_magaz2, Me.ec_quaeva, Me.ec_quapre, Me.ec_flevas, Me.ec_flevapre, Me.ec_provv, Me.ec_vprovv, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_stasino, Me.ec_prelist, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.ec_codcena, Me.xxo_codcena, Me.ec_confermato, Me.ec_rilasciato, Me.ec_aperto, Me.xx_lottox, Me.ec_ubicaz, Me.ec_causale, Me.xxo_causale, Me.ec_causale2, Me.ec_fase, Me.xxo_fase, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.ec_datini, Me.ec_datfin, Me.ec_ortipo, Me.ec_oranno, Me.ec_orserie, Me.ec_ornum, Me.ec_orriga, Me.ec_salcon, Me.ec_nptipo, Me.ec_npanno, Me.ec_npserie, Me.ec_npnum, Me.ec_npvers, Me.ec_npriga, Me.ec_pnsalcon, Me.ec_vers, Me.xxo_conto})
    Me.grvCorpo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCorpo.Enabled = True
    Me.grvCorpo.GridControl = Me.grCorpo
    Me.grvCorpo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCorpo.Name = "grvCorpo"
    Me.grvCorpo.NTSAllowDelete = True
    Me.grvCorpo.NTSAllowInsert = True
    Me.grvCorpo.NTSAllowUpdate = True
    Me.grvCorpo.NTSMenuContext = Nothing
    Me.grvCorpo.OptionsCustomization.AllowRowSizing = True
    Me.grvCorpo.OptionsFilter.AllowFilterEditor = False
    Me.grvCorpo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCorpo.OptionsNavigation.UseTabKey = False
    Me.grvCorpo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCorpo.OptionsView.ColumnAutoWidth = False
    Me.grvCorpo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCorpo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCorpo.OptionsView.ShowGroupPanel = False
    Me.grvCorpo.RowHeight = 14
    '
    'ec_tipork
    '
    Me.ec_tipork.AppearanceCell.Options.UseBackColor = True
    Me.ec_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.ec_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_tipork.Caption = "Tipo"
    Me.ec_tipork.Enabled = True
    Me.ec_tipork.FieldName = "ec_tipork"
    Me.ec_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_tipork.Name = "ec_tipork"
    Me.ec_tipork.NTSRepositoryComboBox = Nothing
    Me.ec_tipork.NTSRepositoryItemCheck = Nothing
    Me.ec_tipork.NTSRepositoryItemMemo = Nothing
    Me.ec_tipork.NTSRepositoryItemText = Nothing
    Me.ec_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_tipork.OptionsFilter.AllowFilter = False
    Me.ec_tipork.Visible = True
    Me.ec_tipork.VisibleIndex = 0
    Me.ec_tipork.Width = 70
    '
    'ec_anno
    '
    Me.ec_anno.AppearanceCell.Options.UseBackColor = True
    Me.ec_anno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_anno.Caption = "Anno"
    Me.ec_anno.Enabled = True
    Me.ec_anno.FieldName = "ec_anno"
    Me.ec_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_anno.Name = "ec_anno"
    Me.ec_anno.NTSRepositoryComboBox = Nothing
    Me.ec_anno.NTSRepositoryItemCheck = Nothing
    Me.ec_anno.NTSRepositoryItemMemo = Nothing
    Me.ec_anno.NTSRepositoryItemText = Nothing
    Me.ec_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_anno.OptionsFilter.AllowFilter = False
    Me.ec_anno.Visible = True
    Me.ec_anno.VisibleIndex = 1
    Me.ec_anno.Width = 70
    '
    'ec_serie
    '
    Me.ec_serie.AppearanceCell.Options.UseBackColor = True
    Me.ec_serie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_serie.Caption = "Serie"
    Me.ec_serie.Enabled = True
    Me.ec_serie.FieldName = "ec_serie"
    Me.ec_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_serie.Name = "ec_serie"
    Me.ec_serie.NTSRepositoryComboBox = Nothing
    Me.ec_serie.NTSRepositoryItemCheck = Nothing
    Me.ec_serie.NTSRepositoryItemMemo = Nothing
    Me.ec_serie.NTSRepositoryItemText = Nothing
    Me.ec_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_serie.OptionsFilter.AllowFilter = False
    Me.ec_serie.Visible = True
    Me.ec_serie.VisibleIndex = 2
    '
    'ec_numdoc
    '
    Me.ec_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.ec_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.ec_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_numdoc.Caption = "Numero"
    Me.ec_numdoc.Enabled = True
    Me.ec_numdoc.FieldName = "ec_numdoc"
    Me.ec_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_numdoc.Name = "ec_numdoc"
    Me.ec_numdoc.NTSRepositoryComboBox = Nothing
    Me.ec_numdoc.NTSRepositoryItemCheck = Nothing
    Me.ec_numdoc.NTSRepositoryItemMemo = Nothing
    Me.ec_numdoc.NTSRepositoryItemText = Nothing
    Me.ec_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_numdoc.OptionsFilter.AllowFilter = False
    Me.ec_numdoc.Visible = True
    Me.ec_numdoc.VisibleIndex = 3
    '
    'ec_codart
    '
    Me.ec_codart.AppearanceCell.Options.UseBackColor = True
    Me.ec_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codart.Caption = "Articolo"
    Me.ec_codart.Enabled = True
    Me.ec_codart.FieldName = "ec_codart"
    Me.ec_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codart.Name = "ec_codart"
    Me.ec_codart.NTSRepositoryComboBox = Nothing
    Me.ec_codart.NTSRepositoryItemCheck = Nothing
    Me.ec_codart.NTSRepositoryItemMemo = Nothing
    Me.ec_codart.NTSRepositoryItemText = Nothing
    Me.ec_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codart.OptionsFilter.AllowFilter = False
    Me.ec_codart.Visible = True
    Me.ec_codart.VisibleIndex = 4
    '
    'ec_descr
    '
    Me.ec_descr.AppearanceCell.Options.UseBackColor = True
    Me.ec_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_descr.Caption = "Descr. articolo"
    Me.ec_descr.Enabled = True
    Me.ec_descr.FieldName = "ec_descr"
    Me.ec_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_descr.Name = "ec_descr"
    Me.ec_descr.NTSRepositoryComboBox = Nothing
    Me.ec_descr.NTSRepositoryItemCheck = Nothing
    Me.ec_descr.NTSRepositoryItemMemo = Nothing
    Me.ec_descr.NTSRepositoryItemText = Nothing
    Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_descr.OptionsFilter.AllowFilter = False
    Me.ec_descr.Visible = True
    Me.ec_descr.VisibleIndex = 5
    '
    'ec_desint
    '
    Me.ec_desint.AppearanceCell.Options.UseBackColor = True
    Me.ec_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ec_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_desint.Caption = "Descr. int. art."
    Me.ec_desint.Enabled = True
    Me.ec_desint.FieldName = "ec_desint"
    Me.ec_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_desint.Name = "ec_desint"
    Me.ec_desint.NTSRepositoryComboBox = Nothing
    Me.ec_desint.NTSRepositoryItemCheck = Nothing
    Me.ec_desint.NTSRepositoryItemMemo = Nothing
    Me.ec_desint.NTSRepositoryItemText = Nothing
    Me.ec_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_desint.OptionsFilter.AllowFilter = False
    Me.ec_desint.Visible = True
    Me.ec_desint.VisibleIndex = 6
    '
    'ec_unmis
    '
    Me.ec_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ec_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_unmis.Caption = "U.m."
    Me.ec_unmis.Enabled = True
    Me.ec_unmis.FieldName = "ec_unmis"
    Me.ec_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_unmis.Name = "ec_unmis"
    Me.ec_unmis.NTSRepositoryComboBox = Nothing
    Me.ec_unmis.NTSRepositoryItemCheck = Nothing
    Me.ec_unmis.NTSRepositoryItemMemo = Nothing
    Me.ec_unmis.NTSRepositoryItemText = Nothing
    Me.ec_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_unmis.OptionsFilter.AllowFilter = False
    Me.ec_unmis.Visible = True
    Me.ec_unmis.VisibleIndex = 7
    '
    'ec_colli
    '
    Me.ec_colli.AppearanceCell.Options.UseBackColor = True
    Me.ec_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_colli.Caption = "Colli"
    Me.ec_colli.Enabled = True
    Me.ec_colli.FieldName = "ec_colli"
    Me.ec_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_colli.Name = "ec_colli"
    Me.ec_colli.NTSRepositoryComboBox = Nothing
    Me.ec_colli.NTSRepositoryItemCheck = Nothing
    Me.ec_colli.NTSRepositoryItemMemo = Nothing
    Me.ec_colli.NTSRepositoryItemText = Nothing
    Me.ec_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_colli.OptionsFilter.AllowFilter = False
    Me.ec_colli.Visible = True
    Me.ec_colli.VisibleIndex = 8
    '
    'ec_ump
    '
    Me.ec_ump.AppearanceCell.Options.UseBackColor = True
    Me.ec_ump.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ump.Caption = "UMP"
    Me.ec_ump.Enabled = True
    Me.ec_ump.FieldName = "ec_ump"
    Me.ec_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ump.Name = "ec_ump"
    Me.ec_ump.NTSRepositoryComboBox = Nothing
    Me.ec_ump.NTSRepositoryItemCheck = Nothing
    Me.ec_ump.NTSRepositoryItemMemo = Nothing
    Me.ec_ump.NTSRepositoryItemText = Nothing
    Me.ec_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ump.OptionsFilter.AllowFilter = False
    Me.ec_ump.Visible = True
    Me.ec_ump.VisibleIndex = 9
    '
    'ec_quant
    '
    Me.ec_quant.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant.Caption = "Quantità"
    Me.ec_quant.Enabled = True
    Me.ec_quant.FieldName = "ec_quant"
    Me.ec_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant.Name = "ec_quant"
    Me.ec_quant.NTSRepositoryComboBox = Nothing
    Me.ec_quant.NTSRepositoryItemCheck = Nothing
    Me.ec_quant.NTSRepositoryItemMemo = Nothing
    Me.ec_quant.NTSRepositoryItemText = Nothing
    Me.ec_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant.OptionsFilter.AllowFilter = False
    Me.ec_quant.Visible = True
    Me.ec_quant.VisibleIndex = 10
    '
    'ec_prezzo
    '
    Me.ec_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezzo.Caption = "Prezzo"
    Me.ec_prezzo.Enabled = True
    Me.ec_prezzo.FieldName = "ec_prezzo"
    Me.ec_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezzo.Name = "ec_prezzo"
    Me.ec_prezzo.NTSRepositoryComboBox = Nothing
    Me.ec_prezzo.NTSRepositoryItemCheck = Nothing
    Me.ec_prezzo.NTSRepositoryItemMemo = Nothing
    Me.ec_prezzo.NTSRepositoryItemText = Nothing
    Me.ec_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezzo.OptionsFilter.AllowFilter = False
    Me.ec_prezzo.Visible = True
    Me.ec_prezzo.VisibleIndex = 11
    '
    'ec_prezvalc
    '
    Me.ec_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezvalc.Caption = "Prezzo val."
    Me.ec_prezvalc.Enabled = True
    Me.ec_prezvalc.FieldName = "ec_prezvalc"
    Me.ec_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezvalc.Name = "ec_prezvalc"
    Me.ec_prezvalc.NTSRepositoryComboBox = Nothing
    Me.ec_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.ec_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.ec_prezvalc.NTSRepositoryItemText = Nothing
    Me.ec_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezvalc.OptionsFilter.AllowFilter = False
    Me.ec_prezvalc.Visible = True
    Me.ec_prezvalc.VisibleIndex = 12
    '
    'ec_preziva
    '
    Me.ec_preziva.AppearanceCell.Options.UseBackColor = True
    Me.ec_preziva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_preziva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_preziva.Caption = "Prezzo c/IVA"
    Me.ec_preziva.Enabled = True
    Me.ec_preziva.FieldName = "ec_preziva"
    Me.ec_preziva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_preziva.Name = "ec_preziva"
    Me.ec_preziva.NTSRepositoryComboBox = Nothing
    Me.ec_preziva.NTSRepositoryItemCheck = Nothing
    Me.ec_preziva.NTSRepositoryItemMemo = Nothing
    Me.ec_preziva.NTSRepositoryItemText = Nothing
    Me.ec_preziva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_preziva.OptionsFilter.AllowFilter = False
    Me.ec_preziva.Visible = True
    Me.ec_preziva.VisibleIndex = 13
    '
    'ec_valore
    '
    Me.ec_valore.AppearanceCell.Options.UseBackColor = True
    Me.ec_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valore.Caption = "Valore"
    Me.ec_valore.Enabled = True
    Me.ec_valore.FieldName = "ec_valore"
    Me.ec_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valore.Name = "ec_valore"
    Me.ec_valore.NTSRepositoryComboBox = Nothing
    Me.ec_valore.NTSRepositoryItemCheck = Nothing
    Me.ec_valore.NTSRepositoryItemMemo = Nothing
    Me.ec_valore.NTSRepositoryItemText = Nothing
    Me.ec_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valore.OptionsFilter.AllowFilter = False
    Me.ec_valore.Visible = True
    Me.ec_valore.VisibleIndex = 14
    '
    'ec_scont1
    '
    Me.ec_scont1.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont1.Caption = "Sconto 1"
    Me.ec_scont1.Enabled = True
    Me.ec_scont1.FieldName = "ec_scont1"
    Me.ec_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont1.Name = "ec_scont1"
    Me.ec_scont1.NTSRepositoryComboBox = Nothing
    Me.ec_scont1.NTSRepositoryItemCheck = Nothing
    Me.ec_scont1.NTSRepositoryItemMemo = Nothing
    Me.ec_scont1.NTSRepositoryItemText = Nothing
    Me.ec_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont1.OptionsFilter.AllowFilter = False
    Me.ec_scont1.Visible = True
    Me.ec_scont1.VisibleIndex = 15
    '
    'ec_scont2
    '
    Me.ec_scont2.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont2.Caption = "Sconto 2"
    Me.ec_scont2.Enabled = True
    Me.ec_scont2.FieldName = "ec_scont2"
    Me.ec_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont2.Name = "ec_scont2"
    Me.ec_scont2.NTSRepositoryComboBox = Nothing
    Me.ec_scont2.NTSRepositoryItemCheck = Nothing
    Me.ec_scont2.NTSRepositoryItemMemo = Nothing
    Me.ec_scont2.NTSRepositoryItemText = Nothing
    Me.ec_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont2.OptionsFilter.AllowFilter = False
    Me.ec_scont2.Visible = True
    Me.ec_scont2.VisibleIndex = 16
    '
    'ec_scont3
    '
    Me.ec_scont3.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont3.Caption = "Sconto 3"
    Me.ec_scont3.Enabled = True
    Me.ec_scont3.FieldName = "ec_scont3"
    Me.ec_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont3.Name = "ec_scont3"
    Me.ec_scont3.NTSRepositoryComboBox = Nothing
    Me.ec_scont3.NTSRepositoryItemCheck = Nothing
    Me.ec_scont3.NTSRepositoryItemMemo = Nothing
    Me.ec_scont3.NTSRepositoryItemText = Nothing
    Me.ec_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont3.OptionsFilter.AllowFilter = False
    Me.ec_scont3.Visible = True
    Me.ec_scont3.VisibleIndex = 17
    '
    'ec_scont4
    '
    Me.ec_scont4.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont4.Caption = "Sconto 4"
    Me.ec_scont4.Enabled = True
    Me.ec_scont4.FieldName = "ec_scont4"
    Me.ec_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont4.Name = "ec_scont4"
    Me.ec_scont4.NTSRepositoryComboBox = Nothing
    Me.ec_scont4.NTSRepositoryItemCheck = Nothing
    Me.ec_scont4.NTSRepositoryItemMemo = Nothing
    Me.ec_scont4.NTSRepositoryItemText = Nothing
    Me.ec_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont4.OptionsFilter.AllowFilter = False
    Me.ec_scont4.Visible = True
    Me.ec_scont4.VisibleIndex = 18
    '
    'ec_scont5
    '
    Me.ec_scont5.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont5.Caption = "Sconto 5"
    Me.ec_scont5.Enabled = True
    Me.ec_scont5.FieldName = "ec_scont5"
    Me.ec_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont5.Name = "ec_scont5"
    Me.ec_scont5.NTSRepositoryComboBox = Nothing
    Me.ec_scont5.NTSRepositoryItemCheck = Nothing
    Me.ec_scont5.NTSRepositoryItemMemo = Nothing
    Me.ec_scont5.NTSRepositoryItemText = Nothing
    Me.ec_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont5.OptionsFilter.AllowFilter = False
    Me.ec_scont5.Visible = True
    Me.ec_scont5.VisibleIndex = 19
    '
    'ec_scont6
    '
    Me.ec_scont6.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont6.Caption = "Sconto 6"
    Me.ec_scont6.Enabled = True
    Me.ec_scont6.FieldName = "ec_scont6"
    Me.ec_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont6.Name = "ec_scont6"
    Me.ec_scont6.NTSRepositoryComboBox = Nothing
    Me.ec_scont6.NTSRepositoryItemCheck = Nothing
    Me.ec_scont6.NTSRepositoryItemMemo = Nothing
    Me.ec_scont6.NTSRepositoryItemText = Nothing
    Me.ec_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont6.OptionsFilter.AllowFilter = False
    Me.ec_scont6.Visible = True
    Me.ec_scont6.VisibleIndex = 20
    '
    'ec_note
    '
    Me.ec_note.AppearanceCell.Options.UseBackColor = True
    Me.ec_note.AppearanceCell.Options.UseTextOptions = True
    Me.ec_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_note.Caption = "Note"
    Me.ec_note.Enabled = True
    Me.ec_note.FieldName = "ec_note"
    Me.ec_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_note.Name = "ec_note"
    Me.ec_note.NTSRepositoryComboBox = Nothing
    Me.ec_note.NTSRepositoryItemCheck = Nothing
    Me.ec_note.NTSRepositoryItemMemo = Nothing
    Me.ec_note.NTSRepositoryItemText = Nothing
    Me.ec_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_note.OptionsFilter.AllowFilter = False
    Me.ec_note.Visible = True
    Me.ec_note.VisibleIndex = 21
    '
    'ec_datcons
    '
    Me.ec_datcons.AppearanceCell.Options.UseBackColor = True
    Me.ec_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datcons.Caption = "Data cons."
    Me.ec_datcons.Enabled = True
    Me.ec_datcons.FieldName = "ec_datcons"
    Me.ec_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datcons.Name = "ec_datcons"
    Me.ec_datcons.NTSRepositoryComboBox = Nothing
    Me.ec_datcons.NTSRepositoryItemCheck = Nothing
    Me.ec_datcons.NTSRepositoryItemMemo = Nothing
    Me.ec_datcons.NTSRepositoryItemText = Nothing
    Me.ec_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datcons.OptionsFilter.AllowFilter = False
    Me.ec_datcons.Visible = True
    Me.ec_datcons.VisibleIndex = 22
    '
    'ec_magaz
    '
    Me.ec_magaz.AppearanceCell.Options.UseBackColor = True
    Me.ec_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magaz.Caption = "Magaz."
    Me.ec_magaz.Enabled = True
    Me.ec_magaz.FieldName = "ec_magaz"
    Me.ec_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magaz.Name = "ec_magaz"
    Me.ec_magaz.NTSRepositoryComboBox = Nothing
    Me.ec_magaz.NTSRepositoryItemCheck = Nothing
    Me.ec_magaz.NTSRepositoryItemMemo = Nothing
    Me.ec_magaz.NTSRepositoryItemText = Nothing
    Me.ec_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magaz.OptionsFilter.AllowFilter = False
    Me.ec_magaz.Visible = True
    Me.ec_magaz.VisibleIndex = 23
    '
    'xxo_magaz
    '
    Me.xxo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magaz.Caption = "Descr. magaz."
    Me.xxo_magaz.Enabled = True
    Me.xxo_magaz.FieldName = "xx_magaz"
    Me.xxo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magaz.Name = "xxo_magaz"
    Me.xxo_magaz.NTSRepositoryComboBox = Nothing
    Me.xxo_magaz.NTSRepositoryItemCheck = Nothing
    Me.xxo_magaz.NTSRepositoryItemMemo = Nothing
    Me.xxo_magaz.NTSRepositoryItemText = Nothing
    Me.xxo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magaz.OptionsFilter.AllowFilter = False
    Me.xxo_magaz.Visible = True
    Me.xxo_magaz.VisibleIndex = 24
    '
    'ec_magaz2
    '
    Me.ec_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.ec_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magaz2.Caption = "Magaz.2"
    Me.ec_magaz2.Enabled = True
    Me.ec_magaz2.FieldName = "ec_magaz2"
    Me.ec_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magaz2.Name = "ec_magaz2"
    Me.ec_magaz2.NTSRepositoryComboBox = Nothing
    Me.ec_magaz2.NTSRepositoryItemCheck = Nothing
    Me.ec_magaz2.NTSRepositoryItemMemo = Nothing
    Me.ec_magaz2.NTSRepositoryItemText = Nothing
    Me.ec_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magaz2.OptionsFilter.AllowFilter = False
    Me.ec_magaz2.Visible = True
    Me.ec_magaz2.VisibleIndex = 25
    '
    'xxo_magaz2
    '
    Me.xxo_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magaz2.Caption = "Descr. magaz.2"
    Me.xxo_magaz2.Enabled = True
    Me.xxo_magaz2.FieldName = "xxo_magaz2"
    Me.xxo_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magaz2.Name = "xxo_magaz2"
    Me.xxo_magaz2.NTSRepositoryComboBox = Nothing
    Me.xxo_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xxo_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xxo_magaz2.NTSRepositoryItemText = Nothing
    Me.xxo_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magaz2.OptionsFilter.AllowFilter = False
    Me.xxo_magaz2.Visible = True
    Me.xxo_magaz2.VisibleIndex = 26
    '
    'ec_quaeva
    '
    Me.ec_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.ec_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quaeva.Caption = "Qta evasa"
    Me.ec_quaeva.Enabled = True
    Me.ec_quaeva.FieldName = "ec_quaeva"
    Me.ec_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quaeva.Name = "ec_quaeva"
    Me.ec_quaeva.NTSRepositoryComboBox = Nothing
    Me.ec_quaeva.NTSRepositoryItemCheck = Nothing
    Me.ec_quaeva.NTSRepositoryItemMemo = Nothing
    Me.ec_quaeva.NTSRepositoryItemText = Nothing
    Me.ec_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quaeva.OptionsFilter.AllowFilter = False
    Me.ec_quaeva.Visible = True
    Me.ec_quaeva.VisibleIndex = 27
    '
    'ec_quapre
    '
    Me.ec_quapre.AppearanceCell.Options.UseBackColor = True
    Me.ec_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quapre.Caption = "Qta prenot."
    Me.ec_quapre.Enabled = True
    Me.ec_quapre.FieldName = "ec_quapre"
    Me.ec_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quapre.Name = "ec_quapre"
    Me.ec_quapre.NTSRepositoryComboBox = Nothing
    Me.ec_quapre.NTSRepositoryItemCheck = Nothing
    Me.ec_quapre.NTSRepositoryItemMemo = Nothing
    Me.ec_quapre.NTSRepositoryItemText = Nothing
    Me.ec_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quapre.OptionsFilter.AllowFilter = False
    Me.ec_quapre.Visible = True
    Me.ec_quapre.VisibleIndex = 28
    '
    'ec_flevas
    '
    Me.ec_flevas.AppearanceCell.Options.UseBackColor = True
    Me.ec_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flevas.Caption = "Evaso"
    Me.ec_flevas.Enabled = True
    Me.ec_flevas.FieldName = "ec_flevas"
    Me.ec_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flevas.Name = "ec_flevas"
    Me.ec_flevas.NTSRepositoryComboBox = Nothing
    Me.ec_flevas.NTSRepositoryItemCheck = Nothing
    Me.ec_flevas.NTSRepositoryItemMemo = Nothing
    Me.ec_flevas.NTSRepositoryItemText = Nothing
    Me.ec_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flevas.OptionsFilter.AllowFilter = False
    Me.ec_flevas.Visible = True
    Me.ec_flevas.VisibleIndex = 29
    '
    'ec_flevapre
    '
    Me.ec_flevapre.AppearanceCell.Options.UseBackColor = True
    Me.ec_flevapre.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flevapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flevapre.Caption = "NP evasa"
    Me.ec_flevapre.Enabled = True
    Me.ec_flevapre.FieldName = "ec_flevapre"
    Me.ec_flevapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flevapre.Name = "ec_flevapre"
    Me.ec_flevapre.NTSRepositoryComboBox = Nothing
    Me.ec_flevapre.NTSRepositoryItemCheck = Nothing
    Me.ec_flevapre.NTSRepositoryItemMemo = Nothing
    Me.ec_flevapre.NTSRepositoryItemText = Nothing
    Me.ec_flevapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flevapre.OptionsFilter.AllowFilter = False
    Me.ec_flevapre.Visible = True
    Me.ec_flevapre.VisibleIndex = 30
    '
    'ec_provv
    '
    Me.ec_provv.AppearanceCell.Options.UseBackColor = True
    Me.ec_provv.AppearanceCell.Options.UseTextOptions = True
    Me.ec_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_provv.Caption = "Provv."
    Me.ec_provv.Enabled = True
    Me.ec_provv.FieldName = "ec_provv"
    Me.ec_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_provv.Name = "ec_provv"
    Me.ec_provv.NTSRepositoryComboBox = Nothing
    Me.ec_provv.NTSRepositoryItemCheck = Nothing
    Me.ec_provv.NTSRepositoryItemMemo = Nothing
    Me.ec_provv.NTSRepositoryItemText = Nothing
    Me.ec_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_provv.OptionsFilter.AllowFilter = False
    Me.ec_provv.Visible = True
    Me.ec_provv.VisibleIndex = 31
    '
    'ec_vprovv
    '
    Me.ec_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.ec_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.ec_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_vprovv.Caption = "Provv. valore"
    Me.ec_vprovv.Enabled = True
    Me.ec_vprovv.FieldName = "ec_vprovv"
    Me.ec_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_vprovv.Name = "ec_vprovv"
    Me.ec_vprovv.NTSRepositoryComboBox = Nothing
    Me.ec_vprovv.NTSRepositoryItemCheck = Nothing
    Me.ec_vprovv.NTSRepositoryItemMemo = Nothing
    Me.ec_vprovv.NTSRepositoryItemText = Nothing
    Me.ec_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_vprovv.OptionsFilter.AllowFilter = False
    Me.ec_vprovv.Visible = True
    Me.ec_vprovv.VisibleIndex = 32
    '
    'ec_controp
    '
    Me.ec_controp.AppearanceCell.Options.UseBackColor = True
    Me.ec_controp.AppearanceCell.Options.UseTextOptions = True
    Me.ec_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_controp.Caption = "Controp."
    Me.ec_controp.Enabled = True
    Me.ec_controp.FieldName = "ec_controp"
    Me.ec_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_controp.Name = "ec_controp"
    Me.ec_controp.NTSRepositoryComboBox = Nothing
    Me.ec_controp.NTSRepositoryItemCheck = Nothing
    Me.ec_controp.NTSRepositoryItemMemo = Nothing
    Me.ec_controp.NTSRepositoryItemText = Nothing
    Me.ec_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_controp.OptionsFilter.AllowFilter = False
    Me.ec_controp.Visible = True
    Me.ec_controp.VisibleIndex = 33
    '
    'xxo_controp
    '
    Me.xxo_controp.AppearanceCell.Options.UseBackColor = True
    Me.xxo_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_controp.Caption = "Descr. controp."
    Me.xxo_controp.Enabled = True
    Me.xxo_controp.FieldName = "xxo_controp"
    Me.xxo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_controp.Name = "xxo_controp"
    Me.xxo_controp.NTSRepositoryComboBox = Nothing
    Me.xxo_controp.NTSRepositoryItemCheck = Nothing
    Me.xxo_controp.NTSRepositoryItemMemo = Nothing
    Me.xxo_controp.NTSRepositoryItemText = Nothing
    Me.xxo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_controp.OptionsFilter.AllowFilter = False
    Me.xxo_controp.Visible = True
    Me.xxo_controp.VisibleIndex = 34
    '
    'ec_codiva
    '
    Me.ec_codiva.AppearanceCell.Options.UseBackColor = True
    Me.ec_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codiva.Caption = "Cod. IVA"
    Me.ec_codiva.Enabled = True
    Me.ec_codiva.FieldName = "ec_codiva"
    Me.ec_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codiva.Name = "ec_codiva"
    Me.ec_codiva.NTSRepositoryComboBox = Nothing
    Me.ec_codiva.NTSRepositoryItemCheck = Nothing
    Me.ec_codiva.NTSRepositoryItemMemo = Nothing
    Me.ec_codiva.NTSRepositoryItemText = Nothing
    Me.ec_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codiva.OptionsFilter.AllowFilter = False
    Me.ec_codiva.Visible = True
    Me.ec_codiva.VisibleIndex = 35
    '
    'xxo_codiva
    '
    Me.xxo_codiva.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codiva.Caption = "Descr. IVA"
    Me.xxo_codiva.Enabled = True
    Me.xxo_codiva.FieldName = "xxo_codiva"
    Me.xxo_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codiva.Name = "xxo_codiva"
    Me.xxo_codiva.NTSRepositoryComboBox = Nothing
    Me.xxo_codiva.NTSRepositoryItemCheck = Nothing
    Me.xxo_codiva.NTSRepositoryItemMemo = Nothing
    Me.xxo_codiva.NTSRepositoryItemText = Nothing
    Me.xxo_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codiva.OptionsFilter.AllowFilter = False
    Me.xxo_codiva.Visible = True
    Me.xxo_codiva.VisibleIndex = 36
    '
    'ec_stasino
    '
    Me.ec_stasino.AppearanceCell.Options.UseBackColor = True
    Me.ec_stasino.AppearanceCell.Options.UseTextOptions = True
    Me.ec_stasino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_stasino.Caption = "Stampa riga"
    Me.ec_stasino.Enabled = True
    Me.ec_stasino.FieldName = "ec_stasino"
    Me.ec_stasino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_stasino.Name = "ec_stasino"
    Me.ec_stasino.NTSRepositoryComboBox = Nothing
    Me.ec_stasino.NTSRepositoryItemCheck = Nothing
    Me.ec_stasino.NTSRepositoryItemMemo = Nothing
    Me.ec_stasino.NTSRepositoryItemText = Nothing
    Me.ec_stasino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_stasino.OptionsFilter.AllowFilter = False
    Me.ec_stasino.Visible = True
    Me.ec_stasino.VisibleIndex = 37
    '
    'ec_prelist
    '
    Me.ec_prelist.AppearanceCell.Options.UseBackColor = True
    Me.ec_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prelist.Caption = "Prz. listino"
    Me.ec_prelist.Enabled = True
    Me.ec_prelist.FieldName = "ec_prelist"
    Me.ec_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prelist.Name = "ec_prelist"
    Me.ec_prelist.NTSRepositoryComboBox = Nothing
    Me.ec_prelist.NTSRepositoryItemCheck = Nothing
    Me.ec_prelist.NTSRepositoryItemMemo = Nothing
    Me.ec_prelist.NTSRepositoryItemText = Nothing
    Me.ec_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prelist.OptionsFilter.AllowFilter = False
    Me.ec_prelist.Visible = True
    Me.ec_prelist.VisibleIndex = 38
    '
    'ec_codcfam
    '
    Me.ec_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcfam.Caption = "Linea/fam."
    Me.ec_codcfam.Enabled = True
    Me.ec_codcfam.FieldName = "ec_codcfam"
    Me.ec_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcfam.Name = "ec_codcfam"
    Me.ec_codcfam.NTSRepositoryComboBox = Nothing
    Me.ec_codcfam.NTSRepositoryItemCheck = Nothing
    Me.ec_codcfam.NTSRepositoryItemMemo = Nothing
    Me.ec_codcfam.NTSRepositoryItemText = Nothing
    Me.ec_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcfam.OptionsFilter.AllowFilter = False
    Me.ec_codcfam.Visible = True
    Me.ec_codcfam.VisibleIndex = 39
    '
    'xxo_codcfam
    '
    Me.xxo_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcfam.Caption = "Descr. linea/fam."
    Me.xxo_codcfam.Enabled = True
    Me.xxo_codcfam.FieldName = "xxo_codcfam"
    Me.xxo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcfam.Name = "xxo_codcfam"
    Me.xxo_codcfam.NTSRepositoryComboBox = Nothing
    Me.xxo_codcfam.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcfam.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcfam.NTSRepositoryItemText = Nothing
    Me.xxo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcfam.OptionsFilter.AllowFilter = False
    Me.xxo_codcfam.Visible = True
    Me.xxo_codcfam.VisibleIndex = 40
    '
    'ec_commeca
    '
    Me.ec_commeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_commeca.Caption = "Commessa"
    Me.ec_commeca.Enabled = True
    Me.ec_commeca.FieldName = "ec_commeca"
    Me.ec_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_commeca.Name = "ec_commeca"
    Me.ec_commeca.NTSRepositoryComboBox = Nothing
    Me.ec_commeca.NTSRepositoryItemCheck = Nothing
    Me.ec_commeca.NTSRepositoryItemMemo = Nothing
    Me.ec_commeca.NTSRepositoryItemText = Nothing
    Me.ec_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_commeca.OptionsFilter.AllowFilter = False
    Me.ec_commeca.Visible = True
    Me.ec_commeca.VisibleIndex = 41
    '
    'xxo_commeca
    '
    Me.xxo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xxo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_commeca.Caption = "Descr. commessa"
    Me.xxo_commeca.Enabled = True
    Me.xxo_commeca.FieldName = "xxo_commeca"
    Me.xxo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_commeca.Name = "xxo_commeca"
    Me.xxo_commeca.NTSRepositoryComboBox = Nothing
    Me.xxo_commeca.NTSRepositoryItemCheck = Nothing
    Me.xxo_commeca.NTSRepositoryItemMemo = Nothing
    Me.xxo_commeca.NTSRepositoryItemText = Nothing
    Me.xxo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_commeca.OptionsFilter.AllowFilter = False
    Me.xxo_commeca.Visible = True
    Me.xxo_commeca.VisibleIndex = 42
    '
    'ec_subcommeca
    '
    Me.ec_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_subcommeca.Caption = "Subcomm."
    Me.ec_subcommeca.Enabled = True
    Me.ec_subcommeca.FieldName = "ec_subcommeca"
    Me.ec_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_subcommeca.Name = "ec_subcommeca"
    Me.ec_subcommeca.NTSRepositoryComboBox = Nothing
    Me.ec_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.ec_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.ec_subcommeca.NTSRepositoryItemText = Nothing
    Me.ec_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_subcommeca.OptionsFilter.AllowFilter = False
    Me.ec_subcommeca.Visible = True
    Me.ec_subcommeca.VisibleIndex = 43
    '
    'ec_codcena
    '
    Me.ec_codcena.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcena.Caption = "Centro CA"
    Me.ec_codcena.Enabled = True
    Me.ec_codcena.FieldName = "ec_codcena"
    Me.ec_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcena.Name = "ec_codcena"
    Me.ec_codcena.NTSRepositoryComboBox = Nothing
    Me.ec_codcena.NTSRepositoryItemCheck = Nothing
    Me.ec_codcena.NTSRepositoryItemMemo = Nothing
    Me.ec_codcena.NTSRepositoryItemText = Nothing
    Me.ec_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcena.OptionsFilter.AllowFilter = False
    Me.ec_codcena.Visible = True
    Me.ec_codcena.VisibleIndex = 44
    '
    'xxo_codcena
    '
    Me.xxo_codcena.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcena.Caption = "Descr. centro"
    Me.xxo_codcena.Enabled = True
    Me.xxo_codcena.FieldName = "xxo_codcena"
    Me.xxo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcena.Name = "xxo_codcena"
    Me.xxo_codcena.NTSRepositoryComboBox = Nothing
    Me.xxo_codcena.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcena.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcena.NTSRepositoryItemText = Nothing
    Me.xxo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcena.OptionsFilter.AllowFilter = False
    Me.xxo_codcena.Visible = True
    Me.xxo_codcena.VisibleIndex = 45
    '
    'ec_confermato
    '
    Me.ec_confermato.AppearanceCell.Options.UseBackColor = True
    Me.ec_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_confermato.Caption = "Confermato"
    Me.ec_confermato.Enabled = True
    Me.ec_confermato.FieldName = "ec_confermato"
    Me.ec_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_confermato.Name = "ec_confermato"
    Me.ec_confermato.NTSRepositoryComboBox = Nothing
    Me.ec_confermato.NTSRepositoryItemCheck = Nothing
    Me.ec_confermato.NTSRepositoryItemMemo = Nothing
    Me.ec_confermato.NTSRepositoryItemText = Nothing
    Me.ec_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_confermato.OptionsFilter.AllowFilter = False
    Me.ec_confermato.Visible = True
    Me.ec_confermato.VisibleIndex = 46
    '
    'ec_rilasciato
    '
    Me.ec_rilasciato.AppearanceCell.Options.UseBackColor = True
    Me.ec_rilasciato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rilasciato.Caption = "Rilasciato"
    Me.ec_rilasciato.Enabled = True
    Me.ec_rilasciato.FieldName = "ec_rilasciato"
    Me.ec_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rilasciato.Name = "ec_rilasciato"
    Me.ec_rilasciato.NTSRepositoryComboBox = Nothing
    Me.ec_rilasciato.NTSRepositoryItemCheck = Nothing
    Me.ec_rilasciato.NTSRepositoryItemMemo = Nothing
    Me.ec_rilasciato.NTSRepositoryItemText = Nothing
    Me.ec_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rilasciato.OptionsFilter.AllowFilter = False
    Me.ec_rilasciato.Visible = True
    Me.ec_rilasciato.VisibleIndex = 47
    '
    'ec_aperto
    '
    Me.ec_aperto.AppearanceCell.Options.UseBackColor = True
    Me.ec_aperto.AppearanceCell.Options.UseTextOptions = True
    Me.ec_aperto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_aperto.Caption = "Aperto"
    Me.ec_aperto.Enabled = True
    Me.ec_aperto.FieldName = "ec_aperto"
    Me.ec_aperto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_aperto.Name = "ec_aperto"
    Me.ec_aperto.NTSRepositoryComboBox = Nothing
    Me.ec_aperto.NTSRepositoryItemCheck = Nothing
    Me.ec_aperto.NTSRepositoryItemMemo = Nothing
    Me.ec_aperto.NTSRepositoryItemText = Nothing
    Me.ec_aperto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_aperto.OptionsFilter.AllowFilter = False
    Me.ec_aperto.Visible = True
    Me.ec_aperto.VisibleIndex = 48
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 49
    '
    'ec_ubicaz
    '
    Me.ec_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.ec_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ubicaz.Caption = "Ubicazione"
    Me.ec_ubicaz.Enabled = True
    Me.ec_ubicaz.FieldName = "ec_ubicaz"
    Me.ec_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ubicaz.Name = "ec_ubicaz"
    Me.ec_ubicaz.NTSRepositoryComboBox = Nothing
    Me.ec_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.ec_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.ec_ubicaz.NTSRepositoryItemText = Nothing
    Me.ec_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ubicaz.OptionsFilter.AllowFilter = False
    Me.ec_ubicaz.Visible = True
    Me.ec_ubicaz.VisibleIndex = 50
    '
    'ec_causale
    '
    Me.ec_causale.AppearanceCell.Options.UseBackColor = True
    Me.ec_causale.AppearanceCell.Options.UseTextOptions = True
    Me.ec_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_causale.Caption = "Causale"
    Me.ec_causale.Enabled = True
    Me.ec_causale.FieldName = "ec_causale"
    Me.ec_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_causale.Name = "ec_causale"
    Me.ec_causale.NTSRepositoryComboBox = Nothing
    Me.ec_causale.NTSRepositoryItemCheck = Nothing
    Me.ec_causale.NTSRepositoryItemMemo = Nothing
    Me.ec_causale.NTSRepositoryItemText = Nothing
    Me.ec_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_causale.OptionsFilter.AllowFilter = False
    Me.ec_causale.Visible = True
    Me.ec_causale.VisibleIndex = 51
    '
    'xxo_causale
    '
    Me.xxo_causale.AppearanceCell.Options.UseBackColor = True
    Me.xxo_causale.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_causale.Caption = "Descr. causale"
    Me.xxo_causale.Enabled = True
    Me.xxo_causale.FieldName = "xxo_causale"
    Me.xxo_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_causale.Name = "xxo_causale"
    Me.xxo_causale.NTSRepositoryComboBox = Nothing
    Me.xxo_causale.NTSRepositoryItemCheck = Nothing
    Me.xxo_causale.NTSRepositoryItemMemo = Nothing
    Me.xxo_causale.NTSRepositoryItemText = Nothing
    Me.xxo_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_causale.OptionsFilter.AllowFilter = False
    Me.xxo_causale.Visible = True
    Me.xxo_causale.VisibleIndex = 52
    '
    'ec_causale2
    '
    Me.ec_causale2.AppearanceCell.Options.UseBackColor = True
    Me.ec_causale2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_causale2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_causale2.Caption = "Causale 2"
    Me.ec_causale2.Enabled = True
    Me.ec_causale2.FieldName = "ec_causale2"
    Me.ec_causale2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_causale2.Name = "ec_causale2"
    Me.ec_causale2.NTSRepositoryComboBox = Nothing
    Me.ec_causale2.NTSRepositoryItemCheck = Nothing
    Me.ec_causale2.NTSRepositoryItemMemo = Nothing
    Me.ec_causale2.NTSRepositoryItemText = Nothing
    Me.ec_causale2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_causale2.OptionsFilter.AllowFilter = False
    Me.ec_causale2.Visible = True
    Me.ec_causale2.VisibleIndex = 53
    '
    'ec_fase
    '
    Me.ec_fase.AppearanceCell.Options.UseBackColor = True
    Me.ec_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_fase.Caption = "Fase art."
    Me.ec_fase.Enabled = True
    Me.ec_fase.FieldName = "ec_fase"
    Me.ec_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_fase.Name = "ec_fase"
    Me.ec_fase.NTSRepositoryComboBox = Nothing
    Me.ec_fase.NTSRepositoryItemCheck = Nothing
    Me.ec_fase.NTSRepositoryItemMemo = Nothing
    Me.ec_fase.NTSRepositoryItemText = Nothing
    Me.ec_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_fase.OptionsFilter.AllowFilter = False
    Me.ec_fase.Visible = True
    Me.ec_fase.VisibleIndex = 54
    '
    'xxo_fase
    '
    Me.xxo_fase.AppearanceCell.Options.UseBackColor = True
    Me.xxo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_fase.Caption = "Descr. fase"
    Me.xxo_fase.Enabled = True
    Me.xxo_fase.FieldName = "xxo_fase"
    Me.xxo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_fase.Name = "xxo_fase"
    Me.xxo_fase.NTSRepositoryComboBox = Nothing
    Me.xxo_fase.NTSRepositoryItemCheck = Nothing
    Me.xxo_fase.NTSRepositoryItemMemo = Nothing
    Me.xxo_fase.NTSRepositoryItemText = Nothing
    Me.xxo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_fase.OptionsFilter.AllowFilter = False
    Me.xxo_fase.Visible = True
    Me.xxo_fase.VisibleIndex = 55
    '
    'ec_misura1
    '
    Me.ec_misura1.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura1.Caption = "Misura 1"
    Me.ec_misura1.Enabled = True
    Me.ec_misura1.FieldName = "ec_misura1"
    Me.ec_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura1.Name = "ec_misura1"
    Me.ec_misura1.NTSRepositoryComboBox = Nothing
    Me.ec_misura1.NTSRepositoryItemCheck = Nothing
    Me.ec_misura1.NTSRepositoryItemMemo = Nothing
    Me.ec_misura1.NTSRepositoryItemText = Nothing
    Me.ec_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura1.OptionsFilter.AllowFilter = False
    Me.ec_misura1.Visible = True
    Me.ec_misura1.VisibleIndex = 56
    '
    'ec_misura2
    '
    Me.ec_misura2.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura2.Caption = "Misura 2"
    Me.ec_misura2.Enabled = True
    Me.ec_misura2.FieldName = "ec_misura2"
    Me.ec_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura2.Name = "ec_misura2"
    Me.ec_misura2.NTSRepositoryComboBox = Nothing
    Me.ec_misura2.NTSRepositoryItemCheck = Nothing
    Me.ec_misura2.NTSRepositoryItemMemo = Nothing
    Me.ec_misura2.NTSRepositoryItemText = Nothing
    Me.ec_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura2.OptionsFilter.AllowFilter = False
    Me.ec_misura2.Visible = True
    Me.ec_misura2.VisibleIndex = 57
    '
    'ec_misura3
    '
    Me.ec_misura3.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura3.Caption = "Misura 3"
    Me.ec_misura3.Enabled = True
    Me.ec_misura3.FieldName = "ec_misura3"
    Me.ec_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura3.Name = "ec_misura3"
    Me.ec_misura3.NTSRepositoryComboBox = Nothing
    Me.ec_misura3.NTSRepositoryItemCheck = Nothing
    Me.ec_misura3.NTSRepositoryItemMemo = Nothing
    Me.ec_misura3.NTSRepositoryItemText = Nothing
    Me.ec_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura3.OptionsFilter.AllowFilter = False
    Me.ec_misura3.Visible = True
    Me.ec_misura3.VisibleIndex = 58
    '
    'ec_datini
    '
    Me.ec_datini.AppearanceCell.Options.UseBackColor = True
    Me.ec_datini.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datini.Caption = "Data inizio"
    Me.ec_datini.Enabled = True
    Me.ec_datini.FieldName = "ec_datini"
    Me.ec_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datini.Name = "ec_datini"
    Me.ec_datini.NTSRepositoryComboBox = Nothing
    Me.ec_datini.NTSRepositoryItemCheck = Nothing
    Me.ec_datini.NTSRepositoryItemMemo = Nothing
    Me.ec_datini.NTSRepositoryItemText = Nothing
    Me.ec_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datini.OptionsFilter.AllowFilter = False
    Me.ec_datini.Visible = True
    Me.ec_datini.VisibleIndex = 59
    '
    'ec_datfin
    '
    Me.ec_datfin.AppearanceCell.Options.UseBackColor = True
    Me.ec_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datfin.Caption = "Data fine"
    Me.ec_datfin.Enabled = True
    Me.ec_datfin.FieldName = "ec_datfin"
    Me.ec_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datfin.Name = "ec_datfin"
    Me.ec_datfin.NTSRepositoryComboBox = Nothing
    Me.ec_datfin.NTSRepositoryItemCheck = Nothing
    Me.ec_datfin.NTSRepositoryItemMemo = Nothing
    Me.ec_datfin.NTSRepositoryItemText = Nothing
    Me.ec_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datfin.OptionsFilter.AllowFilter = False
    Me.ec_datfin.Visible = True
    Me.ec_datfin.VisibleIndex = 60
    '
    'ec_ortipo
    '
    Me.ec_ortipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_ortipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ortipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ortipo.Caption = "OR tipo"
    Me.ec_ortipo.Enabled = True
    Me.ec_ortipo.FieldName = "ec_ortipo"
    Me.ec_ortipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ortipo.Name = "ec_ortipo"
    Me.ec_ortipo.NTSRepositoryComboBox = Nothing
    Me.ec_ortipo.NTSRepositoryItemCheck = Nothing
    Me.ec_ortipo.NTSRepositoryItemMemo = Nothing
    Me.ec_ortipo.NTSRepositoryItemText = Nothing
    Me.ec_ortipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ortipo.OptionsFilter.AllowFilter = False
    Me.ec_ortipo.Visible = True
    Me.ec_ortipo.VisibleIndex = 61
    '
    'ec_oranno
    '
    Me.ec_oranno.AppearanceCell.Options.UseBackColor = True
    Me.ec_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_oranno.Caption = "OR anno"
    Me.ec_oranno.Enabled = True
    Me.ec_oranno.FieldName = "ec_oranno"
    Me.ec_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_oranno.Name = "ec_oranno"
    Me.ec_oranno.NTSRepositoryComboBox = Nothing
    Me.ec_oranno.NTSRepositoryItemCheck = Nothing
    Me.ec_oranno.NTSRepositoryItemMemo = Nothing
    Me.ec_oranno.NTSRepositoryItemText = Nothing
    Me.ec_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_oranno.OptionsFilter.AllowFilter = False
    Me.ec_oranno.Visible = True
    Me.ec_oranno.VisibleIndex = 62
    '
    'ec_orserie
    '
    Me.ec_orserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orserie.Caption = "OR serie"
    Me.ec_orserie.Enabled = True
    Me.ec_orserie.FieldName = "ec_orserie"
    Me.ec_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orserie.Name = "ec_orserie"
    Me.ec_orserie.NTSRepositoryComboBox = Nothing
    Me.ec_orserie.NTSRepositoryItemCheck = Nothing
    Me.ec_orserie.NTSRepositoryItemMemo = Nothing
    Me.ec_orserie.NTSRepositoryItemText = Nothing
    Me.ec_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orserie.OptionsFilter.AllowFilter = False
    Me.ec_orserie.Visible = True
    Me.ec_orserie.VisibleIndex = 63
    '
    'ec_ornum
    '
    Me.ec_ornum.AppearanceCell.Options.UseBackColor = True
    Me.ec_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ornum.Caption = "OR num."
    Me.ec_ornum.Enabled = True
    Me.ec_ornum.FieldName = "ec_ornum"
    Me.ec_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ornum.Name = "ec_ornum"
    Me.ec_ornum.NTSRepositoryComboBox = Nothing
    Me.ec_ornum.NTSRepositoryItemCheck = Nothing
    Me.ec_ornum.NTSRepositoryItemMemo = Nothing
    Me.ec_ornum.NTSRepositoryItemText = Nothing
    Me.ec_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ornum.OptionsFilter.AllowFilter = False
    Me.ec_ornum.Visible = True
    Me.ec_ornum.VisibleIndex = 64
    '
    'ec_orriga
    '
    Me.ec_orriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_orriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orriga.Caption = "OR riga"
    Me.ec_orriga.Enabled = True
    Me.ec_orriga.FieldName = "ec_orriga"
    Me.ec_orriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orriga.Name = "ec_orriga"
    Me.ec_orriga.NTSRepositoryComboBox = Nothing
    Me.ec_orriga.NTSRepositoryItemCheck = Nothing
    Me.ec_orriga.NTSRepositoryItemMemo = Nothing
    Me.ec_orriga.NTSRepositoryItemText = Nothing
    Me.ec_orriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orriga.OptionsFilter.AllowFilter = False
    Me.ec_orriga.Visible = True
    Me.ec_orriga.VisibleIndex = 65
    '
    'ec_salcon
    '
    Me.ec_salcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_salcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_salcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_salcon.Caption = "OR saldato"
    Me.ec_salcon.Enabled = True
    Me.ec_salcon.FieldName = "ec_salcon"
    Me.ec_salcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_salcon.Name = "ec_salcon"
    Me.ec_salcon.NTSRepositoryComboBox = Nothing
    Me.ec_salcon.NTSRepositoryItemCheck = Nothing
    Me.ec_salcon.NTSRepositoryItemMemo = Nothing
    Me.ec_salcon.NTSRepositoryItemText = Nothing
    Me.ec_salcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_salcon.OptionsFilter.AllowFilter = False
    Me.ec_salcon.Visible = True
    Me.ec_salcon.VisibleIndex = 66
    '
    'ec_nptipo
    '
    Me.ec_nptipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_nptipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nptipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nptipo.Caption = "PN/Off tipo"
    Me.ec_nptipo.Enabled = True
    Me.ec_nptipo.FieldName = "ec_nptipo"
    Me.ec_nptipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nptipo.Name = "ec_nptipo"
    Me.ec_nptipo.NTSRepositoryComboBox = Nothing
    Me.ec_nptipo.NTSRepositoryItemCheck = Nothing
    Me.ec_nptipo.NTSRepositoryItemMemo = Nothing
    Me.ec_nptipo.NTSRepositoryItemText = Nothing
    Me.ec_nptipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nptipo.OptionsFilter.AllowFilter = False
    Me.ec_nptipo.Visible = True
    Me.ec_nptipo.VisibleIndex = 67
    '
    'ec_npanno
    '
    Me.ec_npanno.AppearanceCell.Options.UseBackColor = True
    Me.ec_npanno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npanno.Caption = "PN/Off anno"
    Me.ec_npanno.Enabled = True
    Me.ec_npanno.FieldName = "ec_npanno"
    Me.ec_npanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npanno.Name = "ec_npanno"
    Me.ec_npanno.NTSRepositoryComboBox = Nothing
    Me.ec_npanno.NTSRepositoryItemCheck = Nothing
    Me.ec_npanno.NTSRepositoryItemMemo = Nothing
    Me.ec_npanno.NTSRepositoryItemText = Nothing
    Me.ec_npanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npanno.OptionsFilter.AllowFilter = False
    Me.ec_npanno.Visible = True
    Me.ec_npanno.VisibleIndex = 68
    '
    'ec_npserie
    '
    Me.ec_npserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_npserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npserie.Caption = "PN/Off serie"
    Me.ec_npserie.Enabled = True
    Me.ec_npserie.FieldName = "ec_npserie"
    Me.ec_npserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npserie.Name = "ec_npserie"
    Me.ec_npserie.NTSRepositoryComboBox = Nothing
    Me.ec_npserie.NTSRepositoryItemCheck = Nothing
    Me.ec_npserie.NTSRepositoryItemMemo = Nothing
    Me.ec_npserie.NTSRepositoryItemText = Nothing
    Me.ec_npserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npserie.OptionsFilter.AllowFilter = False
    Me.ec_npserie.Visible = True
    Me.ec_npserie.VisibleIndex = 69
    '
    'ec_npnum
    '
    Me.ec_npnum.AppearanceCell.Options.UseBackColor = True
    Me.ec_npnum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npnum.Caption = "PN/Off num."
    Me.ec_npnum.Enabled = True
    Me.ec_npnum.FieldName = "ec_npnum"
    Me.ec_npnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npnum.Name = "ec_npnum"
    Me.ec_npnum.NTSRepositoryComboBox = Nothing
    Me.ec_npnum.NTSRepositoryItemCheck = Nothing
    Me.ec_npnum.NTSRepositoryItemMemo = Nothing
    Me.ec_npnum.NTSRepositoryItemText = Nothing
    Me.ec_npnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npnum.OptionsFilter.AllowFilter = False
    Me.ec_npnum.Visible = True
    Me.ec_npnum.VisibleIndex = 70
    '
    'ec_npvers
    '
    Me.ec_npvers.AppearanceCell.Options.UseBackColor = True
    Me.ec_npvers.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npvers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npvers.Caption = "PN/Off vers."
    Me.ec_npvers.Enabled = True
    Me.ec_npvers.FieldName = "ec_npvers"
    Me.ec_npvers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npvers.Name = "ec_npvers"
    Me.ec_npvers.NTSRepositoryComboBox = Nothing
    Me.ec_npvers.NTSRepositoryItemCheck = Nothing
    Me.ec_npvers.NTSRepositoryItemMemo = Nothing
    Me.ec_npvers.NTSRepositoryItemText = Nothing
    Me.ec_npvers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npvers.OptionsFilter.AllowFilter = False
    Me.ec_npvers.Visible = True
    Me.ec_npvers.VisibleIndex = 71
    '
    'ec_npriga
    '
    Me.ec_npriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_npriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npriga.Caption = "PN/Off riga"
    Me.ec_npriga.Enabled = True
    Me.ec_npriga.FieldName = "ec_npriga"
    Me.ec_npriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npriga.Name = "ec_npriga"
    Me.ec_npriga.NTSRepositoryComboBox = Nothing
    Me.ec_npriga.NTSRepositoryItemCheck = Nothing
    Me.ec_npriga.NTSRepositoryItemMemo = Nothing
    Me.ec_npriga.NTSRepositoryItemText = Nothing
    Me.ec_npriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npriga.OptionsFilter.AllowFilter = False
    Me.ec_npriga.Visible = True
    Me.ec_npriga.VisibleIndex = 72
    '
    'ec_pnsalcon
    '
    Me.ec_pnsalcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_pnsalcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pnsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pnsalcon.Caption = "PN/Off saldata"
    Me.ec_pnsalcon.Enabled = True
    Me.ec_pnsalcon.FieldName = "ec_pnsalcon"
    Me.ec_pnsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pnsalcon.Name = "ec_pnsalcon"
    Me.ec_pnsalcon.NTSRepositoryComboBox = Nothing
    Me.ec_pnsalcon.NTSRepositoryItemCheck = Nothing
    Me.ec_pnsalcon.NTSRepositoryItemMemo = Nothing
    Me.ec_pnsalcon.NTSRepositoryItemText = Nothing
    Me.ec_pnsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pnsalcon.OptionsFilter.AllowFilter = False
    Me.ec_pnsalcon.Visible = True
    Me.ec_pnsalcon.VisibleIndex = 73
    '
    'ec_vers
    '
    Me.ec_vers.AppearanceCell.Options.UseBackColor = True
    Me.ec_vers.AppearanceCell.Options.UseTextOptions = True
    Me.ec_vers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_vers.Caption = "Vers."
    Me.ec_vers.Enabled = True
    Me.ec_vers.FieldName = "ec_vers"
    Me.ec_vers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_vers.Name = "ec_vers"
    Me.ec_vers.NTSRepositoryComboBox = Nothing
    Me.ec_vers.NTSRepositoryItemCheck = Nothing
    Me.ec_vers.NTSRepositoryItemMemo = Nothing
    Me.ec_vers.NTSRepositoryItemText = Nothing
    Me.ec_vers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_vers.OptionsFilter.AllowFilter = False
    Me.ec_vers.Visible = True
    Me.ec_vers.VisibleIndex = 74
    '
    'xxo_conto
    '
    Me.xxo_conto.AppearanceCell.Options.UseBackColor = True
    Me.xxo_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_conto.Caption = "Conto"
    Me.xxo_conto.Enabled = True
    Me.xxo_conto.FieldName = "xxo_conto"
    Me.xxo_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_conto.Name = "xxo_conto"
    Me.xxo_conto.NTSRepositoryComboBox = Nothing
    Me.xxo_conto.NTSRepositoryItemCheck = Nothing
    Me.xxo_conto.NTSRepositoryItemMemo = Nothing
    Me.xxo_conto.NTSRepositoryItemText = Nothing
    Me.xxo_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_conto.OptionsFilter.AllowFilter = False
    Me.xxo_conto.Visible = True
    Me.xxo_conto.VisibleIndex = 75
    '
    'NtsSplitter2
    '
    Me.NtsSplitter2.BackColor = System.Drawing.SystemColors.ActiveCaption
    Me.NtsSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.NtsSplitter2.Location = New System.Drawing.Point(0, 209)
    Me.NtsSplitter2.MinExtra = 50
    Me.NtsSplitter2.Name = "NtsSplitter2"
    Me.NtsSplitter2.Size = New System.Drawing.Size(782, 3)
    Me.NtsSplitter2.TabIndex = 3
    Me.NtsSplitter2.TabStop = False
    '
    'grPrin
    '
    Me.grPrin.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.grPrin.EmbeddedNavigator.Name = ""
    Me.grPrin.Location = New System.Drawing.Point(0, 212)
    Me.grPrin.MainView = Me.grvPrin
    Me.grPrin.Name = "grPrin"
    Me.grPrin.Size = New System.Drawing.Size(782, 103)
    Me.grPrin.TabIndex = 0
    Me.grPrin.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPrin})
    '
    'grvPrin
    '
    Me.grvPrin.ActiveFilterEnabled = False
    Me.grvPrin.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pn_datreg, Me.pn_numreg, Me.pn_riga, Me.pn_causale, Me.pn_descauc, Me.pn_conto, Me.xxp_conto, Me.pn_descr, Me.pn_darave, Me.pn_importo, Me.pn_dare, Me.pn_avere, Me.pn_datdoc, Me.pn_numdoc, Me.pn_alfdoc, Me.pn_controp, Me.xxp_controp, Me.pn_annpar, Me.pn_alfpar, Me.pn_numpar, Me.pn_codvalu, Me.xxp_codvalu, Me.pn_cambio, Me.pn_impval, Me.pn_dareval, Me.pn_avereval, Me.pn_tregiva, Me.pn_nregiva, Me.pn_codiva, Me.xxp_codiva, Me.pn_aliqiva, Me.pn_indetr, Me.pn_contocf, Me.xxp_contocf, Me.pn_imponib, Me.pn_imponibval, Me.pn_tipacq, Me.pn_numpro, Me.pn_alfpro, Me.pn_integr, Me.pn_fllg, Me.pn_dtcomiva, Me.pn_dtvaluta, Me.pn_dtcomplaf, Me.pn_datini, Me.pn_datfin, Me.pn_ultagg, Me.pn_opnome, Me.pn_escomp})
    Me.grvPrin.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvPrin.Enabled = True
    Me.grvPrin.GridControl = Me.grPrin
    Me.grvPrin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvPrin.Name = "grvPrin"
    Me.grvPrin.NTSAllowDelete = True
    Me.grvPrin.NTSAllowInsert = True
    Me.grvPrin.NTSAllowUpdate = True
    Me.grvPrin.NTSMenuContext = Nothing
    Me.grvPrin.OptionsCustomization.AllowRowSizing = True
    Me.grvPrin.OptionsFilter.AllowFilterEditor = False
    Me.grvPrin.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvPrin.OptionsNavigation.UseTabKey = False
    Me.grvPrin.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvPrin.OptionsView.ColumnAutoWidth = False
    Me.grvPrin.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvPrin.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvPrin.OptionsView.ShowGroupPanel = False
    Me.grvPrin.RowHeight = 14
    '
    'pn_datreg
    '
    Me.pn_datreg.AppearanceCell.Options.UseBackColor = True
    Me.pn_datreg.AppearanceCell.Options.UseTextOptions = True
    Me.pn_datreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_datreg.Caption = "Data reg."
    Me.pn_datreg.Enabled = True
    Me.pn_datreg.FieldName = "pn_datreg"
    Me.pn_datreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_datreg.Name = "pn_datreg"
    Me.pn_datreg.NTSRepositoryComboBox = Nothing
    Me.pn_datreg.NTSRepositoryItemCheck = Nothing
    Me.pn_datreg.NTSRepositoryItemMemo = Nothing
    Me.pn_datreg.NTSRepositoryItemText = Nothing
    Me.pn_datreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_datreg.OptionsFilter.AllowFilter = False
    Me.pn_datreg.Visible = True
    Me.pn_datreg.VisibleIndex = 0
    Me.pn_datreg.Width = 70
    '
    'pn_numreg
    '
    Me.pn_numreg.AppearanceCell.Options.UseBackColor = True
    Me.pn_numreg.AppearanceCell.Options.UseTextOptions = True
    Me.pn_numreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_numreg.Caption = "Num. reg."
    Me.pn_numreg.Enabled = True
    Me.pn_numreg.FieldName = "pn_numreg"
    Me.pn_numreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_numreg.Name = "pn_numreg"
    Me.pn_numreg.NTSRepositoryComboBox = Nothing
    Me.pn_numreg.NTSRepositoryItemCheck = Nothing
    Me.pn_numreg.NTSRepositoryItemMemo = Nothing
    Me.pn_numreg.NTSRepositoryItemText = Nothing
    Me.pn_numreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_numreg.OptionsFilter.AllowFilter = False
    Me.pn_numreg.Visible = True
    Me.pn_numreg.VisibleIndex = 1
    Me.pn_numreg.Width = 70
    '
    'pn_riga
    '
    Me.pn_riga.AppearanceCell.Options.UseBackColor = True
    Me.pn_riga.AppearanceCell.Options.UseTextOptions = True
    Me.pn_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_riga.Caption = "Riga"
    Me.pn_riga.Enabled = True
    Me.pn_riga.FieldName = "pn_riga"
    Me.pn_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_riga.Name = "pn_riga"
    Me.pn_riga.NTSRepositoryComboBox = Nothing
    Me.pn_riga.NTSRepositoryItemCheck = Nothing
    Me.pn_riga.NTSRepositoryItemMemo = Nothing
    Me.pn_riga.NTSRepositoryItemText = Nothing
    Me.pn_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_riga.OptionsFilter.AllowFilter = False
    Me.pn_riga.Visible = True
    Me.pn_riga.VisibleIndex = 2
    '
    'pn_causale
    '
    Me.pn_causale.AppearanceCell.Options.UseBackColor = True
    Me.pn_causale.AppearanceCell.Options.UseTextOptions = True
    Me.pn_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_causale.Caption = "Causale"
    Me.pn_causale.Enabled = True
    Me.pn_causale.FieldName = "pn_causale"
    Me.pn_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_causale.Name = "pn_causale"
    Me.pn_causale.NTSRepositoryComboBox = Nothing
    Me.pn_causale.NTSRepositoryItemCheck = Nothing
    Me.pn_causale.NTSRepositoryItemMemo = Nothing
    Me.pn_causale.NTSRepositoryItemText = Nothing
    Me.pn_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_causale.OptionsFilter.AllowFilter = False
    Me.pn_causale.Visible = True
    Me.pn_causale.VisibleIndex = 3
    '
    'pn_descauc
    '
    Me.pn_descauc.AppearanceCell.Options.UseBackColor = True
    Me.pn_descauc.AppearanceCell.Options.UseTextOptions = True
    Me.pn_descauc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_descauc.Caption = "Descr. causale"
    Me.pn_descauc.Enabled = True
    Me.pn_descauc.FieldName = "pn_descauc"
    Me.pn_descauc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_descauc.Name = "pn_descauc"
    Me.pn_descauc.NTSRepositoryComboBox = Nothing
    Me.pn_descauc.NTSRepositoryItemCheck = Nothing
    Me.pn_descauc.NTSRepositoryItemMemo = Nothing
    Me.pn_descauc.NTSRepositoryItemText = Nothing
    Me.pn_descauc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_descauc.OptionsFilter.AllowFilter = False
    Me.pn_descauc.Visible = True
    Me.pn_descauc.VisibleIndex = 4
    '
    'pn_conto
    '
    Me.pn_conto.AppearanceCell.Options.UseBackColor = True
    Me.pn_conto.AppearanceCell.Options.UseTextOptions = True
    Me.pn_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_conto.Caption = "Conto"
    Me.pn_conto.Enabled = True
    Me.pn_conto.FieldName = "pn_conto"
    Me.pn_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_conto.Name = "pn_conto"
    Me.pn_conto.NTSRepositoryComboBox = Nothing
    Me.pn_conto.NTSRepositoryItemCheck = Nothing
    Me.pn_conto.NTSRepositoryItemMemo = Nothing
    Me.pn_conto.NTSRepositoryItemText = Nothing
    Me.pn_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_conto.OptionsFilter.AllowFilter = False
    Me.pn_conto.Visible = True
    Me.pn_conto.VisibleIndex = 5
    '
    'xxp_conto
    '
    Me.xxp_conto.AppearanceCell.Options.UseBackColor = True
    Me.xxp_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xxp_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxp_conto.Caption = "Descr. conto"
    Me.xxp_conto.Enabled = True
    Me.xxp_conto.FieldName = "xxp_conto"
    Me.xxp_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxp_conto.Name = "xxp_conto"
    Me.xxp_conto.NTSRepositoryComboBox = Nothing
    Me.xxp_conto.NTSRepositoryItemCheck = Nothing
    Me.xxp_conto.NTSRepositoryItemMemo = Nothing
    Me.xxp_conto.NTSRepositoryItemText = Nothing
    Me.xxp_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxp_conto.OptionsFilter.AllowFilter = False
    Me.xxp_conto.Visible = True
    Me.xxp_conto.VisibleIndex = 6
    '
    'pn_descr
    '
    Me.pn_descr.AppearanceCell.Options.UseBackColor = True
    Me.pn_descr.AppearanceCell.Options.UseTextOptions = True
    Me.pn_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_descr.Caption = "Descr."
    Me.pn_descr.Enabled = True
    Me.pn_descr.FieldName = "pn_descr"
    Me.pn_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_descr.Name = "pn_descr"
    Me.pn_descr.NTSRepositoryComboBox = Nothing
    Me.pn_descr.NTSRepositoryItemCheck = Nothing
    Me.pn_descr.NTSRepositoryItemMemo = Nothing
    Me.pn_descr.NTSRepositoryItemText = Nothing
    Me.pn_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_descr.OptionsFilter.AllowFilter = False
    Me.pn_descr.Visible = True
    Me.pn_descr.VisibleIndex = 7
    '
    'pn_darave
    '
    Me.pn_darave.AppearanceCell.Options.UseBackColor = True
    Me.pn_darave.AppearanceCell.Options.UseTextOptions = True
    Me.pn_darave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_darave.Caption = "D/A"
    Me.pn_darave.Enabled = True
    Me.pn_darave.FieldName = "pn_darave"
    Me.pn_darave.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_darave.Name = "pn_darave"
    Me.pn_darave.NTSRepositoryComboBox = Nothing
    Me.pn_darave.NTSRepositoryItemCheck = Nothing
    Me.pn_darave.NTSRepositoryItemMemo = Nothing
    Me.pn_darave.NTSRepositoryItemText = Nothing
    Me.pn_darave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_darave.OptionsFilter.AllowFilter = False
    Me.pn_darave.Visible = True
    Me.pn_darave.VisibleIndex = 8
    '
    'pn_importo
    '
    Me.pn_importo.AppearanceCell.Options.UseBackColor = True
    Me.pn_importo.AppearanceCell.Options.UseTextOptions = True
    Me.pn_importo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_importo.Caption = "Importo"
    Me.pn_importo.Enabled = True
    Me.pn_importo.FieldName = "pn_importo"
    Me.pn_importo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_importo.Name = "pn_importo"
    Me.pn_importo.NTSRepositoryComboBox = Nothing
    Me.pn_importo.NTSRepositoryItemCheck = Nothing
    Me.pn_importo.NTSRepositoryItemMemo = Nothing
    Me.pn_importo.NTSRepositoryItemText = Nothing
    Me.pn_importo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_importo.OptionsFilter.AllowFilter = False
    Me.pn_importo.Visible = True
    Me.pn_importo.VisibleIndex = 9
    '
    'pn_dare
    '
    Me.pn_dare.AppearanceCell.Options.UseBackColor = True
    Me.pn_dare.AppearanceCell.Options.UseTextOptions = True
    Me.pn_dare.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_dare.Caption = "Dare"
    Me.pn_dare.Enabled = True
    Me.pn_dare.FieldName = "pn_dare"
    Me.pn_dare.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_dare.Name = "pn_dare"
    Me.pn_dare.NTSRepositoryComboBox = Nothing
    Me.pn_dare.NTSRepositoryItemCheck = Nothing
    Me.pn_dare.NTSRepositoryItemMemo = Nothing
    Me.pn_dare.NTSRepositoryItemText = Nothing
    Me.pn_dare.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_dare.OptionsFilter.AllowFilter = False
    Me.pn_dare.Visible = True
    Me.pn_dare.VisibleIndex = 10
    '
    'pn_avere
    '
    Me.pn_avere.AppearanceCell.Options.UseBackColor = True
    Me.pn_avere.AppearanceCell.Options.UseTextOptions = True
    Me.pn_avere.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_avere.Caption = "Avere"
    Me.pn_avere.Enabled = True
    Me.pn_avere.FieldName = "pn_avere"
    Me.pn_avere.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_avere.Name = "pn_avere"
    Me.pn_avere.NTSRepositoryComboBox = Nothing
    Me.pn_avere.NTSRepositoryItemCheck = Nothing
    Me.pn_avere.NTSRepositoryItemMemo = Nothing
    Me.pn_avere.NTSRepositoryItemText = Nothing
    Me.pn_avere.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_avere.OptionsFilter.AllowFilter = False
    Me.pn_avere.Visible = True
    Me.pn_avere.VisibleIndex = 11
    '
    'pn_datdoc
    '
    Me.pn_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.pn_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pn_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_datdoc.Caption = "Data doc"
    Me.pn_datdoc.Enabled = True
    Me.pn_datdoc.FieldName = "pn_datdoc"
    Me.pn_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_datdoc.Name = "pn_datdoc"
    Me.pn_datdoc.NTSRepositoryComboBox = Nothing
    Me.pn_datdoc.NTSRepositoryItemCheck = Nothing
    Me.pn_datdoc.NTSRepositoryItemMemo = Nothing
    Me.pn_datdoc.NTSRepositoryItemText = Nothing
    Me.pn_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_datdoc.OptionsFilter.AllowFilter = False
    Me.pn_datdoc.Visible = True
    Me.pn_datdoc.VisibleIndex = 12
    '
    'pn_numdoc
    '
    Me.pn_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.pn_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pn_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_numdoc.Caption = "Num. doc"
    Me.pn_numdoc.Enabled = True
    Me.pn_numdoc.FieldName = "pn_numdoc"
    Me.pn_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_numdoc.Name = "pn_numdoc"
    Me.pn_numdoc.NTSRepositoryComboBox = Nothing
    Me.pn_numdoc.NTSRepositoryItemCheck = Nothing
    Me.pn_numdoc.NTSRepositoryItemMemo = Nothing
    Me.pn_numdoc.NTSRepositoryItemText = Nothing
    Me.pn_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_numdoc.OptionsFilter.AllowFilter = False
    Me.pn_numdoc.Visible = True
    Me.pn_numdoc.VisibleIndex = 13
    '
    'pn_alfdoc
    '
    Me.pn_alfdoc.AppearanceCell.Options.UseBackColor = True
    Me.pn_alfdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pn_alfdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_alfdoc.Caption = "Serie doc"
    Me.pn_alfdoc.Enabled = True
    Me.pn_alfdoc.FieldName = "pn_alfdoc"
    Me.pn_alfdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_alfdoc.Name = "pn_alfdoc"
    Me.pn_alfdoc.NTSRepositoryComboBox = Nothing
    Me.pn_alfdoc.NTSRepositoryItemCheck = Nothing
    Me.pn_alfdoc.NTSRepositoryItemMemo = Nothing
    Me.pn_alfdoc.NTSRepositoryItemText = Nothing
    Me.pn_alfdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_alfdoc.OptionsFilter.AllowFilter = False
    Me.pn_alfdoc.Visible = True
    Me.pn_alfdoc.VisibleIndex = 14
    '
    'pn_controp
    '
    Me.pn_controp.AppearanceCell.Options.UseBackColor = True
    Me.pn_controp.AppearanceCell.Options.UseTextOptions = True
    Me.pn_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_controp.Caption = "Controp."
    Me.pn_controp.Enabled = True
    Me.pn_controp.FieldName = "pn_controp"
    Me.pn_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_controp.Name = "pn_controp"
    Me.pn_controp.NTSRepositoryComboBox = Nothing
    Me.pn_controp.NTSRepositoryItemCheck = Nothing
    Me.pn_controp.NTSRepositoryItemMemo = Nothing
    Me.pn_controp.NTSRepositoryItemText = Nothing
    Me.pn_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_controp.OptionsFilter.AllowFilter = False
    Me.pn_controp.Visible = True
    Me.pn_controp.VisibleIndex = 15
    '
    'xxp_controp
    '
    Me.xxp_controp.AppearanceCell.Options.UseBackColor = True
    Me.xxp_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xxp_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxp_controp.Caption = "Descr. controp."
    Me.xxp_controp.Enabled = True
    Me.xxp_controp.FieldName = "xxp_controp"
    Me.xxp_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxp_controp.Name = "xxp_controp"
    Me.xxp_controp.NTSRepositoryComboBox = Nothing
    Me.xxp_controp.NTSRepositoryItemCheck = Nothing
    Me.xxp_controp.NTSRepositoryItemMemo = Nothing
    Me.xxp_controp.NTSRepositoryItemText = Nothing
    Me.xxp_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxp_controp.OptionsFilter.AllowFilter = False
    Me.xxp_controp.Visible = True
    Me.xxp_controp.VisibleIndex = 16
    '
    'pn_annpar
    '
    Me.pn_annpar.AppearanceCell.Options.UseBackColor = True
    Me.pn_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.pn_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_annpar.Caption = "Anno par."
    Me.pn_annpar.Enabled = True
    Me.pn_annpar.FieldName = "pn_annpar"
    Me.pn_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_annpar.Name = "pn_annpar"
    Me.pn_annpar.NTSRepositoryComboBox = Nothing
    Me.pn_annpar.NTSRepositoryItemCheck = Nothing
    Me.pn_annpar.NTSRepositoryItemMemo = Nothing
    Me.pn_annpar.NTSRepositoryItemText = Nothing
    Me.pn_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_annpar.OptionsFilter.AllowFilter = False
    Me.pn_annpar.Visible = True
    Me.pn_annpar.VisibleIndex = 17
    '
    'pn_alfpar
    '
    Me.pn_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.pn_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.pn_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_alfpar.Caption = "Serie par."
    Me.pn_alfpar.Enabled = True
    Me.pn_alfpar.FieldName = "pn_alfpar"
    Me.pn_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_alfpar.Name = "pn_alfpar"
    Me.pn_alfpar.NTSRepositoryComboBox = Nothing
    Me.pn_alfpar.NTSRepositoryItemCheck = Nothing
    Me.pn_alfpar.NTSRepositoryItemMemo = Nothing
    Me.pn_alfpar.NTSRepositoryItemText = Nothing
    Me.pn_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_alfpar.OptionsFilter.AllowFilter = False
    Me.pn_alfpar.Visible = True
    Me.pn_alfpar.VisibleIndex = 18
    '
    'pn_numpar
    '
    Me.pn_numpar.AppearanceCell.Options.UseBackColor = True
    Me.pn_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.pn_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_numpar.Caption = "Num. par."
    Me.pn_numpar.Enabled = True
    Me.pn_numpar.FieldName = "pn_numpar"
    Me.pn_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_numpar.Name = "pn_numpar"
    Me.pn_numpar.NTSRepositoryComboBox = Nothing
    Me.pn_numpar.NTSRepositoryItemCheck = Nothing
    Me.pn_numpar.NTSRepositoryItemMemo = Nothing
    Me.pn_numpar.NTSRepositoryItemText = Nothing
    Me.pn_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_numpar.OptionsFilter.AllowFilter = False
    Me.pn_numpar.Visible = True
    Me.pn_numpar.VisibleIndex = 19
    '
    'pn_codvalu
    '
    Me.pn_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.pn_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.pn_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_codvalu.Caption = "Valuta"
    Me.pn_codvalu.Enabled = True
    Me.pn_codvalu.FieldName = "pn_codvalu"
    Me.pn_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_codvalu.Name = "pn_codvalu"
    Me.pn_codvalu.NTSRepositoryComboBox = Nothing
    Me.pn_codvalu.NTSRepositoryItemCheck = Nothing
    Me.pn_codvalu.NTSRepositoryItemMemo = Nothing
    Me.pn_codvalu.NTSRepositoryItemText = Nothing
    Me.pn_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_codvalu.OptionsFilter.AllowFilter = False
    Me.pn_codvalu.Visible = True
    Me.pn_codvalu.VisibleIndex = 20
    '
    'xxp_codvalu
    '
    Me.xxp_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xxp_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xxp_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxp_codvalu.Caption = "Descr. valuta"
    Me.xxp_codvalu.Enabled = True
    Me.xxp_codvalu.FieldName = "xxp_codvalu"
    Me.xxp_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxp_codvalu.Name = "xxp_codvalu"
    Me.xxp_codvalu.NTSRepositoryComboBox = Nothing
    Me.xxp_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xxp_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xxp_codvalu.NTSRepositoryItemText = Nothing
    Me.xxp_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxp_codvalu.OptionsFilter.AllowFilter = False
    Me.xxp_codvalu.Visible = True
    Me.xxp_codvalu.VisibleIndex = 21
    '
    'pn_cambio
    '
    Me.pn_cambio.AppearanceCell.Options.UseBackColor = True
    Me.pn_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.pn_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_cambio.Caption = "Cambio"
    Me.pn_cambio.Enabled = True
    Me.pn_cambio.FieldName = "pn_cambio"
    Me.pn_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_cambio.Name = "pn_cambio"
    Me.pn_cambio.NTSRepositoryComboBox = Nothing
    Me.pn_cambio.NTSRepositoryItemCheck = Nothing
    Me.pn_cambio.NTSRepositoryItemMemo = Nothing
    Me.pn_cambio.NTSRepositoryItemText = Nothing
    Me.pn_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_cambio.OptionsFilter.AllowFilter = False
    Me.pn_cambio.Visible = True
    Me.pn_cambio.VisibleIndex = 22
    '
    'pn_impval
    '
    Me.pn_impval.AppearanceCell.Options.UseBackColor = True
    Me.pn_impval.AppearanceCell.Options.UseTextOptions = True
    Me.pn_impval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_impval.Caption = "Imp. valuta"
    Me.pn_impval.Enabled = True
    Me.pn_impval.FieldName = "pn_impval"
    Me.pn_impval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_impval.Name = "pn_impval"
    Me.pn_impval.NTSRepositoryComboBox = Nothing
    Me.pn_impval.NTSRepositoryItemCheck = Nothing
    Me.pn_impval.NTSRepositoryItemMemo = Nothing
    Me.pn_impval.NTSRepositoryItemText = Nothing
    Me.pn_impval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_impval.OptionsFilter.AllowFilter = False
    Me.pn_impval.Visible = True
    Me.pn_impval.VisibleIndex = 23
    '
    'pn_dareval
    '
    Me.pn_dareval.AppearanceCell.Options.UseBackColor = True
    Me.pn_dareval.AppearanceCell.Options.UseTextOptions = True
    Me.pn_dareval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_dareval.Caption = "Dare val."
    Me.pn_dareval.Enabled = True
    Me.pn_dareval.FieldName = "pn_dareval"
    Me.pn_dareval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_dareval.Name = "pn_dareval"
    Me.pn_dareval.NTSRepositoryComboBox = Nothing
    Me.pn_dareval.NTSRepositoryItemCheck = Nothing
    Me.pn_dareval.NTSRepositoryItemMemo = Nothing
    Me.pn_dareval.NTSRepositoryItemText = Nothing
    Me.pn_dareval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_dareval.OptionsFilter.AllowFilter = False
    Me.pn_dareval.Visible = True
    Me.pn_dareval.VisibleIndex = 24
    '
    'pn_avereval
    '
    Me.pn_avereval.AppearanceCell.Options.UseBackColor = True
    Me.pn_avereval.AppearanceCell.Options.UseTextOptions = True
    Me.pn_avereval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_avereval.Caption = "Avere val."
    Me.pn_avereval.Enabled = True
    Me.pn_avereval.FieldName = "pn_avereval"
    Me.pn_avereval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_avereval.Name = "pn_avereval"
    Me.pn_avereval.NTSRepositoryComboBox = Nothing
    Me.pn_avereval.NTSRepositoryItemCheck = Nothing
    Me.pn_avereval.NTSRepositoryItemMemo = Nothing
    Me.pn_avereval.NTSRepositoryItemText = Nothing
    Me.pn_avereval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_avereval.OptionsFilter.AllowFilter = False
    Me.pn_avereval.Visible = True
    Me.pn_avereval.VisibleIndex = 25
    '
    'pn_tregiva
    '
    Me.pn_tregiva.AppearanceCell.Options.UseBackColor = True
    Me.pn_tregiva.AppearanceCell.Options.UseTextOptions = True
    Me.pn_tregiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_tregiva.Caption = "Reg. IVA"
    Me.pn_tregiva.Enabled = True
    Me.pn_tregiva.FieldName = "pn_tregiva"
    Me.pn_tregiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_tregiva.Name = "pn_tregiva"
    Me.pn_tregiva.NTSRepositoryComboBox = Nothing
    Me.pn_tregiva.NTSRepositoryItemCheck = Nothing
    Me.pn_tregiva.NTSRepositoryItemMemo = Nothing
    Me.pn_tregiva.NTSRepositoryItemText = Nothing
    Me.pn_tregiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_tregiva.OptionsFilter.AllowFilter = False
    Me.pn_tregiva.Visible = True
    Me.pn_tregiva.VisibleIndex = 26
    '
    'pn_nregiva
    '
    Me.pn_nregiva.AppearanceCell.Options.UseBackColor = True
    Me.pn_nregiva.AppearanceCell.Options.UseTextOptions = True
    Me.pn_nregiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_nregiva.Caption = "Num. reg. IVA"
    Me.pn_nregiva.Enabled = True
    Me.pn_nregiva.FieldName = "pn_nregiva"
    Me.pn_nregiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_nregiva.Name = "pn_nregiva"
    Me.pn_nregiva.NTSRepositoryComboBox = Nothing
    Me.pn_nregiva.NTSRepositoryItemCheck = Nothing
    Me.pn_nregiva.NTSRepositoryItemMemo = Nothing
    Me.pn_nregiva.NTSRepositoryItemText = Nothing
    Me.pn_nregiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_nregiva.OptionsFilter.AllowFilter = False
    Me.pn_nregiva.Visible = True
    Me.pn_nregiva.VisibleIndex = 27
    '
    'pn_codiva
    '
    Me.pn_codiva.AppearanceCell.Options.UseBackColor = True
    Me.pn_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.pn_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_codiva.Caption = "Cod. IVA"
    Me.pn_codiva.Enabled = True
    Me.pn_codiva.FieldName = "pn_codiva"
    Me.pn_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_codiva.Name = "pn_codiva"
    Me.pn_codiva.NTSRepositoryComboBox = Nothing
    Me.pn_codiva.NTSRepositoryItemCheck = Nothing
    Me.pn_codiva.NTSRepositoryItemMemo = Nothing
    Me.pn_codiva.NTSRepositoryItemText = Nothing
    Me.pn_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_codiva.OptionsFilter.AllowFilter = False
    Me.pn_codiva.Visible = True
    Me.pn_codiva.VisibleIndex = 28
    '
    'xxp_codiva
    '
    Me.xxp_codiva.AppearanceCell.Options.UseBackColor = True
    Me.xxp_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.xxp_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxp_codiva.Caption = "Descr. IVA"
    Me.xxp_codiva.Enabled = True
    Me.xxp_codiva.FieldName = "xxp_codiva"
    Me.xxp_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxp_codiva.Name = "xxp_codiva"
    Me.xxp_codiva.NTSRepositoryComboBox = Nothing
    Me.xxp_codiva.NTSRepositoryItemCheck = Nothing
    Me.xxp_codiva.NTSRepositoryItemMemo = Nothing
    Me.xxp_codiva.NTSRepositoryItemText = Nothing
    Me.xxp_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxp_codiva.OptionsFilter.AllowFilter = False
    Me.xxp_codiva.Visible = True
    Me.xxp_codiva.VisibleIndex = 29
    '
    'pn_aliqiva
    '
    Me.pn_aliqiva.AppearanceCell.Options.UseBackColor = True
    Me.pn_aliqiva.AppearanceCell.Options.UseTextOptions = True
    Me.pn_aliqiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_aliqiva.Caption = "Aliq. IVA"
    Me.pn_aliqiva.Enabled = True
    Me.pn_aliqiva.FieldName = "pn_aliqiva"
    Me.pn_aliqiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_aliqiva.Name = "pn_aliqiva"
    Me.pn_aliqiva.NTSRepositoryComboBox = Nothing
    Me.pn_aliqiva.NTSRepositoryItemCheck = Nothing
    Me.pn_aliqiva.NTSRepositoryItemMemo = Nothing
    Me.pn_aliqiva.NTSRepositoryItemText = Nothing
    Me.pn_aliqiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_aliqiva.OptionsFilter.AllowFilter = False
    Me.pn_aliqiva.Visible = True
    Me.pn_aliqiva.VisibleIndex = 30
    '
    'pn_indetr
    '
    Me.pn_indetr.AppearanceCell.Options.UseBackColor = True
    Me.pn_indetr.AppearanceCell.Options.UseTextOptions = True
    Me.pn_indetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_indetr.Caption = "Indetr."
    Me.pn_indetr.Enabled = True
    Me.pn_indetr.FieldName = "pn_indetr"
    Me.pn_indetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_indetr.Name = "pn_indetr"
    Me.pn_indetr.NTSRepositoryComboBox = Nothing
    Me.pn_indetr.NTSRepositoryItemCheck = Nothing
    Me.pn_indetr.NTSRepositoryItemMemo = Nothing
    Me.pn_indetr.NTSRepositoryItemText = Nothing
    Me.pn_indetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_indetr.OptionsFilter.AllowFilter = False
    Me.pn_indetr.Visible = True
    Me.pn_indetr.VisibleIndex = 31
    '
    'pn_contocf
    '
    Me.pn_contocf.AppearanceCell.Options.UseBackColor = True
    Me.pn_contocf.AppearanceCell.Options.UseTextOptions = True
    Me.pn_contocf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_contocf.Caption = "Conto C/F"
    Me.pn_contocf.Enabled = True
    Me.pn_contocf.FieldName = "pn_contocf"
    Me.pn_contocf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_contocf.Name = "pn_contocf"
    Me.pn_contocf.NTSRepositoryComboBox = Nothing
    Me.pn_contocf.NTSRepositoryItemCheck = Nothing
    Me.pn_contocf.NTSRepositoryItemMemo = Nothing
    Me.pn_contocf.NTSRepositoryItemText = Nothing
    Me.pn_contocf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_contocf.OptionsFilter.AllowFilter = False
    Me.pn_contocf.Visible = True
    Me.pn_contocf.VisibleIndex = 32
    '
    'xxp_contocf
    '
    Me.xxp_contocf.AppearanceCell.Options.UseBackColor = True
    Me.xxp_contocf.AppearanceCell.Options.UseTextOptions = True
    Me.xxp_contocf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxp_contocf.Caption = "Descr. conto C/F"
    Me.xxp_contocf.Enabled = True
    Me.xxp_contocf.FieldName = "xxp_contocf"
    Me.xxp_contocf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxp_contocf.Name = "xxp_contocf"
    Me.xxp_contocf.NTSRepositoryComboBox = Nothing
    Me.xxp_contocf.NTSRepositoryItemCheck = Nothing
    Me.xxp_contocf.NTSRepositoryItemMemo = Nothing
    Me.xxp_contocf.NTSRepositoryItemText = Nothing
    Me.xxp_contocf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxp_contocf.OptionsFilter.AllowFilter = False
    Me.xxp_contocf.Visible = True
    Me.xxp_contocf.VisibleIndex = 33
    '
    'pn_imponib
    '
    Me.pn_imponib.AppearanceCell.Options.UseBackColor = True
    Me.pn_imponib.AppearanceCell.Options.UseTextOptions = True
    Me.pn_imponib.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_imponib.Caption = "Imponib. IVA"
    Me.pn_imponib.Enabled = True
    Me.pn_imponib.FieldName = "pn_imponib"
    Me.pn_imponib.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_imponib.Name = "pn_imponib"
    Me.pn_imponib.NTSRepositoryComboBox = Nothing
    Me.pn_imponib.NTSRepositoryItemCheck = Nothing
    Me.pn_imponib.NTSRepositoryItemMemo = Nothing
    Me.pn_imponib.NTSRepositoryItemText = Nothing
    Me.pn_imponib.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_imponib.OptionsFilter.AllowFilter = False
    Me.pn_imponib.Visible = True
    Me.pn_imponib.VisibleIndex = 34
    '
    'pn_imponibval
    '
    Me.pn_imponibval.AppearanceCell.Options.UseBackColor = True
    Me.pn_imponibval.AppearanceCell.Options.UseTextOptions = True
    Me.pn_imponibval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_imponibval.Caption = "Imponib. IVA val"
    Me.pn_imponibval.Enabled = True
    Me.pn_imponibval.FieldName = "pn_imponibval"
    Me.pn_imponibval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_imponibval.Name = "pn_imponibval"
    Me.pn_imponibval.NTSRepositoryComboBox = Nothing
    Me.pn_imponibval.NTSRepositoryItemCheck = Nothing
    Me.pn_imponibval.NTSRepositoryItemMemo = Nothing
    Me.pn_imponibval.NTSRepositoryItemText = Nothing
    Me.pn_imponibval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_imponibval.OptionsFilter.AllowFilter = False
    Me.pn_imponibval.Visible = True
    Me.pn_imponibval.VisibleIndex = 35
    '
    'pn_tipacq
    '
    Me.pn_tipacq.AppearanceCell.Options.UseBackColor = True
    Me.pn_tipacq.AppearanceCell.Options.UseTextOptions = True
    Me.pn_tipacq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_tipacq.Caption = "Tipo acquisto"
    Me.pn_tipacq.Enabled = True
    Me.pn_tipacq.FieldName = "pn_tipacq"
    Me.pn_tipacq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_tipacq.Name = "pn_tipacq"
    Me.pn_tipacq.NTSRepositoryComboBox = Nothing
    Me.pn_tipacq.NTSRepositoryItemCheck = Nothing
    Me.pn_tipacq.NTSRepositoryItemMemo = Nothing
    Me.pn_tipacq.NTSRepositoryItemText = Nothing
    Me.pn_tipacq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_tipacq.OptionsFilter.AllowFilter = False
    Me.pn_tipacq.Visible = True
    Me.pn_tipacq.VisibleIndex = 36
    '
    'pn_numpro
    '
    Me.pn_numpro.AppearanceCell.Options.UseBackColor = True
    Me.pn_numpro.AppearanceCell.Options.UseTextOptions = True
    Me.pn_numpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_numpro.Caption = "Num. prot."
    Me.pn_numpro.Enabled = True
    Me.pn_numpro.FieldName = "pn_numpro"
    Me.pn_numpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_numpro.Name = "pn_numpro"
    Me.pn_numpro.NTSRepositoryComboBox = Nothing
    Me.pn_numpro.NTSRepositoryItemCheck = Nothing
    Me.pn_numpro.NTSRepositoryItemMemo = Nothing
    Me.pn_numpro.NTSRepositoryItemText = Nothing
    Me.pn_numpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_numpro.OptionsFilter.AllowFilter = False
    Me.pn_numpro.Visible = True
    Me.pn_numpro.VisibleIndex = 37
    '
    'pn_alfpro
    '
    Me.pn_alfpro.AppearanceCell.Options.UseBackColor = True
    Me.pn_alfpro.AppearanceCell.Options.UseTextOptions = True
    Me.pn_alfpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_alfpro.Caption = "Serie prot."
    Me.pn_alfpro.Enabled = True
    Me.pn_alfpro.FieldName = "pn_alfpro"
    Me.pn_alfpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_alfpro.Name = "pn_alfpro"
    Me.pn_alfpro.NTSRepositoryComboBox = Nothing
    Me.pn_alfpro.NTSRepositoryItemCheck = Nothing
    Me.pn_alfpro.NTSRepositoryItemMemo = Nothing
    Me.pn_alfpro.NTSRepositoryItemText = Nothing
    Me.pn_alfpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_alfpro.OptionsFilter.AllowFilter = False
    Me.pn_alfpro.Visible = True
    Me.pn_alfpro.VisibleIndex = 38
    '
    'pn_integr
    '
    Me.pn_integr.AppearanceCell.Options.UseBackColor = True
    Me.pn_integr.AppearanceCell.Options.UseTextOptions = True
    Me.pn_integr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_integr.Caption = "Integrativa"
    Me.pn_integr.Enabled = True
    Me.pn_integr.FieldName = "pn_integr"
    Me.pn_integr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_integr.Name = "pn_integr"
    Me.pn_integr.NTSRepositoryComboBox = Nothing
    Me.pn_integr.NTSRepositoryItemCheck = Nothing
    Me.pn_integr.NTSRepositoryItemMemo = Nothing
    Me.pn_integr.NTSRepositoryItemText = Nothing
    Me.pn_integr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_integr.OptionsFilter.AllowFilter = False
    Me.pn_integr.Visible = True
    Me.pn_integr.VisibleIndex = 39
    '
    'pn_fllg
    '
    Me.pn_fllg.AppearanceCell.Options.UseBackColor = True
    Me.pn_fllg.AppearanceCell.Options.UseTextOptions = True
    Me.pn_fllg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_fllg.Caption = "Stampato LG"
    Me.pn_fllg.Enabled = True
    Me.pn_fllg.FieldName = "pn_fllg"
    Me.pn_fllg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_fllg.Name = "pn_fllg"
    Me.pn_fllg.NTSRepositoryComboBox = Nothing
    Me.pn_fllg.NTSRepositoryItemCheck = Nothing
    Me.pn_fllg.NTSRepositoryItemMemo = Nothing
    Me.pn_fllg.NTSRepositoryItemText = Nothing
    Me.pn_fllg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_fllg.OptionsFilter.AllowFilter = False
    Me.pn_fllg.Visible = True
    Me.pn_fllg.VisibleIndex = 40
    '
    'pn_dtcomiva
    '
    Me.pn_dtcomiva.AppearanceCell.Options.UseBackColor = True
    Me.pn_dtcomiva.AppearanceCell.Options.UseTextOptions = True
    Me.pn_dtcomiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_dtcomiva.Caption = "Data comp. IVA"
    Me.pn_dtcomiva.Enabled = True
    Me.pn_dtcomiva.FieldName = "pn_dtcomiva"
    Me.pn_dtcomiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_dtcomiva.Name = "pn_dtcomiva"
    Me.pn_dtcomiva.NTSRepositoryComboBox = Nothing
    Me.pn_dtcomiva.NTSRepositoryItemCheck = Nothing
    Me.pn_dtcomiva.NTSRepositoryItemMemo = Nothing
    Me.pn_dtcomiva.NTSRepositoryItemText = Nothing
    Me.pn_dtcomiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_dtcomiva.OptionsFilter.AllowFilter = False
    Me.pn_dtcomiva.Visible = True
    Me.pn_dtcomiva.VisibleIndex = 41
    '
    'pn_dtvaluta
    '
    Me.pn_dtvaluta.AppearanceCell.Options.UseBackColor = True
    Me.pn_dtvaluta.AppearanceCell.Options.UseTextOptions = True
    Me.pn_dtvaluta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_dtvaluta.Caption = "Data valuta"
    Me.pn_dtvaluta.Enabled = True
    Me.pn_dtvaluta.FieldName = "pn_dtvaluta"
    Me.pn_dtvaluta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_dtvaluta.Name = "pn_dtvaluta"
    Me.pn_dtvaluta.NTSRepositoryComboBox = Nothing
    Me.pn_dtvaluta.NTSRepositoryItemCheck = Nothing
    Me.pn_dtvaluta.NTSRepositoryItemMemo = Nothing
    Me.pn_dtvaluta.NTSRepositoryItemText = Nothing
    Me.pn_dtvaluta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_dtvaluta.OptionsFilter.AllowFilter = False
    Me.pn_dtvaluta.Visible = True
    Me.pn_dtvaluta.VisibleIndex = 42
    '
    'pn_dtcomplaf
    '
    Me.pn_dtcomplaf.AppearanceCell.Options.UseBackColor = True
    Me.pn_dtcomplaf.AppearanceCell.Options.UseTextOptions = True
    Me.pn_dtcomplaf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_dtcomplaf.Caption = "Data plafond"
    Me.pn_dtcomplaf.Enabled = True
    Me.pn_dtcomplaf.FieldName = "pn_dtcomplaf"
    Me.pn_dtcomplaf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_dtcomplaf.Name = "pn_dtcomplaf"
    Me.pn_dtcomplaf.NTSRepositoryComboBox = Nothing
    Me.pn_dtcomplaf.NTSRepositoryItemCheck = Nothing
    Me.pn_dtcomplaf.NTSRepositoryItemMemo = Nothing
    Me.pn_dtcomplaf.NTSRepositoryItemText = Nothing
    Me.pn_dtcomplaf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_dtcomplaf.OptionsFilter.AllowFilter = False
    Me.pn_dtcomplaf.Visible = True
    Me.pn_dtcomplaf.VisibleIndex = 43
    '
    'pn_datini
    '
    Me.pn_datini.AppearanceCell.Options.UseBackColor = True
    Me.pn_datini.AppearanceCell.Options.UseTextOptions = True
    Me.pn_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_datini.Caption = "Data inizio"
    Me.pn_datini.Enabled = True
    Me.pn_datini.FieldName = "pn_datini"
    Me.pn_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_datini.Name = "pn_datini"
    Me.pn_datini.NTSRepositoryComboBox = Nothing
    Me.pn_datini.NTSRepositoryItemCheck = Nothing
    Me.pn_datini.NTSRepositoryItemMemo = Nothing
    Me.pn_datini.NTSRepositoryItemText = Nothing
    Me.pn_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_datini.OptionsFilter.AllowFilter = False
    Me.pn_datini.Visible = True
    Me.pn_datini.VisibleIndex = 44
    '
    'pn_datfin
    '
    Me.pn_datfin.AppearanceCell.Options.UseBackColor = True
    Me.pn_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.pn_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_datfin.Caption = "Data fine"
    Me.pn_datfin.Enabled = True
    Me.pn_datfin.FieldName = "pn_datfin"
    Me.pn_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_datfin.Name = "pn_datfin"
    Me.pn_datfin.NTSRepositoryComboBox = Nothing
    Me.pn_datfin.NTSRepositoryItemCheck = Nothing
    Me.pn_datfin.NTSRepositoryItemMemo = Nothing
    Me.pn_datfin.NTSRepositoryItemText = Nothing
    Me.pn_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_datfin.OptionsFilter.AllowFilter = False
    Me.pn_datfin.Visible = True
    Me.pn_datfin.VisibleIndex = 45
    '
    'pn_ultagg
    '
    Me.pn_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.pn_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.pn_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_ultagg.Caption = "Data ult. agg."
    Me.pn_ultagg.Enabled = True
    Me.pn_ultagg.FieldName = "pn_ultagg"
    Me.pn_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_ultagg.Name = "pn_ultagg"
    Me.pn_ultagg.NTSRepositoryComboBox = Nothing
    Me.pn_ultagg.NTSRepositoryItemCheck = Nothing
    Me.pn_ultagg.NTSRepositoryItemMemo = Nothing
    Me.pn_ultagg.NTSRepositoryItemText = Nothing
    Me.pn_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_ultagg.OptionsFilter.AllowFilter = False
    Me.pn_ultagg.Visible = True
    Me.pn_ultagg.VisibleIndex = 46
    '
    'pn_opnome
    '
    Me.pn_opnome.AppearanceCell.Options.UseBackColor = True
    Me.pn_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.pn_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_opnome.Caption = "Operatore"
    Me.pn_opnome.Enabled = True
    Me.pn_opnome.FieldName = "pn_opnome"
    Me.pn_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_opnome.Name = "pn_opnome"
    Me.pn_opnome.NTSRepositoryComboBox = Nothing
    Me.pn_opnome.NTSRepositoryItemCheck = Nothing
    Me.pn_opnome.NTSRepositoryItemMemo = Nothing
    Me.pn_opnome.NTSRepositoryItemText = Nothing
    Me.pn_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_opnome.OptionsFilter.AllowFilter = False
    Me.pn_opnome.Visible = True
    Me.pn_opnome.VisibleIndex = 47
    '
    'pn_escomp
    '
    Me.pn_escomp.AppearanceCell.Options.UseBackColor = True
    Me.pn_escomp.AppearanceCell.Options.UseTextOptions = True
    Me.pn_escomp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_escomp.Caption = "Es. comp."
    Me.pn_escomp.Enabled = True
    Me.pn_escomp.FieldName = "pn_escomp"
    Me.pn_escomp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_escomp.Name = "pn_escomp"
    Me.pn_escomp.NTSRepositoryComboBox = Nothing
    Me.pn_escomp.NTSRepositoryItemCheck = Nothing
    Me.pn_escomp.NTSRepositoryItemMemo = Nothing
    Me.pn_escomp.NTSRepositoryItemText = Nothing
    Me.pn_escomp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_escomp.OptionsFilter.AllowFilter = False
    Me.pn_escomp.Visible = True
    Me.pn_escomp.VisibleIndex = 48
    '
    'NtsSplitter3
    '
    Me.NtsSplitter3.BackColor = System.Drawing.SystemColors.ActiveCaption
    Me.NtsSplitter3.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.NtsSplitter3.Location = New System.Drawing.Point(0, 315)
    Me.NtsSplitter3.MinExtra = 50
    Me.NtsSplitter3.Name = "NtsSplitter3"
    Me.NtsSplitter3.Size = New System.Drawing.Size(782, 3)
    Me.NtsSplitter3.TabIndex = 5
    Me.NtsSplitter3.TabStop = False
    '
    'grScad
    '
    Me.grScad.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.grScad.EmbeddedNavigator.Name = ""
    Me.grScad.Location = New System.Drawing.Point(0, 318)
    Me.grScad.MainView = Me.grvScad
    Me.grScad.Name = "grScad"
    Me.grScad.Size = New System.Drawing.Size(782, 104)
    Me.grScad.TabIndex = 0
    Me.grScad.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvScad})
    '
    'grvScad
    '
    Me.grvScad.ActiveFilterEnabled = False
    Me.grvScad.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.sc_conto, Me.xxc_conto, Me.sc_datsca, Me.sc_importoda, Me.sc_impvalda, Me.sc_annpar, Me.sc_alfpar, Me.sc_numpar, Me.sc_numrata, Me.sc_darave, Me.sc_flsaldato, Me.sc_datdoc, Me.sc_alfdoc, Me.sc_numdoc, Me.sc_codpaga, Me.xxc_codpaga, Me.sc_tippaga, Me.sc_descr, Me.sc_causale, Me.xxc_causale, Me.sc_codvalu, Me.xxc_codvalu, Me.sc_cambio, Me.sc_insolu, Me.sc_codbanc, Me.xxc_codbanc, Me.sc_abi, Me.sc_cab, Me.sc_banc1, Me.sc_banc2, Me.sc_numcc, Me.sc_cin, Me.sc_prefiban, Me.sc_iban, Me.sc_alfpro, Me.sc_numprot, Me.sc_codcage, Me.xxc_codcage, Me.sc_controp, Me.xxc_controp, Me.sc_commeca, Me.xxc_commeca, Me.sc_subcommeca, Me.sc_fldis, Me.sc_dtdist, Me.sc_numdist, Me.sc_opdist, Me.sc_datreg, Me.sc_numreg, Me.sc_dtsaldato, Me.sc_rgsaldato, Me.sc_integr})
    Me.grvScad.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvScad.Enabled = True
    Me.grvScad.GridControl = Me.grScad
    Me.grvScad.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvScad.Name = "grvScad"
    Me.grvScad.NTSAllowDelete = True
    Me.grvScad.NTSAllowInsert = True
    Me.grvScad.NTSAllowUpdate = True
    Me.grvScad.NTSMenuContext = Nothing
    Me.grvScad.OptionsCustomization.AllowRowSizing = True
    Me.grvScad.OptionsFilter.AllowFilterEditor = False
    Me.grvScad.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvScad.OptionsNavigation.UseTabKey = False
    Me.grvScad.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvScad.OptionsView.ColumnAutoWidth = False
    Me.grvScad.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvScad.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvScad.OptionsView.ShowGroupPanel = False
    Me.grvScad.RowHeight = 14
    '
    'sc_conto
    '
    Me.sc_conto.AppearanceCell.Options.UseBackColor = True
    Me.sc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.sc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_conto.Caption = "Conto"
    Me.sc_conto.Enabled = True
    Me.sc_conto.FieldName = "sc_conto"
    Me.sc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_conto.Name = "sc_conto"
    Me.sc_conto.NTSRepositoryComboBox = Nothing
    Me.sc_conto.NTSRepositoryItemCheck = Nothing
    Me.sc_conto.NTSRepositoryItemMemo = Nothing
    Me.sc_conto.NTSRepositoryItemText = Nothing
    Me.sc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_conto.OptionsFilter.AllowFilter = False
    Me.sc_conto.Visible = True
    Me.sc_conto.VisibleIndex = 0
    Me.sc_conto.Width = 70
    '
    'xxc_conto
    '
    Me.xxc_conto.AppearanceCell.Options.UseBackColor = True
    Me.xxc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_conto.Caption = "Descr. conto"
    Me.xxc_conto.Enabled = True
    Me.xxc_conto.FieldName = "xxc_conto"
    Me.xxc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_conto.Name = "xxc_conto"
    Me.xxc_conto.NTSRepositoryComboBox = Nothing
    Me.xxc_conto.NTSRepositoryItemCheck = Nothing
    Me.xxc_conto.NTSRepositoryItemMemo = Nothing
    Me.xxc_conto.NTSRepositoryItemText = Nothing
    Me.xxc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_conto.OptionsFilter.AllowFilter = False
    Me.xxc_conto.Visible = True
    Me.xxc_conto.VisibleIndex = 1
    Me.xxc_conto.Width = 70
    '
    'sc_datsca
    '
    Me.sc_datsca.AppearanceCell.Options.UseBackColor = True
    Me.sc_datsca.AppearanceCell.Options.UseTextOptions = True
    Me.sc_datsca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_datsca.Caption = "Data scad."
    Me.sc_datsca.CustomizationCaption = "sc_datsca"
    Me.sc_datsca.Enabled = True
    Me.sc_datsca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_datsca.Name = "sc_datsca"
    Me.sc_datsca.NTSRepositoryComboBox = Nothing
    Me.sc_datsca.NTSRepositoryItemCheck = Nothing
    Me.sc_datsca.NTSRepositoryItemMemo = Nothing
    Me.sc_datsca.NTSRepositoryItemText = Nothing
    Me.sc_datsca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_datsca.OptionsFilter.AllowFilter = False
    Me.sc_datsca.Visible = True
    Me.sc_datsca.VisibleIndex = 2
    '
    'sc_importoda
    '
    Me.sc_importoda.AppearanceCell.Options.UseBackColor = True
    Me.sc_importoda.AppearanceCell.Options.UseTextOptions = True
    Me.sc_importoda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_importoda.Caption = "Importo"
    Me.sc_importoda.Enabled = True
    Me.sc_importoda.FieldName = "sc_importoda"
    Me.sc_importoda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_importoda.Name = "sc_importoda"
    Me.sc_importoda.NTSRepositoryComboBox = Nothing
    Me.sc_importoda.NTSRepositoryItemCheck = Nothing
    Me.sc_importoda.NTSRepositoryItemMemo = Nothing
    Me.sc_importoda.NTSRepositoryItemText = Nothing
    Me.sc_importoda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_importoda.OptionsFilter.AllowFilter = False
    Me.sc_importoda.Visible = True
    Me.sc_importoda.VisibleIndex = 3
    '
    'sc_impvalda
    '
    Me.sc_impvalda.AppearanceCell.Options.UseBackColor = True
    Me.sc_impvalda.AppearanceCell.Options.UseTextOptions = True
    Me.sc_impvalda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_impvalda.Caption = "Importo valuta"
    Me.sc_impvalda.Enabled = True
    Me.sc_impvalda.FieldName = "sc_impvalda"
    Me.sc_impvalda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_impvalda.Name = "sc_impvalda"
    Me.sc_impvalda.NTSRepositoryComboBox = Nothing
    Me.sc_impvalda.NTSRepositoryItemCheck = Nothing
    Me.sc_impvalda.NTSRepositoryItemMemo = Nothing
    Me.sc_impvalda.NTSRepositoryItemText = Nothing
    Me.sc_impvalda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_impvalda.OptionsFilter.AllowFilter = False
    Me.sc_impvalda.Visible = True
    Me.sc_impvalda.VisibleIndex = 4
    '
    'sc_annpar
    '
    Me.sc_annpar.AppearanceCell.Options.UseBackColor = True
    Me.sc_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.sc_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_annpar.Caption = "Anno par"
    Me.sc_annpar.Enabled = True
    Me.sc_annpar.FieldName = "sc_annpar"
    Me.sc_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_annpar.Name = "sc_annpar"
    Me.sc_annpar.NTSRepositoryComboBox = Nothing
    Me.sc_annpar.NTSRepositoryItemCheck = Nothing
    Me.sc_annpar.NTSRepositoryItemMemo = Nothing
    Me.sc_annpar.NTSRepositoryItemText = Nothing
    Me.sc_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_annpar.OptionsFilter.AllowFilter = False
    Me.sc_annpar.Visible = True
    Me.sc_annpar.VisibleIndex = 5
    '
    'sc_alfpar
    '
    Me.sc_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.sc_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.sc_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_alfpar.Caption = "Serie par"
    Me.sc_alfpar.Enabled = True
    Me.sc_alfpar.FieldName = "sc_alfpar"
    Me.sc_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_alfpar.Name = "sc_alfpar"
    Me.sc_alfpar.NTSRepositoryComboBox = Nothing
    Me.sc_alfpar.NTSRepositoryItemCheck = Nothing
    Me.sc_alfpar.NTSRepositoryItemMemo = Nothing
    Me.sc_alfpar.NTSRepositoryItemText = Nothing
    Me.sc_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_alfpar.OptionsFilter.AllowFilter = False
    Me.sc_alfpar.Visible = True
    Me.sc_alfpar.VisibleIndex = 6
    '
    'sc_numpar
    '
    Me.sc_numpar.AppearanceCell.Options.UseBackColor = True
    Me.sc_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numpar.Caption = "Num. par"
    Me.sc_numpar.Enabled = True
    Me.sc_numpar.FieldName = "sc_numpar"
    Me.sc_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numpar.Name = "sc_numpar"
    Me.sc_numpar.NTSRepositoryComboBox = Nothing
    Me.sc_numpar.NTSRepositoryItemCheck = Nothing
    Me.sc_numpar.NTSRepositoryItemMemo = Nothing
    Me.sc_numpar.NTSRepositoryItemText = Nothing
    Me.sc_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numpar.OptionsFilter.AllowFilter = False
    Me.sc_numpar.Visible = True
    Me.sc_numpar.VisibleIndex = 7
    '
    'sc_numrata
    '
    Me.sc_numrata.AppearanceCell.Options.UseBackColor = True
    Me.sc_numrata.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numrata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numrata.Caption = "Rata"
    Me.sc_numrata.Enabled = True
    Me.sc_numrata.FieldName = "sc_numrata"
    Me.sc_numrata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numrata.Name = "sc_numrata"
    Me.sc_numrata.NTSRepositoryComboBox = Nothing
    Me.sc_numrata.NTSRepositoryItemCheck = Nothing
    Me.sc_numrata.NTSRepositoryItemMemo = Nothing
    Me.sc_numrata.NTSRepositoryItemText = Nothing
    Me.sc_numrata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numrata.OptionsFilter.AllowFilter = False
    Me.sc_numrata.Visible = True
    Me.sc_numrata.VisibleIndex = 8
    '
    'sc_darave
    '
    Me.sc_darave.AppearanceCell.Options.UseBackColor = True
    Me.sc_darave.AppearanceCell.Options.UseTextOptions = True
    Me.sc_darave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_darave.Caption = "D/A"
    Me.sc_darave.Enabled = True
    Me.sc_darave.FieldName = "sc_darave"
    Me.sc_darave.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_darave.Name = "sc_darave"
    Me.sc_darave.NTSRepositoryComboBox = Nothing
    Me.sc_darave.NTSRepositoryItemCheck = Nothing
    Me.sc_darave.NTSRepositoryItemMemo = Nothing
    Me.sc_darave.NTSRepositoryItemText = Nothing
    Me.sc_darave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_darave.OptionsFilter.AllowFilter = False
    Me.sc_darave.Visible = True
    Me.sc_darave.VisibleIndex = 9
    '
    'sc_flsaldato
    '
    Me.sc_flsaldato.AppearanceCell.Options.UseBackColor = True
    Me.sc_flsaldato.AppearanceCell.Options.UseTextOptions = True
    Me.sc_flsaldato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_flsaldato.Caption = "Saldato"
    Me.sc_flsaldato.Enabled = True
    Me.sc_flsaldato.FieldName = "sc_flsaldato"
    Me.sc_flsaldato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_flsaldato.Name = "sc_flsaldato"
    Me.sc_flsaldato.NTSRepositoryComboBox = Nothing
    Me.sc_flsaldato.NTSRepositoryItemCheck = Nothing
    Me.sc_flsaldato.NTSRepositoryItemMemo = Nothing
    Me.sc_flsaldato.NTSRepositoryItemText = Nothing
    Me.sc_flsaldato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_flsaldato.OptionsFilter.AllowFilter = False
    Me.sc_flsaldato.Visible = True
    Me.sc_flsaldato.VisibleIndex = 10
    '
    'sc_datdoc
    '
    Me.sc_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.sc_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.sc_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_datdoc.Caption = "Data doc."
    Me.sc_datdoc.Enabled = True
    Me.sc_datdoc.FieldName = "sc_datdoc"
    Me.sc_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_datdoc.Name = "sc_datdoc"
    Me.sc_datdoc.NTSRepositoryComboBox = Nothing
    Me.sc_datdoc.NTSRepositoryItemCheck = Nothing
    Me.sc_datdoc.NTSRepositoryItemMemo = Nothing
    Me.sc_datdoc.NTSRepositoryItemText = Nothing
    Me.sc_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_datdoc.OptionsFilter.AllowFilter = False
    Me.sc_datdoc.Visible = True
    Me.sc_datdoc.VisibleIndex = 11
    '
    'sc_alfdoc
    '
    Me.sc_alfdoc.AppearanceCell.Options.UseBackColor = True
    Me.sc_alfdoc.AppearanceCell.Options.UseTextOptions = True
    Me.sc_alfdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_alfdoc.Caption = "Serie doc."
    Me.sc_alfdoc.Enabled = True
    Me.sc_alfdoc.FieldName = "sc_alfdoc"
    Me.sc_alfdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_alfdoc.Name = "sc_alfdoc"
    Me.sc_alfdoc.NTSRepositoryComboBox = Nothing
    Me.sc_alfdoc.NTSRepositoryItemCheck = Nothing
    Me.sc_alfdoc.NTSRepositoryItemMemo = Nothing
    Me.sc_alfdoc.NTSRepositoryItemText = Nothing
    Me.sc_alfdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_alfdoc.OptionsFilter.AllowFilter = False
    Me.sc_alfdoc.Visible = True
    Me.sc_alfdoc.VisibleIndex = 12
    '
    'sc_numdoc
    '
    Me.sc_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.sc_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numdoc.Caption = "Num. doc."
    Me.sc_numdoc.Enabled = True
    Me.sc_numdoc.FieldName = "sc_numdoc"
    Me.sc_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numdoc.Name = "sc_numdoc"
    Me.sc_numdoc.NTSRepositoryComboBox = Nothing
    Me.sc_numdoc.NTSRepositoryItemCheck = Nothing
    Me.sc_numdoc.NTSRepositoryItemMemo = Nothing
    Me.sc_numdoc.NTSRepositoryItemText = Nothing
    Me.sc_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numdoc.OptionsFilter.AllowFilter = False
    Me.sc_numdoc.Visible = True
    Me.sc_numdoc.VisibleIndex = 13
    '
    'sc_codpaga
    '
    Me.sc_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.sc_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.sc_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_codpaga.Caption = "Cod. pag."
    Me.sc_codpaga.Enabled = True
    Me.sc_codpaga.FieldName = "sc_codpaga"
    Me.sc_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_codpaga.Name = "sc_codpaga"
    Me.sc_codpaga.NTSRepositoryComboBox = Nothing
    Me.sc_codpaga.NTSRepositoryItemCheck = Nothing
    Me.sc_codpaga.NTSRepositoryItemMemo = Nothing
    Me.sc_codpaga.NTSRepositoryItemText = Nothing
    Me.sc_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_codpaga.OptionsFilter.AllowFilter = False
    Me.sc_codpaga.Visible = True
    Me.sc_codpaga.VisibleIndex = 14
    '
    'xxc_codpaga
    '
    Me.xxc_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xxc_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_codpaga.Caption = "Descr. pag."
    Me.xxc_codpaga.Enabled = True
    Me.xxc_codpaga.FieldName = "xxc_codpaga"
    Me.xxc_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_codpaga.Name = "xxc_codpaga"
    Me.xxc_codpaga.NTSRepositoryComboBox = Nothing
    Me.xxc_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xxc_codpaga.NTSRepositoryItemMemo = Nothing
    Me.xxc_codpaga.NTSRepositoryItemText = Nothing
    Me.xxc_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_codpaga.OptionsFilter.AllowFilter = False
    Me.xxc_codpaga.Visible = True
    Me.xxc_codpaga.VisibleIndex = 15
    '
    'sc_tippaga
    '
    Me.sc_tippaga.AppearanceCell.Options.UseBackColor = True
    Me.sc_tippaga.AppearanceCell.Options.UseTextOptions = True
    Me.sc_tippaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_tippaga.Caption = "Tipo pag."
    Me.sc_tippaga.Enabled = True
    Me.sc_tippaga.FieldName = "sc_tippaga"
    Me.sc_tippaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_tippaga.Name = "sc_tippaga"
    Me.sc_tippaga.NTSRepositoryComboBox = Nothing
    Me.sc_tippaga.NTSRepositoryItemCheck = Nothing
    Me.sc_tippaga.NTSRepositoryItemMemo = Nothing
    Me.sc_tippaga.NTSRepositoryItemText = Nothing
    Me.sc_tippaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_tippaga.OptionsFilter.AllowFilter = False
    Me.sc_tippaga.Visible = True
    Me.sc_tippaga.VisibleIndex = 16
    '
    'sc_descr
    '
    Me.sc_descr.AppearanceCell.Options.UseBackColor = True
    Me.sc_descr.AppearanceCell.Options.UseTextOptions = True
    Me.sc_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_descr.Caption = "Descr."
    Me.sc_descr.Enabled = True
    Me.sc_descr.FieldName = "sc_descr"
    Me.sc_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_descr.Name = "sc_descr"
    Me.sc_descr.NTSRepositoryComboBox = Nothing
    Me.sc_descr.NTSRepositoryItemCheck = Nothing
    Me.sc_descr.NTSRepositoryItemMemo = Nothing
    Me.sc_descr.NTSRepositoryItemText = Nothing
    Me.sc_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_descr.OptionsFilter.AllowFilter = False
    Me.sc_descr.Visible = True
    Me.sc_descr.VisibleIndex = 17
    '
    'sc_causale
    '
    Me.sc_causale.AppearanceCell.Options.UseBackColor = True
    Me.sc_causale.AppearanceCell.Options.UseTextOptions = True
    Me.sc_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_causale.Caption = "Causale"
    Me.sc_causale.Enabled = True
    Me.sc_causale.FieldName = "sc_causale"
    Me.sc_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_causale.Name = "sc_causale"
    Me.sc_causale.NTSRepositoryComboBox = Nothing
    Me.sc_causale.NTSRepositoryItemCheck = Nothing
    Me.sc_causale.NTSRepositoryItemMemo = Nothing
    Me.sc_causale.NTSRepositoryItemText = Nothing
    Me.sc_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_causale.OptionsFilter.AllowFilter = False
    Me.sc_causale.Visible = True
    Me.sc_causale.VisibleIndex = 18
    '
    'xxc_causale
    '
    Me.xxc_causale.AppearanceCell.Options.UseBackColor = True
    Me.xxc_causale.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_causale.Caption = "Descr. causale"
    Me.xxc_causale.Enabled = True
    Me.xxc_causale.FieldName = "xxc_causale"
    Me.xxc_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_causale.Name = "xxc_causale"
    Me.xxc_causale.NTSRepositoryComboBox = Nothing
    Me.xxc_causale.NTSRepositoryItemCheck = Nothing
    Me.xxc_causale.NTSRepositoryItemMemo = Nothing
    Me.xxc_causale.NTSRepositoryItemText = Nothing
    Me.xxc_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_causale.OptionsFilter.AllowFilter = False
    Me.xxc_causale.Visible = True
    Me.xxc_causale.VisibleIndex = 19
    '
    'sc_codvalu
    '
    Me.sc_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.sc_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.sc_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_codvalu.Caption = "Valuta"
    Me.sc_codvalu.Enabled = True
    Me.sc_codvalu.FieldName = "sc_codvalu"
    Me.sc_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_codvalu.Name = "sc_codvalu"
    Me.sc_codvalu.NTSRepositoryComboBox = Nothing
    Me.sc_codvalu.NTSRepositoryItemCheck = Nothing
    Me.sc_codvalu.NTSRepositoryItemMemo = Nothing
    Me.sc_codvalu.NTSRepositoryItemText = Nothing
    Me.sc_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_codvalu.OptionsFilter.AllowFilter = False
    Me.sc_codvalu.Visible = True
    Me.sc_codvalu.VisibleIndex = 20
    '
    'xxc_codvalu
    '
    Me.xxc_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xxc_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_codvalu.Caption = "Descr. valuta"
    Me.xxc_codvalu.Enabled = True
    Me.xxc_codvalu.FieldName = "xxc_codvalu"
    Me.xxc_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_codvalu.Name = "xxc_codvalu"
    Me.xxc_codvalu.NTSRepositoryComboBox = Nothing
    Me.xxc_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xxc_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xxc_codvalu.NTSRepositoryItemText = Nothing
    Me.xxc_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_codvalu.OptionsFilter.AllowFilter = False
    Me.xxc_codvalu.Visible = True
    Me.xxc_codvalu.VisibleIndex = 21
    '
    'sc_cambio
    '
    Me.sc_cambio.AppearanceCell.Options.UseBackColor = True
    Me.sc_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.sc_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_cambio.Caption = "Cambio"
    Me.sc_cambio.Enabled = True
    Me.sc_cambio.FieldName = "sc_cambio"
    Me.sc_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_cambio.Name = "sc_cambio"
    Me.sc_cambio.NTSRepositoryComboBox = Nothing
    Me.sc_cambio.NTSRepositoryItemCheck = Nothing
    Me.sc_cambio.NTSRepositoryItemMemo = Nothing
    Me.sc_cambio.NTSRepositoryItemText = Nothing
    Me.sc_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_cambio.OptionsFilter.AllowFilter = False
    Me.sc_cambio.Visible = True
    Me.sc_cambio.VisibleIndex = 22
    '
    'sc_insolu
    '
    Me.sc_insolu.AppearanceCell.Options.UseBackColor = True
    Me.sc_insolu.AppearanceCell.Options.UseTextOptions = True
    Me.sc_insolu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_insolu.Caption = "Insoluto"
    Me.sc_insolu.Enabled = True
    Me.sc_insolu.FieldName = "sc_insolu"
    Me.sc_insolu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_insolu.Name = "sc_insolu"
    Me.sc_insolu.NTSRepositoryComboBox = Nothing
    Me.sc_insolu.NTSRepositoryItemCheck = Nothing
    Me.sc_insolu.NTSRepositoryItemMemo = Nothing
    Me.sc_insolu.NTSRepositoryItemText = Nothing
    Me.sc_insolu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_insolu.OptionsFilter.AllowFilter = False
    Me.sc_insolu.Visible = True
    Me.sc_insolu.VisibleIndex = 23
    '
    'sc_codbanc
    '
    Me.sc_codbanc.AppearanceCell.Options.UseBackColor = True
    Me.sc_codbanc.AppearanceCell.Options.UseTextOptions = True
    Me.sc_codbanc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_codbanc.Caption = "Ns. banca"
    Me.sc_codbanc.Enabled = True
    Me.sc_codbanc.FieldName = "sc_codbanc"
    Me.sc_codbanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_codbanc.Name = "sc_codbanc"
    Me.sc_codbanc.NTSRepositoryComboBox = Nothing
    Me.sc_codbanc.NTSRepositoryItemCheck = Nothing
    Me.sc_codbanc.NTSRepositoryItemMemo = Nothing
    Me.sc_codbanc.NTSRepositoryItemText = Nothing
    Me.sc_codbanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_codbanc.OptionsFilter.AllowFilter = False
    Me.sc_codbanc.Visible = True
    Me.sc_codbanc.VisibleIndex = 24
    '
    'xxc_codbanc
    '
    Me.xxc_codbanc.AppearanceCell.Options.UseBackColor = True
    Me.xxc_codbanc.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_codbanc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_codbanc.Caption = "Descr. ns. banca"
    Me.xxc_codbanc.Enabled = True
    Me.xxc_codbanc.FieldName = "xxc_codbanc"
    Me.xxc_codbanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_codbanc.Name = "xxc_codbanc"
    Me.xxc_codbanc.NTSRepositoryComboBox = Nothing
    Me.xxc_codbanc.NTSRepositoryItemCheck = Nothing
    Me.xxc_codbanc.NTSRepositoryItemMemo = Nothing
    Me.xxc_codbanc.NTSRepositoryItemText = Nothing
    Me.xxc_codbanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_codbanc.OptionsFilter.AllowFilter = False
    Me.xxc_codbanc.Visible = True
    Me.xxc_codbanc.VisibleIndex = 25
    '
    'sc_abi
    '
    Me.sc_abi.AppearanceCell.Options.UseBackColor = True
    Me.sc_abi.AppearanceCell.Options.UseTextOptions = True
    Me.sc_abi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_abi.Caption = "Abi"
    Me.sc_abi.Enabled = True
    Me.sc_abi.FieldName = "sc_abi"
    Me.sc_abi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_abi.Name = "sc_abi"
    Me.sc_abi.NTSRepositoryComboBox = Nothing
    Me.sc_abi.NTSRepositoryItemCheck = Nothing
    Me.sc_abi.NTSRepositoryItemMemo = Nothing
    Me.sc_abi.NTSRepositoryItemText = Nothing
    Me.sc_abi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_abi.OptionsFilter.AllowFilter = False
    Me.sc_abi.Visible = True
    Me.sc_abi.VisibleIndex = 26
    '
    'sc_cab
    '
    Me.sc_cab.AppearanceCell.Options.UseBackColor = True
    Me.sc_cab.AppearanceCell.Options.UseTextOptions = True
    Me.sc_cab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_cab.Caption = "Cab"
    Me.sc_cab.Enabled = True
    Me.sc_cab.FieldName = "sc_cab"
    Me.sc_cab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_cab.Name = "sc_cab"
    Me.sc_cab.NTSRepositoryComboBox = Nothing
    Me.sc_cab.NTSRepositoryItemCheck = Nothing
    Me.sc_cab.NTSRepositoryItemMemo = Nothing
    Me.sc_cab.NTSRepositoryItemText = Nothing
    Me.sc_cab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_cab.OptionsFilter.AllowFilter = False
    Me.sc_cab.Visible = True
    Me.sc_cab.VisibleIndex = 27
    '
    'sc_banc1
    '
    Me.sc_banc1.AppearanceCell.Options.UseBackColor = True
    Me.sc_banc1.AppearanceCell.Options.UseTextOptions = True
    Me.sc_banc1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_banc1.Caption = "Banca"
    Me.sc_banc1.Enabled = True
    Me.sc_banc1.FieldName = "sc_banc1"
    Me.sc_banc1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_banc1.Name = "sc_banc1"
    Me.sc_banc1.NTSRepositoryComboBox = Nothing
    Me.sc_banc1.NTSRepositoryItemCheck = Nothing
    Me.sc_banc1.NTSRepositoryItemMemo = Nothing
    Me.sc_banc1.NTSRepositoryItemText = Nothing
    Me.sc_banc1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_banc1.OptionsFilter.AllowFilter = False
    Me.sc_banc1.Visible = True
    Me.sc_banc1.VisibleIndex = 28
    '
    'sc_banc2
    '
    Me.sc_banc2.AppearanceCell.Options.UseBackColor = True
    Me.sc_banc2.AppearanceCell.Options.UseTextOptions = True
    Me.sc_banc2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_banc2.Caption = "Filiale"
    Me.sc_banc2.Enabled = True
    Me.sc_banc2.FieldName = "sc_banc2"
    Me.sc_banc2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_banc2.Name = "sc_banc2"
    Me.sc_banc2.NTSRepositoryComboBox = Nothing
    Me.sc_banc2.NTSRepositoryItemCheck = Nothing
    Me.sc_banc2.NTSRepositoryItemMemo = Nothing
    Me.sc_banc2.NTSRepositoryItemText = Nothing
    Me.sc_banc2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_banc2.OptionsFilter.AllowFilter = False
    Me.sc_banc2.Visible = True
    Me.sc_banc2.VisibleIndex = 29
    '
    'sc_numcc
    '
    Me.sc_numcc.AppearanceCell.Options.UseBackColor = True
    Me.sc_numcc.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numcc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numcc.Caption = "C/C"
    Me.sc_numcc.Enabled = True
    Me.sc_numcc.FieldName = "sc_numcc"
    Me.sc_numcc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numcc.Name = "sc_numcc"
    Me.sc_numcc.NTSRepositoryComboBox = Nothing
    Me.sc_numcc.NTSRepositoryItemCheck = Nothing
    Me.sc_numcc.NTSRepositoryItemMemo = Nothing
    Me.sc_numcc.NTSRepositoryItemText = Nothing
    Me.sc_numcc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numcc.OptionsFilter.AllowFilter = False
    Me.sc_numcc.Visible = True
    Me.sc_numcc.VisibleIndex = 30
    '
    'sc_cin
    '
    Me.sc_cin.AppearanceCell.Options.UseBackColor = True
    Me.sc_cin.AppearanceCell.Options.UseTextOptions = True
    Me.sc_cin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_cin.Caption = "Cin"
    Me.sc_cin.Enabled = True
    Me.sc_cin.FieldName = "sc_cin"
    Me.sc_cin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_cin.Name = "sc_cin"
    Me.sc_cin.NTSRepositoryComboBox = Nothing
    Me.sc_cin.NTSRepositoryItemCheck = Nothing
    Me.sc_cin.NTSRepositoryItemMemo = Nothing
    Me.sc_cin.NTSRepositoryItemText = Nothing
    Me.sc_cin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_cin.OptionsFilter.AllowFilter = False
    Me.sc_cin.Visible = True
    Me.sc_cin.VisibleIndex = 31
    '
    'sc_prefiban
    '
    Me.sc_prefiban.AppearanceCell.Options.UseBackColor = True
    Me.sc_prefiban.AppearanceCell.Options.UseTextOptions = True
    Me.sc_prefiban.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_prefiban.Caption = "Pref. IBAN"
    Me.sc_prefiban.Enabled = True
    Me.sc_prefiban.FieldName = "sc_prefiban"
    Me.sc_prefiban.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_prefiban.Name = "sc_prefiban"
    Me.sc_prefiban.NTSRepositoryComboBox = Nothing
    Me.sc_prefiban.NTSRepositoryItemCheck = Nothing
    Me.sc_prefiban.NTSRepositoryItemMemo = Nothing
    Me.sc_prefiban.NTSRepositoryItemText = Nothing
    Me.sc_prefiban.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_prefiban.OptionsFilter.AllowFilter = False
    Me.sc_prefiban.Visible = True
    Me.sc_prefiban.VisibleIndex = 32
    '
    'sc_iban
    '
    Me.sc_iban.AppearanceCell.Options.UseBackColor = True
    Me.sc_iban.AppearanceCell.Options.UseTextOptions = True
    Me.sc_iban.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_iban.Caption = "IBAN estero"
    Me.sc_iban.Enabled = True
    Me.sc_iban.FieldName = "sc_iban"
    Me.sc_iban.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_iban.Name = "sc_iban"
    Me.sc_iban.NTSRepositoryComboBox = Nothing
    Me.sc_iban.NTSRepositoryItemCheck = Nothing
    Me.sc_iban.NTSRepositoryItemMemo = Nothing
    Me.sc_iban.NTSRepositoryItemText = Nothing
    Me.sc_iban.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_iban.OptionsFilter.AllowFilter = False
    Me.sc_iban.Visible = True
    Me.sc_iban.VisibleIndex = 33
    '
    'sc_alfpro
    '
    Me.sc_alfpro.AppearanceCell.Options.UseBackColor = True
    Me.sc_alfpro.AppearanceCell.Options.UseTextOptions = True
    Me.sc_alfpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_alfpro.Caption = "Serie prot."
    Me.sc_alfpro.Enabled = True
    Me.sc_alfpro.FieldName = "sc_alfpro"
    Me.sc_alfpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_alfpro.Name = "sc_alfpro"
    Me.sc_alfpro.NTSRepositoryComboBox = Nothing
    Me.sc_alfpro.NTSRepositoryItemCheck = Nothing
    Me.sc_alfpro.NTSRepositoryItemMemo = Nothing
    Me.sc_alfpro.NTSRepositoryItemText = Nothing
    Me.sc_alfpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_alfpro.OptionsFilter.AllowFilter = False
    Me.sc_alfpro.Visible = True
    Me.sc_alfpro.VisibleIndex = 34
    '
    'sc_numprot
    '
    Me.sc_numprot.AppearanceCell.Options.UseBackColor = True
    Me.sc_numprot.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numprot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numprot.Caption = "Num. prot."
    Me.sc_numprot.Enabled = True
    Me.sc_numprot.FieldName = "sc_numprot"
    Me.sc_numprot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numprot.Name = "sc_numprot"
    Me.sc_numprot.NTSRepositoryComboBox = Nothing
    Me.sc_numprot.NTSRepositoryItemCheck = Nothing
    Me.sc_numprot.NTSRepositoryItemMemo = Nothing
    Me.sc_numprot.NTSRepositoryItemText = Nothing
    Me.sc_numprot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numprot.OptionsFilter.AllowFilter = False
    Me.sc_numprot.Visible = True
    Me.sc_numprot.VisibleIndex = 35
    '
    'sc_codcage
    '
    Me.sc_codcage.AppearanceCell.Options.UseBackColor = True
    Me.sc_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.sc_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_codcage.Caption = "Agente"
    Me.sc_codcage.Enabled = True
    Me.sc_codcage.FieldName = "sc_codcage"
    Me.sc_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_codcage.Name = "sc_codcage"
    Me.sc_codcage.NTSRepositoryComboBox = Nothing
    Me.sc_codcage.NTSRepositoryItemCheck = Nothing
    Me.sc_codcage.NTSRepositoryItemMemo = Nothing
    Me.sc_codcage.NTSRepositoryItemText = Nothing
    Me.sc_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_codcage.OptionsFilter.AllowFilter = False
    Me.sc_codcage.Visible = True
    Me.sc_codcage.VisibleIndex = 36
    '
    'xxc_codcage
    '
    Me.xxc_codcage.AppearanceCell.Options.UseBackColor = True
    Me.xxc_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_codcage.Caption = "Descr. agente"
    Me.xxc_codcage.Enabled = True
    Me.xxc_codcage.FieldName = "xxc_codcage"
    Me.xxc_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_codcage.Name = "xxc_codcage"
    Me.xxc_codcage.NTSRepositoryComboBox = Nothing
    Me.xxc_codcage.NTSRepositoryItemCheck = Nothing
    Me.xxc_codcage.NTSRepositoryItemMemo = Nothing
    Me.xxc_codcage.NTSRepositoryItemText = Nothing
    Me.xxc_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_codcage.OptionsFilter.AllowFilter = False
    Me.xxc_codcage.Visible = True
    Me.xxc_codcage.VisibleIndex = 37
    '
    'sc_controp
    '
    Me.sc_controp.AppearanceCell.Options.UseBackColor = True
    Me.sc_controp.AppearanceCell.Options.UseTextOptions = True
    Me.sc_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_controp.Caption = "Contropartita"
    Me.sc_controp.Enabled = True
    Me.sc_controp.FieldName = "sc_controp"
    Me.sc_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_controp.Name = "sc_controp"
    Me.sc_controp.NTSRepositoryComboBox = Nothing
    Me.sc_controp.NTSRepositoryItemCheck = Nothing
    Me.sc_controp.NTSRepositoryItemMemo = Nothing
    Me.sc_controp.NTSRepositoryItemText = Nothing
    Me.sc_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_controp.OptionsFilter.AllowFilter = False
    Me.sc_controp.Visible = True
    Me.sc_controp.VisibleIndex = 38
    '
    'xxc_controp
    '
    Me.xxc_controp.AppearanceCell.Options.UseBackColor = True
    Me.xxc_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_controp.Caption = "Descr. controp."
    Me.xxc_controp.Enabled = True
    Me.xxc_controp.FieldName = "xxc_controp"
    Me.xxc_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_controp.Name = "xxc_controp"
    Me.xxc_controp.NTSRepositoryComboBox = Nothing
    Me.xxc_controp.NTSRepositoryItemCheck = Nothing
    Me.xxc_controp.NTSRepositoryItemMemo = Nothing
    Me.xxc_controp.NTSRepositoryItemText = Nothing
    Me.xxc_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_controp.OptionsFilter.AllowFilter = False
    Me.xxc_controp.Visible = True
    Me.xxc_controp.VisibleIndex = 39
    '
    'sc_commeca
    '
    Me.sc_commeca.AppearanceCell.Options.UseBackColor = True
    Me.sc_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.sc_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_commeca.Caption = "Commessa"
    Me.sc_commeca.Enabled = True
    Me.sc_commeca.FieldName = "sc_commeca"
    Me.sc_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_commeca.Name = "sc_commeca"
    Me.sc_commeca.NTSRepositoryComboBox = Nothing
    Me.sc_commeca.NTSRepositoryItemCheck = Nothing
    Me.sc_commeca.NTSRepositoryItemMemo = Nothing
    Me.sc_commeca.NTSRepositoryItemText = Nothing
    Me.sc_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_commeca.OptionsFilter.AllowFilter = False
    Me.sc_commeca.Visible = True
    Me.sc_commeca.VisibleIndex = 40
    '
    'xxc_commeca
    '
    Me.xxc_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xxc_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xxc_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxc_commeca.Caption = "Descr. commessa"
    Me.xxc_commeca.Enabled = True
    Me.xxc_commeca.FieldName = "xxc_commeca"
    Me.xxc_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxc_commeca.Name = "xxc_commeca"
    Me.xxc_commeca.NTSRepositoryComboBox = Nothing
    Me.xxc_commeca.NTSRepositoryItemCheck = Nothing
    Me.xxc_commeca.NTSRepositoryItemMemo = Nothing
    Me.xxc_commeca.NTSRepositoryItemText = Nothing
    Me.xxc_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxc_commeca.OptionsFilter.AllowFilter = False
    Me.xxc_commeca.Visible = True
    Me.xxc_commeca.VisibleIndex = 41
    '
    'sc_subcommeca
    '
    Me.sc_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.sc_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.sc_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_subcommeca.Caption = "Subcomm."
    Me.sc_subcommeca.Enabled = True
    Me.sc_subcommeca.FieldName = "sc_subcommeca"
    Me.sc_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_subcommeca.Name = "sc_subcommeca"
    Me.sc_subcommeca.NTSRepositoryComboBox = Nothing
    Me.sc_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.sc_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.sc_subcommeca.NTSRepositoryItemText = Nothing
    Me.sc_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_subcommeca.OptionsFilter.AllowFilter = False
    Me.sc_subcommeca.Visible = True
    Me.sc_subcommeca.VisibleIndex = 42
    '
    'sc_fldis
    '
    Me.sc_fldis.AppearanceCell.Options.UseBackColor = True
    Me.sc_fldis.AppearanceCell.Options.UseTextOptions = True
    Me.sc_fldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_fldis.Caption = "Distinta"
    Me.sc_fldis.Enabled = True
    Me.sc_fldis.FieldName = "sc_fldis"
    Me.sc_fldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_fldis.Name = "sc_fldis"
    Me.sc_fldis.NTSRepositoryComboBox = Nothing
    Me.sc_fldis.NTSRepositoryItemCheck = Nothing
    Me.sc_fldis.NTSRepositoryItemMemo = Nothing
    Me.sc_fldis.NTSRepositoryItemText = Nothing
    Me.sc_fldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_fldis.OptionsFilter.AllowFilter = False
    Me.sc_fldis.Visible = True
    Me.sc_fldis.VisibleIndex = 43
    '
    'sc_dtdist
    '
    Me.sc_dtdist.AppearanceCell.Options.UseBackColor = True
    Me.sc_dtdist.AppearanceCell.Options.UseTextOptions = True
    Me.sc_dtdist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_dtdist.Caption = "Data distinta"
    Me.sc_dtdist.Enabled = True
    Me.sc_dtdist.FieldName = "sc_dtdist"
    Me.sc_dtdist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_dtdist.Name = "sc_dtdist"
    Me.sc_dtdist.NTSRepositoryComboBox = Nothing
    Me.sc_dtdist.NTSRepositoryItemCheck = Nothing
    Me.sc_dtdist.NTSRepositoryItemMemo = Nothing
    Me.sc_dtdist.NTSRepositoryItemText = Nothing
    Me.sc_dtdist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_dtdist.OptionsFilter.AllowFilter = False
    Me.sc_dtdist.Visible = True
    Me.sc_dtdist.VisibleIndex = 44
    '
    'sc_numdist
    '
    Me.sc_numdist.AppearanceCell.Options.UseBackColor = True
    Me.sc_numdist.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numdist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numdist.Caption = "Num. distinta"
    Me.sc_numdist.Enabled = True
    Me.sc_numdist.FieldName = "sc_numdist"
    Me.sc_numdist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numdist.Name = "sc_numdist"
    Me.sc_numdist.NTSRepositoryComboBox = Nothing
    Me.sc_numdist.NTSRepositoryItemCheck = Nothing
    Me.sc_numdist.NTSRepositoryItemMemo = Nothing
    Me.sc_numdist.NTSRepositoryItemText = Nothing
    Me.sc_numdist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numdist.OptionsFilter.AllowFilter = False
    Me.sc_numdist.Visible = True
    Me.sc_numdist.VisibleIndex = 45
    '
    'sc_opdist
    '
    Me.sc_opdist.AppearanceCell.Options.UseBackColor = True
    Me.sc_opdist.AppearanceCell.Options.UseTextOptions = True
    Me.sc_opdist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_opdist.Caption = "Op. distinta"
    Me.sc_opdist.Enabled = True
    Me.sc_opdist.FieldName = "sc_opdist"
    Me.sc_opdist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_opdist.Name = "sc_opdist"
    Me.sc_opdist.NTSRepositoryComboBox = Nothing
    Me.sc_opdist.NTSRepositoryItemCheck = Nothing
    Me.sc_opdist.NTSRepositoryItemMemo = Nothing
    Me.sc_opdist.NTSRepositoryItemText = Nothing
    Me.sc_opdist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_opdist.OptionsFilter.AllowFilter = False
    Me.sc_opdist.Visible = True
    Me.sc_opdist.VisibleIndex = 46
    '
    'sc_datreg
    '
    Me.sc_datreg.AppearanceCell.Options.UseBackColor = True
    Me.sc_datreg.AppearanceCell.Options.UseTextOptions = True
    Me.sc_datreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_datreg.Caption = "Data reg."
    Me.sc_datreg.Enabled = True
    Me.sc_datreg.FieldName = "sc_datreg"
    Me.sc_datreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_datreg.Name = "sc_datreg"
    Me.sc_datreg.NTSRepositoryComboBox = Nothing
    Me.sc_datreg.NTSRepositoryItemCheck = Nothing
    Me.sc_datreg.NTSRepositoryItemMemo = Nothing
    Me.sc_datreg.NTSRepositoryItemText = Nothing
    Me.sc_datreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_datreg.OptionsFilter.AllowFilter = False
    Me.sc_datreg.Visible = True
    Me.sc_datreg.VisibleIndex = 47
    '
    'sc_numreg
    '
    Me.sc_numreg.AppearanceCell.Options.UseBackColor = True
    Me.sc_numreg.AppearanceCell.Options.UseTextOptions = True
    Me.sc_numreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_numreg.Caption = "Num. reg."
    Me.sc_numreg.Enabled = True
    Me.sc_numreg.FieldName = "sc_numreg"
    Me.sc_numreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_numreg.Name = "sc_numreg"
    Me.sc_numreg.NTSRepositoryComboBox = Nothing
    Me.sc_numreg.NTSRepositoryItemCheck = Nothing
    Me.sc_numreg.NTSRepositoryItemMemo = Nothing
    Me.sc_numreg.NTSRepositoryItemText = Nothing
    Me.sc_numreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_numreg.OptionsFilter.AllowFilter = False
    Me.sc_numreg.Visible = True
    Me.sc_numreg.VisibleIndex = 48
    '
    'sc_dtsaldato
    '
    Me.sc_dtsaldato.AppearanceCell.Options.UseBackColor = True
    Me.sc_dtsaldato.AppearanceCell.Options.UseTextOptions = True
    Me.sc_dtsaldato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_dtsaldato.Caption = "Data saldo"
    Me.sc_dtsaldato.Enabled = True
    Me.sc_dtsaldato.FieldName = "sc_dtsaldato"
    Me.sc_dtsaldato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_dtsaldato.Name = "sc_dtsaldato"
    Me.sc_dtsaldato.NTSRepositoryComboBox = Nothing
    Me.sc_dtsaldato.NTSRepositoryItemCheck = Nothing
    Me.sc_dtsaldato.NTSRepositoryItemMemo = Nothing
    Me.sc_dtsaldato.NTSRepositoryItemText = Nothing
    Me.sc_dtsaldato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_dtsaldato.OptionsFilter.AllowFilter = False
    Me.sc_dtsaldato.Visible = True
    Me.sc_dtsaldato.VisibleIndex = 49
    '
    'sc_rgsaldato
    '
    Me.sc_rgsaldato.AppearanceCell.Options.UseBackColor = True
    Me.sc_rgsaldato.AppearanceCell.Options.UseTextOptions = True
    Me.sc_rgsaldato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_rgsaldato.Caption = "Num. reg. saldo"
    Me.sc_rgsaldato.Enabled = True
    Me.sc_rgsaldato.FieldName = "sc_rgsaldato"
    Me.sc_rgsaldato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_rgsaldato.Name = "sc_rgsaldato"
    Me.sc_rgsaldato.NTSRepositoryComboBox = Nothing
    Me.sc_rgsaldato.NTSRepositoryItemCheck = Nothing
    Me.sc_rgsaldato.NTSRepositoryItemMemo = Nothing
    Me.sc_rgsaldato.NTSRepositoryItemText = Nothing
    Me.sc_rgsaldato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_rgsaldato.OptionsFilter.AllowFilter = False
    Me.sc_rgsaldato.Visible = True
    Me.sc_rgsaldato.VisibleIndex = 50
    '
    'sc_integr
    '
    Me.sc_integr.AppearanceCell.Options.UseBackColor = True
    Me.sc_integr.AppearanceCell.Options.UseTextOptions = True
    Me.sc_integr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sc_integr.Caption = "Integrativa"
    Me.sc_integr.Enabled = True
    Me.sc_integr.FieldName = "sc_integr"
    Me.sc_integr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sc_integr.Name = "sc_integr"
    Me.sc_integr.NTSRepositoryComboBox = Nothing
    Me.sc_integr.NTSRepositoryItemCheck = Nothing
    Me.sc_integr.NTSRepositoryItemMemo = Nothing
    Me.sc_integr.NTSRepositoryItemText = Nothing
    Me.sc_integr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sc_integr.OptionsFilter.AllowFilter = False
    Me.sc_integr.Visible = True
    Me.sc_integr.VisibleIndex = 51
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnFlusso)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(782, 422)
    Me.NtsTabPage3.Text = "&3 - Flusso documentale"
    '
    'pnFlusso
    '
    Me.pnFlusso.AllowDrop = True
    Me.pnFlusso.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFlusso.Appearance.Options.UseBackColor = True
    Me.pnFlusso.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFlusso.Controls.Add(Me.ceFldo)
    Me.pnFlusso.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFlusso.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFlusso.Location = New System.Drawing.Point(0, 0)
    Me.pnFlusso.Name = "pnFlusso"
    Me.pnFlusso.Size = New System.Drawing.Size(782, 422)
    Me.pnFlusso.TabIndex = 0
    Me.pnFlusso.Text = "NtsPanel1"
    '
    'ceFldo
    '
    Me.ceFldo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.ceFldo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFldo.Location = New System.Drawing.Point(0, 0)
    Me.ceFldo.MinimumSize = New System.Drawing.Size(400, 400)
    Me.ceFldo.Name = "ceFldo"
    Me.ceFldo.Size = New System.Drawing.Size(782, 422)
    Me.ceFldo.strNomeCampo = ""
    Me.ceFldo.TabIndex = 0
    '
    'tlbLocalizzaGoogle
    '
    Me.tlbLocalizzaGoogle.Caption = "Localizza Offerte/Ordini/Documenti con Google"
    Me.tlbLocalizzaGoogle.Id = 27
    Me.tlbLocalizzaGoogle.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbLocalizzaGoogle.Name = "tlbLocalizzaGoogle"
    Me.tlbLocalizzaGoogle.NTSIsCheckBox = False
    Me.tlbLocalizzaGoogle.Visible = True
    '
    'FRM__FLDO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(791, 482)
    Me.Controls.Add(Me.tsFldo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__FLDO"
    Me.Text = "ANALISI FLUSSO DOCUMENTALE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAnalisi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAnalisi.ResumeLayout(False)
    Me.fmAnalisi.PerformLayout()
    CType(Me.ckScad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckContab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatture.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOrdini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOfferte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmfiltriGlobali, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmfiltriGlobali.ResumeLayout(False)
    Me.fmfiltriGlobali.PerformLayout()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommessa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClifor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOperatore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    CType(Me.grFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbScenario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGriSoloArtFiltri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGriSoloUnDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsFldo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsFldo.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri.ResumeLayout(False)
    Me.pnFiltri.PerformLayout()
    CType(Me.pnFiltriSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltriSx.ResumeLayout(False)
    Me.pnFiltriSx.PerformLayout()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnGriglie, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglie.ResumeLayout(False)
    CType(Me.grTesta, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTesta, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grPrin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvPrin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grScad, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvScad, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnFlusso, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFlusso.ResumeLayout(False)
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__FLDO", "BE__FLDO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128778019230783000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleFldo = CType(oTmp, CLE__FLDO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__FLDO", strRemoteServer, strRemotePort)
    AddHandler oCleFldo.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleFldo.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim FORMAT_CAMB As String = "#,##0.000000000"

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipoRk As New DataTable()

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"", ""})
      dttTipoRk.Rows.Add(New Object() {"!", "Offerta"})
      dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      dttTipoRk.Rows.Add(New Object() {"A", "Fatture immediate emesse"})
      dttTipoRk.Rows.Add(New Object() {"B", "DDT emessi"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipoRk.Rows.Add(New Object() {"D", "Fatture differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"I", "Riemissione ricevute fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Note di accredito ricevute"})
      dttTipoRk.Rows.Add(New Object() {"(", "Note di accredito differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fatture immediate ricevute"})
      dttTipoRk.Rows.Add(New Object() {"M", "DDT ricevuti"})
      dttTipoRk.Rows.Add(New Object() {"K", "Fatture differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"P", "Fatture/ricevute fiscali differite"})
      dttTipoRk.Rows.Add(New Object() {"N", "Note di accredito emesse"})
      dttTipoRk.Rows.Add(New Object() {"£", "Note di accredito differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatture/ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carichi da produzione"})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarichi a produzione"})
      dttTipoRk.Rows.Add(New Object() {"W", "Note di prelievo"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolle di movimentazione interna"})
      dttTipoRk.AcceptChanges()


      Dim dttStasino As New DataTable()
      dttStasino.Columns.Add("cod", GetType(String))
      dttStasino.Columns.Add("val", GetType(String))
      dttStasino.Rows.Add(New Object() {"S", "Si"})
      dttStasino.Rows.Add(New Object() {"N", "No"})
      dttStasino.Rows.Add(New Object() {"B", "Solo in bolla"})
      dttStasino.Rows.Add(New Object() {"D", "Solo in fattura"})
      dttStasino.Rows.Add(New Object() {"O", "Omaggi (imponibile)"})
      dttStasino.Rows.Add(New Object() {"M", "Sconto merce"})
      dttStasino.Rows.Add(New Object() {"X", "Sconto merce NC"})
      dttStasino.Rows.Add(New Object() {"P", "Omaggi (imp. + IVA)"})
      dttStasino.AcceptChanges()

      Dim dttTipacq As New DataTable()
      dttTipacq.Columns.Add("cod", GetType(String))
      dttTipacq.Columns.Add("val", GetType(String))
      dttTipacq.Rows.Add(New Object() {" ", "Non interessa"})
      dttTipacq.Rows.Add(New Object() {"A", "Altro"})
      dttTipacq.Rows.Add(New Object() {"B", "Beni strum.non ammortiz."})
      dttTipacq.Rows.Add(New Object() {"I", "Rimanenze iniziali"})
      dttTipacq.Rows.Add(New Object() {"L", "Leasing"})
      dttTipacq.Rows.Add(New Object() {"N", "Non definito"})
      dttTipacq.Rows.Add(New Object() {"R", "Beni destinati alla rivendita"})
      dttTipacq.Rows.Add(New Object() {"S", "Beni Ammortizzabili (cespiti)"})
      dttTipacq.Rows.Add(New Object() {"X", "Beni amm. non Iva 11"})
      dttTipacq.AcceptChanges()

      Dim dttTipPag As New DataTable()
      dttTipPag.Columns.Add("cod", GetType(Short))
      dttTipPag.Columns.Add("val", GetType(String))
      dttTipPag.Rows.Add(New Object() {1, "Tratta"})
      dttTipPag.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTipPag.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTipPag.Rows.Add(New Object() {4, "Contanti"})
      dttTipPag.Rows.Add(New Object() {5, "Accr.Bancario"})
      dttTipPag.AcceptChanges()

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      ckScad.NTSSetParam(oMenu, oApp.Tr(Me, 128778089762541000, "Scadenze"), "S", "N")
      ckContab.NTSSetParam(oMenu, oApp.Tr(Me, 128778089762697000, "Contabilità"), "S", "N")
      ckFatture.NTSSetParam(oMenu, oApp.Tr(Me, 128778089762853000, "Fatture"), "S", "N")
      ckMagaz.NTSSetParam(oMenu, oApp.Tr(Me, 128778089763009000, "Movimenti di mag."), "S", "N")
      ckNote.NTSSetParam(oMenu, oApp.Tr(Me, 128778089763165000, "Note di prelievo"), "S", "N")
      ckOrdini.NTSSetParam(oMenu, oApp.Tr(Me, 128778089763321000, "Ordini"), "S", "N")
      ckOfferte.NTSSetParam(oMenu, oApp.Tr(Me, 128778093554291000, "Offerte"), "S", "N")
      edDataA.NTSSetParam(oMenu, oApp.Tr(Me, 128778093579095000, "A data"), False)
      edDataDa.NTSSetParam(oMenu, oApp.Tr(Me, 128778089763633000, "Da data"), False)
      edCommessa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128778089763789000, "Commessa"), tabcommess)
      edEscomp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128778089763945000, "Esercizio di competenza contabile"), tabesco)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128778089764101000, "Anno documenti"), "0", 4, 0, 2099)
      edLead.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128778089764257000, "Lead"), tableads)
      edClifor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128778089764413000, "Cliente/fornitore"), tabanagrac)
      edOperatore.NTSSetParam(oMenu, oApp.Tr(Me, 128778089764569000, "Operatore di Business"), 20, True)
      edArticolo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128778089764725000, "Articolo"), tabartico, True)
      cbScenario.NTSSetParam(oApp.Tr(Me, 128778089766129000, "Scenario"))
      ckGriSoloUnDoc.NTSSetParam(oMenu, oApp.Tr(Me, 128778089766909000, "Carica la griglia movimenti solo con le righe del documento si cui si è posizionati nella griglia testate"), "S", "N")
      ckGriSoloArtFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 128778089767065000, "Carica la griglia movimenti solo con le righe degli articoli impostati nei filtri"), "S", "N")
      grvFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 128778061429727000, "Griglia filtri aggiuntivi"))
      grvFiltri.NTSAllowDelete = False
      grvFiltri.NTSAllowInsert = False
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128778061459835000, "Nome filtro"), dttTipi, "cod", "val")
      xx_valoreda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128778061469351000, "Valore filtro DA"), 0)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128778061477931000, "Valore filtro A"), 0)
      xx_valoreda.NTSSetParamZoom("__")
      xx_valorea.NTSSetParamZoom("__")

      '--------------
      grvTesta.NTSSetParam(oMenu, oApp.Tr(Me, 128778951850158000, "Testate documenti"))
      et_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128783189002378000, "Tipo"), dttTipoRk, "val", "cod")
      et_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189002534000, "Anno"), "0", 4, 0, 9999)
      et_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189002690000, "Serie"), CLN__STD.SerieMaxLen, True)
      et_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189002846000, "Numero"), "0", 9, 0, 999999999)
      et_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128783189003002000, "Data doc"), True)
      et_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189003158000, "Riferim."), 0, True)
      et_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189003314000, "Conto"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189003470000, "Descr. conto"), 0, True)
      et_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189003626000, "Destin."), "0", 9, 0, 999999999)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189003782000, "Descr. destin."), 0, True)
      et_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189003938000, "Tot. doc."), oApp.FormatImporti, 15)
      et_totdocv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189004094000, "Tot. doc. Val."), oApp.FormatImpVal, 15)
      et_valuta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189004250000, "Valuta"), "0", 4, 0, 9999)
      xx_valuta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189004406000, "Descr. valuta"), 0, True)
      et_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189004562000, "Cambio"), FORMAT_CAMB, 20, 0, 99999999)
      et_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783189004718000, "Evaso"), "S", "N")
      et_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783189004874000, "Rilasciato"), "S", "N")
      et_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783189005030000, "Confermato"), "S", "N")
      et_tipobf.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189005186000, "Tipo B/F"), "0", 4, 0, 9999)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189005342000, "Descr. tipo B/F"), 0, True)
      et_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189005498000, "Causale"), "0", 9, 0, 999999999)
      xx_causale.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189005654000, "Descr. causale"), 0, True)
      et_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189005810000, "Magaz."), "0", 9, 0, 999999999)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189005966000, "Descr. magaz."), 0, True)
      et_magaz2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189006122000, "Magaz. 2"), "0", 9, 0, 999999999)
      xx_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189006278000, "Descr. magaz. 2"), 0, True)
      et_magimp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189006434000, "Mag. impegni"), "0", 9, 0, 999999999)
      et_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128783189006590000, "Data consegna"), True)
      et_codagen.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189006746000, "Agente"), "0", 4, 0, 9999)
      xx_codagen.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189006902000, "Descr. agente"), 0, True)
      et_listino.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189007058000, "Listino"), "0", 4, 0, 9999)
      et_codese.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189007214000, "Cod. esenzione"), "0", 9, 0, 999999999)
      xx_codese.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189007370000, "Descr. esenzione"), 0, True)
      et_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189007526000, "Controp."), "0", 9, 0, 999999999)
      xx_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189007682000, "Descr. controp."), 0, True)
      et_contfatt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189007838000, "Conto fatturaz."), "0", 9, 0, 999999999)
      et_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189007994000, "Sconto 1"), "#,##0.00", 15)
      et_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189008150000, "Sconto 2"), "#,##0.00", 15)
      et_scopag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189008306000, "Sconto pag."), "#,##0.00", 15)
      et_codpaga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189008462000, "Cod. pagam."), "0", 4, 0, 9999)
      xx_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189008618000, "Descr. pagam."), 0, True)
      et_abi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189008774000, "Abi"), "0", 9, 0, 999999999)
      et_cab.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189008930000, "Cab"), "0", 9, 0, 999999999)
      et_banc1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189009086000, "Banca"), 0, True)
      et_banc2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189009242000, "Filiale"), 0, True)
      et_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189009398000, "Num. partita"), "0", 9, 0, 999999999)
      et_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129279830452257474, "Data partita"), True)
      et_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189009710000, "Operatore"), 0, True)
      et_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189009866000, "Note"), 0, True)
      et_opinc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189010022000, "Op. incaricato"), "0", 9, 0, 999999999)
      et_coddest2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189010178000, "Destin. 2"), "0", 9, 0, 999999999)
      et_oggetto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189010334000, "Oggetto"), 0, True)
      et_vers.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189010490000, "Vers."), "0", 9, 0, 999999999)
      et_codlead.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783189010646000, "Lead"), "0", 9, 0, 999999999)
      xx_codlead.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783189010802000, "Descr. lead"), 0, True)
      grvTesta.NTSAllowInsert = False
      grvTesta.NTSAllowUpdate = False
      grvTesta.NTSAllowDelete = False
      grvTesta.Enabled = False

      '--------------
      grvCorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128778951952338000, "Corpo documenti"))
      ec_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128783930314190000, "Tipo"), dttTipoRk, "val", "cod")
      ec_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928389462000, "Anno"), "0", 4, 0, 9999)
      ec_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928389618000, "Serie"), CLN__STD.SerieMaxLen, True)
      ec_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928389774000, "Numero"), "0", 9, 0, 999999999)
      ec_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928389930000, "Articolo"), 0, True)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928390086000, "Descr. articolo"), 0, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928390242000, "Descr. int. art."), 0, True)
      ec_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928390398000, "U.m."), 0, True)
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928390554000, "Colli"), oApp.FormatQta, 15)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928390710000, "UMP"), 0, True)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928390866000, "Quantità"), oApp.FormatQta, 15)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391022000, "Prezzo"), oApp.FormatPrzUn, 15)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391178000, "Prezzo val."), oApp.FormatPrzUnVal, 15)
      ec_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391334000, "Prezzo c/IVA"), oApp.FormatPrzUn, 15)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391490000, "Valore"), oApp.FormatImporti, 15)
      ec_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391646000, "Sconto 1"), oApp.FormatSconti, 15)
      ec_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391802000, "Sconto 2"), oApp.FormatSconti, 15)
      ec_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928391958000, "Sconto 3"), oApp.FormatSconti, 15)
      ec_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928392114000, "Sconto 4"), oApp.FormatSconti, 15)
      ec_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928392270000, "Sconto 5"), oApp.FormatSconti, 15)
      ec_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928392426000, "Sconto 6"), oApp.FormatSconti, 15)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928392582000, "Note"), 0, True)
      ec_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128783928392738000, "Data cons."), True)
      ec_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928392894000, "Magaz."), "0", 4, 0, 9999)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928393050000, "Descr. magaz."), 0, True)
      ec_magaz2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928393206000, "Magaz.2"), "0", 4, 0, 9999)
      xxo_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928393362000, "Descr. magaz.2"), 0, True)
      ec_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928393518000, "Qta evasa"), oApp.FormatQta, 15)
      ec_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928393674000, "Qta prenot."), "0", 9, 0, 999999999)
      ec_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928393830000, "Evaso"), "S", "C")
      ec_flevapre.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928393986000, "NP evasa"), "S", "C")
      ec_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928394142000, "Provv."), oApp.FormatSconti, 15)
      ec_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928394298000, "Provv. valore"), oApp.FormatImporti, 15)
      ec_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928394454000, "Controp."), "0", 4, 0, 9999)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928394610000, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928394766000, "Cod. IVA"), "0", 4, 0, 9999)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928394922000, "Descr. IVA"), 0, True)
      ec_stasino.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128783928395078000, "Stampa riga"), dttStasino, "val", "cod")
      ec_prelist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928395234000, "Prz. listino"), oApp.FormatPrzUn, 15)
      ec_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928395390000, "Linea/fam."), 0, True)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928395546000, "Descr. linea/fam."), 0, True)
      ec_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928395702000, "Commessa"), "0", 9, 0, 999999999)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928395858000, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928396014000, "Subcomm."), 0, True)
      ec_codcena.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928396170000, "Centro CA"), "0", 9, 0, 999999999)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928396326000, "Descr. centro"), 0, True)
      ec_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928396482000, "Confermato"), "S", "N")
      ec_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928396638000, "Rilasciato"), "S", "N")
      ec_aperto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928396794000, "Aperto"), "S", "N")
      xx_lottox.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928396950000, "Lotto"), "0", 9, 0, 999999999)
      ec_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928397106000, "Ubicazione"), 0, True)
      ec_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928397262000, "Causale"), "0", 4, 0, 9999)
      xxo_causale.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928397418000, "Descr. causale"), 0, True)
      ec_causale2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928397574000, "Causale 2"), "0", 4, 0, 9999)
      ec_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928397730000, "Fase art."), "0", 4, 0, 9999)
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928397886000, "Descr. fase"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928398042000, "Misura 1"), oApp.FormatQta, 15)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928398198000, "Misura 2"), oApp.FormatQta, 15)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928398354000, "Misura 3"), oApp.FormatQta, 15)
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128783928398510000, "Data inizio"), True)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128783928398666000, "Data fine"), True)
      ec_ortipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128783930837102000, "OR Tipo"), dttTipoRk, "val", "cod")
      ec_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928398978000, "OR anno"), "0", 4, 0, 9999)
      ec_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928399134000, "OR serie"), CLN__STD.SerieMaxLen, True)
      ec_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928399290000, "OR num."), "0", 9, 0, 999999999)
      ec_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928399446000, "OR riga"), "0", 9, 0, 999999999)
      ec_salcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928399602000, "OR saldato"), "S", "C")
      ec_nptipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128783928399758000, "PN/Off tipo"), dttTipoRk, "val", "cod")
      ec_npanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928399914000, "PN/Off anno"), "0", 4, 0, 9999)
      ec_npserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128783928400070000, "PN/Off serie"), CLN__STD.SerieMaxLen, True)
      ec_npnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928400226000, "PN/Off num."), "0", 9, 0, 999999999)
      ec_npvers.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928400382000, "PN/Off vers."), "0", 9, 0, 999999999)
      ec_npriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928400538000, "PN/Off riga"), "0", 9, 0, 999999999)
      ec_pnsalcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128783928400694000, "PN/Off saldata"), "S", "C")
      ec_vers.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128783928400850000, "Vers."), "0", 9, 0, 999999999)
      xxo_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784029534470000, "Conto"), "0", 9, 0, 999999999)
      grvCorpo.NTSAllowInsert = False
      grvCorpo.NTSAllowUpdate = False
      grvCorpo.NTSAllowDelete = False
      grvCorpo.Enabled = False

      '--------------
      grvPrin.NTSSetParam(oMenu, oApp.Tr(Me, 128778951966222000, "Prima nota"))
      pn_datreg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368300000, "Data reg."), True)
      pn_numreg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368310000, "Num. reg."), "0", 9, 0, 999999999)
      pn_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368320000, "Riga"), "0", 9, 0, 999999999)
      pn_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368330000, "Causale"), "0", 4, 0, 9999)
      pn_descauc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368340000, "Descr. causale"), 0, True)
      pn_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368350000, "Conto"), "0", 9, 0, 999999999)
      xxp_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368360000, "Descr. conto"), 0, True)
      pn_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368370000, "Descr."), 0, True)
      pn_darave.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368380000, "D/A"), 0, True)
      pn_importo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368390000, "Importo"), oApp.FormatImporti, 15)
      pn_dare.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368400000, "Dare"), oApp.FormatImporti, 15)
      pn_avere.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368410000, "Avere"), oApp.FormatImporti, 15)
      pn_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368420000, "Data doc"), True)
      pn_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368430000, "Num. doc"), "0", 9, 0, 999999999)
      pn_alfdoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368440000, "Serie doc"), CLN__STD.SerieMaxLen, True)
      pn_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368450000, "Controp."), "0", 9, 0, 999999999)
      xxp_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368460000, "Descr. controp."), 0, True)
      pn_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368470000, "Anno par."), "0", 4, 0, 9999)
      pn_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368480000, "Serie par."), CLN__STD.SerieMaxLen, True)
      pn_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368490000, "Num. par."), "0", 9, 0, 999999999)
      pn_codvalu.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368500000, "Valuta"), "0", 4, 0, 9999)
      xxp_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368510000, "Descr. valuta"), 0, True)
      pn_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368520000, "Cambio"), FORMAT_CAMB, 20, 0, 99999999)
      pn_impval.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368530000, "Imp. valuta"), oApp.FormatImpVal, 15)
      pn_dareval.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368540000, "Dare val."), oApp.FormatImpVal, 15)
      pn_avereval.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368550000, "Avere val."), oApp.FormatImpVal, 15)
      pn_tregiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368560000, "Reg. IVA"), 0, True)
      pn_nregiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368570000, "Num. reg. IVA"), "0", 4, 0, 9999)
      pn_codiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368580000, "Cod. IVA"), "0", 4, 0, 9999)
      xxp_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368590000, "Descr. IVA"), 0, True)
      pn_aliqiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368600000, "Aliq. IVA"), oApp.FormatSconti, 15)
      pn_indetr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368610000, "Indetr."), oApp.FormatSconti, 15)
      pn_contocf.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368620000, "Conto C/F"), "0", 9, 0, 999999999)
      xxp_contocf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368630000, "Descr. conto C/F"), 0, True)
      pn_imponib.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368640000, "Imponib. IVA"), oApp.FormatImporti, 15)
      pn_imponibval.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368650000, "Imponib. IVA val"), oApp.FormatImpVal, 15)
      pn_tipacq.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128784018368660000, "Tipo acquisto"), dttTipacq, "val", "cod")
      pn_numpro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368670000, "Num. prot."), "0", 9, 0, 999999999)
      pn_alfpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368680000, "Serie prot."), CLN__STD.SerieMaxLen, True)
      pn_integr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784018368690000, "Integrativa"), "S", "N")
      pn_fllg.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784018368700000, "Stampato LG"), "S", " ")
      pn_dtcomiva.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368710000, "Data comp. IVA"), True)
      pn_dtvaluta.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368720000, "Data valuta"), True)
      pn_dtcomplaf.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368730000, "Data plafond"), True)
      pn_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368740000, "Data inizio"), True)
      pn_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368750000, "Data fine"), True)
      pn_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784018368760000, "Data ult. agg."), True)
      pn_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784018368770000, "Operatore"), 0, True)
      pn_escomp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784018368780000, "Es. comp."), "0", 4, 0, 9999)
      grvPrin.NTSAllowInsert = False
      grvPrin.NTSAllowUpdate = False
      grvPrin.NTSAllowDelete = False
      grvPrin.Enabled = False

      '--------------
      grvScad.NTSSetParam(oMenu, oApp.Tr(Me, 128778951976674000, "Scadenze"))
      sc_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043471964000, "Conto"), "0", 9, 0, 999999999)
      xxc_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043471974000, "Descr. conto"), 0, True)
      sc_datsca.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784043471984000, "Data scad."), True)
      sc_importoda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043471994000, "Importo"), oApp.FormatImporti, 15)
      sc_impvalda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472004000, "Importo valuta"), oApp.FormatImpVal, 15)
      sc_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472014000, "Anno par"), "0", 4, 0, 9999)
      sc_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472024000, "Serie par"), CLN__STD.SerieMaxLen, True)
      sc_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472034000, "Num. par"), "0", 9, 0, 999999999)
      sc_numrata.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472044000, "Rata"), "0", 4, 0, 9999)
      sc_darave.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472054000, "D/A"), 1, True)
      sc_flsaldato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784043472074000, "Saldato"), "S", "N")
      sc_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784043472094000, "Data doc."), True)
      sc_alfdoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472104000, "Serie doc."), CLN__STD.SerieMaxLen, True)
      sc_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472114000, "Num. doc."), "0", 9, 0, 999999999)
      sc_codpaga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472124000, "Cod. pag."), "0", 4, 0, 9999)
      xxc_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472134000, "Descr. pag."), 0, True)
      sc_tippaga.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128784043472144000, "Tipo pag."), dttTipPag, "val", "cod")
      sc_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472154000, "Descr."), 0, True)
      sc_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472164000, "Causale"), "0", 4, 0, 9999)
      xxc_causale.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472174000, "Descr. causale"), 0, True)
      sc_codvalu.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472184000, "Valuta"), "0", 4, 0, 9999)
      xxc_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472194000, "Descr. valuta"), 0, True)
      sc_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472204000, "Cambio"), FORMAT_CAMB, 20, 0, 99999999)
      sc_insolu.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784043472214000, "Insoluto"), "S", "N")
      sc_codbanc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472224000, "Ns. banca"), "0", 4, 0, 9999)
      xxc_codbanc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472234000, "Descr. ns. banca"), 0, True)
      sc_abi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472244000, "Abi"), "0", 9, 0, 999999999)
      sc_cab.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472254000, "Cab"), "0", 9, 0, 999999999)
      sc_banc1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472264000, "Banca"), 0, True)
      sc_banc2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472274000, "Filiale"), 0, True)
      sc_numcc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472284000, "C/C"), 0, True)
      sc_cin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472294000, "Cin"), 0, True)
      sc_prefiban.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472304000, "Pref. IBAN"), 0, True)
      sc_iban.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472314000, "IBAN estero"), 0, True)
      sc_alfpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472324000, "Serie prot."), CLN__STD.SerieMaxLen, True)
      sc_numprot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472334000, "Num. prot."), "0", 9, 0, 999999999)
      sc_codcage.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472344000, "Agente"), "0", 4, 0, 9999)
      xxc_codcage.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472354000, "Descr. agente"), 0, True)
      sc_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472364000, "Contropartita"), "0", 9, 0, 999999999)
      xxc_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472374000, "Descr. controp."), 0, True)
      sc_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472384000, "Commessa"), "0", 9, 0, 999999999)
      xxc_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472394000, "Descr. commessa"), 0, True)
      sc_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472404000, "Subcomm."), 1, True)
      sc_fldis.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784043472414000, "Distinta"), "S", "N")
      sc_dtdist.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784043472424000, "Data distinta"), True)
      sc_numdist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472434000, "Num. distinta"), "0", 9, 0, 999999999)
      sc_opdist.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128784043472444000, "Op. distinta"), 0, True)
      sc_datreg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784043472454000, "Data reg."), True)
      sc_numreg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472464000, "Num. reg."), "0", 9, 0, 999999999)
      sc_dtsaldato.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128784043472474000, "Data saldo"), True)
      sc_rgsaldato.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128784043472484000, "Num. reg. saldo"), "0", 9, 0, 999999999)
      sc_integr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128784043472494000, "Integrativa"), "S", "N")
      grvScad.NTSAllowInsert = False
      grvScad.NTSAllowUpdate = False
      grvScad.NTSAllowDelete = False
      grvScad.Enabled = False

      ceFldo.NTSSetParam(oMenu, "Flusso documentale", "BN__FLDO", Nothing)

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Function CaricaCombo() As Boolean
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim nRow As Integer = 0
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing

    Try
      '-----------------------
      'carico i valori possibili per i filtri personalizzati
      If Not oCleFldo.GetTableFiltri(dttTipi) Then Return False
      dttTipi.AcceptChanges()

      '---------------------------
      'creo le righe della griglia
      dsFiltri.Tables.Add("FILTRI")
      dsFiltri.Tables("FILTRI").Columns.Add("xx_nome", GetType(String))
      dsFiltri.Tables("FILTRI").Columns.Add("xx_valoreda", GetType(String))
      dsFiltri.Tables("FILTRI").Columns.Add("xx_valorea", GetType(String))
      dsFiltri.AcceptChanges()

      For i = 0 To MAXROW
        dsFiltri.Tables("FILTRI").Rows.Add(New Object() {".", "", ""})
      Next

      '---------------------------
      'carico gli schemi memorizzati
      'per adesso  da arcproc, ma dovrebbero venir memorizzati un una tabella del DB, 
      'altrimenti quando si chiede il db al cli non ci sono le impostazioni ...
      strTmp = oMenu.GetSettingBus("BS--FLDO", "OPZIONI", ".", "Scenari", "", "", "")
      dttScenario.Columns.Add("cod", GetType(String))
      dttScenario.Columns.Add("val", GetType(String))
      dttScenario.Rows.Add(New Object() {".", ""})
      If strTmp <> "" Then
        strT = strTmp.Split("|"c)   'elenco degli scenari
        For i = 0 To strT.Length - 1
          strT1 = strT(i).Split("§"c)
          dttScenario.Rows.Add(New Object() {strT1(1), strT1(0)})
        Next
      End If    'If strTmp <> "" Then
      dttScenario.AcceptChanges()
      cbScenario.DataSource = dttScenario
      cbScenario.ValueMember = "cod"
      cbScenario.DisplayMember = "val"
      cbScenario.SelectedValue = "."

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Eventi di Form"
  Public Overridable Sub FRM__FLDO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Try
      Me.Cursor = Cursors.WaitCursor

      CaricaCombo()
      dcFiltri.DataSource = dsFiltri.Tables("FILTRI")
      dsFiltri.AcceptChanges()
      grFiltri.DataSource = dcFiltri

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      tlbNoModal.Checked = CBool(oMenu.GetSettingBus("BS--FLDO", "RECENT", ".", "ChildNoModal", "0", " ", "0"))

      edAnno.Text = DateTime.Now.Year.ToString
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then edEscomp.Text = dttTmp.Rows(0)!tb_escomp.ToString
      dttTmp.Clear()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      tsFldo.SelectedTabPageIndex = 0
      tlbStrumenti.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRM__FLDO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Dim strFldoTipork As String
    Dim strFldoSerie As String
    Dim strFldoDatreg As String
    Dim strFldoAlfpar As String
    Dim strFldoTipoogg As String
    Dim nFldoAnno As Integer
    Dim nFldoRigareg As Integer
    Dim nFldoAnnpar As Integer
    Dim nFldoNumrata As Integer
    Dim lFldoNumero As Integer
    Dim lFldoConto As Integer
    Dim lFldoNumreg As Integer
    Dim lFldoNumpar As Integer
    Dim strParam() As String

    Try
      '--------------------------------------------
      i = NTSCInt(oMenu.GetSettingBus("BS__FLDO", "RECENT", ".", "Splitter1", "0", " ", "0"))
      If i <> 0 Then grCorpo.Height = i
      i = NTSCInt(oMenu.GetSettingBus("BS__FLDO", "RECENT", ".", "Splitter2", "0", " ", "0"))
      If i <> 0 Then grPrin.Height = i
      i = NTSCInt(oMenu.GetSettingBus("BS__FLDO", "RECENT", ".", "Splitter3", "0", " ", "0"))
      If i <> 0 Then grScad.Height = i

      '--------------------------------------------
      'sono stato chiamato da un altro child ...
      bChiamatoDaChild = False
      If Not oCallParams Is Nothing Then
        strParam = oCallParams.strParam.Split(";"c)
        If strParam(0) = "APRI" Then
          bChiamatoDaChild = True
          strFldoTipork = strParam(1).ToUpper
          nFldoAnno = NTSCInt(strParam(2))
          strFldoSerie = strParam(3).ToUpper
          lFldoNumero = NTSCInt(strParam(4))
          lFldoConto = NTSCInt(strParam(5))
          strFldoDatreg = strParam(6).Trim
          lFldoNumreg = NTSCInt(strParam(7))
          nFldoRigareg = NTSCInt(strParam(8))
          nFldoAnnpar = NTSCInt(strParam(9))
          strFldoAlfpar = strParam(10).ToUpper
          lFldoNumpar = NTSCInt(strParam(11))
          nFldoNumrata = NTSCInt(strParam(12))
          strFldoTipoogg = strParam(13).Trim

          tsFldo.SelectedTabPageIndex = 2
          NtsTabPage1.Enable = False
          NtsTabPage2.Enable = False
          Me.Cursor = Cursors.WaitCursor
          Select Case strFldoTipoogg
            Case "5"
              'prima nota
              strFldoTipork = "1"
              If Not ceFldo.CalcolaFlusso(strFldoTipork, 0, "", 0, 0, lFldoConto, NTSCDate(strFldoDatreg).ToShortDateString, lFldoNumreg, nFldoRigareg, 0, "", 0, 0) Then
                Me.Close()
                Return
              End If
            Case "6"
              'scadenze
              strFldoTipork = "2"
              If Not ceFldo.CalcolaFlusso(strFldoTipork, 0, "", 0, 0, lFldoConto, "", 0, 0, nFldoAnnpar, strFldoAlfpar, lFldoNumpar, nFldoNumrata) Then
                Me.Close()
                Return
              End If
            Case Else
              'documenti
              If Not ceFldo.CalcolaFlusso(strFldoTipork, nFldoAnno, strFldoSerie, lFldoNumero, lFldoNumreg, 0, "", 0, 0, 0, "", 0, 0) Then
                Me.Close()
                Return
              End If
          End Select
        End If
      End If  'If Not oCallParams Is Nothing Then
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bChiamatoDaChild = False
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRM__FLDO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim i As Integer = 0
    Try
      '-----------------------
      i = grCorpo.Height
      oMenu.SaveSettingBus("BS__FLDO", "RECENT", ".", "Splitter1", i.ToString, " ", "NS.", "...", "...")
      i = grPrin.Height
      oMenu.SaveSettingBus("BS__FLDO", "RECENT", ".", "Splitter2", i.ToString, " ", "NS.", "...", "...")
      i = grScad.Height
      oMenu.SaveSettingBus("BS__FLDO", "RECENT", ".", "Splitter3", i.ToString, " ", "NS.", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRM__FLDO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      If tlbNoModal.Checked Then
        oMenu.SaveSettingBus("BS--FLDO", "RECENT", ".", "ChildNoModal", "-1", " ", "NS.", "...", "...")
      Else
        oMenu.SaveSettingBus("BS--FLDO", "RECENT", ".", "ChildNoModal", "0", " ", "NS.", "...", "...")
      End If
      dcFiltri.Dispose()
      dsFiltri.Dispose()
      dcTesta.Dispose()
      dcCorpo.Dispose()
      dcPrin.Dispose()
      dcScad.Dispose()
      dsGri.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Try
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128779720832782000, "Indicare il nome per il nuovo schema"), "")
      If strTmp.Trim = "" Then Return
      If dttScenario.Select("val = " & CStrSQL(strTmp)).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128778896806250000, "Scenario già esistente"))
        Return
      End If
      dttScenario.Rows.Add(New Object() {CalcolaStringaSchenario(), strTmp})
      dttScenario.AcceptChanges()
      cbScenario.Text = strTmp
      tlbSalva_ItemClick(tlbSalva, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    'salvo SEMPRE tutti gli scenari nel registro di Business, in una unica stringa
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Try
      bInSalva = True

      '-----------------------
      'rimemorizzo le scenario nel combobox
      i = cbScenario.SelectedIndex
      dtrT = dttScenario.Select("val = " & CStrSQL(cbScenario.Text))
      If cbScenario.Text.Trim <> "" Then
        dtrT(0)!cod = CalcolaStringaSchenario()   'in questo momento il combo viene impostato a -1 perchè non trova più la chiave (datatable colonna cod)
      Else
        dtrT(0)!cod = "."
      End If
      dttScenario.AcceptChanges()
      cbScenario.SelectedIndex = i

      '-----------------------
      'salvo nel reg. di bus i filtri ed i check
      dsFiltri.Tables("FILTRI").AcceptChanges()

      For i = 0 To dttScenario.Rows.Count - 1
        If NTSCStr(dttScenario.Rows(i)!cod) <> "." Then     'non salvo il valore 'non impostato'
          strTmp += NTSCStr(dttScenario.Rows(i)!val) & "§" & NTSCStr(dttScenario.Rows(i)!cod) & "|"
        End If
      Next
      If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
      oMenu.SaveSettingBus("BS--FLDO", "OPZIONI", ".", "Scenari", strTmp, " ", "...", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bInSalva = False
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim dtrT() As DataRow = Nothing
    Try
      If cbScenario.SelectedValue = "." Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128778899975832000, "Lo scenario di partenza non può essere cancellato"))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128778924049568000, "Cancellare lo scenario corrente?")) = Windows.Forms.DialogResult.Yes Then
        tlbSalva_ItemClick(tlbSalva, Nothing)     'prima risalvo tutto
        dtrT = dttScenario.Select("val = " & CStrSQL(cbScenario.Text))
        If dtrT.Length > 0 Then
          dtrT(0).Delete()
          dttScenario.AcceptChanges()
          cbScenario.SelectedValue = "."
          tlbSalva_ItemClick(tlbSalva, Nothing)   'salvo gli scenari restanti
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      ApplicaScenario()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    'zoom standard
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    Dim oParam As New CLE__PATB
    Dim strTmp As String = ""

    Try
      If ctrlTmp Is Nothing Then Return

      If grFiltri.ContainsFocus Then
        '------------------------------------
        'zoom su filtri di griglia
        If NTSCStr(grvFiltri.NTSGetCurrentDataRow!xx_nome) = "." Then Return 'sono su una colonna N.A.
        strTmp = NTSCStr(grvFiltri.EditingValue)
        ApriZoomTabella(strTmp, NTSCStr(grvFiltri.NTSGetCurrentDataRow!xx_nome))
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApridoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApridoc.ItemClick
    Dim strParam As String = ""
    Dim bSaldo As Boolean = False
    Try
      '------------------------
      If grTesta.Focused Then
        If Not grvTesta.NTSGetCurrentDataRow Is Nothing Then
          Select Case grvTesta.NTSGetCurrentDataRow!et_tipork.ToString
            Case "!"
              With grvTesta.NTSGetCurrentDataRow
                strParam = "APRI;" & !et_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !et_serie.ToString & ";" & _
                           !et_numdoc.ToString.PadLeft(9, "0"c) & ";" & _
                           !et_vers.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 128784083954510000, "Gestione offerte"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "$", "H", "O", "Q", "R", "V", "X", "Y"
              With grvTesta.NTSGetCurrentDataRow
                strParam = "APRI;" & IIf(!et_tipork.ToString = "Y", "H", !et_tipork.ToString).ToString & ";" & _
                           !et_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !et_serie.ToString & ";" & _
                           !et_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128784083619890000, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "D", "K", "P", "£", "("
              With grvTesta.NTSGetCurrentDataRow
                strParam = "APRI;" & !et_tipork.ToString & ";" & _
                           !et_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !et_serie.ToString & ";" & _
                           !et_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 128784083632838000, "Fatturazione differita interattiva"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case Else
              With grvTesta.NTSGetCurrentDataRow
                strParam = "APRI;" & IIf(!et_tipork.ToString = "U", "T", !et_tipork.ToString).ToString & ";" & _
                           !et_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !et_serie.ToString & ";" & _
                           !et_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128784083644226000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End Select
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128784088078740000, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      '------------------------
      If grCorpo.Focused Then
        If Not grvCorpo.NTSGetCurrentDataRow Is Nothing Then
          Select Case grvCorpo.NTSGetCurrentDataRow!ec_tipork.ToString
            Case "!"
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "APRI;" & !ec_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !ec_serie.ToString & ";" & _
                           !ec_numdoc.ToString.PadLeft(9, "0"c) & ";" & _
                           !ec_vers.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 128784095957782000, "Gestione offerte"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "$", "H", "O", "Q", "R", "V", "X", "Y"
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "APRI;" & IIf(!ec_tipork.ToString = "Y", "H", !ec_tipork.ToString).ToString & ";" & _
                           !ec_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !ec_serie.ToString & ";" & _
                           !ec_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128784095937970000, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "D", "K", "P", "£", "("
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "APRI;" & !ec_tipork.ToString & ";" & _
                           !ec_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !ec_serie.ToString & ";" & _
                           !ec_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 128784095921434000, "Fatturazione differita interattiva"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case Else
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "APRI;" & IIf(!ec_tipork.ToString = "U", "T", !ec_tipork.ToString).ToString & ";" & _
                           !ec_anno.ToString.PadLeft(4, "0"c) & ";" & _
                           !ec_serie.ToString & ";" & _
                           !ec_numdoc.ToString.PadLeft(9, "0"c) & ";"
              End With
              oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128784095912854000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End Select
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 129611760462001953, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      '------------------------
      If grPrin.Focused Then
        If Not grvPrin.NTSGetCurrentDataRow Is Nothing Then
          strParam = "APRI;" & _
                     NTSCDate(grvPrin.NTSGetCurrentDataRow!pn_datreg).ToShortDateString & ";" & _
                     NTSCInt(grvPrin.NTSGetCurrentDataRow!pn_numreg).ToString("000000")
          oMenu.RunChild("BSCGPRIN", "CLSCGPRIN", oApp.Tr(Me, 128784102048828000, "Gestione prima nota"), DittaCorrente, "", "BNCGPRIN", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128784088053624000, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      '------------------------
      If grScad.Focused Then
        If Not grvScad.NTSGetCurrentDataRow Is Nothing Then
          bSaldo = False
          If NTSCInt(grvScad.NTSGetCurrentDataRow!sc_rgsaldato) <> 0 Then
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128784103780776000, "Apro la registrazione che ha saldato la scadenza (SI)" & vbCrLf & _
                                           "o quella che l'ha creata (NO)?")) = Windows.Forms.DialogResult.Yes Then bSaldo = True
          End If

          If NTSCInt(grvScad.NTSGetCurrentDataRow!sc_numreg) <> 0 And bSaldo = False Then
            strParam = "APRI;" & _
                       NTSCDate(grvScad.NTSGetCurrentDataRow!sc_datreg).ToShortDateString & ";" & _
                       NTSCInt(grvScad.NTSGetCurrentDataRow!sc_numreg).ToString("000000")
            oMenu.RunChild("BSCGPRIN", "CLSCGPRIN", oApp.Tr(Me, 128784105251562000, "Gestione prima nota"), DittaCorrente, "", "BNCGPRIN", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End If
          If NTSCInt(grvScad.NTSGetCurrentDataRow!sc_rgsaldato) <> 0 And bSaldo = True Then
            strParam = "APRI;" & _
                       NTSCDate(grvScad.NTSGetCurrentDataRow!sc_dtsaldato).ToShortDateString & ";" & _
                       NTSCInt(grvScad.NTSGetCurrentDataRow!sc_rgsaldato).ToString("000000")
            oMenu.RunChild("BSCGPRIN", "CLSCGPRIN", oApp.Tr(Me, 128784102032760000, "Gestione prima nota"), DittaCorrente, "", "BNCGPRIN", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End If
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128784088042080000, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      oApp.MsgBoxErr(oApp.Tr(Me, 128784088310604000, "Posizionarsi prima sulla riga di griglia da aprire"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCreaNewDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCreaNewDoc.ItemClick
    Dim strParam As String = ""
    Dim bSaldo As Boolean = False
    Dim lInstidTmp As Integer = 0
    Try
      '------------------------
      If grTesta.Focused Then
        If Not grvTesta.NTSGetCurrentDataRow Is Nothing Then
          Select Case grvTesta.NTSGetCurrentDataRow!et_tipork.ToString
            Case "!"
              lInstidTmp = oMenu.GetTblInstId("TTMOVOFF", False)
              strParam = "NUOV;" & lInstidTmp.ToString("000000000") & ";" & _
                         NTSCInt(grvTesta.NTSGetCurrentDataRow!et_codlead).ToString("000000000") & ";" & _
                         "000000000" & ";" & _
                         "000000000" & ";"
              oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 129611755600957032, "Gestione offerte"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "$", "H", "O", "Q", "R", "V", "X", "Y"
              With grvTesta.NTSGetCurrentDataRow
                strParam = "NUOD;" & IIf(!et_tipork.ToString = "Y", "H", !et_tipork.ToString).ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           NTSCInt(grvTesta.NTSGetCurrentDataRow!et_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 129611755573603516, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "D", "K", "P", "£", "("
              With grvTesta.NTSGetCurrentDataRow
                strParam = "NUOD;" & !et_tipork.ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           !et_serie.ToString & ";" & _
                           NTSCInt(grvTesta.NTSGetCurrentDataRow!et_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 129611755558798828, "Fatturazione differita interattiva"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case Else
              With grvTesta.NTSGetCurrentDataRow
                strParam = "NUOD;" & IIf(!et_tipork.ToString = "Y", "H", !et_tipork.ToString).ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           NTSCInt(grvTesta.NTSGetCurrentDataRow!et_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 129611755543203125, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End Select
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 129611760547597657, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      '------------------------
      If grCorpo.Focused Then
        If Not grvCorpo.NTSGetCurrentDataRow Is Nothing Then
          Select Case grvCorpo.NTSGetCurrentDataRow!ec_tipork.ToString
            Case "!"
              lInstidTmp = oMenu.GetTblInstId("TTMOVOFF", False)
              strParam = "NUOV;" & lInstidTmp.ToString("000000000") & ";" & _
                         NTSCInt(grvCorpo.NTSGetCurrentDataRow!xxo_codlead).ToString("000000000") & ";" & _
                         "000000000" & ";" & _
                         "000000000" & ";"
              oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 129611755640322266, "Gestione offerte"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "$", "H", "O", "Q", "R", "V", "X", "Y"
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "NUOD;" & IIf(!ec_tipork.ToString = "Y", "H", !ec_tipork.ToString).ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           NTSCInt(!xxo_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 129611755657919922, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case "D", "K", "P", "£", "("
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "NUOD;" & !ec_tipork.ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           !ec_serie.ToString & ";" & _
                           NTSCInt(!xxo_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 129611755675361328, "Fatturazione differita interattiva"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
            Case Else
              With grvCorpo.NTSGetCurrentDataRow
                strParam = "NUOD;" & IIf(!ec_tipork.ToString = "Y", "H", !ec_tipork.ToString).ToString & ";" & _
                           DateTime.Now.Year.ToString.PadLeft(4, "0"c) & ";" & _
                           NTSCInt(!xxo_conto).ToString("000000000") & ";"
              End With
              oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 129611755705361328, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, Not tlbNoModal.Checked, Not tlbNoModal.Checked)
          End Select
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128784088063296000, "La griglia su cui si è posizionati non contiene righe"))
        End If
        Return
      End If

      '------------------------
      If grPrin.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129611760386640625, "Non è possibile creare nuovi documenti quando si è posizionati sulla griglia di prima nota"))
        Return
      End If

      '------------------------
      If grScad.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129611760372158203, "Non è possibile creare nuovi documenti quando si è posizionati sulla griglia delle scadenze"))
        Return
      End If

      oApp.MsgBoxErr(oApp.Tr(Me, 129611760314804688, "Posizionarsi prima sulla riga di griglia 'testate' o 'corpo' documenti"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbLocalizzaGoogle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLocalizzaGoogle.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      If grTesta.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129769843783373039, "Posizionarsi prima nella griglia delle testate dei documenti"))
        Return
      End If

      dttTmp.Columns.Add("xx_sel", GetType(String))
      dttTmp.Columns.Add("xx_order", GetType(Integer))
      dttTmp.Columns.Add("xx_tipo", GetType(String))
      dttTmp.Columns.Add("xx_conto", GetType(Integer))
      dttTmp.Columns.Add("xx_coddest", GetType(Integer))
      dttTmp.TableName = "ANAGRAFICHE"

      For Each dtrT As DataRow In dsGri.Tables("TESTA").Select("", dcTesta.Sort)
        If NTSCInt(dtrT!et_codlead) <> 0 Then
          If dttTmp.Select("xx_tipo = 'L' AND xx_conto = " & dtrT!et_codlead.ToString).Length = 0 Then
            i += 1
            dttTmp.Rows.Add(New Object() {"S", i, "L", dtrT!et_codlead, 0})
          End If
        Else
          If dttTmp.Select("xx_tipo = 'A' AND xx_conto = " & dtrT!et_conto.ToString & "  AND xx_coddest = " & dtrT!et_coddest.ToString).Length = 0 Then
            i += 1
            dttTmp.Rows.Add(New Object() {"S", i, "A", dtrT!et_conto, dtrT!et_coddest})
          End If
        End If
      Next

      oPar.ctlPar1 = dttTmp
      oMenu.RunChild("NTSInformatica", "FRM__LOCA", "", DittaCorrente, "", "BN__LOCA", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub


  Public Overridable Sub tlbGriTesta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGriTesta.ItemClick
    Try
      If grTesta.Visible Then
        grTesta.Visible = False
      Else
        GctlSetVisEnab(grTesta, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGriCorpo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGriCorpo.ItemClick
    Try
      If grCorpo.Visible Then
        grCorpo.Visible = False
      Else
        GctlSetVisEnab(grCorpo, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGriPrin_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGriPrin.ItemClick
    Try
      If grPrin.Visible Then
        grPrin.Visible = False
      Else
        GctlSetVisEnab(grPrin, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGriScad_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGriScad.ItemClick
    Try
      If grScad.Visible Then
        grScad.Visible = False
      Else
        GctlSetVisEnab(grScad, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGriReset_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGriReset.ItemClick
    Try
      grScad.Height = 104
      grPrin.Height = 103
      grCorpo.Height = 103

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

  Public Overridable Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
    Try
      xx_nome.Enabled = Not xx_nome.Enabled
      grvFiltri.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cbScenario_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbScenario.SelectedIndexChanged
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing

    Try
      'il test sotto non funziona, visto che quando lancio la tlbSalva o check e la griglia contiene già le nuove impostazioni
      'dtrT = dttScenario.Select("val = " & CStrSQL(strScenarioCorrente))
      'If dtrT.Length > 0 Then
      '  strTmp = CalcolaStringaSchenario()
      '  If dtrT(0)!cod.ToString <> strTmp And dtrT(0)!cod.ToString <> "." Then
      '    If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128778915620978000, "Salvare lo scenario")) = Windows.Forms.DialogResult.Yes Then
      '      tlbSalva_ItemClick(tlbSalva, Nothing)
      '    End If
      '  End If
      'End If

      ApplicaScenario()
      strScenarioCorrente = cbScenario.Text
      xx_nome.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grvFiltri_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvFiltri.NTSCellValueChanged
    Try
      If e.Column.Name = "xx_valoreda" Then
        grvFiltri.SetFocusedRowCellValue(xx_valorea, e.Value)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "Ricalcolo della griglia 'CORPO'"
  Public Overridable Sub grvTesta_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTesta.NTSFocusedRowChanged
    Try
      If ckGriSoloUnDoc.Checked Then RicalcolaGrigliaCorpo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub ckGriSoloUnDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckGriSoloUnDoc.CheckedChanged
    Try
      RicalcolaGrigliaCorpo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckGriSoloArtFiltri_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckGriSoloArtFiltri.CheckedChanged
    Try
      RicalcolaGrigliaCorpo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function RicalcolaGrigliaCorpo() As Boolean
    Dim strQuery As String = ""
    Try
      If grvTesta.RowCount > 0 Then
        Me.Cursor = Cursors.WaitCursor
        strQuery = SettaQuery()
        grCorpo.DataSource = Nothing
        If Not grvTesta.NTSGetCurrentDataRow Is Nothing Then
          oCleFldo.CaricaGrigliaCorpo(ckOfferte.Checked, ckOrdini.Checked, ckNote.Checked, _
                                      ckMagaz.Checked, ckFatture.Checked, ckContab.Checked, _
                                      ckScad.Checked, strQuery, ckGriSoloUnDoc.Checked, _
                                      ckGriSoloArtFiltri.Checked, grvTesta.NTSGetCurrentDataRow, dsGri)
          If dsGri.Tables.Contains("CORPO") Then
            dcCorpo.DataSource = dsGri.Tables("CORPO")
            grCorpo.DataSource = dcCorpo
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Sub tsFldo_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsFldo.SelectedPageChanged
    Dim bTabZero As Boolean = False
    Try
      tlbStrumenti.Enabled = False

      If e.PrevPage.Equals(tsFldo.TabPages(0)) And e.Page.Equals(tsFldo.TabPages(2)) And bChiamatoDaChild = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128778100220877000, "Prima di passare alla visualizzazione del " & _
                      "flusso documentale occorre selezionare la riga di griglia da utilizzare " & _
                      "come nodo di partenza"))
        bTabZero = VaiTabZero()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If e.PrevPage.Equals(tsFldo.TabPages(0)) And e.Page.Equals(tsFldo.TabPages(1)) Then
        If GctlControllaOutNotEqual() = False Then
          bTabZero = VaiTabZero()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'entro nel tab delle griglia: devo ricalcolare le griglie se ho cambiato dei nel tab 1
      If e.Page.Equals(tsFldo.TabPages(1)) Then
        GctlSetVisEnab(tlbStrumenti, False)
        If e.PrevPage.Equals(tsFldo.TabPages(0)) Then
          If Not CaricaGriglie() Then
            bTabZero = VaiTabZero()
          End If
        End If
        If e.PrevPage.Equals(tsFldo.TabPages(2)) Then
          Select Case strLastGriFocus
            Case "grTesta" : grTesta.Focus()
            Case "grCorpo" : grCorpo.Focus()
            Case "grPrin" : grPrin.Focus()
            Case "grScad" : grScad.Focus()
          End Select
        End If
      End If    'If e.Page.Equals(tsFldo.TabPages(1)) Then

      '--------------------------------
      'entro nel tab della visualizzazione grafica: se sono posizionato su una griglia devo calcolare il flusso
      'prima devo capire su che griglia ero prima di arrivare qui
      If e.Page.Equals(tsFldo.TabPages(2)) Then
        If Not CalcolaFlusso() Then
          bTabZero = VaiTabZero()
        End If
      End If    'If e.Page.Equals(tsFldo.TabPages(2)) Then

      If e.Page.Equals(tsFldo.TabPages(0)) Then
        strLastGriFocus = ""
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbZoom, False)
      Else
        If bTabZero = False Then
          tlbNuovo.Enabled = False
          tlbSalva.Enabled = False
          tlbCancella.Enabled = False
          tlbRipristina.Enabled = False
          tlbZoom.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function VaiTabZero() As Boolean
    Try
      tsFldo.SelectedTabPageIndex = 0
      strLastGriFocus = ""
      GctlSetVisEnab(tlbNuovo, False)
      GctlSetVisEnab(tlbSalva, False)
      GctlSetVisEnab(tlbCancella, False)
      GctlSetVisEnab(tlbRipristina, False)
      GctlSetVisEnab(tlbZoom, False)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function



  Public Overridable Function CalcolaStringaSchenario() As String
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Try
      strTmp += IIf(ckOfferte.Checked, "S", "N").ToString
      strTmp += IIf(ckOrdini.Checked, "S", "N").ToString
      strTmp += IIf(ckNote.Checked, "S", "N").ToString
      strTmp += IIf(ckMagaz.Checked, "S", "N").ToString
      strTmp += IIf(ckFatture.Checked, "S", "N").ToString
      strTmp += IIf(ckContab.Checked, "S", "N").ToString
      strTmp += IIf(ckScad.Checked, "S", "N").ToString
      strTmp += ";"
      For i = 0 To MAXROW
        strTmp += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & ";"
      Next
      strTmp = strTmp.Substring(0, strTmp.Length - 1)
      Return strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Return ""
    End Try
  End Function
  Public Overridable Function ApplicaScenario() As Boolean
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Try
      If cbScenario.SelectedValue = "." Then
        strTmp = "NNNNNNN"
        For i = 0 To MAXROW
          strTmp += ";."
        Next
      Else
        strTmp = cbScenario.SelectedValue
      End If
      strT = strTmp.Split(";"c)

      'azzero i filtri globali
      'per ora non li azzero: potrebbero fare comodo per passare da uno 
      'scenario ad un'altro appartenenti alla stessa tipologia di analisi
      'edAnno.Text = DateTime.Now.Year.ToString
      'edClifor.Text = "0"
      'edArticolo.Text = ""
      'edCommessa.Text = "0"
      'edDataDa.Text = IntSetDate("01/01/1900")
      'edDataA.Text = IntSetDate("31/12/2099")
      'edOperatore.Text = ""
      'edLead.Text = "0"
      'Dim dttTmp As New DataTable
      'oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp)
      'If dttTmp.Rows.Count > 0 Then edEscomp.Text = dttTmp.Rows(0)!tb_escomp.ToString
      'dttTmp.Clear()
      'dttTmp = Nothing

      ckOfferte.Checked = CBool(IIf(strT(0).Substring(0, 1) = "S", True, False))
      ckOrdini.Checked = CBool(IIf(strT(0).Substring(1, 1) = "S", True, False))
      ckNote.Checked = CBool(IIf(strT(0).Substring(2, 1) = "S", True, False))
      ckMagaz.Checked = CBool(IIf(strT(0).Substring(3, 1) = "S", True, False))
      ckFatture.Checked = CBool(IIf(strT(0).Substring(4, 1) = "S", True, False))
      ckContab.Checked = CBool(IIf(strT(0).Substring(5, 1) = "S", True, False))
      ckScad.Checked = CBool(IIf(strT(0).Substring(6, 1) = "S", True, False))
      For i = 0 To MAXROW
        dsFiltri.Tables("FILTRI").Rows(i)!xx_nome = strT(i + 1)
        If bInSalva = False Then
          dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda = ""
          dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea = ""
        End If
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub ApriZoomTabella(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try
      If strCampo.IndexOf("ftestmag.") > -1 Then strCampo = strCampo.Substring(1)
      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = strCampo.Substring(0, strCampo.IndexOf("."))
        oParam.strDescr = strCampo.Substring(oParam.strTipo.Length + 1)
        NTSZOOM.strIn = NTSCStr(grvFiltri.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri.EditingValue) Then grvFiltri.SetFocusedValue(NTSZOOM.strIn)
      Else
        SetFastZoom(NTSCStr(grvFiltri.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvFiltri.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri.EditingValue) Then grvFiltri.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function CheckFiltri() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsFiltri.Tables("FILTRI").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_nome) = "." Then
          dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda = ""
          dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea = ""
        Else
          If NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda) <> "" Then
            dtrT = dttTipi.Select("val = " & CStrSQL(dsFiltri.Tables("FILTRI").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!tipo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128780431170954000, "Nel filtro '|" & NTSCStr(dtrT(0)!cod) & "|' sono ammessi solo numeri"))
                  Return False
                End If
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128780432078626000, "Nel filtro '|" & NTSCStr(dtrT(0)!cod) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro '|" & NTSCStr(dtrT(0)!cod) & "|' sono ammesse solo date"))
                  Return False
                End If
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128780432235874000, "Nel filtro '|" & NTSCStr(dtrT(0)!cod) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If
        End If
      Next
      dsFiltri.Tables("FILTRI").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub grTesta_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grTesta.Enter
    Try
      strLastGriFocus = "grTesta"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grCorpo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grCorpo.Enter
    Try
      strLastGriFocus = "grCorpo"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grPrin_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grPrin.Enter
    Try
      strLastGriFocus = "grPrin"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grScad_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grScad.Enter
    Try
      strLastGriFocus = "grScad"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function SettaQuery() As String
    Dim strQuery As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try

      '----------------------
      'griglia FILTRI
      For i = 0 To dsFiltri.Tables("FILTRI").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_nome).Trim <> "." Then
          If NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea).Trim <> "" Then
            dtrT = dttTipi.Select("val = " & CStrSQL(dsFiltri.Tables("FILTRI").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!tipo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea)) & "§"
              Case 8
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " >= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " <= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea)) & "§"
              Case Else
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " >= " & CStrSQL(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI").Rows(i)!xx_nome.ToString & " <= " & CStrSQL(NTSCStr(dsFiltri.Tables("FILTRI").Rows(i)!xx_valorea)) & "§"
            End Select
          End If
        End If
      Next    'For i = 0 To dsFiltri.Tables("FILTRI").Rows.Count - 1

      '----------------------
      'aggiungo i filtri globali
      If ckOfferte.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "testoff.td_conto = " & edClifor.Text & "§"
        If NTSCInt(edLead.Text) <> 0 Then strQuery += "testoff.td_codlead = " & edLead.Text & "§"
        If edArticolo.Text <> "" Then strQuery += "movoff.mo_codart = " & CStrSQL(edArticolo.Text) & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "testoff.td_datord >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "testoff.td_datord <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "testoff.td_anno = " & edAnno.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "testoff.td_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckOrdini.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "testord.td_conto = " & edClifor.Text & "§"
        If edArticolo.Text <> "" Then strQuery += "movord.mo_codart = " & CStrSQL(edArticolo.Text) & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "testord.td_datord >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "testord.td_datord <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "testord.td_anno = " & edAnno.Text & "§"
        If NTSCInt(edCommessa.Text) <> 0 Then strQuery += "testord.td_commeca = " & edCommessa.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "testord.td_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckNote.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "testprb.tm_conto = " & edClifor.Text & "§"
        If edArticolo.Text <> "" Then strQuery += "movprb.mm_codart = " & CStrSQL(edArticolo.Text) & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "testprb.tm_datdoc >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "testprb.tm_datdoc <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "testprb.tm_anno = " & edAnno.Text & "§"
        If NTSCInt(edCommessa.Text) <> 0 Then strQuery += "testprb.tm_commeca = " & edCommessa.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "testprb.tm_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckMagaz.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "testmag.tm_conto = " & edClifor.Text & "§"
        If edArticolo.Text <> "" Then strQuery += "movmag.mm_codart = " & CStrSQL(edArticolo.Text) & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "testmag.tm_datdoc >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "testmag.tm_datdoc <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "testmag.tm_anno = " & edAnno.Text & "§"
        If NTSCInt(edCommessa.Text) <> 0 Then strQuery += "testmag.tm_commeca = " & edCommessa.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "testmag.tm_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckFatture.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "ftestmag.tm_conto = " & edClifor.Text & "§"
        If edArticolo.Text <> "" Then strQuery += "movmag.mm_codart = " & CStrSQL(edArticolo.Text) & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "ftestmag.tm_datdoc >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "ftestmag.tm_datdoc <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "ftestmag.tm_anno = " & edAnno.Text & "§"
        If NTSCInt(edCommessa.Text) <> 0 Then strQuery += "ftestmag.tm_commeca = " & edCommessa.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "ftestmag.tm_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckContab.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "prinot.pn_conto = " & edClifor.Text & "§"
        If NTSCInt(edEscomp.Text) <> 0 Then strQuery += "prinot.pn_escomp = " & edEscomp.Text & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "prinot.pn_annpar = " & edAnno.Text & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "prinot.pn_datreg >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "prinot.pn_datreg <= " & CDataSQL(edDataA.Text) & "§"
        If edOperatore.Text <> "" Then strQuery += "prinot.pn_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If
      If ckScad.Checked Then
        If NTSCInt(edClifor.Text) <> 0 Then strQuery += "scaden.sc_conto = " & edClifor.Text & "§"
        If NTSCInt(edAnno.Text) <> 0 Then strQuery += "scaden.sc_annpar = " & edAnno.Text & "§"
        If NTSCDate(edDataDa.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += "scaden.sc_datsca >= " & CDataSQL(edDataDa.Text) & "§"
        If NTSCDate(edDataA.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += "scaden.sc_datsca <= " & CDataSQL(edDataA.Text) & "§"
        If NTSCInt(edCommessa.Text) <> 0 Then strQuery += "scaden.sc_commeca = " & edCommessa.Text & "§"
        If edOperatore.Text <> "" Then strQuery += "scaden.sc_opnome = " & CStrSQL(edOperatore.Text) & "§"
      End If

      If strQuery <> "" Then strQuery = strQuery.Substring(0, strQuery.Length - 1)

      Return strQuery

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Return ""
    End Try
  End Function


  Public Overridable Function CaricaGriglie() As Boolean
    Dim strQuery As String = ""
    Try
      Me.ValidaLastControl()

      If Not CheckFiltri() Then Return False

      Me.Cursor = Cursors.WaitCursor

      strQuery = SettaQuery()

      '----------------------
      'scollego le griglie
      grTesta.DataSource = Nothing
      grCorpo.DataSource = Nothing
      grPrin.DataSource = Nothing
      grScad.DataSource = Nothing

      If Not oCleFldo.CaricaGriglie(ckOfferte.Checked, ckOrdini.Checked, ckNote.Checked, _
                                    ckMagaz.Checked, ckFatture.Checked, ckContab.Checked, _
                                    ckScad.Checked, strQuery, ckGriSoloUnDoc.Checked, _
                                    ckGriSoloArtFiltri.Checked, dsGri) Then Return False

      '----------------------
      'collego le griglie
      dsGri.AcceptChanges()

      If dsGri.Tables.Contains("TESTA") Then
        dcTesta.DataSource = dsGri.Tables("TESTA")
        grTesta.DataSource = dcTesta
      End If
      If dsGri.Tables.Contains("CORPO") Then
        dcCorpo.DataSource = dsGri.Tables("CORPO")
        grCorpo.DataSource = dcCorpo
      End If
      If dsGri.Tables.Contains("PRIN") Then
        dcPrin.DataSource = dsGri.Tables("PRIN")
        grPrin.DataSource = dcPrin
      End If
      If dsGri.Tables.Contains("SCAD") Then
        dcScad.DataSource = dsGri.Tables("SCAD")
        grScad.DataSource = dcScad
      End If

      If ckGriSoloUnDoc.Checked Then RicalcolaGrigliaCorpo()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function CalcolaFlusso() As Boolean
    Dim strTipork As String = ""
    Dim dtrT As DataRow = Nothing
    Try
      If bChiamatoDaChild Then Return True

      If strLastGriFocus = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128787510962236790, "Prima di passare alla visualizzazione del " & _
                              "flusso documentale occorre selezionare la riga di griglia da utilizzare " & _
                              "come nodo di partenza"))
        Return False
      End If

      Select Case strLastGriFocus
        Case "grTesta"
          If grvTesta.RowCount > 0 Then
            dtrT = grvTesta.NTSGetCurrentDataRow
            strTipork = dtrT!et_tipork.ToString
          End If
        Case "grCorpo"
          If grvCorpo.RowCount > 0 Then
            dtrT = grvCorpo.NTSGetCurrentDataRow
            strTipork = dtrT!ec_tipork.ToString
          End If
        Case "grPrin"
          If grvPrin.RowCount > 0 Then
            dtrT = grvPrin.NTSGetCurrentDataRow
            strTipork = "1"
          End If
        Case "grScad"
          If grvScad.RowCount > 0 Then
            dtrT = grvScad.NTSGetCurrentDataRow
            strTipork = "2"
          End If
      End Select

      Select Case strTipork
        Case ""
          oApp.MsgBoxErr(oApp.Tr(Me, 128787507067762790, "Nella griglia su cui si era posizionati non sono presenti righe"))
          Return False
        Case "1"
          'prima nota
          If Not ceFldo.CalcolaFlusso(strTipork, 0, "", 0, 0, NTSCInt(dtrT!pn_conto), NTSCDate(dtrT!pn_datreg).ToShortDateString, NTSCInt(dtrT!pn_numreg), NTSCInt(dtrT!pn_riga), 0, "", 0, 0) Then Return False
        Case "2"
          'scadenze
          If Not ceFldo.CalcolaFlusso(strTipork, 0, "", 0, 0, NTSCInt(dtrT!sc_conto), "", 0, 0, NTSCInt(dtrT!sc_annpar), dtrT!sc_alfpar.ToString, NTSCInt(dtrT!sc_numpar), NTSCInt(dtrT!sc_numrata)) Then Return False
        Case Else
          If strLastGriFocus = "grTesta" Then
            'testa
            If Not ceFldo.CalcolaFlusso(strTipork, NTSCInt(dtrT!et_anno), dtrT!et_serie.ToString, NTSCInt(dtrT!et_numdoc), NTSCInt(dtrT!et_vers), 0, "", 0, 0, 0, "", 0, 0) Then Return False
          Else
            'corpo
            If Not ceFldo.CalcolaFlusso(strTipork, NTSCInt(dtrT!ec_anno), dtrT!ec_serie.ToString, NTSCInt(dtrT!ec_numdoc), NTSCInt(dtrT!ec_vers), 0, "", 0, 0, 0, "", 0, 0) Then Return False
          End If
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class