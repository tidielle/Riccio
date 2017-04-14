Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEGNBF
  Private Moduli_P As Integer = bsModVE + bsModMG
  Private ModuliExt_P As Integer = 0
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
  Public oCleGnbf As CLEVEGNBF
  Public oCallParams As CLE__CLDP
  Public dsGnbf As New DataSet
  Public dcGnbf As BindingSource = New BindingSource()
  Public strTiporkDaProgramma As String = ""
  Public strElencoProgressivi As String = ""

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
  Public WithEvents grGnbf As NTSInformatica.NTSGrid
  Public WithEvents grvGnbf As NTSInformatica.NTSGridView
  Public WithEvents fd_flsel As NTSInformatica.NTSGridColumn
  Public WithEvents fd_anno As NTSInformatica.NTSGridColumn
  Public WithEvents fd_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_serie As NTSInformatica.NTSGridColumn
  Public WithEvents fd_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_conto As NTSInformatica.NTSGridColumn
  Public WithEvents fd_descr As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEGNBF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
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
    Me.grGnbf = New NTSInformatica.NTSGrid
    Me.grvGnbf = New NTSInformatica.NTSGridView
    Me.fd_flsel = New NTSInformatica.NTSGridColumn
    Me.fd_anno = New NTSInformatica.NTSGridColumn
    Me.fd_numdoc = New NTSInformatica.NTSGridColumn
    Me.fd_serie = New NTSInformatica.NTSGridColumn
    Me.fd_datdoc = New NTSInformatica.NTSGridColumn
    Me.fd_totdoc = New NTSInformatica.NTSGridColumn
    Me.fd_conto = New NTSInformatica.NTSGridColumn
    Me.fd_descr = New NTSInformatica.NTSGridColumn
    Me.fd_ornum = New NTSInformatica.NTSGridColumn
    Me.fd_orserie = New NTSInformatica.NTSGridColumn
    Me.fd_oranno = New NTSInformatica.NTSGridColumn
    Me.fd_ordata = New NTSInformatica.NTSGridColumn
    Me.fd_soloasa = New NTSInformatica.NTSGridColumn
    Me.fd_tdflevas = New NTSInformatica.NTSGridColumn
    Me.fd_codpaga = New NTSInformatica.NTSGridColumn
    Me.fd_despaga = New NTSInformatica.NTSGridColumn
    Me.fd_codtpbf = New NTSInformatica.NTSGridColumn
    Me.fd_destpbf = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGnbf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGnbf, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'grGnbf
    '
    Me.grGnbf.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGnbf.EmbeddedNavigator.Name = ""
    Me.grGnbf.Location = New System.Drawing.Point(0, 26)
    Me.grGnbf.MainView = Me.grvGnbf
    Me.grGnbf.Name = "grGnbf"
    Me.grGnbf.Size = New System.Drawing.Size(648, 416)
    Me.grGnbf.TabIndex = 5
    Me.grGnbf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGnbf})
    '
    'grvGnbf
    '
    Me.grvGnbf.ActiveFilterEnabled = False
    Me.grvGnbf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.fd_flsel, Me.fd_anno, Me.fd_numdoc, Me.fd_serie, Me.fd_datdoc, Me.fd_totdoc, Me.fd_conto, Me.fd_descr, Me.fd_ornum, Me.fd_orserie, Me.fd_oranno, Me.fd_ordata, Me.fd_soloasa, Me.fd_tdflevas, Me.fd_codpaga, Me.fd_despaga, Me.fd_codtpbf, Me.fd_destpbf})
    Me.grvGnbf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGnbf.Enabled = True
    Me.grvGnbf.GridControl = Me.grGnbf
    Me.grvGnbf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGnbf.Name = "grvGnbf"
    Me.grvGnbf.NTSAllowDelete = True
    Me.grvGnbf.NTSAllowInsert = True
    Me.grvGnbf.NTSAllowUpdate = True
    Me.grvGnbf.NTSMenuContext = Nothing
    Me.grvGnbf.OptionsCustomization.AllowRowSizing = True
    Me.grvGnbf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGnbf.OptionsNavigation.UseTabKey = False
    Me.grvGnbf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGnbf.OptionsView.ColumnAutoWidth = False
    Me.grvGnbf.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGnbf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGnbf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGnbf.OptionsView.ShowGroupPanel = False
    Me.grvGnbf.RowHeight = 16
    '
    'fd_flsel
    '
    Me.fd_flsel.AppearanceCell.Options.UseBackColor = True
    Me.fd_flsel.AppearanceCell.Options.UseTextOptions = True
    Me.fd_flsel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_flsel.Caption = "Seleziona"
    Me.fd_flsel.Enabled = True
    Me.fd_flsel.FieldName = "fd_flsel"
    Me.fd_flsel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_flsel.Name = "fd_flsel"
    Me.fd_flsel.NTSRepositoryComboBox = Nothing
    Me.fd_flsel.NTSRepositoryItemCheck = Nothing
    Me.fd_flsel.NTSRepositoryItemText = Nothing
    Me.fd_flsel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_flsel.OptionsFilter.AllowFilter = False
    Me.fd_flsel.Visible = True
    Me.fd_flsel.VisibleIndex = 0
    Me.fd_flsel.Width = 70
    '
    'fd_anno
    '
    Me.fd_anno.AppearanceCell.Options.UseBackColor = True
    Me.fd_anno.AppearanceCell.Options.UseTextOptions = True
    Me.fd_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_anno.Caption = "Anno"
    Me.fd_anno.Enabled = False
    Me.fd_anno.FieldName = "fd_anno"
    Me.fd_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_anno.Name = "fd_anno"
    Me.fd_anno.NTSRepositoryComboBox = Nothing
    Me.fd_anno.NTSRepositoryItemCheck = Nothing
    Me.fd_anno.NTSRepositoryItemText = Nothing
    Me.fd_anno.OptionsColumn.AllowEdit = False
    Me.fd_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_anno.OptionsColumn.ReadOnly = True
    Me.fd_anno.OptionsFilter.AllowFilter = False
    Me.fd_anno.Visible = True
    Me.fd_anno.VisibleIndex = 1
    Me.fd_anno.Width = 70
    '
    'fd_numdoc
    '
    Me.fd_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_numdoc.Caption = "Numero"
    Me.fd_numdoc.Enabled = False
    Me.fd_numdoc.FieldName = "fd_numdoc"
    Me.fd_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_numdoc.Name = "fd_numdoc"
    Me.fd_numdoc.NTSRepositoryComboBox = Nothing
    Me.fd_numdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_numdoc.NTSRepositoryItemText = Nothing
    Me.fd_numdoc.OptionsColumn.AllowEdit = False
    Me.fd_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_numdoc.OptionsColumn.ReadOnly = True
    Me.fd_numdoc.OptionsFilter.AllowFilter = False
    Me.fd_numdoc.Visible = True
    Me.fd_numdoc.VisibleIndex = 2
    Me.fd_numdoc.Width = 70
    '
    'fd_serie
    '
    Me.fd_serie.AppearanceCell.Options.UseBackColor = True
    Me.fd_serie.AppearanceCell.Options.UseTextOptions = True
    Me.fd_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_serie.Caption = "Serie"
    Me.fd_serie.Enabled = False
    Me.fd_serie.FieldName = "fd_serie"
    Me.fd_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_serie.Name = "fd_serie"
    Me.fd_serie.NTSRepositoryComboBox = Nothing
    Me.fd_serie.NTSRepositoryItemCheck = Nothing
    Me.fd_serie.NTSRepositoryItemText = Nothing
    Me.fd_serie.OptionsColumn.AllowEdit = False
    Me.fd_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_serie.OptionsColumn.ReadOnly = True
    Me.fd_serie.OptionsFilter.AllowFilter = False
    Me.fd_serie.Visible = True
    Me.fd_serie.VisibleIndex = 3
    Me.fd_serie.Width = 70
    '
    'fd_datdoc
    '
    Me.fd_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_datdoc.Caption = "Data"
    Me.fd_datdoc.Enabled = False
    Me.fd_datdoc.FieldName = "fd_datdoc"
    Me.fd_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_datdoc.Name = "fd_datdoc"
    Me.fd_datdoc.NTSRepositoryComboBox = Nothing
    Me.fd_datdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_datdoc.NTSRepositoryItemText = Nothing
    Me.fd_datdoc.OptionsColumn.AllowEdit = False
    Me.fd_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_datdoc.OptionsColumn.ReadOnly = True
    Me.fd_datdoc.OptionsFilter.AllowFilter = False
    Me.fd_datdoc.Visible = True
    Me.fd_datdoc.VisibleIndex = 4
    Me.fd_datdoc.Width = 70
    '
    'fd_totdoc
    '
    Me.fd_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_totdoc.Caption = "Totale doc."
    Me.fd_totdoc.Enabled = False
    Me.fd_totdoc.FieldName = "fd_totdoc"
    Me.fd_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_totdoc.Name = "fd_totdoc"
    Me.fd_totdoc.NTSRepositoryComboBox = Nothing
    Me.fd_totdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_totdoc.NTSRepositoryItemText = Nothing
    Me.fd_totdoc.OptionsColumn.AllowEdit = False
    Me.fd_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_totdoc.OptionsColumn.ReadOnly = True
    Me.fd_totdoc.OptionsFilter.AllowFilter = False
    Me.fd_totdoc.Visible = True
    Me.fd_totdoc.VisibleIndex = 5
    Me.fd_totdoc.Width = 70
    '
    'fd_conto
    '
    Me.fd_conto.AppearanceCell.Options.UseBackColor = True
    Me.fd_conto.AppearanceCell.Options.UseTextOptions = True
    Me.fd_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_conto.Caption = "Cliente"
    Me.fd_conto.Enabled = False
    Me.fd_conto.FieldName = "fd_conto"
    Me.fd_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_conto.Name = "fd_conto"
    Me.fd_conto.NTSRepositoryComboBox = Nothing
    Me.fd_conto.NTSRepositoryItemCheck = Nothing
    Me.fd_conto.NTSRepositoryItemText = Nothing
    Me.fd_conto.OptionsColumn.AllowEdit = False
    Me.fd_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_conto.OptionsColumn.ReadOnly = True
    Me.fd_conto.OptionsFilter.AllowFilter = False
    Me.fd_conto.Visible = True
    Me.fd_conto.VisibleIndex = 6
    Me.fd_conto.Width = 70
    '
    'fd_descr
    '
    Me.fd_descr.AppearanceCell.Options.UseBackColor = True
    Me.fd_descr.AppearanceCell.Options.UseTextOptions = True
    Me.fd_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_descr.Caption = "Descr. clie."
    Me.fd_descr.Enabled = False
    Me.fd_descr.FieldName = "fd_descr"
    Me.fd_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_descr.Name = "fd_descr"
    Me.fd_descr.NTSRepositoryComboBox = Nothing
    Me.fd_descr.NTSRepositoryItemCheck = Nothing
    Me.fd_descr.NTSRepositoryItemText = Nothing
    Me.fd_descr.OptionsColumn.AllowEdit = False
    Me.fd_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_descr.OptionsColumn.ReadOnly = True
    Me.fd_descr.OptionsFilter.AllowFilter = False
    Me.fd_descr.Visible = True
    Me.fd_descr.VisibleIndex = 7
    Me.fd_descr.Width = 70
    '
    'fd_ornum
    '
    Me.fd_ornum.AppearanceCell.Options.UseBackColor = True
    Me.fd_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.fd_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_ornum.Caption = "N.Ord"
    Me.fd_ornum.Enabled = False
    Me.fd_ornum.FieldName = "fd_ornum"
    Me.fd_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_ornum.Name = "fd_ornum"
    Me.fd_ornum.NTSRepositoryComboBox = Nothing
    Me.fd_ornum.NTSRepositoryItemCheck = Nothing
    Me.fd_ornum.NTSRepositoryItemText = Nothing
    Me.fd_ornum.OptionsColumn.AllowEdit = False
    Me.fd_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_ornum.OptionsColumn.ReadOnly = True
    Me.fd_ornum.OptionsFilter.AllowFilter = False
    Me.fd_ornum.Visible = True
    Me.fd_ornum.VisibleIndex = 8
    '
    'fd_orserie
    '
    Me.fd_orserie.AppearanceCell.Options.UseBackColor = True
    Me.fd_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.fd_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_orserie.Caption = "Ser.Ord."
    Me.fd_orserie.Enabled = False
    Me.fd_orserie.FieldName = "fd_orserie"
    Me.fd_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_orserie.Name = "fd_orserie"
    Me.fd_orserie.NTSRepositoryComboBox = Nothing
    Me.fd_orserie.NTSRepositoryItemCheck = Nothing
    Me.fd_orserie.NTSRepositoryItemText = Nothing
    Me.fd_orserie.OptionsColumn.AllowEdit = False
    Me.fd_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_orserie.OptionsColumn.ReadOnly = True
    Me.fd_orserie.OptionsFilter.AllowFilter = False
    Me.fd_orserie.Visible = True
    Me.fd_orserie.VisibleIndex = 9
    '
    'fd_oranno
    '
    Me.fd_oranno.AppearanceCell.Options.UseBackColor = True
    Me.fd_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.fd_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_oranno.Caption = "An.Ord."
    Me.fd_oranno.Enabled = False
    Me.fd_oranno.FieldName = "fd_oranno"
    Me.fd_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_oranno.Name = "fd_oranno"
    Me.fd_oranno.NTSRepositoryComboBox = Nothing
    Me.fd_oranno.NTSRepositoryItemCheck = Nothing
    Me.fd_oranno.NTSRepositoryItemText = Nothing
    Me.fd_oranno.OptionsColumn.AllowEdit = False
    Me.fd_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_oranno.OptionsColumn.ReadOnly = True
    Me.fd_oranno.OptionsFilter.AllowFilter = False
    Me.fd_oranno.Visible = True
    Me.fd_oranno.VisibleIndex = 10
    '
    'fd_ordata
    '
    Me.fd_ordata.AppearanceCell.Options.UseBackColor = True
    Me.fd_ordata.AppearanceCell.Options.UseTextOptions = True
    Me.fd_ordata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_ordata.Caption = "Data Ord."
    Me.fd_ordata.Enabled = False
    Me.fd_ordata.FieldName = "fd_ordata"
    Me.fd_ordata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_ordata.Name = "fd_ordata"
    Me.fd_ordata.NTSRepositoryComboBox = Nothing
    Me.fd_ordata.NTSRepositoryItemCheck = Nothing
    Me.fd_ordata.NTSRepositoryItemText = Nothing
    Me.fd_ordata.OptionsColumn.AllowEdit = False
    Me.fd_ordata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_ordata.OptionsColumn.ReadOnly = True
    Me.fd_ordata.OptionsFilter.AllowFilter = False
    Me.fd_ordata.Visible = True
    Me.fd_ordata.VisibleIndex = 11
    '
    'fd_soloasa
    '
    Me.fd_soloasa.AppearanceCell.Options.UseBackColor = True
    Me.fd_soloasa.AppearanceCell.Options.UseTextOptions = True
    Me.fd_soloasa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_soloasa.Caption = "Solo a saldo"
    Me.fd_soloasa.Enabled = False
    Me.fd_soloasa.FieldName = "fd_soloasa"
    Me.fd_soloasa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_soloasa.Name = "fd_soloasa"
    Me.fd_soloasa.NTSRepositoryComboBox = Nothing
    Me.fd_soloasa.NTSRepositoryItemCheck = Nothing
    Me.fd_soloasa.NTSRepositoryItemText = Nothing
    Me.fd_soloasa.OptionsColumn.AllowEdit = False
    Me.fd_soloasa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_soloasa.OptionsColumn.ReadOnly = True
    Me.fd_soloasa.OptionsFilter.AllowFilter = False
    Me.fd_soloasa.Visible = True
    Me.fd_soloasa.VisibleIndex = 12
    '
    'fd_tdflevas
    '
    Me.fd_tdflevas.AppearanceCell.Options.UseBackColor = True
    Me.fd_tdflevas.AppearanceCell.Options.UseTextOptions = True
    Me.fd_tdflevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_tdflevas.Caption = "Evaso a saldo"
    Me.fd_tdflevas.Enabled = False
    Me.fd_tdflevas.FieldName = "fd_tdflevas"
    Me.fd_tdflevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_tdflevas.Name = "fd_tdflevas"
    Me.fd_tdflevas.NTSRepositoryComboBox = Nothing
    Me.fd_tdflevas.NTSRepositoryItemCheck = Nothing
    Me.fd_tdflevas.NTSRepositoryItemText = Nothing
    Me.fd_tdflevas.OptionsColumn.AllowEdit = False
    Me.fd_tdflevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_tdflevas.OptionsColumn.ReadOnly = True
    Me.fd_tdflevas.OptionsFilter.AllowFilter = False
    Me.fd_tdflevas.Visible = True
    Me.fd_tdflevas.VisibleIndex = 13
    '
    'fd_codpaga
    '
    Me.fd_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.fd_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.fd_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_codpaga.Caption = "C.Pag."
    Me.fd_codpaga.Enabled = False
    Me.fd_codpaga.FieldName = "fd_codpaga"
    Me.fd_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_codpaga.Name = "fd_codpaga"
    Me.fd_codpaga.NTSRepositoryComboBox = Nothing
    Me.fd_codpaga.NTSRepositoryItemCheck = Nothing
    Me.fd_codpaga.NTSRepositoryItemText = Nothing
    Me.fd_codpaga.OptionsColumn.AllowEdit = False
    Me.fd_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_codpaga.OptionsColumn.ReadOnly = True
    Me.fd_codpaga.OptionsFilter.AllowFilter = False
    Me.fd_codpaga.Visible = True
    Me.fd_codpaga.VisibleIndex = 14
    '
    'fd_despaga
    '
    Me.fd_despaga.AppearanceCell.Options.UseBackColor = True
    Me.fd_despaga.AppearanceCell.Options.UseTextOptions = True
    Me.fd_despaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_despaga.Caption = "Des. Pag."
    Me.fd_despaga.Enabled = False
    Me.fd_despaga.FieldName = "fd_despaga"
    Me.fd_despaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_despaga.Name = "fd_despaga"
    Me.fd_despaga.NTSRepositoryComboBox = Nothing
    Me.fd_despaga.NTSRepositoryItemCheck = Nothing
    Me.fd_despaga.NTSRepositoryItemText = Nothing
    Me.fd_despaga.OptionsColumn.AllowEdit = False
    Me.fd_despaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_despaga.OptionsColumn.ReadOnly = True
    Me.fd_despaga.OptionsFilter.AllowFilter = False
    Me.fd_despaga.Visible = True
    Me.fd_despaga.VisibleIndex = 15
    '
    'fd_codtpbf
    '
    Me.fd_codtpbf.AppearanceCell.Options.UseBackColor = True
    Me.fd_codtpbf.AppearanceCell.Options.UseTextOptions = True
    Me.fd_codtpbf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_codtpbf.Caption = "Tipo bf"
    Me.fd_codtpbf.Enabled = False
    Me.fd_codtpbf.FieldName = "fd_codtpbf"
    Me.fd_codtpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_codtpbf.Name = "fd_codtpbf"
    Me.fd_codtpbf.NTSRepositoryComboBox = Nothing
    Me.fd_codtpbf.NTSRepositoryItemCheck = Nothing
    Me.fd_codtpbf.NTSRepositoryItemText = Nothing
    Me.fd_codtpbf.OptionsColumn.AllowEdit = False
    Me.fd_codtpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_codtpbf.OptionsColumn.ReadOnly = True
    Me.fd_codtpbf.OptionsFilter.AllowFilter = False
    Me.fd_codtpbf.Visible = True
    Me.fd_codtpbf.VisibleIndex = 16
    '
    'fd_destpbf
    '
    Me.fd_destpbf.AppearanceCell.Options.UseBackColor = True
    Me.fd_destpbf.AppearanceCell.Options.UseTextOptions = True
    Me.fd_destpbf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_destpbf.Caption = "Des. Tipo bf"
    Me.fd_destpbf.Enabled = False
    Me.fd_destpbf.FieldName = "fd_destpbf"
    Me.fd_destpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_destpbf.Name = "fd_destpbf"
    Me.fd_destpbf.NTSRepositoryComboBox = Nothing
    Me.fd_destpbf.NTSRepositoryItemCheck = Nothing
    Me.fd_destpbf.NTSRepositoryItemText = Nothing
    Me.fd_destpbf.OptionsColumn.AllowEdit = False
    Me.fd_destpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_destpbf.OptionsColumn.ReadOnly = True
    Me.fd_destpbf.OptionsFilter.AllowFilter = False
    Me.fd_destpbf.Visible = True
    Me.fd_destpbf.VisibleIndex = 17
    '
    'FRMVEGNBF
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grGnbf)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVEGNBF"
    Me.NTSLastControlFocussed = Me.grGnbf
    Me.Text = "GENERAZIONE DOCUMENTI DA NOTE DI PRELIEVO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGnbf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGnbf, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEGNBF", "BEVEGNBF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGnbf = CType(oTmp, CLEVEGNBF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEGNBF", strRemoteServer, strRemotePort)
    AddHandler oCleGnbf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGnbf.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGnbf.NTSSetParam(oMenu, "GENERA ORDINI DA PROP. D'ORDINE")
      fd_flsel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128607611685625000, "Seleziona"), "S", "N")
      fd_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685781250, "Anno"), "0", 4, 1900, 2099)
      fd_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685937500, "Numero"), "0", 1, 0, 999999999)
      fd_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686093750, "Serie"), CLN__STD.SerieMaxLen, False)
      fd_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128607611686250000, "Data"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      fd_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611686406250, "Totale doc."), oApp.FormatImporti, 9, -999999999, 999999999)
      fd_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128607611686562500, "Fornitore"), tabanagra)
      fd_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686718750, "Descr. forn."), 0, True)
      fd_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128714060589310375, "N.Ord"), "0", 9, 0, 999999999)
      fd_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128714060589466613, "Serie ordine"), CLN__STD.SerieMaxLen, True)
      fd_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128714060589622851, "Anno ordine"), "0", 4, 0, 9999)
      fd_ordata.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128714060589779089, "Data Ord."), True)
      fd_soloasa.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128714060589935327, "Solo a saldo"), "S", "N")
      fd_tdflevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128714060590091565, "Evaso a saldo"), "S", "N")
      fd_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128714060590247803, "C.Pag."), tabpaga)
      fd_despaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128714060590404041, "Des. Pag."), 0, True)
      fd_codtpbf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128714060590560279, "Tipo bolla/fattura"), tabtpbf)
      fd_destpbf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128714060590716517, "Des. Tipo bolla/fattura"), 0, True)
      grvGnbf.NTSAllowInsert = False
      grvGnbf.NTSAllowDelete = False

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
  Public Overridable Sub FRMVEGNBF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      grGnbf.Visible = False

      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then oCleGnbf.bModExtTCO = True Else oCleGnbf.bModExtTCO = False

      '-------------------
      'verifico che tabpeve e tabpeac siano compilate
      If Not oCleGnbf.InitExt() Then
        Me.Close()
        Return
      End If

      oCleGnbf.lIIGegnbf = oMenu.GetTblInstId("GEGNBF", False)
      oCleGnbf.lIIGegnbmm = oMenu.GetTblInstId("GEGNBMM", False)
      oCleGnbf.lIIGegnbmo = oMenu.GetTblInstId("GEGNBMO", False)
      oCleGnbf.lIIGegnbtd = oMenu.GetTblInstId("GEGNBTD", False)

      oCleGnbf.Apri(dsGnbf)

      'Recupero flags di ricalcolo
      oCleGnbf.bRicalcPrez = CBool(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "RicalcolaPrezzi", "0", " ", "0"))
      oCleGnbf.bRicalcScon = CBool(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "RicalcolaSconti", "0", " ", "0"))
      oCleGnbf.bRicalcProv = CBool(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "RicalcolaProvvigioni", "0", " ", "0"))

      '---
      ' tes se c'è il modulo PM
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModPM) Then oCleGnbf.bModPM = True Else oCleGnbf.bModPM = False

      'oCleGnbf.lIItttasks = oMenu.GetTblInstId("TTTASKS", False)
      'oCleGnbf.lIIttproesebappo = oMenu.GetTblInstId("TTPROESEC", False)

      oCleGnbf.bScriviRigheZero = CBool(NTSCInt(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "ScriviRighe0", "0", " ", "0")))
      oCleGnbf.bEreditaMatricole = CBool(NTSCInt(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "EreditaMatricole", "0", " ", "0")))
      oCleGnbf.bValMinSoloImponib = CBool(NTSCInt(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "ValMinSoloImponibile", "0", " ", "0")))
      oCleGnbf.bSommaPesoColli = CBool(NTSCInt(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "RaggruppaSommaPesiColli", "0", " ", "0")))
      oCleGnbf.lIncrementoContatoreRiga = NTSCInt(oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "IncremContatoreRiga", "1", " ", "1"))

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

  Public Overridable Sub FRMVEGNBF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      oMenu.ResetTblInstId("GEGNBF", False, oCleGnbf.lIIGegnbf)
      oMenu.ResetTblInstId("GEGNBMM", False, oCleGnbf.lIIGegnbmm)
      oMenu.ResetTblInstId("GEGNBMO", False, oCleGnbf.lIIGegnbmo)
      oMenu.ResetTblInstId("GEGNBTD", False, oCleGnbf.lIIGegnbtd)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEGNBF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGnbf.Dispose()
      dsGnbf.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmSebp As FRMVESEBP = Nothing
    Dim frmSval As FRM__SVA2 = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      '--------------------------
      'visualizzo la form per la selezione degli estremi del documento da generare
      frmSebp = CType(NTSNewFormModal("FRMVESEBP"), FRMVESEBP)
      frmSebp.Init(oMenu, Nothing, DittaCorrente)
      frmSebp.InitEntity(oCleGnbf)
      frmSebp.ShowDialog()
      If frmSebp.bOk = False Then
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      oMenu.ResetTblInstId("GEGNBF", False, oCleGnbf.lIIGegnbf)
      oMenu.ResetTblInstId("GEGNBMM", False, oCleGnbf.lIIGegnbmm)
      oMenu.ResetTblInstId("GEGNBMO", False, oCleGnbf.lIIGegnbmo)
      oMenu.ResetTblInstId("GEGNBTD", False, oCleGnbf.lIIGegnbtd)

      '----------------------
      'Chiede come operare con le valute
      frmSval = CType(NTSNewFormModal("FRM__SVA2"), FRM__SVA2)
      frmSval.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmSval.opValuta0.Enabled = False
      frmSval.opValuta1.Checked = True
      frmSval.ShowDialog()
      oCleGnbf.nSvalOpzione = frmSval.nOptionOut

      oCleGnbf.FileNuovo()

      oCleGnbf.Apri(dsGnbf)

      If dsGnbf.Tables("GEGNBF").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128713886673735846, "Non ci sono Note di prelievo con queste caratteristiche."))
        Return
      End If

      oCleGnbf.CreaGegnbmm()
      oCleGnbf.SettaOrdiniNonASaldo()

      oCleGnbf.Apri(dsGnbf)

      If dsGnbf.Tables("GEGNBF").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128713906326085803, "Non ci sono Note di prelievo con queste carrateristiche."))
        Return
      End If

      GctlSetVisEnab(grGnbf, True)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dcGnbf.DataSource = dsGnbf.Tables("GEGNBF")
      dsGnbf.AcceptChanges()
      grGnbf.DataSource = dcGnbf

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSval Is Nothing Then frmSval.Dispose()
      frmSval = Nothing
      If Not frmSebp Is Nothing Then frmSebp.Dispose()
      frmSebp = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If grvGnbf.RowCount = 0 Then Return
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
      If grvGnbf.RowCount = 0 Then Return
      Salva()
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
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub grvGnbf_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGnbf.NTSBeforeRowUpdate
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
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGnbf.NTSSalvaRigaCorrente(dcGnbf, oCleGnbf.RecordIsChanged, False)

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleGnbf.Salva(False) Then
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
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim dtrT() As DataRow = Nothing
    Dim nGiro As Integer = 0
    Dim strTmp As String = ""
    Dim frmDtac As FRMVEDTAC = Nothing
    Dim dttTmp As New DataTable
    Dim strkey2 As String = ""
    Dim strNomeReport As String = ""
    Try
      '--------------------------------------------------
      'test pre-stampa
      If dsGnbf.Tables.Count = 0 Then Return
      If dsGnbf.Tables("GEGNBF").Rows.Count = 0 Then Return
      dsGnbf.Tables("GEGNBF").AcceptChanges()
      dtrT = dsGnbf.Tables("GEGNBF").Select("fd_flsel = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611101258125000, "Non è stata selezionato nessun ordine."))
        Return
      End If

      frmDtac = CType(NTSNewFormModal("FRMVEDTAC"), FRMVEDTAC)
      frmDtac.Init(oMenu, Nothing, DittaCorrente)
      frmDtac.InitEntity(oCleGnbf)
      frmDtac.ShowDialog()
      If frmDtac.bOk = False Then
        Return
      End If

      If Not (oCleGnbf.strDtacTipork = "A" Or oCleGnbf.strDtacTipork = "B" Or oCleGnbf.strDtacTipork = "C" Or oCleGnbf.strDtacTipork = "Z") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128720839309094856, "Tipo documento da emettere non ammesso."))
        Return
      End If

      oCleGnbf.GetTestateTemp(dttTmp)

      If Not oCleGnbf.LogStart("BNVEGNBF", "GENERAZIONE DOCUMENTI DA NOTE DI PRELIEVO") Then Exit Sub

      '--------------------------------------------------------------------------------------------------------------
      If oCleGnbf.bDtacRaggruppa = False Then
        For i = 0 To dttTmp.Rows.Count - 1
          oCleGnbf.LogWrite(oApp.Tr(Me, 128843566209814000, "Creazione documento |" & (i + 1).ToString & "| di |" & dttTmp.Rows.Count.ToString & "| in corso ..."), False)
          oCleGnbf.CreaDocDaNotaPre(dttTmp.Rows(i))
        Next
      Else
        oCleGnbf.CreaDocDaNotaPreRagg(dttTmp)
      End If
      '--------------------------------------------------------------------------------------------------------------

      oCleGnbf.LogStop()

      If oCleGnbf.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleGnbf.LogFileName)
        End If
      End If

      If oCleGnbf.bVal Then
        strkey2 = "Reports3"
      ElseIf oCleGnbf.bScorp Then
        strkey2 = "Reports2"
      Else
        strkey2 = "Reports1"
      End If
      'Preimposta il nome del report da stampare
      Select Case oCleGnbf.strDtacTipork
        Case "A", "C"
          If oCleGnbf.bVal Then
            strNomeReport = "BSVEFATV.RPT"
          ElseIf oCleGnbf.bScorp Then
            strNomeReport = "BSVEFATC.RPT"
          Else
            strNomeReport = "BSVEFATI.RPT"
          End If
        Case "B", "Z"
          If oCleGnbf.bVal Then
            strNomeReport = "BSVEBOLV.RPT"
          ElseIf oCleGnbf.bScorp Then
            strNomeReport = "BSVEBOLC.RPT"
          Else
            strNomeReport = "BSVEBOLL.RPT"
          End If
      End Select
      oCleGnbf.bReprintDoc = CBool(NTSCInt(oMenu.GetSettingBus("Bsvegnbf", "Opzioni", ".", "ConfermaRistampa", "0", " ", "0")))
      oCleGnbf.bRistampato = False

      '-----------------------------------------------------------------------------------------
      '--- Torna in stato 0 prima del lancio delle stampe, così, in caso di errore, non si può
      '--- rigenerare i documenti provocando errore di chiave duplicata
      '-----------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("GEGNBF", False, oCleGnbf.lIIGegnbf)
      oMenu.ResetTblInstId("GEGNBMM", False, oCleGnbf.lIIGegnbmm)
      oMenu.ResetTblInstId("GEGNBMO", False, oCleGnbf.lIIGegnbmo)
      oMenu.ResetTblInstId("GEGNBTD", False, oCleGnbf.lIIGegnbtd)

