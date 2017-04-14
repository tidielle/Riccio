Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEFADI
  Private Moduli_P As Integer = bsModVE + bsModMG
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

  Public oCleFadi As CLEVEFADI
  Public oCallParams As CLE__CLDP
  Public dsFadi As New DataSet
  Public dcFadi As BindingSource = New BindingSource()
  Public dtLastDate As DateTime
  Public bElabora As Boolean = False

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEFADI))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbRielabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbBolleCollegate = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbSelAll = New NTSInformatica.NTSBarMenuItem
    Me.tlbDeselAll = New NTSInformatica.NTSBarMenuItem
    Me.tlbInvertiSel = New NTSInformatica.NTSBarMenuItem
    Me.tlbGeneraConad = New NTSInformatica.NTSBarMenuItem
    Me.tlbEmail = New NTSInformatica.NTSBarButtonItem
    Me.tlbNumerazioni = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grFadi = New NTSInformatica.NTSGrid
    Me.grvFadi = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.xx_rielab = New NTSInformatica.NTSGridColumn
    Me.tm_anno = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_totdoc = New NTSInformatica.NTSGridColumn
    Me.tm_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tm_flagiva_1 = New NTSInformatica.NTSGridColumn
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.lbStatus = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grFadi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFadi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbApri, Me.tlbRielabora, Me.tlbCancella, Me.tlbStampaPdf, Me.tlbGeneraConad, Me.tlbBolleCollegate, Me.tlbSelAll, Me.tlbDeselAll, Me.tlbInvertiSel, Me.tlbNumerazioni, Me.tlbEmail})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRielabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbBolleCollegate, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 17
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbRielabora
    '
    Me.tlbRielabora.Caption = "Rielabora"
    Me.tlbRielabora.Glyph = CType(resources.GetObject("tlbRielabora.Glyph"), System.Drawing.Image)
    Me.tlbRielabora.Id = 18
    Me.tlbRielabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbRielabora.Name = "tlbRielabora"
    Me.tlbRielabora.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 19
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbBolleCollegate
    '
    Me.tlbBolleCollegate.Caption = "Bolle collegate"
    Me.tlbBolleCollegate.Glyph = CType(resources.GetObject("tlbBolleCollegate.Glyph"), System.Drawing.Image)
    Me.tlbBolleCollegate.Id = 22
    Me.tlbBolleCollegate.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbBolleCollegate.Name = "tlbBolleCollegate"
    Me.tlbBolleCollegate.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInvertiSel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraConad, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEmail, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNumerazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbSelAll
    '
    Me.tlbSelAll.Caption = "Seleziona tutto"
    Me.tlbSelAll.Id = 23
    Me.tlbSelAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K))
    Me.tlbSelAll.Name = "tlbSelAll"
    Me.tlbSelAll.NTSIsCheckBox = False
    Me.tlbSelAll.Visible = True
    '
    'tlbDeselAll
    '
    Me.tlbDeselAll.Caption = "Deseleziona tutto"
    Me.tlbDeselAll.Id = 24
    Me.tlbDeselAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W))
    Me.tlbDeselAll.Name = "tlbDeselAll"
    Me.tlbDeselAll.NTSIsCheckBox = False
    Me.tlbDeselAll.Visible = True
    '
    'tlbInvertiSel
    '
    Me.tlbInvertiSel.Caption = "Inverti selezione"
    Me.tlbInvertiSel.Id = 25
    Me.tlbInvertiSel.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y))
    Me.tlbInvertiSel.Name = "tlbInvertiSel"
    Me.tlbInvertiSel.NTSIsCheckBox = False
    Me.tlbInvertiSel.Visible = True
    '
    'tlbGeneraConad
    '
    Me.tlbGeneraConad.Caption = "Genera file CONAD"
    Me.tlbGeneraConad.Id = 21
    Me.tlbGeneraConad.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbGeneraConad.Name = "tlbGeneraConad"
    Me.tlbGeneraConad.NTSIsCheckBox = False
    Me.tlbGeneraConad.Visible = True
    '
    'tlbEmail
    '
    Me.tlbEmail.Caption = "Visualizza e-mail scambiate"
    Me.tlbEmail.Id = 27
    Me.tlbEmail.Name = "tlbEmail"
    Me.tlbEmail.Visible = True
    '
    'tlbNumerazioni
    '
    Me.tlbNumerazioni.Caption = "Numerazioni"
    Me.tlbNumerazioni.Id = 26
    Me.tlbNumerazioni.Name = "tlbNumerazioni"
    Me.tlbNumerazioni.NTSIsCheckBox = False
    Me.tlbNumerazioni.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "Stampa Pdf"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.Id = 20
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = True
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
    'grFadi
    '
    Me.grFadi.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grFadi.EmbeddedNavigator.Name = ""
    Me.grFadi.Location = New System.Drawing.Point(0, 0)
    Me.grFadi.MainView = Me.grvFadi
    Me.grFadi.Name = "grFadi"
    Me.grFadi.Size = New System.Drawing.Size(648, 390)
    Me.grFadi.TabIndex = 5
    Me.grFadi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFadi})
    '
    'grvFadi
    '
    Me.grvFadi.ActiveFilterEnabled = False
    Me.grvFadi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.xx_rielab, Me.tm_anno, Me.tm_numdoc, Me.tm_serie, Me.tm_datdoc, Me.tm_totdoc, Me.tm_conto, Me.xx_conto, Me.tm_flagiva_1})
    Me.grvFadi.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFadi.Enabled = True
    Me.grvFadi.GridControl = Me.grFadi
    Me.grvFadi.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFadi.Name = "grvFadi"
    Me.grvFadi.NTSAllowDelete = True
    Me.grvFadi.NTSAllowInsert = True
    Me.grvFadi.NTSAllowUpdate = True
    Me.grvFadi.NTSMenuContext = Nothing
    Me.grvFadi.OptionsCustomization.AllowRowSizing = True
    Me.grvFadi.OptionsFilter.AllowFilterEditor = False
    Me.grvFadi.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFadi.OptionsNavigation.UseTabKey = False
    Me.grvFadi.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFadi.OptionsView.ColumnAutoWidth = False
    Me.grvFadi.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFadi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFadi.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFadi.OptionsView.ShowGroupPanel = False
    Me.grvFadi.RowHeight = 16
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
    Me.xx_seleziona.Width = 70
    '
    'xx_rielab
    '
    Me.xx_rielab.AppearanceCell.Options.UseBackColor = True
    Me.xx_rielab.AppearanceCell.Options.UseTextOptions = True
    Me.xx_rielab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_rielab.Caption = "Rielaborata"
    Me.xx_rielab.Enabled = False
    Me.xx_rielab.FieldName = "xx_rielab"
    Me.xx_rielab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_rielab.Name = "xx_rielab"
    Me.xx_rielab.NTSRepositoryComboBox = Nothing
    Me.xx_rielab.NTSRepositoryItemCheck = Nothing
    Me.xx_rielab.NTSRepositoryItemMemo = Nothing
    Me.xx_rielab.NTSRepositoryItemText = Nothing
    Me.xx_rielab.OptionsColumn.AllowEdit = False
    Me.xx_rielab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_rielab.OptionsColumn.ReadOnly = True
    Me.xx_rielab.OptionsFilter.AllowFilter = False
    Me.xx_rielab.Visible = True
    Me.xx_rielab.VisibleIndex = 1
    '
    'tm_anno
    '
    Me.tm_anno.AppearanceCell.Options.UseBackColor = True
    Me.tm_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tm_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_anno.Caption = "Anno"
    Me.tm_anno.Enabled = False
    Me.tm_anno.FieldName = "tm_anno"
    Me.tm_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_anno.Name = "tm_anno"
    Me.tm_anno.NTSRepositoryComboBox = Nothing
    Me.tm_anno.NTSRepositoryItemCheck = Nothing
    Me.tm_anno.NTSRepositoryItemMemo = Nothing
    Me.tm_anno.NTSRepositoryItemText = Nothing
    Me.tm_anno.OptionsColumn.AllowEdit = False
    Me.tm_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_anno.OptionsColumn.ReadOnly = True
    Me.tm_anno.OptionsFilter.AllowFilter = False
    Me.tm_anno.Visible = True
    Me.tm_anno.VisibleIndex = 2
    Me.tm_anno.Width = 70
    '
    'tm_numdoc
    '
    Me.tm_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numdoc.Caption = "Numero"
    Me.tm_numdoc.Enabled = False
    Me.tm_numdoc.FieldName = "tm_numdoc"
    Me.tm_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numdoc.Name = "tm_numdoc"
    Me.tm_numdoc.NTSRepositoryComboBox = Nothing
    Me.tm_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_numdoc.NTSRepositoryItemText = Nothing
    Me.tm_numdoc.OptionsColumn.AllowEdit = False
    Me.tm_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numdoc.OptionsColumn.ReadOnly = True
    Me.tm_numdoc.OptionsFilter.AllowFilter = False
    Me.tm_numdoc.Visible = True
    Me.tm_numdoc.VisibleIndex = 3
    Me.tm_numdoc.Width = 70
    '
    'tm_serie
    '
    Me.tm_serie.AppearanceCell.Options.UseBackColor = True
    Me.tm_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tm_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_serie.Caption = "Serie"
    Me.tm_serie.Enabled = False
    Me.tm_serie.FieldName = "tm_serie"
    Me.tm_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_serie.Name = "tm_serie"
    Me.tm_serie.NTSRepositoryComboBox = Nothing
    Me.tm_serie.NTSRepositoryItemCheck = Nothing
    Me.tm_serie.NTSRepositoryItemMemo = Nothing
    Me.tm_serie.NTSRepositoryItemText = Nothing
    Me.tm_serie.OptionsColumn.AllowEdit = False
    Me.tm_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_serie.OptionsColumn.ReadOnly = True
    Me.tm_serie.OptionsFilter.AllowFilter = False
    Me.tm_serie.Visible = True
    Me.tm_serie.VisibleIndex = 4
    Me.tm_serie.Width = 70
    '
    'tm_datdoc
    '
    Me.tm_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datdoc.Caption = "Data"
    Me.tm_datdoc.Enabled = False
    Me.tm_datdoc.FieldName = "tm_datdoc"
    Me.tm_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datdoc.Name = "tm_datdoc"
    Me.tm_datdoc.NTSRepositoryComboBox = Nothing
    Me.tm_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_datdoc.NTSRepositoryItemText = Nothing
    Me.tm_datdoc.OptionsColumn.AllowEdit = False
    Me.tm_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datdoc.OptionsColumn.ReadOnly = True
    Me.tm_datdoc.OptionsFilter.AllowFilter = False
    Me.tm_datdoc.Visible = True
    Me.tm_datdoc.VisibleIndex = 5
    Me.tm_datdoc.Width = 70
    '
    'tm_totdoc
    '
    Me.tm_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totdoc.Caption = "Totale doc."
    Me.tm_totdoc.Enabled = False
    Me.tm_totdoc.FieldName = "tm_totdoc"
    Me.tm_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totdoc.Name = "tm_totdoc"
    Me.tm_totdoc.NTSRepositoryComboBox = Nothing
    Me.tm_totdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_totdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_totdoc.NTSRepositoryItemText = Nothing
    Me.tm_totdoc.OptionsColumn.AllowEdit = False
    Me.tm_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totdoc.OptionsColumn.ReadOnly = True
    Me.tm_totdoc.OptionsFilter.AllowFilter = False
    Me.tm_totdoc.Visible = True
    Me.tm_totdoc.VisibleIndex = 6
    Me.tm_totdoc.Width = 70
    '
    'tm_conto
    '
    Me.tm_conto.AppearanceCell.Options.UseBackColor = True
    Me.tm_conto.AppearanceCell.Options.UseTextOptions = True
    Me.tm_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_conto.Caption = "Cliente"
    Me.tm_conto.Enabled = False
    Me.tm_conto.FieldName = "tm_conto"
    Me.tm_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_conto.Name = "tm_conto"
    Me.tm_conto.NTSRepositoryComboBox = Nothing
    Me.tm_conto.NTSRepositoryItemCheck = Nothing
    Me.tm_conto.NTSRepositoryItemMemo = Nothing
    Me.tm_conto.NTSRepositoryItemText = Nothing
    Me.tm_conto.OptionsColumn.AllowEdit = False
    Me.tm_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_conto.OptionsColumn.ReadOnly = True
    Me.tm_conto.OptionsFilter.AllowFilter = False
    Me.tm_conto.Visible = True
    Me.tm_conto.VisibleIndex = 7
    Me.tm_conto.Width = 70
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. cliente"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 8
    Me.xx_conto.Width = 70
    '
    'tm_flagiva_1
    '
    Me.tm_flagiva_1.AppearanceCell.Options.UseBackColor = True
    Me.tm_flagiva_1.AppearanceCell.Options.UseTextOptions = True
    Me.tm_flagiva_1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_flagiva_1.Caption = "Da rielabor."
    Me.tm_flagiva_1.Enabled = False
    Me.tm_flagiva_1.FieldName = "tm_flagiva_1"
    Me.tm_flagiva_1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_flagiva_1.Name = "tm_flagiva_1"
    Me.tm_flagiva_1.NTSRepositoryComboBox = Nothing
    Me.tm_flagiva_1.NTSRepositoryItemCheck = Nothing
    Me.tm_flagiva_1.NTSRepositoryItemMemo = Nothing
    Me.tm_flagiva_1.NTSRepositoryItemText = Nothing
    Me.tm_flagiva_1.OptionsColumn.AllowEdit = False
    Me.tm_flagiva_1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_flagiva_1.OptionsColumn.ReadOnly = True
    Me.tm_flagiva_1.OptionsFilter.AllowFilter = False
    Me.tm_flagiva_1.Visible = True
    Me.tm_flagiva_1.VisibleIndex = 9
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grFadi)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 30)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(648, 390)
    Me.pnGrid.TabIndex = 6
    Me.pnGrid.Text = "NtsPanel1"
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.lbStatus)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 420)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(648, 22)
    Me.pnBottom.TabIndex = 7
    Me.pnBottom.Text = "NtsPanel1"
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(3, 3)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(11, 13)
    Me.lbStatus.TabIndex = 0
    Me.lbStatus.Text = "."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'FRMVEFADI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVEFADI"
    Me.NTSLastControlFocussed = Me.grFadi
    Me.Text = "FATTURAZIONE DIFFERITA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grFadi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFadi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEFADI", "BEVEFADI", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleFadi = CType(oTmp, CLEVEFADI)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEFADI", strRemoteServer, strRemotePort)
    AddHandler oCleFadi.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleFadi.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbRielabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbBolleCollegate.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvFadi.NTSSetParam(oMenu, "GENERA FATTURE DIFFERITE")
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128607611685625000, "Seleziona"), "S", "N")
      xx_rielab.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128750393743048000, "Rielaborata"), "S", "N")
      tm_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685781250, "Anno"), "0", 4, 1900, 2099)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685937500, "Numero"), "0", 1, 0, 999999999)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686093750, "Serie"), CLN__STD.SerieMaxLen, False)
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128607611686250000, "Data"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tm_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611686406250, "Totale doc."), oApp.FormatImporti, 9, -999999999, 999999999)
      tm_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128607611686562500, "Cliente"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686718750, "Descr. cliente"), 0, True)
      tm_flagiva_1.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129302211226640625, "Da rielaborare"), "S", " ")
      grvFadi.NTSAllowInsert = False
      grvFadi.NTSAllowDelete = False

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

  Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
      MyBase.GestisciEventiEntity(sender, e)

      If e.TipoEvento.Trim.Length < 10 Then Return
      Select Case e.TipoEvento
        Case "PREZZIZERO"
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128757961301056000, "Attenzione!" & vbCrLf & _
                                          "Esistono righe nella selezione che posseggono prezzi a zero." & vbCrLf & _
                                          "L'elaborazione è stata annullata." & vbCrLf & _
                                          "Visualizzarle?")) = Windows.Forms.DialogResult.Yes Then
            e.RetValue = ThMsg.RETVALUE_YES
          End If
        Case "AGGIOLABEL"
          lbStatus.Text = e.Message
          lbStatus.Refresh()
          Application.DoEvents()    'altrimenti la label non viene aggiornata ...
        Case "ASKVISLOG:"
          NTSProcessStart("notepad", e.Message)
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


