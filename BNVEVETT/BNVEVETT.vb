Imports System.Data
Imports NTSInformatica.CLN__STD
Public Class FRMVEVETT

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE + CLN__STD.bsModExtCRM
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

  Public oCleVett As CLEVEVETT
  Public oCallParams As CLE__CLDP
  Public dsVett As DataSet
  Public dcVett As BindingSource = New BindingSource()

#Region "DICHIARAZIONE CONTROLLI"

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grVett As NTSInformatica.NTSGrid
  Public WithEvents grvVett As NTSInformatica.NTSGridView
  Public WithEvents tb_codvett As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desvett As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desvet2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_ntelef As NTSInformatica.NTSGridColumn
  Public WithEvents tb_nfax As NTSInformatica.NTSGridColumn
  Public WithEvents tb_vettemail As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codforn As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codforn As NTSInformatica.NTSGridColumn
  Public WithEvents tb_iscralb As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsBarButtonItem1 As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsBarSubItem1 As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbSpeseTrasporto As NTSInformatica.NTSBarMenuItem

#End Region

#Region "INIZIALIZZAZIONE  CONTROLLI"

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEVETT))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbSpeseTrasporto = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.NtsBarButtonItem1 = New NTSInformatica.NTSBarButtonItem
    Me.NtsBarSubItem1 = New NTSInformatica.NTSBarSubItem
    Me.grVett = New NTSInformatica.NTSGrid
    Me.grvVett = New NTSInformatica.NTSGridView
    Me.tb_codvett = New NTSInformatica.NTSGridColumn
    Me.tb_desvett = New NTSInformatica.NTSGridColumn
    Me.tb_desvet2 = New NTSInformatica.NTSGridColumn
    Me.tb_ntelef = New NTSInformatica.NTSGridColumn
    Me.tb_nfax = New NTSInformatica.NTSGridColumn
    Me.tb_vettemail = New NTSInformatica.NTSGridColumn
    Me.tb_codforn = New NTSInformatica.NTSGridColumn
    Me.xx_codforn = New NTSInformatica.NTSGridColumn
    Me.tb_iscralb = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grVett, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVett, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbSpeseTrasporto})
    Me.NtsBarManager1.MaxItemId = 17
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSpeseTrasporto)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbSpeseTrasporto
    '
    Me.tlbSpeseTrasporto.Caption = "SpeseTrasporto"
    Me.tlbSpeseTrasporto.Id = 16
    Me.tlbSpeseTrasporto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbSpeseTrasporto.Name = "tlbSpeseTrasporto"
    Me.tlbSpeseTrasporto.NTSIsCheckBox = False
    Me.tlbSpeseTrasporto.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 9
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 10
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'NtsBarButtonItem1
    '
    Me.NtsBarButtonItem1.Caption = "tlbStrumenti"
    Me.NtsBarButtonItem1.Id = 12
    Me.NtsBarButtonItem1.Name = "NtsBarButtonItem1"
    Me.NtsBarButtonItem1.Visible = True
    '
    'NtsBarSubItem1
    '
    Me.NtsBarSubItem1.Caption = "Imposta Stampante"
    Me.NtsBarSubItem1.Id = 13
    Me.NtsBarSubItem1.Name = "NtsBarSubItem1"
    Me.NtsBarSubItem1.Visible = True
    '
    'grVett
    '
    Me.grVett.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grVett.EmbeddedNavigator.Name = ""
    Me.grVett.Location = New System.Drawing.Point(0, 26)
    Me.grVett.MainView = Me.grvVett
    Me.grVett.Name = "grVett"
    Me.grVett.Size = New System.Drawing.Size(600, 294)
    Me.grVett.TabIndex = 4
    Me.grVett.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVett})
    '
    'grvVett
    '
    Me.grvVett.ActiveFilterEnabled = False
    Me.grvVett.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codvett, Me.tb_desvett, Me.tb_desvet2, Me.tb_ntelef, Me.tb_nfax, Me.tb_vettemail, Me.tb_codforn, Me.xx_codforn, Me.tb_iscralb})
    Me.grvVett.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVett.Enabled = True
    Me.grvVett.GridControl = Me.grVett
    Me.grvVett.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVett.Name = "grvVett"
    Me.grvVett.NTSAllowDelete = True
    Me.grvVett.NTSAllowInsert = True
    Me.grvVett.NTSAllowUpdate = True
    Me.grvVett.NTSMenuContext = Nothing
    Me.grvVett.OptionsCustomization.AllowRowSizing = True
    Me.grvVett.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVett.OptionsNavigation.UseTabKey = False
    Me.grvVett.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVett.OptionsView.ColumnAutoWidth = False
    Me.grvVett.OptionsView.EnableAppearanceEvenRow = True
    Me.grvVett.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVett.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVett.OptionsView.ShowGroupPanel = False
    Me.grvVett.RowHeight = 16
    '
    'tb_codvett
    '
    Me.tb_codvett.AppearanceCell.Options.UseBackColor = True
    Me.tb_codvett.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codvett.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codvett.Caption = "Codice vettore"
    Me.tb_codvett.Enabled = True
    Me.tb_codvett.FieldName = "tb_codvett"
    Me.tb_codvett.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codvett.Name = "tb_codvett"
    Me.tb_codvett.NTSRepositoryComboBox = Nothing
    Me.tb_codvett.NTSRepositoryItemCheck = Nothing
    Me.tb_codvett.NTSRepositoryItemMemo = Nothing
    Me.tb_codvett.NTSRepositoryItemText = Nothing
    Me.tb_codvett.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codvett.OptionsFilter.AllowFilter = False
    Me.tb_codvett.Visible = True
    Me.tb_codvett.VisibleIndex = 0
    '
    'tb_desvett
    '
    Me.tb_desvett.AppearanceCell.Options.UseBackColor = True
    Me.tb_desvett.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desvett.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desvett.Caption = "Descrizione"
    Me.tb_desvett.Enabled = True
    Me.tb_desvett.FieldName = "tb_desvett"
    Me.tb_desvett.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desvett.Name = "tb_desvett"
    Me.tb_desvett.NTSRepositoryComboBox = Nothing
    Me.tb_desvett.NTSRepositoryItemCheck = Nothing
    Me.tb_desvett.NTSRepositoryItemMemo = Nothing
    Me.tb_desvett.NTSRepositoryItemText = Nothing
    Me.tb_desvett.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desvett.OptionsFilter.AllowFilter = False
    Me.tb_desvett.Visible = True
    Me.tb_desvett.VisibleIndex = 1
    '
    'tb_desvet2
    '
    Me.tb_desvet2.AppearanceCell.Options.UseBackColor = True
    Me.tb_desvet2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desvet2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desvet2.Caption = "Indirizzo"
    Me.tb_desvet2.Enabled = True
    Me.tb_desvet2.FieldName = "tb_desvet2"
    Me.tb_desvet2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desvet2.Name = "tb_desvet2"
    Me.tb_desvet2.NTSRepositoryComboBox = Nothing
    Me.tb_desvet2.NTSRepositoryItemCheck = Nothing
    Me.tb_desvet2.NTSRepositoryItemMemo = Nothing
    Me.tb_desvet2.NTSRepositoryItemText = Nothing
    Me.tb_desvet2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desvet2.OptionsFilter.AllowFilter = False
    Me.tb_desvet2.Visible = True
    Me.tb_desvet2.VisibleIndex = 2
    '
    'tb_ntelef
    '
    Me.tb_ntelef.AppearanceCell.Options.UseBackColor = True
    Me.tb_ntelef.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ntelef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ntelef.Caption = "Nr. Telefono"
    Me.tb_ntelef.Enabled = True
    Me.tb_ntelef.FieldName = "tb_ntelef"
    Me.tb_ntelef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_ntelef.Name = "tb_ntelef"
    Me.tb_ntelef.NTSRepositoryComboBox = Nothing
    Me.tb_ntelef.NTSRepositoryItemCheck = Nothing
    Me.tb_ntelef.NTSRepositoryItemMemo = Nothing
    Me.tb_ntelef.NTSRepositoryItemText = Nothing
    Me.tb_ntelef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_ntelef.OptionsFilter.AllowFilter = False
    Me.tb_ntelef.Visible = True
    Me.tb_ntelef.VisibleIndex = 3
    '
    'tb_nfax
    '
    Me.tb_nfax.AppearanceCell.Options.UseBackColor = True
    Me.tb_nfax.AppearanceCell.Options.UseTextOptions = True
    Me.tb_nfax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_nfax.Caption = "Nr. Fax"
    Me.tb_nfax.Enabled = True
    Me.tb_nfax.FieldName = "tb_nfax"
    Me.tb_nfax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_nfax.Name = "tb_nfax"
    Me.tb_nfax.NTSRepositoryComboBox = Nothing
    Me.tb_nfax.NTSRepositoryItemCheck = Nothing
    Me.tb_nfax.NTSRepositoryItemMemo = Nothing
    Me.tb_nfax.NTSRepositoryItemText = Nothing
    Me.tb_nfax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_nfax.OptionsFilter.AllowFilter = False
    Me.tb_nfax.Visible = True
    Me.tb_nfax.VisibleIndex = 4
    '
    'tb_vettemail
    '
    Me.tb_vettemail.AppearanceCell.Options.UseBackColor = True
    Me.tb_vettemail.AppearanceCell.Options.UseTextOptions = True
    Me.tb_vettemail.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_vettemail.Caption = "Indirizzo posta elettronica"
    Me.tb_vettemail.Enabled = True
    Me.tb_vettemail.FieldName = "tb_vettemail"
    Me.tb_vettemail.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_vettemail.Name = "tb_vettemail"
    Me.tb_vettemail.NTSRepositoryComboBox = Nothing
    Me.tb_vettemail.NTSRepositoryItemCheck = Nothing
    Me.tb_vettemail.NTSRepositoryItemMemo = Nothing
    Me.tb_vettemail.NTSRepositoryItemText = Nothing
    Me.tb_vettemail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_vettemail.OptionsFilter.AllowFilter = False
    Me.tb_vettemail.Visible = True
    Me.tb_vettemail.VisibleIndex = 5
    '
    'tb_codforn
    '
    Me.tb_codforn.AppearanceCell.Options.UseBackColor = True
    Me.tb_codforn.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codforn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codforn.Caption = "Codice Fornitore"
    Me.tb_codforn.Enabled = True
    Me.tb_codforn.FieldName = "tb_codforn"
    Me.tb_codforn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codforn.Name = "tb_codforn"
    Me.tb_codforn.NTSRepositoryComboBox = Nothing
    Me.tb_codforn.NTSRepositoryItemCheck = Nothing
    Me.tb_codforn.NTSRepositoryItemMemo = Nothing
    Me.tb_codforn.NTSRepositoryItemText = Nothing
    Me.tb_codforn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codforn.OptionsFilter.AllowFilter = False
    Me.tb_codforn.Visible = True
    Me.tb_codforn.VisibleIndex = 6
    '
    'xx_codforn
    '
    Me.xx_codforn.AppearanceCell.Options.UseBackColor = True
    Me.xx_codforn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codforn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codforn.Caption = "Descrizione Fornitore"
    Me.xx_codforn.Enabled = False
    Me.xx_codforn.FieldName = "xx_codforn"
    Me.xx_codforn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codforn.Name = "xx_codforn"
    Me.xx_codforn.NTSRepositoryComboBox = Nothing
    Me.xx_codforn.NTSRepositoryItemCheck = Nothing
    Me.xx_codforn.NTSRepositoryItemMemo = Nothing
    Me.xx_codforn.NTSRepositoryItemText = Nothing
    Me.xx_codforn.OptionsColumn.AllowEdit = False
    Me.xx_codforn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codforn.OptionsColumn.ReadOnly = True
    Me.xx_codforn.OptionsFilter.AllowFilter = False
    Me.xx_codforn.Visible = True
    Me.xx_codforn.VisibleIndex = 7
    '
    'tb_iscralb
    '
    Me.tb_iscralb.AppearanceCell.Options.UseBackColor = True
    Me.tb_iscralb.AppearanceCell.Options.UseTextOptions = True
    Me.tb_iscralb.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_iscralb.Caption = "N. iscr. albo autotrasportatori"
    Me.tb_iscralb.Enabled = True
    Me.tb_iscralb.FieldName = "tb_iscralb"
    Me.tb_iscralb.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_iscralb.Name = "tb_iscralb"
    Me.tb_iscralb.NTSRepositoryComboBox = Nothing
    Me.tb_iscralb.NTSRepositoryItemCheck = Nothing
    Me.tb_iscralb.NTSRepositoryItemMemo = Nothing
    Me.tb_iscralb.NTSRepositoryItemText = Nothing
    Me.tb_iscralb.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_iscralb.OptionsFilter.AllowFilter = False
    Me.tb_iscralb.Visible = True
    Me.tb_iscralb.VisibleIndex = 8
    '
    'FRMVEVETT
    '
    Me.ClientSize = New System.Drawing.Size(600, 320)
    Me.Controls.Add(Me.grVett)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.KeyPreview = False
    Me.Name = "FRMVEVETT"
    Me.NTSLastControlFocussed = Me.grVett
    Me.Text = "VETTORI/SPEDIZIONIERI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grVett, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVett, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEVETT", "BEVEVETT", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128173197397045107, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleVett = CType(oTmp, CLEVEVETT)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEVETT", strRemoteServer, strRemotePort)
    AddHandler oCleVett.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleVett.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvVett.NTSSetParam(oMenu, oApp.Tr(Me, 128230023368564516, "Griglia Vettori/Spedizionieri"))
      tb_codvett.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023368720689, "Codice vettore"), CLN__STD.tabvett)
      tb_desvett.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023368876862, "Descrizione"), 40, False)
      tb_desvet2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023369033035, "Indirizzo"), 40, False)
      tb_ntelef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023369189208, "Nr. Telefono"), 20, False)
      tb_nfax.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023369345381, "Nr. Fax"), 20, False)
      tb_vettemail.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023369501554, "Indirizzo posta elettronica"), 50, False)
      tb_codforn.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128974941911516576, "Codice Fornitore"), tabanagraf)
      tb_iscralb.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128974942547610326, "N. iscrizione albo autrasportatori"), 50, True)

      tb_desvett.NTSSetRichiesto()

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

