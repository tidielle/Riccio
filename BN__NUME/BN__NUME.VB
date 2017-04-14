Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__NUME

  Public oCleNume As CLE__NUME
  Public oCallParams As CLE__CLDP
  Public dsNume As DataSet
  Public dcNume As BindingSource = New BindingSource()

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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
  Public strTipoSelected As String = ""

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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbInizAnno As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmTipo As NTSInformatica.NTSGroupBox
  Public WithEvents fmNumerazioni As NTSInformatica.NTSGroupBox
  Public WithEvents liTipo As NTSInformatica.NTSListBox
  Public WithEvents grDati As NTSInformatica.NTSGrid
  Public WithEvents grvDati As NTSInformatica.NTSGridView
  Public WithEvents tb_numserie As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numcodl As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numprog As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numdata As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numprog2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numdata2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numval As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn11 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numcodl As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn19 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn110 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_num As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numserie As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn112 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numserie1 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn113 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numtipo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codfam As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_mastro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numserie2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codroot As NTSInformatica.NTSGridColumn
  Public WithEvents tb_alfpro As NTSInformatica.NTSGridColumn
  Public WithEvents tb_note As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numserie3 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numflag As NTSInformatica.NTSGridColumn
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__NUME))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbInizAnno = New NTSInformatica.NTSBarMenuItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grDati = New NTSInformatica.NTSGrid
    Me.grvDati = New NTSInformatica.NTSGridView
    Me.xx_numserie3 = New NTSInformatica.NTSGridColumn
    Me.tb_numtipo = New NTSInformatica.NTSGridColumn
    Me.tb_numserie = New NTSInformatica.NTSGridColumn
    Me.xx_numserie = New NTSInformatica.NTSGridColumn
    Me.xx_numserie1 = New NTSInformatica.NTSGridColumn
    Me.xx_codfam = New NTSInformatica.NTSGridColumn
    Me.xx_codart = New NTSInformatica.NTSGridColumn
    Me.xx_tipo = New NTSInformatica.NTSGridColumn
    Me.xx_num = New NTSInformatica.NTSGridColumn
    Me.xx_mastro = New NTSInformatica.NTSGridColumn
    Me.tb_numcodl = New NTSInformatica.NTSGridColumn
    Me.xx_numcodl = New NTSInformatica.NTSGridColumn
    Me.tb_numprog = New NTSInformatica.NTSGridColumn
    Me.tb_numdata = New NTSInformatica.NTSGridColumn
    Me.tb_numprog2 = New NTSInformatica.NTSGridColumn
    Me.tb_numdata2 = New NTSInformatica.NTSGridColumn
    Me.tb_numval = New NTSInformatica.NTSGridColumn
    Me.tb_numflag = New NTSInformatica.NTSGridColumn
    Me.xx_numserie2 = New NTSInformatica.NTSGridColumn
    Me.xx_codroot = New NTSInformatica.NTSGridColumn
    Me.tb_alfpro = New NTSInformatica.NTSGridColumn
    Me.tb_note = New NTSInformatica.NTSGridColumn
    Me.liTipo = New NTSInformatica.NTSListBox
    Me.fmTipo = New NTSInformatica.NTSGroupBox
    Me.fmNumerazioni = New NTSInformatica.NTSGroupBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDati, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDati, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipo.SuspendLayout()
    CType(Me.fmNumerazioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNumerazioni.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbInizAnno, Me.tlbCambioDitta})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.Id = 13
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInizAnno), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbInizAnno
    '
    Me.tlbInizAnno.Caption = "Inizializza nuovo anno"
    Me.tlbInizAnno.Id = 14
    Me.tlbInizAnno.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbInizAnno.Name = "tlbInizAnno"
    Me.tlbInizAnno.NTSIsCheckBox = False
    Me.tlbInizAnno.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambio ditta"
    Me.tlbCambioDitta.Id = 15
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.NTSIsCheckBox = False
    Me.tlbCambioDitta.Visible = True
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
    'grDati
    '
    Me.grDati.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDati.EmbeddedNavigator.Name = ""
    Me.grDati.Location = New System.Drawing.Point(2, 20)
    Me.grDati.MainView = Me.grvDati
    Me.grDati.Name = "grDati"
    Me.grDati.Size = New System.Drawing.Size(532, 464)
    Me.grDati.TabIndex = 4
    Me.grDati.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDati})
    '
    'grvDati
    '
    Me.grvDati.ActiveFilterEnabled = False
    '
    'xx_numserie3
    '
    Me.xx_numserie3.AppearanceCell.Options.UseBackColor = True
    Me.xx_numserie3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numserie3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numserie3.Caption = "Tipo (C/F)"
    Me.xx_numserie3.Enabled = True
    Me.xx_numserie3.FieldName = "xx_numserie3"
    Me.xx_numserie3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numserie3.Name = "xx_numserie3"
    Me.xx_numserie3.NTSRepositoryComboBox = Nothing
    Me.xx_numserie3.NTSRepositoryItemCheck = Nothing
    Me.xx_numserie3.NTSRepositoryItemMemo = Nothing
    Me.xx_numserie3.NTSRepositoryItemText = Nothing
    Me.xx_numserie3.Visible = True
    Me.xx_numserie3.VisibleIndex = 21
    Me.grvDati.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_numtipo, Me.tb_numserie, Me.xx_numserie, Me.xx_numserie1, Me.xx_codfam, Me.xx_codart, Me.xx_tipo, Me.xx_num, Me.xx_mastro, Me.tb_numcodl, Me.xx_numcodl, Me.tb_numprog, Me.tb_numdata, Me.tb_numprog2, Me.tb_numdata2, Me.tb_numval, Me.tb_numflag, Me.xx_numserie2, Me.xx_codroot, Me.tb_alfpro, Me.tb_note, Me.xx_numserie3})
    Me.grvDati.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDati.Enabled = True
    Me.grvDati.GridControl = Me.grDati
    Me.grvDati.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDati.MinRowHeight = 14
    Me.grvDati.Name = "grvDati"
    Me.grvDati.NTSAllowDelete = True
    Me.grvDati.NTSAllowInsert = True
    Me.grvDati.NTSAllowUpdate = True
    Me.grvDati.NTSMenuContext = Nothing
    Me.grvDati.OptionsCustomization.AllowRowSizing = True
    Me.grvDati.OptionsFilter.AllowFilterEditor = False
    Me.grvDati.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDati.OptionsNavigation.UseTabKey = False
    Me.grvDati.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDati.OptionsView.ColumnAutoWidth = False
    Me.grvDati.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDati.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDati.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDati.OptionsView.ShowGroupPanel = False
    Me.grvDati.RowHeight = 16
    '
    'tb_numtipo
    '
    Me.tb_numtipo.AppearanceCell.Options.UseBackColor = True
    Me.tb_numtipo.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numtipo.Caption = "Tipo"
    Me.tb_numtipo.Enabled = True
    Me.tb_numtipo.FieldName = "tb_numtipo"
    Me.tb_numtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numtipo.Name = "tb_numtipo"
    Me.tb_numtipo.NTSRepositoryComboBox = Nothing
    Me.tb_numtipo.NTSRepositoryItemCheck = Nothing
    Me.tb_numtipo.NTSRepositoryItemMemo = Nothing
    Me.tb_numtipo.NTSRepositoryItemText = Nothing
    Me.tb_numtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numtipo.OptionsFilter.AllowFilter = False
    Me.tb_numtipo.Visible = True
    Me.tb_numtipo.VisibleIndex = 13
    Me.tb_numtipo.Width = 127
    '
    'tb_numserie
    '
    Me.tb_numserie.AppearanceCell.Options.UseBackColor = True
    Me.tb_numserie.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numserie.Caption = "Serie"
    Me.tb_numserie.Enabled = True
    Me.tb_numserie.FieldName = "tb_numserie"
    Me.tb_numserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numserie.Name = "tb_numserie"
    Me.tb_numserie.NTSRepositoryComboBox = Nothing
    Me.tb_numserie.NTSRepositoryItemCheck = Nothing
    Me.tb_numserie.NTSRepositoryItemMemo = Nothing
    Me.tb_numserie.NTSRepositoryItemText = Nothing
    Me.tb_numserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numserie.OptionsFilter.AllowFilter = False
    Me.tb_numserie.Visible = True
    Me.tb_numserie.VisibleIndex = 0
    Me.tb_numserie.Width = 42
    '
    'xx_numserie
    '
    Me.xx_numserie.AppearanceCell.Options.UseBackColor = True
    Me.xx_numserie.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numserie.Caption = "Tipo Pagamento"
    Me.xx_numserie.Enabled = True
    Me.xx_numserie.FieldName = "xx_numserie"
    Me.xx_numserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numserie.Name = "xx_numserie"
    Me.xx_numserie.NTSRepositoryComboBox = Nothing
    Me.xx_numserie.NTSRepositoryItemCheck = Nothing
    Me.xx_numserie.NTSRepositoryItemMemo = Nothing
    Me.xx_numserie.NTSRepositoryItemText = Nothing
    Me.xx_numserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numserie.OptionsFilter.AllowFilter = False
    Me.xx_numserie.Visible = True
    Me.xx_numserie.VisibleIndex = 11
    Me.xx_numserie.Width = 131
    '
    'xx_numserie1
    '
    Me.xx_numserie1.AppearanceCell.Options.UseBackColor = True
    Me.xx_numserie1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numserie1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numserie1.Caption = "Tipo"
    Me.xx_numserie1.Enabled = True
    Me.xx_numserie1.FieldName = "xx_numserie1"
    Me.xx_numserie1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numserie1.Name = "xx_numserie1"
    Me.xx_numserie1.NTSRepositoryComboBox = Nothing
    Me.xx_numserie1.NTSRepositoryItemCheck = Nothing
    Me.xx_numserie1.NTSRepositoryItemMemo = Nothing
    Me.xx_numserie1.NTSRepositoryItemText = Nothing
    Me.xx_numserie1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numserie1.OptionsFilter.AllowFilter = False
    Me.xx_numserie1.Visible = True
    Me.xx_numserie1.VisibleIndex = 12
    Me.xx_numserie1.Width = 135
    '
    'xx_codfam
    '
    Me.xx_codfam.AppearanceCell.Options.UseBackColor = True
    Me.xx_codfam.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codfam.Caption = "Codice Famiglia"
    Me.xx_codfam.Enabled = True
    Me.xx_codfam.FieldName = "xx_codfam"
    Me.xx_codfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codfam.Name = "xx_codfam"
    Me.xx_codfam.NTSRepositoryComboBox = Nothing
    Me.xx_codfam.NTSRepositoryItemCheck = Nothing
    Me.xx_codfam.NTSRepositoryItemMemo = Nothing
    Me.xx_codfam.NTSRepositoryItemText = Nothing
    Me.xx_codfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codfam.OptionsFilter.AllowFilter = False
    Me.xx_codfam.Visible = True
    Me.xx_codfam.VisibleIndex = 14
    Me.xx_codfam.Width = 86
    '
    'xx_codart
    '
    Me.xx_codart.AppearanceCell.Options.UseBackColor = True
    Me.xx_codart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codart.Caption = "Codice Articolo"
    Me.xx_codart.Enabled = True
    Me.xx_codart.FieldName = "xx_codart"
    Me.xx_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codart.Name = "xx_codart"
    Me.xx_codart.NTSRepositoryComboBox = Nothing
    Me.xx_codart.NTSRepositoryItemCheck = Nothing
    Me.xx_codart.NTSRepositoryItemMemo = Nothing
    Me.xx_codart.NTSRepositoryItemText = Nothing
    Me.xx_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codart.OptionsFilter.AllowFilter = False
    Me.xx_codart.Visible = True
    Me.xx_codart.VisibleIndex = 15
    Me.xx_codart.Width = 88
    '
    'xx_tipo
    '
    Me.xx_tipo.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipo.Caption = "Tipo registro"
    Me.xx_tipo.Enabled = True
    Me.xx_tipo.FieldName = "xx_tipo"
    Me.xx_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipo.Name = "xx_tipo"
    Me.xx_tipo.NTSRepositoryComboBox = Nothing
    Me.xx_tipo.NTSRepositoryItemCheck = Nothing
    Me.xx_tipo.NTSRepositoryItemMemo = Nothing
    Me.xx_tipo.NTSRepositoryItemText = Nothing
    Me.xx_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipo.OptionsFilter.AllowFilter = False
    Me.xx_tipo.Visible = True
    Me.xx_tipo.VisibleIndex = 9
    Me.xx_tipo.Width = 123
    '
    'xx_num
    '
    Me.xx_num.AppearanceCell.Options.UseBackColor = True
    Me.xx_num.AppearanceCell.Options.UseTextOptions = True
    Me.xx_num.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_num.Caption = "Numero Registro"
    Me.xx_num.Enabled = True
    Me.xx_num.FieldName = "xx_num"
    Me.xx_num.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_num.Name = "xx_num"
    Me.xx_num.NTSRepositoryComboBox = Nothing
    Me.xx_num.NTSRepositoryItemCheck = Nothing
    Me.xx_num.NTSRepositoryItemMemo = Nothing
    Me.xx_num.NTSRepositoryItemText = Nothing
    Me.xx_num.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_num.OptionsFilter.AllowFilter = False
    Me.xx_num.Visible = True
    Me.xx_num.VisibleIndex = 10
    Me.xx_num.Width = 92
    '
    'xx_mastro
    '
    Me.xx_mastro.AppearanceCell.Options.UseBackColor = True
    Me.xx_mastro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_mastro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_mastro.Caption = "Mastro"
    Me.xx_mastro.Enabled = True
    Me.xx_mastro.FieldName = "xx_mastro"
    Me.xx_mastro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_mastro.Name = "xx_mastro"
    Me.xx_mastro.NTSRepositoryComboBox = Nothing
    Me.xx_mastro.NTSRepositoryItemCheck = Nothing
    Me.xx_mastro.NTSRepositoryItemMemo = Nothing
    Me.xx_mastro.NTSRepositoryItemText = Nothing
    Me.xx_mastro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_mastro.OptionsFilter.AllowFilter = False
    Me.xx_mastro.Visible = True
    Me.xx_mastro.VisibleIndex = 16
    '
    'tb_numcodl
    '
    Me.tb_numcodl.AppearanceCell.Options.UseBackColor = True
    Me.tb_numcodl.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numcodl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numcodl.Caption = "Anno"
    Me.tb_numcodl.Enabled = True
    Me.tb_numcodl.FieldName = "tb_numcodl"
    Me.tb_numcodl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numcodl.Name = "tb_numcodl"
    Me.tb_numcodl.NTSRepositoryComboBox = Nothing
    Me.tb_numcodl.NTSRepositoryItemCheck = Nothing
    Me.tb_numcodl.NTSRepositoryItemMemo = Nothing
    Me.tb_numcodl.NTSRepositoryItemText = Nothing
    Me.tb_numcodl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numcodl.OptionsFilter.AllowFilter = False
    Me.tb_numcodl.Visible = True
    Me.tb_numcodl.VisibleIndex = 1
    Me.tb_numcodl.Width = 60
    '
    'xx_numcodl
    '
    Me.xx_numcodl.AppearanceCell.Options.UseBackColor = True
    Me.xx_numcodl.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numcodl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numcodl.Caption = "Descrizione"
    Me.xx_numcodl.Enabled = True
    Me.xx_numcodl.FieldName = "xx_numcodl"
    Me.xx_numcodl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numcodl.Name = "xx_numcodl"
    Me.xx_numcodl.NTSRepositoryComboBox = Nothing
    Me.xx_numcodl.NTSRepositoryItemCheck = Nothing
    Me.xx_numcodl.NTSRepositoryItemMemo = Nothing
    Me.xx_numcodl.NTSRepositoryItemText = Nothing
    Me.xx_numcodl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numcodl.OptionsFilter.AllowFilter = False
    Me.xx_numcodl.Visible = True
    Me.xx_numcodl.VisibleIndex = 2
    Me.xx_numcodl.Width = 225
    '
    'tb_numprog
    '
    Me.tb_numprog.AppearanceCell.Options.UseBackColor = True
    Me.tb_numprog.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numprog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numprog.Caption = "N°Pro."
    Me.tb_numprog.Enabled = True
    Me.tb_numprog.FieldName = "tb_numprog"
    Me.tb_numprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numprog.Name = "tb_numprog"
    Me.tb_numprog.NTSRepositoryComboBox = Nothing
    Me.tb_numprog.NTSRepositoryItemCheck = Nothing
    Me.tb_numprog.NTSRepositoryItemMemo = Nothing
    Me.tb_numprog.NTSRepositoryItemText = Nothing
    Me.tb_numprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numprog.OptionsFilter.AllowFilter = False
    Me.tb_numprog.Visible = True
    Me.tb_numprog.VisibleIndex = 3
    Me.tb_numprog.Width = 56
    '
    'tb_numdata
    '
    Me.tb_numdata.AppearanceCell.Options.UseBackColor = True
    Me.tb_numdata.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numdata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numdata.Caption = "Data Ult.Em."
    Me.tb_numdata.Enabled = True
    Me.tb_numdata.FieldName = "tb_numdata"
    Me.tb_numdata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numdata.Name = "tb_numdata"
    Me.tb_numdata.NTSRepositoryComboBox = Nothing
    Me.tb_numdata.NTSRepositoryItemCheck = Nothing
    Me.tb_numdata.NTSRepositoryItemMemo = Nothing
    Me.tb_numdata.NTSRepositoryItemText = Nothing
    Me.tb_numdata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numdata.OptionsFilter.AllowFilter = False
    Me.tb_numdata.Visible = True
    Me.tb_numdata.VisibleIndex = 4
    Me.tb_numdata.Width = 71
    '
    'tb_numprog2
    '
    Me.tb_numprog2.AppearanceCell.Options.UseBackColor = True
    Me.tb_numprog2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numprog2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numprog2.Caption = "N°Ult.Tr."
    Me.tb_numprog2.Enabled = True
    Me.tb_numprog2.FieldName = "tb_numprog2"
    Me.tb_numprog2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numprog2.Name = "tb_numprog2"
    Me.tb_numprog2.NTSRepositoryComboBox = Nothing
    Me.tb_numprog2.NTSRepositoryItemCheck = Nothing
    Me.tb_numprog2.NTSRepositoryItemMemo = Nothing
    Me.tb_numprog2.NTSRepositoryItemText = Nothing
    Me.tb_numprog2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numprog2.OptionsFilter.AllowFilter = False
    Me.tb_numprog2.Visible = True
    Me.tb_numprog2.VisibleIndex = 5
    Me.tb_numprog2.Width = 53
    '
    'tb_numdata2
    '
    Me.tb_numdata2.AppearanceCell.Options.UseBackColor = True
    Me.tb_numdata2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numdata2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numdata2.Caption = "Data Ult.Tr."
    Me.tb_numdata2.Enabled = True
    Me.tb_numdata2.FieldName = "tb_numdata2"
    Me.tb_numdata2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numdata2.Name = "tb_numdata2"
    Me.tb_numdata2.NTSRepositoryComboBox = Nothing
    Me.tb_numdata2.NTSRepositoryItemCheck = Nothing
    Me.tb_numdata2.NTSRepositoryItemMemo = Nothing
    Me.tb_numdata2.NTSRepositoryItemText = Nothing
    Me.tb_numdata2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numdata2.OptionsFilter.AllowFilter = False
    Me.tb_numdata2.Visible = True
    Me.tb_numdata2.VisibleIndex = 6
    Me.tb_numdata2.Width = 72
    '
    'tb_numval
    '
    Me.tb_numval.AppearanceCell.Options.UseBackColor = True
    Me.tb_numval.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numval.Caption = "N°Reg.IVA"
    Me.tb_numval.Enabled = True
    Me.tb_numval.FieldName = "tb_numval"
    Me.tb_numval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numval.Name = "tb_numval"
    Me.tb_numval.NTSRepositoryComboBox = Nothing
    Me.tb_numval.NTSRepositoryItemCheck = Nothing
    Me.tb_numval.NTSRepositoryItemMemo = Nothing
    Me.tb_numval.NTSRepositoryItemText = Nothing
    Me.tb_numval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numval.OptionsFilter.AllowFilter = False
    Me.tb_numval.Visible = True
    Me.tb_numval.VisibleIndex = 7
    Me.tb_numval.Width = 65
    '
    'tb_numflag
    '
    Me.tb_numflag.AppearanceCell.Options.UseBackColor = True
    Me.tb_numflag.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numflag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numflag.Caption = "Tipo Registr."
    Me.tb_numflag.Enabled = True
    Me.tb_numflag.FieldName = "tb_numflag"
    Me.tb_numflag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numflag.Name = "tb_numflag"
    Me.tb_numflag.NTSRepositoryComboBox = Nothing
    Me.tb_numflag.NTSRepositoryItemCheck = Nothing
    Me.tb_numflag.NTSRepositoryItemMemo = Nothing
    Me.tb_numflag.NTSRepositoryItemText = Nothing
    Me.tb_numflag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numflag.OptionsFilter.AllowFilter = False
    Me.tb_numflag.Visible = True
    Me.tb_numflag.VisibleIndex = 8
    Me.tb_numflag.Width = 76
    '
    'xx_numserie2
    '
    Me.xx_numserie2.AppearanceCell.Options.UseBackColor = True
    Me.xx_numserie2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numserie2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numserie2.Caption = "Serie"
    Me.xx_numserie2.Enabled = True
    Me.xx_numserie2.FieldName = "xx_numserie2"
    Me.xx_numserie2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numserie2.Name = "xx_numserie2"
    Me.xx_numserie2.NTSRepositoryComboBox = Nothing
    Me.xx_numserie2.NTSRepositoryItemCheck = Nothing
    Me.xx_numserie2.NTSRepositoryItemMemo = Nothing
    Me.xx_numserie2.NTSRepositoryItemText = Nothing
    Me.xx_numserie2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numserie2.OptionsFilter.AllowFilter = False
    Me.xx_numserie2.Visible = True
    Me.xx_numserie2.VisibleIndex = 17
    Me.xx_numserie2.Width = 42
    '
    'xx_codroot
    '
    Me.xx_codroot.AppearanceCell.Options.UseBackColor = True
    Me.xx_codroot.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codroot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codroot.Caption = "Codice Root"
    Me.xx_codroot.Enabled = True
    Me.xx_codroot.FieldName = "xx_codroot"
    Me.xx_codroot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codroot.Name = "xx_codroot"
    Me.xx_codroot.NTSRepositoryComboBox = Nothing
    Me.xx_codroot.NTSRepositoryItemCheck = Nothing
    Me.xx_codroot.NTSRepositoryItemMemo = Nothing
    Me.xx_codroot.NTSRepositoryItemText = Nothing
    Me.xx_codroot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codroot.OptionsFilter.AllowFilter = False
    Me.xx_codroot.Visible = True
    Me.xx_codroot.VisibleIndex = 18
    '
    'tb_alfpro
    '
    Me.tb_alfpro.AppearanceCell.Options.UseBackColor = True
    Me.tb_alfpro.AppearanceCell.Options.UseTextOptions = True
    Me.tb_alfpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_alfpro.Caption = "Serie Pro."
    Me.tb_alfpro.Enabled = True
    Me.tb_alfpro.FieldName = "tb_alfpro"
    Me.tb_alfpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_alfpro.Name = "tb_alfpro"
    Me.tb_alfpro.NTSRepositoryComboBox = Nothing
    Me.tb_alfpro.NTSRepositoryItemCheck = Nothing
    Me.tb_alfpro.NTSRepositoryItemMemo = Nothing
    Me.tb_alfpro.NTSRepositoryItemText = Nothing
    Me.tb_alfpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_alfpro.OptionsFilter.AllowFilter = False
    Me.tb_alfpro.Visible = True
    Me.tb_alfpro.VisibleIndex = 19
    '
    'tb_note
    '
    Me.tb_note.AppearanceCell.Options.UseBackColor = True
    Me.tb_note.AppearanceCell.Options.UseTextOptions = True
    Me.tb_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_note.Caption = "Note"
    Me.tb_note.Enabled = True
    Me.tb_note.FieldName = "tb_note"
    Me.tb_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_note.Name = "tb_note"
    Me.tb_note.NTSRepositoryComboBox = Nothing
    Me.tb_note.NTSRepositoryItemCheck = Nothing
    Me.tb_note.NTSRepositoryItemMemo = Nothing
    Me.tb_note.NTSRepositoryItemText = Nothing
    Me.tb_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_note.OptionsFilter.AllowFilter = False
    Me.tb_note.Visible = True
    Me.tb_note.VisibleIndex = 20
    '
    'liTipo
    '
    Me.liTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTipo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.liTipo.ItemHeight = 14
    Me.liTipo.Location = New System.Drawing.Point(2, 20)
    Me.liTipo.Name = "liTipo"
    Me.liTipo.NTSDbField = ""
    Me.liTipo.Size = New System.Drawing.Size(252, 464)
    Me.liTipo.TabIndex = 6
    '
    'fmTipo
    '
    Me.fmTipo.AllowDrop = True
    Me.fmTipo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipo.Appearance.Options.UseBackColor = True
    Me.fmTipo.Controls.Add(Me.liTipo)
    Me.fmTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipo.Dock = System.Windows.Forms.DockStyle.Left
    Me.fmTipo.Location = New System.Drawing.Point(0, 30)
    Me.fmTipo.Name = "fmTipo"
    Me.fmTipo.Size = New System.Drawing.Size(256, 486)
    Me.fmTipo.TabIndex = 6
    Me.fmTipo.Text = "Tipo numerazione"
    '
    'fmNumerazioni
    '
    Me.fmNumerazioni.AllowDrop = True
    Me.fmNumerazioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNumerazioni.Appearance.Options.UseBackColor = True
    Me.fmNumerazioni.Controls.Add(Me.grDati)
    Me.fmNumerazioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNumerazioni.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmNumerazioni.Location = New System.Drawing.Point(256, 30)
    Me.fmNumerazioni.Name = "fmNumerazioni"
    Me.fmNumerazioni.Size = New System.Drawing.Size(536, 486)
    Me.fmNumerazioni.TabIndex = 7
    Me.fmNumerazioni.Text = "Numeratori"
    '
    'FRM__NUME
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(792, 516)
    Me.Controls.Add(Me.fmNumerazioni)
    Me.Controls.Add(Me.fmTipo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__NUME"
    Me.NTSLastControlFocussed = Me.grDati
    Me.Text = "NUMERAZIONI DITTA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDati, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDati, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liTipo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipo.ResumeLayout(False)
    CType(Me.fmNumerazioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNumerazioni.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__NUME", "BE__NUME", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128328672233059763, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleNume = CType(oTmp, CLE__NUME)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__NUME", strRemoteServer, strRemotePort)
    AddHandler oCleNume.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleNume.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim ddtTipoF As New DataTable()
      Dim ddtTipoReg As New DataTable()
      Dim ddtTipoFlag As New DataTable()
      Dim ddtTipoPag As New DataTable()
      Dim ddtTipo1 As New DataTable()
      Dim ddtTipo As New DataTable()
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      ddtTipoF.Columns.Add("cod", GetType(String))
      ddtTipoF.Columns.Add("val", GetType(String))
      ddtTipoF.Rows.Add(New Object() {"RO", oApp.Tr(Me, 128358937110704245, "RDA Acquisto")})
      ddtTipoF.Rows.Add(New Object() {"RH", oApp.Tr(Me, 128358937077768411, "RDA Lavorazione")})
      ddtTipoF.Rows.Add(New Object() {"SO", oApp.Tr(Me, 128358937164556675, "RDO Acquisto")})
      ddtTipoF.Rows.Add(New Object() {"SH", oApp.Tr(Me, 128780435902312031, "RDO Lavorazione")})

      ddtTipoReg.Columns.Add("cod", GetType(String))
      ddtTipoReg.Columns.Add("val", GetType(String))
      ddtTipoReg.Rows.Add(New Object() {"A", oApp.Tr(Me, 128780435941749671, "Acquisti")})
      ddtTipoReg.Rows.Add(New Object() {"V", oApp.Tr(Me, 128358937235267257, "Vendite")})
      ddtTipoReg.Rows.Add(New Object() {"C", oApp.Tr(Me, 128358937291773285, "Corrispettivi")})
      ddtTipoReg.Rows.Add(New Object() {"T", oApp.Tr(Me, 128358937308319249, "In sospensione")})
      ddtTipoReg.Rows.Add(New Object() {"S", oApp.Tr(Me, 128358937330640691, "In sospensione acq.")})

      ddtTipoFlag.Columns.Add("cod", GetType(String))
      ddtTipoFlag.Columns.Add("val", GetType(String))
      ddtTipoFlag.Rows.Add(New Object() {"N", oApp.Tr(Me, 128358937372629977, "(Nessuno)")})
      ddtTipoFlag.Rows.Add(New Object() {"A", oApp.Tr(Me, 128358937395107513, "Acquisti")})
      ddtTipoFlag.Rows.Add(New Object() {"V", oApp.Tr(Me, 128358937417741143, "Vendite")})
      ddtTipoFlag.Rows.Add(New Object() {"C", oApp.Tr(Me, 128358937438657739, "Corrispettivi")})
      ddtTipoFlag.Rows.Add(New Object() {"T", oApp.Tr(Me, 128780437555419431, "Vendite in sosp.")})
      ddtTipoFlag.Rows.Add(New Object() {"S", oApp.Tr(Me, 128358937471281385, "Acquisti in sosp.")})

      ddtTipoPag.Columns.Add("cod", GetType(String))
      ddtTipoPag.Columns.Add("val", GetType(String))
      ddtTipoPag.Rows.Add(New Object() {"0", oApp.Tr(Me, 128360730117105597, "(Nessuno)")})
      ddtTipoPag.Rows.Add(New Object() {"1", oApp.Tr(Me, 128358937497192989, "Tratta")})
      ddtTipoPag.Rows.Add(New Object() {"2", oApp.Tr(Me, 128358937515768175, "R.B. o RIBA")})
      ddtTipoPag.Rows.Add(New Object() {"3", oApp.Tr(Me, 128358937561815905, "Rim.Diretta")})
      ddtTipoPag.Rows.Add(New Object() {"4", oApp.Tr(Me, 128358937578517963, "Contanti")})
      ddtTipoPag.Rows.Add(New Object() {"5", oApp.Tr(Me, 128358937620975531, "Accr.Bancario")})

      ddtTipo1.Columns.Add("cod", GetType(String))
      ddtTipo1.Columns.Add("val", GetType(String))
      ddtTipo1.Rows.Add(New Object() {"0", oApp.Tr(Me, 128360730077573841, "(Nessuno)")})
      ddtTipo1.Rows.Add(New Object() {"A", oApp.Tr(Me, 128358937650789485, "Acquisti")})
      ddtTipo1.Rows.Add(New Object() {"V", oApp.Tr(Me, 128358937667335449, "Vendite")})

      ddtTipo.Columns.Add("cod", GetType(String))
      ddtTipo.Columns.Add("val", GetType(String))
      If CLN__STD.FRIENDLY = False Then
        ddtTipo.Rows.Add(New Object() {"AD", oApp.Tr(Me, 128890048326622972, "Aree di download")}) 'NTS
        ddtTipo.Rows.Add(New Object() {"C4", oApp.Tr(Me, 128365720797306354, "Assist. Attività da svolgere")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C5", oApp.Tr(Me, 128365720844805746, "Assist. Attività eseguite")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C3", oApp.Tr(Me, 128365720777306610, "Assist. Chiamate")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C6", oApp.Tr(Me, 128365720745119522, "Assist. Contratti di assistenza")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C2", oApp.Tr(Me, 128365720873242882, "Assist. Elenco ricambi")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"GP", oApp.Tr(Me, 128365720928710922, "Assist. Prepagati e monte-ore")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C1", oApp.Tr(Me, 128365720898398810, "Assist. Problemi/soluzioni")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"G", oApp.Tr(Me, 128358938305916003, "Avanz.produzione")})
        ddtTipo.Rows.Add(New Object() {"P2", oApp.Tr(Me, 128365692531730658, "Avv.parc./notula (par.)")}) 'BS--NUMX
        ddtTipo.Rows.Add(New Object() {"Z", oApp.Tr(Me, 128358938004810677, "Bolle di mov. interna")})
        ddtTipo.Rows.Add(New Object() {"T", oApp.Tr(Me, 128358938208669441, "Carico da produzione")})
        ddtTipo.Rows.Add(New Object() {"CC", oApp.Tr(Me, 128358937687003293, "Clienti")})
        ddtTipo.Rows.Add(New Object() {"V1", oApp.Tr(Me, 128358992437910357, "Codice pacco")}) 'varie
        ddtTipo.Rows.Add(New Object() {"CL", oApp.Tr(Me, 128358938491823957, "Collaudi")})
        ddtTipo.Rows.Add(New Object() {"PR", oApp.Tr(Me, 128365720957148058, "Comm. Sempl. Preventivi Commessa semplice")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"CQ", oApp.Tr(Me, 128358938458263747, "Configurat.per articolo")})
        ddtTipo.Rows.Add(New Object() {"CP", oApp.Tr(Me, 128358938417679307, "Configurat.per famiglia")})
        ddtTipo.Rows.Add(New Object() {"P3", oApp.Tr(Me, 128365692550636666, "Corrispettivi (parc.)")}) 'BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C", oApp.Tr(Me, 128358937990606123, "Corrispettivi emessi")})
        ddtTipo.Rows.Add(New Object() {"C7", oApp.Tr(Me, 128365720678245378, "CRM Attività")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C8", oApp.Tr(Me, 128365720659964362, "CRM Leads")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"C9", oApp.Tr(Me, 128365720695276410, "CRM Opportunità")}) 'varie BS--NUMX
        ddtTipo.Rows.Add(New Object() {"B", oApp.Tr(Me, 128358937931446497, "D.D.T. emessi")})
        ddtTipo.Rows.Add(New Object() {"M", oApp.Tr(Me, 128358937947836367, "D.D.T. ricevuti")})
        ddtTipo.Rows.Add(New Object() {"DI", oApp.Tr(Me, 128358938155753576, "Dichiarazioni d'intento")})
        ddtTipo.Rows.Add(New Object() {"DA", oApp.Tr(Me, 128358938155753575, "Distinte")})
        ddtTipo.Rows.Add(New Object() {"P1", oApp.Tr(Me, 128365692507824714, "Fatture (parcellazione)")}) 'BS--NUMX
        ddtTipo.Rows.Add(New Object() {"A", oApp.Tr(Me, 128358937870257649, "Fatture emesse")})
        ddtTipo.Rows.Add(New Object() {"S", oApp.Tr(Me, 128358938381933781, "Fatture ricev.fiscali")})
        ddtTipo.Rows.Add(New Object() {"L", oApp.Tr(Me, 128358937883213451, "Fatture ricevute")})
        ddtTipo.Rows.Add(New Object() {"AF", oApp.Tr(Me, 128890048341662524, "File delle aree di download")}) 'NTS
        ddtTipo.Rows.Add(New Object() {"FF", oApp.Tr(Me, 128358937701051753, "Fornitori")})
        ddtTipo.Rows.Add(New Object() {"X", oApp.Tr(Me, 128358938190562537, "Imp.di trasferimento")})
        ddtTipo.Rows.Add(New Object() {"R", oApp.Tr(Me, 128358937771450147, "Impegni clienti")})
        ddtTipo.Rows.Add(New Object() {"VV", oApp.Tr(Me, 128358938546925139, "Impegni Clienti aperti")})
        ddtTipo.Rows.Add(New Object() {"Y", oApp.Tr(Me, 128358938041336673, "Impegni produzione")})
        ddtTipo.Rows.Add(New Object() {"#", oApp.Tr(Me, 128358938598436159, "Impegni su commessa")})
        ddtTipo.Rows.Add(New Object() {"PK", oApp.Tr(Me, 128358992472875413, "Lista Picking")}) 'varie
        ddtTipo.Rows.Add(New Object() {"W", oApp.Tr(Me, 128358938179323769, "Note di prelievo")})
        ddtTipo.Rows.Add(New Object() {"RO", oApp.Tr(Me, 128358938585324263, "Numer. RDA/RDO")})
        ddtTipo.Rows.Add(New Object() {"AA", oApp.Tr(Me, 128358938472780489, "Numerazioni articolo")})
        ddtTipo.Rows.Add(New Object() {"MA", oApp.Tr(Me, 128358992394828413, "Numerazione attività")}) 'varie
        ddtTipo.Rows.Add(New Object() {"TP", oApp.Tr(Me, 128358992377814167, "Numerazione attività da svolgere")}) 'varie
        ddtTipo.Rows.Add(New Object() {"SM", oApp.Tr(Me, 128358938351963733, "Numerazioni RSM")})
        ddtTipo.Rows.Add(New Object() {"CP2", oApp.Tr(Me, 128358990046550277, "N°configuratore di prodotto")}) 'varie
        ddtTipo.Rows.Add(New Object() {"CQ2", oApp.Tr(Me, 128358990089788315, "N°contratti quadro")}) 'varie
        ddtTipo.Rows.Add(New Object() {"DT", oApp.Tr(Me, 128358990062940147, "N°documenti associati ai task")}) 'varie
        ddtTipo.Rows.Add(New Object() {"FL", oApp.Tr(Me, 128358990017204605, "N°flussi finanziari")}) 'varie
        ddtTipo.Rows.Add(New Object() {"BC", oApp.Tr(Me, 128358989866417801, "N°progressivi barcode")}) 'varie
        ddtTipo.Rows.Add(New Object() {"BG", oApp.Tr(Me, 129992634376704410, "N°progressivi barcode Gift Card")}) 'varie
        ddtTipo.Rows.Add(New Object() {"CE", oApp.Tr(Me, 128358989939001511, "N°progressivi cespiti")}) 'varie
        ddtTipo.Rows.Add(New Object() {"CO", oApp.Tr(Me, 128780436251171471, "N°progressivi commesse C.A.")}) 'varie
        ddtTipo.Rows.Add(New Object() {"OP", oApp.Tr(Me, 128358989745757139, "N°progressivi liste di produzione")}) 'varie
        ddtTipo.Rows.Add(New Object() {"LO", oApp.Tr(Me, 128780436286244471, "N°progressivi lotti")}) 'varie
        ddtTipo.Rows.Add(New Object() {"PA", oApp.Tr(Me, 128358989713913963, "N°progressivi movimenti di C.A.")}) 'varie
        ddtTipo.Rows.Add(New Object() {"OO", oApp.Tr(Me, 128358989807258175, "N°progressivi oggetti ole")}) 'varie
        ddtTipo.Rows.Add(New Object() {"OL", oApp.Tr(Me, 128358989760273881, "N°progressivi proposte d'ordine")}) 'varie
        ddtTipo.Rows.Add(New Object() {"RT", oApp.Tr(Me, 128358989914182565, "N°progressivi ritenute")}) 'varie
        ddtTipo.Rows.Add(New Object() {"IC", oApp.Tr(Me, 128358989956015757, "N°progressivi transazioni intracomunitarie")}) 'varie
        ddtTipo.Rows.Add(New Object() {"MT", oApp.Tr(Me, 128358990031565253, "N°matricole")}) 'varie
        ddtTipo.Rows.Add(New Object() {"MF", oApp.Tr(Me, 128358990000502547, "N°movimenti finanziari previsti")}) 'varie
        ddtTipo.Rows.Add(New Object() {"OG", oApp.Tr(Me, 128358990075895949, "N°progressivo organigramma")}) 'varie
        ddtTipo.Rows.Add(New Object() {"SP", oApp.Tr(Me, 128358990103368493, "N°schemi di commessa")}) 'varie
        ddtTipo.Rows.Add(New Object() {"EL", oApp.Tr(Me, 128358989985205335, "N°ultimo elenco intracomunitario presentato")}) 'varie
        ddtTipo.Rows.Add(New Object() {"!", oApp.Tr(Me, 128365692574542610, "Offerte (CRM)")}) 'BS--NUMX
        ddtTipo.Rows.Add(New Object() {"$", oApp.Tr(Me, 128358938533344961, "Ordini Forn.aperti")})
        ddtTipo.Rows.Add(New Object() {"O", oApp.Tr(Me, 128358937845282609, "Ordini fornitori")})
        ddtTipo.Rows.Add(New Object() {"H", oApp.Tr(Me, 128358938054604663, "Ordini produzione")})
        ddtTipo.Rows.Add(New Object() {"V", oApp.Tr(Me, 128358938403943035, "Packing List")})
        ddtTipo.Rows.Add(New Object() {"Q", oApp.Tr(Me, 128358937858394505, "Preventivi")})
        ddtTipo.Rows.Add(New Object() {"ID", oApp.Tr(Me, 129706806492909189, "Progressivo incassi differiti")})
        ddtTipo.Rows.Add(New Object() {"IM", oApp.Tr(Me, 128358992409032967, "Progressivo interessi di mora")}) 'varie
        ddtTipo.Rows.Add(New Object() {"F4", oApp.Tr(Me, 128358992422769239, "Progressivo F24")}) 'varie
        ddtTipo.Rows.Add(New Object() {"PD", oApp.Tr(Me, 128365692598761050, "Prot. docum. PM")}) 'BS--NUMX
        ddtTipo.Rows.Add(New Object() {"P", oApp.Tr(Me, 128358938094564727, "Protocolli acq/vend")})
        ddtTipo.Rows.Add(New Object() {"NC", oApp.Tr(Me, 128358938505872417, "Rapp.non conformità")})
        ddtTipo.Rows.Add(New Object() {"F", oApp.Tr(Me, 128358938367104851, "Ricevute fiscali")})
        ddtTipo.Rows.Add(New Object() {"EA", oApp.Tr(Me, 128358938110486315, "Scadenze")})
        ddtTipo.Rows.Add(New Object() {"U", oApp.Tr(Me, 128358938271419229, "Scarico a produzione")})
        ddtTipo.Rows.Add(New Object() {"SA", oApp.Tr(Me, 129820598807056163, "Sottoconti CA")})
        ddtTipo.Rows.Add(New Object() {"AP", oApp.Tr(Me, 128358992349873341, "Ultima anagrafica percipiente")}) 'varie
        ddtTipo.Rows.Add(New Object() {"UC", oApp.Tr(Me, 128873912490485670, "Unità di carico")}) 'varie
      Else
        ddtTipo.Rows.Add(New Object() {"Z", oApp.Tr(Me, 130148275537319179, "Bolle di mov. interna")})
        ddtTipo.Rows.Add(New Object() {"CC", oApp.Tr(Me, 130148275560073959, "Clienti")})
        ddtTipo.Rows.Add(New Object() {"C", oApp.Tr(Me, 130148275598073465, "Corrispettivi emessi")})
        ddtTipo.Rows.Add(New Object() {"B", oApp.Tr(Me, 130148275615554605, "D.D.T. emessi")})
        ddtTipo.Rows.Add(New Object() {"M", oApp.Tr(Me, 130148275632723233, "D.D.T. ricevuti")})
        ddtTipo.Rows.Add(New Object() {"DA", oApp.Tr(Me, 130148275660986037, "Distinte")})
        ddtTipo.Rows.Add(New Object() {"A", oApp.Tr(Me, 130148275695362357, "Fatture emesse")})
        ddtTipo.Rows.Add(New Object() {"S", oApp.Tr(Me, 130148275712736071, "Fatture ricev.fiscali")})
        ddtTipo.Rows.Add(New Object() {"L", oApp.Tr(Me, 130148275729728911, "Fatture ricevute")})
        ddtTipo.Rows.Add(New Object() {"FF", oApp.Tr(Me, 130148275756517049, "Fornitori")})
        ddtTipo.Rows.Add(New Object() {"R", oApp.Tr(Me, 130148275773187611, "Impegni clienti")})
        ddtTipo.Rows.Add(New Object() {"SM", oApp.Tr(Me, 130148275794252873, "Numerazioni RSM")})
        ddtTipo.Rows.Add(New Object() {"BC", oApp.Tr(Me, 130148275810845307, "N°progressivi barcode")}) 'varie
        ddtTipo.Rows.Add(New Object() {"O", oApp.Tr(Me, 130148275830826543, "Ordini fornitori")})
        ddtTipo.Rows.Add(New Object() {"Q", oApp.Tr(Me, 130148275848913175, "Preventivi")})
        ddtTipo.Rows.Add(New Object() {"ID", oApp.Tr(Me, 130245676923228033, "Progressivo incassi differiti")})
        ddtTipo.Rows.Add(New Object() {"F4", oApp.Tr(Me, 131030273057463801, "Progressivo F24")}) 'varie
        ddtTipo.Rows.Add(New Object() {"P", oApp.Tr(Me, 130148275865271225, "Protocolli acq/vend")})
        ddtTipo.Rows.Add(New Object() {"F", oApp.Tr(Me, 130148275881521849, "Ricevute fiscali")})
        ddtTipo.Rows.Add(New Object() {"EA", oApp.Tr(Me, 130148275903241433, "Scadenze")})
      End If
      ddtTipo.AcceptChanges()
      liTipo.DisplayMember = "val"
      liTipo.ValueMember = "cod"
      liTipo.DataSource = ddtTipo
      liTipo.SelectedIndex = -1
      ' abilito solo ora il gestore eventi
      AddHandler liTipo.SelectedIndexChanged, AddressOf Me.liTipo_SelectedIndexChanged

      grvDati.NTSSetParam(oMenu, oApp.Tr(Me, 128308575133593750, "Griglia Numerazioni Sottoconti"))
      tb_numtipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128308691861406250, "Tipo"), ddtTipoF, "val", "cod")
      tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706806928709955, "Serie"), CLN__STD.SerieMaxLen, False)
      xx_numserie.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128780436410792591, "Tipo Pagamento"), ddtTipoPag, "val", "cod")
      xx_numserie1.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128780436597380951, "Tipo"), ddtTipo1, "val", "cod")
      xx_codfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128308900587812500, "Codice Famiglia"), CLN__STD.tabcfam, False)
      xx_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128308900626875000, "Codice Articolo"), CLN__STD.tabartico, False)
      xx_tipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128308655075000000, "Tipo registro"), ddtTipoReg, "val", "cod")
      xx_num.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128308654461562500, "Numero Registro"), "0", 3)
      xx_mastro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128332792496250000, "Mastro"), CLN__STD.tabmast)
      tb_numcodl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128308575180468750, "Anno"), "0", 5)
      xx_numcodl.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780436866118071, "Descrizione"), 100)
      tb_numprog.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128308575241250000, "N°Pro."), "0", 9)
      tb_numprog2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128308575268437500, "N°Ult.Tr."), "0", 6)
      tb_numdata.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128308575336406250, "Data Ult.Em."), False)
      tb_numdata2.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128308575385937500, "Data Ult.Tr."), False)
      tb_numflag.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128308575408593750, "Tipo Registr."), ddtTipoFlag, "val", "cod")
      tb_numval.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128308575433750000, "N°Reg.IVA"), "0", 8)
      xx_numserie2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128360475488447727, "Serie"), CLN__STD.SerieMaxLen, False)
      xx_codroot.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780460635170745, "Codice Root"), CodartMaxLen, False)
      tb_alfpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129945193706693250, "Serie protocollo"), CLN__STD.SerieMaxLen, True)
      tb_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130511135036040605, "Note"), 255, True)
      fmTipo.Text = oApp.Tr(Me, 128308575471093750, "Tipo numerazione")
      fmNumerazioni.Text = oApp.Tr(Me, 128308575496875000, "Numeratori")
      liTipo.NTSSetParam(oApp.Tr(Me, 128358692847117231, "Tipo numerazione"))

      ' disabilito il grid
      Me.grvDati.NTSAllowDelete = False
      Me.grvDati.NTSAllowInsert = False
      Me.grvDati.NTSAllowUpdate = False

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

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "GETVARIAB:"
            oCleNume.strTipoSelected = strTipoSelected
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__NUME_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__NUME_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      LeggiDatiDitta()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If oCallParams.strOpen <> "" Then

        End If
      End If  'If Not oCallParams Is Nothing Then
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__NUME_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcNume.Dispose()
      dsNume.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRM__NUME_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub liTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles liTipo.SelectedIndexChanged
    Try
      If oCleNume.strCodpcon = "" Then Return 'si verifica in form_load prima di aver selezionato la ditta
      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub
