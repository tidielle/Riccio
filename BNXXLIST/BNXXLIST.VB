Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class NTSXXLIST

  Public oCleList As CLEXXLIST
  Public oCallParams As CLE__CLDP
  Public dsListConfiguratore As DataSet
  Public strCodcas As String
  Public dsList As New DataSet
  Public dcList As BindingSource = New BindingSource()
  Public oParent As FRM__CHIL = Nothing
  Public dttUm As New DataTable

  Public WithEvents pnListini As NTSInformatica.NTSPanel
  Public WithEvents grList As NTSInformatica.NTSGrid
  Public WithEvents grvList As NTSInformatica.NTSGridView
  Public WithEvents pnListDx As NTSInformatica.NTSPanel
  Public WithEvents lbtipolist As NTSInformatica.NTSLabel
  Public WithEvents cbTipolistino As NTSInformatica.NTSComboBox
  Public WithEvents fmValuta As NTSInformatica.NTSGroupBox
  Public WithEvents lbCodvalu As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu As NTSInformatica.NTSTextBoxNum
  Public WithEvents opValAltro As NTSInformatica.NTSRadioButton
  Public WithEvents opValEur As NTSInformatica.NTSRadioButton
  Public WithEvents ckPromo As NTSInformatica.NTSCheckBox
  Public WithEvents fmValidita As NTSInformatica.NTSGroupBox
  Public WithEvents edDtval As NTSInformatica.NTSTextBoxData
  Public WithEvents opValDay As NTSInformatica.NTSRadioButton
  Public WithEvents opValTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lc_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codart As NTSInformatica.NTSGridColumn
  Public WithEvents lc_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents lc_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents lc_listino As NTSInformatica.NTSGridColumn
  Public WithEvents lc_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_daquant As NTSInformatica.NTSGridColumn
  Public WithEvents lc_aquant As NTSInformatica.NTSGridColumn
  Public WithEvents lc_datagg As NTSInformatica.NTSGridColumn
  Public WithEvents lc_datscad As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents lc_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents lc_netto As NTSInformatica.NTSGridColumn
  Public WithEvents lc_note As NTSInformatica.NTSGridColumn
  Public WithEvents xx_listino As NTSInformatica.NTSGridColumn
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbListNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbListSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbListRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbListCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbListZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbListEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents cmdSconto As NTSInformatica.NTSButton
  Public WithEvents lc_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desint As NTSInformatica.NTSGridColumn

  Public Event VaiScontoCollegato As NTSEventHandler 'scateno l'evento passando i parametri per far passare a BNXXLIST e posizionarsi sul listino collegato (sconto specifico cli/arti->listino specifico)


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NTSXXLIST))
    Me.pnListini = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grList = New NTSInformatica.NTSGrid
    Me.grvList = New NTSInformatica.NTSGridView
    Me.lc_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.lc_codart = New NTSInformatica.NTSGridColumn
    Me.xx_codart = New NTSInformatica.NTSGridColumn
    Me.lc_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.lc_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.lc_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.lc_tipo = New NTSInformatica.NTSGridColumn
    Me.lc_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    Me.lc_listino = New NTSInformatica.NTSGridColumn
    Me.lc_prezzo = New NTSInformatica.NTSGridColumn
    Me.lc_daquant = New NTSInformatica.NTSGridColumn
    Me.lc_aquant = New NTSInformatica.NTSGridColumn
    Me.lc_datagg = New NTSInformatica.NTSGridColumn
    Me.lc_datscad = New NTSInformatica.NTSGridColumn
    Me.lc_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_codarfo = New NTSInformatica.NTSGridColumn
    Me.lc_perqta = New NTSInformatica.NTSGridColumn
    Me.lc_unmis = New NTSInformatica.NTSGridColumn
    Me.lc_netto = New NTSInformatica.NTSGridColumn
    Me.lc_note = New NTSInformatica.NTSGridColumn
    Me.xx_listino = New NTSInformatica.NTSGridColumn
    Me.xx_desint = New NTSInformatica.NTSGridColumn
    Me.pnListDx = New NTSInformatica.NTSPanel
    Me.cmdSconto = New NTSInformatica.NTSButton
    Me.lbtipolist = New NTSInformatica.NTSLabel
    Me.cbTipolistino = New NTSInformatica.NTSComboBox
    Me.fmValuta = New NTSInformatica.NTSGroupBox
    Me.lbCodvalu = New NTSInformatica.NTSLabel
    Me.edCodvalu = New NTSInformatica.NTSTextBoxNum
    Me.opValAltro = New NTSInformatica.NTSRadioButton
    Me.opValEur = New NTSInformatica.NTSRadioButton
    Me.ckPromo = New NTSInformatica.NTSCheckBox
    Me.fmValidita = New NTSInformatica.NTSGroupBox
    Me.edDtval = New NTSInformatica.NTSTextBoxData
    Me.opValDay = New NTSInformatica.NTSRadioButton
    Me.opValTutti = New NTSInformatica.NTSRadioButton
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbListNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbListSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbListRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbListCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbListZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbListEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.pnListini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnListini.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnListDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnListDx.SuspendLayout()
    CType(Me.cbTipolistino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValuta.SuspendLayout()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValAltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValEur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPromo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValidita.SuspendLayout()
    CType(Me.edDtval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pnListini
    '
    Me.pnListini.AllowDrop = True
    Me.pnListini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnListini.Appearance.Options.UseBackColor = True
    Me.pnListini.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnListini.Controls.Add(Me.pnGrid)
    Me.pnListini.Controls.Add(Me.pnListDx)
    Me.pnListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnListini.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnListini.Location = New System.Drawing.Point(0, 30)
    Me.pnListini.Name = "pnListini"
    Me.pnListini.NTSActiveTrasparency = True
    Me.pnListini.Size = New System.Drawing.Size(506, 295)
    Me.pnListini.TabIndex = 0
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grList)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(306, 295)
    Me.pnGrid.TabIndex = 9
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grList
    '
    Me.grList.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grList.EmbeddedNavigator.Name = ""
    Me.grList.Location = New System.Drawing.Point(0, 0)
    Me.grList.MainView = Me.grvList
    Me.grList.Name = "grList"
    Me.grList.Size = New System.Drawing.Size(306, 295)
    Me.grList.TabIndex = 7
    Me.grList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvList})
    '
    'grvList
    '
    Me.grvList.ActiveFilterEnabled = False
    '
    'lc_coddest
    '
    Me.lc_coddest.AppearanceCell.Options.UseBackColor = True
    Me.lc_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.lc_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_coddest.Caption = "Destinaz."
    Me.lc_coddest.Enabled = True
    Me.lc_coddest.FieldName = "lc_coddest"
    Me.lc_coddest.Name = "lc_coddest"
    Me.lc_coddest.NTSRepositoryComboBox = Nothing
    Me.lc_coddest.NTSRepositoryItemCheck = Nothing
    Me.lc_coddest.NTSRepositoryItemMemo = Nothing
    Me.lc_coddest.NTSRepositoryItemText = Nothing
    Me.lc_coddest.Visible = True
    Me.lc_coddest.VisibleIndex = 10
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr.destinaz."
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 11
    Me.grvList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lc_codart, Me.xx_codart, Me.lc_fase, Me.xx_fase, Me.lc_codlavo, Me.xx_codlavo, Me.lc_conto, Me.xx_conto, Me.lc_tipo, Me.lc_codvalu, Me.xx_codvalu, Me.lc_listino, Me.lc_prezzo, Me.lc_daquant, Me.lc_aquant, Me.lc_datagg, Me.lc_datscad, Me.lc_codtpro, Me.xx_codtpro, Me.xx_codarfo, Me.lc_perqta, Me.lc_unmis, Me.lc_netto, Me.lc_note, Me.xx_listino, Me.xx_desint, Me.lc_coddest, Me.xx_coddest})
    Me.grvList.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvList.Enabled = True
    Me.grvList.GridControl = Me.grList
    Me.grvList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvList.MinRowHeight = 14
    Me.grvList.Name = "grvList"
    Me.grvList.NTSAllowDelete = True
    Me.grvList.NTSAllowInsert = True
    Me.grvList.NTSAllowUpdate = True
    Me.grvList.NTSMenuContext = Nothing
    Me.grvList.OptionsCustomization.AllowRowSizing = True
    Me.grvList.OptionsFilter.AllowFilterEditor = False
    Me.grvList.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvList.OptionsNavigation.UseTabKey = False
    Me.grvList.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvList.OptionsView.ColumnAutoWidth = False
    Me.grvList.OptionsView.EnableAppearanceEvenRow = True
    Me.grvList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvList.OptionsView.ShowGroupPanel = False
    Me.grvList.RowHeight = 14
    '
    'lc_codart
    '
    Me.lc_codart.AppearanceCell.Options.UseBackColor = True
    Me.lc_codart.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codart.Caption = "Cod. articolo"
    Me.lc_codart.Enabled = True
    Me.lc_codart.FieldName = "lc_codart"
    Me.lc_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codart.Name = "lc_codart"
    Me.lc_codart.NTSRepositoryComboBox = Nothing
    Me.lc_codart.NTSRepositoryItemCheck = Nothing
    Me.lc_codart.NTSRepositoryItemMemo = Nothing
    Me.lc_codart.NTSRepositoryItemText = Nothing
    Me.lc_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codart.OptionsFilter.AllowFilter = False
    Me.lc_codart.Visible = True
    Me.lc_codart.VisibleIndex = 0
    '
    'xx_codart
    '
    Me.xx_codart.AppearanceCell.Options.UseBackColor = True
    Me.xx_codart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codart.Caption = "Descr. articolo"
    Me.xx_codart.Enabled = False
    Me.xx_codart.FieldName = "xx_codart"
    Me.xx_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codart.Name = "xx_codart"
    Me.xx_codart.NTSRepositoryComboBox = Nothing
    Me.xx_codart.NTSRepositoryItemCheck = Nothing
    Me.xx_codart.NTSRepositoryItemMemo = Nothing
    Me.xx_codart.NTSRepositoryItemText = Nothing
    Me.xx_codart.OptionsColumn.AllowEdit = False
    Me.xx_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codart.OptionsColumn.ReadOnly = True
    Me.xx_codart.OptionsFilter.AllowFilter = False
    Me.xx_codart.Visible = True
    Me.xx_codart.VisibleIndex = 1
    '
    'lc_fase
    '
    Me.lc_fase.AppearanceCell.Options.UseBackColor = True
    Me.lc_fase.AppearanceCell.Options.UseTextOptions = True
    Me.lc_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_fase.Caption = "Fase"
    Me.lc_fase.Enabled = True
    Me.lc_fase.FieldName = "lc_fase"
    Me.lc_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_fase.Name = "lc_fase"
    Me.lc_fase.NTSRepositoryComboBox = Nothing
    Me.lc_fase.NTSRepositoryItemCheck = Nothing
    Me.lc_fase.NTSRepositoryItemMemo = Nothing
    Me.lc_fase.NTSRepositoryItemText = Nothing
    Me.lc_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_fase.OptionsFilter.AllowFilter = False
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descr. fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    '
    'lc_codlavo
    '
    Me.lc_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.lc_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codlavo.Caption = "Cod.lav."
    Me.lc_codlavo.Enabled = True
    Me.lc_codlavo.FieldName = "lc_codlavo"
    Me.lc_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codlavo.Name = "lc_codlavo"
    Me.lc_codlavo.NTSRepositoryComboBox = Nothing
    Me.lc_codlavo.NTSRepositoryItemCheck = Nothing
    Me.lc_codlavo.NTSRepositoryItemMemo = Nothing
    Me.lc_codlavo.NTSRepositoryItemText = Nothing
    Me.lc_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codlavo.OptionsFilter.AllowFilter = False
    '
    'xx_codlavo
    '
    Me.xx_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlavo.Caption = "Descr.lav."
    Me.xx_codlavo.Enabled = False
    Me.xx_codlavo.FieldName = "xx_codlavo"
    Me.xx_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlavo.Name = "xx_codlavo"
    Me.xx_codlavo.NTSRepositoryComboBox = Nothing
    Me.xx_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xx_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xx_codlavo.NTSRepositoryItemText = Nothing
    Me.xx_codlavo.OptionsColumn.AllowEdit = False
    Me.xx_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlavo.OptionsColumn.ReadOnly = True
    Me.xx_codlavo.OptionsFilter.AllowFilter = False
    '
    'lc_conto
    '
    Me.lc_conto.AppearanceCell.Options.UseBackColor = True
    Me.lc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_conto.Caption = "Cliente/forn."
    Me.lc_conto.Enabled = True
    Me.lc_conto.FieldName = "lc_conto"
    Me.lc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_conto.Name = "lc_conto"
    Me.lc_conto.NTSRepositoryComboBox = Nothing
    Me.lc_conto.NTSRepositoryItemCheck = Nothing
    Me.lc_conto.NTSRepositoryItemMemo = Nothing
    Me.lc_conto.NTSRepositoryItemText = Nothing
    Me.lc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_conto.OptionsFilter.AllowFilter = False
    Me.lc_conto.Visible = True
    Me.lc_conto.VisibleIndex = 2
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descrizione"
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
    Me.xx_conto.VisibleIndex = 3
    '
    'lc_tipo
    '
    Me.lc_tipo.AppearanceCell.Options.UseBackColor = True
    Me.lc_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_tipo.Caption = "Tipo"
    Me.lc_tipo.Enabled = False
    Me.lc_tipo.FieldName = "lc_tipo"
    Me.lc_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_tipo.Name = "lc_tipo"
    Me.lc_tipo.NTSRepositoryComboBox = Nothing
    Me.lc_tipo.NTSRepositoryItemCheck = Nothing
    Me.lc_tipo.NTSRepositoryItemMemo = Nothing
    Me.lc_tipo.NTSRepositoryItemText = Nothing
    Me.lc_tipo.OptionsColumn.AllowEdit = False
    Me.lc_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_tipo.OptionsColumn.ReadOnly = True
    Me.lc_tipo.OptionsFilter.AllowFilter = False
    '
    'lc_codvalu
    '
    Me.lc_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.lc_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codvalu.Caption = "Valuta"
    Me.lc_codvalu.Enabled = True
    Me.lc_codvalu.FieldName = "lc_codvalu"
    Me.lc_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codvalu.Name = "lc_codvalu"
    Me.lc_codvalu.NTSRepositoryComboBox = Nothing
    Me.lc_codvalu.NTSRepositoryItemCheck = Nothing
    Me.lc_codvalu.NTSRepositoryItemMemo = Nothing
    Me.lc_codvalu.NTSRepositoryItemText = Nothing
    Me.lc_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codvalu.OptionsFilter.AllowFilter = False
    '
    'xx_codvalu
    '
    Me.xx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvalu.Caption = "Descr.valuta"
    Me.xx_codvalu.Enabled = False
    Me.xx_codvalu.FieldName = "xx_codvalu"
    Me.xx_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvalu.Name = "xx_codvalu"
    Me.xx_codvalu.NTSRepositoryComboBox = Nothing
    Me.xx_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xx_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xx_codvalu.NTSRepositoryItemText = Nothing
    Me.xx_codvalu.OptionsColumn.AllowEdit = False
    Me.xx_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvalu.OptionsColumn.ReadOnly = True
    Me.xx_codvalu.OptionsFilter.AllowFilter = False
    '
    'lc_listino
    '
    Me.lc_listino.AppearanceCell.Options.UseBackColor = True
    Me.lc_listino.AppearanceCell.Options.UseTextOptions = True
    Me.lc_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_listino.Caption = "Listino"
    Me.lc_listino.Enabled = True
    Me.lc_listino.FieldName = "lc_listino"
    Me.lc_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_listino.Name = "lc_listino"
    Me.lc_listino.NTSRepositoryComboBox = Nothing
    Me.lc_listino.NTSRepositoryItemCheck = Nothing
    Me.lc_listino.NTSRepositoryItemMemo = Nothing
    Me.lc_listino.NTSRepositoryItemText = Nothing
    Me.lc_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_listino.OptionsFilter.AllowFilter = False
    Me.lc_listino.Visible = True
    Me.lc_listino.VisibleIndex = 4
    '
    'lc_prezzo
    '
    Me.lc_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.lc_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_prezzo.Caption = "Prezzo"
    Me.lc_prezzo.Enabled = True
    Me.lc_prezzo.FieldName = "lc_prezzo"
    Me.lc_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_prezzo.Name = "lc_prezzo"
    Me.lc_prezzo.NTSRepositoryComboBox = Nothing
    Me.lc_prezzo.NTSRepositoryItemCheck = Nothing
    Me.lc_prezzo.NTSRepositoryItemMemo = Nothing
    Me.lc_prezzo.NTSRepositoryItemText = Nothing
    Me.lc_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_prezzo.OptionsFilter.AllowFilter = False
    Me.lc_prezzo.Visible = True
    Me.lc_prezzo.VisibleIndex = 5
    '
    'lc_daquant
    '
    Me.lc_daquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_daquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_daquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_daquant.Caption = "Da quantità"
    Me.lc_daquant.Enabled = True
    Me.lc_daquant.FieldName = "lc_daquant"
    Me.lc_daquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_daquant.Name = "lc_daquant"
    Me.lc_daquant.NTSRepositoryComboBox = Nothing
    Me.lc_daquant.NTSRepositoryItemCheck = Nothing
    Me.lc_daquant.NTSRepositoryItemMemo = Nothing
    Me.lc_daquant.NTSRepositoryItemText = Nothing
    Me.lc_daquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_daquant.OptionsFilter.AllowFilter = False
    '
    'lc_aquant
    '
    Me.lc_aquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_aquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_aquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_aquant.Caption = "A quantità"
    Me.lc_aquant.Enabled = True
    Me.lc_aquant.FieldName = "lc_aquant"
    Me.lc_aquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_aquant.Name = "lc_aquant"
    Me.lc_aquant.NTSRepositoryComboBox = Nothing
    Me.lc_aquant.NTSRepositoryItemCheck = Nothing
    Me.lc_aquant.NTSRepositoryItemMemo = Nothing
    Me.lc_aquant.NTSRepositoryItemText = Nothing
    Me.lc_aquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_aquant.OptionsFilter.AllowFilter = False
    '
    'lc_datagg
    '
    Me.lc_datagg.AppearanceCell.Options.UseBackColor = True
    Me.lc_datagg.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datagg.Caption = "Data aggiorn."
    Me.lc_datagg.Enabled = True
    Me.lc_datagg.FieldName = "lc_datagg"
    Me.lc_datagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datagg.Name = "lc_datagg"
    Me.lc_datagg.NTSRepositoryComboBox = Nothing
    Me.lc_datagg.NTSRepositoryItemCheck = Nothing
    Me.lc_datagg.NTSRepositoryItemMemo = Nothing
    Me.lc_datagg.NTSRepositoryItemText = Nothing
    Me.lc_datagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datagg.OptionsFilter.AllowFilter = False
    Me.lc_datagg.Visible = True
    Me.lc_datagg.VisibleIndex = 6
    '
    'lc_datscad
    '
    Me.lc_datscad.AppearanceCell.Options.UseBackColor = True
    Me.lc_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datscad.Caption = "Data scad."
    Me.lc_datscad.Enabled = True
    Me.lc_datscad.FieldName = "lc_datscad"
    Me.lc_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datscad.Name = "lc_datscad"
    Me.lc_datscad.NTSRepositoryComboBox = Nothing
    Me.lc_datscad.NTSRepositoryItemCheck = Nothing
    Me.lc_datscad.NTSRepositoryItemMemo = Nothing
    Me.lc_datscad.NTSRepositoryItemText = Nothing
    Me.lc_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datscad.OptionsFilter.AllowFilter = False
    Me.lc_datscad.Visible = True
    Me.lc_datscad.VisibleIndex = 7
    '
    'lc_codtpro
    '
    Me.lc_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.lc_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codtpro.Caption = "Cod. promoz."
    Me.lc_codtpro.Enabled = True
    Me.lc_codtpro.FieldName = "lc_codtpro"
    Me.lc_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codtpro.Name = "lc_codtpro"
    Me.lc_codtpro.NTSRepositoryComboBox = Nothing
    Me.lc_codtpro.NTSRepositoryItemCheck = Nothing
    Me.lc_codtpro.NTSRepositoryItemMemo = Nothing
    Me.lc_codtpro.NTSRepositoryItemText = Nothing
    Me.lc_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codtpro.OptionsFilter.AllowFilter = False
    '
    'xx_codtpro
    '
    Me.xx_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtpro.Caption = "Descr. promoz."
    Me.xx_codtpro.Enabled = False
    Me.xx_codtpro.FieldName = "xx_codtpro"
    Me.xx_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpro.Name = "xx_codtpro"
    Me.xx_codtpro.NTSRepositoryComboBox = Nothing
    Me.xx_codtpro.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpro.NTSRepositoryItemMemo = Nothing
    Me.xx_codtpro.NTSRepositoryItemText = Nothing
    Me.xx_codtpro.OptionsColumn.AllowEdit = False
    Me.xx_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpro.OptionsColumn.ReadOnly = True
    Me.xx_codtpro.OptionsFilter.AllowFilter = False
    '
    'xx_codarfo
    '
    Me.xx_codarfo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codarfo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codarfo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codarfo.Caption = "Cod.art.cli/for"
    Me.xx_codarfo.Enabled = False
    Me.xx_codarfo.FieldName = "xx_codarfo"
    Me.xx_codarfo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codarfo.Name = "xx_codarfo"
    Me.xx_codarfo.NTSRepositoryComboBox = Nothing
    Me.xx_codarfo.NTSRepositoryItemCheck = Nothing
    Me.xx_codarfo.NTSRepositoryItemMemo = Nothing
    Me.xx_codarfo.NTSRepositoryItemText = Nothing
    Me.xx_codarfo.OptionsColumn.AllowEdit = False
    Me.xx_codarfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codarfo.OptionsColumn.ReadOnly = True
    Me.xx_codarfo.OptionsFilter.AllowFilter = False
    '
    'lc_perqta
    '
    Me.lc_perqta.AppearanceCell.Options.UseBackColor = True
    Me.lc_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.lc_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_perqta.Caption = "Q.tà prezzo"
    Me.lc_perqta.Enabled = False
    Me.lc_perqta.FieldName = "lc_perqta"
    Me.lc_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_perqta.Name = "lc_perqta"
    Me.lc_perqta.NTSRepositoryComboBox = Nothing
    Me.lc_perqta.NTSRepositoryItemCheck = Nothing
    Me.lc_perqta.NTSRepositoryItemMemo = Nothing
    Me.lc_perqta.NTSRepositoryItemText = Nothing
    Me.lc_perqta.OptionsColumn.AllowEdit = False
    Me.lc_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_perqta.OptionsColumn.ReadOnly = True
    Me.lc_perqta.OptionsFilter.AllowFilter = False
    '
    'lc_unmis
    '
    Me.lc_unmis.AppearanceCell.Options.UseBackColor = True
    Me.lc_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.lc_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_unmis.Caption = "U.M."
    Me.lc_unmis.Enabled = True
    Me.lc_unmis.FieldName = "lc_unmis"
    Me.lc_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_unmis.Name = "lc_unmis"
    Me.lc_unmis.NTSRepositoryComboBox = Nothing
    Me.lc_unmis.NTSRepositoryItemCheck = Nothing
    Me.lc_unmis.NTSRepositoryItemMemo = Nothing
    Me.lc_unmis.NTSRepositoryItemText = Nothing
    Me.lc_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_unmis.OptionsFilter.AllowFilter = False
    Me.lc_unmis.Visible = True
    Me.lc_unmis.VisibleIndex = 8
    '
    'lc_netto
    '
    Me.lc_netto.AppearanceCell.Options.UseBackColor = True
    Me.lc_netto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_netto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_netto.Caption = "Prezzo netto"
    Me.lc_netto.Enabled = True
    Me.lc_netto.FieldName = "lc_netto"
    Me.lc_netto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_netto.Name = "lc_netto"
    Me.lc_netto.NTSRepositoryComboBox = Nothing
    Me.lc_netto.NTSRepositoryItemCheck = Nothing
    Me.lc_netto.NTSRepositoryItemMemo = Nothing
    Me.lc_netto.NTSRepositoryItemText = Nothing
    Me.lc_netto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_netto.OptionsFilter.AllowFilter = False
    '
    'lc_note
    '
    Me.lc_note.AppearanceCell.Options.UseBackColor = True
    Me.lc_note.AppearanceCell.Options.UseTextOptions = True
    Me.lc_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_note.Caption = "Note"
    Me.lc_note.Enabled = True
    Me.lc_note.FieldName = "lc_note"
    Me.lc_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_note.Name = "lc_note"
    Me.lc_note.NTSRepositoryComboBox = Nothing
    Me.lc_note.NTSRepositoryItemCheck = Nothing
    Me.lc_note.NTSRepositoryItemMemo = Nothing
    Me.lc_note.NTSRepositoryItemText = Nothing
    Me.lc_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_note.OptionsFilter.AllowFilter = False
    Me.lc_note.Visible = True
    Me.lc_note.VisibleIndex = 9
    '
    'xx_listino
    '
    Me.xx_listino.AppearanceCell.Options.UseBackColor = True
    Me.xx_listino.AppearanceCell.Options.UseTextOptions = True
    Me.xx_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_listino.Caption = "Descr. listino"
    Me.xx_listino.Enabled = False
    Me.xx_listino.FieldName = "xx_listino"
    Me.xx_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_listino.Name = "xx_listino"
    Me.xx_listino.NTSRepositoryComboBox = Nothing
    Me.xx_listino.NTSRepositoryItemCheck = Nothing
    Me.xx_listino.NTSRepositoryItemMemo = Nothing
    Me.xx_listino.NTSRepositoryItemText = Nothing
    Me.xx_listino.OptionsColumn.AllowEdit = False
    Me.xx_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_listino.OptionsColumn.ReadOnly = True
    Me.xx_listino.OptionsFilter.AllowFilter = False
    '
    'xx_desint
    '
    Me.xx_desint.AppearanceCell.Options.UseBackColor = True
    Me.xx_desint.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desint.Caption = "Descr. interna art."
    Me.xx_desint.Enabled = False
    Me.xx_desint.FieldName = "xx_desint"
    Me.xx_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desint.Name = "xx_desint"
    Me.xx_desint.NTSRepositoryComboBox = Nothing
    Me.xx_desint.NTSRepositoryItemCheck = Nothing
    Me.xx_desint.NTSRepositoryItemMemo = Nothing
    Me.xx_desint.NTSRepositoryItemText = Nothing
    Me.xx_desint.OptionsColumn.AllowEdit = False
    Me.xx_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desint.OptionsColumn.ReadOnly = True
    Me.xx_desint.OptionsFilter.AllowFilter = False
    '
    'pnListDx
    '
    Me.pnListDx.AllowDrop = True
    Me.pnListDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnListDx.Appearance.Options.UseBackColor = True
    Me.pnListDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnListDx.Controls.Add(Me.cmdSconto)
    Me.pnListDx.Controls.Add(Me.lbtipolist)
    Me.pnListDx.Controls.Add(Me.cbTipolistino)
    Me.pnListDx.Controls.Add(Me.fmValuta)
    Me.pnListDx.Controls.Add(Me.ckPromo)
    Me.pnListDx.Controls.Add(Me.fmValidita)
    Me.pnListDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnListDx.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnListDx.Location = New System.Drawing.Point(306, 0)
    Me.pnListDx.Name = "pnListDx"
    Me.pnListDx.NTSActiveTrasparency = True
    Me.pnListDx.Size = New System.Drawing.Size(200, 295)
    Me.pnListDx.TabIndex = 8
    Me.pnListDx.Text = "NtsPanel1"
    '
    'cmdSconto
    '
    Me.cmdSconto.ImageText = ""
    Me.cmdSconto.Location = New System.Drawing.Point(7, 263)
    Me.cmdSconto.Name = "cmdSconto"
    Me.cmdSconto.NTSContextMenu = Nothing
    Me.cmdSconto.Size = New System.Drawing.Size(186, 26)
    Me.cmdSconto.TabIndex = 506
    Me.cmdSconto.Text = "Vai allo sconto collegato"
    '
    'lbtipolist
    '
    Me.lbtipolist.AutoSize = True
    Me.lbtipolist.BackColor = System.Drawing.Color.Transparent
    Me.lbtipolist.Location = New System.Drawing.Point(7, 152)
    Me.lbtipolist.Name = "lbtipolist"
    Me.lbtipolist.NTSDbField = ""
    Me.lbtipolist.Size = New System.Drawing.Size(57, 13)
    Me.lbtipolist.TabIndex = 4
    Me.lbtipolist.Text = "Tipo listino"
    Me.lbtipolist.Tooltip = ""
    Me.lbtipolist.UseMnemonic = False
    '
    'cbTipolistino
    '
    Me.cbTipolistino.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipolistino.DataSource = Nothing
    Me.cbTipolistino.DisplayMember = ""
    Me.cbTipolistino.Location = New System.Drawing.Point(65, 149)
    Me.cbTipolistino.Name = "cbTipolistino"
    Me.cbTipolistino.NTSDbField = ""
    Me.cbTipolistino.Properties.AutoHeight = False
    Me.cbTipolistino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipolistino.Properties.DropDownRows = 30
    Me.cbTipolistino.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipolistino.SelectedValue = ""
    Me.cbTipolistino.Size = New System.Drawing.Size(121, 20)
    Me.cbTipolistino.TabIndex = 3
    Me.cbTipolistino.ValueMember = ""
    '
    'fmValuta
    '
    Me.fmValuta.AllowDrop = True
    Me.fmValuta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValuta.Appearance.Options.UseBackColor = True
    Me.fmValuta.Controls.Add(Me.lbCodvalu)
    Me.fmValuta.Controls.Add(Me.edCodvalu)
    Me.fmValuta.Controls.Add(Me.opValAltro)
    Me.fmValuta.Controls.Add(Me.opValEur)
    Me.fmValuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValuta.Location = New System.Drawing.Point(7, 188)
    Me.fmValuta.Name = "fmValuta"
    Me.fmValuta.Size = New System.Drawing.Size(186, 69)
    Me.fmValuta.TabIndex = 2
    Me.fmValuta.Text = "Valuta"
    '
    'lbCodvalu
    '
    Me.lbCodvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbCodvalu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodvalu.Location = New System.Drawing.Point(99, 44)
    Me.lbCodvalu.Name = "lbCodvalu"
    Me.lbCodvalu.NTSDbField = ""
    Me.lbCodvalu.Size = New System.Drawing.Size(80, 20)
    Me.lbCodvalu.TabIndex = 3
    Me.lbCodvalu.Tooltip = ""
    Me.lbCodvalu.UseMnemonic = False
    '
    'edCodvalu
    '
    Me.edCodvalu.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodvalu.EditValue = "0"
    Me.edCodvalu.Location = New System.Drawing.Point(58, 44)
    Me.edCodvalu.Name = "edCodvalu"
    Me.edCodvalu.NTSDbField = ""
    Me.edCodvalu.NTSFormat = "0"
    Me.edCodvalu.NTSForzaVisZoom = False
    Me.edCodvalu.NTSOldValue = ""
    Me.edCodvalu.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu.Properties.AutoHeight = False
    Me.edCodvalu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu.Properties.MaxLength = 65536
    Me.edCodvalu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu.Size = New System.Drawing.Size(41, 20)
    Me.edCodvalu.TabIndex = 3
    Me.edCodvalu.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu.TextInt = 0
    '
    'opValAltro
    '
    Me.opValAltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValAltro.Location = New System.Drawing.Point(5, 45)
    Me.opValAltro.Name = "opValAltro"
    Me.opValAltro.NTSCheckValue = "S"
    Me.opValAltro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValAltro.Properties.Appearance.Options.UseBackColor = True
    Me.opValAltro.Properties.AutoHeight = False
    Me.opValAltro.Properties.Caption = "Altro"
    Me.opValAltro.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValAltro.Size = New System.Drawing.Size(56, 19)
    Me.opValAltro.TabIndex = 2
    '
    'opValEur
    '
    Me.opValEur.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValEur.EditValue = True
    Me.opValEur.Location = New System.Drawing.Point(5, 23)
    Me.opValEur.Name = "opValEur"
    Me.opValEur.NTSCheckValue = "S"
    Me.opValEur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValEur.Properties.Appearance.Options.UseBackColor = True
    Me.opValEur.Properties.AutoHeight = False
    Me.opValEur.Properties.Caption = "Euro"
    Me.opValEur.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValEur.Size = New System.Drawing.Size(48, 19)
    Me.opValEur.TabIndex = 1
    '
    'ckPromo
    '
    Me.ckPromo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckPromo.Location = New System.Drawing.Point(9, 120)
    Me.ckPromo.Name = "ckPromo"
    Me.ckPromo.NTSCheckValue = "S"
    Me.ckPromo.NTSUnCheckValue = "N"
    Me.ckPromo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPromo.Properties.Appearance.Options.UseBackColor = True
    Me.ckPromo.Properties.AutoHeight = False
    Me.ckPromo.Properties.Caption = "Prezzi in promozione"
    Me.ckPromo.Size = New System.Drawing.Size(129, 19)
    Me.ckPromo.TabIndex = 1
    '
    'fmValidita
    '
    Me.fmValidita.AllowDrop = True
    Me.fmValidita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValidita.Appearance.Options.UseBackColor = True
    Me.fmValidita.Controls.Add(Me.edDtval)
    Me.fmValidita.Controls.Add(Me.opValDay)
    Me.fmValidita.Controls.Add(Me.opValTutti)
    Me.fmValidita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValidita.Location = New System.Drawing.Point(7, 38)
    Me.fmValidita.Name = "fmValidita"
    Me.fmValidita.Size = New System.Drawing.Size(186, 70)
    Me.fmValidita.TabIndex = 0
    Me.fmValidita.Text = "Validità"
    '
    'edDtval
    '
    Me.edDtval.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtval.EditValue = "15/01/2008"
    Me.edDtval.Location = New System.Drawing.Point(67, 46)
    Me.edDtval.Name = "edDtval"
    Me.edDtval.NTSDbField = ""
    Me.edDtval.NTSForzaVisZoom = False
    Me.edDtval.NTSOldValue = ""
    Me.edDtval.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtval.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtval.Properties.AutoHeight = False
    Me.edDtval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtval.Properties.MaxLength = 65536
    Me.edDtval.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtval.Size = New System.Drawing.Size(112, 20)
    Me.edDtval.TabIndex = 2
    Me.edDtval.TextData = New Date(2008, 1, 15, 0, 0, 0, 0)
    '
    'opValDay
    '
    Me.opValDay.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValDay.EditValue = True
    Me.opValDay.Location = New System.Drawing.Point(5, 46)
    Me.opValDay.Name = "opValDay"
    Me.opValDay.NTSCheckValue = "S"
    Me.opValDay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValDay.Properties.Appearance.Options.UseBackColor = True
    Me.opValDay.Properties.AutoHeight = False
    Me.opValDay.Properties.Caption = "Valido il"
    Me.opValDay.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValDay.Size = New System.Drawing.Size(56, 19)
    Me.opValDay.TabIndex = 1
    '
    'opValTutti
    '
    Me.opValTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValTutti.Location = New System.Drawing.Point(5, 23)
    Me.opValTutti.Name = "opValTutti"
    Me.opValTutti.NTSCheckValue = "S"
    Me.opValTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opValTutti.Properties.AutoHeight = False
    Me.opValTutti.Properties.Caption = "Tutti"
    Me.opValTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValTutti.Size = New System.Drawing.Size(48, 19)
    Me.opValTutti.TabIndex = 0
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me.pnListDx
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbListNuovo, Me.tlbListSalva, Me.tlbListRipristina, Me.tlbListCancella, Me.tlbListZoom, Me.tlbListEsci})
    Me.NtsBarManager1.MaxItemId = 8
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbListEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.MultiLine = True
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbListNuovo
    '
    Me.tlbListNuovo.Caption = "Nuovo"
    Me.tlbListNuovo.Glyph = CType(resources.GetObject("tlbListNuovo.Glyph"), System.Drawing.Image)
    Me.tlbListNuovo.Id = 1
    Me.tlbListNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbListNuovo.Name = "tlbListNuovo"
    Me.tlbListNuovo.Visible = True
    '
    'tlbListSalva
    '
    Me.tlbListSalva.Caption = "Salva"
    Me.tlbListSalva.Glyph = CType(resources.GetObject("tlbListSalva.Glyph"), System.Drawing.Image)
    Me.tlbListSalva.Id = 2
    Me.tlbListSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbListSalva.Name = "tlbListSalva"
    Me.tlbListSalva.Visible = True
    '
    'tlbListRipristina
    '
    Me.tlbListRipristina.Caption = "Ripristina"
    Me.tlbListRipristina.Glyph = CType(resources.GetObject("tlbListRipristina.Glyph"), System.Drawing.Image)
    Me.tlbListRipristina.Id = 3
    Me.tlbListRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbListRipristina.Name = "tlbListRipristina"
    Me.tlbListRipristina.Visible = True
    '
    'tlbListCancella
    '
    Me.tlbListCancella.Caption = "Cancella"
    Me.tlbListCancella.Glyph = CType(resources.GetObject("tlbListCancella.Glyph"), System.Drawing.Image)
    Me.tlbListCancella.Id = 4
    Me.tlbListCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbListCancella.Name = "tlbListCancella"
    Me.tlbListCancella.Visible = True
    '
    'tlbListZoom
    '
    Me.tlbListZoom.Caption = "Zoom"
    Me.tlbListZoom.Glyph = CType(resources.GetObject("tlbListZoom.Glyph"), System.Drawing.Image)
    Me.tlbListZoom.Id = 5
    Me.tlbListZoom.Name = "tlbListZoom"
    Me.tlbListZoom.Visible = True
    '
    'tlbListEsci
    '
    Me.tlbListEsci.Caption = "Esci"
    Me.tlbListEsci.Glyph = CType(resources.GetObject("tlbListEsci.Glyph"), System.Drawing.Image)
    Me.tlbListEsci.Id = 6
    Me.tlbListEsci.Name = "tlbListEsci"
    Me.tlbListEsci.Visible = True
    '
    'NTSXXLIST
    '
    Me.Controls.Add(Me.pnListini)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "NTSXXLIST"
    Me.Size = New System.Drawing.Size(506, 325)
    CType(Me.pnListini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnListini.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnListDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnListDx.ResumeLayout(False)
    Me.pnListDx.PerformLayout()
    CType(Me.cbTipolistino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValuta.ResumeLayout(False)
    Me.fmValuta.PerformLayout()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValAltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValEur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPromo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValidita.ResumeLayout(False)
    Me.fmValidita.PerformLayout()
    CType(Me.edDtval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Sub New()
    InitializeComponent()
    Me.MinimumSize = Me.Size
  End Sub

  Public Overloads Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim oCtrl As Control = Nothing
    Try
      'per ora giro i messaggi al chiamante
      oParent.GestisciEventiEntity(sender, e)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    oParent.InitControlsBeginEndInit(Me, False)
    Try

      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbListNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbListSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbListCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbListRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbListZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbListEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      'tlbMain.NTSSetToolTip()       'non so perchè, ma nel controllo personalizzato se lo metto qui non lavora

      Dim dttTipoListino As New DataTable()
      dttTipoListino.Columns.Add("cod", GetType(String))
      dttTipoListino.Columns.Add("val", GetType(String))
      dttTipoListino.Rows.Add(New Object() {" ", "Generico"})
      dttTipoListino.Rows.Add(New Object() {"C", "Specifico cliente"})
      If oCleList.strChildParent <> "BNCPIMCO" Then
        dttTipoListino.Rows.Add(New Object() {"F", "Specifico fornitore"})
      End If
      dttTipoListino.AcceptChanges()
      cbTipolistino.DataSource = dttTipoListino
      cbTipolistino.ValueMember = "cod"
      cbTipolistino.DisplayMember = "val"

      grvList.NTSSetParam(oMenu, oApp.Tr(Me, 128448857602376000, "Griglia listini"))
      cbTipolistino.NTSSetParam(oApp.Tr(Me, 128448891663990000, "Tipo listino"))
      edCodvalu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128448891664302000, "Valuta"), tabvalu)
      opValAltro.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664458000, "Altro"), "A")
      opValEur.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664614000, "Euro"), "E")
      ckPromo.NTSSetParam(oMenu, oApp.Tr(Me, 128802055933292720, "Prezzi in promozione"), "S", "N")
      edDtval.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664770000, "Data validità listino"), False)
      opValDay.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664926000, "Valido il"), "D")
      opValTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128448891665082000, "Tutti"), "T")

      lc_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128802056783303600, "Cod. articolo"), tabartico, False)
      xx_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056054700524, "Descr. articolo"), 0, True)
      lc_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056090169728, "Fase"), tabartfasi)
      lc_fase.ArtiPerFase = lc_codart
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056104701164, "Descr. fase"), 0, True)
      lc_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056161108136, "Cod.lav."), tablavo)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056181108392, "Descr.lav."), 0, True)
      lc_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056194702316, "Cliente/forn."), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056209390004, "Descrizione"), 0, True)
      lc_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130584381926511480, "Destinazione diversa"), tabdestdiv)
      lc_coddest.CliePerDestdiv = lc_conto
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130584381905755382, "Descrizione destinazione diversa"), 0, True)
      lc_tipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056220796400, "Tipo"), 1, False)
      lc_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056234702828, "Valuta"), tabvalu)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056247515492, "Descr.valuta"), 0, True)
      lc_listino.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056260796912, "Listino"), tablist)
      lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056295641108, "Prezzo"), oApp.FormatPrzUn, 15)
      lc_daquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056306891252, "Da quantità"), oApp.FormatQta, 15)
      lc_aquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056320016420, "A quantità"), oApp.FormatQta, 15)
      lc_datagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128802056350329308, "Data aggiorn."), False, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lc_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128802056362048208, "Data scad."), True, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lc_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128802056377673408, "Cod. promoz."), tabtpro)
      xx_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056391267332, "Descr. promoz."), 0, True)
      xx_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056405173760, "Cod.art.cli/for"), 0, True)
      lc_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056417517668, "Q.tà prezzo"), "#,##0", 15)
      lc_netto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128802056464237016, "Prezzo netto"), "S", "N")
      lc_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056507206316, "Note"), 255, True, True)
      xx_listino.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056535175424, "Descr. listino"), 0, True)
      xx_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802056553300656, "Descr. interna art."), 0, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    oParent.InitControlsBeginEndInit(Me, True)
  End Sub


