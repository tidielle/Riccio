Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__UICF
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

  Public oCleUicf As CLE__UICF
  Public oCallParams As CLE__CLDP
  Public dsUicf As DataSet
  Public dcUicf As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__UICF))
    Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Business")
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbTrova = New NTSInformatica.NTSBarMenuItem
    Me.tlbCancellaCartella = New NTSInformatica.NTSBarMenuItem
    Me.tlbEsporta = New NTSInformatica.NTSBarMenuItem
    Me.tlbEsportaAgg = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsportaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbImporta = New NTSInformatica.NTSBarMenuItem
    Me.tlbPersExport = New NTSInformatica.NTSBarMenuItem
    Me.tlbPersImport = New NTSInformatica.NTSBarMenuItem
    Me.tlbPersDelete = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grUicf = New NTSInformatica.NTSGrid
    Me.grvUicf = New NTSInformatica.NTSGridView
    Me.ui_db = New NTSInformatica.NTSGridColumn
    Me.ui_ditta = New NTSInformatica.NTSGridColumn
    Me.ui_tipodoc = New NTSInformatica.NTSGridColumn
    Me.ui_ruolo = New NTSInformatica.NTSGridColumn
    Me.ui_opnome = New NTSInformatica.NTSGridColumn
    Me.ui_codling = New NTSInformatica.NTSGridColumn
    Me.ui_nomprop = New NTSInformatica.NTSGridColumn
    Me.ui_valprop = New NTSInformatica.NTSGridColumn
    Me.ui_usascript = New NTSInformatica.NTSGridColumn
    Me.ui_script = New NTSInformatica.NTSGridColumn
    Me.ui_parent = New NTSInformatica.NTSGridColumn
    Me.pnSx = New NTSInformatica.NTSPanel
    Me.trUicf = New NTSInformatica.NTSTreeView
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cbVisualizza = New NTSInformatica.NTSComboBox
    Me.lbVisualizza = New NTSInformatica.NTSLabel
    Me.tlbTrasferisciConf = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grUicf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvUicf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSx.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.cbVisualizza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbTrova, Me.tlbEsporta, Me.tlbImporta, Me.tlbCancellaCartella, Me.tlbEsportaAgg, Me.tlbEsportaGriglia, Me.tlbPersDelete, Me.tlbPersImport, Me.tlbPersExport, Me.tlbTrasferisciConf})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTrova), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancellaCartella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsporta, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsportaAgg), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsportaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImporta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPersExport, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPersImport), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPersDelete), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTrasferisciConf)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbTrova
    '
    Me.tlbTrova.Caption = "Trova cartella"
    Me.tlbTrova.Id = 17
    Me.tlbTrova.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbTrova.Name = "tlbTrova"
    Me.tlbTrova.NTSIsCheckBox = False
    Me.tlbTrova.Visible = True
    '
    'tlbCancellaCartella
    '
    Me.tlbCancellaCartella.Caption = "Cancella cartella"
    Me.tlbCancellaCartella.Id = 20
    Me.tlbCancellaCartella.Name = "tlbCancellaCartella"
    Me.tlbCancellaCartella.NTSIsCheckBox = False
    Me.tlbCancellaCartella.Visible = True
    '
    'tlbEsporta
    '
    Me.tlbEsporta.Caption = "Esporta ramo completo"
    Me.tlbEsporta.Id = 18
    Me.tlbEsporta.Name = "tlbEsporta"
    Me.tlbEsporta.NTSIsCheckBox = False
    Me.tlbEsporta.Visible = True
    '
    'tlbEsportaAgg
    '
    Me.tlbEsportaAgg.Caption = "Esporta solo campi aggiunti"
    Me.tlbEsportaAgg.Id = 21
    Me.tlbEsportaAgg.Name = "tlbEsportaAgg"
    Me.tlbEsportaAgg.Visible = True
    '
    'tlbEsportaGriglia
    '
    Me.tlbEsportaGriglia.Caption = "Esporta personalizzazione griglia"
    Me.tlbEsportaGriglia.Id = 22
    Me.tlbEsportaGriglia.Name = "tlbEsportaGriglia"
    Me.tlbEsportaGriglia.Visible = True
    '
    'tlbImporta
    '
    Me.tlbImporta.Caption = "Importa"
    Me.tlbImporta.Id = 19
    Me.tlbImporta.Name = "tlbImporta"
    Me.tlbImporta.NTSIsCheckBox = False
    Me.tlbImporta.Visible = True
    '
    'tlbPersExport
    '
    Me.tlbPersExport.Caption = "Esporta personalizzazioni"
    Me.tlbPersExport.Id = 26
    Me.tlbPersExport.Name = "tlbPersExport"
    Me.tlbPersExport.NTSIsCheckBox = False
    Me.tlbPersExport.Visible = True
    '
    'tlbPersImport
    '
    Me.tlbPersImport.Caption = "Importa personalizzazioni"
    Me.tlbPersImport.Id = 25
    Me.tlbPersImport.Name = "tlbPersImport"
    Me.tlbPersImport.NTSIsCheckBox = False
    Me.tlbPersImport.Visible = True
    '
    'tlbPersDelete
    '
    Me.tlbPersDelete.Caption = "Cancella personalizzazioni"
    Me.tlbPersDelete.Id = 24
    Me.tlbPersDelete.Name = "tlbPersDelete"
    Me.tlbPersDelete.NTSIsCheckBox = False
    Me.tlbPersDelete.Visible = True
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
    'grUicf
    '
    Me.grUicf.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grUicf.EmbeddedNavigator.Name = ""
    Me.grUicf.Location = New System.Drawing.Point(0, 0)
    Me.grUicf.MainView = Me.grvUicf
    Me.grUicf.Name = "grUicf"
    Me.grUicf.Size = New System.Drawing.Size(442, 381)
    Me.grUicf.TabIndex = 5
    Me.grUicf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvUicf})
    '
    'grvUicf
    '
    Me.grvUicf.ActiveFilterEnabled = False
    Me.grvUicf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ui_db, Me.ui_ditta, Me.ui_tipodoc, Me.ui_ruolo, Me.ui_opnome, Me.ui_codling, Me.ui_nomprop, Me.ui_valprop, Me.ui_usascript, Me.ui_script, Me.ui_parent})
    Me.grvUicf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvUicf.Enabled = True
    Me.grvUicf.GridControl = Me.grUicf
    Me.grvUicf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvUicf.MinRowHeight = 14
    Me.grvUicf.Name = "grvUicf"
    Me.grvUicf.NTSAllowDelete = True
    Me.grvUicf.NTSAllowInsert = True
    Me.grvUicf.NTSAllowUpdate = True
    Me.grvUicf.NTSMenuContext = Nothing
    Me.grvUicf.OptionsCustomization.AllowRowSizing = True
    Me.grvUicf.OptionsFilter.AllowFilterEditor = False
    Me.grvUicf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvUicf.OptionsNavigation.UseTabKey = False
    Me.grvUicf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvUicf.OptionsView.ColumnAutoWidth = False
    Me.grvUicf.OptionsView.EnableAppearanceEvenRow = True
    Me.grvUicf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvUicf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvUicf.OptionsView.ShowGroupPanel = False
    Me.grvUicf.RowHeight = 16
    '
    'ui_db
    '
    Me.ui_db.AppearanceCell.Options.UseBackColor = True
    Me.ui_db.AppearanceCell.Options.UseTextOptions = True
    Me.ui_db.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_db.Caption = "Database"
    Me.ui_db.Enabled = True
    Me.ui_db.FieldName = "ui_db"
    Me.ui_db.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_db.Name = "ui_db"
    Me.ui_db.NTSRepositoryComboBox = Nothing
    Me.ui_db.NTSRepositoryItemCheck = Nothing
    Me.ui_db.NTSRepositoryItemMemo = Nothing
    Me.ui_db.NTSRepositoryItemText = Nothing
    Me.ui_db.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_db.OptionsFilter.AllowFilter = False
    Me.ui_db.Visible = True
    Me.ui_db.VisibleIndex = 0
    Me.ui_db.Width = 70
    '
    'ui_ditta
    '
    Me.ui_ditta.AppearanceCell.Options.UseBackColor = True
    Me.ui_ditta.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ditta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ditta.Caption = "Ditta"
    Me.ui_ditta.Enabled = True
    Me.ui_ditta.FieldName = "ui_ditta"
    Me.ui_ditta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ditta.Name = "ui_ditta"
    Me.ui_ditta.NTSRepositoryComboBox = Nothing
    Me.ui_ditta.NTSRepositoryItemCheck = Nothing
    Me.ui_ditta.NTSRepositoryItemMemo = Nothing
    Me.ui_ditta.NTSRepositoryItemText = Nothing
    Me.ui_ditta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ditta.OptionsFilter.AllowFilter = False
    Me.ui_ditta.Visible = True
    Me.ui_ditta.VisibleIndex = 1
    Me.ui_ditta.Width = 70
    '
    'ui_tipodoc
    '
    Me.ui_tipodoc.AppearanceCell.Options.UseBackColor = True
    Me.ui_tipodoc.AppearanceCell.Options.UseTextOptions = True
    Me.ui_tipodoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_tipodoc.Caption = "Tipo documento"
    Me.ui_tipodoc.Enabled = True
    Me.ui_tipodoc.FieldName = "ui_tipodoc"
    Me.ui_tipodoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_tipodoc.Name = "ui_tipodoc"
    Me.ui_tipodoc.NTSRepositoryComboBox = Nothing
    Me.ui_tipodoc.NTSRepositoryItemCheck = Nothing
    Me.ui_tipodoc.NTSRepositoryItemMemo = Nothing
    Me.ui_tipodoc.NTSRepositoryItemText = Nothing
    Me.ui_tipodoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_tipodoc.OptionsFilter.AllowFilter = False
    Me.ui_tipodoc.Visible = True
    Me.ui_tipodoc.VisibleIndex = 2
    Me.ui_tipodoc.Width = 70
    '
    'ui_ruolo
    '
    Me.ui_ruolo.AppearanceCell.Options.UseBackColor = True
    Me.ui_ruolo.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ruolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ruolo.Caption = "Ruolo operat."
    Me.ui_ruolo.Enabled = True
    Me.ui_ruolo.FieldName = "ui_ruolo"
    Me.ui_ruolo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ruolo.Name = "ui_ruolo"
    Me.ui_ruolo.NTSRepositoryComboBox = Nothing
    Me.ui_ruolo.NTSRepositoryItemCheck = Nothing
    Me.ui_ruolo.NTSRepositoryItemMemo = Nothing
    Me.ui_ruolo.NTSRepositoryItemText = Nothing
    Me.ui_ruolo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ruolo.OptionsFilter.AllowFilter = False
    Me.ui_ruolo.Visible = True
    Me.ui_ruolo.VisibleIndex = 3
    Me.ui_ruolo.Width = 70
    '
    'ui_opnome
    '
    Me.ui_opnome.AppearanceCell.Options.UseBackColor = True
    Me.ui_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.ui_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_opnome.Caption = "Operatore"
    Me.ui_opnome.Enabled = True
    Me.ui_opnome.FieldName = "ui_opnome"
    Me.ui_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_opnome.Name = "ui_opnome"
    Me.ui_opnome.NTSRepositoryComboBox = Nothing
    Me.ui_opnome.NTSRepositoryItemCheck = Nothing
    Me.ui_opnome.NTSRepositoryItemMemo = Nothing
    Me.ui_opnome.NTSRepositoryItemText = Nothing
    Me.ui_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_opnome.OptionsFilter.AllowFilter = False
    Me.ui_opnome.Visible = True
    Me.ui_opnome.VisibleIndex = 4
    Me.ui_opnome.Width = 70
    '
    'ui_codling
    '
    Me.ui_codling.AppearanceCell.Options.UseBackColor = True
    Me.ui_codling.AppearanceCell.Options.UseTextOptions = True
    Me.ui_codling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_codling.Caption = "Cod. lingua"
    Me.ui_codling.Enabled = True
    Me.ui_codling.FieldName = "ui_codling"
    Me.ui_codling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_codling.Name = "ui_codling"
    Me.ui_codling.NTSRepositoryComboBox = Nothing
    Me.ui_codling.NTSRepositoryItemCheck = Nothing
    Me.ui_codling.NTSRepositoryItemMemo = Nothing
    Me.ui_codling.NTSRepositoryItemText = Nothing
    Me.ui_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_codling.OptionsFilter.AllowFilter = False
    Me.ui_codling.Visible = True
    Me.ui_codling.VisibleIndex = 5
    Me.ui_codling.Width = 70
    '
    'ui_nomprop
    '
    Me.ui_nomprop.AppearanceCell.Options.UseBackColor = True
    Me.ui_nomprop.AppearanceCell.Options.UseTextOptions = True
    Me.ui_nomprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_nomprop.Caption = "Proprietà"
    Me.ui_nomprop.Enabled = True
    Me.ui_nomprop.FieldName = "ui_nomprop"
    Me.ui_nomprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_nomprop.Name = "ui_nomprop"
    Me.ui_nomprop.NTSRepositoryComboBox = Nothing
    Me.ui_nomprop.NTSRepositoryItemCheck = Nothing
    Me.ui_nomprop.NTSRepositoryItemMemo = Nothing
    Me.ui_nomprop.NTSRepositoryItemText = Nothing
    Me.ui_nomprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_nomprop.OptionsFilter.AllowFilter = False
    Me.ui_nomprop.Visible = True
    Me.ui_nomprop.VisibleIndex = 6
    Me.ui_nomprop.Width = 70
    '
    'ui_valprop
    '
    Me.ui_valprop.AppearanceCell.Options.UseBackColor = True
    Me.ui_valprop.AppearanceCell.Options.UseTextOptions = True
    Me.ui_valprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_valprop.Caption = "Valore"
    Me.ui_valprop.Enabled = True
    Me.ui_valprop.FieldName = "ui_valprop"
    Me.ui_valprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_valprop.Name = "ui_valprop"
    Me.ui_valprop.NTSRepositoryComboBox = Nothing
    Me.ui_valprop.NTSRepositoryItemCheck = Nothing
    Me.ui_valprop.NTSRepositoryItemMemo = Nothing
    Me.ui_valprop.NTSRepositoryItemText = Nothing
    Me.ui_valprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_valprop.OptionsFilter.AllowFilter = False
    Me.ui_valprop.Visible = True
    Me.ui_valprop.VisibleIndex = 7
    Me.ui_valprop.Width = 70
    '
    'ui_usascript
    '
    Me.ui_usascript.AppearanceCell.Options.UseBackColor = True
    Me.ui_usascript.AppearanceCell.Options.UseTextOptions = True
    Me.ui_usascript.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_usascript.Caption = "Usa Script"
    Me.ui_usascript.Enabled = True
    Me.ui_usascript.FieldName = "ui_usascript"
    Me.ui_usascript.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_usascript.Name = "ui_usascript"
    Me.ui_usascript.NTSRepositoryComboBox = Nothing
    Me.ui_usascript.NTSRepositoryItemCheck = Nothing
    Me.ui_usascript.NTSRepositoryItemMemo = Nothing
    Me.ui_usascript.NTSRepositoryItemText = Nothing
    Me.ui_usascript.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_usascript.OptionsFilter.AllowFilter = False
    Me.ui_usascript.Visible = True
    Me.ui_usascript.VisibleIndex = 8
    Me.ui_usascript.Width = 70
    '
    'ui_script
    '
    Me.ui_script.AppearanceCell.Options.UseBackColor = True
    Me.ui_script.AppearanceCell.Options.UseTextOptions = True
    Me.ui_script.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_script.Caption = "Testo script"
    Me.ui_script.Enabled = True
    Me.ui_script.FieldName = "ui_script"
    Me.ui_script.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_script.Name = "ui_script"
    Me.ui_script.NTSRepositoryComboBox = Nothing
    Me.ui_script.NTSRepositoryItemCheck = Nothing
    Me.ui_script.NTSRepositoryItemMemo = Nothing
    Me.ui_script.NTSRepositoryItemText = Nothing
    Me.ui_script.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_script.OptionsFilter.AllowFilter = False
    Me.ui_script.Visible = True
    Me.ui_script.VisibleIndex = 9
    Me.ui_script.Width = 70
    '
    'ui_parent
    '
    Me.ui_parent.AppearanceCell.Options.UseBackColor = True
    Me.ui_parent.AppearanceCell.Options.UseTextOptions = True
    Me.ui_parent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_parent.Caption = "Parent"
    Me.ui_parent.Enabled = True
    Me.ui_parent.FieldName = "ui_parent"
    Me.ui_parent.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_parent.Name = "ui_parent"
    Me.ui_parent.NTSRepositoryComboBox = Nothing
    Me.ui_parent.NTSRepositoryItemCheck = Nothing
    Me.ui_parent.NTSRepositoryItemMemo = Nothing
    Me.ui_parent.NTSRepositoryItemText = Nothing
    Me.ui_parent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_parent.OptionsFilter.AllowFilter = False
    Me.ui_parent.Visible = True
    Me.ui_parent.VisibleIndex = 10
    Me.ui_parent.Width = 70
    '
    'pnSx
    '
    Me.pnSx.AllowDrop = True
    Me.pnSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSx.Appearance.Options.UseBackColor = True
    Me.pnSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSx.Controls.Add(Me.trUicf)
    Me.pnSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSx.Location = New System.Drawing.Point(0, 26)
    Me.pnSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSx.Name = "pnSx"
    Me.pnSx.NTSActiveTrasparency = True
    Me.pnSx.Size = New System.Drawing.Size(206, 416)
    Me.pnSx.TabIndex = 6
    Me.pnSx.Text = "NtsPanel1"
    '
    'trUicf
    '
    Me.trUicf.Dock = System.Windows.Forms.DockStyle.Fill
    Me.trUicf.Location = New System.Drawing.Point(0, 0)
    Me.trUicf.Name = "trUicf"
    TreeNode2.Name = "Business"
    TreeNode2.Text = "Business"
    Me.trUicf.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
    Me.trUicf.Size = New System.Drawing.Size(206, 416)
    Me.trUicf.TabIndex = 0
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grUicf)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(206, 61)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(442, 381)
    Me.pnGrid.TabIndex = 8
    Me.pnGrid.Text = "NtsPanel2"
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cbVisualizza)
    Me.pnTop.Controls.Add(Me.lbVisualizza)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(206, 26)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(442, 35)
    Me.pnTop.TabIndex = 9
    Me.pnTop.Text = "NtsPanel1"
    '
    'cbVisualizza
    '
    Me.cbVisualizza.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbVisualizza.DataSource = Nothing
    Me.cbVisualizza.DisplayMember = ""
    Me.cbVisualizza.Location = New System.Drawing.Point(64, 6)
    Me.cbVisualizza.Name = "cbVisualizza"
    Me.cbVisualizza.NTSDbField = ""
    Me.cbVisualizza.Properties.AutoHeight = False
    Me.cbVisualizza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbVisualizza.Properties.DropDownRows = 30
    Me.cbVisualizza.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbVisualizza.SelectedValue = ""
    Me.cbVisualizza.Size = New System.Drawing.Size(305, 20)
    Me.cbVisualizza.TabIndex = 1
    Me.cbVisualizza.ValueMember = ""
    '
    'lbVisualizza
    '
    Me.lbVisualizza.AutoSize = True
    Me.lbVisualizza.BackColor = System.Drawing.Color.Transparent
    Me.lbVisualizza.Location = New System.Drawing.Point(6, 9)
    Me.lbVisualizza.Name = "lbVisualizza"
    Me.lbVisualizza.NTSDbField = ""
    Me.lbVisualizza.Size = New System.Drawing.Size(52, 13)
    Me.lbVisualizza.TabIndex = 0
    Me.lbVisualizza.Text = "Visualizza"
    Me.lbVisualizza.Tooltip = ""
    Me.lbVisualizza.UseMnemonic = False
    '
    'tlbTrasferisciConf
    '
    Me.tlbTrasferisciConf.Caption = "Trasferisci configurazione da std (BN) a pers. (BO)"
    Me.tlbTrasferisciConf.Id = 27
    Me.tlbTrasferisciConf.Name = "tlbTrasferisciConf"
    Me.tlbTrasferisciConf.Visible = True
    '
    'FRM__UICF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnSx)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__UICF"
    Me.NTSLastControlFocussed = Me.grUicf
    Me.Text = "CONFIGURAZIONE USER INTERFACE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grUicf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvUicf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSx.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.cbVisualizza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__UICF", "BE__UICF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128342559569758000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleUicf = CType(oTmp, CLE__UICF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__UICF", strRemoteServer, strRemotePort)
    AddHandler oCleUicf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleUicf.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvUicf.NTSSetParam(oMenu, "CONFIGURAZIONE USER INTERFACE")
      ui_db.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559567106000, "Database"), 25, True)
      ui_ditta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559567262000, "Ditta"), 12, True)
      ui_tipodoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559567730000, "Tipo documento"), 3, False)
      ui_ruolo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559567886000, "Ruolo operat."), 20, True)
      ui_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559568042000, "Operatore"), 20, True)
      ui_codling.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128342559568198000, "Cod. lingua"), "0", 4, 0, 9999)
      ui_nomprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559568978000, "Proprietà"), 20, True)
      ui_valprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559569134000, "Valore"), 0, True)
      ui_usascript.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128342559569290000, "Usa Script"), "S", "N")
      ui_script.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128342559569446000, "Testo script"), 0, True)
      ui_parent.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128927215885569195, "Parent"), 255, True)
      cbVisualizza.NTSSetParam(oApp.Tr(Me, 128342559569602000, "Visualizza"))
      grvUicf.NTSAllowInsert = False

      ui_db.NTSSetParamZoom("ZOOMAZIENDE")
      ui_ditta.NTSSetParamZoom("ZOOMTABANAZ")
      ui_ruolo.NTSSetParamZoom("ZOOMRUOLI")
      ui_opnome.NTSSetParamZoom("ZOOMOPERAT")
      ui_codling.NTSSetParamZoom("ZOOMTABLINGP")
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
  Public Overridable Sub FRM__UICF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()


      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(Short))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {0, "Tutto"})
      dttTipo.Rows.Add(New Object() {1, "Controlli aggiunti e posizionamento in Form"})
      dttTipo.Rows.Add(New Object() {2, "Traduzioni in lingua"})
      dttTipo.Rows.Add(New Object() {3, "Text, ErrorText, Bold, Out-not-equal"})
      dttTipo.Rows.Add(New Object() {4, "Visible, Enable"})
      dttTipo.Rows.Add(New Object() {5, "Default, Checked"})
      dttTipo.AcceptChanges()
      cbVisualizza.DataSource = dttTipo
      cbVisualizza.ValueMember = "cod"
      cbVisualizza.DisplayMember = "val"

      '-------------------------------------------------
      Try
        ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
        ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeviewsel.gif"))
        trUicf.ImageList = ImageList1
        trUicf.ImageIndex = 0
        trUicf.SelectedImageIndex = 1
      Catch ex As Exception
        'se le immagini non ci sono non do errore
      End Try


      grUicf.DataSource = dcUicf

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128965530351621094, "L'utente non è abilitato all'utilizzo di questro programma non essendo un amministratore"))
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'se chiamato da bn__child
      If Not oCallParams Is Nothing Then
        If oCallParams.strPar1 <> "" Then
          trUicf.SelectedNode = trUicf.Nodes(0)
          For Each nodeT As TreeNode In trUicf.SelectedNode.Nodes
            If nodeT.Name = oCallParams.strPar1 Then
              trUicf.SelectedNode = nodeT
            End If
          Next
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRM__UICF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public overridable Sub FRM__UICF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      oCleUicf.dttTrv.Clear()
      dcUicf.Dispose()
      dsUicf.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbTrova_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTrova.ItemClick
    Dim strTmp As String = ""
    Try
      Me.Cursor = Cursors.WaitCursor
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128344894014192000, "Trova cartella"), "")
      If strTmp.Trim = "" Then Exit Try

      For Each nodeT As TreeNode In trUicf.SelectedNode.Nodes
        If nodeT.Name = strTmp.ToUpper Then
          trUicf.SelectedNode = nodeT
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      If grUicf.Visible = False Then Return
      grvUicf.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If grUicf.Visible = False Then Return
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If grUicf.Visible = False Then Return
      If Not grvUicf.NTSDeleteRigaCorrente(dcUicf, True) Then Return
      oCleUicf.Salva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grUicf.Visible = False Then Return
      If Not grvUicf.NTSRipristinaRigaCorrenteBefore(dcUicf, True) Then Return
      oCleUicf.Ripristina(dcUicf.Position, dcUicf.Filter)
      grvUicf.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbCancellaCartella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancellaCartella.ItemClick
    Dim strPathCor As String
    Dim strChild As String = ""
    Dim strForm As String = ""
    Dim strCtrl As String = ""
    Dim strGridCol As String = ""
    Dim strCmbItem As String = ""
    Dim nodTmp As TreeNode
    Dim strTmp As String = ""
    Dim strT() As String = Nothing

    Try
      nodTmp = trUicf.SelectedNode
      strPathCor = nodTmp.FullPath

      If nodTmp.Name.ToString.ToLower = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128927215949803365, "Non è possibile eliminare la cartella Root."))
        Exit Sub
      End If

      strTmp = trUicf.SelectedNode.Text
      If Not trUicf.SelectedNode.Parent Is Nothing Then
        strTmp = trUicf.SelectedNode.Parent.Text & ";" & strTmp
        If Not trUicf.SelectedNode.Parent.Parent Is Nothing Then
          strTmp = trUicf.SelectedNode.Parent.Parent.Text & ";" & strTmp
          If Not trUicf.SelectedNode.Parent.Parent.Parent Is Nothing Then
            strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Text & ";" & strTmp
            If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent Is Nothing Then
              strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Text & ";" & strTmp
              If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent Is Nothing Then strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text & ";" & strTmp
            End If
          End If
        End If
      End If
      strT = strTmp.Split(";"c)
      If strT.Length > 1 Then strChild = strT(1)
      If strT.Length > 2 Then strForm = strT(2)
      If strT.Length > 3 Then strCtrl = strT(3)
      If strT.Length > 4 Then strGridCol = strT(4)
      If strT.Length > 5 Then strCmbItem = strT(5)

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128927216004282300, "Confermi l'eliminazione della cartella " & _
                                      "su cui si è posizionati e tutte le sottocartelle?" & _
                                      vbCrLf & "(|" & strTmp & "|)" & vbCrLf & _
                                      "(verranno cancellate tutte le impostazioni, " & _
                                      "indipendentemente dal filtro di visualizzazione applicato)")) = Windows.Forms.DialogResult.No Then Return

      'cancello la cartella e tutte le sottostanti
      Me.Cursor = Cursors.WaitCursor
      If oCleUicf.CancellaCartella(strChild, strForm, strCtrl, strGridCol, strCmbItem) Then
        CreaTreeview()

        'Riseleziona il Nodo Child
        trUicf.SelectedNode = trUicf.Nodes(0)
        For Each nodeT As TreeNode In trUicf.SelectedNode.Nodes
          If nodeT.Name = strChild Then
            trUicf.SelectedNode = nodeT

            'Riseleziona Nodo Form
            If strForm <> "" Then
              For Each nodeT2 As TreeNode In trUicf.SelectedNode.Nodes
                If nodeT2.Name = strForm Then
                  trUicf.SelectedNode = nodeT2
                End If
              Next
            End If

          End If
        Next

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbEsporta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsporta.ItemClick
    Dim strPathCor As String
    Dim strChild As String = ""
    Dim strForm As String = ""
    Dim strCtrl As String = ""
    Dim strGridCol As String = ""
    Dim strCmbItem As String = ""
    Dim nodTmp As TreeNode
    Dim strTmp As String = ""
    Dim strT() As String = Nothing

    Try
      nodTmp = trUicf.SelectedNode
      strPathCor = nodTmp.FullPath

      If nodTmp.Name.ToString.ToLower = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129163066828437500, "Non è possibile esportare la cartella Root."))
        Return
      End If

      strTmp = trUicf.SelectedNode.Text
      If Not trUicf.SelectedNode.Parent Is Nothing Then
        strTmp = trUicf.SelectedNode.Parent.Text & ";" & strTmp
        If Not trUicf.SelectedNode.Parent.Parent Is Nothing Then
          strTmp = trUicf.SelectedNode.Parent.Parent.Text & ";" & strTmp
          If Not trUicf.SelectedNode.Parent.Parent.Parent Is Nothing Then
            strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Text & ";" & strTmp
            If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent Is Nothing Then
              strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Text & ";" & strTmp
              If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent Is Nothing Then strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text & ";" & strTmp
            End If
          End If
        End If
      End If
      strT = strTmp.Split(";"c)

      If strT.Length > 4 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128328712953803825, "E' possibile esportare solo le cartelle dipendenti dalla root 'Business' fino al 2 sottolivello (il controllo)"))
        Return
      End If

      If strT.Length > 1 Then strChild = strT(1)
      If strT.Length > 2 Then strForm = strT(2)
      If strT.Length > 3 Then strCtrl = strT(3)
      If strT.Length > 4 Then strGridCol = strT(4)
      If strT.Length > 5 Then strCmbItem = strT(5)

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128328755894719916, "Confermi l'export della cartella " & _
                                      "su cui si è posizionati e tutte le sottocartelle?" & _
                                      vbCrLf & "(|" & strTmp & "|)" & vbCrLf & _
                                      "(verranno esportate tutte le impostazioni, " & _
                                      "indipendentemente dal filtro di visualizzazione applicato." & _
                                      vbCrLf & "non verranno esportate le disposizioni delle colonne della griglia ed il loro layout)")) = Windows.Forms.DialogResult.No Then Return

      'eseguo l'export
      Me.Cursor = Cursors.WaitCursor
      oCleUicf.EsportaCartella(strChild, strForm, strCtrl, strGridCol, strCmbItem, strTmp.Replace(";", "_"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbEsportaAgg_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsportaAgg.ItemClick
    Dim strPathCor As String
    Dim strChild As String = ""
    Dim strForm As String = ""
    Dim strCtrl As String = ""
    Dim strGridCol As String = ""
    Dim strCmbItem As String = ""
    Dim nodTmp As TreeNode
    Dim strTmp As String = ""
    Dim strT() As String = Nothing

    Try
      nodTmp = trUicf.SelectedNode
      strPathCor = nodTmp.FullPath

      If nodTmp.Name.ToString.ToLower = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128407300480690000, "Non è possibile esportare la cartella Root."))
        Return
      End If

      strTmp = trUicf.SelectedNode.Text
      If Not trUicf.SelectedNode.Parent Is Nothing Then
        strTmp = trUicf.SelectedNode.Parent.Text & ";" & strTmp
        If Not trUicf.SelectedNode.Parent.Parent Is Nothing Then
          strTmp = trUicf.SelectedNode.Parent.Parent.Text & ";" & strTmp
          If Not trUicf.SelectedNode.Parent.Parent.Parent Is Nothing Then
            strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Text & ";" & strTmp
            If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent Is Nothing Then
              strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Text & ";" & strTmp
              If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent Is Nothing Then strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text & ";" & strTmp
            End If
          End If
        End If
      End If
      strT = strTmp.Split(";"c)

      If strT.Length > 4 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129162534808758682, "E' possibile esportare solo le cartelle dipendenti dalla root 'Business' fino al 2 sottolivello (il controllo)"))
        Return
      End If

      If strT.Length > 1 Then strChild = strT(1)
      If strT.Length > 2 Then strForm = strT(2)
      If strT.Length > 3 Then strCtrl = strT(3)
      If strT.Length > 4 Then strGridCol = strT(4)
      If strT.Length > 5 Then strCmbItem = strT(5)

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129162534775789932, "Confermi l'export dei componenti aggiunti della cartella " & _
                                      "su cui si è posizionati e tutte le sottocartelle?" & _
                                      vbCrLf & "(|" & strTmp & "|)")) = Windows.Forms.DialogResult.No Then Return

      'eseguo l'export
      Me.Cursor = Cursors.WaitCursor
      oCleUicf.EsportaCartellaAgg(strChild, strForm, strCtrl, strGridCol, strCmbItem, strTmp.Replace(";", "_"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbEsportaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsportaGriglia.ItemClick
    Dim strPathCor As String
    Dim strChild As String = ""
    Dim strForm As String = ""
    Dim strCtrl As String = ""
    Dim strGridCol As String = ""
    Dim strCmbItem As String = ""
    Dim nodTmp As TreeNode
    Dim strTmp As String = ""
    Dim strT() As String = Nothing

    Try
      nodTmp = trUicf.SelectedNode
      strPathCor = nodTmp.FullPath

      If nodTmp.Name.ToString.ToLower = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129411077471874534, "Non è possibile esportare la cartella Root."))
        Return
      End If

      strTmp = trUicf.SelectedNode.Text
      If Not trUicf.SelectedNode.Parent Is Nothing Then
        strTmp = trUicf.SelectedNode.Parent.Text & ";" & strTmp
        If Not trUicf.SelectedNode.Parent.Parent Is Nothing Then
          strTmp = trUicf.SelectedNode.Parent.Parent.Text & ";" & strTmp
          If Not trUicf.SelectedNode.Parent.Parent.Parent Is Nothing Then
            strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Text & ";" & strTmp
            If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent Is Nothing Then
              strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Text & ";" & strTmp
              If Not trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent Is Nothing Then strTmp = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text & ";" & strTmp
            End If
          End If
        End If
      End If
      strT = strTmp.Split(";"c)

      If strT.Length > 4 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129411069603328956, "E' possibile esportare solo le cartelle dipendenti dalla root 'Business' fino al 2 sottolivello (il controllo)"))
        Return
      End If

      If strT.Length > 1 Then strChild = strT(1)
      If strT.Length > 2 Then strForm = strT(2)
      If strT.Length > 3 Then strCtrl = strT(3)
      If strT.Length > 4 Then strGridCol = strT(4)
      If strT.Length > 5 Then strCmbItem = strT(5)

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129411069616764822, "Confermi l'export della personalizzazione di griglia della cartella " & _
                                      "su cui si è posizionati e tutte le sottocartelle?" & _
                                      vbCrLf & "(|" & strTmp & "|)")) = Windows.Forms.DialogResult.No Then Return

      'eseguo l'export
      Me.Cursor = Cursors.WaitCursor
      oCleUicf.EsportaGriglia(strChild, strForm, strCtrl, strGridCol, strCmbItem, strTmp.Replace(";", "_"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImporta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImporta.ItemClick
    Dim OpenFileDialog1 As NTSOpenFileDialog = Nothing
    Try
      OpenFileDialog1 = New NTSOpenFileDialog
      OpenFileDialog1.DefaultExt = "Xml"
      OpenFileDialog1.CheckFileExists = True
      OpenFileDialog1.Filter = "File Xml|*.xml"
      OpenFileDialog1.InitialDirectory = "c:\"
      OpenFileDialog1.oMenu = oMenu
      If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Return

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128407278706694000, "Importare il contenuto del file |" & OpenFileDialog1.FileName & "| " & vbCrLf & _
                                    "(le eventuali impostazioni esistenti verranno cancellate a parità di PROGRAMMA/FORM/CONTROLLO senza distinzione per vincoli/dipendenze)")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor
      If oCleUicf.ImportaFile(OpenFileDialog1.FileName) Then CreaTreeview()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try

  End Sub


  Public Overridable Sub tlbPersDelete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPersDelete.ItemClick
    'cancella tutte le personalizzazioni presenti in business, ovvero:
    'svuota la directory script sul server
    'cancellare nel reg. di business le Business/OPZIONI/CHILD_xxxx
    'cancellare nel reg. di business le Business/OPZIONI/ZOOMxxxx
    'cancella tutti i files BO, BF, BH dalla dir di Business
    'da UICONF cancella i controlli estesi e tutte le proprietà tipo NAME/TEXT/PARENT/OUTNOTEQUAL/POSIZIONE DEI CONTROLLI/.... 
    'eccetto le configurazioni di griglia (ex CTRL+ALT+F2)
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129416309894990235, _
         "ATTENZIONE: questo comando provvederà in modo irreversibile a rimuovere tutte le personalizzazioni" & vbCrLf & _
         "presenti in Business NET, ovvero:" & vbCrLf & _
         "- svuotare la directory SCRIPT" & vbCrLf & _
         "- rimuove dal registro di Business le proprietà di Business/opzioni/CHILD_xxx" & vbCrLf & _
         "- rimuove dal registro di Business le proprietà di Business/opzioni/ZOOMxxx" & vbCrLf & _
         "- cancellare dal configuratore user interface tutto quello che non sia la disposizione delle colonne in griglia" & vbCrLf & vbCrLf & _
         "- rimozione di tutte le voci di menu collegate a quella di primo livello identificata dalla lettere 'H'" & vbCrLf & _
         "NB: per la rimozione delle DLL dei client la procedura andrà lanciata su ogni PC." & vbCrLf & _
         "       Per utilizzare questa funzione occorre essere appena entrati in Business, non bisogna aver avviato" & vbCrLf & _
         "       programmi personalizzati dall'avvio di Business e nessun altro utente deve lavorare in Business." & vbCrLf & _
         "(SI CONSIGLIA DI ESEGUIRE UN BACKUP DEL DATABASE ARCPROC PRIMA DI PROSEGUIRE)" & vbCrLf & vbCrLf & _
         "proseguire?")) = Windows.Forms.DialogResult.No Then Return
      Me.Cursor = Cursors.WaitCursor
      oCleUicf.PersDelete()
      CreaTreeview()
      oApp.MsgBoxInfo(oApp.Tr(Me, 129416480745634766, "Cancellazione terminata."))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbPersImport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPersImport.ItemClick
    'per dll personalizzate, verifico le firme 'ereditate', le confronto con quelle del padre e se nel padre sono presenti
    'più firme per la stessa funzione avviso che la personalizzazion dovrà essere provata, perchè non è detto che la firma
    'ereditata (ad esempio in un BD) sia ancora chiamata da in BE
    Dim strDirIn As String = ""
    Try
      strDirIn = oApp.AscDir & "\PERS"
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129416377057031250, _
         "ATTENZIONE: questo comando provvederà ad importare tutte le personalizzazioni" & vbCrLf & _
         "presenti nella dir |" & strDirIn & "|, ovvero:" & vbCrLf & _
         "- la directory SCRIPT" & vbCrLf & _
         "- nel registro di Business le proprietà Business/opzioni/CHILD_xxx" & vbCrLf & _
         "- nel registro di Business le proprietà Business/opzioni/ZOOMxxx" & vbCrLf & _
         "- nel configuratore user interface tutto quello che non sia la disposizione delle colonne in griglia" & vbCrLf & vbCrLf & _
         "- creazione (se necessario) della voce di menu 'H' e di quelle a lei sottostanti" & vbCrLf & _
         "NB: In Business non devono essere presenti altre personalizzazioni." & vbCrLf & _
         "    Questa procedura serve SOLO per installare la personalizzazione su un PC/SERVER. " & vbCrLf & _
         "    Per distribuire eventuali dll personalizzate sui client utilizzare il sistema classico di aggiornamento di Business" & vbCrLf & _
         "(SI CONSIGLIA DI ESEGUIRE UN BACKUP DEL DATABASE ARCPROC PRIMA DI PROSEGUIRE)" & vbCrLf & vbCrLf & _
         "proseguire?")) = Windows.Forms.DialogResult.No Then Return
      Me.Cursor = Cursors.WaitCursor

      If oCleUicf.PersImport(strDirIn) Then
        CreaTreeview()
        oApp.MsgBoxInfo(oApp.Tr(Me, 129416360965341797, "Importazione terminata."))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbPersExport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPersExport.ItemClick
    Dim strDirOut As String = ""
    Try
      strDirOut = oApp.AscDir & "\PERS"
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129416383070332032, _
         "ATTENZIONE: questo comando provvederà ad esportare tutte le personalizzazioni" & vbCrLf & _
         "presenti in Business NET (ed a copiarle nella dir |" & strDirOut & "|, eventualmente svuotandola) ovvero:" & vbCrLf & _
         "- la directory SCRIPT" & vbCrLf & _
         "- dal registro di Business le proprietà Business/opzioni/CHILD_xxx" & vbCrLf & _
         "- dal registro di Business le proprietà Business/opzioni/ZOOMxxx" & vbCrLf & _
         "- dal configuratore user interface tutto quello che non sia la disposizione delle colonne in griglia" & vbCrLf & _
         "- voce di menu collegate a quella di primo livello identificata dalla lettere 'H'" & vbCrLf & vbCrLf & _
         "proseguire?")) = Windows.Forms.DialogResult.No Then Return
      Me.Cursor = Cursors.WaitCursor

      oCleUicf.PersExport(strDirOut)

      oApp.MsgBoxInfo(oApp.Tr(Me, 129416383048486329, "Esportazione terminata. I files sono disponibili in '|" & strDirOut & "|'"))
      System.Diagnostics.Process.Start(strDirOut)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub

  Public Overridable Sub tlbTrasferisciConf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTrasferisciConf.ItemClick
    Dim nodTmp As TreeNode
    Try
      nodTmp = trUicf.SelectedNode

      If nodTmp Is Nothing Then Return

      If oCleUicf.TrasferisciConfigurazione(nodTmp.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130353799583469696, "Trasferimento configurazione completato."))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub grvUicf_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvUicf.NTSBeforeRowUpdate
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvUicf.NTSSalvaRigaCorrente(dcUicf, oCleUicf.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleUicf.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleUicf.Ripristina(dcUicf.Position, dcUicf.Filter)
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

  Public Overridable Sub trUicf_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trUicf.AfterSelect
    Dim strChild As String = ""
    Dim strForm As String = ""
    Dim strCtrl As String = ""
    Dim strGridCol As String = ""
    Dim strCmbItem As String = ""

    Try
      'se ho cliccato su un child carico i nodi figli
      If trUicf.SelectedNode.Nodes.Count = 0 And Not trUicf.SelectedNode.Parent Is Nothing AndAlso trUicf.SelectedNode.Parent.Name = "Business" Then
        CreaTreeview(trUicf.SelectedNode.Name)
      End If

      If trUicf.SelectedNode.Nodes.Count > 0 Then
        grUicf.Visible = False
        Return
      Else
        grUicf.Visible = True
      End If
      strCmbItem = trUicf.SelectedNode.Text
      If trUicf.SelectedNode.Parent Is Nothing Then
        strGridCol = ""
        strCtrl = ""
        strForm = ""
        strChild = ""
      Else
        strGridCol = trUicf.SelectedNode.Parent.Text
        strCtrl = trUicf.SelectedNode.Parent.Parent.Text
        strForm = trUicf.SelectedNode.Parent.Parent.Parent.Text
        strChild = trUicf.SelectedNode.Parent.Parent.Parent.Parent.Text
      End If


      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleUicf.Apri(strChild, strForm, strCtrl, strGridCol, strCmbItem, dsUicf) Then
        Me.Close()
        Return
      End If
      dcUicf.DataSource = dsUicf.Tables("UICONF")
      dsUicf.AcceptChanges()

      grUicf.DataSource = dcUicf


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbVisualizza_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVisualizza.SelectedValueChanged
    Try
      If oCleUicf Is Nothing Then Return

      oCleUicf.nTipo = NTSCInt(cbVisualizza.SelectedValue)
      If Not CreaTreeview() Then
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CreaTreeview() As Boolean
    Try
      Return CreaTreeview("")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CreaTreeview(ByVal strChild As String) As Boolean
    Dim strPrevChild As String = ""
    Dim strPrevForm As String = ""
    Dim strPrevCtrl As String = ""
    Dim strPrevGridCol As String = ""
    Dim strPrevCmbItem As String = ""
    Dim strMsg As String = ""

    Dim nodeT As TreeNode = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor

      '----------------
      'per evitare di dare il messaggio di funzioni con nomi doppi
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      '----------------

      If strChild = "" Then
        'devo ricostruire tutto il treeview 
        trUicf.Nodes.Clear()
        grUicf.Visible = False

        nodeT = New TreeNode
        nodeT.Name = "Business"
        nodeT.Text = "Business"
        trUicf.Nodes.Add(nodeT)
      End If

      '-------------------------------------------------
      'ricreo solo dal nodo parent in giù, per migliorare le prestazioni
      'quando cambio il tipo di visualizzazione il nodo padre è sempre la root
      If Not oCleUicf.CreaTreeview(strChild) Then Return False

      For Each dtrT As DataRow In oCleUicf.dttTrv.Rows
        If strChild = "" Then
          'creo il ramo dei childs
          If NTSCStr(dtrT!ui_child).ToUpper <> strPrevChild Then
            nodeT = New TreeNode
            nodeT.Name = NTSCStr(dtrT!ui_child).ToUpper
            nodeT.Text = NTSCStr(dtrT!ui_child).ToUpper
            trUicf.Nodes("Business").Nodes.Add(nodeT)
            strPrevForm = ""
            strPrevCtrl = ""
            strPrevGridCol = ""
            strPrevCmbItem = ""
          End If
          strPrevChild = NTSCStr(dtrT!ui_child).ToUpper
        Else
          'creo i rami sotto al child su cui ho cliccato
          strPrevChild = strChild.ToUpper

          If NTSCStr(dtrT!ui_form).ToUpper <> strPrevForm Then
            nodeT = New TreeNode
            nodeT.Name = NTSCStr(dtrT!ui_form).ToUpper
            nodeT.Text = NTSCStr(dtrT!ui_form).ToUpper
            trUicf.Nodes("Business").Nodes(strPrevChild).Nodes.Add(nodeT)
            strPrevCtrl = ""
            strPrevGridCol = ""
            strPrevCmbItem = ""
          End If
          strPrevForm = NTSCStr(dtrT!ui_form).ToUpper

          If NTSCStr(dtrT!ui_ctrlname).ToUpper <> strPrevCtrl Then
            nodeT = New TreeNode
            nodeT.Name = NTSCStr(dtrT!ui_ctrlname).ToUpper
            nodeT.Text = NTSCStr(dtrT!ui_ctrlname).ToUpper
            trUicf.Nodes("Business").Nodes(strPrevChild).Nodes(strPrevForm).Nodes.Add(nodeT)
            strPrevGridCol = ""
            strPrevCmbItem = ""
          End If
          strPrevCtrl = NTSCStr(dtrT!ui_ctrlname).ToUpper

          If NTSCStr(dtrT!ui_gridcol).ToUpper <> strPrevGridCol Then
            nodeT = New TreeNode
            nodeT.Name = NTSCStr(dtrT!ui_gridcol).ToUpper
            nodeT.Text = NTSCStr(dtrT!ui_gridcol).ToUpper
            If strPrevCtrl.Trim <> "" Then    'Busweb con ntsgrid: non ha il ctrlname impostato!!!
              trUicf.Nodes("Business").Nodes(strPrevChild).Nodes(strPrevForm).Nodes(strPrevCtrl).Nodes.Add(nodeT)
            End If
            strPrevCmbItem = ""
          End If
          strPrevGridCol = NTSCStr(dtrT!ui_gridcol).ToUpper

          If NTSCStr(dtrT!ui_comboitem).ToUpper <> strPrevCmbItem Then
            nodeT = New TreeNode
            nodeT.Name = NTSCStr(dtrT!ui_comboitem).ToUpper
            nodeT.Text = NTSCStr(dtrT!ui_comboitem).ToUpper
            trUicf.Nodes("Business").Nodes(strPrevChild).Nodes(strPrevForm).Nodes(strPrevCtrl).Nodes(strPrevGridCol).Nodes.Add(nodeT)
          End If
          strPrevCmbItem = NTSCStr(dtrT!ui_comboitem).ToUpper
        End If
      Next

      If strChild = "" Then
        trUicf.ExpandAll()
      End If

      Return True

    Catch ex As Exception
      strMsg = oApp.Tr(Me, 128927214913883340, "Errore processando il nodo: Child '|" & strPrevChild & "|' Form '|" & strPrevForm & "|' Control '|" & strPrevCtrl & "|'")
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, strMsg, oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function
End Class