#End Region

#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If oCleNume.IsInCodiceVarie(strTipoSelected) = True Then
        If dsNume.Tables("TABNUMA").Rows.Count < 1 Then 'un solo record
          grvDati.NTSNuovo()
        End If
      Else 'se non varie
        grvDati.NTSNuovo()
      End If
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
      If oCleNume.IsInCodice(strTipoSelected) = True Then
        If Not grvDati.NTSDeleteRigaCorrente(dcNume, True) Then Return
        oCleNume.Salva(True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDati.NTSRipristinaRigaCorrenteBefore(dcNume, True) Then Return
      oCleNume.Ripristina(dcNume.Position, dcNume.Filter)
      grvDati.NTSRipristinaRigaCorrenteAfter()
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
      If grDati.ContainsFocus And grvDati.FocusedColumn.Name.ToLower = "xx_mastro" Then
        Select Case NTSCStr(liTipo.SelectedValue)
          Case "SS"
            '----------------------------------------------
            'zoom specifico per mastri di contabilità generale
            oParam.strCodPdc = oCleNume.strCodpcon      'passo il piano dei conti
            SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
            NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
            NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)
          Case "SA"
            '----------------------------------------------
            'zoom specifico per mastri di contabilità analitica
            SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
            NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
            NTSZOOM.ZoomStrIn("ZOOMTABMACA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)
        End Select

      ElseIf grDati.ContainsFocus And grvDati.FocusedColumn.Name.ToLower = "xx_codfam" Then
        SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
        NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMTABCFAM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grDati.ContainsFocus And grvDati.FocusedColumn.Name.ToLower = "xx_codart" Then
        NTSZOOM.strIn = NTSCStr(grvDati.GetFocusedValue)
        oParam.nMagaz = 0
        oParam.nListino = 0
        oParam.lContoCF = 0
        NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvDati.GetFocusedValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)
      Else
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbInizAnno_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInizAnno.ItemClick
    Try
      oCleNume.FileElabora()
      liTipo.SelectedIndex = 0
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      LeggiDatiDitta()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