#Region "NTSSetparam e Parametetri aggiuntivi di init"
  Public Sub NTSSetParam(ByRef Menu As CLE__MENU, ByVal NomeCampo As String, ByVal ChildChiamante As String, ByRef Param As CLE__CLDP)
    Dim strCurrent, strUnmis() As String
    Try
      '------------------------------------------------
      oMenu = Menu
      oApp = oMenu.oApp
      strNomeCampo = NomeCampo
      oCallParams = Param
      oParent = CType(Me.FindForm, FRM__CHIL)

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNXXLIST", "BEXXLIST", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
        Return
      End If
      oCleList = CType(oTmp, CLEXXLIST)
      '------------------------------------------------
      oParent.bRemoting = Menu.Remoting("BNXXLIST", oParent.strRemoteServer, oParent.strRemotePort)
      AddHandler oCleList.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleList.Init(oApp, oParent.NTSScript, oMenu.oCleComm, "LISTINI", oParent.bRemoting, oParent.strRemoteServer, oParent.strRemotePort) = False Then Return

      '------------------------------------------------
      'predispongo i controlli
      oCleList.strChildParent = ChildChiamante

      InitControls()

      dttUm.Columns.Add("tb_codumis")

      ' Inizializzo qui la colonna unità di misura per avere l'articolo caricato e attivare solo le sue unità di misura
      strUnmis = oCleList.GetArticoUnMis("D").Split("§"c)     'con l'articolo "D" vengono prese tutte le unità di misura ...
      dttUm.Clear()
      For Each strCurrent In strUnmis
        dttUm.Rows.Add()
        dttUm.Rows(dttUm.Rows.Count - 1)!tb_codumis = strCurrent
      Next
      dttUm.AcceptChanges()

      lc_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128708025253792763, "U.M."), dttUm, "tb_codumis", "tb_codumis")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Property LcTipo() As String
    Get
      If strNomeCampo = "" Then Return "" 'per il design
      If oCleList Is Nothing Then Return " "
      Return oCleList.strTipork
    End Get
    Set(ByVal value As String)
      If strNomeCampo = "" Then Return 'per il design
      If oCleList Is Nothing Then Return
      oCleList.strTipork = value
      ImpostaColonneGriglia()
    End Set
  End Property

  Public Property LcConto() As Integer
    Get
      If strNomeCampo = "" Then Return 0 'per il design
      Return oCleList.lConto
    End Get
    Set(ByVal value As Integer)
      If oCleList Is Nothing Then Return
      oCleList.lConto = value

      'al cambio del conto reimposto il default del controllo (non lo faccio se passo direttamente da un cliente ad un altro - tipo selezione multipla clienti)
      If value = 0 Then
        ckPromo.Checked = False
        opValDay.Checked = True
        edDtval.Text = DateTime.Now.ToShortDateString
        If Not CBool(oMenu.GetSettingBus("BSXXLIST", "RECENT", ".", "ValutaAttiva", "0", ".", "0")) Then
          opValEur.Checked = True
        Else
          opValAltro.Checked = True
        End If
        If opValAltro.Checked Then
          edCodvalu.Text = oMenu.GetSettingBus("BSXXLIST", "RECENT", ".", "CodiceValutaAttiva", "0", ".", "0")
        Else
          edCodvalu.Text = "0"
        End If
      End If
    End Set
  End Property

  Public Property LcCodart() As String
    Get
      If strNomeCampo = "" Then Return "" 'per il design
      Return oCleList.strCodart
    End Get
    Set(ByVal value As String)
      If oCleList Is Nothing Then Return
      oCleList.strCodart = value

      'al cambio dell'articolo reimposto il default del controllo (non lo faccio se passo direttamente da un articolo ad un altro - tipo selezione multipla articoli)
      If oCleList.strCodart = "" Then
        ckPromo.Checked = False
        opValDay.Checked = True
        edDtval.Text = DateTime.Now.ToShortDateString
        If Not CBool(oMenu.GetSettingBus("BSXXLIST", "RECENT", ".", "ValutaAttiva", "0", ".", "0")) Then
          opValEur.Checked = True
        Else
          opValAltro.Checked = True
        End If
        If opValAltro.Checked Then
          edCodvalu.Text = oMenu.GetSettingBus("BSXXLIST", "RECENT", ".", "CodiceValutaAttiva", "0", ".", "0")
        Else
          edCodvalu.Text = "0"
        End If
      End If
    End Set
  End Property

  Public Property LcCodartRoot() As String
    Get
      If strNomeCampo = "" Then Return "" 'per il design
      Return oCleList.strCodartRoot
    End Get
    Set(ByVal value As String)
      If oCleList Is Nothing Then Return
      oCleList.strCodartRoot = value
    End Set
  End Property

  Public Property LcFaseArticolo() As Integer
    Get
      If strNomeCampo = "" Then Return 0 'per il design
      Return oCleList.nFase
    End Get
    Set(ByVal value As Integer)
      If oCleList Is Nothing Then Return
      oCleList.nFase = value
    End Set
  End Property
