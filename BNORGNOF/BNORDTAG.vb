Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORDTAG
  Public oCleGnof As CLEORGNOF
  Public oCallParams As CLE__CLDP
  Public dsDtag As New DataSet
  Public dcDtag As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORDTAG))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTco = New NTSInformatica.NTSGrid
    Me.grvTco = New NTSInformatica.NTSGridView
    Me.mm_riga = New NTSInformatica.NTSGridColumn
    Me.mm_codart = New NTSInformatica.NTSGridColumn
    Me.mm_descr = New NTSInformatica.NTSGridColumn
    Me.mm_fase = New NTSInformatica.NTSGridColumn
    Me.mm_desfase = New NTSInformatica.NTSGridColumn
    Me.mm_caption = New NTSInformatica.NTSGridColumn
    Me.mm_quant01 = New NTSInformatica.NTSGridColumn
    Me.mm_quant02 = New NTSInformatica.NTSGridColumn
    Me.mm_quant03 = New NTSInformatica.NTSGridColumn
    Me.mm_quant04 = New NTSInformatica.NTSGridColumn
    Me.mm_quant05 = New NTSInformatica.NTSGridColumn
    Me.mm_quant06 = New NTSInformatica.NTSGridColumn
    Me.mm_quant07 = New NTSInformatica.NTSGridColumn
    Me.mm_quant08 = New NTSInformatica.NTSGridColumn
    Me.mm_quant09 = New NTSInformatica.NTSGridColumn
    Me.mm_quant10 = New NTSInformatica.NTSGridColumn
    Me.mm_quant11 = New NTSInformatica.NTSGridColumn
    Me.mm_quant12 = New NTSInformatica.NTSGridColumn
    Me.mm_quant13 = New NTSInformatica.NTSGridColumn
    Me.mm_quant14 = New NTSInformatica.NTSGridColumn
    Me.mm_quant15 = New NTSInformatica.NTSGridColumn
    Me.mm_quant16 = New NTSInformatica.NTSGridColumn
    Me.mm_quant17 = New NTSInformatica.NTSGridColumn
    Me.mm_quant18 = New NTSInformatica.NTSGridColumn
    Me.mm_quant19 = New NTSInformatica.NTSGridColumn
    Me.mm_quant20 = New NTSInformatica.NTSGridColumn
    Me.mm_quant21 = New NTSInformatica.NTSGridColumn
    Me.mm_quant22 = New NTSInformatica.NTSGridColumn
    Me.mm_quant23 = New NTSInformatica.NTSGridColumn
    Me.mm_quant24 = New NTSInformatica.NTSGridColumn
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mm_riga, Me.mm_codart, Me.mm_descr, Me.mm_fase, Me.mm_desfase, Me.mm_caption, Me.mm_quant01, Me.mm_quant02, Me.mm_quant03, Me.mm_quant04, Me.mm_quant05, Me.mm_quant06, Me.mm_quant07, Me.mm_quant08, Me.mm_quant09, Me.mm_quant10, Me.mm_quant11, Me.mm_quant12, Me.mm_quant13, Me.mm_quant14, Me.mm_quant15, Me.mm_quant16, Me.mm_quant17, Me.mm_quant18, Me.mm_quant19, Me.mm_quant20, Me.mm_quant21, Me.mm_quant22, Me.mm_quant23, Me.mm_quant24})
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
    'mm_riga
    '
    Me.mm_riga.AppearanceCell.Options.UseBackColor = True
    Me.mm_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mm_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_riga.Caption = "Riga"
    Me.mm_riga.Enabled = False
    Me.mm_riga.FieldName = "mm_riga"
    Me.mm_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_riga.Name = "mm_riga"
    Me.mm_riga.NTSRepositoryComboBox = Nothing
    Me.mm_riga.NTSRepositoryItemCheck = Nothing
    Me.mm_riga.NTSRepositoryItemMemo = Nothing
    Me.mm_riga.NTSRepositoryItemText = Nothing
    Me.mm_riga.OptionsColumn.AllowEdit = False
    Me.mm_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_riga.OptionsColumn.ReadOnly = True
    Me.mm_riga.OptionsFilter.AllowFilter = False
    Me.mm_riga.Visible = True
    Me.mm_riga.VisibleIndex = 0
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
    Me.mm_codart.VisibleIndex = 1
    '
    'mm_descr
    '
    Me.mm_descr.AppearanceCell.Options.UseBackColor = True
    Me.mm_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mm_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_descr.Caption = "Descr. articolo"
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
    Me.mm_descr.VisibleIndex = 2
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
    '
    'mm_caption
    '
    Me.mm_caption.AppearanceCell.Options.UseBackColor = True
    Me.mm_caption.AppearanceCell.Options.UseTextOptions = True
    Me.mm_caption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_caption.Caption = "    "
    Me.mm_caption.Enabled = False
    Me.mm_caption.FieldName = "mm_caption"
    Me.mm_caption.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_caption.Name = "mm_caption"
    Me.mm_caption.NTSRepositoryComboBox = Nothing
    Me.mm_caption.NTSRepositoryItemCheck = Nothing
    Me.mm_caption.NTSRepositoryItemMemo = Nothing
    Me.mm_caption.NTSRepositoryItemText = Nothing
    Me.mm_caption.OptionsColumn.AllowEdit = False
    Me.mm_caption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_caption.OptionsColumn.ReadOnly = True
    Me.mm_caption.OptionsFilter.AllowFilter = False
    Me.mm_caption.Visible = True
    Me.mm_caption.VisibleIndex = 3
    '
    'mm_quant01
    '
    Me.mm_quant01.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant01.Caption = "QTA1"
    Me.mm_quant01.Enabled = True
    Me.mm_quant01.FieldName = "mm_quant01"
    Me.mm_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant01.Name = "mm_quant01"
    Me.mm_quant01.NTSRepositoryComboBox = Nothing
    Me.mm_quant01.NTSRepositoryItemCheck = Nothing
    Me.mm_quant01.NTSRepositoryItemMemo = Nothing
    Me.mm_quant01.NTSRepositoryItemText = Nothing
    Me.mm_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant01.OptionsFilter.AllowFilter = False
    Me.mm_quant01.Visible = True
    Me.mm_quant01.VisibleIndex = 4
    '
    'mm_quant02
    '
    Me.mm_quant02.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant02.Caption = "QTA2"
    Me.mm_quant02.Enabled = True
    Me.mm_quant02.FieldName = "mm_quant02"
    Me.mm_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant02.Name = "mm_quant02"
    Me.mm_quant02.NTSRepositoryComboBox = Nothing
    Me.mm_quant02.NTSRepositoryItemCheck = Nothing
    Me.mm_quant02.NTSRepositoryItemMemo = Nothing
    Me.mm_quant02.NTSRepositoryItemText = Nothing
    Me.mm_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant02.OptionsFilter.AllowFilter = False
    Me.mm_quant02.Visible = True
    Me.mm_quant02.VisibleIndex = 5
    '
    'mm_quant03
    '
    Me.mm_quant03.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant03.Caption = "QTA3"
    Me.mm_quant03.Enabled = True
    Me.mm_quant03.FieldName = "mm_quant03"
    Me.mm_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant03.Name = "mm_quant03"
    Me.mm_quant03.NTSRepositoryComboBox = Nothing
    Me.mm_quant03.NTSRepositoryItemCheck = Nothing
    Me.mm_quant03.NTSRepositoryItemMemo = Nothing
    Me.mm_quant03.NTSRepositoryItemText = Nothing
    Me.mm_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant03.OptionsFilter.AllowFilter = False
    Me.mm_quant03.Visible = True
    Me.mm_quant03.VisibleIndex = 6
    '
    'mm_quant04
    '
    Me.mm_quant04.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant04.Caption = "QTA4"
    Me.mm_quant04.Enabled = True
    Me.mm_quant04.FieldName = "mm_quant04"
    Me.mm_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant04.Name = "mm_quant04"
    Me.mm_quant04.NTSRepositoryComboBox = Nothing
    Me.mm_quant04.NTSRepositoryItemCheck = Nothing
    Me.mm_quant04.NTSRepositoryItemMemo = Nothing
    Me.mm_quant04.NTSRepositoryItemText = Nothing
    Me.mm_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant04.OptionsFilter.AllowFilter = False
    Me.mm_quant04.Visible = True
    Me.mm_quant04.VisibleIndex = 7
    '
    'mm_quant05
    '
    Me.mm_quant05.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant05.Caption = "QTA5"
    Me.mm_quant05.Enabled = True
    Me.mm_quant05.FieldName = "mm_quant05"
    Me.mm_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant05.Name = "mm_quant05"
    Me.mm_quant05.NTSRepositoryComboBox = Nothing
    Me.mm_quant05.NTSRepositoryItemCheck = Nothing
    Me.mm_quant05.NTSRepositoryItemMemo = Nothing
    Me.mm_quant05.NTSRepositoryItemText = Nothing
    Me.mm_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant05.OptionsFilter.AllowFilter = False
    Me.mm_quant05.Visible = True
    Me.mm_quant05.VisibleIndex = 8
    '
    'mm_quant06
    '
    Me.mm_quant06.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant06.Caption = "QTA6"
    Me.mm_quant06.Enabled = True
    Me.mm_quant06.FieldName = "mm_quant06"
    Me.mm_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant06.Name = "mm_quant06"
    Me.mm_quant06.NTSRepositoryComboBox = Nothing
    Me.mm_quant06.NTSRepositoryItemCheck = Nothing
    Me.mm_quant06.NTSRepositoryItemMemo = Nothing
    Me.mm_quant06.NTSRepositoryItemText = Nothing
    Me.mm_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant06.OptionsFilter.AllowFilter = False
    Me.mm_quant06.Visible = True
    Me.mm_quant06.VisibleIndex = 9
    '
    'mm_quant07
    '
    Me.mm_quant07.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant07.Caption = "QTA7"
    Me.mm_quant07.Enabled = True
    Me.mm_quant07.FieldName = "mm_quant07"
    Me.mm_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant07.Name = "mm_quant07"
    Me.mm_quant07.NTSRepositoryComboBox = Nothing
    Me.mm_quant07.NTSRepositoryItemCheck = Nothing
    Me.mm_quant07.NTSRepositoryItemMemo = Nothing
    Me.mm_quant07.NTSRepositoryItemText = Nothing
    Me.mm_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant07.OptionsFilter.AllowFilter = False
    Me.mm_quant07.Visible = True
    Me.mm_quant07.VisibleIndex = 10
    '
    'mm_quant08
    '
    Me.mm_quant08.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant08.Caption = "QTA8"
    Me.mm_quant08.Enabled = True
    Me.mm_quant08.FieldName = "mm_quant08"
    Me.mm_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant08.Name = "mm_quant08"
    Me.mm_quant08.NTSRepositoryComboBox = Nothing
    Me.mm_quant08.NTSRepositoryItemCheck = Nothing
    Me.mm_quant08.NTSRepositoryItemMemo = Nothing
    Me.mm_quant08.NTSRepositoryItemText = Nothing
    Me.mm_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant08.OptionsFilter.AllowFilter = False
    Me.mm_quant08.Visible = True
    Me.mm_quant08.VisibleIndex = 11
    '
    'mm_quant09
    '
    Me.mm_quant09.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant09.Caption = "QTA9"
    Me.mm_quant09.Enabled = True
    Me.mm_quant09.FieldName = "mm_quant09"
    Me.mm_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant09.Name = "mm_quant09"
    Me.mm_quant09.NTSRepositoryComboBox = Nothing
    Me.mm_quant09.NTSRepositoryItemCheck = Nothing
    Me.mm_quant09.NTSRepositoryItemMemo = Nothing
    Me.mm_quant09.NTSRepositoryItemText = Nothing
    Me.mm_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant09.OptionsFilter.AllowFilter = False
    Me.mm_quant09.Visible = True
    Me.mm_quant09.VisibleIndex = 12
    '
    'mm_quant10
    '
    Me.mm_quant10.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant10.Caption = "QTA10"
    Me.mm_quant10.Enabled = True
    Me.mm_quant10.FieldName = "mm_quant10"
    Me.mm_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant10.Name = "mm_quant10"
    Me.mm_quant10.NTSRepositoryComboBox = Nothing
    Me.mm_quant10.NTSRepositoryItemCheck = Nothing
    Me.mm_quant10.NTSRepositoryItemMemo = Nothing
    Me.mm_quant10.NTSRepositoryItemText = Nothing
    Me.mm_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant10.OptionsFilter.AllowFilter = False
    Me.mm_quant10.Visible = True
    Me.mm_quant10.VisibleIndex = 13
    '
    'mm_quant11
    '
    Me.mm_quant11.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant11.Caption = "QTA11"
    Me.mm_quant11.Enabled = True
    Me.mm_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant11.Name = "mm_quant11"
    Me.mm_quant11.NTSRepositoryComboBox = Nothing
    Me.mm_quant11.NTSRepositoryItemCheck = Nothing
    Me.mm_quant11.NTSRepositoryItemMemo = Nothing
    Me.mm_quant11.NTSRepositoryItemText = Nothing
    Me.mm_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant11.OptionsFilter.AllowFilter = False
    Me.mm_quant11.Visible = True
    Me.mm_quant11.VisibleIndex = 14
    '
    'mm_quant12
    '
    Me.mm_quant12.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant12.Caption = "QTA12"
    Me.mm_quant12.Enabled = True
    Me.mm_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant12.Name = "mm_quant12"
    Me.mm_quant12.NTSRepositoryComboBox = Nothing
    Me.mm_quant12.NTSRepositoryItemCheck = Nothing
    Me.mm_quant12.NTSRepositoryItemMemo = Nothing
    Me.mm_quant12.NTSRepositoryItemText = Nothing
    Me.mm_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant12.OptionsFilter.AllowFilter = False
    Me.mm_quant12.Visible = True
    Me.mm_quant12.VisibleIndex = 15
    '
    'mm_quant13
    '
    Me.mm_quant13.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant13.Caption = "QTA13"
    Me.mm_quant13.Enabled = True
    Me.mm_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant13.Name = "mm_quant13"
    Me.mm_quant13.NTSRepositoryComboBox = Nothing
    Me.mm_quant13.NTSRepositoryItemCheck = Nothing
    Me.mm_quant13.NTSRepositoryItemMemo = Nothing
    Me.mm_quant13.NTSRepositoryItemText = Nothing
    Me.mm_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant13.OptionsFilter.AllowFilter = False
    Me.mm_quant13.Visible = True
    Me.mm_quant13.VisibleIndex = 16
    '
    'mm_quant14
    '
    Me.mm_quant14.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant14.Caption = "QTA14"
    Me.mm_quant14.Enabled = True
    Me.mm_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant14.Name = "mm_quant14"
    Me.mm_quant14.NTSRepositoryComboBox = Nothing
    Me.mm_quant14.NTSRepositoryItemCheck = Nothing
    Me.mm_quant14.NTSRepositoryItemMemo = Nothing
    Me.mm_quant14.NTSRepositoryItemText = Nothing
    Me.mm_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant14.OptionsFilter.AllowFilter = False
    Me.mm_quant14.Visible = True
    Me.mm_quant14.VisibleIndex = 17
    '
    'mm_quant15
    '
    Me.mm_quant15.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant15.Caption = "QTA15"
    Me.mm_quant15.Enabled = True
    Me.mm_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant15.Name = "mm_quant15"
    Me.mm_quant15.NTSRepositoryComboBox = Nothing
    Me.mm_quant15.NTSRepositoryItemCheck = Nothing
    Me.mm_quant15.NTSRepositoryItemMemo = Nothing
    Me.mm_quant15.NTSRepositoryItemText = Nothing
    Me.mm_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant15.OptionsFilter.AllowFilter = False
    Me.mm_quant15.Visible = True
    Me.mm_quant15.VisibleIndex = 18
    '
    'mm_quant16
    '
    Me.mm_quant16.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant16.Caption = "QTA16"
    Me.mm_quant16.Enabled = True
    Me.mm_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant16.Name = "mm_quant16"
    Me.mm_quant16.NTSRepositoryComboBox = Nothing
    Me.mm_quant16.NTSRepositoryItemCheck = Nothing
    Me.mm_quant16.NTSRepositoryItemMemo = Nothing
    Me.mm_quant16.NTSRepositoryItemText = Nothing
    Me.mm_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant16.OptionsFilter.AllowFilter = False
    Me.mm_quant16.Visible = True
    Me.mm_quant16.VisibleIndex = 19
    '
    'mm_quant17
    '
    Me.mm_quant17.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant17.Caption = "QTA17"
    Me.mm_quant17.Enabled = True
    Me.mm_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant17.Name = "mm_quant17"
    Me.mm_quant17.NTSRepositoryComboBox = Nothing
    Me.mm_quant17.NTSRepositoryItemCheck = Nothing
    Me.mm_quant17.NTSRepositoryItemMemo = Nothing
    Me.mm_quant17.NTSRepositoryItemText = Nothing
    Me.mm_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant17.OptionsFilter.AllowFilter = False
    Me.mm_quant17.Visible = True
    Me.mm_quant17.VisibleIndex = 20
    '
    'mm_quant18
    '
    Me.mm_quant18.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant18.Caption = "QTA18"
    Me.mm_quant18.Enabled = True
    Me.mm_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant18.Name = "mm_quant18"
    Me.mm_quant18.NTSRepositoryComboBox = Nothing
    Me.mm_quant18.NTSRepositoryItemCheck = Nothing
    Me.mm_quant18.NTSRepositoryItemMemo = Nothing
    Me.mm_quant18.NTSRepositoryItemText = Nothing
    Me.mm_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant18.OptionsFilter.AllowFilter = False
    Me.mm_quant18.Visible = True
    Me.mm_quant18.VisibleIndex = 21
    '
    'mm_quant19
    '
    Me.mm_quant19.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant19.Caption = "QTA19"
    Me.mm_quant19.Enabled = True
    Me.mm_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant19.Name = "mm_quant19"
    Me.mm_quant19.NTSRepositoryComboBox = Nothing
    Me.mm_quant19.NTSRepositoryItemCheck = Nothing
    Me.mm_quant19.NTSRepositoryItemMemo = Nothing
    Me.mm_quant19.NTSRepositoryItemText = Nothing
    Me.mm_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant19.OptionsFilter.AllowFilter = False
    Me.mm_quant19.Visible = True
    Me.mm_quant19.VisibleIndex = 22
    '
    'mm_quant20
    '
    Me.mm_quant20.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant20.Caption = "QTA20"
    Me.mm_quant20.Enabled = True
    Me.mm_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant20.Name = "mm_quant20"
    Me.mm_quant20.NTSRepositoryComboBox = Nothing
    Me.mm_quant20.NTSRepositoryItemCheck = Nothing
    Me.mm_quant20.NTSRepositoryItemMemo = Nothing
    Me.mm_quant20.NTSRepositoryItemText = Nothing
    Me.mm_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant20.OptionsFilter.AllowFilter = False
    Me.mm_quant20.Visible = True
    Me.mm_quant20.VisibleIndex = 23
    '
    'mm_quant21
    '
    Me.mm_quant21.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant21.Caption = "QTA21"
    Me.mm_quant21.Enabled = True
    Me.mm_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant21.Name = "mm_quant21"
    Me.mm_quant21.NTSRepositoryComboBox = Nothing
    Me.mm_quant21.NTSRepositoryItemCheck = Nothing
    Me.mm_quant21.NTSRepositoryItemMemo = Nothing
    Me.mm_quant21.NTSRepositoryItemText = Nothing
    Me.mm_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant21.OptionsFilter.AllowFilter = False
    Me.mm_quant21.Visible = True
    Me.mm_quant21.VisibleIndex = 24
    '
    'mm_quant22
    '
    Me.mm_quant22.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant22.Caption = "QTA22"
    Me.mm_quant22.Enabled = True
    Me.mm_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant22.Name = "mm_quant22"
    Me.mm_quant22.NTSRepositoryComboBox = Nothing
    Me.mm_quant22.NTSRepositoryItemCheck = Nothing
    Me.mm_quant22.NTSRepositoryItemMemo = Nothing
    Me.mm_quant22.NTSRepositoryItemText = Nothing
    Me.mm_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant22.OptionsFilter.AllowFilter = False
    Me.mm_quant22.Visible = True
    Me.mm_quant22.VisibleIndex = 25
    '
    'mm_quant23
    '
    Me.mm_quant23.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant23.Caption = "QTA23"
    Me.mm_quant23.Enabled = True
    Me.mm_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant23.Name = "mm_quant23"
    Me.mm_quant23.NTSRepositoryComboBox = Nothing
    Me.mm_quant23.NTSRepositoryItemCheck = Nothing
    Me.mm_quant23.NTSRepositoryItemMemo = Nothing
    Me.mm_quant23.NTSRepositoryItemText = Nothing
    Me.mm_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant23.OptionsFilter.AllowFilter = False
    Me.mm_quant23.Visible = True
    Me.mm_quant23.VisibleIndex = 26
    '
    'mm_quant24
    '
    Me.mm_quant24.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant24.Caption = "QTA24"
    Me.mm_quant24.Enabled = True
    Me.mm_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant24.Name = "mm_quant24"
    Me.mm_quant24.NTSRepositoryComboBox = Nothing
    Me.mm_quant24.NTSRepositoryItemCheck = Nothing
    Me.mm_quant24.NTSRepositoryItemMemo = Nothing
    Me.mm_quant24.NTSRepositoryItemText = Nothing
    Me.mm_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant24.OptionsFilter.AllowFilter = False
    Me.mm_quant24.Visible = True
    Me.mm_quant24.VisibleIndex = 27
    '
    'FRMORDTAG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(492, 166)
    Me.Controls.Add(Me.grTco)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(1500, 200)
    Me.MinimizeBox = False
    Me.Name = "FRMORDTAG"
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

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 129048470351076307, "Griglia dettaglio taglie"))
      mm_riga.NTSSetParamNUM(oMenu, "Riga", "0", 6)
      mm_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457546406250, "Codice articolo"), 0, True)
      mm_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571457556406250, "Descrizione"), 0, True)
      mm_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128685783617187500, "Fase"), "0", 3)
      mm_desfase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128685783591406250, "Descrizione fase"), 0, True)
      mm_caption.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128556540578040000, "Tipo riga"), 0, True)
      mm_quant01.NTSSetParamNUM(oMenu, "Quantità 1^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant02.NTSSetParamNUM(oMenu, "Quantità 2^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant03.NTSSetParamNUM(oMenu, "Quantità 3^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant04.NTSSetParamNUM(oMenu, "Quantità 4^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant05.NTSSetParamNUM(oMenu, "Quantità 5^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant06.NTSSetParamNUM(oMenu, "Quantità 6^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant07.NTSSetParamNUM(oMenu, "Quantità 7^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant08.NTSSetParamNUM(oMenu, "Quantità 8^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant09.NTSSetParamNUM(oMenu, "Quantità 9^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant10.NTSSetParamNUM(oMenu, "Quantità 10^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant11.NTSSetParamNUM(oMenu, "Quantità 11^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant12.NTSSetParamNUM(oMenu, "Quantità 12^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant13.NTSSetParamNUM(oMenu, "Quantità 13^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant14.NTSSetParamNUM(oMenu, "Quantità 14^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant15.NTSSetParamNUM(oMenu, "Quantità 15^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant16.NTSSetParamNUM(oMenu, "Quantità 16^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant17.NTSSetParamNUM(oMenu, "Quantità 17^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant18.NTSSetParamNUM(oMenu, "Quantità 18^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant19.NTSSetParamNUM(oMenu, "Quantità 19^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant20.NTSSetParamNUM(oMenu, "Quantità 20^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant21.NTSSetParamNUM(oMenu, "Quantità 21^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant22.NTSSetParamNUM(oMenu, "Quantità 22^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant23.NTSSetParamNUM(oMenu, "Quantità 23^ taglia", "#,##0", 6, -99999, 99999)
      mm_quant24.NTSSetParamNUM(oMenu, "Quantità 24^ taglia", "#,##0", 6, -99999, 99999)

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

  Public Overridable Sub InitEntity(ByRef cleGnof As CLEORGNOF)
    '----------------------------------------------------------------------------------------------------------------
    oCleGnof = cleGnof
    '----------------------------------------------------------------------------------------------------------------
    AddHandler oCleGnof.RemoteEvent, AddressOf GestisciEventiEntity
    '----------------------------------------------------------------------------------------------------------------
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMORDTAG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      oCleGnof.bDtagAnnullato = False
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not Apri() Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'collego i dati alla griglia
      dsDtag.Tables("MMTRANSTC").AcceptChanges()
      dcDtag.DataSource = dsDtag.Tables("MMTRANSTC")
      dcDtag.Sort = "mm_riga ASC"
      grTco.DataSource = dcDtag

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      mm_riga.Visible = False
      mm_codart.Visible = False
      mm_descr.Visible = False
      mm_fase.Visible = False
      mm_desfase.Visible = False

      If oCleGnof.bDtagAbilCampi = False Then
        grvTco.Enabled = False
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORDTAG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDtag.Dispose()
      dsDtag.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMORDTAG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
      oCleGnof.bDtagAnnullato = False
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Not Salva() Then Return
      oCleGnof.bDtagAnnullato = False
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      dsDtag.Tables("MMTRANSTC").RejectChanges()
      grvTco.NTSMoveFirstColunn()
      grvTco.RefreshData()

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
  Public Overridable Sub grvTco_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTco.NTSFocusedRowChanged
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      '-------------------------
      'carico l'intestazione delle colonne
      If oMenu.ValCodiceDb(oCleGnof.strDtagCodart, DittaCorrente, "ARTICO", "S", "", dttTmp) Then
        oMenu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!ar_codtagl), DittaCorrente, "TABTAGL", "N", "", dttTmp)
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

      If grvTco.NTSGetCurrentDataRow Is Nothing Then Return
      '-------------------------
      'posso modificare solo la riga delle quantità
      If grvTco.NTSGetCurrentDataRow!mm_tipo.ToString <> "Q" Then
        grvTco.Enabled = False
      End If

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
      oCleGnof.GetTco(dsDtag)

      '-------------------------------------------------
      'aggiungo la colonna dell'intestazione
      dsDtag.Tables("MMTRANSTC").Columns.Add("mm_caption", GetType(String))
      dsDtag.Tables("MMTRANSTC").Columns.Add("mm_tipo", GetType(String))

      For l = 0 To dsDtag.Tables("MMTRANSTC").Rows.Count - 1
        dsDtag.Tables("MMTRANSTC").Rows(l)!mm_tipo = "Q" 'quantità
        dsDtag.Tables("MMTRANSTC").Rows(l)!mm_caption = "Qta per taglia"
      Next

      dsDtag.Tables("MMTRANSTC").AcceptChanges()

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
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      '----------------------------
      'sommo totale taglie e confermo tutto
      dsDtag.Tables("MMTRANSTC").Rows(0).AcceptChanges()
      If oCleGnof.bDtagAbilCampi Then oCleGnof.UpdateMmtranstc(dsDtag)
      oCleGnof.dDtagQuant = 0
      For i = 1 To 24
        oCleGnof.dDtagQuant = oCleGnof.dDtagQuant + NTSCDec(dsDtag.Tables("MMTRANSTC").Rows(0)("mm_quant" & i.ToString.PadLeft(2, "0"c)))
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
