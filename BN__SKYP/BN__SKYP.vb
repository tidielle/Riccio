Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SKYP
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

  Public oCleSkyp As CLE__SKYP
  Public dsSkyp As DataSet
  Public oCallParams As CLE__CLDP
  Public dcSkyp As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.fmOrganig = New NTSInformatica.NTSGroupBox
    Me.pnOrganig = New NTSInformatica.NTSPanel
    Me.grOrga = New NTSInformatica.NTSGrid
    Me.grvOrga = New NTSInformatica.NTSGridView
    Me.og_descont = New NTSInformatica.NTSGridColumn
    Me.og_descont2 = New NTSInformatica.NTSGridColumn
    Me.og_codruaz = New NTSInformatica.NTSGridColumn
    Me.xx_codruaz = New NTSInformatica.NTSGridColumn
    Me.og_telef = New NTSInformatica.NTSGridColumn
    Me.og_cell = New NTSInformatica.NTSGridColumn
    Me.pnOrgaBottom = New NTSInformatica.NTSPanel
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.cmdChiamaCellOrg = New NTSInformatica.NTSButton
    Me.cmdChiamaTelOrg = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnConto = New NTSInformatica.NTSPanel
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesConto = New NTSInformatica.NTSLabel
    Me.lbCliente = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.fmOrganig, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrganig.SuspendLayout()
    CType(Me.pnOrganig, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnOrganig.SuspendLayout()
    CType(Me.grOrga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvOrga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnOrgaBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnOrgaBottom.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnConto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnConto.SuspendLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.MaxItemId = 26
    '
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.fmOrganig)
    Me.pnAll.Controls.Add(Me.pnTop)
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 0)
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(626, 364)
    Me.pnAll.TabIndex = 4
    Me.pnAll.Text = "NtsPanel1"
    '
    'fmOrganig
    '
    Me.fmOrganig.AllowDrop = True
    Me.fmOrganig.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrganig.Appearance.Options.UseBackColor = True
    Me.fmOrganig.Controls.Add(Me.pnOrganig)
    Me.fmOrganig.Controls.Add(Me.pnOrgaBottom)
    Me.fmOrganig.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmOrganig.Location = New System.Drawing.Point(0, 31)
    Me.fmOrganig.Name = "fmOrganig"
    Me.fmOrganig.Size = New System.Drawing.Size(626, 333)
    Me.fmOrganig.TabIndex = 0
    Me.fmOrganig.Text = "Contatti"
    '
    'pnOrganig
    '
    Me.pnOrganig.AllowDrop = True
    Me.pnOrganig.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnOrganig.Appearance.Options.UseBackColor = True
    Me.pnOrganig.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnOrganig.Controls.Add(Me.grOrga)
    Me.pnOrganig.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnOrganig.Location = New System.Drawing.Point(2, 20)
    Me.pnOrganig.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnOrganig.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnOrganig.Name = "pnOrganig"
    Me.pnOrganig.NTSActiveTrasparency = True
    Me.pnOrganig.Size = New System.Drawing.Size(622, 281)
    Me.pnOrganig.TabIndex = 0
    Me.pnOrganig.Text = "NtsPanel1"
    '
    'grOrga
    '
    Me.grOrga.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grOrga.EmbeddedNavigator.Name = ""
    Me.grOrga.Location = New System.Drawing.Point(0, 0)
    Me.grOrga.MainView = Me.grvOrga
    Me.grOrga.Name = "grOrga"
    Me.grOrga.Size = New System.Drawing.Size(622, 281)
    Me.grOrga.TabIndex = 0
    Me.grOrga.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvOrga})
    '
    'grvOrga
    '
    Me.grvOrga.ActiveFilterEnabled = False
    Me.grvOrga.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.og_descont, Me.og_descont2, Me.og_codruaz, Me.xx_codruaz, Me.og_telef, Me.og_cell})
    Me.grvOrga.Enabled = True
    Me.grvOrga.GridControl = Me.grOrga
    Me.grvOrga.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvOrga.MinRowHeight = 14
    Me.grvOrga.Name = "grvOrga"
    Me.grvOrga.NTSAllowDelete = True
    Me.grvOrga.NTSAllowInsert = True
    Me.grvOrga.NTSAllowUpdate = True
    Me.grvOrga.NTSMenuContext = Nothing
    Me.grvOrga.OptionsCustomization.AllowRowSizing = True
    Me.grvOrga.OptionsFilter.AllowFilterEditor = False
    Me.grvOrga.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvOrga.OptionsNavigation.UseTabKey = False
    Me.grvOrga.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvOrga.OptionsView.ColumnAutoWidth = False
    Me.grvOrga.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvOrga.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvOrga.OptionsView.ShowGroupPanel = False
    Me.grvOrga.RowHeight = 14
    '
    'og_descont
    '
    Me.og_descont.AppearanceCell.Options.UseBackColor = True
    Me.og_descont.AppearanceCell.Options.UseTextOptions = True
    Me.og_descont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_descont.Caption = "Cognome"
    Me.og_descont.Enabled = True
    Me.og_descont.FieldName = "og_descont"
    Me.og_descont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_descont.Name = "og_descont"
    Me.og_descont.NTSRepositoryComboBox = Nothing
    Me.og_descont.NTSRepositoryItemCheck = Nothing
    Me.og_descont.NTSRepositoryItemMemo = Nothing
    Me.og_descont.NTSRepositoryItemText = Nothing
    Me.og_descont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_descont.OptionsFilter.AllowFilter = False
    Me.og_descont.Visible = True
    Me.og_descont.VisibleIndex = 0
    '
    'og_descont2
    '
    Me.og_descont2.AppearanceCell.Options.UseBackColor = True
    Me.og_descont2.AppearanceCell.Options.UseTextOptions = True
    Me.og_descont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_descont2.Caption = "Nome"
    Me.og_descont2.Enabled = True
    Me.og_descont2.FieldName = "og_descont2"
    Me.og_descont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_descont2.Name = "og_descont2"
    Me.og_descont2.NTSRepositoryComboBox = Nothing
    Me.og_descont2.NTSRepositoryItemCheck = Nothing
    Me.og_descont2.NTSRepositoryItemMemo = Nothing
    Me.og_descont2.NTSRepositoryItemText = Nothing
    Me.og_descont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_descont2.OptionsFilter.AllowFilter = False
    Me.og_descont2.Visible = True
    Me.og_descont2.VisibleIndex = 1
    '
    'og_codruaz
    '
    Me.og_codruaz.AppearanceCell.Options.UseBackColor = True
    Me.og_codruaz.AppearanceCell.Options.UseTextOptions = True
    Me.og_codruaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codruaz.Caption = "Ruolo"
    Me.og_codruaz.Enabled = True
    Me.og_codruaz.FieldName = "og_codruaz"
    Me.og_codruaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codruaz.Name = "og_codruaz"
    Me.og_codruaz.NTSRepositoryComboBox = Nothing
    Me.og_codruaz.NTSRepositoryItemCheck = Nothing
    Me.og_codruaz.NTSRepositoryItemMemo = Nothing
    Me.og_codruaz.NTSRepositoryItemText = Nothing
    Me.og_codruaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codruaz.OptionsFilter.AllowFilter = False
    Me.og_codruaz.Visible = True
    Me.og_codruaz.VisibleIndex = 2
    '
    'xx_codruaz
    '
    Me.xx_codruaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_codruaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codruaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codruaz.Caption = "Descr. Ruolo"
    Me.xx_codruaz.Enabled = True
    Me.xx_codruaz.FieldName = "xx_codruaz"
    Me.xx_codruaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codruaz.Name = "xx_codruaz"
    Me.xx_codruaz.NTSRepositoryComboBox = Nothing
    Me.xx_codruaz.NTSRepositoryItemCheck = Nothing
    Me.xx_codruaz.NTSRepositoryItemMemo = Nothing
    Me.xx_codruaz.NTSRepositoryItemText = Nothing
    Me.xx_codruaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codruaz.OptionsFilter.AllowFilter = False
    Me.xx_codruaz.Visible = True
    Me.xx_codruaz.VisibleIndex = 3
    '
    'og_telef
    '
    Me.og_telef.AppearanceCell.Options.UseBackColor = True
    Me.og_telef.AppearanceCell.Options.UseTextOptions = True
    Me.og_telef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_telef.Caption = "Telefono"
    Me.og_telef.Enabled = True
    Me.og_telef.FieldName = "og_telef"
    Me.og_telef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_telef.Name = "og_telef"
    Me.og_telef.NTSRepositoryComboBox = Nothing
    Me.og_telef.NTSRepositoryItemCheck = Nothing
    Me.og_telef.NTSRepositoryItemMemo = Nothing
    Me.og_telef.NTSRepositoryItemText = Nothing
    Me.og_telef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_telef.OptionsFilter.AllowFilter = False
    Me.og_telef.Visible = True
    Me.og_telef.VisibleIndex = 4
    '
    'og_cell
    '
    Me.og_cell.AppearanceCell.Options.UseBackColor = True
    Me.og_cell.AppearanceCell.Options.UseTextOptions = True
    Me.og_cell.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_cell.Caption = "Cellulare"
    Me.og_cell.Enabled = True
    Me.og_cell.FieldName = "og_cell"
    Me.og_cell.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_cell.Name = "og_cell"
    Me.og_cell.NTSRepositoryComboBox = Nothing
    Me.og_cell.NTSRepositoryItemCheck = Nothing
    Me.og_cell.NTSRepositoryItemMemo = Nothing
    Me.og_cell.NTSRepositoryItemText = Nothing
    Me.og_cell.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_cell.OptionsFilter.AllowFilter = False
    Me.og_cell.Visible = True
    Me.og_cell.VisibleIndex = 5
    '
    'pnOrgaBottom
    '
    Me.pnOrgaBottom.AllowDrop = True
    Me.pnOrgaBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnOrgaBottom.Appearance.Options.UseBackColor = True
    Me.pnOrgaBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnOrgaBottom.Controls.Add(Me.cmdEsci)
    Me.pnOrgaBottom.Controls.Add(Me.cmdChiamaCellOrg)
    Me.pnOrgaBottom.Controls.Add(Me.cmdChiamaTelOrg)
    Me.pnOrgaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnOrgaBottom.Location = New System.Drawing.Point(2, 301)
    Me.pnOrgaBottom.Name = "pnOrgaBottom"
    Me.pnOrgaBottom.NTSActiveTrasparency = True
    Me.pnOrgaBottom.Size = New System.Drawing.Size(622, 30)
    Me.pnOrgaBottom.TabIndex = 0
    Me.pnOrgaBottom.Text = "NtsPanel1"
    '
    'cmdEsci
    '
    Me.cmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsci.ImagePath = ""
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(544, 2)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(75, 26)
    Me.cmdEsci.TabIndex = 0
    Me.cmdEsci.Text = "Esci"
    '
    'cmdChiamaCellOrg
    '
    Me.cmdChiamaCellOrg.ImagePath = ""
    Me.cmdChiamaCellOrg.ImageText = ""
    Me.cmdChiamaCellOrg.Location = New System.Drawing.Point(210, 2)
    Me.cmdChiamaCellOrg.Name = "cmdChiamaCellOrg"
    Me.cmdChiamaCellOrg.NTSContextMenu = Nothing
    Me.cmdChiamaCellOrg.Size = New System.Drawing.Size(200, 26)
    Me.cmdChiamaCellOrg.TabIndex = 4
    Me.cmdChiamaCellOrg.Text = "Chiama Cellulare"
    '
    'cmdChiamaTelOrg
    '
    Me.cmdChiamaTelOrg.ImagePath = ""
    Me.cmdChiamaTelOrg.ImageText = ""
    Me.cmdChiamaTelOrg.Location = New System.Drawing.Point(3, 2)
    Me.cmdChiamaTelOrg.Name = "cmdChiamaTelOrg"
    Me.cmdChiamaTelOrg.NTSContextMenu = Nothing
    Me.cmdChiamaTelOrg.Size = New System.Drawing.Size(200, 26)
    Me.cmdChiamaTelOrg.TabIndex = 4
    Me.cmdChiamaTelOrg.Text = "Chiama Telefono"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.pnConto)
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(626, 31)
    Me.pnTop.TabIndex = 1
    Me.pnTop.Text = "NtsPanel1"
    '
    'pnConto
    '
    Me.pnConto.AllowDrop = True
    Me.pnConto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnConto.Appearance.Options.UseBackColor = True
    Me.pnConto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnConto.Controls.Add(Me.cbTipo)
    Me.pnConto.Controls.Add(Me.edConto)
    Me.pnConto.Controls.Add(Me.lbDesConto)
    Me.pnConto.Controls.Add(Me.lbCliente)
    Me.pnConto.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnConto.Location = New System.Drawing.Point(0, 0)
    Me.pnConto.Name = "pnConto"
    Me.pnConto.NTSActiveTrasparency = True
    Me.pnConto.Size = New System.Drawing.Size(626, 30)
    Me.pnConto.TabIndex = 5
    Me.pnConto.Text = "NtsPanel3"
    '
    'cbTipo
    '
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(7, 5)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(69, 20)
    Me.cbTipo.TabIndex = 3
    Me.cbTipo.ValueMember = ""
    Me.cbTipo.Visible = False
    '
    'edConto
    '
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(79, 5)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(80, 20)
    Me.edConto.TabIndex = 1
    '
    'lbDesConto
    '
    Me.lbDesConto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesConto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesConto.Location = New System.Drawing.Point(162, 5)
    Me.lbDesConto.Name = "lbDesConto"
    Me.lbDesConto.NTSDbField = ""
    Me.lbDesConto.Size = New System.Drawing.Size(462, 20)
    Me.lbDesConto.TabIndex = 0
    Me.lbDesConto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesConto.Tooltip = ""
    Me.lbDesConto.UseMnemonic = False
    '
    'lbCliente
    '
    Me.lbCliente.AutoSize = True
    Me.lbCliente.BackColor = System.Drawing.Color.Transparent
    Me.lbCliente.Location = New System.Drawing.Point(5, 9)
    Me.lbCliente.Name = "lbCliente"
    Me.lbCliente.NTSDbField = ""
    Me.lbCliente.Size = New System.Drawing.Size(36, 13)
    Me.lbCliente.TabIndex = 2
    Me.lbCliente.Text = "Conto"
    Me.lbCliente.Tooltip = ""
    Me.lbCliente.UseMnemonic = False
    '
    'FRM__SKYP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(626, 364)
    Me.Controls.Add(Me.pnAll)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__SKYP"
    Me.Text = "SKYPE TO CALL"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.fmOrganig, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrganig.ResumeLayout(False)
    CType(Me.pnOrganig, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnOrganig.ResumeLayout(False)
    CType(Me.grOrga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvOrga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnOrgaBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnOrgaBottom.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnConto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnConto.ResumeLayout(False)
    Me.pnConto.PerformLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__SKYP", "BE__SKYP", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130397817176137230, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleSkyp = CType(oTmp, CLE__SKYP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__SKYP", strRemoteServer, strRemotePort)
    AddHandler oCleSkyp.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSkyp.Init(oApp, oScript, oMenu.oCleComm, "ORGANIG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei controlli
      grvOrga.NTSSetParam(oMenu, oApp.Tr(Me, 130397861516468885, "Organizzazione"))
      og_descont2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130397861516478897, "Nome"), 0, True)
      og_descont.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130397861516488889, "Cognome"), 0, True)
      og_codruaz.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 130397861516498894, "Ruolo"), tabruaz, True)
      xx_codruaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130397861516508902, "Descr. Ruolo"), 0, True)
      og_telef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130397861516518907, "Telefono"), 0, True)
      og_cell.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130397861516588963, "Cellulare"), 0, True)
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130397861516779056, "Cliente"), tabanagrac)

      grvOrga.Enabled = False
      grvOrga.NTSAllowDelete = False
      grvOrga.NTSAllowInsert = False
      grvOrga.NTSAllowUpdate = False

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
    Dim dttTipi As New DataTable
    Try
      dttTipi.Columns.Add("cod", GetType(String))
      dttTipi.Columns.Add("val", GetType(String))
      dttTipi.Rows.Add(New Object() {"C", "Conto"})
      dttTipi.Rows.Add(New Object() {"L", "Lead"})
      dttTipi.AcceptChanges()
      cbTipo.DataSource = dttTipi
      cbTipo.DisplayMember = "val"
      cbTipo.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


