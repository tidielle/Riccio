Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORDQTA
  Public oCleGsor As CLEORGSOR
  Public oCallParams As CLE__CLDP
  Public dsDqta As New DataSet
  Public dcDqta As BindingSource = New BindingSource()
  Public dtrEc As DataRow = Nothing         'riga del corpo a cui è associata corpotc
  Public dsGsor As DataSet                  'dataset di BNORGSOR
  Public bVisDoc As Boolean = False
  Public nRighe As Integer = 3

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORDQTA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbDispVariante = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTco = New NTSInformatica.NTSGrid
    Me.grvTco = New NTSInformatica.NTSGridView
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.ec_desfase = New NTSInformatica.NTSGridColumn
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
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
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbEsci, Me.tlbDispVariante})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDispVariante, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbDispVariante
    '
    Me.tlbDispVariante.Caption = "Visualizza disponibilità variante/magazzino"
    Me.tlbDispVariante.Glyph = CType(resources.GetObject("tlbDispVariante.Glyph"), System.Drawing.Image)
    Me.tlbDispVariante.Id = 13
    Me.tlbDispVariante.Name = "tlbDispVariante"
    Me.tlbDispVariante.Visible = True
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
    Me.grTco.Location = New System.Drawing.Point(0, 30)
    Me.grTco.MainView = Me.grvTco
    Me.grTco.Name = "grTco"
    Me.grTco.Size = New System.Drawing.Size(492, 136)
    Me.grTco.TabIndex = 4
    Me.grTco.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTco})
    '
    'grvTco
    '
    Me.grvTco.ActiveFilterEnabled = False
    '
    'ec_fase
    '
    Me.ec_fase.AppearanceCell.Options.UseBackColor = True
    Me.ec_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_fase.Caption = "Fase"
    Me.ec_fase.Enabled = False
    Me.ec_fase.FieldName = "ec_fase"
    Me.ec_fase.Name = "ec_fase"
    Me.ec_fase.NTSRepositoryComboBox = Nothing
    Me.ec_fase.NTSRepositoryItemCheck = Nothing
    Me.ec_fase.NTSRepositoryItemText = Nothing
    Me.ec_fase.OptionsColumn.AllowEdit = False
    Me.ec_fase.OptionsColumn.ReadOnly = True
    '
    'ec_desfase
    '
    Me.ec_desfase.AppearanceCell.Options.UseBackColor = True
    Me.ec_desfase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_desfase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_desfase.Caption = "Descr. fase"
    Me.ec_desfase.Enabled = False
    Me.ec_desfase.FieldName = "ec_desfase"
    Me.ec_desfase.Name = "ec_desfase"
    Me.ec_desfase.NTSRepositoryComboBox = Nothing
    Me.ec_desfase.NTSRepositoryItemCheck = Nothing
    Me.ec_desfase.NTSRepositoryItemText = Nothing
    Me.ec_desfase.OptionsColumn.AllowEdit = False
    Me.ec_desfase.OptionsColumn.ReadOnly = True
    Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_riga, Me.ec_codart, Me.ec_descr, Me.ec_fase, Me.ec_desfase, Me.ec_caption, Me.ec_quant01, Me.ec_quant02, Me.ec_quant03, Me.ec_quant04, Me.ec_quant05, Me.ec_quant06, Me.ec_quant07, Me.ec_quant08, Me.ec_quant09, Me.ec_quant10, Me.ec_quant11, Me.ec_quant12, Me.ec_quant13, Me.ec_quant14, Me.ec_quant15, Me.ec_quant16, Me.ec_quant17, Me.ec_quant18, Me.ec_quant19, Me.ec_quant20, Me.ec_quant21, Me.ec_quant22, Me.ec_quant23, Me.ec_quant24})
    Me.grvTco.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTco.Enabled = True
    Me.grvTco.GridControl = Me.grTco
    Me.grvTco.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTco.Name = "grvTco"
    Me.grvTco.NTSAllowDelete = True
    Me.grvTco.NTSAllowInsert = True
    Me.grvTco.NTSAllowUpdate = True
    Me.grvTco.NTSMenuContext = Nothing
    Me.grvTco.OptionsCustomization.AllowRowSizing = True
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
    Me.ec_descr.NTSRepositoryItemText = Nothing
    Me.ec_descr.OptionsColumn.AllowEdit = False
    Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_descr.OptionsColumn.ReadOnly = True
    Me.ec_descr.OptionsFilter.AllowFilter = False
    Me.ec_descr.Visible = True
    Me.ec_descr.VisibleIndex = 2
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
    Me.ec_caption.NTSRepositoryItemText = Nothing
    Me.ec_caption.OptionsColumn.AllowEdit = False
    Me.ec_caption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_caption.OptionsColumn.ReadOnly = True
    Me.ec_caption.OptionsFilter.AllowFilter = False
    Me.ec_caption.Visible = True
    Me.ec_caption.VisibleIndex = 3
    '
    'ec_quant01
    '
    Me.ec_quant01.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant01.Caption = "QTA1"
    Me.ec_quant01.Enabled = True
    Me.ec_quant01.FieldName = "ec_quant01"
    Me.ec_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant01.Name = "ec_quant01"
    Me.ec_quant01.NTSRepositoryComboBox = Nothing
    Me.ec_quant01.NTSRepositoryItemCheck = Nothing
    Me.ec_quant01.NTSRepositoryItemText = Nothing
    Me.ec_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant01.OptionsFilter.AllowFilter = False
    Me.ec_quant01.Visible = True
    Me.ec_quant01.VisibleIndex = 4
    '
    'ec_quant02
    '
    Me.ec_quant02.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant02.Caption = "QTA2"
    Me.ec_quant02.Enabled = True
    Me.ec_quant02.FieldName = "ec_quant02"
    Me.ec_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant02.Name = "ec_quant02"
    Me.ec_quant02.NTSRepositoryComboBox = Nothing
    Me.ec_quant02.NTSRepositoryItemCheck = Nothing
    Me.ec_quant02.NTSRepositoryItemText = Nothing
    Me.ec_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant02.OptionsFilter.AllowFilter = False
    Me.ec_quant02.Visible = True
    Me.ec_quant02.VisibleIndex = 5
    '
    'ec_quant03
    '
    Me.ec_quant03.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant03.Caption = "QTA3"
    Me.ec_quant03.Enabled = True
    Me.ec_quant03.FieldName = "ec_quant03"
    Me.ec_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant03.Name = "ec_quant03"
    Me.ec_quant03.NTSRepositoryComboBox = Nothing
    Me.ec_quant03.NTSRepositoryItemCheck = Nothing
    Me.ec_quant03.NTSRepositoryItemText = Nothing
    Me.ec_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant03.OptionsFilter.AllowFilter = False
    Me.ec_quant03.Visible = True
    Me.ec_quant03.VisibleIndex = 6
    '
    'ec_quant04
    '
    Me.ec_quant04.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant04.Caption = "QTA4"
    Me.ec_quant04.Enabled = True
    Me.ec_quant04.FieldName = "ec_quant04"
    Me.ec_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant04.Name = "ec_quant04"
    Me.ec_quant04.NTSRepositoryComboBox = Nothing
    Me.ec_quant04.NTSRepositoryItemCheck = Nothing
    Me.ec_quant04.NTSRepositoryItemText = Nothing
    Me.ec_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant04.OptionsFilter.AllowFilter = False
    Me.ec_quant04.Visible = True
    Me.ec_quant04.VisibleIndex = 7
    '
    'ec_quant05
    '
    Me.ec_quant05.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant05.Caption = "QTA5"
    Me.ec_quant05.Enabled = True
    Me.ec_quant05.FieldName = "ec_quant05"
    Me.ec_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant05.Name = "ec_quant05"
    Me.ec_quant05.NTSRepositoryComboBox = Nothing
    Me.ec_quant05.NTSRepositoryItemCheck = Nothing
    Me.ec_quant05.NTSRepositoryItemText = Nothing
    Me.ec_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant05.OptionsFilter.AllowFilter = False
    Me.ec_quant05.Visible = True
    Me.ec_quant05.VisibleIndex = 8
    '
    'ec_quant06
    '
    Me.ec_quant06.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant06.Caption = "QTA6"
    Me.ec_quant06.Enabled = True
    Me.ec_quant06.FieldName = "ec_quant06"
    Me.ec_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant06.Name = "ec_quant06"
    Me.ec_quant06.NTSRepositoryComboBox = Nothing
    Me.ec_quant06.NTSRepositoryItemCheck = Nothing
    Me.ec_quant06.NTSRepositoryItemText = Nothing
    Me.ec_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant06.OptionsFilter.AllowFilter = False
    Me.ec_quant06.Visible = True
    Me.ec_quant06.VisibleIndex = 9
    '
    'ec_quant07
    '
    Me.ec_quant07.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant07.Caption = "QTA7"
    Me.ec_quant07.Enabled = True
    Me.ec_quant07.FieldName = "ec_quant07"
    Me.ec_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant07.Name = "ec_quant07"
    Me.ec_quant07.NTSRepositoryComboBox = Nothing
    Me.ec_quant07.NTSRepositoryItemCheck = Nothing
    Me.ec_quant07.NTSRepositoryItemText = Nothing
    Me.ec_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant07.OptionsFilter.AllowFilter = False
    Me.ec_quant07.Visible = True
    Me.ec_quant07.VisibleIndex = 10
    '
    'ec_quant08
    '
    Me.ec_quant08.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant08.Caption = "QTA8"
    Me.ec_quant08.Enabled = True
    Me.ec_quant08.FieldName = "ec_quant08"
    Me.ec_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant08.Name = "ec_quant08"
    Me.ec_quant08.NTSRepositoryComboBox = Nothing
    Me.ec_quant08.NTSRepositoryItemCheck = Nothing
    Me.ec_quant08.NTSRepositoryItemText = Nothing
    Me.ec_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant08.OptionsFilter.AllowFilter = False
    Me.ec_quant08.Visible = True
    Me.ec_quant08.VisibleIndex = 11
    '
    'ec_quant09
    '
    Me.ec_quant09.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant09.Caption = "QTA9"
    Me.ec_quant09.Enabled = True
    Me.ec_quant09.FieldName = "ec_quant09"
    Me.ec_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant09.Name = "ec_quant09"
    Me.ec_quant09.NTSRepositoryComboBox = Nothing
    Me.ec_quant09.NTSRepositoryItemCheck = Nothing
    Me.ec_quant09.NTSRepositoryItemText = Nothing
    Me.ec_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant09.OptionsFilter.AllowFilter = False
    Me.ec_quant09.Visible = True
    Me.ec_quant09.VisibleIndex = 12
    '
    'ec_quant10
    '
    Me.ec_quant10.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant10.Caption = "QTA10"
    Me.ec_quant10.Enabled = True
    Me.ec_quant10.FieldName = "ec_quant10"
    Me.ec_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant10.Name = "ec_quant10"
    Me.ec_quant10.NTSRepositoryComboBox = Nothing
    Me.ec_quant10.NTSRepositoryItemCheck = Nothing
    Me.ec_quant10.NTSRepositoryItemText = Nothing
    Me.ec_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant10.OptionsFilter.AllowFilter = False
    Me.ec_quant10.Visible = True
    Me.ec_quant10.VisibleIndex = 13
    '
    'ec_quant11
    '
    Me.ec_quant11.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant11.Caption = "QTA11"
    Me.ec_quant11.Enabled = True
    Me.ec_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant11.Name = "ec_quant11"
    Me.ec_quant11.NTSRepositoryComboBox = Nothing
    Me.ec_quant11.NTSRepositoryItemCheck = Nothing
    Me.ec_quant11.NTSRepositoryItemText = Nothing
    Me.ec_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant11.OptionsFilter.AllowFilter = False
    Me.ec_quant11.Visible = True
    Me.ec_quant11.VisibleIndex = 14
    '
    'ec_quant12
    '
    Me.ec_quant12.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant12.Caption = "QTA12"
    Me.ec_quant12.Enabled = True
    Me.ec_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant12.Name = "ec_quant12"
    Me.ec_quant12.NTSRepositoryComboBox = Nothing
    Me.ec_quant12.NTSRepositoryItemCheck = Nothing
    Me.ec_quant12.NTSRepositoryItemText = Nothing
    Me.ec_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant12.OptionsFilter.AllowFilter = False
    Me.ec_quant12.Visible = True
    Me.ec_quant12.VisibleIndex = 15
    '
    'ec_quant13
    '
    Me.ec_quant13.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant13.Caption = "QTA13"
    Me.ec_quant13.Enabled = True
    Me.ec_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant13.Name = "ec_quant13"
    Me.ec_quant13.NTSRepositoryComboBox = Nothing
    Me.ec_quant13.NTSRepositoryItemCheck = Nothing
    Me.ec_quant13.NTSRepositoryItemText = Nothing
    Me.ec_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant13.OptionsFilter.AllowFilter = False
    Me.ec_quant13.Visible = True
    Me.ec_quant13.VisibleIndex = 16
    '
    'ec_quant14
    '
    Me.ec_quant14.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant14.Caption = "QTA14"
    Me.ec_quant14.Enabled = True
    Me.ec_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant14.Name = "ec_quant14"
    Me.ec_quant14.NTSRepositoryComboBox = Nothing
    Me.ec_quant14.NTSRepositoryItemCheck = Nothing
    Me.ec_quant14.NTSRepositoryItemText = Nothing
    Me.ec_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant14.OptionsFilter.AllowFilter = False
    Me.ec_quant14.Visible = True
    Me.ec_quant14.VisibleIndex = 17
    '
    'ec_quant15
    '
    Me.ec_quant15.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant15.Caption = "QTA15"
    Me.ec_quant15.Enabled = True
    Me.ec_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant15.Name = "ec_quant15"
    Me.ec_quant15.NTSRepositoryComboBox = Nothing
    Me.ec_quant15.NTSRepositoryItemCheck = Nothing
    Me.ec_quant15.NTSRepositoryItemText = Nothing
    Me.ec_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant15.OptionsFilter.AllowFilter = False
    Me.ec_quant15.Visible = True
    Me.ec_quant15.VisibleIndex = 18
    '
    'ec_quant16
    '
    Me.ec_quant16.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant16.Caption = "QTA16"
    Me.ec_quant16.Enabled = True
    Me.ec_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant16.Name = "ec_quant16"
    Me.ec_quant16.NTSRepositoryComboBox = Nothing
    Me.ec_quant16.NTSRepositoryItemCheck = Nothing
    Me.ec_quant16.NTSRepositoryItemText = Nothing
    Me.ec_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant16.OptionsFilter.AllowFilter = False
    Me.ec_quant16.Visible = True
    Me.ec_quant16.VisibleIndex = 19
    '
    'ec_quant17
    '
    Me.ec_quant17.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant17.Caption = "QTA17"
    Me.ec_quant17.Enabled = True
    Me.ec_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant17.Name = "ec_quant17"
    Me.ec_quant17.NTSRepositoryComboBox = Nothing
    Me.ec_quant17.NTSRepositoryItemCheck = Nothing
    Me.ec_quant17.NTSRepositoryItemText = Nothing
    Me.ec_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant17.OptionsFilter.AllowFilter = False
    Me.ec_quant17.Visible = True
    Me.ec_quant17.VisibleIndex = 20
    '
    'ec_quant18
    '
    Me.ec_quant18.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant18.Caption = "QTA18"
    Me.ec_quant18.Enabled = True
    Me.ec_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant18.Name = "ec_quant18"
    Me.ec_quant18.NTSRepositoryComboBox = Nothing
    Me.ec_quant18.NTSRepositoryItemCheck = Nothing
    Me.ec_quant18.NTSRepositoryItemText = Nothing
    Me.ec_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant18.OptionsFilter.AllowFilter = False
    Me.ec_quant18.Visible = True
    Me.ec_quant18.VisibleIndex = 21
    '
    'ec_quant19
    '
    Me.ec_quant19.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant19.Caption = "QTA19"
    Me.ec_quant19.Enabled = True
    Me.ec_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant19.Name = "ec_quant19"
    Me.ec_quant19.NTSRepositoryComboBox = Nothing
    Me.ec_quant19.NTSRepositoryItemCheck = Nothing
    Me.ec_quant19.NTSRepositoryItemText = Nothing
    Me.ec_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant19.OptionsFilter.AllowFilter = False
    Me.ec_quant19.Visible = True
    Me.ec_quant19.VisibleIndex = 22
    '
    'ec_quant20
    '
    Me.ec_quant20.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant20.Caption = "QTA20"
    Me.ec_quant20.Enabled = True
    Me.ec_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant20.Name = "ec_quant20"
    Me.ec_quant20.NTSRepositoryComboBox = Nothing
    Me.ec_quant20.NTSRepositoryItemCheck = Nothing
    Me.ec_quant20.NTSRepositoryItemText = Nothing
    Me.ec_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant20.OptionsFilter.AllowFilter = False
    Me.ec_quant20.Visible = True
    Me.ec_quant20.VisibleIndex = 23
    '
    'ec_quant21
    '
    Me.ec_quant21.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant21.Caption = "QTA21"
    Me.ec_quant21.Enabled = True
    Me.ec_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant21.Name = "ec_quant21"
    Me.ec_quant21.NTSRepositoryComboBox = Nothing
    Me.ec_quant21.NTSRepositoryItemCheck = Nothing
    Me.ec_quant21.NTSRepositoryItemText = Nothing
    Me.ec_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant21.OptionsFilter.AllowFilter = False
    Me.ec_quant21.Visible = True
    Me.ec_quant21.VisibleIndex = 24
    '
    'ec_quant22
    '
    Me.ec_quant22.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant22.Caption = "QTA22"
    Me.ec_quant22.Enabled = True
    Me.ec_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant22.Name = "ec_quant22"
    Me.ec_quant22.NTSRepositoryComboBox = Nothing
    Me.ec_quant22.NTSRepositoryItemCheck = Nothing
    Me.ec_quant22.NTSRepositoryItemText = Nothing
    Me.ec_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant22.OptionsFilter.AllowFilter = False
    Me.ec_quant22.Visible = True
    Me.ec_quant22.VisibleIndex = 25
    '
    'ec_quant23
    '
    Me.ec_quant23.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant23.Caption = "QTA23"
    Me.ec_quant23.Enabled = True
    Me.ec_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant23.Name = "ec_quant23"
    Me.ec_quant23.NTSRepositoryComboBox = Nothing
    Me.ec_quant23.NTSRepositoryItemCheck = Nothing
    Me.ec_quant23.NTSRepositoryItemText = Nothing
    Me.ec_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant23.OptionsFilter.AllowFilter = False
    Me.ec_quant23.Visible = True
    Me.ec_quant23.VisibleIndex = 26
    '
    'ec_quant24
    '
    Me.ec_quant24.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant24.Caption = "QTA24"
    Me.ec_quant24.Enabled = True
    Me.ec_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant24.Name = "ec_quant24"
    Me.ec_quant24.NTSRepositoryComboBox = Nothing
    Me.ec_quant24.NTSRepositoryItemCheck = Nothing
    Me.ec_quant24.NTSRepositoryItemText = Nothing
    Me.ec_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant24.OptionsFilter.AllowFilter = False
    Me.ec_quant24.Visible = True
    Me.ec_quant24.VisibleIndex = 27
    '
    'FRMORDQTA
    '
    Me.ClientSize = New System.Drawing.Size(492, 166)
    Me.Controls.Add(Me.grTco)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(1500, 200)
    Me.MinimizeBox = False
    Me.Name = "FRMORDQTA"
    Me.Text = "DETTAGLIO QUANTITÀ - TAGLIE E COLORI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR, ByRef dtrCorpo As DataRow)
    oCleGsor = cleGsor
    dtrEc = dtrCorpo
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbDispVariante.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 129048470351076307, "Griglia dettaglio taglie"))
      ec_riga.NTSSetParamNUM(oMenu, "Riga", "0", 6)
      ec_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457546406250, "Codice articolo"), 0, True)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457556406250, "Descrizione"), 0, True)
      ec_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128685783617187500, "Fase"), "0", 3)
      ec_desfase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128685783591406250, "Descrizione fase"), 0, True)
      ec_caption.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128556540578040000, "Tipo riga"), 0, True)
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
      grvTco.NTSAllowUpdate = True
      grvTco.NTSAllowInsert = False

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
  Public Overridable Sub FRMORDQTA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not Apri() Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'collego i dati alla griglia
      dsDqta.Tables("TEMPTCO").AcceptChanges()
      dcDqta.DataSource = dsDqta.Tables("TEMPTCO")
      dcDqta.Sort = "ec_riga ASC"
      grTco.DataSource = dcDqta

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-------------------------
      'carico l'intestazione delle colonne e nascondo quelle che non servono
      'solo se non sono in modalità 'visualizzazione intero documento'
      If Not dtrEc Is Nothing Then
        oMenu.ValCodiceDb(NTSCInt(dtrEc!xxo_codtagl).ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          For i = 0 To grvTco.Columns.Count - 1
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
      End If

      '-------------------------
      'se chiamato da menu file, visualizza dettaglio taglie e colori, non posso modificare nulla
      If bVisDoc Then
        Me.Text = oApp.Tr(Me, 128571442009531250, "Dettaglio taglie")
        ec_codart.Visible = True
        ec_descr.Visible = True
        tlbSalva.Visible = False
        tlbRipristina.Visible = False
        tlbDispVariante.Visible = False
        grvTco.Enabled = False
        Me.MaximumSize = New System.Drawing.Size(1500, 1500)
        Me.Height = 400
      Else
        Me.Text = oApp.Tr(Me, 128556622491346000, "Dettaglio taglie articolo: ") & NTSCStr(dtrEc!ec_codart) & " - " & NTSCStr(dtrEc!ec_descr)
        If NTSCInt(dtrEc!ec_fase) <> 0 Then Me.Text += oApp.Tr(Me, 128685778291093750, " - Fase ") & dtrEc!ec_fase.ToString & " - " & dtrEc!xxo_fase.ToString
        ec_riga.Visible = False
        ec_codart.Visible = False
        ec_descr.Visible = False
        ec_fase.Visible = False
        ec_desfase.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORDQTA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If bVisDoc = False Then
      If Not Salva() Then e.Cancel = True
    End If
  End Sub

  Public Overridable Sub FRMORDQTA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDqta.Dispose()
      dsDqta.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      dsDqta.Tables("TEMPTCO").RejectChanges()
      grvTco.NTSMoveFirstColunn()
      grvTco.RefreshData()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDispVariante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDispVariante.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If NTSCInt(dtrEc!ec_magaz) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048470477954555, "Magazzino sulla riga del documento non impostato"))
        Return
      End If

      'la chiamata è identica a quella di frmGsor.tlbVisDispVariante_ItemClick
      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BS--HLAP"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(dtrEc!ec_codart) & "|" & _
               NTSCStr(dtrEc!ec_codart) & "|" & _
               NTSCInt(dtrEc!ec_magaz).ToString & "|" & _
               NTSCInt(dtrEc!ec_magaz).ToString & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCInt(dtrEc!ec_fase).ToString & "|" & _
               NTSCInt(dtrEc!ec_fase).ToString & "|" & _
               "ARTPROTC" & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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

