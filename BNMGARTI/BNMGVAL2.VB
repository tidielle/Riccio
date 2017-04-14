Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVAL2
#Region "Variabili"
  Public oCleArti As CLEMGARTI
  Public oCallParams As CLE__CLDP
  Public dsVal2 As New DataSet
  Public dcVal2 As New BindingSource()
  Public dttVal2 As DataTable = Nothing
  Public dtrVal2() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer

  Public WithEvents grMulti As NTSInformatica.NTSGrid
  Public WithEvents grvMulti As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents alv_ncampo As NTSInformatica.NTSGridColumn
  Public WithEvents alv_index As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valtext As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valdata As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore2 As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valvari As NTSInformatica.NTSGridColumn
  Public WithEvents alv_desvvar As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codvari As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvari As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codling As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codling As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codartl As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codartl As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codart As NTSInformatica.NTSGridColumn
  Public WithEvents alv_multi As NTSInformatica.NTSGridColumn
  Public WithEvents alv_linkpr As NTSInformatica.NTSGridColumn
  Public WithEvents alv_linkric As NTSInformatica.NTSGridColumn
  Public WithEvents alv_gestling As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codtipa As NTSInformatica.NTSGridColumn
  Public WithEvents alv_descamp As NTSInformatica.NTSGridColumn
  Public WithEvents alv_tipcamp As NTSInformatica.NTSGridColumn
  Public WithEvents alv_flcamporic As NTSInformatica.NTSGridColumn
  Public WithEvents alv_flurl As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valdouble As NTSInformatica.NTSGridColumn
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVAL2))
    Me.grMulti = New NTSInformatica.NTSGrid
    Me.grvMulti = New NTSInformatica.NTSGridView
    Me.alv_ncampo = New NTSInformatica.NTSGridColumn
    Me.alv_index = New NTSInformatica.NTSGridColumn
    Me.alv_valdouble = New NTSInformatica.NTSGridColumn
    Me.alv_valtext = New NTSInformatica.NTSGridColumn
    Me.alv_valdata = New NTSInformatica.NTSGridColumn
    Me.xx_valore2 = New NTSInformatica.NTSGridColumn
    Me.alv_valvari = New NTSInformatica.NTSGridColumn
    Me.alv_desvvar = New NTSInformatica.NTSGridColumn
    Me.alv_codvari = New NTSInformatica.NTSGridColumn
    Me.xx_codvari = New NTSInformatica.NTSGridColumn
    Me.alv_codling = New NTSInformatica.NTSGridColumn
    Me.xx_codling = New NTSInformatica.NTSGridColumn
    Me.alv_codartl = New NTSInformatica.NTSGridColumn
    Me.xx_codartl = New NTSInformatica.NTSGridColumn
    Me.alv_codart = New NTSInformatica.NTSGridColumn
    Me.alv_multi = New NTSInformatica.NTSGridColumn
    Me.alv_linkpr = New NTSInformatica.NTSGridColumn
    Me.alv_linkric = New NTSInformatica.NTSGridColumn
    Me.alv_gestling = New NTSInformatica.NTSGridColumn
    Me.alv_codtipa = New NTSInformatica.NTSGridColumn
    Me.alv_descamp = New NTSInformatica.NTSGridColumn
    Me.alv_tipcamp = New NTSInformatica.NTSGridColumn
    Me.alv_flcamporic = New NTSInformatica.NTSGridColumn
    Me.alv_flurl = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnBottom = New NTSInformatica.NTSPanel
    CType(Me.grMulti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMulti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'grMulti
    '
    Me.grMulti.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grMulti.EmbeddedNavigator.Name = ""
    Me.grMulti.Location = New System.Drawing.Point(0, 0)
    Me.grMulti.MainView = Me.grvMulti
    Me.grMulti.Name = "grMulti"
    Me.grMulti.Size = New System.Drawing.Size(667, 212)
    Me.grMulti.TabIndex = 0
    Me.grMulti.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMulti})
    '
    'grvMulti
    '
    Me.grvMulti.ActiveFilterEnabled = False
    Me.grvMulti.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.alv_ncampo, Me.alv_index, Me.alv_valdouble, Me.alv_valtext, Me.alv_valdata, Me.xx_valore2, Me.alv_valvari, Me.alv_desvvar, Me.alv_codvari, Me.xx_codvari, Me.alv_codling, Me.xx_codling, Me.alv_codartl, Me.xx_codartl, Me.alv_codart, Me.alv_multi, Me.alv_linkpr, Me.alv_linkric, Me.alv_gestling, Me.alv_codtipa, Me.alv_descamp, Me.alv_tipcamp, Me.alv_flcamporic, Me.alv_flurl, Me.codditt})
    Me.grvMulti.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvMulti.Enabled = True
    Me.grvMulti.GridControl = Me.grMulti
    Me.grvMulti.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvMulti.Name = "grvMulti"
    Me.grvMulti.NTSAllowDelete = True
    Me.grvMulti.NTSAllowInsert = True
    Me.grvMulti.NTSAllowUpdate = True
    Me.grvMulti.NTSMenuContext = Nothing
    Me.grvMulti.OptionsCustomization.AllowRowSizing = True
    Me.grvMulti.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMulti.OptionsNavigation.UseTabKey = False
    Me.grvMulti.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMulti.OptionsView.ColumnAutoWidth = False
    Me.grvMulti.OptionsView.EnableAppearanceEvenRow = True
    Me.grvMulti.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMulti.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvMulti.OptionsView.ShowGroupPanel = False
    Me.grvMulti.RowHeight = 16
    '
    'alv_ncampo
    '
    Me.alv_ncampo.AppearanceCell.Options.UseBackColor = True
    Me.alv_ncampo.AppearanceCell.Options.UseTextOptions = True
    Me.alv_ncampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_ncampo.Caption = "N. campo"
    Me.alv_ncampo.Enabled = False
    Me.alv_ncampo.FieldName = "alv_ncampo"
    Me.alv_ncampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_ncampo.Name = "alv_ncampo"
    Me.alv_ncampo.NTSRepositoryComboBox = Nothing
    Me.alv_ncampo.NTSRepositoryItemCheck = Nothing
    Me.alv_ncampo.NTSRepositoryItemText = Nothing
    Me.alv_ncampo.OptionsColumn.AllowEdit = False
    Me.alv_ncampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_ncampo.OptionsColumn.ReadOnly = True
    Me.alv_ncampo.OptionsFilter.AllowFilter = False
    '
    'alv_index
    '
    Me.alv_index.AppearanceCell.Options.UseBackColor = True
    Me.alv_index.AppearanceCell.Options.UseTextOptions = True
    Me.alv_index.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_index.Caption = "Index"
    Me.alv_index.Enabled = False
    Me.alv_index.FieldName = "alv_index"
    Me.alv_index.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_index.Name = "alv_index"
    Me.alv_index.NTSRepositoryComboBox = Nothing
    Me.alv_index.NTSRepositoryItemCheck = Nothing
    Me.alv_index.NTSRepositoryItemText = Nothing
    Me.alv_index.OptionsColumn.AllowEdit = False
    Me.alv_index.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_index.OptionsColumn.ReadOnly = True
    Me.alv_index.OptionsFilter.AllowFilter = False
    Me.alv_index.Visible = True
    Me.alv_index.VisibleIndex = 0
    '
    'alv_valdouble
    '
    Me.alv_valdouble.AppearanceCell.Options.UseBackColor = True
    Me.alv_valdouble.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valdouble.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valdouble.Caption = "Valdouble"
    Me.alv_valdouble.Enabled = False
    Me.alv_valdouble.FieldName = "alv_valdouble"
    Me.alv_valdouble.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valdouble.Name = "alv_valdouble"
    Me.alv_valdouble.NTSRepositoryComboBox = Nothing
    Me.alv_valdouble.NTSRepositoryItemCheck = Nothing
    Me.alv_valdouble.NTSRepositoryItemText = Nothing
    Me.alv_valdouble.OptionsColumn.AllowEdit = False
    Me.alv_valdouble.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valdouble.OptionsColumn.ReadOnly = True
    Me.alv_valdouble.OptionsFilter.AllowFilter = False
    '
    'alv_valtext
    '
    Me.alv_valtext.AppearanceCell.Options.UseBackColor = True
    Me.alv_valtext.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valtext.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valtext.Caption = "Valtext"
    Me.alv_valtext.Enabled = False
    Me.alv_valtext.FieldName = "alv_valtext"
    Me.alv_valtext.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valtext.Name = "alv_valtext"
    Me.alv_valtext.NTSRepositoryComboBox = Nothing
    Me.alv_valtext.NTSRepositoryItemCheck = Nothing
    Me.alv_valtext.NTSRepositoryItemText = Nothing
    Me.alv_valtext.OptionsColumn.AllowEdit = False
    Me.alv_valtext.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valtext.OptionsColumn.ReadOnly = True
    Me.alv_valtext.OptionsFilter.AllowFilter = False
    '
    'alv_valdata
    '
    Me.alv_valdata.AppearanceCell.Options.UseBackColor = True
    Me.alv_valdata.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valdata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valdata.Caption = "Valdata"
    Me.alv_valdata.Enabled = False
    Me.alv_valdata.FieldName = "alv_valdata"
    Me.alv_valdata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valdata.Name = "alv_valdata"
    Me.alv_valdata.NTSRepositoryComboBox = Nothing
    Me.alv_valdata.NTSRepositoryItemCheck = Nothing
    Me.alv_valdata.NTSRepositoryItemText = Nothing
    Me.alv_valdata.OptionsColumn.AllowEdit = False
    Me.alv_valdata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valdata.OptionsColumn.ReadOnly = True
    Me.alv_valdata.OptionsFilter.AllowFilter = False
    '
    'xx_valore2
    '
    Me.xx_valore2.AppearanceCell.Options.UseBackColor = True
    Me.xx_valore2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valore2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valore2.Caption = "Valore"
    Me.xx_valore2.Enabled = True
    Me.xx_valore2.FieldName = "xx_valore2"
    Me.xx_valore2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valore2.Name = "xx_valore2"
    Me.xx_valore2.NTSRepositoryComboBox = Nothing
    Me.xx_valore2.NTSRepositoryItemCheck = Nothing
    Me.xx_valore2.NTSRepositoryItemText = Nothing
    Me.xx_valore2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valore2.OptionsFilter.AllowFilter = False
    Me.xx_valore2.Visible = True
    Me.xx_valore2.VisibleIndex = 1
    '
    'alv_valvari
    '
    Me.alv_valvari.AppearanceCell.Options.UseBackColor = True
    Me.alv_valvari.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valvari.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valvari.Caption = "Valore variante"
    Me.alv_valvari.Enabled = True
    Me.alv_valvari.FieldName = "alv_valvari"
    Me.alv_valvari.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valvari.Name = "alv_valvari"
    Me.alv_valvari.NTSRepositoryComboBox = Nothing
    Me.alv_valvari.NTSRepositoryItemCheck = Nothing
    Me.alv_valvari.NTSRepositoryItemText = Nothing
    Me.alv_valvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valvari.OptionsFilter.AllowFilter = False
    '
    'alv_desvvar
    '
    Me.alv_desvvar.AppearanceCell.Options.UseBackColor = True
    Me.alv_desvvar.AppearanceCell.Options.UseTextOptions = True
    Me.alv_desvvar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_desvvar.Caption = "Desc. valore variante"
    Me.alv_desvvar.Enabled = False
    Me.alv_desvvar.FieldName = "alv_desvvar"
    Me.alv_desvvar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_desvvar.Name = "alv_desvvar"
    Me.alv_desvvar.NTSRepositoryComboBox = Nothing
    Me.alv_desvvar.NTSRepositoryItemCheck = Nothing
    Me.alv_desvvar.NTSRepositoryItemText = Nothing
    Me.alv_desvvar.OptionsColumn.AllowEdit = False
    Me.alv_desvvar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_desvvar.OptionsColumn.ReadOnly = True
    Me.alv_desvvar.OptionsFilter.AllowFilter = False
    Me.alv_desvvar.Visible = True
    Me.alv_desvvar.VisibleIndex = 2
    '
    'alv_codvari
    '
    Me.alv_codvari.AppearanceCell.Options.UseBackColor = True
    Me.alv_codvari.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codvari.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codvari.Caption = "Cod. variante"
    Me.alv_codvari.Enabled = False
    Me.alv_codvari.FieldName = "alv_codvari"
    Me.alv_codvari.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codvari.Name = "alv_codvari"
    Me.alv_codvari.NTSRepositoryComboBox = Nothing
    Me.alv_codvari.NTSRepositoryItemCheck = Nothing
    Me.alv_codvari.NTSRepositoryItemText = Nothing
    Me.alv_codvari.OptionsColumn.AllowEdit = False
    Me.alv_codvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codvari.OptionsColumn.ReadOnly = True
    Me.alv_codvari.OptionsFilter.AllowFilter = False
    '
    'xx_codvari
    '
    Me.xx_codvari.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvari.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvari.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvari.Caption = "Desc. variante"
    Me.xx_codvari.Enabled = False
    Me.xx_codvari.FieldName = "xx_codvari"
    Me.xx_codvari.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvari.Name = "xx_codvari"
    Me.xx_codvari.NTSRepositoryComboBox = Nothing
    Me.xx_codvari.NTSRepositoryItemCheck = Nothing
    Me.xx_codvari.NTSRepositoryItemText = Nothing
    Me.xx_codvari.OptionsColumn.AllowEdit = False
    Me.xx_codvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvari.OptionsColumn.ReadOnly = True
    Me.xx_codvari.OptionsFilter.AllowFilter = False
    '
    'alv_codling
    '
    Me.alv_codling.AppearanceCell.Options.UseBackColor = True
    Me.alv_codling.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codling.Caption = "Cod. lingua"
    Me.alv_codling.Enabled = True
    Me.alv_codling.FieldName = "alv_codling"
    Me.alv_codling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codling.Name = "alv_codling"
    Me.alv_codling.NTSRepositoryComboBox = Nothing
    Me.alv_codling.NTSRepositoryItemCheck = Nothing
    Me.alv_codling.NTSRepositoryItemText = Nothing
    Me.alv_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codling.OptionsFilter.AllowFilter = False
    Me.alv_codling.Visible = True
    Me.alv_codling.VisibleIndex = 3
    '
    'xx_codling
    '
    Me.xx_codling.AppearanceCell.Options.UseBackColor = True
    Me.xx_codling.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codling.Caption = "Desc. lingua"
    Me.xx_codling.Enabled = False
    Me.xx_codling.FieldName = "xx_codling"
    Me.xx_codling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codling.Name = "xx_codling"
    Me.xx_codling.NTSRepositoryComboBox = Nothing
    Me.xx_codling.NTSRepositoryItemCheck = Nothing
    Me.xx_codling.NTSRepositoryItemText = Nothing
    Me.xx_codling.OptionsColumn.AllowEdit = False
    Me.xx_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codling.OptionsColumn.ReadOnly = True
    Me.xx_codling.OptionsFilter.AllowFilter = False
    Me.xx_codling.Visible = True
    Me.xx_codling.VisibleIndex = 4
    '
    'alv_codartl
    '
    Me.alv_codartl.AppearanceCell.Options.UseBackColor = True
    Me.alv_codartl.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codartl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codartl.Caption = "Cod. articolo (link)"
    Me.alv_codartl.Enabled = True
    Me.alv_codartl.FieldName = "alv_codartl"
    Me.alv_codartl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codartl.Name = "alv_codartl"
    Me.alv_codartl.NTSRepositoryComboBox = Nothing
    Me.alv_codartl.NTSRepositoryItemCheck = Nothing
    Me.alv_codartl.NTSRepositoryItemText = Nothing
    Me.alv_codartl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codartl.OptionsFilter.AllowFilter = False
    Me.alv_codartl.Visible = True
    Me.alv_codartl.VisibleIndex = 5
    '
    'xx_codartl
    '
    Me.xx_codartl.AppearanceCell.Options.UseBackColor = True
    Me.xx_codartl.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codartl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codartl.Caption = "Desc. articolo"
    Me.xx_codartl.Enabled = False
    Me.xx_codartl.FieldName = "xx_codartl"
    Me.xx_codartl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codartl.Name = "xx_codartl"
    Me.xx_codartl.NTSRepositoryComboBox = Nothing
    Me.xx_codartl.NTSRepositoryItemCheck = Nothing
    Me.xx_codartl.NTSRepositoryItemText = Nothing
    Me.xx_codartl.OptionsColumn.AllowEdit = False
    Me.xx_codartl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codartl.OptionsColumn.ReadOnly = True
    Me.xx_codartl.OptionsFilter.AllowFilter = False
    Me.xx_codartl.Visible = True
    Me.xx_codartl.VisibleIndex = 6
    '
    'alv_codart
    '
    Me.alv_codart.AppearanceCell.Options.UseBackColor = True
    Me.alv_codart.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codart.Caption = "Cod. articolo"
    Me.alv_codart.Enabled = False
    Me.alv_codart.FieldName = "alv_codart"
    Me.alv_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codart.Name = "alv_codart"
    Me.alv_codart.NTSRepositoryComboBox = Nothing
    Me.alv_codart.NTSRepositoryItemCheck = Nothing
    Me.alv_codart.NTSRepositoryItemText = Nothing
    Me.alv_codart.OptionsColumn.AllowEdit = False
    Me.alv_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codart.OptionsColumn.ReadOnly = True
    Me.alv_codart.OptionsFilter.AllowFilter = False
    '
    'alv_multi
    '
    Me.alv_multi.AppearanceCell.Options.UseBackColor = True
    Me.alv_multi.AppearanceCell.Options.UseTextOptions = True
    Me.alv_multi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_multi.Caption = "Att. multi"
    Me.alv_multi.Enabled = False
    Me.alv_multi.FieldName = "alv_multi"
    Me.alv_multi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_multi.Name = "alv_multi"
    Me.alv_multi.NTSRepositoryComboBox = Nothing
    Me.alv_multi.NTSRepositoryItemCheck = Nothing
    Me.alv_multi.NTSRepositoryItemText = Nothing
    Me.alv_multi.OptionsColumn.AllowEdit = False
    Me.alv_multi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_multi.OptionsColumn.ReadOnly = True
    Me.alv_multi.OptionsFilter.AllowFilter = False
    '
    'alv_linkpr
    '
    Me.alv_linkpr.AppearanceCell.Options.UseBackColor = True
    Me.alv_linkpr.AppearanceCell.Options.UseTextOptions = True
    Me.alv_linkpr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_linkpr.Caption = "Linkpr"
    Me.alv_linkpr.Enabled = False
    Me.alv_linkpr.FieldName = "alv_linkpr"
    Me.alv_linkpr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_linkpr.Name = "alv_linkpr"
    Me.alv_linkpr.NTSRepositoryComboBox = Nothing
    Me.alv_linkpr.NTSRepositoryItemCheck = Nothing
    Me.alv_linkpr.NTSRepositoryItemText = Nothing
    Me.alv_linkpr.OptionsColumn.AllowEdit = False
    Me.alv_linkpr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_linkpr.OptionsColumn.ReadOnly = True
    Me.alv_linkpr.OptionsFilter.AllowFilter = False
    '
    'alv_linkric
    '
    Me.alv_linkric.AppearanceCell.Options.UseBackColor = True
    Me.alv_linkric.AppearanceCell.Options.UseTextOptions = True
    Me.alv_linkric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_linkric.Caption = "Link ric"
    Me.alv_linkric.Enabled = False
    Me.alv_linkric.FieldName = "alv_linkric"
    Me.alv_linkric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_linkric.Name = "alv_linkric"
    Me.alv_linkric.NTSRepositoryComboBox = Nothing
    Me.alv_linkric.NTSRepositoryItemCheck = Nothing
    Me.alv_linkric.NTSRepositoryItemText = Nothing
    Me.alv_linkric.OptionsColumn.AllowEdit = False
    Me.alv_linkric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_linkric.OptionsColumn.ReadOnly = True
    Me.alv_linkric.OptionsFilter.AllowFilter = False
    '
    'alv_gestling
    '
    Me.alv_gestling.AppearanceCell.Options.UseBackColor = True
    Me.alv_gestling.AppearanceCell.Options.UseTextOptions = True
    Me.alv_gestling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_gestling.Caption = "Gest. ling"
    Me.alv_gestling.Enabled = False
    Me.alv_gestling.FieldName = "alv_gestling"
    Me.alv_gestling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_gestling.Name = "alv_gestling"
    Me.alv_gestling.NTSRepositoryComboBox = Nothing
    Me.alv_gestling.NTSRepositoryItemCheck = Nothing
    Me.alv_gestling.NTSRepositoryItemText = Nothing
    Me.alv_gestling.OptionsColumn.AllowEdit = False
    Me.alv_gestling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_gestling.OptionsColumn.ReadOnly = True
    Me.alv_gestling.OptionsFilter.AllowFilter = False
    '
    'alv_codtipa
    '
    Me.alv_codtipa.AppearanceCell.Options.UseBackColor = True
    Me.alv_codtipa.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codtipa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codtipa.Caption = "Cod. tip. articolo"
    Me.alv_codtipa.Enabled = False
    Me.alv_codtipa.FieldName = "alv_codtipa"
    Me.alv_codtipa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codtipa.Name = "alv_codtipa"
    Me.alv_codtipa.NTSRepositoryComboBox = Nothing
    Me.alv_codtipa.NTSRepositoryItemCheck = Nothing
    Me.alv_codtipa.NTSRepositoryItemText = Nothing
    Me.alv_codtipa.OptionsColumn.AllowEdit = False
    Me.alv_codtipa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codtipa.OptionsColumn.ReadOnly = True
    Me.alv_codtipa.OptionsFilter.AllowFilter = False
    '
    'alv_descamp
    '
    Me.alv_descamp.AppearanceCell.Options.UseBackColor = True
    Me.alv_descamp.AppearanceCell.Options.UseTextOptions = True
    Me.alv_descamp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_descamp.Caption = "Descamp"
    Me.alv_descamp.Enabled = False
    Me.alv_descamp.FieldName = "alv_descamp"
    Me.alv_descamp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_descamp.Name = "alv_descamp"
    Me.alv_descamp.NTSRepositoryComboBox = Nothing
    Me.alv_descamp.NTSRepositoryItemCheck = Nothing
    Me.alv_descamp.NTSRepositoryItemText = Nothing
    Me.alv_descamp.OptionsColumn.AllowEdit = False
    Me.alv_descamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_descamp.OptionsColumn.ReadOnly = True
    Me.alv_descamp.OptionsFilter.AllowFilter = False
    '
    'alv_tipcamp
    '
    Me.alv_tipcamp.AppearanceCell.Options.UseBackColor = True
    Me.alv_tipcamp.AppearanceCell.Options.UseTextOptions = True
    Me.alv_tipcamp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_tipcamp.Caption = "Tipo campo"
    Me.alv_tipcamp.Enabled = False
    Me.alv_tipcamp.FieldName = "alv_tipcamp"
    Me.alv_tipcamp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_tipcamp.Name = "alv_tipcamp"
    Me.alv_tipcamp.NTSRepositoryComboBox = Nothing
    Me.alv_tipcamp.NTSRepositoryItemCheck = Nothing
    Me.alv_tipcamp.NTSRepositoryItemText = Nothing
    Me.alv_tipcamp.OptionsColumn.AllowEdit = False
    Me.alv_tipcamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_tipcamp.OptionsColumn.ReadOnly = True
    Me.alv_tipcamp.OptionsFilter.AllowFilter = False
    '
    'alv_flcamporic
    '
    Me.alv_flcamporic.AppearanceCell.Options.UseBackColor = True
    Me.alv_flcamporic.AppearanceCell.Options.UseTextOptions = True
    Me.alv_flcamporic.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_flcamporic.Caption = "Camp/att. di ricerca"
    Me.alv_flcamporic.Enabled = False
    Me.alv_flcamporic.FieldName = "alv_flcamporic"
    Me.alv_flcamporic.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_flcamporic.Name = "alv_flcamporic"
    Me.alv_flcamporic.NTSRepositoryComboBox = Nothing
    Me.alv_flcamporic.NTSRepositoryItemCheck = Nothing
    Me.alv_flcamporic.NTSRepositoryItemText = Nothing
    Me.alv_flcamporic.OptionsColumn.AllowEdit = False
    Me.alv_flcamporic.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_flcamporic.OptionsColumn.ReadOnly = True
    Me.alv_flcamporic.OptionsFilter.AllowFilter = False
    Me.alv_flcamporic.Visible = True
    Me.alv_flcamporic.VisibleIndex = 7
    '
    'alv_flurl
    '
    Me.alv_flurl.AppearanceCell.Options.UseBackColor = True
    Me.alv_flurl.AppearanceCell.Options.UseTextOptions = True
    Me.alv_flurl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_flurl.Caption = "Url"
    Me.alv_flurl.Enabled = False
    Me.alv_flurl.FieldName = "alv_flurl"
    Me.alv_flurl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_flurl.Name = "alv_flurl"
    Me.alv_flurl.NTSRepositoryComboBox = Nothing
    Me.alv_flurl.NTSRepositoryItemCheck = Nothing
    Me.alv_flurl.NTSRepositoryItemText = Nothing
    Me.alv_flurl.OptionsColumn.AllowEdit = False
    Me.alv_flurl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_flurl.OptionsColumn.ReadOnly = True
    Me.alv_flurl.OptionsFilter.AllowFilter = False
    Me.alv_flurl.Visible = True
    Me.alv_flurl.VisibleIndex = 8
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "codditt"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
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
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.grMulti)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBottom.Location = New System.Drawing.Point(0, 30)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(667, 212)
    Me.pnBottom.TabIndex = 5
    Me.pnBottom.Text = "NtsPanel1"
    '
    'FRMMGVAL2
    '
    Me.ClientSize = New System.Drawing.Size(667, 242)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGVAL2"
    Me.NTSLastControlFocussed = Me.grMulti
    Me.Text = "Valori"
    CType(Me.grMulti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMulti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvMulti.NTSSetParam(oMenu, oApp.Tr(Me, 128511031986669954, "Valori"))
      alv_ncampo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391092954955, "N. campo"), "0", 4, 0, 9999)
      alv_index.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391109353540, "Index"), "0", 4, 0, 9999)
      alv_valdouble.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391125595948, "Valdouble"), "#,##0.00", 15)
      alv_valtext.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391139964232, "Valtext"), 255, True)
      alv_valdata.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128788391164640198, "Valdata"), True)
      alv_valvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391181351137, "Valore variante"), 8, True)
      alv_desvvar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391195094713, "Desc. valore variante"), 60, True)
      alv_codvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391209931528, "Cod. variante"), 4, False)
      xx_codvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391226798644, "Desc. variante"), 0, True)
      alv_codling.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391239448981, "Cod. lingua"), "0", 4, 0, 9999)
      xx_codling.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391255379035, "Desc. lingua"), 0, True)
      alv_codartl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391293954754, "Cod. articolo (link)"), CLN__STD.CodartMaxLen, True)
      xx_codartl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391312071286, "Desc. articolo"), 0, True)
      alv_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391323628384, "Cod. articolo"), CLN__STD.CodartMaxLen, False)
      alv_multi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391340651677, "Att. multi"), 1, False)
      alv_linkpr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391353145837, "Linkpr"), 1, False)
      alv_linkric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391367670298, "Link ric"), 1, False)
      alv_gestling.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391383756529, "Gest. ling"), 1, False)
      alv_codtipa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391397187751, "Cod. tip. articolo"), "0", 4, 0, 9999)
      alv_descamp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391413742513, "Descamp"), 30, False)
      alv_tipcamp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788391428110797, "Tipo campo"), "0", 4, 0, 9999)
      alv_flcamporic.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788391442947612, "Camp/att. di ricerca"), "S", "N")
      alv_flurl.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788391460595613, "Url"), "S", "N")
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391475744782, "codditt"), 12, False)
      xx_valore2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788391497609562, "Valore"), 0, True)

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
  Public Overridable Sub InitEntity(ByRef cleArti As CLEMGARTI)
    oCleArti = cleArti
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvMulti.NTSSalvaRigaCorrente(dcVal2, oCleArti.Val2RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArti.Val2Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArti.Val2Ripristina(dcVal2.Position, dcVal2.Filter)
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
  Public Overridable Sub FRMMGVAL2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArti.Val2Apri(DittaCorrente, dsVal2) Then Me.Close()

      dcVal2.DataSource = dsVal2.Tables("VALARTI")
      grMulti.DataSource = dcVal2

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
  Public Overridable Sub FRMMGVAL2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcVal2.Dispose()
      dsVal2.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGVAL2_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvMulti.NTSNuovo()
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
      If grvMulti.NTSGetCurrentDataRow Is Nothing Then Return

      If Not grvMulti.NTSDeleteRigaCorrente(dcVal2, True) Then Return
      oCleArti.Val2Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvMulti.NTSRipristinaRigaCorrenteBefore(dcVal2, True) Then Return
      oCleArti.Val2Ripristina(dcVal2.Position, dcVal2.Filter)
      grvMulti.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------------------
      'questa funzione viene sempre scatenata quando premo F5 o ALT+F2 o ALT+F3 sulla griglia
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If ctrlTmp.GetType.ToString.IndexOf("NTSTextBox") > -1 Then
        SetFastZoom(CType(ctrlTmp, NTSTextBox).Text, oParam)    'abilito la gestione dello zoom veloce
        CType(ctrlTmp, NTSTextBox).Zoom()
      ElseIf ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grvMulti.FocusedColumn.Equals(alv_codartl) Then
          If grvMulti.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
          If NTSCStr(grvMulti.NTSGetCurrentDataRow()!alv_linkpr) = "N" Then Exit Sub
          NTSZOOM.strIn = NTSCStr(grvMulti.GetFocusedValue)
          oParam.nMagaz = 0
          oParam.nListino = 0
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvMulti.GetFocusedValue) Then grvMulti.SetFocusedValue(NTSZOOM.strIn)
        ElseIf grvMulti.FocusedColumn.Equals(alv_valvari) Then
          If grvMulti.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
          If NTSCStr(grvMulti.NTSGetCurrentDataRow()!alv_codvari) = "" Then Exit Sub
          NTSZOOM.strIn = NTSCStr(grvMulti.GetFocusedValue)
          oParam.strCodPdc = NTSCStr(grvMulti.NTSGetCurrentDataRow()!alv_codvari)
          NTSZOOM.ZoomStrIn("ZOOMVALVARI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvMulti.GetFocusedValue) Then grvMulti.SetFocusedValue(NTSZOOM.strIn)
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      Else
      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
      End If

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
  Public Overridable Sub grvMulti_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvMulti.NTSBeforeRowUpdate
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

  Public Overridable Sub grvMulti_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvMulti.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArti Is Nothing Then Return

      dtrT = grvMulti.NTSGetCurrentDataRow

      If dtrT Is Nothing Then Return

      If NTSCStr(dtrT!alv_linkpr) = "N" Then
        alv_codartl.Enabled = False
        xx_codartl.Enabled = False
      Else
        alv_codartl.Enabled = True
        xx_codartl.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class