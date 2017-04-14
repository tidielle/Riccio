Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SOTC_F
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__SOTC_F))
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
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbCambiaDitta = New NTSInformatica.NTSBarButtonItem
    Me.tlbAnacent = New NTSInformatica.NTSBarMenuItem
    Me.tlbAnalink = New NTSInformatica.NTSBarMenuItem
    Me.tlbAnacent2 = New NTSInformatica.NTSBarMenuItem
    Me.tsSotc = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.fmRiclassificati = New NTSInformatica.NTSGroupBox
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
    Me.cbAn_colbil = New NTSInformatica.NTSComboBox
    Me.lbAn_colbil = New NTSInformatica.NTSLabel
    Me.lbAn_ivainded = New NTSInformatica.NTSLabel
    Me.ckAn_ivainded = New NTSInformatica.NTSCheckBox
    Me.edAn_pcconto = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_pccontoLabel = New NTSInformatica.NTSLabel
    Me.fmComportamento = New NTSInformatica.NTSGroupBox
    Me.ckAn_partite = New NTSInformatica.NTSCheckBox
    Me.ckAn_scaden = New NTSInformatica.NTSCheckBox
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbAn_contropLabel = New NTSInformatica.NTSLabel
    Me.edAn_controp = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_accperi = New NTSInformatica.NTSLabel
    Me.cbAn_accperi = New NTSInformatica.NTSComboBox
    Me.lbAn_note = New NTSInformatica.NTSLabel
    Me.edAn_note = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPag4 = New NTSInformatica.NTSPanel
    Me.edAn_note2 = New NTSInformatica.NTSMemoBox
    Me.lbAn_contoLabel = New NTSInformatica.NTSLabel
    Me.edAn_descr1 = New NTSInformatica.NTSTextBoxStr
    Me.lbAn_codmastLabel = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbAn_conto = New NTSInformatica.NTSLabel
    Me.lbAn_codmast = New NTSInformatica.NTSLabel
    Me.lbAn_descr1 = New NTSInformatica.NTSLabel
    Me.pnMain = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsSotc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSotc.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRiclassificati.SuspendLayout()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_ivainded.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_pcconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmComportamento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmComportamento.SuspendLayout()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAn_accperi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4.SuspendLayout()
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbCambiaDitta, Me.tlbApri, Me.tlbPartitario, Me.tlbAnacent, Me.tlbAnalink, Me.tlbAnacent2, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 35
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
    Me.tlbStrumenti.Id = 31
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 32
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 33
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 34
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
    'tsSotc
    '
    Me.tsSotc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSotc.Location = New System.Drawing.Point(0, 0)
    Me.tsSotc.Name = "tsSotc"
    Me.tsSotc.SelectedTabPage = Me.NtsTabPage1
    Me.tsSotc.Size = New System.Drawing.Size(670, 263)
    Me.tsSotc.TabIndex = 4
    Me.tsSotc.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage4})
    Me.tsSotc.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(661, 233)
    Me.NtsTabPage1.Text = "&1- Generale"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.fmRiclassificati)
    Me.pnPag1.Controls.Add(Me.cbAn_colbil)
    Me.pnPag1.Controls.Add(Me.lbAn_colbil)
    Me.pnPag1.Controls.Add(Me.lbAn_ivainded)
    Me.pnPag1.Controls.Add(Me.ckAn_ivainded)
    Me.pnPag1.Controls.Add(Me.edAn_pcconto)
    Me.pnPag1.Controls.Add(Me.lbAn_pccontoLabel)
    Me.pnPag1.Controls.Add(Me.fmComportamento)
    Me.pnPag1.Controls.Add(Me.lbXx_controp)
    Me.pnPag1.Controls.Add(Me.lbAn_contropLabel)
    Me.pnPag1.Controls.Add(Me.edAn_controp)
    Me.pnPag1.Controls.Add(Me.lbAn_accperi)
    Me.pnPag1.Controls.Add(Me.cbAn_accperi)
    Me.pnPag1.Controls.Add(Me.lbAn_note)
    Me.pnPag1.Controls.Add(Me.edAn_note)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(661, 233)
    Me.pnPag1.TabIndex = 526
    Me.pnPag1.Text = "NtsPanel2"
    '
    'fmRiclassificati
    '
    Me.fmRiclassificati.AllowDrop = True
    Me.fmRiclassificati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRiclassificati.Appearance.Options.UseBackColor = True
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
    Me.fmRiclassificati.Location = New System.Drawing.Point(219, 115)
    Me.fmRiclassificati.Name = "fmRiclassificati"
    Me.fmRiclassificati.Size = New System.Drawing.Size(436, 86)
    Me.fmRiclassificati.TabIndex = 594
    Me.fmRiclassificati.Text = "Riclassificati su Excel"
    '
    'lbRiclassif
    '
    Me.lbRiclassif.AutoSize = True
    Me.lbRiclassif.BackColor = System.Drawing.Color.Transparent
    Me.lbRiclassif.Location = New System.Drawing.Point(334, 22)
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
    Me.edAn_kpccee2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAn_kpccee2.Location = New System.Drawing.Point(80, 59)
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
    Me.lbAn_kpccee2.Location = New System.Drawing.Point(7, 62)
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
    Me.lbAn_kpccee.Location = New System.Drawing.Point(7, 40)
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
    Me.lbAn_rifrica.Location = New System.Drawing.Point(250, 62)
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
    Me.edAn_rifrica.Location = New System.Drawing.Point(319, 59)
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
    Me.edAn_rifricd.Location = New System.Drawing.Point(319, 37)
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
    Me.lbAn_rifricd.Location = New System.Drawing.Point(250, 40)
    Me.lbAn_rifricd.Name = "lbAn_rifricd"
    Me.lbAn_rifricd.NTSDbField = ""
    Me.lbAn_rifricd.Size = New System.Drawing.Size(59, 13)
    Me.lbAn_rifricd.TabIndex = 534
    Me.lbAn_rifricd.Text = "Saldo Dare"
    Me.lbAn_rifricd.Tooltip = ""
    Me.lbAn_rifricd.UseMnemonic = False
    '
    'cbAn_colbil
    '
    Me.cbAn_colbil.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAn_colbil.DataSource = Nothing
    Me.cbAn_colbil.DisplayMember = ""
    Me.cbAn_colbil.Location = New System.Drawing.Point(268, 205)
    Me.cbAn_colbil.Name = "cbAn_colbil"
    Me.cbAn_colbil.NTSDbField = ""
    Me.cbAn_colbil.Properties.AutoHeight = False
    Me.cbAn_colbil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAn_colbil.Properties.DropDownRows = 30
    Me.cbAn_colbil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAn_colbil.SelectedValue = ""
    Me.cbAn_colbil.Size = New System.Drawing.Size(178, 20)
    Me.cbAn_colbil.TabIndex = 593
    Me.cbAn_colbil.ValueMember = ""
    '
    'lbAn_colbil
    '
    Me.lbAn_colbil.AutoSize = True
    Me.lbAn_colbil.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_colbil.Location = New System.Drawing.Point(10, 208)
    Me.lbAn_colbil.Name = "lbAn_colbil"
    Me.lbAn_colbil.NTSDbField = ""
    Me.lbAn_colbil.Size = New System.Drawing.Size(244, 13)
    Me.lbAn_colbil.TabIndex = 592
    Me.lbAn_colbil.Text = "Colonna in stampa bilancio a sezioni contrapposte"
    Me.lbAn_colbil.Tooltip = ""
    Me.lbAn_colbil.UseMnemonic = False
    '
    'lbAn_ivainded
    '
    Me.lbAn_ivainded.AutoSize = True
    Me.lbAn_ivainded.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_ivainded.Location = New System.Drawing.Point(10, 73)
    Me.lbAn_ivainded.Name = "lbAn_ivainded"
    Me.lbAn_ivainded.NTSDbField = ""
    Me.lbAn_ivainded.Size = New System.Drawing.Size(123, 13)
    Me.lbAn_ivainded.TabIndex = 591
    Me.lbAn_ivainded.Text = "Gestione Iva indetraibile"
    Me.lbAn_ivainded.Tooltip = ""
    Me.lbAn_ivainded.UseMnemonic = False
    '
    'ckAn_ivainded
    '
    Me.ckAn_ivainded.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_ivainded.Location = New System.Drawing.Point(143, 71)
    Me.ckAn_ivainded.Name = "ckAn_ivainded"
    Me.ckAn_ivainded.NTSCheckValue = "S"
    Me.ckAn_ivainded.NTSUnCheckValue = "N"
    Me.ckAn_ivainded.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_ivainded.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_ivainded.Properties.AutoHeight = False
    Me.ckAn_ivainded.Properties.Caption = "(se spuntato il conto può ricevere il giroconto IVA indetraibile in automatico da" & _
        " prima nota)"
    Me.ckAn_ivainded.Size = New System.Drawing.Size(469, 19)
    Me.ckAn_ivainded.TabIndex = 590
    '
    'edAn_pcconto
    '
    Me.edAn_pcconto.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAn_pcconto.EditValue = "0"
    Me.edAn_pcconto.Enabled = False
    Me.edAn_pcconto.Location = New System.Drawing.Point(143, 3)
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
    Me.lbAn_pccontoLabel.Location = New System.Drawing.Point(10, 6)
    Me.lbAn_pccontoLabel.Name = "lbAn_pccontoLabel"
    Me.lbAn_pccontoLabel.NTSDbField = ""
    Me.lbAn_pccontoLabel.Size = New System.Drawing.Size(110, 13)
    Me.lbAn_pccontoLabel.TabIndex = 582
    Me.lbAn_pccontoLabel.Text = "Conto Piano dei Conti"
    Me.lbAn_pccontoLabel.Tooltip = ""
    Me.lbAn_pccontoLabel.UseMnemonic = False
    '
    'fmComportamento
    '
    Me.fmComportamento.AllowDrop = True
    Me.fmComportamento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmComportamento.Appearance.Options.UseBackColor = True
    Me.fmComportamento.Controls.Add(Me.ckAn_partite)
    Me.fmComportamento.Controls.Add(Me.ckAn_scaden)
    Me.fmComportamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmComportamento.Location = New System.Drawing.Point(13, 115)
    Me.fmComportamento.Name = "fmComportamento"
    Me.fmComportamento.Size = New System.Drawing.Size(189, 68)
    Me.fmComportamento.TabIndex = 580
    Me.fmComportamento.Text = "Comportamento"
    '
    'ckAn_partite
    '
    Me.ckAn_partite.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_partite.Location = New System.Drawing.Point(14, 22)
    Me.ckAn_partite.Name = "ckAn_partite"
    Me.ckAn_partite.NTSCheckValue = "S"
    Me.ckAn_partite.NTSUnCheckValue = "N"
    Me.ckAn_partite.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAn_partite.Properties.Appearance.Options.UseBackColor = True
    Me.ckAn_partite.Properties.AutoHeight = False
    Me.ckAn_partite.Properties.Caption = "Gestione &partite"
    Me.ckAn_partite.Size = New System.Drawing.Size(106, 19)
    Me.ckAn_partite.TabIndex = 549
    '
    'ckAn_scaden
    '
    Me.ckAn_scaden.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAn_scaden.Location = New System.Drawing.Point(14, 44)
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
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(249, 25)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(406, 20)
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
    Me.lbAn_contropLabel.Location = New System.Drawing.Point(10, 28)
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
    Me.edAn_controp.Location = New System.Drawing.Point(143, 25)
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
    Me.lbAn_accperi.Location = New System.Drawing.Point(11, 50)
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
    Me.cbAn_accperi.Location = New System.Drawing.Point(143, 47)
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
    Me.lbAn_note.Location = New System.Drawing.Point(10, 96)
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
    Me.edAn_note.Location = New System.Drawing.Point(143, 93)
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
    Me.edAn_note.Size = New System.Drawing.Size(512, 20)
    Me.edAn_note.TabIndex = 552
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPag4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(661, 233)
    Me.NtsTabPage4.Text = "&2 - Note"
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
    Me.pnPag4.Size = New System.Drawing.Size(661, 233)
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
    Me.edAn_note2.Size = New System.Drawing.Size(661, 233)
    Me.edAn_note2.TabIndex = 554
    '
    'lbAn_contoLabel
    '
    Me.lbAn_contoLabel.AutoSize = True
    Me.lbAn_contoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_contoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAn_contoLabel.Location = New System.Drawing.Point(13, 32)
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
    Me.edAn_descr1.Location = New System.Drawing.Point(280, 6)
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
    Me.edAn_descr1.Size = New System.Drawing.Size(378, 20)
    Me.edAn_descr1.TabIndex = 501
    '
    'lbAn_codmastLabel
    '
    Me.lbAn_codmastLabel.AutoSize = True
    Me.lbAn_codmastLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_codmastLabel.Location = New System.Drawing.Point(13, 10)
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
    Me.pnTop.Controls.Add(Me.lbAn_conto)
    Me.pnTop.Controls.Add(Me.lbAn_codmast)
    Me.pnTop.Controls.Add(Me.edAn_descr1)
    Me.pnTop.Controls.Add(Me.lbAn_descr1)
    Me.pnTop.Controls.Add(Me.lbAn_contoLabel)
    Me.pnTop.Controls.Add(Me.lbAn_codmastLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(670, 53)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "pnTop"
    '
    'lbAn_conto
    '
    Me.lbAn_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_conto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAn_conto.Location = New System.Drawing.Point(94, 28)
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
    Me.lbAn_codmast.Location = New System.Drawing.Point(94, 6)
    Me.lbAn_codmast.Name = "lbAn_codmast"
    Me.lbAn_codmast.NTSDbField = ""
    Me.lbAn_codmast.Size = New System.Drawing.Size(100, 20)
    Me.lbAn_codmast.TabIndex = 509
    Me.lbAn_codmast.Text = "0"
    Me.lbAn_codmast.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbAn_codmast.Tooltip = ""
    Me.lbAn_codmast.UseMnemonic = False
    '
    'lbAn_descr1
    '
    Me.lbAn_descr1.AutoSize = True
    Me.lbAn_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr1.Location = New System.Drawing.Point(213, 10)
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
    Me.pnMain.Location = New System.Drawing.Point(0, 83)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(670, 263)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel2"
    '
    'FRM__SOTC_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(670, 346)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__SOTC_F"
    Me.Text = "SOTTOCONTI DITTA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsSotc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSotc.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    Me.pnPag1.PerformLayout()
    CType(Me.fmRiclassificati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRiclassificati.ResumeLayout(False)
    Me.fmRiclassificati.PerformLayout()
    CType(Me.edAn_kpccee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_kpccee2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifrica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_rifricd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_colbil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_ivainded.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_pcconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmComportamento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmComportamento.ResumeLayout(False)
    Me.fmComportamento.PerformLayout()
    CType(Me.ckAn_partite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAn_scaden.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAn_accperi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4.ResumeLayout(False)
    CType(Me.edAn_note2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAn_descr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
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
        tlbPartitario.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAn_descr1.NTSSetParam(oMenu, oApp.Tr(Me, 128326960191176000, "Descrizione 1"), 30, False)
      edAn_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960229616000, "Controp. ratei/risconti"), tabanagras)
      ckAn_partite.NTSSetParam(oMenu, oApp.Tr(Me, 128326960243166000, "Gestione partite"), "S", "N")
      ckAn_scaden.NTSSetParam(oMenu, oApp.Tr(Me, 128326960257806000, "Gestione scadenze"), "S", "N")
      edAn_pcconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128326960282606000, "Conto PDC"), tabanagpc)
      cbAn_accperi.NTSSetParam(oApp.Tr(Me, 128326960301886000, "Richiedi date"))
      edAn_note.NTSSetParam(oMenu, oApp.Tr(Me, 128326960318846000, "Note brevi"), 30, True)
      edAn_kpccee.NTSSetParam(oMenu, oApp.Tr(Me, 128326960331886000, "Saldo dare CEE"), 8, True)
      edAn_kpccee2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960345646000, "Saldo avere CEE"), 8, True)
      edAn_rifricd.NTSSetParam(oMenu, oApp.Tr(Me, 128326960360916000, "Sado dare RICLASS."), 8, True)
      edAn_rifrica.NTSSetParam(oMenu, oApp.Tr(Me, 128326960384356000, "Saldo avere RICLASS"), 8, True)
      cbAn_colbil.NTSSetParam(oApp.Tr(Me, 128326960496516000, "Colonna in stampa bilancio a sez. contrapposte"))
      edAn_note2.NTSSetParam(oMenu, oApp.Tr(Me, 128326960718506000, "Note"), 0)
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
      cbAn_colbil.NTSDbField = "ANAGRA.an_colbil"
      edAn_pcconto.NTSDbField = "ANAGRA.an_pcconto"
      lbXx_controp.NTSDbField = "ANAGRA.xx_controp"
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
      End If

      '----------------------------------------------
      'blocco tutto se è un pdc standard e l'utnete non è NTS / NTS
      If oCleSotc.bAllowOperation = False Then
        'edAn_descr1.Properties.ReadOnly = True
        edAn_controp.Enabled = False
        cbAn_accperi.Enabled = False
        edAn_note.Enabled = False
        fmComportamento.Enabled = False
        edAn_rifricd.Enabled = False
        edAn_rifrica.Enabled = False
        edAn_kpccee.Enabled = False
        edAn_kpccee2.Enabled = False
        pnPag4.Enabled = False
      Else
        'edAn_descr1.Properties.ReadOnly = False
        GctlSetVisEnab(edAn_controp, False)
        GctlSetVisEnab(cbAn_accperi, False)
        GctlSetVisEnab(edAn_note, False)
        GctlSetVisEnab(fmComportamento, False)
        GctlSetVisEnab(edAn_rifricd, False)
        GctlSetVisEnab(edAn_rifrica, False)
        GctlSetVisEnab(edAn_kpccee, False)
        GctlSetVisEnab(edAn_kpccee2, False)
        GctlSetVisEnab(pnPag4, False)
      End If

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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim nPjob As Object

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{tabanaz.codditt} = " & CStrSQL(DittaCorrente)
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