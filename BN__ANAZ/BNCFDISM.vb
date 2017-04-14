Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCFDISM
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDism As DataSet
  Public dcDism As BindingSource = New BindingSource()

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
  Public WithEvents grDism As NTSInformatica.NTSGrid
  Public WithEvents grvDism As NTSInformatica.NTSGridView
  Public WithEvents asm_numesco As NTSInformatica.NTSGridColumn
  Public WithEvents asm_numprog As NTSInformatica.NTSGridColumn
  Public WithEvents asm_studipar As NTSInformatica.NTSGridColumn
  Public WithEvents asm_stuflagg As NTSInformatica.NTSGridColumn
  Public WithEvents asm_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents asm_stucodat As NTSInformatica.NTSGridColumn
  Public WithEvents xx_stucodat As NTSInformatica.NTSGridColumn
  Public WithEvents asm_stufldis As NTSInformatica.NTSGridColumn
  Public WithEvents asm_stucaues As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCFDISM))
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
    Me.grDism = New NTSInformatica.NTSGrid
    Me.grvDism = New NTSInformatica.NTSGridView
    Me.asm_numesco = New NTSInformatica.NTSGridColumn
    Me.asm_numprog = New NTSInformatica.NTSGridColumn
    Me.asm_studipar = New NTSInformatica.NTSGridColumn
    Me.asm_stuflagg = New NTSInformatica.NTSGridColumn
    Me.asm_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.asm_stucodat = New NTSInformatica.NTSGridColumn
    Me.xx_stucodat = New NTSInformatica.NTSGridColumn
    Me.asm_stufldis = New NTSInformatica.NTSGridColumn
    Me.asm_stucaues = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDism, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDism, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
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
    'grDism
    '
    Me.grDism.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDism.EmbeddedNavigator.Name = ""
    Me.grDism.Location = New System.Drawing.Point(0, 26)
    Me.grDism.MainView = Me.grvDism
    Me.grDism.Name = "grDism"
    Me.grDism.Size = New System.Drawing.Size(648, 416)
    Me.grDism.TabIndex = 5
    Me.grDism.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDism})
    '
    'grvDism
    '
    Me.grvDism.ActiveFilterEnabled = False
    Me.grvDism.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.asm_numesco, Me.asm_numprog, Me.asm_studipar, Me.asm_stuflagg, Me.asm_coddest, Me.xx_coddest, Me.asm_stucodat, Me.xx_stucodat, Me.asm_stufldis, Me.asm_stucaues})
    Me.grvDism.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDism.Enabled = True
    Me.grvDism.GridControl = Me.grDism
    Me.grvDism.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDism.Name = "grvDism"
    Me.grvDism.NTSAllowDelete = True
    Me.grvDism.NTSAllowInsert = True
    Me.grvDism.NTSAllowUpdate = True
    Me.grvDism.NTSMenuContext = Nothing
    Me.grvDism.OptionsCustomization.AllowRowSizing = True
    Me.grvDism.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDism.OptionsNavigation.UseTabKey = False
    Me.grvDism.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDism.OptionsView.ColumnAutoWidth = False
    Me.grvDism.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDism.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDism.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDism.OptionsView.ShowGroupPanel = False
    Me.grvDism.RowHeight = 16
    '
    'asm_numesco
    '
    Me.asm_numesco.AppearanceCell.Options.UseBackColor = True
    Me.asm_numesco.AppearanceCell.Options.UseTextOptions = True
    Me.asm_numesco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_numesco.Caption = "Esercizio"
    Me.asm_numesco.Enabled = True
    Me.asm_numesco.FieldName = "asm_numesco"
    Me.asm_numesco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_numesco.Name = "asm_numesco"
    Me.asm_numesco.NTSRepositoryComboBox = Nothing
    Me.asm_numesco.NTSRepositoryItemCheck = Nothing
    Me.asm_numesco.NTSRepositoryItemText = Nothing
    Me.asm_numesco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_numesco.OptionsFilter.AllowFilter = False
    Me.asm_numesco.Visible = True
    Me.asm_numesco.VisibleIndex = 0
    Me.asm_numesco.Width = 70
    '
    'asm_numprog
    '
    Me.asm_numprog.AppearanceCell.Options.UseBackColor = True
    Me.asm_numprog.AppearanceCell.Options.UseTextOptions = True
    Me.asm_numprog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_numprog.Caption = "Progressivo"
    Me.asm_numprog.Enabled = True
    Me.asm_numprog.FieldName = "asm_numprog"
    Me.asm_numprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_numprog.Name = "asm_numprog"
    Me.asm_numprog.NTSRepositoryComboBox = Nothing
    Me.asm_numprog.NTSRepositoryItemCheck = Nothing
    Me.asm_numprog.NTSRepositoryItemText = Nothing
    Me.asm_numprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_numprog.OptionsFilter.AllowFilter = False
    Me.asm_numprog.Visible = True
    Me.asm_numprog.VisibleIndex = 1
    Me.asm_numprog.Width = 70
    '
    'asm_studipar
    '
    Me.asm_studipar.AppearanceCell.Options.UseBackColor = True
    Me.asm_studipar.AppearanceCell.Options.UseTextOptions = True
    Me.asm_studipar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_studipar.Caption = "Elenco"
    Me.asm_studipar.Enabled = True
    Me.asm_studipar.FieldName = "asm_studipar"
    Me.asm_studipar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_studipar.Name = "asm_studipar"
    Me.asm_studipar.NTSRepositoryComboBox = Nothing
    Me.asm_studipar.NTSRepositoryItemCheck = Nothing
    Me.asm_studipar.NTSRepositoryItemText = Nothing
    Me.asm_studipar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_studipar.OptionsFilter.AllowFilter = False
    Me.asm_studipar.Visible = True
    Me.asm_studipar.VisibleIndex = 2
    Me.asm_studipar.Width = 70
    '
    'asm_stuflagg
    '
    Me.asm_stuflagg.AppearanceCell.Options.UseBackColor = True
    Me.asm_stuflagg.AppearanceCell.Options.UseTextOptions = True
    Me.asm_stuflagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_stuflagg.Caption = "Aggio"
    Me.asm_stuflagg.Enabled = True
    Me.asm_stuflagg.FieldName = "asm_stuflagg"
    Me.asm_stuflagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_stuflagg.Name = "asm_stuflagg"
    Me.asm_stuflagg.NTSRepositoryComboBox = Nothing
    Me.asm_stuflagg.NTSRepositoryItemCheck = Nothing
    Me.asm_stuflagg.NTSRepositoryItemText = Nothing
    Me.asm_stuflagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_stuflagg.OptionsFilter.AllowFilter = False
    Me.asm_stuflagg.Visible = True
    Me.asm_stuflagg.VisibleIndex = 3
    Me.asm_stuflagg.Width = 70
    '
    'asm_coddest
    '
    Me.asm_coddest.AppearanceCell.Options.UseBackColor = True
    Me.asm_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.asm_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_coddest.Caption = "Sede anagrafica"
    Me.asm_coddest.Enabled = True
    Me.asm_coddest.FieldName = "asm_coddest"
    Me.asm_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_coddest.Name = "asm_coddest"
    Me.asm_coddest.NTSRepositoryComboBox = Nothing
    Me.asm_coddest.NTSRepositoryItemCheck = Nothing
    Me.asm_coddest.NTSRepositoryItemText = Nothing
    Me.asm_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_coddest.OptionsFilter.AllowFilter = False
    Me.asm_coddest.Visible = True
    Me.asm_coddest.VisibleIndex = 4
    Me.asm_coddest.Width = 70
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. sede anagr."
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 5
    '
    'asm_stucodat
    '
    Me.asm_stucodat.AppearanceCell.Options.UseBackColor = True
    Me.asm_stucodat.AppearanceCell.Options.UseTextOptions = True
    Me.asm_stucodat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_stucodat.Caption = "Cod. attività"
    Me.asm_stucodat.Enabled = True
    Me.asm_stucodat.FieldName = "asm_stucodat"
    Me.asm_stucodat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_stucodat.Name = "asm_stucodat"
    Me.asm_stucodat.NTSRepositoryComboBox = Nothing
    Me.asm_stucodat.NTSRepositoryItemCheck = Nothing
    Me.asm_stucodat.NTSRepositoryItemText = Nothing
    Me.asm_stucodat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_stucodat.OptionsFilter.AllowFilter = False
    Me.asm_stucodat.Visible = True
    Me.asm_stucodat.VisibleIndex = 6
    '
    'xx_stucodat
    '
    Me.xx_stucodat.AppearanceCell.Options.UseBackColor = True
    Me.xx_stucodat.AppearanceCell.Options.UseTextOptions = True
    Me.xx_stucodat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_stucodat.Caption = "Descr. attività"
    Me.xx_stucodat.Enabled = False
    Me.xx_stucodat.FieldName = "xx_stucodat"
    Me.xx_stucodat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_stucodat.Name = "xx_stucodat"
    Me.xx_stucodat.NTSRepositoryComboBox = Nothing
    Me.xx_stucodat.NTSRepositoryItemCheck = Nothing
    Me.xx_stucodat.NTSRepositoryItemText = Nothing
    Me.xx_stucodat.OptionsColumn.AllowEdit = False
    Me.xx_stucodat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_stucodat.OptionsColumn.ReadOnly = True
    Me.xx_stucodat.OptionsFilter.AllowFilter = False
    Me.xx_stucodat.Visible = True
    Me.xx_stucodat.VisibleIndex = 7
    '
    'asm_stufldis
    '
    Me.asm_stufldis.AppearanceCell.Options.UseBackColor = True
    Me.asm_stufldis.AppearanceCell.Options.UseTextOptions = True
    Me.asm_stufldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_stufldis.Caption = "Annotaz. distinta"
    Me.asm_stufldis.Enabled = True
    Me.asm_stufldis.FieldName = "asm_stufldis"
    Me.asm_stufldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_stufldis.Name = "asm_stufldis"
    Me.asm_stufldis.NTSRepositoryComboBox = Nothing
    Me.asm_stufldis.NTSRepositoryItemCheck = Nothing
    Me.asm_stufldis.NTSRepositoryItemText = Nothing
    Me.asm_stufldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_stufldis.OptionsFilter.AllowFilter = False
    Me.asm_stufldis.Visible = True
    Me.asm_stufldis.VisibleIndex = 8
    '
    'asm_stucaues
    '
    Me.asm_stucaues.AppearanceCell.Options.UseBackColor = True
    Me.asm_stucaues.AppearanceCell.Options.UseTextOptions = True
    Me.asm_stucaues.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asm_stucaues.Caption = "Esclusione"
    Me.asm_stucaues.Enabled = True
    Me.asm_stucaues.FieldName = "asm_stucaues"
    Me.asm_stucaues.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asm_stucaues.Name = "asm_stucaues"
    Me.asm_stucaues.NTSRepositoryComboBox = Nothing
    Me.asm_stucaues.NTSRepositoryItemCheck = Nothing
    Me.asm_stucaues.NTSRepositoryItemText = Nothing
    Me.asm_stucaues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asm_stucaues.OptionsFilter.AllowFilter = False
    Me.asm_stucaues.Visible = True
    Me.asm_stucaues.VisibleIndex = 9
    '
    'FRMCFDISM
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDism)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMCFDISM"
    Me.NTSLastControlFocussed = Me.grDism
    Me.Text = "STUDI - DETTAGLIO RIGHE QUADRO M"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDism, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDism, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity
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

      Dim dttTipo As New DataTable()

      dttTipo.Columns.Add("cod", GetType(Short))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {0, "Soggetta"})
      dttTipo.Rows.Add(New Object() {1, "Att. corso periodo imp."})
      dttTipo.Rows.Add(New Object() {2, "Per. non norm. svolg. attività"})
      dttTipo.Rows.Add(New Object() {3, "Periodo imposta no 12 mesi"})
      dttTipo.Rows.Add(New Object() {4, "Altre cause"})
      dttTipo.AcceptChanges()

      grvDism.NTSSetParam(oMenu, "STUDI - DETTAGLIO RIGHE QUADRO M")
      asm_numesco.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128644966336875000, "Esercizio"), tabesco)
      asm_numprog.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128647539192343750, "Progressivo"), "0", 4, 0, 9999)
      asm_studipar.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128647539192500000, "Elenco"), "S", "N")
      asm_stuflagg.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128647539192656250, "Aggio"), "S", "N")
      asm_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128647539192812500, "Sede anagrafica"), tabdestdiv)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128647539192968750, "Descr. sede anagr."), 0, True)
      asm_stucodat.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128647539193125000, "Cod. attività"), tabatec, True)
      xx_stucodat.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128647539193281250, "Descr. attività"), 0, True)
      asm_stufldis.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128647560714375000, "Annotazione distinta"), "S", "N")
      asm_stucaues.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128647560724843750, "Aggio"), dttTipo, "val", "cod")

      asm_numesco.NTSForzaVisZoom = False

      asm_numesco.NTSSetRichiesto()
      asm_numprog.NTSSetRichiesto()

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
  Public Overridable Sub FRMCFDISM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dsDism = oCleAnaz.dsShared
      dcDism.DataSource = dsDism.Tables("ANADITASM")
      dsDism.Tables("ANADITASM").AcceptChanges()

      grDism.DataSource = dcDism

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDism_NTSFocusedRowChanged(grvDism, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMCFDISM_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMCFDISM_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDism.Dispose()
      dsDism.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDism.NTSNuovo()

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
      If Not grvDism.NTSDeleteRigaCorrente(dcDism, True) Then Return
      oCleAnaz.DismSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDism.NTSRipristinaRigaCorrenteBefore(dcDism, True) Then Return
      oCleAnaz.DismRipristina(dcDism.Position, dcDism.Filter)
      grvDism.NTSRipristinaRigaCorrenteAfter()
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
      Dim strTmp As String = ""
      Dim i As Integer = 0
      Dim ds As New DataSet

      If grvDism.FocusedColumn.Name = "asm_coddest" Then
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
        NTSZOOM.strIn = NTSCStr(grvDism.EditingValue)
        oParam.lContoCF = 1   'passo il conto cliente/fornitore solo per dire che non è zoom su anazul
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvDism.EditingValue) Then grvDism.SetFocusedValue(NTSZOOM.strIn)
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

  Public Overridable Sub grvDism_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDism.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDism_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDism.NTSFocusedRowChanged
    Try
      If NTSCInt(grvDism.GetFocusedRowCellValue("asm_numesco")) <> 0 Then
        asm_numesco.Enabled = False
        asm_numprog.Enabled = False
      Else
        GctlSetVisEnab(asm_numesco, False)
        GctlSetVisEnab(asm_numprog, False)
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
      dRes = grvDism.NTSSalvaRigaCorrente(dcDism, oCleAnaz.DismRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DismSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DismRipristina(dcDism.Position, dcDism.Filter)
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
  Public Overridable Sub asm_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles asm_coddest.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmdesg As FRM__DESD = Nothing
    Try
      frmdesg = CType(NTSNewFormModal("FRM__DESD"), FRM__DESD)

      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If IsNumeric(dsDism.Tables("ANADITASM").Rows(dcDism.Position)!tb_ucoddest) Then
          nCodDestTmp = NTSCInt(dsDism.Tables("ANADITASM").Rows(dcDism.Position)!tb_ucoddest)
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

End Class