#Region "Eventi di Form"
  Public Overridable Sub FRMVEFADI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      grFadi.Visible = False
      bElabora = False

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
        tlbEmail.Visible = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEFADI_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '-------------------
      'verifico che tabpeve siano compilate;  istanzio bevefdin
      Me.Cursor = Cursors.WaitCursor
      If Not oCleFadi.InitExt() Then
        Me.Close()
        Return
      End If

      SetStato()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRMVEFADI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      'non posso uscire durante un'elaborazione di creazione fatture, sopratutto se è una elaborazione di prova. rimarrebbero le fatture con numero negativo generate
      If bElabora Then
        e.Cancel = True
      Else
        If Not oCleFadi Is Nothing Then
          If CBool(oCleFadi.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
            oMenu.ResetTblInstId("TTKEYS", False, oCleFadi.lIITtkeys)
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMVEFADI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcFadi.Dispose()
      dsFadi.Dispose()
      If Not oCallParams Is Nothing Then oCallParams.strParam = "N" 'valore di ritorno se chiamato da BSORGSOL
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmNuovo As FRMVESEFD = Nothing
    Dim frmNuovo_F As FRMVESEFD_F = Nothing
    Dim frmDdts As FRMVEDDTS = Nothing
    Dim bOk As Boolean = False
    Dim strFattSplit As String = ""
    Dim dsTmp As New DataSet
    Try
      If Not dsFadi Is Nothing Then
        If dsFadi.Tables.Count > 0 Then
          dsFadi.Tables("FADI").Clear()
          dsFadi.Tables("FADI").AcceptChanges()
        End If
      End If
      oCleFadi.dttDDTs.Clear()

      SetStato()
      '-------------------
      'verifico se è rimasta una vecchia elaborazione di prova aperta
      oCleFadi.CaricaDoc("", "", 0, 0, 99999999, " ", "Z", dsTmp, True, "T")
      If dsTmp.Tables("FADI").Rows.Count > 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129095926678476563, "ATTENZIONE: è stata trovata una precedente elaborazione di prova (ovvero con numero fattura minore di 0). Aprire le fatture utilizzando come filtro solo 'Elaborazione di prova' e cancellare i documenti il prima possibile"))
        Return
      End If

      If CLN__STD.FRIENDLY = False Then
        frmNuovo = CType(NTSNewFormModal("FRMVESEFD"), FRMVESEFD)
        frmNuovo.Init(oMenu, Nothing, DittaCorrente)
        If oCleFadi.bRiproponiDataDoc = True Then frmNuovo.edDatadoc.Text = dtLastDate.ToShortDateString
        frmNuovo.oCleFadi = oCleFadi
        frmNuovo.bRielabora = False
        frmNuovo.ShowDialog()
        If frmNuovo.bOk = False Then Return
        If frmNuovo.ckVisDDT.Checked = False Then GoTo INIZIA
      Else
        frmNuovo_F = CType(NTSNewFormModal("FRMVESEFD_F"), FRMVESEFD_F)
        frmNuovo_F.Init(oMenu, Nothing, DittaCorrente)
        If oCleFadi.bRiproponiDataDoc = True Then frmNuovo_F.edDatadoc.Text = dtLastDate.ToShortDateString
        frmNuovo_F.oCleFadi = oCleFadi
        frmNuovo_F.bRielabora = False
        frmNuovo_F.ShowDialog()
        If frmNuovo_F.bOk = False Then Return
        If frmNuovo_F.ckVisDDT.Checked = False Then GoTo INIZIA
      End If

      '-------------------------------------------------------
      'carico l'elenco dei DDT che dovranno essere riepilogati
      'potrò rimuoverne alcuni, ma non modificarli, diversamente dovrei ricaricare la lista (cosa succederebbe se cambiassero su un DDT il codpaga?)
      Me.Cursor = Cursors.WaitCursor
      If CLN__STD.FRIENDLY = False Then
        If Not oCleFadi.GettDDTDaElab(frmNuovo.cbTipodoc.SelectedValue, NTSCInt(frmNuovo.edAnnodoc.Text), frmNuovo.edSeriedoc.Text, _
                                 NTSCDate(frmNuovo.edDatadoc.Text), NTSCInt(frmNuovo.edNumdoc.Text), frmNuovo.ckProva.Checked, _
                                 frmNuovo.cbCheckrighe.SelectedValue, _
                                 frmNuovo.strOutTestmag, frmNuovo.strOutAnagra, NTSCInt(frmNuovo.strOutLista), _
                                 frmNuovo.lOutNumdocDa, frmNuovo.lOutNumdocA, frmNuovo.strOutSerieDa, frmNuovo.strOutSerieA, _
                                 strFattSplit, False, frmNuovo.ckForzaDataDiversa.Checked, frmNuovo.edDataDiversa.Text) Then Return
      Else
        If Not oCleFadi.GettDDTDaElab(frmNuovo_F.cbTipodoc.SelectedValue, NTSCInt(frmNuovo_F.edAnnodoc.Text), _
          frmNuovo_F.edSeriedoc.Text, NTSCDate(frmNuovo_F.edDatadoc.Text), NTSCInt(frmNuovo_F.edNumdoc.Text), _
          frmNuovo_F.ckProva.Checked, frmNuovo_F.cbCheckrighe.SelectedValue, frmNuovo_F.strOutTestmag, _
          frmNuovo_F.strOutAnagra, NTSCInt(frmNuovo_F.strOutLista), frmNuovo_F.lOutNumdocDa, frmNuovo_F.lOutNumdocA, _
          frmNuovo_F.strOutSerieDa, frmNuovo_F.strOutSerieA, strFattSplit, False, False, "") Then Return
      End If
      frmDdts = CType(NTSNewFormModal("FRMVEDDTS"), FRMVEDDTS)
      frmDdts.Init(oMenu, Nothing, DittaCorrente)
      frmDdts.oCleFadi = oCleFadi
      frmDdts.ShowDialog()
      If frmDdts.bOk = False Or oCleFadi.dttDDTs.Rows.Count = 0 Then
        Me.Cursor = Cursors.Default
        Return
      End If

      '-------------------------------------------------------
      'inizio l'elaborazione vera e propria
