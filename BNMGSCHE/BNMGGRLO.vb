Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRLO

#Region "Variabili"
  Public oCleSche As CLEMGSCHE
  Public oCallParams As CLE__CLDP
  Public dsGrlo As DataSet
  Public dcGrlo As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bNoModal As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrlo As NTSInformatica.NTSGrid
  Public WithEvents grvGrlo As NTSInformatica.NTSGridView
  Public WithEvents pnGrid As NTSInformatica.NTSPanel

  Public WithEvents xx_deslotto As NTSInformatica.NTSGridColumn
  Public WithEvents tt_aammgg As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents tt_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tt_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tt_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents xx_carichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_scarichi As NTSInformatica.NTSGridColumn
  Public WithEvents tt_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents xx_clfor As NTSInformatica.NTSGridColumn
  Public WithEvents xx_rimlotto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_costomedio As NTSInformatica.NTSGridColumn
  Public WithEvents xx_vallotto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datcarico As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipodoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numero As NTSInformatica.NTSGridColumn
  Public WithEvents xx_serie As NTSInformatica.NTSGridColumn
  Public WithEvents xx_forn As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desforn As NTSInformatica.NTSGridColumn
  Public WithEvents tt_ubicaz As NTSInformatica.NTSGridColumn

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem

  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents edValPrezzounitario As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValPrezzounitario As NTSInformatica.NTSLabel
  Public WithEvents edPrezzounitario As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPrezzounitario As NTSInformatica.NTSLabel
  Public WithEvents lbTotarticolo As NTSInformatica.NTSLabel
  Public WithEvents edTotarticolo As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents lbXx_magaz As NTSInformatica.NTSLabel
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGRLO))
    Me.grGrlo = New NTSInformatica.NTSGrid
    Me.grvGrlo = New NTSInformatica.NTSGridView
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.xx_deslotto = New NTSInformatica.NTSGridColumn
    Me.tt_aammgg = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.tt_serie = New NTSInformatica.NTSGridColumn
    Me.tt_numdoc = New NTSInformatica.NTSGridColumn
    Me.tt_riferim = New NTSInformatica.NTSGridColumn
    Me.xx_carichi = New NTSInformatica.NTSGridColumn
    Me.xx_scarichi = New NTSInformatica.NTSGridColumn
    Me.tt_prezzo = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.xx_clfor = New NTSInformatica.NTSGridColumn
    Me.xx_rimlotto = New NTSInformatica.NTSGridColumn
    Me.xx_costomedio = New NTSInformatica.NTSGridColumn
    Me.xx_vallotto = New NTSInformatica.NTSGridColumn
    Me.xx_datcarico = New NTSInformatica.NTSGridColumn
    Me.xx_tipodoc = New NTSInformatica.NTSGridColumn
    Me.xx_numero = New NTSInformatica.NTSGridColumn
    Me.xx_serie = New NTSInformatica.NTSGridColumn
    Me.xx_forn = New NTSInformatica.NTSGridColumn
    Me.xx_desforn = New NTSInformatica.NTSGridColumn
    Me.tt_ubicaz = New NTSInformatica.NTSGridColumn
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.edValPrezzounitario = New NTSInformatica.NTSTextBoxNum
    Me.lbValPrezzounitario = New NTSInformatica.NTSLabel
    Me.edPrezzounitario = New NTSInformatica.NTSTextBoxNum
    Me.lbPrezzounitario = New NTSInformatica.NTSLabel
    Me.lbTotarticolo = New NTSInformatica.NTSLabel
    Me.edTotarticolo = New NTSInformatica.NTSTextBoxNum
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.lbData = New NTSInformatica.NTSLabel
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    CType(Me.grGrlo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrlo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.edValPrezzounitario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzounitario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotarticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grGrlo
    '
    Me.grGrlo.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrlo.EmbeddedNavigator.Name = ""
    Me.grGrlo.Location = New System.Drawing.Point(0, 0)
    Me.grGrlo.MainView = Me.grvGrlo
    Me.grGrlo.Name = "grGrlo"
    Me.grGrlo.Size = New System.Drawing.Size(660, 314)
    Me.grGrlo.TabIndex = 5
    Me.grGrlo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrlo})
    '
    'grvGrlo
    '
    Me.grvGrlo.ActiveFilterEnabled = False
    Me.grvGrlo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_lottox, Me.xx_deslotto, Me.tt_aammgg, Me.xx_descr, Me.tt_serie, Me.tt_numdoc, Me.tt_riferim, Me.xx_carichi, Me.xx_scarichi, Me.tt_prezzo, Me.xx_valore, Me.xx_clfor, Me.xx_rimlotto, Me.xx_costomedio, Me.xx_vallotto, Me.xx_datcarico, Me.xx_tipodoc, Me.xx_numero, Me.xx_serie, Me.xx_forn, Me.xx_desforn, Me.tt_ubicaz})
    Me.grvGrlo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrlo.Enabled = True
    Me.grvGrlo.GridControl = Me.grGrlo
    Me.grvGrlo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrlo.MinRowHeight = 14
    Me.grvGrlo.Name = "grvGrlo"
    Me.grvGrlo.NTSAllowDelete = True
    Me.grvGrlo.NTSAllowInsert = True
    Me.grvGrlo.NTSAllowUpdate = True
    Me.grvGrlo.NTSMenuContext = Nothing
    Me.grvGrlo.OptionsCustomization.AllowRowSizing = True
    Me.grvGrlo.OptionsFilter.AllowFilterEditor = False
    Me.grvGrlo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrlo.OptionsNavigation.UseTabKey = False
    Me.grvGrlo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrlo.OptionsView.ColumnAutoWidth = False
    Me.grvGrlo.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrlo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrlo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrlo.OptionsView.ShowGroupPanel = False
    Me.grvGrlo.RowHeight = 16
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
    Me.xx_lottox.VisibleIndex = 0
    '
    'xx_deslotto
    '
    Me.xx_deslotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_deslotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_deslotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_deslotto.Caption = "Descr."
    Me.xx_deslotto.Enabled = True
    Me.xx_deslotto.FieldName = "xx_deslotto"
    Me.xx_deslotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_deslotto.Name = "xx_deslotto"
    Me.xx_deslotto.NTSRepositoryComboBox = Nothing
    Me.xx_deslotto.NTSRepositoryItemCheck = Nothing
    Me.xx_deslotto.NTSRepositoryItemMemo = Nothing
    Me.xx_deslotto.NTSRepositoryItemText = Nothing
    Me.xx_deslotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_deslotto.OptionsFilter.AllowFilter = False
    Me.xx_deslotto.Visible = True
    Me.xx_deslotto.VisibleIndex = 1
    '
    'tt_aammgg
    '
    Me.tt_aammgg.AppearanceCell.Options.UseBackColor = True
    Me.tt_aammgg.AppearanceCell.Options.UseTextOptions = True
    Me.tt_aammgg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_aammgg.Caption = "Data"
    Me.tt_aammgg.Enabled = True
    Me.tt_aammgg.FieldName = "tt_aammgg"
    Me.tt_aammgg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_aammgg.Name = "tt_aammgg"
    Me.tt_aammgg.NTSRepositoryComboBox = Nothing
    Me.tt_aammgg.NTSRepositoryItemCheck = Nothing
    Me.tt_aammgg.NTSRepositoryItemMemo = Nothing
    Me.tt_aammgg.NTSRepositoryItemText = Nothing
    Me.tt_aammgg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_aammgg.OptionsFilter.AllowFilter = False
    Me.tt_aammgg.Visible = True
    Me.tt_aammgg.VisibleIndex = 2
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Causale"
    Me.xx_descr.Enabled = True
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 3
    '
    'tt_serie
    '
    Me.tt_serie.AppearanceCell.Options.UseBackColor = True
    Me.tt_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tt_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_serie.Caption = "Serie"
    Me.tt_serie.Enabled = True
    Me.tt_serie.FieldName = "tt_serie"
    Me.tt_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_serie.Name = "tt_serie"
    Me.tt_serie.NTSRepositoryComboBox = Nothing
    Me.tt_serie.NTSRepositoryItemCheck = Nothing
    Me.tt_serie.NTSRepositoryItemMemo = Nothing
    Me.tt_serie.NTSRepositoryItemText = Nothing
    Me.tt_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_serie.OptionsFilter.AllowFilter = False
    Me.tt_serie.Visible = True
    Me.tt_serie.VisibleIndex = 4
    '
    'tt_numdoc
    '
    Me.tt_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tt_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tt_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_numdoc.Caption = "N°Bolla"
    Me.tt_numdoc.Enabled = True
    Me.tt_numdoc.FieldName = "tt_numdoc"
    Me.tt_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_numdoc.Name = "tt_numdoc"
    Me.tt_numdoc.NTSRepositoryComboBox = Nothing
    Me.tt_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tt_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tt_numdoc.NTSRepositoryItemText = Nothing
    Me.tt_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_numdoc.OptionsFilter.AllowFilter = False
    Me.tt_numdoc.Visible = True
    Me.tt_numdoc.VisibleIndex = 5
    '
    'tt_riferim
    '
    Me.tt_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tt_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tt_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_riferim.Caption = "Rifer.Bolla"
    Me.tt_riferim.Enabled = True
    Me.tt_riferim.FieldName = "tt_riferim"
    Me.tt_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_riferim.Name = "tt_riferim"
    Me.tt_riferim.NTSRepositoryComboBox = Nothing
    Me.tt_riferim.NTSRepositoryItemCheck = Nothing
    Me.tt_riferim.NTSRepositoryItemMemo = Nothing
    Me.tt_riferim.NTSRepositoryItemText = Nothing
    Me.tt_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_riferim.OptionsFilter.AllowFilter = False
    Me.tt_riferim.Visible = True
    Me.tt_riferim.VisibleIndex = 6
    '
    'xx_carichi
    '
    Me.xx_carichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_carichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_carichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_carichi.Caption = "Carichi"
    Me.xx_carichi.Enabled = True
    Me.xx_carichi.FieldName = "xx_carichi"
    Me.xx_carichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_carichi.Name = "xx_carichi"
    Me.xx_carichi.NTSRepositoryComboBox = Nothing
    Me.xx_carichi.NTSRepositoryItemCheck = Nothing
    Me.xx_carichi.NTSRepositoryItemMemo = Nothing
    Me.xx_carichi.NTSRepositoryItemText = Nothing
    Me.xx_carichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_carichi.OptionsFilter.AllowFilter = False
    Me.xx_carichi.Visible = True
    Me.xx_carichi.VisibleIndex = 7
    '
    'xx_scarichi
    '
    Me.xx_scarichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_scarichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_scarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_scarichi.Caption = "Scarichi"
    Me.xx_scarichi.Enabled = True
    Me.xx_scarichi.FieldName = "xx_scarichi"
    Me.xx_scarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_scarichi.Name = "xx_scarichi"
    Me.xx_scarichi.NTSRepositoryComboBox = Nothing
    Me.xx_scarichi.NTSRepositoryItemCheck = Nothing
    Me.xx_scarichi.NTSRepositoryItemMemo = Nothing
    Me.xx_scarichi.NTSRepositoryItemText = Nothing
    Me.xx_scarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_scarichi.OptionsFilter.AllowFilter = False
    Me.xx_scarichi.Visible = True
    Me.xx_scarichi.VisibleIndex = 8
    '
    'tt_prezzo
    '
    Me.tt_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.tt_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.tt_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_prezzo.Caption = "Prezzo Un."
    Me.tt_prezzo.Enabled = True
    Me.tt_prezzo.FieldName = "tt_prezzo"
    Me.tt_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_prezzo.Name = "tt_prezzo"
    Me.tt_prezzo.NTSRepositoryComboBox = Nothing
    Me.tt_prezzo.NTSRepositoryItemCheck = Nothing
    Me.tt_prezzo.NTSRepositoryItemMemo = Nothing
    Me.tt_prezzo.NTSRepositoryItemText = Nothing
    Me.tt_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_prezzo.OptionsFilter.AllowFilter = False
    Me.tt_prezzo.Visible = True
    Me.tt_prezzo.VisibleIndex = 9
    '
    'xx_valore
    '
    Me.xx_valore.AppearanceCell.Options.UseBackColor = True
    Me.xx_valore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valore.Caption = "Valore"
    Me.xx_valore.Enabled = True
    Me.xx_valore.FieldName = "xx_valore"
    Me.xx_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valore.Name = "xx_valore"
    Me.xx_valore.NTSRepositoryComboBox = Nothing
    Me.xx_valore.NTSRepositoryItemCheck = Nothing
    Me.xx_valore.NTSRepositoryItemMemo = Nothing
    Me.xx_valore.NTSRepositoryItemText = Nothing
    Me.xx_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valore.OptionsFilter.AllowFilter = False
    Me.xx_valore.Visible = True
    Me.xx_valore.VisibleIndex = 10
    '
    'xx_clfor
    '
    Me.xx_clfor.AppearanceCell.Options.UseBackColor = True
    Me.xx_clfor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_clfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_clfor.Caption = "Cliente/Forn."
    Me.xx_clfor.Enabled = True
    Me.xx_clfor.FieldName = "xx_clfor"
    Me.xx_clfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_clfor.Name = "xx_clfor"
    Me.xx_clfor.NTSRepositoryComboBox = Nothing
    Me.xx_clfor.NTSRepositoryItemCheck = Nothing
    Me.xx_clfor.NTSRepositoryItemMemo = Nothing
    Me.xx_clfor.NTSRepositoryItemText = Nothing
    Me.xx_clfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_clfor.OptionsFilter.AllowFilter = False
    Me.xx_clfor.Visible = True
    Me.xx_clfor.VisibleIndex = 11
    '
    'xx_rimlotto
    '
    Me.xx_rimlotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_rimlotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_rimlotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_rimlotto.Caption = "Rimanenza Lotto"
    Me.xx_rimlotto.Enabled = True
    Me.xx_rimlotto.FieldName = "xx_rimlotto"
    Me.xx_rimlotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_rimlotto.Name = "xx_rimlotto"
    Me.xx_rimlotto.NTSRepositoryComboBox = Nothing
    Me.xx_rimlotto.NTSRepositoryItemCheck = Nothing
    Me.xx_rimlotto.NTSRepositoryItemMemo = Nothing
    Me.xx_rimlotto.NTSRepositoryItemText = Nothing
    Me.xx_rimlotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_rimlotto.OptionsFilter.AllowFilter = False
    Me.xx_rimlotto.Visible = True
    Me.xx_rimlotto.VisibleIndex = 12
    '
    'xx_costomedio
    '
    Me.xx_costomedio.AppearanceCell.Options.UseBackColor = True
    Me.xx_costomedio.AppearanceCell.Options.UseTextOptions = True
    Me.xx_costomedio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_costomedio.Caption = "Costo Medio"
    Me.xx_costomedio.Enabled = True
    Me.xx_costomedio.FieldName = "xx_costomedio"
    Me.xx_costomedio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_costomedio.Name = "xx_costomedio"
    Me.xx_costomedio.NTSRepositoryComboBox = Nothing
    Me.xx_costomedio.NTSRepositoryItemCheck = Nothing
    Me.xx_costomedio.NTSRepositoryItemMemo = Nothing
    Me.xx_costomedio.NTSRepositoryItemText = Nothing
    Me.xx_costomedio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_costomedio.OptionsFilter.AllowFilter = False
    Me.xx_costomedio.Visible = True
    Me.xx_costomedio.VisibleIndex = 13
    '
    'xx_vallotto
    '
    Me.xx_vallotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_vallotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_vallotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_vallotto.Caption = "Valore Saldo Lotto"
    Me.xx_vallotto.Enabled = True
    Me.xx_vallotto.FieldName = "xx_vallotto"
    Me.xx_vallotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_vallotto.Name = "xx_vallotto"
    Me.xx_vallotto.NTSRepositoryComboBox = Nothing
    Me.xx_vallotto.NTSRepositoryItemCheck = Nothing
    Me.xx_vallotto.NTSRepositoryItemMemo = Nothing
    Me.xx_vallotto.NTSRepositoryItemText = Nothing
    Me.xx_vallotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_vallotto.OptionsFilter.AllowFilter = False
    Me.xx_vallotto.Visible = True
    Me.xx_vallotto.VisibleIndex = 14
    '
    'xx_datcarico
    '
    Me.xx_datcarico.AppearanceCell.Options.UseBackColor = True
    Me.xx_datcarico.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datcarico.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datcarico.Caption = "Data carico"
    Me.xx_datcarico.Enabled = True
    Me.xx_datcarico.FieldName = "xx_datcarico"
    Me.xx_datcarico.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datcarico.Name = "xx_datcarico"
    Me.xx_datcarico.NTSRepositoryComboBox = Nothing
    Me.xx_datcarico.NTSRepositoryItemCheck = Nothing
    Me.xx_datcarico.NTSRepositoryItemMemo = Nothing
    Me.xx_datcarico.NTSRepositoryItemText = Nothing
    Me.xx_datcarico.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datcarico.OptionsFilter.AllowFilter = False
    '
    'xx_tipodoc
    '
    Me.xx_tipodoc.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipodoc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipodoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipodoc.Caption = "Tipo doc."
    Me.xx_tipodoc.Enabled = True
    Me.xx_tipodoc.FieldName = "xx_tipodoc"
    Me.xx_tipodoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipodoc.Name = "xx_tipodoc"
    Me.xx_tipodoc.NTSRepositoryComboBox = Nothing
    Me.xx_tipodoc.NTSRepositoryItemCheck = Nothing
    Me.xx_tipodoc.NTSRepositoryItemMemo = Nothing
    Me.xx_tipodoc.NTSRepositoryItemText = Nothing
    Me.xx_tipodoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipodoc.OptionsFilter.AllowFilter = False
    '
    'xx_numero
    '
    Me.xx_numero.AppearanceCell.Options.UseBackColor = True
    Me.xx_numero.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numero.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numero.Caption = "Numero"
    Me.xx_numero.Enabled = True
    Me.xx_numero.FieldName = "xx_numero"
    Me.xx_numero.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numero.Name = "xx_numero"
    Me.xx_numero.NTSRepositoryComboBox = Nothing
    Me.xx_numero.NTSRepositoryItemCheck = Nothing
    Me.xx_numero.NTSRepositoryItemMemo = Nothing
    Me.xx_numero.NTSRepositoryItemText = Nothing
    Me.xx_numero.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numero.OptionsFilter.AllowFilter = False
    '
    'xx_serie
    '
    Me.xx_serie.AppearanceCell.Options.UseBackColor = True
    Me.xx_serie.AppearanceCell.Options.UseTextOptions = True
    Me.xx_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_serie.Caption = "Serie"
    Me.xx_serie.Enabled = True
    Me.xx_serie.FieldName = "xx_serie"
    Me.xx_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_serie.Name = "xx_serie"
    Me.xx_serie.NTSRepositoryComboBox = Nothing
    Me.xx_serie.NTSRepositoryItemCheck = Nothing
    Me.xx_serie.NTSRepositoryItemMemo = Nothing
    Me.xx_serie.NTSRepositoryItemText = Nothing
    Me.xx_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_serie.OptionsFilter.AllowFilter = False
    '
    'xx_forn
    '
    Me.xx_forn.AppearanceCell.Options.UseBackColor = True
    Me.xx_forn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_forn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_forn.Caption = "Fornitore"
    Me.xx_forn.Enabled = True
    Me.xx_forn.FieldName = "xx_forn"
    Me.xx_forn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_forn.Name = "xx_forn"
    Me.xx_forn.NTSRepositoryComboBox = Nothing
    Me.xx_forn.NTSRepositoryItemCheck = Nothing
    Me.xx_forn.NTSRepositoryItemMemo = Nothing
    Me.xx_forn.NTSRepositoryItemText = Nothing
    Me.xx_forn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_forn.OptionsFilter.AllowFilter = False
    '
    'xx_desforn
    '
    Me.xx_desforn.AppearanceCell.Options.UseBackColor = True
    Me.xx_desforn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desforn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desforn.Caption = "Descrizione"
    Me.xx_desforn.Enabled = True
    Me.xx_desforn.FieldName = "xx_desforn"
    Me.xx_desforn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desforn.Name = "xx_desforn"
    Me.xx_desforn.NTSRepositoryComboBox = Nothing
    Me.xx_desforn.NTSRepositoryItemCheck = Nothing
    Me.xx_desforn.NTSRepositoryItemMemo = Nothing
    Me.xx_desforn.NTSRepositoryItemText = Nothing
    Me.xx_desforn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desforn.OptionsFilter.AllowFilter = False
    '
    'tt_ubicaz
    '
    Me.tt_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.tt_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.tt_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_ubicaz.Caption = "Ubicaz."
    Me.tt_ubicaz.Enabled = True
    Me.tt_ubicaz.FieldName = "tt_ubicaz"
    Me.tt_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_ubicaz.Name = "tt_ubicaz"
    Me.tt_ubicaz.NTSRepositoryComboBox = Nothing
    Me.tt_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.tt_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.tt_ubicaz.NTSRepositoryItemText = Nothing
    Me.tt_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_ubicaz.OptionsFilter.AllowFilter = False
    Me.tt_ubicaz.Visible = True
    Me.tt_ubicaz.VisibleIndex = 15
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrlo)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 92)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 314)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbImpostaStampante, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbTaglie, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 15
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 9
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 10
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 11
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 12
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.Id = 13
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 8
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 7
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.edValPrezzounitario)
    Me.pnBottom.Controls.Add(Me.lbValPrezzounitario)
    Me.pnBottom.Controls.Add(Me.edPrezzounitario)
    Me.pnBottom.Controls.Add(Me.lbPrezzounitario)
    Me.pnBottom.Controls.Add(Me.lbTotarticolo)
    Me.pnBottom.Controls.Add(Me.edTotarticolo)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 406)
    Me.pnBottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnBottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(660, 36)
    Me.pnBottom.TabIndex = 8
    Me.pnBottom.Text = "NtsPanel1"
    '
    'edValPrezzounitario
    '
    Me.edValPrezzounitario.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValPrezzounitario.Location = New System.Drawing.Point(521, 6)
    Me.edValPrezzounitario.Name = "edValPrezzounitario"
    Me.edValPrezzounitario.NTSDbField = ""
    Me.edValPrezzounitario.NTSFormat = "0"
    Me.edValPrezzounitario.NTSForzaVisZoom = False
    Me.edValPrezzounitario.NTSOldValue = ""
    Me.edValPrezzounitario.Properties.Appearance.Options.UseTextOptions = True
    Me.edValPrezzounitario.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValPrezzounitario.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValPrezzounitario.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValPrezzounitario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValPrezzounitario.Properties.MaxLength = 65536
    Me.edValPrezzounitario.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValPrezzounitario.Size = New System.Drawing.Size(100, 20)
    Me.edValPrezzounitario.TabIndex = 53
    '
    'lbValPrezzounitario
    '
    Me.lbValPrezzounitario.AutoSize = True
    Me.lbValPrezzounitario.BackColor = System.Drawing.Color.Transparent
    Me.lbValPrezzounitario.Location = New System.Drawing.Point(447, 9)
    Me.lbValPrezzounitario.Name = "lbValPrezzounitario"
    Me.lbValPrezzounitario.NTSDbField = ""
    Me.lbValPrezzounitario.Size = New System.Drawing.Size(58, 13)
    Me.lbValPrezzounitario.TabIndex = 52
    Me.lbValPrezzounitario.Text = "Valore tot."
    Me.lbValPrezzounitario.Tooltip = ""
    Me.lbValPrezzounitario.UseMnemonic = False
    '
    'edPrezzounitario
    '
    Me.edPrezzounitario.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzounitario.Location = New System.Drawing.Point(310, 6)
    Me.edPrezzounitario.Name = "edPrezzounitario"
    Me.edPrezzounitario.NTSDbField = ""
    Me.edPrezzounitario.NTSFormat = "0"
    Me.edPrezzounitario.NTSForzaVisZoom = False
    Me.edPrezzounitario.NTSOldValue = ""
    Me.edPrezzounitario.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzounitario.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzounitario.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzounitario.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzounitario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzounitario.Properties.MaxLength = 65536
    Me.edPrezzounitario.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzounitario.Size = New System.Drawing.Size(100, 20)
    Me.edPrezzounitario.TabIndex = 51
    '
    'lbPrezzounitario
    '
    Me.lbPrezzounitario.AutoSize = True
    Me.lbPrezzounitario.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzounitario.Location = New System.Drawing.Point(213, 9)
    Me.lbPrezzounitario.Name = "lbPrezzounitario"
    Me.lbPrezzounitario.NTSDbField = ""
    Me.lbPrezzounitario.Size = New System.Drawing.Size(88, 13)
    Me.lbPrezzounitario.TabIndex = 50
    Me.lbPrezzounitario.Text = "Costo unit.medio"
    Me.lbPrezzounitario.Tooltip = ""
    Me.lbPrezzounitario.UseMnemonic = False
    '
    'lbTotarticolo
    '
    Me.lbTotarticolo.AutoSize = True
    Me.lbTotarticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbTotarticolo.Location = New System.Drawing.Point(6, 9)
    Me.lbTotarticolo.Name = "lbTotarticolo"
    Me.lbTotarticolo.NTSDbField = ""
    Me.lbTotarticolo.Size = New System.Drawing.Size(67, 13)
    Me.lbTotarticolo.TabIndex = 48
    Me.lbTotarticolo.Text = "Rim. Articolo"
    Me.lbTotarticolo.Tooltip = ""
    Me.lbTotarticolo.UseMnemonic = False
    '
    'edTotarticolo
    '
    Me.edTotarticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotarticolo.Location = New System.Drawing.Point(79, 6)
    Me.edTotarticolo.Name = "edTotarticolo"
    Me.edTotarticolo.NTSDbField = ""
    Me.edTotarticolo.NTSFormat = "0"
    Me.edTotarticolo.NTSForzaVisZoom = False
    Me.edTotarticolo.NTSOldValue = ""
    Me.edTotarticolo.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotarticolo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotarticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotarticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotarticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotarticolo.Properties.MaxLength = 65536
    Me.edTotarticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotarticolo.Size = New System.Drawing.Size(100, 20)
    Me.edTotarticolo.TabIndex = 49
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(10, 9)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 63
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'lbXx_articolo
    '
    Me.lbXx_articolo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_articolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_articolo.Location = New System.Drawing.Point(199, 8)
    Me.lbXx_articolo.Name = "lbXx_articolo"
    Me.lbXx_articolo.NTSDbField = ""
    Me.lbXx_articolo.Size = New System.Drawing.Size(251, 20)
    Me.lbXx_articolo.TabIndex = 70
    Me.lbXx_articolo.Tooltip = ""
    Me.lbXx_articolo.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(10, 34)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(35, 13)
    Me.lbMagaz.TabIndex = 64
    Me.lbMagaz.Text = "Mag.:"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(460, 11)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(34, 13)
    Me.lbFase.TabIndex = 65
    Me.lbFase.Text = "Fase:"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(62, 6)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(131, 20)
    Me.edArticolo.TabIndex = 69
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(498, 8)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(57, 20)
    Me.edFase.TabIndex = 71
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.Location = New System.Drawing.Point(62, 31)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(53, 20)
    Me.edMagaz.TabIndex = 72
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbXx_magaz)
    Me.pnTop.Controls.Add(Me.lbData)
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 62)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(121, 31)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(328, 20)
    Me.lbXx_magaz.TabIndex = 74
    Me.lbXx_magaz.Tooltip = ""
    Me.lbXx_magaz.UseMnemonic = False
    '
    'lbData
    '
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbData.Location = New System.Drawing.Point(455, 31)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(193, 20)
    Me.lbData.TabIndex = 73
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa anteprima a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 14
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'FRMMGGRLO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(660, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGGRLO"
    Me.NTSLastControlFocussed = Me.grGrlo
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE ARTICOLI PER LOTTI"
    CType(Me.grGrlo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrlo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.edValPrezzounitario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzounitario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotarticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function

  Public Overridable Sub InitEntity(ByVal cleSche As CLEMGSCHE)
    oCleSche = cleSche
    AddHandler oCleSche.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipork As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 129055190875147121, "Articolo"), 0)
      edValPrezzounitario.NTSSetParam(oMenu, oApp.Tr(Me, 128650154391688398, "Costo unit.medio"), oApp.FormatImpVal)
      edPrezzounitario.NTSSetParam(oMenu, oApp.Tr(Me, 128650154392000900, "Costo unit.medio"), oApp.FormatImporti)
      edTotarticolo.NTSSetParam(oMenu, oApp.Tr(Me, 128650154392469653, "Rim. Articolo"), oApp.FormatQta)
      edMagaz.NTSSetParam(oMenu, oApp.Tr(Me, 128650154392782155, "Mag.:"), "0")
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 128650154392938406, "Fase:"), "0")

      grvGrlo.NTSSetParam(oMenu, "Stampa/Visualizzazione Schede Articoli per Lotti")

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"R", "IC"})
      dttTipork.Rows.Add(New Object() {"O", "OF"})
      dttTipork.Rows.Add(New Object() {"H", "OP"})
      dttTipork.Rows.Add(New Object() {"Y", "IP"})
      dttTipork.Rows.Add(New Object() {"Q", "PR"})
      dttTipork.Rows.Add(New Object() {"X", "IT"})
      dttTipork.Rows.Add(New Object() {"$", "OFA"})
      dttTipork.Rows.Add(New Object() {"V", "ICA"})
      dttTipork.Rows.Add(New Object() {"#", "ICO"})
      dttTipork.AcceptChanges()

      tt_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650154386219613, "Prezzo Un."), oCleSche.TrovaFmtPrz, 15)
      tt_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650154385438358, "Serie"), CLN__STD.SerieMaxLen, True)
      tt_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650154385594609, "N°Bolla"), "0", 9, 0, 999999999)
      tt_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650154385750860, "Rifer.Bolla"), 0, True)
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650154384500852, "Lotto"), 50, True)
      xx_deslotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650154384657103, "Descr."), 0, True)
      tt_aammgg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128650154384813354, "Data"), True)
      xx_desforn.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650169958350524, "Descrizione"), 0, True)
      xx_rimlotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169957100516, "Rimanenza Commessa"), "#,##0.00", 15)
      xx_costomedio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169957256767, "Costo Medio"), oCleSche.TrovaFmtPrz, 15)
      xx_vallotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169957413018, "Valore Saldo Commessa"), "#,##0.00", 15)
      xx_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650169954756751, "Causale"), 0, True)
      xx_carichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169955381755, "Carichi"), "#,##0.00", 15)
      xx_scarichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169955538006, "Scarichi"), "#,##0.00", 15)
      xx_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650169955850508, "Valore"), "#,##0.00", 15)
      xx_clfor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650174467129380, "Cliente/Forn."), 0, True)
      xx_datcarico.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128650174467754384, "Data carico"), True)
      xx_tipodoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650174467910635, "Tipo doc."), 0, True)
      xx_numero.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650174468066886, "Numero"), "0", 9, 0, 999999999)
      xx_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650174468223137, "Serie"), CLN__STD.SerieMaxLen, True)
      xx_forn.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650174468379388, "Fornitore"), "0", 9, 0, 999999999)
      tt_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650174468691890, "Ubicaz."), 0, True)

      grvGrlo.Enabled = False
      grvGrlo.NTSAllowInsert = False

      edArticolo.Enabled = False
      edFase.Enabled = False
      edMagaz.Enabled = False
      edTotarticolo.Enabled = False
      edPrezzounitario.Enabled = False
      edValPrezzounitario.Enabled = False

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

