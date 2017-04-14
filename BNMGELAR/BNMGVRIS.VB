Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVRIS
#Region "Variabili"
  Public oCleElar As CLEMGELAR
  Public oCallParams As CLE__CLDP
  Public dcVris As New BindingSource()
  Public dsVris As DataSet = Nothing
  Private components As System.ComponentModel.IContainer

  Public strVrisCodart As String

  Public WithEvents grVris As NTSInformatica.NTSGrid
  Public WithEvents grvVris As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents lc_codart As NTSInformatica.NTSGridColumn
  Public WithEvents lc_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_nuovovalore As NTSInformatica.NTSGridColumn
  Public WithEvents lc_listino As NTSInformatica.NTSGridColumn
  Public WithEvents lc_conto As NTSInformatica.NTSGridColumn
  Public WithEvents lc_datagg As NTSInformatica.NTSGridColumn
  Public WithEvents lc_datscad As NTSInformatica.NTSGridColumn
  Public WithEvents lc_fase As NTSInformatica.NTSGridColumn
  Public WithEvents lc_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents lc_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_daquant As NTSInformatica.NTSGridColumn
  Public WithEvents lc_aquant As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents lc_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents lc_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents lc_note As NTSInformatica.NTSGridColumn
  Public WithEvents lc_netto As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codpdon As NTSInformatica.NTSGridColumn
  Public WithEvents ar_gesvar As NTSInformatica.NTSGridColumn
  Public WithEvents ar_prevar As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codroot As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents ar_conver As NTSInformatica.NTSGridColumn
  Public WithEvents ar_qtacon2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_confez2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis2 As NTSInformatica.NTSGridColumn
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents xx_prezzobase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_percvar As NTSInformatica.NTSGridColumn
  Public WithEvents lc_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
