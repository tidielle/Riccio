Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__LOCA
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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

  Public oCleLoca As CLE__LOCA
  Public oCallParams As CLE__CLDP
  Public dsLoca As DataSet
  Public dcLoca As BindingSource = New BindingSource()
  Public strFile As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents grLoca As NTSInformatica.NTSGrid
  Public WithEvents grvLoca As NTSInformatica.NTSGridView
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_indir As NTSInformatica.NTSGridColumn
  Public WithEvents xx_cap As NTSInformatica.NTSGridColumn
  Public WithEvents xx_citta As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prov As NTSInformatica.NTSGridColumn
  Public WithEvents xx_telef As NTSInformatica.NTSGridColumn
  Public WithEvents xx_cell As NTSInformatica.NTSGridColumn
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdVisualizza As NTSInformatica.NTSButton
  Public WithEvents cmdDeselAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents xx_order As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Me.grLoca = New NTSInformatica.NTSGrid
    Me.grvLoca = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.xx_order = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn
    Me.xx_indir = New NTSInformatica.NTSGridColumn
    Me.xx_cap = New NTSInformatica.NTSGridColumn
    Me.xx_citta = New NTSInformatica.NTSGridColumn
    Me.xx_prov = New NTSInformatica.NTSGridColumn
    Me.xx_telef = New NTSInformatica.NTSGridColumn
    Me.xx_cell = New NTSInformatica.NTSGridColumn
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdVisualizza = New NTSInformatica.NTSButton
    Me.cmdDeselAll = New NTSInformatica.NTSButton
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.edStart = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.edEnd = New NTSInformatica.NTSTextBoxStr
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbNota = New NTSInformatica.NTSLabel
    CType(Me.grLoca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLoca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.edStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grLoca
    '
    Me.grLoca.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grLoca.EmbeddedNavigator.Name = ""
    Me.grLoca.Location = New System.Drawing.Point(0, 0)
    Me.grLoca.MainView = Me.grvLoca
    Me.grLoca.Name = "grLoca"
    Me.grLoca.Size = New System.Drawing.Size(528, 372)
    Me.grLoca.TabIndex = 5
    Me.grLoca.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLoca})
    '
    'grvLoca
    '
    Me.grvLoca.ActiveFilterEnabled = False
    Me.grvLoca.Appearance.FocusedCell.BackColor = System.Drawing.Color.FloralWhite
    Me.grvLoca.Appearance.FocusedCell.Options.UseBackColor = True
    Me.grvLoca.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.xx_order, Me.xx_conto, Me.xx_coddest, Me.xx_descr1, Me.xx_indir, Me.xx_cap, Me.xx_citta, Me.xx_prov, Me.xx_telef, Me.xx_cell})
    Me.grvLoca.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvLoca.Enabled = True
    Me.grvLoca.GridControl = Me.grLoca
    Me.grvLoca.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLoca.Name = "grvLoca"
    Me.grvLoca.NTSAllowDelete = True
    Me.grvLoca.NTSAllowInsert = True
    Me.grvLoca.NTSAllowUpdate = True
    Me.grvLoca.NTSMenuContext = Nothing
    Me.grvLoca.OptionsCustomization.AllowRowSizing = True
    Me.grvLoca.OptionsFilter.AllowFilterEditor = False
    Me.grvLoca.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLoca.OptionsNavigation.UseTabKey = False
    Me.grvLoca.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLoca.OptionsView.ColumnAutoWidth = False
    Me.grvLoca.OptionsView.EnableAppearanceEvenRow = True
    Me.grvLoca.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLoca.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLoca.OptionsView.ShowGroupPanel = False
    Me.grvLoca.RowHeight = 16
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Selez."
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
    'xx_order
    '
    Me.xx_order.AppearanceCell.Options.UseBackColor = True
    Me.xx_order.AppearanceCell.Options.UseTextOptions = True
    Me.xx_order.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_order.Caption = "Ordinam."
    Me.xx_order.Enabled = True
    Me.xx_order.FieldName = "xx_order"
    Me.xx_order.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_order.Name = "xx_order"
    Me.xx_order.NTSRepositoryComboBox = Nothing
    Me.xx_order.NTSRepositoryItemCheck = Nothing
    Me.xx_order.NTSRepositoryItemMemo = Nothing
    Me.xx_order.NTSRepositoryItemText = Nothing
    Me.xx_order.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_order.OptionsFilter.AllowFilter = False
    Me.xx_order.Visible = True
    Me.xx_order.VisibleIndex = 1
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Codice"
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
    Me.xx_conto.VisibleIndex = 2
    Me.xx_conto.Width = 70
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Cod. destinaz"
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
    Me.xx_coddest.VisibleIndex = 3
    Me.xx_coddest.Width = 70
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Descrizione"
    Me.xx_descr1.Enabled = False
    Me.xx_descr1.FieldName = "xx_descr1"
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.NTSRepositoryComboBox = Nothing
    Me.xx_descr1.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1.NTSRepositoryItemText = Nothing
    Me.xx_descr1.OptionsColumn.AllowEdit = False
    Me.xx_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1.OptionsColumn.ReadOnly = True
    Me.xx_descr1.OptionsFilter.AllowFilter = False
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 4
    Me.xx_descr1.Width = 70
    '
    'xx_indir
    '
    Me.xx_indir.AppearanceCell.Options.UseBackColor = True
    Me.xx_indir.AppearanceCell.Options.UseTextOptions = True
    Me.xx_indir.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_indir.Caption = "Indirizzo"
    Me.xx_indir.Enabled = False
    Me.xx_indir.FieldName = "xx_indir"
    Me.xx_indir.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_indir.Name = "xx_indir"
    Me.xx_indir.NTSRepositoryComboBox = Nothing
    Me.xx_indir.NTSRepositoryItemCheck = Nothing
    Me.xx_indir.NTSRepositoryItemMemo = Nothing
    Me.xx_indir.NTSRepositoryItemText = Nothing
    Me.xx_indir.OptionsColumn.AllowEdit = False
    Me.xx_indir.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_indir.OptionsColumn.ReadOnly = True
    Me.xx_indir.OptionsFilter.AllowFilter = False
    Me.xx_indir.Visible = True
    Me.xx_indir.VisibleIndex = 5
    Me.xx_indir.Width = 70
    '
    'xx_cap
    '
    Me.xx_cap.AppearanceCell.Options.UseBackColor = True
    Me.xx_cap.AppearanceCell.Options.UseTextOptions = True
    Me.xx_cap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_cap.Caption = "Cap"
    Me.xx_cap.Enabled = False
    Me.xx_cap.FieldName = "xx_cap"
    Me.xx_cap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_cap.Name = "xx_cap"
    Me.xx_cap.NTSRepositoryComboBox = Nothing
    Me.xx_cap.NTSRepositoryItemCheck = Nothing
    Me.xx_cap.NTSRepositoryItemMemo = Nothing
    Me.xx_cap.NTSRepositoryItemText = Nothing
    Me.xx_cap.OptionsColumn.AllowEdit = False
    Me.xx_cap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_cap.OptionsColumn.ReadOnly = True
    Me.xx_cap.OptionsFilter.AllowFilter = False
    Me.xx_cap.Visible = True
    Me.xx_cap.VisibleIndex = 6
    Me.xx_cap.Width = 70
    '
    'xx_citta
    '
    Me.xx_citta.AppearanceCell.Options.UseBackColor = True
    Me.xx_citta.AppearanceCell.Options.UseTextOptions = True
    Me.xx_citta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_citta.Caption = "Città"
    Me.xx_citta.Enabled = False
    Me.xx_citta.FieldName = "xx_citta"
    Me.xx_citta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_citta.Name = "xx_citta"
    Me.xx_citta.NTSRepositoryComboBox = Nothing
    Me.xx_citta.NTSRepositoryItemCheck = Nothing
    Me.xx_citta.NTSRepositoryItemMemo = Nothing
    Me.xx_citta.NTSRepositoryItemText = Nothing
    Me.xx_citta.OptionsColumn.AllowEdit = False
    Me.xx_citta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_citta.OptionsColumn.ReadOnly = True
    Me.xx_citta.OptionsFilter.AllowFilter = False
    Me.xx_citta.Visible = True
    Me.xx_citta.VisibleIndex = 7
    Me.xx_citta.Width = 70
    '
    'xx_prov
    '
    Me.xx_prov.AppearanceCell.Options.UseBackColor = True
    Me.xx_prov.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prov.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prov.Caption = "Prov"
    Me.xx_prov.Enabled = False
    Me.xx_prov.FieldName = "xx_prov"
    Me.xx_prov.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prov.Name = "xx_prov"
    Me.xx_prov.NTSRepositoryComboBox = Nothing
    Me.xx_prov.NTSRepositoryItemCheck = Nothing
    Me.xx_prov.NTSRepositoryItemMemo = Nothing
    Me.xx_prov.NTSRepositoryItemText = Nothing
    Me.xx_prov.OptionsColumn.AllowEdit = False
    Me.xx_prov.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prov.OptionsColumn.ReadOnly = True
    Me.xx_prov.OptionsFilter.AllowFilter = False
    Me.xx_prov.Visible = True
    Me.xx_prov.VisibleIndex = 8
    Me.xx_prov.Width = 70
    '
    'xx_telef
    '
    Me.xx_telef.AppearanceCell.Options.UseBackColor = True
    Me.xx_telef.AppearanceCell.Options.UseTextOptions = True
    Me.xx_telef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_telef.Caption = "Telef"
    Me.xx_telef.Enabled = False
    Me.xx_telef.FieldName = "xx_telef"
    Me.xx_telef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_telef.Name = "xx_telef"
    Me.xx_telef.NTSRepositoryComboBox = Nothing
    Me.xx_telef.NTSRepositoryItemCheck = Nothing
    Me.xx_telef.NTSRepositoryItemMemo = Nothing
    Me.xx_telef.NTSRepositoryItemText = Nothing
    Me.xx_telef.OptionsColumn.AllowEdit = False
    Me.xx_telef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_telef.OptionsColumn.ReadOnly = True
    Me.xx_telef.OptionsFilter.AllowFilter = False
    Me.xx_telef.Visible = True
    Me.xx_telef.VisibleIndex = 9
    Me.xx_telef.Width = 70
    '
    'xx_cell
    '
    Me.xx_cell.AppearanceCell.Options.UseBackColor = True
    Me.xx_cell.AppearanceCell.Options.UseTextOptions = True
    Me.xx_cell.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_cell.Caption = "Tel. cell."
    Me.xx_cell.Enabled = False
    Me.xx_cell.FieldName = "xx_cell"
    Me.xx_cell.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_cell.Name = "xx_cell"
    Me.xx_cell.NTSRepositoryComboBox = Nothing
    Me.xx_cell.NTSRepositoryItemCheck = Nothing
    Me.xx_cell.NTSRepositoryItemMemo = Nothing
    Me.xx_cell.NTSRepositoryItemText = Nothing
    Me.xx_cell.OptionsColumn.AllowEdit = False
    Me.xx_cell.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_cell.OptionsColumn.ReadOnly = True
    Me.xx_cell.OptionsFilter.AllowFilter = False
    Me.xx_cell.Visible = True
    Me.xx_cell.VisibleIndex = 10
    Me.xx_cell.Width = 70
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cmdAnnulla)
    Me.pnRight.Controls.Add(Me.cmdVisualizza)
    Me.pnRight.Controls.Add(Me.cmdDeselAll)
    Me.pnRight.Controls.Add(Me.cmdSelAll)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(528, 0)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.Size = New System.Drawing.Size(120, 442)
    Me.pnRight.TabIndex = 6
    Me.pnRight.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 44)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(103, 26)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdVisualizza
    '
    Me.cmdVisualizza.ImageText = ""
    Me.cmdVisualizza.Location = New System.Drawing.Point(6, 12)
    Me.cmdVisualizza.Name = "cmdVisualizza"
    Me.cmdVisualizza.Size = New System.Drawing.Size(103, 26)
    Me.cmdVisualizza.TabIndex = 2
    Me.cmdVisualizza.Text = "Visualizza"
    '
    'cmdDeselAll
    '
    Me.cmdDeselAll.ImageText = ""
    Me.cmdDeselAll.Location = New System.Drawing.Point(6, 147)
    Me.cmdDeselAll.Name = "cmdDeselAll"
    Me.cmdDeselAll.Size = New System.Drawing.Size(103, 26)
    Me.cmdDeselAll.TabIndex = 1
    Me.cmdDeselAll.Text = "Deseleziona tutti"
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(6, 115)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.Size = New System.Drawing.Size(103, 26)
    Me.cmdSelAll.TabIndex = 0
    Me.cmdSelAll.Text = "Seleziona tutti"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grLoca)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 70)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(528, 372)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(11, 21)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(93, 13)
    Me.NtsLabel1.TabIndex = 4
    Me.NtsLabel1.Text = "Luogo di partenza"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'edStart
    '
    Me.edStart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edStart.Location = New System.Drawing.Point(119, 18)
    Me.edStart.Name = "edStart"
    Me.edStart.NTSDbField = ""
    Me.edStart.NTSForzaVisZoom = False
    Me.edStart.NTSOldValue = ""
    Me.edStart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edStart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edStart.Properties.MaxLength = 65536
    Me.edStart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edStart.Size = New System.Drawing.Size(384, 20)
    Me.edStart.TabIndex = 5
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(11, 42)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(78, 13)
    Me.NtsLabel2.TabIndex = 6
    Me.NtsLabel2.Text = "Luogo di arrivo"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'edEnd
    '
    Me.edEnd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEnd.Location = New System.Drawing.Point(119, 39)
    Me.edEnd.Name = "edEnd"
    Me.edEnd.NTSDbField = ""
    Me.edEnd.NTSForzaVisZoom = False
    Me.edEnd.NTSOldValue = ""
    Me.edEnd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEnd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEnd.Properties.MaxLength = 65536
    Me.edEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEnd.Size = New System.Drawing.Size(384, 20)
    Me.edEnd.TabIndex = 7
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbNota)
    Me.pnTop.Controls.Add(Me.edEnd)
    Me.pnTop.Controls.Add(Me.NtsLabel1)
    Me.pnTop.Controls.Add(Me.NtsLabel2)
    Me.pnTop.Controls.Add(Me.edStart)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(528, 70)
    Me.pnTop.TabIndex = 8
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNota.Location = New System.Drawing.Point(116, 2)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(331, 11)
    Me.lbNota.TabIndex = 8
    Me.lbNota.Text = "Indicare il luogo di partenza e di arrivo nel formato VIA, COMUNE (, STATO)"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'FRM__LOCA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnRight)
    Me.Name = "FRM__LOCA"
    Me.NTSLastControlFocussed = Me.grLoca
    Me.Text = "LOCALIZZA CON GOOGLE"
    CType(Me.grLoca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLoca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.edStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__LOCA", "BE__LOCA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 129768980375683694, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleLoca = CType(oTmp, CLE__LOCA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__LOCA", strRemoteServer, strRemotePort)
    AddHandler oCleLoca.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleLoca.Init(oApp, oScript, oMenu.oCleComm, "ANAGRA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvLoca.NTSSetParam(oMenu, "LOCALIZZA CON GOOGLE")
      grvLoca.NTSAllowInsert = False
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129768990096796779, "Seleziona"), "S", "N")
      xx_order.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129768989945692504, "Ordinamento"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129768980374279691, "Codice"), "0", 9, 0, 999999999)
      xx_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129768980374435691, "Cod. destinaz"), "0", 9, 0, 999999999)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980374591692, "Descrizione"), 50, True)
      xx_indir.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980374747692, "Indirizzo"), 70, True)
      xx_cap.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980374903692, "Cap"), 9, True)
      xx_citta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980375059693, "Città"), 50, True)
      xx_prov.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980375215693, "Prov"), 2, True)
      xx_telef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980375371693, "Telef"), 18, True)
      xx_cell.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129768980375527693, "Tel. cell."), 18, True)
      edStart.NTSSetParam(oMenu, oApp.Tr(Me, 129769042761437999, "Luogo di partenza"), 0, True)
      edEnd.NTSSetParam(oMenu, oApp.Tr(Me, 129769043021254459, "Luogo di arrivo"), 0, True)

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

  Public Overridable Sub FRM__LOCA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        dsLoca = New DataSet
        dsLoca.Tables.Add(CType(oCallParams.ctlPar1, DataTable))
      Else
        Me.Close()
        Return
      End If  'If Not oCallParams Is Nothing Then

      If Not oCleLoca.GetAnagrafiche(dsLoca) Then
        Me.Close()
        Return
      End If

      strFile = oApp.AscDir & "\loc" & oApp.User.Nome.Replace("_", "") & ".html"  'non so perchè, ma se co sino degli '_' il file .html è LENTISSIMO ad elaborare i waypoint ed a calcolare il percorso

      dcLoca.DataSource = dsLoca.Tables("ANAGRAFICHE")
      dsLoca.AcceptChanges()

      grLoca.DataSource = dcLoca

      edStart.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BN__LOCA", "Recent", ".", "EdStart", "", " ", "")
      edEnd.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BN__LOCA", "Recent", ".", "edEnd", "", " ", "")

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRM__LOCA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLoca.Dispose()
      dsLoca.Dispose()
      If strFile <> "" Then System.IO.File.Delete(strFile)
    Catch
    End Try
  End Sub

  Public Overridable Sub grvLoca_NTSCellLeave(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles grvLoca.NTSCellLeave
    'al cambio dell'ordinamento riordino/correggo l'ordinamento di tutti i nominativi successivi
    Dim nOrderChanged As Integer = NTSCInt(e.Value)
    Try
      If grvLoca.FocusedColumn.Name = "xx_order" Then
        For Each dtrT As DataRow In dsLoca.Tables("ANAGRAFICHE").Select("xx_order >= " & nOrderChanged.ToString & " AND (xx_conto <> " & grvLoca.NTSGetCurrentDataRow!xx_conto.ToString & " OR (xx_conto = " & grvLoca.NTSGetCurrentDataRow!xx_conto.ToString & " AND xx_coddest <> " & grvLoca.NTSGetCurrentDataRow!xx_coddest.ToString & "))", "xx_order")
          nOrderChanged += 1
          dtrT!xx_order = nOrderChanged
        Next
        dsLoca.Tables("ANAGRAFICHE").AcceptChanges()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdVisualizza_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisualizza.Click
    Dim strHtmlPage As String = ""
    Dim strTmp As String = ""
    Try
      If edStart.Text.Trim = "" Or edEnd.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129769042456185451, "Indicare il luogo di partenza e di arrivo"))
        Return
      End If

      oMenu.SaveSettingBus("BN__LOCA", "RECENT", ".", "EdStart", edStart.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BN__LOCA", "RECENT", ".", "edEnd", edEnd.Text, " ", "NS.", "...", "...")

      For Each dtrT As DataRow In dsLoca.Tables("ANAGRAFICHE").Select("xx_sel = 'S' AND xx_citta <> ''", "xx_order")
        If NTSCStr(dtrT!xx_latitud).Trim <> "" And NTSCStr(dtrT!xx_longitud).Trim <> "" Then
          strTmp += "<option selected value=""*" & NTSCStr(dtrT!xx_latitud) & ", " & NTSCStr(dtrT!xx_longitud) & """>" & NTSCStr(dtrT!xx_descr1) & " - " & NTSCStr(dtrT!xx_citta) & "(" & NTSCStr(dtrT!xx_latitud) & ", " & NTSCStr(dtrT!xx_longitud) & ")</input>" & vbCrLf
        Else
          strTmp += "<option selected value=""" & NTSCStr(dtrT!xx_indir) & ", " & NTSCStr(dtrT!xx_citta) & """>" & NTSCStr(dtrT!xx_descr1) & " - " & NTSCStr(dtrT!xx_citta) & "(" & NTSCStr(dtrT!xx_indir) & ")</input>" & vbCrLf
        End If
      Next

      'prelevo il modello della pagina HTML dalle risorse
      strHtmlPage = My.Resources.strHtmlPage
      strHtmlPage = strHtmlPage.Replace("zoom: 9,", "zoom: 7,")
      strHtmlPage = strHtmlPage.Replace("***START***", edStart.Text.Trim)
      strHtmlPage = strHtmlPage.Replace("***END***", edEnd.Text.Trim)
      strHtmlPage = strHtmlPage.Replace("***<option selected value=""""></input>***", strTmp)
      System.IO.File.Delete(strFile)
      Dim w1 As New System.IO.StreamWriter(strFile, False, System.Text.Encoding.Default)
      w1.Write(strHtmlPage)
      w1.Close()

      'apro il browser per visualizzare
      If CLN__STD.IsBis Then
        'invio la pagina al client che la renderizzerà
        IS_ShowFileOnSbc(strFile)
      Else
        NTSProcessStart(strFile, "")
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Dim i As Integer = 0
    Try
      For i = 0 To grvLoca.RowCount - 1
        grvLoca.SetRowCellValue(i, "xx_sel", "S")
      Next
      dsLoca.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeselAll.Click
    Dim i As Integer = 0
    Try
      For i = 0 To grvLoca.RowCount - 1
        grvLoca.SetRowCellValue(i, "xx_sel", "N")
      Next
      dsLoca.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