#Region "Eventi di Form"
  Public Overridable Sub FRMMGGRLO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      oCleSche.strDatini = CDataSQL(oCleSche.strScarDatini)
      oCleSche.strDatfin = CDataSQL(oCleSche.strScarDatfin)
      If Not Apri() Then Exit Sub
      ApriRecordset()
      RiempiLabel()

      lbData.Text = oApp.Tr(Me, 130421130763649152, _
                    "Dal: |" & oCleSche.strScarDatini & "|  Al: |" & oCleSche.strScarDatfin & "|")

      If oCleSche.nScarOrdin = 3 Or oCleSche.nScarOrdin = 4 Then
        xx_lottox.Caption = oApp.Tr(Me, 129080496632531982, "Lotto")
        xx_rimlotto.Caption = oApp.Tr(Me, 129080496680971342, "Rimanenza Lotto")
        xx_vallotto.Caption = oApp.Tr(Me, 129080496699878318, "Valore Saldo Lotto")
        tt_ubicaz.Visible = False
        xx_lottox.Visible = True
        xx_deslotto.Visible = True
      End If
      If oCleSche.nScarOrdin = 6 Or oCleSche.nScarOrdin = 7 Then
        xx_lottox.Caption = oApp.Tr(Me, 129080496722066670, "Comm.")
        xx_rimlotto.Caption = oApp.Tr(Me, 129080496742692462, "Rimanenza Commessa")
        xx_vallotto.Caption = oApp.Tr(Me, 129080496761286926, "Valore Saldo Commessa")
        tt_ubicaz.Visible = False
        xx_lottox.Visible = True
        xx_deslotto.Visible = True
      End If
      If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then 'ubicaz. aperte e aperte-chiuse
        xx_lottox.Visible = False
        xx_deslotto.Visible = False
        tt_ubicaz.Visible = True
        xx_rimlotto.Caption = oApp.Tr(Me, 129080496788162958, "Rimanenza Ubicazione")
        xx_vallotto.Caption = oApp.Tr(Me, 129080496819101646, "Valore Saldo Ubicazione")
      End If
      Select Case oCleSche.nScarOrdin
        Case 3, 4, 5 : Me.Text = oApp.Tr(Me, 129080491418600650, "Stampa/Visualizzazione Schede Articoli per Lotti")
        Case 6, 7, 8 : Me.Text = oApp.Tr(Me, 129080491633451275, "Stampa/Visualizzazione Schede Articoli per Commesse")
        Case 12, 13, 14 : Me.Text = oApp.Tr(Me, 129080491667358610, "Stampa/Visualizzazione Schede Articoli per Ubicazione")
      End Select
      If Not ((oCleSche.nScarOrdin = 5) Or (oCleSche.nScarOrdin = 8) Or (oCleSche.nScarOrdin = 14)) Then xx_vallotto.Visible = False
      If (oCleSche.nScarOrdin = 5) Or (oCleSche.nScarOrdin = 8) Or (oCleSche.nScarOrdin = 14) Then ' solo saldi
        tt_aammgg.Visible = False
        xx_descr.Visible = False
        tt_serie.Visible = False
        tt_numdoc.Visible = False
        tt_riferim.Visible = False
        xx_carichi.Visible = False
        xx_scarichi.Visible = False
        tt_prezzo.Visible = False
        xx_valore.Visible = False
        xx_clfor.Visible = False
        If (oCleSche.nScarOrdin = 5) Then
          xx_lottox.Caption = oApp.Tr(Me, 129080497428500046, "Lotto")
          xx_rimlotto.Caption = oApp.Tr(Me, 129080497451782190, "Rimanenza Lotto")
          xx_vallotto.Caption = oApp.Tr(Me, 129080497473189262, "Valore Saldo Lotto")
          tt_ubicaz.Visible = False
          xx_lottox.Visible = True
          xx_deslotto.Visible = True
        End If
        If (oCleSche.nScarOrdin = 8) Then
          xx_lottox.Caption = oApp.Tr(Me, 129080497502721646, "Comm.")
          xx_rimlotto.Caption = oApp.Tr(Me, 129080497523347438, "Rimanenza Comm")
          xx_vallotto.Caption = oApp.Tr(Me, 129080497543816974, "Valore Saldo Comm")
          tt_ubicaz.Visible = False
          xx_lottox.Visible = True
          xx_deslotto.Visible = True
        End If
        If (oCleSche.nScarOrdin = 14) Then ' saldo ubicazioni (=14)
          xx_lottox.Visible = False
          xx_deslotto.Visible = False
          tt_ubicaz.Visible = True
          xx_rimlotto.Caption = oApp.Tr(Me, 129080497823515214, "Rimanenza Ubicazione")
          xx_vallotto.Caption = oApp.Tr(Me, 129080497842578446, "Valore Saldo Ubicazione")
        End If
      End If
      '-----------------------------------------------------------------------------------------
      If oCleSche.bModTCO = False Then
        tlbTaglie.Visible = False
      Else
        Select Case oCleSche.nScarOrdin
          Case 5, 8, 14 : tlbTaglie.Enabled = False
        End Select
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

  Public Overridable Sub FRMMGGRLO_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGRLO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then oCleSche.SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGGRLO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrlo.Dispose()
      dsGrlo.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      dcGrlo.MoveFirst()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      dcGrlo.MovePrevious()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      dcGrlo.MoveNext()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      dcGrlo.MoveLast()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsArtico As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie e colori esce
      '-----------------------------------------------------------------------------------------
      If oCleSche.bModTCO = False Then Exit Sub

      If grvGrlo.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      Select Case oCleSche.nScarOrdin
        Case 5, 8, 14 : Exit Sub
      End Select
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è gestito per taglia, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If oCleSche.bModTCO = True Then
        If Not oCleSche.CheckArticotaglie(NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_codart)) Then
          Exit Sub
        End If
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSMGSCHE"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_codart) & "|" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_codart) & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_fase) & "|" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_fase) & "|" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_tipork) & ";" & _
               Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_anno), 4) & ";" & _
               NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_serie) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_numdoc), 9) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrlo.NTSGetCurrentDataRow!tt_riga), 9) & ";|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Dim strHeader As String = ""
    Dim strFooter As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strHeader = lbArticolo.Text & "".PadLeft(1) & edArticolo.Text & "".PadLeft(1) & lbXx_articolo.Text & "".PadLeft(5) & _
        lbMagaz.Text & "".PadLeft(1) & edMagaz.Text & "".PadLeft(1) & lbXx_magaz.Text
      strFooter = lbTotarticolo.Text & "".PadLeft(1) & edTotarticolo.Text & "".PadLeft(5) & _
        lbPrezzounitario.Text & "".PadLeft(1) & edPrezzounitario.Text & "".PadLeft(5) & _
        lbValPrezzounitario.Text & "".PadLeft(1) & edValPrezzounitario.Text
      '--------------------------------------------------------------------------------------------------------------      
      grvGrlo.NTSPrintPreview(strHeader, strFooter)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub ApriRecordset()
    Try
      ComponiStringa()

      CaricaColonneUnbound()

      TotRimanenzeArticoli()
      TotCostoUnitario()

      RiempiLabel()

      dcGrid.DataSource = dsGrid.Tables("TTLOTTI")
      dsGrid.AcceptChanges()

      grGrlo.DataSource = dcGrid

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiLabel()
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If dsGrlo.Tables("TTLOTTI").Rows.Count = 0 Then Exit Sub

      edArticolo.Text = NTSCStr(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position)!tt_codart)
      If Not oCleSche.lbArticolo_Validated(NTSCStr(edArticolo.Text), strTmp, dttTmp) Then
        lbXx_articolo.Text = ""
      Else
        lbXx_articolo.Text = strTmp & " " & NTSCStr(dttTmp.Rows(0)!ar_desint)
      End If
      edFase.Text = NTSCStr(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position)!tt_fase)

      edMagaz.Text = NTSCStr(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position)!tt_magaz)
      If Not oCleSche.GrlolbMaga_Validated(NTSCInt(edMagaz.Text), strTmp) Then
        lbXx_magaz.Text = ""
      Else
        lbXx_magaz.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringa()
    Dim ds As DataSet = Nothing
    Try
      oCleSche.GrloComponiStringa(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsGrid)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleSche.GrloApri(dsGrlo) Then
        bClose = True
        Return False
      End If

      dcGrlo.DataSource = dsGrlo.Tables("TTLOTTI")
      dsGrlo.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaColonneUnbound() As Boolean
    Dim i As Integer
    Dim strDescr As String = ""
    Dim dsAnalotti As DataSet = Nothing
    Dim lLotto As Integer
    Dim lLotto1 As Integer
    Dim strUbicaz As String = ""
    Dim strUbicaz1 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dtrTmp As DataRow
    Try
      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (Not oCleSche.nScarOrdin = 5) And (Not oCleSche.nScarOrdin = 8) And (Not oCleSche.nScarOrdin = 14) Then
          If NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_causale) = 0 Then GoTo ProssimaCausale
          If Not oCleSche.GrloCausale_Validated(NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_causale), strDescr) Then
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_descr = ""
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_descr = strDescr
          End If
        End If