#End Region

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
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVRIS))
    Me.grVris = New NTSInformatica.NTSGrid
    Me.grvVris = New NTSInformatica.NTSGridView
    Me.lc_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.lc_codart = New NTSInformatica.NTSGridColumn
    Me.xx_codart = New NTSInformatica.NTSGridColumn
    Me.lc_prezzo = New NTSInformatica.NTSGridColumn
    Me.xx_nuovovalore = New NTSInformatica.NTSGridColumn
    Me.lc_listino = New NTSInformatica.NTSGridColumn
    Me.lc_datagg = New NTSInformatica.NTSGridColumn
    Me.lc_datscad = New NTSInformatica.NTSGridColumn
    Me.lc_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.lc_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.lc_unmis = New NTSInformatica.NTSGridColumn
    Me.lc_tipo = New NTSInformatica.NTSGridColumn
    Me.lc_daquant = New NTSInformatica.NTSGridColumn
    Me.lc_aquant = New NTSInformatica.NTSGridColumn
    Me.lc_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.lc_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    Me.lc_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_codtpro = New NTSInformatica.NTSGridColumn
    Me.lc_perqta = New NTSInformatica.NTSGridColumn
    Me.lc_note = New NTSInformatica.NTSGridColumn
    Me.lc_netto = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_codpdon = New NTSInformatica.NTSGridColumn
    Me.ar_gesvar = New NTSInformatica.NTSGridColumn
    Me.ar_prevar = New NTSInformatica.NTSGridColumn
    Me.ar_codroot = New NTSInformatica.NTSGridColumn
    Me.ar_codvar1 = New NTSInformatica.NTSGridColumn
    Me.ar_conver = New NTSInformatica.NTSGridColumn
    Me.ar_qtacon2 = New NTSInformatica.NTSGridColumn
    Me.ar_confez2 = New NTSInformatica.NTSGridColumn
    Me.ar_unmis2 = New NTSInformatica.NTSGridColumn
    Me.xx_prezzobase = New NTSInformatica.NTSGridColumn
    Me.xx_percvar = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grVris, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVris, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
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
    'grVris
    '
    Me.grVris.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grVris.EmbeddedNavigator.Name = ""
    Me.grVris.Location = New System.Drawing.Point(0, 30)
    Me.grVris.MainView = Me.grvVris
    Me.grVris.Name = "grVris"
    Me.grVris.Size = New System.Drawing.Size(667, 336)
    Me.grVris.TabIndex = 0
    Me.grVris.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVris})
    '
    'grvVris
    '
    Me.grvVris.ActiveFilterEnabled = False
    '
    'lc_coddest
    '
    Me.lc_coddest.AppearanceCell.Options.UseBackColor = True
    Me.lc_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.lc_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_coddest.Caption = "Destinaz. diversa"
    Me.lc_coddest.Enabled = False
    Me.lc_coddest.FieldName = "lc_coddest"
    Me.lc_coddest.Name = "lc_coddest"
    Me.lc_coddest.NTSRepositoryComboBox = Nothing
    Me.lc_coddest.NTSRepositoryItemCheck = Nothing
    Me.lc_coddest.NTSRepositoryItemMemo = Nothing
    Me.lc_coddest.NTSRepositoryItemText = Nothing
    Me.lc_coddest.OptionsColumn.AllowEdit = False
    Me.lc_coddest.OptionsColumn.ReadOnly = True
    Me.lc_coddest.Visible = True
    Me.lc_coddest.VisibleIndex = 36
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destinaz. diversa"
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
    Me.xx_coddest.VisibleIndex = 37
    Me.grvVris.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lc_codart, Me.xx_codart, Me.lc_prezzo, Me.xx_nuovovalore, Me.lc_listino, Me.lc_datagg, Me.lc_datscad, Me.lc_conto, Me.xx_conto, Me.lc_fase, Me.xx_fase, Me.lc_unmis, Me.lc_tipo, Me.lc_daquant, Me.lc_aquant, Me.lc_codlavo, Me.xx_codlavo, Me.lc_codvalu, Me.xx_codvalu, Me.lc_codtpro, Me.xx_codtpro, Me.lc_perqta, Me.lc_note, Me.lc_netto, Me.ar_codart, Me.ar_codpdon, Me.ar_gesvar, Me.ar_prevar, Me.ar_codroot, Me.ar_codvar1, Me.ar_conver, Me.ar_qtacon2, Me.ar_confez2, Me.ar_unmis2, Me.xx_prezzobase, Me.xx_percvar, Me.lc_coddest, Me.xx_coddest})
    Me.grvVris.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVris.Enabled = True
    Me.grvVris.GridControl = Me.grVris
    Me.grvVris.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVris.MinRowHeight = 14
    Me.grvVris.Name = "grvVris"
    Me.grvVris.NTSAllowDelete = True
    Me.grvVris.NTSAllowInsert = True
    Me.grvVris.NTSAllowUpdate = True
    Me.grvVris.NTSMenuContext = Nothing
    Me.grvVris.OptionsCustomization.AllowRowSizing = True
    Me.grvVris.OptionsFilter.AllowFilterEditor = False
    Me.grvVris.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVris.OptionsNavigation.UseTabKey = False
    Me.grvVris.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVris.OptionsView.ColumnAutoWidth = False
    Me.grvVris.OptionsView.EnableAppearanceEvenRow = True
    Me.grvVris.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVris.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVris.OptionsView.ShowGroupPanel = False
    Me.grvVris.RowHeight = 16
    '
    'lc_codart
    '
    Me.lc_codart.AppearanceCell.Options.UseBackColor = True
    Me.lc_codart.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codart.Caption = "Cod. articolo"
    Me.lc_codart.Enabled = False
    Me.lc_codart.FieldName = "lc_codart"
    Me.lc_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codart.Name = "lc_codart"
    Me.lc_codart.NTSRepositoryComboBox = Nothing
    Me.lc_codart.NTSRepositoryItemCheck = Nothing
    Me.lc_codart.NTSRepositoryItemMemo = Nothing
    Me.lc_codart.NTSRepositoryItemText = Nothing
    Me.lc_codart.OptionsColumn.AllowEdit = False
    Me.lc_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codart.OptionsColumn.ReadOnly = True
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
    'lc_prezzo
    '
    Me.lc_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.lc_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_prezzo.Caption = "Prezzo attuale"
    Me.lc_prezzo.Enabled = False
    Me.lc_prezzo.FieldName = "lc_prezzo"
    Me.lc_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_prezzo.Name = "lc_prezzo"
    Me.lc_prezzo.NTSRepositoryComboBox = Nothing
    Me.lc_prezzo.NTSRepositoryItemCheck = Nothing
    Me.lc_prezzo.NTSRepositoryItemMemo = Nothing
    Me.lc_prezzo.NTSRepositoryItemText = Nothing
    Me.lc_prezzo.OptionsColumn.AllowEdit = False
    Me.lc_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_prezzo.OptionsColumn.ReadOnly = True
    Me.lc_prezzo.OptionsFilter.AllowFilter = False
    Me.lc_prezzo.Visible = True
    Me.lc_prezzo.VisibleIndex = 2
    '
    'xx_nuovovalore
    '
    Me.xx_nuovovalore.AppearanceCell.Options.UseBackColor = True
    Me.xx_nuovovalore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nuovovalore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nuovovalore.Caption = "Prezzo nuovo"
    Me.xx_nuovovalore.Enabled = True
    Me.xx_nuovovalore.FieldName = "xx_nuovovalore"
    Me.xx_nuovovalore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nuovovalore.Name = "xx_nuovovalore"
    Me.xx_nuovovalore.NTSRepositoryComboBox = Nothing
    Me.xx_nuovovalore.NTSRepositoryItemCheck = Nothing
    Me.xx_nuovovalore.NTSRepositoryItemMemo = Nothing
    Me.xx_nuovovalore.NTSRepositoryItemText = Nothing
    Me.xx_nuovovalore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nuovovalore.OptionsFilter.AllowFilter = False
    Me.xx_nuovovalore.Visible = True
    Me.xx_nuovovalore.VisibleIndex = 3
    '
    'lc_listino
    '
    Me.lc_listino.AppearanceCell.Options.UseBackColor = True
    Me.lc_listino.AppearanceCell.Options.UseTextOptions = True
    Me.lc_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_listino.Caption = "Listino"
    Me.lc_listino.Enabled = False
    Me.lc_listino.FieldName = "lc_listino"
    Me.lc_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_listino.Name = "lc_listino"
    Me.lc_listino.NTSRepositoryComboBox = Nothing
    Me.lc_listino.NTSRepositoryItemCheck = Nothing
    Me.lc_listino.NTSRepositoryItemMemo = Nothing
    Me.lc_listino.NTSRepositoryItemText = Nothing
    Me.lc_listino.OptionsColumn.AllowEdit = False
    Me.lc_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_listino.OptionsColumn.ReadOnly = True
    Me.lc_listino.OptionsFilter.AllowFilter = False
    Me.lc_listino.Visible = True
    Me.lc_listino.VisibleIndex = 4
    '
    'lc_datagg
    '
    Me.lc_datagg.AppearanceCell.Options.UseBackColor = True
    Me.lc_datagg.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datagg.Caption = "Data inizio"
    Me.lc_datagg.Enabled = False
    Me.lc_datagg.FieldName = "lc_datagg"
    Me.lc_datagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datagg.Name = "lc_datagg"
    Me.lc_datagg.NTSRepositoryComboBox = Nothing
    Me.lc_datagg.NTSRepositoryItemCheck = Nothing
    Me.lc_datagg.NTSRepositoryItemMemo = Nothing
    Me.lc_datagg.NTSRepositoryItemText = Nothing
    Me.lc_datagg.OptionsColumn.AllowEdit = False
    Me.lc_datagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datagg.OptionsColumn.ReadOnly = True
    Me.lc_datagg.OptionsFilter.AllowFilter = False
    Me.lc_datagg.Visible = True
    Me.lc_datagg.VisibleIndex = 5
    '
    'lc_datscad
    '
    Me.lc_datscad.AppearanceCell.Options.UseBackColor = True
    Me.lc_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datscad.Caption = "Data fine"
    Me.lc_datscad.Enabled = False
    Me.lc_datscad.FieldName = "lc_datscad"
    Me.lc_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datscad.Name = "lc_datscad"
    Me.lc_datscad.NTSRepositoryComboBox = Nothing
    Me.lc_datscad.NTSRepositoryItemCheck = Nothing
    Me.lc_datscad.NTSRepositoryItemMemo = Nothing
    Me.lc_datscad.NTSRepositoryItemText = Nothing
    Me.lc_datscad.OptionsColumn.AllowEdit = False
    Me.lc_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datscad.OptionsColumn.ReadOnly = True
    Me.lc_datscad.OptionsFilter.AllowFilter = False
    Me.lc_datscad.Visible = True
    Me.lc_datscad.VisibleIndex = 6
    '
    'lc_conto
    '
    Me.lc_conto.AppearanceCell.Options.UseBackColor = True
    Me.lc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_conto.Caption = "Cliente/forn."
    Me.lc_conto.Enabled = False
    Me.lc_conto.FieldName = "lc_conto"
    Me.lc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_conto.Name = "lc_conto"
    Me.lc_conto.NTSRepositoryComboBox = Nothing
    Me.lc_conto.NTSRepositoryItemCheck = Nothing
    Me.lc_conto.NTSRepositoryItemMemo = Nothing
    Me.lc_conto.NTSRepositoryItemText = Nothing
    Me.lc_conto.OptionsColumn.AllowEdit = False
    Me.lc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_conto.OptionsColumn.ReadOnly = True
    Me.lc_conto.OptionsFilter.AllowFilter = False
    Me.lc_conto.Visible = True
    Me.lc_conto.VisibleIndex = 7
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
    Me.xx_conto.VisibleIndex = 8
    '
    'lc_fase
    '
    Me.lc_fase.AppearanceCell.Options.UseBackColor = True
    Me.lc_fase.AppearanceCell.Options.UseTextOptions = True
    Me.lc_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_fase.Caption = "Fase"
    Me.lc_fase.Enabled = False
    Me.lc_fase.FieldName = "lc_fase"
    Me.lc_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_fase.Name = "lc_fase"
    Me.lc_fase.NTSRepositoryComboBox = Nothing
    Me.lc_fase.NTSRepositoryItemCheck = Nothing
    Me.lc_fase.NTSRepositoryItemMemo = Nothing
    Me.lc_fase.NTSRepositoryItemText = Nothing
    Me.lc_fase.OptionsColumn.AllowEdit = False
    Me.lc_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_fase.OptionsColumn.ReadOnly = True
    Me.lc_fase.OptionsFilter.AllowFilter = False
    Me.lc_fase.Visible = True
    Me.lc_fase.VisibleIndex = 9
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
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 10
    '
    'lc_unmis
    '
    Me.lc_unmis.AppearanceCell.Options.UseBackColor = True
    Me.lc_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.lc_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_unmis.Caption = "U.M."
    Me.lc_unmis.Enabled = False
    Me.lc_unmis.FieldName = "lc_unmis"
    Me.lc_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_unmis.Name = "lc_unmis"
    Me.lc_unmis.NTSRepositoryComboBox = Nothing
    Me.lc_unmis.NTSRepositoryItemCheck = Nothing
    Me.lc_unmis.NTSRepositoryItemMemo = Nothing
    Me.lc_unmis.NTSRepositoryItemText = Nothing
    Me.lc_unmis.OptionsColumn.AllowEdit = False
    Me.lc_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_unmis.OptionsColumn.ReadOnly = True
    Me.lc_unmis.OptionsFilter.AllowFilter = False
    Me.lc_unmis.Visible = True
    Me.lc_unmis.VisibleIndex = 11
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
    Me.lc_tipo.Visible = True
    Me.lc_tipo.VisibleIndex = 12
    '
    'lc_daquant
    '
    Me.lc_daquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_daquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_daquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_daquant.Caption = "Da quantità"
    Me.lc_daquant.Enabled = False
    Me.lc_daquant.FieldName = "lc_daquant"
    Me.lc_daquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_daquant.Name = "lc_daquant"
    Me.lc_daquant.NTSRepositoryComboBox = Nothing
    Me.lc_daquant.NTSRepositoryItemCheck = Nothing
    Me.lc_daquant.NTSRepositoryItemMemo = Nothing
    Me.lc_daquant.NTSRepositoryItemText = Nothing
    Me.lc_daquant.OptionsColumn.AllowEdit = False
    Me.lc_daquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_daquant.OptionsColumn.ReadOnly = True
    Me.lc_daquant.OptionsFilter.AllowFilter = False
    Me.lc_daquant.Visible = True
    Me.lc_daquant.VisibleIndex = 13
    '
    'lc_aquant
    '
    Me.lc_aquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_aquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_aquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_aquant.Caption = "A quantità"
    Me.lc_aquant.Enabled = False
    Me.lc_aquant.FieldName = "lc_aquant"
    Me.lc_aquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_aquant.Name = "lc_aquant"
    Me.lc_aquant.NTSRepositoryComboBox = Nothing
    Me.lc_aquant.NTSRepositoryItemCheck = Nothing
    Me.lc_aquant.NTSRepositoryItemMemo = Nothing
    Me.lc_aquant.NTSRepositoryItemText = Nothing
    Me.lc_aquant.OptionsColumn.AllowEdit = False
    Me.lc_aquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_aquant.OptionsColumn.ReadOnly = True
    Me.lc_aquant.OptionsFilter.AllowFilter = False
    Me.lc_aquant.Visible = True
    Me.lc_aquant.VisibleIndex = 14
    '
    'lc_codlavo
    '
    Me.lc_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.lc_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codlavo.Caption = "Cod.lav."
    Me.lc_codlavo.Enabled = False
    Me.lc_codlavo.FieldName = "lc_codlavo"
    Me.lc_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codlavo.Name = "lc_codlavo"
    Me.lc_codlavo.NTSRepositoryComboBox = Nothing
    Me.lc_codlavo.NTSRepositoryItemCheck = Nothing
    Me.lc_codlavo.NTSRepositoryItemMemo = Nothing
    Me.lc_codlavo.NTSRepositoryItemText = Nothing
    Me.lc_codlavo.OptionsColumn.AllowEdit = False
    Me.lc_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codlavo.OptionsColumn.ReadOnly = True
    Me.lc_codlavo.OptionsFilter.AllowFilter = False
    Me.lc_codlavo.Visible = True
    Me.lc_codlavo.VisibleIndex = 15
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
    Me.xx_codlavo.Visible = True
    Me.xx_codlavo.VisibleIndex = 16
    '
    'lc_codvalu
    '
    Me.lc_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.lc_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codvalu.Caption = "Valuta"
    Me.lc_codvalu.Enabled = False
    Me.lc_codvalu.FieldName = "lc_codvalu"
    Me.lc_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codvalu.Name = "lc_codvalu"
    Me.lc_codvalu.NTSRepositoryComboBox = Nothing
    Me.lc_codvalu.NTSRepositoryItemCheck = Nothing
    Me.lc_codvalu.NTSRepositoryItemMemo = Nothing
    Me.lc_codvalu.NTSRepositoryItemText = Nothing
    Me.lc_codvalu.OptionsColumn.AllowEdit = False
    Me.lc_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codvalu.OptionsColumn.ReadOnly = True
    Me.lc_codvalu.OptionsFilter.AllowFilter = False
    Me.lc_codvalu.Visible = True
    Me.lc_codvalu.VisibleIndex = 17
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
    Me.xx_codvalu.Visible = True
    Me.xx_codvalu.VisibleIndex = 18
    '
    'lc_codtpro
    '
    Me.lc_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.lc_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codtpro.Caption = "Cod. promoz."
    Me.lc_codtpro.Enabled = False
    Me.lc_codtpro.FieldName = "lc_codtpro"
    Me.lc_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codtpro.Name = "lc_codtpro"
    Me.lc_codtpro.NTSRepositoryComboBox = Nothing
    Me.lc_codtpro.NTSRepositoryItemCheck = Nothing
    Me.lc_codtpro.NTSRepositoryItemMemo = Nothing
    Me.lc_codtpro.NTSRepositoryItemText = Nothing
    Me.lc_codtpro.OptionsColumn.AllowEdit = False
    Me.lc_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codtpro.OptionsColumn.ReadOnly = True
    Me.lc_codtpro.OptionsFilter.AllowFilter = False
    Me.lc_codtpro.Visible = True
    Me.lc_codtpro.VisibleIndex = 19
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
    Me.xx_codtpro.Visible = True
    Me.xx_codtpro.VisibleIndex = 20
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
    Me.lc_perqta.Visible = True
    Me.lc_perqta.VisibleIndex = 21
    '
    'lc_note
    '
    Me.lc_note.AppearanceCell.Options.UseBackColor = True
    Me.lc_note.AppearanceCell.Options.UseTextOptions = True
    Me.lc_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_note.Caption = "Note"
    Me.lc_note.Enabled = False
    Me.lc_note.FieldName = "lc_note"
    Me.lc_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_note.Name = "lc_note"
    Me.lc_note.NTSRepositoryComboBox = Nothing
    Me.lc_note.NTSRepositoryItemCheck = Nothing
    Me.lc_note.NTSRepositoryItemMemo = Nothing
    Me.lc_note.NTSRepositoryItemText = Nothing
    Me.lc_note.OptionsColumn.AllowEdit = False
    Me.lc_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_note.OptionsColumn.ReadOnly = True
    Me.lc_note.OptionsFilter.AllowFilter = False
    Me.lc_note.Visible = True
    Me.lc_note.VisibleIndex = 22
    '
    'lc_netto
    '
    Me.lc_netto.AppearanceCell.Options.UseBackColor = True
    Me.lc_netto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_netto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_netto.Caption = "Prezzo netto"
    Me.lc_netto.Enabled = False
    Me.lc_netto.FieldName = "lc_netto"
    Me.lc_netto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_netto.Name = "lc_netto"
    Me.lc_netto.NTSRepositoryComboBox = Nothing
    Me.lc_netto.NTSRepositoryItemCheck = Nothing
    Me.lc_netto.NTSRepositoryItemMemo = Nothing
    Me.lc_netto.NTSRepositoryItemText = Nothing
    Me.lc_netto.OptionsColumn.AllowEdit = False
    Me.lc_netto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_netto.OptionsColumn.ReadOnly = True
    Me.lc_netto.OptionsFilter.AllowFilter = False
    Me.lc_netto.Visible = True
    Me.lc_netto.VisibleIndex = 23
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Cod. articolo"
    Me.ar_codart.Enabled = False
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowEdit = False
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsColumn.ReadOnly = True
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 24
    '
    'ar_codpdon
    '
    Me.ar_codpdon.AppearanceCell.Options.UseBackColor = True
    Me.ar_codpdon.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codpdon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codpdon.Caption = "Cod. relaz. listini"
    Me.ar_codpdon.Enabled = False
    Me.ar_codpdon.FieldName = "ar_codpdon"
    Me.ar_codpdon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codpdon.Name = "ar_codpdon"
    Me.ar_codpdon.NTSRepositoryComboBox = Nothing
    Me.ar_codpdon.NTSRepositoryItemCheck = Nothing
    Me.ar_codpdon.NTSRepositoryItemMemo = Nothing
    Me.ar_codpdon.NTSRepositoryItemText = Nothing
    Me.ar_codpdon.OptionsColumn.AllowEdit = False
    Me.ar_codpdon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codpdon.OptionsColumn.ReadOnly = True
    Me.ar_codpdon.OptionsFilter.AllowFilter = False
    Me.ar_codpdon.Visible = True
    Me.ar_codpdon.VisibleIndex = 25
    '
    'ar_gesvar
    '
    Me.ar_gesvar.AppearanceCell.Options.UseBackColor = True
    Me.ar_gesvar.AppearanceCell.Options.UseTextOptions = True
    Me.ar_gesvar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_gesvar.Caption = "Gest. varianti"
    Me.ar_gesvar.Enabled = False
    Me.ar_gesvar.FieldName = "ar_gesvar"
    Me.ar_gesvar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_gesvar.Name = "ar_gesvar"
    Me.ar_gesvar.NTSRepositoryComboBox = Nothing
    Me.ar_gesvar.NTSRepositoryItemCheck = Nothing
    Me.ar_gesvar.NTSRepositoryItemMemo = Nothing
    Me.ar_gesvar.NTSRepositoryItemText = Nothing
    Me.ar_gesvar.OptionsColumn.AllowEdit = False
    Me.ar_gesvar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_gesvar.OptionsColumn.ReadOnly = True
    Me.ar_gesvar.OptionsFilter.AllowFilter = False
    Me.ar_gesvar.Visible = True
    Me.ar_gesvar.VisibleIndex = 26
    '
    'ar_prevar
    '
    Me.ar_prevar.AppearanceCell.Options.UseBackColor = True
    Me.ar_prevar.AppearanceCell.Options.UseTextOptions = True
    Me.ar_prevar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_prevar.Caption = "Prezzi per var."
    Me.ar_prevar.Enabled = False
    Me.ar_prevar.FieldName = "ar_prevar"
    Me.ar_prevar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_prevar.Name = "ar_prevar"
    Me.ar_prevar.NTSRepositoryComboBox = Nothing
    Me.ar_prevar.NTSRepositoryItemCheck = Nothing
    Me.ar_prevar.NTSRepositoryItemMemo = Nothing
    Me.ar_prevar.NTSRepositoryItemText = Nothing
    Me.ar_prevar.OptionsColumn.AllowEdit = False
    Me.ar_prevar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_prevar.OptionsColumn.ReadOnly = True
    Me.ar_prevar.OptionsFilter.AllowFilter = False
    Me.ar_prevar.Visible = True
    Me.ar_prevar.VisibleIndex = 27
    '
    'ar_codroot
    '
    Me.ar_codroot.AppearanceCell.Options.UseBackColor = True
    Me.ar_codroot.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codroot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codroot.Caption = "Codice root"
    Me.ar_codroot.Enabled = False
    Me.ar_codroot.FieldName = "ar_codroot"
    Me.ar_codroot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codroot.Name = "ar_codroot"
    Me.ar_codroot.NTSRepositoryComboBox = Nothing
    Me.ar_codroot.NTSRepositoryItemCheck = Nothing
    Me.ar_codroot.NTSRepositoryItemMemo = Nothing
    Me.ar_codroot.NTSRepositoryItemText = Nothing
    Me.ar_codroot.OptionsColumn.AllowEdit = False
    Me.ar_codroot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codroot.OptionsColumn.ReadOnly = True
    Me.ar_codroot.OptionsFilter.AllowFilter = False
    Me.ar_codroot.Visible = True
    Me.ar_codroot.VisibleIndex = 28
    '
    'ar_codvar1
    '
    Me.ar_codvar1.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar1.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar1.Caption = "Cod. variante 1"
    Me.ar_codvar1.Enabled = False
    Me.ar_codvar1.FieldName = "ar_codvar1"
    Me.ar_codvar1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar1.Name = "ar_codvar1"
    Me.ar_codvar1.NTSRepositoryComboBox = Nothing
    Me.ar_codvar1.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar1.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar1.NTSRepositoryItemText = Nothing
    Me.ar_codvar1.OptionsColumn.AllowEdit = False
    Me.ar_codvar1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar1.OptionsColumn.ReadOnly = True
    Me.ar_codvar1.OptionsFilter.AllowFilter = False
    Me.ar_codvar1.Visible = True
    Me.ar_codvar1.VisibleIndex = 29
    '
    'ar_conver
    '
    Me.ar_conver.AppearanceCell.Options.UseBackColor = True
    Me.ar_conver.AppearanceCell.Options.UseTextOptions = True
    Me.ar_conver.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_conver.Caption = "Rap. convers."
    Me.ar_conver.Enabled = False
    Me.ar_conver.FieldName = "ar_conver"
    Me.ar_conver.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_conver.Name = "ar_conver"
    Me.ar_conver.NTSRepositoryComboBox = Nothing
    Me.ar_conver.NTSRepositoryItemCheck = Nothing
    Me.ar_conver.NTSRepositoryItemMemo = Nothing
    Me.ar_conver.NTSRepositoryItemText = Nothing
    Me.ar_conver.OptionsColumn.AllowEdit = False
    Me.ar_conver.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_conver.OptionsColumn.ReadOnly = True
    Me.ar_conver.OptionsFilter.AllowFilter = False
    Me.ar_conver.Visible = True
    Me.ar_conver.VisibleIndex = 30
    '
    'ar_qtacon2
    '
    Me.ar_qtacon2.AppearanceCell.Options.UseBackColor = True
    Me.ar_qtacon2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_qtacon2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_qtacon2.Caption = "Qta confez. 2"
    Me.ar_qtacon2.Enabled = False
    Me.ar_qtacon2.FieldName = "ar_qtacon2"
    Me.ar_qtacon2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_qtacon2.Name = "ar_qtacon2"
    Me.ar_qtacon2.NTSRepositoryComboBox = Nothing
    Me.ar_qtacon2.NTSRepositoryItemCheck = Nothing
    Me.ar_qtacon2.NTSRepositoryItemMemo = Nothing
    Me.ar_qtacon2.NTSRepositoryItemText = Nothing
    Me.ar_qtacon2.OptionsColumn.AllowEdit = False
    Me.ar_qtacon2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_qtacon2.OptionsColumn.ReadOnly = True
    Me.ar_qtacon2.OptionsFilter.AllowFilter = False
    Me.ar_qtacon2.Visible = True
    Me.ar_qtacon2.VisibleIndex = 31
    '
    'ar_confez2
    '
    Me.ar_confez2.AppearanceCell.Options.UseBackColor = True
    Me.ar_confez2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_confez2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_confez2.Caption = "Confez. 2"
    Me.ar_confez2.Enabled = False
    Me.ar_confez2.FieldName = "ar_confez2"
    Me.ar_confez2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_confez2.Name = "ar_confez2"
    Me.ar_confez2.NTSRepositoryComboBox = Nothing
    Me.ar_confez2.NTSRepositoryItemCheck = Nothing
    Me.ar_confez2.NTSRepositoryItemMemo = Nothing
    Me.ar_confez2.NTSRepositoryItemText = Nothing
    Me.ar_confez2.OptionsColumn.AllowEdit = False
    Me.ar_confez2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_confez2.OptionsColumn.ReadOnly = True
    Me.ar_confez2.OptionsFilter.AllowFilter = False
    Me.ar_confez2.Visible = True
    Me.ar_confez2.VisibleIndex = 32
    '
    'ar_unmis2
    '
    Me.ar_unmis2.AppearanceCell.Options.UseBackColor = True
    Me.ar_unmis2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_unmis2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_unmis2.Caption = "U.M. 2"
    Me.ar_unmis2.Enabled = False
    Me.ar_unmis2.FieldName = "ar_unmis2"
    Me.ar_unmis2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_unmis2.Name = "ar_unmis2"
    Me.ar_unmis2.NTSRepositoryComboBox = Nothing
    Me.ar_unmis2.NTSRepositoryItemCheck = Nothing
    Me.ar_unmis2.NTSRepositoryItemMemo = Nothing
    Me.ar_unmis2.NTSRepositoryItemText = Nothing
    Me.ar_unmis2.OptionsColumn.AllowEdit = False
    Me.ar_unmis2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_unmis2.OptionsColumn.ReadOnly = True
    Me.ar_unmis2.OptionsFilter.AllowFilter = False
    Me.ar_unmis2.Visible = True
    Me.ar_unmis2.VisibleIndex = 33
    '
    'xx_prezzobase
    '
    Me.xx_prezzobase.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzobase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzobase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzobase.Caption = "Prezzo base"
    Me.xx_prezzobase.Enabled = False
    Me.xx_prezzobase.FieldName = "xx_prezzobase"
    Me.xx_prezzobase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzobase.Name = "xx_prezzobase"
    Me.xx_prezzobase.NTSRepositoryComboBox = Nothing
    Me.xx_prezzobase.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzobase.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzobase.NTSRepositoryItemText = Nothing
    Me.xx_prezzobase.OptionsColumn.AllowEdit = False
    Me.xx_prezzobase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzobase.OptionsColumn.ReadOnly = True
    Me.xx_prezzobase.OptionsFilter.AllowFilter = False
    Me.xx_prezzobase.Visible = True
    Me.xx_prezzobase.VisibleIndex = 34
    '
    'xx_percvar
    '
    Me.xx_percvar.AppearanceCell.Options.UseBackColor = True
    Me.xx_percvar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_percvar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_percvar.Caption = "% Variazione"
    Me.xx_percvar.Enabled = False
    Me.xx_percvar.FieldName = "xx_percvar"
    Me.xx_percvar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_percvar.Name = "xx_percvar"
    Me.xx_percvar.NTSRepositoryComboBox = Nothing
    Me.xx_percvar.NTSRepositoryItemCheck = Nothing
    Me.xx_percvar.NTSRepositoryItemMemo = Nothing
    Me.xx_percvar.NTSRepositoryItemText = Nothing
    Me.xx_percvar.OptionsColumn.AllowEdit = False
    Me.xx_percvar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_percvar.OptionsColumn.ReadOnly = True
    Me.xx_percvar.OptionsFilter.AllowFilter = False
    Me.xx_percvar.Visible = True
    Me.xx_percvar.VisibleIndex = 35
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbElabora})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 16
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMMGVRIS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grVris)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGVRIS"
    Me.NTSLastControlFocussed = Me.grVris
    Me.Text = "VISUALIZZA RISULTATI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grVris, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVris, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvVris.NTSSetParam(oMenu, oApp.Tr(Me, 128563535825894188, "Visualizza risultati"))
      lc_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754866307740, "Cod. articolo"), 0, True)
      lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754866932752, "Prezzo attuale"), oCleElar.TrovaFmtPrz, 15)
      lc_listino.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754867245258, "Listino"), "0", 4, 0, 9999)
      lc_datagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128695754867401511, "Data inizio"), True)
      lc_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128695754867557764, "Data fine"), True)
      lc_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754867714017, "Cliente/forn."), "0", 9, 0, 999999999)
      lc_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754868026523, "Fase"), "0", 4, 0, 9999)
      lc_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754868339029, "U.M."), 0, True)
      lc_tipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754868495282, "Tipo"), 0, True)
      lc_daquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754868651535, "Da quantità"), "#,##0.00", 15)
      lc_aquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754868807788, "A quantità"), "#,##0.00", 15)
      lc_codlavo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754868964041, "Cod.lav."), "0", 4, 0, 9999)
      lc_codvalu.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754869276547, "Valuta"), "0", 4, 0, 9999)
      lc_codtpro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754869589053, "Cod. promoz."), "0", 4, 0, 9999)
      lc_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754869901559, "Q.tà prezzo"), "#,##0.00", 15)
      lc_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870057812, "Note"), 0, True)
      lc_netto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870214065, "Prezzo netto"), 0, True)
      ar_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870370318, "Cod. articolo"), 0, True)
      ar_codpdon.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754870526571, "Cod. relaz. listini"), "0", 4, 0, 9999)
      ar_gesvar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870682824, "Gest. varianti"), 0, True)
      ar_prevar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870839077, "Prezzi per var."), 0, True)
      ar_codroot.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754870995330, "Codice root"), 0, True)
      ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754871151583, "Cod. variante 1"), 0, True)
      ar_conver.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754871307836, "Rap. convers."), "#,##0.00", 15)
      ar_qtacon2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695754871464089, "Qta confez. 2"), "#,##0.00", 15)
      ar_confez2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754871620342, "Confez. 2"), 0, True)
      ar_unmis2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695754871776595, "U.M. 2"), 0, True)
      xx_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762796151613, "Descr. articolo"), 0, True)
      xx_nuovovalore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128695762796464097, "Prezzo nuovo"), oCleElar.TrovaFmtPrz, 15)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762797245307, "Descrizione"), 0, True)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762797557791, "Descr. fase"), 0, True)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762798495243, "Descr.lav."), 0, True)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762798807727, "Descr.valuta"), 0, True)
      xx_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128695762799120211, "Descr. promoz."), 0, True)
      xx_prezzobase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128814363339762804, "Prezzo base"), oCleElar.TrovaFmtPrz, 15)
      xx_percvar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130203590212944215, "% Variazione"), oApp.FormatSconti, 6)
      lc_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), "0", 9, -1, 999999999)
      lc_coddest.CliePerDestdiv = lc_conto
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130585244380533691, "Descr. destinazione diversa"), 0, True)

      grvVris.AddColumnBackColor("backcolor_row") 'sempre nella InitControls

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
  Public Overridable Sub InitEntity(ByRef cleElar As CLEMGELAR)
    oCleElar = cleElar
    AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvVris.NTSSalvaRigaCorrente(dcVris, oCleElar.VrisRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleElar.VrisSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleElar.VrisRipristina(dcVris.Position, dcVris.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMMGVRIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleElar.nVrisRichiesta = 0

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigatorù
      If Not oCleElar.VrisApri(DittaCorrente, dsVris) Then
        Me.Close()
        Return
      End If

      dcVris.DataSource = dsVris.Tables("LISTINI")
      For Each dtrT As DataRow In dsVris.Tables("LISTINI").Rows
        AssegnaColoriGriglia(dtrT)
      Next
      grVris.DataSource = dcVris

      grvVris.NTSAllowInsert = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGVRIS_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcVris.Dispose()
      dsVris.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGVRIS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim dRes As DialogResult

    If Not Salva() Then e.Cancel = True
    If grvVris.RowCount > 0 Then
      If oCleElar.nVrisRichiesta = 0 Then
        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128696615682317817, "Confermi l'annullamento del'elaborazione?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          oCleElar.nVrisRichiesta = 0
        Else
          e.Cancel = True
          Return
        End If
      End If
    End If
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      oCleElar.nVrisRichiesta = 1
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvVris.NTSDeleteRigaCorrente(dcVris, True) Then Return
      oCleElar.VrisSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvVris.NTSRipristinaRigaCorrenteBefore(dcVris, True) Then Return
      oCleElar.VrisRipristina(dcVris.Position, dcVris.Filter)
      grvVris.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvVris_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvVris.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

      AssegnaColoriGriglia(grvVris.NTSGetCurrentDataRow)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function AssegnaColoriGriglia(ByRef dtrT As DataRow) As Boolean
    'coloro le celle
    Try
      'quando viene cambiato il colore della cella backcolor_row o della backcolor_*, 
      'sbs invia al client la segnalazione di 'cella cambiata'
      'su SBC la cellace backcolor_ non viene mai disegnata, ma viene utilizzata per colorare la riga/cella
      'NB: la colonna backcolor_ deve essere inserita anche nella griglia (magari non visibile) altrimenti non viene passata a SBC!!!

      'dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))        'costante 'backcolor_row' utilizzato da SBC per colorare tutta la riga
      'dtrIn.Table.Columns.Add("backcolor_xx_status", GetType(Integer))  'prefisso 'backcolor_' utilizzato da SBC per colorare la singola cella

      If dtrT Is Nothing Then Return True

      'memorizzo lo stato prima della modifica del campo
      Dim oState As DataRowState = dtrT.RowState
      Dim bOk As Boolean = oCleElar.bHasChanges

      If dtrT.Table.Columns.Contains("backcolor_row") = False Then
        dtrT.Table.Columns.Add("backcolor_row", GetType(Integer))
      End If

      'per default non è
      dtrT("backcolor_row") = -1

      If NTSCDec(dtrT!xx_percvar) < 0 Then dtrT!backcolor_row = Color.Red.ToArgb

      'reimposto lo stato a prima della modifica del campo
      If oState = DataRowState.Unchanged Then dtrT.AcceptChanges()
      oCleElar.bHasChanges = bOk

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class