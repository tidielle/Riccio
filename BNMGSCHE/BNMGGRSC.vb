Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRSC
  Public ocleSche As CLEMGSCHE
  Public oCallParams As CLE__CLDP
  Public dsGrsc As DataSet
  Public dcGrsc As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bOnLoading As Boolean = False
  Public bNoModal As Boolean = False

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrsc As NTSInformatica.NTSGrid
  Public WithEvents grvGrsc As NTSInformatica.NTSGridView
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbConto As NTSInformatica.NTSLabel

  'per compatibilita con la store procedure
  Public WithEvents km_aammgg As NTSInformatica.NTSGridColumn
  Public WithEvents km_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents km_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents km_serie As NTSInformatica.NTSGridColumn
  Public WithEvents km_causale As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descaum As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents desconto As NTSInformatica.NTSGridColumn
  Public WithEvents carichi As NTSInformatica.NTSGridColumn
  Public WithEvents scarichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_disp As NTSInformatica.NTSGridColumn
  Public WithEvents prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_valore As NTSInformatica.NTSGridColumn
  Public WithEvents mm_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prelist As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents km_anno As NTSInformatica.NTSGridColumn
  Public WithEvents mm_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_controp As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codnomc As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents valorecsa As NTSInformatica.NTSGridColumn
  Public WithEvents valoreucsa As NTSInformatica.NTSGridColumn
  Public WithEvents speacc As NTSInformatica.NTSGridColumn
  Public WithEvents speaccun As NTSInformatica.NTSGridColumn
  Public WithEvents desport As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents mm_datini As NTSInformatica.NTSGridColumn
  Public WithEvents mm_datfin As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant As NTSInformatica.NTSGridColumn
  Public WithEvents km_riga As NTSInformatica.NTSGridColumn
  Public WithEvents tm_scorpo As NTSInformatica.NTSGridColumn
  Public WithEvents tm_valuta As NTSInformatica.NTSGridColumn

  Public WithEvents lbXx_conto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbEsistpr As NTSInformatica.NTSLabel
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbNavigazioneDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem

  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAbilitaModificheDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEsistpr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbDocumenti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCollaudi As NTSInformatica.NTSBarButtonItem
  Public WithEvents co_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents edTotvscarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotvscarichi As NTSInformatica.NTSLabel
  Public WithEvents edTotscarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotscarichi As NTSInformatica.NTSLabel
  Public WithEvents edTotvcarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotvcarichi As NTSInformatica.NTSLabel
  Public WithEvents edTotcarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotcarichi As NTSInformatica.NTSLabel
  Public WithEvents edRettificheVen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRettificheVen As NTSInformatica.NTSLabel
  Public WithEvents edRettificheAcq As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRettificheAcq As NTSInformatica.NTSLabel
  Public WithEvents edEsistfi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEsistfi As NTSInformatica.NTSLabel
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
#End Region

