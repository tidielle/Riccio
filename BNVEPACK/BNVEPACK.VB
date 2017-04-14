Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEPACK

#Region "Dichiarazione variabili"
  Public oClePack As CLEVEPACK
  Public dsPack As DataSet
  Public dcMovPac As BindingSource = New BindingSource
  Public dcTestPac As BindingSource = New BindingSource
  Public dttMovPac, dttMovPacTmp As New DataTable
  Public dttUm As New DataTable
  '----- Reg. Business -------------------
  Public bNoModifQuantSuColli As Boolean
  Public bOrdMatrMultiRow As Boolean
  Public lIncremContatoreRiga As Decimal
  Public lOrdinamentoNuoveRighe As Integer
  Public strPrefissoNumeroPacco As String
  '---------------------------------------
  Public dtUltAgg As Date = NTSCDate(IntSetDate("01/01/1900"))
  Public lNumPack As Integer = 0
  Public lAnnoPack As Integer = 0
  Public strSeriePack As String = ""
  Public dtDataPack As Date
  Public lCodling As Integer
  Public bNuovo As Boolean = False
  Public bChiudiAlSalvataggio As Boolean = False
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents tsPack As NTSInformatica.NTSTabControl
  Public WithEvents tsTestata As NTSInformatica.NTSTabPage
  Public WithEvents tsNote As NTSInformatica.NTSTabPage
  Public WithEvents pnTestata As NTSInformatica.NTSPanel
  Public WithEvents pnNote As NTSInformatica.NTSPanel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnPack As NTSInformatica.NTSPanel
  Public WithEvents edTp_note As NTSInformatica.NTSMemoBox
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecAggiorna As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDividi As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImposta As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGenera As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPDF As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbTitolo As NTSInformatica.NTSLabel
  Public WithEvents tsCorpo As NTSInformatica.NTSTabPage
  Public WithEvents tsPiede As NTSInformatica.NTSTabPage
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStp As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSeleziona As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDeseleziona As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDaDocu As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDaOrdine As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDaPacking As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDaNiente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRileva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrnEti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrnAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrnscreenEti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrnscreenAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnPiede As NTSInformatica.NTSPanel
  Public WithEvents pnCorpo As NTSInformatica.NTSPanel
  Public WithEvents edTp_riferim As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTp_conto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_datdoc As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDescrizione As NTSInformatica.NTSLabel
  Public WithEvents lbRiferimenti As NTSInformatica.NTSLabel
  Public WithEvents lbClifor As NTSInformatica.NTSLabel
  Public WithEvents lbDatdoc As NTSInformatica.NTSLabel
  Public WithEvents fmDadocu As NTSInformatica.NTSGroupBox
  Public WithEvents edTp_mmnumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_mmserie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTp_mmtipork As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTp_mmanno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbBarra As NTSInformatica.NTSLabel
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents lbDesctipo As NTSInformatica.NTSLabel
  Public WithEvents fmAccompagnatori As NTSInformatica.NTSGroupBox
  Public WithEvents edTp_coddest2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_coddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_codvett2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_codvett As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDest2 As NTSInformatica.NTSLabel
  Public WithEvents lbDest1 As NTSInformatica.NTSLabel
  Public WithEvents lbCodVett2 As NTSInformatica.NTSLabel
  Public WithEvents lbCodVet As NTSInformatica.NTSLabel
  Public WithEvents lbDescdest2 As NTSInformatica.NTSLabel
  Public WithEvents lbDescdest1 As NTSInformatica.NTSLabel
  Public WithEvents lbDescvett2 As NTSInformatica.NTSLabel
  Public WithEvents lbDescVet As NTSInformatica.NTSLabel
  Public WithEvents fmTotali As NTSInformatica.NTSGroupBox
  Public WithEvents lbVolume As NTSInformatica.NTSLabel
  Public WithEvents lbNetto As NTSInformatica.NTSLabel
  Public WithEvents lbLordo As NTSInformatica.NTSLabel
  Public WithEvents lbColli As NTSInformatica.NTSLabel
  Public WithEvents ckTp_sblocca As NTSInformatica.NTSCheckBox
  Public WithEvents edTp_totvolume As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_peso As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_pesonetto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTp_totcoll As NTSInformatica.NTSTextBoxNum
  Public WithEvents grPack As NTSInformatica.NTSGrid
  Public WithEvents grvPack As NTSInformatica.NTSGridView
  Public WithEvents xx_selez As NTSInformatica.NTSGridColumn
  Public WithEvents mp_riga As NTSInformatica.NTSGridColumn
  Public WithEvents mp_numpack As NTSInformatica.NTSGridColumn
  Public WithEvents mp_despack As NTSInformatica.NTSGridColumn
  Public WithEvents mp_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mp_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mp_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents mp_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mp_ump As NTSInformatica.NTSGridColumn
  Public WithEvents mp_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mp_matric As NTSInformatica.NTSGridColumn
  Public WithEvents mp_peso As NTSInformatica.NTSGridColumn
  Public WithEvents mp_pesonetto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents mp_codvuo As NTSInformatica.NTSGridColumn
  Public WithEvents mp_note As NTSInformatica.NTSGridColumn
  Public WithEvents mp_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mp_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mp_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mp_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents mp_mmtipork As NTSInformatica.NTSGridColumn
  Public WithEvents mp_mmanno As NTSInformatica.NTSGridColumn
  Public WithEvents mp_mmserie As NTSInformatica.NTSGridColumn
  Public WithEvents mp_mmnumdoc As NTSInformatica.NTSGridColumn
  Public WithEvents mp_mmriga As NTSInformatica.NTSGridColumn
  Public WithEvents mp_anno As NTSInformatica.NTSGridColumn
  Public WithEvents mp_serie As NTSInformatica.NTSGridColumn
  Public WithEvents mp_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents mp_volume As NTSInformatica.NTSGridColumn
  Public WithEvents mp_desint As NTSInformatica.NTSGridColumn
  Public WithEvents mp_code As NTSInformatica.NTSGridColumn
  Public WithEvents mp_datscad As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRilevaMisure As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbMisurea1 As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRilevaDaDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNonRilevare As NTSInformatica.NTSBarButtonItem
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModVE
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Public WithEvents tlbAggiungiRighe As NTSInformatica.NTSBarMenuItem
  Public WithEvents mp_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents mp_indtagl As NTSInformatica.NTSGridColumn
  Public WithEvents mp_idpallet As NTSInformatica.NTSGridColumn
  Public WithEvents pnTotali As NTSInformatica.NTSPanel
  Public WithEvents pnControlliPiede As NTSInformatica.NTSPanel
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEPACK", "BEVEPACK", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePack = CType(oTmp, CLEVEPACK)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEPACK", strRemoteServer, strRemotePort)
    AddHandler oClePack.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePack.Init(oApp, NTSScript, oMenu.oCleComm, "TESTPAC", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__PAGA che eredita da BE__PAGA
    'CallByName(oCleProv, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------
    Return True
  End Function
  Public Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEPACK))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecAggiorna = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStp = New NTSInformatica.NTSBarButtonItem
    Me.tlbSeleziona = New NTSInformatica.NTSBarButtonItem
    Me.tlbDeseleziona = New NTSInformatica.NTSBarButtonItem
    Me.tlbDaDocu = New NTSInformatica.NTSBarButtonItem
    Me.tlbDaOrdine = New NTSInformatica.NTSBarButtonItem
    Me.tlbDaPacking = New NTSInformatica.NTSBarButtonItem
    Me.tlbDaNiente = New NTSInformatica.NTSBarButtonItem
    Me.tlbRilevaMisure = New NTSInformatica.NTSBarSubItem
    Me.tlbRileva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRilevaDaDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbNonRilevare = New NTSInformatica.NTSBarButtonItem
    Me.tlbMisurea1 = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrnEti = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrnAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrnscreenEti = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrnscreenAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiRighe = New NTSInformatica.NTSBarMenuItem
    Me.tlbDividi = New NTSInformatica.NTSBarButtonItem
    Me.tlbImposta = New NTSInformatica.NTSBarButtonItem
    Me.tlbGenera = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaPDF = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tsPack = New NTSInformatica.NTSTabControl
    Me.tsTestata = New NTSInformatica.NTSTabPage
    Me.pnTestata = New NTSInformatica.NTSPanel
    Me.fmDadocu = New NTSInformatica.NTSGroupBox
    Me.lbDesctipo = New NTSInformatica.NTSLabel
    Me.edTp_mmnumdoc = New NTSInformatica.NTSTextBoxNum
    Me.edTp_mmserie = New NTSInformatica.NTSTextBoxStr
    Me.edTp_mmtipork = New NTSInformatica.NTSTextBoxStr
    Me.edTp_mmanno = New NTSInformatica.NTSTextBoxNum
    Me.lbBarra = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edTp_riferim = New NTSInformatica.NTSTextBoxStr
    Me.edTp_conto = New NTSInformatica.NTSTextBoxNum
    Me.edTp_datdoc = New NTSInformatica.NTSTextBoxData
    Me.lbDescrizione = New NTSInformatica.NTSLabel
    Me.lbRiferimenti = New NTSInformatica.NTSLabel
    Me.lbClifor = New NTSInformatica.NTSLabel
    Me.lbDatdoc = New NTSInformatica.NTSLabel
    Me.tsCorpo = New NTSInformatica.NTSTabPage
    Me.pnCorpo = New NTSInformatica.NTSPanel
    Me.grPack = New NTSInformatica.NTSGrid
    Me.grvPack = New NTSInformatica.NTSGridView
    Me.xx_selez = New NTSInformatica.NTSGridColumn
    Me.mp_riga = New NTSInformatica.NTSGridColumn
    Me.mp_numpack = New NTSInformatica.NTSGridColumn
    Me.mp_despack = New NTSInformatica.NTSGridColumn
    Me.mp_codart = New NTSInformatica.NTSGridColumn
    Me.mp_descr = New NTSInformatica.NTSGridColumn
    Me.mp_unmis = New NTSInformatica.NTSGridColumn
    Me.mp_colli = New NTSInformatica.NTSGridColumn
    Me.mp_ump = New NTSInformatica.NTSGridColumn
    Me.mp_quant = New NTSInformatica.NTSGridColumn
    Me.mp_matric = New NTSInformatica.NTSGridColumn
    Me.mp_peso = New NTSInformatica.NTSGridColumn
    Me.mp_pesonetto = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.mp_codvuo = New NTSInformatica.NTSGridColumn
    Me.mp_note = New NTSInformatica.NTSGridColumn
    Me.mp_misura1 = New NTSInformatica.NTSGridColumn
    Me.mp_misura2 = New NTSInformatica.NTSGridColumn
    Me.mp_misura3 = New NTSInformatica.NTSGridColumn
    Me.mp_ultagg = New NTSInformatica.NTSGridColumn
    Me.mp_mmtipork = New NTSInformatica.NTSGridColumn
    Me.mp_mmanno = New NTSInformatica.NTSGridColumn
    Me.mp_mmserie = New NTSInformatica.NTSGridColumn
    Me.mp_mmnumdoc = New NTSInformatica.NTSGridColumn
    Me.mp_mmriga = New NTSInformatica.NTSGridColumn
    Me.mp_volume = New NTSInformatica.NTSGridColumn
    Me.mp_desint = New NTSInformatica.NTSGridColumn
    Me.mp_code = New NTSInformatica.NTSGridColumn
    Me.mp_datscad = New NTSInformatica.NTSGridColumn
    Me.mp_anno = New NTSInformatica.NTSGridColumn
    Me.mp_serie = New NTSInformatica.NTSGridColumn
    Me.mp_numdoc = New NTSInformatica.NTSGridColumn
    Me.mp_tctaglia = New NTSInformatica.NTSGridColumn
    Me.mp_indtagl = New NTSInformatica.NTSGridColumn
    Me.mp_idpallet = New NTSInformatica.NTSGridColumn
    Me.tsPiede = New NTSInformatica.NTSTabPage
    Me.pnPiede = New NTSInformatica.NTSPanel
    Me.fmAccompagnatori = New NTSInformatica.NTSGroupBox
    Me.pnControlliPiede = New NTSInformatica.NTSPanel
    Me.lbCodVet = New NTSInformatica.NTSLabel
    Me.lbCodVett2 = New NTSInformatica.NTSLabel
    Me.lbDescdest2 = New NTSInformatica.NTSLabel
    Me.lbDest1 = New NTSInformatica.NTSLabel
    Me.lbDescdest1 = New NTSInformatica.NTSLabel
    Me.lbDest2 = New NTSInformatica.NTSLabel
    Me.lbDescvett2 = New NTSInformatica.NTSLabel
    Me.edTp_codvett = New NTSInformatica.NTSTextBoxNum
    Me.lbDescVet = New NTSInformatica.NTSLabel
    Me.edTp_codvett2 = New NTSInformatica.NTSTextBoxNum
    Me.edTp_coddest2 = New NTSInformatica.NTSTextBoxNum
    Me.edTp_coddest = New NTSInformatica.NTSTextBoxNum
    Me.pnTotali = New NTSInformatica.NTSPanel
    Me.fmTotali = New NTSInformatica.NTSGroupBox
    Me.ckTp_sblocca = New NTSInformatica.NTSCheckBox
    Me.edTp_totvolume = New NTSInformatica.NTSTextBoxNum
    Me.edTp_peso = New NTSInformatica.NTSTextBoxNum
    Me.edTp_pesonetto = New NTSInformatica.NTSTextBoxNum
    Me.edTp_totcoll = New NTSInformatica.NTSTextBoxNum
    Me.lbVolume = New NTSInformatica.NTSLabel
    Me.lbNetto = New NTSInformatica.NTSLabel
    Me.lbLordo = New NTSInformatica.NTSLabel
    Me.lbColli = New NTSInformatica.NTSLabel
    Me.tsNote = New NTSInformatica.NTSTabPage
    Me.pnNote = New NTSInformatica.NTSPanel
    Me.edTp_note = New NTSInformatica.NTSMemoBox
    Me.pnPack = New NTSInformatica.NTSPanel
    Me.lbTitolo = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsPack, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsPack.SuspendLayout()
    Me.tsTestata.SuspendLayout()
    CType(Me.pnTestata, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestata.SuspendLayout()
    CType(Me.fmDadocu, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDadocu.SuspendLayout()
    CType(Me.edTp_mmnumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_mmserie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_mmtipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_mmanno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_riferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_conto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_datdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsCorpo.SuspendLayout()
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCorpo.SuspendLayout()
    CType(Me.grPack, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvPack, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsPiede.SuspendLayout()
    CType(Me.pnPiede, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPiede.SuspendLayout()
    CType(Me.fmAccompagnatori, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAccompagnatori.SuspendLayout()
    CType(Me.pnControlliPiede, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlliPiede.SuspendLayout()
    CType(Me.edTp_codvett.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_codvett2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_coddest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTotali, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTotali.SuspendLayout()
    CType(Me.fmTotali, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTotali.SuspendLayout()
    CType(Me.ckTp_sblocca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_totvolume.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_peso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_pesonetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTp_totcoll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsNote.SuspendLayout()
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNote.SuspendLayout()
    CType(Me.edTp_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPack, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPack.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbGuida, Me.tlbEsci, Me.tlbApri, Me.tlbZoom, Me.tlbRecNuovo, Me.tlbRecAggiorna, Me.tlbRecCancella, Me.tlbRecRipristina, Me.tlbDividi, Me.tlbImposta, Me.tlbGenera, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaPDF, Me.tlbStrumenti, Me.tlbImpostaStp, Me.tlbSeleziona, Me.tlbDeseleziona, Me.tlbDaDocu, Me.tlbDaOrdine, Me.tlbDaPacking, Me.tlbDaNiente, Me.tlbRileva, Me.tlbPrnEti, Me.tlbPrnAll, Me.tlbPrnscreenEti, Me.tlbPrnscreenAll, Me.tlbDuplica, Me.tlbRilevaMisure, Me.tlbRilevaDaDoc, Me.tlbNonRilevare, Me.tlbMisurea1, Me.tlbAggiungiRighe})
    Me.NtsBarManager1.MaxItemId = 47
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecAggiorna), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDividi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImposta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGenera), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPDF), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 13
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 14
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRecNuovo
    '
    Me.tlbRecNuovo.Caption = "Nuova riga"
    Me.tlbRecNuovo.Glyph = CType(resources.GetObject("tlbRecNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecNuovo.GlyphPath = ""
    Me.tlbRecNuovo.Id = 16
    Me.tlbRecNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecNuovo.Name = "tlbRecNuovo"
    Me.tlbRecNuovo.Visible = True
    '
    'tlbRecAggiorna
    '
    Me.tlbRecAggiorna.Caption = "Salva riga"
    Me.tlbRecAggiorna.Glyph = CType(resources.GetObject("tlbRecAggiorna.Glyph"), System.Drawing.Image)
    Me.tlbRecAggiorna.GlyphPath = ""
    Me.tlbRecAggiorna.Id = 17
    Me.tlbRecAggiorna.Name = "tlbRecAggiorna"
    Me.tlbRecAggiorna.Visible = True
    '
    'tlbRecRipristina
    '
    Me.tlbRecRipristina.Caption = "Ripristina riga"
    Me.tlbRecRipristina.Glyph = CType(resources.GetObject("tlbRecRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecRipristina.GlyphPath = ""
    Me.tlbRecRipristina.Id = 19
    Me.tlbRecRipristina.Name = "tlbRecRipristina"
    Me.tlbRecRipristina.Visible = True
    '
    'tlbRecCancella
    '
    Me.tlbRecCancella.Caption = "Cancella riga"
    Me.tlbRecCancella.Glyph = CType(resources.GetObject("tlbRecCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecCancella.GlyphPath = ""
    Me.tlbRecCancella.Id = 18
    Me.tlbRecCancella.Name = "tlbRecCancella"
    Me.tlbRecCancella.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 30
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeseleziona), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaDocu, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaOrdine), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaPacking), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaNiente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRilevaMisure, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrnEti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrnAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrnscreenEti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrnscreenAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiRighe, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStp
    '
    Me.tlbImpostaStp.Caption = "Imposta stampante"
    Me.tlbImpostaStp.GlyphPath = ""
    Me.tlbImpostaStp.Id = 31
    Me.tlbImpostaStp.Name = "tlbImpostaStp"
    Me.tlbImpostaStp.Visible = True
    '
    'tlbSeleziona
    '
    Me.tlbSeleziona.Caption = "Seleziona tutto"
    Me.tlbSeleziona.GlyphPath = ""
    Me.tlbSeleziona.Id = 32
    Me.tlbSeleziona.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H))
    Me.tlbSeleziona.Name = "tlbSeleziona"
    Me.tlbSeleziona.Visible = True
    '
    'tlbDeseleziona
    '
    Me.tlbDeseleziona.Caption = "Deseleziona tutto"
    Me.tlbDeseleziona.GlyphPath = ""
    Me.tlbDeseleziona.Id = 33
    Me.tlbDeseleziona.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbDeseleziona.Name = "tlbDeseleziona"
    Me.tlbDeseleziona.Visible = True
    '
    'tlbDaDocu
    '
    Me.tlbDaDocu.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbDaDocu.Caption = "Preleva da documento"
    Me.tlbDaDocu.GlyphPath = ""
    Me.tlbDaDocu.Id = 34
    Me.tlbDaDocu.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDaDocu.Name = "tlbDaDocu"
    Me.tlbDaDocu.Visible = True
    '
    'tlbDaOrdine
    '
    Me.tlbDaOrdine.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbDaOrdine.Caption = "Preleva da ordine"
    Me.tlbDaOrdine.GlyphPath = ""
    Me.tlbDaOrdine.Id = 35
    Me.tlbDaOrdine.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbDaOrdine.Name = "tlbDaOrdine"
    Me.tlbDaOrdine.Visible = True
    '
    'tlbDaPacking
    '
    Me.tlbDaPacking.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbDaPacking.Caption = "Preleva da altra Packing List"
    Me.tlbDaPacking.GlyphPath = ""
    Me.tlbDaPacking.Id = 36
    Me.tlbDaPacking.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbDaPacking.Name = "tlbDaPacking"
    Me.tlbDaPacking.Visible = True
    '
    'tlbDaNiente
    '
    Me.tlbDaNiente.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbDaNiente.Caption = "Non prelevare"
    Me.tlbDaNiente.GlyphPath = ""
    Me.tlbDaNiente.Id = 37
    Me.tlbDaNiente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbDaNiente.Name = "tlbDaNiente"
    Me.tlbDaNiente.Visible = True
    '
    'tlbRilevaMisure
    '
    Me.tlbRilevaMisure.Caption = "Rileva misure/volume"
    Me.tlbRilevaMisure.GlyphPath = ""
    Me.tlbRilevaMisure.Id = 45
    Me.tlbRilevaMisure.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRileva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRilevaDaDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNonRilevare), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMisurea1)})
    Me.tlbRilevaMisure.Name = "tlbRilevaMisure"
    Me.tlbRilevaMisure.Visible = True
    '
    'tlbRileva
    '
    Me.tlbRileva.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbRileva.Caption = "da articolo"
    Me.tlbRileva.GlyphPath = ""
    Me.tlbRileva.Id = 39
    Me.tlbRileva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
    Me.tlbRileva.Name = "tlbRileva"
    Me.tlbRileva.Visible = True
    '
    'tlbRilevaDaDoc
    '
    Me.tlbRilevaDaDoc.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbRilevaDaDoc.Caption = "da movimenti"
    Me.tlbRilevaDaDoc.GlyphPath = ""
    Me.tlbRilevaDaDoc.Id = 46
    Me.tlbRilevaDaDoc.Name = "tlbRilevaDaDoc"
    Me.tlbRilevaDaDoc.Visible = True
    '
    'tlbNonRilevare
    '
    Me.tlbNonRilevare.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbNonRilevare.Caption = "non rilevare"
    Me.tlbNonRilevare.GlyphPath = ""
    Me.tlbNonRilevare.Id = 47
    Me.tlbNonRilevare.Name = "tlbNonRilevare"
    Me.tlbNonRilevare.Visible = True
    '
    'tlbMisurea1
    '
    Me.tlbMisurea1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbMisurea1.Caption = "misure a 1, volume a 0"
    Me.tlbMisurea1.GlyphPath = ""
    Me.tlbMisurea1.Id = 48
    Me.tlbMisurea1.Name = "tlbMisurea1"
    Me.tlbMisurea1.Visible = True
    '
    'tlbPrnEti
    '
    Me.tlbPrnEti.Caption = "Stampa etichette"
    Me.tlbPrnEti.GlyphPath = ""
    Me.tlbPrnEti.Id = 40
    Me.tlbPrnEti.Name = "tlbPrnEti"
    Me.tlbPrnEti.Visible = True
    '
    'tlbPrnAll
    '
    Me.tlbPrnAll.Caption = "Stampa entrambe"
    Me.tlbPrnAll.GlyphPath = ""
    Me.tlbPrnAll.Id = 41
    Me.tlbPrnAll.Name = "tlbPrnAll"
    Me.tlbPrnAll.Visible = True
    '
    'tlbPrnscreenEti
    '
    Me.tlbPrnscreenEti.Caption = "Stampa etichette a video"
    Me.tlbPrnscreenEti.GlyphPath = ""
    Me.tlbPrnscreenEti.Id = 42
    Me.tlbPrnscreenEti.Name = "tlbPrnscreenEti"
    Me.tlbPrnscreenEti.Visible = True
    '
    'tlbPrnscreenAll
    '
    Me.tlbPrnscreenAll.Caption = "Stampa entrambe a video"
    Me.tlbPrnscreenAll.GlyphPath = ""
    Me.tlbPrnscreenAll.Id = 43
    Me.tlbPrnscreenAll.Name = "tlbPrnscreenAll"
    Me.tlbPrnscreenAll.Visible = True
    '
    'tlbAggiungiRighe
    '
    Me.tlbAggiungiRighe.Caption = "Aggiungi righe da altro documento"
    Me.tlbAggiungiRighe.GlyphPath = ""
    Me.tlbAggiungiRighe.Id = 45
    Me.tlbAggiungiRighe.Name = "tlbAggiungiRighe"
    Me.tlbAggiungiRighe.NTSIsCheckBox = False
    Me.tlbAggiungiRighe.Visible = True
    '
    'tlbDividi
    '
    Me.tlbDividi.Caption = "Dividi riga"
    Me.tlbDividi.Glyph = CType(resources.GetObject("tlbDividi.Glyph"), System.Drawing.Image)
    Me.tlbDividi.GlyphPath = ""
    Me.tlbDividi.Id = 20
    Me.tlbDividi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbDividi.Name = "tlbDividi"
    Me.tlbDividi.Visible = True
    '
    'tlbImposta
    '
    Me.tlbImposta.Caption = "Imposta codice/numero pacco sulle righe selezionate "
    Me.tlbImposta.Glyph = CType(resources.GetObject("tlbImposta.Glyph"), System.Drawing.Image)
    Me.tlbImposta.GlyphPath = ""
    Me.tlbImposta.Id = 23
    Me.tlbImposta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbImposta.Name = "tlbImposta"
    Me.tlbImposta.Visible = True
    '
    'tlbGenera
    '
    Me.tlbGenera.Caption = "Genera codice pacco sulla riga corrente"
    Me.tlbGenera.Glyph = CType(resources.GetObject("tlbGenera.Glyph"), System.Drawing.Image)
    Me.tlbGenera.GlyphPath = ""
    Me.tlbGenera.Id = 24
    Me.tlbGenera.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbGenera.Name = "tlbGenera"
    Me.tlbGenera.Visible = True
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica righe selezionate"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 44
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 25
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 26
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaPDF
    '
    Me.tlbStampaPDF.Caption = "Stampa su PDF"
    Me.tlbStampaPDF.Glyph = CType(resources.GetObject("tlbStampaPDF.Glyph"), System.Drawing.Image)
    Me.tlbStampaPDF.GlyphPath = ""
    Me.tlbStampaPDF.Id = 27
    Me.tlbStampaPDF.Name = "tlbStampaPDF"
    Me.tlbStampaPDF.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tsPack
    '
    Me.tsPack.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tsPack.Location = New System.Drawing.Point(12, 23)
    Me.tsPack.Name = "tsPack"
    Me.tsPack.SelectedTabPage = Me.tsTestata
    Me.tsPack.Size = New System.Drawing.Size(602, 235)
    Me.tsPack.TabIndex = 9
    Me.tsPack.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tsTestata, Me.tsCorpo, Me.tsPiede, Me.tsNote})
    Me.tsPack.Text = "NtsTabControl1"
    '
    'tsTestata
    '
    Me.tsTestata.AllowDrop = True
    Me.tsTestata.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.tsTestata.Appearance.Header.Options.UseFont = True
    Me.tsTestata.Controls.Add(Me.pnTestata)
    Me.tsTestata.Enable = True
    Me.tsTestata.Name = "tsTestata"
    Me.tsTestata.Size = New System.Drawing.Size(593, 205)
    Me.tsTestata.Text = "Testata"
    '
    'pnTestata
    '
    Me.pnTestata.AllowDrop = True
    Me.pnTestata.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestata.Appearance.Options.UseBackColor = True
    Me.pnTestata.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestata.Controls.Add(Me.fmDadocu)
    Me.pnTestata.Controls.Add(Me.edTp_riferim)
    Me.pnTestata.Controls.Add(Me.edTp_conto)
    Me.pnTestata.Controls.Add(Me.edTp_datdoc)
    Me.pnTestata.Controls.Add(Me.lbDescrizione)
    Me.pnTestata.Controls.Add(Me.lbRiferimenti)
    Me.pnTestata.Controls.Add(Me.lbClifor)
    Me.pnTestata.Controls.Add(Me.lbDatdoc)
    Me.pnTestata.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestata.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTestata.Location = New System.Drawing.Point(0, 0)
    Me.pnTestata.Name = "pnTestata"
    Me.pnTestata.NTSActiveTrasparency = True
    Me.pnTestata.Size = New System.Drawing.Size(593, 205)
    Me.pnTestata.TabIndex = 0
    Me.pnTestata.Text = "NtsPanel1"
    '
    'fmDadocu
    '
    Me.fmDadocu.AllowDrop = True
    Me.fmDadocu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmDadocu.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDadocu.Appearance.Options.UseBackColor = True
    Me.fmDadocu.Controls.Add(Me.lbDesctipo)
    Me.fmDadocu.Controls.Add(Me.edTp_mmnumdoc)
    Me.fmDadocu.Controls.Add(Me.edTp_mmserie)
    Me.fmDadocu.Controls.Add(Me.edTp_mmtipork)
    Me.fmDadocu.Controls.Add(Me.edTp_mmanno)
    Me.fmDadocu.Controls.Add(Me.lbBarra)
    Me.fmDadocu.Controls.Add(Me.lbSerie)
    Me.fmDadocu.Controls.Add(Me.lbTipo)
    Me.fmDadocu.Controls.Add(Me.lbAnno)
    Me.fmDadocu.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDadocu.Location = New System.Drawing.Point(307, 97)
    Me.fmDadocu.Name = "fmDadocu"
    Me.fmDadocu.Size = New System.Drawing.Size(279, 104)
    Me.fmDadocu.TabIndex = 7
    Me.fmDadocu.Text = "Relativo al documento:"
    '
    'lbDesctipo
    '
    Me.lbDesctipo.BackColor = System.Drawing.Color.Transparent
    Me.lbDesctipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesctipo.Location = New System.Drawing.Point(137, 49)
    Me.lbDesctipo.Name = "lbDesctipo"
    Me.lbDesctipo.NTSDbField = ""
    Me.lbDesctipo.Size = New System.Drawing.Size(131, 20)
    Me.lbDesctipo.TabIndex = 8
    Me.lbDesctipo.Tooltip = ""
    Me.lbDesctipo.UseMnemonic = False
    '
    'edTp_mmnumdoc
    '
    Me.edTp_mmnumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_mmnumdoc.Enabled = False
    Me.edTp_mmnumdoc.Location = New System.Drawing.Point(153, 76)
    Me.edTp_mmnumdoc.Name = "edTp_mmnumdoc"
    Me.edTp_mmnumdoc.NTSDbField = ""
    Me.edTp_mmnumdoc.NTSFormat = "0"
    Me.edTp_mmnumdoc.NTSForzaVisZoom = False
    Me.edTp_mmnumdoc.NTSOldValue = ""
    Me.edTp_mmnumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_mmnumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_mmnumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_mmnumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_mmnumdoc.Properties.AutoHeight = False
    Me.edTp_mmnumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_mmnumdoc.Properties.MaxLength = 65536
    Me.edTp_mmnumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_mmnumdoc.Size = New System.Drawing.Size(115, 20)
    Me.edTp_mmnumdoc.TabIndex = 7
    '
    'edTp_mmserie
    '
    Me.edTp_mmserie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_mmserie.Enabled = False
    Me.edTp_mmserie.Location = New System.Drawing.Point(83, 76)
    Me.edTp_mmserie.Name = "edTp_mmserie"
    Me.edTp_mmserie.NTSDbField = ""
    Me.edTp_mmserie.NTSForzaVisZoom = False
    Me.edTp_mmserie.NTSOldValue = ""
    Me.edTp_mmserie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_mmserie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_mmserie.Properties.AutoHeight = False
    Me.edTp_mmserie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_mmserie.Properties.MaxLength = 65536
    Me.edTp_mmserie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_mmserie.Size = New System.Drawing.Size(47, 20)
    Me.edTp_mmserie.TabIndex = 6
    '
    'edTp_mmtipork
    '
    Me.edTp_mmtipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_mmtipork.Enabled = False
    Me.edTp_mmtipork.Location = New System.Drawing.Point(83, 49)
    Me.edTp_mmtipork.Name = "edTp_mmtipork"
    Me.edTp_mmtipork.NTSDbField = ""
    Me.edTp_mmtipork.NTSForzaVisZoom = False
    Me.edTp_mmtipork.NTSOldValue = ""
    Me.edTp_mmtipork.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_mmtipork.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_mmtipork.Properties.AutoHeight = False
    Me.edTp_mmtipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_mmtipork.Properties.MaxLength = 65536
    Me.edTp_mmtipork.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_mmtipork.Size = New System.Drawing.Size(47, 20)
    Me.edTp_mmtipork.TabIndex = 5
    '
    'edTp_mmanno
    '
    Me.edTp_mmanno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_mmanno.EditValue = ""
    Me.edTp_mmanno.Enabled = False
    Me.edTp_mmanno.Location = New System.Drawing.Point(83, 23)
    Me.edTp_mmanno.Name = "edTp_mmanno"
    Me.edTp_mmanno.NTSDbField = ""
    Me.edTp_mmanno.NTSFormat = "0"
    Me.edTp_mmanno.NTSForzaVisZoom = False
    Me.edTp_mmanno.NTSOldValue = "0"
    Me.edTp_mmanno.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_mmanno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_mmanno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_mmanno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_mmanno.Properties.AutoHeight = False
    Me.edTp_mmanno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_mmanno.Properties.MaxLength = 65536
    Me.edTp_mmanno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_mmanno.Size = New System.Drawing.Size(47, 20)
    Me.edTp_mmanno.TabIndex = 4
    '
    'lbBarra
    '
    Me.lbBarra.AutoSize = True
    Me.lbBarra.BackColor = System.Drawing.Color.Transparent
    Me.lbBarra.Location = New System.Drawing.Point(136, 79)
    Me.lbBarra.Name = "lbBarra"
    Me.lbBarra.NTSDbField = ""
    Me.lbBarra.Size = New System.Drawing.Size(11, 13)
    Me.lbBarra.TabIndex = 3
    Me.lbBarra.Text = "/"
    Me.lbBarra.Tooltip = ""
    Me.lbBarra.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(5, 79)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(72, 13)
    Me.lbSerie.TabIndex = 2
    Me.lbSerie.Text = "Serie/Numero"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(5, 52)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(27, 13)
    Me.lbTipo.TabIndex = 1
    Me.lbTipo.Text = "Tipo"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(5, 26)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 0
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'edTp_riferim
    '
    Me.edTp_riferim.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTp_riferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_riferim.Location = New System.Drawing.Point(95, 71)
    Me.edTp_riferim.Name = "edTp_riferim"
    Me.edTp_riferim.NTSDbField = ""
    Me.edTp_riferim.NTSForzaVisZoom = False
    Me.edTp_riferim.NTSOldValue = ""
    Me.edTp_riferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_riferim.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_riferim.Properties.AutoHeight = False
    Me.edTp_riferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_riferim.Properties.MaxLength = 65536
    Me.edTp_riferim.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_riferim.Size = New System.Drawing.Size(491, 20)
    Me.edTp_riferim.TabIndex = 6
    '
    'edTp_conto
    '
    Me.edTp_conto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_conto.EditValue = "0"
    Me.edTp_conto.Location = New System.Drawing.Point(95, 33)
    Me.edTp_conto.Name = "edTp_conto"
    Me.edTp_conto.NTSDbField = ""
    Me.edTp_conto.NTSFormat = "0"
    Me.edTp_conto.NTSForzaVisZoom = False
    Me.edTp_conto.NTSOldValue = ""
    Me.edTp_conto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_conto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_conto.Properties.AutoHeight = False
    Me.edTp_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_conto.Properties.MaxLength = 65536
    Me.edTp_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_conto.Size = New System.Drawing.Size(100, 20)
    Me.edTp_conto.TabIndex = 5
    '
    'edTp_datdoc
    '
    Me.edTp_datdoc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTp_datdoc.Location = New System.Drawing.Point(95, 7)
    Me.edTp_datdoc.Name = "edTp_datdoc"
    Me.edTp_datdoc.NTSDbField = ""
    Me.edTp_datdoc.NTSForzaVisZoom = False
    Me.edTp_datdoc.NTSOldValue = ""
    Me.edTp_datdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_datdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_datdoc.Properties.AutoHeight = False
    Me.edTp_datdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_datdoc.Properties.MaxLength = 65536
    Me.edTp_datdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_datdoc.Size = New System.Drawing.Size(100, 20)
    Me.edTp_datdoc.TabIndex = 4
    '
    'lbDescrizione
    '
    Me.lbDescrizione.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDescrizione.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrizione.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescrizione.Location = New System.Drawing.Point(201, 7)
    Me.lbDescrizione.Name = "lbDescrizione"
    Me.lbDescrizione.NTSDbField = ""
    Me.lbDescrizione.Size = New System.Drawing.Size(385, 61)
    Me.lbDescrizione.TabIndex = 3
    Me.lbDescrizione.Tooltip = ""
    Me.lbDescrizione.UseMnemonic = False
    '
    'lbRiferimenti
    '
    Me.lbRiferimenti.AutoSize = True
    Me.lbRiferimenti.BackColor = System.Drawing.Color.Transparent
    Me.lbRiferimenti.Location = New System.Drawing.Point(3, 74)
    Me.lbRiferimenti.Name = "lbRiferimenti"
    Me.lbRiferimenti.NTSDbField = ""
    Me.lbRiferimenti.Size = New System.Drawing.Size(58, 13)
    Me.lbRiferimenti.TabIndex = 2
    Me.lbRiferimenti.Text = "Riferimenti"
    Me.lbRiferimenti.Tooltip = ""
    Me.lbRiferimenti.UseMnemonic = False
    '
    'lbClifor
    '
    Me.lbClifor.AutoSize = True
    Me.lbClifor.BackColor = System.Drawing.Color.Transparent
    Me.lbClifor.Location = New System.Drawing.Point(3, 33)
    Me.lbClifor.Name = "lbClifor"
    Me.lbClifor.NTSDbField = ""
    Me.lbClifor.Size = New System.Drawing.Size(88, 13)
    Me.lbClifor.TabIndex = 1
    Me.lbClifor.Text = "Cliente/Fornitore"
    Me.lbClifor.Tooltip = ""
    Me.lbClifor.UseMnemonic = False
    '
    'lbDatdoc
    '
    Me.lbDatdoc.AutoSize = True
    Me.lbDatdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbDatdoc.Location = New System.Drawing.Point(3, 10)
    Me.lbDatdoc.Name = "lbDatdoc"
    Me.lbDatdoc.NTSDbField = ""
    Me.lbDatdoc.Size = New System.Drawing.Size(86, 13)
    Me.lbDatdoc.TabIndex = 0
    Me.lbDatdoc.Text = "Data documento"
    Me.lbDatdoc.Tooltip = ""
    Me.lbDatdoc.UseMnemonic = False
    '
    'tsCorpo
    '
    Me.tsCorpo.AllowDrop = True
    Me.tsCorpo.Controls.Add(Me.pnCorpo)
    Me.tsCorpo.Enable = True
    Me.tsCorpo.Name = "tsCorpo"
    Me.tsCorpo.Size = New System.Drawing.Size(593, 205)
    Me.tsCorpo.Text = "Corpo"
    '
    'pnCorpo
    '
    Me.pnCorpo.AllowDrop = True
    Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCorpo.Appearance.Options.UseBackColor = True
    Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCorpo.Controls.Add(Me.grPack)
    Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCorpo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCorpo.Location = New System.Drawing.Point(0, 0)
    Me.pnCorpo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCorpo.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCorpo.Name = "pnCorpo"
    Me.pnCorpo.NTSActiveTrasparency = True
    Me.pnCorpo.Size = New System.Drawing.Size(593, 205)
    Me.pnCorpo.TabIndex = 0
    Me.pnCorpo.Text = "NtsPanel1"
    '
    'grPack
    '
    Me.grPack.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grPack.EmbeddedNavigator.Name = ""
    Me.grPack.Location = New System.Drawing.Point(0, 0)
    Me.grPack.MainView = Me.grvPack
    Me.grPack.Name = "grPack"
    Me.grPack.Size = New System.Drawing.Size(593, 205)
    Me.grPack.TabIndex = 0
    Me.grPack.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPack})
    '
    'grvPack
    '
    Me.grvPack.ActiveFilterEnabled = False
    Me.grvPack.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_selez, Me.mp_riga, Me.mp_numpack, Me.mp_despack, Me.mp_codart, Me.mp_descr, Me.mp_unmis, Me.mp_colli, Me.mp_ump, Me.mp_quant, Me.mp_matric, Me.mp_peso, Me.mp_pesonetto, Me.xx_lottox, Me.mp_codvuo, Me.mp_note, Me.mp_misura1, Me.mp_misura2, Me.mp_misura3, Me.mp_ultagg, Me.mp_mmtipork, Me.mp_mmanno, Me.mp_mmserie, Me.mp_mmnumdoc, Me.mp_mmriga, Me.mp_volume, Me.mp_desint, Me.mp_code, Me.mp_datscad, Me.mp_anno, Me.mp_serie, Me.mp_numdoc, Me.mp_tctaglia, Me.mp_indtagl, Me.mp_idpallet})
    Me.grvPack.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvPack.Enabled = True
    Me.grvPack.GridControl = Me.grPack
    Me.grvPack.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvPack.MinRowHeight = 14
    Me.grvPack.Name = "grvPack"
    Me.grvPack.NTSAllowDelete = True
    Me.grvPack.NTSAllowInsert = True
    Me.grvPack.NTSAllowUpdate = True
    Me.grvPack.NTSMenuContext = Nothing
    Me.grvPack.OptionsCustomization.AllowRowSizing = True
    Me.grvPack.OptionsFilter.AllowFilterEditor = False
    Me.grvPack.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvPack.OptionsNavigation.UseTabKey = False
    Me.grvPack.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvPack.OptionsView.ColumnAutoWidth = False
    Me.grvPack.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvPack.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvPack.OptionsView.ShowGroupPanel = False
    Me.grvPack.RowHeight = 14
    '
    'xx_selez
    '
    Me.xx_selez.AppearanceCell.Options.UseBackColor = True
    Me.xx_selez.AppearanceCell.Options.UseTextOptions = True
    Me.xx_selez.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_selez.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_selez.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_selez.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_selez.Caption = "Sel."
    Me.xx_selez.Enabled = True
    Me.xx_selez.FieldName = "xx_selez"
    Me.xx_selez.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_selez.Name = "xx_selez"
    Me.xx_selez.NTSRepositoryComboBox = Nothing
    Me.xx_selez.NTSRepositoryItemCheck = Nothing
    Me.xx_selez.NTSRepositoryItemMemo = Nothing
    Me.xx_selez.NTSRepositoryItemText = Nothing
    Me.xx_selez.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_selez.OptionsFilter.AllowFilter = False
    Me.xx_selez.Visible = True
    Me.xx_selez.VisibleIndex = 0
    Me.xx_selez.Width = 40
    '
    'mp_riga
    '
    Me.mp_riga.AppearanceCell.Options.UseBackColor = True
    Me.mp_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mp_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_riga.Caption = "Riga"
    Me.mp_riga.Enabled = False
    Me.mp_riga.FieldName = "mp_riga"
    Me.mp_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_riga.Name = "mp_riga"
    Me.mp_riga.NTSRepositoryComboBox = Nothing
    Me.mp_riga.NTSRepositoryItemCheck = Nothing
    Me.mp_riga.NTSRepositoryItemMemo = Nothing
    Me.mp_riga.NTSRepositoryItemText = Nothing
    Me.mp_riga.OptionsColumn.AllowEdit = False
    Me.mp_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_riga.OptionsColumn.ReadOnly = True
    Me.mp_riga.OptionsFilter.AllowFilter = False
    Me.mp_riga.Visible = True
    Me.mp_riga.VisibleIndex = 1
    Me.mp_riga.Width = 70
    '
    'mp_numpack
    '
    Me.mp_numpack.AppearanceCell.Options.UseBackColor = True
    Me.mp_numpack.AppearanceCell.Options.UseTextOptions = True
    Me.mp_numpack.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_numpack.Caption = "Codice pacco"
    Me.mp_numpack.Enabled = True
    Me.mp_numpack.FieldName = "mp_numpack"
    Me.mp_numpack.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_numpack.Name = "mp_numpack"
    Me.mp_numpack.NTSRepositoryComboBox = Nothing
    Me.mp_numpack.NTSRepositoryItemCheck = Nothing
    Me.mp_numpack.NTSRepositoryItemMemo = Nothing
    Me.mp_numpack.NTSRepositoryItemText = Nothing
    Me.mp_numpack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_numpack.OptionsFilter.AllowFilter = False
    Me.mp_numpack.Visible = True
    Me.mp_numpack.VisibleIndex = 2
    Me.mp_numpack.Width = 70
    '
    'mp_despack
    '
    Me.mp_despack.AppearanceCell.Options.UseBackColor = True
    Me.mp_despack.AppearanceCell.Options.UseTextOptions = True
    Me.mp_despack.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_despack.Caption = "Descrizione pacco"
    Me.mp_despack.Enabled = True
    Me.mp_despack.FieldName = "mp_despack"
    Me.mp_despack.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_despack.Name = "mp_despack"
    Me.mp_despack.NTSRepositoryComboBox = Nothing
    Me.mp_despack.NTSRepositoryItemCheck = Nothing
    Me.mp_despack.NTSRepositoryItemMemo = Nothing
    Me.mp_despack.NTSRepositoryItemText = Nothing
    Me.mp_despack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_despack.OptionsFilter.AllowFilter = False
    Me.mp_despack.Visible = True
    Me.mp_despack.VisibleIndex = 3
    Me.mp_despack.Width = 70
    '
    'mp_codart
    '
    Me.mp_codart.AppearanceCell.Options.UseBackColor = True
    Me.mp_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mp_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_codart.Caption = "Codice articolo"
    Me.mp_codart.Enabled = True
    Me.mp_codart.FieldName = "mp_codart"
    Me.mp_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_codart.Name = "mp_codart"
    Me.mp_codart.NTSRepositoryComboBox = Nothing
    Me.mp_codart.NTSRepositoryItemCheck = Nothing
    Me.mp_codart.NTSRepositoryItemMemo = Nothing
    Me.mp_codart.NTSRepositoryItemText = Nothing
    Me.mp_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_codart.OptionsFilter.AllowFilter = False
    Me.mp_codart.Visible = True
    Me.mp_codart.VisibleIndex = 4
    Me.mp_codart.Width = 70
    '
    'mp_descr
    '
    Me.mp_descr.AppearanceCell.Options.UseBackColor = True
    Me.mp_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mp_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_descr.Caption = "Descrizione"
    Me.mp_descr.Enabled = True
    Me.mp_descr.FieldName = "mp_descr"
    Me.mp_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_descr.Name = "mp_descr"
    Me.mp_descr.NTSRepositoryComboBox = Nothing
    Me.mp_descr.NTSRepositoryItemCheck = Nothing
    Me.mp_descr.NTSRepositoryItemMemo = Nothing
    Me.mp_descr.NTSRepositoryItemText = Nothing
    Me.mp_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_descr.OptionsFilter.AllowFilter = False
    Me.mp_descr.Visible = True
    Me.mp_descr.VisibleIndex = 5
    Me.mp_descr.Width = 70
    '
    'mp_unmis
    '
    Me.mp_unmis.AppearanceCell.Options.UseBackColor = True
    Me.mp_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.mp_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_unmis.Caption = "U.M."
    Me.mp_unmis.Enabled = True
    Me.mp_unmis.FieldName = "mp_unmis"
    Me.mp_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_unmis.Name = "mp_unmis"
    Me.mp_unmis.NTSRepositoryComboBox = Nothing
    Me.mp_unmis.NTSRepositoryItemCheck = Nothing
    Me.mp_unmis.NTSRepositoryItemMemo = Nothing
    Me.mp_unmis.NTSRepositoryItemText = Nothing
    Me.mp_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_unmis.OptionsFilter.AllowFilter = False
    Me.mp_unmis.Visible = True
    Me.mp_unmis.VisibleIndex = 6
    Me.mp_unmis.Width = 70
    '
    'mp_colli
    '
    Me.mp_colli.AppearanceCell.Options.UseBackColor = True
    Me.mp_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mp_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_colli.Caption = "Colli"
    Me.mp_colli.Enabled = True
    Me.mp_colli.FieldName = "mp_colli"
    Me.mp_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_colli.Name = "mp_colli"
    Me.mp_colli.NTSRepositoryComboBox = Nothing
    Me.mp_colli.NTSRepositoryItemCheck = Nothing
    Me.mp_colli.NTSRepositoryItemMemo = Nothing
    Me.mp_colli.NTSRepositoryItemText = Nothing
    Me.mp_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_colli.OptionsFilter.AllowFilter = False
    Me.mp_colli.Visible = True
    Me.mp_colli.VisibleIndex = 7
    Me.mp_colli.Width = 70
    '
    'mp_ump
    '
    Me.mp_ump.AppearanceCell.Options.UseBackColor = True
    Me.mp_ump.AppearanceCell.Options.UseTextOptions = True
    Me.mp_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_ump.Caption = "U.M.P."
    Me.mp_ump.Enabled = True
    Me.mp_ump.FieldName = "mp_ump"
    Me.mp_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_ump.Name = "mp_ump"
    Me.mp_ump.NTSRepositoryComboBox = Nothing
    Me.mp_ump.NTSRepositoryItemCheck = Nothing
    Me.mp_ump.NTSRepositoryItemMemo = Nothing
    Me.mp_ump.NTSRepositoryItemText = Nothing
    Me.mp_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_ump.OptionsFilter.AllowFilter = False
    Me.mp_ump.Visible = True
    Me.mp_ump.VisibleIndex = 8
    Me.mp_ump.Width = 70
    '
    'mp_quant
    '
    Me.mp_quant.AppearanceCell.Options.UseBackColor = True
    Me.mp_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mp_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_quant.Caption = "Quantità"
    Me.mp_quant.Enabled = True
    Me.mp_quant.FieldName = "mp_quant"
    Me.mp_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_quant.Name = "mp_quant"
    Me.mp_quant.NTSRepositoryComboBox = Nothing
    Me.mp_quant.NTSRepositoryItemCheck = Nothing
    Me.mp_quant.NTSRepositoryItemMemo = Nothing
    Me.mp_quant.NTSRepositoryItemText = Nothing
    Me.mp_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_quant.OptionsFilter.AllowFilter = False
    Me.mp_quant.Visible = True
    Me.mp_quant.VisibleIndex = 9
    Me.mp_quant.Width = 70
    '
    'mp_matric
    '
    Me.mp_matric.AppearanceCell.Options.UseBackColor = True
    Me.mp_matric.AppearanceCell.Options.UseTextOptions = True
    Me.mp_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_matric.Caption = "Matricola"
    Me.mp_matric.Enabled = True
    Me.mp_matric.FieldName = "mp_matric"
    Me.mp_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_matric.Name = "mp_matric"
    Me.mp_matric.NTSRepositoryComboBox = Nothing
    Me.mp_matric.NTSRepositoryItemCheck = Nothing
    Me.mp_matric.NTSRepositoryItemMemo = Nothing
    Me.mp_matric.NTSRepositoryItemText = Nothing
    Me.mp_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_matric.OptionsFilter.AllowFilter = False
    Me.mp_matric.Visible = True
    Me.mp_matric.VisibleIndex = 10
    Me.mp_matric.Width = 70
    '
    'mp_peso
    '
    Me.mp_peso.AppearanceCell.Options.UseBackColor = True
    Me.mp_peso.AppearanceCell.Options.UseTextOptions = True
    Me.mp_peso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_peso.Caption = "Peso"
    Me.mp_peso.Enabled = True
    Me.mp_peso.FieldName = "mp_peso"
    Me.mp_peso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_peso.Name = "mp_peso"
    Me.mp_peso.NTSRepositoryComboBox = Nothing
    Me.mp_peso.NTSRepositoryItemCheck = Nothing
    Me.mp_peso.NTSRepositoryItemMemo = Nothing
    Me.mp_peso.NTSRepositoryItemText = Nothing
    Me.mp_peso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_peso.OptionsFilter.AllowFilter = False
    Me.mp_peso.Visible = True
    Me.mp_peso.VisibleIndex = 11
    Me.mp_peso.Width = 70
    '
    'mp_pesonetto
    '
    Me.mp_pesonetto.AppearanceCell.Options.UseBackColor = True
    Me.mp_pesonetto.AppearanceCell.Options.UseTextOptions = True
    Me.mp_pesonetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_pesonetto.Caption = "Peso netto"
    Me.mp_pesonetto.Enabled = True
    Me.mp_pesonetto.FieldName = "mp_pesonetto"
    Me.mp_pesonetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_pesonetto.Name = "mp_pesonetto"
    Me.mp_pesonetto.NTSRepositoryComboBox = Nothing
    Me.mp_pesonetto.NTSRepositoryItemCheck = Nothing
    Me.mp_pesonetto.NTSRepositoryItemMemo = Nothing
    Me.mp_pesonetto.NTSRepositoryItemText = Nothing
    Me.mp_pesonetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_pesonetto.OptionsFilter.AllowFilter = False
    Me.mp_pesonetto.Visible = True
    Me.mp_pesonetto.VisibleIndex = 12
    Me.mp_pesonetto.Width = 70
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
    Me.xx_lottox.VisibleIndex = 13
    Me.xx_lottox.Width = 70
    '
    'mp_codvuo
    '
    Me.mp_codvuo.AppearanceCell.Options.UseBackColor = True
    Me.mp_codvuo.AppearanceCell.Options.UseTextOptions = True
    Me.mp_codvuo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_codvuo.Caption = "Cod. cont."
    Me.mp_codvuo.Enabled = True
    Me.mp_codvuo.FieldName = "mp_codvuo"
    Me.mp_codvuo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_codvuo.Name = "mp_codvuo"
    Me.mp_codvuo.NTSRepositoryComboBox = Nothing
    Me.mp_codvuo.NTSRepositoryItemCheck = Nothing
    Me.mp_codvuo.NTSRepositoryItemMemo = Nothing
    Me.mp_codvuo.NTSRepositoryItemText = Nothing
    Me.mp_codvuo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_codvuo.OptionsFilter.AllowFilter = False
    Me.mp_codvuo.Visible = True
    Me.mp_codvuo.VisibleIndex = 14
    Me.mp_codvuo.Width = 70
    '
    'mp_note
    '
    Me.mp_note.AppearanceCell.Options.UseBackColor = True
    Me.mp_note.AppearanceCell.Options.UseTextOptions = True
    Me.mp_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_note.Caption = "Note"
    Me.mp_note.Enabled = True
    Me.mp_note.FieldName = "mp_note"
    Me.mp_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_note.Name = "mp_note"
    Me.mp_note.NTSRepositoryComboBox = Nothing
    Me.mp_note.NTSRepositoryItemCheck = Nothing
    Me.mp_note.NTSRepositoryItemMemo = Nothing
    Me.mp_note.NTSRepositoryItemText = Nothing
    Me.mp_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_note.OptionsFilter.AllowFilter = False
    Me.mp_note.Visible = True
    Me.mp_note.VisibleIndex = 15
    Me.mp_note.Width = 70
    '
    'mp_misura1
    '
    Me.mp_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mp_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mp_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_misura1.Caption = "Misura 1"
    Me.mp_misura1.Enabled = True
    Me.mp_misura1.FieldName = "mp_misura1"
    Me.mp_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_misura1.Name = "mp_misura1"
    Me.mp_misura1.NTSRepositoryComboBox = Nothing
    Me.mp_misura1.NTSRepositoryItemCheck = Nothing
    Me.mp_misura1.NTSRepositoryItemMemo = Nothing
    Me.mp_misura1.NTSRepositoryItemText = Nothing
    Me.mp_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_misura1.OptionsFilter.AllowFilter = False
    Me.mp_misura1.Visible = True
    Me.mp_misura1.VisibleIndex = 16
    Me.mp_misura1.Width = 70
    '
    'mp_misura2
    '
    Me.mp_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mp_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mp_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_misura2.Caption = "Misura 2"
    Me.mp_misura2.Enabled = True
    Me.mp_misura2.FieldName = "mp_misura2"
    Me.mp_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_misura2.Name = "mp_misura2"
    Me.mp_misura2.NTSRepositoryComboBox = Nothing
    Me.mp_misura2.NTSRepositoryItemCheck = Nothing
    Me.mp_misura2.NTSRepositoryItemMemo = Nothing
    Me.mp_misura2.NTSRepositoryItemText = Nothing
    Me.mp_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_misura2.OptionsFilter.AllowFilter = False
    Me.mp_misura2.Visible = True
    Me.mp_misura2.VisibleIndex = 17
    Me.mp_misura2.Width = 70
    '
    'mp_misura3
    '
    Me.mp_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mp_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mp_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_misura3.Caption = "Misura 3"
    Me.mp_misura3.Enabled = True
    Me.mp_misura3.FieldName = "mp_misura3"
    Me.mp_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_misura3.Name = "mp_misura3"
    Me.mp_misura3.NTSRepositoryComboBox = Nothing
    Me.mp_misura3.NTSRepositoryItemCheck = Nothing
    Me.mp_misura3.NTSRepositoryItemMemo = Nothing
    Me.mp_misura3.NTSRepositoryItemText = Nothing
    Me.mp_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_misura3.OptionsFilter.AllowFilter = False
    Me.mp_misura3.Visible = True
    Me.mp_misura3.VisibleIndex = 18
    Me.mp_misura3.Width = 70
    '
    'mp_ultagg
    '
    Me.mp_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.mp_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.mp_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_ultagg.Caption = "Data ult. agg."
    Me.mp_ultagg.Enabled = False
    Me.mp_ultagg.FieldName = "mp_ultagg"
    Me.mp_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_ultagg.Name = "mp_ultagg"
    Me.mp_ultagg.NTSRepositoryComboBox = Nothing
    Me.mp_ultagg.NTSRepositoryItemCheck = Nothing
    Me.mp_ultagg.NTSRepositoryItemMemo = Nothing
    Me.mp_ultagg.NTSRepositoryItemText = Nothing
    Me.mp_ultagg.OptionsColumn.AllowEdit = False
    Me.mp_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_ultagg.OptionsColumn.ReadOnly = True
    Me.mp_ultagg.OptionsFilter.AllowFilter = False
    Me.mp_ultagg.Visible = True
    Me.mp_ultagg.VisibleIndex = 19
    Me.mp_ultagg.Width = 70
    '
    'mp_mmtipork
    '
    Me.mp_mmtipork.AppearanceCell.Options.UseBackColor = True
    Me.mp_mmtipork.AppearanceCell.Options.UseTextOptions = True
    Me.mp_mmtipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_mmtipork.Caption = "Tipo Doc."
    Me.mp_mmtipork.Enabled = False
    Me.mp_mmtipork.FieldName = "mp_mmtipork"
    Me.mp_mmtipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_mmtipork.Name = "mp_mmtipork"
    Me.mp_mmtipork.NTSRepositoryComboBox = Nothing
    Me.mp_mmtipork.NTSRepositoryItemCheck = Nothing
    Me.mp_mmtipork.NTSRepositoryItemMemo = Nothing
    Me.mp_mmtipork.NTSRepositoryItemText = Nothing
    Me.mp_mmtipork.OptionsColumn.AllowEdit = False
    Me.mp_mmtipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_mmtipork.OptionsColumn.ReadOnly = True
    Me.mp_mmtipork.OptionsFilter.AllowFilter = False
    Me.mp_mmtipork.Visible = True
    Me.mp_mmtipork.VisibleIndex = 20
    Me.mp_mmtipork.Width = 70
    '
    'mp_mmanno
    '
    Me.mp_mmanno.AppearanceCell.Options.UseBackColor = True
    Me.mp_mmanno.AppearanceCell.Options.UseTextOptions = True
    Me.mp_mmanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_mmanno.Caption = "Anno Doc."
    Me.mp_mmanno.Enabled = False
    Me.mp_mmanno.FieldName = "mp_mmanno"
    Me.mp_mmanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_mmanno.Name = "mp_mmanno"
    Me.mp_mmanno.NTSRepositoryComboBox = Nothing
    Me.mp_mmanno.NTSRepositoryItemCheck = Nothing
    Me.mp_mmanno.NTSRepositoryItemMemo = Nothing
    Me.mp_mmanno.NTSRepositoryItemText = Nothing
    Me.mp_mmanno.OptionsColumn.AllowEdit = False
    Me.mp_mmanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_mmanno.OptionsColumn.ReadOnly = True
    Me.mp_mmanno.OptionsFilter.AllowFilter = False
    Me.mp_mmanno.Visible = True
    Me.mp_mmanno.VisibleIndex = 21
    Me.mp_mmanno.Width = 70
    '
    'mp_mmserie
    '
    Me.mp_mmserie.AppearanceCell.Options.UseBackColor = True
    Me.mp_mmserie.AppearanceCell.Options.UseTextOptions = True
    Me.mp_mmserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_mmserie.Caption = "Serie Doc."
    Me.mp_mmserie.Enabled = False
    Me.mp_mmserie.FieldName = "mp_mmserie"
    Me.mp_mmserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_mmserie.Name = "mp_mmserie"
    Me.mp_mmserie.NTSRepositoryComboBox = Nothing
    Me.mp_mmserie.NTSRepositoryItemCheck = Nothing
    Me.mp_mmserie.NTSRepositoryItemMemo = Nothing
    Me.mp_mmserie.NTSRepositoryItemText = Nothing
    Me.mp_mmserie.OptionsColumn.AllowEdit = False
    Me.mp_mmserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_mmserie.OptionsColumn.ReadOnly = True
    Me.mp_mmserie.OptionsFilter.AllowFilter = False
    Me.mp_mmserie.Visible = True
    Me.mp_mmserie.VisibleIndex = 22
    Me.mp_mmserie.Width = 70
    '
    'mp_mmnumdoc
    '
    Me.mp_mmnumdoc.AppearanceCell.Options.UseBackColor = True
    Me.mp_mmnumdoc.AppearanceCell.Options.UseTextOptions = True
    Me.mp_mmnumdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_mmnumdoc.Caption = "Num. Doc."
    Me.mp_mmnumdoc.Enabled = False
    Me.mp_mmnumdoc.FieldName = "mp_mmnumdoc"
    Me.mp_mmnumdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_mmnumdoc.Name = "mp_mmnumdoc"
    Me.mp_mmnumdoc.NTSRepositoryComboBox = Nothing
    Me.mp_mmnumdoc.NTSRepositoryItemCheck = Nothing
    Me.mp_mmnumdoc.NTSRepositoryItemMemo = Nothing
    Me.mp_mmnumdoc.NTSRepositoryItemText = Nothing
    Me.mp_mmnumdoc.OptionsColumn.AllowEdit = False
    Me.mp_mmnumdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_mmnumdoc.OptionsColumn.ReadOnly = True
    Me.mp_mmnumdoc.OptionsFilter.AllowFilter = False
    Me.mp_mmnumdoc.Visible = True
    Me.mp_mmnumdoc.VisibleIndex = 23
    Me.mp_mmnumdoc.Width = 70
    '
    'mp_mmriga
    '
    Me.mp_mmriga.AppearanceCell.Options.UseBackColor = True
    Me.mp_mmriga.AppearanceCell.Options.UseTextOptions = True
    Me.mp_mmriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_mmriga.Caption = "Riga Documento"
    Me.mp_mmriga.Enabled = False
    Me.mp_mmriga.FieldName = "mp_mmriga"
    Me.mp_mmriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_mmriga.Name = "mp_mmriga"
    Me.mp_mmriga.NTSRepositoryComboBox = Nothing
    Me.mp_mmriga.NTSRepositoryItemCheck = Nothing
    Me.mp_mmriga.NTSRepositoryItemMemo = Nothing
    Me.mp_mmriga.NTSRepositoryItemText = Nothing
    Me.mp_mmriga.OptionsColumn.AllowEdit = False
    Me.mp_mmriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_mmriga.OptionsColumn.ReadOnly = True
    Me.mp_mmriga.OptionsFilter.AllowFilter = False
    Me.mp_mmriga.Visible = True
    Me.mp_mmriga.VisibleIndex = 24
    Me.mp_mmriga.Width = 70
    '
    'mp_volume
    '
    Me.mp_volume.AppearanceCell.Options.UseBackColor = True
    Me.mp_volume.AppearanceCell.Options.UseTextOptions = True
    Me.mp_volume.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_volume.Caption = "Volume"
    Me.mp_volume.Enabled = True
    Me.mp_volume.FieldName = "mp_volume"
    Me.mp_volume.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_volume.Name = "mp_volume"
    Me.mp_volume.NTSRepositoryComboBox = Nothing
    Me.mp_volume.NTSRepositoryItemCheck = Nothing
    Me.mp_volume.NTSRepositoryItemMemo = Nothing
    Me.mp_volume.NTSRepositoryItemText = Nothing
    Me.mp_volume.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_volume.OptionsFilter.AllowFilter = False
    Me.mp_volume.Visible = True
    Me.mp_volume.VisibleIndex = 25
    Me.mp_volume.Width = 70
    '
    'mp_desint
    '
    Me.mp_desint.AppearanceCell.Options.UseBackColor = True
    Me.mp_desint.AppearanceCell.Options.UseTextOptions = True
    Me.mp_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_desint.Caption = "Descrizione interna"
    Me.mp_desint.Enabled = True
    Me.mp_desint.FieldName = "mp_desint"
    Me.mp_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_desint.Name = "mp_desint"
    Me.mp_desint.NTSRepositoryComboBox = Nothing
    Me.mp_desint.NTSRepositoryItemCheck = Nothing
    Me.mp_desint.NTSRepositoryItemMemo = Nothing
    Me.mp_desint.NTSRepositoryItemText = Nothing
    Me.mp_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_desint.OptionsFilter.AllowFilter = False
    Me.mp_desint.Visible = True
    Me.mp_desint.VisibleIndex = 26
    Me.mp_desint.Width = 70
    '
    'mp_code
    '
    Me.mp_code.AppearanceCell.Options.UseBackColor = True
    Me.mp_code.AppearanceCell.Options.UseTextOptions = True
    Me.mp_code.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_code.Caption = "Barcode"
    Me.mp_code.Enabled = True
    Me.mp_code.FieldName = "mp_code"
    Me.mp_code.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_code.Name = "mp_code"
    Me.mp_code.NTSRepositoryComboBox = Nothing
    Me.mp_code.NTSRepositoryItemCheck = Nothing
    Me.mp_code.NTSRepositoryItemMemo = Nothing
    Me.mp_code.NTSRepositoryItemText = Nothing
    Me.mp_code.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_code.OptionsFilter.AllowFilter = False
    Me.mp_code.Visible = True
    Me.mp_code.VisibleIndex = 27
    Me.mp_code.Width = 70
    '
    'mp_datscad
    '
    Me.mp_datscad.AppearanceCell.Options.UseBackColor = True
    Me.mp_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.mp_datscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_datscad.Caption = "Data scadenza"
    Me.mp_datscad.Enabled = True
    Me.mp_datscad.FieldName = "mp_datscad"
    Me.mp_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_datscad.Name = "mp_datscad"
    Me.mp_datscad.NTSRepositoryComboBox = Nothing
    Me.mp_datscad.NTSRepositoryItemCheck = Nothing
    Me.mp_datscad.NTSRepositoryItemMemo = Nothing
    Me.mp_datscad.NTSRepositoryItemText = Nothing
    Me.mp_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_datscad.OptionsFilter.AllowFilter = False
    Me.mp_datscad.Visible = True
    Me.mp_datscad.VisibleIndex = 28
    Me.mp_datscad.Width = 70
    '
    'mp_anno
    '
    Me.mp_anno.AppearanceCell.Options.UseBackColor = True
    Me.mp_anno.AppearanceCell.Options.UseTextOptions = True
    Me.mp_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_anno.Caption = "Riga Documento"
    Me.mp_anno.Enabled = False
    Me.mp_anno.FieldName = "mp_anno"
    Me.mp_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_anno.Name = "mp_anno"
    Me.mp_anno.NTSRepositoryComboBox = Nothing
    Me.mp_anno.NTSRepositoryItemCheck = Nothing
    Me.mp_anno.NTSRepositoryItemMemo = Nothing
    Me.mp_anno.NTSRepositoryItemText = Nothing
    Me.mp_anno.OptionsColumn.AllowEdit = False
    Me.mp_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_anno.OptionsColumn.ReadOnly = True
    Me.mp_anno.OptionsFilter.AllowFilter = False
    Me.mp_anno.Width = 70
    '
    'mp_serie
    '
    Me.mp_serie.AppearanceCell.Options.UseBackColor = True
    Me.mp_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mp_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_serie.Caption = "Riga Documento"
    Me.mp_serie.Enabled = False
    Me.mp_serie.FieldName = "mp_serie"
    Me.mp_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_serie.Name = "mp_serie"
    Me.mp_serie.NTSRepositoryComboBox = Nothing
    Me.mp_serie.NTSRepositoryItemCheck = Nothing
    Me.mp_serie.NTSRepositoryItemMemo = Nothing
    Me.mp_serie.NTSRepositoryItemText = Nothing
    Me.mp_serie.OptionsColumn.AllowEdit = False
    Me.mp_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_serie.OptionsColumn.ReadOnly = True
    Me.mp_serie.OptionsFilter.AllowFilter = False
    Me.mp_serie.Width = 70
    '
    'mp_numdoc
    '
    Me.mp_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.mp_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.mp_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_numdoc.Caption = "Riga Documento"
    Me.mp_numdoc.Enabled = False
    Me.mp_numdoc.FieldName = "mp_numdoc"
    Me.mp_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_numdoc.Name = "mp_numdoc"
    Me.mp_numdoc.NTSRepositoryComboBox = Nothing
    Me.mp_numdoc.NTSRepositoryItemCheck = Nothing
    Me.mp_numdoc.NTSRepositoryItemMemo = Nothing
    Me.mp_numdoc.NTSRepositoryItemText = Nothing
    Me.mp_numdoc.OptionsColumn.AllowEdit = False
    Me.mp_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_numdoc.OptionsColumn.ReadOnly = True
    Me.mp_numdoc.OptionsFilter.AllowFilter = False
    Me.mp_numdoc.Width = 70
    '
    'mp_tctaglia
    '
    Me.mp_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.mp_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.mp_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_tctaglia.Caption = "Taglia"
    Me.mp_tctaglia.Enabled = False
    Me.mp_tctaglia.FieldName = "mp_tctaglia"
    Me.mp_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_tctaglia.Name = "mp_tctaglia"
    Me.mp_tctaglia.NTSRepositoryComboBox = Nothing
    Me.mp_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.mp_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.mp_tctaglia.NTSRepositoryItemText = Nothing
    Me.mp_tctaglia.OptionsColumn.AllowEdit = False
    Me.mp_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_tctaglia.OptionsColumn.ReadOnly = True
    Me.mp_tctaglia.OptionsFilter.AllowFilter = False
    Me.mp_tctaglia.Visible = True
    Me.mp_tctaglia.VisibleIndex = 29
    '
    'mp_indtagl
    '
    Me.mp_indtagl.AppearanceCell.Options.UseBackColor = True
    Me.mp_indtagl.AppearanceCell.Options.UseTextOptions = True
    Me.mp_indtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_indtagl.Caption = "Indice taglia"
    Me.mp_indtagl.Enabled = False
    Me.mp_indtagl.FieldName = "mp_indtagl"
    Me.mp_indtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_indtagl.Name = "mp_indtagl"
    Me.mp_indtagl.NTSRepositoryComboBox = Nothing
    Me.mp_indtagl.NTSRepositoryItemCheck = Nothing
    Me.mp_indtagl.NTSRepositoryItemMemo = Nothing
    Me.mp_indtagl.NTSRepositoryItemText = Nothing
    Me.mp_indtagl.OptionsColumn.AllowEdit = False
    Me.mp_indtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_indtagl.OptionsColumn.ReadOnly = True
    Me.mp_indtagl.OptionsFilter.AllowFilter = False
    '
    'mp_idpallet
    '
    Me.mp_idpallet.AppearanceCell.Options.UseBackColor = True
    Me.mp_idpallet.AppearanceCell.Options.UseTextOptions = True
    Me.mp_idpallet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mp_idpallet.Caption = "Id pallet"
    Me.mp_idpallet.Enabled = True
    Me.mp_idpallet.FieldName = "mp_idpallet"
    Me.mp_idpallet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mp_idpallet.Name = "mp_idpallet"
    Me.mp_idpallet.NTSRepositoryComboBox = Nothing
    Me.mp_idpallet.NTSRepositoryItemCheck = Nothing
    Me.mp_idpallet.NTSRepositoryItemMemo = Nothing
    Me.mp_idpallet.NTSRepositoryItemText = Nothing
    Me.mp_idpallet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mp_idpallet.OptionsFilter.AllowFilter = False
    Me.mp_idpallet.Visible = True
    Me.mp_idpallet.VisibleIndex = 30
    '
    'tsPiede
    '
    Me.tsPiede.AllowDrop = True
    Me.tsPiede.Controls.Add(Me.pnPiede)
    Me.tsPiede.Enable = True
    Me.tsPiede.Name = "tsPiede"
    Me.tsPiede.Size = New System.Drawing.Size(593, 205)
    Me.tsPiede.Text = "Piede"
    '
    'pnPiede
    '
    Me.pnPiede.AllowDrop = True
    Me.pnPiede.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPiede.Appearance.Options.UseBackColor = True
    Me.pnPiede.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPiede.Controls.Add(Me.fmAccompagnatori)
    Me.pnPiede.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPiede.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPiede.Location = New System.Drawing.Point(0, 0)
    Me.pnPiede.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPiede.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPiede.Name = "pnPiede"
    Me.pnPiede.NTSActiveTrasparency = True
    Me.pnPiede.Size = New System.Drawing.Size(593, 205)
    Me.pnPiede.TabIndex = 0
    Me.pnPiede.Text = "NtsPanel1"
    '
    'fmAccompagnatori
    '
    Me.fmAccompagnatori.AllowDrop = True
    Me.fmAccompagnatori.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmAccompagnatori.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAccompagnatori.Appearance.Options.UseBackColor = True
    Me.fmAccompagnatori.Controls.Add(Me.pnControlliPiede)
    Me.fmAccompagnatori.Controls.Add(Me.pnTotali)
    Me.fmAccompagnatori.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAccompagnatori.Location = New System.Drawing.Point(0, 0)
    Me.fmAccompagnatori.Name = "fmAccompagnatori"
    Me.fmAccompagnatori.Size = New System.Drawing.Size(593, 205)
    Me.fmAccompagnatori.TabIndex = 0
    Me.fmAccompagnatori.Text = "Dati accompagnatori"
    '
    'pnControlliPiede
    '
    Me.pnControlliPiede.AllowDrop = True
    Me.pnControlliPiede.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlliPiede.Appearance.Options.UseBackColor = True
    Me.pnControlliPiede.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlliPiede.Controls.Add(Me.lbCodVet)
    Me.pnControlliPiede.Controls.Add(Me.lbCodVett2)
    Me.pnControlliPiede.Controls.Add(Me.lbDescdest2)
    Me.pnControlliPiede.Controls.Add(Me.lbDest1)
    Me.pnControlliPiede.Controls.Add(Me.lbDescdest1)
    Me.pnControlliPiede.Controls.Add(Me.lbDest2)
    Me.pnControlliPiede.Controls.Add(Me.lbDescvett2)
    Me.pnControlliPiede.Controls.Add(Me.edTp_codvett)
    Me.pnControlliPiede.Controls.Add(Me.lbDescVet)
    Me.pnControlliPiede.Controls.Add(Me.edTp_codvett2)
    Me.pnControlliPiede.Controls.Add(Me.edTp_coddest2)
    Me.pnControlliPiede.Controls.Add(Me.edTp_coddest)
    Me.pnControlliPiede.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlliPiede.Location = New System.Drawing.Point(5, 23)
    Me.pnControlliPiede.Name = "pnControlliPiede"
    Me.pnControlliPiede.NTSActiveTrasparency = True
    Me.pnControlliPiede.Size = New System.Drawing.Size(372, 176)
    Me.pnControlliPiede.TabIndex = 95
    Me.pnControlliPiede.Text = "NtsPanel1"
    '
    'lbCodVet
    '
    Me.lbCodVet.AutoSize = True
    Me.lbCodVet.BackColor = System.Drawing.Color.Transparent
    Me.lbCodVet.Location = New System.Drawing.Point(3, 13)
    Me.lbCodVet.Name = "lbCodVet"
    Me.lbCodVet.NTSDbField = ""
    Me.lbCodVet.Size = New System.Drawing.Size(78, 13)
    Me.lbCodVet.TabIndex = 0
    Me.lbCodVet.Text = "Codice Vettore"
    Me.lbCodVet.Tooltip = ""
    Me.lbCodVet.UseMnemonic = False
    '
    'lbCodVett2
    '
    Me.lbCodVett2.AutoSize = True
    Me.lbCodVett2.BackColor = System.Drawing.Color.Transparent
    Me.lbCodVett2.Location = New System.Drawing.Point(3, 39)
    Me.lbCodVett2.Name = "lbCodVett2"
    Me.lbCodVett2.NTSDbField = ""
    Me.lbCodVett2.Size = New System.Drawing.Size(87, 13)
    Me.lbCodVett2.TabIndex = 1
    Me.lbCodVett2.Text = "Codice Vettore 2"
    Me.lbCodVett2.Tooltip = ""
    Me.lbCodVett2.UseMnemonic = False
    '
    'lbDescdest2
    '
    Me.lbDescdest2.BackColor = System.Drawing.Color.Transparent
    Me.lbDescdest2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescdest2.Location = New System.Drawing.Point(152, 88)
    Me.lbDescdest2.Name = "lbDescdest2"
    Me.lbDescdest2.NTSDbField = ""
    Me.lbDescdest2.Size = New System.Drawing.Size(215, 20)
    Me.lbDescdest2.TabIndex = 11
    Me.lbDescdest2.Tooltip = ""
    Me.lbDescdest2.UseMnemonic = False
    '
    'lbDest1
    '
    Me.lbDest1.AutoSize = True
    Me.lbDest1.BackColor = System.Drawing.Color.Transparent
    Me.lbDest1.Location = New System.Drawing.Point(3, 65)
    Me.lbDest1.Name = "lbDest1"
    Me.lbDest1.NTSDbField = ""
    Me.lbDest1.Size = New System.Drawing.Size(88, 13)
    Me.lbDest1.TabIndex = 2
    Me.lbDest1.Text = "Destin. diversa 1"
    Me.lbDest1.Tooltip = ""
    Me.lbDest1.UseMnemonic = False
    '
    'lbDescdest1
    '
    Me.lbDescdest1.BackColor = System.Drawing.Color.Transparent
    Me.lbDescdest1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescdest1.Location = New System.Drawing.Point(152, 62)
    Me.lbDescdest1.Name = "lbDescdest1"
    Me.lbDescdest1.NTSDbField = ""
    Me.lbDescdest1.Size = New System.Drawing.Size(215, 20)
    Me.lbDescdest1.TabIndex = 10
    Me.lbDescdest1.Tooltip = ""
    Me.lbDescdest1.UseMnemonic = False
    '
    'lbDest2
    '
    Me.lbDest2.AutoSize = True
    Me.lbDest2.BackColor = System.Drawing.Color.Transparent
    Me.lbDest2.Location = New System.Drawing.Point(3, 91)
    Me.lbDest2.Name = "lbDest2"
    Me.lbDest2.NTSDbField = ""
    Me.lbDest2.Size = New System.Drawing.Size(88, 13)
    Me.lbDest2.TabIndex = 3
    Me.lbDest2.Text = "Destin. diversa 2"
    Me.lbDest2.Tooltip = ""
    Me.lbDest2.UseMnemonic = False
    '
    'lbDescvett2
    '
    Me.lbDescvett2.BackColor = System.Drawing.Color.Transparent
    Me.lbDescvett2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescvett2.Location = New System.Drawing.Point(152, 36)
    Me.lbDescvett2.Name = "lbDescvett2"
    Me.lbDescvett2.NTSDbField = ""
    Me.lbDescvett2.Size = New System.Drawing.Size(215, 20)
    Me.lbDescvett2.TabIndex = 9
    Me.lbDescvett2.Tooltip = ""
    Me.lbDescvett2.UseMnemonic = False
    '
    'edTp_codvett
    '
    Me.edTp_codvett.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_codvett.EditValue = "0"
    Me.edTp_codvett.Location = New System.Drawing.Point(97, 10)
    Me.edTp_codvett.Name = "edTp_codvett"
    Me.edTp_codvett.NTSDbField = ""
    Me.edTp_codvett.NTSFormat = "0"
    Me.edTp_codvett.NTSForzaVisZoom = False
    Me.edTp_codvett.NTSOldValue = ""
    Me.edTp_codvett.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_codvett.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_codvett.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_codvett.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_codvett.Properties.AutoHeight = False
    Me.edTp_codvett.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_codvett.Properties.MaxLength = 65536
    Me.edTp_codvett.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_codvett.Size = New System.Drawing.Size(49, 20)
    Me.edTp_codvett.TabIndex = 4
    '
    'lbDescVet
    '
    Me.lbDescVet.BackColor = System.Drawing.Color.Transparent
    Me.lbDescVet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescVet.Location = New System.Drawing.Point(152, 10)
    Me.lbDescVet.Name = "lbDescVet"
    Me.lbDescVet.NTSDbField = ""
    Me.lbDescVet.Size = New System.Drawing.Size(215, 20)
    Me.lbDescVet.TabIndex = 8
    Me.lbDescVet.Tooltip = ""
    Me.lbDescVet.UseMnemonic = False
    '
    'edTp_codvett2
    '
    Me.edTp_codvett2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_codvett2.EditValue = "0"
    Me.edTp_codvett2.Location = New System.Drawing.Point(97, 36)
    Me.edTp_codvett2.Name = "edTp_codvett2"
    Me.edTp_codvett2.NTSDbField = ""
    Me.edTp_codvett2.NTSFormat = "0"
    Me.edTp_codvett2.NTSForzaVisZoom = False
    Me.edTp_codvett2.NTSOldValue = ""
    Me.edTp_codvett2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_codvett2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_codvett2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_codvett2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_codvett2.Properties.AutoHeight = False
    Me.edTp_codvett2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_codvett2.Properties.MaxLength = 65536
    Me.edTp_codvett2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_codvett2.Size = New System.Drawing.Size(49, 20)
    Me.edTp_codvett2.TabIndex = 5
    '
    'edTp_coddest2
    '
    Me.edTp_coddest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_coddest2.EditValue = "0"
    Me.edTp_coddest2.Location = New System.Drawing.Point(97, 88)
    Me.edTp_coddest2.Name = "edTp_coddest2"
    Me.edTp_coddest2.NTSDbField = ""
    Me.edTp_coddest2.NTSFormat = "0"
    Me.edTp_coddest2.NTSForzaVisZoom = False
    Me.edTp_coddest2.NTSOldValue = ""
    Me.edTp_coddest2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_coddest2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_coddest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_coddest2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_coddest2.Properties.AutoHeight = False
    Me.edTp_coddest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_coddest2.Properties.MaxLength = 65536
    Me.edTp_coddest2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_coddest2.Size = New System.Drawing.Size(49, 20)
    Me.edTp_coddest2.TabIndex = 7
    '
    'edTp_coddest
    '
    Me.edTp_coddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_coddest.EditValue = "0"
    Me.edTp_coddest.Location = New System.Drawing.Point(97, 62)
    Me.edTp_coddest.Name = "edTp_coddest"
    Me.edTp_coddest.NTSDbField = ""
    Me.edTp_coddest.NTSFormat = "0"
    Me.edTp_coddest.NTSForzaVisZoom = False
    Me.edTp_coddest.NTSOldValue = ""
    Me.edTp_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_coddest.Properties.AutoHeight = False
    Me.edTp_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_coddest.Properties.MaxLength = 65536
    Me.edTp_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_coddest.Size = New System.Drawing.Size(49, 20)
    Me.edTp_coddest.TabIndex = 6
    '
    'pnTotali
    '
    Me.pnTotali.AllowDrop = True
    Me.pnTotali.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTotali.Appearance.Options.UseBackColor = True
    Me.pnTotali.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTotali.Controls.Add(Me.fmTotali)
    Me.pnTotali.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTotali.Location = New System.Drawing.Point(377, 23)
    Me.pnTotali.Name = "pnTotali"
    Me.pnTotali.NTSActiveTrasparency = True
    Me.pnTotali.Size = New System.Drawing.Size(215, 176)
    Me.pnTotali.TabIndex = 94
    Me.pnTotali.Text = "NtsPanel1"
    '
    'fmTotali
    '
    Me.fmTotali.AllowDrop = True
    Me.fmTotali.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTotali.Appearance.Options.UseBackColor = True
    Me.fmTotali.Controls.Add(Me.ckTp_sblocca)
    Me.fmTotali.Controls.Add(Me.edTp_totvolume)
    Me.fmTotali.Controls.Add(Me.edTp_peso)
    Me.fmTotali.Controls.Add(Me.edTp_pesonetto)
    Me.fmTotali.Controls.Add(Me.edTp_totcoll)
    Me.fmTotali.Controls.Add(Me.lbVolume)
    Me.fmTotali.Controls.Add(Me.lbNetto)
    Me.fmTotali.Controls.Add(Me.lbLordo)
    Me.fmTotali.Controls.Add(Me.lbColli)
    Me.fmTotali.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTotali.Location = New System.Drawing.Point(1, 3)
    Me.fmTotali.Name = "fmTotali"
    Me.fmTotali.Size = New System.Drawing.Size(212, 155)
    Me.fmTotali.TabIndex = 12
    Me.fmTotali.Text = "Totali"
    '
    'ckTp_sblocca
    '
    Me.ckTp_sblocca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTp_sblocca.Location = New System.Drawing.Point(8, 128)
    Me.ckTp_sblocca.Name = "ckTp_sblocca"
    Me.ckTp_sblocca.NTSCheckValue = "S"
    Me.ckTp_sblocca.NTSUnCheckValue = "N"
    Me.ckTp_sblocca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTp_sblocca.Properties.Appearance.Options.UseBackColor = True
    Me.ckTp_sblocca.Properties.AutoHeight = False
    Me.ckTp_sblocca.Properties.Caption = "Sblocca"
    Me.ckTp_sblocca.Size = New System.Drawing.Size(75, 19)
    Me.ckTp_sblocca.TabIndex = 8
    '
    'edTp_totvolume
    '
    Me.edTp_totvolume.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_totvolume.EditValue = "0"
    Me.edTp_totvolume.Enabled = False
    Me.edTp_totvolume.Location = New System.Drawing.Point(68, 101)
    Me.edTp_totvolume.Name = "edTp_totvolume"
    Me.edTp_totvolume.NTSDbField = ""
    Me.edTp_totvolume.NTSFormat = "0"
    Me.edTp_totvolume.NTSForzaVisZoom = False
    Me.edTp_totvolume.NTSOldValue = ""
    Me.edTp_totvolume.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_totvolume.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_totvolume.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_totvolume.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_totvolume.Properties.AutoHeight = False
    Me.edTp_totvolume.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_totvolume.Properties.MaxLength = 65536
    Me.edTp_totvolume.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_totvolume.Size = New System.Drawing.Size(131, 20)
    Me.edTp_totvolume.TabIndex = 7
    '
    'edTp_peso
    '
    Me.edTp_peso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_peso.EditValue = "0"
    Me.edTp_peso.Enabled = False
    Me.edTp_peso.Location = New System.Drawing.Point(68, 49)
    Me.edTp_peso.Name = "edTp_peso"
    Me.edTp_peso.NTSDbField = ""
    Me.edTp_peso.NTSFormat = "0"
    Me.edTp_peso.NTSForzaVisZoom = False
    Me.edTp_peso.NTSOldValue = ""
    Me.edTp_peso.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_peso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_peso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_peso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_peso.Properties.AutoHeight = False
    Me.edTp_peso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_peso.Properties.MaxLength = 65536
    Me.edTp_peso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_peso.Size = New System.Drawing.Size(131, 20)
    Me.edTp_peso.TabIndex = 6
    '
    'edTp_pesonetto
    '
    Me.edTp_pesonetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_pesonetto.EditValue = "0"
    Me.edTp_pesonetto.Enabled = False
    Me.edTp_pesonetto.Location = New System.Drawing.Point(68, 75)
    Me.edTp_pesonetto.Name = "edTp_pesonetto"
    Me.edTp_pesonetto.NTSDbField = ""
    Me.edTp_pesonetto.NTSFormat = "0"
    Me.edTp_pesonetto.NTSForzaVisZoom = False
    Me.edTp_pesonetto.NTSOldValue = ""
    Me.edTp_pesonetto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_pesonetto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_pesonetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_pesonetto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_pesonetto.Properties.AutoHeight = False
    Me.edTp_pesonetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_pesonetto.Properties.MaxLength = 65536
    Me.edTp_pesonetto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_pesonetto.Size = New System.Drawing.Size(131, 20)
    Me.edTp_pesonetto.TabIndex = 5
    '
    'edTp_totcoll
    '
    Me.edTp_totcoll.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_totcoll.EditValue = "0"
    Me.edTp_totcoll.Enabled = False
    Me.edTp_totcoll.Location = New System.Drawing.Point(68, 23)
    Me.edTp_totcoll.Name = "edTp_totcoll"
    Me.edTp_totcoll.NTSDbField = ""
    Me.edTp_totcoll.NTSFormat = "0"
    Me.edTp_totcoll.NTSForzaVisZoom = False
    Me.edTp_totcoll.NTSOldValue = ""
    Me.edTp_totcoll.Properties.Appearance.Options.UseTextOptions = True
    Me.edTp_totcoll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTp_totcoll.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTp_totcoll.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTp_totcoll.Properties.AutoHeight = False
    Me.edTp_totcoll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTp_totcoll.Properties.MaxLength = 65536
    Me.edTp_totcoll.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_totcoll.Size = New System.Drawing.Size(131, 20)
    Me.edTp_totcoll.TabIndex = 4
    '
    'lbVolume
    '
    Me.lbVolume.AutoSize = True
    Me.lbVolume.BackColor = System.Drawing.Color.Transparent
    Me.lbVolume.Location = New System.Drawing.Point(5, 104)
    Me.lbVolume.Name = "lbVolume"
    Me.lbVolume.NTSDbField = ""
    Me.lbVolume.Size = New System.Drawing.Size(41, 13)
    Me.lbVolume.TabIndex = 3
    Me.lbVolume.Text = "Volume"
    Me.lbVolume.Tooltip = ""
    Me.lbVolume.UseMnemonic = False
    '
    'lbNetto
    '
    Me.lbNetto.AutoSize = True
    Me.lbNetto.BackColor = System.Drawing.Color.Transparent
    Me.lbNetto.Location = New System.Drawing.Point(5, 80)
    Me.lbNetto.Name = "lbNetto"
    Me.lbNetto.NTSDbField = ""
    Me.lbNetto.Size = New System.Drawing.Size(59, 13)
    Me.lbNetto.TabIndex = 2
    Me.lbNetto.Text = "Peso netto"
    Me.lbNetto.Tooltip = ""
    Me.lbNetto.UseMnemonic = False
    '
    'lbLordo
    '
    Me.lbLordo.AutoSize = True
    Me.lbLordo.BackColor = System.Drawing.Color.Transparent
    Me.lbLordo.Location = New System.Drawing.Point(5, 52)
    Me.lbLordo.Name = "lbLordo"
    Me.lbLordo.NTSDbField = ""
    Me.lbLordo.Size = New System.Drawing.Size(57, 13)
    Me.lbLordo.TabIndex = 1
    Me.lbLordo.Text = "Peso lordo"
    Me.lbLordo.Tooltip = ""
    Me.lbLordo.UseMnemonic = False
    '
    'lbColli
    '
    Me.lbColli.AutoSize = True
    Me.lbColli.BackColor = System.Drawing.Color.Transparent
    Me.lbColli.Location = New System.Drawing.Point(5, 26)
    Me.lbColli.Name = "lbColli"
    Me.lbColli.NTSDbField = ""
    Me.lbColli.Size = New System.Drawing.Size(26, 13)
    Me.lbColli.TabIndex = 0
    Me.lbColli.Text = "Colli"
    Me.lbColli.Tooltip = ""
    Me.lbColli.UseMnemonic = False
    '
    'tsNote
    '
    Me.tsNote.AllowDrop = True
    Me.tsNote.Controls.Add(Me.pnNote)
    Me.tsNote.Enable = True
    Me.tsNote.Name = "tsNote"
    Me.tsNote.Size = New System.Drawing.Size(593, 205)
    Me.tsNote.Text = "Note"
    '
    'pnNote
    '
    Me.pnNote.AllowDrop = True
    Me.pnNote.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNote.Appearance.Options.UseBackColor = True
    Me.pnNote.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNote.Controls.Add(Me.edTp_note)
    Me.pnNote.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNote.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnNote.Location = New System.Drawing.Point(0, 0)
    Me.pnNote.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnNote.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnNote.Name = "pnNote"
    Me.pnNote.NTSActiveTrasparency = True
    Me.pnNote.Size = New System.Drawing.Size(593, 205)
    Me.pnNote.TabIndex = 1
    Me.pnNote.Text = "NtsPanel1"
    '
    'edTp_note
    '
    Me.edTp_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTp_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edTp_note.Location = New System.Drawing.Point(0, 0)
    Me.edTp_note.Name = "edTp_note"
    Me.edTp_note.NTSDbField = ""
    Me.edTp_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTp_note.Size = New System.Drawing.Size(593, 205)
    Me.edTp_note.TabIndex = 0
    '
    'pnPack
    '
    Me.pnPack.AllowDrop = True
    Me.pnPack.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPack.Appearance.Options.UseBackColor = True
    Me.pnPack.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPack.Controls.Add(Me.lbTitolo)
    Me.pnPack.Controls.Add(Me.tsPack)
    Me.pnPack.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPack.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPack.Location = New System.Drawing.Point(0, 30)
    Me.pnPack.Name = "pnPack"
    Me.pnPack.NTSActiveTrasparency = True
    Me.pnPack.Size = New System.Drawing.Size(626, 269)
    Me.pnPack.TabIndex = 10
    Me.pnPack.Text = "NtsPanel1"
    '
    'lbTitolo
    '
    Me.lbTitolo.AutoSize = True
    Me.lbTitolo.BackColor = System.Drawing.Color.Transparent
    Me.lbTitolo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTitolo.Location = New System.Drawing.Point(12, 7)
    Me.lbTitolo.Name = "lbTitolo"
    Me.lbTitolo.NTSDbField = ""
    Me.lbTitolo.Size = New System.Drawing.Size(294, 13)
    Me.lbTitolo.TabIndex = 10
    Me.lbTitolo.Text = "Packing list n° 0 del 00/00/00 - 00000000: Nessuno"
    Me.lbTitolo.Tooltip = ""
    Me.lbTitolo.UseMnemonic = False
    '
    'FRMVEPACK
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(626, 299)
    Me.Controls.Add(Me.pnPack)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMVEPACK"
    Me.Text = "GESTIONE PACKING LIST"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsPack, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsPack.ResumeLayout(False)
    Me.tsTestata.ResumeLayout(False)
    CType(Me.pnTestata, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestata.ResumeLayout(False)
    Me.pnTestata.PerformLayout()
    CType(Me.fmDadocu, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDadocu.ResumeLayout(False)
    Me.fmDadocu.PerformLayout()
    CType(Me.edTp_mmnumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_mmserie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_mmtipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_mmanno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_riferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_conto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_datdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsCorpo.ResumeLayout(False)
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCorpo.ResumeLayout(False)
    CType(Me.grPack, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvPack, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsPiede.ResumeLayout(False)
    CType(Me.pnPiede, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPiede.ResumeLayout(False)
    CType(Me.fmAccompagnatori, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAccompagnatori.ResumeLayout(False)
    CType(Me.pnControlliPiede, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlliPiede.ResumeLayout(False)
    Me.pnControlliPiede.PerformLayout()
    CType(Me.edTp_codvett.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_codvett2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_coddest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTotali, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTotali.ResumeLayout(False)
    CType(Me.fmTotali, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTotali.ResumeLayout(False)
    Me.fmTotali.PerformLayout()
    CType(Me.ckTp_sblocca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_totvolume.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_peso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_pesonetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTp_totcoll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsNote.ResumeLayout(False)
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNote.ResumeLayout(False)
    CType(Me.edTp_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPack, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPack.ResumeLayout(False)
    Me.pnPack.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbDividi.GlyphPath = (oApp.ChildImageDir & "\dividi.gif")
        tlbRecNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecAggiorna.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRecRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaPDF.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbImposta.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGenera.GlyphPath = (oApp.ChildImageDir & "\conai.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edTp_mmnumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128695949933875362, "Numero documento"), "0", 9, 0, 999999999)
      edTp_mmserie.NTSSetParam(oMenu, oApp.Tr(Me, 128695949934031613, "Serie documento"), CLN__STD.SerieMaxLen)
      edTp_mmtipork.NTSSetParam(oMenu, oApp.Tr(Me, 128695949934187864, "Tipo documento"), 1)
      edTp_mmanno.NTSSetParam(oMenu, oApp.Tr(Me, 128695949934344115, "Anno documento"), "0", 4, 0, 9999)
      edTp_riferim.NTSSetParam(oMenu, oApp.Tr(Me, 128695949934969119, "Riferimento"), 50, True)
      edTp_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128695949935125370, "Cliente/Fornitore"), tabanagrac)
      edTp_datdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128695949935281621, "Data documento"), False)
      ckTp_sblocca.NTSSetParam(oMenu, oApp.Tr(Me, 128695949936219127, "Sblocca"), "S", "N")
      edTp_totvolume.NTSSetParam(oMenu, oApp.Tr(Me, 128695949936375378, "Volume totale"), oApp.FormatQta, 9, 0, 999999999)
      edTp_peso.NTSSetParam(oMenu, oApp.Tr(Me, 128695949936531629, "Peso lordo totale"), oApp.FormatQta, 9, 0, 999999999)
      edTp_pesonetto.NTSSetParam(oMenu, oApp.Tr(Me, 128695949936687880, "Peso netto totale"), oApp.FormatQta, 9, 0, 999999999)
      edTp_totcoll.NTSSetParam(oMenu, oApp.Tr(Me, 128695949936844131, "Totale colli"), oApp.FormatQta, 9, 0, 999999999)
      edTp_coddest2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128695949938094139, "Destinazione Diversa 2"), tabdestdiv)
      edTp_coddest2.CliePerDestdiv = edTp_conto
      edTp_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128695949938250390, "Destinazione Diversa 1"), tabdestdiv)
      edTp_coddest.CliePerDestdiv = edTp_conto
      edTp_codvett2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128695949938406641, "Codice vettore 2"), tabvett)
      edTp_codvett.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128695949938562892, "Codice vettore"), tabvett)
      edTp_note.NTSSetParam(oMenu, oApp.Tr(Me, 128695949939187896, "Note"), 0, True)

      dttUm = oClePack.CaricaUnMis()
      dttUm.AcceptChanges()

      grvPack.NTSSetParam(oMenu, oApp.Tr(Me, 128699350863463843, "Gestione Packing List"))
      xx_selez.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128701953128290396, "Selezionato"), "S", "N")
      mp_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128701990904874982, "Riga"), "#,##0.00", 15)
      mp_numpack.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350863932590, "Codice pacco"), 20, False)
      mp_despack.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350864088839, "Descrizione pacco"), 50, True)
      mp_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128699350864245088, "Codice articolo"), tabartico, False)
      mp_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350864401337, "Descrizione"), 40, True)
      mp_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128708025253792763, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      mp_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350864713835, "Colli"), oApp.FormatQta, 15)
      mp_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350864870084, "Quantità"), oApp.FormatQta, 15)
      mp_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350865026333, "Matricola"), 18, True)
      mp_peso.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350865182582, "Peso"), oApp.FormatQta, 15)
      mp_pesonetto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350865338831, "Peso netto"), oApp.FormatQta, 15)
      If oClePack.bLottoNew = False Then
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129520241870050023, "Lotto"), 9, True)
      Else
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350865495080, "Lotto"), 50, True)
      End If
      mp_codvuo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350865651329, "Cod. cont."), "0", 4, 0, 9999)
      mp_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350865807578, "Note"), 0, True)
      mp_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350865963827, "Misura 1"), oApp.FormatQta, 15)
      mp_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350866120076, "Misura 2"), oApp.FormatQta, 15)
      mp_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350866276325, "Misura 3"), oApp.FormatQta, 15)
      mp_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128699350866432574, "Data ult. agg."), False)
      mp_mmtipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350866588823, "mp_mmtipork"), 1, False)
      mp_mmanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350866745072, "mp_mmanno"), "0", 4, 0, 9999)
      mp_mmserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350866901321, "mp_mmserie"), CLN__STD.SerieMaxLen, False)
      mp_mmnumdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350867057570, "mp_mmnumdoc"), "0", 9, 0, 999999999)
      mp_mmriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350867213819, "mp_mmriga"), "0", 9, 0, 999999999)
      mp_volume.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128699350867370068, "Volume"), oApp.FormatQta, 15)
      mp_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350867526317, "Descrizione interna"), 40, True)
      mp_code.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128699350868151313, "Barcode"), 255, False)
      mp_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128699350868307562, "Data scadenza"), True)
      mp_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128707859241941072, "Anno"), "0", 4, 0, 9999)
      mp_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128707859242097324, "Serie"), CLN__STD.SerieMaxLen, False)
      mp_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128707859242253576, "Numero doc."), "0", 9, 0, 999999999)
      mp_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128710447775337919, "U.M.P."), 3, False)
      mp_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130408414412089563, "Taglia"), 5, False)
      mp_indtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130408414412245817, "Indice taglia"), "0", 4, 0, 9999)
      mp_idpallet.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130408414412402071, "Id pallet"), 18, True)

      xx_lottox.NTSSetParamZoom("ZOOMLOTTI")

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMVEPACK_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim dttTmp As New DataTable
    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oClePack.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()

      InitControls()

      NascondiControlli()

      tsPack.SelectedTabPageIndex = 0

      bOrdMatrMultiRow = CBool(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "OrdMatrMultiRow", "-1", " ", "-1"))
      oClePack.lNoModifQuantSuColli = NTSCInt(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "NoModifQuantSuColli", "0", " ", "0"))
      bNoModifQuantSuColli = CBool(oClePack.lNoModifQuantSuColli)
      bChiudiAlSalvataggio = CBool(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "ChiudiAlSalvataggio", "-1", ".", "-1"))
      oClePack.bDopoDividiMantieniSelezione = CBool(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "DopoDividiMantieniSelezione", "0", ".", "0"))
      oClePack.bNoModifQuantSuColli = bNoModifQuantSuColli

      Select Case oMenu.GetSettingBus("BSVEPACK", "RECENT", ".", "PrelevaDati", "D", " ", "D")
        Case "D"
          tlbDaDocu.Down = True
          tlbDaOrdine.Down = False
          tlbDaNiente.Down = False
          tlbDaPacking.Down = False
        Case "O"
          tlbDaDocu.Down = False
          tlbDaOrdine.Down = True
          tlbDaNiente.Down = False
          tlbDaPacking.Down = False
        Case "N"
          tlbDaDocu.Down = False
          tlbDaOrdine.Down = False
          tlbDaNiente.Down = True
          tlbDaPacking.Down = False
        Case "L"
          tlbDaDocu.Down = False
          tlbDaOrdine.Down = False
          tlbDaNiente.Down = False
          tlbDaPacking.Down = True
      End Select
      '-----------------------------------------------------------------------------------------
      Select Case oMenu.GetSettingBus("BSVEPACK", "RECENT", ".", "RilevaMisVol", "0", " ", "0")
        Case "-1" : tlbRileva.Down = True
        Case "1" : tlbRilevaDaDoc.Down = True
        Case "2" : tlbNonRilevare.Down = True
        Case Else : tlbMisurea1.Down = True
      End Select
      lIncremContatoreRiga = NTSCDec(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "IncremContatoreRiga", "1", " ", "1")) 'NON DOCUMENTARE
      oClePack.lIncremContatoreRiga = lIncremContatoreRiga
      '-----------------------------------------------------------------------------------------
      strPrefissoNumeroPacco = oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "PrefissoNumeroPacco", "", " ", "")  ' NON DOCUMENTARE
      If strPrefissoNumeroPacco.Length > 9 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128696756079400508, "Attenzione!" & vbCrLf & _
                               "Il prefisso numero pacco impostato nel registro di Business supera i 9 caratteri." & vbCrLf & _
                               "Pertanto non sarà utilizzato nell'eventuale generazione automatica del codice pacco."))

        strPrefissoNumeroPacco = ""
      End If
      '-----------------------------------------------------------------------------------------
      '--- Personalizzazione VORPA:
      '--- Se settato diverso da 0, ordina le righe in modo diverso quando sono prelevate
      '--- dai documenti in FileNuovo
      '-----------------------------------------------------------------------------------------
      lOrdinamentoNuoveRighe = NTSCInt(oMenu.GetSettingBus("BSVEPACK", "OPZIONI", ".", "OrdinamentoNuoveRighe", "0", " ", "0"))  ' NON DOCUMENTARE
      '-----------------------------------------------------------------------------------------

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRMVEPACK_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim strTmp As String = ""
    Try
      If tlbStampa.Enabled Then
        If Not Ripristina() Then
          e.Cancel = True
        End If
      End If

      '----------------------------------------------------------------------------------------
      '--- Salva nel RECENT l'origine dei dati (documenti/ordini) dalla quale prelevare i dati
      '----------------------------------------------------------------------------------------
      If tlbDaDocu.Down Then
        oMenu.SaveSettingBus("BSVEPACK", "RECENT", ".", "PrelevaDati", "D", " ", "NS.", "...", "...")
      ElseIf tlbDaOrdine.Down Then
        oMenu.SaveSettingBus("BSVEPACK", "RECENT", ".", "PrelevaDati", "O", " ", "NS.", "...", "...")
      ElseIf tlbDaPacking.Down Then
        oMenu.SaveSettingBus("BSVEPACK", "RECENT", ".", "PrelevaDati", "L", " ", "NS.", "...", "...")
      ElseIf tlbDaNiente.Down Then
        oMenu.SaveSettingBus("BSVEPACK", "RECENT", ".", "PrelevaDati", "N", " ", "NS.", "...", "...")
      End If

      TipoRilevaMisure()
      oMenu.SaveSettingBus("BSVEPACK", "RECENT", ".", "RilevaMisVol", oClePack.strTipoRilevaMisure, " ", "NS.", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEPACK_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTestPac.Dispose()
      dcMovPac.Dispose()
      dsPack.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edTp_mmnumdoc.NTSDbField = "testpac.tp_mmnumdoc"
      edTp_mmserie.NTSDbField = "testpac.tp_mmserie"
      edTp_mmtipork.NTSDbField = "testpac.tp_mmtipork"
      edTp_mmanno.NTSDbField = "testpac.tp_mmanno"
      edTp_riferim.NTSDbField = "testpac.tp_riferim"
      edTp_conto.NTSDbField = "testpac.tp_conto"
      edTp_datdoc.NTSDbField = "testpac.tp_datdoc"
      ckTp_sblocca.NTSText.NTSDbField = "testpac.tp_sblocca"
      edTp_totvolume.NTSDbField = "testpac.tp_totvolume"
      edTp_peso.NTSDbField = "testpac.tp_peso"
      edTp_pesonetto.NTSDbField = "testpac.tp_pesonetto"
      edTp_totcoll.NTSDbField = "testpac.tp_totcoll"
      edTp_coddest2.NTSDbField = "testpac.tp_coddest2"
      edTp_coddest.NTSDbField = "testpac.tp_coddest"
      edTp_codvett2.NTSDbField = "testpac.tp_codvett2"
      edTp_codvett.NTSDbField = "testpac.tp_codvett"
      edTp_note.NTSDbField = "testpac.tp_note"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcTestPac, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      Nuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      Apri()

      grvPack.LeftCoord = 0
      If xx_selez.Visible Then grvPack.FocusedColumn = xx_selez

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
    Dim dlgRes As DialogResult
    Try
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128605257529062500, "Confermare la cancellazione del record?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If Not bNuovo Then
            If dtUltAgg <> oClePack.ControllaUltAgg(lAnnoPack, strSeriePack, lNumPack) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128708082764092911, "La Packing List che si cerca di cancellare è stata modificata da un altro utente o sessione. E' possibile solo ripristinare."))
              Return
            End If

            oClePack.CancellaPac(dsPack.Tables("TESTPAC"))
          End If

          dsPack = Nothing
          dttMovPac = Nothing

          oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
          oClePack.Ripristina(dcTestPac.Position, dcTestPac.Filter)

          lbDescrizione.Text = ""
          lbDescVet.Text = ""
          lbDescvett2.Text = ""
          lbDescdest1.Text = ""
          lbDescdest2.Text = ""
          lbDesctipo.Text = ""

          dttMovPac = New DataTable
          dsPack = New DataSet
          dcMovPac = New BindingSource
          dcTestPac = New BindingSource


          NascondiControlli()
      End Select
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcTestPac, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      Ripristina()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB

    Try
      If grvPack.FocusedColumn.Equals(xx_lottox) Then
        '------------------------------------
        'zoom lotti
        If NTSCStr(grvPack.GetRowCellValue(grvPack.FocusedRowHandle, mp_codart)).Trim = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128569627222968750, "Indicare prima il codice articolo"))
          Return
        End If
        NTSZOOM.strIn = NTSCStr(grvPack.EditingValue)
        oParam.strTipo = NTSCStr(grvPack.GetRowCellValue(grvPack.FocusedRowHandle, mp_codart))
        'oParam.nMagaz = NTSCInt(grvPack.GetRowCellValue(grvPack.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
        oParam.nAnno = lAnnoPack                                                                'serve per visual solo i lotti aperti
        'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                        'serve per visual solo i lotti aperti
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvPack.EditingValue) Then grvPack.SetFocusedValue(NTSZOOM.strIn)
      ElseIf edTp_coddest.Focused Or edTp_coddest2.Focused Then

        If edTp_coddest.Focused Then
          SetFastZoom(edTp_coddest.Text, oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = edTp_coddest.Text
        Else
          SetFastZoom(edTp_coddest2.Text, oParam)
          NTSZOOM.strIn = edTp_coddest2.Text
        End If

        oParam.lContoCF = NTSCInt(edTp_conto.Text)   'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)

        If edTp_coddest.Focused Then
          If NTSZOOM.strIn <> edTp_coddest.Text Then edTp_coddest.NTSTextDB = NTSZOOM.strIn
        Else
          If NTSZOOM.strIn <> edTp_coddest2.Text Then edTp_coddest2.NTSTextDB = NTSZOOM.strIn
        End If

      Else
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecAggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecAggiorna.ItemClick
    Try
      SalvaMovpac()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecNuovo.ItemClick
    Try
      grvPack.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecCancella.ItemClick
    Dim lCount As Integer
    Try
      If grvPack.NTSGetCurrentDataRow Is Nothing Then Return

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128710447775337910, "Eliminare la riga corrente?")) = Windows.Forms.DialogResult.Yes Then
        lCount = grvPack.RowCount
        grvPack.NTSDeleteRigaCorrente(dcMovPac, False)
        dsPack.Tables("MOVPAC").AcceptChanges()
        If lCount = grvPack.RowCount Then ' se non è riuscito a cancellarla la ripristino.
          oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
          'grvPack.NTSRipristinaRigaCorrenteAfter()
        End If
      End If

      grvPack.LeftCoord = 0
      If xx_selez.Visible Then grvPack.FocusedColumn = xx_selez

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecRipristina.ItemClick
    Dim dtrRow(), dtrSelRow As DataRow
    Dim i As Integer
    Try
      If grvPack.NTSGetCurrentDataRow() Is Nothing Then Return

      dtrSelRow = grvPack.NTSGetCurrentDataRow()
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128695949938250391, "Ripristinare la riga corrente?")) = Windows.Forms.DialogResult.Yes Then
        If dttMovPacTmp.Columns.Contains("mp_riga") Then
          dtrRow = dttMovPacTmp.Select("mp_riga = " & NTSCDec(dtrSelRow!mp_riga))

          If dtrRow.Length = 0 Then
            oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
            'grvPack.NTSRipristinaRigaCorrenteAfter()
          Else
            For i = 0 To dtrRow(0).ItemArray.Length - 1
              dtrSelRow(i) = dtrRow(0)(i)
            Next
          End If
        Else
          oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
        End If
      End If

      'If grvPack.NTSRipristinaRigaCorrenteBefore(dcMovPac, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
      '  oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
      '  grvPack.NTSRipristinaRigaCorrenteAfter()
      'End If
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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDaDocu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaDocu.ItemClick
    Try
      If tlbDaDocu.Down Then
        tlbDaOrdine.Down = False
        tlbDaPacking.Down = False
        tlbDaNiente.Down = False
      Else
        tlbDaDocu.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDaOrdine_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaOrdine.ItemClick
    Try
      If tlbDaOrdine.Down Then
        tlbDaDocu.Down = False
        tlbDaPacking.Down = False
        tlbDaNiente.Down = False
      Else
        tlbDaOrdine.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDaPacking_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaPacking.ItemClick
    Try
      If tlbDaPacking.Down Then
        tlbDaOrdine.Down = False
        tlbDaDocu.Down = False
        tlbDaNiente.Down = False
      Else
        tlbDaPacking.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDaNiente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaNiente.ItemClick
    Try
      If tlbDaNiente.Down Then
        tlbDaOrdine.Down = False
        tlbDaPacking.Down = False
        tlbDaDocu.Down = False
      Else
        tlbDaNiente.Down = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSeleziona_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSeleziona.ItemClick
    Try
      SelDeselTutto("S")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDeseleziona_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeseleziona.ItemClick
    Try
      SelDeselTutto("N")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStp.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDividi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDividi.ItemClick
    Dim dtrRow() As DataRow
    Dim dttArtico As New DataTable
    Try
      oClePack.bInDividi = True
      '--------------------------------------------------------------------------------------------------------------
      'Controlla che la gestione filtri sia disabilitata
      If grvPack.NTSGetColumnFilterStatus = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129593435103261717, "Attenzione!" & vbCrLf & _
                  "Disabilita i Sort/Filter sulla grigia per proseguire."))
        Return
      End If

      dtrRow = dsPack.Tables("MOVPAC").Select("xx_selez = 'S'")
      If dtrRow.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129593435103261719, "Attenzione!" & vbCrLf & _
          "Selezionare almeno una riga da dividere."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oClePack.Dividi()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      oClePack.bInDividi = False
    End Try
  End Sub

  Public Overridable Sub tlbImposta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImposta.ItemClick
    Try
      Imposta()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGenera_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGenera.ItemClick
    Try
      Genera()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1, "L")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      ' If Not Salva() Then Return

      Stampa(0, "L")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaPDF_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPDF.ItemClick
    Try
      StampaPdf()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrnEti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrnEti.ItemClick
    Try
      Stampa(1, "E")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrnAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrnAll.ItemClick
    Try
      Stampa(1, "A")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrnscreenEti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrnscreenEti.ItemClick
    Try
      Stampa(0, "E")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrnscreenAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrnscreenAll.ItemClick
    Try
      Stampa(0, "A")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim dtrRow() As DataRow
    Dim lValue, lCod, i As Integer
    Dim strNumPack As String
    Try
      'Seleziono le righe da duplicare
      dtrRow = dsPack.Tables("MOVPAC").Select("xx_selez = 'S'")

      If dtrRow.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129240218991406250, "Selezionare almeno una riga da duplicare in un nuovo pacco."))
        Return
      End If

      'Chiedo quante volte duplicare il pacco
      lValue = NTSCInt(oApp.InputBoxNew(oApp.Tr(Me, 129240219704218750, "Selezionare il numero di pacchi da creare"), "1", "N", False))

      If lValue <= 0 Then Return

      Dim lMax As Decimal = NTSCDec(dsPack.Tables("MOVPAC").Compute("MAX(mp_riga)", ""))
      'Duplico le righe il numero di volte richiesto.
      For z As Integer = 0 To lValue - 1
        'Considero un nuovo pacco generato
        If Not oClePack.NumaImpa(lCod) Then Return
        strNumPack = strPrefissoNumeroPacco & NTSCStr(lCod).PadLeft(9).Replace(" ", "0")

        For i = 0 To dtrRow.Length - 1
          With dsPack.Tables("MOVPAC")
            .Rows.Add(dtrRow(i).ItemArray)

            'Riassegno il numero di riga
            lMax += lIncremContatoreRiga
            .Rows(.Rows.Count - 1)!mp_riga = lMax

            'Assegno il nuovo numero del pacco generato
            .Rows(.Rows.Count - 1)!mp_numpack = strNumPack

            'Tolgo la spunta di selezionato
            .Rows(.Rows.Count - 1)!xx_selez = "N"
          End With
        Next
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbMisurea1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMisurea1.ItemClick
    Try
      tlbRilevaDaDoc.Down = False
      tlbRileva.Down = False
      tlbNonRilevare.Down = False
      tlbMisurea1.Down = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbRileva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRileva.ItemClick
    Try
      tlbRilevaDaDoc.Down = False
      tlbRileva.Down = True
      tlbNonRilevare.Down = False
      tlbMisurea1.Down = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbRilevaDaDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRilevaDaDoc.ItemClick
    Try
      tlbRilevaDaDoc.Down = True
      tlbRileva.Down = False
      tlbNonRilevare.Down = False
      tlbMisurea1.Down = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbNonRilevare_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNonRilevare.ItemClick
    Try
      tlbRilevaDaDoc.Down = False
      tlbRileva.Down = False
      tlbNonRilevare.Down = True
      tlbMisurea1.Down = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiRighe.ItemClick
    Try
      oClePack.bAggiungiRighe = True
      Aggiungi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oClePack.bAggiungiRighe = False
    End Try
  End Sub
