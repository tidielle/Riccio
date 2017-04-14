Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEPEVE_F
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE + CLN__STD.bsModExtCRM
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
  Public oClePeve As CLEVEPEVE
  Public dsPeve As DataSet
  Public dcPeve As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents tabPeve As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnPeve1 As NTSInformatica.NTSPanel
  Public WithEvents lbTb_cauemft As NTSInformatica.NTSLabel
  Public WithEvents edTb_cauemft As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_cauemft As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cauemfs As NTSInformatica.NTSLabel
  Public WithEvents edTb_cauemfs As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_cauemfs As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caunoac As NTSInformatica.NTSLabel
  Public WithEvents edTb_caunoac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caunoac As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caunoaf As NTSInformatica.NTSLabel
  Public WithEvents edTb_caunoaf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caunoaf As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cauinc As NTSInformatica.NTSLabel
  Public WithEvents edTb_cauinc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_Cauinc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caucorr As NTSInformatica.NTSLabel
  Public WithEvents edTb_caucorr As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caucorr As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caustom As NTSInformatica.NTSLabel
  Public WithEvents edTb_caustom As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caustom As NTSInformatica.NTSLabel
  Public WithEvents lbXx_iva15 As NTSInformatica.NTSLabel
  Public WithEvents edTb_iva15 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_iva15 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_ivainc As NTSInformatica.NTSLabel
  Public WithEvents edTb_ivainc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_ivainc As NTSInformatica.NTSLabel
  Public WithEvents pnPeve2 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_concas As NTSInformatica.NTSLabel
  Public WithEvents edTb_concas As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_concas As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conabat As NTSInformatica.NTSLabel
  Public WithEvents edTb_conabat As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conabat As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conabpa As NTSInformatica.NTSLabel
  Public WithEvents edTb_conabpa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conabpa As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conomag As NTSInformatica.NTSLabel
  Public WithEvents edTb_conomag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conomag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conribo As NTSInformatica.NTSLabel
  Public WithEvents edTb_conribo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conribo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conriin As NTSInformatica.NTSLabel
  Public WithEvents edTb_conriin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conriin As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conriac As NTSInformatica.NTSLabel
  Public WithEvents edTb_conriac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conriac As NTSInformatica.NTSLabel
  Public WithEvents lbXx_concauz As NTSInformatica.NTSLabel
  Public WithEvents edTb_concauz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_concauz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conclpriv As NTSInformatica.NTSLabel
  Public WithEvents edTb_conclpriv As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conclpriv As NTSInformatica.NTSLabel
  Public WithEvents lbXx_contimba As NTSInformatica.NTSLabel
  Public WithEvents edTb_contimba As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_contimba As NTSInformatica.NTSLabel
  Public WithEvents pnPeve3 As NTSInformatica.NTSPanel
  Public WithEvents edTb_vtipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vtipobf As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vtipobf As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vcontro As NTSInformatica.NTSLabel
  Public WithEvents edTb_vcontro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vcontro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vmagazz As NTSInformatica.NTSLabel
  Public WithEvents edTb_vmagazz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vmagazz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vcaumag As NTSInformatica.NTSLabel
  Public WithEvents edTb_vcaumag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vcaumag As NTSInformatica.NTSLabel
  Public WithEvents edTb_vlistin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vlistin As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vcautra As NTSInformatica.NTSLabel
  Public WithEvents edTb_vcautra As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vcautra As NTSInformatica.NTSLabel
  Public WithEvents lbTb_vacurad As NTSInformatica.NTSLabel
  Public WithEvents cbTb_vacurad As NTSInformatica.NTSComboBox
  Public WithEvents fraFrame1 As NTSInformatica.NTSGroupBox
  Public WithEvents ckTb_vscorpo As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_vdatora As NTSInformatica.NTSCheckBox
  Public WithEvents pnPeve4 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_concrrfc As NTSInformatica.NTSLabel
  Public WithEvents edTb_concrrfc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_concrrfc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_concrrfp As NTSInformatica.NTSLabel
  Public WithEvents edTb_concrrfp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_concrrfp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conivrfp As NTSInformatica.NTSLabel
  Public WithEvents edTb_conivrfp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conivrfp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfincc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfincc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfincc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfparc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfparc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfparc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfnonc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfnonc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfnonc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfriec As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfriec As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfriec As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfincp As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfincp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfincp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfparp As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfparp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfparp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfnonp As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfnonp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfnonp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurfriep As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurfriep As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurfriep As NTSInformatica.NTSLabel
  Public WithEvents lbTb_caustib As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caustib As NTSInformatica.NTSLabel
  Public WithEvents edTb_caustib As NTSInformatica.NTSTextBoxNum
  Private components As System.ComponentModel.IContainer
  Public WithEvents edTb_datfinivainc3 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_ivainc3 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_ivainc3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_ivainc3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_datfinivainc2 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbXx_ivainc2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_ivainc2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_ivainc2 As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
#End Region

