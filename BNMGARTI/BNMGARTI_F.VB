Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRMMGARTI_F
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE + bsModExtCRM
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

#Region "Variabili"
  Public oCleArti As CLEMGARTI
  Public dsArti As DataSet
  Public oCallParams As CLE__CLDP
  Public dcArti As BindingSource = New BindingSource

  Public nStato As Integer

  Private components As System.ComponentModel.IContainer
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
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents pnArti As NTSInformatica.NTSPanel
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbKit As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaEtichette As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbVisualizzaMovimenti As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents tsArti As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag1 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag3 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag4 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag5 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag6 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage7 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag7 As NTSInformatica.NTSPanel
  Public WithEvents edAr_note As NTSInformatica.NTSMemoBox
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents fmUnitamisura As NTSInformatica.NTSGroupBox
  Public WithEvents edAr_unmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_unmis As NTSInformatica.NTSLabel
  Public WithEvents lbAr_conver As NTSInformatica.NTSLabel
  Public WithEvents lbAr_unmis2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_conver As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_confez2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_unmis2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_qtacon2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_confez2 As NTSInformatica.NTSLabel
  Public WithEvents pnTabpag3Right As NTSInformatica.NTSPanel
  Public WithEvents fmAltriDati As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_stainv As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_stasche As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_pesoca As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_stalist As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_pesolor As NTSInformatica.NTSLabel
  Public WithEvents edAr_pesolor As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_pesonet As NTSInformatica.NTSLabel
  Public WithEvents edAr_pesonet As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTabpag3Left As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codiva As NTSInformatica.NTSLabel
  Public WithEvents lbXx_gruppo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_sotgru As NTSInformatica.NTSLabel
  Public WithEvents lbXx_controp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_controa As NTSInformatica.NTSLabel
  Public WithEvents lbXx_famprod As NTSInformatica.NTSLabel
  Public WithEvents lbAr_famprod As NTSInformatica.NTSLabel
  Public WithEvents edAr_famprod As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_codiva As NTSInformatica.NTSLabel
  Public WithEvents edAr_codiva As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_gruppo As NTSInformatica.NTSLabel
  Public WithEvents edAr_gruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_sotgru As NTSInformatica.NTSLabel
  Public WithEvents edAr_sotgru As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_controp As NTSInformatica.NTSLabel
  Public WithEvents edAr_controp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_controa As NTSInformatica.NTSLabel
  Public WithEvents edAr_controa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_tipo As NTSInformatica.NTSLabel
  Public WithEvents edAr_tipo As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnTopLeft As NTSInformatica.NTSPanel
  Public WithEvents lbAr_codart As NTSInformatica.NTSLabel
  Public WithEvents edAr_desint As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_codalt As NTSInformatica.NTSLabel
  Public WithEvents lbAr_descr As NTSInformatica.NTSLabel
  Public WithEvents edAr_codalt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_descr As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents ceListini As NTSInformatica.NTSXXLIST
  Public WithEvents ceSconti As NTSInformatica.NTSXXSCON
  Public WithEvents ceProvvig As NTSInformatica.NTSXXPROV
  Public WithEvents pnTopLeftBut As NTSInformatica.NTSPanel
  Public WithEvents cmdCodarfo As NTSInformatica.NTSButton
  Public WithEvents cmdProgressivi As NTSInformatica.NTSButton
  Public WithEvents cmdValuta As NTSInformatica.NTSButton
  Public WithEvents cmdProgtot As NTSInformatica.NTSButton
  Public WithEvents OpenFileDialog1 As NTSOpenFileDialog
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARTI_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbStampaEtichette = New NTSInformatica.NTSBarMenuItem
    Me.tlbVisualizzaMovimenti = New NTSInformatica.NTSBarMenuItem
    Me.tlbBarcode = New NTSInformatica.NTSBarButtonItem
    Me.tlbKit = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnArti = New NTSInformatica.NTSPanel
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tsArti = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTabpag1 = New NTSInformatica.NTSPanel
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.lbAr_flricmar = New NTSInformatica.NTSLabel
    Me.lbXx_reparto = New NTSInformatica.NTSLabel
    Me.lbXx_codpdon = New NTSInformatica.NTSLabel
    Me.cbAr_flricmar = New NTSInformatica.NTSComboBox
    Me.lbAr_tipokit = New NTSInformatica.NTSLabel
    Me.edAr_ricar1 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_ricar2 = New NTSInformatica.NTSTextBoxNum
    Me.cbAr_tipokit = New NTSInformatica.NTSComboBox
    Me.lbAr_codpdon = New NTSInformatica.NTSLabel
    Me.edAr_codpdon = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_reparto = New NTSInformatica.NTSLabel
    Me.edAr_reparto = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_perqta = New NTSInformatica.NTSLabel
    Me.edAr_perqta = New NTSInformatica.NTSTextBoxNum
    Me.fmAcquisti = New NTSInformatica.NTSGroupBox
    Me.lbXx_forn = New NTSInformatica.NTSLabel
    Me.lbXx_forn2 = New NTSInformatica.NTSLabel
    Me.lbAr_scomax = New NTSInformatica.NTSLabel
    Me.lbAr_scomin = New NTSInformatica.NTSLabel
    Me.edAr_scomin = New NTSInformatica.NTSTextBoxNum
    Me.edAr_scomax = New NTSInformatica.NTSTextBoxNum
    Me.edAr_forn = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_forn = New NTSInformatica.NTSLabel
    Me.edAr_forn2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_forn2 = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTabpag3 = New NTSInformatica.NTSPanel
    Me.pnTabpag3Right = New NTSInformatica.NTSPanel
    Me.imArtGif = New NTSInformatica.NTSPictureBox
    Me.fmAltriDati = New NTSInformatica.NTSGroupBox
    Me.ckAr_stainv = New NTSInformatica.NTSCheckBox
    Me.ckAr_stasche = New NTSInformatica.NTSCheckBox
    Me.ckAr_pesoca = New NTSInformatica.NTSCheckBox
    Me.ckAr_stalist = New NTSInformatica.NTSCheckBox
    Me.pnTabpag3Left = New NTSInformatica.NTSPanel
    Me.cmdArtgif2 = New NTSInformatica.NTSButton
    Me.lbAr_pesonet = New NTSInformatica.NTSLabel
    Me.cmdArtGif1 = New NTSInformatica.NTSButton
    Me.edAr_pesolor = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_pesolor = New NTSInformatica.NTSLabel
    Me.cmdVisGif2 = New NTSInformatica.NTSButton
    Me.edAr_pesonet = New NTSInformatica.NTSTextBoxNum
    Me.cmdVisGif1 = New NTSInformatica.NTSButton
    Me.edAr_gif2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_tipo = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_tipo = New NTSInformatica.NTSLabel
    Me.lbAr_gif2 = New NTSInformatica.NTSLabel
    Me.edAr_gif1 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_gif1 = New NTSInformatica.NTSLabel
    Me.lbXx_clascon = New NTSInformatica.NTSLabel
    Me.lbXx_claprov = New NTSInformatica.NTSLabel
    Me.lbAr_claprov = New NTSInformatica.NTSLabel
    Me.edAr_claprov = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_clascon = New NTSInformatica.NTSLabel
    Me.edAr_clascon = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codiva = New NTSInformatica.NTSLabel
    Me.lbXx_gruppo = New NTSInformatica.NTSLabel
    Me.lbXx_sotgru = New NTSInformatica.NTSLabel
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbXx_controa = New NTSInformatica.NTSLabel
    Me.lbXx_famprod = New NTSInformatica.NTSLabel
    Me.lbAr_famprod = New NTSInformatica.NTSLabel
    Me.edAr_famprod = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_codiva = New NTSInformatica.NTSLabel
    Me.edAr_codiva = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_gruppo = New NTSInformatica.NTSLabel
    Me.edAr_gruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_sotgru = New NTSInformatica.NTSLabel
    Me.edAr_sotgru = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_controp = New NTSInformatica.NTSLabel
    Me.edAr_controp = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_controa = New NTSInformatica.NTSLabel
    Me.edAr_controa = New NTSInformatica.NTSTextBoxNum
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnTabpag4 = New NTSInformatica.NTSPanel
    Me.ceListini = New NTSInformatica.NTSXXLIST
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnTabpag5 = New NTSInformatica.NTSPanel
    Me.ceSconti = New NTSInformatica.NTSXXSCON
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnTabpag6 = New NTSInformatica.NTSPanel
    Me.ceProvvig = New NTSInformatica.NTSXXPROV
    Me.NtsTabPage7 = New NTSInformatica.NTSTabPage
    Me.pnTabpag7 = New NTSInformatica.NTSPanel
    Me.edAr_note = New NTSInformatica.NTSMemoBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.fmUnitamisura = New NTSInformatica.NTSGroupBox
    Me.edAr_unmis = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_unmis = New NTSInformatica.NTSLabel
    Me.lbAr_conver = New NTSInformatica.NTSLabel
    Me.lbAr_unmis2 = New NTSInformatica.NTSLabel
    Me.edAr_conver = New NTSInformatica.NTSTextBoxNum
    Me.edAr_confez2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_unmis2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_qtacon2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_confez2 = New NTSInformatica.NTSLabel
    Me.pnTopLeftBut = New NTSInformatica.NTSPanel
    Me.cmdCodarfo = New NTSInformatica.NTSButton
    Me.cmdProgressivi = New NTSInformatica.NTSButton
    Me.cmdProgtot = New NTSInformatica.NTSButton
    Me.cmdValuta = New NTSInformatica.NTSButton
    Me.pnTopLeft = New NTSInformatica.NTSPanel
    Me.lbAr_codart = New NTSInformatica.NTSLabel
    Me.edAr_desint = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_codalt = New NTSInformatica.NTSLabel
    Me.lbAr_descr = New NTSInformatica.NTSLabel
    Me.edAr_codalt = New NTSInformatica.NTSTextBoxStr
    Me.edAr_descr = New NTSInformatica.NTSTextBoxStr
    Me.edAr_codart = New NTSInformatica.NTSTextBoxStr
    Me.edFocus = New NTSInformatica.NTSTextBoxNum
    Me.tlbSimula = New NTSInformatica.NTSBarMenuItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnArti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnArti.SuspendLayout()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tsArti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsArti.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnTabpag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag1.SuspendLayout()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.cbAr_flricmar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ricar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ricar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_tipokit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codpdon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_reparto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAcquisti.SuspendLayout()
    CType(Me.edAr_scomin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_scomax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_forn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_forn2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3.SuspendLayout()
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3Right.SuspendLayout()
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAltriDati.SuspendLayout()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTabpag3Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3Left.SuspendLayout()
    CType(Me.edAr_pesolor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_pesonet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_tipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_claprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_clascon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_famprod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_sotgru.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_controa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag4.SuspendLayout()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag5.SuspendLayout()
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag6.SuspendLayout()
    Me.NtsTabPage7.SuspendLayout()
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag7.SuspendLayout()
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUnitamisura.SuspendLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_conver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_qtacon2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeftBut.SuspendLayout()
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeft.SuspendLayout()
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codalt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_descr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbDuplica, Me.tlbApri, Me.tlbKit, Me.tlbStampaEtichette, Me.tlbVisualizzaMovimenti, Me.tlbBarcode, Me.tlbSimula})
    Me.NtsBarManager1.MaxItemId = 46
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbBarcode), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbKit), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 26
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 27
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichette), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisualizzaMovimenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSimula, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbStampaEtichette
    '
    Me.tlbStampaEtichette.Caption = "Stampa Etichette"
    Me.tlbStampaEtichette.Id = 32
    Me.tlbStampaEtichette.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbStampaEtichette.Name = "tlbStampaEtichette"
    Me.tlbStampaEtichette.NTSIsCheckBox = False
    Me.tlbStampaEtichette.Visible = True
    '
    'tlbVisualizzaMovimenti
    '
    Me.tlbVisualizzaMovimenti.Caption = "Visualizza movimenti"
    Me.tlbVisualizzaMovimenti.Id = 36
    Me.tlbVisualizzaMovimenti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbVisualizzaMovimenti.Name = "tlbVisualizzaMovimenti"
    Me.tlbVisualizzaMovimenti.NTSIsCheckBox = False
    Me.tlbVisualizzaMovimenti.Visible = True
    '
    'tlbBarcode
    '
    Me.tlbBarcode.Caption = "Barcode"
    Me.tlbBarcode.Glyph = CType(resources.GetObject("tlbBarcode.Glyph"), System.Drawing.Image)
    Me.tlbBarcode.Id = 44
    Me.tlbBarcode.Name = "tlbBarcode"
    Me.tlbBarcode.Visible = True
    '
    'tlbKit
    '
    Me.tlbKit.Caption = "Kit"
    Me.tlbKit.Glyph = CType(resources.GetObject("tlbKit.Glyph"), System.Drawing.Image)
    Me.tlbKit.Id = 29
    Me.tlbKit.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K))
    Me.tlbKit.Name = "tlbKit"
    Me.tlbKit.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnArti
    '
    Me.pnArti.AllowDrop = True
    Me.pnArti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnArti.Appearance.Options.UseBackColor = True
    Me.pnArti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnArti.Controls.Add(Me.pnMain)
    Me.pnArti.Controls.Add(Me.pnTop)
    Me.pnArti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnArti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnArti.Location = New System.Drawing.Point(0, 30)
    Me.pnArti.Name = "pnArti"
    Me.pnArti.NTSActiveTrasparency = True
    Me.pnArti.Size = New System.Drawing.Size(764, 449)
    Me.pnArti.TabIndex = 503
    Me.pnArti.Text = "NtsPanel1"
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsArti)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 99)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(764, 350)
    Me.pnMain.TabIndex = 576
    Me.pnMain.Text = "NtsPanel2"
    '
    'tsArti
    '
    Me.tsArti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsArti.Location = New System.Drawing.Point(0, 0)
    Me.tsArti.Name = "tsArti"
    Me.tsArti.SelectedTabPage = Me.NtsTabPage1
    Me.tsArti.Size = New System.Drawing.Size(764, 350)
    Me.tsArti.TabIndex = 582
    Me.tsArti.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6, Me.NtsTabPage7})
    Me.tsArti.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnTabpag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage1.Text = "&1 - Vendite/Acquisti"
    '
    'pnTabpag1
    '
    Me.pnTabpag1.AllowDrop = True
    Me.pnTabpag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag1.Appearance.Options.UseBackColor = True
    Me.pnTabpag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag1.Controls.Add(Me.NtsGroupBox1)
    Me.pnTabpag1.Controls.Add(Me.fmAcquisti)
    Me.pnTabpag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag1.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag1.Name = "pnTabpag1"
    Me.pnTabpag1.NTSActiveTrasparency = True
    Me.pnTabpag1.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag1.TabIndex = 611
    Me.pnTabpag1.Text = "NtsPanel1"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.lbAr_flricmar)
    Me.NtsGroupBox1.Controls.Add(Me.lbXx_reparto)
    Me.NtsGroupBox1.Controls.Add(Me.lbXx_codpdon)
    Me.NtsGroupBox1.Controls.Add(Me.cbAr_flricmar)
    Me.NtsGroupBox1.Controls.Add(Me.lbAr_tipokit)
    Me.NtsGroupBox1.Controls.Add(Me.edAr_ricar1)
    Me.NtsGroupBox1.Controls.Add(Me.edAr_ricar2)
    Me.NtsGroupBox1.Controls.Add(Me.cbAr_tipokit)
    Me.NtsGroupBox1.Controls.Add(Me.lbAr_codpdon)
    Me.NtsGroupBox1.Controls.Add(Me.edAr_codpdon)
    Me.NtsGroupBox1.Controls.Add(Me.lbAr_reparto)
    Me.NtsGroupBox1.Controls.Add(Me.edAr_reparto)
    Me.NtsGroupBox1.Controls.Add(Me.lbAr_perqta)
    Me.NtsGroupBox1.Controls.Add(Me.edAr_perqta)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(3, 3)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(674, 132)
    Me.NtsGroupBox1.TabIndex = 670
    Me.NtsGroupBox1.Text = "VENDITE"
    '
    'lbAr_flricmar
    '
    Me.lbAr_flricmar.AutoSize = True
    Me.lbAr_flricmar.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_flricmar.Location = New System.Drawing.Point(6, 112)
    Me.lbAr_flricmar.Name = "lbAr_flricmar"
    Me.lbAr_flricmar.NTSDbField = ""
    Me.lbAr_flricmar.Size = New System.Drawing.Size(86, 13)
    Me.lbAr_flricmar.TabIndex = 671
    Me.lbAr_flricmar.Text = "Ricarico/Margine"
    Me.lbAr_flricmar.Tooltip = ""
    Me.lbAr_flricmar.UseMnemonic = False
    '
    'lbXx_reparto
    '
    Me.lbXx_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_reparto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_reparto.Location = New System.Drawing.Point(211, 43)
    Me.lbXx_reparto.Name = "lbXx_reparto"
    Me.lbXx_reparto.NTSDbField = ""
    Me.lbXx_reparto.Size = New System.Drawing.Size(446, 20)
    Me.lbXx_reparto.TabIndex = 675
    Me.lbXx_reparto.Tooltip = ""
    Me.lbXx_reparto.UseMnemonic = False
    '
    'lbXx_codpdon
    '
    Me.lbXx_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpdon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpdon.Location = New System.Drawing.Point(211, 87)
    Me.lbXx_codpdon.Name = "lbXx_codpdon"
    Me.lbXx_codpdon.NTSDbField = ""
    Me.lbXx_codpdon.Size = New System.Drawing.Size(446, 20)
    Me.lbXx_codpdon.TabIndex = 675
    Me.lbXx_codpdon.Tooltip = ""
    Me.lbXx_codpdon.UseMnemonic = False
    '
    'cbAr_flricmar
    '
    Me.cbAr_flricmar.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbAr_flricmar.DataSource = Nothing
    Me.cbAr_flricmar.DisplayMember = ""
    Me.cbAr_flricmar.Location = New System.Drawing.Point(155, 109)
    Me.cbAr_flricmar.Name = "cbAr_flricmar"
    Me.cbAr_flricmar.NTSDbField = ""
    Me.cbAr_flricmar.Properties.AutoHeight = False
    Me.cbAr_flricmar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_flricmar.Properties.DropDownRows = 30
    Me.cbAr_flricmar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_flricmar.SelectedValue = ""
    Me.cbAr_flricmar.Size = New System.Drawing.Size(106, 20)
    Me.cbAr_flricmar.TabIndex = 672
    Me.cbAr_flricmar.ValueMember = ""
    '
    'lbAr_tipokit
    '
    Me.lbAr_tipokit.AutoSize = True
    Me.lbAr_tipokit.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipokit.Location = New System.Drawing.Point(6, 68)
    Me.lbAr_tipokit.Name = "lbAr_tipokit"
    Me.lbAr_tipokit.NTSDbField = ""
    Me.lbAr_tipokit.Size = New System.Drawing.Size(46, 13)
    Me.lbAr_tipokit.TabIndex = 673
    Me.lbAr_tipokit.Text = "Tipo KIT"
    Me.lbAr_tipokit.Tooltip = ""
    Me.lbAr_tipokit.UseMnemonic = False
    '
    'edAr_ricar1
    '
    Me.edAr_ricar1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ricar1.EditValue = "0"
    Me.edAr_ricar1.Location = New System.Drawing.Point(274, 109)
    Me.edAr_ricar1.Name = "edAr_ricar1"
    Me.edAr_ricar1.NTSDbField = ""
    Me.edAr_ricar1.NTSFormat = "0"
    Me.edAr_ricar1.NTSForzaVisZoom = False
    Me.edAr_ricar1.NTSOldValue = ""
    Me.edAr_ricar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ricar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ricar1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ricar1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ricar1.Properties.AutoHeight = False
    Me.edAr_ricar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ricar1.Properties.MaxLength = 65536
    Me.edAr_ricar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ricar1.Size = New System.Drawing.Size(73, 20)
    Me.edAr_ricar1.TabIndex = 673
    Me.edAr_ricar1.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_ricar1.TextInt = 0
    '
    'edAr_ricar2
    '
    Me.edAr_ricar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ricar2.EditValue = "0"
    Me.edAr_ricar2.Location = New System.Drawing.Point(353, 109)
    Me.edAr_ricar2.Name = "edAr_ricar2"
    Me.edAr_ricar2.NTSDbField = ""
    Me.edAr_ricar2.NTSFormat = "0"
    Me.edAr_ricar2.NTSForzaVisZoom = False
    Me.edAr_ricar2.NTSOldValue = ""
    Me.edAr_ricar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ricar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ricar2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ricar2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ricar2.Properties.AutoHeight = False
    Me.edAr_ricar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ricar2.Properties.MaxLength = 65536
    Me.edAr_ricar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ricar2.Size = New System.Drawing.Size(73, 20)
    Me.edAr_ricar2.TabIndex = 674
    Me.edAr_ricar2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_ricar2.TextInt = 0
    '
    'cbAr_tipokit
    '
    Me.cbAr_tipokit.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipokit.DataSource = Nothing
    Me.cbAr_tipokit.DisplayMember = ""
    Me.cbAr_tipokit.Location = New System.Drawing.Point(155, 65)
    Me.cbAr_tipokit.Name = "cbAr_tipokit"
    Me.cbAr_tipokit.NTSDbField = ""
    Me.cbAr_tipokit.Properties.AutoHeight = False
    Me.cbAr_tipokit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipokit.Properties.DropDownRows = 30
    Me.cbAr_tipokit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipokit.SelectedValue = ""
    Me.cbAr_tipokit.Size = New System.Drawing.Size(136, 20)
    Me.cbAr_tipokit.TabIndex = 674
    Me.cbAr_tipokit.ValueMember = ""
    '
    'lbAr_codpdon
    '
    Me.lbAr_codpdon.AutoSize = True
    Me.lbAr_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codpdon.Location = New System.Drawing.Point(6, 90)
    Me.lbAr_codpdon.Name = "lbAr_codpdon"
    Me.lbAr_codpdon.NTSDbField = ""
    Me.lbAr_codpdon.Size = New System.Drawing.Size(79, 13)
    Me.lbAr_codpdon.TabIndex = 671
    Me.lbAr_codpdon.Text = "Relazione listini"
    Me.lbAr_codpdon.Tooltip = ""
    Me.lbAr_codpdon.UseMnemonic = False
    '
    'edAr_codpdon
    '
    Me.edAr_codpdon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codpdon.EditValue = "0"
    Me.edAr_codpdon.Location = New System.Drawing.Point(155, 87)
    Me.edAr_codpdon.Name = "edAr_codpdon"
    Me.edAr_codpdon.NTSDbField = ""
    Me.edAr_codpdon.NTSFormat = "0"
    Me.edAr_codpdon.NTSForzaVisZoom = False
    Me.edAr_codpdon.NTSOldValue = ""
    Me.edAr_codpdon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codpdon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codpdon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codpdon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codpdon.Properties.AutoHeight = False
    Me.edAr_codpdon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codpdon.Properties.MaxLength = 65536
    Me.edAr_codpdon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codpdon.Size = New System.Drawing.Size(50, 20)
    Me.edAr_codpdon.TabIndex = 672
    Me.edAr_codpdon.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_codpdon.TextInt = 0
    '
    'lbAr_reparto
    '
    Me.lbAr_reparto.AutoSize = True
    Me.lbAr_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_reparto.Location = New System.Drawing.Point(6, 46)
    Me.lbAr_reparto.Name = "lbAr_reparto"
    Me.lbAr_reparto.NTSDbField = ""
    Me.lbAr_reparto.Size = New System.Drawing.Size(77, 13)
    Me.lbAr_reparto.TabIndex = 669
    Me.lbAr_reparto.Text = "Reparto (ECR)"
    Me.lbAr_reparto.Tooltip = ""
    Me.lbAr_reparto.UseMnemonic = False
    '
    'edAr_reparto
    '
    Me.edAr_reparto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_reparto.EditValue = "0"
    Me.edAr_reparto.Location = New System.Drawing.Point(155, 43)
    Me.edAr_reparto.Name = "edAr_reparto"
    Me.edAr_reparto.NTSDbField = ""
    Me.edAr_reparto.NTSFormat = "0"
    Me.edAr_reparto.NTSForzaVisZoom = False
    Me.edAr_reparto.NTSOldValue = ""
    Me.edAr_reparto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_reparto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_reparto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_reparto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_reparto.Properties.AutoHeight = False
    Me.edAr_reparto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_reparto.Properties.MaxLength = 65536
    Me.edAr_reparto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_reparto.Size = New System.Drawing.Size(50, 20)
    Me.edAr_reparto.TabIndex = 670
    Me.edAr_reparto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_reparto.TextInt = 0
    '
    'lbAr_perqta
    '
    Me.lbAr_perqta.AutoSize = True
    Me.lbAr_perqta.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perqta.Location = New System.Drawing.Point(6, 24)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(125, 13)
    Me.lbAr_perqta.TabIndex = 667
    Me.lbAr_perqta.Text = "Moltiplicatore qtà/prezzo"
    Me.lbAr_perqta.Tooltip = ""
    Me.lbAr_perqta.UseMnemonic = False
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.EditValue = "0"
    Me.edAr_perqta.Location = New System.Drawing.Point(155, 21)
    Me.edAr_perqta.Name = "edAr_perqta"
    Me.edAr_perqta.NTSDbField = ""
    Me.edAr_perqta.NTSFormat = "0"
    Me.edAr_perqta.NTSForzaVisZoom = False
    Me.edAr_perqta.NTSOldValue = ""
    Me.edAr_perqta.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_perqta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_perqta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_perqta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_perqta.Properties.AutoHeight = False
    Me.edAr_perqta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_perqta.Properties.MaxLength = 65536
    Me.edAr_perqta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_perqta.Size = New System.Drawing.Size(89, 20)
    Me.edAr_perqta.TabIndex = 668
    Me.edAr_perqta.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_perqta.TextInt = 0
    '
    'fmAcquisti
    '
    Me.fmAcquisti.AllowDrop = True
    Me.fmAcquisti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAcquisti.Appearance.Options.UseBackColor = True
    Me.fmAcquisti.Controls.Add(Me.lbXx_forn)
    Me.fmAcquisti.Controls.Add(Me.lbXx_forn2)
    Me.fmAcquisti.Controls.Add(Me.lbAr_scomax)
    Me.fmAcquisti.Controls.Add(Me.lbAr_scomin)
    Me.fmAcquisti.Controls.Add(Me.edAr_scomin)
    Me.fmAcquisti.Controls.Add(Me.edAr_scomax)
    Me.fmAcquisti.Controls.Add(Me.edAr_forn)
    Me.fmAcquisti.Controls.Add(Me.lbAr_forn)
    Me.fmAcquisti.Controls.Add(Me.edAr_forn2)
    Me.fmAcquisti.Controls.Add(Me.lbAr_forn2)
    Me.fmAcquisti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAcquisti.Location = New System.Drawing.Point(3, 137)
    Me.fmAcquisti.Name = "fmAcquisti"
    Me.fmAcquisti.Size = New System.Drawing.Size(674, 88)
    Me.fmAcquisti.TabIndex = 669
    Me.fmAcquisti.Text = "ACQUISTI"
    '
    'lbXx_forn
    '
    Me.lbXx_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn.Location = New System.Drawing.Point(250, 21)
    Me.lbXx_forn.Name = "lbXx_forn"
    Me.lbXx_forn.NTSDbField = ""
    Me.lbXx_forn.Size = New System.Drawing.Size(407, 20)
    Me.lbXx_forn.TabIndex = 627
    Me.lbXx_forn.Tooltip = ""
    Me.lbXx_forn.UseMnemonic = False
    '
    'lbXx_forn2
    '
    Me.lbXx_forn2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn2.Location = New System.Drawing.Point(250, 43)
    Me.lbXx_forn2.Name = "lbXx_forn2"
    Me.lbXx_forn2.NTSDbField = ""
    Me.lbXx_forn2.Size = New System.Drawing.Size(407, 20)
    Me.lbXx_forn2.TabIndex = 628
    Me.lbXx_forn2.Tooltip = ""
    Me.lbXx_forn2.UseMnemonic = False
    '
    'lbAr_scomax
    '
    Me.lbAr_scomax.AutoSize = True
    Me.lbAr_scomax.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scomax.Location = New System.Drawing.Point(252, 69)
    Me.lbAr_scomax.Name = "lbAr_scomax"
    Me.lbAr_scomax.NTSDbField = ""
    Me.lbAr_scomax.Size = New System.Drawing.Size(11, 13)
    Me.lbAr_scomax.TabIndex = 618
    Me.lbAr_scomax.Text = "/"
    Me.lbAr_scomax.Tooltip = ""
    Me.lbAr_scomax.UseMnemonic = False
    '
    'lbAr_scomin
    '
    Me.lbAr_scomin.AutoSize = True
    Me.lbAr_scomin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scomin.Location = New System.Drawing.Point(6, 68)
    Me.lbAr_scomin.Name = "lbAr_scomin"
    Me.lbAr_scomin.NTSDbField = ""
    Me.lbAr_scomin.Size = New System.Drawing.Size(117, 13)
    Me.lbAr_scomin.TabIndex = 613
    Me.lbAr_scomin.Text = "Scorta minima/massima"
    Me.lbAr_scomin.Tooltip = ""
    Me.lbAr_scomin.UseMnemonic = False
    '
    'edAr_scomin
    '
    Me.edAr_scomin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scomin.EditValue = "0"
    Me.edAr_scomin.Location = New System.Drawing.Point(155, 65)
    Me.edAr_scomin.Name = "edAr_scomin"
    Me.edAr_scomin.NTSDbField = ""
    Me.edAr_scomin.NTSFormat = "0"
    Me.edAr_scomin.NTSForzaVisZoom = False
    Me.edAr_scomin.NTSOldValue = ""
    Me.edAr_scomin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scomin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scomin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scomin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scomin.Properties.AutoHeight = False
    Me.edAr_scomin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scomin.Properties.MaxLength = 65536
    Me.edAr_scomin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scomin.Size = New System.Drawing.Size(89, 20)
    Me.edAr_scomin.TabIndex = 615
    Me.edAr_scomin.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_scomin.TextInt = 0
    '
    'edAr_scomax
    '
    Me.edAr_scomax.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scomax.EditValue = "0"
    Me.edAr_scomax.Location = New System.Drawing.Point(267, 65)
    Me.edAr_scomax.Name = "edAr_scomax"
    Me.edAr_scomax.NTSDbField = ""
    Me.edAr_scomax.NTSFormat = "0"
    Me.edAr_scomax.NTSForzaVisZoom = False
    Me.edAr_scomax.NTSOldValue = ""
    Me.edAr_scomax.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scomax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scomax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scomax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scomax.Properties.AutoHeight = False
    Me.edAr_scomax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scomax.Properties.MaxLength = 65536
    Me.edAr_scomax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scomax.Size = New System.Drawing.Size(89, 20)
    Me.edAr_scomax.TabIndex = 616
    Me.edAr_scomax.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_scomax.TextInt = 0
    '
    'edAr_forn
    '
    Me.edAr_forn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_forn.EditValue = "0"
    Me.edAr_forn.Location = New System.Drawing.Point(155, 21)
    Me.edAr_forn.Name = "edAr_forn"
    Me.edAr_forn.NTSDbField = ""
    Me.edAr_forn.NTSFormat = "0"
    Me.edAr_forn.NTSForzaVisZoom = False
    Me.edAr_forn.NTSOldValue = ""
    Me.edAr_forn.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_forn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_forn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_forn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_forn.Properties.AutoHeight = False
    Me.edAr_forn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_forn.Properties.MaxLength = 65536
    Me.edAr_forn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_forn.Size = New System.Drawing.Size(89, 20)
    Me.edAr_forn.TabIndex = 602
    Me.edAr_forn.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_forn.TextInt = 0
    '
    'lbAr_forn
    '
    Me.lbAr_forn.AutoSize = True
    Me.lbAr_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_forn.Location = New System.Drawing.Point(6, 24)
    Me.lbAr_forn.Name = "lbAr_forn"
    Me.lbAr_forn.NTSDbField = ""
    Me.lbAr_forn.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_forn.TabIndex = 600
    Me.lbAr_forn.Text = "Fornitore 1"
    Me.lbAr_forn.Tooltip = ""
    Me.lbAr_forn.UseMnemonic = False
    '
    'edAr_forn2
    '
    Me.edAr_forn2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_forn2.EditValue = "0"
    Me.edAr_forn2.Location = New System.Drawing.Point(155, 43)
    Me.edAr_forn2.Name = "edAr_forn2"
    Me.edAr_forn2.NTSDbField = ""
    Me.edAr_forn2.NTSFormat = "0"
    Me.edAr_forn2.NTSForzaVisZoom = False
    Me.edAr_forn2.NTSOldValue = ""
    Me.edAr_forn2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_forn2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_forn2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_forn2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_forn2.Properties.AutoHeight = False
    Me.edAr_forn2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_forn2.Properties.MaxLength = 65536
    Me.edAr_forn2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_forn2.Size = New System.Drawing.Size(89, 20)
    Me.edAr_forn2.TabIndex = 603
    Me.edAr_forn2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_forn2.TextInt = 0
    '
    'lbAr_forn2
    '
    Me.lbAr_forn2.AutoSize = True
    Me.lbAr_forn2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_forn2.Location = New System.Drawing.Point(6, 46)
    Me.lbAr_forn2.Name = "lbAr_forn2"
    Me.lbAr_forn2.NTSDbField = ""
    Me.lbAr_forn2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_forn2.TabIndex = 601
    Me.lbAr_forn2.Text = "Fornitore 2"
    Me.lbAr_forn2.Tooltip = ""
    Me.lbAr_forn2.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnTabpag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage3.Text = "&2 - Altri dati"
    '
    'pnTabpag3
    '
    Me.pnTabpag3.AllowDrop = True
    Me.pnTabpag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3.Appearance.Options.UseBackColor = True
    Me.pnTabpag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3.Controls.Add(Me.pnTabpag3Right)
    Me.pnTabpag3.Controls.Add(Me.pnTabpag3Left)
    Me.pnTabpag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag3.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3.Name = "pnTabpag3"
    Me.pnTabpag3.NTSActiveTrasparency = True
    Me.pnTabpag3.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag3.TabIndex = 0
    Me.pnTabpag3.Text = "NtsPanel1"
    '
    'pnTabpag3Right
    '
    Me.pnTabpag3Right.AllowDrop = True
    Me.pnTabpag3Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Right.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3Right.Controls.Add(Me.imArtGif)
    Me.pnTabpag3Right.Controls.Add(Me.fmAltriDati)
    Me.pnTabpag3Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3Right.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag3Right.Location = New System.Drawing.Point(490, 0)
    Me.pnTabpag3Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3Right.Name = "pnTabpag3Right"
    Me.pnTabpag3Right.NTSActiveTrasparency = True
    Me.pnTabpag3Right.Size = New System.Drawing.Size(265, 320)
    Me.pnTabpag3Right.TabIndex = 670
    Me.pnTabpag3Right.Text = "NtsPanel1"
    '
    'imArtGif
    '
    Me.imArtGif.AllowDrop = True
    Me.imArtGif.Cursor = System.Windows.Forms.Cursors.Default
    Me.imArtGif.Location = New System.Drawing.Point(5, 120)
    Me.imArtGif.Name = "imArtGif"
    Me.imArtGif.NTSContextMenu = Nothing
    Me.imArtGif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.imArtGif.Properties.Appearance.Options.UseBackColor = True
    Me.imArtGif.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.imArtGif.Properties.ShowMenu = False
    Me.imArtGif.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
    Me.imArtGif.Size = New System.Drawing.Size(260, 194)
    Me.imArtGif.TabIndex = 657
    '
    'fmAltriDati
    '
    Me.fmAltriDati.AllowDrop = True
    Me.fmAltriDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAltriDati.Appearance.Options.UseBackColor = True
    Me.fmAltriDati.Controls.Add(Me.ckAr_stainv)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stasche)
    Me.fmAltriDati.Controls.Add(Me.ckAr_pesoca)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stalist)
    Me.fmAltriDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAltriDati.Location = New System.Drawing.Point(2, 6)
    Me.fmAltriDati.Name = "fmAltriDati"
    Me.fmAltriDati.Size = New System.Drawing.Size(260, 108)
    Me.fmAltriDati.TabIndex = 656
    Me.fmAltriDati.Text = "OPZIONI VARIE"
    '
    'ckAr_stainv
    '
    Me.ckAr_stainv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stainv.Location = New System.Drawing.Point(5, 62)
    Me.ckAr_stainv.Name = "ckAr_stainv"
    Me.ckAr_stainv.NTSCheckValue = "S"
    Me.ckAr_stainv.NTSUnCheckValue = "N"
    Me.ckAr_stainv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stainv.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stainv.Properties.AutoHeight = False
    Me.ckAr_stainv.Properties.Caption = "Stampa articolo nell'inventario"
    Me.ckAr_stainv.Size = New System.Drawing.Size(172, 19)
    Me.ckAr_stainv.TabIndex = 629
    '
    'ckAr_stasche
    '
    Me.ckAr_stasche.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stasche.Location = New System.Drawing.Point(5, 82)
    Me.ckAr_stasche.Name = "ckAr_stasche"
    Me.ckAr_stasche.NTSCheckValue = "S"
    Me.ckAr_stasche.NTSUnCheckValue = "N"
    Me.ckAr_stasche.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stasche.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stasche.Properties.AutoHeight = False
    Me.ckAr_stasche.Properties.Caption = "Stampa scheda articolo"
    Me.ckAr_stasche.Size = New System.Drawing.Size(137, 19)
    Me.ckAr_stasche.TabIndex = 630
    '
    'ckAr_pesoca
    '
    Me.ckAr_pesoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_pesoca.Location = New System.Drawing.Point(5, 22)
    Me.ckAr_pesoca.Name = "ckAr_pesoca"
    Me.ckAr_pesoca.NTSCheckValue = "S"
    Me.ckAr_pesoca.NTSUnCheckValue = "N"
    Me.ckAr_pesoca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_pesoca.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_pesoca.Properties.AutoHeight = False
    Me.ckAr_pesoca.Properties.Caption = "Non proporre note articolo su righe documenti"
    Me.ckAr_pesoca.Size = New System.Drawing.Size(251, 19)
    Me.ckAr_pesoca.TabIndex = 635
    '
    'ckAr_stalist
    '
    Me.ckAr_stalist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stalist.Location = New System.Drawing.Point(5, 42)
    Me.ckAr_stalist.Name = "ckAr_stalist"
    Me.ckAr_stalist.NTSCheckValue = "S"
    Me.ckAr_stalist.NTSUnCheckValue = "N"
    Me.ckAr_stalist.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stalist.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stalist.Properties.AutoHeight = False
    Me.ckAr_stalist.Properties.Caption = "Stampa articolo nel listino"
    Me.ckAr_stalist.Size = New System.Drawing.Size(147, 19)
    Me.ckAr_stalist.TabIndex = 637
    '
    'pnTabpag3Left
    '
    Me.pnTabpag3Left.AllowDrop = True
    Me.pnTabpag3Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Left.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3Left.Controls.Add(Me.cmdArtgif2)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_pesonet)
    Me.pnTabpag3Left.Controls.Add(Me.cmdArtGif1)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_pesolor)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_pesolor)
    Me.pnTabpag3Left.Controls.Add(Me.cmdVisGif2)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_pesonet)
    Me.pnTabpag3Left.Controls.Add(Me.cmdVisGif1)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_gif2)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_tipo)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_tipo)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_gif2)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_gif1)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_gif1)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_controp)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_controa)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_controp)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_controp)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_controa)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_controa)
    Me.pnTabpag3Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3Left.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTabpag3Left.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag3Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3Left.Name = "pnTabpag3Left"
    Me.pnTabpag3Left.NTSActiveTrasparency = True
    Me.pnTabpag3Left.Size = New System.Drawing.Size(490, 320)
    Me.pnTabpag3Left.TabIndex = 646
    Me.pnTabpag3Left.Text = "NtsPanel1"
    '
    'cmdArtgif2
    '
    Me.cmdArtgif2.ImageText = ""
    Me.cmdArtgif2.Location = New System.Drawing.Point(342, 270)
    Me.cmdArtgif2.Name = "cmdArtgif2"
    Me.cmdArtgif2.NTSContextMenu = Nothing
    Me.cmdArtgif2.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtgif2.TabIndex = 683
    Me.cmdArtgif2.Text = "..."
    '
    'lbAr_pesonet
    '
    Me.lbAr_pesonet.AutoSize = True
    Me.lbAr_pesonet.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesonet.Location = New System.Drawing.Point(9, 229)
    Me.lbAr_pesonet.Name = "lbAr_pesonet"
    Me.lbAr_pesonet.NTSDbField = ""
    Me.lbAr_pesonet.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_pesonet.TabIndex = 649
    Me.lbAr_pesonet.Text = "Peso netto"
    Me.lbAr_pesonet.Tooltip = ""
    Me.lbAr_pesonet.UseMnemonic = False
    '
    'cmdArtGif1
    '
    Me.cmdArtGif1.ImageText = ""
    Me.cmdArtGif1.Location = New System.Drawing.Point(342, 248)
    Me.cmdArtGif1.Name = "cmdArtGif1"
    Me.cmdArtGif1.NTSContextMenu = Nothing
    Me.cmdArtGif1.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtGif1.TabIndex = 682
    Me.cmdArtGif1.Text = "..."
    '
    'edAr_pesolor
    '
    Me.edAr_pesolor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_pesolor.EditValue = "0"
    Me.edAr_pesolor.Location = New System.Drawing.Point(130, 204)
    Me.edAr_pesolor.Name = "edAr_pesolor"
    Me.edAr_pesolor.NTSDbField = ""
    Me.edAr_pesolor.NTSFormat = "0"
    Me.edAr_pesolor.NTSForzaVisZoom = False
    Me.edAr_pesolor.NTSOldValue = ""
    Me.edAr_pesolor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_pesolor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_pesolor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_pesolor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_pesolor.Properties.AutoHeight = False
    Me.edAr_pesolor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_pesolor.Properties.MaxLength = 65536
    Me.edAr_pesolor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_pesolor.Size = New System.Drawing.Size(85, 20)
    Me.edAr_pesolor.TabIndex = 650
    Me.edAr_pesolor.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_pesolor.TextInt = 0
    '
    'lbAr_pesolor
    '
    Me.lbAr_pesolor.AutoSize = True
    Me.lbAr_pesolor.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesolor.Location = New System.Drawing.Point(9, 207)
    Me.lbAr_pesolor.Name = "lbAr_pesolor"
    Me.lbAr_pesolor.NTSDbField = ""
    Me.lbAr_pesolor.Size = New System.Drawing.Size(57, 13)
    Me.lbAr_pesolor.TabIndex = 648
    Me.lbAr_pesolor.Text = "Peso lordo"
    Me.lbAr_pesolor.Tooltip = ""
    Me.lbAr_pesolor.UseMnemonic = False
    '
    'cmdVisGif2
    '
    Me.cmdVisGif2.ImageText = ""
    Me.cmdVisGif2.Location = New System.Drawing.Point(373, 270)
    Me.cmdVisGif2.Name = "cmdVisGif2"
    Me.cmdVisGif2.NTSContextMenu = Nothing
    Me.cmdVisGif2.Size = New System.Drawing.Size(103, 20)
    Me.cmdVisGif2.TabIndex = 681
    Me.cmdVisGif2.Text = "Immagine scheda"
    '
    'edAr_pesonet
    '
    Me.edAr_pesonet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_pesonet.EditValue = "0"
    Me.edAr_pesonet.Location = New System.Drawing.Point(130, 226)
    Me.edAr_pesonet.Name = "edAr_pesonet"
    Me.edAr_pesonet.NTSDbField = ""
    Me.edAr_pesonet.NTSFormat = "0"
    Me.edAr_pesonet.NTSForzaVisZoom = False
    Me.edAr_pesonet.NTSOldValue = ""
    Me.edAr_pesonet.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_pesonet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_pesonet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_pesonet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_pesonet.Properties.AutoHeight = False
    Me.edAr_pesonet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_pesonet.Properties.MaxLength = 65536
    Me.edAr_pesonet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_pesonet.Size = New System.Drawing.Size(85, 20)
    Me.edAr_pesonet.TabIndex = 651
    Me.edAr_pesonet.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_pesonet.TextInt = 0
    '
    'cmdVisGif1
    '
    Me.cmdVisGif1.ImageText = ""
    Me.cmdVisGif1.Location = New System.Drawing.Point(373, 248)
    Me.cmdVisGif1.Name = "cmdVisGif1"
    Me.cmdVisGif1.NTSContextMenu = Nothing
    Me.cmdVisGif1.Size = New System.Drawing.Size(103, 20)
    Me.cmdVisGif1.TabIndex = 680
    Me.cmdVisGif1.Text = "Immagine catalogo"
    '
    'edAr_gif2
    '
    Me.edAr_gif2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif2.Location = New System.Drawing.Point(130, 270)
    Me.edAr_gif2.Name = "edAr_gif2"
    Me.edAr_gif2.NTSDbField = ""
    Me.edAr_gif2.NTSForzaVisZoom = False
    Me.edAr_gif2.NTSOldValue = ""
    Me.edAr_gif2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gif2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gif2.Properties.AutoHeight = False
    Me.edAr_gif2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gif2.Properties.MaxLength = 65536
    Me.edAr_gif2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gif2.Size = New System.Drawing.Size(206, 20)
    Me.edAr_gif2.TabIndex = 679
    Me.edAr_gif2.TextStr = ""
    '
    'edAr_tipo
    '
    Me.edAr_tipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_tipo.Location = New System.Drawing.Point(130, 182)
    Me.edAr_tipo.Name = "edAr_tipo"
    Me.edAr_tipo.NTSDbField = ""
    Me.edAr_tipo.NTSForzaVisZoom = False
    Me.edAr_tipo.NTSOldValue = ""
    Me.edAr_tipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_tipo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_tipo.Properties.AutoHeight = False
    Me.edAr_tipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_tipo.Properties.MaxLength = 65536
    Me.edAr_tipo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_tipo.Size = New System.Drawing.Size(64, 20)
    Me.edAr_tipo.TabIndex = 647
    Me.edAr_tipo.TextStr = ""
    '
    'lbAr_tipo
    '
    Me.lbAr_tipo.AutoSize = True
    Me.lbAr_tipo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipo.Location = New System.Drawing.Point(9, 185)
    Me.lbAr_tipo.Name = "lbAr_tipo"
    Me.lbAr_tipo.NTSDbField = ""
    Me.lbAr_tipo.Size = New System.Drawing.Size(27, 13)
    Me.lbAr_tipo.TabIndex = 645
    Me.lbAr_tipo.Text = "Tipo"
    Me.lbAr_tipo.Tooltip = ""
    Me.lbAr_tipo.UseMnemonic = False
    '
    'lbAr_gif2
    '
    Me.lbAr_gif2.AutoSize = True
    Me.lbAr_gif2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif2.Location = New System.Drawing.Point(9, 273)
    Me.lbAr_gif2.Name = "lbAr_gif2"
    Me.lbAr_gif2.NTSDbField = ""
    Me.lbAr_gif2.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_gif2.TabIndex = 677
    Me.lbAr_gif2.Text = "Immagine scheda"
    Me.lbAr_gif2.Tooltip = ""
    Me.lbAr_gif2.UseMnemonic = False
    '
    'edAr_gif1
    '
    Me.edAr_gif1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif1.Location = New System.Drawing.Point(130, 248)
    Me.edAr_gif1.Name = "edAr_gif1"
    Me.edAr_gif1.NTSDbField = ""
    Me.edAr_gif1.NTSForzaVisZoom = False
    Me.edAr_gif1.NTSOldValue = ""
    Me.edAr_gif1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gif1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gif1.Properties.AutoHeight = False
    Me.edAr_gif1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gif1.Properties.MaxLength = 65536
    Me.edAr_gif1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gif1.Size = New System.Drawing.Size(206, 20)
    Me.edAr_gif1.TabIndex = 678
    Me.edAr_gif1.TextStr = ""
    '
    'lbAr_gif1
    '
    Me.lbAr_gif1.AutoSize = True
    Me.lbAr_gif1.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif1.Location = New System.Drawing.Point(9, 251)
    Me.lbAr_gif1.Name = "lbAr_gif1"
    Me.lbAr_gif1.NTSDbField = ""
    Me.lbAr_gif1.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_gif1.TabIndex = 676
    Me.lbAr_gif1.Text = "Immagine catalogo"
    Me.lbAr_gif1.Tooltip = ""
    Me.lbAr_gif1.UseMnemonic = False
    '
    'lbXx_clascon
    '
    Me.lbXx_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_clascon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_clascon.Location = New System.Drawing.Point(200, 138)
    Me.lbXx_clascon.Name = "lbXx_clascon"
    Me.lbXx_clascon.NTSDbField = ""
    Me.lbXx_clascon.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_clascon.TabIndex = 674
    Me.lbXx_clascon.Tooltip = ""
    Me.lbXx_clascon.UseMnemonic = False
    '
    'lbXx_claprov
    '
    Me.lbXx_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_claprov.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_claprov.Location = New System.Drawing.Point(200, 160)
    Me.lbXx_claprov.Name = "lbXx_claprov"
    Me.lbXx_claprov.NTSDbField = ""
    Me.lbXx_claprov.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_claprov.TabIndex = 675
    Me.lbXx_claprov.Tooltip = ""
    Me.lbXx_claprov.UseMnemonic = False
    '
    'lbAr_claprov
    '
    Me.lbAr_claprov.AutoSize = True
    Me.lbAr_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_claprov.Location = New System.Drawing.Point(9, 164)
    Me.lbAr_claprov.Name = "lbAr_claprov"
    Me.lbAr_claprov.NTSDbField = ""
    Me.lbAr_claprov.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_claprov.TabIndex = 672
    Me.lbAr_claprov.Text = "Classe provvigione"
    Me.lbAr_claprov.Tooltip = ""
    Me.lbAr_claprov.UseMnemonic = False
    '
    'edAr_claprov
    '
    Me.edAr_claprov.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_claprov.EditValue = "0"
    Me.edAr_claprov.Location = New System.Drawing.Point(130, 160)
    Me.edAr_claprov.Name = "edAr_claprov"
    Me.edAr_claprov.NTSDbField = ""
    Me.edAr_claprov.NTSFormat = "0"
    Me.edAr_claprov.NTSForzaVisZoom = False
    Me.edAr_claprov.NTSOldValue = ""
    Me.edAr_claprov.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_claprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_claprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_claprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_claprov.Properties.AutoHeight = False
    Me.edAr_claprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_claprov.Properties.MaxLength = 65536
    Me.edAr_claprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_claprov.Size = New System.Drawing.Size(64, 20)
    Me.edAr_claprov.TabIndex = 673
    Me.edAr_claprov.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_claprov.TextInt = 0
    '
    'lbAr_clascon
    '
    Me.lbAr_clascon.AutoSize = True
    Me.lbAr_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_clascon.Location = New System.Drawing.Point(9, 141)
    Me.lbAr_clascon.Name = "lbAr_clascon"
    Me.lbAr_clascon.NTSDbField = ""
    Me.lbAr_clascon.Size = New System.Drawing.Size(84, 13)
    Me.lbAr_clascon.TabIndex = 670
    Me.lbAr_clascon.Text = "Classe di sconto"
    Me.lbAr_clascon.Tooltip = ""
    Me.lbAr_clascon.UseMnemonic = False
    '
    'edAr_clascon
    '
    Me.edAr_clascon.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_clascon.EditValue = "0"
    Me.edAr_clascon.Location = New System.Drawing.Point(130, 138)
    Me.edAr_clascon.Name = "edAr_clascon"
    Me.edAr_clascon.NTSDbField = ""
    Me.edAr_clascon.NTSFormat = "0"
    Me.edAr_clascon.NTSForzaVisZoom = False
    Me.edAr_clascon.NTSOldValue = ""
    Me.edAr_clascon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_clascon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_clascon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_clascon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_clascon.Properties.AutoHeight = False
    Me.edAr_clascon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_clascon.Properties.MaxLength = 65536
    Me.edAr_clascon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_clascon.Size = New System.Drawing.Size(64, 20)
    Me.edAr_clascon.TabIndex = 671
    Me.edAr_clascon.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_clascon.TextInt = 0
    '
    'lbXx_codiva
    '
    Me.lbXx_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codiva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codiva.Location = New System.Drawing.Point(200, 6)
    Me.lbXx_codiva.Name = "lbXx_codiva"
    Me.lbXx_codiva.NTSDbField = ""
    Me.lbXx_codiva.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_codiva.TabIndex = 663
    Me.lbXx_codiva.Tooltip = ""
    Me.lbXx_codiva.UseMnemonic = False
    '
    'lbXx_gruppo
    '
    Me.lbXx_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_gruppo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_gruppo.Location = New System.Drawing.Point(200, 28)
    Me.lbXx_gruppo.Name = "lbXx_gruppo"
    Me.lbXx_gruppo.NTSDbField = ""
    Me.lbXx_gruppo.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_gruppo.TabIndex = 664
    Me.lbXx_gruppo.Tooltip = ""
    Me.lbXx_gruppo.UseMnemonic = False
    '
    'lbXx_sotgru
    '
    Me.lbXx_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_sotgru.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_sotgru.Location = New System.Drawing.Point(200, 50)
    Me.lbXx_sotgru.Name = "lbXx_sotgru"
    Me.lbXx_sotgru.NTSDbField = ""
    Me.lbXx_sotgru.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_sotgru.TabIndex = 665
    Me.lbXx_sotgru.Tooltip = ""
    Me.lbXx_sotgru.UseMnemonic = False
    '
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(200, 72)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_controp.TabIndex = 666
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbXx_controa
    '
    Me.lbXx_controa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controa.Location = New System.Drawing.Point(200, 94)
    Me.lbXx_controa.Name = "lbXx_controa"
    Me.lbXx_controa.NTSDbField = ""
    Me.lbXx_controa.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_controa.TabIndex = 667
    Me.lbXx_controa.Tooltip = ""
    Me.lbXx_controa.UseMnemonic = False
    '
    'lbXx_famprod
    '
    Me.lbXx_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_famprod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_famprod.Location = New System.Drawing.Point(200, 116)
    Me.lbXx_famprod.Name = "lbXx_famprod"
    Me.lbXx_famprod.NTSDbField = ""
    Me.lbXx_famprod.Size = New System.Drawing.Size(276, 20)
    Me.lbXx_famprod.TabIndex = 669
    Me.lbXx_famprod.Tooltip = ""
    Me.lbXx_famprod.UseMnemonic = False
    '
    'lbAr_famprod
    '
    Me.lbAr_famprod.AutoSize = True
    Me.lbAr_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_famprod.Location = New System.Drawing.Point(9, 119)
    Me.lbAr_famprod.Name = "lbAr_famprod"
    Me.lbAr_famprod.NTSDbField = ""
    Me.lbAr_famprod.Size = New System.Drawing.Size(45, 13)
    Me.lbAr_famprod.TabIndex = 661
    Me.lbAr_famprod.Text = "Famiglia"
    Me.lbAr_famprod.Tooltip = ""
    Me.lbAr_famprod.UseMnemonic = False
    '
    'edAr_famprod
    '
    Me.edAr_famprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_famprod.Location = New System.Drawing.Point(130, 116)
    Me.edAr_famprod.Name = "edAr_famprod"
    Me.edAr_famprod.NTSDbField = ""
    Me.edAr_famprod.NTSForzaVisZoom = False
    Me.edAr_famprod.NTSOldValue = ""
    Me.edAr_famprod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_famprod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_famprod.Properties.AutoHeight = False
    Me.edAr_famprod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_famprod.Properties.MaxLength = 65536
    Me.edAr_famprod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_famprod.Size = New System.Drawing.Size(64, 20)
    Me.edAr_famprod.TabIndex = 662
    Me.edAr_famprod.TextStr = ""
    '
    'lbAr_codiva
    '
    Me.lbAr_codiva.AutoSize = True
    Me.lbAr_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codiva.Location = New System.Drawing.Point(9, 9)
    Me.lbAr_codiva.Name = "lbAr_codiva"
    Me.lbAr_codiva.NTSDbField = ""
    Me.lbAr_codiva.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_codiva.TabIndex = 649
    Me.lbAr_codiva.Text = "Codice IVA"
    Me.lbAr_codiva.Tooltip = ""
    Me.lbAr_codiva.UseMnemonic = False
    '
    'edAr_codiva
    '
    Me.edAr_codiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codiva.EditValue = "0"
    Me.edAr_codiva.Location = New System.Drawing.Point(130, 6)
    Me.edAr_codiva.Name = "edAr_codiva"
    Me.edAr_codiva.NTSDbField = ""
    Me.edAr_codiva.NTSFormat = "0"
    Me.edAr_codiva.NTSForzaVisZoom = False
    Me.edAr_codiva.NTSOldValue = ""
    Me.edAr_codiva.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codiva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codiva.Properties.AutoHeight = False
    Me.edAr_codiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codiva.Properties.MaxLength = 65536
    Me.edAr_codiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codiva.Size = New System.Drawing.Size(64, 20)
    Me.edAr_codiva.TabIndex = 654
    Me.edAr_codiva.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_codiva.TextInt = 0
    '
    'lbAr_gruppo
    '
    Me.lbAr_gruppo.AutoSize = True
    Me.lbAr_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gruppo.Location = New System.Drawing.Point(9, 31)
    Me.lbAr_gruppo.Name = "lbAr_gruppo"
    Me.lbAr_gruppo.NTSDbField = ""
    Me.lbAr_gruppo.Size = New System.Drawing.Size(107, 13)
    Me.lbAr_gruppo.TabIndex = 650
    Me.lbAr_gruppo.Text = "Gruppo merceologico"
    Me.lbAr_gruppo.Tooltip = ""
    Me.lbAr_gruppo.UseMnemonic = False
    '
    'edAr_gruppo
    '
    Me.edAr_gruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gruppo.EditValue = "0"
    Me.edAr_gruppo.Location = New System.Drawing.Point(130, 28)
    Me.edAr_gruppo.Name = "edAr_gruppo"
    Me.edAr_gruppo.NTSDbField = ""
    Me.edAr_gruppo.NTSFormat = "0"
    Me.edAr_gruppo.NTSForzaVisZoom = False
    Me.edAr_gruppo.NTSOldValue = ""
    Me.edAr_gruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_gruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_gruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gruppo.Properties.AutoHeight = False
    Me.edAr_gruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gruppo.Properties.MaxLength = 65536
    Me.edAr_gruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gruppo.Size = New System.Drawing.Size(64, 20)
    Me.edAr_gruppo.TabIndex = 655
    Me.edAr_gruppo.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_gruppo.TextInt = 0
    '
    'lbAr_sotgru
    '
    Me.lbAr_sotgru.AutoSize = True
    Me.lbAr_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sotgru.Location = New System.Drawing.Point(9, 53)
    Me.lbAr_sotgru.Name = "lbAr_sotgru"
    Me.lbAr_sotgru.NTSDbField = ""
    Me.lbAr_sotgru.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_sotgru.TabIndex = 651
    Me.lbAr_sotgru.Text = "Sottogruppo merceol."
    Me.lbAr_sotgru.Tooltip = ""
    Me.lbAr_sotgru.UseMnemonic = False
    '
    'edAr_sotgru
    '
    Me.edAr_sotgru.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sotgru.EditValue = "0"
    Me.edAr_sotgru.Location = New System.Drawing.Point(130, 50)
    Me.edAr_sotgru.Name = "edAr_sotgru"
    Me.edAr_sotgru.NTSDbField = ""
    Me.edAr_sotgru.NTSFormat = "0"
    Me.edAr_sotgru.NTSForzaVisZoom = False
    Me.edAr_sotgru.NTSOldValue = ""
    Me.edAr_sotgru.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_sotgru.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_sotgru.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_sotgru.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_sotgru.Properties.AutoHeight = False
    Me.edAr_sotgru.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_sotgru.Properties.MaxLength = 65536
    Me.edAr_sotgru.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_sotgru.Size = New System.Drawing.Size(64, 20)
    Me.edAr_sotgru.TabIndex = 656
    Me.edAr_sotgru.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_sotgru.TextInt = 0
    '
    'lbAr_controp
    '
    Me.lbAr_controp.AutoSize = True
    Me.lbAr_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_controp.Location = New System.Drawing.Point(9, 75)
    Me.lbAr_controp.Name = "lbAr_controp"
    Me.lbAr_controp.NTSDbField = ""
    Me.lbAr_controp.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_controp.TabIndex = 652
    Me.lbAr_controp.Text = "Contropartita vendite"
    Me.lbAr_controp.Tooltip = ""
    Me.lbAr_controp.UseMnemonic = False
    '
    'edAr_controp
    '
    Me.edAr_controp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_controp.EditValue = "0"
    Me.edAr_controp.Location = New System.Drawing.Point(130, 72)
    Me.edAr_controp.Name = "edAr_controp"
    Me.edAr_controp.NTSDbField = ""
    Me.edAr_controp.NTSFormat = "0"
    Me.edAr_controp.NTSForzaVisZoom = False
    Me.edAr_controp.NTSOldValue = ""
    Me.edAr_controp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_controp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_controp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_controp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_controp.Properties.AutoHeight = False
    Me.edAr_controp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_controp.Properties.MaxLength = 65536
    Me.edAr_controp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_controp.Size = New System.Drawing.Size(64, 20)
    Me.edAr_controp.TabIndex = 657
    Me.edAr_controp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_controp.TextInt = 0
    '
    'lbAr_controa
    '
    Me.lbAr_controa.AutoSize = True
    Me.lbAr_controa.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_controa.Location = New System.Drawing.Point(9, 97)
    Me.lbAr_controa.Name = "lbAr_controa"
    Me.lbAr_controa.NTSDbField = ""
    Me.lbAr_controa.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_controa.TabIndex = 653
    Me.lbAr_controa.Text = "Contropartita acquisti"
    Me.lbAr_controa.Tooltip = ""
    Me.lbAr_controa.UseMnemonic = False
    '
    'edAr_controa
    '
    Me.edAr_controa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_controa.EditValue = "0"
    Me.edAr_controa.Location = New System.Drawing.Point(130, 94)
    Me.edAr_controa.Name = "edAr_controa"
    Me.edAr_controa.NTSDbField = ""
    Me.edAr_controa.NTSFormat = "0"
    Me.edAr_controa.NTSForzaVisZoom = False
    Me.edAr_controa.NTSOldValue = ""
    Me.edAr_controa.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_controa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_controa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_controa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_controa.Properties.AutoHeight = False
    Me.edAr_controa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_controa.Properties.MaxLength = 65536
    Me.edAr_controa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_controa.Size = New System.Drawing.Size(64, 20)
    Me.edAr_controa.TabIndex = 658
    Me.edAr_controa.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_controa.TextInt = 0
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnTabpag4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage4.Text = "&3 - Listini"
    '
    'pnTabpag4
    '
    Me.pnTabpag4.AllowDrop = True
    Me.pnTabpag4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag4.Appearance.Options.UseBackColor = True
    Me.pnTabpag4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag4.Controls.Add(Me.ceListini)
    Me.pnTabpag4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag4.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag4.Name = "pnTabpag4"
    Me.pnTabpag4.NTSActiveTrasparency = True
    Me.pnTabpag4.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag4.TabIndex = 0
    Me.pnTabpag4.Text = "NtsPanel1"
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
    Me.ceListini.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceListini.Name = "ceListini"
    Me.ceListini.Size = New System.Drawing.Size(755, 320)
    Me.ceListini.strNomeCampo = ""
    Me.ceListini.TabIndex = 0
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnTabpag5)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage5.Text = "&4 - Sconti"
    '
    'pnTabpag5
    '
    Me.pnTabpag5.AllowDrop = True
    Me.pnTabpag5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag5.Appearance.Options.UseBackColor = True
    Me.pnTabpag5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag5.Controls.Add(Me.ceSconti)
    Me.pnTabpag5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag5.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag5.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag5.Name = "pnTabpag5"
    Me.pnTabpag5.NTSActiveTrasparency = True
    Me.pnTabpag5.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag5.TabIndex = 0
    Me.pnTabpag5.Text = "NtsPanel1"
    '
    'ceSconti
    '
    Me.ceSconti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceSconti.GridColumn1_954_20 = Nothing
    Me.ceSconti.Location = New System.Drawing.Point(0, 0)
    Me.ceSconti.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceSconti.Name = "ceSconti"
    Me.ceSconti.Size = New System.Drawing.Size(755, 320)
    Me.ceSconti.SoClasseArt = 0
    Me.ceSconti.SoClasseCli = 0
    Me.ceSconti.SoCodart = ""
    Me.ceSconti.SoCodartRoot = ""
    Me.ceSconti.SoConto = 0
    Me.ceSconti.strNomeCampo = ""
    Me.ceSconti.TabIndex = 0
    Me.ceSconti.TipoSconto = 0
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnTabpag6)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage6.Text = "&5 - Provvigioni"
    '
    'pnTabpag6
    '
    Me.pnTabpag6.AllowDrop = True
    Me.pnTabpag6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag6.Appearance.Options.UseBackColor = True
    Me.pnTabpag6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag6.Controls.Add(Me.ceProvvig)
    Me.pnTabpag6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag6.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag6.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag6.Name = "pnTabpag6"
    Me.pnTabpag6.NTSActiveTrasparency = True
    Me.pnTabpag6.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag6.TabIndex = 0
    Me.pnTabpag6.Text = "NtsPanel1"
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
    Me.ceProvvig.Size = New System.Drawing.Size(755, 320)
    Me.ceProvvig.strNomeCampo = ""
    Me.ceProvvig.TabIndex = 0
    Me.ceProvvig.TipoProvv = 0
    '
    'NtsTabPage7
    '
    Me.NtsTabPage7.AllowDrop = True
    Me.NtsTabPage7.Controls.Add(Me.pnTabpag7)
    Me.NtsTabPage7.Enable = True
    Me.NtsTabPage7.Name = "NtsTabPage7"
    Me.NtsTabPage7.Size = New System.Drawing.Size(755, 320)
    Me.NtsTabPage7.Text = "&6 - Note"
    '
    'pnTabpag7
    '
    Me.pnTabpag7.AllowDrop = True
    Me.pnTabpag7.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag7.Appearance.Options.UseBackColor = True
    Me.pnTabpag7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag7.Controls.Add(Me.edAr_note)
    Me.pnTabpag7.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag7.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag7.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag7.Name = "pnTabpag7"
    Me.pnTabpag7.NTSActiveTrasparency = True
    Me.pnTabpag7.Size = New System.Drawing.Size(755, 320)
    Me.pnTabpag7.TabIndex = 0
    Me.pnTabpag7.Text = "NtsPanel1"
    '
    'edAr_note
    '
    Me.edAr_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edAr_note.Location = New System.Drawing.Point(0, 0)
    Me.edAr_note.Name = "edAr_note"
    Me.edAr_note.NTSDbField = ""
    Me.edAr_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_note.Size = New System.Drawing.Size(755, 320)
    Me.edAr_note.TabIndex = 552
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.fmUnitamisura)
    Me.pnTop.Controls.Add(Me.pnTopLeftBut)
    Me.pnTop.Controls.Add(Me.pnTopLeft)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(764, 99)
    Me.pnTop.TabIndex = 575
    Me.pnTop.Text = "NtsPanel1"
    '
    'fmUnitamisura
    '
    Me.fmUnitamisura.AllowDrop = True
    Me.fmUnitamisura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUnitamisura.Appearance.Options.UseBackColor = True
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_conver)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_conver)
    Me.fmUnitamisura.Controls.Add(Me.edAr_confez2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_qtacon2)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_confez2)
    Me.fmUnitamisura.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUnitamisura.Location = New System.Drawing.Point(405, 4)
    Me.fmUnitamisura.Name = "fmUnitamisura"
    Me.fmUnitamisura.Size = New System.Drawing.Size(208, 90)
    Me.fmUnitamisura.TabIndex = 588
    Me.fmUnitamisura.Text = "UNITA' DI MISURA"
    '
    'edAr_unmis
    '
    Me.edAr_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis.Location = New System.Drawing.Point(81, 22)
    Me.edAr_unmis.Name = "edAr_unmis"
    Me.edAr_unmis.NTSDbField = ""
    Me.edAr_unmis.NTSForzaVisZoom = False
    Me.edAr_unmis.NTSOldValue = ""
    Me.edAr_unmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis.Properties.AutoHeight = False
    Me.edAr_unmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis.Properties.MaxLength = 65536
    Me.edAr_unmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis.Size = New System.Drawing.Size(48, 20)
    Me.edAr_unmis.TabIndex = 505
    Me.edAr_unmis.TextStr = ""
    '
    'lbAr_unmis
    '
    Me.lbAr_unmis.AutoSize = True
    Me.lbAr_unmis.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis.Location = New System.Drawing.Point(11, 25)
    Me.lbAr_unmis.Name = "lbAr_unmis"
    Me.lbAr_unmis.NTSDbField = ""
    Me.lbAr_unmis.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_unmis.TabIndex = 15
    Me.lbAr_unmis.Text = "Principale"
    Me.lbAr_unmis.Tooltip = ""
    Me.lbAr_unmis.UseMnemonic = False
    '
    'lbAr_conver
    '
    Me.lbAr_conver.AutoSize = True
    Me.lbAr_conver.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_conver.Location = New System.Drawing.Point(140, 25)
    Me.lbAr_conver.Name = "lbAr_conver"
    Me.lbAr_conver.NTSDbField = ""
    Me.lbAr_conver.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_conver.TabIndex = 17
    Me.lbAr_conver.Text = "QUANTITA'"
    Me.lbAr_conver.Tooltip = ""
    Me.lbAr_conver.UseMnemonic = False
    '
    'lbAr_unmis2
    '
    Me.lbAr_unmis2.AutoSize = True
    Me.lbAr_unmis2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis2.Location = New System.Drawing.Point(12, 69)
    Me.lbAr_unmis2.Name = "lbAr_unmis2"
    Me.lbAr_unmis2.NTSDbField = ""
    Me.lbAr_unmis2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_unmis2.TabIndex = 16
    Me.lbAr_unmis2.Text = "Secondaria"
    Me.lbAr_unmis2.Tooltip = ""
    Me.lbAr_unmis2.UseMnemonic = False
    '
    'edAr_conver
    '
    Me.edAr_conver.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_conver.EditValue = "0"
    Me.edAr_conver.Location = New System.Drawing.Point(135, 66)
    Me.edAr_conver.Name = "edAr_conver"
    Me.edAr_conver.NTSDbField = ""
    Me.edAr_conver.NTSFormat = "0"
    Me.edAr_conver.NTSForzaVisZoom = False
    Me.edAr_conver.NTSOldValue = ""
    Me.edAr_conver.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_conver.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_conver.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_conver.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_conver.Properties.AutoHeight = False
    Me.edAr_conver.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_conver.Properties.MaxLength = 65536
    Me.edAr_conver.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_conver.Size = New System.Drawing.Size(70, 20)
    Me.edAr_conver.TabIndex = 507
    Me.edAr_conver.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_conver.TextInt = 0
    '
    'edAr_confez2
    '
    Me.edAr_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_confez2.Location = New System.Drawing.Point(81, 44)
    Me.edAr_confez2.Name = "edAr_confez2"
    Me.edAr_confez2.NTSDbField = ""
    Me.edAr_confez2.NTSForzaVisZoom = False
    Me.edAr_confez2.NTSOldValue = ""
    Me.edAr_confez2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_confez2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_confez2.Properties.AutoHeight = False
    Me.edAr_confez2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_confez2.Properties.MaxLength = 65536
    Me.edAr_confez2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_confez2.Size = New System.Drawing.Size(48, 20)
    Me.edAr_confez2.TabIndex = 508
    Me.edAr_confez2.TextStr = ""
    '
    'edAr_unmis2
    '
    Me.edAr_unmis2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis2.Location = New System.Drawing.Point(81, 66)
    Me.edAr_unmis2.Name = "edAr_unmis2"
    Me.edAr_unmis2.NTSDbField = ""
    Me.edAr_unmis2.NTSForzaVisZoom = False
    Me.edAr_unmis2.NTSOldValue = ""
    Me.edAr_unmis2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis2.Properties.AutoHeight = False
    Me.edAr_unmis2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis2.Properties.MaxLength = 65536
    Me.edAr_unmis2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis2.Size = New System.Drawing.Size(48, 20)
    Me.edAr_unmis2.TabIndex = 506
    Me.edAr_unmis2.TextStr = ""
    '
    'edAr_qtacon2
    '
    Me.edAr_qtacon2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_qtacon2.EditValue = "0"
    Me.edAr_qtacon2.Location = New System.Drawing.Point(135, 44)
    Me.edAr_qtacon2.Name = "edAr_qtacon2"
    Me.edAr_qtacon2.NTSDbField = ""
    Me.edAr_qtacon2.NTSFormat = "0"
    Me.edAr_qtacon2.NTSForzaVisZoom = False
    Me.edAr_qtacon2.NTSOldValue = ""
    Me.edAr_qtacon2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_qtacon2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_qtacon2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_qtacon2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_qtacon2.Properties.AutoHeight = False
    Me.edAr_qtacon2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_qtacon2.Properties.MaxLength = 65536
    Me.edAr_qtacon2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_qtacon2.Size = New System.Drawing.Size(70, 20)
    Me.edAr_qtacon2.TabIndex = 509
    Me.edAr_qtacon2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAr_qtacon2.TextInt = 0
    '
    'lbAr_confez2
    '
    Me.lbAr_confez2.AutoSize = True
    Me.lbAr_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_confez2.Location = New System.Drawing.Point(11, 47)
    Me.lbAr_confez2.Name = "lbAr_confez2"
    Me.lbAr_confez2.NTSDbField = ""
    Me.lbAr_confez2.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_confez2.TabIndex = 18
    Me.lbAr_confez2.Text = "Confezione"
    Me.lbAr_confez2.Tooltip = ""
    Me.lbAr_confez2.UseMnemonic = False
    '
    'pnTopLeftBut
    '
    Me.pnTopLeftBut.AllowDrop = True
    Me.pnTopLeftBut.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTopLeftBut.Appearance.Options.UseBackColor = True
    Me.pnTopLeftBut.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTopLeftBut.Controls.Add(Me.cmdCodarfo)
    Me.pnTopLeftBut.Controls.Add(Me.cmdProgressivi)
    Me.pnTopLeftBut.Controls.Add(Me.cmdProgtot)
    Me.pnTopLeftBut.Controls.Add(Me.cmdValuta)
    Me.pnTopLeftBut.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTopLeftBut.Location = New System.Drawing.Point(619, 5)
    Me.pnTopLeftBut.Name = "pnTopLeftBut"
    Me.pnTopLeftBut.NTSActiveTrasparency = True
    Me.pnTopLeftBut.Size = New System.Drawing.Size(139, 89)
    Me.pnTopLeftBut.TabIndex = 510
    Me.pnTopLeftBut.Text = "NtsPanel1"
    '
    'cmdCodarfo
    '
    Me.cmdCodarfo.ImageText = ""
    Me.cmdCodarfo.Location = New System.Drawing.Point(3, 66)
    Me.cmdCodarfo.Name = "cmdCodarfo"
    Me.cmdCodarfo.NTSContextMenu = Nothing
    Me.cmdCodarfo.Size = New System.Drawing.Size(133, 23)
    Me.cmdCodarfo.TabIndex = 602
    Me.cmdCodarfo.Text = "&Codice articolo C/F"
    '
    'cmdProgressivi
    '
    Me.cmdProgressivi.ImageText = ""
    Me.cmdProgressivi.Location = New System.Drawing.Point(3, 22)
    Me.cmdProgressivi.Name = "cmdProgressivi"
    Me.cmdProgressivi.NTSContextMenu = Nothing
    Me.cmdProgressivi.Size = New System.Drawing.Size(133, 23)
    Me.cmdProgressivi.TabIndex = 601
    Me.cmdProgressivi.Text = "Progressivi"
    '
    'cmdProgtot
    '
    Me.cmdProgtot.ImageText = ""
    Me.cmdProgtot.Location = New System.Drawing.Point(3, 0)
    Me.cmdProgtot.Name = "cmdProgtot"
    Me.cmdProgtot.NTSContextMenu = Nothing
    Me.cmdProgtot.Size = New System.Drawing.Size(133, 23)
    Me.cmdProgtot.TabIndex = 599
    Me.cmdProgtot.Text = "Progressivi &totali"
    '
    'cmdValuta
    '
    Me.cmdValuta.ImageText = ""
    Me.cmdValuta.Location = New System.Drawing.Point(3, 44)
    Me.cmdValuta.Name = "cmdValuta"
    Me.cmdValuta.NTSContextMenu = Nothing
    Me.cmdValuta.Size = New System.Drawing.Size(133, 23)
    Me.cmdValuta.TabIndex = 600
    Me.cmdValuta.Text = "Descrizioni in ling&ua"
    '
    'pnTopLeft
    '
    Me.pnTopLeft.AllowDrop = True
    Me.pnTopLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTopLeft.Appearance.Options.UseBackColor = True
    Me.pnTopLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTopLeft.Controls.Add(Me.lbAr_codart)
    Me.pnTopLeft.Controls.Add(Me.edAr_desint)
    Me.pnTopLeft.Controls.Add(Me.lbAr_codalt)
    Me.pnTopLeft.Controls.Add(Me.lbAr_descr)
    Me.pnTopLeft.Controls.Add(Me.edAr_codalt)
    Me.pnTopLeft.Controls.Add(Me.edAr_descr)
    Me.pnTopLeft.Controls.Add(Me.edAr_codart)
    Me.pnTopLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTopLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTopLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnTopLeft.Name = "pnTopLeft"
    Me.pnTopLeft.NTSActiveTrasparency = True
    Me.pnTopLeft.Size = New System.Drawing.Size(402, 99)
    Me.pnTopLeft.TabIndex = 589
    Me.pnTopLeft.Text = "NtsPanel1"
    '
    'lbAr_codart
    '
    Me.lbAr_codart.AutoSize = True
    Me.lbAr_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codart.Location = New System.Drawing.Point(7, 11)
    Me.lbAr_codart.Name = "lbAr_codart"
    Me.lbAr_codart.NTSDbField = ""
    Me.lbAr_codart.Size = New System.Drawing.Size(77, 13)
    Me.lbAr_codart.TabIndex = 588
    Me.lbAr_codart.Text = "Codice articolo"
    Me.lbAr_codart.Tooltip = ""
    Me.lbAr_codart.UseMnemonic = False
    '
    'edAr_desint
    '
    Me.edAr_desint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_desint.Location = New System.Drawing.Point(104, 74)
    Me.edAr_desint.Name = "edAr_desint"
    Me.edAr_desint.NTSDbField = ""
    Me.edAr_desint.NTSForzaVisZoom = False
    Me.edAr_desint.NTSOldValue = ""
    Me.edAr_desint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_desint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_desint.Properties.AutoHeight = False
    Me.edAr_desint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_desint.Properties.MaxLength = 65536
    Me.edAr_desint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_desint.Size = New System.Drawing.Size(287, 20)
    Me.edAr_desint.TabIndex = 594
    Me.edAr_desint.TextStr = ""
    '
    'lbAr_codalt
    '
    Me.lbAr_codalt.AutoSize = True
    Me.lbAr_codalt.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codalt.Location = New System.Drawing.Point(7, 33)
    Me.lbAr_codalt.Name = "lbAr_codalt"
    Me.lbAr_codalt.NTSDbField = ""
    Me.lbAr_codalt.Size = New System.Drawing.Size(94, 13)
    Me.lbAr_codalt.TabIndex = 589
    Me.lbAr_codalt.Text = "Codice alternativo"
    Me.lbAr_codalt.Tooltip = ""
    Me.lbAr_codalt.UseMnemonic = False
    '
    'lbAr_descr
    '
    Me.lbAr_descr.AutoSize = True
    Me.lbAr_descr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_descr.Location = New System.Drawing.Point(7, 55)
    Me.lbAr_descr.Name = "lbAr_descr"
    Me.lbAr_descr.NTSDbField = ""
    Me.lbAr_descr.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_descr.TabIndex = 590
    Me.lbAr_descr.Text = "Descrizione"
    Me.lbAr_descr.Tooltip = ""
    Me.lbAr_descr.UseMnemonic = False
    '
    'edAr_codalt
    '
    Me.edAr_codalt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codalt.Location = New System.Drawing.Point(104, 30)
    Me.edAr_codalt.Name = "edAr_codalt"
    Me.edAr_codalt.NTSDbField = ""
    Me.edAr_codalt.NTSForzaVisZoom = False
    Me.edAr_codalt.NTSOldValue = ""
    Me.edAr_codalt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codalt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codalt.Properties.AutoHeight = False
    Me.edAr_codalt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codalt.Properties.MaxLength = 65536
    Me.edAr_codalt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codalt.Size = New System.Drawing.Size(182, 20)
    Me.edAr_codalt.TabIndex = 592
    Me.edAr_codalt.TextStr = ""
    '
    'edAr_descr
    '
    Me.edAr_descr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_descr.Location = New System.Drawing.Point(104, 52)
    Me.edAr_descr.Name = "edAr_descr"
    Me.edAr_descr.NTSDbField = ""
    Me.edAr_descr.NTSForzaVisZoom = False
    Me.edAr_descr.NTSOldValue = ""
    Me.edAr_descr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_descr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_descr.Properties.AutoHeight = False
    Me.edAr_descr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_descr.Properties.MaxLength = 65536
    Me.edAr_descr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_descr.Size = New System.Drawing.Size(287, 20)
    Me.edAr_descr.TabIndex = 593
    Me.edAr_descr.TextStr = ""
    '
    'edAr_codart
    '
    Me.edAr_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codart.Enabled = False
    Me.edAr_codart.Location = New System.Drawing.Point(104, 8)
    Me.edAr_codart.Name = "edAr_codart"
    Me.edAr_codart.NTSDbField = ""
    Me.edAr_codart.NTSForzaVisZoom = False
    Me.edAr_codart.NTSOldValue = ""
    Me.edAr_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codart.Properties.AutoHeight = False
    Me.edAr_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codart.Properties.MaxLength = 65536
    Me.edAr_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codart.Size = New System.Drawing.Size(182, 20)
    Me.edAr_codart.TabIndex = 591
    Me.edAr_codart.TextStr = ""
    '
    'edFocus
    '
    Me.edFocus.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFocus.Location = New System.Drawing.Point(-1000, -1000)
    Me.edFocus.Name = "edFocus"
    Me.edFocus.NTSDbField = ""
    Me.edFocus.NTSFormat = "0"
    Me.edFocus.NTSForzaVisZoom = False
    Me.edFocus.NTSOldValue = ""
    Me.edFocus.Properties.Appearance.Options.UseTextOptions = True
    Me.edFocus.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFocus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFocus.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFocus.Properties.AutoHeight = False
    Me.edFocus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFocus.Properties.MaxLength = 65536
    Me.edFocus.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFocus.Size = New System.Drawing.Size(10, 20)
    Me.edFocus.TabIndex = 504
    Me.edFocus.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edFocus.TextInt = 0
    '
    'tlbSimula
    '
    Me.tlbSimula.Caption = "Simulazione vendite"
    Me.tlbSimula.Id = 45
    Me.tlbSimula.Name = "tlbSimula"
    Me.tlbSimula.NTSIsCheckBox = False
    Me.tlbSimula.Visible = True
    '
    'FRMMGARTI_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(764, 479)
    Me.Controls.Add(Me.edFocus)
    Me.Controls.Add(Me.pnArti)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGARTI_F"
    Me.Text = "ANAGRAFICA ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnArti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnArti.ResumeLayout(False)
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tsArti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsArti.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnTabpag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag1.ResumeLayout(False)
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.cbAr_flricmar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ricar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ricar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_tipokit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codpdon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_reparto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAcquisti.ResumeLayout(False)
    Me.fmAcquisti.PerformLayout()
    CType(Me.edAr_scomin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_scomax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_forn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_forn2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3.ResumeLayout(False)
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3Right.ResumeLayout(False)
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAltriDati.ResumeLayout(False)
    Me.fmAltriDati.PerformLayout()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTabpag3Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3Left.ResumeLayout(False)
    Me.pnTabpag3Left.PerformLayout()
    CType(Me.edAr_pesolor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_pesonet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_tipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_claprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_clascon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_famprod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_sotgru.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_controa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag4.ResumeLayout(False)
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag5.ResumeLayout(False)
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag6.ResumeLayout(False)
    Me.NtsTabPage7.ResumeLayout(False)
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag7.ResumeLayout(False)
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUnitamisura.ResumeLayout(False)
    Me.fmUnitamisura.PerformLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_conver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_qtacon2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeftBut.ResumeLayout(False)
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeft.ResumeLayout(False)
    Me.pnTopLeft.PerformLayout()
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codalt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_descr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARTI", "BEMGARTI", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleArti = CType(oTmp, CLEMGARTI)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARTI", strRemoteServer, strRemotePort)
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArti.Init(oApp, oScript, oMenu.oCleComm, "ARTICO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttAr_tipokit As New DataTable()

    Try
      dttAr_tipokit.Columns.Add("cod", GetType(String))
      dttAr_tipokit.Columns.Add("val", GetType(String))
      dttAr_tipokit.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttAr_tipokit.Rows.Add(New Object() {"A", "Prezzi analitici"})
      dttAr_tipokit.Rows.Add(New Object() {"S", "Prezzo sintetici"})
      dttAr_tipokit.AcceptChanges()

      cbAr_tipokit.DataSource = dttAr_tipokit
      cbAr_tipokit.ValueMember = "cod"
      cbAr_tipokit.DisplayMember = "val"

      '-------------------------------------------------------------------------------------------------------------
      '--- Legge l'opzione globale per l'abilitazione/disabilitazione del campo del Conf. Tecnico(3)/Conf. Comm.(4):
      '--- cbTipitemcp3 (ARTICO.ar_tipitemcp3)
      '-------------------------------------------------------------------------------------------------------------
      oCleArti.strTipoConfiguratore = oMenu.GetSettingBus("OPZIONI", ".", ".", "TipoConfiguratore", "1", " ", "1")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim strFormatAr_conver As String
    Dim nLungMaxAr_conver As Integer
    Dim strFormatAr_sublotto As String
    Dim nLungMaxAr_sublotto As Integer
    Dim strNomeCampoAr_sublotto As String
    Dim strNomeCampoAr_rrfence As String
    Dim strNomeCampoAr_ggant As String
    Dim strNomeCampoAr_ggpost As String
    Dim strNomeCampoAr_forn As String
    Dim strNomeCampoAr_scomin As String
    Dim strFormatAr_scomin As String
    Dim strNomeCampoAr_minord As String
    Dim strFormatAr_minord As String
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbBarcode.GlyphPath = (oApp.ChildImageDir & "\barcode.gif")
        tlbKit.GlyphPath = (oApp.ChildImageDir & "\kit_1.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      If oCleArti.bConver9Dec = False Then
        strFormatAr_conver = oApp.FormatQta
        nLungMaxAr_conver = 12
      Else
        strFormatAr_conver = "#,##0.000000000"
        nLungMaxAr_conver = 18
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_sublotto = oApp.Tr(Me, 128788387403213993, "Lotto fisso")
        strFormatAr_sublotto = "0"
        nLungMaxAr_sublotto = 10
      Else
        strNomeCampoAr_sublotto = oApp.Tr(Me, 128788387442591009, "Sottolotto")
        strFormatAr_sublotto = oApp.FormatQta
        nLungMaxAr_sublotto = 13
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_rrfence = oApp.Tr(Me, 128554260471611269, "Lead Time standard")
      Else
        strNomeCampoAr_rrfence = oApp.Tr(Me, 128554260491717209, "RR Fence")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_ggant = oApp.Tr(Me, 128554262098010369, "N° giorni orizzonte per rilascio")
      Else
        strNomeCampoAr_ggant = oApp.Tr(Me, 128554262119674909, "Giorni di tolleranza Anticipo M.R.P.")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_ggpost = oApp.Tr(Me, 128556850606120748, "N° giorni orizzonte di controllo preventivo consegna")
      Else
        strNomeCampoAr_ggpost = oApp.Tr(Me, 128556850631449934, "Giorni di tolleranza Posticipo M.R.P.")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_forn = oApp.Tr(Me, 128557487229732616, "Fornitore standard")
      Else
        strNomeCampoAr_forn = oApp.Tr(Me, 128557487270680334, "Fornitore 1")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_scomin = oApp.Tr(Me, 129866656872819290, "Scorta di sicurezza/R.O.P.")
        strFormatAr_scomin = "0"
      Else
        strNomeCampoAr_scomin = oApp.Tr(Me, 129866656981588114, "Scorta minima")
        strFormatAr_scomin = oApp.FormatQta
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_minord = oApp.Tr(Me, 129866657016294922, "Lotto min. d'ord.")
        strFormatAr_minord = "0"
      Else
        strNomeCampoAr_minord = oApp.Tr(Me, 129866657044439272, "Qta Lotto std pr./ac.")
        strFormatAr_minord = oApp.FormatQta
      End If

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAr_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387525564007, "Codice articolo"), tabartico, False)
      edAr_codalt.NTSSetParam(oMenu, oApp.Tr(Me, 128788387549471481, "Codice altern.:"), CLN__STD.CodartMaxLen, True)
      edAr_descr.NTSSetParam(oMenu, oApp.Tr(Me, 128788387568222441, "Descrizione:"), 40, False)
      edAr_desint.NTSSetParam(oMenu, oApp.Tr(Me, 128788387583535725, "Descrizione2"), 40, True)
      edAr_tipo.NTSSetParam(oMenu, oApp.Tr(Me, 128788387601974169, "Tipo:"), 1, True)
      edAr_unmis.NTSSetParam(oMenu, oApp.Tr(Me, 130385777881391653, "Un. misura principale"), 3, True)
      edAr_unmis2.NTSSetParam(oMenu, oApp.Tr(Me, 130385778736079153, "Un. misura secondaria:"), 3, True)
      edAr_conver.NTSSetParam(oMenu, oApp.Tr(Me, 128788387652289245, "Quantità secondaria"), strFormatAr_conver, nLungMaxAr_conver, 0, 99999999)
      edAr_confez2.NTSSetParam(oMenu, oApp.Tr(Me, 130385779693735403, "Un. misura confezione:"), 3, True)
      edAr_qtacon2.NTSSetParam(oMenu, oApp.Tr(Me, 128788387704323159, "Quantità confezione"), oApp.FormatQta, 6, 0, 99999999)
      edAr_forn.NTSSetParamTabe(oMenu, strNomeCampoAr_forn, tabanagraf)
      edAr_forn2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387722761603, "Fornitore 2"), tabanagraf)
      edAr_codiva.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387744793981, "Codice IVA:"), tabciva)
      edAr_gruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387766201327, "Gruppo merc.:"), tabgmer)
      edAr_sotgru.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387787921189, "Sottogruppo merc.:"), tabsgme)
      edAr_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387801828151, "Controp. vendite:"), tabcove)
      edAr_scomin.NTSSetParam(oMenu, strNomeCampoAr_scomin, strFormatAr_scomin, 13, 0, 9999999999)
      edAr_scomax.NTSSetParam(oMenu, oApp.Tr(Me, 128788387961367569, "Scorta max"), oApp.FormatQta, 13, 0, 9999999999)
      edAr_controa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388043090503, "Controp. acquisti:"), tabcove)
      edAr_reparto.NTSSetParam(oMenu, oApp.Tr(Me, 128788388068404299, "Rep. (ECR):"), "0", 3, 0, 999)
      ckAr_stalist.NTSSetParam(oMenu, oApp.Tr(Me, 128788388084967647, "Stampa articolo nel listino"), "S", "N")
      edAr_famprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388116219247, "Famiglia:"), tabcfam, True)
      edAr_codpdon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388217318173, "Relazione listini:"), tabpdon)

      Dim dttAr_flricmar As New DataTable()
      dttAr_flricmar.Columns.Add("cod", GetType(String))
      dttAr_flricmar.Columns.Add("val", GetType(String))
      dttAr_flricmar.Rows.Add(New Object() {"M", "Margine"})
      dttAr_flricmar.Rows.Add(New Object() {"R", "Ricarico"})
      dttAr_flricmar.AcceptChanges()
      cbAr_flricmar.DataSource = dttAr_flricmar
      cbAr_flricmar.ValueMember = "cod"
      cbAr_flricmar.DisplayMember = "val"
      cbAr_flricmar.NTSSetParam(oApp.Tr(Me, 128788388166846839, "Ricarico/Margine:"))
      edAr_ricar1.NTSSetParam(oMenu, oApp.Tr(Me, 128788388233412747, "Ricarico/Margine1"), oApp.FormatImporti, 8, 0, 99999999)
      edAr_ricar2.NTSSetParam(oMenu, oApp.Tr(Me, 128788388246382161, "Ricarico/Margine2"), oApp.FormatImporti, 8, 0, 99999999)

      edAr_pesolor.NTSSetParam(oMenu, oApp.Tr(Me, 128788388341543283, "Peso lordo:"), "#,##0.000000", 13, 0, 999999)
      edAr_pesonet.NTSSetParam(oMenu, oApp.Tr(Me, 128788388357169083, "P.netto:"), "#,##0.000000", 13, 0, 999999)
      ckAr_stainv.NTSSetParam(oMenu, oApp.Tr(Me, 128788388479362839, "Stampa articolo nell'inventario"), "S", "N")
      ckAr_stasche.NTSSetParam(oMenu, oApp.Tr(Me, 128788388521083725, "Stampa scheda articolo"), "S", "N")
      ckAr_pesoca.NTSSetParam(oMenu, oApp.Tr(Me, 128788388736868733, "Non proporre le note art.sulle righe docum."), "1", "0")
      cbAr_tipokit.NTSSetParam(oApp.Tr(Me, 128788388911318442, "Tipo kit:"))
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 130385780652641653, "Molt.qtà/prezzo:"), "0", 12, 0, 999999999999)
      edAr_gif1.NTSSetParam(oMenu, oApp.Tr(Me, 128788389100292612, "Immagine catalogo"), 50, True)
      edAr_gif2.NTSSetParam(oMenu, oApp.Tr(Me, 128788389121064153, "Immagine scheda"), 50, True)
      edAr_note.NTSSetParam(oMenu, oApp.Tr(Me, 128788389591781631, "Note"), 0, True)
      edAr_claprov.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389609429632, "Cl.provv."), tabcpar)
      edAr_clascon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389631606766, "Cl.sconto"), tabcsar)

      ceListini.NTSSetParam(oMenu, "Listini", "BNMGARTI", Nothing)
      ceListini.tlbListEsci.Enabled = False
      ceListini.LcTipo = " "
      ceListini.LcCodart = ""
      ceListini.LcConto = 0
      AddHandler ceListini.VaiScontoCollegato, AddressOf ceListini_VaiScontoCollegato

      ceSconti.NTSSetParam(oMenu, "Sconti", "BNMGARTI", Nothing)
      ceSconti.tlbScontiEsci.Enabled = False
      ceSconti.TipoSconto = 0
      ceSconti.SoCodart = ""
      ceSconti.SoConto = 0
      ceSconti.SoClasseCli = 0
      ceSconti.SoClasseArt = 0
      AddHandler ceSconti.VaiListinoCollegato, AddressOf ceSconti_VaiListinoCollegato

      ceProvvig.NTSSetParam(oMenu, "Provvigioni", "BNMGARTI", Nothing)
      ceProvvig.tlbProvEsci.Enabled = False
      ceProvvig.TipoProvv = 1
      ceProvvig.PerCodart = ""
      ceProvvig.PerConto = 0
      ceProvvig.PerCodcage = 0
      ceProvvig.PerClasseCli = 0
      ceProvvig.PerClasseArt = 0

      edAr_descr.NTSSetRichiesto()

      edAr_unmis.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_unmis2.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_confez2.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_reparto.NTSSetParamZoom("ZOOMTABREAR")
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

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAr_codart.NTSDbField = "ARTICO.ar_codart"
      edAr_codalt.NTSDbField = "ARTICO.ar_codalt"
      edAr_descr.NTSDbField = "ARTICO.ar_descr"
      edAr_desint.NTSDbField = "ARTICO.ar_desint"
      edAr_tipo.NTSDbField = "ARTICO.ar_tipo"
      edAr_unmis.NTSDbField = "ARTICO.ar_unmis"
      edAr_unmis2.NTSDbField = "ARTICO.ar_unmis2"
      edAr_conver.NTSDbField = "ARTICO.ar_conver"
      edAr_confez2.NTSDbField = "ARTICO.ar_confez2"
      edAr_qtacon2.NTSDbField = "ARTICO.ar_qtacon2"
      edAr_forn.NTSDbField = "ARTICO.ar_forn"
      edAr_forn2.NTSDbField = "ARTICO.ar_forn2"
      edAr_codiva.NTSDbField = "ARTICO.ar_codiva"
      edAr_gruppo.NTSDbField = "ARTICO.ar_gruppo"
      edAr_sotgru.NTSDbField = "ARTICO.ar_sotgru"
      edAr_controp.NTSDbField = "ARTICO.ar_controp"
      edAr_scomin.NTSDbField = "ARTICO.ar_scomin"
      edAr_scomax.NTSDbField = "ARTICO.ar_scomax"
      edAr_controa.NTSDbField = "ARTICO.ar_controa"
      edAr_reparto.NTSDbField = "ARTICO.ar_reparto"
      ckAr_stalist.NTSText.NTSDbField = "ARTICO.ar_stalist"
      edAr_famprod.NTSDbField = "ARTICO.ar_famprod"
      edAr_codpdon.NTSDbField = "ARTICO.ar_codpdon"
      cbAr_flricmar.NTSDbField = "ARTICO.ar_flricmar"
      edAr_ricar1.NTSDbField = "ARTICO.ar_ricar1"
      edAr_ricar2.NTSDbField = "ARTICO.ar_ricar2"
      edAr_pesolor.NTSDbField = "ARTICO.ar_pesolor"
      edAr_pesonet.NTSDbField = "ARTICO.ar_pesonet"
      ckAr_stainv.NTSText.NTSDbField = "ARTICO.ar_stainv"
      ckAr_stasche.NTSText.NTSDbField = "ARTICO.ar_stasche"
      edAr_note.NTSDbField = "ARTICO.ar_note"
      ckAr_pesoca.NTSText.NTSDbField = "ARTICO.ar_pesoca"
      cbAr_tipokit.NTSDbField = "ARTICO.ar_tipokit"
      edAr_perqta.NTSDbField = "ARTICO.ar_perqta"
      edAr_gif1.NTSDbField = "ARTICO.ar_gif1"
      edAr_gif2.NTSDbField = "ARTICO.ar_gif2"
      lbXx_codpdon.NTSDbField = "ARTICO.xx_codpdon"
      lbXx_forn.NTSDbField = "ARTICO.xx_forn"
      lbXx_forn2.NTSDbField = "ARTICO.xx_forn2"
      lbXx_codiva.NTSDbField = "ARTICO.xx_codiva"
      lbXx_gruppo.NTSDbField = "ARTICO.xx_gruppo"
      lbXx_sotgru.NTSDbField = "ARTICO.xx_sotgru"
      lbXx_controp.NTSDbField = "ARTICO.xx_controp"
      lbXx_controa.NTSDbField = "ARTICO.xx_controa"
      lbXx_famprod.NTSDbField = "ARTICO.xx_famprod"
      edAr_claprov.NTSDbField = "ARTICO.ar_claprov"
      edAr_clascon.NTSDbField = "ARTICO.ar_clascon"
      lbXx_clascon.NTSDbField = "ARTICO.xx_clascon"
      lbXx_claprov.NTSDbField = "ARTICO.xx_claprov"
      lbXx_reparto.NTSDbField = "ARTICO.xx_reparto"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcArti, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGARTI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      If Me.Modal = True Then Me.MinimizeBox = False

      If bPreload Then Return

      'prende, se esistono, le opzioni dal registro
      oCleArti.nCodiva = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodIva", "0", " ", "0"))
      oCleArti.nControa = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controa", "0", " ", "0"))
      oCleArti.nControp = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controp", "0", " ", "0"))
      oCleArti.nContros = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Contros", "0", " ", "0"))
      oCleArti.strUnmis = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "UnMis", " ", " ", " ")
      oCleArti.strInizioValListini = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValListini", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.strInizioValSconti = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValSconti", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.strInizioValProvv = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValProvv", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.bConver9Dec = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Conver9Dec", "0", " ", "0"))
      oCleArti.bSbloccaFLotto = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SbloccaFLotto", "0", " ", "0"))
      oCleArti.strListiniAbilitati = oMenu.GetSettingBus("BSMGARTI", "OPZIONIUT", ".", "ListiniAbilitati", "", " ", "")
      oCleArti.bGestTabUnmis = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestTabUnmis", "0", " ", "0"))
      oCleArti.nCodArtDaCat = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoTipoGen", "0", " ", "0"))
      oCleArti.nCodArtDaCatNListPubb = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      oCleArti.nCodArtDaCatNListIngr = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))
      '----------------------------------------------------------------------
      '--- Opzioni di registro per la gestione dei flag nella modale relativa
      '--- alla duplicazione dell'articolo
      '----------------------------------------------------------------------
      oCleArti.bDuplicaListini = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaListini", "-1", " ", "-1"))
      oCleArti.bDuplicaSconti = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaSconti", "-1", " ", "-1"))
      oCleArti.bDuplicaProvvigioni = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaProvvigioni", "-1", " ", "-1"))
      oCleArti.bDuplicaDescrLingua = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaDescrLingua", "-1", " ", "-1"))
      oCleArti.bDuplicaKit = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaKit", "-1", " ", "-1"))
      oCleArti.bDuplicaConai = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaConai", "-1", " ", "-1"))
      oCleArti.bDuplicaFasi = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaFasi", "-1", " ", "-1"))
      oCleArti.bGestUbicSenzaLext = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestUbicSenzaLext", "0", " ", "0"))) 'NON DOCUMENTARE
      '-------------------------------------------------------------------------------------------------------
      'Opzione globale per la gestione dei prezzi riferiti ad una unità di misura diversa da quella principale
      oCleArti.bAbilitaPrezzoUM = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))
      oCleArti.bValidaNomenclCombin = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ValidaNomenclCombin", "0", " ", "0"))) 'se abilitata in fase di inserimento delal nomenclatura la valida
      oCleArti.strAltezzaMaxImg = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "AltezzaMaxImg", "0", " ", "0")
      '-------------------------------------------------------------------------------------------------------
      oCleArti.bSelCodiceNoApri = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SelCodiceNoApri", "0", " ", "0"))
      '-------------------------------------------------------------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------------------------------------------------------------
      '--- Se c'è il modulo "Config. di Prodotto su D.B. Neutra"
      '--- abilita il campo relativo a 'Gestione Serial': ar_gestser
      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCP2) Then
        oCleArti.bCP2 = True
      Else
        oCleArti.bCP2 = False
      End If
      '-------------------------------------------------------------------------------------------------------
      '--- Se non c'è il modulo "Logistica Estesa"
      '--- disabilita i campi relativi a: ar_gesubic, ar_gesfasi, ar_ultfase
      '--- e la voce di menu per la gestione di ATRFASI
      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtLEX) Or CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP) Then
        oCleArti.bLogisticaEstesa = True
      Else
        oCleArti.bLogisticaEstesa = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleArti.bModuloORTO = False
      If CBool(oMenu.ModuliSupExtDittaDitt(DittaCorrente) And bsModSupExtJOR) Then oCleArti.bModuloORTO = True
      '--------------------------------------------------------------------------------------------------------------
      SetStato(0)

      oCleArti.bDuplicaArt = False
      '-----------------------------------------------------------------------------------------
      oCleArti.strUnmisOrigine = ""
      oCleArti.strUnmis2Origine = ""
      oCleArti.strConfez2Origine = ""
      oCleArti.strUm4Origine = ""
      '-----------------------------------------------------------------------------------------
      '--- Per Euro2000:
      '--- Se attiva l'opzione, il valore minimo impostato per i campio Scorta Minima/Massima/Qtà lotto/std
      '--- con Politica di riordino, è zero
      '-----------------------------------------------------------------------------------------
      oCleArti.bNoMsgCongruenzaPolSconte = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "NoMsgCongruenzaPolScorte", "0", " ", "0")))
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim dsTmp As DataSet = Nothing
    Dim dttPeca As New DataTable
    Try
      '----------------------------------------------------------------------------------------
      '--- Se non esiste almeno la tipologia 1 chiude
      '----------------------------------------------------------------------------------------
      If Not oCleArti.GetTabTipa1() Then
        Me.Close()
        Return
      End If
      '----------------------------------------------------------------------------------------
      oCleArti.GetTabTipa(dsTmp)

      oCleArti.SetImagesDir()
      NTSFormClearDataBinding(Me)

      '---------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        oMenu.ValCodiceDb("1", DittaCorrente, "TABPECA", "N", , dttPeca)
        If dttPeca.Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129319677734956014, "Tabella delle Personalizzazioni CA-DC (Globale) non configurata. Imposibile continuare"))
          Me.Close()
          Return
        End If
        oCleArti.bCampiCAEAttivi = CBool(IIf(NTSCStr(dttPeca.Rows(0)!tb_richarti) = "S", True, False))
      End If

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then
          tlbApri_ItemClick(Me, Nothing)
        ElseIf oCallParams.strParam <> "" AndAlso Microsoft.VisualBasic.Mid(oCallParams.strParam, 5) <> "DUPL;" Then
          tlbDuplica_ItemClick(Me, Nothing)
        ElseIf oCallParams.strPar1 <> "" Then
          oCleArti.strWhereFiar = oCallParams.strPar1
          oCleArti.strOrderBy = oCallParams.strPar2
          ApriArticoli()
        End If
      End If    'If Not oCallParams Is Nothing Then

      oCallParams = Nothing 'in questo modo se vengo chiamato da bnmghlar, prima apro l'articolo scelto, poi se faccio ripristina posso scegliere un altro articolo 
      oCleArti.bDaGest = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If edAr_note.Focused And e.KeyCode = Keys.Return Then
        edAr_note.Focus()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then
          e.Cancel = True
          Return
        End If
      End If
      oCleArti.Ripristina(dcArti.Position, dcArti.Filter)
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
        ceListini.Ripristina()
      ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
        ceSconti.Ripristina()
      ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
        ceProvvig.Ripristina()
      End If
      SetStato(0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArti.Dispose()
      dsArti.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdVisGif1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisGif1.Click
    Dim oPar As New CLE__CLDP
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArti.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129301475003, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste il file indicato nel TextBox relativo, nella cartella delle immagini
      '--- avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not File.Exists(oCleArti.strImageDir & "\" & edAr_gif1.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129514857263, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif1.Text = ""
        edAr_gif1.Focus()
        cmdVisGif1.Enabled = False
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = 1
      oPar.strPar2 = oCleArti.strImageDir & "\" & edAr_gif1.Text
      oPar.strPar3 = oCleArti.strAltezzaMaxImg

      oMenu.RunChild("NTSInformatica", "FRMMGVGIF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdVisGif2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisGif2.Click
    Dim oPar As New CLE__CLDP
    Try
      If Not Directory.Exists(oCleArti.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129803371577, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      If Not File.Exists(oCleArti.strImageDir & "\" & edAr_gif2.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128788389897732374, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif2.Text = ""
        edAr_gif2.Focus()
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = 2
      oPar.strPar2 = oCleArti.strImageDir & "\" & edAr_gif2.Text
      oPar.strPar3 = oCleArti.strAltezzaMaxImg

      oMenu.RunChild("NTSInformatica", "FRMMGVGIF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdArtGif1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtGif1.Click
    Dim nArtgif As Integer
    Try
      nArtgif = 1
      ApriGif(nArtgif)
      If edAr_gif1.Text <> "" Then
        cmdVisGif1.Enabled = True
        oCleArti.bHasChanges = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdArtgif2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtgif2.Click
    Dim nArtgif As Integer
    Try
      nArtgif = 2
      ApriGif(nArtgif)
      If edAr_gif2.Text <> "" Then
        cmdVisGif2.Enabled = True
        oCleArti.bHasChanges = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgtot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgtot.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = NTSCDec(edAr_perqta.Text)
      oPar.strPar2 = edAr_codart.Text
      oPar.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdValuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValuta.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGHLAV", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgressivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgressivi.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text
      oPar.dPar1 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCodarfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodarfo.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGCACF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edAr_forn.ContainsFocus Or edAr_forn2.ContainsFocus Then
        strTabName = "ANAGRA_FOR"
      Else
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
          Select Case ceListini.cbTipolistino.SelectedValue
            Case "C" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_CLI"
            Case "F" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_FOR"
          End Select
        End If
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
          Select Case NTSCInt(ceSconti.cbTiposconti.SelectedValue)
            Case 4 : If ceSconti.grvSconti.FocusedColumn.Name = "so_conto" Then strTabName = "ANAGRACF"
          End Select
        End If
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
          Select Case NTSCInt(ceProvvig.cbTipoProv.SelectedValue)
            Case 3 : If ceProvvig.grvProv.FocusedColumn.Name = "per_conto" Then strTabName = "ANAGRACF"
          End Select
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(True, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(False, True) Then Return

      ceSconti.cbTiposconti.SelectedValue = "1"

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
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(False, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '-------------------------------------------------
      'prima di salvare simulo una lostfocus del campo su cui mi trovo, altrimenti potrei salvare un dato non corretto
      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728307978584, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If Not oCleArti.TestPreCancella(NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codart), _
              NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipoopz), _
              NTSCInt(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codtipa)) Then Return

          If dsArti.Tables("ARTICO").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcArti.RemoveAt(dcArti.Position)
          oCleArti.Salva(True)

          If dsArti.Tables("ARTICO").Rows.Count = 0 Then SetStato(0)

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArti, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      If nStato = 0 Then Return

      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308134760, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsArti.Tables("ARTICO").Rows.Count = 1 Then
            If dsArti.Tables("ARTICO").Rows(0).RowState = DataRowState.Added Then
              bRemovBinding = True
              NTSFormClearDataBinding(Me)
            End If
          End If

          oCleArti.Ripristina(dcArti.Position, dcArti.Filter)

          If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
            ceListini.Ripristina()
          ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
            ceSconti.Ripristina()
          ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
            ceProvvig.Ripristina()
          End If
          If Not sender Is Nothing Then
            SetStato(0)
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArti, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If oCleArti.bGestTabUnmis = False And _
      (edAr_unmis.Focused Or edAr_confez2.Focused Or edAr_unmis2.Focused) Then Return

      If ceListini.grList.Focused Then
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
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      tlbRipristina_ItemClick(Nothing, Nothing)
    End If
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcArti.MoveFirst()
      AbilitaControlli()
      ApriListSconProv()
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
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
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcArti.MovePrevious()
      AbilitaControlli()
      ApriListSconProv()
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
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
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcArti.MoveNext()
      AbilitaControlli()
      ApriListSconProv()
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
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
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcArti.MoveLast()
      AbilitaControlli()
      ApriListSconProv()
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbBarcode_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbBarcode.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsTmp As DataSet = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text
      oPar.strPar3 = edAr_unmis.Text
      oPar.strPar4 = edAr_confez2.Text
      oPar.strPar5 = edAr_unmis2.Text
      oPar.dPar1 = NTSCDec(edAr_qtacon2.Text)
      oPar.dPar2 = NTSCDec(edAr_conver.Text)
      oPar.bPar1 = False
      oPar.dPar3 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGBARC", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbKit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbKit.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipokit) = " " Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128565244716608415, "Articolo non tipo Kit, apertura Composizione Kit non possibile."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGCKIT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaEtichette_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaEtichette.ItemClick
    Dim strParam As String
    Dim strCodart As String
    Dim strQuant As String
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      strCodart = edAr_codart.Text
      If strCodart.Trim <> "" Then
        strCodart = strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
        strQuant = NTSCInt(1).ToString.PadLeft(9)
        strQuant = Microsoft.VisualBasic.Right(strQuant, 9)
        strParam = "ART;" & strCodart & ";" & strQuant & ";"
        oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128575004651798150, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbVisualizzaMovimenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisualizzaMovimenti.ItemClick
    Dim strParam As String
    Dim strCodart As String
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      strCodart = edAr_codart.Text
      If strCodart.Trim <> "" Then
        strCodart = strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
        strParam = "APRI:" & strCodart & ";0000;000000000;A"
        oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", oApp.Tr(Me, 128575010044763178, "Stampa schede articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSimula_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSimula.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
        If Salva() = False Then Return
      Else
        If oCleArti.bNew = True Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oPar.strPar1 = edAr_codart.Text
      oMenu.RunChild("NTSInformatica", "FRMMGSIMU", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri(ByVal bNew As Boolean, ByVal bDuplica As Boolean) As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmCoar As FRMMGCOAR = Nothing
    Dim bDuplEster As Boolean
    Dim strArtProposto As String = ""
    Try
      oCleArti.strOrderBy = ""
      If Not oCallParams Is Nothing Then
        'APRI DA GEST
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then

          oCleArti.strCodart = NTSCStr(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          bNew = False
          'se richiamato apri da gestione con codice articolo salta la modale di apertura
          If oCleArti.strCodart <> "" Then oCleArti.bDaGest = True

          'non apre un articolo a varianti
          If oCleArti.strCodart <> "" Then
            If Not oCleArti.CheckArtVarianti(oCleArti.strCodart) Then Return False
          End If

          'DUPL DA GEST
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "DUPL;" Then

          oCleArti.strArtiDescr = Trim(Mid(oCallParams.strParam, 6, 40))
          oCleArti.strArtiDesint = Trim(Mid(oCallParams.strParam, 47, 40))
          oCleArti.lArtiForn = NTSCInt(Mid(oCallParams.strParam, 88, 9))
          oCleArti.strArtiNote = Trim(Mid(oCallParams.strParam, 98))

          bNew = False
          bDuplica = True
          bDuplEster = True

          'NUOV DA GEST
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then

          oCleArti.strCodart = NTSCStr(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          bNew = True
          bDuplica = False
          'se richiamato nuovo da gestione con codice articolo salta la modale di apertura
          If oCleArti.strCodart <> "" AndAlso oCleArti.strCodart <> "0" Then
            If oCleArti.strCodart.StartsWith("0§") Then
              strArtProposto = oCleArti.strCodart.Substring(2)
            Else
              oCleArti.bDaGest = True
            End If
          End If
        End If
      End If

      If bNew Then
        'NUOVO
        '-------------------------------------------------
        'creo un nuovo articolo
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return False
        End If
        SetStato(0)

        If oCleArti.bDaGest = False Then 'SALTA la form modale se chiamato in apertura da gestione NUOVO;
          oCleArti.bCoarGeneraArticoli = True
          oCleArti.nCoarEditmode = 2
          oCleArti.strCodart = ""

          frmCoar = CType(NTSNewFormModal("FRMMGCOAR"), FRMMGCOAR)
          frmCoar.Init(oMenu, oParam, DittaCorrente)
          frmCoar.InitEntity(oCleArti)
          frmCoar.bCoarDuplica = False
          frmCoar.edCodart.Text = strArtProposto
          frmCoar.ShowDialog()

          If frmCoar.bCoarAnnullato = True Then Return False
        End If

        If oCleArti.strCodart = "" Then Return False

        If oCleArti.strModello.Trim = "" Then
          If Not NuovoArticolo() Then Return False
        Else
          bDuplica = True
          Dim strArt As String = oCleArti.strCodart
          oCleArti.strCodart = oCleArti.strModello
          If Not ApriArticolo(bDuplica, 1) Then Return False
          If Not DuplicaArticolo(bDuplEster, strArt) Then Return False
          bDuplica = False
        End If
      Else
        If Not ApriArticolo(bDuplica, nStato) Then Return False
      End If

      '------------------------------------------------
      If bDuplica Then DuplicaArticolo(bDuplEster, "")

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      If bNew And Not bDuplica Then
        oCleArti.bHasChanges = True
        Me.GctlApplicaDefaultValue()
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoar Is Nothing Then frmCoar.Dispose()
      frmCoar = Nothing
    End Try
  End Function
  Public Overridable Function ApriArticoli() As Boolean
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleArti.Apri(DittaCorrente, dsArti) Then Return False
      If dsArti.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128553319361865862, "Nessun articolo trovato."))
        Return False
      End If
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dsArti.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcArti.ResetBindings(False)
      dcArti.MoveFirst()
      If Not oCallParams Is Nothing AndAlso oCallParams.strPar3 <> "" Then
        'Cerca di posizionarsi sullo stesso articolo che è stato passato
        For z As Integer = 0 To dsArti.Tables("ARTICO").Rows.Count - 1
          If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codart) = oCallParams.strPar3 Then Exit For
          dcArti.MoveNext()
        Next
      End If

      ApriListSconProv()

      SetStato(1)

      edAr_codart.Enabled = False
      edAr_unmis.Enabled = False
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      End If
      edAr_descr.Focus()
      oCleArti.bValarti = False

      AbilitaControlli()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function NuovoArticolo() As Boolean
    Try
      oCleArti.Nuovo(dsArti)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcArti = New BindingSource
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dcArti.MoveLast()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcArti.ResetBindings(False)

      SetStato(1)

      GctlSetVisEnab(edAr_unmis, False)
      GctlSetVisEnab(edAr_confez2, False)
      GctlSetVisEnab(edAr_unmis2, False)
      GctlSetVisEnab(edAr_perqta, False)

      edAr_codalt.Focus()
      oCleArti.bValarti = False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function DuplicaArticolo(ByVal bDuplEster As Boolean, ByVal strCodart As String) As Boolean
    Dim frmDuar As FRMMGDUAR_F = Nothing
    Try
      '------------------------------------------------
      oCleArti.bDuplicazioneInCorso = True
      oCleArti.strCodartDuar = ""
      frmDuar = CType(NTSNewFormModal("FRMMGDUAR_F"), FRMMGDUAR_F)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oCleArti = oCleArti
      frmDuar.edCodart.Text = strCodart
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or oCleArti.strCodartDuar = "" Then
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se ar_gesvar = "K" non permette la duplicazione
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_gesvar) = "K" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128560027153593750, "Articolo configurato non duplicabile."))
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se ar_tipitemcp3 = "E" non permette la duplicazione
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipitemcp3) = "E" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128560027876562500, "Articolo extralogistico non duplicabile."))
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If
      '------------

      SetStato(1)

      GctlTipoDoc = " "
      GctlSetRoules()
      ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
      ceSconti.TipoSconto = 0
      ceProvvig.TipoProvv = 1
      GctlApplicaDefaultValue()

      If Not oCleArti.DuplicaArticolo(oCleArti.strCodartDuar, _
                                   frmDuar.ckListini.Checked, frmDuar.ckSconti.Checked, _
                                   frmDuar.ckProvvigioni.Checked, frmDuar.ckArtval.Checked, _
                                   frmDuar.ckKit.Checked, False, False, bDuplEster) Then
        'ripristino
        If dsArti.Tables("ARTICO").Rows.Count = 1 And dsArti.Tables("ARTICO").Rows(0).RowState = DataRowState.Added Then
          NTSFormClearDataBinding(Me)
        End If
        oCleArti.Ripristina(dcArti.Position, dcArti.Filter)
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
          ceListini.Ripristina()
        ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
          ceSconti.Ripristina()
        ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
          ceProvvig.Ripristina()
        End If

        SetStato(0)

        oCleArti.bDuplicazioneInCorso = False

        Return False
      End If

      AbilitaControlli()
      oCleArti.bDuplicazioneInCorso = False

      oApp.MsgBoxInfo(oApp.Tr(Me, 128473853444846662, "Duplicazione terminata"))
      oCleArti.strCodart = oCleArti.strCodartDuar
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ApriArticolo(ByVal bDuplica As Boolean, ByVal nPrecStato As Integer) As Boolean
    Dim frmCoar As FRMMGCOAR = Nothing
    Dim oParam As New CLE__CLDP
    Try
      'APRI
      '-------------------------------------------------
      'apro un nuovo articolo
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return False
      End If
      SetStato(0)

      'SALTA la form modale se chiamato in apertura da gestione APRI;
      'SALTA la form modale se chiamato in apertura da duplica in stato 1
      If oCleArti.bDaGest = False And (Not (bDuplica = True And nPrecStato = 1 And oCleArti.strCodart <> "")) Then

        oCleArti.nCoarEditmode = 1
        oCleArti.strCodart = ""
        oCleArti.strWhereFiar = ""

        frmCoar = CType(NTSNewFormModal("FRMMGCOAR"), FRMMGCOAR)
        frmCoar.Init(oMenu, oParam, DittaCorrente)
        frmCoar.InitEntity(oCleArti)
        If bDuplica Then
          frmCoar.bCoarDuplica = True
          frmCoar.cmdSeleziona.Enabled = False
        End If
        frmCoar.ShowDialog()

        If frmCoar.bCoarAnnullato = True Then Return False

        If (oCleArti.strCodart = "") And (oCleArti.strWhereFiar = "") Then Return False

      End If

      ceSconti.TipoSconto = 1
      ceProvvig.TipoProvv = 4
      GctlApplicaDefaultValue()

      ApriArticoli()
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  




  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Dim bNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il contenuto del TextBox relativo alla Distinta Base NON è cambiato rispetto al dato nel database
      '--- e nel database il codice è diverso, mette il contenuto presente nel database nel Textbox relativo
      '--- (questo prima del salvataggio)
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bNew = False Then
        If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)("ar_coddb", DataRowVersion.Original)).ToUpper() = NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb).ToUpper Then
          If oMenu.ValCodiceDb(edAr_codart.Text, DittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
            If NTSCStr(dttTmp.Rows(0)!ar_coddb).ToUpper <> NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb).ToUpper Then
              dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb = NTSCStr(dttTmp.Rows(0)!ar_coddb)
            End If
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308290936, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          bNew = oCleArti.bNew
          If Not oCleArti.Salva(False) Then
            Return False
          Else
            If bNew Then
              'memorizzo il nuovo conto negli appunti, così se sono stato chiamato da ALT+F2 posso acquisire in automatico il nuovo conto
              oApp.NTSClipboard = "NTSNEW:ARTICO:" & edAr_codart.Text
            End If
            AbilitaControlli()
          End If
        End If
      End If

      '-------------------------------------------------
      'salvo i listini/sconti/provvigioni
      If Not ceListini.Salva Then Return False
      If Not ceSconti.Salva Then Return False
      If Not ceProvvig.Salva Then Return False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaTipo()
      '--------------------------------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      '----------------------------------
      '0 = nessuna registraz aperta
      If nSetStato = 0 Then
        nStato = 0

        ceListini.LcCodart = ""
        ceSconti.SoCodart = ""
        ceProvvig.PerCodart = ""

        '---------------------------------------
        'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
        'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
        'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
        'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida
        ceListini.Enabled = False
        ceSconti.Enabled = False
        ceProvvig.Enabled = False

        pnArti.Visible = False

        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbDuplica, False)

        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbZoom.Enabled = False
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbBarcode.Enabled = False
        tlbKit.Enabled = False
        tlbStampaEtichette.Enabled = False
        tlbVisualizzaMovimenti.Enabled = False
        tlbSimula.Enabled = False

        If Not dsArti Is Nothing Then
          dsArti.AcceptChanges()
          oCleArti.bHasChanges = False
        End If
        edFocus.Enabled = True
        If Me.NTSActiveFirstOccured Then edFocus.Focus()

        imArtGif.Image = Nothing
      Else
        nStato = 1

        tsArti.SelectedTabPageIndex = 0

        GctlSetVisEnab(pnArti, True)

        GctlSetVisEnab(pnArti, False)
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbApri, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbPrimo, False)
        GctlSetVisEnab(tlbPrecedente, False)
        GctlSetVisEnab(tlbSuccessivo, False)
        GctlSetVisEnab(tlbUltimo, False)
        GctlSetVisEnab(tlbBarcode, False)
        GctlSetVisEnab(tlbKit, False)
        GctlSetVisEnab(tlbStampaEtichette, False)
        GctlSetVisEnab(tlbVisualizzaMovimenti, False)
        GctlSetVisEnab(tlbSimula, False)

        edFocus.Enabled = False
      End If

      edAr_codart.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaTipo()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AbilitaControlli()
    Try
      edAr_unmis.Enabled = False
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      Else
        GctlSetVisEnab(edAr_confez2, False)
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      Else
        GctlSetVisEnab(edAr_unmis2, False)
      End If
      edAr_perqta.Enabled = False
      '---------------------------------------------------------------------------------------
      If Trim(edAr_gif1.Text) = "" Then
        cmdVisGif1.Enabled = False
      Else
        GctlSetVisEnab(edAr_gif1, False)
      End If
      If Trim(edAr_gif2.Text) = "" Then
        cmdVisGif2.Enabled = False
      Else
        GctlSetVisEnab(edAr_gif2, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ApriListSconProv()
    Try
      '-------------------------------
      'se serve ricarico i listini
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(2)) Then
        If ceListini.LcCodart <> NTSCStr(edAr_codart.Text) Then
          ceListini.LcCodart = NTSCStr(edAr_codart.Text)
          ceListini.ApriListini()
        End If

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico gli sconti
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
        If ceSconti.SoCodart <> NTSCStr(edAr_codart.Text) Then
          ceSconti.SoCodart = NTSCStr(edAr_codart.Text)
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        GctlSetVisEnab(ceSconti.grvSconti, False)
        ceSconti.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico le provvigioni
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
        If ceProvvig.PerCodart <> NTSCStr(edAr_codart.Text) Then
          ceProvvig.PerCodart = NTSCStr(edAr_codart.Text)
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        GctlSetVisEnab(ceProvvig.grvProv, False)
        ceProvvig.tlbMain.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAr_gif1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_gif1.Validated
    Try
      If Trim(NTSCStr(edAr_gif1.Text)) = "" Then
        cmdVisGif1.Enabled = False
      Else
        GctlSetVisEnab(cmdVisGif1, False)
      End If
      '------------------------------------------------------------------------------------------------------------
      CaricaImmagine()
      '------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAr_gif2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_gif2.Validated
    Try
      If Trim(NTSCStr(edAr_gif2.Text)) = "" Then
        cmdVisGif2.Enabled = False
      Else
        GctlSetVisEnab(cmdVisGif2, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ApriGif(ByVal nQualegif As Integer) As Boolean
    Dim dRes As DialogResult
    Dim strNomeFile As String = ""
    Dim strPathFile As String = ""
    Dim nPosSep As Integer = 0
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini chiede di crearla
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArti.strImageDir) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128559133252059319, "La cartella delle immagini non esiste. Crearla?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Try
            MkDir(oCleArti.strImageDir)
          Catch ex As Exception
            Exit Function
          End Try
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Adesso apre la Common Dialog sulla cartella delle immagini
      '-----------------------------------------------------------------------------------------
      If OpenFileDialog1 Is Nothing Then OpenFileDialog1 = New NTSOpenFileDialog
      OpenFileDialog1.CheckFileExists = True
      OpenFileDialog1.ShowReadOnly = False
      OpenFileDialog1.ShowHelp = False
      OpenFileDialog1.DefaultExt = "gif"
      OpenFileDialog1.Title = "Selezione immagine"
      OpenFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
      OpenFileDialog1.InitialDirectory = oCleArti.strImageDir
      OpenFileDialog1.FileName = ""
      OpenFileDialog1.oMenu = oMenu
      OpenFileDialog1.ShowDialog()

      '-----------------------------------------------------------------------------------------
      If OpenFileDialog1.FileName <> "" Then
        'cerca l'ultimo simbolo \ per dividere il path dal nome del file
        'ATTENZIONE il nome del file non deve contenere il simbolo \
        For i = 1 To Len(OpenFileDialog1.FileName)
          If Mid(OpenFileDialog1.FileName, i, 1) = "\" Then
            nPosSep = i
          End If
        Next
        If nPosSep > 0 Then
          strNomeFile = Microsoft.VisualBasic.Mid(OpenFileDialog1.FileName, nPosSep + 1)
          strPathFile = Microsoft.VisualBasic.Left(OpenFileDialog1.FileName, nPosSep)
        Else
          strNomeFile = OpenFileDialog1.FileName
        End If

        '---------------------------------------------------------------------------------------
        '--- Se l'immagine selezionata non è nella cartella delle immagini
        '--- allora la copio nella cartella delle immagini
        '---------------------------------------------------------------------------------------
        If UCase(strPathFile) <> UCase(oCleArti.strImageDir) Then
          If Not System.IO.File.Exists(oCleArti.strImageDir & strNomeFile) Then
            Try
              FileCopy(OpenFileDialog1.FileName, oCleArti.strImageDir & strNomeFile)
            Catch ex As Exception
              Exit Function
            End Try
          End If
        End If
        '---------------------------------------------------------------------------------------
        If strNomeFile.Length > 50 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130374521581617698, "Attenzione!" & vbCrLf & _
            "Il nome/estensione de file indicato non può surerare i 50 caratteri."))
          Exit Function
        End If
        '---------------------------------------------------------------------------------------
        '--- Inseridce nel TextBox relativo il file immagine selezionato
        'Lo inserisco dopo aver ricopiato l'immagine se no da msg la BeforeColUpdate
        '---------------------------------------------------------------------------------------
        If nQualegif = 1 Then
          edAr_gif1.NTSTextDB = strNomeFile
        Else ' = 2
          edAr_gif2.NTSTextDB = strNomeFile
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub tsArti_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsArti.SelectedPageChanged
    Try
      '---------------------------------------
      'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
      'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
      'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
      'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida

      '-----------------------------
      'pagina dei listini
      If e.Page.Equals(tsArti.TabPages(2)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128466988465551088, "Salvare il nuovo articolo prima di passare alla cartella LISTINI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        ceListini.LcCodart = NTSCStr(edAr_codart.Text)
        ceListini.ApriListini()

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True

      End If

      If e.PrevPage.Equals(tsArti.TabPages(2)) Then
        If Not ceListini.Salva Then
          tsArti.SelectedTabPageIndex = 2
        Else
          ceListini.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina degli sconti
      If e.Page.Equals(tsArti.TabPages(3)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931444347256, "Salvare il nuovo articolo prima di passare alla cartella SCONTI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        If ceSconti.SoCodart <> NTSCStr(edAr_codart.Text) Then
          ceSconti.SoCodart = NTSCStr(edAr_codart.Text)
          Dim i As Integer = ceSconti.TipoSconto
          ceSconti.TipoSconto = 1   'serve per far applicare le impostazioni di griglia e ricaricare gli sconti
          ceSconti.TipoSconto = i
        Else
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        GctlSetVisEnab(ceSconti.grvSconti, False)
        ceSconti.tlbMain.Visible = True
      End If

      If e.PrevPage.Equals(tsArti.TabPages(3)) Then
        If Not ceSconti.Salva Then
          tsArti.SelectedTabPageIndex = 3
        Else
          ceSconti.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina delle provvigioni
      If e.Page.Equals(tsArti.TabPages(4)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931950723256, "Salvare il nuovo articolo prima di passare alla cartella PROVVIGIONI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        If ceProvvig.PerCodart <> NTSCStr(edAr_codart.Text) Then
          ceProvvig.PerCodart = NTSCStr(edAr_codart.Text)
          ceProvvig.TipoProvv = 4   'serve per far applicare le impostazioni di griglia e ricaricare le provvigioni
        Else
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        GctlSetVisEnab(ceProvvig.grvProv, False)
        ceProvvig.tlbMain.Visible = True
      End If

      If e.PrevPage.Equals(tsArti.TabPages(4)) Then
        If Not ceProvvig.Salva Then
          tsArti.SelectedTabPageIndex = 4
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

  Public Overridable Sub edAr_note_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAr_note.Enter
    Try
      Me.NTSDisableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAr_note_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAr_note.Leave
    Try
      Me.NTSEnableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AbilitaDisabilitaTipo()
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se Ã¨ attivo il modulo ORTO, in apertura di un articolo esistente, disabilita il campo "Tipo"
      '--------------------------------------------------------------------------------------------------------------
      GctlSetVisEnab(edAr_tipo, False)
      If (oCleArti.bNew = False) And (oCleArti.bModuloORTO = True) Then edAr_tipo.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaImmagine()
    Dim fs As System.IO.FileStream

    Try
      '--------------------------------------------------------------------------------------------------------------
      imArtGif.Image = Nothing
      '--------------------------------------------------------------------------------------------------------------
      If edAr_gif1.Text.Trim = "" Then
        imArtGif.Visible = False
        Return
      End If
      '------------------------------------------------------------------------------------------------------------
      '--- Carico l' immagine nella form se da cartella immagini o da percorso specifico
      '------------------------------------------------------------------------------------------------------------
      Try
        fs = New System.IO.FileStream(oCleArti.strImageDir & "\" & edAr_gif1.Text, IO.FileMode.Open, IO.FileAccess.Read)
        imArtGif.Image = System.Drawing.Image.FromStream(fs)
      Catch ex As Exception
        Try
          fs = New System.IO.FileStream(edAr_gif1.Text, IO.FileMode.Open, IO.FileAccess.Read)
          imArtGif.Image = System.Drawing.Image.FromStream(fs)
        Catch ex2 As Exception
        End Try
      End Try
      '------------------------------------------------------------------------------------------------------------
      If IsNothing(imArtGif.Image) Then
        imArtGif.Visible = False
      Else
        GctlSetVisEnab(imArtGif, True)
      End If
      '------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

      tsArti.SelectedTabPage = NtsTabPage5
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
    Dim dttTmp As New DataTable
    Try
      'mi posiziono sul tab 'listini', imposto il combo su 'specifico cli/arti', 
      'setto la data validità, imposto la promozione, valuta fissa = 0 e cerco di
      'posizionarmi sulla riga con listino cli/arti con cli e arti = quello passato da bnxxscon
      strT = e.Message.Split("§"c)

      tsArti.SelectedTabPage = NtsTabPage4
      ceListini.ckPromo.Checked = CBool(strT(2))
      ceListini.opValDay.Checked = True
      ceListini.edDtval.Text = strT(4)
      ceListini.edCodvalu.Text = "0"
      ceListini.opValEur.Checked = True
      ceListini.cbTipolistino.SelectedValue = "C"
      oMenu.ValCodiceDb(strT(0), DittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then ceListini.cbTipolistino.SelectedValue = dttTmp.Rows(0)!an_tipo.ToString
      dttTmp.Clear()
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
End Class