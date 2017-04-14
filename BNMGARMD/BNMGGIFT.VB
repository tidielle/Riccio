Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGIFT
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dttGift As New DataTable
  Public dcGift As New BindingSource()
  Private components As System.ComponentModel.IContainer

  Public bServer As Boolean

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
  Public WithEvents pnGift As NTSInformatica.NTSPanel
  Public WithEvents lbAg_valore As NTSInformatica.NTSLabel
  Public WithEvents edAg_durata As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAg_asaldo As NTSInformatica.NTSCheckBox
  Public WithEvents ckAg_rimbors As NTSInformatica.NTSCheckBox
  Public WithEvents edAg_dtscad As NTSInformatica.NTSTextBoxData
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents lbGiorni As NTSInformatica.NTSLabel
  Public WithEvents edAg_valore As NTSInformatica.NTSTextBoxNum
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

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGIFT))
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
    Me.pnGift = New NTSInformatica.NTSPanel
    Me.lbGiorni = New NTSInformatica.NTSLabel
    Me.ckAg_asaldo = New NTSInformatica.NTSCheckBox
    Me.ckAg_rimbors = New NTSInformatica.NTSCheckBox
    Me.edAg_dtscad = New NTSInformatica.NTSTextBoxData
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.lbAg_valore = New NTSInformatica.NTSLabel
    Me.edAg_durata = New NTSInformatica.NTSTextBoxNum
    Me.edAg_valore = New NTSInformatica.NTSTextBoxNum
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGift, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGift.SuspendLayout()
    CType(Me.ckAg_asaldo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAg_rimbors.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAg_dtscad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAg_durata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAg_valore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.am_codart.NTSRepositoryItemMemo = Nothing
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
    Me.am_codmaga.NTSRepositoryItemMemo = Nothing
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
    Me.xx_codmaga.NTSRepositoryItemMemo = Nothing
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
    Me.am_ubicaz.NTSRepositoryItemMemo = Nothing
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
    Me.am_scomin.NTSRepositoryItemMemo = Nothing
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
    Me.am_scomax.NTSRepositoryItemMemo = Nothing
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
    Me.am_minord.NTSRepositoryItemMemo = Nothing
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
    Me.am_polriord.NTSRepositoryItemMemo = Nothing
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
    Me.am_ultagg.NTSRepositoryItemMemo = Nothing
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
    Me.am_opnome.NTSRepositoryItemMemo = Nothing
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
    Me.am_sublotto.NTSRepositoryItemMemo = Nothing
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
    Me.am_maxlotto.NTSRepositoryItemMemo = Nothing
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
    Me.am_perragg.NTSRepositoryItemMemo = Nothing
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
    Me.am_ggragg.NTSRepositoryItemMemo = Nothing
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
    Me.am_fase.NTSRepositoryItemMemo = Nothing
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
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 12
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
    'pnGift
    '
    Me.pnGift.AllowDrop = True
    Me.pnGift.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGift.Appearance.Options.UseBackColor = True
    Me.pnGift.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGift.Controls.Add(Me.lbGiorni)
    Me.pnGift.Controls.Add(Me.ckAg_asaldo)
    Me.pnGift.Controls.Add(Me.ckAg_rimbors)
    Me.pnGift.Controls.Add(Me.edAg_dtscad)
    Me.pnGift.Controls.Add(Me.NtsLabel1)
    Me.pnGift.Controls.Add(Me.lbAg_valore)
    Me.pnGift.Controls.Add(Me.edAg_durata)
    Me.pnGift.Controls.Add(Me.edAg_valore)
    Me.pnGift.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGift.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGift.Location = New System.Drawing.Point(0, 26)
    Me.pnGift.Name = "pnGift"
    Me.pnGift.NTSActiveTrasparency = True
    Me.pnGift.Size = New System.Drawing.Size(473, 102)
    Me.pnGift.TabIndex = 4
    Me.pnGift.Text = "NtsPanel1"
    '
    'lbGiorni
    '
    Me.lbGiorni.AutoSize = True
    Me.lbGiorni.BackColor = System.Drawing.Color.Transparent
    Me.lbGiorni.Location = New System.Drawing.Point(280, 32)
    Me.lbGiorni.Name = "lbGiorni"
    Me.lbGiorni.NTSDbField = ""
    Me.lbGiorni.Size = New System.Drawing.Size(97, 13)
    Me.lbGiorni.TabIndex = 31
    Me.lbGiorni.Text = "giorni o fino a data"
    Me.lbGiorni.Tooltip = ""
    Me.lbGiorni.UseMnemonic = False
    '
    'ckAg_asaldo
    '
    Me.ckAg_asaldo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAg_asaldo.Location = New System.Drawing.Point(12, 77)
    Me.ckAg_asaldo.Name = "ckAg_asaldo"
    Me.ckAg_asaldo.NTSCheckValue = "S"
    Me.ckAg_asaldo.NTSUnCheckValue = "N"
    Me.ckAg_asaldo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAg_asaldo.Properties.Appearance.Options.UseBackColor = True
    Me.ckAg_asaldo.Properties.Caption = "La gift card può essere evasa solo a saldo"
    Me.ckAg_asaldo.Size = New System.Drawing.Size(229, 19)
    Me.ckAg_asaldo.TabIndex = 30
    '
    'ckAg_rimbors
    '
    Me.ckAg_rimbors.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAg_rimbors.Location = New System.Drawing.Point(12, 52)
    Me.ckAg_rimbors.Name = "ckAg_rimbors"
    Me.ckAg_rimbors.NTSCheckValue = "S"
    Me.ckAg_rimbors.NTSUnCheckValue = "N"
    Me.ckAg_rimbors.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAg_rimbors.Properties.Appearance.Options.UseBackColor = True
    Me.ckAg_rimbors.Properties.Caption = "La gift card può essere rimborsata per la parte residua"
    Me.ckAg_rimbors.Size = New System.Drawing.Size(287, 19)
    Me.ckAg_rimbors.TabIndex = 30
    Me.ckAg_rimbors.Visible = False
    '
    'edAg_dtscad
    '
    Me.edAg_dtscad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAg_dtscad.Location = New System.Drawing.Point(379, 29)
    Me.edAg_dtscad.Name = "edAg_dtscad"
    Me.edAg_dtscad.NTSDbField = ""
    Me.edAg_dtscad.NTSForzaVisZoom = False
    Me.edAg_dtscad.NTSOldValue = ""
    Me.edAg_dtscad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAg_dtscad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAg_dtscad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAg_dtscad.Properties.MaxLength = 65536
    Me.edAg_dtscad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAg_dtscad.Size = New System.Drawing.Size(82, 20)
    Me.edAg_dtscad.TabIndex = 29
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(12, 32)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(221, 13)
    Me.NtsLabel1.TabIndex = 28
    Me.NtsLabel1.Text = "Dalla data di vendita la gift card è valida per "
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'lbAg_valore
    '
    Me.lbAg_valore.AutoSize = True
    Me.lbAg_valore.BackColor = System.Drawing.Color.Transparent
    Me.lbAg_valore.Location = New System.Drawing.Point(12, 9)
    Me.lbAg_valore.Name = "lbAg_valore"
    Me.lbAg_valore.NTSDbField = ""
    Me.lbAg_valore.Size = New System.Drawing.Size(37, 13)
    Me.lbAg_valore.TabIndex = 28
    Me.lbAg_valore.Text = "Valore"
    Me.lbAg_valore.Tooltip = ""
    Me.lbAg_valore.UseMnemonic = False
    '
    'edAg_durata
    '
    Me.edAg_durata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAg_durata.Location = New System.Drawing.Point(234, 29)
    Me.edAg_durata.Name = "edAg_durata"
    Me.edAg_durata.NTSDbField = ""
    Me.edAg_durata.NTSFormat = "0"
    Me.edAg_durata.NTSForzaVisZoom = False
    Me.edAg_durata.NTSOldValue = ""
    Me.edAg_durata.Properties.Appearance.Options.UseTextOptions = True
    Me.edAg_durata.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAg_durata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAg_durata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAg_durata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAg_durata.Properties.MaxLength = 65536
    Me.edAg_durata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAg_durata.Size = New System.Drawing.Size(40, 20)
    Me.edAg_durata.TabIndex = 27
    '
    'edAg_valore
    '
    Me.edAg_valore.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAg_valore.Location = New System.Drawing.Point(55, 6)
    Me.edAg_valore.Name = "edAg_valore"
    Me.edAg_valore.NTSDbField = ""
    Me.edAg_valore.NTSFormat = "0"
    Me.edAg_valore.NTSForzaVisZoom = False
    Me.edAg_valore.NTSOldValue = ""
    Me.edAg_valore.Properties.Appearance.Options.UseTextOptions = True
    Me.edAg_valore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAg_valore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAg_valore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAg_valore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAg_valore.Properties.MaxLength = 65536
    Me.edAg_valore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAg_valore.Size = New System.Drawing.Size(85, 20)
    Me.edAg_valore.TabIndex = 27
    '
    'FRMMGGIFT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(473, 128)
    Me.Controls.Add(Me.pnGift)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGGIFT"
    Me.Text = "GIFT CARD"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGift, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGift.ResumeLayout(False)
    Me.pnGift.PerformLayout()
    CType(Me.ckAg_asaldo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAg_rimbors.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAg_dtscad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAg_durata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAg_valore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

      ckAg_asaldo.NTSSetParam(oMenu, oApp.Tr(Me, 129991996148399374, "La gift card può essere evasa solo a saldo"), "S", "N")
      ckAg_rimbors.NTSSetParam(oMenu, oApp.Tr(Me, 129991996148555623, "La gift card può essere rimborsata per la parte residua"), "S", "N")
      edAg_dtscad.NTSSetParam(oMenu, oApp.Tr(Me, 129991996148711872, "Data scadenza"), False)
      edAg_durata.NTSSetParam(oMenu, oApp.Tr(Me, 129991996149180619, "Durata"), "0", 4, 0, 9999)
      edAg_valore.NTSSetParam(oMenu, oApp.Tr(Me, 129991996149336868, "Valore"), "#,##0.00", 6, 0, 999999)

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
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleArmd.GiftRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308290936, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleArmd.GiftSalva(False) Then Return False
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)
      '-------------------------------------------------
      ckAg_asaldo.NTSText.NTSDbField = "ARTICOGIFT.ag_asaldo"
      ckAg_rimbors.NTSText.NTSDbField = "ARTICOGIFT.ag_rimbors"
      edAg_dtscad.NTSDbField = "ARTICOGIFT.ag_dtscad"
      edAg_durata.NTSDbField = "ARTICOGIFT.ag_durata"
      edAg_valore.NTSDbField = "ARTICOGIFT.ag_valore"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGift, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRMMGGIFT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.GiftApri(DittaCorrente, oCallParams.strPar2, dttGift) Then Me.Close()

      dcGift.DataSource = dttGift
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGGIFT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGift.Dispose()
      dttGift.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGGIFT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128569602749918943, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dttGift.Rows.Count = 1 And dttGift.Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleArmd.GiftRipristina(dcGift.Position, dcGift.Filter)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGift, Me)
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
End Class