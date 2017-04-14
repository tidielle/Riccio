Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNOF
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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
  Public oCleGnof As CLEORGNOF
  Public oCallParams As CLE__CLDP
  Public dsGnof As New DataSet
  Public dcGnof As BindingSource = New BindingSource()

  Public nStato As Integer 'ATTENZIONE non coincide con vb6 va da 0 a 4

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents grGnof As NTSInformatica.NTSGrid
  Public WithEvents grvGnof As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tt_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tt_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tt_numero As NTSInformatica.NTSGridColumn
  Public WithEvents tt_riga As NTSInformatica.NTSGridColumn
  Public WithEvents tt_datord As NTSInformatica.NTSGridColumn
  Public WithEvents tt_codart As NTSInformatica.NTSGridColumn
  Public WithEvents tt_desart As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quant As NTSInformatica.NTSGridColumn
  Public WithEvents tt_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents lbXxConto As NTSInformatica.NTSLabel
  Public WithEvents tlbSelRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSelRigheOrd As NTSInformatica.NTSBarButtonItem
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGNOF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelRigheOrd = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiorna = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettQta = New NTSInformatica.NTSBarButtonItem
    Me.tlbAccorpa = New NTSInformatica.NTSBarButtonItem
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
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
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.grGnof = New NTSInformatica.NTSGrid
    Me.grvGnof = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tt_anno = New NTSInformatica.NTSGridColumn
    Me.tt_serie = New NTSInformatica.NTSGridColumn
    Me.tt_numero = New NTSInformatica.NTSGridColumn
    Me.tt_riga = New NTSInformatica.NTSGridColumn
    Me.tt_datord = New NTSInformatica.NTSGridColumn
    Me.tt_codart = New NTSInformatica.NTSGridColumn
    Me.tt_desart = New NTSInformatica.NTSGridColumn
    Me.tt_quant = New NTSInformatica.NTSGridColumn
    Me.tt_datcons = New NTSInformatica.NTSGridColumn
    Me.lbXxConto = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.grGnof, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGnof, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbRipristina, Me.tlbSelRighe, Me.tlbSelRigheOrd, Me.tlbRecordCancella, Me.tlbRecordRipristina, Me.tlbAggiorna, Me.tlbDettQta, Me.tlbAccorpa, Me.tlbElabora})
    Me.NtsBarManager1.MaxItemId = 41
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelRighe, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelRigheOrd), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiorna, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettQta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAccorpa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 17
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbSelRighe
    '
    Me.tlbSelRighe.Caption = "Seleziona righe"
    Me.tlbSelRighe.Glyph = CType(resources.GetObject("tlbSelRighe.Glyph"), System.Drawing.Image)
    Me.tlbSelRighe.Id = 33
    Me.tlbSelRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H))
    Me.tlbSelRighe.Name = "tlbSelRighe"
    Me.tlbSelRighe.Visible = True
    '
    'tlbSelRigheOrd
    '
    Me.tlbSelRigheOrd.Caption = "Seleziona righe d'ordine"
    Me.tlbSelRigheOrd.Glyph = CType(resources.GetObject("tlbSelRigheOrd.Glyph"), System.Drawing.Image)
    Me.tlbSelRigheOrd.Id = 34
    Me.tlbSelRigheOrd.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbSelRigheOrd.Name = "tlbSelRigheOrd"
    Me.tlbSelRigheOrd.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Record cancella"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 35
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Record ripristina"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.Id = 36
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbAggiorna
    '
    Me.tlbAggiorna.Caption = "Aggiorna flag rilasciato"
    Me.tlbAggiorna.Glyph = CType(resources.GetObject("tlbAggiorna.Glyph"), System.Drawing.Image)
    Me.tlbAggiorna.Id = 37
    Me.tlbAggiorna.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbAggiorna.Name = "tlbAggiorna"
    Me.tlbAggiorna.Visible = True
    '
    'tlbDettQta
    '
    Me.tlbDettQta.Caption = "Dettaglio qta"
    Me.tlbDettQta.Glyph = CType(resources.GetObject("tlbDettQta.Glyph"), System.Drawing.Image)
    Me.tlbDettQta.Id = 38
    Me.tlbDettQta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
    Me.tlbDettQta.Name = "tlbDettQta"
    Me.tlbDettQta.Visible = True
    '
    'tlbAccorpa
    '
    Me.tlbAccorpa.Caption = "Accorpa righe"
    Me.tlbAccorpa.Glyph = CType(resources.GetObject("tlbAccorpa.Glyph"), System.Drawing.Image)
    Me.tlbAccorpa.Id = 39
    Me.tlbAccorpa.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C))
    Me.tlbAccorpa.Name = "tlbAccorpa"
    Me.tlbAccorpa.Visible = True
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 40
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
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
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.grGnof)
    Me.pnBottom.Controls.Add(Me.lbXxConto)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBottom.Location = New System.Drawing.Point(0, 30)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(648, 412)
    Me.pnBottom.TabIndex = 10
    Me.pnBottom.Text = "pnBottom"
    '
    'grGnof
    '
    Me.grGnof.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGnof.EmbeddedNavigator.Name = ""
    Me.grGnof.Location = New System.Drawing.Point(0, 20)
    Me.grGnof.MainView = Me.grvGnof
    Me.grGnof.Name = "grGnof"
    Me.grGnof.Size = New System.Drawing.Size(648, 392)
    Me.grGnof.TabIndex = 21
    Me.grGnof.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGnof})
    '
    'grvGnof
    '
    Me.grvGnof.ActiveFilterEnabled = False
    Me.grvGnof.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.tt_anno, Me.tt_serie, Me.tt_numero, Me.tt_riga, Me.tt_datord, Me.tt_codart, Me.tt_desart, Me.tt_quant, Me.tt_datcons})
    Me.grvGnof.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGnof.Enabled = True
    Me.grvGnof.GridControl = Me.grGnof
    Me.grvGnof.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGnof.MinRowHeight = 14
    Me.grvGnof.Name = "grvGnof"
    Me.grvGnof.NTSAllowDelete = True
    Me.grvGnof.NTSAllowInsert = True
    Me.grvGnof.NTSAllowUpdate = True
    Me.grvGnof.NTSMenuContext = Nothing
    Me.grvGnof.OptionsCustomization.AllowRowSizing = True
    Me.grvGnof.OptionsFilter.AllowFilterEditor = False
    Me.grvGnof.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGnof.OptionsNavigation.UseTabKey = False
    Me.grvGnof.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGnof.OptionsView.ColumnAutoWidth = False
    Me.grvGnof.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGnof.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGnof.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGnof.OptionsView.ShowGroupPanel = False
    Me.grvGnof.RowHeight = 16
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
    'tt_anno
    '
    Me.tt_anno.AppearanceCell.Options.UseBackColor = True
    Me.tt_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tt_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_anno.Caption = "Anno"
    Me.tt_anno.Enabled = False
    Me.tt_anno.FieldName = "tt_anno"
    Me.tt_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_anno.Name = "tt_anno"
    Me.tt_anno.NTSRepositoryComboBox = Nothing
    Me.tt_anno.NTSRepositoryItemCheck = Nothing
    Me.tt_anno.NTSRepositoryItemMemo = Nothing
    Me.tt_anno.NTSRepositoryItemText = Nothing
    Me.tt_anno.OptionsColumn.AllowEdit = False
    Me.tt_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_anno.OptionsColumn.ReadOnly = True
    Me.tt_anno.OptionsFilter.AllowFilter = False
    Me.tt_anno.Visible = True
    Me.tt_anno.VisibleIndex = 0
    '
    'tt_serie
    '
    Me.tt_serie.AppearanceCell.Options.UseBackColor = True
    Me.tt_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tt_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_serie.Caption = "Serie"
    Me.tt_serie.Enabled = False
    Me.tt_serie.FieldName = "tt_serie"
    Me.tt_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_serie.Name = "tt_serie"
    Me.tt_serie.NTSRepositoryComboBox = Nothing
    Me.tt_serie.NTSRepositoryItemCheck = Nothing
    Me.tt_serie.NTSRepositoryItemMemo = Nothing
    Me.tt_serie.NTSRepositoryItemText = Nothing
    Me.tt_serie.OptionsColumn.AllowEdit = False
    Me.tt_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_serie.OptionsColumn.ReadOnly = True
    Me.tt_serie.OptionsFilter.AllowFilter = False
    Me.tt_serie.Visible = True
    Me.tt_serie.VisibleIndex = 1
    '
    'tt_numero
    '
    Me.tt_numero.AppearanceCell.Options.UseBackColor = True
    Me.tt_numero.AppearanceCell.Options.UseTextOptions = True
    Me.tt_numero.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_numero.Caption = "Nr. ordine"
    Me.tt_numero.Enabled = False
    Me.tt_numero.FieldName = "tt_numero"
    Me.tt_numero.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_numero.Name = "tt_numero"
    Me.tt_numero.NTSRepositoryComboBox = Nothing
    Me.tt_numero.NTSRepositoryItemCheck = Nothing
    Me.tt_numero.NTSRepositoryItemMemo = Nothing
    Me.tt_numero.NTSRepositoryItemText = Nothing
    Me.tt_numero.OptionsColumn.AllowEdit = False
    Me.tt_numero.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_numero.OptionsColumn.ReadOnly = True
    Me.tt_numero.OptionsFilter.AllowFilter = False
    Me.tt_numero.Visible = True
    Me.tt_numero.VisibleIndex = 2
    '
    'tt_riga
    '
    Me.tt_riga.AppearanceCell.Options.UseBackColor = True
    Me.tt_riga.AppearanceCell.Options.UseTextOptions = True
    Me.tt_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_riga.Caption = "Riga"
    Me.tt_riga.Enabled = False
    Me.tt_riga.FieldName = "tt_riga"
    Me.tt_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_riga.Name = "tt_riga"
    Me.tt_riga.NTSRepositoryComboBox = Nothing
    Me.tt_riga.NTSRepositoryItemCheck = Nothing
    Me.tt_riga.NTSRepositoryItemMemo = Nothing
    Me.tt_riga.NTSRepositoryItemText = Nothing
    Me.tt_riga.OptionsColumn.AllowEdit = False
    Me.tt_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_riga.OptionsColumn.ReadOnly = True
    Me.tt_riga.OptionsFilter.AllowFilter = False
    Me.tt_riga.Visible = True
    Me.tt_riga.VisibleIndex = 3
    '
    'tt_datord
    '
    Me.tt_datord.AppearanceCell.Options.UseBackColor = True
    Me.tt_datord.AppearanceCell.Options.UseTextOptions = True
    Me.tt_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_datord.Caption = "Dt. ordine"
    Me.tt_datord.Enabled = False
    Me.tt_datord.FieldName = "tt_datord"
    Me.tt_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_datord.Name = "tt_datord"
    Me.tt_datord.NTSRepositoryComboBox = Nothing
    Me.tt_datord.NTSRepositoryItemCheck = Nothing
    Me.tt_datord.NTSRepositoryItemMemo = Nothing
    Me.tt_datord.NTSRepositoryItemText = Nothing
    Me.tt_datord.OptionsColumn.AllowEdit = False
    Me.tt_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_datord.OptionsColumn.ReadOnly = True
    Me.tt_datord.OptionsFilter.AllowFilter = False
    Me.tt_datord.Visible = True
    Me.tt_datord.VisibleIndex = 8
    '
    'tt_codart
    '
    Me.tt_codart.AppearanceCell.Options.UseBackColor = True
    Me.tt_codart.AppearanceCell.Options.UseTextOptions = True
    Me.tt_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_codart.Caption = "Cod. articolo"
    Me.tt_codart.Enabled = False
    Me.tt_codart.FieldName = "tt_codart"
    Me.tt_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_codart.Name = "tt_codart"
    Me.tt_codart.NTSRepositoryComboBox = Nothing
    Me.tt_codart.NTSRepositoryItemCheck = Nothing
    Me.tt_codart.NTSRepositoryItemMemo = Nothing
    Me.tt_codart.NTSRepositoryItemText = Nothing
    Me.tt_codart.OptionsColumn.AllowEdit = False
    Me.tt_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_codart.OptionsColumn.ReadOnly = True
    Me.tt_codart.OptionsFilter.AllowFilter = False
    Me.tt_codart.Visible = True
    Me.tt_codart.VisibleIndex = 4
    '
    'tt_desart
    '
    Me.tt_desart.AppearanceCell.Options.UseBackColor = True
    Me.tt_desart.AppearanceCell.Options.UseTextOptions = True
    Me.tt_desart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_desart.Caption = "Descr. articolo"
    Me.tt_desart.Enabled = False
    Me.tt_desart.FieldName = "tt_desart"
    Me.tt_desart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_desart.Name = "tt_desart"
    Me.tt_desart.NTSRepositoryComboBox = Nothing
    Me.tt_desart.NTSRepositoryItemCheck = Nothing
    Me.tt_desart.NTSRepositoryItemMemo = Nothing
    Me.tt_desart.NTSRepositoryItemText = Nothing
    Me.tt_desart.OptionsColumn.AllowEdit = False
    Me.tt_desart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_desart.OptionsColumn.ReadOnly = True
    Me.tt_desart.OptionsFilter.AllowFilter = False
    Me.tt_desart.Visible = True
    Me.tt_desart.VisibleIndex = 5
    '
    'tt_quant
    '
    Me.tt_quant.AppearanceCell.Options.UseBackColor = True
    Me.tt_quant.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quant.Caption = "Q.ta"
    Me.tt_quant.Enabled = False
    Me.tt_quant.FieldName = "tt_quant"
    Me.tt_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_quant.Name = "tt_quant"
    Me.tt_quant.NTSRepositoryComboBox = Nothing
    Me.tt_quant.NTSRepositoryItemCheck = Nothing
    Me.tt_quant.NTSRepositoryItemMemo = Nothing
    Me.tt_quant.NTSRepositoryItemText = Nothing
    Me.tt_quant.OptionsColumn.AllowEdit = False
    Me.tt_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_quant.OptionsColumn.ReadOnly = True
    Me.tt_quant.OptionsFilter.AllowFilter = False
    Me.tt_quant.Visible = True
    Me.tt_quant.VisibleIndex = 6
    '
    'tt_datcons
    '
    Me.tt_datcons.AppearanceCell.Options.UseBackColor = True
    Me.tt_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.tt_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_datcons.Caption = "Dt. consegna"
    Me.tt_datcons.Enabled = False
    Me.tt_datcons.FieldName = "tt_datcons"
    Me.tt_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_datcons.Name = "tt_datcons"
    Me.tt_datcons.NTSRepositoryComboBox = Nothing
    Me.tt_datcons.NTSRepositoryItemCheck = Nothing
    Me.tt_datcons.NTSRepositoryItemMemo = Nothing
    Me.tt_datcons.NTSRepositoryItemText = Nothing
    Me.tt_datcons.OptionsColumn.AllowEdit = False
    Me.tt_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_datcons.OptionsColumn.ReadOnly = True
    Me.tt_datcons.OptionsFilter.AllowFilter = False
    Me.tt_datcons.Visible = True
    Me.tt_datcons.VisibleIndex = 7
    '
    'lbXxConto
    '
    Me.lbXxConto.BackColor = System.Drawing.Color.Transparent
    Me.lbXxConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXxConto.Dock = System.Windows.Forms.DockStyle.Top
    Me.lbXxConto.Location = New System.Drawing.Point(0, 0)
    Me.lbXxConto.Name = "lbXxConto"
    Me.lbXxConto.NTSDbField = ""
    Me.lbXxConto.Size = New System.Drawing.Size(648, 20)
    Me.lbXxConto.TabIndex = 20
    Me.lbXxConto.Tooltip = ""
    Me.lbXxConto.UseMnemonic = False
    '
    'FRMORGNOF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORGNOF"
    Me.Text = "GENERAZIONE ORDINI FORNITORI DA IMPEGNI CLIENTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.grGnof, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGnof, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGNOF", "BEORGNOF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGnof = CType(oTmp, CLEORGNOF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORGNOF", strRemoteServer, strRemotePort)
    AddHandler oCleGnof.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGnof.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbSelRighe.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbSelRigheOrd.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbDettQta.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbAccorpa.GlyphPath = (oApp.ChildImageDir & "\dividi.gif")
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGnof.NTSSetParam(oMenu, oApp.Tr(Me, 129533737784559134, "Gnof"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129533737784715483, "codditt"), 0, True)
      tt_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129533737784871832, "Anno"), "0", 4, 0, 9999)
      tt_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129533737785028181, "Serie"), CLN__STD.SerieMaxLen, True)
      tt_numero.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129533737785184530, "Nr. ordine"), "0", 9, 0, 999999999)
      tt_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129533737785340879, "Riga"), "0", 9, 0, 999999999)
      tt_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129533737785497228, "Dt. ordine"), True)
      tt_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129533737785653577, "Cod. articolo"), 0, True)
      tt_desart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129533737785809926, "Descr. articolo"), 0, True)
      tt_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129533737785966275, "Q.ta"), "#,##0.00", 15)
      tt_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129533737786122624, "Dt. consegna"), True)

      grvGnof.NTSAllowInsert = False
      'grvGnof.NTSAllowDelete = False
      'grvGnof.NTSAllowUpdate = False
      'grvGnof.Enabled = False

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
  Public Overridable Sub FRMORGNOF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      grGnof.Visible = False

      '-------------------
      'verifico che tabpeve e tabpeac siano compilate
      If Not oCleGnof.InitExt() Then
        Me.Close()
        Return
      End If

      '-----------------------------------------------------------------------------------------
      oCleGnof.bDeterminaBolliSuOperazEsenti = CBool(NTSCInt(oMenu.GetSettingBus("Opzioni", ".", ".", "DeterminaBolliSuOperazEsenti", "0", " ", "0"))) 'Se attiva il bollo non viene determinato solo se in testata vi è il codice di esenzione, ma se la somma delle operazioni esenti del documenti (righe e spese di piede) supera la soglia minima in TABBOTR  ' NON DOCUMENTARE
      '-----------------------------------------------------------------------------------------
      '--- Test per controllare l'esistenza o meno del modulo TCO 'Taglie e colori'
      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then
        oCleGnof.bModTCO = True
      Else
        oCleGnof.bModTCO = False
        'tbrDettqta.visible = False
      End If

      oCleGnof.lIITTSeldoc = oMenu.GetTblInstId("TTSELDOC", False)
      oCleGnof.lIITTSeldocsec = oMenu.GetTblInstId("TTSELDOC", False)
      If oCleGnof.bModTCO = True Then
        oCleGnof.lIIMmtranstc = oMenu.GetTblInstId("MMTRANSTC", False)
        oCleGnof.lIIMmtranstcSec = oMenu.GetTblInstId("MMTRANSTC", False)
      End If

      '-----------------------------------------------------------------------------------------
      '--- OPzione di registro che aggiunge, al raggruppamento, la data di consegna di riga
      '-----------------------------------------------------------------------------------------
      oCleGnof.bConsideraDataCons = CBool(oMenu.GetSettingBus("BSORGNOF", "OPZIONI", ".", "ConsideraDataCons", "0", " ", "0"))

      SetStato(0)

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNOF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleGnof.lIITTSeldoc > 0 Then oMenu.ResetTblInstId("TTSELDOC", False, oCleGnof.lIITTSeldoc)
      If oCleGnof.lIITTSeldocsec > 0 Then oMenu.ResetTblInstId("TTSELDOC", False, oCleGnof.lIITTSeldocsec)
      If oCleGnof.lIIMmtranstc > 0 Then oMenu.ResetTblInstId("MMTRANSTC", False, oCleGnof.lIIMmtranstc)
      If oCleGnof.lIIMmtranstcSec > 0 Then oMenu.ResetTblInstId("MMTRANSTC", False, oCleGnof.lIIMmtranstcSec)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNOF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGnof.Dispose()
      dsGnof.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmSefo As FRMORSEFO = Nothing
    Dim strTmp As String = ""
    Dim frmSval As FRM__SVAL = Nothing
    Try
      oMenu.ResetTblInstId("TTSELDOC", False, oCleGnof.lIITTSeldoc)
      oMenu.ResetTblInstId("TTSELDOC", False, oCleGnof.lIITTSeldocsec)
      If oCleGnof.bModTCO = True Then
        oMenu.ResetTblInstId("MMTRANSTC", False, oCleGnof.lIIMmtranstc)
        oMenu.ResetTblInstId("MMTRANSTC", False, oCleGnof.lIIMmtranstcSec)
      End If

      frmSefo = CType(NTSNewFormModal("FRMORSEFO"), FRMORSEFO)
      frmSefo.Init(oMenu, oParam, DittaCorrente)
      frmSefo.InitEntity(oCleGnof)
      frmSefo.ShowDialog()

      If oCleGnof.bSefoAnnulla = True Then Exit Sub

      oCleGnof.lConto = oCleGnof.lSefoConto

      If oMenu.ValCodiceDb(NTSCStr(oCleGnof.lConto), DittaCorrente, "ANAGRA", "N", strTmp) And oCleGnof.lConto <> 0 Then
        lbXxConto.Text = oApp.Tr(Me, 131052077707183974, "Fornitore: ") & strTmp
      End If

      '----------------------
      'Chiede come operare con le valute
      If oCleGnof.IsFornitoreInValuta(DittaCorrente, oCleGnof.lSefoConto) Then
        'visualizzo la form della definizione del cambio solo se sono presenti righe in valuta 
        frmSval = CType(NTSNewFormModal("FRM__SVAL"), FRM__SVAL)
        frmSval.Init(oMenu, Nothing, DittaCorrente, Nothing)
        frmSval.ShowDialog()
        oCleGnof.nSvalOpzione = frmSval.nOptionOut
      Else
        oCleGnof.nSvalOpzione = 0
      End If

      Apri()

      '-----------------------------------------------------------------------------------------
      SetStato(1)
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSefo Is Nothing Then frmSefo.Dispose()
      frmSefo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelRighe.ItemClick
    Dim frmFxor As FRMDBFXOR = Nothing
    Try
      frmFxor = CType(NTSNewFormModal("FRMDBFXOR"), FRMDBFXOR)
      '----------------------------------------------------------------------------------
      frmFxor.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmFxor.InitEntity(oCleGnof)
      frmFxor.bInit = False
      frmFxor.ShowDialog()
      '----------------------------------------------------------------------------------
      If frmFxor.bCancel = True Then Return
      '----------------------------------------------------------------------------------
      oCleGnof.strSQLWHERE = frmFxor.strSQLWHERE
      '----------------------------------------------------------------------------------

      RiempiTTSELDOCdaFxor()
      Apri()
      If oCleGnof.bInsertTTSELDOC Then SetStato(2)

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmFxor Is Nothing Then frmFxor.Dispose()
      frmFxor = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSelRigheOrd_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelRigheOrd.ItemClick
    Try
      SelezionaOrdini("R")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim nAnnoTmp As Integer
    Dim strSerieTmp As String
    Dim lNumordTmp As Integer
    Dim lRigaTmp As Integer
    Try
      If grvGnof.NTSGetCurrentDataRow() Is Nothing Then Return
      nAnnoTmp = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_anno)
      strSerieTmp = NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_serie)
      lNumordTmp = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_numero)
      lRigaTmp = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_riga)

      If Not grvGnof.NTSDeleteRigaCorrente(dcGnof, False) Then Return
      oCleGnof.Salva(True)

      If oCleGnof.bModTCO Then
        oCleGnof.CancellaTC(nAnnoTmp, strSerieTmp, _
                            lNumordTmp, lRigaTmp)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If Not grvGnof.NTSRipristinaRigaCorrenteBefore(dcGnof, False) Then Return
      oCleGnof.Ripristina(dcGnof.Position, dcGnof.Filter)
      grvGnof.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiorna.ItemClick
    Dim dRes As DialogResult
    Try
      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129531993219062500, "Dopo l'aggiornamento del flag non sarà più possibile modificare le righe dell'ordine. Procedere con l'elaborazione?"))
      If dRes = Windows.Forms.DialogResult.No Then
        Return
      End If

      oCleGnof.UpdateRilasciato()

      SetStato(3)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettQta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettQta.ItemClick
    Dim dttTmp As New DataTable
    Dim oParam As New CLE__CLDP
    Dim frmDtag As FRMORDTAG = Nothing
    Try
      If grvGnof.NTSGetCurrentDataRow() Is Nothing Then Return
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è stato creato con 'taglie e colori' (ARTICO.ar_codtagl <> 0) esce
      '-----------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_codart), DittaCorrente, "ARTICO", "S", , dttTmp) _
        And NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_codart) <> "" Then
        If NTSCInt(dttTmp.Rows(0)!ar_codtagl) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129532012859961724, "L'articolo non è di tipo 'Taglie e colori'." & vbCrLf & _
             "Apertura 'Dettaglio quantità' non possibile."))
          Return
        End If
      End If
      '-----------------------------------------------------------------------------------------
      oCleGnof.lDtagInstid = oCleGnof.lIIMmtranstc 'differisce da vb6
      oCleGnof.nDtagAnno = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_anno)
      oCleGnof.strDtagSerie = NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_serie)
      oCleGnof.lDtagNumdoc = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_numero)
      oCleGnof.lDtagRiga = NTSCInt(grvGnof.NTSGetCurrentDataRow()!tt_riga)
      oCleGnof.strDtagCodart = UCase(NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_codart))

      oCleGnof.bDtagAbilCampi = CBool(IIf(nStato = 4, True, False))

      frmDtag = CType(NTSNewFormModal("FRMORDTAG"), FRMORDTAG)
      frmDtag.Init(oMenu, oParam, DittaCorrente)
      frmDtag.InitEntity(oCleGnof)
      frmDtag.ShowDialog()

      If (nStato = 4) And (oCleGnof.bDtagAnnullato = False) And tt_quant.Enabled = False Then
        grvGnof.NTSGetCurrentDataRow()!tt_quant = NTSCDec(oCleGnof.dDtagQuant)
        Salva()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDtag Is Nothing Then frmDtag.Dispose()
      frmDtag = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbAccorpa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAccorpa.ItemClick
    Try
      FileAccorpaRighe()
      SetStato(4)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      GeneraOrdineFornitore(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If grvGnof.RowCount = 0 Then Return
      Salva()
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If grvGnof.RowCount = 0 Then Return
      Salva()
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    Me.Close()
  End Sub

#End Region

  Public Overridable Sub grvGnof_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs)
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
  Public Overridable Sub grvGnof_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGnof.NTSFocusedRowChanged
    Dim dttTmp As New DataTable
    Try
      '-------------------------------------------------
      'se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If nStato = 4 Then
        If NTSCStr(grvGnof.GetFocusedRowCellValue(tt_codart).ToString.Trim) <> "" Then
          If oMenu.ValCodiceDb(NTSCStr(grvGnof.NTSGetCurrentDataRow()!tt_codart), DittaCorrente, "ARTICO", "S", , dttTmp) Then
            If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
              tt_quant.Enabled = False
              Return
            End If
          End If
        End If
        GctlSetVisEnab(tt_quant, False)
      Else
        tt_quant.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGnof.NTSSalvaRigaCorrente(dcGnof, oCleGnof.RecordIsChanged, False)

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleGnof.Salva(False) Then
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Try
      'Genera l'ordine
      If Not GeneraOrdineFornitore(True) Then Exit Sub

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{MOVORD.codditt} = '" & DittaCorrente & "'" & _
          " And {MOVORD.mo_tipork} = 'O'" & _
          " And {MOVORD.mo_anno} = " & oCleGnof.nGndoAnno & _
          " And {MOVORD.mo_serie} = " & ConvStrRpt(oCleGnof.strGndoSerie) & _
          " And {MOVORD.mo_numord} = " & oCleGnof.lGndoNumord & _
          " AND {MOVORD.mo_stasino} <> 'N'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOR", "Reports1", " ", 0, nDestin, "BSORGSOR.RPT", False, "Stampa Ordine", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      SetStato(0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      oCleGnof.Apri(dsGnof)

      ''-------------------------------------------------
      ''leggo dal database i dati e collego il NTSBindingNavigator
      dcGnof.DataSource = dsGnof.Tables("TTSELDOC")
      dsGnof.AcceptChanges()
      grGnof.DataSource = dcGnof

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      If nSetStato = 0 Then
        GctlSetVisEnab(tt_anno, True)
        GctlSetVisEnab(tt_serie, True)
        GctlSetVisEnab(tt_numero, True)
        GctlSetVisEnab(tt_riga, True)
        GctlSetVisEnab(tt_datord, True)
        tt_quant.Enabled = False
        tt_datcons.Enabled = False

        tlbSelRighe.Enabled = False
        tlbSelRigheOrd.Enabled = False
        tlbRipristina.Enabled = False
        tlbNuovo.Enabled = True
        tlbRecordCancella.Enabled = False
        tlbRecordRipristina.Enabled = False
        tlbAccorpa.Enabled = False
        tlbAggiorna.Enabled = False
        tlbDettQta.Enabled = False
        tlbElabora.Enabled = False
        tlbStampa.Enabled = False
        tlbStampaVideo.Enabled = False

        grGnof.Visible = False
        lbXxConto.Visible = False
        nStato = 0
      ElseIf nSetStato = 1 Then
        GctlSetVisEnab(tlbSelRighe, False)
        GctlSetVisEnab(tlbSelRigheOrd, False)
        GctlSetVisEnab(tlbRipristina, False)
        tlbNuovo.Enabled = False

        grGnof.Visible = True
        lbXxConto.Visible = True
        nStato = 1
      ElseIf nSetStato = 2 Then
        tlbSelRighe.Enabled = False
        tlbSelRigheOrd.Enabled = False

        GctlSetVisEnab(tlbRecordCancella, False)
        GctlSetVisEnab(tlbDettQta, False)
        GctlSetVisEnab(tlbAggiorna, False)
        nStato = 2
      ElseIf nSetStato = 3 Then
        tlbRecordCancella.Enabled = False
        tlbAggiorna.Enabled = False

        GctlSetVisEnab(tlbAccorpa, False)
        nStato = 3
      ElseIf nSetStato = 4 Then
        tt_anno.Visible = False
        tt_serie.Visible = False
        tt_numero.Visible = False
        tt_riga.Visible = False
        tt_datord.Visible = False
        GctlSetVisEnab(tt_quant, False)
        GctlSetVisEnab(tt_datcons, False)

        tlbAccorpa.Enabled = False

        GctlSetVisEnab(tlbRecordRipristina, False)
        GctlSetVisEnab(tlbElabora, False)
        GctlSetVisEnab(tlbStampa, False)
        GctlSetVisEnab(tlbStampaVideo, False)
        nStato = 4
        grvGnof_NTSFocusedRowChanged(Nothing, Nothing)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SelezionaOrdini(ByVal strTipo As String)
    'strTipo: R = righe, 
    Dim lCodConto As Integer = 0
    Dim oPar As New CLE__PATB
    Dim dQuant As Decimal = 0
    Dim strCodart As String = ""
    Dim nFase As Integer = 0
    Dim dttTmp As New DataTable
    Try
      Select Case strTipo
        Case "R"
          '------------------------
          'Chiama lo zoom righe ordini

          '------------------------
          'Chiama lo zoom ordini aperti
          oPar.strDescr = " testord.td_tipork = 'R'" & _
            " AND movord.mo_rilasciato = 'N'" & _
            " AND (artico.ar_forn = " & oCleGnof.lConto & " OR artico.ar_forn2 = " & oCleGnof.lConto & ")"
          oPar.strTipo = "R"
          'oPar.lContoCF = lCodConto
          'oPar.strCodart = strCodart
          'oPar.nFase = nFase
          'oPar.dImporto = dQuant
          'oPar.lCommessa = 0
          'oPar.bFlag1 = False
          'oPar.nAnno = NTSCInt(edAnnoDoc.Text)
          'oPar.strAlfpar = edSerieDoc.Text
          'oPar.lNumreg = NTSCInt(edNumDoc.Text)
          oPar.nTipologia = 2                     '0 solo visualizzaz, 2 = possibilità di selez le righe
          'Chiama lo zoom righe ordini "statisco"
          oPar.oParam = Nothing
          oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
          NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo
          If oPar Is Nothing Then Return
          If oPar.oParam Is Nothing Then Return
          CType(oPar.oParam, DataTable).AcceptChanges()

      End Select

      'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      dttTmp = CType(oPar.oParam, DataTable)

      AggiornaRecordTabella(dttTmp)
      Apri()
      SetStato(2)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub RiempiTTSELDOCdaFxor()
    Try
      oCleGnof.RiempiTTSELDOCdaFxor()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub AggiornaRecordTabella(ByVal dttTmp As DataTable)
    Try
      oCleGnof.AggiornaRecordTabella(dttTmp)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FileAccorpaRighe()
    Try
      oCleGnof.FileAccorpaRighe()
      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function GeneraOrdineFornitore(ByVal bStampa As Boolean) As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmGndo As FRMORGNDO = Nothing
    Dim strMsg As String = ""
    Try
      frmGndo = CType(NTSNewFormModal("FRMORGNDO"), FRMORGNDO)
      frmGndo.Init(oMenu, oParam, DittaCorrente)
      frmGndo.InitEntity(oCleGnof)
      frmGndo.ShowDialog()

      If oCleGnof.bGndoAnnulla Then Return False

      If Not oCleGnof.GeneraOrdineFornitore(dsGnof) Then Return False

      SetStato(0)

      If bStampa = False Then
        strMsg = "Generazione Ordine Fornitore n°" & NTSCStr(oCleGnof.lGndoNumord) & NTSCStr(IIf(Trim(oCleGnof.strGndoSerie) <> "", "/" & oCleGnof.strGndoSerie, "")) & _
          " anno: " & NTSCStr(oCleGnof.nGndoAnno) & ", da Impegni Clienti terminata."
        oApp.MsgBoxInfo(oApp.Tr(Me, 129658405329351080, strMsg))
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGndo Is Nothing Then frmGndo.Dispose()
      frmGndo = Nothing
    End Try
  End Function

End Class
