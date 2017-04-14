Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLCL
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
  Public WithEvents mm_cltipo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_clanno As NTSInformatica.NTSGridColumn
  Public WithEvents mm_clserie As NTSInformatica.NTSGridColumn
  Public WithEvents mm_clnum As NTSInformatica.NTSGridColumn
  Public WithEvents mm_clriga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_saldo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents xx_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prznet As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents mm_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mm_desint As NTSInformatica.NTSGridColumn

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.mm_cltipo = New NTSInformatica.NTSGridColumn
    Me.mm_clanno = New NTSInformatica.NTSGridColumn
    Me.mm_clserie = New NTSInformatica.NTSGridColumn
    Me.mm_clnum = New NTSInformatica.NTSGridColumn
    Me.mm_clriga = New NTSInformatica.NTSGridColumn
    Me.xx_saldo = New NTSInformatica.NTSGridColumn
    Me.xx_datdoc = New NTSInformatica.NTSGridColumn
    Me.xx_riferim = New NTSInformatica.NTSGridColumn
    Me.xx_annpar = New NTSInformatica.NTSGridColumn
    Me.xx_alfpar = New NTSInformatica.NTSGridColumn
    Me.xx_numpar = New NTSInformatica.NTSGridColumn
    Me.xx_datpar = New NTSInformatica.NTSGridColumn
    Me.xx_prznet = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_subcommeca = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.mm_descr = New NTSInformatica.NTSGridColumn
    Me.mm_desint = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.xx_desconto = New NTSInformatica.NTSGridColumn
    Me.mm_note = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(102, 442)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(7, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(83, 24)
    Me.cmdAnnulla.TabIndex = 11
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(7, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
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
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
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
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mm_cltipo, Me.mm_clanno, Me.mm_clserie, Me.mm_clnum, Me.mm_clriga, Me.xx_saldo, Me.xx_datdoc, Me.xx_riferim, Me.xx_annpar, Me.xx_alfpar, Me.xx_numpar, Me.xx_datpar, Me.xx_prznet, Me.mm_commeca, Me.xx_commeca, Me.mm_subcommeca, Me.xx_lottox, Me.mm_descr, Me.mm_desint, Me.xx_conto, Me.xx_desconto, Me.mm_note})
    Me.grvZoom.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvZoom.Enabled = True
    Me.grvZoom.GridControl = Me.grZoom
    Me.grvZoom.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvZoom.MinRowHeight = 14
    Me.grvZoom.Name = "grvZoom"
    Me.grvZoom.NTSAllowDelete = True
    Me.grvZoom.NTSAllowInsert = True
    Me.grvZoom.NTSAllowUpdate = True
    Me.grvZoom.NTSMenuContext = Nothing
    Me.grvZoom.OptionsCustomization.AllowRowSizing = True
    Me.grvZoom.OptionsFilter.AllowFilterEditor = False
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
    'mm_cltipo
    '
    Me.mm_cltipo.AppearanceCell.Options.UseBackColor = True
    Me.mm_cltipo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_cltipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_cltipo.Caption = "Tipo"
    Me.mm_cltipo.Enabled = True
    Me.mm_cltipo.FieldName = "mm_cltipo"
    Me.mm_cltipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_cltipo.Name = "mm_cltipo"
    Me.mm_cltipo.NTSRepositoryComboBox = Nothing
    Me.mm_cltipo.NTSRepositoryItemCheck = Nothing
    Me.mm_cltipo.NTSRepositoryItemMemo = Nothing
    Me.mm_cltipo.NTSRepositoryItemText = Nothing
    Me.mm_cltipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_cltipo.OptionsFilter.AllowFilter = False
    Me.mm_cltipo.Visible = True
    Me.mm_cltipo.VisibleIndex = 0
    '
    'mm_clanno
    '
    Me.mm_clanno.AppearanceCell.Options.UseBackColor = True
    Me.mm_clanno.AppearanceCell.Options.UseTextOptions = True
    Me.mm_clanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_clanno.Caption = "Anno"
    Me.mm_clanno.Enabled = True
    Me.mm_clanno.FieldName = "mm_clanno"
    Me.mm_clanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_clanno.Name = "mm_clanno"
    Me.mm_clanno.NTSRepositoryComboBox = Nothing
    Me.mm_clanno.NTSRepositoryItemCheck = Nothing
    Me.mm_clanno.NTSRepositoryItemMemo = Nothing
    Me.mm_clanno.NTSRepositoryItemText = Nothing
    Me.mm_clanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_clanno.OptionsFilter.AllowFilter = False
    Me.mm_clanno.Visible = True
    Me.mm_clanno.VisibleIndex = 1
    '
    'mm_clserie
    '
    Me.mm_clserie.AppearanceCell.Options.UseBackColor = True
    Me.mm_clserie.AppearanceCell.Options.UseTextOptions = True
    Me.mm_clserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_clserie.Caption = "Serie"
    Me.mm_clserie.Enabled = True
    Me.mm_clserie.FieldName = "mm_clserie"
    Me.mm_clserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_clserie.Name = "mm_clserie"
    Me.mm_clserie.NTSRepositoryComboBox = Nothing
    Me.mm_clserie.NTSRepositoryItemCheck = Nothing
    Me.mm_clserie.NTSRepositoryItemMemo = Nothing
    Me.mm_clserie.NTSRepositoryItemText = Nothing
    Me.mm_clserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_clserie.OptionsFilter.AllowFilter = False
    Me.mm_clserie.Visible = True
    Me.mm_clserie.VisibleIndex = 2
    '
    'mm_clnum
    '
    Me.mm_clnum.AppearanceCell.Options.UseBackColor = True
    Me.mm_clnum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_clnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_clnum.Caption = "Numero"
    Me.mm_clnum.Enabled = True
    Me.mm_clnum.FieldName = "mm_clnum"
    Me.mm_clnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_clnum.Name = "mm_clnum"
    Me.mm_clnum.NTSRepositoryComboBox = Nothing
    Me.mm_clnum.NTSRepositoryItemCheck = Nothing
    Me.mm_clnum.NTSRepositoryItemMemo = Nothing
    Me.mm_clnum.NTSRepositoryItemText = Nothing
    Me.mm_clnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_clnum.OptionsFilter.AllowFilter = False
    Me.mm_clnum.Visible = True
    Me.mm_clnum.VisibleIndex = 3
    '
    'mm_clriga
    '
    Me.mm_clriga.AppearanceCell.Options.UseBackColor = True
    Me.mm_clriga.AppearanceCell.Options.UseTextOptions = True
    Me.mm_clriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_clriga.Caption = "Riga"
    Me.mm_clriga.Enabled = True
    Me.mm_clriga.FieldName = "mm_clriga"
    Me.mm_clriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_clriga.Name = "mm_clriga"
    Me.mm_clriga.NTSRepositoryComboBox = Nothing
    Me.mm_clriga.NTSRepositoryItemCheck = Nothing
    Me.mm_clriga.NTSRepositoryItemMemo = Nothing
    Me.mm_clriga.NTSRepositoryItemText = Nothing
    Me.mm_clriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_clriga.OptionsFilter.AllowFilter = False
    Me.mm_clriga.Visible = True
    Me.mm_clriga.VisibleIndex = 4
    '
    'xx_saldo
    '
    Me.xx_saldo.AppearanceCell.Options.UseBackColor = True
    Me.xx_saldo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_saldo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_saldo.Caption = "Saldo"
    Me.xx_saldo.Enabled = True
    Me.xx_saldo.FieldName = "xx_saldo"
    Me.xx_saldo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_saldo.Name = "xx_saldo"
    Me.xx_saldo.NTSRepositoryComboBox = Nothing
    Me.xx_saldo.NTSRepositoryItemCheck = Nothing
    Me.xx_saldo.NTSRepositoryItemMemo = Nothing
    Me.xx_saldo.NTSRepositoryItemText = Nothing
    Me.xx_saldo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_saldo.OptionsFilter.AllowFilter = False
    Me.xx_saldo.Visible = True
    Me.xx_saldo.VisibleIndex = 5
    '
    'xx_datdoc
    '
    Me.xx_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.xx_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datdoc.Caption = "Data doc."
    Me.xx_datdoc.Enabled = True
    Me.xx_datdoc.FieldName = "xx_datdoc"
    Me.xx_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datdoc.Name = "xx_datdoc"
    Me.xx_datdoc.NTSRepositoryComboBox = Nothing
    Me.xx_datdoc.NTSRepositoryItemCheck = Nothing
    Me.xx_datdoc.NTSRepositoryItemMemo = Nothing
    Me.xx_datdoc.NTSRepositoryItemText = Nothing
    Me.xx_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datdoc.OptionsFilter.AllowFilter = False
    Me.xx_datdoc.Visible = True
    Me.xx_datdoc.VisibleIndex = 6
    '
    'xx_riferim
    '
    Me.xx_riferim.AppearanceCell.Options.UseBackColor = True
    Me.xx_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.xx_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_riferim.Caption = "Riferim."
    Me.xx_riferim.Enabled = True
    Me.xx_riferim.FieldName = "xx_riferim"
    Me.xx_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_riferim.Name = "xx_riferim"
    Me.xx_riferim.NTSRepositoryComboBox = Nothing
    Me.xx_riferim.NTSRepositoryItemCheck = Nothing
    Me.xx_riferim.NTSRepositoryItemMemo = Nothing
    Me.xx_riferim.NTSRepositoryItemText = Nothing
    Me.xx_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_riferim.OptionsFilter.AllowFilter = False
    Me.xx_riferim.Visible = True
    Me.xx_riferim.VisibleIndex = 7
    '
    'xx_annpar
    '
    Me.xx_annpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_annpar.Caption = "Anno P."
    Me.xx_annpar.Enabled = True
    Me.xx_annpar.FieldName = "xx_annpar"
    Me.xx_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_annpar.Name = "xx_annpar"
    Me.xx_annpar.NTSRepositoryComboBox = Nothing
    Me.xx_annpar.NTSRepositoryItemCheck = Nothing
    Me.xx_annpar.NTSRepositoryItemMemo = Nothing
    Me.xx_annpar.NTSRepositoryItemText = Nothing
    Me.xx_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_annpar.OptionsFilter.AllowFilter = False
    Me.xx_annpar.Visible = True
    Me.xx_annpar.VisibleIndex = 8
    '
    'xx_alfpar
    '
    Me.xx_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_alfpar.Caption = "Serie P."
    Me.xx_alfpar.Enabled = True
    Me.xx_alfpar.FieldName = "xx_alfpar"
    Me.xx_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_alfpar.Name = "xx_alfpar"
    Me.xx_alfpar.NTSRepositoryComboBox = Nothing
    Me.xx_alfpar.NTSRepositoryItemCheck = Nothing
    Me.xx_alfpar.NTSRepositoryItemMemo = Nothing
    Me.xx_alfpar.NTSRepositoryItemText = Nothing
    Me.xx_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_alfpar.OptionsFilter.AllowFilter = False
    Me.xx_alfpar.Visible = True
    Me.xx_alfpar.VisibleIndex = 9
    '
    'xx_numpar
    '
    Me.xx_numpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numpar.Caption = "Num. P."
    Me.xx_numpar.Enabled = True
    Me.xx_numpar.FieldName = "xx_numpar"
    Me.xx_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numpar.Name = "xx_numpar"
    Me.xx_numpar.NTSRepositoryComboBox = Nothing
    Me.xx_numpar.NTSRepositoryItemCheck = Nothing
    Me.xx_numpar.NTSRepositoryItemMemo = Nothing
    Me.xx_numpar.NTSRepositoryItemText = Nothing
    Me.xx_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numpar.OptionsFilter.AllowFilter = False
    Me.xx_numpar.Visible = True
    Me.xx_numpar.VisibleIndex = 10
    '
    'xx_datpar
    '
    Me.xx_datpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datpar.Caption = "Data P."
    Me.xx_datpar.Enabled = True
    Me.xx_datpar.FieldName = "xx_datpar"
    Me.xx_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datpar.Name = "xx_datpar"
    Me.xx_datpar.NTSRepositoryComboBox = Nothing
    Me.xx_datpar.NTSRepositoryItemCheck = Nothing
    Me.xx_datpar.NTSRepositoryItemMemo = Nothing
    Me.xx_datpar.NTSRepositoryItemText = Nothing
    Me.xx_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datpar.OptionsFilter.AllowFilter = False
    Me.xx_datpar.Visible = True
    Me.xx_datpar.VisibleIndex = 11
    '
    'xx_prznet
    '
    Me.xx_prznet.AppearanceCell.Options.UseBackColor = True
    Me.xx_prznet.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prznet.Caption = "Prezzo netto"
    Me.xx_prznet.Enabled = True
    Me.xx_prznet.FieldName = "xx_prznet"
    Me.xx_prznet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prznet.Name = "xx_prznet"
    Me.xx_prznet.NTSRepositoryComboBox = Nothing
    Me.xx_prznet.NTSRepositoryItemCheck = Nothing
    Me.xx_prznet.NTSRepositoryItemMemo = Nothing
    Me.xx_prznet.NTSRepositoryItemText = Nothing
    Me.xx_prznet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prznet.OptionsFilter.AllowFilter = False
    Me.xx_prznet.Visible = True
    Me.xx_prznet.VisibleIndex = 12
    '
    'mm_commeca
    '
    Me.mm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_commeca.Caption = "Commessa"
    Me.mm_commeca.Enabled = True
    Me.mm_commeca.FieldName = "mm_commeca"
    Me.mm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_commeca.Name = "mm_commeca"
    Me.mm_commeca.NTSRepositoryComboBox = Nothing
    Me.mm_commeca.NTSRepositoryItemCheck = Nothing
    Me.mm_commeca.NTSRepositoryItemMemo = Nothing
    Me.mm_commeca.NTSRepositoryItemText = Nothing
    Me.mm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_commeca.OptionsFilter.AllowFilter = False
    Me.mm_commeca.Visible = True
    Me.mm_commeca.VisibleIndex = 13
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 14
    '
    'mm_subcommeca
    '
    Me.mm_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_subcommeca.Caption = "Sottocommessa"
    Me.mm_subcommeca.Enabled = True
    Me.mm_subcommeca.FieldName = "mm_subcommeca"
    Me.mm_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_subcommeca.Name = "mm_subcommeca"
    Me.mm_subcommeca.NTSRepositoryComboBox = Nothing
    Me.mm_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.mm_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.mm_subcommeca.NTSRepositoryItemText = Nothing
    Me.mm_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_subcommeca.OptionsFilter.AllowFilter = False
    Me.mm_subcommeca.Visible = True
    Me.mm_subcommeca.VisibleIndex = 15
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 16
    '
    'mm_descr
    '
    Me.mm_descr.AppearanceCell.Options.UseBackColor = True
    Me.mm_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mm_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_descr.Caption = "Descrizione"
    Me.mm_descr.Enabled = True
    Me.mm_descr.FieldName = "mm_descr"
    Me.mm_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_descr.Name = "mm_descr"
    Me.mm_descr.NTSRepositoryComboBox = Nothing
    Me.mm_descr.NTSRepositoryItemCheck = Nothing
    Me.mm_descr.NTSRepositoryItemMemo = Nothing
    Me.mm_descr.NTSRepositoryItemText = Nothing
    Me.mm_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_descr.OptionsFilter.AllowFilter = False
    Me.mm_descr.Visible = True
    Me.mm_descr.VisibleIndex = 17
    '
    'mm_desint
    '
    Me.mm_desint.AppearanceCell.Options.UseBackColor = True
    Me.mm_desint.AppearanceCell.Options.UseTextOptions = True
    Me.mm_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_desint.Caption = "Descr. interna"
    Me.mm_desint.Enabled = True
    Me.mm_desint.FieldName = "mm_desint"
    Me.mm_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_desint.Name = "mm_desint"
    Me.mm_desint.NTSRepositoryComboBox = Nothing
    Me.mm_desint.NTSRepositoryItemCheck = Nothing
    Me.mm_desint.NTSRepositoryItemMemo = Nothing
    Me.mm_desint.NTSRepositoryItemText = Nothing
    Me.mm_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_desint.OptionsFilter.AllowFilter = False
    Me.mm_desint.Visible = True
    Me.mm_desint.VisibleIndex = 18
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Conto cli/forn"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsFilter.AllowFilter = False
    '
    'xx_desconto
    '
    Me.xx_desconto.AppearanceCell.Options.UseBackColor = True
    Me.xx_desconto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desconto.Caption = "Descr. conto cli/forn"
    Me.xx_desconto.Enabled = True
    Me.xx_desconto.FieldName = "xx_desconto"
    Me.xx_desconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desconto.Name = "xx_desconto"
    Me.xx_desconto.NTSRepositoryComboBox = Nothing
    Me.xx_desconto.NTSRepositoryItemCheck = Nothing
    Me.xx_desconto.NTSRepositoryItemMemo = Nothing
    Me.xx_desconto.NTSRepositoryItemText = Nothing
    Me.xx_desconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desconto.OptionsFilter.AllowFilter = False
    '
    'mm_note
    '
    Me.mm_note.AppearanceCell.Options.UseBackColor = True
    Me.mm_note.AppearanceCell.Options.UseTextOptions = True
    Me.mm_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_note.Caption = "Note"
    Me.mm_note.Enabled = True
    Me.mm_note.FieldName = "mm_note"
    Me.mm_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_note.Name = "mm_note"
    Me.mm_note.NTSRepositoryComboBox = Nothing
    Me.mm_note.NTSRepositoryItemCheck = Nothing
    Me.mm_note.NTSRepositoryItemMemo = Nothing
    Me.mm_note.NTSRepositoryItemText = Nothing
    Me.mm_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_note.OptionsFilter.AllowFilter = False
    Me.mm_note.Visible = True
    Me.mm_note.VisibleIndex = 19
    '
    'FRMMGHLCL
    '
    Me.AcceptButton = Me.cmdConferma
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMMGHLCL"
    Me.Text = "RIGHE BOLLE C/TO LAVORO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGHLCL", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825587398175, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGHLCL", strRemoteServer, strRemotePort)
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

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 130086826458804425, "Bolle c/to lavoro aperte"))
      mm_cltipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128696757640312500, "Tipo"), dttTipoRk, "val", "cod")
      mm_clanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086826643023175, "Anno"), "0", 4, 0, 9999)
      mm_clserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086826846773175, "Serie"), 1, True)
      mm_clnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086826996929425, "Numero"), "0", 9, 0, 999999999)
      mm_clriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827019585675, "Riga"), "0", 9, 0, 999999999)
      xx_saldo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827039429425, "Saldo"), oApp.FormatQta, 15)
      xx_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130086827075679425, "Data doc."), True)
      xx_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827191616925, "Riferim."), 0, True)
      xx_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827215366925, "Anno P."), "0", 4)
      xx_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827233804425, "Serie P."), 1, True)
      xx_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827257866925, "Num. P."), "0", 9)
      xx_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130086827277398175, "Data P."), True)
      xx_prznet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827296460675, "Prezzo netto"), oApp.FormatPrzUn, 9)
      mm_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130086827314429425, "Commessa"), "0", 9, 0, 999999999)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827349898175, "Descr. commessa"), 0, True)
      mm_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827400679425, "Sottocommessa"), 2, True)
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827433804425, "Lotto"), 0, True)
      mm_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827457085675, "Descrizione"), 0, True)
      mm_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130086827478179425, "Descr. interna"), 0, True)
      xx_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130269876286845906, "Conto cli/forn"), "0", 9, 0, 999999999)
      xx_desconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130269876301305875, "Descr. conto cli/forn"), 0, True)
      mm_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130861806283768154, "Note"), 0, True)

      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowInsert = False
      grvZoom.Enabled = False

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

  Public Overridable Sub FRMMGHLCL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego i dati
      dsZoom.Tables.Add(CType(oCallParams.ctlPar1, DataTable))
      dcZoom.DataSource = dsZoom.Tables(0)
      grZoom.DataSource = dcZoom

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If oCallParams.strPar2 = "SALCL" Then
        Me.Text = oApp.Tr(Me, 128775260725202000, "RIGHE BOLLE C/TO LAVORO")

        Select Case NTSCInt(oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "RiportaDescrNoteDaCaricoCLAV", "0", " ", "0"))
          Case 0
            'nascondo le colonne non presenti nel datatable
            mm_descr.Visible = False
            mm_desint.Visible = False
          Case -1
            mm_descr.Visible = True
            mm_desint.Visible = True
        End Select
      End If

      If oCallParams.strPar2 = "SALCP" Then
        Me.Text = oApp.Tr(Me, 128775260751722000, "RIGHE CARICHI DI PRODUZIONE")
      End If

      Me.Text += oApp.Tr(Me, 128699443720937500, " - (Articolo: |" & oCallParams.strPar1 & "|, fase: |" & oCallParams.dPar1.ToString & "|, Magazzino: |" & oCallParams.dPar2.ToString & "|)")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLCL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcZoom.Dispose()
      dsZoom.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
        '-------------------------
        'cancello tutte le righe non selezionate
        With grvZoom.NTSGetCurrentDataRow
          oCallParams.dPar1 = NTSCDec(!xx_saldo)               'xx_saldo
          oCallParams.strPar1 = !mm_cltipo.ToString         'tipork
          oCallParams.dPar2 = NTSCInt(!mm_clanno)           'anno
          oCallParams.strPar2 = !mm_clserie.ToString        'serie
          oCallParams.dPar3 = NTSCInt(!mm_clnum)            'numdoc
          oCallParams.dPar4 = NTSCInt(!mm_clriga)           'riga
          If (NTSCInt(oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "RiportaDescrNoteDaCaricoCLAV", "0", " ", "0")) = -1) Then
            oCallParams.strPar3 = NTSCStr(!mm_descr)
            oCallParams.strPar4 = NTSCStr(!mm_desint)
            oCallParams.strPar5 = NTSCStr(!mm_note)
          End If
        End With

        oCallParams.bPar1 = True
        Me.Close()
      End If

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

  Public Overridable Sub grvZoom_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvZoom.DoubleClick
    Try
      cmdConferma_Click(cmdConferma, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