ProssimaCausale:
      Next

      'Case DESLOTTO
      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        'Lotti
        If (oCleSche.nScarOrdin = 3) Or (oCleSche.nScarOrdin = 4) Or (oCleSche.nScarOrdin = 5) Then
          oCleSche.GrloGetAnalotti(NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i)!tt_codart), NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i)!tt_lotto), _
                              dsAnalotti)

          'non viene utilizzata la UnboundColumnFetch alo_codalf perchè il campo è bound in alo_descr
          If dsAnalotti.Tables("ANALOTTI").Rows.Count = 0 Then
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_deslotto = ""
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_deslotto = NTSCStr(dsAnalotti.Tables("ANALOTTI").Rows(0)!alo_descr)
          End If
        End If
        'Commesse
        If (oCleSche.nScarOrdin = 6) Or (oCleSche.nScarOrdin = 7) Or (oCleSche.nScarOrdin = 8) Then
          If oCleSche.nScarOrdin = 8 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_lottox = dsGrid.Tables("TTLOTTI").Rows(i)!tt_lotto
          If NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!xx_lottox) = 0 Then GoTo ProssimaCommessa
          If Not oCleSche.GrloCommessa_Validated(NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!xx_lottox), strDescr) Then
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_deslotto = ""
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_deslotto = strDescr
          End If
        End If