#Region "Eventi di griglia"
  Public Overridable Sub grvTco_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTco.NTSBeforeRowUpdate
    Try
      If bVisDoc = False Then
        If grvTco.NTSGetCurrentDataRow!ec_tipo.ToString = "Q" Then
          If Not Salva() Then
            'rimango sulla riga su cui sono
            e.Allow = False
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvTco_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTco.NTSFocusedRowChanged
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      '-------------------------
      'posso modificare solo la riga delle quantità
      If oCleGsor Is Nothing Then Return
      If grvTco.NTSGetCurrentDataRow!ec_tipo.ToString <> "Q" Then
        grvTco.Enabled = False
      Else
        If bVisDoc = False Then GctlSetVisEnab(grvTco, False) 'posso modificare lequantità solo se non sono in 'visualizzazione documento intero'
      End If

      If bVisDoc = False Then CalcolaResiduo()

      '-------------------------
      'carico l'intestazione delle colonne
      If bVisDoc Then
        dtrT = dsGsor.Tables("CORPO").Select("ec_riga = " & NTSCInt(grvTco.NTSGetCurrentDataRow!ec_riga).ToString)
        If dtrT.Length > 0 Then
          oMenu.ValCodiceDb(NTSCInt(dtrT(0)!xxo_codtagl).ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            For i = 0 To grvTco.Columns.Count - 1
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
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvTco_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvTco.NTSFocusedColumnChanged
    Try
      If bVisDoc = False Then CalcolaResiduo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT2() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      '-------------------------------------------------
      'collego i dati: prima creo la tabella temporanea, poi la popolo
      dsDqta.Tables.Add(dsGsor.Tables("CORPOTC").Clone())
      dsDqta.Tables(0).TableName = "TEMPTCO"

      dsDqta.Tables("TEMPTCO").Clear()

      If Not dtrEc Is Nothing Then
        'chiamato per inserire/modificare le quantità
        dtrT = dsGsor.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrEc!ec_riga).ToString)
      Else
        'chiamato per visualizzare l'interno documento 
        dtrT = dsGsor.Tables("CORPOTC").Select()
      End If
      If dtrT.Length > 0 Then
        For i = 0 To dtrT.Length - 1
          dsDqta.Tables("TEMPTCO").ImportRow(dtrT(i))
        Next
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128556573776346000, "Nella griglia del dettaglio TCO non è stata trovata la riga collegata alla rig adel documento |" & NTSCInt(dtrEc!ec_riga).ToString & "|"))
        Return False
      End If
      dsDqta.Tables("TEMPTCO").AcceptChanges()

      '-------------------------------------------------
      'aggiungo la colonna dell'intestazione
      dsDqta.Tables("TEMPTCO").Columns.Add("ec_caption", GetType(String))
      dsDqta.Tables("TEMPTCO").Columns.Add("ec_tipo", GetType(String))

      '-------------------------------------------------
      'se serve aggiungo le colonne codice e descr. articolo e le riempio
      If bVisDoc Then
        dsDqta.Tables("TEMPTCO").Columns.Add("ec_codart", GetType(String))
        dsDqta.Tables("TEMPTCO").Columns.Add("ec_descr", GetType(String))
        dsDqta.Tables("TEMPTCO").Columns.Add("ec_fase", GetType(Integer))
        dsDqta.Tables("TEMPTCO").Columns.Add("ec_desfase", GetType(String))
        For Each dtrT1 As DataRow In dsDqta.Tables("TEMPTCO").Rows
          dtrT2 = dsGsor.Tables("CORPO").Select("ec_riga = " & NTSCInt(dtrT1!ec_riga).ToString)
          If dtrT2.Length > 0 Then
            dtrT1!ec_codart = dtrT2(0)!ec_codart
            dtrT1!ec_descr = dtrT2(0)!ec_descr
            dtrT1!ec_fase = dtrT2(0)!ec_fase
            dtrT1!ec_desfase = dtrT2(0)!xxo_fase
          End If
        Next
      End If

      For l = 0 To dsDqta.Tables("TEMPTCO").Rows.Count - 1
        dsDqta.Tables("TEMPTCO").Rows(l)!ec_tipo = "Q" 'quantità
        dsDqta.Tables("TEMPTCO").Rows(l)!ec_caption = "Qta per taglia"

        '-------------------------------------------------
        'aggiungo le righe 'disimpegnate', 'spedite', 'prenotate', 'residue'
        If dsGsor.Tables("CORPOTC").Columns.Contains("ec_qtadis01") Then
          dsDqta.Tables("TEMPTCO").Rows.Add(dsDqta.Tables("TEMPTCO").NewRow())
          With dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)
            !ec_tipo = "D" 'disimpegnata
            !ec_caption = "Qta disimpegno (aperto)"
            !ec_riga = dsDqta.Tables("TEMPTCO").Rows(l)!ec_riga
            For i = 1 To 24
              dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsDqta.Tables("TEMPTCO").Rows(l)("ec_qtadis" & i.ToString.PadLeft(2, "0"c)))
            Next
          End With
          nRighe = 4
        End If

        dsDqta.Tables("TEMPTCO").Rows.Add(dsDqta.Tables("TEMPTCO").NewRow())
        With dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)
          !ec_tipo = "S" 'spedite
          !ec_caption = "Qta spedita"
          !ec_riga = dsDqta.Tables("TEMPTCO").Rows(l)!ec_riga
          For i = 1 To 24
            dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsDqta.Tables("TEMPTCO").Rows(l)("ec_quaeva" & i.ToString.PadLeft(2, "0"c)))
          Next
        End With

        If bVisDoc = False Then
          If NTSCStr(dsDqta.Tables("TEMPTCO").Rows(0)!ec_tipork) <> "V" Then
            dsDqta.Tables("TEMPTCO").Rows.Add(dsDqta.Tables("TEMPTCO").NewRow())
            With dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)
              !ec_tipo = "P" 'prenotate
              !ec_caption = "Qta prenotata"
              !ec_riga = dsDqta.Tables("TEMPTCO").Rows(l)!ec_riga
              For i = 1 To 24
                dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsDqta.Tables("TEMPTCO").Rows(l)("ec_quapre" & i.ToString.PadLeft(2, "0"c)))
              Next
            End With
          End If
        End If

        dsDqta.Tables("TEMPTCO").Rows.Add(dsDqta.Tables("TEMPTCO").NewRow())
        With dsDqta.Tables("TEMPTCO").Rows(dsDqta.Tables("TEMPTCO").Rows.Count - 1)
          !ec_tipo = "R" 'residua
          !ec_caption = "Qta residua"
          !ec_riga = dsDqta.Tables("TEMPTCO").Rows(l)!ec_riga
        End With

      Next

      dsDqta.Tables("TEMPTCO").AcceptChanges()

      'se sono in visualizzazione intero documento calcolo il residuo una volta sola
      If bVisDoc Then CalcolaResiduo()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------
      'nelle righe di ordini di produzione una volta inserite righe negli impegni collegati 
      'non è più possibile cambiare le quantità
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        If oCleGsor.dttECIMP.Select("ec_rigaor = " & NTSCInt(dtrEc!ec_riga).ToString).Length > 0 Or _
           oCleGsor.dttATTIVIT.Select("at_riga = " & NTSCInt(dtrEc!ec_riga).ToString).Length > 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128587650648906250, "Una volta inserite righe negli impegni di produzione e/o nelle lavorazioni collegate non è più possibile cambiare le quantità dell'articolo da produrre"))
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
          Return True
        End If
      End If

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      '----------------------------
      'trovo la riga di CORPOTC di gsor su cui sto lavorando
      dtrT = dsGsor.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrEc!ec_riga).ToString)

      '----------------------------
      'riverso i dati in CORPOTC e confermo tutto
      dsDqta.Tables("TEMPTCO").Rows(0).AcceptChanges()
      For i = 1 To 24
        dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsDqta.Tables("TEMPTCO").Rows(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
      Next
      'dsGsor.Tables("CORPOTC").AcceptChanges() 'non farlo: diversamente se faccio 'ripristina riga da gsor non posso ripristinare il dettaglio TCO

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Function CalcolaResiduo() As Boolean
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrQ() As DataRow = Nothing
    Dim dtrS() As DataRow = Nothing
    Dim dtrR() As DataRow = Nothing
    Try
      If oCleGsor Is Nothing Then Return False
      If Not dsDqta.Tables.Contains("TEMPTCO") Then Return False
      If dsDqta.Tables("TEMPTCO").Rows.Count < nRighe Then Return False

      '-----------------------
      'calcolo la colonna 'residuo'
      dtrQ = dsDqta.Tables("TEMPTCO").Select("ec_tipo = 'Q'")
      For l = 0 To dtrQ.Length - 1
        dtrS = dsDqta.Tables("TEMPTCO").Select("ec_tipo = 'S' AND ec_riga = " & NTSCInt(dtrQ(l)!ec_riga).ToString)
        dtrR = dsDqta.Tables("TEMPTCO").Select("ec_tipo = 'R' AND ec_riga = " & NTSCInt(dtrQ(l)!ec_riga).ToString)
        For i = 1 To 24
          'residuo (3) = quant (0) - spedite (1) 
          dtrR(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = _
              NTSCDec(dtrQ(l)("ec_quant" & i.ToString.PadLeft(2, "0"c))) - _
              NTSCDec(dtrS(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
        dtrR(0).AcceptChanges()
      Next

      '-----------------------
      'calcolo la colonna 'residuo'
      'For i = 1 To 24
      '  'residuo (3) = quant (0) - spedite (1) 
      '  dsDqta.Tables("TEMPTCO").Rows(3)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = _
      '      NTSCDec(dsDqta.Tables("TEMPTCO").Rows(0)("ec_quant" & i.ToString.PadLeft(2, "0"c))) - _
      '      NTSCDec(dsDqta.Tables("TEMPTCO").Rows(1)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
      'Next
      If dsDqta.Tables("TEMPTCO").Rows.Count > nRighe Then dsDqta.Tables("TEMPTCO").Rows(3).AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


End Class
