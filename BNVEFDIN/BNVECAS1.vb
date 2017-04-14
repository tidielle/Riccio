Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVECAS1
  Public oCleFdin As CLEVEFDIN
  Public oCallParams As CLE__CLDP
  Public ds As New DataSet
  Public dcIva As New BindingSource()
  Public dcContr As New BindingSource()
  Public bBloccaGriglie As Boolean = True
  Public dDiffCorpoRighe As Decimal = 0

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVECAS1))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.grControp = New NTSInformatica.NTSGrid
    Me.grvControp = New NTSInformatica.NTSGridView
    Me.xx_id = New NTSInformatica.NTSGridColumn
    Me.xx_ccontr = New NTSInformatica.NTSGridColumn
    Me.xx_impcont = New NTSInformatica.NTSGridColumn
    Me.xx_impcontv = New NTSInformatica.NTSGridColumn
    Me.fmIva = New NTSInformatica.NTSGroupBox
    Me.grIva = New NTSInformatica.NTSGrid
    Me.grvIva = New NTSInformatica.NTSGridView
    Me.xx_idiva = New NTSInformatica.NTSGridColumn
    Me.xx_codiva = New NTSInformatica.NTSGridColumn
    Me.xx_imponib = New NTSInformatica.NTSGridColumn
    Me.xx_imposta = New NTSInformatica.NTSGridColumn
    Me.xx_imponibv = New NTSInformatica.NTSGridColumn
    Me.xx_impostav = New NTSInformatica.NTSGridColumn
    Me.edDtcomiva = New NTSInformatica.NTSTextBoxData
    Me.cmdControlla = New NTSInformatica.NTSButton
    Me.edDiffIva = New NTSInformatica.NTSTextBoxNum
    Me.lbDtcomiva = New NTSInformatica.NTSLabel
    Me.lbDiffIva = New NTSInformatica.NTSLabel
    Me.fmControp = New NTSInformatica.NTSGroupBox
    Me.edDiffrighecorpo = New NTSInformatica.NTSTextBoxNum
    Me.edDiffDA = New NTSInformatica.NTSTextBoxNum
    Me.lbDiffDA = New NTSInformatica.NTSLabel
    Me.lbDiffrighecorpo = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grControp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvControp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIva, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIva.SuspendLayout()
    CType(Me.grIva, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvIva, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtcomiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDiffIva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmControp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmControp.SuspendLayout()
    CType(Me.edDiffrighecorpo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDiffDA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'grControp
    '
    Me.grControp.EmbeddedNavigator.Name = ""
    Me.grControp.Location = New System.Drawing.Point(5, 57)
    Me.grControp.MainView = Me.grvControp
    Me.grControp.Name = "grControp"
    Me.grControp.Size = New System.Drawing.Size(659, 175)
    Me.grControp.TabIndex = 5
    Me.grControp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvControp})
    '
    'grvControp
    '
    Me.grvControp.ActiveFilterEnabled = False
    Me.grvControp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_id, Me.xx_ccontr, Me.xx_impcont, Me.xx_impcontv})
    Me.grvControp.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvControp.Enabled = True
    Me.grvControp.GridControl = Me.grControp
    Me.grvControp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvControp.Name = "grvControp"
    Me.grvControp.NTSAllowDelete = True
    Me.grvControp.NTSAllowInsert = True
    Me.grvControp.NTSAllowUpdate = True
    Me.grvControp.NTSMenuContext = Nothing
    Me.grvControp.OptionsCustomization.AllowRowSizing = True
    Me.grvControp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvControp.OptionsNavigation.UseTabKey = False
    Me.grvControp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvControp.OptionsView.ColumnAutoWidth = False
    Me.grvControp.OptionsView.EnableAppearanceEvenRow = True
    Me.grvControp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvControp.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvControp.OptionsView.ShowGroupPanel = False
    Me.grvControp.RowHeight = 16
    '
    'xx_id
    '
    Me.xx_id.AppearanceCell.Options.UseBackColor = True
    Me.xx_id.AppearanceCell.Options.UseTextOptions = True
    Me.xx_id.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_id.Caption = "ID"
    Me.xx_id.Enabled = False
    Me.xx_id.FieldName = "xx_id"
    Me.xx_id.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_id.Name = "xx_id"
    Me.xx_id.NTSRepositoryComboBox = Nothing
    Me.xx_id.NTSRepositoryItemCheck = Nothing
    Me.xx_id.NTSRepositoryItemText = Nothing
    Me.xx_id.OptionsColumn.AllowEdit = False
    Me.xx_id.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_id.OptionsColumn.ReadOnly = True
    Me.xx_id.OptionsFilter.AllowFilter = False
    Me.xx_id.Visible = True
    Me.xx_id.VisibleIndex = 0
    '
    'xx_ccontr
    '
    Me.xx_ccontr.AppearanceCell.Options.UseBackColor = True
    Me.xx_ccontr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ccontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ccontr.Caption = "Cod. Controp"
    Me.xx_ccontr.Enabled = True
    Me.xx_ccontr.FieldName = "xx_ccontr"
    Me.xx_ccontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ccontr.Name = "xx_ccontr"
    Me.xx_ccontr.NTSRepositoryComboBox = Nothing
    Me.xx_ccontr.NTSRepositoryItemCheck = Nothing
    Me.xx_ccontr.NTSRepositoryItemText = Nothing
    Me.xx_ccontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ccontr.OptionsFilter.AllowFilter = False
    Me.xx_ccontr.Visible = True
    Me.xx_ccontr.VisibleIndex = 1
    Me.xx_ccontr.Width = 70
    '
    'xx_impcont
    '
    Me.xx_impcont.AppearanceCell.Options.UseBackColor = True
    Me.xx_impcont.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impcont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impcont.Caption = "Importo"
    Me.xx_impcont.Enabled = True
    Me.xx_impcont.FieldName = "xx_impcont"
    Me.xx_impcont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impcont.Name = "xx_impcont"
    Me.xx_impcont.NTSRepositoryComboBox = Nothing
    Me.xx_impcont.NTSRepositoryItemCheck = Nothing
    Me.xx_impcont.NTSRepositoryItemText = Nothing
    Me.xx_impcont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impcont.OptionsFilter.AllowFilter = False
    Me.xx_impcont.Visible = True
    Me.xx_impcont.VisibleIndex = 2
    Me.xx_impcont.Width = 70
    '
    'xx_impcontv
    '
    Me.xx_impcontv.AppearanceCell.Options.UseBackColor = True
    Me.xx_impcontv.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impcontv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impcontv.Caption = "Importo valuta"
    Me.xx_impcontv.Enabled = True
    Me.xx_impcontv.FieldName = "xx_impcontv"
    Me.xx_impcontv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impcontv.Name = "xx_impcontv"
    Me.xx_impcontv.NTSRepositoryComboBox = Nothing
    Me.xx_impcontv.NTSRepositoryItemCheck = Nothing
    Me.xx_impcontv.NTSRepositoryItemText = Nothing
    Me.xx_impcontv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impcontv.OptionsFilter.AllowFilter = False
    Me.xx_impcontv.Visible = True
    Me.xx_impcontv.VisibleIndex = 3
    Me.xx_impcontv.Width = 70
    '
    'fmIva
    '
    Me.fmIva.AllowDrop = True
    Me.fmIva.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIva.Appearance.Options.UseBackColor = True
    Me.fmIva.Controls.Add(Me.grIva)
    Me.fmIva.Controls.Add(Me.edDtcomiva)
    Me.fmIva.Controls.Add(Me.cmdControlla)
    Me.fmIva.Controls.Add(Me.edDiffIva)
    Me.fmIva.Controls.Add(Me.lbDtcomiva)
    Me.fmIva.Controls.Add(Me.lbDiffIva)
    Me.fmIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIva.Dock = System.Windows.Forms.DockStyle.Top
    Me.fmIva.Location = New System.Drawing.Point(0, 26)
    Me.fmIva.Name = "fmIva"
    Me.fmIva.Size = New System.Drawing.Size(669, 174)
    Me.fmIva.TabIndex = 6
    Me.fmIva.Text = "Dettaglio IVA"
    '
    'grIva
    '
    Me.grIva.EmbeddedNavigator.Name = ""
    Me.grIva.Location = New System.Drawing.Point(5, 49)
    Me.grIva.MainView = Me.grvIva
    Me.grIva.Name = "grIva"
    Me.grIva.Size = New System.Drawing.Size(659, 119)
    Me.grIva.TabIndex = 16
    Me.grIva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvIva})
    '
    'grvIva
    '
    Me.grvIva.ActiveFilterEnabled = False
    '
    'xx_idiva
    '
    Me.xx_idiva.AppearanceCell.Options.UseBackColor = True
    Me.xx_idiva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_idiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_idiva.Caption = "ID"
    Me.xx_idiva.Enabled = False
    Me.xx_idiva.FieldName = "xx_idiva"
    Me.xx_idiva.Name = "xx_idiva"
    Me.xx_idiva.NTSRepositoryComboBox = Nothing
    Me.xx_idiva.NTSRepositoryItemCheck = Nothing
    Me.xx_idiva.NTSRepositoryItemText = Nothing
    Me.xx_idiva.OptionsColumn.AllowEdit = False
    Me.xx_idiva.OptionsColumn.ReadOnly = True
    Me.xx_idiva.Visible = True
    Me.xx_idiva.VisibleIndex = 0
    Me.grvIva.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_idiva, Me.xx_codiva, Me.xx_imponib, Me.xx_imposta, Me.xx_imponibv, Me.xx_impostav})
    Me.grvIva.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvIva.Enabled = True
    Me.grvIva.GridControl = Me.grIva
    Me.grvIva.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvIva.Name = "grvIva"
    Me.grvIva.NTSAllowDelete = True
    Me.grvIva.NTSAllowInsert = True
    Me.grvIva.NTSAllowUpdate = True
    Me.grvIva.NTSMenuContext = Nothing
    Me.grvIva.OptionsCustomization.AllowRowSizing = True
    Me.grvIva.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvIva.OptionsNavigation.UseTabKey = False
    Me.grvIva.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvIva.OptionsView.ColumnAutoWidth = False
    Me.grvIva.OptionsView.EnableAppearanceEvenRow = True
    Me.grvIva.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvIva.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvIva.OptionsView.ShowGroupPanel = False
    Me.grvIva.RowHeight = 16
    '
    'xx_codiva
    '
    Me.xx_codiva.AppearanceCell.Options.UseBackColor = True
    Me.xx_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codiva.Caption = "Cod. IVA"
    Me.xx_codiva.Enabled = True
    Me.xx_codiva.FieldName = "xx_codiva"
    Me.xx_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codiva.Name = "xx_codiva"
    Me.xx_codiva.NTSRepositoryComboBox = Nothing
    Me.xx_codiva.NTSRepositoryItemCheck = Nothing
    Me.xx_codiva.NTSRepositoryItemText = Nothing
    Me.xx_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codiva.OptionsFilter.AllowFilter = False
    Me.xx_codiva.Visible = True
    Me.xx_codiva.VisibleIndex = 1
    Me.xx_codiva.Width = 70
    '
    'xx_imponib
    '
    Me.xx_imponib.AppearanceCell.Options.UseBackColor = True
    Me.xx_imponib.AppearanceCell.Options.UseTextOptions = True
    Me.xx_imponib.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_imponib.Caption = "Imponibile"
    Me.xx_imponib.Enabled = True
    Me.xx_imponib.FieldName = "xx_imponib"
    Me.xx_imponib.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_imponib.Name = "xx_imponib"
    Me.xx_imponib.NTSRepositoryComboBox = Nothing
    Me.xx_imponib.NTSRepositoryItemCheck = Nothing
    Me.xx_imponib.NTSRepositoryItemText = Nothing
    Me.xx_imponib.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_imponib.OptionsFilter.AllowFilter = False
    Me.xx_imponib.Visible = True
    Me.xx_imponib.VisibleIndex = 2
    Me.xx_imponib.Width = 70
    '
    'xx_imposta
    '
    Me.xx_imposta.AppearanceCell.Options.UseBackColor = True
    Me.xx_imposta.AppearanceCell.Options.UseTextOptions = True
    Me.xx_imposta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_imposta.Caption = "Imposta"
    Me.xx_imposta.Enabled = True
    Me.xx_imposta.FieldName = "xx_imposta"
    Me.xx_imposta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_imposta.Name = "xx_imposta"
    Me.xx_imposta.NTSRepositoryComboBox = Nothing
    Me.xx_imposta.NTSRepositoryItemCheck = Nothing
    Me.xx_imposta.NTSRepositoryItemText = Nothing
    Me.xx_imposta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_imposta.OptionsFilter.AllowFilter = False
    Me.xx_imposta.Visible = True
    Me.xx_imposta.VisibleIndex = 3
    Me.xx_imposta.Width = 70
    '
    'xx_imponibv
    '
    Me.xx_imponibv.AppearanceCell.Options.UseBackColor = True
    Me.xx_imponibv.AppearanceCell.Options.UseTextOptions = True
    Me.xx_imponibv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_imponibv.Caption = "Imponib. valuta"
    Me.xx_imponibv.Enabled = True
    Me.xx_imponibv.FieldName = "xx_imponibv"
    Me.xx_imponibv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_imponibv.Name = "xx_imponibv"
    Me.xx_imponibv.NTSRepositoryComboBox = Nothing
    Me.xx_imponibv.NTSRepositoryItemCheck = Nothing
    Me.xx_imponibv.NTSRepositoryItemText = Nothing
    Me.xx_imponibv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_imponibv.OptionsFilter.AllowFilter = False
    Me.xx_imponibv.Visible = True
    Me.xx_imponibv.VisibleIndex = 4
    '
    'xx_impostav
    '
    Me.xx_impostav.AppearanceCell.Options.UseBackColor = True
    Me.xx_impostav.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impostav.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impostav.Caption = "Imposta valuta"
    Me.xx_impostav.Enabled = True
    Me.xx_impostav.FieldName = "xx_impostav"
    Me.xx_impostav.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impostav.Name = "xx_impostav"
    Me.xx_impostav.NTSRepositoryComboBox = Nothing
    Me.xx_impostav.NTSRepositoryItemCheck = Nothing
    Me.xx_impostav.NTSRepositoryItemText = Nothing
    Me.xx_impostav.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impostav.OptionsFilter.AllowFilter = False
    Me.xx_impostav.Visible = True
    Me.xx_impostav.VisibleIndex = 5
    '
    'edDtcomiva
    '
    Me.edDtcomiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtcomiva.Location = New System.Drawing.Point(564, 23)
    Me.edDtcomiva.Name = "edDtcomiva"
    Me.edDtcomiva.NTSDbField = ""
    Me.edDtcomiva.NTSForzaVisZoom = False
    Me.edDtcomiva.NTSOldValue = ""
    Me.edDtcomiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtcomiva.Properties.MaxLength = 65536
    Me.edDtcomiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtcomiva.Size = New System.Drawing.Size(100, 20)
    Me.edDtcomiva.TabIndex = 15
    '
    'cmdControlla
    '
    Me.cmdControlla.Location = New System.Drawing.Point(5, 23)
    Me.cmdControlla.Name = "cmdControlla"
    Me.cmdControlla.Size = New System.Drawing.Size(102, 20)
    Me.cmdControlla.TabIndex = 14
    Me.cmdControlla.Text = "Controlla"
    '
    'edDiffIva
    '
    Me.edDiffIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDiffIva.Enabled = False
    Me.edDiffIva.Location = New System.Drawing.Point(348, 23)
    Me.edDiffIva.Name = "edDiffIva"
    Me.edDiffIva.NTSDbField = ""
    Me.edDiffIva.NTSFormat = "0"
    Me.edDiffIva.NTSForzaVisZoom = False
    Me.edDiffIva.NTSOldValue = ""
    Me.edDiffIva.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiffIva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiffIva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiffIva.Properties.MaxLength = 65536
    Me.edDiffIva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiffIva.Size = New System.Drawing.Size(100, 20)
    Me.edDiffIva.TabIndex = 13
    '
    'lbDtcomiva
    '
    Me.lbDtcomiva.AutoSize = True
    Me.lbDtcomiva.BackColor = System.Drawing.Color.Transparent
    Me.lbDtcomiva.Location = New System.Drawing.Point(464, 26)
    Me.lbDtcomiva.Name = "lbDtcomiva"
    Me.lbDtcomiva.NTSDbField = ""
    Me.lbDtcomiva.Size = New System.Drawing.Size(92, 13)
    Me.lbDtcomiva.TabIndex = 11
    Me.lbDtcomiva.Text = "Data compet. IVA"
    '
    'lbDiffIva
    '
    Me.lbDiffIva.AutoSize = True
    Me.lbDiffIva.BackColor = System.Drawing.Color.Transparent
    Me.lbDiffIva.Location = New System.Drawing.Point(257, 26)
    Me.lbDiffIva.Name = "lbDiffIva"
    Me.lbDiffIva.NTSDbField = ""
    Me.lbDiffIva.Size = New System.Drawing.Size(83, 13)
    Me.lbDiffIva.TabIndex = 10
    Me.lbDiffIva.Text = "Differ. convers."
    '
    'fmControp
    '
    Me.fmControp.AllowDrop = True
    Me.fmControp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmControp.Appearance.Options.UseBackColor = True
    Me.fmControp.Controls.Add(Me.edDiffrighecorpo)
    Me.fmControp.Controls.Add(Me.edDiffDA)
    Me.fmControp.Controls.Add(Me.lbDiffDA)
    Me.fmControp.Controls.Add(Me.lbDiffrighecorpo)
    Me.fmControp.Controls.Add(Me.grControp)
    Me.fmControp.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmControp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmControp.Location = New System.Drawing.Point(0, 200)
    Me.fmControp.Name = "fmControp"
    Me.fmControp.Size = New System.Drawing.Size(669, 237)
    Me.fmControp.TabIndex = 7
    Me.fmControp.Text = "Dettaglio contropartite"
    '
    'edDiffrighecorpo
    '
    Me.edDiffrighecorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDiffrighecorpo.Enabled = False
    Me.edDiffrighecorpo.Location = New System.Drawing.Point(348, 26)
    Me.edDiffrighecorpo.Name = "edDiffrighecorpo"
    Me.edDiffrighecorpo.NTSDbField = ""
    Me.edDiffrighecorpo.NTSFormat = "0"
    Me.edDiffrighecorpo.NTSForzaVisZoom = False
    Me.edDiffrighecorpo.NTSOldValue = ""
    Me.edDiffrighecorpo.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiffrighecorpo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiffrighecorpo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiffrighecorpo.Properties.MaxLength = 65536
    Me.edDiffrighecorpo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiffrighecorpo.Size = New System.Drawing.Size(100, 20)
    Me.edDiffrighecorpo.TabIndex = 9
    '
    'edDiffDA
    '
    Me.edDiffDA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDiffDA.Enabled = False
    Me.edDiffDA.Location = New System.Drawing.Point(564, 26)
    Me.edDiffDA.Name = "edDiffDA"
    Me.edDiffDA.NTSDbField = ""
    Me.edDiffDA.NTSFormat = "0"
    Me.edDiffDA.NTSForzaVisZoom = False
    Me.edDiffDA.NTSOldValue = ""
    Me.edDiffDA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiffDA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiffDA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiffDA.Properties.MaxLength = 65536
    Me.edDiffDA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiffDA.Size = New System.Drawing.Size(100, 20)
    Me.edDiffDA.TabIndex = 8
    '
    'lbDiffDA
    '
    Me.lbDiffDA.AutoSize = True
    Me.lbDiffDA.BackColor = System.Drawing.Color.Transparent
    Me.lbDiffDA.Location = New System.Drawing.Point(464, 29)
    Me.lbDiffDA.Name = "lbDiffDA"
    Me.lbDiffDA.NTSDbField = ""
    Me.lbDiffDA.Size = New System.Drawing.Size(94, 13)
    Me.lbDiffDA.TabIndex = 7
    Me.lbDiffDA.Text = "Diff. convers. D/A"
    '
    'lbDiffrighecorpo
    '
    Me.lbDiffrighecorpo.AutoSize = True
    Me.lbDiffrighecorpo.BackColor = System.Drawing.Color.Transparent
    Me.lbDiffrighecorpo.Location = New System.Drawing.Point(257, 29)
    Me.lbDiffrighecorpo.Name = "lbDiffrighecorpo"
    Me.lbDiffrighecorpo.NTSDbField = ""
    Me.lbDiffrighecorpo.Size = New System.Drawing.Size(85, 13)
    Me.lbDiffrighecorpo.TabIndex = 6
    Me.lbDiffrighecorpo.Text = "Diff. conv. righe"
    '
    'FRMVECAS1
    '
    Me.ClientSize = New System.Drawing.Size(669, 437)
    Me.Controls.Add(Me.fmControp)
    Me.Controls.Add(Me.fmIva)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVECAS1"
    Me.NTSLastControlFocussed = Me.grControp
    Me.Text = "Dettaglio Castelletti"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grControp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvControp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIva, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIva.ResumeLayout(False)
    Me.fmIva.PerformLayout()
    CType(Me.grIva, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvIva, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtcomiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDiffIva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmControp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmControp.ResumeLayout(False)
    Me.fmControp.PerformLayout()
    CType(Me.edDiffrighecorpo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDiffDA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleFdin As CLEVEFDIN)
    oCleFdin = cleFdin
    AddHandler oCleFdin.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvIva.NTSSetParam(oMenu, "IVA CONTROPARTITE")
      xx_idiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128763416050902000, "ID"), "0", 2)
      xx_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128666390782031250, "Codice IVA"), tabciva)
      xx_imponib.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666390793125000, "Imponibile"), oApp.FormatImporti, 15)
      xx_imponibv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666390803906250, "Imponibile in valuta"), oApp.FormatImpVal, 15)
      xx_imposta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666391289531250, "Imposta"), oApp.FormatImporti, 15)
      xx_impostav.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666391300937500, "Imposta in valuta"), oApp.FormatImpVal, 15)

      grvControp.NTSSetParam(oMenu, "DETTAGLIO CONTROPARTITE")
      xx_id.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128763416083194000, "ID"), "0", 2)
      xx_ccontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128558581828004000, "Codice contropartita"), tabcove)
      xx_impcont.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128558581828160000, "Importo"), oApp.FormatImporti, 15)
      xx_impcontv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128558581828316000, "Importo in valuta"), oApp.FormatImpVal, 15)

      edDtcomiva.NTSSetParam(oMenu, oApp.Tr(Me, 128666391507656250, "Data competenza IVA"), False)
      edDiffDA.NTSSetParam(oMenu, oApp.Tr(Me, 128666392994375000, "Differ. convers."), oApp.FormatImporti)
      edDiffIva.NTSSetParam(oMenu, oApp.Tr(Me, 128666393010781250, "Diff. convers. D/A"), oApp.FormatImporti)
      edDiffrighecorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128666393022500000, "Diff. conv. righe"), oApp.FormatImporti)

      grvIva.NTSAllowInsert = False
      grvControp.NTSAllowInsert = False

      grvIva.DisableColumnSortFilter()
      grvControp.DisableColumnSortFilter()

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

  Public Overridable Sub FRMVECAS1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego il NTSBindingNavigator
      dcIva.DataSource = ds.Tables("IVA")
      dcContr.DataSource = ds.Tables("CONTROP")
      ds.AcceptChanges()

      grIva.DataSource = dcIva
      grControp.DataSource = dcContr

      edDtcomiva.Text = NTSCDate(oCleFdin.dttET.Rows(0)!tm_dtcomiva).ToShortDateString
      edDiffDA.Text = NTSCDec(oCleFdin.dttET.Rows(0)!tm_diffda).ToString
      edDiffIva.Text = NTSCDec(oCleFdin.dttET.Rows(0)!tm_diffiva).ToString
      edDiffrighecorpo.Text = dDiffCorpoRighe.ToString

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If bBloccaGriglie Then
        tlbZoom.Enabled = False
        tlbRipristina.Enabled = False
        tlbSalva.Enabled = False
        grvIva.Enabled = False
        grvControp.Enabled = False
        grvIva.NTSAllowUpdate = False
        grvControp.NTSAllowUpdate = False
        For i = 0 To grvIva.Columns.Count - 1
          CType(grvIva.Columns(i), NTSGridColumn).Enabled = False
        Next
        For i = 0 To grvControp.Columns.Count - 1
          CType(grvControp.Columns(i), NTSGridColumn).Enabled = False
        Next
      Else
        If NTSCInt(oCleFdin.dttET.Rows(0)!tm_valuta) = 0 Then
          xx_imponibv.Enabled = False
          xx_impostav.Enabled = False
          xx_impcontv.Enabled = False
        End If
      End If

      oCleFdin.CastApri(ds)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVECAS1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      grvIva.Focus()
      If Not Salva() Then
        e.Cancel = True
        Return
      End If

      grvControp.Focus()
      If Not Salva() Then
        e.Cancel = True
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub


