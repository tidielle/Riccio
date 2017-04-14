Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORDQT1
  Public oCleGsor As CLEORGSOR
  Public oCallParams As CLE__CLDP

  Public dttTest As New DataTable
  Public dcTest As BindingSource = New BindingSource()
  Public dsDqta As New DataSet
  Public dcDqta As BindingSource = New BindingSource()
  Public dsGsor As DataSet                  'dataset di BNORGSOR

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
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

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORDQT1))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTco = New NTSInformatica.NTSGrid
    Me.grvTco = New NTSInformatica.NTSGridView
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.ec_desfase = New NTSInformatica.NTSGridColumn
    Me.ec_caption = New NTSInformatica.NTSGridColumn
    Me.ec_quant01 = New NTSInformatica.NTSGridColumn
    Me.ec_quant02 = New NTSInformatica.NTSGridColumn
    Me.ec_quant03 = New NTSInformatica.NTSGridColumn
    Me.ec_quant04 = New NTSInformatica.NTSGridColumn
    Me.ec_quant05 = New NTSInformatica.NTSGridColumn
    Me.ec_quant06 = New NTSInformatica.NTSGridColumn
    Me.ec_quant07 = New NTSInformatica.NTSGridColumn
    Me.ec_quant08 = New NTSInformatica.NTSGridColumn
    Me.ec_quant09 = New NTSInformatica.NTSGridColumn
    Me.ec_quant10 = New NTSInformatica.NTSGridColumn
    Me.ec_quant11 = New NTSInformatica.NTSGridColumn
    Me.ec_quant12 = New NTSInformatica.NTSGridColumn
    Me.ec_quant13 = New NTSInformatica.NTSGridColumn
    Me.ec_quant14 = New NTSInformatica.NTSGridColumn
    Me.ec_quant15 = New NTSInformatica.NTSGridColumn
    Me.ec_quant16 = New NTSInformatica.NTSGridColumn
    Me.ec_quant17 = New NTSInformatica.NTSGridColumn
    Me.ec_quant18 = New NTSInformatica.NTSGridColumn
    Me.ec_quant19 = New NTSInformatica.NTSGridColumn
    Me.ec_quant20 = New NTSInformatica.NTSGridColumn
    Me.ec_quant21 = New NTSInformatica.NTSGridColumn
    Me.ec_quant22 = New NTSInformatica.NTSGridColumn
    Me.ec_quant23 = New NTSInformatica.NTSGridColumn
    Me.ec_quant24 = New NTSInformatica.NTSGridColumn
    Me.grTest = New NTSInformatica.NTSGrid
    Me.grvTest = New NTSInformatica.NTSGridView
    Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
    Me.xxo_destagl = New NTSInformatica.NTSGridColumn
    Me.SplitCliHoriz = New NTSInformatica.NTSSplitter
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTest, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTest, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grTco
    '
    Me.grTco.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTco.EmbeddedNavigator.Name = ""
    Me.grTco.Location = New System.Drawing.Point(0, 125)
    Me.grTco.MainView = Me.grvTco
    Me.grTco.Name = "grTco"
    Me.grTco.Size = New System.Drawing.Size(631, 228)
    Me.grTco.TabIndex = 4
    Me.grTco.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTco})
    '
    'grvTco
    '
    Me.grvTco.ActiveFilterEnabled = False
    Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_riga, Me.ec_codart, Me.ec_descr, Me.ec_fase, Me.ec_desfase, Me.ec_caption, Me.ec_quant01, Me.ec_quant02, Me.ec_quant03, Me.ec_quant04, Me.ec_quant05, Me.ec_quant06, Me.ec_quant07, Me.ec_quant08, Me.ec_quant09, Me.ec_quant10, Me.ec_quant11, Me.ec_quant12, Me.ec_quant13, Me.ec_quant14, Me.ec_quant15, Me.ec_quant16, Me.ec_quant17, Me.ec_quant18, Me.ec_quant19, Me.ec_quant20, Me.ec_quant21, Me.ec_quant22, Me.ec_quant23, Me.ec_quant24})
    Me.grvTco.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTco.Enabled = True
    Me.grvTco.GridControl = Me.grTco
    Me.grvTco.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTco.MinRowHeight = 14
    Me.grvTco.Name = "grvTco"
    Me.grvTco.NTSAllowDelete = True
    Me.grvTco.NTSAllowInsert = True
    Me.grvTco.NTSAllowUpdate = True
    Me.grvTco.NTSMenuContext = Nothing
    Me.grvTco.OptionsCustomization.AllowRowSizing = True
    Me.grvTco.OptionsFilter.AllowFilterEditor = False
    Me.grvTco.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTco.OptionsNavigation.UseTabKey = False
    Me.grvTco.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTco.OptionsView.ColumnAutoWidth = False
    Me.grvTco.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTco.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTco.OptionsView.ShowGroupPanel = False
    Me.grvTco.RowHeight = 14
    '
    'ec_riga
    '
    Me.ec_riga.AppearanceCell.Options.UseBackColor = True
    Me.ec_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_riga.Caption = "Riga"
    Me.ec_riga.Enabled = False
    Me.ec_riga.FieldName = "ec_riga"
    Me.ec_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_riga.Name = "ec_riga"
    Me.ec_riga.NTSRepositoryComboBox = Nothing
    Me.ec_riga.NTSRepositoryItemCheck = Nothing
    Me.ec_riga.NTSRepositoryItemMemo = Nothing
    Me.ec_riga.NTSRepositoryItemText = Nothing
    Me.ec_riga.OptionsColumn.AllowEdit = False
    Me.ec_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_riga.OptionsColumn.ReadOnly = True
    Me.ec_riga.OptionsFilter.AllowFilter = False
    Me.ec_riga.Visible = True
    Me.ec_riga.VisibleIndex = 0
    '
    'ec_codart
    '
    Me.ec_codart.AppearanceCell.Options.UseBackColor = True
    Me.ec_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codart.Caption = "Articolo"
    Me.ec_codart.Enabled = False
    Me.ec_codart.FieldName = "ec_codart"
    Me.ec_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codart.Name = "ec_codart"
    Me.ec_codart.NTSRepositoryComboBox = Nothing
    Me.ec_codart.NTSRepositoryItemCheck = Nothing
    Me.ec_codart.NTSRepositoryItemMemo = Nothing
    Me.ec_codart.NTSRepositoryItemText = Nothing
    Me.ec_codart.OptionsColumn.AllowEdit = False
    Me.ec_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codart.OptionsColumn.ReadOnly = True
    Me.ec_codart.OptionsFilter.AllowFilter = False
    Me.ec_codart.Visible = True
    Me.ec_codart.VisibleIndex = 1
    '
    'ec_descr
    '
    Me.ec_descr.AppearanceCell.Options.UseBackColor = True
    Me.ec_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_descr.Caption = "Descr. articolo"
    Me.ec_descr.Enabled = False
    Me.ec_descr.FieldName = "ec_descr"
    Me.ec_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_descr.Name = "ec_descr"
    Me.ec_descr.NTSRepositoryComboBox = Nothing
    Me.ec_descr.NTSRepositoryItemCheck = Nothing
    Me.ec_descr.NTSRepositoryItemMemo = Nothing
    Me.ec_descr.NTSRepositoryItemText = Nothing
    Me.ec_descr.OptionsColumn.AllowEdit = False
    Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_descr.OptionsColumn.ReadOnly = True
    Me.ec_descr.OptionsFilter.AllowFilter = False
    Me.ec_descr.Visible = True
    Me.ec_descr.VisibleIndex = 2
    '
    'ec_fase
    '
    Me.ec_fase.AppearanceCell.Options.UseBackColor = True
    Me.ec_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_fase.Caption = "Fase"
    Me.ec_fase.Enabled = False
    Me.ec_fase.FieldName = "ec_fase"
    Me.ec_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_fase.Name = "ec_fase"
    Me.ec_fase.NTSRepositoryComboBox = Nothing
    Me.ec_fase.NTSRepositoryItemCheck = Nothing
    Me.ec_fase.NTSRepositoryItemMemo = Nothing
    Me.ec_fase.NTSRepositoryItemText = Nothing
    Me.ec_fase.OptionsColumn.AllowEdit = False
    Me.ec_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_fase.OptionsColumn.ReadOnly = True
    Me.ec_fase.OptionsFilter.AllowFilter = False
    '
    'ec_desfase
    '
    Me.ec_desfase.AppearanceCell.Options.UseBackColor = True
    Me.ec_desfase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_desfase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_desfase.Caption = "Descr. fase"
    Me.ec_desfase.Enabled = False
    Me.ec_desfase.FieldName = "ec_desfase"
    Me.ec_desfase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_desfase.Name = "ec_desfase"
    Me.ec_desfase.NTSRepositoryComboBox = Nothing
    Me.ec_desfase.NTSRepositoryItemCheck = Nothing
    Me.ec_desfase.NTSRepositoryItemMemo = Nothing
    Me.ec_desfase.NTSRepositoryItemText = Nothing
    Me.ec_desfase.OptionsColumn.AllowEdit = False
    Me.ec_desfase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_desfase.OptionsColumn.ReadOnly = True
    Me.ec_desfase.OptionsFilter.AllowFilter = False
    '
    'ec_caption
    '
    Me.ec_caption.AppearanceCell.Options.UseBackColor = True
    Me.ec_caption.AppearanceCell.Options.UseTextOptions = True
    Me.ec_caption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_caption.Caption = "    "
    Me.ec_caption.Enabled = False
    Me.ec_caption.FieldName = "ec_caption"
    Me.ec_caption.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_caption.Name = "ec_caption"
    Me.ec_caption.NTSRepositoryComboBox = Nothing
    Me.ec_caption.NTSRepositoryItemCheck = Nothing
    Me.ec_caption.NTSRepositoryItemMemo = Nothing
    Me.ec_caption.NTSRepositoryItemText = Nothing
    Me.ec_caption.OptionsColumn.AllowEdit = False
    Me.ec_caption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_caption.OptionsColumn.ReadOnly = True
    Me.ec_caption.OptionsFilter.AllowFilter = False
    '
    'ec_quant01
    '
    Me.ec_quant01.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant01.Caption = "QTA1"
    Me.ec_quant01.Enabled = False
    Me.ec_quant01.FieldName = "ec_quant01"
    Me.ec_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant01.Name = "ec_quant01"
    Me.ec_quant01.NTSRepositoryComboBox = Nothing
    Me.ec_quant01.NTSRepositoryItemCheck = Nothing
    Me.ec_quant01.NTSRepositoryItemMemo = Nothing
    Me.ec_quant01.NTSRepositoryItemText = Nothing
    Me.ec_quant01.OptionsColumn.AllowEdit = False
    Me.ec_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant01.OptionsColumn.ReadOnly = True
    Me.ec_quant01.OptionsFilter.AllowFilter = False
    Me.ec_quant01.Visible = True
    Me.ec_quant01.VisibleIndex = 3
    '
    'ec_quant02
    '
    Me.ec_quant02.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant02.Caption = "QTA2"
    Me.ec_quant02.Enabled = False
    Me.ec_quant02.FieldName = "ec_quant02"
    Me.ec_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant02.Name = "ec_quant02"
    Me.ec_quant02.NTSRepositoryComboBox = Nothing
    Me.ec_quant02.NTSRepositoryItemCheck = Nothing
    Me.ec_quant02.NTSRepositoryItemMemo = Nothing
    Me.ec_quant02.NTSRepositoryItemText = Nothing
    Me.ec_quant02.OptionsColumn.AllowEdit = False
    Me.ec_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant02.OptionsColumn.ReadOnly = True
    Me.ec_quant02.OptionsFilter.AllowFilter = False
    Me.ec_quant02.Visible = True
    Me.ec_quant02.VisibleIndex = 4
    '
    'ec_quant03
    '
    Me.ec_quant03.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant03.Caption = "QTA3"
    Me.ec_quant03.Enabled = False
    Me.ec_quant03.FieldName = "ec_quant03"
    Me.ec_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant03.Name = "ec_quant03"
    Me.ec_quant03.NTSRepositoryComboBox = Nothing
    Me.ec_quant03.NTSRepositoryItemCheck = Nothing
    Me.ec_quant03.NTSRepositoryItemMemo = Nothing
    Me.ec_quant03.NTSRepositoryItemText = Nothing
    Me.ec_quant03.OptionsColumn.AllowEdit = False
    Me.ec_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant03.OptionsColumn.ReadOnly = True
    Me.ec_quant03.OptionsFilter.AllowFilter = False
    Me.ec_quant03.Visible = True
    Me.ec_quant03.VisibleIndex = 5
    '
    'ec_quant04
    '
    Me.ec_quant04.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant04.Caption = "QTA4"
    Me.ec_quant04.Enabled = False
    Me.ec_quant04.FieldName = "ec_quant04"
    Me.ec_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant04.Name = "ec_quant04"
    Me.ec_quant04.NTSRepositoryComboBox = Nothing
    Me.ec_quant04.NTSRepositoryItemCheck = Nothing
    Me.ec_quant04.NTSRepositoryItemMemo = Nothing
    Me.ec_quant04.NTSRepositoryItemText = Nothing
    Me.ec_quant04.OptionsColumn.AllowEdit = False
    Me.ec_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant04.OptionsColumn.ReadOnly = True
    Me.ec_quant04.OptionsFilter.AllowFilter = False
    Me.ec_quant04.Visible = True
    Me.ec_quant04.VisibleIndex = 6
    '
    'ec_quant05
    '
    Me.ec_quant05.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant05.Caption = "QTA5"
    Me.ec_quant05.Enabled = False
    Me.ec_quant05.FieldName = "ec_quant05"
    Me.ec_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant05.Name = "ec_quant05"
    Me.ec_quant05.NTSRepositoryComboBox = Nothing
    Me.ec_quant05.NTSRepositoryItemCheck = Nothing
    Me.ec_quant05.NTSRepositoryItemMemo = Nothing
    Me.ec_quant05.NTSRepositoryItemText = Nothing
    Me.ec_quant05.OptionsColumn.AllowEdit = False
    Me.ec_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant05.OptionsColumn.ReadOnly = True
    Me.ec_quant05.OptionsFilter.AllowFilter = False
    Me.ec_quant05.Visible = True
    Me.ec_quant05.VisibleIndex = 7
    '
    'ec_quant06
    '
    Me.ec_quant06.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant06.Caption = "QTA6"
    Me.ec_quant06.Enabled = False
    Me.ec_quant06.FieldName = "ec_quant06"
    Me.ec_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant06.Name = "ec_quant06"
    Me.ec_quant06.NTSRepositoryComboBox = Nothing
    Me.ec_quant06.NTSRepositoryItemCheck = Nothing
    Me.ec_quant06.NTSRepositoryItemMemo = Nothing
    Me.ec_quant06.NTSRepositoryItemText = Nothing
    Me.ec_quant06.OptionsColumn.AllowEdit = False
    Me.ec_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant06.OptionsColumn.ReadOnly = True
    Me.ec_quant06.OptionsFilter.AllowFilter = False
    Me.ec_quant06.Visible = True
    Me.ec_quant06.VisibleIndex = 8
    '
    'ec_quant07
    '
    Me.ec_quant07.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant07.Caption = "QTA7"
    Me.ec_quant07.Enabled = False
    Me.ec_quant07.FieldName = "ec_quant07"
    Me.ec_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant07.Name = "ec_quant07"
    Me.ec_quant07.NTSRepositoryComboBox = Nothing
    Me.ec_quant07.NTSRepositoryItemCheck = Nothing
    Me.ec_quant07.NTSRepositoryItemMemo = Nothing
    Me.ec_quant07.NTSRepositoryItemText = Nothing
    Me.ec_quant07.OptionsColumn.AllowEdit = False
    Me.ec_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant07.OptionsColumn.ReadOnly = True
    Me.ec_quant07.OptionsFilter.AllowFilter = False
    Me.ec_quant07.Visible = True
    Me.ec_quant07.VisibleIndex = 9
    '
    'ec_quant08
    '
    Me.ec_quant08.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant08.Caption = "QTA8"
    Me.ec_quant08.Enabled = False
    Me.ec_quant08.FieldName = "ec_quant08"
    Me.ec_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant08.Name = "ec_quant08"
    Me.ec_quant08.NTSRepositoryComboBox = Nothing
    Me.ec_quant08.NTSRepositoryItemCheck = Nothing
    Me.ec_quant08.NTSRepositoryItemMemo = Nothing
    Me.ec_quant08.NTSRepositoryItemText = Nothing
    Me.ec_quant08.OptionsColumn.AllowEdit = False
    Me.ec_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant08.OptionsColumn.ReadOnly = True
    Me.ec_quant08.OptionsFilter.AllowFilter = False
    Me.ec_quant08.Visible = True
    Me.ec_quant08.VisibleIndex = 10
    '
    'ec_quant09
    '
    Me.ec_quant09.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant09.Caption = "QTA9"
    Me.ec_quant09.Enabled = False
    Me.ec_quant09.FieldName = "ec_quant09"
    Me.ec_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant09.Name = "ec_quant09"
    Me.ec_quant09.NTSRepositoryComboBox = Nothing
    Me.ec_quant09.NTSRepositoryItemCheck = Nothing
    Me.ec_quant09.NTSRepositoryItemMemo = Nothing
    Me.ec_quant09.NTSRepositoryItemText = Nothing
    Me.ec_quant09.OptionsColumn.AllowEdit = False
    Me.ec_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant09.OptionsColumn.ReadOnly = True
    Me.ec_quant09.OptionsFilter.AllowFilter = False
    Me.ec_quant09.Visible = True
    Me.ec_quant09.VisibleIndex = 11
    '
    'ec_quant10
    '
    Me.ec_quant10.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant10.Caption = "QTA10"
    Me.ec_quant10.Enabled = False
    Me.ec_quant10.FieldName = "ec_quant10"
    Me.ec_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant10.Name = "ec_quant10"
    Me.ec_quant10.NTSRepositoryComboBox = Nothing
    Me.ec_quant10.NTSRepositoryItemCheck = Nothing
    Me.ec_quant10.NTSRepositoryItemMemo = Nothing
    Me.ec_quant10.NTSRepositoryItemText = Nothing
    Me.ec_quant10.OptionsColumn.AllowEdit = False
    Me.ec_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant10.OptionsColumn.ReadOnly = True
    Me.ec_quant10.OptionsFilter.AllowFilter = False
    Me.ec_quant10.Visible = True
    Me.ec_quant10.VisibleIndex = 12
    '
    'ec_quant11
    '
    Me.ec_quant11.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant11.Caption = "QTA11"
    Me.ec_quant11.Enabled = False
    Me.ec_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant11.Name = "ec_quant11"
    Me.ec_quant11.NTSRepositoryComboBox = Nothing
    Me.ec_quant11.NTSRepositoryItemCheck = Nothing
    Me.ec_quant11.NTSRepositoryItemMemo = Nothing
    Me.ec_quant11.NTSRepositoryItemText = Nothing
    Me.ec_quant11.OptionsColumn.AllowEdit = False
    Me.ec_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant11.OptionsColumn.ReadOnly = True
    Me.ec_quant11.OptionsFilter.AllowFilter = False
    Me.ec_quant11.Visible = True
    Me.ec_quant11.VisibleIndex = 13
    '
    'ec_quant12
    '
    Me.ec_quant12.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant12.Caption = "QTA12"
    Me.ec_quant12.Enabled = False
    Me.ec_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant12.Name = "ec_quant12"
    Me.ec_quant12.NTSRepositoryComboBox = Nothing
    Me.ec_quant12.NTSRepositoryItemCheck = Nothing
    Me.ec_quant12.NTSRepositoryItemMemo = Nothing
    Me.ec_quant12.NTSRepositoryItemText = Nothing
    Me.ec_quant12.OptionsColumn.AllowEdit = False
    Me.ec_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant12.OptionsColumn.ReadOnly = True
    Me.ec_quant12.OptionsFilter.AllowFilter = False
    Me.ec_quant12.Visible = True
    Me.ec_quant12.VisibleIndex = 14
    '
    'ec_quant13
    '
    Me.ec_quant13.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant13.Caption = "QTA13"
    Me.ec_quant13.Enabled = False
    Me.ec_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant13.Name = "ec_quant13"
    Me.ec_quant13.NTSRepositoryComboBox = Nothing
    Me.ec_quant13.NTSRepositoryItemCheck = Nothing
    Me.ec_quant13.NTSRepositoryItemMemo = Nothing
    Me.ec_quant13.NTSRepositoryItemText = Nothing
    Me.ec_quant13.OptionsColumn.AllowEdit = False
    Me.ec_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant13.OptionsColumn.ReadOnly = True
    Me.ec_quant13.OptionsFilter.AllowFilter = False
    Me.ec_quant13.Visible = True
    Me.ec_quant13.VisibleIndex = 15
    '
    'ec_quant14
    '
    Me.ec_quant14.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant14.Caption = "QTA14"
    Me.ec_quant14.Enabled = False
    Me.ec_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant14.Name = "ec_quant14"
    Me.ec_quant14.NTSRepositoryComboBox = Nothing
    Me.ec_quant14.NTSRepositoryItemCheck = Nothing
    Me.ec_quant14.NTSRepositoryItemMemo = Nothing
    Me.ec_quant14.NTSRepositoryItemText = Nothing
    Me.ec_quant14.OptionsColumn.AllowEdit = False
    Me.ec_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant14.OptionsColumn.ReadOnly = True
    Me.ec_quant14.OptionsFilter.AllowFilter = False
    Me.ec_quant14.Visible = True
    Me.ec_quant14.VisibleIndex = 16
    '
    'ec_quant15
    '
    Me.ec_quant15.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant15.Caption = "QTA15"
    Me.ec_quant15.Enabled = False
    Me.ec_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant15.Name = "ec_quant15"
    Me.ec_quant15.NTSRepositoryComboBox = Nothing
    Me.ec_quant15.NTSRepositoryItemCheck = Nothing
    Me.ec_quant15.NTSRepositoryItemMemo = Nothing
    Me.ec_quant15.NTSRepositoryItemText = Nothing
    Me.ec_quant15.OptionsColumn.AllowEdit = False
    Me.ec_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant15.OptionsColumn.ReadOnly = True
    Me.ec_quant15.OptionsFilter.AllowFilter = False
    Me.ec_quant15.Visible = True
    Me.ec_quant15.VisibleIndex = 17
    '
    'ec_quant16
    '
    Me.ec_quant16.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant16.Caption = "QTA16"
    Me.ec_quant16.Enabled = False
    Me.ec_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant16.Name = "ec_quant16"
    Me.ec_quant16.NTSRepositoryComboBox = Nothing
    Me.ec_quant16.NTSRepositoryItemCheck = Nothing
    Me.ec_quant16.NTSRepositoryItemMemo = Nothing
    Me.ec_quant16.NTSRepositoryItemText = Nothing
    Me.ec_quant16.OptionsColumn.AllowEdit = False
    Me.ec_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant16.OptionsColumn.ReadOnly = True
    Me.ec_quant16.OptionsFilter.AllowFilter = False
    Me.ec_quant16.Visible = True
    Me.ec_quant16.VisibleIndex = 18
    '
    'ec_quant17
    '
    Me.ec_quant17.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant17.Caption = "QTA17"
    Me.ec_quant17.Enabled = False
    Me.ec_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant17.Name = "ec_quant17"
    Me.ec_quant17.NTSRepositoryComboBox = Nothing
    Me.ec_quant17.NTSRepositoryItemCheck = Nothing
    Me.ec_quant17.NTSRepositoryItemMemo = Nothing
    Me.ec_quant17.NTSRepositoryItemText = Nothing
    Me.ec_quant17.OptionsColumn.AllowEdit = False
    Me.ec_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant17.OptionsColumn.ReadOnly = True
    Me.ec_quant17.OptionsFilter.AllowFilter = False
    Me.ec_quant17.Visible = True
    Me.ec_quant17.VisibleIndex = 19
    '
    'ec_quant18
    '
    Me.ec_quant18.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant18.Caption = "QTA18"
    Me.ec_quant18.Enabled = False
    Me.ec_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant18.Name = "ec_quant18"
    Me.ec_quant18.NTSRepositoryComboBox = Nothing
    Me.ec_quant18.NTSRepositoryItemCheck = Nothing
    Me.ec_quant18.NTSRepositoryItemMemo = Nothing
    Me.ec_quant18.NTSRepositoryItemText = Nothing
    Me.ec_quant18.OptionsColumn.AllowEdit = False
    Me.ec_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant18.OptionsColumn.ReadOnly = True
    Me.ec_quant18.OptionsFilter.AllowFilter = False
    Me.ec_quant18.Visible = True
    Me.ec_quant18.VisibleIndex = 20
    '
    'ec_quant19
    '
    Me.ec_quant19.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant19.Caption = "QTA19"
    Me.ec_quant19.Enabled = False
    Me.ec_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant19.Name = "ec_quant19"
    Me.ec_quant19.NTSRepositoryComboBox = Nothing
    Me.ec_quant19.NTSRepositoryItemCheck = Nothing
    Me.ec_quant19.NTSRepositoryItemMemo = Nothing
    Me.ec_quant19.NTSRepositoryItemText = Nothing
    Me.ec_quant19.OptionsColumn.AllowEdit = False
    Me.ec_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant19.OptionsColumn.ReadOnly = True
    Me.ec_quant19.OptionsFilter.AllowFilter = False
    Me.ec_quant19.Visible = True
    Me.ec_quant19.VisibleIndex = 21
    '
    'ec_quant20
    '
    Me.ec_quant20.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant20.Caption = "QTA20"
    Me.ec_quant20.Enabled = False
    Me.ec_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant20.Name = "ec_quant20"
    Me.ec_quant20.NTSRepositoryComboBox = Nothing
    Me.ec_quant20.NTSRepositoryItemCheck = Nothing
    Me.ec_quant20.NTSRepositoryItemMemo = Nothing
    Me.ec_quant20.NTSRepositoryItemText = Nothing
    Me.ec_quant20.OptionsColumn.AllowEdit = False
    Me.ec_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant20.OptionsColumn.ReadOnly = True
    Me.ec_quant20.OptionsFilter.AllowFilter = False
    Me.ec_quant20.Visible = True
    Me.ec_quant20.VisibleIndex = 22
    '
    'ec_quant21
    '
    Me.ec_quant21.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant21.Caption = "QTA21"
    Me.ec_quant21.Enabled = False
    Me.ec_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant21.Name = "ec_quant21"
    Me.ec_quant21.NTSRepositoryComboBox = Nothing
    Me.ec_quant21.NTSRepositoryItemCheck = Nothing
    Me.ec_quant21.NTSRepositoryItemMemo = Nothing
    Me.ec_quant21.NTSRepositoryItemText = Nothing
    Me.ec_quant21.OptionsColumn.AllowEdit = False
    Me.ec_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant21.OptionsColumn.ReadOnly = True
    Me.ec_quant21.OptionsFilter.AllowFilter = False
    Me.ec_quant21.Visible = True
    Me.ec_quant21.VisibleIndex = 23
    '
    'ec_quant22
    '
    Me.ec_quant22.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant22.Caption = "QTA22"
    Me.ec_quant22.Enabled = False
    Me.ec_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant22.Name = "ec_quant22"
    Me.ec_quant22.NTSRepositoryComboBox = Nothing
    Me.ec_quant22.NTSRepositoryItemCheck = Nothing
    Me.ec_quant22.NTSRepositoryItemMemo = Nothing
    Me.ec_quant22.NTSRepositoryItemText = Nothing
    Me.ec_quant22.OptionsColumn.AllowEdit = False
    Me.ec_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant22.OptionsColumn.ReadOnly = True
    Me.ec_quant22.OptionsFilter.AllowFilter = False
    Me.ec_quant22.Visible = True
    Me.ec_quant22.VisibleIndex = 24
    '
    'ec_quant23
    '
    Me.ec_quant23.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant23.Caption = "QTA23"
    Me.ec_quant23.Enabled = False
    Me.ec_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant23.Name = "ec_quant23"
    Me.ec_quant23.NTSRepositoryComboBox = Nothing
    Me.ec_quant23.NTSRepositoryItemCheck = Nothing
    Me.ec_quant23.NTSRepositoryItemMemo = Nothing
    Me.ec_quant23.NTSRepositoryItemText = Nothing
    Me.ec_quant23.OptionsColumn.AllowEdit = False
    Me.ec_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant23.OptionsColumn.ReadOnly = True
    Me.ec_quant23.OptionsFilter.AllowFilter = False
    Me.ec_quant23.Visible = True
    Me.ec_quant23.VisibleIndex = 25
    '
    'ec_quant24
    '
    Me.ec_quant24.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant24.Caption = "QTA24"
    Me.ec_quant24.Enabled = False
    Me.ec_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant24.Name = "ec_quant24"
    Me.ec_quant24.NTSRepositoryComboBox = Nothing
    Me.ec_quant24.NTSRepositoryItemCheck = Nothing
    Me.ec_quant24.NTSRepositoryItemMemo = Nothing
    Me.ec_quant24.NTSRepositoryItemText = Nothing
    Me.ec_quant24.OptionsColumn.AllowEdit = False
    Me.ec_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant24.OptionsColumn.ReadOnly = True
    Me.ec_quant24.OptionsFilter.AllowFilter = False
    Me.ec_quant24.Visible = True
    Me.ec_quant24.VisibleIndex = 26
    '
    'grTest
    '
    Me.grTest.Dock = System.Windows.Forms.DockStyle.Top
    '
    '
    '
    Me.grTest.EmbeddedNavigator.Name = ""
    Me.grTest.Location = New System.Drawing.Point(0, 24)
    Me.grTest.MainView = Me.grvTest
    Me.grTest.Name = "grTest"
    Me.grTest.Size = New System.Drawing.Size(631, 100)
    Me.grTest.TabIndex = 5
    Me.grTest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTest})
    '
    'grvTest
    '
    Me.grvTest.ActiveFilterEnabled = False
    Me.grvTest.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xxo_codtagl, Me.xxo_destagl})
    Me.grvTest.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTest.Enabled = True
    Me.grvTest.GridControl = Me.grTest
    Me.grvTest.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTest.MinRowHeight = 14
    Me.grvTest.Name = "grvTest"
    Me.grvTest.NTSAllowDelete = True
    Me.grvTest.NTSAllowInsert = True
    Me.grvTest.NTSAllowUpdate = True
    Me.grvTest.NTSMenuContext = Nothing
    Me.grvTest.OptionsCustomization.AllowRowSizing = True
    Me.grvTest.OptionsFilter.AllowFilterEditor = False
    Me.grvTest.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTest.OptionsNavigation.UseTabKey = False
    Me.grvTest.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTest.OptionsView.ColumnAutoWidth = False
    Me.grvTest.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTest.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTest.OptionsView.ShowGroupPanel = False
    Me.grvTest.RowHeight = 14
    '
    'xxo_codtagl
    '
    Me.xxo_codtagl.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codtagl.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codtagl.Caption = "Codice"
    Me.xxo_codtagl.Enabled = False
    Me.xxo_codtagl.FieldName = "xxo_codtagl"
    Me.xxo_codtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codtagl.Name = "xxo_codtagl"
    Me.xxo_codtagl.NTSRepositoryComboBox = Nothing
    Me.xxo_codtagl.NTSRepositoryItemCheck = Nothing
    Me.xxo_codtagl.NTSRepositoryItemMemo = Nothing
    Me.xxo_codtagl.NTSRepositoryItemText = Nothing
    Me.xxo_codtagl.OptionsColumn.AllowEdit = False
    Me.xxo_codtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codtagl.OptionsColumn.ReadOnly = True
    Me.xxo_codtagl.OptionsFilter.AllowFilter = False
    Me.xxo_codtagl.Visible = True
    Me.xxo_codtagl.VisibleIndex = 0
    '
    'xxo_destagl
    '
    Me.xxo_destagl.AppearanceCell.Options.UseBackColor = True
    Me.xxo_destagl.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_destagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_destagl.Caption = "Descrizione"
    Me.xxo_destagl.Enabled = False
    Me.xxo_destagl.FieldName = "xxo_destagl"
    Me.xxo_destagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_destagl.Name = "xxo_destagl"
    Me.xxo_destagl.NTSRepositoryComboBox = Nothing
    Me.xxo_destagl.NTSRepositoryItemCheck = Nothing
    Me.xxo_destagl.NTSRepositoryItemMemo = Nothing
    Me.xxo_destagl.NTSRepositoryItemText = Nothing
    Me.xxo_destagl.OptionsColumn.AllowEdit = False
    Me.xxo_destagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_destagl.OptionsColumn.ReadOnly = True
    Me.xxo_destagl.OptionsFilter.AllowFilter = False
    Me.xxo_destagl.Visible = True
    Me.xxo_destagl.VisibleIndex = 1
    '
    'SplitCliHoriz
    '
    Me.SplitCliHoriz.Dock = System.Windows.Forms.DockStyle.Top
    Me.SplitCliHoriz.Location = New System.Drawing.Point(0, 124)
    Me.SplitCliHoriz.Name = "SplitCliHoriz"
    Me.SplitCliHoriz.Size = New System.Drawing.Size(631, 1)
    Me.SplitCliHoriz.TabIndex = 13
    Me.SplitCliHoriz.TabStop = False
    '
    'FRMORDQT1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(631, 353)
    Me.Controls.Add(Me.grTco)
    Me.Controls.Add(Me.SplitCliHoriz)
    Me.Controls.Add(Me.grTest)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMORDQT1"
    Me.Text = "DETTAGLIO TAGLIE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTest, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTest, System.ComponentModel.ISupportInitialize).EndInit()
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
      grvTest.NTSSetParam(oMenu, oApp.Tr(Me, 130005797327938555, "Griglia testata taglie"))
      xxo_codtagl.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130005797780856337, "Codice"), tabtagl)
      xxo_destagl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130005797982621897, "Descrizione"), 0, True)
      grvTest.NTSAllowDelete = False
      grvTest.NTSAllowUpdate = False
      grvTest.NTSAllowInsert = False
      '--------------------------------------------------------------------------------------------------------------
      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 128517787751994000, "Griglia dettaglio taglie"))
      ec_riga.NTSSetParamNUM(oMenu, "Riga", "0", 6)
      ec_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457546406250, "Codice articolo"), 0, True)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457556406250, "Descrizione"), 0, True)
      ec_caption.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128556540578040000, "Tipo riga"), 0, True)
      ec_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128685783617187500, "Fase"), "0", 3)
      ec_desfase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128685783591406250, "Descrizione fase"), 0, True)
      ec_quant01.NTSSetParamNUM(oMenu, "Quantità 1^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant02.NTSSetParamNUM(oMenu, "Quantità 2^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant03.NTSSetParamNUM(oMenu, "Quantità 3^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant04.NTSSetParamNUM(oMenu, "Quantità 4^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant05.NTSSetParamNUM(oMenu, "Quantità 5^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant06.NTSSetParamNUM(oMenu, "Quantità 6^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant07.NTSSetParamNUM(oMenu, "Quantità 7^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant08.NTSSetParamNUM(oMenu, "Quantità 8^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant09.NTSSetParamNUM(oMenu, "Quantità 9^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant10.NTSSetParamNUM(oMenu, "Quantità 10^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant11.NTSSetParamNUM(oMenu, "Quantità 11^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant12.NTSSetParamNUM(oMenu, "Quantità 12^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant13.NTSSetParamNUM(oMenu, "Quantità 13^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant14.NTSSetParamNUM(oMenu, "Quantità 14^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant15.NTSSetParamNUM(oMenu, "Quantità 15^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant16.NTSSetParamNUM(oMenu, "Quantità 16^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant17.NTSSetParamNUM(oMenu, "Quantità 17^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant18.NTSSetParamNUM(oMenu, "Quantità 18^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant19.NTSSetParamNUM(oMenu, "Quantità 19^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant20.NTSSetParamNUM(oMenu, "Quantità 20^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant21.NTSSetParamNUM(oMenu, "Quantità 21^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant22.NTSSetParamNUM(oMenu, "Quantità 22^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant23.NTSSetParamNUM(oMenu, "Quantità 23^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant24.NTSSetParamNUM(oMenu, "Quantità 24^ taglia", "#,##0", 6, -99999, 99999)
      grvTco.NTSAllowDelete = False
      grvTco.NTSAllowUpdate = False
      grvTco.NTSAllowInsert = False
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

  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR, ByRef dtrCorpo As DataRow)
    oCleGsor = cleGsor
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMORDQT1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If ApriTestata() = False Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi di Griglia"
  Public Overridable Sub grvTest_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTest.NTSFocusedRowChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      ApriCorpo(NTSCInt(grvTest.NTSGetCurrentDataRow!xxo_codtagl))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function ApriCorpo(ByVal nCodtagl As Integer) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT2() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Fra un clone del DataSet dei dettagli Taglie & Colori de documento corrente
      '--------------------------------------------------------------------------------------------------------------
      If dsDqta.Tables.Contains("CORPOTC") = False Then dsDqta.Tables.Add(dsGsor.Tables("CORPOTC").Clone())
      '--------------------------------------------------------------------------------------------------------------
      '--- Rinomina CORPO in TEMPTCO
      '--------------------------------------------------------------------------------------------------------------
      dsDqta.Tables(0).TableName = "TEMPTCO"
      '--------------------------------------------------------------------------------------------------------------
      '--- Pulisce il DataSet
      '--------------------------------------------------------------------------------------------------------------
      dsDqta.Tables("TEMPTCO").Clear()
      dsDqta.Tables("TEMPTCO").Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Carico l'intestazione delle colonne e nascondo quelle che non servono
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ValCodiceDb(nCodtagl.ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        For i = 0 To (grvTco.Columns.Count - 1)
          l = NTSCInt(grvTco.Columns(i).Name.Substring(grvTco.Columns(i).Name.Length - 2))
          If l <> 0 Then
            grvTco.Columns(i).Caption = NTSCStr(dttTmp.Rows(0)("tb_dest" & l.ToString.PadLeft(2, "0"c)))
            If NTSCStr(grvTco.Columns(i).Caption).Trim = "" Then
              grvTco.Columns(i).Visible = False
            Else
              GctlSetVisEnab(grvTco.Columns(i), True)
            End If
          End If
        Next
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      dtrT = dsGsor.Tables("CORPO").Select("xxo_codtagl = " & nCodtagl)
      If dtrT.Length > 0 Then
        For i = 0 To (dtrT.Length - 1)
          dsDqta.Tables("TEMPTCO").ImportRow(dtrT(i))
        Next
      Else
        Return False
      End If
      dsDqta.Tables("TEMPTCO").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If dsDqta.Tables("TEMPTCO").Columns.Contains("ec_caption") = False Then
        With dsDqta.Tables("TEMPTCO").Columns
          .Add("ec_caption", GetType(String))
          .Add("ec_tipo", GetType(String))
          .Add("ec_fase", GetType(Integer))
          .Add("ec_desfase", GetType(String))
          .Add("ec_codart", GetType(String))
          .Add("ec_descr", GetType(String))
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrT1 As DataRow In dsDqta.Tables("TEMPTCO").Rows
        dtrT2 = dsGsor.Tables("CORPO").Select("ec_riga = " & NTSCInt(dtrT1!ec_riga).ToString)
        If dtrT2.Length > 0 Then
          dtrT1!ec_codart = dtrT2(0)!ec_codart
          dtrT1!ec_descr = dtrT2(0)!ec_descr
          dtrT1!ec_fase = dtrT2(0)!ec_fase
          dtrT1!ec_desfase = dtrT2(0)!xxo_fase
        End If
        dtrT2 = dsGsor.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrT1!ec_riga).ToString)
        If dtrT2.Length > 0 Then
          For i = 1 To 24
            dtrT1("ec_quant" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)) = _
            dtrT2(0)("ec_quant" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))
          Next
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dsDqta.Tables("TEMPTCO").Rows.Count - 1)
        dsDqta.Tables("TEMPTCO").Rows(i)!ec_tipo = "Q" 'quantità
        dsDqta.Tables("TEMPTCO").Rows(i)!ec_caption = "Qta per taglia"
      Next
      '--------------------------------------------------------------------------------------------------------------
      dsDqta.Tables("TEMPTCO").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      dcDqta.DataSource = dsDqta.Tables("TEMPTCO")
      dcDqta.Sort = "ec_riga ASC"
      grTco.DataSource = dcDqta
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ApriTestata() As Boolean
    Dim strTmp As String = ""
    Dim oDttgr As New CLEGROUPBY

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTest.Clear()
      dttTest.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Raggruppo il DataSet del dettaglio Taglie & Colori, per Scala Taglie
      '--------------------------------------------------------------------------------------------------------------
      oDttgr.NTSGroupBy(dsGsor.Tables("CORPO"), dttTest, "xxo_codtagl", "", "xxo_codtagl")
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo, al DataTable raggruppato, un campo per la Descrizione
      '--------------------------------------------------------------------------------------------------------------
      dttTest.Columns.Add("xxo_destagl", GetType(String))
      '--------------------------------------------------------------------------------------------------------------
      '--- Ciclo sul DataTable per decodificare la descrizione
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTest.Rows.Count - 1)
        If oMenu.ValCodiceDb(NTSCStr(dttTest.Rows(i)!xxo_codtagl), DittaCorrente, "TABTAGL", "N", strTmp) = True Then
          dttTest.Rows(i)!xxo_destagl = strTmp
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiorno il DataTable
      '--------------------------------------------------------------------------------------------------------------
      dttTest.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Collego il DataTable alla griglia dei testata
      '--------------------------------------------------------------------------------------------------------------
      dcTest.DataSource = dttTest
      grTest.DataSource = dcTest
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class