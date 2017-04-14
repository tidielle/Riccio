Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__PAGA
  Public oCallParams As CLE__CLDP
  Public oClePaga As CLE__PAGA

  Public dsPaga As DataSet
  Public dcPaga As BindingSource = New BindingSource

#Region "Moduli"
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
#End Region

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents edTb_codpaga As NTSTextBoxNum
  Public WithEvents lbTb_codpaga As NTSLabel
  Public WithEvents lbTb_despaga As NTSLabel
  Public WithEvents edTb_numrate As NTSTextBoxNum
  Public WithEvents lbTb_numrate As NTSLabel
  Public WithEvents edTb_gioiniz As NTSTextBoxNum
  Public WithEvents edTb_gioperi As NTSTextBoxNum
  Public WithEvents edTb_gioiniz2 As NTSTextBoxNum
  Public WithEvents lbTb_gioiniz As NTSLabel
  Public WithEvents lbTb_gioiniz2 As NTSLabel
  Public WithEvents lbTb_gioperi As NTSLabel
  Public WithEvents lbTb_scopaga As NTSLabel
  Public WithEvents lbTb_speinca As NTSLabel
  Public WithEvents edTb_scopaga As NTSTextBoxNum
  Public WithEvents edTb_speinca As NTSTextBoxNum
  Public WithEvents lbTb_concassp As NTSLabel
  Public WithEvents edTb_concassp As NTSTextBoxNum
  Public WithEvents lbXx_concassp As NTSLabel
  Public WithEvents lbTb_datafdec As NTSLabel
  Public WithEvents cbTb_tippaga As NTSComboBox
  Public WithEvents cbTb_decpaga As NTSComboBox
  Public WithEvents edTb_datafdec As NTSTextBoxData
  Public WithEvents edTb_despaga As NTSTextBoxStr
  Public WithEvents ckTb_shiftm As NTSCheckBox
  Public WithEvents lbTb_mesees As NTSLabel
  Public WithEvents edTb_mesees1 As NTSTextBoxNum
  Public WithEvents edTb_mesees2 As NTSTextBoxNum
  Public WithEvents cbTb_prirata As NTSComboBox
  Public WithEvents lbTb_giofiss As NTSLabel
  Public WithEvents edTb_giofiss As NTSTextBoxNum
  Public WithEvents cbTb_tipincecr As NTSComboBox
  Public WithEvents fmCondPart As NTSGroupBox
  Public WithEvents ckTb_flcondp As NTSCheckBox
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbDesLingua As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCalcolaScadenze As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents edTb_giorni_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbUno As NTSInformatica.NTSLabel
  Public WithEvents lbSei As NTSInformatica.NTSLabel
  Public WithEvents lbCinque As NTSInformatica.NTSLabel
  Public WithEvents lbQuattro As NTSInformatica.NTSLabel
  Public WithEvents lbTre As NTSInformatica.NTSLabel
  Public WithEvents lbDue As NTSInformatica.NTSLabel
  Public WithEvents edTb_quota_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_quota_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_quota_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_quota_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_quota_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_quota_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_base_1 As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_decpaga As NTSInformatica.NTSLabel
  Public WithEvents lbTb_tippaga As NTSInformatica.NTSLabel
  Public WithEvents lbTb_tipincecr As NTSInformatica.NTSLabel
  Public WithEvents lbTb_prirata As NTSInformatica.NTSLabel
  Public WithEvents lbRata As NTSInformatica.NTSLabel
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents lbQuota As NTSInformatica.NTSLabel
  Public WithEvents cbTb_tipp_6 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tipp_5 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tipp_4 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tipp_3 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tipp_2 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tipp_1 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_base_6 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_base_5 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_base_4 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_base_3 As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_base_2 As NTSInformatica.NTSComboBox
  Public WithEvents lbDescrPart As NTSInformatica.NTSLabel
  Public WithEvents edTb_desp_6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbGG As NTSInformatica.NTSLabel
  Public WithEvents edTb_desp_5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desp_4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desp_3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbBaseQuota As NTSInformatica.NTSLabel
  Public WithEvents edTb_desp_2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desp_1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_codstpg As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codstpg As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codmodi As NTSInformatica.NTSLabel
  Public WithEvents edTb_codmodi As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_codmodi As NTSInformatica.NTSLabel
  Public WithEvents lbTb_ggcalend As NTSInformatica.NTSLabel
  Public WithEvents cbTb_ggcalend As NTSInformatica.NTSComboBox
  Public WithEvents ckTb_webvis As NTSInformatica.NTSCheckBox
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbXx_codtpsg As NTSInformatica.NTSLabel
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__PAGA", "BE__PAGA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222149375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePaga = CType(oTmp, CLE__PAGA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__PAGA", strRemoteServer, strRemotePort)
    AddHandler oClePaga.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePaga.Init(oApp, NTSScript, oMenu.oCleComm, "TABPAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__PAGA che eredita da BE__PAGA
    'CallByName(oClePaga, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__PAGA))
    Me.edTb_codpaga = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codpaga = New NTSInformatica.NTSLabel
    Me.lbTb_despaga = New NTSInformatica.NTSLabel
    Me.edTb_numrate = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_numrate = New NTSInformatica.NTSLabel
    Me.edTb_gioiniz = New NTSInformatica.NTSTextBoxNum
    Me.edTb_gioperi = New NTSInformatica.NTSTextBoxNum
    Me.edTb_gioiniz2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_gioiniz = New NTSInformatica.NTSLabel
    Me.lbTb_gioiniz2 = New NTSInformatica.NTSLabel
    Me.lbTb_gioperi = New NTSInformatica.NTSLabel
    Me.lbTb_scopaga = New NTSInformatica.NTSLabel
    Me.lbTb_speinca = New NTSInformatica.NTSLabel
    Me.edTb_scopaga = New NTSInformatica.NTSTextBoxNum
    Me.edTb_speinca = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_concassp = New NTSInformatica.NTSLabel
    Me.edTb_concassp = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_concassp = New NTSInformatica.NTSLabel
    Me.lbTb_datafdec = New NTSInformatica.NTSLabel
    Me.cbTb_tippaga = New NTSInformatica.NTSComboBox
    Me.cbTb_decpaga = New NTSInformatica.NTSComboBox
    Me.edTb_datafdec = New NTSInformatica.NTSTextBoxData
    Me.edTb_despaga = New NTSInformatica.NTSTextBoxStr
    Me.ckTb_shiftm = New NTSInformatica.NTSCheckBox
    Me.lbTb_mesees = New NTSInformatica.NTSLabel
    Me.edTb_mesees1 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_mesees2 = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_prirata = New NTSInformatica.NTSComboBox
    Me.lbTb_giofiss = New NTSInformatica.NTSLabel
    Me.edTb_giofiss = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_tipincecr = New NTSInformatica.NTSComboBox
    Me.fmCondPart = New NTSInformatica.NTSGroupBox
    Me.cbTb_tipp_6 = New NTSInformatica.NTSComboBox
    Me.cbTb_tipp_5 = New NTSInformatica.NTSComboBox
    Me.cbTb_tipp_4 = New NTSInformatica.NTSComboBox
    Me.cbTb_tipp_3 = New NTSInformatica.NTSComboBox
    Me.cbTb_tipp_2 = New NTSInformatica.NTSComboBox
    Me.cbTb_tipp_1 = New NTSInformatica.NTSComboBox
    Me.cbTb_base_6 = New NTSInformatica.NTSComboBox
    Me.cbTb_base_5 = New NTSInformatica.NTSComboBox
    Me.cbTb_base_4 = New NTSInformatica.NTSComboBox
    Me.cbTb_base_3 = New NTSInformatica.NTSComboBox
    Me.cbTb_base_2 = New NTSInformatica.NTSComboBox
    Me.lbDescrPart = New NTSInformatica.NTSLabel
    Me.edTb_desp_6 = New NTSInformatica.NTSTextBoxStr
    Me.lbGG = New NTSInformatica.NTSLabel
    Me.edTb_desp_5 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_giorni_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_desp_4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_giorni_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_desp_3 = New NTSInformatica.NTSTextBoxStr
    Me.lbBaseQuota = New NTSInformatica.NTSLabel
    Me.edTb_desp_2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_giorni_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_desp_1 = New NTSInformatica.NTSTextBoxStr
    Me.cbTb_base_1 = New NTSInformatica.NTSComboBox
    Me.edTb_giorni_3 = New NTSInformatica.NTSTextBoxNum
    Me.lbQuota = New NTSInformatica.NTSLabel
    Me.edTb_giorni_2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.edTb_giorni_1 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_quota_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_quota_5 = New NTSInformatica.NTSTextBoxNum
    Me.lbRata = New NTSInformatica.NTSLabel
    Me.edTb_quota_4 = New NTSInformatica.NTSTextBoxNum
    Me.lbSei = New NTSInformatica.NTSLabel
    Me.edTb_quota_3 = New NTSInformatica.NTSTextBoxNum
    Me.lbCinque = New NTSInformatica.NTSLabel
    Me.edTb_quota_2 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_quota_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbQuattro = New NTSInformatica.NTSLabel
    Me.lbTre = New NTSInformatica.NTSLabel
    Me.lbDue = New NTSInformatica.NTSLabel
    Me.lbUno = New NTSInformatica.NTSLabel
    Me.ckTb_flcondp = New NTSInformatica.NTSCheckBox
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbDesLingua = New NTSInformatica.NTSBarButtonItem
    Me.tlbCalcolaScadenze = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbTb_tippaga = New NTSInformatica.NTSLabel
    Me.lbTb_decpaga = New NTSInformatica.NTSLabel
    Me.lbTb_tipincecr = New NTSInformatica.NTSLabel
    Me.lbTb_prirata = New NTSInformatica.NTSLabel
    Me.lbTb_codstpg = New NTSInformatica.NTSLabel
    Me.edTb_codstpg = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codtpsg = New NTSInformatica.NTSLabel
    Me.lbTb_codmodi = New NTSInformatica.NTSLabel
    Me.edTb_codmodi = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codmodi = New NTSInformatica.NTSLabel
    Me.cbTb_ggcalend = New NTSInformatica.NTSComboBox
    Me.lbTb_ggcalend = New NTSInformatica.NTSLabel
    Me.ckTb_webvis = New NTSInformatica.NTSCheckBox
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codpaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_numrate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_gioiniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_gioperi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_gioiniz2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_scopaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_speinca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_concassp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tippaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_decpaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datafdec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_despaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_shiftm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_mesees1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_mesees2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_prirata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giofiss.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipincecr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCondPart, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCondPart.SuspendLayout()
    CType(Me.cbTb_tipp_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipp_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipp_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipp_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipp_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tipp_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desp_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_base_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_quota_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_flcondp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codstpg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codmodi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_ggcalend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_webvis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'edTb_codpaga
    '
    Me.edTb_codpaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codpaga.EditValue = "0"
    Me.edTb_codpaga.Location = New System.Drawing.Point(141, 34)
    Me.edTb_codpaga.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_codpaga.Name = "edTb_codpaga"
    Me.edTb_codpaga.NTSDbField = ""
    Me.edTb_codpaga.NTSFormat = "0"
    Me.edTb_codpaga.NTSForzaVisZoom = False
    Me.edTb_codpaga.NTSOldValue = ""
    Me.edTb_codpaga.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.edTb_codpaga.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codpaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codpaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codpaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codpaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codpaga.Properties.AutoHeight = False
    Me.edTb_codpaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codpaga.Properties.MaxLength = 65536
    Me.edTb_codpaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codpaga.Size = New System.Drawing.Size(48, 20)
    Me.edTb_codpaga.TabIndex = 0
    '
    'lbTb_codpaga
    '
    Me.lbTb_codpaga.AutoSize = True
    Me.lbTb_codpaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codpaga.Location = New System.Drawing.Point(12, 39)
    Me.lbTb_codpaga.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_codpaga.Name = "lbTb_codpaga"
    Me.lbTb_codpaga.NTSDbField = ""
    Me.lbTb_codpaga.Size = New System.Drawing.Size(96, 13)
    Me.lbTb_codpaga.TabIndex = 9
    Me.lbTb_codpaga.Text = "Codice pagamento"
    Me.lbTb_codpaga.Tooltip = ""
    Me.lbTb_codpaga.UseMnemonic = False
    '
    'lbTb_despaga
    '
    Me.lbTb_despaga.AutoSize = True
    Me.lbTb_despaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_despaga.Location = New System.Drawing.Point(12, 63)
    Me.lbTb_despaga.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_despaga.Name = "lbTb_despaga"
    Me.lbTb_despaga.NTSDbField = ""
    Me.lbTb_despaga.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_despaga.TabIndex = 10
    Me.lbTb_despaga.Text = "Descrizione"
    Me.lbTb_despaga.Tooltip = ""
    Me.lbTb_despaga.UseMnemonic = False
    '
    'edTb_numrate
    '
    Me.edTb_numrate.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_numrate.EditValue = "0"
    Me.edTb_numrate.Location = New System.Drawing.Point(141, 144)
    Me.edTb_numrate.Name = "edTb_numrate"
    Me.edTb_numrate.NTSDbField = ""
    Me.edTb_numrate.NTSFormat = "0"
    Me.edTb_numrate.NTSForzaVisZoom = False
    Me.edTb_numrate.NTSOldValue = ""
    Me.edTb_numrate.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_numrate.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_numrate.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_numrate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_numrate.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_numrate.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_numrate.Properties.AutoHeight = False
    Me.edTb_numrate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_numrate.Properties.MaxLength = 65536
    Me.edTb_numrate.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_numrate.Size = New System.Drawing.Size(39, 20)
    Me.edTb_numrate.TabIndex = 5
    '
    'lbTb_numrate
    '
    Me.lbTb_numrate.AutoSize = True
    Me.lbTb_numrate.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_numrate.Location = New System.Drawing.Point(12, 147)
    Me.lbTb_numrate.Name = "lbTb_numrate"
    Me.lbTb_numrate.NTSDbField = ""
    Me.lbTb_numrate.Size = New System.Drawing.Size(67, 13)
    Me.lbTb_numrate.TabIndex = 11
    Me.lbTb_numrate.Text = "Numero rate"
    Me.lbTb_numrate.Tooltip = ""
    Me.lbTb_numrate.UseMnemonic = False
    '
    'edTb_gioiniz
    '
    Me.edTb_gioiniz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_gioiniz.EditValue = "0"
    Me.edTb_gioiniz.Location = New System.Drawing.Point(141, 166)
    Me.edTb_gioiniz.Name = "edTb_gioiniz"
    Me.edTb_gioiniz.NTSDbField = ""
    Me.edTb_gioiniz.NTSFormat = "0"
    Me.edTb_gioiniz.NTSForzaVisZoom = False
    Me.edTb_gioiniz.NTSOldValue = ""
    Me.edTb_gioiniz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_gioiniz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_gioiniz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_gioiniz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_gioiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_gioiniz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_gioiniz.Properties.AutoHeight = False
    Me.edTb_gioiniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_gioiniz.Properties.MaxLength = 65536
    Me.edTb_gioiniz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_gioiniz.Size = New System.Drawing.Size(39, 20)
    Me.edTb_gioiniz.TabIndex = 6
    '
    'edTb_gioperi
    '
    Me.edTb_gioperi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_gioperi.EditValue = "0"
    Me.edTb_gioperi.Location = New System.Drawing.Point(251, 166)
    Me.edTb_gioperi.Name = "edTb_gioperi"
    Me.edTb_gioperi.NTSDbField = ""
    Me.edTb_gioperi.NTSFormat = "0"
    Me.edTb_gioperi.NTSForzaVisZoom = False
    Me.edTb_gioperi.NTSOldValue = ""
    Me.edTb_gioperi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_gioperi.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_gioperi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_gioperi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_gioperi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_gioperi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_gioperi.Properties.AutoHeight = False
    Me.edTb_gioperi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_gioperi.Properties.MaxLength = 65536
    Me.edTb_gioperi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_gioperi.Size = New System.Drawing.Size(39, 20)
    Me.edTb_gioperi.TabIndex = 7
    '
    'edTb_gioiniz2
    '
    Me.edTb_gioiniz2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_gioiniz2.EditValue = "0"
    Me.edTb_gioiniz2.Location = New System.Drawing.Point(141, 188)
    Me.edTb_gioiniz2.Name = "edTb_gioiniz2"
    Me.edTb_gioiniz2.NTSDbField = ""
    Me.edTb_gioiniz2.NTSFormat = "0"
    Me.edTb_gioiniz2.NTSForzaVisZoom = False
    Me.edTb_gioiniz2.NTSOldValue = ""
    Me.edTb_gioiniz2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_gioiniz2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_gioiniz2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_gioiniz2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_gioiniz2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_gioiniz2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_gioiniz2.Properties.AutoHeight = False
    Me.edTb_gioiniz2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_gioiniz2.Properties.MaxLength = 65536
    Me.edTb_gioiniz2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_gioiniz2.Size = New System.Drawing.Size(39, 20)
    Me.edTb_gioiniz2.TabIndex = 8
    '
    'lbTb_gioiniz
    '
    Me.lbTb_gioiniz.AutoSize = True
    Me.lbTb_gioiniz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_gioiniz.Location = New System.Drawing.Point(12, 169)
    Me.lbTb_gioiniz.Name = "lbTb_gioiniz"
    Me.lbTb_gioiniz.NTSDbField = ""
    Me.lbTb_gioiniz.Size = New System.Drawing.Size(100, 13)
    Me.lbTb_gioiniz.TabIndex = 12
    Me.lbTb_gioiniz.Text = "Decorrenza 1a rata"
    Me.lbTb_gioiniz.Tooltip = ""
    Me.lbTb_gioiniz.UseMnemonic = False
    '
    'lbTb_gioiniz2
    '
    Me.lbTb_gioiniz2.AutoSize = True
    Me.lbTb_gioiniz2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_gioiniz2.Location = New System.Drawing.Point(12, 191)
    Me.lbTb_gioiniz2.Name = "lbTb_gioiniz2"
    Me.lbTb_gioiniz2.NTSDbField = ""
    Me.lbTb_gioiniz2.Size = New System.Drawing.Size(100, 13)
    Me.lbTb_gioiniz2.TabIndex = 13
    Me.lbTb_gioiniz2.Text = "Decorrenza 2a rata"
    Me.lbTb_gioiniz2.Tooltip = ""
    Me.lbTb_gioiniz2.UseMnemonic = False
    '
    'lbTb_gioperi
    '
    Me.lbTb_gioperi.AutoSize = True
    Me.lbTb_gioperi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_gioperi.Location = New System.Drawing.Point(189, 169)
    Me.lbTb_gioperi.Name = "lbTb_gioperi"
    Me.lbTb_gioperi.NTSDbField = ""
    Me.lbTb_gioperi.Size = New System.Drawing.Size(56, 13)
    Me.lbTb_gioperi.TabIndex = 15
    Me.lbTb_gioperi.Text = "Periodicità"
    Me.lbTb_gioperi.Tooltip = ""
    Me.lbTb_gioperi.UseMnemonic = False
    '
    'lbTb_scopaga
    '
    Me.lbTb_scopaga.AutoSize = True
    Me.lbTb_scopaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scopaga.Location = New System.Drawing.Point(317, 169)
    Me.lbTb_scopaga.Name = "lbTb_scopaga"
    Me.lbTb_scopaga.NTSDbField = ""
    Me.lbTb_scopaga.Size = New System.Drawing.Size(97, 13)
    Me.lbTb_scopaga.TabIndex = 16
    Me.lbTb_scopaga.Text = "Sconto pagamento"
    Me.lbTb_scopaga.Tooltip = ""
    Me.lbTb_scopaga.UseMnemonic = False
    '
    'lbTb_speinca
    '
    Me.lbTb_speinca.AutoSize = True
    Me.lbTb_speinca.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_speinca.Location = New System.Drawing.Point(466, 169)
    Me.lbTb_speinca.Name = "lbTb_speinca"
    Me.lbTb_speinca.NTSDbField = ""
    Me.lbTb_speinca.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_speinca.TabIndex = 17
    Me.lbTb_speinca.Text = "Spese incasso"
    Me.lbTb_speinca.Tooltip = ""
    Me.lbTb_speinca.UseMnemonic = False
    '
    'edTb_scopaga
    '
    Me.edTb_scopaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_scopaga.EditValue = "0"
    Me.edTb_scopaga.Location = New System.Drawing.Point(421, 166)
    Me.edTb_scopaga.Name = "edTb_scopaga"
    Me.edTb_scopaga.NTSDbField = ""
    Me.edTb_scopaga.NTSFormat = "0"
    Me.edTb_scopaga.NTSForzaVisZoom = False
    Me.edTb_scopaga.NTSOldValue = ""
    Me.edTb_scopaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_scopaga.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_scopaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_scopaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_scopaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_scopaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_scopaga.Properties.AutoHeight = False
    Me.edTb_scopaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_scopaga.Properties.MaxLength = 65536
    Me.edTb_scopaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_scopaga.Size = New System.Drawing.Size(39, 20)
    Me.edTb_scopaga.TabIndex = 14
    '
    'edTb_speinca
    '
    Me.edTb_speinca.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_speinca.EditValue = "0"
    Me.edTb_speinca.Location = New System.Drawing.Point(551, 166)
    Me.edTb_speinca.Name = "edTb_speinca"
    Me.edTb_speinca.NTSDbField = ""
    Me.edTb_speinca.NTSFormat = "0"
    Me.edTb_speinca.NTSForzaVisZoom = False
    Me.edTb_speinca.NTSOldValue = ""
    Me.edTb_speinca.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_speinca.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_speinca.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_speinca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_speinca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_speinca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_speinca.Properties.AutoHeight = False
    Me.edTb_speinca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_speinca.Properties.MaxLength = 65536
    Me.edTb_speinca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_speinca.Size = New System.Drawing.Size(84, 20)
    Me.edTb_speinca.TabIndex = 15
    '
    'lbTb_concassp
    '
    Me.lbTb_concassp.AutoSize = True
    Me.lbTb_concassp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_concassp.Location = New System.Drawing.Point(12, 215)
    Me.lbTb_concassp.Name = "lbTb_concassp"
    Me.lbTb_concassp.NTSDbField = ""
    Me.lbTb_concassp.Size = New System.Drawing.Size(226, 13)
    Me.lbTb_concassp.TabIndex = 14
    Me.lbTb_concassp.Text = "Contropartita vendite/acquisti per contabilizz."
    Me.lbTb_concassp.Tooltip = ""
    Me.lbTb_concassp.UseMnemonic = False
    '
    'edTb_concassp
    '
    Me.edTb_concassp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_concassp.EditValue = "0"
    Me.edTb_concassp.Location = New System.Drawing.Point(251, 212)
    Me.edTb_concassp.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.edTb_concassp.Name = "edTb_concassp"
    Me.edTb_concassp.NTSDbField = ""
    Me.edTb_concassp.NTSFormat = "0"
    Me.edTb_concassp.NTSForzaVisZoom = False
    Me.edTb_concassp.NTSOldValue = ""
    Me.edTb_concassp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_concassp.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_concassp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_concassp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_concassp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_concassp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_concassp.Properties.AutoHeight = False
    Me.edTb_concassp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_concassp.Properties.MaxLength = 65536
    Me.edTb_concassp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_concassp.Size = New System.Drawing.Size(54, 20)
    Me.edTb_concassp.TabIndex = 16
    '
    'lbXx_concassp
    '
    Me.lbXx_concassp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_concassp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_concassp.Location = New System.Drawing.Point(311, 211)
    Me.lbXx_concassp.Name = "lbXx_concassp"
    Me.lbXx_concassp.NTSDbField = ""
    Me.lbXx_concassp.Size = New System.Drawing.Size(327, 20)
    Me.lbXx_concassp.TabIndex = 18
    Me.lbXx_concassp.Text = "*********"
    Me.lbXx_concassp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_concassp.Tooltip = ""
    Me.lbXx_concassp.UseMnemonic = False
    '
    'lbTb_datafdec
    '
    Me.lbTb_datafdec.AutoSize = True
    Me.lbTb_datafdec.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_datafdec.Location = New System.Drawing.Point(12, 125)
    Me.lbTb_datafdec.Name = "lbTb_datafdec"
    Me.lbTb_datafdec.NTSDbField = ""
    Me.lbTb_datafdec.Size = New System.Drawing.Size(123, 13)
    Me.lbTb_datafdec.TabIndex = 25
    Me.lbTb_datafdec.Text = "Data fissa di decorrenza"
    Me.lbTb_datafdec.Tooltip = ""
    Me.lbTb_datafdec.UseMnemonic = False
    '
    'cbTb_tippaga
    '
    Me.cbTb_tippaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tippaga.DataSource = Nothing
    Me.cbTb_tippaga.DisplayMember = ""
    Me.cbTb_tippaga.Location = New System.Drawing.Point(141, 78)
    Me.cbTb_tippaga.Name = "cbTb_tippaga"
    Me.cbTb_tippaga.NTSDbField = ""
    Me.cbTb_tippaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tippaga.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tippaga.Properties.AutoHeight = False
    Me.cbTb_tippaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tippaga.Properties.DropDownRows = 30
    Me.cbTb_tippaga.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tippaga.SelectedValue = ""
    Me.cbTb_tippaga.Size = New System.Drawing.Size(149, 20)
    Me.cbTb_tippaga.TabIndex = 2
    Me.cbTb_tippaga.ValueMember = ""
    '
    'cbTb_decpaga
    '
    Me.cbTb_decpaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_decpaga.DataSource = Nothing
    Me.cbTb_decpaga.DisplayMember = ""
    Me.cbTb_decpaga.Location = New System.Drawing.Point(141, 100)
    Me.cbTb_decpaga.Name = "cbTb_decpaga"
    Me.cbTb_decpaga.NTSDbField = ""
    Me.cbTb_decpaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_decpaga.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_decpaga.Properties.AutoHeight = False
    Me.cbTb_decpaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_decpaga.Properties.DropDownRows = 30
    Me.cbTb_decpaga.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_decpaga.SelectedValue = ""
    Me.cbTb_decpaga.Size = New System.Drawing.Size(149, 20)
    Me.cbTb_decpaga.TabIndex = 3
    Me.cbTb_decpaga.ValueMember = ""
    '
    'edTb_datafdec
    '
    Me.edTb_datafdec.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datafdec.EditValue = "01/01/2000"
    Me.edTb_datafdec.Location = New System.Drawing.Point(141, 122)
    Me.edTb_datafdec.Name = "edTb_datafdec"
    Me.edTb_datafdec.NTSDbField = ""
    Me.edTb_datafdec.NTSForzaVisZoom = False
    Me.edTb_datafdec.NTSOldValue = ""
    Me.edTb_datafdec.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_datafdec.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_datafdec.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datafdec.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datafdec.Properties.AutoHeight = False
    Me.edTb_datafdec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datafdec.Properties.MaxLength = 65536
    Me.edTb_datafdec.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datafdec.Size = New System.Drawing.Size(149, 20)
    Me.edTb_datafdec.TabIndex = 4
    '
    'edTb_despaga
    '
    Me.edTb_despaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_despaga.EditValue = ""
    Me.edTb_despaga.Location = New System.Drawing.Point(141, 56)
    Me.edTb_despaga.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_despaga.Name = "edTb_despaga"
    Me.edTb_despaga.NTSDbField = ""
    Me.edTb_despaga.NTSForzaVisZoom = False
    Me.edTb_despaga.NTSOldValue = ""
    Me.edTb_despaga.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_despaga.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_despaga.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_despaga.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_despaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_despaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_despaga.Properties.AutoHeight = False
    Me.edTb_despaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_despaga.Properties.MaxLength = 65536
    Me.edTb_despaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_despaga.Size = New System.Drawing.Size(494, 20)
    Me.edTb_despaga.TabIndex = 1
    '
    'ckTb_shiftm
    '
    Me.ckTb_shiftm.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_shiftm.Location = New System.Drawing.Point(421, 144)
    Me.ckTb_shiftm.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.ckTb_shiftm.Name = "ckTb_shiftm"
    Me.ckTb_shiftm.NTSCheckValue = "S"
    Me.ckTb_shiftm.NTSUnCheckValue = "N"
    Me.ckTb_shiftm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_shiftm.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_shiftm.Properties.AutoHeight = False
    Me.ckTb_shiftm.Properties.Caption = "S&posta di un mese le rate successive"
    Me.ckTb_shiftm.Size = New System.Drawing.Size(201, 19)
    Me.ckTb_shiftm.TabIndex = 13
    '
    'lbTb_mesees
    '
    Me.lbTb_mesees.AutoSize = True
    Me.lbTb_mesees.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_mesees.Location = New System.Drawing.Point(317, 125)
    Me.lbTb_mesees.Name = "lbTb_mesees"
    Me.lbTb_mesees.NTSDbField = ""
    Me.lbTb_mesees.Size = New System.Drawing.Size(62, 13)
    Me.lbTb_mesees.TabIndex = 31
    Me.lbTb_mesees.Tag = ""
    Me.lbTb_mesees.Text = "Mesi esclusi"
    Me.lbTb_mesees.Tooltip = ""
    Me.lbTb_mesees.UseMnemonic = False
    '
    'edTb_mesees1
    '
    Me.edTb_mesees1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_mesees1.EditValue = "0"
    Me.edTb_mesees1.Location = New System.Drawing.Point(421, 122)
    Me.edTb_mesees1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.edTb_mesees1.Name = "edTb_mesees1"
    Me.edTb_mesees1.NTSDbField = ""
    Me.edTb_mesees1.NTSFormat = "0"
    Me.edTb_mesees1.NTSForzaVisZoom = False
    Me.edTb_mesees1.NTSOldValue = ""
    Me.edTb_mesees1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_mesees1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_mesees1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_mesees1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_mesees1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_mesees1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_mesees1.Properties.AutoHeight = False
    Me.edTb_mesees1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_mesees1.Properties.MaxLength = 65536
    Me.edTb_mesees1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_mesees1.Size = New System.Drawing.Size(39, 20)
    Me.edTb_mesees1.TabIndex = 10
    '
    'edTb_mesees2
    '
    Me.edTb_mesees2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_mesees2.EditValue = "0"
    Me.edTb_mesees2.Location = New System.Drawing.Point(466, 122)
    Me.edTb_mesees2.Name = "edTb_mesees2"
    Me.edTb_mesees2.NTSDbField = ""
    Me.edTb_mesees2.NTSFormat = "0"
    Me.edTb_mesees2.NTSForzaVisZoom = False
    Me.edTb_mesees2.NTSOldValue = ""
    Me.edTb_mesees2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_mesees2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_mesees2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_mesees2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_mesees2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_mesees2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_mesees2.Properties.AutoHeight = False
    Me.edTb_mesees2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_mesees2.Properties.MaxLength = 65536
    Me.edTb_mesees2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_mesees2.Size = New System.Drawing.Size(39, 20)
    Me.edTb_mesees2.TabIndex = 11
    '
    'cbTb_prirata
    '
    Me.cbTb_prirata.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_prirata.DataSource = Nothing
    Me.cbTb_prirata.DisplayMember = ""
    Me.cbTb_prirata.Location = New System.Drawing.Point(421, 78)
    Me.cbTb_prirata.Name = "cbTb_prirata"
    Me.cbTb_prirata.NTSDbField = ""
    Me.cbTb_prirata.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_prirata.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_prirata.Properties.AutoHeight = False
    Me.cbTb_prirata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_prirata.Properties.DropDownRows = 30
    Me.cbTb_prirata.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_prirata.SelectedValue = ""
    Me.cbTb_prirata.Size = New System.Drawing.Size(214, 20)
    Me.cbTb_prirata.TabIndex = 9
    Me.cbTb_prirata.ValueMember = ""
    '
    'lbTb_giofiss
    '
    Me.lbTb_giofiss.AutoSize = True
    Me.lbTb_giofiss.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_giofiss.Location = New System.Drawing.Point(527, 125)
    Me.lbTb_giofiss.Name = "lbTb_giofiss"
    Me.lbTb_giofiss.NTSDbField = ""
    Me.lbTb_giofiss.Size = New System.Drawing.Size(63, 13)
    Me.lbTb_giofiss.TabIndex = 35
    Me.lbTb_giofiss.Tag = ""
    Me.lbTb_giofiss.Text = "Giorno fisso"
    Me.lbTb_giofiss.Tooltip = ""
    Me.lbTb_giofiss.UseMnemonic = False
    '
    'edTb_giofiss
    '
    Me.edTb_giofiss.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giofiss.EditValue = "0"
    Me.edTb_giofiss.Location = New System.Drawing.Point(596, 122)
    Me.edTb_giofiss.Name = "edTb_giofiss"
    Me.edTb_giofiss.NTSDbField = ""
    Me.edTb_giofiss.NTSFormat = "0"
    Me.edTb_giofiss.NTSForzaVisZoom = False
    Me.edTb_giofiss.NTSOldValue = ""
    Me.edTb_giofiss.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giofiss.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giofiss.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giofiss.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giofiss.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giofiss.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giofiss.Properties.AutoHeight = False
    Me.edTb_giofiss.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giofiss.Properties.MaxLength = 65536
    Me.edTb_giofiss.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giofiss.Size = New System.Drawing.Size(39, 20)
    Me.edTb_giofiss.TabIndex = 12
    '
    'cbTb_tipincecr
    '
    Me.cbTb_tipincecr.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipincecr.DataSource = Nothing
    Me.cbTb_tipincecr.DisplayMember = ""
    Me.cbTb_tipincecr.Location = New System.Drawing.Point(141, 234)
    Me.cbTb_tipincecr.Name = "cbTb_tipincecr"
    Me.cbTb_tipincecr.NTSDbField = ""
    Me.cbTb_tipincecr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipincecr.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipincecr.Properties.AutoHeight = False
    Me.cbTb_tipincecr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipincecr.Properties.DropDownRows = 30
    Me.cbTb_tipincecr.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipincecr.SelectedValue = ""
    Me.cbTb_tipincecr.Size = New System.Drawing.Size(164, 20)
    Me.cbTb_tipincecr.TabIndex = 17
    Me.cbTb_tipincecr.ValueMember = ""
    '
    'fmCondPart
    '
    Me.fmCondPart.AllowDrop = True
    Me.fmCondPart.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCondPart.Appearance.Options.UseBackColor = True
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_6)
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_5)
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_4)
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_3)
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_2)
    Me.fmCondPart.Controls.Add(Me.cbTb_tipp_1)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_6)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_5)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_4)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_3)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_2)
    Me.fmCondPart.Controls.Add(Me.lbDescrPart)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_6)
    Me.fmCondPart.Controls.Add(Me.lbGG)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_5)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_6)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_4)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_5)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_3)
    Me.fmCondPart.Controls.Add(Me.lbBaseQuota)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_2)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_4)
    Me.fmCondPart.Controls.Add(Me.edTb_desp_1)
    Me.fmCondPart.Controls.Add(Me.cbTb_base_1)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_3)
    Me.fmCondPart.Controls.Add(Me.lbQuota)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_2)
    Me.fmCondPart.Controls.Add(Me.lbTipo)
    Me.fmCondPart.Controls.Add(Me.edTb_giorni_1)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_6)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_5)
    Me.fmCondPart.Controls.Add(Me.lbRata)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_4)
    Me.fmCondPart.Controls.Add(Me.lbSei)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_3)
    Me.fmCondPart.Controls.Add(Me.lbCinque)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_2)
    Me.fmCondPart.Controls.Add(Me.edTb_quota_1)
    Me.fmCondPart.Controls.Add(Me.lbQuattro)
    Me.fmCondPart.Controls.Add(Me.lbTre)
    Me.fmCondPart.Controls.Add(Me.lbDue)
    Me.fmCondPart.Controls.Add(Me.lbUno)
    Me.fmCondPart.Controls.Add(Me.ckTb_flcondp)
    Me.fmCondPart.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCondPart.Location = New System.Drawing.Point(11, 319)
    Me.fmCondPart.Name = "fmCondPart"
    Me.fmCondPart.Size = New System.Drawing.Size(624, 172)
    Me.fmCondPart.TabIndex = 18
    '
    'cbTb_tipp_6
    '
    Me.cbTb_tipp_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_6.DataSource = Nothing
    Me.cbTb_tipp_6.DisplayMember = ""
    Me.cbTb_tipp_6.Location = New System.Drawing.Point(39, 148)
    Me.cbTb_tipp_6.Name = "cbTb_tipp_6"
    Me.cbTb_tipp_6.NTSDbField = ""
    Me.cbTb_tipp_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_6.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_6.Properties.AutoHeight = False
    Me.cbTb_tipp_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_6.Properties.DropDownRows = 30
    Me.cbTb_tipp_6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_6.SelectedValue = ""
    Me.cbTb_tipp_6.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_6.TabIndex = 59
    Me.cbTb_tipp_6.ValueMember = ""
    '
    'cbTb_tipp_5
    '
    Me.cbTb_tipp_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_5.DataSource = Nothing
    Me.cbTb_tipp_5.DisplayMember = ""
    Me.cbTb_tipp_5.Location = New System.Drawing.Point(39, 126)
    Me.cbTb_tipp_5.Name = "cbTb_tipp_5"
    Me.cbTb_tipp_5.NTSDbField = ""
    Me.cbTb_tipp_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_5.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_5.Properties.AutoHeight = False
    Me.cbTb_tipp_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_5.Properties.DropDownRows = 30
    Me.cbTb_tipp_5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_5.SelectedValue = ""
    Me.cbTb_tipp_5.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_5.TabIndex = 58
    Me.cbTb_tipp_5.ValueMember = ""
    '
    'cbTb_tipp_4
    '
    Me.cbTb_tipp_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_4.DataSource = Nothing
    Me.cbTb_tipp_4.DisplayMember = ""
    Me.cbTb_tipp_4.Location = New System.Drawing.Point(39, 104)
    Me.cbTb_tipp_4.Name = "cbTb_tipp_4"
    Me.cbTb_tipp_4.NTSDbField = ""
    Me.cbTb_tipp_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_4.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_4.Properties.AutoHeight = False
    Me.cbTb_tipp_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_4.Properties.DropDownRows = 30
    Me.cbTb_tipp_4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_4.SelectedValue = ""
    Me.cbTb_tipp_4.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_4.TabIndex = 57
    Me.cbTb_tipp_4.ValueMember = ""
    '
    'cbTb_tipp_3
    '
    Me.cbTb_tipp_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_3.DataSource = Nothing
    Me.cbTb_tipp_3.DisplayMember = ""
    Me.cbTb_tipp_3.Location = New System.Drawing.Point(39, 82)
    Me.cbTb_tipp_3.Name = "cbTb_tipp_3"
    Me.cbTb_tipp_3.NTSDbField = ""
    Me.cbTb_tipp_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_3.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_3.Properties.AutoHeight = False
    Me.cbTb_tipp_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_3.Properties.DropDownRows = 30
    Me.cbTb_tipp_3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_3.SelectedValue = ""
    Me.cbTb_tipp_3.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_3.TabIndex = 56
    Me.cbTb_tipp_3.ValueMember = ""
    '
    'cbTb_tipp_2
    '
    Me.cbTb_tipp_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_2.DataSource = Nothing
    Me.cbTb_tipp_2.DisplayMember = ""
    Me.cbTb_tipp_2.Location = New System.Drawing.Point(39, 60)
    Me.cbTb_tipp_2.Name = "cbTb_tipp_2"
    Me.cbTb_tipp_2.NTSDbField = ""
    Me.cbTb_tipp_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_2.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_2.Properties.AutoHeight = False
    Me.cbTb_tipp_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_2.Properties.DropDownRows = 30
    Me.cbTb_tipp_2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_2.SelectedValue = ""
    Me.cbTb_tipp_2.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_2.TabIndex = 55
    Me.cbTb_tipp_2.ValueMember = ""
    '
    'cbTb_tipp_1
    '
    Me.cbTb_tipp_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tipp_1.DataSource = Nothing
    Me.cbTb_tipp_1.DisplayMember = ""
    Me.cbTb_tipp_1.Location = New System.Drawing.Point(39, 38)
    Me.cbTb_tipp_1.Name = "cbTb_tipp_1"
    Me.cbTb_tipp_1.NTSDbField = ""
    Me.cbTb_tipp_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tipp_1.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tipp_1.Properties.AutoHeight = False
    Me.cbTb_tipp_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tipp_1.Properties.DropDownRows = 30
    Me.cbTb_tipp_1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tipp_1.SelectedValue = ""
    Me.cbTb_tipp_1.Size = New System.Drawing.Size(117, 20)
    Me.cbTb_tipp_1.TabIndex = 54
    Me.cbTb_tipp_1.ValueMember = ""
    '
    'cbTb_base_6
    '
    Me.cbTb_base_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_6.DataSource = Nothing
    Me.cbTb_base_6.DisplayMember = ""
    Me.cbTb_base_6.Location = New System.Drawing.Point(218, 148)
    Me.cbTb_base_6.Name = "cbTb_base_6"
    Me.cbTb_base_6.NTSDbField = ""
    Me.cbTb_base_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_6.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_6.Properties.AutoHeight = False
    Me.cbTb_base_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_6.Properties.DropDownRows = 30
    Me.cbTb_base_6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_6.SelectedValue = ""
    Me.cbTb_base_6.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_6.TabIndex = 53
    Me.cbTb_base_6.ValueMember = ""
    '
    'cbTb_base_5
    '
    Me.cbTb_base_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_5.DataSource = Nothing
    Me.cbTb_base_5.DisplayMember = ""
    Me.cbTb_base_5.Location = New System.Drawing.Point(218, 126)
    Me.cbTb_base_5.Name = "cbTb_base_5"
    Me.cbTb_base_5.NTSDbField = ""
    Me.cbTb_base_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_5.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_5.Properties.AutoHeight = False
    Me.cbTb_base_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_5.Properties.DropDownRows = 30
    Me.cbTb_base_5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_5.SelectedValue = ""
    Me.cbTb_base_5.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_5.TabIndex = 52
    Me.cbTb_base_5.ValueMember = ""
    '
    'cbTb_base_4
    '
    Me.cbTb_base_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_4.DataSource = Nothing
    Me.cbTb_base_4.DisplayMember = ""
    Me.cbTb_base_4.Location = New System.Drawing.Point(218, 104)
    Me.cbTb_base_4.Name = "cbTb_base_4"
    Me.cbTb_base_4.NTSDbField = ""
    Me.cbTb_base_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_4.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_4.Properties.AutoHeight = False
    Me.cbTb_base_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_4.Properties.DropDownRows = 30
    Me.cbTb_base_4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_4.SelectedValue = ""
    Me.cbTb_base_4.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_4.TabIndex = 51
    Me.cbTb_base_4.ValueMember = ""
    '
    'cbTb_base_3
    '
    Me.cbTb_base_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_3.DataSource = Nothing
    Me.cbTb_base_3.DisplayMember = ""
    Me.cbTb_base_3.Location = New System.Drawing.Point(218, 82)
    Me.cbTb_base_3.Name = "cbTb_base_3"
    Me.cbTb_base_3.NTSDbField = ""
    Me.cbTb_base_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_3.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_3.Properties.AutoHeight = False
    Me.cbTb_base_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_3.Properties.DropDownRows = 30
    Me.cbTb_base_3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_3.SelectedValue = ""
    Me.cbTb_base_3.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_3.TabIndex = 50
    Me.cbTb_base_3.ValueMember = ""
    '
    'cbTb_base_2
    '
    Me.cbTb_base_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_2.DataSource = Nothing
    Me.cbTb_base_2.DisplayMember = ""
    Me.cbTb_base_2.Location = New System.Drawing.Point(218, 60)
    Me.cbTb_base_2.Name = "cbTb_base_2"
    Me.cbTb_base_2.NTSDbField = ""
    Me.cbTb_base_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_2.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_2.Properties.AutoHeight = False
    Me.cbTb_base_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_2.Properties.DropDownRows = 30
    Me.cbTb_base_2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_2.SelectedValue = ""
    Me.cbTb_base_2.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_2.TabIndex = 49
    Me.cbTb_base_2.ValueMember = ""
    '
    'lbDescrPart
    '
    Me.lbDescrPart.AutoSize = True
    Me.lbDescrPart.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrPart.Location = New System.Drawing.Point(379, 20)
    Me.lbDescrPart.Name = "lbDescrPart"
    Me.lbDescrPart.NTSDbField = ""
    Me.lbDescrPart.Size = New System.Drawing.Size(61, 13)
    Me.lbDescrPart.TabIndex = 48
    Me.lbDescrPart.Tag = ""
    Me.lbDescrPart.Text = "Descrizione"
    Me.lbDescrPart.Tooltip = ""
    Me.lbDescrPart.UseMnemonic = False
    '
    'edTb_desp_6
    '
    Me.edTb_desp_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_6.EditValue = ""
    Me.edTb_desp_6.Location = New System.Drawing.Point(374, 148)
    Me.edTb_desp_6.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_6.Name = "edTb_desp_6"
    Me.edTb_desp_6.NTSDbField = ""
    Me.edTb_desp_6.NTSForzaVisZoom = False
    Me.edTb_desp_6.NTSOldValue = ""
    Me.edTb_desp_6.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_6.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_6.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_6.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_6.Properties.AutoHeight = False
    Me.edTb_desp_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_6.Properties.MaxLength = 65536
    Me.edTb_desp_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_6.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_6.TabIndex = 42
    '
    'lbGG
    '
    Me.lbGG.AutoSize = True
    Me.lbGG.BackColor = System.Drawing.Color.Transparent
    Me.lbGG.Location = New System.Drawing.Point(340, 20)
    Me.lbGG.Name = "lbGG"
    Me.lbGG.NTSDbField = ""
    Me.lbGG.Size = New System.Drawing.Size(21, 13)
    Me.lbGG.TabIndex = 47
    Me.lbGG.Tag = ""
    Me.lbGG.Text = "GG"
    Me.lbGG.Tooltip = ""
    Me.lbGG.UseMnemonic = False
    '
    'edTb_desp_5
    '
    Me.edTb_desp_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_5.EditValue = ""
    Me.edTb_desp_5.Location = New System.Drawing.Point(374, 126)
    Me.edTb_desp_5.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_5.Name = "edTb_desp_5"
    Me.edTb_desp_5.NTSDbField = ""
    Me.edTb_desp_5.NTSForzaVisZoom = False
    Me.edTb_desp_5.NTSOldValue = ""
    Me.edTb_desp_5.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_5.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_5.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_5.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_5.Properties.AutoHeight = False
    Me.edTb_desp_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_5.Properties.MaxLength = 65536
    Me.edTb_desp_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_5.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_5.TabIndex = 41
    '
    'edTb_giorni_6
    '
    Me.edTb_giorni_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giorni_6.EditValue = "0"
    Me.edTb_giorni_6.Location = New System.Drawing.Point(337, 148)
    Me.edTb_giorni_6.Name = "edTb_giorni_6"
    Me.edTb_giorni_6.NTSDbField = ""
    Me.edTb_giorni_6.NTSFormat = "0"
    Me.edTb_giorni_6.NTSForzaVisZoom = False
    Me.edTb_giorni_6.NTSOldValue = ""
    Me.edTb_giorni_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_6.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_6.Properties.AutoHeight = False
    Me.edTb_giorni_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_6.Properties.MaxLength = 65536
    Me.edTb_giorni_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_6.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_6.TabIndex = 11
    '
    'edTb_desp_4
    '
    Me.edTb_desp_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_4.EditValue = ""
    Me.edTb_desp_4.Location = New System.Drawing.Point(374, 104)
    Me.edTb_desp_4.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_4.Name = "edTb_desp_4"
    Me.edTb_desp_4.NTSDbField = ""
    Me.edTb_desp_4.NTSForzaVisZoom = False
    Me.edTb_desp_4.NTSOldValue = ""
    Me.edTb_desp_4.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_4.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_4.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_4.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_4.Properties.AutoHeight = False
    Me.edTb_desp_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_4.Properties.MaxLength = 65536
    Me.edTb_desp_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_4.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_4.TabIndex = 40
    '
    'edTb_giorni_5
    '
    Me.edTb_giorni_5.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_giorni_5.EditValue = "0"
    Me.edTb_giorni_5.Location = New System.Drawing.Point(337, 126)
    Me.edTb_giorni_5.Name = "edTb_giorni_5"
    Me.edTb_giorni_5.NTSDbField = ""
    Me.edTb_giorni_5.NTSFormat = "0"
    Me.edTb_giorni_5.NTSForzaVisZoom = False
    Me.edTb_giorni_5.NTSOldValue = ""
    Me.edTb_giorni_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_5.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_5.Properties.AutoHeight = False
    Me.edTb_giorni_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_5.Properties.MaxLength = 65536
    Me.edTb_giorni_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_5.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_5.TabIndex = 10
    '
    'edTb_desp_3
    '
    Me.edTb_desp_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_3.EditValue = ""
    Me.edTb_desp_3.Location = New System.Drawing.Point(374, 82)
    Me.edTb_desp_3.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_3.Name = "edTb_desp_3"
    Me.edTb_desp_3.NTSDbField = ""
    Me.edTb_desp_3.NTSForzaVisZoom = False
    Me.edTb_desp_3.NTSOldValue = ""
    Me.edTb_desp_3.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_3.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_3.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_3.Properties.AutoHeight = False
    Me.edTb_desp_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_3.Properties.MaxLength = 65536
    Me.edTb_desp_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_3.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_3.TabIndex = 39
    '
    'lbBaseQuota
    '
    Me.lbBaseQuota.AutoSize = True
    Me.lbBaseQuota.BackColor = System.Drawing.Color.Transparent
    Me.lbBaseQuota.Location = New System.Drawing.Point(221, 20)
    Me.lbBaseQuota.Name = "lbBaseQuota"
    Me.lbBaseQuota.NTSDbField = ""
    Me.lbBaseQuota.Size = New System.Drawing.Size(61, 13)
    Me.lbBaseQuota.TabIndex = 46
    Me.lbBaseQuota.Tag = ""
    Me.lbBaseQuota.Text = "Base quota"
    Me.lbBaseQuota.Tooltip = ""
    Me.lbBaseQuota.UseMnemonic = False
    '
    'edTb_desp_2
    '
    Me.edTb_desp_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_2.EditValue = ""
    Me.edTb_desp_2.Location = New System.Drawing.Point(374, 60)
    Me.edTb_desp_2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_2.Name = "edTb_desp_2"
    Me.edTb_desp_2.NTSDbField = ""
    Me.edTb_desp_2.NTSForzaVisZoom = False
    Me.edTb_desp_2.NTSOldValue = ""
    Me.edTb_desp_2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_2.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_2.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_2.Properties.AutoHeight = False
    Me.edTb_desp_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_2.Properties.MaxLength = 65536
    Me.edTb_desp_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_2.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_2.TabIndex = 38
    '
    'edTb_giorni_4
    '
    Me.edTb_giorni_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giorni_4.EditValue = "0"
    Me.edTb_giorni_4.Location = New System.Drawing.Point(337, 104)
    Me.edTb_giorni_4.Name = "edTb_giorni_4"
    Me.edTb_giorni_4.NTSDbField = ""
    Me.edTb_giorni_4.NTSFormat = "0"
    Me.edTb_giorni_4.NTSForzaVisZoom = False
    Me.edTb_giorni_4.NTSOldValue = ""
    Me.edTb_giorni_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_4.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_4.Properties.AutoHeight = False
    Me.edTb_giorni_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_4.Properties.MaxLength = 65536
    Me.edTb_giorni_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_4.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_4.TabIndex = 9
    '
    'edTb_desp_1
    '
    Me.edTb_desp_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desp_1.EditValue = ""
    Me.edTb_desp_1.Location = New System.Drawing.Point(374, 38)
    Me.edTb_desp_1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_desp_1.Name = "edTb_desp_1"
    Me.edTb_desp_1.NTSDbField = ""
    Me.edTb_desp_1.NTSForzaVisZoom = False
    Me.edTb_desp_1.NTSOldValue = ""
    Me.edTb_desp_1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_desp_1.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_desp_1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desp_1.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_desp_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desp_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desp_1.Properties.AutoHeight = False
    Me.edTb_desp_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desp_1.Properties.MaxLength = 65536
    Me.edTb_desp_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desp_1.Size = New System.Drawing.Size(245, 20)
    Me.edTb_desp_1.TabIndex = 37
    '
    'cbTb_base_1
    '
    Me.cbTb_base_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_base_1.DataSource = Nothing
    Me.cbTb_base_1.DisplayMember = ""
    Me.cbTb_base_1.Location = New System.Drawing.Point(218, 38)
    Me.cbTb_base_1.Name = "cbTb_base_1"
    Me.cbTb_base_1.NTSDbField = ""
    Me.cbTb_base_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_base_1.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_base_1.Properties.AutoHeight = False
    Me.cbTb_base_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_base_1.Properties.DropDownRows = 30
    Me.cbTb_base_1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_base_1.SelectedValue = ""
    Me.cbTb_base_1.Size = New System.Drawing.Size(113, 20)
    Me.cbTb_base_1.TabIndex = 36
    Me.cbTb_base_1.ValueMember = ""
    '
    'edTb_giorni_3
    '
    Me.edTb_giorni_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giorni_3.EditValue = "0"
    Me.edTb_giorni_3.Location = New System.Drawing.Point(337, 82)
    Me.edTb_giorni_3.Name = "edTb_giorni_3"
    Me.edTb_giorni_3.NTSDbField = ""
    Me.edTb_giorni_3.NTSFormat = "0"
    Me.edTb_giorni_3.NTSForzaVisZoom = False
    Me.edTb_giorni_3.NTSOldValue = ""
    Me.edTb_giorni_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_3.Properties.AutoHeight = False
    Me.edTb_giorni_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_3.Properties.MaxLength = 65536
    Me.edTb_giorni_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_3.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_3.TabIndex = 8
    '
    'lbQuota
    '
    Me.lbQuota.AutoSize = True
    Me.lbQuota.BackColor = System.Drawing.Color.Transparent
    Me.lbQuota.Location = New System.Drawing.Point(159, 20)
    Me.lbQuota.Name = "lbQuota"
    Me.lbQuota.NTSDbField = ""
    Me.lbQuota.Size = New System.Drawing.Size(37, 13)
    Me.lbQuota.TabIndex = 45
    Me.lbQuota.Tag = ""
    Me.lbQuota.Text = "Quota"
    Me.lbQuota.Tooltip = ""
    Me.lbQuota.UseMnemonic = False
    '
    'edTb_giorni_2
    '
    Me.edTb_giorni_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giorni_2.EditValue = "0"
    Me.edTb_giorni_2.Location = New System.Drawing.Point(337, 60)
    Me.edTb_giorni_2.Name = "edTb_giorni_2"
    Me.edTb_giorni_2.NTSDbField = ""
    Me.edTb_giorni_2.NTSFormat = "0"
    Me.edTb_giorni_2.NTSForzaVisZoom = False
    Me.edTb_giorni_2.NTSOldValue = ""
    Me.edTb_giorni_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_2.Properties.AutoHeight = False
    Me.edTb_giorni_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_2.Properties.MaxLength = 65536
    Me.edTb_giorni_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_2.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_2.TabIndex = 7
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(39, 20)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(27, 13)
    Me.lbTipo.TabIndex = 44
    Me.lbTipo.Tag = ""
    Me.lbTipo.Text = "Tipo"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'edTb_giorni_1
    '
    Me.edTb_giorni_1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_giorni_1.EditValue = "0"
    Me.edTb_giorni_1.Location = New System.Drawing.Point(337, 38)
    Me.edTb_giorni_1.Name = "edTb_giorni_1"
    Me.edTb_giorni_1.NTSDbField = ""
    Me.edTb_giorni_1.NTSFormat = "0"
    Me.edTb_giorni_1.NTSForzaVisZoom = False
    Me.edTb_giorni_1.NTSOldValue = ""
    Me.edTb_giorni_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_giorni_1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_giorni_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni_1.Properties.AutoHeight = False
    Me.edTb_giorni_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni_1.Properties.MaxLength = 65536
    Me.edTb_giorni_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni_1.Size = New System.Drawing.Size(28, 20)
    Me.edTb_giorni_1.TabIndex = 6
    '
    'edTb_quota_6
    '
    Me.edTb_quota_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_6.EditValue = "0"
    Me.edTb_quota_6.Location = New System.Drawing.Point(162, 148)
    Me.edTb_quota_6.Name = "edTb_quota_6"
    Me.edTb_quota_6.NTSDbField = ""
    Me.edTb_quota_6.NTSFormat = "0"
    Me.edTb_quota_6.NTSForzaVisZoom = False
    Me.edTb_quota_6.NTSOldValue = ""
    Me.edTb_quota_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_6.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_6.Properties.AutoHeight = False
    Me.edTb_quota_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_6.Properties.MaxLength = 65536
    Me.edTb_quota_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_6.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_6.TabIndex = 20
    '
    'edTb_quota_5
    '
    Me.edTb_quota_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_5.EditValue = "0"
    Me.edTb_quota_5.Location = New System.Drawing.Point(162, 126)
    Me.edTb_quota_5.Name = "edTb_quota_5"
    Me.edTb_quota_5.NTSDbField = ""
    Me.edTb_quota_5.NTSFormat = "0"
    Me.edTb_quota_5.NTSForzaVisZoom = False
    Me.edTb_quota_5.NTSOldValue = ""
    Me.edTb_quota_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_5.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_5.Properties.AutoHeight = False
    Me.edTb_quota_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_5.Properties.MaxLength = 65536
    Me.edTb_quota_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_5.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_5.TabIndex = 19
    '
    'lbRata
    '
    Me.lbRata.AutoSize = True
    Me.lbRata.BackColor = System.Drawing.Color.Transparent
    Me.lbRata.Location = New System.Drawing.Point(5, 20)
    Me.lbRata.Name = "lbRata"
    Me.lbRata.NTSDbField = ""
    Me.lbRata.Size = New System.Drawing.Size(30, 13)
    Me.lbRata.TabIndex = 43
    Me.lbRata.Tag = ""
    Me.lbRata.Text = "Rata"
    Me.lbRata.Tooltip = ""
    Me.lbRata.UseMnemonic = False
    '
    'edTb_quota_4
    '
    Me.edTb_quota_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_4.EditValue = "0"
    Me.edTb_quota_4.Location = New System.Drawing.Point(162, 104)
    Me.edTb_quota_4.Name = "edTb_quota_4"
    Me.edTb_quota_4.NTSDbField = ""
    Me.edTb_quota_4.NTSFormat = "0"
    Me.edTb_quota_4.NTSForzaVisZoom = False
    Me.edTb_quota_4.NTSOldValue = ""
    Me.edTb_quota_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_4.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_4.Properties.AutoHeight = False
    Me.edTb_quota_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_4.Properties.MaxLength = 65536
    Me.edTb_quota_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_4.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_4.TabIndex = 18
    '
    'lbSei
    '
    Me.lbSei.AutoSize = True
    Me.lbSei.BackColor = System.Drawing.Color.Transparent
    Me.lbSei.Location = New System.Drawing.Point(14, 151)
    Me.lbSei.Name = "lbSei"
    Me.lbSei.NTSDbField = ""
    Me.lbSei.Size = New System.Drawing.Size(13, 13)
    Me.lbSei.TabIndex = 42
    Me.lbSei.Tag = ""
    Me.lbSei.Text = "6"
    Me.lbSei.Tooltip = ""
    Me.lbSei.UseMnemonic = False
    '
    'edTb_quota_3
    '
    Me.edTb_quota_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_3.EditValue = "0"
    Me.edTb_quota_3.Location = New System.Drawing.Point(162, 82)
    Me.edTb_quota_3.Name = "edTb_quota_3"
    Me.edTb_quota_3.NTSDbField = ""
    Me.edTb_quota_3.NTSFormat = "0"
    Me.edTb_quota_3.NTSForzaVisZoom = False
    Me.edTb_quota_3.NTSOldValue = ""
    Me.edTb_quota_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_3.Properties.AutoHeight = False
    Me.edTb_quota_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_3.Properties.MaxLength = 65536
    Me.edTb_quota_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_3.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_3.TabIndex = 17
    '
    'lbCinque
    '
    Me.lbCinque.AutoSize = True
    Me.lbCinque.BackColor = System.Drawing.Color.Transparent
    Me.lbCinque.Location = New System.Drawing.Point(14, 129)
    Me.lbCinque.Name = "lbCinque"
    Me.lbCinque.NTSDbField = ""
    Me.lbCinque.Size = New System.Drawing.Size(13, 13)
    Me.lbCinque.TabIndex = 41
    Me.lbCinque.Tag = ""
    Me.lbCinque.Text = "5"
    Me.lbCinque.Tooltip = ""
    Me.lbCinque.UseMnemonic = False
    '
    'edTb_quota_2
    '
    Me.edTb_quota_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_2.EditValue = "0"
    Me.edTb_quota_2.Location = New System.Drawing.Point(162, 60)
    Me.edTb_quota_2.Name = "edTb_quota_2"
    Me.edTb_quota_2.NTSDbField = ""
    Me.edTb_quota_2.NTSFormat = "0"
    Me.edTb_quota_2.NTSForzaVisZoom = False
    Me.edTb_quota_2.NTSOldValue = ""
    Me.edTb_quota_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_2.Properties.AutoHeight = False
    Me.edTb_quota_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_2.Properties.MaxLength = 65536
    Me.edTb_quota_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_2.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_2.TabIndex = 16
    '
    'edTb_quota_1
    '
    Me.edTb_quota_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_quota_1.EditValue = "0"
    Me.edTb_quota_1.Location = New System.Drawing.Point(162, 38)
    Me.edTb_quota_1.Name = "edTb_quota_1"
    Me.edTb_quota_1.NTSDbField = ""
    Me.edTb_quota_1.NTSFormat = "0"
    Me.edTb_quota_1.NTSForzaVisZoom = False
    Me.edTb_quota_1.NTSOldValue = ""
    Me.edTb_quota_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_quota_1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_quota_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_quota_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_quota_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_quota_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_quota_1.Properties.AutoHeight = False
    Me.edTb_quota_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_quota_1.Properties.MaxLength = 65536
    Me.edTb_quota_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_quota_1.Size = New System.Drawing.Size(50, 20)
    Me.edTb_quota_1.TabIndex = 15
    '
    'lbQuattro
    '
    Me.lbQuattro.AutoSize = True
    Me.lbQuattro.BackColor = System.Drawing.Color.Transparent
    Me.lbQuattro.Location = New System.Drawing.Point(14, 107)
    Me.lbQuattro.Name = "lbQuattro"
    Me.lbQuattro.NTSDbField = ""
    Me.lbQuattro.Size = New System.Drawing.Size(13, 13)
    Me.lbQuattro.TabIndex = 40
    Me.lbQuattro.Tag = ""
    Me.lbQuattro.Text = "4"
    Me.lbQuattro.Tooltip = ""
    Me.lbQuattro.UseMnemonic = False
    '
    'lbTre
    '
    Me.lbTre.AutoSize = True
    Me.lbTre.BackColor = System.Drawing.Color.Transparent
    Me.lbTre.Location = New System.Drawing.Point(14, 85)
    Me.lbTre.Name = "lbTre"
    Me.lbTre.NTSDbField = ""
    Me.lbTre.Size = New System.Drawing.Size(13, 13)
    Me.lbTre.TabIndex = 39
    Me.lbTre.Tag = ""
    Me.lbTre.Text = "3"
    Me.lbTre.Tooltip = ""
    Me.lbTre.UseMnemonic = False
    '
    'lbDue
    '
    Me.lbDue.AutoSize = True
    Me.lbDue.BackColor = System.Drawing.Color.Transparent
    Me.lbDue.Location = New System.Drawing.Point(14, 63)
    Me.lbDue.Name = "lbDue"
    Me.lbDue.NTSDbField = ""
    Me.lbDue.Size = New System.Drawing.Size(13, 13)
    Me.lbDue.TabIndex = 38
    Me.lbDue.Tag = ""
    Me.lbDue.Text = "2"
    Me.lbDue.Tooltip = ""
    Me.lbDue.UseMnemonic = False
    '
    'lbUno
    '
    Me.lbUno.AutoSize = True
    Me.lbUno.BackColor = System.Drawing.Color.Transparent
    Me.lbUno.Location = New System.Drawing.Point(14, 41)
    Me.lbUno.Name = "lbUno"
    Me.lbUno.NTSDbField = ""
    Me.lbUno.Size = New System.Drawing.Size(13, 13)
    Me.lbUno.TabIndex = 37
    Me.lbUno.Tag = ""
    Me.lbUno.Text = "1"
    Me.lbUno.Tooltip = ""
    Me.lbUno.UseMnemonic = False
    '
    'ckTb_flcondp
    '
    Me.ckTb_flcondp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_flcondp.Location = New System.Drawing.Point(6, 1)
    Me.ckTb_flcondp.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.ckTb_flcondp.Name = "ckTb_flcondp"
    Me.ckTb_flcondp.NTSCheckValue = "S"
    Me.ckTb_flcondp.NTSUnCheckValue = "N"
    Me.ckTb_flcondp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_flcondp.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_flcondp.Properties.AutoHeight = False
    Me.ckTb_flcondp.Properties.Caption = "Condizioni particolari:"
    Me.ckTb_flcondp.Size = New System.Drawing.Size(125, 19)
    Me.ckTb_flcondp.TabIndex = 0
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbDesLingua, Me.tlbCalcolaScadenze, Me.tlbImpostaStampante, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
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
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDesLingua), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCalcolaScadenze), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbDesLingua
    '
    Me.tlbDesLingua.Caption = "Descrizioni in lingua"
    Me.tlbDesLingua.GlyphPath = ""
    Me.tlbDesLingua.Id = 23
    Me.tlbDesLingua.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDesLingua.Name = "tlbDesLingua"
    Me.tlbDesLingua.Visible = True
    '
    'tlbCalcolaScadenze
    '
    Me.tlbCalcolaScadenze.Caption = "Calcola scadenze"
    Me.tlbCalcolaScadenze.GlyphPath = ""
    Me.tlbCalcolaScadenze.Id = 24
    Me.tlbCalcolaScadenze.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbCalcolaScadenze.Name = "tlbCalcolaScadenze"
    Me.tlbCalcolaScadenze.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbTb_tippaga
    '
    Me.lbTb_tippaga.AutoSize = True
    Me.lbTb_tippaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tippaga.Location = New System.Drawing.Point(12, 81)
    Me.lbTb_tippaga.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_tippaga.Name = "lbTb_tippaga"
    Me.lbTb_tippaga.NTSDbField = ""
    Me.lbTb_tippaga.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_tippaga.TabIndex = 36
    Me.lbTb_tippaga.Text = "Tipo pagamento"
    Me.lbTb_tippaga.Tooltip = ""
    Me.lbTb_tippaga.UseMnemonic = False
    '
    'lbTb_decpaga
    '
    Me.lbTb_decpaga.AutoSize = True
    Me.lbTb_decpaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_decpaga.Location = New System.Drawing.Point(12, 103)
    Me.lbTb_decpaga.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_decpaga.Name = "lbTb_decpaga"
    Me.lbTb_decpaga.NTSDbField = ""
    Me.lbTb_decpaga.Size = New System.Drawing.Size(75, 13)
    Me.lbTb_decpaga.TabIndex = 37
    Me.lbTb_decpaga.Text = "Tipo scadenza"
    Me.lbTb_decpaga.Tooltip = ""
    Me.lbTb_decpaga.UseMnemonic = False
    '
    'lbTb_tipincecr
    '
    Me.lbTb_tipincecr.AutoSize = True
    Me.lbTb_tipincecr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipincecr.Location = New System.Drawing.Point(12, 237)
    Me.lbTb_tipincecr.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_tipincecr.Name = "lbTb_tipincecr"
    Me.lbTb_tipincecr.NTSDbField = ""
    Me.lbTb_tipincecr.Size = New System.Drawing.Size(65, 13)
    Me.lbTb_tipincecr.TabIndex = 38
    Me.lbTb_tipincecr.Text = "Tipo incasso"
    Me.lbTb_tipincecr.Tooltip = ""
    Me.lbTb_tipincecr.UseMnemonic = False
    '
    'lbTb_prirata
    '
    Me.lbTb_prirata.AutoSize = True
    Me.lbTb_prirata.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_prirata.Location = New System.Drawing.Point(317, 81)
    Me.lbTb_prirata.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_prirata.Name = "lbTb_prirata"
    Me.lbTb_prirata.NTSDbField = ""
    Me.lbTb_prirata.Size = New System.Drawing.Size(92, 13)
    Me.lbTb_prirata.TabIndex = 39
    Me.lbTb_prirata.Text = "Eccezione 1a rata"
    Me.lbTb_prirata.Tooltip = ""
    Me.lbTb_prirata.UseMnemonic = False
    '
    'lbTb_codstpg
    '
    Me.lbTb_codstpg.AutoSize = True
    Me.lbTb_codstpg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codstpg.Location = New System.Drawing.Point(12, 259)
    Me.lbTb_codstpg.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_codstpg.Name = "lbTb_codstpg"
    Me.lbTb_codstpg.NTSDbField = ""
    Me.lbTb_codstpg.Size = New System.Drawing.Size(108, 13)
    Me.lbTb_codstpg.TabIndex = 40
    Me.lbTb_codstpg.Text = "Sottotipo pagamento"
    Me.lbTb_codstpg.Tooltip = ""
    Me.lbTb_codstpg.UseMnemonic = False
    '
    'edTb_codstpg
    '
    Me.edTb_codstpg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codstpg.EditValue = "0"
    Me.edTb_codstpg.Location = New System.Drawing.Point(141, 256)
    Me.edTb_codstpg.Name = "edTb_codstpg"
    Me.edTb_codstpg.NTSDbField = ""
    Me.edTb_codstpg.NTSFormat = "0"
    Me.edTb_codstpg.NTSForzaVisZoom = False
    Me.edTb_codstpg.NTSOldValue = ""
    Me.edTb_codstpg.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codstpg.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codstpg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codstpg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codstpg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codstpg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codstpg.Properties.AutoHeight = False
    Me.edTb_codstpg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codstpg.Properties.MaxLength = 65536
    Me.edTb_codstpg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codstpg.Size = New System.Drawing.Size(48, 20)
    Me.edTb_codstpg.TabIndex = 41
    '
    'lbXx_codtpsg
    '
    Me.lbXx_codtpsg.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtpsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtpsg.Location = New System.Drawing.Point(191, 256)
    Me.lbXx_codtpsg.Name = "lbXx_codtpsg"
    Me.lbXx_codtpsg.NTSDbField = ""
    Me.lbXx_codtpsg.Size = New System.Drawing.Size(447, 20)
    Me.lbXx_codtpsg.TabIndex = 42
    Me.lbXx_codtpsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codtpsg.Tooltip = ""
    Me.lbXx_codtpsg.UseMnemonic = False
    '
    'lbTb_codmodi
    '
    Me.lbTb_codmodi.AutoSize = True
    Me.lbTb_codmodi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codmodi.Location = New System.Drawing.Point(12, 281)
    Me.lbTb_codmodi.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_codmodi.Name = "lbTb_codmodi"
    Me.lbTb_codmodi.NTSDbField = ""
    Me.lbTb_codmodi.Size = New System.Drawing.Size(113, 13)
    Me.lbTb_codmodi.TabIndex = 43
    Me.lbTb_codmodi.Text = "Mod. incasso intrastat"
    Me.lbTb_codmodi.Tooltip = ""
    Me.lbTb_codmodi.UseMnemonic = False
    '
    'edTb_codmodi
    '
    Me.edTb_codmodi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codmodi.EditValue = " "
    Me.edTb_codmodi.Location = New System.Drawing.Point(141, 278)
    Me.edTb_codmodi.Margin = New System.Windows.Forms.Padding(3, 0, 3, 2)
    Me.edTb_codmodi.Name = "edTb_codmodi"
    Me.edTb_codmodi.NTSDbField = ""
    Me.edTb_codmodi.NTSForzaVisZoom = False
    Me.edTb_codmodi.NTSOldValue = " "
    Me.edTb_codmodi.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTb_codmodi.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
    Me.edTb_codmodi.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codmodi.Properties.Appearance.Options.UseForeColor = True
    Me.edTb_codmodi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codmodi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codmodi.Properties.AutoHeight = False
    Me.edTb_codmodi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codmodi.Properties.MaxLength = 65536
    Me.edTb_codmodi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codmodi.Size = New System.Drawing.Size(48, 20)
    Me.edTb_codmodi.TabIndex = 44
    '
    'lbXx_codmodi
    '
    Me.lbXx_codmodi.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codmodi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codmodi.Location = New System.Drawing.Point(191, 278)
    Me.lbXx_codmodi.Name = "lbXx_codmodi"
    Me.lbXx_codmodi.NTSDbField = ""
    Me.lbXx_codmodi.Size = New System.Drawing.Size(447, 20)
    Me.lbXx_codmodi.TabIndex = 45
    Me.lbXx_codmodi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codmodi.Tooltip = ""
    Me.lbXx_codmodi.UseMnemonic = False
    '
    'cbTb_ggcalend
    '
    Me.cbTb_ggcalend.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_ggcalend.DataSource = Nothing
    Me.cbTb_ggcalend.DisplayMember = ""
    Me.cbTb_ggcalend.Location = New System.Drawing.Point(421, 100)
    Me.cbTb_ggcalend.Name = "cbTb_ggcalend"
    Me.cbTb_ggcalend.NTSDbField = ""
    Me.cbTb_ggcalend.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_ggcalend.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_ggcalend.Properties.AutoHeight = False
    Me.cbTb_ggcalend.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_ggcalend.Properties.DropDownRows = 30
    Me.cbTb_ggcalend.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_ggcalend.SelectedValue = ""
    Me.cbTb_ggcalend.Size = New System.Drawing.Size(214, 20)
    Me.cbTb_ggcalend.TabIndex = 47
    Me.cbTb_ggcalend.ValueMember = ""
    '
    'lbTb_ggcalend
    '
    Me.lbTb_ggcalend.AutoSize = True
    Me.lbTb_ggcalend.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ggcalend.Location = New System.Drawing.Point(317, 103)
    Me.lbTb_ggcalend.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbTb_ggcalend.Name = "lbTb_ggcalend"
    Me.lbTb_ggcalend.NTSDbField = ""
    Me.lbTb_ggcalend.Size = New System.Drawing.Size(105, 13)
    Me.lbTb_ggcalend.TabIndex = 48
    Me.lbTb_ggcalend.Text = "Scadenza max entro"
    Me.lbTb_ggcalend.Tooltip = ""
    Me.lbTb_ggcalend.UseMnemonic = False
    '
    'ckTb_webvis
    '
    Me.ckTb_webvis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_webvis.Location = New System.Drawing.Point(395, 299)
    Me.ckTb_webvis.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.ckTb_webvis.Name = "ckTb_webvis"
    Me.ckTb_webvis.NTSCheckValue = "S"
    Me.ckTb_webvis.NTSUnCheckValue = "N"
    Me.ckTb_webvis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_webvis.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_webvis.Properties.AutoHeight = False
    Me.ckTb_webvis.Properties.Caption = "Pagamento visibile dall'applicazione esterna"
    Me.ckTb_webvis.Size = New System.Drawing.Size(238, 19)
    Me.ckTb_webvis.TabIndex = 49
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 26
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'FRM__PAGA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(653, 497)
    Me.Controls.Add(Me.ckTb_webvis)
    Me.Controls.Add(Me.cbTb_ggcalend)
    Me.Controls.Add(Me.lbTb_ggcalend)
    Me.Controls.Add(Me.lbXx_codmodi)
    Me.Controls.Add(Me.edTb_codmodi)
    Me.Controls.Add(Me.lbTb_codmodi)
    Me.Controls.Add(Me.lbXx_codtpsg)
    Me.Controls.Add(Me.edTb_codstpg)
    Me.Controls.Add(Me.lbTb_codstpg)
    Me.Controls.Add(Me.lbTb_prirata)
    Me.Controls.Add(Me.lbTb_tipincecr)
    Me.Controls.Add(Me.lbTb_decpaga)
    Me.Controls.Add(Me.lbTb_tippaga)
    Me.Controls.Add(Me.fmCondPart)
    Me.Controls.Add(Me.edTb_concassp)
    Me.Controls.Add(Me.cbTb_tipincecr)
    Me.Controls.Add(Me.edTb_giofiss)
    Me.Controls.Add(Me.lbTb_giofiss)
    Me.Controls.Add(Me.cbTb_prirata)
    Me.Controls.Add(Me.edTb_mesees2)
    Me.Controls.Add(Me.edTb_mesees1)
    Me.Controls.Add(Me.lbTb_mesees)
    Me.Controls.Add(Me.ckTb_shiftm)
    Me.Controls.Add(Me.edTb_despaga)
    Me.Controls.Add(Me.edTb_datafdec)
    Me.Controls.Add(Me.cbTb_decpaga)
    Me.Controls.Add(Me.cbTb_tippaga)
    Me.Controls.Add(Me.lbTb_datafdec)
    Me.Controls.Add(Me.lbXx_concassp)
    Me.Controls.Add(Me.lbTb_concassp)
    Me.Controls.Add(Me.edTb_speinca)
    Me.Controls.Add(Me.edTb_scopaga)
    Me.Controls.Add(Me.lbTb_speinca)
    Me.Controls.Add(Me.lbTb_scopaga)
    Me.Controls.Add(Me.lbTb_gioperi)
    Me.Controls.Add(Me.lbTb_gioiniz2)
    Me.Controls.Add(Me.lbTb_gioiniz)
    Me.Controls.Add(Me.edTb_gioiniz2)
    Me.Controls.Add(Me.edTb_gioperi)
    Me.Controls.Add(Me.edTb_gioiniz)
    Me.Controls.Add(Me.lbTb_numrate)
    Me.Controls.Add(Me.edTb_numrate)
    Me.Controls.Add(Me.lbTb_despaga)
    Me.Controls.Add(Me.lbTb_codpaga)
    Me.Controls.Add(Me.edTb_codpaga)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__PAGA"
    Me.Text = "FORME DI PAGAMENTO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codpaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_numrate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_gioiniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_gioperi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_gioiniz2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_scopaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_speinca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_concassp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tippaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_decpaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datafdec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_despaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_shiftm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_mesees1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_mesees2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_prirata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giofiss.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipincecr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCondPart, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCondPart.ResumeLayout(False)
    Me.fmCondPart.PerformLayout()
    CType(Me.cbTb_tipp_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipp_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipp_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipp_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipp_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tipp_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desp_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_base_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_quota_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_flcondp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codstpg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codmodi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_ggcalend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_webvis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edTb_codpaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023476167713, "Codice pagamento"), tabpaga)
      edTb_despaga.NTSSetParam(oMenu, oApp.Tr(Me, 128230023476323886, "Descrizione pagamento"), 50, False)
      edTb_datafdec.NTSSetParam(oMenu, oApp.Tr(Me, 128230023476480059, "Data fissa di scadenza"), True)
      edTb_numrate.NTSSetParam(oMenu, oApp.Tr(Me, 128230023476636232, "Numero delle rate"), "0", 2, 0, 60)
      edTb_gioiniz.NTSSetParam(oMenu, oApp.Tr(Me, 128230023476792405, "Prima rata"), "0", 3, 0, 999)
      edTb_gioiniz2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023476948578, "Seconda rata"), "0", 3, 0, 999)
      edTb_gioperi.NTSSetParam(oMenu, oApp.Tr(Me, 128230023477104751, "Periodicità"), "0", 3, 0, 999)
      edTb_scopaga.NTSSetParam(oMenu, oApp.Tr(Me, 128230023477260924, "Sconto pagamento"), "0.00", 5, -100, 100)
      edTb_speinca.NTSSetParam(oMenu, oApp.Tr(Me, 128230023477417097, "Spese incasso"), oApp.FormatImporti, 13, 0, 9999999999999)
      edTb_concassp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023477573270, "Contropartita vendite/acquisti"), tabcove)
      edTb_mesees1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023477729443, "Primo mese escluso"), "0", 2, 0, 12)
      edTb_mesees2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023477885616, "Secondo mese escluso"), "0", 2, 0, 12)
      edTb_giofiss.NTSSetParam(oMenu, oApp.Tr(Me, 128230023478041789, "Giorno fisso"), "0", 2, 0, 31)
      edTb_codstpg.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129436398215341834, "Sottotipo pagamento"), tabstpg)
      edTb_codmodi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129453723171660157, "Modalità di incasso intrastat"), tabmodi, False)
      cbTb_ggcalend.NTSSetParam(oApp.Tr(Me, 129985769231602268, "data scadenza massima (art. 62/2012)"))

      ckTb_shiftm.NTSSetParam(oMenu, oApp.Tr(Me, 128230023478197962, "Sposta rate successive"), "S", "N")
      ckTb_flcondp.NTSSetParam(oMenu, oApp.Tr(Me, 128230023478354135, "Condizioni particolari"), "S", "N")
      ckTb_webvis.NTSSetParam(oMenu, oApp.Tr(Me, 130415277012067812, "Pagamento visibile dall'applicazione esterna"), "S", "N")

      cbTb_tipincecr.NTSSetParam(oApp.Tr(Me, 128230023478510308, "Tipo incasso"))
      cbTb_prirata.NTSSetParam(oApp.Tr(Me, 128230023478666481, "Eccezione prima rata"))
      cbTb_decpaga.NTSSetParam(oApp.Tr(Me, 128230023478822654, "Tipo scadenza"))
      cbTb_tippaga.NTSSetParam(oApp.Tr(Me, 128230023478978827, "Tipo pagamento"))

      cbTb_tipp_6.NTSSetParam(oApp.Tr(Me, 128230023479135000, "Tipo rata 6"))
      cbTb_tipp_5.NTSSetParam(oApp.Tr(Me, 128230023479291173, "Tipo rata 5"))
      cbTb_tipp_4.NTSSetParam(oApp.Tr(Me, 128230023479447346, "Tipo rata 4"))
      cbTb_tipp_3.NTSSetParam(oApp.Tr(Me, 128230023479603519, "Tipo rata 3"))
      cbTb_tipp_2.NTSSetParam(oApp.Tr(Me, 128230023479759692, "Tipo rata 2"))
      cbTb_tipp_1.NTSSetParam(oApp.Tr(Me, 128230023479915865, "Tipo rata 1"))
      cbTb_base_6.NTSSetParam(oApp.Tr(Me, 128230023480072038, "Base quota rata 6"))
      cbTb_base_5.NTSSetParam(oApp.Tr(Me, 128230023480228211, "Base quota rata 5"))
      cbTb_base_4.NTSSetParam(oApp.Tr(Me, 128230023480384384, "Base quota rata 4"))
      cbTb_base_3.NTSSetParam(oApp.Tr(Me, 128230023480540557, "Base quota rata 3"))
      cbTb_base_2.NTSSetParam(oApp.Tr(Me, 128230023480696730, "Base quota rata 2"))
      cbTb_base_1.NTSSetParam(oApp.Tr(Me, 128230023480852903, "Base quota rata 1"))
      edTb_desp_6.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481009076, "Descriz. rata 6"), 30, True)
      edTb_desp_5.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481165249, "Descriz. rata 5"), 30, True)
      edTb_desp_4.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481321422, "Descriz. rata 4"), 30, True)
      edTb_desp_3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481477595, "Descriz. rata 3"), 30, True)
      edTb_desp_2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481633768, "Descriz. rata 2"), 30, True)
      edTb_desp_1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481789941, "Descriz. rata 1"), 30, True)
      edTb_giorni_6.NTSSetParam(oMenu, oApp.Tr(Me, 128230023481946114, "Giorni rata 6"), "0", 3, 0, 999)
      edTb_giorni_5.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482102287, "Giorni rata 5"), "0", 3, 0, 999)
      edTb_giorni_4.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482258460, "Giorni rata 4"), "0", 3, 0, 999)
      edTb_giorni_3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482414633, "Giorni rata 3"), "0", 3, 0, 999)
      edTb_giorni_2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482570806, "Giorni rata 2"), "0", 3, 0, 999)
      edTb_giorni_1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482726979, "Giorni rata 1"), "0", 3, 0, 999)
      edTb_quota_6.NTSSetParam(oMenu, oApp.Tr(Me, 128230023482883152, "Quota rata 6"), oApp.FormatSconti, 6, 0, 100)
      edTb_quota_5.NTSSetParam(oMenu, oApp.Tr(Me, 128230023483039325, "Quota rata 5"), oApp.FormatSconti, 6, 0, 100)
      edTb_quota_4.NTSSetParam(oMenu, oApp.Tr(Me, 128230023483195498, "Quota rata 4"), oApp.FormatSconti, 6, 0, 100)
      edTb_quota_3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023483351671, "Quota rata 3"), oApp.FormatSconti, 6, 0, 100)
      edTb_quota_2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023483507844, "Quota rata 2"), oApp.FormatSconti, 6, 0, 100)
      edTb_quota_1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023483664017, "Quota rata 1"), oApp.FormatSconti, 6, 0, 100)

      'non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!
      edTb_despaga.NTSSetParamZoom("")
      edTb_despaga.NTSForzaVisZoom = True
      edTb_concassp.NTSForzaVisZoom = True

      edTb_codpaga.NTSForzaVisZoom = False
      edTb_codpaga.NTSSetRichiesto()
      edTb_despaga.NTSSetRichiesto()

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__PAGA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()

      '--------------------------------------------------------------------------------------------------------------
      If Not oClePaga.Apri(DittaCorrente, dsPaga) Then
        Me.Close()
        Return
      End If

      dcPaga.DataSource = dsPaga.Tables("TABPAGA")
      dsPaga.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If dsPaga.Tables("TABPAGA").Rows.Count > 0 Then edTb_codpaga.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      If dsPaga.Tables("TABPAGA").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcPaga.ResetBindings(False)
      dcPaga.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      ckTb_flcondp_CheckedChanged(ckTb_flcondp, Nothing)
      cbTb_decpaga_SelectedValueChanged(cbTb_decpaga, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      If (CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC)) And (CLN__STD.FRIENDLY = False) Then
      Else
        ckTb_webvis.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsPaga.Tables("TABPAGA").Rows.Count = 1 And dsPaga.Tables("TABPAGA").Rows(0).RowState = DataRowState.Added Then
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          Dim lPos As Integer = dcPaga.Find("tb_codpaga", Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          If lPos >= 0 Then dcPaga.Position = lPos
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__PAGA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__PAGA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcPaga.Dispose()
      dsPaga.Dispose()
    Catch
    End Try
  End Sub
#End Region



#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      oClePaga.Nuovo()
      dcPaga.MoveLast()
      edTb_codpaga.Enabled = True
      edTb_codpaga.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim frmDuar As FRM__DUAR = Nothing
    Dim strCodOld As String = ""
    Try
      If Not Salva() Then Return

      'test su riga valida
      If edTb_codpaga.Text = "0" Or edTb_codpaga.Text = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130723482390952496, "Posizionarsi su un record valido"))
        Return
      Else
        strCodOld = edTb_codpaga.Text
      End If

      frmDuar = CType(NTSNewFormModal("FRM__DUAR"), FRM__DUAR)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oClePaga = oClePaga
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or frmDuar.strCodDupl = "" Then
        'annullato
        Return
      End If

      If Not oClePaga.Duplica(frmDuar.strCodDupl, strCodOld, frmDuar.bLing) Then
        Return
      End If

      dcPaga.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDuar Is Nothing Then frmDuar.Dispose()
      frmDuar = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '-------------------------------------------------
      'prima di salvare simulo una lostfocus del campo su cui mi trovo, altrimenti potrei salvare un dato non corretto
      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oClePaga.IsCodpagaDeletable(NTSCInt(edTb_codpaga.Text)) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952085312500, "Cancellare la forma di pagamento?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsPaga.Tables("TABPAGA").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcPaga.RemoveAt(dcPaga.Position)
          oClePaga.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcPaga, Me)
            bRemovBinding = False
            edTb_codpaga.Enabled = True
          Else
            edTb_codpaga.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcPaga, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952835468750, "Ripristinare le modifiche apportate al pagamento?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsPaga.Tables("TABPAGA").Rows.Count = 1 And dsPaga.Tables("TABPAGA").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oClePaga.Ripristina(dcPaga.Position, dcPaga.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcPaga, Me)
            bRemovBinding = False
            edTb_codpaga.Enabled = True
          Else
            edTb_codpaga.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcPaga, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
      Dim i As Integer = 0
      If edTb_despaga.Focused Then
        'lancio lo zoom solo per riposizionare il BindingSource
        Dim oParam As New CLE__PATB

        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!
        If oClePaga.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 127791222149531250, "Salvare o ripristinare le modifiche prima di selezionare una nuova forma di pagamento"))
          Return
        End If

        NTSZOOM.strIn = edTb_codpaga.Text
        NTSZOOM.ZoomStrIn("ZOOMTABPAGA", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codpaga.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcPaga.List.Count - 1
            If CType(dcPaga.Item(i), DataRowView)!tb_codpaga.ToString = NTSZOOM.strIn Then
              dcPaga.Position = i
              Exit For
            End If
          Next
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcPaga.MoveFirst()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If Not Salva() Then Return
      dcPaga.MovePrevious()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If Not Salva() Then Return
      dcPaga.MoveNext()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If Not Salva() Then Return
      dcPaga.MoveLast()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDesLingua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDesLingua.ItemClick
    Dim frmPali As FRM__PALI = Nothing
    Try
      If Not Salva() Then Return

      oClePaga.lCodPaga = NTSCInt(edTb_codpaga.Text)
      frmPali = CType(NTSNewFormModal("FRM__PALI"), FRM__PALI)
      If Not frmPali.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmPali.InitEntity(oClePaga)
      frmPali.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      'senza le due righe sotto la form non viene scaricata dalla memoria
      If Not frmPali Is Nothing Then frmPali.Dispose()
      frmPali = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCalcolaScadenze_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCalcolaScadenze.ItemClick
    Dim frmTest As FRM__TEST = Nothing
    Try
      If Not Salva() Then Return

      frmTest = CType(NTSNewFormModal("FRM__TEST"), FRM__TEST)
      frmTest.Init(oMenu, Nothing, DittaCorrente)
      frmTest.oClePaga = oClePaga
      frmTest.dttTipscad = CType(cbTb_tippaga.DataSource, DataTable)
      frmTest.nCodpaga = NTSCInt(edTb_codpaga.Text)
      frmTest.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      'senza le due righe sotto la form non viene scaricata dalla memoria
      If Not frmTest Is Nothing Then frmTest.Dispose()
      frmTest = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckTb_flcondp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTb_flcondp.CheckedChanged
    Try
      If ckTb_flcondp.Checked Then
        GctlSetVisEnab(cbTb_base_1, False)
        GctlSetVisEnab(cbTb_base_2, False)
        GctlSetVisEnab(cbTb_base_3, False)
        GctlSetVisEnab(cbTb_base_4, False)
        GctlSetVisEnab(cbTb_base_5, False)
        GctlSetVisEnab(cbTb_base_6, False)
        GctlSetVisEnab(cbTb_tipp_1, False)
        GctlSetVisEnab(cbTb_tipp_2, False)
        GctlSetVisEnab(cbTb_tipp_3, False)
        GctlSetVisEnab(cbTb_tipp_4, False)
        GctlSetVisEnab(cbTb_tipp_5, False)
        GctlSetVisEnab(cbTb_tipp_6, False)
        GctlSetVisEnab(edTb_desp_1, False)
        GctlSetVisEnab(edTb_desp_2, False)
        GctlSetVisEnab(edTb_desp_3, False)
        GctlSetVisEnab(edTb_desp_4, False)
        GctlSetVisEnab(edTb_desp_5, False)
        GctlSetVisEnab(edTb_desp_6, False)
        GctlSetVisEnab(edTb_giorni_1, False)
        GctlSetVisEnab(edTb_giorni_2, False)
        GctlSetVisEnab(edTb_giorni_3, False)
        GctlSetVisEnab(edTb_giorni_4, False)
        GctlSetVisEnab(edTb_giorni_5, False)
        GctlSetVisEnab(edTb_giorni_6, False)
        GctlSetVisEnab(edTb_quota_1, False)
        GctlSetVisEnab(edTb_quota_2, False)
        GctlSetVisEnab(edTb_quota_3, False)
        GctlSetVisEnab(edTb_quota_4, False)
        GctlSetVisEnab(edTb_quota_5, False)
        GctlSetVisEnab(edTb_quota_6, False)
      Else
        cbTb_base_1.Enabled = False
        cbTb_base_2.Enabled = False
        cbTb_base_3.Enabled = False
        cbTb_base_4.Enabled = False
        cbTb_base_5.Enabled = False
        cbTb_base_6.Enabled = False
        cbTb_tipp_1.Enabled = False
        cbTb_tipp_2.Enabled = False
        cbTb_tipp_3.Enabled = False
        cbTb_tipp_4.Enabled = False
        cbTb_tipp_5.Enabled = False
        cbTb_tipp_6.Enabled = False
        edTb_desp_1.Enabled = False
        edTb_desp_2.Enabled = False
        edTb_desp_3.Enabled = False
        edTb_desp_4.Enabled = False
        edTb_desp_5.Enabled = False
        edTb_desp_6.Enabled = False
        edTb_giorni_1.Enabled = False
        edTb_giorni_2.Enabled = False
        edTb_giorni_3.Enabled = False
        edTb_giorni_4.Enabled = False
        edTb_giorni_5.Enabled = False
        edTb_giorni_6.Enabled = False
        edTb_quota_1.Enabled = False
        edTb_quota_2.Enabled = False
        edTb_quota_3.Enabled = False
        edTb_quota_4.Enabled = False
        edTb_quota_5.Enabled = False
        edTb_quota_6.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbTb_decpaga_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_decpaga.SelectedValueChanged
    Try
      If NTSCInt(cbTb_decpaga.SelectedValue) = 6 Then
        GctlSetVisEnab(edTb_datafdec, False)
      Else
        edTb_datafdec.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edTb_codpaga.NTSDbField = "tabpaga.tb_codpaga"
      edTb_despaga.NTSDbField = "tabpaga.tb_despaga"
      edTb_datafdec.NTSDbField = "tabpaga.tb_datafdec"
      edTb_numrate.NTSDbField = "tabpaga.tb_numrate"
      edTb_gioperi.NTSDbField = "tabpaga.tb_gioperi"
      edTb_concassp.NTSDbField = "tabpaga.tb_concassp"
      edTb_gioiniz.NTSDbField = "tabpaga.tb_gioiniz"
      edTb_gioiniz2.NTSDbField = "tabpaga.tb_gioiniz2"
      edTb_scopaga.NTSDbField = "tabpaga.tb_scopaga"
      edTb_speinca.NTSDbField = "tabpaga.tb_speinca"
      edTb_mesees1.NTSDbField = "tabpaga.tb_mesees1"
      edTb_mesees2.NTSDbField = "tabpaga.tb_mesees2"
      edTb_giofiss.NTSDbField = "tabpaga.tb_giofiss"
      edTb_codstpg.NTSDbField = "tabpaga.tb_codstpg"
      edTb_codmodi.NTSDbField = "tabpaga.tb_codmodi"

      cbTb_tippaga.NTSDbField = "tabpaga.tb_tippaga"
      cbTb_decpaga.NTSDbField = "tabpaga.tb_decpaga"
      cbTb_tipincecr.NTSDbField = "tabpaga.tb_tipincecr"
      cbTb_prirata.NTSDbField = "tabpaga.tb_prirata"

      cbTb_ggcalend.NTSDbField = "tabpaga.tb_ggcalend"

      ckTb_shiftm.NTSText.NTSDbField = "tabpaga.tb_shiftm"
      ckTb_flcondp.NTSText.NTSDbField = "tabpaga.tb_flcondp"
      ckTb_webvis.NTSText.NTSDbField = "tabpaga.tb_webvis"

      lbXx_concassp.NTSDbField = "tabpaga.xx_concassp"
      lbXx_codtpsg.NTSDbField = "tabpaga.xx_codstpg"
      lbXx_codmodi.NTSDbField = "tabpaga.xx_codmodi"

      cbTb_tipp_6.NTSDbField = "tabpaga.tb_tipp_6"
      cbTb_tipp_5.NTSDbField = "tabpaga.tb_tipp_5"
      cbTb_tipp_4.NTSDbField = "tabpaga.tb_tipp_4"
      cbTb_tipp_3.NTSDbField = "tabpaga.tb_tipp_3"
      cbTb_tipp_2.NTSDbField = "tabpaga.tb_tipp_2"
      cbTb_tipp_1.NTSDbField = "tabpaga.tb_tipp_1"
      cbTb_base_6.NTSDbField = "tabpaga.tb_base_6"
      cbTb_base_5.NTSDbField = "tabpaga.tb_base_5"
      cbTb_base_4.NTSDbField = "tabpaga.tb_base_4"
      cbTb_base_3.NTSDbField = "tabpaga.tb_base_3"
      cbTb_base_2.NTSDbField = "tabpaga.tb_base_2"
      edTb_desp_6.NTSDbField = "tabpaga.tb_desp_6"
      edTb_desp_5.NTSDbField = "tabpaga.tb_desp_5"
      edTb_giorni_6.NTSDbField = "tabpaga.tb_giorni_6"
      edTb_desp_4.NTSDbField = "tabpaga.tb_desp_4"
      edTb_giorni_5.NTSDbField = "tabpaga.tb_giorni_5"
      edTb_desp_3.NTSDbField = "tabpaga.tb_desp_3"
      edTb_desp_2.NTSDbField = "tabpaga.tb_desp_2"
      edTb_giorni_4.NTSDbField = "tabpaga.tb_giorni_4"
      edTb_desp_1.NTSDbField = "tabpaga.tb_desp_1"
      cbTb_base_1.NTSDbField = "tabpaga.tb_base_1"
      edTb_giorni_3.NTSDbField = "tabpaga.tb_giorni_3"
      edTb_giorni_2.NTSDbField = "tabpaga.tb_giorni_2"
      edTb_giorni_1.NTSDbField = "tabpaga.tb_giorni_1"
      edTb_quota_6.NTSDbField = "tabpaga.tb_quota_6"
      edTb_quota_5.NTSDbField = "tabpaga.tb_quota_5"
      edTb_quota_4.NTSDbField = "tabpaga.tb_quota_4"
      edTb_quota_3.NTSDbField = "tabpaga.tb_quota_3"
      edTb_quota_2.NTSDbField = "tabpaga.tb_quota_2"
      edTb_quota_1.NTSDbField = "tabpaga.tb_quota_1"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcPaga, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()

    Try
      Dim dttTipPag As New DataTable()
      Dim dttTipScad As New DataTable()
      Dim dttTipInc As New DataTable()
      Dim dttPriRata As New DataTable()
      Dim dttBaseQuota As New DataTable()
      Dim dttGgcalend As New DataTable()


      dttGgcalend.Columns.Add("cod", GetType(String))
      dttGgcalend.Columns.Add("val", GetType(String))
      dttGgcalend.Rows.Add(New Object() {" ", "Nessun limite"})
      dttGgcalend.Rows.Add(New Object() {"A", "Entro 30 gg da FM data ricev. fattura"})
      dttGgcalend.Rows.Add(New Object() {"B", "Entro 60 gg da FM data ricev. fattura"})
      dttGgcalend.AcceptChanges()
      cbTb_ggcalend.DataSource = dttGgcalend
      cbTb_ggcalend.ValueMember = "cod"
      cbTb_ggcalend.DisplayMember = "val"

      dttTipPag.Columns.Add("cod", GetType(Integer))
      dttTipPag.Columns.Add("val", GetType(String))
      dttTipPag.Rows.Add(New Object() {1, "Tratta"})
      dttTipPag.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTipPag.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTipPag.Rows.Add(New Object() {4, "Contanti"})
      dttTipPag.Rows.Add(New Object() {5, "Accr.Bancario"})
      dttTipPag.AcceptChanges()
      cbTb_tippaga.DataSource = dttTipPag
      cbTb_tippaga.ValueMember = "cod"
      cbTb_tippaga.DisplayMember = "val"

      cbTb_tipp_1.DataSource = dttTipPag
      cbTb_tipp_1.ValueMember = "cod"
      cbTb_tipp_1.DisplayMember = "val"
      cbTb_tipp_2.DataSource = dttTipPag
      cbTb_tipp_2.ValueMember = "cod"
      cbTb_tipp_2.DisplayMember = "val"
      cbTb_tipp_3.DataSource = dttTipPag
      cbTb_tipp_3.ValueMember = "cod"
      cbTb_tipp_3.DisplayMember = "val"
      cbTb_tipp_4.DataSource = dttTipPag
      cbTb_tipp_4.ValueMember = "cod"
      cbTb_tipp_4.DisplayMember = "val"
      cbTb_tipp_5.DataSource = dttTipPag
      cbTb_tipp_5.ValueMember = "cod"
      cbTb_tipp_5.DisplayMember = "val"
      cbTb_tipp_6.DataSource = dttTipPag
      cbTb_tipp_6.ValueMember = "cod"
      cbTb_tipp_6.DisplayMember = "val"

      dttTipScad.Columns.Add("cod", GetType(Integer))
      dttTipScad.Columns.Add("val", GetType(String))
      dttTipScad.Rows.Add(New Object() {1, "Data Fattura"})
      dttTipScad.Rows.Add(New Object() {2, "Fine mese"})
      dttTipScad.Rows.Add(New Object() {3, "Data bolla"})
      dttTipScad.Rows.Add(New Object() {4, "Data diversa"})
      dttTipScad.Rows.Add(New Object() {5, "F.M. escl. 1.a rata"})
      dttTipScad.Rows.Add(New Object() {6, "Data fissa"})
      dttTipScad.AcceptChanges()
      cbTb_decpaga.DataSource = dttTipScad
      cbTb_decpaga.ValueMember = "cod"
      cbTb_decpaga.DisplayMember = "val"

      dttPriRata.Columns.Add("cod", GetType(String))
      dttPriRata.Columns.Add("val", GetType(String))
      dttPriRata.Rows.Add(New Object() {0, "(Nessuna eccez.)"})
      dttPriRata.Rows.Add(New Object() {1, "1.a rata solo iva"})
      dttPriRata.Rows.Add(New Object() {2, "1.a rata Iva+spese"})
      dttPriRata.Rows.Add(New Object() {3, "Iva su  1.a rata"})
      dttPriRata.Rows.Add(New Object() {4, "Iva+spese 1.a rata"})
      dttPriRata.AcceptChanges()
      cbTb_prirata.DataSource = dttPriRata
      cbTb_prirata.ValueMember = "cod"
      cbTb_prirata.DisplayMember = "val"

      dttTipInc.Columns.Add("cod", GetType(String))
      dttTipInc.Columns.Add("val", GetType(String))
      dttTipInc.Rows.Add(New Object() {" ", "Non definito"})
      dttTipInc.Rows.Add(New Object() {"C", "Contanti"})
      dttTipInc.Rows.Add(New Object() {"A", "Assegni"})
      dttTipInc.Rows.Add(New Object() {"V", "Carte di credito"})
      dttTipInc.Rows.Add(New Object() {"B", "Bancomat"})
      dttTipInc.Rows.Add(New Object() {"F", "Finanziamento"})
      dttTipInc.Rows.Add(New Object() {"P", "Buoni pasto"})
      dttTipInc.Rows.Add(New Object() {"T", "Tessera"})
      'dttTipInc.Rows.Add(New Object() {"X", "Buono"})
      dttTipInc.Rows.Add(New Object() {"Z", "Altro"})
      dttTipInc.AcceptChanges()
      cbTb_tipincecr.DataSource = dttTipInc
      cbTb_tipincecr.ValueMember = "cod"
      cbTb_tipincecr.DisplayMember = "val"

      dttBaseQuota.Columns.Add("cod", GetType(String))
      dttBaseQuota.Columns.Add("val", GetType(String))
      dttBaseQuota.Rows.Add(New Object() {"T", "Totale Doc."})
      dttBaseQuota.Rows.Add(New Object() {"I", "IVA"})
      dttBaseQuota.Rows.Add(New Object() {"S", "Iva + Spese"})
      dttBaseQuota.Rows.Add(New Object() {"M", "Merce"})
      dttBaseQuota.Rows.Add(New Object() {"N", "Merce + Spese"})
      dttBaseQuota.Rows.Add(New Object() {"R", "Valore residuo"})
      dttBaseQuota.AcceptChanges()
      cbTb_base_1.DataSource = dttBaseQuota
      cbTb_base_1.ValueMember = "cod"
      cbTb_base_1.DisplayMember = "val"
      cbTb_base_2.DataSource = dttBaseQuota
      cbTb_base_2.ValueMember = "cod"
      cbTb_base_2.DisplayMember = "val"
      cbTb_base_3.DataSource = dttBaseQuota
      cbTb_base_3.ValueMember = "cod"
      cbTb_base_3.DisplayMember = "val"
      cbTb_base_4.DataSource = dttBaseQuota
      cbTb_base_4.ValueMember = "cod"
      cbTb_base_4.DisplayMember = "val"
      cbTb_base_5.DataSource = dttBaseQuota
      cbTb_base_5.ValueMember = "cod"
      cbTb_base_5.DisplayMember = "val"
      cbTb_base_6.DataSource = dttBaseQuota
      cbTb_base_6.ValueMember = "cod"
      cbTb_base_6.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oClePaga.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952559218750, "Salvo la forma di pagamento?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oClePaga.Salva(False) Then Return False
          If dsPaga.Tables("TABPAGA").Rows.Count > 0 Then
            edTb_codpaga.Enabled = False
          End If
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = ""
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--PAGA", "Reports1", " ", 0, nDestin, "BS--PAGA.RPT", False, "Forme di pagamento", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
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

End Class
