Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVALA

#Region "Variabili"
  Public dsVala As DataSet
  Public oCleArti As CLEMGARTI
  Public oCallParams As CLE__CLDP
  Public dttTipoCombo As New DataTable 'combo per valori varianti
  Public dcVala As BindingSource = New BindingSource
  Private components As System.ComponentModel.IContainer

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
  Public WithEvents pnVala As NTSInformatica.NTSPanel
  Public WithEvents lbCodArt As NTSInformatica.NTSLabel
  Public WithEvents grAtt As NTSInformatica.NTSGrid
  Public WithEvents grvAtt As NTSInformatica.NTSGridView
  Public WithEvents pnValaTop As NTSInformatica.NTSPanel
  Public WithEvents pnValaBottom As NTSInformatica.NTSPanel
  Public WithEvents alv_codart As NTSInformatica.NTSGridColumn
  Public WithEvents alv_ncampo As NTSInformatica.NTSGridColumn
  Public WithEvents alv_index As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valdouble As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valtext As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valdata As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valcombo As NTSInformatica.NTSGridColumn
  Public WithEvents alv_tipcamp As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codtipa As NTSInformatica.NTSGridColumn
  Public WithEvents alv_desval As NTSInformatica.NTSGridColumn
  Public WithEvents alv_descamp As NTSInformatica.NTSGridColumn
  Public WithEvents alv_multi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipovalore As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valvari As NTSInformatica.NTSGridColumn
  Public WithEvents alv_desvvar As NTSInformatica.NTSGridColumn
  Public WithEvents alv_linkpr As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codartl As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codartl As NTSInformatica.NTSGridColumn
  Public WithEvents alv_linkric As NTSInformatica.NTSGridColumn
  Public WithEvents alv_gestling As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codling As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codling As NTSInformatica.NTSGridColumn
  Public WithEvents alv_codvari As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvari As NTSInformatica.NTSGridColumn
  Public WithEvents xx_destipa As NTSInformatica.NTSGridColumn
  Public WithEvents alv_flcamporic As NTSInformatica.NTSGridColumn
  Public WithEvents alv_flurl As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbAggiornaAttrib As NTSInformatica.NTSBarButtonItem
  Public WithEvents cmdValori As NTSInformatica.NTSButton
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVALA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbAggiornaAttrib = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnVala = New NTSInformatica.NTSPanel
    Me.pnValaBottom = New NTSInformatica.NTSPanel
    Me.grAtt = New NTSInformatica.NTSGrid
    Me.grvAtt = New NTSInformatica.NTSGridView
    Me.alv_codart = New NTSInformatica.NTSGridColumn
    Me.alv_ncampo = New NTSInformatica.NTSGridColumn
    Me.alv_index = New NTSInformatica.NTSGridColumn
    Me.alv_valdouble = New NTSInformatica.NTSGridColumn
    Me.alv_valtext = New NTSInformatica.NTSGridColumn
    Me.alv_valdata = New NTSInformatica.NTSGridColumn
    Me.xx_valcombo = New NTSInformatica.NTSGridColumn
    Me.alv_tipcamp = New NTSInformatica.NTSGridColumn
    Me.alv_codtipa = New NTSInformatica.NTSGridColumn
    Me.alv_desval = New NTSInformatica.NTSGridColumn
    Me.alv_descamp = New NTSInformatica.NTSGridColumn
    Me.alv_multi = New NTSInformatica.NTSGridColumn
    Me.xx_tipovalore = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.alv_valvari = New NTSInformatica.NTSGridColumn
    Me.alv_desvvar = New NTSInformatica.NTSGridColumn
    Me.alv_linkpr = New NTSInformatica.NTSGridColumn
    Me.alv_codartl = New NTSInformatica.NTSGridColumn
    Me.xx_codartl = New NTSInformatica.NTSGridColumn
    Me.alv_linkric = New NTSInformatica.NTSGridColumn
    Me.alv_gestling = New NTSInformatica.NTSGridColumn
    Me.alv_codling = New NTSInformatica.NTSGridColumn
    Me.xx_codling = New NTSInformatica.NTSGridColumn
    Me.alv_codvari = New NTSInformatica.NTSGridColumn
    Me.xx_codvari = New NTSInformatica.NTSGridColumn
    Me.xx_destipa = New NTSInformatica.NTSGridColumn
    Me.alv_flcamporic = New NTSInformatica.NTSGridColumn
    Me.alv_flurl = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.pnValaTop = New NTSInformatica.NTSPanel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.lbCodArt = New NTSInformatica.NTSLabel
    Me.cmdValori = New NTSInformatica.NTSButton
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnVala, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnVala.SuspendLayout()
    CType(Me.pnValaBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnValaBottom.SuspendLayout()
    CType(Me.grAtt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAtt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnValaTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnValaTop.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbAggiornaAttrib})
    Me.NtsBarManager1.MaxItemId = 29
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Id = 27
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiornaAttrib)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbAggiornaAttrib
    '
    Me.tlbAggiornaAttrib.Caption = "Aggiorna elenco attributi"
    Me.tlbAggiornaAttrib.Id = 28
    Me.tlbAggiornaAttrib.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbAggiornaAttrib.Name = "tlbAggiornaAttrib"
    Me.tlbAggiornaAttrib.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 17
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 18
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnVala
    '
    Me.pnVala.AllowDrop = True
    Me.pnVala.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnVala.Appearance.Options.UseBackColor = True
    Me.pnVala.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnVala.Controls.Add(Me.pnValaBottom)
    Me.pnVala.Controls.Add(Me.pnValaTop)
    Me.pnVala.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnVala.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnVala.Location = New System.Drawing.Point(0, 30)
    Me.pnVala.Name = "pnVala"
    Me.pnVala.Size = New System.Drawing.Size(588, 357)
    Me.pnVala.TabIndex = 24
    Me.pnVala.Text = "NtsPanel1"
    '
    'pnValaBottom
    '
    Me.pnValaBottom.AllowDrop = True
    Me.pnValaBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnValaBottom.Appearance.Options.UseBackColor = True
    Me.pnValaBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnValaBottom.Controls.Add(Me.grAtt)
    Me.pnValaBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnValaBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnValaBottom.Location = New System.Drawing.Point(0, 47)
    Me.pnValaBottom.Name = "pnValaBottom"
    Me.pnValaBottom.Size = New System.Drawing.Size(588, 310)
    Me.pnValaBottom.TabIndex = 36
    Me.pnValaBottom.Text = "NtsPanel2"
    '
    'grAtt
    '
    Me.grAtt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grAtt.EmbeddedNavigator.Name = ""
    Me.grAtt.Location = New System.Drawing.Point(0, 0)
    Me.grAtt.MainView = Me.grvAtt
    Me.grAtt.Name = "grAtt"
    Me.grAtt.Size = New System.Drawing.Size(588, 310)
    Me.grAtt.TabIndex = 34
    Me.grAtt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAtt})
    '
    'grvAtt
    '
    Me.grvAtt.ActiveFilterEnabled = False
    Me.grvAtt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.alv_codart, Me.alv_ncampo, Me.alv_index, Me.alv_valdouble, Me.alv_valtext, Me.alv_valdata, Me.xx_valcombo, Me.alv_tipcamp, Me.alv_codtipa, Me.alv_desval, Me.alv_descamp, Me.alv_multi, Me.xx_tipovalore, Me.xx_valore, Me.alv_valvari, Me.alv_desvvar, Me.alv_linkpr, Me.alv_codartl, Me.xx_codartl, Me.alv_linkric, Me.alv_gestling, Me.alv_codling, Me.xx_codling, Me.alv_codvari, Me.xx_codvari, Me.xx_destipa, Me.alv_flcamporic, Me.alv_flurl, Me.codditt})
    Me.grvAtt.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAtt.Enabled = True
    Me.grvAtt.GridControl = Me.grAtt
    Me.grvAtt.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAtt.Name = "grvAtt"
    Me.grvAtt.NTSAllowDelete = True
    Me.grvAtt.NTSAllowInsert = True
    Me.grvAtt.NTSAllowUpdate = True
    Me.grvAtt.NTSMenuContext = Nothing
    Me.grvAtt.OptionsCustomization.AllowRowSizing = True
    Me.grvAtt.OptionsFilter.AllowFilterEditor = False
    Me.grvAtt.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAtt.OptionsNavigation.UseTabKey = False
    Me.grvAtt.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAtt.OptionsView.ColumnAutoWidth = False
    Me.grvAtt.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAtt.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAtt.OptionsView.ShowGroupPanel = False
    Me.grvAtt.RowHeight = 14
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
    Me.alv_codart.NTSRepositoryItemMemo = Nothing
    Me.alv_codart.NTSRepositoryItemText = Nothing
    Me.alv_codart.OptionsColumn.AllowEdit = False
    Me.alv_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codart.OptionsColumn.ReadOnly = True
    Me.alv_codart.OptionsFilter.AllowFilter = False
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
    Me.alv_ncampo.NTSRepositoryItemMemo = Nothing
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
    Me.alv_index.Caption = "Indice"
    Me.alv_index.Enabled = False
    Me.alv_index.FieldName = "alv_index"
    Me.alv_index.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_index.Name = "alv_index"
    Me.alv_index.NTSRepositoryComboBox = Nothing
    Me.alv_index.NTSRepositoryItemCheck = Nothing
    Me.alv_index.NTSRepositoryItemMemo = Nothing
    Me.alv_index.NTSRepositoryItemText = Nothing
    Me.alv_index.OptionsColumn.AllowEdit = False
    Me.alv_index.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_index.OptionsColumn.ReadOnly = True
    Me.alv_index.OptionsFilter.AllowFilter = False
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
    Me.alv_valdouble.NTSRepositoryItemMemo = Nothing
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
    Me.alv_valtext.NTSRepositoryItemMemo = Nothing
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
    Me.alv_valdata.NTSRepositoryItemMemo = Nothing
    Me.alv_valdata.NTSRepositoryItemText = Nothing
    Me.alv_valdata.OptionsColumn.AllowEdit = False
    Me.alv_valdata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valdata.OptionsColumn.ReadOnly = True
    Me.alv_valdata.OptionsFilter.AllowFilter = False
    '
    'xx_valcombo
    '
    Me.xx_valcombo.AppearanceCell.Options.UseBackColor = True
    Me.xx_valcombo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valcombo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valcombo.Caption = "Valcombo"
    Me.xx_valcombo.Enabled = True
    Me.xx_valcombo.FieldName = "xx_valcombo"
    Me.xx_valcombo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valcombo.Name = "xx_valcombo"
    Me.xx_valcombo.NTSRepositoryComboBox = Nothing
    Me.xx_valcombo.NTSRepositoryItemCheck = Nothing
    Me.xx_valcombo.NTSRepositoryItemMemo = Nothing
    Me.xx_valcombo.NTSRepositoryItemText = Nothing
    Me.xx_valcombo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valcombo.OptionsFilter.AllowFilter = False
    Me.xx_valcombo.Visible = True
    Me.xx_valcombo.VisibleIndex = 4
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
    Me.alv_tipcamp.NTSRepositoryItemMemo = Nothing
    Me.alv_tipcamp.NTSRepositoryItemText = Nothing
    Me.alv_tipcamp.OptionsColumn.AllowEdit = False
    Me.alv_tipcamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_tipcamp.OptionsColumn.ReadOnly = True
    Me.alv_tipcamp.OptionsFilter.AllowFilter = False
    '
    'alv_codtipa
    '
    Me.alv_codtipa.AppearanceCell.Options.UseBackColor = True
    Me.alv_codtipa.AppearanceCell.Options.UseTextOptions = True
    Me.alv_codtipa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_codtipa.Caption = "Cod. tipo articolo"
    Me.alv_codtipa.Enabled = False
    Me.alv_codtipa.FieldName = "alv_codtipa"
    Me.alv_codtipa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_codtipa.Name = "alv_codtipa"
    Me.alv_codtipa.NTSRepositoryComboBox = Nothing
    Me.alv_codtipa.NTSRepositoryItemCheck = Nothing
    Me.alv_codtipa.NTSRepositoryItemMemo = Nothing
    Me.alv_codtipa.NTSRepositoryItemText = Nothing
    Me.alv_codtipa.OptionsColumn.AllowEdit = False
    Me.alv_codtipa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codtipa.OptionsColumn.ReadOnly = True
    Me.alv_codtipa.OptionsFilter.AllowFilter = False
    '
    'alv_desval
    '
    Me.alv_desval.AppearanceCell.Options.UseBackColor = True
    Me.alv_desval.AppearanceCell.Options.UseTextOptions = True
    Me.alv_desval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_desval.Caption = "Descr. valore combo"
    Me.alv_desval.Enabled = False
    Me.alv_desval.FieldName = "alv_desval"
    Me.alv_desval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_desval.Name = "alv_desval"
    Me.alv_desval.NTSRepositoryComboBox = Nothing
    Me.alv_desval.NTSRepositoryItemCheck = Nothing
    Me.alv_desval.NTSRepositoryItemMemo = Nothing
    Me.alv_desval.NTSRepositoryItemText = Nothing
    Me.alv_desval.OptionsColumn.AllowEdit = False
    Me.alv_desval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_desval.OptionsColumn.ReadOnly = True
    Me.alv_desval.OptionsFilter.AllowFilter = False
    '
    'alv_descamp
    '
    Me.alv_descamp.AppearanceCell.Options.UseBackColor = True
    Me.alv_descamp.AppearanceCell.Options.UseTextOptions = True
    Me.alv_descamp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_descamp.Caption = "Nome attributo"
    Me.alv_descamp.Enabled = False
    Me.alv_descamp.FieldName = "alv_descamp"
    Me.alv_descamp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_descamp.Name = "alv_descamp"
    Me.alv_descamp.NTSRepositoryComboBox = Nothing
    Me.alv_descamp.NTSRepositoryItemCheck = Nothing
    Me.alv_descamp.NTSRepositoryItemMemo = Nothing
    Me.alv_descamp.NTSRepositoryItemText = Nothing
    Me.alv_descamp.OptionsColumn.AllowEdit = False
    Me.alv_descamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_descamp.OptionsColumn.ReadOnly = True
    Me.alv_descamp.OptionsFilter.AllowFilter = False
    Me.alv_descamp.Visible = True
    Me.alv_descamp.VisibleIndex = 0
    '
    'alv_multi
    '
    Me.alv_multi.AppearanceCell.Options.UseBackColor = True
    Me.alv_multi.AppearanceCell.Options.UseTextOptions = True
    Me.alv_multi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_multi.Caption = "Campo multiplo"
    Me.alv_multi.Enabled = False
    Me.alv_multi.FieldName = "alv_multi"
    Me.alv_multi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_multi.Name = "alv_multi"
    Me.alv_multi.NTSRepositoryComboBox = Nothing
    Me.alv_multi.NTSRepositoryItemCheck = Nothing
    Me.alv_multi.NTSRepositoryItemMemo = Nothing
    Me.alv_multi.NTSRepositoryItemText = Nothing
    Me.alv_multi.OptionsColumn.AllowEdit = False
    Me.alv_multi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_multi.OptionsColumn.ReadOnly = True
    Me.alv_multi.OptionsFilter.AllowFilter = False
    Me.alv_multi.Visible = True
    Me.alv_multi.VisibleIndex = 1
    '
    'xx_tipovalore
    '
    Me.xx_tipovalore.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipovalore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipovalore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipovalore.Caption = "Tipo valore"
    Me.xx_tipovalore.Enabled = False
    Me.xx_tipovalore.FieldName = "xx_tipovalore"
    Me.xx_tipovalore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipovalore.Name = "xx_tipovalore"
    Me.xx_tipovalore.NTSRepositoryComboBox = Nothing
    Me.xx_tipovalore.NTSRepositoryItemCheck = Nothing
    Me.xx_tipovalore.NTSRepositoryItemMemo = Nothing
    Me.xx_tipovalore.NTSRepositoryItemText = Nothing
    Me.xx_tipovalore.OptionsColumn.AllowEdit = False
    Me.xx_tipovalore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipovalore.OptionsColumn.ReadOnly = True
    Me.xx_tipovalore.OptionsFilter.AllowFilter = False
    Me.xx_tipovalore.Visible = True
    Me.xx_tipovalore.VisibleIndex = 2
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
    Me.xx_valore.VisibleIndex = 3
    '
    'alv_valvari
    '
    Me.alv_valvari.AppearanceCell.Options.UseBackColor = True
    Me.alv_valvari.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valvari.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valvari.Caption = "Val. variante"
    Me.alv_valvari.Enabled = False
    Me.alv_valvari.FieldName = "alv_valvari"
    Me.alv_valvari.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valvari.Name = "alv_valvari"
    Me.alv_valvari.NTSRepositoryComboBox = Nothing
    Me.alv_valvari.NTSRepositoryItemCheck = Nothing
    Me.alv_valvari.NTSRepositoryItemMemo = Nothing
    Me.alv_valvari.NTSRepositoryItemText = Nothing
    Me.alv_valvari.OptionsColumn.AllowEdit = False
    Me.alv_valvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valvari.OptionsColumn.ReadOnly = True
    Me.alv_valvari.OptionsFilter.AllowFilter = False
    '
    'alv_desvvar
    '
    Me.alv_desvvar.AppearanceCell.Options.UseBackColor = True
    Me.alv_desvvar.AppearanceCell.Options.UseTextOptions = True
    Me.alv_desvvar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_desvvar.Caption = "Descr. valore variante"
    Me.alv_desvvar.Enabled = False
    Me.alv_desvvar.FieldName = "alv_desvvar"
    Me.alv_desvvar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_desvvar.Name = "alv_desvvar"
    Me.alv_desvvar.NTSRepositoryComboBox = Nothing
    Me.alv_desvvar.NTSRepositoryItemCheck = Nothing
    Me.alv_desvvar.NTSRepositoryItemMemo = Nothing
    Me.alv_desvvar.NTSRepositoryItemText = Nothing
    Me.alv_desvvar.OptionsColumn.AllowEdit = False
    Me.alv_desvvar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_desvvar.OptionsColumn.ReadOnly = True
    Me.alv_desvvar.OptionsFilter.AllowFilter = False
    Me.alv_desvvar.Visible = True
    Me.alv_desvvar.VisibleIndex = 5
    '
    'alv_linkpr
    '
    Me.alv_linkpr.AppearanceCell.Options.UseBackColor = True
    Me.alv_linkpr.AppearanceCell.Options.UseTextOptions = True
    Me.alv_linkpr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_linkpr.Caption = "Link prodotto"
    Me.alv_linkpr.Enabled = False
    Me.alv_linkpr.FieldName = "alv_linkpr"
    Me.alv_linkpr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_linkpr.Name = "alv_linkpr"
    Me.alv_linkpr.NTSRepositoryComboBox = Nothing
    Me.alv_linkpr.NTSRepositoryItemCheck = Nothing
    Me.alv_linkpr.NTSRepositoryItemMemo = Nothing
    Me.alv_linkpr.NTSRepositoryItemText = Nothing
    Me.alv_linkpr.OptionsColumn.AllowEdit = False
    Me.alv_linkpr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_linkpr.OptionsColumn.ReadOnly = True
    Me.alv_linkpr.OptionsFilter.AllowFilter = False
    Me.alv_linkpr.Visible = True
    Me.alv_linkpr.VisibleIndex = 6
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
    Me.alv_codartl.NTSRepositoryItemMemo = Nothing
    Me.alv_codartl.NTSRepositoryItemText = Nothing
    Me.alv_codartl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codartl.OptionsFilter.AllowFilter = False
    Me.alv_codartl.Visible = True
    Me.alv_codartl.VisibleIndex = 7
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
    Me.xx_codartl.NTSRepositoryItemMemo = Nothing
    Me.xx_codartl.NTSRepositoryItemText = Nothing
    Me.xx_codartl.OptionsColumn.AllowEdit = False
    Me.xx_codartl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codartl.OptionsColumn.ReadOnly = True
    Me.xx_codartl.OptionsFilter.AllowFilter = False
    Me.xx_codartl.Visible = True
    Me.xx_codartl.VisibleIndex = 8
    '
    'alv_linkric
    '
    Me.alv_linkric.AppearanceCell.Options.UseBackColor = True
    Me.alv_linkric.AppearanceCell.Options.UseTextOptions = True
    Me.alv_linkric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_linkric.Caption = "Link nuova ricerca"
    Me.alv_linkric.Enabled = False
    Me.alv_linkric.FieldName = "alv_linkric"
    Me.alv_linkric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_linkric.Name = "alv_linkric"
    Me.alv_linkric.NTSRepositoryComboBox = Nothing
    Me.alv_linkric.NTSRepositoryItemCheck = Nothing
    Me.alv_linkric.NTSRepositoryItemMemo = Nothing
    Me.alv_linkric.NTSRepositoryItemText = Nothing
    Me.alv_linkric.OptionsColumn.AllowEdit = False
    Me.alv_linkric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_linkric.OptionsColumn.ReadOnly = True
    Me.alv_linkric.OptionsFilter.AllowFilter = False
    Me.alv_linkric.Visible = True
    Me.alv_linkric.VisibleIndex = 9
    '
    'alv_gestling
    '
    Me.alv_gestling.AppearanceCell.Options.UseBackColor = True
    Me.alv_gestling.AppearanceCell.Options.UseTextOptions = True
    Me.alv_gestling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_gestling.Caption = "Gest. lingua"
    Me.alv_gestling.Enabled = False
    Me.alv_gestling.FieldName = "alv_gestling"
    Me.alv_gestling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_gestling.Name = "alv_gestling"
    Me.alv_gestling.NTSRepositoryComboBox = Nothing
    Me.alv_gestling.NTSRepositoryItemCheck = Nothing
    Me.alv_gestling.NTSRepositoryItemMemo = Nothing
    Me.alv_gestling.NTSRepositoryItemText = Nothing
    Me.alv_gestling.OptionsColumn.AllowEdit = False
    Me.alv_gestling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_gestling.OptionsColumn.ReadOnly = True
    Me.alv_gestling.OptionsFilter.AllowFilter = False
    Me.alv_gestling.Visible = True
    Me.alv_gestling.VisibleIndex = 10
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
    Me.alv_codling.NTSRepositoryItemMemo = Nothing
    Me.alv_codling.NTSRepositoryItemText = Nothing
    Me.alv_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codling.OptionsFilter.AllowFilter = False
    Me.alv_codling.Visible = True
    Me.alv_codling.VisibleIndex = 11
    '
    'xx_codling
    '
    Me.xx_codling.AppearanceCell.Options.UseBackColor = True
    Me.xx_codling.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codling.Caption = "Des. lingua"
    Me.xx_codling.Enabled = False
    Me.xx_codling.FieldName = "xx_codling"
    Me.xx_codling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codling.Name = "xx_codling"
    Me.xx_codling.NTSRepositoryComboBox = Nothing
    Me.xx_codling.NTSRepositoryItemCheck = Nothing
    Me.xx_codling.NTSRepositoryItemMemo = Nothing
    Me.xx_codling.NTSRepositoryItemText = Nothing
    Me.xx_codling.OptionsColumn.AllowEdit = False
    Me.xx_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codling.OptionsColumn.ReadOnly = True
    Me.xx_codling.OptionsFilter.AllowFilter = False
    Me.xx_codling.Visible = True
    Me.xx_codling.VisibleIndex = 12
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
    Me.alv_codvari.NTSRepositoryItemMemo = Nothing
    Me.alv_codvari.NTSRepositoryItemText = Nothing
    Me.alv_codvari.OptionsColumn.AllowEdit = False
    Me.alv_codvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_codvari.OptionsColumn.ReadOnly = True
    Me.alv_codvari.OptionsFilter.AllowFilter = False
    Me.alv_codvari.Visible = True
    Me.alv_codvari.VisibleIndex = 13
    '
    'xx_codvari
    '
    Me.xx_codvari.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvari.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvari.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvari.Caption = "Des. variante"
    Me.xx_codvari.Enabled = False
    Me.xx_codvari.FieldName = "xx_codvari"
    Me.xx_codvari.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvari.Name = "xx_codvari"
    Me.xx_codvari.NTSRepositoryComboBox = Nothing
    Me.xx_codvari.NTSRepositoryItemCheck = Nothing
    Me.xx_codvari.NTSRepositoryItemMemo = Nothing
    Me.xx_codvari.NTSRepositoryItemText = Nothing
    Me.xx_codvari.OptionsColumn.AllowEdit = False
    Me.xx_codvari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvari.OptionsColumn.ReadOnly = True
    Me.xx_codvari.OptionsFilter.AllowFilter = False
    Me.xx_codvari.Visible = True
    Me.xx_codvari.VisibleIndex = 14
    '
    'xx_destipa
    '
    Me.xx_destipa.AppearanceCell.Options.UseBackColor = True
    Me.xx_destipa.AppearanceCell.Options.UseTextOptions = True
    Me.xx_destipa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_destipa.Caption = "Des. tipo articolo"
    Me.xx_destipa.Enabled = False
    Me.xx_destipa.FieldName = "xx_destipa"
    Me.xx_destipa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_destipa.Name = "xx_destipa"
    Me.xx_destipa.NTSRepositoryComboBox = Nothing
    Me.xx_destipa.NTSRepositoryItemCheck = Nothing
    Me.xx_destipa.NTSRepositoryItemMemo = Nothing
    Me.xx_destipa.NTSRepositoryItemText = Nothing
    Me.xx_destipa.OptionsColumn.AllowEdit = False
    Me.xx_destipa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_destipa.OptionsColumn.ReadOnly = True
    Me.xx_destipa.OptionsFilter.AllowFilter = False
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
    Me.alv_flcamporic.NTSRepositoryItemMemo = Nothing
    Me.alv_flcamporic.NTSRepositoryItemText = Nothing
    Me.alv_flcamporic.OptionsColumn.AllowEdit = False
    Me.alv_flcamporic.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_flcamporic.OptionsColumn.ReadOnly = True
    Me.alv_flcamporic.OptionsFilter.AllowFilter = False
    Me.alv_flcamporic.Visible = True
    Me.alv_flcamporic.VisibleIndex = 15
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
    Me.alv_flurl.NTSRepositoryItemMemo = Nothing
    Me.alv_flurl.NTSRepositoryItemText = Nothing
    Me.alv_flurl.OptionsColumn.AllowEdit = False
    Me.alv_flurl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_flurl.OptionsColumn.ReadOnly = True
    Me.alv_flurl.OptionsFilter.AllowFilter = False
    Me.alv_flurl.Visible = True
    Me.alv_flurl.VisibleIndex = 16
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
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'pnValaTop
    '
    Me.pnValaTop.AllowDrop = True
    Me.pnValaTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnValaTop.Appearance.Options.UseBackColor = True
    Me.pnValaTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnValaTop.Controls.Add(Me.edCodart)
    Me.pnValaTop.Controls.Add(Me.lbXx_codart)
    Me.pnValaTop.Controls.Add(Me.lbCodArt)
    Me.pnValaTop.Controls.Add(Me.cmdValori)
    Me.pnValaTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnValaTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnValaTop.Location = New System.Drawing.Point(0, 0)
    Me.pnValaTop.Name = "pnValaTop"
    Me.pnValaTop.Size = New System.Drawing.Size(588, 47)
    Me.pnValaTop.TabIndex = 35
    Me.pnValaTop.Text = "NtsPanel1"
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(59, 13)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(155, 20)
    Me.edCodart.TabIndex = 58
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(220, 13)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(245, 20)
    Me.lbXx_codart.TabIndex = 57
    Me.lbXx_codart.Tooltip = ""
    Me.lbXx_codart.UseMnemonic = False
    '
    'lbCodArt
    '
    Me.lbCodArt.AutoSize = True
    Me.lbCodArt.BackColor = System.Drawing.Color.Transparent
    Me.lbCodArt.Location = New System.Drawing.Point(10, 16)
    Me.lbCodArt.Name = "lbCodArt"
    Me.lbCodArt.NTSDbField = ""
    Me.lbCodArt.Size = New System.Drawing.Size(43, 13)
    Me.lbCodArt.TabIndex = 24
    Me.lbCodArt.Text = "Articolo"
    Me.lbCodArt.Tooltip = ""
    Me.lbCodArt.UseMnemonic = False
    '
    'cmdValori
    '
    Me.cmdValori.ImageText = ""
    Me.cmdValori.Location = New System.Drawing.Point(483, 13)
    Me.cmdValori.Name = "cmdValori"
    Me.cmdValori.Size = New System.Drawing.Size(100, 26)
    Me.cmdValori.TabIndex = 33
    Me.cmdValori.Text = "Valori multipli"
    '
    'FRMMGVALA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(588, 387)
    Me.Controls.Add(Me.pnVala)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGVALA"
    Me.NTSLastControlFocussed = Me.grAtt
    Me.Text = "ATTRIBUTI ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnVala, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnVala.ResumeLayout(False)
    CType(Me.pnValaBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnValaBottom.ResumeLayout(False)
    CType(Me.grAtt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAtt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnValaTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnValaTop.ResumeLayout(False)
    Me.pnValaTop.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttMulti As New DataTable()
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
      '-------------------------------------------------

      dttMulti.Columns.Add("cod", GetType(String))
      dttMulti.Columns.Add("val", GetType(String))
      dttMulti.Rows.Add(New Object() {"S", "Attributi articolo"})
      dttMulti.Rows.Add(New Object() {"N", "Attributi articolo"})
      dttMulti.Rows.Add(New Object() {"F", "Valori multipli"})
      dttMulti.AcceptChanges()

      grvAtt.NTSSetParam(oMenu, oApp.Tr(Me, 128581694016022339, "Attributi articolo"))
      alv_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385460614537, "Cod. articolo"), CLN__STD.CodartMaxLen, False)
      alv_ncampo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385535930893, "N. campo"), "0", 4, 0, 9999)
      alv_index.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385560619657, "Indice"), "0", 4, 0, 9999)
      alv_valdouble.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385579370617, "Valdouble"), "#,##0.00", 15)
      alv_valtext.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385593590095, "Valtext"), 255, True)
      alv_valdata.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128788385608122089, "Valdata"), True)
      alv_tipcamp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385634529691, "Tipo campo"), "0", 4, 0, 9999)
      alv_codtipa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385648905427, "Cod. tipo articolo"), "0", 4, 0, 9999)
      alv_desval.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385662812389, "Descr. valore combo"), 30, True)
      alv_descamp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385677656899, "Nome attributo"), 30, True)
      alv_multi.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128788385693438957, "Campo multiplo"), dttMulti, "val", "cod")
      xx_tipovalore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385707189661, "Tipo valore"), 0, True)
      xx_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385720940365, "Valore"), 0, True)
      xx_destipa.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128581805016135093, "Des. tipo articolo"), 0, True)
      alv_valvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385779224599, "Val. variante"), 8, True)
      alv_desvvar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385816726519, "Descr. valore variante"), 60, True)
      alv_linkpr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788385834071157, "Link prodotto"), "S", "N")
      alv_codartl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385924857055, "Cod. articolo (link)"), CLN__STD.CodartMaxLen, True)
      xx_codartl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788385867510369, "Desc. articolo"), 0, True)
      alv_linkric.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788385940639113, "Link nuova ricerca"), "S", "N")
      alv_gestling.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788385956421171, "Gest. lingua"), "S", "N")
      alv_codling.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788385994860639, "Cod. lingua"), "0", 4)
      xx_codling.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386012361535, "Des. lingua"), 0, True)
      alv_codvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386030331205, "Cod. variante"), 6, True)
      xx_codvari.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386046738295, "Des. variante"), 0, True)
      alv_flcamporic.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788386065489255, "Camp/att. di ricerca"), "S", "N")
      alv_flurl.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788386078927443, "Url"), "S", "N")
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386104866271, "codditt"), 12, False)

      edCodart.NTSSetParam(oMenu, oApp.Tr(Me, 128581694018209223, "Codice articolo"), CLN__STD.CodartMaxLen)

      edCodart.Enabled = False

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub InitEntity(ByRef cleArti As CLEMGARTI)
    oCleArti = cleArti
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMMGVALA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      pnVala.Visible = False
      pnVala.Visible = True

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleArti.ValaApri(DittaCorrente, dsVala) Then
        Me.Close()
        Exit Sub
      End If

      grAtt.DataSource = Nothing
      dcVala.DataSource = Nothing
      dcVala.DataSource = dsVala.Tables("VALARTI")
      dsVala.AcceptChanges()

      grAtt.DataSource = dcVala

      edCodart.Text = oCleArti.strValaCodart
      lbXx_codart.Text = oCleArti.strValaDesart

      tlbNuovo.Enabled = False
      tlbCancella.Enabled = False
      grvAtt.NTSAllowInsert = False
      grvAtt.NTSAllowDelete = False

      'Questa parte la faccio qui invece che nell'initcontrols perchè altrimenti non so i dati del documento aperto
      'visto che nei valori varianti posso avere più codici uguali e descrizioni diverse, devo creare una colonna univoca composta da 
      'cast(vt_ncampo  as varchar(5)) + '.' + vt_valore as cod
      'poi al cambio dle combo aggiornerò la colonna corretta (vt_valore) presente solo sul datatable 
      oCleArti.CaricaTipoCombo(NTSCInt(dsVala.Tables("VALARTI").Rows(0)!alv_codtipa), dttTipoCombo)
      dttTipoCombo.AcceptChanges()
      xx_valcombo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128788385619841439, "Valcombo"), dttTipoCombo, "val", "cod")

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGVALA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcVala.Dispose()
      dsVala.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGVALA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

  Public Overridable Sub cmdValori_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValori.Click
    Dim oParam As New CLE__CLDP
    Dim frmVal2 As FRMMGVAL2 = Nothing
    Try
      If Not Salva() Then Return

      If dsVala.Tables("VALARTI").Rows.Count > 0 Then
        If NTSCStr(dsVala.Tables("VALARTI").Rows(dcVala.Position)!alv_multi) <> "F" Then
          Exit Sub
        End If
      Else
        Exit Sub
      End If

      oCleArti.strVal2Campo = NTSCStr(dsVala.Tables("VALARTI").Rows(dcVala.Position)!alv_ncampo)
      oCleArti.strVal2CodLing = NTSCStr(dsVala.Tables("VALARTI").Rows(dcVala.Position)!alv_codling)
      oCleArti.strVal2XxCodLing = NTSCStr(dsVala.Tables("VALARTI").Rows(dcVala.Position)!xx_codling)
      oCleArti.strVal2Tipcamp = NTSCStr(dsVala.Tables("VALARTI").Rows(dcVala.Position)!alv_tipcamp)

      frmVal2 = CType(NTSNewFormModal("FRMMGVAL2"), FRMMGVAL2)
      frmVal2.Init(oMenu, oParam, DittaCorrente)
      frmVal2.InitEntity(oCleArti)
      frmVal2.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmVal2 Is Nothing Then frmVal2.Dispose()
      frmVal2 = Nothing
      grvAtt.Focus()
    End Try
  End Sub

