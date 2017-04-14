Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGRS2

#Region "Variabili"
  Public oCleScho As CLEORSCHO
  Public oCallParams As CLE__CLDP
  Public dsGrs2 As DataSet
  Public dcGrs2 As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bNoModal As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrs2 As NTSInformatica.NTSGrid
  Public WithEvents grvGrs2 As NTSInformatica.NTSGridView
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel

  Public WithEvents mo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents af_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ko_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents ko_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents ko_serie As NTSInformatica.NTSGridColumn
  Public WithEvents ko_numord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desmaga As NTSInformatica.NTSGridColumn
  Public WithEvents td_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents td_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents mo_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents mo_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents mo_flevapre As NTSInformatica.NTSGridColumn
  Public WithEvents quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datconsor As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents mo_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mo_valore As NTSInformatica.NTSGridColumn
  Public WithEvents an_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents PRZNET As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prelist As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents mo_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mo_coleva As NTSInformatica.NTSGridColumn
  Public WithEvents mo_colpre As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_controp As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents mo_provv As NTSInformatica.NTSGridColumn
  Public WithEvents mo_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents mo_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_ubicaz As NTSInformatica.NTSGridColumn

  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbXx_commeca As NTSInformatica.NTSLabel
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents lbUnmis As NTSInformatica.NTSLabel
  Public WithEvents lbAdatcons As NTSInformatica.NTSLabel
  Public WithEvents lbDadatcons As NTSInformatica.NTSLabel
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazioneDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edUnmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGRS2))
    Me.grGrs2 = New NTSInformatica.NTSGrid
    Me.grvGrs2 = New NTSInformatica.NTSGridView
    Me.mo_fase = New NTSInformatica.NTSGridColumn
    Me.af_descr = New NTSInformatica.NTSGridColumn
    Me.ko_datcons = New NTSInformatica.NTSGridColumn
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.ko_tipork = New NTSInformatica.NTSGridColumn
    Me.ko_serie = New NTSInformatica.NTSGridColumn
    Me.ko_numord = New NTSInformatica.NTSGridColumn
    Me.mo_magaz = New NTSInformatica.NTSGridColumn
    Me.tb_desmaga = New NTSInformatica.NTSGridColumn
    Me.td_riferim = New NTSInformatica.NTSGridColumn
    Me.td_alfpar = New NTSInformatica.NTSGridColumn
    Me.td_numpar = New NTSInformatica.NTSGridColumn
    Me.td_datpar = New NTSInformatica.NTSGridColumn
    Me.mo_quant = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva = New NTSInformatica.NTSGridColumn
    Me.mo_flevas = New NTSInformatica.NTSGridColumn
    Me.mo_quapre = New NTSInformatica.NTSGridColumn
    Me.mo_flevapre = New NTSInformatica.NTSGridColumn
    Me.quaeva = New NTSInformatica.NTSGridColumn
    Me.mo_datconsor = New NTSInformatica.NTSGridColumn
    Me.mo_prezzo = New NTSInformatica.NTSGridColumn
    Me.mo_valore = New NTSInformatica.NTSGridColumn
    Me.an_descr1 = New NTSInformatica.NTSGridColumn
    Me.PRZNET = New NTSInformatica.NTSGridColumn
    Me.mo_scont1 = New NTSInformatica.NTSGridColumn
    Me.mo_scont2 = New NTSInformatica.NTSGridColumn
    Me.mo_scont3 = New NTSInformatica.NTSGridColumn
    Me.mo_scont4 = New NTSInformatica.NTSGridColumn
    Me.mo_scont5 = New NTSInformatica.NTSGridColumn
    Me.mo_scont6 = New NTSInformatica.NTSGridColumn
    Me.mo_prelist = New NTSInformatica.NTSGridColumn
    Me.mo_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mo_colli = New NTSInformatica.NTSGridColumn
    Me.mo_coleva = New NTSInformatica.NTSGridColumn
    Me.mo_colpre = New NTSInformatica.NTSGridColumn
    Me.mo_misura1 = New NTSInformatica.NTSGridColumn
    Me.mo_misura2 = New NTSInformatica.NTSGridColumn
    Me.mo_misura3 = New NTSInformatica.NTSGridColumn
    Me.mo_controp = New NTSInformatica.NTSGridColumn
    Me.mo_codcena = New NTSInformatica.NTSGridColumn
    Me.mo_codcfam = New NTSInformatica.NTSGridColumn
    Me.mo_provv = New NTSInformatica.NTSGridColumn
    Me.mo_vprovv = New NTSInformatica.NTSGridColumn
    Me.mo_provv2 = New NTSInformatica.NTSGridColumn
    Me.mo_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.mo_ubicaz = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.edUnmis = New NTSInformatica.NTSTextBoxStr
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.lbUnmis = New NTSInformatica.NTSLabel
    Me.lbAdatcons = New NTSInformatica.NTSLabel
    Me.lbDadatcons = New NTSInformatica.NTSLabel
    Me.lbXx_commeca = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdini = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grGrs2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrs2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'grGrs2
    '
    Me.grGrs2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grGrs2.EmbeddedNavigator.Name = ""
    Me.grGrs2.Location = New System.Drawing.Point(0, 0)
    Me.grGrs2.MainView = Me.grvGrs2
    Me.grGrs2.Name = "grGrs2"
    Me.grGrs2.Size = New System.Drawing.Size(660, 310)
    Me.grGrs2.TabIndex = 5
    Me.grGrs2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrs2})
    '
    'grvGrs2
    '
    Me.grvGrs2.ActiveFilterEnabled = False
    Me.grvGrs2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mo_fase, Me.af_descr, Me.ko_datcons, Me.td_datord, Me.ko_tipork, Me.ko_serie, Me.ko_numord, Me.mo_magaz, Me.tb_desmaga, Me.td_riferim, Me.td_alfpar, Me.td_numpar, Me.td_datpar, Me.mo_quant, Me.mo_quaeva, Me.mo_flevas, Me.mo_quapre, Me.mo_flevapre, Me.quaeva, Me.mo_datconsor, Me.mo_prezzo, Me.mo_valore, Me.an_descr1, Me.PRZNET, Me.mo_scont1, Me.mo_scont2, Me.mo_scont3, Me.mo_scont4, Me.mo_scont5, Me.mo_scont6, Me.mo_prelist, Me.mo_prezvalc, Me.mo_colli, Me.mo_coleva, Me.mo_colpre, Me.mo_misura1, Me.mo_misura2, Me.mo_misura3, Me.mo_controp, Me.mo_codcena, Me.mo_codcfam, Me.mo_provv, Me.mo_vprovv, Me.mo_provv2, Me.mo_vprovv2, Me.mo_ubicaz})
    Me.grvGrs2.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrs2.Enabled = True
    Me.grvGrs2.GridControl = Me.grGrs2
    Me.grvGrs2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrs2.MinRowHeight = 14
    Me.grvGrs2.Name = "grvGrs2"
    Me.grvGrs2.NTSAllowDelete = True
    Me.grvGrs2.NTSAllowInsert = True
    Me.grvGrs2.NTSAllowUpdate = True
    Me.grvGrs2.NTSMenuContext = Nothing
    Me.grvGrs2.OptionsCustomization.AllowRowSizing = True
    Me.grvGrs2.OptionsFilter.AllowFilterEditor = False
    Me.grvGrs2.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrs2.OptionsNavigation.UseTabKey = False
    Me.grvGrs2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrs2.OptionsView.ColumnAutoWidth = False
    Me.grvGrs2.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrs2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrs2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrs2.OptionsView.ShowGroupPanel = False
    Me.grvGrs2.RowHeight = 16
    '
    'mo_fase
    '
    Me.mo_fase.AppearanceCell.Options.UseBackColor = True
    Me.mo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.mo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_fase.Caption = "Fase"
    Me.mo_fase.Enabled = True
    Me.mo_fase.FieldName = "mo_fase"
    Me.mo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_fase.Name = "mo_fase"
    Me.mo_fase.NTSRepositoryComboBox = Nothing
    Me.mo_fase.NTSRepositoryItemCheck = Nothing
    Me.mo_fase.NTSRepositoryItemMemo = Nothing
    Me.mo_fase.NTSRepositoryItemText = Nothing
    Me.mo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_fase.OptionsFilter.AllowFilter = False
    Me.mo_fase.Visible = True
    Me.mo_fase.VisibleIndex = 0
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
    Me.af_descr.VisibleIndex = 1
    '
    'ko_datcons
    '
    Me.ko_datcons.AppearanceCell.Options.UseBackColor = True
    Me.ko_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.ko_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_datcons.Caption = "Consegna"
    Me.ko_datcons.Enabled = True
    Me.ko_datcons.FieldName = "ko_datcons"
    Me.ko_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_datcons.Name = "ko_datcons"
    Me.ko_datcons.NTSRepositoryComboBox = Nothing
    Me.ko_datcons.NTSRepositoryItemCheck = Nothing
    Me.ko_datcons.NTSRepositoryItemMemo = Nothing
    Me.ko_datcons.NTSRepositoryItemText = Nothing
    Me.ko_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_datcons.OptionsFilter.AllowFilter = False
    Me.ko_datcons.Visible = True
    Me.ko_datcons.VisibleIndex = 2
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data ordine"
    Me.td_datord.Enabled = True
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datord.OptionsFilter.AllowFilter = False
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 3
    '
    'ko_tipork
    '
    Me.ko_tipork.AppearanceCell.Options.UseBackColor = True
    Me.ko_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.ko_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_tipork.Caption = "Tipo"
    Me.ko_tipork.Enabled = True
    Me.ko_tipork.FieldName = "ko_tipork"
    Me.ko_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_tipork.Name = "ko_tipork"
    Me.ko_tipork.NTSRepositoryComboBox = Nothing
    Me.ko_tipork.NTSRepositoryItemCheck = Nothing
    Me.ko_tipork.NTSRepositoryItemMemo = Nothing
    Me.ko_tipork.NTSRepositoryItemText = Nothing
    Me.ko_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_tipork.OptionsFilter.AllowFilter = False
    Me.ko_tipork.Visible = True
    Me.ko_tipork.VisibleIndex = 4
    '
    'ko_serie
    '
    Me.ko_serie.AppearanceCell.Options.UseBackColor = True
    Me.ko_serie.AppearanceCell.Options.UseTextOptions = True
    Me.ko_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_serie.Caption = "Serie"
    Me.ko_serie.Enabled = True
    Me.ko_serie.FieldName = "ko_serie"
    Me.ko_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_serie.Name = "ko_serie"
    Me.ko_serie.NTSRepositoryComboBox = Nothing
    Me.ko_serie.NTSRepositoryItemCheck = Nothing
    Me.ko_serie.NTSRepositoryItemMemo = Nothing
    Me.ko_serie.NTSRepositoryItemText = Nothing
    Me.ko_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_serie.OptionsFilter.AllowFilter = False
    Me.ko_serie.Visible = True
    Me.ko_serie.VisibleIndex = 5
    '
    'ko_numord
    '
    Me.ko_numord.AppearanceCell.Options.UseBackColor = True
    Me.ko_numord.AppearanceCell.Options.UseTextOptions = True
    Me.ko_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_numord.Caption = "N°ordine"
    Me.ko_numord.Enabled = True
    Me.ko_numord.FieldName = "ko_numord"
    Me.ko_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_numord.Name = "ko_numord"
    Me.ko_numord.NTSRepositoryComboBox = Nothing
    Me.ko_numord.NTSRepositoryItemCheck = Nothing
    Me.ko_numord.NTSRepositoryItemMemo = Nothing
    Me.ko_numord.NTSRepositoryItemText = Nothing
    Me.ko_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_numord.OptionsFilter.AllowFilter = False
    Me.ko_numord.Visible = True
    Me.ko_numord.VisibleIndex = 6
    '
    'mo_magaz
    '
    Me.mo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_magaz.Caption = "Magazzino"
    Me.mo_magaz.Enabled = True
    Me.mo_magaz.FieldName = "mo_magaz"
    Me.mo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_magaz.Name = "mo_magaz"
    Me.mo_magaz.NTSRepositoryComboBox = Nothing
    Me.mo_magaz.NTSRepositoryItemCheck = Nothing
    Me.mo_magaz.NTSRepositoryItemMemo = Nothing
    Me.mo_magaz.NTSRepositoryItemText = Nothing
    Me.mo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_magaz.OptionsFilter.AllowFilter = False
    Me.mo_magaz.Visible = True
    Me.mo_magaz.VisibleIndex = 7
    '
    'tb_desmaga
    '
    Me.tb_desmaga.AppearanceCell.Options.UseBackColor = True
    Me.tb_desmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desmaga.Caption = "Descrizione magazzino"
    Me.tb_desmaga.Enabled = True
    Me.tb_desmaga.FieldName = "tb_desmaga"
    Me.tb_desmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desmaga.Name = "tb_desmaga"
    Me.tb_desmaga.NTSRepositoryComboBox = Nothing
    Me.tb_desmaga.NTSRepositoryItemCheck = Nothing
    Me.tb_desmaga.NTSRepositoryItemMemo = Nothing
    Me.tb_desmaga.NTSRepositoryItemText = Nothing
    Me.tb_desmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desmaga.OptionsFilter.AllowFilter = False
    Me.tb_desmaga.Visible = True
    Me.tb_desmaga.VisibleIndex = 8
    '
    'td_riferim
    '
    Me.td_riferim.AppearanceCell.Options.UseBackColor = True
    Me.td_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.td_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_riferim.Caption = "Riferimenti"
    Me.td_riferim.Enabled = True
    Me.td_riferim.FieldName = "td_riferim"
    Me.td_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_riferim.Name = "td_riferim"
    Me.td_riferim.NTSRepositoryComboBox = Nothing
    Me.td_riferim.NTSRepositoryItemCheck = Nothing
    Me.td_riferim.NTSRepositoryItemMemo = Nothing
    Me.td_riferim.NTSRepositoryItemText = Nothing
    Me.td_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_riferim.OptionsFilter.AllowFilter = False
    Me.td_riferim.Visible = True
    Me.td_riferim.VisibleIndex = 9
    '
    'td_alfpar
    '
    Me.td_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.td_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_alfpar.Caption = "Alfa partita"
    Me.td_alfpar.Enabled = True
    Me.td_alfpar.FieldName = "td_alfpar"
    Me.td_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_alfpar.Name = "td_alfpar"
    Me.td_alfpar.NTSRepositoryComboBox = Nothing
    Me.td_alfpar.NTSRepositoryItemCheck = Nothing
    Me.td_alfpar.NTSRepositoryItemMemo = Nothing
    Me.td_alfpar.NTSRepositoryItemText = Nothing
    Me.td_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_alfpar.OptionsFilter.AllowFilter = False
    Me.td_alfpar.Visible = True
    Me.td_alfpar.VisibleIndex = 10
    '
    'td_numpar
    '
    Me.td_numpar.AppearanceCell.Options.UseBackColor = True
    Me.td_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numpar.Caption = "N°partita"
    Me.td_numpar.Enabled = True
    Me.td_numpar.FieldName = "td_numpar"
    Me.td_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numpar.Name = "td_numpar"
    Me.td_numpar.NTSRepositoryComboBox = Nothing
    Me.td_numpar.NTSRepositoryItemCheck = Nothing
    Me.td_numpar.NTSRepositoryItemMemo = Nothing
    Me.td_numpar.NTSRepositoryItemText = Nothing
    Me.td_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numpar.OptionsFilter.AllowFilter = False
    Me.td_numpar.Visible = True
    Me.td_numpar.VisibleIndex = 11
    '
    'td_datpar
    '
    Me.td_datpar.AppearanceCell.Options.UseBackColor = True
    Me.td_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datpar.Caption = "Data partita"
    Me.td_datpar.Enabled = True
    Me.td_datpar.FieldName = "td_datpar"
    Me.td_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datpar.Name = "td_datpar"
    Me.td_datpar.NTSRepositoryComboBox = Nothing
    Me.td_datpar.NTSRepositoryItemCheck = Nothing
    Me.td_datpar.NTSRepositoryItemMemo = Nothing
    Me.td_datpar.NTSRepositoryItemText = Nothing
    Me.td_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datpar.OptionsFilter.AllowFilter = False
    Me.td_datpar.Visible = True
    Me.td_datpar.VisibleIndex = 12
    '
    'mo_quant
    '
    Me.mo_quant.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant.Caption = "Qtà ordinata"
    Me.mo_quant.Enabled = True
    Me.mo_quant.FieldName = "mo_quant"
    Me.mo_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant.Name = "mo_quant"
    Me.mo_quant.NTSRepositoryComboBox = Nothing
    Me.mo_quant.NTSRepositoryItemCheck = Nothing
    Me.mo_quant.NTSRepositoryItemMemo = Nothing
    Me.mo_quant.NTSRepositoryItemText = Nothing
    Me.mo_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant.OptionsFilter.AllowFilter = False
    Me.mo_quant.Visible = True
    Me.mo_quant.VisibleIndex = 13
    '
    'mo_quaeva
    '
    Me.mo_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva.Caption = "Qtà spedita"
    Me.mo_quaeva.Enabled = True
    Me.mo_quaeva.FieldName = "mo_quaeva"
    Me.mo_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva.Name = "mo_quaeva"
    Me.mo_quaeva.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva.NTSRepositoryItemText = Nothing
    Me.mo_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva.OptionsFilter.AllowFilter = False
    Me.mo_quaeva.Visible = True
    Me.mo_quaeva.VisibleIndex = 14
    '
    'mo_flevas
    '
    Me.mo_flevas.AppearanceCell.Options.UseBackColor = True
    Me.mo_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.mo_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_flevas.Caption = "Evaso"
    Me.mo_flevas.Enabled = True
    Me.mo_flevas.FieldName = "mo_flevas"
    Me.mo_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_flevas.Name = "mo_flevas"
    Me.mo_flevas.NTSRepositoryComboBox = Nothing
    Me.mo_flevas.NTSRepositoryItemCheck = Nothing
    Me.mo_flevas.NTSRepositoryItemMemo = Nothing
    Me.mo_flevas.NTSRepositoryItemText = Nothing
    Me.mo_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_flevas.OptionsFilter.AllowFilter = False
    Me.mo_flevas.Visible = True
    Me.mo_flevas.VisibleIndex = 15
    '
    'mo_quapre
    '
    Me.mo_quapre.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre.Caption = "Qtà prenotata"
    Me.mo_quapre.Enabled = True
    Me.mo_quapre.FieldName = "mo_quapre"
    Me.mo_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre.Name = "mo_quapre"
    Me.mo_quapre.NTSRepositoryComboBox = Nothing
    Me.mo_quapre.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre.NTSRepositoryItemText = Nothing
    Me.mo_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre.OptionsFilter.AllowFilter = False
    Me.mo_quapre.Visible = True
    Me.mo_quapre.VisibleIndex = 16
    '
    'mo_flevapre
    '
    Me.mo_flevapre.AppearanceCell.Options.UseBackColor = True
    Me.mo_flevapre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_flevapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_flevapre.Caption = "Pr."
    Me.mo_flevapre.Enabled = True
    Me.mo_flevapre.FieldName = "mo_flevapre"
    Me.mo_flevapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_flevapre.Name = "mo_flevapre"
    Me.mo_flevapre.NTSRepositoryComboBox = Nothing
    Me.mo_flevapre.NTSRepositoryItemCheck = Nothing
    Me.mo_flevapre.NTSRepositoryItemMemo = Nothing
    Me.mo_flevapre.NTSRepositoryItemText = Nothing
    Me.mo_flevapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_flevapre.OptionsFilter.AllowFilter = False
    Me.mo_flevapre.Visible = True
    Me.mo_flevapre.VisibleIndex = 17
    '
    'quaeva
    '
    Me.quaeva.AppearanceCell.Options.UseBackColor = True
    Me.quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quaeva.Caption = "Qtà.residua"
    Me.quaeva.Enabled = True
    Me.quaeva.FieldName = "quaeva"
    Me.quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quaeva.Name = "quaeva"
    Me.quaeva.NTSRepositoryComboBox = Nothing
    Me.quaeva.NTSRepositoryItemCheck = Nothing
    Me.quaeva.NTSRepositoryItemMemo = Nothing
    Me.quaeva.NTSRepositoryItemText = Nothing
    Me.quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quaeva.OptionsFilter.AllowFilter = False
    Me.quaeva.Visible = True
    Me.quaeva.VisibleIndex = 18
    '
    'mo_datconsor
    '
    Me.mo_datconsor.AppearanceCell.Options.UseBackColor = True
    Me.mo_datconsor.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datconsor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datconsor.Caption = "Data cons. ordine"
    Me.mo_datconsor.Enabled = True
    Me.mo_datconsor.FieldName = "mo_datconsor"
    Me.mo_datconsor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datconsor.Name = "mo_datconsor"
    Me.mo_datconsor.NTSRepositoryComboBox = Nothing
    Me.mo_datconsor.NTSRepositoryItemCheck = Nothing
    Me.mo_datconsor.NTSRepositoryItemMemo = Nothing
    Me.mo_datconsor.NTSRepositoryItemText = Nothing
    Me.mo_datconsor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datconsor.OptionsFilter.AllowFilter = False
    Me.mo_datconsor.Visible = True
    Me.mo_datconsor.VisibleIndex = 19
    '
    'mo_prezzo
    '
    Me.mo_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezzo.Caption = "Prezzo"
    Me.mo_prezzo.Enabled = True
    Me.mo_prezzo.FieldName = "mo_prezzo"
    Me.mo_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezzo.Name = "mo_prezzo"
    Me.mo_prezzo.NTSRepositoryComboBox = Nothing
    Me.mo_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mo_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mo_prezzo.NTSRepositoryItemText = Nothing
    Me.mo_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezzo.OptionsFilter.AllowFilter = False
    Me.mo_prezzo.Visible = True
    Me.mo_prezzo.VisibleIndex = 20
    '
    'mo_valore
    '
    Me.mo_valore.AppearanceCell.Options.UseBackColor = True
    Me.mo_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mo_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_valore.Caption = "Valore"
    Me.mo_valore.Enabled = True
    Me.mo_valore.FieldName = "mo_valore"
    Me.mo_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_valore.Name = "mo_valore"
    Me.mo_valore.NTSRepositoryComboBox = Nothing
    Me.mo_valore.NTSRepositoryItemCheck = Nothing
    Me.mo_valore.NTSRepositoryItemMemo = Nothing
    Me.mo_valore.NTSRepositoryItemText = Nothing
    Me.mo_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_valore.OptionsFilter.AllowFilter = False
    Me.mo_valore.Visible = True
    Me.mo_valore.VisibleIndex = 21
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Cliente/Fornitore"
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
    Me.an_descr1.VisibleIndex = 22
    '
    'PRZNET
    '
    Me.PRZNET.AppearanceCell.Options.UseBackColor = True
    Me.PRZNET.AppearanceCell.Options.UseTextOptions = True
    Me.PRZNET.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.PRZNET.Caption = "Prezzo N."
    Me.PRZNET.Enabled = True
    Me.PRZNET.FieldName = "PRZNET"
    Me.PRZNET.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.PRZNET.Name = "PRZNET"
    Me.PRZNET.NTSRepositoryComboBox = Nothing
    Me.PRZNET.NTSRepositoryItemCheck = Nothing
    Me.PRZNET.NTSRepositoryItemMemo = Nothing
    Me.PRZNET.NTSRepositoryItemText = Nothing
    Me.PRZNET.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.PRZNET.OptionsFilter.AllowFilter = False
    Me.PRZNET.Visible = True
    Me.PRZNET.VisibleIndex = 23
    '
    'mo_scont1
    '
    Me.mo_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont1.Caption = "Sconto 1"
    Me.mo_scont1.Enabled = True
    Me.mo_scont1.FieldName = "mo_scont1"
    Me.mo_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont1.Name = "mo_scont1"
    Me.mo_scont1.NTSRepositoryComboBox = Nothing
    Me.mo_scont1.NTSRepositoryItemCheck = Nothing
    Me.mo_scont1.NTSRepositoryItemMemo = Nothing
    Me.mo_scont1.NTSRepositoryItemText = Nothing
    Me.mo_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont1.OptionsFilter.AllowFilter = False
    Me.mo_scont1.Visible = True
    Me.mo_scont1.VisibleIndex = 24
    '
    'mo_scont2
    '
    Me.mo_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont2.Caption = "Sconto 2"
    Me.mo_scont2.Enabled = True
    Me.mo_scont2.FieldName = "mo_scont2"
    Me.mo_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont2.Name = "mo_scont2"
    Me.mo_scont2.NTSRepositoryComboBox = Nothing
    Me.mo_scont2.NTSRepositoryItemCheck = Nothing
    Me.mo_scont2.NTSRepositoryItemMemo = Nothing
    Me.mo_scont2.NTSRepositoryItemText = Nothing
    Me.mo_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont2.OptionsFilter.AllowFilter = False
    Me.mo_scont2.Visible = True
    Me.mo_scont2.VisibleIndex = 25
    '
    'mo_scont3
    '
    Me.mo_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont3.Caption = "Sconto 3"
    Me.mo_scont3.Enabled = True
    Me.mo_scont3.FieldName = "mo_scont3"
    Me.mo_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont3.Name = "mo_scont3"
    Me.mo_scont3.NTSRepositoryComboBox = Nothing
    Me.mo_scont3.NTSRepositoryItemCheck = Nothing
    Me.mo_scont3.NTSRepositoryItemMemo = Nothing
    Me.mo_scont3.NTSRepositoryItemText = Nothing
    Me.mo_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont3.OptionsFilter.AllowFilter = False
    Me.mo_scont3.Visible = True
    Me.mo_scont3.VisibleIndex = 26
    '
    'mo_scont4
    '
    Me.mo_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont4.Caption = "Sconto 4"
    Me.mo_scont4.Enabled = True
    Me.mo_scont4.FieldName = "mo_scont4"
    Me.mo_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont4.Name = "mo_scont4"
    Me.mo_scont4.NTSRepositoryComboBox = Nothing
    Me.mo_scont4.NTSRepositoryItemCheck = Nothing
    Me.mo_scont4.NTSRepositoryItemMemo = Nothing
    Me.mo_scont4.NTSRepositoryItemText = Nothing
    Me.mo_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont4.OptionsFilter.AllowFilter = False
    Me.mo_scont4.Visible = True
    Me.mo_scont4.VisibleIndex = 27
    '
    'mo_scont5
    '
    Me.mo_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont5.Caption = "Sconto 5"
    Me.mo_scont5.Enabled = True
    Me.mo_scont5.FieldName = "mo_scont5"
    Me.mo_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont5.Name = "mo_scont5"
    Me.mo_scont5.NTSRepositoryComboBox = Nothing
    Me.mo_scont5.NTSRepositoryItemCheck = Nothing
    Me.mo_scont5.NTSRepositoryItemMemo = Nothing
    Me.mo_scont5.NTSRepositoryItemText = Nothing
    Me.mo_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont5.OptionsFilter.AllowFilter = False
    Me.mo_scont5.Visible = True
    Me.mo_scont5.VisibleIndex = 28
    '
    'mo_scont6
    '
    Me.mo_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont6.Caption = "Sconto 6 "
    Me.mo_scont6.Enabled = True
    Me.mo_scont6.FieldName = "mo_scont6"
    Me.mo_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont6.Name = "mo_scont6"
    Me.mo_scont6.NTSRepositoryComboBox = Nothing
    Me.mo_scont6.NTSRepositoryItemCheck = Nothing
    Me.mo_scont6.NTSRepositoryItemMemo = Nothing
    Me.mo_scont6.NTSRepositoryItemText = Nothing
    Me.mo_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont6.OptionsFilter.AllowFilter = False
    Me.mo_scont6.Visible = True
    Me.mo_scont6.VisibleIndex = 29
    '
    'mo_prelist
    '
    Me.mo_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mo_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prelist.Caption = "Prezzo listino"
    Me.mo_prelist.Enabled = True
    Me.mo_prelist.FieldName = "mo_prelist"
    Me.mo_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prelist.Name = "mo_prelist"
    Me.mo_prelist.NTSRepositoryComboBox = Nothing
    Me.mo_prelist.NTSRepositoryItemCheck = Nothing
    Me.mo_prelist.NTSRepositoryItemMemo = Nothing
    Me.mo_prelist.NTSRepositoryItemText = Nothing
    Me.mo_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prelist.OptionsFilter.AllowFilter = False
    Me.mo_prelist.Visible = True
    Me.mo_prelist.VisibleIndex = 30
    '
    'mo_prezvalc
    '
    Me.mo_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezvalc.Caption = "Prz.Val."
    Me.mo_prezvalc.Enabled = True
    Me.mo_prezvalc.FieldName = "mo_prezvalc"
    Me.mo_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezvalc.Name = "mo_prezvalc"
    Me.mo_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mo_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mo_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mo_prezvalc.NTSRepositoryItemText = Nothing
    Me.mo_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezvalc.OptionsFilter.AllowFilter = False
    Me.mo_prezvalc.Visible = True
    Me.mo_prezvalc.VisibleIndex = 31
    '
    'mo_colli
    '
    Me.mo_colli.AppearanceCell.Options.UseBackColor = True
    Me.mo_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mo_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_colli.Caption = "Colli"
    Me.mo_colli.Enabled = True
    Me.mo_colli.FieldName = "mo_colli"
    Me.mo_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_colli.Name = "mo_colli"
    Me.mo_colli.NTSRepositoryComboBox = Nothing
    Me.mo_colli.NTSRepositoryItemCheck = Nothing
    Me.mo_colli.NTSRepositoryItemMemo = Nothing
    Me.mo_colli.NTSRepositoryItemText = Nothing
    Me.mo_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_colli.OptionsFilter.AllowFilter = False
    Me.mo_colli.Visible = True
    Me.mo_colli.VisibleIndex = 32
    '
    'mo_coleva
    '
    Me.mo_coleva.AppearanceCell.Options.UseBackColor = True
    Me.mo_coleva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_coleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_coleva.Caption = "Colli evasi"
    Me.mo_coleva.Enabled = True
    Me.mo_coleva.FieldName = "mo_coleva"
    Me.mo_coleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_coleva.Name = "mo_coleva"
    Me.mo_coleva.NTSRepositoryComboBox = Nothing
    Me.mo_coleva.NTSRepositoryItemCheck = Nothing
    Me.mo_coleva.NTSRepositoryItemMemo = Nothing
    Me.mo_coleva.NTSRepositoryItemText = Nothing
    Me.mo_coleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_coleva.OptionsFilter.AllowFilter = False
    Me.mo_coleva.Visible = True
    Me.mo_coleva.VisibleIndex = 33
    '
    'mo_colpre
    '
    Me.mo_colpre.AppearanceCell.Options.UseBackColor = True
    Me.mo_colpre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_colpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_colpre.Caption = "Colli prenotati"
    Me.mo_colpre.Enabled = True
    Me.mo_colpre.FieldName = "mo_colpre"
    Me.mo_colpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_colpre.Name = "mo_colpre"
    Me.mo_colpre.NTSRepositoryComboBox = Nothing
    Me.mo_colpre.NTSRepositoryItemCheck = Nothing
    Me.mo_colpre.NTSRepositoryItemMemo = Nothing
    Me.mo_colpre.NTSRepositoryItemText = Nothing
    Me.mo_colpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_colpre.OptionsFilter.AllowFilter = False
    Me.mo_colpre.Visible = True
    Me.mo_colpre.VisibleIndex = 34
    '
    'mo_misura1
    '
    Me.mo_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura1.Caption = "Mis.1"
    Me.mo_misura1.Enabled = True
    Me.mo_misura1.FieldName = "mo_misura1"
    Me.mo_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura1.Name = "mo_misura1"
    Me.mo_misura1.NTSRepositoryComboBox = Nothing
    Me.mo_misura1.NTSRepositoryItemCheck = Nothing
    Me.mo_misura1.NTSRepositoryItemMemo = Nothing
    Me.mo_misura1.NTSRepositoryItemText = Nothing
    Me.mo_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura1.OptionsFilter.AllowFilter = False
    '
    'mo_misura2
    '
    Me.mo_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura2.Caption = "Mis.2"
    Me.mo_misura2.Enabled = True
    Me.mo_misura2.FieldName = "mo_misura2"
    Me.mo_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura2.Name = "mo_misura2"
    Me.mo_misura2.NTSRepositoryComboBox = Nothing
    Me.mo_misura2.NTSRepositoryItemCheck = Nothing
    Me.mo_misura2.NTSRepositoryItemMemo = Nothing
    Me.mo_misura2.NTSRepositoryItemText = Nothing
    Me.mo_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura2.OptionsFilter.AllowFilter = False
    '
    'mo_misura3
    '
    Me.mo_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura3.Caption = "Mis.3"
    Me.mo_misura3.Enabled = True
    Me.mo_misura3.FieldName = "mo_misura3"
    Me.mo_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura3.Name = "mo_misura3"
    Me.mo_misura3.NTSRepositoryComboBox = Nothing
    Me.mo_misura3.NTSRepositoryItemCheck = Nothing
    Me.mo_misura3.NTSRepositoryItemMemo = Nothing
    Me.mo_misura3.NTSRepositoryItemText = Nothing
    Me.mo_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura3.OptionsFilter.AllowFilter = False
    '
    'mo_controp
    '
    Me.mo_controp.AppearanceCell.Options.UseBackColor = True
    Me.mo_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_controp.Caption = "Contropartita"
    Me.mo_controp.Enabled = True
    Me.mo_controp.FieldName = "mo_controp"
    Me.mo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_controp.Name = "mo_controp"
    Me.mo_controp.NTSRepositoryComboBox = Nothing
    Me.mo_controp.NTSRepositoryItemCheck = Nothing
    Me.mo_controp.NTSRepositoryItemMemo = Nothing
    Me.mo_controp.NTSRepositoryItemText = Nothing
    Me.mo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_controp.OptionsFilter.AllowFilter = False
    Me.mo_controp.Visible = True
    Me.mo_controp.VisibleIndex = 35
    '
    'mo_codcena
    '
    Me.mo_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mo_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codcena.Caption = "Centro"
    Me.mo_codcena.Enabled = True
    Me.mo_codcena.FieldName = "mo_codcena"
    Me.mo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codcena.Name = "mo_codcena"
    Me.mo_codcena.NTSRepositoryComboBox = Nothing
    Me.mo_codcena.NTSRepositoryItemCheck = Nothing
    Me.mo_codcena.NTSRepositoryItemMemo = Nothing
    Me.mo_codcena.NTSRepositoryItemText = Nothing
    Me.mo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codcena.OptionsFilter.AllowFilter = False
    Me.mo_codcena.Visible = True
    Me.mo_codcena.VisibleIndex = 36
    '
    'mo_codcfam
    '
    Me.mo_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mo_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codcfam.Caption = "Linea"
    Me.mo_codcfam.Enabled = True
    Me.mo_codcfam.FieldName = "mo_codcfam"
    Me.mo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codcfam.Name = "mo_codcfam"
    Me.mo_codcfam.NTSRepositoryComboBox = Nothing
    Me.mo_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mo_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mo_codcfam.NTSRepositoryItemText = Nothing
    Me.mo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codcfam.OptionsFilter.AllowFilter = False
    Me.mo_codcfam.Visible = True
    Me.mo_codcfam.VisibleIndex = 37
    '
    'mo_provv
    '
    Me.mo_provv.AppearanceCell.Options.UseBackColor = True
    Me.mo_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mo_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_provv.Caption = "Provvigione"
    Me.mo_provv.Enabled = True
    Me.mo_provv.FieldName = "mo_provv"
    Me.mo_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_provv.Name = "mo_provv"
    Me.mo_provv.NTSRepositoryComboBox = Nothing
    Me.mo_provv.NTSRepositoryItemCheck = Nothing
    Me.mo_provv.NTSRepositoryItemMemo = Nothing
    Me.mo_provv.NTSRepositoryItemText = Nothing
    Me.mo_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_provv.OptionsFilter.AllowFilter = False
    Me.mo_provv.Visible = True
    Me.mo_provv.VisibleIndex = 38
    '
    'mo_vprovv
    '
    Me.mo_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mo_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mo_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_vprovv.Caption = "Valore provvigione"
    Me.mo_vprovv.Enabled = True
    Me.mo_vprovv.FieldName = "mo_vprovv"
    Me.mo_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_vprovv.Name = "mo_vprovv"
    Me.mo_vprovv.NTSRepositoryComboBox = Nothing
    Me.mo_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mo_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mo_vprovv.NTSRepositoryItemText = Nothing
    Me.mo_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_vprovv.OptionsFilter.AllowFilter = False
    Me.mo_vprovv.Visible = True
    Me.mo_vprovv.VisibleIndex = 39
    '
    'mo_provv2
    '
    Me.mo_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mo_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_provv2.Caption = "Provvigione 2"
    Me.mo_provv2.Enabled = True
    Me.mo_provv2.FieldName = "mo_provv2"
    Me.mo_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_provv2.Name = "mo_provv2"
    Me.mo_provv2.NTSRepositoryComboBox = Nothing
    Me.mo_provv2.NTSRepositoryItemCheck = Nothing
    Me.mo_provv2.NTSRepositoryItemMemo = Nothing
    Me.mo_provv2.NTSRepositoryItemText = Nothing
    Me.mo_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_provv2.OptionsFilter.AllowFilter = False
    Me.mo_provv2.Visible = True
    Me.mo_provv2.VisibleIndex = 40
    '
    'mo_vprovv2
    '
    Me.mo_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mo_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_vprovv2.Caption = "Valore provvigione 2"
    Me.mo_vprovv2.Enabled = True
    Me.mo_vprovv2.FieldName = "mo_vprovv2"
    Me.mo_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_vprovv2.Name = "mo_vprovv2"
    Me.mo_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mo_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mo_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mo_vprovv2.NTSRepositoryItemText = Nothing
    Me.mo_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_vprovv2.OptionsFilter.AllowFilter = False
    Me.mo_vprovv2.Visible = True
    Me.mo_vprovv2.VisibleIndex = 41
    '
    'mo_ubicaz
    '
    Me.mo_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_ubicaz.Caption = "Ubicazione"
    Me.mo_ubicaz.Enabled = True
    Me.mo_ubicaz.FieldName = "mo_ubicaz"
    Me.mo_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_ubicaz.Name = "mo_ubicaz"
    Me.mo_ubicaz.NTSRepositoryComboBox = Nothing
    Me.mo_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.mo_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.mo_ubicaz.NTSRepositoryItemText = Nothing
    Me.mo_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_ubicaz.OptionsFilter.AllowFilter = False
    Me.mo_ubicaz.Visible = True
    Me.mo_ubicaz.VisibleIndex = 42
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edCommeca)
    Me.pnTop.Controls.Add(Me.edUnmis)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.edAdatcons)
    Me.pnTop.Controls.Add(Me.edDadatcons)
    Me.pnTop.Controls.Add(Me.lbUnmis)
    Me.pnTop.Controls.Add(Me.lbAdatcons)
    Me.pnTop.Controls.Add(Me.lbDadatcons)
    Me.pnTop.Controls.Add(Me.lbXx_commeca)
    Me.pnTop.Controls.Add(Me.lbCommeca)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 102)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.Location = New System.Drawing.Point(62, 3)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(74, 20)
    Me.edCommeca.TabIndex = 32
    '
    'edUnmis
    '
    Me.edUnmis.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edUnmis.Location = New System.Drawing.Point(594, 46)
    Me.edUnmis.Name = "edUnmis"
    Me.edUnmis.NTSDbField = ""
    Me.edUnmis.NTSForzaVisZoom = False
    Me.edUnmis.NTSOldValue = ""
    Me.edUnmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUnmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUnmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUnmis.Properties.MaxLength = 65536
    Me.edUnmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUnmis.Size = New System.Drawing.Size(54, 20)
    Me.edUnmis.TabIndex = 30
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edArticolo.Location = New System.Drawing.Point(62, 24)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(131, 20)
    Me.edArticolo.TabIndex = 26
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatcons.Location = New System.Drawing.Point(217, 46)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(88, 20)
    Me.edAdatcons.TabIndex = 25
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatcons.Location = New System.Drawing.Point(62, 46)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(88, 20)
    Me.edDadatcons.TabIndex = 24
    '
    'lbUnmis
    '
    Me.lbUnmis.AutoSize = True
    Me.lbUnmis.BackColor = System.Drawing.Color.Transparent
    Me.lbUnmis.Location = New System.Drawing.Point(499, 46)
    Me.lbUnmis.Name = "lbUnmis"
    Me.lbUnmis.NTSDbField = ""
    Me.lbUnmis.Size = New System.Drawing.Size(77, 13)
    Me.lbUnmis.TabIndex = 16
    Me.lbUnmis.Text = "Unità di misura"
    Me.lbUnmis.Tooltip = ""
    Me.lbUnmis.UseMnemonic = False
    '
    'lbAdatcons
    '
    Me.lbAdatcons.AutoSize = True
    Me.lbAdatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbAdatcons.Location = New System.Drawing.Point(156, 46)
    Me.lbAdatcons.Name = "lbAdatcons"
    Me.lbAdatcons.NTSDbField = ""
    Me.lbAdatcons.Size = New System.Drawing.Size(22, 13)
    Me.lbAdatcons.TabIndex = 14
    Me.lbAdatcons.Text = "al :"
    Me.lbAdatcons.Tooltip = ""
    Me.lbAdatcons.UseMnemonic = False
    '
    'lbDadatcons
    '
    Me.lbDadatcons.AutoSize = True
    Me.lbDadatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatcons.Location = New System.Drawing.Point(0, 46)
    Me.lbDadatcons.Name = "lbDadatcons"
    Me.lbDadatcons.NTSDbField = ""
    Me.lbDadatcons.Size = New System.Drawing.Size(56, 13)
    Me.lbDadatcons.TabIndex = 12
    Me.lbDadatcons.Text = "Cons.dal :"
    Me.lbDadatcons.Tooltip = ""
    Me.lbDadatcons.UseMnemonic = False
    '
    'lbXx_commeca
    '
    Me.lbXx_commeca.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_commeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_commeca.Location = New System.Drawing.Point(145, 3)
    Me.lbXx_commeca.Name = "lbXx_commeca"
    Me.lbXx_commeca.NTSDbField = ""
    Me.lbXx_commeca.Size = New System.Drawing.Size(503, 20)
    Me.lbXx_commeca.TabIndex = 11
    Me.lbXx_commeca.Tooltip = ""
    Me.lbXx_commeca.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.AutoSize = True
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.Location = New System.Drawing.Point(7, 4)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(40, 13)
    Me.lbCommeca.TabIndex = 9
    Me.lbCommeca.Text = "Comm."
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'lbXx_articolo
    '
    Me.lbXx_articolo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_articolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_articolo.Location = New System.Drawing.Point(199, 23)
    Me.lbXx_articolo.Name = "lbXx_articolo"
    Me.lbXx_articolo.NTSDbField = ""
    Me.lbXx_articolo.Size = New System.Drawing.Size(449, 20)
    Me.lbXx_articolo.TabIndex = 27
    Me.lbXx_articolo.Tooltip = ""
    Me.lbXx_articolo.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(7, 24)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 3
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrs2)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 132)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 310)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbTaglie, Me.tlbNavigazioneDoc, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbOrdini, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaVideo, Me.tlbStampa})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdini, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.tlbPrimo.Id = 9
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 10
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 11
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 12
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.Id = 0
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbOrdini
    '
    Me.tlbOrdini.Caption = "Apri ordine"
    Me.tlbOrdini.Glyph = CType(resources.GetObject("tlbOrdini.Glyph"), System.Drawing.Image)
    Me.tlbOrdini.Id = 13
    Me.tlbOrdini.Name = "tlbOrdini"
    Me.tlbOrdini.Visible = True
    '
    'tlbNavigazioneDoc
    '
    Me.tlbNavigazioneDoc.Caption = "NavigazioneDoc"
    Me.tlbNavigazioneDoc.Glyph = CType(resources.GetObject("tlbNavigazioneDoc.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneDoc.Id = 4
    Me.tlbNavigazioneDoc.Name = "tlbNavigazioneDoc"
    Me.tlbNavigazioneDoc.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 14
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 16
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 17
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 8
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMORGRS2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(660, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMORGRS2"
    Me.NTSLastControlFocussed = Me.grGrs2
    Me.Text = "STAMPA / VISUALIZZAZIONE SCHEDE ORDINI"
    CType(Me.grGrs2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrs2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByVal cleScho As CLEORSCHO)
    oCleScho = cleScho
    AddHandler oCleScho.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipork As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbOrdini.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbNavigazioneDoc.GlyphPath = (oApp.ChildImageDir & "\navigazione.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edCommeca.NTSSetParam(oMenu, oApp.Tr(Me, 129048516535613995, "Comm."), "0")
      edUnmis.NTSSetParam(oMenu, oApp.Tr(Me, 129048516596709309, "Unità di misura"), 0)
      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 129048516633897761, "Articolo"), 0)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129048516661710973, "al :"), True)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129048516685461581, "Cons.dal :"), True)

      grvGrs2.NTSSetParam(oMenu, "Stampa / Visualizzazione Schede Ordini")

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"R", "IC"})
      dttTipork.Rows.Add(New Object() {"O", "OF"})
      dttTipork.Rows.Add(New Object() {"H", "OP"})
      dttTipork.Rows.Add(New Object() {"Y", "IP"})
      dttTipork.Rows.Add(New Object() {"Q", "PR"})
      dttTipork.Rows.Add(New Object() {"X", "IT"})
      dttTipork.Rows.Add(New Object() {"$", "OFA"})
      dttTipork.Rows.Add(New Object() {"V", "ICA"})
      dttTipork.Rows.Add(New Object() {"#", "ICO"})
      dttTipork.AcceptChanges()

      mo_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752991232960, "Fase"), "0", 4, 0, 9999)
      af_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752991389330, "Descrizione fase"), 0, True)
      ko_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128822752991545700, "Consegna"), True)
      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128822752991702070, "Data ordine"), True)
      ko_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128822752991858440, "Tipo"), dttTipork, "val", "cod")
      ko_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752992014810, "Serie"), CLN__STD.SerieMaxLen, True)
      ko_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752992171180, "N°ordine"), "0", 9, 0, 999999999)
      mo_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752992327550, "Magazzino"), "0", 4, 0, 9999)
      tb_desmaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752992483920, "Descrizione magazzino"), 0, True)
      td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752992640290, "Riferimenti"), 0, True)
      td_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752992796660, "Alfa partita"), CLN__STD.SerieMaxLen, True)
      td_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752992953030, "N°partita"), "0", 9, 0, 999999999)
      td_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128822752993109400, "Data partita"), True)
      mo_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752993265770, "Qtà ordinata"), "#,##0.000", 15)
      mo_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752993422140, "Qtà spedita"), "#,##0.000", 15)
      mo_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128822752993578510, "Evaso"), "S", "C")
      mo_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752993734880, "Qtà prenotata"), "#,##0.000", 15)
      mo_flevapre.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752993891250, "Pr."), 0, True)
      quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752994047620, "Qtà.residua"), "#,##0.000", 15)
      mo_datconsor.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128822752994203990, "Data cons. ordine"), True)
      mo_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752994360360, "Prezzo"), TrovaFmtPrz, 15)
      mo_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752994516730, "Valore"), "#,##0.00", 15)
      an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752994673100, "Cliente/Fornitore"), 0, True)
      PRZNET.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752994829470, "Prezzo N."), TrovaFmtPrz, 15)
      mo_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752994985840, "Sconto 1"), "#,##0.00", 15)
      mo_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995142210, "Sconto 2"), "#,##0.00", 15)
      mo_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995298580, "Sconto 3"), "#,##0.00", 15)
      mo_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995454950, "Sconto 4"), "#,##0.00", 15)
      mo_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995611320, "Sconto 5"), "#,##0.00", 15)
      mo_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995767690, "Sconto 6 "), "#,##0.00", 15)
      mo_prelist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752995924060, "Prezzo listino"), "#,##0.00", 15)
      mo_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996080430, "Prz.Val."), TrovaFmtPrz(1), 15)
      mo_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996236800, "Colli"), "#,##0.000", 15)
      mo_coleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996393170, "Colli evasi"), "#,##0.000", 15)
      mo_colpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996549540, "Colli prenotati"), "#,##0.000", 15)
      mo_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996705910, "Mis.1"), "#,##0.000", 15)
      mo_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752996862280, "Mis.2"), "#,##0.000", 15)
      mo_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997018650, "Mis.3"), "#,##0.000", 15)
      mo_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997175020, "Contropartita"), "0", 4, 0, 9999)
      mo_codcena.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997331390, "Centro"), "0", 9, 0, 999999999)
      mo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752997487760, "Linea"), 0, True)
      mo_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997644130, "Provvigione"), "#,##0.000", 15)
      mo_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997800500, "Valore provvigione"), "#,##0.00", 15)
      mo_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752997956870, "Provvigione 2"), "#,##0.000", 15)
      mo_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128822752998113240, "Valore provvigione 2"), "#,##0.00", 15)
      mo_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128822752998269610, "Ubicazione"), 0, True)
      grvGrs2.Enabled = False
      grvGrs2.NTSAllowInsert = False

      edDadatcons.Enabled = False
      edAdatcons.Enabled = False
      edArticolo.Enabled = False
      edUnmis.Enabled = False
      edCommeca.Enabled = False

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
  Public Overridable Sub FRMORGRS2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      edDadatcons.Text = oCleScho.strSchoDadatcons
      edAdatcons.Text = oCleScho.strSchoAdatcons
      If Not Apri() Then Exit Sub
      ApriRecordset()
      RiempiLabel()
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs2ModTCO = False Then tlbTaglie.Visible = False
      '-----------------------------------------------------------------------------------------
      If Not oCleScho.GetRelease() Then
        tlbNavigazioneDoc.Visible = True
      Else
        tlbNavigazioneDoc.Visible = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRS2_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRS2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then oMenu.ResetTblInstId("TTSCHO", False, oCleScho.lIITTScho)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORGRS2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrs2.Dispose()
      dsGrs2.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      dcGrs2.MoveFirst()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      dcGrs2.MovePrevious()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      dcGrs2.MoveNext()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      dcGrs2.MoveLast()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsArtico As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie e colori esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs2ModTCO = False Then Exit Sub

      If grvGrs2.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è gestito per taglia, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs2ModTCO = True Then
        If Not oCleScho.CheckArticotaglie(edArticolo.Text) Then
          Exit Sub
        End If
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSORSCHO"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               edArticolo.Text & "|" & _
               edArticolo.Text & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(grvGrs2.NTSGetCurrentDataRow!mo_fase) & "|" & _
               NTSCStr(grvGrs2.NTSGetCurrentDataRow!mo_fase) & "|" & _
               NTSCStr(grvGrs2.NTSGetCurrentDataRow!ko_tipork) & ";" & _
               Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrs2.NTSGetCurrentDataRow!ko_anno), 4) & ";" & _
               NTSCStr(grvGrs2.NTSGetCurrentDataRow!ko_serie) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrs2.NTSGetCurrentDataRow!ko_numord), 9) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrs2.NTSGetCurrentDataRow!ko_riga), 9) & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdini.ItemClick
    Dim strTipork As String
    Dim nAnno As Integer
    Dim strSerie As String
    Dim lNumdoc As Integer
    Dim strParam As String
    Try
      If grvGrs2.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      strTipork = NTSCStr(grvGrs2.NTSGetCurrentDataRow()!ko_tipork)
      nAnno = NTSCInt(grvGrs2.NTSGetCurrentDataRow()!ko_anno)
      strSerie = NTSCStr(grvGrs2.NTSGetCurrentDataRow()!ko_serie)
      lNumdoc = NTSCInt(grvGrs2.NTSGetCurrentDataRow()!ko_numord)
      '-------------------------------------------------------------------------------------
      strParam = "APRI;" & strTipork & ";" & _
           nAnno.ToString.PadLeft(4, "0"c) & ";" & _
           strSerie & ";" & _
           lNumdoc.ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 130420258219754328, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneDoc.ItemClick
    Dim strParam As String
    Try
      If grvGrs2.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      strParam = "APRI;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork), 1) & ";" & _
          Microsoft.VisualBasic.Right("0000" & NTSCStr(Year(NTSCDate(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!td_datord))), 4) & ";" & _
          NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_serie).PadLeft(1) & ";" & _
          Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_numord), 9) & ";" & _
          "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!td_datord).ToShortDateString, 10) & _
          ";000000000;0000;0000; ;000000000;0000;1"

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 129048516804214621, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub ApriRecordset()
    Try
      ComponiStringa()

      dcGrid.DataSource = dsGrid.Tables("MOVORD")
      dsGrid.AcceptChanges()

      grGrs2.DataSource = dcGrid

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiLabel()
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If dsGrs2.Tables("MOVORD").Rows.Count = 0 Then Exit Sub

      edArticolo.Text = NTSCStr(dsGrs2.Tables("MOVORD").Rows(dcGrs2.Position)!ko_codart)
      If Not oCleScho.Grs1lbArticolo_Validated(edArticolo.Text, strTmp, dttTmp) Then
        lbXx_articolo.Text = ""
      Else
        lbXx_articolo.Text = strTmp & " " & NTSCStr(dttTmp.Rows(0)!ar_desint)
      End If

      If Not NTSCStr(dsGrs2.Tables("MOVORD").Rows(dcGrs2.Position)!mo_ump) = "" Then edUnmis.Text = NTSCStr(dsGrs2.Tables("MOVORD").Rows(dcGrs2.Position)!mo_ump)
      edCommeca.Text = NTSCStr(dsGrs2.Tables("MOVORD").Rows(dcGrs2.Position)!ko_commecap)
      If Not oCleScho.lbCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
        lbXx_commeca.Text = ""
      Else
        lbXx_commeca.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringa()
    Dim ds As DataSet = Nothing
    Try
      oCleScho.Grs2ComponiStringa(dsGrs2.Tables("MOVORD").Rows(dcGrs2.Position), dsGrid)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleScho.Grs2Apri(dsGrs2) Then
        bClose = True
        Return False
      End If

      dcGrs2.DataSource = dsGrs2.Tables("MOVORD")
      dsGrs2.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaFmtPrz(Optional ByVal nCodvalu As Integer = 0) As String
    Dim i As Integer
    Try
      TrovaFmtPrz = "#,##0"
      If nCodvalu = 0 Then
        If oApp.NDecPrzUn > 0 Then TrovaFmtPrz = TrovaFmtPrz & "."
        For i = 1 To oApp.NDecPrzUn
          TrovaFmtPrz = TrovaFmtPrz & "0"
        Next
      Else
        If oApp.NDecPrzUnVal > 0 Then
          TrovaFmtPrz = TrovaFmtPrz & "."
          For i = 1 To oApp.NDecPrzUnVal
            TrovaFmtPrz = TrovaFmtPrz & "0"
          Next
        End If
      End If

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Sub StampaGriglia(ByVal nDestinazione As Integer)
    '----------------------------------------------------------------------------------------------------------------
    '--- nDestinazione:
    '----- 0 --> video
    '----- 1 --> stampante
    '----------------------------------------------------------------------------------------------------------------
    Dim strCaption As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCaption = "COMMESSA:  " & edCommeca.Text & " " & lbXx_commeca.Text.ToUpper & vbCrLf & _
        "   ARTICOLO: " & edArticolo.Text & " " & lbXx_articolo.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      If grGrs2.IsPrintingAvailable Then
        grvGrs2.OptionsPrint.AutoWidth = False
        grvGrs2.OptionsPrint.UsePrintStyles = True
        grvGrs2.OptionsPrint.EnableAppearanceEvenRow = True
        grvGrs2.AppearancePrint.EvenRow.BackColor = Color.White
        Dim PrintingSystem1 As New DevExpress.XtraPrinting.PrintingSystem
        Dim PrintableComponentLink1 As New DevExpress.XtraPrinting.PrintableComponentLink
        PrintingSystem1.Links.AddRange(New Object() {PrintableComponentLink1})
        PrintableComponentLink1.Component = grvGrs2.GridControl
        PrintableComponentLink1.PageHeaderFooter = New DevExpress.XtraPrinting.PageHeaderFooter(New DevExpress.XtraPrinting.PageHeaderArea(New String() {strCaption, "", DateTime.Now.ToShortDateString}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near), New DevExpress.XtraPrinting.PageFooterArea(New String() {"", "", "[Page # of Pages #]"}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near))
        PrintableComponentLink1.Margins = New System.Drawing.Printing.Margins(20, 20, 70, 30)
        PrintableComponentLink1.Landscape = True
        PrintableComponentLink1.PaperKind = Printing.PaperKind.A4
        PrintableComponentLink1.PrintingSystem = PrintingSystem1
        PrintableComponentLink1.CreateDocument()
        '------------------------------------------------------------------------------------------------------------
        Select Case nDestinazione
          Case 0 '--- VIDEO
            PrintableComponentLink1.ShowPreview()
          Case 1 '--- STAMPANTE
            Dim pd As New System.Drawing.Printing.PrintDocument()
            PrintableComponentLink1.Print(pd.PrinterSettings.PrinterName)
            pd.Dispose()
        End Select
        '------------------------------------------------------------------------------------------------------------
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 130420257903033551, "Stampa griglia non abilitata. File DevExpress.XtraPrinting non trovato"))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grGrs2_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grGrs2.MouseDoubleClick
    Try
      If tlbOrdini.Enabled And tlbOrdini.Visible Then tlbOrdini_ItemClick(tlbOrdini, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
