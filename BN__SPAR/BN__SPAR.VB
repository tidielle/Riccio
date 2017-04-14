Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SPAR

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

#Region "Variabili"
  Public dsSpar As DataSet
  Public oCleSpar As CLE__SPAR
  Public oCallParams As CLE__CLDP
  Public dcSpar As BindingSource = New BindingSource
  Private components As System.ComponentModel.IContainer

  Public bAnagen As Boolean

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOrderByDescr As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnSpar As NTSInformatica.NTSPanel
  Public WithEvents cbParstag As NTSInformatica.NTSComboBox
  Public WithEvents lbParstag As NTSInformatica.NTSLabel
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Val1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Op1 As NTSInformatica.NTSLabel
  Public WithEvents lb5 As NTSInformatica.NTSLabel
  Public WithEvents lb4 As NTSInformatica.NTSLabel
  Public WithEvents lb3 As NTSInformatica.NTSLabel
  Public WithEvents lb2 As NTSInformatica.NTSLabel
  Public WithEvents lb1 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Val5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op5 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op4 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op3 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op2 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op1 As NTSInformatica.NTSComboBox
  Public WithEvents lbPz_Descam1 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descam5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lb10 As NTSInformatica.NTSLabel
  Public WithEvents lb9 As NTSInformatica.NTSLabel
  Public WithEvents lb8 As NTSInformatica.NTSLabel
  Public WithEvents lb7 As NTSInformatica.NTSLabel
  Public WithEvents lb6 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Val10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op10 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op9 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op8 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op7 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op6 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descam10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_strOprel As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDisplayfiltri As NTSInformatica.NTSMemoBox
  Public WithEvents cmdRicarica As NTSInformatica.NTSButton
  Public WithEvents lb15 As NTSInformatica.NTSLabel
  Public WithEvents lb14 As NTSInformatica.NTSLabel
  Public WithEvents lb13 As NTSInformatica.NTSLabel
  Public WithEvents lb12 As NTSInformatica.NTSLabel
  Public WithEvents lb11 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Val15 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val14 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val13 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val12 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val11 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op15 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op14 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op13 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op12 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op11 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descam15 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam14 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam13 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam12 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam11 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdNuovaStampa As NTSInformatica.NTSButton
  Public WithEvents lbSubQuery As NTSInformatica.NTSLabel
  Public WithEvents lbs3 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Vals3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops3 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descams3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbs2 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Vals2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops2 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descams2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbs1 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Vals1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops1 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descams1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbs4 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Vals4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops4 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descams4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbs5 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Vals5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops5 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Descams5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_strOprel2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDisplayfiltriS As NTSInformatica.NTSMemoBox
  Public WithEvents cmdZoomStampe As NTSInformatica.NTSButton
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__SPAR", "BE__SPAR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128326757174560148, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSpar = CType(oTmp, CLE__SPAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__SPAR", strRemoteServer, strRemotePort)
    AddHandler oCleSpar.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSpar.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__SPAR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrderByDescr = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnSpar = New NTSInformatica.NTSPanel
    Me.cmdZoomStampe = New NTSInformatica.NTSButton
    Me.cmdNuovaStampa = New NTSInformatica.NTSButton
    Me.cmdRicarica = New NTSInformatica.NTSButton
    Me.cbParstag = New NTSInformatica.NTSComboBox
    Me.lbParstag = New NTSInformatica.NTSLabel
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.lbs5 = New NTSInformatica.NTSLabel
    Me.edPz_Vals5 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops5 = New NTSInformatica.NTSComboBox
    Me.edPz_Descams5 = New NTSInformatica.NTSTextBoxStr
    Me.lbs4 = New NTSInformatica.NTSLabel
    Me.edPz_Vals4 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops4 = New NTSInformatica.NTSComboBox
    Me.edPz_Descams4 = New NTSInformatica.NTSTextBoxStr
    Me.lbs3 = New NTSInformatica.NTSLabel
    Me.edPz_Vals3 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops3 = New NTSInformatica.NTSComboBox
    Me.edPz_Descams3 = New NTSInformatica.NTSTextBoxStr
    Me.lbs2 = New NTSInformatica.NTSLabel
    Me.edPz_Vals2 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops2 = New NTSInformatica.NTSComboBox
    Me.edPz_Descams2 = New NTSInformatica.NTSTextBoxStr
    Me.lbs1 = New NTSInformatica.NTSLabel
    Me.edPz_Vals1 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops1 = New NTSInformatica.NTSComboBox
    Me.edPz_Descams1 = New NTSInformatica.NTSTextBoxStr
    Me.lbSubQuery = New NTSInformatica.NTSLabel
    Me.lb15 = New NTSInformatica.NTSLabel
    Me.edPz_Val15 = New NTSInformatica.NTSTextBoxStr
    Me.lb6 = New NTSInformatica.NTSLabel
    Me.edPz_Val14 = New NTSInformatica.NTSTextBoxStr
    Me.lb14 = New NTSInformatica.NTSLabel
    Me.cbPz_Op9 = New NTSInformatica.NTSComboBox
    Me.edPz_Val13 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam6 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op10 = New NTSInformatica.NTSComboBox
    Me.edPz_Descam7 = New NTSInformatica.NTSTextBoxStr
    Me.lb5 = New NTSInformatica.NTSLabel
    Me.edPz_Val12 = New NTSInformatica.NTSTextBoxStr
    Me.lb8 = New NTSInformatica.NTSLabel
    Me.lb4 = New NTSInformatica.NTSLabel
    Me.edPz_Val9 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam8 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val11 = New NTSInformatica.NTSTextBoxStr
    Me.lb13 = New NTSInformatica.NTSLabel
    Me.edPz_Val10 = New NTSInformatica.NTSTextBoxStr
    Me.lb3 = New NTSInformatica.NTSLabel
    Me.cbPz_Op15 = New NTSInformatica.NTSComboBox
    Me.lb7 = New NTSInformatica.NTSLabel
    Me.cbPz_Op14 = New NTSInformatica.NTSComboBox
    Me.lb2 = New NTSInformatica.NTSLabel
    Me.cbPz_Op13 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op6 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op12 = New NTSInformatica.NTSComboBox
    Me.lb1 = New NTSInformatica.NTSLabel
    Me.cbPz_Op11 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op7 = New NTSInformatica.NTSComboBox
    Me.lb12 = New NTSInformatica.NTSLabel
    Me.edPz_Val5 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op8 = New NTSInformatica.NTSComboBox
    Me.edPz_Descam9 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam14 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val6 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val3 = New NTSInformatica.NTSTextBoxStr
    Me.lb11 = New NTSInformatica.NTSLabel
    Me.edPz_Val8 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam15 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val7 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val1 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam13 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op5 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op4 = New NTSInformatica.NTSComboBox
    Me.edPz_Descam12 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op3 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op2 = New NTSInformatica.NTSComboBox
    Me.edPz_Descam10 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op1 = New NTSInformatica.NTSComboBox
    Me.edPz_Descam11 = New NTSInformatica.NTSTextBoxStr
    Me.lb10 = New NTSInformatica.NTSLabel
    Me.edPz_Descam5 = New NTSInformatica.NTSTextBoxStr
    Me.lb9 = New NTSInformatica.NTSLabel
    Me.edPz_Descam4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam1 = New NTSInformatica.NTSTextBoxStr
    Me.edDisplayfiltriS = New NTSInformatica.NTSMemoBox
    Me.edPz_strOprel2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_strOprel = New NTSInformatica.NTSTextBoxStr
    Me.edDisplayfiltri = New NTSInformatica.NTSMemoBox
    Me.lbPz_Val1 = New NTSInformatica.NTSLabel
    Me.lbPz_Descam1 = New NTSInformatica.NTSLabel
    Me.lbPz_Op1 = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSpar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSpar.SuspendLayout()
    CType(Me.cbParstag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.edPz_Vals5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDisplayfiltriS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_strOprel2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_strOprel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDisplayfiltri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaGriglia, Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbCambioDitta, Me.tlbOrderByDescr, Me.tlbImpostaStampante, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 0
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 1
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "StampaGriglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.GlyphPath = ""
    Me.tlbStampaGriglia.Id = 2
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 3
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 10
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 7
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrderByDescr), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "CambioDitta"
    Me.tlbCambioDitta.GlyphPath = ""
    Me.tlbCambioDitta.Id = 8
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.Visible = True
    '
    'tlbOrderByDescr
    '
    Me.tlbOrderByDescr.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbOrderByDescr.Caption = "Ordina per Descrizione"
    Me.tlbOrderByDescr.GlyphPath = ""
    Me.tlbOrderByDescr.Id = 8
    Me.tlbOrderByDescr.Name = "tlbOrderByDescr"
    Me.tlbOrderByDescr.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "ImpostaStampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 9
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 5
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 6
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnSpar
    '
    Me.pnSpar.AllowDrop = True
    Me.pnSpar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSpar.Appearance.Options.UseBackColor = True
    Me.pnSpar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSpar.Controls.Add(Me.cmdZoomStampe)
    Me.pnSpar.Controls.Add(Me.cmdNuovaStampa)
    Me.pnSpar.Controls.Add(Me.cmdRicarica)
    Me.pnSpar.Controls.Add(Me.cbParstag)
    Me.pnSpar.Controls.Add(Me.lbParstag)
    Me.pnSpar.Controls.Add(Me.fmFiltri)
    Me.pnSpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSpar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSpar.Location = New System.Drawing.Point(0, 30)
    Me.pnSpar.Name = "pnSpar"
    Me.pnSpar.NTSActiveTrasparency = True
    Me.pnSpar.Size = New System.Drawing.Size(654, 524)
    Me.pnSpar.TabIndex = 9
    Me.pnSpar.Text = "NtsPanel1"
    '
    'cmdZoomStampe
    '
    Me.cmdZoomStampe.ImagePath = ""
    Me.cmdZoomStampe.ImageText = ""
    Me.cmdZoomStampe.Location = New System.Drawing.Point(348, 4)
    Me.cmdZoomStampe.Name = "cmdZoomStampe"
    Me.cmdZoomStampe.NTSContextMenu = Nothing
    Me.cmdZoomStampe.Size = New System.Drawing.Size(24, 24)
    Me.cmdZoomStampe.TabIndex = 14
    Me.cmdZoomStampe.Text = "..."
    '
    'cmdNuovaStampa
    '
    Me.cmdNuovaStampa.ImagePath = ""
    Me.cmdNuovaStampa.ImageText = ""
    Me.cmdNuovaStampa.Location = New System.Drawing.Point(484, 4)
    Me.cmdNuovaStampa.Name = "cmdNuovaStampa"
    Me.cmdNuovaStampa.NTSContextMenu = Nothing
    Me.cmdNuovaStampa.Size = New System.Drawing.Size(158, 24)
    Me.cmdNuovaStampa.TabIndex = 13
    Me.cmdNuovaStampa.Text = "&Imposta nuova stampa"
    '
    'cmdRicarica
    '
    Me.cmdRicarica.ImagePath = ""
    Me.cmdRicarica.ImageText = ""
    Me.cmdRicarica.Location = New System.Drawing.Point(378, 4)
    Me.cmdRicarica.Name = "cmdRicarica"
    Me.cmdRicarica.NTSContextMenu = Nothing
    Me.cmdRicarica.Size = New System.Drawing.Size(100, 24)
    Me.cmdRicarica.TabIndex = 12
    Me.cmdRicarica.Text = "&Ricarica lista"
    '
    'cbParstag
    '
    Me.cbParstag.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbParstag.DataSource = Nothing
    Me.cbParstag.DisplayMember = ""
    Me.cbParstag.Location = New System.Drawing.Point(114, 4)
    Me.cbParstag.Name = "cbParstag"
    Me.cbParstag.NTSDbField = ""
    Me.cbParstag.Properties.AutoHeight = False
    Me.cbParstag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbParstag.Properties.DropDownRows = 30
    Me.cbParstag.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbParstag.SelectedValue = ""
    Me.cbParstag.Size = New System.Drawing.Size(228, 24)
    Me.cbParstag.TabIndex = 11
    Me.cbParstag.ValueMember = ""
    '
    'lbParstag
    '
    Me.lbParstag.AutoSize = True
    Me.lbParstag.BackColor = System.Drawing.Color.Transparent
    Me.lbParstag.Location = New System.Drawing.Point(12, 10)
    Me.lbParstag.Name = "lbParstag"
    Me.lbParstag.NTSDbField = ""
    Me.lbParstag.Size = New System.Drawing.Size(96, 13)
    Me.lbParstag.TabIndex = 10
    Me.lbParstag.Text = "Formato di stampa"
    Me.lbParstag.Tooltip = ""
    Me.lbParstag.UseMnemonic = False
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.pnPag1)
    Me.fmFiltri.Controls.Add(Me.edDisplayfiltriS)
    Me.fmFiltri.Controls.Add(Me.edPz_strOprel2)
    Me.fmFiltri.Controls.Add(Me.edPz_strOprel)
    Me.fmFiltri.Controls.Add(Me.edDisplayfiltri)
    Me.fmFiltri.Controls.Add(Me.lbPz_Val1)
    Me.fmFiltri.Controls.Add(Me.lbPz_Descam1)
    Me.fmFiltri.Controls.Add(Me.lbPz_Op1)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(12, 32)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(630, 489)
    Me.fmFiltri.TabIndex = 9
    Me.fmFiltri.Text = "FILTRI UTENTE:"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.lbs5)
    Me.pnPag1.Controls.Add(Me.edPz_Vals5)
    Me.pnPag1.Controls.Add(Me.cbPz_Ops5)
    Me.pnPag1.Controls.Add(Me.edPz_Descams5)
    Me.pnPag1.Controls.Add(Me.lbs4)
    Me.pnPag1.Controls.Add(Me.edPz_Vals4)
    Me.pnPag1.Controls.Add(Me.cbPz_Ops4)
    Me.pnPag1.Controls.Add(Me.edPz_Descams4)
    Me.pnPag1.Controls.Add(Me.lbs3)
    Me.pnPag1.Controls.Add(Me.edPz_Vals3)
    Me.pnPag1.Controls.Add(Me.cbPz_Ops3)
    Me.pnPag1.Controls.Add(Me.edPz_Descams3)
    Me.pnPag1.Controls.Add(Me.lbs2)
    Me.pnPag1.Controls.Add(Me.edPz_Vals2)
    Me.pnPag1.Controls.Add(Me.cbPz_Ops2)
    Me.pnPag1.Controls.Add(Me.edPz_Descams2)
    Me.pnPag1.Controls.Add(Me.lbs1)
    Me.pnPag1.Controls.Add(Me.edPz_Vals1)
    Me.pnPag1.Controls.Add(Me.cbPz_Ops1)
    Me.pnPag1.Controls.Add(Me.edPz_Descams1)
    Me.pnPag1.Controls.Add(Me.lbSubQuery)
    Me.pnPag1.Controls.Add(Me.lb15)
    Me.pnPag1.Controls.Add(Me.edPz_Val15)
    Me.pnPag1.Controls.Add(Me.lb6)
    Me.pnPag1.Controls.Add(Me.edPz_Val14)
    Me.pnPag1.Controls.Add(Me.lb14)
    Me.pnPag1.Controls.Add(Me.cbPz_Op9)
    Me.pnPag1.Controls.Add(Me.edPz_Val13)
    Me.pnPag1.Controls.Add(Me.edPz_Descam6)
    Me.pnPag1.Controls.Add(Me.cbPz_Op10)
    Me.pnPag1.Controls.Add(Me.edPz_Descam7)
    Me.pnPag1.Controls.Add(Me.lb5)
    Me.pnPag1.Controls.Add(Me.edPz_Val12)
    Me.pnPag1.Controls.Add(Me.lb8)
    Me.pnPag1.Controls.Add(Me.lb4)
    Me.pnPag1.Controls.Add(Me.edPz_Val9)
    Me.pnPag1.Controls.Add(Me.edPz_Descam8)
    Me.pnPag1.Controls.Add(Me.edPz_Val11)
    Me.pnPag1.Controls.Add(Me.lb13)
    Me.pnPag1.Controls.Add(Me.edPz_Val10)
    Me.pnPag1.Controls.Add(Me.lb3)
    Me.pnPag1.Controls.Add(Me.cbPz_Op15)
    Me.pnPag1.Controls.Add(Me.lb7)
    Me.pnPag1.Controls.Add(Me.cbPz_Op14)
    Me.pnPag1.Controls.Add(Me.lb2)
    Me.pnPag1.Controls.Add(Me.cbPz_Op13)
    Me.pnPag1.Controls.Add(Me.cbPz_Op6)
    Me.pnPag1.Controls.Add(Me.cbPz_Op12)
    Me.pnPag1.Controls.Add(Me.lb1)
    Me.pnPag1.Controls.Add(Me.cbPz_Op11)
    Me.pnPag1.Controls.Add(Me.cbPz_Op7)
    Me.pnPag1.Controls.Add(Me.lb12)
    Me.pnPag1.Controls.Add(Me.edPz_Val5)
    Me.pnPag1.Controls.Add(Me.cbPz_Op8)
    Me.pnPag1.Controls.Add(Me.edPz_Descam9)
    Me.pnPag1.Controls.Add(Me.edPz_Val4)
    Me.pnPag1.Controls.Add(Me.edPz_Descam14)
    Me.pnPag1.Controls.Add(Me.edPz_Val6)
    Me.pnPag1.Controls.Add(Me.edPz_Val3)
    Me.pnPag1.Controls.Add(Me.lb11)
    Me.pnPag1.Controls.Add(Me.edPz_Val8)
    Me.pnPag1.Controls.Add(Me.edPz_Val2)
    Me.pnPag1.Controls.Add(Me.edPz_Descam15)
    Me.pnPag1.Controls.Add(Me.edPz_Val7)
    Me.pnPag1.Controls.Add(Me.edPz_Val1)
    Me.pnPag1.Controls.Add(Me.edPz_Descam13)
    Me.pnPag1.Controls.Add(Me.cbPz_Op5)
    Me.pnPag1.Controls.Add(Me.cbPz_Op4)
    Me.pnPag1.Controls.Add(Me.edPz_Descam12)
    Me.pnPag1.Controls.Add(Me.cbPz_Op3)
    Me.pnPag1.Controls.Add(Me.cbPz_Op2)
    Me.pnPag1.Controls.Add(Me.edPz_Descam10)
    Me.pnPag1.Controls.Add(Me.cbPz_Op1)
    Me.pnPag1.Controls.Add(Me.edPz_Descam11)
    Me.pnPag1.Controls.Add(Me.lb10)
    Me.pnPag1.Controls.Add(Me.edPz_Descam5)
    Me.pnPag1.Controls.Add(Me.lb9)
    Me.pnPag1.Controls.Add(Me.edPz_Descam4)
    Me.pnPag1.Controls.Add(Me.edPz_Descam3)
    Me.pnPag1.Controls.Add(Me.edPz_Descam2)
    Me.pnPag1.Controls.Add(Me.edPz_Descam1)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag1.Location = New System.Drawing.Point(2, 20)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(626, 467)
    Me.pnPag1.TabIndex = 6
    Me.pnPag1.Text = "NtsPanel1"
    '
    'lbs5
    '
    Me.lbs5.AutoSize = True
    Me.lbs5.BackColor = System.Drawing.Color.Transparent
    Me.lbs5.Location = New System.Drawing.Point(9, 444)
    Me.lbs5.Name = "lbs5"
    Me.lbs5.NTSDbField = ""
    Me.lbs5.Size = New System.Drawing.Size(13, 13)
    Me.lbs5.TabIndex = 86
    Me.lbs5.Text = "5"
    Me.lbs5.Tooltip = ""
    Me.lbs5.UseMnemonic = False
    '
    'edPz_Vals5
    '
    Me.edPz_Vals5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals5.Location = New System.Drawing.Point(454, 441)
    Me.edPz_Vals5.Name = "edPz_Vals5"
    Me.edPz_Vals5.NTSDbField = ""
    Me.edPz_Vals5.NTSForzaVisZoom = False
    Me.edPz_Vals5.NTSOldValue = ""
    Me.edPz_Vals5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals5.Properties.AutoHeight = False
    Me.edPz_Vals5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals5.Properties.MaxLength = 65536
    Me.edPz_Vals5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals5.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Vals5.TabIndex = 85
    '
    'cbPz_Ops5
    '
    Me.cbPz_Ops5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops5.DataSource = Nothing
    Me.cbPz_Ops5.DisplayMember = ""
    Me.cbPz_Ops5.Location = New System.Drawing.Point(358, 441)
    Me.cbPz_Ops5.Name = "cbPz_Ops5"
    Me.cbPz_Ops5.NTSDbField = ""
    Me.cbPz_Ops5.Properties.AutoHeight = False
    Me.cbPz_Ops5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops5.Properties.DropDownRows = 30
    Me.cbPz_Ops5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops5.SelectedValue = ""
    Me.cbPz_Ops5.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Ops5.TabIndex = 84
    Me.cbPz_Ops5.ValueMember = ""
    '
    'edPz_Descams5
    '
    Me.edPz_Descams5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams5.Enabled = False
    Me.edPz_Descams5.Location = New System.Drawing.Point(25, 441)
    Me.edPz_Descams5.Name = "edPz_Descams5"
    Me.edPz_Descams5.NTSDbField = ""
    Me.edPz_Descams5.NTSForzaVisZoom = False
    Me.edPz_Descams5.NTSOldValue = ""
    Me.edPz_Descams5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams5.Properties.AutoHeight = False
    Me.edPz_Descams5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams5.Properties.MaxLength = 65536
    Me.edPz_Descams5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams5.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descams5.TabIndex = 83
    '
    'lbs4
    '
    Me.lbs4.AutoSize = True
    Me.lbs4.BackColor = System.Drawing.Color.Transparent
    Me.lbs4.Location = New System.Drawing.Point(9, 422)
    Me.lbs4.Name = "lbs4"
    Me.lbs4.NTSDbField = ""
    Me.lbs4.Size = New System.Drawing.Size(13, 13)
    Me.lbs4.TabIndex = 82
    Me.lbs4.Text = "4"
    Me.lbs4.Tooltip = ""
    Me.lbs4.UseMnemonic = False
    '
    'edPz_Vals4
    '
    Me.edPz_Vals4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals4.Location = New System.Drawing.Point(454, 419)
    Me.edPz_Vals4.Name = "edPz_Vals4"
    Me.edPz_Vals4.NTSDbField = ""
    Me.edPz_Vals4.NTSForzaVisZoom = False
    Me.edPz_Vals4.NTSOldValue = ""
    Me.edPz_Vals4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals4.Properties.AutoHeight = False
    Me.edPz_Vals4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals4.Properties.MaxLength = 65536
    Me.edPz_Vals4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals4.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Vals4.TabIndex = 81
    '
    'cbPz_Ops4
    '
    Me.cbPz_Ops4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops4.DataSource = Nothing
    Me.cbPz_Ops4.DisplayMember = ""
    Me.cbPz_Ops4.Location = New System.Drawing.Point(358, 419)
    Me.cbPz_Ops4.Name = "cbPz_Ops4"
    Me.cbPz_Ops4.NTSDbField = ""
    Me.cbPz_Ops4.Properties.AutoHeight = False
    Me.cbPz_Ops4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops4.Properties.DropDownRows = 30
    Me.cbPz_Ops4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops4.SelectedValue = ""
    Me.cbPz_Ops4.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Ops4.TabIndex = 80
    Me.cbPz_Ops4.ValueMember = ""
    '
    'edPz_Descams4
    '
    Me.edPz_Descams4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams4.Enabled = False
    Me.edPz_Descams4.Location = New System.Drawing.Point(25, 419)
    Me.edPz_Descams4.Name = "edPz_Descams4"
    Me.edPz_Descams4.NTSDbField = ""
    Me.edPz_Descams4.NTSForzaVisZoom = False
    Me.edPz_Descams4.NTSOldValue = ""
    Me.edPz_Descams4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams4.Properties.AutoHeight = False
    Me.edPz_Descams4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams4.Properties.MaxLength = 65536
    Me.edPz_Descams4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams4.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descams4.TabIndex = 79
    '
    'lbs3
    '
    Me.lbs3.AutoSize = True
    Me.lbs3.BackColor = System.Drawing.Color.Transparent
    Me.lbs3.Location = New System.Drawing.Point(9, 400)
    Me.lbs3.Name = "lbs3"
    Me.lbs3.NTSDbField = ""
    Me.lbs3.Size = New System.Drawing.Size(13, 13)
    Me.lbs3.TabIndex = 78
    Me.lbs3.Text = "3"
    Me.lbs3.Tooltip = ""
    Me.lbs3.UseMnemonic = False
    '
    'edPz_Vals3
    '
    Me.edPz_Vals3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals3.Location = New System.Drawing.Point(454, 397)
    Me.edPz_Vals3.Name = "edPz_Vals3"
    Me.edPz_Vals3.NTSDbField = ""
    Me.edPz_Vals3.NTSForzaVisZoom = False
    Me.edPz_Vals3.NTSOldValue = ""
    Me.edPz_Vals3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals3.Properties.AutoHeight = False
    Me.edPz_Vals3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals3.Properties.MaxLength = 65536
    Me.edPz_Vals3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals3.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Vals3.TabIndex = 77
    '
    'cbPz_Ops3
    '
    Me.cbPz_Ops3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops3.DataSource = Nothing
    Me.cbPz_Ops3.DisplayMember = ""
    Me.cbPz_Ops3.Location = New System.Drawing.Point(358, 397)
    Me.cbPz_Ops3.Name = "cbPz_Ops3"
    Me.cbPz_Ops3.NTSDbField = ""
    Me.cbPz_Ops3.Properties.AutoHeight = False
    Me.cbPz_Ops3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops3.Properties.DropDownRows = 30
    Me.cbPz_Ops3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops3.SelectedValue = ""
    Me.cbPz_Ops3.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Ops3.TabIndex = 76
    Me.cbPz_Ops3.ValueMember = ""
    '
    'edPz_Descams3
    '
    Me.edPz_Descams3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams3.Enabled = False
    Me.edPz_Descams3.Location = New System.Drawing.Point(25, 397)
    Me.edPz_Descams3.Name = "edPz_Descams3"
    Me.edPz_Descams3.NTSDbField = ""
    Me.edPz_Descams3.NTSForzaVisZoom = False
    Me.edPz_Descams3.NTSOldValue = ""
    Me.edPz_Descams3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams3.Properties.AutoHeight = False
    Me.edPz_Descams3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams3.Properties.MaxLength = 65536
    Me.edPz_Descams3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams3.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descams3.TabIndex = 75
    '
    'lbs2
    '
    Me.lbs2.AutoSize = True
    Me.lbs2.BackColor = System.Drawing.Color.Transparent
    Me.lbs2.Location = New System.Drawing.Point(9, 378)
    Me.lbs2.Name = "lbs2"
    Me.lbs2.NTSDbField = ""
    Me.lbs2.Size = New System.Drawing.Size(13, 13)
    Me.lbs2.TabIndex = 74
    Me.lbs2.Text = "2"
    Me.lbs2.Tooltip = ""
    Me.lbs2.UseMnemonic = False
    '
    'edPz_Vals2
    '
    Me.edPz_Vals2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals2.Location = New System.Drawing.Point(454, 375)
    Me.edPz_Vals2.Name = "edPz_Vals2"
    Me.edPz_Vals2.NTSDbField = ""
    Me.edPz_Vals2.NTSForzaVisZoom = False
    Me.edPz_Vals2.NTSOldValue = ""
    Me.edPz_Vals2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals2.Properties.AutoHeight = False
    Me.edPz_Vals2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals2.Properties.MaxLength = 65536
    Me.edPz_Vals2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals2.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Vals2.TabIndex = 73
    '
    'cbPz_Ops2
    '
    Me.cbPz_Ops2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops2.DataSource = Nothing
    Me.cbPz_Ops2.DisplayMember = ""
    Me.cbPz_Ops2.Location = New System.Drawing.Point(358, 375)
    Me.cbPz_Ops2.Name = "cbPz_Ops2"
    Me.cbPz_Ops2.NTSDbField = ""
    Me.cbPz_Ops2.Properties.AutoHeight = False
    Me.cbPz_Ops2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops2.Properties.DropDownRows = 30
    Me.cbPz_Ops2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops2.SelectedValue = ""
    Me.cbPz_Ops2.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Ops2.TabIndex = 72
    Me.cbPz_Ops2.ValueMember = ""
    '
    'edPz_Descams2
    '
    Me.edPz_Descams2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams2.Enabled = False
    Me.edPz_Descams2.Location = New System.Drawing.Point(25, 375)
    Me.edPz_Descams2.Name = "edPz_Descams2"
    Me.edPz_Descams2.NTSDbField = ""
    Me.edPz_Descams2.NTSForzaVisZoom = False
    Me.edPz_Descams2.NTSOldValue = ""
    Me.edPz_Descams2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams2.Properties.AutoHeight = False
    Me.edPz_Descams2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams2.Properties.MaxLength = 65536
    Me.edPz_Descams2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams2.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descams2.TabIndex = 71
    '
    'lbs1
    '
    Me.lbs1.AutoSize = True
    Me.lbs1.BackColor = System.Drawing.Color.Transparent
    Me.lbs1.Location = New System.Drawing.Point(9, 356)
    Me.lbs1.Name = "lbs1"
    Me.lbs1.NTSDbField = ""
    Me.lbs1.Size = New System.Drawing.Size(13, 13)
    Me.lbs1.TabIndex = 70
    Me.lbs1.Text = "1"
    Me.lbs1.Tooltip = ""
    Me.lbs1.UseMnemonic = False
    '
    'edPz_Vals1
    '
    Me.edPz_Vals1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals1.Location = New System.Drawing.Point(454, 353)
    Me.edPz_Vals1.Name = "edPz_Vals1"
    Me.edPz_Vals1.NTSDbField = ""
    Me.edPz_Vals1.NTSForzaVisZoom = False
    Me.edPz_Vals1.NTSOldValue = ""
    Me.edPz_Vals1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals1.Properties.AutoHeight = False
    Me.edPz_Vals1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals1.Properties.MaxLength = 65536
    Me.edPz_Vals1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals1.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Vals1.TabIndex = 69
    '
    'cbPz_Ops1
    '
    Me.cbPz_Ops1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops1.DataSource = Nothing
    Me.cbPz_Ops1.DisplayMember = ""
    Me.cbPz_Ops1.Location = New System.Drawing.Point(358, 353)
    Me.cbPz_Ops1.Name = "cbPz_Ops1"
    Me.cbPz_Ops1.NTSDbField = ""
    Me.cbPz_Ops1.Properties.AutoHeight = False
    Me.cbPz_Ops1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops1.Properties.DropDownRows = 30
    Me.cbPz_Ops1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops1.SelectedValue = ""
    Me.cbPz_Ops1.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Ops1.TabIndex = 68
    Me.cbPz_Ops1.ValueMember = ""
    '
    'edPz_Descams1
    '
    Me.edPz_Descams1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams1.Enabled = False
    Me.edPz_Descams1.Location = New System.Drawing.Point(25, 353)
    Me.edPz_Descams1.Name = "edPz_Descams1"
    Me.edPz_Descams1.NTSDbField = ""
    Me.edPz_Descams1.NTSForzaVisZoom = False
    Me.edPz_Descams1.NTSOldValue = ""
    Me.edPz_Descams1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams1.Properties.AutoHeight = False
    Me.edPz_Descams1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams1.Properties.MaxLength = 65536
    Me.edPz_Descams1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams1.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descams1.TabIndex = 67
    '
    'lbSubQuery
    '
    Me.lbSubQuery.AutoSize = True
    Me.lbSubQuery.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQuery.Location = New System.Drawing.Point(3, 337)
    Me.lbSubQuery.Name = "lbSubQuery"
    Me.lbSubQuery.NTSDbField = ""
    Me.lbSubQuery.Size = New System.Drawing.Size(99, 13)
    Me.lbSubQuery.TabIndex = 66
    Me.lbSubQuery.Text = "FILTRI SUBQUERY:"
    Me.lbSubQuery.Tooltip = ""
    Me.lbSubQuery.UseMnemonic = False
    '
    'lb15
    '
    Me.lb15.AutoSize = True
    Me.lb15.BackColor = System.Drawing.Color.Transparent
    Me.lb15.Location = New System.Drawing.Point(3, 317)
    Me.lb15.Name = "lb15"
    Me.lb15.NTSDbField = ""
    Me.lb15.Size = New System.Drawing.Size(19, 13)
    Me.lb15.TabIndex = 65
    Me.lb15.Text = "15"
    Me.lb15.Tooltip = ""
    Me.lb15.UseMnemonic = False
    '
    'edPz_Val15
    '
    Me.edPz_Val15.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPz_Val15.Location = New System.Drawing.Point(454, 314)
    Me.edPz_Val15.Name = "edPz_Val15"
    Me.edPz_Val15.NTSDbField = ""
    Me.edPz_Val15.NTSForzaVisZoom = False
    Me.edPz_Val15.NTSOldValue = ""
    Me.edPz_Val15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val15.Properties.AutoHeight = False
    Me.edPz_Val15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val15.Properties.MaxLength = 65536
    Me.edPz_Val15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val15.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val15.TabIndex = 60
    '
    'lb6
    '
    Me.lb6.AutoSize = True
    Me.lb6.BackColor = System.Drawing.Color.Transparent
    Me.lb6.Location = New System.Drawing.Point(9, 119)
    Me.lb6.Name = "lb6"
    Me.lb6.NTSDbField = ""
    Me.lb6.Size = New System.Drawing.Size(13, 13)
    Me.lb6.TabIndex = 37
    Me.lb6.Text = "6"
    Me.lb6.Tooltip = ""
    Me.lb6.UseMnemonic = False
    '
    'edPz_Val14
    '
    Me.edPz_Val14.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val14.Location = New System.Drawing.Point(454, 292)
    Me.edPz_Val14.Name = "edPz_Val14"
    Me.edPz_Val14.NTSDbField = ""
    Me.edPz_Val14.NTSForzaVisZoom = False
    Me.edPz_Val14.NTSOldValue = ""
    Me.edPz_Val14.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val14.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val14.Properties.AutoHeight = False
    Me.edPz_Val14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val14.Properties.MaxLength = 65536
    Me.edPz_Val14.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val14.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val14.TabIndex = 59
    '
    'lb14
    '
    Me.lb14.AutoSize = True
    Me.lb14.BackColor = System.Drawing.Color.Transparent
    Me.lb14.Location = New System.Drawing.Point(3, 295)
    Me.lb14.Name = "lb14"
    Me.lb14.NTSDbField = ""
    Me.lb14.Size = New System.Drawing.Size(19, 13)
    Me.lb14.TabIndex = 64
    Me.lb14.Text = "14"
    Me.lb14.Tooltip = ""
    Me.lb14.UseMnemonic = False
    '
    'cbPz_Op9
    '
    Me.cbPz_Op9.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op9.DataSource = Nothing
    Me.cbPz_Op9.DisplayMember = ""
    Me.cbPz_Op9.Location = New System.Drawing.Point(358, 182)
    Me.cbPz_Op9.Name = "cbPz_Op9"
    Me.cbPz_Op9.NTSDbField = ""
    Me.cbPz_Op9.Properties.AutoHeight = False
    Me.cbPz_Op9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op9.Properties.DropDownRows = 30
    Me.cbPz_Op9.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op9.SelectedValue = ""
    Me.cbPz_Op9.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op9.TabIndex = 25
    Me.cbPz_Op9.ValueMember = ""
    '
    'edPz_Val13
    '
    Me.edPz_Val13.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val13.Location = New System.Drawing.Point(454, 270)
    Me.edPz_Val13.Name = "edPz_Val13"
    Me.edPz_Val13.NTSDbField = ""
    Me.edPz_Val13.NTSForzaVisZoom = False
    Me.edPz_Val13.NTSOldValue = ""
    Me.edPz_Val13.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val13.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val13.Properties.AutoHeight = False
    Me.edPz_Val13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val13.Properties.MaxLength = 65536
    Me.edPz_Val13.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val13.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val13.TabIndex = 58
    '
    'edPz_Descam6
    '
    Me.edPz_Descam6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam6.Enabled = False
    Me.edPz_Descam6.Location = New System.Drawing.Point(25, 116)
    Me.edPz_Descam6.Name = "edPz_Descam6"
    Me.edPz_Descam6.NTSDbField = ""
    Me.edPz_Descam6.NTSForzaVisZoom = False
    Me.edPz_Descam6.NTSOldValue = ""
    Me.edPz_Descam6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam6.Properties.AutoHeight = False
    Me.edPz_Descam6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam6.Properties.MaxLength = 65536
    Me.edPz_Descam6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam6.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam6.TabIndex = 6
    '
    'cbPz_Op10
    '
    Me.cbPz_Op10.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op10.DataSource = Nothing
    Me.cbPz_Op10.DisplayMember = ""
    Me.cbPz_Op10.Location = New System.Drawing.Point(358, 204)
    Me.cbPz_Op10.Name = "cbPz_Op10"
    Me.cbPz_Op10.NTSDbField = ""
    Me.cbPz_Op10.Properties.AutoHeight = False
    Me.cbPz_Op10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op10.Properties.DropDownRows = 30
    Me.cbPz_Op10.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op10.SelectedValue = ""
    Me.cbPz_Op10.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op10.TabIndex = 26
    Me.cbPz_Op10.ValueMember = ""
    '
    'edPz_Descam7
    '
    Me.edPz_Descam7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam7.Enabled = False
    Me.edPz_Descam7.Location = New System.Drawing.Point(25, 138)
    Me.edPz_Descam7.Name = "edPz_Descam7"
    Me.edPz_Descam7.NTSDbField = ""
    Me.edPz_Descam7.NTSForzaVisZoom = False
    Me.edPz_Descam7.NTSOldValue = ""
    Me.edPz_Descam7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam7.Properties.AutoHeight = False
    Me.edPz_Descam7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam7.Properties.MaxLength = 65536
    Me.edPz_Descam7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam7.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam7.TabIndex = 7
    '
    'lb5
    '
    Me.lb5.AutoSize = True
    Me.lb5.BackColor = System.Drawing.Color.Transparent
    Me.lb5.Location = New System.Drawing.Point(9, 97)
    Me.lb5.Name = "lb5"
    Me.lb5.NTSDbField = ""
    Me.lb5.Size = New System.Drawing.Size(13, 13)
    Me.lb5.TabIndex = 41
    Me.lb5.Text = "5"
    Me.lb5.Tooltip = ""
    Me.lb5.UseMnemonic = False
    '
    'edPz_Val12
    '
    Me.edPz_Val12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val12.Location = New System.Drawing.Point(454, 248)
    Me.edPz_Val12.Name = "edPz_Val12"
    Me.edPz_Val12.NTSDbField = ""
    Me.edPz_Val12.NTSForzaVisZoom = False
    Me.edPz_Val12.NTSOldValue = ""
    Me.edPz_Val12.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val12.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val12.Properties.AutoHeight = False
    Me.edPz_Val12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val12.Properties.MaxLength = 65536
    Me.edPz_Val12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val12.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val12.TabIndex = 57
    '
    'lb8
    '
    Me.lb8.AutoSize = True
    Me.lb8.BackColor = System.Drawing.Color.Transparent
    Me.lb8.Location = New System.Drawing.Point(9, 163)
    Me.lb8.Name = "lb8"
    Me.lb8.NTSDbField = ""
    Me.lb8.Size = New System.Drawing.Size(13, 13)
    Me.lb8.TabIndex = 39
    Me.lb8.Text = "8"
    Me.lb8.Tooltip = ""
    Me.lb8.UseMnemonic = False
    '
    'lb4
    '
    Me.lb4.AutoSize = True
    Me.lb4.BackColor = System.Drawing.Color.Transparent
    Me.lb4.Location = New System.Drawing.Point(9, 75)
    Me.lb4.Name = "lb4"
    Me.lb4.NTSDbField = ""
    Me.lb4.Size = New System.Drawing.Size(13, 13)
    Me.lb4.TabIndex = 40
    Me.lb4.Text = "4"
    Me.lb4.Tooltip = ""
    Me.lb4.UseMnemonic = False
    '
    'edPz_Val9
    '
    Me.edPz_Val9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val9.Location = New System.Drawing.Point(454, 182)
    Me.edPz_Val9.Name = "edPz_Val9"
    Me.edPz_Val9.NTSDbField = ""
    Me.edPz_Val9.NTSForzaVisZoom = False
    Me.edPz_Val9.NTSOldValue = ""
    Me.edPz_Val9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val9.Properties.AutoHeight = False
    Me.edPz_Val9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val9.Properties.MaxLength = 65536
    Me.edPz_Val9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val9.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val9.TabIndex = 30
    '
    'edPz_Descam8
    '
    Me.edPz_Descam8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam8.Enabled = False
    Me.edPz_Descam8.Location = New System.Drawing.Point(25, 160)
    Me.edPz_Descam8.Name = "edPz_Descam8"
    Me.edPz_Descam8.NTSDbField = ""
    Me.edPz_Descam8.NTSForzaVisZoom = False
    Me.edPz_Descam8.NTSOldValue = ""
    Me.edPz_Descam8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam8.Properties.AutoHeight = False
    Me.edPz_Descam8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam8.Properties.MaxLength = 65536
    Me.edPz_Descam8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam8.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam8.TabIndex = 8
    '
    'edPz_Val11
    '
    Me.edPz_Val11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val11.Location = New System.Drawing.Point(454, 226)
    Me.edPz_Val11.Name = "edPz_Val11"
    Me.edPz_Val11.NTSDbField = ""
    Me.edPz_Val11.NTSForzaVisZoom = False
    Me.edPz_Val11.NTSOldValue = ""
    Me.edPz_Val11.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val11.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val11.Properties.AutoHeight = False
    Me.edPz_Val11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val11.Properties.MaxLength = 65536
    Me.edPz_Val11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val11.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val11.TabIndex = 56
    '
    'lb13
    '
    Me.lb13.AutoSize = True
    Me.lb13.BackColor = System.Drawing.Color.Transparent
    Me.lb13.Location = New System.Drawing.Point(3, 273)
    Me.lb13.Name = "lb13"
    Me.lb13.NTSDbField = ""
    Me.lb13.Size = New System.Drawing.Size(19, 13)
    Me.lb13.TabIndex = 63
    Me.lb13.Text = "13"
    Me.lb13.Tooltip = ""
    Me.lb13.UseMnemonic = False
    '
    'edPz_Val10
    '
    Me.edPz_Val10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val10.Location = New System.Drawing.Point(454, 204)
    Me.edPz_Val10.Name = "edPz_Val10"
    Me.edPz_Val10.NTSDbField = ""
    Me.edPz_Val10.NTSForzaVisZoom = False
    Me.edPz_Val10.NTSOldValue = ""
    Me.edPz_Val10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val10.Properties.AutoHeight = False
    Me.edPz_Val10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val10.Properties.MaxLength = 65536
    Me.edPz_Val10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val10.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val10.TabIndex = 31
    '
    'lb3
    '
    Me.lb3.AutoSize = True
    Me.lb3.BackColor = System.Drawing.Color.Transparent
    Me.lb3.Location = New System.Drawing.Point(9, 53)
    Me.lb3.Name = "lb3"
    Me.lb3.NTSDbField = ""
    Me.lb3.Size = New System.Drawing.Size(13, 13)
    Me.lb3.TabIndex = 39
    Me.lb3.Text = "3"
    Me.lb3.Tooltip = ""
    Me.lb3.UseMnemonic = False
    '
    'cbPz_Op15
    '
    Me.cbPz_Op15.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op15.DataSource = Nothing
    Me.cbPz_Op15.DisplayMember = ""
    Me.cbPz_Op15.Location = New System.Drawing.Point(358, 314)
    Me.cbPz_Op15.Name = "cbPz_Op15"
    Me.cbPz_Op15.NTSDbField = ""
    Me.cbPz_Op15.Properties.AutoHeight = False
    Me.cbPz_Op15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op15.Properties.DropDownRows = 30
    Me.cbPz_Op15.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op15.SelectedValue = ""
    Me.cbPz_Op15.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op15.TabIndex = 55
    Me.cbPz_Op15.ValueMember = ""
    '
    'lb7
    '
    Me.lb7.AutoSize = True
    Me.lb7.BackColor = System.Drawing.Color.Transparent
    Me.lb7.Location = New System.Drawing.Point(9, 141)
    Me.lb7.Name = "lb7"
    Me.lb7.NTSDbField = ""
    Me.lb7.Size = New System.Drawing.Size(13, 13)
    Me.lb7.TabIndex = 38
    Me.lb7.Text = "7"
    Me.lb7.Tooltip = ""
    Me.lb7.UseMnemonic = False
    '
    'cbPz_Op14
    '
    Me.cbPz_Op14.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op14.DataSource = Nothing
    Me.cbPz_Op14.DisplayMember = ""
    Me.cbPz_Op14.Location = New System.Drawing.Point(358, 292)
    Me.cbPz_Op14.Name = "cbPz_Op14"
    Me.cbPz_Op14.NTSDbField = ""
    Me.cbPz_Op14.Properties.AutoHeight = False
    Me.cbPz_Op14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op14.Properties.DropDownRows = 30
    Me.cbPz_Op14.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op14.SelectedValue = ""
    Me.cbPz_Op14.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op14.TabIndex = 54
    Me.cbPz_Op14.ValueMember = ""
    '
    'lb2
    '
    Me.lb2.AutoSize = True
    Me.lb2.BackColor = System.Drawing.Color.Transparent
    Me.lb2.Location = New System.Drawing.Point(9, 31)
    Me.lb2.Name = "lb2"
    Me.lb2.NTSDbField = ""
    Me.lb2.Size = New System.Drawing.Size(13, 13)
    Me.lb2.TabIndex = 38
    Me.lb2.Text = "2"
    Me.lb2.Tooltip = ""
    Me.lb2.UseMnemonic = False
    '
    'cbPz_Op13
    '
    Me.cbPz_Op13.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op13.DataSource = Nothing
    Me.cbPz_Op13.DisplayMember = ""
    Me.cbPz_Op13.Location = New System.Drawing.Point(358, 270)
    Me.cbPz_Op13.Name = "cbPz_Op13"
    Me.cbPz_Op13.NTSDbField = ""
    Me.cbPz_Op13.Properties.AutoHeight = False
    Me.cbPz_Op13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op13.Properties.DropDownRows = 30
    Me.cbPz_Op13.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op13.SelectedValue = ""
    Me.cbPz_Op13.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op13.TabIndex = 53
    Me.cbPz_Op13.ValueMember = ""
    '
    'cbPz_Op6
    '
    Me.cbPz_Op6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op6.DataSource = Nothing
    Me.cbPz_Op6.DisplayMember = ""
    Me.cbPz_Op6.Location = New System.Drawing.Point(358, 116)
    Me.cbPz_Op6.Name = "cbPz_Op6"
    Me.cbPz_Op6.NTSDbField = ""
    Me.cbPz_Op6.Properties.AutoHeight = False
    Me.cbPz_Op6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op6.Properties.DropDownRows = 30
    Me.cbPz_Op6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op6.SelectedValue = ""
    Me.cbPz_Op6.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op6.TabIndex = 22
    Me.cbPz_Op6.ValueMember = ""
    '
    'cbPz_Op12
    '
    Me.cbPz_Op12.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op12.DataSource = Nothing
    Me.cbPz_Op12.DisplayMember = ""
    Me.cbPz_Op12.Location = New System.Drawing.Point(358, 248)
    Me.cbPz_Op12.Name = "cbPz_Op12"
    Me.cbPz_Op12.NTSDbField = ""
    Me.cbPz_Op12.Properties.AutoHeight = False
    Me.cbPz_Op12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op12.Properties.DropDownRows = 30
    Me.cbPz_Op12.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op12.SelectedValue = ""
    Me.cbPz_Op12.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op12.TabIndex = 52
    Me.cbPz_Op12.ValueMember = ""
    '
    'lb1
    '
    Me.lb1.AutoSize = True
    Me.lb1.BackColor = System.Drawing.Color.Transparent
    Me.lb1.Location = New System.Drawing.Point(9, 9)
    Me.lb1.Name = "lb1"
    Me.lb1.NTSDbField = ""
    Me.lb1.Size = New System.Drawing.Size(13, 13)
    Me.lb1.TabIndex = 37
    Me.lb1.Text = "1"
    Me.lb1.Tooltip = ""
    Me.lb1.UseMnemonic = False
    '
    'cbPz_Op11
    '
    Me.cbPz_Op11.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op11.DataSource = Nothing
    Me.cbPz_Op11.DisplayMember = ""
    Me.cbPz_Op11.Location = New System.Drawing.Point(358, 226)
    Me.cbPz_Op11.Name = "cbPz_Op11"
    Me.cbPz_Op11.NTSDbField = ""
    Me.cbPz_Op11.Properties.AutoHeight = False
    Me.cbPz_Op11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op11.Properties.DropDownRows = 30
    Me.cbPz_Op11.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op11.SelectedValue = ""
    Me.cbPz_Op11.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op11.TabIndex = 51
    Me.cbPz_Op11.ValueMember = ""
    '
    'cbPz_Op7
    '
    Me.cbPz_Op7.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op7.DataSource = Nothing
    Me.cbPz_Op7.DisplayMember = ""
    Me.cbPz_Op7.Location = New System.Drawing.Point(358, 138)
    Me.cbPz_Op7.Name = "cbPz_Op7"
    Me.cbPz_Op7.NTSDbField = ""
    Me.cbPz_Op7.Properties.AutoHeight = False
    Me.cbPz_Op7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op7.Properties.DropDownRows = 30
    Me.cbPz_Op7.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op7.SelectedValue = ""
    Me.cbPz_Op7.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op7.TabIndex = 23
    Me.cbPz_Op7.ValueMember = ""
    '
    'lb12
    '
    Me.lb12.AutoSize = True
    Me.lb12.BackColor = System.Drawing.Color.Transparent
    Me.lb12.Location = New System.Drawing.Point(3, 251)
    Me.lb12.Name = "lb12"
    Me.lb12.NTSDbField = ""
    Me.lb12.Size = New System.Drawing.Size(19, 13)
    Me.lb12.TabIndex = 62
    Me.lb12.Text = "12"
    Me.lb12.Tooltip = ""
    Me.lb12.UseMnemonic = False
    '
    'edPz_Val5
    '
    Me.edPz_Val5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val5.Location = New System.Drawing.Point(454, 94)
    Me.edPz_Val5.Name = "edPz_Val5"
    Me.edPz_Val5.NTSDbField = ""
    Me.edPz_Val5.NTSForzaVisZoom = False
    Me.edPz_Val5.NTSOldValue = ""
    Me.edPz_Val5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val5.Properties.AutoHeight = False
    Me.edPz_Val5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val5.Properties.MaxLength = 65536
    Me.edPz_Val5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val5.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val5.TabIndex = 31
    '
    'cbPz_Op8
    '
    Me.cbPz_Op8.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op8.DataSource = Nothing
    Me.cbPz_Op8.DisplayMember = ""
    Me.cbPz_Op8.Location = New System.Drawing.Point(358, 160)
    Me.cbPz_Op8.Name = "cbPz_Op8"
    Me.cbPz_Op8.NTSDbField = ""
    Me.cbPz_Op8.Properties.AutoHeight = False
    Me.cbPz_Op8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op8.Properties.DropDownRows = 30
    Me.cbPz_Op8.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op8.SelectedValue = ""
    Me.cbPz_Op8.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op8.TabIndex = 24
    Me.cbPz_Op8.ValueMember = ""
    '
    'edPz_Descam9
    '
    Me.edPz_Descam9.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Descam9.Enabled = False
    Me.edPz_Descam9.Location = New System.Drawing.Point(25, 182)
    Me.edPz_Descam9.Name = "edPz_Descam9"
    Me.edPz_Descam9.NTSDbField = ""
    Me.edPz_Descam9.NTSForzaVisZoom = False
    Me.edPz_Descam9.NTSOldValue = ""
    Me.edPz_Descam9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam9.Properties.AutoHeight = False
    Me.edPz_Descam9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam9.Properties.MaxLength = 65536
    Me.edPz_Descam9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam9.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam9.TabIndex = 9
    '
    'edPz_Val4
    '
    Me.edPz_Val4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val4.Location = New System.Drawing.Point(454, 72)
    Me.edPz_Val4.Name = "edPz_Val4"
    Me.edPz_Val4.NTSDbField = ""
    Me.edPz_Val4.NTSForzaVisZoom = False
    Me.edPz_Val4.NTSOldValue = ""
    Me.edPz_Val4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val4.Properties.AutoHeight = False
    Me.edPz_Val4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val4.Properties.MaxLength = 65536
    Me.edPz_Val4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val4.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val4.TabIndex = 30
    '
    'edPz_Descam14
    '
    Me.edPz_Descam14.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam14.Enabled = False
    Me.edPz_Descam14.Location = New System.Drawing.Point(25, 292)
    Me.edPz_Descam14.Name = "edPz_Descam14"
    Me.edPz_Descam14.NTSDbField = ""
    Me.edPz_Descam14.NTSForzaVisZoom = False
    Me.edPz_Descam14.NTSOldValue = ""
    Me.edPz_Descam14.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam14.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam14.Properties.AutoHeight = False
    Me.edPz_Descam14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam14.Properties.MaxLength = 65536
    Me.edPz_Descam14.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam14.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam14.TabIndex = 49
    '
    'edPz_Val6
    '
    Me.edPz_Val6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val6.Location = New System.Drawing.Point(454, 116)
    Me.edPz_Val6.Name = "edPz_Val6"
    Me.edPz_Val6.NTSDbField = ""
    Me.edPz_Val6.NTSForzaVisZoom = False
    Me.edPz_Val6.NTSOldValue = ""
    Me.edPz_Val6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val6.Properties.AutoHeight = False
    Me.edPz_Val6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val6.Properties.MaxLength = 65536
    Me.edPz_Val6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val6.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val6.TabIndex = 27
    '
    'edPz_Val3
    '
    Me.edPz_Val3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val3.Location = New System.Drawing.Point(454, 50)
    Me.edPz_Val3.Name = "edPz_Val3"
    Me.edPz_Val3.NTSDbField = ""
    Me.edPz_Val3.NTSForzaVisZoom = False
    Me.edPz_Val3.NTSOldValue = ""
    Me.edPz_Val3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val3.Properties.AutoHeight = False
    Me.edPz_Val3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val3.Properties.MaxLength = 65536
    Me.edPz_Val3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val3.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val3.TabIndex = 29
    '
    'lb11
    '
    Me.lb11.AutoSize = True
    Me.lb11.BackColor = System.Drawing.Color.Transparent
    Me.lb11.Location = New System.Drawing.Point(3, 229)
    Me.lb11.Name = "lb11"
    Me.lb11.NTSDbField = ""
    Me.lb11.Size = New System.Drawing.Size(19, 13)
    Me.lb11.TabIndex = 61
    Me.lb11.Text = "11"
    Me.lb11.Tooltip = ""
    Me.lb11.UseMnemonic = False
    '
    'edPz_Val8
    '
    Me.edPz_Val8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val8.Location = New System.Drawing.Point(454, 160)
    Me.edPz_Val8.Name = "edPz_Val8"
    Me.edPz_Val8.NTSDbField = ""
    Me.edPz_Val8.NTSForzaVisZoom = False
    Me.edPz_Val8.NTSOldValue = ""
    Me.edPz_Val8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val8.Properties.AutoHeight = False
    Me.edPz_Val8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val8.Properties.MaxLength = 65536
    Me.edPz_Val8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val8.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val8.TabIndex = 29
    '
    'edPz_Val2
    '
    Me.edPz_Val2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val2.Location = New System.Drawing.Point(454, 28)
    Me.edPz_Val2.Name = "edPz_Val2"
    Me.edPz_Val2.NTSDbField = ""
    Me.edPz_Val2.NTSForzaVisZoom = False
    Me.edPz_Val2.NTSOldValue = ""
    Me.edPz_Val2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val2.Properties.AutoHeight = False
    Me.edPz_Val2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val2.Properties.MaxLength = 65536
    Me.edPz_Val2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val2.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val2.TabIndex = 28
    '
    'edPz_Descam15
    '
    Me.edPz_Descam15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam15.Enabled = False
    Me.edPz_Descam15.Location = New System.Drawing.Point(25, 314)
    Me.edPz_Descam15.Name = "edPz_Descam15"
    Me.edPz_Descam15.NTSDbField = ""
    Me.edPz_Descam15.NTSForzaVisZoom = False
    Me.edPz_Descam15.NTSOldValue = ""
    Me.edPz_Descam15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam15.Properties.AutoHeight = False
    Me.edPz_Descam15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam15.Properties.MaxLength = 65536
    Me.edPz_Descam15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam15.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam15.TabIndex = 50
    '
    'edPz_Val7
    '
    Me.edPz_Val7.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Val7.Location = New System.Drawing.Point(454, 138)
    Me.edPz_Val7.Name = "edPz_Val7"
    Me.edPz_Val7.NTSDbField = ""
    Me.edPz_Val7.NTSForzaVisZoom = False
    Me.edPz_Val7.NTSOldValue = ""
    Me.edPz_Val7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val7.Properties.AutoHeight = False
    Me.edPz_Val7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val7.Properties.MaxLength = 65536
    Me.edPz_Val7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val7.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val7.TabIndex = 28
    '
    'edPz_Val1
    '
    Me.edPz_Val1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val1.Location = New System.Drawing.Point(454, 6)
    Me.edPz_Val1.Name = "edPz_Val1"
    Me.edPz_Val1.NTSDbField = ""
    Me.edPz_Val1.NTSForzaVisZoom = False
    Me.edPz_Val1.NTSOldValue = ""
    Me.edPz_Val1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val1.Properties.AutoHeight = False
    Me.edPz_Val1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val1.Properties.MaxLength = 65536
    Me.edPz_Val1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val1.Size = New System.Drawing.Size(164, 20)
    Me.edPz_Val1.TabIndex = 27
    '
    'edPz_Descam13
    '
    Me.edPz_Descam13.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam13.Enabled = False
    Me.edPz_Descam13.Location = New System.Drawing.Point(25, 270)
    Me.edPz_Descam13.Name = "edPz_Descam13"
    Me.edPz_Descam13.NTSDbField = ""
    Me.edPz_Descam13.NTSForzaVisZoom = False
    Me.edPz_Descam13.NTSOldValue = ""
    Me.edPz_Descam13.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam13.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam13.Properties.AutoHeight = False
    Me.edPz_Descam13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam13.Properties.MaxLength = 65536
    Me.edPz_Descam13.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam13.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam13.TabIndex = 48
    '
    'cbPz_Op5
    '
    Me.cbPz_Op5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op5.DataSource = Nothing
    Me.cbPz_Op5.DisplayMember = ""
    Me.cbPz_Op5.Location = New System.Drawing.Point(358, 94)
    Me.cbPz_Op5.Name = "cbPz_Op5"
    Me.cbPz_Op5.NTSDbField = ""
    Me.cbPz_Op5.Properties.AutoHeight = False
    Me.cbPz_Op5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op5.Properties.DropDownRows = 30
    Me.cbPz_Op5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op5.SelectedValue = ""
    Me.cbPz_Op5.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op5.TabIndex = 26
    Me.cbPz_Op5.ValueMember = ""
    '
    'cbPz_Op4
    '
    Me.cbPz_Op4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op4.DataSource = Nothing
    Me.cbPz_Op4.DisplayMember = ""
    Me.cbPz_Op4.Location = New System.Drawing.Point(358, 72)
    Me.cbPz_Op4.Name = "cbPz_Op4"
    Me.cbPz_Op4.NTSDbField = ""
    Me.cbPz_Op4.Properties.AutoHeight = False
    Me.cbPz_Op4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op4.Properties.DropDownRows = 30
    Me.cbPz_Op4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op4.SelectedValue = ""
    Me.cbPz_Op4.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op4.TabIndex = 25
    Me.cbPz_Op4.ValueMember = ""
    '
    'edPz_Descam12
    '
    Me.edPz_Descam12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam12.Enabled = False
    Me.edPz_Descam12.Location = New System.Drawing.Point(25, 248)
    Me.edPz_Descam12.Name = "edPz_Descam12"
    Me.edPz_Descam12.NTSDbField = ""
    Me.edPz_Descam12.NTSForzaVisZoom = False
    Me.edPz_Descam12.NTSOldValue = ""
    Me.edPz_Descam12.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam12.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam12.Properties.AutoHeight = False
    Me.edPz_Descam12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam12.Properties.MaxLength = 65536
    Me.edPz_Descam12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam12.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam12.TabIndex = 47
    '
    'cbPz_Op3
    '
    Me.cbPz_Op3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op3.DataSource = Nothing
    Me.cbPz_Op3.DisplayMember = ""
    Me.cbPz_Op3.Location = New System.Drawing.Point(358, 50)
    Me.cbPz_Op3.Name = "cbPz_Op3"
    Me.cbPz_Op3.NTSDbField = ""
    Me.cbPz_Op3.Properties.AutoHeight = False
    Me.cbPz_Op3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op3.Properties.DropDownRows = 30
    Me.cbPz_Op3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op3.SelectedValue = ""
    Me.cbPz_Op3.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op3.TabIndex = 24
    Me.cbPz_Op3.ValueMember = ""
    '
    'cbPz_Op2
    '
    Me.cbPz_Op2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op2.DataSource = Nothing
    Me.cbPz_Op2.DisplayMember = ""
    Me.cbPz_Op2.Location = New System.Drawing.Point(358, 28)
    Me.cbPz_Op2.Name = "cbPz_Op2"
    Me.cbPz_Op2.NTSDbField = ""
    Me.cbPz_Op2.Properties.AutoHeight = False
    Me.cbPz_Op2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op2.Properties.DropDownRows = 30
    Me.cbPz_Op2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op2.SelectedValue = ""
    Me.cbPz_Op2.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op2.TabIndex = 23
    Me.cbPz_Op2.ValueMember = ""
    '
    'edPz_Descam10
    '
    Me.edPz_Descam10.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Descam10.Enabled = False
    Me.edPz_Descam10.Location = New System.Drawing.Point(25, 204)
    Me.edPz_Descam10.Name = "edPz_Descam10"
    Me.edPz_Descam10.NTSDbField = ""
    Me.edPz_Descam10.NTSForzaVisZoom = False
    Me.edPz_Descam10.NTSOldValue = ""
    Me.edPz_Descam10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam10.Properties.AutoHeight = False
    Me.edPz_Descam10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam10.Properties.MaxLength = 65536
    Me.edPz_Descam10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam10.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam10.TabIndex = 10
    '
    'cbPz_Op1
    '
    Me.cbPz_Op1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op1.DataSource = Nothing
    Me.cbPz_Op1.DisplayMember = ""
    Me.cbPz_Op1.Location = New System.Drawing.Point(358, 6)
    Me.cbPz_Op1.Name = "cbPz_Op1"
    Me.cbPz_Op1.NTSDbField = ""
    Me.cbPz_Op1.Properties.AutoHeight = False
    Me.cbPz_Op1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op1.Properties.DropDownRows = 30
    Me.cbPz_Op1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op1.SelectedValue = ""
    Me.cbPz_Op1.Size = New System.Drawing.Size(90, 20)
    Me.cbPz_Op1.TabIndex = 22
    Me.cbPz_Op1.ValueMember = ""
    '
    'edPz_Descam11
    '
    Me.edPz_Descam11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam11.Enabled = False
    Me.edPz_Descam11.Location = New System.Drawing.Point(25, 226)
    Me.edPz_Descam11.Name = "edPz_Descam11"
    Me.edPz_Descam11.NTSDbField = ""
    Me.edPz_Descam11.NTSForzaVisZoom = False
    Me.edPz_Descam11.NTSOldValue = ""
    Me.edPz_Descam11.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam11.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam11.Properties.AutoHeight = False
    Me.edPz_Descam11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam11.Properties.MaxLength = 65536
    Me.edPz_Descam11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam11.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam11.TabIndex = 46
    '
    'lb10
    '
    Me.lb10.AutoSize = True
    Me.lb10.BackColor = System.Drawing.Color.Transparent
    Me.lb10.Location = New System.Drawing.Point(3, 207)
    Me.lb10.Name = "lb10"
    Me.lb10.NTSDbField = ""
    Me.lb10.Size = New System.Drawing.Size(19, 13)
    Me.lb10.TabIndex = 41
    Me.lb10.Text = "10"
    Me.lb10.Tooltip = ""
    Me.lb10.UseMnemonic = False
    '
    'edPz_Descam5
    '
    Me.edPz_Descam5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam5.Enabled = False
    Me.edPz_Descam5.Location = New System.Drawing.Point(25, 94)
    Me.edPz_Descam5.Name = "edPz_Descam5"
    Me.edPz_Descam5.NTSDbField = ""
    Me.edPz_Descam5.NTSForzaVisZoom = False
    Me.edPz_Descam5.NTSOldValue = ""
    Me.edPz_Descam5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam5.Properties.AutoHeight = False
    Me.edPz_Descam5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam5.Properties.MaxLength = 65536
    Me.edPz_Descam5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam5.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam5.TabIndex = 10
    '
    'lb9
    '
    Me.lb9.AutoSize = True
    Me.lb9.BackColor = System.Drawing.Color.Transparent
    Me.lb9.Location = New System.Drawing.Point(3, 185)
    Me.lb9.Name = "lb9"
    Me.lb9.NTSDbField = ""
    Me.lb9.Size = New System.Drawing.Size(19, 13)
    Me.lb9.TabIndex = 40
    Me.lb9.Text = "  9"
    Me.lb9.Tooltip = ""
    Me.lb9.UseMnemonic = False
    '
    'edPz_Descam4
    '
    Me.edPz_Descam4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam4.Enabled = False
    Me.edPz_Descam4.Location = New System.Drawing.Point(25, 72)
    Me.edPz_Descam4.Name = "edPz_Descam4"
    Me.edPz_Descam4.NTSDbField = ""
    Me.edPz_Descam4.NTSForzaVisZoom = False
    Me.edPz_Descam4.NTSOldValue = ""
    Me.edPz_Descam4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam4.Properties.AutoHeight = False
    Me.edPz_Descam4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam4.Properties.MaxLength = 65536
    Me.edPz_Descam4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam4.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam4.TabIndex = 9
    '
    'edPz_Descam3
    '
    Me.edPz_Descam3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam3.Enabled = False
    Me.edPz_Descam3.Location = New System.Drawing.Point(25, 50)
    Me.edPz_Descam3.Name = "edPz_Descam3"
    Me.edPz_Descam3.NTSDbField = ""
    Me.edPz_Descam3.NTSForzaVisZoom = False
    Me.edPz_Descam3.NTSOldValue = ""
    Me.edPz_Descam3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam3.Properties.AutoHeight = False
    Me.edPz_Descam3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam3.Properties.MaxLength = 65536
    Me.edPz_Descam3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam3.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam3.TabIndex = 8
    '
    'edPz_Descam2
    '
    Me.edPz_Descam2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam2.Enabled = False
    Me.edPz_Descam2.Location = New System.Drawing.Point(25, 28)
    Me.edPz_Descam2.Name = "edPz_Descam2"
    Me.edPz_Descam2.NTSDbField = ""
    Me.edPz_Descam2.NTSForzaVisZoom = False
    Me.edPz_Descam2.NTSOldValue = ""
    Me.edPz_Descam2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam2.Properties.AutoHeight = False
    Me.edPz_Descam2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam2.Properties.MaxLength = 65536
    Me.edPz_Descam2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam2.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam2.TabIndex = 7
    '
    'edPz_Descam1
    '
    Me.edPz_Descam1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam1.Enabled = False
    Me.edPz_Descam1.Location = New System.Drawing.Point(25, 6)
    Me.edPz_Descam1.Name = "edPz_Descam1"
    Me.edPz_Descam1.NTSDbField = ""
    Me.edPz_Descam1.NTSForzaVisZoom = False
    Me.edPz_Descam1.NTSOldValue = ""
    Me.edPz_Descam1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam1.Properties.AutoHeight = False
    Me.edPz_Descam1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam1.Properties.MaxLength = 65536
    Me.edPz_Descam1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam1.Size = New System.Drawing.Size(327, 20)
    Me.edPz_Descam1.TabIndex = 6
    '
    'edDisplayfiltriS
    '
    Me.edDisplayfiltriS.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDisplayfiltriS.Enabled = False
    Me.edDisplayfiltriS.Location = New System.Drawing.Point(48, 26)
    Me.edDisplayfiltriS.Name = "edDisplayfiltriS"
    Me.edDisplayfiltriS.NTSDbField = ""
    Me.edDisplayfiltriS.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDisplayfiltriS.Size = New System.Drawing.Size(34, 24)
    Me.edDisplayfiltriS.TabIndex = 47
    Me.edDisplayfiltriS.Visible = False
    '
    'edPz_strOprel2
    '
    Me.edPz_strOprel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_strOprel2.Enabled = False
    Me.edPz_strOprel2.Location = New System.Drawing.Point(48, 80)
    Me.edPz_strOprel2.Name = "edPz_strOprel2"
    Me.edPz_strOprel2.NTSDbField = ""
    Me.edPz_strOprel2.NTSForzaVisZoom = False
    Me.edPz_strOprel2.NTSOldValue = ""
    Me.edPz_strOprel2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_strOprel2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_strOprel2.Properties.AutoHeight = False
    Me.edPz_strOprel2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_strOprel2.Properties.MaxLength = 65536
    Me.edPz_strOprel2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_strOprel2.Size = New System.Drawing.Size(34, 20)
    Me.edPz_strOprel2.TabIndex = 46
    Me.edPz_strOprel2.Visible = False
    '
    'edPz_strOprel
    '
    Me.edPz_strOprel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_strOprel.Enabled = False
    Me.edPz_strOprel.Location = New System.Drawing.Point(8, 80)
    Me.edPz_strOprel.Name = "edPz_strOprel"
    Me.edPz_strOprel.NTSDbField = ""
    Me.edPz_strOprel.NTSForzaVisZoom = False
    Me.edPz_strOprel.NTSOldValue = ""
    Me.edPz_strOprel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_strOprel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_strOprel.Properties.AutoHeight = False
    Me.edPz_strOprel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_strOprel.Properties.MaxLength = 65536
    Me.edPz_strOprel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_strOprel.Size = New System.Drawing.Size(34, 20)
    Me.edPz_strOprel.TabIndex = 5
    Me.edPz_strOprel.Visible = False
    '
    'edDisplayfiltri
    '
    Me.edDisplayfiltri.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDisplayfiltri.Enabled = False
    Me.edDisplayfiltri.Location = New System.Drawing.Point(8, 26)
    Me.edDisplayfiltri.Name = "edDisplayfiltri"
    Me.edDisplayfiltri.NTSDbField = ""
    Me.edDisplayfiltri.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDisplayfiltri.Size = New System.Drawing.Size(34, 24)
    Me.edDisplayfiltri.TabIndex = 4
    Me.edDisplayfiltri.Visible = False
    '
    'lbPz_Val1
    '
    Me.lbPz_Val1.AutoSize = True
    Me.lbPz_Val1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Val1.Location = New System.Drawing.Point(494, 3)
    Me.lbPz_Val1.Name = "lbPz_Val1"
    Me.lbPz_Val1.NTSDbField = ""
    Me.lbPz_Val1.Size = New System.Drawing.Size(73, 13)
    Me.lbPz_Val1.TabIndex = 45
    Me.lbPz_Val1.Text = "Valore Campo"
    Me.lbPz_Val1.Tooltip = ""
    Me.lbPz_Val1.UseMnemonic = False
    '
    'lbPz_Descam1
    '
    Me.lbPz_Descam1.AutoSize = True
    Me.lbPz_Descam1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Descam1.Location = New System.Drawing.Point(130, 3)
    Me.lbPz_Descam1.Name = "lbPz_Descam1"
    Me.lbPz_Descam1.NTSDbField = ""
    Me.lbPz_Descam1.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Descam1.TabIndex = 11
    Me.lbPz_Descam1.Text = "Descrizione Campo"
    Me.lbPz_Descam1.Tooltip = ""
    Me.lbPz_Descam1.UseMnemonic = False
    '
    'lbPz_Op1
    '
    Me.lbPz_Op1.AutoSize = True
    Me.lbPz_Op1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Op1.Location = New System.Drawing.Point(371, 3)
    Me.lbPz_Op1.Name = "lbPz_Op1"
    Me.lbPz_Op1.NTSDbField = ""
    Me.lbPz_Op1.Size = New System.Drawing.Size(57, 13)
    Me.lbPz_Op1.TabIndex = 44
    Me.lbPz_Op1.Text = "Operatore"
    Me.lbPz_Op1.Tooltip = ""
    Me.lbPz_Op1.UseMnemonic = False
    '
    'FRM__SPAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(654, 554)
    Me.Controls.Add(Me.pnSpar)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "FRM__SPAR"
    Me.Text = "STAMPE/VISUALIZZAZIONI PARAMETRICHE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSpar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSpar.ResumeLayout(False)
    Me.pnSpar.PerformLayout()
    CType(Me.cbParstag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    Me.pnPag1.PerformLayout()
    CType(Me.edPz_Vals5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDisplayfiltriS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_strOprel2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_strOprel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDisplayfiltri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      cbParstag.NTSSetParam(oApp.Tr(Me, 128327022446431230, "Formato di stampa"))
      '--------------------------------------------------------------------------------------------------------------
      edPz_Val15.NTSSetParam(oMenu, oApp.Tr(Me, 129077625239619140, "Valore Campo15"), 0)
      edPz_Val14.NTSSetParam(oMenu, oApp.Tr(Me, 129077625252333984, "Valore Campo14"), 0)
      edPz_Val13.NTSSetParam(oMenu, oApp.Tr(Me, 129077625263134765, "Valore Campo13"), 0)
      edPz_Val12.NTSSetParam(oMenu, oApp.Tr(Me, 129077625272490234, "Valore Campo12"), 0)
      edPz_Val11.NTSSetParam(oMenu, oApp.Tr(Me, 129077625281533203, "Valore Campo11"), 0)
      edPz_Val10.NTSSetParam(oMenu, oApp.Tr(Me, 129077625291210937, "Valore Campo10"), 0)
      edPz_Val9.NTSSetParam(oMenu, oApp.Tr(Me, 128327022451429534, "Valore Campo9"), 0)
      edPz_Val8.NTSSetParam(oMenu, oApp.Tr(Me, 128327022451585731, "Valore Campo8"), 0)
      edPz_Val7.NTSSetParam(oMenu, oApp.Tr(Me, 128327022451741928, "Valore Campo7"), 0)
      edPz_Val6.NTSSetParam(oMenu, oApp.Tr(Me, 128327022451898125, "Valore Campo6"), 0)
      edPz_Val5.NTSSetParam(oMenu, oApp.Tr(Me, 128327022447837003, "Valore Campo5"), 0)
      edPz_Val4.NTSSetParam(oMenu, oApp.Tr(Me, 128327022447993200, "Valore Campo4"), 0)
      edPz_Val3.NTSSetParam(oMenu, oApp.Tr(Me, 128327022448149397, "Valore Campo3"), 0)
      edPz_Val2.NTSSetParam(oMenu, oApp.Tr(Me, 128327022448305594, "Valore Campo2"), 0)
      edPz_Val1.NTSSetParam(oMenu, oApp.Tr(Me, 128327022448461791, "Valore Campo1"), 0)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals1.NTSSetParam(oMenu, oApp.Tr(Me, 130449871038806284, "Valore Campo 1 (Subquery)"), 0)
      edPz_Vals2.NTSSetParam(oMenu, oApp.Tr(Me, 130449871055154072, "Valore Campo 2 (Subquery)"), 0)
      edPz_Vals3.NTSSetParam(oMenu, oApp.Tr(Me, 130449871073503820, "Valore Campo 3 (Subquery)"), 0)
      edPz_Vals4.NTSSetParam(oMenu, oApp.Tr(Me, 130449871098767671, "Valore Campo 4 (Subquery)"), 0)
      edPz_Vals5.NTSSetParam(oMenu, oApp.Tr(Me, 130449871113797072, "Valore Campo 5 (Subquery)"), 0)
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op15.NTSSetParam(oApp.Tr(Me, 129077624940341797, "Operatore15"))
      cbPz_Op14.NTSSetParam(oApp.Tr(Me, 129077624953779297, "Operatore14"))
      cbPz_Op13.NTSSetParam(oApp.Tr(Me, 129077624965859375, "Operatore13"))
      cbPz_Op12.NTSSetParam(oApp.Tr(Me, 129077624977773437, "Operatore12"))
      cbPz_Op11.NTSSetParam(oApp.Tr(Me, 129077624989296875, "Operatore11"))
      cbPz_Op10.NTSSetParam(oApp.Tr(Me, 129077624999619140, "Operatore10"))
      cbPz_Op9.NTSSetParam(oApp.Tr(Me, 128327022452210519, "Operatore9"))
      cbPz_Op8.NTSSetParam(oApp.Tr(Me, 128327022452366716, "Operatore8"))
      cbPz_Op7.NTSSetParam(oApp.Tr(Me, 128327022452522913, "Operatore7"))
      cbPz_Op6.NTSSetParam(oApp.Tr(Me, 128327022452679110, "Operatore6"))
      cbPz_Op5.NTSSetParam(oApp.Tr(Me, 128327022448617988, "Operatore5"))
      cbPz_Op4.NTSSetParam(oApp.Tr(Me, 128327022448774185, "Operatore4"))
      cbPz_Op3.NTSSetParam(oApp.Tr(Me, 128327022448930382, "Operatore3"))
      cbPz_Op2.NTSSetParam(oApp.Tr(Me, 128327022449086579, "Operatore2"))
      cbPz_Op1.NTSSetParam(oApp.Tr(Me, 128327022449242776, "Operatore1"))
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.NTSSetParam(oApp.Tr(Me, 130449871405244269, "Operatore 1 (Subquery)"))
      cbPz_Ops2.NTSSetParam(oApp.Tr(Me, 130449871425967068, "Operatore 2 (Subquery)"))
      cbPz_Ops3.NTSSetParam(oApp.Tr(Me, 130449871441924214, "Operatore 3 (Subquery)"))
      cbPz_Ops4.NTSSetParam(oApp.Tr(Me, 130449871461553256, "Operatore 4 (Subquery)"))
      cbPz_Ops5.NTSSetParam(oApp.Tr(Me, 130449871478291657, "Operatore 5 (Subquery)"))
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descam15.NTSSetParam(oMenu, oApp.Tr(Me, 129077624629570312, "Descrizione Campo15"), 40)
      edPz_Descam14.NTSSetParam(oMenu, oApp.Tr(Me, 129077624638125000, "Descrizione Campo14"), 40)
      edPz_Descam13.NTSSetParam(oMenu, oApp.Tr(Me, 129077624651572265, "Descrizione Campo13"), 40)
      edPz_Descam12.NTSSetParam(oMenu, oApp.Tr(Me, 129077624665166015, "Descrizione Campo12"), 40)
      edPz_Descam11.NTSSetParam(oMenu, oApp.Tr(Me, 129077624678359375, "Descrizione Campo11"), 40)
      edPz_Descam10.NTSSetParam(oMenu, oApp.Tr(Me, 129077624691308593, "Descrizione Campo10"), 40)
      edPz_Descam9.NTSSetParam(oMenu, oApp.Tr(Me, 128327022453147701, "Descrizione Campo9"), 40)
      edPz_Descam8.NTSSetParam(oMenu, oApp.Tr(Me, 128327022453303898, "Descrizione Campo8"), 40)
      edPz_Descam7.NTSSetParam(oMenu, oApp.Tr(Me, 128327022453460095, "Descrizione Campo7"), 40)
      edPz_Descam6.NTSSetParam(oMenu, oApp.Tr(Me, 128327022453616292, "Descrizione Campo6"), 40)
      edPz_Descam5.NTSSetParam(oMenu, oApp.Tr(Me, 128327022449555170, "Descrizione Campo5"), 40)
      edPz_Descam4.NTSSetParam(oMenu, oApp.Tr(Me, 128327022449711367, "Descrizione Campo4"), 40)
      edPz_Descam3.NTSSetParam(oMenu, oApp.Tr(Me, 128327022449867564, "Descrizione Campo3"), 40)
      edPz_Descam2.NTSSetParam(oMenu, oApp.Tr(Me, 128327022450023761, "Descrizione Campo2"), 40)
      edPz_Descam1.NTSSetParam(oMenu, oApp.Tr(Me, 128327022450179958, "Descrizione Campo1"), 40)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descams1.NTSSetParam(oMenu, oApp.Tr(Me, 130449871773000586, "Descrizione Campo 1 (Subquery)"), 40)
      edPz_Descams2.NTSSetParam(oMenu, oApp.Tr(Me, 130449871788176477, "Descrizione Campo 2 (Subquery)"), 40)
      edPz_Descams3.NTSSetParam(oMenu, oApp.Tr(Me, 130449871801555479, "Descrizione Campo 3 (Subquery)"), 40)
      edPz_Descams4.NTSSetParam(oMenu, oApp.Tr(Me, 130449871818528254, "Descrizione Campo 4 (Subquery)"), 40)
      edPz_Descams5.NTSSetParam(oMenu, oApp.Tr(Me, 130449871833157264, "Descrizione Campo 5 (Subquery)"), 40)
      '--------------------------------------------------------------------------------------------------------------
      edPz_strOprel.NTSSetParam(oMenu, oApp.Tr(Me, 128735677152396035, "Composizione relazionale filtri ridotta"), 255)
      edPz_strOprel2.NTSSetParam(oMenu, oApp.Tr(Me, 130449871973998029, "Composizione relazionale filtri ridotta (Subquery)"), 255)
      edDisplayfiltri.NTSSetParam(oMenu, oApp.Tr(Me, 128327022453772489, "Composizione relazionale filtri estesa"), 0)
      edDisplayfiltriS.NTSSetParam(oMenu, oApp.Tr(Me, 130449874015905651, "Composizione relazionale filtri estesa (Subquery)"), 0)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__SPAR_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim i As Integer = 0
    Dim strCampo As String = ""

    Try
      LeggiDatiDitta()

      If Not oCallParams Is Nothing Then
        'chiamato da BN__CHIL tramite SmartArt
        'esempio di chiamata (chiamante; numero stampa; conto; articolo; fase articolo)
        'BN__CHIL;102;4010001;MP1;1
        Dim strT() As String = oCallParams.strParam.Split(";"c)
        cbParstag.SelectedValue = strT(1)
        If cbParstag.SelectedIndex > -1 Then
          cbParstag_SelectedIndexChanged(Me, Nothing)

          If NTSCInt(strT(2)) <> 0 Then
            'cerco di proporre il conto
            For i = 1 To 15
              strCampo = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column" & i.ToString))
              If strCampo.ToLower.Contains("_conto") Then
                If pnPag1.Controls("edPz_Val" & i.ToString).Enabled Then
                  pnPag1.Controls("edPz_Val" & i.ToString).Text = strT(2)
                End If
              End If
            Next
          End If
          If NTSCInt(strT(3)) <> 0 Then
            'cerco di proporre l'articolo
            For i = 1 To 15
              strCampo = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column" & i.ToString))
              If strCampo.ToLower.Contains("_codart") Then
                If pnPag1.Controls("edPz_Val" & i.ToString).Enabled Then
                  pnPag1.Controls("edPz_Val" & i.ToString).Text = strT(3)
                End If
              End If
            Next
          End If
          If NTSCInt(strT(4)) <> 0 Then
            'cerco di proporre la fase articolo
            For i = 1 To 15
              strCampo = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column" & i.ToString))
              If strCampo.ToLower.Contains("_fase") Then
                If pnPag1.Controls("edPz_Val" & i.ToString).Enabled Then
                  pnPag1.Controls("edPz_Val" & i.ToString).Text = strT(4)
                End If
              End If
            Next
          End If
        Else
          'stampa inesistente
          cbParstag.SelectedIndex = 0
        End If
      End If

      cbParstag_SelectedIndexChanged(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__SPAR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      'Carica RECENT
      tlbOrderByDescr.Down = CBool(oMenu.GetSettingBus("BS--SPAR", "RECENT", ".", "OrderByDesc", "", "", "0"))

      Apri()

      GctlSetRoules()

      AddHandler edPz_Val1.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val2.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val3.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val4.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val5.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val6.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val7.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val8.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val9.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val10.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val11.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val12.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val13.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val14.Validating, AddressOf Valori_Validating
      AddHandler edPz_Val15.Validating, AddressOf Valori_Validating

      AddHandler edPz_Vals1.Validating, AddressOf Valori_Validating
      AddHandler edPz_Vals2.Validating, AddressOf Valori_Validating
      AddHandler edPz_Vals3.Validating, AddressOf Valori_Validating
      AddHandler edPz_Vals4.Validating, AddressOf Valori_Validating
      AddHandler edPz_Vals5.Validating, AddressOf Valori_Validating

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      StampaGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      Elabora()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edPz_Val1.Focused And edPz_Descam1.Text <> "" Then
        ApriZoom(1)
      ElseIf edPz_Val2.Focused And edPz_Descam2.Text <> "" Then
        ApriZoom(2)
      ElseIf edPz_Val3.Focused And edPz_Descam3.Text <> "" Then
        ApriZoom(3)
      ElseIf edPz_Val4.Focused And edPz_Descam4.Text <> "" Then
        ApriZoom(4)
      ElseIf edPz_Val5.Focused And edPz_Descam5.Text <> "" Then
        ApriZoom(5)
      ElseIf edPz_Val6.Focused And edPz_Descam6.Text <> "" Then
        ApriZoom(6)
      ElseIf edPz_Val7.Focused And edPz_Descam7.Text <> "" Then
        ApriZoom(7)
      ElseIf edPz_Val8.Focused And edPz_Descam8.Text <> "" Then
        ApriZoom(8)
      ElseIf edPz_Val9.Focused And edPz_Descam9.Text <> "" Then
        ApriZoom(9)
      ElseIf edPz_Val10.Focused And edPz_Descam10.Text <> "" Then
        ApriZoom(10)
      ElseIf edPz_Val11.Focused And edPz_Descam11.Text <> "" Then
        ApriZoom(11)
      ElseIf edPz_Val12.Focused And edPz_Descam12.Text <> "" Then
        ApriZoom(12)
      ElseIf edPz_Val13.Focused And edPz_Descam13.Text <> "" Then
        ApriZoom(13)
      ElseIf edPz_Val14.Focused And edPz_Descam14.Text <> "" Then
        ApriZoom(14)
      ElseIf edPz_Val15.Focused And edPz_Descam15.Text <> "" Then
        ApriZoom(15)
      ElseIf cbParstag.Focused Then
        cmdZoomStampe_Click(Nothing, Nothing)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edPz_Vals1.Focused And edPz_Descams1.Text <> "" Then
        ApriZoomS(1)
      ElseIf edPz_Vals2.Focused And edPz_Descams2.Text <> "" Then
        ApriZoomS(2)
      ElseIf edPz_Vals3.Focused And edPz_Descams3.Text <> "" Then
        ApriZoomS(3)
      ElseIf edPz_Vals4.Focused And edPz_Descams4.Text <> "" Then
        ApriZoomS(4)
      ElseIf edPz_Vals5.Focused And edPz_Descams5.Text <> "" Then
        ApriZoomS(5)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Dim bOut As Boolean
    Try
      pnSpar.Visible = False
      LeggiDatiDitta()
      pnSpar.Visible = True

      bOut = Apri()
      CaricaCbParstag()

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
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbOrderByDescr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrderByDescr.ItemClick
    Try
      cmdRicarica_Click(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbOrderByDescr_DownChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrderByDescr.DownChanged
    Try
      oMenu.SaveSettingBus("BS--SPAR", "RECENT", ".", "OrderByDesc", NTSCStr(tlbOrderByDescr.Down), " ", ".S.", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edPz_strOprel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_strOprel.TextChanged
    Try
      If Len(edPz_strOprel.Text) > 0 Then
        Espandifiltri()
      Else
        edDisplayfiltri.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_strOprel2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_strOprel2.TextChanged
    Try
      If edPz_strOprel2.Text.Length > 0 Then
        Espandifiltri()
      Else
        edDisplayfiltri.Text = ""
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edPz_Val1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val1.TextChanged
    Try
      edPz_Val1.Text = edPz_Val1.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val2.TextChanged
    Try
      edPz_Val2.Text = edPz_Val2.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val3.TextChanged
    Try
      edPz_Val3.Text = edPz_Val3.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val4.TextChanged
    Try
      edPz_Val4.Text = edPz_Val4.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val5.TextChanged
    Try
      edPz_Val5.Text = edPz_Val5.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val6.TextChanged
    Try
      edPz_Val6.Text = edPz_Val6.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val7.TextChanged
    Try
      edPz_Val7.Text = edPz_Val7.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val8.TextChanged
    Try
      edPz_Val8.Text = edPz_Val8.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val9.TextChanged
    Try
      edPz_Val9.Text = edPz_Val9.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val10.TextChanged
    Try
      edPz_Val10.Text = edPz_Val10.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val11.TextChanged
    Try
      edPz_Val11.Text = edPz_Val11.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val12.TextChanged
    Try
      edPz_Val12.Text = edPz_Val12.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val13.TextChanged
    Try
      edPz_Val13.Text = edPz_Val13.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val14.TextChanged
    Try
      edPz_Val14.Text = edPz_Val14.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val15.TextChanged
    Try
      edPz_Val15.Text = edPz_Val15.Text.ToUpper
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edPz_Descam1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam1.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam2.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam3.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam4.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam5.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam6.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam7.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam8.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam9.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam10.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam11.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam12.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam13.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam14.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam15.TextChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbPz_Op1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op1.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op2.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op3.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op4.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op5.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op6.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op7.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op8.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op9.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op10.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op11_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op11.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op12.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op13_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op13.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op14_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op14.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op15_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op15.SelectedIndexChanged
    Try
      Espandifiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbParstag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbParstag.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      posiziona(oCleSpar.PrimaParola(cbParstag.Text).Trim)
      resettaRiga()
      abilitaToolbar()
      '--------------------------------------------------------------------------------------------------------------
      '--- Disabilito i filtri non usati
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op1.Enabled = CBool(edPz_Descam1.Text.Trim <> "")
      edPz_Val1.Enabled = CBool(edPz_Descam1.Text.Trim <> "")
      cbPz_Op2.Enabled = CBool(edPz_Descam2.Text.Trim <> "")
      edPz_Val2.Enabled = CBool(edPz_Descam2.Text.Trim <> "")
      cbPz_Op3.Enabled = CBool(edPz_Descam3.Text.Trim <> "")
      edPz_Val3.Enabled = CBool(edPz_Descam3.Text.Trim <> "")
      cbPz_Op4.Enabled = CBool(edPz_Descam4.Text.Trim <> "")
      edPz_Val4.Enabled = CBool(edPz_Descam4.Text.Trim <> "")
      cbPz_Op5.Enabled = CBool(edPz_Descam5.Text.Trim <> "")
      edPz_Val5.Enabled = CBool(edPz_Descam5.Text.Trim <> "")
      cbPz_Op6.Enabled = CBool(edPz_Descam6.Text.Trim <> "")
      edPz_Val6.Enabled = CBool(edPz_Descam6.Text.Trim <> "")
      cbPz_Op7.Enabled = CBool(edPz_Descam7.Text.Trim <> "")
      edPz_Val7.Enabled = CBool(edPz_Descam7.Text.Trim <> "")
      cbPz_Op8.Enabled = CBool(edPz_Descam8.Text.Trim <> "")
      edPz_Val8.Enabled = CBool(edPz_Descam8.Text.Trim <> "")
      cbPz_Op9.Enabled = CBool(edPz_Descam9.Text.Trim <> "")
      edPz_Val9.Enabled = CBool(edPz_Descam9.Text.Trim <> "")
      cbPz_Op10.Enabled = CBool(edPz_Descam10.Text.Trim <> "")
      edPz_Val10.Enabled = CBool(edPz_Descam10.Text.Trim <> "")
      cbPz_Op11.Enabled = CBool(edPz_Descam11.Text.Trim <> "")
      edPz_Val11.Enabled = CBool(edPz_Descam11.Text.Trim <> "")
      cbPz_Op12.Enabled = CBool(edPz_Descam12.Text.Trim <> "")
      edPz_Val12.Enabled = CBool(edPz_Descam12.Text.Trim <> "")
      cbPz_Op13.Enabled = CBool(edPz_Descam13.Text.Trim <> "")
      edPz_Val13.Enabled = CBool(edPz_Descam13.Text.Trim <> "")
      cbPz_Op14.Enabled = CBool(edPz_Descam14.Text.Trim <> "")
      edPz_Val14.Enabled = CBool(edPz_Descam14.Text.Trim <> "")
      cbPz_Op15.Enabled = CBool(edPz_Descam15.Text.Trim <> "")
      edPz_Val15.Enabled = CBool(edPz_Descam15.Text.Trim <> "")
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.Enabled = CBool(edPz_Descams1.Text.Trim <> "")
      edPz_Vals1.Enabled = CBool(edPz_Descams1.Text.Trim <> "")
      cbPz_Ops2.Enabled = CBool(edPz_Descams2.Text.Trim <> "")
      edPz_Vals2.Enabled = CBool(edPz_Descams2.Text.Trim <> "")
      cbPz_Ops3.Enabled = CBool(edPz_Descams3.Text.Trim <> "")
      edPz_Vals3.Enabled = CBool(edPz_Descams3.Text.Trim <> "")
      cbPz_Ops4.Enabled = CBool(edPz_Descams4.Text.Trim <> "")
      edPz_Vals4.Enabled = CBool(edPz_Descams4.Text.Trim <> "")
      cbPz_Ops5.Enabled = CBool(edPz_Descams5.Text.Trim <> "")
      edPz_Vals5.Enabled = CBool(edPz_Descams5.Text.Trim <> "")
      '--------------------------------------------------------------------------------------------------------------

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdRicarica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicarica.Click
    Dim bOut As Boolean
    Try
      bOut = Apri()
      CaricaCbParstag()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdNuovaStampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuovaStampa.Click
    Dim oParApri As CLE__CLDP = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      oParApri = Nothing
      oParApri = New CLE__CLDP
      oMenu.RunChild("NTSInformatica", "FRM__ISTP", "", DittaCorrente, "", "BN__ISTP", oParApri, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = System.Windows.Forms.Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
      cmdRicarica_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = System.Windows.Forms.Cursors.Default
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      NTSFormClearDataBinding(Me)
      If Not oCleSpar.Apri(DittaCorrente, dsSpar) Then Me.Close()
      dcSpar.DataSource = dsSpar.Tables("PARSTAG")
      dsSpar.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      dcSpar.ResetBindings(False)
      dcSpar.MoveFirst()

      If (dsSpar.Tables("PARSTAG").Rows.Count <> 0) And (oCleSpar.strElencoCodici.Trim <> "") Then
        posiziona(oCleSpar.PrimaParola(cbParstag.Text).Trim)
        resettaRiga()
      End If

      abilitaToolbar()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ApriZoom(ByVal i As Integer)
    Dim nPos As Integer
    Dim strTabella As String
    Dim strCampo As String
    Dim strColonna As String
    Dim oParam As New CLE__PATB

    Try
      strColonna = "Pz_Column" & i
      nPos = InStr(NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna)), ".") - 1
      strTabella = Mid(NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna)), 1, nPos)
      strCampo = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna))
      Select Case i
        Case 1
          If (edPz_Val1.Focused) And edPz_Descam1.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(1, strTabella, strCampo)
            End If
          End If
        Case 2
          If (edPz_Val2.Focused) And edPz_Descam2.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(2, strTabella, strCampo)
            End If
          End If
        Case 3
          If (edPz_Val3.Focused) And edPz_Descam3.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(3, strTabella, strCampo)
            End If
          End If
        Case 4
          If (edPz_Val4.Focused) And edPz_Descam4.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(4, strTabella, strCampo)
            End If
          End If
        Case 5
          If (edPz_Val5.Focused) And edPz_Descam5.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(5, strTabella, strCampo)
            End If
          End If
        Case 6
          If (edPz_Val6.Focused) And edPz_Descam6.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(6, strTabella, strCampo)
            End If
          End If
        Case 7
          If (edPz_Val7.Focused) And edPz_Descam7.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(7, strTabella, strCampo)
            End If
          End If
        Case 8
          If (edPz_Val8.Focused) And edPz_Descam8.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(8, strTabella, strCampo)
            End If
          End If
        Case 9
          If (edPz_Val9.Focused) And edPz_Descam9.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(9, strTabella, strCampo)
            End If
          End If
        Case 10
          If (edPz_Val10.Focused) And edPz_Descam10.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(10, strTabella, strCampo)
            End If
          End If
        Case 11
          If (edPz_Val11.Focused) And edPz_Descam11.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(11, strTabella, strCampo)
            End If
          End If
        Case 12
          If (edPz_Val12.Focused) And edPz_Descam12.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(12, strTabella, strCampo)
            End If
          End If
        Case 13
          If (edPz_Val13.Focused) And edPz_Descam13.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(13, strTabella, strCampo)
            End If
          End If
        Case 14
          If (edPz_Val14.Focused) And edPz_Descam14.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(14, strTabella, strCampo)
            End If
          End If
        Case 15
          If (edPz_Val15.Focused) And edPz_Descam15.Text <> "" Then
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(15, strTabella, strCampo)
            End If
          End If
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ApriZoomS(ByVal i As Integer)
    Dim nPos As Integer = 0
    Dim strTabella As String = ""
    Dim strCampo As String = ""
    Dim strColonna As String = ""
    Dim oParam As New CLE__PATB

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ((edPz_Vals1.Focused = True) And (edPz_Descams1.Text <> "")) Or _
         ((edPz_Vals2.Focused = True) And (edPz_Descams2.Text <> "")) Or _
         ((edPz_Vals3.Focused = True) And (edPz_Descams3.Text <> "")) Or _
         ((edPz_Vals4.Focused = True) And (edPz_Descams4.Text <> "")) Or _
         ((edPz_Vals5.Focused = True) And (edPz_Descams5.Text <> "")) Then
      Else
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      strColonna = "Pz_Columns" & i
      nPos = InStr(NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna)), ".") - 1
      strTabella = Mid(NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna)), 1, nPos)
      strCampo = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)(strColonna))
      '--------------------------------------------------------------------------------------------------------------
      If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And _
         Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And _
         Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
        NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
        If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
      Else
        ApriZoomTabellaS(i, strTabella, strCampo)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub ApriZoomTabella(ByVal i As Integer, ByVal strTabella As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim oParamHlvl As New CLE__CLDP
    Dim frmHlvl As FRM__HLVL = Nothing
    Dim strValore As String
    Dim lConto As Integer
    Dim j As Integer
    Try
      NTSZOOM.strIn = ""

      Select Case strCampo.ToUpper
        Case "ANAGRA.AN_CONTO", "BUDGET.BU_CONTO", "CODARFO.CAF_CONTO", "COMMESS.CO_CONTO", "DESTDIV.DD_CONTO", _
             "KEYMAG.KM_CONTO", "KEYORD.KO_CONTO", "KEYPRB.KM_CONTO", "LISTINI.LC_CONTO", "LISTSEL.LSE_CONTO", _
             "MOVIVA.MI_CONTO", "ORDLIST.OL_CONTO", "PERPROV.PER_CONTO", "PRINOT.PN_CONTO", _
             "PROBUDGET.PB_CONTO", "PROVVIG.PV_CONTO", "SCADEN.SC_CONTO", "SCONTI.SO_CONTO", "TESTMAG.TM_CONTO", _
             "TESTORD.TD_CONTO", "TESTPRB.TM_CONTO", "ALLOLE.AO_CODICE", "MOVIVA.MI_CONTOCF", "PRINOT.PN_CONTOCF", _
             "TESTMAG.TM_CONTO2", "TESTPRB.TM_CONTO2"
          NTSZOOM.strIn = getPz_Val(i)
          NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
        Case "PRIANA.PA_CONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGCA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
        Case "ARTICO.AR_FORN", "ARTICO.AR_FORN2", "TABCAGE.TB_CODFORN", "TABMAGA.TB_MAGCONTO"

          NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODMARC"

          NTSZOOM.ZoomStrIn("ZOOMTABMARC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVIVA.MI_CONTROP", "PRINOT.PN_CONTROP", "SCADEN.SC_CONTROP", "TABBANC.TB_CONBANC", _
             "TABCAUC.TB_CONTIVA", "TABCAUC.TB_CD_1", "TABCAUC.TB_CD_2", "TABCAUC.TB_CD_3", "TABCAUC.TB_CD_4", _
             "TABCAUC.TB_CA_1", "TABCAUC.TB_CA_2", "TABCAUC.TB_CA_3", "TABCAUC.TB_CA_4", _
             "TABPEAC.TB_ACONCAS", "TABPEAC.TB_ACONABAT", "TABPEAC.TB_ACONABPA", "TABPEAC.TB_ACONOMAG", _
             "TABPEAC.TB_ACONRIBO", "TABPEAC.TB_ACONRIIN", "TABPEAC.TB_ACONRIAC", "TABPEAC.TB_ACONCAUZ", _
             "TABPECG.TB_CONEFF", "TABPECG.TB_CONABAT", "TABPECG.TB_CONABPA", "TABPECG.TB_CONDCAT", _
             "TABPECG.TB_CONDCPA", "TABPEVE.TB_CONCAS", "TABPEVE.TB_CONABAT", "TABPEVE.TB_CONABPA", _
             "TABPEVE.TB_CONOMAG", "TABPEVE.TB_CONRIBO", "TABPEVE.TB_CONRIIN", "TABPEVE.TB_CONRIAC", _
             "TABPEVE.TB_CONCAUZ"

          NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVMAG.MM_CONTOCONTR", "MOVORD.MO_CONTOCONTR", "MOVPRB.MM_CONTOCONTR"

          NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODART", "ARTDEF.AD_CODART", "ARTDEFX.ADX_CODART", "ARTLIF.AL_CODART", _
             "ARTPRO.AP_CODART", "ARTPROX.APX_CODART", "ARTVAL.AX_CODART", "BARCODE.BC_CODART", _
             "BUDGETD.BUD_CODART", "CODARFO.CAF_CODART", "KEYMAG.KM_CODART", "KEYORD.KO_CODART", _
             "KEYPRB.KM_CODART", "LISTINI.LC_CODART", "MOVMAG.MM_CODART", "MOVORD.MO_CODART", _
             "MOVPRB.MM_CODART", "ORDLIST.OL_CODART", "PERPROV.PER_CODART", "PRIANA.PA_CODART", _
             "PROBUDGETD.PBD_CODART", "SCONTI.SO_CODART", "ZZAGAD.ZZ_CODART", "ZZXAGAD.ZZX_CODART", _
             "MOVOFF.MO_CODART"

          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_AGENTE", "ANAGRA.AN_AGENTE2", "PERPROV.PER_CODCAGE", "PROVVIG.PV_CODAGE", _
             "SCADEN.SC_CODCAGE", "TESTMAG.TM_CODAGE", "TESTMAG.TM_CODAGE2", "TESTORD.TD_CODAGEN", _
             "TESTORD.TD_CODAGEN2", "TESTPRB.TM_CODAGEN", "TESTPRB.TM_CODAGEN2"

          NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CATEG"

          NTSZOOM.ZoomStrIn("ZOOMTABCATE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODESE", "ARTICO.AR_CODIVA", "MOVIVA.MI_CODIVA", "MOVMAG.MM_CODIVA", _
             "MOVORD.MO_CODIVA", "MOVPRB.MM_CODIVA", "ORDLIST.OL_CODIVA", "PRINOT.PN_CODIVA", _
             "TABENAS.TB_CODIVFT", "TABPRIV.TB_PCODIVA", _
             "TESTMAG.TM_CODESE", "TESTMAG.TM_CODIVA_1", "TESTMAG.TM_CODIVA_2", "TESTMAG.TM_CODIVA_3", _
             "TESTMAG.TM_CODIVA_4", "TESTMAG.TM_CODIVA_5", "TESTMAG.TM_CODIVA_6", "TESTMAG.TM_CODIVA_7", _
             "TESTMAG.TM_CODIVA_8", "TESTORD.TD_CODESE", _
             "TESTPRB.TM_CODESE", "TESTPRB.TM_CODIVA_1", "TESTPRB.TM_CODIVA_2", "TESTPRB.TM_CODIVA_3", _
             "TESTPRB.TM_CODIVA_4", "TESTPRB.TM_CODIVA_5", "TESTPRB.TM_CODIVA_6", "TESTPRB.TM_CODIVA_7", _
             "TESTPRB.TM_CODIVA_8"

          NTSZOOM.ZoomStrIn("ZOOMTABCIVA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODMAST", "PRIANA.PA_MASTRO", _
             "TABANAZ.TB_MASCLI_1", "TABANAZ.TB_MASCLI_2", "TABANAZ.TB_MASCLI_3", "TABANAZ.TB_MASCLI_4", _
             "TABANAZ.TB_MASCLI_5", "TABANAZ.TB_MASCLI_6", "TABANAZ.TB_MASCLI_7", "TABANAZ.TB_MASCLI_8", _
             "TABANAZ.TB_MASCLI_9", "TABANAZ.TB_MASCLI_10", "TABANAZ.TB_MASFOR_1", "TABANAZ.TB_MASFOR_2", _
             "TABANAZ.TB_MASFOR_3", "TABANAZ.TB_MASFOR_4", "TABANAZ.TB_MASFOR_5", "TABANAZ.TB_MASFOR_6", _
             "TABANAZ.TB_MASFOR_7", "TABANAZ.TB_MASFOR_8", "TABANAZ.TB_MASFOR_9", "TABANAZ.TB_MASFOR_10"

          NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
             "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA"

          NTSZOOM.ZoomStrIn("ZOOMTABPAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_PORTO", "TESTMAG.TM_PORTO", "TESTORD.TD_PORTO", "TESTPRB.TM_PORTO"

          NTSZOOM.ZoomStrIn("ZOOMTABPORT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_VALUTA", "ARTVAL.AX_CODVALU", "CAMBI.WXCODVALU", "LISTINI.LC_CODVALU", _
             "MOVIVA.MI_CODVALU", "ORDLIST.OL_CODVALU", "PRIANA.PA_CODVALU", "PRINOT.PNCODVALU", _
             "SCADEN.SC_CODVALU", "TESTMAG.TM_VALUTA", "TESTORD.TD_VALUTA", "TESTPRB.TM_VALUTA"

          NTSZOOM.ZoomStrIn("ZOOMTABVALU", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_VETT", "TESTMAG.TM_VETTOR", "TESTORD.TD_VETTOR", "TESTPRB.TM_VETTOR"

          NTSZOOM.ZoomStrIn("ZOOMTABVETT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_ZONA", "DESTDIV.DD_CODZONA"

          NTSZOOM.ZoomStrIn("ZOOMTABZONE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "KEYMAG.KM_MAGAZ", "KEYORD.KO_MAGAZ", "KEYPRB.KM_MAGAZ", "ARTPRO.AP_MAGAZ", _
             "MOVMAG.MM_MAGAZ", "MOVMAG.MM_MAGAZ2", "MOVMAG.MM_MAGDISIMP", "MOVMAG.MM_MAGDISIMP2", _
             "MOVORD.MO_MAGAZ", "MOVORD.MO_MAGAZ2", _
             "MOVPRB.MM_MAGAZ", "MOVPRB.MM_MAGAZ2", "MOVPRB.MM_MAGDISIMP", "MOVPRB.MM_MAGDISIMP2", _
             "ORDLIST.OL_MAGAZ", "ORDLIST.OL_MAGAZ2", "TABTPBF.TB_TMAGAZZ", "TABTPBF.TB_TMAGAZZ2", _
             "TESTMAG.TM_MAGAZ", "TESTMAG.TM_MAGAZ2", "TESTORD.TD_MAGAZ", "TESTORD.TD_MAGAZ2", _
             "TESTPRB.TM_MAGAZ", "ARTDEF.AD_MAGAZ", "TESTPRB.TM_MAGAZ2"

          NTSZOOM.ZoomStrIn("ZOOMTABMAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODPDON"

          NTSZOOM.ZoomStrIn("ZOOMTABPDON", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CONTROA", "ARTICO.AR_CONTROP", "MOVMAG.MM_CONTROP", "MOVORD.MO_CONTROP", _
             "MOVPRB.MM_CONTROP", "ORDLIST.OL_CONTROP", "TABCOPE.TB_OCODCONTR", "TABTPBF.TB_TCONTRO", _
             "TESTMAG.TM_CONTROP", "TESTORD.TD_CONTROP", "TESTPRB.TM_CONTROP"

          NTSZOOM.ZoomStrIn("ZOOMTABCOVE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_FAMPROD", "BUDGET.BU_CODCFAM", "BUDGETD.BUD_CODCFAM", "BUDGETDMEN.BUDM_CODCFAM", _
             "BUDGETMEN.BUM_CODCFAM", "MOVMAG.MM_CODCFAM", "MOVORD.MO_CODCFAM", "MOVPRB.MM_CODCFAM", _
             "ORDLIST.OL_CODCFAM", "PRIANA.PA_CODCFAM"

          NTSZOOM.ZoomStrIn("ZOOMTABCFAM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_GRUPPO", "TABSGME.TB_CODGRUPM"

          NTSZOOM.ZoomStrIn("ZOOMTABGMER", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_SOTGRU"

          NTSZOOM.ZoomStrIn("ZOOMTABSGME", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "BUDGET.BU_CODCENA", "BUDGETD.BUD_CODCENA", "BUDGETDMEN.BUDM_CODCENA", "BUDGETMEN.BUM_CODCENA", _
             "MOVMAG.MM_CODCENA", "MOVORD.MO_CODCENA", "MOVPRB.MM_CODCENA", "ORDLIST.OL_CODCENA", _
             "PRIANA.PA_CODCENA", "TABCOPE.TB_OCODCENA", "TABTPBF.TB_VCODCEN", "TESTMAG.TM_CODCENA", _
             "TESTORD.TD_CODCENA", "TESTPRB.TM_CODCENA"

          NTSZOOM.ZoomStrIn("ZOOMTABCENA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "PRIANA.PA_CODLAVO", "PROBUDGETD.PBD_CODLAVO", "TABCOPE.TB_OCODLAVO", "BUDGETD.BUD_CODLAVO"

          NTSZOOM.ZoomStrIn("ZOOMTABLAVO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "CONDBAN.CB_CAUSALE", "PRINOT.PN_CAUSALE", "SCADEN.SC_CAUSALE"

          NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "CONDBAN.CB_CODBANC", "SCADEN.SC_CODBANC", "TASSI.TX_CODBANC"

          NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "LISTINI.LC_CODTPRO", "PERPROV.PER_CODTPRO"

          NTSZOOM.ZoomStrIn("ZOOMTABTPRO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVMAG.MM_CAUSALE", "MOVMAG.MM_CAUSALE2", "MOVPRB.MM_CAUSALE", "MOVPRB.MM_CAUSALE2", _
             "TABTPBF.TB_TCAUMAG", "TABTPBF.TB_TCAUTRA", "TESTMAG.TMCAUSALE", "TESTMAG.CAUSTRA", _
             "TESTORD.TD_CAMBIO", "TESTPRB.TM_CAUSALE", "TESTPRB.TM_CAUSTRA"

          oParam.strCodPdc = "*"
          oParam.nEscomp = -1
          NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "PRIANA.PA_CODCOPE"

          NTSZOOM.ZoomStrIn("ZOOMTABCOPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TABCAGE.TB_CODTBEN"

          NTSZOOM.ZoomStrIn("ZOOMTABENAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TABMAST.TB_CLASMAST"

          NTSZOOM.ZoomStrIn("ZOOMTABCLAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODASPE", "TESTORD.TD_CODASPE", "TESTPRB.TM_CODASPE"

          NTSZOOM.ZoomStrIn("ZOOMTABASPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODNTRA", "TESTPRB.TM_CODNTRA"

          NTSZOOM.ZoomStrIn("ZOOMTABNTRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODPORT", "TESTPRB.TM_CODPORT"

          NTSZOOM.ZoomStrIn("ZOOMTABMPOR", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODRSTA", "TESTPRB.TM_CODRSTA"

          NTSZOOM.ZoomStrIn("ZOOMTABRSTA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"

          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTOFF.TD_CODLEAD", "LEADS.LE_CODLEAD"

          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"

          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ALLOLE.AO_COMMECA", "BUDGET.BU_COMMECA", "BUDGETD.bud_COMMECA", _
             "BUDGETDMEN.BUDM_COMMECA", "BUDGETMEN.BUM_COMMECA", "CICLI.DD_COMMECA", _
             "COMCHIU.CCH_COMMECA", "COMMESS.CO_COMME", "DBNOTE.DBN_COMMECA", _
             "DOCUMTAS.DOC_COMMECA", "FLUSSI.FL_COMMECA", "KEYMAG.KM_COMMECA", _
             "KEYMAG.KM_COMMECAP", "KEYORD.KO_COMMECAP", "KEYPRB.KM_COMMECA", _
             "KEYPRB.KM_COMMECAP", "LAVCENT.LCE_COMMECA", "LISTPROD.LP_COMMECA", _
             "LOTCDEF.LD_COMMECA", "LOTCPRO.LP_COMMECA", "MOVDIS.MD_COMMECA", _
             "MOVFPR.MFP_COMMECA", "MOVMAG.MM_COMMECA", "MOVORD.MO_COMMECA", _
             "MOVPRB.MM_COMMECA", "NNCONSTO.NNC_COMMECA", "NNCONTRA.NNC_COMMECA", _
             "NNMASTO.RL_COMMECA", "NNMATRIC.RL_COMMECA", "ORDLIST.OL_COMMECA", _
             "PREVENT.PRE_COMMECA", "PRIANA.PA_COMMECA", "PROESEB.PES_COMMECA", _
             "PROESEC.PES_COMMECA", "RAPPNC.NC_COMMECA", "RITENUT.RT_COMMECA", _
             "SCADEN.SC_COMMECA", "SUBCOMM.SCO_COMMECA", "TASKS.TSK_COMMECA", _
             "TASKS.TSK_COMMESU", "TESTMAG.TM_COMMECA", "TESTORD.TD_COMMECA", _
             "TESTPRB.TM_COMMECA", "TESTRDA.TD_COMMECA", "TESTRDO.TO_COMMECA", _
             "VALTASK.ALV_COMMECA"

          NTSZOOM.ZoomStrIn("ZOOMCOMMESS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "DESTDIV.DD_CODDEST"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "DESTDIV.DD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Column(j + 1)) = "DESTDIV.DD_CONTO" Then
              If IsNumeric(getPz_Val(j + 1)) Then
                If (NTSCInt(getPz_Val(j + 1)) <> 0) And (NTSCInt(getPz_Val(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Val(j + 1))) Then
                    lConto = NTSCInt(getPz_Val(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Val(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()

            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "TESTMAG.TM_CODDEST", "TESTMAG.TM_CODDEST2"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTMAG.TM_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Column(j + 1)) = "TESTMAG.TM_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Val(j + 1))) Then
                If (NTSCInt(getPz_Val(j + 1)) <> 0) And (NTSCInt(getPz_Val(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Val(j + 1))) Then
                    lConto = NTSCInt(getPz_Val(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Val(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()

            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "TESTORD.TD_CODDEST", "TESTORD.TD_CODDEST2"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTORD.TD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Column(j + 1)) = "TESTORD.TD_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Val(j + 1))) Then
                If (NTSCInt(getPz_Val(j + 1)) <> 0) And (NTSCInt(getPz_Val(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Val(j + 1))) Then
                    lConto = NTSCInt(getPz_Val(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Val(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()

            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "ANAGRA.AN_DESTIN"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTORD.TD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Column(j + 1)) = "ANAGRA.AN_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Val(j + 1))) Then
                If (NTSCInt(getPz_Val(j + 1)) <> 0) And (NTSCInt(getPz_Val(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Val(j + 1))) Then
                    lConto = NTSCInt(getPz_Val(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Val(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()

            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If

        Case Else

          oCleSpar.strHlvlTabella = strTabella
          oCleSpar.strHlvlCampo = strCampo
          frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
          frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
          frmHlvl.InitEntity(oCleSpar)
          frmHlvl.ShowDialog()

          strValore = oCleSpar.strHlvlValore
          If strValore <> "" Then
            oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
          End If
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHlvl Is Nothing Then frmHlvl.Dispose()
      frmHlvl = Nothing
    End Try
  End Sub
  Public Overridable Sub ApriZoomTabellaS(ByVal i As Integer, ByVal strTabella As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim oParamHlvl As New CLE__CLDP
    Dim frmHlvl As FRM__HLVL = Nothing
    Dim strValore As String = ""
    Dim lConto As Integer = 0
    Dim j As Integer = 0

    Try
      Select Case strCampo.ToUpper
        Case "ANAGRA.AN_CONTO", "BUDGET.BU_CONTO", "CODARFO.CAF_CONTO", "COMMESS.CO_CONTO", "DESTDIV.DD_CONTO", _
             "KEYMAG.KM_CONTO", "KEYORD.KO_CONTO", "KEYPRB.KM_CONTO", "LISTINI.LC_CONTO", "LISTSEL.LSE_CONTO", _
             "MOVIVA.MI_CONTO", "ORDLIST.OL_CONTO", "PERPROV.PER_CONTO", "PRINOT.PN_CONTO", _
             "PROBUDGET.PB_CONTO", "PROVVIG.PV_CONTO", "SCADEN.SC_CONTO", "SCONTI.SO_CONTO", "TESTMAG.TM_CONTO", _
             "TESTORD.TD_CONTO", "TESTPRB.TM_CONTO", "ALLOLE.AO_CODICE", "MOVIVA.MI_CONTOCF", "PRINOT.PN_CONTOCF", _
             "TESTMAG.TM_CONTO2", "TESTPRB.TM_CONTO2"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGCA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_FORN", "ARTICO.AR_FORN2", "TABCAGE.TB_CODFORN", "TABMAGA.TB_MAGCONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODMARC"
          NTSZOOM.ZoomStrIn("ZOOMTABMARC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVIVA.MI_CONTROP", "PRINOT.PN_CONTROP", "SCADEN.SC_CONTROP", "TABBANC.TB_CONBANC", _
             "TABCAUC.TB_CONTIVA", "TABCAUC.TB_CD_1", "TABCAUC.TB_CD_2", "TABCAUC.TB_CD_3", "TABCAUC.TB_CD_4", _
             "TABCAUC.TB_CA_1", "TABCAUC.TB_CA_2", "TABCAUC.TB_CA_3", "TABCAUC.TB_CA_4", _
             "TABPEAC.TB_ACONCAS", "TABPEAC.TB_ACONABAT", "TABPEAC.TB_ACONABPA", "TABPEAC.TB_ACONOMAG", _
             "TABPEAC.TB_ACONRIBO", "TABPEAC.TB_ACONRIIN", "TABPEAC.TB_ACONRIAC", "TABPEAC.TB_ACONCAUZ", _
             "TABPECG.TB_CONEFF", "TABPECG.TB_CONABAT", "TABPECG.TB_CONABPA", "TABPECG.TB_CONDCAT", _
             "TABPECG.TB_CONDCPA", "TABPEVE.TB_CONCAS", "TABPEVE.TB_CONABAT", "TABPEVE.TB_CONABPA", _
             "TABPEVE.TB_CONOMAG", "TABPEVE.TB_CONRIBO", "TABPEVE.TB_CONRIIN", "TABPEVE.TB_CONRIAC", _
             "TABPEVE.TB_CONCAUZ"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVMAG.MM_CONTOCONTR", "MOVORD.MO_CONTOCONTR", "MOVPRB.MM_CONTOCONTR"
          NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODART", "ARTDEF.AD_CODART", "ARTDEFX.ADX_CODART", "ARTLIF.AL_CODART", _
             "ARTPRO.AP_CODART", "ARTPROX.APX_CODART", "ARTVAL.AX_CODART", "BARCODE.BC_CODART", _
             "BUDGETD.BUD_CODART", "CODARFO.CAF_CODART", "KEYMAG.KM_CODART", "KEYORD.KO_CODART", _
             "KEYPRB.KM_CODART", "LISTINI.LC_CODART", "MOVMAG.MM_CODART", "MOVORD.MO_CODART", _
             "MOVPRB.MM_CODART", "ORDLIST.OL_CODART", "PERPROV.PER_CODART", "PRIANA.PA_CODART", _
             "PROBUDGETD.PBD_CODART", "SCONTI.SO_CODART", "ZZAGAD.ZZ_CODART", "ZZXAGAD.ZZX_CODART", _
             "MOVOFF.MO_CODART"
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_AGENTE", "ANAGRA.AN_AGENTE2", "PERPROV.PER_CODCAGE", "PROVVIG.PV_CODAGE", _
             "SCADEN.SC_CODCAGE", "TESTMAG.TM_CODAGE", "TESTMAG.TM_CODAGE2", "TESTORD.TD_CODAGEN", _
             "TESTORD.TD_CODAGEN2", "TESTPRB.TM_CODAGEN", "TESTPRB.TM_CODAGEN2"
          NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CATEG"
          NTSZOOM.ZoomStrIn("ZOOMTABCATE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODESE", "ARTICO.AR_CODIVA", "MOVIVA.MI_CODIVA", "MOVMAG.MM_CODIVA", _
             "MOVORD.MO_CODIVA", "MOVPRB.MM_CODIVA", "ORDLIST.OL_CODIVA", "PRINOT.PN_CODIVA", _
             "TABENAS.TB_CODIVFT", "TABPRIV.TB_PCODIVA", _
             "TESTMAG.TM_CODESE", "TESTMAG.TM_CODIVA_1", "TESTMAG.TM_CODIVA_2", "TESTMAG.TM_CODIVA_3", _
             "TESTMAG.TM_CODIVA_4", "TESTMAG.TM_CODIVA_5", "TESTMAG.TM_CODIVA_6", "TESTMAG.TM_CODIVA_7", _
             "TESTMAG.TM_CODIVA_8", "TESTORD.TD_CODESE", _
             "TESTPRB.TM_CODESE", "TESTPRB.TM_CODIVA_1", "TESTPRB.TM_CODIVA_2", "TESTPRB.TM_CODIVA_3", _
             "TESTPRB.TM_CODIVA_4", "TESTPRB.TM_CODIVA_5", "TESTPRB.TM_CODIVA_6", "TESTPRB.TM_CODIVA_7", _
             "TESTPRB.TM_CODIVA_8"
          NTSZOOM.ZoomStrIn("ZOOMTABCIVA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODMAST", "PRIANA.PA_MASTRO", _
             "TABANAZ.TB_MASCLI_1", "TABANAZ.TB_MASCLI_2", "TABANAZ.TB_MASCLI_3", "TABANAZ.TB_MASCLI_4", _
             "TABANAZ.TB_MASCLI_5", "TABANAZ.TB_MASCLI_6", "TABANAZ.TB_MASCLI_7", "TABANAZ.TB_MASCLI_8", _
             "TABANAZ.TB_MASCLI_9", "TABANAZ.TB_MASCLI_10", "TABANAZ.TB_MASFOR_1", "TABANAZ.TB_MASFOR_2", _
             "TABANAZ.TB_MASFOR_3", "TABANAZ.TB_MASFOR_4", "TABANAZ.TB_MASFOR_5", "TABANAZ.TB_MASFOR_6", _
             "TABANAZ.TB_MASFOR_7", "TABANAZ.TB_MASFOR_8", "TABANAZ.TB_MASFOR_9", "TABANAZ.TB_MASFOR_10"
          NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
             "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA"
          NTSZOOM.ZoomStrIn("ZOOMTABPAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_PORTO", "TESTMAG.TM_PORTO", "TESTORD.TD_PORTO", "TESTPRB.TM_PORTO"
          NTSZOOM.ZoomStrIn("ZOOMTABPORT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_VALUTA", "ARTVAL.AX_CODVALU", "CAMBI.WXCODVALU", "LISTINI.LC_CODVALU", _
             "MOVIVA.MI_CODVALU", "ORDLIST.OL_CODVALU", "PRIANA.PA_CODVALU", "PRINOT.PNCODVALU", _
             "SCADEN.SC_CODVALU", "TESTMAG.TM_VALUTA", "TESTORD.TD_VALUTA", "TESTPRB.TM_VALUTA"
          NTSZOOM.ZoomStrIn("ZOOMTABVALU", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_VETT", "TESTMAG.TM_VETTOR", "TESTORD.TD_VETTOR", "TESTPRB.TM_VETTOR"
          NTSZOOM.ZoomStrIn("ZOOMTABVETT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_ZONA", "DESTDIV.DD_CODZONA"
          NTSZOOM.ZoomStrIn("ZOOMTABZONE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "KEYMAG.KM_MAGAZ", "KEYORD.KO_MAGAZ", "KEYPRB.KM_MAGAZ", "ARTPRO.AP_MAGAZ", _
             "MOVMAG.MM_MAGAZ", "MOVMAG.MM_MAGAZ2", "MOVMAG.MM_MAGDISIMP", "MOVMAG.MM_MAGDISIMP2", _
             "MOVORD.MO_MAGAZ", "MOVORD.MO_MAGAZ2", _
             "MOVPRB.MM_MAGAZ", "MOVPRB.MM_MAGAZ2", "MOVPRB.MM_MAGDISIMP", "MOVPRB.MM_MAGDISIMP2", _
             "ORDLIST.OL_MAGAZ", "ORDLIST.OL_MAGAZ2", "TABTPBF.TB_TMAGAZZ", "TABTPBF.TB_TMAGAZZ2", _
             "TESTMAG.TM_MAGAZ", "TESTMAG.TM_MAGAZ2", "TESTORD.TD_MAGAZ", "TESTORD.TD_MAGAZ2", _
             "TESTPRB.TM_MAGAZ", "ARTDEF.AD_MAGAZ", "TESTPRB.TM_MAGAZ2"
          NTSZOOM.ZoomStrIn("ZOOMTABMAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODPDON"
          NTSZOOM.ZoomStrIn("ZOOMTABPDON", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CONTROA", "ARTICO.AR_CONTROP", "MOVMAG.MM_CONTROP", "MOVORD.MO_CONTROP", _
             "MOVPRB.MM_CONTROP", "ORDLIST.OL_CONTROP", "TABCOPE.TB_OCODCONTR", "TABTPBF.TB_TCONTRO", _
             "TESTMAG.TM_CONTROP", "TESTORD.TD_CONTROP", "TESTPRB.TM_CONTROP"
          NTSZOOM.ZoomStrIn("ZOOMTABCOVE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_FAMPROD", "BUDGET.BU_CODCFAM", "BUDGETD.BUD_CODCFAM", "BUDGETDMEN.BUDM_CODCFAM", _
             "BUDGETMEN.BUM_CODCFAM", "MOVMAG.MM_CODCFAM", "MOVORD.MO_CODCFAM", "MOVPRB.MM_CODCFAM", _
             "ORDLIST.OL_CODCFAM", "PRIANA.PA_CODCFAM"
          NTSZOOM.ZoomStrIn("ZOOMTABCFAM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_GRUPPO", "TABSGME.TB_CODGRUPM"
          NTSZOOM.ZoomStrIn("ZOOMTABGMER", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_SOTGRU"
          NTSZOOM.ZoomStrIn("ZOOMTABSGME", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "BUDGET.BU_CODCENA", "BUDGETD.BUD_CODCENA", "BUDGETDMEN.BUDM_CODCENA", "BUDGETMEN.BUM_CODCENA", _
             "MOVMAG.MM_CODCENA", "MOVORD.MO_CODCENA", "MOVPRB.MM_CODCENA", "ORDLIST.OL_CODCENA", _
             "PRIANA.PA_CODCENA", "TABCOPE.TB_OCODCENA", "TABTPBF.TB_VCODCEN", "TESTMAG.TM_CODCENA", _
             "TESTORD.TD_CODCENA", "TESTPRB.TM_CODCENA"
          NTSZOOM.ZoomStrIn("ZOOMTABCENA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CODLAVO", "PROBUDGETD.PBD_CODLAVO", "TABCOPE.TB_OCODLAVO", "BUDGETD.BUD_CODLAVO"
          NTSZOOM.ZoomStrIn("ZOOMTABLAVO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "CONDBAN.CB_CAUSALE", "PRINOT.PN_CAUSALE", "SCADEN.SC_CAUSALE"
          NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "CONDBAN.CB_CODBANC", "SCADEN.SC_CODBANC", "TASSI.TX_CODBANC"
          NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "LISTINI.LC_CODTPRO", "PERPROV.PER_CODTPRO"
          NTSZOOM.ZoomStrIn("ZOOMTABTPRO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVMAG.MM_CAUSALE", "MOVMAG.MM_CAUSALE2", "MOVPRB.MM_CAUSALE", "MOVPRB.MM_CAUSALE2", _
             "TABTPBF.TB_TCAUMAG", "TABTPBF.TB_TCAUTRA", "TESTMAG.TMCAUSALE", "TESTMAG.CAUSTRA", _
             "TESTORD.TD_CAMBIO", "TESTPRB.TM_CAUSALE", "TESTPRB.TM_CAUSTRA"
          oParam.strCodPdc = "*"
          oParam.nEscomp = -1
          NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CODCOPE"
          NTSZOOM.ZoomStrIn("ZOOMTABCOPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TABCAGE.TB_CODTBEN"
          NTSZOOM.ZoomStrIn("ZOOMTABENAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TABMAST.TB_CLASMAST"
          NTSZOOM.ZoomStrIn("ZOOMTABCLAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODASPE", "TESTORD.TD_CODASPE", "TESTPRB.TM_CODASPE"
          NTSZOOM.ZoomStrIn("ZOOMTABASPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODNTRA", "TESTPRB.TM_CODNTRA"
          NTSZOOM.ZoomStrIn("ZOOMTABNTRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODPORT", "TESTPRB.TM_CODPORT"
          NTSZOOM.ZoomStrIn("ZOOMTABMPOR", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODRSTA", "TESTPRB.TM_CODRSTA"
          NTSZOOM.ZoomStrIn("ZOOMTABRSTA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"
          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTOFF.TD_CODLEAD", "LEADS.LE_CODLEAD"
          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"
          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ALLOLE.AO_COMMECA", "BUDGET.BU_COMMECA", "BUDGETD.bud_COMMECA", _
             "BUDGETDMEN.BUDM_COMMECA", "BUDGETMEN.BUM_COMMECA", "CICLI.DD_COMMECA", _
             "COMCHIU.CCH_COMMECA", "COMMESS.CO_COMME", "DBNOTE.DBN_COMMECA", _
             "DOCUMTAS.DOC_COMMECA", "FLUSSI.FL_COMMECA", "KEYMAG.KM_COMMECA", _
             "KEYMAG.KM_COMMECAP", "KEYORD.KO_COMMECAP", "KEYPRB.KM_COMMECA", _
             "KEYPRB.KM_COMMECAP", "LAVCENT.LCE_COMMECA", "LISTPROD.LP_COMMECA", _
             "LOTCDEF.LD_COMMECA", "LOTCPRO.LP_COMMECA", "MOVDIS.MD_COMMECA", _
             "MOVFPR.MFP_COMMECA", "MOVMAG.MM_COMMECA", "MOVORD.MO_COMMECA", _
             "MOVPRB.MM_COMMECA", "NNCONSTO.NNC_COMMECA", "NNCONTRA.NNC_COMMECA", _
             "NNMASTO.RL_COMMECA", "NNMATRIC.RL_COMMECA", "ORDLIST.OL_COMMECA", _
             "PREVENT.PRE_COMMECA", "PRIANA.PA_COMMECA", "PROESEB.PES_COMMECA", _
             "PROESEC.PES_COMMECA", "RAPPNC.NC_COMMECA", "RITENUT.RT_COMMECA", _
             "SCADEN.SC_COMMECA", "SUBCOMM.SCO_COMMECA", "TASKS.TSK_COMMECA", _
             "TASKS.TSK_COMMESU", "TESTMAG.TM_COMMECA", "TESTORD.TD_COMMECA", _
             "TESTPRB.TM_COMMECA", "TESTRDA.TD_COMMECA", "TESTRDO.TO_COMMECA", _
             "VALTASK.ALV_COMMECA"
          NTSZOOM.ZoomStrIn("ZOOMCOMMESS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "DESTDIV.DD_CODDEST"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "DESTDIV.DD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Columns(j + 1)) = "DESTDIV.DD_CONTO" Then
              If IsNumeric(getPz_Vals(j + 1)) Then
                If (NTSCInt(getPz_Vals(j + 1)) <> 0) And (NTSCInt(getPz_Vals(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Vals(j + 1))) Then
                    lConto = NTSCInt(getPz_Vals(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Vals(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()
            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "TESTMAG.TM_CODDEST", "TESTMAG.TM_CODDEST2"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTMAG.TM_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Columns(j + 1)) = "TESTMAG.TM_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Vals(j + 1))) Then
                If (NTSCInt(getPz_Vals(j + 1)) <> 0) And (NTSCInt(getPz_Vals(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Vals(j + 1))) Then
                    lConto = NTSCInt(getPz_Vals(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Vals(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()
            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "TESTORD.TD_CODDEST", "TESTORD.TD_CODDEST2"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTORD.TD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Columns(j + 1)) = "TESTORD.TD_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Vals(j + 1))) Then
                If (NTSCInt(getPz_Vals(j + 1)) <> 0) And (NTSCInt(getPz_Vals(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Vals(j + 1))) Then
                    lConto = NTSCInt(getPz_Vals(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Vals(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()

            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
          '------------------------------------------------------------------------------
        Case "ANAGRA.AN_DESTIN"
          '------------------------------------------------------------------------------
          '--- Se lo zoom è stato richiamato su "Codice destinazione",
          '--- cerca se esiste un altro campo contenente "TESTORD.TD_CONTO" valido,
          '--- per poi richiare ZOOMDESTDIV, altrimenti quello generico
          '------------------------------------------------------------------------------
          lConto = 0
          For j = 0 To 14
            If UCase(getPz_Columns(j + 1)) = "ANAGRA.AN_CONTO" Then
              If IsNumeric(NTSCInt(getPz_Vals(j + 1))) Then
                If (NTSCInt(getPz_Vals(j + 1)) <> 0) And (NTSCInt(getPz_Vals(j + 1)) <> 999999999) Then
                  If oCleSpar.GetConto(NTSCStr(getPz_Vals(j + 1))) Then
                    lConto = NTSCInt(getPz_Vals(j + 1))
                    Exit For
                  End If
                End If
              End If
            End If
          Next
          If lConto <> 0 Then
            NTSZOOM.strIn = getPz_Vals(i)
            oParam.lContoCF = lConto
            NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
            If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
          Else
            oCleSpar.strHlvlTabella = strTabella
            oCleSpar.strHlvlCampo = strCampo
            frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
            frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
            frmHlvl.InitEntity(oCleSpar)
            frmHlvl.ShowDialog()
            strValore = oCleSpar.strHlvlValore
            If strValore <> "" Then
              oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
            End If
          End If
        Case Else
          oCleSpar.strHlvlTabella = strTabella
          oCleSpar.strHlvlCampo = strCampo
          frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
          frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
          frmHlvl.InitEntity(oCleSpar)
          frmHlvl.ShowDialog()
          strValore = oCleSpar.strHlvlValore
          If strValore <> "" Then
            oCleSpar.SalvaValore(i, NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform), strValore)
          End If
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmHlvl Is Nothing Then frmHlvl.Dispose()
      frmHlvl = Nothing
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Val15.NTSDbField = "parstag.pz_val15"
      edPz_Val14.NTSDbField = "parstag.pz_val14"
      edPz_Val13.NTSDbField = "parstag.pz_val13"
      edPz_Val12.NTSDbField = "parstag.pz_val12"
      edPz_Val11.NTSDbField = "parstag.pz_val11"
      edPz_Val10.NTSDbField = "parstag.pz_val10"
      edPz_Val9.NTSDbField = "parstag.pz_val9"
      edPz_Val8.NTSDbField = "parstag.pz_val8"
      edPz_Val7.NTSDbField = "parstag.pz_val7"
      edPz_Val6.NTSDbField = "parstag.pz_val6"
      edPz_Val5.NTSDbField = "parstag.pz_val5"
      edPz_Val4.NTSDbField = "parstag.pz_val4"
      edPz_Val3.NTSDbField = "parstag.pz_val3"
      edPz_Val2.NTSDbField = "parstag.pz_val2"
      edPz_Val1.NTSDbField = "parstag.pz_val1"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals1.NTSDbField = "parstag.pz_vals1"
      edPz_Vals2.NTSDbField = "parstag.pz_vals2"
      edPz_Vals3.NTSDbField = "parstag.pz_vals3"
      edPz_Vals4.NTSDbField = "parstag.pz_vals4"
      edPz_Vals5.NTSDbField = "parstag.pz_vals5"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op15.NTSDbField = "parstag.pz_op15"
      cbPz_Op14.NTSDbField = "parstag.pz_op14"
      cbPz_Op13.NTSDbField = "parstag.pz_op13"
      cbPz_Op12.NTSDbField = "parstag.pz_op12"
      cbPz_Op11.NTSDbField = "parstag.pz_op11"
      cbPz_Op10.NTSDbField = "parstag.pz_op10"
      cbPz_Op9.NTSDbField = "parstag.pz_op9"
      cbPz_Op8.NTSDbField = "parstag.pz_op8"
      cbPz_Op7.NTSDbField = "parstag.pz_op7"
      cbPz_Op6.NTSDbField = "parstag.pz_op6"
      cbPz_Op5.NTSDbField = "parstag.pz_op5"
      cbPz_Op4.NTSDbField = "parstag.pz_op4"
      cbPz_Op3.NTSDbField = "parstag.pz_op3"
      cbPz_Op2.NTSDbField = "parstag.pz_op2"
      cbPz_Op1.NTSDbField = "parstag.pz_op1"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.NTSDbField = "parstag.pz_ops1"
      cbPz_Ops2.NTSDbField = "parstag.pz_ops2"
      cbPz_Ops3.NTSDbField = "parstag.pz_ops3"
      cbPz_Ops4.NTSDbField = "parstag.pz_ops4"
      cbPz_Ops5.NTSDbField = "parstag.pz_ops5"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descam15.NTSDbField = "parstag.pz_descam15"
      edPz_Descam14.NTSDbField = "parstag.pz_descam14"
      edPz_Descam13.NTSDbField = "parstag.pz_descam13"
      edPz_Descam12.NTSDbField = "parstag.pz_descam12"
      edPz_Descam11.NTSDbField = "parstag.pz_descam11"
      edPz_Descam10.NTSDbField = "parstag.pz_descam10"
      edPz_Descam9.NTSDbField = "parstag.pz_descam9"
      edPz_Descam8.NTSDbField = "parstag.pz_descam8"
      edPz_Descam7.NTSDbField = "parstag.pz_descam7"
      edPz_Descam6.NTSDbField = "parstag.pz_descam6"
      edPz_Descam5.NTSDbField = "parstag.pz_descam5"
      edPz_Descam4.NTSDbField = "parstag.pz_descam4"
      edPz_Descam3.NTSDbField = "parstag.pz_descam3"
      edPz_Descam2.NTSDbField = "parstag.pz_descam2"
      edPz_Descam1.NTSDbField = "parstag.pz_descam1"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descams1.NTSDbField = "parstag.pz_descams1"
      edPz_Descams2.NTSDbField = "parstag.pz_descams2"
      edPz_Descams3.NTSDbField = "parstag.pz_descams3"
      edPz_Descams4.NTSDbField = "parstag.pz_descams4"
      edPz_Descams5.NTSDbField = "parstag.pz_descams5"
      '--------------------------------------------------------------------------------------------------------------
      edPz_strOprel.NTSDbField = "parstag.pz_stroprel"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcSpar, Me)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttOperatore As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttOperatore.Columns.Add("cod", GetType(String))
      dttOperatore.Columns.Add("val", GetType(String))
      dttOperatore.Rows.Add(New Object() {"=", "="})
      dttOperatore.Rows.Add(New Object() {"<>", "<>"})
      dttOperatore.Rows.Add(New Object() {">", ">"})
      dttOperatore.Rows.Add(New Object() {">=", ">="})
      dttOperatore.Rows.Add(New Object() {"<", "<"})
      dttOperatore.Rows.Add(New Object() {"<=", "<="})
      dttOperatore.Rows.Add(New Object() {"Like", "Like"})
      dttOperatore.Rows.Add(New Object() {"IS NULL", "IS NULL"})
      dttOperatore.Rows.Add(New Object() {"IS NOT NULL", "IS NOT NULL"})
      dttOperatore.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op1.DataSource = dttOperatore
      cbPz_Op1.ValueMember = "cod"
      cbPz_Op1.DisplayMember = "val"
      cbPz_Op2.DataSource = dttOperatore
      cbPz_Op2.ValueMember = "cod"
      cbPz_Op2.DisplayMember = "val"
      cbPz_Op3.DataSource = dttOperatore
      cbPz_Op3.ValueMember = "cod"
      cbPz_Op3.DisplayMember = "val"
      cbPz_Op4.DataSource = dttOperatore
      cbPz_Op4.ValueMember = "cod"
      cbPz_Op4.DisplayMember = "val"
      cbPz_Op5.DataSource = dttOperatore
      cbPz_Op5.ValueMember = "cod"
      cbPz_Op5.DisplayMember = "val"
      cbPz_Op6.DataSource = dttOperatore
      cbPz_Op6.ValueMember = "cod"
      cbPz_Op6.DisplayMember = "val"
      cbPz_Op7.DataSource = dttOperatore
      cbPz_Op7.ValueMember = "cod"
      cbPz_Op7.DisplayMember = "val"
      cbPz_Op8.DataSource = dttOperatore
      cbPz_Op8.ValueMember = "cod"
      cbPz_Op8.DisplayMember = "val"
      cbPz_Op9.DataSource = dttOperatore
      cbPz_Op9.ValueMember = "cod"
      cbPz_Op9.DisplayMember = "val"
      cbPz_Op10.DataSource = dttOperatore
      cbPz_Op10.ValueMember = "cod"
      cbPz_Op10.DisplayMember = "val"
      cbPz_Op11.DataSource = dttOperatore
      cbPz_Op11.ValueMember = "cod"
      cbPz_Op11.DisplayMember = "val"
      cbPz_Op12.DataSource = dttOperatore
      cbPz_Op12.ValueMember = "cod"
      cbPz_Op12.DisplayMember = "val"
      cbPz_Op13.DataSource = dttOperatore
      cbPz_Op13.ValueMember = "cod"
      cbPz_Op13.DisplayMember = "val"
      cbPz_Op14.DataSource = dttOperatore
      cbPz_Op14.ValueMember = "cod"
      cbPz_Op14.DisplayMember = "val"
      cbPz_Op15.DataSource = dttOperatore
      cbPz_Op15.ValueMember = "cod"
      cbPz_Op15.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.DataSource = dttOperatore
      cbPz_Ops1.ValueMember = "cod"
      cbPz_Ops1.DisplayMember = "val"
      cbPz_Ops2.DataSource = dttOperatore
      cbPz_Ops2.ValueMember = "cod"
      cbPz_Ops2.DisplayMember = "val"
      cbPz_Ops3.DataSource = dttOperatore
      cbPz_Ops3.ValueMember = "cod"
      cbPz_Ops3.DisplayMember = "val"
      cbPz_Ops4.DataSource = dttOperatore
      cbPz_Ops4.ValueMember = "cod"
      cbPz_Ops4.DisplayMember = "val"
      cbPz_Ops5.DataSource = dttOperatore
      cbPz_Ops5.ValueMember = "cod"
      cbPz_Ops5.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      CaricaCbParstag()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub CaricaCbParstag()
    Dim ds As New DataSet
    Dim dttCodici As New DataTable()
    Dim i As Integer
    Dim strCodici As String
    Try
      oCleSpar.LeggiOpzioniUt()
      oCleSpar.GetCodici(ds)

      If ds.Tables("PARSTAG").Rows.Count > 0 Then
        dttCodici.Columns.Add("cod", GetType(String))
        dttCodici.Columns.Add("val", GetType(String))
        For i = 0 To ds.Tables("PARSTAG").Rows.Count - 1
          strCodici = NTSCStr(ds.Tables("PARSTAG").Rows(i)!Pz_Codform) & " - " & NTSCStr(ds.Tables("PARSTAG").Rows(i)!Pz_Titstam)
          dttCodici.Rows.Add(New Object() {NTSCStr(ds.Tables("PARSTAG").Rows(i)!Pz_Codform), strCodici})
          dttCodici.AcceptChanges()
        Next
      End If
      cbParstag.DataSource = dttCodici
      cbParstag.ValueMember = "cod"
      cbParstag.DisplayMember = "val"
      cbParstag.SelectedIndex = 0 'Seleziona I° Elemento nella Combo

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Elabora()
    Dim i As Integer = 0
    Dim nRec As Integer = 0
    Dim strSQL As String = ""
    Dim strOprel As String = ""
    Dim strOprel2 As String = ""
    Dim strOp(16) As String
    Dim strOps(6) As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      tlbCambioDitta.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      If cbParstag.Text = "" Then Return
      If dsSpar.Tables("PARSTAG").Rows.Count = 0 Then Return
      If Not oCleSpar.ControllaCoerenzaWhere(edPz_strOprel.Text) Then Return
      If Not oCleSpar.ControllaCoerenzaWhere(edPz_strOprel2.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If (Not ((NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt) = "" Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt Is System.DBNull.Value)) And _
         (NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql) = "" Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql Is System.DBNull.Value)) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSpar.ControllaValoreCampo(cbParstag.Text, _
          cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
          cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
          edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
          edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
          edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
          edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
          cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text, _
          edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
          edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpar.ControllaValoreCampoS(cbParstag.Text, _
        cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
        edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
        edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esiste, tra i filtri, è stato indicato un campo di 'Memo',
      '--- l'operatore non deve essere diverso da 'Like', 'IS NULL', 'IS NOT NULL'
      '--------------------------------------------------------------------------------------------------------------
      strOp(1) = cbPz_Op1.SelectedValue : strOp(2) = cbPz_Op2.SelectedValue : strOp(3) = cbPz_Op3.SelectedValue
      strOp(4) = cbPz_Op4.SelectedValue : strOp(5) = cbPz_Op5.SelectedValue : strOp(6) = cbPz_Op6.SelectedValue
      strOp(7) = cbPz_Op7.SelectedValue : strOp(8) = cbPz_Op8.SelectedValue : strOp(9) = cbPz_Op9.SelectedValue
      strOp(10) = cbPz_Op10.SelectedValue : strOp(11) = cbPz_Op11.SelectedValue : strOp(12) = cbPz_Op12.SelectedValue
      strOp(13) = cbPz_Op13.SelectedValue : strOp(14) = cbPz_Op14.SelectedValue : strOp(15) = cbPz_Op15.SelectedValue
      For i = 1 To 15
        If NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Tipo" & i.ToString)) = 11 Then
          Select Case strOp(i).ToUpper
            Case "LIKE", "IS NULL", "IS NOT NULL"
            Case Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 129271272470118608, "Attenzione!" & vbCrLf & _
                "Il campo '|" & i.ToString & "|' è di tipo 'Memo'." & vbCrLf & _
                "L'operatore selezionato può SOLO essere:" & vbCrLf & _
                " . Like" & vbCrLf & _
                " . IS NULL" & vbCrLf & _
                " . IS NOT NULL"))
              Return
          End Select
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esiste, tra i filtri, è stato indicato un campo di 'Memo',
      '--- l'operatore non deve essere diverso da 'Like', 'IS NULL', 'IS NOT NULL'
      '--------------------------------------------------------------------------------------------------------------
      strOps(1) = cbPz_Ops1.SelectedValue : strOps(2) = cbPz_Ops2.SelectedValue : strOp(3) = cbPz_Ops3.SelectedValue
      strOps(4) = cbPz_Ops4.SelectedValue : strOps(5) = cbPz_Ops5.SelectedValue
      For i = 1 To 5
        If NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Tipos" & i.ToString)) = 11 Then
          Select Case strOps(i).ToUpper
            Case "LIKE", "IS NULL", "IS NOT NULL"
            Case Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 130450475222738669, "Attenzione!" & vbCrLf & _
                "Il campo '|" & i.ToString & "|' è di tipo 'Memo'." & vbCrLf & _
                "L'operatore selezionato può SOLO essere:" & vbCrLf & _
                " . Like" & vbCrLf & _
                " . IS NULL" & vbCrLf & _
                " . IS NOT NULL"))
              Return
          End Select
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt) = "" Or _
        dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt Is System.DBNull.Value Then
        strSQL = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql)
        If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel Is System.DBNull.Value Then
          strOprel = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel)
        Else
          strOprel = ""
        End If
        If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2 Is System.DBNull.Value Then
          strOprel2 = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2)
        Else
          strOprel2 = ""
        End If
      Else
        If NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql) = "" Or _
          dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql Is System.DBNull.Value Then
          Return
        Else
          strSQL = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql)
          If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel Is System.DBNull.Value Then
            strOprel = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel)
          Else
            strOprel = ""
          End If
          If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2 Is System.DBNull.Value Then
            strOprel2 = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2)
          Else
            strOprel2 = ""
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      strSQL = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql)
      strSQL = oCleSpar.CreateQuerySql(strSQL, strOprel, strOprel2, _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_maintab), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt), _
        NTSCInt(cbPz_Op1.SelectedIndex), NTSCInt(cbPz_Op2.SelectedIndex), NTSCInt(cbPz_Op3.SelectedIndex), _
        NTSCInt(cbPz_Op4.SelectedIndex), NTSCInt(cbPz_Op5.SelectedIndex), NTSCInt(cbPz_Op6.SelectedIndex), _
        NTSCInt(cbPz_Op7.SelectedIndex), NTSCInt(cbPz_Op8.SelectedIndex), NTSCInt(cbPz_Op9.SelectedIndex), _
        NTSCInt(cbPz_Op10.SelectedIndex), _
        edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
        edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
        edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
        edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo1), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo2), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo3), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo4), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo5), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo6), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo7), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo8), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo9), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo10), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column1), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column2), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column3), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column4), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column5), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column6), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column7), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column8), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column9), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column10), _
        cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
        cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
        NTSCInt(cbPz_Op11.SelectedIndex), NTSCInt(cbPz_Op12.SelectedIndex), NTSCInt(cbPz_Op13.SelectedIndex), _
        NTSCInt(cbPz_Op14.SelectedIndex), NTSCInt(cbPz_Op15.SelectedIndex), _
        edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
        edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text, _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo11), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo12), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo13), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo14), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo15), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column11), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column12), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column13), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column14), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column15), _
        cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text, _
        NTSCInt(cbPz_Ops1.SelectedIndex), NTSCInt(cbPz_Ops2.SelectedIndex), NTSCInt(cbPz_Ops3.SelectedIndex), NTSCInt(cbPz_Ops4.SelectedIndex), NTSCInt(cbPz_Ops5.SelectedIndex), _
        edPz_Descams1.Text, edPz_Descams2.Text, edPz_Descams3.Text, edPz_Descams4.Text, edPz_Descams5.Text, _
        edPz_Vals1.Text, edPz_Vals2.Text, edPz_Vals3.Text, edPz_Vals4.Text, edPz_Vals5.Text, _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos1), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos2), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos3), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos4), _
        NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos5), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns1), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns2), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns3), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns4), _
        NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns5), _
        cbPz_Ops1.Text, cbPz_Ops2.Text, cbPz_Ops3.Text, cbPz_Ops4.Text, cbPz_Ops5.Text)
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpar.Esegui(strSQL, nRec) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128326974639684031, "Operazione Terminata.  Record Modificati ") & nRec.ToString)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128735677285677285, "Errore Istruzione SQL NON eseguita. Record Modificati: 0"))
      End If
      If bAnagen = True Then tlbCambioDitta.Enabled = True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function Espandifiltri() As Boolean
    Try
      edDisplayfiltri.Text = oCleSpar.EspandiFiltri(edPz_strOprel.Text, _
              NTSCInt(cbPz_Op1.SelectedIndex), NTSCInt(cbPz_Op2.SelectedIndex), NTSCInt(cbPz_Op3.SelectedIndex), _
              NTSCInt(cbPz_Op4.SelectedIndex), NTSCInt(cbPz_Op5.SelectedIndex), NTSCInt(cbPz_Op6.SelectedIndex), _
              NTSCInt(cbPz_Op7.SelectedIndex), NTSCInt(cbPz_Op8.SelectedIndex), NTSCInt(cbPz_Op9.SelectedIndex), _
              NTSCInt(cbPz_Op10.SelectedIndex), _
              edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
              edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
              edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
              edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
              NTSCInt(cbPz_Op11.SelectedIndex), NTSCInt(cbPz_Op12.SelectedIndex), NTSCInt(cbPz_Op13.SelectedIndex), _
              NTSCInt(cbPz_Op14.SelectedIndex), NTSCInt(cbPz_Op15.SelectedIndex), _
              edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
              edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text)
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function EspandifiltriS() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      edDisplayfiltriS.Text = oCleSpar.EspandiFiltriS(edPz_strOprel2.Text, _
        NTSCInt(cbPz_Ops1.SelectedIndex), NTSCInt(cbPz_Ops2.SelectedIndex), NTSCInt(cbPz_Ops3.SelectedIndex), _
        NTSCInt(cbPz_Ops4.SelectedIndex), NTSCInt(cbPz_Ops5.SelectedIndex), _
        edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
        edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub LeggiDatiDitta()
    Dim bDbMultiDitta As Boolean = False
    Dim dtTmp As DateTime = Nothing
    Try
      bAnagen = False
Riprova:
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      'se oCallParams.strOpen <> "" non visualizzera lo zoom ditte e prednerà la ditta passata da lchild padre
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BN__SPAR", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Me.Close()

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Enabled = False
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      '-------------------------------------------------
      'leggo le informazioni relative alla ditta corrente
      If Not oCleSpar.LeggiDatiDitta(DittaCorrente) Then Me.Close()
      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)

      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function getPz_Val(ByVal i As Integer) As String
    getPz_Val = ""
    Try
      Select Case i
        Case 1
          Return edPz_Val1.Text
        Case 2
          Return edPz_Val2.Text
        Case 3
          Return edPz_Val3.Text
        Case 4
          Return edPz_Val4.Text
        Case 5
          Return edPz_Val5.Text
        Case 6
          Return edPz_Val6.Text
        Case 7
          Return edPz_Val7.Text
        Case 8
          Return edPz_Val8.Text
        Case 9
          Return edPz_Val9.Text
        Case 10
          Return edPz_Val10.Text
        Case 11
          Return edPz_Val11.Text
        Case 12
          Return edPz_Val12.Text
        Case 13
          Return edPz_Val13.Text
        Case 14
          Return edPz_Val14.Text
        Case 15
          Return edPz_Val15.Text

      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function getPz_ValS(ByVal i As Integer) As String
    '----------------------------------------------------------------------------------------------------------------
    getPz_ValS = ""
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return edPz_Vals1.Text
        Case 2 : Return edPz_Vals2.Text
        Case 3 : Return edPz_Vals3.Text
        Case 4 : Return edPz_Vals4.Text
        Case 5 : Return edPz_Vals5.Text
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub setPz_Val(ByVal i As Integer)
    Try
      Select Case i
        Case 1
          edPz_Val1.NTSTextDB = NTSZOOM.strIn
        Case 2
          edPz_Val2.NTSTextDB = NTSZOOM.strIn
        Case 3
          edPz_Val3.NTSTextDB = NTSZOOM.strIn
        Case 4
          edPz_Val4.NTSTextDB = NTSZOOM.strIn
        Case 5
          edPz_Val5.NTSTextDB = NTSZOOM.strIn
        Case 6
          edPz_Val6.NTSTextDB = NTSZOOM.strIn
        Case 7
          edPz_Val7.NTSTextDB = NTSZOOM.strIn
        Case 8
          edPz_Val8.NTSTextDB = NTSZOOM.strIn
        Case 9
          edPz_Val9.NTSTextDB = NTSZOOM.strIn
        Case 10
          edPz_Val10.NTSTextDB = NTSZOOM.strIn
        Case 11
          edPz_Val11.NTSTextDB = NTSZOOM.strIn
        Case 12
          edPz_Val12.NTSTextDB = NTSZOOM.strIn
        Case 13
          edPz_Val13.NTSTextDB = NTSZOOM.strIn
        Case 14
          edPz_Val14.NTSTextDB = NTSZOOM.strIn
        Case 15
          edPz_Val15.NTSTextDB = NTSZOOM.strIn

      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub setPz_ValS(ByVal i As Integer)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : edPz_Vals1.NTSTextDB = NTSZOOM.strIn
        Case 2 : edPz_Vals2.NTSTextDB = NTSZOOM.strIn
        Case 3 : edPz_Vals3.NTSTextDB = NTSZOOM.strIn
        Case 4 : edPz_Vals4.NTSTextDB = NTSZOOM.strIn
        Case 5 : edPz_Vals5.NTSTextDB = NTSZOOM.strIn
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function getPz_Column(ByVal i As Integer) As String
    getPz_Column = ""
    Try
      Select Case i
        Case 1
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column1"))
        Case 2
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column2"))
        Case 3
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column3"))
        Case 4
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column4"))
        Case 5
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column5"))
        Case 6
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column6"))
        Case 7
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column7"))
        Case 8
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column8"))
        Case 9
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column9"))
        Case 10
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column10"))
        Case 11
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column11"))
        Case 12
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column12"))
        Case 13
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column13"))
        Case 14
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column14"))
        Case 15
          Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Column15"))

      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function getPz_ColumnS(ByVal i As Integer) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Columns" & i.ToString))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub posiziona(ByVal strCod As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To dcSpar.List.Count - 1
        If CType(dcSpar.Item(i), DataRowView)!Pz_Codform.ToString = strCod Then
          dcSpar.Position = i
          Exit For
        End If
      Next
      oCleSpar.nCurrentCodForm = NTSCInt(strCod)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub abilitaToolbar()
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If dcSpar.Position = -1 Then Return
      If dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql Is System.DBNull.Value Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql.ToString = "" Then
        tlbStampaVideo.Enabled = True
        tlbImpostaStampante.Enabled = True
        tlbStampaGriglia.Enabled = False
        tlbElabora.Enabled = False
      Else
        strSQL = dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql.ToString
        If Not (strSQL.ToUpper.Replace("(", " ").IndexOf(" INSERT ") > -1 Or _
                strSQL.ToUpper.Replace("(", " ").IndexOf(" UPDATE ") > -1 Or _
                strSQL.ToUpper.Replace("(", " ").IndexOf(" DELETE ") > -1 Or _
                strSQL.ToUpper.StartsWith("INSERT") Or _
                strSQL.ToUpper.StartsWith("UPDATE") Or _
                strSQL.ToUpper.StartsWith("DELETE")) Then
          'è una select
          tlbStampaVideo.Enabled = True
          tlbImpostaStampante.Enabled = True
          tlbStampaGriglia.Enabled = True
          tlbElabora.Enabled = False
        Else
          tlbStampaVideo.Enabled = False
          tlbImpostaStampante.Enabled = False
          tlbStampaGriglia.Enabled = False
          tlbElabora.Enabled = True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt).Trim <> "" Then
        GctlSetVisEnab(tlbStampaVideo, False)
        GctlSetVisEnab(tlbStampa, False)
      Else
        tlbStampaVideo.Enabled = False
        tlbStampa.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub resettaRiga()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If dcSpar.Position = -1 Then Return
      dsSpar.Tables("PARSTAG").Rows(dcSpar.Position).RejectChanges()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim i As Integer = 0
    Dim nRis As Integer = 0
    Dim k As Integer = 0
    Dim nIndiceCampo As Integer = 0
    Dim strCrpe As String = ""
    Dim strNomFile As String = ""
    Dim strTitle As String = ""
    Dim strMainTab As String = ""
    Dim strSelFormula As String = ""
    Dim strNuova As String = ""
    Dim strSelRecord As String = ""
    Dim nPjob As Object

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      tlbCambioDitta.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      If cbParstag.Text = "" Then Return
      If dsSpar.Tables("PARSTAG").Rows.Count = 0 Then Return
      If Not oCleSpar.ControllaCoerenzaWhere(edPz_strOprel.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSpar.ControllaValoreCampo(cbParstag.Text, _
          cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
          cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
          edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
          edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
          edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
          edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
          cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text, _
          edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
          edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpar.ControllaValoreCampoS(cbParstag.Text, _
        cbPz_Ops1.Text, cbPz_Ops2.Text, cbPz_Ops3.Text, cbPz_Ops4.Text, cbPz_Ops5.Text, _
        edPz_Descams1.Text, edPz_Descams2.Text, edPz_Descams3.Text, edPz_Descams4.Text, edPz_Descams5.Text, _
        edPz_Vals1.Text, edPz_Vals2.Text, edPz_Vals3.Text, edPz_Vals4.Text, edPz_Vals5.Text) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Esiste un report associato
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt) <> "") And _
         (Not (dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt Is System.DBNull.Value)) Then
        strNomFile = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt)
        strTitle = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Titstam)
        '------------------------------------------------------------------------------------------------------------
        '--- Se la versione di Crystal Report è inferiore alla 12 e c'è la presenza di un campo memo fra i filtri
        '--- indicati, avvisa e impedisce la stampa (errore 515 Error In Formula)
        '------------------------------------------------------------------------------------------------------------
        If NTSCDec(oMenu.CrpeVersion) = 16 Then
          For nIndiceCampo = 1 To 15
            If NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Tipo" & nIndiceCampo.ToString)) = 11 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 129271190749720718, "Attenzione!" & vbCrLf & _
                "Il campo '|" & nIndiceCampo.ToString & "|' è di tipo 'Memo'." & vbCrLf & _
                "La run-time di Crystal Report corrente non ne permette la stampa."))
              Return
            End If
          Next
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Preparo il motore di stampa
        '------------------------------------------------------------------------------------------------------------
        strCrpe = ""
        nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BS--SPAR", "Reports" & NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Codform).Trim, " ", 0, nDestin, strNomFile, False, strTitle, False)
        If nPjob Is Nothing Then Return
        '------------------------------------------------------------------------------------------------------------
        For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
          strMainTab = oMenu.PEGetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "MAINTAB")
          '----------------------------------------------------------------------------------------------------------
          '--- SetSlectionFormula (se serve)
          '----------------------------------------------------------------------------------------------------------
          If (edPz_Descam1.Text <> "") Or (edPz_Descam2.Text <> "") Or _
             (edPz_Descam3.Text <> "") Or (edPz_Descam4.Text <> "") Or _
             (edPz_Descam5.Text <> "") Or (edPz_Descam6.Text <> "") Or _
             (edPz_Descam7.Text <> "") Or (edPz_Descam8.Text <> "") Or _
             (edPz_Descam9.Text <> "") Or (edPz_Descam10.Text <> "") Or _
             (edPz_Descam11.Text <> "") Or (edPz_Descam12.Text <> "") Or _
             (edPz_Descam13.Text <> "") Or (edPz_Descam14.Text <> "") Or _
             (edPz_Descam15.Text <> "") Then
            With dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)
              strSelFormula = oCleSpar.CreateSelectionFormula(edPz_strOprel.Text, _
                edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
                edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
                edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text, _
                NTSCInt(!Pz_Tipo1), NTSCInt(!Pz_Tipo2), NTSCInt(!Pz_Tipo3), NTSCInt(!Pz_Tipo4), NTSCInt(!Pz_Tipo5), _
                NTSCInt(!Pz_Tipo6), NTSCInt(!Pz_Tipo7), NTSCInt(!Pz_Tipo8), NTSCInt(!Pz_Tipo9), NTSCInt(!Pz_Tipo10), _
                NTSCInt(!Pz_Tipo11), NTSCInt(!Pz_Tipo12), NTSCInt(!Pz_Tipo13), NTSCInt(!Pz_Tipo14), NTSCInt(!Pz_Tipo15), _
                NTSCStr(!Pz_Column1), NTSCStr(!Pz_Column2), NTSCStr(!Pz_Column3), NTSCStr(!Pz_Column4), NTSCStr(!Pz_Column5), _
                NTSCStr(!Pz_Column6), NTSCStr(!Pz_Column7), NTSCStr(!Pz_Column8), NTSCStr(!Pz_Column9), NTSCStr(!Pz_Column10), _
                NTSCStr(!Pz_Column11), NTSCStr(!Pz_Column12), NTSCStr(!Pz_Column13), NTSCStr(!Pz_Column14), NTSCStr(!Pz_Column15), _
                cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
                cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
                cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text)
            End With

            strSelRecord = oMenu.PEGetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)))

            If strSelRecord <> "" Then
              strNuova = "( " & strSelRecord & vbCrLf & " ) AND ( "
            Else
              strNuova = "( " & strSelRecord & vbCrLf
            End If

            If strSelFormula <> "" Then
              strNuova = strNuova & strSelFormula & " )"
            End If
            '--------------------------------------------------------------------------------------------------------
            '--- Controlla se esiste la MAINTAB
            '--------------------------------------------------------------------------------------------------------
            If strMainTab <> "" Then
              strNuova = strNuova & " And ({" & strMainTab & ".codditt} = '" & DittaCorrente & "')"
            End If
            '--------------------------------------------------------------------------------------------------------
            '--- Controlla il nome del file RPT
            '--------------------------------------------------------------------------------------------------------
            If UCase(strNomFile) = "PAR0010.RPT" Then
              strNuova = strNuova & " And (IsNull({anagra.codditt}) Or {anagra.codditt} = '" & DittaCorrente & "')"
            End If
            '--------------------------------------------------------------------------------------------------------
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strNuova)
          Else
            '--------------------------------------------------------------------------------------------------------
            '--- Controlla se esiste la MAINTAB
            '--------------------------------------------------------------------------------------------------------
            If strMainTab <> "" Then
              strNuova = "({" & strMainTab & ".codditt} = '" & DittaCorrente & "')"
              '------------------------------------------------------------------------------------------------------
              '--- Controlla il nome del file RPT
              '------------------------------------------------------------------------------------------------------
              If UCase(strNomFile) = "PAR0010.RPT" Then
                strNuova = strNuova & " And (IsNull({anagra.codditt}) Or {anagra.codditt} = '" & DittaCorrente & "')"
              End If
              '------------------------------------------------------------------------------------------------------
              strSelRecord = oMenu.PEGetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
              If strSelRecord <> "" Then
                strNuova = strNuova & " AND ( " & strSelRecord & " ) "
              End If
              '------------------------------------------------------------------------------------------------------
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strNuova)
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Passa le formule preimpostate
          '----------------------------------------------------------------------------------------------------------
          For k = 0 To 14
            If getPz_Val(k + 1) <> "" Then
              oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTRO" & Trim(NTSCStr(k + 1)), ConvStrRpt(getPz_Val(k + 1)))
            End If
          Next
          oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FORMSTAMPA", ConvStrRpt(Trim(oCleSpar.PrimaParola(cbParstag.Text))))
          oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NOMESTAMPA", ConvStrRpt(strTitle))
          oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILESTAMPA", ConvStrRpt(strNomFile))
          '--- Lancia il JOB
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Next
      End If

      If bAnagen = True Then tlbCambioDitta.Enabled = True

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub StampaGriglia()
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strOprel As String = ""
    Dim strOprel2 As String = ""
    Dim strOp(16) As String
    Dim strOps(6) As String
    Dim nRec As Integer = 0
    Dim oParamGrsp As New CLE__CLDP
    Dim frmGrsp As FRM__GRSP = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      tlbCambioDitta.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      If cbParstag.Text = "" Then Return
      If dsSpar.Tables("PARSTAG").Rows.Count = 0 Then Return
      If Not oCleSpar.ControllaCoerenzaWhere(edPz_strOprel.Text) Then Return
      If Not oCleSpar.ControllaCoerenzaWhere(edPz_strOprel2.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If (Not ((NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt) = "" Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt Is System.DBNull.Value)) And _
         (NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql) = "" Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql Is System.DBNull.Value)) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSpar.ControllaValoreCampo(cbParstag.Text, _
          cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
          cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
          edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
          edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
          edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
          edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
          cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text, _
          edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
          edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text) Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esiste, tra i filtri, è stato indicato un campo di 'Memo',
      '--- l'operatore non deve essere diverso da 'Like', 'IS NULL', 'IS NOT NULL'
      '--------------------------------------------------------------------------------------------------------------
      strOp(1) = cbPz_Op1.SelectedValue : strOp(2) = cbPz_Op2.SelectedValue : strOp(3) = cbPz_Op3.SelectedValue
      strOp(4) = cbPz_Op4.SelectedValue : strOp(5) = cbPz_Op5.SelectedValue : strOp(6) = cbPz_Op6.SelectedValue
      strOp(7) = cbPz_Op7.SelectedValue : strOp(8) = cbPz_Op8.SelectedValue : strOp(9) = cbPz_Op9.SelectedValue
      strOp(10) = cbPz_Op10.SelectedValue : strOp(11) = cbPz_Op11.SelectedValue : strOp(12) = cbPz_Op12.SelectedValue
      strOp(13) = cbPz_Op13.SelectedValue : strOp(14) = cbPz_Op14.SelectedValue : strOp(15) = cbPz_Op15.SelectedValue
      For i = 1 To 15
        If NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Tipo" & i.ToString)) = 11 Then
          Select Case strOp(i).ToUpper
            Case "LIKE", "IS NULL", "IS NOT NULL"
            Case Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 129985043802132868, "Attenzione!" & vbCrLf & _
                "Il campo '|" & i.ToString & "|' è di tipo 'Memo'." & vbCrLf & _
                "L'operatore selezionato può SOLO essere:" & vbCrLf & _
                " . Like" & vbCrLf & _
                " . IS NULL" & vbCrLf & _
                " . IS NOT NULL"))
              Return
          End Select
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpar.ControllaValoreCampoS(cbParstag.Text, _
        cbPz_Ops1.Text, cbPz_Ops2.Text, cbPz_Ops3.Text, cbPz_Ops4.Text, cbPz_Ops5.Text, _
        edPz_Descams1.Text, edPz_Descams2.Text, edPz_Descams3.Text, edPz_Descams4.Text, edPz_Descams5.Text, _
        edPz_Vals1.Text, edPz_Vals2.Text, edPz_Vals3.Text, edPz_Vals4.Text, edPz_Vals5.Text) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      strOps(1) = cbPz_Ops1.SelectedValue : strOps(2) = cbPz_Ops2.SelectedValue : strOps(3) = cbPz_Ops3.SelectedValue
      strOps(4) = cbPz_Ops4.SelectedValue : strOps(5) = cbPz_Ops5.SelectedValue
      For i = 1 To 5
        If NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)("Pz_Tipos" & i.ToString)) = 11 Then
          Select Case strOp(i).ToUpper
            Case "LIKE", "IS NULL", "IS NOT NULL"
            Case Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 130450451308436853, "Attenzione!" & vbCrLf & _
                "Il campo '|" & i.ToString & "|' è di tipo 'Memo'." & vbCrLf & _
                "L'operatore selezionato può SOLO essere:" & vbCrLf & _
                " . Like" & vbCrLf & _
                " . IS NULL" & vbCrLf & _
                " . IS NOT NULL"))
              Return
          End Select
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt) = "" Or _
         dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt Is System.DBNull.Value Then
        strSQL = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql)
        If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel Is System.DBNull.Value Then
          strOprel = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel)
        Else
          strOprel = ""
        End If
        If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2 Is System.DBNull.Value Then
          strOprel2 = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2)
        Else
          strOprel2 = ""
        End If
      Else
        If NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql) = "" Or _
           dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql Is System.DBNull.Value Then
          Return
        Else
          strSQL = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql)
          If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel Is System.DBNull.Value Then
            strOprel = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel)
          Else
            strOprel = ""
          End If
          If Not dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2 Is System.DBNull.Value Then
            strOprel2 = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_strOprel2)
          Else
            strOprel2 = ""
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not (strSQL.ToUpper.Replace("(", " ").IndexOf(" INSERT ") > -1 Or _
              strSQL.ToUpper.Replace("(", " ").IndexOf(" UPDATE ") > -1 Or _
              strSQL.ToUpper.Replace("(", " ").IndexOf(" DELETE ") > -1 Or _
              strSQL.ToUpper.StartsWith("INSERT") Or _
              strSQL.ToUpper.StartsWith("UPDATE") Or _
              strSQL.ToUpper.StartsWith("DELETE")) Then
        '------------------------------------------------------------------------------------------------------------
        '--- E' una SELECT
        '------------------------------------------------------------------------------------------------------------
        oCleSpar.strGrspSQL = oCleSpar.CreateQuerySql(strSQL, strOprel, strOprel2, _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_maintab), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Nomrpt), _
          NTSCInt(cbPz_Op1.SelectedIndex), NTSCInt(cbPz_Op2.SelectedIndex), NTSCInt(cbPz_Op3.SelectedIndex), _
          NTSCInt(cbPz_Op4.SelectedIndex), NTSCInt(cbPz_Op5.SelectedIndex), NTSCInt(cbPz_Op6.SelectedIndex), _
          NTSCInt(cbPz_Op7.SelectedIndex), NTSCInt(cbPz_Op8.SelectedIndex), NTSCInt(cbPz_Op9.SelectedIndex), _
          NTSCInt(cbPz_Op10.SelectedIndex), _
          edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, edPz_Descam4.Text, edPz_Descam5.Text, _
          edPz_Descam6.Text, edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, edPz_Descam10.Text, _
          edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, edPz_Val4.Text, edPz_Val5.Text, _
          edPz_Val6.Text, edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, edPz_Val10.Text, _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo1), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo2), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo3), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo4), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo5), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo6), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo7), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo8), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo9), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo10), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column1), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column2), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column3), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column4), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column5), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column6), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column7), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column8), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column9), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column10), _
          cbPz_Op1.Text, cbPz_Op2.Text, cbPz_Op3.Text, cbPz_Op4.Text, cbPz_Op5.Text, _
          cbPz_Op6.Text, cbPz_Op7.Text, cbPz_Op8.Text, cbPz_Op9.Text, cbPz_Op10.Text, _
          NTSCInt(cbPz_Op11.SelectedIndex), NTSCInt(cbPz_Op12.SelectedIndex), NTSCInt(cbPz_Op13.SelectedIndex), _
          NTSCInt(cbPz_Op14.SelectedIndex), NTSCInt(cbPz_Op15.SelectedIndex), _
          edPz_Descam11.Text, edPz_Descam12.Text, edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
          edPz_Val11.Text, edPz_Val12.Text, edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text, _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo11), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo12), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo13), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo14), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipo15), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column11), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column12), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column13), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column14), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Column15), _
          cbPz_Op11.Text, cbPz_Op12.Text, cbPz_Op13.Text, cbPz_Op14.Text, cbPz_Op15.Text, _
          NTSCInt(cbPz_Ops1.SelectedIndex), NTSCInt(cbPz_Ops2.SelectedIndex), NTSCInt(cbPz_Ops3.SelectedIndex), NTSCInt(cbPz_Ops4.SelectedIndex), NTSCInt(cbPz_Ops5.SelectedIndex), _
          edPz_Descams1.Text, edPz_Descams2.Text, edPz_Descams3.Text, edPz_Descams4.Text, edPz_Descams5.Text, _
          edPz_Vals1.Text, edPz_Vals2.Text, edPz_Vals3.Text, edPz_Vals4.Text, edPz_Vals5.Text, _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos1), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos2), _
          NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos3), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos4), NTSCInt(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Tipos5), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns1), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns2), _
          NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns3), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns4), NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Columns5), _
          cbPz_Ops1.Text, cbPz_Ops2.Text, cbPz_Ops3.Text, cbPz_Ops4.Text, cbPz_Ops5.Text)
        oCleSpar.strGrspTitle = NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Titstam)
        frmGrsp = CType(NTSNewFormModal("FRM__GRSP"), FRM__GRSP)
        frmGrsp.Init(oMenu, oParamGrsp, DittaCorrente)
        frmGrsp.InitEntity(oCleSpar)
        frmGrsp.ShowDialog()
      Else
        Me.Cursor = Cursors.WaitCursor
        If oCleSpar.Esegui(NTSCStr(dsSpar.Tables("PARSTAG").Rows(dcSpar.Position)!Pz_Sqlsql), nRec) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128735677023177285, "Operazione Terminata.  Record Modificati ") & nRec.ToString)
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128326974668424279, "Errore Istruzione SQL NON eseguita. Record Modificati: 0"))
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bAnagen = True Then tlbCambioDitta.Enabled = True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmGrsp Is Nothing Then frmGrsp.Dispose()
      frmGrsp = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub Valori_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles edPz_Val1.Validating
    Dim strNome As String = ""
    Dim strValue As String = ""
    Dim strDateSeparator As String = ""
    Dim nIndiceControllo As Integer = 0
    Try
      strNome = CType(sender, NTSTextBoxStr).Name
      strValue = CType(sender, NTSTextBoxStr).Text
      strDateSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator
      If IsNumeric(strNome.Substring(strNome.Length - 2)) Then
        nIndiceControllo = NTSCInt(strNome.Substring(strNome.Length - 2))
      Else
        nIndiceControllo = NTSCInt(strNome.Substring(strNome.Length - 1))
      End If
      If NTSCInt(CType(dcSpar.Current, DataRowView).Row("pz_tipo" & nIndiceControllo.ToString)) = 8 Then
        'se ho inserito una data senza separatori, la formatto correttamente
        If strValue.Length = 4 And IsNumeric(strValue) Then strValue += DateTime.Now.Year.ToString
        If strValue.Length = 8 Then
          If strValue.IndexOf(strDateSeparator) < 0 Then
            strValue = strValue.Substring(0, 2) + strDateSeparator + strValue.Substring(2, 2) + strDateSeparator + strValue.Substring(4, 4)
            CType(sender, NTSTextBoxStr).NTSTextDB = strValue
          End If
        End If
        If strValue.Length = 6 Then
          strValue = strValue.Substring(0, 2) + strDateSeparator + strValue.Substring(2, 2) + strDateSeparator + strValue.Substring(4, 2)
          CType(sender, NTSTextBoxStr).NTSTextDB = strValue
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdZoomStampe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZoomStampe.Click
    Dim oParam As New CLE__PATB
    Try

      oParam.strCodPdc = oCleSpar.strElencoCodici

      NTSZOOM.ZoomStrIn("ZOOMPARSTAG", DittaCorrente, oParam)

      If NTSZOOM.strIn <> "" Then
        cbParstag.SelectedValue = NTSZOOM.strIn
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
