Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRMA
  'MAI VISUALIZZATA se non abilitato da gestione sicurezza

#Region "Variabili"
  Public oCleSche As CLEMGSCHE
  Public oCallParams As CLE__CLDP
  Public dsGrma As DataSet
  Public dcGrma As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bNoModal As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrma As NTSInformatica.NTSGrid
  Public WithEvents grvGrma As NTSInformatica.NTSGridView
  Public WithEvents pnGrid As NTSInformatica.NTSPanel

  Public WithEvents km_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents km_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents km_causale As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents xx_clfor As NTSInformatica.NTSGridColumn
  Public WithEvents xx_scarichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_esistenza As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents mm_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prelist As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents mm_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_controp As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codnomc As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_perqta As NTSInformatica.NTSGridColumn

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
  Public WithEvents edTotvscarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotvscarichi As NTSInformatica.NTSLabel
  Public WithEvents edTotscarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotscarichi As NTSInformatica.NTSLabel
  Public WithEvents lbEsistfi As NTSInformatica.NTSLabel
  Public WithEvents edEsistfi As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents edMatricola As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbMatricola As NTSInformatica.NTSLabel
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEsistpr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEsistpr As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conto As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents km_aammgg As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGRMA))
    Me.grGrma = New NTSInformatica.NTSGrid
    Me.grvGrma = New NTSInformatica.NTSGridView
    Me.km_aammgg = New NTSInformatica.NTSGridColumn
    Me.km_tipork = New NTSInformatica.NTSGridColumn
    Me.km_numdoc = New NTSInformatica.NTSGridColumn
    Me.km_causale = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.xx_clfor = New NTSInformatica.NTSGridColumn
    Me.xx_scarichi = New NTSInformatica.NTSGridColumn
    Me.xx_esistenza = New NTSInformatica.NTSGridColumn
    Me.xx_prezzo = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.mm_ornum = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.mm_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_scont1 = New NTSInformatica.NTSGridColumn
    Me.mm_scont2 = New NTSInformatica.NTSGridColumn
    Me.mm_scont3 = New NTSInformatica.NTSGridColumn
    Me.mm_prelist = New NTSInformatica.NTSGridColumn
    Me.mm_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mm_colli = New NTSInformatica.NTSGridColumn
    Me.mm_misura1 = New NTSInformatica.NTSGridColumn
    Me.mm_misura2 = New NTSInformatica.NTSGridColumn
    Me.mm_misura3 = New NTSInformatica.NTSGridColumn
    Me.mm_controp = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_codcena = New NTSInformatica.NTSGridColumn
    Me.mm_codcfam = New NTSInformatica.NTSGridColumn
    Me.mm_codnomc = New NTSInformatica.NTSGridColumn
    Me.mm_provv = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv = New NTSInformatica.NTSGridColumn
    Me.mm_provv2 = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.mm_perqta = New NTSInformatica.NTSGridColumn
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.edTotvscarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotvscarichi = New NTSInformatica.NTSLabel
    Me.edTotscarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotscarichi = New NTSInformatica.NTSLabel
    Me.lbEsistfi = New NTSInformatica.NTSLabel
    Me.edEsistfi = New NTSInformatica.NTSTextBoxNum
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbXx_conto = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbEsistpr = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.edEsistpr = New NTSInformatica.NTSTextBoxNum
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbData = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edMatricola = New NTSInformatica.NTSTextBoxStr
    Me.lbMatricola = New NTSInformatica.NTSLabel
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    CType(Me.grGrma, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrma, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.edTotvscarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotscarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsistfi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsistpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edMatricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grGrma
    '
    Me.grGrma.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grGrma.EmbeddedNavigator.Name = ""
    Me.grGrma.Location = New System.Drawing.Point(0, 0)
    Me.grGrma.MainView = Me.grvGrma
    Me.grGrma.Name = "grGrma"
    Me.grGrma.Size = New System.Drawing.Size(660, 292)
    Me.grGrma.TabIndex = 5
    Me.grGrma.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrma})
    '
    'grvGrma
    '
    Me.grvGrma.ActiveFilterEnabled = False
    Me.grvGrma.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.km_aammgg, Me.km_tipork, Me.km_numdoc, Me.km_causale, Me.xx_descr, Me.tm_riferim, Me.xx_clfor, Me.xx_scarichi, Me.xx_esistenza, Me.xx_prezzo, Me.xx_valore, Me.mm_ornum, Me.xx_lottox, Me.mm_prezzo, Me.mm_scont1, Me.mm_scont2, Me.mm_scont3, Me.mm_prelist, Me.mm_prezvalc, Me.mm_colli, Me.mm_misura1, Me.mm_misura2, Me.mm_misura3, Me.mm_controp, Me.mm_commeca, Me.mm_codcena, Me.mm_codcfam, Me.mm_codnomc, Me.mm_provv, Me.mm_vprovv, Me.mm_provv2, Me.mm_vprovv2, Me.mm_perqta})
    Me.grvGrma.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrma.Enabled = True
    Me.grvGrma.GridControl = Me.grGrma
    Me.grvGrma.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrma.MinRowHeight = 14
    Me.grvGrma.Name = "grvGrma"
    Me.grvGrma.NTSAllowDelete = True
    Me.grvGrma.NTSAllowInsert = True
    Me.grvGrma.NTSAllowUpdate = True
    Me.grvGrma.NTSMenuContext = Nothing
    Me.grvGrma.OptionsCustomization.AllowRowSizing = True
    Me.grvGrma.OptionsFilter.AllowFilterEditor = False
    Me.grvGrma.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrma.OptionsNavigation.UseTabKey = False
    Me.grvGrma.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrma.OptionsView.ColumnAutoWidth = False
    Me.grvGrma.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrma.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrma.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrma.OptionsView.ShowGroupPanel = False
    Me.grvGrma.RowHeight = 16
    '
    'km_aammgg
    '
    Me.km_aammgg.AppearanceCell.Options.UseBackColor = True
    Me.km_aammgg.AppearanceCell.Options.UseTextOptions = True
    Me.km_aammgg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_aammgg.Caption = "Data"
    Me.km_aammgg.Enabled = True
    Me.km_aammgg.FieldName = "km_aammgg"
    Me.km_aammgg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_aammgg.Name = "km_aammgg"
    Me.km_aammgg.NTSRepositoryComboBox = Nothing
    Me.km_aammgg.NTSRepositoryItemCheck = Nothing
    Me.km_aammgg.NTSRepositoryItemMemo = Nothing
    Me.km_aammgg.NTSRepositoryItemText = Nothing
    Me.km_aammgg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_aammgg.OptionsFilter.AllowFilter = False
    Me.km_aammgg.Visible = True
    Me.km_aammgg.VisibleIndex = 0
    '
    'km_tipork
    '
    Me.km_tipork.AppearanceCell.Options.UseBackColor = True
    Me.km_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.km_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_tipork.Caption = "Tipo"
    Me.km_tipork.Enabled = True
    Me.km_tipork.FieldName = "km_tipork"
    Me.km_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_tipork.Name = "km_tipork"
    Me.km_tipork.NTSRepositoryComboBox = Nothing
    Me.km_tipork.NTSRepositoryItemCheck = Nothing
    Me.km_tipork.NTSRepositoryItemMemo = Nothing
    Me.km_tipork.NTSRepositoryItemText = Nothing
    Me.km_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_tipork.OptionsFilter.AllowFilter = False
    Me.km_tipork.Visible = True
    Me.km_tipork.VisibleIndex = 1
    '
    'km_numdoc
    '
    Me.km_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.km_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.km_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_numdoc.Caption = "Numero"
    Me.km_numdoc.Enabled = True
    Me.km_numdoc.FieldName = "km_numdoc"
    Me.km_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_numdoc.Name = "km_numdoc"
    Me.km_numdoc.NTSRepositoryComboBox = Nothing
    Me.km_numdoc.NTSRepositoryItemCheck = Nothing
    Me.km_numdoc.NTSRepositoryItemMemo = Nothing
    Me.km_numdoc.NTSRepositoryItemText = Nothing
    Me.km_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_numdoc.OptionsFilter.AllowFilter = False
    Me.km_numdoc.Visible = True
    Me.km_numdoc.VisibleIndex = 2
    '
    'km_causale
    '
    Me.km_causale.AppearanceCell.Options.UseBackColor = True
    Me.km_causale.AppearanceCell.Options.UseTextOptions = True
    Me.km_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_causale.Caption = "Caus."
    Me.km_causale.Enabled = True
    Me.km_causale.FieldName = "km_causale"
    Me.km_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_causale.Name = "km_causale"
    Me.km_causale.NTSRepositoryComboBox = Nothing
    Me.km_causale.NTSRepositoryItemCheck = Nothing
    Me.km_causale.NTSRepositoryItemMemo = Nothing
    Me.km_causale.NTSRepositoryItemText = Nothing
    Me.km_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_causale.OptionsFilter.AllowFilter = False
    Me.km_causale.Visible = True
    Me.km_causale.VisibleIndex = 3
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descr."
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
    Me.xx_descr.VisibleIndex = 4
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Rifer."
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 5
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
    Me.xx_clfor.VisibleIndex = 6
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
    Me.xx_scarichi.VisibleIndex = 7
    '
    'xx_esistenza
    '
    Me.xx_esistenza.AppearanceCell.Options.UseBackColor = True
    Me.xx_esistenza.AppearanceCell.Options.UseTextOptions = True
    Me.xx_esistenza.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_esistenza.Caption = "Esistenza"
    Me.xx_esistenza.Enabled = True
    Me.xx_esistenza.FieldName = "xx_esistenza"
    Me.xx_esistenza.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_esistenza.Name = "xx_esistenza"
    Me.xx_esistenza.NTSRepositoryComboBox = Nothing
    Me.xx_esistenza.NTSRepositoryItemCheck = Nothing
    Me.xx_esistenza.NTSRepositoryItemMemo = Nothing
    Me.xx_esistenza.NTSRepositoryItemText = Nothing
    Me.xx_esistenza.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_esistenza.OptionsFilter.AllowFilter = False
    Me.xx_esistenza.Visible = True
    Me.xx_esistenza.VisibleIndex = 8
    '
    'xx_prezzo
    '
    Me.xx_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzo.Caption = "Prezzo N"
    Me.xx_prezzo.Enabled = True
    Me.xx_prezzo.FieldName = "xx_prezzo"
    Me.xx_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzo.Name = "xx_prezzo"
    Me.xx_prezzo.NTSRepositoryComboBox = Nothing
    Me.xx_prezzo.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzo.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzo.NTSRepositoryItemText = Nothing
    Me.xx_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzo.OptionsFilter.AllowFilter = False
    Me.xx_prezzo.Visible = True
    Me.xx_prezzo.VisibleIndex = 9
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
    'mm_ornum
    '
    Me.mm_ornum.AppearanceCell.Options.UseBackColor = True
    Me.mm_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ornum.Caption = "N.Ord."
    Me.mm_ornum.Enabled = True
    Me.mm_ornum.FieldName = "mm_ornum"
    Me.mm_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ornum.Name = "mm_ornum"
    Me.mm_ornum.NTSRepositoryComboBox = Nothing
    Me.mm_ornum.NTSRepositoryItemCheck = Nothing
    Me.mm_ornum.NTSRepositoryItemMemo = Nothing
    Me.mm_ornum.NTSRepositoryItemText = Nothing
    Me.mm_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ornum.OptionsFilter.AllowFilter = False
    Me.mm_ornum.Visible = True
    Me.mm_ornum.VisibleIndex = 11
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
    Me.xx_lottox.VisibleIndex = 12
    '
    'mm_prezzo
    '
    Me.mm_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezzo.Caption = "Prezzo Lordo"
    Me.mm_prezzo.Enabled = True
    Me.mm_prezzo.FieldName = "mm_prezzo"
    Me.mm_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezzo.Name = "mm_prezzo"
    Me.mm_prezzo.NTSRepositoryComboBox = Nothing
    Me.mm_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mm_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mm_prezzo.NTSRepositoryItemText = Nothing
    Me.mm_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezzo.OptionsFilter.AllowFilter = False
    Me.mm_prezzo.Visible = True
    Me.mm_prezzo.VisibleIndex = 13
    '
    'mm_scont1
    '
    Me.mm_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont1.Caption = "Sc.1"
    Me.mm_scont1.Enabled = True
    Me.mm_scont1.FieldName = "mm_scont1"
    Me.mm_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont1.Name = "mm_scont1"
    Me.mm_scont1.NTSRepositoryComboBox = Nothing
    Me.mm_scont1.NTSRepositoryItemCheck = Nothing
    Me.mm_scont1.NTSRepositoryItemMemo = Nothing
    Me.mm_scont1.NTSRepositoryItemText = Nothing
    Me.mm_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont1.OptionsFilter.AllowFilter = False
    Me.mm_scont1.Visible = True
    Me.mm_scont1.VisibleIndex = 14
    '
    'mm_scont2
    '
    Me.mm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont2.Caption = "Sc. 2"
    Me.mm_scont2.Enabled = True
    Me.mm_scont2.FieldName = "mm_scont2"
    Me.mm_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont2.Name = "mm_scont2"
    Me.mm_scont2.NTSRepositoryComboBox = Nothing
    Me.mm_scont2.NTSRepositoryItemCheck = Nothing
    Me.mm_scont2.NTSRepositoryItemMemo = Nothing
    Me.mm_scont2.NTSRepositoryItemText = Nothing
    Me.mm_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont2.OptionsFilter.AllowFilter = False
    Me.mm_scont2.Visible = True
    Me.mm_scont2.VisibleIndex = 15
    '
    'mm_scont3
    '
    Me.mm_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont3.Caption = "Sc.3"
    Me.mm_scont3.Enabled = True
    Me.mm_scont3.FieldName = "mm_scont3"
    Me.mm_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont3.Name = "mm_scont3"
    Me.mm_scont3.NTSRepositoryComboBox = Nothing
    Me.mm_scont3.NTSRepositoryItemCheck = Nothing
    Me.mm_scont3.NTSRepositoryItemMemo = Nothing
    Me.mm_scont3.NTSRepositoryItemText = Nothing
    Me.mm_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont3.OptionsFilter.AllowFilter = False
    Me.mm_scont3.Visible = True
    Me.mm_scont3.VisibleIndex = 16
    '
    'mm_prelist
    '
    Me.mm_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mm_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prelist.Caption = "Prz.List."
    Me.mm_prelist.Enabled = True
    Me.mm_prelist.FieldName = "mm_prelist"
    Me.mm_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prelist.Name = "mm_prelist"
    Me.mm_prelist.NTSRepositoryComboBox = Nothing
    Me.mm_prelist.NTSRepositoryItemCheck = Nothing
    Me.mm_prelist.NTSRepositoryItemMemo = Nothing
    Me.mm_prelist.NTSRepositoryItemText = Nothing
    Me.mm_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prelist.OptionsFilter.AllowFilter = False
    Me.mm_prelist.Visible = True
    Me.mm_prelist.VisibleIndex = 17
    '
    'mm_prezvalc
    '
    Me.mm_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezvalc.Caption = "Prz.Val."
    Me.mm_prezvalc.Enabled = True
    Me.mm_prezvalc.FieldName = "mm_prezvalc"
    Me.mm_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezvalc.Name = "mm_prezvalc"
    Me.mm_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mm_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mm_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mm_prezvalc.NTSRepositoryItemText = Nothing
    Me.mm_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezvalc.OptionsFilter.AllowFilter = False
    Me.mm_prezvalc.Visible = True
    Me.mm_prezvalc.VisibleIndex = 18
    '
    'mm_colli
    '
    Me.mm_colli.AppearanceCell.Options.UseBackColor = True
    Me.mm_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mm_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_colli.Caption = "Colli"
    Me.mm_colli.Enabled = True
    Me.mm_colli.FieldName = "mm_colli"
    Me.mm_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_colli.Name = "mm_colli"
    Me.mm_colli.NTSRepositoryComboBox = Nothing
    Me.mm_colli.NTSRepositoryItemCheck = Nothing
    Me.mm_colli.NTSRepositoryItemMemo = Nothing
    Me.mm_colli.NTSRepositoryItemText = Nothing
    Me.mm_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_colli.OptionsFilter.AllowFilter = False
    Me.mm_colli.Visible = True
    Me.mm_colli.VisibleIndex = 19
    '
    'mm_misura1
    '
    Me.mm_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura1.Caption = "Mis.1"
    Me.mm_misura1.Enabled = True
    Me.mm_misura1.FieldName = "mm_misura1"
    Me.mm_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura1.Name = "mm_misura1"
    Me.mm_misura1.NTSRepositoryComboBox = Nothing
    Me.mm_misura1.NTSRepositoryItemCheck = Nothing
    Me.mm_misura1.NTSRepositoryItemMemo = Nothing
    Me.mm_misura1.NTSRepositoryItemText = Nothing
    Me.mm_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura1.OptionsFilter.AllowFilter = False
    '
    'mm_misura2
    '
    Me.mm_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura2.Caption = "Mis.2"
    Me.mm_misura2.Enabled = True
    Me.mm_misura2.FieldName = "mm_misura2"
    Me.mm_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura2.Name = "mm_misura2"
    Me.mm_misura2.NTSRepositoryComboBox = Nothing
    Me.mm_misura2.NTSRepositoryItemCheck = Nothing
    Me.mm_misura2.NTSRepositoryItemMemo = Nothing
    Me.mm_misura2.NTSRepositoryItemText = Nothing
    Me.mm_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura2.OptionsFilter.AllowFilter = False
    '
    'mm_misura3
    '
    Me.mm_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura3.Caption = "Mis.3"
    Me.mm_misura3.Enabled = True
    Me.mm_misura3.FieldName = "mm_misura3"
    Me.mm_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura3.Name = "mm_misura3"
    Me.mm_misura3.NTSRepositoryComboBox = Nothing
    Me.mm_misura3.NTSRepositoryItemCheck = Nothing
    Me.mm_misura3.NTSRepositoryItemMemo = Nothing
    Me.mm_misura3.NTSRepositoryItemText = Nothing
    Me.mm_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura3.OptionsFilter.AllowFilter = False
    '
    'mm_controp
    '
    Me.mm_controp.AppearanceCell.Options.UseBackColor = True
    Me.mm_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mm_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_controp.Caption = "Controp."
    Me.mm_controp.Enabled = True
    Me.mm_controp.FieldName = "mm_controp"
    Me.mm_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_controp.Name = "mm_controp"
    Me.mm_controp.NTSRepositoryComboBox = Nothing
    Me.mm_controp.NTSRepositoryItemCheck = Nothing
    Me.mm_controp.NTSRepositoryItemMemo = Nothing
    Me.mm_controp.NTSRepositoryItemText = Nothing
    Me.mm_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_controp.OptionsFilter.AllowFilter = False
    Me.mm_controp.Visible = True
    Me.mm_controp.VisibleIndex = 20
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
    Me.mm_commeca.VisibleIndex = 21
    '
    'mm_codcena
    '
    Me.mm_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcena.Caption = "C.Centro"
    Me.mm_codcena.Enabled = True
    Me.mm_codcena.FieldName = "mm_codcena"
    Me.mm_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcena.Name = "mm_codcena"
    Me.mm_codcena.NTSRepositoryComboBox = Nothing
    Me.mm_codcena.NTSRepositoryItemCheck = Nothing
    Me.mm_codcena.NTSRepositoryItemMemo = Nothing
    Me.mm_codcena.NTSRepositoryItemText = Nothing
    Me.mm_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcena.OptionsFilter.AllowFilter = False
    Me.mm_codcena.Visible = True
    Me.mm_codcena.VisibleIndex = 22
    '
    'mm_codcfam
    '
    Me.mm_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcfam.Caption = "Linea"
    Me.mm_codcfam.Enabled = True
    Me.mm_codcfam.FieldName = "mm_codcfam"
    Me.mm_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcfam.Name = "mm_codcfam"
    Me.mm_codcfam.NTSRepositoryComboBox = Nothing
    Me.mm_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mm_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mm_codcfam.NTSRepositoryItemText = Nothing
    Me.mm_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcfam.OptionsFilter.AllowFilter = False
    Me.mm_codcfam.Visible = True
    Me.mm_codcfam.VisibleIndex = 23
    '
    'mm_codnomc
    '
    Me.mm_codnomc.AppearanceCell.Options.UseBackColor = True
    Me.mm_codnomc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codnomc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codnomc.Caption = "Nom.Comb."
    Me.mm_codnomc.Enabled = True
    Me.mm_codnomc.FieldName = "mm_codnomc"
    Me.mm_codnomc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codnomc.Name = "mm_codnomc"
    Me.mm_codnomc.NTSRepositoryComboBox = Nothing
    Me.mm_codnomc.NTSRepositoryItemCheck = Nothing
    Me.mm_codnomc.NTSRepositoryItemMemo = Nothing
    Me.mm_codnomc.NTSRepositoryItemText = Nothing
    Me.mm_codnomc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codnomc.OptionsFilter.AllowFilter = False
    Me.mm_codnomc.Visible = True
    Me.mm_codnomc.VisibleIndex = 24
    '
    'mm_provv
    '
    Me.mm_provv.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv.Caption = "Provv.1"
    Me.mm_provv.Enabled = True
    Me.mm_provv.FieldName = "mm_provv"
    Me.mm_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv.Name = "mm_provv"
    Me.mm_provv.NTSRepositoryComboBox = Nothing
    Me.mm_provv.NTSRepositoryItemCheck = Nothing
    Me.mm_provv.NTSRepositoryItemMemo = Nothing
    Me.mm_provv.NTSRepositoryItemText = Nothing
    Me.mm_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv.OptionsFilter.AllowFilter = False
    Me.mm_provv.Visible = True
    Me.mm_provv.VisibleIndex = 25
    '
    'mm_vprovv
    '
    Me.mm_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv.Caption = "Val.Pr.1"
    Me.mm_vprovv.Enabled = True
    Me.mm_vprovv.FieldName = "mm_vprovv"
    Me.mm_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv.Name = "mm_vprovv"
    Me.mm_vprovv.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv.NTSRepositoryItemText = Nothing
    Me.mm_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv.OptionsFilter.AllowFilter = False
    Me.mm_vprovv.Visible = True
    Me.mm_vprovv.VisibleIndex = 26
    '
    'mm_provv2
    '
    Me.mm_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv2.Caption = "Provv.2"
    Me.mm_provv2.Enabled = True
    Me.mm_provv2.FieldName = "mm_provv2"
    Me.mm_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv2.Name = "mm_provv2"
    Me.mm_provv2.NTSRepositoryComboBox = Nothing
    Me.mm_provv2.NTSRepositoryItemCheck = Nothing
    Me.mm_provv2.NTSRepositoryItemMemo = Nothing
    Me.mm_provv2.NTSRepositoryItemText = Nothing
    Me.mm_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv2.OptionsFilter.AllowFilter = False
    Me.mm_provv2.Visible = True
    Me.mm_provv2.VisibleIndex = 27
    '
    'mm_vprovv2
    '
    Me.mm_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv2.Caption = "Val.Pr.2"
    Me.mm_vprovv2.Enabled = True
    Me.mm_vprovv2.FieldName = "mm_vprovv2"
    Me.mm_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv2.Name = "mm_vprovv2"
    Me.mm_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv2.NTSRepositoryItemText = Nothing
    Me.mm_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv2.OptionsFilter.AllowFilter = False
    Me.mm_vprovv2.Visible = True
    Me.mm_vprovv2.VisibleIndex = 28
    '
    'mm_perqta
    '
    Me.mm_perqta.AppearanceCell.Options.UseBackColor = True
    Me.mm_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_perqta.Caption = "Molt.qtà/prezzo"
    Me.mm_perqta.Enabled = True
    Me.mm_perqta.FieldName = "mm_perqta"
    Me.mm_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_perqta.Name = "mm_perqta"
    Me.mm_perqta.NTSRepositoryComboBox = Nothing
    Me.mm_perqta.NTSRepositoryItemCheck = Nothing
    Me.mm_perqta.NTSRepositoryItemMemo = Nothing
    Me.mm_perqta.NTSRepositoryItemText = Nothing
    Me.mm_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_perqta.OptionsFilter.AllowFilter = False
    Me.mm_perqta.Visible = True
    Me.mm_perqta.VisibleIndex = 29
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrma)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 114)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 292)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbImpostaStampante, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 14
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.pnBottom.Controls.Add(Me.edTotvscarichi)
    Me.pnBottom.Controls.Add(Me.lbTotvscarichi)
    Me.pnBottom.Controls.Add(Me.edTotscarichi)
    Me.pnBottom.Controls.Add(Me.lbTotscarichi)
    Me.pnBottom.Controls.Add(Me.lbEsistfi)
    Me.pnBottom.Controls.Add(Me.edEsistfi)
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
    'edTotvscarichi
    '
    Me.edTotvscarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotvscarichi.Location = New System.Drawing.Point(498, 6)
    Me.edTotvscarichi.Name = "edTotvscarichi"
    Me.edTotvscarichi.NTSDbField = ""
    Me.edTotvscarichi.NTSFormat = "0"
    Me.edTotvscarichi.NTSForzaVisZoom = False
    Me.edTotvscarichi.NTSOldValue = ""
    Me.edTotvscarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotvscarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotvscarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotvscarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotvscarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotvscarichi.Properties.MaxLength = 65536
    Me.edTotvscarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotvscarichi.Size = New System.Drawing.Size(100, 20)
    Me.edTotvscarichi.TabIndex = 53
    '
    'lbTotvscarichi
    '
    Me.lbTotvscarichi.AutoSize = True
    Me.lbTotvscarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotvscarichi.Location = New System.Drawing.Point(424, 9)
    Me.lbTotvscarichi.Name = "lbTotvscarichi"
    Me.lbTotvscarichi.NTSDbField = ""
    Me.lbTotvscarichi.Size = New System.Drawing.Size(61, 13)
    Me.lbTotvscarichi.TabIndex = 52
    Me.lbTotvscarichi.Text = "Val.Scarichi"
    Me.lbTotvscarichi.Tooltip = ""
    Me.lbTotvscarichi.UseMnemonic = False
    '
    'edTotscarichi
    '
    Me.edTotscarichi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTotscarichi.Location = New System.Drawing.Point(297, 6)
    Me.edTotscarichi.Name = "edTotscarichi"
    Me.edTotscarichi.NTSDbField = ""
    Me.edTotscarichi.NTSFormat = "0"
    Me.edTotscarichi.NTSForzaVisZoom = False
    Me.edTotscarichi.NTSOldValue = ""
    Me.edTotscarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotscarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotscarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotscarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotscarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotscarichi.Properties.MaxLength = 65536
    Me.edTotscarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotscarichi.Size = New System.Drawing.Size(100, 20)
    Me.edTotscarichi.TabIndex = 51
    '
    'lbTotscarichi
    '
    Me.lbTotscarichi.AutoSize = True
    Me.lbTotscarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotscarichi.Location = New System.Drawing.Point(221, 9)
    Me.lbTotscarichi.Name = "lbTotscarichi"
    Me.lbTotscarichi.NTSDbField = ""
    Me.lbTotscarichi.Size = New System.Drawing.Size(62, 13)
    Me.lbTotscarichi.TabIndex = 50
    Me.lbTotscarichi.Text = "Tot Scarichi"
    Me.lbTotscarichi.Tooltip = ""
    Me.lbTotscarichi.UseMnemonic = False
    '
    'lbEsistfi
    '
    Me.lbEsistfi.AutoSize = True
    Me.lbEsistfi.BackColor = System.Drawing.Color.Transparent
    Me.lbEsistfi.Location = New System.Drawing.Point(6, 9)
    Me.lbEsistfi.Name = "lbEsistfi"
    Me.lbEsistfi.NTSDbField = ""
    Me.lbEsistfi.Size = New System.Drawing.Size(81, 13)
    Me.lbEsistfi.TabIndex = 48
    Me.lbEsistfi.Text = "Saldo Esistenza"
    Me.lbEsistfi.Tooltip = ""
    Me.lbEsistfi.UseMnemonic = False
    '
    'edEsistfi
    '
    Me.edEsistfi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsistfi.Location = New System.Drawing.Point(93, 6)
    Me.edEsistfi.Name = "edEsistfi"
    Me.edEsistfi.NTSDbField = ""
    Me.edEsistfi.NTSFormat = "0"
    Me.edEsistfi.NTSForzaVisZoom = False
    Me.edEsistfi.NTSOldValue = ""
    Me.edEsistfi.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsistfi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsistfi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsistfi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsistfi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsistfi.Properties.MaxLength = 65536
    Me.edEsistfi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsistfi.Size = New System.Drawing.Size(100, 20)
    Me.edEsistfi.TabIndex = 49
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(10, 61)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(36, 13)
    Me.lbConto.TabIndex = 61
    Me.lbConto.Text = "Conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbXx_conto
    '
    Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conto.Location = New System.Drawing.Point(142, 60)
    Me.lbXx_conto.Name = "lbXx_conto"
    Me.lbXx_conto.NTSDbField = ""
    Me.lbXx_conto.Size = New System.Drawing.Size(151, 20)
    Me.lbXx_conto.TabIndex = 62
    Me.lbXx_conto.Tooltip = ""
    Me.lbXx_conto.UseMnemonic = False
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
    Me.lbMagaz.Location = New System.Drawing.Point(299, 34)
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
    Me.lbFase.Location = New System.Drawing.Point(198, 34)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(34, 13)
    Me.lbFase.TabIndex = 65
    Me.lbFase.Text = "Fase:"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbEsistpr
    '
    Me.lbEsistpr.AutoSize = True
    Me.lbEsistpr.BackColor = System.Drawing.Color.Transparent
    Me.lbEsistpr.Location = New System.Drawing.Point(399, 34)
    Me.lbEsistpr.Name = "lbEsistpr"
    Me.lbEsistpr.NTSDbField = ""
    Me.lbEsistpr.Size = New System.Drawing.Size(75, 13)
    Me.lbEsistpr.TabIndex = 66
    Me.lbEsistpr.Text = "Esist. Attuale:"
    Me.lbEsistpr.Tooltip = ""
    Me.lbEsistpr.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(62, 58)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(74, 20)
    Me.edConto.TabIndex = 67
    '
    'edEsistpr
    '
    Me.edEsistpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsistpr.Location = New System.Drawing.Point(472, 31)
    Me.edEsistpr.Name = "edEsistpr"
    Me.edEsistpr.NTSDbField = ""
    Me.edEsistpr.NTSFormat = "0"
    Me.edEsistpr.NTSForzaVisZoom = False
    Me.edEsistpr.NTSOldValue = ""
    Me.edEsistpr.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsistpr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsistpr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsistpr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsistpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsistpr.Properties.MaxLength = 65536
    Me.edEsistpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsistpr.Size = New System.Drawing.Size(75, 20)
    Me.edEsistpr.TabIndex = 68
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
    Me.edFase.Location = New System.Drawing.Point(236, 31)
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
    Me.edMagaz.Location = New System.Drawing.Point(340, 31)
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
    'lbData
    '
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbData.Location = New System.Drawing.Point(456, 8)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(193, 20)
    Me.lbData.TabIndex = 73
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edMatricola)
    Me.pnTop.Controls.Add(Me.lbMatricola)
    Me.pnTop.Controls.Add(Me.lbData)
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.edEsistpr)
    Me.pnTop.Controls.Add(Me.edConto)
    Me.pnTop.Controls.Add(Me.lbEsistpr)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Controls.Add(Me.lbXx_conto)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 84)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'edMatricola
    '
    Me.edMatricola.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatricola.Location = New System.Drawing.Point(62, 31)
    Me.edMatricola.Name = "edMatricola"
    Me.edMatricola.NTSDbField = ""
    Me.edMatricola.NTSForzaVisZoom = False
    Me.edMatricola.NTSOldValue = ""
    Me.edMatricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatricola.Properties.MaxLength = 65536
    Me.edMatricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatricola.Size = New System.Drawing.Size(131, 20)
    Me.edMatricola.TabIndex = 75
    '
    'lbMatricola
    '
    Me.lbMatricola.AutoSize = True
    Me.lbMatricola.BackColor = System.Drawing.Color.Transparent
    Me.lbMatricola.Location = New System.Drawing.Point(10, 34)
    Me.lbMatricola.Name = "lbMatricola"
    Me.lbMatricola.NTSDbField = ""
    Me.lbMatricola.Size = New System.Drawing.Size(54, 13)
    Me.lbMatricola.TabIndex = 74
    Me.lbMatricola.Text = "Matricola:"
    Me.lbMatricola.Tooltip = ""
    Me.lbMatricola.UseMnemonic = False
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa anteprima a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 13
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'FRMMGGRMA
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
    Me.Name = "FRMMGGRMA"
    Me.NTSLastControlFocussed = Me.grGrma
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE ARTICOLI DA NOTE DI PRELIEVO PER MATRICOLE"
    CType(Me.grGrma, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrma, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.edTotvscarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotscarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsistfi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsistpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edMatricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 129055191032964671, "Articolo"), 0)


      grvGrma.NTSSetParam(oMenu, "Stampa/Visualizzazione Schede articoli da Note di prelievo per matricole")

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






      grvGrma.Enabled = False
      grvGrma.NTSAllowInsert = False

      edArticolo.Enabled = False
      edMatricola.Enabled = False
      edFase.Enabled = False
      edMagaz.Enabled = False
      edEsistpr.Enabled = False
      edConto.Enabled = False
      edEsistfi.Enabled = False
      edTotscarichi.Enabled = False
      edTotvscarichi.Enabled = False

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
  Public Overridable Sub FRMMGGRMA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

      lbData.Text = oApp.Tr(Me, 130421130991461652, _
                    "Dal: |" & oCleSche.strScarDatini & "|  Al: |" & oCleSche.strScarDatfin & "|")
      If oCleSche.strScarOrdin = "A" Then
        Saldo()
        GctlSetVisEnab(xx_esistenza, True)
        lbConto.Visible = False
        edConto.Visible = False
        lbXx_conto.Visible = False
      Else
        xx_clfor.Visible = False
        xx_esistenza.Visible = False
        lbEsistpr.Visible = False
        lbEsistfi.Visible = False
        edEsistpr.Visible = False
        edEsistfi.Visible = False
        TotValore()
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

  Public Overridable Sub FRMMGGRMA_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGRMA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then oCleSche.SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGGRMA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrma.Dispose()
      dsGrma.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      dcGrma.MoveFirst()
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
      dcGrma.MovePrevious()
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
      dcGrma.MoveNext()
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
      dcGrma.MoveLast()
      ApriRecordset()
      RiempiLabel()

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
      strHeader = lbArticolo.Text & "".PadLeft(1) & lbArticolo.Text & " " & lbXx_articolo.Text.ToUpper & "".PadLeft(5) & _
        lbMatricola.Text & "".PadLeft(1) & edMatricola.Text & "".PadLeft(5)
      Select Case oCleSche.strScarOrdin
        Case "A"
          strHeader += lbMagaz.Text & "".PadLeft(1) & edMagaz.Text & "".PadLeft(5) & _
            lbEsistpr.Text & "".PadLeft(1) & edEsistpr.Text
          strFooter = lbEsistfi.Text & "".PadLeft(1) & "".PadLeft(5)
        Case Else
          strHeader += lbConto.Text & "".PadLeft(1) & lbXx_conto.Text & "".PadLeft(5) & _
            lbMagaz.Text & "".PadLeft(1) & edMagaz.Text
      End Select
      strFooter += lbTotscarichi.Text & "".PadLeft(1) & edTotscarichi.Text & "".PadLeft(5) & _
        lbTotvscarichi.Text & "".PadLeft(1) & edTotvscarichi.Text
      '--------------------------------------------------------------------------------------------------------------      
      grvGrma.NTSPrintPreview(strHeader, strFooter)
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

      If oCleSche.strScarOrdin = "A" Then Saldo() Else TotValore()
      CaricaColonneUnbound()

      dcGrid.DataSource = dsGrid.Tables("MOVPRB")
      dsGrid.AcceptChanges()

      grGrma.DataSource = dcGrid

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
      If dsGrma.Tables("MOVPRB").Rows.Count = 0 Then Exit Sub

      edMagaz.Text = NTSCStr(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position)!km_magaz)
      edFase.Text = NTSCStr(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position)!km_fase)
      edMatricola.Text = NTSCStr(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position)!mma_matric)

      edArticolo.Text = NTSCStr(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position)!km_codart)
      If Not oCleSche.lbArticolo_Validated(NTSCStr(edArticolo.Text), strTmp, dttTmp) Then
        lbXx_articolo.Text = ""
      Else
        lbXx_articolo.Text = strTmp & " " & NTSCStr(dttTmp.Rows(0)!ar_desint)
      End If

      If oCleSche.strScarOrdin = "C" Then
        edConto.Text = NTSCStr(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position)!km_conto)
        If Not oCleSche.lbConto_Validated(NTSCInt(edConto.Text), strTmp) Then
          lbXx_conto.Text = ""
        Else
          lbXx_conto.Text = strTmp
        End If
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
      oCleSche.GrmaComponiStringa(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position), dsGrid)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleSche.GrmaApri(dsGrma) Then
        bClose = True
        Return False
      End If

      dcGrma.DataSource = dsGrma.Tables("MOVPRB")
      dsGrma.AcceptChanges()

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
    Try
      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        If NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!km_causale) = 0 Then GoTo ProssimaCausale
        If Not oCleSche.GrmaCausale_Validated(NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!km_causale), strDescr) Then
          dsGrid.Tables("MOVPRB").Rows(i)!xx_descr = ""
        Else
          dsGrid.Tables("MOVPRB").Rows(i)!xx_descr = strDescr
        End If
