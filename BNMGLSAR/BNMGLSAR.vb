Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLSAR
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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

#Region "Variabili"
  Public oCleLsar As CLEMGLSAR
  Public oCallParams As CLE__CLDP
  Public dsLsar As DataSet
  Public dcLsar As BindingSource = New BindingSource()

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
  Public WithEvents grLsar As NTSInformatica.NTSGrid
  Public WithEvents grvLsar As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codlsar As NTSInformatica.NTSGridColumn
  Public WithEvents tb_deslsar As NTSInformatica.NTSGridColumn
  Public WithEvents tb_lsarnote As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dtcomp As NTSInformatica.NTSGridColumn
  Public WithEvents tb_status As NTSInformatica.NTSGridColumn
  Public WithEvents tb_nomfileinv As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codmagap As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codmagap As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDettagli As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbFileGeneraFile As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbFileAcquisisciFile As NTSInformatica.NTSBarButtonItem
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGLSAR))
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
    Me.tlbFileGeneraFile = New NTSInformatica.NTSBarButtonItem
    Me.tlbFileAcquisisciFile = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettagli = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grLsar = New NTSInformatica.NTSGrid
    Me.grvLsar = New NTSInformatica.NTSGridView
    Me.tb_codlsar = New NTSInformatica.NTSGridColumn
    Me.tb_deslsar = New NTSInformatica.NTSGridColumn
    Me.tb_lsarnote = New NTSInformatica.NTSGridColumn
    Me.tb_dtcomp = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tb_status = New NTSInformatica.NTSGridColumn
    Me.tb_nomfileinv = New NTSInformatica.NTSGridColumn
    Me.tb_codmagap = New NTSInformatica.NTSGridColumn
    Me.xx_codmagap = New NTSInformatica.NTSGridColumn
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grLsar, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLsar, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbDettagli, Me.tlbFileGeneraFile, Me.tlbFileAcquisisciFile, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 21
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFileGeneraFile, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFileAcquisisciFile), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettagli, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
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
    'tlbFileGeneraFile
    '
    Me.tlbFileGeneraFile.Caption = "Genera File"
    Me.tlbFileGeneraFile.Glyph = CType(resources.GetObject("tlbFileGeneraFile.Glyph"), System.Drawing.Image)
    Me.tlbFileGeneraFile.Id = 18
    Me.tlbFileGeneraFile.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbFileGeneraFile.Name = "tlbFileGeneraFile"
    Me.tlbFileGeneraFile.Visible = True
    '
    'tlbFileAcquisisciFile
    '
    Me.tlbFileAcquisisciFile.Caption = "Acquisisci File"
    Me.tlbFileAcquisisciFile.Glyph = CType(resources.GetObject("tlbFileAcquisisciFile.Glyph"), System.Drawing.Image)
    Me.tlbFileAcquisisciFile.Id = 19
    Me.tlbFileAcquisisciFile.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbFileAcquisisciFile.Name = "tlbFileAcquisisciFile"
    Me.tlbFileAcquisisciFile.Visible = True
    '
    'tlbDettagli
    '
    Me.tlbDettagli.Caption = "Dettagli"
    Me.tlbDettagli.Glyph = CType(resources.GetObject("tlbDettagli.Glyph"), System.Drawing.Image)
    Me.tlbDettagli.Id = 17
    Me.tlbDettagli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDettagli.Name = "tlbDettagli"
    Me.tlbDettagli.Visible = True
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
    'grLsar
    '
    Me.grLsar.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grLsar.EmbeddedNavigator.Name = ""
    Me.grLsar.Location = New System.Drawing.Point(0, 30)
    Me.grLsar.MainView = Me.grvLsar
    Me.grLsar.Name = "grLsar"
    Me.grLsar.Size = New System.Drawing.Size(648, 412)
    Me.grLsar.TabIndex = 5
    Me.grLsar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLsar})
    '
    'grvLsar
    '
    Me.grvLsar.ActiveFilterEnabled = False
    Me.grvLsar.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codlsar, Me.tb_deslsar, Me.tb_lsarnote, Me.tb_dtcomp, Me.codditt, Me.tb_status, Me.tb_nomfileinv, Me.tb_codmagap, Me.xx_codmagap})
    Me.grvLsar.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvLsar.Enabled = True
    Me.grvLsar.GridControl = Me.grLsar
    Me.grvLsar.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLsar.MinRowHeight = 14
    Me.grvLsar.Name = "grvLsar"
    Me.grvLsar.NTSAllowDelete = True
    Me.grvLsar.NTSAllowInsert = True
    Me.grvLsar.NTSAllowUpdate = True
    Me.grvLsar.NTSMenuContext = Nothing
    Me.grvLsar.OptionsCustomization.AllowRowSizing = True
    Me.grvLsar.OptionsFilter.AllowFilterEditor = False
    Me.grvLsar.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLsar.OptionsNavigation.UseTabKey = False
    Me.grvLsar.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLsar.OptionsView.ColumnAutoWidth = False
    Me.grvLsar.OptionsView.EnableAppearanceEvenRow = True
    Me.grvLsar.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLsar.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLsar.OptionsView.ShowGroupPanel = False
    Me.grvLsar.RowHeight = 16
    '
    'tb_codlsar
    '
    Me.tb_codlsar.AppearanceCell.Options.UseBackColor = True
    Me.tb_codlsar.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codlsar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codlsar.Caption = "Codice"
    Me.tb_codlsar.Enabled = True
    Me.tb_codlsar.FieldName = "tb_codlsar"
    Me.tb_codlsar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codlsar.Name = "tb_codlsar"
    Me.tb_codlsar.NTSRepositoryComboBox = Nothing
    Me.tb_codlsar.NTSRepositoryItemCheck = Nothing
    Me.tb_codlsar.NTSRepositoryItemMemo = Nothing
    Me.tb_codlsar.NTSRepositoryItemText = Nothing
    Me.tb_codlsar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codlsar.OptionsFilter.AllowFilter = False
    Me.tb_codlsar.Visible = True
    Me.tb_codlsar.VisibleIndex = 0
    Me.tb_codlsar.Width = 70
    '
    'tb_deslsar
    '
    Me.tb_deslsar.AppearanceCell.Options.UseBackColor = True
    Me.tb_deslsar.AppearanceCell.Options.UseTextOptions = True
    Me.tb_deslsar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_deslsar.Caption = "Descrizione"
    Me.tb_deslsar.Enabled = True
    Me.tb_deslsar.FieldName = "tb_deslsar"
    Me.tb_deslsar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_deslsar.Name = "tb_deslsar"
    Me.tb_deslsar.NTSRepositoryComboBox = Nothing
    Me.tb_deslsar.NTSRepositoryItemCheck = Nothing
    Me.tb_deslsar.NTSRepositoryItemMemo = Nothing
    Me.tb_deslsar.NTSRepositoryItemText = Nothing
    Me.tb_deslsar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_deslsar.OptionsFilter.AllowFilter = False
    Me.tb_deslsar.Visible = True
    Me.tb_deslsar.VisibleIndex = 1
    Me.tb_deslsar.Width = 70
    '
    'tb_lsarnote
    '
    Me.tb_lsarnote.AppearanceCell.Options.UseBackColor = True
    Me.tb_lsarnote.AppearanceCell.Options.UseTextOptions = True
    Me.tb_lsarnote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_lsarnote.Caption = "Note"
    Me.tb_lsarnote.Enabled = True
    Me.tb_lsarnote.FieldName = "tb_lsarnote"
    Me.tb_lsarnote.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_lsarnote.Name = "tb_lsarnote"
    Me.tb_lsarnote.NTSRepositoryComboBox = Nothing
    Me.tb_lsarnote.NTSRepositoryItemCheck = Nothing
    Me.tb_lsarnote.NTSRepositoryItemMemo = Nothing
    Me.tb_lsarnote.NTSRepositoryItemText = Nothing
    Me.tb_lsarnote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_lsarnote.OptionsFilter.AllowFilter = False
    Me.tb_lsarnote.Visible = True
    Me.tb_lsarnote.VisibleIndex = 2
    Me.tb_lsarnote.Width = 70
    '
    'tb_dtcomp
    '
    Me.tb_dtcomp.AppearanceCell.Options.UseBackColor = True
    Me.tb_dtcomp.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dtcomp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dtcomp.Caption = "Data compilazione"
    Me.tb_dtcomp.Enabled = True
    Me.tb_dtcomp.FieldName = "tb_dtcomp"
    Me.tb_dtcomp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dtcomp.Name = "tb_dtcomp"
    Me.tb_dtcomp.NTSRepositoryComboBox = Nothing
    Me.tb_dtcomp.NTSRepositoryItemCheck = Nothing
    Me.tb_dtcomp.NTSRepositoryItemMemo = Nothing
    Me.tb_dtcomp.NTSRepositoryItemText = Nothing
    Me.tb_dtcomp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dtcomp.OptionsFilter.AllowFilter = False
    Me.tb_dtcomp.Visible = True
    Me.tb_dtcomp.VisibleIndex = 3
    Me.tb_dtcomp.Width = 70
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. Ditta"
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
    Me.codditt.Width = 70
    '
    'tb_status
    '
    Me.tb_status.AppearanceCell.Options.UseBackColor = True
    Me.tb_status.AppearanceCell.Options.UseTextOptions = True
    Me.tb_status.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_status.Caption = "Status"
    Me.tb_status.Enabled = True
    Me.tb_status.FieldName = "tb_status"
    Me.tb_status.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_status.Name = "tb_status"
    Me.tb_status.NTSRepositoryComboBox = Nothing
    Me.tb_status.NTSRepositoryItemCheck = Nothing
    Me.tb_status.NTSRepositoryItemMemo = Nothing
    Me.tb_status.NTSRepositoryItemText = Nothing
    Me.tb_status.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_status.OptionsFilter.AllowFilter = False
    Me.tb_status.Visible = True
    Me.tb_status.VisibleIndex = 4
    Me.tb_status.Width = 70
    '
    'tb_nomfileinv
    '
    Me.tb_nomfileinv.AppearanceCell.Options.UseBackColor = True
    Me.tb_nomfileinv.AppearanceCell.Options.UseTextOptions = True
    Me.tb_nomfileinv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_nomfileinv.Caption = "Nome file inventario"
    Me.tb_nomfileinv.Enabled = False
    Me.tb_nomfileinv.FieldName = "tb_nomfileinv"
    Me.tb_nomfileinv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_nomfileinv.Name = "tb_nomfileinv"
    Me.tb_nomfileinv.NTSRepositoryComboBox = Nothing
    Me.tb_nomfileinv.NTSRepositoryItemCheck = Nothing
    Me.tb_nomfileinv.NTSRepositoryItemMemo = Nothing
    Me.tb_nomfileinv.NTSRepositoryItemText = Nothing
    Me.tb_nomfileinv.OptionsColumn.AllowEdit = False
    Me.tb_nomfileinv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_nomfileinv.OptionsColumn.ReadOnly = True
    Me.tb_nomfileinv.OptionsFilter.AllowFilter = False
    Me.tb_nomfileinv.Visible = True
    Me.tb_nomfileinv.VisibleIndex = 5
    Me.tb_nomfileinv.Width = 70
    '
    'tb_codmagap
    '
    Me.tb_codmagap.AppearanceCell.Options.UseBackColor = True
    Me.tb_codmagap.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codmagap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codmagap.Caption = "Magazzino"
    Me.tb_codmagap.Enabled = True
    Me.tb_codmagap.FieldName = "tb_codmagap"
    Me.tb_codmagap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codmagap.Name = "tb_codmagap"
    Me.tb_codmagap.NTSRepositoryComboBox = Nothing
    Me.tb_codmagap.NTSRepositoryItemCheck = Nothing
    Me.tb_codmagap.NTSRepositoryItemMemo = Nothing
    Me.tb_codmagap.NTSRepositoryItemText = Nothing
    Me.tb_codmagap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codmagap.OptionsFilter.AllowFilter = False
    Me.tb_codmagap.Visible = True
    Me.tb_codmagap.VisibleIndex = 6
    Me.tb_codmagap.Width = 70
    '
    'xx_codmagap
    '
    Me.xx_codmagap.AppearanceCell.Options.UseBackColor = True
    Me.xx_codmagap.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codmagap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codmagap.Caption = "Descr. magazz."
    Me.xx_codmagap.Enabled = False
    Me.xx_codmagap.FieldName = "xx_codmagap"
    Me.xx_codmagap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codmagap.Name = "xx_codmagap"
    Me.xx_codmagap.NTSRepositoryComboBox = Nothing
    Me.xx_codmagap.NTSRepositoryItemCheck = Nothing
    Me.xx_codmagap.NTSRepositoryItemMemo = Nothing
    Me.xx_codmagap.NTSRepositoryItemText = Nothing
    Me.xx_codmagap.OptionsColumn.AllowEdit = False
    Me.xx_codmagap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codmagap.OptionsColumn.ReadOnly = True
    Me.xx_codmagap.OptionsFilter.AllowFilter = False
    Me.xx_codmagap.Visible = True
    Me.xx_codmagap.VisibleIndex = 7
    Me.xx_codmagap.Width = 70
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 20
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'FRMMGLSAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grLsar)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGLSAR"
    Me.NTSLastControlFocussed = Me.grLsar
    Me.Text = "LISTE SELEZIONATE ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grLsar, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLsar, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGLSAR", "BEMGLSAR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128521372931662949, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleLsar = CType(oTmp, CLEMGLSAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGLSAR", strRemoteServer, strRemotePort)
    AddHandler oCleLsar.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleLsar.Init(oApp, oScript, oMenu.oCleComm, "TABLSAR", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTb_status As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbDettagli.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbFileGeneraFile.GlyphPath = (oApp.ChildImageDir & "\export.gif")
        tlbFileAcquisisciFile.GlyphPath = (oApp.ChildImageDir & "\import.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttTb_status.Columns.Add("cod", GetType(String))
      dttTb_status.Columns.Add("val", GetType(String))
      dttTb_status.Rows.Add(New Object() {"M", "Modificabile"})
      dttTb_status.Rows.Add(New Object() {"P", "Prelevato"})
      dttTb_status.Rows.Add(New Object() {"S", "Sospeso"})
      dttTb_status.Rows.Add(New Object() {"C", "Completato"})
      dttTb_status.AcceptChanges()

      grvLsar.NTSSetParam(oMenu, "LISTE SELEZIONATE ARTICOLI")

      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521372930256636, "Cod. Ditta"), 12, True)
      tb_codlsar.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128521372930412893, "Codice"), tablsar)
      tb_deslsar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521372930569150, "Descrizione"), 50, True)
      tb_lsarnote.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521372930725407, "Note"), 0, True)
      tb_dtcomp.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128521372930881664, "Data compilazione"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_status.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128521372931037921, "Status"), dttTb_status, "val", "cod")
      tb_nomfileinv.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521372931194178, "Nome file inventario"), 255, True)
      tb_codmagap.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128521372931350435, "Magazzino"), tabmaga)
      xx_codmagap.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521372931506692, "Descr. magazz."), 0, True)

      tb_codlsar.NTSSetRichiesto()

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
  Public overridable Sub FRMMGLSAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupIPL) Then
        oCleLsar.bModSupIPL = True
      Else
        oCleLsar.bModSupIPL = False
      End If

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()

      'controllo su chiave di attivazione
      If oCleLsar.bModSupIPL = False Then
        tlbFileGeneraFile.Enabled = False
        tlbFileAcquisisciFile.Enabled = False
      End If

      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvLsar.NTSAllowInsert Then
            dcLsar.MoveLast()
            grvLsar.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i As Integer = 0 To dcLsar.List.Count - 1
            If CType(dcLsar.Item(i), DataRowView)!tb_codlsar.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcLsar.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then

      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione FRIENDLY nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbFileGeneraFile.Enabled = False
        tlbFileAcquisisciFile.Enabled = False
        tlbFileGeneraFile.Visible = False
        tlbFileAcquisisciFile.Visible = False
        tb_status.Visible = False
        tb_nomfileinv.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGLSAR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public overridable Sub FRMMGLSAR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLsar.Dispose()
      dsLsar.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvLsar.NTSNuovo()

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
    Dim strCodLsar As String = ""
    Try
      If grvLsar.NTSGetCurrentDataRow Is Nothing Then Return
      strCodLsar = NTSCStr(dsLsar.Tables("TABLSAR").Rows(dcLsar.Position)!tb_codlsar)

      'Se è presente nelle promozioni non può essere cancellata.
      If Not oCleLsar.CheckInPromozioni(NTSCInt(strCodLsar)) Then Return


      If Not grvLsar.NTSDeleteRigaCorrente(dcLsar, True) Then Return
      If Not oCleLsar.DeleteListsar(strCodLsar) Then Return
      oCleLsar.Salva(True)
      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvLsar.NTSRipristinaRigaCorrenteBefore(dcLsar, True) Then Return
      oCleLsar.Ripristina(dcLsar.Position, dcLsar.Filter)
      grvLsar.NTSRipristinaRigaCorrenteAfter()
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

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
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

  Public Overridable Sub tlbDettagli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettagli.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmElsa As FRMMGELSA = Nothing
    Try
      Salva()

      If grvLsar.NTSGetCurrentDataRow() Is Nothing Then Return

      '-----------------------------------------------------------------------------------------
      oCleLsar.strCodLSar = NTSCStr(grvLsar.NTSGetCurrentDataRow()!tb_codlsar)
      oCleLsar.nCodMagP = NTSCInt(grvLsar.NTSGetCurrentDataRow()!tb_codmagap)
      If NTSCInt(oCleLsar.strCodLsar) = 0 Then Exit Sub

      Select Case NTSCStr(grvLsar.NTSGetCurrentDataRow()!tb_status)
        Case "C", "M" : oCleLsar.bElsaGrigliaBloccata = False
        Case "P", "S" : oCleLsar.bElsaGrigliaBloccata = True
      End Select

      frmElsa = CType(NTSNewFormModal("FRMMGELSA"), FRMMGELSA)
      frmElsa.Init(oMenu, oParam, DittaCorrente)
      frmElsa.InitEntity(oCleLsar)
      frmElsa.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmElsa Is Nothing Then frmElsa.Dispose()
      frmElsa = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbFileGeneraFile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFileGeneraFile.ItemClick
    Dim strMsg As String = ""
    Dim strnomfileinv As String = ""
    Dim nCodlsarTmp As Integer
    Try
      If grvLsar.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      If Not Salva() Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se non esistono records, esce
      '-----------------------------------------------------------------------------------------
      If dsLsar.Tables("TABLSAR").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128523918756156345, "Attenzione!" & vbCrLf & _
          "Non esiste una lista selezionata valida dalla quale generare il file inventario." & vbCrLf & _
          "Generazione file inventario non possibile."))
        Exit Sub
      End If

      If Not oCleLsar.FileGeneraFile(grvLsar.NTSGetCurrentDataRow, strnomfileinv, nCodlsarTmp) Then Exit Sub

      '-----------------------------------------------------------------------------------------
      '--- Riapre il DataSet
      '-----------------------------------------------------------------------------------------
      Apri()
      '-----------------------------------------------------------------------------------------
      posizionaAfter(NTSCStr(nCodlsarTmp))
      '-----------------------------------------------------------------------------------------
      oApp.MsgBoxInfo(oApp.Tr(Me, 128523979041244759, "Generazione file inventario '|" & strnomfileinv & "|' terminata."))
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbFileAcquisisciFile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFileAcquisisciFile.ItemClick
    Try
      If grvLsar.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      If Not Salva() Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se non esistono records, esce
      '-----------------------------------------------------------------------------------------
      If dsLsar.Tables("TABLSAR").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128523985207264572, "Attenzione!" & vbCrLf & _
          "Non esiste una lista selezionata valida per la quale generare righe di dettaglio da file inventario." & vbCrLf & _
          "Acquisizione da file inventario non possibile."))
        Exit Sub
      End If

      If Not oCleLsar.FileAcquisisciFile(grvLsar.NTSGetCurrentDataRow) Then Exit Sub

      '-----------------------------------------------------------------------------------------
      oApp.MsgBoxInfo(oApp.Tr(Me, 128523983861411596, "Acquisizione da file inventario '|" & NTSCStr(grvLsar.NTSGetCurrentDataRow()!tb_nomfileinv) & "|' terminata."))
      '-----------------------------------------------------------------------------------------

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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvLsar_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvLsar.NTSBeforeRowUpdate
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
  Public Overridable Sub grvLsar_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLsar.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleLsar Is Nothing Then Return

      dtrT = grvLsar.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        tb_codlsar.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!tb_codlsar) <> 0 Then
        tb_codlsar.Enabled = False
      Else
        tb_codlsar.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvLsar.NTSSalvaRigaCorrente(dcLsar, oCleLsar.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleLsar.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleLsar.Ripristina(dcLsar.Position, dcLsar.Filter)
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

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{tablsar.codditt} = '" & DittaCorrente & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGLSAR", "Reports1", " ", 0, nDestin, "BSMGLSAR.RPT", False, "LISTE SELEZIONATE ARTICOLI", False)
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
      If dsLsar.Tables("TABLSAR").Rows.Count = 0 Then
        tlbDettagli.Visible = False
        tlbStampaVideo.Visible = False
        tlbStampa.Visible = False
        tlbImpostaStampante.Visible = False
      Else
        tlbDettagli.Visible = True
        tlbStampaVideo.Visible = True
        tlbStampa.Visible = True
        tlbImpostaStampante.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Dim i As Integer
    Try
      dcLsar.DataSource = Nothing
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleLsar.Apri(DittaCorrente, dsLsar) Then Me.Close()
      dcLsar.DataSource = dsLsar.Tables("TABLSAR")
      dsLsar.AcceptChanges()

      grLsar.DataSource = dcLsar

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvLsar.NTSAllowInsert Then
            grvLsar.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcLsar.List.Count - 1
            If CType(dcLsar.Item(i), DataRowView)!tb_codlsar.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcLsar.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then

      AbilDisabControlli()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub posizionaAfter(ByVal strCod As String)
    Dim i As Integer
    Dim lPos As Integer
    Try
      lPos = dcLsar.Position
      dcLsar.MoveFirst()
      For i = 0 To dcLsar.Count - 1
        If dsLsar.Tables("TABLSAR").Rows(dcLsar.Position)("tb_codlsar").ToString = strCod Then
          Exit Sub
        End If
        dcLsar.MoveNext()
      Next
      dcLsar.Position = lPos
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim strCodlistsel As String
    Dim strNewCodlistsel As String
    Try
      If grvLsar.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130354650523211037, "Posizionarsi su una lista esistente da duplicare."))
        Return
      End If
      strNewCodlistsel = oApp.InputBoxNew("NUOVO CODICE LISTA SELEZIONATA")

      If strNewCodlistsel <> "" Then
        If Not IsNumeric(strNewCodlistsel) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650452741385, "Inserire un codice numerico per la nuova lista selezionata."))
          Return
        End If
        If NTSCInt(strNewCodlistsel) <= 0 Or NTSCInt(strNewCodlistsel) > 9999 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650476179185, "Inserire un codice numerico all'interno del intervallo 1-9999"))
          Return
        End If
        If oMenu.ValCodiceDb(strNewCodlistsel, DittaCorrente, "TABLSAR", "N") Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650498835725, "Codice nuova lista selezionata già esistente."))
          Return
        End If

        strCodlistsel = NTSCStr(grvLsar.NTSGetCurrentDataRow!tb_codlsar)

        oCleLsar.Duplica(strCodlistsel, strNewCodlistsel)
        Apri()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
