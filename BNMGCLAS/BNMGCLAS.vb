Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRMMGCLAS

  Public strCodcla1AssTmp As String = ""
  Public strCodcla2AssTmp As String = ""
  Public strCodcla3AssTmp As String = ""
  Public strCodcla4AssTmp As String = ""
  Public strCodcla5AssTmp As String = ""

#Region "Moduli"
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
#End Region

#Region "Variabili"
  Public oCleClas As CLEMGCLAS
  Public oCallParams As CLE__CLDP
  Public dsClas As DataSet
  Public dttClas As DataTable
  Public dcClas As BindingSource = New BindingSource()

  Public dsArti As DataSet
  Public dcArti As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents trClas As NTSInformatica.NTSTreeView
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents ImageList1 As System.Windows.Forms.ImageList
  Public WithEvents tlbCancellaCartella As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbGifView As NTSInformatica.NTSBarMenuItem
  Public WithEvents acl_codcla As NTSInformatica.NTSGridColumn
  Public WithEvents acl_descla As NTSInformatica.NTSGridColumn
  Public WithEvents acl_note As NTSInformatica.NTSGridColumn
  Public WithEvents acl_gif As NTSInformatica.NTSGridColumn
#End Region

#Region "Inizializzazione"
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCLAS))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbDesAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbRicarica = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbDesLingua = New NTSInformatica.NTSBarMenuItem
    Me.tlbRiordinaAlbero = New NTSInformatica.NTSBarButtonItem
    Me.tlbGifView = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbCancellaCartella = New NTSInformatica.NTSBarMenuItem
    Me.trClas = New NTSInformatica.NTSTreeView
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.tsClas = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.grClas = New NTSInformatica.NTSGrid
    Me.grvClas = New NTSInformatica.NTSGridView
    Me.acl_ordin = New NTSInformatica.NTSGridColumn
    Me.acl_codcla = New NTSInformatica.NTSGridColumn
    Me.acl_descla = New NTSInformatica.NTSGridColumn
    Me.acl_gif = New NTSInformatica.NTSGridColumn
    Me.acl_note = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.NtsPanel2 = New NTSInformatica.NTSPanel
    Me.grArti = New NTSInformatica.NTSGrid
    Me.grvArti = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_desint = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.tsClas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsClas.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.grClas, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvClas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel2.SuspendLayout()
    CType(Me.grArti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvArti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbCancellaCartella, Me.tlbStrumenti, Me.tlbGifView, Me.tlbZoom, Me.tlbSelAll, Me.tlbDesAll, Me.tlbRicarica, Me.tlbDesLingua, Me.tlbRiordinaAlbero})
    Me.NtsBarManager1.MaxItemId = 45
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelAll, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDesAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicarica, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 39
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    'tlbSelAll
    '
    Me.tlbSelAll.Caption = "Seleziona tutte le righe"
    Me.tlbSelAll.Glyph = CType(resources.GetObject("tlbSelAll.Glyph"), System.Drawing.Image)
    Me.tlbSelAll.GlyphPath = ""
    Me.tlbSelAll.Id = 40
    Me.tlbSelAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbSelAll.Name = "tlbSelAll"
    Me.tlbSelAll.Visible = True
    '
    'tlbDesAll
    '
    Me.tlbDesAll.Caption = "Deseleziona tutte le righe"
    Me.tlbDesAll.Glyph = CType(resources.GetObject("tlbDesAll.Glyph"), System.Drawing.Image)
    Me.tlbDesAll.GlyphPath = ""
    Me.tlbDesAll.Id = 41
    Me.tlbDesAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDesAll.Name = "tlbDesAll"
    Me.tlbDesAll.Visible = True
    '
    'tlbRicarica
    '
    Me.tlbRicarica.Caption = "Ricarica associazione articoli"
    Me.tlbRicarica.Glyph = CType(resources.GetObject("tlbRicarica.Glyph"), System.Drawing.Image)
    Me.tlbRicarica.GlyphPath = ""
    Me.tlbRicarica.Id = 42
    Me.tlbRicarica.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbRicarica.Name = "tlbRicarica"
    Me.tlbRicarica.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 36
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDesLingua), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiordinaAlbero), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGifView, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbDesLingua
    '
    Me.tlbDesLingua.Caption = "Descrizioni in lingua"
    Me.tlbDesLingua.GlyphPath = ""
    Me.tlbDesLingua.Id = 43
    Me.tlbDesLingua.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDesLingua.Name = "tlbDesLingua"
    Me.tlbDesLingua.NTSIsCheckBox = False
    Me.tlbDesLingua.Visible = True
    '
    'tlbRiordinaAlbero
    '
    Me.tlbRiordinaAlbero.Caption = "Riordina albero"
    Me.tlbRiordinaAlbero.GlyphPath = ""
    Me.tlbRiordinaAlbero.Id = 44
    Me.tlbRiordinaAlbero.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbRiordinaAlbero.Name = "tlbRiordinaAlbero"
    Me.tlbRiordinaAlbero.Visible = True
    '
    'tlbGifView
    '
    Me.tlbGifView.Caption = "Visualizza immagine"
    Me.tlbGifView.GlyphPath = ""
    Me.tlbGifView.Id = 38
    Me.tlbGifView.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbGifView.Name = "tlbGifView"
    Me.tlbGifView.NTSIsCheckBox = False
    Me.tlbGifView.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbCancellaCartella
    '
    Me.tlbCancellaCartella.Caption = "Cancella cartella"
    Me.tlbCancellaCartella.GlyphPath = ""
    Me.tlbCancellaCartella.Id = 20
    Me.tlbCancellaCartella.Name = "tlbCancellaCartella"
    Me.tlbCancellaCartella.NTSIsCheckBox = False
    Me.tlbCancellaCartella.Visible = True
    '
    'trClas
    '
    Me.trClas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.trClas.Location = New System.Drawing.Point(0, 0)
    Me.trClas.Name = "trClas"
    Me.trClas.Size = New System.Drawing.Size(257, 400)
    Me.trClas.TabIndex = 0
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.pnRight)
    Me.pnGrid.Controls.Add(Me.pnLeft)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 30)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(671, 400)
    Me.pnGrid.TabIndex = 8
    Me.pnGrid.Text = "NtsPanel2"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.tsClas)
    Me.pnRight.Controls.Add(Me.pnTop)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnRight.Location = New System.Drawing.Point(257, 0)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(414, 400)
    Me.pnRight.TabIndex = 7
    '
    'tsClas
    '
    Me.tsClas.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsClas.Location = New System.Drawing.Point(0, 23)
    Me.tsClas.Name = "tsClas"
    Me.tsClas.SelectedTabPage = Me.NtsTabPage1
    Me.tsClas.Size = New System.Drawing.Size(414, 377)
    Me.tsClas.TabIndex = 90
    Me.tsClas.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsClas.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.NtsPanel1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(405, 347)
    Me.NtsTabPage1.Text = "&1- Classificazioni"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.grClas)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(405, 347)
    Me.NtsPanel1.TabIndex = 54
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'grClas
    '
    Me.grClas.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grClas.EmbeddedNavigator.Name = ""
    Me.grClas.Location = New System.Drawing.Point(0, 0)
    Me.grClas.MainView = Me.grvClas
    Me.grClas.Name = "grClas"
    Me.grClas.Size = New System.Drawing.Size(405, 347)
    Me.grClas.TabIndex = 5
    Me.grClas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvClas})
    Me.grClas.Visible = False
    '
    'grvClas
    '
    Me.grvClas.ActiveFilterEnabled = False
    Me.grvClas.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.acl_ordin, Me.acl_codcla, Me.acl_descla, Me.acl_gif, Me.acl_note})
    Me.grvClas.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvClas.Enabled = True
    Me.grvClas.GridControl = Me.grClas
    Me.grvClas.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvClas.MinRowHeight = 14
    Me.grvClas.Name = "grvClas"
    Me.grvClas.NTSAllowDelete = True
    Me.grvClas.NTSAllowInsert = True
    Me.grvClas.NTSAllowUpdate = True
    Me.grvClas.NTSMenuContext = Nothing
    Me.grvClas.OptionsCustomization.AllowRowSizing = True
    Me.grvClas.OptionsFilter.AllowFilterEditor = False
    Me.grvClas.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvClas.OptionsNavigation.UseTabKey = False
    Me.grvClas.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvClas.OptionsView.ColumnAutoWidth = False
    Me.grvClas.OptionsView.EnableAppearanceEvenRow = True
    Me.grvClas.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvClas.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvClas.OptionsView.ShowGroupPanel = False
    Me.grvClas.RowHeight = 16
    '
    'acl_ordin
    '
    Me.acl_ordin.AppearanceCell.Options.UseBackColor = True
    Me.acl_ordin.AppearanceCell.Options.UseTextOptions = True
    Me.acl_ordin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acl_ordin.Caption = "Ordin."
    Me.acl_ordin.Enabled = True
    Me.acl_ordin.FieldName = "acl_ordin"
    Me.acl_ordin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acl_ordin.Name = "acl_ordin"
    Me.acl_ordin.NTSRepositoryComboBox = Nothing
    Me.acl_ordin.NTSRepositoryItemCheck = Nothing
    Me.acl_ordin.NTSRepositoryItemMemo = Nothing
    Me.acl_ordin.NTSRepositoryItemText = Nothing
    Me.acl_ordin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acl_ordin.OptionsFilter.AllowFilter = False
    Me.acl_ordin.Visible = True
    Me.acl_ordin.VisibleIndex = 0
    '
    'acl_codcla
    '
    Me.acl_codcla.AppearanceCell.Options.UseBackColor = True
    Me.acl_codcla.AppearanceCell.Options.UseTextOptions = True
    Me.acl_codcla.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acl_codcla.Caption = "Codice"
    Me.acl_codcla.Enabled = True
    Me.acl_codcla.FieldName = "acl_codcla"
    Me.acl_codcla.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acl_codcla.Name = "acl_codcla"
    Me.acl_codcla.NTSRepositoryComboBox = Nothing
    Me.acl_codcla.NTSRepositoryItemCheck = Nothing
    Me.acl_codcla.NTSRepositoryItemMemo = Nothing
    Me.acl_codcla.NTSRepositoryItemText = Nothing
    Me.acl_codcla.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acl_codcla.OptionsFilter.AllowFilter = False
    Me.acl_codcla.Visible = True
    Me.acl_codcla.VisibleIndex = 1
    Me.acl_codcla.Width = 90
    '
    'acl_descla
    '
    Me.acl_descla.AppearanceCell.Options.UseBackColor = True
    Me.acl_descla.AppearanceCell.Options.UseTextOptions = True
    Me.acl_descla.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acl_descla.Caption = "Descrizione"
    Me.acl_descla.Enabled = True
    Me.acl_descla.FieldName = "acl_descla"
    Me.acl_descla.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acl_descla.Name = "acl_descla"
    Me.acl_descla.NTSRepositoryComboBox = Nothing
    Me.acl_descla.NTSRepositoryItemCheck = Nothing
    Me.acl_descla.NTSRepositoryItemMemo = Nothing
    Me.acl_descla.NTSRepositoryItemText = Nothing
    Me.acl_descla.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acl_descla.OptionsFilter.AllowFilter = False
    Me.acl_descla.Visible = True
    Me.acl_descla.VisibleIndex = 2
    Me.acl_descla.Width = 100
    '
    'acl_gif
    '
    Me.acl_gif.AppearanceCell.Options.UseBackColor = True
    Me.acl_gif.AppearanceCell.Options.UseTextOptions = True
    Me.acl_gif.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acl_gif.Caption = "Immagine"
    Me.acl_gif.Enabled = True
    Me.acl_gif.FieldName = "acl_gif"
    Me.acl_gif.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acl_gif.Name = "acl_gif"
    Me.acl_gif.NTSRepositoryComboBox = Nothing
    Me.acl_gif.NTSRepositoryItemCheck = Nothing
    Me.acl_gif.NTSRepositoryItemMemo = Nothing
    Me.acl_gif.NTSRepositoryItemText = Nothing
    Me.acl_gif.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acl_gif.OptionsFilter.AllowFilter = False
    Me.acl_gif.Visible = True
    Me.acl_gif.VisibleIndex = 3
    '
    'acl_note
    '
    Me.acl_note.AppearanceCell.Options.UseBackColor = True
    Me.acl_note.AppearanceCell.Options.UseTextOptions = True
    Me.acl_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acl_note.Caption = "Note"
    Me.acl_note.Enabled = True
    Me.acl_note.FieldName = "acl_note"
    Me.acl_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acl_note.Name = "acl_note"
    Me.acl_note.NTSRepositoryComboBox = Nothing
    Me.acl_note.NTSRepositoryItemCheck = Nothing
    Me.acl_note.NTSRepositoryItemMemo = Nothing
    Me.acl_note.NTSRepositoryItemText = Nothing
    Me.acl_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acl_note.OptionsFilter.AllowFilter = False
    Me.acl_note.Visible = True
    Me.acl_note.VisibleIndex = 4
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.NtsPanel2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(405, 347)
    Me.NtsTabPage2.Text = "&2 - Articoli associati"
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel2.Appearance.Options.UseBackColor = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.grArti)
    Me.NtsPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel2.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSActiveTrasparency = True
    Me.NtsPanel2.Size = New System.Drawing.Size(405, 347)
    Me.NtsPanel2.TabIndex = 0
    Me.NtsPanel2.Text = "NtsPanel1"
    '
    'grArti
    '
    Me.grArti.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grArti.EmbeddedNavigator.Name = ""
    Me.grArti.Location = New System.Drawing.Point(0, 0)
    Me.grArti.MainView = Me.grvArti
    Me.grArti.Name = "grArti"
    Me.grArti.Size = New System.Drawing.Size(405, 347)
    Me.grArti.TabIndex = 6
    Me.grArti.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvArti})
    '
    'grvArti
    '
    Me.grvArti.ActiveFilterEnabled = False
    Me.grvArti.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.ar_codart, Me.ar_descr, Me.ar_desint, Me.codditt})
    Me.grvArti.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvArti.Enabled = True
    Me.grvArti.GridControl = Me.grArti
    Me.grvArti.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvArti.MinRowHeight = 14
    Me.grvArti.Name = "grvArti"
    Me.grvArti.NTSAllowDelete = True
    Me.grvArti.NTSAllowInsert = True
    Me.grvArti.NTSAllowUpdate = True
    Me.grvArti.NTSMenuContext = Nothing
    Me.grvArti.OptionsCustomization.AllowRowSizing = True
    Me.grvArti.OptionsFilter.AllowFilterEditor = False
    Me.grvArti.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvArti.OptionsNavigation.UseTabKey = False
    Me.grvArti.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvArti.OptionsView.ColumnAutoWidth = False
    Me.grvArti.OptionsView.EnableAppearanceEvenRow = True
    Me.grvArti.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvArti.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvArti.OptionsView.ShowGroupPanel = False
    Me.grvArti.RowHeight = 16
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    Me.xx_seleziona.Width = 57
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Articolo"
    Me.ar_codart.Enabled = True
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 1
    Me.ar_codart.Width = 120
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione 1"
    Me.ar_descr.Enabled = False
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowEdit = False
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsColumn.ReadOnly = True
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 2
    Me.ar_descr.Width = 146
    '
    'ar_desint
    '
    Me.ar_desint.AppearanceCell.Options.UseBackColor = True
    Me.ar_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ar_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_desint.Caption = "Descrizione 2"
    Me.ar_desint.Enabled = False
    Me.ar_desint.FieldName = "ar_desint"
    Me.ar_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_desint.Name = "ar_desint"
    Me.ar_desint.NTSRepositoryComboBox = Nothing
    Me.ar_desint.NTSRepositoryItemCheck = Nothing
    Me.ar_desint.NTSRepositoryItemMemo = Nothing
    Me.ar_desint.NTSRepositoryItemText = Nothing
    Me.ar_desint.OptionsColumn.AllowEdit = False
    Me.ar_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_desint.OptionsColumn.ReadOnly = True
    Me.ar_desint.OptionsFilter.AllowFilter = False
    Me.ar_desint.Visible = True
    Me.ar_desint.VisibleIndex = 3
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbDescr)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(414, 23)
    Me.pnTop.TabIndex = 95
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDescr.Location = New System.Drawing.Point(6, 5)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(28, 13)
    Me.lbDescr.TabIndex = 116
    Me.lbDescr.Text = "XXX"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.trClas)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnLeft.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(257, 400)
    Me.pnLeft.TabIndex = 6
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'FRMMGCLAS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(671, 430)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGCLAS"
    Me.NTSLastControlFocussed = Me.grClas
    Me.Text = "CLASSIFICAZIONE ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.tsClas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsClas.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    CType(Me.grClas, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvClas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel2.ResumeLayout(False)
    CType(Me.grArti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvArti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGCLAS", "BEMGCLAS", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128342559569758000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleClas = CType(oTmp, CLEMGCLAS)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGCLAS", strRemoteServer, strRemotePort)
    AddHandler oCleClas.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleClas.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim dttTipo As New DataTable
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbSelAll.GlyphPath = (oApp.ChildImageDir & "\add_filter.gif")
        tlbDesAll.GlyphPath = (oApp.ChildImageDir & "\del_filter.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRicarica.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvClas.NTSSetParam(oMenu, oApp.Tr(Me, 128792746611394093, "Classificazione articoli"))
      acl_ordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129113890521718070, "Ordinamento"), "0", 11, -2147483648, 2147483648)
      acl_codcla.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129113890521718069, "Codice"), 5, False)
      acl_descla.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129533979611777542, "Descrizione"), 50, False)
      acl_gif.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130223270086900969, "Immagine"), 50, True)
      acl_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130223270110217210, "Note"), 0, True, True)
      acl_gif.NTSForzaVisZoom = True
      acl_codcla.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      grvArti.NTSSetParam(oMenu, oApp.Tr(Me, 130421966808783099, "Articoli associati"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130421967039055053, "Ditta"), 0, False)
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130421967674295100, "seleziona"), "S", "N")
      ar_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 130421967816003163, "Articolo"), tabartico, False)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130421968097612665, "Descrizione 1"), 0, True)
      ar_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130421968244379289, "Descrizione 2"), 0, True)
      ar_codart.NTSSetRichiesto()
      ar_codart.NTSForzaVisZoom = True
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGCLAS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      Try
        ' Crea la lista delle immagini
        ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
        ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeviewsel.gif"))
        trClas.ImageList = ImageList1
        trClas.ImageIndex = 0
        trClas.SelectedImageIndex = 1
      Catch ex As Exception
        'se le immagini non ci sono non do errore
      End Try

      oCleClas.strDittaCorrente = DittaCorrente

      If Not CreaTreeview() Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      tsClas.SelectedTabPageIndex = 0
      StatoPulsanti()
      '--------------------------------------------------------------------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGCLAS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGCLAS_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dsClas.Dispose()
      dcClas.Dispose()
      dsArti.Dispose()
      dcArti.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If (grvClas.Focused = False) And (grvArti.Focused = False) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If grvClas.Focused = True Then
        If grvClas.FocusedColumn.Name = "acl_gif" Then ApriGif()
      Else
        '--------------------------------------------------------------------------------------------------------------
        If grvArti.FocusedColumn.Equals(ar_codart) = True Then
          NTSZOOM.strIn = NTSCStr(grvArti.EditingValue)
          oParam.bTipoProposto = False '--- Zoom multiselezione
          oParam.strAlfpar = "BNMGCLAS"
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn = "*" Then
            '----------------------------------------------------------------------------------------------------------
            If oParam.oParam Is Nothing Then Return
            '----------------------------------------------------------------------------------------------------------
            If CType(oParam.oParam, DataTable).Rows.Count = 0 Then Return
            '----------------------------------------------------------------------------------------------------------
            Me.Cursor = Cursors.WaitCursor
            Me.Refresh()
            '----------------------------------------------------------------------------------------------------------
            If oCleClas.AssociaArticoli(CType(oParam.oParam, DataTable)) = False Then Return
            '----------------------------------------------------------------------------------------------------------
            CaricaGrigliaArticoli()
            '----------------------------------------------------------------------------------------------------------
            Me.Cursor = Cursors.Default
            Me.Refresh()
            '----------------------------------------------------------------------------------------------------------
            oApp.MsgBoxInfo(oApp.Tr(Me, 130422132502498895, "Associazione articoli terminata."))
            '----------------------------------------------------------------------------------------------------------        
          Else
            If NTSZOOM.strIn <> NTSCStr(grvArti.EditingValue) Then grvArti.SetFocusedValue(NTSZOOM.strIn)
          End If
        Else
          NTSCallStandardZoom()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      Me.Refresh()
    End Try
  End Sub

  Public Overridable Sub tlbSelAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelAll.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDesAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDesAll.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRicarica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicarica.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      CaricaGrigliaArticoli()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If grClas.Visible = False Then Return
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim i As Integer = 0
    Dim dtrCurrent As DataRow = Nothing
    Try
      If trClas.Focused = True Then
        If trClas.SelectedNode.Name <> ".BASE." Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130725254433402456, "Attenzione! " & vbCrLf & _
            "Cancellando IL NODO SU CUI SI E' POSIZIONATI, sarà eliminata anche la sua struttura sottostante!" & vbCrLf & "Procedere?")) = Windows.Forms.DialogResult.No Then Return
          oCleClas.EliminaNodo(trClas.SelectedNode.Tag.ToString)
          trClas.SelectedNode.Remove()
          trClas.SelectedNode = trClas.Nodes(0)
          Return
        End If
      End If

      If oCleClas.bGrigliaArticoli = False Then
        If grClas.Visible = False Then Return

        dtrCurrent = grvClas.NTSGetCurrentDataRow
        If dtrCurrent IsNot Nothing Then
          Select Case dtrCurrent.RowState
            Case DataRowState.Added
              tlbRipristina_ItemClick(Me, Nothing)
            Case Else
              If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128793335641960769, "Attenzione! " & vbCrLf & _
                "Cancellando LA RIGA DI GRIGLIA SU CUI SI E' POSIZIONATI verrà eliminata anche la sua struttura sottostante!" & vbCrLf & "Procedere?")) = Windows.Forms.DialogResult.No Then Return
              Dim strCodice As String = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
              If Not grvClas.NTSDeleteRigaCorrente(dcClas, False) Then Return
              If oCleClas.TestPreCancella() Then
                Dim nodeFnd() As TreeNode = trClas.Nodes.Find(oCleClas.nLivello & "_" & strCodice, True)

                oCleClas.EliminaNodo(nodeFnd(0).Tag.ToString)
                trClas.SelectedNode.Nodes(oCleClas.nLivello & "_" & strCodice).Remove()
              Else
                oApp.MsgBoxErr(oApp.Tr(Me, 128874539872802148, "Impossibile eliminare classificazioni che risultano utilizzate. Operazione annullata"))
                dsClas.RejectChanges()
              End If
          End Select
        End If
      Else
        If dsArti.Tables("ARTICO").Select("xx_seleziona = 'S'").Length = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130422104190618394, "Attenzione!" & vbCrLf & _
            "Non esistono righe selezionate." & vbCrLf & _
            "Cancellazione associazione articoli non possibile."))
          Return
        End If
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130422104526269685, _
          "Confermare la cancellazione dell'associazione sugli articoli raltiva alla classificazione corrente?")) = Windows.Forms.DialogResult.No Then Return
        For i = (dsArti.Tables("ARTICO").Rows.Count - 1) To 0 Step -1
          If NTSCStr(dsArti.Tables("ARTICO").Rows(i)!xx_seleziona) = "S" Then
            With dsArti.Tables("ARTICO").Rows(i)
              If oCleClas.DeleteDataArtAssociati(NTSCStr(dsArti.Tables("ARTICO").Rows(i)!ar_codart)) = False Then
                Return
              End If
              dsArti.Tables("ARTICO").Rows(i).Delete()
              dsArti.Tables("ARTICO").AcceptChanges()
            End With
          End If
        Next
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClas.bGrigliaArticoli = False Then
        If grClas.Visible = False Then Return
        If Not grvClas.NTSRipristinaRigaCorrenteBefore(dcClas, True) Then Return
        oCleClas.Ripristina(dcClas.Position, dcClas.Filter)
        grvClas.NTSRipristinaRigaCorrenteAfter()
      Else
        If Not grvArti.NTSRipristinaRigaCorrenteBefore(dcArti, True) Then Return
        oCleClas.Ripristina(dcArti.Position, dcArti.Filter)
        grvArti.NTSRipristinaRigaCorrenteAfter()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGifView_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGifView.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If grvClas.NTSGetCurrentDataRow Is Nothing OrElse NTSCStr(grvClas.NTSGetCurrentDataRow!acl_gif).Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130223319183786284, "Posizionarsi su una riga di griglia con l'immagine impostata"))
        Return
      End If

      If Not Directory.Exists(oApp.ImgDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129803371577, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      If Not File.Exists(oApp.ImgDir & "\" & NTSCStr(grvClas.NTSGetCurrentDataRow!acl_gif)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128788389897732374, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = 2
      oPar.strPar2 = oApp.ImgDir & "\" & NTSCStr(grvClas.NTSGetCurrentDataRow!acl_gif)

      oMenu.RunChild("NTSInformatica", "FRMMGVGIF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub

  Public Overridable Sub tlbDesLingua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDesLingua.ItemClick
    Dim frmCsli As FRMMGCSLI = Nothing
    Try
      If dsClas IsNot Nothing AndAlso dsClas.Tables.Contains("ARTCLAS") AndAlso oCleClas.nLivello > 1 Then
        If Not Salva() Then Return
        frmCsli = CType(NTSNewFormModal("FRMMGCSLI"), FRMMGCSLI)
        If Not frmCsli.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
        frmCsli.InitEntity(oCleClas)
        frmCsli.ShowDialog(Me)
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmCsli Is Nothing Then frmCsli.Dispose()
      frmCsli = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbRiordinaAlbero_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRiordinaAlbero.ItemClick
    Try
      trClas.Nodes.Clear()

      CreaTreeview()
      trClas.Nodes(0).Collapse() 'per il refresh del nodo
      trClas_BeforeSelect(Me, Nothing)
      trClas_AfterSelect(Me, Nothing)
      trClas.Nodes(0).Expand()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Treeview"
  Public Overridable Sub trClas_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trClas.AfterSelect
    Dim nTabIndex As Integer = tsClas.SelectedTabPageIndex
    Dim strT() As String = Nothing
    Dim nodeTmp As TreeNode = Nothing
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      tsClas.SelectedTabPageIndex = 0
      '--------------------------------------------------------------------------------------------------------------
      trClas.SelectedNode.BackColor = Color.Aqua
      '--------------------------------------------------------------------------------------------------------------
      '--- Imposto il livello di lavoro e determino le chiavi per inserire nuovi record
      '--------------------------------------------------------------------------------------------------------------
      oCleClas.nLivello = 1
      oCleClas.strCodcla1 = ""
      oCleClas.strCodcla2 = ""
      oCleClas.strCodcla3 = ""
      oCleClas.strCodcla4 = ""
      oCleClas.strCodcla5 = ""
      strT = NTSCStr(trClas.SelectedNode.Tag).Split("|"c)
      '--------------------------------------------------------------------------------------------------------------
      If strT.Length = 1 And strT(0) = "" Then
        '------------------------------------------------------------------------------------------------------------
        '--- Sono sul livello 0 '.BASE.
        '------------------------------------------------------------------------------------------------------------
        oCleClas.strCodcla1 = "0"
      Else
        If strT.Length >= 1 Then oCleClas.strCodcla1 = strT(0)
        If strT.Length >= 2 Then oCleClas.strCodcla2 = strT(1)
        If strT.Length >= 3 Then oCleClas.strCodcla3 = strT(2)
        If strT.Length >= 4 Then oCleClas.strCodcla4 = strT(3)
        If strT.Length >= 5 Then oCleClas.strCodcla5 = strT(4)
        oCleClas.nLivello = strT.Length + 1
      End If

      If oCleClas.nLivello > 5 Then
        'Non posso inserire livelli oltre il 5
        grClas.Visible = False
        Return
      End If

      grClas.Visible = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Carico i dati da mostrare in griglia
      '--------------------------------------------------------------------------------------------------------------
      If Not ApriGriglia(NTSCStr(trClas.SelectedNode.Name)) Then Return
      If nTabIndex = 1 Then tsClas.SelectedTabPageIndex = 1
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub trClas_BeforeSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trClas.BeforeSelect
    Try
      If trClas.SelectedNode Is Nothing Then Return
      grClas.DataSource = Nothing
      trClas.SelectedNode.BackColor = Color.White
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvClas_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvClas.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvClas_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvClas.NTSFocusedRowChanged
    Dim strNodo As String = ""
    Try
      If dsClas Is Nothing Then Return
      If grClas.DataSource Is Nothing Then Return

      If Not grvClas.Columns.Contains(acl_codcla) Then Return

      ' se la riga è vuota posso inserire un valore, se è già presente una locazione non la faccio modificare
      If grvClas.GetFocusedRowCellValue(acl_codcla).ToString.Trim <> "" Then
        acl_codcla.Enabled = False
      Else
        GctlSetVisEnab(acl_codcla, False)
        grvClas.FocusedColumn = acl_codcla
      End If

      If grvClas.Focused = True Then SettaVariabiliAssociazioniDaGriglia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvArti_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvArti.LostFocus
    Try
      '--------------------------------------------------------------------------------------------------------------
      'If Salva() = False Then grvArti.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvArti_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvArti.NTSBeforeRowUpdate
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then e.Allow = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvArti_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvArti.NTSFocusedRowChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(grvArti.GetFocusedRowCellValue(ar_codart)).Trim <> "" Then
        ar_codart.Enabled = False
      Else
        GctlSetVisEnab(ar_codart, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi TabStrip"
  Public Overridable Sub tsClas_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsClas.SelectedPageChanged
    Dim bOk As Boolean = True

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.Page.Equals(tsClas.TabPages(1)) Then
        '------------------------------------------------------------------------------------------------------------
        'If grvClas.NTSGetCurrentDataRow Is Nothing Then
        '  oApp.MsgBoxInfo(oApp.Tr(Me, 130755450866821481, "Prima di procedere con l'associazione articoli selezionare una classificazione."))
        '  bOk = False
        'End If

        If Salva() = False Then bOk = False
        If bOk = False Then
          tsClas.SelectedTabPageIndex = 0
          Return
        End If
        '------------------------------------------------------------------------------------------------------------
        SettaVariabiliAssociazioni()
        '------------------------------------------------------------------------------------------------------------
        CaricaGrigliaArticoli()
        '------------------------------------------------------------------------------------------------------------
        oCleClas.bGrigliaArticoli = True
        '------------------------------------------------------------------------------------------------------------
      Else
        '------------------------------------------------------------------------------------------------------------
        oCleClas.bGrigliaArticoli = False
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      StatoPulsanti()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Functions"
  Public Overridable Function CreaTreeview() As Boolean
    Dim nodeT As TreeNode = Nothing
    Dim nodeTmp As TreeNode = Nothing
    Dim nodeFnd As TreeNode = Nothing
    Try
      'Creo il nodo radice
      nodeT = New TreeNode
      nodeT.Name = ".BASE."
      nodeT.Text = "CLASSIFICAZIONE ARTICOLI"
      nodeT.Tag = ""
      trClas.Nodes.Add(nodeT)

      For nLivello As Integer = 1 To 5
        dsClas = New DataSet
        dsClas.Clear()
        oCleClas.ApriLivello(nLivello, dsClas)

        For Each dtrT As DataRow In dsClas.Tables("ARTCLAS").Rows
          nodeTmp = New TreeNode
          nodeFnd = trClas.Nodes(0)
          Select Case nLivello
            Case 1
              nodeTmp.Name = nLivello & "_" & NTSCStr(dtrT!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla)
              nodeTmp.Tag = NTSCStr(dtrT!acl_codcla)
              nodeFnd.Nodes.Add(nodeTmp)
            Case 2
              nodeTmp.Name = nLivello & "_" & NTSCStr(dtrT!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla)
              nodeTmp.Tag = NTSCStr(dtrT!acl_codcla1) & "|" & NTSCStr(dtrT!acl_codcla)
              nodeFnd = nodeFnd.Nodes("1_" & NTSCStr(dtrT!acl_codcla1))
              If nodeFnd IsNot Nothing Then
                nodeFnd.Nodes.Add(nodeTmp)
              Else
                oApp.MsgBoxErr(ComponiMessaggioAssenzaNodo(1, _
                              NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla), _
                              NTSCStr(dtrT!acl_codcla1)))
                Return False
              End If

            Case 3
              nodeTmp.Name = nLivello & "_" & NTSCStr(dtrT!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla)
              nodeTmp.Tag = NTSCStr(dtrT!acl_codcla1) & "|" & NTSCStr(dtrT!acl_codcla2) & "|" & NTSCStr(dtrT!acl_codcla)
              nodeFnd = nodeFnd.Nodes("1_" & NTSCStr(dtrT!acl_codcla1))
              nodeFnd = nodeFnd.Nodes("2_" & NTSCStr(dtrT!acl_codcla2))
              If nodeFnd IsNot Nothing Then
                nodeFnd.Nodes.Add(nodeTmp)
              Else
                oApp.MsgBoxErr(ComponiMessaggioAssenzaNodo(2, _
                              NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla), _
                              NTSCStr(dtrT!acl_codcla2)))
                Return False
              End If

            Case 4
              nodeTmp.Name = nLivello & "_" & NTSCStr(dtrT!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla)
              nodeTmp.Tag = NTSCStr(dtrT!acl_codcla1) & "|" & NTSCStr(dtrT!acl_codcla2) & "|" & NTSCStr(dtrT!acl_codcla3) & "|" & NTSCStr(dtrT!acl_codcla)
              nodeFnd = nodeFnd.Nodes("1_" & NTSCStr(dtrT!acl_codcla1))
              nodeFnd = nodeFnd.Nodes("2_" & NTSCStr(dtrT!acl_codcla2))
              nodeFnd = nodeFnd.Nodes("3_" & NTSCStr(dtrT!acl_codcla3))
              If nodeFnd IsNot Nothing Then
                nodeFnd.Nodes.Add(nodeTmp)
              Else
                oApp.MsgBoxErr(ComponiMessaggioAssenzaNodo(3, _
                              NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla), _
                              NTSCStr(dtrT!acl_codcla3)))
                Return False
              End If
            Case 5
              nodeTmp.Name = nLivello & "_" & NTSCStr(dtrT!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla)
              nodeTmp.Tag = NTSCStr(dtrT!acl_codcla1) & "|" & NTSCStr(dtrT!acl_codcla2) & "|" & NTSCStr(dtrT!acl_codcla3) & "|" & NTSCStr(dtrT!acl_codcla4) & "|" & NTSCStr(dtrT!acl_codcla)
              nodeFnd = nodeFnd.Nodes("1_" & NTSCStr(dtrT!acl_codcla1))
              nodeFnd = nodeFnd.Nodes("2_" & NTSCStr(dtrT!acl_codcla2))
              nodeFnd = nodeFnd.Nodes("3_" & NTSCStr(dtrT!acl_codcla3))
              nodeFnd = nodeFnd.Nodes("4_" & NTSCStr(dtrT!acl_codcla4))
              If nodeFnd IsNot Nothing Then
                nodeFnd.Nodes.Add(nodeTmp)
              Else
                oApp.MsgBoxErr(ComponiMessaggioAssenzaNodo(4, _
                              NTSCStr(dtrT!acl_codcla) & " - " & NTSCStr(dtrT!acl_descla), _
                              NTSCStr(dtrT!acl_codcla4)))
                Return False
              End If
          End Select
        Next
      Next


      trClas.SelectedNode = trClas.Nodes(0)
      trClas.Nodes(0).Expand()

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiMessaggioAssenzaNodo(ByVal nLivelloPadre As Integer, _
         ByVal strCodiceDescrFiglio As String, ByVal strPadreMancante As String) As String
    Dim strResult As String = ""
    Try
      strResult = oApp.Tr(Me, 130933741326464785, _
                      "Impossibile procedere." & vbCrLf & _
                      "Il nodo da assocciare come padre alla classificazione |" & _
                      CStrSQL(strCodiceDescrFiglio) & "| non è stato creato." & vbCrLf & _
                      "Probabilmente si tratta di errata struttura dati: verificare la presenza della classificazione |" & _
                      CStrSQL(strPadreMancante) & "| nella tabella artclas|" & nLivelloPadre & "|.")

      Return strResult
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ApriGriglia(ByVal strCodclas As String) As Boolean
    Try
      dsClas = New DataSet
      dcClas = New BindingSource()

      If oCleClas.ApriGriglia(strCodclas, dsClas) Then
        pnRight.Visible = True
        grClas.Visible = True

        dcClas.DataSource = dsClas.Tables("ARTCLAS")
        dsClas.AcceptChanges()
        grClas.DataSource = dcClas
      Else
        pnRight.Visible = False
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriGif() As Boolean
    Dim bOk As Boolean = False
    Dim dRes As DialogResult
    Dim strNomeFile As String = ""
    Dim strPathFile As String = ""
    Dim nPosSep As Integer = 0
    Dim i As Integer = 0
    Dim strFileTmp As String = ""
    Dim strExtension As String = ""
    Dim OpenFileDialog1 As New NTSOpenFileDialog
    Try
      If grvClas.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130223382115270226, "Posizionarsi su una riga di griglia"))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini chiede di crearla
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oApp.ImgDir) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128559133252059319, "La cartella delle immagini non esiste. Crearla?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Try
            MkDir(oApp.ImgDir)
          Catch ex As Exception
            Exit Function
          End Try
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Adesso apre la Common Dialog sulla cartella delle immagini
      '-----------------------------------------------------------------------------------------
      OpenFileDialog1.CheckFileExists = True
      OpenFileDialog1.ShowReadOnly = False
      OpenFileDialog1.ShowHelp = False
      OpenFileDialog1.DefaultExt = "gif"
      OpenFileDialog1.Title = "Selezione immagine"
      OpenFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
      OpenFileDialog1.InitialDirectory = oApp.ImgDir
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
        If UCase(strPathFile) <> UCase(oApp.ImgDir & "\") Then
          If Not System.IO.File.Exists(oApp.ImgDir & "\" & strNomeFile) Then
            Try
              FileCopy(OpenFileDialog1.FileName, oApp.ImgDir & "\" & strNomeFile)
            Catch ex As Exception
              Exit Function
            End Try
          Else
            strFileTmp = oApp.ImgDir & "\" & strNomeFile
            strExtension = System.IO.Path.GetExtension(strFileTmp)
            strFileTmp = Mid(strFileTmp, 1, strFileTmp.Length - strExtension.Length)
            For i = 1 To 1000
              If System.IO.File.Exists(strFileTmp & "_" & i.ToString & strExtension) = False Then
                FileCopy(OpenFileDialog1.FileName, strFileTmp & "_" & i.ToString & strExtension)
                strNomeFile = strFileTmp & "_" & i.ToString & strExtension
                bOk = True
                Exit For
              End If
            Next
            If bOk = False Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 130421312902636697, "Attenzione!" & vbCrLf & _
                "Non è possibile selezionare questo file."))
              Return False
            Else
              For i = 1 To strNomeFile.Length
                If Mid(strNomeFile, i, 1) = "\" Then nPosSep = i
              Next
              If nPosSep > 0 Then strNomeFile = Microsoft.VisualBasic.Mid(strNomeFile, nPosSep + 1)
            End If
          End If
        End If

        grvClas.NTSGetCurrentDataRow!acl_gif = strNomeFile

      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub CaricaGrigliaArticoli()
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleClas.ApriGrigliaArtico(dsArti)
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dsArti.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grArti.DataSource = dcArti
      '--------------------------------------------------------------------------------------------------------------
      If dsArti.Tables("ARTICO").Rows.Count > 0 Then
        dcArti.MoveFirst()
        grvArti.LeftCoord = 0
        grvArti.FocusedColumn = xx_seleziona
        grvArti.Focus()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim bAsk As Boolean = False
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim dtrTmp() As DataRow
    Dim nodeTmp As TreeNode = Nothing
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleClas.bGrigliaArticoli = False Then
        '------------------------------------------------------------------------------------------------------------
        dRes = grvClas.NTSSalvaRigaCorrente(dcClas, oCleClas.RecordIsChanged, False)
        '------------------------------------------------------------------------------------------------------------
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Yes
            '--------------------------------------------------------------------------------------------------------
            If GctlControllaOutNotEqual() = False Then Return False
            '--------------------------------------------------------------------------------------------------------
            If Not oCleClas.Salva(False) Then Return False
            '--------------------------------------------------------------------------------------------------------
            '--- Aggiunge il nodo appena creato
            '--------------------------------------------------------------------------------------------------------
            dtrTmp = dsClas.Tables("ARTCLAS").Select(Nothing, Nothing, DataViewRowState.Added)
            For i = 0 To dtrTmp.Length - 1
              nodeTmp = New TreeNode
              nodeTmp.Name = oCleClas.nLivello & "_" & NTSCStr(dtrTmp(i)!acl_codcla)
              nodeTmp.Text = NTSCStr(dtrTmp(i)!acl_codcla) & " - " & NTSCStr(dtrTmp(i)!acl_descla)

              Select Case trClas.SelectedNode.Name
                Case ".BASE."
                  oCleClas.nLivello = 1
                  oCleClas.strCodcla1 = ""
                  oCleClas.strCodcla2 = ""
                  oCleClas.strCodcla3 = ""
                  oCleClas.strCodcla4 = ""
                  oCleClas.strCodcla1 = NTSCStr(dtrTmp(i)!acl_codcla)
                  strTmp = oCleClas.strCodcla1
                  nodeTmp.Tag = strTmp
                  trClas.SelectedNode.Nodes.Add(nodeTmp)
                Case Else
                  strT = NTSCStr(trClas.SelectedNode.Tag).Split("|"c)
                  If strT.Length >= 1 Then strTmp = strT(0)
                  If strT.Length >= 2 Then strTmp += "|" & strT(1)
                  If strT.Length >= 3 Then strTmp += "|" & strT(2)
                  If strT.Length >= 4 Then strTmp += "|" & strT(3)
                  If strTmp <> "" Then strTmp += "|"
                  strTmp += NTSCStr(dtrTmp(i)!acl_codcla)
                  nodeTmp.Tag = strTmp
                  trClas.SelectedNode.Nodes.Add(nodeTmp)
              End Select
            Next
            '--------------------------------------------------------------------------------------------------------
            '--- Corregge il nodo appena modificato
            '--------------------------------------------------------------------------------------------------------
            dtrTmp = dsClas.Tables("ARTCLAS").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
            For i = 0 To dtrTmp.Length - 1
              trClas.Nodes.Find(oCleClas.nLivello & "_" & NTSCStr(dtrTmp(i)!acl_codcla), True)(0).Text = NTSCStr(dtrTmp(i)!acl_codcla) & " - " & NTSCStr(dtrTmp(i)!acl_descla)
            Next
            '--------------------------------------------------------------------------------------------------------
            oCleClas.bHasChanges = False
            dsClas.AcceptChanges()
            '--------------------------------------------------------------------------------------------------------
          Case System.Windows.Forms.DialogResult.No : oCleClas.Ripristina(dcClas.Position, dcClas.Filter)
          Case System.Windows.Forms.DialogResult.Cancel : Return False
          Case System.Windows.Forms.DialogResult.Abort
        End Select
      Else
        '------------------------------------------------------------------------------------------------------------
        If dsArti.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added).Length > 0 Then bAsk = True
        dRes = grvArti.NTSSalvaRigaCorrente(dcArti, oCleClas.RecordIsChangedArtico, bAsk)
        '------------------------------------------------------------------------------------------------------------
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Yes
            '--------------------------------------------------------------------------------------------------------
            If GctlControllaOutNotEqual() = False Then Return False
            '--------------------------------------------------------------------------------------------------------
            If oCleClas.Salva(False) = False Then Return False
            '--------------------------------------------------------------------------------------------------------
            oCleClas.bHasChangesArtico = False
            dsArti.AcceptChanges()
            '--------------------------------------------------------------------------------------------------------
          Case System.Windows.Forms.DialogResult.No : oCleClas.Ripristina(dcClas.Position, dcClas.Filter)
          Case System.Windows.Forms.DialogResult.Cancel : Return False
          Case System.Windows.Forms.DialogResult.Abort
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SelezionaDeselezionaRighe(ByVal bSeleziona As Boolean)
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsArti.Tables("ARTICO").AcceptChanges()
      '------------------------------------------------------------------------------------------------------------
      If dsArti.Tables("ARTICO").Rows.Count = 0 Then Return
      '------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsArti.Tables("ARTICO").Rows.Count - 1)
        dsArti.Tables("ARTICO").Rows(i)!xx_seleziona = IIf(bSeleziona = True, "S", "N").ToString
      Next
      '------------------------------------------------------------------------------------------------------------
      dsArti.Tables("ARTICO").AcceptChanges()
      oCleClas.bHasChangesArtico = False
      '------------------------------------------------------------------------------------------------------------
      dcArti.MoveFirst()
      grvArti.LeftCoord = 0
      grvArti.FocusedColumn = xx_seleziona
      grvArti.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub StatoPulsanti()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClas.bGrigliaArticoli = False Then
        GctlSetVisEnab(tlbStrumenti, False)
        'tlbZoom.Enabled = False
        tlbSelAll.Enabled = False
        tlbDesAll.Enabled = False
        tlbRicarica.Enabled = False
        lbDescr.Text = ""
        lbDescr.Visible = False
      Else
        'GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbSelAll, False)
        GctlSetVisEnab(tlbDesAll, False)
        GctlSetVisEnab(tlbRicarica, False)
        tlbStrumenti.Enabled = False
        GctlSetVisEnab(lbDescr, True)
        lbDescr.Text = trClas.SelectedNode.Text
        'lbDescr.Text = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla) & " - " & _
        'NTSCStr(grvClas.NTSGetCurrentDataRow!acl_descla)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub SettaVariabiliAssociazioni()
    Dim strT() As String = Nothing
    Try
      oCleClas.nLivello = 1
      oCleClas.strCodcla1 = ""
      oCleClas.strCodcla2 = ""
      oCleClas.strCodcla3 = ""
      oCleClas.strCodcla4 = ""
      oCleClas.strCodcla5 = ""
      oCleClas.strCodcla1Ass = ""
      oCleClas.strCodcla2Ass = ""
      oCleClas.strCodcla3Ass = ""
      oCleClas.strCodcla4Ass = ""
      oCleClas.strCodcla5Ass = ""
      strT = NTSCStr(trClas.SelectedNode.Tag).Split("|"c)
      If strT.Length = 1 And strT(0) = "" Then
        oCleClas.strCodcla1Ass = "0"
      Else
        If strT.Length >= 1 Then oCleClas.strCodcla1Ass = strT(0)
        If strT.Length >= 2 Then oCleClas.strCodcla2Ass = strT(1)
        If strT.Length >= 3 Then oCleClas.strCodcla3Ass = strT(2)
        If strT.Length >= 4 Then oCleClas.strCodcla4Ass = strT(3)
        If strT.Length >= 5 Then oCleClas.strCodcla5Ass = strT(4)
        oCleClas.nLivello = strT.Length + 1
      End If
      oCleClas.strCodcla1 = oCleClas.strCodcla1Ass
      oCleClas.strCodcla2 = oCleClas.strCodcla2Ass
      oCleClas.strCodcla3 = oCleClas.strCodcla3Ass
      oCleClas.strCodcla4 = oCleClas.strCodcla4Ass
      oCleClas.strCodcla5 = oCleClas.strCodcla5Ass
      Return




      '--------------------------------------------------------------------------------------------------------------
      oCleClas.strCodcla1Ass = ""
      oCleClas.strCodcla2Ass = ""
      oCleClas.strCodcla3Ass = ""
      oCleClas.strCodcla4Ass = ""
      oCleClas.strCodcla5Ass = ""
      '--------------------------------------------------------------------------------------------------------------
      If grvClas.NTSGetCurrentDataRow Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleClas.nLivello
        Case 1
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 2
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 3
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 4
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla3)
          oCleClas.strCodcla4Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 5
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla3)
          oCleClas.strCodcla4Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla4)
          oCleClas.strCodcla5Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub SettaVariabiliAssociazioniDaGriglia()
    Dim strT() As String = Nothing
    Try
      oCleClas.nLivello = 1
      oCleClas.strCodcla1 = ""
      oCleClas.strCodcla2 = ""
      oCleClas.strCodcla3 = ""
      oCleClas.strCodcla4 = ""
      oCleClas.strCodcla5 = ""
      oCleClas.strCodcla1Ass = ""
      oCleClas.strCodcla2Ass = ""
      oCleClas.strCodcla3Ass = ""
      oCleClas.strCodcla4Ass = ""
      oCleClas.strCodcla5Ass = ""
      strT = NTSCStr(trClas.SelectedNode.Tag).Split("|"c)
      If strT.Length = 1 And strT(0) = "" Then
        oCleClas.strCodcla1Ass = "0"
      Else
        If strT.Length >= 1 Then oCleClas.strCodcla1Ass = strT(0)
        If strT.Length >= 2 Then oCleClas.strCodcla2Ass = strT(1)
        If strT.Length >= 3 Then oCleClas.strCodcla3Ass = strT(2)
        If strT.Length >= 4 Then oCleClas.strCodcla4Ass = strT(3)
        If strT.Length >= 5 Then oCleClas.strCodcla5Ass = NTSCStr(trClas.SelectedNode.Name)
        oCleClas.nLivello = strT.Length + 1
      End If
      '--------------------------------------------------------------------------------------------------------------
      If grvClas.NTSGetCurrentDataRow Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If trClas.SelectedNode.Name = ".BASE." Then Return
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleClas.nLivello
        Case 1
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 2
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 3
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 4
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla3)
          oCleClas.strCodcla4Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
        Case 5
          oCleClas.strCodcla1Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla1)
          oCleClas.strCodcla2Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla2)
          oCleClas.strCodcla3Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla3)
          oCleClas.strCodcla4Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla4)
          oCleClas.strCodcla5Ass = NTSCStr(grvClas.NTSGetCurrentDataRow!acl_codcla)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region
End Class