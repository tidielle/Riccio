Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLCV
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsZoom As New DataSet
  Public dcZoom As BindingSource = New BindingSource()

  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents mm_cvtipo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_cvanno As NTSInformatica.NTSGridColumn
  Public WithEvents mm_cvserie As NTSInformatica.NTSGridColumn
  Public WithEvents mm_cvnum As NTSInformatica.NTSGridColumn
  Public WithEvents mm_cvriga As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents mm_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mm_qtares As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quadaeva As NTSInformatica.NTSGridColumn
  Public WithEvents mm_ump As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mm_fase As NTSInformatica.NTSGridColumn
  Public WithEvents mm_desfase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_selez As NTSInformatica.NTSGridColumn

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.xx_selez = New NTSInformatica.NTSGridColumn
    Me.mm_codart = New NTSInformatica.NTSGridColumn
    Me.mm_descr = New NTSInformatica.NTSGridColumn
    Me.mm_ump = New NTSInformatica.NTSGridColumn
    Me.mm_qtares = New NTSInformatica.NTSGridColumn
    Me.mm_quadaeva = New NTSInformatica.NTSGridColumn
    Me.mm_cvtipo = New NTSInformatica.NTSGridColumn
    Me.mm_cvanno = New NTSInformatica.NTSGridColumn
    Me.mm_cvserie = New NTSInformatica.NTSGridColumn
    Me.mm_cvnum = New NTSInformatica.NTSGridColumn
    Me.mm_cvriga = New NTSInformatica.NTSGridColumn
    Me.mm_fase = New NTSInformatica.NTSGridColumn
    Me.mm_desfase = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_subcommeca = New NTSInformatica.NTSGridColumn
    Me.mm_lotto = New NTSInformatica.NTSGridColumn
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdAnnulla)
    Me.pnTop.Controls.Add(Me.cmdConferma)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnTop.Location = New System.Drawing.Point(697, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(102, 442)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(7, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(83, 24)
    Me.cmdAnnulla.TabIndex = 11
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(7, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(83, 24)
    Me.cmdConferma.TabIndex = 10
    Me.cmdConferma.Text = "&Conferma"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grZoom)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(697, 442)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 0)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(697, 442)
    Me.grZoom.TabIndex = 6
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_selez, Me.mm_quadaeva, Me.mm_qtares, Me.mm_codart, Me.mm_descr, Me.mm_ump, Me.mm_cvtipo, Me.mm_cvanno, Me.mm_cvserie, Me.mm_cvnum, Me.mm_cvriga, Me.mm_fase, Me.mm_desfase, Me.mm_commeca, Me.mm_subcommeca, Me.mm_lotto})
    Me.grvZoom.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvZoom.Enabled = True
    Me.grvZoom.GridControl = Me.grZoom
    Me.grvZoom.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvZoom.Name = "grvZoom"
    Me.grvZoom.NTSAllowDelete = True
    Me.grvZoom.NTSAllowInsert = True
    Me.grvZoom.NTSAllowUpdate = True
    Me.grvZoom.NTSMenuContext = Nothing
    Me.grvZoom.OptionsCustomization.AllowRowSizing = True
    Me.grvZoom.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvZoom.OptionsNavigation.UseTabKey = False
    Me.grvZoom.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvZoom.OptionsView.ColumnAutoWidth = False
    Me.grvZoom.OptionsView.EnableAppearanceEvenRow = True
    Me.grvZoom.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvZoom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvZoom.OptionsView.ShowGroupPanel = False
    Me.grvZoom.RowHeight = 16
    '
    'xx_selez
    '
    Me.xx_selez.AppearanceCell.Options.UseBackColor = True
    Me.xx_selez.AppearanceCell.Options.UseTextOptions = True
    Me.xx_selez.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_selez.Caption = "Selez."
    Me.xx_selez.Enabled = True
    Me.xx_selez.FieldName = "xx_selez"
    Me.xx_selez.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_selez.Name = "xx_selez"
    Me.xx_selez.NTSRepositoryComboBox = Nothing
    Me.xx_selez.NTSRepositoryItemCheck = Nothing
    Me.xx_selez.NTSRepositoryItemMemo = Nothing
    Me.xx_selez.NTSRepositoryItemText = Nothing
    Me.xx_selez.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_selez.OptionsFilter.AllowFilter = False
    Me.xx_selez.Visible = True
    Me.xx_selez.VisibleIndex = 0
    '
    'mm_codart
    '
    Me.mm_codart.AppearanceCell.Options.UseBackColor = True
    Me.mm_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codart.Caption = "Articolo"
    Me.mm_codart.Enabled = False
    Me.mm_codart.FieldName = "mm_codart"
    Me.mm_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codart.Name = "mm_codart"
    Me.mm_codart.NTSRepositoryComboBox = Nothing
    Me.mm_codart.NTSRepositoryItemCheck = Nothing
    Me.mm_codart.NTSRepositoryItemMemo = Nothing
    Me.mm_codart.NTSRepositoryItemText = Nothing
    Me.mm_codart.OptionsColumn.AllowEdit = False
    Me.mm_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codart.OptionsColumn.ReadOnly = True
    Me.mm_codart.OptionsFilter.AllowFilter = False
    Me.mm_codart.Visible = True
    Me.mm_codart.VisibleIndex = 3
    '
    'mm_descr
    '
    Me.mm_descr.AppearanceCell.Options.UseBackColor = True
    Me.mm_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mm_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_descr.Caption = "Descrizione"
    Me.mm_descr.Enabled = False
    Me.mm_descr.FieldName = "mm_descr"
    Me.mm_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_descr.Name = "mm_descr"
    Me.mm_descr.NTSRepositoryComboBox = Nothing
    Me.mm_descr.NTSRepositoryItemCheck = Nothing
    Me.mm_descr.NTSRepositoryItemMemo = Nothing
    Me.mm_descr.NTSRepositoryItemText = Nothing
    Me.mm_descr.OptionsColumn.AllowEdit = False
    Me.mm_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_descr.OptionsColumn.ReadOnly = True
    Me.mm_descr.OptionsFilter.AllowFilter = False
    Me.mm_descr.Visible = True
    Me.mm_descr.VisibleIndex = 4
    '
    'mm_ump
    '
    Me.mm_ump.AppearanceCell.Options.UseBackColor = True
    Me.mm_ump.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ump.Caption = "Ump"
    Me.mm_ump.Enabled = False
    Me.mm_ump.FieldName = "mm_ump"
    Me.mm_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ump.Name = "mm_ump"
    Me.mm_ump.NTSRepositoryComboBox = Nothing
    Me.mm_ump.NTSRepositoryItemCheck = Nothing
    Me.mm_ump.NTSRepositoryItemMemo = Nothing
    Me.mm_ump.NTSRepositoryItemText = Nothing
    Me.mm_ump.OptionsColumn.AllowEdit = False
    Me.mm_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ump.OptionsColumn.ReadOnly = True
    Me.mm_ump.OptionsFilter.AllowFilter = False
    Me.mm_ump.Visible = True
    Me.mm_ump.VisibleIndex = 5
    '
    'mm_qtares
    '
    Me.mm_qtares.AppearanceCell.Options.UseBackColor = True
    Me.mm_qtares.AppearanceCell.Options.UseTextOptions = True
    Me.mm_qtares.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_qtares.Caption = "Saldo"
    Me.mm_qtares.Enabled = False
    Me.mm_qtares.FieldName = "mm_qtares"
    Me.mm_qtares.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_qtares.Name = "mm_qtares"
    Me.mm_qtares.NTSRepositoryComboBox = Nothing
    Me.mm_qtares.NTSRepositoryItemCheck = Nothing
    Me.mm_qtares.NTSRepositoryItemMemo = Nothing
    Me.mm_qtares.NTSRepositoryItemText = Nothing
    Me.mm_qtares.OptionsColumn.AllowEdit = False
    Me.mm_qtares.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_qtares.OptionsColumn.ReadOnly = True
    Me.mm_qtares.OptionsFilter.AllowFilter = False
    Me.mm_qtares.Visible = True
    Me.mm_qtares.VisibleIndex = 2
    '
    'mm_quadaeva
    '
    Me.mm_quadaeva.AppearanceCell.Options.UseBackColor = True
    Me.mm_quadaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quadaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quadaeva.Caption = "Qta da evadere"
    Me.mm_quadaeva.Enabled = True
    Me.mm_quadaeva.FieldName = "mm_quadaeva"
    Me.mm_quadaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quadaeva.Name = "mm_quadaeva"
    Me.mm_quadaeva.NTSRepositoryComboBox = Nothing
    Me.mm_quadaeva.NTSRepositoryItemCheck = Nothing
    Me.mm_quadaeva.NTSRepositoryItemMemo = Nothing
    Me.mm_quadaeva.NTSRepositoryItemText = Nothing
    Me.mm_quadaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quadaeva.OptionsFilter.AllowFilter = False
    Me.mm_quadaeva.Visible = True
    Me.mm_quadaeva.VisibleIndex = 1
    '
    'mm_cvtipo
    '
    Me.mm_cvtipo.AppearanceCell.Options.UseBackColor = True
    Me.mm_cvtipo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cvtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cvtipo.Caption = "Tipo"
    Me.mm_cvtipo.Enabled = False
    Me.mm_cvtipo.FieldName = "mm_cvtipo"
    Me.mm_cvtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cvtipo.Name = "mm_cvtipo"
    Me.mm_cvtipo.NTSRepositoryComboBox = Nothing
    Me.mm_cvtipo.NTSRepositoryItemCheck = Nothing
    Me.mm_cvtipo.NTSRepositoryItemMemo = Nothing
    Me.mm_cvtipo.NTSRepositoryItemText = Nothing
    Me.mm_cvtipo.OptionsColumn.AllowEdit = False
    Me.mm_cvtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cvtipo.OptionsColumn.ReadOnly = True
    Me.mm_cvtipo.OptionsFilter.AllowFilter = False
    Me.mm_cvtipo.Visible = True
    Me.mm_cvtipo.VisibleIndex = 6
    '
    'mm_cvanno
    '
    Me.mm_cvanno.AppearanceCell.Options.UseBackColor = True
    Me.mm_cvanno.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cvanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cvanno.Caption = "Anno"
    Me.mm_cvanno.Enabled = False
    Me.mm_cvanno.FieldName = "mm_cvanno"
    Me.mm_cvanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cvanno.Name = "mm_cvanno"
    Me.mm_cvanno.NTSRepositoryComboBox = Nothing
    Me.mm_cvanno.NTSRepositoryItemCheck = Nothing
    Me.mm_cvanno.NTSRepositoryItemMemo = Nothing
    Me.mm_cvanno.NTSRepositoryItemText = Nothing
    Me.mm_cvanno.OptionsColumn.AllowEdit = False
    Me.mm_cvanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cvanno.OptionsColumn.ReadOnly = True
    Me.mm_cvanno.OptionsFilter.AllowFilter = False
    Me.mm_cvanno.Visible = True
    Me.mm_cvanno.VisibleIndex = 7
    '
    'mm_cvserie
    '
    Me.mm_cvserie.AppearanceCell.Options.UseBackColor = True
    Me.mm_cvserie.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cvserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cvserie.Caption = "Serie"
    Me.mm_cvserie.Enabled = False
    Me.mm_cvserie.FieldName = "mm_cvserie"
    Me.mm_cvserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cvserie.Name = "mm_cvserie"
    Me.mm_cvserie.NTSRepositoryComboBox = Nothing
    Me.mm_cvserie.NTSRepositoryItemCheck = Nothing
    Me.mm_cvserie.NTSRepositoryItemMemo = Nothing
    Me.mm_cvserie.NTSRepositoryItemText = Nothing
    Me.mm_cvserie.OptionsColumn.AllowEdit = False
    Me.mm_cvserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cvserie.OptionsColumn.ReadOnly = True
    Me.mm_cvserie.OptionsFilter.AllowFilter = False
    Me.mm_cvserie.Visible = True
    Me.mm_cvserie.VisibleIndex = 8
    '
    'mm_cvnum
    '
    Me.mm_cvnum.AppearanceCell.Options.UseBackColor = True
    Me.mm_cvnum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cvnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cvnum.Caption = "Numero"
    Me.mm_cvnum.Enabled = False
    Me.mm_cvnum.FieldName = "mm_cvnum"
    Me.mm_cvnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cvnum.Name = "mm_cvnum"
    Me.mm_cvnum.NTSRepositoryComboBox = Nothing
    Me.mm_cvnum.NTSRepositoryItemCheck = Nothing
    Me.mm_cvnum.NTSRepositoryItemMemo = Nothing
    Me.mm_cvnum.NTSRepositoryItemText = Nothing
    Me.mm_cvnum.OptionsColumn.AllowEdit = False
    Me.mm_cvnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cvnum.OptionsColumn.ReadOnly = True
    Me.mm_cvnum.OptionsFilter.AllowFilter = False
    Me.mm_cvnum.Visible = True
    Me.mm_cvnum.VisibleIndex = 9
    '
    'mm_cvriga
    '
    Me.mm_cvriga.AppearanceCell.Options.UseBackColor = True
    Me.mm_cvriga.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cvriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cvriga.Caption = "Riga"
    Me.mm_cvriga.Enabled = False
    Me.mm_cvriga.FieldName = "mm_cvriga"
    Me.mm_cvriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cvriga.Name = "mm_cvriga"
    Me.mm_cvriga.NTSRepositoryComboBox = Nothing
    Me.mm_cvriga.NTSRepositoryItemCheck = Nothing
    Me.mm_cvriga.NTSRepositoryItemMemo = Nothing
    Me.mm_cvriga.NTSRepositoryItemText = Nothing
    Me.mm_cvriga.OptionsColumn.AllowEdit = False
    Me.mm_cvriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cvriga.OptionsColumn.ReadOnly = True
    Me.mm_cvriga.OptionsFilter.AllowFilter = False
    Me.mm_cvriga.Visible = True
    Me.mm_cvriga.VisibleIndex = 10
    '
    'mm_fase
    '
    Me.mm_fase.AppearanceCell.Options.UseBackColor = True
    Me.mm_fase.AppearanceCell.Options.UseTextOptions = True
    Me.mm_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_fase.Caption = "Fase"
    Me.mm_fase.Enabled = False
    Me.mm_fase.FieldName = "mm_fase"
    Me.mm_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_fase.Name = "mm_fase"
    Me.mm_fase.NTSRepositoryComboBox = Nothing
    Me.mm_fase.NTSRepositoryItemCheck = Nothing
    Me.mm_fase.NTSRepositoryItemMemo = Nothing
    Me.mm_fase.NTSRepositoryItemText = Nothing
    Me.mm_fase.OptionsColumn.AllowEdit = False
    Me.mm_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_fase.OptionsColumn.ReadOnly = True
    Me.mm_fase.OptionsFilter.AllowFilter = False
    Me.mm_fase.Visible = True
    Me.mm_fase.VisibleIndex = 11
    '
    'mm_desfase
    '
    Me.mm_desfase.AppearanceCell.Options.UseBackColor = True
    Me.mm_desfase.AppearanceCell.Options.UseTextOptions = True
    Me.mm_desfase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_desfase.Caption = "Descr. fase"
    Me.mm_desfase.Enabled = False
    Me.mm_desfase.FieldName = "mm_desfase"
    Me.mm_desfase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_desfase.Name = "mm_desfase"
    Me.mm_desfase.NTSRepositoryComboBox = Nothing
    Me.mm_desfase.NTSRepositoryItemCheck = Nothing
    Me.mm_desfase.NTSRepositoryItemMemo = Nothing
    Me.mm_desfase.NTSRepositoryItemText = Nothing
    Me.mm_desfase.OptionsColumn.AllowEdit = False
    Me.mm_desfase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_desfase.OptionsColumn.ReadOnly = True
    Me.mm_desfase.OptionsFilter.AllowFilter = False
    Me.mm_desfase.Visible = True
    Me.mm_desfase.VisibleIndex = 12
    '
    'mm_commeca
    '
    Me.mm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_commeca.Caption = "Commessa"
    Me.mm_commeca.Enabled = False
    Me.mm_commeca.FieldName = "mm_commeca"
    Me.mm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_commeca.Name = "mm_commeca"
    Me.mm_commeca.NTSRepositoryComboBox = Nothing
    Me.mm_commeca.NTSRepositoryItemCheck = Nothing
    Me.mm_commeca.NTSRepositoryItemMemo = Nothing
    Me.mm_commeca.NTSRepositoryItemText = Nothing
    Me.mm_commeca.OptionsColumn.AllowEdit = False
    Me.mm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_commeca.OptionsColumn.ReadOnly = True
    Me.mm_commeca.OptionsFilter.AllowFilter = False
    Me.mm_commeca.Visible = True
    Me.mm_commeca.VisibleIndex = 13
    '
    'mm_subcommeca
    '
    Me.mm_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_subcommeca.Caption = "Sottocommessa"
    Me.mm_subcommeca.Enabled = False
    Me.mm_subcommeca.FieldName = "mm_subcommeca"
    Me.mm_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_subcommeca.Name = "mm_subcommeca"
    Me.mm_subcommeca.NTSRepositoryComboBox = Nothing
    Me.mm_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.mm_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.mm_subcommeca.NTSRepositoryItemText = Nothing
    Me.mm_subcommeca.OptionsColumn.AllowEdit = False
    Me.mm_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_subcommeca.OptionsColumn.ReadOnly = True
    Me.mm_subcommeca.OptionsFilter.AllowFilter = False
    Me.mm_subcommeca.Visible = True
    Me.mm_subcommeca.VisibleIndex = 14
    '
    'mm_lotto
    '
    Me.mm_lotto.AppearanceCell.Options.UseBackColor = True
    Me.mm_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.mm_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_lotto.Caption = "Lotto"
    Me.mm_lotto.Enabled = False
    Me.mm_lotto.FieldName = "mm_lotto"
    Me.mm_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_lotto.Name = "mm_lotto"
    Me.mm_lotto.NTSRepositoryComboBox = Nothing
    Me.mm_lotto.NTSRepositoryItemCheck = Nothing
    Me.mm_lotto.NTSRepositoryItemMemo = Nothing
    Me.mm_lotto.NTSRepositoryItemText = Nothing
    Me.mm_lotto.OptionsColumn.AllowEdit = False
    Me.mm_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_lotto.OptionsColumn.ReadOnly = True
    Me.mm_lotto.OptionsFilter.AllowFilter = False
    Me.mm_lotto.Visible = True
    Me.mm_lotto.VisibleIndex = 15
    '
    'FRMMGHLCV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGHLCV"
    Me.Text = "RIGHE BOLLE C/TO VISIONE"
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGHLCV", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825630210675, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGHLCV", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      Dim dttTipoRk As New DataTable()
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttTipoRk.Rows.Add(New Object() {"B", "DDT emesso"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipoRk.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipoRk.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"M", "DDT ricevuto"})
      dttTipoRk.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarico a produzione"})
      dttTipoRk.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 130086826237241925, "Bolle c/to visione aperte"))
      xx_selez.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129606291216337890, "Seleziona"), "S", "N")
      mm_cvtipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130086826489898175, "Tipo"), dttTipoRk, "val", "cod")
      mm_cvanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086826674898175, "Anno"), "0", 4, 0, 9999)
      mm_cvserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086826947085675, "Serie"), 1, True)
      mm_cvnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696757640781250, "Numero"), "0", 9, 0, 999999999)
      mm_cvriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696757640937500, "Riga"), "0", 9, 0, 999999999)
      mm_qtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696757641093750, "Saldo"), oApp.FormatQta, 15)
      mm_quadaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696757641562500, "Qta da evadere"), oApp.FormatQta, 15)
      mm_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128696757641250000, "Articolo"), tabartico, False)
      mm_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128696757641406250, "Fase art."), 0, True)
      mm_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128696757641718750, "Ump"), 3, True)
      mm_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129424353968070793, "Commessa"), "0", 9, 0, 999999999)
      mm_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129424354257232287, "Sottocommessa"), 2, True)
      mm_lotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129424354888262377, "Lotto"), 0, True)
      mm_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129424355056374301, "Descrizione articolo"), 0, True)
      mm_desfase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129424355214124499, "Descr. fase"), 0, True)

      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowInsert = False

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

  Public Overridable Sub FRMMGHLCV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego i dati
      dsZoom = CType(oCallParams.ctlPar1, DataSet)
      dcZoom.DataSource = dsZoom.Tables("SALDI")
      grZoom.DataSource = dcZoom

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGHLCV_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcZoom.Dispose()
      dsZoom.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      oCallParams.bPar1 = True
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCallParams.bPar1 = False
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvZoom_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvZoom.NTSFocusedRowChanged
    Try
      'per gli articoli TCO la quantità deve essere uguale alla qta residua. eventuali variazioni potranno essere eseguite all'interno del documento
      If grvZoom.NTSGetCurrentDataRow Is Nothing Then
        mm_quadaeva.Enabled = False
      Else
        If NTSCInt(grvZoom.NTSGetCurrentDataRow!xx_codtagl) <> 0 Then
          mm_quadaeva.Enabled = False
        Else
          GctlSetVisEnab(mm_quadaeva, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