#End Region

  Public Overridable Sub SelDeselTutto(ByVal strValore As String)
    Try
      For Each dtrRow As DataRow In dsPack.Tables("MOVPAC").Rows
        Dim oState As DataRowState = dtrRow.RowState
        dtrRow!xx_selez = strValore
        dtrRow.AcceptChanges()
        Select Case oState
          Case DataRowState.Added : dtrRow.SetAdded()
          Case DataRowState.Modified : dtrRow.SetModified()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Nuovo()
    Dim dttTmp As New DataTable
    Dim bEredita As Boolean

    Dim frmSepa As FRMVESEPA = Nothing
    Dim frmSebo As FRMVESEB1 = Nothing
    Dim frmPror As FRMVEPROR = Nothing
    Dim frmPrelevaSepa As FRMVESEPA = Nothing
    Try
      bNuovo = True

      frmSepa = CType(NTSNewFormModal("FRMVESEPA"), FRMVESEPA)
      If Not frmSepa.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSepa.InitEntity(oClePack)
      frmSepa.strTipoForm = "N"
      frmSepa.ShowDialog(Me)

      If frmSepa.bOk Then

        lNumPack = frmSepa.lNumero
        strSeriePack = frmSepa.strSerie
        lAnnoPack = frmSepa.lAnno
        dtDataPack = Now.Date

        If tlbDaDocu.Down Then
          frmSebo = CType(NTSNewFormModal("FRMVESEB1"), FRMVESEB1)
          If Not frmSebo.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
          frmSebo.InitEntity(oClePack)
          frmSebo.ShowDialog(Me)

          If frmSebo.bOk Then
            If Not oClePack.CreaMovPac(dttMovPac) Then Return

            TipoRilevaMisure()
            If Not oClePack.CaricaDaDocu(frmSebo.strTipo, frmSebo.lAnno, frmSebo.strSerie, frmSebo.lNumero, lOrdinamentoNuoveRighe, _
                                     lIncremContatoreRiga, tlbRileva.Down, dttTmp, lAnnoPack, strSeriePack, lNumPack, dttMovPac) Then Return

            MostraControlli()

            dcMovPac = New BindingSource
            dcTestPac = New BindingSource
            dsPack = New DataSet

            dsPack.Tables.Add(dttMovPac.Copy)
            '-------------------------------------------------
            'leggo dal database i dati e collego il NTSBinding
            '-------------------------------------------------
            If Not oClePack.ApriNuovoPac(DittaCorrente, dsPack) Then Return
            dcMovPac.DataSource = dsPack.Tables("MOVPAC")
            dcTestPac.DataSource = dsPack.Tables("TESTPAC")
            dsPack.Tables("TESTPAC").Rows.Add()
            dsPack.Tables("TESTPAC").Rows(0)!tp_anno = lAnnoPack
            dsPack.Tables("TESTPAC").Rows(0)!tp_serie = strSeriePack
            dsPack.Tables("TESTPAC").Rows(0)!tp_numdoc = lNumPack

            Bindcontrols()


            With dttTmp.Rows(0)
              edTp_conto.NTSTextDB = NTSCStr(!tm_conto)
              edTp_codvett.NTSTextDB = NTSCStr(!tm_vettor)
              edTp_codvett2.NTSTextDB = NTSCStr(!tm_vettor2)
              edTp_riferim.NTSTextDB = NTSCStr(!tm_riferim)
              edTp_coddest.NTSTextDB = NTSCStr(!tm_coddest)
              edTp_coddest2.NTSTextDB = NTSCStr(!tm_coddest2)
              edTp_note.NTSTextDB = NTSCStr(!tm_note)
              edTp_datdoc.NTSTextDB = dtDataPack.ToShortDateString
            End With

            edTp_mmanno.NTSTextDB = NTSCStr(frmSebo.lAnno)
            edTp_mmtipork.NTSTextDB = frmSebo.strTipo
            edTp_mmserie.NTSTextDB = frmSebo.strSerie
            edTp_mmnumdoc.NTSTextDB = NTSCStr(frmSebo.lNumero)

            'passo i dati all'entity
            oClePack.lNumPack = lNumPack
            oClePack.lAnnoPack = lAnnoPack
            oClePack.strSeriePack = strSeriePack
            oClePack.dtDataPack = dtDataPack
            oClePack.strTp_mmtipork = frmSebo.strTipo
            oClePack.nTp_mmanno = frmSebo.lAnno
            oClePack.strTp_mmserie = frmSebo.strSerie
            oClePack.lTp_mmnumdoc = frmSebo.lNumero

            CreaTitolo()

            DecodificaTipo()

            grPack.DataSource = dcMovPac
            '-------------------------------------------------
            'imposto i valori di default come impostato nella GCTL
            Me.GctlApplicaDefaultValue()

            SalvaMovpac()
          End If
        ElseIf tlbDaOrdine.Down Then
          frmPror = CType(NTSNewFormModal("FRMVEPROR"), FRMVEPROR)
          If Not frmPror.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
          frmPror.InitEntity(oClePack)
          frmPror.ShowDialog(Me)

          If frmPror.bOk Then
            If Not oClePack.CreaMovPac(dttMovPac) Then Return

            TipoRilevaMisure()
            If Not oClePack.CaricaDaOrdine(frmPror.strTipo, frmPror.lAnno, frmPror.strSerie, frmPror.lNumero, tlbRileva.Down, _
                                       bOrdMatrMultiRow, lIncremContatoreRiga, lAnnoPack, strSeriePack, lNumPack, dttMovPac, dttTmp) Then Return

            MostraControlli()

            dcMovPac = New BindingSource
            dcTestPac = New BindingSource
            dsPack = New DataSet

            dsPack.Tables.Add(dttMovPac.Copy)
            '-------------------------------------------------
            'leggo dal database i dati e collego il NTSBinding
            '-------------------------------------------------
            If Not oClePack.ApriNuovoPac(DittaCorrente, dsPack) Then Return
            dcMovPac.DataSource = dsPack.Tables("MOVPAC")
            dcTestPac.DataSource = dsPack.Tables("TESTPAC")
            dsPack.Tables("TESTPAC").Rows.Add()
            dsPack.Tables("TESTPAC").Rows(0)!tp_anno = lAnnoPack
            dsPack.Tables("TESTPAC").Rows(0)!tp_serie = strSeriePack
            dsPack.Tables("TESTPAC").Rows(0)!tp_numdoc = lNumPack

            Bindcontrols()

            With dttTmp.Rows(0)
              edTp_conto.NTSTextDB = NTSCStr(!td_conto)
              edTp_codvett.NTSTextDB = NTSCStr(!td_vettor)
              edTp_codvett2.NTSTextDB = NTSCStr(!td_vettor2)
              edTp_riferim.NTSTextDB = NTSCStr(!td_riferim)
              edTp_datdoc.NTSTextDB = dtDataPack.ToShortDateString
              edTp_coddest.NTSTextDB = NTSCStr(!td_coddest)
              edTp_coddest2.NTSTextDB = NTSCStr(!td_coddest2)
              edTp_note.NTSTextDB = NTSCStr(!td_note)
            End With

            edTp_mmanno.NTSTextDB = NTSCStr(frmPror.lAnno)
            edTp_mmtipork.NTSTextDB = frmPror.strTipo
            edTp_mmserie.NTSTextDB = frmPror.strSerie
            edTp_mmnumdoc.NTSTextDB = NTSCStr(frmPror.lNumero)

            'passo i dati all'entity
            oClePack.lNumPack = lNumPack
            oClePack.lAnnoPack = lAnnoPack
            oClePack.strSeriePack = strSeriePack
            oClePack.dtDataPack = dtDataPack
            oClePack.strTp_mmtipork = frmPror.strTipo
            oClePack.nTp_mmanno = frmPror.lAnno
            oClePack.strTp_mmserie = frmPror.strSerie
            oClePack.lTp_mmnumdoc = frmPror.lNumero

            CreaTitolo()

            DecodificaTipo()

            grPack.DataSource = dcMovPac
            '-------------------------------------------------
            'imposto i valori di default come impostato nella GCTL
            Me.GctlApplicaDefaultValue()

            SalvaMovpac()
          End If
        ElseIf tlbDaPacking.Down Then
          frmPrelevaSepa = CType(NTSNewFormModal("FRMVESEPA"), FRMVESEPA)
          If Not frmPrelevaSepa.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
          frmPrelevaSepa.InitEntity(oClePack)
          frmPrelevaSepa.strTipoForm = "D"
          frmPrelevaSepa.ShowDialog(Me)

          If frmPrelevaSepa.bOk Then
            If Not oClePack.CreaMovPac(dttMovPac) Then Return

            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130548211552751666, "Ereditare nella nuova packing list anche codice/descrizione pacco dalla precedente?")) = Windows.Forms.DialogResult.Yes Then
              bEredita = True
            Else
              bEredita = False
            End If

            TipoRilevaMisure()
            If Not oClePack.CaricaDaPacking(frmPrelevaSepa.lAnno, frmPrelevaSepa.strSerie, frmPrelevaSepa.lNumero, bEredita, lIncremContatoreRiga, tlbRileva.Down, dttTmp, _
                                       lAnnoPack, strSeriePack, lNumPack, dttMovPac) Then Return

            MostraControlli()

            DecodificaTipo()

            dcMovPac = New BindingSource
            dcTestPac = New BindingSource
            dsPack = New DataSet

            dsPack.Tables.Add(dttMovPac.Copy)
            '-------------------------------------------------
            'leggo dal database i dati e collego il NTSBinding
            '-------------------------------------------------
            If Not oClePack.ApriNuovoPac(DittaCorrente, dsPack) Then Return
            dcMovPac.DataSource = dsPack.Tables("MOVPAC")
            dcTestPac.DataSource = dsPack.Tables("TESTPAC")
            dsPack.Tables("TESTPAC").Rows.Add()
            dsPack.Tables("TESTPAC").Rows(0)!tp_anno = lAnnoPack
            dsPack.Tables("TESTPAC").Rows(0)!tp_serie = strSeriePack
            dsPack.Tables("TESTPAC").Rows(0)!tp_numdoc = lNumPack

            Bindcontrols()

            With dttTmp.Rows(0)
              edTp_conto.NTSTextDB = NTSCStr(!tp_conto)
              edTp_codvett.NTSTextDB = NTSCStr(!tp_codvett)
              edTp_codvett2.NTSTextDB = NTSCStr(!tp_codvett2)
              edTp_riferim.NTSTextDB = NTSCStr(!tp_riferim)
              edTp_datdoc.NTSTextDB = dtDataPack.ToShortDateString
              edTp_coddest.NTSTextDB = NTSCStr(!tp_coddest)
              edTp_coddest2.NTSTextDB = NTSCStr(!tp_coddest2)
              edTp_note.NTSTextDB = NTSCStr(!tp_note)
              edTp_mmanno.NTSTextDB = NTSCStr(!tp_anno)
              edTp_mmnumdoc.NTSTextDB = NTSCStr(!tp_numdoc)
              edTp_mmtipork.NTSTextDB = " "
              edTp_mmserie.NTSTextDB = NTSCStr(!tp_serie)
            End With


            'passo i dati all'entity
            oClePack.lNumPack = lNumPack
            oClePack.lAnnoPack = lAnnoPack
            oClePack.strSeriePack = strSeriePack
            oClePack.dtDataPack = dtDataPack
            oClePack.strTp_mmtipork = " "
            oClePack.nTp_mmanno = frmPrelevaSepa.lAnno
            oClePack.strTp_mmserie = frmPrelevaSepa.strSerie
            oClePack.lTp_mmnumdoc = frmPrelevaSepa.lNumero

            CreaTitolo()

            grPack.DataSource = dcMovPac
            '-------------------------------------------------
            'imposto i valori di default come impostato nella GCTL
            Me.GctlApplicaDefaultValue()

            SalvaMovpac()
          End If
        ElseIf tlbDaNiente.Down Then
          If Not oClePack.CreaMovPac(dttMovPac) Then Return

          MostraControlli()

          TipoRilevaMisure()

          dcMovPac = New BindingSource
          dcTestPac = New BindingSource
          dsPack = New DataSet

          dsPack.Tables.Add(dttMovPac.Copy)
          '-------------------------------------------------
          'leggo dal database i dati e collego il NTSBinding
          '-------------------------------------------------
          If Not oClePack.ApriNuovoPac(DittaCorrente, dsPack) Then Return
          dcMovPac.DataSource = dsPack.Tables("MOVPAC")
          dcTestPac.DataSource = dsPack.Tables("TESTPAC")
          dsPack.Tables("TESTPAC").Rows.Add()
          dsPack.Tables("TESTPAC").Rows(0)!tp_anno = lAnnoPack
          dsPack.Tables("TESTPAC").Rows(0)!tp_serie = strSeriePack
          dsPack.Tables("TESTPAC").Rows(0)!tp_numdoc = lNumPack

          Bindcontrols()

          edTp_conto.NTSTextDB = "0"
          edTp_codvett.NTSTextDB = "0"
          edTp_codvett2.NTSTextDB = "0"
          edTp_riferim.NTSTextDB = ""
          edTp_datdoc.NTSTextDB = dtDataPack.ToShortDateString
          edTp_coddest.NTSTextDB = "0"
          edTp_coddest2.NTSTextDB = "0"
          edTp_note.NTSTextDB = ""
          edTp_mmnumdoc.NTSTextDB = "0"
          edTp_mmtipork.NTSTextDB = " "
          edTp_mmserie.NTSTextDB = " "
          lbDesctipo.Text = ""

          'passo i dati all'entity
          oClePack.lNumPack = lNumPack
          oClePack.lAnnoPack = lAnnoPack
          oClePack.strSeriePack = strSeriePack
          oClePack.dtDataPack = dtDataPack
          oClePack.strTp_mmtipork = " "
          oClePack.nTp_mmanno = 0
          oClePack.strTp_mmserie = " "
          oClePack.lTp_mmnumdoc = 0

          CreaTitolo()

          grPack.DataSource = dcMovPac
          '-------------------------------------------------
          'imposto i valori di default come impostato nella GCTL
          Me.GctlApplicaDefaultValue()

          SalvaMovpac()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSepa Is Nothing Then frmSepa.Dispose()
      frmSepa = Nothing
      If Not frmSebo Is Nothing Then frmSebo.Dispose()
      frmSebo = Nothing
      If Not frmPror Is Nothing Then frmPror.Dispose()
      frmPror = Nothing
      If Not frmPrelevaSepa Is Nothing Then frmPrelevaSepa.Dispose()
      frmPrelevaSepa = Nothing
    End Try
  End Sub
  Public Overridable Sub Apri()
    Dim strContoNew As String = ""
    Dim frmSepa As FRMVESEPA = Nothing
    Try
      bNuovo = False

      frmSepa = CType(NTSNewFormModal("FRMVESEPA"), FRMVESEPA)
      If Not frmSepa.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSepa.InitEntity(oClePack)
      frmSepa.strTipoForm = "A"
      frmSepa.ShowDialog(Me)

      lAnnoPack = frmSepa.lAnno
      strSeriePack = frmSepa.strSerie
      lNumPack = frmSepa.lNumero

      If frmSepa.bOk Then
        MostraControlli()

        If Not oClePack.ApriPac(DittaCorrente, lAnnoPack, strSeriePack, lNumPack, dsPack) Then Return

        dcMovPac.DataSource = dsPack.Tables("MOVPAC")
        dcTestPac.DataSource = dsPack.Tables("TESTPAC")

        Bindcontrols()

        grPack.DataSource = dcMovPac

        CreaTitolo()
        DecodificaTipo()

        oClePack.ComponiDescrizione(NTSCInt(edTp_conto.Text), lbDescrizione.Text, lCodling)

        dsPack.AcceptChanges()

        dtUltAgg = oClePack.ControllaUltAgg(lAnnoPack, strSeriePack, lNumPack)

        SalvaMovpac()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSepa Is Nothing Then frmSepa.Dispose()
      frmSepa = Nothing
    End Try
  End Sub
  Public Overridable Function Ripristina() As Boolean
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco
      '-------------------------------------------------
      'ripristino
      Dim dlgRes As DialogResult
      'chiamato facendo pressione sulla funzione 'ripristina'
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128606880861093750, "Ripristinare le modifiche apportate al record?"))

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return False
        Case Windows.Forms.DialogResult.Yes

          oClePack.RipristinaMov(dcMovPac.Position, dcMovPac.Filter)
          oClePack.Ripristina(dcTestPac.Position, dcTestPac.Filter)

          lbDescrizione.Text = ""
          lbDescVet.Text = ""
          lbDescvett2.Text = ""
          lbDescdest1.Text = ""
          lbDescdest2.Text = ""
          lbDesctipo.Text = ""

          dttMovPac = New DataTable
          dsPack = New DataSet
          dcMovPac = New BindingSource
          dcTestPac = New BindingSource

          NascondiControlli()
      End Select

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Sub Aggiungi()
    Dim dttTmp As New DataTable
    Dim bEredita As Boolean

    Dim frmSepa As FRMVESEPA = Nothing
    Dim frmSebo As FRMVESEB1 = Nothing
    Dim frmPror As FRMVEPROR = Nothing
    Dim frmPrelevaSepa As FRMVESEPA = Nothing

    Dim i As Integer
    Try
      If Not oClePack.bAggiungiRighe Then bNuovo = True

      If tlbDaDocu.Down Then
        frmSebo = CType(NTSNewFormModal("FRMVESEB1"), FRMVESEB1)
        If Not frmSebo.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
        frmSebo.InitEntity(oClePack)
        frmSebo.ShowDialog(Me)

        If frmSebo.bOk Then
          If Not oClePack.CreaMovPac(dttMovPac) Then Return

          TipoRilevaMisure()
          If Not oClePack.CaricaDaDocu(frmSebo.strTipo, frmSebo.lAnno, frmSebo.strSerie, frmSebo.lNumero, lOrdinamentoNuoveRighe, _
                                   lIncremContatoreRiga, tlbRileva.Down, dttTmp, lAnnoPack, strSeriePack, lNumPack, dttMovPac) Then Return

          For i = 0 To dttMovPac.Rows.Count - 1
            dsPack.Tables("MOVPAC").ImportRow(dttMovPac.Rows(i))
          Next

          grPack.DataSource = dcMovPac
          '-------------------------------------------------
          'imposto i valori di default come impostato nella GCTL
          Me.GctlApplicaDefaultValue()

          SalvaMovpac()
        End If
      ElseIf tlbDaOrdine.Down Then
        frmPror = CType(NTSNewFormModal("FRMVEPROR"), FRMVEPROR)
        If Not frmPror.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
        frmPror.InitEntity(oClePack)
        frmPror.ShowDialog(Me)

        If frmPror.bOk Then
          If Not oClePack.CreaMovPac(dttMovPac) Then Return

          TipoRilevaMisure()
          If Not oClePack.CaricaDaOrdine(frmPror.strTipo, frmPror.lAnno, frmPror.strSerie, frmPror.lNumero, tlbRileva.Down, _
                                     bOrdMatrMultiRow, lIncremContatoreRiga, lAnnoPack, strSeriePack, lNumPack, dttMovPac, dttTmp) Then Return

          For i = 0 To dttMovPac.Rows.Count - 1
            dsPack.Tables("MOVPAC").ImportRow(dttMovPac.Rows(i))
          Next

          grPack.DataSource = dcMovPac
          '-------------------------------------------------
          'imposto i valori di default come impostato nella GCTL
          Me.GctlApplicaDefaultValue()

          SalvaMovpac()
        End If
      ElseIf tlbDaPacking.Down Then
        frmPrelevaSepa = CType(NTSNewFormModal("FRMVESEPA"), FRMVESEPA)
        If Not frmPrelevaSepa.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
        frmPrelevaSepa.InitEntity(oClePack)
        frmPrelevaSepa.strTipoForm = "D"
        frmPrelevaSepa.ShowDialog(Me)

        If frmPrelevaSepa.bOk Then
          If Not oClePack.CreaMovPac(dttMovPac) Then Return

          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128698582474314827, "Ereditare nella nuova packing list anche codice/descrizione pacco dalla precedente?")) = Windows.Forms.DialogResult.Yes Then
            bEredita = True
          Else
            bEredita = False
          End If

          TipoRilevaMisure()
          If Not oClePack.CaricaDaPacking(frmPrelevaSepa.lAnno, frmPrelevaSepa.strSerie, frmPrelevaSepa.lNumero, bEredita, lIncremContatoreRiga, tlbRileva.Down, dttTmp, _
                                     lAnnoPack, strSeriePack, lNumPack, dttMovPac) Then Return

          For i = 0 To dttMovPac.Rows.Count - 1
            dsPack.Tables("MOVPAC").ImportRow(dttMovPac.Rows(i))
          Next

          grPack.DataSource = dcMovPac
          '-------------------------------------------------
          'imposto i valori di default come impostato nella GCTL
          Me.GctlApplicaDefaultValue()

          SalvaMovpac()
        End If
      ElseIf tlbDaNiente.Down Then
        If Not oClePack.CreaMovPac(dttMovPac) Then Return

        For i = 0 To dttMovPac.Rows.Count - 1
          dsPack.Tables("MOVPAC").ImportRow(dttMovPac.Rows(i))
        Next

        grPack.DataSource = dcMovPac
        '-------------------------------------------------
        'imposto i valori di default come impostato nella GCTL
        Me.GctlApplicaDefaultValue()

        SalvaMovpac()

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSepa Is Nothing Then frmSepa.Dispose()
      frmSepa = Nothing
      If Not frmSebo Is Nothing Then frmSebo.Dispose()
      frmSebo = Nothing
      If Not frmPror Is Nothing Then frmPror.Dispose()
      frmPror = Nothing
      If Not frmPrelevaSepa Is Nothing Then frmPrelevaSepa.Dispose()
      frmPrelevaSepa = Nothing
    End Try
  End Sub


  Public Overridable Function SalvaMovpac() As Boolean
    '   Dim dRes As DialogResult
    Dim bSalva As Boolean = False
    Try
      'non so come siano riusciti a farlo, ma facendo i raggruppamenti ingriglia viene inserita una riga vuota!!!
      'la cancello
      Dim dtrT1() As DataRow = dsPack.Tables("MOVPAC").Select("mp_riga = 0 and mp_codart = ' '")
      If dtrT1.Length > 0 Then
        dtrT1(0).Delete()
        dsPack.Tables("MOVPAC").AcceptChanges()
      End If

      For Each dtrT As DataRow In dsPack.Tables("MOVPAC").Rows
        If dtrT.RowState <> DataRowState.Deleted And dtrT.RowState <> DataRowState.Detached Then
          If NTSCStr(dtrT!mp_codart).Trim = "" And Not oClePack.bInDuplica Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129307424663773190, "Il codice articolo non è valido sulla riga '|" & NTSCDec(dtrT!mp_riga) & "|'."))
            Return False
          End If
        End If
      Next

      dttMovPacTmp = dttMovPac
      dttMovPacTmp.Clear()
      For Each dtrT As DataRow In dsPack.Tables("MOVPAC").Rows
        dttMovPacTmp.ImportRow(dtrT)
        dttMovPacTmp.AcceptChanges()
      Next

      ''-------------------------------------------------
      ''chiedo conferma e, se necessario, salvo

      'Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      'If oClePack.RecordIsChanged Or bNuovo Then
      '  '-------------------------------------------------
      '  'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      '  If GctlControllaOutNotEqual() = False Then Return False

      '  If bNuovo Then
      '    dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128794467408591685, "Salvo la packing list?"))
      '    If dRes = System.Windows.Forms.DialogResult.No Then Return False
      '    If dRes = System.Windows.Forms.DialogResult.Yes Then
      '      CalcolaTotali()
      '      If Not oClePack.SalvaTestPac(False, bNuovo) Then Return False
      '      bNuovo = False
      '      bSalva = True
      '    End If
      '  End If

      '  If bSalva Then
      '    If Not oClePack.SalvaMovpac(False) Then Return False
      '    dtUltAgg = oClePack.ControllaUltAgg(lAnnoPack, strSeriePack, lNumPack)
      '  End If
      'End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If GctlControllaOutNotEqual() = False Then Return False

      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128794467447185929, "Salvo la packing list?"))
      If dRes = System.Windows.Forms.DialogResult.No Then Return False
      If dRes = System.Windows.Forms.DialogResult.Yes Then
        CalcolaTotali()

        If Not oClePack.SalvaTestPac(False, bNuovo) Then Return False
        If Not oClePack.SalvaMovpac(False) Then Return False

        If bNuovo And oClePack.bNumChanged Then
          lNumPack = oClePack.lNumNuovo
          CreaTitolo()
        End If

        dtUltAgg = oClePack.ControllaUltAgg(lAnnoPack, strSeriePack, lNumPack)
        bNuovo = False
        GctlSetVisEnab(tlbCancella, False)

        If bChiudiAlSalvataggio Then
          lbDescrizione.Text = ""
          lbDescVet.Text = ""
          lbDescvett2.Text = ""
          lbDescdest1.Text = ""
          lbDescdest2.Text = ""
          lbDesctipo.Text = ""

          dttMovPac = New DataTable
          dsPack = New DataSet
          dcMovPac = New BindingSource
          dcTestPac = New BindingSource

          NascondiControlli()
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub NascondiControlli()
    Try
      ' Toolbar
      tlbNuovo.Enabled = True
      tlbApri.Enabled = True
      tlbDaOrdine.Enabled = True
      tlbDaDocu.Enabled = True
      tlbDaPacking.Enabled = True
      tlbDaNiente.Enabled = True
      tlbRilevaMisure.Enabled = True

      tlbSalva.Enabled = False
      tlbRipristina.Enabled = False
      tlbCancella.Enabled = False
      tlbZoom.Enabled = False
      tlbDividi.Enabled = False
      tlbGenera.Enabled = False
      tlbDuplica.Enabled = False
      tlbImposta.Enabled = False
      tlbStampa.Enabled = False
      tlbStampaVideo.Enabled = False
      tlbStampaPDF.Enabled = False
      tlbRecNuovo.Enabled = False
      tlbRecAggiorna.Enabled = False
      tlbRecCancella.Enabled = False
      tlbRecRipristina.Enabled = False
      tlbSeleziona.Enabled = False
      tlbDeseleziona.Enabled = False
      tlbPrnAll.Enabled = False
      tlbPrnEti.Enabled = False
      tlbPrnscreenAll.Enabled = False
      tlbPrnscreenEti.Enabled = False

      tlbAggiungiRighe.Enabled = False

      ' form
      pnPack.Visible = False
      pnTestata.Visible = False
      pnCorpo.Visible = False
      pnPiede.Visible = False
      pnNote.Visible = False
      lbTitolo.Visible = False
      lbClifor.Visible = False
      lbDescrizione.Visible = False
      lbDatdoc.Visible = False
      lbRiferimenti.Visible = False
      edTp_datdoc.Visible = False
      edTp_conto.Visible = False
      edTp_riferim.Visible = False
      lbAnno.Visible = False
      lbTipo.Visible = False
      lbSerie.Visible = False
      lbBarra.Visible = False
      edTp_mmanno.Visible = False
      edTp_mmtipork.Visible = False
      edTp_mmserie.Visible = False
      edTp_mmnumdoc.Visible = False
      grPack.Visible = False
      edTp_note.Visible = False
      lbCodVet.Visible = False
      lbCodVett2.Visible = False
      lbDest1.Visible = False
      lbDest2.Visible = False
      edTp_codvett.Visible = False
      edTp_codvett2.Visible = False
      edTp_coddest.Visible = False
      edTp_coddest2.Visible = False
      lbDescVet.Visible = False
      lbDescvett2.Visible = False
      lbDescdest1.Visible = False
      lbDescdest2.Visible = False
      lbColli.Visible = False
      lbNetto.Visible = False
      lbLordo.Visible = False
      lbVolume.Visible = False
      edTp_totcoll.Visible = False
      edTp_peso.Visible = False
      edTp_pesonetto.Visible = False
      edTp_totvolume.Visible = False
      ckTp_sblocca.Visible = False

      grvPack.LeftCoord = 0
      If xx_selez.Visible Then grvPack.FocusedColumn = xx_selez

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub MostraControlli()
    Try
      oClePack.dMaxRiga = 0
      'toolbar
      tlbNuovo.Enabled = False
      tlbApri.Enabled = False
      tlbSalva.Enabled = True
      tlbDaOrdine.Enabled = False
      tlbDaDocu.Enabled = False
      tlbDaPacking.Enabled = False
      tlbDaNiente.Enabled = False
      tlbRilevaMisure.Enabled = False

      tlbRipristina.Enabled = True
      If Not bNuovo Then tlbCancella.Enabled = True
      tlbStampa.Enabled = True
      tlbStampaVideo.Enabled = True
      tlbStampaPDF.Enabled = True
      tlbZoom.Enabled = True
      tlbPrnAll.Enabled = True
      tlbPrnEti.Enabled = True
      tlbPrnscreenAll.Enabled = True
      tlbPrnscreenEti.Enabled = True

      tlbAggiungiRighe.Enabled = True

      ' form
      pnPack.Visible = True
      pnTestata.Visible = True
      pnCorpo.Visible = True
      pnPiede.Visible = True
      pnNote.Visible = True
      lbTitolo.Visible = True
      lbClifor.Visible = True
      lbDescrizione.Visible = True
      lbDatdoc.Visible = True
      lbRiferimenti.Visible = True
      edTp_datdoc.Visible = True
      edTp_conto.Visible = True
      edTp_riferim.Visible = True
      lbAnno.Visible = True
      lbTipo.Visible = True
      lbSerie.Visible = True
      lbBarra.Visible = True
      edTp_mmanno.Visible = True
      edTp_mmtipork.Visible = True
      edTp_mmserie.Visible = True
      edTp_mmnumdoc.Visible = True
      grPack.Visible = True
      edTp_note.Visible = True
      lbCodVet.Visible = True
      lbCodVett2.Visible = True
      lbDest1.Visible = True
      lbDest2.Visible = True
      edTp_codvett.Visible = True
      edTp_codvett2.Visible = True
      edTp_coddest.Visible = True
      edTp_coddest2.Visible = True
      lbDescVet.Visible = True
      lbDescvett2.Visible = True
      lbDescdest1.Visible = True
      lbDescdest2.Visible = True
      lbColli.Visible = True
      lbNetto.Visible = True
      lbLordo.Visible = True
      lbVolume.Visible = True
      edTp_totcoll.Visible = True
      edTp_peso.Visible = True
      edTp_pesonetto.Visible = True
      edTp_totvolume.Visible = True
      ckTp_sblocca.Visible = True

      tsPack.SelectedTabPageIndex() = 0
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub TipoRilevaMisure()
    Try
      Select Case True
        Case tlbRileva.Down : oClePack.strTipoRilevaMisure = "-1"
        Case tlbRilevaDaDoc.Down : oClePack.strTipoRilevaMisure = "1"
        Case tlbNonRilevare.Down : oClePack.strTipoRilevaMisure = "2"
        Case Else : oClePack.strTipoRilevaMisure = "0"
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "Eventi"
  Public Overridable Sub ckTp_sblocca_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTp_sblocca.CheckedChanged
    Try
      If ckTp_sblocca.Checked Then
        edTp_totvolume.Enabled = True
        edTp_peso.Enabled = True
        edTp_pesonetto.Enabled = True
        edTp_totcoll.Enabled = True
      Else
        CalcolaTotali()

        edTp_totvolume.Enabled = False
        edTp_peso.Enabled = False
        edTp_pesonetto.Enabled = False
        edTp_totcoll.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTp_codvett_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_codvett.Validated
    Try
      oClePack.LeggiDescr(edTp_codvett.Text, "TABVETT", lbDescVet.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTp_codvett2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_codvett2.Validated
    Try
      oClePack.LeggiDescr(edTp_codvett2.Text, "TABVETT", lbDescvett2.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTp_coddest_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_coddest.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oClePack.LeggiDescr(edTp_coddest.Text, "DESTDIV", strTmp, edTp_conto.Text) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692578274088411, "Attenzione!" & vbCrLf & _
          "Codice destinazione inesistente."))
        edTp_coddest.NTSTextDB = edTp_coddest.OldEditValue.ToString
      Else
        lbDescdest1.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edTp_coddest2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_coddest2.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oClePack.LeggiDescr(edTp_coddest2.Text, "DESTDIV", strTmp, edTp_conto.Text) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692588578482763, "Attenzione!" & vbCrLf & _
          "Codice seconda destinazione inesistente."))
        edTp_coddest2.NTSTextDB = edTp_coddest2.OldEditValue.ToString
      Else
        lbDescdest2.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edTp_conto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_conto.Validated
    Try
      If Not oClePack.ComponiDescrizione(NTSCInt(edTp_conto.Text), lbDescrizione.Text, lCodling) Then
        edTp_conto.Text = "0"
        lbDescrizione.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tsPack_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsPack.SelectedPageChanged
    Try
      If e.Page.Equals(tsPack.TabPages(1)) Then
        GctlSetVisEnab(tlbRecAggiorna, False)
        GctlSetVisEnab(tlbRecNuovo, False)
        GctlSetVisEnab(tlbRecCancella, False)
        GctlSetVisEnab(tlbRecRipristina, False)
        GctlSetVisEnab(tlbDividi, False)
        GctlSetVisEnab(tlbGenera, False)
        GctlSetVisEnab(tlbDuplica, False)
        GctlSetVisEnab(tlbImposta, False)
        GctlSetVisEnab(tlbSeleziona, False)
        GctlSetVisEnab(tlbDeseleziona, False)
      Else
        tlbRecCancella.Enabled = False
        tlbRecRipristina.Enabled = False
        tlbRecAggiorna.Enabled = False
        tlbRecNuovo.Enabled = False
        tlbDividi.Enabled = False
        tlbGenera.Enabled = False
        tlbDuplica.Enabled = False
        tlbImposta.Enabled = False
        tlbDeseleziona.Enabled = False
        tlbSeleziona.Enabled = False
      End If

      If e.Page.Equals(tsPack.TabPages(2)) Then CalcolaTotali()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub edTp_mmserie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTp_mmserie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edTp_mmserie.Text, False)
      If strTmp <> edTp_mmserie.Text Then edTp_mmserie.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