INIZIA:
      bElabora = True
      'valori di ritorno
      Cursor.Current = Cursors.WaitCursor
      If CLN__STD.FRIENDLY = False Then
        dtLastDate = NTSCDate(frmNuovo.edDatadoc.Text)
        oCleFadi.strNoteFatture = frmNuovo.edNoteFattura.Text
        bOk = oCleFadi.GeneraDoc(frmNuovo.cbTipodoc.SelectedValue, NTSCInt(frmNuovo.edAnnodoc.Text), frmNuovo.edSeriedoc.Text, _
                               NTSCDate(frmNuovo.edDatadoc.Text), NTSCInt(frmNuovo.edNumdoc.Text), frmNuovo.ckProva.Checked, _
                               frmNuovo.cbCheckrighe.SelectedValue, _
                               frmNuovo.strOutTestmag, frmNuovo.strOutAnagra, NTSCInt(frmNuovo.strOutLista), _
                               frmNuovo.lOutNumdocDa, frmNuovo.lOutNumdocA, frmNuovo.strOutSerieDa, frmNuovo.strOutSerieA, _
                               strFattSplit, False, frmNuovo.ckForzaDataDiversa.Checked, frmNuovo.edDataDiversa.Text)
      Else
        dtLastDate = NTSCDate(frmNuovo_F.edDatadoc.Text)
        oCleFadi.strNoteFatture = frmNuovo_F.edNoteFattura.Text
        bOk = oCleFadi.GeneraDoc(frmNuovo_F.cbTipodoc.SelectedValue, NTSCInt(frmNuovo_F.edAnnodoc.Text), _
          frmNuovo_F.edSeriedoc.Text, NTSCDate(frmNuovo_F.edDatadoc.Text), NTSCInt(frmNuovo_F.edNumdoc.Text), _
          frmNuovo_F.ckProva.Checked, frmNuovo_F.cbCheckrighe.SelectedValue, frmNuovo_F.strOutTestmag, _
          frmNuovo_F.strOutAnagra, NTSCInt(frmNuovo_F.strOutLista), frmNuovo_F.lOutNumdocDa, frmNuovo_F.lOutNumdocA, _
          frmNuovo_F.strOutSerieDa, frmNuovo_F.strOutSerieA, strFattSplit, False, False, "")
      End If

      'carico le fatture generate: esco se non ho generato nulla
      If strFattSplit = "" Then GoTo FINE

      If CLN__STD.FRIENDLY = False Then
        If Not oCleFadi.CaricaDocGenerati(frmNuovo.cbTipodoc.SelectedValue, NTSCInt(frmNuovo.edAnnodoc.Text), _
                                          frmNuovo.edSeriedoc.Text, strFattSplit, False, dsFadi) Then GoTo FINE
      Else
        If Not oCleFadi.CaricaDocGenerati(frmNuovo_F.cbTipodoc.SelectedValue, NTSCInt(frmNuovo_F.edAnnodoc.Text), _
          frmNuovo_F.edSeriedoc.Text, strFattSplit, False, dsFadi) Then GoTo FINE
      End If
      dcFadi.DataSource = dsFadi.Tables("FADI")
      dsFadi.AcceptChanges()
      grFadi.DataSource = dcFadi
      If CLN__STD.FRIENDLY = False Then
        If NTSCInt(frmNuovo.nCodiceNuovaListaSel) > 0 Then
          If Not oCleFadi.GeneraNuovaListaSelezionata(frmNuovo.nCodiceNuovaListaSel, frmNuovo.strDescrNuovaListaSel, _
                                                      dsFadi.Tables("FADI")) Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129975566433855140, "La nuova lista selezionata dei clienti coinvolti nella fatturazione non è stata generata."))
          End If
        End If
      End If

      '----------------------
      'se elaborazione di prova genero le fatture, le stampo e le cancello
      If CLN__STD.FRIENDLY = False Then
        If frmNuovo.ckProva.Checked Then
          tlbSelAll_ItemClick(tlbSelAll, Nothing)
          tlbStampaVideo_ItemClick(tlbStampaVideo, Nothing)
          tlbCancella_ItemClick(tlbCancella, Nothing)
        End If
      Else
        If frmNuovo_F.ckProva.Checked Then
          tlbSelAll_ItemClick(tlbSelAll, Nothing)
          tlbStampaVideo_ItemClick(tlbStampaVideo, Nothing)
          tlbCancella_ItemClick(tlbCancella, Nothing)
        End If
      End If
