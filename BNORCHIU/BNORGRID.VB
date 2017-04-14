Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGRID
  Public oCallParams As CLE__CLDP
  Public oCleChiu As CLEORCHIU

  Private components As System.ComponentModel.IContainer

  Public dttGrid As New DataTable
  Public dcGrid As BindingSource = New BindingSource()

#Region "Inizializzazione"
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

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGRID))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSelTutto = New NTSInformatica.NTSBarButtonItem
    Me.tlbDeSelTutto = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grQuant = New NTSInformatica.NTSGrid
    Me.grvQuant = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.td_anno = New NTSInformatica.NTSGridColumn
    Me.td_serie = New NTSInformatica.NTSGridColumn
    Me.td_numord = New NTSInformatica.NTSGridColumn
    Me.td_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.td_codagen = New NTSInformatica.NTSGridColumn
    Me.xx_codagen = New NTSInformatica.NTSGridColumn
    Me.td_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.td_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.td_vettor = New NTSInformatica.NTSGridColumn
    Me.xx_vettor = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbDescr = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grQuant, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvQuant, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbEsci, Me.tlbSelTutto, Me.tlbDeSelTutto})
    Me.NtsBarManager1.MaxItemId = 23
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelTutto, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeSelTutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSelTutto
    '
    Me.tlbSelTutto.Caption = "Seleziona Tutto"
    Me.tlbSelTutto.Glyph = CType(resources.GetObject("tlbSelTutto.Glyph"), System.Drawing.Image)
    Me.tlbSelTutto.Id = 21
    Me.tlbSelTutto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbSelTutto.Name = "tlbSelTutto"
    Me.tlbSelTutto.Visible = True
    '
    'tlbDeSelTutto
    '
    Me.tlbDeSelTutto.Caption = "Deseleziona tutto"
    Me.tlbDeSelTutto.Glyph = CType(resources.GetObject("tlbDeSelTutto.Glyph"), System.Drawing.Image)
    Me.tlbDeSelTutto.Id = 22
    Me.tlbDeSelTutto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDeSelTutto.Name = "tlbDeSelTutto"
    Me.tlbDeSelTutto.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grQuant
    '
    Me.grQuant.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grQuant.EmbeddedNavigator.Name = ""
    Me.grQuant.Location = New System.Drawing.Point(0, 63)
    Me.grQuant.MainView = Me.grvQuant
    Me.grQuant.Name = "grQuant"
    Me.grQuant.Size = New System.Drawing.Size(727, 325)
    Me.grQuant.TabIndex = 4
    Me.grQuant.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvQuant})
    '
    'grvQuant
    '
    Me.grvQuant.ActiveFilterEnabled = False
    Me.grvQuant.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.td_anno, Me.td_serie, Me.td_numord, Me.td_conto, Me.xx_conto, Me.td_codagen, Me.xx_codagen, Me.td_tipobf, Me.xx_tipobf, Me.td_coddest, Me.xx_coddest, Me.td_vettor, Me.xx_vettor})
    Me.grvQuant.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvQuant.Enabled = True
    Me.grvQuant.GridControl = Me.grQuant
    Me.grvQuant.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvQuant.MinRowHeight = 14
    Me.grvQuant.Name = "grvQuant"
    Me.grvQuant.NTSAllowDelete = True
    Me.grvQuant.NTSAllowInsert = True
    Me.grvQuant.NTSAllowUpdate = True
    Me.grvQuant.NTSMenuContext = Nothing
    Me.grvQuant.OptionsCustomization.AllowRowSizing = True
    Me.grvQuant.OptionsFilter.AllowFilterEditor = False
    Me.grvQuant.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvQuant.OptionsNavigation.UseTabKey = False
    Me.grvQuant.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvQuant.OptionsView.ColumnAutoWidth = False
    Me.grvQuant.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvQuant.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvQuant.OptionsView.ShowGroupPanel = False
    Me.grvQuant.RowHeight = 14
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Seleziona"
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.NTSRepositoryComboBox = Nothing
    Me.xx_sel.NTSRepositoryItemCheck = Nothing
    Me.xx_sel.NTSRepositoryItemMemo = Nothing
    Me.xx_sel.NTSRepositoryItemText = Nothing
    Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_sel.OptionsFilter.AllowFilter = False
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'td_anno
    '
    Me.td_anno.AppearanceCell.Options.UseBackColor = True
    Me.td_anno.AppearanceCell.Options.UseTextOptions = True
    Me.td_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_anno.Caption = "Anno"
    Me.td_anno.Enabled = False
    Me.td_anno.FieldName = "td_anno"
    Me.td_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_anno.Name = "td_anno"
    Me.td_anno.NTSRepositoryComboBox = Nothing
    Me.td_anno.NTSRepositoryItemCheck = Nothing
    Me.td_anno.NTSRepositoryItemMemo = Nothing
    Me.td_anno.NTSRepositoryItemText = Nothing
    Me.td_anno.OptionsColumn.AllowEdit = False
    Me.td_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_anno.OptionsColumn.ReadOnly = True
    Me.td_anno.OptionsFilter.AllowFilter = False
    Me.td_anno.Visible = True
    Me.td_anno.VisibleIndex = 1
    '
    'td_serie
    '
    Me.td_serie.AppearanceCell.Options.UseBackColor = True
    Me.td_serie.AppearanceCell.Options.UseTextOptions = True
    Me.td_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_serie.Caption = "Serie"
    Me.td_serie.Enabled = False
    Me.td_serie.FieldName = "td_serie"
    Me.td_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_serie.Name = "td_serie"
    Me.td_serie.NTSRepositoryComboBox = Nothing
    Me.td_serie.NTSRepositoryItemCheck = Nothing
    Me.td_serie.NTSRepositoryItemMemo = Nothing
    Me.td_serie.NTSRepositoryItemText = Nothing
    Me.td_serie.OptionsColumn.AllowEdit = False
    Me.td_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_serie.OptionsColumn.ReadOnly = True
    Me.td_serie.OptionsFilter.AllowFilter = False
    Me.td_serie.Visible = True
    Me.td_serie.VisibleIndex = 2
    '
    'td_numord
    '
    Me.td_numord.AppearanceCell.Options.UseBackColor = True
    Me.td_numord.AppearanceCell.Options.UseTextOptions = True
    Me.td_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numord.Caption = "Numero"
    Me.td_numord.Enabled = False
    Me.td_numord.FieldName = "td_numord"
    Me.td_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numord.Name = "td_numord"
    Me.td_numord.NTSRepositoryComboBox = Nothing
    Me.td_numord.NTSRepositoryItemCheck = Nothing
    Me.td_numord.NTSRepositoryItemMemo = Nothing
    Me.td_numord.NTSRepositoryItemText = Nothing
    Me.td_numord.OptionsColumn.AllowEdit = False
    Me.td_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numord.OptionsColumn.ReadOnly = True
    Me.td_numord.OptionsFilter.AllowFilter = False
    Me.td_numord.Visible = True
    Me.td_numord.VisibleIndex = 3
    '
    'td_conto
    '
    Me.td_conto.AppearanceCell.Options.UseBackColor = True
    Me.td_conto.AppearanceCell.Options.UseTextOptions = True
    Me.td_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_conto.Caption = "Conto"
    Me.td_conto.Enabled = False
    Me.td_conto.FieldName = "td_conto"
    Me.td_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_conto.Name = "td_conto"
    Me.td_conto.NTSRepositoryComboBox = Nothing
    Me.td_conto.NTSRepositoryItemCheck = Nothing
    Me.td_conto.NTSRepositoryItemMemo = Nothing
    Me.td_conto.NTSRepositoryItemText = Nothing
    Me.td_conto.OptionsColumn.AllowEdit = False
    Me.td_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_conto.OptionsColumn.ReadOnly = True
    Me.td_conto.OptionsFilter.AllowFilter = False
    Me.td_conto.Visible = True
    Me.td_conto.VisibleIndex = 4
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Ragione sociale"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "Ragione sociale"
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
    Me.xx_conto.VisibleIndex = 5
    '
    'td_codagen
    '
    Me.td_codagen.AppearanceCell.Options.UseBackColor = True
    Me.td_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.td_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_codagen.Caption = "Codice agente"
    Me.td_codagen.Enabled = False
    Me.td_codagen.FieldName = "td_codagen"
    Me.td_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_codagen.Name = "td_codagen"
    Me.td_codagen.NTSRepositoryComboBox = Nothing
    Me.td_codagen.NTSRepositoryItemCheck = Nothing
    Me.td_codagen.NTSRepositoryItemMemo = Nothing
    Me.td_codagen.NTSRepositoryItemText = Nothing
    Me.td_codagen.OptionsColumn.AllowEdit = False
    Me.td_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_codagen.OptionsColumn.ReadOnly = True
    Me.td_codagen.OptionsFilter.AllowFilter = False
    Me.td_codagen.Visible = True
    Me.td_codagen.VisibleIndex = 6
    '
    'xx_codagen
    '
    Me.xx_codagen.AppearanceCell.Options.UseBackColor = True
    Me.xx_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codagen.Caption = "Descrizione agente"
    Me.xx_codagen.Enabled = False
    Me.xx_codagen.FieldName = "xx_codagen"
    Me.xx_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codagen.Name = "xx_codagen"
    Me.xx_codagen.NTSRepositoryComboBox = Nothing
    Me.xx_codagen.NTSRepositoryItemCheck = Nothing
    Me.xx_codagen.NTSRepositoryItemMemo = Nothing
    Me.xx_codagen.NTSRepositoryItemText = Nothing
    Me.xx_codagen.OptionsColumn.AllowEdit = False
    Me.xx_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codagen.OptionsColumn.ReadOnly = True
    Me.xx_codagen.OptionsFilter.AllowFilter = False
    Me.xx_codagen.Visible = True
    Me.xx_codagen.VisibleIndex = 7
    '
    'td_tipobf
    '
    Me.td_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.td_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipobf.Caption = "Tipo bolla/fattura"
    Me.td_tipobf.Enabled = False
    Me.td_tipobf.FieldName = "td_tipobf"
    Me.td_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipobf.Name = "td_tipobf"
    Me.td_tipobf.NTSRepositoryComboBox = Nothing
    Me.td_tipobf.NTSRepositoryItemCheck = Nothing
    Me.td_tipobf.NTSRepositoryItemMemo = Nothing
    Me.td_tipobf.NTSRepositoryItemText = Nothing
    Me.td_tipobf.OptionsColumn.AllowEdit = False
    Me.td_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_tipobf.OptionsColumn.ReadOnly = True
    Me.td_tipobf.OptionsFilter.AllowFilter = False
    Me.td_tipobf.Visible = True
    Me.td_tipobf.VisibleIndex = 8
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descrizione bolla/fattura"
    Me.xx_tipobf.Enabled = False
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowEdit = False
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipobf.OptionsColumn.ReadOnly = True
    Me.xx_tipobf.OptionsFilter.AllowFilter = False
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 9
    '
    'td_coddest
    '
    Me.td_coddest.AppearanceCell.Options.UseBackColor = True
    Me.td_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.td_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_coddest.Caption = "Codice destinazione"
    Me.td_coddest.Enabled = False
    Me.td_coddest.FieldName = "td_coddest"
    Me.td_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_coddest.Name = "td_coddest"
    Me.td_coddest.NTSRepositoryComboBox = Nothing
    Me.td_coddest.NTSRepositoryItemCheck = Nothing
    Me.td_coddest.NTSRepositoryItemMemo = Nothing
    Me.td_coddest.NTSRepositoryItemText = Nothing
    Me.td_coddest.OptionsColumn.AllowEdit = False
    Me.td_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_coddest.OptionsColumn.ReadOnly = True
    Me.td_coddest.OptionsFilter.AllowFilter = False
    Me.td_coddest.Visible = True
    Me.td_coddest.VisibleIndex = 10
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descrizione destinazione"
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 11
    '
    'td_vettor
    '
    Me.td_vettor.AppearanceCell.Options.UseBackColor = True
    Me.td_vettor.AppearanceCell.Options.UseTextOptions = True
    Me.td_vettor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_vettor.Caption = "Codice vettore"
    Me.td_vettor.Enabled = False
    Me.td_vettor.FieldName = "td_vettor"
    Me.td_vettor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_vettor.Name = "td_vettor"
    Me.td_vettor.NTSRepositoryComboBox = Nothing
    Me.td_vettor.NTSRepositoryItemCheck = Nothing
    Me.td_vettor.NTSRepositoryItemMemo = Nothing
    Me.td_vettor.NTSRepositoryItemText = Nothing
    Me.td_vettor.OptionsColumn.AllowEdit = False
    Me.td_vettor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_vettor.OptionsColumn.ReadOnly = True
    Me.td_vettor.OptionsFilter.AllowFilter = False
    '
    'xx_vettor
    '
    Me.xx_vettor.AppearanceCell.Options.UseBackColor = True
    Me.xx_vettor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_vettor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_vettor.Caption = "Descrizione vettore"
    Me.xx_vettor.Enabled = True
    Me.xx_vettor.FieldName = "xx_vettor"
    Me.xx_vettor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_vettor.Name = "xx_vettor"
    Me.xx_vettor.NTSRepositoryComboBox = Nothing
    Me.xx_vettor.NTSRepositoryItemCheck = Nothing
    Me.xx_vettor.NTSRepositoryItemMemo = Nothing
    Me.xx_vettor.NTSRepositoryItemText = Nothing
    Me.xx_vettor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_vettor.OptionsFilter.AllowFilter = False
    Me.xx_vettor.Visible = True
    Me.xx_vettor.VisibleIndex = 12
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
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(727, 33)
    Me.pnTop.TabIndex = 94
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbDescr
    '
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbDescr.Location = New System.Drawing.Point(12, 5)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(384, 20)
    Me.lbDescr.TabIndex = 83
    Me.lbDescr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'FRMORGRID
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(727, 388)
    Me.Controls.Add(Me.grQuant)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORGRID"
    Me.Text = "DETTAGLIO TESTATE ORDINI/IMPEGNI DA CHIUDERE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grQuant, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvQuant, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '---------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '---------------------------------------------------------------------------------------------------------------
    Try
      '-------------------------------------------------------------------------------------------------------------
      Try
        tlbSelTutto.GlyphPath = (oApp.ChildImageDir & "\add_filter.gif")
        tlbDeSelTutto.GlyphPath = (oApp.ChildImageDir & "\del_filter.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------------------------------------------------------------------
      grvQuant.NTSSetParam(oMenu, oApp.Tr(Me, 130185419399059478, "Griglia"))
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130185419399069453, "Seleziona"), "S", "N")
      td_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130492167370954424, "Anno"), "0", 9, 0, 999999999)
      td_serie.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130185419399089466, "Serie"), "0", 4, 0, 9999)
      td_numord.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130185419399099479, "Numero"), 1, True)
      td_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130185419399109487, "Conto"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130185467345900736, "Ragione sociale"), 0, True)
      td_codagen.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130185419399129492, "Codice agente"), tabcage)
      xx_codagen.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130185419399139500, "Descrizione agente"), 0, True)
      td_tipobf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130185419399149504, "Tipo bolla/fattura"), tabtpbf)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130185419399159513, "Descrizione bolla/fattura"), 0, True)
      td_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130185419399169517, "Codice destinazione"), tabdestdiv)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130185419399179525, "Descrizione destinazione"), 0, True)
      td_vettor.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130185419399189530, "Codice vettore"), tabvett)
      xx_vettor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130186248293303940, "Descrizione vettore"), 0, True)
      '-------------------------------------------------------------------------------------------------------------
      grvQuant.NTSAllowInsert = False
      grvQuant.NTSAllowDelete = False
      grvQuant.NTSAllowUpdate = True
      '-------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '---------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '---------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleChiu As CLEORCHIU)
    oCleChiu = cleChiu
    AddHandler oCleChiu.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMORGRID_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      dttGrid = oCleChiu.dttTest
      dttGrid.AcceptChanges()
      dcGrid.DataSource = dttGrid
      grQuant.DataSource = dcGrid
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORGRID_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If dttGrid Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      dttGrid.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If dttGrid.Rows.Count > 0 Then
        If dttGrid.Select("xx_sel = 'S'").Length = 0 Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130492193345016273, "Attenzione!" & vbCrLf & _
            "Non sono stati selezionati ordini da chiudere." & vbCrLf & _
            "Proseguire annullando l'operazione?")) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Return
          Else
            dttGrid.Clear()
            dttGrid.Dispose()
          End If
        Else
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130492197151632455, "Attenzione!" & vbCrLf & _
            "Confermare l'operazione di chiusura degli ordini selezionati?")) = Windows.Forms.DialogResult.No Then
            dttGrid.Clear()
            dttGrid.Dispose()
          Else
            For i As Integer = (dttGrid.Rows.Count - 1) To 0 Step -1
              If NTSCStr(dttGrid.Rows(i)!xx_sel) = "N" Then dttGrid.Rows(i).Delete()
              dttGrid.AcceptChanges()
            Next
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbSelTutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelTutto.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeseleziona("S")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDeSelTutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeSelTutto.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeseleziona("N")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub SelezionaDeseleziona(ByVal strSel As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      For z As Integer = 0 To (dttGrid.Rows.Count - 1)
        dttGrid.Rows(z)!xx_sel = strSel
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class