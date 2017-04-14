Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SOTC
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

  Public oCleSotc As CLE__SOTC
  Public dsSotc As DataSet
  Public oCallParams As CLE__CLDP
  Public dcSotc As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__SOTC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPartitario = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCambiaDitta = New NTSInformatica.NTSBarButtonItem
    Me.tlbAnacent = New NTSInformatica.NTSBarMenuItem
    Me.tlbAnalink = New NTSInformatica.NTSBarMenuItem
    Me.tlbAnacent2 = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tsSotc = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.lbAn_ivainded = New NTSInformatica.NTSLabel
    Me.ckAn_ivainded = New NTSInformatica.NTSCheckBox
    Me.lbAn_codpconLabel = New NTSInformatica.NTSLabel
    Me.lbAn_codpcon = New NTSInformatica.NTSLabel
    Me.lbHelp2 = New NTSInformatica.NTSLabel
    Me.lbHelp = New NTSInformatica.NTSLabel
    Me.lbXx_valuta = New NTSInformatica.NTSLabel
    Me.edAn_valuta = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_valuta = New NTSInformatica.NTSLabel
    Me.lbXx_pccontodescr = New NTSInformatica.NTSLabel
    Me.edAn_pcconto = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_pccontoLabel = New NTSInformatica.NTSLabel
    Me.fmCollegamenti = New NTSInformatica.NTSGroupBox
    Me.ckAn_sosppr = New NTSInformatica.NTSCheckBox
    Me.ckAn_flci = New NTSInformatica.NTSCheckBox
    Me.fmComportamento = New NTSInformatica.NTSGroupBox
    Me.ckAn_partite = New NTSInformatica.NTSCheckBox
    Me.ckAn_scaden = New NTSInformatica.NTSCheckBox
    Me.fmValidita = New NTSInformatica.NTSGroupBox
    Me.edAn_datini = New NTSInformatica.NTSTextBoxData
    Me.lbAn_datfin = New NTSInformatica.NTSLabel
    Me.edAn_datfin = New NTSInformatica.NTSTextBoxData
    Me.lbAn_datini = New NTSInformatica.NTSLabel
    Me.lbXx_funzion = New NTSInformatica.NTSLabel
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbAn_contropLabel = New NTSInformatica.NTSLabel
    Me.edAn_controp = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_accperi = New NTSInformatica.NTSLabel
    Me.cbAn_accperi = New NTSInformatica.NTSComboBox
    Me.lbAn_note = New NTSInformatica.NTSLabel
    Me.edAn_note = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_funzionLabel = New NTSInformatica.NTSLabel
    Me.edAn_funzion = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_cksegno = New NTSInformatica.NTSLabel
    Me.cbAn_cksegno = New NTSInformatica.NTSComboBox
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPag2 = New NTSInformatica.NTSPanel
    Me.pnDati2Top = New NTSInformatica.NTSPanel
    Me.lbAn_tipacq = New NTSInformatica.NTSLabel
    Me.cbAn_cosvend = New NTSInformatica.NTSComboBox
    Me.lbAn_cosvend = New NTSInformatica.NTSLabel
    Me.cbAn_tipacq = New NTSInformatica.NTSComboBox
    Me.cbAn_colbil = New NTSInformatica.NTSComboBox
    Me.lbAn_conprof = New NTSInformatica.NTSLabel
    Me.lbAn_colbil = New NTSInformatica.NTSLabel
    Me.cbAn_conprof = New NTSInformatica.NTSComboBox
    Me.edAn_percman = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_totcron = New NTSInformatica.NTSLabel
    Me.lbAn_percman = New NTSInformatica.NTSLabel
    Me.cbAn_totcron = New NTSInformatica.NTSComboBox
    Me.cbAn_manrip = New NTSInformatica.NTSComboBox
    Me.lbAn_contrsemp = New NTSInformatica.NTSLabel
    Me.lbAn_manrip = New NTSInformatica.NTSLabel
    Me.cbAn_contrsemp = New NTSInformatica.NTSComboBox
    Me.fmRiclassificati = New NTSInformatica.NTSGroupBox
    Me.ckOpzgest1 = New NTSInformatica.NTSCheckBox
    Me.cmdRiclassificazioni = New NTSInformatica.NTSButton
    Me.lbRiclassif = New NTSInformatica.NTSLabel
    Me.lbCee = New NTSInformatica.NTSLabel
    Me.edAn_kpccee = New NTSInformatica.NTSTextBoxStr
    Me.edAn_kpccee2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_kpccee2 = New NTSInformatica.NTSLabel
    Me.lbAn_kpccee = New NTSInformatica.NTSLabel
    Me.lbAn_rifrica = New NTSInformatica.NTSLabel
    Me.edAn_rifrica = New NTSInformatica.NTSTextBoxStr
    Me.edAn_rifricd = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_rifricd = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTesoreria = New NTSInformatica.NTSPanel
    Me.fmTesoreria = New NTSInformatica.NTSGroupBox
    Me.lbXx_codvfde = New NTSInformatica.NTSLabel
    Me.cbAn_trating = New NTSInformatica.NTSComboBox
    Me.edAn_codvfde = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codvfde = New NTSInformatica.NTSLabel
    Me.lbAn_trating = New NTSInformatica.NTSLabel
    Me.pnPag3 = New NTSInformatica.NTSPanel
    Me.fmUsaContoFunz = New NTSInformatica.NTSGroupBox
    Me.ckOpzgest6 = New NTSInformatica.NTSCheckBox
    Me.ckOpzgest5 = New NTSInformatica.NTSCheckBox
    Me.ckOpzgest4 = New NTSInformatica.NTSCheckBox
    Me.ckOpzgest3 = New NTSInformatica.NTSCheckBox
    Me.ckOpzgest2 = New NTSInformatica.NTSCheckBox
    Me.fmStudi = New NTSInformatica.NTSGroupBox
    Me.lbAn_stseimp = New NTSInformatica.NTSLabel
    Me.cbAn_stseimp = New NTSInformatica.NTSComboBox
    Me.lbAn_stsepro = New NTSInformatica.NTSLabel
    Me.cbAn_stsepro = New NTSInformatica.NTSComboBox
    Me.fmRicavometro = New NTSInformatica.NTSGroupBox
    Me.ckAn_ricmimp = New NTSInformatica.NTSCheckBox
    Me.ckAn_ricmpro = New NTSInformatica.NTSCheckBox
    Me.fmImposte = New NTSInformatica.NTSGroupBox
    Me.edAn_indiidd = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_indiidd = New NTSInformatica.NTSLabel
    Me.edAn_indiiddsit = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_indiiddsit = New NTSInformatica.NTSLabel
    Me.ckAn_intragr = New NTSInformatica.NTSCheckBox
    Me.lbAn_azcom = New NTSInformatica.NTSLabel
    Me.fmIrap = New NTSInformatica.NTSGroupBox
    Me.lbXx_voceirap = New NTSInformatica.NTSLabel
    Me.edAn_pervari = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_pervari = New NTSInformatica.NTSLabel
    Me.lbAn_varirap = New NTSInformatica.NTSLabel
    Me.edAn_voceirap = New NTSInformatica.NTSTextBoxNum
    Me.cbAn_varirap = New NTSInformatica.NTSComboBox
    Me.lbAn_voceirapLabel = New NTSInformatica.NTSLabel
    Me.edAn_indirap = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_indirap = New NTSInformatica.NTSLabel
    Me.cbAn_azcom = New NTSInformatica.NTSComboBox
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPag4 = New NTSInformatica.NTSPanel
    Me.edAn_note2 = New NTSInformatica.NTSMemoBox
    Me.lbAn_contoLabel = New NTSInformatica.NTSLabel
    Me.edAn_descr1 = New NTSInformatica.NTSTextBoxStr
    Me.edAn_descr2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codmastLabel = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.ckAn_fldespdc = New NTSInformatica.NTSCheckBox
    Me.lbAn_conto = New NTSInformatica.NTSLabel
    Me.lbAn_codmast = New NTSInformatica.NTSLabel
    Me.lbXx_codmast = New NTSInformatica.NTSLabel
    Me.lbAn_descr2 = New NTSInformatica.NTSLabel
    Me.lbAn_descr1 = New NTSInformatica.NTSLabel
    Me.pnMain = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsSotc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSotc.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.ckAn_ivainded.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_valuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_pcconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCollegamenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCollegamenti.SuspendLayout()
    CType(Me.ckAn_sosppr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_flci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmComportamento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmComportamento.SuspendLayout()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValidita.SuspendLayout()
    CType(Me.edAn_datini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_datfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_accperi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_funzion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_cksegno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2.SuspendLayout()
    CType(Me.pnDati2Top, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDati2Top.SuspendLayout()
    CType(Me.cbAn_cosvend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_tipacq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_conprof.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_percman.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_totcron.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_manrip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_contrsemp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRiclassificati.SuspendLayout()
    CType(Me.ckOpzgest1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnTesoreria, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTesoreria.SuspendLayout()
    CType(Me.fmTesoreria, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTesoreria.SuspendLayout()
    CType(Me.cbAn_trating.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_codvfde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag3.SuspendLayout()
    CType(Me.fmUsaContoFunz, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUsaContoFunz.SuspendLayout()
    CType(Me.ckOpzgest6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpzgest5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpzgest4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpzgest3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpzgest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStudi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStudi.SuspendLayout()
    CType(Me.cbAn_stseimp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_stsepro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRicavometro, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRicavometro.SuspendLayout()
    CType(Me.ckAn_ricmimp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_ricmpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmImposte, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmImposte.SuspendLayout()
    CType(Me.edAn_indiidd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_indiiddsit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_intragr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIrap, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIrap.SuspendLayout()
    CType(Me.edAn_pervari.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_voceirap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_varirap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_indirap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_azcom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4.SuspendLayout()
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_descr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.ckAn_fldespdc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbCambiaDitta, Me.tlbApri, Me.tlbPartitario, Me.tlbAnacent, Me.tlbAnalink, Me.tlbAnacent2, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 34
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPartitario, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 26
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbPartitario
    '
    Me.tlbPartitario.Caption = "Partitario"
    Me.tlbPartitario.Glyph = CType(resources.GetObject("tlbPartitario.Glyph"), System.Drawing.Image)
    Me.tlbPartitario.GlyphPath = ""
    Me.tlbPartitario.Id = 27
    Me.tlbPartitario.Name = "tlbPartitario"
    Me.tlbPartitario.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambiaDitta, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAnacent, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAnalink), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAnacent2), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambiaDitta
    '
    Me.tlbCambiaDitta.Caption = "Cambia Ditta"
    Me.tlbCambiaDitta.GlyphPath = ""
    Me.tlbCambiaDitta.Id = 25
    Me.tlbCambiaDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambiaDitta.Name = "tlbCambiaDitta"
    Me.tlbCambiaDitta.Visible = True
    '
    'tlbAnacent
    '
    Me.tlbAnacent.Caption = "Ripartizioni CA"
    Me.tlbAnacent.GlyphPath = ""
    Me.tlbAnacent.Id = 28
    Me.tlbAnacent.Name = "tlbAnacent"
    Me.tlbAnacent.NTSIsCheckBox = False
    Me.tlbAnacent.Visible = True
    '
    'tlbAnalink
    '
    Me.tlbAnalink.Caption = "Collegamento con Pdc di CA"
    Me.tlbAnalink.GlyphPath = ""
    Me.tlbAnalink.Id = 29
    Me.tlbAnalink.Name = "tlbAnalink"
    Me.tlbAnalink.NTSIsCheckBox = False
    Me.tlbAnalink.Visible = True
    '
    'tlbAnacent2
    '
    Me.tlbAnacent2.Caption = "Ripartizioni CA DC"
    Me.tlbAnacent2.GlyphPath = ""
    Me.tlbAnacent2.Id = 30
    Me.tlbAnacent2.Name = "tlbAnacent2"
    Me.tlbAnacent2.NTSIsCheckBox = False
    Me.tlbAnacent2.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 31
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 32
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 33
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'tsSotc
    '
    Me.tsSotc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSotc.Location = New System.Drawing.Point(0, 0)
    Me.tsSotc.Name = "tsSotc"
    Me.tsSotc.SelectedTabPage = Me.NtsTabPage1
    Me.tsSotc.Size = New System.Drawing.Size(670, 323)
    Me.tsSotc.TabIndex = 4
    Me.tsSotc.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4})
    Me.tsSotc.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(661, 293)
    Me.NtsTabPage1.Text = "&1- Generale"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.lbAn_ivainded)
    Me.pnPag1.Controls.Add(Me.ckAn_ivainded)
    Me.pnPag1.Controls.Add(Me.lbAn_codpconLabel)
    Me.pnPag1.Controls.Add(Me.lbAn_codpcon)
    Me.pnPag1.Controls.Add(Me.lbHelp2)
    Me.pnPag1.Controls.Add(Me.lbHelp)
    Me.pnPag1.Controls.Add(Me.lbXx_valuta)
    Me.pnPag1.Controls.Add(Me.edAn_valuta)
    Me.pnPag1.Controls.Add(Me.lbAn_valuta)
    Me.pnPag1.Controls.Add(Me.lbXx_pccontodescr)
    Me.pnPag1.Controls.Add(Me.edAn_pcconto)
    Me.pnPag1.Controls.Add(Me.lbAn_pccontoLabel)
    Me.pnPag1.Controls.Add(Me.fmCollegamenti)
    Me.pnPag1.Controls.Add(Me.fmComportamento)
    Me.pnPag1.Controls.Add(Me.fmValidita)
    Me.pnPag1.Controls.Add(Me.lbXx_funzion)
    Me.pnPag1.Controls.Add(Me.lbXx_controp)
    Me.pnPag1.Controls.Add(Me.lbAn_contropLabel)
    Me.pnPag1.Controls.Add(Me.edAn_controp)
    Me.pnPag1.Controls.Add(Me.lbAn_accperi)
    Me.pnPag1.Controls.Add(Me.cbAn_accperi)
    Me.pnPag1.Controls.Add(Me.lbAn_note)
    Me.pnPag1.Controls.Add(Me.edAn_note)
    Me.pnPag1.Controls.Add(Me.lbAn_funzionLabel)
    Me.pnPag1.Controls.Add(Me.edAn_funzion)
    Me.pnPag1.Controls.Add(Me.lbAn_cksegno)
    Me.pnPag1.Controls.Add(Me.cbAn_cksegno)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(661, 293)
    Me.pnPag1.TabIndex = 526
    Me.pnPag1.Text = "NtsPanel2"
    '
    'lbAn_ivainded
    '
    Me.lbAn_ivainded.AutoSize = True
    Me.lbAn_ivainded.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_ivainded.Location = New System.Drawing.Point(11, 140)
    Me.lbAn_ivainded.Name = "lbAn_ivainded"
    Me.lbAn_ivainded.NTSDbField = ""
    Me.lbAn_ivainded.Size = New System.Drawing.Size(93, 13)
    Me.lbAn_ivainded.TabIndex = 591
    Me.lbAn_ivainded.Text = "Gest. Iva indetr.*"
    Me.lbAn_ivainded.Tooltip = ""
    Me.lbAn_ivainded.UseMnemonic = False
    '
    'ckAn_ivainded
    '
    Me.ckAn_ivainded.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_ivainded.Location = New System.Drawing.Point(126, 138)
    Me.ckAn_ivainded.Name = "ckAn_ivainded"
    Me.ckAn_ivainded.NTSCheckValue = "S"
    Me.ckAn_ivainded.NTSUnCheckValue = "N"
    Me.ckAn_ivainded.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_ivainded.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_ivainded.Properties.AutoHeight = False
    Me.ckAn_ivainded.Properties.Caption = "(se spuntato il conto può ricevere il giroconto IVA indetr. in automatico da prim" & _
        "a nota)"
    Me.ckAn_ivainded.Size = New System.Drawing.Size(447, 19)
    Me.ckAn_ivainded.TabIndex = 590
    '
    'lbAn_codpconLabel
    '
    Me.lbAn_codpconLabel.AutoSize = True
    Me.lbAn_codpconLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpconLabel.Location = New System.Drawing.Point(518, 10)
    Me.lbAn_codpconLabel.Name = "lbAn_codpconLabel"
    Me.lbAn_codpconLabel.NTSDbField = ""
    Me.lbAn_codpconLabel.Size = New System.Drawing.Size(27, 13)
    Me.lbAn_codpconLabel.TabIndex = 511
    Me.lbAn_codpconLabel.Text = "PDC"
    Me.lbAn_codpconLabel.Tooltip = ""
    Me.lbAn_codpconLabel.UseMnemonic = False
    '
    'lbAn_codpcon
    '
    Me.lbAn_codpcon.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codpcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_codpcon.Location = New System.Drawing.Point(551, 7)
    Me.lbAn_codpcon.Name = "lbAn_codpcon"
    Me.lbAn_codpcon.NTSDbField = ""
    Me.lbAn_codpcon.Size = New System.Drawing.Size(100, 20)
    Me.lbAn_codpcon.TabIndex = 512
    Me.lbAn_codpcon.Text = "lbAn_codpcon"
    Me.lbAn_codpcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbAn_codpcon.Tooltip = ""
    Me.lbAn_codpcon.UseMnemonic = False
    '
    'lbHelp2
    '
    Me.lbHelp2.AutoSize = True
    Me.lbHelp2.BackColor = System.Drawing.Color.Transparent
    Me.lbHelp2.Location = New System.Drawing.Point(235, 10)
    Me.lbHelp2.Name = "lbHelp2"
    Me.lbHelp2.NTSDbField = ""
    Me.lbHelp2.Size = New System.Drawing.Size(270, 13)
    Me.lbHelp2.TabIndex = 589
    Me.lbHelp2.Text = "Se diverso da 0 il conto è utilizzato da tutte le ditte con"
    Me.lbHelp2.Tooltip = ""
    Me.lbHelp2.UseMnemonic = False
    '
    'lbHelp
    '
    Me.lbHelp.AutoSize = True
    Me.lbHelp.BackColor = System.Drawing.Color.Transparent
    Me.lbHelp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbHelp.Location = New System.Drawing.Point(309, 100)
    Me.lbHelp.Name = "lbHelp"
    Me.lbHelp.NTSDbField = ""
    Me.lbHelp.Size = New System.Drawing.Size(318, 26)
    Me.lbHelp.TabIndex = 588
    Me.lbHelp.Text = "I campi marcati con * sono sempre specifici per la ditta" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "corrente, anche se il c" & _
        "onto è utilizzabile da più ditte"
    Me.lbHelp.Tooltip = ""
    Me.lbHelp.UseMnemonic = False
    '
    'lbXx_valuta
    '
    Me.lbXx_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_valuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_valuta.Location = New System.Drawing.Point(235, 158)
    Me.lbXx_valuta.Name = "lbXx_valuta"
    Me.lbXx_valuta.NTSDbField = ""
    Me.lbXx_valuta.Size = New System.Drawing.Size(416, 20)
    Me.lbXx_valuta.TabIndex = 587
    Me.lbXx_valuta.Text = "lbXx_valuta"
    Me.lbXx_valuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_valuta.Tooltip = ""
    Me.lbXx_valuta.UseMnemonic = False
    '
    'edAn_valuta
    '
    Me.edAn_valuta.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_valuta.EditValue = "0"
    Me.edAn_valuta.Location = New System.Drawing.Point(129, 158)
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
    Me.edAn_valuta.Size = New System.Drawing.Size(100, 20)
    Me.edAn_valuta.TabIndex = 586
    '
    'lbAn_valuta
    '
    Me.lbAn_valuta.AutoSize = True
    Me.lbAn_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_valuta.Location = New System.Drawing.Point(11, 161)
    Me.lbAn_valuta.Name = "lbAn_valuta"
    Me.lbAn_valuta.NTSDbField = ""
    Me.lbAn_valuta.Size = New System.Drawing.Size(46, 13)
    Me.lbAn_valuta.TabIndex = 585
    Me.lbAn_valuta.Text = "Valuta *"
    Me.lbAn_valuta.Tooltip = ""
    Me.lbAn_valuta.UseMnemonic = False
    '
    'lbXx_pccontodescr
    '
    Me.lbXx_pccontodescr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_pccontodescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_pccontodescr.Location = New System.Drawing.Point(235, 7)
    Me.lbXx_pccontodescr.Name = "lbXx_pccontodescr"
    Me.lbXx_pccontodescr.NTSDbField = ""
    Me.lbXx_pccontodescr.Size = New System.Drawing.Size(25, 20)
    Me.lbXx_pccontodescr.TabIndex = 584
    Me.lbXx_pccontodescr.Text = "lbXx_pccontodescr"
    Me.lbXx_pccontodescr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_pccontodescr.Tooltip = ""
    Me.lbXx_pccontodescr.UseMnemonic = False
    Me.lbXx_pccontodescr.Visible = False
    '
    'edAn_pcconto
    '
    Me.edAn_pcconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_pcconto.EditValue = "0"
    Me.edAn_pcconto.Enabled = False
    Me.edAn_pcconto.Location = New System.Drawing.Point(129, 7)
    Me.edAn_pcconto.Name = "edAn_pcconto"
    Me.edAn_pcconto.NTSDbField = ""
    Me.edAn_pcconto.NTSFormat = "0"
    Me.edAn_pcconto.NTSForzaVisZoom = False
    Me.edAn_pcconto.NTSOldValue = ""
    Me.edAn_pcconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_pcconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_pcconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_pcconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_pcconto.Properties.AutoHeight = False
    Me.edAn_pcconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_pcconto.Properties.MaxLength = 65536
    Me.edAn_pcconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_pcconto.Size = New System.Drawing.Size(100, 20)
    Me.edAn_pcconto.TabIndex = 583
    '
    'lbAn_pccontoLabel
    '
    Me.lbAn_pccontoLabel.AutoSize = True
    Me.lbAn_pccontoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_pccontoLabel.Location = New System.Drawing.Point(10, 10)
    Me.lbAn_pccontoLabel.Name = "lbAn_pccontoLabel"
    Me.lbAn_pccontoLabel.NTSDbField = ""
    Me.lbAn_pccontoLabel.Size = New System.Drawing.Size(108, 13)
    Me.lbAn_pccontoLabel.TabIndex = 582
    Me.lbAn_pccontoLabel.Text = "Conto piano dei conti"
    Me.lbAn_pccontoLabel.Tooltip = ""
    Me.lbAn_pccontoLabel.UseMnemonic = False
    '
    'fmCollegamenti
    '
    Me.fmCollegamenti.AllowDrop = True
    Me.fmCollegamenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCollegamenti.Appearance.Options.UseBackColor = True
    Me.fmCollegamenti.Controls.Add(Me.ckAn_sosppr)
    Me.fmCollegamenti.Controls.Add(Me.ckAn_flci)
    Me.fmCollegamenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCollegamenti.Location = New System.Drawing.Point(195, 210)
    Me.fmCollegamenti.Name = "fmCollegamenti"
    Me.fmCollegamenti.Size = New System.Drawing.Size(242, 72)
    Me.fmCollegamenti.TabIndex = 581
    Me.fmCollegamenti.Text = "Collegamenti"
    '
    'ckAn_sosppr
    '
    Me.ckAn_sosppr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_sosppr.Location = New System.Drawing.Point(14, 48)
    Me.ckAn_sosppr.Name = "ckAn_sosppr"
    Me.ckAn_sosppr.NTSCheckValue = "S"
    Me.ckAn_sosppr.NTSUnCheckValue = "N"
    Me.ckAn_sosppr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_sosppr.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_sosppr.Properties.AutoHeight = False
    Me.ckAn_sosppr.Properties.Caption = "Collegamento a costi/ricavi sospesi"
    Me.ckAn_sosppr.Size = New System.Drawing.Size(202, 19)
    Me.ckAn_sosppr.TabIndex = 573
    '
    'ckAn_flci
    '
    Me.ckAn_flci.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_flci.Location = New System.Drawing.Point(14, 23)
    Me.ckAn_flci.Name = "ckAn_flci"
    Me.ckAn_flci.NTSCheckValue = "S"
    Me.ckAn_flci.NTSUnCheckValue = "N"
    Me.ckAn_flci.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_flci.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_flci.Properties.AutoHeight = False
    Me.ckAn_flci.Properties.Caption = "Collegamento con PDC di C.A."
    Me.ckAn_flci.Size = New System.Drawing.Size(176, 19)
    Me.ckAn_flci.TabIndex = 574
    '
    'fmComportamento
    '
    Me.fmComportamento.AllowDrop = True
    Me.fmComportamento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmComportamento.Appearance.Options.UseBackColor = True
    Me.fmComportamento.Controls.Add(Me.ckAn_partite)
    Me.fmComportamento.Controls.Add(Me.ckAn_scaden)
    Me.fmComportamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmComportamento.Location = New System.Drawing.Point(13, 210)
    Me.fmComportamento.Name = "fmComportamento"
    Me.fmComportamento.Size = New System.Drawing.Size(169, 72)
    Me.fmComportamento.TabIndex = 580
    Me.fmComportamento.Text = "Comportamento"
    '
    'ckAn_partite
    '
    Me.ckAn_partite.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_partite.Location = New System.Drawing.Point(14, 23)
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
    'ckAn_scaden
    '
    Me.ckAn_scaden.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_scaden.Location = New System.Drawing.Point(14, 48)
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
    'fmValidita
    '
    Me.fmValidita.AllowDrop = True
    Me.fmValidita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValidita.Appearance.Options.UseBackColor = True
    Me.fmValidita.Controls.Add(Me.edAn_datini)
    Me.fmValidita.Controls.Add(Me.lbAn_datfin)
    Me.fmValidita.Controls.Add(Me.edAn_datfin)
    Me.fmValidita.Controls.Add(Me.lbAn_datini)
    Me.fmValidita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValidita.Location = New System.Drawing.Point(448, 210)
    Me.fmValidita.Name = "fmValidita"
    Me.fmValidita.Size = New System.Drawing.Size(203, 72)
    Me.fmValidita.TabIndex = 579
    Me.fmValidita.Text = "Validità"
    '
    'edAn_datini
    '
    Me.edAn_datini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_datini.EditValue = "01/01/1900"
    Me.edAn_datini.Location = New System.Drawing.Point(99, 23)
    Me.edAn_datini.Name = "edAn_datini"
    Me.edAn_datini.NTSDbField = ""
    Me.edAn_datini.NTSForzaVisZoom = False
    Me.edAn_datini.NTSOldValue = ""
    Me.edAn_datini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_datini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_datini.Properties.AutoHeight = False
    Me.edAn_datini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_datini.Properties.MaxLength = 65536
    Me.edAn_datini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_datini.Size = New System.Drawing.Size(89, 20)
    Me.edAn_datini.TabIndex = 577
    '
    'lbAn_datfin
    '
    Me.lbAn_datfin.AutoSize = True
    Me.lbAn_datfin.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_datfin.Location = New System.Drawing.Point(14, 50)
    Me.lbAn_datfin.Name = "lbAn_datfin"
    Me.lbAn_datfin.NTSDbField = ""
    Me.lbAn_datfin.Size = New System.Drawing.Size(51, 13)
    Me.lbAn_datfin.TabIndex = 576
    Me.lbAn_datfin.Text = "Data fine"
    Me.lbAn_datfin.Tooltip = ""
    Me.lbAn_datfin.UseMnemonic = False
    '
    'edAn_datfin
    '
    Me.edAn_datfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_datfin.EditValue = "31/12/2099"
    Me.edAn_datfin.Location = New System.Drawing.Point(99, 47)
    Me.edAn_datfin.Name = "edAn_datfin"
    Me.edAn_datfin.NTSDbField = ""
    Me.edAn_datfin.NTSForzaVisZoom = False
    Me.edAn_datfin.NTSOldValue = ""
    Me.edAn_datfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_datfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_datfin.Properties.AutoHeight = False
    Me.edAn_datfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_datfin.Properties.MaxLength = 65536
    Me.edAn_datfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_datfin.Size = New System.Drawing.Size(89, 20)
    Me.edAn_datfin.TabIndex = 578
    '
    'lbAn_datini
    '
    Me.lbAn_datini.AutoSize = True
    Me.lbAn_datini.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_datini.Location = New System.Drawing.Point(14, 25)
    Me.lbAn_datini.Name = "lbAn_datini"
    Me.lbAn_datini.NTSDbField = ""
    Me.lbAn_datini.Size = New System.Drawing.Size(56, 13)
    Me.lbAn_datini.TabIndex = 575
    Me.lbAn_datini.Text = "Data inizio"
    Me.lbAn_datini.Tooltip = ""
    Me.lbAn_datini.UseMnemonic = False
    '
    'lbXx_funzion
    '
    Me.lbXx_funzion.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_funzion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_funzion.Location = New System.Drawing.Point(235, 64)
    Me.lbXx_funzion.Name = "lbXx_funzion"
    Me.lbXx_funzion.NTSDbField = ""
    Me.lbXx_funzion.Size = New System.Drawing.Size(416, 20)
    Me.lbXx_funzion.TabIndex = 571
    Me.lbXx_funzion.Text = "lbXx_funzion"
    Me.lbXx_funzion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_funzion.Tooltip = ""
    Me.lbXx_funzion.UseMnemonic = False
    '
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(235, 40)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(416, 20)
    Me.lbXx_controp.TabIndex = 570
    Me.lbXx_controp.Text = "lbXx_controp"
    Me.lbXx_controp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbAn_contropLabel
    '
    Me.lbAn_contropLabel.AutoSize = True
    Me.lbAn_contropLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contropLabel.Location = New System.Drawing.Point(10, 44)
    Me.lbAn_contropLabel.Name = "lbAn_contropLabel"
    Me.lbAn_contropLabel.NTSDbField = ""
    Me.lbAn_contropLabel.Size = New System.Drawing.Size(113, 13)
    Me.lbAn_contropLabel.TabIndex = 526
    Me.lbAn_contropLabel.Text = "Controp. ratei/risconti"
    Me.lbAn_contropLabel.Tooltip = ""
    Me.lbAn_contropLabel.UseMnemonic = False
    '
    'edAn_controp
    '
    Me.edAn_controp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_controp.EditValue = "0"
    Me.edAn_controp.Location = New System.Drawing.Point(129, 41)
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
    Me.edAn_controp.Size = New System.Drawing.Size(100, 20)
    Me.edAn_controp.TabIndex = 548
    '
    'lbAn_accperi
    '
    Me.lbAn_accperi.AutoSize = True
    Me.lbAn_accperi.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_accperi.Location = New System.Drawing.Point(11, 95)
    Me.lbAn_accperi.Name = "lbAn_accperi"
    Me.lbAn_accperi.NTSDbField = ""
    Me.lbAn_accperi.Size = New System.Drawing.Size(68, 13)
    Me.lbAn_accperi.TabIndex = 529
    Me.lbAn_accperi.Text = "Richiedi date"
    Me.lbAn_accperi.Tooltip = ""
    Me.lbAn_accperi.UseMnemonic = False
    '
    'cbAn_accperi
    '
    Me.cbAn_accperi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_accperi.DataSource = Nothing
    Me.cbAn_accperi.DisplayMember = ""
    Me.cbAn_accperi.Location = New System.Drawing.Point(129, 92)
    Me.cbAn_accperi.Name = "cbAn_accperi"
    Me.cbAn_accperi.NTSDbField = ""
    Me.cbAn_accperi.Properties.AutoHeight = False
    Me.cbAn_accperi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_accperi.Properties.DropDownRows = 30
    Me.cbAn_accperi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_accperi.SelectedValue = ""
    Me.cbAn_accperi.Size = New System.Drawing.Size(100, 20)
    Me.cbAn_accperi.TabIndex = 551
    Me.cbAn_accperi.ValueMember = ""
    '
    'lbAn_note
    '
    Me.lbAn_note.AutoSize = True
    Me.lbAn_note.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_note.Location = New System.Drawing.Point(10, 187)
    Me.lbAn_note.Name = "lbAn_note"
    Me.lbAn_note.NTSDbField = ""
    Me.lbAn_note.Size = New System.Drawing.Size(57, 13)
    Me.lbAn_note.TabIndex = 530
    Me.lbAn_note.Text = "Note brevi"
    Me.lbAn_note.Tooltip = ""
    Me.lbAn_note.UseMnemonic = False
    '
    'edAn_note
    '
    Me.edAn_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_note.Location = New System.Drawing.Point(129, 184)
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
    Me.edAn_note.Size = New System.Drawing.Size(522, 20)
    Me.edAn_note.TabIndex = 552
    '
    'lbAn_funzionLabel
    '
    Me.lbAn_funzionLabel.AutoSize = True
    Me.lbAn_funzionLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_funzionLabel.Location = New System.Drawing.Point(10, 67)
    Me.lbAn_funzionLabel.Name = "lbAn_funzionLabel"
    Me.lbAn_funzionLabel.NTSDbField = ""
    Me.lbAn_funzionLabel.Size = New System.Drawing.Size(110, 13)
    Me.lbAn_funzionLabel.TabIndex = 536
    Me.lbAn_funzionLabel.Text = "Conto funzionamento"
    Me.lbAn_funzionLabel.Tooltip = ""
    Me.lbAn_funzionLabel.UseMnemonic = False
    '
    'edAn_funzion
    '
    Me.edAn_funzion.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_funzion.EditValue = "0"
    Me.edAn_funzion.Location = New System.Drawing.Point(129, 64)
    Me.edAn_funzion.Name = "edAn_funzion"
    Me.edAn_funzion.NTSDbField = ""
    Me.edAn_funzion.NTSFormat = "0"
    Me.edAn_funzion.NTSForzaVisZoom = False
    Me.edAn_funzion.NTSOldValue = ""
    Me.edAn_funzion.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_funzion.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_funzion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_funzion.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_funzion.Properties.AutoHeight = False
    Me.edAn_funzion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_funzion.Properties.MaxLength = 65536
    Me.edAn_funzion.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_funzion.Size = New System.Drawing.Size(100, 20)
    Me.edAn_funzion.TabIndex = 558
    '
    'lbAn_cksegno
    '
    Me.lbAn_cksegno.AutoSize = True
    Me.lbAn_cksegno.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cksegno.Location = New System.Drawing.Point(11, 119)
    Me.lbAn_cksegno.Name = "lbAn_cksegno"
    Me.lbAn_cksegno.NTSDbField = ""
    Me.lbAn_cksegno.Size = New System.Drawing.Size(60, 13)
    Me.lbAn_cksegno.TabIndex = 538
    Me.lbAn_cksegno.Text = "Test segno"
    Me.lbAn_cksegno.Tooltip = ""
    Me.lbAn_cksegno.UseMnemonic = False
    '
    'cbAn_cksegno
    '
    Me.cbAn_cksegno.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_cksegno.DataSource = Nothing
    Me.cbAn_cksegno.DisplayMember = ""
    Me.cbAn_cksegno.Location = New System.Drawing.Point(129, 116)
    Me.cbAn_cksegno.Name = "cbAn_cksegno"
    Me.cbAn_cksegno.NTSDbField = ""
    Me.cbAn_cksegno.Properties.AutoHeight = False
    Me.cbAn_cksegno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_cksegno.Properties.DropDownRows = 30
    Me.cbAn_cksegno.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_cksegno.SelectedValue = ""
    Me.cbAn_cksegno.Size = New System.Drawing.Size(100, 20)
    Me.cbAn_cksegno.TabIndex = 560
    Me.cbAn_cksegno.ValueMember = ""
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(661, 293)
    Me.NtsTabPage2.Text = "&2 - Dati 1"
    '
    'pnPag2
    '
    Me.pnPag2.AllowDrop = True
    Me.pnPag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2.Appearance.Options.UseBackColor = True
    Me.pnPag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2.Controls.Add(Me.pnDati2Top)
    Me.pnPag2.Controls.Add(Me.fmRiclassificati)
    Me.pnPag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag2.Location = New System.Drawing.Point(0, 0)
    Me.pnPag2.Name = "pnPag2"
    Me.pnPag2.NTSActiveTrasparency = True
    Me.pnPag2.Size = New System.Drawing.Size(661, 293)
    Me.pnPag2.TabIndex = 539
    Me.pnPag2.Text = "NtsPanel2"
    '
    'pnDati2Top
    '
    Me.pnDati2Top.AllowDrop = True
    Me.pnDati2Top.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDati2Top.Appearance.Options.UseBackColor = True
    Me.pnDati2Top.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDati2Top.Controls.Add(Me.lbAn_tipacq)
    Me.pnDati2Top.Controls.Add(Me.cbAn_cosvend)
    Me.pnDati2Top.Controls.Add(Me.lbAn_cosvend)
    Me.pnDati2Top.Controls.Add(Me.cbAn_tipacq)
    Me.pnDati2Top.Controls.Add(Me.cbAn_colbil)
    Me.pnDati2Top.Controls.Add(Me.lbAn_conprof)
    Me.pnDati2Top.Controls.Add(Me.lbAn_colbil)
    Me.pnDati2Top.Controls.Add(Me.cbAn_conprof)
    Me.pnDati2Top.Controls.Add(Me.edAn_percman)
    Me.pnDati2Top.Controls.Add(Me.lbAn_totcron)
    Me.pnDati2Top.Controls.Add(Me.lbAn_percman)
    Me.pnDati2Top.Controls.Add(Me.cbAn_totcron)
    Me.pnDati2Top.Controls.Add(Me.cbAn_manrip)
    Me.pnDati2Top.Controls.Add(Me.lbAn_contrsemp)
    Me.pnDati2Top.Controls.Add(Me.lbAn_manrip)
    Me.pnDati2Top.Controls.Add(Me.cbAn_contrsemp)
    Me.pnDati2Top.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDati2Top.Location = New System.Drawing.Point(0, 0)
    Me.pnDati2Top.Name = "pnDati2Top"
    Me.pnDati2Top.NTSActiveTrasparency = True
    Me.pnDati2Top.Size = New System.Drawing.Size(658, 183)
    Me.pnDati2Top.TabIndex = 588
    Me.pnDati2Top.Text = "NtsPanel1"
    '
    'lbAn_tipacq
    '
    Me.lbAn_tipacq.AutoSize = True
    Me.lbAn_tipacq.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_tipacq.Location = New System.Drawing.Point(20, 9)
    Me.lbAn_tipacq.Name = "lbAn_tipacq"
    Me.lbAn_tipacq.NTSDbField = ""
    Me.lbAn_tipacq.Size = New System.Drawing.Size(158, 13)
    Me.lbAn_tipacq.TabIndex = 570
    Me.lbAn_tipacq.Text = "Tipo acquisto Quadro 'A' IVA 11"
    Me.lbAn_tipacq.Tooltip = ""
    Me.lbAn_tipacq.UseMnemonic = False
    '
    'cbAn_cosvend
    '
    Me.cbAn_cosvend.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_cosvend.DataSource = Nothing
    Me.cbAn_cosvend.DisplayMember = ""
    Me.cbAn_cosvend.Location = New System.Drawing.Point(236, 163)
    Me.cbAn_cosvend.Name = "cbAn_cosvend"
    Me.cbAn_cosvend.NTSDbField = ""
    Me.cbAn_cosvend.Properties.AutoHeight = False
    Me.cbAn_cosvend.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_cosvend.Properties.DropDownRows = 30
    Me.cbAn_cosvend.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_cosvend.SelectedValue = ""
    Me.cbAn_cosvend.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_cosvend.TabIndex = 526
    Me.cbAn_cosvend.ValueMember = ""
    '
    'lbAn_cosvend
    '
    Me.lbAn_cosvend.AutoSize = True
    Me.lbAn_cosvend.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_cosvend.Location = New System.Drawing.Point(21, 166)
    Me.lbAn_cosvend.Name = "lbAn_cosvend"
    Me.lbAn_cosvend.NTSDbField = ""
    Me.lbAn_cosvend.Size = New System.Drawing.Size(130, 13)
    Me.lbAn_cosvend.TabIndex = 36
    Me.lbAn_cosvend.Text = "Calcola costo del venduto"
    Me.lbAn_cosvend.Tooltip = ""
    Me.lbAn_cosvend.UseMnemonic = False
    '
    'cbAn_tipacq
    '
    Me.cbAn_tipacq.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_tipacq.DataSource = Nothing
    Me.cbAn_tipacq.DisplayMember = ""
    Me.cbAn_tipacq.Location = New System.Drawing.Point(236, 6)
    Me.cbAn_tipacq.Name = "cbAn_tipacq"
    Me.cbAn_tipacq.NTSDbField = ""
    Me.cbAn_tipacq.Properties.AutoHeight = False
    Me.cbAn_tipacq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_tipacq.Properties.DropDownRows = 30
    Me.cbAn_tipacq.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_tipacq.SelectedValue = ""
    Me.cbAn_tipacq.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_tipacq.TabIndex = 580
    Me.cbAn_tipacq.ValueMember = ""
    '
    'cbAn_colbil
    '
    Me.cbAn_colbil.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_colbil.DataSource = Nothing
    Me.cbAn_colbil.DisplayMember = ""
    Me.cbAn_colbil.Location = New System.Drawing.Point(236, 137)
    Me.cbAn_colbil.Name = "cbAn_colbil"
    Me.cbAn_colbil.NTSDbField = ""
    Me.cbAn_colbil.Properties.AutoHeight = False
    Me.cbAn_colbil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_colbil.Properties.DropDownRows = 30
    Me.cbAn_colbil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_colbil.SelectedValue = ""
    Me.cbAn_colbil.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_colbil.TabIndex = 586
    Me.cbAn_colbil.ValueMember = ""
    '
    'lbAn_conprof
    '
    Me.lbAn_conprof.AutoSize = True
    Me.lbAn_conprof.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_conprof.Location = New System.Drawing.Point(21, 36)
    Me.lbAn_conprof.Name = "lbAn_conprof"
    Me.lbAn_conprof.NTSDbField = ""
    Me.lbAn_conprof.Size = New System.Drawing.Size(74, 13)
    Me.lbAn_conprof.TabIndex = 571
    Me.lbAn_conprof.Text = "Imprese miste"
    Me.lbAn_conprof.Tooltip = ""
    Me.lbAn_conprof.UseMnemonic = False
    '
    'lbAn_colbil
    '
    Me.lbAn_colbil.AutoSize = True
    Me.lbAn_colbil.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_colbil.Location = New System.Drawing.Point(21, 140)
    Me.lbAn_colbil.Name = "lbAn_colbil"
    Me.lbAn_colbil.NTSDbField = ""
    Me.lbAn_colbil.Size = New System.Drawing.Size(206, 13)
    Me.lbAn_colbil.TabIndex = 576
    Me.lbAn_colbil.Text = "Colonna in stampa bilancio sez. contrapp."
    Me.lbAn_colbil.Tooltip = ""
    Me.lbAn_colbil.UseMnemonic = False
    '
    'cbAn_conprof
    '
    Me.cbAn_conprof.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_conprof.DataSource = Nothing
    Me.cbAn_conprof.DisplayMember = ""
    Me.cbAn_conprof.Location = New System.Drawing.Point(236, 33)
    Me.cbAn_conprof.Name = "cbAn_conprof"
    Me.cbAn_conprof.NTSDbField = ""
    Me.cbAn_conprof.Properties.AutoHeight = False
    Me.cbAn_conprof.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_conprof.Properties.DropDownRows = 30
    Me.cbAn_conprof.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_conprof.SelectedValue = ""
    Me.cbAn_conprof.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_conprof.TabIndex = 581
    Me.cbAn_conprof.ValueMember = ""
    '
    'edAn_percman
    '
    Me.edAn_percman.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_percman.EditValue = "0"
    Me.edAn_percman.Location = New System.Drawing.Point(590, 110)
    Me.edAn_percman.Name = "edAn_percman"
    Me.edAn_percman.NTSDbField = ""
    Me.edAn_percman.NTSFormat = "0"
    Me.edAn_percman.NTSForzaVisZoom = False
    Me.edAn_percman.NTSOldValue = ""
    Me.edAn_percman.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_percman.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_percman.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_percman.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_percman.Properties.AutoHeight = False
    Me.edAn_percman.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_percman.Properties.MaxLength = 65536
    Me.edAn_percman.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_percman.Size = New System.Drawing.Size(60, 20)
    Me.edAn_percman.TabIndex = 585
    '
    'lbAn_totcron
    '
    Me.lbAn_totcron.AutoSize = True
    Me.lbAn_totcron.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_totcron.Location = New System.Drawing.Point(21, 62)
    Me.lbAn_totcron.Name = "lbAn_totcron"
    Me.lbAn_totcron.NTSDbField = ""
    Me.lbAn_totcron.Size = New System.Drawing.Size(144, 13)
    Me.lbAn_totcron.TabIndex = 572
    Me.lbAn_totcron.Text = "Totalizz. registro cronologico"
    Me.lbAn_totcron.Tooltip = ""
    Me.lbAn_totcron.UseMnemonic = False
    '
    'lbAn_percman
    '
    Me.lbAn_percman.AutoSize = True
    Me.lbAn_percman.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_percman.Location = New System.Drawing.Point(464, 114)
    Me.lbAn_percman.Name = "lbAn_percman"
    Me.lbAn_percman.NTSDbField = ""
    Me.lbAn_percman.Size = New System.Drawing.Size(121, 13)
    Me.lbAn_percman.TabIndex = 575
    Me.lbAn_percman.Text = "% manutenz. e riparaz."
    Me.lbAn_percman.Tooltip = ""
    Me.lbAn_percman.UseMnemonic = False
    '
    'cbAn_totcron
    '
    Me.cbAn_totcron.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_totcron.DataSource = Nothing
    Me.cbAn_totcron.DisplayMember = ""
    Me.cbAn_totcron.Location = New System.Drawing.Point(236, 59)
    Me.cbAn_totcron.Name = "cbAn_totcron"
    Me.cbAn_totcron.NTSDbField = ""
    Me.cbAn_totcron.Properties.AutoHeight = False
    Me.cbAn_totcron.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_totcron.Properties.DropDownRows = 30
    Me.cbAn_totcron.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_totcron.SelectedValue = ""
    Me.cbAn_totcron.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_totcron.TabIndex = 582
    Me.cbAn_totcron.ValueMember = ""
    '
    'cbAn_manrip
    '
    Me.cbAn_manrip.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_manrip.DataSource = Nothing
    Me.cbAn_manrip.DisplayMember = ""
    Me.cbAn_manrip.Location = New System.Drawing.Point(236, 111)
    Me.cbAn_manrip.Name = "cbAn_manrip"
    Me.cbAn_manrip.NTSDbField = ""
    Me.cbAn_manrip.Properties.AutoHeight = False
    Me.cbAn_manrip.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_manrip.Properties.DropDownRows = 30
    Me.cbAn_manrip.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_manrip.SelectedValue = ""
    Me.cbAn_manrip.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_manrip.TabIndex = 584
    Me.cbAn_manrip.ValueMember = ""
    '
    'lbAn_contrsemp
    '
    Me.lbAn_contrsemp.AutoSize = True
    Me.lbAn_contrsemp.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contrsemp.Location = New System.Drawing.Point(21, 88)
    Me.lbAn_contrsemp.Name = "lbAn_contrsemp"
    Me.lbAn_contrsemp.NTSDbField = ""
    Me.lbAn_contrsemp.Size = New System.Drawing.Size(109, 13)
    Me.lbAn_contrsemp.TabIndex = 573
    Me.lbAn_contrsemp.Text = "Controlla semplificata"
    Me.lbAn_contrsemp.Tooltip = ""
    Me.lbAn_contrsemp.UseMnemonic = False
    '
    'lbAn_manrip
    '
    Me.lbAn_manrip.AutoSize = True
    Me.lbAn_manrip.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_manrip.Location = New System.Drawing.Point(21, 114)
    Me.lbAn_manrip.Name = "lbAn_manrip"
    Me.lbAn_manrip.NTSDbField = ""
    Me.lbAn_manrip.Size = New System.Drawing.Size(139, 13)
    Me.lbAn_manrip.TabIndex = 574
    Me.lbAn_manrip.Text = "Manutenzione e riparazione"
    Me.lbAn_manrip.Tooltip = ""
    Me.lbAn_manrip.UseMnemonic = False
    '
    'cbAn_contrsemp
    '
    Me.cbAn_contrsemp.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_contrsemp.DataSource = Nothing
    Me.cbAn_contrsemp.DisplayMember = ""
    Me.cbAn_contrsemp.Location = New System.Drawing.Point(236, 85)
    Me.cbAn_contrsemp.Name = "cbAn_contrsemp"
    Me.cbAn_contrsemp.NTSDbField = ""
    Me.cbAn_contrsemp.Properties.AutoHeight = False
    Me.cbAn_contrsemp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_contrsemp.Properties.DropDownRows = 30
    Me.cbAn_contrsemp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_contrsemp.SelectedValue = ""
    Me.cbAn_contrsemp.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_contrsemp.TabIndex = 583
    Me.cbAn_contrsemp.ValueMember = ""
    '
    'fmRiclassificati
    '
    Me.fmRiclassificati.AllowDrop = True
    Me.fmRiclassificati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRiclassificati.Appearance.Options.UseBackColor = True
    Me.fmRiclassificati.Controls.Add(Me.ckOpzgest1)
    Me.fmRiclassificati.Controls.Add(Me.cmdRiclassificazioni)
    Me.fmRiclassificati.Controls.Add(Me.lbRiclassif)
    Me.fmRiclassificati.Controls.Add(Me.lbCee)
    Me.fmRiclassificati.Controls.Add(Me.edAn_kpccee)
    Me.fmRiclassificati.Controls.Add(Me.edAn_kpccee2)
    Me.fmRiclassificati.Controls.Add(Me.lbAn_kpccee2)
    Me.fmRiclassificati.Controls.Add(Me.lbAn_kpccee)
    Me.fmRiclassificati.Controls.Add(Me.lbAn_rifrica)
    Me.fmRiclassificati.Controls.Add(Me.edAn_rifrica)
    Me.fmRiclassificati.Controls.Add(Me.edAn_rifricd)
    Me.fmRiclassificati.Controls.Add(Me.lbAn_rifricd)
    Me.fmRiclassificati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRiclassificati.Location = New System.Drawing.Point(13, 189)
    Me.fmRiclassificati.Name = "fmRiclassificati"
    Me.fmRiclassificati.Size = New System.Drawing.Size(638, 92)
    Me.fmRiclassificati.TabIndex = 587
    Me.fmRiclassificati.Text = "Riclassificati su Excel"
    '
    'ckOpzgest1
    '
    Me.ckOpzgest1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest1.Location = New System.Drawing.Point(486, 61)
    Me.ckOpzgest1.Name = "ckOpzgest1"
    Me.ckOpzgest1.NTSCheckValue = "S"
    Me.ckOpzgest1.NTSUnCheckValue = "N"
    Me.ckOpzgest1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest1.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest1.Properties.AutoHeight = False
    Me.ckOpzgest1.Properties.Caption = "Usa conto funzionamento"
    Me.ckOpzgest1.Size = New System.Drawing.Size(147, 19)
    Me.ckOpzgest1.TabIndex = 589
    '
    'cmdRiclassificazioni
    '
    Me.cmdRiclassificazioni.ImagePath = ""
    Me.cmdRiclassificazioni.ImageText = ""
    Me.cmdRiclassificazioni.Location = New System.Drawing.Point(486, 32)
    Me.cmdRiclassificazioni.Name = "cmdRiclassificazioni"
    Me.cmdRiclassificazioni.NTSContextMenu = Nothing
    Me.cmdRiclassificazioni.Size = New System.Drawing.Size(147, 23)
    Me.cmdRiclassificazioni.TabIndex = 588
    Me.cmdRiclassificazioni.Text = "Tipi riclassificazioni"
    '
    'lbRiclassif
    '
    Me.lbRiclassif.AutoSize = True
    Me.lbRiclassif.BackColor = System.Drawing.Color.Transparent
    Me.lbRiclassif.Location = New System.Drawing.Point(320, 21)
    Me.lbRiclassif.Name = "lbRiclassif"
    Me.lbRiclassif.NTSDbField = ""
    Me.lbRiclassif.Size = New System.Drawing.Size(68, 13)
    Me.lbRiclassif.TabIndex = 558
    Me.lbRiclassif.Text = "Riclassificato"
    Me.lbRiclassif.Tooltip = ""
    Me.lbRiclassif.UseMnemonic = False
    '
    'lbCee
    '
    Me.lbCee.AutoSize = True
    Me.lbCee.BackColor = System.Drawing.Color.Transparent
    Me.lbCee.Location = New System.Drawing.Point(94, 21)
    Me.lbCee.Name = "lbCee"
    Me.lbCee.NTSDbField = ""
    Me.lbCee.Size = New System.Drawing.Size(64, 13)
    Me.lbCee.TabIndex = 556
    Me.lbCee.Text = "Bilancio CEE"
    Me.lbCee.Tooltip = ""
    Me.lbCee.UseMnemonic = False
    '
    'edAn_kpccee
    '
    Me.edAn_kpccee.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_kpccee.Location = New System.Drawing.Point(80, 37)
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
    'edAn_kpccee2
    '
    Me.edAn_kpccee2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_kpccee2.Location = New System.Drawing.Point(80, 63)
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
    Me.lbAn_kpccee2.Location = New System.Drawing.Point(7, 67)
    Me.lbAn_kpccee2.Name = "lbAn_kpccee2"
    Me.lbAn_kpccee2.NTSDbField = ""
    Me.lbAn_kpccee2.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_kpccee2.TabIndex = 533
    Me.lbAn_kpccee2.Text = "Saldo Avere"
    Me.lbAn_kpccee2.Tooltip = ""
    Me.lbAn_kpccee2.UseMnemonic = False
    '
    'lbAn_kpccee
    '
    Me.lbAn_kpccee.AutoSize = True
    Me.lbAn_kpccee.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_kpccee.Location = New System.Drawing.Point(8, 42)
    Me.lbAn_kpccee.Name = "lbAn_kpccee"
    Me.lbAn_kpccee.NTSDbField = ""
    Me.lbAn_kpccee.Size = New System.Drawing.Size(59, 13)
    Me.lbAn_kpccee.TabIndex = 532
    Me.lbAn_kpccee.Text = "Saldo Dare"
    Me.lbAn_kpccee.Tooltip = ""
    Me.lbAn_kpccee.UseMnemonic = False
    '
    'lbAn_rifrica
    '
    Me.lbAn_rifrica.AutoSize = True
    Me.lbAn_rifrica.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rifrica.Location = New System.Drawing.Point(219, 67)
    Me.lbAn_rifrica.Name = "lbAn_rifrica"
    Me.lbAn_rifrica.NTSDbField = ""
    Me.lbAn_rifrica.Size = New System.Drawing.Size(65, 13)
    Me.lbAn_rifrica.TabIndex = 535
    Me.lbAn_rifrica.Text = "Saldo Avere"
    Me.lbAn_rifrica.Tooltip = ""
    Me.lbAn_rifrica.UseMnemonic = False
    '
    'edAn_rifrica
    '
    Me.edAn_rifrica.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rifrica.Location = New System.Drawing.Point(313, 63)
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
    'edAn_rifricd
    '
    Me.edAn_rifricd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_rifricd.Location = New System.Drawing.Point(313, 37)
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
    'lbAn_rifricd
    '
    Me.lbAn_rifricd.AutoSize = True
    Me.lbAn_rifricd.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_rifricd.Location = New System.Drawing.Point(219, 42)
    Me.lbAn_rifricd.Name = "lbAn_rifricd"
    Me.lbAn_rifricd.NTSDbField = ""
    Me.lbAn_rifricd.Size = New System.Drawing.Size(59, 13)
    Me.lbAn_rifricd.TabIndex = 534
    Me.lbAn_rifricd.Text = "Saldo Dare"
    Me.lbAn_rifricd.Tooltip = ""
    Me.lbAn_rifricd.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnTesoreria)
    Me.NtsTabPage3.Controls.Add(Me.pnPag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(661, 293)
    Me.NtsTabPage3.Text = "&3 - Dati 2"
    '
    'pnTesoreria
    '
    Me.pnTesoreria.AllowDrop = True
    Me.pnTesoreria.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTesoreria.Appearance.Options.UseBackColor = True
    Me.pnTesoreria.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTesoreria.Controls.Add(Me.fmTesoreria)
    Me.pnTesoreria.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTesoreria.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnTesoreria.Location = New System.Drawing.Point(0, 237)
    Me.pnTesoreria.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTesoreria.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTesoreria.Name = "pnTesoreria"
    Me.pnTesoreria.NTSActiveTrasparency = True
    Me.pnTesoreria.Size = New System.Drawing.Size(661, 56)
    Me.pnTesoreria.TabIndex = 2
    Me.pnTesoreria.Text = "NtsPanel1"
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
    Me.fmTesoreria.Location = New System.Drawing.Point(9, 3)
    Me.fmTesoreria.Name = "fmTesoreria"
    Me.fmTesoreria.Size = New System.Drawing.Size(646, 50)
    Me.fmTesoreria.TabIndex = 630
    Me.fmTesoreria.Text = "Tesoreria e flussi finanziari"
    '
    'lbXx_codvfde
    '
    Me.lbXx_codvfde.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvfde.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvfde.Location = New System.Drawing.Point(398, 24)
    Me.lbXx_codvfde.Name = "lbXx_codvfde"
    Me.lbXx_codvfde.NTSDbField = ""
    Me.lbXx_codvfde.Size = New System.Drawing.Size(241, 20)
    Me.lbXx_codvfde.TabIndex = 622
    Me.lbXx_codvfde.Tooltip = ""
    Me.lbXx_codvfde.UseMnemonic = False
    '
    'cbAn_trating
    '
    Me.cbAn_trating.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_trating.DataSource = Nothing
    Me.cbAn_trating.DisplayMember = ""
    Me.cbAn_trating.Location = New System.Drawing.Point(49, 24)
    Me.cbAn_trating.Name = "cbAn_trating"
    Me.cbAn_trating.NTSDbField = ""
    Me.cbAn_trating.Properties.AutoHeight = False
    Me.cbAn_trating.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_trating.Properties.DropDownRows = 30
    Me.cbAn_trating.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_trating.SelectedValue = ""
    Me.cbAn_trating.Size = New System.Drawing.Size(172, 20)
    Me.cbAn_trating.TabIndex = 3
    Me.cbAn_trating.ValueMember = ""
    '
    'edAn_codvfde
    '
    Me.edAn_codvfde.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_codvfde.EditValue = ""
    Me.edAn_codvfde.Location = New System.Drawing.Point(334, 24)
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
    Me.lbAn_codvfde.Location = New System.Drawing.Point(246, 27)
    Me.lbAn_codvfde.Name = "lbAn_codvfde"
    Me.lbAn_codvfde.NTSDbField = ""
    Me.lbAn_codvfde.Size = New System.Drawing.Size(91, 13)
    Me.lbAn_codvfde.TabIndex = 1
    Me.lbAn_codvfde.Text = "Voce finanziaria *"
    Me.lbAn_codvfde.Tooltip = ""
    Me.lbAn_codvfde.UseMnemonic = False
    '
    'lbAn_trating
    '
    Me.lbAn_trating.AutoSize = True
    Me.lbAn_trating.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_trating.Location = New System.Drawing.Point(5, 27)
    Me.lbAn_trating.Name = "lbAn_trating"
    Me.lbAn_trating.NTSDbField = ""
    Me.lbAn_trating.Size = New System.Drawing.Size(47, 13)
    Me.lbAn_trating.TabIndex = 0
    Me.lbAn_trating.Text = "Rating *"
    Me.lbAn_trating.Tooltip = ""
    Me.lbAn_trating.UseMnemonic = False
    '
    'pnPag3
    '
    Me.pnPag3.AllowDrop = True
    Me.pnPag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag3.Appearance.Options.UseBackColor = True
    Me.pnPag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag3.Controls.Add(Me.fmUsaContoFunz)
    Me.pnPag3.Controls.Add(Me.fmStudi)
    Me.pnPag3.Controls.Add(Me.fmRicavometro)
    Me.pnPag3.Controls.Add(Me.fmImposte)
    Me.pnPag3.Controls.Add(Me.ckAn_intragr)
    Me.pnPag3.Controls.Add(Me.lbAn_azcom)
    Me.pnPag3.Controls.Add(Me.fmIrap)
    Me.pnPag3.Controls.Add(Me.cbAn_azcom)
    Me.pnPag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag3.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnPag3.Location = New System.Drawing.Point(0, 0)
    Me.pnPag3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag3.Name = "pnPag3"
    Me.pnPag3.NTSActiveTrasparency = True
    Me.pnPag3.Size = New System.Drawing.Size(661, 237)
    Me.pnPag3.TabIndex = 1
    Me.pnPag3.Text = "NtsPanel2"
    '
    'fmUsaContoFunz
    '
    Me.fmUsaContoFunz.AllowDrop = True
    Me.fmUsaContoFunz.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUsaContoFunz.Appearance.Options.UseBackColor = True
    Me.fmUsaContoFunz.Controls.Add(Me.ckOpzgest6)
    Me.fmUsaContoFunz.Controls.Add(Me.ckOpzgest5)
    Me.fmUsaContoFunz.Controls.Add(Me.ckOpzgest4)
    Me.fmUsaContoFunz.Controls.Add(Me.ckOpzgest3)
    Me.fmUsaContoFunz.Controls.Add(Me.ckOpzgest2)
    Me.fmUsaContoFunz.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUsaContoFunz.Location = New System.Drawing.Point(411, 3)
    Me.fmUsaContoFunz.Name = "fmUsaContoFunz"
    Me.fmUsaContoFunz.Size = New System.Drawing.Size(244, 127)
    Me.fmUsaContoFunz.TabIndex = 616
    Me.fmUsaContoFunz.Text = "Usa conto funzionamento per"
    '
    'ckOpzgest6
    '
    Me.ckOpzgest6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest6.Location = New System.Drawing.Point(11, 105)
    Me.ckOpzgest6.Name = "ckOpzgest6"
    Me.ckOpzgest6.NTSCheckValue = "S"
    Me.ckOpzgest6.NTSUnCheckValue = "N"
    Me.ckOpzgest6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest6.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest6.Properties.AutoHeight = False
    Me.ckOpzgest6.Properties.Caption = "Azienda di comodo"
    Me.ckOpzgest6.Size = New System.Drawing.Size(225, 19)
    Me.ckOpzgest6.TabIndex = 4
    '
    'ckOpzgest5
    '
    Me.ckOpzgest5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest5.Location = New System.Drawing.Point(11, 83)
    Me.ckOpzgest5.Name = "ckOpzgest5"
    Me.ckOpzgest5.NTSCheckValue = "S"
    Me.ckOpzgest5.NTSUnCheckValue = "N"
    Me.ckOpzgest5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest5.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest5.Properties.AutoHeight = False
    Me.ckOpzgest5.Properties.Caption = "Studi di settore"
    Me.ckOpzgest5.Size = New System.Drawing.Size(225, 19)
    Me.ckOpzgest5.TabIndex = 3
    '
    'ckOpzgest4
    '
    Me.ckOpzgest4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest4.Location = New System.Drawing.Point(11, 63)
    Me.ckOpzgest4.Name = "ckOpzgest4"
    Me.ckOpzgest4.NTSCheckValue = "S"
    Me.ckOpzgest4.NTSUnCheckValue = "N"
    Me.ckOpzgest4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest4.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest4.Properties.AutoHeight = False
    Me.ckOpzgest4.Properties.Caption = "Ricavometro"
    Me.ckOpzgest4.Size = New System.Drawing.Size(225, 19)
    Me.ckOpzgest4.TabIndex = 2
    '
    'ckOpzgest3
    '
    Me.ckOpzgest3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest3.Location = New System.Drawing.Point(11, 43)
    Me.ckOpzgest3.Name = "ckOpzgest3"
    Me.ckOpzgest3.NTSCheckValue = "S"
    Me.ckOpzgest3.NTSUnCheckValue = "N"
    Me.ckOpzgest3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest3.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest3.Properties.AutoHeight = False
    Me.ckOpzgest3.Properties.Caption = "Percentuali inded. imposte dirette"
    Me.ckOpzgest3.Size = New System.Drawing.Size(225, 19)
    Me.ckOpzgest3.TabIndex = 1
    '
    'ckOpzgest2
    '
    Me.ckOpzgest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpzgest2.Location = New System.Drawing.Point(11, 22)
    Me.ckOpzgest2.Name = "ckOpzgest2"
    Me.ckOpzgest2.NTSCheckValue = "S"
    Me.ckOpzgest2.NTSUnCheckValue = "N"
    Me.ckOpzgest2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpzgest2.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpzgest2.Properties.AutoHeight = False
    Me.ckOpzgest2.Properties.Caption = "Dati IRAP"
    Me.ckOpzgest2.Size = New System.Drawing.Size(225, 19)
    Me.ckOpzgest2.TabIndex = 0
    '
    'fmStudi
    '
    Me.fmStudi.AllowDrop = True
    Me.fmStudi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStudi.Appearance.Options.UseBackColor = True
    Me.fmStudi.Controls.Add(Me.lbAn_stseimp)
    Me.fmStudi.Controls.Add(Me.cbAn_stseimp)
    Me.fmStudi.Controls.Add(Me.lbAn_stsepro)
    Me.fmStudi.Controls.Add(Me.cbAn_stsepro)
    Me.fmStudi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStudi.Location = New System.Drawing.Point(245, 136)
    Me.fmStudi.Name = "fmStudi"
    Me.fmStudi.Size = New System.Drawing.Size(244, 76)
    Me.fmStudi.TabIndex = 615
    Me.fmStudi.Text = "Studi di settore"
    '
    'lbAn_stseimp
    '
    Me.lbAn_stseimp.AutoSize = True
    Me.lbAn_stseimp.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_stseimp.Location = New System.Drawing.Point(11, 26)
    Me.lbAn_stseimp.Name = "lbAn_stseimp"
    Me.lbAn_stseimp.NTSDbField = ""
    Me.lbAn_stseimp.Size = New System.Drawing.Size(46, 13)
    Me.lbAn_stseimp.TabIndex = 602
    Me.lbAn_stseimp.Text = "Imprese"
    Me.lbAn_stseimp.Tooltip = ""
    Me.lbAn_stseimp.UseMnemonic = False
    '
    'cbAn_stseimp
    '
    Me.cbAn_stseimp.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_stseimp.DataSource = Nothing
    Me.cbAn_stseimp.DisplayMember = ""
    Me.cbAn_stseimp.Location = New System.Drawing.Point(86, 23)
    Me.cbAn_stseimp.Name = "cbAn_stseimp"
    Me.cbAn_stseimp.NTSDbField = ""
    Me.cbAn_stseimp.Properties.AutoHeight = False
    Me.cbAn_stseimp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_stseimp.Properties.DropDownRows = 30
    Me.cbAn_stseimp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_stseimp.SelectedValue = ""
    Me.cbAn_stseimp.Size = New System.Drawing.Size(153, 20)
    Me.cbAn_stseimp.TabIndex = 604
    Me.cbAn_stseimp.ValueMember = ""
    '
    'lbAn_stsepro
    '
    Me.lbAn_stsepro.AutoSize = True
    Me.lbAn_stsepro.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_stsepro.Location = New System.Drawing.Point(11, 52)
    Me.lbAn_stsepro.Name = "lbAn_stsepro"
    Me.lbAn_stsepro.NTSDbField = ""
    Me.lbAn_stsepro.Size = New System.Drawing.Size(70, 13)
    Me.lbAn_stsepro.TabIndex = 603
    Me.lbAn_stsepro.Text = "Professionisti"
    Me.lbAn_stsepro.Tooltip = ""
    Me.lbAn_stsepro.UseMnemonic = False
    '
    'cbAn_stsepro
    '
    Me.cbAn_stsepro.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbAn_stsepro.DataSource = Nothing
    Me.cbAn_stsepro.DisplayMember = ""
    Me.cbAn_stsepro.Location = New System.Drawing.Point(86, 49)
    Me.cbAn_stsepro.Name = "cbAn_stsepro"
    Me.cbAn_stsepro.NTSDbField = ""
    Me.cbAn_stsepro.Properties.AutoHeight = False
    Me.cbAn_stsepro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_stsepro.Properties.DropDownRows = 30
    Me.cbAn_stsepro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_stsepro.SelectedValue = ""
    Me.cbAn_stsepro.Size = New System.Drawing.Size(153, 20)
    Me.cbAn_stsepro.TabIndex = 605
    Me.cbAn_stsepro.ValueMember = ""
    '
    'fmRicavometro
    '
    Me.fmRicavometro.AllowDrop = True
    Me.fmRicavometro.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRicavometro.Appearance.Options.UseBackColor = True
    Me.fmRicavometro.Controls.Add(Me.ckAn_ricmimp)
    Me.fmRicavometro.Controls.Add(Me.ckAn_ricmpro)
    Me.fmRicavometro.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRicavometro.Location = New System.Drawing.Point(495, 136)
    Me.fmRicavometro.Name = "fmRicavometro"
    Me.fmRicavometro.Size = New System.Drawing.Size(160, 76)
    Me.fmRicavometro.TabIndex = 614
    Me.fmRicavometro.Text = "Ricavometro"
    '
    'ckAn_ricmimp
    '
    Me.ckAn_ricmimp.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckAn_ricmimp.Location = New System.Drawing.Point(14, 24)
    Me.ckAn_ricmimp.Name = "ckAn_ricmimp"
    Me.ckAn_ricmimp.NTSCheckValue = "S"
    Me.ckAn_ricmimp.NTSUnCheckValue = "N"
    Me.ckAn_ricmimp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_ricmimp.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_ricmimp.Properties.AutoHeight = False
    Me.ckAn_ricmimp.Properties.Caption = "Imprese"
    Me.ckAn_ricmimp.Size = New System.Drawing.Size(99, 19)
    Me.ckAn_ricmimp.TabIndex = 598
    '
    'ckAn_ricmpro
    '
    Me.ckAn_ricmpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_ricmpro.Location = New System.Drawing.Point(14, 50)
    Me.ckAn_ricmpro.Name = "ckAn_ricmpro"
    Me.ckAn_ricmpro.NTSCheckValue = "S"
    Me.ckAn_ricmpro.NTSUnCheckValue = "N"
    Me.ckAn_ricmpro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_ricmpro.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_ricmpro.Properties.AutoHeight = False
    Me.ckAn_ricmpro.Properties.Caption = "Professionisti"
    Me.ckAn_ricmpro.Size = New System.Drawing.Size(99, 19)
    Me.ckAn_ricmpro.TabIndex = 599
    '
    'fmImposte
    '
    Me.fmImposte.AllowDrop = True
    Me.fmImposte.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmImposte.Appearance.Options.UseBackColor = True
    Me.fmImposte.Controls.Add(Me.edAn_indiidd)
    Me.fmImposte.Controls.Add(Me.lbAn_indiidd)
    Me.fmImposte.Controls.Add(Me.edAn_indiiddsit)
    Me.fmImposte.Controls.Add(Me.lbAn_indiiddsit)
    Me.fmImposte.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmImposte.Location = New System.Drawing.Point(9, 136)
    Me.fmImposte.Name = "fmImposte"
    Me.fmImposte.Size = New System.Drawing.Size(230, 76)
    Me.fmImposte.TabIndex = 611
    Me.fmImposte.Text = "Percentuali inded. imposte dirette"
    '
    'edAn_indiidd
    '
    Me.edAn_indiidd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_indiidd.EditValue = "0"
    Me.edAn_indiidd.Location = New System.Drawing.Point(162, 23)
    Me.edAn_indiidd.Name = "edAn_indiidd"
    Me.edAn_indiidd.NTSDbField = ""
    Me.edAn_indiidd.NTSFormat = "0"
    Me.edAn_indiidd.NTSForzaVisZoom = False
    Me.edAn_indiidd.NTSOldValue = ""
    Me.edAn_indiidd.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_indiidd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_indiidd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_indiidd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_indiidd.Properties.AutoHeight = False
    Me.edAn_indiidd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_indiidd.Properties.MaxLength = 65536
    Me.edAn_indiidd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_indiidd.Size = New System.Drawing.Size(58, 20)
    Me.edAn_indiidd.TabIndex = 596
    '
    'lbAn_indiidd
    '
    Me.lbAn_indiidd.AutoSize = True
    Me.lbAn_indiidd.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_indiidd.Location = New System.Drawing.Point(6, 26)
    Me.lbAn_indiidd.Name = "lbAn_indiidd"
    Me.lbAn_indiidd.NTSDbField = ""
    Me.lbAn_indiidd.Size = New System.Drawing.Size(56, 13)
    Me.lbAn_indiidd.TabIndex = 590
    Me.lbAn_indiidd.Text = "Per redditi"
    Me.lbAn_indiidd.Tooltip = ""
    Me.lbAn_indiidd.UseMnemonic = False
    '
    'edAn_indiiddsit
    '
    Me.edAn_indiiddsit.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_indiiddsit.EditValue = "0"
    Me.edAn_indiiddsit.Location = New System.Drawing.Point(162, 49)
    Me.edAn_indiiddsit.Name = "edAn_indiiddsit"
    Me.edAn_indiiddsit.NTSDbField = ""
    Me.edAn_indiiddsit.NTSFormat = "0"
    Me.edAn_indiiddsit.NTSForzaVisZoom = False
    Me.edAn_indiiddsit.NTSOldValue = ""
    Me.edAn_indiiddsit.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_indiiddsit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_indiiddsit.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_indiiddsit.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_indiiddsit.Properties.AutoHeight = False
    Me.edAn_indiiddsit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_indiiddsit.Properties.MaxLength = 65536
    Me.edAn_indiiddsit.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_indiiddsit.Size = New System.Drawing.Size(58, 20)
    Me.edAn_indiiddsit.TabIndex = 603
    '
    'lbAn_indiiddsit
    '
    Me.lbAn_indiiddsit.AutoSize = True
    Me.lbAn_indiiddsit.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_indiiddsit.Location = New System.Drawing.Point(6, 52)
    Me.lbAn_indiiddsit.Name = "lbAn_indiiddsit"
    Me.lbAn_indiiddsit.NTSDbField = ""
    Me.lbAn_indiiddsit.Size = New System.Drawing.Size(140, 13)
    Me.lbAn_indiiddsit.TabIndex = 595
    Me.lbAn_indiiddsit.Text = "Per situaz. econom./patrim."
    Me.lbAn_indiiddsit.Tooltip = ""
    Me.lbAn_indiiddsit.UseMnemonic = False
    '
    'ckAn_intragr
    '
    Me.ckAn_intragr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_intragr.Location = New System.Drawing.Point(14, 215)
    Me.ckAn_intragr.Name = "ckAn_intragr"
    Me.ckAn_intragr.NTSCheckValue = "S"
    Me.ckAn_intragr.NTSUnCheckValue = "N"
    Me.ckAn_intragr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_intragr.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_intragr.Properties.AutoHeight = False
    Me.ckAn_intragr.Properties.Caption = "Crediti/Debiti/Costi/Ricavi intragruppo"
    Me.ckAn_intragr.Size = New System.Drawing.Size(213, 19)
    Me.ckAn_intragr.TabIndex = 613
    '
    'lbAn_azcom
    '
    Me.lbAn_azcom.AutoSize = True
    Me.lbAn_azcom.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_azcom.Location = New System.Drawing.Point(405, 217)
    Me.lbAn_azcom.Name = "lbAn_azcom"
    Me.lbAn_azcom.NTSDbField = ""
    Me.lbAn_azcom.Size = New System.Drawing.Size(96, 13)
    Me.lbAn_azcom.TabIndex = 591
    Me.lbAn_azcom.Text = "Azienda di comodo"
    Me.lbAn_azcom.Tooltip = ""
    Me.lbAn_azcom.UseMnemonic = False
    '
    'fmIrap
    '
    Me.fmIrap.AllowDrop = True
    Me.fmIrap.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIrap.Appearance.Options.UseBackColor = True
    Me.fmIrap.Controls.Add(Me.lbXx_voceirap)
    Me.fmIrap.Controls.Add(Me.edAn_pervari)
    Me.fmIrap.Controls.Add(Me.lbAn_pervari)
    Me.fmIrap.Controls.Add(Me.lbAn_varirap)
    Me.fmIrap.Controls.Add(Me.edAn_voceirap)
    Me.fmIrap.Controls.Add(Me.cbAn_varirap)
    Me.fmIrap.Controls.Add(Me.lbAn_voceirapLabel)
    Me.fmIrap.Controls.Add(Me.edAn_indirap)
    Me.fmIrap.Controls.Add(Me.lbAn_indirap)
    Me.fmIrap.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIrap.Location = New System.Drawing.Point(9, 3)
    Me.fmIrap.Name = "fmIrap"
    Me.fmIrap.Size = New System.Drawing.Size(371, 127)
    Me.fmIrap.TabIndex = 610
    Me.fmIrap.Text = "IRAP"
    '
    'lbXx_voceirap
    '
    Me.lbXx_voceirap.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_voceirap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_voceirap.Location = New System.Drawing.Point(226, 23)
    Me.lbXx_voceirap.Name = "lbXx_voceirap"
    Me.lbXx_voceirap.NTSDbField = ""
    Me.lbXx_voceirap.Size = New System.Drawing.Size(140, 20)
    Me.lbXx_voceirap.TabIndex = 608
    Me.lbXx_voceirap.Text = "lbXx_voceirap"
    Me.lbXx_voceirap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_voceirap.Tooltip = ""
    Me.lbXx_voceirap.UseMnemonic = False
    '
    'edAn_pervari
    '
    Me.edAn_pervari.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_pervari.EditValue = "0"
    Me.edAn_pervari.Location = New System.Drawing.Point(120, 75)
    Me.edAn_pervari.Name = "edAn_pervari"
    Me.edAn_pervari.NTSDbField = ""
    Me.edAn_pervari.NTSFormat = "0"
    Me.edAn_pervari.NTSForzaVisZoom = False
    Me.edAn_pervari.NTSOldValue = ""
    Me.edAn_pervari.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_pervari.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_pervari.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_pervari.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_pervari.Properties.AutoHeight = False
    Me.edAn_pervari.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_pervari.Properties.MaxLength = 65536
    Me.edAn_pervari.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_pervari.Size = New System.Drawing.Size(100, 20)
    Me.edAn_pervari.TabIndex = 609
    '
    'lbAn_pervari
    '
    Me.lbAn_pervari.AutoSize = True
    Me.lbAn_pervari.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_pervari.Location = New System.Drawing.Point(6, 78)
    Me.lbAn_pervari.Name = "lbAn_pervari"
    Me.lbAn_pervari.NTSDbField = ""
    Me.lbAn_pervari.Size = New System.Drawing.Size(70, 13)
    Me.lbAn_pervari.TabIndex = 606
    Me.lbAn_pervari.Text = "% Variazione"
    Me.lbAn_pervari.Tooltip = ""
    Me.lbAn_pervari.UseMnemonic = False
    '
    'lbAn_varirap
    '
    Me.lbAn_varirap.AutoSize = True
    Me.lbAn_varirap.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_varirap.Location = New System.Drawing.Point(6, 52)
    Me.lbAn_varirap.Name = "lbAn_varirap"
    Me.lbAn_varirap.NTSDbField = ""
    Me.lbAn_varirap.Size = New System.Drawing.Size(79, 13)
    Me.lbAn_varirap.TabIndex = 605
    Me.lbAn_varirap.Text = "Tipo variazione"
    Me.lbAn_varirap.Tooltip = ""
    Me.lbAn_varirap.UseMnemonic = False
    '
    'edAn_voceirap
    '
    Me.edAn_voceirap.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAn_voceirap.EditValue = "0"
    Me.edAn_voceirap.Location = New System.Drawing.Point(120, 23)
    Me.edAn_voceirap.Name = "edAn_voceirap"
    Me.edAn_voceirap.NTSDbField = ""
    Me.edAn_voceirap.NTSFormat = "0"
    Me.edAn_voceirap.NTSForzaVisZoom = False
    Me.edAn_voceirap.NTSOldValue = ""
    Me.edAn_voceirap.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_voceirap.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_voceirap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_voceirap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_voceirap.Properties.AutoHeight = False
    Me.edAn_voceirap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_voceirap.Properties.MaxLength = 65536
    Me.edAn_voceirap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_voceirap.Size = New System.Drawing.Size(100, 20)
    Me.edAn_voceirap.TabIndex = 607
    '
    'cbAn_varirap
    '
    Me.cbAn_varirap.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_varirap.DataSource = Nothing
    Me.cbAn_varirap.DisplayMember = ""
    Me.cbAn_varirap.Location = New System.Drawing.Point(120, 49)
    Me.cbAn_varirap.Name = "cbAn_varirap"
    Me.cbAn_varirap.NTSDbField = ""
    Me.cbAn_varirap.Properties.AutoHeight = False
    Me.cbAn_varirap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_varirap.Properties.DropDownRows = 30
    Me.cbAn_varirap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_varirap.SelectedValue = ""
    Me.cbAn_varirap.Size = New System.Drawing.Size(100, 20)
    Me.cbAn_varirap.TabIndex = 608
    Me.cbAn_varirap.ValueMember = ""
    '
    'lbAn_voceirapLabel
    '
    Me.lbAn_voceirapLabel.AutoSize = True
    Me.lbAn_voceirapLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_voceirapLabel.Location = New System.Drawing.Point(6, 26)
    Me.lbAn_voceirapLabel.Name = "lbAn_voceirapLabel"
    Me.lbAn_voceirapLabel.NTSDbField = ""
    Me.lbAn_voceirapLabel.Size = New System.Drawing.Size(57, 13)
    Me.lbAn_voceirapLabel.TabIndex = 604
    Me.lbAn_voceirapLabel.Text = "Voce IRAP"
    Me.lbAn_voceirapLabel.Tooltip = ""
    Me.lbAn_voceirapLabel.UseMnemonic = False
    '
    'edAn_indirap
    '
    Me.edAn_indirap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_indirap.EditValue = "0"
    Me.edAn_indirap.Location = New System.Drawing.Point(120, 101)
    Me.edAn_indirap.Name = "edAn_indirap"
    Me.edAn_indirap.NTSDbField = ""
    Me.edAn_indirap.NTSFormat = "0"
    Me.edAn_indirap.NTSForzaVisZoom = False
    Me.edAn_indirap.NTSOldValue = ""
    Me.edAn_indirap.Properties.Appearance.Options.UseTextOptions = True
    Me.edAn_indirap.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAn_indirap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_indirap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_indirap.Properties.AutoHeight = False
    Me.edAn_indirap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_indirap.Properties.MaxLength = 65536
    Me.edAn_indirap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_indirap.Size = New System.Drawing.Size(100, 20)
    Me.edAn_indirap.TabIndex = 602
    '
    'lbAn_indirap
    '
    Me.lbAn_indirap.AutoSize = True
    Me.lbAn_indirap.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_indirap.Location = New System.Drawing.Point(6, 104)
    Me.lbAn_indirap.Name = "lbAn_indirap"
    Me.lbAn_indirap.NTSDbField = ""
    Me.lbAn_indirap.Size = New System.Drawing.Size(84, 13)
    Me.lbAn_indirap.TabIndex = 594
    Me.lbAn_indirap.Text = "% Indeducibilità"
    Me.lbAn_indirap.Tooltip = ""
    Me.lbAn_indirap.UseMnemonic = False
    '
    'cbAn_azcom
    '
    Me.cbAn_azcom.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_azcom.DataSource = Nothing
    Me.cbAn_azcom.DisplayMember = ""
    Me.cbAn_azcom.Location = New System.Drawing.Point(507, 214)
    Me.cbAn_azcom.Name = "cbAn_azcom"
    Me.cbAn_azcom.NTSDbField = ""
    Me.cbAn_azcom.Properties.AutoHeight = False
    Me.cbAn_azcom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_azcom.Properties.DropDownRows = 30
    Me.cbAn_azcom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_azcom.SelectedValue = ""
    Me.cbAn_azcom.Size = New System.Drawing.Size(140, 20)
    Me.cbAn_azcom.TabIndex = 597
    Me.cbAn_azcom.ValueMember = ""
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPag4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(661, 293)
    Me.NtsTabPage4.Text = "&4 - Note"
    '
    'pnPag4
    '
    Me.pnPag4.AllowDrop = True
    Me.pnPag4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag4.Appearance.Options.UseBackColor = True
    Me.pnPag4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag4.Controls.Add(Me.edAn_note2)
    Me.pnPag4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag4.Location = New System.Drawing.Point(0, 0)
    Me.pnPag4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag4.Name = "pnPag4"
    Me.pnPag4.NTSActiveTrasparency = True
    Me.pnPag4.Size = New System.Drawing.Size(661, 293)
    Me.pnPag4.TabIndex = 0
    Me.pnPag4.Text = "NtsPanel2"
    '
    'edAn_note2
    '
    Me.edAn_note2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_note2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edAn_note2.Location = New System.Drawing.Point(0, 0)
    Me.edAn_note2.Name = "edAn_note2"
    Me.edAn_note2.NTSDbField = ""
    Me.edAn_note2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_note2.Size = New System.Drawing.Size(661, 293)
    Me.edAn_note2.TabIndex = 554
    '
    'lbAn_contoLabel
    '
    Me.lbAn_contoLabel.AutoSize = True
    Me.lbAn_contoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAn_contoLabel.Location = New System.Drawing.Point(13, 31)
    Me.lbAn_contoLabel.Name = "lbAn_contoLabel"
    Me.lbAn_contoLabel.NTSDbField = ""
    Me.lbAn_contoLabel.Size = New System.Drawing.Size(69, 13)
    Me.lbAn_contoLabel.TabIndex = 10
    Me.lbAn_contoLabel.Text = "Codice conto"
    Me.lbAn_contoLabel.Tooltip = ""
    Me.lbAn_contoLabel.UseMnemonic = False
    '
    'edAn_descr1
    '
    Me.edAn_descr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_descr1.EditValue = "."
    Me.edAn_descr1.Location = New System.Drawing.Point(340, 28)
    Me.edAn_descr1.Name = "edAn_descr1"
    Me.edAn_descr1.NTSDbField = ""
    Me.edAn_descr1.NTSForzaVisZoom = False
    Me.edAn_descr1.NTSOldValue = "."
    Me.edAn_descr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAn_descr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAn_descr1.Properties.AutoHeight = False
    Me.edAn_descr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAn_descr1.Properties.MaxLength = 65536
    Me.edAn_descr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAn_descr1.Size = New System.Drawing.Size(314, 20)
    Me.edAn_descr1.TabIndex = 501
    '
    'edAn_descr2
    '
    Me.edAn_descr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_descr2.Enabled = False
    Me.edAn_descr2.Location = New System.Drawing.Point(132, 54)
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
    Me.edAn_descr2.Size = New System.Drawing.Size(100, 20)
    Me.edAn_descr2.TabIndex = 502
    Me.edAn_descr2.Visible = False
    '
    'lbAn_codmastLabel
    '
    Me.lbAn_codmastLabel.AutoSize = True
    Me.lbAn_codmastLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codmastLabel.Location = New System.Drawing.Point(13, 7)
    Me.lbAn_codmastLabel.Name = "lbAn_codmastLabel"
    Me.lbAn_codmastLabel.NTSDbField = ""
    Me.lbAn_codmastLabel.Size = New System.Drawing.Size(75, 13)
    Me.lbAn_codmastLabel.TabIndex = 17
    Me.lbAn_codmastLabel.Text = "Codice mastro"
    Me.lbAn_codmastLabel.Tooltip = ""
    Me.lbAn_codmastLabel.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.ckAn_fldespdc)
    Me.pnTop.Controls.Add(Me.lbAn_conto)
    Me.pnTop.Controls.Add(Me.lbAn_codmast)
    Me.pnTop.Controls.Add(Me.edAn_descr2)
    Me.pnTop.Controls.Add(Me.lbXx_codmast)
    Me.pnTop.Controls.Add(Me.lbAn_descr2)
    Me.pnTop.Controls.Add(Me.edAn_descr1)
    Me.pnTop.Controls.Add(Me.lbAn_descr1)
    Me.pnTop.Controls.Add(Me.lbAn_contoLabel)
    Me.pnTop.Controls.Add(Me.lbAn_codmastLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(670, 88)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "pnTop"
    '
    'ckAn_fldespdc
    '
    Me.ckAn_fldespdc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_fldespdc.Location = New System.Drawing.Point(340, 55)
    Me.ckAn_fldespdc.Name = "ckAn_fldespdc"
    Me.ckAn_fldespdc.NTSCheckValue = "S"
    Me.ckAn_fldespdc.NTSUnCheckValue = "N"
    Me.ckAn_fldespdc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_fldespdc.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_fldespdc.Properties.AutoHeight = False
    Me.ckAn_fldespdc.Properties.Caption = "Descrizione conto uguale per tutte le ditte con lo stesso PDC"
    Me.ckAn_fldespdc.Size = New System.Drawing.Size(314, 19)
    Me.ckAn_fldespdc.TabIndex = 550
    '
    'lbAn_conto
    '
    Me.lbAn_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_conto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAn_conto.Location = New System.Drawing.Point(132, 27)
    Me.lbAn_conto.Name = "lbAn_conto"
    Me.lbAn_conto.NTSDbField = ""
    Me.lbAn_conto.Size = New System.Drawing.Size(100, 20)
    Me.lbAn_conto.TabIndex = 510
    Me.lbAn_conto.Text = "0"
    Me.lbAn_conto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbAn_conto.Tooltip = ""
    Me.lbAn_conto.UseMnemonic = False
    '
    'lbAn_codmast
    '
    Me.lbAn_codmast.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codmast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_codmast.Location = New System.Drawing.Point(132, 3)
    Me.lbAn_codmast.Name = "lbAn_codmast"
    Me.lbAn_codmast.NTSDbField = ""
    Me.lbAn_codmast.Size = New System.Drawing.Size(100, 20)
    Me.lbAn_codmast.TabIndex = 509
    Me.lbAn_codmast.Text = "0"
    Me.lbAn_codmast.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbAn_codmast.Tooltip = ""
    Me.lbAn_codmast.UseMnemonic = False
    '
    'lbXx_codmast
    '
    Me.lbXx_codmast.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codmast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codmast.Location = New System.Drawing.Point(340, 3)
    Me.lbXx_codmast.Name = "lbXx_codmast"
    Me.lbXx_codmast.NTSDbField = ""
    Me.lbXx_codmast.Size = New System.Drawing.Size(314, 20)
    Me.lbXx_codmast.TabIndex = 508
    Me.lbXx_codmast.Text = "lbXx_codmast"
    Me.lbXx_codmast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codmast.Tooltip = ""
    Me.lbXx_codmast.UseMnemonic = False
    '
    'lbAn_descr2
    '
    Me.lbAn_descr2.AutoSize = True
    Me.lbAn_descr2.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr2.Location = New System.Drawing.Point(14, 57)
    Me.lbAn_descr2.Name = "lbAn_descr2"
    Me.lbAn_descr2.NTSDbField = ""
    Me.lbAn_descr2.Size = New System.Drawing.Size(70, 13)
    Me.lbAn_descr2.TabIndex = 12
    Me.lbAn_descr2.Text = "Descrizione 2"
    Me.lbAn_descr2.Tooltip = ""
    Me.lbAn_descr2.UseMnemonic = False
    Me.lbAn_descr2.Visible = False
    '
    'lbAn_descr1
    '
    Me.lbAn_descr1.AutoSize = True
    Me.lbAn_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr1.Location = New System.Drawing.Point(264, 31)
    Me.lbAn_descr1.Name = "lbAn_descr1"
    Me.lbAn_descr1.NTSDbField = ""
    Me.lbAn_descr1.Size = New System.Drawing.Size(61, 13)
    Me.lbAn_descr1.TabIndex = 11
    Me.lbAn_descr1.Text = "Descrizione"
    Me.lbAn_descr1.Tooltip = ""
    Me.lbAn_descr1.UseMnemonic = False
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsSotc)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 118)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(670, 323)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel2"
    '
    'FRM__SOTC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(670, 441)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__SOTC"
    Me.Text = "SOTTOCONTI DITTA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsSotc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSotc.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    Me.pnPag1.PerformLayout()
    CType(Me.ckAn_ivainded.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_valuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_pcconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCollegamenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCollegamenti.ResumeLayout(False)
    Me.fmCollegamenti.PerformLayout()
    CType(Me.ckAn_sosppr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_flci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmComportamento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmComportamento.ResumeLayout(False)
    Me.fmComportamento.PerformLayout()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValidita.ResumeLayout(False)
    Me.fmValidita.PerformLayout()
    CType(Me.edAn_datini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_datfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_accperi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_funzion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_cksegno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2.ResumeLayout(False)
    CType(Me.pnDati2Top, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDati2Top.ResumeLayout(False)
    Me.pnDati2Top.PerformLayout()
    CType(Me.cbAn_cosvend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_tipacq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_conprof.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_percman.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_totcron.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_manrip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_contrsemp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRiclassificati.ResumeLayout(False)
    Me.fmRiclassificati.PerformLayout()
    CType(Me.ckOpzgest1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnTesoreria, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTesoreria.ResumeLayout(False)
    CType(Me.fmTesoreria, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTesoreria.ResumeLayout(False)
    Me.fmTesoreria.PerformLayout()
    CType(Me.cbAn_trating.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_codvfde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag3.ResumeLayout(False)
    Me.pnPag3.PerformLayout()
    CType(Me.fmUsaContoFunz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUsaContoFunz.ResumeLayout(False)
    Me.fmUsaContoFunz.PerformLayout()
    CType(Me.ckOpzgest6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpzgest5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpzgest4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpzgest3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpzgest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStudi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStudi.ResumeLayout(False)
    Me.fmStudi.PerformLayout()
    CType(Me.cbAn_stseimp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_stsepro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRicavometro, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRicavometro.ResumeLayout(False)
    Me.fmRicavometro.PerformLayout()
    CType(Me.ckAn_ricmimp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_ricmpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmImposte, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmImposte.ResumeLayout(False)
    Me.fmImposte.PerformLayout()
    CType(Me.edAn_indiidd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_indiiddsit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_intragr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIrap, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIrap.ResumeLayout(False)
    Me.fmIrap.PerformLayout()
    CType(Me.edAn_pervari.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_voceirap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_varirap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_indirap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_azcom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4.ResumeLayout(False)
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_descr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.ckAn_fldespdc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__SOTC", "BE__SOTC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128308083950844000, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleSotc = CType(oTmp, CLE__SOTC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__SOTC", strRemoteServer, strRemotePort)
    AddHandler oCleSotc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSotc.Init(oApp, NTSScript, oMenu.oCleComm, "ANAGRA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub CaricaCombo()
    Try

      Dim dttAccPeri As New DataTable()
      dttAccPeri.Columns.Add("cod", GetType(String))
      dttAccPeri.Columns.Add("val", GetType(String))
      dttAccPeri.Rows.Add(New Object() {"N", "(Nessuna)"})
      dttAccPeri.Rows.Add(New Object() {"S", "Periodo di compet. econ."})
      dttAccPeri.Rows.Add(New Object() {"D", "Data compet. Econ."})
      dttAccPeri.Rows.Add(New Object() {"I", "Data compet. Iva"})
      dttAccPeri.Rows.Add(New Object() {"V", "Data Valuta"})
      dttAccPeri.AcceptChanges()
      cbAn_accperi.DataSource = dttAccPeri
      cbAn_accperi.ValueMember = "cod"
      cbAn_accperi.DisplayMember = "val"

      Dim dttSegno As New DataTable()
      dttSegno.Columns.Add("cod", GetType(String))
      dttSegno.Columns.Add("val", GetType(String))
      dttSegno.Rows.Add(New Object() {"0", "Non controlla"})
      dttSegno.Rows.Add(New Object() {"1", "Dare"})
      dttSegno.Rows.Add(New Object() {"2", "Avere"})
      dttSegno.AcceptChanges()
      cbAn_cksegno.DataSource = dttSegno
      cbAn_cksegno.ValueMember = "cod"
      cbAn_cksegno.DisplayMember = "val"

      Dim dttConprof As New DataTable()
      dttConprof.Columns.Add("cod", GetType(String))
      dttConprof.Columns.Add("val", GetType(String))
      dttConprof.Rows.Add(New Object() {"0", "Non interessa"})
      dttConprof.Rows.Add(New Object() {"1", "Altre attività"})
      dttConprof.Rows.Add(New Object() {"2", "Servizi"})
      dttConprof.AcceptChanges()
      cbAn_conprof.DataSource = dttConprof
      cbAn_conprof.ValueMember = "cod"
      cbAn_conprof.DisplayMember = "val"

      Dim dttTotcron As New DataTable()
      dttTotcron.Columns.Add("cod", GetType(String))
      dttTotcron.Columns.Add("val", GetType(String))
      dttTotcron.Rows.Add(New Object() {"0", "Non interessa"})
      dttTotcron.Rows.Add(New Object() {"1", "Compensi percepiti"})
      dttTotcron.Rows.Add(New Object() {"2", "Proventi in sostituzione redditi"})
      dttTotcron.Rows.Add(New Object() {"3", "Ritenute subite"})
      dttTotcron.Rows.Add(New Object() {"4", "Personale dipendente"})
      dttTotcron.Rows.Add(New Object() {"5", "Compensi a terzi"})
      dttTotcron.Rows.Add(New Object() {"6", "Canoni in locazione"})
      dttTotcron.Rows.Add(New Object() {"7", "Interessi passivi"})
      dttTotcron.Rows.Add(New Object() {"8", "Premi di assicurazione"})
      dttTotcron.Rows.Add(New Object() {"9", "Spese alberghiere"})
      dttTotcron.Rows.Add(New Object() {"10", "Spese di rappresentanza"})
      dttTotcron.Rows.Add(New Object() {"11", "Convegni e corsi"})
      dttTotcron.Rows.Add(New Object() {"12", "Altri costi e spese"})
      dttTotcron.Rows.Add(New Object() {"13", "Beni strumentali"})
      dttTotcron.Rows.Add(New Object() {"14", "Contributo Art.12 L.414/91"})
      dttTotcron.Rows.Add(New Object() {"15", "Altre voci non totalizzate"})
      dttTotcron.Rows.Add(New Object() {"16", "Cassa/banca"})
      dttTotcron.AcceptChanges()
      cbAn_totcron.DataSource = dttTotcron
      cbAn_totcron.ValueMember = "cod"
      cbAn_totcron.DisplayMember = "val"

      Dim dttContrSemp As New DataTable()
      dttContrSemp.Columns.Add("cod", GetType(String))
      dttContrSemp.Columns.Add("val", GetType(String))
      dttContrSemp.Rows.Add(New Object() {"0", "Non interessa"})
      dttContrSemp.Rows.Add(New Object() {"1", "Non utilizzabile in semplif."})
      dttContrSemp.Rows.Add(New Object() {"2", "Utilizzabile in semplif."})
      dttContrSemp.Rows.Add(New Object() {"3", "Utilizz. ma non stampa nei Registri"})
      dttContrSemp.AcceptChanges()
      cbAn_contrsemp.DataSource = dttContrSemp
      cbAn_contrsemp.ValueMember = "cod"
      cbAn_contrsemp.DisplayMember = "val"

      Dim dttManrip As New DataTable()
      dttManrip.Columns.Add("cod", GetType(String))
      dttManrip.Columns.Add("val", GetType(String))
      dttManrip.Rows.Add(New Object() {"0", "Non interessa"})
      dttManrip.Rows.Add(New Object() {"1", "Interessa +"})
      dttManrip.Rows.Add(New Object() {"2", "Interessa -"})
      dttManrip.AcceptChanges()
      cbAn_manrip.DataSource = dttManrip
      cbAn_manrip.ValueMember = "cod"
      cbAn_manrip.DisplayMember = "val"

      Dim dttColbil As New DataTable()
      dttColbil.Columns.Add("cod", GetType(String))
      dttColbil.Columns.Add("val", GetType(String))
      dttColbil.Rows.Add(New Object() {"0", "Non interessa"})
      dttColbil.Rows.Add(New Object() {"1", "Sezione opposta"})
      dttColbil.Rows.Add(New Object() {"2", "Dare"})
      dttColbil.Rows.Add(New Object() {"3", "Avere"})
      dttColbil.AcceptChanges()
      cbAn_colbil.DataSource = dttColbil
      cbAn_colbil.ValueMember = "cod"
      cbAn_colbil.DisplayMember = "val"

      Dim dttVarirap As New DataTable()
      dttVarirap.Columns.Add("cod", GetType(String))
      dttVarirap.Columns.Add("val", GetType(String))
      dttVarirap.Rows.Add(New Object() {"0", "Non interessa"})
      dttVarirap.Rows.Add(New Object() {"3", "In aumento + valore contabile"})
      dttVarirap.Rows.Add(New Object() {"4", "In diminuzione + valore contabile"})
      dttVarirap.AcceptChanges()
      cbAn_varirap.DataSource = dttVarirap
      cbAn_varirap.ValueMember = "cod"
      cbAn_varirap.DisplayMember = "val"

      Dim dttAzcom As New DataTable()
      dttAzcom.Columns.Add("cod", GetType(String))
      dttAzcom.Columns.Add("val", GetType(String))
      dttAzcom.Rows.Add(New Object() {"0", "Non interessa"})
      dttAzcom.Rows.Add(New Object() {"10", "Partecipazioni"})
      dttAzcom.Rows.Add(New Object() {"20", "Crediti"})
      dttAzcom.Rows.Add(New Object() {"30", "Immobili"})
      dttAzcom.Rows.Add(New Object() {"31", "Altre immobilizzazioni"})
      dttAzcom.Rows.Add(New Object() {"35", "Ricavi"})
      dttAzcom.Rows.Add(New Object() {"36", "Incrementi di rimanenze"})
      dttAzcom.Rows.Add(New Object() {"37", "Proventi"})
      dttAzcom.AcceptChanges()
      cbAn_azcom.DataSource = dttAzcom
      cbAn_azcom.ValueMember = "cod"
      cbAn_azcom.DisplayMember = "val"

      Dim dttCosvend As New DataTable()
      dttCosvend.Columns.Add("cod", GetType(String))
      dttCosvend.Columns.Add("val", GetType(String))
      dttCosvend.Rows.Add(New Object() {"0", "Non interessa"})
      dttCosvend.Rows.Add(New Object() {"1", "Acquisto merci 1.a"})
      dttCosvend.Rows.Add(New Object() {"2", "Acquisto merci 2.a"})
      dttCosvend.Rows.Add(New Object() {"3", "Rim. in. 1.a"})
      dttCosvend.Rows.Add(New Object() {"4", "Rim. in. 2.a"})
      dttCosvend.Rows.Add(New Object() {"5", "Ricavi 1.a"})
      dttCosvend.Rows.Add(New Object() {"6", "Ricavi 2.a"})
      dttCosvend.AcceptChanges()
      cbAn_cosvend.DataSource = dttCosvend
      cbAn_cosvend.ValueMember = "cod"
      cbAn_cosvend.DisplayMember = "val"

      Dim dttStseimp As New DataTable()
      dttStseimp.Columns.Add("cod", GetType(String))
      dttStseimp.Columns.Add("val", GetType(String))
      dttStseimp.Rows.Add(New Object() {"0", "Non interessa"})
      dttStseimp.Rows.Add(New Object() {"1", "Studi di settore"})
      dttStseimp.Rows.Add(New Object() {"2", "Gestione punto/attività"})
      dttStseimp.Rows.Add(New Object() {"3", "Studi e gestione punto/attività"})
      dttStseimp.AcceptChanges()
      cbAn_stseimp.DataSource = dttStseimp
      cbAn_stseimp.ValueMember = "cod"
      cbAn_stseimp.DisplayMember = "val"

      Dim dttStsepro As New DataTable()
      dttStsepro.Columns.Add("cod", GetType(String))
      dttStsepro.Columns.Add("val", GetType(String))
      dttStsepro.Rows.Add(New Object() {"0", "Non interessa"})
      dttStsepro.Rows.Add(New Object() {"1", "Studi di settore"})
      dttStsepro.Rows.Add(New Object() {"2", "Gestione punto/attività"})
      dttStsepro.Rows.Add(New Object() {"3", "Studi e gestione punto/attività"})
      dttStsepro.AcceptChanges()
      cbAn_stsepro.DataSource = dttStsepro
      cbAn_stsepro.ValueMember = "cod"
      cbAn_stsepro.DisplayMember = "val"

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
      cbAn_tipacq.DataSource = dttTipacq
      cbAn_tipacq.ValueMember = "cod"
      cbAn_tipacq.DisplayMember = "val"

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

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbPartitario.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAn_descr1.NTSSetParam(oMenu, oApp.Tr(Me, 128326960191176000, "Descrizione 1"), 30, False)
      edAn_descr2.NTSSetParam(oMenu, oApp.Tr(Me, 128308083950532000, "Descrizione 2"), 30, True)
      ckAn_fldespdc.NTSSetParam(oMenu, oApp.Tr(Me, 129762081225783443, "Descrizione conto uguale per tutte le ditte con lo stesso PDC"), "S", "N")
      edAn_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960229616000, "Controp. ratei/risconti"), tabanagras)
      ckAn_partite.NTSSetParam(oMenu, oApp.Tr(Me, 128326960243166000, "Gestione partite"), "S", "N")
      ckAn_scaden.NTSSetParam(oMenu, oApp.Tr(Me, 128326960257806000, "Gestione scadenze"), "S", "N")
      edAn_pcconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960282606000, "Conto PDC"), tabanagpc)
      cbAn_accperi.NTSSetParam(oApp.Tr(Me, 128326960301886000, "Richiedi date"))
      edAn_valuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326999713556000, "Valuta estera"), tabvalu)
      edAn_note.NTSSetParam(oMenu, oApp.Tr(Me, 128326960318846000, "Note brevi"), 30, True)
      edAn_kpccee.NTSSetParam(oMenu, oApp.Tr(Me, 128326960331886000, "Saldo dare CEE"), 8, True)
      edAn_kpccee2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960345646000, "Saldo avere CEE"), 8, True)
      edAn_rifricd.NTSSetParam(oMenu, oApp.Tr(Me, 128326960360916000, "Sado dare RICLASS."), 8, True)
      edAn_rifrica.NTSSetParam(oMenu, oApp.Tr(Me, 128326960384356000, "Saldo avere RICLASS"), 8, True)
      edAn_funzion.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960398596000, "Conto funzionamento"), tabanagpc)
      cbAn_tipacq.NTSSetParam(oApp.Tr(Me, 128326960416436000, "Tipo acquisto Quadro 'A' IVA 11"))
      cbAn_cksegno.NTSSetParam(oApp.Tr(Me, 128326960429156000, "Test segno"))
      cbAn_conprof.NTSSetParam(oApp.Tr(Me, 128326960441076000, "Imprese miste"))
      cbAn_totcron.NTSSetParam(oApp.Tr(Me, 128326960452516000, "Totalizz. registro cronologico"))
      cbAn_contrsemp.NTSSetParam(oApp.Tr(Me, 128326960463956000, "Controlla semplificata"))
      cbAn_manrip.NTSSetParam(oApp.Tr(Me, 128326960474676000, "Manutenzione e riparazione"))
      edAn_percman.NTSSetParam(oMenu, oApp.Tr(Me, 128326960485636000, "% manutenz. e riparaz."), oApp.FormatSconti, 6, 0, 100)
      cbAn_colbil.NTSSetParam(oApp.Tr(Me, 128326960496516000, "Colonna in stampa bilancio a sez. contrapposte"))
      edAn_voceirap.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960509796000, "Voce IRAP"), tabdira)
      cbAn_varirap.NTSSetParam(oApp.Tr(Me, 128326960524116000, "Tipo variaz. IRAP"))
      edAn_pervari.NTSSetParam(oMenu, oApp.Tr(Me, 128326960536276000, "% variaz. IRAP"), oApp.FormatSconti, 6, 0, 100)
      cbAn_cosvend.NTSSetParam(oApp.Tr(Me, 128326960552106000, "Calcola costo del venduto"))
      edAn_indiidd.NTSSetParam(oMenu, oApp.Tr(Me, 128326960565706000, "% inded. imposte dirette"), oApp.FormatSconti, 6, 0, 100)
      cbAn_azcom.NTSSetParam(oApp.Tr(Me, 128326960578586000, "Azienda di comodo"))
      ckAn_ricmimp.NTSSetParam(oMenu, oApp.Tr(Me, 128326960590276000, "Ricavometro Imprese"), "1", "0")
      ckAn_ricmpro.NTSSetParam(oMenu, oApp.Tr(Me, 128326960602266000, "Ricavometro Professionisti"), "1", "0")
      cbAn_stseimp.NTSSetParam(oApp.Tr(Me, 128326960612986000, "Studi di settore Imprese"))
      cbAn_stsepro.NTSSetParam(oApp.Tr(Me, 128326960624426000, "Studi di settore Professionisti"))
      ckAn_sosppr.NTSSetParam(oMenu, oApp.Tr(Me, 128326960638986000, "Collegamento a costi/ricavi sospesi"), "S", "N")
      ckAn_intragr.NTSSetParam(oMenu, oApp.Tr(Me, 128326960650106000, "Crediti/Debiti/Costi/Ricavi intragruppo"), "S", "N")
      edAn_indirap.NTSSetParam(oMenu, oApp.Tr(Me, 128326960660826000, "% indeducibilita IRAP"), oApp.FormatSconti, 6, 0, 100)
      edAn_indiiddsit.NTSSetParam(oMenu, oApp.Tr(Me, 128326960671866000, "% indeducibilità imposte dirette"), oApp.FormatSconti, 6, -100, 100)
      edAn_datini.NTSSetParam(oMenu, oApp.Tr(Me, 128326960681866000, "Data inizio validità"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edAn_datfin.NTSSetParam(oMenu, oApp.Tr(Me, 128326960691786000, "Data fine validità"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      ckAn_flci.NTSSetParam(oMenu, oApp.Tr(Me, 128326960699546000, "Collegamento con Piano dei Conti di C.A."), "1", " ")
      edAn_note2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960718506000, "Note"), 0)
      ckOpzgest1.NTSSetParam(oMenu, oApp.Tr(Me, 128326960738976000, "Usa conto funzionamento"), "S", "N")
      ckOpzgest6.NTSSetParam(oMenu, oApp.Tr(Me, 128326960748416000, "Azienda di comodo"), "S", "N")
      ckOpzgest5.NTSSetParam(oMenu, oApp.Tr(Me, 128326960758096000, "Studi di settore"), "S", "N")
      ckOpzgest4.NTSSetParam(oMenu, oApp.Tr(Me, 128326960767616000, "Ricavometro"), "S", "N")
      ckOpzgest3.NTSSetParam(oMenu, oApp.Tr(Me, 128326960778576000, "Percentuali inded. imposte dirette"), "S", "N")
      ckOpzgest2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960788416000, "Dati IRAP"), "S", "N")
      cbAn_trating.NTSSetParam(oApp.Tr(Me, 129525996340650134, "Rating"))
      edAn_codvfde.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129525996340806386, "Voce finanziaria"), tabvfde, True)
      ckAn_ivainded.NTSSetParam(oMenu, oApp.Tr(Me, 129773928638549080, "Flag Gestione iva indetraibile"), "S", "N")

      edAn_descr1.NTSSetRichiesto()
      edAn_descr1.NTSSetParamZoom("")
      edAn_descr1.NTSForzaVisZoom = True
      edAn_kpccee.NTSSetParamZoom("ZOOMHLCE")
      edAn_kpccee2.NTSSetParamZoom("ZOOMHLCE")
      edAn_rifricd.NTSSetParamZoom("ZOOMHLCE")
      edAn_rifrica.NTSSetParamZoom("ZOOMHLCE")

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
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      lbAn_conto.NTSDbField = "ANAGRA.an_conto"
      edAn_descr1.NTSDbField = "ANAGRA.an_descr1"
      edAn_descr2.NTSDbField = "ANAGRA.an_descr2"
      ckAn_fldespdc.NTSText.NTSDbField = "ANAGRA.an_fldespdc"
      edAn_controp.NTSDbField = "ANAGRA.an_controp"
      ckAn_partite.NTSText.NTSDbField = "ANAGRA.an_partite"
      ckAn_scaden.NTSText.NTSDbField = "ANAGRA.an_scaden"
      cbAn_accperi.NTSDbField = "ANAGRA.an_accperi"
      lbAn_codmast.NTSDbField = "ANAGRA.an_codmast"
      edAn_note.NTSDbField = "ANAGRA.an_note"
      edAn_note2.NTSDbField = "ANAGRA.an_note2"
      edAn_kpccee.NTSDbField = "ANAGRA.an_kpccee"
      edAn_kpccee2.NTSDbField = "ANAGRA.an_kpccee2"
      edAn_rifricd.NTSDbField = "ANAGRA.an_rifricd"
      edAn_rifrica.NTSDbField = "ANAGRA.an_rifrica"
      edAn_funzion.NTSDbField = "ANAGRA.an_funzion"
      cbAn_tipacq.NTSDbField = "ANAGRA.an_tipacq"
      cbAn_cksegno.NTSDbField = "ANAGRA.an_cksegno"
      cbAn_conprof.NTSDbField = "ANAGRA.an_conprof"
      cbAn_totcron.NTSDbField = "ANAGRA.an_totcron"
      cbAn_contrsemp.NTSDbField = "ANAGRA.an_contrsemp"
      cbAn_manrip.NTSDbField = "ANAGRA.an_manrip"
      edAn_percman.NTSDbField = "ANAGRA.an_percman"
      cbAn_colbil.NTSDbField = "ANAGRA.an_colbil"
      edAn_voceirap.NTSDbField = "ANAGRA.an_voceirap"
      cbAn_varirap.NTSDbField = "ANAGRA.an_varirap"
      edAn_pervari.NTSDbField = "ANAGRA.an_pervari"
      cbAn_cosvend.NTSDbField = "ANAGRA.an_cosvend"
      edAn_indiidd.NTSDbField = "ANAGRA.an_indiidd"
      cbAn_azcom.NTSDbField = "ANAGRA.an_azcom"
      ckAn_ricmimp.NTSText.NTSDbField = "ANAGRA.an_ricmimp"
      ckAn_ricmpro.NTSText.NTSDbField = "ANAGRA.an_ricmpro"
      cbAn_stseimp.NTSDbField = "ANAGRA.an_stseimp"
      cbAn_stsepro.NTSDbField = "ANAGRA.an_stsepro"
      ckAn_sosppr.NTSText.NTSDbField = "ANAGRA.an_sosppr"
      ckAn_intragr.NTSText.NTSDbField = "ANAGRA.an_intragr"
      edAn_indirap.NTSDbField = "ANAGRA.an_indirap"
      edAn_indiiddsit.NTSDbField = "ANAGRA.an_indiiddsit"
      edAn_datini.NTSDbField = "ANAGRA.an_datini"
      edAn_datfin.NTSDbField = "ANAGRA.an_datfin"
      ckAn_flci.NTSText.NTSDbField = "ANAGRA.an_flci"
      edAn_valuta.NTSDbField = "ANAGRA.an_valuta"
      edAn_pcconto.NTSDbField = "ANAGRA.an_pcconto"
      lbAn_codpcon.NTSDbField = "ANAGRA.an_codpcon"
      lbXx_codmast.NTSDbField = "ANAGRA.xx_codmast"
      lbXx_voceirap.NTSDbField = "ANAGRA.xx_voceirap"
      lbXx_controp.NTSDbField = "ANAGRA.xx_controp"
      lbXx_funzion.NTSDbField = "ANAGRA.xx_funzion"
      lbXx_pccontodescr.NTSDbField = "ANAGRA.xx_pcconto"
      lbXx_valuta.NTSDbField = "ANAGRA.xx_valuta"
      cbAn_trating.NTSDbField = "ANAGRA.an_trating"
      edAn_codvfde.NTSDbField = "ANAGRA.an_codvfde"
      lbXx_codvfde.NTSDbField = "ANAGRA.xx_codvfde"
      ckAn_ivainded.NTSText.NTSDbField = "ANAGRA.an_ivainded"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcSotc, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


#Region "Eventi Form"
  Public Overridable Sub FRM__SOTC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      CaricaCombo()
      InitControls()

      tsSotc.SelectedTabPageIndex = 0
      pnTop.Visible = False
      pnMain.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCI) = True Then
        oCleSotc.bModCI = True
      Else
        oCleSotc.bsModSupCAE = True
      End If
      If oCleSotc.bsModSupCAE = True Then
        tlbAnacent.Enabled = False
        tlbAnacent.Visible = False
        tlbAnalink.Visible = False
        ckAn_flci.Enabled = False
      Else
        tlbAnacent2.Enabled = False
        tlbAnacent2.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__SOTC_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Dim lConto As Integer = 0

    Try
      If oCallParams Is Nothing Then
        oCleSotc.strDittaCorrente = DittaCorrente
        tlbCambiaDitta_ItemClick(tlbCambiaDitta, Nothing)
      Else
        '--------------------------------------------
        'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
        oCleSotc.LeggiDatiDitta(DittaCorrente)

        GctlSetVisEnab(pnTop, True)
        GctlSetVisEnab(pnMain, True)

        If Not Apri() Then
          Me.Close()
          Return
        End If

        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          tlbNuovo_ItemClick(Me, Nothing)
        ElseIf oCallParams.strParam.Trim <> "" Then
          If IsNumeric(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6)) Then
            lConto = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          ElseIf IsNumeric(Microsoft.VisualBasic.Mid(oCallParams.strParam, 8)) Then
            lConto = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 8))
          End If
          For i = 0 To dcSotc.List.Count - 1
            If NTSCInt(CType(dcSotc.Item(i), DataRowView)!an_conto.ToString) = lConto Then
              dcSotc.Position = i
              CaricaColonneUnbound(CType(dcSotc.Item(i), DataRowView).Row)
              Exit For
            End If
          Next
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__SOTC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__SOTC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSotc.Dispose()
      dsSotc.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmNuoa As FRM__NUOA = Nothing
    Try
      frmNuoa = CType(NTSNewFormModal("FRM__NUOA"), FRM__NUOA)
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return

      oCleSotc.bNuovoContoProposto = True

      '------------------------
      'visualizzo la form
      frmNuoa.Init(oMenu, Nothing)
      frmNuoa.InitEntity(oCleSotc)
      frmNuoa.ShowDialog()
      If frmNuoa.bOk = False Then Return

      Me.NTSCorreggiTabIndex(Me)

      oCleSotc.lNuovoMastro = NTSCInt(frmNuoa.edMastro.Text())
      oCleSotc.Nuovo()
      dcSotc.MoveLast()

      CType(dcSotc.Current, DataRowView).Row!an_conto = frmNuoa.lContoOut.ToString
      If frmNuoa.ckPdc.Checked Then CType(dcSotc.Current, DataRowView).Row!an_pcconto = frmNuoa.lContoOut.ToString 'nuovo conto valido come conto PDC

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

      CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)

      'su nuovi sottoconti la descrizione è sempre valida per tutte le ditte!!!!! (altrimenti che descrizione metterei sulle altre?)
      ckAn_fldespdc.Checked = True
      ckAn_fldespdc.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmNuoa Is Nothing Then frmNuoa.Dispose()
      frmNuoa = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Try
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128309626884786000, "Inserire il codice sottoconto"), "0")
      If Not IsNumeric(strTmp) Then Return
      If dsSotc.Tables("ANAGRA").Select("an_conto = " & strTmp).Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128309627733894000, "Codice Inesistente"))
        Return
      Else
        'mi posiziono sul codice inserito
        For i = 0 To dcSotc.List.Count - 1
          If CType(dcSotc.Item(i), DataRowView)!an_conto.ToString = NTSCInt(strTmp).ToString Then
            dcSotc.Position = i
            CaricaColonneUnbound(CType(dcSotc.Item(i), DataRowView).Row)
            Exit For
          End If
        Next
      End If


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
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Try

      '-------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128308083951000000, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          '--------------------------
          'test pre-cancellazione
          If Not oCleSotc.TestPreCancellaAnagra(CType(dcSotc.Current, DataRowView).Row) Then Return

          '--------------------------
          'cancello prima da DB poi dal datatable corrente
          Me.Cursor = Cursors.WaitCursor
          If oCleSotc.SalvaAnagra(True, CType(dcSotc.Current, DataRowView).Row) Then

            If NTSCInt(edAn_pcconto.Text) <> 0 Then
              'verifico se anche l'anagra corrente è stato cancellato: se non cancellato perchè movimentato devo rileggere anagra 
              'perchè è stato sganciato dal PDC generale
              oMenu.ValCodiceDb(lbAn_conto.Text, DittaCorrente, "ANAGRA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                CType(dcSotc.Current, DataRowView).Row!an_pcconto = 0
                CType(dcSotc.Current, DataRowView).Row!xx_pcconto = ""
                CType(dcSotc.Current, DataRowView).Row!an_ultagg = dttTmp.Rows(0)!an_ultagg
                CType(dcSotc.Current, DataRowView).Row.AcceptChanges()
                oCleSotc.bHasChanges = False
              Else
                bOk = True
              End If
              dttTmp.Clear()
            Else
              bOk = True
            End If

            If bOk Then
              If dsSotc.Tables("ANAGRA").Rows.Count = 1 Then
                bRemovBinding = True
                NTSFormClearDataBinding(Me)
              End If

              dcSotc.RemoveAt(dcSotc.Position)
            End If
            dsSotc.AcceptChanges()

          End If    'If oCleSotc.SalvaAnagra(True, CType(dcSotc.Current, DataRowView).Row) Then
          Me.Cursor = Cursors.Default

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcSotc, Me)
            bRemovBinding = False
          End If
          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSotc, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128308083951156000, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsSotc.Tables("ANAGRA").Rows.Count = 1 And dsSotc.Tables("ANAGRA").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleSotc.Ripristina(dcSotc.Position, dcSotc.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcSotc, Me)
            bRemovBinding = False
          End If
          edAn_funzion_ValidatedAndChanged(edAn_funzion, Nothing)
          CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSotc, Me)
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

      Dim i As Integer = 0
      If edAn_descr1.Focused Then
        '------------------------------------
        'lancio lo zoom solo per riposizionare il BindingSource
        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!
        If oCleSotc.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128999172466993156, "Salvare o ripristinare le modifiche prima di selezionare un nuovo sottoconto"))
          Return
        End If

        NTSZOOM.strIn = lbAn_conto.Text
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)

        If NTSZOOM.strIn <> lbAn_conto.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcSotc.List.Count - 1
            If CType(dcSotc.Item(i), DataRowView)!an_conto.ToString = NTSZOOM.strIn Then
              dcSotc.Position = i
              CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
              Exit For
            End If
          Next
        End If
      ElseIf edAn_controp.Focused Then
        '------------------------------------
        'zoom controp ratei/risconti
        SetFastZoom(edAn_controp.Text, oParam)
        NTSZOOM.strIn = edAn_controp.Text
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSCInt(edAn_controp.Text) <> NTSCInt(NTSZOOM.strIn) Then edAn_controp.Text = NTSZOOM.strIn

      ElseIf edAn_funzion.Focused Then
        '------------------------------------
        'zoom controp conto funzionamento
        SetFastZoom(edAn_funzion.Text, oParam)
        NTSZOOM.strIn = edAn_funzion.Text
        oParam.bTipoProposto = False
        oParam.strCodPdc = IIf(oCleSotc.bPDCProfess, "Standard-PR", "Standard-AZ").ToString
        NTSZOOM.ZoomStrIn("ZOOMANAGPC", DittaCorrente, oParam)
        If NTSCInt(edAn_funzion.Text) <> NTSCInt(NTSZOOM.strIn) Then edAn_funzion.Text = NTSZOOM.strIn

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
    If Not Salva() Then Return
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    '-------------------------------------------------
    'vado sul primo record
    If Not Salva() Then Return
    dcSotc.MoveFirst()
    CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva() Then Return
    dcSotc.MovePrevious()
    CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva() Then Return
    dcSotc.MoveNext()
    CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva() Then Return
    dcSotc.MoveLast()
    CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
  End Sub

  Public Overridable Sub tlbPartitario_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPartitario.ItemClick
    Dim oParam As New CLE__PATB
    Try
      If oCleSotc Is Nothing Then Return

      oParam.lContoCF = NTSCInt(lbAn_conto.Text)
      NTSZOOM.strIn = ""
      oParam.nEscomp = NTSCInt(oCleSotc.dttAnaz.Rows(0)!tb_escomp)

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
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbCambiaDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambiaDitta.ItemClick
    Dim oParam As New CLE__PATB

    Try
      '---------------------------
      'se il sottoconto è in modifica prima chiedo di salvare
      Me.ValidaLastControl()
      If oCleSotc.RecordIsChanged Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128999172522932088, "Salvare o ripristinare le modifiche prima di selezionare un nuovo sottoconto"))
        Return
      End If

      If Not LeggiDatiDitta() Then
        Me.Close()
        Return
      End If

      GctlSetVisEnab(pnTop, True)
      GctlSetVisEnab(pnMain, True)


      If Not Apri() Then
        Me.Close()
        Return
      End If

      '--------------------------------
      'se PDC non professionista nasconto campi che non servono
      If oCleSotc.bPDCProfess Then
        GctlSetVisEnab(ckAn_sosppr, False)
        GctlSetVisEnab(cbAn_totcron, False)
        GctlSetVisEnab(lbAn_totcron, False)
        GctlSetVisEnab(ckAn_ricmpro, False)
        GctlSetVisEnab(cbAn_stsepro, False)
        GctlSetVisEnab(lbAn_stsepro, False)
      Else
        ckAn_sosppr.Enabled = False
        cbAn_totcron.Enabled = False
        lbAn_totcron.Enabled = False
        ckAn_ricmpro.Enabled = False
        cbAn_stsepro.Enabled = False
        lbAn_stsepro.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbAnacent_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAnacent.ItemClick
    Dim dsTmp As DataSet = Nothing
    Dim frmAnac As FRMCIANAC = Nothing
    Try
      frmAnac = CType(NTSNewFormModal("FRMCIANAC"), FRMCIANAC)
      If Not Salva() Then Return

      '-----------------------------------------------------------------------------------------
      '--- Controlla se esistono conti in ANAGCA
      '-----------------------------------------------------------------------------------------
      oCleSotc.GetAnagCA(dsTmp)
      If dsTmp.Tables("ANAGCA").Rows.Count > 0 Then
        If NTSCInt(dsTmp.Tables("ANAGCA").Rows(0)!Records) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128582765318756789, "Non esistono Conti in Anagrafica Sottoconti di Contabilità Analitica."))
          Exit Sub
        End If
      End If

      oCleSotc.lContoAnac = NTSCInt(lbAn_conto.Text)

      frmAnac.Init(oMenu, Nothing, DittaCorrente)
      frmAnac.InitEntity(oCleSotc)
      frmAnac.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmAnac Is Nothing Then frmAnac.Dispose()
      frmAnac = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbAnalink_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAnalink.ItemClick
    Try
      If Not Salva() Then Return

      oCleSotc.lContoAlin = NTSCInt(lbAn_conto.Text)

      Dim frmAlin As FRMCIALIN = Nothing
      frmAlin = CType(NTSNewFormModal("FRMCIALIN"), FRMCIALIN)
      frmAlin.Init(oMenu, Nothing, DittaCorrente)
      frmAlin.InitEntity(oCleSotc)
      frmAlin.ShowDialog(Me)
      If Not frmAlin Is Nothing Then frmAlin.Dispose()
      frmAlin = Nothing

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbAnacent2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAnacent2.ItemClick
    Dim frmAna2 As FRMCIANA2 = Nothing

    Try
      frmAna2 = CType(NTSNewFormModal("FRMCIANA2"), FRMCIANA2)
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva() Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleSotc.strCodpcon = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_codpcon)
      oCleSotc.lConto = NTSCInt(CType(dcSotc.Current, DataRowView).Row!an_pcconto)
      '--------------------------------------------------------------------------------------------------------------
      frmAna2.Init(oMenu, Nothing, DittaCorrente)
      frmAna2.InitEntity(oCleSotc)
      frmAna2.ShowDialog(Me)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmAna2 Is Nothing Then frmAna2.Dispose()
      frmAna2 = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Stampa(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Stampa(0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function LeggiDatiDitta() As Boolean
    Dim bDbMultiDitta As Boolean = False
    Dim bAnagen As Boolean = False
    Try
Riprova:
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BN--SOTC", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Return False

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambiaDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambiaDitta, True)
      End If

      '-------------------------------------------------
      'leggo le informazioni relative alla ditta corrente
      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)
      If Not oCleSotc.LeggiDatiDitta(DittaCorrente) Then
        If bDbMultiDitta Then
          GoTo Riprova
        Else
          Return False
        End If
      End If

      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCI) = True Then
        oCleSotc.bModCI = True
      Else
        oCleSotc.bsModSupCAE = True
      End If
      If oCleSotc.bsModSupCAE = True Then
        tlbAnacent.Enabled = False
        tlbAnacent.Visible = False
        tlbAnalink.Visible = False
        ckAn_flci.Enabled = False
        GctlSetVisEnab(tlbAnacent2, True)
      Else
        tlbAnacent2.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------

      'se il database è monoditta non posso inserire sottoconti ditta (per evitare che l'utente si confonda)
      'GctlSetVisEnab(tlbNuovo, False)
      'If bDbMultiDitta = False Then
      '  If CBool(oMenu.GetSettingBus("BS--SOTC", "OPZIONI", ".", "AllowNew", "0", " ", "0")) = False Then  tlbNuovo.Enabled = False
      'End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Function Apri() As Boolean
    Try
      Me.Cursor = Cursors.WaitCursor
      tsSotc.SelectedTabPageIndex = 0

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleSotc.Apri(DittaCorrente, dsSotc) Then
        Me.Close()
        Return False
      End If
      Try
        dcSotc.DataSource = dsSotc.Tables("ANAGRA")
      Catch
        'bo.... quando cambio da pdc con sottocnti ad uno senza sottoconti la prima volta da errore, poi entra ...
      End Try
      dcSotc.DataSource = dsSotc.Tables("ANAGRA")

      dsSotc.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      If dsSotc.Tables("ANAGRA").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      If dsSotc.Tables("ANAGRA").Rows.Count = 0 Then Return False
      dcSotc.ResetBindings(False)
      dcSotc.MoveFirst()

      CaricaColonneUnbound(CType(dcSotc.Current, DataRowView).Row)
      ckAn_flci_CheckedChanged(ckAn_flci, Nothing)
      Me.NTSCorreggiTabIndex(Me)

      oCleSotc.bHasChanges = False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub CaricaColonneUnbound(ByRef dtrIn As DataRow)
    Dim bContoMovimentato As Boolean = False
    Try

      edAn_funzion_ValidatedAndChanged(edAn_funzion, Nothing)
      oCleSotc.CaricaColonneUnbound(dtrIn, bContoMovimentato)
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

      If oCleSotc.bsModSupCAE = True Then
        tlbAnacent.Enabled = False
        tlbAnacent.Visible = False
        tlbAnalink.Visible = False
        'posso cambiare il flag di gestione CA2 solo se è un conto collegato al PDC!!!
        If NTSCInt(edAn_pcconto.Text) <> 0 Then
          GctlSetVisEnab(ckAn_flci, False)
        Else
          ckAn_flci.Enabled = False
        End If
      End If

      '----------------------------------------------
      'il flag di descrizione specifica ditta è utilizzabile solo su sottoconti non specifici ditta
      If NTSCInt(edAn_pcconto.Text) = 0 Then
        ckAn_fldespdc.Enabled = False
      Else
        GctlSetVisEnab(ckAn_fldespdc, False)
      End If

      '----------------------------------------------
      'blocco tutto se è un pdc standard e l'utnete non è NTS / NTS
      If oCleSotc.bAllowOperation = False Then
        'edAn_descr1.Properties.ReadOnly = True
        edAn_controp.Enabled = False
        edAn_funzion.Enabled = False
        cbAn_accperi.Enabled = False
        cbAn_cksegno.Enabled = False
        edAn_note.Enabled = False
        fmComportamento.Enabled = False
        fmCollegamenti.Enabled = False
        fmValidita.Enabled = False
        pnDati2Top.Enabled = False
        ckOpzgest1.Enabled = False
        edAn_rifricd.Enabled = False
        edAn_rifrica.Enabled = False
        edAn_kpccee.Enabled = False
        edAn_kpccee2.Enabled = False
        pnPag3.Enabled = False
        pnPag4.Enabled = False
      Else
        'edAn_descr1.Properties.ReadOnly = False
        GctlSetVisEnab(edAn_controp, False)
        GctlSetVisEnab(edAn_funzion, False)
        GctlSetVisEnab(cbAn_accperi, False)
        GctlSetVisEnab(cbAn_cksegno, False)
        GctlSetVisEnab(edAn_note, False)
        GctlSetVisEnab(fmComportamento, False)
        GctlSetVisEnab(fmCollegamenti, False)
        GctlSetVisEnab(fmValidita, False)
        GctlSetVisEnab(pnDati2Top, False)
        GctlSetVisEnab(ckOpzgest1, False)
        GctlSetVisEnab(edAn_rifricd, False)
        GctlSetVisEnab(edAn_rifrica, False)
        GctlSetVisEnab(edAn_kpccee, False)
        GctlSetVisEnab(edAn_kpccee2, False)
        GctlSetVisEnab(pnPag3, False)
        GctlSetVisEnab(pnPag4, False)
      End If
      ckAn_fldespdc_CheckedChanged(ckAn_fldespdc, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleSotc.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128308083951312000, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Me.Cursor = Cursors.WaitCursor
          If Not oCleSotc.SalvaAnagra(False, CType(dcSotc.Current, DataRowView).Row) Then Return False
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub ckAn_fldespdc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAn_fldespdc.CheckedChanged
    'se sottoconti specifici ditta: sempre spuntata e disabilitata
    'su sottoconti in stato NUOVO: sempre spuntata e disabilitata
    'su sottoconti PDC di PDC non NTS: modificabile
    'su sottoconti PDC di PDC NTS e utente/pwd NTS/NTS: modificabile
    'su sottoconti PDC di PDC NTS e utente/pwd no NTS/NTS: se spuntata non modificabile, altrimenti modificabile
    Dim strTmp As String = ""
    Try
      If ckAn_fldespdc.Checked Then
        If NTSCInt(CType(dcSotc.Current, DataRowView).Row!an_pcconto) <> 0 Then
          'se il conto è collegato ad un pdc, se rimetto la spunta devo riproporre la descrizione 
          'comune in tutte le altre anagra delle ditte con stesso pdc
          If CType(dcSotc.Current, DataRowView).Row.RowState = DataRowState.Modified Then
            oMenu.ValCodiceDb(CType(dcSotc.Current, DataRowView).Row!an_pcconto.ToString, "", "ANAGPC", "N", strTmp, Nothing, CType(dcSotc.Current, DataRowView).Row!an_codpcon.ToString)
            CType(dcSotc.Current, DataRowView).Row!an_descr1 = strTmp
          End If
        End If
        If oCleSotc.bAllowOperation = False Then
          edAn_descr1.Properties.ReadOnly = True
        Else
          edAn_descr1.Properties.ReadOnly = False
        End If
      Else
        'anche se è un sottotonto del pdc NTS e non è l'utente NTS faccio modificare la descrizione, 
        'visto che verrà salvata solo sulla ditta corrente
        If oCleSotc.bAllowOperation = False Then
          edAn_descr1.Properties.ReadOnly = False
        Else
          edAn_descr1.Properties.ReadOnly = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edAn_funzion_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAn_funzion.ValidatedAndChanged
    Dim dtrIn As DataRow = Nothing
    Try
      If dcSotc.Current Is Nothing Then Return

      dtrIn = CType(dcSotc.Current, DataRowView).Row
      '-------------------------------
      'imposto i flag di conto funzionamento
      If NTSCInt(edAn_funzion.Text) <> 0 Then
        GctlSetVisEnab(ckOpzgest1, False)
        GctlSetVisEnab(fmUsaContoFunz, False)

        If NTSCStr(dtrIn!an_opzgest) <> "" Then
          ckOpzgest1.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(0, 1) = "S", True, False))
          ckOpzgest2.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(1, 1) = "S", True, False))
          ckOpzgest3.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(2, 1) = "S", True, False))
          ckOpzgest4.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(3, 1) = "S", True, False))
          ckOpzgest5.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(4, 1) = "S", True, False))
          ckOpzgest6.Checked = CBool(IIf(dtrIn!an_opzgest.ToString.Substring(5, 1) = "S", True, False))
        Else
          ckOpzgest1.Checked = False
          ckOpzgest2.Checked = False
          ckOpzgest3.Checked = False
          ckOpzgest4.Checked = False
          ckOpzgest5.Checked = False
          ckOpzgest6.Checked = False
        End If
      Else
        fmUsaContoFunz.Enabled = False
        ckOpzgest1.Enabled = False

        ckOpzgest1.Checked = False
        ckOpzgest2.Checked = False
        ckOpzgest3.Checked = False
        ckOpzgest4.Checked = False
        ckOpzgest5.Checked = False
        ckOpzgest6.Checked = False
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckOpzgest1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest1.CheckedChanged
    Try
      If ckOpzgest1.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(1)
        cmdRiclassificazioni.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(1)
        GctlSetVisEnab(cmdRiclassificazioni, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckOpzgest2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest2.CheckedChanged
    Try
      If ckOpzgest2.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 1) & "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(2)
        fmIrap.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 1) & "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(2)
        GctlSetVisEnab(fmIrap, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckOpzgest3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest3.CheckedChanged
    Try
      If ckOpzgest3.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 2) & "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(3)
        fmImposte.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 2) & "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(3)
        GctlSetVisEnab(fmImposte, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckOpzgest4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest4.CheckedChanged
    Try
      If ckOpzgest4.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 3) & "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(4)
        fmRicavometro.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 3) & "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(4)
        GctlSetVisEnab(fmRicavometro, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckOpzgest5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest5.CheckedChanged
    Try
      If ckOpzgest5.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 4) & "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(5)
        fmStudi.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 4) & "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(5)
        GctlSetVisEnab(fmStudi, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckOpzgest6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpzgest6.CheckedChanged
    Try
      If ckOpzgest6.Checked Then
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 5) & "S" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(6)
        cbAn_azcom.Enabled = False
      Else
        If NTSCInt(edAn_funzion.Text) <> 0 Then CType(dcSotc.Current, DataRowView).Row!an_opzgest = NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(0, 5) & "N" & NTSCStr(CType(dcSotc.Current, DataRowView).Row!an_opzgest).Substring(6)
        GctlSetVisEnab(cbAn_azcom, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edAn_descr1_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAn_descr1.NTSZoomGest
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAn_controp_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAn_controp.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()
      If e.TipoEvento = "OPEN" Then bNuovo = False
      oTmp.Text = edAn_controp.Text

      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAS", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edAn_controp.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAn_funzion_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAn_funzion.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;"
      Else
        oPar.strParam = "NUOV;"
      End If
      oPar.strParam += IIf(oCleSotc.bPDCProfess, "Standard-PR", "Standard-AZ").ToString.PadRight(12) & ";" 'passo il piano dei conti
      oTmp.Text = edAn_funzion.Text
      oPar.strParam += oTmp.Text
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGPC", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edAn_funzion.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdRiclassificazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRiclassificazioni.Click
    Dim oPar As New CLE__CLDP
    Try
      If Not Salva() Then Return

      oPar.Ditta = DittaCorrente            'serve solo per BN__CLIE (anatric)
      oPar.strPar1 = oCleSotc.strPDC        'serve solo per BN__SOTC  (anptric)
      oPar.dPar1 = NTSCInt(lbAn_conto.Text)
      If NTSCInt(edAn_pcconto.Text) = 0 Then
        oPar.strNomProg = "BN__SOTC"
        'oPar.dPar2 = NTSCInt(edAn_pcconto.Text) 'sottoconto ditta
      Else
        oPar.strNomProg = "BN__ANPC"
      End If

      oMenu.RunChild("NTSInformatica", "FRMCGTRIC", "", DittaCorrente, "", "BNCGTRIC", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edAn_descr1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles edAn_descr1.Validating
    Try
      '----------------------------------------------
      'blocco tutto se il conto è collegato ad un pdc standard 
      'Select Case oCleSotc.strPDC.ToLower
      'Case "standard-az", "standard-pr", "standard-cna", "standard-asm", "standard-psm", "standard-xxx"
      'se il conto è collegato ad un conto PDC, blocco
      If NTSCInt(edAn_pcconto.Text) <> 0 And oCleSotc.bAllowOperation = False Then
        If ckAn_fldespdc.Checked Then edAn_descr1.Text = edAn_descr1.NTSOldValue
      End If
      'End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckAn_flci_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAn_flci.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckAn_flci.Checked Then
        GctlSetVisEnab(tlbAnalink, False)
        If oCleSotc.bsModSupCAE = False Then
          GctlSetVisEnab(tlbAnacent, False)
        Else
          GctlSetVisEnab(tlbAnacent2, False)
        End If
      Else
        tlbAnalink.Enabled = False
        If oCleSotc.bsModSupCAE = False Then
          tlbAnacent.Enabled = False
        Else
          tlbAnacent2.Enabled = False
        End If
      End If

      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim nPjob As Object

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{tabanaz.codditt} = " & CStrSQL(DittaCorrente) & _
        " And {anagra.codditt} = " & CStrSQL(DittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--SOTC", "Reports1", " ", 0, nDestin, "PAR0010.RPT", False, "PIANO DEI CONTI", False)
      '--------------------------------------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class