#End Region

#Region "Griglia"
  Public Overridable Sub grvPack_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvPack.NTSBeforeRowUpdate
    Try
      If Not SalvaMovpac() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvCena_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvPack.NTSFocusedRowChanged
    Try
      'Se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile

      GrvPack_RowColChange()

      If NTSCStr(grvPack.GetFocusedRowCellValue(mp_numpack)) = "" And grvPack.NTSGetCurrentDataRow Is Nothing Then
        grvPack.LeftCoord = 0
        grvPack.FocusedColumn = mp_numpack
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvPack_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvPack.NTSFocusedColumnChanged
    Try
      GrvPack_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function GrvPack_RowColChange() As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Dim strT() As String = Nothing
    Try

      If oClePack Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      If grvPack.NTSGetCurrentDataRow Is Nothing Then
        If Not mp_unmis.ColumnEdit Is Nothing Then CType(mp_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        mp_unmis.Enabled = False
        xx_selez.Enabled = False
      Else
        GctlSetVisEnab(mp_unmis, False)
        GctlSetVisEnab(xx_selez, False)
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not mp_unmis.ColumnEdit Is Nothing Then
        CType(mp_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvPack.FocusedColumn.Name = "mp_unmis" And grvPack.GetFocusedRowCellValue(mp_codart).ToString <> "" And NTSCStr(grvPack.GetFocusedRowCellValue(mp_codart)).Trim <> "" Then
          grvPack.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          grPack.Refresh()
          strTmp = oClePack.GetArticoUnMis(NTSCStr(grvPack.GetFocusedRowCellValue(mp_codart)))
          strT = strTmp.Split("§"c)
          dttTmp.Columns.Add("tb_codumis", GetType(String))
          For i As Integer = 0 To strT.Length - 1
            dttTmp.Rows.Add(New Object() {strT(i)})
          Next
          dttTmp.AcceptChanges()
          CType(mp_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
          'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
          'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
          dcTmp.DataSource = dttTmp
          For i As Integer = 0 To dcTmp.List.Count - 1
            If NTSCStr(CType(dcTmp.Current, DataRowView).Row!tb_codumis).ToUpper <> NTSCStr(grvPack.GetFocusedRowCellValue(mp_unmis)).ToUpper Then
              dcTmp.MoveNext()
            Else
              Exit For
            End If
          Next
          grvPack.liListCmb.DataSource = dcTmp
          grvPack.liListCmb.Visible = True
        End If
      End If    'If Not ec_unmis.ColumnEdit Is Nothing Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Function CalcolaRiga() As Decimal
    'non più utilizzata. spostata nell'entity
    Return 0
  End Function
  Public Overridable Sub CalcolaTotali()
    Dim i As Integer = 0
    Dim dPesoNetto As Decimal = 0
    Dim dPeso As Decimal = 0
    Dim dColli As Decimal = 0
    Dim dVolume As Decimal = 0
    Try
      If Not ckTp_sblocca.Checked Then
        For i = 0 To grvPack.RowCount - 1
          dPesoNetto += NTSCDec(grvPack.GetRowCellValue(i, mp_pesonetto))
          dPeso += NTSCDec(grvPack.GetRowCellValue(i, mp_peso))
          dColli += NTSCDec(grvPack.GetRowCellValue(i, mp_colli))
          dVolume += NTSCDec(grvPack.GetRowCellValue(i, mp_volume))
        Next

        edTp_pesonetto.NTSTextDB = NTSCStr(dPesoNetto)
        edTp_peso.NTSTextDB = NTSCStr(dPeso)
        edTp_totcoll.NTSTextDB = NTSCStr(dColli)
        edTp_totvolume.NTSTextDB = NTSCStr(dVolume)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub DecodificaTipo()
    Try
      Select Case edTp_mmtipork.Text
        ' Documenti
        Case "A" : lbDesctipo.Text = oApp.Tr(Me, 129080467085983630, "Fatture imm. emessa")
        Case "B" : lbDesctipo.Text = oApp.Tr(Me, 129080467108797006, "D.D.T. emesso")
        Case "C" : lbDesctipo.Text = oApp.Tr(Me, 129080467141767022, "Corrispettivo emesso")
        Case "D" : lbDesctipo.Text = oApp.Tr(Me, 129080467168955566, "Fatture diff. emesse")
        Case "E" : lbDesctipo.Text = oApp.Tr(Me, 129080467199581742, "Note di addeb. emessa")
        Case "F" : lbDesctipo.Text = oApp.Tr(Me, 129080467224270190, "Ric. fiscale emessa")
        Case "I" : lbDesctipo.Text = oApp.Tr(Me, 129080467248177358, "Riemissione ric. fiscale")
        Case "J" : lbDesctipo.Text = oApp.Tr(Me, 129080467272709550, "Nota accr. ricevuta")
        Case "L" : lbDesctipo.Text = oApp.Tr(Me, 129080467298648046, "Fattura imm. ricevuta")
        Case "M" : lbDesctipo.Text = oApp.Tr(Me, 129080467318492558, "D.D.T. ricevuto")
        Case "N" : lbDesctipo.Text = oApp.Tr(Me, 129080467339587118, "Nota accr. emessa")
        Case "S" : lbDesctipo.Text = oApp.Tr(Me, 129080467360837934, "Fat. ric. fisc. emessa")
        Case "T" : lbDesctipo.Text = oApp.Tr(Me, 129080467383338798, "Carico di produzione")
        Case "U" : lbDesctipo.Text = oApp.Tr(Me, 129080467405058382, "Scarico di produzione")
        Case "W" : lbDesctipo.Text = oApp.Tr(Me, 129080467429590574, "Nota di prelievo")
        Case "Z" : lbDesctipo.Text = oApp.Tr(Me, 129080467453966510, "Bolla di mov. interna")

          ' Ordini
        Case "R" : lbDesctipo.Text = oApp.Tr(Me, 129080467946172910, "Impegno cliente")
        Case "V" : lbDesctipo.Text = oApp.Tr(Me, 129080467978674158, "Imp. cli. aperto")
        Case "Q" : lbDesctipo.Text = oApp.Tr(Me, 129080467998987438, "Preventivo")
        Case "X" : lbDesctipo.Text = oApp.Tr(Me, 129080468018519438, "Imp. di trasferimento")
        Case "H" : lbDesctipo.Text = oApp.Tr(Me, 129080468038832718, "Ordine di produzione")
        Case Else
          If tlbDaPacking.Down Then
            lbDesctipo.Text = oApp.Tr(Me, 129080468061177326, "Packing List")
          Else
            lbDesctipo.Text = ""
          End If
      End Select

      oClePack.LeggiDescr(edTp_codvett.Text, "TABVETT", lbDescVet.Text)
      oClePack.LeggiDescr(edTp_codvett2.Text, "TABVETT", lbDescvett2.Text)
      oClePack.LeggiDescr(edTp_coddest.Text, "DESTDIV", lbDescdest1.Text, edTp_conto.Text)
      oClePack.LeggiDescr(edTp_coddest2.Text, "DESTDIV", lbDescdest2.Text, edTp_conto.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CreaTitolo()
    Dim strDesc As String = ""
    Try
      lbTitolo.Text = oApp.Tr(Me, 129080464440882062, "Packing list n° ") & NTSCStr(lNumPack)
      If strSeriePack <> " " Then
        lbTitolo.Text &= "/" & strSeriePack
      End If
      lbTitolo.Text &= " del " & edTp_datdoc.Text
      If edTp_conto.Text <> "0" Then
        oClePack.LeggiDescrizioneConto(NTSCInt(edTp_conto.Text), strDesc)
        lbTitolo.Text &= " - " & edTp_conto.Text & ": " & strDesc
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Imposta()
    Dim i As Integer = 0
    Dim x As Integer = 0
    Dim bTrovato As Boolean = False
    Dim strCodice As String = ""
    Dim strDesc As String = ""
    Dim frmImpa As FRMVEIMPA = Nothing
    Dim strIdPallet As String = ""

    Try
      For i = 0 To dsPack.Tables("MOVPAC").Rows.Count - 1
        If NTSCStr(dsPack.Tables("MOVPAC").Rows(i)!xx_selez) = "S" Then
          bTrovato = True
          Exit For
        End If
      Next

      If Not bTrovato Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128713936771280369, "Non sono state selezionate righe sulle quali impostare codice/descrizione pacco."))
        Return
      End If

      frmImpa = CType(NTSNewFormModal("FRMVEIMPA"), FRMVEIMPA)
      If Not frmImpa.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmImpa.InitEntity(oClePack)
      frmImpa.strPrefissoNumeroPacco = strPrefissoNumeroPacco
      frmImpa.ShowDialog(Me)

      If frmImpa.bOk Then
        strCodice = frmImpa.strCodicePacco
        strDesc = frmImpa.strDescPacco
        strIdPallet = frmImpa.strIdPallet

        For i = 0 To dsPack.Tables("MOVPAC").Rows.Count - 1
          If NTSCStr(dsPack.Tables("MOVPAC").Rows(i)!xx_selez) = "S" Then
            dsPack.Tables("MOVPAC").Rows(i)!mp_numpack = strCodice
            dsPack.Tables("MOVPAC").Rows(i)!mp_despack = strDesc
            dsPack.Tables("MOVPAC").Rows(i)!mp_idpallet = strIdPallet
          End If
        Next
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmImpa Is Nothing Then frmImpa.Dispose()
      frmImpa = Nothing
    End Try
  End Sub
  Public Overridable Sub Genera()
    Dim lCod As Integer = 0
    Try
      If Not oClePack.NumaImpa(lCod) Then Return

      grvPack.SetFocusedRowCellValue(mp_numpack, strPrefissoNumeroPacco & NTSCStr(lCod).PadLeft(9, "0"c))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer, ByVal strTipo As String)
    '------------------------------------------------------------
    'Se strTipo = E -> Stampa solo etichette, se = A stampa tutto, qualsiasi altro valore stampa solo la lista
    '------------------------------------------------------------
    Dim strNomeRpt As String = "BSVEPACK.RPT"
    Dim strTitleRpt As String = "Packing List"
    Dim strReports As String = "Reports1"
    Dim strCrpe As String
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer
    Dim j As Integer

    Try
      If Not Salva() Then Return

      ' Se = E stampa solo le etichette, quindi usa un report diverso
      If strTipo = "E" Then
        strNomeRpt = "BSVEPKET.RPT"
        strReports = "Reports2"
      End If

      '--------------------------------------------------
      'Preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEPACK", strReports, " ", 0, nDestin, strNomeRpt, False, strTitleRpt, False)
      If nPjob Is Nothing Then Return

      strCrpe = "{testpac.codditt} = '" & DittaCorrente & "'" & _
              " AND {testpac.tp_anno} = " & lAnnoPack & _
              " AND {testpac.tp_serie} = '" & strSeriePack & "'" & _
              " AND {testpac.tp_numdoc} = " & lNumPack & _
              " AND {movpac.mp_anno}= " & lAnnoPack & _
              " AND {movpac.mp_serie} = '" & strSeriePack & "'" & _
              " AND {movpac.mp_numdoc} = " & lNumPack

      '--------------------------------------------------
      'Lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        If i = 1 Then
          '-------------------------------------------------------------------------------------
          '--- Il primo report (quello standard)
          '-------------------------------------------------------------------------------------
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strCrpe)
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          '-------------------------------------------------------------------------------------
        Else
          '-------------------------------------------------------------------------------------
          '--- I report multipli
          '-------------------------------------------------------------------------------------
          If Trim(NTSCStr(CType(nPjob, Array).GetValue(2, i))) <> "" Then
            '--- RecordselectionFormula
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          End If
          For j = 3 To 12
            '--- FormulaJ
            If Trim(NTSCStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), NTSCStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(j + 10, i))))
            End If
          Next j
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          '-------------------------------------------------------------------------------------
        End If
      Next i
      '-----------------------------------------------------------------------------------------
      If strTipo = "A" Then
        strNomeRpt = "BSVEPKET.RPT"

        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEPACK", "Reports2", " ", 0, nDestin, strNomeRpt, False, "Packing List", False)
        If nPjob Is Nothing Then Return

        For i = 1 To UBound(CType(nPjob, Array), 2)
          If i = 1 Then
            '-----------------------------------------------------------------------------------
            '--- Il primo report (quello standard)
            '-----------------------------------------------------------------------------------
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strCrpe)
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            '-----------------------------------------------------------------------------------
          Else
            '-----------------------------------------------------------------------------------
            '--- I report multipli
            '-----------------------------------------------------------------------------------
            If Trim(NTSCStr(CType(nPjob, Array).GetValue(2, i))) <> "" Then
              '--- RecordselectionFormula
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            End If
            For j = 3 To 12
              '--- FormulaJ
              If Trim(NTSCStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), NTSCStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(j + 10, i))))
              End If
            Next j
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            '-----------------------------------------------------------------------------------
          End If
        Next i
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub StampaPdf()
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante

    '------------------------------
    'PER UN ESEMPIO COMPLETO VEDI BNORCONF: questo programma può stampare solo con un PDF per file, non un pdf per tutti i documenti
    '------------------------------

    Try
      If Not Salva() Then Return

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oClePack.GetQueryStampaPdf(lAnnoPack, strSeriePack, lNumPack)
      strQueryGetDocUnico = strQueryGetDocMultipli
      If strQueryGetDocMultipli = "" Or strQueryGetDocUnico = "" Then Return

      '--------------------------------
      'se devo passare delle formule lo faccio tramite questo datatable (per la 'PeSetFormula'
      'devo compilare o num, o str, o data a seconda del tipo di dato. 'name' deve sempre essere impostata
      dttFormule.Columns.Add("name", GetType(String))
      dttFormule.Columns.Add("num", GetType(Decimal))
      dttFormule.Columns.Add("str", GetType(String))
      dttFormule.Columns.Add("data", GetType(DateTime))
      dttFormule.Columns("name").DefaultValue = Nothing
      dttFormule.Columns("num").DefaultValue = Nothing
      dttFormule.Columns("str").DefaultValue = Nothing
      dttFormule.Columns("data").DefaultValue = Nothing
      dttFormule.AcceptChanges()

      'parte aggiuntiva diPeSetSelectionFormula (i marcatori verranno sostituiti con i campi di 'strQueryGetGroupBy'
      strQueryCrw32FileMultipli = "{testpac.codditt} = '" & DittaCorrente & "'" & _
                                  " AND {testpac.tp_anno} = |anno|" & _
                                  " AND {testpac.tp_serie} = |serie|" & _
                                  " AND {testpac.tp_numdoc} = |numdoc|" & _
                                  " AND {movpac.mp_anno}= |anno|" & _
                                  " AND {movpac.mp_serie} = |serie|" & _
                                  " AND {movpac.mp_numdoc} = |numdoc|"
      strQueryCrw32FileUnico = strQueryCrw32FileMultipli

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BSVEPACK"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico
      oPar.strParam = "Stampa Packing List"
      oPar.ctlPar1 = Me
      oPar.ctlPar2 = dttFormule
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

      oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)

      '-------------------------------
      'da bnpdgenp è stato scelto di stampare anche su carta
      If oPar.bPar4 Then
        Stampa(1, "L")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Select Case strIn.Substring(0, 1)
      Case "#"
        'Variabile esposte SPECIFICHE PER PROGRAMMA
        Select Case UCase$(Mid$(strIn, 2))
          Case "ANNO"
            Return lAnnoPack.ToString
          Case "SERIE"
            Return strSeriePack
          Case "NUMDOC"
            Return lNumPack.ToString
          Case Else
            Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else
        Return MyBase.ResolveField(strIn)
    End Select
    Return ""
  End Function
End Class
