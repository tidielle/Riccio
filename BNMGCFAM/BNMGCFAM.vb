Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCFAM

  Public Shared ReadOnly Property TIPO_ENTITY() As String
    Get
      Return CLEMGCFAM.TIPO_ENTITY
    End Get
  End Property

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = CLN__STD.bsModSupCAE
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
#End Region

#Region "Variabili"
  Public oCleCfam As CLEMGCFAM
  Public oCallParams As CLE__CLDP
  Public dsCfam As DataSet
  Public dcCfam As BindingSource = New BindingSource()
  Private strNomeReport As String = "BSMGCFAM"

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDesLingua As NTSInformatica.NTSBarMenuItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grCfam As NTSInformatica.NTSGrid
  Public WithEvents grvCfam As NTSInformatica.NTSGridView
  Public WithEvents tb_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descfam As NTSInformatica.NTSGridColumn
  Public WithEvents tb_unmisfam As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descfa2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_coddica As NTSInformatica.NTSGridColumn
  Public WithEvents tb_coddicv As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codtcdc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddica As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddicv As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtcdc As NTSInformatica.NTSGridColumn
#End Region

#Region "Inizializzazione"

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCFAM))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbDesLingua = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grCfam = New NTSInformatica.NTSGrid
    Me.grvCfam = New NTSInformatica.NTSGridView
    Me.tb_codcfam = New NTSInformatica.NTSGridColumn
    Me.tb_descfam = New NTSInformatica.NTSGridColumn
    Me.tb_unmisfam = New NTSInformatica.NTSGridColumn
    Me.tb_descfa2 = New NTSInformatica.NTSGridColumn
    Me.tb_coddica = New NTSInformatica.NTSGridColumn
    Me.xx_coddica = New NTSInformatica.NTSGridColumn
    Me.tb_coddicv = New NTSInformatica.NTSGridColumn
    Me.xx_coddicv = New NTSInformatica.NTSGridColumn
    Me.tb_codtcdc = New NTSInformatica.NTSGridColumn
    Me.xx_codtcdc = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grCfam, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCfam, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbDesLingua})
    Me.NtsBarManager1.MaxItemId = 18
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDesLingua), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbDesLingua
    '
    Me.tlbDesLingua.Caption = "Descrizioni in lingua"
    Me.tlbDesLingua.Id = 17
    Me.tlbDesLingua.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDesLingua.Name = "tlbDesLingua"
    Me.tlbDesLingua.NTSIsCheckBox = False
    Me.tlbDesLingua.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
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
    'grCfam
    '
    Me.grCfam.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCfam.EmbeddedNavigator.Name = ""
    Me.grCfam.Location = New System.Drawing.Point(0, 26)
    Me.grCfam.MainView = Me.grvCfam
    Me.grCfam.Name = "grCfam"
    Me.grCfam.Size = New System.Drawing.Size(648, 416)
    Me.grCfam.TabIndex = 5
    Me.grCfam.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCfam})
    '
    'grvCfam
    '
    Me.grvCfam.ActiveFilterEnabled = False
    Me.grvCfam.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codcfam, Me.tb_descfam, Me.tb_unmisfam, Me.tb_descfa2, Me.tb_coddica, Me.xx_coddica, Me.tb_coddicv, Me.xx_coddicv, Me.tb_codtcdc, Me.xx_codtcdc})
    Me.grvCfam.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCfam.Enabled = True
    Me.grvCfam.GridControl = Me.grCfam
    Me.grvCfam.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCfam.Name = "grvCfam"
    Me.grvCfam.NTSAllowDelete = True
    Me.grvCfam.NTSAllowInsert = True
    Me.grvCfam.NTSAllowUpdate = True
    Me.grvCfam.NTSMenuContext = Nothing
    Me.grvCfam.OptionsCustomization.AllowRowSizing = True
    Me.grvCfam.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCfam.OptionsNavigation.UseTabKey = False
    Me.grvCfam.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCfam.OptionsView.ColumnAutoWidth = False
    Me.grvCfam.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCfam.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCfam.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCfam.OptionsView.ShowGroupPanel = False
    Me.grvCfam.RowHeight = 16
    '
    'tb_codcfam
    '
    Me.tb_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.tb_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codcfam.Caption = "Codice"
    Me.tb_codcfam.Enabled = True
    Me.tb_codcfam.FieldName = "tb_codcfam"
    Me.tb_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codcfam.Name = "tb_codcfam"
    Me.tb_codcfam.NTSRepositoryComboBox = Nothing
    Me.tb_codcfam.NTSRepositoryItemCheck = Nothing
    Me.tb_codcfam.NTSRepositoryItemMemo = Nothing
    Me.tb_codcfam.NTSRepositoryItemText = Nothing
    Me.tb_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codcfam.OptionsFilter.AllowFilter = False
    Me.tb_codcfam.Visible = True
    Me.tb_codcfam.VisibleIndex = 0
    Me.tb_codcfam.Width = 70
    '
    'tb_descfam
    '
    Me.tb_descfam.AppearanceCell.Options.UseBackColor = True
    Me.tb_descfam.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descfam.Caption = "Descrizione"
    Me.tb_descfam.Enabled = True
    Me.tb_descfam.FieldName = "tb_descfam"
    Me.tb_descfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descfam.Name = "tb_descfam"
    Me.tb_descfam.NTSRepositoryComboBox = Nothing
    Me.tb_descfam.NTSRepositoryItemCheck = Nothing
    Me.tb_descfam.NTSRepositoryItemMemo = Nothing
    Me.tb_descfam.NTSRepositoryItemText = Nothing
    Me.tb_descfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descfam.OptionsFilter.AllowFilter = False
    Me.tb_descfam.Visible = True
    Me.tb_descfam.VisibleIndex = 1
    Me.tb_descfam.Width = 70
    '
    'tb_unmisfam
    '
    Me.tb_unmisfam.AppearanceCell.Options.UseBackColor = True
    Me.tb_unmisfam.AppearanceCell.Options.UseTextOptions = True
    Me.tb_unmisfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_unmisfam.Caption = "UM"
    Me.tb_unmisfam.Enabled = True
    Me.tb_unmisfam.FieldName = "tb_unmisfam"
    Me.tb_unmisfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_unmisfam.Name = "tb_unmisfam"
    Me.tb_unmisfam.NTSRepositoryComboBox = Nothing
    Me.tb_unmisfam.NTSRepositoryItemCheck = Nothing
    Me.tb_unmisfam.NTSRepositoryItemMemo = Nothing
    Me.tb_unmisfam.NTSRepositoryItemText = Nothing
    Me.tb_unmisfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_unmisfam.OptionsFilter.AllowFilter = False
    Me.tb_unmisfam.Visible = True
    Me.tb_unmisfam.VisibleIndex = 2
    Me.tb_unmisfam.Width = 70
    '
    'tb_descfa2
    '
    Me.tb_descfa2.AppearanceCell.Options.UseBackColor = True
    Me.tb_descfa2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descfa2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descfa2.Caption = "Descrizione 2"
    Me.tb_descfa2.Enabled = True
    Me.tb_descfa2.FieldName = "tb_descfa2"
    Me.tb_descfa2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descfa2.Name = "tb_descfa2"
    Me.tb_descfa2.NTSRepositoryComboBox = Nothing
    Me.tb_descfa2.NTSRepositoryItemCheck = Nothing
    Me.tb_descfa2.NTSRepositoryItemMemo = Nothing
    Me.tb_descfa2.NTSRepositoryItemText = Nothing
    Me.tb_descfa2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descfa2.OptionsFilter.AllowFilter = False
    Me.tb_descfa2.ToolTip = "Descrizione Linea/Prodotto (2.a parte)"
    Me.tb_descfa2.Visible = True
    Me.tb_descfa2.VisibleIndex = 3
    '
    'tb_coddica
    '
    Me.tb_coddica.AppearanceCell.Options.UseBackColor = True
    Me.tb_coddica.AppearanceCell.Options.UseTextOptions = True
    Me.tb_coddica.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_coddica.Caption = "Dimensione C.A."
    Me.tb_coddica.Enabled = True
    Me.tb_coddica.FieldName = "tb_coddica"
    Me.tb_coddica.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_coddica.Name = "tb_coddica"
    Me.tb_coddica.NTSRepositoryComboBox = Nothing
    Me.tb_coddica.NTSRepositoryItemCheck = Nothing
    Me.tb_coddica.NTSRepositoryItemMemo = Nothing
    Me.tb_coddica.NTSRepositoryItemText = Nothing
    Me.tb_coddica.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_coddica.OptionsFilter.AllowFilter = False
    Me.tb_coddica.ToolTip = "Codice dimensione ca"
    Me.tb_coddica.Visible = True
    Me.tb_coddica.VisibleIndex = 4
    '
    'xx_coddica
    '
    Me.xx_coddica.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddica.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddica.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddica.Caption = "Descr. dimensione C.A."
    Me.xx_coddica.Enabled = False
    Me.xx_coddica.FieldName = "xx_coddica"
    Me.xx_coddica.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddica.Name = "xx_coddica"
    Me.xx_coddica.NTSRepositoryComboBox = Nothing
    Me.xx_coddica.NTSRepositoryItemCheck = Nothing
    Me.xx_coddica.NTSRepositoryItemMemo = Nothing
    Me.xx_coddica.NTSRepositoryItemText = Nothing
    Me.xx_coddica.OptionsColumn.AllowEdit = False
    Me.xx_coddica.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddica.OptionsColumn.ReadOnly = True
    Me.xx_coddica.OptionsFilter.AllowFilter = False
    Me.xx_coddica.Visible = True
    Me.xx_coddica.VisibleIndex = 5
    '
    'tb_coddicv
    '
    Me.tb_coddicv.AppearanceCell.Options.UseBackColor = True
    Me.tb_coddicv.AppearanceCell.Options.UseTextOptions = True
    Me.tb_coddicv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_coddicv.Caption = "Valore dimensione C.A."
    Me.tb_coddicv.Enabled = True
    Me.tb_coddicv.FieldName = "tb_coddicv"
    Me.tb_coddicv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_coddicv.Name = "tb_coddicv"
    Me.tb_coddicv.NTSRepositoryComboBox = Nothing
    Me.tb_coddicv.NTSRepositoryItemCheck = Nothing
    Me.tb_coddicv.NTSRepositoryItemMemo = Nothing
    Me.tb_coddicv.NTSRepositoryItemText = Nothing
    Me.tb_coddicv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_coddicv.OptionsFilter.AllowFilter = False
    Me.tb_coddicv.ToolTip = "Valore dimensione"
    Me.tb_coddicv.Visible = True
    Me.tb_coddicv.VisibleIndex = 6
    '
    'xx_coddicv
    '
    Me.xx_coddicv.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddicv.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddicv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddicv.Caption = "Descr. valore dimensione C.A."
    Me.xx_coddicv.Enabled = False
    Me.xx_coddicv.FieldName = "xx_coddicv"
    Me.xx_coddicv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddicv.Name = "xx_coddicv"
    Me.xx_coddicv.NTSRepositoryComboBox = Nothing
    Me.xx_coddicv.NTSRepositoryItemCheck = Nothing
    Me.xx_coddicv.NTSRepositoryItemMemo = Nothing
    Me.xx_coddicv.NTSRepositoryItemText = Nothing
    Me.xx_coddicv.OptionsColumn.AllowEdit = False
    Me.xx_coddicv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddicv.OptionsColumn.ReadOnly = True
    Me.xx_coddicv.OptionsFilter.AllowFilter = False
    Me.xx_coddicv.Visible = True
    Me.xx_coddicv.VisibleIndex = 7
    '
    'tb_codtcdc
    '
    Me.tb_codtcdc.AppearanceCell.Options.UseBackColor = True
    Me.tb_codtcdc.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codtcdc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codtcdc.Caption = "Tip. linea\prodotto"
    Me.tb_codtcdc.Enabled = True
    Me.tb_codtcdc.FieldName = "tb_codtcdc"
    Me.tb_codtcdc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codtcdc.Name = "tb_codtcdc"
    Me.tb_codtcdc.NTSRepositoryComboBox = Nothing
    Me.tb_codtcdc.NTSRepositoryItemCheck = Nothing
    Me.tb_codtcdc.NTSRepositoryItemMemo = Nothing
    Me.tb_codtcdc.NTSRepositoryItemText = Nothing
    Me.tb_codtcdc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codtcdc.OptionsFilter.AllowFilter = False
    Me.tb_codtcdc.ToolTip = "Codice tipologia linea\prodotto (per l'applic. degli schemi)"
    Me.tb_codtcdc.Visible = True
    Me.tb_codtcdc.VisibleIndex = 8
    '
    'xx_codtcdc
    '
    Me.xx_codtcdc.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtcdc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtcdc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtcdc.Caption = "Descr. linea\prodotto"
    Me.xx_codtcdc.Enabled = False
    Me.xx_codtcdc.FieldName = "xx_codtcdc"
    Me.xx_codtcdc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtcdc.Name = "xx_codtcdc"
    Me.xx_codtcdc.NTSRepositoryComboBox = Nothing
    Me.xx_codtcdc.NTSRepositoryItemCheck = Nothing
    Me.xx_codtcdc.NTSRepositoryItemMemo = Nothing
    Me.xx_codtcdc.NTSRepositoryItemText = Nothing
    Me.xx_codtcdc.OptionsColumn.AllowEdit = False
    Me.xx_codtcdc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtcdc.OptionsColumn.ReadOnly = True
    Me.xx_codtcdc.OptionsFilter.AllowFilter = False
    Me.xx_codtcdc.Visible = True
    Me.xx_codtcdc.VisibleIndex = 9
    '
    'FRMMGCFAM
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grCfam)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGCFAM"
    Me.NTSLastControlFocussed = Me.grCfam
    Me.Text = "LINEE/FAMIGLIE PRODOTTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grCfam, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCfam, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGCFAM", "BEMGCFAM", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128490898873912000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCfam = CType(oTmp, CLEMGCFAM)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGCFAM", strRemoteServer, strRemotePort)
    AddHandler oCleCfam.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCfam.Init(oApp, oScript, oMenu.oCleComm, "TABCFAM", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
      oCleCfam.IsNuovaAnalitica = True
      strNomeReport = "BSMGCFA1"
    End If
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

      grvCfam.NTSSetParam(oMenu, oApp.Tr(Me, 129138343800717110, "Linee/Famiglie prodotti"))

      tb_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128490898873444000, "Codice"), tabcfam, False)
      tb_descfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128490898873600000, "Descrizione"), 50, False)
      tb_unmisfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128490898873756000, "UM"), 3, True)
      tb_descfa2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129138959455270488, "Descrizione 2da parte"), 40, True)
      tb_coddica.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129138959394798255, "Dimensione C.A."), tabdica, False)
      xx_coddica.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129138959372296959, "Descrizione Dimensione C.A."), 40, True)
      tb_coddicv.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129138959350264440, "Valore dimensione C.A."), 12, False)
      xx_coddicv.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129138959323387892, "Descrizione Valore dimensione C.A."), 40)
      tb_codtcdc.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129138959279635372, "Tipologia linea\prodotto"), tabtcdc)
      xx_codtcdc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129138959301667891, "Descrizione Tipologia linea\prodotto"), 30)


      tb_codcfam.NTSSetRichiesto()

      tb_coddica.NTSSetRichiesto()
      tb_coddicv.NTSSetRichiesto()
      tb_codtcdc.NTSSetRichiesto()

      tb_codcfam.NTSSetParamZoom("")
      tb_coddicv.NTSSetParamZoom("..")

      tb_coddica.Visible = oCleCfam.IsNuovaAnalitica
      tb_coddicv.Visible = oCleCfam.IsNuovaAnalitica
      tb_codtcdc.Visible = oCleCfam.IsNuovaAnalitica
      xx_coddica.Visible = oCleCfam.IsNuovaAnalitica
      xx_coddicv.Visible = oCleCfam.IsNuovaAnalitica
      xx_codtcdc.Visible = oCleCfam.IsNuovaAnalitica

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

