Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ELSE

#Region "Variabili"
  Public oCleLsel As CLE__LSEL
  Public oCallParams As CLE__CLDP
  Public dsElse As DataSet
  Public dcElse As BindingSource = New BindingSource()

  Public strTipocle As String

  Public bConsentiModifica As Boolean = False

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
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grElse As NTSInformatica.NTSGrid
  Public WithEvents grvElse As NTSInformatica.NTSGridView
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents lse_tipolc As NTSInformatica.NTSGridColumn
  Public WithEvents lse_conto As NTSInformatica.NTSGridColumn
  Public WithEvents lse_codlead As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_citta As NTSInformatica.NTSGridColumn
  Public WithEvents xx_telef As NTSInformatica.NTSGridColumn
  Public WithEvents xx_faxtlx As NTSInformatica.NTSGridColumn
  Public WithEvents xx_email As NTSInformatica.NTSGridColumn
  Public WithEvents lse_note As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents lse_codlsel As NTSInformatica.NTSGridColumn
  Public WithEvents xx_selriga As NTSInformatica.NTSGridColumn
  Public WithEvents tlbRigheSel As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRigheDesel As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRigheSelCancella As NTSInformatica.NTSBarButtonItem
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ELSE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grElse = New NTSInformatica.NTSGrid
    Me.grvElse = New NTSInformatica.NTSGridView
    Me.xx_selriga = New NTSInformatica.NTSGridColumn
    Me.lse_tipolc = New NTSInformatica.NTSGridColumn
    Me.lse_conto = New NTSInformatica.NTSGridColumn
    Me.lse_codlead = New NTSInformatica.NTSGridColumn
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn
    Me.xx_citta = New NTSInformatica.NTSGridColumn
    Me.xx_telef = New NTSInformatica.NTSGridColumn
    Me.xx_faxtlx = New NTSInformatica.NTSGridColumn
    Me.xx_email = New NTSInformatica.NTSGridColumn
    Me.lse_note = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.lse_codlsel = New NTSInformatica.NTSGridColumn
    Me.lse_ogprogr = New NTSInformatica.NTSGridColumn
    Me.xx_emailc = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.tlbRigheSel = New NTSInformatica.NTSBarButtonItem
    Me.tlbRigheDesel = New NTSInformatica.NTSBarButtonItem
    Me.tlbRigheSelCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbLocalizzaGoogle = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grElse, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvElse, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaWord, Me.tlbDuplica, Me.tlbLocalizzaGoogle, Me.tlbRigheSel, Me.tlbRigheDesel, Me.tlbRigheSelCancella})
    Me.NtsBarManager1.MaxItemId = 25
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLocalizzaGoogle, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 20
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRigheSel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRigheDesel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRigheSelCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
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
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.Id = 17
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
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
    'grElse
    '
    Me.grElse.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grElse.EmbeddedNavigator.Name = ""
    Me.grElse.Location = New System.Drawing.Point(0, 0)
    Me.grElse.MainView = Me.grvElse
    Me.grElse.Name = "grElse"
    Me.grElse.Size = New System.Drawing.Size(648, 358)
    Me.grElse.TabIndex = 5
    Me.grElse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvElse})
    '
    'grvElse
    '
    Me.grvElse.ActiveFilterEnabled = False
    Me.grvElse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lse_tipolc, Me.xx_selriga, Me.lse_conto, Me.lse_codlead, Me.xx_descr1, Me.xx_citta, Me.xx_telef, Me.xx_faxtlx, Me.xx_email, Me.lse_note, Me.codditt, Me.lse_codlsel, Me.lse_ogprogr, Me.xx_emailc})
    Me.grvElse.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvElse.Enabled = True
    Me.grvElse.GridControl = Me.grElse
    Me.grvElse.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvElse.Name = "grvElse"
    Me.grvElse.NTSAllowDelete = True
    Me.grvElse.NTSAllowInsert = True
    Me.grvElse.NTSAllowUpdate = True
    Me.grvElse.NTSMenuContext = Nothing
    Me.grvElse.OptionsCustomization.AllowRowSizing = True
    Me.grvElse.OptionsFilter.AllowFilterEditor = False
    Me.grvElse.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvElse.OptionsNavigation.UseTabKey = False
    Me.grvElse.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvElse.OptionsView.ColumnAutoWidth = False
    Me.grvElse.OptionsView.EnableAppearanceEvenRow = True
    Me.grvElse.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvElse.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvElse.OptionsView.ShowGroupPanel = False
    Me.grvElse.RowHeight = 16
    '
    'xx_selriga
    '
    Me.xx_selriga.AppearanceCell.Options.UseBackColor = True
    Me.xx_selriga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_selriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_selriga.Caption = "Sel."
    Me.xx_selriga.Enabled = True
    Me.xx_selriga.FieldName = "xx_selriga"
    Me.xx_selriga.Name = "xx_selriga"
    Me.xx_selriga.NTSRepositoryComboBox = Nothing
    Me.xx_selriga.NTSRepositoryItemCheck = Nothing
    Me.xx_selriga.NTSRepositoryItemMemo = Nothing
    Me.xx_selriga.NTSRepositoryItemText = Nothing
    Me.xx_selriga.Visible = True
    Me.xx_selriga.VisibleIndex = 0
    '
    'lse_tipolc
    '
    Me.lse_tipolc.AppearanceCell.Options.UseBackColor = True
    Me.lse_tipolc.AppearanceCell.Options.UseTextOptions = True
    Me.lse_tipolc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_tipolc.Caption = "lse_tipolc"
    Me.lse_tipolc.Enabled = False
    Me.lse_tipolc.FieldName = "lse_tipolc"
    Me.lse_tipolc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_tipolc.Name = "lse_tipolc"
    Me.lse_tipolc.NTSRepositoryComboBox = Nothing
    Me.lse_tipolc.NTSRepositoryItemCheck = Nothing
    Me.lse_tipolc.NTSRepositoryItemMemo = Nothing
    Me.lse_tipolc.NTSRepositoryItemText = Nothing
    Me.lse_tipolc.OptionsColumn.AllowEdit = False
    Me.lse_tipolc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_tipolc.OptionsColumn.ReadOnly = True
    Me.lse_tipolc.OptionsFilter.AllowFilter = False
    '
    'lse_conto
    '
    Me.lse_conto.AppearanceCell.Options.UseBackColor = True
    Me.lse_conto.AppearanceCell.Options.UseTextOptions = True
    Me.lse_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_conto.Caption = "Conto"
    Me.lse_conto.Enabled = True
    Me.lse_conto.FieldName = "lse_conto"
    Me.lse_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_conto.Name = "lse_conto"
    Me.lse_conto.NTSRepositoryComboBox = Nothing
    Me.lse_conto.NTSRepositoryItemCheck = Nothing
    Me.lse_conto.NTSRepositoryItemMemo = Nothing
    Me.lse_conto.NTSRepositoryItemText = Nothing
    Me.lse_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_conto.OptionsFilter.AllowFilter = False
    Me.lse_conto.Visible = True
    Me.lse_conto.VisibleIndex = 1
    '
    'lse_codlead
    '
    Me.lse_codlead.AppearanceCell.Options.UseBackColor = True
    Me.lse_codlead.AppearanceCell.Options.UseTextOptions = True
    Me.lse_codlead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_codlead.Caption = "Codice Lead"
    Me.lse_codlead.Enabled = True
    Me.lse_codlead.FieldName = "lse_codlead"
    Me.lse_codlead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_codlead.Name = "lse_codlead"
    Me.lse_codlead.NTSRepositoryComboBox = Nothing
    Me.lse_codlead.NTSRepositoryItemCheck = Nothing
    Me.lse_codlead.NTSRepositoryItemMemo = Nothing
    Me.lse_codlead.NTSRepositoryItemText = Nothing
    Me.lse_codlead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_codlead.OptionsFilter.AllowFilter = False
    Me.lse_codlead.Visible = True
    Me.lse_codlead.VisibleIndex = 2
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Ragione sociale/descr."
    Me.xx_descr1.Enabled = False
    Me.xx_descr1.FieldName = "xx_descr1"
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.NTSRepositoryComboBox = Nothing
    Me.xx_descr1.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1.NTSRepositoryItemText = Nothing
    Me.xx_descr1.OptionsColumn.AllowEdit = False
    Me.xx_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1.OptionsColumn.ReadOnly = True
    Me.xx_descr1.OptionsFilter.AllowFilter = False
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 3
    '
    'xx_citta
    '
    Me.xx_citta.AppearanceCell.Options.UseBackColor = True
    Me.xx_citta.AppearanceCell.Options.UseTextOptions = True
    Me.xx_citta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_citta.Caption = "Città"
    Me.xx_citta.Enabled = False
    Me.xx_citta.FieldName = "xx_citta"
    Me.xx_citta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_citta.Name = "xx_citta"
    Me.xx_citta.NTSRepositoryComboBox = Nothing
    Me.xx_citta.NTSRepositoryItemCheck = Nothing
    Me.xx_citta.NTSRepositoryItemMemo = Nothing
    Me.xx_citta.NTSRepositoryItemText = Nothing
    Me.xx_citta.OptionsColumn.AllowEdit = False
    Me.xx_citta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_citta.OptionsColumn.ReadOnly = True
    Me.xx_citta.OptionsFilter.AllowFilter = False
    Me.xx_citta.Visible = True
    Me.xx_citta.VisibleIndex = 4
    '
    'xx_telef
    '
    Me.xx_telef.AppearanceCell.Options.UseBackColor = True
    Me.xx_telef.AppearanceCell.Options.UseTextOptions = True
    Me.xx_telef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_telef.Caption = "Telefono"
    Me.xx_telef.Enabled = False
    Me.xx_telef.FieldName = "xx_telef"
    Me.xx_telef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_telef.Name = "xx_telef"
    Me.xx_telef.NTSRepositoryComboBox = Nothing
    Me.xx_telef.NTSRepositoryItemCheck = Nothing
    Me.xx_telef.NTSRepositoryItemMemo = Nothing
    Me.xx_telef.NTSRepositoryItemText = Nothing
    Me.xx_telef.OptionsColumn.AllowEdit = False
    Me.xx_telef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_telef.OptionsColumn.ReadOnly = True
    Me.xx_telef.OptionsFilter.AllowFilter = False
    Me.xx_telef.Visible = True
    Me.xx_telef.VisibleIndex = 5
    '
    'xx_faxtlx
    '
    Me.xx_faxtlx.AppearanceCell.Options.UseBackColor = True
    Me.xx_faxtlx.AppearanceCell.Options.UseTextOptions = True
    Me.xx_faxtlx.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_faxtlx.Caption = "Fax"
    Me.xx_faxtlx.Enabled = False
    Me.xx_faxtlx.FieldName = "xx_faxtlx"
    Me.xx_faxtlx.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_faxtlx.Name = "xx_faxtlx"
    Me.xx_faxtlx.NTSRepositoryComboBox = Nothing
    Me.xx_faxtlx.NTSRepositoryItemCheck = Nothing
    Me.xx_faxtlx.NTSRepositoryItemMemo = Nothing
    Me.xx_faxtlx.NTSRepositoryItemText = Nothing
    Me.xx_faxtlx.OptionsColumn.AllowEdit = False
    Me.xx_faxtlx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_faxtlx.OptionsColumn.ReadOnly = True
    Me.xx_faxtlx.OptionsFilter.AllowFilter = False
    Me.xx_faxtlx.Visible = True
    Me.xx_faxtlx.VisibleIndex = 6
    '
    'xx_email
    '
    Me.xx_email.AppearanceCell.Options.UseBackColor = True
    Me.xx_email.AppearanceCell.Options.UseTextOptions = True
    Me.xx_email.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_email.Caption = "E-mail"
    Me.xx_email.Enabled = False
    Me.xx_email.FieldName = "xx_email"
    Me.xx_email.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_email.Name = "xx_email"
    Me.xx_email.NTSRepositoryComboBox = Nothing
    Me.xx_email.NTSRepositoryItemCheck = Nothing
    Me.xx_email.NTSRepositoryItemMemo = Nothing
    Me.xx_email.NTSRepositoryItemText = Nothing
    Me.xx_email.OptionsColumn.AllowEdit = False
    Me.xx_email.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_email.OptionsColumn.ReadOnly = True
    Me.xx_email.OptionsFilter.AllowFilter = False
    Me.xx_email.Visible = True
    Me.xx_email.VisibleIndex = 7
    '
    'lse_note
    '
    Me.lse_note.AppearanceCell.Options.UseBackColor = True
    Me.lse_note.AppearanceCell.Options.UseTextOptions = True
    Me.lse_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_note.Caption = "Note"
    Me.lse_note.Enabled = True
    Me.lse_note.FieldName = "lse_note"
    Me.lse_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_note.Name = "lse_note"
    Me.lse_note.NTSRepositoryComboBox = Nothing
    Me.lse_note.NTSRepositoryItemCheck = Nothing
    Me.lse_note.NTSRepositoryItemMemo = Nothing
    Me.lse_note.NTSRepositoryItemText = Nothing
    Me.lse_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_note.OptionsFilter.AllowFilter = False
    Me.lse_note.Visible = True
    Me.lse_note.VisibleIndex = 8
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
    'lse_codlsel
    '
    Me.lse_codlsel.AppearanceCell.Options.UseBackColor = True
    Me.lse_codlsel.AppearanceCell.Options.UseTextOptions = True
    Me.lse_codlsel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_codlsel.Caption = "lse_codlsel"
    Me.lse_codlsel.Enabled = False
    Me.lse_codlsel.FieldName = "lse_codlsel"
    Me.lse_codlsel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_codlsel.Name = "lse_codlsel"
    Me.lse_codlsel.NTSRepositoryComboBox = Nothing
    Me.lse_codlsel.NTSRepositoryItemCheck = Nothing
    Me.lse_codlsel.NTSRepositoryItemMemo = Nothing
    Me.lse_codlsel.NTSRepositoryItemText = Nothing
    Me.lse_codlsel.OptionsColumn.AllowEdit = False
    Me.lse_codlsel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_codlsel.OptionsColumn.ReadOnly = True
    Me.lse_codlsel.OptionsFilter.AllowFilter = False
    '
    'lse_ogprogr
    '
    Me.lse_ogprogr.AppearanceCell.Options.UseBackColor = True
    Me.lse_ogprogr.AppearanceCell.Options.UseTextOptions = True
    Me.lse_ogprogr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lse_ogprogr.Caption = "Contatto"
    Me.lse_ogprogr.Enabled = True
    Me.lse_ogprogr.FieldName = "lse_ogprogr"
    Me.lse_ogprogr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lse_ogprogr.Name = "lse_ogprogr"
    Me.lse_ogprogr.NTSRepositoryComboBox = Nothing
    Me.lse_ogprogr.NTSRepositoryItemCheck = Nothing
    Me.lse_ogprogr.NTSRepositoryItemMemo = Nothing
    Me.lse_ogprogr.NTSRepositoryItemText = Nothing
    Me.lse_ogprogr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lse_ogprogr.OptionsFilter.AllowFilter = False
    Me.lse_ogprogr.Visible = True
    Me.lse_ogprogr.VisibleIndex = 9
    '
    'xx_emailc
    '
    Me.xx_emailc.AppearanceCell.Options.UseBackColor = True
    Me.xx_emailc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_emailc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_emailc.Caption = "E-mail contatto"
    Me.xx_emailc.Enabled = False
    Me.xx_emailc.FieldName = "xx_emailc"
    Me.xx_emailc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_emailc.Name = "xx_emailc"
    Me.xx_emailc.NTSRepositoryComboBox = Nothing
    Me.xx_emailc.NTSRepositoryItemCheck = Nothing
    Me.xx_emailc.NTSRepositoryItemMemo = Nothing
    Me.xx_emailc.NTSRepositoryItemText = Nothing
    Me.xx_emailc.OptionsColumn.AllowEdit = False
    Me.xx_emailc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_emailc.OptionsColumn.ReadOnly = True
    Me.xx_emailc.OptionsFilter.AllowFilter = False
    Me.xx_emailc.Visible = True
    Me.xx_emailc.VisibleIndex = 10
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.pnRight)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 388)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(648, 54)
    Me.pnBottom.TabIndex = 7
    Me.pnBottom.Text = "NtsPanel1"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cmdCancella)
    Me.pnRight.Controls.Add(Me.cmdSeleziona)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(418, 0)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.Size = New System.Drawing.Size(230, 54)
    Me.pnRight.TabIndex = 2
    Me.pnRight.Text = "NtsPanel1"
    '
    'cmdCancella
    '
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(116, 12)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.Size = New System.Drawing.Size(102, 30)
    Me.cmdCancella.TabIndex = 1
    Me.cmdCancella.Text = "&Cancella tutto"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(8, 12)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(102, 30)
    Me.cmdSeleziona.TabIndex = 0
    Me.cmdSeleziona.Text = "&Seleziona Articoli"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.grElse)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(648, 358)
    Me.pnTop.TabIndex = 8
    Me.pnTop.Text = "NtsPanel1"
    '
    'tlbLocalizzaGoogle
    '
    Me.tlbLocalizzaGoogle.Caption = "Localizza con Google"
    Me.tlbLocalizzaGoogle.Glyph = CType(resources.GetObject("tlbLocalizzaGoogle.Glyph"), System.Drawing.Image)
    Me.tlbLocalizzaGoogle.Id = 24
    Me.tlbLocalizzaGoogle.Name = "tlbLocalizzaGoogle"
    Me.tlbLocalizzaGoogle.Visible = True
    '
    'tlbRigheSel
    '
    Me.tlbRigheSel.Caption = "Seleziona tutte le righe"
    Me.tlbRigheSel.Id = 21
    Me.tlbRigheSel.Name = "tlbRigheSel"
    Me.tlbRigheSel.Visible = True
    '
    'tlbRigheDesel
    '
    Me.tlbRigheDesel.Caption = "Deseleziona tutte le righe"
    Me.tlbRigheDesel.Id = 22
    Me.tlbRigheDesel.Name = "tlbRigheDesel"
    Me.tlbRigheDesel.Visible = True
    '
    'tlbRigheSelCancella
    '
    Me.tlbRigheSelCancella.Caption = "Cancella le righe selezionate"
    Me.tlbRigheSelCancella.Id = 23
    Me.tlbRigheSelCancella.Name = "tlbRigheSelCancella"
    Me.tlbRigheSelCancella.Visible = True
    '
    'FRM__ELSE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__ELSE"
    Me.NTSLastControlFocussed = Me.grElse
    Me.Text = "LISTA SELEZIONATA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grElse, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvElse, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleLsel As CLE__LSEL)
    oCleLsel = cleLsel
    AddHandler oCleLsel.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttLse_tipolc As New DataTable
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
        tlbLocalizzaGoogle.GlyphPath = (oApp.ChildImageDir & "\zona.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttLse_tipolc.Columns.Add("cod", GetType(String))
      dttLse_tipolc.Columns.Add("val", GetType(String))
      dttLse_tipolc.Rows.Add(New Object() {"C", "Conto"})
      dttLse_tipolc.Rows.Add(New Object() {"L", "Lead"})
      dttLse_tipolc.AcceptChanges()

      grvElse.NTSSetParam(oMenu, "LISTA SELEZIONATA")

      lse_tipolc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128969763461502070, "lse_tipolc"), dttLse_tipolc, "cod", "val")
      lse_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128969763475134010, "Conto"), tabanagra)
      lse_codlead.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128969763501206560, "Codice Lead"), tableads)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763523773475, "Ragione sociale/descr."), 0, True)
      xx_citta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763545744725, "Città"), 0, True)
      xx_telef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763560294575, "Telefono"), 0, True)
      xx_faxtlx.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763574746775, "Fax"), 0, True)
      xx_email.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763588105295, "E-mail"), 0, True)
      lse_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763601454050, "Note"), 50, True)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128969763614451265, "codditt"), 12, False)
      lse_codlsel.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128969763632809465, "lse_codlsel"), "0", 4, 0, 9999)
      lse_ogprogr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129375920677464918, "Contatto"), "0", 9, 0, 999999999)
      xx_emailc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129375920677621179, "E-mail contatto"), 0, True)
      xx_selriga.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129040481695264040, "Sel."), "S", "N")

      lse_ogprogr.NTSForzaVisZoom = True

      lse_conto.NTSSetRichiesto()
      lse_codlead.NTSSetRichiesto()

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
  Public Overridable Sub FRM__ELSE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------      
      oCleLsel.bGestAnaExtAnagra = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaext", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      '--- Legge sempre la stessa opzione (ujnica impostazione per clienti e leads, ma anche due record su anaext)
      '--------------------------------------------------------------------------------------------------------------
      oCleLsel.bGestAnaExtLeads = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaext", "0", " ", "0"))
      oCleLsel.lModuliDittaDitt = oMenu.ModuliDittaDitt(DittaCorrente)
      oCleLsel.lModuliExtDittaDitt = oMenu.ModuliExtDittaDitt(DittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
      Apri()
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleLsel.strElseTipocl
        Case "C" : cmdSeleziona.Text = oApp.Tr(Me, 130421078150425928, "&Seleziona conti")
        Case "L" : cmdSeleziona.Text = oApp.Tr(Me, 130421078268708692, "&Seleziona leads")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If bConsentiModifica = False Then
        grvElse.NTSAllowDelete = False
        grvElse.NTSAllowInsert = False
        grvElse.NTSAllowUpdate = False
        tlbNuovo.Enabled = False
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbZoom.Enabled = False
        cmdSeleziona.Enabled = False
        cmdCancella.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbLocalizzaGoogle.Visible = False
        lse_ogprogr.Visible = False
        xx_emailc.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__ELSE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__ELSE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcElse.Dispose()
      dsElse.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oCleLsel.strWhereAnagra = ""
      oCleLsel.strWhereLeads = ""

      Select Case oCleLsel.strElseTipocl
        Case "C"
          oPar = New CLE__PATB
          oPar.bTipoProposto = True
          oPar.strTipo = "C"
          oPar.bVisGriglia = False
          NTSZOOM.strIn = ""
          NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oPar)
          oCleLsel.strWhereAnagra = oPar.strOut.Trim

          If oCleLsel.strWhereAnagra = "" Then Exit Sub
        Case "L"
          oPar = New CLE__PATB
          oPar.bVisGriglia = False
          NTSZOOM.strIn = ""
          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oPar)
          oCleLsel.strWhereLeads = oPar.strOut.Trim

          If oCleLsel.strWhereLeads = "" Then Exit Sub
      End Select

      Seleziona()

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Dim dRes As DialogResult
    Try
      Salva()

      If dsElse.Tables("LISTSEL").Rows.Count = 0 Then Return

      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128522901675202696, "Cancellare tutti i records ?"))
      If Not dRes = System.Windows.Forms.DialogResult.Yes Then Exit Sub

      If Not oCleLsel.DeleteListsel(oCleLsel.strCodLSel) Then Exit Sub

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvElse.NTSNuovo()

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
      If Not grvElse.NTSDeleteRigaCorrente(dcElse, True) Then Return
      oCleLsel.ElseSalva(True)
      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvElse.NTSRipristinaRigaCorrenteBefore(dcElse, True) Then Return
      oCleLsel.ElseRipristina(dcElse.Position, dcElse.Filter)
      grvElse.NTSRipristinaRigaCorrenteAfter()
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

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grElse.Focused And grvElse.FocusedColumn.Name.ToLower = "lse_conto" Then
          'zoom su anagra
          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.bVisGriglia = True
          oParam.bTipoProposto = True
          NTSZOOM.strIn = NTSCStr(grvElse.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvElse.EditingValue) Then grvElse.SetFocusedValue(NTSZOOM.strIn)
          Return
        ElseIf grElse.Focused And grvElse.FocusedColumn.Name.ToLower = "lse_codlead" Then
          'zoom su leads
          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strIn = ""
          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvElse.EditingValue) Then grvElse.SetFocusedValue(NTSZOOM.strIn)
          Return
        ElseIf grElse.Focused And grvElse.FocusedColumn.Name.ToLower = "lse_ogprogr" Then
          If grvElse.NTSGetCurrentDataRow Is Nothing Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130868710150242632, "Prima indicare il conto\lead"))
            Return
          End If

          SetFastZoom(NTSCStr(grvElse.GetFocusedValue), oParam)    '--- Abilito la gestione dello zoom veloce
          NTSZOOM.strIn = NTSCStr(grvElse.GetFocusedValue)

          Select Case oCleLsel.strElseTipocl
            Case "L"
              oParam.strTipo = "A"
              oParam.lCommessa = NTSCInt(grvElse.NTSGetCurrentDataRow!lse_codlead)
              oParam.lContoCF = 0
            Case Else
              oParam.strTipo = "C"
              oParam.lCommessa = 0
              oParam.lContoCF = NTSCInt(grvElse.NTSGetCurrentDataRow!lse_conto)
          End Select

          NTSZOOM.ZoomStrIn("ZOOMORGANIG", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvElse.GetFocusedValue) Then
            grvElse.SetFocusedValue(NTSZOOM.strIn)
          End If
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

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Try
      '-----------------------
      'faccio comporre la query al dl
      strQueryWord = oCleLsel.GetQueryStampaWord(oCleLsel.strCodLSel)
      If strQueryWord = "" Then Return

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BN__LSEL"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = NTSCStr(oCleLsel.lCodorgaOperat)
      oPar.strPar4 = oCleLsel.strElseTipocl
      oPar.strPar5 = oCleLsel.strCodLSel
      oPar.bPar1 = oCleLsel.bGestAnaExtAnagra
      oPar.bPar2 = oCleLsel.bGestAnaExtLeads

      oMenu.RunChild("NTSInformatica", "FRM__STW2", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbLocalizzaGoogle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLocalizzaGoogle.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      dttTmp.Columns.Add("xx_sel", GetType(String))
      dttTmp.Columns.Add("xx_order", GetType(Integer))
      dttTmp.Columns.Add("xx_tipo", GetType(String))
      dttTmp.Columns.Add("xx_conto", GetType(Integer))
      dttTmp.Columns.Add("xx_coddest", GetType(Integer))
      dttTmp.TableName = "ANAGRAFICHE"

      For Each dtrT As DataRow In dsElse.Tables("LISTSEL").Select("", dcElse.Sort)
        If NTSCInt(dtrT!lse_codlead) <> 0 Then
          If dttTmp.Select("xx_tipo = 'L' AND xx_conto = " & dtrT!lse_codlead.ToString).Length = 0 Then
            i += 1
            dttTmp.Rows.Add(New Object() {"S", i, "L", dtrT!lse_codlead, 0})
          End If
        Else
          If dttTmp.Select("xx_tipo = 'A' AND xx_conto = " & dtrT!lse_conto.ToString).Length = 0 Then
            i += 1
            dttTmp.Rows.Add(New Object() {"S", i, "A", dtrT!lse_conto, 0})
          End If
        End If
      Next

      oPar.ctlPar1 = dttTmp
      oMenu.RunChild("NTSInformatica", "FRM__LOCA", "", DittaCorrente, "", "BN__LOCA", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
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

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim oParam As New CLE__PATB
    Try
      oParam.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMTABLSEL", DittaCorrente, oParam)

      If NTSZOOM.strIn <> "" Then oCleLsel.Duplica(NTSZOOM.strIn)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbRigheSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRigheSel.ItemClick
    Dim i As Integer = 0
    Try
      Salva()
      For Each dtrTmp As DataRow In dsElse.Tables("LISTSEL").Rows
        dtrTmp!xx_selriga = "S"
      Next
      dsElse.Tables("LISTSEL").AcceptChanges()
      oCleLsel.bElseHasChanges = False
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRigheDesel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRigheDesel.ItemClick
    Dim i As Integer = 0
    Try
      Salva()
      For Each dtrTmp As DataRow In dsElse.Tables("LISTSEL").Rows
        dtrTmp!xx_selriga = "N"
      Next
      dsElse.Tables("LISTSEL").AcceptChanges()
      oCleLsel.bElseHasChanges = False
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRigheSelCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRigheSelCancella.ItemClick
    Dim dtrT() As DataRow = Nothing
    Dim currentRow As DataRow = Nothing
    Dim dRes As DialogResult
    Try
      dtrT = dsElse.Tables("LISTSEL").Select("xx_selriga = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129599631362153776, "Non è stata selezionata nessuna riga."))
        Return
      End If

      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129599631287151376, _
      "Cancellare le righe selezionate?"))
      If dRes = Windows.Forms.DialogResult.No Then
        Return
      End If

      Me.Cursor = Cursors.WaitCursor
      '1) Si ripristina la riga appena inserita non salvata
      If grvElse.FocusedRowHandle >= 0 Then
        currentRow = dsElse.Tables("LISTSEL").Rows(grvElse.FocusedRowHandle)
        If (currentRow!xx_selriga).Equals("S") AndAlso _
        currentRow.RowState = DataRowState.Added Then
          If Not grvElse.NTSRipristinaRigaCorrenteBefore(dcElse, False) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129599658533653863, _
            "Impossibile cancellare la riga corrente." & vbCrLf & _
            "Non è stata cancellata nessuna riga."))
          Else
            oCleLsel.Ripristina(dcElse.Position, dcElse.Filter)
            grvElse.NTSRipristinaRigaCorrenteAfter()
          End If
        End If
      End If

      '2) Si cancellano le righe salvate
      dtrT = dsElse.Tables("LISTSEL").Select("xx_selriga = 'S'")
      For Each row As DataRow In dtrT
        dcElse.RemoveAt(dsElse.Tables("LISTSEL").Rows.IndexOf(row))
        oCleLsel.ElseSalva(True)
      Next

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvElse_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvElse.NTSBeforeRowUpdate
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
  Public Overridable Sub grvElse_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvElse.NTSFocusedRowChanged
    '--- Blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsel Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      dtrT = grvElse.NTSGetCurrentDataRow
      '--------------------------------------------------------------------------------------------------------------
      '--- Sono su una nuova riga
      '--------------------------------------------------------------------------------------------------------------
      If dtrT Is Nothing Then
        lse_tipolc.Enabled = True
        lse_ogprogr.Enabled = True
        Select Case strTipocle
          Case "E"
            lse_conto.Enabled = True
            lse_codlead.Enabled = True
          Case "C" : lse_conto.Enabled = True
          Case "L" : lse_codlead.Enabled = True
        End Select
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrT!lse_conto) <> 0 Or NTSCInt(dtrT!lse_codlead) <> 0 Then
        lse_tipolc.Enabled = False
        lse_conto.Enabled = False
        lse_codlead.Enabled = False
        lse_ogprogr.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try
      dcElse.DataSource = Nothing
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleLsel.ElseApri(DittaCorrente, dsElse) Then Me.Close()
      dcElse.DataSource = dsElse.Tables("LISTSEL")
      dsElse.AcceptChanges()

      grElse.DataSource = dcElse

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-----------------------------------------------------------------------------------------
      '--- Se il esiste il codice raggruppamento nella form chiamante TABLSEL
      '--- ed esiste anche in TABRAGG e TABRAGG.tb_tipolce <> 'E', blocca le colonne relative a:
      '--- Codice Lead
      '--- Tipo
      '--- altrimenti le lascia libere
      '-----------------------------------------------------------------------------------------
      strTipocle = "E"
      If oCleLsel.nElseCodragg <> 0 Then
        oCleLsel.GetTabragg(strTipocle)
      End If
      If (strTipocle = "C") Or (strTipocle = "L") Then
        lse_tipolc.Enabled = False
        lse_tipolc.Visible = False
      End If
      Select Case oCleLsel.strElseTipocl
        Case "C"
          lse_codlead.Enabled = False
          lse_codlead.Visible = False
        Case "L"
          lse_conto.Enabled = False
          lse_conto.Visible = False
      End Select

      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvElse.NTSSalvaRigaCorrente(dcElse, oCleLsel.ElseRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleLsel.ElseSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleLsel.ElseRipristina(dcElse.Position, dcElse.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select

      AbilDisabControlli()

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
    Dim strReports As String = ""
    Dim bReportPerLista As Boolean
    Try
      bReportPerLista = CBool(oMenu.GetSettingBus("BS--LSEL", "OPZIONI", ".", "ReportPerLista", "0", " ", "0"))
      '---------------------------------
      If bReportPerLista Then
        strReports = "Reports" & oCleLsel.strCodLsel
      Else
        strReports = "Reports1"
      End If

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{listsel.codditt} = '" & DittaCorrente & "' AND {listsel.lse_codlsel} = " & oCleLsel.strCodLSel
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--ELSE", strReports, " ", 0, nDestin, "BS--ELSE.RPT", False, "LISTA SELEZIONATA", False)
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

  Public Overridable Sub AbilDisabControlli()
    Try
      If dsElse.Tables("LISTSEL").Rows.Count = 0 Then
        cmdCancella.Enabled = False
        tlbStampaVideo.Visible = False
        tlbStampa.Visible = False
        tlbStampaWord.Visible = False
        tlbImpostaStampante.Visible = False
      Else
        cmdCancella.Enabled = True
        tlbStampaVideo.Visible = True
        tlbStampa.Visible = True
        tlbStampaWord.Visible = True
        tlbImpostaStampante.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Seleziona()
    Dim bFiltraVettor As Boolean
    Dim strCodVett As String
    Try
      If oCleLsel.strWhereAnagra <> "" Then
        If oCleLsel.strWhereAnagra.ToLower.IndexOf("anaext") > -1 Then
          oCleLsel.bAnaextSelFiltri = True
        Else
          oCleLsel.bAnaextSelFiltri = False
        End If
      Else
        If oCleLsel.strWhereLeads.ToLower.IndexOf("anaext") > -1 Then
          oCleLsel.bAnaextSelFiltri = True
        Else
          oCleLsel.bAnaextSelFiltri = False
        End If
      End If
        '-----------------------------------------------------------------------------------------
        '--- Personalizzazione Unigel ------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        bFiltraVettor = CBool(oMenu.GetSettingBus("BS--LSEL", "OPZIONI", ".", "FiltraVettore", "0", " ", "0"))
        If bFiltraVettor Then
          Do
            strCodVett = oApp.InputBoxNew(oApp.Tr(Me, 128527233526456827, "Inserire il codice del vettore (0=tutti)"), "0")

            If NTSCStr(NTSCInt(strCodVett)) <> strCodVett Then oApp.MsgBoxErr(oApp.Tr(Me, 128527248043130480, "Inserire solo valori numerici"))
          Loop While NTSCStr(NTSCInt(strCodVett)) <> strCodVett
          If NTSCInt(strCodVett) <> 0 Then oCleLsel.strWhereAnagra = oCleLsel.strWhereAnagra & "§" & "an_vett = " & strCodVett
        End If

        oCleLsel.Seleziona()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class
