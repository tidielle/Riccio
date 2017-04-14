#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGSTBO
#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
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
#End Region

#Region "Variabili"
  Public oCleStbo As CLEMGSTBO
  Public dsStbo As DataSet
  Public oCallParams As CLE__CLDP
  Public dcStbo As BindingSource = New BindingSource

  Public dttCampi As New DataTable          'elenco campi filtrabili di TESTORD
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource

  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri
  Public Const MAX_ROWS_COUNT As Integer = 1000

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPdf As NTSInformatica.NTSBarButtonItem
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmSelAnnoStag As NTSInformatica.NTSGroupBox
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDatordfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatordini As NTSInformatica.NTSLabel
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbCommecafin As NTSInformatica.NTSLabel
  Public WithEvents edCommecafin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCommecaini As NTSInformatica.NTSLabel
  Public WithEvents lbContoini As NTSInformatica.NTSLabel
  Public WithEvents lbNumordini As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edCommecaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDanumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents ckSelAnnoStag As NTSInformatica.NTSCheckBox
  Public WithEvents lbNumordfin As NTSInformatica.NTSLabel
  Public WithEvents edAnumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodstag As NTSInformatica.NTSLabel
  Public WithEvents edCodstag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnotco As NTSInformatica.NTSLabel
  Public WithEvents edAnnotco As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbContofin As NTSInformatica.NTSLabel
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesagen As NTSInformatica.NTSLabel
  Public WithEvents lbDescodcfam As NTSInformatica.NTSLabel
  Public WithEvents lbDescodstag As NTSInformatica.NTSLabel
  Public WithEvents pnUp As NTSInformatica.NTSPanel
  Public WithEvents pnDown As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents tsConf As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnFiltri2 As NTSInformatica.NTSPanel
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents cmdLock As NTSInformatica.NTSButton
  Public WithEvents fmFlcont As NTSInformatica.NTSGroupBox
  Public WithEvents opFlcontEntrambe As NTSInformatica.NTSRadioButton
  Public WithEvents opFlcontNo As NTSInformatica.NTSRadioButton
  Public WithEvents opFlcontSi As NTSInformatica.NTSRadioButton
  Public WithEvents fmVistati As NTSInformatica.NTSGroupBox
  Public WithEvents opVistatiEntrambi As NTSInformatica.NTSRadioButton
  Public WithEvents opVistatiNo As NTSInformatica.NTSRadioButton
  Public WithEvents opVistatiSi As NTSInformatica.NTSRadioButton
  Public WithEvents fmBolleFatturate As NTSInformatica.NTSGroupBox
  Public WithEvents opBolleFatturateEntrambe As NTSInformatica.NTSRadioButton
  Public WithEvents opBolleFatturateNo As NTSInformatica.NTSRadioButton
  Public WithEvents opBolleFatturateSi As NTSInformatica.NTSRadioButton
  Public WithEvents fmSeldocumenti As NTSInformatica.NTSGroupBox
  Public WithEvents ckSeldocumenti As NTSInformatica.NTSCheckBox
  Public WithEvents ckReport As NTSInformatica.NTSCheckBox
  Public WithEvents opNoteprel As NTSInformatica.NTSRadioButton
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents opTipork As NTSInformatica.NTSRadioButton
  Public WithEvents fmTipoStampa As NTSInformatica.NTSGroupBox
  Public WithEvents opTipoStampaCompleta As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoStampaRidotta As NTSInformatica.NTSRadioButton
  Public WithEvents fmTippaga As NTSInformatica.NTSGroupBox
  Public WithEvents opTutti As NTSInformatica.NTSRadioButton
  Public WithEvents cbTippaga As NTSInformatica.NTSComboBox
  Public WithEvents opSpecifico As NTSInformatica.NTSRadioButton
  Public WithEvents lbDesTipobf As NTSInformatica.NTSLabel
  Public WithEvents lbDesdest As NTSInformatica.NTSLabel
  Public WithEvents lbDesagen2 As NTSInformatica.NTSLabel
  Public WithEvents lbTipobf As NTSInformatica.NTSLabel
  Public WithEvents edTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodagen2 As NTSInformatica.NTSLabel
  Public WithEvents edCodagen2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckFlevas As NTSInformatica.NTSCheckBox
  Public WithEvents pnDownLeft As NTSInformatica.NTSPanel
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSTBO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.edDanumdoc = New NTSInformatica.NTSTextBoxNum
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.edCommecaini = New NTSInformatica.NTSTextBoxNum
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbNumordini = New NTSInformatica.NTSLabel
    Me.lbContoini = New NTSInformatica.NTSLabel
    Me.lbCommecaini = New NTSInformatica.NTSLabel
    Me.lbCommecafin = New NTSInformatica.NTSLabel
    Me.edCommecafin = New NTSInformatica.NTSTextBoxNum
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatordini = New NTSInformatica.NTSLabel
    Me.lbDatordfin = New NTSInformatica.NTSLabel
    Me.lbCodagen = New NTSInformatica.NTSLabel
    Me.edCodagen = New NTSInformatica.NTSTextBoxNum
    Me.fmSelAnnoStag = New NTSInformatica.NTSGroupBox
    Me.lbDescodstag = New NTSInformatica.NTSLabel
    Me.lbCodstag = New NTSInformatica.NTSLabel
    Me.edCodstag = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnotco = New NTSInformatica.NTSLabel
    Me.edAnnotco = New NTSInformatica.NTSTextBoxNum
    Me.ckSelAnnoStag = New NTSInformatica.NTSCheckBox
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbNumordfin = New NTSInformatica.NTSLabel
    Me.edAnumdoc = New NTSInformatica.NTSTextBoxNum
    Me.lbContofin = New NTSInformatica.NTSLabel
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesagen = New NTSInformatica.NTSLabel
    Me.lbDescodcfam = New NTSInformatica.NTSLabel
    Me.pnDown = New NTSInformatica.NTSPanel
    Me.edCodlsel = New NTSInformatica.NTSTextBoxNum
    Me.lbDescodlsel = New NTSInformatica.NTSLabel
    Me.lbCodlsel = New NTSInformatica.NTSLabel
    Me.pnDownLeft = New NTSInformatica.NTSPanel
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.fmTipoStampa = New NTSInformatica.NTSGroupBox
    Me.opTipoStampaCompleta = New NTSInformatica.NTSRadioButton
    Me.opTipoStampaRidotta = New NTSInformatica.NTSRadioButton
    Me.fmTippaga = New NTSInformatica.NTSGroupBox
    Me.opTutti = New NTSInformatica.NTSRadioButton
    Me.cbTippaga = New NTSInformatica.NTSComboBox
    Me.opSpecifico = New NTSInformatica.NTSRadioButton
    Me.lbDesTipobf = New NTSInformatica.NTSLabel
    Me.lbDesdest = New NTSInformatica.NTSLabel
    Me.lbDesagen2 = New NTSInformatica.NTSLabel
    Me.lbTipobf = New NTSInformatica.NTSLabel
    Me.edTipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddest = New NTSInformatica.NTSLabel
    Me.edCoddest = New NTSInformatica.NTSTextBoxNum
    Me.lbCodagen2 = New NTSInformatica.NTSLabel
    Me.edCodagen2 = New NTSInformatica.NTSTextBoxNum
    Me.pnUp = New NTSInformatica.NTSPanel
    Me.fmSeldocumenti = New NTSInformatica.NTSGroupBox
    Me.ckFlevas = New NTSInformatica.NTSCheckBox
    Me.ckReport = New NTSInformatica.NTSCheckBox
    Me.opNoteprel = New NTSInformatica.NTSRadioButton
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.ckSeldocumenti = New NTSInformatica.NTSCheckBox
    Me.opTipork = New NTSInformatica.NTSRadioButton
    Me.fmFlcont = New NTSInformatica.NTSGroupBox
    Me.opFlcontEntrambe = New NTSInformatica.NTSRadioButton
    Me.opFlcontNo = New NTSInformatica.NTSRadioButton
    Me.opFlcontSi = New NTSInformatica.NTSRadioButton
    Me.fmVistati = New NTSInformatica.NTSGroupBox
    Me.opVistatiEntrambi = New NTSInformatica.NTSRadioButton
    Me.opVistatiNo = New NTSInformatica.NTSRadioButton
    Me.opVistatiSi = New NTSInformatica.NTSRadioButton
    Me.fmBolleFatturate = New NTSInformatica.NTSGroupBox
    Me.opBolleFatturateEntrambe = New NTSInformatica.NTSRadioButton
    Me.opBolleFatturateNo = New NTSInformatica.NTSRadioButton
    Me.opBolleFatturateSi = New NTSInformatica.NTSRadioButton
    Me.tsConf = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnFiltri2 = New NTSInformatica.NTSPanel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdApriFiltri = New NTSInformatica.NTSButton
    Me.cbFiltro = New NTSInformatica.NTSComboBox
    Me.lbFiltri = New NTSInformatica.NTSLabel
    Me.pnFiltriExt = New NTSInformatica.NTSPanel
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSelAnnoStag, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSelAnnoStag.SuspendLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDown, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDown.SuspendLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDownLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDownLeft.SuspendLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoStampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoStampa.SuspendLayout()
    CType(Me.opTipoStampaCompleta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampaRidotta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTippaga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTippaga.SuspendLayout()
    CType(Me.opTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTippaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSpecifico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagen2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnUp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnUp.SuspendLayout()
    CType(Me.fmSeldocumenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSeldocumenti.SuspendLayout()
    CType(Me.ckFlevas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opNoteprel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSeldocumenti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFlcont, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFlcont.SuspendLayout()
    CType(Me.opFlcontEntrambe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlcontNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlcontSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmVistati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmVistati.SuspendLayout()
    CType(Me.opVistatiEntrambi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opVistatiNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opVistatiSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmBolleFatturate, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmBolleFatturate.SuspendLayout()
    CType(Me.opBolleFatturateEntrambe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBolleFatturateNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBolleFatturateSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsConf, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsConf.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri2.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFiltriExt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltriExt.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbStampaPdf})
    Me.NtsBarManager1.MaxItemId = 29
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 26
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "Stampa PDF"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.Id = 27
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = True
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
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
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(107, 4)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(64, 20)
    Me.edAnno.TabIndex = 6
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodcfam.Location = New System.Drawing.Point(107, 203)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.AutoHeight = False
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(85, 20)
    Me.edCodcfam.TabIndex = 7
    '
    'edDanumdoc
    '
    Me.edDanumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumdoc.Location = New System.Drawing.Point(107, 70)
    Me.edDanumdoc.Name = "edDanumdoc"
    Me.edDanumdoc.NTSDbField = ""
    Me.edDanumdoc.NTSFormat = "0"
    Me.edDanumdoc.NTSForzaVisZoom = False
    Me.edDanumdoc.NTSOldValue = ""
    Me.edDanumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDanumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDanumdoc.Properties.AutoHeight = False
    Me.edDanumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumdoc.Properties.MaxLength = 65536
    Me.edDanumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumdoc.Size = New System.Drawing.Size(85, 20)
    Me.edDanumdoc.TabIndex = 8
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.Location = New System.Drawing.Point(107, 26)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.AutoHeight = False
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(85, 20)
    Me.edDaconto.TabIndex = 9
    '
    'edCommecaini
    '
    Me.edCommecaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecaini.Location = New System.Drawing.Point(107, 92)
    Me.edCommecaini.Name = "edCommecaini"
    Me.edCommecaini.NTSDbField = ""
    Me.edCommecaini.NTSFormat = "0"
    Me.edCommecaini.NTSForzaVisZoom = False
    Me.edCommecaini.NTSOldValue = ""
    Me.edCommecaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecaini.Properties.AutoHeight = False
    Me.edCommecaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecaini.Properties.MaxLength = 65536
    Me.edCommecaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecaini.Size = New System.Drawing.Size(85, 20)
    Me.edCommecaini.TabIndex = 10
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 7)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 11
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbNumordini
    '
    Me.lbNumordini.AutoSize = True
    Me.lbNumordini.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordini.Location = New System.Drawing.Point(12, 73)
    Me.lbNumordini.Name = "lbNumordini"
    Me.lbNumordini.NTSDbField = ""
    Me.lbNumordini.Size = New System.Drawing.Size(91, 13)
    Me.lbNumordini.TabIndex = 12
    Me.lbNumordini.Text = "Dal documento n."
    Me.lbNumordini.Tooltip = ""
    Me.lbNumordini.UseMnemonic = False
    '
    'lbContoini
    '
    Me.lbContoini.AutoSize = True
    Me.lbContoini.BackColor = System.Drawing.Color.Transparent
    Me.lbContoini.Location = New System.Drawing.Point(12, 29)
    Me.lbContoini.Name = "lbContoini"
    Me.lbContoini.NTSDbField = ""
    Me.lbContoini.Size = New System.Drawing.Size(52, 13)
    Me.lbContoini.TabIndex = 13
    Me.lbContoini.Text = "Dal conto"
    Me.lbContoini.Tooltip = ""
    Me.lbContoini.UseMnemonic = False
    '
    'lbCommecaini
    '
    Me.lbCommecaini.AutoSize = True
    Me.lbCommecaini.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaini.Location = New System.Drawing.Point(12, 95)
    Me.lbCommecaini.Name = "lbCommecaini"
    Me.lbCommecaini.NTSDbField = ""
    Me.lbCommecaini.Size = New System.Drawing.Size(72, 13)
    Me.lbCommecaini.TabIndex = 14
    Me.lbCommecaini.Text = "Da commessa"
    Me.lbCommecaini.Tooltip = ""
    Me.lbCommecaini.UseMnemonic = False
    '
    'lbCommecafin
    '
    Me.lbCommecafin.AutoSize = True
    Me.lbCommecafin.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecafin.Location = New System.Drawing.Point(219, 95)
    Me.lbCommecafin.Name = "lbCommecafin"
    Me.lbCommecafin.NTSDbField = ""
    Me.lbCommecafin.Size = New System.Drawing.Size(65, 13)
    Me.lbCommecafin.TabIndex = 16
    Me.lbCommecafin.Text = "a commessa"
    Me.lbCommecafin.Tooltip = ""
    Me.lbCommecafin.UseMnemonic = False
    '
    'edCommecafin
    '
    Me.edCommecafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecafin.Location = New System.Drawing.Point(318, 92)
    Me.edCommecafin.Name = "edCommecafin"
    Me.edCommecafin.NTSDbField = ""
    Me.edCommecafin.NTSFormat = "0"
    Me.edCommecafin.NTSForzaVisZoom = False
    Me.edCommecafin.NTSOldValue = ""
    Me.edCommecafin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecafin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecafin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecafin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecafin.Properties.AutoHeight = False
    Me.edCommecafin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecafin.Properties.MaxLength = 65536
    Me.edCommecafin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecafin.Size = New System.Drawing.Size(85, 20)
    Me.edCommecafin.TabIndex = 15
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.Location = New System.Drawing.Point(107, 48)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.AutoHeight = False
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(85, 20)
    Me.edDatini.TabIndex = 17
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.Location = New System.Drawing.Point(318, 48)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatfin.Properties.AutoHeight = False
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(85, 20)
    Me.edDatfin.TabIndex = 18
    '
    'lbDatordini
    '
    Me.lbDatordini.AutoSize = True
    Me.lbDatordini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordini.Location = New System.Drawing.Point(12, 51)
    Me.lbDatordini.Name = "lbDatordini"
    Me.lbDatordini.NTSDbField = ""
    Me.lbDatordini.Size = New System.Drawing.Size(55, 13)
    Me.lbDatordini.TabIndex = 19
    Me.lbDatordini.Text = "Dalla data"
    Me.lbDatordini.Tooltip = ""
    Me.lbDatordini.UseMnemonic = False
    '
    'lbDatordfin
    '
    Me.lbDatordfin.AutoSize = True
    Me.lbDatordfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordfin.Location = New System.Drawing.Point(219, 51)
    Me.lbDatordfin.Name = "lbDatordfin"
    Me.lbDatordfin.NTSDbField = ""
    Me.lbDatordfin.Size = New System.Drawing.Size(48, 13)
    Me.lbDatordfin.TabIndex = 20
    Me.lbDatordfin.Text = "alla data"
    Me.lbDatordfin.Tooltip = ""
    Me.lbDatordfin.UseMnemonic = False
    '
    'lbCodagen
    '
    Me.lbCodagen.AutoSize = True
    Me.lbCodagen.BackColor = System.Drawing.Color.Transparent
    Me.lbCodagen.Location = New System.Drawing.Point(12, 140)
    Me.lbCodagen.Name = "lbCodagen"
    Me.lbCodagen.NTSDbField = ""
    Me.lbCodagen.Size = New System.Drawing.Size(76, 13)
    Me.lbCodagen.TabIndex = 34
    Me.lbCodagen.Text = "Codice agente"
    Me.lbCodagen.Tooltip = ""
    Me.lbCodagen.UseMnemonic = False
    '
    'edCodagen
    '
    Me.edCodagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagen.Location = New System.Drawing.Point(107, 137)
    Me.edCodagen.Name = "edCodagen"
    Me.edCodagen.NTSDbField = ""
    Me.edCodagen.NTSFormat = "0"
    Me.edCodagen.NTSForzaVisZoom = False
    Me.edCodagen.NTSOldValue = ""
    Me.edCodagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodagen.Properties.AutoHeight = False
    Me.edCodagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagen.Properties.MaxLength = 65536
    Me.edCodagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagen.Size = New System.Drawing.Size(85, 20)
    Me.edCodagen.TabIndex = 33
    '
    'fmSelAnnoStag
    '
    Me.fmSelAnnoStag.AllowDrop = True
    Me.fmSelAnnoStag.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSelAnnoStag.Appearance.Options.UseBackColor = True
    Me.fmSelAnnoStag.Controls.Add(Me.lbDescodstag)
    Me.fmSelAnnoStag.Controls.Add(Me.lbCodstag)
    Me.fmSelAnnoStag.Controls.Add(Me.edCodstag)
    Me.fmSelAnnoStag.Controls.Add(Me.lbAnnotco)
    Me.fmSelAnnoStag.Controls.Add(Me.edAnnotco)
    Me.fmSelAnnoStag.Controls.Add(Me.ckSelAnnoStag)
    Me.fmSelAnnoStag.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSelAnnoStag.Location = New System.Drawing.Point(10, 264)
    Me.fmSelAnnoStag.Name = "fmSelAnnoStag"
    Me.fmSelAnnoStag.Size = New System.Drawing.Size(665, 86)
    Me.fmSelAnnoStag.TabIndex = 37
    '
    'lbDescodstag
    '
    Me.lbDescodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodstag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodstag.Location = New System.Drawing.Point(188, 58)
    Me.lbDescodstag.Name = "lbDescodstag"
    Me.lbDescodstag.NTSDbField = ""
    Me.lbDescodstag.Size = New System.Drawing.Size(469, 20)
    Me.lbDescodstag.TabIndex = 50
    Me.lbDescodstag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodstag.Tooltip = ""
    Me.lbDescodstag.UseMnemonic = False
    '
    'lbCodstag
    '
    Me.lbCodstag.AutoSize = True
    Me.lbCodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodstag.Location = New System.Drawing.Point(13, 61)
    Me.lbCodstag.Name = "lbCodstag"
    Me.lbCodstag.NTSDbField = ""
    Me.lbCodstag.Size = New System.Drawing.Size(49, 13)
    Me.lbCodstag.TabIndex = 42
    Me.lbCodstag.Text = "Stagione"
    Me.lbCodstag.Tooltip = ""
    Me.lbCodstag.UseMnemonic = False
    '
    'edCodstag
    '
    Me.edCodstag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodstag.Location = New System.Drawing.Point(97, 58)
    Me.edCodstag.Name = "edCodstag"
    Me.edCodstag.NTSDbField = ""
    Me.edCodstag.NTSFormat = "0"
    Me.edCodstag.NTSForzaVisZoom = False
    Me.edCodstag.NTSOldValue = ""
    Me.edCodstag.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodstag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodstag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodstag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodstag.Properties.AutoHeight = False
    Me.edCodstag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodstag.Properties.MaxLength = 65536
    Me.edCodstag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodstag.Size = New System.Drawing.Size(85, 20)
    Me.edCodstag.TabIndex = 41
    '
    'lbAnnotco
    '
    Me.lbAnnotco.AutoSize = True
    Me.lbAnnotco.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnotco.Location = New System.Drawing.Point(13, 35)
    Me.lbAnnotco.Name = "lbAnnotco"
    Me.lbAnnotco.NTSDbField = ""
    Me.lbAnnotco.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnotco.TabIndex = 40
    Me.lbAnnotco.Text = "Anno"
    Me.lbAnnotco.Tooltip = ""
    Me.lbAnnotco.UseMnemonic = False
    '
    'edAnnotco
    '
    Me.edAnnotco.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAnnotco.Location = New System.Drawing.Point(97, 32)
    Me.edAnnotco.Name = "edAnnotco"
    Me.edAnnotco.NTSDbField = ""
    Me.edAnnotco.NTSFormat = "0"
    Me.edAnnotco.NTSForzaVisZoom = False
    Me.edAnnotco.NTSOldValue = ""
    Me.edAnnotco.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnotco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnotco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnotco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnotco.Properties.AutoHeight = False
    Me.edAnnotco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnotco.Properties.MaxLength = 65536
    Me.edAnnotco.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnotco.Size = New System.Drawing.Size(85, 20)
    Me.edAnnotco.TabIndex = 39
    '
    'ckSelAnnoStag
    '
    Me.ckSelAnnoStag.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSelAnnoStag.Location = New System.Drawing.Point(5, 0)
    Me.ckSelAnnoStag.Name = "ckSelAnnoStag"
    Me.ckSelAnnoStag.NTSCheckValue = "S"
    Me.ckSelAnnoStag.NTSUnCheckValue = "N"
    Me.ckSelAnnoStag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSelAnnoStag.Properties.Appearance.Options.UseBackColor = True
    Me.ckSelAnnoStag.Properties.AutoHeight = False
    Me.ckSelAnnoStag.Properties.Caption = "Seleziona Anno/Stagione"
    Me.ckSelAnnoStag.Size = New System.Drawing.Size(145, 19)
    Me.ckSelAnnoStag.TabIndex = 38
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(12, 206)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(72, 13)
    Me.lbCodcfam.TabIndex = 38
    Me.lbCodcfam.Text = "Linea/famiglia"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(318, 4)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 39
    '
    'lbNumordfin
    '
    Me.lbNumordfin.AutoSize = True
    Me.lbNumordfin.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordfin.Location = New System.Drawing.Point(219, 73)
    Me.lbNumordfin.Name = "lbNumordfin"
    Me.lbNumordfin.NTSDbField = ""
    Me.lbNumordfin.Size = New System.Drawing.Size(84, 13)
    Me.lbNumordfin.TabIndex = 42
    Me.lbNumordfin.Text = "al documento n."
    Me.lbNumordfin.Tooltip = ""
    Me.lbNumordfin.UseMnemonic = False
    '
    'edAnumdoc
    '
    Me.edAnumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnumdoc.Location = New System.Drawing.Point(318, 70)
    Me.edAnumdoc.Name = "edAnumdoc"
    Me.edAnumdoc.NTSDbField = ""
    Me.edAnumdoc.NTSFormat = "0"
    Me.edAnumdoc.NTSForzaVisZoom = False
    Me.edAnumdoc.NTSOldValue = ""
    Me.edAnumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnumdoc.Properties.AutoHeight = False
    Me.edAnumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnumdoc.Properties.MaxLength = 65536
    Me.edAnumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnumdoc.Size = New System.Drawing.Size(85, 20)
    Me.edAnumdoc.TabIndex = 41
    '
    'lbContofin
    '
    Me.lbContofin.AutoSize = True
    Me.lbContofin.BackColor = System.Drawing.Color.Transparent
    Me.lbContofin.Location = New System.Drawing.Point(219, 29)
    Me.lbContofin.Name = "lbContofin"
    Me.lbContofin.NTSDbField = ""
    Me.lbContofin.Size = New System.Drawing.Size(45, 13)
    Me.lbContofin.TabIndex = 48
    Me.lbContofin.Text = "al conto"
    Me.lbContofin.Tooltip = ""
    Me.lbContofin.UseMnemonic = False
    '
    'edAconto
    '
    Me.edAconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAconto.Location = New System.Drawing.Point(318, 26)
    Me.edAconto.Name = "edAconto"
    Me.edAconto.NTSDbField = ""
    Me.edAconto.NTSFormat = "0"
    Me.edAconto.NTSForzaVisZoom = False
    Me.edAconto.NTSOldValue = ""
    Me.edAconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAconto.Properties.AutoHeight = False
    Me.edAconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAconto.Properties.MaxLength = 65536
    Me.edAconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAconto.Size = New System.Drawing.Size(85, 20)
    Me.edAconto.TabIndex = 47
    '
    'lbDesagen
    '
    Me.lbDesagen.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagen.Location = New System.Drawing.Point(198, 137)
    Me.lbDesagen.Name = "lbDesagen"
    Me.lbDesagen.NTSDbField = ""
    Me.lbDesagen.Size = New System.Drawing.Size(469, 20)
    Me.lbDesagen.TabIndex = 49
    Me.lbDesagen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesagen.Tooltip = ""
    Me.lbDesagen.UseMnemonic = False
    '
    'lbDescodcfam
    '
    Me.lbDescodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodcfam.Location = New System.Drawing.Point(198, 203)
    Me.lbDescodcfam.Name = "lbDescodcfam"
    Me.lbDescodcfam.NTSDbField = ""
    Me.lbDescodcfam.Size = New System.Drawing.Size(469, 20)
    Me.lbDescodcfam.TabIndex = 51
    Me.lbDescodcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodcfam.Tooltip = ""
    Me.lbDescodcfam.UseMnemonic = False
    '
    'pnDown
    '
    Me.pnDown.AllowDrop = True
    Me.pnDown.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDown.Appearance.Options.UseBackColor = True
    Me.pnDown.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDown.Controls.Add(Me.edCodlsel)
    Me.pnDown.Controls.Add(Me.lbDescodlsel)
    Me.pnDown.Controls.Add(Me.lbCodlsel)
    Me.pnDown.Controls.Add(Me.pnDownLeft)
    Me.pnDown.Controls.Add(Me.fmTipoStampa)
    Me.pnDown.Controls.Add(Me.fmTippaga)
    Me.pnDown.Controls.Add(Me.lbDescodcfam)
    Me.pnDown.Controls.Add(Me.edCodcfam)
    Me.pnDown.Controls.Add(Me.lbDesTipobf)
    Me.pnDown.Controls.Add(Me.lbDesdest)
    Me.pnDown.Controls.Add(Me.lbDesagen2)
    Me.pnDown.Controls.Add(Me.lbDesagen)
    Me.pnDown.Controls.Add(Me.lbCodcfam)
    Me.pnDown.Controls.Add(Me.fmSelAnnoStag)
    Me.pnDown.Controls.Add(Me.lbTipobf)
    Me.pnDown.Controls.Add(Me.edTipobf)
    Me.pnDown.Controls.Add(Me.lbCoddest)
    Me.pnDown.Controls.Add(Me.edCoddest)
    Me.pnDown.Controls.Add(Me.lbCodagen2)
    Me.pnDown.Controls.Add(Me.edCodagen2)
    Me.pnDown.Controls.Add(Me.lbCodagen)
    Me.pnDown.Controls.Add(Me.edCodagen)
    Me.pnDown.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDown.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDown.Location = New System.Drawing.Point(0, 0)
    Me.pnDown.Name = "pnDown"
    Me.pnDown.NTSActiveTrasparency = True
    Me.pnDown.Size = New System.Drawing.Size(686, 356)
    Me.pnDown.TabIndex = 54
    Me.pnDown.Text = "NtsPanel1"
    '
    'edCodlsel
    '
    Me.edCodlsel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlsel.Location = New System.Drawing.Point(107, 224)
    Me.edCodlsel.Name = "edCodlsel"
    Me.edCodlsel.NTSDbField = ""
    Me.edCodlsel.NTSFormat = "0"
    Me.edCodlsel.NTSForzaVisZoom = False
    Me.edCodlsel.NTSOldValue = ""
    Me.edCodlsel.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlsel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlsel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlsel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlsel.Properties.AutoHeight = False
    Me.edCodlsel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlsel.Properties.MaxLength = 65536
    Me.edCodlsel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlsel.Size = New System.Drawing.Size(85, 20)
    Me.edCodlsel.TabIndex = 59
    '
    'lbDescodlsel
    '
    Me.lbDescodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsel.Location = New System.Drawing.Point(198, 224)
    Me.lbDescodlsel.Name = "lbDescodlsel"
    Me.lbDescodlsel.NTSDbField = ""
    Me.lbDescodlsel.Size = New System.Drawing.Size(469, 20)
    Me.lbDescodlsel.TabIndex = 58
    Me.lbDescodlsel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodlsel.Tooltip = ""
    Me.lbDescodlsel.UseMnemonic = False
    '
    'lbCodlsel
    '
    Me.lbCodlsel.AutoSize = True
    Me.lbCodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodlsel.Location = New System.Drawing.Point(12, 228)
    Me.lbCodlsel.Name = "lbCodlsel"
    Me.lbCodlsel.NTSDbField = ""
    Me.lbCodlsel.Size = New System.Drawing.Size(86, 13)
    Me.lbCodlsel.TabIndex = 57
    Me.lbCodlsel.Text = "Lista selezionata"
    Me.lbCodlsel.Tooltip = ""
    Me.lbCodlsel.UseMnemonic = False
    '
    'pnDownLeft
    '
    Me.pnDownLeft.AllowDrop = True
    Me.pnDownLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDownLeft.Appearance.Options.UseBackColor = True
    Me.pnDownLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDownLeft.Controls.Add(Me.ckSerie)
    Me.pnDownLeft.Controls.Add(Me.edAnno)
    Me.pnDownLeft.Controls.Add(Me.lbDatordfin)
    Me.pnDownLeft.Controls.Add(Me.lbDatordini)
    Me.pnDownLeft.Controls.Add(Me.edSerie)
    Me.pnDownLeft.Controls.Add(Me.edDatfin)
    Me.pnDownLeft.Controls.Add(Me.edDanumdoc)
    Me.pnDownLeft.Controls.Add(Me.edDatini)
    Me.pnDownLeft.Controls.Add(Me.edAnumdoc)
    Me.pnDownLeft.Controls.Add(Me.lbCommecafin)
    Me.pnDownLeft.Controls.Add(Me.lbNumordfin)
    Me.pnDownLeft.Controls.Add(Me.edCommecafin)
    Me.pnDownLeft.Controls.Add(Me.edDaconto)
    Me.pnDownLeft.Controls.Add(Me.lbCommecaini)
    Me.pnDownLeft.Controls.Add(Me.lbContofin)
    Me.pnDownLeft.Controls.Add(Me.lbContoini)
    Me.pnDownLeft.Controls.Add(Me.edCommecaini)
    Me.pnDownLeft.Controls.Add(Me.lbNumordini)
    Me.pnDownLeft.Controls.Add(Me.edAconto)
    Me.pnDownLeft.Controls.Add(Me.lbAnno)
    Me.pnDownLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDownLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnDownLeft.Name = "pnDownLeft"
    Me.pnDownLeft.NTSActiveTrasparency = True
    Me.pnDownLeft.Size = New System.Drawing.Size(430, 114)
    Me.pnDownLeft.TabIndex = 55
    Me.pnDownLeft.Text = "NtsPanel1"
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(222, 5)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(62, 19)
    Me.ckSerie.TabIndex = 49
    '
    'fmTipoStampa
    '
    Me.fmTipoStampa.AllowDrop = True
    Me.fmTipoStampa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoStampa.Appearance.Options.UseBackColor = True
    Me.fmTipoStampa.Controls.Add(Me.opTipoStampaCompleta)
    Me.fmTipoStampa.Controls.Add(Me.opTipoStampaRidotta)
    Me.fmTipoStampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoStampa.Location = New System.Drawing.Point(442, 67)
    Me.fmTipoStampa.Name = "fmTipoStampa"
    Me.fmTipoStampa.Size = New System.Drawing.Size(233, 45)
    Me.fmTipoStampa.TabIndex = 46
    Me.fmTipoStampa.Text = "Tipo di stampa:"
    '
    'opTipoStampaCompleta
    '
    Me.opTipoStampaCompleta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampaCompleta.Location = New System.Drawing.Point(111, 23)
    Me.opTipoStampaCompleta.Name = "opTipoStampaCompleta"
    Me.opTipoStampaCompleta.NTSCheckValue = "S"
    Me.opTipoStampaCompleta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampaCompleta.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampaCompleta.Properties.AutoHeight = False
    Me.opTipoStampaCompleta.Properties.Caption = "Completa"
    Me.opTipoStampaCompleta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampaCompleta.Size = New System.Drawing.Size(100, 19)
    Me.opTipoStampaCompleta.TabIndex = 1
    '
    'opTipoStampaRidotta
    '
    Me.opTipoStampaRidotta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampaRidotta.EditValue = True
    Me.opTipoStampaRidotta.Location = New System.Drawing.Point(9, 23)
    Me.opTipoStampaRidotta.Name = "opTipoStampaRidotta"
    Me.opTipoStampaRidotta.NTSCheckValue = "S"
    Me.opTipoStampaRidotta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampaRidotta.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampaRidotta.Properties.AutoHeight = False
    Me.opTipoStampaRidotta.Properties.Caption = "Ridotta"
    Me.opTipoStampaRidotta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampaRidotta.Size = New System.Drawing.Size(100, 19)
    Me.opTipoStampaRidotta.TabIndex = 0
    '
    'fmTippaga
    '
    Me.fmTippaga.AllowDrop = True
    Me.fmTippaga.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTippaga.Appearance.Options.UseBackColor = True
    Me.fmTippaga.Controls.Add(Me.opTutti)
    Me.fmTippaga.Controls.Add(Me.cbTippaga)
    Me.fmTippaga.Controls.Add(Me.opSpecifico)
    Me.fmTippaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTippaga.Location = New System.Drawing.Point(442, 4)
    Me.fmTippaga.Name = "fmTippaga"
    Me.fmTippaga.Size = New System.Drawing.Size(233, 62)
    Me.fmTippaga.TabIndex = 54
    Me.fmTippaga.Text = "Tipo pagamento:"
    '
    'opTutti
    '
    Me.opTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutti.EditValue = True
    Me.opTutti.Location = New System.Drawing.Point(9, 43)
    Me.opTutti.Name = "opTutti"
    Me.opTutti.NTSCheckValue = "S"
    Me.opTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opTutti.Properties.AutoHeight = False
    Me.opTutti.Properties.Caption = "Tutti"
    Me.opTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutti.Size = New System.Drawing.Size(111, 19)
    Me.opTutti.TabIndex = 1
    '
    'cbTippaga
    '
    Me.cbTippaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTippaga.DataSource = Nothing
    Me.cbTippaga.DisplayMember = ""
    Me.cbTippaga.Location = New System.Drawing.Point(81, 22)
    Me.cbTippaga.Name = "cbTippaga"
    Me.cbTippaga.NTSDbField = ""
    Me.cbTippaga.Properties.AutoHeight = False
    Me.cbTippaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTippaga.Properties.DropDownRows = 30
    Me.cbTippaga.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTippaga.SelectedValue = ""
    Me.cbTippaga.Size = New System.Drawing.Size(144, 20)
    Me.cbTippaga.TabIndex = 5
    Me.cbTippaga.ValueMember = ""
    '
    'opSpecifico
    '
    Me.opSpecifico.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSpecifico.Location = New System.Drawing.Point(9, 23)
    Me.opSpecifico.Name = "opSpecifico"
    Me.opSpecifico.NTSCheckValue = "S"
    Me.opSpecifico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSpecifico.Properties.Appearance.Options.UseBackColor = True
    Me.opSpecifico.Properties.AutoHeight = False
    Me.opSpecifico.Properties.Caption = "Specifico"
    Me.opSpecifico.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSpecifico.Size = New System.Drawing.Size(83, 19)
    Me.opSpecifico.TabIndex = 0
    '
    'lbDesTipobf
    '
    Me.lbDesTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbDesTipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesTipobf.Location = New System.Drawing.Point(198, 115)
    Me.lbDesTipobf.Name = "lbDesTipobf"
    Me.lbDesTipobf.NTSDbField = ""
    Me.lbDesTipobf.Size = New System.Drawing.Size(469, 20)
    Me.lbDesTipobf.TabIndex = 49
    Me.lbDesTipobf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesTipobf.Tooltip = ""
    Me.lbDesTipobf.UseMnemonic = False
    '
    'lbDesdest
    '
    Me.lbDesdest.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdest.Location = New System.Drawing.Point(198, 181)
    Me.lbDesdest.Name = "lbDesdest"
    Me.lbDesdest.NTSDbField = ""
    Me.lbDesdest.Size = New System.Drawing.Size(469, 20)
    Me.lbDesdest.TabIndex = 49
    Me.lbDesdest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesdest.Tooltip = ""
    Me.lbDesdest.UseMnemonic = False
    '
    'lbDesagen2
    '
    Me.lbDesagen2.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagen2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagen2.Location = New System.Drawing.Point(198, 159)
    Me.lbDesagen2.Name = "lbDesagen2"
    Me.lbDesagen2.NTSDbField = ""
    Me.lbDesagen2.Size = New System.Drawing.Size(469, 20)
    Me.lbDesagen2.TabIndex = 49
    Me.lbDesagen2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesagen2.Tooltip = ""
    Me.lbDesagen2.UseMnemonic = False
    '
    'lbTipobf
    '
    Me.lbTipobf.AutoSize = True
    Me.lbTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobf.Location = New System.Drawing.Point(12, 118)
    Me.lbTipobf.Name = "lbTipobf"
    Me.lbTipobf.NTSDbField = ""
    Me.lbTipobf.Size = New System.Drawing.Size(90, 13)
    Me.lbTipobf.TabIndex = 34
    Me.lbTipobf.Text = "Tipo bolla/fattura"
    Me.lbTipobf.Tooltip = ""
    Me.lbTipobf.UseMnemonic = False
    '
    'edTipobf
    '
    Me.edTipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipobf.Location = New System.Drawing.Point(107, 115)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipobf.Properties.AutoHeight = False
    Me.edTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipobf.Properties.MaxLength = 65536
    Me.edTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipobf.Size = New System.Drawing.Size(85, 20)
    Me.edTipobf.TabIndex = 33
    '
    'lbCoddest
    '
    Me.lbCoddest.AutoSize = True
    Me.lbCoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddest.Location = New System.Drawing.Point(12, 184)
    Me.lbCoddest.Name = "lbCoddest"
    Me.lbCoddest.NTSDbField = ""
    Me.lbCoddest.Size = New System.Drawing.Size(68, 13)
    Me.lbCoddest.TabIndex = 34
    Me.lbCoddest.Text = "Destinazione"
    Me.lbCoddest.Tooltip = ""
    Me.lbCoddest.UseMnemonic = False
    '
    'edCoddest
    '
    Me.edCoddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest.Location = New System.Drawing.Point(107, 181)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSFormat = "0"
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = ""
    Me.edCoddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.AutoHeight = False
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.Properties.MaxLength = 65536
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(85, 20)
    Me.edCoddest.TabIndex = 33
    '
    'lbCodagen2
    '
    Me.lbCodagen2.AutoSize = True
    Me.lbCodagen2.BackColor = System.Drawing.Color.Transparent
    Me.lbCodagen2.Location = New System.Drawing.Point(12, 162)
    Me.lbCodagen2.Name = "lbCodagen2"
    Me.lbCodagen2.NTSDbField = ""
    Me.lbCodagen2.Size = New System.Drawing.Size(85, 13)
    Me.lbCodagen2.TabIndex = 34
    Me.lbCodagen2.Text = "Codice agente 2"
    Me.lbCodagen2.Tooltip = ""
    Me.lbCodagen2.UseMnemonic = False
    '
    'edCodagen2
    '
    Me.edCodagen2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagen2.Location = New System.Drawing.Point(107, 159)
    Me.edCodagen2.Name = "edCodagen2"
    Me.edCodagen2.NTSDbField = ""
    Me.edCodagen2.NTSFormat = "0"
    Me.edCodagen2.NTSForzaVisZoom = False
    Me.edCodagen2.NTSOldValue = ""
    Me.edCodagen2.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagen2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagen2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodagen2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodagen2.Properties.AutoHeight = False
    Me.edCodagen2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagen2.Properties.MaxLength = 65536
    Me.edCodagen2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagen2.Size = New System.Drawing.Size(85, 20)
    Me.edCodagen2.TabIndex = 33
    '
    'pnUp
    '
    Me.pnUp.AllowDrop = True
    Me.pnUp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnUp.Appearance.Options.UseBackColor = True
    Me.pnUp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnUp.Controls.Add(Me.fmSeldocumenti)
    Me.pnUp.Controls.Add(Me.fmFlcont)
    Me.pnUp.Controls.Add(Me.fmVistati)
    Me.pnUp.Controls.Add(Me.fmBolleFatturate)
    Me.pnUp.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnUp.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnUp.Location = New System.Drawing.Point(0, 62)
    Me.pnUp.Name = "pnUp"
    Me.pnUp.NTSActiveTrasparency = True
    Me.pnUp.Size = New System.Drawing.Size(695, 98)
    Me.pnUp.TabIndex = 55
    Me.pnUp.Text = "NtsPanel1"
    '
    'fmSeldocumenti
    '
    Me.fmSeldocumenti.AllowDrop = True
    Me.fmSeldocumenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSeldocumenti.Appearance.Options.UseBackColor = True
    Me.fmSeldocumenti.Controls.Add(Me.ckFlevas)
    Me.fmSeldocumenti.Controls.Add(Me.ckReport)
    Me.fmSeldocumenti.Controls.Add(Me.opNoteprel)
    Me.fmSeldocumenti.Controls.Add(Me.cbTipork)
    Me.fmSeldocumenti.Controls.Add(Me.ckSeldocumenti)
    Me.fmSeldocumenti.Controls.Add(Me.opTipork)
    Me.fmSeldocumenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSeldocumenti.Location = New System.Drawing.Point(3, 7)
    Me.fmSeldocumenti.Name = "fmSeldocumenti"
    Me.fmSeldocumenti.Size = New System.Drawing.Size(341, 87)
    Me.fmSeldocumenti.TabIndex = 38
    '
    'ckFlevas
    '
    Me.ckFlevas.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFlevas.Location = New System.Drawing.Point(105, 43)
    Me.ckFlevas.Name = "ckFlevas"
    Me.ckFlevas.NTSCheckValue = "S"
    Me.ckFlevas.NTSUnCheckValue = "N"
    Me.ckFlevas.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFlevas.Properties.Appearance.Options.UseBackColor = True
    Me.ckFlevas.Properties.AutoHeight = False
    Me.ckFlevas.Properties.Caption = "Solo non evase"
    Me.ckFlevas.Size = New System.Drawing.Size(145, 19)
    Me.ckFlevas.TabIndex = 38
    '
    'ckReport
    '
    Me.ckReport.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckReport.Location = New System.Drawing.Point(9, 63)
    Me.ckReport.Name = "ckReport"
    Me.ckReport.NTSCheckValue = "S"
    Me.ckReport.NTSUnCheckValue = "N"
    Me.ckReport.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckReport.Properties.Appearance.Options.UseBackColor = True
    Me.ckReport.Properties.AutoHeight = False
    Me.ckReport.Properties.Caption = "Utilizza reports standard"
    Me.ckReport.Size = New System.Drawing.Size(145, 19)
    Me.ckReport.TabIndex = 38
    '
    'opNoteprel
    '
    Me.opNoteprel.Cursor = System.Windows.Forms.Cursors.Default
    Me.opNoteprel.Location = New System.Drawing.Point(9, 43)
    Me.opNoteprel.Name = "opNoteprel"
    Me.opNoteprel.NTSCheckValue = "S"
    Me.opNoteprel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opNoteprel.Properties.Appearance.Options.UseBackColor = True
    Me.opNoteprel.Properties.AutoHeight = False
    Me.opNoteprel.Properties.Caption = "Note di prelievo"
    Me.opNoteprel.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opNoteprel.Size = New System.Drawing.Size(111, 19)
    Me.opNoteprel.TabIndex = 1
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(107, 22)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.AutoHeight = False
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(229, 20)
    Me.cbTipork.TabIndex = 5
    Me.cbTipork.ValueMember = ""
    '
    'ckSeldocumenti
    '
    Me.ckSeldocumenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSeldocumenti.Location = New System.Drawing.Point(5, 0)
    Me.ckSeldocumenti.Name = "ckSeldocumenti"
    Me.ckSeldocumenti.NTSCheckValue = "S"
    Me.ckSeldocumenti.NTSUnCheckValue = "N"
    Me.ckSeldocumenti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSeldocumenti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSeldocumenti.Properties.AutoHeight = False
    Me.ckSeldocumenti.Properties.Caption = "Seleziona documenti:"
    Me.ckSeldocumenti.Size = New System.Drawing.Size(145, 19)
    Me.ckSeldocumenti.TabIndex = 38
    '
    'opTipork
    '
    Me.opTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipork.EditValue = True
    Me.opTipork.Location = New System.Drawing.Point(9, 23)
    Me.opTipork.Name = "opTipork"
    Me.opTipork.NTSCheckValue = "S"
    Me.opTipork.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipork.Properties.Appearance.Options.UseBackColor = True
    Me.opTipork.Properties.AutoHeight = False
    Me.opTipork.Properties.Caption = "Documento"
    Me.opTipork.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipork.Size = New System.Drawing.Size(105, 19)
    Me.opTipork.TabIndex = 0
    '
    'fmFlcont
    '
    Me.fmFlcont.AllowDrop = True
    Me.fmFlcont.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFlcont.Appearance.Options.UseBackColor = True
    Me.fmFlcont.Controls.Add(Me.opFlcontEntrambe)
    Me.fmFlcont.Controls.Add(Me.opFlcontNo)
    Me.fmFlcont.Controls.Add(Me.opFlcontSi)
    Me.fmFlcont.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFlcont.Location = New System.Drawing.Point(582, 7)
    Me.fmFlcont.Name = "fmFlcont"
    Me.fmFlcont.Size = New System.Drawing.Size(110, 87)
    Me.fmFlcont.TabIndex = 45
    Me.fmFlcont.Text = "Fatt. contabilizzate:"
    '
    'opFlcontEntrambe
    '
    Me.opFlcontEntrambe.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlcontEntrambe.EditValue = True
    Me.opFlcontEntrambe.Location = New System.Drawing.Point(9, 63)
    Me.opFlcontEntrambe.Name = "opFlcontEntrambe"
    Me.opFlcontEntrambe.NTSCheckValue = "S"
    Me.opFlcontEntrambe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontEntrambe.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontEntrambe.Properties.AutoHeight = False
    Me.opFlcontEntrambe.Properties.Caption = "Entrambe"
    Me.opFlcontEntrambe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontEntrambe.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontEntrambe.TabIndex = 2
    '
    'opFlcontNo
    '
    Me.opFlcontNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlcontNo.Location = New System.Drawing.Point(9, 43)
    Me.opFlcontNo.Name = "opFlcontNo"
    Me.opFlcontNo.NTSCheckValue = "S"
    Me.opFlcontNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontNo.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontNo.Properties.AutoHeight = False
    Me.opFlcontNo.Properties.Caption = "No"
    Me.opFlcontNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontNo.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontNo.TabIndex = 1
    '
    'opFlcontSi
    '
    Me.opFlcontSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlcontSi.Location = New System.Drawing.Point(9, 23)
    Me.opFlcontSi.Name = "opFlcontSi"
    Me.opFlcontSi.NTSCheckValue = "S"
    Me.opFlcontSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontSi.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontSi.Properties.AutoHeight = False
    Me.opFlcontSi.Properties.Caption = "Si"
    Me.opFlcontSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontSi.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontSi.TabIndex = 0
    '
    'fmVistati
    '
    Me.fmVistati.AllowDrop = True
    Me.fmVistati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmVistati.Appearance.Options.UseBackColor = True
    Me.fmVistati.Controls.Add(Me.opVistatiEntrambi)
    Me.fmVistati.Controls.Add(Me.opVistatiNo)
    Me.fmVistati.Controls.Add(Me.opVistatiSi)
    Me.fmVistati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmVistati.Location = New System.Drawing.Point(466, 7)
    Me.fmVistati.Name = "fmVistati"
    Me.fmVistati.Size = New System.Drawing.Size(110, 87)
    Me.fmVistati.TabIndex = 45
    Me.fmVistati.Text = "Vistati:"
    '
    'opVistatiEntrambi
    '
    Me.opVistatiEntrambi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opVistatiEntrambi.EditValue = True
    Me.opVistatiEntrambi.Location = New System.Drawing.Point(9, 63)
    Me.opVistatiEntrambi.Name = "opVistatiEntrambi"
    Me.opVistatiEntrambi.NTSCheckValue = "S"
    Me.opVistatiEntrambi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opVistatiEntrambi.Properties.Appearance.Options.UseBackColor = True
    Me.opVistatiEntrambi.Properties.AutoHeight = False
    Me.opVistatiEntrambi.Properties.Caption = "Entrambi"
    Me.opVistatiEntrambi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opVistatiEntrambi.Size = New System.Drawing.Size(111, 19)
    Me.opVistatiEntrambi.TabIndex = 2
    '
    'opVistatiNo
    '
    Me.opVistatiNo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opVistatiNo.Location = New System.Drawing.Point(9, 43)
    Me.opVistatiNo.Name = "opVistatiNo"
    Me.opVistatiNo.NTSCheckValue = "S"
    Me.opVistatiNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opVistatiNo.Properties.Appearance.Options.UseBackColor = True
    Me.opVistatiNo.Properties.AutoHeight = False
    Me.opVistatiNo.Properties.Caption = "No"
    Me.opVistatiNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opVistatiNo.Size = New System.Drawing.Size(111, 19)
    Me.opVistatiNo.TabIndex = 1
    '
    'opVistatiSi
    '
    Me.opVistatiSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opVistatiSi.Location = New System.Drawing.Point(9, 23)
    Me.opVistatiSi.Name = "opVistatiSi"
    Me.opVistatiSi.NTSCheckValue = "S"
    Me.opVistatiSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opVistatiSi.Properties.Appearance.Options.UseBackColor = True
    Me.opVistatiSi.Properties.AutoHeight = False
    Me.opVistatiSi.Properties.Caption = "Si"
    Me.opVistatiSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opVistatiSi.Size = New System.Drawing.Size(111, 19)
    Me.opVistatiSi.TabIndex = 0
    '
    'fmBolleFatturate
    '
    Me.fmBolleFatturate.AllowDrop = True
    Me.fmBolleFatturate.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmBolleFatturate.Appearance.Options.UseBackColor = True
    Me.fmBolleFatturate.Controls.Add(Me.opBolleFatturateEntrambe)
    Me.fmBolleFatturate.Controls.Add(Me.opBolleFatturateNo)
    Me.fmBolleFatturate.Controls.Add(Me.opBolleFatturateSi)
    Me.fmBolleFatturate.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmBolleFatturate.Location = New System.Drawing.Point(350, 7)
    Me.fmBolleFatturate.Name = "fmBolleFatturate"
    Me.fmBolleFatturate.Size = New System.Drawing.Size(110, 87)
    Me.fmBolleFatturate.TabIndex = 44
    Me.fmBolleFatturate.Text = "Bolle fatturate:"
    '
    'opBolleFatturateEntrambe
    '
    Me.opBolleFatturateEntrambe.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateEntrambe.EditValue = True
    Me.opBolleFatturateEntrambe.Location = New System.Drawing.Point(9, 63)
    Me.opBolleFatturateEntrambe.Name = "opBolleFatturateEntrambe"
    Me.opBolleFatturateEntrambe.NTSCheckValue = "S"
    Me.opBolleFatturateEntrambe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateEntrambe.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateEntrambe.Properties.AutoHeight = False
    Me.opBolleFatturateEntrambe.Properties.Caption = "Entrambe"
    Me.opBolleFatturateEntrambe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateEntrambe.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateEntrambe.TabIndex = 2
    '
    'opBolleFatturateNo
    '
    Me.opBolleFatturateNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateNo.Location = New System.Drawing.Point(9, 43)
    Me.opBolleFatturateNo.Name = "opBolleFatturateNo"
    Me.opBolleFatturateNo.NTSCheckValue = "S"
    Me.opBolleFatturateNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateNo.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateNo.Properties.AutoHeight = False
    Me.opBolleFatturateNo.Properties.Caption = "No"
    Me.opBolleFatturateNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateNo.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateNo.TabIndex = 1
    '
    'opBolleFatturateSi
    '
    Me.opBolleFatturateSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateSi.Location = New System.Drawing.Point(9, 23)
    Me.opBolleFatturateSi.Name = "opBolleFatturateSi"
    Me.opBolleFatturateSi.NTSCheckValue = "S"
    Me.opBolleFatturateSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateSi.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateSi.Properties.AutoHeight = False
    Me.opBolleFatturateSi.Properties.Caption = "Si"
    Me.opBolleFatturateSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateSi.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateSi.TabIndex = 0
    '
    'tsConf
    '
    Me.tsConf.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsConf.Location = New System.Drawing.Point(0, 160)
    Me.tsConf.Name = "tsConf"
    Me.tsConf.SelectedTabPage = Me.NtsTabPage1
    Me.tsConf.Size = New System.Drawing.Size(695, 386)
    Me.tsConf.TabIndex = 56
    Me.tsConf.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsConf.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.pnDown)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(686, 356)
    Me.NtsTabPage1.Text = "&1- Principale"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnFiltriExt)
    Me.NtsTabPage2.Controls.Add(Me.pnFiltri2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(686, 356)
    Me.NtsTabPage2.Text = "&2 - Filtri Estesi"
    '
    'pnFiltri2
    '
    Me.pnFiltri2.AllowDrop = True
    Me.pnFiltri2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri2.Appearance.Options.UseBackColor = True
    Me.pnFiltri2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri2.Controls.Add(Me.cmdLock)
    Me.pnFiltri2.Controls.Add(Me.grFiltri1)
    Me.pnFiltri2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri2.Location = New System.Drawing.Point(588, 285)
    Me.pnFiltri2.Name = "pnFiltri2"
    Me.pnFiltri2.NTSActiveTrasparency = True
    Me.pnFiltri2.Size = New System.Drawing.Size(98, 71)
    Me.pnFiltri2.TabIndex = 0
    Me.pnFiltri2.Text = "NtsPanel1"
    Me.pnFiltri2.Visible = False
    '
    'cmdLock
    '
    Me.cmdLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(-16, 45)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(111, 20)
    Me.cmdLock.TabIndex = 9
    Me.cmdLock.Text = "Blocca/sblocca filtri"
    '
    'grFiltri1
    '
    Me.grFiltri1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grFiltri1.EmbeddedNavigator.Name = ""
    Me.grFiltri1.Location = New System.Drawing.Point(3, 3)
    Me.grFiltri1.MainView = Me.grvFiltri1
    Me.grFiltri1.Name = "grFiltri1"
    Me.grFiltri1.Size = New System.Drawing.Size(92, 36)
    Me.grFiltri1.TabIndex = 6
    Me.grFiltri1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFiltri1})
    '
    'grvFiltri1
    '
    Me.grvFiltri1.ActiveFilterEnabled = False
    Me.grvFiltri1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_valoreda, Me.xx_valorea})
    Me.grvFiltri1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFiltri1.Enabled = True
    Me.grvFiltri1.GridControl = Me.grFiltri1
    Me.grvFiltri1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri1.MinRowHeight = 14
    Me.grvFiltri1.Name = "grvFiltri1"
    Me.grvFiltri1.NTSAllowDelete = True
    Me.grvFiltri1.NTSAllowInsert = True
    Me.grvFiltri1.NTSAllowUpdate = True
    Me.grvFiltri1.NTSMenuContext = Nothing
    Me.grvFiltri1.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri1.OptionsFilter.AllowFilterEditor = False
    Me.grvFiltri1.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri1.OptionsNavigation.UseTabKey = False
    Me.grvFiltri1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri1.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri1.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFiltri1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri1.OptionsView.ShowGroupPanel = False
    Me.grvFiltri1.RowHeight = 16
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
    Me.xx_nome.Width = 178
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
    Me.xx_valoreda.Width = 300
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
    Me.xx_valorea.Width = 300
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdApriFiltri)
    Me.pnTop.Controls.Add(Me.cbFiltro)
    Me.pnTop.Controls.Add(Me.lbFiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(695, 32)
    Me.pnTop.TabIndex = 57
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdApriFiltri
    '
    Me.cmdApriFiltri.Image = CType(resources.GetObject("cmdApriFiltri.Image"), System.Drawing.Image)
    Me.cmdApriFiltri.ImageAlignment = DevExpress.Utils.HorzAlignment.[Default]
    Me.cmdApriFiltri.ImageText = ""
    Me.cmdApriFiltri.Location = New System.Drawing.Point(237, 3)
    Me.cmdApriFiltri.Name = "cmdApriFiltri"
    Me.cmdApriFiltri.NTSContextMenu = Nothing
    Me.cmdApriFiltri.Size = New System.Drawing.Size(28, 28)
    Me.cmdApriFiltri.TabIndex = 5
    '
    'cbFiltro
    '
    Me.cbFiltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbFiltro.DataSource = Nothing
    Me.cbFiltro.DisplayMember = ""
    Me.cbFiltro.Location = New System.Drawing.Point(88, 6)
    Me.cbFiltro.Name = "cbFiltro"
    Me.cbFiltro.NTSDbField = ""
    Me.cbFiltro.Properties.AutoHeight = False
    Me.cbFiltro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbFiltro.Properties.DropDownRows = 30
    Me.cbFiltro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbFiltro.SelectedValue = ""
    Me.cbFiltro.Size = New System.Drawing.Size(146, 20)
    Me.cbFiltro.TabIndex = 4
    Me.cbFiltro.ValueMember = ""
    '
    'lbFiltri
    '
    Me.lbFiltri.AutoSize = True
    Me.lbFiltri.BackColor = System.Drawing.Color.Transparent
    Me.lbFiltri.Location = New System.Drawing.Point(3, 9)
    Me.lbFiltri.Name = "lbFiltri"
    Me.lbFiltri.NTSDbField = ""
    Me.lbFiltri.Size = New System.Drawing.Size(79, 13)
    Me.lbFiltri.TabIndex = 3
    Me.lbFiltri.Text = "Configurazione"
    Me.lbFiltri.Tooltip = ""
    Me.lbFiltri.UseMnemonic = False
    '
    'pnFiltriExt
    '
    Me.pnFiltriExt.AllowDrop = True
    Me.pnFiltriExt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltriExt.Appearance.Options.UseBackColor = True
    Me.pnFiltriExt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltriExt.Controls.Add(Me.ceFiltriExt)
    Me.pnFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltriExt.Name = "pnFiltriExt"
    Me.pnFiltriExt.NTSActiveTrasparency = True
    Me.pnFiltriExt.Size = New System.Drawing.Size(686, 356)
    Me.pnFiltriExt.TabIndex = 1
    Me.pnFiltriExt.Text = "NtsPanel1"
    '
    'ceFiltriExt
    '
    Me.ceFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.ceFiltriExt.MinimumSize = New System.Drawing.Size(399, 193)
    Me.ceFiltriExt.Name = "ceFiltriExt"
    Me.ceFiltriExt.oParent = Nothing
    Me.ceFiltriExt.Size = New System.Drawing.Size(686, 356)
    Me.ceFiltriExt.strNomeCampo = ""
    Me.ceFiltriExt.TabIndex = 0
    '
    'FRMMGSTBO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(695, 546)
    Me.Controls.Add(Me.tsConf)
    Me.Controls.Add(Me.pnUp)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSTBO"
    Me.Text = "STAMPA/VISUALIZZAZIONE MOVIMENTI DI MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSelAnnoStag, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSelAnnoStag.ResumeLayout(False)
    Me.fmSelAnnoStag.PerformLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDown, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDown.ResumeLayout(False)
    Me.pnDown.PerformLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDownLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDownLeft.ResumeLayout(False)
    Me.pnDownLeft.PerformLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoStampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoStampa.ResumeLayout(False)
    Me.fmTipoStampa.PerformLayout()
    CType(Me.opTipoStampaCompleta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampaRidotta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTippaga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTippaga.ResumeLayout(False)
    Me.fmTippaga.PerformLayout()
    CType(Me.opTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTippaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSpecifico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagen2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnUp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnUp.ResumeLayout(False)
    CType(Me.fmSeldocumenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSeldocumenti.ResumeLayout(False)
    Me.fmSeldocumenti.PerformLayout()
    CType(Me.ckFlevas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opNoteprel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSeldocumenti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFlcont, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFlcont.ResumeLayout(False)
    Me.fmFlcont.PerformLayout()
    CType(Me.opFlcontEntrambe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlcontNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlcontSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmVistati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmVistati.ResumeLayout(False)
    Me.fmVistati.PerformLayout()
    CType(Me.opVistatiEntrambi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opVistatiNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opVistatiSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmBolleFatturate, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmBolleFatturate.ResumeLayout(False)
    Me.fmBolleFatturate.PerformLayout()
    CType(Me.opBolleFatturateEntrambe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBolleFatturateNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBolleFatturateSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsConf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsConf.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri2.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltriExt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltriExt.ResumeLayout(False)
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
    'Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSTBO", "BEMGSTBO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128534975466065282, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleStbo = CType(oTmp, CLEMGSTBO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSTBO", strRemoteServer, strRemotePort)
    AddHandler oCleStbo.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleStbo.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub LoadImage()
    '-------------------------------------------------
    'carico le immagini della toolbar
    Try
      tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
    Catch ex As Exception
    End Try
    Try
      tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
    Catch ex As Exception
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      cbTipork.NTSSetParam(oApp.Tr(Me, 128535054431439924, "Tipo documento"))
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128535054431283713, "Anno"), "0", 4, 1900, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425503906, "Serie"), CLN__STD.SerieMaxLen, True)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430815080, "Dal conto"), tabanagra)
      edAconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054423785585, "al conto"), tabanagra)
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429565392, "Dalla data"), False)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429409181, "alla data"), False)
      edDanumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128535054430971291, "Da numero documento"), "0", 9, 0, 999999999)
      edAnumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425191484, "A numero documento"), "0", 9, 0, 999999999)
      edCommecaini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430658869, "Da commessa"), tabcommess)
      edCommecafin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054429877814, "A commessa"), tabcommess)
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128641477567187500, "Tipo bolla/fattura"), tabtpbf)
      edCodagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427066016, "Agente"), tabcage)
      edCodagen2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128641456641250000, "Agente 2"), tabcage)
      edCoddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427690860, "Destinazione"), tabdestdiv)
      edCoddest.CliePerDestdiv = edDaconto
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054431127502, "Linea/famiglia"), tabcfam, True)
      edCodlsel.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130374682731288176, "Lista selezionata"), tablsel)

      edCodstag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054426128750, "Stagione"), tabstag)
      edAnnotco.NTSSetParam(oMenu, oApp.Tr(Me, 128535054426441172, "Anno"), "0", 4, 0, 2099)
      ckSelAnnoStag.NTSSetParam(oMenu, oApp.Tr(Me, 128535054426597383, "Seleziona Anno/Stagione"), "S", "N")
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")

      opTipoStampaCompleta.NTSSetParam(oMenu, oApp.Tr(Me, 129195093162014394, "Completa"), "C")
      opTipoStampaRidotta.NTSSetParam(oMenu, oApp.Tr(Me, 129195095171076894, "Ridotta"), "R")
      opTutti.NTSSetParam(oMenu, oApp.Tr(Me, 129195095182795644, "Tutti"), "T")
      cbTippaga.NTSSetParam(oApp.Tr(Me, 129195093162326894, "Tipo pagamento"))
      opSpecifico.NTSSetParam(oMenu, oApp.Tr(Me, 129195093162483144, "Specifico"), "S")
      ckFlevas.NTSSetParam(oMenu, oApp.Tr(Me, 129195095225451894, "Solo non evase"), "S", "N")
      ckReport.NTSSetParam(oMenu, oApp.Tr(Me, 129195093166545644, "Utilizza reports standard"), "S", "N")
      opNoteprel.NTSSetParam(oMenu, oApp.Tr(Me, 129195093166701894, "Note di prelievo"), "N")
      ckSeldocumenti.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167014394, "Seleziona documenti:"), "S", "N")
      opTipork.NTSSetParam(oMenu, oApp.Tr(Me, 129195095356545644, "Documento"), "K")
      opFlcontEntrambe.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167170644, "Entrambe"), "E")
      opFlcontNo.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167326894, "No"), "F")
      opFlcontSi.NTSSetParam(oMenu, oApp.Tr(Me, 129195095469358144, "Si"), "S")
      opVistatiEntrambi.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167483144, "Entrambi"), "V")
      opVistatiNo.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167639394, "No"), "N")
      opVistatiSi.NTSSetParam(oMenu, oApp.Tr(Me, 129195095546076894, "Si"), "S")
      opBolleFatturateEntrambe.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167795644, "Entrambe"), "E")
      opBolleFatturateNo.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167951894, "No"), "N")
      opBolleFatturateSi.NTSSetParam(oMenu, oApp.Tr(Me, 129195093168108144, "Si"), "S")
      cbFiltro.NTSSetParam(oApp.Tr(Me, 129195093168264394, "Filtro"))

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvFiltri1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia filtri 1"))
      grvFiltri1.NTSAllowDelete = False
      grvFiltri1.NTSAllowInsert = False
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128491820088062000, "Nome filtro"), dttCampi, "xx_nome", "cb_nomcampo")
      xx_valoreda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128491820607854000, "Valore filtro DA"), 0)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128584498502187500, "Valore filtro A"), 0)
      xx_valoreda.NTSSetParamZoom("__")
      xx_valorea.NTSSetParamZoom("__")


      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 130422010506752223, "Filtri Estesi"), "BSMGSTBO", New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTMAG")
      ceFiltriExt.AggiungiTabella("MOVMAG")
      ceFiltriExt.AggiungiTabella("ANAGRA")
      ceFiltriExt.AggiungiTabella("TABPAGA")
      ceFiltriExt.InizializzaFiltri()
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
    Dim dttTipork As New DataTable()
    Dim dttTippaga As New DataTable()
    Try
      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))


      dttTipork.Rows.Add(New Object() {"A", "Fatture Imm. emesse"})
      dttTipork.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipork.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipork.Rows.Add(New Object() {"D", "Fatture Diff. emesse"})
      dttTipork.Rows.Add(New Object() {"E", "Note di Addebito emesse"})
      dttTipork.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipork.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipork.Rows.Add(New Object() {"J", "Note Accr. ricevute"})
      dttTipork.Rows.Add(New Object() {"K", "Fatture Diff. ricevute"})
      dttTipork.Rows.Add(New Object() {"L", "Fatture Imm. ricevute"})
      dttTipork.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipork.Rows.Add(New Object() {"N", "Note Accr. emesse"})
      dttTipork.Rows.Add(New Object() {"P", "Fatt.Ric.Fisc.Differita"})
      dttTipork.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipork.Rows.Add(New Object() {"T", "Carico da produz."})
      dttTipork.Rows.Add(New Object() {"U", "Scarico a produz."})
      dttTipork.Rows.Add(New Object() {"Z", "Bolle di mov. interna"})
      dttTipork.Rows.Add(New Object() {"£", "Note accred. diff. emesse"})
      dttTipork.Rows.Add(New Object() {"(", "Note accred. diff. ricevute"})

      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"
      cbTipork.SelectedValue = "A"

      dttTippaga.Columns.Add("cod", GetType(Integer))
      dttTippaga.Columns.Add("val", GetType(String))


      dttTippaga.Rows.Add(New Object() {1, "Tratta"})
      dttTippaga.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTippaga.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTippaga.Rows.Add(New Object() {4, "Contanti"})
      dttTippaga.Rows.Add(New Object() {5, "Accr.Bancario"})

      cbTippaga.DataSource = dttTippaga
      cbTippaga.ValueMember = "cod"
      cbTippaga.DisplayMember = "val"
      cbTippaga.SelectedValue = "1"

      CaricaFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGSTBO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim dttRelease As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.GetBusRelease(dttRelease) = True Then
        If dttRelease.Rows.Count <> 0 Then
          With dttRelease.Rows(0)
            If NTSCInt(!rel_maior) >= 15 Or (NTSCInt(!rel_maior) = 14 And NTSCStr(!rel_pers) = "a") Then oCleStbo.bIs15 = True
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se esiste il modulo del CRM
      '--------------------------------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM)) Or _
        CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleStbo.bModuloCRM = True Else oCleStbo.bModuloCRM = False
      '--------------------------------------------------------------------------------------------------------------
      If oCleStbo.bModuloCRM Then
        oCleStbo.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleStbo.bAmm, oCleStbo.strAccvis, oCleStbo.strAccmod, oCleStbo.strRegvis, oCleStbo.strRegmod)
        If oCleStbo.bIsCRMUser Then
          oCleStbo.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleStbo.lCodorgaOperat)
          If oCleStbo.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128257748101012000, "Attenzione!" & vbCrLf & _
              "L'operatore '|" & oApp.User.Nome & "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & _
              DittaCorrente & "|'." & vbCrLf & _
              "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (oCleStbo.bModuloCRM = True) And (oCleStbo.bIsCRMUser = True) And (oCleStbo.bIs15 = True) Then
        oCleStbo.lIITtsubqcrm = oMenu.GetTblInstId("TTSUBQCRM", False)
        oCleStbo.nCodcageAccdito = oCleStbo.RitornaCodcageAccdito(oCleStbo.bModuloCRM, oCleStbo.bIsCRMUser)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Test per controllare l'esistenza o meno del modulo TCO 'Taglie e colori'
      '-----------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then oCleStbo.bModTCO = True Else oCleStbo.bModTCO = False


      '--------------------------------------------------------
      'Lettura opzione di registro su utilizzazione di KEYMAG/KEYPRB nella SelectionFormula
      oCleStbo.bUsaKeymag = CBool(oMenu.GetSettingBus("BSMGSTBO", "OPZIONI", ".", "UsaKeymag", "0", " ", "0"))
      '--------------------------------------------------------

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("testmag", dttCampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()

      CreaDatatableFiltri()

      dcFiltri1.DataSource = dsFiltri.Tables("FILTRI1")
      dsFiltri.AcceptChanges()
      grFiltri1.DataSource = dcFiltri1

      '-------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------
      InitControls()

      tsConf.SelectedTabPageIndex = 0

      If oCleStbo.bModTCO = False Then fmSelAnnoStag.Visible = False

      Me.MinimumSize = Me.Size

      '-------------------------------------------------------
      '--- Inizializzazione dei dati della form
      '-------------------------------------------------------
      edAnno.Text = Format(Now, "yyyy")
      edSerie.Text = ""
      edDanumdoc.Text = "0"
      edAnumdoc.Text = "".PadLeft(9, "9"c)
      edDaconto.Text = "0"
      edAconto.Text = "999999999"
      edDatini.Text = IntSetDate("01/01/1900")
      edDatfin.Text = IntSetDate("31/12/2099")
      edCoddest.Text = "0"
      edCoddest.Enabled = False
      edCodagen.Text = "0"
      edCodagen2.Text = "0"
      edTipobf.Text = "0"
      edCommecaini.Text = "0"
      edCommecafin.Text = "999999999"
      edCodcfam.Text = ""
      lbDescodcfam.Text = ""
      edCodlsel.Text = "0"

      'Settaggio group box tipo documento
      opTipork.Enabled = False
      cbTipork.Enabled = False
      opNoteprel.Enabled = False
      ckFlevas.Visible = False
      ckReport.Enabled = False
      'Settagio group box tipo pagamento
      cbTippaga.Enabled = False

      '-----------------------------------------------------------------------------------------
      If oCleStbo.bModTCO = False Then
        fmSelAnnoStag.Enabled = False
        ckSelAnnoStag.Enabled = False
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
        fmSelAnnoStag.Visible = False
        ckSelAnnoStag.Visible = False
      Else
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
      End If

      edSerie.Enabled = False
      ckSerie.Checked = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      Me.GctlApplicaDefaultValue()

      'Prende la struttura della tabella
      oCleStbo.GetTableStructMovIfil(dttDefault)

      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")

      ComponiDatatable(dttDefault, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGSTBO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      'Necessario per ovviare al problema che non caricava i dati se si forzava un valore del combo dalla configuratore user interface
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMMGSTBO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      If Not dsFiltri Is Nothing Then
        If Not dsFiltri.Tables("FILTRI1") Is Nothing Then
          '-------------------------------------------------
          'salvo il recent
          Dim strTmp As String = ""
          Dim i As Integer = 0
          dsFiltri.Tables("FILTRI1").AcceptChanges()
          For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
            strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BNMGSTBO", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
        End If
        dcFiltri1.Dispose()
        dsFiltri.Dispose()
      End If

      dcStbo.Dispose()
      If Not dsStbo Is Nothing Then dsStbo.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGSTBO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      SvuotaTmpTable()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Dim frmGrbo As FRMMGGRBO = Nothing
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionate solo Fatture Differite Emesse/Ricevute
      '--- non permette la visualizzazione in griglia
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked And opTipork.Checked Then
        If cbTipork.SelectedValue = "D" Or cbTipork.SelectedValue = "K" Or cbTipork.SelectedValue = "P" Or _
           cbTipork.SelectedValue = "£" Or cbTipork.SelectedValue = "(" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128729689702656250, "Stampa/Visualizzazione documenti riepilogativi non possibile su griglia."))
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa(True) Then Return
      '--------------------------------------------------------------------------------------------------------------
      frmGrbo = CType(NTSNewFormModal("FRMMGGRBO"), FRMMGGRBO)
      frmGrbo.Init(oMenu, oCallParams, DittaCorrente)
      frmGrbo.InitEntity(oCleStbo)
      frmGrbo.ShowDialog()
      frmGrbo.Dispose()
      frmGrbo = Nothing
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPdf.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim strQueryGetDocClienti As String = ""        'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per cliente)
    Dim strQueryGetDocAgenti As String = ""         'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per agente 1)
    Dim strQueryTmp As String = ""

    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante
    Dim strTabellaMov, strTabella As String
    Dim strMessComponiFormula As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa() Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiamo la stampa su PDF passandogli le query
      '--------------------------------------------------------------------------------------------------------------
      strQueryGetDocMultipli = oCleStbo.GetQueryStampaPdf(NTSCInt(edDaconto.Text), NTSCInt(edAconto.Text), _
                              NTSCInt(edAnno.Text), IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                              NTSCDate(edDatini.Text), NTSCDate(edDatfin.Text), _
                              NTSCInt(edDanumdoc.Text), NTSCInt(edAnumdoc.Text), _
                              NTSCInt(edCommecaini.Text), NTSCInt(edCommecafin.Text), _
                              ckSeldocumenti.Checked, opNoteprel.Checked, _
                              IIf(opNoteprel.Checked, "W", cbTipork.SelectedValue).ToString, NTSCInt(edTipobf.Text), _
                              NTSCInt(edCoddest.Text), NTSCInt(edCodagen.Text), NTSCInt(edCodagen2.Text), _
                              opBolleFatturateEntrambe.Checked, opBolleFatturateSi.Checked, _
                              opVistatiEntrambi.Checked, opVistatiSi.Checked, _
                              opFlcontEntrambe.Checked, opFlcontSi.Checked, _
                              edCodcfam.Text, ckSelAnnoStag.Checked, _
                              NTSCInt(edAnnotco.Text), NTSCInt(edCodstag.Text), _
                              ComponiWhereFiltriEstesi(False), ckFlevas.Checked, _
                              NTSCInt(IIf(opSpecifico.Checked, cbTippaga.SelectedValue, 0)), strQueryGetDocUnico, _
                              strQueryGetDocClienti, strQueryGetDocAgenti, ckReport.Checked, edCodlsel.textint)
      If strQueryGetDocMultipli = "" Or strQueryGetDocUnico = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644944132812500, "Errore durante la creazione della query di selezione per il modulo PDF."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se devo passare delle formule lo faccio tramite questo datatable (per la 'PeSetFormula'
      '--- devo compilare o num, o str, o data a seconda del tipo di dato. 'name' deve sempre essere impostata
      '--------------------------------------------------------------------------------------------------------------
      dttFormule.Columns.Add("name", GetType(String))
      dttFormule.Columns.Add("num", GetType(Decimal))
      dttFormule.Columns.Add("str", GetType(String))
      dttFormule.Columns.Add("data", GetType(DateTime))
      dttFormule.Columns("name").DefaultValue = Nothing
      dttFormule.Columns("num").DefaultValue = Nothing
      dttFormule.Columns("str").DefaultValue = Nothing
      dttFormule.Columns("data").DefaultValue = Nothing
      dttFormule.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked And opNoteprel.Checked And (Not ckReport.Checked) Then
        strTabella = "TESTPRB"
      Else
        strTabella = "TESTMAG"
      End If
      strTabellaMov = "mov" & strTabella.Substring(4)
      '--------------------------------------------------------------------------------------------------------------
      'prima parte di PeSetSelectionFormula
      strQueryCrw32FileMultipli = ComponiFormula(strMessComponiFormula)
      If strMessComponiFormula.Length <> 0 Then
        oApp.MsgBoxInfo(strMessComponiFormula)
      Else
        'passo le query per la stampa raggruppata per cliente e per agente
        strQueryTmp = strQueryGetDocClienti & "§" & _
                      strQueryGetDocAgenti & "§" & _
                      strQueryCrw32FileMultipli & " AND {" & strTabella & ".tm_conto} = |conto|" & "§" & _
                      strQueryCrw32FileMultipli & " AND {" & strTabella & ".tm_codagen} = |agente|"

        'pdf unico, un pdf per ogni documento
        strQueryCrw32FileUnico = strQueryCrw32FileMultipli
        strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                                    " AND {" & strTabella & ".tm_anno} = |anno|" & _
                                    " AND {" & strTabella & ".tm_numdoc} = |numero|" & _
                                    " AND {" & strTabella & ".tm_serie} = |serie|" & _
                                    " AND {" & strTabella & ".tm_tipork} = |tipork|"
        If ckReport.Checked Then
          strQueryCrw32FileUnico = strQueryCrw32FileUnico & _
                                   " AND {" & strTabella & ".tm_valuta} = |valuta|" & _
                                   " AND {" & strTabella & ".tm_scorpo} = |scorpo|"
          strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                                      " AND {" & strTabella & ".tm_valuta} = |valuta|" & _
                                      " AND {" & strTabella & ".tm_scorpo} = |scorpo|"
        End If

        '--------------------------------------------------------------------------------------------------------------
        oPar = New CLE__CLDP
        oPar.Ditta = DittaCorrente

        oPar.strPar1 = "BSMGSTBO"
        oPar.strPar2 = strQueryCrw32FileMultipli
        oPar.strPar3 = strQueryCrw32FileUnico
        oPar.strPar4 = strQueryGetDocMultipli
        oPar.strPar5 = strQueryGetDocUnico
        oPar.strParam = GetInfoStampa()

        oPar.ctlPar1 = Me
        oPar.ctlPar2 = dttFormule
        oPar.ctlPar3 = New CLE__CLDP
        oPar.ctlPar4 = strQueryTmp    'passo le query per la stampa raggruppata per cliente e per agente

        oPar.bPar1 = ckSeldocumenti.Checked
        oPar.bPar2 = opTipoStampaRidotta.Checked
        oPar.bPar3 = ckReport.Checked

        oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
        oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

        oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)
        '--------------------------------------------------------------------------------------------------------------
        '--- Da bnpdgenp è stato scelto di stampare anche su carta
        '--------------------------------------------------------------------------------------------------------------
        If oPar.bPar4 Then Stampa(1)
        '--------------------------------------------------------------------------------------------------------------
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTipork As String = ""
    Dim strTmp As String = ""

    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edDaconto.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "J", "K", "L", "M", "(" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edDaconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edDaconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edDaconto.Text Then edDaconto.NTSTextDB = NTSZOOM.strIn

      ElseIf edAconto.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "J", "K", "L", "M", "(" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edAconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edAconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAconto.Text Then edAconto.NTSTextDB = NTSZOOM.strIn

      ElseIf edCoddest.Focused Then
        SetFastZoom(edCoddest.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddest.Text
        oParam.lContoCF = NTSCInt(edDaconto.Text)
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddest.Text Then edCoddest.NTSTextDB = NTSZOOM.strIn

      ElseIf grFiltri1.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        If NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome) = "." Then Return 'sono su una colonna N.A.
        If grvFiltri1.FocusedColumn.Name = "xx_valoreda" Then
          strTmp = NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valoreda)
          ApriZoomTabella(strTmp, NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome))
        Else
          strTmp = NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valorea)
          ApriZoomTabella(strTmp, NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome))
        End If
      ElseIf ceFiltriExt.ContainsFocus Then
        ceFiltriExt.Zoom()
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
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "ALT+F2 e ALT+F3 rimappati"
  Public Overridable Sub edConto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edDaconto.NTSZoomGest, edAconto.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim strTipoConto As String
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      Select Case NTSCStr(cbTipork.SelectedValue)
        Case "J", "K", "L", "M", "(" : strTipoConto = "F"
        Case Else : strTipoConto = "C"
      End Select

      If e.TipoEvento = "OPEN" Then
        Dim dttTmpConto As New DataTable
        If oCleStbo.ValCodiceConto(CType(sender, NTSTextBoxNum).Text, dttTmpConto) Then
          If dttTmpConto.Rows.Count > 0 Then
            strTipoConto = dttTmpConto.Rows(0)!an_tipo.ToString()
          End If
        End If
        bNuovo = False
      End If

      oTmp.Text = CType(sender, NTSTextBoxNum).Text

      If strTipoConto <> "C" And strTipoConto <> "F" Then
        strTipoConto = "C"
        oTmp.Text = "0"
        bNuovo = False
      End If

      Select Case strTipoConto
        Case "C"
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Case "F"
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
      End Select

      CType(sender, NTSTextBoxNum).Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


  Public Overridable Function GetInfoStampa() As String
    Dim strTipork As String = ""
    Dim strTitle As String = ""
    Try

      If ckSeldocumenti.Checked Then
        If opTipork.Checked Then
          strTipork = cbTipork.SelectedValue
        Else
          strTipork = "W"
        End If
      End If

      If Not ckSeldocumenti.Checked Then
        '---------------------------------------------------------------
        '--- Report brogliaccio su TESTMAG (tutti i tipi record)
        '---------------------------------------------------------------
        strTitle += "Stampa/Visualizzazione movimenti di magazzino"
        '--- Report brogliaccio su TESTMAG (tutti i tipi record)
      Else
        If opTipork.Checked Then
          If ckReport.Checked Then
            '---------------------------------------------------------------
            '--- Reports standard su TESTMAG (solo un tipo record)
            '---------------------------------------------------------------
            Select Case strTipork
              Case "A", "C", "E", "J", "L", "N"
                Select Case strTipork
                  Case "A"
                    strTitle += "Stampa Fatture immediate emesse"
                  Case "C"
                    strTitle += "Stampa Corrispettivi emessi"
                  Case "E"
                    strTitle += "Stampa Note di addebito emesse"
                  Case "J"
                    strTitle += "Stampa Note di accredito ricevute"
                  Case "L"
                    strTitle += "Stampa Fatture immediate ricevute"
                  Case "N"
                    strTitle += "Stampa Note di accredito emesse"
                End Select
              Case "B", "M", "T", "Z"
                Select Case strTipork
                  Case "B"
                    strTitle += "Stampa D.D.T. emessi"
                  Case "M"
                    strTitle += "Stampa D.D.T. ricevuti"
                  Case "T"
                    strTitle += "Stampa Carico da produzione"
                  Case "Z"
                    strTitle += "Stampa Bolle di movimentazione interna"
                End Select
              Case "D", "K", "£", "("
                Select Case strTipork
                  Case "D"
                    strTitle += "Stampa Fatture differite emesse"
                  Case "K"
                    strTitle += "Stampa Fatture differite ricevute"
                  Case "£"
                    strTitle += "Stampa Note accredito differite emesse"
                  Case "("
                    strTitle += "Stampa Note accredito differite ricevute"
                End Select
              Case "P"
                strTitle += "Stampa Fatture/ricevute fiscali differite"
              Case "F", "I"
                Select Case strTipork
                  Case "F"
                    strTitle += "Stampa Ricevute fiscali emesse"
                  Case "I"
                    strTitle += "Stampa Riemissione ricevute fiscali"
                End Select
              Case "S"
                strTitle += "Stampa Fatture/ricevute fiscali emesse"
            End Select
            '--- Reports standard su TESTMAG (solo un tipo record)
          Else
            '---------------------------------------------------------------
            '--- Report brogliaccio su TESTMAG (solo un tipo record)
            '---------------------------------------------------------------
            Select Case strTipork
              Case "D"
                strTitle += "Stampa Fatture differite emesse"
              Case "K"
                strTitle += "Stampa Fatture differite ricevute"
              Case "P"
                strTitle += "Stampa Fatture/ricevute fiscali differite"
              Case "A"
                strTitle += "Stampa Fatture immediate emesse"
              Case "B"
                strTitle += "Stampa D.D.T. emessi"
              Case "C"
                strTitle += "Stampa Corrispettivi emessi"
              Case "E"
                strTitle += "Stampa Note di addebito emesse"
              Case "F"
                strTitle += "Stampa Ricevute fiscali emesse"
              Case "I"
                strTitle += "Stampa Riemissioni ricevute fiscali"
              Case "J"
                strTitle += "Stampa Note di accredito ricevute"
              Case "L"
                strTitle += "Stampa Fatture immediate ricevute"
              Case "M"
                strTitle += "Stampa D.D.T. ricevuti"
              Case "N"
                strTitle += "Stampa Note di accredito emesse"
              Case "S"
                strTitle += "Stampa Fatture/ricevute fiscali emesse"
              Case "T"
                strTitle += "Stampa Carico da produzione"
              Case "U"
                strTitle += "Stampa Scarico a produzione"
              Case "Z"
                strTitle += "Stampa Bolle di movimentazione interna"
              Case "£"
                strTitle += "Stampa Note accredito differite emesse"
              Case "("
                strTitle += "Stampa Note accredito differite ricevute"
            End Select
            '--- Report brogliaccio su TESTMAG (solo un tipo record)
          End If
        Else
          If ckReport.Checked Then
            '---------------------------------------------------------------
            '--- Reports standard su TESTPRB
            '---------------------------------------------------------------
            strTitle += "Stampa Note di prelievo)"
            '--- Reports standard su TESTPRB
          Else
            '---------------------------------------------------------------
            '--- Reports brogliaccio su TESTPRB
            '---------------------------------------------------------------
            strTitle += "Stampa/Visualizzazione Note di prelievo"
            '--- Reports brogliaccio su TESTPRB
          End If
        End If
      End If

      Return UCase(strTitle)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim strCrpe As String
    Dim strTipork As String = " "
    Dim strTitle As String = ""
    Dim strMessComponiFormula As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa() Then Return
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = ComponiFormula(strMessComponiFormula)
      If strMessComponiFormula.Length <> 0 Then
        oApp.MsgBoxInfo(strMessComponiFormula)
      Else
        If strCrpe = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128642964853750000, "Errore durante la composizione della formula CRPE. Impossibile continuare."))
          Return
        End If
        '--------------------------------------------------------------------------------------------------------------
        If ckSeldocumenti.Checked Then
          If opTipork.Checked Then
            strTipork = cbTipork.SelectedValue
          Else
            strTipork = "W"
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        If Not ckSeldocumenti.Checked Then
          '---------------------------------------------------------------
          '--- Report brogliaccio su TESTMAG (tutti i tipi record)
          '---------------------------------------------------------------
          strTitle = "Stampa/Visualizzazione movimenti di magazzino"
          If opTipoStampaRidotta.Checked Then
            StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports4", strTipork, "BSMGSTB3.RPT", strTitle)
          Else
            StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports1", strTipork, "BSMGSTBO.RPT", strTitle)
          End If
          '--- Report brogliaccio su TESTMAG (tutti i tipi record)
        Else
          If opTipork.Checked Then
            If ckReport.Checked Then
              '---------------------------------------------------------------
              '--- Reports standard su TESTMAG (solo un tipo record)
              '---------------------------------------------------------------
              strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'N'"

              Select Case strTipork
                Case "A", "C", "E", "J", "L", "N"
                  Select Case strTipork
                    Case "A"
                      strTitle = "Stampa Fatture immediate emesse"
                    Case "C"
                      strTitle = "Stampa Corrispettivi emessi"
                    Case "E"
                      strTitle = "Stampa Note di addebito emesse"
                    Case "J"
                      strTitle = "Stampa Note di accredito ricevute"
                    Case "L"
                      strTitle = "Stampa Fatture immediate ricevute"
                    Case "N"
                      strTitle = "Stampa Note di accredito emesse"
                  End Select
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports1", strTipork, "BSVEFATI.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports3", strTipork, "BSVEFATV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsveboll", "Reports2", strTipork, "BSVEFATC.RPT", strTitle & " (in Lire con scorporo)")
                Case "B", "M", "T", "Z"
                  Select Case strTipork
                    Case "B"
                      strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'D'"
                      strTitle = "Stampa D.D.T. emessi"
                    Case "M"
                      strTitle = "Stampa D.D.T. ricevuti"
                    Case "T"
                      strTitle = "Stampa Carico da produzione"
                    Case "Z"
                      strTitle = "Stampa Bolle di movimentazione interna"
                  End Select
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports1", strTipork, "BSVEBOLL.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports3", strTipork, "BSVEBOLV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsveboll", "Reports2", strTipork, "BSVEBOLC.RPT", strTitle & " (in Lire con scorporo)")
                Case "D", "K", "£", "("
                  Select Case strTipork
                    Case "D"
                      strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'B'"
                      strTitle = "Stampa Fatture differite emesse"
                    Case "K"
                      strTitle = "Stampa Fatture differite ricevute"
                    Case "£"
                      strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'B'"
                      strTitle = "Stampa Note accredito differite emesse"
                    Case "("
                      strTitle = "Stampa Note accredito differite ricevute"
                  End Select
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsvefdin", "Reports1", strTipork, "BSVEFATD.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsvefdin", "Reports3", strTipork, "BSVEFADV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsvefdin", "Reports2", strTipork, "BSVEFADC.RPT", strTitle & " (in Lire con scorporo)")
                Case "P"
                  strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'B'"
                  strTitle = "Stampa Fatture/ricevute fiscali differite"
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsvefadi", "Reports1", strTipork, "BSVEFRFD.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsvefadi", "Reports3", strTipork, "BSVEFRDV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsvefadi", "Reports2", strTipork, "BSVEFRDC.RPT", strTitle & " (in Lire con scorporo)")
                Case "F", "I"
                  Select Case strTipork
                    Case "F"
                      strTitle = "Stampa Ricevute fiscali emesse"
                    Case "I"
                      strTitle = "Stampa Riemissione ricevute fiscali"
                  End Select
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports1", strTipork, "BSVERIFI.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports3", strTipork, "BSVERIFV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsveboll", "Reports2", strTipork, "BSVERIFC.RPT", strTitle & " (in Lire con scorporo)")
                Case "S"
                  strTitle = "Stampa Fatture/ricevute fiscali emesse"
                  '--- In Lire senza scorporo
                  If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports1", strTipork, "BSVEFRFI.RPT", strTitle & " (in Lire senza scorporo)")
                  '--- In valuta
                  If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports3", strTipork, "BSVEFRFV.RPT", strTitle & " (in valuta)")
                  '--- In Lire con scorporo
                  If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsveboll", "Reports2", strTipork, "BSVEFRFC.RPT", strTitle & " (in Lire con scorporo)")
              End Select
              '--- Reports standard su TESTMAG (solo un tipo record)
            Else
              '---------------------------------------------------------------
              '--- Report brogliaccio su TESTMAG (solo un tipo record)
              '---------------------------------------------------------------
              Select Case strTipork
                Case "D"
                  strTitle = "Stampa Fatture differite emesse"
                Case "K"
                  strTitle = "Stampa Fatture differite ricevute"
                Case "P"
                  strTitle = "Stampa Fatture/ricevute fiscali differite"
                Case "A"
                  strTitle = "Stampa Fatture immediate emesse"
                Case "B"
                  strTitle = "Stampa D.D.T. emessi"
                Case "C"
                  strTitle = "Stampa Corrispettivi emessi"
                Case "E"
                  strTitle = "Stampa Note di addebito emesse"
                Case "F"
                  strTitle = "Stampa Ricevute fiscali emesse"
                Case "I"
                  strTitle = "Stampa Riemissioni ricevute fiscali"
                Case "J"
                  strTitle = "Stampa Note di accredito ricevute"
                Case "L"
                  strTitle = "Stampa Fatture immediate ricevute"
                Case "M"
                  strTitle = "Stampa D.D.T. ricevuti"
                Case "N"
                  strTitle = "Stampa Note di accredito emesse"
                Case "S"
                  strTitle = "Stampa Fatture/ricevute fiscali emesse"
                Case "T"
                  strTitle = "Stampa Carico da produzione"
                Case "U"
                  strTitle = "Stampa Scarico a produzione"
                Case "Z"
                  strTitle = "Stampa Bolle di movimentazione interna"
                Case "£"
                  strTitle = "Stampa Note accredito differite emesse"
                Case "("
                  strTitle = "Stampa Note accredito differite ricevute"
              End Select
              Select Case strTipork
                Case "D", "K", "P", "£", "("
                  If opTipoStampaRidotta.Checked Then
                    StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports5", strTipork, "BSMGSTB4.RPT", strTitle)
                  Else
                    StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports2", strTipork, "BSMGSTB1.RPT", strTitle)
                  End If
                Case Else
                  If opTipoStampaRidotta.Checked Then
                    StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports4", strTipork, "BSMGSTB3.RPT", strTitle)
                  Else
                    StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports1", strTipork, "BSMGSTBO.RPT", strTitle)
                  End If
              End Select
              '--- Report brogliaccio su TESTMAG (solo un tipo record)
            End If
          Else
            If ckReport.Checked Then
              '---------------------------------------------------------------
              '--- Reports standard su TESTPRB
              '---------------------------------------------------------------
              strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'N'"
              strTitle = "Stampa Note di prelievo"
              '--- In Lire senza scorporo
              If CheckReports(1) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports1", strTipork, "BSVEPRBN.RPT", strTitle & " (in Lire senza scorporo)")
              '--- In valuta
              If CheckReports(2) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} > 0 And {TESTMAG.tm_scorpo} = 'N'", "Bsveboll", "Reports3", strTipork, "BSVEPRBV.RPT", strTitle & " (in valuta)")
              '--- In Lire con scorporo
              If CheckReports(3) Then StampaReport(nDestin, strCrpe & " And {TESTMAG.tm_valuta} = 0 And {TESTMAG.tm_scorpo} = 'S'", "Bsveboll", "Reports2", strTipork, "BSVEPRBC.RPT", strTitle & " (in Lire con scorporo)")
              '--- Reports standard su TESTPRB
            Else
              '---------------------------------------------------------------
              '--- Reports brogliaccio su TESTPRB
              '---------------------------------------------------------------
              strTitle = "Stampa/Visualizzazione Note di prelievo"
              If opTipoStampaRidotta.Checked = True Then
                StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports6", strTipork, "BSMGSTB5.RPT", strTitle)
              Else
                StampaReport(nDestin, strCrpe, "Bsmgstbo", "Reports3", strTipork, "BSMGSTB2.RPT", strTitle)
              End If
              '--- Reports brogliaccio su TESTPRB
            End If
          End If
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function ComponiFormula(ByRef strMess As String) As String
    Dim strC As String = "", strTabella, strTipork, strAggwhereCRM As String
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strMess})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMess = NTSCStr(oIn(0))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked And opNoteprel.Checked And (Not ckReport.Checked) Then
        strTabella = "TESTPRB"
      Else
        strTabella = "TESTMAG"
      End If
      Dim strFiltriAgg As String = ceFiltriExt.GeneraQueryReport
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
      If strFiltriAgg = "" OrElse strFiltriAgg.IndexOf("movmag", StringComparison.CurrentCultureIgnoreCase) = -1 Then
        strAggwhereCRM = ""
        If (oCleStbo.bModuloCRM = True) And (oCleStbo.bIsCRMUser = True) Then
          If oCleStbo.bIs15 = False Then
            strAggwhereCRM = " And ( " ' fisso
            If oCleStbo.strAccvis <> "T" Then
              strAggwhereCRM += " ({anagra.an_tipo} = 'C' AND {leads.le_coddest} = 0"
              Select Case oCleStbo.strAccvis
                Case "P" : strAggwhereCRM += " And {leads.le_opinc} = " & oCleStbo.lCodorgaOperat
                Case "C" : strAggwhereCRM += " And {leads.le_opinc} In [" & oCleStbo.strRegvis & "]"
              End Select
              strAggwhereCRM += ")"
            Else ' tutti i clienti
              strAggwhereCRM += " {anagra.an_tipo} = 'C'"
            End If
            If oCleStbo.bAmm = True Then
              strAggwhereCRM += " OR {anagra.an_tipo} <> 'C'"
            End If
            strAggwhereCRM += ")"
          Else
            If oCleStbo.nCodcageAccdito <> 0 Then
              strAggwhereCRM &= " And ({TESTMAG.tm_codagen} = " & oCleStbo.nCodcageAccdito & " Or {TESTMAG.tm_codagen2} = " & oCleStbo.nCodcageAccdito & ")"
            End If
            RiempiTmpTable()
            strAggwhereCRM &= " And {TTSUBQCRM.instid} = " & oCleStbo.lIITtsubqcrm
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        strC = "{" & strTabella & ".tm_anno} = " & edAnno.Text & _
          " And {" & strTabella & ".codditt} = '" & DittaCorrente & "'"
        If (NTSCInt(edDaconto.Text) <> 0) Or (NTSCInt(edAconto.Text) <> 999999999) Then
          strC += " And {" & strTabella & ".tm_conto} in " & edDaconto.Text & " to " & edAconto.Text
        End If
        If (NTSCDate(edDatini.Text) <> New Date(1900, 1, 1)) OrElse (NTSCDate(edDatfin.Text) <> New Date(2099, 12, 31)) Then
          strC += " And {" & strTabella & ".tm_datdoc} in " & ConvDataRpt(edDatini.Text) & " to " & ConvDataRpt(edDatfin.Text)
        End If
        If (NTSCInt(edDanumdoc.Text) <> 0) Or (NTSCInt(edAnumdoc.Text) <> 999999999) Then
          strC += " And {" & strTabella & ".tm_numdoc} in " & edDanumdoc.Text & " to " & edAnumdoc.Text
        End If
        If (NTSCInt(edCommecaini.Text) <> 0) Or (NTSCInt(edCommecafin.Text) <> 999999999) Then
          strC += " And {" & strTabella & ".tm_commeca} in " & edCommecaini.Text & " to " & edCommecafin.Text
        End If
        '--------------------------------------------------------------------------------------------------------------
        If oCleStbo.bUsaKeymag AndAlso ckSeldocumenti.Checked AndAlso ckReport.Checked Then
          If opNoteprel.Checked Then
            strC += " And {keyprb.km_magaz} = {movprb.mm_magaz}"
          Else
            strC += " And {keymag.km_magaz} = {movmag.mm_magaz}"
          End If
        ElseIf ckSeldocumenti.Checked AndAlso ckReport.Checked Then
          If opNoteprel.Checked AndAlso CBool(oMenu.GetSettingBus("BSMGSTBO", "OPZIONI", ".", "UsaKeymagW", "0", " ", "0")) Then
            strC += " And {keyprb.km_magaz} = {movprb.mm_magaz}"
          ElseIf CBool(oMenu.GetSettingBus("BSMGSTBO", "OPZIONI", ".", "UsaKeymag" & cbTipork.SelectedValue.ToUpper, "0", " ", "0")) Then
            strC += " And {keymag.km_magaz} = {movmag.mm_magaz}"
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        If ckSeldocumenti.Checked Then
          If opTipork.Checked Then strTipork = cbTipork.SelectedValue Else strTipork = "W"
          strC += " And {" & strTabella & ".tm_tipork} = '" & strTipork & "'"
        End If
        '--------------------------------------------------------------------------------------------------------------
        If ckSerie.Checked Then
          strC += " And {" & strTabella & ".tm_serie} = '" & edSerie.Text & "'"
        Else
          edSerie.Text = ""
        End If
        If NTSCInt(edTipobf.Text) <> 0 Then strC += " And {" & strTabella & ".tm_tipobf} = " & edTipobf.Text
        If NTSCInt(edCodagen.Text) <> 0 Then strC += " And {" & strTabella & ".tm_codagen} = " & edCodagen.Text
        If NTSCInt(edCodagen2.Text) <> 0 Then strC += " And {" & strTabella & ".tm_codagen2} = " & edCodagen2.Text
        If NTSCInt(edCoddest.Text) <> 0 Then strC += " And {" & strTabella & ".tm_coddest} = " & edCoddest.Text
        If opBolleFatturateSi.Checked Then strC += " And {" & strTabella & ".tm_flfatt} = 'S'"
        If opBolleFatturateNo.Checked Then strC += " And {" & strTabella & ".tm_flfatt} = 'N'"
        If opVistatiSi.Checked Then strC += " And {" & strTabella & ".tm_vistato} = 'S'"
        If opVistatiNo.Checked Then strC += " And {" & strTabella & ".tm_vistato} = 'N'"
        If (ckSeldocumenti.Checked) And (opNoteprel.Checked) And (ckFlevas.Checked) Then
          strC += " And {" & strTabella & ".tm_flevas} = 'N'"
        End If
        '--------------------------------------------------------------------------------------------------------------
        '--- Condizione Fatt. Contabilizzate
        '--------------------------------------------------------------------------------------------------------------
        If opFlcontSi.Checked Then strC += " And {" & strTabella & ".tm_flcont} = 'S'"
        If opFlcontNo.Checked Then strC += " And {" & strTabella & ".tm_flcont} = 'N'"
        '--------------------------------------------------------------------------------------------------------------
        '--- Condizione sul pagamento
        '--------------------------------------------------------------------------------------------------------------
        If opSpecifico.Checked Then strC += " And {tabpaga.tb_tippaga}= " & NTSCInt(cbTippaga.SelectedValue)
        '--------------------------------------------------------------------------------------------------------------
        If Trim(edCodcfam.Text) <> "" Then strC += " And {" & strTabella & ".tm_codcfam} = '" & edCodcfam.Text & "'"
        '--------------------------------------------------------------------------------------------------------------
        If NTSCInt(edCodlsel.Text) > 0 Then
          If oCleStbo.RitornaLISTSEL(NTSCInt(edCodlsel.Text), dttTmp) = True Then
            strC += " And {" & strTabella & ".tm_conto} In ["
            For i = 0 To (dttTmp.Rows.Count - 1)
              If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                strC += NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
              Else
                strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                  " Or {" & strTabella & ".tm_conto} In [" & _
                  NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
              End If
            Next
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        '--- Aggiunge eventuali condizioni TC
        '--------------------------------------------------------------------------------------------------------------
        If oCleStbo.bModTCO = True Then
          If ckSelAnnoStag.Checked Then
            If CInt(edAnnotco.Text) > 0 Then
              strC += " And {" & strTabella & ".tm_annotco} = " & edAnnotco.Text
            End If
            If CInt(edCodstag.Text) > 0 Then
              strC += " And {" & strTabella & ".tm_codstag} = " & edCodstag.Text
            End If
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        '--- Aggiunge eventuali condizioni CRM
        '--------------------------------------------------------------------------------------------------------------
        strC += strAggwhereCRM
        '--------------------------------------------------------------------------------------------------------------
        If strFiltriAgg <> "" Then
          strFiltriAgg = strFiltriAgg.Replace("testmag", strTabella)
          strC += " AND " & strFiltriAgg
        End If
        '--------------------------------------------------------------------------------------------------------------
      Else
        Dim nRowsCount As Integer = oCleStbo.dsShared.Tables(strTabella).Rows.Count
        If nRowsCount <= MAX_ROWS_COUNT Then
          For Each dtrRow As DataRow In oCleStbo.dsShared.Tables(strTabella).Rows
            strC &= "({" & strTabella & ".codditt} = " & ConvStrRpt(DittaCorrente) & _
                    " AND {" & strTabella & ".tm_tipork} = " & ConvStrRpt(NTSCStr(dtrRow!tm_tipork)) & _
                    " AND {" & strTabella & ".tm_anno} = " & NTSCInt(dtrRow!tm_anno) & _
                    " AND {" & strTabella & ".tm_serie} = " & ConvStrRpt(NTSCStr(dtrRow!tm_serie)) & _
                    " AND {" & strTabella & ".tm_numdoc} = " & NTSCInt(dtrRow!tm_numdoc) & ") OR "
          Next
          If strC.Length = 0 Then
            strC = "{" & strTabella & ".tm_anno} = 1800"
          Else
            strC = strC.Remove(strC.Length - 3)
          End If
        Else
          strMess = oApp.Tr(Me, 130997626161123989, _
          "Attenzione: utilizzando filtri sul corpo del documento non è possibile prelevare più di |" & _
          MAX_ROWS_COUNT & "| documenti. La selezione attuale ne ha trovati |" & nRowsCount & "|.")
        End If
      End If

      Return strC
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return ""
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function ComponiFormula() As String
    Try
      Return ComponiFormula("")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub StampaReport(ByVal nDestin As Integer, ByVal strCrpe As String, ByVal strkey1 As String, ByVal strkey2 As String, ByVal strTipork As String, ByVal strNomRpt As String, ByVal strTitle As String)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer
    Dim j As Integer = 0
    Try
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, strkey1, strkey2, strTipork, 0, nDestin, strNomRpt, False, strTitle, False)

      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già  il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
        For j = 3 To 12
          If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
          End If
        Next j
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function TestPreStampa(Optional ByVal bCaricaDatiPerGriglia As Boolean = False) As Boolean
    Try
      Me.ValidaLastControl()

      '--- Controlla gli intervalli
      If NTSCInt(edDanumdoc.Text) > NTSCInt(edAnumdoc.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457464773159, "Il numero documento di partenza non può essere superiore a quello di arrivo."))
        Return False
      End If
      If NTSCInt(edDaconto.Text) > NTSCInt(edAconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457487684579, "Il conto di partenza non può essere superiore a quello di arrivo."))
        Return False
      End If
      If NTSCInt(edCommecaini.Text) > NTSCInt(edCommecafin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457508881539, "La commessa di partenza non può essere superiore a quella di arrivo."))
        Return False
      End If
      If NTSCDate(edDatini.Text) > NTSCDate(edDatfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457527584739, "La data di partenza non può essere superiore a quella di arrivo."))
        Return False
      End If

      '-------------------------------------------
      'filtri si 'filtri 1': se il tipo non è compatibile con il campo (esempio stringa in campo numerico) do errore ed esco
      If Not CheckFiltri1() Then Return False

      'Test sulla presenza di almeno 1 dato da stampare
      If Not CheckReports(0, bCaricaDatiPerGriglia) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128642341404531250, "Non esistono dati con queste caratteristiche."))
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckReports(Optional ByVal nTiporeport As Integer = 0, Optional ByVal bCaricaDatiPerGriglia As Boolean = False) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim bOk As Boolean = False
    Dim i As Integer = 0
    Dim strMsg As String = ""
    Try
      bOk = oCleStbo.CheckReports(NTSCInt(edDaconto.Text), NTSCInt(edAconto.Text), _
                                   NTSCInt(edAnno.Text), IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                                   NTSCDate(edDatini.Text), NTSCDate(edDatfin.Text), _
                                   NTSCInt(edDanumdoc.Text), NTSCInt(edAnumdoc.Text), _
                                   NTSCInt(edCommecaini.Text), NTSCInt(edCommecafin.Text), _
                                   ckSeldocumenti.Checked, opNoteprel.Checked, _
                                   cbTipork.SelectedValue, NTSCInt(edTipobf.Text), _
                                   NTSCInt(edCoddest.Text), NTSCInt(edCodagen.Text), NTSCInt(edCodagen2.Text), _
                                   opBolleFatturateEntrambe.Checked, opBolleFatturateSi.Checked, _
                                   opVistatiEntrambi.Checked, opVistatiSi.Checked, _
                                   opFlcontEntrambe.Checked, opFlcontSi.Checked, _
                                   edCodcfam.Text, ckSelAnnoStag.Checked, _
                                   NTSCInt(edAnnotco.Text), NTSCInt(edCodstag.Text), _
                                   ceFiltriExt.GeneraQuerySQL(), ckFlevas.Checked, _
                                   NTSCInt(IIf(opSpecifico.Checked, cbTippaga.SelectedValue, 0)), _
                                   nTiporeport, bCaricaDatiPerGriglia, NTSCInt(edCodlsel.Text))
      If bOk Then
        dtrT = oCleStbo.dsShared.Tables(oCleStbo.strNomeTabella).Select("tm_flagiva_1 = 'S'")
        If dtrT.Length > 0 Then
          For i = 0 To dtrT.Length - 1
            strMsg += vbCrLf & dtrT(i)!tm_tipork.ToString & " - " & _
                      dtrT(i)!tm_anno.ToString & " - '" & _
                      dtrT(i)!tm_serie.ToString & "' - " & _
                      dtrT(i)!tm_numdoc.ToString
            If i > 20 Then Exit For
          Next
          oApp.MsgBoxInfo(oApp.Tr(Me, 129302245721210938, "ATTENZIONE: sono presenti uno o più documenti riepilogativi" & vbCrLf & _
                "(Fatture differite emesse, Fatture differite ricevute, Note accred. diff. emesse, Note accred. diff. ricevute, Fatt. ric. fisc. differita)" & vbCrLf & _
                "che devono essere rielaborati perchè contenenti documenti modificati dopo la creazione della fattura/nota accred." & vbCrLf & _
                "Primi documenti da rielaborare:") & strMsg)
        End If
      End If

      Return bOk

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Sub edTipobf_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTipobf.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edTipobf_Validated(NTSCInt(edTipobf.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edTipobf.Text = "0"
        lbDesTipobf.Text = ""
      Else
        lbDesTipobf.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCoddest_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCoddest.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCoddest_Validated(NTSCInt(edCoddest.Text), strTmp, strError, edDaconto.Text) Then
        If Not sender Is Nothing Then oApp.MsgBoxErr(strError)
        edCoddest.Text = "0"
        lbDesdest.Text = ""
      Else
        lbDesdest.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodagen_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodagen.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCodagen_Validated(NTSCInt(edCodagen.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodagen.Text = "0"
        lbDesagen.Text = ""
      Else
        lbDesagen.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodagen2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodagen2.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCodagen_Validated(NTSCInt(edCodagen2.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodagen2.Text = "0"
        lbDesagen2.Text = ""
      Else
        lbDesagen2.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodcfam_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodcfam.Text = ""
        lbDescodcfam.Text = ""
      Else
        lbDescodcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodlsel_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodlsel.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCodlsel_Validated(NTSCInt(edCodlsel.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodlsel.Text = "0"
        lbDescodlsel.Text = ""
      Else
        lbDescodlsel.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckSelAnnoStag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSelAnnoStag.CheckedChanged
    Try
      edAnnotco.Text = NTSCStr(Year(Now))
      edCodstag.Text = "0"
      If ckSelAnnoStag.Checked Then
        GctlSetVisEnab(lbAnnotco, False)
        GctlSetVisEnab(lbCodstag, False)
        GctlSetVisEnab(edAnnotco, False)
        GctlSetVisEnab(edCodstag, False)
      Else
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodstag_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodstag.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleStbo Is Nothing Then Return
      If Not oCleStbo.edCodstag_Validated(NTSCInt(edCodstag.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodstag.Text = "0"
        lbDescodstag.Text = ""
      Else
        lbDescodstag.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAnnotco_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAnnotco.Validated
    Try
      If NTSCInt(edAnnotco.Text) = 0 Then Return

      If NTSCInt(edAnnotco.Text) < 1900 Or NTSCInt(edAnnotco.Text) > 2099 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644036870937500, "L'anno se specificato deve essere compreso fra 1900 e 2099."))
        edAnnotco.Text = "0"
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Filtri Estesi"
  Public Overridable Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
    Try
      xx_nome.Enabled = Not xx_nome.Enabled
      grvFiltri1.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function CreaDatatableFiltri() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsFiltri.Tables.Add("FILTRI1")
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_nome", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valoreda", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valorea", GetType(String))
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dsFiltri.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNMGSTBO", "RECENT", ".", "Filtri1", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsFiltri.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CheckFiltri1() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome) = "." Then
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda = ""
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea = ""
        Else
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584505571093750, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128729689900781250, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584506055937500, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503331406250, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503236718750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsFiltri.Tables("FILTRI1").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ComponiWhereFiltriEstesi(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQuery As String = ""
    Try

      If bCrystal Then
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda))
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea))
              Case 8
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).ToShortDateString)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).ToShortDateString)
              Case Else
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False)
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case 8
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case Else
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)
      End If

      strQuery += ceFiltriExt.GeneraQuerySQL()

      Return strQuery

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      strOut = CStrSQL(strTesto & bFil)
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function
  Public Overridable Sub ApriZoomTabella(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try

      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        If ckSeldocumenti.Checked And opNoteprel.Checked And (Not ckReport.Checked) Then
          oParam.strTipo = "TESTPRB"
        Else
          oParam.strTipo = "TESTMAG"
        End If
        oParam.strDescr = strCampo
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)

      Else

        SetFastZoom(NTSCStr(grvFiltri1.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub opSpecifico_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opSpecifico.CheckStateChanged
    Try
      If opSpecifico.Checked Then
        GctlSetVisEnab(cbTippaga, False)
      Else
        cbTippaga.SelectedValue = "1"
        cbTippaga.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSeldocumenti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSeldocumenti.CheckedChanged
    Try
      If ckSeldocumenti.Checked Then
        GctlSetVisEnab(opTipork, False)
        GctlSetVisEnab(cbTipork, False)
        GctlSetVisEnab(opNoteprel, False)
        GctlSetVisEnab(ckReport, False)
      Else
        opTipork.Checked = True
        cbTipork.SelectedValue = "A"
        opNoteprel.Checked = False
        ckFlevas.Checked = False
        ckReport.Checked = False

        opTipork.Enabled = False
        cbTipork.Enabled = False
        opNoteprel.Enabled = False
        ckFlevas.Visible = False
        ckReport.Enabled = False

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opNoteprel_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opNoteprel.CheckStateChanged
    Try
      If opNoteprel.Checked Then
        cbTipork.SelectedValue = "A"
        cbTipork.Enabled = False
        GctlSetVisEnab(ckFlevas, True)
      Else
        GctlSetVisEnab(cbTipork, False)
        ckFlevas.Checked = False
        ckFlevas.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If (edDaconto.Text.Trim = "") Or (edDaconto.Text = "0") Then Return
      If edDaconto.Text <> "0" Then
        edAconto.Text = edDaconto.Text
      End If
      If edAconto.Text = edDaconto.Text Then
        Dim dttTmpConto As New DataTable
        If oCleStbo.ValCodiceConto(edAconto.Text, dttTmpConto) Then
          If Not dttTmpConto Is Nothing Then
            If NTSCStr(dttTmpConto.Rows(0)!an_tipo) = "C" Or NTSCStr(dttTmpConto.Rows(0)!an_tipo) = "F" Then
              GctlSetVisEnab(edCoddest, False)
              edCoddest_Validated(Nothing, Nothing)
              Return
            End If
          End If
        End If
      End If
      edCoddest.Text = "0"
      edCoddest.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAconto.Validated
    Try
      If (edAconto.Text.Trim = "") Or (edAconto.Text = "0") Then Return
      If edAconto.Text = edDaconto.Text Then
        Dim dttTmpConto As New DataTable
        If oCleStbo.ValCodiceConto(edAconto.Text, dttTmpConto) Then
          If Not dttTmpConto Is Nothing Then
            If NTSCStr(dttTmpConto.Rows(0)!an_tipo) = "C" Or NTSCStr(dttTmpConto.Rows(0)!an_tipo) = "F" Then
              GctlSetVisEnab(edCoddest, False)
              edCoddest_Validated(Nothing, Nothing)
              Return
            End If
          End If
        End If
      End If
      edCoddest.Text = "0"
      edCoddest.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCommecaini_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommecaini.Validated
    Try
      If edCommecaini.Text <> "0" Then
        edCommecafin.Text = edCommecaini.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        edSerie.Text = " "
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If ckSerie.Checked And edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      If (oCleStbo.bModuloCRM = False) Or (oCleStbo.bIsCRMUser = False) Or (oCleStbo.bIs15 = False) Then Return True
      oMenu.ResetTblInstId("TTSUBQCRM", False, oCleStbo.lIITtsubqcrm)
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTmpTable() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      oCleStbo.RiempiTmpTable(oCleStbo.bModuloCRM, oCleStbo.bIsCRMUser)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "Gestione filtri"
  Public Overridable Sub CaricaFiltri()
    Dim dttTmp As New DataTable
    Try
      oCleStbo.CaricaFiltri(dttTmp)

      dttTmp.AcceptChanges()

      cbFiltro.DataSource = dttTmp
      cbFiltro.ValueMember = "cod"
      cbFiltro.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdApriFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriFiltri.Click
    Dim dttCampiForm As New DataTable
    Dim oPar As New CLE__CLDP
    Try
      'Prende la struttura della tabella
      oCleStbo.GetTableStructMovIfil(dttCampiForm)

      dttCampiForm.Columns.Add("xx_descr")
      dttCampiForm.Columns.Add("xx_info")
      dttCampiForm.Columns.Add("xx_tipo")

      'Compongo il datatable con i campi da passare al programma per la gestione dei dati
      If Not ComponiDatatable(dttCampiForm, Me) Then Return

      'Riempie le colonne mancanti
      For z As Integer = 0 To dttCampiForm.Rows.Count - 1
        dttCampiForm.Rows(z)!mo_child = "BNMGSTBO"
        dttCampiForm.Rows(z)!mo_form = "FRMMGSTBO"
        dttCampiForm.Rows(z)!mo_locked = "N"
        dttCampiForm.Rows(z)!mo_codifil = NTSCInt(cbFiltro.SelectedValue)
      Next

      'Avvia il programma
      oPar.ctlPar1 = dttCampiForm
      oPar.strPar1 = "BNMGSTBO"
      oPar.dPar1 = NTSCInt(cbFiltro.SelectedValue)

      oMenu.RunChild("NTSInformatica", "FRM__IFIL", oApp.Tr(Me, 129181266305000000, "Impostazione filtri"), DittaCorrente, "", "BN__IFIL", oPar, "", True, True)

      CaricaFiltri()
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbFiltro_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFiltro.SelectedValueChanged
    Try
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ComponiDatatable(ByRef dttCampiForm As DataTable, ByVal oControl As Control) As Boolean
    Dim dtrRow As DataRow
    Try
      'Verifico a quale controllo corrisponde e lo aggiungo al datatable dei campi.
      Select Case oControl.GetType().ToString
        Case "NTSInformatica.NTSButton"
          If oControl.Name = "cmdApriFiltri" Then Return True ' è un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = oControl.Text
            !mo_control = oControl.Name
            !mo_valore = ""
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxNum"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxNum).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxNum).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxNum"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxStr"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxStr).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxStr).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxStr"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxData"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxData).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxData"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSMemoBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSMemoBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSMemoBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSCheckBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSCheckBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSCheckBox).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSCheckBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSComboBox"
          If oControl.Name = "cbFiltro" Then Return True ' è un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSComboBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = CType(oControl, NTSComboBox).SelectedValue
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSComboBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSRadioButton"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSRadioButton).strNomeCampo
            !xx_info = CType(oControl, NTSRadioButton).Parent.Name
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSRadioButton).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSRadioButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSGrid"
          Select Case oControl.Name
            'Le griglie le gestisco a mano
            Case "grFilt"
              For z As Integer = 0 To ceFiltriExt.grvFilt.RowCount - 1
                dtrRow = ceFiltriExt.grvFilt.GetDataRow(z)
                If NTSCStr(dtrRow!xx_nome) = "-" Then Continue For

                dttCampiForm.Rows.Add()


                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_valore"
                  !xx_descr = oApp.Tr(Me, 129182143688273058, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo)
                  !mo_valore = dtrRow!xx_valore
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With

                dttCampiForm.Rows.Add()
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_valorea"
                  !xx_descr = oApp.Tr(Me, 129181522451445167, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo) & " (A)"
                  !mo_valore = dtrRow!xx_valorea
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With

                dttCampiForm.Rows.Add()
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_tipo"
                  !xx_descr = oApp.Tr(Me, 130606857219845115, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo) & " (Tipo operatore)"
                  !mo_valore = dtrRow!xx_tipo
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With
              Next
            Case Else
              'Avviso se è una griglia non gestita
              'oApp.MsgBoxErr(oApp.Tr(Me, 129181211327812500, "Griglia '|" & oControl.Name & "|' non gestita"))
          End Select
          Return True
      End Select

      'Ricorsivamente scorro tutti i controlli
      For z As Integer = 0 To oControl.Controls.Count - 1
        If Not ComponiDatatable(dttCampiForm, oControl.Controls(z)) Then Return False
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApplicaFiltro(ByVal lCod As Integer) As Boolean
    Try
      If Me.NTSActiveFirstOccured = False Then Return True
      'Ripristina lo stato di default dei controlli
      GctlSetRoules()
      AggiornaForm(dttDefault, True)

      If lCod <> 0 Then
        'Carica il filtro e lo applica (la gctlSetRoules vince sempre)
        If Not oCleStbo.LeggiFiltro(lCod, "BNMGSTBO", "FRMMGSTBO", dttPersForm) Then Return False
        AggiornaForm(dttPersForm, False)

        If Not grvFiltri1.NTSGetCurrentDataRow() Is Nothing Then grvFiltri1_NTSFocusedRowChanged(Me, Nothing)
      End If

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaForm(ByVal dttControl As DataTable, ByVal bDoEnable As Boolean) As Boolean
    Dim oControl As New Object
    Try
      For z As Integer = 0 To dttControl.Rows.Count - 1
        oControl = NTSFindControlByName(Me, NTSCStr(dttControl.Rows(z)!mo_control))
        If oControl Is Nothing Then Continue For

        Select Case oControl.GetType().ToString
          Case "NTSInformatica.NTSButton"
            Dim cmdControl As NTSButton = CType(oControl, NTSButton)
            If cmdControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cmdControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxNum"
            Dim edControl As NTSTextBoxNum = CType(oControl, NTSTextBoxNum)
            edControl.Text = NTSCInt(dttControl.Rows(z)!mo_valore).ToString
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxStr"
            Dim edControl As NTSTextBoxStr = CType(oControl, NTSTextBoxStr)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxData"
            Dim edControl As NTSTextBoxData = CType(oControl, NTSTextBoxData)
            edControl.Text = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSMemoBox"
            Dim edControl As NTSMemoBox = CType(oControl, NTSMemoBox)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSCheckBox"
            Dim ckControl As NTSCheckBox = CType(oControl, NTSCheckBox)
            ckControl.Checked = CBool(dttControl.Rows(z)!mo_valore)
            If ckControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              ckControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSComboBox"
            Dim cbControl As NTSComboBox = CType(oControl, NTSComboBox)
            cbControl.SelectedValue = NTSCStr(dttControl.Rows(z)!mo_valore)
            If cbControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cbControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSRadioButton"
            Dim opControl As NTSRadioButton = CType(oControl, NTSRadioButton)
            If CBool(dttControl.Rows(z)!mo_valore) Then opControl.Checked = True
            If opControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              opControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSGrid"
            Dim grControl As NTSGrid = CType(oControl, NTSGrid)
            Dim dttTmp As DataTable = CType(CType(grControl.DataSource, BindingSource).DataSource, DataTable)
            Dim dtrRow() As DataRow

            dtrRow = dttTmp.Select(NTSCStr(dttControl.Rows(z)!mo_colkeyname) & " = '" & NTSCStr(dttControl.Rows(z)!mo_colkeyvalue) & "'")
            If dtrRow.Length > 0 Then
              'Se è di tipo data faccio la conversione, altrimenti no
              Dim dtrTmp() As DataRow = ceFiltriExt.dttFilt.Select("xx_nome = " & CStrSQL(dttControl.Rows(z)!mo_colkeyvalue))
              If dtrTmp.Length > 0 Then
                If NTSCInt(dtrTmp(0)!xx_tipocampo) = 8 Then ' 8 = data
                  dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
                Else
                  dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = NTSCStr(dttControl.Rows(z)!mo_valore)
                End If
              End If
            End If
            ' L'enable\disable dei vari campi lo gestisco con lo spostamento nella griglia
        End Select
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvertiInData(ByVal strData As String) As String
    Dim strPart() As String
    Try
      strData = strData.Trim

      'Data odierna o oggi è quella del giorno di stampa
      If strData.ToLower = "data odierna" Or strData.ToLower = "oggi" Or _
         strData.ToLower = "today" Or strData.ToLower = "now" Or _
         strData.ToLower = oApp.Tr(Me, 129223786674775598, "data odierna").ToLower Or _
         strData.ToLower = oApp.Tr(Me, 129223786568213098, "oggi").ToLower Then Return Now.ToShortDateString

      'Non è di tipo "+1 Giorno", ecc... tutti con uno spazio in mezzo, quindi è una data classica
      strPart = strData.Split(" "c)

      If strPart.Length = 1 Then Return strData

      'dovrebbero essere supportati solo i nomi italiani, ma aggiungerne anche altri non costa niente.
      Select Case strPart(1).ToLower
        Case "giorni", "giorno", "g", "day", "days", "d", oApp.Tr(Me, 129223783990556848, "giorni").ToLower, oApp.Tr(Me, 129223784107275598, "giorno").ToLower
          Return Now.AddDays(NTSCInt(strPart(0))).ToShortDateString
        Case "mese", "mesi", "month", "months", "m", oApp.Tr(Me, 129223784378681848, "mese").ToLower, oApp.Tr(Me, 129223784395244348, "mesi").ToLower
          Return Now.AddMonths(NTSCInt(strPart(0))).ToShortDateString
        Case "anno", "anni", "a", "year", "years", "y", oApp.Tr(Me, 129223784387119348, "anno").ToLower, oApp.Tr(Me, 129223784402744348, "anni").ToLower
          Return Now.AddYears(NTSCInt(strPart(0))).ToShortDateString
      End Select

      'Se non riesce a codificarla in nulla ritorna la data di oggi e avvisa
      oApp.MsgBoxErr(oApp.Tr(Me, 129223785065556848, "Non è stato possibile tradurre la data '|" & strData & "|'"))
      Return Now.ToShortDateString
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub grvFiltri1_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFiltri1.NTSFocusedRowChanged
    Dim dtrRow() As DataRow
    Try
      ' se non c'è una impostazione attiva le colonne devono rimanere come sono
      If NTSCInt(cbFiltro.SelectedValue) = 0 Then Return

      dtrRow = dttPersForm.Select("mo_colkeyvalue = '" & NTSCStr(grvFiltri1.NTSGetCurrentDataRow()!xx_nome) & "'")

      For z As Integer = 0 To dtrRow.Length - 1
        CType(grvFiltri1.Columns(NTSCStr(dtrRow(z)!mo_colvalue)), NTSGridColumn).Enabled = CBool(IIf(NTSCStr(dtrRow(z)!mo_locked) = "S", False, True))
      Next

    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Sub
#End Region
End Class

