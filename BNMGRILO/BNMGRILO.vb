Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGRILO
  Private Moduli_P As Integer = bsModMG
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

  Public oCleRilo As CLEMGRILO
  Public oCallParams As CLE__CLDP
  Public dsRilo As DataSet
  Public dcRilo As BindingSource = New BindingSource()
  Public dsCp As DataSet
  Public dcCp As BindingSource = New BindingSource()

  Public dttRecent As New DataTable()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbFldo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApridoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grRilo As NTSInformatica.NTSGrid

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGRILO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbCerca2 = New NTSInformatica.NTSBarButtonItem
    Me.tlbFldo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApridoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbLottoMatr = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grRilo = New NTSInformatica.NTSGrid
    Me.grvRilo = New NTSInformatica.NTSGridView
    Me.tm_tipork = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.mm_riga = New NTSInformatica.NTSGridColumn
    Me.tm_conto = New NTSInformatica.NTSGridColumn
    Me.an_descr1 = New NTSInformatica.NTSGridColumn
    Me.km_causale = New NTSInformatica.NTSGridColumn
    Me.tb_descaum = New NTSInformatica.NTSGridColumn
    Me.mm_ump = New NTSInformatica.NTSGridColumn
    Me.mm_quant = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.omm_tipork = New NTSInformatica.NTSGridColumn
    Me.oxx_lottox = New NTSInformatica.NTSGridColumn
    Me.omm_codart = New NTSInformatica.NTSGridColumn
    Me.omm_descr = New NTSInformatica.NTSGridColumn
    Me.omm_riga = New NTSInformatica.NTSGridColumn
    Me.omm_ump = New NTSInformatica.NTSGridColumn
    Me.omm_quant = New NTSInformatica.NTSGridColumn
    Me.pnSx = New NTSInformatica.NTSPanel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbMatricola = New NTSInformatica.NTSLabel
    Me.edMatricola = New NTSInformatica.NTSTextBoxStr
    Me.lbNota2 = New NTSInformatica.NTSLabel
    Me.lbDesart = New NTSInformatica.NTSLabel
    Me.cbRecent = New NTSInformatica.NTSComboBox
    Me.lbRecent = New NTSInformatica.NTSLabel
    Me.lbNota = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.edLotto = New NTSInformatica.NTSTextBoxStr
    Me.lbLotto = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.NtsSplitter1 = New NTSInformatica.NTSSplitter
    Me.pnDx = New NTSInformatica.NTSPanel
    Me.grCp = New NTSInformatica.NTSGrid
    Me.grvCp = New NTSInformatica.NTSGridView
    Me.omma_quant = New NTSInformatica.NTSGridColumn
    Me.omma_matric = New NTSInformatica.NTSGridColumn
    Me.pnMain = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grRilo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvRilo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSx.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edMatricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbRecent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDx.SuspendLayout()
    CType(Me.grCp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbFldo, Me.tlbApridoc, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbCerca2, Me.tlbStrumenti, Me.tlbLottoMatr})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCerca2), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFldo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApridoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Visualizza movimenti"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbCerca2
    '
    Me.tlbCerca2.Caption = "Cerca articolo/lotto di riga di scarico"
    Me.tlbCerca2.Glyph = CType(resources.GetObject("tlbCerca2.Glyph"), System.Drawing.Image)
    Me.tlbCerca2.Id = 17
    Me.tlbCerca2.Name = "tlbCerca2"
    Me.tlbCerca2.Visible = True
    '
    'tlbFldo
    '
    Me.tlbFldo.Caption = "Flusso documentale"
    Me.tlbFldo.Glyph = CType(resources.GetObject("tlbFldo.Glyph"), System.Drawing.Image)
    Me.tlbFldo.Id = 1
    Me.tlbFldo.Name = "tlbFldo"
    Me.tlbFldo.Visible = True
    '
    'tlbApridoc
    '
    Me.tlbApridoc.Caption = "Apri documento"
    Me.tlbApridoc.Glyph = CType(resources.GetObject("tlbApridoc.Glyph"), System.Drawing.Image)
    Me.tlbApridoc.Id = 2
    Me.tlbApridoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApridoc.Name = "tlbApridoc"
    Me.tlbApridoc.Visible = True
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
    Me.tlbStrumenti.Id = 18
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLottoMatr)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbLottoMatr
    '
    Me.tlbLottoMatr.Caption = "Analisi per lotto e/o matricola"
    Me.tlbLottoMatr.Id = 19
    Me.tlbLottoMatr.Name = "tlbLottoMatr"
    Me.tlbLottoMatr.NTSIsCheckBox = True
    Me.tlbLottoMatr.Visible = True
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
    'grRilo
    '
    Me.grRilo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grRilo.EmbeddedNavigator.Name = ""
    Me.grRilo.Location = New System.Drawing.Point(0, 0)
    Me.grRilo.MainView = Me.grvRilo
    Me.grRilo.Name = "grRilo"
    Me.grRilo.Size = New System.Drawing.Size(468, 334)
    Me.grRilo.TabIndex = 5
    Me.grRilo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRilo})
    '
    'grvRilo
    '
    Me.grvRilo.ActiveFilterEnabled = False
    Me.grvRilo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tm_tipork, Me.tm_datdoc, Me.tm_numdoc, Me.tm_serie, Me.mm_riga, Me.tm_conto, Me.an_descr1, Me.km_causale, Me.tb_descaum, Me.mm_ump, Me.mm_quant, Me.tm_riferim})
    Me.grvRilo.Enabled = True
    Me.grvRilo.GridControl = Me.grRilo
    Me.grvRilo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvRilo.Name = "grvRilo"
    Me.grvRilo.NTSAllowDelete = True
    Me.grvRilo.NTSAllowInsert = True
    Me.grvRilo.NTSAllowUpdate = True
    Me.grvRilo.NTSMenuContext = Nothing
    Me.grvRilo.OptionsCustomization.AllowRowSizing = True
    Me.grvRilo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvRilo.OptionsNavigation.UseTabKey = False
    Me.grvRilo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvRilo.OptionsView.ColumnAutoWidth = False
    Me.grvRilo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvRilo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvRilo.OptionsView.ShowGroupPanel = False
    Me.grvRilo.RowHeight = 14
    '
    'tm_tipork
    '
    Me.tm_tipork.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipork.Caption = "Tipo doc"
    Me.tm_tipork.Enabled = True
    Me.tm_tipork.FieldName = "tm_tipork"
    Me.tm_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipork.Name = "tm_tipork"
    Me.tm_tipork.NTSRepositoryComboBox = Nothing
    Me.tm_tipork.NTSRepositoryItemCheck = Nothing
    Me.tm_tipork.NTSRepositoryItemMemo = Nothing
    Me.tm_tipork.NTSRepositoryItemText = Nothing
    Me.tm_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipork.OptionsFilter.AllowFilter = False
    Me.tm_tipork.Visible = True
    Me.tm_tipork.VisibleIndex = 0
    '
    'tm_datdoc
    '
    Me.tm_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datdoc.Caption = "Data doc"
    Me.tm_datdoc.Enabled = True
    Me.tm_datdoc.FieldName = "tm_datdoc"
    Me.tm_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datdoc.Name = "tm_datdoc"
    Me.tm_datdoc.NTSRepositoryComboBox = Nothing
    Me.tm_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_datdoc.NTSRepositoryItemText = Nothing
    Me.tm_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datdoc.OptionsFilter.AllowFilter = False
    Me.tm_datdoc.Visible = True
    Me.tm_datdoc.VisibleIndex = 1
    '
    'tm_numdoc
    '
    Me.tm_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numdoc.Caption = "Num. doc"
    Me.tm_numdoc.Enabled = True
    Me.tm_numdoc.FieldName = "tm_numdoc"
    Me.tm_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numdoc.Name = "tm_numdoc"
    Me.tm_numdoc.NTSRepositoryComboBox = Nothing
    Me.tm_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_numdoc.NTSRepositoryItemText = Nothing
    Me.tm_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numdoc.OptionsFilter.AllowFilter = False
    Me.tm_numdoc.Visible = True
    Me.tm_numdoc.VisibleIndex = 2
    '
    'tm_serie
    '
    Me.tm_serie.AppearanceCell.Options.UseBackColor = True
    Me.tm_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tm_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_serie.Caption = "Serie doc"
    Me.tm_serie.Enabled = True
    Me.tm_serie.FieldName = "tm_serie"
    Me.tm_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_serie.Name = "tm_serie"
    Me.tm_serie.NTSRepositoryComboBox = Nothing
    Me.tm_serie.NTSRepositoryItemCheck = Nothing
    Me.tm_serie.NTSRepositoryItemMemo = Nothing
    Me.tm_serie.NTSRepositoryItemText = Nothing
    Me.tm_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_serie.OptionsFilter.AllowFilter = False
    Me.tm_serie.Visible = True
    Me.tm_serie.VisibleIndex = 3
    '
    'mm_riga
    '
    Me.mm_riga.AppearanceCell.Options.UseBackColor = True
    Me.mm_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mm_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_riga.Caption = "Riga"
    Me.mm_riga.Enabled = True
    Me.mm_riga.FieldName = "mm_riga"
    Me.mm_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_riga.Name = "mm_riga"
    Me.mm_riga.NTSRepositoryComboBox = Nothing
    Me.mm_riga.NTSRepositoryItemCheck = Nothing
    Me.mm_riga.NTSRepositoryItemMemo = Nothing
    Me.mm_riga.NTSRepositoryItemText = Nothing
    Me.mm_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_riga.OptionsFilter.AllowFilter = False
    Me.mm_riga.Visible = True
    Me.mm_riga.VisibleIndex = 4
    '
    'tm_conto
    '
    Me.tm_conto.AppearanceCell.Options.UseBackColor = True
    Me.tm_conto.AppearanceCell.Options.UseTextOptions = True
    Me.tm_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_conto.Caption = "conto C/F"
    Me.tm_conto.Enabled = True
    Me.tm_conto.FieldName = "tm_conto"
    Me.tm_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_conto.Name = "tm_conto"
    Me.tm_conto.NTSRepositoryComboBox = Nothing
    Me.tm_conto.NTSRepositoryItemCheck = Nothing
    Me.tm_conto.NTSRepositoryItemMemo = Nothing
    Me.tm_conto.NTSRepositoryItemText = Nothing
    Me.tm_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_conto.OptionsFilter.AllowFilter = False
    Me.tm_conto.Visible = True
    Me.tm_conto.VisibleIndex = 5
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Descr. conto"
    Me.an_descr1.Enabled = True
    Me.an_descr1.FieldName = "an_descr1"
    Me.an_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr1.Name = "an_descr1"
    Me.an_descr1.NTSRepositoryComboBox = Nothing
    Me.an_descr1.NTSRepositoryItemCheck = Nothing
    Me.an_descr1.NTSRepositoryItemMemo = Nothing
    Me.an_descr1.NTSRepositoryItemText = Nothing
    Me.an_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_descr1.OptionsFilter.AllowFilter = False
    Me.an_descr1.Visible = True
    Me.an_descr1.VisibleIndex = 6
    '
    'km_causale
    '
    Me.km_causale.AppearanceCell.Options.UseBackColor = True
    Me.km_causale.AppearanceCell.Options.UseTextOptions = True
    Me.km_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_causale.Caption = "Causale"
    Me.km_causale.Enabled = True
    Me.km_causale.FieldName = "km_causale"
    Me.km_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_causale.Name = "km_causale"
    Me.km_causale.NTSRepositoryComboBox = Nothing
    Me.km_causale.NTSRepositoryItemCheck = Nothing
    Me.km_causale.NTSRepositoryItemMemo = Nothing
    Me.km_causale.NTSRepositoryItemText = Nothing
    Me.km_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_causale.OptionsFilter.AllowFilter = False
    Me.km_causale.Visible = True
    Me.km_causale.VisibleIndex = 7
    '
    'tb_descaum
    '
    Me.tb_descaum.AppearanceCell.Options.UseBackColor = True
    Me.tb_descaum.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descaum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descaum.Caption = "Descr. causale"
    Me.tb_descaum.Enabled = True
    Me.tb_descaum.FieldName = "tb_descaum"
    Me.tb_descaum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descaum.Name = "tb_descaum"
    Me.tb_descaum.NTSRepositoryComboBox = Nothing
    Me.tb_descaum.NTSRepositoryItemCheck = Nothing
    Me.tb_descaum.NTSRepositoryItemMemo = Nothing
    Me.tb_descaum.NTSRepositoryItemText = Nothing
    Me.tb_descaum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descaum.OptionsFilter.AllowFilter = False
    Me.tb_descaum.Visible = True
    Me.tb_descaum.VisibleIndex = 8
    '
    'mm_ump
    '
    Me.mm_ump.AppearanceCell.Options.UseBackColor = True
    Me.mm_ump.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ump.Caption = "UMP"
    Me.mm_ump.Enabled = True
    Me.mm_ump.FieldName = "mm_ump"
    Me.mm_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ump.Name = "mm_ump"
    Me.mm_ump.NTSRepositoryComboBox = Nothing
    Me.mm_ump.NTSRepositoryItemCheck = Nothing
    Me.mm_ump.NTSRepositoryItemMemo = Nothing
    Me.mm_ump.NTSRepositoryItemText = Nothing
    Me.mm_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ump.OptionsFilter.AllowFilter = False
    Me.mm_ump.Visible = True
    Me.mm_ump.VisibleIndex = 9
    '
    'mm_quant
    '
    Me.mm_quant.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant.Caption = "Qta"
    Me.mm_quant.Enabled = True
    Me.mm_quant.FieldName = "mm_quant"
    Me.mm_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant.Name = "mm_quant"
    Me.mm_quant.NTSRepositoryComboBox = Nothing
    Me.mm_quant.NTSRepositoryItemCheck = Nothing
    Me.mm_quant.NTSRepositoryItemMemo = Nothing
    Me.mm_quant.NTSRepositoryItemText = Nothing
    Me.mm_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant.OptionsFilter.AllowFilter = False
    Me.mm_quant.Visible = True
    Me.mm_quant.VisibleIndex = 10
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 11
    '
    'omm_tipork
    '
    Me.omm_tipork.AppearanceCell.Options.UseBackColor = True
    Me.omm_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.omm_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_tipork.Caption = "Tipo doc"
    Me.omm_tipork.Enabled = True
    Me.omm_tipork.FieldName = "omm_tipork"
    Me.omm_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_tipork.Name = "omm_tipork"
    Me.omm_tipork.NTSRepositoryComboBox = Nothing
    Me.omm_tipork.NTSRepositoryItemCheck = Nothing
    Me.omm_tipork.NTSRepositoryItemMemo = Nothing
    Me.omm_tipork.NTSRepositoryItemText = Nothing
    Me.omm_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_tipork.OptionsFilter.AllowFilter = False
    Me.omm_tipork.Visible = True
    Me.omm_tipork.VisibleIndex = 0
    '
    'oxx_lottox
    '
    Me.oxx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.oxx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.oxx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.oxx_lottox.Caption = "Lotto"
    Me.oxx_lottox.Enabled = True
    Me.oxx_lottox.FieldName = "oxx_lottox"
    Me.oxx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.oxx_lottox.Name = "oxx_lottox"
    Me.oxx_lottox.NTSRepositoryComboBox = Nothing
    Me.oxx_lottox.NTSRepositoryItemCheck = Nothing
    Me.oxx_lottox.NTSRepositoryItemMemo = Nothing
    Me.oxx_lottox.NTSRepositoryItemText = Nothing
    Me.oxx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.oxx_lottox.OptionsFilter.AllowFilter = False
    Me.oxx_lottox.Visible = True
    Me.oxx_lottox.VisibleIndex = 4
    '
    'omm_codart
    '
    Me.omm_codart.AppearanceCell.Options.UseBackColor = True
    Me.omm_codart.AppearanceCell.Options.UseTextOptions = True
    Me.omm_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_codart.Caption = "Cod. articolo"
    Me.omm_codart.Enabled = True
    Me.omm_codart.FieldName = "omm_codart"
    Me.omm_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_codart.Name = "omm_codart"
    Me.omm_codart.NTSRepositoryComboBox = Nothing
    Me.omm_codart.NTSRepositoryItemCheck = Nothing
    Me.omm_codart.NTSRepositoryItemMemo = Nothing
    Me.omm_codart.NTSRepositoryItemText = Nothing
    Me.omm_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_codart.OptionsFilter.AllowFilter = False
    Me.omm_codart.Visible = True
    Me.omm_codart.VisibleIndex = 2
    '
    'omm_descr
    '
    Me.omm_descr.AppearanceCell.Options.UseBackColor = True
    Me.omm_descr.AppearanceCell.Options.UseTextOptions = True
    Me.omm_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_descr.Caption = "Descr. articolo"
    Me.omm_descr.Enabled = True
    Me.omm_descr.FieldName = "omm_descr"
    Me.omm_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_descr.Name = "omm_descr"
    Me.omm_descr.NTSRepositoryComboBox = Nothing
    Me.omm_descr.NTSRepositoryItemCheck = Nothing
    Me.omm_descr.NTSRepositoryItemMemo = Nothing
    Me.omm_descr.NTSRepositoryItemText = Nothing
    Me.omm_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_descr.OptionsFilter.AllowFilter = False
    Me.omm_descr.Visible = True
    Me.omm_descr.VisibleIndex = 3
    '
    'omm_riga
    '
    Me.omm_riga.AppearanceCell.Options.UseBackColor = True
    Me.omm_riga.AppearanceCell.Options.UseTextOptions = True
    Me.omm_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_riga.Caption = "Riga doc"
    Me.omm_riga.Enabled = True
    Me.omm_riga.FieldName = "omm_riga"
    Me.omm_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_riga.Name = "omm_riga"
    Me.omm_riga.NTSRepositoryComboBox = Nothing
    Me.omm_riga.NTSRepositoryItemCheck = Nothing
    Me.omm_riga.NTSRepositoryItemMemo = Nothing
    Me.omm_riga.NTSRepositoryItemText = Nothing
    Me.omm_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_riga.OptionsFilter.AllowFilter = False
    Me.omm_riga.Visible = True
    Me.omm_riga.VisibleIndex = 1
    '
    'omm_ump
    '
    Me.omm_ump.AppearanceCell.Options.UseBackColor = True
    Me.omm_ump.AppearanceCell.Options.UseTextOptions = True
    Me.omm_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_ump.Caption = "UMP"
    Me.omm_ump.Enabled = True
    Me.omm_ump.FieldName = "omm_ump"
    Me.omm_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_ump.Name = "omm_ump"
    Me.omm_ump.NTSRepositoryComboBox = Nothing
    Me.omm_ump.NTSRepositoryItemCheck = Nothing
    Me.omm_ump.NTSRepositoryItemMemo = Nothing
    Me.omm_ump.NTSRepositoryItemText = Nothing
    Me.omm_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_ump.OptionsFilter.AllowFilter = False
    Me.omm_ump.Visible = True
    Me.omm_ump.VisibleIndex = 5
    '
    'omm_quant
    '
    Me.omm_quant.AppearanceCell.Options.UseBackColor = True
    Me.omm_quant.AppearanceCell.Options.UseTextOptions = True
    Me.omm_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omm_quant.Caption = "Qta"
    Me.omm_quant.Enabled = True
    Me.omm_quant.FieldName = "omm_quant"
    Me.omm_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omm_quant.Name = "omm_quant"
    Me.omm_quant.NTSRepositoryComboBox = Nothing
    Me.omm_quant.NTSRepositoryItemCheck = Nothing
    Me.omm_quant.NTSRepositoryItemMemo = Nothing
    Me.omm_quant.NTSRepositoryItemText = Nothing
    Me.omm_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omm_quant.OptionsFilter.AllowFilter = False
    Me.omm_quant.Visible = True
    Me.omm_quant.VisibleIndex = 6
    '
    'pnSx
    '
    Me.pnSx.AllowDrop = True
    Me.pnSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSx.Appearance.Options.UseBackColor = True
    Me.pnSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSx.Controls.Add(Me.grRilo)
    Me.pnSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSx.Location = New System.Drawing.Point(0, 0)
    Me.pnSx.Name = "pnSx"
    Me.pnSx.Size = New System.Drawing.Size(468, 334)
    Me.pnSx.TabIndex = 6
    Me.pnSx.Text = "NtsPanel1"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbMatricola)
    Me.pnTop.Controls.Add(Me.edMatricola)
    Me.pnTop.Controls.Add(Me.lbNota2)
    Me.pnTop.Controls.Add(Me.lbDesart)
    Me.pnTop.Controls.Add(Me.cbRecent)
    Me.pnTop.Controls.Add(Me.lbRecent)
    Me.pnTop.Controls.Add(Me.lbNota)
    Me.pnTop.Controls.Add(Me.edCodart)
    Me.pnTop.Controls.Add(Me.edLotto)
    Me.pnTop.Controls.Add(Me.lbLotto)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(648, 78)
    Me.pnTop.TabIndex = 9
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbMatricola
    '
    Me.lbMatricola.AutoSize = True
    Me.lbMatricola.BackColor = System.Drawing.Color.Transparent
    Me.lbMatricola.Location = New System.Drawing.Point(162, 30)
    Me.lbMatricola.Name = "lbMatricola"
    Me.lbMatricola.NTSDbField = ""
    Me.lbMatricola.Size = New System.Drawing.Size(50, 13)
    Me.lbMatricola.TabIndex = 10
    Me.lbMatricola.Text = "Matricola"
    Me.lbMatricola.Tooltip = ""
    Me.lbMatricola.UseMnemonic = False
    '
    'edMatricola
    '
    Me.edMatricola.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatricola.Location = New System.Drawing.Point(218, 27)
    Me.edMatricola.Name = "edMatricola"
    Me.edMatricola.NTSDbField = ""
    Me.edMatricola.NTSForzaVisZoom = False
    Me.edMatricola.NTSOldValue = ""
    Me.edMatricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatricola.Properties.MaxLength = 65536
    Me.edMatricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatricola.Size = New System.Drawing.Size(132, 20)
    Me.edMatricola.TabIndex = 9
    '
    'lbNota2
    '
    Me.lbNota2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbNota2.AutoSize = True
    Me.lbNota2.BackColor = System.Drawing.Color.Transparent
    Me.lbNota2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNota2.Location = New System.Drawing.Point(438, 58)
    Me.lbNota2.Name = "lbNota2"
    Me.lbNota2.NTSDbField = ""
    Me.lbNota2.Size = New System.Drawing.Size(198, 13)
    Me.lbNota2.TabIndex = 8
    Me.lbNota2.Text = "Documenti di produzione collegati"
    Me.lbNota2.Tooltip = ""
    Me.lbNota2.UseMnemonic = False
    '
    'lbDesart
    '
    Me.lbDesart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesart.BackColor = System.Drawing.Color.Transparent
    Me.lbDesart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesart.Location = New System.Drawing.Point(162, 4)
    Me.lbDesart.Name = "lbDesart"
    Me.lbDesart.NTSDbField = ""
    Me.lbDesart.Size = New System.Drawing.Size(474, 20)
    Me.lbDesart.TabIndex = 7
    Me.lbDesart.Tooltip = ""
    Me.lbDesart.UseMnemonic = False
    '
    'cbRecent
    '
    Me.cbRecent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbRecent.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbRecent.DataSource = Nothing
    Me.cbRecent.DisplayMember = ""
    Me.cbRecent.Location = New System.Drawing.Point(441, 27)
    Me.cbRecent.Name = "cbRecent"
    Me.cbRecent.NTSDbField = ""
    Me.cbRecent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbRecent.Properties.DropDownRows = 30
    Me.cbRecent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbRecent.SelectedValue = ""
    Me.cbRecent.Size = New System.Drawing.Size(195, 20)
    Me.cbRecent.TabIndex = 6
    Me.cbRecent.ValueMember = ""
    '
    'lbRecent
    '
    Me.lbRecent.AutoSize = True
    Me.lbRecent.BackColor = System.Drawing.Color.Transparent
    Me.lbRecent.Location = New System.Drawing.Point(358, 30)
    Me.lbRecent.Name = "lbRecent"
    Me.lbRecent.NTSDbField = ""
    Me.lbRecent.Size = New System.Drawing.Size(77, 13)
    Me.lbRecent.TabIndex = 5
    Me.lbRecent.Text = "Ultime ricerche"
    Me.lbRecent.Tooltip = ""
    Me.lbRecent.UseMnemonic = False
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNota.Location = New System.Drawing.Point(4, 58)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(131, 13)
    Me.lbNota.TabIndex = 4
    Me.lbNota.Text = "Movimentazione lotto"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(56, 4)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(100, 20)
    Me.edCodart.TabIndex = 3
    '
    'edLotto
    '
    Me.edLotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLotto.EditValue = ""
    Me.edLotto.Location = New System.Drawing.Point(56, 27)
    Me.edLotto.Name = "edLotto"
    Me.edLotto.NTSDbField = ""
    Me.edLotto.NTSForzaVisZoom = False
    Me.edLotto.NTSOldValue = ""
    Me.edLotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edLotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLotto.Properties.MaxLength = 65536
    Me.edLotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLotto.Size = New System.Drawing.Size(100, 20)
    Me.edLotto.TabIndex = 2
    '
    'lbLotto
    '
    Me.lbLotto.AutoSize = True
    Me.lbLotto.BackColor = System.Drawing.Color.Transparent
    Me.lbLotto.Location = New System.Drawing.Point(4, 30)
    Me.lbLotto.Name = "lbLotto"
    Me.lbLotto.NTSDbField = ""
    Me.lbLotto.Size = New System.Drawing.Size(32, 13)
    Me.lbLotto.TabIndex = 1
    Me.lbLotto.Text = "Lotto"
    Me.lbLotto.Tooltip = ""
    Me.lbLotto.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(4, 7)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 0
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'NtsSplitter1
    '
    Me.NtsSplitter1.Location = New System.Drawing.Point(468, 0)
    Me.NtsSplitter1.Name = "NtsSplitter1"
    Me.NtsSplitter1.Size = New System.Drawing.Size(3, 334)
    Me.NtsSplitter1.TabIndex = 9
    Me.NtsSplitter1.TabStop = False
    '
    'pnDx
    '
    Me.pnDx.AllowDrop = True
    Me.pnDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDx.Appearance.Options.UseBackColor = True
    Me.pnDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDx.Controls.Add(Me.grCp)
    Me.pnDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDx.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDx.Location = New System.Drawing.Point(471, 0)
    Me.pnDx.Name = "pnDx"
    Me.pnDx.Size = New System.Drawing.Size(177, 334)
    Me.pnDx.TabIndex = 10
    Me.pnDx.Text = "NtsPanel1"
    '
    'grCp
    '
    Me.grCp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grCp.EmbeddedNavigator.Name = ""
    Me.grCp.Location = New System.Drawing.Point(0, 0)
    Me.grCp.MainView = Me.grvCp
    Me.grCp.Name = "grCp"
    Me.grCp.Size = New System.Drawing.Size(177, 334)
    Me.grCp.TabIndex = 6
    Me.grCp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCp})
    '
    'grvCp
    '
    Me.grvCp.ActiveFilterEnabled = False
    '
    'omma_quant
    '
    Me.omma_quant.AppearanceCell.Options.UseBackColor = True
    Me.omma_quant.AppearanceCell.Options.UseTextOptions = True
    Me.omma_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omma_quant.Caption = "Qta matricola"
    Me.omma_quant.Enabled = True
    Me.omma_quant.FieldName = "omma_quant"
    Me.omma_quant.Name = "omma_quant"
    Me.omma_quant.NTSRepositoryComboBox = Nothing
    Me.omma_quant.NTSRepositoryItemCheck = Nothing
    Me.omma_quant.NTSRepositoryItemMemo = Nothing
    Me.omma_quant.NTSRepositoryItemText = Nothing
    Me.omma_quant.Visible = True
    Me.omma_quant.VisibleIndex = 8
    Me.grvCp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.omm_tipork, Me.omm_riga, Me.omm_codart, Me.omm_descr, Me.oxx_lottox, Me.omm_ump, Me.omm_quant, Me.omma_matric, Me.omma_quant})
    Me.grvCp.Enabled = True
    Me.grvCp.GridControl = Me.grCp
    Me.grvCp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCp.Name = "grvCp"
    Me.grvCp.NTSAllowDelete = True
    Me.grvCp.NTSAllowInsert = True
    Me.grvCp.NTSAllowUpdate = True
    Me.grvCp.NTSMenuContext = Nothing
    Me.grvCp.OptionsCustomization.AllowRowSizing = True
    Me.grvCp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCp.OptionsNavigation.UseTabKey = False
    Me.grvCp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCp.OptionsView.ColumnAutoWidth = False
    Me.grvCp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCp.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCp.OptionsView.ShowGroupPanel = False
    Me.grvCp.RowHeight = 14
    '
    'omma_matric
    '
    Me.omma_matric.AppearanceCell.Options.UseBackColor = True
    Me.omma_matric.AppearanceCell.Options.UseTextOptions = True
    Me.omma_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.omma_matric.Caption = "Matricola"
    Me.omma_matric.Enabled = True
    Me.omma_matric.FieldName = "omma_matric"
    Me.omma_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.omma_matric.Name = "omma_matric"
    Me.omma_matric.NTSRepositoryComboBox = Nothing
    Me.omma_matric.NTSRepositoryItemCheck = Nothing
    Me.omma_matric.NTSRepositoryItemMemo = Nothing
    Me.omma_matric.NTSRepositoryItemText = Nothing
    Me.omma_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.omma_matric.OptionsFilter.AllowFilter = False
    Me.omma_matric.Visible = True
    Me.omma_matric.VisibleIndex = 7
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.pnDx)
    Me.pnMain.Controls.Add(Me.NtsSplitter1)
    Me.pnMain.Controls.Add(Me.pnSx)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 108)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.Size = New System.Drawing.Size(648, 334)
    Me.pnMain.TabIndex = 10
    Me.pnMain.Text = "NtsPanel1"
    '
    'FRMMGRILO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGRILO"
    Me.NTSLastControlFocussed = Me.grRilo
    Me.Text = "RINTRACCIABILITA' LOTTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grRilo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRilo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSx.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edMatricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbRecent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDx.ResumeLayout(False)
    CType(Me.grCp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGRILO", "BEMGRILO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 129489115751943359, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleRilo = CType(oTmp, CLEMGRILO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGRILO", strRemoteServer, strRemotePort)
    AddHandler oCleRilo.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleRilo.Init(oApp, oScript, oMenu.oCleComm, "MOVLOT", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbCerca2.GlyphPath = (oApp.ChildImageDir & "\visualizza.gif")
        tlbFldo.GlyphPath = (oApp.ChildImageDir & "\naviga.gif")
        tlbApridoc.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129489134230185546, "Codice articolo"), tabartico, True)
      If oCleRilo.bLottoNew Then
        edLotto.NTSSetParam(oMenu, oApp.Tr(Me, 129518300385588763, "Codice lotto"), 50, True)
      Else
        edLotto.NTSSetParam(oMenu, oApp.Tr(Me, 129489134543847656, "Codice lotto"), 9, True)
      End If
      edLotto.NTSForzaVisZoom = True

      edMatricola.NTSSetParam(oMenu, oApp.Tr(Me, 129490840870244141, "Codice matricola"), 0, True)

      dttRecent.Columns.Add("cod", GetType(String))
      dttRecent.Columns.Add("val", GetType(String))
      dttRecent.AcceptChanges()

      cbRecent.NTSSetParam(oApp.Tr(Me, 129489135050800781, "Ultime ricerche"))
      cbRecent.DataSource = dttRecent
      cbRecent.ValueMember = "cod"
      cbRecent.DisplayMember = "val"

      Dim dttTipoRk As New DataTable()
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttTipoRk.Rows.Add(New Object() {"B", "DDT emesso"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipoRk.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipoRk.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"M", "DDT ricevuto"})
      dttTipoRk.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarico a produzione"})
      dttTipoRk.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})

      grvRilo.NTSSetParam(oMenu, "Movimenti lotto")
      grvRilo.NTSAllowInsert = False
      grvRilo.NTSAllowDelete = False
      grvRilo.NTSAllowUpdate = False
      grvRilo.Enabled = False
      tm_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129489943952871094, "Tipo doc"), dttTipoRk, "val", "cod")
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129489943952880860, "Data doc"), True)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129489943952890625, "Num. doc"), "0", 9, 0, 999999999)
      mm_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129489988401318360, "Riga"), "0", 9)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489943952900391, "Serie doc"), CLN__STD.SerieMaxLen, True)
      tm_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129489943952910157, "conto C/F"), tabanagra)
      an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489943952919922, "Descr. conto"), 50, True)
      km_causale.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129489943952929688, "Causale"), tabcaum)
      tb_descaum.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489943952939454, "Descr. causale"), 0, True)
      mm_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489943952949219, "UMP"), 0, True)
      mm_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129489943952958985, "Qta"), oApp.FormatQta, 15)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489943952968750, "Riferimenti"), 50, True)

      grvCp.NTSSetParam(oMenu, "Movimenti collegati")
      grvCp.NTSAllowInsert = False
      grvCp.NTSAllowDelete = False
      grvCp.NTSAllowUpdate = False
      grvCp.Enabled = False
      omm_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129489971324765625, "Tipo doc"), dttTipoRk, "val", "cod")
      oxx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489971343164063, "Lotto"), 50, True)
      omm_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129489971368037110, "Cod. articolo"), tabartico, True)
      omm_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489971376923829, "Descr. articolo"), 50, True)
      omm_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129489971384277344, "Riga"), "0", 9)
      omm_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129489971399062500, "UMP"), 0, True)
      omm_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129489971405878907, "Qta"), oApp.FormatQta, 15)
      omma_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129490857496542969, "Matricola"), 50, True)
      omma_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129490860278271484, "Qta matricola"), oApp.FormatQta, 15)

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
  Public Overridable Sub FRMMGRILO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable

    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleRilo.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      cbRecent.Enabled = False

      tlbLottoMatr.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BNMGRILO", "RECENT", ".", "LottoMatr", "0", " ", "0"))
      tlbLottoMatr_ItemClick(tlbLottoMatr, Nothing)

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRMMGRILO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      oMenu.SaveSettingBus("BNMGRILO", "RECENT", ".", "LottoMatr", IIf(tlbLottoMatr.Checked, "-1", "0").ToString, " ", "NS.", "...", "...")
      dcRilo.Dispose()
      dsRilo.Dispose()
      dcCp.Dispose()
      dsCp.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      Me.ValidaLastControl()

      Cerca(edCodart.Text, 0, edMatricola.Text, edLotto.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbLottoMatr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLottoMatr.ItemClick
    Try
      If tlbLottoMatr.Checked Then
        GctlSetVisEnab(lbMatricola, True)
        GctlSetVisEnab(edMatricola, True)
        GctlSetVisEnab(omma_matric, True)
        GctlSetVisEnab(omma_quant, True)
      Else
        lbMatricola.Visible = False
        edMatricola.Visible = False
        edMatricola.Text = ""
        omma_matric.Visible = False
        omma_quant.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCerca2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCerca2.ItemClick
    Try
      If grvCp.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129489991244160157, "Posizionarsi prima su una riga della griglia di destra"))
        Return
      End If

      edCodart.Text = NTSCStr(grvCp.NTSGetCurrentDataRow!omm_codart)
      edMatricola.Text = NTSCStr(grvCp.NTSGetCurrentDataRow!omma_matric).Trim
      'ricerca per lotto o matricola, non entrambi.
      'visto che la matricola è più precisa, escludo il lotto
      If edMatricola.Text.Trim = "" Then
        edLotto.Text = NTSCStr(grvCp.NTSGetCurrentDataRow!oxx_lottox)
      Else
        edLotto.Text = ""
      End If

      Cerca(edCodart.Text, 0, edMatricola.Text, edLotto.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try
      If edLotto.ContainsFocus Then
        NTSZOOM.strIn = edLotto.Text
        oParam.strTipo = edCodart.Text
        'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
        'oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
        'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edLotto.Text Then edLotto.Text = NTSZOOM.strIn
      Else
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApridoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApridoc.ItemClick
    Dim strParam As String = ""
    Try
      If grvRilo.NTSGetCurrentDataRow Is Nothing Then Return

      With grvRilo.NTSGetCurrentDataRow
        strParam = "APRI;" & IIf(NTSCStr(!tm_tipork) = "U", "T", NTSCStr(!tm_tipork)).ToString & ";" & _
                   NTSCInt(!mm_anno).ToString.PadLeft(4, "0"c) & ";" & _
                   NTSCStr(!mm_serie) & ";" & _
                   NTSCInt(!mm_numdoc).ToString.PadLeft(9, "0"c) & ";"
      End With
      oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 129489984781376954, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


  Public Overridable Sub tlbFldo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFldo.ItemClick
    Dim strParam As String
    Try
      If grvRilo.NTSGetCurrentDataRow Is Nothing Then Return

      With grvRilo.NTSGetCurrentDataRow
        strParam = "APRI;" & IIf(NTSCStr(!tm_tipork) = "U", "T", NTSCStr(!tm_tipork)).ToString & ";" & _
            NTSCInt(!mm_anno).ToString.PadLeft(4, "0"c) & ";" & _
            NTSCStr(!mm_serie) & ";" & _
            NTSCInt(!mm_numdoc).ToString.PadLeft(9, "0"c) & ";" & _
            "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(!tm_datdoc).ToShortDateString, 10) & _
            ";000000000;0000;0000; ;000000000;0000;2"
      End With

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 129489985447363282, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)

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

  Public Overridable Function Cerca(ByVal strCodart As String, ByVal lLotto As Integer, ByVal strMatric As String) As Boolean
    Try
      Return Cerca(strCodart, lLotto, strMatric, "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Cerca(ByVal strCodart As String, ByVal lLotto As Integer, ByVal strMatric As String, ByVal strLottox As String) As Boolean
    Dim dEsist As Decimal = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, lLotto, strMatric, strLottox})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Me.ValidaLastControl()

      If strCodart.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129489929370976563, "Indicare prima il codice articolo"))
        Return False
      End If

      If strLottox <> "" And lLotto = 0 Then
        'dato il lotto alfanumerico, cerco il lotto numerico
        lLotto = oCleRilo.GetIdFromLottoX(strCodart.Trim, strLottox)
      End If

      If tlbLottoMatr.Checked = False Then
        If lLotto = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129490848522910156, "Indicare prima il codice lotto"))
          Return False
        End If
      Else
        If lLotto = 0 And strMatric.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129490848546181641, "Indicare prima il codice lotto o il codice matricola"))
          Return False
        End If
      End If

      If lLotto <> 0 And strMatric.Trim <> "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129490842301230469, "Indicare o il codice lotto o il codice matricola, non entrambi"))
        Return False
      End If

      Me.Cursor = Cursors.WaitCursor

      '-----------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleRilo.GetData(DittaCorrente, strCodart, lLotto, strMatric, tlbLottoMatr.Checked, _
                              dsRilo, "", 0, "", 0, 0, False) Then Return False
      dcRilo.DataSource = dsRilo.Tables("MOVMAG")
      dsRilo.AcceptChanges()
      grRilo.DataSource = dcRilo


      If dsRilo.Tables("MOVMAG").Rows.Count > 0 Then
        dEsist = oCleRilo.GetEsist(DittaCorrente, strCodart, NTSCInt(dsRilo.Tables("MOVMAG").Rows(0)!mm_fase), lLotto)
      End If

      If tlbLottoMatr.Checked Then
        lbNota.Text = oApp.Tr(Me, 129489999384599610, "Movimentazione articolo '|" & strCodart & _
                      "|' lotto '|" & strLottox & "|' matricola '|" & strMatric & "|' Esistenza |" & _
                      dEsist.ToString(oApp.FormatQta) & "|")
      Else
        lbNota.Text = oApp.Tr(Me, 129490846666210938, "Movimentazione articolo '|" & strCodart & _
                      "|' lotto '|" & strLottox & "|' Esistenza |" & _
                      dEsist.ToString(oApp.FormatQta) & "|")
      End If


      '-----------------
      'aggiorno i recent
      If dttRecent.Select("cod = '" & strCodart & " - " & strLottox & " - " & strMatric & "'").Length = 0 Then
        dttRecent.Rows.Add(dttRecent.NewRow())
        dttRecent.Rows(dttRecent.Rows.Count - 1)!cod = strCodart & " - " & strLottox & " - " & strMatric
        dttRecent.Rows(dttRecent.Rows.Count - 1)!val = strCodart & " - " & strLottox & " - " & strMatric
        dttRecent.AcceptChanges()
      End If
      GctlSetVisEnab(cbRecent, False)

      '-----------------
      'faccio aggiornare la seconda griglia
      grvRilo_NTSFocusedRowChanged(grvRilo, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function


  Public Overridable Sub grvRilo_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRilo.FocusedRowChanged
    Try
      'scollego la griglia dei Carichi/scarichi collegati
      grCp.DataSource = Nothing
      tlbCerca2.Enabled = False

      If grvRilo.NTSGetCurrentDataRow Is Nothing Then Return

      'devo visualizzare gli scarichi collegati
      If grvRilo.NTSGetCurrentDataRow!tm_tipork.ToString <> "T" And _
         grvRilo.NTSGetCurrentDataRow!tm_tipork.ToString <> "U" Then Return

      Me.Cursor = Cursors.WaitCursor

      '-----------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      With grvRilo.NTSGetCurrentDataRow
        If NTSCStr(!tm_tipork) = "T" Then
          If Not oCleRilo.GetData(DittaCorrente, "", 0, "", tlbLottoMatr.Checked, dsCp, NTSCStr(!mm_tipork), _
                                  NTSCInt(!mm_anno), NTSCStr(!mm_serie), NTSCInt(!mm_numdoc), _
                                  NTSCInt(!mm_riga), True) Then Return
        Else
          If Not oCleRilo.GetData(DittaCorrente, "", 0, "", tlbLottoMatr.Checked, dsCp, NTSCStr(!mm_prtipo), _
                                  NTSCInt(!mm_pranno), NTSCStr(!mm_prserie), NTSCInt(!mm_prnum), _
                                  NTSCInt(!mm_prriga), False) Then Return
        End If
      End With
      dcCp.DataSource = dsCp.Tables("MOVMAG")
      dsCp.AcceptChanges()
      grCp.DataSource = dcCp

      'abilito il comando per poter effettuare la ricerca a partire dall'articolo/lotto della griglia di DX
      If dsCp.Tables("MOVMAG").Rows.Count > 0 Then GctlSetVisEnab(tlbCerca2, False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cbRecent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRecent.SelectedIndexChanged
    Try
      If cbRecent.Text.Trim <> "" Then
        edCodart.Text = cbRecent.Text.Split("-"c)(0).Trim
        edLotto.Text = cbRecent.Text.Split("-"c)(1).Trim
        edMatricola.Text = cbRecent.Text.Split("-"c)(2).Trim
        Cerca(edCodart.Text, 0, edMatricola.Text, edLotto.Text)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCodart_ValidatedAndChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If edCodart.Text.Trim = "" Then
        lbDesart.Text = ""
      Else
        If Not oMenu.ValCodiceDb(edCodart.Text, DittaCorrente, "ARTICO", "S", strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129506204035361328, "Articolo inesistente"))
          edCodart.Text = ""
          lbDesart.Text = ""
        Else
          lbDesart.Text = strTmp
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edLotto_ValidatedAndChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edLotto.ValidatedAndChanged
    Try
      If oCleRilo Is Nothing Then Return
      If oCleRilo.bLottoNew = False Then
        edLotto.Text = NTSCInt(edLotto.Text).ToString("000000000")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