#End Region

#Region "EVENTI FORM"

  Public Overridable Sub FRMVEVETT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleVett.Apri(DittaCorrente, dsVett) Then Me.Close()
      dcVett.DataSource = dsVett.Tables("TABVETT")
      dsVett.AcceptChanges()
      grVett.DataSource = dcVett
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvVett.NTSAllowInsert Then
            grvVett.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcVett.List.Count - 1
            If CType(dcVett.Item(i), DataRowView)!tb_codvett.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcVett.Position = i
              Exit For
            End If
          Next
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbSpeseTrasporto.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRMVEVETT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEVETT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcVett.Dispose()
      dsVett.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "EVENTI GRIGLIA"

  Public Overridable Sub grvVett_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvVett.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'Rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvVett_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvVett.NTSFocusedRowChanged
    Try
      'Se il codice valuta è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCInt(grvVett.GetFocusedRowCellValue(tb_codvett).ToString.Trim) <> 0 Then
        tb_codvett.Enabled = False
      Else
        tb_codvett.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "FUNZIONI"

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvVett.NTSSalvaRigaCorrente(dcVett, oCleVett.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleVett.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleVett.Ripristina(dcVett.Position, dcVett.Filter)
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
  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{tabvett.codditt} = '" & oApp.Ditta & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEVETT", "Reports1", " ", 0, nDestin, "BSVEVETT.RPT", False, "Vettori/Spedizionieri", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "EVENTI TOOLBAR"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvVett.NTSNuovo()
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
      If Not grvVett.NTSDeleteRigaCorrente(dcVett, True) Then Return
      oCleVett.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvVett.NTSRipristinaRigaCorrenteBefore(dcVett, True) Then Return
      oCleVett.Ripristina(dcVett.Position, dcVett.Filter)
      grvVett.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim strErr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvVett.FocusedColumn.Name = "tb_codforn" Then
        SetFastZoom(NTSCStr(grvVett.EditingValue), oParam)
        NTSZOOM.strIn = NTSCStr(grvVett.EditingValue)
        oParam.bVisGriglia = True
        oParam.strTipo = "F"
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvVett.EditingValue) Then grvVett.SetFocusedValue(NTSZOOM.strIn)
      Else
        NTSCallStandardZoom()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSpeseTrasporto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSpeseTrasporto.ItemClick
    Dim frmSprt As FRMVESPRT = Nothing
    Try
      frmSprt = CType(NTSNewFormModal("FRMVESPRT"), FRMVESPRT)
      If grvVett.NTSGetCurrentDataRow() Is Nothing Then Return
      If Not Salva() Then Return

      oCleVett.lCodVett = NTSCInt(grvVett.NTSGetCurrentDataRow()!tb_codvett)
      If Not frmSprt.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSprt.InitEntity(oCleVett)
      frmSprt.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSprt Is Nothing Then frmSprt.Dispose()
      frmSprt = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
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

End Class