ProssimaCommessa:
        ' ubicaz: NON ESISTE descrizione
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (Not oCleSche.nScarOrdin = 5) And (Not oCleSche.nScarOrdin = 8) And (Not oCleSche.nScarOrdin = 14) Then
          Select Case NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_carscar)
            Case 1 : dsGrid.Tables("TTLOTTI").Rows(i)!xx_carichi = dsGrid.Tables("TTLOTTI").Rows(i)!tt_quant
            Case -1 : dsGrid.Tables("TTLOTTI").Rows(i)!xx_carichi = 0
          End Select
        End If
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (Not oCleSche.nScarOrdin = 5) And (Not oCleSche.nScarOrdin = 8) And (Not oCleSche.nScarOrdin = 14) Then
          Select Case NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_carscar)
            Case 1 : dsGrid.Tables("TTLOTTI").Rows(i)!xx_scarichi = 0
            Case -1 : dsGrid.Tables("TTLOTTI").Rows(i)!xx_scarichi = dsGrid.Tables("TTLOTTI").Rows(i)!tt_quant
          End Select
        End If
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (Not oCleSche.nScarOrdin = 5) And (Not oCleSche.nScarOrdin = 8) And (Not oCleSche.nScarOrdin = 14) Then
          dsGrid.Tables("TTLOTTI").Rows(i)!xx_valore = NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_quant) * NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_prezzo)
        End If
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (Not oCleSche.nScarOrdin = 5) And (Not oCleSche.nScarOrdin = 8) And (Not oCleSche.nScarOrdin = 14) Then
          If NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_conto) = 0 Then GoTo ProssimoConto
          If Not oCleSche.GrlolbConto_Validated(NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_conto), strDescr) Then
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_desforn = ""
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_desforn = strDescr
          End If
        End If