FINE:
      If oCleFadi.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleFadi.LogFileName)
        End If
      End If

      SetStato()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bElabora = False
      dsTmp.Clear()
      Cursor.Current = Cursors.Default
      If Not frmNuovo Is Nothing Then frmNuovo.Dispose()
      frmNuovo = Nothing
      If Not frmNuovo_F Is Nothing Then frmNuovo_F.Dispose()
      frmNuovo_F = Nothing
      If Not frmDdts Is Nothing Then frmDdts.Dispose()
      frmDdts = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim frmApri As FRMVESERF = Nothing
    Dim frmApri_f As FRMVESERF_F = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not dsFadi Is Nothing Then
        If dsFadi.Tables.Count > 0 Then
          dsFadi.Tables("FADI").Clear()
          dsFadi.Tables("FADI").AcceptChanges()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      SetStato()
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then
        frmApri = CType(NTSNewFormModal("FRMVESERF"), FRMVESERF)
        frmApri.Init(oMenu, Nothing, DittaCorrente)
        frmApri.oCleFadi = oCleFadi
        frmApri.ShowDialog()
        If frmApri.bOk = False Then Return
      Else
        frmApri_f = CType(NTSNewFormModal("FRMVESERF_F"), FRMVESERF_F)
        frmApri_f.Init(oMenu, Nothing, DittaCorrente)
        frmApri_f.oCleFadi = oCleFadi
        frmApri_f.ShowDialog()
        If frmApri_f.bOk = False Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      'valori di ritorno
      Cursor.Current = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then
        oCleFadi.strNoteFatture = frmApri.edNoteFattura.Text
        oCleFadi.CaricaDoc(frmApri.strOutTestmag, frmApri.strOutAnagra, NTSCInt(frmApri.strOutLista), _
                            frmApri.lOutNumdocDa, frmApri.lOutNumdocA, frmApri.strOutSerieDa, _
                            frmApri.strOutSerieA, dsFadi, frmApri.ckProva.Checked, _
                            frmApri.cbContab.SelectedValue)
      Else
        oCleFadi.strNoteFatture = frmApri_f.edNoteFattura.Text
        oCleFadi.CaricaDoc(frmApri_f.strOutTestmag, frmApri_f.strOutAnagra, NTSCInt(frmApri_f.strOutLista), _
                            frmApri_f.lOutNumdocDa, frmApri_f.lOutNumdocA, frmApri_f.strOutSerieDa, _
                            frmApri_f.strOutSerieA, dsFadi, frmApri_f.ckProva.Checked, _
                            frmApri_f.cbContab.SelectedValue)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dsFadi.Tables("FADI").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128757936477848000, "Non è stato trovato nessun documento riepilogativo con i filtri impostati"))
      End If
      dcFadi.DataSource = dsFadi.Tables("FADI")
      dsFadi.AcceptChanges()
      grFadi.DataSource = dcFadi
      '--------------------------------------------------------------------------------------------------------------
      SetStato()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Cursor.Current = Cursors.Default
      If Not frmApri Is Nothing Then frmApri.Dispose()
      frmApri = Nothing
      If Not frmApri_f Is Nothing Then frmApri_f.Dispose()
      frmApri_f = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbRielabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRielabora.ItemClick
    Dim frmNuovo As FRMVESEFD = Nothing
    Dim frmNuovo_f As FRMVESEFD_F = Nothing
    Dim bOk As Boolean = False
    Dim strFattSplit As String = ""
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa(False) Then Return
      If Not oCleFadi.TestPreRielabora Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly scaduta e nella selezione esistono documenti successivi alla data di
      '--- scadenza della chiave, avvisa ed esce
      '--------------------------------------------------------------------------------------------------------------
      If (CLN__STD.FRIENDLY = True) And _
         (NTSCDate(Now.ToShortDateString) > NTSCDate(oApp.ActKey.DataScad)) Then
        dtrT = dsFadi.Tables("FADI").Select("xx_seleziona = 'S' AND tm_datdoc > " & CDataSQL(NTSCDate(oApp.ActKey.DataScad)))
        If dtrT.Length > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130162738724675984, "Attenzione!" & vbCrLf & _
            "Chiave di attivazione scaduta!" & vbCrLf & _
            "Nella selezione risultano documenti con data posteriore alla data di scadenza della chiave di attivazione ('|" & oApp.ActKey.DataScad & "|')." & vbCrLf & _
            "Operazione di rielaborazione non possibile."))
          Return
        End If
      End If

      If Not oCleFadi.TestaBlocchi(False, True, dsFadi.Tables("FADI")) Then Return

      '--------------------------------------------------------------------------------------------------------------
      dtrT = dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_numdoc ASC")
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then
        frmNuovo = CType(NTSNewFormModal("FRMVESEFD"), FRMVESEFD)
        frmNuovo.Init(oMenu, Nothing, DittaCorrente)
        frmNuovo.bRielabora = True
        frmNuovo.ckRiepilogatutto.Checked = False
        frmNuovo.ckProva.Checked = False
        frmNuovo.strRielabTipork = dtrT(0)!tm_tipork.ToString
        frmNuovo.edAnnodoc.Text = dtrT(0)!tm_anno.ToString
        frmNuovo.edSeriedoc.Text = dtrT(0)!tm_serie.ToString
        frmNuovo.nRielabFirstNumdoc = NTSCInt(dtrT(0)!tm_numdoc)
        frmNuovo.oCleFadi = oCleFadi
        frmNuovo.bRielabora = True
        frmNuovo.edDatadoc.Text = NTSCDate(dtrT(0)!tm_datdoc).ToShortDateString
        frmNuovo.ckRiepilogatutto.Checked = False
        frmNuovo.opCliDaa.Checked = True
        frmNuovo.edClienteda.Text = NTSCInt(dtrT(0)!tm_conto).ToString
        frmNuovo.edClientea.Text = NTSCInt(dtrT(0)!tm_conto).ToString
        frmNuovo.cbPeriodofatt.SelectedValue = " "
        frmNuovo.ShowDialog()
        If frmNuovo.bOk = False Then Return
      Else
        frmNuovo_f = CType(NTSNewFormModal("FRMVESEFD_F"), FRMVESEFD_F)
        frmNuovo_f.Init(oMenu, Nothing, DittaCorrente)
        frmNuovo_f.bRielabora = True
        frmNuovo_f.ckRiepilogatutto.Checked = False
        frmNuovo_f.ckProva.Checked = False
        frmNuovo_f.strRielabTipork = dtrT(0)!tm_tipork.ToString
        frmNuovo_f.edAnnodoc.Text = dtrT(0)!tm_anno.ToString
        frmNuovo_f.edSeriedoc.Text = dtrT(0)!tm_serie.ToString
        frmNuovo_f.nRielabFirstNumdoc = NTSCInt(dtrT(0)!tm_numdoc)
        frmNuovo_f.oCleFadi = oCleFadi
        frmNuovo_f.bRielabora = True
        frmNuovo_f.edDatadoc.Text = NTSCDate(dtrT(0)!tm_datdoc).ToShortDateString
        frmNuovo_f.ckRiepilogatutto.Checked = False
        frmNuovo_f.edClienteda.Text = NTSCInt(dtrT(0)!tm_conto).ToString
        frmNuovo_f.edClientea.Text = NTSCInt(dtrT(0)!tm_conto).ToString
        frmNuovo_f.cbPeriodofatt.SelectedValue = " "
        frmNuovo_f.ShowDialog()
        If frmNuovo_f.bOk = False Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Cursor.Current = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128764151860303000, _
                        "ATTENZIONE: la rielaborazione provvederà prima a cancellare le fatture selezionate, " & vbCrLf & _
                        "quindi creerà nuovi documenti in base alla selezione appena effettuata: proseguire?" & vbCrLf & vbCrLf & _
                        "NB: In presenza di modulo Provvigioni e/o modulo Intrastat con dati già estratti " & vbCrLf & _
                        "nel periodo dei documenti da cancellare" & vbCrLf & _
                        "sarà necessario eseguire manualmente la riestrazione delle provvigioni e/o dati intrastat")) = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancello i documenti da rielaborare
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleFadi.CancellaFatture(dsFadi.Tables("FADI")) Then Return
      SetStato()
      '--------------------------------------------------------------------------------------------------------------
      Cursor.Current = Cursors.WaitCursor
      If CLN__STD.FRIENDLY = False Then
        dtLastDate = NTSCDate(frmNuovo.edDatadoc.Text)
        oCleFadi.strNoteFatture = frmNuovo.edNoteFattura.Text
      Else
        dtLastDate = NTSCDate(frmNuovo_f.edDatadoc.Text)
        oCleFadi.strNoteFatture = frmNuovo_f.edNoteFattura.Text
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then
        bOk = oCleFadi.GeneraDoc(frmNuovo.cbTipodoc.SelectedValue, NTSCInt(frmNuovo.edAnnodoc.Text), frmNuovo.edSeriedoc.Text, _
                                 NTSCDate(frmNuovo.edDatadoc.Text), NTSCInt(frmNuovo.edNumdoc.Text), frmNuovo.ckProva.Checked, _
                                 frmNuovo.cbCheckrighe.SelectedValue, _
                                 frmNuovo.strOutTestmag, frmNuovo.strOutAnagra, NTSCInt(frmNuovo.strOutLista), _
                                 frmNuovo.lOutNumdocDa, frmNuovo.lOutNumdocA, frmNuovo.strOutSerieDa, frmNuovo.strOutSerieA, _
                                 strFattSplit, True)
      Else
        bOk = oCleFadi.GeneraDoc(frmNuovo_f.cbTipodoc.SelectedValue, NTSCInt(frmNuovo_f.edAnnodoc.Text), _
                                 frmNuovo_f.edSeriedoc.Text, NTSCDate(frmNuovo_f.edDatadoc.Text), _
                                 NTSCInt(frmNuovo_f.edNumdoc.Text), frmNuovo_f.ckProva.Checked, _
                                 frmNuovo_f.cbCheckrighe.SelectedValue, frmNuovo_f.strOutTestmag, _
                                 frmNuovo_f.strOutAnagra, NTSCInt(frmNuovo_f.strOutLista), _
                                 frmNuovo_f.lOutNumdocDa, frmNuovo_f.lOutNumdocA, frmNuovo_f.strOutSerieDa, _
                                 frmNuovo_f.strOutSerieA, strFattSplit, True)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Carico le fatture generate: esco se non ho generato nulla
      '--------------------------------------------------------------------------------------------------------------
      If strFattSplit <> "" Then
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiungo al dattable corrente le fatture aggiunte, marcandole come 'rielaborate'
        '------------------------------------------------------------------------------------------------------------
        If CLN__STD.FRIENDLY = False Then
          If Not oCleFadi.CaricaDocGenerati(frmNuovo.cbTipodoc.SelectedValue, NTSCInt(frmNuovo.edAnnodoc.Text), _
                                            frmNuovo.edSeriedoc.Text, strFattSplit, True, dsFadi) Then Return
        Else
          If Not oCleFadi.CaricaDocGenerati(frmNuovo_f.cbTipodoc.SelectedValue, NTSCInt(frmNuovo_f.edAnnodoc.Text), _
                                            frmNuovo_f.edSeriedoc.Text, strFattSplit, True, dsFadi) Then Return
        End If
        dcFadi.DataSource = dsFadi.Tables("FADI")
        dsFadi.AcceptChanges()
        grFadi.DataSource = dcFadi
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleFadi.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129276325761286406, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleFadi.LogFileName)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      SetStato()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Cursor.Current = Cursors.Default
      If Not frmNuovo Is Nothing Then frmNuovo.Dispose()
      frmNuovo = Nothing
      If Not frmNuovo_f Is Nothing Then frmNuovo_f.Dispose()
      frmNuovo_f = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strMsg As String = ""
    Dim dtrT() As DataRow = Nothing

    Try
      If e Is Nothing Then GoTo Cancella 'se generato dalla fatturazione di prova non eseguo nessun test e cancello direttamente

      If Not TestPreStampa(False) Then Return

      strMsg = oApp.Tr(Me, 129763890572077667, "Eliminare i documenti selezionati?" & vbCrLf & vbCrLf & _
                        "NB: In presenza di modulo Provvigioni e/o modulo Intrastat con dati già estratti " & vbCrLf & _
                        "nel periodo dei documenti da cancellare" & vbCrLf & _
                        "sarà necessario eseguire manualmente la riestrazione delle provvigioni e/o dati intrastat")
      If oApp.MsgBoxInfoYesNo_DefNo(strMsg) = Windows.Forms.DialogResult.No Then Return

      If Not oCleFadi.TestaBlocchi(False, False, dsFadi.Tables("FADI")) Then Return

      'posso cancellare
