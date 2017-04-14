Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMPRSCPR

#Region "Dichiarazione variabili"
  Public oCleScpr As CLEPRSCPR
  Public dsScpr As DataSet
  Public dcScpr As BindingSource = New BindingSource
  Public oPar As CLE__CLDP
  Private components As System.ComponentModel.IComponent

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbTb_codscpr As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desscpr As NTSInformatica.NTSLabel
  Public WithEvents edTb_codscpr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_desscpr As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnFrame As NTSInformatica.NTSPanel
  Public WithEvents opXx_listino As NTSInformatica.NTSRadioButton
  Public WithEvents opXx_ultcos As NTSInformatica.NTSRadioButton
  Public WithEvents edTb_lisscpr As NTSInformatica.NTSTextBoxNum
  Public WithEvents tabScpr As NTSInformatica.NTSTabControl
  Public WithEvents tabSconti16 As NTSInformatica.NTSTabPage
  Public WithEvents tabSconti712 As NTSInformatica.NTSTabPage
  Public WithEvents pnScpr16 As NTSInformatica.NTSPanel
  Public WithEvents lbTb_dasco_6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_dasco_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_asco_6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_asco_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_pripa_6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_pprov_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_pprov_6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_1 As NTSInformatica.NTSLabel
  Public WithEvents pnScpr712 As NTSInformatica.NTSPanel
  Public WithEvents edTb_pripa_12 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_11 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_10 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pripa_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_pripa_12 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_11 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pripa_7 As NTSInformatica.NTSLabel
  Public WithEvents edTb_pprov_12 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_11 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_10 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pprov_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_pprov_12 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_11 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pprov_7 As NTSInformatica.NTSLabel
  Public WithEvents edTb_asco_12 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_11 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_10 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_asco_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_asco_12 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_11 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_asco_7 As NTSInformatica.NTSLabel
  Public WithEvents edTb_dasco_12 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_11 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_10 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dasco_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_dasco_12 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_11 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dasco_7 As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModPR
  Private ModuliExt_P As Integer = 0
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

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oPar = Param
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRSCPR", "BEPRSCPR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleScpr = CType(oTmp, CLEPRSCPR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRSCPR", strRemoteServer, strRemotePort)
    AddHandler oCleScpr.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleScpr.Init(oApp, NTSScript, oMenu.oCleComm, "TABSCPR", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__PAGA che eredita da BE__PAGA
    'CallByName(oCleProv, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRSCPR))
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
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edTb_desscpr = New NTSInformatica.NTSTextBoxStr
    Me.edTb_codscpr = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_desscpr = New NTSInformatica.NTSLabel
    Me.lbTb_codscpr = New NTSInformatica.NTSLabel
    Me.pnFrame = New NTSInformatica.NTSPanel
    Me.edTb_lisscpr = New NTSInformatica.NTSTextBoxNum
    Me.opXx_listino = New NTSInformatica.NTSRadioButton
    Me.opXx_ultcos = New NTSInformatica.NTSRadioButton
    Me.tabScpr = New NTSInformatica.NTSTabControl
    Me.tabSconti16 = New NTSInformatica.NTSTabPage
    Me.pnScpr16 = New NTSInformatica.NTSPanel
    Me.edTb_pripa_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_2 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_pripa_6 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_5 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_4 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_3 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_2 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_1 = New NTSInformatica.NTSLabel
    Me.edTb_pprov_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_2 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_pprov_6 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_5 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_4 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_3 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_2 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_1 = New NTSInformatica.NTSLabel
    Me.edTb_asco_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_2 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_asco_6 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_5 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_4 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_3 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_2 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_1 = New NTSInformatica.NTSLabel
    Me.edTb_dasco_6 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_2 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_dasco_6 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_5 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_4 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_3 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_2 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_1 = New NTSInformatica.NTSLabel
    Me.tabSconti712 = New NTSInformatica.NTSTabPage
    Me.pnScpr712 = New NTSInformatica.NTSPanel
    Me.edTb_pripa_12 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_11 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_10 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_9 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_8 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pripa_7 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_pripa_12 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_11 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_10 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_9 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_8 = New NTSInformatica.NTSLabel
    Me.lbTb_pripa_7 = New NTSInformatica.NTSLabel
    Me.edTb_pprov_12 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_11 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_10 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_9 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_8 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pprov_7 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_pprov_12 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_11 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_10 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_9 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_8 = New NTSInformatica.NTSLabel
    Me.lbTb_pprov_7 = New NTSInformatica.NTSLabel
    Me.edTb_asco_12 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_11 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_10 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_9 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_8 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_asco_7 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_asco_12 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_11 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_10 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_9 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_8 = New NTSInformatica.NTSLabel
    Me.lbTb_asco_7 = New NTSInformatica.NTSLabel
    Me.edTb_dasco_12 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_11 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_10 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_9 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_8 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dasco_7 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_dasco_12 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_11 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_10 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_9 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_8 = New NTSInformatica.NTSLabel
    Me.lbTb_dasco_7 = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desscpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codscpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFrame, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFrame.SuspendLayout()
    CType(Me.edTb_lisscpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opXx_listino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opXx_ultcos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tabScpr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabScpr.SuspendLayout()
    Me.tabSconti16.SuspendLayout()
    CType(Me.pnScpr16, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnScpr16.SuspendLayout()
    CType(Me.edTb_pripa_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabSconti712.SuspendLayout()
    CType(Me.pnScpr712, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnScpr712.SuspendLayout()
    CType(Me.edTb_pripa_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pripa_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pprov_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_asco_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dasco_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 13
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
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
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 8
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'edTb_desscpr
    '
    Me.edTb_desscpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desscpr.Location = New System.Drawing.Point(125, 62)
    Me.edTb_desscpr.Name = "edTb_desscpr"
    Me.edTb_desscpr.NTSDbField = ""
    Me.edTb_desscpr.NTSForzaVisZoom = False
    Me.edTb_desscpr.NTSOldValue = ""
    Me.edTb_desscpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desscpr.Properties.MaxLength = 65536
    Me.edTb_desscpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desscpr.Size = New System.Drawing.Size(243, 20)
    Me.edTb_desscpr.TabIndex = 4
    '
    'edTb_codscpr
    '
    Me.edTb_codscpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codscpr.Location = New System.Drawing.Point(125, 36)
    Me.edTb_codscpr.Name = "edTb_codscpr"
    Me.edTb_codscpr.NTSDbField = ""
    Me.edTb_codscpr.NTSFormat = "0"
    Me.edTb_codscpr.NTSForzaVisZoom = False
    Me.edTb_codscpr.NTSOldValue = ""
    Me.edTb_codscpr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codscpr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codscpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codscpr.Properties.MaxLength = 65536
    Me.edTb_codscpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codscpr.Size = New System.Drawing.Size(76, 20)
    Me.edTb_codscpr.TabIndex = 5
    '
    'lbTb_desscpr
    '
    Me.lbTb_desscpr.AutoSize = True
    Me.lbTb_desscpr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desscpr.Location = New System.Drawing.Point(12, 64)
    Me.lbTb_desscpr.Name = "lbTb_desscpr"
    Me.lbTb_desscpr.NTSDbField = ""
    Me.lbTb_desscpr.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_desscpr.TabIndex = 6
    Me.lbTb_desscpr.Text = "Descrizione"
    '
    'lbTb_codscpr
    '
    Me.lbTb_codscpr.AutoSize = True
    Me.lbTb_codscpr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codscpr.Location = New System.Drawing.Point(12, 39)
    Me.lbTb_codscpr.Name = "lbTb_codscpr"
    Me.lbTb_codscpr.NTSDbField = ""
    Me.lbTb_codscpr.Size = New System.Drawing.Size(107, 13)
    Me.lbTb_codscpr.TabIndex = 7
    Me.lbTb_codscpr.Text = "Codice Sconti/Provv."
    '
    'pnFrame
    '
    Me.pnFrame.AllowDrop = True
    Me.pnFrame.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFrame.Appearance.Options.UseBackColor = True
    Me.pnFrame.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.pnFrame.Controls.Add(Me.edTb_lisscpr)
    Me.pnFrame.Controls.Add(Me.opXx_listino)
    Me.pnFrame.Controls.Add(Me.opXx_ultcos)
    Me.pnFrame.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFrame.Location = New System.Drawing.Point(384, 36)
    Me.pnFrame.Name = "pnFrame"
    Me.pnFrame.Size = New System.Drawing.Size(143, 61)
    Me.pnFrame.TabIndex = 8
    Me.pnFrame.Text = "NtsPanel1"
    '
    'edTb_lisscpr
    '
    Me.edTb_lisscpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_lisscpr.Location = New System.Drawing.Point(68, 29)
    Me.edTb_lisscpr.Name = "edTb_lisscpr"
    Me.edTb_lisscpr.NTSDbField = ""
    Me.edTb_lisscpr.NTSFormat = "0"
    Me.edTb_lisscpr.NTSForzaVisZoom = False
    Me.edTb_lisscpr.NTSOldValue = ""
    Me.edTb_lisscpr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_lisscpr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_lisscpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_lisscpr.Properties.MaxLength = 65536
    Me.edTb_lisscpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_lisscpr.Size = New System.Drawing.Size(70, 20)
    Me.edTb_lisscpr.TabIndex = 2
    '
    'opXx_listino
    '
    Me.opXx_listino.Cursor = System.Windows.Forms.Cursors.Default
    Me.opXx_listino.Location = New System.Drawing.Point(5, 30)
    Me.opXx_listino.Name = "opXx_listino"
    Me.opXx_listino.NTSCheckValue = "S"
    Me.opXx_listino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opXx_listino.Properties.Appearance.Options.UseBackColor = True
    Me.opXx_listino.Properties.Caption = "Listino"
    Me.opXx_listino.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opXx_listino.Size = New System.Drawing.Size(63, 19)
    Me.opXx_listino.TabIndex = 1
    '
    'opXx_ultcos
    '
    Me.opXx_ultcos.Cursor = System.Windows.Forms.Cursors.Default
    Me.opXx_ultcos.Location = New System.Drawing.Point(5, 5)
    Me.opXx_ultcos.Name = "opXx_ultcos"
    Me.opXx_ultcos.NTSCheckValue = "S"
    Me.opXx_ultcos.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opXx_ultcos.Properties.Appearance.Options.UseBackColor = True
    Me.opXx_ultcos.Properties.Caption = "Ultimo Costo d'Acquisto"
    Me.opXx_ultcos.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opXx_ultcos.Size = New System.Drawing.Size(133, 19)
    Me.opXx_ultcos.TabIndex = 0
    '
    'tabScpr
    '
    Me.tabScpr.Location = New System.Drawing.Point(13, 103)
    Me.tabScpr.Name = "tabScpr"
    Me.tabScpr.SelectedTabPage = Me.tabSconti16
    Me.tabScpr.Size = New System.Drawing.Size(514, 196)
    Me.tabScpr.TabIndex = 9
    Me.tabScpr.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabSconti16, Me.tabSconti712})
    Me.tabScpr.Text = "NtsTabControl1"
    '
    'tabSconti16
    '
    Me.tabSconti16.AllowDrop = True
    Me.tabSconti16.Controls.Add(Me.pnScpr16)
    Me.tabSconti16.Enable = True
    Me.tabSconti16.Name = "tabSconti16"
    Me.tabSconti16.Size = New System.Drawing.Size(505, 166)
    Me.tabSconti16.Text = "Sconti (1 - 6)"
    '
    'pnScpr16
    '
    Me.pnScpr16.AllowDrop = True
    Me.pnScpr16.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnScpr16.Appearance.Options.UseBackColor = True
    Me.pnScpr16.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_6)
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_5)
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_4)
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_3)
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_2)
    Me.pnScpr16.Controls.Add(Me.edTb_pripa_1)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_6)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_5)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_4)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_3)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_2)
    Me.pnScpr16.Controls.Add(Me.lbTb_pripa_1)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_6)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_5)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_4)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_3)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_2)
    Me.pnScpr16.Controls.Add(Me.edTb_pprov_1)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_6)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_5)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_4)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_3)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_2)
    Me.pnScpr16.Controls.Add(Me.lbTb_pprov_1)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_6)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_5)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_4)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_3)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_2)
    Me.pnScpr16.Controls.Add(Me.edTb_asco_1)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_6)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_5)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_4)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_3)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_2)
    Me.pnScpr16.Controls.Add(Me.lbTb_asco_1)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_6)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_5)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_4)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_3)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_2)
    Me.pnScpr16.Controls.Add(Me.edTb_dasco_1)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_6)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_5)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_4)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_3)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_2)
    Me.pnScpr16.Controls.Add(Me.lbTb_dasco_1)
    Me.pnScpr16.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnScpr16.Location = New System.Drawing.Point(4, 4)
    Me.pnScpr16.Name = "pnScpr16"
    Me.pnScpr16.Size = New System.Drawing.Size(498, 161)
    Me.pnScpr16.TabIndex = 0
    Me.pnScpr16.Text = "NtsPanel1"
    '
    'edTb_pripa_6
    '
    Me.edTb_pripa_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_6.Location = New System.Drawing.Point(431, 134)
    Me.edTb_pripa_6.Name = "edTb_pripa_6"
    Me.edTb_pripa_6.NTSDbField = ""
    Me.edTb_pripa_6.NTSFormat = "0"
    Me.edTb_pripa_6.NTSForzaVisZoom = False
    Me.edTb_pripa_6.NTSOldValue = ""
    Me.edTb_pripa_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_6.Properties.MaxLength = 65536
    Me.edTb_pripa_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_6.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_6.TabIndex = 47
    '
    'edTb_pripa_5
    '
    Me.edTb_pripa_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_5.Location = New System.Drawing.Point(431, 108)
    Me.edTb_pripa_5.Name = "edTb_pripa_5"
    Me.edTb_pripa_5.NTSDbField = ""
    Me.edTb_pripa_5.NTSFormat = "0"
    Me.edTb_pripa_5.NTSForzaVisZoom = False
    Me.edTb_pripa_5.NTSOldValue = ""
    Me.edTb_pripa_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_5.Properties.MaxLength = 65536
    Me.edTb_pripa_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_5.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_5.TabIndex = 46
    '
    'edTb_pripa_4
    '
    Me.edTb_pripa_4.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_pripa_4.Location = New System.Drawing.Point(431, 82)
    Me.edTb_pripa_4.Name = "edTb_pripa_4"
    Me.edTb_pripa_4.NTSDbField = ""
    Me.edTb_pripa_4.NTSFormat = "0"
    Me.edTb_pripa_4.NTSForzaVisZoom = False
    Me.edTb_pripa_4.NTSOldValue = ""
    Me.edTb_pripa_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_4.Properties.MaxLength = 65536
    Me.edTb_pripa_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_4.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_4.TabIndex = 45
    '
    'edTb_pripa_3
    '
    Me.edTb_pripa_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_3.Location = New System.Drawing.Point(431, 56)
    Me.edTb_pripa_3.Name = "edTb_pripa_3"
    Me.edTb_pripa_3.NTSDbField = ""
    Me.edTb_pripa_3.NTSFormat = "0"
    Me.edTb_pripa_3.NTSForzaVisZoom = False
    Me.edTb_pripa_3.NTSOldValue = ""
    Me.edTb_pripa_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_3.Properties.MaxLength = 65536
    Me.edTb_pripa_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_3.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_3.TabIndex = 44
    '
    'edTb_pripa_2
    '
    Me.edTb_pripa_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_2.Location = New System.Drawing.Point(431, 30)
    Me.edTb_pripa_2.Name = "edTb_pripa_2"
    Me.edTb_pripa_2.NTSDbField = ""
    Me.edTb_pripa_2.NTSFormat = "0"
    Me.edTb_pripa_2.NTSForzaVisZoom = False
    Me.edTb_pripa_2.NTSOldValue = ""
    Me.edTb_pripa_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_2.Properties.MaxLength = 65536
    Me.edTb_pripa_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_2.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_2.TabIndex = 43
    '
    'edTb_pripa_1
    '
    Me.edTb_pripa_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_1.Location = New System.Drawing.Point(431, 4)
    Me.edTb_pripa_1.Name = "edTb_pripa_1"
    Me.edTb_pripa_1.NTSDbField = ""
    Me.edTb_pripa_1.NTSFormat = "0"
    Me.edTb_pripa_1.NTSForzaVisZoom = False
    Me.edTb_pripa_1.NTSOldValue = ""
    Me.edTb_pripa_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_1.Properties.MaxLength = 65536
    Me.edTb_pripa_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_1.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_1.TabIndex = 42
    '
    'lbTb_pripa_6
    '
    Me.lbTb_pripa_6.AutoSize = True
    Me.lbTb_pripa_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_6.Location = New System.Drawing.Point(355, 137)
    Me.lbTb_pripa_6.Name = "lbTb_pripa_6"
    Me.lbTb_pripa_6.NTSDbField = ""
    Me.lbTb_pripa_6.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_6.TabIndex = 41
    Me.lbTb_pripa_6.Text = "+ % ripartita"
    '
    'lbTb_pripa_5
    '
    Me.lbTb_pripa_5.AutoSize = True
    Me.lbTb_pripa_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_5.Location = New System.Drawing.Point(355, 111)
    Me.lbTb_pripa_5.Name = "lbTb_pripa_5"
    Me.lbTb_pripa_5.NTSDbField = ""
    Me.lbTb_pripa_5.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_5.TabIndex = 40
    Me.lbTb_pripa_5.Text = "+ % ripartita"
    '
    'lbTb_pripa_4
    '
    Me.lbTb_pripa_4.AutoSize = True
    Me.lbTb_pripa_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_4.Location = New System.Drawing.Point(355, 85)
    Me.lbTb_pripa_4.Name = "lbTb_pripa_4"
    Me.lbTb_pripa_4.NTSDbField = ""
    Me.lbTb_pripa_4.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_4.TabIndex = 39
    Me.lbTb_pripa_4.Text = "+ % ripartita"
    '
    'lbTb_pripa_3
    '
    Me.lbTb_pripa_3.AutoSize = True
    Me.lbTb_pripa_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_3.Location = New System.Drawing.Point(355, 59)
    Me.lbTb_pripa_3.Name = "lbTb_pripa_3"
    Me.lbTb_pripa_3.NTSDbField = ""
    Me.lbTb_pripa_3.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_3.TabIndex = 38
    Me.lbTb_pripa_3.Text = "+ % ripartita"
    '
    'lbTb_pripa_2
    '
    Me.lbTb_pripa_2.AutoSize = True
    Me.lbTb_pripa_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_2.Location = New System.Drawing.Point(355, 33)
    Me.lbTb_pripa_2.Name = "lbTb_pripa_2"
    Me.lbTb_pripa_2.NTSDbField = ""
    Me.lbTb_pripa_2.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_2.TabIndex = 37
    Me.lbTb_pripa_2.Text = "+ % ripartita"
    '
    'lbTb_pripa_1
    '
    Me.lbTb_pripa_1.AutoSize = True
    Me.lbTb_pripa_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_1.Location = New System.Drawing.Point(355, 7)
    Me.lbTb_pripa_1.Name = "lbTb_pripa_1"
    Me.lbTb_pripa_1.NTSDbField = ""
    Me.lbTb_pripa_1.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_1.TabIndex = 36
    Me.lbTb_pripa_1.Text = "+ % ripartita"
    '
    'edTb_pprov_6
    '
    Me.edTb_pprov_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_6.Location = New System.Drawing.Point(288, 134)
    Me.edTb_pprov_6.Name = "edTb_pprov_6"
    Me.edTb_pprov_6.NTSDbField = ""
    Me.edTb_pprov_6.NTSFormat = "0"
    Me.edTb_pprov_6.NTSForzaVisZoom = False
    Me.edTb_pprov_6.NTSOldValue = ""
    Me.edTb_pprov_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_6.Properties.MaxLength = 65536
    Me.edTb_pprov_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_6.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_6.TabIndex = 35
    '
    'edTb_pprov_5
    '
    Me.edTb_pprov_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_5.Location = New System.Drawing.Point(288, 108)
    Me.edTb_pprov_5.Name = "edTb_pprov_5"
    Me.edTb_pprov_5.NTSDbField = ""
    Me.edTb_pprov_5.NTSFormat = "0"
    Me.edTb_pprov_5.NTSForzaVisZoom = False
    Me.edTb_pprov_5.NTSOldValue = ""
    Me.edTb_pprov_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_5.Properties.MaxLength = 65536
    Me.edTb_pprov_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_5.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_5.TabIndex = 34
    '
    'edTb_pprov_4
    '
    Me.edTb_pprov_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_4.Location = New System.Drawing.Point(288, 82)
    Me.edTb_pprov_4.Name = "edTb_pprov_4"
    Me.edTb_pprov_4.NTSDbField = ""
    Me.edTb_pprov_4.NTSFormat = "0"
    Me.edTb_pprov_4.NTSForzaVisZoom = False
    Me.edTb_pprov_4.NTSOldValue = ""
    Me.edTb_pprov_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_4.Properties.MaxLength = 65536
    Me.edTb_pprov_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_4.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_4.TabIndex = 33
    '
    'edTb_pprov_3
    '
    Me.edTb_pprov_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_3.Location = New System.Drawing.Point(288, 56)
    Me.edTb_pprov_3.Name = "edTb_pprov_3"
    Me.edTb_pprov_3.NTSDbField = ""
    Me.edTb_pprov_3.NTSFormat = "0"
    Me.edTb_pprov_3.NTSForzaVisZoom = False
    Me.edTb_pprov_3.NTSOldValue = ""
    Me.edTb_pprov_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_3.Properties.MaxLength = 65536
    Me.edTb_pprov_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_3.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_3.TabIndex = 32
    '
    'edTb_pprov_2
    '
    Me.edTb_pprov_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_2.Location = New System.Drawing.Point(288, 30)
    Me.edTb_pprov_2.Name = "edTb_pprov_2"
    Me.edTb_pprov_2.NTSDbField = ""
    Me.edTb_pprov_2.NTSFormat = "0"
    Me.edTb_pprov_2.NTSForzaVisZoom = False
    Me.edTb_pprov_2.NTSOldValue = ""
    Me.edTb_pprov_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_2.Properties.MaxLength = 65536
    Me.edTb_pprov_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_2.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_2.TabIndex = 31
    '
    'edTb_pprov_1
    '
    Me.edTb_pprov_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_1.Location = New System.Drawing.Point(288, 4)
    Me.edTb_pprov_1.Name = "edTb_pprov_1"
    Me.edTb_pprov_1.NTSDbField = ""
    Me.edTb_pprov_1.NTSFormat = "0"
    Me.edTb_pprov_1.NTSForzaVisZoom = False
    Me.edTb_pprov_1.NTSOldValue = ""
    Me.edTb_pprov_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_1.Properties.MaxLength = 65536
    Me.edTb_pprov_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_1.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_1.TabIndex = 30
    '
    'lbTb_pprov_6
    '
    Me.lbTb_pprov_6.AutoSize = True
    Me.lbTb_pprov_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_6.Location = New System.Drawing.Point(198, 137)
    Me.lbTb_pprov_6.Name = "lbTb_pprov_6"
    Me.lbTb_pprov_6.NTSDbField = ""
    Me.lbTb_pprov_6.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_6.TabIndex = 29
    Me.lbTb_pprov_6.Text = "% provvigione :"
    '
    'lbTb_pprov_5
    '
    Me.lbTb_pprov_5.AutoSize = True
    Me.lbTb_pprov_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_5.Location = New System.Drawing.Point(198, 111)
    Me.lbTb_pprov_5.Name = "lbTb_pprov_5"
    Me.lbTb_pprov_5.NTSDbField = ""
    Me.lbTb_pprov_5.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_5.TabIndex = 28
    Me.lbTb_pprov_5.Text = "% provvigione :"
    '
    'lbTb_pprov_4
    '
    Me.lbTb_pprov_4.AutoSize = True
    Me.lbTb_pprov_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_4.Location = New System.Drawing.Point(198, 85)
    Me.lbTb_pprov_4.Name = "lbTb_pprov_4"
    Me.lbTb_pprov_4.NTSDbField = ""
    Me.lbTb_pprov_4.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_4.TabIndex = 27
    Me.lbTb_pprov_4.Text = "% provvigione :"
    '
    'lbTb_pprov_3
    '
    Me.lbTb_pprov_3.AutoSize = True
    Me.lbTb_pprov_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_3.Location = New System.Drawing.Point(198, 59)
    Me.lbTb_pprov_3.Name = "lbTb_pprov_3"
    Me.lbTb_pprov_3.NTSDbField = ""
    Me.lbTb_pprov_3.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_3.TabIndex = 26
    Me.lbTb_pprov_3.Text = "% provvigione :"
    '
    'lbTb_pprov_2
    '
    Me.lbTb_pprov_2.AutoSize = True
    Me.lbTb_pprov_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_2.Location = New System.Drawing.Point(198, 33)
    Me.lbTb_pprov_2.Name = "lbTb_pprov_2"
    Me.lbTb_pprov_2.NTSDbField = ""
    Me.lbTb_pprov_2.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_2.TabIndex = 25
    Me.lbTb_pprov_2.Text = "% provvigione :"
    '
    'lbTb_pprov_1
    '
    Me.lbTb_pprov_1.AutoSize = True
    Me.lbTb_pprov_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_1.Location = New System.Drawing.Point(198, 7)
    Me.lbTb_pprov_1.Name = "lbTb_pprov_1"
    Me.lbTb_pprov_1.NTSDbField = ""
    Me.lbTb_pprov_1.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_1.TabIndex = 24
    Me.lbTb_pprov_1.Text = "% provvigione :"
    '
    'edTb_asco_6
    '
    Me.edTb_asco_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_6.Location = New System.Drawing.Point(131, 134)
    Me.edTb_asco_6.Name = "edTb_asco_6"
    Me.edTb_asco_6.NTSDbField = ""
    Me.edTb_asco_6.NTSFormat = "0"
    Me.edTb_asco_6.NTSForzaVisZoom = False
    Me.edTb_asco_6.NTSOldValue = ""
    Me.edTb_asco_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_6.Properties.MaxLength = 65536
    Me.edTb_asco_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_6.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_6.TabIndex = 23
    '
    'edTb_asco_5
    '
    Me.edTb_asco_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_5.Location = New System.Drawing.Point(131, 108)
    Me.edTb_asco_5.Name = "edTb_asco_5"
    Me.edTb_asco_5.NTSDbField = ""
    Me.edTb_asco_5.NTSFormat = "0"
    Me.edTb_asco_5.NTSForzaVisZoom = False
    Me.edTb_asco_5.NTSOldValue = ""
    Me.edTb_asco_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_5.Properties.MaxLength = 65536
    Me.edTb_asco_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_5.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_5.TabIndex = 22
    '
    'edTb_asco_4
    '
    Me.edTb_asco_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_4.Location = New System.Drawing.Point(131, 82)
    Me.edTb_asco_4.Name = "edTb_asco_4"
    Me.edTb_asco_4.NTSDbField = ""
    Me.edTb_asco_4.NTSFormat = "0"
    Me.edTb_asco_4.NTSForzaVisZoom = False
    Me.edTb_asco_4.NTSOldValue = ""
    Me.edTb_asco_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_4.Properties.MaxLength = 65536
    Me.edTb_asco_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_4.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_4.TabIndex = 21
    '
    'edTb_asco_3
    '
    Me.edTb_asco_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_3.Location = New System.Drawing.Point(131, 56)
    Me.edTb_asco_3.Name = "edTb_asco_3"
    Me.edTb_asco_3.NTSDbField = ""
    Me.edTb_asco_3.NTSFormat = "0"
    Me.edTb_asco_3.NTSForzaVisZoom = False
    Me.edTb_asco_3.NTSOldValue = ""
    Me.edTb_asco_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_3.Properties.MaxLength = 65536
    Me.edTb_asco_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_3.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_3.TabIndex = 20
    '
    'edTb_asco_2
    '
    Me.edTb_asco_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_2.Location = New System.Drawing.Point(131, 30)
    Me.edTb_asco_2.Name = "edTb_asco_2"
    Me.edTb_asco_2.NTSDbField = ""
    Me.edTb_asco_2.NTSFormat = "0"
    Me.edTb_asco_2.NTSForzaVisZoom = False
    Me.edTb_asco_2.NTSOldValue = ""
    Me.edTb_asco_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_2.Properties.MaxLength = 65536
    Me.edTb_asco_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_2.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_2.TabIndex = 19
    '
    'edTb_asco_1
    '
    Me.edTb_asco_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_1.Location = New System.Drawing.Point(131, 4)
    Me.edTb_asco_1.Name = "edTb_asco_1"
    Me.edTb_asco_1.NTSDbField = ""
    Me.edTb_asco_1.NTSFormat = "0"
    Me.edTb_asco_1.NTSForzaVisZoom = False
    Me.edTb_asco_1.NTSOldValue = ""
    Me.edTb_asco_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_1.Properties.MaxLength = 65536
    Me.edTb_asco_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_1.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_1.TabIndex = 18
    '
    'lbTb_asco_6
    '
    Me.lbTb_asco_6.AutoSize = True
    Me.lbTb_asco_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_6.Location = New System.Drawing.Point(105, 137)
    Me.lbTb_asco_6.Name = "lbTb_asco_6"
    Me.lbTb_asco_6.NTSDbField = ""
    Me.lbTb_asco_6.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_6.TabIndex = 17
    Me.lbTb_asco_6.Text = "a :"
    '
    'lbTb_asco_5
    '
    Me.lbTb_asco_5.AutoSize = True
    Me.lbTb_asco_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_5.Location = New System.Drawing.Point(105, 111)
    Me.lbTb_asco_5.Name = "lbTb_asco_5"
    Me.lbTb_asco_5.NTSDbField = ""
    Me.lbTb_asco_5.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_5.TabIndex = 16
    Me.lbTb_asco_5.Text = "a :"
    '
    'lbTb_asco_4
    '
    Me.lbTb_asco_4.AutoSize = True
    Me.lbTb_asco_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_4.Location = New System.Drawing.Point(105, 85)
    Me.lbTb_asco_4.Name = "lbTb_asco_4"
    Me.lbTb_asco_4.NTSDbField = ""
    Me.lbTb_asco_4.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_4.TabIndex = 15
    Me.lbTb_asco_4.Text = "a :"
    '
    'lbTb_asco_3
    '
    Me.lbTb_asco_3.AutoSize = True
    Me.lbTb_asco_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_3.Location = New System.Drawing.Point(105, 59)
    Me.lbTb_asco_3.Name = "lbTb_asco_3"
    Me.lbTb_asco_3.NTSDbField = ""
    Me.lbTb_asco_3.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_3.TabIndex = 14
    Me.lbTb_asco_3.Text = "a :"
    '
    'lbTb_asco_2
    '
    Me.lbTb_asco_2.AutoSize = True
    Me.lbTb_asco_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_2.Location = New System.Drawing.Point(105, 33)
    Me.lbTb_asco_2.Name = "lbTb_asco_2"
    Me.lbTb_asco_2.NTSDbField = ""
    Me.lbTb_asco_2.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_2.TabIndex = 13
    Me.lbTb_asco_2.Text = "a :"
    '
    'lbTb_asco_1
    '
    Me.lbTb_asco_1.AutoSize = True
    Me.lbTb_asco_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_1.Location = New System.Drawing.Point(105, 7)
    Me.lbTb_asco_1.Name = "lbTb_asco_1"
    Me.lbTb_asco_1.NTSDbField = ""
    Me.lbTb_asco_1.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_1.TabIndex = 12
    Me.lbTb_asco_1.Text = "a :"
    '
    'edTb_dasco_6
    '
    Me.edTb_dasco_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_6.Location = New System.Drawing.Point(38, 134)
    Me.edTb_dasco_6.Name = "edTb_dasco_6"
    Me.edTb_dasco_6.NTSDbField = ""
    Me.edTb_dasco_6.NTSFormat = "0"
    Me.edTb_dasco_6.NTSForzaVisZoom = False
    Me.edTb_dasco_6.NTSOldValue = ""
    Me.edTb_dasco_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_6.Properties.MaxLength = 65536
    Me.edTb_dasco_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_6.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_6.TabIndex = 11
    '
    'edTb_dasco_5
    '
    Me.edTb_dasco_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_5.Location = New System.Drawing.Point(38, 108)
    Me.edTb_dasco_5.Name = "edTb_dasco_5"
    Me.edTb_dasco_5.NTSDbField = ""
    Me.edTb_dasco_5.NTSFormat = "0"
    Me.edTb_dasco_5.NTSForzaVisZoom = False
    Me.edTb_dasco_5.NTSOldValue = ""
    Me.edTb_dasco_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_5.Properties.MaxLength = 65536
    Me.edTb_dasco_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_5.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_5.TabIndex = 10
    '
    'edTb_dasco_4
    '
    Me.edTb_dasco_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_4.Location = New System.Drawing.Point(38, 82)
    Me.edTb_dasco_4.Name = "edTb_dasco_4"
    Me.edTb_dasco_4.NTSDbField = ""
    Me.edTb_dasco_4.NTSFormat = "0"
    Me.edTb_dasco_4.NTSForzaVisZoom = False
    Me.edTb_dasco_4.NTSOldValue = ""
    Me.edTb_dasco_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_4.Properties.MaxLength = 65536
    Me.edTb_dasco_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_4.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_4.TabIndex = 9
    '
    'edTb_dasco_3
    '
    Me.edTb_dasco_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_3.Location = New System.Drawing.Point(38, 56)
    Me.edTb_dasco_3.Name = "edTb_dasco_3"
    Me.edTb_dasco_3.NTSDbField = ""
    Me.edTb_dasco_3.NTSFormat = "0"
    Me.edTb_dasco_3.NTSForzaVisZoom = False
    Me.edTb_dasco_3.NTSOldValue = ""
    Me.edTb_dasco_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_3.Properties.MaxLength = 65536
    Me.edTb_dasco_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_3.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_3.TabIndex = 8
    '
    'edTb_dasco_2
    '
    Me.edTb_dasco_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_2.Location = New System.Drawing.Point(38, 30)
    Me.edTb_dasco_2.Name = "edTb_dasco_2"
    Me.edTb_dasco_2.NTSDbField = ""
    Me.edTb_dasco_2.NTSFormat = "0"
    Me.edTb_dasco_2.NTSForzaVisZoom = False
    Me.edTb_dasco_2.NTSOldValue = ""
    Me.edTb_dasco_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_2.Properties.MaxLength = 65536
    Me.edTb_dasco_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_2.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_2.TabIndex = 7
    '
    'edTb_dasco_1
    '
    Me.edTb_dasco_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_1.Location = New System.Drawing.Point(38, 4)
    Me.edTb_dasco_1.Name = "edTb_dasco_1"
    Me.edTb_dasco_1.NTSDbField = ""
    Me.edTb_dasco_1.NTSFormat = "0"
    Me.edTb_dasco_1.NTSForzaVisZoom = False
    Me.edTb_dasco_1.NTSOldValue = ""
    Me.edTb_dasco_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_1.Properties.MaxLength = 65536
    Me.edTb_dasco_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_1.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_1.TabIndex = 6
    '
    'lbTb_dasco_6
    '
    Me.lbTb_dasco_6.AutoSize = True
    Me.lbTb_dasco_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_6.Location = New System.Drawing.Point(4, 137)
    Me.lbTb_dasco_6.Name = "lbTb_dasco_6"
    Me.lbTb_dasco_6.NTSDbField = ""
    Me.lbTb_dasco_6.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_6.TabIndex = 5
    Me.lbTb_dasco_6.Text = "Da :"
    '
    'lbTb_dasco_5
    '
    Me.lbTb_dasco_5.AutoSize = True
    Me.lbTb_dasco_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_5.Location = New System.Drawing.Point(4, 111)
    Me.lbTb_dasco_5.Name = "lbTb_dasco_5"
    Me.lbTb_dasco_5.NTSDbField = ""
    Me.lbTb_dasco_5.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_5.TabIndex = 4
    Me.lbTb_dasco_5.Text = "Da :"
    '
    'lbTb_dasco_4
    '
    Me.lbTb_dasco_4.AutoSize = True
    Me.lbTb_dasco_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_4.Location = New System.Drawing.Point(4, 85)
    Me.lbTb_dasco_4.Name = "lbTb_dasco_4"
    Me.lbTb_dasco_4.NTSDbField = ""
    Me.lbTb_dasco_4.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_4.TabIndex = 3
    Me.lbTb_dasco_4.Text = "Da :"
    '
    'lbTb_dasco_3
    '
    Me.lbTb_dasco_3.AutoSize = True
    Me.lbTb_dasco_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_3.Location = New System.Drawing.Point(4, 59)
    Me.lbTb_dasco_3.Name = "lbTb_dasco_3"
    Me.lbTb_dasco_3.NTSDbField = ""
    Me.lbTb_dasco_3.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_3.TabIndex = 2
    Me.lbTb_dasco_3.Text = "Da :"
    '
    'lbTb_dasco_2
    '
    Me.lbTb_dasco_2.AutoSize = True
    Me.lbTb_dasco_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_2.Location = New System.Drawing.Point(4, 33)
    Me.lbTb_dasco_2.Name = "lbTb_dasco_2"
    Me.lbTb_dasco_2.NTSDbField = ""
    Me.lbTb_dasco_2.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_2.TabIndex = 1
    Me.lbTb_dasco_2.Text = "Da :"
    '
    'lbTb_dasco_1
    '
    Me.lbTb_dasco_1.AutoSize = True
    Me.lbTb_dasco_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_1.Location = New System.Drawing.Point(4, 7)
    Me.lbTb_dasco_1.Name = "lbTb_dasco_1"
    Me.lbTb_dasco_1.NTSDbField = ""
    Me.lbTb_dasco_1.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_1.TabIndex = 0
    Me.lbTb_dasco_1.Text = "Da :"
    '
    'tabSconti712
    '
    Me.tabSconti712.AllowDrop = True
    Me.tabSconti712.Controls.Add(Me.pnScpr712)
    Me.tabSconti712.Enable = True
    Me.tabSconti712.Name = "tabSconti712"
    Me.tabSconti712.Size = New System.Drawing.Size(505, 166)
    Me.tabSconti712.Text = "Sconti (7 - 12)"
    '
    'pnScpr712
    '
    Me.pnScpr712.AllowDrop = True
    Me.pnScpr712.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnScpr712.Appearance.Options.UseBackColor = True
    Me.pnScpr712.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_12)
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_11)
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_10)
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_9)
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_8)
    Me.pnScpr712.Controls.Add(Me.edTb_pripa_7)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_12)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_11)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_10)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_9)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_8)
    Me.pnScpr712.Controls.Add(Me.lbTb_pripa_7)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_12)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_11)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_10)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_9)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_8)
    Me.pnScpr712.Controls.Add(Me.edTb_pprov_7)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_12)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_11)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_10)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_9)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_8)
    Me.pnScpr712.Controls.Add(Me.lbTb_pprov_7)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_12)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_11)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_10)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_9)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_8)
    Me.pnScpr712.Controls.Add(Me.edTb_asco_7)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_12)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_11)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_10)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_9)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_8)
    Me.pnScpr712.Controls.Add(Me.lbTb_asco_7)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_12)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_11)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_10)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_9)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_8)
    Me.pnScpr712.Controls.Add(Me.edTb_dasco_7)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_12)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_11)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_10)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_9)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_8)
    Me.pnScpr712.Controls.Add(Me.lbTb_dasco_7)
    Me.pnScpr712.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnScpr712.Location = New System.Drawing.Point(4, 4)
    Me.pnScpr712.Name = "pnScpr712"
    Me.pnScpr712.Size = New System.Drawing.Size(498, 161)
    Me.pnScpr712.TabIndex = 1
    Me.pnScpr712.Text = "NtsPanel1"
    '
    'edTb_pripa_12
    '
    Me.edTb_pripa_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_12.Location = New System.Drawing.Point(431, 134)
    Me.edTb_pripa_12.Name = "edTb_pripa_12"
    Me.edTb_pripa_12.NTSDbField = ""
    Me.edTb_pripa_12.NTSFormat = "0"
    Me.edTb_pripa_12.NTSForzaVisZoom = False
    Me.edTb_pripa_12.NTSOldValue = ""
    Me.edTb_pripa_12.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_12.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_12.Properties.MaxLength = 65536
    Me.edTb_pripa_12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_12.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_12.TabIndex = 47
    '
    'edTb_pripa_11
    '
    Me.edTb_pripa_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_11.Location = New System.Drawing.Point(431, 108)
    Me.edTb_pripa_11.Name = "edTb_pripa_11"
    Me.edTb_pripa_11.NTSDbField = ""
    Me.edTb_pripa_11.NTSFormat = "0"
    Me.edTb_pripa_11.NTSForzaVisZoom = False
    Me.edTb_pripa_11.NTSOldValue = ""
    Me.edTb_pripa_11.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_11.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_11.Properties.MaxLength = 65536
    Me.edTb_pripa_11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_11.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_11.TabIndex = 46
    '
    'edTb_pripa_10
    '
    Me.edTb_pripa_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_10.Location = New System.Drawing.Point(431, 82)
    Me.edTb_pripa_10.Name = "edTb_pripa_10"
    Me.edTb_pripa_10.NTSDbField = ""
    Me.edTb_pripa_10.NTSFormat = "0"
    Me.edTb_pripa_10.NTSForzaVisZoom = False
    Me.edTb_pripa_10.NTSOldValue = ""
    Me.edTb_pripa_10.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_10.Properties.MaxLength = 65536
    Me.edTb_pripa_10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_10.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_10.TabIndex = 45
    '
    'edTb_pripa_9
    '
    Me.edTb_pripa_9.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_pripa_9.Location = New System.Drawing.Point(431, 56)
    Me.edTb_pripa_9.Name = "edTb_pripa_9"
    Me.edTb_pripa_9.NTSDbField = ""
    Me.edTb_pripa_9.NTSFormat = "0"
    Me.edTb_pripa_9.NTSForzaVisZoom = False
    Me.edTb_pripa_9.NTSOldValue = ""
    Me.edTb_pripa_9.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_9.Properties.MaxLength = 65536
    Me.edTb_pripa_9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_9.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_9.TabIndex = 44
    '
    'edTb_pripa_8
    '
    Me.edTb_pripa_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_8.Location = New System.Drawing.Point(431, 30)
    Me.edTb_pripa_8.Name = "edTb_pripa_8"
    Me.edTb_pripa_8.NTSDbField = ""
    Me.edTb_pripa_8.NTSFormat = "0"
    Me.edTb_pripa_8.NTSForzaVisZoom = False
    Me.edTb_pripa_8.NTSOldValue = ""
    Me.edTb_pripa_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_8.Properties.MaxLength = 65536
    Me.edTb_pripa_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_8.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_8.TabIndex = 43
    '
    'edTb_pripa_7
    '
    Me.edTb_pripa_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pripa_7.Location = New System.Drawing.Point(431, 4)
    Me.edTb_pripa_7.Name = "edTb_pripa_7"
    Me.edTb_pripa_7.NTSDbField = ""
    Me.edTb_pripa_7.NTSFormat = "0"
    Me.edTb_pripa_7.NTSForzaVisZoom = False
    Me.edTb_pripa_7.NTSOldValue = ""
    Me.edTb_pripa_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pripa_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pripa_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pripa_7.Properties.MaxLength = 65536
    Me.edTb_pripa_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pripa_7.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pripa_7.TabIndex = 42
    '
    'lbTb_pripa_12
    '
    Me.lbTb_pripa_12.AutoSize = True
    Me.lbTb_pripa_12.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_12.Location = New System.Drawing.Point(355, 137)
    Me.lbTb_pripa_12.Name = "lbTb_pripa_12"
    Me.lbTb_pripa_12.NTSDbField = ""
    Me.lbTb_pripa_12.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_12.TabIndex = 41
    Me.lbTb_pripa_12.Text = "+ % ripartita"
    '
    'lbTb_pripa_11
    '
    Me.lbTb_pripa_11.AutoSize = True
    Me.lbTb_pripa_11.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_11.Location = New System.Drawing.Point(355, 111)
    Me.lbTb_pripa_11.Name = "lbTb_pripa_11"
    Me.lbTb_pripa_11.NTSDbField = ""
    Me.lbTb_pripa_11.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_11.TabIndex = 40
    Me.lbTb_pripa_11.Text = "+ % ripartita"
    '
    'lbTb_pripa_10
    '
    Me.lbTb_pripa_10.AutoSize = True
    Me.lbTb_pripa_10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_10.Location = New System.Drawing.Point(355, 85)
    Me.lbTb_pripa_10.Name = "lbTb_pripa_10"
    Me.lbTb_pripa_10.NTSDbField = ""
    Me.lbTb_pripa_10.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_10.TabIndex = 39
    Me.lbTb_pripa_10.Text = "+ % ripartita"
    '
    'lbTb_pripa_9
    '
    Me.lbTb_pripa_9.AutoSize = True
    Me.lbTb_pripa_9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_9.Location = New System.Drawing.Point(355, 59)
    Me.lbTb_pripa_9.Name = "lbTb_pripa_9"
    Me.lbTb_pripa_9.NTSDbField = ""
    Me.lbTb_pripa_9.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_9.TabIndex = 38
    Me.lbTb_pripa_9.Text = "+ % ripartita"
    '
    'lbTb_pripa_8
    '
    Me.lbTb_pripa_8.AutoSize = True
    Me.lbTb_pripa_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_8.Location = New System.Drawing.Point(355, 33)
    Me.lbTb_pripa_8.Name = "lbTb_pripa_8"
    Me.lbTb_pripa_8.NTSDbField = ""
    Me.lbTb_pripa_8.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_8.TabIndex = 37
    Me.lbTb_pripa_8.Text = "+ % ripartita"
    '
    'lbTb_pripa_7
    '
    Me.lbTb_pripa_7.AutoSize = True
    Me.lbTb_pripa_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pripa_7.Location = New System.Drawing.Point(355, 7)
    Me.lbTb_pripa_7.Name = "lbTb_pripa_7"
    Me.lbTb_pripa_7.NTSDbField = ""
    Me.lbTb_pripa_7.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_pripa_7.TabIndex = 36
    Me.lbTb_pripa_7.Text = "+ % ripartita"
    '
    'edTb_pprov_12
    '
    Me.edTb_pprov_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_12.Location = New System.Drawing.Point(288, 134)
    Me.edTb_pprov_12.Name = "edTb_pprov_12"
    Me.edTb_pprov_12.NTSDbField = ""
    Me.edTb_pprov_12.NTSFormat = "0"
    Me.edTb_pprov_12.NTSForzaVisZoom = False
    Me.edTb_pprov_12.NTSOldValue = ""
    Me.edTb_pprov_12.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_12.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_12.Properties.MaxLength = 65536
    Me.edTb_pprov_12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_12.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_12.TabIndex = 35
    '
    'edTb_pprov_11
    '
    Me.edTb_pprov_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_11.Location = New System.Drawing.Point(288, 108)
    Me.edTb_pprov_11.Name = "edTb_pprov_11"
    Me.edTb_pprov_11.NTSDbField = ""
    Me.edTb_pprov_11.NTSFormat = "0"
    Me.edTb_pprov_11.NTSForzaVisZoom = False
    Me.edTb_pprov_11.NTSOldValue = ""
    Me.edTb_pprov_11.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_11.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_11.Properties.MaxLength = 65536
    Me.edTb_pprov_11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_11.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_11.TabIndex = 34
    '
    'edTb_pprov_10
    '
    Me.edTb_pprov_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_10.Location = New System.Drawing.Point(288, 82)
    Me.edTb_pprov_10.Name = "edTb_pprov_10"
    Me.edTb_pprov_10.NTSDbField = ""
    Me.edTb_pprov_10.NTSFormat = "0"
    Me.edTb_pprov_10.NTSForzaVisZoom = False
    Me.edTb_pprov_10.NTSOldValue = ""
    Me.edTb_pprov_10.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_10.Properties.MaxLength = 65536
    Me.edTb_pprov_10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_10.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_10.TabIndex = 33
    '
    'edTb_pprov_9
    '
    Me.edTb_pprov_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_9.Location = New System.Drawing.Point(288, 56)
    Me.edTb_pprov_9.Name = "edTb_pprov_9"
    Me.edTb_pprov_9.NTSDbField = ""
    Me.edTb_pprov_9.NTSFormat = "0"
    Me.edTb_pprov_9.NTSForzaVisZoom = False
    Me.edTb_pprov_9.NTSOldValue = ""
    Me.edTb_pprov_9.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_9.Properties.MaxLength = 65536
    Me.edTb_pprov_9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_9.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_9.TabIndex = 32
    '
    'edTb_pprov_8
    '
    Me.edTb_pprov_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pprov_8.Location = New System.Drawing.Point(288, 30)
    Me.edTb_pprov_8.Name = "edTb_pprov_8"
    Me.edTb_pprov_8.NTSDbField = ""
    Me.edTb_pprov_8.NTSFormat = "0"
    Me.edTb_pprov_8.NTSForzaVisZoom = False
    Me.edTb_pprov_8.NTSOldValue = ""
    Me.edTb_pprov_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_8.Properties.MaxLength = 65536
    Me.edTb_pprov_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_8.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_8.TabIndex = 31
    '
    'edTb_pprov_7
    '
    Me.edTb_pprov_7.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_pprov_7.Location = New System.Drawing.Point(288, 4)
    Me.edTb_pprov_7.Name = "edTb_pprov_7"
    Me.edTb_pprov_7.NTSDbField = ""
    Me.edTb_pprov_7.NTSFormat = "0"
    Me.edTb_pprov_7.NTSForzaVisZoom = False
    Me.edTb_pprov_7.NTSOldValue = ""
    Me.edTb_pprov_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pprov_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pprov_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pprov_7.Properties.MaxLength = 65536
    Me.edTb_pprov_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pprov_7.Size = New System.Drawing.Size(61, 20)
    Me.edTb_pprov_7.TabIndex = 30
    '
    'lbTb_pprov_12
    '
    Me.lbTb_pprov_12.AutoSize = True
    Me.lbTb_pprov_12.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_12.Location = New System.Drawing.Point(198, 137)
    Me.lbTb_pprov_12.Name = "lbTb_pprov_12"
    Me.lbTb_pprov_12.NTSDbField = ""
    Me.lbTb_pprov_12.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_12.TabIndex = 29
    Me.lbTb_pprov_12.Text = "% provvigione :"
    '
    'lbTb_pprov_11
    '
    Me.lbTb_pprov_11.AutoSize = True
    Me.lbTb_pprov_11.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_11.Location = New System.Drawing.Point(198, 111)
    Me.lbTb_pprov_11.Name = "lbTb_pprov_11"
    Me.lbTb_pprov_11.NTSDbField = ""
    Me.lbTb_pprov_11.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_11.TabIndex = 28
    Me.lbTb_pprov_11.Text = "% provvigione :"
    '
    'lbTb_pprov_10
    '
    Me.lbTb_pprov_10.AutoSize = True
    Me.lbTb_pprov_10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_10.Location = New System.Drawing.Point(198, 85)
    Me.lbTb_pprov_10.Name = "lbTb_pprov_10"
    Me.lbTb_pprov_10.NTSDbField = ""
    Me.lbTb_pprov_10.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_10.TabIndex = 27
    Me.lbTb_pprov_10.Text = "% provvigione :"
    '
    'lbTb_pprov_9
    '
    Me.lbTb_pprov_9.AutoSize = True
    Me.lbTb_pprov_9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_9.Location = New System.Drawing.Point(198, 59)
    Me.lbTb_pprov_9.Name = "lbTb_pprov_9"
    Me.lbTb_pprov_9.NTSDbField = ""
    Me.lbTb_pprov_9.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_9.TabIndex = 26
    Me.lbTb_pprov_9.Text = "% provvigione :"
    '
    'lbTb_pprov_8
    '
    Me.lbTb_pprov_8.AutoSize = True
    Me.lbTb_pprov_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_8.Location = New System.Drawing.Point(198, 33)
    Me.lbTb_pprov_8.Name = "lbTb_pprov_8"
    Me.lbTb_pprov_8.NTSDbField = ""
    Me.lbTb_pprov_8.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_8.TabIndex = 25
    Me.lbTb_pprov_8.Text = "% provvigione :"
    '
    'lbTb_pprov_7
    '
    Me.lbTb_pprov_7.AutoSize = True
    Me.lbTb_pprov_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pprov_7.Location = New System.Drawing.Point(198, 7)
    Me.lbTb_pprov_7.Name = "lbTb_pprov_7"
    Me.lbTb_pprov_7.NTSDbField = ""
    Me.lbTb_pprov_7.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_pprov_7.TabIndex = 24
    Me.lbTb_pprov_7.Text = "% provvigione :"
    '
    'edTb_asco_12
    '
    Me.edTb_asco_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_12.Location = New System.Drawing.Point(131, 134)
    Me.edTb_asco_12.Name = "edTb_asco_12"
    Me.edTb_asco_12.NTSDbField = ""
    Me.edTb_asco_12.NTSFormat = "0"
    Me.edTb_asco_12.NTSForzaVisZoom = False
    Me.edTb_asco_12.NTSOldValue = ""
    Me.edTb_asco_12.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_12.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_12.Properties.MaxLength = 65536
    Me.edTb_asco_12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_12.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_12.TabIndex = 23
    '
    'edTb_asco_11
    '
    Me.edTb_asco_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_11.Location = New System.Drawing.Point(131, 108)
    Me.edTb_asco_11.Name = "edTb_asco_11"
    Me.edTb_asco_11.NTSDbField = ""
    Me.edTb_asco_11.NTSFormat = "0"
    Me.edTb_asco_11.NTSForzaVisZoom = False
    Me.edTb_asco_11.NTSOldValue = ""
    Me.edTb_asco_11.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_11.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_11.Properties.MaxLength = 65536
    Me.edTb_asco_11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_11.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_11.TabIndex = 22
    '
    'edTb_asco_10
    '
    Me.edTb_asco_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_10.Location = New System.Drawing.Point(131, 82)
    Me.edTb_asco_10.Name = "edTb_asco_10"
    Me.edTb_asco_10.NTSDbField = ""
    Me.edTb_asco_10.NTSFormat = "0"
    Me.edTb_asco_10.NTSForzaVisZoom = False
    Me.edTb_asco_10.NTSOldValue = ""
    Me.edTb_asco_10.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_10.Properties.MaxLength = 65536
    Me.edTb_asco_10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_10.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_10.TabIndex = 21
    '
    'edTb_asco_9
    '
    Me.edTb_asco_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_9.Location = New System.Drawing.Point(131, 56)
    Me.edTb_asco_9.Name = "edTb_asco_9"
    Me.edTb_asco_9.NTSDbField = ""
    Me.edTb_asco_9.NTSFormat = "0"
    Me.edTb_asco_9.NTSForzaVisZoom = False
    Me.edTb_asco_9.NTSOldValue = ""
    Me.edTb_asco_9.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_9.Properties.MaxLength = 65536
    Me.edTb_asco_9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_9.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_9.TabIndex = 20
    '
    'edTb_asco_8
    '
    Me.edTb_asco_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_8.Location = New System.Drawing.Point(131, 30)
    Me.edTb_asco_8.Name = "edTb_asco_8"
    Me.edTb_asco_8.NTSDbField = ""
    Me.edTb_asco_8.NTSFormat = "0"
    Me.edTb_asco_8.NTSForzaVisZoom = False
    Me.edTb_asco_8.NTSOldValue = ""
    Me.edTb_asco_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_8.Properties.MaxLength = 65536
    Me.edTb_asco_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_8.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_8.TabIndex = 19
    '
    'edTb_asco_7
    '
    Me.edTb_asco_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_asco_7.Location = New System.Drawing.Point(131, 4)
    Me.edTb_asco_7.Name = "edTb_asco_7"
    Me.edTb_asco_7.NTSDbField = ""
    Me.edTb_asco_7.NTSFormat = "0"
    Me.edTb_asco_7.NTSForzaVisZoom = False
    Me.edTb_asco_7.NTSOldValue = ""
    Me.edTb_asco_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_asco_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_asco_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_asco_7.Properties.MaxLength = 65536
    Me.edTb_asco_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_asco_7.Size = New System.Drawing.Size(61, 20)
    Me.edTb_asco_7.TabIndex = 18
    '
    'lbTb_asco_12
    '
    Me.lbTb_asco_12.AutoSize = True
    Me.lbTb_asco_12.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_12.Location = New System.Drawing.Point(105, 137)
    Me.lbTb_asco_12.Name = "lbTb_asco_12"
    Me.lbTb_asco_12.NTSDbField = ""
    Me.lbTb_asco_12.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_12.TabIndex = 17
    Me.lbTb_asco_12.Text = "a :"
    '
    'lbTb_asco_11
    '
    Me.lbTb_asco_11.AutoSize = True
    Me.lbTb_asco_11.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_11.Location = New System.Drawing.Point(105, 111)
    Me.lbTb_asco_11.Name = "lbTb_asco_11"
    Me.lbTb_asco_11.NTSDbField = ""
    Me.lbTb_asco_11.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_11.TabIndex = 16
    Me.lbTb_asco_11.Text = "a :"
    '
    'lbTb_asco_10
    '
    Me.lbTb_asco_10.AutoSize = True
    Me.lbTb_asco_10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_10.Location = New System.Drawing.Point(105, 85)
    Me.lbTb_asco_10.Name = "lbTb_asco_10"
    Me.lbTb_asco_10.NTSDbField = ""
    Me.lbTb_asco_10.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_10.TabIndex = 15
    Me.lbTb_asco_10.Text = "a :"
    '
    'lbTb_asco_9
    '
    Me.lbTb_asco_9.AutoSize = True
    Me.lbTb_asco_9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_9.Location = New System.Drawing.Point(105, 59)
    Me.lbTb_asco_9.Name = "lbTb_asco_9"
    Me.lbTb_asco_9.NTSDbField = ""
    Me.lbTb_asco_9.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_9.TabIndex = 14
    Me.lbTb_asco_9.Text = "a :"
    '
    'lbTb_asco_8
    '
    Me.lbTb_asco_8.AutoSize = True
    Me.lbTb_asco_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_8.Location = New System.Drawing.Point(105, 33)
    Me.lbTb_asco_8.Name = "lbTb_asco_8"
    Me.lbTb_asco_8.NTSDbField = ""
    Me.lbTb_asco_8.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_8.TabIndex = 13
    Me.lbTb_asco_8.Text = "a :"
    '
    'lbTb_asco_7
    '
    Me.lbTb_asco_7.AutoSize = True
    Me.lbTb_asco_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_asco_7.Location = New System.Drawing.Point(105, 7)
    Me.lbTb_asco_7.Name = "lbTb_asco_7"
    Me.lbTb_asco_7.NTSDbField = ""
    Me.lbTb_asco_7.Size = New System.Drawing.Size(20, 13)
    Me.lbTb_asco_7.TabIndex = 12
    Me.lbTb_asco_7.Text = "a :"
    '
    'edTb_dasco_12
    '
    Me.edTb_dasco_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_12.Location = New System.Drawing.Point(38, 134)
    Me.edTb_dasco_12.Name = "edTb_dasco_12"
    Me.edTb_dasco_12.NTSDbField = ""
    Me.edTb_dasco_12.NTSFormat = "0"
    Me.edTb_dasco_12.NTSForzaVisZoom = False
    Me.edTb_dasco_12.NTSOldValue = ""
    Me.edTb_dasco_12.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_12.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_12.Properties.MaxLength = 65536
    Me.edTb_dasco_12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_12.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_12.TabIndex = 11
    '
    'edTb_dasco_11
    '
    Me.edTb_dasco_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_11.Location = New System.Drawing.Point(38, 108)
    Me.edTb_dasco_11.Name = "edTb_dasco_11"
    Me.edTb_dasco_11.NTSDbField = ""
    Me.edTb_dasco_11.NTSFormat = "0"
    Me.edTb_dasco_11.NTSForzaVisZoom = False
    Me.edTb_dasco_11.NTSOldValue = ""
    Me.edTb_dasco_11.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_11.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_11.Properties.MaxLength = 65536
    Me.edTb_dasco_11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_11.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_11.TabIndex = 10
    '
    'edTb_dasco_10
    '
    Me.edTb_dasco_10.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_dasco_10.Location = New System.Drawing.Point(38, 82)
    Me.edTb_dasco_10.Name = "edTb_dasco_10"
    Me.edTb_dasco_10.NTSDbField = ""
    Me.edTb_dasco_10.NTSFormat = "0"
    Me.edTb_dasco_10.NTSForzaVisZoom = False
    Me.edTb_dasco_10.NTSOldValue = ""
    Me.edTb_dasco_10.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_10.Properties.MaxLength = 65536
    Me.edTb_dasco_10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_10.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_10.TabIndex = 9
    '
    'edTb_dasco_9
    '
    Me.edTb_dasco_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_9.Location = New System.Drawing.Point(38, 56)
    Me.edTb_dasco_9.Name = "edTb_dasco_9"
    Me.edTb_dasco_9.NTSDbField = ""
    Me.edTb_dasco_9.NTSFormat = "0"
    Me.edTb_dasco_9.NTSForzaVisZoom = False
    Me.edTb_dasco_9.NTSOldValue = ""
    Me.edTb_dasco_9.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_9.Properties.MaxLength = 65536
    Me.edTb_dasco_9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_9.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_9.TabIndex = 8
    '
    'edTb_dasco_8
    '
    Me.edTb_dasco_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_8.Location = New System.Drawing.Point(38, 30)
    Me.edTb_dasco_8.Name = "edTb_dasco_8"
    Me.edTb_dasco_8.NTSDbField = ""
    Me.edTb_dasco_8.NTSFormat = "0"
    Me.edTb_dasco_8.NTSForzaVisZoom = False
    Me.edTb_dasco_8.NTSOldValue = ""
    Me.edTb_dasco_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_8.Properties.MaxLength = 65536
    Me.edTb_dasco_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_8.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_8.TabIndex = 7
    '
    'edTb_dasco_7
    '
    Me.edTb_dasco_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dasco_7.Location = New System.Drawing.Point(38, 4)
    Me.edTb_dasco_7.Name = "edTb_dasco_7"
    Me.edTb_dasco_7.NTSDbField = ""
    Me.edTb_dasco_7.NTSFormat = "0"
    Me.edTb_dasco_7.NTSForzaVisZoom = False
    Me.edTb_dasco_7.NTSOldValue = ""
    Me.edTb_dasco_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_dasco_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_dasco_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dasco_7.Properties.MaxLength = 65536
    Me.edTb_dasco_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dasco_7.Size = New System.Drawing.Size(61, 20)
    Me.edTb_dasco_7.TabIndex = 6
    '
    'lbTb_dasco_12
    '
    Me.lbTb_dasco_12.AutoSize = True
    Me.lbTb_dasco_12.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_12.Location = New System.Drawing.Point(4, 137)
    Me.lbTb_dasco_12.Name = "lbTb_dasco_12"
    Me.lbTb_dasco_12.NTSDbField = ""
    Me.lbTb_dasco_12.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_12.TabIndex = 5
    Me.lbTb_dasco_12.Text = "Da :"
    '
    'lbTb_dasco_11
    '
    Me.lbTb_dasco_11.AutoSize = True
    Me.lbTb_dasco_11.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_11.Location = New System.Drawing.Point(4, 111)
    Me.lbTb_dasco_11.Name = "lbTb_dasco_11"
    Me.lbTb_dasco_11.NTSDbField = ""
    Me.lbTb_dasco_11.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_11.TabIndex = 4
    Me.lbTb_dasco_11.Text = "Da :"
    '
    'lbTb_dasco_10
    '
    Me.lbTb_dasco_10.AutoSize = True
    Me.lbTb_dasco_10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_10.Location = New System.Drawing.Point(4, 85)
    Me.lbTb_dasco_10.Name = "lbTb_dasco_10"
    Me.lbTb_dasco_10.NTSDbField = ""
    Me.lbTb_dasco_10.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_10.TabIndex = 3
    Me.lbTb_dasco_10.Text = "Da :"
    '
    'lbTb_dasco_9
    '
    Me.lbTb_dasco_9.AutoSize = True
    Me.lbTb_dasco_9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_9.Location = New System.Drawing.Point(4, 59)
    Me.lbTb_dasco_9.Name = "lbTb_dasco_9"
    Me.lbTb_dasco_9.NTSDbField = ""
    Me.lbTb_dasco_9.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_9.TabIndex = 2
    Me.lbTb_dasco_9.Text = "Da :"
    '
    'lbTb_dasco_8
    '
    Me.lbTb_dasco_8.AutoSize = True
    Me.lbTb_dasco_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_8.Location = New System.Drawing.Point(4, 33)
    Me.lbTb_dasco_8.Name = "lbTb_dasco_8"
    Me.lbTb_dasco_8.NTSDbField = ""
    Me.lbTb_dasco_8.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_8.TabIndex = 1
    Me.lbTb_dasco_8.Text = "Da :"
    '
    'lbTb_dasco_7
    '
    Me.lbTb_dasco_7.AutoSize = True
    Me.lbTb_dasco_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dasco_7.Location = New System.Drawing.Point(4, 7)
    Me.lbTb_dasco_7.Name = "lbTb_dasco_7"
    Me.lbTb_dasco_7.NTSDbField = ""
    Me.lbTb_dasco_7.Size = New System.Drawing.Size(27, 13)
    Me.lbTb_dasco_7.TabIndex = 0
    Me.lbTb_dasco_7.Text = "Da :"
    '
    'FRMPRSCPR
    '
    Me.ClientSize = New System.Drawing.Size(539, 304)
    Me.Controls.Add(Me.tabScpr)
    Me.Controls.Add(Me.pnFrame)
    Me.Controls.Add(Me.lbTb_codscpr)
    Me.Controls.Add(Me.lbTb_desscpr)
    Me.Controls.Add(Me.edTb_codscpr)
    Me.Controls.Add(Me.edTb_desscpr)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMPRSCPR"
    Me.Text = "Relazioni Sconti/Provvigioni"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desscpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codscpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFrame, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFrame.ResumeLayout(False)
    Me.pnFrame.PerformLayout()
    CType(Me.edTb_lisscpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opXx_listino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opXx_ultcos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tabScpr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabScpr.ResumeLayout(False)
    Me.tabSconti16.ResumeLayout(False)
    CType(Me.pnScpr16, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnScpr16.ResumeLayout(False)
    Me.pnScpr16.PerformLayout()
    CType(Me.edTb_pripa_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabSconti712.ResumeLayout(False)
    CType(Me.pnScpr712, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnScpr712.ResumeLayout(False)
    Me.pnScpr712.PerformLayout()
    CType(Me.edTb_pripa_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pripa_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pprov_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_asco_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dasco_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    Dim strOpzRegSCPR As String = ""
    Dim iMaxLenCodice As Integer = 0
    Dim lMaxRangeCodice As Integer = 0

    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      edTb_pripa_6.NTSSetParam(oMenu, oApp.Tr(Me, 128589367488750000, "% riparita (6)"), "0.00", 7, -999, 999)
      edTb_pripa_5.NTSSetParam(oMenu, oApp.Tr(Me, 128589367488906250, "% riparita (5)"), "0.00", 7, -999, 999)
      edTb_pripa_4.NTSSetParam(oMenu, oApp.Tr(Me, 128589367489062500, "% riparita (4)"), "0.00", 7, -999, 999)
      edTb_pripa_3.NTSSetParam(oMenu, oApp.Tr(Me, 128589367489218750, "% riparita (3)"), "0.00", 7, -999, 999)
      edTb_pripa_2.NTSSetParam(oMenu, oApp.Tr(Me, 128589367489375000, "% riparita (2)"), "0.00", 7, -999, 999)
      edTb_pripa_1.NTSSetParam(oMenu, oApp.Tr(Me, 128589367489531250, "% riparita (1)"), "0.00", 7, -999, 999)
      edTb_pprov_6.NTSSetParam(oMenu, oApp.Tr(Me, 128589367490625000, "% provvigione (6)"), "0.00", 7, -999, 999)
      edTb_pprov_5.NTSSetParam(oMenu, oApp.Tr(Me, 128589367490781250, "% provvigione (5)"), "0.00", 7, -999, 999)
      edTb_pprov_4.NTSSetParam(oMenu, oApp.Tr(Me, 128589367490937500, "% provvigione (4)"), "0.00", 7, -999, 999)
      edTb_pprov_3.NTSSetParam(oMenu, oApp.Tr(Me, 128589367491093750, "% provvigione (3)"), "0.00", 7, -999, 999)
      edTb_pprov_2.NTSSetParam(oMenu, oApp.Tr(Me, 128589367491250000, "% provvigione (2)"), "0.00", 7, -999, 999)
      edTb_pprov_1.NTSSetParam(oMenu, oApp.Tr(Me, 128589367491406250, "% provvigione (1)"), "0.00", 7, -999, 999)
      edTb_asco_6.NTSSetParam(oMenu, oApp.Tr(Me, 128589367492500000, "a sconto (6)"), "0.00", 7, -999, 999)
      edTb_asco_5.NTSSetParam(oMenu, oApp.Tr(Me, 128589367492656250, "a sconto (5)"), "0.00", 7, -999, 999)
      edTb_asco_4.NTSSetParam(oMenu, oApp.Tr(Me, 128589367492812500, "a sconto (4)"), "0.00", 7, -999, 999)
      edTb_asco_3.NTSSetParam(oMenu, oApp.Tr(Me, 128589367492968750, "a sconto (3)"), "0.00", 7, -999, 999)
      edTb_asco_2.NTSSetParam(oMenu, oApp.Tr(Me, 128589367493125000, "a sconto (2)"), "0.00", 7, -999, 999)
      edTb_asco_1.NTSSetParam(oMenu, oApp.Tr(Me, 128589367493281250, "a sconto (1)"), "0.00", 7, -999, 999)
      edTb_dasco_6.NTSSetParam(oMenu, oApp.Tr(Me, 128589367494375000, "da sconto (6)"), "0.00", 7, -999, 999)
      edTb_dasco_5.NTSSetParam(oMenu, oApp.Tr(Me, 128589367494531250, "da sconto (5)"), "0.00", 7, -999, 999)
      edTb_dasco_4.NTSSetParam(oMenu, oApp.Tr(Me, 128589367494687500, "da sconto (4)"), "0.00", 7, -999, 999)
      edTb_dasco_3.NTSSetParam(oMenu, oApp.Tr(Me, 128589367494843750, "da sconto (3)"), "0.00", 7, -999, 999)
      edTb_dasco_2.NTSSetParam(oMenu, oApp.Tr(Me, 128589367495000000, "da sconto (2)"), "0.00", 7, -999, 999)
      edTb_dasco_1.NTSSetParam(oMenu, oApp.Tr(Me, 128589367495156250, "da sconto (1)"), "0.00", 7, -999, 999)
      edTb_pripa_12.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496093750, "% riparita (12)"), "0.00", 7, -999, 999)
      edTb_pripa_11.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496250000, "% riparita (11)"), "0.00", 7, -999, 999)
      edTb_pripa_10.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496406250, "% riparita (10)"), "0.00", 7, -999, 999)
      edTb_pripa_9.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496562500, "% riparita (9)"), "0.00", 7, -999, 999)
      edTb_pripa_8.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496718750, "% riparita (8)"), "0.00", 7, -999, 999)
      edTb_pripa_7.NTSSetParam(oMenu, oApp.Tr(Me, 128589367496875000, "% riparita (7)"), "0.00", 7, -999, 999)
      edTb_pprov_12.NTSSetParam(oMenu, oApp.Tr(Me, 128589367497968750, "% provvigione (12)"), "0.00", 7, -999, 999)
      edTb_pprov_11.NTSSetParam(oMenu, oApp.Tr(Me, 128589367498125000, "% provvigione (11)"), "0.00", 7, -999, 999)
      edTb_pprov_10.NTSSetParam(oMenu, oApp.Tr(Me, 128589367498281250, "% provvigione (10)"), "0.00", 7, -999, 999)
      edTb_pprov_9.NTSSetParam(oMenu, oApp.Tr(Me, 128589367498437500, "% provvigione (9)"), "0.00", 7, -999, 999)
      edTb_pprov_8.NTSSetParam(oMenu, oApp.Tr(Me, 128589367498593750, "% provvigione (8)"), "0.00", 7, -999, 999)
      edTb_pprov_7.NTSSetParam(oMenu, oApp.Tr(Me, 128589367498750000, "% provvigione (7)"), "0.00", 7, -999, 999)
      edTb_asco_12.NTSSetParam(oMenu, oApp.Tr(Me, 128589367499843750, "a sconto (12)"), "0.00", 7, -999, 999)
      edTb_asco_11.NTSSetParam(oMenu, oApp.Tr(Me, 128589367500000000, "a sconto (11)"), "0.00", 7, -999, 999)
      edTb_asco_10.NTSSetParam(oMenu, oApp.Tr(Me, 128589367500156250, "a sconto (10)"), "0.00", 7, -999, 999)
      edTb_asco_9.NTSSetParam(oMenu, oApp.Tr(Me, 128589367500312500, "a sconto (9)"), "0.00", 7, -999, 999)
      edTb_asco_8.NTSSetParam(oMenu, oApp.Tr(Me, 128589367500468750, "a sconto (8)"), "0.00", 7, -999, 999)
      edTb_asco_7.NTSSetParam(oMenu, oApp.Tr(Me, 128589367500625000, "a sconto (7)"), "0.00", 7, -999, 999)
      edTb_dasco_12.NTSSetParam(oMenu, oApp.Tr(Me, 128589367501718750, "da sconto (12)"), "0.00", 7, -999, 999)
      edTb_dasco_11.NTSSetParam(oMenu, oApp.Tr(Me, 128589367501875000, "da sconto (11)"), "0.00", 7, -999, 999)
      edTb_dasco_10.NTSSetParam(oMenu, oApp.Tr(Me, 128589367502031250, "da sconto (10)"), "0.00", 7, -999, 999)
      edTb_dasco_9.NTSSetParam(oMenu, oApp.Tr(Me, 128589367502187500, "da sconto (9)"), "0.00", 7, -999, 999)
      edTb_dasco_8.NTSSetParam(oMenu, oApp.Tr(Me, 128589367502343750, "da sconto (8)"), "0.00", 7, -999, 999)
      edTb_dasco_7.NTSSetParam(oMenu, oApp.Tr(Me, 128589367502500000, "da sconto (7)"), "0.00", 7, -999, 999)
      edTb_lisscpr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128589367503437500, "Numero Listino"), tablist)
      edTb_lisscpr.NTSSetRichiesto()
      opXx_listino.NTSSetParam(oMenu, oApp.Tr(Me, 128589367503593750, "Listino"), "N")
      opXx_ultcos.NTSSetParam(oMenu, oApp.Tr(Me, 128589367503750000, "Ultimo Costo d'Acquisto"), "S")
      edTb_desscpr.NTSSetParam(oMenu, oApp.Tr(Me, 128589367504218750, "Descrizione"), 20)
      edTb_desscpr.NTSForzaVisZoom = True

      strOpzRegSCPR = oMenu.GetSettingBus("Bsveboll", "Opzioni", ".", "Opzscpr", "ClasseArticolo", " ", "ClasseArticolo")
      Select Case strOpzRegSCPR
        Case "AgenteClasseArticoloAAAAC"
          iMaxLenCodice = 5
          lMaxRangeCodice = 99999
        Case "AgenteClasseArticoloAAAACC"
          iMaxLenCodice = 6
          lMaxRangeCodice = 999999
        Case "AgenteClasseArticoloAAAACCC", "AgentePromozioneAAAAPPP"
          iMaxLenCodice = 7
          lMaxRangeCodice = 9999999
        Case "AgenteClassePromozioneAAACCCPPP"
          iMaxLenCodice = 9
          lMaxRangeCodice = 999999999
        Case Else
          iMaxLenCodice = 4
          lMaxRangeCodice = 9999
      End Select
      edTb_codscpr.NTSSetParam(oMenu, oApp.Tr(Me, 128589367504062500, "Codice"), "0", iMaxLenCodice, 0, lMaxRangeCodice)

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMPRSCPR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      '-------------------------------------------------
      If Not oCleScpr.Apri(DittaCorrente, dsScpr) Then Me.Close()
      dcScpr.DataSource = dsScpr.Tables("TABSCPR")
      dsScpr.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      '-------------------------------------------------
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      If Not oPar Is Nothing Then
        If oPar.strParam.Trim <> "" Then
          If oPar.strParam.IndexOf("APRI;") <> -1 Then
            Dim lCod As Integer = NTSCInt(oPar.strParam.Split(";"c)(1))
            For z As Integer = 0 To dcScpr.Count - 1
              If NTSCInt(dsScpr.Tables("TABSCPR").Rows(z)!tb_codscpr) = lCod Then
                dcScpr.Position = z
                Exit For
              End If
            Next
          End If
        End If
      End If

      '-------------------------------------------------
      'all'avvio blocco il codice descrizione e imposto i radio button
      edTb_codscpr.Enabled = False
      ModificaOpt()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMPRSCPR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then e.Cancel = True
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMPRSCPR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcScpr.Dispose()
      dsScpr.Dispose()
    Catch
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

      edTb_pripa_6.NTSDbField = "tabscpr.tb_pripa_6"
      edTb_pripa_5.NTSDbField = "tabscpr.tb_pripa_5"
      edTb_pripa_4.NTSDbField = "tabscpr.tb_pripa_4"
      edTb_pripa_3.NTSDbField = "tabscpr.tb_pripa_3"
      edTb_pripa_2.NTSDbField = "tabscpr.tb_pripa_2"
      edTb_pripa_1.NTSDbField = "tabscpr.tb_pripa_1"
      edTb_pprov_6.NTSDbField = "tabscpr.tb_pprov_6"
      edTb_pprov_5.NTSDbField = "tabscpr.tb_pprov_5"
      edTb_pprov_4.NTSDbField = "tabscpr.tb_pprov_4"
      edTb_pprov_3.NTSDbField = "tabscpr.tb_pprov_3"
      edTb_pprov_2.NTSDbField = "tabscpr.tb_pprov_2"
      edTb_pprov_1.NTSDbField = "tabscpr.tb_pprov_1"
      edTb_asco_6.NTSDbField = "tabscpr.tb_asco_6"
      edTb_asco_5.NTSDbField = "tabscpr.tb_asco_5"
      edTb_asco_4.NTSDbField = "tabscpr.tb_asco_4"
      edTb_asco_3.NTSDbField = "tabscpr.tb_asco_3"
      edTb_asco_2.NTSDbField = "tabscpr.tb_asco_2"
      edTb_asco_1.NTSDbField = "tabscpr.tb_asco_1"
      edTb_dasco_6.NTSDbField = "tabscpr.tb_dasco_6"
      edTb_dasco_5.NTSDbField = "tabscpr.tb_dasco_5"
      edTb_dasco_4.NTSDbField = "tabscpr.tb_dasco_4"
      edTb_dasco_3.NTSDbField = "tabscpr.tb_dasco_3"
      edTb_dasco_2.NTSDbField = "tabscpr.tb_dasco_2"
      edTb_dasco_1.NTSDbField = "tabscpr.tb_dasco_1"
      edTb_pripa_12.NTSDbField = "tabscpr.tb_pripa_12"
      edTb_pripa_11.NTSDbField = "tabscpr.tb_pripa_11"
      edTb_pripa_10.NTSDbField = "tabscpr.tb_pripa_10"
      edTb_pripa_9.NTSDbField = "tabscpr.tb_pripa_9"
      edTb_pripa_8.NTSDbField = "tabscpr.tb_pripa_8"
      edTb_pripa_7.NTSDbField = "tabscpr.tb_pripa_7"
      edTb_pprov_12.NTSDbField = "tabscpr.tb_pprov_12"
      edTb_pprov_11.NTSDbField = "tabscpr.tb_pprov_11"
      edTb_pprov_10.NTSDbField = "tabscpr.tb_pprov_10"
      edTb_pprov_9.NTSDbField = "tabscpr.tb_pprov_9"
      edTb_pprov_8.NTSDbField = "tabscpr.tb_pprov_8"
      edTb_pprov_7.NTSDbField = "tabscpr.tb_pprov_7"
      edTb_asco_12.NTSDbField = "tabscpr.tb_asco_12"
      edTb_asco_11.NTSDbField = "tabscpr.tb_asco_11"
      edTb_asco_10.NTSDbField = "tabscpr.tb_asco_10"
      edTb_asco_9.NTSDbField = "tabscpr.tb_asco_9"
      edTb_asco_8.NTSDbField = "tabscpr.tb_asco_8"
      edTb_asco_7.NTSDbField = "tabscpr.tb_asco_7"
      edTb_dasco_12.NTSDbField = "tabscpr.tb_dasco_12"
      edTb_dasco_11.NTSDbField = "tabscpr.tb_dasco_11"
      edTb_dasco_10.NTSDbField = "tabscpr.tb_dasco_10"
      edTb_dasco_9.NTSDbField = "tabscpr.tb_dasco_9"
      edTb_dasco_8.NTSDbField = "tabscpr.tb_dasco_8"
      edTb_dasco_7.NTSDbField = "tabscpr.tb_dasco_7"
      edTb_lisscpr.NTSDbField = "tabscpr.tb_lisscpr"
      edTb_codscpr.NTSDbField = "tabscpr.tb_codscpr"
      edTb_desscpr.NTSDbField = "tabscpr.tb_desscpr"
      opXx_ultcos.NTSText.NTSDbField = "tabscpr.xx_ultcos"
      opXx_listino.NTSText.NTSDbField = "tabscpr.xx_ultcos"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcScpr, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo un nuovo record
      If Not Salva() Then Return
      oCleScpr.Nuovo()
      dcScpr.MoveLast()
      edTb_codscpr.Enabled = True
      edTb_codscpr.Focus()
      opXx_ultcos.Checked = True
      edTb_lisscpr.Enabled = False

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

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
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'cancello
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589410075156250, "Confermare la cancellazione del record?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsScpr.Tables("TABSCPR").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcScpr.RemoveAt(dcScpr.Position)
          oCleScpr.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcScpr, Me)
            bRemovBinding = False
            edTb_codscpr.Enabled = True
          Else
            edTb_codscpr.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcScpr, Me)
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
      'ripristino
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589410346250000, "Ripristinare le modifiche apportate al record?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsScpr.Tables("TABSCPR").Rows.Count = 1 And dsScpr.Tables("TABSCPR").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleScpr.Ripristina(dcScpr.Position, dcScpr.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcScpr, Me)
            bRemovBinding = False
            edTb_codscpr.Enabled = True
          Else
            edTb_codscpr.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcScpr, Me)
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
      If edTb_desscpr.Focused Then
        'lancio lo zoom solo per riposizionare il BindingSource
        Dim oParam As New CLE__PATB

        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!
        If oCleScpr.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128589411616718750, "Salvare o ripristinare le modifiche prima di selezionare un nuovo record"))
          Return
        End If

        NTSZOOM.strIn = edTb_codscpr.Text
        NTSZOOM.ZoomStrIn("ZOOMTABSCPR", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codscpr.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcScpr.List.Count - 1
            If CType(dcScpr.Item(i), DataRowView)!tb_codScpr.ToString = NTSZOOM.strIn Then
              dcScpr.Position = i
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcScpr.MoveFirst()

      ModificaOpt()
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcScpr.MovePrevious()

      ModificaOpt()
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcScpr.MoveNext()

      ModificaOpt()
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      dcScpr.MoveLast()

      ModificaOpt()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleScpr.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589412364687500, "Salvo le relazioni sconti/provvigioni?"))
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleScpr.Salva(False) Then Return False
          If dsScpr.Tables("TABSCPR").Rows.Count > 0 Then
            edTb_codscpr.Enabled = False
          End If
        End If
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ModificaOpt()
    ' Abilita o disabilita il campo Listino
    If edTb_lisscpr.Text = "0" Then
      edTb_lisscpr.Enabled = False
    Else
      edTb_lisscpr.Enabled = True
    End If
  End Sub

  Public Overridable Sub opXx_listino_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opXx_listino.Click
    edTb_lisscpr.Enabled = True
  End Sub

  Public Overridable Sub opXx_ultcos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opXx_ultcos.Click
    edTb_lisscpr.Enabled = False
  End Sub
End Class