#Region "Eventi Form"
  Public Overridable Sub FRM__SKYP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      CaricaCombo()

      oCleSkyp.bModCRM = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM)
      oCleSkyp.bModCS = CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModAS)
      If oCleSkyp.bModCRM Then oCleSkyp.bIsUserCrm = oMenu.IsCrmUser(DittaCorrente)
      If oCleSkyp.bModCS OrElse oCleSkyp.bIsUserCrm Then
        cbTipo.Visible = True
      Else
        cbTipo.SelectedValue = "C"
      End If

      If Not oCallParams Is Nothing Then
        cbTipo.SelectedValue = NTSCStr(oCallParams.strPar1)
        edConto.Text = NTSCStr(oCallParams.dPar1)
      End If

      CaricaDati(NTSCInt(edConto.Text))

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__SKYP_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      For i As Integer = 0 To dcSkyp.List.Count - 1
        If NTSCInt(CType(dcSkyp.Item(i), DataRowView)!og_progr) = oCallParams.dPar2 Then
          dcSkyp.Position = i
          Exit For
        End If
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRM__SKYP_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSkyp.Dispose()
      dsSkyp.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub edConto_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.ValidatedAndChanged
    Try
      CaricaDati(NTSCInt(edConto.Text))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdChiamaTelOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChiamaTelOrg.Click
    Try
      AvviaChiamata(NTSCStr(cmdChiamaTelOrg.Tag))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdChiamaCellOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChiamaCellOrg.Click
    Try
      AvviaChiamata(NTSCStr(cmdChiamaCellOrg.Tag))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbTipo_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedValueChanged
    Try
      Select Case cbTipo.SelectedValue
        Case "C" : edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130426365335025260, "Conto"), tabanagrac)
        Case "L" : edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130397946058380713, "Lead"), tableads)
      End Select
      edConto.Text = "0"
      CaricaDati(NTSCInt(edConto.Text))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvOrga_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvOrga.FocusedRowChanged
    Try
      If grvOrga.NTSGetCurrentDataRow Is Nothing Then
        ConfiguraPulsanteNumeroChiamare(cmdChiamaTelOrg, "T", "")
        ConfiguraPulsanteNumeroChiamare(cmdChiamaCellOrg, "C", "")
      Else
        ConfiguraPulsanteNumeroChiamare(cmdChiamaTelOrg, "T", NTSCStr(grvOrga.NTSGetCurrentDataRow!og_telef))
        ConfiguraPulsanteNumeroChiamare(cmdChiamaCellOrg, "C", NTSCStr(grvOrga.NTSGetCurrentDataRow!og_cell))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaDati(ByVal lConto As Integer)
    Try
      If lConto = 0 Then
        If Not oCleSkyp.CaricaDatiInterni(dsSkyp) Then Return
      Else
        Select Case cbTipo.SelectedValue
          Case "C" : If Not oCleSkyp.CaricaDatiAnagra(lConto, dsSkyp) Then Return
          Case "L" : If Not oCleSkyp.CaricaDatiLead(lConto, dsSkyp) Then Return
          Case Else : Return
        End Select
      End If

      If dsSkyp.Tables("AZIENDA").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130397943574789848, "Codice anagrafica non valido"))
        edConto.Text = "0"
        Return
      End If

      If lConto > 0 AndAlso oCleSkyp.bIsUserCrm Then
        Dim lLead As Integer
        If cbTipo.SelectedValue = "C" Then
          lLead = NTSCInt(dsSkyp.Tables("AZIENDA").Rows(0)!xx_lead)
        Else
          lLead = lConto
        End If

        If Not oMenu.CercaAccessiCrmDaLead(DittaCorrente, lLead) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128366770952828001, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi a questa anagrafica."))
          edConto.Text = "0"
          Return
        End If
      End If

      lbDesConto.Text = ComponiDescrizioneConto(dsSkyp.Tables("AZIENDA").Rows(0))

      dsSkyp.Tables("ORGANIG").Rows.InsertAt(dsSkyp.Tables("ORGANIG").NewRow, 0)
      With dsSkyp.Tables("ORGANIG").Rows(0)
        !xx_codruaz = oApp.Tr(Me, 130397958459178440, "Contatto Aziendale")
        !og_telef = dsSkyp.Tables("AZIENDA").Rows(0)!xx_telef
        !og_cell = dsSkyp.Tables("AZIENDA").Rows(0)!xx_cell
      End With

      dsSkyp.AcceptChanges()
      dcSkyp = New BindingSource
      dcSkyp.DataSource = dsSkyp.Tables("ORGANIG")
      grOrga.DataSource = dcSkyp

      grvOrga_FocusedRowChanged(Nothing, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ComponiDescrizioneConto(ByVal dtrRow As DataRow) As String
    Dim strDescr As String = ""
    Try
      strDescr = NTSCStr(dtrRow!xx_descr)
      If NTSCStr(dtrRow!xx_indir).Trim <> "" Then strDescr &= " - " & NTSCStr(dtrRow!xx_indir)
      If NTSCStr(dtrRow!xx_citta).Trim <> "" Then strDescr &= " - " & NTSCStr(dtrRow!xx_citta)

      Return strDescr
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
    Return strDescr
  End Function

  Public Overridable Sub ConfiguraPulsanteNumeroChiamare(ByVal cmdPulsante As NTSButton, ByVal strTipo As String, ByVal strTelef As String)
    Try
      If strTipo = "C" Then
        cmdPulsante.Text = oApp.Tr(Me, 130397852210796767, "Chiama Cellulare:")
      Else
        cmdPulsante.Text = oApp.Tr(Me, 130397852354005876, "Chiama Telefono:")
      End If

      cmdPulsante.Tag = strTelef

      If strTelef.Trim = "" Then
        cmdPulsante.Text &= " N/A"
        cmdPulsante.Enabled = False
      Else
        cmdPulsante.Text &= " " & strTelef
        cmdPulsante.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AvviaChiamata(ByVal strNumero As String)
    Try
      Try
        If CLN__STD.IsBis Then
          IS_ExecOnSbc("", "skype:" & strNumero)
        Else
          NTSProcessStart("skype:" & strNumero, "")
        End If
      Catch ex As Exception
        oApp.MsgBoxErr(oApp.Tr(Me, 130669097249095436, "Errore in avvio applicazione skype."))
        Return
      End Try

      AvviaAttivitTelefonica(NTSCInt(dsSkyp.Tables("AZIENDA").Rows(0)!xx_lead))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AvviaAttivitTelefonica(ByVal lLead As Integer)
    Dim oPar As New CLE__CLDP
    Try
      If lLead = 0 OrElse Not oCleSkyp.bIsUserCrm Then Return

      oPar.bPar1 = False      'al ritorno da cratte è true se ho creato l'attività
      oPar.bPar2 = False      'al ritorno da cratte è true se ho creato l'opportunità

      oPar.strParam = lLead.ToString.PadLeft(9, "0"c) & ";"
      oPar.dPar1 = 0
      oPar.dPar2 = NTSCInt(grvOrga.NTSGetCurrentDataRow!og_progr)
      oPar.bPar3 = False ' No Modal

      oMenu.RunChild("NTSInformatica", "FRMCRATTE", oApp.Tr(Me, 128922305693088080, "Attività telefonica"), DittaCorrente, "", "BNCRATTE", oPar, "", True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class