#End Region


  Public Overridable Function ApriListini() As Boolean
    Try
      tlbMain.NTSSetToolTip()
      If tlbListEsci.Enabled = False Then tlbListEsci.Visible = False

      '-------------------------------------------------
      'svuoto eventualmente i listini caricati in precedenza
      If dsList.Tables.Count > 0 Then
        dsList.Tables.Clear()
        grList.DataSource = Nothing
        dsList.AcceptChanges()
      End If

      Me.Cursor = Cursors.WaitCursor

      Select Case oCleList.strChildParent
        Case "BNMGARTI", "BNMGARTV", "BNTCARTV"
          If oCleList.strCodart <> "" Then oCleList.strTipork = cbTipolistino.SelectedValue
      End Select

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleList.Apri(oParent.DittaCorrente, Not opValAltro.Checked, _
                           NTSCInt(IIf(opValEur.Checked, 0, NTSCInt(edCodvalu.Text))), ckPromo.Checked, _
                           opValTutti.Checked, edDtval.Text, dsList, xx_codarfo.Visible) Then Return False
      dcList.DataSource = dsList.Tables("LISTINI")
      dsList.AcceptChanges()

      If ckPromo.Checked Then
        oParent.GctlSetVisEnab(lc_codtpro, True)
        oParent.GctlSetVisEnab(xx_codtpro, True)
      Else
        lc_codtpro.Visible = False
        xx_codtpro.Visible = False
      End If

      grList.DataSource = dcList
      grvList.NTSMoveFirstRowColunn()

      SettaVisibilitaColonne()

      If CLN__STD.FRIENDLY Then
        oParent.GctlSetVisEnab(lc_daquant, True)
        oParent.GctlSetVisEnab(lc_aquant, True)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function ApriListiniConfiguratore() As Boolean
    Dim strFiltro As String = ""
    Dim dtrRow() As DataRow
    Try

      tlbMain.NTSSetToolTip()
      If tlbListEsci.Enabled = False Then tlbListEsci.Visible = False

      '-------------------------------------------------
      'svuoto eventualmente i listini caricati in precedenza
      If dsList.Tables.Count > 0 Then
        dsList.Tables.Clear()
        grList.DataSource = Nothing
        dsList.AcceptChanges()
      End If

      Me.Cursor = Cursors.WaitCursor

      oCleList.nCount = 1
      dtrRow = dsListConfiguratore.Tables("LISTINI").Select("")
      For Each Row As DataRow In dtrRow
        Row!xx_progr = oCleList.nCount
        oCleList.nCount += 1
      Next

      oCleList.dsListConf = dsListConfiguratore
      dsList.Tables.Add(dsListConfiguratore.Tables("LISTINI").Clone)
      CostruisciFiltro(strFiltro)
      dtrRow = dsListConfiguratore.Tables("LISTINI").Select(strFiltro, "lc_codlavo, lc_conto, lc_listino, lc_codvalu, lc_codtpro, lc_datagg DESC, lc_daquant")
      For Each Row As DataRow In dtrRow
        dsList.Tables("LISTINI").ImportRow(Row)
      Next

      If Not oCleList.ApriListiniConf(oParent.DittaCorrente, LcCodart, strCodcas, NTSCStr(IIf(cbTipolistino.SelectedValue = "C", "J", "K")), _
                                      Not opValAltro.Checked, NTSCInt(IIf(opValEur.Checked, 0, NTSCInt(edCodvalu.Text))), ckPromo.Checked, _
                                      dsList) Then Return False

      dcList.DataSource = dsList.Tables("LISTINI")
      dsList.AcceptChanges()

      If ckPromo.Checked Then
        oParent.GctlSetVisEnab(lc_codtpro, True)
        oParent.GctlSetVisEnab(xx_codtpro, True)
      Else
        lc_codtpro.Visible = False
        xx_codtpro.Visible = False
      End If

      grList.DataSource = dcList
      grvList.NTSMoveFirstRowColunn()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub CostruisciFiltro(ByRef strFiltro As String)
    Try
      Dim bNoTestCodvalu As Boolean
      Dim nCodvalu As Integer
      Dim bInpromo As Boolean
      Dim bNoTestValidoil As Boolean
      Dim strValidoil As String

      bNoTestCodvalu = Not opValAltro.Checked
      nCodvalu = NTSCInt(IIf(opValEur.Checked, 0, NTSCInt(edCodvalu.Text)))
      bInpromo = ckPromo.Checked
      bNoTestValidoil = opValTutti.Checked
      strValidoil = edDtval.Text

      strFiltro += " lc_codcas = " & CStrSQL(strCodcas)

      Select Case cbTipolistino.SelectedValue
        Case "C"
          strFiltro += " AND lc_tipo = 'J'"
        Case Else
          strFiltro += " AND lc_tipo = 'K'"
      End Select

      If bNoTestCodvalu = False Then
        If nCodvalu = 0 Then
          strFiltro += " AND lc_codvalu <> 0"
        Else
          strFiltro += " AND lc_codvalu = " & nCodvalu.ToString
        End If
      Else
        strFiltro += " AND lc_codvalu = 0"
      End If

      If bNoTestValidoil = False Then
        strFiltro += " AND lc_datagg <= " & CDataSQL(strValidoil) & " AND lc_datscad >= " & CDataSQL(strValidoil)
      End If

      If bInpromo Then
        strFiltro += " AND lc_codtpro > 0 "
      Else
        strFiltro += " AND lc_codtpro = 0 "
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ImpostaColonneGriglia() As Boolean
    Try
      If oCleList.strTipork <> cbTipolistino.SelectedValue Or cbTipolistino.SelectedValue Is Nothing Then
        cbTipolistino.SelectedValue = oCleList.strTipork
      Else
        cbTipolistino_SelectedIndexChanged(cbTipolistino, Nothing)
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public ReadOnly Property NTSVisibleToUser() As Boolean
    'true se il controllo è visibile all'utente (potrebbe avere la proprietà visible = true 
    'ma essere in una pagina di tabstrip non visibile)
    Get
      Return True
    End Get
  End Property


#Region "Nuovo/salva/cancella/ripristina"
  Public Overridable Sub Nuovo()
    Try
      grvList.NTSNuovo()
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
      oParent.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      'non faccio chiedere se salvare o no: deve averci pensato la form ...
      dRes = grvList.NTSSalvaRigaCorrente(dcList, oCleList.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If oParent.GctlControllaOutNotEqual() = False Then Return False

          Me.Cursor = Cursors.WaitCursor
          If Not oCleList.Salva(False) Then
            Return False
          Else
            'se sto visualizzando solo i listini in vigore ad oggi,
            'ho inserito un listino con data validità oggi che ha fatto scadere uno che era in griglia
            'il dal ha già corretto la data di scadenza del listino vecchio: ora devo solo toglierlo dalla 
            'griglia
            If opValDay.Checked Then
              'lc_datagg < " & CDataSQL(NTSCDate(edDtval.Text).AddDays(1) -> AddDays serve perchè lc_datagg ha anche l'ora, mentre edDtVal no. 
              'I listini inseriti con la stessa data del filtro sparivano appena inseriti
              For Each dtrT As DataRow In dsList.Tables("LISTINI").Select("NOT (lc_datagg < " & CDataSQL(NTSCDate(edDtval.Text).AddDays(1)) & " AND lc_datscad >= " & CDataSQL(NTSCDate(edDtval.Text)) & ")")
                dtrT.Delete()
              Next
              dsList.Tables("LISTINI").AcceptChanges()
            End If
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleList.Ripristina(dcList.Position, dcList.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select

      oParent.GctlSetVisEnab(opValTutti, False)
      oParent.GctlSetVisEnab(opValDay, False)
      oParent.GctlSetVisEnab(edDtval, False)
      oParent.GctlSetVisEnab(ckPromo, False)
      oParent.GctlSetVisEnab(cbTipolistino, False)
      oParent.GctlSetVisEnab(opValEur, False)
      oParent.GctlSetVisEnab(opValAltro, False)
      oParent.GctlSetVisEnab(edCodvalu, False)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Cancella()
    Try
      If Not grvList.NTSDeleteRigaCorrente(dcList, False) Then Return
      oCleList.Salva(True)


      oParent.GctlSetVisEnab(opValTutti, False)
      oParent.GctlSetVisEnab(opValDay, False)
      oParent.GctlSetVisEnab(edDtval, False)
      oParent.GctlSetVisEnab(ckPromo, False)
      oParent.GctlSetVisEnab(cbTipolistino, False)
      oParent.GctlSetVisEnab(opValEur, False)
      oParent.GctlSetVisEnab(opValAltro, False)
      oParent.GctlSetVisEnab(edCodvalu, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Ripristina()
    Try
      If Not grvList.NTSRipristinaRigaCorrenteBefore(dcList, False) Then Return
      oCleList.Ripristina(dcList.Position, dcList.Filter)
      grvList.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


#Region "Per far ricaricare i listini al cambio di data validità/promozioni/valuta/tipo/..."
  Public Overridable Sub cbTipolistino_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipolistino.SelectedIndexChanged
    '-------------------------------------------------
    'imposto la visibilità delle colonne standard, dopo verrà applicata la GctlSetroules dal programma chiamante
    Try
      cmdSconto.Visible = False

      If oCleList Is Nothing Then Return

      Salva()

      'C = griglia per clienti
      'F = griglia per fornitori
      'L = griglia per listino generico
      Select Case oCleList.strChildParent
        Case "BN__CLIE"
          oParent.GctlSetVisEnab(lc_codart, True)
          oParent.GctlSetVisEnab(xx_codart, True)
          lc_conto.Visible = False
          xx_conto.Visible = False
          If CLN__STD.FRIENDLY Then
            lc_coddest.Visible = False
            xx_coddest.Visible = False
          Else
            oParent.GctlSetVisEnab(lc_coddest, True)
            oParent.GctlSetVisEnab(xx_coddest, True)
          End If
          lc_listino.Visible = False
          lbtipolist.Visible = False
          cbTipolistino.Visible = False

          oParent.GctlSetVisEnab(cmdSconto, True)

          If oCleList.lConto <> 0 Then ApriListini()

        Case "BNMGARTI", "BNMGARTV", "BNTCARTV"
          Select Case cbTipolistino.SelectedValue
            Case "C", "F"
              'listino specifico cliente/fornitore
              lc_codart.Visible = False
              xx_codart.Visible = False
              oParent.GctlSetVisEnab(lc_conto, True)
              oParent.GctlSetVisEnab(xx_conto, True)
              If CLN__STD.FRIENDLY Then
                lc_coddest.Visible = False
                xx_coddest.Visible = False
              Else
                oParent.GctlSetVisEnab(lc_coddest, True)
                oParent.GctlSetVisEnab(xx_coddest, True)
              End If
              lc_listino.Visible = False
              xx_listino.Visible = False

              oParent.GctlSetVisEnab(cmdSconto, True)
            Case " "
              'numero di listino
              lc_codart.Visible = False
              xx_codart.Visible = False
              lc_conto.Visible = False
              xx_conto.Visible = False
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              oParent.GctlSetVisEnab(lc_listino, True)
              oParent.GctlSetVisEnab(xx_listino, True)

          End Select

          If oCleList.strCodart <> "" Then
            oCleList.strTipork = cbTipolistino.SelectedValue
            ApriListini()
          End If

        Case "BNMGHLAR", "BNVEBOLX"
          oParent.GctlSetVisEnab(lc_conto, True)
          oParent.GctlSetVisEnab(xx_conto, True)
          If CLN__STD.FRIENDLY Then
            lc_coddest.Visible = False
            xx_coddest.Visible = False
          Else
            oParent.GctlSetVisEnab(lc_coddest, True)
            oParent.GctlSetVisEnab(xx_coddest, True)
          End If
          oParent.GctlSetVisEnab(lc_listino, True)
          oParent.GctlSetVisEnab(xx_listino, True)
          lc_codart.Visible = False
          xx_codart.Visible = False
          lbtipolist.Visible = False
          cbTipolistino.Visible = False

        Case "BNCPIMCO"
          Select Case cbTipolistino.SelectedValue
            Case "C", "F"
              'listino specifico cliente/fornitore
              lc_codart.Visible = False
              xx_codart.Visible = False
              oParent.GctlSetVisEnab(lc_conto, True)
              oParent.GctlSetVisEnab(xx_conto, True)
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              lc_listino.Visible = False
              xx_listino.Visible = False

            Case " "
              'numero di listino
              lc_codart.Visible = False
              xx_codart.Visible = False
              lc_conto.Visible = False
              xx_conto.Visible = False
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              oParent.GctlSetVisEnab(lc_listino, True)
              oParent.GctlSetVisEnab(xx_listino, True)

          End Select

          If strCodcas <> "" Then
            oCleList.strTipork = NTSCStr(IIf(cbTipolistino.SelectedValue = "C", "J", "K"))
            ApriListiniConfiguratore()
          End If

      End Select    'Select Case strChildParent


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckPromo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPromo.CheckedChanged
    Try
      If oCleList Is Nothing Then Return

      If oCleList.strChildParent = "BN__CLIE" And oCleList.lConto <> 0 Then ApriListini()
      If oCleList.strChildParent <> "" And oCleList.strChildParent <> "BN__CLIE" And oCleList.strChildParent <> "BNCPIMCO" And oCleList.strCodart <> "" Then ApriListini()
      If oCleList.strChildParent = "BNCPIMCO" And strCodcas <> "" Then ApriListiniConfiguratore()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opValDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValDay.CheckedChanged
    Try
      If opValDay.Checked Then
        oParent.GctlSetVisEnab(edDtval, False)
      Else
        edDtval.Enabled = False
      End If
      If oCleList Is Nothing Then Return
      If oCleList.strChildParent = "BN__CLIE" And oCleList.lConto <> 0 Then ApriListini()
      If oCleList.strChildParent <> "" And oCleList.strChildParent <> "BN__CLIE" And oCleList.strChildParent <> "BNCPIMCO" And oCleList.strCodart <> "" Then ApriListini()
      If oCleList.strChildParent = "BNCPIMCO" And strCodcas <> "" Then ApriListiniConfiguratore()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opValAltro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValAltro.CheckedChanged
    Try
      If opValAltro.Checked Then
        oParent.GctlSetVisEnab(edCodvalu, False)
        oParent.GctlSetVisEnab(lc_codvalu, True)
        oParent.GctlSetVisEnab(xx_codvalu, True)
        lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056738303024, "Prezzo"), oApp.FormatPrzUnVal, 15)
        oMenu.SaveSettingBus("BSXXLIST", "RECENT", ".", "ValutaAttiva", "-1", ".", "SS.", "SS.", "NNN")
        'oMenu.SaveSettingBus("BSXXLIST", "RECENT", ".", "CodiceValutaAttiva", edCodvalu.Text, ".", "NNN", "NS.", "NNN")
      Else
        edCodvalu.Enabled = False
        lc_codvalu.Visible = False
        xx_codvalu.Visible = False
        lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802056820491576, "Prezzo"), oApp.FormatPrzUn, 15)
        oMenu.SaveSettingBus("BSXXLIST", "RECENT", ".", "ValutaAttiva", "0", ".", "SS.", "SS.", "NNN")
        'oMenu.SaveSettingBus("BSXXLIST", "RECENT", ".", "CodiceValutaAttiva", "0", ".", "NNN", "NS.", "NNN")
      End If
      If oCleList Is Nothing Then Return
      If oCleList.strChildParent = "BN__CLIE" And oCleList.lConto <> 0 Then ApriListini()
      If oCleList.strChildParent <> "" And oCleList.strChildParent <> "BN__CLIE" And oCleList.strChildParent <> "BNCPIMCO" And oCleList.strCodart <> "" Then ApriListini()
      If oCleList.strChildParent = "BNCPIMCO" And strCodcas <> "" Then ApriListiniConfiguratore()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub edDtval_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDtval.Validated
    Try
      If edDtval.Text = edDtval.OldEditValue.ToString Then Return

      If oCleList Is Nothing Then Return
      If oCleList.strChildParent = "BN__CLIE" And oCleList.lConto <> 0 Then ApriListini()
      If oCleList.strChildParent <> "" And oCleList.strChildParent <> "BN__CLIE" And oCleList.strChildParent <> "BNCPIMCO" And oCleList.strCodart <> "" Then ApriListini()
      If oCleList.strChildParent = "BNCPIMCO" And strCodcas <> "" Then ApriListiniConfiguratore()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCodvalu_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu.Validated
    Dim strTmp As String = ""
    Try
      If edCodvalu.Text = edCodvalu.OldEditValue.ToString Then Return
      If oCleList Is Nothing Then Return

      If Not oCleList.edCodvalu_Validated(NTSCInt(edCodvalu.Text), strTmp) Then
        edCodvalu.Text = "0"
        lbCodvalu.Text = ""
        Return
      Else
        lbCodvalu.Text = strTmp
      End If

      If oCleList.strChildParent = "BN__CLIE" And oCleList.lConto <> 0 Then ApriListini()
      If oCleList.strChildParent <> "" And oCleList.strChildParent <> "BN__CLIE" And oCleList.strChildParent <> "BNCPIMCO" And oCleList.strCodart <> "" Then ApriListini()
      If oCleList.strChildParent = "BNCPIMCO" And strCodcas <> "" Then ApriListiniConfiguratore()

      oMenu.SaveSettingBus("BSXXLIST", "RECENT", ".", "CodiceValutaAttiva", edCodvalu.Text, ".", "SS.", "SS.", "NNN")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbListNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListNuovo.ItemClick
    Try
      If NTSVisibleToUser = False Then Return
      grvList.NTSNuovo()
      grvList.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbListSalva_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListSalva.ItemClick
    Try
      If NTSVisibleToUser = False Then Return

      Salva()
      grvList.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbListRipristina_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListRipristina.ItemClick
    Try
      If NTSVisibleToUser = False Then Return

      If Not grvList.NTSRipristinaRigaCorrenteBefore(dcList, True) Then Return
      oCleList.Ripristina(dcList.Position, dcList.Filter)
      grvList.NTSRipristinaRigaCorrenteAfter()
      grvList.Focus()

      oParent.GctlSetVisEnab(opValTutti, False)
      oParent.GctlSetVisEnab(opValDay, False)
      oParent.GctlSetVisEnab(edDtval, False)
      oParent.GctlSetVisEnab(ckPromo, False)
      oParent.GctlSetVisEnab(cbTipolistino, False)
      oParent.GctlSetVisEnab(opValEur, False)
      oParent.GctlSetVisEnab(opValAltro, False)
      oParent.GctlSetVisEnab(edCodvalu, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbListCancella_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListCancella.ItemClick
    Try
      If NTSVisibleToUser = False Then Return

      If Not grvList.NTSDeleteRigaCorrente(dcList, True) Then Return
      oCleList.Salva(True)
      grvList.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbListZoom_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListZoom.ItemClick
    'SendKeys.SendWait("{F5}")
    'se faccio la riga sopra va in un loop infinito....
    'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
    Dim oParam As New CLE__PATB
    Dim oTmp As Object = Nothing

    If NTSVisibleToUser = False Then Return

    Try
      If grvList.FocusedColumn.Name = "lc_conto" Then
        '--------------------------------------------
        'zoom anagra
        If oCleList.strTipork = "C" Or oCleList.strTipork = "F" Then
          oParent.SetFastZoom(NTSCStr(grvList.EditingValue), oParam)    'abilito la gestione dello zoom veloce
          oParent.NTSZOOM.strIn = NTSCStr(grvList.EditingValue)
          oParam.bVisGriglia = True
          oParam.strTipo = oCleList.strTipork
          oParam.bTipoProposto = True
          oParent.NTSZOOM.ZoomStrIn("ZOOMANAGRA", oParent.DittaCorrente, oParam)
          If oParent.NTSZOOM.strIn <> NTSCStr(grvList.EditingValue) Then grvList.SetFocusedValue(oParent.NTSZOOM.strIn)
        Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473058636877164, "Lo zoon Clienti/fornitori è abilitato solo per listini specifici Clienti/fornitori"))
        End If
      ElseIf grvList.FocusedColumn.Name = "lc_coddest" Then
        'zoom destinaz. diversa
        If grvList.NTSGetCurrentDataRow Is Nothing Then
          If oCleList.lConto = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130584383594525492, "Indicare prima il codice cliente/fornitore"))
            Return
          End If
        Else
          If NTSCInt(grvList.NTSGetCurrentDataRow!lc_conto) = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130584383594525491, "Indicare prima il codice cliente/fornitore"))
            Return
          End If
        End If
        oParent.SetFastZoom(NTSCStr(grvList.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        oParent.NTSZOOM.strIn = NTSCStr(grvList.EditingValue)
        If grvList.NTSGetCurrentDataRow Is Nothing Then
          oParam.lContoCF = oCleList.lConto
        Else
          oParam.lContoCF = NTSCInt(grvList.NTSGetCurrentDataRow!lc_conto)
        End If
        oParent.NTSZOOM.ZoomStrIn("ZOOMDESTDIV", oParent.DittaCorrente, oParam)
        If oParent.NTSZOOM.strIn <> NTSCStr(grvList.EditingValue) Then grvList.SetFocusedValue(oParent.NTSZOOM.strIn)
      ElseIf grvList.FocusedColumn.Name = "lc_fase" Then
        'zoom fase articolo
        If grvList.NTSGetCurrentDataRow Is Nothing Then Return
        If NTSCStr(grvList.NTSGetCurrentDataRow!lc_codart).Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128473637654151389, "Indicare prima il codice articolo"))
          Return
        End If
        oParent.SetFastZoom(NTSCStr(grvList.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        oParent.NTSZOOM.strIn = NTSCStr(grvList.EditingValue)
        oParam.strTipo = NTSCStr(grvList.NTSGetCurrentDataRow!lc_codart).Trim
        oParent.NTSZOOM.ZoomStrIn("ZOOMARTFASI", oParent.DittaCorrente, oParam)
        If oParent.NTSZOOM.strIn <> NTSCStr(grvList.EditingValue) Then grvList.SetFocusedValue(oParent.NTSZOOM.strIn)
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        oParent.NTSCallStandardZoom()
      End If
      oParent.NTSLastControlFocussed.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbListEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbListEsci.ItemClick
    If NTSVisibleToUser = False Then Return

    If Not Salva() Then Return
    Me.FindForm.Close()
  End Sub
#End Region


#Region "Eventi Griglia"
  Public Overridable Sub grvList_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvList.NTSBeforeRowUpdate
    Try
      If oParent Is Nothing Then Return

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

  Public Overridable Sub grvList_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvList.NTSFocusedRowChanged
    Try
      '-------------------------
      'mi posiziono sulla prima cella a SX sulle nuove righe
      If grvList.FocusedRowHandle < 0 Then grvList.NTSMoveFirstColunn()

      GrvList_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvList_NTSNewRow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvList.NTSNewRow
    Try
      opValTutti.Enabled = False
      opValDay.Enabled = False
      edDtval.Enabled = False
      ckPromo.Enabled = False
      cbTipolistino.Enabled = False
      opValEur.Enabled = False
      opValAltro.Enabled = False
      edCodvalu.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvList_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvList.NTSFocusedColumnChanged
    Try
      GrvList_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function GrvList_RowColChange() As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Dim strT() As String = Nothing
    Try

      If oCleList Is Nothing Then Return True
      If dttUm Is Nothing Then Return True
      If LcCodart = "" Then Return True

      If grvList.NTSGetCurrentDataRow Is Nothing Then
        If Not lc_unmis.ColumnEdit Is Nothing Then CType(lc_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        lc_unmis.Enabled = False
      Else
        oParent.GctlSetVisEnab(lc_unmis, False)
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not lc_unmis.ColumnEdit Is Nothing Then
        CType(lc_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvList.FocusedColumn.Name = "lc_unmis" And grvList.GetFocusedRowCellValue(lc_codart).ToString <> "" And NTSCStr(grvList.GetFocusedRowCellValue(lc_codart)).Trim <> "" Then
          grvList.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          grList.Refresh()
          If oCleList.strCodartRoot.Trim = "" Then
            strTmp = oCleList.GetArticoUnMis(NTSCStr(grvList.GetFocusedRowCellValue(lc_codart)))
          Else
            strTmp = oCleList.GetArticoUnMis(oCleList.strCodartRoot)
          End If
          strT = strTmp.Split("§"c)
          dttTmp.Columns.Add("tb_codumis", GetType(String))
          For i As Integer = 0 To strT.Length - 1
            dttTmp.Rows.Add(New Object() {strT(i)})
          Next
          dttTmp.AcceptChanges()
          CType(lc_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
          'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
          'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
          dcTmp.DataSource = dttTmp
          For i As Integer = 0 To dcTmp.List.Count - 1
            If NTSCStr(CType(dcTmp.Current, DataRowView).Row!tb_codumis).ToUpper <> NTSCStr(grvList.GetFocusedRowCellValue(lc_unmis)).ToUpper Then
              dcTmp.MoveNext()
            Else
              Exit For
            End If
          Next
          grvList.liListCmb.DataSource = dcTmp
          grvList.liListCmb.Visible = True
        End If
      End If    'If Not lc_unmis.ColumnEdit Is Nothing Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Sub SettaVisibilitaColonne()
    Try
      '-------------------------------------------------------------------------------------------------------------
      Select Case oCleList.strChildParent
        Case "BN__CLIE"
          oParent.GctlSetVisEnab(lc_codart, True)
          oParent.GctlSetVisEnab(xx_codart, True)
          lc_conto.Visible = False
          xx_conto.Visible = False
          If CLN__STD.FRIENDLY Then
            lc_coddest.Visible = False
            xx_coddest.Visible = False
          Else
            oParent.GctlSetVisEnab(lc_coddest, True)
            oParent.GctlSetVisEnab(xx_coddest, True)
          End If
          lc_listino.Visible = False
          lbtipolist.Visible = False
          cbTipolistino.Visible = False
        Case "BNMGARTI", "BNMGARTV", "BNTCARTV"
          Select Case cbTipolistino.SelectedValue
            Case "C", "F"
              'listino specifico cliente/fornitore
              lc_codart.Visible = False
              xx_codart.Visible = False
              oParent.GctlSetVisEnab(lc_conto, True)
              oParent.GctlSetVisEnab(xx_conto, True)
              If CLN__STD.FRIENDLY Then
                lc_coddest.Visible = False
                xx_coddest.Visible = False
              Else
                oParent.GctlSetVisEnab(lc_coddest, True)
                oParent.GctlSetVisEnab(xx_coddest, True)
              End If
              lc_listino.Visible = False
              xx_listino.Visible = False
            Case " "
              'numero di listino
              lc_codart.Visible = False
              xx_codart.Visible = False
              lc_conto.Visible = False
              xx_conto.Visible = False
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              oParent.GctlSetVisEnab(lc_listino, True)
              oParent.GctlSetVisEnab(xx_listino, True)
          End Select
        Case "BNMGHLAR", "BNVEBOLX"
          oParent.GctlSetVisEnab(lc_conto, True)
          oParent.GctlSetVisEnab(xx_conto, True)
          If CLN__STD.FRIENDLY Then
            lc_coddest.Visible = False
            xx_coddest.Visible = False
          Else
            oParent.GctlSetVisEnab(lc_coddest, True)
            oParent.GctlSetVisEnab(xx_coddest, True)
          End If
          oParent.GctlSetVisEnab(lc_listino, True)
          oParent.GctlSetVisEnab(xx_listino, True)
          lc_codart.Visible = False
          xx_codart.Visible = False
          lbtipolist.Visible = False
          cbTipolistino.Visible = False
        Case "BNCPIMCO"
          Select Case cbTipolistino.SelectedValue
            Case "C", "F"
              'listino specifico cliente/fornitore
              lc_codart.Visible = False
              xx_codart.Visible = False
              oParent.GctlSetVisEnab(lc_conto, True)
              oParent.GctlSetVisEnab(xx_conto, True)
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              lc_listino.Visible = False
              xx_listino.Visible = False
            Case " "
              'numero di listino
              lc_codart.Visible = False
              xx_codart.Visible = False
              lc_conto.Visible = False
              xx_conto.Visible = False
              lc_coddest.Visible = False
              xx_coddest.Visible = False
              oParent.GctlSetVisEnab(lc_listino, True)
              oParent.GctlSetVisEnab(xx_listino, True)
          End Select
      End Select    'Select Case strChildParent
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSconto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSconto.Click
    Try
      'solo per sconti specifici cli/arti
      'If cbTiposconti.SelectedValue.ToString <> "4" Then Return 'già bloccato al cambio del combo, qui non serve

      If Not Salva() Then Return

      If grvList.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130438572832911429, "Posizionarsi prima su una riga di griglia"))
        Return
      End If

      Dim evnt As New NTSEventArgs("APRI", grvList.NTSGetCurrentDataRow!lc_conto.ToString & "§" & _
                                           grvList.NTSGetCurrentDataRow!lc_codart.ToString & "§" & _
                                           IIf(ckPromo.Checked, "-1", "0").ToString & "§" & _
                                           CDblSQL(NTSCDec(grvList.NTSGetCurrentDataRow!lc_daquant)) & "§" & _
                                           NTSCDate(edDtval.Text).ToShortDateString)

      RaiseEvent VaiScontoCollegato(Me, evnt)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
