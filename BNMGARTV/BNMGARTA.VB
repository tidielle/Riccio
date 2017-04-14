Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGARTA
#Region "Variabili"
  Public oCleArtv As CLEMGARTV
  Public oCallParams As CLE__CLDP
  Public dsArta As New DataSet
  Public dcArta As New BindingSource()
  Public dttArta As DataTable = Nothing
  Public dtrArta() As DataRow = Nothing

  Public strArtaCodart As String
  Public strArtaDesart As String
  Public bServer As Boolean
  Public bInCancellazione As Boolean = False

  Public WithEvents grArta As NTSInformatica.NTSGrid
  Public WithEvents grvArta As NTSInformatica.NTSGridView
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

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents apa_codart As NTSInformatica.NTSGridColumn
  Public WithEvents apa_codartas As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codartas As NTSInformatica.NTSGridColumn
  Public WithEvents apa_note As NTSInformatica.NTSGridColumn
  Public WithEvents apa_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents apa_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents apa_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARTA))
    Me.grArta = New NTSInformatica.NTSGrid
    Me.grvArta = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.apa_codart = New NTSInformatica.NTSGridColumn
    Me.apa_codartas = New NTSInformatica.NTSGridColumn
    Me.xx_codartas = New NTSInformatica.NTSGridColumn
    Me.apa_tipo = New NTSInformatica.NTSGridColumn
    Me.apa_note = New NTSInformatica.NTSGridColumn
    Me.apa_ultagg = New NTSInformatica.NTSGridColumn
    Me.apa_opnome = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbDescr = New NTSInformatica.NTSLabel
    CType(Me.grArta, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvArta, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 20)
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'grArta
    '
    Me.grArta.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grArta.EmbeddedNavigator.Name = ""
    Me.grArta.Location = New System.Drawing.Point(0, 53)
    Me.grArta.MainView = Me.grvArta
    Me.grArta.Name = "grArta"
    Me.grArta.Size = New System.Drawing.Size(667, 313)
    Me.grArta.TabIndex = 0
    Me.grArta.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvArta})
    '
    'grvArta
    '
    Me.grvArta.ActiveFilterEnabled = False
    Me.grvArta.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.apa_codart, Me.apa_codartas, Me.xx_codartas, Me.apa_tipo, Me.apa_note, Me.apa_ultagg, Me.apa_opnome})
    Me.grvArta.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvArta.Enabled = True
    Me.grvArta.GridControl = Me.grArta
    Me.grvArta.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvArta.MinRowHeight = 14
    Me.grvArta.Name = "grvArta"
    Me.grvArta.NTSAllowDelete = True
    Me.grvArta.NTSAllowInsert = True
    Me.grvArta.NTSAllowUpdate = True
    Me.grvArta.NTSMenuContext = Nothing
    Me.grvArta.OptionsCustomization.AllowRowSizing = True
    Me.grvArta.OptionsFilter.AllowFilterEditor = False
    Me.grvArta.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvArta.OptionsNavigation.UseTabKey = False
    Me.grvArta.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvArta.OptionsView.ColumnAutoWidth = False
    Me.grvArta.OptionsView.EnableAppearanceEvenRow = True
    Me.grvArta.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvArta.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvArta.OptionsView.ShowGroupPanel = False
    Me.grvArta.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. ditta"
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
    'apa_codart
    '
    Me.apa_codart.AppearanceCell.Options.UseBackColor = True
    Me.apa_codart.AppearanceCell.Options.UseTextOptions = True
    Me.apa_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_codart.Caption = "Codice articolo"
    Me.apa_codart.Enabled = False
    Me.apa_codart.FieldName = "apa_codart"
    Me.apa_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_codart.Name = "apa_codart"
    Me.apa_codart.NTSRepositoryComboBox = Nothing
    Me.apa_codart.NTSRepositoryItemCheck = Nothing
    Me.apa_codart.NTSRepositoryItemMemo = Nothing
    Me.apa_codart.NTSRepositoryItemText = Nothing
    Me.apa_codart.OptionsColumn.AllowEdit = False
    Me.apa_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_codart.OptionsColumn.ReadOnly = True
    Me.apa_codart.OptionsFilter.AllowFilter = False
    '
    'apa_codartas
    '
    Me.apa_codartas.AppearanceCell.Options.UseBackColor = True
    Me.apa_codartas.AppearanceCell.Options.UseTextOptions = True
    Me.apa_codartas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_codartas.Caption = "Articolo"
    Me.apa_codartas.Enabled = True
    Me.apa_codartas.FieldName = "apa_codartas"
    Me.apa_codartas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_codartas.Name = "apa_codartas"
    Me.apa_codartas.NTSRepositoryComboBox = Nothing
    Me.apa_codartas.NTSRepositoryItemCheck = Nothing
    Me.apa_codartas.NTSRepositoryItemMemo = Nothing
    Me.apa_codartas.NTSRepositoryItemText = Nothing
    Me.apa_codartas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_codartas.OptionsFilter.AllowFilter = False
    Me.apa_codartas.Visible = True
    Me.apa_codartas.VisibleIndex = 0
    '
    'xx_codartas
    '
    Me.xx_codartas.AppearanceCell.Options.UseBackColor = True
    Me.xx_codartas.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codartas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codartas.Caption = "Descrizione"
    Me.xx_codartas.Enabled = False
    Me.xx_codartas.FieldName = "xx_codartas"
    Me.xx_codartas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codartas.Name = "xx_codartas"
    Me.xx_codartas.NTSRepositoryComboBox = Nothing
    Me.xx_codartas.NTSRepositoryItemCheck = Nothing
    Me.xx_codartas.NTSRepositoryItemMemo = Nothing
    Me.xx_codartas.NTSRepositoryItemText = Nothing
    Me.xx_codartas.OptionsColumn.AllowEdit = False
    Me.xx_codartas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codartas.OptionsColumn.ReadOnly = True
    Me.xx_codartas.OptionsFilter.AllowFilter = False
    Me.xx_codartas.Visible = True
    Me.xx_codartas.VisibleIndex = 1
    '
    'apa_tipo
    '
    Me.apa_tipo.AppearanceCell.Options.UseBackColor = True
    Me.apa_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.apa_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_tipo.Caption = "Tipo"
    Me.apa_tipo.Enabled = True
    Me.apa_tipo.FieldName = "apa_tipo"
    Me.apa_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_tipo.Name = "apa_tipo"
    Me.apa_tipo.NTSRepositoryComboBox = Nothing
    Me.apa_tipo.NTSRepositoryItemCheck = Nothing
    Me.apa_tipo.NTSRepositoryItemMemo = Nothing
    Me.apa_tipo.NTSRepositoryItemText = Nothing
    Me.apa_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_tipo.OptionsFilter.AllowFilter = False
    Me.apa_tipo.Visible = True
    Me.apa_tipo.VisibleIndex = 2
    '
    'apa_note
    '
    Me.apa_note.AppearanceCell.Options.UseBackColor = True
    Me.apa_note.AppearanceCell.Options.UseTextOptions = True
    Me.apa_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_note.Caption = "Note"
    Me.apa_note.Enabled = True
    Me.apa_note.FieldName = "apa_note"
    Me.apa_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_note.Name = "apa_note"
    Me.apa_note.NTSRepositoryComboBox = Nothing
    Me.apa_note.NTSRepositoryItemCheck = Nothing
    Me.apa_note.NTSRepositoryItemMemo = Nothing
    Me.apa_note.NTSRepositoryItemText = Nothing
    Me.apa_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_note.OptionsFilter.AllowFilter = False
    Me.apa_note.Visible = True
    Me.apa_note.VisibleIndex = 3
    '
    'apa_ultagg
    '
    Me.apa_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.apa_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.apa_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_ultagg.Enabled = False
    Me.apa_ultagg.FieldName = "apa_ultagg"
    Me.apa_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_ultagg.Name = "apa_ultagg"
    Me.apa_ultagg.NTSRepositoryComboBox = Nothing
    Me.apa_ultagg.NTSRepositoryItemCheck = Nothing
    Me.apa_ultagg.NTSRepositoryItemMemo = Nothing
    Me.apa_ultagg.NTSRepositoryItemText = Nothing
    Me.apa_ultagg.OptionsColumn.AllowEdit = False
    Me.apa_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_ultagg.OptionsColumn.ReadOnly = True
    Me.apa_ultagg.OptionsFilter.AllowFilter = False
    '
    'apa_opnome
    '
    Me.apa_opnome.AppearanceCell.Options.UseBackColor = True
    Me.apa_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.apa_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apa_opnome.Enabled = False
    Me.apa_opnome.FieldName = "apa_opnome"
    Me.apa_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apa_opnome.Name = "apa_opnome"
    Me.apa_opnome.NTSRepositoryComboBox = Nothing
    Me.apa_opnome.NTSRepositoryItemCheck = Nothing
    Me.apa_opnome.NTSRepositoryItemMemo = Nothing
    Me.apa_opnome.NTSRepositoryItemText = Nothing
    Me.apa_opnome.OptionsColumn.AllowEdit = False
    Me.apa_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apa_opnome.OptionsColumn.ReadOnly = True
    Me.apa_opnome.OptionsFilter.AllowFilter = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStampaVideo, Me.tlbStampa, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 14
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 12
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 11
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbDescr)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(667, 23)
    Me.pnTop.TabIndex = 644
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbDescr.Location = New System.Drawing.Point(12, 5)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(576, 13)
    Me.lbDescr.TabIndex = 651
    Me.lbDescr.Text = "Variante: XXXXXXXXXXXXXXXXXXXXXXXXXXXX - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & _
        "XXXX"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'FRMMGARTA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grArta)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGARTA"
    Me.NTSLastControlFocussed = Me.grArta
    Me.Text = "ARTICOLI ACCESSORI/SUCCEDANEI"
    CType(Me.grArta, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvArta, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttApa_tipo As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttApa_tipo.Columns.Add("cod", GetType(String))
      dttApa_tipo.Columns.Add("val", GetType(String))
      dttApa_tipo.Rows.Add(New Object() {"A", "Accessorio"})
      dttApa_tipo.Rows.Add(New Object() {"S", "Succedaneo"})
      dttApa_tipo.AcceptChanges()

      grvArta.NTSSetParam(oMenu, oApp.Tr(Me, 128788387058196329, "Articoli accessori/succedanei"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788387075853483, "Cod. ditta"), 12, False)
      apa_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788387093198121, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      apa_codartas.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128788387107105083, "Articolo"), tabartico, False)
      xx_codartas.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788387122574625, "Descrizione"), 0, True)
      apa_tipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128788387146950873, "Tipo"), dttApa_tipo, "val", "cod")
      apa_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788387162732931, "Note"), 0, True)
      apa_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128788387177577441, ""), False)
      apa_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788387195078337, ""), 20, False)

      apa_codartas.NTSSetRichiesto()

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
  Public Overridable Sub InitEntity(ByRef cleArti As CLEMGARTV)
    oCleArtv = cleArti
    AddHandler oCleArtv.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvArta.NTSSalvaRigaCorrente(dcArta, oCleArtv.ArtaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArtv.ArtaSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArtv.ArtaRipristina(dcArta.Position, dcArta.Filter)
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
  Public Overridable Sub FRMMGARTA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArtv.ArtaApri(DittaCorrente, strArtaCodart, dsArta) Then Me.Close()

      dcArta.DataSource = dsArta.Tables("ARTACCE")
      grArta.DataSource = dcArta

      apa_codartas.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      lbDescr.Text = "Variante: '" & strArtaCodart & "'" & _
        IIf(strArtaDesart.Trim <> "", " - ", "").ToString & strArtaDesart.Trim
      '--------------------------------------------------------------------------------------------------------------
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
  Public Overridable Sub FRMMGARTA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArta.Dispose()
      dsArta.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGARTA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvArta.NTSNuovo()
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
      bInCancellazione = True
      If Not grvArta.NTSDeleteRigaCorrente(dcArta, True) Then Return
      oCleArtv.ArtaSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bInCancellazione = False
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvArta.NTSRipristinaRigaCorrenteBefore(dcArta, True) Then Return
      oCleArtv.ArtaRipristina(dcArta.Position, dcArta.Filter)
      grvArta.NTSRipristinaRigaCorrenteAfter()
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
      '------------------------------------
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)    'abilito la gestione dello zoom veloce

        If grvArta.FocusedColumn.Equals(apa_codartas) Then
          'colonna particolare: codice articolo
          NTSZOOM.strIn = NTSCStr(grvArta.GetFocusedValue)
          oParam.nMagaz = 0
          oParam.nListino = 0
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvArta.GetFocusedValue) Then grvArta.SetFocusedValue(NTSZOOM.strIn)
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

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
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
  Public Overridable Sub grvArta_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvArta.NTSBeforeRowUpdate
    Try
      If Not IsNothing(grvArta.NTSGetCurrentDataRow) Then
        If (bInCancellazione = False) And (grvArta.NTSGetCurrentDataRow.RowState = DataRowState.Added) Then
          oCleArtv.CompletaSuccedaneiAccessori()
        End If
      End If

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

  Public Overridable Sub grvArta_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvArta.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArtv Is Nothing Then Return

      dtrT = grvArta.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        apa_codartas.Enabled = True
        Return
      End If

      If NTSCStr(dtrT!apa_codartas) <> " " Then
        apa_codartas.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{ARTACCE.codditt} = " & CStrSQL(DittaCorrente) & _
        " And {ARTACCE.apa_codart} = " & CStrSQL(strArtaCodart)
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGARTV", "Reports1", " ", 0, nDestin, "BSMGARTA.RPT", False, "Accessori/succedanei", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESCR", ConvStrRpt(NTSCStr("ARTICOLO --> " & strArtaCodart & " - " & strArtaDesart)))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class