Reprint:
      dtrT = dsGnbf.Tables("GEGNBF").Select(strTmp)
      If dtrT.Length > 0 Then
        '--------------------------------------------------
        'preparo il motore di stampa      
        strCrpe = "{testmag.codditt} = '" & DittaCorrente & "'" & _
          " And {testmag.tm_tipork} = '" & oCleGnbf.strDtacTipork & "'" & _
          " AND {testmag.tm_anno} = " & oCleGnbf.nDtacAnno & _
          " AND {testmag.tm_serie} = '" & oCleGnbf.strDtacSerie & "'" & _
          " AND {movmag.mm_stasino} <> 'N'"
        strCrpe += " and {testmag.tm_numdoc} In " & oCleGnbf.lDtacNumdoc & " To " & oCleGnbf.lANumero

        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEBOLL", strkey2, oCleGnbf.strDtacTipork, 0, nDestin, strNomeReport, False, "Stampa Ordine", False)
        If nPjob Is Nothing Then Return

        '--------------------------------------------------
        'lancio tutti gli eventuali reports (gestisce già il multireport)
        For i = 1 To UBound(CType(nPjob, Array), 2)
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Next
      End If    'If dtrT.Length > 0 Then

      If oCleGnbf.bReprintDoc And Not oCleGnbf.bRistampato Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128715674754166396, "Ristampare il documento?")) = Windows.Forms.DialogResult.Yes Then
          oCleGnbf.bRistampato = True
          GoTo Reprint
        End If
      End If

      grGnbf.Visible = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDtac Is Nothing Then frmDtac.Dispose()
      frmDtac = Nothing
      oCleGnbf.LogStop()
    End Try
  End Sub

End Class
