Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__BANC
  Public oCleclie As CLE__CLIE
  Public oCallParams As CLE__CLDP
  Public dsBanc As DataSet
  Public dcBanc As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__BANC))
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
    Me.grBanc = New NTSInformatica.NTSGrid
    Me.grvBanc = New NTSInformatica.NTSGridView
    Me.cba_swift = New NTSInformatica.NTSGridColumn
    Me.cba_abi = New NTSInformatica.NTSGridColumn
    Me.xx_abi = New NTSInformatica.NTSGridColumn
    Me.cba_cab = New NTSInformatica.NTSGridColumn
    Me.xx_cab = New NTSInformatica.NTSGridColumn
    Me.cba_rifriba = New NTSInformatica.NTSGridColumn
    Me.cba_note = New NTSInformatica.NTSGridColumn
    Me.cba_prefiban = New NTSInformatica.NTSGridColumn
    Me.cba_cin = New NTSInformatica.NTSGridColumn
    Me.cba_iban = New NTSInformatica.NTSGridColumn
    Me.cba_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grBanc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvBanc, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'grBanc
    '
    Me.grBanc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grBanc.EmbeddedNavigator.Name = ""
    Me.grBanc.Location = New System.Drawing.Point(0, 26)
    Me.grBanc.MainView = Me.grvBanc
    Me.grBanc.Name = "grBanc"
    Me.grBanc.Size = New System.Drawing.Size(648, 416)
    Me.grBanc.TabIndex = 5
    Me.grBanc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvBanc})
    '
    'grvBanc
    '
    Me.grvBanc.ActiveFilterEnabled = False
    '
    'cba_swift
    '
    Me.cba_swift.AppearanceCell.Options.UseBackColor = True
    Me.cba_swift.AppearanceCell.Options.UseTextOptions = True
    Me.cba_swift.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_swift.Caption = "Codice Bic/Swift"
    Me.cba_swift.Enabled = True
    Me.cba_swift.FieldName = "cba_swift"
    Me.cba_swift.Name = "cba_swift"
    Me.cba_swift.NTSRepositoryComboBox = Nothing
    Me.cba_swift.NTSRepositoryItemCheck = Nothing
    Me.cba_swift.NTSRepositoryItemMemo = Nothing
    Me.cba_swift.NTSRepositoryItemText = Nothing
    Me.cba_swift.Visible = True
    Me.cba_swift.VisibleIndex = 11
    Me.grvBanc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cba_abi, Me.xx_abi, Me.cba_cab, Me.xx_cab, Me.cba_rifriba, Me.cba_note, Me.cba_prefiban, Me.cba_cin, Me.cba_iban, Me.cba_codvalu, Me.xx_codvalu, Me.cba_swift})
    Me.grvBanc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvBanc.Enabled = True
    Me.grvBanc.GridControl = Me.grBanc
    Me.grvBanc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvBanc.MinRowHeight = 14
    Me.grvBanc.Name = "grvBanc"
    Me.grvBanc.NTSAllowDelete = True
    Me.grvBanc.NTSAllowInsert = True
    Me.grvBanc.NTSAllowUpdate = True
    Me.grvBanc.NTSMenuContext = Nothing
    Me.grvBanc.OptionsCustomization.AllowRowSizing = True
    Me.grvBanc.OptionsFilter.AllowFilterEditor = False
    Me.grvBanc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvBanc.OptionsNavigation.UseTabKey = False
    Me.grvBanc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvBanc.OptionsView.ColumnAutoWidth = False
    Me.grvBanc.OptionsView.EnableAppearanceEvenRow = True
    Me.grvBanc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvBanc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvBanc.OptionsView.ShowGroupPanel = False
    Me.grvBanc.RowHeight = 16
    '
    'cba_abi
    '
    Me.cba_abi.AppearanceCell.Options.UseBackColor = True
    Me.cba_abi.AppearanceCell.Options.UseTextOptions = True
    Me.cba_abi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_abi.Caption = "Abi"
    Me.cba_abi.Enabled = True
    Me.cba_abi.FieldName = "cba_abi"
    Me.cba_abi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_abi.Name = "cba_abi"
    Me.cba_abi.NTSRepositoryComboBox = Nothing
    Me.cba_abi.NTSRepositoryItemCheck = Nothing
    Me.cba_abi.NTSRepositoryItemMemo = Nothing
    Me.cba_abi.NTSRepositoryItemText = Nothing
    Me.cba_abi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_abi.OptionsFilter.AllowFilter = False
    Me.cba_abi.Visible = True
    Me.cba_abi.VisibleIndex = 0
    Me.cba_abi.Width = 70
    '
    'xx_abi
    '
    Me.xx_abi.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_abi.AppearanceCell.Options.UseBackColor = True
    Me.xx_abi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_abi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_abi.Caption = "Descr. banca"
    Me.xx_abi.Enabled = False
    Me.xx_abi.FieldName = "xx_abi"
    Me.xx_abi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_abi.Name = "xx_abi"
    Me.xx_abi.NTSRepositoryComboBox = Nothing
    Me.xx_abi.NTSRepositoryItemCheck = Nothing
    Me.xx_abi.NTSRepositoryItemMemo = Nothing
    Me.xx_abi.NTSRepositoryItemText = Nothing
    Me.xx_abi.OptionsColumn.AllowEdit = False
    Me.xx_abi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_abi.OptionsColumn.ReadOnly = True
    Me.xx_abi.OptionsFilter.AllowFilter = False
    Me.xx_abi.Visible = True
    Me.xx_abi.VisibleIndex = 1
    Me.xx_abi.Width = 70
    '
    'cba_cab
    '
    Me.cba_cab.AppearanceCell.Options.UseBackColor = True
    Me.cba_cab.AppearanceCell.Options.UseTextOptions = True
    Me.cba_cab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_cab.Caption = "Cab"
    Me.cba_cab.Enabled = True
    Me.cba_cab.FieldName = "cba_cab"
    Me.cba_cab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_cab.Name = "cba_cab"
    Me.cba_cab.NTSRepositoryComboBox = Nothing
    Me.cba_cab.NTSRepositoryItemCheck = Nothing
    Me.cba_cab.NTSRepositoryItemMemo = Nothing
    Me.cba_cab.NTSRepositoryItemText = Nothing
    Me.cba_cab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_cab.OptionsFilter.AllowFilter = False
    Me.cba_cab.Visible = True
    Me.cba_cab.VisibleIndex = 2
    Me.cba_cab.Width = 70
    '
    'xx_cab
    '
    Me.xx_cab.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_cab.AppearanceCell.Options.UseBackColor = True
    Me.xx_cab.AppearanceCell.Options.UseTextOptions = True
    Me.xx_cab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_cab.Caption = "Descr.  filiale"
    Me.xx_cab.Enabled = False
    Me.xx_cab.FieldName = "xx_cab"
    Me.xx_cab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_cab.Name = "xx_cab"
    Me.xx_cab.NTSRepositoryComboBox = Nothing
    Me.xx_cab.NTSRepositoryItemCheck = Nothing
    Me.xx_cab.NTSRepositoryItemMemo = Nothing
    Me.xx_cab.NTSRepositoryItemText = Nothing
    Me.xx_cab.OptionsColumn.AllowEdit = False
    Me.xx_cab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_cab.OptionsColumn.ReadOnly = True
    Me.xx_cab.OptionsFilter.AllowFilter = False
    Me.xx_cab.Visible = True
    Me.xx_cab.VisibleIndex = 3
    Me.xx_cab.Width = 70
    '
    'cba_rifriba
    '
    Me.cba_rifriba.AppearanceCell.Options.UseBackColor = True
    Me.cba_rifriba.AppearanceCell.Options.UseTextOptions = True
    Me.cba_rifriba.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_rifriba.Caption = "C/C"
    Me.cba_rifriba.Enabled = True
    Me.cba_rifriba.FieldName = "cba_rifriba"
    Me.cba_rifriba.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_rifriba.Name = "cba_rifriba"
    Me.cba_rifriba.NTSRepositoryComboBox = Nothing
    Me.cba_rifriba.NTSRepositoryItemCheck = Nothing
    Me.cba_rifriba.NTSRepositoryItemMemo = Nothing
    Me.cba_rifriba.NTSRepositoryItemText = Nothing
    Me.cba_rifriba.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_rifriba.OptionsFilter.AllowFilter = False
    Me.cba_rifriba.Visible = True
    Me.cba_rifriba.VisibleIndex = 4
    Me.cba_rifriba.Width = 70
    '
    'cba_note
    '
    Me.cba_note.AppearanceCell.Options.UseBackColor = True
    Me.cba_note.AppearanceCell.Options.UseTextOptions = True
    Me.cba_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_note.Caption = "Note"
    Me.cba_note.Enabled = True
    Me.cba_note.FieldName = "cba_note"
    Me.cba_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_note.Name = "cba_note"
    Me.cba_note.NTSRepositoryComboBox = Nothing
    Me.cba_note.NTSRepositoryItemCheck = Nothing
    Me.cba_note.NTSRepositoryItemMemo = Nothing
    Me.cba_note.NTSRepositoryItemText = Nothing
    Me.cba_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_note.OptionsFilter.AllowFilter = False
    Me.cba_note.Visible = True
    Me.cba_note.VisibleIndex = 5
    Me.cba_note.Width = 70
    '
    'cba_prefiban
    '
    Me.cba_prefiban.AppearanceCell.Options.UseBackColor = True
    Me.cba_prefiban.AppearanceCell.Options.UseTextOptions = True
    Me.cba_prefiban.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_prefiban.Caption = "Pref. IBAN italia"
    Me.cba_prefiban.Enabled = True
    Me.cba_prefiban.FieldName = "cba_prefiban"
    Me.cba_prefiban.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_prefiban.Name = "cba_prefiban"
    Me.cba_prefiban.NTSRepositoryComboBox = Nothing
    Me.cba_prefiban.NTSRepositoryItemCheck = Nothing
    Me.cba_prefiban.NTSRepositoryItemMemo = Nothing
    Me.cba_prefiban.NTSRepositoryItemText = Nothing
    Me.cba_prefiban.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_prefiban.OptionsFilter.AllowFilter = False
    Me.cba_prefiban.Visible = True
    Me.cba_prefiban.VisibleIndex = 6
    Me.cba_prefiban.Width = 70
    '
    'cba_cin
    '
    Me.cba_cin.AppearanceCell.Options.UseBackColor = True
    Me.cba_cin.AppearanceCell.Options.UseTextOptions = True
    Me.cba_cin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_cin.Caption = "CIN"
    Me.cba_cin.Enabled = True
    Me.cba_cin.FieldName = "cba_cin"
    Me.cba_cin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_cin.Name = "cba_cin"
    Me.cba_cin.NTSRepositoryComboBox = Nothing
    Me.cba_cin.NTSRepositoryItemCheck = Nothing
    Me.cba_cin.NTSRepositoryItemMemo = Nothing
    Me.cba_cin.NTSRepositoryItemText = Nothing
    Me.cba_cin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_cin.OptionsFilter.AllowFilter = False
    Me.cba_cin.Visible = True
    Me.cba_cin.VisibleIndex = 7
    '
    'cba_iban
    '
    Me.cba_iban.AppearanceCell.Options.UseBackColor = True
    Me.cba_iban.AppearanceCell.Options.UseTextOptions = True
    Me.cba_iban.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_iban.Caption = "IBAN estero"
    Me.cba_iban.Enabled = True
    Me.cba_iban.FieldName = "cba_iban"
    Me.cba_iban.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_iban.Name = "cba_iban"
    Me.cba_iban.NTSRepositoryComboBox = Nothing
    Me.cba_iban.NTSRepositoryItemCheck = Nothing
    Me.cba_iban.NTSRepositoryItemMemo = Nothing
    Me.cba_iban.NTSRepositoryItemText = Nothing
    Me.cba_iban.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_iban.OptionsFilter.AllowFilter = False
    Me.cba_iban.Visible = True
    Me.cba_iban.VisibleIndex = 8
    '
    'cba_codvalu
    '
    Me.cba_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.cba_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.cba_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cba_codvalu.Caption = "Valuta"
    Me.cba_codvalu.Enabled = True
    Me.cba_codvalu.FieldName = "cba_codvalu"
    Me.cba_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cba_codvalu.Name = "cba_codvalu"
    Me.cba_codvalu.NTSRepositoryComboBox = Nothing
    Me.cba_codvalu.NTSRepositoryItemCheck = Nothing
    Me.cba_codvalu.NTSRepositoryItemMemo = Nothing
    Me.cba_codvalu.NTSRepositoryItemText = Nothing
    Me.cba_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cba_codvalu.OptionsFilter.AllowFilter = False
    Me.cba_codvalu.Visible = True
    Me.cba_codvalu.VisibleIndex = 9
    '
    'xx_codvalu
    '
    Me.xx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvalu.Caption = "Descr. valuta"
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
    Me.xx_codvalu.VisibleIndex = 10
    '
    'FRM__BANC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grBanc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__BANC"
    Me.NTSLastControlFocussed = Me.grBanc
    Me.Text = "ALTRE BANCHE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grBanc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvBanc, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleBanc As CLE__CLIE)
    oCleclie = cleBanc
    AddHandler oCleclie.RemoteEvent, AddressOf GestisciEventiEntity
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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvBanc.NTSSetParam(oMenu, "Altre banche")
      cba_abi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387232249038000, "ABI"), tababi)
      cba_cab.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387232249194000, "CAB"), tababicab)
      xx_cab.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249818000, "Descr. banca"), 0, True)
      xx_abi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249974000, "Descr. filiale"), 0, True)
      cba_rifriba.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249350000, "C/C"), 70, False)
      cba_prefiban.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249662000, "Prefisso IBAN italia"), 4, True)
      cba_iban.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387343871454000, "IBAN estero"), 70, True)
      cba_cin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387320580842000, "CIN"), 1, True)
      cba_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387420914426000, "Valuta"), tabvalu)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387421102094000, "Descr. valuta"), 0, True)
      cba_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387420532226000, "Note"), 255)
      cba_swift.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130187049061101724, "Codice BIC/SWIFT"), 14, True)
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
  Public Overridable Sub FRM__BANC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dsBanc = oCleclie.dsShared
      dcBanc.DataSource = dsBanc.Tables("CLIBANC")
      dsBanc.AcceptChanges()

      grBanc.DataSource = dcBanc

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleclie.strTipoConto
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__BANC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__BANC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcBanc.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvBanc.NTSNuovo()

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
      If Not grvBanc.NTSDeleteRigaCorrente(dcBanc, True) Then Return
      oCleclie.BancSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvBanc.NTSRipristinaRigaCorrenteBefore(dcBanc, True) Then Return
      oCleclie.BancRipristina(dcBanc.Position, dcBanc.Filter)
      grvBanc.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If grBanc.Focused And grvBanc.FocusedColumn.Name.ToLower = "cba_cab" Then
        '--------------------------------------------
        'zoom scaden su cba_cab
        NTSZOOM.strIn = NTSCStr(grvBanc.EditingValue)
        oParam.strDescr = NTSCStr(grvBanc.GetRowCellValue(grvBanc.FocusedRowHandle, cba_abi))   'passo il codice abi
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvBanc.EditingValue) And NTSZOOM.strIn <> "" Then grvBanc.SetFocusedValue(NTSZOOM.strIn)

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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvBanc_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvBanc.NTSBeforeRowUpdate
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
      dRes = grvBanc.NTSSalvaRigaCorrente(dcBanc, oCleclie.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleclie.BancSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleclie.BancRipristina(dcBanc.Position, dcBanc.Filter)
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
End Class
