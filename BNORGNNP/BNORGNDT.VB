Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNDT
#Region "Variabili"
  Public oCleGnnp As CLEORGNNP
  Public oCallParams As CLE__CLDP
  Public dsGndt As New DataSet
  Public dcGndt As New BindingSource()
  Private components As System.ComponentModel.IContainer

  Public bClose As Boolean = False

  Public WithEvents grGndt As NTSInformatica.NTSGrid
  Public WithEvents grvGndt As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents mn_riga As NTSInformatica.NTSGridColumn
  Public WithEvents mn_conto As NTSInformatica.NTSGridColumn
  Public WithEvents mn_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents mn_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mn_numord As NTSInformatica.NTSGridColumn
  Public WithEvents mn_serie As NTSInformatica.NTSGridColumn
  Public WithEvents mn_datord As NTSInformatica.NTSGridColumn
  Public WithEvents mn_descr As NTSInformatica.NTSGridColumn
  Public WithEvents edTaglia As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edLotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCliente As NTSInformatica.NTSTextBoxStr
  Public WithEvents edImpegno As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTaglia As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbLotto As NTSInformatica.NTSLabel
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents lbCliente As NTSInformatica.NTSLabel
  Public WithEvents lbImpegno As NTSInformatica.NTSLabel
  Public WithEvents lbQtaass As NTSInformatica.NTSLabel
  Public WithEvents lbDisp As NTSInformatica.NTSLabel
  Public WithEvents edQtaass As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDisp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Taglia As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Fase As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Lotto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Commeca As NTSInformatica.NTSLabel
  Public WithEvents mn_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents mn_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mn_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents mn_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents mn_mmquant As NTSInformatica.NTSGridColumn
  Public WithEvents mn_mmflevas As NTSInformatica.NTSGridColumn
  Public WithEvents mn_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mn_coleva As NTSInformatica.NTSGridColumn
  Public WithEvents mn_colpre As NTSInformatica.NTSGridColumn
  Public WithEvents mn_mmcolli As NTSInformatica.NTSGridColumn
  Public WithEvents mn_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents mn_rigaa As NTSInformatica.NTSGridColumn
  Public WithEvents mn_tcindtagl As NTSInformatica.NTSGridColumn
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAndescr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAndescr1 As NTSInformatica.NTSLabel
  Public WithEvents edAndescr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAnprov As NTSInformatica.NTSLabel
  Public WithEvents edAnprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAncitta As NTSInformatica.NTSLabel
  Public WithEvents edAncitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAnindir As NTSInformatica.NTSLabel
  Public WithEvents edAnindir As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDdprov As NTSInformatica.NTSLabel
  Public WithEvents edDdprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDdcitta As NTSInformatica.NTSLabel
  Public WithEvents edDdcitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDdindir As NTSInformatica.NTSLabel
  Public WithEvents edDdindir As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCoddest2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edLottox As NTSInformatica.NTSTextBoxStr
  Public WithEvents mn_tctaglia As NTSInformatica.NTSGridColumn
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGNDT))
    Me.grGndt = New NTSInformatica.NTSGrid
    Me.grvGndt = New NTSInformatica.NTSGridView
    Me.mn_riga = New NTSInformatica.NTSGridColumn
    Me.mn_conto = New NTSInformatica.NTSGridColumn
    Me.mn_descr1 = New NTSInformatica.NTSGridColumn
    Me.mn_codart = New NTSInformatica.NTSGridColumn
    Me.mn_descr = New NTSInformatica.NTSGridColumn
    Me.mn_numord = New NTSInformatica.NTSGridColumn
    Me.mn_serie = New NTSInformatica.NTSGridColumn
    Me.mn_datord = New NTSInformatica.NTSGridColumn
    Me.mn_datcons = New NTSInformatica.NTSGridColumn
    Me.mn_quant = New NTSInformatica.NTSGridColumn
    Me.mn_quaeva = New NTSInformatica.NTSGridColumn
    Me.mn_quapre = New NTSInformatica.NTSGridColumn
    Me.mn_mmquant = New NTSInformatica.NTSGridColumn
    Me.mn_mmflevas = New NTSInformatica.NTSGridColumn
    Me.mn_colli = New NTSInformatica.NTSGridColumn
    Me.mn_coleva = New NTSInformatica.NTSGridColumn
    Me.mn_colpre = New NTSInformatica.NTSGridColumn
    Me.mn_mmcolli = New NTSInformatica.NTSGridColumn
    Me.mn_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.xx_lotto = New NTSInformatica.NTSGridColumn
    Me.mn_rigaa = New NTSInformatica.NTSGridColumn
    Me.mn_tcindtagl = New NTSInformatica.NTSGridColumn
    Me.mn_tctaglia = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edImpegno = New NTSInformatica.NTSTextBoxStr
    Me.edCliente = New NTSInformatica.NTSTextBoxStr
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.edLotto = New NTSInformatica.NTSTextBoxNum
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edTaglia = New NTSInformatica.NTSTextBoxNum
    Me.lbImpegno = New NTSInformatica.NTSLabel
    Me.lbCliente = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.lbLotto = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbTaglia = New NTSInformatica.NTSLabel
    Me.lbXx_Commeca = New NTSInformatica.NTSLabel
    Me.lbXx_Lotto = New NTSInformatica.NTSLabel
    Me.lbXx_Fase = New NTSInformatica.NTSLabel
    Me.lbXx_Taglia = New NTSInformatica.NTSLabel
    Me.edQtaass = New NTSInformatica.NTSTextBoxNum
    Me.edDisp = New NTSInformatica.NTSTextBoxNum
    Me.lbDisp = New NTSInformatica.NTSLabel
    Me.lbQtaass = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edLottox = New NTSInformatica.NTSTextBoxStr
    Me.edCoddest2 = New NTSInformatica.NTSTextBoxStr
    Me.lbDdprov = New NTSInformatica.NTSLabel
    Me.edDdprov = New NTSInformatica.NTSTextBoxStr
    Me.lbDdcitta = New NTSInformatica.NTSLabel
    Me.edDdcitta = New NTSInformatica.NTSTextBoxStr
    Me.lbDdindir = New NTSInformatica.NTSLabel
    Me.edDdindir = New NTSInformatica.NTSTextBoxStr
    Me.lbAnprov = New NTSInformatica.NTSLabel
    Me.edAnprov = New NTSInformatica.NTSTextBoxStr
    Me.lbAncitta = New NTSInformatica.NTSLabel
    Me.edAncitta = New NTSInformatica.NTSTextBoxStr
    Me.lbAnindir = New NTSInformatica.NTSLabel
    Me.edAnindir = New NTSInformatica.NTSTextBoxStr
    Me.edAndescr2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAndescr1 = New NTSInformatica.NTSLabel
    Me.edAndescr1 = New NTSInformatica.NTSTextBoxStr
    Me.lbCoddest = New NTSInformatica.NTSLabel
    Me.edCoddest = New NTSInformatica.NTSTextBoxStr
    Me.pnFill = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGndt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGndt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edImpegno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTaglia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQtaass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDisp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edLottox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDdprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDdcitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDdindir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAncitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnindir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAndescr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAndescr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
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
    'grGndt
    '
    Me.grGndt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grGndt.EmbeddedNavigator.Name = ""
    Me.grGndt.Location = New System.Drawing.Point(0, 0)
    Me.grGndt.MainView = Me.grvGndt
    Me.grGndt.Name = "grGndt"
    Me.grGndt.Size = New System.Drawing.Size(720, 178)
    Me.grGndt.TabIndex = 0
    Me.grGndt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGndt})
    '
    'grvGndt
    '
    Me.grvGndt.ActiveFilterEnabled = False
    Me.grvGndt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mn_riga, Me.mn_conto, Me.mn_descr1, Me.mn_codart, Me.mn_descr, Me.mn_numord, Me.mn_serie, Me.mn_datord, Me.mn_datcons, Me.mn_quant, Me.mn_quaeva, Me.mn_quapre, Me.mn_mmquant, Me.mn_mmflevas, Me.mn_colli, Me.mn_coleva, Me.mn_colpre, Me.mn_mmcolli, Me.mn_commeca, Me.xx_commeca, Me.xx_lottox, Me.xx_lotto, Me.mn_rigaa, Me.mn_tcindtagl, Me.mn_tctaglia})
    Me.grvGndt.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGndt.Enabled = True
    Me.grvGndt.GridControl = Me.grGndt
    Me.grvGndt.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGndt.MinRowHeight = 14
    Me.grvGndt.Name = "grvGndt"
    Me.grvGndt.NTSAllowDelete = True
    Me.grvGndt.NTSAllowInsert = True
    Me.grvGndt.NTSAllowUpdate = True
    Me.grvGndt.NTSMenuContext = Nothing
    Me.grvGndt.OptionsCustomization.AllowRowSizing = True
    Me.grvGndt.OptionsFilter.AllowFilterEditor = False
    Me.grvGndt.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGndt.OptionsNavigation.UseTabKey = False
    Me.grvGndt.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGndt.OptionsView.ColumnAutoWidth = False
    Me.grvGndt.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGndt.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGndt.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGndt.OptionsView.ShowGroupPanel = False
    Me.grvGndt.RowHeight = 16
    '
    'mn_riga
    '
    Me.mn_riga.AppearanceCell.Options.UseBackColor = True
    Me.mn_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mn_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_riga.Caption = "Riga"
    Me.mn_riga.Enabled = False
    Me.mn_riga.FieldName = "mn_riga"
    Me.mn_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_riga.Name = "mn_riga"
    Me.mn_riga.NTSRepositoryComboBox = Nothing
    Me.mn_riga.NTSRepositoryItemCheck = Nothing
    Me.mn_riga.NTSRepositoryItemMemo = Nothing
    Me.mn_riga.NTSRepositoryItemText = Nothing
    Me.mn_riga.OptionsColumn.AllowEdit = False
    Me.mn_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_riga.OptionsColumn.ReadOnly = True
    Me.mn_riga.OptionsFilter.AllowFilter = False
    Me.mn_riga.Visible = True
    Me.mn_riga.VisibleIndex = 0
    '
    'mn_conto
    '
    Me.mn_conto.AppearanceCell.Options.UseBackColor = True
    Me.mn_conto.AppearanceCell.Options.UseTextOptions = True
    Me.mn_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_conto.Caption = "Cliente"
    Me.mn_conto.Enabled = False
    Me.mn_conto.FieldName = "mn_conto"
    Me.mn_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_conto.Name = "mn_conto"
    Me.mn_conto.NTSRepositoryComboBox = Nothing
    Me.mn_conto.NTSRepositoryItemCheck = Nothing
    Me.mn_conto.NTSRepositoryItemMemo = Nothing
    Me.mn_conto.NTSRepositoryItemText = Nothing
    Me.mn_conto.OptionsColumn.AllowEdit = False
    Me.mn_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_conto.OptionsColumn.ReadOnly = True
    Me.mn_conto.OptionsFilter.AllowFilter = False
    Me.mn_conto.Visible = True
    Me.mn_conto.VisibleIndex = 1
    '
    'mn_descr1
    '
    Me.mn_descr1.AppearanceCell.Options.UseBackColor = True
    Me.mn_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.mn_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_descr1.Caption = "Descrizione"
    Me.mn_descr1.Enabled = False
    Me.mn_descr1.FieldName = "mn_descr1"
    Me.mn_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_descr1.Name = "mn_descr1"
    Me.mn_descr1.NTSRepositoryComboBox = Nothing
    Me.mn_descr1.NTSRepositoryItemCheck = Nothing
    Me.mn_descr1.NTSRepositoryItemMemo = Nothing
    Me.mn_descr1.NTSRepositoryItemText = Nothing
    Me.mn_descr1.OptionsColumn.AllowEdit = False
    Me.mn_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_descr1.OptionsColumn.ReadOnly = True
    Me.mn_descr1.OptionsFilter.AllowFilter = False
    Me.mn_descr1.Visible = True
    Me.mn_descr1.VisibleIndex = 2
    '
    'mn_codart
    '
    Me.mn_codart.AppearanceCell.Options.UseBackColor = True
    Me.mn_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mn_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_codart.Caption = "Codice art."
    Me.mn_codart.Enabled = False
    Me.mn_codart.FieldName = "mn_codart"
    Me.mn_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_codart.Name = "mn_codart"
    Me.mn_codart.NTSRepositoryComboBox = Nothing
    Me.mn_codart.NTSRepositoryItemCheck = Nothing
    Me.mn_codart.NTSRepositoryItemMemo = Nothing
    Me.mn_codart.NTSRepositoryItemText = Nothing
    Me.mn_codart.OptionsColumn.AllowEdit = False
    Me.mn_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_codart.OptionsColumn.ReadOnly = True
    Me.mn_codart.OptionsFilter.AllowFilter = False
    Me.mn_codart.Visible = True
    Me.mn_codart.VisibleIndex = 3
    '
    'mn_descr
    '
    Me.mn_descr.AppearanceCell.Options.UseBackColor = True
    Me.mn_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mn_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_descr.Caption = "Descr. articolo"
    Me.mn_descr.Enabled = False
    Me.mn_descr.FieldName = "mn_descr"
    Me.mn_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_descr.Name = "mn_descr"
    Me.mn_descr.NTSRepositoryComboBox = Nothing
    Me.mn_descr.NTSRepositoryItemCheck = Nothing
    Me.mn_descr.NTSRepositoryItemMemo = Nothing
    Me.mn_descr.NTSRepositoryItemText = Nothing
    Me.mn_descr.OptionsColumn.AllowEdit = False
    Me.mn_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_descr.OptionsColumn.ReadOnly = True
    Me.mn_descr.OptionsFilter.AllowFilter = False
    Me.mn_descr.Visible = True
    Me.mn_descr.VisibleIndex = 4
    '
    'mn_numord
    '
    Me.mn_numord.AppearanceCell.Options.UseBackColor = True
    Me.mn_numord.AppearanceCell.Options.UseTextOptions = True
    Me.mn_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_numord.Caption = "Num. ord."
    Me.mn_numord.Enabled = False
    Me.mn_numord.FieldName = "mn_numord"
    Me.mn_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_numord.Name = "mn_numord"
    Me.mn_numord.NTSRepositoryComboBox = Nothing
    Me.mn_numord.NTSRepositoryItemCheck = Nothing
    Me.mn_numord.NTSRepositoryItemMemo = Nothing
    Me.mn_numord.NTSRepositoryItemText = Nothing
    Me.mn_numord.OptionsColumn.AllowEdit = False
    Me.mn_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_numord.OptionsColumn.ReadOnly = True
    Me.mn_numord.OptionsFilter.AllowFilter = False
    Me.mn_numord.Visible = True
    Me.mn_numord.VisibleIndex = 5
    '
    'mn_serie
    '
    Me.mn_serie.AppearanceCell.Options.UseBackColor = True
    Me.mn_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mn_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_serie.Caption = "Serie"
    Me.mn_serie.Enabled = False
    Me.mn_serie.FieldName = "mn_serie"
    Me.mn_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_serie.Name = "mn_serie"
    Me.mn_serie.NTSRepositoryComboBox = Nothing
    Me.mn_serie.NTSRepositoryItemCheck = Nothing
    Me.mn_serie.NTSRepositoryItemMemo = Nothing
    Me.mn_serie.NTSRepositoryItemText = Nothing
    Me.mn_serie.OptionsColumn.AllowEdit = False
    Me.mn_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_serie.OptionsColumn.ReadOnly = True
    Me.mn_serie.OptionsFilter.AllowFilter = False
    Me.mn_serie.Visible = True
    Me.mn_serie.VisibleIndex = 6
    '
    'mn_datord
    '
    Me.mn_datord.AppearanceCell.Options.UseBackColor = True
    Me.mn_datord.AppearanceCell.Options.UseTextOptions = True
    Me.mn_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_datord.Caption = "Data ord."
    Me.mn_datord.Enabled = False
    Me.mn_datord.FieldName = "mn_datord"
    Me.mn_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_datord.Name = "mn_datord"
    Me.mn_datord.NTSRepositoryComboBox = Nothing
    Me.mn_datord.NTSRepositoryItemCheck = Nothing
    Me.mn_datord.NTSRepositoryItemMemo = Nothing
    Me.mn_datord.NTSRepositoryItemText = Nothing
    Me.mn_datord.OptionsColumn.AllowEdit = False
    Me.mn_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_datord.OptionsColumn.ReadOnly = True
    Me.mn_datord.OptionsFilter.AllowFilter = False
    Me.mn_datord.Visible = True
    Me.mn_datord.VisibleIndex = 7
    '
    'mn_datcons
    '
    Me.mn_datcons.AppearanceCell.Options.UseBackColor = True
    Me.mn_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.mn_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_datcons.Caption = "Data cons."
    Me.mn_datcons.Enabled = False
    Me.mn_datcons.FieldName = "mn_datcons"
    Me.mn_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_datcons.Name = "mn_datcons"
    Me.mn_datcons.NTSRepositoryComboBox = Nothing
    Me.mn_datcons.NTSRepositoryItemCheck = Nothing
    Me.mn_datcons.NTSRepositoryItemMemo = Nothing
    Me.mn_datcons.NTSRepositoryItemText = Nothing
    Me.mn_datcons.OptionsColumn.AllowEdit = False
    Me.mn_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_datcons.OptionsColumn.ReadOnly = True
    Me.mn_datcons.OptionsFilter.AllowFilter = False
    Me.mn_datcons.Visible = True
    Me.mn_datcons.VisibleIndex = 8
    '
    'mn_quant
    '
    Me.mn_quant.AppearanceCell.Options.UseBackColor = True
    Me.mn_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mn_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_quant.Caption = "Q.tà ordin."
    Me.mn_quant.Enabled = False
    Me.mn_quant.FieldName = "mn_quant"
    Me.mn_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_quant.Name = "mn_quant"
    Me.mn_quant.NTSRepositoryComboBox = Nothing
    Me.mn_quant.NTSRepositoryItemCheck = Nothing
    Me.mn_quant.NTSRepositoryItemMemo = Nothing
    Me.mn_quant.NTSRepositoryItemText = Nothing
    Me.mn_quant.OptionsColumn.AllowEdit = False
    Me.mn_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_quant.OptionsColumn.ReadOnly = True
    Me.mn_quant.OptionsFilter.AllowFilter = False
    Me.mn_quant.Visible = True
    Me.mn_quant.VisibleIndex = 9
    '
    'mn_quaeva
    '
    Me.mn_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.mn_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mn_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_quaeva.Caption = "Q.tà evasa"
    Me.mn_quaeva.Enabled = False
    Me.mn_quaeva.FieldName = "mn_quaeva"
    Me.mn_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_quaeva.Name = "mn_quaeva"
    Me.mn_quaeva.NTSRepositoryComboBox = Nothing
    Me.mn_quaeva.NTSRepositoryItemCheck = Nothing
    Me.mn_quaeva.NTSRepositoryItemMemo = Nothing
    Me.mn_quaeva.NTSRepositoryItemText = Nothing
    Me.mn_quaeva.OptionsColumn.AllowEdit = False
    Me.mn_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_quaeva.OptionsColumn.ReadOnly = True
    Me.mn_quaeva.OptionsFilter.AllowFilter = False
    Me.mn_quaeva.Visible = True
    Me.mn_quaeva.VisibleIndex = 10
    '
    'mn_quapre
    '
    Me.mn_quapre.AppearanceCell.Options.UseBackColor = True
    Me.mn_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.mn_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_quapre.Caption = "Q.tà pren."
    Me.mn_quapre.Enabled = False
    Me.mn_quapre.FieldName = "mn_quapre"
    Me.mn_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_quapre.Name = "mn_quapre"
    Me.mn_quapre.NTSRepositoryComboBox = Nothing
    Me.mn_quapre.NTSRepositoryItemCheck = Nothing
    Me.mn_quapre.NTSRepositoryItemMemo = Nothing
    Me.mn_quapre.NTSRepositoryItemText = Nothing
    Me.mn_quapre.OptionsColumn.AllowEdit = False
    Me.mn_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_quapre.OptionsColumn.ReadOnly = True
    Me.mn_quapre.OptionsFilter.AllowFilter = False
    Me.mn_quapre.Visible = True
    Me.mn_quapre.VisibleIndex = 11
    '
    'mn_mmquant
    '
    Me.mn_mmquant.AppearanceCell.Options.UseBackColor = True
    Me.mn_mmquant.AppearanceCell.Options.UseTextOptions = True
    Me.mn_mmquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_mmquant.Caption = "Q.tà evadibile"
    Me.mn_mmquant.Enabled = True
    Me.mn_mmquant.FieldName = "mn_mmquant"
    Me.mn_mmquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_mmquant.Name = "mn_mmquant"
    Me.mn_mmquant.NTSRepositoryComboBox = Nothing
    Me.mn_mmquant.NTSRepositoryItemCheck = Nothing
    Me.mn_mmquant.NTSRepositoryItemMemo = Nothing
    Me.mn_mmquant.NTSRepositoryItemText = Nothing
    Me.mn_mmquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_mmquant.OptionsFilter.AllowFilter = False
    Me.mn_mmquant.Visible = True
    Me.mn_mmquant.VisibleIndex = 12
    '
    'mn_mmflevas
    '
    Me.mn_mmflevas.AppearanceCell.Options.UseBackColor = True
    Me.mn_mmflevas.AppearanceCell.Options.UseTextOptions = True
    Me.mn_mmflevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_mmflevas.Caption = "Evaso tot."
    Me.mn_mmflevas.Enabled = False
    Me.mn_mmflevas.FieldName = "mn_mmflevas"
    Me.mn_mmflevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_mmflevas.Name = "mn_mmflevas"
    Me.mn_mmflevas.NTSRepositoryComboBox = Nothing
    Me.mn_mmflevas.NTSRepositoryItemCheck = Nothing
    Me.mn_mmflevas.NTSRepositoryItemMemo = Nothing
    Me.mn_mmflevas.NTSRepositoryItemText = Nothing
    Me.mn_mmflevas.OptionsColumn.AllowEdit = False
    Me.mn_mmflevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_mmflevas.OptionsColumn.ReadOnly = True
    Me.mn_mmflevas.OptionsFilter.AllowFilter = False
    Me.mn_mmflevas.Visible = True
    Me.mn_mmflevas.VisibleIndex = 13
    '
    'mn_colli
    '
    Me.mn_colli.AppearanceCell.Options.UseBackColor = True
    Me.mn_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mn_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_colli.Caption = "Colli ord."
    Me.mn_colli.Enabled = False
    Me.mn_colli.FieldName = "mn_colli"
    Me.mn_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_colli.Name = "mn_colli"
    Me.mn_colli.NTSRepositoryComboBox = Nothing
    Me.mn_colli.NTSRepositoryItemCheck = Nothing
    Me.mn_colli.NTSRepositoryItemMemo = Nothing
    Me.mn_colli.NTSRepositoryItemText = Nothing
    Me.mn_colli.OptionsColumn.AllowEdit = False
    Me.mn_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_colli.OptionsColumn.ReadOnly = True
    Me.mn_colli.OptionsFilter.AllowFilter = False
    Me.mn_colli.Visible = True
    Me.mn_colli.VisibleIndex = 14
    '
    'mn_coleva
    '
    Me.mn_coleva.AppearanceCell.Options.UseBackColor = True
    Me.mn_coleva.AppearanceCell.Options.UseTextOptions = True
    Me.mn_coleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_coleva.Caption = "Colli evasi"
    Me.mn_coleva.Enabled = False
    Me.mn_coleva.FieldName = "mn_coleva"
    Me.mn_coleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_coleva.Name = "mn_coleva"
    Me.mn_coleva.NTSRepositoryComboBox = Nothing
    Me.mn_coleva.NTSRepositoryItemCheck = Nothing
    Me.mn_coleva.NTSRepositoryItemMemo = Nothing
    Me.mn_coleva.NTSRepositoryItemText = Nothing
    Me.mn_coleva.OptionsColumn.AllowEdit = False
    Me.mn_coleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_coleva.OptionsColumn.ReadOnly = True
    Me.mn_coleva.OptionsFilter.AllowFilter = False
    Me.mn_coleva.Visible = True
    Me.mn_coleva.VisibleIndex = 15
    '
    'mn_colpre
    '
    Me.mn_colpre.AppearanceCell.Options.UseBackColor = True
    Me.mn_colpre.AppearanceCell.Options.UseTextOptions = True
    Me.mn_colpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_colpre.Caption = "Colli prenot."
    Me.mn_colpre.Enabled = False
    Me.mn_colpre.FieldName = "mn_colpre"
    Me.mn_colpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_colpre.Name = "mn_colpre"
    Me.mn_colpre.NTSRepositoryComboBox = Nothing
    Me.mn_colpre.NTSRepositoryItemCheck = Nothing
    Me.mn_colpre.NTSRepositoryItemMemo = Nothing
    Me.mn_colpre.NTSRepositoryItemText = Nothing
    Me.mn_colpre.OptionsColumn.AllowEdit = False
    Me.mn_colpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_colpre.OptionsColumn.ReadOnly = True
    Me.mn_colpre.OptionsFilter.AllowFilter = False
    Me.mn_colpre.Visible = True
    Me.mn_colpre.VisibleIndex = 16
    '
    'mn_mmcolli
    '
    Me.mn_mmcolli.AppearanceCell.Options.UseBackColor = True
    Me.mn_mmcolli.AppearanceCell.Options.UseTextOptions = True
    Me.mn_mmcolli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_mmcolli.Caption = "Colli evadibili"
    Me.mn_mmcolli.Enabled = True
    Me.mn_mmcolli.FieldName = "mn_mmcolli"
    Me.mn_mmcolli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_mmcolli.Name = "mn_mmcolli"
    Me.mn_mmcolli.NTSRepositoryComboBox = Nothing
    Me.mn_mmcolli.NTSRepositoryItemCheck = Nothing
    Me.mn_mmcolli.NTSRepositoryItemMemo = Nothing
    Me.mn_mmcolli.NTSRepositoryItemText = Nothing
    Me.mn_mmcolli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_mmcolli.OptionsFilter.AllowFilter = False
    Me.mn_mmcolli.Visible = True
    Me.mn_mmcolli.VisibleIndex = 17
    '
    'mn_commeca
    '
    Me.mn_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mn_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mn_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_commeca.Caption = "Commessa"
    Me.mn_commeca.Enabled = False
    Me.mn_commeca.FieldName = "mn_commeca"
    Me.mn_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_commeca.Name = "mn_commeca"
    Me.mn_commeca.NTSRepositoryComboBox = Nothing
    Me.mn_commeca.NTSRepositoryItemCheck = Nothing
    Me.mn_commeca.NTSRepositoryItemMemo = Nothing
    Me.mn_commeca.NTSRepositoryItemText = Nothing
    Me.mn_commeca.OptionsColumn.AllowEdit = False
    Me.mn_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_commeca.OptionsColumn.ReadOnly = True
    Me.mn_commeca.OptionsFilter.AllowFilter = False
    Me.mn_commeca.Visible = True
    Me.mn_commeca.VisibleIndex = 18
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = False
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 19
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = False
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowEdit = False
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsColumn.ReadOnly = True
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 20
    '
    'xx_lotto
    '
    Me.xx_lotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lotto.Caption = "Descr. lotto"
    Me.xx_lotto.Enabled = False
    Me.xx_lotto.FieldName = "xx_lotto"
    Me.xx_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lotto.Name = "xx_lotto"
    Me.xx_lotto.NTSRepositoryComboBox = Nothing
    Me.xx_lotto.NTSRepositoryItemCheck = Nothing
    Me.xx_lotto.NTSRepositoryItemMemo = Nothing
    Me.xx_lotto.NTSRepositoryItemText = Nothing
    Me.xx_lotto.OptionsColumn.AllowEdit = False
    Me.xx_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lotto.OptionsColumn.ReadOnly = True
    Me.xx_lotto.OptionsFilter.AllowFilter = False
    Me.xx_lotto.Visible = True
    Me.xx_lotto.VisibleIndex = 21
    '
    'mn_rigaa
    '
    Me.mn_rigaa.AppearanceCell.Options.UseBackColor = True
    Me.mn_rigaa.AppearanceCell.Options.UseTextOptions = True
    Me.mn_rigaa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_rigaa.Caption = "Progr."
    Me.mn_rigaa.Enabled = False
    Me.mn_rigaa.FieldName = "mn_rigaa"
    Me.mn_rigaa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_rigaa.Name = "mn_rigaa"
    Me.mn_rigaa.NTSRepositoryComboBox = Nothing
    Me.mn_rigaa.NTSRepositoryItemCheck = Nothing
    Me.mn_rigaa.NTSRepositoryItemMemo = Nothing
    Me.mn_rigaa.NTSRepositoryItemText = Nothing
    Me.mn_rigaa.OptionsColumn.AllowEdit = False
    Me.mn_rigaa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_rigaa.OptionsColumn.ReadOnly = True
    Me.mn_rigaa.OptionsFilter.AllowFilter = False
    Me.mn_rigaa.Visible = True
    Me.mn_rigaa.VisibleIndex = 22
    '
    'mn_tcindtagl
    '
    Me.mn_tcindtagl.AppearanceCell.Options.UseBackColor = True
    Me.mn_tcindtagl.AppearanceCell.Options.UseTextOptions = True
    Me.mn_tcindtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_tcindtagl.Caption = "Taglia"
    Me.mn_tcindtagl.Enabled = False
    Me.mn_tcindtagl.FieldName = "mn_tcindtagl"
    Me.mn_tcindtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_tcindtagl.Name = "mn_tcindtagl"
    Me.mn_tcindtagl.NTSRepositoryComboBox = Nothing
    Me.mn_tcindtagl.NTSRepositoryItemCheck = Nothing
    Me.mn_tcindtagl.NTSRepositoryItemMemo = Nothing
    Me.mn_tcindtagl.NTSRepositoryItemText = Nothing
    Me.mn_tcindtagl.OptionsColumn.AllowEdit = False
    Me.mn_tcindtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_tcindtagl.OptionsColumn.ReadOnly = True
    Me.mn_tcindtagl.OptionsFilter.AllowFilter = False
    Me.mn_tcindtagl.Visible = True
    Me.mn_tcindtagl.VisibleIndex = 23
    '
    'mn_tctaglia
    '
    Me.mn_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.mn_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.mn_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mn_tctaglia.Caption = "Descr. taglia"
    Me.mn_tctaglia.Enabled = False
    Me.mn_tctaglia.FieldName = "mn_tctaglia"
    Me.mn_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mn_tctaglia.Name = "mn_tctaglia"
    Me.mn_tctaglia.NTSRepositoryComboBox = Nothing
    Me.mn_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.mn_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.mn_tctaglia.NTSRepositoryItemText = Nothing
    Me.mn_tctaglia.OptionsColumn.AllowEdit = False
    Me.mn_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mn_tctaglia.OptionsColumn.ReadOnly = True
    Me.mn_tctaglia.OptionsFilter.AllowFilter = False
    Me.mn_tctaglia.Visible = True
    Me.mn_tctaglia.VisibleIndex = 24
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'edImpegno
    '
    Me.edImpegno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edImpegno.Location = New System.Drawing.Point(89, 12)
    Me.edImpegno.Name = "edImpegno"
    Me.edImpegno.NTSDbField = ""
    Me.edImpegno.NTSForzaVisZoom = False
    Me.edImpegno.NTSOldValue = ""
    Me.edImpegno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edImpegno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edImpegno.Properties.AutoHeight = False
    Me.edImpegno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edImpegno.Properties.MaxLength = 65536
    Me.edImpegno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edImpegno.Size = New System.Drawing.Size(259, 20)
    Me.edImpegno.TabIndex = 4
    '
    'edCliente
    '
    Me.edCliente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCliente.Location = New System.Drawing.Point(89, 38)
    Me.edCliente.Name = "edCliente"
    Me.edCliente.NTSDbField = ""
    Me.edCliente.NTSForzaVisZoom = False
    Me.edCliente.NTSOldValue = ""
    Me.edCliente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCliente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCliente.Properties.AutoHeight = False
    Me.edCliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCliente.Properties.MaxLength = 65536
    Me.edCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCliente.Size = New System.Drawing.Size(398, 20)
    Me.edCliente.TabIndex = 5
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Arrow
    Me.edCommeca.Location = New System.Drawing.Point(89, 116)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.AutoHeight = False
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(78, 20)
    Me.edCommeca.TabIndex = 6
    '
    'edLotto
    '
    Me.edLotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLotto.Location = New System.Drawing.Point(519, 142)
    Me.edLotto.Name = "edLotto"
    Me.edLotto.NTSDbField = ""
    Me.edLotto.NTSFormat = "0"
    Me.edLotto.NTSForzaVisZoom = False
    Me.edLotto.NTSOldValue = ""
    Me.edLotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edLotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLotto.Properties.AutoHeight = False
    Me.edLotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLotto.Properties.MaxLength = 65536
    Me.edLotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLotto.Size = New System.Drawing.Size(28, 20)
    Me.edLotto.TabIndex = 7
    Me.edLotto.Visible = False
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(89, 168)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(78, 20)
    Me.edFase.TabIndex = 8
    '
    'edTaglia
    '
    Me.edTaglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTaglia.Location = New System.Drawing.Point(89, 194)
    Me.edTaglia.Name = "edTaglia"
    Me.edTaglia.NTSDbField = ""
    Me.edTaglia.NTSFormat = "0"
    Me.edTaglia.NTSForzaVisZoom = False
    Me.edTaglia.NTSOldValue = ""
    Me.edTaglia.Properties.Appearance.Options.UseTextOptions = True
    Me.edTaglia.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTaglia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTaglia.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTaglia.Properties.AutoHeight = False
    Me.edTaglia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTaglia.Properties.MaxLength = 65536
    Me.edTaglia.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTaglia.Size = New System.Drawing.Size(78, 20)
    Me.edTaglia.TabIndex = 9
    '
    'lbImpegno
    '
    Me.lbImpegno.AutoSize = True
    Me.lbImpegno.BackColor = System.Drawing.Color.Transparent
    Me.lbImpegno.Location = New System.Drawing.Point(11, 15)
    Me.lbImpegno.Name = "lbImpegno"
    Me.lbImpegno.NTSDbField = ""
    Me.lbImpegno.Size = New System.Drawing.Size(63, 13)
    Me.lbImpegno.TabIndex = 10
    Me.lbImpegno.Text = "Impegno n°"
    Me.lbImpegno.Tooltip = ""
    Me.lbImpegno.UseMnemonic = False
    '
    'lbCliente
    '
    Me.lbCliente.AutoSize = True
    Me.lbCliente.BackColor = System.Drawing.Color.Transparent
    Me.lbCliente.Location = New System.Drawing.Point(11, 41)
    Me.lbCliente.Name = "lbCliente"
    Me.lbCliente.NTSDbField = ""
    Me.lbCliente.Size = New System.Drawing.Size(40, 13)
    Me.lbCliente.TabIndex = 11
    Me.lbCliente.Text = "Cliente"
    Me.lbCliente.Tooltip = ""
    Me.lbCliente.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.AutoSize = True
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.Location = New System.Drawing.Point(11, 119)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(58, 13)
    Me.lbCommeca.TabIndex = 12
    Me.lbCommeca.Text = "Commessa"
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'lbLotto
    '
    Me.lbLotto.AutoSize = True
    Me.lbLotto.BackColor = System.Drawing.Color.Transparent
    Me.lbLotto.Location = New System.Drawing.Point(11, 145)
    Me.lbLotto.Name = "lbLotto"
    Me.lbLotto.NTSDbField = ""
    Me.lbLotto.Size = New System.Drawing.Size(32, 13)
    Me.lbLotto.TabIndex = 13
    Me.lbLotto.Text = "Lotto"
    Me.lbLotto.Tooltip = ""
    Me.lbLotto.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(11, 171)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 14
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbTaglia
    '
    Me.lbTaglia.AutoSize = True
    Me.lbTaglia.BackColor = System.Drawing.Color.Transparent
    Me.lbTaglia.Location = New System.Drawing.Point(11, 197)
    Me.lbTaglia.Name = "lbTaglia"
    Me.lbTaglia.NTSDbField = ""
    Me.lbTaglia.Size = New System.Drawing.Size(35, 13)
    Me.lbTaglia.TabIndex = 15
    Me.lbTaglia.Text = "Taglia"
    Me.lbTaglia.Tooltip = ""
    Me.lbTaglia.UseMnemonic = False
    '
    'lbXx_Commeca
    '
    Me.lbXx_Commeca.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Commeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Commeca.Location = New System.Drawing.Point(173, 116)
    Me.lbXx_Commeca.Name = "lbXx_Commeca"
    Me.lbXx_Commeca.NTSDbField = ""
    Me.lbXx_Commeca.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_Commeca.TabIndex = 665
    Me.lbXx_Commeca.Tooltip = ""
    Me.lbXx_Commeca.UseMnemonic = False
    '
    'lbXx_Lotto
    '
    Me.lbXx_Lotto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Lotto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Lotto.Location = New System.Drawing.Point(173, 142)
    Me.lbXx_Lotto.Name = "lbXx_Lotto"
    Me.lbXx_Lotto.NTSDbField = ""
    Me.lbXx_Lotto.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_Lotto.TabIndex = 666
    Me.lbXx_Lotto.Tooltip = ""
    Me.lbXx_Lotto.UseMnemonic = False
    '
    'lbXx_Fase
    '
    Me.lbXx_Fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Fase.Location = New System.Drawing.Point(173, 168)
    Me.lbXx_Fase.Name = "lbXx_Fase"
    Me.lbXx_Fase.NTSDbField = ""
    Me.lbXx_Fase.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_Fase.TabIndex = 667
    Me.lbXx_Fase.Tooltip = ""
    Me.lbXx_Fase.UseMnemonic = False
    '
    'lbXx_Taglia
    '
    Me.lbXx_Taglia.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Taglia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Taglia.Location = New System.Drawing.Point(173, 194)
    Me.lbXx_Taglia.Name = "lbXx_Taglia"
    Me.lbXx_Taglia.NTSDbField = ""
    Me.lbXx_Taglia.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_Taglia.TabIndex = 668
    Me.lbXx_Taglia.Tooltip = ""
    Me.lbXx_Taglia.UseMnemonic = False
    '
    'edQtaass
    '
    Me.edQtaass.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQtaass.Location = New System.Drawing.Point(604, 38)
    Me.edQtaass.Name = "edQtaass"
    Me.edQtaass.NTSDbField = ""
    Me.edQtaass.NTSFormat = "0"
    Me.edQtaass.NTSForzaVisZoom = False
    Me.edQtaass.NTSOldValue = ""
    Me.edQtaass.Properties.Appearance.Options.UseTextOptions = True
    Me.edQtaass.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQtaass.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQtaass.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQtaass.Properties.AutoHeight = False
    Me.edQtaass.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQtaass.Properties.MaxLength = 65536
    Me.edQtaass.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQtaass.Size = New System.Drawing.Size(104, 20)
    Me.edQtaass.TabIndex = 670
    '
    'edDisp
    '
    Me.edDisp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDisp.Location = New System.Drawing.Point(604, 12)
    Me.edDisp.Name = "edDisp"
    Me.edDisp.NTSDbField = ""
    Me.edDisp.NTSFormat = "0"
    Me.edDisp.NTSForzaVisZoom = False
    Me.edDisp.NTSOldValue = ""
    Me.edDisp.Properties.Appearance.Options.UseTextOptions = True
    Me.edDisp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDisp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDisp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDisp.Properties.AutoHeight = False
    Me.edDisp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDisp.Properties.MaxLength = 65536
    Me.edDisp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDisp.Size = New System.Drawing.Size(104, 20)
    Me.edDisp.TabIndex = 669
    '
    'lbDisp
    '
    Me.lbDisp.AutoSize = True
    Me.lbDisp.BackColor = System.Drawing.Color.Transparent
    Me.lbDisp.Location = New System.Drawing.Point(493, 15)
    Me.lbDisp.Name = "lbDisp"
    Me.lbDisp.NTSDbField = ""
    Me.lbDisp.Size = New System.Drawing.Size(105, 13)
    Me.lbDisp.TabIndex = 671
    Me.lbDisp.Text = "Quantità disponibile:"
    Me.lbDisp.Tooltip = ""
    Me.lbDisp.UseMnemonic = False
    '
    'lbQtaass
    '
    Me.lbQtaass.AutoSize = True
    Me.lbQtaass.BackColor = System.Drawing.Color.Transparent
    Me.lbQtaass.Location = New System.Drawing.Point(493, 41)
    Me.lbQtaass.Name = "lbQtaass"
    Me.lbQtaass.NTSDbField = ""
    Me.lbQtaass.Size = New System.Drawing.Size(106, 13)
    Me.lbQtaass.TabIndex = 672
    Me.lbQtaass.Text = "Quantità assegnata:"
    Me.lbQtaass.Tooltip = ""
    Me.lbQtaass.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edLottox)
    Me.pnTop.Controls.Add(Me.edCoddest2)
    Me.pnTop.Controls.Add(Me.lbDdprov)
    Me.pnTop.Controls.Add(Me.edDdprov)
    Me.pnTop.Controls.Add(Me.lbDdcitta)
    Me.pnTop.Controls.Add(Me.edDdcitta)
    Me.pnTop.Controls.Add(Me.lbDdindir)
    Me.pnTop.Controls.Add(Me.edDdindir)
    Me.pnTop.Controls.Add(Me.lbAnprov)
    Me.pnTop.Controls.Add(Me.edAnprov)
    Me.pnTop.Controls.Add(Me.lbAncitta)
    Me.pnTop.Controls.Add(Me.edAncitta)
    Me.pnTop.Controls.Add(Me.lbAnindir)
    Me.pnTop.Controls.Add(Me.edAnindir)
    Me.pnTop.Controls.Add(Me.edAndescr2)
    Me.pnTop.Controls.Add(Me.lbAndescr1)
    Me.pnTop.Controls.Add(Me.edAndescr1)
    Me.pnTop.Controls.Add(Me.lbCoddest)
    Me.pnTop.Controls.Add(Me.edCoddest)
    Me.pnTop.Controls.Add(Me.edImpegno)
    Me.pnTop.Controls.Add(Me.lbQtaass)
    Me.pnTop.Controls.Add(Me.edCliente)
    Me.pnTop.Controls.Add(Me.lbDisp)
    Me.pnTop.Controls.Add(Me.edCommeca)
    Me.pnTop.Controls.Add(Me.edQtaass)
    Me.pnTop.Controls.Add(Me.edLotto)
    Me.pnTop.Controls.Add(Me.edDisp)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.lbXx_Taglia)
    Me.pnTop.Controls.Add(Me.edTaglia)
    Me.pnTop.Controls.Add(Me.lbXx_Fase)
    Me.pnTop.Controls.Add(Me.lbImpegno)
    Me.pnTop.Controls.Add(Me.lbXx_Lotto)
    Me.pnTop.Controls.Add(Me.lbCliente)
    Me.pnTop.Controls.Add(Me.lbXx_Commeca)
    Me.pnTop.Controls.Add(Me.lbCommeca)
    Me.pnTop.Controls.Add(Me.lbTaglia)
    Me.pnTop.Controls.Add(Me.lbLotto)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(720, 271)
    Me.pnTop.TabIndex = 673
    Me.pnTop.Text = "NtsPanel1"
    '
    'edLottox
    '
    Me.edLottox.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLottox.Location = New System.Drawing.Point(89, 142)
    Me.edLottox.Name = "edLottox"
    Me.edLottox.NTSDbField = ""
    Me.edLottox.NTSForzaVisZoom = False
    Me.edLottox.NTSOldValue = ""
    Me.edLottox.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLottox.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLottox.Properties.AutoHeight = False
    Me.edLottox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLottox.Properties.MaxLength = 65536
    Me.edLottox.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLottox.Size = New System.Drawing.Size(78, 20)
    Me.edLottox.TabIndex = 691
    '
    'edCoddest2
    '
    Me.edCoddest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest2.Location = New System.Drawing.Point(519, 220)
    Me.edCoddest2.Name = "edCoddest2"
    Me.edCoddest2.NTSDbField = ""
    Me.edCoddest2.NTSForzaVisZoom = False
    Me.edCoddest2.NTSOldValue = ""
    Me.edCoddest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest2.Properties.AutoHeight = False
    Me.edCoddest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest2.Properties.MaxLength = 65536
    Me.edCoddest2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest2.Size = New System.Drawing.Size(189, 20)
    Me.edCoddest2.TabIndex = 690
    '
    'lbDdprov
    '
    Me.lbDdprov.AutoSize = True
    Me.lbDdprov.BackColor = System.Drawing.Color.Transparent
    Me.lbDdprov.Location = New System.Drawing.Point(605, 248)
    Me.lbDdprov.Name = "lbDdprov"
    Me.lbDdprov.NTSDbField = ""
    Me.lbDdprov.Size = New System.Drawing.Size(54, 13)
    Me.lbDdprov.TabIndex = 689
    Me.lbDdprov.Text = "Dest prov"
    Me.lbDdprov.Tooltip = ""
    Me.lbDdprov.UseMnemonic = False
    '
    'edDdprov
    '
    Me.edDdprov.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDdprov.Location = New System.Drawing.Point(659, 245)
    Me.edDdprov.Name = "edDdprov"
    Me.edDdprov.NTSDbField = ""
    Me.edDdprov.NTSForzaVisZoom = False
    Me.edDdprov.NTSOldValue = ""
    Me.edDdprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDdprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDdprov.Properties.AutoHeight = False
    Me.edDdprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDdprov.Properties.MaxLength = 65536
    Me.edDdprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDdprov.Size = New System.Drawing.Size(49, 20)
    Me.edDdprov.TabIndex = 688
    '
    'lbDdcitta
    '
    Me.lbDdcitta.AutoSize = True
    Me.lbDdcitta.BackColor = System.Drawing.Color.Transparent
    Me.lbDdcitta.Location = New System.Drawing.Point(399, 248)
    Me.lbDdcitta.Name = "lbDdcitta"
    Me.lbDdcitta.NTSDbField = ""
    Me.lbDdcitta.Size = New System.Drawing.Size(53, 13)
    Me.lbDdcitta.TabIndex = 687
    Me.lbDdcitta.Text = "Dest città"
    Me.lbDdcitta.Tooltip = ""
    Me.lbDdcitta.UseMnemonic = False
    '
    'edDdcitta
    '
    Me.edDdcitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDdcitta.Location = New System.Drawing.Point(455, 245)
    Me.edDdcitta.Name = "edDdcitta"
    Me.edDdcitta.NTSDbField = ""
    Me.edDdcitta.NTSForzaVisZoom = False
    Me.edDdcitta.NTSOldValue = ""
    Me.edDdcitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDdcitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDdcitta.Properties.AutoHeight = False
    Me.edDdcitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDdcitta.Properties.MaxLength = 65536
    Me.edDdcitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDdcitta.Size = New System.Drawing.Size(144, 20)
    Me.edDdcitta.TabIndex = 686
    '
    'lbDdindir
    '
    Me.lbDdindir.AutoSize = True
    Me.lbDdindir.BackColor = System.Drawing.Color.Transparent
    Me.lbDdindir.Location = New System.Drawing.Point(11, 248)
    Me.lbDdindir.Name = "lbDdindir"
    Me.lbDdindir.NTSDbField = ""
    Me.lbDdindir.Size = New System.Drawing.Size(59, 13)
    Me.lbDdindir.TabIndex = 685
    Me.lbDdindir.Text = "Dest indiriz"
    Me.lbDdindir.Tooltip = ""
    Me.lbDdindir.UseMnemonic = False
    '
    'edDdindir
    '
    Me.edDdindir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDdindir.Location = New System.Drawing.Point(89, 245)
    Me.edDdindir.Name = "edDdindir"
    Me.edDdindir.NTSDbField = ""
    Me.edDdindir.NTSForzaVisZoom = False
    Me.edDdindir.NTSOldValue = ""
    Me.edDdindir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDdindir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDdindir.Properties.AutoHeight = False
    Me.edDdindir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDdindir.Properties.MaxLength = 65536
    Me.edDdindir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDdindir.Size = New System.Drawing.Size(307, 20)
    Me.edDdindir.TabIndex = 684
    '
    'lbAnprov
    '
    Me.lbAnprov.AutoSize = True
    Me.lbAnprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAnprov.Location = New System.Drawing.Point(605, 93)
    Me.lbAnprov.Name = "lbAnprov"
    Me.lbAnprov.NTSDbField = ""
    Me.lbAnprov.Size = New System.Drawing.Size(29, 13)
    Me.lbAnprov.TabIndex = 683
    Me.lbAnprov.Text = "Prov"
    Me.lbAnprov.Tooltip = ""
    Me.lbAnprov.UseMnemonic = False
    '
    'edAnprov
    '
    Me.edAnprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnprov.Location = New System.Drawing.Point(659, 90)
    Me.edAnprov.Name = "edAnprov"
    Me.edAnprov.NTSDbField = ""
    Me.edAnprov.NTSForzaVisZoom = False
    Me.edAnprov.NTSOldValue = ""
    Me.edAnprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnprov.Properties.AutoHeight = False
    Me.edAnprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnprov.Properties.MaxLength = 65536
    Me.edAnprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnprov.Size = New System.Drawing.Size(49, 20)
    Me.edAnprov.TabIndex = 682
    '
    'lbAncitta
    '
    Me.lbAncitta.AutoSize = True
    Me.lbAncitta.BackColor = System.Drawing.Color.Transparent
    Me.lbAncitta.Location = New System.Drawing.Point(399, 93)
    Me.lbAncitta.Name = "lbAncitta"
    Me.lbAncitta.NTSDbField = ""
    Me.lbAncitta.Size = New System.Drawing.Size(30, 13)
    Me.lbAncitta.TabIndex = 681
    Me.lbAncitta.Text = "Città"
    Me.lbAncitta.Tooltip = ""
    Me.lbAncitta.UseMnemonic = False
    '
    'edAncitta
    '
    Me.edAncitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAncitta.Location = New System.Drawing.Point(455, 90)
    Me.edAncitta.Name = "edAncitta"
    Me.edAncitta.NTSDbField = ""
    Me.edAncitta.NTSForzaVisZoom = False
    Me.edAncitta.NTSOldValue = ""
    Me.edAncitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAncitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAncitta.Properties.AutoHeight = False
    Me.edAncitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAncitta.Properties.MaxLength = 65536
    Me.edAncitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAncitta.Size = New System.Drawing.Size(144, 20)
    Me.edAncitta.TabIndex = 680
    '
    'lbAnindir
    '
    Me.lbAnindir.AutoSize = True
    Me.lbAnindir.BackColor = System.Drawing.Color.Transparent
    Me.lbAnindir.Location = New System.Drawing.Point(11, 93)
    Me.lbAnindir.Name = "lbAnindir"
    Me.lbAnindir.NTSDbField = ""
    Me.lbAnindir.Size = New System.Drawing.Size(47, 13)
    Me.lbAnindir.TabIndex = 679
    Me.lbAnindir.Text = "Indirizzo"
    Me.lbAnindir.Tooltip = ""
    Me.lbAnindir.UseMnemonic = False
    '
    'edAnindir
    '
    Me.edAnindir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnindir.Location = New System.Drawing.Point(89, 90)
    Me.edAnindir.Name = "edAnindir"
    Me.edAnindir.NTSDbField = ""
    Me.edAnindir.NTSForzaVisZoom = False
    Me.edAnindir.NTSOldValue = ""
    Me.edAnindir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnindir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnindir.Properties.AutoHeight = False
    Me.edAnindir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnindir.Properties.MaxLength = 65536
    Me.edAnindir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnindir.Size = New System.Drawing.Size(307, 20)
    Me.edAnindir.TabIndex = 678
    '
    'edAndescr2
    '
    Me.edAndescr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAndescr2.Location = New System.Drawing.Point(402, 64)
    Me.edAndescr2.Name = "edAndescr2"
    Me.edAndescr2.NTSDbField = ""
    Me.edAndescr2.NTSForzaVisZoom = False
    Me.edAndescr2.NTSOldValue = ""
    Me.edAndescr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAndescr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAndescr2.Properties.AutoHeight = False
    Me.edAndescr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAndescr2.Properties.MaxLength = 65536
    Me.edAndescr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAndescr2.Size = New System.Drawing.Size(306, 20)
    Me.edAndescr2.TabIndex = 677
    '
    'lbAndescr1
    '
    Me.lbAndescr1.AutoSize = True
    Me.lbAndescr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAndescr1.Location = New System.Drawing.Point(11, 67)
    Me.lbAndescr1.Name = "lbAndescr1"
    Me.lbAndescr1.NTSDbField = ""
    Me.lbAndescr1.Size = New System.Drawing.Size(61, 13)
    Me.lbAndescr1.TabIndex = 676
    Me.lbAndescr1.Text = "Descrizione"
    Me.lbAndescr1.Tooltip = ""
    Me.lbAndescr1.UseMnemonic = False
    '
    'edAndescr1
    '
    Me.edAndescr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAndescr1.Location = New System.Drawing.Point(89, 64)
    Me.edAndescr1.Name = "edAndescr1"
    Me.edAndescr1.NTSDbField = ""
    Me.edAndescr1.NTSForzaVisZoom = False
    Me.edAndescr1.NTSOldValue = ""
    Me.edAndescr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAndescr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAndescr1.Properties.AutoHeight = False
    Me.edAndescr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAndescr1.Properties.MaxLength = 65536
    Me.edAndescr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAndescr1.Size = New System.Drawing.Size(307, 20)
    Me.edAndescr1.TabIndex = 675
    '
    'lbCoddest
    '
    Me.lbCoddest.AutoSize = True
    Me.lbCoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddest.Location = New System.Drawing.Point(11, 223)
    Me.lbCoddest.Name = "lbCoddest"
    Me.lbCoddest.NTSDbField = ""
    Me.lbCoddest.Size = New System.Drawing.Size(52, 13)
    Me.lbCoddest.TabIndex = 674
    Me.lbCoddest.Text = "Destinaz."
    Me.lbCoddest.Tooltip = ""
    Me.lbCoddest.UseMnemonic = False
    '
    'edCoddest
    '
    Me.edCoddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest.Location = New System.Drawing.Point(89, 220)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = ""
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.AutoHeight = False
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.Properties.MaxLength = 65536
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(424, 20)
    Me.edCoddest.TabIndex = 673
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.grGndt)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(0, 301)
    Me.pnFill.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnFill.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnFill.Name = "pnFill"
    Me.pnFill.NTSActiveTrasparency = True
    Me.pnFill.Size = New System.Drawing.Size(720, 178)
    Me.pnFill.TabIndex = 674
    Me.pnFill.Text = "NtsPanel1"
    '
    'FRMORGNDT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(720, 479)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMORGNDT"
    Me.NTSLastControlFocussed = Me.grGndt
    Me.Text = "DETTAGLIO RIGHE IMPEGNI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGndt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGndt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edImpegno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTaglia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQtaass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDisp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edLottox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDdprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDdcitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDdindir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAncitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnindir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAndescr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAndescr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGndt.NTSSetParam(oMenu, oApp.Tr(Me, 128726040765058063, "Dettaglio righe Impegni"))
      mn_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259949082969, "Riga"), "0", 9, 0, 999999999)
      mn_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259949239221, "Cliente"), tabanagrac)
      mn_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726794029677982, "Descrizione"), 50, True)
      mn_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128726259949551725, "Codice art."), tabartico, True)
      mn_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726794030015488, "Descr. articolo"), 40, True)
      mn_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259949864229, "Num. ord."), "0", 9, 0, 999999999)
      mn_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726259950020481, "Serie"), CLN__STD.SerieMaxLen, False)
      mn_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128726259950176733, "Data ord."), True)
      mn_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128726259950332985, "Data cons."), False)
      mn_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259950489237, "Q.tà ordin."), "#,##0.00", 15)
      mn_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259950645489, "Q.tà evasa"), "#,##0.00", 15)
      mn_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259950801741, "Q.tà pren."), "#,##0.00", 15)
      mn_mmquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259950957993, "Q.tà evadibile"), "#,##0.00", 15, -999999999, 999999999)
      mn_mmflevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128726259951114245, "Evaso tot."), "S", "C")
      mn_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259951270497, "Colli ord."), "#,##0.00", 15)
      mn_coleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259951426749, "Colli evasi"), "#,##0.00", 15)
      mn_colpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259951583001, "Colli prenot."), "#,##0.00", 15)
      mn_mmcolli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259951739253, "Colli evadibili"), "#,##0.00", 15, -999999999, 999999999)
      mn_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259951895505, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726828264293084, "Descr. commessa"), 0, True)
      xx_lottox.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259952208009, "Lotto"), "0", 9, 0, 999999999)
      xx_lotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726828264606328, "Descr. lotto"), 0, True)
      mn_rigaa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259952520513, "Progr."), "0", 9, 0, 999999999)
      mn_tcindtagl.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259952676765, "Taglia"), tabtagl)
      mn_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726259952833017, "Descr. taglia"), 5, False)
      edQtaass.NTSSetParam(oMenu, oApp.Tr(Me, 128726259954239285, "Quantità assegnata:"), oApp.FormatQta)
      edDisp.NTSSetParam(oMenu, oApp.Tr(Me, 128726259954395537, "Quantità disponibile:"), oApp.FormatQta)
      edTaglia.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726259956114309, "Taglia"), tabtagl)
      edFase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726259956270561, "Fase"), tabartfasi)
      edLotto.NTSSetParam(oMenu, oApp.Tr(Me, 128726259956426813, "Lotto"), "0", 9, 0, 999999999)
      edLottox.NTSSetParam(oMenu, oApp.Tr(Me, 129519289933580315, "Lotto"), 50, True)
      edCommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726259956583065, "Commessa"), tabcommess)
      edCliente.NTSSetParam(oMenu, oApp.Tr(Me, 128726259956739317, "Cliente"), 0)
      edImpegno.NTSSetParam(oMenu, oApp.Tr(Me, 128726259956895569, "Impegno n°"), 0)

      mn_mmquant.NTSSetRichiesto()
      mn_mmcolli.NTSSetRichiesto()

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
  Public Overridable Sub InitEntity(ByRef cleGnnp As CLEORGNNP)
    oCleGnnp = cleGnnp
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Dim dsTmp As DataSet = Nothing
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      dRes = grvGndt.NTSSalvaRigaCorrente(dcGndt, oCleGnnp.GndtRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          '----------------------------------------------------------------------------------------------------------
          If GctlControllaOutNotEqual() = False Then Return False
          '----------------------------------------------------------------------------------------------------------
          dttTmp = dsGndt.Tables("TTMOPERNP").Copy
          '----------------------------------------------------------------------------------------------------------
          If Not oCleGnnp.GndtSalva(False, edCommeca.Text, edLotto.Text, edFase.Text, edTaglia.Text) Then Return False
          '----------------------------------------------------------------------------------------------------------
          If oCleGnnp.AggiornaQtaLottoSuTTMOPERNP(dttTmp.Rows(dcGndt.Position), dsGndt) = True Then
            oCleGnnp.GndtApri(DittaCorrente, dsGndt)
            dcGndt.DataSource = dsGndt.Tables("TTMOPERNP")
            grGndt.DataSource = dcGndt
            grvGndt.NTSAllowInsert = False
            grvGndt.NTSAllowDelete = False
          End If
          '----------------------------------------------------------------------------------------------------------
          oCleGnnp.GetSumTTMopernp(NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(dcGndt.Position)!mn_codart), _
                                   edCommeca.Text, edLotto.Text, edFase.Text, edTaglia.Text, dsTmp)
          If dsTmp.Tables("TTMOPERNP").Rows.Count = 0 Then
            edQtaass.Text = "0"
            oCleGnnp.dGndtQtaass = 0
          Else
            If dsTmp.Tables("TTMOPERNP").Rows(0)!expr1.Equals(DBNull.Value) Then
              edQtaass.Text = "0"
              oCleGnnp.dGndtQtaass = 0
            Else
              edQtaass.Text = NTSCStr(dsTmp.Tables("TTMOPERNP").Rows(0)!expr1)
              oCleGnnp.dGndtQtaass = NTSCDec(dsTmp.Tables("TTMOPERNP").Rows(0)!expr1)
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
        Case System.Windows.Forms.DialogResult.No : oCleGnnp.GndtRipristina(dcGndt.Position, dcGndt.Filter)
        Case System.Windows.Forms.DialogResult.Cancel : Return False
        Case System.Windows.Forms.DialogResult.Abort
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMORGNDT_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------
      If bClose Then
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMORGNDT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""
    Dim dtrTmp As DataRow = Nothing
    Dim dttTmp As New DataTable
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edTaglia.Enabled = False
      edFase.Enabled = False
      edLotto.Enabled = False
      edLottox.Enabled = False
      edCommeca.Enabled = False
      edCliente.Enabled = False
      edImpegno.Enabled = False
      edQtaass.Enabled = False
      edDisp.Enabled = False
      edCoddest.Enabled = False
      edAndescr1.Enabled = False
      edAndescr2.Enabled = False
      edAnindir.Enabled = False
      edAncitta.Enabled = False
      edAnprov.Enabled = False
      edCoddest2.Enabled = False
      edDdindir.Enabled = False
      edDdcitta.Enabled = False
      edDdprov.Enabled = False

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGnnp.GndtApri(DittaCorrente, dsGndt) Then
        Me.Close()
        Return
      End If

      If oCleGnnp.strGndtCodart = "" Then
        'Chiamato da BSORGNNP
        '--- Se non esistono quantità assegnate esce
        If dsGndt.Tables("TTMOPERNP").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128726145607513330, "Nessuna quantità assegnata."))
          bClose = True
          Return
        End If
        '-------------------------------------------
        lbImpegno.Text = oApp.Tr(Me, 130421126684742902, "Impegno n°")
        If Trim(oCleGnnp.strGndtSerie) = "" Then
          edImpegno.Text = NTSCStr(oCleGnnp.lGndtNumdoc)
        Else
          edImpegno.Text = NTSCStr(oCleGnnp.lGndtNumdoc) & "/" & NTSCStr(oCleGnnp.strGndtSerie)
        End If
        edImpegno.Text = edImpegno.Text & " del " & NTSCDate(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_datord).ToShortDateString
        lbCliente.Text = oApp.Tr(Me, 130421126327086652, "Cliente:")
        edCliente.Text = NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_conto) & " - " & NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_descr1)

        'riempio i campi relativi ai clienti
        If NTSCInt(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_conto) <> 0 Then
          edCoddest.Text = NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_coddest)

          oCleGnnp.GetAnagra(NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_conto), dtrTmp)

          If Not dtrTmp Is Nothing Then
            If Trim(NTSCStr(dtrTmp!an_descr1)) <> "" Then
              edAndescr1.Text = Trim(NTSCStr(dtrTmp!an_descr1))
            End If
            If Trim(NTSCStr(dtrTmp!an_descr2)) <> "" Then
              edAndescr2.Text = Trim(NTSCStr(dtrTmp!an_descr2))
            End If
            If Trim(NTSCStr(dtrTmp!an_indir)) <> "" Then
              edAnindir.Text = Trim(NTSCStr(dtrTmp!an_indir))
            End If
            If Trim(NTSCStr(dtrTmp!an_citta)) <> "" Then
              edAncitta.Text = Trim(NTSCStr(dtrTmp!an_citta))
            End If
            If Trim(NTSCStr(dtrTmp!an_prov)) <> "" Then
              edAnprov.Text = Trim(NTSCStr(dtrTmp!an_prov))
            End If
          End If
        End If

        'riempio i campi relativi alla destinazione diversa
        If NTSCInt(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_coddest) <> 0 Then
          edCoddest.Text = NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_coddest)

          oCleGnnp.GetDestDiv(NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_conto), _
                              NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_coddest), dtrTmp)

          If Not dtrTmp Is Nothing Then
            edCoddest.Text = edCoddest.Text & " - " & Trim(NTSCStr(dtrTmp!dd_nomdest))

            If Trim(NTSCStr(dtrTmp!dd_nomdest2)) <> "" Then
              edCoddest2.Text = Trim(NTSCStr(dtrTmp!dd_nomdest2))
            End If
            If Trim(NTSCStr(dtrTmp!dd_inddest)) <> "" Then
              edDdindir.Text = Trim(NTSCStr(dtrTmp!dd_inddest))
            End If
            If Trim(NTSCStr(dtrTmp!dd_locdest)) <> "" Then
              edDdcitta.Text = Trim(NTSCStr(dtrTmp!dd_locdest))
            End If
            If Trim(NTSCStr(dtrTmp!dd_prodest)) <> "" Then
              edDdprov.Text = Trim(NTSCStr(dtrTmp!dd_prodest))
            End If
          End If
        End If

        lbDisp.Visible = False
        edDisp.Visible = False
        lbQtaass.Visible = False
        edQtaass.Visible = False
        grvGndt.NTSAllowUpdate = False
        mn_mmquant.Enabled = False
        mn_mmcolli.Enabled = False
        GctlSetVisEnab(mn_riga, True)
        mn_mmflevas.Enabled = False
        mn_conto.Visible = False
        mn_descr.Visible = False
        Me.Text = oApp.Tr(Me, 130421125239899152, "Dettaglio righe impegni clienti")
        mn_numord.Visible = False
        mn_serie.Visible = False
        mn_datord.Visible = False
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False
        '---------------------------------------------------------------------------------------
        lbCommeca.Visible = False : edCommeca.Visible = False : lbXx_Commeca.Visible = False
        lbLotto.Visible = False : edLotto.Visible = False : edLottox.Visible = False : lbXx_Lotto.Visible = False
        lbFase.Visible = False : edFase.Visible = False : lbXx_Fase.Visible = False
        lbTaglia.Visible = False : edTaglia.Visible = False : lbXx_Taglia.Visible = False
        '---------------------------------------------------------------------------------------
      Else
        'Chiamato da BSORGNDA
        '--- Se non esistono quantità assegnate esce
        If dsGndt.Tables("TTMOPERNP").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128726146179592173, "Nessuna quantità assegnata."))
          bClose = True
          Return
        End If
        '-------------------------------------------
        lbImpegno.Text = oApp.Tr(Me, 130421126796930402, "Cod. articolo:")
        edImpegno.Text = oCleGnnp.strGndtCodart
        lbCliente.Text = oApp.Tr(Me, 130421126450055402, "Descrizione:")
        edCliente.Text = NTSCStr(dsGndt.Tables("TTMOPERNP").Rows(0)!mn_descr)
        edDisp.Text = NTSCStr(oCleGnnp.dGndtDispon)
        GctlSetVisEnab(lbDisp, True)
        GctlSetVisEnab(edDisp, True)
        GctlSetVisEnab(lbQtaass, True)
        GctlSetVisEnab(edQtaass, True)
        edQtaass.Text = NTSCStr(oCleGnnp.dGndtQtaass)
        grvGndt.NTSAllowUpdate = True
        GctlSetVisEnab(mn_mmquant, False)
        GctlSetVisEnab(mn_mmcolli, False)
        mn_riga.Visible = False
        GctlSetVisEnab(mn_mmflevas, False)
        mn_codart.Visible = False
        mn_descr.Visible = False
        Me.Text = oApp.Tr(Me, 130421125345680402, "Modifica proposta di evasione articolo")
        GctlSetVisEnab(mn_numord, True)
        GctlSetVisEnab(mn_serie, True)
        GctlSetVisEnab(mn_datord, True)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbRipristina, False)
        lbAndescr1.Visible = False
        edAndescr1.Visible = False
        edAndescr2.Visible = False
        lbAnindir.Visible = False
        edAnindir.Visible = False
        lbAncitta.Visible = False
        edAncitta.Visible = False
        lbAnprov.Visible = False
        edAnprov.Visible = False
        edCoddest2.Visible = False
        lbCoddest.Visible = False
        edDdindir.Visible = False
        lbDdcitta.Visible = False
        edDdcitta.Visible = False
        lbDdprov.Visible = False
        edDdprov.Visible = False
        edCoddest.Visible = False
        lbDdindir.Visible = False
        '---------------------------------------------------------------------------------------
        edCommeca.Text = NTSCStr(oCleGnnp.lGndtCommeca)
        If Not oCleGnnp.edCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
          edCommeca.Text = "0"
          lbXx_Commeca.Text = ""
        Else
          lbXx_Commeca.Text = strTmp
        End If

        edLotto.Text = NTSCStr(oCleGnnp.lGndtLotto)
        If Not oMenu.ValCodiceDb(NTSCInt(edLotto.Text).ToString, DittaCorrente, "ANALOTTI", "N", strTmp, dttTmp, oCleGnnp.strGndtCodart) Then
          edLotto.Text = "0"
          lbXx_Lotto.Text = ""
          edLottox.Text = ""
        Else
          lbXx_Lotto.Text = strTmp
          If dttTmp.Rows.Count > 0 Then
            edLottox.Text = NTSCStr(dttTmp.Rows(0)!alo_lottox)
          Else
            edLottox.Text = ""
          End If
        End If

        edFase.Text = NTSCStr(oCleGnnp.nGndtFase)
        If Not oCleGnnp.edFase_Validated(NTSCInt(edFase.Text), strTmp) Then
          edFase.Text = "0"
          lbXx_Fase.Text = ""
        Else
          lbXx_Fase.Text = strTmp
        End If

        edTaglia.Text = NTSCStr(oCleGnnp.nGndtTaglia)
        If Not oCleGnnp.edTaglia_Validated(NTSCInt(edTaglia.Text), strTmp) Then
          edTaglia.Text = "0"
          lbXx_Taglia.Text = ""
        Else
          lbXx_Taglia.Text = strTmp
        End If
      End If

      dcGndt.DataSource = dsGndt.Tables("TTMOPERNP")
      grGndt.DataSource = dcGndt

      grvGndt.NTSAllowInsert = False
      grvGndt.NTSAllowDelete = False

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
  Public Overridable Sub FRMORGNDT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGndt.Dispose()
      dsGndt.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMORGNDT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvGndt.NTSRipristinaRigaCorrenteBefore(dcGndt, True) Then Return
      oCleGnnp.GndtRipristina(dcGndt.Position, dcGndt.Filter)
      grvGndt.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

#Region "EventiGriglia"
  Public Overridable Sub grvGndt_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGndt.NTSBeforeRowUpdate
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
#End Region

End Class