ProssimoConto:
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (oCleSche.nScarOrdin = 5) Or (oCleSche.nScarOrdin = 8) Or (oCleSche.nScarOrdin = 14) Then
          dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = dsGrid.Tables("TTLOTTI").Rows(i)!tt_quant
        Else
          If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
            strUbicaz = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i)!tt_ubicaz)
          Else
            lLotto = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_lotto)
          End If
          'controllo i valori sul record successivo o sul primo record di griglia dtrTmp
          If i < dsGrid.Tables("TTLOTTI").Rows.Count - 1 Then
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(i + 1)
            If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
              strUbicaz1 = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_ubicaz)
            Else
              lLotto1 = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_lotto)
            End If
          Else
            lLotto1 = 0
            strUbicaz1 = ""
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(0)
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = (NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_quant) * NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_carscar))
          End If
          If (Not (oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13) And lLotto <> lLotto1) Then

            oCleSche.GrloGetRimLotto(dtrTmp, lLotto, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Saldo) Else dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = IntSetNum("0,000")
          End If
          If ((oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13) And strUbicaz <> strUbicaz1) Then

            oCleSche.GrloGetRimLotto2(dtrTmp, strUbicaz, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Saldo) Else dsGrid.Tables("TTLOTTI").Rows(i)!xx_rimlotto = IntSetNum("0,000")
          End If
        End If
      Next


      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (oCleSche.nScarOrdin = 5) Or (oCleSche.nScarOrdin = 8) Or (oCleSche.nScarOrdin = 14) Then
          If Not ArrDbl(NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_qtacar), 3) = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = ArrDbl((NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_valcar) / NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_qtacar)), 2)
        Else
          If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
            strUbicaz = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i)!tt_ubicaz)
          Else
            lLotto = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_lotto)
          End If
          'controllo i valori sul record successivo o sul primo record di griglia dtrTmp
          If i < dsGrid.Tables("TTLOTTI").Rows.Count - 1 Then
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(i + 1)
            If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
              strUbicaz1 = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_ubicaz)
            Else
              lLotto1 = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_lotto)
            End If
          Else
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(0)
            If Not ArrDbl(NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_qtacar), 3) = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = ArrDbl((NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_qtacar)), 2)
          End If
          ' lotti e commesse
          If (Not (oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13)) And (lLotto <> lLotto1) And (NTSCDec(dtrTmp!tt_qtacar) <> 0) Then

            oCleSche.GrloGetCostoMedio(dtrTmp, lLotto, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Valsaldo), 2) Else dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = IntSetNum("0,00")
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = IntSetNum("0,00")
          End If
          ' ubicazioni
          If ((oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13)) And (strUbicaz <> strUbicaz1) And (NTSCDec(dtrTmp!tt_qtacar) <> 0) Then

            oCleSche.GrloGetCostoMedio2(dtrTmp, strUbicaz, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Valsaldo), 2) Else dsGrid.Tables("TTLOTTI").Rows(i)!xx_costomedio = IntSetNum("0,00")
          End If
        End If
      Next

      For i = 0 To dsGrid.Tables("TTLOTTI").Rows.Count - 1
        If (oCleSche.nScarOrdin = 5) Or (oCleSche.nScarOrdin = 8) Or (oCleSche.nScarOrdin = 14) Then
          If Not ArrDbl(NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_qtacar), 3) = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = (NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_valcar) / NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_qtacar)) * NTSCDec(dsGrid.Tables("TTLOTTI").Rows(i)!tt_quant)
        Else
          If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
            strUbicaz = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i)!tt_ubicaz)
          Else
            lLotto = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i)!tt_lotto)
          End If
          'controllo i valori sul record successivo o sul primo record di griglia dtrTmp
          If i < dsGrid.Tables("TTLOTTI").Rows.Count - 1 Then
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(i + 1)
            If oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13 Then
              strUbicaz1 = NTSCStr(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_ubicaz)
            Else
              lLotto1 = NTSCInt(dsGrid.Tables("TTLOTTI").Rows(i + 1)!tt_lotto)
            End If
          Else
            dtrTmp = dsGrid.Tables("TTLOTTI").Rows(0)
            If Not ArrDbl(NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_qtacar), 3) = 0 Then dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = ArrDbl((NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_qtacar)) * (NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_quant) * NTSCDec(dsGrid.Tables("TTLOTTI").Rows(0)!tt_carscar)), 2)
          End If
          ' lotti e commesse
          If (Not (oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13)) And (lLotto <> lLotto1) And (NTSCDec(dtrTmp!tt_qtacar) <> 0) Then

            oCleSche.GrloGetValLotto(dtrTmp, lLotto, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then
              If NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar) <> 0 Then
                If (NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar)) * NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quant) > 0 Then
                  dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = (NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar)) * NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quant)
                Else
                  dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
                End If
              Else
                dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
              End If
            Else
              dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
            End If
          Else
            dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
          End If
          ' ubicazioni
          If ((oCleSche.nScarOrdin = 12 Or oCleSche.nScarOrdin = 13)) And (strUbicaz <> strUbicaz1) And (NTSCDec(dtrTmp!tt_qtacar) <> 0) Then

            oCleSche.GrloGetValLotto2(dtrTmp, strUbicaz, dsTmp)

            If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then
              If NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar) <> 0 Then 'per evitare err tentativo di divisione per 0
                If (NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar)) * NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quant) > 0 Then
                  dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = (NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!tt_qtacar)) * NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quant)
                Else
                  dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
                End If
              Else
                dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
              End If
            Else
              dsGrid.Tables("TTLOTTI").Rows(i)!xx_vallotto = "0"
            End If
          End If
        End If
      Next
      dsGrid.Tables("TTLOTTI").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub TotCostoUnitario()
    Dim dTot As Decimal
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp1 As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing
    Dim i As Integer
    Try
      '----------------------------------------------------------------------------------------
      edPrezzounitario.Text = "0"
      edValPrezzounitario.Text = "0"
      '----------------------------------------------------------------------------------------
      Select Case oCleSche.nScarOrdin
        '--------------------------------------------------------------------------------------
        '--- LOTTI/COMMESSE APERTI E CHIUSI - A LOTTI/COMMESSE APERTI
        '--------------------------------------------------------------------------------------
        Case 3, 4, 6, 7
          oCleSche.GrloTotCostoUnitario(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsTmp)

          If dsTmp.Tables("TTLOTTI").Rows.Count > 0 Then
            If ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Qta1), 3) = 0 Then
              edPrezzounitario.Text = "0"
            Else
              edPrezzounitario.Text = NTSCStr(ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Tot1) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Qta1), 2))
            End If
            edValPrezzounitario.Text = NTSCStr(ArrDbl((NTSCDec(edTotarticolo.Text) * NTSCDec(edPrezzounitario.Text)), 2))
          Else
            edPrezzounitario.Text = "0"
            edValPrezzounitario.Text = "0"
          End If
          '--------------------------------------------------------------------------------------
          '--- SALDO LOTTI/COMMESSE APERTI saldo ubicazioni
          '--------------------------------------------------------------------------------------
        Case 5, 8, 14
          oCleSche.GrloTotCostoUnitario2(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsTmp)

          If dsTmp.Tables("TTLOTTI").Rows.Count > 0 Then
            If Not ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Qtacar), 3) = 0 Then
              edPrezzounitario.Text = NTSCStr(ArrDbl((NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Qtacar)), 2))
              edValPrezzounitario.Text = Format(ArrDbl(((NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Valcar) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Qtacar)) * NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quant)), 2))
            End If
          Else
            edPrezzounitario.Text = "0"
            edValPrezzounitario.Text = "0"
          End If
          '--------------------------------------------------------------------------------------
          '--- Ubicazioni aperte/chiuse e ubicazioni aperte
          '--------------------------------------------------------------------------------------
        Case 12, 13
          oCleSche.GrloTotCostoUnitario3(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsTmp, dsTmp1)

          If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then
            dTot = NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Valoreun)
            For i = 0 To dsTmp1.Tables("TTLOTTI").Rows.Count - 1

              oCleSche.GrloTotCostoUnitario4(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsTmp1.Tables("TTLOTTI").Rows(i), dsTmp2)

              If Not dsTmp2.Tables("TTLOTTI").Rows.Count = 0 Then
                dTot = dTot - NTSCDec(dsTmp2.Tables("TTLOTTI").Rows(0)!somma)
              End If
            Next
            If NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quantita) <> 0 Then
              edPrezzounitario.Text = NTSCStr(ArrDbl(NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!PREZZOUN) / NTSCDec(dsTmp.Tables("TTLOTTI").Rows(0)!Quantita), 2))
            Else
              edPrezzounitario.Text = "0"
            End If
            edValPrezzounitario.Text = NTSCStr(dTot)
          Else
            edPrezzounitario.Text = "0"
            edValPrezzounitario.Text = "0"
          End If
      End Select

      '----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub TotRimanenzeArticoli()
    Dim dsTmp As DataSet = Nothing
    Try
      edTotarticolo.Text = IntSetNum("0,000")

      oCleSche.GrloTotRimanenzeArticoli(dsGrlo.Tables("TTLOTTI").Rows(dcGrlo.Position), dsTmp)

      If Not dsTmp.Tables("TTLOTTI").Rows.Count = 0 Then
        edTotarticolo.Text = NTSCStr(dsTmp.Tables("TTLOTTI").Rows(0)!Somma)
      Else
        edTotarticolo.Text = IntSetNum("0,000")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