Cancella:
      Me.Cursor = Cursors.WaitCursor

      oCleFadi.CancellaFatture(dsFadi.Tables("FADI"))

      If oCleFadi.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129276325149372344, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleFadi.LogFileName)
        End If
      End If

      SetStato()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbBolleCollegate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbBolleCollegate.ItemClick
    Dim strWhere As String = ""
    Dim lNumero As Integer = 0
    Dim nAnno As Integer = 0
    Dim strSerie As String = ""
    Dim strTipork As String = ""
    Dim oPar As New CLE__PATB
    Dim strParam As String = ""
    Try
      If dsFadi.Tables("FADI").Rows.Count = 0 Then Return
      If grvFadi.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128753936212368000, "Posizionarsi prima su un documento riepilogativo"))
        Return
      End If

      With grvFadi.NTSGetCurrentDataRow
        strWhere = oCleFadi.GetWhereHltm(!tm_tipork.ToString, NTSCInt(!tm_anno), !tm_serie.ToString, NTSCInt(!tm_numdoc))
        oPar.strDescr = strWhere
        oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione
        oPar.strTipo = !tm_tipork.ToString
        oPar.bFlag1 = True            'invece di visualizzare il comando 'conferma' visualizza 'modifica'
        NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)        'in vb6 la dohltm
      End With

      If oPar.oParam Is Nothing Then Return
      'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return
      strTipork = NTSCStr(CType(oPar.oParam, DataTable).Rows(0)!tm_tipork)
      nAnno = NTSCInt(CType(oPar.oParam, DataTable).Rows(0)!tm_anno)
      strSerie = NTSCStr(CType(oPar.oParam, DataTable).Rows(0)!tm_serie)
      lNumero = NTSCInt(CType(oPar.oParam, DataTable).Rows(0)!tm_numdoc)
      If lNumero = 0 Then Return
      strParam = "APRI;" & strTipork & ";" & _
                 nAnno.ToString.PadLeft(4, "0"c) & ";" & _
                 strSerie & ";" & _
                 lNumero.ToString.PadLeft(9, "0"c) & ";"

      '-----------------------------------------------------------------------------------------
      If oCleFadi.bORTO_ChiamaBsjoboll = True Then
        oMenu.RunChild("BSJOBOLL", "CLSJOBOLL", oApp.Tr(Me, 128753947952228000, "Gestione documenti orto"), DittaCorrente, "", "", Nothing, strParam, True, True)
      Else
        If oCleFadi.IsDocRetail(strTipork, nAnno, strSerie, lNumero) = True Then
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 130021098292564380, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
        ElseIf oCleFadi.IsDocRetailNew(strTipork, nAnno, strSerie, lNumero) = True Then
          oMenu.RunChild("BSREGSRE", "CLSREGSRE", oApp.Tr(Me, 129588246709638672, "Gestione retail"), DittaCorrente, "", "", Nothing, strParam, True, True)
        Else
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128753947612460000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
        End If
      End If

      If Not oCleFadi.IsInTestmag(strTipork, nAnno, strSerie, lNumero) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128753949926876000, "Il DDT/Ricevuta fisc. n° |" & IIf(Trim$(strSerie) = "", lNumero, lNumero & "/" & strSerie).ToString & "| è stato rimosso." & vbCrLf & vbCrLf & "Provvedere a rielaborare la fattura differita corrente."))
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128753948421008000, "Se sono state apportate modifiche al DDT/Ricevuta fisc. n° |" & IIf(Trim$(strSerie) = "", lNumero.ToString, lNumero.ToString & "/" & strSerie).ToString & "| aperto, provvedere a rielaborare la fattura differita corrente."))
      End If
      grvFadi.NTSGetCurrentDataRow!tm_flagiva_1 = "S"
      grvFadi.NTSGetCurrentDataRow.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

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

  Public Overridable Sub tlbStampaPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPdf.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante
    Dim strTabellaMov, strTabella As String
    Dim lNumMin As Integer = 0
    Dim lNumMax As Integer = 0
    Dim strSerieMin As String = ""
    Dim strSerieMax As String = ""
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Try

      If Not TestPreStampa(True) Then Return

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleFadi.GetQueryStampaPDF(dsFadi.Tables("FADI"), strQueryGetDocUnico)
      If strQueryGetDocMultipli = "" Or strQueryGetDocUnico = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644944132812500, "Errore durante la creazione della query di selezione per il modulo PDF."))
        Return
      End If

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

      strTabella = "TESTMAG"
      strTabellaMov = "MOVMAG"

      oDttgr.NTSGroupBy(dsFadi.Tables("FADI"), dttGr, "tm_tipork, tm_anno, tm_serie", _
                                                      "xx_seleziona = 'S'", _
                                                      "tm_tipork, tm_anno, tm_serie")

      lNumMin = NTSCInt(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_numdoc ASC")(0)!tm_numdoc)
      lNumMax = NTSCInt(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_numdoc DESC")(0)!tm_numdoc)
      strSerieMin = NTSCStr(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_serie ASC")(0)!tm_serie)
      strSerieMax = NTSCStr(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_serie DESC")(0)!tm_serie)

      'prima parte di PeSetSelectionFormula
      strQueryCrw32FileMultipli = "{testmag.codditt} = '" & DittaCorrente & "'" & _
                                  " And {testmag.tm_tipork} = '" & dttGr.Rows(0)!tm_tipork.ToString & "'" & _
                                  " And {testmag.tm_anno} = " & dttGr.Rows(0)!tm_anno.ToString & _
                                  " And {testmag.tm_serie} >= '" & strSerieMin & "'" & _
                                  " And {testmag.tm_serie} <= '" & strSerieMax & "'" & _
                                  " And {testmag.tm_numdoc} >= " & lNumMin.ToString & _
                                  " And {testmag.tm_numdoc} <= " & lNumMax.ToString & _
                                  " And {movmag.mm_stasino} <> 'N'" & _
                                  " And {movmag.mm_stasino} <> 'B'"
      If oCleFadi.bUsaKeymag Then strQueryCrw32FileMultipli += " AND {keymag.km_magaz} = {movmag.mm_magaz}"


      strQueryCrw32FileMultipli += " AND ("
      dtrT = dsFadi.Tables("FADI").Select("xx_seleziona = 'S'")
      For i = 0 To dtrT.Length - 1
        If i > 0 Then strQueryCrw32FileMultipli += " OR "
        strQueryCrw32FileMultipli += " {testmag.tm_numdoc} = " & dtrT(i)!tm_numdoc.ToString
      Next
      strQueryCrw32FileMultipli += ")"

      strQueryCrw32FileUnico = strQueryCrw32FileMultipli
      strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                                  " AND {" & strTabella & ".tm_anno} = |anno|" & _
                                  " AND {" & strTabella & ".tm_numdoc} = |numero|" & _
                                  " AND {" & strTabella & ".tm_serie} = |serie|" & _
                                  " AND {" & strTabella & ".tm_tipork} = |tipork|"
      strQueryCrw32FileUnico = strQueryCrw32FileUnico & _
                               " AND {" & strTabella & ".tm_valuta} = |valuta|" & _
                               " AND {" & strTabella & ".tm_scorpo} = |scorpo|"
      strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                               " AND {" & strTabella & ".tm_valuta} = |valuta|" & _
                               " AND {" & strTabella & ".tm_scorpo} = |scorpo|"

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente

      oPar.strPar1 = "BSVEFADI"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico

      Select Case dsFadi.Tables("FADI").Rows(0)!tm_tipork.ToString
        Case "D"
          oPar.strParam = "Stampa Fatture differite emesse"
        Case "P"
          oPar.strParam = "Stampa Fatture/ricevute fiscali differite"
        Case "£"
          oPar.strParam = "Stampa Note accredito differite emesse"
      End Select

      oPar.ctlPar1 = Me
      oPar.ctlPar2 = dttFormule
      oPar.ctlPar3 = New CLE__CLDP
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

      oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)

      '-------------------------------
      'da bnpdgenp è stato scelto di stampare anche su carta
      If oPar.bPar4 Then Stampa(1)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttGr.Clear()
    End Try
  End Sub

  Public Overridable Sub tlbGeneraConad_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraConad.ItemClick
    Try
      '--------------------------------------------------
      'test pre-stampa
      If Not TestPreStampa(True) Then Return
      Select Case dsFadi.Tables("FADI").Rows(0)!tm_tipork.ToString
        Case "D", "£"
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128752754139506000, _
                        "E' possibile esportare solo i documenti di tipo" & vbCrLf & _
                        "'Fattura diff. emessa' e" & vbCrLf & _
                        "'Nota i accredito differita emessa'."))
          Return
      End Select


      Me.Cursor = Cursors.WaitCursor
      oCleFadi.GeneraFileConad()
      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbSelAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelAll.ItemClick
    Try
      If dsFadi Is Nothing Then Return
      If dsFadi.Tables.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      dsFadi.AcceptChanges()

      For Each dtrT As DataRow In dsFadi.Tables("FADI").Rows
        dtrT!xx_seleziona = "S"
      Next

      Me.Cursor = Cursors.Default
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbDeselAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeselAll.ItemClick
    Try
      If dsFadi Is Nothing Then Return
      If dsFadi.Tables.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      dsFadi.AcceptChanges()

      For Each dtrT As DataRow In dsFadi.Tables("FADI").Rows
        dtrT!xx_seleziona = "N"
      Next

      Me.Cursor = Cursors.Default
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbInvertiSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInvertiSel.ItemClick
    Try
      If dsFadi Is Nothing Then Return
      If dsFadi.Tables.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      dsFadi.AcceptChanges()

      For Each dtrT As DataRow In dsFadi.Tables("FADI").Rows
        If dtrT!xx_seleziona.ToString = "S" Then
          dtrT!xx_seleziona = "N"
        Else
          dtrT!xx_seleziona = "S"
        End If
      Next

      dsFadi.AcceptChanges()

      Me.Cursor = Cursors.Default
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbNumerazioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNumerazioni.ItemClick
    Try
      oMenu.RunChild("BS__NUME", "CLS__NUME", oApp.Tr(Me, 128521252906680000, "Numerazioni"), DittaCorrente, "", "", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbEmail_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEmail.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.strPar2 = NTSCStr(grvFadi.NTSGetCurrentDataRow()!tm_tipork)
      oPar.strPar3 = NTSCStr(grvFadi.NTSGetCurrentDataRow()!tm_serie)
      oPar.dPar1 = NTSCInt(grvFadi.NTSGetCurrentDataRow()!tm_anno)
      oPar.dPar2 = NTSCInt(grvFadi.NTSGetCurrentDataRow()!tm_numdoc)

      oMenu.RunChild("NTSInformatica", "FRMEMCMAI", oApp.Tr(Me, 129048457736382788, "Console e-mail"), DittaCorrente, "", "BNEMCMAI", oPar)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function SetStato() As Boolean
    Dim nStato As Integer = 0
    Try
      lbStatus.Text = ""

      If grvFadi.RowCount = 0 Then
        nStato = 0
      Else
        nStato = 1
      End If
      Select Case nStato
        Case 0  'griglia senza documenti
          tlbRielabora.Enabled = False
          tlbCancella.Enabled = False
          tlbBolleCollegate.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tlbStampaPdf.Enabled = False
          tlbSelAll.Enabled = False
          tlbDeselAll.Enabled = False
          tlbInvertiSel.Enabled = False
          tlbGeneraConad.Enabled = False
          tlbEmail.Enabled = False
          grFadi.Visible = False
        Case 1  'griglia con documenti
          GctlSetVisEnab(tlbRielabora, False)
          GctlSetVisEnab(tlbCancella, False)
          GctlSetVisEnab(tlbBolleCollegate, False)
          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          GctlSetVisEnab(tlbStampaPdf, False)
          GctlSetVisEnab(tlbSelAll, False)
          GctlSetVisEnab(tlbDeselAll, False)
          GctlSetVisEnab(tlbInvertiSel, False)
          GctlSetVisEnab(tlbGeneraConad, False)
          GctlSetVisEnab(tlbEmail, False)
          GctlSetVisEnab(grFadi, True)
      End Select
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim dtrT() As DataRow = Nothing
    Dim nGiro As Integer = 0
    Dim strTmp As String = ""
    Dim lNumMin As Integer = 0
    Dim lNumMax As Integer = 0
    Dim strRptName As String = ""
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY

    Try
      '--------------------------------------------------
      'test pre-stampa
      If Not TestPreStampa(True) Then Return

      oDttgr.NTSGroupBy(dsFadi.Tables("FADI"), dttGr, "tm_tipork, tm_anno, tm_serie", _
                                                "xx_seleziona = 'S'", _
                                                "tm_tipork, tm_anno, tm_serie")

      lNumMin = NTSCInt(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_numdoc ASC")(0)!tm_numdoc)
      lNumMax = NTSCInt(dsFadi.Tables("FADI").Select("xx_seleziona = 'S'", "tm_numdoc DESC")(0)!tm_numdoc)

      For nGiro = 0 To 2
        'documento normale
        If nGiro = 0 Then strTmp = "xx_seleziona = 'S' AND tm_valuta = 0 AND tm_scorpo = 'N'"
        'documento in valuta
        If nGiro = 1 Then strTmp = "xx_seleziona = 'S' AND tm_valuta > 0 AND tm_scorpo = 'N'"
        'documento con scorporo
        If nGiro = 2 Then strTmp = "xx_seleziona = 'S' AND tm_valuta = 0 AND tm_scorpo = 'S'"
        dtrT = dsFadi.Tables("FADI").Select(strTmp)
        If dtrT.Length > 0 Then
          '--------------------------------------------------
          'preparo il motore di stampa
          strCrpe = "{testmag.codditt} = '" & DittaCorrente & "'" & _
                    " And {testmag.tm_tipork} = '" & dttGr.Rows(0)!tm_tipork.ToString & "'" & _
                    " And {testmag.tm_anno} = " & dttGr.Rows(0)!tm_anno.ToString & _
                    " And {testmag.tm_serie} = '" & dttGr.Rows(0)!tm_serie.ToString & "'" & _
                    " And {testmag.tm_numdoc} >= " & lNumMin.ToString & _
                    " And {testmag.tm_numdoc} <= " & lNumMax.ToString & _
                    " And {movmag.mm_stasino} <> 'N'" & _
                    " And {movmag.mm_stasino} <> 'B'"
          If oCleFadi.bUsaKeymag Then strCrpe += " AND {keymag.km_magaz} = {movmag.mm_magaz}"

          'documento normale
          If nGiro = 0 Then strCrpe += " AND {testmag.tm_scorpo} = 'N' AND {testmag.tm_valuta} = 0"
          'documento in valuta
          If nGiro = 1 Then strCrpe += " AND {testmag.tm_scorpo} = 'N' AND {testmag.tm_valuta} > 0"
          'documento con scorporo
          If nGiro = 2 Then strCrpe += " AND {testmag.tm_scorpo} = 'S' AND {testmag.tm_valuta} = 0"

          strCrpe += " AND ("
          For i = 0 To dtrT.Length - 1
            If i > 0 Then strCrpe += " OR "
            strCrpe += " {testmag.tm_numdoc} = " & dtrT(i)!tm_numdoc.ToString
          Next
          strCrpe += ")"


          Select Case dttGr.Rows(0)!tm_tipork.ToString
            Case "D", "£"
              Select Case nGiro
                Case 0 : strRptName = "BSVEFATD.RPT"
                Case 1 : strRptName = "BSVEFADV.RPT"
                Case 2 : strRptName = "BSVEFADC.RPT"
              End Select
            Case "P"
              Select Case nGiro
                Case 0 : strRptName = "BSVEFRFD.RPT"
                Case 1 : strRptName = "BSVEFRDV.RPT"
                Case 2 : strRptName = "BSVEFRDC.RPT"
              End Select
          End Select

          If nGiro = 0 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEFADI", "Reports1", dttGr.Rows(0)!tm_tipork.ToString, 0, nDestin, strRptName, False, "Stampa fatture differite", False)
          If nGiro = 1 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEFADI", "Reports3", dttGr.Rows(0)!tm_tipork.ToString, 0, nDestin, strRptName, False, "Stampa fatture differite", False)
          If nGiro = 2 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEFADI", "Reports2", dttGr.Rows(0)!tm_tipork.ToString, 0, nDestin, strRptName, False, "Stampa fatture differite", False)
          If nPjob Is Nothing Then Return

          '--------------------------------------------------
          'lancio tutti gli eventuali reports (gestisce già il multireport)
          For i = 1 To UBound(CType(nPjob, Array), 2)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NOTEFATTURA", ConvStrRpt(oCleFadi.strNoteFatture))
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Next
        End If    'If dtrT.Length > 0 Then
      Next    'For nGiro = 0 To 3

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttGr.Clear()
    End Try
  End Sub


  Public Overridable Function TestPreStampa() As Boolean
    Try
      Return TestPreStampa(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function TestPreStampa(ByVal bPerStampa As Boolean) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bPerStampa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------
      'test pre-stampa
      If dsFadi.Tables.Count = 0 Then Return False
      If dsFadi.Tables("FADI").Rows.Count = 0 Then Return False
      dsFadi.Tables("FADI").AcceptChanges()
      dtrT = dsFadi.Tables("FADI").Select("xx_seleziona = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611101258125000, "Non è stata selezionato nessun documento."))
        Return False
      End If

      If bPerStampa Then
        If dsFadi.Tables("FADI").Select("xx_seleziona = 'S' AND tm_flagiva_1 = 'S'").Length > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129302213191757813, "ATTENZIONE: tra i documenti da trattare alcuni devo essere rielaborati per il fatto che dopo essere stati generati sono stati cambiati uno o più documenti in essi contenuti"))
        End If
      End If

      '--------------------------------------------------
      'posso stampare contemporaneamente solo documenti dello stesso tipo/anno/serie
      oDttgr.NTSGroupBy(dsFadi.Tables("FADI"), dttGr, "tm_tipork, tm_anno, tm_serie", _
                                                      "xx_seleziona = 'S'", _
                                                      "tm_tipork, tm_anno, tm_serie")
      If dttGr.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128750377831608000, "Non è stato selezionato nessun documento da trattare."))
        Return False
      End If
      If dttGr.Rows.Count > 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128750375189278000, "E' possibile trattare contemporaneamente solo documenti dello stesso tipo/anno/serie."))
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttGr.Clear()
    End Try
  End Function

End Class