#Region "Eventi di Form"

  Public Overridable Sub FRMMGCFAM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleCfam.Apri(DittaCorrente, dsCfam) Then Me.Close()
      dcCfam.DataSource = dsCfam.Tables("TABCFAM")
      dsCfam.AcceptChanges()

      grCfam.DataSource = dcCfam
      checkCoddicaEnabled()
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvCfam.NTSAllowInsert Then
            grvCfam.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcCfam.List.Count - 1
            If CType(dcCfam.Item(i), DataRowView)!tb_codcfam.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcCfam.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then
      grvCfam.MoveLast()

      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupBUD) Then
        tb_coddicv.Visible = False
        tb_coddicv.Enabled = False
        xx_coddicv.Visible = False
        tb_coddica.Visible = False
        tb_coddica.Enabled = False
        xx_coddica.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGCFAM_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGCFAM_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCfam.Dispose()
      dsCfam.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCfam.NTSNuovo()

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
      If Not grvCfam.NTSDeleteRigaCorrente(dcCfam, True) Then Return
      oCleCfam.Salva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvCfam.NTSRipristinaRigaCorrenteBefore(dcCfam, True) Then Return
      oCleCfam.Ripristina(dcCfam.Position, dcCfam.Filter)
      grvCfam.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing

    Try
      SetFastZoom(NTSCStr(grvCfam.EditingValue), oParam)    'abilito la gestione dello zoom veloce
      NTSZOOM.strIn = NTSCStr(grvCfam.EditingValue)
      '--------------------------------------------------------------------------------------------------------------
      If grvCfam.FocusedColumn.Name = "tb_coddicv" Then
        oParam.strCodice = NTSCStr(grvCfam.GetFocusedRowCellValue("tb_coddica"))
        NTSZOOM.ZoomStrIn("ZOOMTABDICV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCfam.EditingValue) Then grvCfam.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvCfam.FocusedColumn.Name = "tb_coddica" Then
        oParam.strTipo = TIPO_ENTITY
        NTSZOOM.ZoomStrIn("ZOOMTABDICA", "", oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCfam.EditingValue) Then grvCfam.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvCfam.FocusedColumn.Name = "tb_codtcdc" Then
        oParam.strTipo = TIPO_ENTITY
        NTSZOOM.ZoomStrIn("ZOOMTABTCDC", "", oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCfam.EditingValue) Then grvCfam.SetFocusedValue(NTSZOOM.strIn)
      Else
        NTSCallStandardZoom()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

  Public Overridable Sub tlbDesLingua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDesLingua.ItemClick
    Dim frmFali As FRM__FALI = Nothing

    Try
      If Not Salva() Then Return
      If grvCfam.NTSGetCurrentDataRow() Is Nothing Then Return

      oCleCfam.strCodCfam = NTSCStr(grvCfam.NTSGetCurrentDataRow!tb_codcfam)
      frmFali = CType(NTSNewFormModal("FRM__FALI"), FRM__FALI)
      If Not frmFali.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmFali.InitEntity(oCleCfam)
      frmFali.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmFali Is Nothing Then frmFali.Dispose()
      frmFali = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub grvCfam_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCfam.NTSBeforeRowUpdate
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
      dRes = grvCfam.NTSSalvaRigaCorrente(dcCfam, oCleCfam.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleCfam.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleCfam.Ripristina(dcCfam.Position, dcCfam.Filter)
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
      strCrpe = "{tabcfam.codditt} = '" & DittaCorrente & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGCFAM", "Reports1", " ", 0, nDestin, strNomeReport & ".RPT", False, "FAMIGLIE ARTICOLI", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvCfam_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCfam.NTSCellValueChanged
    If e.Column.Equals(tb_coddica) Then
      checkCoddicaEnabled()
    End If
  End Sub

  Public Overridable Sub grvCfam_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvCfam.NTSFocusedRowChanged
    Try
      If grvCfam.GetFocusedRowCellValue(tb_codcfam).ToString.Trim.Length <> 0 Then
        tb_codcfam.Enabled = False
      Else
        tb_codcfam.Enabled = True
      End If
      checkCoddicaEnabled()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub checkCoddicaEnabled()
    If grvCfam.GetFocusedRowCellValue(tb_coddica).ToString.Trim.Length = 0 Then
      tb_coddicv.Enabled = False
    Else
      tb_coddicv.Enabled = True
    End If
  End Sub

End Class