#Region "Inizializzazione"
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

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGRSC))
    Me.grGrsc = New NTSInformatica.NTSGrid
    Me.grvGrsc = New NTSInformatica.NTSGridView
    Me.mm_stasino = New NTSInformatica.NTSGridColumn
    Me.km_aammgg = New NTSInformatica.NTSGridColumn
    Me.km_tipork = New NTSInformatica.NTSGridColumn
    Me.km_numdoc = New NTSInformatica.NTSGridColumn
    Me.km_serie = New NTSInformatica.NTSGridColumn
    Me.km_causale = New NTSInformatica.NTSGridColumn
    Me.tb_descaum = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.desconto = New NTSInformatica.NTSGridColumn
    Me.carichi = New NTSInformatica.NTSGridColumn
    Me.scarichi = New NTSInformatica.NTSGridColumn
    Me.xx_disp = New NTSInformatica.NTSGridColumn
    Me.prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_valore = New NTSInformatica.NTSGridColumn
    Me.mm_ornum = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.mm_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_scont1 = New NTSInformatica.NTSGridColumn
    Me.mm_scont2 = New NTSInformatica.NTSGridColumn
    Me.mm_scont3 = New NTSInformatica.NTSGridColumn
    Me.mm_scont4 = New NTSInformatica.NTSGridColumn
    Me.mm_scont5 = New NTSInformatica.NTSGridColumn
    Me.mm_scont6 = New NTSInformatica.NTSGridColumn
    Me.mm_prelist = New NTSInformatica.NTSGridColumn
    Me.mm_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mm_colli = New NTSInformatica.NTSGridColumn
    Me.mm_misura1 = New NTSInformatica.NTSGridColumn
    Me.mm_misura2 = New NTSInformatica.NTSGridColumn
    Me.mm_misura3 = New NTSInformatica.NTSGridColumn
    Me.mm_controp = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_codcena = New NTSInformatica.NTSGridColumn
    Me.mm_codcfam = New NTSInformatica.NTSGridColumn
    Me.mm_codnomc = New NTSInformatica.NTSGridColumn
    Me.mm_provv = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv = New NTSInformatica.NTSGridColumn
    Me.mm_provv2 = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.mm_perqta = New NTSInformatica.NTSGridColumn
    Me.valorecsa = New NTSInformatica.NTSGridColumn
    Me.valoreucsa = New NTSInformatica.NTSGridColumn
    Me.speacc = New NTSInformatica.NTSGridColumn
    Me.speaccun = New NTSInformatica.NTSGridColumn
    Me.desport = New NTSInformatica.NTSGridColumn
    Me.km_anno = New NTSInformatica.NTSGridColumn
    Me.co_descr1 = New NTSInformatica.NTSGridColumn
    Me.mm_codiva = New NTSInformatica.NTSGridColumn
    Me.mm_datini = New NTSInformatica.NTSGridColumn
    Me.mm_datfin = New NTSInformatica.NTSGridColumn
    Me.mm_quant = New NTSInformatica.NTSGridColumn
    Me.km_riga = New NTSInformatica.NTSGridColumn
    Me.tm_scorpo = New NTSInformatica.NTSGridColumn
    Me.tm_valuta = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbData = New NTSInformatica.NTSLabel
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edEsistpr = New NTSInformatica.NTSTextBoxNum
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbEsistpr = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbXx_conto = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.edTotvscarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotvscarichi = New NTSInformatica.NTSLabel
    Me.edTotscarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotscarichi = New NTSInformatica.NTSLabel
    Me.edTotvcarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotvcarichi = New NTSInformatica.NTSLabel
    Me.edTotcarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotcarichi = New NTSInformatica.NTSLabel
    Me.edRettificheVen = New NTSInformatica.NTSTextBoxNum
    Me.lbRettificheVen = New NTSInformatica.NTSLabel
    Me.edRettificheAcq = New NTSInformatica.NTSTextBoxNum
    Me.lbRettificheAcq = New NTSInformatica.NTSLabel
    Me.edEsistfi = New NTSInformatica.NTSTextBoxNum
    Me.lbEsistfi = New NTSInformatica.NTSLabel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbDocumenti = New NTSInformatica.NTSBarButtonItem
    Me.tlbCollaudi = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbAbilitaModificheDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrsc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrsc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsistpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotvscarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotscarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotvcarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotcarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRettificheVen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRettificheAcq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsistfi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
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
    'grGrsc
    '
    Me.grGrsc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrsc.EmbeddedNavigator.Name = ""
    Me.grGrsc.Location = New System.Drawing.Point(0, 0)
    Me.grGrsc.MainView = Me.grvGrsc
    Me.grGrsc.Name = "grGrsc"
    Me.grGrsc.Size = New System.Drawing.Size(660, 264)
    Me.grGrsc.TabIndex = 5
    Me.grGrsc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrsc})
    '
    'grvGrsc
    '
    Me.grvGrsc.ActiveFilterEnabled = False
    '
    'mm_stasino
    '
    Me.mm_stasino.AppearanceCell.Options.UseBackColor = True
    Me.mm_stasino.AppearanceCell.Options.UseTextOptions = True
    Me.mm_stasino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_stasino.Caption = "Stampa Riga"
    Me.mm_stasino.Enabled = True
    Me.mm_stasino.FieldName = "mm_stasino"
    Me.mm_stasino.Name = "mm_stasino"
    Me.mm_stasino.NTSRepositoryComboBox = Nothing
    Me.mm_stasino.NTSRepositoryItemCheck = Nothing
    Me.mm_stasino.NTSRepositoryItemMemo = Nothing
    Me.mm_stasino.NTSRepositoryItemText = Nothing
    Me.mm_stasino.Visible = True
    Me.mm_stasino.VisibleIndex = 41
    Me.grvGrsc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.km_aammgg, Me.km_tipork, Me.km_numdoc, Me.km_serie, Me.km_causale, Me.tb_descaum, Me.tm_riferim, Me.desconto, Me.carichi, Me.scarichi, Me.xx_disp, Me.prezzo, Me.mm_valore, Me.mm_ornum, Me.xx_lottox, Me.mm_prezzo, Me.mm_scont1, Me.mm_scont2, Me.mm_scont3, Me.mm_scont4, Me.mm_scont5, Me.mm_scont6, Me.mm_prelist, Me.mm_prezvalc, Me.mm_colli, Me.mm_misura1, Me.mm_misura2, Me.mm_misura3, Me.mm_controp, Me.mm_commeca, Me.mm_codcena, Me.mm_codcfam, Me.mm_codnomc, Me.mm_provv, Me.mm_vprovv, Me.mm_provv2, Me.mm_vprovv2, Me.mm_perqta, Me.valorecsa, Me.valoreucsa, Me.speacc, Me.speaccun, Me.desport, Me.km_anno, Me.co_descr1, Me.mm_codiva, Me.mm_datini, Me.mm_datfin, Me.mm_quant, Me.km_riga, Me.tm_scorpo, Me.tm_valuta, Me.mm_stasino})
    Me.grvGrsc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrsc.Enabled = True
    Me.grvGrsc.GridControl = Me.grGrsc
    Me.grvGrsc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrsc.MinRowHeight = 14
    Me.grvGrsc.Name = "grvGrsc"
    Me.grvGrsc.NTSAllowDelete = True
    Me.grvGrsc.NTSAllowInsert = True
    Me.grvGrsc.NTSAllowUpdate = True
    Me.grvGrsc.NTSMenuContext = Nothing
    Me.grvGrsc.OptionsCustomization.AllowRowSizing = True
    Me.grvGrsc.OptionsFilter.AllowFilterEditor = False
    Me.grvGrsc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrsc.OptionsNavigation.UseTabKey = False
    Me.grvGrsc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrsc.OptionsView.ColumnAutoWidth = False
    Me.grvGrsc.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrsc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrsc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrsc.OptionsView.ShowGroupPanel = False
    Me.grvGrsc.RowHeight = 16
    '
    'km_aammgg
    '
    Me.km_aammgg.AppearanceCell.Options.UseBackColor = True
    Me.km_aammgg.AppearanceCell.Options.UseTextOptions = True
    Me.km_aammgg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_aammgg.Caption = "Data"
    Me.km_aammgg.Enabled = True
    Me.km_aammgg.FieldName = "km_aammgg"
    Me.km_aammgg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_aammgg.Name = "km_aammgg"
    Me.km_aammgg.NTSRepositoryComboBox = Nothing
    Me.km_aammgg.NTSRepositoryItemCheck = Nothing
    Me.km_aammgg.NTSRepositoryItemMemo = Nothing
    Me.km_aammgg.NTSRepositoryItemText = Nothing
    Me.km_aammgg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_aammgg.OptionsFilter.AllowFilter = False
    Me.km_aammgg.Visible = True
    Me.km_aammgg.VisibleIndex = 0
    '
    'km_tipork
    '
    Me.km_tipork.AppearanceCell.Options.UseBackColor = True
    Me.km_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.km_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_tipork.Caption = "Tipo"
    Me.km_tipork.Enabled = True
    Me.km_tipork.FieldName = "km_tipork"
    Me.km_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_tipork.Name = "km_tipork"
    Me.km_tipork.NTSRepositoryComboBox = Nothing
    Me.km_tipork.NTSRepositoryItemCheck = Nothing
    Me.km_tipork.NTSRepositoryItemMemo = Nothing
    Me.km_tipork.NTSRepositoryItemText = Nothing
    Me.km_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_tipork.OptionsFilter.AllowFilter = False
    Me.km_tipork.Visible = True
    Me.km_tipork.VisibleIndex = 1
    '
    'km_numdoc
    '
    Me.km_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.km_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.km_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_numdoc.Caption = "Numero"
    Me.km_numdoc.Enabled = True
    Me.km_numdoc.FieldName = "km_numdoc"
    Me.km_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_numdoc.Name = "km_numdoc"
    Me.km_numdoc.NTSRepositoryComboBox = Nothing
    Me.km_numdoc.NTSRepositoryItemCheck = Nothing
    Me.km_numdoc.NTSRepositoryItemMemo = Nothing
    Me.km_numdoc.NTSRepositoryItemText = Nothing
    Me.km_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_numdoc.OptionsFilter.AllowFilter = False
    Me.km_numdoc.Visible = True
    Me.km_numdoc.VisibleIndex = 2
    '
    'km_serie
    '
    Me.km_serie.AppearanceCell.Options.UseBackColor = True
    Me.km_serie.AppearanceCell.Options.UseTextOptions = True
    Me.km_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_serie.Caption = "Serie"
    Me.km_serie.Enabled = True
    Me.km_serie.FieldName = "km_serie"
    Me.km_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_serie.Name = "km_serie"
    Me.km_serie.NTSRepositoryComboBox = Nothing
    Me.km_serie.NTSRepositoryItemCheck = Nothing
    Me.km_serie.NTSRepositoryItemMemo = Nothing
    Me.km_serie.NTSRepositoryItemText = Nothing
    Me.km_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_serie.OptionsFilter.AllowFilter = False
    Me.km_serie.Visible = True
    Me.km_serie.VisibleIndex = 3
    '
    'km_causale
    '
    Me.km_causale.AppearanceCell.Options.UseBackColor = True
    Me.km_causale.AppearanceCell.Options.UseTextOptions = True
    Me.km_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_causale.Caption = "Caus."
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
    Me.km_causale.VisibleIndex = 4
    '
    'tb_descaum
    '
    Me.tb_descaum.AppearanceCell.Options.UseBackColor = True
    Me.tb_descaum.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descaum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descaum.Caption = "Descr."
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
    Me.tb_descaum.VisibleIndex = 5
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Rifer."
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
    Me.tm_riferim.VisibleIndex = 6
    '
    'desconto
    '
    Me.desconto.AppearanceCell.Options.UseBackColor = True
    Me.desconto.AppearanceCell.Options.UseTextOptions = True
    Me.desconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.desconto.Caption = "Cliente/Forn."
    Me.desconto.Enabled = True
    Me.desconto.FieldName = "desconto"
    Me.desconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.desconto.Name = "desconto"
    Me.desconto.NTSRepositoryComboBox = Nothing
    Me.desconto.NTSRepositoryItemCheck = Nothing
    Me.desconto.NTSRepositoryItemMemo = Nothing
    Me.desconto.NTSRepositoryItemText = Nothing
    Me.desconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.desconto.OptionsFilter.AllowFilter = False
    Me.desconto.Visible = True
    Me.desconto.VisibleIndex = 7
    '
    'carichi
    '
    Me.carichi.AppearanceCell.Options.UseBackColor = True
    Me.carichi.AppearanceCell.Options.UseTextOptions = True
    Me.carichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.carichi.Caption = "Carichi"
    Me.carichi.Enabled = True
    Me.carichi.FieldName = "carichi"
    Me.carichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.carichi.Name = "carichi"
    Me.carichi.NTSRepositoryComboBox = Nothing
    Me.carichi.NTSRepositoryItemCheck = Nothing
    Me.carichi.NTSRepositoryItemMemo = Nothing
    Me.carichi.NTSRepositoryItemText = Nothing
    Me.carichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.carichi.OptionsFilter.AllowFilter = False
    Me.carichi.Visible = True
    Me.carichi.VisibleIndex = 8
    '
    'scarichi
    '
    Me.scarichi.AppearanceCell.Options.UseBackColor = True
    Me.scarichi.AppearanceCell.Options.UseTextOptions = True
    Me.scarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.scarichi.Caption = "Scarichi"
    Me.scarichi.Enabled = True
    Me.scarichi.FieldName = "scarichi"
    Me.scarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.scarichi.Name = "scarichi"
    Me.scarichi.NTSRepositoryComboBox = Nothing
    Me.scarichi.NTSRepositoryItemCheck = Nothing
    Me.scarichi.NTSRepositoryItemMemo = Nothing
    Me.scarichi.NTSRepositoryItemText = Nothing
    Me.scarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.scarichi.OptionsFilter.AllowFilter = False
    Me.scarichi.Visible = True
    Me.scarichi.VisibleIndex = 9
    '
    'xx_disp
    '
    Me.xx_disp.AppearanceCell.Options.UseBackColor = True
    Me.xx_disp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disp.Caption = "Esistenza"
    Me.xx_disp.Enabled = True
    Me.xx_disp.FieldName = "xx_disp"
    Me.xx_disp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disp.Name = "xx_disp"
    Me.xx_disp.NTSRepositoryComboBox = Nothing
    Me.xx_disp.NTSRepositoryItemCheck = Nothing
    Me.xx_disp.NTSRepositoryItemMemo = Nothing
    Me.xx_disp.NTSRepositoryItemText = Nothing
    Me.xx_disp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disp.OptionsFilter.AllowFilter = False
    Me.xx_disp.Visible = True
    Me.xx_disp.VisibleIndex = 10
    '
    'prezzo
    '
    Me.prezzo.AppearanceCell.Options.UseBackColor = True
    Me.prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.prezzo.Caption = "Prezzo N"
    Me.prezzo.Enabled = True
    Me.prezzo.FieldName = "prezzo"
    Me.prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.prezzo.Name = "prezzo"
    Me.prezzo.NTSRepositoryComboBox = Nothing
    Me.prezzo.NTSRepositoryItemCheck = Nothing
    Me.prezzo.NTSRepositoryItemMemo = Nothing
    Me.prezzo.NTSRepositoryItemText = Nothing
    Me.prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.prezzo.OptionsFilter.AllowFilter = False
    Me.prezzo.Visible = True
    Me.prezzo.VisibleIndex = 11
    '
    'mm_valore
    '
    Me.mm_valore.AppearanceCell.Options.UseBackColor = True
    Me.mm_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mm_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_valore.Caption = "Valore"
    Me.mm_valore.Enabled = True
    Me.mm_valore.FieldName = "mm_valore"
    Me.mm_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_valore.Name = "mm_valore"
    Me.mm_valore.NTSRepositoryComboBox = Nothing
    Me.mm_valore.NTSRepositoryItemCheck = Nothing
    Me.mm_valore.NTSRepositoryItemMemo = Nothing
    Me.mm_valore.NTSRepositoryItemText = Nothing
    Me.mm_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_valore.OptionsFilter.AllowFilter = False
    Me.mm_valore.Visible = True
    Me.mm_valore.VisibleIndex = 12
    '
    'mm_ornum
    '
    Me.mm_ornum.AppearanceCell.Options.UseBackColor = True
    Me.mm_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ornum.Caption = "N.Ord."
    Me.mm_ornum.Enabled = True
    Me.mm_ornum.FieldName = "mm_ornum"
    Me.mm_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ornum.Name = "mm_ornum"
    Me.mm_ornum.NTSRepositoryComboBox = Nothing
    Me.mm_ornum.NTSRepositoryItemCheck = Nothing
    Me.mm_ornum.NTSRepositoryItemMemo = Nothing
    Me.mm_ornum.NTSRepositoryItemText = Nothing
    Me.mm_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ornum.OptionsFilter.AllowFilter = False
    Me.mm_ornum.Visible = True
    Me.mm_ornum.VisibleIndex = 13
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
    Me.xx_lottox.VisibleIndex = 14
    '
    'mm_prezzo
    '
    Me.mm_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezzo.Caption = "Prezzo Lordo"
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
    Me.mm_prezzo.VisibleIndex = 15
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
    Me.mm_scont1.VisibleIndex = 16
    '
    'mm_scont2
    '
    Me.mm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont2.Caption = "Sc. 2"
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
    Me.mm_scont2.VisibleIndex = 17
    '
    'mm_scont3
    '
    Me.mm_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont3.Caption = "Sc. 3"
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
    Me.mm_scont3.VisibleIndex = 18
    '
    'mm_scont4
    '
    Me.mm_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont4.Caption = "Sc. 4"
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
    '
    'mm_scont5
    '
    Me.mm_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont5.Caption = "Sc. 5"
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
    '
    'mm_scont6
    '
    Me.mm_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont6.Caption = "Sc. 6"
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
    '
    'mm_prelist
    '
    Me.mm_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mm_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prelist.Caption = "Prz.List."
    Me.mm_prelist.Enabled = True
    Me.mm_prelist.FieldName = "mm_prelist"
    Me.mm_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prelist.Name = "mm_prelist"
    Me.mm_prelist.NTSRepositoryComboBox = Nothing
    Me.mm_prelist.NTSRepositoryItemCheck = Nothing
    Me.mm_prelist.NTSRepositoryItemMemo = Nothing
    Me.mm_prelist.NTSRepositoryItemText = Nothing
    Me.mm_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prelist.OptionsFilter.AllowFilter = False
    Me.mm_prelist.Visible = True
    Me.mm_prelist.VisibleIndex = 19
    '
    'mm_prezvalc
    '
    Me.mm_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezvalc.Caption = "Prz.Val."
    Me.mm_prezvalc.Enabled = True
    Me.mm_prezvalc.FieldName = "mm_prezvalc"
    Me.mm_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezvalc.Name = "mm_prezvalc"
    Me.mm_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mm_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mm_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mm_prezvalc.NTSRepositoryItemText = Nothing
    Me.mm_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezvalc.OptionsFilter.AllowFilter = False
    Me.mm_prezvalc.Visible = True
    Me.mm_prezvalc.VisibleIndex = 20
    '
    'mm_colli
    '
    Me.mm_colli.AppearanceCell.Options.UseBackColor = True
    Me.mm_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mm_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_colli.Caption = "Colli"
    Me.mm_colli.Enabled = True
    Me.mm_colli.FieldName = "mm_colli"
    Me.mm_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_colli.Name = "mm_colli"
    Me.mm_colli.NTSRepositoryComboBox = Nothing
    Me.mm_colli.NTSRepositoryItemCheck = Nothing
    Me.mm_colli.NTSRepositoryItemMemo = Nothing
    Me.mm_colli.NTSRepositoryItemText = Nothing
    Me.mm_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_colli.OptionsFilter.AllowFilter = False
    Me.mm_colli.Visible = True
    Me.mm_colli.VisibleIndex = 21
    '
    'mm_misura1
    '
    Me.mm_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura1.Caption = "Mis.1"
    Me.mm_misura1.Enabled = True
    Me.mm_misura1.FieldName = "mm_misura1"
    Me.mm_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura1.Name = "mm_misura1"
    Me.mm_misura1.NTSRepositoryComboBox = Nothing
    Me.mm_misura1.NTSRepositoryItemCheck = Nothing
    Me.mm_misura1.NTSRepositoryItemMemo = Nothing
    Me.mm_misura1.NTSRepositoryItemText = Nothing
    Me.mm_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura1.OptionsFilter.AllowFilter = False
    '
    'mm_misura2
    '
    Me.mm_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura2.Caption = "Mis.2"
    Me.mm_misura2.Enabled = True
    Me.mm_misura2.FieldName = "mm_misura2"
    Me.mm_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura2.Name = "mm_misura2"
    Me.mm_misura2.NTSRepositoryComboBox = Nothing
    Me.mm_misura2.NTSRepositoryItemCheck = Nothing
    Me.mm_misura2.NTSRepositoryItemMemo = Nothing
    Me.mm_misura2.NTSRepositoryItemText = Nothing
    Me.mm_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura2.OptionsFilter.AllowFilter = False
    '
    'mm_misura3
    '
    Me.mm_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura3.Caption = "Mis.3"
    Me.mm_misura3.Enabled = True
    Me.mm_misura3.FieldName = "mm_misura3"
    Me.mm_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura3.Name = "mm_misura3"
    Me.mm_misura3.NTSRepositoryComboBox = Nothing
    Me.mm_misura3.NTSRepositoryItemCheck = Nothing
    Me.mm_misura3.NTSRepositoryItemMemo = Nothing
    Me.mm_misura3.NTSRepositoryItemText = Nothing
    Me.mm_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura3.OptionsFilter.AllowFilter = False
    '
    'mm_controp
    '
    Me.mm_controp.AppearanceCell.Options.UseBackColor = True
    Me.mm_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mm_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_controp.Caption = "Controp."
    Me.mm_controp.Enabled = True
    Me.mm_controp.FieldName = "mm_controp"
    Me.mm_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_controp.Name = "mm_controp"
    Me.mm_controp.NTSRepositoryComboBox = Nothing
    Me.mm_controp.NTSRepositoryItemCheck = Nothing
    Me.mm_controp.NTSRepositoryItemMemo = Nothing
    Me.mm_controp.NTSRepositoryItemText = Nothing
    Me.mm_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_controp.OptionsFilter.AllowFilter = False
    Me.mm_controp.Visible = True
    Me.mm_controp.VisibleIndex = 22
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
    Me.mm_commeca.VisibleIndex = 23
    '
    'mm_codcena
    '
    Me.mm_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcena.Caption = "C.Centro"
    Me.mm_codcena.Enabled = True
    Me.mm_codcena.FieldName = "mm_codcena"
    Me.mm_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcena.Name = "mm_codcena"
    Me.mm_codcena.NTSRepositoryComboBox = Nothing
    Me.mm_codcena.NTSRepositoryItemCheck = Nothing
    Me.mm_codcena.NTSRepositoryItemMemo = Nothing
    Me.mm_codcena.NTSRepositoryItemText = Nothing
    Me.mm_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcena.OptionsFilter.AllowFilter = False
    Me.mm_codcena.Visible = True
    Me.mm_codcena.VisibleIndex = 24
    '
    'mm_codcfam
    '
    Me.mm_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcfam.Caption = "Linea"
    Me.mm_codcfam.Enabled = True
    Me.mm_codcfam.FieldName = "mm_codcfam"
    Me.mm_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcfam.Name = "mm_codcfam"
    Me.mm_codcfam.NTSRepositoryComboBox = Nothing
    Me.mm_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mm_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mm_codcfam.NTSRepositoryItemText = Nothing
    Me.mm_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcfam.OptionsFilter.AllowFilter = False
    Me.mm_codcfam.Visible = True
    Me.mm_codcfam.VisibleIndex = 25
    '
    'mm_codnomc
    '
    Me.mm_codnomc.AppearanceCell.Options.UseBackColor = True
    Me.mm_codnomc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codnomc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codnomc.Caption = "Nom.Comb."
    Me.mm_codnomc.Enabled = True
    Me.mm_codnomc.FieldName = "mm_codnomc"
    Me.mm_codnomc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codnomc.Name = "mm_codnomc"
    Me.mm_codnomc.NTSRepositoryComboBox = Nothing
    Me.mm_codnomc.NTSRepositoryItemCheck = Nothing
    Me.mm_codnomc.NTSRepositoryItemMemo = Nothing
    Me.mm_codnomc.NTSRepositoryItemText = Nothing
    Me.mm_codnomc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codnomc.OptionsFilter.AllowFilter = False
    Me.mm_codnomc.Visible = True
    Me.mm_codnomc.VisibleIndex = 26
    '
    'mm_provv
    '
    Me.mm_provv.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv.Caption = "Provv.1"
    Me.mm_provv.Enabled = True
    Me.mm_provv.FieldName = "mm_provv"
    Me.mm_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv.Name = "mm_provv"
    Me.mm_provv.NTSRepositoryComboBox = Nothing
    Me.mm_provv.NTSRepositoryItemCheck = Nothing
    Me.mm_provv.NTSRepositoryItemMemo = Nothing
    Me.mm_provv.NTSRepositoryItemText = Nothing
    Me.mm_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv.OptionsFilter.AllowFilter = False
    Me.mm_provv.Visible = True
    Me.mm_provv.VisibleIndex = 27
    '
    'mm_vprovv
    '
    Me.mm_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv.Caption = "Val.Pr.1"
    Me.mm_vprovv.Enabled = True
    Me.mm_vprovv.FieldName = "mm_vprovv"
    Me.mm_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv.Name = "mm_vprovv"
    Me.mm_vprovv.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv.NTSRepositoryItemText = Nothing
    Me.mm_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv.OptionsFilter.AllowFilter = False
    Me.mm_vprovv.Visible = True
    Me.mm_vprovv.VisibleIndex = 28
    '
    'mm_provv2
    '
    Me.mm_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv2.Caption = "Provv.2"
    Me.mm_provv2.Enabled = True
    Me.mm_provv2.FieldName = "mm_provv2"
    Me.mm_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv2.Name = "mm_provv2"
    Me.mm_provv2.NTSRepositoryComboBox = Nothing
    Me.mm_provv2.NTSRepositoryItemCheck = Nothing
    Me.mm_provv2.NTSRepositoryItemMemo = Nothing
    Me.mm_provv2.NTSRepositoryItemText = Nothing
    Me.mm_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv2.OptionsFilter.AllowFilter = False
    Me.mm_provv2.Visible = True
    Me.mm_provv2.VisibleIndex = 29
    '
    'mm_vprovv2
    '
    Me.mm_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv2.Caption = "Val.Pr.2"
    Me.mm_vprovv2.Enabled = True
    Me.mm_vprovv2.FieldName = "mm_vprovv2"
    Me.mm_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv2.Name = "mm_vprovv2"
    Me.mm_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv2.NTSRepositoryItemText = Nothing
    Me.mm_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv2.OptionsFilter.AllowFilter = False
    Me.mm_vprovv2.Visible = True
    Me.mm_vprovv2.VisibleIndex = 30
    '
    'mm_perqta
    '
    Me.mm_perqta.AppearanceCell.Options.UseBackColor = True
    Me.mm_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_perqta.Caption = "Molt.qtà/prezzo"
    Me.mm_perqta.Enabled = True
    Me.mm_perqta.FieldName = "mm_perqta"
    Me.mm_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_perqta.Name = "mm_perqta"
    Me.mm_perqta.NTSRepositoryComboBox = Nothing
    Me.mm_perqta.NTSRepositoryItemCheck = Nothing
    Me.mm_perqta.NTSRepositoryItemMemo = Nothing
    Me.mm_perqta.NTSRepositoryItemText = Nothing
    Me.mm_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_perqta.OptionsFilter.AllowFilter = False
    Me.mm_perqta.Visible = True
    Me.mm_perqta.VisibleIndex = 31
    '
    'valorecsa
    '
    Me.valorecsa.AppearanceCell.Options.UseBackColor = True
    Me.valorecsa.AppearanceCell.Options.UseTextOptions = True
    Me.valorecsa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.valorecsa.Caption = "Valore C.S.A."
    Me.valorecsa.Enabled = True
    Me.valorecsa.FieldName = "valorecsa"
    Me.valorecsa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.valorecsa.Name = "valorecsa"
    Me.valorecsa.NTSRepositoryComboBox = Nothing
    Me.valorecsa.NTSRepositoryItemCheck = Nothing
    Me.valorecsa.NTSRepositoryItemMemo = Nothing
    Me.valorecsa.NTSRepositoryItemText = Nothing
    Me.valorecsa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.valorecsa.OptionsFilter.AllowFilter = False
    Me.valorecsa.Visible = True
    Me.valorecsa.VisibleIndex = 32
    '
    'valoreucsa
    '
    Me.valoreucsa.AppearanceCell.Options.UseBackColor = True
    Me.valoreucsa.AppearanceCell.Options.UseTextOptions = True
    Me.valoreucsa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.valoreucsa.Caption = "Valore Un.C.S.A."
    Me.valoreucsa.Enabled = True
    Me.valoreucsa.FieldName = "valoreucsa"
    Me.valoreucsa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.valoreucsa.Name = "valoreucsa"
    Me.valoreucsa.NTSRepositoryComboBox = Nothing
    Me.valoreucsa.NTSRepositoryItemCheck = Nothing
    Me.valoreucsa.NTSRepositoryItemMemo = Nothing
    Me.valoreucsa.NTSRepositoryItemText = Nothing
    Me.valoreucsa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.valoreucsa.OptionsFilter.AllowFilter = False
    Me.valoreucsa.Visible = True
    Me.valoreucsa.VisibleIndex = 35
    '
    'speacc
    '
    Me.speacc.AppearanceCell.Options.UseBackColor = True
    Me.speacc.AppearanceCell.Options.UseTextOptions = True
    Me.speacc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.speacc.Caption = "Spese acc."
    Me.speacc.Enabled = True
    Me.speacc.FieldName = "speacc"
    Me.speacc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.speacc.Name = "speacc"
    Me.speacc.NTSRepositoryComboBox = Nothing
    Me.speacc.NTSRepositoryItemCheck = Nothing
    Me.speacc.NTSRepositoryItemMemo = Nothing
    Me.speacc.NTSRepositoryItemText = Nothing
    Me.speacc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.speacc.OptionsFilter.AllowFilter = False
    Me.speacc.Visible = True
    Me.speacc.VisibleIndex = 36
    '
    'speaccun
    '
    Me.speaccun.AppearanceCell.Options.UseBackColor = True
    Me.speaccun.AppearanceCell.Options.UseTextOptions = True
    Me.speaccun.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.speaccun.Caption = "Spese acc.un."
    Me.speaccun.Enabled = True
    Me.speaccun.FieldName = "speaccun"
    Me.speaccun.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.speaccun.Name = "speaccun"
    Me.speaccun.NTSRepositoryComboBox = Nothing
    Me.speaccun.NTSRepositoryItemCheck = Nothing
    Me.speaccun.NTSRepositoryItemMemo = Nothing
    Me.speaccun.NTSRepositoryItemText = Nothing
    Me.speaccun.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.speaccun.OptionsFilter.AllowFilter = False
    Me.speaccun.Visible = True
    Me.speaccun.VisibleIndex = 37
    '
    'desport
    '
    Me.desport.AppearanceCell.Options.UseBackColor = True
    Me.desport.AppearanceCell.Options.UseTextOptions = True
    Me.desport.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.desport.Caption = "Porto"
    Me.desport.Enabled = True
    Me.desport.FieldName = "desport"
    Me.desport.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.desport.Name = "desport"
    Me.desport.NTSRepositoryComboBox = Nothing
    Me.desport.NTSRepositoryItemCheck = Nothing
    Me.desport.NTSRepositoryItemMemo = Nothing
    Me.desport.NTSRepositoryItemText = Nothing
    Me.desport.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.desport.OptionsFilter.AllowFilter = False
    Me.desport.Visible = True
    Me.desport.VisibleIndex = 33
    '
    'km_anno
    '
    Me.km_anno.AppearanceCell.Options.UseBackColor = True
    Me.km_anno.AppearanceCell.Options.UseTextOptions = True
    Me.km_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_anno.Caption = "KM_ANNO"
    Me.km_anno.Enabled = True
    Me.km_anno.FieldName = "km_anno"
    Me.km_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_anno.Name = "km_anno"
    Me.km_anno.NTSRepositoryComboBox = Nothing
    Me.km_anno.NTSRepositoryItemCheck = Nothing
    Me.km_anno.NTSRepositoryItemMemo = Nothing
    Me.km_anno.NTSRepositoryItemText = Nothing
    Me.km_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_anno.OptionsFilter.AllowFilter = False
    '
    'co_descr1
    '
    Me.co_descr1.AppearanceCell.Options.UseBackColor = True
    Me.co_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.co_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_descr1.Caption = "Descr. commessa"
    Me.co_descr1.Enabled = True
    Me.co_descr1.FieldName = "co_descr1"
    Me.co_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_descr1.Name = "co_descr1"
    Me.co_descr1.NTSRepositoryComboBox = Nothing
    Me.co_descr1.NTSRepositoryItemCheck = Nothing
    Me.co_descr1.NTSRepositoryItemMemo = Nothing
    Me.co_descr1.NTSRepositoryItemText = Nothing
    Me.co_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_descr1.OptionsFilter.AllowFilter = False
    Me.co_descr1.Visible = True
    Me.co_descr1.VisibleIndex = 34
    '
    'mm_codiva
    '
    Me.mm_codiva.AppearanceCell.Options.UseBackColor = True
    Me.mm_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codiva.Caption = "Codice Iva"
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
    Me.mm_codiva.VisibleIndex = 38
    '
    'mm_datini
    '
    Me.mm_datini.AppearanceCell.Options.UseBackColor = True
    Me.mm_datini.AppearanceCell.Options.UseTextOptions = True
    Me.mm_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_datini.Caption = "Data inizio"
    Me.mm_datini.Enabled = True
    Me.mm_datini.FieldName = "mm_datini"
    Me.mm_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_datini.Name = "mm_datini"
    Me.mm_datini.NTSRepositoryComboBox = Nothing
    Me.mm_datini.NTSRepositoryItemCheck = Nothing
    Me.mm_datini.NTSRepositoryItemMemo = Nothing
    Me.mm_datini.NTSRepositoryItemText = Nothing
    Me.mm_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_datini.OptionsFilter.AllowFilter = False
    Me.mm_datini.Visible = True
    Me.mm_datini.VisibleIndex = 39
    '
    'mm_datfin
    '
    Me.mm_datfin.AppearanceCell.Options.UseBackColor = True
    Me.mm_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.mm_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_datfin.Caption = "Data fine"
    Me.mm_datfin.Enabled = True
    Me.mm_datfin.FieldName = "mm_datfin"
    Me.mm_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_datfin.Name = "mm_datfin"
    Me.mm_datfin.NTSRepositoryComboBox = Nothing
    Me.mm_datfin.NTSRepositoryItemCheck = Nothing
    Me.mm_datfin.NTSRepositoryItemMemo = Nothing
    Me.mm_datfin.NTSRepositoryItemText = Nothing
    Me.mm_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_datfin.OptionsFilter.AllowFilter = False
    Me.mm_datfin.Visible = True
    Me.mm_datfin.VisibleIndex = 40
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
    '
    'km_riga
    '
    Me.km_riga.AppearanceCell.Options.UseBackColor = True
    Me.km_riga.AppearanceCell.Options.UseTextOptions = True
    Me.km_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_riga.Caption = "Riga"
    Me.km_riga.Enabled = False
    Me.km_riga.FieldName = "km_riga"
    Me.km_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_riga.Name = "km_riga"
    Me.km_riga.NTSRepositoryComboBox = Nothing
    Me.km_riga.NTSRepositoryItemCheck = Nothing
    Me.km_riga.NTSRepositoryItemMemo = Nothing
    Me.km_riga.NTSRepositoryItemText = Nothing
    Me.km_riga.OptionsColumn.AllowEdit = False
    Me.km_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_riga.OptionsColumn.ReadOnly = True
    Me.km_riga.OptionsFilter.AllowFilter = False
    '
    'tm_scorpo
    '
    Me.tm_scorpo.AppearanceCell.Options.UseBackColor = True
    Me.tm_scorpo.AppearanceCell.Options.UseTextOptions = True
    Me.tm_scorpo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_scorpo.Caption = "Scorporo"
    Me.tm_scorpo.Enabled = False
    Me.tm_scorpo.FieldName = "tm_scorpo"
    Me.tm_scorpo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_scorpo.Name = "tm_scorpo"
    Me.tm_scorpo.NTSRepositoryComboBox = Nothing
    Me.tm_scorpo.NTSRepositoryItemCheck = Nothing
    Me.tm_scorpo.NTSRepositoryItemMemo = Nothing
    Me.tm_scorpo.NTSRepositoryItemText = Nothing
    Me.tm_scorpo.OptionsColumn.AllowEdit = False
    Me.tm_scorpo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_scorpo.OptionsColumn.ReadOnly = True
    Me.tm_scorpo.OptionsFilter.AllowFilter = False
    '
    'tm_valuta
    '
    Me.tm_valuta.AppearanceCell.Options.UseBackColor = True
    Me.tm_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.tm_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_valuta.Caption = "Valuta"
    Me.tm_valuta.Enabled = False
    Me.tm_valuta.FieldName = "tm_valuta"
    Me.tm_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_valuta.Name = "tm_valuta"
    Me.tm_valuta.NTSRepositoryComboBox = Nothing
    Me.tm_valuta.NTSRepositoryItemCheck = Nothing
    Me.tm_valuta.NTSRepositoryItemMemo = Nothing
    Me.tm_valuta.NTSRepositoryItemText = Nothing
    Me.tm_valuta.OptionsColumn.AllowEdit = False
    Me.tm_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_valuta.OptionsColumn.ReadOnly = True
    Me.tm_valuta.OptionsFilter.AllowFilter = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbData)
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.edEsistpr)
    Me.pnTop.Controls.Add(Me.edConto)
    Me.pnTop.Controls.Add(Me.lbEsistpr)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Controls.Add(Me.lbXx_conto)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 63)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbData
    '
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbData.Location = New System.Drawing.Point(456, 8)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(193, 20)
    Me.lbData.TabIndex = 47
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.Location = New System.Drawing.Point(456, 34)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(53, 20)
    Me.edMagaz.TabIndex = 31
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(352, 32)
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
    Me.edFase.Size = New System.Drawing.Size(57, 20)
    Me.edFase.TabIndex = 29
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(62, 6)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.AutoHeight = False
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(131, 20)
    Me.edArticolo.TabIndex = 26
    '
    'edEsistpr
    '
    Me.edEsistpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsistpr.Location = New System.Drawing.Point(573, 35)
    Me.edEsistpr.Name = "edEsistpr"
    Me.edEsistpr.NTSDbField = ""
    Me.edEsistpr.NTSFormat = "0"
    Me.edEsistpr.NTSForzaVisZoom = False
    Me.edEsistpr.NTSOldValue = ""
    Me.edEsistpr.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsistpr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsistpr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsistpr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsistpr.Properties.AutoHeight = False
    Me.edEsistpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsistpr.Properties.MaxLength = 65536
    Me.edEsistpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsistpr.Size = New System.Drawing.Size(75, 20)
    Me.edEsistpr.TabIndex = 23
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(62, 30)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(74, 20)
    Me.edConto.TabIndex = 22
    '
    'lbEsistpr
    '
    Me.lbEsistpr.AutoSize = True
    Me.lbEsistpr.BackColor = System.Drawing.Color.Transparent
    Me.lbEsistpr.Location = New System.Drawing.Point(515, 37)
    Me.lbEsistpr.Name = "lbEsistpr"
    Me.lbEsistpr.NTSDbField = ""
    Me.lbEsistpr.Size = New System.Drawing.Size(54, 13)
    Me.lbEsistpr.TabIndex = 20
    Me.lbEsistpr.Text = "Esist.Prec"
    Me.lbEsistpr.Tooltip = ""
    Me.lbEsistpr.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(312, 35)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(34, 13)
    Me.lbFase.TabIndex = 18
    Me.lbFase.Text = "Fase:"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(415, 37)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(35, 13)
    Me.lbMagaz.TabIndex = 6
    Me.lbMagaz.Text = "Mag.:"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbXx_articolo
    '
    Me.lbXx_articolo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_articolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_articolo.Location = New System.Drawing.Point(199, 8)
    Me.lbXx_articolo.Name = "lbXx_articolo"
    Me.lbXx_articolo.NTSDbField = ""
    Me.lbXx_articolo.Size = New System.Drawing.Size(251, 20)
    Me.lbXx_articolo.TabIndex = 27
    Me.lbXx_articolo.Tooltip = ""
    Me.lbXx_articolo.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(10, 9)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 3
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'lbXx_conto
    '
    Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conto.Location = New System.Drawing.Point(142, 32)
    Me.lbXx_conto.Name = "lbXx_conto"
    Me.lbXx_conto.NTSDbField = ""
    Me.lbXx_conto.Size = New System.Drawing.Size(164, 20)
    Me.lbXx_conto.TabIndex = 2
    Me.lbXx_conto.Tooltip = ""
    Me.lbXx_conto.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(10, 33)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(36, 13)
    Me.lbConto.TabIndex = 0
    Me.lbConto.Text = "Conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(12, 6)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(62, 18)
    Me.cmdOk.TabIndex = 46
    Me.cmdOk.Text = "Seleziona"
    Me.cmdOk.Visible = False
    '
    'edTotvscarichi
    '
    Me.edTotvscarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotvscarichi.Location = New System.Drawing.Point(568, 53)
    Me.edTotvscarichi.Name = "edTotvscarichi"
    Me.edTotvscarichi.NTSDbField = ""
    Me.edTotvscarichi.NTSFormat = "0"
    Me.edTotvscarichi.NTSForzaVisZoom = False
    Me.edTotvscarichi.NTSOldValue = ""
    Me.edTotvscarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotvscarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotvscarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotvscarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotvscarichi.Properties.AutoHeight = False
    Me.edTotvscarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotvscarichi.Properties.MaxLength = 65536
    Me.edTotvscarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotvscarichi.Size = New System.Drawing.Size(80, 20)
    Me.edTotvscarichi.TabIndex = 45
    '
    'lbTotvscarichi
    '
    Me.lbTotvscarichi.AutoSize = True
    Me.lbTotvscarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotvscarichi.Location = New System.Drawing.Point(494, 56)
    Me.lbTotvscarichi.Name = "lbTotvscarichi"
    Me.lbTotvscarichi.NTSDbField = ""
    Me.lbTotvscarichi.Size = New System.Drawing.Size(61, 13)
    Me.lbTotvscarichi.TabIndex = 44
    Me.lbTotvscarichi.Text = "Val.Scarichi"
    Me.lbTotvscarichi.Tooltip = ""
    Me.lbTotvscarichi.UseMnemonic = False
    '
    'edTotscarichi
    '
    Me.edTotscarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotscarichi.Location = New System.Drawing.Point(568, 27)
    Me.edTotscarichi.Name = "edTotscarichi"
    Me.edTotscarichi.NTSDbField = ""
    Me.edTotscarichi.NTSFormat = "0"
    Me.edTotscarichi.NTSForzaVisZoom = False
    Me.edTotscarichi.NTSOldValue = ""
    Me.edTotscarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotscarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotscarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotscarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotscarichi.Properties.AutoHeight = False
    Me.edTotscarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotscarichi.Properties.MaxLength = 65536
    Me.edTotscarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotscarichi.Size = New System.Drawing.Size(80, 20)
    Me.edTotscarichi.TabIndex = 43
    '
    'lbTotscarichi
    '
    Me.lbTotscarichi.AutoSize = True
    Me.lbTotscarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotscarichi.Location = New System.Drawing.Point(492, 30)
    Me.lbTotscarichi.Name = "lbTotscarichi"
    Me.lbTotscarichi.NTSDbField = ""
    Me.lbTotscarichi.Size = New System.Drawing.Size(62, 13)
    Me.lbTotscarichi.TabIndex = 42
    Me.lbTotscarichi.Text = "Tot Scarichi"
    Me.lbTotscarichi.Tooltip = ""
    Me.lbTotscarichi.UseMnemonic = False
    '
    'edTotvcarichi
    '
    Me.edTotvcarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotvcarichi.Location = New System.Drawing.Point(406, 53)
    Me.edTotvcarichi.Name = "edTotvcarichi"
    Me.edTotvcarichi.NTSDbField = ""
    Me.edTotvcarichi.NTSFormat = "0"
    Me.edTotvcarichi.NTSForzaVisZoom = False
    Me.edTotvcarichi.NTSOldValue = ""
    Me.edTotvcarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotvcarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotvcarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotvcarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotvcarichi.Properties.AutoHeight = False
    Me.edTotvcarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotvcarichi.Properties.MaxLength = 65536
    Me.edTotvcarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotvcarichi.Size = New System.Drawing.Size(80, 20)
    Me.edTotvcarichi.TabIndex = 41
    '
    'lbTotvcarichi
    '
    Me.lbTotvcarichi.AutoSize = True
    Me.lbTotvcarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotvcarichi.Location = New System.Drawing.Point(341, 56)
    Me.lbTotvcarichi.Name = "lbTotvcarichi"
    Me.lbTotvcarichi.NTSDbField = ""
    Me.lbTotvcarichi.Size = New System.Drawing.Size(57, 13)
    Me.lbTotvcarichi.TabIndex = 40
    Me.lbTotvcarichi.Text = "Val.Carichi"
    Me.lbTotvcarichi.Tooltip = ""
    Me.lbTotvcarichi.UseMnemonic = False
    '
    'edTotcarichi
    '
    Me.edTotcarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotcarichi.Location = New System.Drawing.Point(406, 27)
    Me.edTotcarichi.Name = "edTotcarichi"
    Me.edTotcarichi.NTSDbField = ""
    Me.edTotcarichi.NTSFormat = "0"
    Me.edTotcarichi.NTSForzaVisZoom = False
    Me.edTotcarichi.NTSOldValue = ""
    Me.edTotcarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotcarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotcarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotcarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotcarichi.Properties.AutoHeight = False
    Me.edTotcarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotcarichi.Properties.MaxLength = 65536
    Me.edTotcarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotcarichi.Size = New System.Drawing.Size(80, 20)
    Me.edTotcarichi.TabIndex = 39
    '
    'lbTotcarichi
    '
    Me.lbTotcarichi.AutoSize = True
    Me.lbTotcarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotcarichi.Location = New System.Drawing.Point(341, 30)
    Me.lbTotcarichi.Name = "lbTotcarichi"
    Me.lbTotcarichi.NTSDbField = ""
    Me.lbTotcarichi.Size = New System.Drawing.Size(59, 13)
    Me.lbTotcarichi.TabIndex = 38
    Me.lbTotcarichi.Text = "Tot.Carichi"
    Me.lbTotcarichi.Tooltip = ""
    Me.lbTotcarichi.UseMnemonic = False
    '
    'edRettificheVen
    '
    Me.edRettificheVen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRettificheVen.Location = New System.Drawing.Point(249, 50)
    Me.edRettificheVen.Name = "edRettificheVen"
    Me.edRettificheVen.NTSDbField = ""
    Me.edRettificheVen.NTSFormat = "0"
    Me.edRettificheVen.NTSForzaVisZoom = False
    Me.edRettificheVen.NTSOldValue = ""
    Me.edRettificheVen.Properties.Appearance.Options.UseTextOptions = True
    Me.edRettificheVen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRettificheVen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRettificheVen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRettificheVen.Properties.AutoHeight = False
    Me.edRettificheVen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRettificheVen.Properties.MaxLength = 65536
    Me.edRettificheVen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRettificheVen.Size = New System.Drawing.Size(74, 20)
    Me.edRettificheVen.TabIndex = 37
    '
    'lbRettificheVen
    '
    Me.lbRettificheVen.AutoSize = True
    Me.lbRettificheVen.BackColor = System.Drawing.Color.Transparent
    Me.lbRettificheVen.Location = New System.Drawing.Point(232, 53)
    Me.lbRettificheVen.Name = "lbRettificheVen"
    Me.lbRettificheVen.NTSDbField = ""
    Me.lbRettificheVen.Size = New System.Drawing.Size(11, 13)
    Me.lbRettificheVen.TabIndex = 36
    Me.lbRettificheVen.Text = "/"
    Me.lbRettificheVen.Tooltip = ""
    Me.lbRettificheVen.UseMnemonic = False
    '
    'edRettificheAcq
    '
    Me.edRettificheAcq.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRettificheAcq.Location = New System.Drawing.Point(152, 50)
    Me.edRettificheAcq.Name = "edRettificheAcq"
    Me.edRettificheAcq.NTSDbField = ""
    Me.edRettificheAcq.NTSFormat = "0"
    Me.edRettificheAcq.NTSForzaVisZoom = False
    Me.edRettificheAcq.NTSOldValue = ""
    Me.edRettificheAcq.Properties.Appearance.Options.UseTextOptions = True
    Me.edRettificheAcq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRettificheAcq.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRettificheAcq.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRettificheAcq.Properties.AutoHeight = False
    Me.edRettificheAcq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRettificheAcq.Properties.MaxLength = 65536
    Me.edRettificheAcq.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRettificheAcq.Size = New System.Drawing.Size(74, 20)
    Me.edRettificheAcq.TabIndex = 35
    '
    'lbRettificheAcq
    '
    Me.lbRettificheAcq.AutoSize = True
    Me.lbRettificheAcq.BackColor = System.Drawing.Color.Transparent
    Me.lbRettificheAcq.Location = New System.Drawing.Point(10, 53)
    Me.lbRettificheAcq.Name = "lbRettificheAcq"
    Me.lbRettificheAcq.NTSDbField = ""
    Me.lbRettificheAcq.Size = New System.Drawing.Size(140, 13)
    Me.lbRettificheAcq.TabIndex = 34
    Me.lbRettificheAcq.Text = "Rett.val.su acquisti/vendite"
    Me.lbRettificheAcq.Tooltip = ""
    Me.lbRettificheAcq.UseMnemonic = False
    '
    'edEsistfi
    '
    Me.edEsistfi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsistfi.Location = New System.Drawing.Point(152, 24)
    Me.edEsistfi.Name = "edEsistfi"
    Me.edEsistfi.NTSDbField = ""
    Me.edEsistfi.NTSFormat = "0"
    Me.edEsistfi.NTSForzaVisZoom = False
    Me.edEsistfi.NTSOldValue = ""
    Me.edEsistfi.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsistfi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsistfi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsistfi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsistfi.Properties.AutoHeight = False
    Me.edEsistfi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsistfi.Properties.MaxLength = 65536
    Me.edEsistfi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsistfi.Size = New System.Drawing.Size(74, 20)
    Me.edEsistfi.TabIndex = 33
    '
    'lbEsistfi
    '
    Me.lbEsistfi.AutoSize = True
    Me.lbEsistfi.BackColor = System.Drawing.Color.Transparent
    Me.lbEsistfi.Location = New System.Drawing.Point(10, 27)
    Me.lbEsistfi.Name = "lbEsistfi"
    Me.lbEsistfi.NTSDbField = ""
    Me.lbEsistfi.Size = New System.Drawing.Size(81, 13)
    Me.lbEsistfi.TabIndex = 32
    Me.lbEsistfi.Text = "Saldo Esistenza"
    Me.lbEsistfi.Tooltip = ""
    Me.lbEsistfi.UseMnemonic = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbTaglie, Me.tlbNavigazioneDoc, Me.tlbImpostaStampante, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbDocumenti, Me.tlbCollaudi, Me.tlbAbilitaModificheDoc, Me.tlbZoom, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDocumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCollaudi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneDoc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAbilitaModificheDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 10
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 11
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 12
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 16
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.Id = 0
    Me.tlbTaglie.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbDocumenti
    '
    Me.tlbDocumenti.Caption = "Documenti"
    Me.tlbDocumenti.Glyph = CType(resources.GetObject("tlbDocumenti.Glyph"), System.Drawing.Image)
    Me.tlbDocumenti.Id = 13
    Me.tlbDocumenti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDocumenti.Name = "tlbDocumenti"
    Me.tlbDocumenti.Visible = True
    '
    'tlbCollaudi
    '
    Me.tlbCollaudi.Caption = "Collaudi"
    Me.tlbCollaudi.Glyph = CType(resources.GetObject("tlbCollaudi.Glyph"), System.Drawing.Image)
    Me.tlbCollaudi.Id = 14
    Me.tlbCollaudi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbCollaudi.Name = "tlbCollaudi"
    Me.tlbCollaudi.Visible = True
    '
    'tlbNavigazioneDoc
    '
    Me.tlbNavigazioneDoc.Caption = "NavigazioneDoc"
    Me.tlbNavigazioneDoc.Glyph = CType(resources.GetObject("tlbNavigazioneDoc.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneDoc.Id = 4
    Me.tlbNavigazioneDoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbNavigazioneDoc.Name = "tlbNavigazioneDoc"
    Me.tlbNavigazioneDoc.Visible = True
    '
    'tlbAbilitaModificheDoc
    '
    Me.tlbAbilitaModificheDoc.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbAbilitaModificheDoc.Caption = "Abilita modifica righe documento"
    Me.tlbAbilitaModificheDoc.Glyph = CType(resources.GetObject("tlbAbilitaModificheDoc.Glyph"), System.Drawing.Image)
    Me.tlbAbilitaModificheDoc.Id = 15
    Me.tlbAbilitaModificheDoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbAbilitaModificheDoc.Name = "tlbAbilitaModificheDoc"
    Me.tlbAbilitaModificheDoc.Visible = True
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
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 8
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 7
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.cmdOk)
    Me.pnBottom.Controls.Add(Me.lbEsistfi)
    Me.pnBottom.Controls.Add(Me.edTotvscarichi)
    Me.pnBottom.Controls.Add(Me.edEsistfi)
    Me.pnBottom.Controls.Add(Me.lbTotvscarichi)
    Me.pnBottom.Controls.Add(Me.lbRettificheAcq)
    Me.pnBottom.Controls.Add(Me.edTotscarichi)
    Me.pnBottom.Controls.Add(Me.edRettificheAcq)
    Me.pnBottom.Controls.Add(Me.lbTotscarichi)
    Me.pnBottom.Controls.Add(Me.lbRettificheVen)
    Me.pnBottom.Controls.Add(Me.edTotvcarichi)
    Me.pnBottom.Controls.Add(Me.edRettificheVen)
    Me.pnBottom.Controls.Add(Me.lbTotvcarichi)
    Me.pnBottom.Controls.Add(Me.lbTotcarichi)
    Me.pnBottom.Controls.Add(Me.edTotcarichi)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 357)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(660, 85)
    Me.pnBottom.TabIndex = 8
    Me.pnBottom.Text = "NtsPanel1"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrsc)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 93)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 264)
    Me.pnGrid.TabIndex = 9
    Me.pnGrid.Text = "NtsPanel1"
    '
    'FRMMGGRSC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(660, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGGRSC"
    Me.NTSLastControlFocussed = Me.grGrsc
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrsc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrsc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsistpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotvscarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotscarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotvcarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotcarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRettificheVen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRettificheAcq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsistfi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipork As New DataTable()
    Dim dttStasino As New DataTable
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbDocumenti.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbCollaudi.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbNavigazioneDoc.GlyphPath = (oApp.ChildImageDir & "\navigazione.gif")
        tlbAbilitaModificheDoc.GlyphPath = (oApp.ChildImageDir & "\unlock.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttStasino.Columns.Add("cod", GetType(String))
      dttStasino.Columns.Add("val", GetType(String))
      dttStasino.Rows.Add(New Object() {"S", "Si"})
      dttStasino.Rows.Add(New Object() {"N", "No"})
      dttStasino.Rows.Add(New Object() {"B", "Solo in bolla"})
      dttStasino.Rows.Add(New Object() {"D", "Solo in fattura"})
      dttStasino.Rows.Add(New Object() {"O", "Omaggi (imponibile)"})
      dttStasino.Rows.Add(New Object() {"M", "Sconto merce"})
      dttStasino.Rows.Add(New Object() {"X", "Sconto merce NC"})
      dttStasino.Rows.Add(New Object() {"P", "Omaggi (imp. + IVA)"})
      dttStasino.AcceptChanges()

      edMagaz.NTSSetParam(oMenu, oApp.Tr(Me, 129055191477041381, "Magazz."), "0")
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 129055191560637806, "Fase:"), "0")
      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 129055191589701236, "Articolo"), 0)
      edEsistpr.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331733531, "Esistenza"), oApp.FormatQta)
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331889391, "Conto"), "0")
      edTotvscarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128648460935922658, "Val.Scarichi"), oApp.FormatImporti)
      edTotscarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128648460936234378, "Tot Scarichi"), oApp.FormatQta)
      edTotvcarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128648460936546098, "Val.Carichi"), oApp.FormatImporti)
      edTotcarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128648460936857818, "Tot.Carichi"), oApp.FormatQta)
      edRettificheVen.NTSSetParam(oMenu, oApp.Tr(Me, 128648460937169538, "/"), oApp.FormatImporti)
      edRettificheAcq.NTSSetParam(oMenu, oApp.Tr(Me, 128648460937481258, "Rett.val.su acquisti/vendite"), oApp.FormatImporti)
      edEsistfi.NTSSetParam(oMenu, oApp.Tr(Me, 128648460937792978, "Saldo Esistenza"), oApp.FormatQta)

      grvGrsc.NTSSetParam(oMenu, "Stampa / Visualizzazione Schede Articoli")

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"A", "Fattura immediata emessa"})
      dttTipork.Rows.Add(New Object() {"B", "D.D.T. emesso"})
      dttTipork.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipork.Rows.Add(New Object() {"D", "Fattura differita emessa"})
      dttTipork.Rows.Add(New Object() {"E", "Nota di addebito emessa"})
      dttTipork.Rows.Add(New Object() {"F", "Ric. fiscale emessa"})
      dttTipork.Rows.Add(New Object() {"I", "Riemissione ric. fiscali"})
      dttTipork.Rows.Add(New Object() {"J", "Nota di accredito ricevuta"})
      dttTipork.Rows.Add(New Object() {"(", "Nota di accredito diff. ricevuta"})
      dttTipork.Rows.Add(New Object() {"K", "Fattura diff. ricevuta"})
      dttTipork.Rows.Add(New Object() {"L", "Fattura immediata ricevuta"})
      dttTipork.Rows.Add(New Object() {"M", "D.D.T. ricevuto"})
      dttTipork.Rows.Add(New Object() {"N", "Nota di accredito emessa"})
      dttTipork.Rows.Add(New Object() {"£", "Nota di accredito diff. emessa"})
      dttTipork.Rows.Add(New Object() {"P", "Fatt. ric. fisc. differita"})
      dttTipork.Rows.Add(New Object() {"S", "Fatt. ric. fisc. emessa"})
      dttTipork.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipork.Rows.Add(New Object() {"U", "Scarico a produzione"})
      dttTipork.Rows.Add(New Object() {"Z", "Bolla di movimentazione interna"})
      dttTipork.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipork.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipork.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipork.Rows.Add(New Object() {"W", "Nota di Prelievo"})
      dttTipork.Rows.Add(New Object() {"X", "Impegno di trasferimento"})
      dttTipork.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      dttTipork.AcceptChanges()

      km_aammgg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128648460922518698, "Data"), True)
      km_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128648460922674558, "Tipo"), dttTipork, "val", "cod")
      km_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460922830418, "Numero"), "0", 9, 0, 999999999)
      km_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460922986278, "Serie"), CLN__STD.SerieMaxLen, True)
      km_causale.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648460923142138, "Caus."), tabcaum)
      tb_descaum.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460923297998, "Descr."), 0, True)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460923453858, "Rifer."), 0, True)
      desconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460923609718, "Cliente/Forn."), 0, True)
      carichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460923765578, "Carichi"), oApp.FormatQta, 15)
      scarichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460923921438, "Scarichi"), oApp.FormatQta, 15)
      xx_disp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460924077298, "Esistenza"), oApp.FormatQta, 15)
      prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460924233158, "Prezzo N"), ocleSche.TrovaFmtPrz, 15)
      mm_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460924389018, "Valore"), "#,##0.00", 15)
      mm_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460924544878, "N.Ord."), "0", 9, 0, 999999999)
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460924700738, "Lotto"), 50, True)
      mm_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460924856598, "Prezzo Lordo"), ocleSche.TrovaFmtPrz, 15)
      mm_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925012458, "Sc.1"), oApp.FormatSconti, 15)
      mm_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925168318, "Sc. 2"), oApp.FormatSconti, 15)
      mm_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925324178, "Sc. 3"), oApp.FormatSconti, 15)
      mm_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925480038, "Sc. 4"), oApp.FormatSconti, 15)
      mm_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925635898, "Sc. 5"), oApp.FormatSconti, 15)
      mm_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925791758, "Sc. 6"), oApp.FormatSconti, 15)
      mm_prelist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460925947618, "Prz.List."), ocleSche.TrovaFmtPrz, 15)
      mm_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460926103478, "Prz.Val."), ocleSche.TrovaFmtPrz(1), 15)
      mm_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460926259338, "Colli"), oApp.FormatQta, 15)
      mm_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460926415198, "Mis.1"), oApp.FormatQta, 15)
      mm_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460926571058, "Mis.2"), oApp.FormatQta, 15)
      mm_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460926726918, "Mis.3"), oApp.FormatQta, 15)
      mm_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648460926882778, "Controp."), tabcove)
      mm_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648460927194498, "Commessa"), tabcommess)
      mm_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648460927350358, "C.Centro"), tabcena)
      mm_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128648460927506218, "Linea"), tabcfam, False)
      mm_codnomc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460927662078, "Nom.Comb."), 0, True)
      mm_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460927817938, "Provv.1"), "#,##0.00", 15)
      mm_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460927973798, "Val.Pr.1"), "#,##0.00", 15)
      mm_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928129658, "Provv.2"), "#,##0.00", 15)
      mm_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928285518, "Val.Pr.2"), "#,##0.00", 15)
      mm_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928441378, "Molt.qtà/prezzo"), "#,##0.00", 15)
      mm_stasino.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130143941874773750, "Stampa Riga"), dttStasino, "val", "cod")
      valorecsa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928597238, "Valore C.S.A."), oApp.FormatImporti, 15)
      valoreucsa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928753098, "Valore Un.C.S.A."), oApp.FormatImporti, 15)
      speacc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460928908958, "Spese acc."), oApp.FormatImporti, 15)
      speaccun.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460929064818, "Spese acc.un."), oApp.FormatImporti, 15)
      desport.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648460929220678, "Porto"), 0, True)
      km_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648460929376538, "KM_ANNO"), "0", 4, 0, 9999)
      co_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649025705122431, "Descr. commessa"), 0, True)
      mm_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129737763670576172, "Codice Iva"), tabciva)
      mm_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129737763954462891, "Data inizio"), False)
      mm_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129737763973046875, "Data fine"), False)
      mm_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129737761626738281, "Quantità"), oApp.FormatQta, 15)
      km_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129737786263750000, "Riga"), "0", 9, 0, 999999999)
      tm_scorpo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129737795141347656, "Scorporo"), 0, True)
      tm_valuta.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129737795396191406, "Valuta"), tabvalu)
      tm_valuta.NTSSetParamZoom("")

      grvGrsc.Enabled = False
      grvGrsc.NTSAllowInsert = False

      edConto.Enabled = False
      edEsistpr.Enabled = False
      edArticolo.Enabled = False
      edMagaz.Enabled = False
      edFase.Enabled = False
      edEsistfi.Enabled = False
      edRettificheAcq.Enabled = False
      edRettificheVen.Enabled = False
      edTotcarichi.Enabled = False
      edTotvcarichi.Enabled = False
      edTotscarichi.Enabled = False
      edTotvscarichi.Enabled = False

      If Not CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModSQ) Then
        tlbCollaudi.Visible = False
      Else
        GctlSetVisEnab(tlbCollaudi, True)
      End If

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

  Public Overridable Sub InitEntity(ByVal cleSche As CLEMGSCHE)
    ocleSche = cleSche
    AddHandler ocleSche.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGGRSC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      bOnLoading = True
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edEsistfi.Text = "0"
      edEsistpr.Text = "0"
      edTotvcarichi.Text = "0"
      edTotvscarichi.Text = "0"
      edTotcarichi.Text = "0"
      edTotscarichi.Text = "0"

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      ocleSche.strDatini = CDataSQL(ocleSche.strScarDatini)
      ocleSche.strDatinimeno1 = CDataSQL(NTSCStr(DateAdd("d", -1, NTSCDate(ocleSche.strScarDatini))))
      ocleSche.strDatfin = CDataSQL(ocleSche.strScarDatfin)

      If Not Apri() Then Return

      ApriRecordset()
      lbData.Text = oApp.Tr(Me, 129279754979133863, _
                    "Dal: |" & ocleSche.strScarDatini & "|  Al: |" & ocleSche.strScarDatfin & "|")
      If ocleSche.strScarOrdin = "A" Then
        ocleSche.SaldoEsistenza(edEsistfi.Text, edEsistpr.Text, edTotvcarichi.Text, edTotvscarichi.Text, _
                                edTotcarichi.Text, edTotscarichi.Text, dcGrsc.Position)
        If ocleSche.bGrscSaldiIniziali = True Then
          GctlSetVisEnab(xx_disp, True)
        Else
          xx_disp.Visible = False
          lbEsistpr.Visible = False
          lbEsistfi.Visible = False
          edEsistpr.Visible = False
          edEsistfi.Visible = False
        End If
        lbConto.Visible = False
        edConto.Visible = False
        lbXx_conto.Visible = False
        'fmGrsc.Height = 5220
      Else
        lbRettificheVen.Visible = False
        desconto.Visible = False
        xx_disp.Visible = False
        lbEsistpr.Visible = False
        lbEsistfi.Visible = False
        lbTotvcarichi.Visible = False
        lbTotvscarichi.Visible = False
        edEsistpr.Visible = False
        edEsistfi.Visible = False
        edTotvcarichi.Visible = False
        edTotvscarichi.Visible = False
        lbRettificheAcq.Visible = False
        edRettificheVen.Visible = False
        edRettificheAcq.Visible = False
        edRettificheVen.Visible = False
        ocleSche.TotValore(edTotvcarichi.Text, edTotvscarichi.Text, edTotcarichi.Text, edTotscarichi.Text)
        'fmGrsc.Height = 4845
      End If
      '------------------
      If ocleSche.bScarDaveboll Then
        Me.Text = oApp.Tr(Me, 129080490759056089, "Precedenti movimenti Conto - Articolo")
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        GctlSetVisEnab(cmdOk, True)
        lbData.Visible = False
        ' se non impostati i nuovi campi ..
        If ocleSche.lScarAlotto = 0 Then ocleSche.lScarAlotto = 999999999
        If ocleSche.lScarAcomme = 0 Then ocleSche.lScarAcomme = 999999999
      End If
      '-----------------------------------------------------------------------------------------
      If ocleSche.bModTCO = False Then tlbTaglie.Visible = False
      '-----------------------------------------------------------------------------------------
      If Not ocleSche.GetRelease() Then
        tlbNavigazioneDoc.Visible = True
      Else
        tlbNavigazioneDoc.Visible = False
      End If

      CaricaColonneUnbound()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------------------------------------------------------------------------
      '--- Se Friendly, nasconde SEMPRE alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        mm_scont3.Visible = False
        mm_scont4.Visible = False
        mm_scont5.Visible = False
        mm_scont6.Visible = False
        xx_lottox.Visible = False
        mm_codcena.Visible = False
        mm_provv2.Visible = False
        mm_vprovv2.Visible = False
        mm_commeca.Visible = False
        co_descr1.Visible = False
        mm_codnomc.Visible = False
        desconto.VisibleIndex = 0
      End If

      If CLN__STD.IsBis Then tlbStampaVideo.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then
        tlbAbilitaModificheDoc.Enabled = False
        tlbAbilitaModificheDoc.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bOnLoading = False
    End Try
  End Sub

  Public Overridable Sub FRMMGGRSC_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGRSC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = False Then
        If SalvaDocumento() = False Then e.Cancel = True
      Else
        ocleSche.SvuotaTmpTable()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGGRSC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrsc.Dispose()
      dsGrsc.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      Sposta("PRIMO")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      Sposta("PRECEDENTE")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      Sposta("SUCCESSIVO")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      Sposta("ULTIMO")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsArtico As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie e colori esce
      '-----------------------------------------------------------------------------------------
      If ocleSche.bModTCO = False Then Exit Sub

      If grvGrsc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è gestito per taglia, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If ocleSche.bModTCO = True Then
        If Not ocleSche.CheckArticotaglie(NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_codart)) Then
          Exit Sub
        End If
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSMGSCHE"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_codart) & "|" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_codart) & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_fase) & "|" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_fase) & "|" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_tipork) & ";" & _
               Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_anno), 4) & ";" & _
               NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_serie) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_numdoc), 9) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrsc.NTSGetCurrentDataRow!km_riga), 9) & ";|" & _
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

  Public Overridable Sub tlbDocumenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDocumenti.ItemClick
    Dim strTipork As String
    Dim nAnno As Integer
    Dim strSerie As String
    Dim lNumdoc As Integer
    Dim strParam As String
    Try
      If grvGrsc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      strTipork = NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_tipork)
      nAnno = NTSCInt(grvGrsc.NTSGetCurrentDataRow()!km_anno)
      strSerie = NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_serie)
      lNumdoc = NTSCInt(grvGrsc.NTSGetCurrentDataRow()!km_numdoc)
      '-------------------------------------------------------------------------------------
      If ocleSche.IsDocRetail(DittaCorrente, strTipork, nAnno, strSerie, lNumdoc) = True Then
        strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
          strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumdoc), 9) & ";"
        oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 130021090395342338, "Gestione Documenti"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
      ElseIf ocleSche.IsDocRetailNew(DittaCorrente, strTipork, nAnno, strSerie, lNumdoc) = True Then
        strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
          strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumdoc), 9) & ";"
        oMenu.RunChild("NTSInformatica", "FRMREGSRE", "Gestione retail", DittaCorrente, "", "BNREGSRE", Nothing, strParam, Not bNoModal, True)
      Else
        strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
          strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumdoc), 9) & ";"
        oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", "Gestione Documenti", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
      End If

      If bNoModal = True Then Return

      If Not Apri() Then
        Me.Close()
        Exit Sub
      End If
      ApriRecordset()
      '--------------------------------------------------------------------------------------------------------------
      RiempiLabelColonneUnbound
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCollaudi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCollaudi.ItemClick
    Dim dsTmp As DataSet = Nothing
    Dim nMagimp As Integer
    Dim strDatdoc As String
    Dim strParam As String
    Try
      If grvGrsc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      If ocleSche.CheckEsistCollaudo(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_tipork), _
        NTSCInt(grvGrsc.NTSGetCurrentDataRow()!km_anno), _
        NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_serie), _
        NTSCInt(grvGrsc.NTSGetCurrentDataRow()!km_numdoc), _
        NTSCInt(grvGrsc.NTSGetCurrentDataRow()!km_riga)) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129917604023539662, "Attenzione!" & vbCrLf & _
          "Non esistono collaudi relativi alla riga corrente."))
        Return
      End If

      ocleSche.GetCollaudi(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_tipork), _
                            NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_anno), _
                            NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_serie), _
                            NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_numdoc), dsTmp)

      nMagimp = NTSCInt(dsTmp.Tables("TESTMAG").Rows(0)!tm_magimp)
      strDatdoc = NTSCDate(dsTmp.Tables("TESTMAG").Rows(0)!tm_datdoc).ToShortDateString

      strParam = "APRI;BSMGSCHE;000000000;" & NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_tipork) & ";" & _
        Microsoft.VisualBasic.Right("0000" & NTSCStr(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_anno)), 4) & ";" & _
        NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_serie) & ";" & _
        Microsoft.VisualBasic.Right("000000000" & NTSCStr(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_numdoc)), 9) & ";" & _
        Microsoft.VisualBasic.Right("000000000" & NTSCStr(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_riga)), 9) & _
        ";000000000;" & _
        Microsoft.VisualBasic.Right("0000" & NTSCStr(nMagimp), 4) & ";" & _
        Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_conto), 9) & _
        ";0000;" & _
        Microsoft.VisualBasic.Left(NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_codart) & "".PadLeft(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen) & ";" & _
        Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrsc.NTSGetCurrentDataRow()!km_causale), 4) & _
        ";0000;" & _
        strDatdoc & ";" & _
        "0000;0000;0000;0000;" & _
        NTSCDec(grvGrsc.NTSGetCurrentDataRow()!mm_quant).ToString("0000000000.000").Replace(",", ".") & _
        ";N;"

      oMenu.RunChild("BSSQCOLL", "CLSSQCOLL", "Gestione Collaudi", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneDoc.ItemClick
    Dim strParam As String
    Try
      If grvGrsc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      If NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork) = "D" Or NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork) = "K" Or _
         NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork) = "£" Or NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork) = "(" Then
        strParam = "APRI;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork), 1) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(Year(NTSCDate(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_aammgg))), 4) & ";" & _
            Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_serie), 1) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_numdoc), 9) & ";" & _
            "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_aammgg).ToShortDateString, 10) & _
            ";000000000;0000;0000; ;000000000;0000;4"
      Else
        strParam = "APRI;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_tipork), 1) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(Year(NTSCDate(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_aammgg))), 4) & ";" & _
            NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_serie).PadLeft(1) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_numdoc), 9) & ";" & _
            "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(dsGrid.Tables("MOVMAG").Rows(dcGrid.Position)!km_aammgg).ToShortDateString, 10) & _
            ";000000000;0000;0000; ;000000000;0000;2"
      End If

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 130420311152280594, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAbilitaModificheDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAbilitaModificheDoc.ItemClick
    Try
      '-------------------------------------------------------------------------------------------------------------
      grvGrsc_NTSFocusedRowChanged(Me, Nothing)
      '-------------------------------------------------------------------------------------------------------------
      If tlbAbilitaModificheDoc.Down = False Then
        ApriRecordset()
        RiempiLabelColonneUnbound()
      End If
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    Dim strHeader As String = ""
    Dim strFooter As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strHeader = lbArticolo.Text & "".PadLeft(1) & edArticolo.Text & " " & lbXx_articolo.Text.ToUpper & "".PadLeft(5) & _
        lbFase.Text & "".PadLeft(1) & edFase.Text & "".PadLeft(5)
      Select Case oCleSche.strScarOrdin
        Case "A"
          strHeader += lbMagaz.Text & "".PadLeft(1) & edMagaz.Text & "".PadLeft(5) & _
            lbEsistpr.Text & "".PadLeft(1) & edEsistpr.Text
          strFooter = "TOT CAR/SCAR: " & edTotcarichi.Text & " / " & edTotscarichi.Text & "".PadLeft(5) & _
            "   TOT.VAL.CAR/SCAR: " & edTotvcarichi.Text & " / " & edTotvscarichi.Text & "".PadLeft(5) & _
            "   SALDO ES: " & edEsistfi.Text
        Case Else
          strHeader += lbConto.Text & "".PadLeft(1) & lbXx_conto.Text & "".PadLeft(5) & _
            lbMagaz.Text & "".PadLeft(1) & edMagaz.Text
          strFooter = lbTotcarichi.Text & "".PadLeft(1) & edTotcarichi.Text & _
            lbTotscarichi.Text & "".PadLeft(1) & edTotscarichi.Text
      End Select
      '--------------------------------------------------------------------------------------------------------------      
      grvGrsc.NTSPrintPreview(strHeader, strFooter)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