#Region "Inizializzazione controlli"
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
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEPEVE", "BEVEPEVE", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePeve = CType(objTmp, CLEVEPEVE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEPEVE", strRemoteServer, strRemotePort)
    AddHandler oClePeve.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePeve.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEPEVE_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tabPeve = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPeve1 = New NTSInformatica.NTSPanel
    Me.lbTb_ivainc3 = New NTSInformatica.NTSLabel
    Me.lbXx_ivainc3 = New NTSInformatica.NTSLabel
    Me.edTb_ivainc3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_datfinivainc2 = New NTSInformatica.NTSTextBoxData
    Me.lbXx_ivainc2 = New NTSInformatica.NTSLabel
    Me.edTb_ivainc2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_ivainc2 = New NTSInformatica.NTSLabel
    Me.edTb_datfinivainc3 = New NTSInformatica.NTSTextBoxData
    Me.lbTb_caustib = New NTSInformatica.NTSLabel
    Me.lbXx_caustib = New NTSInformatica.NTSLabel
    Me.edTb_caustib = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_ivainc = New NTSInformatica.NTSLabel
    Me.edTb_ivainc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_ivainc = New NTSInformatica.NTSLabel
    Me.lbXx_iva15 = New NTSInformatica.NTSLabel
    Me.edTb_iva15 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_iva15 = New NTSInformatica.NTSLabel
    Me.lbXx_caustom = New NTSInformatica.NTSLabel
    Me.edTb_caustom = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caustom = New NTSInformatica.NTSLabel
    Me.lbXx_caucorr = New NTSInformatica.NTSLabel
    Me.edTb_caucorr = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caucorr = New NTSInformatica.NTSLabel
    Me.lbXx_cauinc = New NTSInformatica.NTSLabel
    Me.edTb_cauinc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_Cauinc = New NTSInformatica.NTSLabel
    Me.lbXx_caunoaf = New NTSInformatica.NTSLabel
    Me.edTb_caunoaf = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caunoaf = New NTSInformatica.NTSLabel
    Me.lbXx_caunoac = New NTSInformatica.NTSLabel
    Me.edTb_caunoac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caunoac = New NTSInformatica.NTSLabel
    Me.lbXx_cauemfs = New NTSInformatica.NTSLabel
    Me.edTb_cauemfs = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cauemfs = New NTSInformatica.NTSLabel
    Me.lbXx_cauemft = New NTSInformatica.NTSLabel
    Me.edTb_cauemft = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cauemft = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPeve2 = New NTSInformatica.NTSPanel
    Me.lbXx_contimba = New NTSInformatica.NTSLabel
    Me.edTb_contimba = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_contimba = New NTSInformatica.NTSLabel
    Me.lbXx_conclpriv = New NTSInformatica.NTSLabel
    Me.edTb_conclpriv = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conclpriv = New NTSInformatica.NTSLabel
    Me.lbXx_concauz = New NTSInformatica.NTSLabel
    Me.edTb_concauz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_concauz = New NTSInformatica.NTSLabel
    Me.lbXx_conriac = New NTSInformatica.NTSLabel
    Me.edTb_conriac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conriac = New NTSInformatica.NTSLabel
    Me.lbXx_conriin = New NTSInformatica.NTSLabel
    Me.edTb_conriin = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conriin = New NTSInformatica.NTSLabel
    Me.lbXx_conribo = New NTSInformatica.NTSLabel
    Me.edTb_conribo = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conribo = New NTSInformatica.NTSLabel
    Me.lbXx_conomag = New NTSInformatica.NTSLabel
    Me.edTb_conomag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conomag = New NTSInformatica.NTSLabel
    Me.lbXx_conabpa = New NTSInformatica.NTSLabel
    Me.edTb_conabpa = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conabpa = New NTSInformatica.NTSLabel
    Me.lbXx_conabat = New NTSInformatica.NTSLabel
    Me.edTb_conabat = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conabat = New NTSInformatica.NTSLabel
    Me.lbXx_concas = New NTSInformatica.NTSLabel
    Me.edTb_concas = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_concas = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPeve3 = New NTSInformatica.NTSPanel
    Me.fraFrame1 = New NTSInformatica.NTSGroupBox
    Me.ckTb_vdatora = New NTSInformatica.NTSCheckBox
    Me.ckTb_vscorpo = New NTSInformatica.NTSCheckBox
    Me.lbTb_vacurad = New NTSInformatica.NTSLabel
    Me.cbTb_vacurad = New NTSInformatica.NTSComboBox
    Me.lbXx_vcautra = New NTSInformatica.NTSLabel
    Me.edTb_vcautra = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vcautra = New NTSInformatica.NTSLabel
    Me.edTb_vlistin = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vlistin = New NTSInformatica.NTSLabel
    Me.lbXx_vcaumag = New NTSInformatica.NTSLabel
    Me.edTb_vcaumag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vcaumag = New NTSInformatica.NTSLabel
    Me.lbXx_vmagazz = New NTSInformatica.NTSLabel
    Me.edTb_vmagazz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vmagazz = New NTSInformatica.NTSLabel
    Me.lbXx_vcontro = New NTSInformatica.NTSLabel
    Me.edTb_vcontro = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vcontro = New NTSInformatica.NTSLabel
    Me.lbXx_vtipobf = New NTSInformatica.NTSLabel
    Me.edTb_vtipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vtipobf = New NTSInformatica.NTSLabel
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPeve4 = New NTSInformatica.NTSPanel
    Me.lbXx_caurfriep = New NTSInformatica.NTSLabel
    Me.edTb_caurfriep = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfriep = New NTSInformatica.NTSLabel
    Me.lbXx_caurfnonp = New NTSInformatica.NTSLabel
    Me.edTb_caurfnonp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfnonp = New NTSInformatica.NTSLabel
    Me.lbXx_caurfparp = New NTSInformatica.NTSLabel
    Me.edTb_caurfparp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfparp = New NTSInformatica.NTSLabel
    Me.lbXx_caurfincp = New NTSInformatica.NTSLabel
    Me.edTb_caurfincp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfincp = New NTSInformatica.NTSLabel
    Me.lbXx_caurfriec = New NTSInformatica.NTSLabel
    Me.edTb_caurfriec = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfriec = New NTSInformatica.NTSLabel
    Me.lbXx_caurfnonc = New NTSInformatica.NTSLabel
    Me.edTb_caurfnonc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfnonc = New NTSInformatica.NTSLabel
    Me.lbXx_caurfparc = New NTSInformatica.NTSLabel
    Me.edTb_caurfparc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfparc = New NTSInformatica.NTSLabel
    Me.lbXx_caurfincc = New NTSInformatica.NTSLabel
    Me.edTb_caurfincc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurfincc = New NTSInformatica.NTSLabel
    Me.lbXx_conivrfp = New NTSInformatica.NTSLabel
    Me.edTb_conivrfp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_conivrfp = New NTSInformatica.NTSLabel
    Me.lbXx_concrrfp = New NTSInformatica.NTSLabel
    Me.edTb_concrrfp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_concrrfp = New NTSInformatica.NTSLabel
    Me.lbXx_concrrfc = New NTSInformatica.NTSLabel
    Me.edTb_concrrfc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_concrrfc = New NTSInformatica.NTSLabel
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbXx_tipobfrkq = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkq = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkq = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkr = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkr = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkr = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrks = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrks = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrks = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkn = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkn = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkn = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkf = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrki = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrki = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrki = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkf = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkf = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrke = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrke = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrke = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkc = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkc = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkb = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkb = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkb = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrka = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrka = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrka = New NTSInformatica.NTSLabel
    Me.edXx_vlistin = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tabPeve, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabPeve.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPeve1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeve1.SuspendLayout()
    CType(Me.edTb_ivainc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datfinivainc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ivainc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datfinivainc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caustib.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ivainc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_iva15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caustom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caucorr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cauinc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caunoaf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caunoac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cauemfs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cauemft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPeve2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeve2.SuspendLayout()
    CType(Me.edTb_contimba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conclpriv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_concauz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conriac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conriin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conribo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conomag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conabpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conabat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_concas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPeve3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeve3.SuspendLayout()
    CType(Me.fraFrame1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fraFrame1.SuspendLayout()
    CType(Me.ckTb_vdatora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_vscorpo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vacurad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vcautra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vlistin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vcaumag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vmagazz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vcontro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vtipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPeve4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeve4.SuspendLayout()
    CType(Me.edTb_caurfriep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfnonp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfparp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfincp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfriec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfnonc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfparc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurfincc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conivrfp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_concrrfp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_concrrfc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edTb_tipobfrkq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrki.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrke.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrka.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 5
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbSalva.Id = 0
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 2
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tabPeve
    '
    Me.tabPeve.Location = New System.Drawing.Point(7, 34)
    Me.tabPeve.Name = "tabPeve"
    Me.tabPeve.SelectedTabPage = Me.NtsTabPage1
    Me.tabPeve.Size = New System.Drawing.Size(724, 309)
    Me.tabPeve.TabIndex = 1
    Me.tabPeve.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5})
    Me.tabPeve.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.pnPeve1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(715, 279)
    Me.NtsTabPage1.Text = "Pagina &1"
    '
    'pnPeve1
    '
    Me.pnPeve1.AllowDrop = True
    Me.pnPeve1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeve1.Appearance.Options.UseBackColor = True
    Me.pnPeve1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeve1.Controls.Add(Me.lbTb_ivainc3)
    Me.pnPeve1.Controls.Add(Me.lbXx_ivainc3)
    Me.pnPeve1.Controls.Add(Me.edTb_ivainc3)
    Me.pnPeve1.Controls.Add(Me.edTb_datfinivainc2)
    Me.pnPeve1.Controls.Add(Me.lbXx_ivainc2)
    Me.pnPeve1.Controls.Add(Me.edTb_ivainc2)
    Me.pnPeve1.Controls.Add(Me.lbTb_ivainc2)
    Me.pnPeve1.Controls.Add(Me.edTb_datfinivainc3)
    Me.pnPeve1.Controls.Add(Me.lbTb_caustib)
    Me.pnPeve1.Controls.Add(Me.lbXx_caustib)
    Me.pnPeve1.Controls.Add(Me.edTb_caustib)
    Me.pnPeve1.Controls.Add(Me.lbXx_ivainc)
    Me.pnPeve1.Controls.Add(Me.edTb_ivainc)
    Me.pnPeve1.Controls.Add(Me.lbTb_ivainc)
    Me.pnPeve1.Controls.Add(Me.lbXx_iva15)
    Me.pnPeve1.Controls.Add(Me.edTb_iva15)
    Me.pnPeve1.Controls.Add(Me.lbTb_iva15)
    Me.pnPeve1.Controls.Add(Me.lbXx_caustom)
    Me.pnPeve1.Controls.Add(Me.edTb_caustom)
    Me.pnPeve1.Controls.Add(Me.lbTb_caustom)
    Me.pnPeve1.Controls.Add(Me.lbXx_caucorr)
    Me.pnPeve1.Controls.Add(Me.edTb_caucorr)
    Me.pnPeve1.Controls.Add(Me.lbTb_caucorr)
    Me.pnPeve1.Controls.Add(Me.lbXx_cauinc)
    Me.pnPeve1.Controls.Add(Me.edTb_cauinc)
    Me.pnPeve1.Controls.Add(Me.lbTb_Cauinc)
    Me.pnPeve1.Controls.Add(Me.lbXx_caunoaf)
    Me.pnPeve1.Controls.Add(Me.edTb_caunoaf)
    Me.pnPeve1.Controls.Add(Me.lbTb_caunoaf)
    Me.pnPeve1.Controls.Add(Me.lbXx_caunoac)
    Me.pnPeve1.Controls.Add(Me.edTb_caunoac)
    Me.pnPeve1.Controls.Add(Me.lbTb_caunoac)
    Me.pnPeve1.Controls.Add(Me.lbXx_cauemfs)
    Me.pnPeve1.Controls.Add(Me.edTb_cauemfs)
    Me.pnPeve1.Controls.Add(Me.lbTb_cauemfs)
    Me.pnPeve1.Controls.Add(Me.lbXx_cauemft)
    Me.pnPeve1.Controls.Add(Me.edTb_cauemft)
    Me.pnPeve1.Controls.Add(Me.lbTb_cauemft)
    Me.pnPeve1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeve1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPeve1.Location = New System.Drawing.Point(0, 0)
    Me.pnPeve1.Name = "pnPeve1"
    Me.pnPeve1.NTSActiveTrasparency = True
    Me.pnPeve1.Size = New System.Drawing.Size(715, 279)
    Me.pnPeve1.TabIndex = 0
    Me.pnPeve1.Text = "NtsPanel1"
    '
    'lbTb_ivainc3
    '
    Me.lbTb_ivainc3.AutoSize = True
    Me.lbTb_ivainc3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ivainc3.Location = New System.Drawing.Point(13, 226)
    Me.lbTb_ivainc3.Name = "lbTb_ivainc3"
    Me.lbTb_ivainc3.NTSDbField = ""
    Me.lbTb_ivainc3.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_ivainc3.TabIndex = 125
    Me.lbTb_ivainc3.Text = "Codice Iva spese piede 3 / prima del"
    Me.lbTb_ivainc3.Tooltip = ""
    Me.lbTb_ivainc3.UseMnemonic = False
    '
    'lbXx_ivainc3
    '
    Me.lbXx_ivainc3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ivainc3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_ivainc3.Location = New System.Drawing.Point(311, 223)
    Me.lbXx_ivainc3.Name = "lbXx_ivainc3"
    Me.lbXx_ivainc3.NTSDbField = ""
    Me.lbXx_ivainc3.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_ivainc3.TabIndex = 124
    Me.lbXx_ivainc3.Tooltip = ""
    Me.lbXx_ivainc3.UseMnemonic = False
    '
    'edTb_ivainc3
    '
    Me.edTb_ivainc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ivainc3.Location = New System.Drawing.Point(236, 223)
    Me.edTb_ivainc3.Name = "edTb_ivainc3"
    Me.edTb_ivainc3.NTSDbField = ""
    Me.edTb_ivainc3.NTSFormat = "0"
    Me.edTb_ivainc3.NTSForzaVisZoom = False
    Me.edTb_ivainc3.NTSOldValue = ""
    Me.edTb_ivainc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ivainc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ivainc3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ivainc3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ivainc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ivainc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ivainc3.Properties.AutoHeight = False
    Me.edTb_ivainc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ivainc3.Properties.MaxLength = 65536
    Me.edTb_ivainc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ivainc3.Size = New System.Drawing.Size(69, 20)
    Me.edTb_ivainc3.TabIndex = 123
    Me.edTb_ivainc3.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_ivainc3.TextInt = 0
    '
    'edTb_datfinivainc2
    '
    Me.edTb_datfinivainc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datfinivainc2.EditValue = "17/09/2011"
    Me.edTb_datfinivainc2.Location = New System.Drawing.Point(607, 201)
    Me.edTb_datfinivainc2.Name = "edTb_datfinivainc2"
    Me.edTb_datfinivainc2.NTSDbField = ""
    Me.edTb_datfinivainc2.NTSForzaVisZoom = False
    Me.edTb_datfinivainc2.NTSOldValue = ""
    Me.edTb_datfinivainc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_datfinivainc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_datfinivainc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datfinivainc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datfinivainc2.Properties.AutoHeight = False
    Me.edTb_datfinivainc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datfinivainc2.Properties.MaxLength = 65536
    Me.edTb_datfinivainc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datfinivainc2.Size = New System.Drawing.Size(90, 20)
    Me.edTb_datfinivainc2.TabIndex = 122
    Me.edTb_datfinivainc2.TextData = New Date(2011, 9, 17, 0, 0, 0, 0)
    '
    'lbXx_ivainc2
    '
    Me.lbXx_ivainc2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ivainc2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_ivainc2.Location = New System.Drawing.Point(311, 201)
    Me.lbXx_ivainc2.Name = "lbXx_ivainc2"
    Me.lbXx_ivainc2.NTSDbField = ""
    Me.lbXx_ivainc2.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_ivainc2.TabIndex = 121
    Me.lbXx_ivainc2.Tooltip = ""
    Me.lbXx_ivainc2.UseMnemonic = False
    '
    'edTb_ivainc2
    '
    Me.edTb_ivainc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ivainc2.Location = New System.Drawing.Point(236, 201)
    Me.edTb_ivainc2.Name = "edTb_ivainc2"
    Me.edTb_ivainc2.NTSDbField = ""
    Me.edTb_ivainc2.NTSFormat = "0"
    Me.edTb_ivainc2.NTSForzaVisZoom = False
    Me.edTb_ivainc2.NTSOldValue = ""
    Me.edTb_ivainc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ivainc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ivainc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ivainc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ivainc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ivainc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ivainc2.Properties.AutoHeight = False
    Me.edTb_ivainc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ivainc2.Properties.MaxLength = 65536
    Me.edTb_ivainc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ivainc2.Size = New System.Drawing.Size(69, 20)
    Me.edTb_ivainc2.TabIndex = 119
    Me.edTb_ivainc2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_ivainc2.TextInt = 0
    '
    'lbTb_ivainc2
    '
    Me.lbTb_ivainc2.AutoSize = True
    Me.lbTb_ivainc2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ivainc2.Location = New System.Drawing.Point(13, 204)
    Me.lbTb_ivainc2.Name = "lbTb_ivainc2"
    Me.lbTb_ivainc2.NTSDbField = ""
    Me.lbTb_ivainc2.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_ivainc2.TabIndex = 120
    Me.lbTb_ivainc2.Text = "Codice Iva spese piede 2 / prima del"
    Me.lbTb_ivainc2.Tooltip = ""
    Me.lbTb_ivainc2.UseMnemonic = False
    '
    'edTb_datfinivainc3
    '
    Me.edTb_datfinivainc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datfinivainc3.EditValue = "01/01/1900"
    Me.edTb_datfinivainc3.Location = New System.Drawing.Point(607, 223)
    Me.edTb_datfinivainc3.Name = "edTb_datfinivainc3"
    Me.edTb_datfinivainc3.NTSDbField = ""
    Me.edTb_datfinivainc3.NTSForzaVisZoom = False
    Me.edTb_datfinivainc3.NTSOldValue = ""
    Me.edTb_datfinivainc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_datfinivainc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_datfinivainc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datfinivainc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datfinivainc3.Properties.AutoHeight = False
    Me.edTb_datfinivainc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datfinivainc3.Properties.MaxLength = 65536
    Me.edTb_datfinivainc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datfinivainc3.Size = New System.Drawing.Size(90, 20)
    Me.edTb_datfinivainc3.TabIndex = 118
    Me.edTb_datfinivainc3.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'lbTb_caustib
    '
    Me.lbTb_caustib.AutoSize = True
    Me.lbTb_caustib.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caustib.Location = New System.Drawing.Point(13, 248)
    Me.lbTb_caustib.Name = "lbTb_caustib"
    Me.lbTb_caustib.NTSDbField = ""
    Me.lbTb_caustib.Size = New System.Drawing.Size(185, 13)
    Me.lbTb_caustib.TabIndex = 117
    Me.lbTb_caustib.Text = "Causale storno incassi su DDT emessi"
    Me.lbTb_caustib.Tooltip = ""
    Me.lbTb_caustib.UseMnemonic = False
    '
    'lbXx_caustib
    '
    Me.lbXx_caustib.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caustib.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caustib.Location = New System.Drawing.Point(311, 245)
    Me.lbXx_caustib.Name = "lbXx_caustib"
    Me.lbXx_caustib.NTSDbField = ""
    Me.lbXx_caustib.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caustib.TabIndex = 116
    Me.lbXx_caustib.Tooltip = ""
    Me.lbXx_caustib.UseMnemonic = False
    '
    'edTb_caustib
    '
    Me.edTb_caustib.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caustib.Location = New System.Drawing.Point(236, 245)
    Me.edTb_caustib.Name = "edTb_caustib"
    Me.edTb_caustib.NTSDbField = ""
    Me.edTb_caustib.NTSFormat = "0"
    Me.edTb_caustib.NTSForzaVisZoom = False
    Me.edTb_caustib.NTSOldValue = ""
    Me.edTb_caustib.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caustib.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caustib.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caustib.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caustib.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caustib.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caustib.Properties.AutoHeight = False
    Me.edTb_caustib.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caustib.Properties.MaxLength = 65536
    Me.edTb_caustib.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caustib.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caustib.TabIndex = 115
    Me.edTb_caustib.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caustib.TextInt = 0
    '
    'lbXx_ivainc
    '
    Me.lbXx_ivainc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ivainc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_ivainc.Location = New System.Drawing.Point(311, 179)
    Me.lbXx_ivainc.Name = "lbXx_ivainc"
    Me.lbXx_ivainc.NTSDbField = ""
    Me.lbXx_ivainc.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_ivainc.TabIndex = 114
    Me.lbXx_ivainc.Tooltip = ""
    Me.lbXx_ivainc.UseMnemonic = False
    '
    'edTb_ivainc
    '
    Me.edTb_ivainc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ivainc.Location = New System.Drawing.Point(236, 179)
    Me.edTb_ivainc.Name = "edTb_ivainc"
    Me.edTb_ivainc.NTSDbField = ""
    Me.edTb_ivainc.NTSFormat = "0"
    Me.edTb_ivainc.NTSForzaVisZoom = False
    Me.edTb_ivainc.NTSOldValue = ""
    Me.edTb_ivainc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ivainc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ivainc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ivainc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ivainc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ivainc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ivainc.Properties.AutoHeight = False
    Me.edTb_ivainc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ivainc.Properties.MaxLength = 65536
    Me.edTb_ivainc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ivainc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_ivainc.TabIndex = 10
    Me.edTb_ivainc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_ivainc.TextInt = 0
    '
    'lbTb_ivainc
    '
    Me.lbTb_ivainc.AutoSize = True
    Me.lbTb_ivainc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ivainc.Location = New System.Drawing.Point(13, 182)
    Me.lbTb_ivainc.Name = "lbTb_ivainc"
    Me.lbTb_ivainc.NTSDbField = ""
    Me.lbTb_ivainc.Size = New System.Drawing.Size(173, 13)
    Me.lbTb_ivainc.TabIndex = 112
    Me.lbTb_ivainc.Text = "Codice Iva spese piede predefinito"
    Me.lbTb_ivainc.Tooltip = ""
    Me.lbTb_ivainc.UseMnemonic = False
    '
    'lbXx_iva15
    '
    Me.lbXx_iva15.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_iva15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_iva15.Location = New System.Drawing.Point(311, 157)
    Me.lbXx_iva15.Name = "lbXx_iva15"
    Me.lbXx_iva15.NTSDbField = ""
    Me.lbXx_iva15.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_iva15.TabIndex = 111
    Me.lbXx_iva15.Tooltip = ""
    Me.lbXx_iva15.UseMnemonic = False
    '
    'edTb_iva15
    '
    Me.edTb_iva15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_iva15.Location = New System.Drawing.Point(236, 157)
    Me.edTb_iva15.Name = "edTb_iva15"
    Me.edTb_iva15.NTSDbField = ""
    Me.edTb_iva15.NTSFormat = "0"
    Me.edTb_iva15.NTSForzaVisZoom = False
    Me.edTb_iva15.NTSOldValue = ""
    Me.edTb_iva15.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_iva15.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_iva15.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_iva15.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_iva15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_iva15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_iva15.Properties.AutoHeight = False
    Me.edTb_iva15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_iva15.Properties.MaxLength = 65536
    Me.edTb_iva15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_iva15.Size = New System.Drawing.Size(69, 20)
    Me.edTb_iva15.TabIndex = 9
    Me.edTb_iva15.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_iva15.TextInt = 0
    '
    'lbTb_iva15
    '
    Me.lbTb_iva15.AutoSize = True
    Me.lbTb_iva15.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_iva15.Location = New System.Drawing.Point(13, 160)
    Me.lbTb_iva15.Name = "lbTb_iva15"
    Me.lbTb_iva15.NTSDbField = ""
    Me.lbTb_iva15.Size = New System.Drawing.Size(149, 13)
    Me.lbTb_iva15.TabIndex = 109
    Me.lbTb_iva15.Text = "Codice Iva escluso articolo 15"
    Me.lbTb_iva15.Tooltip = ""
    Me.lbTb_iva15.UseMnemonic = False
    '
    'lbXx_caustom
    '
    Me.lbXx_caustom.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caustom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caustom.Location = New System.Drawing.Point(311, 135)
    Me.lbXx_caustom.Name = "lbXx_caustom"
    Me.lbXx_caustom.NTSDbField = ""
    Me.lbXx_caustom.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caustom.TabIndex = 108
    Me.lbXx_caustom.Tooltip = ""
    Me.lbXx_caustom.UseMnemonic = False
    '
    'edTb_caustom
    '
    Me.edTb_caustom.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caustom.Location = New System.Drawing.Point(236, 135)
    Me.edTb_caustom.Name = "edTb_caustom"
    Me.edTb_caustom.NTSDbField = ""
    Me.edTb_caustom.NTSFormat = "0"
    Me.edTb_caustom.NTSForzaVisZoom = False
    Me.edTb_caustom.NTSOldValue = ""
    Me.edTb_caustom.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caustom.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caustom.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caustom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caustom.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caustom.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caustom.Properties.AutoHeight = False
    Me.edTb_caustom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caustom.Properties.MaxLength = 65536
    Me.edTb_caustom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caustom.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caustom.TabIndex = 8
    Me.edTb_caustom.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caustom.TextInt = 0
    '
    'lbTb_caustom
    '
    Me.lbTb_caustom.AutoSize = True
    Me.lbTb_caustom.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caustom.Location = New System.Drawing.Point(13, 138)
    Me.lbTb_caustom.Name = "lbTb_caustom"
    Me.lbTb_caustom.NTSDbField = ""
    Me.lbTb_caustom.Size = New System.Drawing.Size(118, 13)
    Me.lbTb_caustom.TabIndex = 106
    Me.lbTb_caustom.Text = "Causale storno Omaggi"
    Me.lbTb_caustom.Tooltip = ""
    Me.lbTb_caustom.UseMnemonic = False
    '
    'lbXx_caucorr
    '
    Me.lbXx_caucorr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caucorr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caucorr.Location = New System.Drawing.Point(311, 113)
    Me.lbXx_caucorr.Name = "lbXx_caucorr"
    Me.lbXx_caucorr.NTSDbField = ""
    Me.lbXx_caucorr.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caucorr.TabIndex = 105
    Me.lbXx_caucorr.Tooltip = ""
    Me.lbXx_caucorr.UseMnemonic = False
    '
    'edTb_caucorr
    '
    Me.edTb_caucorr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caucorr.Location = New System.Drawing.Point(236, 113)
    Me.edTb_caucorr.Name = "edTb_caucorr"
    Me.edTb_caucorr.NTSDbField = ""
    Me.edTb_caucorr.NTSFormat = "0"
    Me.edTb_caucorr.NTSForzaVisZoom = False
    Me.edTb_caucorr.NTSOldValue = ""
    Me.edTb_caucorr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caucorr.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caucorr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caucorr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caucorr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caucorr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caucorr.Properties.AutoHeight = False
    Me.edTb_caucorr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caucorr.Properties.MaxLength = 65536
    Me.edTb_caucorr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caucorr.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caucorr.TabIndex = 7
    Me.edTb_caucorr.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caucorr.TextInt = 0
    '
    'lbTb_caucorr
    '
    Me.lbTb_caucorr.AutoSize = True
    Me.lbTb_caucorr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caucorr.Location = New System.Drawing.Point(13, 116)
    Me.lbTb_caucorr.Name = "lbTb_caucorr"
    Me.lbTb_caucorr.NTSDbField = ""
    Me.lbTb_caucorr.Size = New System.Drawing.Size(155, 13)
    Me.lbTb_caucorr.TabIndex = 103
    Me.lbTb_caucorr.Text = "Causale emissione Corrispettivi"
    Me.lbTb_caucorr.Tooltip = ""
    Me.lbTb_caucorr.UseMnemonic = False
    '
    'lbXx_cauinc
    '
    Me.lbXx_cauinc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cauinc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cauinc.Location = New System.Drawing.Point(311, 91)
    Me.lbXx_cauinc.Name = "lbXx_cauinc"
    Me.lbXx_cauinc.NTSDbField = ""
    Me.lbXx_cauinc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_cauinc.TabIndex = 102
    Me.lbXx_cauinc.Tooltip = ""
    Me.lbXx_cauinc.UseMnemonic = False
    '
    'edTb_cauinc
    '
    Me.edTb_cauinc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cauinc.Location = New System.Drawing.Point(236, 91)
    Me.edTb_cauinc.Name = "edTb_cauinc"
    Me.edTb_cauinc.NTSDbField = ""
    Me.edTb_cauinc.NTSFormat = "0"
    Me.edTb_cauinc.NTSForzaVisZoom = False
    Me.edTb_cauinc.NTSOldValue = ""
    Me.edTb_cauinc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cauinc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cauinc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cauinc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cauinc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cauinc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cauinc.Properties.AutoHeight = False
    Me.edTb_cauinc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cauinc.Properties.MaxLength = 65536
    Me.edTb_cauinc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cauinc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_cauinc.TabIndex = 6
    Me.edTb_cauinc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_cauinc.TextInt = 0
    '
    'lbTb_Cauinc
    '
    Me.lbTb_Cauinc.AutoSize = True
    Me.lbTb_Cauinc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_Cauinc.Location = New System.Drawing.Point(13, 94)
    Me.lbTb_Cauinc.Name = "lbTb_Cauinc"
    Me.lbTb_Cauinc.NTSDbField = ""
    Me.lbTb_Cauinc.Size = New System.Drawing.Size(85, 13)
    Me.lbTb_Cauinc.TabIndex = 100
    Me.lbTb_Cauinc.Text = "Causale Incasso"
    Me.lbTb_Cauinc.Tooltip = ""
    Me.lbTb_Cauinc.UseMnemonic = False
    '
    'lbXx_caunoaf
    '
    Me.lbXx_caunoaf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caunoaf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caunoaf.Location = New System.Drawing.Point(311, 69)
    Me.lbXx_caunoaf.Name = "lbXx_caunoaf"
    Me.lbXx_caunoaf.NTSDbField = ""
    Me.lbXx_caunoaf.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caunoaf.TabIndex = 99
    Me.lbXx_caunoaf.Tooltip = ""
    Me.lbXx_caunoaf.UseMnemonic = False
    '
    'edTb_caunoaf
    '
    Me.edTb_caunoaf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caunoaf.Location = New System.Drawing.Point(236, 69)
    Me.edTb_caunoaf.Name = "edTb_caunoaf"
    Me.edTb_caunoaf.NTSDbField = ""
    Me.edTb_caunoaf.NTSFormat = "0"
    Me.edTb_caunoaf.NTSForzaVisZoom = False
    Me.edTb_caunoaf.NTSOldValue = ""
    Me.edTb_caunoaf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caunoaf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caunoaf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caunoaf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caunoaf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caunoaf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caunoaf.Properties.AutoHeight = False
    Me.edTb_caunoaf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caunoaf.Properties.MaxLength = 65536
    Me.edTb_caunoaf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caunoaf.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caunoaf.TabIndex = 5
    Me.edTb_caunoaf.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caunoaf.TextInt = 0
    '
    'lbTb_caunoaf
    '
    Me.lbTb_caunoaf.AutoSize = True
    Me.lbTb_caunoaf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caunoaf.Location = New System.Drawing.Point(13, 72)
    Me.lbTb_caunoaf.Name = "lbTb_caunoaf"
    Me.lbTb_caunoaf.NTSDbField = ""
    Me.lbTb_caunoaf.Size = New System.Drawing.Size(192, 13)
    Me.lbTb_caunoaf.TabIndex = 97
    Me.lbTb_caunoaf.Text = "Causale Note di Accredito sospensione"
    Me.lbTb_caunoaf.Tooltip = ""
    Me.lbTb_caunoaf.UseMnemonic = False
    '
    'lbXx_caunoac
    '
    Me.lbXx_caunoac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caunoac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caunoac.Location = New System.Drawing.Point(311, 47)
    Me.lbXx_caunoac.Name = "lbXx_caunoac"
    Me.lbXx_caunoac.NTSDbField = ""
    Me.lbXx_caunoac.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caunoac.TabIndex = 96
    Me.lbXx_caunoac.Tooltip = ""
    Me.lbXx_caunoac.UseMnemonic = False
    '
    'edTb_caunoac
    '
    Me.edTb_caunoac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caunoac.Location = New System.Drawing.Point(236, 47)
    Me.edTb_caunoac.Name = "edTb_caunoac"
    Me.edTb_caunoac.NTSDbField = ""
    Me.edTb_caunoac.NTSFormat = "0"
    Me.edTb_caunoac.NTSForzaVisZoom = False
    Me.edTb_caunoac.NTSOldValue = ""
    Me.edTb_caunoac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caunoac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caunoac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caunoac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caunoac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caunoac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caunoac.Properties.AutoHeight = False
    Me.edTb_caunoac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caunoac.Properties.MaxLength = 65536
    Me.edTb_caunoac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caunoac.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caunoac.TabIndex = 4
    Me.edTb_caunoac.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caunoac.TextInt = 0
    '
    'lbTb_caunoac
    '
    Me.lbTb_caunoac.AutoSize = True
    Me.lbTb_caunoac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caunoac.Location = New System.Drawing.Point(13, 50)
    Me.lbTb_caunoac.Name = "lbTb_caunoac"
    Me.lbTb_caunoac.NTSDbField = ""
    Me.lbTb_caunoac.Size = New System.Drawing.Size(130, 13)
    Me.lbTb_caunoac.TabIndex = 94
    Me.lbTb_caunoac.Text = "Causale Note di Accredito"
    Me.lbTb_caunoac.Tooltip = ""
    Me.lbTb_caunoac.UseMnemonic = False
    '
    'lbXx_cauemfs
    '
    Me.lbXx_cauemfs.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cauemfs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cauemfs.Location = New System.Drawing.Point(311, 25)
    Me.lbXx_cauemfs.Name = "lbXx_cauemfs"
    Me.lbXx_cauemfs.NTSDbField = ""
    Me.lbXx_cauemfs.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_cauemfs.TabIndex = 93
    Me.lbXx_cauemfs.Tooltip = ""
    Me.lbXx_cauemfs.UseMnemonic = False
    '
    'edTb_cauemfs
    '
    Me.edTb_cauemfs.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cauemfs.Location = New System.Drawing.Point(236, 25)
    Me.edTb_cauemfs.Name = "edTb_cauemfs"
    Me.edTb_cauemfs.NTSDbField = ""
    Me.edTb_cauemfs.NTSFormat = "0"
    Me.edTb_cauemfs.NTSForzaVisZoom = False
    Me.edTb_cauemfs.NTSOldValue = ""
    Me.edTb_cauemfs.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cauemfs.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cauemfs.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cauemfs.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cauemfs.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cauemfs.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cauemfs.Properties.AutoHeight = False
    Me.edTb_cauemfs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cauemfs.Properties.MaxLength = 65536
    Me.edTb_cauemfs.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cauemfs.Size = New System.Drawing.Size(69, 20)
    Me.edTb_cauemfs.TabIndex = 3
    Me.edTb_cauemfs.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_cauemfs.TextInt = 0
    '
    'lbTb_cauemfs
    '
    Me.lbTb_cauemfs.AutoSize = True
    Me.lbTb_cauemfs.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cauemfs.Location = New System.Drawing.Point(13, 28)
    Me.lbTb_cauemfs.Name = "lbTb_cauemfs"
    Me.lbTb_cauemfs.NTSDbField = ""
    Me.lbTb_cauemfs.Size = New System.Drawing.Size(195, 13)
    Me.lbTb_cauemfs.TabIndex = 91
    Me.lbTb_cauemfs.Text = "Causale emissione Fatture sospensione"
    Me.lbTb_cauemfs.Tooltip = ""
    Me.lbTb_cauemfs.UseMnemonic = False
    '
    'lbXx_cauemft
    '
    Me.lbXx_cauemft.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cauemft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cauemft.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_cauemft.Name = "lbXx_cauemft"
    Me.lbXx_cauemft.NTSDbField = ""
    Me.lbXx_cauemft.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_cauemft.TabIndex = 90
    Me.lbXx_cauemft.Tooltip = ""
    Me.lbXx_cauemft.UseMnemonic = False
    '
    'edTb_cauemft
    '
    Me.edTb_cauemft.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cauemft.Location = New System.Drawing.Point(236, 3)
    Me.edTb_cauemft.Name = "edTb_cauemft"
    Me.edTb_cauemft.NTSDbField = ""
    Me.edTb_cauemft.NTSFormat = "0"
    Me.edTb_cauemft.NTSForzaVisZoom = False
    Me.edTb_cauemft.NTSOldValue = ""
    Me.edTb_cauemft.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cauemft.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cauemft.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cauemft.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cauemft.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cauemft.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cauemft.Properties.AutoHeight = False
    Me.edTb_cauemft.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cauemft.Properties.MaxLength = 65536
    Me.edTb_cauemft.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cauemft.Size = New System.Drawing.Size(69, 20)
    Me.edTb_cauemft.TabIndex = 2
    Me.edTb_cauemft.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_cauemft.TextInt = 0
    '
    'lbTb_cauemft
    '
    Me.lbTb_cauemft.AutoSize = True
    Me.lbTb_cauemft.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cauemft.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_cauemft.Name = "lbTb_cauemft"
    Me.lbTb_cauemft.NTSDbField = ""
    Me.lbTb_cauemft.Size = New System.Drawing.Size(133, 13)
    Me.lbTb_cauemft.TabIndex = 81
    Me.lbTb_cauemft.Text = "Causale emissione Fatture"
    Me.lbTb_cauemft.Tooltip = ""
    Me.lbTb_cauemft.UseMnemonic = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPeve2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(715, 279)
    Me.NtsTabPage2.Text = "Pagina &2"
    '
    'pnPeve2
    '
    Me.pnPeve2.AllowDrop = True
    Me.pnPeve2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeve2.Appearance.Options.UseBackColor = True
    Me.pnPeve2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeve2.Controls.Add(Me.lbXx_contimba)
    Me.pnPeve2.Controls.Add(Me.edTb_contimba)
    Me.pnPeve2.Controls.Add(Me.lbTb_contimba)
    Me.pnPeve2.Controls.Add(Me.lbXx_conclpriv)
    Me.pnPeve2.Controls.Add(Me.edTb_conclpriv)
    Me.pnPeve2.Controls.Add(Me.lbTb_conclpriv)
    Me.pnPeve2.Controls.Add(Me.lbXx_concauz)
    Me.pnPeve2.Controls.Add(Me.edTb_concauz)
    Me.pnPeve2.Controls.Add(Me.lbTb_concauz)
    Me.pnPeve2.Controls.Add(Me.lbXx_conriac)
    Me.pnPeve2.Controls.Add(Me.edTb_conriac)
    Me.pnPeve2.Controls.Add(Me.lbTb_conriac)
    Me.pnPeve2.Controls.Add(Me.lbXx_conriin)
    Me.pnPeve2.Controls.Add(Me.edTb_conriin)
    Me.pnPeve2.Controls.Add(Me.lbTb_conriin)
    Me.pnPeve2.Controls.Add(Me.lbXx_conribo)
    Me.pnPeve2.Controls.Add(Me.edTb_conribo)
    Me.pnPeve2.Controls.Add(Me.lbTb_conribo)
    Me.pnPeve2.Controls.Add(Me.lbXx_conomag)
    Me.pnPeve2.Controls.Add(Me.edTb_conomag)
    Me.pnPeve2.Controls.Add(Me.lbTb_conomag)
    Me.pnPeve2.Controls.Add(Me.lbXx_conabpa)
    Me.pnPeve2.Controls.Add(Me.edTb_conabpa)
    Me.pnPeve2.Controls.Add(Me.lbTb_conabpa)
    Me.pnPeve2.Controls.Add(Me.lbXx_conabat)
    Me.pnPeve2.Controls.Add(Me.edTb_conabat)
    Me.pnPeve2.Controls.Add(Me.lbTb_conabat)
    Me.pnPeve2.Controls.Add(Me.lbXx_concas)
    Me.pnPeve2.Controls.Add(Me.edTb_concas)
    Me.pnPeve2.Controls.Add(Me.lbTb_concas)
    Me.pnPeve2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeve2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPeve2.Location = New System.Drawing.Point(0, 0)
    Me.pnPeve2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeve2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeve2.Name = "pnPeve2"
    Me.pnPeve2.NTSActiveTrasparency = True
    Me.pnPeve2.Size = New System.Drawing.Size(715, 279)
    Me.pnPeve2.TabIndex = 0
    Me.pnPeve2.Text = "NtsPanel1"
    '
    'lbXx_contimba
    '
    Me.lbXx_contimba.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_contimba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_contimba.Location = New System.Drawing.Point(330, 201)
    Me.lbXx_contimba.Name = "lbXx_contimba"
    Me.lbXx_contimba.NTSDbField = ""
    Me.lbXx_contimba.Size = New System.Drawing.Size(380, 20)
    Me.lbXx_contimba.TabIndex = 120
    Me.lbXx_contimba.Tooltip = ""
    Me.lbXx_contimba.UseMnemonic = False
    '
    'edTb_contimba
    '
    Me.edTb_contimba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_contimba.Location = New System.Drawing.Point(236, 201)
    Me.edTb_contimba.Name = "edTb_contimba"
    Me.edTb_contimba.NTSDbField = ""
    Me.edTb_contimba.NTSFormat = "0"
    Me.edTb_contimba.NTSForzaVisZoom = False
    Me.edTb_contimba.NTSOldValue = ""
    Me.edTb_contimba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_contimba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_contimba.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_contimba.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_contimba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_contimba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_contimba.Properties.AutoHeight = False
    Me.edTb_contimba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_contimba.Properties.MaxLength = 65536
    Me.edTb_contimba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_contimba.Size = New System.Drawing.Size(88, 20)
    Me.edTb_contimba.TabIndex = 20
    Me.edTb_contimba.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_contimba.TextInt = 0
    '
    'lbTb_contimba
    '
    Me.lbTb_contimba.AutoSize = True
    Me.lbTb_contimba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_contimba.Location = New System.Drawing.Point(13, 204)
    Me.lbTb_contimba.Name = "lbTb_contimba"
    Me.lbTb_contimba.NTSDbField = ""
    Me.lbTb_contimba.Size = New System.Drawing.Size(164, 13)
    Me.lbTb_contimba.TabIndex = 119
    Me.lbTb_contimba.Text = "Conto contropartita spese imballi"
    Me.lbTb_contimba.Tooltip = ""
    Me.lbTb_contimba.UseMnemonic = False
    '
    'lbXx_conclpriv
    '
    Me.lbXx_conclpriv.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conclpriv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conclpriv.Location = New System.Drawing.Point(330, 179)
    Me.lbXx_conclpriv.Name = "lbXx_conclpriv"
    Me.lbXx_conclpriv.NTSDbField = ""
    Me.lbXx_conclpriv.Size = New System.Drawing.Size(380, 20)
    Me.lbXx_conclpriv.TabIndex = 117
    Me.lbXx_conclpriv.Tooltip = ""
    Me.lbXx_conclpriv.UseMnemonic = False
    '
    'edTb_conclpriv
    '
    Me.edTb_conclpriv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conclpriv.Location = New System.Drawing.Point(236, 179)
    Me.edTb_conclpriv.Name = "edTb_conclpriv"
    Me.edTb_conclpriv.NTSDbField = ""
    Me.edTb_conclpriv.NTSFormat = "0"
    Me.edTb_conclpriv.NTSForzaVisZoom = False
    Me.edTb_conclpriv.NTSOldValue = ""
    Me.edTb_conclpriv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conclpriv.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conclpriv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conclpriv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conclpriv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conclpriv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conclpriv.Properties.AutoHeight = False
    Me.edTb_conclpriv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conclpriv.Properties.MaxLength = 65536
    Me.edTb_conclpriv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conclpriv.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conclpriv.TabIndex = 9
    Me.edTb_conclpriv.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conclpriv.TextInt = 0
    '
    'lbTb_conclpriv
    '
    Me.lbTb_conclpriv.AutoSize = True
    Me.lbTb_conclpriv.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conclpriv.Location = New System.Drawing.Point(13, 182)
    Me.lbTb_conclpriv.Name = "lbTb_conclpriv"
    Me.lbTb_conclpriv.NTSDbField = ""
    Me.lbTb_conclpriv.Size = New System.Drawing.Size(142, 13)
    Me.lbTb_conclpriv.TabIndex = 116
    Me.lbTb_conclpriv.Text = "Codice conto Cliente privato"
    Me.lbTb_conclpriv.Tooltip = ""
    Me.lbTb_conclpriv.UseMnemonic = False
    '
    'lbXx_concauz
    '
    Me.lbXx_concauz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_concauz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_concauz.Location = New System.Drawing.Point(330, 157)
    Me.lbXx_concauz.Name = "lbXx_concauz"
    Me.lbXx_concauz.NTSDbField = ""
    Me.lbXx_concauz.Size = New System.Drawing.Size(380, 20)
    Me.lbXx_concauz.TabIndex = 114
    Me.lbXx_concauz.Tooltip = ""
    Me.lbXx_concauz.UseMnemonic = False
    '
    'edTb_concauz
    '
    Me.edTb_concauz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_concauz.Location = New System.Drawing.Point(236, 157)
    Me.edTb_concauz.Name = "edTb_concauz"
    Me.edTb_concauz.NTSDbField = ""
    Me.edTb_concauz.NTSFormat = "0"
    Me.edTb_concauz.NTSForzaVisZoom = False
    Me.edTb_concauz.NTSOldValue = ""
    Me.edTb_concauz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_concauz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_concauz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_concauz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_concauz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_concauz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_concauz.Properties.AutoHeight = False
    Me.edTb_concauz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_concauz.Properties.MaxLength = 65536
    Me.edTb_concauz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_concauz.Size = New System.Drawing.Size(88, 20)
    Me.edTb_concauz.TabIndex = 1
    Me.edTb_concauz.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_concauz.TextInt = 0
    '
    'lbTb_concauz
    '
    Me.lbTb_concauz.AutoSize = True
    Me.lbTb_concauz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_concauz.Location = New System.Drawing.Point(13, 160)
    Me.lbTb_concauz.Name = "lbTb_concauz"
    Me.lbTb_concauz.NTSDbField = ""
    Me.lbTb_concauz.Size = New System.Drawing.Size(172, 13)
    Me.lbTb_concauz.TabIndex = 113
    Me.lbTb_concauz.Text = "Conto contropartita Cauzioni vuoti"
    Me.lbTb_concauz.Tooltip = ""
    Me.lbTb_concauz.UseMnemonic = False
    '
    'lbXx_conriac
    '
    Me.lbXx_conriac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conriac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conriac.Location = New System.Drawing.Point(330, 136)
    Me.lbXx_conriac.Name = "lbXx_conriac"
    Me.lbXx_conriac.NTSDbField = ""
    Me.lbXx_conriac.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conriac.TabIndex = 111
    Me.lbXx_conriac.Tooltip = ""
    Me.lbXx_conriac.UseMnemonic = False
    '
    'edTb_conriac
    '
    Me.edTb_conriac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conriac.Location = New System.Drawing.Point(236, 135)
    Me.edTb_conriac.Name = "edTb_conriac"
    Me.edTb_conriac.NTSDbField = ""
    Me.edTb_conriac.NTSFormat = "0"
    Me.edTb_conriac.NTSForzaVisZoom = False
    Me.edTb_conriac.NTSOldValue = ""
    Me.edTb_conriac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conriac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conriac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conriac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conriac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conriac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conriac.Properties.AutoHeight = False
    Me.edTb_conriac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conriac.Properties.MaxLength = 65536
    Me.edTb_conriac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conriac.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conriac.TabIndex = 17
    Me.edTb_conriac.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conriac.TextInt = 0
    '
    'lbTb_conriac
    '
    Me.lbTb_conriac.AutoSize = True
    Me.lbTb_conriac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conriac.Location = New System.Drawing.Point(13, 138)
    Me.lbTb_conriac.Name = "lbTb_conriac"
    Me.lbTb_conriac.NTSDbField = ""
    Me.lbTb_conriac.Size = New System.Drawing.Size(217, 13)
    Me.lbTb_conriac.TabIndex = 110
    Me.lbTb_conriac.Text = "Conto contropartita Ricavi spese accessorie"
    Me.lbTb_conriac.Tooltip = ""
    Me.lbTb_conriac.UseMnemonic = False
    '
    'lbXx_conriin
    '
    Me.lbXx_conriin.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conriin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conriin.Location = New System.Drawing.Point(330, 113)
    Me.lbXx_conriin.Name = "lbXx_conriin"
    Me.lbXx_conriin.NTSDbField = ""
    Me.lbXx_conriin.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conriin.TabIndex = 108
    Me.lbXx_conriin.Tooltip = ""
    Me.lbXx_conriin.UseMnemonic = False
    '
    'edTb_conriin
    '
    Me.edTb_conriin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conriin.Location = New System.Drawing.Point(236, 113)
    Me.edTb_conriin.Name = "edTb_conriin"
    Me.edTb_conriin.NTSDbField = ""
    Me.edTb_conriin.NTSFormat = "0"
    Me.edTb_conriin.NTSForzaVisZoom = False
    Me.edTb_conriin.NTSOldValue = ""
    Me.edTb_conriin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conriin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conriin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conriin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conriin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conriin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conriin.Properties.AutoHeight = False
    Me.edTb_conriin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conriin.Properties.MaxLength = 65536
    Me.edTb_conriin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conriin.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conriin.TabIndex = 6
    Me.edTb_conriin.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conriin.TextInt = 0
    '
    'lbTb_conriin
    '
    Me.lbTb_conriin.AutoSize = True
    Me.lbTb_conriin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conriin.Location = New System.Drawing.Point(13, 116)
    Me.lbTb_conriin.Name = "lbTb_conriin"
    Me.lbTb_conriin.NTSDbField = ""
    Me.lbTb_conriin.Size = New System.Drawing.Size(202, 13)
    Me.lbTb_conriin.TabIndex = 107
    Me.lbTb_conriin.Text = "Conto contropartita Ricavi spese incasso"
    Me.lbTb_conriin.Tooltip = ""
    Me.lbTb_conriin.UseMnemonic = False
    '
    'lbXx_conribo
    '
    Me.lbXx_conribo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conribo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conribo.Location = New System.Drawing.Point(330, 91)
    Me.lbXx_conribo.Name = "lbXx_conribo"
    Me.lbXx_conribo.NTSDbField = ""
    Me.lbXx_conribo.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conribo.TabIndex = 105
    Me.lbXx_conribo.Tooltip = ""
    Me.lbXx_conribo.UseMnemonic = False
    '
    'edTb_conribo
    '
    Me.edTb_conribo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conribo.Location = New System.Drawing.Point(236, 91)
    Me.edTb_conribo.Name = "edTb_conribo"
    Me.edTb_conribo.NTSDbField = ""
    Me.edTb_conribo.NTSFormat = "0"
    Me.edTb_conribo.NTSForzaVisZoom = False
    Me.edTb_conribo.NTSOldValue = ""
    Me.edTb_conribo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conribo.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conribo.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conribo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conribo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conribo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conribo.Properties.AutoHeight = False
    Me.edTb_conribo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conribo.Properties.MaxLength = 65536
    Me.edTb_conribo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conribo.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conribo.TabIndex = 1
    Me.edTb_conribo.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conribo.TextInt = 0
    '
    'lbTb_conribo
    '
    Me.lbTb_conribo.AutoSize = True
    Me.lbTb_conribo.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conribo.Location = New System.Drawing.Point(13, 94)
    Me.lbTb_conribo.Name = "lbTb_conribo"
    Me.lbTb_conribo.NTSDbField = ""
    Me.lbTb_conribo.Size = New System.Drawing.Size(169, 13)
    Me.lbTb_conribo.TabIndex = 104
    Me.lbTb_conribo.Text = "Conto contropartita Ricavi da bolli"
    Me.lbTb_conribo.Tooltip = ""
    Me.lbTb_conribo.UseMnemonic = False
    '
    'lbXx_conomag
    '
    Me.lbXx_conomag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conomag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conomag.Location = New System.Drawing.Point(330, 69)
    Me.lbXx_conomag.Name = "lbXx_conomag"
    Me.lbXx_conomag.NTSDbField = ""
    Me.lbXx_conomag.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conomag.TabIndex = 102
    Me.lbXx_conomag.Tooltip = ""
    Me.lbXx_conomag.UseMnemonic = False
    '
    'edTb_conomag
    '
    Me.edTb_conomag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conomag.Location = New System.Drawing.Point(236, 69)
    Me.edTb_conomag.Name = "edTb_conomag"
    Me.edTb_conomag.NTSDbField = ""
    Me.edTb_conomag.NTSFormat = "0"
    Me.edTb_conomag.NTSForzaVisZoom = False
    Me.edTb_conomag.NTSOldValue = ""
    Me.edTb_conomag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conomag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conomag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conomag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conomag.Properties.AutoHeight = False
    Me.edTb_conomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conomag.Properties.MaxLength = 65536
    Me.edTb_conomag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conomag.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conomag.TabIndex = 14
    Me.edTb_conomag.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conomag.TextInt = 0
    '
    'lbTb_conomag
    '
    Me.lbTb_conomag.AutoSize = True
    Me.lbTb_conomag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conomag.Location = New System.Drawing.Point(14, 72)
    Me.lbTb_conomag.Name = "lbTb_conomag"
    Me.lbTb_conomag.NTSDbField = ""
    Me.lbTb_conomag.Size = New System.Drawing.Size(178, 13)
    Me.lbTb_conomag.TabIndex = 101
    Me.lbTb_conomag.Text = "Conto contropartita Omaggi (costo)"
    Me.lbTb_conomag.Tooltip = ""
    Me.lbTb_conomag.UseMnemonic = False
    '
    'lbXx_conabpa
    '
    Me.lbXx_conabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conabpa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conabpa.Location = New System.Drawing.Point(330, 47)
    Me.lbXx_conabpa.Name = "lbXx_conabpa"
    Me.lbXx_conabpa.NTSDbField = ""
    Me.lbXx_conabpa.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conabpa.TabIndex = 99
    Me.lbXx_conabpa.Tooltip = ""
    Me.lbXx_conabpa.UseMnemonic = False
    '
    'edTb_conabpa
    '
    Me.edTb_conabpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conabpa.Location = New System.Drawing.Point(236, 47)
    Me.edTb_conabpa.Name = "edTb_conabpa"
    Me.edTb_conabpa.NTSDbField = ""
    Me.edTb_conabpa.NTSFormat = "0"
    Me.edTb_conabpa.NTSForzaVisZoom = False
    Me.edTb_conabpa.NTSOldValue = ""
    Me.edTb_conabpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conabpa.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conabpa.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conabpa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conabpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conabpa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conabpa.Properties.AutoHeight = False
    Me.edTb_conabpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conabpa.Properties.MaxLength = 65536
    Me.edTb_conabpa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conabpa.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conabpa.TabIndex = 13
    Me.edTb_conabpa.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conabpa.TextInt = 0
    '
    'lbTb_conabpa
    '
    Me.lbTb_conabpa.AutoSize = True
    Me.lbTb_conabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conabpa.Location = New System.Drawing.Point(13, 50)
    Me.lbTb_conabpa.Name = "lbTb_conabpa"
    Me.lbTb_conabpa.NTSDbField = ""
    Me.lbTb_conabpa.Size = New System.Drawing.Size(179, 13)
    Me.lbTb_conabpa.TabIndex = 98
    Me.lbTb_conabpa.Text = "Conto contropartita Abbuoni passivi"
    Me.lbTb_conabpa.Tooltip = ""
    Me.lbTb_conabpa.UseMnemonic = False
    '
    'lbXx_conabat
    '
    Me.lbXx_conabat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conabat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conabat.Location = New System.Drawing.Point(330, 25)
    Me.lbXx_conabat.Name = "lbXx_conabat"
    Me.lbXx_conabat.NTSDbField = ""
    Me.lbXx_conabat.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_conabat.TabIndex = 96
    Me.lbXx_conabat.Tooltip = ""
    Me.lbXx_conabat.UseMnemonic = False
    '
    'edTb_conabat
    '
    Me.edTb_conabat.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_conabat.Location = New System.Drawing.Point(236, 25)
    Me.edTb_conabat.Name = "edTb_conabat"
    Me.edTb_conabat.NTSDbField = ""
    Me.edTb_conabat.NTSFormat = "0"
    Me.edTb_conabat.NTSForzaVisZoom = False
    Me.edTb_conabat.NTSOldValue = ""
    Me.edTb_conabat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conabat.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conabat.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conabat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conabat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conabat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conabat.Properties.AutoHeight = False
    Me.edTb_conabat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conabat.Properties.MaxLength = 65536
    Me.edTb_conabat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conabat.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conabat.TabIndex = 12
    Me.edTb_conabat.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conabat.TextInt = 0
    '
    'lbTb_conabat
    '
    Me.lbTb_conabat.AutoSize = True
    Me.lbTb_conabat.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conabat.Location = New System.Drawing.Point(14, 28)
    Me.lbTb_conabat.Name = "lbTb_conabat"
    Me.lbTb_conabat.NTSDbField = ""
    Me.lbTb_conabat.Size = New System.Drawing.Size(171, 13)
    Me.lbTb_conabat.TabIndex = 95
    Me.lbTb_conabat.Text = "Conto contropartita Abbuoni attivi"
    Me.lbTb_conabat.Tooltip = ""
    Me.lbTb_conabat.UseMnemonic = False
    '
    'lbXx_concas
    '
    Me.lbXx_concas.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_concas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_concas.Location = New System.Drawing.Point(330, 3)
    Me.lbXx_concas.Name = "lbXx_concas"
    Me.lbXx_concas.NTSDbField = ""
    Me.lbXx_concas.Size = New System.Drawing.Size(369, 20)
    Me.lbXx_concas.TabIndex = 93
    Me.lbXx_concas.Tooltip = ""
    Me.lbXx_concas.UseMnemonic = False
    '
    'edTb_concas
    '
    Me.edTb_concas.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_concas.Location = New System.Drawing.Point(236, 3)
    Me.edTb_concas.Name = "edTb_concas"
    Me.edTb_concas.NTSDbField = ""
    Me.edTb_concas.NTSFormat = "0"
    Me.edTb_concas.NTSForzaVisZoom = False
    Me.edTb_concas.NTSOldValue = ""
    Me.edTb_concas.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_concas.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_concas.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_concas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_concas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_concas.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_concas.Properties.AutoHeight = False
    Me.edTb_concas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_concas.Properties.MaxLength = 65536
    Me.edTb_concas.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_concas.Size = New System.Drawing.Size(88, 20)
    Me.edTb_concas.TabIndex = 11
    Me.edTb_concas.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_concas.TextInt = 0
    '
    'lbTb_concas
    '
    Me.lbTb_concas.AutoSize = True
    Me.lbTb_concas.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_concas.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_concas.Name = "lbTb_concas"
    Me.lbTb_concas.NTSDbField = ""
    Me.lbTb_concas.Size = New System.Drawing.Size(134, 13)
    Me.lbTb_concas.TabIndex = 92
    Me.lbTb_concas.Text = "Conto contropartita Cassa"
    Me.lbTb_concas.Tooltip = ""
    Me.lbTb_concas.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage3.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage3.Controls.Add(Me.pnPeve3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(715, 279)
    Me.NtsTabPage3.Text = "Pagina &3"
    '
    'pnPeve3
    '
    Me.pnPeve3.AllowDrop = True
    Me.pnPeve3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeve3.Appearance.Options.UseBackColor = True
    Me.pnPeve3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeve3.Controls.Add(Me.edXx_vlistin)
    Me.pnPeve3.Controls.Add(Me.fraFrame1)
    Me.pnPeve3.Controls.Add(Me.lbTb_vacurad)
    Me.pnPeve3.Controls.Add(Me.cbTb_vacurad)
    Me.pnPeve3.Controls.Add(Me.lbXx_vcautra)
    Me.pnPeve3.Controls.Add(Me.edTb_vcautra)
    Me.pnPeve3.Controls.Add(Me.lbTb_vcautra)
    Me.pnPeve3.Controls.Add(Me.edTb_vlistin)
    Me.pnPeve3.Controls.Add(Me.lbTb_vlistin)
    Me.pnPeve3.Controls.Add(Me.lbXx_vcaumag)
    Me.pnPeve3.Controls.Add(Me.edTb_vcaumag)
    Me.pnPeve3.Controls.Add(Me.lbTb_vcaumag)
    Me.pnPeve3.Controls.Add(Me.lbXx_vmagazz)
    Me.pnPeve3.Controls.Add(Me.edTb_vmagazz)
    Me.pnPeve3.Controls.Add(Me.lbTb_vmagazz)
    Me.pnPeve3.Controls.Add(Me.lbXx_vcontro)
    Me.pnPeve3.Controls.Add(Me.edTb_vcontro)
    Me.pnPeve3.Controls.Add(Me.lbTb_vcontro)
    Me.pnPeve3.Controls.Add(Me.lbXx_vtipobf)
    Me.pnPeve3.Controls.Add(Me.edTb_vtipobf)
    Me.pnPeve3.Controls.Add(Me.lbTb_vtipobf)
    Me.pnPeve3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeve3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPeve3.Location = New System.Drawing.Point(0, 0)
    Me.pnPeve3.Name = "pnPeve3"
    Me.pnPeve3.NTSActiveTrasparency = True
    Me.pnPeve3.Size = New System.Drawing.Size(715, 279)
    Me.pnPeve3.TabIndex = 0
    Me.pnPeve3.Text = "NtsPanel1"
    '
    'fraFrame1
    '
    Me.fraFrame1.AllowDrop = True
    Me.fraFrame1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fraFrame1.Appearance.Options.UseBackColor = True
    Me.fraFrame1.Controls.Add(Me.ckTb_vdatora)
    Me.fraFrame1.Controls.Add(Me.ckTb_vscorpo)
    Me.fraFrame1.Cursor = System.Windows.Forms.Cursors.Default
    Me.fraFrame1.Location = New System.Drawing.Point(416, 138)
    Me.fraFrame1.Name = "fraFrame1"
    Me.fraFrame1.ShowCaption = False
    Me.fraFrame1.Size = New System.Drawing.Size(281, 44)
    Me.fraFrame1.TabIndex = 129
    '
    'ckTb_vdatora
    '
    Me.ckTb_vdatora.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_vdatora.Location = New System.Drawing.Point(20, 21)
    Me.ckTb_vdatora.Name = "ckTb_vdatora"
    Me.ckTb_vdatora.NTSCheckValue = "S"
    Me.ckTb_vdatora.NTSUnCheckValue = "N"
    Me.ckTb_vdatora.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_vdatora.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_vdatora.Properties.AutoHeight = False
    Me.ckTb_vdatora.Properties.Caption = "Proposta data/ora inizio trasporto"
    Me.ckTb_vdatora.Size = New System.Drawing.Size(191, 19)
    Me.ckTb_vdatora.TabIndex = 29
    '
    'ckTb_vscorpo
    '
    Me.ckTb_vscorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_vscorpo.Location = New System.Drawing.Point(20, 5)
    Me.ckTb_vscorpo.Name = "ckTb_vscorpo"
    Me.ckTb_vscorpo.NTSCheckValue = "S"
    Me.ckTb_vscorpo.NTSUnCheckValue = "N"
    Me.ckTb_vscorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_vscorpo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_vscorpo.Properties.AutoHeight = False
    Me.ckTb_vscorpo.Properties.Caption = "Scorporo in testata bolla"
    Me.ckTb_vscorpo.Size = New System.Drawing.Size(156, 19)
    Me.ckTb_vscorpo.TabIndex = 28
    '
    'lbTb_vacurad
    '
    Me.lbTb_vacurad.AutoSize = True
    Me.lbTb_vacurad.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vacurad.Location = New System.Drawing.Point(13, 138)
    Me.lbTb_vacurad.Name = "lbTb_vacurad"
    Me.lbTb_vacurad.NTSDbField = ""
    Me.lbTb_vacurad.Size = New System.Drawing.Size(166, 13)
    Me.lbTb_vacurad.TabIndex = 109
    Me.lbTb_vacurad.Text = "Trasporto a cura del (piede bolla)"
    Me.lbTb_vacurad.Tooltip = ""
    Me.lbTb_vacurad.UseMnemonic = False
    '
    'cbTb_vacurad
    '
    Me.cbTb_vacurad.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vacurad.DataSource = Nothing
    Me.cbTb_vacurad.DisplayMember = ""
    Me.cbTb_vacurad.Location = New System.Drawing.Point(236, 135)
    Me.cbTb_vacurad.Name = "cbTb_vacurad"
    Me.cbTb_vacurad.NTSDbField = ""
    Me.cbTb_vacurad.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vacurad.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vacurad.Properties.AutoHeight = False
    Me.cbTb_vacurad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vacurad.Properties.DropDownRows = 30
    Me.cbTb_vacurad.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vacurad.SelectedValue = ""
    Me.cbTb_vacurad.Size = New System.Drawing.Size(164, 20)
    Me.cbTb_vacurad.TabIndex = 27
    Me.cbTb_vacurad.ValueMember = ""
    '
    'lbXx_vcautra
    '
    Me.lbXx_vcautra.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vcautra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vcautra.Location = New System.Drawing.Point(311, 113)
    Me.lbXx_vcautra.Name = "lbXx_vcautra"
    Me.lbXx_vcautra.NTSDbField = ""
    Me.lbXx_vcautra.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_vcautra.TabIndex = 107
    Me.lbXx_vcautra.Tooltip = ""
    Me.lbXx_vcautra.UseMnemonic = False
    '
    'edTb_vcautra
    '
    Me.edTb_vcautra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_vcautra.Location = New System.Drawing.Point(236, 113)
    Me.edTb_vcautra.Name = "edTb_vcautra"
    Me.edTb_vcautra.NTSDbField = ""
    Me.edTb_vcautra.NTSFormat = "0"
    Me.edTb_vcautra.NTSForzaVisZoom = False
    Me.edTb_vcautra.NTSOldValue = ""
    Me.edTb_vcautra.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vcautra.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vcautra.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vcautra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vcautra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vcautra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vcautra.Properties.AutoHeight = False
    Me.edTb_vcautra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vcautra.Properties.MaxLength = 65536
    Me.edTb_vcautra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vcautra.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vcautra.TabIndex = 26
    Me.edTb_vcautra.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vcautra.TextInt = 0
    '
    'lbTb_vcautra
    '
    Me.lbTb_vcautra.AutoSize = True
    Me.lbTb_vcautra.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vcautra.Location = New System.Drawing.Point(13, 116)
    Me.lbTb_vcautra.Name = "lbTb_vcautra"
    Me.lbTb_vcautra.NTSDbField = ""
    Me.lbTb_vcautra.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_vcautra.TabIndex = 106
    Me.lbTb_vcautra.Text = "Codice causale trasporto piede bolla"
    Me.lbTb_vcautra.Tooltip = ""
    Me.lbTb_vcautra.UseMnemonic = False
    '
    'edTb_vlistin
    '
    Me.edTb_vlistin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_vlistin.Location = New System.Drawing.Point(236, 91)
    Me.edTb_vlistin.Name = "edTb_vlistin"
    Me.edTb_vlistin.NTSDbField = ""
    Me.edTb_vlistin.NTSFormat = "0"
    Me.edTb_vlistin.NTSForzaVisZoom = False
    Me.edTb_vlistin.NTSOldValue = ""
    Me.edTb_vlistin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vlistin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vlistin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vlistin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vlistin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vlistin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vlistin.Properties.AutoHeight = False
    Me.edTb_vlistin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vlistin.Properties.MaxLength = 65536
    Me.edTb_vlistin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vlistin.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vlistin.TabIndex = 25
    Me.edTb_vlistin.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vlistin.TextInt = 0
    '
    'lbTb_vlistin
    '
    Me.lbTb_vlistin.AutoSize = True
    Me.lbTb_vlistin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vlistin.Location = New System.Drawing.Point(13, 94)
    Me.lbTb_vlistin.Name = "lbTb_vlistin"
    Me.lbTb_vlistin.NTSDbField = ""
    Me.lbTb_vlistin.Size = New System.Drawing.Size(100, 13)
    Me.lbTb_vlistin.TabIndex = 104
    Me.lbTb_vlistin.Text = "Listino testata bolla"
    Me.lbTb_vlistin.Tooltip = ""
    Me.lbTb_vlistin.UseMnemonic = False
    '
    'lbXx_vcaumag
    '
    Me.lbXx_vcaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vcaumag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vcaumag.Location = New System.Drawing.Point(311, 69)
    Me.lbXx_vcaumag.Name = "lbXx_vcaumag"
    Me.lbXx_vcaumag.NTSDbField = ""
    Me.lbXx_vcaumag.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_vcaumag.TabIndex = 102
    Me.lbXx_vcaumag.Tooltip = ""
    Me.lbXx_vcaumag.UseMnemonic = False
    '
    'edTb_vcaumag
    '
    Me.edTb_vcaumag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_vcaumag.Location = New System.Drawing.Point(236, 69)
    Me.edTb_vcaumag.Name = "edTb_vcaumag"
    Me.edTb_vcaumag.NTSDbField = ""
    Me.edTb_vcaumag.NTSFormat = "0"
    Me.edTb_vcaumag.NTSForzaVisZoom = False
    Me.edTb_vcaumag.NTSOldValue = ""
    Me.edTb_vcaumag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vcaumag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vcaumag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vcaumag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vcaumag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vcaumag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vcaumag.Properties.AutoHeight = False
    Me.edTb_vcaumag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vcaumag.Properties.MaxLength = 65536
    Me.edTb_vcaumag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vcaumag.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vcaumag.TabIndex = 24
    Me.edTb_vcaumag.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vcaumag.TextInt = 0
    '
    'lbTb_vcaumag
    '
    Me.lbTb_vcaumag.AutoSize = True
    Me.lbTb_vcaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vcaumag.Location = New System.Drawing.Point(13, 72)
    Me.lbTb_vcaumag.Name = "lbTb_vcaumag"
    Me.lbTb_vcaumag.NTSDbField = ""
    Me.lbTb_vcaumag.Size = New System.Drawing.Size(194, 13)
    Me.lbTb_vcaumag.TabIndex = 101
    Me.lbTb_vcaumag.Text = "Codice causale magazzino testata bolla"
    Me.lbTb_vcaumag.Tooltip = ""
    Me.lbTb_vcaumag.UseMnemonic = False
    '
    'lbXx_vmagazz
    '
    Me.lbXx_vmagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vmagazz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vmagazz.Location = New System.Drawing.Point(311, 47)
    Me.lbXx_vmagazz.Name = "lbXx_vmagazz"
    Me.lbXx_vmagazz.NTSDbField = ""
    Me.lbXx_vmagazz.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_vmagazz.TabIndex = 99
    Me.lbXx_vmagazz.Tooltip = ""
    Me.lbXx_vmagazz.UseMnemonic = False
    '
    'edTb_vmagazz
    '
    Me.edTb_vmagazz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_vmagazz.Location = New System.Drawing.Point(236, 47)
    Me.edTb_vmagazz.Name = "edTb_vmagazz"
    Me.edTb_vmagazz.NTSDbField = ""
    Me.edTb_vmagazz.NTSFormat = "0"
    Me.edTb_vmagazz.NTSForzaVisZoom = False
    Me.edTb_vmagazz.NTSOldValue = ""
    Me.edTb_vmagazz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vmagazz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vmagazz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vmagazz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vmagazz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vmagazz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vmagazz.Properties.AutoHeight = False
    Me.edTb_vmagazz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vmagazz.Properties.MaxLength = 65536
    Me.edTb_vmagazz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vmagazz.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vmagazz.TabIndex = 23
    Me.edTb_vmagazz.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vmagazz.TextInt = 0
    '
    'lbTb_vmagazz
    '
    Me.lbTb_vmagazz.AutoSize = True
    Me.lbTb_vmagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vmagazz.Location = New System.Drawing.Point(13, 50)
    Me.lbTb_vmagazz.Name = "lbTb_vmagazz"
    Me.lbTb_vmagazz.NTSDbField = ""
    Me.lbTb_vmagazz.Size = New System.Drawing.Size(155, 13)
    Me.lbTb_vmagazz.TabIndex = 98
    Me.lbTb_vmagazz.Text = "Codice magazzino testata bolla"
    Me.lbTb_vmagazz.Tooltip = ""
    Me.lbTb_vmagazz.UseMnemonic = False
    '
    'lbXx_vcontro
    '
    Me.lbXx_vcontro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vcontro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vcontro.Location = New System.Drawing.Point(311, 25)
    Me.lbXx_vcontro.Name = "lbXx_vcontro"
    Me.lbXx_vcontro.NTSDbField = ""
    Me.lbXx_vcontro.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_vcontro.TabIndex = 96
    Me.lbXx_vcontro.Tooltip = ""
    Me.lbXx_vcontro.UseMnemonic = False
    '
    'edTb_vcontro
    '
    Me.edTb_vcontro.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_vcontro.Location = New System.Drawing.Point(236, 25)
    Me.edTb_vcontro.Name = "edTb_vcontro"
    Me.edTb_vcontro.NTSDbField = ""
    Me.edTb_vcontro.NTSFormat = "0"
    Me.edTb_vcontro.NTSForzaVisZoom = False
    Me.edTb_vcontro.NTSOldValue = ""
    Me.edTb_vcontro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vcontro.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vcontro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vcontro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vcontro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vcontro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vcontro.Properties.AutoHeight = False
    Me.edTb_vcontro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vcontro.Properties.MaxLength = 65536
    Me.edTb_vcontro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vcontro.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vcontro.TabIndex = 22
    Me.edTb_vcontro.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vcontro.TextInt = 0
    '
    'lbTb_vcontro
    '
    Me.lbTb_vcontro.AutoSize = True
    Me.lbTb_vcontro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vcontro.Location = New System.Drawing.Point(13, 28)
    Me.lbTb_vcontro.Name = "lbTb_vcontro"
    Me.lbTb_vcontro.NTSDbField = ""
    Me.lbTb_vcontro.Size = New System.Drawing.Size(168, 13)
    Me.lbTb_vcontro.TabIndex = 95
    Me.lbTb_vcontro.Text = "Codice contropartita testata bolla"
    Me.lbTb_vcontro.Tooltip = ""
    Me.lbTb_vcontro.UseMnemonic = False
    '
    'lbXx_vtipobf
    '
    Me.lbXx_vtipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vtipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vtipobf.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_vtipobf.Name = "lbXx_vtipobf"
    Me.lbXx_vtipobf.NTSDbField = ""
    Me.lbXx_vtipobf.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_vtipobf.TabIndex = 93
    Me.lbXx_vtipobf.Tooltip = ""
    Me.lbXx_vtipobf.UseMnemonic = False
    '
    'edTb_vtipobf
    '
    Me.edTb_vtipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_vtipobf.Location = New System.Drawing.Point(236, 3)
    Me.edTb_vtipobf.Name = "edTb_vtipobf"
    Me.edTb_vtipobf.NTSDbField = ""
    Me.edTb_vtipobf.NTSFormat = "0"
    Me.edTb_vtipobf.NTSForzaVisZoom = False
    Me.edTb_vtipobf.NTSOldValue = ""
    Me.edTb_vtipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_vtipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_vtipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vtipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vtipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vtipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vtipobf.Properties.AutoHeight = False
    Me.edTb_vtipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vtipobf.Properties.MaxLength = 65536
    Me.edTb_vtipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vtipobf.Size = New System.Drawing.Size(69, 20)
    Me.edTb_vtipobf.TabIndex = 21
    Me.edTb_vtipobf.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_vtipobf.TextInt = 0
    '
    'lbTb_vtipobf
    '
    Me.lbTb_vtipobf.AutoSize = True
    Me.lbTb_vtipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vtipobf.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_vtipobf.Name = "lbTb_vtipobf"
    Me.lbTb_vtipobf.NTSDbField = ""
    Me.lbTb_vtipobf.Size = New System.Drawing.Size(216, 13)
    Me.lbTb_vtipobf.TabIndex = 92
    Me.lbTb_vtipobf.Text = "Tipo bolla/fattura generico per testata bolla"
    Me.lbTb_vtipobf.Tooltip = ""
    Me.lbTb_vtipobf.UseMnemonic = False
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPeve4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(715, 279)
    Me.NtsTabPage4.Text = "Pagina &4"
    '
    'pnPeve4
    '
    Me.pnPeve4.AllowDrop = True
    Me.pnPeve4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeve4.Appearance.Options.UseBackColor = True
    Me.pnPeve4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfriep)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfriep)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfriep)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfnonp)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfnonp)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfnonp)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfparp)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfparp)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfparp)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfincp)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfincp)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfincp)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfriec)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfriec)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfriec)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfnonc)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfnonc)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfnonc)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfparc)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfparc)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfparc)
    Me.pnPeve4.Controls.Add(Me.lbXx_caurfincc)
    Me.pnPeve4.Controls.Add(Me.edTb_caurfincc)
    Me.pnPeve4.Controls.Add(Me.lbTb_caurfincc)
    Me.pnPeve4.Controls.Add(Me.lbXx_conivrfp)
    Me.pnPeve4.Controls.Add(Me.edTb_conivrfp)
    Me.pnPeve4.Controls.Add(Me.lbTb_conivrfp)
    Me.pnPeve4.Controls.Add(Me.lbXx_concrrfp)
    Me.pnPeve4.Controls.Add(Me.edTb_concrrfp)
    Me.pnPeve4.Controls.Add(Me.lbTb_concrrfp)
    Me.pnPeve4.Controls.Add(Me.lbXx_concrrfc)
    Me.pnPeve4.Controls.Add(Me.edTb_concrrfc)
    Me.pnPeve4.Controls.Add(Me.lbTb_concrrfc)
    Me.pnPeve4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeve4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPeve4.Location = New System.Drawing.Point(0, 0)
    Me.pnPeve4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeve4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeve4.Name = "pnPeve4"
    Me.pnPeve4.NTSActiveTrasparency = True
    Me.pnPeve4.Size = New System.Drawing.Size(715, 279)
    Me.pnPeve4.TabIndex = 0
    Me.pnPeve4.Text = "NtsPanel1"
    '
    'lbXx_caurfriep
    '
    Me.lbXx_caurfriep.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfriep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfriep.Location = New System.Drawing.Point(359, 225)
    Me.lbXx_caurfriep.Name = "lbXx_caurfriep"
    Me.lbXx_caurfriep.NTSDbField = ""
    Me.lbXx_caurfriep.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfriep.TabIndex = 126
    Me.lbXx_caurfriep.Tooltip = ""
    Me.lbXx_caurfriep.UseMnemonic = False
    '
    'edTb_caurfriep
    '
    Me.edTb_caurfriep.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfriep.Location = New System.Drawing.Point(284, 223)
    Me.edTb_caurfriep.Name = "edTb_caurfriep"
    Me.edTb_caurfriep.NTSDbField = ""
    Me.edTb_caurfriep.NTSFormat = "0"
    Me.edTb_caurfriep.NTSForzaVisZoom = False
    Me.edTb_caurfriep.NTSOldValue = ""
    Me.edTb_caurfriep.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfriep.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfriep.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfriep.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfriep.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfriep.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfriep.Properties.AutoHeight = False
    Me.edTb_caurfriep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfriep.Properties.MaxLength = 65536
    Me.edTb_caurfriep.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfriep.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfriep.TabIndex = 42
    Me.edTb_caurfriep.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfriep.TextInt = 0
    '
    'lbTb_caurfriep
    '
    Me.lbTb_caurfriep.AutoSize = True
    Me.lbTb_caurfriep.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfriep.Location = New System.Drawing.Point(13, 226)
    Me.lbTb_caurfriep.Name = "lbTb_caurfriep"
    Me.lbTb_caurfriep.NTSDbField = ""
    Me.lbTb_caurfriep.Size = New System.Drawing.Size(187, 13)
    Me.lbTb_caurfriep.TabIndex = 125
    Me.lbTb_caurfriep.Text = "Causale R.F. riemissione (prestazioni)"
    Me.lbTb_caurfriep.Tooltip = ""
    Me.lbTb_caurfriep.UseMnemonic = False
    '
    'lbXx_caurfnonp
    '
    Me.lbXx_caurfnonp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfnonp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfnonp.Location = New System.Drawing.Point(359, 201)
    Me.lbXx_caurfnonp.Name = "lbXx_caurfnonp"
    Me.lbXx_caurfnonp.NTSDbField = ""
    Me.lbXx_caurfnonp.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfnonp.TabIndex = 123
    Me.lbXx_caurfnonp.Tooltip = ""
    Me.lbXx_caurfnonp.UseMnemonic = False
    '
    'edTb_caurfnonp
    '
    Me.edTb_caurfnonp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfnonp.Location = New System.Drawing.Point(284, 201)
    Me.edTb_caurfnonp.Name = "edTb_caurfnonp"
    Me.edTb_caurfnonp.NTSDbField = ""
    Me.edTb_caurfnonp.NTSFormat = "0"
    Me.edTb_caurfnonp.NTSForzaVisZoom = False
    Me.edTb_caurfnonp.NTSOldValue = ""
    Me.edTb_caurfnonp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfnonp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfnonp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfnonp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfnonp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfnonp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfnonp.Properties.AutoHeight = False
    Me.edTb_caurfnonp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfnonp.Properties.MaxLength = 65536
    Me.edTb_caurfnonp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfnonp.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfnonp.TabIndex = 41
    Me.edTb_caurfnonp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfnonp.TextInt = 0
    '
    'lbTb_caurfnonp
    '
    Me.lbTb_caurfnonp.AutoSize = True
    Me.lbTb_caurfnonp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfnonp.Location = New System.Drawing.Point(13, 204)
    Me.lbTb_caurfnonp.Name = "lbTb_caurfnonp"
    Me.lbTb_caurfnonp.NTSDbField = ""
    Me.lbTb_caurfnonp.Size = New System.Drawing.Size(201, 13)
    Me.lbTb_caurfnonp.TabIndex = 122
    Me.lbTb_caurfnonp.Text = "Causale R.F. non incassata (prestazioni)"
    Me.lbTb_caurfnonp.Tooltip = ""
    Me.lbTb_caurfnonp.UseMnemonic = False
    '
    'lbXx_caurfparp
    '
    Me.lbXx_caurfparp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfparp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfparp.Location = New System.Drawing.Point(359, 179)
    Me.lbXx_caurfparp.Name = "lbXx_caurfparp"
    Me.lbXx_caurfparp.NTSDbField = ""
    Me.lbXx_caurfparp.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfparp.TabIndex = 120
    Me.lbXx_caurfparp.Tooltip = ""
    Me.lbXx_caurfparp.UseMnemonic = False
    '
    'edTb_caurfparp
    '
    Me.edTb_caurfparp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfparp.Location = New System.Drawing.Point(284, 179)
    Me.edTb_caurfparp.Name = "edTb_caurfparp"
    Me.edTb_caurfparp.NTSDbField = ""
    Me.edTb_caurfparp.NTSFormat = "0"
    Me.edTb_caurfparp.NTSForzaVisZoom = False
    Me.edTb_caurfparp.NTSOldValue = ""
    Me.edTb_caurfparp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfparp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfparp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfparp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfparp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfparp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfparp.Properties.AutoHeight = False
    Me.edTb_caurfparp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfparp.Properties.MaxLength = 65536
    Me.edTb_caurfparp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfparp.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfparp.TabIndex = 40
    Me.edTb_caurfparp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfparp.TextInt = 0
    '
    'lbTb_caurfparp
    '
    Me.lbTb_caurfparp.AutoSize = True
    Me.lbTb_caurfparp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfparp.Location = New System.Drawing.Point(13, 182)
    Me.lbTb_caurfparp.Name = "lbTb_caurfparp"
    Me.lbTb_caurfparp.NTSDbField = ""
    Me.lbTb_caurfparp.Size = New System.Drawing.Size(244, 13)
    Me.lbTb_caurfparp.TabIndex = 119
    Me.lbTb_caurfparp.Text = "Causale R.F. parzialmente incassata (prestazioni)"
    Me.lbTb_caurfparp.Tooltip = ""
    Me.lbTb_caurfparp.UseMnemonic = False
    '
    'lbXx_caurfincp
    '
    Me.lbXx_caurfincp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfincp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfincp.Location = New System.Drawing.Point(359, 157)
    Me.lbXx_caurfincp.Name = "lbXx_caurfincp"
    Me.lbXx_caurfincp.NTSDbField = ""
    Me.lbXx_caurfincp.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfincp.TabIndex = 117
    Me.lbXx_caurfincp.Tooltip = ""
    Me.lbXx_caurfincp.UseMnemonic = False
    '
    'edTb_caurfincp
    '
    Me.edTb_caurfincp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfincp.Location = New System.Drawing.Point(284, 157)
    Me.edTb_caurfincp.Name = "edTb_caurfincp"
    Me.edTb_caurfincp.NTSDbField = ""
    Me.edTb_caurfincp.NTSFormat = "0"
    Me.edTb_caurfincp.NTSForzaVisZoom = False
    Me.edTb_caurfincp.NTSOldValue = ""
    Me.edTb_caurfincp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfincp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfincp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfincp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfincp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfincp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfincp.Properties.AutoHeight = False
    Me.edTb_caurfincp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfincp.Properties.MaxLength = 65536
    Me.edTb_caurfincp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfincp.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfincp.TabIndex = 39
    Me.edTb_caurfincp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfincp.TextInt = 0
    '
    'lbTb_caurfincp
    '
    Me.lbTb_caurfincp.AutoSize = True
    Me.lbTb_caurfincp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfincp.Location = New System.Drawing.Point(13, 160)
    Me.lbTb_caurfincp.Name = "lbTb_caurfincp"
    Me.lbTb_caurfincp.NTSDbField = ""
    Me.lbTb_caurfincp.Size = New System.Drawing.Size(211, 13)
    Me.lbTb_caurfincp.TabIndex = 116
    Me.lbTb_caurfincp.Text = "Causale R.F. totale incassata (prestazioni)"
    Me.lbTb_caurfincp.Tooltip = ""
    Me.lbTb_caurfincp.UseMnemonic = False
    '
    'lbXx_caurfriec
    '
    Me.lbXx_caurfriec.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfriec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfriec.Location = New System.Drawing.Point(359, 136)
    Me.lbXx_caurfriec.Name = "lbXx_caurfriec"
    Me.lbXx_caurfriec.NTSDbField = ""
    Me.lbXx_caurfriec.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfriec.TabIndex = 114
    Me.lbXx_caurfriec.Tooltip = ""
    Me.lbXx_caurfriec.UseMnemonic = False
    '
    'edTb_caurfriec
    '
    Me.edTb_caurfriec.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfriec.Location = New System.Drawing.Point(284, 135)
    Me.edTb_caurfriec.Name = "edTb_caurfriec"
    Me.edTb_caurfriec.NTSDbField = ""
    Me.edTb_caurfriec.NTSFormat = "0"
    Me.edTb_caurfriec.NTSForzaVisZoom = False
    Me.edTb_caurfriec.NTSOldValue = ""
    Me.edTb_caurfriec.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfriec.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfriec.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfriec.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfriec.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfriec.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfriec.Properties.AutoHeight = False
    Me.edTb_caurfriec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfriec.Properties.MaxLength = 65536
    Me.edTb_caurfriec.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfriec.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfriec.TabIndex = 38
    Me.edTb_caurfriec.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfriec.TextInt = 0
    '
    'lbTb_caurfriec
    '
    Me.lbTb_caurfriec.AutoSize = True
    Me.lbTb_caurfriec.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfriec.Location = New System.Drawing.Point(13, 138)
    Me.lbTb_caurfriec.Name = "lbTb_caurfriec"
    Me.lbTb_caurfriec.NTSDbField = ""
    Me.lbTb_caurfriec.Size = New System.Drawing.Size(172, 13)
    Me.lbTb_caurfriec.TabIndex = 113
    Me.lbTb_caurfriec.Text = "Causale R.F. riemissione (cessioni)"
    Me.lbTb_caurfriec.Tooltip = ""
    Me.lbTb_caurfriec.UseMnemonic = False
    '
    'lbXx_caurfnonc
    '
    Me.lbXx_caurfnonc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfnonc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfnonc.Location = New System.Drawing.Point(359, 113)
    Me.lbXx_caurfnonc.Name = "lbXx_caurfnonc"
    Me.lbXx_caurfnonc.NTSDbField = ""
    Me.lbXx_caurfnonc.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfnonc.TabIndex = 111
    Me.lbXx_caurfnonc.Tooltip = ""
    Me.lbXx_caurfnonc.UseMnemonic = False
    '
    'edTb_caurfnonc
    '
    Me.edTb_caurfnonc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfnonc.Location = New System.Drawing.Point(284, 113)
    Me.edTb_caurfnonc.Name = "edTb_caurfnonc"
    Me.edTb_caurfnonc.NTSDbField = ""
    Me.edTb_caurfnonc.NTSFormat = "0"
    Me.edTb_caurfnonc.NTSForzaVisZoom = False
    Me.edTb_caurfnonc.NTSOldValue = ""
    Me.edTb_caurfnonc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfnonc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfnonc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfnonc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfnonc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfnonc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfnonc.Properties.AutoHeight = False
    Me.edTb_caurfnonc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfnonc.Properties.MaxLength = 65536
    Me.edTb_caurfnonc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfnonc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfnonc.TabIndex = 37
    Me.edTb_caurfnonc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfnonc.TextInt = 0
    '
    'lbTb_caurfnonc
    '
    Me.lbTb_caurfnonc.AutoSize = True
    Me.lbTb_caurfnonc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfnonc.Location = New System.Drawing.Point(13, 116)
    Me.lbTb_caurfnonc.Name = "lbTb_caurfnonc"
    Me.lbTb_caurfnonc.NTSDbField = ""
    Me.lbTb_caurfnonc.Size = New System.Drawing.Size(186, 13)
    Me.lbTb_caurfnonc.TabIndex = 110
    Me.lbTb_caurfnonc.Text = "Causale R.F. non incassata (cessioni)"
    Me.lbTb_caurfnonc.Tooltip = ""
    Me.lbTb_caurfnonc.UseMnemonic = False
    '
    'lbXx_caurfparc
    '
    Me.lbXx_caurfparc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfparc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfparc.Location = New System.Drawing.Point(359, 91)
    Me.lbXx_caurfparc.Name = "lbXx_caurfparc"
    Me.lbXx_caurfparc.NTSDbField = ""
    Me.lbXx_caurfparc.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfparc.TabIndex = 108
    Me.lbXx_caurfparc.Tooltip = ""
    Me.lbXx_caurfparc.UseMnemonic = False
    '
    'edTb_caurfparc
    '
    Me.edTb_caurfparc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurfparc.Location = New System.Drawing.Point(284, 91)
    Me.edTb_caurfparc.Name = "edTb_caurfparc"
    Me.edTb_caurfparc.NTSDbField = ""
    Me.edTb_caurfparc.NTSFormat = "0"
    Me.edTb_caurfparc.NTSForzaVisZoom = False
    Me.edTb_caurfparc.NTSOldValue = ""
    Me.edTb_caurfparc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfparc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfparc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfparc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfparc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfparc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfparc.Properties.AutoHeight = False
    Me.edTb_caurfparc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfparc.Properties.MaxLength = 65536
    Me.edTb_caurfparc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfparc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfparc.TabIndex = 36
    Me.edTb_caurfparc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfparc.TextInt = 0
    '
    'lbTb_caurfparc
    '
    Me.lbTb_caurfparc.AutoSize = True
    Me.lbTb_caurfparc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfparc.Location = New System.Drawing.Point(13, 94)
    Me.lbTb_caurfparc.Name = "lbTb_caurfparc"
    Me.lbTb_caurfparc.NTSDbField = ""
    Me.lbTb_caurfparc.Size = New System.Drawing.Size(229, 13)
    Me.lbTb_caurfparc.TabIndex = 107
    Me.lbTb_caurfparc.Text = "Causale R.F. parzialmente incassata (cessioni)"
    Me.lbTb_caurfparc.Tooltip = ""
    Me.lbTb_caurfparc.UseMnemonic = False
    '
    'lbXx_caurfincc
    '
    Me.lbXx_caurfincc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurfincc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurfincc.Location = New System.Drawing.Point(359, 69)
    Me.lbXx_caurfincc.Name = "lbXx_caurfincc"
    Me.lbXx_caurfincc.NTSDbField = ""
    Me.lbXx_caurfincc.Size = New System.Drawing.Size(340, 20)
    Me.lbXx_caurfincc.TabIndex = 105
    Me.lbXx_caurfincc.Tooltip = ""
    Me.lbXx_caurfincc.UseMnemonic = False
    '
    'edTb_caurfincc
    '
    Me.edTb_caurfincc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_caurfincc.Location = New System.Drawing.Point(284, 69)
    Me.edTb_caurfincc.Name = "edTb_caurfincc"
    Me.edTb_caurfincc.NTSDbField = ""
    Me.edTb_caurfincc.NTSFormat = "0"
    Me.edTb_caurfincc.NTSForzaVisZoom = False
    Me.edTb_caurfincc.NTSOldValue = ""
    Me.edTb_caurfincc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurfincc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurfincc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurfincc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurfincc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurfincc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurfincc.Properties.AutoHeight = False
    Me.edTb_caurfincc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurfincc.Properties.MaxLength = 65536
    Me.edTb_caurfincc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurfincc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurfincc.TabIndex = 35
    Me.edTb_caurfincc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_caurfincc.TextInt = 0
    '
    'lbTb_caurfincc
    '
    Me.lbTb_caurfincc.AutoSize = True
    Me.lbTb_caurfincc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurfincc.Location = New System.Drawing.Point(13, 72)
    Me.lbTb_caurfincc.Name = "lbTb_caurfincc"
    Me.lbTb_caurfincc.NTSDbField = ""
    Me.lbTb_caurfincc.Size = New System.Drawing.Size(196, 13)
    Me.lbTb_caurfincc.TabIndex = 104
    Me.lbTb_caurfincc.Text = "Causale R.F. totale incassata (cessioni)"
    Me.lbTb_caurfincc.Tooltip = ""
    Me.lbTb_caurfincc.UseMnemonic = False
    '
    'lbXx_conivrfp
    '
    Me.lbXx_conivrfp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conivrfp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conivrfp.Location = New System.Drawing.Point(378, 47)
    Me.lbXx_conivrfp.Name = "lbXx_conivrfp"
    Me.lbXx_conivrfp.NTSDbField = ""
    Me.lbXx_conivrfp.Size = New System.Drawing.Size(321, 20)
    Me.lbXx_conivrfp.TabIndex = 102
    Me.lbXx_conivrfp.Tooltip = ""
    Me.lbXx_conivrfp.UseMnemonic = False
    '
    'edTb_conivrfp
    '
    Me.edTb_conivrfp.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_conivrfp.Location = New System.Drawing.Point(284, 47)
    Me.edTb_conivrfp.Name = "edTb_conivrfp"
    Me.edTb_conivrfp.NTSDbField = ""
    Me.edTb_conivrfp.NTSFormat = "0"
    Me.edTb_conivrfp.NTSForzaVisZoom = False
    Me.edTb_conivrfp.NTSOldValue = ""
    Me.edTb_conivrfp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conivrfp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conivrfp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conivrfp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conivrfp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conivrfp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conivrfp.Properties.AutoHeight = False
    Me.edTb_conivrfp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conivrfp.Properties.MaxLength = 65536
    Me.edTb_conivrfp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conivrfp.Size = New System.Drawing.Size(88, 20)
    Me.edTb_conivrfp.TabIndex = 34
    Me.edTb_conivrfp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_conivrfp.TextInt = 0
    '
    'lbTb_conivrfp
    '
    Me.lbTb_conivrfp.AutoSize = True
    Me.lbTb_conivrfp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conivrfp.Location = New System.Drawing.Point(13, 50)
    Me.lbTb_conivrfp.Name = "lbTb_conivrfp"
    Me.lbTb_conivrfp.NTSDbField = ""
    Me.lbTb_conivrfp.Size = New System.Drawing.Size(250, 13)
    Me.lbTb_conivrfp.TabIndex = 101
    Me.lbTb_conivrfp.Text = "Sottoconto Iva da R.F. non incassata (prestazioni)"
    Me.lbTb_conivrfp.Tooltip = ""
    Me.lbTb_conivrfp.UseMnemonic = False
    '
    'lbXx_concrrfp
    '
    Me.lbXx_concrrfp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_concrrfp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_concrrfp.Location = New System.Drawing.Point(378, 25)
    Me.lbXx_concrrfp.Name = "lbXx_concrrfp"
    Me.lbXx_concrrfp.NTSDbField = ""
    Me.lbXx_concrrfp.Size = New System.Drawing.Size(321, 20)
    Me.lbXx_concrrfp.TabIndex = 99
    Me.lbXx_concrrfp.Tooltip = ""
    Me.lbXx_concrrfp.UseMnemonic = False
    '
    'edTb_concrrfp
    '
    Me.edTb_concrrfp.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_concrrfp.Location = New System.Drawing.Point(284, 25)
    Me.edTb_concrrfp.Name = "edTb_concrrfp"
    Me.edTb_concrrfp.NTSDbField = ""
    Me.edTb_concrrfp.NTSFormat = "0"
    Me.edTb_concrrfp.NTSForzaVisZoom = False
    Me.edTb_concrrfp.NTSOldValue = ""
    Me.edTb_concrrfp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_concrrfp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_concrrfp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_concrrfp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_concrrfp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_concrrfp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_concrrfp.Properties.AutoHeight = False
    Me.edTb_concrrfp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_concrrfp.Properties.MaxLength = 65536
    Me.edTb_concrrfp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_concrrfp.Size = New System.Drawing.Size(88, 20)
    Me.edTb_concrrfp.TabIndex = 33
    Me.edTb_concrrfp.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_concrrfp.TextInt = 0
    '
    'lbTb_concrrfp
    '
    Me.lbTb_concrrfp.AutoSize = True
    Me.lbTb_concrrfp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_concrrfp.Location = New System.Drawing.Point(13, 28)
    Me.lbTb_concrrfp.Name = "lbTb_concrrfp"
    Me.lbTb_concrrfp.NTSDbField = ""
    Me.lbTb_concrrfp.Size = New System.Drawing.Size(263, 13)
    Me.lbTb_concrrfp.TabIndex = 98
    Me.lbTb_concrrfp.Text = "Sottoconto crediti da R.F. non incassata (prestazioni)"
    Me.lbTb_concrrfp.Tooltip = ""
    Me.lbTb_concrrfp.UseMnemonic = False
    '
    'lbXx_concrrfc
    '
    Me.lbXx_concrrfc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_concrrfc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_concrrfc.Location = New System.Drawing.Point(378, 3)
    Me.lbXx_concrrfc.Name = "lbXx_concrrfc"
    Me.lbXx_concrrfc.NTSDbField = ""
    Me.lbXx_concrrfc.Size = New System.Drawing.Size(321, 20)
    Me.lbXx_concrrfc.TabIndex = 96
    Me.lbXx_concrrfc.Tooltip = ""
    Me.lbXx_concrrfc.UseMnemonic = False
    '
    'edTb_concrrfc
    '
    Me.edTb_concrrfc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_concrrfc.Location = New System.Drawing.Point(284, 3)
    Me.edTb_concrrfc.Name = "edTb_concrrfc"
    Me.edTb_concrrfc.NTSDbField = ""
    Me.edTb_concrrfc.NTSFormat = "0"
    Me.edTb_concrrfc.NTSForzaVisZoom = False
    Me.edTb_concrrfc.NTSOldValue = ""
    Me.edTb_concrrfc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_concrrfc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_concrrfc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_concrrfc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_concrrfc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_concrrfc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_concrrfc.Properties.AutoHeight = False
    Me.edTb_concrrfc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_concrrfc.Properties.MaxLength = 65536
    Me.edTb_concrrfc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_concrrfc.Size = New System.Drawing.Size(88, 20)
    Me.edTb_concrrfc.TabIndex = 32
    Me.edTb_concrrfc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_concrrfc.TextInt = 0
    '
    'lbTb_concrrfc
    '
    Me.lbTb_concrrfc.AutoSize = True
    Me.lbTb_concrrfc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_concrrfc.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_concrrfc.Name = "lbTb_concrrfc"
    Me.lbTb_concrrfc.NTSDbField = ""
    Me.lbTb_concrrfc.Size = New System.Drawing.Size(248, 13)
    Me.lbTb_concrrfc.TabIndex = 95
    Me.lbTb_concrrfc.Text = "Sottoconto crediti da R.F. non incassata (cessioni)"
    Me.lbTb_concrrfc.Tooltip = ""
    Me.lbTb_concrrfc.UseMnemonic = False
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.NtsPanel1)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(715, 279)
    Me.NtsTabPage5.Text = "Pagina &5"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkq)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkq)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkq)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkr)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkr)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkr)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrks)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrks)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrks)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkn)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkn)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkn)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkf)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrki)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrki)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrki)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkf)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkf)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrke)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrke)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrke)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkc)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkc)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkc)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkb)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkb)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkb)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrka)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrka)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrka)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(1, 0)
    Me.NtsPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(712, 282)
    Me.NtsPanel1.TabIndex = 2
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'lbXx_tipobfrkq
    '
    Me.lbXx_tipobfrkq.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkq.Location = New System.Drawing.Point(311, 210)
    Me.lbXx_tipobfrkq.Name = "lbXx_tipobfrkq"
    Me.lbXx_tipobfrkq.NTSDbField = ""
    Me.lbXx_tipobfrkq.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkq.TabIndex = 126
    Me.lbXx_tipobfrkq.Tooltip = ""
    Me.lbXx_tipobfrkq.UseMnemonic = False
    '
    'edTb_tipobfrkq
    '
    Me.edTb_tipobfrkq.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkq.Location = New System.Drawing.Point(236, 210)
    Me.edTb_tipobfrkq.Name = "edTb_tipobfrkq"
    Me.edTb_tipobfrkq.NTSDbField = ""
    Me.edTb_tipobfrkq.NTSFormat = "0"
    Me.edTb_tipobfrkq.NTSForzaVisZoom = False
    Me.edTb_tipobfrkq.NTSOldValue = ""
    Me.edTb_tipobfrkq.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkq.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkq.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkq.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkq.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkq.Properties.AutoHeight = False
    Me.edTb_tipobfrkq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkq.Properties.MaxLength = 65536
    Me.edTb_tipobfrkq.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkq.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkq.TabIndex = 113
    Me.edTb_tipobfrkq.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkq.TextInt = 0
    '
    'lbTb_tipobfrkq
    '
    Me.lbTb_tipobfrkq.AutoSize = True
    Me.lbTb_tipobfrkq.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkq.Location = New System.Drawing.Point(13, 213)
    Me.lbTb_tipobfrkq.Name = "lbTb_tipobfrkq"
    Me.lbTb_tipobfrkq.NTSDbField = ""
    Me.lbTb_tipobfrkq.Size = New System.Drawing.Size(170, 13)
    Me.lbTb_tipobfrkq.TabIndex = 123
    Me.lbTb_tipobfrkq.Text = "Tipo bolla/fattura per PREVENTIVI"
    Me.lbTb_tipobfrkq.Tooltip = ""
    Me.lbTb_tipobfrkq.UseMnemonic = False
    '
    'lbXx_tipobfrkr
    '
    Me.lbXx_tipobfrkr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkr.Location = New System.Drawing.Point(311, 187)
    Me.lbXx_tipobfrkr.Name = "lbXx_tipobfrkr"
    Me.lbXx_tipobfrkr.NTSDbField = ""
    Me.lbXx_tipobfrkr.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkr.TabIndex = 120
    Me.lbXx_tipobfrkr.Tooltip = ""
    Me.lbXx_tipobfrkr.UseMnemonic = False
    '
    'edTb_tipobfrkr
    '
    Me.edTb_tipobfrkr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkr.Location = New System.Drawing.Point(236, 187)
    Me.edTb_tipobfrkr.Name = "edTb_tipobfrkr"
    Me.edTb_tipobfrkr.NTSDbField = ""
    Me.edTb_tipobfrkr.NTSFormat = "0"
    Me.edTb_tipobfrkr.NTSForzaVisZoom = False
    Me.edTb_tipobfrkr.NTSOldValue = ""
    Me.edTb_tipobfrkr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkr.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkr.Properties.AutoHeight = False
    Me.edTb_tipobfrkr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkr.Properties.MaxLength = 65536
    Me.edTb_tipobfrkr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkr.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkr.TabIndex = 111
    Me.edTb_tipobfrkr.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkr.TextInt = 0
    '
    'lbTb_tipobfrkr
    '
    Me.lbTb_tipobfrkr.AutoSize = True
    Me.lbTb_tipobfrkr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkr.Location = New System.Drawing.Point(13, 190)
    Me.lbTb_tipobfrkr.Name = "lbTb_tipobfrkr"
    Me.lbTb_tipobfrkr.NTSDbField = ""
    Me.lbTb_tipobfrkr.Size = New System.Drawing.Size(221, 13)
    Me.lbTb_tipobfrkr.TabIndex = 119
    Me.lbTb_tipobfrkr.Text = "Tipo bolla/fattura per IMP. CL./ IMP. CL. AP."
    Me.lbTb_tipobfrkr.Tooltip = ""
    Me.lbTb_tipobfrkr.UseMnemonic = False
    '
    'lbXx_tipobfrks
    '
    Me.lbXx_tipobfrks.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrks.Location = New System.Drawing.Point(311, 164)
    Me.lbXx_tipobfrks.Name = "lbXx_tipobfrks"
    Me.lbXx_tipobfrks.NTSDbField = ""
    Me.lbXx_tipobfrks.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrks.TabIndex = 118
    Me.lbXx_tipobfrks.Tooltip = ""
    Me.lbXx_tipobfrks.UseMnemonic = False
    '
    'edTb_tipobfrks
    '
    Me.edTb_tipobfrks.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrks.Location = New System.Drawing.Point(236, 164)
    Me.edTb_tipobfrks.Name = "edTb_tipobfrks"
    Me.edTb_tipobfrks.NTSDbField = ""
    Me.edTb_tipobfrks.NTSFormat = "0"
    Me.edTb_tipobfrks.NTSForzaVisZoom = False
    Me.edTb_tipobfrks.NTSOldValue = ""
    Me.edTb_tipobfrks.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrks.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrks.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrks.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrks.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrks.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrks.Properties.AutoHeight = False
    Me.edTb_tipobfrks.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrks.Properties.MaxLength = 65536
    Me.edTb_tipobfrks.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrks.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrks.TabIndex = 110
    Me.edTb_tipobfrks.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrks.TextInt = 0
    '
    'lbTb_tipobfrks
    '
    Me.lbTb_tipobfrks.AutoSize = True
    Me.lbTb_tipobfrks.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrks.Location = New System.Drawing.Point(13, 167)
    Me.lbTb_tipobfrks.Name = "lbTb_tipobfrks"
    Me.lbTb_tipobfrks.NTSDbField = ""
    Me.lbTb_tipobfrks.Size = New System.Drawing.Size(218, 13)
    Me.lbTb_tipobfrks.TabIndex = 117
    Me.lbTb_tipobfrks.Text = "Tipo bolla/fattura per FATT./RIC. FISC. EM."
    Me.lbTb_tipobfrks.Tooltip = ""
    Me.lbTb_tipobfrks.UseMnemonic = False
    '
    'lbXx_tipobfrkn
    '
    Me.lbXx_tipobfrkn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkn.Location = New System.Drawing.Point(311, 141)
    Me.lbXx_tipobfrkn.Name = "lbXx_tipobfrkn"
    Me.lbXx_tipobfrkn.NTSDbField = ""
    Me.lbXx_tipobfrkn.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkn.TabIndex = 116
    Me.lbXx_tipobfrkn.Tooltip = ""
    Me.lbXx_tipobfrkn.UseMnemonic = False
    '
    'edTb_tipobfrkn
    '
    Me.edTb_tipobfrkn.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_tipobfrkn.Location = New System.Drawing.Point(236, 141)
    Me.edTb_tipobfrkn.Name = "edTb_tipobfrkn"
    Me.edTb_tipobfrkn.NTSDbField = ""
    Me.edTb_tipobfrkn.NTSFormat = "0"
    Me.edTb_tipobfrkn.NTSForzaVisZoom = False
    Me.edTb_tipobfrkn.NTSOldValue = ""
    Me.edTb_tipobfrkn.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkn.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkn.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkn.Properties.AutoHeight = False
    Me.edTb_tipobfrkn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkn.Properties.MaxLength = 65536
    Me.edTb_tipobfrkn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkn.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkn.TabIndex = 109
    Me.edTb_tipobfrkn.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkn.TextInt = 0
    '
    'lbTb_tipobfrkn
    '
    Me.lbTb_tipobfrkn.AutoSize = True
    Me.lbTb_tipobfrkn.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkn.Location = New System.Drawing.Point(13, 144)
    Me.lbTb_tipobfrkn.Name = "lbTb_tipobfrkn"
    Me.lbTb_tipobfrkn.NTSDbField = ""
    Me.lbTb_tipobfrkn.Size = New System.Drawing.Size(224, 13)
    Me.lbTb_tipobfrkn.TabIndex = 115
    Me.lbTb_tipobfrkn.Text = "Tipo bolla/fattura per NOTE ACCRED EMESSE"
    Me.lbTb_tipobfrkn.Tooltip = ""
    Me.lbTb_tipobfrkn.UseMnemonic = False
    '
    'lbXx_tipobfrkf
    '
    Me.lbXx_tipobfrkf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkf.Location = New System.Drawing.Point(311, 95)
    Me.lbXx_tipobfrkf.Name = "lbXx_tipobfrkf"
    Me.lbXx_tipobfrkf.NTSDbField = ""
    Me.lbXx_tipobfrkf.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkf.TabIndex = 108
    Me.lbXx_tipobfrkf.Tooltip = ""
    Me.lbXx_tipobfrkf.UseMnemonic = False
    '
    'lbXx_tipobfrki
    '
    Me.lbXx_tipobfrki.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrki.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrki.Location = New System.Drawing.Point(311, 118)
    Me.lbXx_tipobfrki.Name = "lbXx_tipobfrki"
    Me.lbXx_tipobfrki.NTSDbField = ""
    Me.lbXx_tipobfrki.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrki.TabIndex = 107
    Me.lbXx_tipobfrki.Tooltip = ""
    Me.lbXx_tipobfrki.UseMnemonic = False
    '
    'edTb_tipobfrki
    '
    Me.edTb_tipobfrki.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrki.Location = New System.Drawing.Point(236, 118)
    Me.edTb_tipobfrki.Name = "edTb_tipobfrki"
    Me.edTb_tipobfrki.NTSDbField = ""
    Me.edTb_tipobfrki.NTSFormat = "0"
    Me.edTb_tipobfrki.NTSForzaVisZoom = False
    Me.edTb_tipobfrki.NTSOldValue = ""
    Me.edTb_tipobfrki.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrki.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrki.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrki.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrki.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrki.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrki.Properties.AutoHeight = False
    Me.edTb_tipobfrki.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrki.Properties.MaxLength = 65536
    Me.edTb_tipobfrki.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrki.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrki.TabIndex = 26
    Me.edTb_tipobfrki.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrki.TextInt = 0
    '
    'lbTb_tipobfrki
    '
    Me.lbTb_tipobfrki.AutoSize = True
    Me.lbTb_tipobfrki.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrki.Location = New System.Drawing.Point(13, 121)
    Me.lbTb_tipobfrki.Name = "lbTb_tipobfrki"
    Me.lbTb_tipobfrki.NTSDbField = ""
    Me.lbTb_tipobfrki.Size = New System.Drawing.Size(217, 13)
    Me.lbTb_tipobfrki.TabIndex = 106
    Me.lbTb_tipobfrki.Text = "Tipo bolla/fattura per RIEM. RIC. FISC. EM."
    Me.lbTb_tipobfrki.Tooltip = ""
    Me.lbTb_tipobfrki.UseMnemonic = False
    '
    'edTb_tipobfrkf
    '
    Me.edTb_tipobfrkf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkf.Location = New System.Drawing.Point(236, 95)
    Me.edTb_tipobfrkf.Name = "edTb_tipobfrkf"
    Me.edTb_tipobfrkf.NTSDbField = ""
    Me.edTb_tipobfrkf.NTSFormat = "0"
    Me.edTb_tipobfrkf.NTSForzaVisZoom = False
    Me.edTb_tipobfrkf.NTSOldValue = ""
    Me.edTb_tipobfrkf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkf.Properties.AutoHeight = False
    Me.edTb_tipobfrkf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkf.Properties.MaxLength = 65536
    Me.edTb_tipobfrkf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkf.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkf.TabIndex = 25
    Me.edTb_tipobfrkf.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkf.TextInt = 0
    '
    'lbTb_tipobfrkf
    '
    Me.lbTb_tipobfrkf.AutoSize = True
    Me.lbTb_tipobfrkf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkf.Location = New System.Drawing.Point(13, 98)
    Me.lbTb_tipobfrkf.Name = "lbTb_tipobfrkf"
    Me.lbTb_tipobfrkf.NTSDbField = ""
    Me.lbTb_tipobfrkf.Size = New System.Drawing.Size(205, 13)
    Me.lbTb_tipobfrkf.TabIndex = 104
    Me.lbTb_tipobfrkf.Text = "Tipo bolla/fattura per RIC. FISC. EMESSE"
    Me.lbTb_tipobfrkf.Tooltip = ""
    Me.lbTb_tipobfrkf.UseMnemonic = False
    '
    'lbXx_tipobfrke
    '
    Me.lbXx_tipobfrke.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrke.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrke.Location = New System.Drawing.Point(311, 72)
    Me.lbXx_tipobfrke.Name = "lbXx_tipobfrke"
    Me.lbXx_tipobfrke.NTSDbField = ""
    Me.lbXx_tipobfrke.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrke.TabIndex = 102
    Me.lbXx_tipobfrke.Tooltip = ""
    Me.lbXx_tipobfrke.UseMnemonic = False
    '
    'edTb_tipobfrke
    '
    Me.edTb_tipobfrke.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrke.Location = New System.Drawing.Point(236, 72)
    Me.edTb_tipobfrke.Name = "edTb_tipobfrke"
    Me.edTb_tipobfrke.NTSDbField = ""
    Me.edTb_tipobfrke.NTSFormat = "0"
    Me.edTb_tipobfrke.NTSForzaVisZoom = False
    Me.edTb_tipobfrke.NTSOldValue = ""
    Me.edTb_tipobfrke.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrke.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrke.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrke.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrke.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrke.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrke.Properties.AutoHeight = False
    Me.edTb_tipobfrke.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrke.Properties.MaxLength = 65536
    Me.edTb_tipobfrke.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrke.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrke.TabIndex = 24
    Me.edTb_tipobfrke.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrke.TextInt = 0
    '
    'lbTb_tipobfrke
    '
    Me.lbTb_tipobfrke.AutoSize = True
    Me.lbTb_tipobfrke.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrke.Location = New System.Drawing.Point(13, 75)
    Me.lbTb_tipobfrke.Name = "lbTb_tipobfrke"
    Me.lbTb_tipobfrke.NTSDbField = ""
    Me.lbTb_tipobfrke.Size = New System.Drawing.Size(220, 13)
    Me.lbTb_tipobfrke.TabIndex = 101
    Me.lbTb_tipobfrke.Text = "Tipo bolla/fattura per NOTE ADDEB. EMESSE"
    Me.lbTb_tipobfrke.Tooltip = ""
    Me.lbTb_tipobfrke.UseMnemonic = False
    '
    'lbXx_tipobfrkc
    '
    Me.lbXx_tipobfrkc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkc.Location = New System.Drawing.Point(311, 49)
    Me.lbXx_tipobfrkc.Name = "lbXx_tipobfrkc"
    Me.lbXx_tipobfrkc.NTSDbField = ""
    Me.lbXx_tipobfrkc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkc.TabIndex = 99
    Me.lbXx_tipobfrkc.Tooltip = ""
    Me.lbXx_tipobfrkc.UseMnemonic = False
    '
    'edTb_tipobfrkc
    '
    Me.edTb_tipobfrkc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkc.Location = New System.Drawing.Point(236, 49)
    Me.edTb_tipobfrkc.Name = "edTb_tipobfrkc"
    Me.edTb_tipobfrkc.NTSDbField = ""
    Me.edTb_tipobfrkc.NTSFormat = "0"
    Me.edTb_tipobfrkc.NTSForzaVisZoom = False
    Me.edTb_tipobfrkc.NTSOldValue = ""
    Me.edTb_tipobfrkc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkc.Properties.AutoHeight = False
    Me.edTb_tipobfrkc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkc.Properties.MaxLength = 65536
    Me.edTb_tipobfrkc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkc.TabIndex = 23
    Me.edTb_tipobfrkc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkc.TextInt = 0
    '
    'lbTb_tipobfrkc
    '
    Me.lbTb_tipobfrkc.AutoSize = True
    Me.lbTb_tipobfrkc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkc.Location = New System.Drawing.Point(13, 52)
    Me.lbTb_tipobfrkc.Name = "lbTb_tipobfrkc"
    Me.lbTb_tipobfrkc.NTSDbField = ""
    Me.lbTb_tipobfrkc.Size = New System.Drawing.Size(218, 13)
    Me.lbTb_tipobfrkc.TabIndex = 98
    Me.lbTb_tipobfrkc.Text = "Tipo bolla/fattura per CORRISPETT. EMESSI"
    Me.lbTb_tipobfrkc.Tooltip = ""
    Me.lbTb_tipobfrkc.UseMnemonic = False
    '
    'lbXx_tipobfrkb
    '
    Me.lbXx_tipobfrkb.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkb.Location = New System.Drawing.Point(311, 26)
    Me.lbXx_tipobfrkb.Name = "lbXx_tipobfrkb"
    Me.lbXx_tipobfrkb.NTSDbField = ""
    Me.lbXx_tipobfrkb.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkb.TabIndex = 96
    Me.lbXx_tipobfrkb.Tooltip = ""
    Me.lbXx_tipobfrkb.UseMnemonic = False
    '
    'edTb_tipobfrkb
    '
    Me.edTb_tipobfrkb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkb.Location = New System.Drawing.Point(236, 26)
    Me.edTb_tipobfrkb.Name = "edTb_tipobfrkb"
    Me.edTb_tipobfrkb.NTSDbField = ""
    Me.edTb_tipobfrkb.NTSFormat = "0"
    Me.edTb_tipobfrkb.NTSForzaVisZoom = False
    Me.edTb_tipobfrkb.NTSOldValue = ""
    Me.edTb_tipobfrkb.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkb.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkb.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkb.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkb.Properties.AutoHeight = False
    Me.edTb_tipobfrkb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkb.Properties.MaxLength = 65536
    Me.edTb_tipobfrkb.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkb.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkb.TabIndex = 22
    Me.edTb_tipobfrkb.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrkb.TextInt = 0
    '
    'lbTb_tipobfrkb
    '
    Me.lbTb_tipobfrkb.AutoSize = True
    Me.lbTb_tipobfrkb.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkb.Location = New System.Drawing.Point(13, 29)
    Me.lbTb_tipobfrkb.Name = "lbTb_tipobfrkb"
    Me.lbTb_tipobfrkb.NTSDbField = ""
    Me.lbTb_tipobfrkb.Size = New System.Drawing.Size(171, 13)
    Me.lbTb_tipobfrkb.TabIndex = 95
    Me.lbTb_tipobfrkb.Text = "Tipo bolla/fattura per DDT EMESSI"
    Me.lbTb_tipobfrkb.Tooltip = ""
    Me.lbTb_tipobfrkb.UseMnemonic = False
    '
    'lbXx_tipobfrka
    '
    Me.lbXx_tipobfrka.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrka.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrka.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_tipobfrka.Name = "lbXx_tipobfrka"
    Me.lbXx_tipobfrka.NTSDbField = ""
    Me.lbXx_tipobfrka.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrka.TabIndex = 93
    Me.lbXx_tipobfrka.Tooltip = ""
    Me.lbXx_tipobfrka.UseMnemonic = False
    '
    'edTb_tipobfrka
    '
    Me.edTb_tipobfrka.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrka.Location = New System.Drawing.Point(236, 3)
    Me.edTb_tipobfrka.Name = "edTb_tipobfrka"
    Me.edTb_tipobfrka.NTSDbField = ""
    Me.edTb_tipobfrka.NTSFormat = "0"
    Me.edTb_tipobfrka.NTSForzaVisZoom = False
    Me.edTb_tipobfrka.NTSOldValue = ""
    Me.edTb_tipobfrka.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrka.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrka.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrka.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrka.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrka.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrka.Properties.AutoHeight = False
    Me.edTb_tipobfrka.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrka.Properties.MaxLength = 65536
    Me.edTb_tipobfrka.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrka.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrka.TabIndex = 21
    Me.edTb_tipobfrka.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edTb_tipobfrka.TextInt = 0
    '
    'lbTb_tipobfrka
    '
    Me.lbTb_tipobfrka.AutoSize = True
    Me.lbTb_tipobfrka.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrka.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_tipobfrka.Name = "lbTb_tipobfrka"
    Me.lbTb_tipobfrka.NTSDbField = ""
    Me.lbTb_tipobfrka.Size = New System.Drawing.Size(222, 13)
    Me.lbTb_tipobfrka.TabIndex = 92
    Me.lbTb_tipobfrka.Text = "Tipo bolla/fattura per FATT. IMMED. EMESSE"
    Me.lbTb_tipobfrka.Tooltip = ""
    Me.lbTb_tipobfrka.UseMnemonic = False
    '
    'edXx_vlistin
    '
    Me.edXx_vlistin.BackColor = System.Drawing.Color.Transparent
    Me.edXx_vlistin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edXx_vlistin.Location = New System.Drawing.Point(311, 91)
    Me.edXx_vlistin.Name = "edXx_vlistin"
    Me.edXx_vlistin.NTSDbField = ""
    Me.edXx_vlistin.Size = New System.Drawing.Size(386, 20)
    Me.edXx_vlistin.TabIndex = 132
    Me.edXx_vlistin.Tooltip = ""
    Me.edXx_vlistin.UseMnemonic = False
    '
    'FRMVEPEVE_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(736, 341)
    Me.Controls.Add(Me.tabPeve)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMVEPEVE_F"
    Me.Text = "PERSONALIZZAZIONE VENDITE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tabPeve, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabPeve.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPeve1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeve1.ResumeLayout(False)
    Me.pnPeve1.PerformLayout()
    CType(Me.edTb_ivainc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datfinivainc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ivainc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datfinivainc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caustib.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ivainc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_iva15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caustom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caucorr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cauinc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caunoaf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caunoac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cauemfs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cauemft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPeve2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeve2.ResumeLayout(False)
    Me.pnPeve2.PerformLayout()
    CType(Me.edTb_contimba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conclpriv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_concauz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conriac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conriin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conribo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conomag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conabpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conabat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_concas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPeve3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeve3.ResumeLayout(False)
    Me.pnPeve3.PerformLayout()
    CType(Me.fraFrame1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fraFrame1.ResumeLayout(False)
    Me.fraFrame1.PerformLayout()
    CType(Me.ckTb_vdatora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_vscorpo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vacurad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vcautra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vlistin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vcaumag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vmagazz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vcontro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vtipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPeve4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeve4.ResumeLayout(False)
    Me.pnPeve4.PerformLayout()
    CType(Me.edTb_caurfriep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfnonp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfparp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfincp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfriec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfnonc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfparc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurfincc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conivrfp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_concrrfp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_concrrfc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edTb_tipobfrkq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrki.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrke.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrka.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      edTb_cauemft.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345138566, "Causale emissione Fatture"), tabcauc)
      edTb_cauemfs.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345294739, "Causale emissione Fatture sospensione"), tabcauc)
      edTb_caunoac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345450912, "Causale Note di accredito"), tabcauc)
      edTb_caunoaf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345607085, "Causale Note di accredito sospensione"), tabcauc)
      edTb_cauinc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345763258, "Causale Incasso"), tabcauc)
      edTb_caucorr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023345919431, "Causale emissione Corrispettivi"), tabcauc)
      edTb_caustom.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346075604, "Causale storno Omaggi"), tabcauc)
      edTb_iva15.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346231777, "Codice Iva escluso articolo 15"), tabciva)
      edTb_ivainc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346387950, "Codice Iva spese incasso"), tabciva)
      edTb_ivainc2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129636658842314453, "Codice Iva spese incasso 2"), tabciva)
      edTb_ivainc3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129636658856796875, "Codice Iva spese incasso 3"), tabciva)
      edTb_datfinivainc3.NTSSetParam(oMenu, oApp.Tr(Me, 129636659026552734, "Data fine validità Codice Iva spese incasso 3"), False)
      edTb_datfinivainc2.NTSSetParam(oMenu, oApp.Tr(Me, 129636659253115234, "Data fine validità Codice Iva spese incasso 2"), False)
      edTb_caustib.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129205481170039063, "Causale storno incassi su DDT emessi"), tabcauc)
      '-------------------------------------------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------------------------------------------
      edTb_concas.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346544123, "Conto contropartita Cassa"), tabanagras)
      edTb_conabat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346700296, "Conto contropartita Abbuoni attivi"), tabanagras)
      edTb_conabpa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023346856469, "Conto contropartita Abbuoni passivi"), tabanagras)
      edTb_conomag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347012642, "Conto contropartita Omaggi (costo)"), tabanagras)
      edTb_conribo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347168815, "Conto contropartita Ricavi da bolli"), tabanagras)
      edTb_conriin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347324988, "Conto contropartita Ricavi spese incasso"), tabanagras)
      edTb_conriac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347481161, "Conto contropartita Ricavi spese accessorie"), tabanagras)
      edTb_concauz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347637334, "Conto contropartita Cauzioni vuoti"), tabanagras)
      edTb_conclpriv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347793507, "Codice conto Cliente privato"), tabanagrac)
      edTb_contimba.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023347949680, "Conto contropartita Spese imballi"), tabanagras)
      '-------------------------------------------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------------------------------------------
      edTb_vtipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348105853, "Tipo bolla/fattura testata bolla"), tabtpbf)
      edTb_vcontro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348262026, "Codice contropartita testata bolla"), tabcove)
      edTb_vmagazz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348418199, "Codice magazzino testata bolla"), tabmaga)
      edTb_vcaumag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348574372, "Codice causale magazzino testata bolla"), tabcaum)
      edTb_vlistin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348730545, "Listini testata bolla"), tablist)
      edTb_vcautra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023348886718, "Codice causale trasporto piede bolla"), tabcaum)
      cbTb_vacurad.NTSSetParam(oApp.Tr(Me, 128230023349042891, "Trasporto a cura del (piede bolla)"))
      ckTb_vscorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023349199064, "Scorporo in testata bolla"), "S", "N")
      ckTb_vdatora.NTSSetParam(oMenu, oApp.Tr(Me, 128230023349355237, "Proposta data/ora inizio trasporto"), "S", "N")
      '-------------------------------------------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------------------------------------------
      edTb_concrrfc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023349823756, "Sottoconto crediti da R.F. non incassata (cessioni)"), tabanagras)
      edTb_concrrfp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023349979929, "Sottoconto crediti da R.F. non incassata (prestazioni)"), tabanagras)
      edTb_conivrfp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350136102, "Sottoconto Iva da R.F. non incassata (prestazioni)"), tabanagras)
      edTb_caurfincc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350292275, "Causale R.F. totale incassata (cessioni)"), tabcauc)
      edTb_caurfparc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350448448, "Causale R.F. parzialmente incassata (cessioni)"), tabcauc)
      edTb_caurfnonc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350604621, "Causale R.F. non incassata (cessioni)"), tabcauc)
      edTb_caurfriec.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350760794, "Causale R.F. riemissione (cessioni)"), tabcauc)
      edTb_caurfincp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023350916967, "Causale R.F. totale incassata (prestazioni)"), tabcauc)
      edTb_caurfparp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023351073140, "Causale R.F. parzialmente incassata (prestazioni)"), tabcauc)
      edTb_caurfnonp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023351229313, "Causale R.F. non incassata (prestazioni)"), tabcauc)
      edTb_caurfriep.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023351385486, "Causale R.F. riemissione (prestazioni)"), tabcauc)
      '-------------------------------------------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------------------------------------------
      edTb_tipobfrka.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231267136193, "Tipo bolla/fattura per FATTURE IMMEDIATE EMESSE"), tabtpbf)
      edTb_tipobfrkb.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231302449371, "Tipo bolla/fattura per DDT EMESSI"), tabtpbf)
      edTb_tipobfrkc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231325887321, "Tipo bolla/fattura per CORRISPETTIVI EMESSI"), tabtpbf)
      edTb_tipobfrke.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231350419042, "Tipo bolla/fattura per NOTE ADDEBITO EMESSE"), tabtpbf)
      edTb_tipobfrkf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231369638161, "Tipo bolla/fattura per RICEVUTE FISCALI EMESSE"), tabtpbf)
      edTb_tipobfrki.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231391044822, "Tipo bolla/fattura per RIEM. RICEVUTE FISCALI EMESSE"), tabtpbf)
      edTb_tipobfrkn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231411357712, "Tipo bolla/fattura per NOTE ACCRED EMESSE"), tabtpbf)
      edTb_tipobfrks.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231436201939, "Tipo bolla/fattura per FATTURE/RIC FISC EMESSE"), tabtpbf)
      edTb_tipobfrkr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231457764853, "Tipo bolla/fattura per IMPEGNI CLIENTI/IMP CLIENTI APERTI"), tabtpbf)
      edTb_tipobfrkq.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185231497921874, "Tipo bolla/fattura per PREVENTIVI"), tabtpbf)

      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      edTb_cauemft.NTSForzaVisZoom = True
      edTb_cauemfs.NTSForzaVisZoom = True
      edTb_caunoac.NTSForzaVisZoom = True
      edTb_caunoaf.NTSForzaVisZoom = True
      edTb_cauinc.NTSForzaVisZoom = True
      edTb_caucorr.NTSForzaVisZoom = True
      edTb_caustom.NTSForzaVisZoom = True
      edTb_iva15.NTSForzaVisZoom = True
      edTb_ivainc.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------------------------------------------
      edTb_concas.NTSForzaVisZoom = True
      edTb_conabat.NTSForzaVisZoom = True
      edTb_conabpa.NTSForzaVisZoom = True
      edTb_conomag.NTSForzaVisZoom = True
      edTb_conribo.NTSForzaVisZoom = True
      edTb_conriin.NTSForzaVisZoom = True
      edTb_conriac.NTSForzaVisZoom = True
      edTb_concauz.NTSForzaVisZoom = True
      edTb_conclpriv.NTSForzaVisZoom = True
      edTb_contimba.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------------------------------------------
      edTb_vtipobf.NTSForzaVisZoom = True
      edTb_vcontro.NTSForzaVisZoom = True
      edTb_vmagazz.NTSForzaVisZoom = True
      edTb_vcaumag.NTSForzaVisZoom = True
      edTb_vcautra.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------------------------------------------
      edTb_concrrfc.NTSForzaVisZoom = True
      edTb_concrrfp.NTSForzaVisZoom = True
      edTb_conivrfp.NTSForzaVisZoom = True
      edTb_caurfincc.NTSForzaVisZoom = True
      edTb_caurfparc.NTSForzaVisZoom = True
      edTb_caurfnonc.NTSForzaVisZoom = True
      edTb_caurfriec.NTSForzaVisZoom = True
      edTb_caurfincp.NTSForzaVisZoom = True
      edTb_caurfparp.NTSForzaVisZoom = True
      edTb_caurfnonp.NTSForzaVisZoom = True
      edTb_caurfriep.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------------------------------------------
      edTb_tipobfrka.NTSForzaVisZoom = True
      edTb_tipobfrkb.NTSForzaVisZoom = True
      edTb_tipobfrkc.NTSForzaVisZoom = True
      edTb_tipobfrke.NTSForzaVisZoom = True
      edTb_tipobfrkf.NTSForzaVisZoom = True
      edTb_tipobfrki.NTSForzaVisZoom = True
      edTb_tipobfrkn.NTSForzaVisZoom = True
      edTb_tipobfrks.NTSForzaVisZoom = True
      edTb_tipobfrkr.NTSForzaVisZoom = True
      edTb_tipobfrkq.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      '-------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------
      edTb_cauemft.NTSDbField = "tabpeve.tb_cauemft"
      lbXx_cauemft.NTSDbField = "tabpeve.xx_cauemft"
      edTb_cauemfs.NTSDbField = "tabpeve.tb_cauemfs"
      lbXx_cauemfs.NTSDbField = "tabpeve.xx_cauemfs"
      edTb_caunoac.NTSDbField = "tabpeve.tb_caunoac"
      lbXx_caunoac.NTSDbField = "tabpeve.xx_caunoac"
      edTb_caunoaf.NTSDbField = "tabpeve.tb_caunoaf"
      lbXx_caunoaf.NTSDbField = "tabpeve.xx_caunoaf"
      edTb_cauinc.NTSDbField = "tabpeve.tb_cauinc"
      lbXx_cauinc.NTSDbField = "tabpeve.xx_cauinc"
      edTb_caucorr.NTSDbField = "tabpeve.tb_caucorr"
      lbXx_caucorr.NTSDbField = "tabpeve.xx_caucorr"
      edTb_caustom.NTSDbField = "tabpeve.tb_caustom"
      lbXx_caustom.NTSDbField = "tabpeve.xx_caustom"
      edTb_iva15.NTSDbField = "tabpeve.tb_iva15"
      lbXx_iva15.NTSDbField = "tabpeve.xx_iva15"
      edTb_ivainc.NTSDbField = "tabpeve.tb_ivainc"
      lbXx_ivainc.NTSDbField = "tabpeve.xx_ivainc"
      edTb_ivainc2.NTSDbField = "tabpeve.tb_ivainc2"
      lbXx_ivainc2.NTSDbField = "tabpeve.xx_ivainc2"
      edTb_ivainc3.NTSDbField = "tabpeve.tb_ivainc3"
      lbXx_ivainc3.NTSDbField = "tabpeve.xx_ivainc3"
      edTb_datfinivainc3.NTSDbField = "tabpeve.tb_datfinivainc3"
      edTb_datfinivainc2.NTSDbField = "tabpeve.tb_datfinivainc2"
      edTb_caustib.NTSDbField = "tabpeve.tb_caustib"
      lbXx_caustib.NTSDbField = "tabpeve.xx_caustib"
      '-------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------
      edTb_concas.NTSDbField = "tabpeve.tb_concas"
      lbXx_concas.NTSDbField = "tabpeve.xx_concas"
      edTb_conabat.NTSDbField = "tabpeve.tb_conabat"
      lbXx_conabat.NTSDbField = "tabpeve.xx_conabat"
      edTb_conabpa.NTSDbField = "tabpeve.tb_conabpa"
      lbXx_conabpa.NTSDbField = "tabpeve.xx_conabpa"
      edTb_conomag.NTSDbField = "tabpeve.tb_conomag"
      lbXx_conomag.NTSDbField = "tabpeve.xx_conomag"
      edTb_conribo.NTSDbField = "tabpeve.tb_conribo"
      lbXx_conribo.NTSDbField = "tabpeve.xx_conribo"
      edTb_conriin.NTSDbField = "tabpeve.tb_conriin"
      lbXx_conriin.NTSDbField = "tabpeve.xx_conriin"
      edTb_conriac.NTSDbField = "tabpeve.tb_conriac"
      lbXx_conriac.NTSDbField = "tabpeve.xx_conriac"
      edTb_concauz.NTSDbField = "tabpeve.tb_concauz"
      lbXx_concauz.NTSDbField = "tabpeve.xx_concauz"
      edTb_conclpriv.NTSDbField = "tabpeve.tb_conclpriv"
      lbXx_conclpriv.NTSDbField = "tabpeve.xx_conclpriv"
      edTb_contimba.NTSDbField = "tabpeve.tb_contimba"
      lbXx_contimba.NTSDbField = "tabpeve.xx_contimba"
      '-------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------
      edTb_vtipobf.NTSDbField = "tabpeve.tb_vtipobf"
      lbXx_vtipobf.NTSDbField = "tabpeve.xx_vtipobf"
      edTb_vcontro.NTSDbField = "tabpeve.tb_vcontro"
      lbXx_vcontro.NTSDbField = "tabpeve.xx_vcontro"
      edTb_vmagazz.NTSDbField = "tabpeve.tb_vmagazz"
      lbXx_vmagazz.NTSDbField = "tabpeve.xx_vmagazz"
      edTb_vcaumag.NTSDbField = "tabpeve.tb_vcaumag"
      lbXx_vcaumag.NTSDbField = "tabpeve.xx_vcaumag"
      edTb_vlistin.NTSDbField = "tabpeve.tb_vlistin"
      edXx_vlistin.NTSDbField = "tabpeve.xx_vlistin"
      edTb_vcautra.NTSDbField = "tabpeve.tb_vcautra"
      lbXx_vcautra.NTSDbField = "tabpeve.xx_vcautra"
      cbTb_vacurad.NTSDbField = "tabpeve.tb_vacurad"
      ckTb_vscorpo.NTSText.NTSDbField = "tabpeve.tb_vscorpo"
      ckTb_vdatora.NTSText.NTSDbField = "tabpeve.tb_vdatora"
      '-------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------
      edTb_concrrfc.NTSDbField = "tabpeve.tb_concrrfc"
      lbXx_concrrfc.NTSDbField = "tabpeve.xx_concrrfc"
      edTb_concrrfp.NTSDbField = "tabpeve.tb_concrrfp"
      lbXx_concrrfp.NTSDbField = "tabpeve.xx_concrrfp"
      edTb_conivrfp.NTSDbField = "tabpeve.tb_conivrfp"
      lbXx_conivrfp.NTSDbField = "tabpeve.xx_conivrfp"
      edTb_caurfincc.NTSDbField = "tabpeve.tb_caurfincc"
      lbXx_caurfincc.NTSDbField = "tabpeve.xx_caurfincc"
      edTb_caurfparc.NTSDbField = "tabpeve.tb_caurfparc"
      lbXx_caurfparc.NTSDbField = "tabpeve.xx_caurfparc"
      edTb_caurfnonc.NTSDbField = "tabpeve.tb_caurfnonc"
      lbXx_caurfnonc.NTSDbField = "tabpeve.xx_caurfnonc"
      edTb_caurfriec.NTSDbField = "tabpeve.tb_caurfriec"
      lbXx_caurfriec.NTSDbField = "tabpeve.xx_caurfriec"
      edTb_caurfincp.NTSDbField = "tabpeve.tb_caurfincp"
      lbXx_caurfincp.NTSDbField = "tabpeve.xx_caurfincp"
      edTb_caurfparp.NTSDbField = "tabpeve.tb_caurfparp"
      lbXx_caurfparp.NTSDbField = "tabpeve.xx_caurfparp"
      edTb_caurfnonp.NTSDbField = "tabpeve.tb_caurfnonp"
      lbXx_caurfnonp.NTSDbField = "tabpeve.xx_caurfnonp"
      edTb_caurfriep.NTSDbField = "tabpeve.tb_caurfriep"
      lbXx_caurfriep.NTSDbField = "tabpeve.xx_caurfriep"
      '-------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------
      edTb_tipobfrka.NTSDbField = "tabpeve.tb_tipobfrka"
      lbXx_tipobfrka.NTSDbField = "tabpeve.xx_tipobfrka"
      edTb_tipobfrkb.NTSDbField = "tabpeve.tb_tipobfrkb"
      lbXx_tipobfrkb.NTSDbField = "tabpeve.xx_tipobfrkb"
      edTb_tipobfrkc.NTSDbField = "tabpeve.tb_tipobfrkc"
      lbXx_tipobfrkc.NTSDbField = "tabpeve.xx_tipobfrkc"
      edTb_tipobfrke.NTSDbField = "tabpeve.tb_tipobfrke"
      lbXx_tipobfrke.NTSDbField = "tabpeve.xx_tipobfrke"
      edTb_tipobfrkf.NTSDbField = "tabpeve.tb_tipobfrkf"
      lbXx_tipobfrkf.NTSDbField = "tabpeve.xx_tipobfrkf"
      edTb_tipobfrki.NTSDbField = "tabpeve.tb_tipobfrki"
      lbXx_tipobfrki.NTSDbField = "tabpeve.xx_tipobfrki"
      edTb_tipobfrkn.NTSDbField = "tabpeve.tb_tipobfrkn"
      lbXx_tipobfrkn.NTSDbField = "tabpeve.xx_tipobfrkn"
      edTb_tipobfrks.NTSDbField = "tabpeve.tb_tipobfrks"
      lbXx_tipobfrks.NTSDbField = "tabpeve.xx_tipobfrks"
      edTb_tipobfrkr.NTSDbField = "tabpeve.tb_tipobfrkr"
      lbXx_tipobfrkr.NTSDbField = "tabpeve.xx_tipobfrkr"
      edTb_tipobfrkq.NTSDbField = "tabpeve.tb_tipobfrkq"
      lbXx_tipobfrkq.NTSDbField = "tabpeve.xx_tipobfrkq"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcPeve, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTb_vacurad As New DataTable()
      dttTb_vacurad.Columns.Add("cod", GetType(String))
      dttTb_vacurad.Columns.Add("val", GetType(String))
      dttTb_vacurad.Rows.Add(New Object() {" ", "(nessuno)"})
      dttTb_vacurad.Rows.Add(New Object() {"D", "Destinatario"})
      dttTb_vacurad.Rows.Add(New Object() {"M", "Mittente"})
      dttTb_vacurad.Rows.Add(New Object() {"V", "Vettore"})
      dttTb_vacurad.AcceptChanges()
      cbTb_vacurad.DataSource = dttTb_vacurad
      cbTb_vacurad.ValueMember = "cod"
      cbTb_vacurad.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRMVEPEVE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------------------------------------------
      '--- Carica i ComboBox
      '-------------------------------------------------------------------------------------
      CaricaCombo()
      '-------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------------------------------------
      InitControls()
      '-------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBinding
      '-------------------------------------------------------------------------------------
      If Not oClePeve.File_Apri(DittaCorrente, dsPeve) Then Me.Close()
      dcPeve.DataSource = dsPeve.Tables("TABPEVE")
      dsPeve.AcceptChanges()
      '-------------------------------------------------------------------------------------
      '--- Collego il BindingSource ai vari controlli 
      '-------------------------------------------------------------------------------------
      Bindcontrols()
      '-------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--- e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      '-------------------------------------------------------------------------------------
      GctlSetRoules()

      If dsPeve.Tables("TABPEVE").Rows.Count = 0 Then
        oClePeve.RecordNuovo()
        dcPeve.MoveLast()
        Me.GctlApplicaDefaultValue()
      End If

      '-------------------------------------------------------------------------------------
      dcPeve.ResetBindings(False)
      dcPeve.MoveFirst()

      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEPEVE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEPEVE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcPeve.Dispose()
      dsPeve.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      If oClePeve.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128056616807812500, "Salvo la personalizzazione?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oClePeve.File_Aggiorna(False) Then Return False
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then oClePeve.RecordRipristina(dcPeve.Position, dcPeve.Filter)
      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlb_Aggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
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
      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128039950429843750, "Ripristinare le modifiche apportate alla personalizzazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oClePeve.RecordRipristina(dcPeve.Position, dcPeve.Filter)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try

      oParam.strCodPdc = oClePeve.dttAnaz.Rows(0)!tb_azcodpcon.ToString
      oParam.nEscomp = NTSCInt(oClePeve.dttAnaz.Rows(0)!tb_escomp.ToString)

      '-------------------------------------------------------------------------------------
      If edTb_cauemft.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_cauemft.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_cauemft.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_cauemft.Text Then edTb_cauemft.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_cauemfs.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_cauemfs.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_cauemfs.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_cauemfs.Text Then edTb_cauemfs.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caunoac.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caunoac.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caunoac.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caunoac.Text Then edTb_caunoac.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caunoaf.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caunoaf.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caunoaf.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caunoaf.Text Then edTb_caunoaf.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_cauinc.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_cauinc.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_cauinc.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_cauinc.Text Then edTb_cauinc.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caucorr.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caucorr.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caucorr.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caucorr.Text Then edTb_caucorr.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caustom.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caustom.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caustom.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caustom.Text Then edTb_caustom.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_concas.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_concas.Text, oParam)
        NTSZOOM.strIn = edTb_concas.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_concas.Text Then edTb_concas.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conabat.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conabat.Text, oParam)
        NTSZOOM.strIn = edTb_conabat.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conabat.Text Then edTb_conabat.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conabpa.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conabpa.Text, oParam)
        NTSZOOM.strIn = edTb_conabpa.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conabpa.Text Then edTb_conabpa.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conomag.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conomag.Text, oParam)
        NTSZOOM.strIn = edTb_conomag.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conomag.Text Then edTb_conomag.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conribo.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conribo.Text, oParam)
        NTSZOOM.strIn = edTb_conribo.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conribo.Text Then edTb_conribo.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conriin.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conriin.Text, oParam)
        NTSZOOM.strIn = edTb_conriin.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conriin.Text Then edTb_conriin.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conriac.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conriac.Text, oParam)
        NTSZOOM.strIn = edTb_conriac.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conriac.Text Then edTb_conriac.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_concauz.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_concauz.Text, oParam)
        NTSZOOM.strIn = edTb_concauz.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_concauz.Text Then edTb_concauz.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conclpriv.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conclpriv.Text, oParam)
        NTSZOOM.strIn = edTb_conclpriv.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "C"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conclpriv.Text Then edTb_conclpriv.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_contimba.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_contimba.Text, oParam)
        NTSZOOM.strIn = edTb_contimba.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_contimba.Text Then edTb_contimba.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_concrrfc.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_concrrfc.Text, oParam)
        NTSZOOM.strIn = edTb_concrrfc.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_concrrfc.Text Then edTb_concrrfc.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_concrrfp.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_concrrfp.Text, oParam)
        NTSZOOM.strIn = edTb_concrrfp.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_concrrfp.Text Then edTb_concrrfp.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_conivrfp.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_conivrfp.Text, oParam)
        NTSZOOM.strIn = edTb_conivrfp.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conivrfp.Text Then edTb_conivrfp.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfincc.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfincc.Text, oParam)
        NTSZOOM.strIn = edTb_caurfincc.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfincc.Text Then edTb_caurfincc.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfparc.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfparc.Text, oParam)
        NTSZOOM.strIn = edTb_caurfparc.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfparc.Text Then edTb_caurfparc.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfnonc.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfnonc.Text, oParam)
        NTSZOOM.strIn = edTb_caurfnonc.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfnonc.Text Then edTb_caurfnonc.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfriec.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfriec.Text, oParam)
        NTSZOOM.strIn = edTb_caurfriec.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfriec.Text Then edTb_caurfriec.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfincp.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfincp.Text, oParam)
        NTSZOOM.strIn = edTb_caurfincp.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfincp.Text Then edTb_caurfincp.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfparp.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfparp.Text, oParam)
        NTSZOOM.strIn = edTb_caurfparp.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfparp.Text Then edTb_caurfparp.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfnonp.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfnonp.Text, oParam)
        NTSZOOM.strIn = edTb_caurfnonp.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfnonp.Text Then edTb_caurfnonp.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurfriep.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurfriep.Text, oParam)
        NTSZOOM.strIn = edTb_caurfriep.Text
        oParam.bVisGriglia = True
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurfriep.Text Then edTb_caurfriep.NTSTextDB = NTSZOOM.strIn
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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

End Class