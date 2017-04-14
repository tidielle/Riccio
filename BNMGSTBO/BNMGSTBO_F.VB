#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGSTBO_F
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
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDatordfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatordini As NTSInformatica.NTSLabel
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbContoini As NTSInformatica.NTSLabel
  Public WithEvents lbNumordini As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDanumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents lbNumordfin As NTSInformatica.NTSLabel
  Public WithEvents edAnumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbContofin As NTSInformatica.NTSLabel
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesagen As NTSInformatica.NTSLabel
  Public WithEvents lbDescodcfam As NTSInformatica.NTSLabel
  Public WithEvents fmFlcont As NTSInformatica.NTSGroupBox
  Public WithEvents opFlcontEntrambe As NTSInformatica.NTSRadioButton
  Public WithEvents opFlcontNo As NTSInformatica.NTSRadioButton
  Public WithEvents opFlcontSi As NTSInformatica.NTSRadioButton
  Public WithEvents fmBolleFatturate As NTSInformatica.NTSGroupBox
  Public WithEvents opBolleFatturateEntrambe As NTSInformatica.NTSRadioButton
  Public WithEvents opBolleFatturateNo As NTSInformatica.NTSRadioButton
  Public WithEvents opBolleFatturateSi As NTSInformatica.NTSRadioButton
  Public WithEvents fmSeldocumenti As NTSInformatica.NTSGroupBox
  Public WithEvents ckSeldocumenti As NTSInformatica.NTSCheckBox
  Public WithEvents ckReport As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents fmTipoStampa As NTSInformatica.NTSGroupBox
  Public WithEvents opTipoStampaCompleta As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoStampaRidotta As NTSInformatica.NTSRadioButton
  Public WithEvents fmTippaga As NTSInformatica.NTSGroupBox
  Public WithEvents opTutti As NTSInformatica.NTSRadioButton
  Public WithEvents cbTippaga As NTSInformatica.NTSComboBox
  Public WithEvents opSpecifico As NTSInformatica.NTSRadioButton
  Public WithEvents lbDesTipobf As NTSInformatica.NTSLabel
  Public WithEvents lbDesdest As NTSInformatica.NTSLabel
  Public WithEvents lbTipobf As NTSInformatica.NTSLabel
  Public WithEvents edTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSTBO_F))
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
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbNumordini = New NTSInformatica.NTSLabel
    Me.lbContoini = New NTSInformatica.NTSLabel
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatordini = New NTSInformatica.NTSLabel
    Me.lbDatordfin = New NTSInformatica.NTSLabel
    Me.lbCodagen = New NTSInformatica.NTSLabel
    Me.edCodagen = New NTSInformatica.NTSTextBoxNum
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbNumordfin = New NTSInformatica.NTSLabel
    Me.edAnumdoc = New NTSInformatica.NTSTextBoxNum
    Me.lbContofin = New NTSInformatica.NTSLabel
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesagen = New NTSInformatica.NTSLabel
    Me.lbDescodcfam = New NTSInformatica.NTSLabel
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
    Me.lbTipobf = New NTSInformatica.NTSLabel
    Me.edTipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddest = New NTSInformatica.NTSLabel
    Me.edCoddest = New NTSInformatica.NTSTextBoxNum
    Me.fmSeldocumenti = New NTSInformatica.NTSGroupBox
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.ckReport = New NTSInformatica.NTSCheckBox
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.ckSeldocumenti = New NTSInformatica.NTSCheckBox
    Me.fmFlcont = New NTSInformatica.NTSGroupBox
    Me.opFlcontEntrambe = New NTSInformatica.NTSRadioButton
    Me.opFlcontNo = New NTSInformatica.NTSRadioButton
    Me.opFlcontSi = New NTSInformatica.NTSRadioButton
    Me.fmBolleFatturate = New NTSInformatica.NTSGroupBox
    Me.opBolleFatturateEntrambe = New NTSInformatica.NTSRadioButton
    Me.opBolleFatturateNo = New NTSInformatica.NTSRadioButton
    Me.opBolleFatturateSi = New NTSInformatica.NTSRadioButton
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    CType(Me.fmSeldocumenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSeldocumenti.SuspendLayout()
    CType(Me.ckReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSeldocumenti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFlcont, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFlcont.SuspendLayout()
    CType(Me.opFlcontEntrambe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlcontNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlcontSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmBolleFatturate, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmBolleFatturate.SuspendLayout()
    CType(Me.opBolleFatturateEntrambe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBolleFatturateNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBolleFatturateSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.edAnno.Location = New System.Drawing.Point(126, 124)
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
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(126, 278)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(85, 20)
    Me.edCodcfam.TabIndex = 7
    '
    'edDanumdoc
    '
    Me.edDanumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumdoc.Location = New System.Drawing.Point(126, 190)
    Me.edDanumdoc.Name = "edDanumdoc"
    Me.edDanumdoc.NTSDbField = ""
    Me.edDanumdoc.NTSFormat = "0"
    Me.edDanumdoc.NTSForzaVisZoom = False
    Me.edDanumdoc.NTSOldValue = ""
    Me.edDanumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDanumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDanumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumdoc.Properties.MaxLength = 65536
    Me.edDanumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumdoc.Size = New System.Drawing.Size(85, 20)
    Me.edDanumdoc.TabIndex = 8
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.Location = New System.Drawing.Point(126, 146)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(85, 20)
    Me.edDaconto.TabIndex = 9
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(20, 127)
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
    Me.lbNumordini.Location = New System.Drawing.Point(19, 193)
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
    Me.lbContoini.Location = New System.Drawing.Point(20, 149)
    Me.lbContoini.Name = "lbContoini"
    Me.lbContoini.NTSDbField = ""
    Me.lbContoini.Size = New System.Drawing.Size(52, 13)
    Me.lbContoini.TabIndex = 13
    Me.lbContoini.Text = "Dal conto"
    Me.lbContoini.Tooltip = ""
    Me.lbContoini.UseMnemonic = False
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.Location = New System.Drawing.Point(126, 168)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(85, 20)
    Me.edDatini.TabIndex = 17
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.Location = New System.Drawing.Point(334, 168)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfin.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDatordini.Location = New System.Drawing.Point(19, 171)
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
    Me.lbDatordfin.Location = New System.Drawing.Point(235, 171)
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
    Me.lbCodagen.Location = New System.Drawing.Point(20, 237)
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
    Me.edCodagen.Location = New System.Drawing.Point(126, 234)
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
    Me.edCodagen.Size = New System.Drawing.Size(85, 20)
    Me.edCodagen.TabIndex = 33
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(20, 282)
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
    Me.edSerie.Location = New System.Drawing.Point(334, 124)
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
    Me.lbNumordfin.Location = New System.Drawing.Point(236, 193)
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
    Me.edAnumdoc.Location = New System.Drawing.Point(334, 190)
    Me.edAnumdoc.Name = "edAnumdoc"
    Me.edAnumdoc.NTSDbField = ""
    Me.edAnumdoc.NTSFormat = "0"
    Me.edAnumdoc.NTSForzaVisZoom = False
    Me.edAnumdoc.NTSOldValue = ""
    Me.edAnumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbContofin.Location = New System.Drawing.Point(235, 149)
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
    Me.edAconto.Location = New System.Drawing.Point(334, 146)
    Me.edAconto.Name = "edAconto"
    Me.edAconto.NTSDbField = ""
    Me.edAconto.NTSFormat = "0"
    Me.edAconto.NTSForzaVisZoom = False
    Me.edAconto.NTSOldValue = ""
    Me.edAconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAconto.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDesagen.Location = New System.Drawing.Point(217, 234)
    Me.lbDesagen.Name = "lbDesagen"
    Me.lbDesagen.NTSDbField = ""
    Me.lbDesagen.Size = New System.Drawing.Size(435, 20)
    Me.lbDesagen.TabIndex = 49
    Me.lbDesagen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesagen.Tooltip = ""
    Me.lbDesagen.UseMnemonic = False
    '
    'lbDescodcfam
    '
    Me.lbDescodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodcfam.Location = New System.Drawing.Point(217, 278)
    Me.lbDescodcfam.Name = "lbDescodcfam"
    Me.lbDescodcfam.NTSDbField = ""
    Me.lbDescodcfam.Size = New System.Drawing.Size(435, 20)
    Me.lbDescodcfam.TabIndex = 51
    Me.lbDescodcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodcfam.Tooltip = ""
    Me.lbDescodcfam.UseMnemonic = False
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(239, 121)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(44, 19)
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
    Me.fmTipoStampa.Location = New System.Drawing.Point(289, 327)
    Me.fmTipoStampa.Name = "fmTipoStampa"
    Me.fmTipoStampa.Size = New System.Drawing.Size(180, 45)
    Me.fmTipoStampa.TabIndex = 46
    Me.fmTipoStampa.Text = "Tipo di stampa:"
    '
    'opTipoStampaCompleta
    '
    Me.opTipoStampaCompleta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampaCompleta.Location = New System.Drawing.Point(85, 23)
    Me.opTipoStampaCompleta.Name = "opTipoStampaCompleta"
    Me.opTipoStampaCompleta.NTSCheckValue = "S"
    Me.opTipoStampaCompleta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampaCompleta.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampaCompleta.Properties.Caption = "Completa"
    Me.opTipoStampaCompleta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampaCompleta.Size = New System.Drawing.Size(75, 19)
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
    Me.opTipoStampaRidotta.Properties.Caption = "Ridotta"
    Me.opTipoStampaRidotta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampaRidotta.Size = New System.Drawing.Size(70, 19)
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
    Me.fmTippaga.Location = New System.Drawing.Point(12, 304)
    Me.fmTippaga.Name = "fmTippaga"
    Me.fmTippaga.Size = New System.Drawing.Size(271, 68)
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
    Me.cbTippaga.Location = New System.Drawing.Point(114, 22)
    Me.cbTippaga.Name = "cbTippaga"
    Me.cbTippaga.NTSDbField = ""
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
    Me.opSpecifico.Properties.Caption = "Specifico"
    Me.opSpecifico.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSpecifico.Size = New System.Drawing.Size(83, 19)
    Me.opSpecifico.TabIndex = 0
    '
    'lbDesTipobf
    '
    Me.lbDesTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbDesTipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesTipobf.Location = New System.Drawing.Point(217, 212)
    Me.lbDesTipobf.Name = "lbDesTipobf"
    Me.lbDesTipobf.NTSDbField = ""
    Me.lbDesTipobf.Size = New System.Drawing.Size(435, 20)
    Me.lbDesTipobf.TabIndex = 49
    Me.lbDesTipobf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesTipobf.Tooltip = ""
    Me.lbDesTipobf.UseMnemonic = False
    '
    'lbDesdest
    '
    Me.lbDesdest.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdest.Location = New System.Drawing.Point(217, 256)
    Me.lbDesdest.Name = "lbDesdest"
    Me.lbDesdest.NTSDbField = ""
    Me.lbDesdest.Size = New System.Drawing.Size(435, 20)
    Me.lbDesdest.TabIndex = 49
    Me.lbDesdest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesdest.Tooltip = ""
    Me.lbDesdest.UseMnemonic = False
    '
    'lbTipobf
    '
    Me.lbTipobf.AutoSize = True
    Me.lbTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobf.Location = New System.Drawing.Point(20, 215)
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
    Me.edTipobf.Location = New System.Drawing.Point(126, 212)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbCoddest.Location = New System.Drawing.Point(20, 259)
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
    Me.edCoddest.Location = New System.Drawing.Point(126, 256)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSFormat = "0"
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = ""
    Me.edCoddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.Properties.MaxLength = 65536
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(85, 20)
    Me.edCoddest.TabIndex = 33
    '
    'fmSeldocumenti
    '
    Me.fmSeldocumenti.AllowDrop = True
    Me.fmSeldocumenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSeldocumenti.Appearance.Options.UseBackColor = True
    Me.fmSeldocumenti.Controls.Add(Me.lbTipork)
    Me.fmSeldocumenti.Controls.Add(Me.ckReport)
    Me.fmSeldocumenti.Controls.Add(Me.cbTipork)
    Me.fmSeldocumenti.Controls.Add(Me.ckSeldocumenti)
    Me.fmSeldocumenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSeldocumenti.Location = New System.Drawing.Point(12, 36)
    Me.fmSeldocumenti.Name = "fmSeldocumenti"
    Me.fmSeldocumenti.Size = New System.Drawing.Size(356, 82)
    Me.fmSeldocumenti.TabIndex = 38
    '
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Location = New System.Drawing.Point(8, 33)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(83, 13)
    Me.lbTipork.TabIndex = 39
    Me.lbTipork.Text = "Tipo documento"
    Me.lbTipork.Tooltip = ""
    Me.lbTipork.UseMnemonic = False
    '
    'ckReport
    '
    Me.ckReport.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckReport.Location = New System.Drawing.Point(11, 56)
    Me.ckReport.Name = "ckReport"
    Me.ckReport.NTSCheckValue = "S"
    Me.ckReport.NTSUnCheckValue = "N"
    Me.ckReport.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckReport.Properties.Appearance.Options.UseBackColor = True
    Me.ckReport.Properties.Caption = "Utilizza reports standard"
    Me.ckReport.Size = New System.Drawing.Size(145, 19)
    Me.ckReport.TabIndex = 38
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(114, 30)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
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
    Me.ckSeldocumenti.Location = New System.Drawing.Point(9, 0)
    Me.ckSeldocumenti.Name = "ckSeldocumenti"
    Me.ckSeldocumenti.NTSCheckValue = "S"
    Me.ckSeldocumenti.NTSUnCheckValue = "N"
    Me.ckSeldocumenti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSeldocumenti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSeldocumenti.Properties.Caption = "Seleziona documenti:"
    Me.ckSeldocumenti.Size = New System.Drawing.Size(135, 19)
    Me.ckSeldocumenti.TabIndex = 38
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
    Me.fmFlcont.Location = New System.Drawing.Point(510, 36)
    Me.fmFlcont.Name = "fmFlcont"
    Me.fmFlcont.Size = New System.Drawing.Size(142, 82)
    Me.fmFlcont.TabIndex = 45
    Me.fmFlcont.Text = "Fatture contabilizzate:"
    '
    'opFlcontEntrambe
    '
    Me.opFlcontEntrambe.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlcontEntrambe.EditValue = True
    Me.opFlcontEntrambe.Location = New System.Drawing.Point(9, 60)
    Me.opFlcontEntrambe.Name = "opFlcontEntrambe"
    Me.opFlcontEntrambe.NTSCheckValue = "S"
    Me.opFlcontEntrambe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontEntrambe.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontEntrambe.Properties.Caption = "Entrambe"
    Me.opFlcontEntrambe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontEntrambe.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontEntrambe.TabIndex = 2
    '
    'opFlcontNo
    '
    Me.opFlcontNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlcontNo.Location = New System.Drawing.Point(9, 40)
    Me.opFlcontNo.Name = "opFlcontNo"
    Me.opFlcontNo.NTSCheckValue = "S"
    Me.opFlcontNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontNo.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontNo.Properties.Caption = "No"
    Me.opFlcontNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontNo.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontNo.TabIndex = 1
    '
    'opFlcontSi
    '
    Me.opFlcontSi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opFlcontSi.Location = New System.Drawing.Point(9, 20)
    Me.opFlcontSi.Name = "opFlcontSi"
    Me.opFlcontSi.NTSCheckValue = "S"
    Me.opFlcontSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlcontSi.Properties.Appearance.Options.UseBackColor = True
    Me.opFlcontSi.Properties.Caption = "Si"
    Me.opFlcontSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlcontSi.Size = New System.Drawing.Size(111, 19)
    Me.opFlcontSi.TabIndex = 0
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
    Me.fmBolleFatturate.Location = New System.Drawing.Point(374, 36)
    Me.fmBolleFatturate.Name = "fmBolleFatturate"
    Me.fmBolleFatturate.Size = New System.Drawing.Size(130, 82)
    Me.fmBolleFatturate.TabIndex = 44
    Me.fmBolleFatturate.Text = "Bolle fatturate:"
    '
    'opBolleFatturateEntrambe
    '
    Me.opBolleFatturateEntrambe.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateEntrambe.EditValue = True
    Me.opBolleFatturateEntrambe.Location = New System.Drawing.Point(9, 60)
    Me.opBolleFatturateEntrambe.Name = "opBolleFatturateEntrambe"
    Me.opBolleFatturateEntrambe.NTSCheckValue = "S"
    Me.opBolleFatturateEntrambe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateEntrambe.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateEntrambe.Properties.Caption = "Entrambe"
    Me.opBolleFatturateEntrambe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateEntrambe.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateEntrambe.TabIndex = 2
    '
    'opBolleFatturateNo
    '
    Me.opBolleFatturateNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateNo.Location = New System.Drawing.Point(9, 40)
    Me.opBolleFatturateNo.Name = "opBolleFatturateNo"
    Me.opBolleFatturateNo.NTSCheckValue = "S"
    Me.opBolleFatturateNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateNo.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateNo.Properties.Caption = "No"
    Me.opBolleFatturateNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateNo.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateNo.TabIndex = 1
    '
    'opBolleFatturateSi
    '
    Me.opBolleFatturateSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBolleFatturateSi.Location = New System.Drawing.Point(9, 20)
    Me.opBolleFatturateSi.Name = "opBolleFatturateSi"
    Me.opBolleFatturateSi.NTSCheckValue = "S"
    Me.opBolleFatturateSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBolleFatturateSi.Properties.Appearance.Options.UseBackColor = True
    Me.opBolleFatturateSi.Properties.Caption = "Si"
    Me.opBolleFatturateSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBolleFatturateSi.Size = New System.Drawing.Size(111, 19)
    Me.opBolleFatturateSi.TabIndex = 0
    '
    'FRMMGSTBO_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(664, 383)
    Me.Controls.Add(Me.fmFlcont)
    Me.Controls.Add(Me.fmSeldocumenti)
    Me.Controls.Add(Me.fmBolleFatturate)
    Me.Controls.Add(Me.ckSerie)
    Me.Controls.Add(Me.lbDescodcfam)
    Me.Controls.Add(Me.edAnno)
    Me.Controls.Add(Me.lbDatordfin)
    Me.Controls.Add(Me.lbDatordini)
    Me.Controls.Add(Me.edCodcfam)
    Me.Controls.Add(Me.edSerie)
    Me.Controls.Add(Me.lbDesTipobf)
    Me.Controls.Add(Me.edDatfin)
    Me.Controls.Add(Me.fmTipoStampa)
    Me.Controls.Add(Me.edDanumdoc)
    Me.Controls.Add(Me.lbDesdest)
    Me.Controls.Add(Me.edDatini)
    Me.Controls.Add(Me.edAnumdoc)
    Me.Controls.Add(Me.lbDesagen)
    Me.Controls.Add(Me.lbNumordfin)
    Me.Controls.Add(Me.fmTippaga)
    Me.Controls.Add(Me.edDaconto)
    Me.Controls.Add(Me.lbCodcfam)
    Me.Controls.Add(Me.lbContofin)
    Me.Controls.Add(Me.lbTipobf)
    Me.Controls.Add(Me.lbContoini)
    Me.Controls.Add(Me.edCodagen)
    Me.Controls.Add(Me.lbNumordini)
    Me.Controls.Add(Me.edTipobf)
    Me.Controls.Add(Me.edAconto)
    Me.Controls.Add(Me.lbCodagen)
    Me.Controls.Add(Me.lbAnno)
    Me.Controls.Add(Me.lbCoddest)
    Me.Controls.Add(Me.edCoddest)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSTBO_F"
    Me.Text = "STAMPA/VISUALIZZAZIONE MOVIMENTI DI MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    CType(Me.fmSeldocumenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSeldocumenti.ResumeLayout(False)
    Me.fmSeldocumenti.PerformLayout()
    CType(Me.ckReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSeldocumenti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFlcont, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFlcont.ResumeLayout(False)
    Me.fmFlcont.PerformLayout()
    CType(Me.opFlcontEntrambe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlcontNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlcontSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmBolleFatturate, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmBolleFatturate.ResumeLayout(False)
    Me.fmBolleFatturate.PerformLayout()
    CType(Me.opBolleFatturateEntrambe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBolleFatturateNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBolleFatturateSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128641477567187500, "Tipo bolla/fattura"), tabtpbf)
      edCodagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427066016, "Agente"), tabcage)
      edCoddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427690860, "Destinazione"), tabdestdiv)
      edCoddest.CliePerDestdiv = edDaconto
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054431127502, "Linea/famiglia"), tabcfam, True)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")
      opTipoStampaCompleta.NTSSetParam(oMenu, oApp.Tr(Me, 129195093162014394, "Completa"), "C")
      opTipoStampaRidotta.NTSSetParam(oMenu, oApp.Tr(Me, 129195095171076894, "Ridotta"), "R")
      opTutti.NTSSetParam(oMenu, oApp.Tr(Me, 129195095182795644, "Tutti"), "T")
      cbTippaga.NTSSetParam(oApp.Tr(Me, 129195093162326894, "Tipo pagamento"))
      opSpecifico.NTSSetParam(oMenu, oApp.Tr(Me, 129195093162483144, "Specifico"), "S")
      ckReport.NTSSetParam(oMenu, oApp.Tr(Me, 129195093166545644, "Utilizza reports standard"), "S", "N")
      ckSeldocumenti.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167014394, "Seleziona documenti:"), "S", "N")
      opFlcontEntrambe.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167170644, "Entrambe"), "E")
      opFlcontNo.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167326894, "No"), "F")
      opFlcontSi.NTSSetParam(oMenu, oApp.Tr(Me, 129195095469358144, "Si"), "S")
      opBolleFatturateEntrambe.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167795644, "Entrambe"), "E")
      opBolleFatturateNo.NTSSetParam(oMenu, oApp.Tr(Me, 129195093167951894, "No"), "N")
      opBolleFatturateSi.NTSSetParam(oMenu, oApp.Tr(Me, 129195093168108144, "Si"), "S")

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
      'Lettura opzione di registro su utilizzazione di KEYMAG nella SelectionFormula
      oCleStbo.bUsaKeymag = CBool(oMenu.GetSettingBus("BSMGSTBO", "OPZIONI", ".", "UsaKeymag", "0", " ", "0"))
      '--------------------------------------------------------

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------
      InitControls()

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
      edTipobf.Text = "0"
      edCodcfam.Text = ""
      lbDescodcfam.Text = ""

      'Settaggio group box tipo documento
      lbTipork.Enabled = False
      cbTipork.Enabled = False
      ckReport.Enabled = False
      'Settagio group box tipo pagamento
      cbTippaga.Enabled = False

      edSerie.Enabled = False
      ckSerie.Checked = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      Me.GctlApplicaDefaultValue()

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
      '--- Se selezionate solo Fatture Differite Emesse/Ricevute
      '--- non permette la visualizzazione in griglia
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked Then
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
    Try
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
                              0, 999999999, ckSeldocumenti.Checked, False, _
                              cbTipork.SelectedValue, NTSCInt(edTipobf.Text), _
                              NTSCInt(edCoddest.Text), NTSCInt(edCodagen.Text), 0, _
                              opBolleFatturateEntrambe.Checked, opBolleFatturateSi.Checked, _
                              True, False, opFlcontEntrambe.Checked, opFlcontSi.Checked, _
                              edCodcfam.Text, False, 0, 0, "", False, _
                              NTSCInt(IIf(opSpecifico.Checked, cbTippaga.SelectedValue, 0)), strQueryGetDocUnico, _
                              strQueryGetDocClienti, strQueryGetDocAgenti, ckReport.Checked)
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
      strTabella = "TESTMAG"
      strTabellaMov = "mov" & strTabella.Substring(4)
      '--------------------------------------------------------------------------------------------------------------
      'prima parte di PeSetSelectionFormula
      strQueryCrw32FileMultipli = ComponiFormula()

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

      If ckSeldocumenti.Checked Then strTipork = cbTipork.SelectedValue

      If Not ckSeldocumenti.Checked Then
        '---------------------------------------------------------------
        '--- Report brogliaccio su TESTMAG (tutti i tipi record)
        '---------------------------------------------------------------
        strTitle += "Stampa/Visualizzazione movimenti di magazzino"
        '--- Report brogliaccio su TESTMAG (tutti i tipi record)
      Else
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
            Case "B", "M", "Z"
              Select Case strTipork
                Case "B"
                  strTitle += "Stampa D.D.T. emessi"
                Case "M"
                  strTitle += "Stampa D.D.T. ricevuti"
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
    Try
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa() Then Return
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = ComponiFormula()
      If strCrpe = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128642964853750000, "Errore durante la composizione della formula CRPE. Impossibile continuare."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked Then strTipork = cbTipork.SelectedValue
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
            Case "B", "M", "Z"
              Select Case strTipork
                Case "B"
                  strCrpe = strCrpe & " AND {movmag.mm_stasino} <> 'D'"
                  strTitle = "Stampa D.D.T. emessi"
                Case "M"
                  strTitle = "Stampa D.D.T. ricevuti"
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
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ComponiFormula() As String
    Dim strC, strTabella, strAggwhereCRM As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      strTabella = "TESTMAG"
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
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
        " And {" & strTabella & ".tm_conto} in " & edDaconto.Text & " to " & edAconto.Text & _
        " And {" & strTabella & ".tm_datdoc} in " & ConvDataRpt(edDatini.Text) & " to " & ConvDataRpt(edDatfin.Text) & _
        " And {" & strTabella & ".tm_numdoc} in " & edDanumdoc.Text & " to " & edAnumdoc.Text & _
        " And {" & strTabella & ".codditt} = '" & DittaCorrente & "'"
      '--------------------------------------------------------------------------------------------------------------
      If oCleStbo.bUsaKeymag AndAlso ckSeldocumenti.Checked AndAlso ckReport.Checked Then
          strC += " And {keymag.km_magaz} = {movmag.mm_magaz}"
      ElseIf ckSeldocumenti.Checked AndAlso ckReport.Checked Then
        If CBool(oMenu.GetSettingBus("BSMGSTBO", "OPZIONI", ".", "UsaKeymag" & cbTipork.SelectedValue.ToUpper, "0", " ", "0")) Then
          strC += " And {keymag.km_magaz} = {movmag.mm_magaz}"
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--------------------------------------------------------------------------------------------------------------
      If ckSeldocumenti.Checked Then strC += " And {" & strTabella & ".tm_tipork} = '" & cbTipork.SelectedValue & "'"
      '--------------------------------------------------------------------------------------------------------------
      If ckSerie.Checked = False Then edSerie.Text = ""
      If edSerie.Text <> "" Then strC += " And {" & strTabella & ".tm_serie} = '" & edSerie.Text & "'"
      If NTSCInt(edTipobf.Text) <> 0 Then strC += " And {" & strTabella & ".tm_tipobf} = " & edTipobf.Text
      If NTSCInt(edCodagen.Text) <> 0 Then strC += " And {" & strTabella & ".tm_codagen} = " & edCodagen.Text
      If NTSCInt(edCoddest.Text) <> 0 Then strC += " And {" & strTabella & ".tm_coddest} = " & edCoddest.Text
      If opBolleFatturateSi.Checked Then strC += " And {" & strTabella & ".tm_flfatt} = 'S'"
      If opBolleFatturateNo.Checked Then strC += " And {" & strTabella & ".tm_flfatt} = 'N'"
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
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      strC += strAggwhereCRM
      '--------------------------------------------------------------------------------------------------------------
      Return strC
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return ""
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
      If NTSCDate(edDatini.Text) > NTSCDate(edDatfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457527584739, "La data di partenza non può essere superiore a quella di arrivo."))
        Return False
      End If

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
                                   0, 999999999, ckSeldocumenti.Checked, False, _
                                   cbTipork.SelectedValue, NTSCInt(edTipobf.Text), _
                                   NTSCInt(edCoddest.Text), NTSCInt(edCodagen.Text), 0, _
                                   opBolleFatturateEntrambe.Checked, opBolleFatturateSi.Checked, _
                                   True, False, opFlcontEntrambe.Checked, opFlcontSi.Checked, _
                                   edCodcfam.Text, False, 0, 0, "", False, _
                                   NTSCInt(IIf(opSpecifico.Checked, cbTippaga.SelectedValue, 0)), nTiporeport, _
                                   bCaricaDatiPerGriglia)
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

  Public Overridable Sub opSpecifico_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opSpecifico.CheckStateChanged
    Try
      If opSpecifico.Checked Then
        GctlSetVisEnab(cbTippaga, False)
        cbTippaga.Focus()
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
        GctlSetVisEnab(lbTipork, False)
        GctlSetVisEnab(cbTipork, False)
        GctlSetVisEnab(ckReport, False)
        cbTipork.Focus()
      Else
        cbTipork.SelectedValue = "A"
        ckReport.Checked = False
        lbTipork.Enabled = False
        cbTipork.Enabled = False
        ckReport.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
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