ProssimaCausale:
      Next

      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        If oCleSche.strScarOrdin = "A" Then
          If NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!km_conto) = 0 Then GoTo ProssimoConto
          If Not oCleSche.GrmalbConto_Validated(NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!km_conto), strDescr) Then
            dsGrid.Tables("MOVPRB").Rows(i)!xx_clfor = ""
          Else
            dsGrid.Tables("MOVPRB").Rows(i)!xx_clfor = strDescr
          End If
        End If
ProssimoConto:
      Next

      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        dsGrid.Tables("MOVPRB").Rows(i)!xx_scarichi = NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant)
      Next

      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        If oCleSche.strScarOrdin = "A" Then
          dsGrid.Tables("MOVPRB").Rows(i)!xx_esistenza = oCleSche.dEsist(i)
        End If
      Next

      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        If NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant) = 0 Then
          dsGrid.Tables("MOVPRB").Rows(i)!xx_prezzo = 0
        Else
          dsGrid.Tables("MOVPRB").Rows(i)!xx_prezzo = NTSCDec((NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_valore) / NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_quant)) * NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant) * NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_perqta))
        End If
      Next

      For i = 0 To dsGrid.Tables("MOVPRB").Rows.Count - 1
        If NTSCInt(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant) = 0 Then
          dsGrid.Tables("MOVPRB").Rows(i)!xx_valore = 0
        Else
          dsGrid.Tables("MOVPRB").Rows(i)!xx_valore = NTSCDec((NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_valore) / NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_quant)) * NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant))
        End If
      Next
      dsGrid.Tables("MOVPRB").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Saldo()
    Dim i As Integer
    Dim dValoreScarichi As Decimal
    Dim dScarichi As Decimal
    Dim dsArtdef As DataSet = Nothing
    Try
      ReDim oCleSche.dEsist(NTSCInt(dsGrid.Tables("MOVPRB").Rows.Count))
      '---------------------------------------------------------------
      oCleSche.GrmaGetArtpro(dsGrma.Tables("MOVPRB").Rows(dcGrma.Position), dsArtdef)

      If dsArtdef.Tables("ARTPRO").Rows.Count > 0 Then
        edEsistpr.Text = NTSCStr(dsArtdef.Tables("ARTPRO").Rows(0)!somma)
        oCleSche.dEsist(0) = NTSCDec(dsArtdef.Tables("ARTPRO").Rows(0)!somma) - NTSCDec(dsGrid.Tables("MOVPRB").Rows(0)!mma_quant)
      Else
        edEsistfi.Text = IntSetNum("0,0000")
      End If
      dValoreScarichi = dValoreScarichi + ((NTSCDec(dsGrid.Tables("MOVPRB").Rows(0)!mm_valore) / NTSCDec(dsGrid.Tables("MOVPRB").Rows(0)!mm_quant)) * NTSCDec(dsGrid.Tables("MOVPRB").Rows(0)!mma_quant))
      dScarichi = dScarichi + NTSCDec(dsGrid.Tables("MOVPRB").Rows(0)!mma_quant)
      '---------------------------------------------------------------
      If NTSCInt(dsGrid.Tables("MOVPRB").Rows.Count) = 1 Then
        edEsistfi.Text = NTSCStr(oCleSche.dEsist(0))
        GoTo Fine
      End If
      For i = 1 To (NTSCInt(dsGrid.Tables("MOVPRB").Rows.Count) - 1)
        oCleSche.dEsist(i) = oCleSche.dEsist(i - 1) - NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant)
        dValoreScarichi = dValoreScarichi + ((NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_valore) / NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_quant)) * NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant))
        dScarichi = dScarichi + NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mma_quant)
      Next
      edEsistfi.Text = NTSCStr(oCleSche.dEsist(NTSCInt(dsGrid.Tables("MOVPRB").Rows.Count) - 1))
Fine:
      edTotvscarichi.Text = NTSCStr(dValoreScarichi)
      edTotscarichi.Text = NTSCStr(dScarichi)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub TotValore()
    Dim dValoreScarichi As Decimal
    Dim dScarichi As Decimal
    Dim i As Integer
    Try
      If NTSCInt(dsGrid.Tables("MOVPRB").Rows.Count) = 0 Then Exit Sub
      For i = 0 To dsGrid.Tables("MOVPRB").Rows().Count - 1
        dValoreScarichi = dValoreScarichi + NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_valore)
        dScarichi = dScarichi + (NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_quant) - NTSCDec(dsGrid.Tables("MOVPRB").Rows(i)!mm_nprquaeva))
      Next
      edTotvscarichi.Text = NTSCStr(dValoreScarichi)
      edTotscarichi.Text = NTSCStr(dScarichi)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
