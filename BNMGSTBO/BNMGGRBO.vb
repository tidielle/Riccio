#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGGRBO
#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
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
#End Region

#Region "Variabili"
  Public oCleGrbo As CLEMGSTBO

  Public oCallParams As CLE__CLDP

  Public dsGrbo As DataSet
  Public dcGrbo As BindingSource = New BindingSource()
  Public dcGrboGrid As BindingSource = New BindingSource()
  Public dcGrboTestata As BindingSource = New BindingSource()

  Public strDettaglioTesta As String = "DettaglioTesta"
  Public strDettaglioRighe As String = "DettaglioRighe"

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettqta As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grGrbo As NTSInformatica.NTSGrid
  Public WithEvents grvGrbo As NTSInformatica.NTSGridView
  Public WithEvents mm_causale As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mm_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desciva As NTSInformatica.NTSGridColumn
  Public WithEvents mm_fase As NTSInformatica.NTSGridColumn
  Public WithEvents af_descr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents lbTm_conto As NTSInformatica.NTSLabel
  Public WithEvents edTm_conto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTm_scont1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumpar As NTSInformatica.NTSLabel
  Public WithEvents edTm_scont2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumdoc As NTSInformatica.NTSLabel
  Public WithEvents edTm_scopag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTm_riferim As NTSInformatica.NTSLabel
  Public WithEvents lbRiferimenti As NTSInformatica.NTSLabel
  Public WithEvents lbAn_descr1 As NTSInformatica.NTSLabel
  Public WithEvents lbSconti As NTSInformatica.NTSLabel
  Public WithEvents lbTb_despaga As NTSInformatica.NTSLabel
  Public WithEvents lbTotaleDoc As NTSInformatica.NTSLabel
  Public WithEvents edTd_totdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPagamento As NTSInformatica.NTSLabel
  Public WithEvents edTm_codpaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbFatturato As NTSInformatica.NTSLabel
  Public WithEvents lbTm_flfatt As NTSInformatica.NTSLabel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGRBO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettqta = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGrbo = New NTSInformatica.NTSGrid
    Me.grvGrbo = New NTSInformatica.NTSGridView
    Me.mm_causale = New NTSInformatica.NTSGridColumn
    Me.mm_codart = New NTSInformatica.NTSGridColumn
    Me.mm_descr = New NTSInformatica.NTSGridColumn
    Me.mm_quant = New NTSInformatica.NTSGridColumn
    Me.mm_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_scont1 = New NTSInformatica.NTSGridColumn
    Me.mm_scont2 = New NTSInformatica.NTSGridColumn
    Me.mm_scont3 = New NTSInformatica.NTSGridColumn
    Me.mm_scont4 = New NTSInformatica.NTSGridColumn
    Me.mm_scont5 = New NTSInformatica.NTSGridColumn
    Me.mm_scont6 = New NTSInformatica.NTSGridColumn
    Me.mm_codiva = New NTSInformatica.NTSGridColumn
    Me.tb_desciva = New NTSInformatica.NTSGridColumn
    Me.mm_fase = New NTSInformatica.NTSGridColumn
    Me.af_descr = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_subcommeca = New NTSInformatica.NTSGridColumn
    Me.lbTm_conto = New NTSInformatica.NTSLabel
    Me.edTm_conto = New NTSInformatica.NTSTextBoxNum
    Me.edTm_scont1 = New NTSInformatica.NTSTextBoxNum
    Me.lbNumpar = New NTSInformatica.NTSLabel
    Me.edTm_scont2 = New NTSInformatica.NTSTextBoxNum
    Me.lbNumdoc = New NTSInformatica.NTSLabel
    Me.edTm_scopag = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_riferim = New NTSInformatica.NTSLabel
    Me.lbRiferimenti = New NTSInformatica.NTSLabel
    Me.lbAn_descr1 = New NTSInformatica.NTSLabel
    Me.lbSconti = New NTSInformatica.NTSLabel
    Me.lbTb_despaga = New NTSInformatica.NTSLabel
    Me.lbTotaleDoc = New NTSInformatica.NTSLabel
    Me.edTd_totdoc = New NTSInformatica.NTSTextBoxNum
    Me.lbPagamento = New NTSInformatica.NTSLabel
    Me.edTm_codpaga = New NTSInformatica.NTSTextBoxNum
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbTotaleDocNoOmag = New NTSInformatica.NTSLabel
    Me.lbTotaleOmag = New NTSInformatica.NTSLabel
    Me.edTd_totdocnoomag = New NTSInformatica.NTSTextBoxNum
    Me.edTd_totomag = New NTSInformatica.NTSTextBoxNum
    Me.lbFatturato = New NTSInformatica.NTSLabel
    Me.lbTm_flfatt = New NTSInformatica.NTSLabel
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrbo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrbo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_conto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scont1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scont2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scopag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTd_totdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codpaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edTd_totdocnoomag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTd_totomag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbPrimo, Me.tlbPrecedente, Me.tlbUltimo, Me.tlbSuccessivo, Me.tlbEsci, Me.tlbDettqta, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettqta, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 0
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 1
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 2
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 3
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbDettqta
    '
    Me.tlbDettqta.Caption = "Taglie"
    Me.tlbDettqta.Glyph = CType(resources.GetObject("tlbDettqta.Glyph"), System.Drawing.Image)
    Me.tlbDettqta.Id = 13
    Me.tlbDettqta.Name = "tlbDettqta"
    Me.tlbDettqta.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grGrbo
    '
    Me.grGrbo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grGrbo.EmbeddedNavigator.Name = ""
    Me.grGrbo.Location = New System.Drawing.Point(0, 79)
    Me.grGrbo.MainView = Me.grvGrbo
    Me.grGrbo.Name = "grGrbo"
    Me.grGrbo.Size = New System.Drawing.Size(694, 284)
    Me.grGrbo.TabIndex = 5
    Me.grGrbo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrbo})
    '
    'grvGrbo
    '
    Me.grvGrbo.ActiveFilterEnabled = False
    Me.grvGrbo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mm_causale, Me.mm_codart, Me.mm_descr, Me.mm_quant, Me.mm_prezzo, Me.mm_scont1, Me.mm_scont2, Me.mm_scont3, Me.mm_scont4, Me.mm_scont5, Me.mm_scont6, Me.mm_codiva, Me.tb_desciva, Me.mm_fase, Me.af_descr, Me.xx_lottox, Me.mm_commeca, Me.mm_subcommeca})
    Me.grvGrbo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrbo.Enabled = True
    Me.grvGrbo.GridControl = Me.grGrbo
    Me.grvGrbo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrbo.MinRowHeight = 14
    Me.grvGrbo.Name = "grvGrbo"
    Me.grvGrbo.NTSAllowDelete = True
    Me.grvGrbo.NTSAllowInsert = True
    Me.grvGrbo.NTSAllowUpdate = True
    Me.grvGrbo.NTSMenuContext = Nothing
    Me.grvGrbo.OptionsCustomization.AllowRowSizing = True
    Me.grvGrbo.OptionsFilter.AllowFilterEditor = False
    Me.grvGrbo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrbo.OptionsNavigation.UseTabKey = False
    Me.grvGrbo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrbo.OptionsView.ColumnAutoWidth = False
    Me.grvGrbo.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrbo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrbo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrbo.OptionsView.ShowGroupPanel = False
    Me.grvGrbo.RowHeight = 16
    '
    'mm_causale
    '
    Me.mm_causale.AppearanceCell.Options.UseBackColor = True
    Me.mm_causale.AppearanceCell.Options.UseTextOptions = True
    Me.mm_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_causale.Caption = "Cs."
    Me.mm_causale.Enabled = True
    Me.mm_causale.FieldName = "mm_causale"
    Me.mm_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_causale.Name = "mm_causale"
    Me.mm_causale.NTSRepositoryComboBox = Nothing
    Me.mm_causale.NTSRepositoryItemCheck = Nothing
    Me.mm_causale.NTSRepositoryItemMemo = Nothing
    Me.mm_causale.NTSRepositoryItemText = Nothing
    Me.mm_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_causale.OptionsFilter.AllowFilter = False
    Me.mm_causale.Visible = True
    Me.mm_causale.VisibleIndex = 0
    Me.mm_causale.Width = 28
    '
    'mm_codart
    '
    Me.mm_codart.AppearanceCell.Options.UseBackColor = True
    Me.mm_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codart.Caption = "Codice Articolo"
    Me.mm_codart.Enabled = True
    Me.mm_codart.FieldName = "mm_codart"
    Me.mm_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codart.Name = "mm_codart"
    Me.mm_codart.NTSRepositoryComboBox = Nothing
    Me.mm_codart.NTSRepositoryItemCheck = Nothing
    Me.mm_codart.NTSRepositoryItemMemo = Nothing
    Me.mm_codart.NTSRepositoryItemText = Nothing
    Me.mm_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codart.OptionsFilter.AllowFilter = False
    Me.mm_codart.Visible = True
    Me.mm_codart.VisibleIndex = 1
    Me.mm_codart.Width = 83
    '
    'mm_descr
    '
    Me.mm_descr.AppearanceCell.Options.UseBackColor = True
    Me.mm_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mm_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_descr.Caption = "Descrizione"
    Me.mm_descr.Enabled = True
    Me.mm_descr.FieldName = "mm_descr"
    Me.mm_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_descr.Name = "mm_descr"
    Me.mm_descr.NTSRepositoryComboBox = Nothing
    Me.mm_descr.NTSRepositoryItemCheck = Nothing
    Me.mm_descr.NTSRepositoryItemMemo = Nothing
    Me.mm_descr.NTSRepositoryItemText = Nothing
    Me.mm_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_descr.OptionsFilter.AllowFilter = False
    Me.mm_descr.Visible = True
    Me.mm_descr.VisibleIndex = 2
    Me.mm_descr.Width = 66
    '
    'mm_quant
    '
    Me.mm_quant.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant.Caption = "Quantità"
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
    Me.mm_quant.VisibleIndex = 3
    Me.mm_quant.Width = 54
    '
    'mm_prezzo
    '
    Me.mm_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezzo.Caption = "Prezzo"
    Me.mm_prezzo.Enabled = True
    Me.mm_prezzo.FieldName = "mm_prezzo"
    Me.mm_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezzo.Name = "mm_prezzo"
    Me.mm_prezzo.NTSRepositoryComboBox = Nothing
    Me.mm_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mm_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mm_prezzo.NTSRepositoryItemText = Nothing
    Me.mm_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezzo.OptionsFilter.AllowFilter = False
    Me.mm_prezzo.Visible = True
    Me.mm_prezzo.VisibleIndex = 4
    Me.mm_prezzo.Width = 44
    '
    'mm_scont1
    '
    Me.mm_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont1.Caption = "Sc.1"
    Me.mm_scont1.Enabled = True
    Me.mm_scont1.FieldName = "mm_scont1"
    Me.mm_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont1.Name = "mm_scont1"
    Me.mm_scont1.NTSRepositoryComboBox = Nothing
    Me.mm_scont1.NTSRepositoryItemCheck = Nothing
    Me.mm_scont1.NTSRepositoryItemMemo = Nothing
    Me.mm_scont1.NTSRepositoryItemText = Nothing
    Me.mm_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont1.OptionsFilter.AllowFilter = False
    Me.mm_scont1.Visible = True
    Me.mm_scont1.VisibleIndex = 5
    Me.mm_scont1.Width = 33
    '
    'mm_scont2
    '
    Me.mm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont2.Caption = "Sc.2"
    Me.mm_scont2.Enabled = True
    Me.mm_scont2.FieldName = "mm_scont2"
    Me.mm_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont2.Name = "mm_scont2"
    Me.mm_scont2.NTSRepositoryComboBox = Nothing
    Me.mm_scont2.NTSRepositoryItemCheck = Nothing
    Me.mm_scont2.NTSRepositoryItemMemo = Nothing
    Me.mm_scont2.NTSRepositoryItemText = Nothing
    Me.mm_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont2.OptionsFilter.AllowFilter = False
    Me.mm_scont2.Visible = True
    Me.mm_scont2.VisibleIndex = 6
    Me.mm_scont2.Width = 33
    '
    'mm_scont3
    '
    Me.mm_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont3.Caption = "Sc.3"
    Me.mm_scont3.Enabled = True
    Me.mm_scont3.FieldName = "mm_scont3"
    Me.mm_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont3.Name = "mm_scont3"
    Me.mm_scont3.NTSRepositoryComboBox = Nothing
    Me.mm_scont3.NTSRepositoryItemCheck = Nothing
    Me.mm_scont3.NTSRepositoryItemMemo = Nothing
    Me.mm_scont3.NTSRepositoryItemText = Nothing
    Me.mm_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont3.OptionsFilter.AllowFilter = False
    Me.mm_scont3.Visible = True
    Me.mm_scont3.VisibleIndex = 7
    Me.mm_scont3.Width = 33
    '
    'mm_scont4
    '
    Me.mm_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont4.Caption = "Sc.4"
    Me.mm_scont4.Enabled = True
    Me.mm_scont4.FieldName = "mm_scont4"
    Me.mm_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont4.Name = "mm_scont4"
    Me.mm_scont4.NTSRepositoryComboBox = Nothing
    Me.mm_scont4.NTSRepositoryItemCheck = Nothing
    Me.mm_scont4.NTSRepositoryItemMemo = Nothing
    Me.mm_scont4.NTSRepositoryItemText = Nothing
    Me.mm_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont4.OptionsFilter.AllowFilter = False
    Me.mm_scont4.Width = 33
    '
    'mm_scont5
    '
    Me.mm_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont5.Caption = "Sc.5"
    Me.mm_scont5.Enabled = True
    Me.mm_scont5.FieldName = "mm_scont5"
    Me.mm_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont5.Name = "mm_scont5"
    Me.mm_scont5.NTSRepositoryComboBox = Nothing
    Me.mm_scont5.NTSRepositoryItemCheck = Nothing
    Me.mm_scont5.NTSRepositoryItemMemo = Nothing
    Me.mm_scont5.NTSRepositoryItemText = Nothing
    Me.mm_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont5.OptionsFilter.AllowFilter = False
    Me.mm_scont5.Width = 33
    '
    'mm_scont6
    '
    Me.mm_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont6.Caption = "Sc.6"
    Me.mm_scont6.Enabled = True
    Me.mm_scont6.FieldName = "mm_scont6"
    Me.mm_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont6.Name = "mm_scont6"
    Me.mm_scont6.NTSRepositoryComboBox = Nothing
    Me.mm_scont6.NTSRepositoryItemCheck = Nothing
    Me.mm_scont6.NTSRepositoryItemMemo = Nothing
    Me.mm_scont6.NTSRepositoryItemText = Nothing
    Me.mm_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont6.OptionsFilter.AllowFilter = False
    Me.mm_scont6.Width = 33
    '
    'mm_codiva
    '
    Me.mm_codiva.AppearanceCell.Options.UseBackColor = True
    Me.mm_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codiva.Caption = "Codice IVA"
    Me.mm_codiva.Enabled = True
    Me.mm_codiva.FieldName = "mm_codiva"
    Me.mm_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codiva.Name = "mm_codiva"
    Me.mm_codiva.NTSRepositoryComboBox = Nothing
    Me.mm_codiva.NTSRepositoryItemCheck = Nothing
    Me.mm_codiva.NTSRepositoryItemMemo = Nothing
    Me.mm_codiva.NTSRepositoryItemText = Nothing
    Me.mm_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codiva.OptionsFilter.AllowFilter = False
    Me.mm_codiva.Visible = True
    Me.mm_codiva.VisibleIndex = 8
    Me.mm_codiva.Width = 64
    '
    'tb_desciva
    '
    Me.tb_desciva.AppearanceCell.Options.UseBackColor = True
    Me.tb_desciva.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desciva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desciva.Caption = "Descrizione IVA"
    Me.tb_desciva.Enabled = True
    Me.tb_desciva.FieldName = "tb_desciva"
    Me.tb_desciva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desciva.Name = "tb_desciva"
    Me.tb_desciva.NTSRepositoryComboBox = Nothing
    Me.tb_desciva.NTSRepositoryItemCheck = Nothing
    Me.tb_desciva.NTSRepositoryItemMemo = Nothing
    Me.tb_desciva.NTSRepositoryItemText = Nothing
    Me.tb_desciva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desciva.OptionsFilter.AllowFilter = False
    Me.tb_desciva.Visible = True
    Me.tb_desciva.VisibleIndex = 9
    Me.tb_desciva.Width = 86
    '
    'mm_fase
    '
    Me.mm_fase.AppearanceCell.Options.UseBackColor = True
    Me.mm_fase.AppearanceCell.Options.UseTextOptions = True
    Me.mm_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_fase.Caption = "Fase"
    Me.mm_fase.Enabled = True
    Me.mm_fase.FieldName = "mm_fase"
    Me.mm_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_fase.Name = "mm_fase"
    Me.mm_fase.NTSRepositoryComboBox = Nothing
    Me.mm_fase.NTSRepositoryItemCheck = Nothing
    Me.mm_fase.NTSRepositoryItemMemo = Nothing
    Me.mm_fase.NTSRepositoryItemText = Nothing
    Me.mm_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_fase.OptionsFilter.AllowFilter = False
    Me.mm_fase.Visible = True
    Me.mm_fase.VisibleIndex = 10
    Me.mm_fase.Width = 35
    '
    'af_descr
    '
    Me.af_descr.AppearanceCell.Options.UseBackColor = True
    Me.af_descr.AppearanceCell.Options.UseTextOptions = True
    Me.af_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_descr.Caption = "Descrizione fase"
    Me.af_descr.Enabled = True
    Me.af_descr.FieldName = "af_descr"
    Me.af_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_descr.Name = "af_descr"
    Me.af_descr.NTSRepositoryComboBox = Nothing
    Me.af_descr.NTSRepositoryItemCheck = Nothing
    Me.af_descr.NTSRepositoryItemMemo = Nothing
    Me.af_descr.NTSRepositoryItemText = Nothing
    Me.af_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_descr.OptionsFilter.AllowFilter = False
    Me.af_descr.Visible = True
    Me.af_descr.VisibleIndex = 11
    Me.af_descr.Width = 90
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 12
    Me.xx_lottox.Width = 37
    '
    'mm_commeca
    '
    Me.mm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_commeca.Caption = "Commessa"
    Me.mm_commeca.Enabled = True
    Me.mm_commeca.FieldName = "mm_commeca"
    Me.mm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_commeca.Name = "mm_commeca"
    Me.mm_commeca.NTSRepositoryComboBox = Nothing
    Me.mm_commeca.NTSRepositoryItemCheck = Nothing
    Me.mm_commeca.NTSRepositoryItemMemo = Nothing
    Me.mm_commeca.NTSRepositoryItemText = Nothing
    Me.mm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_commeca.OptionsFilter.AllowFilter = False
    Me.mm_commeca.Visible = True
    Me.mm_commeca.VisibleIndex = 13
    Me.mm_commeca.Width = 63
    '
    'mm_subcommeca
    '
    Me.mm_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_subcommeca.Caption = "Subcommessa"
    Me.mm_subcommeca.Enabled = True
    Me.mm_subcommeca.FieldName = "mm_subcommeca"
    Me.mm_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_subcommeca.Name = "mm_subcommeca"
    Me.mm_subcommeca.NTSRepositoryComboBox = Nothing
    Me.mm_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.mm_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.mm_subcommeca.NTSRepositoryItemText = Nothing
    Me.mm_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_subcommeca.OptionsFilter.AllowFilter = False
    Me.mm_subcommeca.Visible = True
    Me.mm_subcommeca.VisibleIndex = 14
    Me.mm_subcommeca.Width = 79
    '
    'lbTm_conto
    '
    Me.lbTm_conto.AutoSize = True
    Me.lbTm_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_conto.Location = New System.Drawing.Point(3, 7)
    Me.lbTm_conto.Name = "lbTm_conto"
    Me.lbTm_conto.NTSDbField = ""
    Me.lbTm_conto.Size = New System.Drawing.Size(70, 13)
    Me.lbTm_conto.TabIndex = 61
    Me.lbTm_conto.Text = "Cliente/Forn."
    Me.lbTm_conto.Tooltip = ""
    Me.lbTm_conto.UseMnemonic = False
    '
    'edTm_conto
    '
    Me.edTm_conto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_conto.Enabled = False
    Me.edTm_conto.Location = New System.Drawing.Point(79, 4)
    Me.edTm_conto.Name = "edTm_conto"
    Me.edTm_conto.NTSDbField = ""
    Me.edTm_conto.NTSFormat = "0"
    Me.edTm_conto.NTSForzaVisZoom = False
    Me.edTm_conto.NTSOldValue = ""
    Me.edTm_conto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_conto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_conto.Properties.MaxLength = 65536
    Me.edTm_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_conto.Size = New System.Drawing.Size(64, 20)
    Me.edTm_conto.TabIndex = 57
    '
    'edTm_scont1
    '
    Me.edTm_scont1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_scont1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_scont1.Enabled = False
    Me.edTm_scont1.Location = New System.Drawing.Point(471, 29)
    Me.edTm_scont1.Name = "edTm_scont1"
    Me.edTm_scont1.NTSDbField = ""
    Me.edTm_scont1.NTSFormat = "0"
    Me.edTm_scont1.NTSForzaVisZoom = False
    Me.edTm_scont1.NTSOldValue = ""
    Me.edTm_scont1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scont1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scont1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scont1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scont1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scont1.Properties.MaxLength = 65536
    Me.edTm_scont1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scont1.Size = New System.Drawing.Size(64, 20)
    Me.edTm_scont1.TabIndex = 54
    '
    'lbNumpar
    '
    Me.lbNumpar.BackColor = System.Drawing.Color.Transparent
    Me.lbNumpar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbNumpar.Location = New System.Drawing.Point(79, 56)
    Me.lbNumpar.Name = "lbNumpar"
    Me.lbNumpar.NTSDbField = ""
    Me.lbNumpar.Size = New System.Drawing.Size(322, 20)
    Me.lbNumpar.TabIndex = 64
    Me.lbNumpar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbNumpar.Tooltip = ""
    Me.lbNumpar.UseMnemonic = False
    '
    'edTm_scont2
    '
    Me.edTm_scont2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_scont2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_scont2.Enabled = False
    Me.edTm_scont2.Location = New System.Drawing.Point(541, 29)
    Me.edTm_scont2.Name = "edTm_scont2"
    Me.edTm_scont2.NTSDbField = ""
    Me.edTm_scont2.NTSFormat = "0"
    Me.edTm_scont2.NTSForzaVisZoom = False
    Me.edTm_scont2.NTSOldValue = ""
    Me.edTm_scont2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scont2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scont2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scont2.Properties.MaxLength = 65536
    Me.edTm_scont2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scont2.Size = New System.Drawing.Size(64, 20)
    Me.edTm_scont2.TabIndex = 56
    '
    'lbNumdoc
    '
    Me.lbNumdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbNumdoc.Location = New System.Drawing.Point(79, 29)
    Me.lbNumdoc.Name = "lbNumdoc"
    Me.lbNumdoc.NTSDbField = ""
    Me.lbNumdoc.Size = New System.Drawing.Size(322, 20)
    Me.lbNumdoc.TabIndex = 63
    Me.lbNumdoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbNumdoc.Tooltip = ""
    Me.lbNumdoc.UseMnemonic = False
    '
    'edTm_scopag
    '
    Me.edTm_scopag.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_scopag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_scopag.Enabled = False
    Me.edTm_scopag.Location = New System.Drawing.Point(611, 29)
    Me.edTm_scopag.Name = "edTm_scopag"
    Me.edTm_scopag.NTSDbField = ""
    Me.edTm_scopag.NTSFormat = "0"
    Me.edTm_scopag.NTSForzaVisZoom = False
    Me.edTm_scopag.NTSOldValue = ""
    Me.edTm_scopag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scopag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scopag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scopag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scopag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scopag.Properties.MaxLength = 65536
    Me.edTm_scopag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scopag.Size = New System.Drawing.Size(64, 20)
    Me.edTm_scopag.TabIndex = 55
    '
    'lbTm_riferim
    '
    Me.lbTm_riferim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTm_riferim.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_riferim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTm_riferim.Location = New System.Drawing.Point(471, 4)
    Me.lbTm_riferim.Name = "lbTm_riferim"
    Me.lbTm_riferim.NTSDbField = ""
    Me.lbTm_riferim.Size = New System.Drawing.Size(204, 20)
    Me.lbTm_riferim.TabIndex = 65
    Me.lbTm_riferim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbTm_riferim.Tooltip = ""
    Me.lbTm_riferim.UseMnemonic = False
    '
    'lbRiferimenti
    '
    Me.lbRiferimenti.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbRiferimenti.AutoSize = True
    Me.lbRiferimenti.BackColor = System.Drawing.Color.Transparent
    Me.lbRiferimenti.Location = New System.Drawing.Point(407, 8)
    Me.lbRiferimenti.Name = "lbRiferimenti"
    Me.lbRiferimenti.NTSDbField = ""
    Me.lbRiferimenti.Size = New System.Drawing.Size(58, 13)
    Me.lbRiferimenti.TabIndex = 59
    Me.lbRiferimenti.Text = "Riferimenti"
    Me.lbRiferimenti.Tooltip = ""
    Me.lbRiferimenti.UseMnemonic = False
    '
    'lbAn_descr1
    '
    Me.lbAn_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_descr1.Location = New System.Drawing.Point(149, 4)
    Me.lbAn_descr1.Name = "lbAn_descr1"
    Me.lbAn_descr1.NTSDbField = ""
    Me.lbAn_descr1.Size = New System.Drawing.Size(252, 20)
    Me.lbAn_descr1.TabIndex = 62
    Me.lbAn_descr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbAn_descr1.Tooltip = ""
    Me.lbAn_descr1.UseMnemonic = False
    '
    'lbSconti
    '
    Me.lbSconti.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbSconti.AutoSize = True
    Me.lbSconti.BackColor = System.Drawing.Color.Transparent
    Me.lbSconti.Location = New System.Drawing.Point(407, 32)
    Me.lbSconti.Name = "lbSconti"
    Me.lbSconti.NTSDbField = ""
    Me.lbSconti.Size = New System.Drawing.Size(36, 13)
    Me.lbSconti.TabIndex = 58
    Me.lbSconti.Text = "Sconti"
    Me.lbSconti.Tooltip = ""
    Me.lbSconti.UseMnemonic = False
    '
    'lbTb_despaga
    '
    Me.lbTb_despaga.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbTb_despaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_despaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_despaga.Location = New System.Drawing.Point(148, 369)
    Me.lbTb_despaga.Name = "lbTb_despaga"
    Me.lbTb_despaga.NTSDbField = ""
    Me.lbTb_despaga.Size = New System.Drawing.Size(226, 20)
    Me.lbTb_despaga.TabIndex = 71
    Me.lbTb_despaga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbTb_despaga.Tooltip = ""
    Me.lbTb_despaga.UseMnemonic = False
    '
    'lbTotaleDoc
    '
    Me.lbTotaleDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTotaleDoc.AutoSize = True
    Me.lbTotaleDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTotaleDoc.Location = New System.Drawing.Point(393, 372)
    Me.lbTotaleDoc.Name = "lbTotaleDoc"
    Me.lbTotaleDoc.NTSDbField = ""
    Me.lbTotaleDoc.Size = New System.Drawing.Size(94, 13)
    Me.lbTotaleDoc.TabIndex = 70
    Me.lbTotaleDoc.Text = "Totale Documento"
    Me.lbTotaleDoc.Tooltip = ""
    Me.lbTotaleDoc.UseMnemonic = False
    '
    'edTd_totdoc
    '
    Me.edTd_totdoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTd_totdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTd_totdoc.Enabled = False
    Me.edTd_totdoc.Location = New System.Drawing.Point(569, 369)
    Me.edTd_totdoc.Name = "edTd_totdoc"
    Me.edTd_totdoc.NTSDbField = ""
    Me.edTd_totdoc.NTSFormat = "0"
    Me.edTd_totdoc.NTSForzaVisZoom = False
    Me.edTd_totdoc.NTSOldValue = ""
    Me.edTd_totdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTd_totdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTd_totdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTd_totdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTd_totdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTd_totdoc.Properties.MaxLength = 65536
    Me.edTd_totdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTd_totdoc.Size = New System.Drawing.Size(113, 20)
    Me.edTd_totdoc.TabIndex = 68
    '
    'lbPagamento
    '
    Me.lbPagamento.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbPagamento.AutoSize = True
    Me.lbPagamento.BackColor = System.Drawing.Color.Transparent
    Me.lbPagamento.Location = New System.Drawing.Point(11, 372)
    Me.lbPagamento.Name = "lbPagamento"
    Me.lbPagamento.NTSDbField = ""
    Me.lbPagamento.Size = New System.Drawing.Size(61, 13)
    Me.lbPagamento.TabIndex = 69
    Me.lbPagamento.Text = "Pagamento"
    Me.lbPagamento.Tooltip = ""
    Me.lbPagamento.UseMnemonic = False
    '
    'edTm_codpaga
    '
    Me.edTm_codpaga.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.edTm_codpaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codpaga.Enabled = False
    Me.edTm_codpaga.Location = New System.Drawing.Point(78, 369)
    Me.edTm_codpaga.Name = "edTm_codpaga"
    Me.edTm_codpaga.NTSDbField = ""
    Me.edTm_codpaga.NTSFormat = "0"
    Me.edTm_codpaga.NTSForzaVisZoom = False
    Me.edTm_codpaga.NTSOldValue = ""
    Me.edTm_codpaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codpaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codpaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codpaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codpaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codpaga.Properties.MaxLength = 65536
    Me.edTm_codpaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codpaga.Size = New System.Drawing.Size(64, 20)
    Me.edTm_codpaga.TabIndex = 67
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbTotaleDocNoOmag)
    Me.NtsPanel1.Controls.Add(Me.lbTotaleOmag)
    Me.NtsPanel1.Controls.Add(Me.edTd_totdocnoomag)
    Me.NtsPanel1.Controls.Add(Me.edTd_totomag)
    Me.NtsPanel1.Controls.Add(Me.lbTm_conto)
    Me.NtsPanel1.Controls.Add(Me.lbTotaleDoc)
    Me.NtsPanel1.Controls.Add(Me.edTd_totdoc)
    Me.NtsPanel1.Controls.Add(Me.lbTb_despaga)
    Me.NtsPanel1.Controls.Add(Me.lbAn_descr1)
    Me.NtsPanel1.Controls.Add(Me.lbNumdoc)
    Me.NtsPanel1.Controls.Add(Me.lbNumpar)
    Me.NtsPanel1.Controls.Add(Me.lbPagamento)
    Me.NtsPanel1.Controls.Add(Me.edTm_codpaga)
    Me.NtsPanel1.Controls.Add(Me.edTm_conto)
    Me.NtsPanel1.Controls.Add(Me.lbFatturato)
    Me.NtsPanel1.Controls.Add(Me.lbRiferimenti)
    Me.NtsPanel1.Controls.Add(Me.grGrbo)
    Me.NtsPanel1.Controls.Add(Me.edTm_scont1)
    Me.NtsPanel1.Controls.Add(Me.lbSconti)
    Me.NtsPanel1.Controls.Add(Me.lbTm_flfatt)
    Me.NtsPanel1.Controls.Add(Me.edTm_scont2)
    Me.NtsPanel1.Controls.Add(Me.lbTm_riferim)
    Me.NtsPanel1.Controls.Add(Me.edTm_scopag)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 30)
    Me.NtsPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(694, 453)
    Me.NtsPanel1.TabIndex = 72
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'lbTotaleDocNoOmag
    '
    Me.lbTotaleDocNoOmag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTotaleDocNoOmag.AutoSize = True
    Me.lbTotaleDocNoOmag.BackColor = System.Drawing.Color.Transparent
    Me.lbTotaleDocNoOmag.Location = New System.Drawing.Point(393, 416)
    Me.lbTotaleDocNoOmag.Name = "lbTotaleDocNoOmag"
    Me.lbTotaleDocNoOmag.NTSDbField = ""
    Me.lbTotaleDocNoOmag.Size = New System.Drawing.Size(160, 13)
    Me.lbTotaleDocNoOmag.TabIndex = 74
    Me.lbTotaleDocNoOmag.Text = "Totale Documento netto omaggi"
    Me.lbTotaleDocNoOmag.Tooltip = ""
    Me.lbTotaleDocNoOmag.UseMnemonic = False
    '
    'lbTotaleOmag
    '
    Me.lbTotaleOmag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTotaleOmag.AutoSize = True
    Me.lbTotaleOmag.BackColor = System.Drawing.Color.Transparent
    Me.lbTotaleOmag.Location = New System.Drawing.Point(393, 394)
    Me.lbTotaleOmag.Name = "lbTotaleOmag"
    Me.lbTotaleOmag.NTSDbField = ""
    Me.lbTotaleOmag.Size = New System.Drawing.Size(76, 13)
    Me.lbTotaleOmag.TabIndex = 73
    Me.lbTotaleOmag.Text = "Totale Omaggi"
    Me.lbTotaleOmag.Tooltip = ""
    Me.lbTotaleOmag.UseMnemonic = False
    '
    'edTd_totdocnoomag
    '
    Me.edTd_totdocnoomag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTd_totdocnoomag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTd_totdocnoomag.Enabled = False
    Me.edTd_totdocnoomag.Location = New System.Drawing.Point(569, 413)
    Me.edTd_totdocnoomag.Name = "edTd_totdocnoomag"
    Me.edTd_totdocnoomag.NTSDbField = ""
    Me.edTd_totdocnoomag.NTSFormat = "0"
    Me.edTd_totdocnoomag.NTSForzaVisZoom = False
    Me.edTd_totdocnoomag.NTSOldValue = ""
    Me.edTd_totdocnoomag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTd_totdocnoomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTd_totdocnoomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTd_totdocnoomag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTd_totdocnoomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTd_totdocnoomag.Properties.MaxLength = 65536
    Me.edTd_totdocnoomag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTd_totdocnoomag.Size = New System.Drawing.Size(113, 20)
    Me.edTd_totdocnoomag.TabIndex = 73
    '
    'edTd_totomag
    '
    Me.edTd_totomag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTd_totomag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTd_totomag.Enabled = False
    Me.edTd_totomag.Location = New System.Drawing.Point(569, 391)
    Me.edTd_totomag.Name = "edTd_totomag"
    Me.edTd_totomag.NTSDbField = ""
    Me.edTd_totomag.NTSFormat = "0"
    Me.edTd_totomag.NTSForzaVisZoom = False
    Me.edTd_totomag.NTSOldValue = ""
    Me.edTd_totomag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTd_totomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTd_totomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTd_totomag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTd_totomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTd_totomag.Properties.MaxLength = 65536
    Me.edTd_totomag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTd_totomag.Size = New System.Drawing.Size(113, 20)
    Me.edTd_totomag.TabIndex = 72
    '
    'lbFatturato
    '
    Me.lbFatturato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbFatturato.AutoSize = True
    Me.lbFatturato.BackColor = System.Drawing.Color.Transparent
    Me.lbFatturato.Location = New System.Drawing.Point(407, 60)
    Me.lbFatturato.Name = "lbFatturato"
    Me.lbFatturato.NTSDbField = ""
    Me.lbFatturato.Size = New System.Drawing.Size(53, 13)
    Me.lbFatturato.TabIndex = 59
    Me.lbFatturato.Text = "Fatturato"
    Me.lbFatturato.Tooltip = ""
    Me.lbFatturato.UseMnemonic = False
    '
    'lbTm_flfatt
    '
    Me.lbTm_flfatt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTm_flfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_flfatt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTm_flfatt.Location = New System.Drawing.Point(471, 56)
    Me.lbTm_flfatt.Name = "lbTm_flfatt"
    Me.lbTm_flfatt.NTSDbField = ""
    Me.lbTm_flfatt.Size = New System.Drawing.Size(64, 20)
    Me.lbTm_flfatt.TabIndex = 65
    Me.lbTm_flfatt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbTm_flfatt.Tooltip = ""
    Me.lbTm_flfatt.UseMnemonic = False
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa anteprima a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'FRMMGGRBO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(694, 483)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGGRBO"
    Me.NTSLastControlFocussed = Me.grGrbo
    Me.Text = "STAMPA/VISUALIZZAZIONE MOVIMENTI DI MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrbo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrbo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_conto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scont1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scont2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scopag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTd_totdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codpaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edTd_totdocnoomag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTd_totomag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function
  Public Overridable Sub InitEntity(ByRef oCleGrbo As CLEMGSTBO)
    Try
      Me.oCleGrbo = oCleGrbo
      dsGrbo = oCleGrbo.dsShared
      AddHandler Me.oCleGrbo.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub LoadImage()
    '-------------------------------------------------
    'carico le immagini della toolbar
    Try
      tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
    Catch
    End Try
    Try
      tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
    Catch
    End Try
    Try
      tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
    Catch
    End Try
    Try
      tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
    Catch
    End Try
    Try
      tlbDettqta.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
    Catch
    End Try
    Try
      tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
    Catch
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edTm_conto.NTSSetParam(oMenu, oApp.Tr(Me, 128643822176406250, "Codice cliente/forn."), "0", 9, 0, 999999999)
      edTm_scont1.NTSSetParam(oMenu, oApp.Tr(Me, 128643822208125000, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      edTm_scont2.NTSSetParam(oMenu, oApp.Tr(Me, 128643822240937500, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      edTm_scopag.NTSSetParam(oMenu, oApp.Tr(Me, 128643822310468750, "Sconto pagamento"), oApp.FormatSconti, 6, -100, 100)
      edTm_codpaga.NTSSetParam(oMenu, oApp.Tr(Me, 128643822280625000, "Codice pagamento"), "0", 4, 0, 9999)
      edTd_totdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128643822339687500, "Totale documento"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTd_totomag.NTSSetParam(oMenu, oApp.Tr(Me, 129177171044063475, "Totale omaggi"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTd_totdocnoomag.NTSSetParam(oMenu, oApp.Tr(Me, 129177171091628790, "Totale documento netto omaggi"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)

      '-------------------------------------------------
      'completo le informazioni della griglia
      grvGrbo.NTSSetParam(oMenu, "STAMPA/VISUALIZZAZIONE MOVIMENTI DI MAGAZZINO")
      mm_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199786093750, "Cs."), "0", 4, 0, 9999)
      mm_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199786250000, "Codice Articolo"), CLN__STD.CodartMaxLen, True)
      mm_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199786406250, "Descrizione"), 40, True)
      mm_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199786562500, "Quantità"), oApp.FormatQta, 15)
      mm_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199786718750, "Prezzo"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      mm_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199786875000, "Sc.1"), oApp.FormatSconti, 6, -100, 100)
      mm_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787031250, "Sc.2"), oApp.FormatSconti, 6, -100, 100)
      mm_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787187500, "Sc.3"), oApp.FormatSconti, 6, -100, 100)
      mm_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787343750, "Sc.4"), oApp.FormatSconti, 6, -100, 100)
      mm_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787500000, "Sc.5"), oApp.FormatSconti, 6, -100, 100)
      mm_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787656250, "Sc.6"), oApp.FormatSconti, 6, -100, 100)
      mm_codiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199787812500, "Codice IVA"), "0", 4, 0, 9999)
      tb_desciva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199787968750, "Descrizione IVA"), 20, True)
      mm_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199788125000, "Fase"), "0", 4, 0, 9999)
      af_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199788281250, "Descrizione fase"), 40, True)
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199788437500, "Lotto"), 50, True)
      mm_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128643199788593750, "Commessa"), "0", 9, 0, 999999999)
      mm_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128643199788750000, "Subcommessa"), 2, True)

      grvGrbo.NTSAllowUpdate = False
      grvGrbo.NTSAllowInsert = False
      grvGrbo.NTSAllowDelete = False

      grvGrbo.Enabled = False

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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano gia'  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edTm_conto.NTSDbField = oCleGrbo.strNomeTabella & ".tm_conto"
      lbAn_descr1.NTSDbField = strDettaglioTesta & ".an_descr1"
      edTm_scont1.NTSDbField = strDettaglioTesta & ".tm_scont1"
      edTm_scont2.NTSDbField = strDettaglioTesta & ".tm_scont2"
      edTm_scopag.NTSDbField = strDettaglioTesta & ".tm_scopag"
      edTm_codpaga.NTSDbField = strDettaglioTesta & ".tm_codpaga"
      lbTb_despaga.NTSDbField = strDettaglioTesta & ".tb_despaga"
      edTd_totdoc.NTSDbField = strDettaglioTesta & ".tm_totdoc"
      edTd_totomag.NTSDbField = strDettaglioTesta & ".tm_totomag"
      edTd_totdocnoomag.NTSDbField = strDettaglioTesta & ".tm_totdocnoomag"
      lbTm_riferim.NTSDbField = strDettaglioTesta & ".tm_riferim"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGrbo, Me)
      NTSFormAddDataBinding(dcGrboTestata, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMMGGRBO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dcGrbo.DataSource = dsGrbo.Tables(oCleGrbo.strNomeTabella)
      dsGrbo.AcceptChanges()

      If Not ApriDettaglio() Then
        Me.Close()
        Return
      End If

      If oCleGrbo.bModTCO = False Then tlbDettqta.Enabled = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly, nasconde, sempre, alcuni controlli/colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbDettqta.Visible = False
        mm_scont3.Visible = False
        mm_scont4.Visible = False
        mm_scont5.Visible = False
        mm_scont6.Visible = False
        mm_fase.Visible = False
        af_descr.Visible = False
        xx_lottox.Visible = False
        mm_commeca.Visible = False
        mm_subcommeca.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.IsBis Then tlbStampaVideo.Visible = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGRBO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrbo.Dispose()
      dcGrboGrid.Dispose()
      dcGrboTestata.Dispose()
      If Not dsGrbo Is Nothing Then dsGrbo.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNavigazione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick, tlbPrecedente.ItemClick, tlbSuccessivo.ItemClick, tlbUltimo.ItemClick
    Dim nPosTmp As Integer
    Try
      nPosTmp = dcGrbo.Position
      Select Case e.Item.Name
        Case tlbPrimo.Name
          dcGrbo.MoveFirst()
        Case tlbPrecedente.Name
          dcGrbo.MovePrevious()
        Case tlbSuccessivo.Name
          dcGrbo.MoveNext()
        Case tlbUltimo.Name
          dcGrbo.MoveLast()
      End Select
      If nPosTmp = dcGrbo.Position Then Return

      If Not ApriDettaglio() Then
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettqta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettqta.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSMGSTBO"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               "".PadLeft(CLN__STD.CodartMaxLen) & "|" & _
               "".PadLeft(CLN__STD.CodartMaxLen, "z"c) & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               NTSCStr(dsGrbo.Tables(oCleGrbo.strNomeTabella).Rows(dcGrbo.Position)!tm_tipork) & ";" & _
               NTSCStr(dsGrbo.Tables(oCleGrbo.strNomeTabella).Rows(dcGrbo.Position)!tm_anno).PadLeft(4, "0"c) & ";" & _
               (NTSCStr(dsGrbo.Tables(oCleGrbo.strNomeTabella).Rows(dcGrbo.Position)!tm_serie) & " ").Substring(0, 1) & ";" & _
               NTSCStr(dsGrbo.Tables(oCleGrbo.strNomeTabella).Rows(dcGrbo.Position)!tm_numdoc).PadLeft(9, "0"c) & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Dim strHeader As String = ""
    Dim strFooter As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strHeader = "CONTO: " & edTm_conto.Text & "".PadLeft(1) & lbAn_descr1.Text & "".PadLeft(5) & _
        lbNumdoc.Text & "".PadLeft(5) & lbNumpar.Text
      strFooter = "PAGAMENTO: " & edTm_codpaga.Text & "".PadLeft(1) & lbTb_despaga.Text & "".PadLeft(10) & _
        "TOTALE DOCUMENTO: " & edTd_totdoc.Text
      grvGrbo.NTSPrintPreview(strHeader, strFooter)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Function ApriDettaglio() As Boolean
    Try
      If oCleGrbo.ApriDettaglio(dcGrbo.Position, strDettaglioTesta, strDettaglioRighe) Then
        dcGrboTestata.DataSource = dsGrbo.Tables(strDettaglioTesta)
        dcGrboGrid.DataSource = dsGrbo.Tables(strDettaglioRighe)

        dsGrbo.AcceptChanges()

        grGrbo.DataSource = dcGrboGrid
        'grvGrbo.BestFitColumns()

        Dim strNumdoc As String = ""
        With dsGrbo.Tables(oCleGrbo.strNomeTabella).Rows(dcGrbo.Position)
          Select Case NTSCStr(!tm_tipork)
            Case "A"
              strNumdoc = oApp.Tr(Me, 129065519708515024, "Fattura Acc. emessa il ")
            Case "B"
              strNumdoc = oApp.Tr(Me, 129065519728372744, "Bolla emessa il ")
            Case "C"
              strNumdoc = oApp.Tr(Me, 129065519747448664, "Corrispettivo emesso il ")
            Case "D"
              strNumdoc = oApp.Tr(Me, 129065519774655304, "Fattura Diff. emessa il ")
            Case "E"
              strNumdoc = oApp.Tr(Me, 129065519809523584, "Fattura Imm. emessa il ")
            Case "F"
              strNumdoc = oApp.Tr(Me, 129065519830163104, "Ricevuta fisc.emessa il ")
            Case "I"
              strNumdoc = oApp.Tr(Me, 129065519847988144, "Riemiss.ric.fisc.emessa il ")
            Case "J"
              strNumdoc = oApp.Tr(Me, 129065519868471304, "Nota Accr. ricevuta il ")
            Case "K"
              strNumdoc = oApp.Tr(Me, 129065519887547224, "Fattura Diff. ricevuta il ")
            Case "L"
              strNumdoc = oApp.Tr(Me, 129065519916004744, "Fattura Imm./Acc. ricevuta il ")
            Case "M"
              strNumdoc = oApp.Tr(Me, 129065519935393384, "Bolla ricevuta/Bolla Int. del ")
            Case "N"
              strNumdoc = oApp.Tr(Me, 129065519953218424, "Nota Accr. emessa il ")
            Case "P"
              strNumdoc = oApp.Tr(Me, 129065519984021344, "Fattura ricev.fisc differita del ")
            Case "S"
              strNumdoc = oApp.Tr(Me, 129065520006537184, "Fattura ricev.fisc.emessa il ")
            Case "T"
              strNumdoc = oApp.Tr(Me, 129065520040310944, "Carico da produzione del ")
            Case "U"
              strNumdoc = oApp.Tr(Me, 129065520064390384, "Scarico a produzione del ")
            Case "Z"
              strNumdoc = oApp.Tr(Me, 129065520084560824, "Bolla interna del ")
            Case "W"
              strNumdoc = oApp.Tr(Me, 129065520108327544, "Nota di prelievo del ")
            Case "£"
              strNumdoc = oApp.Tr(Me, 129242878246474609, "Nota accred. diff. emessa il ")
            Case "("
              strNumdoc = oApp.Tr(Me, 129242878264628906, "Nota accred. diff. ricevuta il ")
          End Select
          strNumdoc += NTSCDate(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_datdoc) & " N° " & NTSCStr(!tm_numdoc)
          If Trim(NTSCStr(!tm_serie)) <> "" Then strNumdoc += "/" & NTSCStr(!tm_serie)
          lbNumdoc.Text = strNumdoc

          Dim strNumpar As String = ""
          strNumpar = "Partita N° " & NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_numpar)
          If Trim(NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_alfpar)) <> "" Then strNumpar += "/" & NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_alfpar)
          If NTSCInt(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_numpar) <> 0 And NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_datpar) <> "" Then strNumpar = strNumpar & " del " & NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_datpar)
          lbNumpar.Text = strNumpar

          Select Case NTSCStr(!tm_tipork)
            Case "B", "F", "M"
              If Trim(NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_flfatt)) <> "" Then
                Select Case NTSCStr(dsGrbo.Tables(strDettaglioTesta).Rows(0)!tm_flfatt)
                  Case "S"
                    lbTm_flfatt.Text = oApp.Tr(Me, 129065520171809704, "Sì")
                  Case "N"
                    lbTm_flfatt.Text = oApp.Tr(Me, 129065520197140024, "No")
                End Select
                GctlSetVisEnab(lbFatturato, True)
                GctlSetVisEnab(lbTm_flfatt, True)
              End If
            Case Else
              lbFatturato.Visible = False
              lbTm_flfatt.Visible = False
          End Select
        End With

        '-------------------------------------------------
        'collego il BindingSource ai vari controlli 
        Bindcontrols()

        Return True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
End Class
