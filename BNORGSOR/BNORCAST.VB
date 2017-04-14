Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCAST
  Public oCleGsor As CLEORGSOR
  Public oCallParams As CLE__CLDP

  Public ds As New DataSet
  Public dcIva As New BindingSource()
  Public dcContr As New BindingSource()

  Public dDiffIva As Decimal = 0
  Public dDiffDA As Decimal = 0
  Public dDiffCorpoRighe As Decimal = 0

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, _
    Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCAST))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
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
    Me.edDiffIva = New NTSInformatica.NTSTextBoxNum
    Me.lbDtcomiva = New NTSInformatica.NTSLabel
    Me.lbDiffIva = New NTSInformatica.NTSLabel
    Me.fmControp = New NTSInformatica.NTSGroupBox
    Me.edDiffrighecorpo = New NTSInformatica.NTSTextBoxNum
    Me.edDiffDA = New NTSInformatica.NTSTextBoxNum
    Me.lbDiffDA = New NTSInformatica.NTSLabel
    Me.lbDiffrighecorpo = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    'grControp
    '
    Me.grControp.EmbeddedNavigator.Name = ""
    Me.grControp.Location = New System.Drawing.Point(5, 57)
    Me.grControp.MainView = Me.grvControp
    Me.grControp.Name = "grControp"
    Me.grControp.Size = New System.Drawing.Size(659, 235)
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
    Me.grvControp.MinRowHeight = 14
    Me.grvControp.Name = "grvControp"
    Me.grvControp.NTSAllowDelete = True
    Me.grvControp.NTSAllowInsert = True
    Me.grvControp.NTSAllowUpdate = True
    Me.grvControp.NTSMenuContext = Nothing
    Me.grvControp.OptionsCustomization.AllowRowSizing = True
    Me.grvControp.OptionsFilter.AllowFilterEditor = False
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
    Me.xx_id.NTSRepositoryItemMemo = Nothing
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
    Me.xx_ccontr.NTSRepositoryItemMemo = Nothing
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
    Me.xx_impcont.NTSRepositoryItemMemo = Nothing
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
    Me.xx_impcontv.NTSRepositoryItemMemo = Nothing
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
    Me.fmIva.Controls.Add(Me.edDiffIva)
    Me.fmIva.Controls.Add(Me.lbDtcomiva)
    Me.fmIva.Controls.Add(Me.lbDiffIva)
    Me.fmIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIva.Dock = System.Windows.Forms.DockStyle.Top
    Me.fmIva.Location = New System.Drawing.Point(0, 30)
    Me.fmIva.Name = "fmIva"
    Me.fmIva.Size = New System.Drawing.Size(669, 194)
    Me.fmIva.TabIndex = 6
    Me.fmIva.Text = "Dettaglio IVA"
    '
    'grIva
    '
    Me.grIva.EmbeddedNavigator.Name = ""
    Me.grIva.Location = New System.Drawing.Point(5, 49)
    Me.grIva.MainView = Me.grvIva
    Me.grIva.Name = "grIva"
    Me.grIva.Size = New System.Drawing.Size(659, 140)
    Me.grIva.TabIndex = 16
    Me.grIva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvIva})
    '
    'grvIva
    '
    Me.grvIva.ActiveFilterEnabled = False
    Me.grvIva.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_idiva, Me.xx_codiva, Me.xx_imponib, Me.xx_imposta, Me.xx_imponibv, Me.xx_impostav})
    Me.grvIva.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvIva.Enabled = True
    Me.grvIva.GridControl = Me.grIva
    Me.grvIva.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvIva.MinRowHeight = 14
    Me.grvIva.Name = "grvIva"
    Me.grvIva.NTSAllowDelete = True
    Me.grvIva.NTSAllowInsert = True
    Me.grvIva.NTSAllowUpdate = True
    Me.grvIva.NTSMenuContext = Nothing
    Me.grvIva.OptionsCustomization.AllowRowSizing = True
    Me.grvIva.OptionsFilter.AllowFilterEditor = False
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
    'xx_idiva
    '
    Me.xx_idiva.AppearanceCell.Options.UseBackColor = True
    Me.xx_idiva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_idiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_idiva.Caption = "ID"
    Me.xx_idiva.Enabled = False
    Me.xx_idiva.FieldName = "xx_idiva"
    Me.xx_idiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_idiva.Name = "xx_idiva"
    Me.xx_idiva.NTSRepositoryComboBox = Nothing
    Me.xx_idiva.NTSRepositoryItemCheck = Nothing
    Me.xx_idiva.NTSRepositoryItemMemo = Nothing
    Me.xx_idiva.NTSRepositoryItemText = Nothing
    Me.xx_idiva.OptionsColumn.AllowEdit = False
    Me.xx_idiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_idiva.OptionsColumn.ReadOnly = True
    Me.xx_idiva.OptionsFilter.AllowFilter = False
    Me.xx_idiva.Visible = True
    Me.xx_idiva.VisibleIndex = 0
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
    Me.xx_codiva.NTSRepositoryItemMemo = Nothing
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
    Me.xx_imponib.NTSRepositoryItemMemo = Nothing
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
    Me.xx_imposta.NTSRepositoryItemMemo = Nothing
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
    Me.xx_imponibv.NTSRepositoryItemMemo = Nothing
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
    Me.xx_impostav.NTSRepositoryItemMemo = Nothing
    Me.xx_impostav.NTSRepositoryItemText = Nothing
    Me.xx_impostav.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impostav.OptionsFilter.AllowFilter = False
    Me.xx_impostav.Visible = True
    Me.xx_impostav.VisibleIndex = 5
    '
    'edDtcomiva
    '
    Me.edDtcomiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtcomiva.Enabled = False
    Me.edDtcomiva.Location = New System.Drawing.Point(564, 23)
    Me.edDtcomiva.Name = "edDtcomiva"
    Me.edDtcomiva.NTSDbField = ""
    Me.edDtcomiva.NTSForzaVisZoom = False
    Me.edDtcomiva.NTSOldValue = ""
    Me.edDtcomiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtcomiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtcomiva.Properties.AutoHeight = False
    Me.edDtcomiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtcomiva.Properties.MaxLength = 65536
    Me.edDtcomiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtcomiva.Size = New System.Drawing.Size(100, 20)
    Me.edDtcomiva.TabIndex = 15
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
    Me.edDiffIva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDiffIva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDiffIva.Properties.AutoHeight = False
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
    Me.lbDtcomiva.Tooltip = ""
    Me.lbDtcomiva.UseMnemonic = False
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
    Me.lbDiffIva.Tooltip = ""
    Me.lbDiffIva.UseMnemonic = False
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
    Me.fmControp.Location = New System.Drawing.Point(0, 224)
    Me.fmControp.Name = "fmControp"
    Me.fmControp.Size = New System.Drawing.Size(669, 298)
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
    Me.edDiffrighecorpo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDiffrighecorpo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDiffrighecorpo.Properties.AutoHeight = False
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
    Me.edDiffDA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDiffDA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDiffDA.Properties.AutoHeight = False
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
    Me.lbDiffDA.Tooltip = ""
    Me.lbDiffDA.UseMnemonic = False
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
    Me.lbDiffrighecorpo.Tooltip = ""
    Me.lbDiffrighecorpo.UseMnemonic = False
    '
    'FRMORCAST
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(669, 522)
    Me.Controls.Add(Me.fmControp)
    Me.Controls.Add(Me.fmIva)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORCAST"
    Me.NTSLastControlFocussed = Me.grControp
    Me.Text = "DETTAGLIO CASTELLETTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvIva.NTSSetParam(oMenu, "IVA CONTROPARTITE")
      xx_idiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128776116791760000, "ID"), "0", 2)
      xx_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128666390782031250, "Codice IVA"), tabciva)
      xx_imponib.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666390793125000, "Imponibile"), oApp.FormatImporti, 15)
      xx_imponibv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666390803906250, "Imponibile in valuta"), oApp.FormatImpVal, 15)
      xx_imposta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666391289531250, "Imposta"), oApp.FormatImporti, 15)
      xx_impostav.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666391300937500, "Imposta in valuta"), oApp.FormatImpVal, 15)
      '--------------------------------------------------------------------------------------------------------------
      grvControp.NTSSetParam(oMenu, "DETTAGLIO CONTROPARTITE")
      xx_id.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128666500754062500, "ID"), "0", 2)
      xx_ccontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128558581828004000, "Codice contropartita"), tabcove)
      xx_impcont.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128558581828160000, "Importo"), oApp.FormatImporti, 15)
      xx_impcontv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128558581828316000, "Importo in valuta"), oApp.FormatImpVal, 15)
      '--------------------------------------------------------------------------------------------------------------
      edDtcomiva.NTSSetParam(oMenu, oApp.Tr(Me, 128666391507656250, "Data competenza IVA"), False)
      edDiffDA.NTSSetParam(oMenu, oApp.Tr(Me, 128666392994375000, "Differ. convers."), oApp.FormatImporti)
      edDiffIva.NTSSetParam(oMenu, oApp.Tr(Me, 128666393010781250, "Diff. convers. D/A"), oApp.FormatImporti)
      edDiffrighecorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128666393022500000, "Diff. conv. righe"), oApp.FormatImporti)
      '--------------------------------------------------------------------------------------------------------------
      grvIva.NTSAllowInsert = False
      grvIva.NTSAllowDelete = False
      grvIva.NTSAllowUpdate = False
      grvIva.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      grvControp.NTSAllowInsert = False
      grvControp.NTSAllowDelete = False
      grvControp.NTSAllowUpdate = False
      grvControp.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      grvIva.DisableColumnSortFilter()
      grvControp.DisableColumnSortFilter()
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

  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR)
    oCleGsor = cleGsor
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region
  
#Region "Eventi di Form"
  Public Overridable Sub FRMORCAST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      dcIva.DataSource = ds.Tables("IVA")
      dcContr.DataSource = ds.Tables("CONTROP")
      ds.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grIva.DataSource = dcIva
      grControp.DataSource = dcContr
      '--------------------------------------------------------------------------------------------------------------      
      edDtcomiva.Text = NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc).ToShortDateString
      edDiffDA.Text = dDiffDA.ToString
      edDiffIva.Text = dDiffIva.ToString
      edDiffrighecorpo.Text = dDiffCorpoRighe.ToString
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      oCleGsor.CastApri(ds)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

End Class