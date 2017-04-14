Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLAT_F
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsHlat As New DataSet
  Public dcHlat As New BindingSource()
  Public dttHlat As DataTable = Nothing
  Public dtrHlat() As DataRow = Nothing

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
  Public WithEvents pnHlat As NTSInformatica.NTSPanel
  Public WithEvents fmEsist As NTSInformatica.NTSGroupBox
  Public WithEvents edApx_vgiaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbApx_giaini As NTSInformatica.NTSLabel
  Public WithEvents edApx_giaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbApx_esist As NTSInformatica.NTSLabel
  Public WithEvents edApx_esist As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbQuantità As NTSInformatica.NTSLabel
  Public WithEvents lbValore As NTSInformatica.NTSLabel
  Public WithEvents lbXx_disp As NTSInformatica.NTSLabel
  Public WithEvents edXx_disp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbApx_dtulsca As NTSInformatica.NTSLabel
  Public WithEvents lbApx_dtulcar As NTSInformatica.NTSLabel
  Public WithEvents lbApx_ultpre As NTSInformatica.NTSLabel
  Public WithEvents edApx_ultpre As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_costmed As NTSInformatica.NTSLabel
  Public WithEvents edXx_costmed As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbApx_peucos As NTSInformatica.NTSLabel
  Public WithEvents edApx_peucos As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbApx_ultcos As NTSInformatica.NTSLabel
  Public WithEvents edApx_ultcos As NTSInformatica.NTSTextBoxNum
  Public WithEvents edApx_dtulsca As NTSInformatica.NTSTextBoxData
  Public WithEvents edApx_dtulcar As NTSInformatica.NTSTextBoxData
  Public WithEvents edXx_impegtot As NTSInformatica.NTSTextBoxNum
  Public WithEvents edXx_vimpegtot As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_impegtot As NTSInformatica.NTSLabel
  Public WithEvents edXx_vordintot As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_ordintot As NTSInformatica.NTSLabel
  Public WithEvents edXx_ordintot As NTSInformatica.NTSTextBoxNum
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
    oCleArmd.dHlatPerqta = oCallParams.dPar1
    oCleArmd.strHlatCodart = oCallParams.strPar2
    oCleArmd.dHlatFase = oCallParams.dPar2

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGHLAT_F))
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
    Me.pnHlat = New NTSInformatica.NTSPanel
    Me.edApx_dtulsca = New NTSInformatica.NTSTextBoxData
    Me.edApx_dtulcar = New NTSInformatica.NTSTextBoxData
    Me.lbXx_disp = New NTSInformatica.NTSLabel
    Me.edXx_disp = New NTSInformatica.NTSTextBoxNum
    Me.lbApx_dtulsca = New NTSInformatica.NTSLabel
    Me.lbApx_dtulcar = New NTSInformatica.NTSLabel
    Me.lbApx_ultpre = New NTSInformatica.NTSLabel
    Me.edApx_ultpre = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_costmed = New NTSInformatica.NTSLabel
    Me.edXx_costmed = New NTSInformatica.NTSTextBoxNum
    Me.lbApx_peucos = New NTSInformatica.NTSLabel
    Me.edApx_peucos = New NTSInformatica.NTSTextBoxNum
    Me.lbApx_ultcos = New NTSInformatica.NTSLabel
    Me.edApx_ultcos = New NTSInformatica.NTSTextBoxNum
    Me.fmEsist = New NTSInformatica.NTSGroupBox
    Me.edXx_impegtot = New NTSInformatica.NTSTextBoxNum
    Me.edXx_vimpegtot = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_impegtot = New NTSInformatica.NTSLabel
    Me.edXx_vordintot = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_ordintot = New NTSInformatica.NTSLabel
    Me.lbValore = New NTSInformatica.NTSLabel
    Me.edXx_ordintot = New NTSInformatica.NTSTextBoxNum
    Me.lbQuantità = New NTSInformatica.NTSLabel
    Me.edApx_vgiaini = New NTSInformatica.NTSTextBoxNum
    Me.lbApx_giaini = New NTSInformatica.NTSLabel
    Me.edApx_giaini = New NTSInformatica.NTSTextBoxNum
    Me.lbApx_esist = New NTSInformatica.NTSLabel
    Me.edApx_esist = New NTSInformatica.NTSTextBoxNum
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnHlat, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHlat.SuspendLayout()
    CType(Me.edApx_dtulsca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_dtulcar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_disp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_ultpre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_costmed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_peucos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_ultcos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmEsist, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEsist.SuspendLayout()
    CType(Me.edXx_impegtot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_vimpegtot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_vordintot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_ordintot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_vgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_giaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApx_esist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnHlat
    '
    Me.pnHlat.AllowDrop = True
    Me.pnHlat.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHlat.Appearance.Options.UseBackColor = True
    Me.pnHlat.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHlat.Controls.Add(Me.edApx_dtulsca)
    Me.pnHlat.Controls.Add(Me.edApx_dtulcar)
    Me.pnHlat.Controls.Add(Me.lbXx_disp)
    Me.pnHlat.Controls.Add(Me.edXx_disp)
    Me.pnHlat.Controls.Add(Me.lbApx_dtulsca)
    Me.pnHlat.Controls.Add(Me.lbApx_dtulcar)
    Me.pnHlat.Controls.Add(Me.lbApx_ultpre)
    Me.pnHlat.Controls.Add(Me.edApx_ultpre)
    Me.pnHlat.Controls.Add(Me.lbXx_costmed)
    Me.pnHlat.Controls.Add(Me.edXx_costmed)
    Me.pnHlat.Controls.Add(Me.lbApx_peucos)
    Me.pnHlat.Controls.Add(Me.edApx_peucos)
    Me.pnHlat.Controls.Add(Me.lbApx_ultcos)
    Me.pnHlat.Controls.Add(Me.edApx_ultcos)
    Me.pnHlat.Controls.Add(Me.fmEsist)
    Me.pnHlat.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHlat.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHlat.Location = New System.Drawing.Point(0, 30)
    Me.pnHlat.Name = "pnHlat"
    Me.pnHlat.NTSActiveTrasparency = True
    Me.pnHlat.Size = New System.Drawing.Size(634, 173)
    Me.pnHlat.TabIndex = 4
    Me.pnHlat.Text = "NtsPanel1"
    '
    'edApx_dtulsca
    '
    Me.edApx_dtulsca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_dtulsca.Location = New System.Drawing.Point(521, 120)
    Me.edApx_dtulsca.Name = "edApx_dtulsca"
    Me.edApx_dtulsca.NTSDbField = ""
    Me.edApx_dtulsca.NTSForzaVisZoom = False
    Me.edApx_dtulsca.NTSOldValue = ""
    Me.edApx_dtulsca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_dtulsca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_dtulsca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_dtulsca.Properties.MaxLength = 65536
    Me.edApx_dtulsca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_dtulsca.Size = New System.Drawing.Size(91, 20)
    Me.edApx_dtulsca.TabIndex = 46
    '
    'edApx_dtulcar
    '
    Me.edApx_dtulcar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_dtulcar.Location = New System.Drawing.Point(521, 98)
    Me.edApx_dtulcar.Name = "edApx_dtulcar"
    Me.edApx_dtulcar.NTSDbField = ""
    Me.edApx_dtulcar.NTSForzaVisZoom = False
    Me.edApx_dtulcar.NTSOldValue = ""
    Me.edApx_dtulcar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_dtulcar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_dtulcar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_dtulcar.Properties.MaxLength = 65536
    Me.edApx_dtulcar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_dtulcar.Size = New System.Drawing.Size(91, 20)
    Me.edApx_dtulcar.TabIndex = 45
    '
    'lbXx_disp
    '
    Me.lbXx_disp.AutoSize = True
    Me.lbXx_disp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_disp.Location = New System.Drawing.Point(341, 145)
    Me.lbXx_disp.Name = "lbXx_disp"
    Me.lbXx_disp.NTSDbField = ""
    Me.lbXx_disp.Size = New System.Drawing.Size(63, 13)
    Me.lbXx_disp.TabIndex = 40
    Me.lbXx_disp.Text = "Disponibilità"
    Me.lbXx_disp.Tooltip = ""
    Me.lbXx_disp.UseMnemonic = False
    '
    'edXx_disp
    '
    Me.edXx_disp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_disp.Location = New System.Drawing.Point(521, 142)
    Me.edXx_disp.Name = "edXx_disp"
    Me.edXx_disp.NTSDbField = ""
    Me.edXx_disp.NTSFormat = "0"
    Me.edXx_disp.NTSForzaVisZoom = False
    Me.edXx_disp.NTSOldValue = ""
    Me.edXx_disp.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_disp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_disp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_disp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_disp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_disp.Properties.MaxLength = 65536
    Me.edXx_disp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_disp.Size = New System.Drawing.Size(100, 20)
    Me.edXx_disp.TabIndex = 39
    '
    'lbApx_dtulsca
    '
    Me.lbApx_dtulsca.AutoSize = True
    Me.lbApx_dtulsca.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_dtulsca.Location = New System.Drawing.Point(341, 123)
    Me.lbApx_dtulsca.Name = "lbApx_dtulsca"
    Me.lbApx_dtulsca.NTSDbField = ""
    Me.lbApx_dtulsca.Size = New System.Drawing.Size(152, 13)
    Me.lbApx_dtulsca.TabIndex = 38
    Me.lbApx_dtulsca.Text = "Data ultimo scarico valorizzato"
    Me.lbApx_dtulsca.Tooltip = ""
    Me.lbApx_dtulsca.UseMnemonic = False
    '
    'lbApx_dtulcar
    '
    Me.lbApx_dtulcar.AutoSize = True
    Me.lbApx_dtulcar.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_dtulcar.Location = New System.Drawing.Point(341, 101)
    Me.lbApx_dtulcar.Name = "lbApx_dtulcar"
    Me.lbApx_dtulcar.NTSDbField = ""
    Me.lbApx_dtulcar.Size = New System.Drawing.Size(147, 13)
    Me.lbApx_dtulcar.TabIndex = 36
    Me.lbApx_dtulcar.Text = "Data ultimo carico valorizzato"
    Me.lbApx_dtulcar.Tooltip = ""
    Me.lbApx_dtulcar.UseMnemonic = False
    '
    'lbApx_ultpre
    '
    Me.lbApx_ultpre.AutoSize = True
    Me.lbApx_ultpre.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_ultpre.Location = New System.Drawing.Point(341, 79)
    Me.lbApx_ultpre.Name = "lbApx_ultpre"
    Me.lbApx_ultpre.NTSDbField = ""
    Me.lbApx_ultpre.Size = New System.Drawing.Size(71, 13)
    Me.lbApx_ultpre.TabIndex = 34
    Me.lbApx_ultpre.Text = "Ultimo prezzo"
    Me.lbApx_ultpre.Tooltip = ""
    Me.lbApx_ultpre.UseMnemonic = False
    '
    'edApx_ultpre
    '
    Me.edApx_ultpre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_ultpre.Location = New System.Drawing.Point(521, 76)
    Me.edApx_ultpre.Name = "edApx_ultpre"
    Me.edApx_ultpre.NTSDbField = ""
    Me.edApx_ultpre.NTSFormat = "0"
    Me.edApx_ultpre.NTSForzaVisZoom = False
    Me.edApx_ultpre.NTSOldValue = ""
    Me.edApx_ultpre.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_ultpre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_ultpre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_ultpre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_ultpre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_ultpre.Properties.MaxLength = 65536
    Me.edApx_ultpre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_ultpre.Size = New System.Drawing.Size(100, 20)
    Me.edApx_ultpre.TabIndex = 33
    '
    'lbXx_costmed
    '
    Me.lbXx_costmed.AutoSize = True
    Me.lbXx_costmed.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_costmed.Location = New System.Drawing.Point(341, 57)
    Me.lbXx_costmed.Name = "lbXx_costmed"
    Me.lbXx_costmed.NTSDbField = ""
    Me.lbXx_costmed.Size = New System.Drawing.Size(66, 13)
    Me.lbXx_costmed.TabIndex = 32
    Me.lbXx_costmed.Text = "Costo medio"
    Me.lbXx_costmed.Tooltip = ""
    Me.lbXx_costmed.UseMnemonic = False
    '
    'edXx_costmed
    '
    Me.edXx_costmed.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_costmed.Location = New System.Drawing.Point(521, 54)
    Me.edXx_costmed.Name = "edXx_costmed"
    Me.edXx_costmed.NTSDbField = ""
    Me.edXx_costmed.NTSFormat = "0"
    Me.edXx_costmed.NTSForzaVisZoom = False
    Me.edXx_costmed.NTSOldValue = ""
    Me.edXx_costmed.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_costmed.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_costmed.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_costmed.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_costmed.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_costmed.Properties.MaxLength = 65536
    Me.edXx_costmed.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_costmed.Size = New System.Drawing.Size(100, 20)
    Me.edXx_costmed.TabIndex = 31
    '
    'lbApx_peucos
    '
    Me.lbApx_peucos.AutoSize = True
    Me.lbApx_peucos.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_peucos.Location = New System.Drawing.Point(341, 35)
    Me.lbApx_peucos.Name = "lbApx_peucos"
    Me.lbApx_peucos.NTSDbField = ""
    Me.lbApx_peucos.Size = New System.Drawing.Size(160, 13)
    Me.lbApx_peucos.TabIndex = 30
    Me.lbApx_peucos.Text = "Ultimo costo + spese accessorie"
    Me.lbApx_peucos.Tooltip = ""
    Me.lbApx_peucos.UseMnemonic = False
    '
    'edApx_peucos
    '
    Me.edApx_peucos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_peucos.Location = New System.Drawing.Point(521, 32)
    Me.edApx_peucos.Name = "edApx_peucos"
    Me.edApx_peucos.NTSDbField = ""
    Me.edApx_peucos.NTSFormat = "0"
    Me.edApx_peucos.NTSForzaVisZoom = False
    Me.edApx_peucos.NTSOldValue = ""
    Me.edApx_peucos.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_peucos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_peucos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_peucos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_peucos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_peucos.Properties.MaxLength = 65536
    Me.edApx_peucos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_peucos.Size = New System.Drawing.Size(100, 20)
    Me.edApx_peucos.TabIndex = 29
    '
    'lbApx_ultcos
    '
    Me.lbApx_ultcos.AutoSize = True
    Me.lbApx_ultcos.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_ultcos.Location = New System.Drawing.Point(341, 13)
    Me.lbApx_ultcos.Name = "lbApx_ultcos"
    Me.lbApx_ultcos.NTSDbField = ""
    Me.lbApx_ultcos.Size = New System.Drawing.Size(65, 13)
    Me.lbApx_ultcos.TabIndex = 28
    Me.lbApx_ultcos.Text = "Ultimo costo"
    Me.lbApx_ultcos.Tooltip = ""
    Me.lbApx_ultcos.UseMnemonic = False
    '
    'edApx_ultcos
    '
    Me.edApx_ultcos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_ultcos.Location = New System.Drawing.Point(521, 10)
    Me.edApx_ultcos.Name = "edApx_ultcos"
    Me.edApx_ultcos.NTSDbField = ""
    Me.edApx_ultcos.NTSFormat = "0"
    Me.edApx_ultcos.NTSForzaVisZoom = False
    Me.edApx_ultcos.NTSOldValue = ""
    Me.edApx_ultcos.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_ultcos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_ultcos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_ultcos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_ultcos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_ultcos.Properties.MaxLength = 65536
    Me.edApx_ultcos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_ultcos.Size = New System.Drawing.Size(100, 20)
    Me.edApx_ultcos.TabIndex = 27
    '
    'fmEsist
    '
    Me.fmEsist.AllowDrop = True
    Me.fmEsist.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEsist.Appearance.Options.UseBackColor = True
    Me.fmEsist.Controls.Add(Me.edXx_impegtot)
    Me.fmEsist.Controls.Add(Me.edXx_vimpegtot)
    Me.fmEsist.Controls.Add(Me.lbXx_impegtot)
    Me.fmEsist.Controls.Add(Me.edXx_vordintot)
    Me.fmEsist.Controls.Add(Me.lbXx_ordintot)
    Me.fmEsist.Controls.Add(Me.lbValore)
    Me.fmEsist.Controls.Add(Me.edXx_ordintot)
    Me.fmEsist.Controls.Add(Me.lbQuantità)
    Me.fmEsist.Controls.Add(Me.edApx_vgiaini)
    Me.fmEsist.Controls.Add(Me.lbApx_giaini)
    Me.fmEsist.Controls.Add(Me.edApx_giaini)
    Me.fmEsist.Controls.Add(Me.lbApx_esist)
    Me.fmEsist.Controls.Add(Me.edApx_esist)
    Me.fmEsist.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEsist.Location = New System.Drawing.Point(12, 10)
    Me.fmEsist.Name = "fmEsist"
    Me.fmEsist.Size = New System.Drawing.Size(322, 118)
    Me.fmEsist.TabIndex = 0
    '
    'edXx_impegtot
    '
    Me.edXx_impegtot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_impegtot.Location = New System.Drawing.Point(123, 68)
    Me.edXx_impegtot.Name = "edXx_impegtot"
    Me.edXx_impegtot.NTSDbField = ""
    Me.edXx_impegtot.NTSFormat = "0"
    Me.edXx_impegtot.NTSForzaVisZoom = False
    Me.edXx_impegtot.NTSOldValue = ""
    Me.edXx_impegtot.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_impegtot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_impegtot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_impegtot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_impegtot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_impegtot.Properties.MaxLength = 65536
    Me.edXx_impegtot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_impegtot.Size = New System.Drawing.Size(80, 20)
    Me.edXx_impegtot.TabIndex = 50
    '
    'edXx_vimpegtot
    '
    Me.edXx_vimpegtot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_vimpegtot.Location = New System.Drawing.Point(209, 68)
    Me.edXx_vimpegtot.Name = "edXx_vimpegtot"
    Me.edXx_vimpegtot.NTSDbField = ""
    Me.edXx_vimpegtot.NTSFormat = "0"
    Me.edXx_vimpegtot.NTSForzaVisZoom = False
    Me.edXx_vimpegtot.NTSOldValue = ""
    Me.edXx_vimpegtot.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_vimpegtot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_vimpegtot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_vimpegtot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_vimpegtot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_vimpegtot.Properties.MaxLength = 65536
    Me.edXx_vimpegtot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_vimpegtot.Size = New System.Drawing.Size(100, 20)
    Me.edXx_vimpegtot.TabIndex = 52
    '
    'lbXx_impegtot
    '
    Me.lbXx_impegtot.AutoSize = True
    Me.lbXx_impegtot.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_impegtot.Location = New System.Drawing.Point(9, 72)
    Me.lbXx_impegtot.Name = "lbXx_impegtot"
    Me.lbXx_impegtot.NTSDbField = ""
    Me.lbXx_impegtot.Size = New System.Drawing.Size(59, 13)
    Me.lbXx_impegtot.TabIndex = 51
    Me.lbXx_impegtot.Text = "Impegnato"
    Me.lbXx_impegtot.Tooltip = ""
    Me.lbXx_impegtot.UseMnemonic = False
    '
    'edXx_vordintot
    '
    Me.edXx_vordintot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_vordintot.Location = New System.Drawing.Point(209, 46)
    Me.edXx_vordintot.Name = "edXx_vordintot"
    Me.edXx_vordintot.NTSDbField = ""
    Me.edXx_vordintot.NTSFormat = "0"
    Me.edXx_vordintot.NTSForzaVisZoom = False
    Me.edXx_vordintot.NTSOldValue = ""
    Me.edXx_vordintot.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_vordintot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_vordintot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_vordintot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_vordintot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_vordintot.Properties.MaxLength = 65536
    Me.edXx_vordintot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_vordintot.Size = New System.Drawing.Size(100, 20)
    Me.edXx_vordintot.TabIndex = 49
    '
    'lbXx_ordintot
    '
    Me.lbXx_ordintot.AutoSize = True
    Me.lbXx_ordintot.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ordintot.Location = New System.Drawing.Point(9, 49)
    Me.lbXx_ordintot.Name = "lbXx_ordintot"
    Me.lbXx_ordintot.NTSDbField = ""
    Me.lbXx_ordintot.Size = New System.Drawing.Size(49, 13)
    Me.lbXx_ordintot.TabIndex = 48
    Me.lbXx_ordintot.Text = "Ordinato"
    Me.lbXx_ordintot.Tooltip = ""
    Me.lbXx_ordintot.UseMnemonic = False
    '
    'lbValore
    '
    Me.lbValore.AutoSize = True
    Me.lbValore.BackColor = System.Drawing.Color.Transparent
    Me.lbValore.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbValore.Location = New System.Drawing.Point(236, 3)
    Me.lbValore.Name = "lbValore"
    Me.lbValore.NTSDbField = ""
    Me.lbValore.Size = New System.Drawing.Size(50, 13)
    Me.lbValore.TabIndex = 26
    Me.lbValore.Text = "VALORE"
    Me.lbValore.Tooltip = ""
    Me.lbValore.UseMnemonic = False
    '
    'edXx_ordintot
    '
    Me.edXx_ordintot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_ordintot.Location = New System.Drawing.Point(123, 46)
    Me.edXx_ordintot.Name = "edXx_ordintot"
    Me.edXx_ordintot.NTSDbField = ""
    Me.edXx_ordintot.NTSFormat = "0"
    Me.edXx_ordintot.NTSForzaVisZoom = False
    Me.edXx_ordintot.NTSOldValue = ""
    Me.edXx_ordintot.Properties.Appearance.Options.UseTextOptions = True
    Me.edXx_ordintot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edXx_ordintot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_ordintot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_ordintot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_ordintot.Properties.MaxLength = 65536
    Me.edXx_ordintot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_ordintot.Size = New System.Drawing.Size(80, 20)
    Me.edXx_ordintot.TabIndex = 47
    '
    'lbQuantità
    '
    Me.lbQuantità.AutoSize = True
    Me.lbQuantità.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantità.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbQuantità.Location = New System.Drawing.Point(127, 3)
    Me.lbQuantità.Name = "lbQuantità"
    Me.lbQuantità.NTSDbField = ""
    Me.lbQuantità.Size = New System.Drawing.Size(68, 13)
    Me.lbQuantità.TabIndex = 25
    Me.lbQuantità.Text = "QUANTITA'"
    Me.lbQuantità.Tooltip = ""
    Me.lbQuantità.UseMnemonic = False
    '
    'edApx_vgiaini
    '
    Me.edApx_vgiaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_vgiaini.Location = New System.Drawing.Point(209, 90)
    Me.edApx_vgiaini.Name = "edApx_vgiaini"
    Me.edApx_vgiaini.NTSDbField = ""
    Me.edApx_vgiaini.NTSFormat = "0"
    Me.edApx_vgiaini.NTSForzaVisZoom = False
    Me.edApx_vgiaini.NTSOldValue = ""
    Me.edApx_vgiaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_vgiaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_vgiaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_vgiaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_vgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_vgiaini.Properties.MaxLength = 65536
    Me.edApx_vgiaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_vgiaini.Size = New System.Drawing.Size(100, 20)
    Me.edApx_vgiaini.TabIndex = 23
    '
    'lbApx_giaini
    '
    Me.lbApx_giaini.AutoSize = True
    Me.lbApx_giaini.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_giaini.Location = New System.Drawing.Point(9, 93)
    Me.lbApx_giaini.Name = "lbApx_giaini"
    Me.lbApx_giaini.NTSDbField = ""
    Me.lbApx_giaini.Size = New System.Drawing.Size(84, 13)
    Me.lbApx_giaini.TabIndex = 13
    Me.lbApx_giaini.Text = "Giacenza iniziale"
    Me.lbApx_giaini.Tooltip = ""
    Me.lbApx_giaini.UseMnemonic = False
    '
    'edApx_giaini
    '
    Me.edApx_giaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_giaini.Location = New System.Drawing.Point(123, 90)
    Me.edApx_giaini.Name = "edApx_giaini"
    Me.edApx_giaini.NTSDbField = ""
    Me.edApx_giaini.NTSFormat = "0"
    Me.edApx_giaini.NTSForzaVisZoom = False
    Me.edApx_giaini.NTSOldValue = ""
    Me.edApx_giaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_giaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_giaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_giaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_giaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_giaini.Properties.MaxLength = 65536
    Me.edApx_giaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_giaini.Size = New System.Drawing.Size(80, 20)
    Me.edApx_giaini.TabIndex = 12
    '
    'lbApx_esist
    '
    Me.lbApx_esist.AutoSize = True
    Me.lbApx_esist.BackColor = System.Drawing.Color.Transparent
    Me.lbApx_esist.Location = New System.Drawing.Point(9, 27)
    Me.lbApx_esist.Name = "lbApx_esist"
    Me.lbApx_esist.NTSDbField = ""
    Me.lbApx_esist.Size = New System.Drawing.Size(52, 13)
    Me.lbApx_esist.TabIndex = 1
    Me.lbApx_esist.Text = "Esistenza"
    Me.lbApx_esist.Tooltip = ""
    Me.lbApx_esist.UseMnemonic = False
    '
    'edApx_esist
    '
    Me.edApx_esist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApx_esist.Location = New System.Drawing.Point(123, 24)
    Me.edApx_esist.Name = "edApx_esist"
    Me.edApx_esist.NTSDbField = ""
    Me.edApx_esist.NTSFormat = "0"
    Me.edApx_esist.NTSForzaVisZoom = False
    Me.edApx_esist.NTSOldValue = ""
    Me.edApx_esist.Properties.Appearance.Options.UseTextOptions = True
    Me.edApx_esist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApx_esist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApx_esist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApx_esist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApx_esist.Properties.MaxLength = 65536
    Me.edApx_esist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApx_esist.Size = New System.Drawing.Size(80, 20)
    Me.edApx_esist.TabIndex = 0
    '
    'FRMMGHLAT_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(634, 203)
    Me.Controls.Add(Me.pnHlat)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGHLAT_F"
    Me.Text = "PROGRESSIVI TOTALI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnHlat, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHlat.ResumeLayout(False)
    Me.pnHlat.PerformLayout()
    CType(Me.edApx_dtulsca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_dtulcar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_disp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_ultpre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_costmed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_peucos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_ultcos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmEsist, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEsist.ResumeLayout(False)
    Me.fmEsist.PerformLayout()
    CType(Me.edXx_impegtot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_vimpegtot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_vordintot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_ordintot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_vgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_giaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApx_esist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

      edApx_dtulsca.NTSSetParam(oMenu, oApp.Tr(Me, 128569607816443899, "Data ultimo scarico valorizzato"), True)
      edApx_dtulcar.NTSSetParam(oMenu, oApp.Tr(Me, 128569607816600858, "Data ultimo carico valorizzato"), True)
      edXx_disp.NTSSetParam(oMenu, oApp.Tr(Me, 128569607817542612, "Disponibilità"), oApp.FormatQta)
      edApx_ultpre.NTSSetParam(oMenu, oApp.Tr(Me, 128569607818170448, "Ultimo prezzo"), TrovaFmtPrz)
      edXx_costmed.NTSSetParam(oMenu, oApp.Tr(Me, 128569607818484366, "Costo medio"), TrovaFmtPrz)
      edApx_peucos.NTSSetParam(oMenu, oApp.Tr(Me, 128569607818798284, "Ult. costo + spese acc."), TrovaFmtPrz)
      edApx_ultcos.NTSSetParam(oMenu, oApp.Tr(Me, 128569607819112202, "Ultimo costo"), TrovaFmtPrz)
      edApx_vgiaini.NTSSetParam(oMenu, oApp.Tr(Me, 128569607819896997, "Giacenza iniziale"), oApp.FormatImporti)
      edApx_giaini.NTSSetParam(oMenu, oApp.Tr(Me, 128569607821309628, "Giacenza iniziale"), oApp.FormatQta)
      edApx_esist.NTSSetParam(oMenu, oApp.Tr(Me, 128569607823193136, "Esistenza"), oApp.FormatQta)

      edXx_impegtot.NTSSetParam(oMenu, oApp.Tr(Me, 129822532667862930, "Impegnato totale"), oApp.FormatQta)
      edXx_vimpegtot.NTSSetParam(oMenu, oApp.Tr(Me, 129822532680894957, "Impegnato totale"), oApp.FormatImporti)
      edXx_ordintot.NTSSetParam(oMenu, oApp.Tr(Me, 129822532692770982, "Ordinato totale"), oApp.FormatQta)
      edXx_vordintot.NTSSetParam(oMenu, oApp.Tr(Me, 129822532709571028, "Ordinato totale"), oApp.FormatImporti)

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

      If oCleArmd.HlatRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308290936, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleArmd.HlatSalva(False) Then Return False
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
      'collego il BindingSource ai vari controlli 

      edApx_dtulsca.NTSDbField = "artprox.apx_dtulsca"
      edApx_dtulcar.NTSDbField = "artprox.apx_dtulcar"
      edXx_disp.NTSDbField = "artprox.xx_disp"
      edApx_ultpre.NTSDbField = "artprox.apx_ultpre"
      edXx_costmed.NTSDbField = "artprox.xx_costmed"
      edApx_peucos.NTSDbField = "artprox.apx_peucos"
      edApx_ultcos.NTSDbField = "artprox.apx_ultcos"
      edApx_vgiaini.NTSDbField = "artprox.apx_vgiaini"
      edApx_giaini.NTSDbField = "artprox.apx_giaini"
      edApx_esist.NTSDbField = "artprox.apx_esist"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcHlat, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRMMGARMA_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGHLAT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.HlatApri(DittaCorrente, oCleArmd.strHlatCodart, dsHlat) Then Me.Close()

      dcHlat.DataSource = dsHlat.Tables("ARTPROX")
      dsHlat.AcceptChanges()

      If dsHlat.Tables("ARTPROX").Rows.Count = 0 Then
        oCleArmd.HlatNuovo()
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcHlat.ResetBindings(False)
      dcHlat.MoveFirst()

      'If oCleArmd.strPrgParent = "BNMGARTV" Then
      '  tlbSalva.Visible = False
      '  tlbRipristina.Visible = False
      '  edApx_ultcos.Enabled = False
      '  edApx_peucos.Enabled = False
      '  edApx_ultpre.Enabled = False
      '  edApx_dtulcar.Enabled = False
      '  edApx_dtulsca.Enabled = False
      'End If

      edApx_esist.Enabled = False
      edApx_giaini.Enabled = False
      edApx_vgiaini.Enabled = False

      edXx_costmed.Enabled = False
      edXx_disp.Enabled = False

      edXx_impegtot.Enabled = False
      edXx_vimpegtot.Enabled = False
      edXx_ordintot.Enabled = False
      edXx_vordintot.Enabled = False

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
  Public Overridable Sub FRMMGHLAT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHlat.Dispose()
      dsHlat.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGHLAT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
          If dsHlat.Tables("ARTPROX").Rows.Count = 1 And dsHlat.Tables("ARTPROX").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleArmd.HlatRipristina(dcHlat.Position, dcHlat.Filter)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcHlat, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordDispVariante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Dim oPar As New CLE__CLDP
    Try
      If dsHlat.Tables("ARTPROX").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128566340162716163, "Non esistono dati da visualizzare nel dettaglio delle quantità per taglie."))
        Exit Sub
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BS--HLAT"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               oCleArmd.strHlatCodart & "|" & _
               oCleArmd.strHlatCodart & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(oCleArmd.dHlatFase) & "|" & _
               NTSCStr(oCleArmd.dHlatFase) & "|" & _
               "ARTPROTC" & "|" & _
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