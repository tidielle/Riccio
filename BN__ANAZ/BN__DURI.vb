Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DURI
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDuri As DataSet
  Public dcDuri As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grDuri As NTSInformatica.NTSGrid
  Public WithEvents grvDuri As NTSInformatica.NTSGridView
  Public WithEvents tb_utipreg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_unumreg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desduri As NTSInformatica.NTSGridColumn
  Public WithEvents tb_udatreg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_unureg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_unumpag As NTSInformatica.NTSGridColumn
  Public WithEvents tb_ucodatti As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ucodatti As NTSInformatica.NTSGridColumn
  Public WithEvents tb_ucoddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ucoddest As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipoprot As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tiponume As NTSInformatica.NTSGridColumn
  Public WithEvents tb_flvenesd As NTSInformatica.NTSGridColumn
  Public WithEvents tb_serfatt As NTSInformatica.NTSGridColumn
  Public WithEvents tb_sernoac As NTSInformatica.NTSGridColumn
  Public WithEvents tb_sernoad As NTSInformatica.NTSGridColumn
  Public WithEvents tlbAzzera As NTSInformatica.NTSBarButtonItem

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DURI))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbAzzera = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grDuri = New NTSInformatica.NTSGrid
    Me.grvDuri = New NTSInformatica.NTSGridView
    Me.tb_ftfig = New NTSInformatica.NTSGridColumn
    Me.tb_utipreg = New NTSInformatica.NTSGridColumn
    Me.tb_unumreg = New NTSInformatica.NTSGridColumn
    Me.tb_desduri = New NTSInformatica.NTSGridColumn
    Me.tb_udatreg = New NTSInformatica.NTSGridColumn
    Me.tb_unureg = New NTSInformatica.NTSGridColumn
    Me.tb_unumpag = New NTSInformatica.NTSGridColumn
    Me.tb_ucodatti = New NTSInformatica.NTSGridColumn
    Me.xx_ucodatti = New NTSInformatica.NTSGridColumn
    Me.tb_ucoddest = New NTSInformatica.NTSGridColumn
    Me.xx_ucoddest = New NTSInformatica.NTSGridColumn
    Me.tb_tipoprot = New NTSInformatica.NTSGridColumn
    Me.tb_tiponume = New NTSInformatica.NTSGridColumn
    Me.tb_flvenesd = New NTSInformatica.NTSGridColumn
    Me.tb_serfatt = New NTSInformatica.NTSGridColumn
    Me.tb_sernoac = New NTSInformatica.NTSGridColumn
    Me.tb_sernoad = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDuri, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDuri, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbAzzera})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAzzera, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbAzzera
    '
    Me.tlbAzzera.Caption = "Azzera progressivi"
    Me.tlbAzzera.Glyph = CType(resources.GetObject("tlbAzzera.Glyph"), System.Drawing.Image)
    Me.tlbAzzera.Id = 17
    Me.tlbAzzera.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbAzzera.Name = "tlbAzzera"
    Me.tlbAzzera.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grDuri
    '
    Me.grDuri.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDuri.EmbeddedNavigator.Name = ""
    Me.grDuri.Location = New System.Drawing.Point(0, 30)
    Me.grDuri.MainView = Me.grvDuri
    Me.grDuri.Name = "grDuri"
    Me.grDuri.Size = New System.Drawing.Size(648, 412)
    Me.grDuri.TabIndex = 6
    Me.grDuri.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDuri})
    '
    'grvDuri
    '
    Me.grvDuri.ActiveFilterEnabled = False
    '
    'tb_ftfig
    '
    Me.tb_ftfig.AppearanceCell.Options.UseBackColor = True
    Me.tb_ftfig.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ftfig.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ftfig.Caption = "Fatt. figurativa"
    Me.tb_ftfig.Enabled = True
    Me.tb_ftfig.FieldName = "tb_ftfig"
    Me.tb_ftfig.Name = "tb_ftfig"
    Me.tb_ftfig.NTSRepositoryComboBox = Nothing
    Me.tb_ftfig.NTSRepositoryItemCheck = Nothing
    Me.tb_ftfig.NTSRepositoryItemMemo = Nothing
    Me.tb_ftfig.NTSRepositoryItemText = Nothing
    Me.tb_ftfig.Visible = True
    Me.tb_ftfig.VisibleIndex = 16
    Me.grvDuri.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_utipreg, Me.tb_unumreg, Me.tb_desduri, Me.tb_udatreg, Me.tb_unureg, Me.tb_unumpag, Me.tb_ucodatti, Me.xx_ucodatti, Me.tb_ucoddest, Me.xx_ucoddest, Me.tb_tipoprot, Me.tb_tiponume, Me.tb_flvenesd, Me.tb_serfatt, Me.tb_sernoac, Me.tb_sernoad, Me.tb_ftfig})
    Me.grvDuri.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDuri.Enabled = True
    Me.grvDuri.GridControl = Me.grDuri
    Me.grvDuri.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDuri.MinRowHeight = 14
    Me.grvDuri.Name = "grvDuri"
    Me.grvDuri.NTSAllowDelete = True
    Me.grvDuri.NTSAllowInsert = True
    Me.grvDuri.NTSAllowUpdate = True
    Me.grvDuri.NTSMenuContext = Nothing
    Me.grvDuri.OptionsCustomization.AllowRowSizing = True
    Me.grvDuri.OptionsFilter.AllowFilterEditor = False
    Me.grvDuri.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDuri.OptionsNavigation.UseTabKey = False
    Me.grvDuri.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDuri.OptionsView.ColumnAutoWidth = False
    Me.grvDuri.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDuri.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDuri.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDuri.OptionsView.ShowGroupPanel = False
    Me.grvDuri.RowHeight = 16
    '
    'tb_utipreg
    '
    Me.tb_utipreg.AppearanceCell.Options.UseBackColor = True
    Me.tb_utipreg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_utipreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_utipreg.Caption = "Registro"
    Me.tb_utipreg.Enabled = True
    Me.tb_utipreg.FieldName = "tb_utipreg"
    Me.tb_utipreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_utipreg.Name = "tb_utipreg"
    Me.tb_utipreg.NTSRepositoryComboBox = Nothing
    Me.tb_utipreg.NTSRepositoryItemCheck = Nothing
    Me.tb_utipreg.NTSRepositoryItemMemo = Nothing
    Me.tb_utipreg.NTSRepositoryItemText = Nothing
    Me.tb_utipreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_utipreg.OptionsFilter.AllowFilter = False
    Me.tb_utipreg.Visible = True
    Me.tb_utipreg.VisibleIndex = 0
    Me.tb_utipreg.Width = 70
    '
    'tb_unumreg
    '
    Me.tb_unumreg.AppearanceCell.Options.UseBackColor = True
    Me.tb_unumreg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_unumreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_unumreg.Caption = "Numero"
    Me.tb_unumreg.Enabled = True
    Me.tb_unumreg.FieldName = "tb_unumreg"
    Me.tb_unumreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_unumreg.Name = "tb_unumreg"
    Me.tb_unumreg.NTSRepositoryComboBox = Nothing
    Me.tb_unumreg.NTSRepositoryItemCheck = Nothing
    Me.tb_unumreg.NTSRepositoryItemMemo = Nothing
    Me.tb_unumreg.NTSRepositoryItemText = Nothing
    Me.tb_unumreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_unumreg.OptionsFilter.AllowFilter = False
    Me.tb_unumreg.Visible = True
    Me.tb_unumreg.VisibleIndex = 1
    Me.tb_unumreg.Width = 70
    '
    'tb_desduri
    '
    Me.tb_desduri.AppearanceCell.Options.UseBackColor = True
    Me.tb_desduri.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desduri.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desduri.Caption = "Descrizione"
    Me.tb_desduri.Enabled = True
    Me.tb_desduri.FieldName = "tb_desduri"
    Me.tb_desduri.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desduri.Name = "tb_desduri"
    Me.tb_desduri.NTSRepositoryComboBox = Nothing
    Me.tb_desduri.NTSRepositoryItemCheck = Nothing
    Me.tb_desduri.NTSRepositoryItemMemo = Nothing
    Me.tb_desduri.NTSRepositoryItemText = Nothing
    Me.tb_desduri.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desduri.OptionsFilter.AllowFilter = False
    Me.tb_desduri.Visible = True
    Me.tb_desduri.VisibleIndex = 2
    Me.tb_desduri.Width = 70
    '
    'tb_udatreg
    '
    Me.tb_udatreg.AppearanceCell.Options.UseBackColor = True
    Me.tb_udatreg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_udatreg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_udatreg.Caption = "Data Registraz."
    Me.tb_udatreg.Enabled = True
    Me.tb_udatreg.FieldName = "tb_udatreg"
    Me.tb_udatreg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_udatreg.Name = "tb_udatreg"
    Me.tb_udatreg.NTSRepositoryComboBox = Nothing
    Me.tb_udatreg.NTSRepositoryItemCheck = Nothing
    Me.tb_udatreg.NTSRepositoryItemMemo = Nothing
    Me.tb_udatreg.NTSRepositoryItemText = Nothing
    Me.tb_udatreg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_udatreg.OptionsFilter.AllowFilter = False
    Me.tb_udatreg.Visible = True
    Me.tb_udatreg.VisibleIndex = 3
    Me.tb_udatreg.Width = 70
    '
    'tb_unureg
    '
    Me.tb_unureg.AppearanceCell.Options.UseBackColor = True
    Me.tb_unureg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_unureg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_unureg.Caption = "Num. registraz."
    Me.tb_unureg.Enabled = True
    Me.tb_unureg.FieldName = "tb_unureg"
    Me.tb_unureg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_unureg.Name = "tb_unureg"
    Me.tb_unureg.NTSRepositoryComboBox = Nothing
    Me.tb_unureg.NTSRepositoryItemCheck = Nothing
    Me.tb_unureg.NTSRepositoryItemMemo = Nothing
    Me.tb_unureg.NTSRepositoryItemText = Nothing
    Me.tb_unureg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_unureg.OptionsFilter.AllowFilter = False
    Me.tb_unureg.Visible = True
    Me.tb_unureg.VisibleIndex = 4
    Me.tb_unureg.Width = 70
    '
    'tb_unumpag
    '
    Me.tb_unumpag.AppearanceCell.Options.UseBackColor = True
    Me.tb_unumpag.AppearanceCell.Options.UseTextOptions = True
    Me.tb_unumpag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_unumpag.Caption = "Num. pagina"
    Me.tb_unumpag.Enabled = True
    Me.tb_unumpag.FieldName = "tb_unumpag"
    Me.tb_unumpag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_unumpag.Name = "tb_unumpag"
    Me.tb_unumpag.NTSRepositoryComboBox = Nothing
    Me.tb_unumpag.NTSRepositoryItemCheck = Nothing
    Me.tb_unumpag.NTSRepositoryItemMemo = Nothing
    Me.tb_unumpag.NTSRepositoryItemText = Nothing
    Me.tb_unumpag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_unumpag.OptionsFilter.AllowFilter = False
    Me.tb_unumpag.Visible = True
    Me.tb_unumpag.VisibleIndex = 5
    Me.tb_unumpag.Width = 70
    '
    'tb_ucodatti
    '
    Me.tb_ucodatti.AppearanceCell.Options.UseBackColor = True
    Me.tb_ucodatti.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ucodatti.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ucodatti.Caption = "Cod. attività"
    Me.tb_ucodatti.Enabled = True
    Me.tb_ucodatti.FieldName = "tb_ucodatti"
    Me.tb_ucodatti.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_ucodatti.Name = "tb_ucodatti"
    Me.tb_ucodatti.NTSRepositoryComboBox = Nothing
    Me.tb_ucodatti.NTSRepositoryItemCheck = Nothing
    Me.tb_ucodatti.NTSRepositoryItemMemo = Nothing
    Me.tb_ucodatti.NTSRepositoryItemText = Nothing
    Me.tb_ucodatti.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_ucodatti.OptionsFilter.AllowFilter = False
    Me.tb_ucodatti.Visible = True
    Me.tb_ucodatti.VisibleIndex = 6
    Me.tb_ucodatti.Width = 70
    '
    'xx_ucodatti
    '
    Me.xx_ucodatti.AppearanceCell.Options.UseBackColor = True
    Me.xx_ucodatti.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ucodatti.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ucodatti.Caption = "Descr. attività"
    Me.xx_ucodatti.Enabled = False
    Me.xx_ucodatti.FieldName = "xx_ucodatti"
    Me.xx_ucodatti.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ucodatti.Name = "xx_ucodatti"
    Me.xx_ucodatti.NTSRepositoryComboBox = Nothing
    Me.xx_ucodatti.NTSRepositoryItemCheck = Nothing
    Me.xx_ucodatti.NTSRepositoryItemMemo = Nothing
    Me.xx_ucodatti.NTSRepositoryItemText = Nothing
    Me.xx_ucodatti.OptionsColumn.AllowEdit = False
    Me.xx_ucodatti.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ucodatti.OptionsColumn.ReadOnly = True
    Me.xx_ucodatti.OptionsFilter.AllowFilter = False
    Me.xx_ucodatti.Visible = True
    Me.xx_ucodatti.VisibleIndex = 7
    Me.xx_ucodatti.Width = 70
    '
    'tb_ucoddest
    '
    Me.tb_ucoddest.AppearanceCell.Options.UseBackColor = True
    Me.tb_ucoddest.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ucoddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ucoddest.Caption = "Destinaz. associata"
    Me.tb_ucoddest.Enabled = True
    Me.tb_ucoddest.FieldName = "tb_ucoddest"
    Me.tb_ucoddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_ucoddest.Name = "tb_ucoddest"
    Me.tb_ucoddest.NTSRepositoryComboBox = Nothing
    Me.tb_ucoddest.NTSRepositoryItemCheck = Nothing
    Me.tb_ucoddest.NTSRepositoryItemMemo = Nothing
    Me.tb_ucoddest.NTSRepositoryItemText = Nothing
    Me.tb_ucoddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_ucoddest.OptionsFilter.AllowFilter = False
    Me.tb_ucoddest.Visible = True
    Me.tb_ucoddest.VisibleIndex = 8
    Me.tb_ucoddest.Width = 70
    '
    'xx_ucoddest
    '
    Me.xx_ucoddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_ucoddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ucoddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ucoddest.Caption = "Descr. destinazione"
    Me.xx_ucoddest.Enabled = False
    Me.xx_ucoddest.FieldName = "xx_ucoddest"
    Me.xx_ucoddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ucoddest.Name = "xx_ucoddest"
    Me.xx_ucoddest.NTSRepositoryComboBox = Nothing
    Me.xx_ucoddest.NTSRepositoryItemCheck = Nothing
    Me.xx_ucoddest.NTSRepositoryItemMemo = Nothing
    Me.xx_ucoddest.NTSRepositoryItemText = Nothing
    Me.xx_ucoddest.OptionsColumn.AllowEdit = False
    Me.xx_ucoddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ucoddest.OptionsColumn.ReadOnly = True
    Me.xx_ucoddest.OptionsFilter.AllowFilter = False
    Me.xx_ucoddest.Visible = True
    Me.xx_ucoddest.VisibleIndex = 9
    Me.xx_ucoddest.Width = 70
    '
    'tb_tipoprot
    '
    Me.tb_tipoprot.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipoprot.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipoprot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipoprot.Caption = "Rel doc./prot. vend."
    Me.tb_tipoprot.Enabled = True
    Me.tb_tipoprot.FieldName = "tb_tipoprot"
    Me.tb_tipoprot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipoprot.Name = "tb_tipoprot"
    Me.tb_tipoprot.NTSRepositoryComboBox = Nothing
    Me.tb_tipoprot.NTSRepositoryItemCheck = Nothing
    Me.tb_tipoprot.NTSRepositoryItemMemo = Nothing
    Me.tb_tipoprot.NTSRepositoryItemText = Nothing
    Me.tb_tipoprot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipoprot.OptionsFilter.AllowFilter = False
    Me.tb_tipoprot.Visible = True
    Me.tb_tipoprot.VisibleIndex = 10
    Me.tb_tipoprot.Width = 70
    '
    'tb_tiponume
    '
    Me.tb_tiponume.AppearanceCell.Options.UseBackColor = True
    Me.tb_tiponume.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tiponume.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tiponume.Caption = "Contr. serie prot."
    Me.tb_tiponume.Enabled = True
    Me.tb_tiponume.FieldName = "tb_tiponume"
    Me.tb_tiponume.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tiponume.Name = "tb_tiponume"
    Me.tb_tiponume.NTSRepositoryComboBox = Nothing
    Me.tb_tiponume.NTSRepositoryItemCheck = Nothing
    Me.tb_tiponume.NTSRepositoryItemMemo = Nothing
    Me.tb_tiponume.NTSRepositoryItemText = Nothing
    Me.tb_tiponume.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tiponume.OptionsFilter.AllowFilter = False
    Me.tb_tiponume.Visible = True
    Me.tb_tiponume.VisibleIndex = 11
    Me.tb_tiponume.Width = 70
    '
    'tb_flvenesd
    '
    Me.tb_flvenesd.AppearanceCell.Options.UseBackColor = True
    Me.tb_flvenesd.AppearanceCell.Options.UseTextOptions = True
    Me.tb_flvenesd.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_flvenesd.Caption = "Contr. serie doc."
    Me.tb_flvenesd.Enabled = True
    Me.tb_flvenesd.FieldName = "tb_flvenesd"
    Me.tb_flvenesd.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_flvenesd.Name = "tb_flvenesd"
    Me.tb_flvenesd.NTSRepositoryComboBox = Nothing
    Me.tb_flvenesd.NTSRepositoryItemCheck = Nothing
    Me.tb_flvenesd.NTSRepositoryItemMemo = Nothing
    Me.tb_flvenesd.NTSRepositoryItemText = Nothing
    Me.tb_flvenesd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_flvenesd.OptionsFilter.AllowFilter = False
    Me.tb_flvenesd.Visible = True
    Me.tb_flvenesd.VisibleIndex = 12
    Me.tb_flvenesd.Width = 70
    '
    'tb_serfatt
    '
    Me.tb_serfatt.AppearanceCell.Options.UseBackColor = True
    Me.tb_serfatt.AppearanceCell.Options.UseTextOptions = True
    Me.tb_serfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_serfatt.Caption = "Serie prot. fatture"
    Me.tb_serfatt.Enabled = True
    Me.tb_serfatt.FieldName = "tb_serfatt"
    Me.tb_serfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_serfatt.Name = "tb_serfatt"
    Me.tb_serfatt.NTSRepositoryComboBox = Nothing
    Me.tb_serfatt.NTSRepositoryItemCheck = Nothing
    Me.tb_serfatt.NTSRepositoryItemMemo = Nothing
    Me.tb_serfatt.NTSRepositoryItemText = Nothing
    Me.tb_serfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_serfatt.OptionsFilter.AllowFilter = False
    Me.tb_serfatt.Visible = True
    Me.tb_serfatt.VisibleIndex = 13
    Me.tb_serfatt.Width = 70
    '
    'tb_sernoac
    '
    Me.tb_sernoac.AppearanceCell.Options.UseBackColor = True
    Me.tb_sernoac.AppearanceCell.Options.UseTextOptions = True
    Me.tb_sernoac.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_sernoac.Caption = "Serie prot. note accr."
    Me.tb_sernoac.Enabled = True
    Me.tb_sernoac.FieldName = "tb_sernoac"
    Me.tb_sernoac.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_sernoac.Name = "tb_sernoac"
    Me.tb_sernoac.NTSRepositoryComboBox = Nothing
    Me.tb_sernoac.NTSRepositoryItemCheck = Nothing
    Me.tb_sernoac.NTSRepositoryItemMemo = Nothing
    Me.tb_sernoac.NTSRepositoryItemText = Nothing
    Me.tb_sernoac.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_sernoac.OptionsFilter.AllowFilter = False
    Me.tb_sernoac.Visible = True
    Me.tb_sernoac.VisibleIndex = 14
    Me.tb_sernoac.Width = 70
    '
    'tb_sernoad
    '
    Me.tb_sernoad.AppearanceCell.Options.UseBackColor = True
    Me.tb_sernoad.AppearanceCell.Options.UseTextOptions = True
    Me.tb_sernoad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_sernoad.Caption = "Serie prot. note addeb."
    Me.tb_sernoad.Enabled = True
    Me.tb_sernoad.FieldName = "tb_sernoad"
    Me.tb_sernoad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_sernoad.Name = "tb_sernoad"
    Me.tb_sernoad.NTSRepositoryComboBox = Nothing
    Me.tb_sernoad.NTSRepositoryItemCheck = Nothing
    Me.tb_sernoad.NTSRepositoryItemMemo = Nothing
    Me.tb_sernoad.NTSRepositoryItemText = Nothing
    Me.tb_sernoad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_sernoad.OptionsFilter.AllowFilter = False
    Me.tb_sernoad.Visible = True
    Me.tb_sernoad.VisibleIndex = 15
    Me.tb_sernoad.Width = 70
    '
    'FRM__DURI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDuri)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__DURI"
    Me.Text = "REGISTRI IVA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDuri, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDuri, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ, ByRef ds As DataSet, ByVal nAnnoAperto As Integer)
    oCleAnaz = cleAnaz
    oCleAnaz.nAnnoTabduriAperto = nAnnoAperto
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

    '-------------------------------------------------
    'leggo dal database i dati e collego il NTSBinding
    dsDuri = ds
    oCleAnaz.DuriSetDataTable(DittaCorrente, dsDuri.Tables("TABDURI"))
    dcDuri.DataSource = dsDuri.Tables("TABDURI")
    dsDuri.AcceptChanges()
    grDuri.DataSource = dcDuri

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
        tlbAzzera.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '---------------------
      Dim dttUtipreg As New DataTable()
      dttUtipreg.Columns.Add("cod", GetType(String))
      dttUtipreg.Columns.Add("val", GetType(String))
      dttUtipreg.Rows.Add(New Object() {"A", "Acquisti"})
      dttUtipreg.Rows.Add(New Object() {"V", "Vendite"})
      dttUtipreg.Rows.Add(New Object() {"C", "Corrispettivi"})
      dttUtipreg.Rows.Add(New Object() {"S", "In sospensione"})
      dttUtipreg.Rows.Add(New Object() {"T", "In sospensione Acq."})
      dttUtipreg.AcceptChanges()

      Dim dttTipoprot As New DataTable()
      dttTipoprot.Columns.Add("cod", GetType(String))
      dttTipoprot.Columns.Add("val", GetType(String))
      dttTipoprot.Rows.Add(New Object() {"0", "Svincolati"})
      dttTipoprot.Rows.Add(New Object() {"1", "Coincidenti"})
      dttTipoprot.AcceptChanges()

      Dim dttTiponume As New DataTable()
      dttTiponume.Columns.Add("cod", GetType(String))
      dttTiponume.Columns.Add("val", GetType(String))
      dttTiponume.Rows.Add(New Object() {"0", "No verifica"})
      dttTiponume.Rows.Add(New Object() {"1", "Una numerazione"})
      dttTiponume.Rows.Add(New Object() {"2", "Due numerazioni"})
      dttTiponume.Rows.Add(New Object() {"3", "Tre numerazioni"})
      dttTiponume.AcceptChanges()

      grvDuri.NTSSetParam(oMenu, "ATTIVITA' IVA")
      tb_utipreg.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128649229125000000, "Registro"), dttUtipreg, "val", "cod")
      tb_unumreg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128649229125156250, "Numero"), "0", 3, 0, 999)
      tb_udatreg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128649229125312500, "Data Registraz."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_unureg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128649229125468750, "Num. registraz."), "0", 9, 0, 999999999)
      tb_unumpag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128649229125625000, "Num. pagina"), "0", 9, 0, 999999999)
      tb_ucodatti.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128649229125781250, "Cod. attività"), tabatti)
      tb_desduri.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649229125937500, "Descrizione"), 30, True)
      tb_ucoddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128649229126093750, "Destinaz. associata"), tabdestdiv)
      tb_tipoprot.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128649229126250000, "Rel doc./prot."), dttTipoprot, "val", "cod")
      tb_tiponume.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128649229126406250, "Contr. serie prot."), dttTiponume, "val", "cod")
      tb_serfatt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649229126562500, "Serie prot. fatture"), CLN__STD.SerieMaxLen, False)
      tb_sernoac.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649229126718750, "Serie prot. note accr."), CLN__STD.SerieMaxLen, False)
      tb_sernoad.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649229126875000, "Serie prot. note addeb."), CLN__STD.SerieMaxLen, False)
      tb_flvenesd.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128649229127031250, "Contr. serie doc."), "S", "N")
      xx_ucodatti.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654251669062500, "Descr. attività"), 0, True)
      xx_ucoddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654251669375000, "Descr. destinazione"), 0, True)
      tb_ftfig.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128649229127031251, "Fatt.figurativa"), "S", "N")

      tb_utipreg.NTSSetRichiesto()
      tb_unumreg.NTSSetRichiesto()
      tb_ucodatti.NTSSetRichiesto()

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
  Public Overridable Sub FRM__DURI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Text = oApp.Tr(Me, 129006947279494981, "REGISTRI IVA ANNO ") & oCleAnaz.nAnnoTabduriAperto.ToString

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDuri_NTSFocusedRowChanged(grvDuri, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      SetColonneFRIENDLY()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DURI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDuri.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvDuri.NTSDeleteRigaCorrente(dcDuri, True) Then Return
      oCleAnaz.DuriSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDuri.NTSRipristinaRigaCorrenteBefore(dcDuri, True) Then Return
      oCleAnaz.DuriRipristina(dcDuri.Position, dcDuri.Filter)
      grvDuri.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If grvDuri.FocusedColumn.Name = "tb_ucodatti" Then
        For i = 0 To oCleAnaz.dsShared.Tables("TABATTI").Rows.Count - 1
          If NTSCInt(oCleAnaz.dsShared.Tables("TABATTI").Rows(i)!tb_anno) = oCleAnaz.nAnnoTabduriAperto Then
            strTmp += oCleAnaz.dsShared.Tables("TABATTI").Rows(i)!tb_codatti.ToString & " - " & _
            oCleAnaz.dsShared.Tables("TABATTI").Rows(i)!tb_desatti.ToString & vbCrLf
          End If
        Next
        If strTmp <> "" Then oApp.MsgBoxInfo(strTmp)

      ElseIf grvDuri.FocusedColumn.Name = "tb_ucoddest" Then
        'non posso fare lo zoom standard, visto che potrei selez. una destinaz. diversa appena inserita e non ancora salvata ...
        'creo un dataset contenente tutte le destinazioni diverse che ho in memoria
        ds.Tables.Add(oCleAnaz.dsShared.Tables("ANAZUL").Clone)
        ds.Tables(0).TableName = "DESTDIV"
        For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
          ds.Tables(0).ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
        Next
        'rinomino le colonne per farle uguali a quelle dello zoom
        For i = 0 To ds.Tables("DESTDIV").Columns.Count - 1
          If ds.Tables("DESTDIV").Columns(i).ColumnName.ToLower.Substring(0, 2) = "ul" Then
            ds.Tables("DESTDIV").Columns(i).ColumnName = "dd_" & ds.Tables("DESTDIV").Columns(i).ColumnName.Substring(3)
          End If
        Next
        ds.Tables(0).AcceptChanges()
        NTSZOOM.strIn = NTSCStr(grvDuri.EditingValue)
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvDuri.EditingValue) Then grvDuri.SetFocusedValue(NTSZOOM.strIn)
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbAzzera_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAzzera.ItemClick
    Try
      If Not Salva() Then Return

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128649260018281250, _
                    "Scegliendo 'Sì' saranno azzerati tutti i progressivi." & vbCrLf & _
                    "Continuare con l'operazione?")) = Windows.Forms.DialogResult.No Then Return

      For Each dtrT As DataRow In dsDuri.Tables("TABDURI").Rows
        dtrT!tb_udatreg = NTSCDate(dtrT!tb_udatreg).AddDays(1)
        dtrT!tb_unureg = 0
        dtrT!tb_ualfdoc = " "
        dtrT!tb_unumpro = 0
        dtrT!tb_unumpag = 0
        dtrT!tb_unureg = 0
      Next
      dsDuri.Tables("TABDURI").AcceptChanges()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvDuri_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDuri.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDuri_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDuri.NTSFocusedRowChanged
    Try
      If NTSCInt(grvDuri.GetFocusedRowCellValue(tb_unumreg)) <> 0 Then
        tb_utipreg.Enabled = False
        tb_unumreg.Enabled = False
      Else
        GctlSetVisEnab(tb_utipreg, False)
        GctlSetVisEnab(tb_unumreg, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvDuri.NTSSalvaRigaCorrente(dcDuri, oCleAnaz.DuriRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DuriSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DuriRipristina(dcDuri.Position, dcDuri.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  '--- Gestione tasti apri e nuovo zoom veloce destinazione
  Public Overridable Sub tb_ucoddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles tb_ucoddest.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmdesg As FRM__DESD = Nothing
    Try
      Me.ValidaLastControl()
      frmdesg = CType(NTSNewFormModal("FRM__DESD"), FRM__DESD)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If IsNumeric(dsDuri.Tables("TABDURI").Rows(dcDuri.Position)!tb_ucoddest) Then
          nCodDestTmp = NTSCInt(dsDuri.Tables("TABDURI").Rows(dcDuri.Position)!tb_ucoddest)
        End If
        oCallParamsTmp.strParam = "APRI;" & nCodDestTmp
      Else
        nCodDestTmp = 0
        oCallParamsTmp.strParam = "NUOV;0"
      End If

      oTmp.Text = NTSCStr(nCodDestTmp)

      oCallParamsTmp.strPar1 = "Altri indirizzi"
      nTipo = 0
      If nCodDestTmp > 0 Then
        For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
          If nCodDestTmp = NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) Then
            Select Case NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest)
              Case oCleAnaz.lDestdomf
                oCallParamsTmp.strPar1 = "Domicilio fiscale per provv. amministr."
                nTipo = oCleAnaz.lDestdomf
              Case oCleAnaz.lDestsedel
                oCallParamsTmp.strPar1 = "Resid./Domic. fisc./Sede legale in Italia"
                nTipo = oCleAnaz.lDestsedel
              Case oCleAnaz.lDestresan
                oCallParamsTmp.strPar1 = "Residenza/Sede legale estera"
                nTipo = oCleAnaz.lDestresan
              Case oCleAnaz.lDestcorr
                oCallParamsTmp.strPar1 = "Luogo di esercizio attiv. all'estero"
                nTipo = oCleAnaz.lDestcorr
            End Select
          End If
        Next
      End If

      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(oCleAnaz.dsShared.Tables("ANAZUL").Clone())
      Select Case nTipo
        Case oCleAnaz.lDestdomf, oCleAnaz.lDestsedel, oCleAnaz.lDestresan, oCleAnaz.lDestcorr
          For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
            If NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) = nTipo Then
              ds.Tables("ANAZUL").ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
              oCleAnaz.dsShared.Tables("ANAZUL").Rows(i).Delete()
            End If
          Next
        Case Else
          For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
            If NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestdomf And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestsedel And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestresan And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestcorr Then
              ds.Tables("ANAZUL").ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
              oCleAnaz.dsShared.Tables("ANAZUL").Rows(i).Delete()
            End If
          Next
      End Select
      oCleAnaz.dsShared.Tables("ANAZUL").AcceptChanges()

      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleAnaz, ds, nTipo)
      If NTSCInt(oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azcodanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("ANAZUL").Rows.Count - 1
        If ds.Tables("ANAZUL").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("ANAZUL").Rows(i)!ul_coddest) > 0 Then
            oCleAnaz.dsShared.Tables("ANAZUL").ImportRow(ds.Tables("ANAZUL").Rows(i))
          Else
            ds.Tables("ANAZUL").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("ANAZUL").AcceptChanges()
      oCleAnaz.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If oCleAnaz.dsShared.Tables("TABANAZ").Rows(0).RowState = DataRowState.Unchanged Then oCleAnaz.dsShared.Tables("TABANAZ").Rows(0).SetModified()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
    End Try
  End Sub
  '---

  Public Overridable Sub SetColonneFRIENDLY()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      tb_utipreg.Enabled = False
      tb_unumreg.Enabled = False
      tb_ucodatti.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