#Region "Eventi griglia"
  Public Overridable Sub grvDati_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDati.NTSBeforeRowUpdate
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
  Public Overridable Sub grvDati_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDati.NTSFocusedRowChanged
    ' usata per abilitare o disabilitare le colonne
    Dim dtr As DataRow
    Try
      dtr = grvDati.NTSGetCurrentDataRow()

      Select Case strTipoSelected
        Case "CC", "FF", "SS", "SA"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(xx_mastro, False)
            Else
              xx_mastro.Enabled = False
            End If
          Else
            GctlSetVisEnab(xx_mastro, False)
          End If
        Case "A", "L", "B", "M", "C", "Z", "W", "T", "U", "G", "F", "S", "V", "R", "O", "Q", "Y", "H", "X", "$", "VV", "#", "P1", "P2", "P3"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numserie, False)
              GctlSetVisEnab(tb_numcodl, False)
            Else
              tb_numserie.Enabled = False
              tb_numcodl.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numserie, False)
            GctlSetVisEnab(tb_numcodl, False)
          End If
        Case "P"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(xx_tipo, False)
              GctlSetVisEnab(xx_num, False)
              GctlSetVisEnab(xx_numserie2, False)
              GctlSetVisEnab(tb_numcodl, False)
            Else
              xx_tipo.Enabled = False
              xx_num.Enabled = False
              xx_numserie2.Enabled = False
              tb_numcodl.Enabled = False
            End If
          Else
            GctlSetVisEnab(xx_tipo, False)
            GctlSetVisEnab(xx_num, False)
            GctlSetVisEnab(xx_numserie2, False)
            GctlSetVisEnab(tb_numcodl, False)
          End If
        Case "EA", "DA"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numcodl, False)
              GctlSetVisEnab(xx_numserie, False)
            Else
              tb_numcodl.Enabled = False
              xx_numserie.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numcodl, False)
            GctlSetVisEnab(xx_numserie, False)
          End If
        Case "DI"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numcodl, False)
              GctlSetVisEnab(xx_numserie3, False)
            Else
              tb_numcodl.Enabled = False
              xx_numserie3.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numcodl, False)
            GctlSetVisEnab(xx_numserie3, False)
          End If
        Case "SM"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numcodl, False)
              GctlSetVisEnab(xx_numserie1, False)
            Else
              tb_numcodl.Enabled = False
              xx_numserie1.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numcodl, False)
            GctlSetVisEnab(xx_numserie1, False)
          End If
        Case "CP"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(xx_codfam, False)
            Else
              xx_codfam.Enabled = False
            End If
          Else
            GctlSetVisEnab(xx_codfam, False)
          End If
        Case "CQ"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(xx_codart, False)
            Else
              xx_codart.Enabled = False
            End If
          Else
            GctlSetVisEnab(xx_codart, False)
          End If
        Case "AA"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(xx_codroot, False)
            Else
              xx_codroot.Enabled = False
            End If
          Else
            GctlSetVisEnab(xx_codroot, False)
          End If
        Case "CL", "NC"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numcodl, False)
            Else
              tb_numcodl.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numcodl, False)
          End If
        Case "RO"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numtipo, False)
              GctlSetVisEnab(tb_numcodl, False)
              GctlSetVisEnab(tb_numserie, False)
            Else
              tb_numtipo.Enabled = False
              tb_numcodl.Enabled = False
              tb_numserie.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numtipo, False)
            GctlSetVisEnab(tb_numcodl, False)
            GctlSetVisEnab(tb_numserie, False)
          End If
        Case "!"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numserie, False)
              GctlSetVisEnab(tb_numcodl, False)
            Else
              tb_numserie.Enabled = False
              tb_numcodl.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numserie, False)
            GctlSetVisEnab(tb_numcodl, False)
          End If
        Case "PD"
          If Not dtr Is Nothing Then
            If dtr.RowState = DataRowState.Added Then
              GctlSetVisEnab(tb_numcodl, False)
            Else
              tb_numcodl.Enabled = False
            End If
          Else
            GctlSetVisEnab(tb_numcodl, False)
          End If
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try
      ' mancando l'evento changing devo fare questi controlli
      If liTipo.SelectedIndex = -1 Then Exit Function
      If (strTipoSelected = liTipo.SelectedValue.ToString()) Then Exit Function
      ' verifica se deve salvare
      If strTipoSelected <> "" Then
        If Not Salva() Then
          liTipo.SelectedValue = strTipoSelected
          grDati.Focus()
          Exit Function
        End If
      End If

      ' salvo il tipo corrente
      strTipoSelected = liTipo.SelectedValue.ToString()
      ' imposta il titolo
      fmNumerazioni.Text = oApp.Tr(Me, 128306096147343750, "Numeratori") + " " + CType(liTipo.SelectedItem, DataRowView).Item("val").ToString()
      ' carica i nuovi dati
      If Not oCleNume.Apri(DittaCorrente, strTipoSelected, dsNume) Then
        Me.Close()
        Return False
      End If
      ' metto a nothing il dc prima di riassegnare il datasource perché
      ' altrimenti mantiene riferimenti al vecchio datasource e può generare errori
      grDati.DataSource = Nothing
      dcNume.DataSource = Nothing
      dcNume.DataSource = dsNume.Tables("TABNUMA")
      dsNume.AcceptChanges()
      grDati.DataSource = dcNume


      ' disabilito e nascondo le colonne
      tb_numflag.Visible = False
      tb_numval.Visible = False
      tb_numdata2.Visible = False
      tb_numprog2.Visible = False
      tb_numdata.Visible = False
      tb_numprog.Visible = False
      xx_numcodl.Visible = False
      xx_numserie2.visible = False
      xx_mastro.Visible = False
      tb_numcodl.Visible = False
      xx_num.Visible = False
      xx_tipo.Visible = False
      xx_codart.Visible = False
      xx_codfam.Visible = False
      xx_numserie1.Visible = False
      xx_numserie.Visible = False
      tb_numserie.Visible = False
      tb_numtipo.Visible = False
      xx_codroot.Visible = False
      tb_alfpro.Visible = False
      xx_numserie3.Visible = False

      tb_numflag.Enabled = False
      tb_numval.Enabled = False
      tb_numdata2.Enabled = False
      tb_numprog2.Enabled = False
      tb_numdata.Enabled = False
      tb_numprog.Enabled = False
      xx_numcodl.Enabled = False
      xx_numserie2.enabled = False
      tb_numcodl.Enabled = False
      xx_mastro.Enabled = False
      xx_num.Enabled = False
      xx_tipo.Enabled = False
      xx_codart.Enabled = False
      xx_codfam.Enabled = False
      xx_numserie1.Enabled = False
      xx_numserie.Enabled = False
      tb_numserie.Enabled = False
      tb_numtipo.Enabled = False
      xx_codroot.Enabled = False
      tb_alfpro.Enabled = False
      xx_numserie3.Enabled = False

      tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706806949993996, "Serie"), CLN__STD.SerieMaxLen, False)

      ' abilito solo quello che serve
      'tb_numcodl disabilito lo zomm
      Select Case strTipoSelected
        Case "CC", "FF", "SS", "SA"
          xx_mastro.Visible = True
          xx_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_mastro.VisibleIndex = 0
          xx_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          xx_mastro.Caption = oApp.Tr(Me, 128780436703846991, "Mastro")
          xx_numcodl.Caption = oApp.Tr(Me, 128780436807039551, "Descrizione mastro")
          tb_numprog.Caption = oApp.Tr(Me, 128308627812343750, "N° Progressivo")
        Case "R", "O", "Q", "Y", "H", "X", "$", "VV", "#"
          tb_numserie.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          tb_numdata.Visible = True
          tb_numprog2.Visible = True
          tb_numdata2.Visible = True
          tb_numserie.VisibleIndex = 0
          tb_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          tb_numdata.VisibleIndex = 3
          tb_numprog2.VisibleIndex = 4
          tb_numdata2.VisibleIndex = 5
          GctlSetVisEnab(tb_numdata, False)
          GctlSetVisEnab(tb_numprog2, False)
          GctlSetVisEnab(tb_numdata2, False)
          tb_numserie.Caption = oApp.Tr(Me, 128780436988172111, "Serie")
          tb_numcodl.Caption = oApp.Tr(Me, 128308636544531250, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128308636573125000, "N°Progr.")
          tb_numdata.Caption = oApp.Tr(Me, 128308636636093750, "Data Ultima Emiss.")
          tb_numprog2.Caption = oApp.Tr(Me, 128308636748750000, "N°Ult.Trasf.")
          tb_numdata2.Caption = oApp.Tr(Me, 128308636780312500, "Data Ultimo Trasf.")
        Case "A", "L", "B", "M", "C", "Z", "W", "T", "U", "G", "F", "S", "V", "P1", "P2", "P3"
          tb_numserie.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          tb_numdata.Visible = True
          tb_numprog2.Visible = True
          tb_numdata2.Visible = True
          tb_numval.Visible = True
          tb_numflag.Visible = True
          If strTipoSelected = "L" Then tb_alfpro.Visible = True
          tb_numserie.VisibleIndex = 0
          tb_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          tb_numdata.VisibleIndex = 3
          tb_numprog2.VisibleIndex = 4
          tb_numdata2.VisibleIndex = 5
          tb_numval.VisibleIndex = 6
          tb_numflag.VisibleIndex = 7
          If strTipoSelected = "L" Then tb_alfpro.VisibleIndex = 8
          GctlSetVisEnab(tb_numdata, False)
          GctlSetVisEnab(tb_numprog2, False)
          GctlSetVisEnab(tb_numdata2, False)
          GctlSetVisEnab(tb_numval, False)
          GctlSetVisEnab(tb_numflag, False)
          If strTipoSelected = "L" Then GctlSetVisEnab(tb_alfpro, False)
          tb_numserie.Caption = oApp.Tr(Me, 128780436951384431, "Serie")
          tb_numcodl.Caption = oApp.Tr(Me, 128780437666250111, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128308641800937500, "N°Pro.")
          If strTipoSelected = "L" Then tb_alfpro.Caption = oApp.Tr(Me, 129945184405770738, "Serie Pro.")
          tb_numdata.Caption = oApp.Tr(Me, 128308638987500000, "Data Ult.Em.")
          tb_numprog2.Caption = oApp.Tr(Me, 128308639575781250, "N°Ult.Tr.")
          tb_numdata2.Caption = oApp.Tr(Me, 128308639384843750, "Data Ult.Tr.")
          tb_numval.Caption = oApp.Tr(Me, 128308640157656250, "N°Reg.IVA")
          tb_numflag.Caption = oApp.Tr(Me, 128308640864375000, "Tipo Registr.")
        Case "P"
          tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706806966282032, "Serie"), 20, False)
          xx_tipo.Visible = True
          xx_num.Visible = True
          xx_numserie2.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_tipo.VisibleIndex = 0
          xx_num.VisibleIndex = 1
          xx_numserie2.VisibleIndex = 2
          tb_numcodl.VisibleIndex = 3
          tb_numprog.VisibleIndex = 4
          xx_tipo.Caption = oApp.Tr(Me, 128308660171093750, "Tipo registro")
          xx_num.Caption = oApp.Tr(Me, 128308660196406250, "Numero Registro")
          xx_numserie2.Caption = oApp.Tr(Me, 128308660038281250, "Serie")
          tb_numcodl.Caption = oApp.Tr(Me, 128308660116718750, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128308660144375000, "Numero Progressivo")
        Case "EA", "DA"
          xx_numserie.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_numserie.VisibleIndex = 0
          tb_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          xx_numserie.Caption = oApp.Tr(Me, 128780436447112631, "Tipo Pagamento")
          tb_numcodl.Caption = oApp.Tr(Me, 128780437086376511, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128780437758998711, "Numero Progressivo")
        Case "DI"
          tb_numcodl.Visible = True
          xx_numserie3.Visible = True
          tb_numprog.Visible = True
          tb_numcodl.VisibleIndex = 0
          xx_numserie3.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          tb_numcodl.Caption = oApp.Tr(Me, 128780437086376512, "Anno")
          xx_numserie3.Caption = oApp.Tr(Me, 128780436447112632, "Tipo (C/F)")
          tb_numprog.Caption = oApp.Tr(Me, 128780437758998712, "Numero Progressivo")
        Case "SM"
          xx_numserie1.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_numserie1.VisibleIndex = 0
          tb_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          xx_numserie1.Caption = oApp.Tr(Me, 128780436551240471, "Tipo")
          tb_numcodl.Caption = oApp.Tr(Me, 128780437239606551, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128308661030156250, "Numero Progressivo")
        Case "CP"
          tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706806982742074, "Serie"), 20, False)
          xx_codfam.Visible = True
          xx_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_codfam.VisibleIndex = 0
          xx_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          xx_codfam.Caption = oApp.Tr(Me, 128308904009375000, "Codice Famiglia")
          xx_numcodl.Caption = oApp.Tr(Me, 128308683844375000, "Descrizione")
          tb_numprog.Caption = oApp.Tr(Me, 128308684644843750, "Numero Progressivo")
        Case "CQ"
          tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706807001762116, "Serie"), 20, False)
          xx_codart.Visible = True
          xx_numcodl.Visible = True
          tb_numprog.Visible = True
          xx_codart.VisibleIndex = 0
          xx_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          xx_codart.Caption = oApp.Tr(Me, 128308903833281250, "Codice Articolo")
          xx_numcodl.Caption = oApp.Tr(Me, 128308685235781250, "Descrizione")
          tb_numprog.Caption = oApp.Tr(Me, 128308685255937500, "Numero Progressivo")
        Case "AA"
          tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129706807017648148, "Serie"), 12, False)
          xx_codroot.Visible = True
          tb_numprog.Visible = True
          xx_codroot.VisibleIndex = 0
          tb_numprog.VisibleIndex = 1
          xx_codroot.Caption = oApp.Tr(Me, 128780461599467009, "Codice Root")
          tb_numprog.Caption = oApp.Tr(Me, 128308686491093750, "Numero Progressivo")
        Case "CL", "NC"
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          tb_numcodl.VisibleIndex = 0
          tb_numprog.VisibleIndex = 1
          tb_numcodl.Caption = oApp.Tr(Me, 128308687343750000, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128308687450468750, "Numero Progressivo")
        Case "RO"
          tb_numtipo.Visible = True
          tb_numcodl.Visible = True
          tb_numserie.Visible = True
          tb_numprog.Visible = True
          tb_numtipo.VisibleIndex = 0
          tb_numcodl.VisibleIndex = 1
          tb_numserie.VisibleIndex = 2
          tb_numprog.VisibleIndex = 3
          tb_numtipo.Caption = oApp.Tr(Me, 128308694589687500, "Tipo")
          tb_numcodl.Caption = oApp.Tr(Me, 128308694320312500, "Anno")
          tb_numserie.Caption = oApp.Tr(Me, 128308694345312500, "Serie")
          tb_numprog.Caption = oApp.Tr(Me, 128308694421250000, "Progressivo")
        Case "!"
          tb_numserie.Visible = True
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          tb_numserie.VisibleIndex = 1
          tb_numcodl.VisibleIndex = 2
          tb_numprog.VisibleIndex = 3
          tb_numserie.Caption = oApp.Tr(Me, 128365819582056194, "Serie")
          tb_numcodl.Caption = oApp.Tr(Me, 128365819607368370, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128365819629868082, "Progressivo")
        Case "PD"
          tb_numcodl.Visible = True
          tb_numprog.Visible = True
          tb_numcodl.VisibleIndex = 1
          tb_numprog.VisibleIndex = 2
          tb_numcodl.Caption = oApp.Tr(Me, 128365819835021706, "Anno")
          tb_numprog.Caption = oApp.Tr(Me, 128365819855177698, "Progressivo")
        Case Else
          If oCleNume.IsInCodiceVarie(strTipoSelected) = True Then
            tb_numprog.Visible = True
            tb_numprog.VisibleIndex = 0
            tb_numprog.Caption = oApp.Tr(Me, 128359007196910245, "Numero Progressivo")
          End If

      End Select

      GctlSetVisEnab(tb_numprog, False)

      If oCleNume.IsInCodice(strTipoSelected) = True Then
        grvDati.NTSAllowDelete = True
        grvDati.NTSAllowInsert = True
        grvDati.NTSAllowUpdate = True
      Else
        grvDati.NTSAllowDelete = False
        grvDati.NTSAllowInsert = False
        grvDati.NTSAllowUpdate = False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleNume.IsInCodice(strTipoSelected) = True Then
        Dim dRes As DialogResult
        dRes = grvDati.NTSSalvaRigaCorrente(dcNume, oCleNume.RecordIsChanged, False)
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Yes
            'salvo
            '-------------------------------------------------
            'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
            If GctlControllaOutNotEqual() = False Then Return False

            If Not oCleNume.Salva(False) Then
              Return False
            End If
          Case System.Windows.Forms.DialogResult.No
            'ripristino
            oCleNume.Ripristina(dcNume.Position, dcNume.Filter)
          Case System.Windows.Forms.DialogResult.Cancel
            'non posso fare nulla
            Return False
          Case System.Windows.Forms.DialogResult.Abort
            'la riga non ha subito modifiche
        End Select

      Else
        If (oCleNume.RecordIsChanged) Then
          If Not oCleNume.Salva(False) Then
            Return False
          End If
        End If
      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Sub LeggiDatiDitta()
    Dim bAnagen As Boolean = False
    Dim bDbMultiDitta As Boolean = False
    Dim nTipo As Integer
    Try
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      'se oCallParams.strOpen <> "" non visualizzera lo zoom ditte e prednerà la ditta passata da lchild padre
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BN__NUME", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Me.Close()

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Enabled = False
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      '-------------------------------------------------
      'leggo le informazioni relative alla ditta corrente
      If Not oCleNume.LeggiDatiDitta(DittaCorrente) Then Me.Close()
      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)

      ' ricarico la griglia
      nTipo = liTipo.SelectedIndex
      liTipo.SelectedIndex = -1
      strTipoSelected = ""
      liTipo.SelectedIndex = nTipo

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