#Region "Eventi di Griglia"
  Public Overridable Sub grGrsc_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grGrsc.MouseDoubleClick
    Try
      If tlbDocumenti.Enabled And tlbDocumenti.Visible Then tlbDocumenti_ItemClick(tlbDocumenti, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grvGrsc_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGrsc.NTSBeforeRowUpdate
    Try
      SalvaDocumento()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvGrsc_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGrsc.NTSFocusedRowChanged
    Dim bSblocca As Boolean = CBool(IIf(tlbAbilitaModificheDoc.Down, True, False))

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      For Each oCol As NTSGridColumn In grvGrsc.Columns
        oCol.Enabled = Not bSblocca
      Next
      '-------------------------------------------------------------------------------------------------------------
      grvGrsc.Enabled = bSblocca
      grvGrsc.NTSAllowUpdate = bSblocca
      '-------------------------------------------------------------------------------------------------------------
      Select Case bSblocca
        Case True
          pnBottom.Visible = False
          GctlSetVisEnab(tlbZoom, True)
          mm_codiva.NTSSetParamZoom("ZOOMTABCIVA")
          mm_controp.NTSSetParamZoom("ZOOMTABCOVE")
          mm_codcena.NTSSetParamZoom("ZOOMTABCENA")
          mm_codcfam.NTSSetParamZoom("ZOOMTABCFAM")
          mm_commeca.NTSSetParamZoom("ZOOMCOMMESS")
          GctlSetVisEnab(mm_quant, True)
        Case False
          tlbZoom.Visible = False
          mm_quant.Visible = False
          mm_codiva.NTSSetParamZoom("")
          mm_controp.NTSSetParamZoom("")
          mm_codcena.NTSSetParamZoom("")
          mm_codcfam.NTSSetParamZoom("")
          mm_commeca.NTSSetParamZoom("")
          GctlSetVisEnab(pnBottom, True)
          Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(grvGrsc.GetFocusedRowCellValue(km_tipork))
        Case "T", "U", "D", "K", "(", "£", "I", "P"
        Case Else
          For Each oCol As NTSGridColumn In grvGrsc.Columns
            Select Case oCol.Name.ToLower
              Case "mm_prezzo", "mm_scont1", "mm_scont2", "mm_scont3", "mm_scont4", "mm_scont5", "mm_scont6", _
                "mm_provv", "mm_provv2", "mm_codiva", "mm_controp", "mm_codcena", "mm_codcfam", "mm_commeca", _
                "mm_datini", "mm_datfin"
                GctlSetVisEnab(oCol, False)
              Case "mm_quant", "mm_colli"
                If (ocleSche.bGrscArticoloTCO = False) And (ocleSche.bGrscArticoloMatricola = False) Then
                  GctlSetVisEnab(oCol, False)
                End If
            End Select
          Next
      End Select
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Dim strError As String = ""
    Try
      If Not ocleSche.Apri(dsGrsc, strError) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        bClose = True
        Return False
      End If

      dcGrsc.DataSource = dsGrsc.Tables("MOVMAG")
      dsGrsc.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ApriRecordset()
    Dim strDescr As String = ""
    Dim dttTmp As New DataTable
    Try
      If (ocleSche.bModuloCRM = True) And (ocleSche.bIsCRMUser = True) And Not (ocleSche.strAccvis = "T" And ocleSche.bAmm = True) Then
        ComponiStringa()
      Else
        ComponiStringaSQL()
      End If

      edMagaz.Text = NTSCStr(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_magaz)
      edFase.Text = NTSCStr(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_fase)

      If ocleSche.lbArticolo_Validated(NTSCStr(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_codart), strDescr, dttTmp) Then
        edArticolo.Text = NTSCStr(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_codart)
        lbXx_articolo.Text = strDescr & " " & NTSCStr(dttTmp.Rows(0)!ar_desint)
      End If
      If ocleSche.strScarOrdin = "C" Then
        If ocleSche.lbConto_Validated(NTSCInt(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_conto), strDescr) Then
          edConto.Text = NTSCStr(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position)!km_conto)
          lbXx_conto.Text = strDescr
        End If
      End If
      ocleSche.CalcolaRettifiche(edRettificheAcq.Text, edRettificheVen.Text, dcGrsc.Position)

      dcGrid.DataSource = dsGrid.Tables("MOVMAG")
      dsGrid.AcceptChanges()

      grGrsc.DataSource = dcGrid
      '-------------------------------------------------------------------------------------------------------------
      grvGrsc_NTSFocusedRowChanged(Me, Nothing)
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CaricaColonneUnbound() As Boolean
    Dim i As Integer
    Try
      For i = 0 To dsGrid.Tables("MOVMAG").Rows.Count - 1
        If ocleSche.strScarOrdin = "A" Then
          If ocleSche.bGrscSaldiIniziali Then
            dsGrid.Tables("MOVMAG").Rows(i)!xx_disp = ocleSche.dEsist(i)
          Else
            dsGrid.Tables("MOVMAG").Rows(i)!xx_disp = 0
          End If
        End If
      Next
      dsGrid.Tables("MOVMAG").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ComponiStringa()
    Try
      ocleSche.ComponiStringa(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position), dsGrid)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringaSQL()
    Try
      ocleSche.ComponiStringaSQL(dsGrsc.Tables("MOVMAG").Rows(dcGrsc.Position), dsGrid)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiLabelColonneUnbound()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ocleSche.strScarOrdin = "A" Then
        ocleSche.SaldoEsistenza(edEsistfi.Text, edEsistpr.Text, edTotvcarichi.Text, edTotvscarichi.Text, _
                                edTotcarichi.Text, edTotscarichi.Text, dcGrsc.Position)
      Else
        ocleSche.TotValore(edTotvcarichi.Text, edTotvscarichi.Text, edTotcarichi.Text, edTotscarichi.Text)
      End If
      '--------------------------------------------------------------------------------------------------------------
      CaricaColonneUnbound()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function SalvaDocumento() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se posso modificare le colonne, al cambio di riga riverso le modifiche sul database
      '--------------------------------------------------------------------------------------------------------------
      If tlbAbilitaModificheDoc.Down = False Then Return True
      If grvGrsc.NTSGetCurrentDataRow Is Nothing Then Return True
      If grvGrsc.NTSGetCurrentDataRow.RowState = DataRowState.Unchanged Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(grvGrsc.NTSGetCurrentDataRow!tm_scorpo) = "S" Or _
         NTSCInt(grvGrsc.NTSGetCurrentDataRow!tm_valuta) <> 0 Then
        If NTSCDec(grvGrsc.NTSGetCurrentDataRow!mm_prezzo) <> NTSCDec(grvGrsc.NTSGetCurrentDataRow("mm_prezzo", DataRowVersion.Original)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129684400222844350, "Nei documenti IVA compresa e/o in valuta diversa da 0 il prezzo non può essere modificato"))
          grvGrsc.NTSGetCurrentDataRow.RejectChanges()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If ocleSche.ModificaRigaVeboll(grvGrsc.NTSGetCurrentDataRow) = False Then
        grvGrsc.NTSGetCurrentDataRow.RejectChanges()
        Return False
      Else
        grvGrsc.NTSGetCurrentDataRow.AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Sposta(ByVal strDirezione As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è stata attivata la modifica del documento, prima salva/ripristina, le modifiche in TESTMAG/MOVMAG
      '--------------------------------------------------------------------------------------------------------------
      If SalvaDocumento() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Select Case strDirezione
        Case "PRIMO" : dcGrsc.MoveFirst()
        Case "PRECEDENTE" : dcGrsc.MovePrevious()
        Case "SUCCESSIVO" : dcGrsc.MoveNext()
        Case "ULTIMO" : dcGrsc.MoveLast()
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ApriRecordset()
      '--------------------------------------------------------------------------------------------------------------
      RiempiLabelColonneUnbound
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
