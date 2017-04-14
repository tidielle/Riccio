Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGARMA
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsArma As New DataSet
  Public dcArma As New BindingSource()
  Public dttArma As DataTable = Nothing
  Public dttRelease As New DataTable
  Public dtrArma() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public bServer As Boolean

  Public WithEvents grArma As NTSInformatica.NTSGrid
  Public WithEvents grvArma As NTSInformatica.NTSGridView
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
  Public WithEvents am_codart As NTSInformatica.NTSGridColumn
  Public WithEvents am_codmaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codmaga As NTSInformatica.NTSGridColumn
  Public WithEvents am_scomin As NTSInformatica.NTSGridColumn
  Public WithEvents am_scomax As NTSInformatica.NTSGridColumn
  Public WithEvents am_minord As NTSInformatica.NTSGridColumn
  Public WithEvents am_polriord As NTSInformatica.NTSGridColumn
  Public WithEvents am_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents am_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents am_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents am_sublotto As NTSInformatica.NTSGridColumn
  Public WithEvents am_maxlotto As NTSInformatica.NTSGridColumn
  Public WithEvents am_perragg As NTSInformatica.NTSGridColumn
  Public WithEvents am_ggragg As NTSInformatica.NTSGridColumn
  Public WithEvents am_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents am_ubicst As NTSInformatica.NTSGridColumn
  Public WithEvents am_ubicpr As NTSInformatica.NTSGridColumn
  Public WithEvents am_ubicri As NTSInformatica.NTSGridColumn
  Public WithEvents am_ubicus As NTSInformatica.NTSGridColumn
  Public WithEvents am_indrot As NTSInformatica.NTSGridColumn
  Public WithEvents am_scominpk As NTSInformatica.NTSGridColumn
  Public WithEvents tlbRecordDispVariante As NTSInformatica.NTSBarButtonItem
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
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
    oCleArmd.bArmaGesfasi = oCallParams.bPar1
    oCleArmd.strArmaUltfase = oCallParams.strPar2
    oCleArmd.strArmaCodart = oCallParams.strPar3
    oCleArmd.nArmaCodtagl = NTSCInt(oCallParams.dPar1)

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARMA))
    Me.grArma = New NTSInformatica.NTSGrid
    Me.grvArma = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.am_codart = New NTSInformatica.NTSGridColumn
    Me.am_codmaga = New NTSInformatica.NTSGridColumn
    Me.xx_codmaga = New NTSInformatica.NTSGridColumn
    Me.am_ubicaz = New NTSInformatica.NTSGridColumn
    Me.am_scomin = New NTSInformatica.NTSGridColumn
    Me.am_scomax = New NTSInformatica.NTSGridColumn
    Me.am_minord = New NTSInformatica.NTSGridColumn
    Me.am_polriord = New NTSInformatica.NTSGridColumn
    Me.am_ultagg = New NTSInformatica.NTSGridColumn
    Me.am_opnome = New NTSInformatica.NTSGridColumn
    Me.am_sublotto = New NTSInformatica.NTSGridColumn
    Me.am_maxlotto = New NTSInformatica.NTSGridColumn
    Me.am_perragg = New NTSInformatica.NTSGridColumn
    Me.am_ggragg = New NTSInformatica.NTSGridColumn
    Me.am_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.am_ubicst = New NTSInformatica.NTSGridColumn
    Me.am_ubicpr = New NTSInformatica.NTSGridColumn
    Me.am_ubicri = New NTSInformatica.NTSGridColumn
    Me.am_ubicus = New NTSInformatica.NTSGridColumn
    Me.am_indrot = New NTSInformatica.NTSGridColumn
    Me.am_scominpk = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordDispVariante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grArma, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvArma, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'grArma
    '
    Me.grArma.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grArma.EmbeddedNavigator.Name = ""
    Me.grArma.Location = New System.Drawing.Point(0, 30)
    Me.grArma.MainView = Me.grvArma
    Me.grArma.Name = "grArma"
    Me.grArma.Size = New System.Drawing.Size(667, 336)
    Me.grArma.TabIndex = 0
    Me.grArma.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvArma})
    '
    'grvArma
    '
    Me.grvArma.ActiveFilterEnabled = False
    Me.grvArma.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.am_codart, Me.am_codmaga, Me.xx_codmaga, Me.am_ubicaz, Me.am_scomin, Me.am_scomax, Me.am_minord, Me.am_polriord, Me.am_ultagg, Me.am_opnome, Me.am_sublotto, Me.am_maxlotto, Me.am_perragg, Me.am_ggragg, Me.am_fase, Me.xx_fase, Me.am_ubicpr, Me.am_ubicri, Me.am_ubicst, Me.am_ubicus, Me.am_indrot, Me.am_scominpk})
    Me.grvArma.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvArma.Enabled = True
    Me.grvArma.GridControl = Me.grArma
    Me.grvArma.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvArma.Name = "grvArma"
    Me.grvArma.NTSAllowDelete = True
    Me.grvArma.NTSAllowInsert = True
    Me.grvArma.NTSAllowUpdate = True
    Me.grvArma.NTSMenuContext = Nothing
    Me.grvArma.OptionsCustomization.AllowRowSizing = True
    Me.grvArma.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvArma.OptionsNavigation.UseTabKey = False
    Me.grvArma.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvArma.OptionsView.ColumnAutoWidth = False
    Me.grvArma.OptionsView.EnableAppearanceEvenRow = True
    Me.grvArma.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvArma.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvArma.OptionsView.ShowGroupPanel = False
    Me.grvArma.RowHeight = 16
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
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'am_codart
    '
    Me.am_codart.AppearanceCell.Options.UseBackColor = True
    Me.am_codart.AppearanceCell.Options.UseTextOptions = True
    Me.am_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_codart.Caption = "Codice articolo"
    Me.am_codart.Enabled = False
    Me.am_codart.FieldName = "am_codart"
    Me.am_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_codart.Name = "am_codart"
    Me.am_codart.NTSRepositoryComboBox = Nothing
    Me.am_codart.NTSRepositoryItemCheck = Nothing
    Me.am_codart.NTSRepositoryItemText = Nothing
    Me.am_codart.OptionsColumn.AllowEdit = False
    Me.am_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_codart.OptionsColumn.ReadOnly = True
    Me.am_codart.OptionsFilter.AllowFilter = False
    '
    'am_codmaga
    '
    Me.am_codmaga.AppearanceCell.Options.UseBackColor = True
    Me.am_codmaga.AppearanceCell.Options.UseTextOptions = True
    Me.am_codmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_codmaga.Caption = "Codice magazzino"
    Me.am_codmaga.Enabled = True
    Me.am_codmaga.FieldName = "am_codmaga"
    Me.am_codmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_codmaga.Name = "am_codmaga"
    Me.am_codmaga.NTSRepositoryComboBox = Nothing
    Me.am_codmaga.NTSRepositoryItemCheck = Nothing
    Me.am_codmaga.NTSRepositoryItemText = Nothing
    Me.am_codmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_codmaga.OptionsFilter.AllowFilter = False
    Me.am_codmaga.Visible = True
    Me.am_codmaga.VisibleIndex = 0
    '
    'xx_codmaga
    '
    Me.xx_codmaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codmaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codmaga.Caption = "Magazzino"
    Me.xx_codmaga.Enabled = False
    Me.xx_codmaga.FieldName = "xx_codmaga"
    Me.xx_codmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codmaga.Name = "xx_codmaga"
    Me.xx_codmaga.NTSRepositoryComboBox = Nothing
    Me.xx_codmaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codmaga.NTSRepositoryItemText = Nothing
    Me.xx_codmaga.OptionsColumn.AllowEdit = False
    Me.xx_codmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codmaga.OptionsColumn.ReadOnly = True
    Me.xx_codmaga.OptionsFilter.AllowFilter = False
    Me.xx_codmaga.Visible = True
    Me.xx_codmaga.VisibleIndex = 1
    '
    'am_ubicaz
    '
    Me.am_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.am_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.am_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ubicaz.Caption = "Ubicazione magazzino"
    Me.am_ubicaz.Enabled = True
    Me.am_ubicaz.FieldName = "am_ubicaz"
    Me.am_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ubicaz.Name = "am_ubicaz"
    Me.am_ubicaz.NTSRepositoryComboBox = Nothing
    Me.am_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.am_ubicaz.NTSRepositoryItemText = Nothing
    Me.am_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ubicaz.OptionsFilter.AllowFilter = False
    Me.am_ubicaz.Visible = True
    Me.am_ubicaz.VisibleIndex = 2
    '
    'am_scomin
    '
    Me.am_scomin.AppearanceCell.Options.UseBackColor = True
    Me.am_scomin.AppearanceCell.Options.UseTextOptions = True
    Me.am_scomin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_scomin.Caption = "Scorta minima"
    Me.am_scomin.Enabled = True
    Me.am_scomin.FieldName = "am_scomin"
    Me.am_scomin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_scomin.Name = "am_scomin"
    Me.am_scomin.NTSRepositoryComboBox = Nothing
    Me.am_scomin.NTSRepositoryItemCheck = Nothing
    Me.am_scomin.NTSRepositoryItemText = Nothing
    Me.am_scomin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_scomin.OptionsFilter.AllowFilter = False
    Me.am_scomin.Visible = True
    Me.am_scomin.VisibleIndex = 3
    '
    'am_scomax
    '
    Me.am_scomax.AppearanceCell.Options.UseBackColor = True
    Me.am_scomax.AppearanceCell.Options.UseTextOptions = True
    Me.am_scomax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_scomax.Caption = "Scorta massima"
    Me.am_scomax.Enabled = True
    Me.am_scomax.FieldName = "am_scomax"
    Me.am_scomax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_scomax.Name = "am_scomax"
    Me.am_scomax.NTSRepositoryComboBox = Nothing
    Me.am_scomax.NTSRepositoryItemCheck = Nothing
    Me.am_scomax.NTSRepositoryItemText = Nothing
    Me.am_scomax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_scomax.OptionsFilter.AllowFilter = False
    Me.am_scomax.Visible = True
    Me.am_scomax.VisibleIndex = 4
    '
    'am_minord
    '
    Me.am_minord.AppearanceCell.Options.UseBackColor = True
    Me.am_minord.AppearanceCell.Options.UseTextOptions = True
    Me.am_minord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_minord.Caption = "Lotto std prod"
    Me.am_minord.Enabled = True
    Me.am_minord.FieldName = "am_minord"
    Me.am_minord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_minord.Name = "am_minord"
    Me.am_minord.NTSRepositoryComboBox = Nothing
    Me.am_minord.NTSRepositoryItemCheck = Nothing
    Me.am_minord.NTSRepositoryItemText = Nothing
    Me.am_minord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_minord.OptionsFilter.AllowFilter = False
    Me.am_minord.Visible = True
    Me.am_minord.VisibleIndex = 5
    '
    'am_polriord
    '
    Me.am_polriord.AppearanceCell.Options.UseBackColor = True
    Me.am_polriord.AppearanceCell.Options.UseTextOptions = True
    Me.am_polriord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_polriord.Caption = "Politica riordino"
    Me.am_polriord.Enabled = True
    Me.am_polriord.FieldName = "am_polriord"
    Me.am_polriord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_polriord.Name = "am_polriord"
    Me.am_polriord.NTSRepositoryComboBox = Nothing
    Me.am_polriord.NTSRepositoryItemCheck = Nothing
    Me.am_polriord.NTSRepositoryItemText = Nothing
    Me.am_polriord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_polriord.OptionsFilter.AllowFilter = False
    Me.am_polriord.Visible = True
    Me.am_polriord.VisibleIndex = 6
    '
    'am_ultagg
    '
    Me.am_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.am_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.am_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ultagg.Caption = "Ultimo agg "
    Me.am_ultagg.Enabled = True
    Me.am_ultagg.FieldName = "am_ultagg"
    Me.am_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ultagg.Name = "am_ultagg"
    Me.am_ultagg.NTSRepositoryComboBox = Nothing
    Me.am_ultagg.NTSRepositoryItemCheck = Nothing
    Me.am_ultagg.NTSRepositoryItemText = Nothing
    Me.am_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ultagg.OptionsFilter.AllowFilter = False
    '
    'am_opnome
    '
    Me.am_opnome.AppearanceCell.Options.UseBackColor = True
    Me.am_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.am_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_opnome.Caption = "Operatore"
    Me.am_opnome.Enabled = True
    Me.am_opnome.FieldName = "am_opnome"
    Me.am_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_opnome.Name = "am_opnome"
    Me.am_opnome.NTSRepositoryComboBox = Nothing
    Me.am_opnome.NTSRepositoryItemCheck = Nothing
    Me.am_opnome.NTSRepositoryItemText = Nothing
    Me.am_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_opnome.OptionsFilter.AllowFilter = False
    '
    'am_sublotto
    '
    Me.am_sublotto.AppearanceCell.Options.UseBackColor = True
    Me.am_sublotto.AppearanceCell.Options.UseTextOptions = True
    Me.am_sublotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_sublotto.Caption = "Sottolotto"
    Me.am_sublotto.Enabled = True
    Me.am_sublotto.FieldName = "am_sublotto"
    Me.am_sublotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_sublotto.Name = "am_sublotto"
    Me.am_sublotto.NTSRepositoryComboBox = Nothing
    Me.am_sublotto.NTSRepositoryItemCheck = Nothing
    Me.am_sublotto.NTSRepositoryItemText = Nothing
    Me.am_sublotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_sublotto.OptionsFilter.AllowFilter = False
    Me.am_sublotto.Visible = True
    Me.am_sublotto.VisibleIndex = 7
    '
    'am_maxlotto
    '
    Me.am_maxlotto.AppearanceCell.Options.UseBackColor = True
    Me.am_maxlotto.AppearanceCell.Options.UseTextOptions = True
    Me.am_maxlotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_maxlotto.Caption = "Lotto massimo"
    Me.am_maxlotto.Enabled = True
    Me.am_maxlotto.FieldName = "am_maxlotto"
    Me.am_maxlotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_maxlotto.Name = "am_maxlotto"
    Me.am_maxlotto.NTSRepositoryComboBox = Nothing
    Me.am_maxlotto.NTSRepositoryItemCheck = Nothing
    Me.am_maxlotto.NTSRepositoryItemText = Nothing
    Me.am_maxlotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_maxlotto.OptionsFilter.AllowFilter = False
    Me.am_maxlotto.Visible = True
    Me.am_maxlotto.VisibleIndex = 8
    '
    'am_perragg
    '
    Me.am_perragg.AppearanceCell.Options.UseBackColor = True
    Me.am_perragg.AppearanceCell.Options.UseTextOptions = True
    Me.am_perragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_perragg.Caption = "Periodo Raggr."
    Me.am_perragg.Enabled = True
    Me.am_perragg.FieldName = "am_perragg"
    Me.am_perragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_perragg.Name = "am_perragg"
    Me.am_perragg.NTSRepositoryComboBox = Nothing
    Me.am_perragg.NTSRepositoryItemCheck = Nothing
    Me.am_perragg.NTSRepositoryItemText = Nothing
    Me.am_perragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_perragg.OptionsFilter.AllowFilter = False
    Me.am_perragg.Visible = True
    Me.am_perragg.VisibleIndex = 9
    '
    'am_ggragg
    '
    Me.am_ggragg.AppearanceCell.Options.UseBackColor = True
    Me.am_ggragg.AppearanceCell.Options.UseTextOptions = True
    Me.am_ggragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ggragg.Caption = "Giorni Raggr."
    Me.am_ggragg.Enabled = True
    Me.am_ggragg.FieldName = "am_ggragg"
    Me.am_ggragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ggragg.Name = "am_ggragg"
    Me.am_ggragg.NTSRepositoryComboBox = Nothing
    Me.am_ggragg.NTSRepositoryItemCheck = Nothing
    Me.am_ggragg.NTSRepositoryItemText = Nothing
    Me.am_ggragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ggragg.OptionsFilter.AllowFilter = False
    Me.am_ggragg.Visible = True
    Me.am_ggragg.VisibleIndex = 10
    '
    'am_fase
    '
    Me.am_fase.AppearanceCell.Options.UseBackColor = True
    Me.am_fase.AppearanceCell.Options.UseTextOptions = True
    Me.am_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_fase.Caption = "Fase"
    Me.am_fase.Enabled = True
    Me.am_fase.FieldName = "am_fase"
    Me.am_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_fase.Name = "am_fase"
    Me.am_fase.NTSRepositoryComboBox = Nothing
    Me.am_fase.NTSRepositoryItemCheck = Nothing
    Me.am_fase.NTSRepositoryItemText = Nothing
    Me.am_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_fase.OptionsFilter.AllowFilter = False
    Me.am_fase.Visible = True
    Me.am_fase.VisibleIndex = 11
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descrizione Fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 12
    '
    'am_ubicst
    '
    Me.am_ubicst.AppearanceCell.Options.UseBackColor = True
    Me.am_ubicst.AppearanceCell.Options.UseTextOptions = True
    Me.am_ubicst.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ubicst.Caption = "Ubicazione stoccaggio"
    Me.am_ubicst.Enabled = True
    Me.am_ubicst.FieldName = "am_ubicst"
    Me.am_ubicst.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ubicst.Name = "am_ubicst"
    Me.am_ubicst.NTSRepositoryComboBox = Nothing
    Me.am_ubicst.NTSRepositoryItemCheck = Nothing
    Me.am_ubicst.NTSRepositoryItemText = Nothing
    Me.am_ubicst.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ubicst.OptionsFilter.AllowFilter = False
    Me.am_ubicst.Visible = True
    Me.am_ubicst.VisibleIndex = 11
    '
    'am_ubicpr
    '
    Me.am_ubicpr.AppearanceCell.Options.UseBackColor = True
    Me.am_ubicpr.AppearanceCell.Options.UseTextOptions = True
    Me.am_ubicpr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ubicpr.Caption = "Ubicazione prelievo"
    Me.am_ubicpr.Enabled = True
    Me.am_ubicpr.FieldName = "am_ubicpr"
    Me.am_ubicpr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ubicpr.Name = "am_ubicpr"
    Me.am_ubicpr.NTSRepositoryComboBox = Nothing
    Me.am_ubicpr.NTSRepositoryItemCheck = Nothing
    Me.am_ubicpr.NTSRepositoryItemText = Nothing
    Me.am_ubicpr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ubicpr.OptionsFilter.AllowFilter = False
    Me.am_ubicpr.Visible = True
    Me.am_ubicpr.VisibleIndex = 11
    '
    'am_ubicri
    '
    Me.am_ubicri.AppearanceCell.Options.UseBackColor = True
    Me.am_ubicri.AppearanceCell.Options.UseTextOptions = True
    Me.am_ubicri.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ubicri.Caption = "Ubicazione ricevimento"
    Me.am_ubicri.Enabled = True
    Me.am_ubicri.FieldName = "am_ubicri"
    Me.am_ubicri.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ubicri.Name = "am_ubicri"
    Me.am_ubicri.NTSRepositoryComboBox = Nothing
    Me.am_ubicri.NTSRepositoryItemCheck = Nothing
    Me.am_ubicri.NTSRepositoryItemText = Nothing
    Me.am_ubicri.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ubicri.OptionsFilter.AllowFilter = False
    Me.am_ubicri.Visible = True
    Me.am_ubicri.VisibleIndex = 11
    '
    'am_ubicus
    '
    Me.am_ubicus.AppearanceCell.Options.UseBackColor = True
    Me.am_ubicus.AppearanceCell.Options.UseTextOptions = True
    Me.am_ubicus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_ubicus.Caption = "Ubicazione uscita"
    Me.am_ubicus.Enabled = True
    Me.am_ubicus.FieldName = "am_ubicus"
    Me.am_ubicus.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_ubicus.Name = "am_ubicus"
    Me.am_ubicus.NTSRepositoryComboBox = Nothing
    Me.am_ubicus.NTSRepositoryItemCheck = Nothing
    Me.am_ubicus.NTSRepositoryItemText = Nothing
    Me.am_ubicus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_ubicus.OptionsFilter.AllowFilter = False
    Me.am_ubicus.Visible = True
    Me.am_ubicus.VisibleIndex = 11
    '
    'am_indrot
    '
    Me.am_indrot.AppearanceCell.Options.UseBackColor = True
    Me.am_indrot.AppearanceCell.Options.UseTextOptions = True
    Me.am_indrot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_indrot.Caption = "Indice rotazione"
    Me.am_indrot.Enabled = True
    Me.am_indrot.FieldName = "am_indrot"
    Me.am_indrot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_indrot.Name = "am_indrot"
    Me.am_indrot.NTSRepositoryComboBox = Nothing
    Me.am_indrot.NTSRepositoryItemCheck = Nothing
    Me.am_indrot.NTSRepositoryItemText = Nothing
    Me.am_indrot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_indrot.OptionsFilter.AllowFilter = False
    Me.am_indrot.Visible = True
    Me.am_indrot.VisibleIndex = 11
    '
    'am_scominpk
    '
    Me.am_scominpk.AppearanceCell.Options.UseBackColor = True
    Me.am_scominpk.AppearanceCell.Options.UseTextOptions = True
    Me.am_scominpk.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_scominpk.Caption = "Scorta minima picking"
    Me.am_scominpk.Enabled = True
    Me.am_scominpk.FieldName = "am_scominpk"
    Me.am_scominpk.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_scominpk.Name = "am_scominpk"
    Me.am_scominpk.NTSRepositoryComboBox = Nothing
    Me.am_scominpk.NTSRepositoryItemCheck = Nothing
    Me.am_scominpk.NTSRepositoryItemText = Nothing
    Me.am_scominpk.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_scominpk.OptionsFilter.AllowFilter = False
    Me.am_scominpk.Visible = True
    Me.am_scominpk.VisibleIndex = 11
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbRecordDispVariante})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordDispVariante, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbRecordDispVariante
    '
    Me.tlbRecordDispVariante.Caption = "Visualizza disponibilità variante/magazzino"
    Me.tlbRecordDispVariante.Glyph = CType(resources.GetObject("tlbRecordDispVariante.Glyph"), System.Drawing.Image)
    Me.tlbRecordDispVariante.Id = 11
    Me.tlbRecordDispVariante.Name = "tlbRecordDispVariante"
    Me.tlbRecordDispVariante.Visible = True
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
    'FRMMGARMA
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grArma)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGARMA"
    Me.NTSLastControlFocussed = Me.grArma
    Me.Text = "Articoli per magazzino"
    CType(Me.grArma, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvArma, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAm_polriord As New DataTable()
    Dim dttAm_perragg As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbRecordDispVariante.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttAm_polriord.Columns.Add("cod", GetType(String))
      dttAm_polriord.Columns.Add("val", GetType(String))
      dttAm_polriord.Rows.Add(New Object() {"F", "Su fabbisogno con lotto"})
      dttAm_polriord.Rows.Add(New Object() {"G", "Su fabbisogno puro"})
      dttAm_polriord.Rows.Add(New Object() {"M", "A punto di riordino con lotto"})
      dttAm_polriord.Rows.Add(New Object() {"N", "A punto di riord. a ric.scorta"})
      dttAm_polriord.Rows.Add(New Object() {"O", "Su fabb. con lotto minimo"})
      dttAm_polriord.AcceptChanges()

      dttAm_perragg.Columns.Add("cod", GetType(String))
      dttAm_perragg.Columns.Add("val", GetType(String))
      dttAm_perragg.Rows.Add(New Object() {"G", "Giorno"})
      dttAm_perragg.Rows.Add(New Object() {"S", "Settimana"})
      dttAm_perragg.Rows.Add(New Object() {"D", "Decade"})
      dttAm_perragg.Rows.Add(New Object() {"Q", "Quindicina"})
      dttAm_perragg.Rows.Add(New Object() {"M", "Mese"})
      dttAm_perragg.Rows.Add(New Object() {"B", "Bimestre"})
      dttAm_perragg.Rows.Add(New Object() {"T", "Trimestre"})
      dttAm_perragg.Rows.Add(New Object() {"R", "Quadrimestre"})
      dttAm_perragg.Rows.Add(New Object() {"U", "Semestre"})
      dttAm_perragg.Rows.Add(New Object() {"A", "Anno"})
      dttAm_perragg.AcceptChanges()

      grvArma.NTSSetParam(oMenu, oApp.Tr(Me, 128828969355717311, "Articoli per magazzino"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969392749035, "Cod. ditta"), 12, False)
      am_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969408842991, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      am_codmaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828969422124411, "Codice magazzino"), tabmaga)
      xx_codmaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969437593359, "Magazzino"), 0, True)
      am_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969450562275, "Ubicazione magazzino"), 18, True)
      am_scomin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969466031223, "Scorta minima"), "#,##0.000", 15, 0, 9999999999)
      am_scomax.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969479781399, "Scorta massima"), "#,##0.000", 15, 0, 9999999999)
      am_minord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969493844079, "Lotto std prod"), "#,##0.000", 15, 0, 9999999999)
      am_polriord.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828969507125499, "Politica riordino"), dttAm_polriord, "val", "cod")
      am_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828969522906951, "Ultimo agg "), False)
      am_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969538532151, "Operatore"), 20, False)
      am_sublotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969552594831, "Sottolotto"), "#,##0.000", 15, 0, 9999999999)
      am_maxlotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969567282519, "Lotto massimo"), "#,##0.000", 15, 0, 9999999999)
      am_perragg.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828969583532727, "Periodo Raggr."), dttAm_perragg, "val", "cod")
      am_ggragg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969598220415, "Giorni Raggr."), "0", 3, 0, 999)
      am_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828969611345583, "Fase"), tabartfasi)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969627283287, "Descrizione Fase"), 0, True)
      am_ubicpr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128939618224927933, "Ubicazione prelievo"), 18, False)
      am_ubicri.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128939618225084188, "Ubicazione ricevimento"), 18, False)
      am_ubicst.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128939618225240443, "Ubicazione stoccaggio"), 18, False)
      am_ubicus.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128939618225396698, "Ubicazione uscita"), 18, False)
      am_indrot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128939618225552953, "Indice rotazione"), "#,##0.00", 15)
      am_scominpk.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128939618225709208, "Scorta minima picking"), "#,##0.00", 15)

      am_ubicaz.NTSForzaVisZoom = CBool(oCleArmd.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME)

      am_codmaga.NTSSetRichiesto()

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

  Public Overridable Function Salva() As Boolean
    Dim strCodart As String = ""
    Dim strCodmaga As String = ""
    Dim strFase As String = ""
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvArma.NTSSalvaRigaCorrente(dcArma, oCleArmd.ArmaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          With dsArma.Tables("ARTMAGA").Rows(dcArma.Position)
            strCodart = NTSCStr(!am_codart)
            strCodmaga = NTSCStr(!am_codmaga)
            strFase = NTSCStr(!am_fase)
          End With

          If Not oCleArmd.ArmaSalva(False, strCodart, strCodmaga, strFase) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.ArmaRipristina(dcArma.Position, dcArma.Filter)
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
  Public Overridable Sub FRMMGARMA_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------
      If Not CheckUltfase() Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128563795351585295, "Impostare l'ultima fase in anagrafica articoli."))
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGARMA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.ArmaApri(DittaCorrente, oCleArmd.strArmaCodart, dsArma) Then Me.Close()

      dcArma.DataSource = dsArma.Tables("ARTMAGA")
      grArma.DataSource = dcArma

      grvArma_NTSFocusedRowChanged(Nothing, Nothing)

      '-----------------------------------------------------------------------------------------
      '--- Se la modale non è chimata da 'Anagrafica articoli - Taglie e colori -'
      '--- rende non visibile il tasto deella ToolBar relativo a
      '--- 'Visualizza disponibilità per taglia'
      '-----------------------------------------------------------------------------------------
      Select Case UCase(oCleArmd.strPrgParent)
        Case "BNTCARTV"
        Case Else : tlbRecordDispVariante.Visible = False
      End Select

      If oCleArmd.bArmaGesfasi Then     'fasi e varianti
        am_fase.Enabled = True
      Else
        am_fase.Enabled = False
      End If

      oMenu.GetBusRelease(dttRelease)

      ' aggiungo le nuove colonne
      If NTSCInt(dttRelease.Rows(0)!rel_maior) >= 15 Or (NTSCInt(dttRelease.Rows(0)!rel_maior) = 14 And NTSCStr(dttRelease.Rows(0)!rel_pers) <> "") Then
        am_ubicst.Visible = True
        am_ubicri.Visible = True
        am_ubicus.Visible = True
        am_ubicpr.Visible = True
        am_indrot.Visible = True
        am_scominpk.Visible = True
      Else
        am_ubicst.Visible = False
        am_ubicri.Visible = False
        am_ubicus.Visible = False
        am_ubicpr.Visible = False
        am_indrot.Visible = False
        am_scominpk.Visible = False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Per Euro2000:
      '--- Se attiva l'opzione, il valore minimo impostato per i campio Scorta Minima/Massima/Qtà lotto/std
      '--- con Politica di riordino, è zero
      '-----------------------------------------------------------------------------------------
      oCleArmd.bNoMsgCongruenzaPolSconte = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "NoMsgCongruenzaPolSconte", "0", " ", "0")))
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
  Public Overridable Sub FRMMGARMA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArma.Dispose()
      dsArma.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGARMA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvArma.NTSNuovo()
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
    Dim strCodart As String = ""
    Dim strCodmaga As String = ""
    Dim strFase As String = ""
    Try
      With dsArma.Tables("ARTMAGA").Rows(dcArma.Position)
        strCodart = NTSCStr(!am_codart)
        strCodmaga = NTSCStr(!am_codmaga)
        strFase = NTSCStr(!am_fase)
      End With
      If Not grvArma.NTSDeleteRigaCorrente(dcArma, True) Then Return
      oCleArmd.ArmaSalva(True, strCodart, strCodmaga, strFase)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvArma.NTSRipristinaRigaCorrenteBefore(dcArma, True) Then Return
      oCleArmd.ArmaRipristina(dcArma.Position, dcArma.Filter)
      grvArma.NTSRipristinaRigaCorrenteAfter()
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

        If grArma.Focused And grvArma.FocusedColumn.Name.ToLower = "am_fase" Then
          'zoom su artfasi
          If grvArma.NTSGetCurrentDataRow() Is Nothing Then Return
          If Trim(NTSCStr(grvArma.NTSGetCurrentDataRow()!am_codart)) = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128576466350788023, "Indicare l'articolo prima di inserire la fase."))
            Return
          End If

          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strTipo = NTSCStr(grvArma.NTSGetCurrentDataRow()!am_codart)
          NTSZOOM.strIn = NTSCStr(grvArma.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvArma.EditingValue) Then grvArma.SetFocusedRowCellValue(am_fase, NTSZOOM.strIn)
          Return
        ElseIf grArma.Focused And grvArma.FocusedColumn.Name = "am_ubicaz" Then
          If grvArma.NTSGetCurrentDataRow Is Nothing Then Return
          If Not CBool(oCleArmd.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) Then Return

          oCallParams = New CLE__CLDP
          oCallParams.strPar1 = ""
          oCallParams.strPar2 = ""
          oCallParams.dPar1 = NTSCInt(grvArma.NTSGetCurrentDataRow!am_codmaga)
          oCallParams.bPar1 = False

          'Chiamo la dll
          oMenu.RunChild("NTSInformatica", "FRMRMSIUB", oApp.Tr(Me, 128885197022015890, "Situazione"), DittaCorrente, "", "BNRMSIUB", oCallParams, Nothing, True, True)

          If oCallParams.strPar3.Trim("-"c).Trim <> "" Then grvArma.NTSGetCurrentDataRow!am_ubicaz = oCallParams.strPar3.PadRight(12, "-"c)
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

  Public Overridable Sub tlbRecordDispVariante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordDispVariante.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If Not Salva() Then Exit Sub

      If grvArma.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      oPar.strPar1 = "BNMGARMA"
      oPar.bPar1 = False
      oPar.strPar2 = oCleArmd.strArmaCodart
      oPar.dPar1 = oCleArmd.nArmaCodtagl
      oPar.dPar2 = NTSCInt(grvArma.NTSGetCurrentDataRow()!am_codmaga)
      oPar.dPar3 = NTSCInt(grvArma.NTSGetCurrentDataRow()!am_fase)

      oMenu.RunChild("NTSInformatica", "FRMTCSCOR", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

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
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvArma_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvArma.NTSBeforeRowUpdate
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

  Public Overridable Sub grvArma_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvArma.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      dtrT = grvArma.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        am_codmaga.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!am_codmaga) <> 0 Then
        am_codmaga.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function CheckUltfase() As Boolean
    Try
      If oCleArmd.bArmaGesfasi Then
        If oCleArmd.strArmaUltfase = "0" Then
          Return False
        End If
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class