#Region "Eventi Toolbar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grControp.ContainsFocus Then
        If Not grvControp.NTSRipristinaRigaCorrenteBefore(dcContr, True) Then Return
        oCleFdin.CastRipristinaControp(dcContr.Position, dcContr.Filter)
        grvControp.NTSRipristinaRigaCorrenteAfter()
      Else
        If Not grvIva.NTSRipristinaRigaCorrenteBefore(dcIva, True) Then Return
        oCleFdin.CastRipristinaIva(dcIva.Position, dcIva.Filter)
        grvIva.NTSRipristinaRigaCorrenteAfter()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvIva_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvIva.NTSBeforeRowUpdate
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

  Public Overridable Sub grvControp_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvControp.NTSBeforeRowUpdate
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      If grControp.ContainsFocus Then
        dRes = grvControp.NTSSalvaRigaCorrente(dcContr, oCleFdin.RecordIsChanged, False)
      Else
        dRes = grvIva.NTSSalvaRigaCorrente(dcIva, oCleFdin.RecordIsChanged, False)
      End If

      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If grControp.ContainsFocus Then
            If Not oCleFdin.CastSalvaControp(False) Then
              Return False
            End If
          Else
            If Not oCleFdin.CastSalvaIva(False) Then
              Return False
            End If
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          If grControp.ContainsFocus Then
            oCleFdin.CastRipristinaControp(dcContr.Position, dcIva.Filter)
          Else
            oCleFdin.CastRipristinaIva(dcIva.Position, dcIva.Filter)
          End If

        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub cmdControlla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdControlla.Click
    Try
      grIva.Focus()
      If Not Salva() Then Return
      oCleFdin.CastControllaCastelletti()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
