Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGAPRO
#Region "Variabili"
  Public oCleArti As CLEMGARTI
  Public oCallParams As CLE__CLDP
  Public dsApro As New DataSet
  Public dcApro As New BindingSource()
  Public dttApro As DataTable = Nothing
  Public dtrApro() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public strAproCodart As String
  Public strAproDesart As String
  Public bServer As Boolean

  Public WithEvents grApro As NTSInformatica.NTSGrid
  Public WithEvents grvApro As NTSInformatica.NTSGridView
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
  Public WithEvents apr_codart As NTSInformatica.NTSGridColumn
  Public WithEvents apr_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddpro As NTSInformatica.NTSGridColumn
  Public WithEvents apr_note As NTSInformatica.NTSGridColumn
  Public WithEvents apr_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents apr_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents apr_coddpro As NTSInformatica.NTSGridColumn
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGAPRO))
    Me.grApro = New NTSInformatica.NTSGrid
    Me.grvApro = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.apr_codart = New NTSInformatica.NTSGridColumn
    Me.apr_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_codtpro = New NTSInformatica.NTSGridColumn
    Me.apr_coddpro = New NTSInformatica.NTSGridColumn
    Me.xx_coddpro = New NTSInformatica.NTSGridColumn
    Me.apr_note = New NTSInformatica.NTSGridColumn
    Me.apr_ultagg = New NTSInformatica.NTSGridColumn
    Me.apr_opnome = New NTSInformatica.NTSGridColumn
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
    CType(Me.grApro, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvApro, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grApro
    '
    Me.grApro.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grApro.EmbeddedNavigator.Name = ""
    Me.grApro.Location = New System.Drawing.Point(0, 30)
    Me.grApro.MainView = Me.grvApro
    Me.grApro.Name = "grApro"
    Me.grApro.Size = New System.Drawing.Size(667, 336)
    Me.grApro.TabIndex = 0
    Me.grApro.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvApro})
    '
    'grvApro
    '
    Me.grvApro.ActiveFilterEnabled = False
    Me.grvApro.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.apr_codart, Me.apr_codtpro, Me.xx_codtpro, Me.apr_coddpro, Me.xx_coddpro, Me.apr_note, Me.apr_ultagg, Me.apr_opnome})
    Me.grvApro.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvApro.Enabled = True
    Me.grvApro.GridControl = Me.grApro
    Me.grvApro.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvApro.Name = "grvApro"
    Me.grvApro.NTSAllowDelete = True
    Me.grvApro.NTSAllowInsert = True
    Me.grvApro.NTSAllowUpdate = True
    Me.grvApro.NTSMenuContext = Nothing
    Me.grvApro.OptionsCustomization.AllowRowSizing = True
    Me.grvApro.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvApro.OptionsNavigation.UseTabKey = False
    Me.grvApro.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvApro.OptionsView.ColumnAutoWidth = False
    Me.grvApro.OptionsView.EnableAppearanceEvenRow = True
    Me.grvApro.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvApro.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvApro.OptionsView.ShowGroupPanel = False
    Me.grvApro.RowHeight = 16
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
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'apr_codart
    '
    Me.apr_codart.AppearanceCell.Options.UseBackColor = True
    Me.apr_codart.AppearanceCell.Options.UseTextOptions = True
    Me.apr_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_codart.Caption = "Codice articolo"
    Me.apr_codart.Enabled = False
    Me.apr_codart.FieldName = "apr_codart"
    Me.apr_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_codart.Name = "apr_codart"
    Me.apr_codart.NTSRepositoryComboBox = Nothing
    Me.apr_codart.NTSRepositoryItemCheck = Nothing
    Me.apr_codart.NTSRepositoryItemText = Nothing
    Me.apr_codart.OptionsColumn.AllowEdit = False
    Me.apr_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_codart.OptionsColumn.ReadOnly = True
    Me.apr_codart.OptionsFilter.AllowFilter = False
    '
    'apr_codtpro
    '
    Me.apr_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.apr_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.apr_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_codtpro.Caption = "Tipo promozione"
    Me.apr_codtpro.Enabled = True
    Me.apr_codtpro.FieldName = "apr_codtpro"
    Me.apr_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_codtpro.Name = "apr_codtpro"
    Me.apr_codtpro.NTSRepositoryComboBox = Nothing
    Me.apr_codtpro.NTSRepositoryItemCheck = Nothing
    Me.apr_codtpro.NTSRepositoryItemText = Nothing
    Me.apr_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_codtpro.OptionsFilter.AllowFilter = False
    Me.apr_codtpro.Visible = True
    Me.apr_codtpro.VisibleIndex = 0
    '
    'xx_codtpro
    '
    Me.xx_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtpro.Caption = "Descrizione promozione"
    Me.xx_codtpro.Enabled = False
    Me.xx_codtpro.FieldName = "xx_codtpro"
    Me.xx_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpro.Name = "xx_codtpro"
    Me.xx_codtpro.NTSRepositoryComboBox = Nothing
    Me.xx_codtpro.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpro.NTSRepositoryItemText = Nothing
    Me.xx_codtpro.OptionsColumn.AllowEdit = False
    Me.xx_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpro.OptionsColumn.ReadOnly = True
    Me.xx_codtpro.OptionsFilter.AllowFilter = False
    Me.xx_codtpro.Visible = True
    Me.xx_codtpro.VisibleIndex = 1
    '
    'apr_coddpro
    '
    Me.apr_coddpro.AppearanceCell.Options.UseBackColor = True
    Me.apr_coddpro.AppearanceCell.Options.UseTextOptions = True
    Me.apr_coddpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_coddpro.Caption = "Dettaglio prom."
    Me.apr_coddpro.Enabled = True
    Me.apr_coddpro.FieldName = "apr_coddpro"
    Me.apr_coddpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_coddpro.Name = "apr_coddpro"
    Me.apr_coddpro.NTSRepositoryComboBox = Nothing
    Me.apr_coddpro.NTSRepositoryItemCheck = Nothing
    Me.apr_coddpro.NTSRepositoryItemText = Nothing
    Me.apr_coddpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_coddpro.OptionsFilter.AllowFilter = False
    Me.apr_coddpro.Visible = True
    Me.apr_coddpro.VisibleIndex = 2
    '
    'xx_coddpro
    '
    Me.xx_coddpro.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddpro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddpro.Caption = "Descrizione dettaglio"
    Me.xx_coddpro.Enabled = False
    Me.xx_coddpro.FieldName = "xx_coddpro"
    Me.xx_coddpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddpro.Name = "xx_coddpro"
    Me.xx_coddpro.NTSRepositoryComboBox = Nothing
    Me.xx_coddpro.NTSRepositoryItemCheck = Nothing
    Me.xx_coddpro.NTSRepositoryItemText = Nothing
    Me.xx_coddpro.OptionsColumn.AllowEdit = False
    Me.xx_coddpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddpro.OptionsColumn.ReadOnly = True
    Me.xx_coddpro.OptionsFilter.AllowFilter = False
    Me.xx_coddpro.Visible = True
    Me.xx_coddpro.VisibleIndex = 3
    '
    'apr_note
    '
    Me.apr_note.AppearanceCell.Options.UseBackColor = True
    Me.apr_note.AppearanceCell.Options.UseTextOptions = True
    Me.apr_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_note.Caption = "Note"
    Me.apr_note.Enabled = True
    Me.apr_note.FieldName = "apr_note"
    Me.apr_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_note.Name = "apr_note"
    Me.apr_note.NTSRepositoryComboBox = Nothing
    Me.apr_note.NTSRepositoryItemCheck = Nothing
    Me.apr_note.NTSRepositoryItemText = Nothing
    Me.apr_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_note.OptionsFilter.AllowFilter = False
    Me.apr_note.Visible = True
    Me.apr_note.VisibleIndex = 4
    '
    'apr_ultagg
    '
    Me.apr_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.apr_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.apr_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_ultagg.Enabled = False
    Me.apr_ultagg.FieldName = "apr_ultagg"
    Me.apr_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_ultagg.Name = "apr_ultagg"
    Me.apr_ultagg.NTSRepositoryComboBox = Nothing
    Me.apr_ultagg.NTSRepositoryItemCheck = Nothing
    Me.apr_ultagg.NTSRepositoryItemText = Nothing
    Me.apr_ultagg.OptionsColumn.AllowEdit = False
    Me.apr_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_ultagg.OptionsColumn.ReadOnly = True
    Me.apr_ultagg.OptionsFilter.AllowFilter = False
    '
    'apr_opnome
    '
    Me.apr_opnome.AppearanceCell.Options.UseBackColor = True
    Me.apr_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.apr_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_opnome.Caption = "apr_opnome"
    Me.apr_opnome.Enabled = False
    Me.apr_opnome.FieldName = "apr_opnome"
    Me.apr_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_opnome.Name = "apr_opnome"
    Me.apr_opnome.NTSRepositoryComboBox = Nothing
    Me.apr_opnome.NTSRepositoryItemCheck = Nothing
    Me.apr_opnome.NTSRepositoryItemText = Nothing
    Me.apr_opnome.OptionsColumn.AllowEdit = False
    Me.apr_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_opnome.OptionsColumn.ReadOnly = True
    Me.apr_opnome.OptionsFilter.AllowFilter = False
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
    'FRMMGAPRO
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grApro)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGAPRO"
    Me.NTSLastControlFocussed = Me.grApro
    Me.Text = "Promozioni articolo"
    CType(Me.grApro, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvApro, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvApro.NTSSetParam(oMenu, oApp.Tr(Me, 128788386634737149, "Promozioni articolo"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386652863077, "Cod. ditta"), 12, False)
      apr_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386669113909, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      apr_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128788386685989773, "Tipo promozione"), tabtpro)
      xx_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386700678025, "Descrizione promozione"), 0, True)
      apr_coddpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128788386717085115, "Dettaglio prom."), tabdpro)
      xx_coddpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386728804465, "Descrizione dettaglio"), 0, True)
      apr_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386742398911, "Note"), 0, True)
      apr_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128788386756305873, ""), False)
      apr_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788386778963283, "apr_opnome"), 20, False)

      apr_codtpro.NTSSetRichiesto()

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
      dRes = grvApro.NTSSalvaRigaCorrente(dcApro, oCleArti.AproRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArti.AproSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArti.AproRipristina(dcApro.Position, dcApro.Filter)
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
  Public Overridable Sub FRMMGAPRO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArti.AproApri(DittaCorrente, strAproCodart, dsApro) Then Me.Close()

      dcApro.DataSource = dsApro.Tables("ARTPROM")
      grApro.DataSource = dcApro

      apr_codtpro.Enabled = False

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
  Public Overridable Sub FRMMGAPRO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcApro.Dispose()
      dsApro.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGAPRO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvApro.NTSNuovo()
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
      If Not grvApro.NTSDeleteRigaCorrente(dcApro, True) Then Return
      oCleArti.AproSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvApro.NTSRipristinaRigaCorrenteBefore(dcApro, True) Then Return
      oCleArti.AproRipristina(dcApro.Position, dcApro.Filter)
      grvApro.NTSRipristinaRigaCorrenteAfter()
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

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

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
  Public Overridable Sub grvApro_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvApro.NTSBeforeRowUpdate
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

  Public Overridable Sub grvApro_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvApro.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArti Is Nothing Then Return

      dtrT = grvApro.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        apr_codtpro.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!apr_codtpro) <> 0 Then
        apr_codtpro.Enabled = False
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
      strCrpe = "{ARTPROM.codditt} = " & CStrSQL(DittaCorrente) & _
        " And {ARTPROM.apr_codart} = " & CStrSQL(strAproCodart)
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGARTI", "Reports1", " ", 0, nDestin, "BSMGAPRO.RPT", False, "Promozioni", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESCR", ConvStrRpt(NTSCStr("ARTICOLO --> " & strAproCodart & " - " & strAproDesart)))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class