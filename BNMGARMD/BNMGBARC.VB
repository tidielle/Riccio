Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGBARC
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsBarc As New DataSet
  Public dcBarc As New BindingSource()
  Public dttBarc As DataTable = Nothing
  Public dtrBarc() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer
  Public bInAbilitaCellaEnter As Boolean = False

  Public bServer As Boolean

  Public dttUm As DataTable = Nothing                    'elenco delle unit‡ di misura utilizzate in artico
  Public dttTagl As DataTable = Nothing                  'elenco delle taglie

  Public Const EAN8 As Integer = 1
  Public Const EAN13 As Integer = 2

  Public WithEvents grBarc As NTSInformatica.NTSGrid
  Public WithEvents grvBarc As NTSInformatica.NTSGridView
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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents bc_code As NTSInformatica.NTSGridColumn
  Public WithEvents bc_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents bc_quant As NTSInformatica.NTSGridColumn
  Public WithEvents bc_orins As NTSInformatica.NTSGridColumn
  Public WithEvents bc_datagg As NTSInformatica.NTSGridColumn
  Public WithEvents bc_oragg As NTSInformatica.NTSGridColumn
  Public WithEvents bc_codart As NTSInformatica.NTSGridColumn
  Public WithEvents bc_datins As NTSInformatica.NTSGridColumn
  Public WithEvents bc_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents bc_tagl As NTSInformatica.NTSGridColumn
  Public WithEvents bc_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbGeneraEAN8 As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbControllaEAN8 As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbControllaEAN13 As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraEAN8t As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraEAN13t As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraEAN13p As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraEAN13v As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGiftCard As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraEAN13 As NTSInformatica.NTSBarButtonItem
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strBarcCodart = oCallParams.strPar2
    oCleArmd.strBarcUnmis = oCallParams.strPar3
    oCleArmd.strBarcConfez2 = oCallParams.strPar4
    oCleArmd.strBarcunmis2 = oCallParams.strPar5
    oCleArmd.dBarcQtacon2 = oCallParams.dPar1
    oCleArmd.dBarcConver = oCallParams.dPar2
    oCleArmd.bBarcGesfasi = oCallParams.bPar1
    oCleArmd.dBarcFase = oCallParams.dPar3
    oCleArmd.nBarcCodtagl = NTSCInt(oCallParams.dPar4)

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGBARC))
    Me.grBarc = New NTSInformatica.NTSGrid
    Me.grvBarc = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.bc_code = New NTSInformatica.NTSGridColumn
    Me.bc_unmis = New NTSInformatica.NTSGridColumn
    Me.bc_quant = New NTSInformatica.NTSGridColumn
    Me.bc_datins = New NTSInformatica.NTSGridColumn
    Me.bc_orins = New NTSInformatica.NTSGridColumn
    Me.bc_datagg = New NTSInformatica.NTSGridColumn
    Me.bc_oragg = New NTSInformatica.NTSGridColumn
    Me.bc_codart = New NTSInformatica.NTSGridColumn
    Me.bc_tipo = New NTSInformatica.NTSGridColumn
    Me.bc_tagl = New NTSInformatica.NTSGridColumn
    Me.bc_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbGeneraEAN8 = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraEAN13 = New NTSInformatica.NTSBarButtonItem
    Me.tlbControllaEAN8 = New NTSInformatica.NTSBarButtonItem
    Me.tlbControllaEAN13 = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraEAN8t = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraEAN13t = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraEAN13p = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraEAN13v = New NTSInformatica.NTSBarButtonItem
    Me.tlbGiftCard = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grBarc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvBarc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grBarc
    '
    Me.grBarc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grBarc.EmbeddedNavigator.Name = ""
    Me.grBarc.Location = New System.Drawing.Point(0, 30)
    Me.grBarc.MainView = Me.grvBarc
    Me.grBarc.Name = "grBarc"
    Me.grBarc.Size = New System.Drawing.Size(667, 336)
    Me.grBarc.TabIndex = 0
    Me.grBarc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvBarc})
    '
    'grvBarc
    '
    Me.grvBarc.ActiveFilterEnabled = False
    Me.grvBarc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.bc_code, Me.bc_unmis, Me.bc_quant, Me.bc_datins, Me.bc_orins, Me.bc_datagg, Me.bc_oragg, Me.bc_codart, Me.bc_tipo, Me.bc_tagl, Me.bc_fase, Me.xx_fase})
    Me.grvBarc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvBarc.Enabled = True
    Me.grvBarc.GridControl = Me.grBarc
    Me.grvBarc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvBarc.MinRowHeight = 14
    Me.grvBarc.Name = "grvBarc"
    Me.grvBarc.NTSAllowDelete = True
    Me.grvBarc.NTSAllowInsert = True
    Me.grvBarc.NTSAllowUpdate = True
    Me.grvBarc.NTSMenuContext = Nothing
    Me.grvBarc.OptionsCustomization.AllowRowSizing = True
    Me.grvBarc.OptionsFilter.AllowFilterEditor = False
    Me.grvBarc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvBarc.OptionsNavigation.UseTabKey = False
    Me.grvBarc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvBarc.OptionsView.ColumnAutoWidth = False
    Me.grvBarc.OptionsView.EnableAppearanceEvenRow = True
    Me.grvBarc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvBarc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvBarc.OptionsView.ShowGroupPanel = False
    Me.grvBarc.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. ditta"
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
    'bc_code
    '
    Me.bc_code.AppearanceCell.Options.UseBackColor = True
    Me.bc_code.AppearanceCell.Options.UseTextOptions = True
    Me.bc_code.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_code.Caption = "Barcode"
    Me.bc_code.Enabled = True
    Me.bc_code.FieldName = "bc_code"
    Me.bc_code.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_code.Name = "bc_code"
    Me.bc_code.NTSRepositoryComboBox = Nothing
    Me.bc_code.NTSRepositoryItemCheck = Nothing
    Me.bc_code.NTSRepositoryItemMemo = Nothing
    Me.bc_code.NTSRepositoryItemText = Nothing
    Me.bc_code.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_code.OptionsFilter.AllowFilter = False
    Me.bc_code.Visible = True
    Me.bc_code.VisibleIndex = 0
    '
    'bc_unmis
    '
    Me.bc_unmis.AppearanceCell.Options.UseBackColor = True
    Me.bc_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.bc_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_unmis.Caption = "Conf."
    Me.bc_unmis.Enabled = True
    Me.bc_unmis.FieldName = "bc_unmis"
    Me.bc_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_unmis.Name = "bc_unmis"
    Me.bc_unmis.NTSRepositoryComboBox = Nothing
    Me.bc_unmis.NTSRepositoryItemCheck = Nothing
    Me.bc_unmis.NTSRepositoryItemMemo = Nothing
    Me.bc_unmis.NTSRepositoryItemText = Nothing
    Me.bc_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_unmis.OptionsFilter.AllowFilter = False
    Me.bc_unmis.Visible = True
    Me.bc_unmis.VisibleIndex = 1
    '
    'bc_quant
    '
    Me.bc_quant.AppearanceCell.Options.UseBackColor = True
    Me.bc_quant.AppearanceCell.Options.UseTextOptions = True
    Me.bc_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_quant.Caption = "Quantit‡"
    Me.bc_quant.Enabled = True
    Me.bc_quant.FieldName = "bc_quant"
    Me.bc_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_quant.Name = "bc_quant"
    Me.bc_quant.NTSRepositoryComboBox = Nothing
    Me.bc_quant.NTSRepositoryItemCheck = Nothing
    Me.bc_quant.NTSRepositoryItemMemo = Nothing
    Me.bc_quant.NTSRepositoryItemText = Nothing
    Me.bc_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_quant.OptionsFilter.AllowFilter = False
    Me.bc_quant.Visible = True
    Me.bc_quant.VisibleIndex = 2
    '
    'bc_datins
    '
    Me.bc_datins.AppearanceCell.Options.UseBackColor = True
    Me.bc_datins.AppearanceCell.Options.UseTextOptions = True
    Me.bc_datins.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_datins.Caption = "Data ins."
    Me.bc_datins.Enabled = True
    Me.bc_datins.FieldName = "bc_datins"
    Me.bc_datins.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_datins.Name = "bc_datins"
    Me.bc_datins.NTSRepositoryComboBox = Nothing
    Me.bc_datins.NTSRepositoryItemCheck = Nothing
    Me.bc_datins.NTSRepositoryItemMemo = Nothing
    Me.bc_datins.NTSRepositoryItemText = Nothing
    Me.bc_datins.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_datins.OptionsFilter.AllowFilter = False
    Me.bc_datins.Visible = True
    Me.bc_datins.VisibleIndex = 3
    '
    'bc_orins
    '
    Me.bc_orins.AppearanceCell.Options.UseBackColor = True
    Me.bc_orins.AppearanceCell.Options.UseTextOptions = True
    Me.bc_orins.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_orins.Caption = "Ora Inserimento"
    Me.bc_orins.Enabled = True
    Me.bc_orins.FieldName = "bc_orins"
    Me.bc_orins.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_orins.Name = "bc_orins"
    Me.bc_orins.NTSRepositoryComboBox = Nothing
    Me.bc_orins.NTSRepositoryItemCheck = Nothing
    Me.bc_orins.NTSRepositoryItemMemo = Nothing
    Me.bc_orins.NTSRepositoryItemText = Nothing
    Me.bc_orins.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_orins.OptionsFilter.AllowFilter = False
    '
    'bc_datagg
    '
    Me.bc_datagg.AppearanceCell.Options.UseBackColor = True
    Me.bc_datagg.AppearanceCell.Options.UseTextOptions = True
    Me.bc_datagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_datagg.Caption = "Data Aggiornamento"
    Me.bc_datagg.Enabled = True
    Me.bc_datagg.FieldName = "bc_datagg"
    Me.bc_datagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_datagg.Name = "bc_datagg"
    Me.bc_datagg.NTSRepositoryComboBox = Nothing
    Me.bc_datagg.NTSRepositoryItemCheck = Nothing
    Me.bc_datagg.NTSRepositoryItemMemo = Nothing
    Me.bc_datagg.NTSRepositoryItemText = Nothing
    Me.bc_datagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_datagg.OptionsFilter.AllowFilter = False
    '
    'bc_oragg
    '
    Me.bc_oragg.AppearanceCell.Options.UseBackColor = True
    Me.bc_oragg.AppearanceCell.Options.UseTextOptions = True
    Me.bc_oragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_oragg.Caption = "Ora Aggiornamento"
    Me.bc_oragg.Enabled = True
    Me.bc_oragg.FieldName = "bc_oragg"
    Me.bc_oragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_oragg.Name = "bc_oragg"
    Me.bc_oragg.NTSRepositoryComboBox = Nothing
    Me.bc_oragg.NTSRepositoryItemCheck = Nothing
    Me.bc_oragg.NTSRepositoryItemMemo = Nothing
    Me.bc_oragg.NTSRepositoryItemText = Nothing
    Me.bc_oragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_oragg.OptionsFilter.AllowFilter = False
    '
    'bc_codart
    '
    Me.bc_codart.AppearanceCell.Options.UseBackColor = True
    Me.bc_codart.AppearanceCell.Options.UseTextOptions = True
    Me.bc_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_codart.Caption = "Codice Articolo"
    Me.bc_codart.Enabled = True
    Me.bc_codart.FieldName = "bc_codart"
    Me.bc_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_codart.Name = "bc_codart"
    Me.bc_codart.NTSRepositoryComboBox = Nothing
    Me.bc_codart.NTSRepositoryItemCheck = Nothing
    Me.bc_codart.NTSRepositoryItemMemo = Nothing
    Me.bc_codart.NTSRepositoryItemText = Nothing
    Me.bc_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_codart.OptionsFilter.AllowFilter = False
    '
    'bc_tipo
    '
    Me.bc_tipo.AppearanceCell.Options.UseBackColor = True
    Me.bc_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.bc_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_tipo.Caption = "Tipo"
    Me.bc_tipo.Enabled = True
    Me.bc_tipo.FieldName = "bc_tipo"
    Me.bc_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_tipo.Name = "bc_tipo"
    Me.bc_tipo.NTSRepositoryComboBox = Nothing
    Me.bc_tipo.NTSRepositoryItemCheck = Nothing
    Me.bc_tipo.NTSRepositoryItemMemo = Nothing
    Me.bc_tipo.NTSRepositoryItemText = Nothing
    Me.bc_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_tipo.OptionsFilter.AllowFilter = False
    Me.bc_tipo.Visible = True
    Me.bc_tipo.VisibleIndex = 4
    '
    'bc_tagl
    '
    Me.bc_tagl.AppearanceCell.Options.UseBackColor = True
    Me.bc_tagl.AppearanceCell.Options.UseTextOptions = True
    Me.bc_tagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_tagl.Caption = "Taglia"
    Me.bc_tagl.Enabled = True
    Me.bc_tagl.FieldName = "bc_tagl"
    Me.bc_tagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_tagl.Name = "bc_tagl"
    Me.bc_tagl.NTSRepositoryComboBox = Nothing
    Me.bc_tagl.NTSRepositoryItemCheck = Nothing
    Me.bc_tagl.NTSRepositoryItemMemo = Nothing
    Me.bc_tagl.NTSRepositoryItemText = Nothing
    Me.bc_tagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_tagl.OptionsFilter.AllowFilter = False
    Me.bc_tagl.Visible = True
    Me.bc_tagl.VisibleIndex = 5
    '
    'bc_fase
    '
    Me.bc_fase.AppearanceCell.Options.UseBackColor = True
    Me.bc_fase.AppearanceCell.Options.UseTextOptions = True
    Me.bc_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.bc_fase.Caption = "Fase"
    Me.bc_fase.Enabled = True
    Me.bc_fase.FieldName = "bc_fase"
    Me.bc_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.bc_fase.Name = "bc_fase"
    Me.bc_fase.NTSRepositoryComboBox = Nothing
    Me.bc_fase.NTSRepositoryItemCheck = Nothing
    Me.bc_fase.NTSRepositoryItemMemo = Nothing
    Me.bc_fase.NTSRepositoryItemText = Nothing
    Me.bc_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.bc_fase.OptionsFilter.AllowFilter = False
    Me.bc_fase.Visible = True
    Me.bc_fase.VisibleIndex = 6
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descr. fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 7
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbGeneraEAN8, Me.tlbGeneraEAN13, Me.tlbControllaEAN8, Me.tlbControllaEAN13, Me.tlbGeneraEAN8t, Me.tlbGeneraEAN13t, Me.tlbGeneraEAN13p, Me.tlbGeneraEAN13v, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGiftCard})
    Me.NtsBarManager1.MaxItemId = 29
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.Id = 16
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN8), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN13), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbControllaEAN8), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbControllaEAN13), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN8t, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN13t), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN13p, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraEAN13v), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbGiftCard, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbGeneraEAN8
    '
    Me.tlbGeneraEAN8.Caption = "Genera EAN8"
    Me.tlbGeneraEAN8.Id = 17
    Me.tlbGeneraEAN8.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F6))
    Me.tlbGeneraEAN8.Name = "tlbGeneraEAN8"
    Me.tlbGeneraEAN8.Visible = True
    '
    'tlbGeneraEAN13
    '
    Me.tlbGeneraEAN13.Caption = "Genera EAN13"
    Me.tlbGeneraEAN13.Id = 18
    Me.tlbGeneraEAN13.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbGeneraEAN13.Name = "tlbGeneraEAN13"
    Me.tlbGeneraEAN13.Visible = True
    '
    'tlbControllaEAN8
    '
    Me.tlbControllaEAN8.Caption = "Controlla EAN8"
    Me.tlbControllaEAN8.Id = 19
    Me.tlbControllaEAN8.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbControllaEAN8.Name = "tlbControllaEAN8"
    Me.tlbControllaEAN8.Visible = True
    '
    'tlbControllaEAN13
    '
    Me.tlbControllaEAN13.Caption = "Controlla EAN13"
    Me.tlbControllaEAN13.Id = 20
    Me.tlbControllaEAN13.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F9))
    Me.tlbControllaEAN13.Name = "tlbControllaEAN13"
    Me.tlbControllaEAN13.Visible = True
    '
    'tlbGeneraEAN8t
    '
    Me.tlbGeneraEAN8t.Caption = "Genera EAN8 per taglia"
    Me.tlbGeneraEAN8t.Id = 21
    Me.tlbGeneraEAN8t.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F6))
    Me.tlbGeneraEAN8t.Name = "tlbGeneraEAN8t"
    Me.tlbGeneraEAN8t.Visible = True
    '
    'tlbGeneraEAN13t
    '
    Me.tlbGeneraEAN13t.Caption = "Genera EAN13 per taglia"
    Me.tlbGeneraEAN13t.Id = 22
    Me.tlbGeneraEAN13t.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbGeneraEAN13t.Name = "tlbGeneraEAN13t"
    Me.tlbGeneraEAN13t.Visible = True
    '
    'tlbGeneraEAN13p
    '
    Me.tlbGeneraEAN13p.Caption = "Genera EAN 13 a peso"
    Me.tlbGeneraEAN13p.Id = 23
    Me.tlbGeneraEAN13p.Name = "tlbGeneraEAN13p"
    Me.tlbGeneraEAN13p.Visible = True
    '
    'tlbGeneraEAN13v
    '
    Me.tlbGeneraEAN13v.Caption = "Genera EAN 13 a valore"
    Me.tlbGeneraEAN13v.Id = 24
    Me.tlbGeneraEAN13v.Name = "tlbGeneraEAN13v"
    Me.tlbGeneraEAN13v.Visible = True
    '
    'tlbGiftCard
    '
    Me.tlbGiftCard.Caption = "Genera EAN 13 per gift card"
    Me.tlbGiftCard.Id = 28
    Me.tlbGiftCard.Name = "tlbGiftCard"
    Me.tlbGiftCard.Visible = False
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 26
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 27
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'FRMMGBARC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grBarc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGBARC"
    Me.NTSLastControlFocussed = Me.grBarc
    Me.Text = "CODICI A BARRE"
    CType(Me.grBarc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvBarc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttBc_tipo As New DataTable
    Dim dttTmp As New DataTable
    Dim i As Integer
    Dim strCampo As String
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
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      oCleArmd.dttBc_tagl.Columns.Add("cod", GetType(String))
      oCleArmd.dttBc_tagl.Columns.Add("val", GetType(String))
      oCleArmd.dttBc_tagl.AcceptChanges()

      dttTagl = New DataTable
      dttTagl.Columns.Add("cod", GetType(String))
      dttTagl.Columns.Add("val", GetType(String))
      dttTagl.AcceptChanges()

      '-------------------------------------------------
      'carico le unit‡ di misura nella colonna colli: caso particolare
      'carico tutte le unit‡ di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleArmd.BarcCaricaUnitaMisura()
      dttUm.AcceptChanges()

      dttBc_tipo.Columns.Add("cod", GetType(String))
      dttBc_tipo.Columns.Add("val", GetType(String))
      dttBc_tipo.Rows.Add(New Object() {"E", "Ean 13"})
      dttBc_tipo.Rows.Add(New Object() {"C", "Code 39"})
      dttBc_tipo.Rows.Add(New Object() {"2", "2 su 5 interleaved"})
      dttBc_tipo.Rows.Add(New Object() {"8", "Ean 8"})
      dttBc_tipo.Rows.Add(New Object() {"I", "ISBN"})
      dttBc_tipo.Rows.Add(New Object() {"F", "Farmaceutico"})
      dttBc_tipo.Rows.Add(New Object() {"A", "Altro industr."})
      dttBc_tipo.Rows.Add(New Object() {"U", "GTIN\EAN14"})
      dttBc_tipo.AcceptChanges()

      If UCase(oCleArmd.strPrgParent) = "BNTCARTV" Then
        oCleArmd.GetTabTagl(dttTmp)

        For i = 1 To 24
          strCampo = "tb_dest" & Microsoft.VisualBasic.Right("00" & NTSCStr(i), 2)
          If Trim(NTSCStr(dttTmp.Rows(0)(strCampo))) <> "" Then
            dttTagl.Rows.Add(New Object() {NTSCStr(dttTmp.Rows(0)(strCampo)), NTSCStr(dttTmp.Rows(0)(strCampo))})
          End If
        Next
        dttTagl.AcceptChanges()
      End If

      grvBarc.NTSSetParam(oMenu, oApp.Tr(Me, 128828969839942259, "Codici a barre"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969856036215, "Cod. ditta"), 12, False)
      bc_code.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969870567651, "Barcode"), 255, False)
      bc_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828969887286615, "Conf."), dttUm, "tb_codumis", "tb_codumis")
      bc_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969902911815, "Quantit‡"), "#,##0.000", 15)
      bc_datins.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828969921818307, "Data ins."), False)
      bc_orins.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969934787223, "Ora Inserimento"), "0", 9, 0, 999999999)
      bc_datagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828969950256171, "Data Aggiornamento"), False)
      bc_oragg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969973850223, "Ora Aggiornamento"), "0", 9, 0, 999999999)
      bc_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969988225407, "Codice Articolo"), CLN__STD.CodartMaxLen, False)
      bc_tipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828970004319363, "Tipo"), dttBc_tipo, "val", "cod")
      bc_tagl.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828970020257067, "Taglia"), dttTagl, "val", "cod")
      bc_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828970069945203, "Fase"), tabartfasi)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970087445427, "Descr. fase"), 0, True)

      bc_code.NTSSetRichiesto()

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

  Public Overridable Sub Apri()
    Dim dttTmp As New DataTable
    Dim i As Integer
    Dim strCampo As String
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.BarcApri(DittaCorrente, oCleArmd.strBarcCodart, dsBarc) Then Me.Close()

      dcBarc.DataSource = Nothing
      dcBarc.DataSource = dsBarc.Tables("BARCODE")
      grBarc.DataSource = dcBarc

      '----------------------------------------------------------------
      oCleArmd.bIndicod = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Indicod", "0", " ", "0"))
      oCleArmd.strPrefixEAN13 = Trim(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "PrefixEAN13", "", " ", ""))
      oCleArmd.strPrefixEAN13GiftCard = Trim(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "PrefixEAN13GiftCard", "", " ", ""))
      oCleArmd.bBarcodeDerogaQtaBOLL = CBool(NTSCInt(oMenu.GetSettingBus("Bsveboll", "Opzioni", ".", "BarcodeDerogaQta", "0", " ", "0"))) 'se attiva (-1) rileva la quantit‡ indicata nei barcode pero' in deroga a BarcodeConQta (sono alternative)
      oCleArmd.bBarcodeDerogaQtaGSOR = CBool(NTSCInt(oMenu.GetSettingBus("Bsorgsor", "Opzioni", ".", "BarcodeDerogaQta", "0", " ", "0"))) 'se attiva (-1) rileva la quantit‡ indicata nei barcode pero' in deroga a BarcodeConQta (sono alternative)
      oCleArmd.bAbilBarcodeVariabili = CBool(NTSCInt(oMenu.GetSettingBus("BSREGDOC", "Opzioni", ".", "AbilBarcodeVariabili", "0", " ", "0"))) 'se attiva (-1) abilita la getsione scrittura e lettura dei codici barcode ean13 a peso e/o valore
      oCleArmd.bCodartDaBarcode = CBool(Val(oMenu.GetSettingBus("bsmgarti", "Opzioni", ".", "CodartDaBarcode", "0", " ", "0"))) 'NON DOCUMENTARE


      oMenu.ValCodiceDb(oCleArmd.strBarcCodart, DittaCorrente, "ARTICO", "S", , dttTmp)
      If dttTmp.Rows.Count = 0 Then
        If oCleArmd.bIndicod = True Then
          Select Case Len(oCleArmd.strPrefixEAN13)
            Case 0
              oApp.MsgBoxErr(oApp.Tr(Me, 128570376414368522, "L'impostazione di registro 'PrefixEAN13' non Ë corretta," & vbCrLf & _
                "pertanto l'eventuale creazione di Barcode EAN13" & _
                "sar‡ fatta seguendo i normali standard."))
              oCleArmd.bIndicod = False
            Case 1 To 11
              'OK
            Case 12
              oApp.MsgBoxErr(oApp.Tr(Me, 128570376560307890, "L'impostazione di registro 'PrefixEAN13' Ë di 12 caratteri," & vbCrLf & _
                "pertanto l'eventuale creazione di Barcode EAN13" & _
                "sar‡ fatta seguendo i normali standard."))
              oCleArmd.bIndicod = False
            Case Else
              oApp.MsgBoxErr(oApp.Tr(Me, 128570376692184578, "L'impostazione di registro 'PrefixEAN13' supera i 12 caratteri," & vbCrLf & _
                "pertanto l'eventuale creazione di Barcode EAN13" & _
                "sar‡ fatta seguendo i normali standard."))
              oCleArmd.bIndicod = False
          End Select
        End If
      Else
        'Per gli articolo GIFT CARD mostro solo la generazione barcode giftcard
        If NTSCStr(dttTmp.Rows(0)!ar_flgift) = "S" Then
          tlbGeneraEAN8.Visible = False
          tlbGeneraEAN13.Visible = False
          tlbGeneraEAN13p.Visible = False
          tlbGeneraEAN13t.Visible = False
          tlbGeneraEAN13v.Visible = False
          tlbControllaEAN8.Visible = False
          tlbGeneraEAN8t.Visible = False
          GctlSetVisEnab(tlbGiftCard, True)

          If oCleArmd.strPrefixEAN13GiftCard.Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129992582819563095, "L'opzione di registro 'BSMGARTI\OPZIONI\PrefixEAN13GiftCard' non Ë stata configurata." & vbCrLf & "Impossibile abilitare la generazione di barcode per gift card"))
            tlbGiftCard.Enabled = False
          ElseIf oCleArmd.strPrefixEAN13GiftCard = oCleArmd.strPrefixEAN13 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129992583611760445, "L'opzione di registro 'BSMGARTI\OPZIONI\PrefixEAN13GiftCard' non puÚ essere uguale all'opzione 'BSMGARTI\OPZIONI\PrefixEAN13'." & vbCrLf & "Impossibile abilitare la generazione di barcode per gift card"))
            tlbGiftCard.Enabled = False
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se chiamato da BSTCARTV, riempie la colonna realtiva alle taglie e la rende visibile
      '-----------------------------------------------------------------------------------------
      If UCase(oCleArmd.strPrgParent) = "BNTCARTV" Then
        '---------------------------------------------------------------------------------------
        oCleArmd.strBarcPrimaTaglia = ""
        '---------------------------------------------------------------------------------------
        oCleArmd.GetTabTagl(dttTmp)

        If dttTmp.Rows.Count > 0 Then
          For i = 1 To 24
            strCampo = "tb_dest" & Microsoft.VisualBasic.Right("00" & NTSCStr(i), 2)
            If Trim(NTSCStr(dttTmp.Rows(0)(strCampo))) <> "" Then
              If oCleArmd.strBarcPrimaTaglia = "" Then oCleArmd.strBarcPrimaTaglia = NTSCStr(dttTmp.Rows(0)(strCampo))
              oCleArmd.dttBc_tagl.Rows.Add(New Object() {NTSCStr(dttTmp.Rows(0)(strCampo)), NTSCStr(dttTmp.Rows(0)(strCampo))})
            End If
          Next i
        End If

        If oCleArmd.strBarcPrimaTaglia = "" Then
          bc_tagl.Enabled = False
          bc_tagl.Visible = False
          tlbGeneraEAN8t.Visible = False
          tlbGeneraEAN13t.Visible = False
        End If
      Else
        bc_tagl.Enabled = False
        bc_tagl.Visible = False
        tlbGeneraEAN8t.Visible = False
        tlbGeneraEAN13t.Visible = False
      End If
      '-----------------------------------------------------------------------------------------
      If Not oCleArmd.bAbilBarcodeVariabili Then
        tlbGeneraEAN13p.Visible = False
        tlbGeneraEAN13v.Visible = False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo Ë gestito a fasi, rende editabile la colonna della fase
      '--- e pulsante e voce di menu Zoom
      '-----------------------------------------------------------------------------------------
      tlbZoom.Enabled = False
      tlbZoom.Visible = False
      If oCleArmd.bBarcGesfasi = True Then
        If oCleArmd.strPrgParent = "BNMGARTI" Then
          GctlSetVisEnab(tlbZoom, False)
          GctlSetVisEnab(tlbZoom, True)
        End If
        GctlSetVisEnab(bc_fase, False)
        '--------------------------------------------------------------------------------------
      Else
        bc_fase.Enabled = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvBarc.NTSSalvaRigaCorrente(dcBarc, oCleArmd.BarcRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArmd.BarcSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.BarcRipristina(dcBarc.Position, dcBarc.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMMGBARC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        bc_tagl.Visible = False
        bc_fase.Visible = False
        xx_fase.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGBARC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcBarc.Dispose()
      dsBarc.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGBARC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvBarc.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvBarc.NTSDeleteRigaCorrente(dcBarc, True) Then Return
      oCleArmd.BarcSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvBarc.NTSRipristinaRigaCorrenteBefore(dcBarc, True) Then Return
      oCleArmd.BarcRipristina(dcBarc.Position, dcBarc.Filter)
      grvBarc.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

        If grBarc.Focused And grvBarc.FocusedColumn.Name.ToLower = "bc_fase" Then
          'zoom su artfasi
          If grvBarc.NTSGetCurrentDataRow() Is Nothing Then Return
          If Trim(NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_codart)) = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128576495701956559, "Indicare l'articolo prima di inserire la fase."))
            Return
          End If

          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_codart)
          NTSZOOM.strIn = NTSCStr(grvBarc.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvBarc.EditingValue) Then grvBarc.SetFocusedRowCellValue(bc_fase, NTSZOOM.strIn)
          Return

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
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGeneraEAN8_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN8.ItemClick
    Dim strCode As String
    Dim strTipo As String
    Try
      If Not grvBarc.NTSGetCurrentDataRow() Is Nothing Then
        strCode = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_code)
        strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_tipo)
      Else
        strCode = ""
        strTipo = "E"
      End If

      If oCleArmd.GeneraEAN(strCode, strTipo, EAN8) Then
        dsBarc.Tables("BARCODE").Rows.Add(dsBarc.Tables("BARCODE").NewRow)
        With dsBarc.Tables("BARCODE").Rows(dsBarc.Tables("BARCODE").Rows.Count - 1)
          !bc_code = strCode
          !bc_tipo = strTipo
        End With
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGeneraEAN13_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN13.ItemClick
    Dim strCode As String
    Dim strTipo As String
    Try
      If Not grvBarc.NTSGetCurrentDataRow() Is Nothing Then
        strCode = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_code)
        strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_tipo)
      Else
        strCode = ""
        strTipo = "E"
      End If

      If oCleArmd.GeneraEAN(strCode, strTipo, EAN13) Then
        dsBarc.Tables("BARCODE").Rows.Add(dsBarc.Tables("BARCODE").NewRow)
        With dsBarc.Tables("BARCODE").Rows(dsBarc.Tables("BARCODE").Rows.Count - 1)
          !bc_code = strCode
          !bc_tipo = strTipo
        End With
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbControllaEAN8_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbControllaEAN8.ItemClick
    Dim strCode As String
    Dim strTipo As String
    Try
      If grvBarc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      strCode = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_code)
      strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_tipo)

      If oCleArmd.ControllaEAN(strCode, strTipo, EAN8) Then
        dsBarc.Tables("BARCODE").Rows(dcBarc.Position)!bc_code = strCode
        dsBarc.Tables("BARCODE").Rows(dcBarc.Position)!bc_tipo = strTipo
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbControllaEAN13_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbControllaEAN13.ItemClick
    Dim strCode As String
    Dim strTipo As String
    Try
      If grvBarc.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      strCode = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_code)
      strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_tipo)

      If oCleArmd.ControllaEAN(strCode, strTipo, EAN13) Then
        dsBarc.Tables("BARCODE").Rows(dcBarc.Position)!bc_code = strCode
        dsBarc.Tables("BARCODE").Rows(dcBarc.Position)!bc_tipo = strTipo
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGeneraEAN8t_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN8t.ItemClick
    Try
      If Not oCleArmd.RecordGeneraEANPerTaglia(EAN8) Then Exit Sub
      apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGeneraEAN13t_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN13t.ItemClick
    Try
      If Not oCleArmd.RecordGeneraEANPerTaglia(EAN13) Then Exit Sub
      apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbGeneraEAN13p_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN13p.ItemClick
    Try
      GeneraEAN13_p_v(1)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbGeneraEAN13v_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraEAN13v.ItemClick
    Try
      GeneraEAN13_p_v(2)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbGiftCard_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGiftCard.ItemClick
    Dim nBarcode As Integer
    Try
      nBarcode = NTSCInt(oApp.InputBoxNew("Indicare il numero di barcode da generare", "1"))

      If nBarcode = 0 Then Return

      If oCleArmd.GeneraEANGiftCard(nBarcode, EAN13) Then Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvBarc_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvBarc.NTSBeforeRowUpdate
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

  Public Overridable Sub grvBarc_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvBarc.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      AbilitaCellaMatricola()

      dtrT = grvBarc.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        bc_code.Enabled = True
        Return
      End If

      If NTSCStr(dtrT!bc_code) <> " " Then
        bc_code.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvBarc_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvBarc.NTSFocusedColumnChanged
    Try
      AbilitaCellaMatricola()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grBarc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grBarc.Enter
    Try
      AbilitaCellaMatricola()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

  Public Overridable Function AbilitaCellaMatricola() As Boolean
    Try
      '--------------------------------------
      'bug di lettura barcode: se sono su una nuova riga ed il cursore non √® dentro alla cella 
      '(quindi cella con focus ma fursore non lampeggiante)
      'se sparo sul barcode, a volte inferte il primo con secondo carattere del barcode
      'SU CUBE NON SERVE: IL PROBLEMA NON SI VERIFICA
      If oCleArmd Is Nothing Then Return True
      If bInAbilitaCellaEnter Then Return True
      If oCleArmd.bCodartDaBarcode Then
        If Not grvBarc.FocusedColumn Is Nothing AndAlso grvBarc.FocusedColumn.Name = "bc_code" _
           AndAlso bc_code.Enabled AndAlso grvBarc.Enabled Then
          If NTSCStr(grvBarc.GetRowCellValue(grvBarc.FocusedRowHandle, "bc_code")) = "" Then
            bInAbilitaCellaEnter = True
            grvBarc.ShowEditorByMouse()
            If Not grvBarc.ActiveEditor Is Nothing Then
              'grvRighe.ActiveEditor.SelectAll() 'non va!!!!
              NTSSendKeys.Send(0, "{Down}") 'faccio apparire il cursore del mouse
            End If
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bInAbilitaCellaEnter = False
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
      strCrpe = "{BARCODE.codditt} = " & CStrSQL(DittaCorrente) & _
        " And {BARCODE.bc_codart} = " & CStrSQL(oCleArmd.strBarcCodart)
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGARTI", "Reports1", " ", 0, nDestin, "BSMGBARC.RPT", False, "Barcode articolo : " & oCleArmd.strBarcCodart, False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce gi‡ il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub GeneraEAN13_p_v(ByVal nTipo As Integer)
    Dim strCode As String = ""
    Dim strTipo As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not grvBarc.NTSGetCurrentDataRow() Is Nothing Then
        If grvBarc.NTSGetCurrentDataRow.RowState = DataRowState.Unchanged Then Return
        strCode = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_code)
        strTipo = NTSCStr(grvBarc.NTSGetCurrentDataRow()!bc_tipo)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.GeneraEAN13Variabili(strCode, strTipo, nTipo) Then
        '------------------------------------------------------------------------------------------------------------
        If Not grvBarc.NTSGetCurrentDataRow() Is Nothing Then
          If dsBarc.Tables("BARCODE").Rows(dsBarc.Tables("BARCODE").Rows.Count - 1).RowState = DataRowState.Unchanged Then
            dsBarc.Tables("BARCODE").Rows.Add(dsBarc.Tables("BARCODE").NewRow)
          End If
        Else
          dsBarc.Tables("BARCODE").Rows.Add(dsBarc.Tables("BARCODE").NewRow)
        End If
        '------------------------------------------------------------------------------------------------------------
        With dsBarc.Tables("BARCODE").Rows(dsBarc.Tables("BARCODE").Rows.Count - 1)
          !bc_code = strCode
          !bc_tipo = strTipo
        End With
        '------------------------------------------------------------------------------------------------------------
        Salva()
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class