#Region "EventiToolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAtt.NTSNuovo()
    Catch ex As Exception
      Me.Cursor = Cursors.Default
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
      If dsVala.Tables("VALARTI").Rows.Count = 0 Then Return
      If grvAtt.NTSGetCurrentDataRow Is Nothing Then Return
      If Not grvAtt.NTSDeleteRigaCorrente(dcVala, True) Then Return
      oCleArti.ValaSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAtt.NTSRipristinaRigaCorrenteBefore(dcVala, True) Then Return
      oCleArti.ValaRipristina(dcVala.Position, dcVala.Filter)
      grvAtt.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grAtt.Focused And grvAtt.FocusedColumn.Name.ToLower = "alv_codvari" Then
          NTSZOOM.strIn = NTSCStr(grvAtt.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMTABVARI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvAtt.EditingValue) Then grvAtt.SetFocusedValue(NTSZOOM.strIn)
          grAtt.Focus()
          Return
        ElseIf grvAtt.FocusedColumn.Equals(alv_codartl) Then
          If grvAtt.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
          If NTSCStr(grvAtt.NTSGetCurrentDataRow()!alv_linkpr) = "N" Then Exit Sub
          NTSZOOM.strIn = NTSCStr(grvAtt.GetFocusedValue)
          oParam.nMagaz = 0
          oParam.nListino = 0
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvAtt.GetFocusedValue) Then grvAtt.SetFocusedValue(NTSZOOM.strIn)
        ElseIf grvAtt.FocusedColumn.Equals(xx_valore) Then
          If grvAtt.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
          If NTSCStr(grvAtt.NTSGetCurrentDataRow()!alv_codvari) = "" Then Exit Sub
          If NTSCStr(grvAtt.NTSGetCurrentDataRow()!alv_codvari).Trim = "" Then Exit Sub
          NTSZOOM.strIn = NTSCStr(grvAtt.GetFocusedValue)
          oParam.strCodPdc = NTSCStr(grvAtt.NTSGetCurrentDataRow()!alv_codvari)
          NTSZOOM.ZoomStrIn("ZOOMVALVARI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvAtt.GetFocusedValue) Then grvAtt.SetFocusedValue(NTSZOOM.strIn)
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
  Public Overridable Sub tlbAggiornaAttrib_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiornaAttrib.ItemClick
    Try
      If Not oCleArti.AggiornaElencoAttributi(dsVala) Then Return
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAtt.NTSSalvaRigaCorrente(dcVala, oCleArti.ValaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArti.ValaSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArti.ValaRipristina(dcVala.Position, dcVala.Filter)
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

#Region "EventiGriglia"
  Public Overridable Sub grvAtt_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAtt.NTSBeforeRowUpdate
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

  Public Overridable Sub grvAtt_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAtt.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArti Is Nothing Then Return

      dtrT = grvAtt.NTSGetCurrentDataRow

      If NTSCStr(dtrT!alv_linkpr) = "N" Or NTSCStr(dtrT!alv_multi) = "F" Then
        alv_codartl.Enabled = False
        xx_codartl.Enabled = False
      Else
        alv_codartl.Enabled = True
        xx_codartl.Enabled = True
      End If

      If NTSCInt(dtrT!alv_tipcamp) = 20 Then
        xx_valore.Enabled = False
        xx_valcombo.Enabled = True
      Else
        xx_valore.Enabled = True
        xx_valcombo.Enabled = False
      End If

      grvAtt_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvAtt_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvatt.NTSFocusedColumnChanged
    Try
      grvAtt_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grAtt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grAtt.Enter
    Try
      grvAtt_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvAtt_RowColChange()
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Try

      If oCleArti Is Nothing Then Return
      If dttTipoCombo Is Nothing Then Return

      '--------------------------------------
      'compilo il combo delle unità di misura
      'compilo il combo dei valori
      If Not xx_valcombo.ColumnEdit Is Nothing Then
        CType(xx_valcombo.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTipoCombo
        If grvAtt.FocusedColumn.Name = "xx_valcombo" Then
          grvAtt.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          grAtt.Refresh()
          oCleArti.GetArticoCmb(NTSCInt(grvAtt.GetFocusedRowCellValue(alv_ncampo)), NTSCInt(grvAtt.GetFocusedRowCellValue(alv_codtipa)), dttTmp)
          dttTmp.AcceptChanges()
          CType(xx_valcombo.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
          'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
          'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
          dcTmp.DataSource = dttTmp
          For i As Integer = 0 To dcTmp.List.Count - 1
            If NTSCStr(CType(dcTmp.Current, DataRowView).Row!cod).ToUpper <> NTSCStr(grvAtt.GetFocusedRowCellValue(xx_valcombo)).ToUpper Then
              dcTmp.MoveNext()
            Else
              Exit For
            End If
          Next
          grvAtt.liListCmb.DataSource = dcTmp
          grvAtt.liListCmb.Visible = True
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

End Class
