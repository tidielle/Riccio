Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCONF_F
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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
  Public oCleConf As CLEORCONF
  Public dsConf As DataSet
  Public oCallParams As CLE__CLDP
  Public dcConf As BindingSource = New BindingSource

  Public dttCampi As New DataTable          'elenco campi filtrabili di TESTORD
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource

  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri

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
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPdf As NTSInformatica.NTSBarButtonItem
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents lbTipork As NTSInformatica.NTSLabel
  Public WithEvents lbZona As NTSInformatica.NTSLabel
  Public WithEvents edZona As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddestfin As NTSInformatica.NTSLabel
  Public WithEvents edCoddestfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddestini As NTSInformatica.NTSLabel
  Public WithEvents edCoddestini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRigafin As NTSInformatica.NTSLabel
  Public WithEvents lbRiga As NTSInformatica.NTSLabel
  Public WithEvents edRigafin As NTSInformatica.NTSTextBoxData
  Public WithEvents edRiga As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDataConsfin As NTSInformatica.NTSLabel
  Public WithEvents lbDataCons As NTSInformatica.NTSLabel
  Public WithEvents edDataConsfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataCons As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatordfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatordini As NTSInformatica.NTSLabel
  Public WithEvents edDatordfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatordini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbContoini As NTSInformatica.NTSLabel
  Public WithEvents lbNumordini As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edContoini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumordini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents lbNumordfin As NTSInformatica.NTSLabel
  Public WithEvents edNumordfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmStorico As NTSInformatica.NTSGroupBox
  Public WithEvents opStorico2 As NTSInformatica.NTSRadioButton
  Public WithEvents opStorico1 As NTSInformatica.NTSRadioButton
  Public WithEvents opStorico0 As NTSInformatica.NTSRadioButton
  Public WithEvents fmCliForn As NTSInformatica.NTSGroupBox
  Public WithEvents ckCliForn As NTSInformatica.NTSCheckBox
  Public WithEvents fmRiga As NTSInformatica.NTSGroupBox
  Public WithEvents opRiga2 As NTSInformatica.NTSRadioButton
  Public WithEvents opRiga1 As NTSInformatica.NTSRadioButton
  Public WithEvents opRiga0 As NTSInformatica.NTSRadioButton
  Public WithEvents fmStampati As NTSInformatica.NTSGroupBox
  Public WithEvents opStampati2 As NTSInformatica.NTSRadioButton
  Public WithEvents opStampati1 As NTSInformatica.NTSRadioButton
  Public WithEvents opStampati0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbContofin As NTSInformatica.NTSLabel
  Public WithEvents edContofin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesagen As NTSInformatica.NTSLabel
  Public WithEvents lbDescontofin As NTSInformatica.NTSLabel
  Public WithEvents lbDescontoini As NTSInformatica.NTSLabel
  Public WithEvents lbDescodcfam As NTSInformatica.NTSLabel
  Public WithEvents lbDeszona As NTSInformatica.NTSLabel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents pnLeft As NTSInformatica.NTSPanel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCONF_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
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
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.edNumordini = New NTSInformatica.NTSTextBoxNum
    Me.edContoini = New NTSInformatica.NTSTextBoxNum
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbNumordini = New NTSInformatica.NTSLabel
    Me.lbContoini = New NTSInformatica.NTSLabel
    Me.edDatordini = New NTSInformatica.NTSTextBoxData
    Me.edDatordfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatordini = New NTSInformatica.NTSLabel
    Me.lbDatordfin = New NTSInformatica.NTSLabel
    Me.lbDataConsfin = New NTSInformatica.NTSLabel
    Me.lbDataCons = New NTSInformatica.NTSLabel
    Me.edDataConsfin = New NTSInformatica.NTSTextBoxData
    Me.edDataCons = New NTSInformatica.NTSTextBoxData
    Me.lbRigafin = New NTSInformatica.NTSLabel
    Me.lbRiga = New NTSInformatica.NTSLabel
    Me.edRigafin = New NTSInformatica.NTSTextBoxData
    Me.edRiga = New NTSInformatica.NTSTextBoxData
    Me.lbCoddestfin = New NTSInformatica.NTSLabel
    Me.edCoddestfin = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddestini = New NTSInformatica.NTSLabel
    Me.edCoddestini = New NTSInformatica.NTSTextBoxNum
    Me.lbCodagen = New NTSInformatica.NTSLabel
    Me.edCodagen = New NTSInformatica.NTSTextBoxNum
    Me.lbZona = New NTSInformatica.NTSLabel
    Me.edZona = New NTSInformatica.NTSTextBoxNum
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbNumordfin = New NTSInformatica.NTSLabel
    Me.edNumordfin = New NTSInformatica.NTSTextBoxNum
    Me.fmStorico = New NTSInformatica.NTSGroupBox
    Me.opStorico2 = New NTSInformatica.NTSRadioButton
    Me.opStorico1 = New NTSInformatica.NTSRadioButton
    Me.opStorico0 = New NTSInformatica.NTSRadioButton
    Me.fmStampati = New NTSInformatica.NTSGroupBox
    Me.opStampati2 = New NTSInformatica.NTSRadioButton
    Me.opStampati1 = New NTSInformatica.NTSRadioButton
    Me.opStampati0 = New NTSInformatica.NTSRadioButton
    Me.fmRiga = New NTSInformatica.NTSGroupBox
    Me.opRiga2 = New NTSInformatica.NTSRadioButton
    Me.opRiga1 = New NTSInformatica.NTSRadioButton
    Me.opRiga0 = New NTSInformatica.NTSRadioButton
    Me.fmCliForn = New NTSInformatica.NTSGroupBox
    Me.ckCliForn = New NTSInformatica.NTSCheckBox
    Me.lbContofin = New NTSInformatica.NTSLabel
    Me.edContofin = New NTSInformatica.NTSTextBoxNum
    Me.lbDesagen = New NTSInformatica.NTSLabel
    Me.lbDeszona = New NTSInformatica.NTSLabel
    Me.lbDescodcfam = New NTSInformatica.NTSLabel
    Me.lbDescontoini = New NTSInformatica.NTSLabel
    Me.lbDescontofin = New NTSInformatica.NTSLabel
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.pnRight = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataConsfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataCons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRigafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRiga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddestfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddestini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStorico, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStorico.SuspendLayout()
    CType(Me.opStorico2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStorico1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStorico0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStampati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStampati.SuspendLayout()
    CType(Me.opStampati2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStampati1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStampati0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRiga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRiga.SuspendLayout()
    CType(Me.opRiga2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRiga1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRiga0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCliForn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCliForn.SuspendLayout()
    CType(Me.ckCliForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaWord, Me.tlbStampaPdf})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.Id = 26
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "StampaPDF"
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
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Location = New System.Drawing.Point(10, 13)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(27, 13)
    Me.lbTipork.TabIndex = 4
    Me.lbTipork.Text = "Tipo"
    Me.lbTipork.Tooltip = ""
    Me.lbTipork.UseMnemonic = False
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(140, 10)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(199, 20)
    Me.cbTipork.TabIndex = 5
    Me.cbTipork.ValueMember = ""
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(140, 32)
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
    Me.edAnno.Size = New System.Drawing.Size(64, 20)
    Me.edAnno.TabIndex = 6
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodcfam.Location = New System.Drawing.Point(140, 252)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(64, 20)
    Me.edCodcfam.TabIndex = 7
    '
    'edNumordini
    '
    Me.edNumordini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordini.Location = New System.Drawing.Point(140, 54)
    Me.edNumordini.Name = "edNumordini"
    Me.edNumordini.NTSDbField = ""
    Me.edNumordini.NTSFormat = "0"
    Me.edNumordini.NTSForzaVisZoom = False
    Me.edNumordini.NTSOldValue = ""
    Me.edNumordini.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordini.Properties.MaxLength = 65536
    Me.edNumordini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordini.Size = New System.Drawing.Size(85, 20)
    Me.edNumordini.TabIndex = 8
    '
    'edContoini
    '
    Me.edContoini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoini.Location = New System.Drawing.Point(140, 76)
    Me.edContoini.Name = "edContoini"
    Me.edContoini.NTSDbField = ""
    Me.edContoini.NTSFormat = "0"
    Me.edContoini.NTSForzaVisZoom = False
    Me.edContoini.NTSOldValue = ""
    Me.edContoini.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoini.Properties.MaxLength = 65536
    Me.edContoini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoini.Size = New System.Drawing.Size(85, 20)
    Me.edContoini.TabIndex = 9
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(10, 35)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(97, 13)
    Me.lbAnno.TabIndex = 11
    Me.lbAnno.Text = "Anno (0=qualsiasi)"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbNumordini
    '
    Me.lbNumordini.AutoSize = True
    Me.lbNumordini.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordini.Location = New System.Drawing.Point(10, 57)
    Me.lbNumordini.Name = "lbNumordini"
    Me.lbNumordini.NTSDbField = ""
    Me.lbNumordini.Size = New System.Drawing.Size(92, 13)
    Me.lbNumordini.TabIndex = 12
    Me.lbNumordini.Text = "Da numero ordine"
    Me.lbNumordini.Tooltip = ""
    Me.lbNumordini.UseMnemonic = False
    '
    'lbContoini
    '
    Me.lbContoini.AutoSize = True
    Me.lbContoini.BackColor = System.Drawing.Color.Transparent
    Me.lbContoini.Location = New System.Drawing.Point(10, 79)
    Me.lbContoini.Name = "lbContoini"
    Me.lbContoini.NTSDbField = ""
    Me.lbContoini.Size = New System.Drawing.Size(50, 13)
    Me.lbContoini.TabIndex = 13
    Me.lbContoini.Text = "Da conto"
    Me.lbContoini.Tooltip = ""
    Me.lbContoini.UseMnemonic = False
    '
    'edDatordini
    '
    Me.edDatordini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordini.Location = New System.Drawing.Point(140, 120)
    Me.edDatordini.Name = "edDatordini"
    Me.edDatordini.NTSDbField = ""
    Me.edDatordini.NTSForzaVisZoom = False
    Me.edDatordini.NTSOldValue = ""
    Me.edDatordini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordini.Properties.MaxLength = 65536
    Me.edDatordini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordini.Size = New System.Drawing.Size(85, 20)
    Me.edDatordini.TabIndex = 17
    '
    'edDatordfin
    '
    Me.edDatordfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordfin.Location = New System.Drawing.Point(377, 120)
    Me.edDatordfin.Name = "edDatordfin"
    Me.edDatordfin.NTSDbField = ""
    Me.edDatordfin.NTSForzaVisZoom = False
    Me.edDatordfin.NTSOldValue = ""
    Me.edDatordfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordfin.Properties.MaxLength = 65536
    Me.edDatordfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordfin.Size = New System.Drawing.Size(85, 20)
    Me.edDatordfin.TabIndex = 18
    '
    'lbDatordini
    '
    Me.lbDatordini.AutoSize = True
    Me.lbDatordini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordini.Location = New System.Drawing.Point(10, 123)
    Me.lbDatordini.Name = "lbDatordini"
    Me.lbDatordini.NTSDbField = ""
    Me.lbDatordini.Size = New System.Drawing.Size(78, 13)
    Me.lbDatordini.TabIndex = 19
    Me.lbDatordini.Text = "Da data ordine"
    Me.lbDatordini.Tooltip = ""
    Me.lbDatordini.UseMnemonic = False
    '
    'lbDatordfin
    '
    Me.lbDatordfin.AutoSize = True
    Me.lbDatordfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordfin.Location = New System.Drawing.Point(237, 123)
    Me.lbDatordfin.Name = "lbDatordfin"
    Me.lbDatordfin.NTSDbField = ""
    Me.lbDatordfin.Size = New System.Drawing.Size(72, 13)
    Me.lbDatordfin.TabIndex = 20
    Me.lbDatordfin.Text = "A data ordine"
    Me.lbDatordfin.Tooltip = ""
    Me.lbDatordfin.UseMnemonic = False
    '
    'lbDataConsfin
    '
    Me.lbDataConsfin.AutoSize = True
    Me.lbDataConsfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDataConsfin.Location = New System.Drawing.Point(237, 145)
    Me.lbDataConsfin.Name = "lbDataConsfin"
    Me.lbDataConsfin.NTSDbField = ""
    Me.lbDataConsfin.Size = New System.Drawing.Size(121, 13)
    Me.lbDataConsfin.TabIndex = 24
    Me.lbDataConsfin.Text = "A data consegna ordine"
    Me.lbDataConsfin.Tooltip = ""
    Me.lbDataConsfin.UseMnemonic = False
    '
    'lbDataCons
    '
    Me.lbDataCons.AutoSize = True
    Me.lbDataCons.BackColor = System.Drawing.Color.Transparent
    Me.lbDataCons.Location = New System.Drawing.Point(10, 145)
    Me.lbDataCons.Name = "lbDataCons"
    Me.lbDataCons.NTSDbField = ""
    Me.lbDataCons.Size = New System.Drawing.Size(127, 13)
    Me.lbDataCons.TabIndex = 23
    Me.lbDataCons.Text = "Da data consegna ordine"
    Me.lbDataCons.Tooltip = ""
    Me.lbDataCons.UseMnemonic = False
    '
    'edDataConsfin
    '
    Me.edDataConsfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataConsfin.Location = New System.Drawing.Point(377, 142)
    Me.edDataConsfin.Name = "edDataConsfin"
    Me.edDataConsfin.NTSDbField = ""
    Me.edDataConsfin.NTSForzaVisZoom = False
    Me.edDataConsfin.NTSOldValue = ""
    Me.edDataConsfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataConsfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataConsfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataConsfin.Properties.MaxLength = 65536
    Me.edDataConsfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataConsfin.Size = New System.Drawing.Size(85, 20)
    Me.edDataConsfin.TabIndex = 22
    '
    'edDataCons
    '
    Me.edDataCons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataCons.Location = New System.Drawing.Point(140, 142)
    Me.edDataCons.Name = "edDataCons"
    Me.edDataCons.NTSDbField = ""
    Me.edDataCons.NTSForzaVisZoom = False
    Me.edDataCons.NTSOldValue = ""
    Me.edDataCons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataCons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataCons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataCons.Properties.MaxLength = 65536
    Me.edDataCons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataCons.Size = New System.Drawing.Size(85, 20)
    Me.edDataCons.TabIndex = 21
    '
    'lbRigafin
    '
    Me.lbRigafin.AutoSize = True
    Me.lbRigafin.BackColor = System.Drawing.Color.Transparent
    Me.lbRigafin.Location = New System.Drawing.Point(237, 167)
    Me.lbRigafin.Name = "lbRigafin"
    Me.lbRigafin.NTSDbField = ""
    Me.lbRigafin.Size = New System.Drawing.Size(109, 13)
    Me.lbRigafin.TabIndex = 28
    Me.lbRigafin.Text = "A data consegna riga"
    Me.lbRigafin.Tooltip = ""
    Me.lbRigafin.UseMnemonic = False
    '
    'lbRiga
    '
    Me.lbRiga.AutoSize = True
    Me.lbRiga.BackColor = System.Drawing.Color.Transparent
    Me.lbRiga.Location = New System.Drawing.Point(10, 167)
    Me.lbRiga.Name = "lbRiga"
    Me.lbRiga.NTSDbField = ""
    Me.lbRiga.Size = New System.Drawing.Size(115, 13)
    Me.lbRiga.TabIndex = 27
    Me.lbRiga.Text = "Da data consegna riga"
    Me.lbRiga.Tooltip = ""
    Me.lbRiga.UseMnemonic = False
    '
    'edRigafin
    '
    Me.edRigafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRigafin.Location = New System.Drawing.Point(377, 164)
    Me.edRigafin.Name = "edRigafin"
    Me.edRigafin.NTSDbField = ""
    Me.edRigafin.NTSForzaVisZoom = False
    Me.edRigafin.NTSOldValue = ""
    Me.edRigafin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRigafin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRigafin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRigafin.Properties.MaxLength = 65536
    Me.edRigafin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRigafin.Size = New System.Drawing.Size(85, 20)
    Me.edRigafin.TabIndex = 26
    '
    'edRiga
    '
    Me.edRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRiga.Location = New System.Drawing.Point(140, 164)
    Me.edRiga.Name = "edRiga"
    Me.edRiga.NTSDbField = ""
    Me.edRiga.NTSForzaVisZoom = False
    Me.edRiga.NTSOldValue = ""
    Me.edRiga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRiga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRiga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRiga.Properties.MaxLength = 65536
    Me.edRiga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRiga.Size = New System.Drawing.Size(85, 20)
    Me.edRiga.TabIndex = 25
    '
    'lbCoddestfin
    '
    Me.lbCoddestfin.AutoSize = True
    Me.lbCoddestfin.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddestfin.Location = New System.Drawing.Point(237, 189)
    Me.lbCoddestfin.Name = "lbCoddestfin"
    Me.lbCoddestfin.NTSDbField = ""
    Me.lbCoddestfin.Size = New System.Drawing.Size(77, 13)
    Me.lbCoddestfin.TabIndex = 32
    Me.lbCoddestfin.Text = "A destinazione"
    Me.lbCoddestfin.Tooltip = ""
    Me.lbCoddestfin.UseMnemonic = False
    '
    'edCoddestfin
    '
    Me.edCoddestfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddestfin.Location = New System.Drawing.Point(377, 186)
    Me.edCoddestfin.Name = "edCoddestfin"
    Me.edCoddestfin.NTSDbField = ""
    Me.edCoddestfin.NTSFormat = "0"
    Me.edCoddestfin.NTSForzaVisZoom = False
    Me.edCoddestfin.NTSOldValue = ""
    Me.edCoddestfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddestfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddestfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddestfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddestfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddestfin.Properties.MaxLength = 65536
    Me.edCoddestfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddestfin.Size = New System.Drawing.Size(85, 20)
    Me.edCoddestfin.TabIndex = 31
    '
    'lbCoddestini
    '
    Me.lbCoddestini.AutoSize = True
    Me.lbCoddestini.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddestini.Location = New System.Drawing.Point(10, 189)
    Me.lbCoddestini.Name = "lbCoddestini"
    Me.lbCoddestini.NTSDbField = ""
    Me.lbCoddestini.Size = New System.Drawing.Size(83, 13)
    Me.lbCoddestini.TabIndex = 30
    Me.lbCoddestini.Text = "Da destinazione"
    Me.lbCoddestini.Tooltip = ""
    Me.lbCoddestini.UseMnemonic = False
    '
    'edCoddestini
    '
    Me.edCoddestini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddestini.Location = New System.Drawing.Point(140, 186)
    Me.edCoddestini.Name = "edCoddestini"
    Me.edCoddestini.NTSDbField = ""
    Me.edCoddestini.NTSFormat = "0"
    Me.edCoddestini.NTSForzaVisZoom = False
    Me.edCoddestini.NTSOldValue = ""
    Me.edCoddestini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddestini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddestini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddestini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddestini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddestini.Properties.MaxLength = 65536
    Me.edCoddestini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddestini.Size = New System.Drawing.Size(85, 20)
    Me.edCoddestini.TabIndex = 29
    '
    'lbCodagen
    '
    Me.lbCodagen.AutoSize = True
    Me.lbCodagen.BackColor = System.Drawing.Color.Transparent
    Me.lbCodagen.Location = New System.Drawing.Point(10, 212)
    Me.lbCodagen.Name = "lbCodagen"
    Me.lbCodagen.NTSDbField = ""
    Me.lbCodagen.Size = New System.Drawing.Size(102, 13)
    Me.lbCodagen.TabIndex = 34
    Me.lbCodagen.Text = "Agente    (0 = tutti)"
    Me.lbCodagen.Tooltip = ""
    Me.lbCodagen.UseMnemonic = False
    '
    'edCodagen
    '
    Me.edCodagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagen.Location = New System.Drawing.Point(140, 208)
    Me.edCodagen.Name = "edCodagen"
    Me.edCodagen.NTSDbField = ""
    Me.edCodagen.NTSFormat = "0"
    Me.edCodagen.NTSForzaVisZoom = False
    Me.edCodagen.NTSOldValue = ""
    Me.edCodagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagen.Properties.MaxLength = 65536
    Me.edCodagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagen.Size = New System.Drawing.Size(64, 20)
    Me.edCodagen.TabIndex = 33
    '
    'lbZona
    '
    Me.lbZona.AutoSize = True
    Me.lbZona.BackColor = System.Drawing.Color.Transparent
    Me.lbZona.Location = New System.Drawing.Point(12, 233)
    Me.lbZona.Name = "lbZona"
    Me.lbZona.NTSDbField = ""
    Me.lbZona.Size = New System.Drawing.Size(104, 13)
    Me.lbZona.TabIndex = 36
    Me.lbZona.Text = "Zona       (0 = tutte)"
    Me.lbZona.Tooltip = ""
    Me.lbZona.UseMnemonic = False
    '
    'edZona
    '
    Me.edZona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZona.Location = New System.Drawing.Point(140, 230)
    Me.edZona.Name = "edZona"
    Me.edZona.NTSDbField = ""
    Me.edZona.NTSFormat = "0"
    Me.edZona.NTSForzaVisZoom = False
    Me.edZona.NTSOldValue = ""
    Me.edZona.Properties.Appearance.Options.UseTextOptions = True
    Me.edZona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZona.Properties.MaxLength = 65536
    Me.edZona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZona.Size = New System.Drawing.Size(64, 20)
    Me.edZona.TabIndex = 35
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(10, 256)
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
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edSerie.Location = New System.Drawing.Point(377, 32)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbNumordfin.Location = New System.Drawing.Point(237, 57)
    Me.lbNumordfin.Name = "lbNumordfin"
    Me.lbNumordfin.NTSDbField = ""
    Me.lbNumordfin.Size = New System.Drawing.Size(86, 13)
    Me.lbNumordfin.TabIndex = 42
    Me.lbNumordfin.Text = "A numero ordine"
    Me.lbNumordfin.Tooltip = ""
    Me.lbNumordfin.UseMnemonic = False
    '
    'edNumordfin
    '
    Me.edNumordfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordfin.Location = New System.Drawing.Point(377, 54)
    Me.edNumordfin.Name = "edNumordfin"
    Me.edNumordfin.NTSDbField = ""
    Me.edNumordfin.NTSFormat = "0"
    Me.edNumordfin.NTSForzaVisZoom = False
    Me.edNumordfin.NTSOldValue = ""
    Me.edNumordfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordfin.Properties.MaxLength = 65536
    Me.edNumordfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordfin.Size = New System.Drawing.Size(85, 20)
    Me.edNumordfin.TabIndex = 41
    '
    'fmStorico
    '
    Me.fmStorico.AllowDrop = True
    Me.fmStorico.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStorico.Appearance.Options.UseBackColor = True
    Me.fmStorico.Controls.Add(Me.opStorico2)
    Me.fmStorico.Controls.Add(Me.opStorico1)
    Me.fmStorico.Controls.Add(Me.opStorico0)
    Me.fmStorico.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStorico.Location = New System.Drawing.Point(7, 10)
    Me.fmStorico.Name = "fmStorico"
    Me.fmStorico.ShowCaption = False
    Me.fmStorico.Size = New System.Drawing.Size(200, 68)
    Me.fmStorico.TabIndex = 43
    '
    'opStorico2
    '
    Me.opStorico2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico2.Location = New System.Drawing.Point(9, 45)
    Me.opStorico2.Name = "opStorico2"
    Me.opStorico2.NTSCheckValue = "S"
    Me.opStorico2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico2.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico2.Properties.Caption = "&Entrambi"
    Me.opStorico2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico2.Size = New System.Drawing.Size(111, 19)
    Me.opStorico2.TabIndex = 2
    '
    'opStorico1
    '
    Me.opStorico1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico1.Location = New System.Drawing.Point(9, 25)
    Me.opStorico1.Name = "opStorico1"
    Me.opStorico1.NTSCheckValue = "S"
    Me.opStorico1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico1.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico1.Properties.Caption = "Ordini in &storico"
    Me.opStorico1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico1.Size = New System.Drawing.Size(111, 19)
    Me.opStorico1.TabIndex = 1
    '
    'opStorico0
    '
    Me.opStorico0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico0.EditValue = True
    Me.opStorico0.Location = New System.Drawing.Point(9, 5)
    Me.opStorico0.Name = "opStorico0"
    Me.opStorico0.NTSCheckValue = "S"
    Me.opStorico0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico0.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico0.Properties.Caption = "&Ordini in essere"
    Me.opStorico0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico0.Size = New System.Drawing.Size(111, 19)
    Me.opStorico0.TabIndex = 0
    '
    'fmStampati
    '
    Me.fmStampati.AllowDrop = True
    Me.fmStampati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStampati.Appearance.Options.UseBackColor = True
    Me.fmStampati.Controls.Add(Me.opStampati2)
    Me.fmStampati.Controls.Add(Me.opStampati1)
    Me.fmStampati.Controls.Add(Me.opStampati0)
    Me.fmStampati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStampati.Location = New System.Drawing.Point(7, 84)
    Me.fmStampati.Name = "fmStampati"
    Me.fmStampati.ShowCaption = False
    Me.fmStampati.Size = New System.Drawing.Size(200, 68)
    Me.fmStampati.TabIndex = 44
    '
    'opStampati2
    '
    Me.opStampati2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati2.Location = New System.Drawing.Point(9, 45)
    Me.opStampati2.Name = "opStampati2"
    Me.opStampati2.NTSCheckValue = "S"
    Me.opStampati2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati2.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati2.Properties.Caption = "Entra&mbi"
    Me.opStampati2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati2.Size = New System.Drawing.Size(111, 19)
    Me.opStampati2.TabIndex = 2
    '
    'opStampati1
    '
    Me.opStampati1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati1.Location = New System.Drawing.Point(9, 25)
    Me.opStampati1.Name = "opStampati1"
    Me.opStampati1.NTSCheckValue = "S"
    Me.opStampati1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati1.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati1.Properties.Caption = "Solo &già stampati"
    Me.opStampati1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati1.Size = New System.Drawing.Size(133, 19)
    Me.opStampati1.TabIndex = 1
    '
    'opStampati0
    '
    Me.opStampati0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati0.EditValue = True
    Me.opStampati0.Location = New System.Drawing.Point(9, 5)
    Me.opStampati0.Name = "opStampati0"
    Me.opStampati0.NTSCheckValue = "S"
    Me.opStampati0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati0.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati0.Properties.Caption = "So&lo non stampati"
    Me.opStampati0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati0.Size = New System.Drawing.Size(133, 19)
    Me.opStampati0.TabIndex = 0
    '
    'fmRiga
    '
    Me.fmRiga.AllowDrop = True
    Me.fmRiga.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRiga.Appearance.Options.UseBackColor = True
    Me.fmRiga.Controls.Add(Me.opRiga2)
    Me.fmRiga.Controls.Add(Me.opRiga1)
    Me.fmRiga.Controls.Add(Me.opRiga0)
    Me.fmRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRiga.Location = New System.Drawing.Point(7, 158)
    Me.fmRiga.Name = "fmRiga"
    Me.fmRiga.ShowCaption = False
    Me.fmRiga.Size = New System.Drawing.Size(200, 68)
    Me.fmRiga.TabIndex = 45
    '
    'opRiga2
    '
    Me.opRiga2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRiga2.EditValue = True
    Me.opRiga2.Location = New System.Drawing.Point(9, 45)
    Me.opRiga2.Name = "opRiga2"
    Me.opRiga2.NTSCheckValue = "S"
    Me.opRiga2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga2.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga2.Properties.Caption = "Entram&be"
    Me.opRiga2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga2.Size = New System.Drawing.Size(111, 19)
    Me.opRiga2.TabIndex = 2
    '
    'opRiga1
    '
    Me.opRiga1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRiga1.Location = New System.Drawing.Point(9, 25)
    Me.opRiga1.Name = "opRiga1"
    Me.opRiga1.NTSCheckValue = "S"
    Me.opRiga1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga1.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga1.Properties.Caption = "Rig&he saldate"
    Me.opRiga1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga1.Size = New System.Drawing.Size(111, 19)
    Me.opRiga1.TabIndex = 1
    '
    'opRiga0
    '
    Me.opRiga0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRiga0.Location = New System.Drawing.Point(9, 5)
    Me.opRiga0.Name = "opRiga0"
    Me.opRiga0.NTSCheckValue = "S"
    Me.opRiga0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga0.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga0.Properties.Caption = "&Righe aperte"
    Me.opRiga0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga0.Size = New System.Drawing.Size(111, 19)
    Me.opRiga0.TabIndex = 0
    '
    'fmCliForn
    '
    Me.fmCliForn.AllowDrop = True
    Me.fmCliForn.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCliForn.Appearance.Options.UseBackColor = True
    Me.fmCliForn.Controls.Add(Me.ckCliForn)
    Me.fmCliForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCliForn.Location = New System.Drawing.Point(7, 230)
    Me.fmCliForn.Name = "fmCliForn"
    Me.fmCliForn.ShowCaption = False
    Me.fmCliForn.Size = New System.Drawing.Size(200, 42)
    Me.fmCliForn.TabIndex = 46
    '
    'ckCliForn
    '
    Me.ckCliForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCliForn.Location = New System.Drawing.Point(9, 11)
    Me.ckCliForn.Name = "ckCliForn"
    Me.ckCliForn.NTSCheckValue = "S"
    Me.ckCliForn.NTSUnCheckValue = "N"
    Me.ckCliForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCliForn.Properties.Appearance.Options.UseBackColor = True
    Me.ckCliForn.Properties.Caption = "Raggruppa per cliente/fornitore"
    Me.ckCliForn.Size = New System.Drawing.Size(191, 19)
    Me.ckCliForn.TabIndex = 0
    '
    'lbContofin
    '
    Me.lbContofin.AutoSize = True
    Me.lbContofin.BackColor = System.Drawing.Color.Transparent
    Me.lbContofin.Location = New System.Drawing.Point(10, 101)
    Me.lbContofin.Name = "lbContofin"
    Me.lbContofin.NTSDbField = ""
    Me.lbContofin.Size = New System.Drawing.Size(44, 13)
    Me.lbContofin.TabIndex = 48
    Me.lbContofin.Text = "A conto"
    Me.lbContofin.Tooltip = ""
    Me.lbContofin.UseMnemonic = False
    '
    'edContofin
    '
    Me.edContofin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContofin.Location = New System.Drawing.Point(140, 98)
    Me.edContofin.Name = "edContofin"
    Me.edContofin.NTSDbField = ""
    Me.edContofin.NTSFormat = "0"
    Me.edContofin.NTSForzaVisZoom = False
    Me.edContofin.NTSOldValue = ""
    Me.edContofin.Properties.Appearance.Options.UseTextOptions = True
    Me.edContofin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContofin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContofin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContofin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContofin.Properties.MaxLength = 65536
    Me.edContofin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContofin.Size = New System.Drawing.Size(85, 20)
    Me.edContofin.TabIndex = 47
    '
    'lbDesagen
    '
    Me.lbDesagen.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagen.Location = New System.Drawing.Point(210, 208)
    Me.lbDesagen.Name = "lbDesagen"
    Me.lbDesagen.NTSDbField = ""
    Me.lbDesagen.Size = New System.Drawing.Size(252, 20)
    Me.lbDesagen.TabIndex = 49
    Me.lbDesagen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesagen.Tooltip = ""
    Me.lbDesagen.UseMnemonic = False
    '
    'lbDeszona
    '
    Me.lbDeszona.BackColor = System.Drawing.Color.Transparent
    Me.lbDeszona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDeszona.Location = New System.Drawing.Point(210, 230)
    Me.lbDeszona.Name = "lbDeszona"
    Me.lbDeszona.NTSDbField = ""
    Me.lbDeszona.Size = New System.Drawing.Size(252, 20)
    Me.lbDeszona.TabIndex = 50
    Me.lbDeszona.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDeszona.Tooltip = ""
    Me.lbDeszona.UseMnemonic = False
    '
    'lbDescodcfam
    '
    Me.lbDescodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodcfam.Location = New System.Drawing.Point(210, 252)
    Me.lbDescodcfam.Name = "lbDescodcfam"
    Me.lbDescodcfam.NTSDbField = ""
    Me.lbDescodcfam.Size = New System.Drawing.Size(252, 20)
    Me.lbDescodcfam.TabIndex = 51
    Me.lbDescodcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodcfam.Tooltip = ""
    Me.lbDescodcfam.UseMnemonic = False
    '
    'lbDescontoini
    '
    Me.lbDescontoini.BackColor = System.Drawing.Color.Transparent
    Me.lbDescontoini.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescontoini.Location = New System.Drawing.Point(231, 77)
    Me.lbDescontoini.Name = "lbDescontoini"
    Me.lbDescontoini.NTSDbField = ""
    Me.lbDescontoini.Size = New System.Drawing.Size(231, 20)
    Me.lbDescontoini.TabIndex = 52
    Me.lbDescontoini.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescontoini.Tooltip = ""
    Me.lbDescontoini.UseMnemonic = False
    '
    'lbDescontofin
    '
    Me.lbDescontofin.BackColor = System.Drawing.Color.Transparent
    Me.lbDescontofin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescontofin.Location = New System.Drawing.Point(231, 98)
    Me.lbDescontofin.Name = "lbDescontofin"
    Me.lbDescontofin.NTSDbField = ""
    Me.lbDescontofin.Size = New System.Drawing.Size(231, 20)
    Me.lbDescontofin.TabIndex = 53
    Me.lbDescontofin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescontofin.Tooltip = ""
    Me.lbDescontofin.UseMnemonic = False
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.ckSerie)
    Me.pnLeft.Controls.Add(Me.lbTipork)
    Me.pnLeft.Controls.Add(Me.lbDescontofin)
    Me.pnLeft.Controls.Add(Me.cbTipork)
    Me.pnLeft.Controls.Add(Me.lbDescontoini)
    Me.pnLeft.Controls.Add(Me.edAnno)
    Me.pnLeft.Controls.Add(Me.lbDescodcfam)
    Me.pnLeft.Controls.Add(Me.edCodcfam)
    Me.pnLeft.Controls.Add(Me.lbDeszona)
    Me.pnLeft.Controls.Add(Me.edNumordini)
    Me.pnLeft.Controls.Add(Me.lbDesagen)
    Me.pnLeft.Controls.Add(Me.edContoini)
    Me.pnLeft.Controls.Add(Me.lbContofin)
    Me.pnLeft.Controls.Add(Me.edContofin)
    Me.pnLeft.Controls.Add(Me.lbAnno)
    Me.pnLeft.Controls.Add(Me.lbNumordini)
    Me.pnLeft.Controls.Add(Me.lbContoini)
    Me.pnLeft.Controls.Add(Me.lbNumordfin)
    Me.pnLeft.Controls.Add(Me.edNumordfin)
    Me.pnLeft.Controls.Add(Me.edDatordini)
    Me.pnLeft.Controls.Add(Me.edDatordfin)
    Me.pnLeft.Controls.Add(Me.edSerie)
    Me.pnLeft.Controls.Add(Me.lbDatordini)
    Me.pnLeft.Controls.Add(Me.lbCodcfam)
    Me.pnLeft.Controls.Add(Me.lbDatordfin)
    Me.pnLeft.Controls.Add(Me.edDataCons)
    Me.pnLeft.Controls.Add(Me.lbZona)
    Me.pnLeft.Controls.Add(Me.edDataConsfin)
    Me.pnLeft.Controls.Add(Me.edZona)
    Me.pnLeft.Controls.Add(Me.lbDataCons)
    Me.pnLeft.Controls.Add(Me.lbCodagen)
    Me.pnLeft.Controls.Add(Me.lbDataConsfin)
    Me.pnLeft.Controls.Add(Me.edCodagen)
    Me.pnLeft.Controls.Add(Me.edRiga)
    Me.pnLeft.Controls.Add(Me.lbCoddestfin)
    Me.pnLeft.Controls.Add(Me.edRigafin)
    Me.pnLeft.Controls.Add(Me.edCoddestfin)
    Me.pnLeft.Controls.Add(Me.lbRiga)
    Me.pnLeft.Controls.Add(Me.lbCoddestini)
    Me.pnLeft.Controls.Add(Me.lbRigafin)
    Me.pnLeft.Controls.Add(Me.edCoddestini)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Location = New System.Drawing.Point(0, 39)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(467, 281)
    Me.pnLeft.TabIndex = 54
    Me.pnLeft.Text = "NtsPanel1"
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(240, 33)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(63, 19)
    Me.ckSerie.TabIndex = 54
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.fmStorico)
    Me.pnRight.Controls.Add(Me.fmStampati)
    Me.pnRight.Controls.Add(Me.fmCliForn)
    Me.pnRight.Controls.Add(Me.fmRiga)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Location = New System.Drawing.Point(473, 39)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(219, 281)
    Me.pnRight.TabIndex = 55
    Me.pnRight.Text = "NtsPanel1"
    '
    'FRMORCONF_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(692, 321)
    Me.Controls.Add(Me.pnLeft)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMORCONF_F"
    Me.Text = "STAMPA ORDINI -- CONFERME D'ORDINE -- PREVENTIVI -- IMPEGNI DI TRASFERIMENTO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataConsfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataCons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRigafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRiga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddestfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddestini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStorico, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStorico.ResumeLayout(False)
    Me.fmStorico.PerformLayout()
    CType(Me.opStorico2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStorico1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStorico0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStampati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStampati.ResumeLayout(False)
    Me.fmStampati.PerformLayout()
    CType(Me.opStampati2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStampati1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStampati0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRiga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRiga.ResumeLayout(False)
    Me.fmRiga.PerformLayout()
    CType(Me.opRiga2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRiga1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRiga0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCliForn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCliForn.ResumeLayout(False)
    Me.fmCliForn.PerformLayout()
    CType(Me.ckCliForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORCONF", "BEORCONF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128534975466065282, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleConf = CType(oTmp, CLEORCONF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORCONF", strRemoteServer, strRemotePort)
    AddHandler oCleConf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleConf.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli

      edContofin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054423785585, "A conto"), tabanagra)
      ckCliForn.NTSSetParam(oMenu, oApp.Tr(Me, 128535054423941796, "Raggruppa per cliente/fornitore"), "S", "N")
      opRiga2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424098007, "Entram&be"), "S")
      opRiga1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424254218, "Rig&he saldate"), "S")
      opRiga0.NTSSetParam(oMenu, oApp.Tr(Me, 128823856330773934, "&Righe aperte"), "S")
      opStampati2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424410429, "Entra&mbi"), "S")
      opStampati1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424566640, "Solo &già stampati"), "S")
      opStampati0.NTSSetParam(oMenu, oApp.Tr(Me, 128823856436712790, "So&lo non stampati"), "S")
      opStorico2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424722851, "&Entrambi"), "S")
      opStorico1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424879062, "Ordini in &storico"), "S")
      opStorico0.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425035273, "&Ordini in essere"), "S")
      edNumordfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425191484, "A numero ordine"), "0", 9, 0, 999999999)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425503906, "Serie"), CLN__STD.SerieMaxLen, True)
      edZona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054426753594, "Zona"), tabzone)
      edCodagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427066016, "Agente"), tabcage)
      edCoddestfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427378438, "A destinazione"), tabdestdiv)
      edCoddestfin.CliePerDestdiv = edContoini
      edCoddestini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427690860, "Da destinazione"), tabdestdiv)
      edCoddestini.CliePerDestdiv = edContoini
      edRigafin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428159493, "A data cons. riga"), False)
      edRiga.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428315704, "Da data cons. riga"), False)
      edDataConsfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428784337, "A data cons. ordine"), False)
      edDataCons.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428940548, "Da data cons. ordine"), False)
      edDatordfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429409181, "A data ordine"), False)
      edDatordini.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429565392, "Da data ordine"), False)
      edContoini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430815080, "Da conto"), tabanagra)
      edNumordini.NTSSetParam(oMenu, oApp.Tr(Me, 128535054430971291, "Da numero ordine"), "0", 9, 0, 999999999)
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054431127502, "Linea/famiglia"), tabcfam, True)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128535054431283713, "Anno"), "0", 4, 0, 2099)
      cbTipork.NTSSetParam(oApp.Tr(Me, 128535054431439924, "Tipo"))
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")

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
    Try
      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))


      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipork.AcceptChanges()

      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"
      cbTipork.SelectedValue = "Q"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMORCONF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim lPunto As Integer
    Dim strPath As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Test per controllare l'esistenza o meno del modulo TCO 'Taglie e colori'
      '-----------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then oCleConf.bModTCO = True Else oCleConf.bModTCO = False
      '-----------------------------------------------------------------------------------------
      '--- Controlla se esiste il modulo CUSTOMER SERVICE
      '-----------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModAS)) Then oCleConf.bModuloAS = True Else oCleConf.bModuloAS = False
      '-----------------------------------------------------------------------------------------

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("testord", dttCampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()

      CreaDatatableFiltri()

      dcFiltri1.DataSource = dsFiltri.Tables("FILTRI1")
      dsFiltri.AcceptChanges()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.MinimumSize = Me.Size

      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo CUSTOMER SERVICE, non mette 'Impegno di commessa'
      '--- nell'elenco dei tipi ordini/impegni
      '-------------------------------------------------------
      edAnno.Text = Format(Now, "yyyy")
      edSerie.Text = ""
      edNumordini.Text = "0"
      edNumordfin.Text = "99999999"
      edContoini.Text = "0"
      edContofin.Text = "999999999"
      edDatordini.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edDatordfin.Text = IntSetDate("31/12/" & Format(Now, "yyyy"))
      edDataCons.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edDataConsfin.Text = IntSetDate("31/12/2099")
      lbCoddestini.Enabled = False : lbCoddestfin.Enabled = False
      edCoddestini.Text = "0" : edCoddestfin.Text = "999999999"
      edCoddestini.Enabled = False : edCoddestfin.Enabled = False
      edRiga.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edRigafin.Text = IntSetDate("31/12/2099")
      edCodagen.Text = "0"
      edZona.Text = "0"
      lbDescontoini.Text = ""
      lbDescontofin.Text = ""
      lbDesagen.Text = ""
      lbDeszona.Text = ""
      edCodcfam.Text = ""
      lbDescodcfam.Text = ""
      '-----------------------------------------------------------------------------------------
      oCleConf.bUsaKeyOrdWord = CBool(oMenu.GetSettingBus("Bsorgsor", "Opzioni", ".", "UsaKeyordWord", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      oCleConf.bNomeDocWordNumero = CBool(oMenu.GetSettingBus("BSORCONF", "OPZIONI", ".", "NomeDocWordNumero", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      oCleConf.nAltezzaGif = NTSCInt(oMenu.GetSettingBus("BSORCONF", "OPZIONI", ".", "AltezzaGif", "20", " ", "20"))
      lPunto = 0
      While InStr((lPunto + 1), oApp.RptDir, "\") > 0
        lPunto = InStr((lPunto + 1), oApp.RptDir, "\")
      End While
      strPath = Microsoft.VisualBasic.Left(oApp.RptDir, lPunto) & "Images\"
      '-----------------------------------------------------------------------------------------

      edSerie.Enabled = False
      ckSerie.Checked = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      Me.GctlApplicaDefaultValue()

      'Prende la struttura della tabella
      oCleConf.GetTableStructMovIfil(dttDefault)

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
  Public Overridable Sub FRMORCONF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      GctlApplicaDefaultValue()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORCONF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    Dim strTmp As String = ""
    Dim i As Integer = 0
    dsFiltri.Tables("FILTRI1").AcceptChanges()
    For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
    Next
    strTmp = strTmp.Substring(0, strTmp.Length - 1)
    oMenu.SaveSettingBus("BNORCONF", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
  End Sub

  Public Overridable Sub FRMORCONF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not TestPreStampa() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not TestPreStampa() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Try
      If Not TestPreStampa() Then Return
      '-----------------------
      'faccio comporre la query al dl
      strQueryWord = oCleConf.GetQueryStampaWord(cbTipork.SelectedValue, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, _
                                       edNumordfin.Text, edContoini.Text, edContofin.Text, edDatordini.Text, _
                                       edDatordfin.Text, edDataCons.Text, edDataConsfin.Text, _
                                       "0", "".PadLeft(9, "9"c), edRiga.Text, edRigafin.Text, edAnno.Text, _
                                       edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, edCodagen.Text, _
                                       edZona.Text, opStorico0.Checked, opStorico1.Checked, opStampati0.Checked, _
                                       opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, edCodcfam.Text, _
                                       oCleConf.bModTCO, False, "0", "0", oCleConf.bUsaKeyOrdWord, ckCliForn.Checked, _
                                       "", False, False)
      If strQueryWord = "" Then Return

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORCONF"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = cbTipork.SelectedValue
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar1 = ckCliForn.Checked
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

      If oPar.bPar5 Then
        AggiornaTestord(0)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPdf.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante

    Try
      If Not TestPreStampa() Then Return

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleConf.GetQueryStampaPdf(cbTipork.SelectedValue, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, _
                                       edNumordfin.Text, edContoini.Text, edContofin.Text, edDatordini.Text, _
                                       edDatordfin.Text, edDataCons.Text, edDataConsfin.Text, _
                                       "0", "".PadLeft(9, "9"c), edRiga.Text, edRigafin.Text, edAnno.Text, _
                                       edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, edCodagen.Text, _
                                       edZona.Text, opStorico0.Checked, opStorico1.Checked, opStampati0.Checked, _
                                       opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, edCodcfam.Text, _
                                       oCleConf.bModTCO, False, "0", "0", oCleConf.bUsaKeyOrdWord, _
                                       ckCliForn.Checked, strQueryGetDocUnico, "", False, False)
      If strQueryGetDocMultipli = "" Or strQueryGetDocUnico = "" Then Return

      '--------------------------------
      'se devo passare delle formule lo faccio tramite questo datatable (per la 'PeSetFormula'
      'devo compilare o num, o str, o data a seconda del tipo di dato. 'name' deve sempre essere impostata
      dttFormule.Columns.Add("name", GetType(String))
      dttFormule.Columns.Add("num", GetType(Decimal))
      dttFormule.Columns.Add("str", GetType(String))
      dttFormule.Columns.Add("data", GetType(DateTime))
      dttFormule.Columns("name").DefaultValue = Nothing
      dttFormule.Columns("num").DefaultValue = Nothing
      dttFormule.Columns("str").DefaultValue = Nothing
      dttFormule.Columns("data").DefaultValue = Nothing
      dttFormule.AcceptChanges()

      'prima parte di PeSetSelectionFormula
      strQueryCrw32FileMultipli = ComponiFormula(0)
      strQueryCrw32FileUnico = strQueryCrw32FileMultipli & _
                                 " AND {TESTORD.td_valuta} = |valuta|" & _
                                 " AND {TESTORD.td_scorpo} = |scorpo|"
      strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                                 " AND {MOVORD.mo_anno} = |anno|" & _
                                 " AND {MOVORD.mo_numord} = |numero|" & _
                                 " AND {MOVORD.mo_serie} = |serie|" & _
                                 " AND {MOVORD.mo_tipork} = |tipork|"
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BSORCONF"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico
      oPar.strParam = "Stampa ordini"
      oPar.ctlPar1 = Me
      oPar.ctlPar2 = dttFormule
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

      oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)


      '-------------------------------
      'da bnpdgenp è stato scelto di stampare anche su carta
      If oPar.bPar4 Then
        Stampa(1)
      ElseIf oPar.bPar5 Then
        '-------------------------------
        'Se faccio la stampa sopra ci pensa lui ad aggiornare la testata
        'aggiorno il flag di 'Stampato' su testord
        AggiornaTestord(0)
      End If



    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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

      If edContoini.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "Q", "R", "V" : strTipork = "C"
          Case "$", "H", "O", "X", "Y" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edContoini.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edContoini.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edContoini.Text Then edContoini.NTSTextDB = NTSZOOM.strIn

      ElseIf edContofin.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "Q", "R", "V" : strTipork = "C"
          Case "$", "H", "O", "X", "Y" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edContofin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edContofin.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edContofin.Text Then edContofin.NTSTextDB = NTSZOOM.strIn

      ElseIf edCoddestini.Focused Then
        SetFastZoom(edCoddestini.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddestini.Text
        oParam.lContoCF = NTSCInt(edContoini.Text)
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddestini.Text Then edCoddestini.Text = NTSZOOM.strIn

      ElseIf edCoddestfin.Focused Then
        SetFastZoom(edCoddestfin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddestfin.Text
        oParam.lContoCF = NTSCInt(edContoini.Text)
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddestfin.Text Then edCoddestfin.Text = NTSZOOM.strIn

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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim n As Integer = 0
    Dim strTipodoc As String
    Dim bExist(3) As Boolean
    Dim bStampato As Boolean
    Dim strCaption As String
    Dim bAggiorna As Boolean
    Dim strNotatotali As String = "N"

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se impostato almeno uno dei filtri sulle righe, setta a "S" il flag da passare alla formula
      '--- NOTATOTALI, in modo da visualizzare nota e asterischi vicino ai totali di report
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCDate(edRiga.Text) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(edRigafin.Text) <> NTSCDate(IntSetDate("31/12/2099"))) Or _
         (opRiga0.Checked = True) Or (opRiga1.Checked = True) Then strNotatotali = "S"
      '--------------------------------------------------------------------------------------------------------------
      strTipodoc = NTSCStr(cbTipork.SelectedValue())
      For j = 1 To 3
        If oCleConf.ReportExist(edDatordini.Text, edDatordfin.Text, edDataCons.Text, _
                                 edDataConsfin.Text, edRiga.Text, edRigafin.Text, cbTipork.SelectedValue, _
                                 IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, edNumordfin.Text, edContoini.Text, _
                                 edContofin.Text, "0", "".PadLeft(9, "9"c), edAnno.Text, _
                                 edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, j, _
                                 edCodagen.Text, edZona.Text, opStorico0.Checked, opStorico1.Checked, _
                                 opStampati0.Checked, opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, _
                                 edCodcfam.Text, oCleConf.bModTCO, False, "0", "0", "", False, False) = True Then
          bExist(j) = True
          bAggiorna = False
          Select Case j
            Case 1 : strCaption = oApp.Tr(Me, 129640274285250112, "Stampa Ordini in Euro senza scorporo")
            Case 2 : strCaption = oApp.Tr(Me, 129640274355712999, "Stampa Ordini in Euro con scorporo")
            Case 3 : strCaption = oApp.Tr(Me, 129640274442424534, "Stampa Ordini in Valuta")
            Case Else : strCaption = "STAMPA ORDINI -- CONFERME D'ORDINE -- PREVENTIVI -- IMPEGNI DI TRASFERIMENTO"
          End Select
          '--------------------------------------------------
          'preparo il motore di stampa
          strCrpe = ComponiFormula(j)
          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORCONF", "Reports" & j, strTipodoc, 0, nDestin, "BSORGSOR.RPT", False, strCaption, False)
          If nPjob Is Nothing Then Return Else bAggiorna = True

          '--------------------------------------------------
          'lancio tutti gli eventuali reports (gestisce già  il multireport)
          For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NOTATOTALI", ConvStrRpt(strNotatotali))
            'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
            For n = 3 To 12
              If Trim(CStr(CType(nPjob, Array).GetValue(n, i))) <> "" Then
                nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(n, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(n + 10, i))))
              End If
            Next n
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Next
          If bAggiorna = True Then AggiornaTestord(j)
        End If
      Next

      For j = 1 To 3
        If bExist(j) = True Then bStampato = True
      Next
      If bStampato = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128538443219687405, "Non esistono dati con queste caratteristiche."))
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If NTSCInt(edAnno.Text) <> 0 Then
        If Not (NTSCInt(edAnno.Text) >= 1900 And NTSCInt(edAnno.Text) <= 2099) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128599196327187500, "Se l'anno e' diverso da zero il numero deve essere compreso fa 1900 e 2099."))
          edAnno.Text = "0"
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodagen_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodagen.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodagen_Validated(NTSCInt(edCodagen.Text), strTmp, strError) Then
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
  Public Overridable Sub edCodcfam_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp, strError) Then
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
  Public Overridable Sub edContoini_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edContoini.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edContoini_Validated(NTSCInt(edContoini.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edContoini.Text = "0"
        lbDescontoini.Text = ""
      Else
        lbDescontoini.Text = strTmp
        If edContoini.Text <> "0" Then edContofin.Text = edContoini.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edContofin_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edContofin.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edContofin_Validated(NTSCInt(edContofin.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edContofin.Text = "999999999"
        lbDescontofin.Text = ""
      Else
        lbDescontofin.Text = strTmp
      End If

      If (NTSCInt(edContoini.Text) = NTSCInt(edContofin.Text)) And (NTSCInt(edContoini.Text) > 0) Then
        If edCoddestini.Enabled = False Then
          GctlSetVisEnab(lbCoddestini, False)
          GctlSetVisEnab(lbCoddestfin, False)
          GctlSetVisEnab(edCoddestini, False)
          GctlSetVisEnab(edCoddestfin, False)
          edCoddestini.Text = "0"
          edCoddestfin.Text = "999999999"
        End If
      Else
        edCoddestini.Text = "0"
        edCoddestfin.Text = "999999999"
        edCoddestini.Enabled = False
        edCoddestfin.Enabled = False
        lbCoddestini.Enabled = False
        lbCoddestfin.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edZona_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edZona.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edZona_Validated(NTSCInt(edZona.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edZona.Text = "0"
        lbDeszona.Text = ""
      Else
        lbDeszona.Text = strTmp
      End If

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
      strTmp = NTSCStr(oMenu.GetSettingBus("BNORCONF", "RECENT", ".", "Filtri1", "", " ", ""))
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


  Public Overridable Function ComponiFormula(ByVal nTipostampa As Integer) As String
    '------------------------------------------------------------
    '
    '---Compone la stringa per la selezione dei record nei report
    '
    '---Case nTipostampa:
    '---0: Tutti i 3 tipi di record
    '---1: In Lire senza scorporo
    '---2: In Lire con scorporo
    '---3: In valuta (senza scorporo)
    '-------------------------------------------------------------
    Dim strC As String, strTipork As String
    Dim strDatordini As String, strDatordfin As String
    Dim strDataCons As String, strDataConsfin As String
    Dim strRiga As String, strRigafin As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckSerie.Checked = False Then edSerie.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      strDatordini = ConvDataRpt(edDatordini.Text)
      strDatordfin = ConvDataRpt(edDatordfin.Text)
      strDataCons = ConvDataRpt(edDataCons.Text)
      strDataConsfin = ConvDataRpt(edDataConsfin.Text)
      strRiga = ConvDataRpt(edRiga.Text)
      strRigafin = ConvDataRpt(edRigafin.Text)
      strTipork = NTSCStr(cbTipork.SelectedValue)
      strC = "{TESTORD.td_conto} in " & edContoini.Text & " to " & edContofin.Text & _
        " and {TESTORD.td_tipork} = '" & strTipork & "'" & _
        " and {TESTORD.td_numord} in " & edNumordini.Text & " to " & edNumordfin.Text & _
        " and {TESTORD.td_datord} in " & strDatordini & " to " & strDatordfin & _
        " and {TESTORD.td_magaz2} <> {KEYORD.ko_magaz}" & _
        " and {MOVORD.mo_stasino} <> 'N'" & _
        " and {MOVORD.mo_datcons} in " & strRiga & " to " & strRigafin & _
        " and {TESTORD.td_datcons} in " & strDataCons & " to " & strDataConsfin
      If edSerie.Text <> "" Then strC = strC & " AND {TESTORD.td_serie} = " & CStrSQL(edSerie.Text)
      If NTSCInt(edAnno.Text) <> 0 Then strC = strC & " and {TESTORD.td_anno} = " & edAnno.Text
      If edCoddestini.Enabled Then
        strC = strC & " and {TESTORD.td_coddest} in " & edCoddestini.Text & " to " & edCoddestfin.Text
      End If
      Select Case nTipostampa
        Case 1 : strC = strC & " and {TESTORD.td_valuta} = 0 and {TESTORD.td_scorpo} = 'N'"
        Case 2 : strC = strC & " and {TESTORD.td_valuta} = 0 and {TESTORD.td_scorpo} = 'S'"
        Case 3 : strC = strC & " and {TESTORD.td_valuta} <> 0 and {TESTORD.td_scorpo} = 'N'"
      End Select
      If NTSCInt(edCodagen.Text) > 0 Then strC = strC & " and {TESTORD.td_codagen} = " & edCodagen.Text
      If NTSCInt(edZona.Text) > 0 Then strC = strC & " and {ANAGRA.an_zona} = " & edZona.Text
      If opStorico0.Checked = True Then strC = strC & " and {TESTORD.td_flevas} = 'N'"
      If opStorico1.Checked = True Then strC = strC & " and {TESTORD.td_flevas} = 'S'"
      If opStampati0.Checked = True Then strC = strC & " and {TESTORD.td_flstam} = 'N'"
      If opStampati1.Checked = True Then strC = strC & " and {TESTORD.td_flstam} = 'S'"
      If opRiga0.Checked = True Then strC = strC & " and {MOVORD.mo_flevas} = 'C'"
      If opRiga1.Checked = True Then strC = strC & " and {MOVORD.mo_flevas} = 'S'"
      strC = strC & " And {testord.codditt} = '" & DittaCorrente & "'"
      If edCodcfam.Text <> "" Then
        strC = strC & " And {testord.td_codcfam} = " & CStrSQL(edCodcfam.Text)
      End If
      '-----------------------------------------------------------------------------------------
      strC = strC & ComponiWhereFiltriEstesi(True)

      ComponiFormula = strC

    Catch ex As Exception
      ComponiFormula = ""
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreStampa() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla gli intervalli
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edNumordini.Text) > NTSCInt(edNumordfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457464773159, "Il numero ordine iniziale non può essere superiore a quello finale."))
        edNumordini.Text = "0"
        edNumordfin.Text = "".PadLeft(9, "9"c)
        edNumordini.Focus()
        Return False
      End If
      If NTSCInt(edContoini.Text) > NTSCInt(edContofin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457487684579, "Il conto iniziale non può essere superiore a quello finale."))
        edContoini.Text = "0"
        edContofin.Text = "".PadLeft(9, "9"c)
        edContoini.Focus()
        Return False
      End If
      If NTSCDate(edDatordini.Text) > NTSCDate(edDatordfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457527584739, "La data ordine iniziale non può essere superiore a quella finale."))
        edDatordini.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edDatordfin.Text = IntSetDate("31/12/" & Format(Now, "yyyy"))
        edDatordini.Focus()
        Return False
      End If
      If NTSCDate(edDataCons.Text) > NTSCDate(edDataConsfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457544885199, "La data consegna ordine iniziale non può essere superiore a quella finale."))
        edDataCons.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edDataConsfin.Text = IntSetDate("31/12/2099")
        edDataCons.Focus()
        Return False
      End If
      If NTSCDate(edRiga.Text) > NTSCDate(edRigafin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457582915039, "La data consegna riga iniziale non può essere superiore a quella finale."))
        edRiga.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edRigafin.Text = IntSetDate("31/12/2099")
        edRiga.Focus()
        Return False
      End If
      If edCoddestini.Enabled Then
        If NTSCInt(edCoddestini.Text) > NTSCInt(edCoddestfin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128538457600527219, "Il codice destinazione iniziale non può essere superiore a quello finale."))
          edCoddestini.Text = "0"
          edCoddestfin.Text = "999999999"
          edCoddestini.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckFiltri1() Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
                  oApp.MsgBoxErr(oApp.Tr(Me, 128823856513901278, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
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

  Public Overridable Function AggiornaTestord(ByVal nTipostampa As Integer) As Boolean
    Try
      '--------------------------------------------------------------------
      '---Se i record sono già stati stampati non fa' nessun aggiornamento
      If opStampati1.Checked = True Then Return True

      Return oCleConf.AggiornaTestord(edDatordini.Text, edDatordfin.Text, edDataCons.Text, _
                                 edDataConsfin.Text, edRiga.Text, edRigafin.Text, cbTipork.SelectedValue, _
                                 edSerie.Text, edNumordini.Text, edNumordfin.Text, edContoini.Text, _
                                 edContofin.Text, "0", "".PadLeft(9, "9"c), edAnno.Text, _
                                 edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, nTipostampa, _
                                 edCodagen.Text, edZona.Text, opStorico0.Checked, opStorico1.Checked, _
                                 opStampati0.Checked, opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, _
                                 edCodcfam.Text, oCleConf.bModTCO, False, "0", "0")

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
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
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
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
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

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
      ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------            
      If edContoini.ContainsFocus Or edContofin.ContainsFocus Then strTabName = "ANAGRACF"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Dim strTdflevas As String = ""
    Dim strFlstam As String = ""
    Dim strMoflevas As String = ""
    '----------------------------------------------------------------------------------------------------------------
    If opStorico0.Checked = True Then strTdflevas = "N"
    If opStorico1.Checked = True Then strTdflevas = "S"
    '----------------------------------------------------------------------------------------------------------------
    If opStampati0.Checked = True Then strFlstam = "N"
    If opStampati1.Checked = True Then strFlstam = "S"
    '----------------------------------------------------------------------------------------------------------------
    If opRiga0.Checked = True Then strMoflevas = "C"
    If opRiga1.Checked = True Then strMoflevas = "S"
    '----------------------------------------------------------------------------------------------------------------
    Select Case strIn.Substring(0, 1)
      Case "#"
        '------------------------------------------------------------------------------------------------------------
        '--- Variabile esposte SPECIFICHE PER PROGRAMMA
        '------------------------------------------------------------------------------------------------------------
        Select Case UCase$(Mid$(strIn, 2))
          Case "TIPORK" : Return cbTipork.SelectedValue
          Case "STORICO" : Return strTdflevas
          Case "STAMPATI" : Return strFlstam
          Case "RIGA" : Return strMoflevas
          Case Else : Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else : Return MyBase.ResolveField(strIn)
    End Select
    '----------------------------------------------------------------------------------------------------------------
    Return ""
    '----------------------------------------------------------------------------------------------------------------
  End Function

#Region "Gestione filtri"
  Public Overridable Function ComponiDatatable(ByRef dttCampiForm As DataTable, ByVal oControl As Control) As Boolean
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
        If Not oCleConf.LeggiFiltro(lCod, "BNORCONF", "FRMORCONF", dttPersForm) Then Return False
        AggiornaForm(dttPersForm, False)

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
              Dim dtrTmp() As DataRow = dttCampi.Select("cb_nomcampo = " & CStrSQL(dttControl.Rows(z)!mo_colkeyvalue))
              If NTSCInt(dtrTmp(0)!cb_tipocampo) = 8 Then ' 8 = data
                dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
              Else
                dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = NTSCStr(dttControl.Rows(z)!mo_valore)
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
#End Region

End Class