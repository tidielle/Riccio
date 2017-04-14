Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSCTR

  Public bOnLoading As Boolean = False

#Region "Variabili"
  Public oCleDocu As CLEMGDOCU
  Public dsSctr As DataSet
  Public dsSctrTesta As DataSet
  Public dcSctr As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public dttDitta As New DataTable
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnSctr As NTSInformatica.NTSPanel
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
  Public WithEvents lbriferim As NTSInformatica.NTSLabel
  Public WithEvents lbXx_riferim As NTSInformatica.NTSLabel
  Public WithEvents cbEt_vetttipo As NTSInformatica.NTSComboBox
  Public WithEvents lbEt_vetttipo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vettcod As NTSInformatica.NTSLabel
  Public WithEvents edEt_vettcod As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_vettalbo As NTSInformatica.NTSLabel
  Public WithEvents edEt_vettalbo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_vettcod As NTSInformatica.NTSLabel
  Public WithEvents NtsPanel2 As NTSInformatica.NTSPanel
  Public WithEvents tsSctr As NTSInformatica.NTSTabControl
  Public WithEvents tsSctrPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnSctrPage1 As NTSInformatica.NTSPanel
  Public WithEvents tsSctrPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnSctrPage2 As NTSInformatica.NTSPanel
  Public WithEvents tsSctrPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnSctrPage3 As NTSInformatica.NTSPanel
  Public WithEvents tsSctrPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbEt_commdest As NTSInformatica.NTSLabel
  Public WithEvents edEt_commdest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_commcod As NTSInformatica.NTSLabel
  Public WithEvents edEt_commcod As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_commtipo As NTSInformatica.NTSLabel
  Public WithEvents cbEt_commtipo As NTSInformatica.NTSComboBox
  Public WithEvents edEt_commrs2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commrs1 As NTSInformatica.NTSLabel
  Public WithEvents edEt_commrs1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edXx_commstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_commcitt As NTSInformatica.NTSLabel
  Public WithEvents edEt_commcitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_commind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_commcap As NTSInformatica.NTSLabel
  Public WithEvents edEt_commstat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commind As NTSInformatica.NTSLabel
  Public WithEvents edEt_commcap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_commprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commprov As NTSInformatica.NTSLabel
  Public WithEvents edEt_commmail As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_commpi As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commmail As NTSInformatica.NTSLabel
  Public WithEvents edEt_commtel As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commpi As NTSInformatica.NTSLabel
  Public WithEvents edEt_commcf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_commcf As NTSInformatica.NTSLabel
  Public WithEvents lbEt_commtel As NTSInformatica.NTSLabel
  Public WithEvents edEt_carimail As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_caripi As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_carimail As NTSInformatica.NTSLabel
  Public WithEvents edEt_caritel As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_caripi As NTSInformatica.NTSLabel
  Public WithEvents edEt_caricf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_caricf As NTSInformatica.NTSLabel
  Public WithEvents lbEt_caritel As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caristat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_caricitt As NTSInformatica.NTSLabel
  Public WithEvents edEt_caricitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_cariind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_caristat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_caricap As NTSInformatica.NTSLabel
  Public WithEvents edEt_caristat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_cariind As NTSInformatica.NTSLabel
  Public WithEvents edEt_caricap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_cariprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_cariprov As NTSInformatica.NTSLabel
  Public WithEvents edEt_carirs2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_carirs1 As NTSInformatica.NTSLabel
  Public WithEvents edEt_carirs1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_caridest As NTSInformatica.NTSLabel
  Public WithEvents edEt_caridest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_caricod As NTSInformatica.NTSLabel
  Public WithEvents edEt_caricod As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_caritipo As NTSInformatica.NTSLabel
  Public WithEvents cbEt_caritipo As NTSInformatica.NTSComboBox
  Public WithEvents edEt_propmail As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_proppi As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propmail As NTSInformatica.NTSLabel
  Public WithEvents edEt_proptel As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_proppi As NTSInformatica.NTSLabel
  Public WithEvents edEt_propcf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propcf As NTSInformatica.NTSLabel
  Public WithEvents lbEt_proptel As NTSInformatica.NTSLabel
  Public WithEvents lbXx_propstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_propcitt As NTSInformatica.NTSLabel
  Public WithEvents edEt_propcitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_propind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_propcap As NTSInformatica.NTSLabel
  Public WithEvents edEt_propstat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propind As NTSInformatica.NTSLabel
  Public WithEvents edEt_propcap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_propprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propprov As NTSInformatica.NTSLabel
  Public WithEvents edEt_proprs2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_proprs1 As NTSInformatica.NTSLabel
  Public WithEvents edEt_proprs1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_propdest As NTSInformatica.NTSLabel
  Public WithEvents edEt_propdest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_propcod As NTSInformatica.NTSLabel
  Public WithEvents edEt_propcod As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_proptipo As NTSInformatica.NTSLabel
  Public WithEvents cbEt_proptipo As NTSInformatica.NTSComboBox
  Public WithEvents lbEt_evendich As NTSInformatica.NTSLabel
  Public WithEvents edEt_evendich As NTSInformatica.NTSMemoBox
  Public WithEvents tsSctrPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnSctrPage5 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_datcomp As NTSInformatica.NTSLabel
  Public WithEvents edXx_datcomp As NTSInformatica.NTSTextBoxData
  Public WithEvents lbEt_compilat As NTSInformatica.NTSLabel
  Public WithEvents edEt_compilat As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_compcitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_compcitt As NTSInformatica.NTSLabel
  Public WithEvents lbEt_istruz As NTSInformatica.NTSLabel
  Public WithEvents edEt_istruz As NTSInformatica.NTSMemoBox
  Public WithEvents lbXx_lsstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lscitt As NTSInformatica.NTSLabel
  Public WithEvents edEt_lscitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_lsind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lsstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lscap As NTSInformatica.NTSLabel
  Public WithEvents edEt_lsstat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lsind As NTSInformatica.NTSLabel
  Public WithEvents edEt_lscap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_lsprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lsprov As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lsdest As NTSInformatica.NTSLabel
  Public WithEvents edEt_lsdest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_lsconto As NTSInformatica.NTSLabel
  Public WithEvents edEt_lsconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_lstipo As NTSInformatica.NTSLabel
  Public WithEvents cbEt_lstipo As NTSInformatica.NTSComboBox
  Public WithEvents pnSctrPage4 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_lcstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lccitt As NTSInformatica.NTSLabel
  Public WithEvents edEt_lccitt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_lcind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lcstat As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lccap As NTSInformatica.NTSLabel
  Public WithEvents edEt_lcstat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lcind As NTSInformatica.NTSLabel
  Public WithEvents edEt_lccap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edEt_lcprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEt_lcprov As NTSInformatica.NTSLabel
  Public WithEvents lbEt_lcdest As NTSInformatica.NTSLabel
  Public WithEvents edEt_lcdest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_lcconto As NTSInformatica.NTSLabel
  Public WithEvents edEt_lcconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_lctipo As NTSInformatica.NTSLabel
  Public WithEvents cbEt_lctipo As NTSInformatica.NTSComboBox
  Public WithEvents lbEt_peso As NTSInformatica.NTSLabel
  Public WithEvents edEt_peso As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_quant As NTSInformatica.NTSLabel
  Public WithEvents edEt_quant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEt_tipmerce As NTSInformatica.NTSLabel
  Public WithEvents edEt_tipmerce As NTSInformatica.NTSMemoBox
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
    Dim strErr As String = ""
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSCTR", "BEMGDOCU", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086828667866925, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(objTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSCTR", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    dsSctr = New DataSet
    dsSctr.Tables.Add(CType(oCallParams.ctlPar1, DataTable).Clone)
    If CType(oCallParams.ctlPar1, DataTable).Rows.Count > 0 Then
      dsSctr.Tables("SCHETRASP").ImportRow(CType(oCallParams.ctlPar1, DataTable).Rows(0))
    End If
    dsSctr.AcceptChanges()

    dsSctrTesta = New DataSet
    dsSctrTesta.Tables.Add(CType(oCallParams.ctlPar2, DataTable).Clone)
    If CType(oCallParams.ctlPar2, DataTable).Rows.Count > 0 Then
      dsSctrTesta.Tables("TESTA").ImportRow(CType(oCallParams.ctlPar2, DataTable).Rows(0))
    End If
    dsSctrTesta.AcceptChanges()

    If oCallParams.strNomProg = "BSVEBOLL" Then
      oCleDocu.BORDINI = False
    ElseIf oCallParams.strNomProg = "BSVEBOLL" Then
      oCleDocu.BORDINI = True
    End If

    oCleDocu.dSctrColli = oCallParams.dPar1
    oCleDocu.dSctrQuat = oCallParams.dPar2
    oCleDocu.dSctrPeso = oCallParams.dPar3
    oCleDocu.bArtConai = oCallParams.bPar1

    oCleDocu.bSctrIsLoad = False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSCTR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnSctr = New NTSInformatica.NTSPanel
    Me.NtsPanel2 = New NTSInformatica.NTSPanel
    Me.tsSctr = New NTSInformatica.NTSTabControl
    Me.tsSctrPage1 = New NTSInformatica.NTSTabPage
    Me.pnSctrPage1 = New NTSInformatica.NTSPanel
    Me.edEt_commmail = New NTSInformatica.NTSTextBoxStr
    Me.edEt_commpi = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commmail = New NTSInformatica.NTSLabel
    Me.edEt_commtel = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commpi = New NTSInformatica.NTSLabel
    Me.edEt_commcf = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commcf = New NTSInformatica.NTSLabel
    Me.lbEt_commtel = New NTSInformatica.NTSLabel
    Me.edXx_commstat = New NTSInformatica.NTSLabel
    Me.lbEt_commcitt = New NTSInformatica.NTSLabel
    Me.edEt_commcitt = New NTSInformatica.NTSTextBoxStr
    Me.edEt_commind = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commstat = New NTSInformatica.NTSLabel
    Me.lbEt_commcap = New NTSInformatica.NTSLabel
    Me.edEt_commstat = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commind = New NTSInformatica.NTSLabel
    Me.edEt_commcap = New NTSInformatica.NTSTextBoxStr
    Me.edEt_commprov = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commprov = New NTSInformatica.NTSLabel
    Me.edEt_commrs2 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commrs1 = New NTSInformatica.NTSLabel
    Me.edEt_commrs1 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_commdest = New NTSInformatica.NTSLabel
    Me.edEt_commdest = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_commcod = New NTSInformatica.NTSLabel
    Me.edEt_commcod = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_commtipo = New NTSInformatica.NTSLabel
    Me.cbEt_commtipo = New NTSInformatica.NTSComboBox
    Me.tsSctrPage2 = New NTSInformatica.NTSTabPage
    Me.pnSctrPage2 = New NTSInformatica.NTSPanel
    Me.edEt_carimail = New NTSInformatica.NTSTextBoxStr
    Me.edEt_caripi = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_carimail = New NTSInformatica.NTSLabel
    Me.edEt_caritel = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_caripi = New NTSInformatica.NTSLabel
    Me.edEt_caricf = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_caricf = New NTSInformatica.NTSLabel
    Me.lbEt_caritel = New NTSInformatica.NTSLabel
    Me.lbXx_caristat = New NTSInformatica.NTSLabel
    Me.lbEt_caricitt = New NTSInformatica.NTSLabel
    Me.edEt_caricitt = New NTSInformatica.NTSTextBoxStr
    Me.edEt_cariind = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_caristat = New NTSInformatica.NTSLabel
    Me.lbEt_caricap = New NTSInformatica.NTSLabel
    Me.edEt_caristat = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_cariind = New NTSInformatica.NTSLabel
    Me.edEt_caricap = New NTSInformatica.NTSTextBoxStr
    Me.edEt_cariprov = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_cariprov = New NTSInformatica.NTSLabel
    Me.edEt_carirs2 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_carirs1 = New NTSInformatica.NTSLabel
    Me.edEt_carirs1 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_caridest = New NTSInformatica.NTSLabel
    Me.edEt_caridest = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_caricod = New NTSInformatica.NTSLabel
    Me.edEt_caricod = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_caritipo = New NTSInformatica.NTSLabel
    Me.cbEt_caritipo = New NTSInformatica.NTSComboBox
    Me.tsSctrPage3 = New NTSInformatica.NTSTabPage
    Me.pnSctrPage3 = New NTSInformatica.NTSPanel
    Me.lbEt_evendich = New NTSInformatica.NTSLabel
    Me.edEt_evendich = New NTSInformatica.NTSMemoBox
    Me.edEt_propmail = New NTSInformatica.NTSTextBoxStr
    Me.edEt_proppi = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propmail = New NTSInformatica.NTSLabel
    Me.edEt_proptel = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_proppi = New NTSInformatica.NTSLabel
    Me.edEt_propcf = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propcf = New NTSInformatica.NTSLabel
    Me.lbEt_proptel = New NTSInformatica.NTSLabel
    Me.lbXx_propstat = New NTSInformatica.NTSLabel
    Me.lbEt_propcitt = New NTSInformatica.NTSLabel
    Me.edEt_propcitt = New NTSInformatica.NTSTextBoxStr
    Me.edEt_propind = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propstat = New NTSInformatica.NTSLabel
    Me.lbEt_propcap = New NTSInformatica.NTSLabel
    Me.edEt_propstat = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propind = New NTSInformatica.NTSLabel
    Me.edEt_propcap = New NTSInformatica.NTSTextBoxStr
    Me.edEt_propprov = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propprov = New NTSInformatica.NTSLabel
    Me.edEt_proprs2 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_proprs1 = New NTSInformatica.NTSLabel
    Me.edEt_proprs1 = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_propdest = New NTSInformatica.NTSLabel
    Me.edEt_propdest = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_propcod = New NTSInformatica.NTSLabel
    Me.edEt_propcod = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_proptipo = New NTSInformatica.NTSLabel
    Me.cbEt_proptipo = New NTSInformatica.NTSComboBox
    Me.tsSctrPage4 = New NTSInformatica.NTSTabPage
    Me.pnSctrPage4 = New NTSInformatica.NTSPanel
    Me.lbXx_lcstat = New NTSInformatica.NTSLabel
    Me.lbEt_lccitt = New NTSInformatica.NTSLabel
    Me.edEt_lccitt = New NTSInformatica.NTSTextBoxStr
    Me.edEt_lcind = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lcstat = New NTSInformatica.NTSLabel
    Me.lbEt_lccap = New NTSInformatica.NTSLabel
    Me.edEt_lcstat = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lcind = New NTSInformatica.NTSLabel
    Me.edEt_lccap = New NTSInformatica.NTSTextBoxStr
    Me.edEt_lcprov = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lcprov = New NTSInformatica.NTSLabel
    Me.lbEt_lcdest = New NTSInformatica.NTSLabel
    Me.edEt_lcdest = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_lcconto = New NTSInformatica.NTSLabel
    Me.edEt_lcconto = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_lctipo = New NTSInformatica.NTSLabel
    Me.cbEt_lctipo = New NTSInformatica.NTSComboBox
    Me.lbEt_peso = New NTSInformatica.NTSLabel
    Me.edEt_peso = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_quant = New NTSInformatica.NTSLabel
    Me.edEt_quant = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_tipmerce = New NTSInformatica.NTSLabel
    Me.edEt_tipmerce = New NTSInformatica.NTSMemoBox
    Me.tsSctrPage5 = New NTSInformatica.NTSTabPage
    Me.pnSctrPage5 = New NTSInformatica.NTSPanel
    Me.lbXx_datcomp = New NTSInformatica.NTSLabel
    Me.edXx_datcomp = New NTSInformatica.NTSTextBoxData
    Me.lbEt_compilat = New NTSInformatica.NTSLabel
    Me.edEt_compilat = New NTSInformatica.NTSTextBoxStr
    Me.edEt_compcitt = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_compcitt = New NTSInformatica.NTSLabel
    Me.lbEt_istruz = New NTSInformatica.NTSLabel
    Me.edEt_istruz = New NTSInformatica.NTSMemoBox
    Me.lbXx_lsstat = New NTSInformatica.NTSLabel
    Me.lbEt_lscitt = New NTSInformatica.NTSLabel
    Me.edEt_lscitt = New NTSInformatica.NTSTextBoxStr
    Me.edEt_lsind = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lsstat = New NTSInformatica.NTSLabel
    Me.lbEt_lscap = New NTSInformatica.NTSLabel
    Me.edEt_lsstat = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lsind = New NTSInformatica.NTSLabel
    Me.edEt_lscap = New NTSInformatica.NTSTextBoxStr
    Me.edEt_lsprov = New NTSInformatica.NTSTextBoxStr
    Me.lbEt_lsprov = New NTSInformatica.NTSLabel
    Me.lbEt_lsdest = New NTSInformatica.NTSLabel
    Me.edEt_lsdest = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_lsconto = New NTSInformatica.NTSLabel
    Me.edEt_lsconto = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_lstipo = New NTSInformatica.NTSLabel
    Me.cbEt_lstipo = New NTSInformatica.NTSComboBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbXx_riferim = New NTSInformatica.NTSLabel
    Me.lbEt_vetttipo = New NTSInformatica.NTSLabel
    Me.edEt_vettalbo = New NTSInformatica.NTSTextBoxStr
    Me.lbriferim = New NTSInformatica.NTSLabel
    Me.lbEt_vettcod = New NTSInformatica.NTSLabel
    Me.cbEt_vetttipo = New NTSInformatica.NTSComboBox
    Me.edEt_vettcod = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_vettcod = New NTSInformatica.NTSLabel
    Me.lbEt_vettalbo = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSctr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctr.SuspendLayout()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel2.SuspendLayout()
    CType(Me.tsSctr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSctr.SuspendLayout()
    Me.tsSctrPage1.SuspendLayout()
    CType(Me.pnSctrPage1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctrPage1.SuspendLayout()
    CType(Me.edEt_commmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commpi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commtel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commcf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commcitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commstat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commcap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commrs2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commrs1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_commcod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_commtipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSctrPage2.SuspendLayout()
    CType(Me.pnSctrPage2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctrPage2.SuspendLayout()
    CType(Me.edEt_carimail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caripi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caritel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caricf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caricitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_cariind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caristat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caricap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_cariprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_carirs2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_carirs1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caridest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_caricod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_caritipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSctrPage3.SuspendLayout()
    CType(Me.pnSctrPage3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctrPage3.SuspendLayout()
    CType(Me.edEt_evendich.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_proppi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_proptel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propcf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propcitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propstat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propcap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_proprs2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_proprs1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_propcod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_proptipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSctrPage4.SuspendLayout()
    CType(Me.pnSctrPage4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctrPage4.SuspendLayout()
    CType(Me.edEt_lccitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lcind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lcstat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lccap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lcprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lcdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lcconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_lctipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_peso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_quant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_tipmerce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSctrPage5.SuspendLayout()
    CType(Me.pnSctrPage5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSctrPage5.SuspendLayout()
    CType(Me.edXx_datcomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_compilat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_compcitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_istruz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lscitt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lsind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lsstat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lscap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lsprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lsdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_lsconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_lstipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edEt_vettalbo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_vetttipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_vettcod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnSctr
    '
    Me.pnSctr.AllowDrop = True
    Me.pnSctr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctr.Appearance.Options.UseBackColor = True
    Me.pnSctr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctr.Controls.Add(Me.NtsPanel2)
    Me.pnSctr.Controls.Add(Me.pnTop)
    Me.pnSctr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctr.Location = New System.Drawing.Point(0, 26)
    Me.pnSctr.Name = "pnSctr"
    Me.pnSctr.NTSActiveTrasparency = True
    Me.pnSctr.Size = New System.Drawing.Size(661, 442)
    Me.pnSctr.TabIndex = 10
    Me.pnSctr.Text = "pnSctr"
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel2.Appearance.Options.UseBackColor = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.tsSctr)
    Me.NtsPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel2.Location = New System.Drawing.Point(0, 113)
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSActiveTrasparency = True
    Me.NtsPanel2.Size = New System.Drawing.Size(661, 329)
    Me.NtsPanel2.TabIndex = 37
    Me.NtsPanel2.Text = "NtsPanel2"
    '
    'tsSctr
    '
    Me.tsSctr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSctr.Location = New System.Drawing.Point(0, 0)
    Me.tsSctr.Name = "tsSctr"
    Me.tsSctr.SelectedTabPage = Me.tsSctrPage1
    Me.tsSctr.Size = New System.Drawing.Size(661, 329)
    Me.tsSctr.TabIndex = 27
    Me.tsSctr.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tsSctrPage1, Me.tsSctrPage2, Me.tsSctrPage3, Me.tsSctrPage4, Me.tsSctrPage5})
    '
    'tsSctrPage1
    '
    Me.tsSctrPage1.AllowDrop = True
    Me.tsSctrPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.tsSctrPage1.Appearance.Header.Options.UseFont = True
    Me.tsSctrPage1.Controls.Add(Me.pnSctrPage1)
    Me.tsSctrPage1.Enable = True
    Me.tsSctrPage1.Name = "tsSctrPage1"
    Me.tsSctrPage1.Size = New System.Drawing.Size(652, 299)
    Me.tsSctrPage1.Text = "Committente"
    '
    'pnSctrPage1
    '
    Me.pnSctrPage1.AllowDrop = True
    Me.pnSctrPage1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctrPage1.Appearance.Options.UseBackColor = True
    Me.pnSctrPage1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctrPage1.Controls.Add(Me.edEt_commmail)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commpi)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commmail)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commtel)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commpi)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commcf)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commcf)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commtel)
    Me.pnSctrPage1.Controls.Add(Me.edXx_commstat)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commcitt)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commcitt)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commind)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commstat)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commcap)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commstat)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commind)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commcap)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commprov)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commprov)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commrs2)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commrs1)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commrs1)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commdest)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commdest)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commcod)
    Me.pnSctrPage1.Controls.Add(Me.edEt_commcod)
    Me.pnSctrPage1.Controls.Add(Me.lbEt_commtipo)
    Me.pnSctrPage1.Controls.Add(Me.cbEt_commtipo)
    Me.pnSctrPage1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctrPage1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctrPage1.Location = New System.Drawing.Point(0, 0)
    Me.pnSctrPage1.Name = "pnSctrPage1"
    Me.pnSctrPage1.NTSActiveTrasparency = True
    Me.pnSctrPage1.Size = New System.Drawing.Size(652, 299)
    Me.pnSctrPage1.TabIndex = 0
    Me.pnSctrPage1.Text = "pnSctrPage1"
    '
    'edEt_commmail
    '
    Me.edEt_commmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commmail.Location = New System.Drawing.Point(442, 194)
    Me.edEt_commmail.Name = "edEt_commmail"
    Me.edEt_commmail.NTSDbField = ""
    Me.edEt_commmail.NTSForzaVisZoom = False
    Me.edEt_commmail.NTSOldValue = ""
    Me.edEt_commmail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commmail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commmail.Properties.AutoHeight = False
    Me.edEt_commmail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commmail.Properties.MaxLength = 65536
    Me.edEt_commmail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commmail.Size = New System.Drawing.Size(194, 20)
    Me.edEt_commmail.TabIndex = 599
    '
    'edEt_commpi
    '
    Me.edEt_commpi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commpi.Location = New System.Drawing.Point(442, 168)
    Me.edEt_commpi.Name = "edEt_commpi"
    Me.edEt_commpi.NTSDbField = ""
    Me.edEt_commpi.NTSForzaVisZoom = False
    Me.edEt_commpi.NTSOldValue = ""
    Me.edEt_commpi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commpi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commpi.Properties.AutoHeight = False
    Me.edEt_commpi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commpi.Properties.MaxLength = 65536
    Me.edEt_commpi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commpi.Size = New System.Drawing.Size(194, 20)
    Me.edEt_commpi.TabIndex = 597
    '
    'lbEt_commmail
    '
    Me.lbEt_commmail.AutoSize = True
    Me.lbEt_commmail.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commmail.Location = New System.Drawing.Point(335, 197)
    Me.lbEt_commmail.Name = "lbEt_commmail"
    Me.lbEt_commmail.NTSDbField = ""
    Me.lbEt_commmail.Size = New System.Drawing.Size(35, 13)
    Me.lbEt_commmail.TabIndex = 595
    Me.lbEt_commmail.Text = "E-mail"
    Me.lbEt_commmail.Tooltip = ""
    Me.lbEt_commmail.UseMnemonic = False
    '
    'edEt_commtel
    '
    Me.edEt_commtel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commtel.Location = New System.Drawing.Point(115, 194)
    Me.edEt_commtel.Name = "edEt_commtel"
    Me.edEt_commtel.NTSDbField = ""
    Me.edEt_commtel.NTSForzaVisZoom = False
    Me.edEt_commtel.NTSOldValue = ""
    Me.edEt_commtel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commtel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commtel.Properties.AutoHeight = False
    Me.edEt_commtel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commtel.Properties.MaxLength = 65536
    Me.edEt_commtel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commtel.Size = New System.Drawing.Size(194, 20)
    Me.edEt_commtel.TabIndex = 598
    '
    'lbEt_commpi
    '
    Me.lbEt_commpi.AutoSize = True
    Me.lbEt_commpi.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commpi.Location = New System.Drawing.Point(335, 171)
    Me.lbEt_commpi.Name = "lbEt_commpi"
    Me.lbEt_commpi.NTSDbField = ""
    Me.lbEt_commpi.Size = New System.Drawing.Size(59, 13)
    Me.lbEt_commpi.TabIndex = 593
    Me.lbEt_commpi.Text = "Partita IVA"
    Me.lbEt_commpi.Tooltip = ""
    Me.lbEt_commpi.UseMnemonic = False
    '
    'edEt_commcf
    '
    Me.edEt_commcf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commcf.Location = New System.Drawing.Point(115, 168)
    Me.edEt_commcf.Name = "edEt_commcf"
    Me.edEt_commcf.NTSDbField = ""
    Me.edEt_commcf.NTSForzaVisZoom = False
    Me.edEt_commcf.NTSOldValue = ""
    Me.edEt_commcf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commcf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commcf.Properties.AutoHeight = False
    Me.edEt_commcf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commcf.Properties.MaxLength = 65536
    Me.edEt_commcf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commcf.Size = New System.Drawing.Size(194, 20)
    Me.edEt_commcf.TabIndex = 596
    '
    'lbEt_commcf
    '
    Me.lbEt_commcf.AutoSize = True
    Me.lbEt_commcf.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commcf.Location = New System.Drawing.Point(8, 171)
    Me.lbEt_commcf.Name = "lbEt_commcf"
    Me.lbEt_commcf.NTSDbField = ""
    Me.lbEt_commcf.Size = New System.Drawing.Size(63, 13)
    Me.lbEt_commcf.TabIndex = 592
    Me.lbEt_commcf.Text = "Cod. fiscale"
    Me.lbEt_commcf.Tooltip = ""
    Me.lbEt_commcf.UseMnemonic = False
    '
    'lbEt_commtel
    '
    Me.lbEt_commtel.AutoSize = True
    Me.lbEt_commtel.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commtel.Location = New System.Drawing.Point(8, 197)
    Me.lbEt_commtel.Name = "lbEt_commtel"
    Me.lbEt_commtel.NTSDbField = ""
    Me.lbEt_commtel.Size = New System.Drawing.Size(49, 13)
    Me.lbEt_commtel.TabIndex = 594
    Me.lbEt_commtel.Text = "Telefono"
    Me.lbEt_commtel.Tooltip = ""
    Me.lbEt_commtel.UseMnemonic = False
    '
    'edXx_commstat
    '
    Me.edXx_commstat.BackColor = System.Drawing.Color.Transparent
    Me.edXx_commstat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edXx_commstat.Location = New System.Drawing.Point(511, 142)
    Me.edXx_commstat.Name = "edXx_commstat"
    Me.edXx_commstat.NTSDbField = ""
    Me.edXx_commstat.Size = New System.Drawing.Size(125, 20)
    Me.edXx_commstat.TabIndex = 591
    Me.edXx_commstat.Tooltip = ""
    Me.edXx_commstat.UseMnemonic = False
    '
    'lbEt_commcitt
    '
    Me.lbEt_commcitt.AutoSize = True
    Me.lbEt_commcitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commcitt.Location = New System.Drawing.Point(8, 119)
    Me.lbEt_commcitt.Name = "lbEt_commcitt"
    Me.lbEt_commcitt.NTSDbField = ""
    Me.lbEt_commcitt.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_commcitt.TabIndex = 583
    Me.lbEt_commcitt.Text = "Citta/località"
    Me.lbEt_commcitt.Tooltip = ""
    Me.lbEt_commcitt.UseMnemonic = False
    '
    'edEt_commcitt
    '
    Me.edEt_commcitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commcitt.Location = New System.Drawing.Point(115, 116)
    Me.edEt_commcitt.Name = "edEt_commcitt"
    Me.edEt_commcitt.NTSDbField = ""
    Me.edEt_commcitt.NTSForzaVisZoom = False
    Me.edEt_commcitt.NTSOldValue = ""
    Me.edEt_commcitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commcitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commcitt.Properties.AutoHeight = False
    Me.edEt_commcitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commcitt.Properties.MaxLength = 65536
    Me.edEt_commcitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commcitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_commcitt.TabIndex = 588
    '
    'edEt_commind
    '
    Me.edEt_commind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commind.Location = New System.Drawing.Point(115, 90)
    Me.edEt_commind.Name = "edEt_commind"
    Me.edEt_commind.NTSDbField = ""
    Me.edEt_commind.NTSForzaVisZoom = False
    Me.edEt_commind.NTSOldValue = ""
    Me.edEt_commind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commind.Properties.AutoHeight = False
    Me.edEt_commind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commind.Properties.MaxLength = 65536
    Me.edEt_commind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commind.Size = New System.Drawing.Size(521, 20)
    Me.edEt_commind.TabIndex = 586
    '
    'lbEt_commstat
    '
    Me.lbEt_commstat.AutoSize = True
    Me.lbEt_commstat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commstat.Location = New System.Drawing.Point(335, 143)
    Me.lbEt_commstat.Name = "lbEt_commstat"
    Me.lbEt_commstat.NTSDbField = ""
    Me.lbEt_commstat.Size = New System.Drawing.Size(58, 13)
    Me.lbEt_commstat.TabIndex = 585
    Me.lbEt_commstat.Text = "Cod. stato"
    Me.lbEt_commstat.Tooltip = ""
    Me.lbEt_commstat.UseMnemonic = False
    '
    'lbEt_commcap
    '
    Me.lbEt_commcap.AutoSize = True
    Me.lbEt_commcap.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commcap.Location = New System.Drawing.Point(8, 145)
    Me.lbEt_commcap.Name = "lbEt_commcap"
    Me.lbEt_commcap.NTSDbField = ""
    Me.lbEt_commcap.Size = New System.Drawing.Size(26, 13)
    Me.lbEt_commcap.TabIndex = 582
    Me.lbEt_commcap.Text = "Cap"
    Me.lbEt_commcap.Tooltip = ""
    Me.lbEt_commcap.UseMnemonic = False
    '
    'edEt_commstat
    '
    Me.edEt_commstat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commstat.Location = New System.Drawing.Point(442, 142)
    Me.edEt_commstat.Name = "edEt_commstat"
    Me.edEt_commstat.NTSDbField = ""
    Me.edEt_commstat.NTSForzaVisZoom = False
    Me.edEt_commstat.NTSOldValue = ""
    Me.edEt_commstat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commstat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commstat.Properties.AutoHeight = False
    Me.edEt_commstat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commstat.Properties.MaxLength = 65536
    Me.edEt_commstat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commstat.Size = New System.Drawing.Size(63, 20)
    Me.edEt_commstat.TabIndex = 590
    '
    'lbEt_commind
    '
    Me.lbEt_commind.AutoSize = True
    Me.lbEt_commind.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commind.Location = New System.Drawing.Point(8, 93)
    Me.lbEt_commind.Name = "lbEt_commind"
    Me.lbEt_commind.NTSDbField = ""
    Me.lbEt_commind.Size = New System.Drawing.Size(47, 13)
    Me.lbEt_commind.TabIndex = 581
    Me.lbEt_commind.Text = "Indirizzo"
    Me.lbEt_commind.Tooltip = ""
    Me.lbEt_commind.UseMnemonic = False
    '
    'edEt_commcap
    '
    Me.edEt_commcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commcap.Location = New System.Drawing.Point(115, 142)
    Me.edEt_commcap.Name = "edEt_commcap"
    Me.edEt_commcap.NTSDbField = ""
    Me.edEt_commcap.NTSForzaVisZoom = False
    Me.edEt_commcap.NTSOldValue = ""
    Me.edEt_commcap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commcap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commcap.Properties.AutoHeight = False
    Me.edEt_commcap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commcap.Properties.MaxLength = 65536
    Me.edEt_commcap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commcap.Size = New System.Drawing.Size(63, 20)
    Me.edEt_commcap.TabIndex = 587
    '
    'edEt_commprov
    '
    Me.edEt_commprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commprov.Location = New System.Drawing.Point(263, 142)
    Me.edEt_commprov.Name = "edEt_commprov"
    Me.edEt_commprov.NTSDbField = ""
    Me.edEt_commprov.NTSForzaVisZoom = False
    Me.edEt_commprov.NTSOldValue = ""
    Me.edEt_commprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commprov.Properties.AutoHeight = False
    Me.edEt_commprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commprov.Properties.MaxLength = 65536
    Me.edEt_commprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commprov.Size = New System.Drawing.Size(45, 20)
    Me.edEt_commprov.TabIndex = 589
    '
    'lbEt_commprov
    '
    Me.lbEt_commprov.AutoSize = True
    Me.lbEt_commprov.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commprov.Location = New System.Drawing.Point(184, 145)
    Me.lbEt_commprov.Name = "lbEt_commprov"
    Me.lbEt_commprov.NTSDbField = ""
    Me.lbEt_commprov.Size = New System.Drawing.Size(50, 13)
    Me.lbEt_commprov.TabIndex = 584
    Me.lbEt_commprov.Text = "Provincia"
    Me.lbEt_commprov.Tooltip = ""
    Me.lbEt_commprov.UseMnemonic = False
    '
    'edEt_commrs2
    '
    Me.edEt_commrs2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commrs2.Location = New System.Drawing.Point(115, 64)
    Me.edEt_commrs2.Name = "edEt_commrs2"
    Me.edEt_commrs2.NTSDbField = ""
    Me.edEt_commrs2.NTSForzaVisZoom = False
    Me.edEt_commrs2.NTSOldValue = ""
    Me.edEt_commrs2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commrs2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commrs2.Properties.AutoHeight = False
    Me.edEt_commrs2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commrs2.Properties.MaxLength = 65536
    Me.edEt_commrs2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commrs2.Size = New System.Drawing.Size(521, 20)
    Me.edEt_commrs2.TabIndex = 505
    '
    'lbEt_commrs1
    '
    Me.lbEt_commrs1.AutoSize = True
    Me.lbEt_commrs1.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commrs1.Location = New System.Drawing.Point(8, 41)
    Me.lbEt_commrs1.Name = "lbEt_commrs1"
    Me.lbEt_commrs1.NTSDbField = ""
    Me.lbEt_commrs1.Size = New System.Drawing.Size(65, 13)
    Me.lbEt_commrs1.TabIndex = 503
    Me.lbEt_commrs1.Text = "Rag. sociale"
    Me.lbEt_commrs1.Tooltip = ""
    Me.lbEt_commrs1.UseMnemonic = False
    '
    'edEt_commrs1
    '
    Me.edEt_commrs1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commrs1.Location = New System.Drawing.Point(115, 38)
    Me.edEt_commrs1.Name = "edEt_commrs1"
    Me.edEt_commrs1.NTSDbField = ""
    Me.edEt_commrs1.NTSForzaVisZoom = False
    Me.edEt_commrs1.NTSOldValue = ""
    Me.edEt_commrs1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commrs1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commrs1.Properties.AutoHeight = False
    Me.edEt_commrs1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commrs1.Properties.MaxLength = 65536
    Me.edEt_commrs1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commrs1.Size = New System.Drawing.Size(521, 20)
    Me.edEt_commrs1.TabIndex = 504
    '
    'lbEt_commdest
    '
    Me.lbEt_commdest.AutoSize = True
    Me.lbEt_commdest.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commdest.Location = New System.Drawing.Point(462, 15)
    Me.lbEt_commdest.Name = "lbEt_commdest"
    Me.lbEt_commdest.NTSDbField = ""
    Me.lbEt_commdest.Size = New System.Drawing.Size(93, 13)
    Me.lbEt_commdest.TabIndex = 40
    Me.lbEt_commdest.Text = "Cod. destinazione"
    Me.lbEt_commdest.Tooltip = ""
    Me.lbEt_commdest.UseMnemonic = False
    '
    'edEt_commdest
    '
    Me.edEt_commdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commdest.Location = New System.Drawing.Point(569, 12)
    Me.edEt_commdest.Name = "edEt_commdest"
    Me.edEt_commdest.NTSDbField = ""
    Me.edEt_commdest.NTSFormat = "0"
    Me.edEt_commdest.NTSForzaVisZoom = False
    Me.edEt_commdest.NTSOldValue = ""
    Me.edEt_commdest.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_commdest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_commdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commdest.Properties.AutoHeight = False
    Me.edEt_commdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commdest.Properties.MaxLength = 65536
    Me.edEt_commdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commdest.Size = New System.Drawing.Size(67, 20)
    Me.edEt_commdest.TabIndex = 39
    '
    'lbEt_commcod
    '
    Me.lbEt_commcod.AutoSize = True
    Me.lbEt_commcod.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commcod.Location = New System.Drawing.Point(259, 15)
    Me.lbEt_commcod.Name = "lbEt_commcod"
    Me.lbEt_commcod.NTSDbField = ""
    Me.lbEt_commcod.Size = New System.Drawing.Size(102, 13)
    Me.lbEt_commcod.TabIndex = 37
    Me.lbEt_commcod.Text = "Cod. soggetto com."
    Me.lbEt_commcod.Tooltip = ""
    Me.lbEt_commcod.UseMnemonic = False
    '
    'edEt_commcod
    '
    Me.edEt_commcod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_commcod.Location = New System.Drawing.Point(366, 12)
    Me.edEt_commcod.Name = "edEt_commcod"
    Me.edEt_commcod.NTSDbField = ""
    Me.edEt_commcod.NTSFormat = "0"
    Me.edEt_commcod.NTSForzaVisZoom = False
    Me.edEt_commcod.NTSOldValue = ""
    Me.edEt_commcod.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_commcod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_commcod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_commcod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_commcod.Properties.AutoHeight = False
    Me.edEt_commcod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_commcod.Properties.MaxLength = 65536
    Me.edEt_commcod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_commcod.Size = New System.Drawing.Size(90, 20)
    Me.edEt_commcod.TabIndex = 36
    '
    'lbEt_commtipo
    '
    Me.lbEt_commtipo.AutoSize = True
    Me.lbEt_commtipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_commtipo.Location = New System.Drawing.Point(8, 15)
    Me.lbEt_commtipo.Name = "lbEt_commtipo"
    Me.lbEt_commtipo.NTSDbField = ""
    Me.lbEt_commtipo.Size = New System.Drawing.Size(68, 13)
    Me.lbEt_commtipo.TabIndex = 32
    Me.lbEt_commtipo.Text = "Committente"
    Me.lbEt_commtipo.Tooltip = ""
    Me.lbEt_commtipo.UseMnemonic = False
    '
    'cbEt_commtipo
    '
    Me.cbEt_commtipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_commtipo.DataSource = Nothing
    Me.cbEt_commtipo.DisplayMember = ""
    Me.cbEt_commtipo.EditValue = ""
    Me.cbEt_commtipo.Location = New System.Drawing.Point(115, 12)
    Me.cbEt_commtipo.Name = "cbEt_commtipo"
    Me.cbEt_commtipo.NTSDbField = ""
    Me.cbEt_commtipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_commtipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_commtipo.Properties.AutoHeight = False
    Me.cbEt_commtipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_commtipo.Properties.DropDownRows = 30
    Me.cbEt_commtipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_commtipo.SelectedValue = ""
    Me.cbEt_commtipo.Size = New System.Drawing.Size(140, 20)
    Me.cbEt_commtipo.TabIndex = 31
    Me.cbEt_commtipo.ValueMember = ""
    '
    'tsSctrPage2
    '
    Me.tsSctrPage2.AllowDrop = True
    Me.tsSctrPage2.Controls.Add(Me.pnSctrPage2)
    Me.tsSctrPage2.Enable = True
    Me.tsSctrPage2.Name = "tsSctrPage2"
    Me.tsSctrPage2.Size = New System.Drawing.Size(652, 299)
    Me.tsSctrPage2.Text = "Caricatore"
    '
    'pnSctrPage2
    '
    Me.pnSctrPage2.AllowDrop = True
    Me.pnSctrPage2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctrPage2.Appearance.Options.UseBackColor = True
    Me.pnSctrPage2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctrPage2.Controls.Add(Me.edEt_carimail)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caripi)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_carimail)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caritel)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caripi)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caricf)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caricf)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caritel)
    Me.pnSctrPage2.Controls.Add(Me.lbXx_caristat)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caricitt)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caricitt)
    Me.pnSctrPage2.Controls.Add(Me.edEt_cariind)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caristat)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caricap)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caristat)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_cariind)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caricap)
    Me.pnSctrPage2.Controls.Add(Me.edEt_cariprov)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_cariprov)
    Me.pnSctrPage2.Controls.Add(Me.edEt_carirs2)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_carirs1)
    Me.pnSctrPage2.Controls.Add(Me.edEt_carirs1)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caridest)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caridest)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caricod)
    Me.pnSctrPage2.Controls.Add(Me.edEt_caricod)
    Me.pnSctrPage2.Controls.Add(Me.lbEt_caritipo)
    Me.pnSctrPage2.Controls.Add(Me.cbEt_caritipo)
    Me.pnSctrPage2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctrPage2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctrPage2.Location = New System.Drawing.Point(0, 0)
    Me.pnSctrPage2.Name = "pnSctrPage2"
    Me.pnSctrPage2.NTSActiveTrasparency = True
    Me.pnSctrPage2.Size = New System.Drawing.Size(652, 299)
    Me.pnSctrPage2.TabIndex = 1
    Me.pnSctrPage2.Text = "NtsPanel1"
    '
    'edEt_carimail
    '
    Me.edEt_carimail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_carimail.Location = New System.Drawing.Point(442, 194)
    Me.edEt_carimail.Name = "edEt_carimail"
    Me.edEt_carimail.NTSDbField = ""
    Me.edEt_carimail.NTSForzaVisZoom = False
    Me.edEt_carimail.NTSOldValue = ""
    Me.edEt_carimail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_carimail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_carimail.Properties.AutoHeight = False
    Me.edEt_carimail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_carimail.Properties.MaxLength = 65536
    Me.edEt_carimail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_carimail.Size = New System.Drawing.Size(194, 20)
    Me.edEt_carimail.TabIndex = 629
    '
    'edEt_caripi
    '
    Me.edEt_caripi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caripi.Location = New System.Drawing.Point(442, 168)
    Me.edEt_caripi.Name = "edEt_caripi"
    Me.edEt_caripi.NTSDbField = ""
    Me.edEt_caripi.NTSForzaVisZoom = False
    Me.edEt_caripi.NTSOldValue = ""
    Me.edEt_caripi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caripi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caripi.Properties.AutoHeight = False
    Me.edEt_caripi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caripi.Properties.MaxLength = 65536
    Me.edEt_caripi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caripi.Size = New System.Drawing.Size(194, 20)
    Me.edEt_caripi.TabIndex = 627
    '
    'lbEt_carimail
    '
    Me.lbEt_carimail.AutoSize = True
    Me.lbEt_carimail.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_carimail.Location = New System.Drawing.Point(335, 197)
    Me.lbEt_carimail.Name = "lbEt_carimail"
    Me.lbEt_carimail.NTSDbField = ""
    Me.lbEt_carimail.Size = New System.Drawing.Size(35, 13)
    Me.lbEt_carimail.TabIndex = 625
    Me.lbEt_carimail.Text = "E-mail"
    Me.lbEt_carimail.Tooltip = ""
    Me.lbEt_carimail.UseMnemonic = False
    '
    'edEt_caritel
    '
    Me.edEt_caritel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caritel.Location = New System.Drawing.Point(115, 194)
    Me.edEt_caritel.Name = "edEt_caritel"
    Me.edEt_caritel.NTSDbField = ""
    Me.edEt_caritel.NTSForzaVisZoom = False
    Me.edEt_caritel.NTSOldValue = ""
    Me.edEt_caritel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caritel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caritel.Properties.AutoHeight = False
    Me.edEt_caritel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caritel.Properties.MaxLength = 65536
    Me.edEt_caritel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caritel.Size = New System.Drawing.Size(194, 20)
    Me.edEt_caritel.TabIndex = 628
    '
    'lbEt_caripi
    '
    Me.lbEt_caripi.AutoSize = True
    Me.lbEt_caripi.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caripi.Location = New System.Drawing.Point(335, 171)
    Me.lbEt_caripi.Name = "lbEt_caripi"
    Me.lbEt_caripi.NTSDbField = ""
    Me.lbEt_caripi.Size = New System.Drawing.Size(59, 13)
    Me.lbEt_caripi.TabIndex = 623
    Me.lbEt_caripi.Text = "Partita IVA"
    Me.lbEt_caripi.Tooltip = ""
    Me.lbEt_caripi.UseMnemonic = False
    '
    'edEt_caricf
    '
    Me.edEt_caricf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caricf.Location = New System.Drawing.Point(115, 168)
    Me.edEt_caricf.Name = "edEt_caricf"
    Me.edEt_caricf.NTSDbField = ""
    Me.edEt_caricf.NTSForzaVisZoom = False
    Me.edEt_caricf.NTSOldValue = ""
    Me.edEt_caricf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caricf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caricf.Properties.AutoHeight = False
    Me.edEt_caricf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caricf.Properties.MaxLength = 65536
    Me.edEt_caricf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caricf.Size = New System.Drawing.Size(194, 20)
    Me.edEt_caricf.TabIndex = 626
    '
    'lbEt_caricf
    '
    Me.lbEt_caricf.AutoSize = True
    Me.lbEt_caricf.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caricf.Location = New System.Drawing.Point(8, 171)
    Me.lbEt_caricf.Name = "lbEt_caricf"
    Me.lbEt_caricf.NTSDbField = ""
    Me.lbEt_caricf.Size = New System.Drawing.Size(63, 13)
    Me.lbEt_caricf.TabIndex = 622
    Me.lbEt_caricf.Text = "Cod. fiscale"
    Me.lbEt_caricf.Tooltip = ""
    Me.lbEt_caricf.UseMnemonic = False
    '
    'lbEt_caritel
    '
    Me.lbEt_caritel.AutoSize = True
    Me.lbEt_caritel.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caritel.Location = New System.Drawing.Point(8, 197)
    Me.lbEt_caritel.Name = "lbEt_caritel"
    Me.lbEt_caritel.NTSDbField = ""
    Me.lbEt_caritel.Size = New System.Drawing.Size(49, 13)
    Me.lbEt_caritel.TabIndex = 624
    Me.lbEt_caritel.Text = "Telefono"
    Me.lbEt_caritel.Tooltip = ""
    Me.lbEt_caritel.UseMnemonic = False
    '
    'lbXx_caristat
    '
    Me.lbXx_caristat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caristat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caristat.Location = New System.Drawing.Point(511, 142)
    Me.lbXx_caristat.Name = "lbXx_caristat"
    Me.lbXx_caristat.NTSDbField = ""
    Me.lbXx_caristat.Size = New System.Drawing.Size(125, 20)
    Me.lbXx_caristat.TabIndex = 621
    Me.lbXx_caristat.Tooltip = ""
    Me.lbXx_caristat.UseMnemonic = False
    '
    'lbEt_caricitt
    '
    Me.lbEt_caricitt.AutoSize = True
    Me.lbEt_caricitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caricitt.Location = New System.Drawing.Point(8, 119)
    Me.lbEt_caricitt.Name = "lbEt_caricitt"
    Me.lbEt_caricitt.NTSDbField = ""
    Me.lbEt_caricitt.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_caricitt.TabIndex = 613
    Me.lbEt_caricitt.Text = "Citta/località"
    Me.lbEt_caricitt.Tooltip = ""
    Me.lbEt_caricitt.UseMnemonic = False
    '
    'edEt_caricitt
    '
    Me.edEt_caricitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caricitt.Location = New System.Drawing.Point(115, 116)
    Me.edEt_caricitt.Name = "edEt_caricitt"
    Me.edEt_caricitt.NTSDbField = ""
    Me.edEt_caricitt.NTSForzaVisZoom = False
    Me.edEt_caricitt.NTSOldValue = ""
    Me.edEt_caricitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caricitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caricitt.Properties.AutoHeight = False
    Me.edEt_caricitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caricitt.Properties.MaxLength = 65536
    Me.edEt_caricitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caricitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_caricitt.TabIndex = 618
    '
    'edEt_cariind
    '
    Me.edEt_cariind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cariind.Location = New System.Drawing.Point(115, 90)
    Me.edEt_cariind.Name = "edEt_cariind"
    Me.edEt_cariind.NTSDbField = ""
    Me.edEt_cariind.NTSForzaVisZoom = False
    Me.edEt_cariind.NTSOldValue = ""
    Me.edEt_cariind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_cariind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_cariind.Properties.AutoHeight = False
    Me.edEt_cariind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cariind.Properties.MaxLength = 65536
    Me.edEt_cariind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cariind.Size = New System.Drawing.Size(521, 20)
    Me.edEt_cariind.TabIndex = 616
    '
    'lbEt_caristat
    '
    Me.lbEt_caristat.AutoSize = True
    Me.lbEt_caristat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caristat.Location = New System.Drawing.Point(335, 143)
    Me.lbEt_caristat.Name = "lbEt_caristat"
    Me.lbEt_caristat.NTSDbField = ""
    Me.lbEt_caristat.Size = New System.Drawing.Size(58, 13)
    Me.lbEt_caristat.TabIndex = 615
    Me.lbEt_caristat.Text = "Cod. stato"
    Me.lbEt_caristat.Tooltip = ""
    Me.lbEt_caristat.UseMnemonic = False
    '
    'lbEt_caricap
    '
    Me.lbEt_caricap.AutoSize = True
    Me.lbEt_caricap.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caricap.Location = New System.Drawing.Point(8, 145)
    Me.lbEt_caricap.Name = "lbEt_caricap"
    Me.lbEt_caricap.NTSDbField = ""
    Me.lbEt_caricap.Size = New System.Drawing.Size(26, 13)
    Me.lbEt_caricap.TabIndex = 612
    Me.lbEt_caricap.Text = "Cap"
    Me.lbEt_caricap.Tooltip = ""
    Me.lbEt_caricap.UseMnemonic = False
    '
    'edEt_caristat
    '
    Me.edEt_caristat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caristat.Location = New System.Drawing.Point(442, 142)
    Me.edEt_caristat.Name = "edEt_caristat"
    Me.edEt_caristat.NTSDbField = ""
    Me.edEt_caristat.NTSForzaVisZoom = False
    Me.edEt_caristat.NTSOldValue = ""
    Me.edEt_caristat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caristat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caristat.Properties.AutoHeight = False
    Me.edEt_caristat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caristat.Properties.MaxLength = 65536
    Me.edEt_caristat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caristat.Size = New System.Drawing.Size(63, 20)
    Me.edEt_caristat.TabIndex = 620
    '
    'lbEt_cariind
    '
    Me.lbEt_cariind.AutoSize = True
    Me.lbEt_cariind.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cariind.Location = New System.Drawing.Point(8, 93)
    Me.lbEt_cariind.Name = "lbEt_cariind"
    Me.lbEt_cariind.NTSDbField = ""
    Me.lbEt_cariind.Size = New System.Drawing.Size(47, 13)
    Me.lbEt_cariind.TabIndex = 611
    Me.lbEt_cariind.Text = "Indirizzo"
    Me.lbEt_cariind.Tooltip = ""
    Me.lbEt_cariind.UseMnemonic = False
    '
    'edEt_caricap
    '
    Me.edEt_caricap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caricap.Location = New System.Drawing.Point(115, 142)
    Me.edEt_caricap.Name = "edEt_caricap"
    Me.edEt_caricap.NTSDbField = ""
    Me.edEt_caricap.NTSForzaVisZoom = False
    Me.edEt_caricap.NTSOldValue = ""
    Me.edEt_caricap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caricap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caricap.Properties.AutoHeight = False
    Me.edEt_caricap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caricap.Properties.MaxLength = 65536
    Me.edEt_caricap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caricap.Size = New System.Drawing.Size(63, 20)
    Me.edEt_caricap.TabIndex = 617
    '
    'edEt_cariprov
    '
    Me.edEt_cariprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cariprov.Location = New System.Drawing.Point(263, 142)
    Me.edEt_cariprov.Name = "edEt_cariprov"
    Me.edEt_cariprov.NTSDbField = ""
    Me.edEt_cariprov.NTSForzaVisZoom = False
    Me.edEt_cariprov.NTSOldValue = ""
    Me.edEt_cariprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_cariprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_cariprov.Properties.AutoHeight = False
    Me.edEt_cariprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cariprov.Properties.MaxLength = 65536
    Me.edEt_cariprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cariprov.Size = New System.Drawing.Size(45, 20)
    Me.edEt_cariprov.TabIndex = 619
    '
    'lbEt_cariprov
    '
    Me.lbEt_cariprov.AutoSize = True
    Me.lbEt_cariprov.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cariprov.Location = New System.Drawing.Point(184, 145)
    Me.lbEt_cariprov.Name = "lbEt_cariprov"
    Me.lbEt_cariprov.NTSDbField = ""
    Me.lbEt_cariprov.Size = New System.Drawing.Size(50, 13)
    Me.lbEt_cariprov.TabIndex = 614
    Me.lbEt_cariprov.Text = "Provincia"
    Me.lbEt_cariprov.Tooltip = ""
    Me.lbEt_cariprov.UseMnemonic = False
    '
    'edEt_carirs2
    '
    Me.edEt_carirs2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_carirs2.Location = New System.Drawing.Point(115, 64)
    Me.edEt_carirs2.Name = "edEt_carirs2"
    Me.edEt_carirs2.NTSDbField = ""
    Me.edEt_carirs2.NTSForzaVisZoom = False
    Me.edEt_carirs2.NTSOldValue = ""
    Me.edEt_carirs2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_carirs2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_carirs2.Properties.AutoHeight = False
    Me.edEt_carirs2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_carirs2.Properties.MaxLength = 65536
    Me.edEt_carirs2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_carirs2.Size = New System.Drawing.Size(521, 20)
    Me.edEt_carirs2.TabIndex = 610
    '
    'lbEt_carirs1
    '
    Me.lbEt_carirs1.AutoSize = True
    Me.lbEt_carirs1.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_carirs1.Location = New System.Drawing.Point(8, 41)
    Me.lbEt_carirs1.Name = "lbEt_carirs1"
    Me.lbEt_carirs1.NTSDbField = ""
    Me.lbEt_carirs1.Size = New System.Drawing.Size(65, 13)
    Me.lbEt_carirs1.TabIndex = 608
    Me.lbEt_carirs1.Text = "Rag. sociale"
    Me.lbEt_carirs1.Tooltip = ""
    Me.lbEt_carirs1.UseMnemonic = False
    '
    'edEt_carirs1
    '
    Me.edEt_carirs1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_carirs1.Location = New System.Drawing.Point(115, 38)
    Me.edEt_carirs1.Name = "edEt_carirs1"
    Me.edEt_carirs1.NTSDbField = ""
    Me.edEt_carirs1.NTSForzaVisZoom = False
    Me.edEt_carirs1.NTSOldValue = ""
    Me.edEt_carirs1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_carirs1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_carirs1.Properties.AutoHeight = False
    Me.edEt_carirs1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_carirs1.Properties.MaxLength = 65536
    Me.edEt_carirs1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_carirs1.Size = New System.Drawing.Size(521, 20)
    Me.edEt_carirs1.TabIndex = 609
    '
    'lbEt_caridest
    '
    Me.lbEt_caridest.AutoSize = True
    Me.lbEt_caridest.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caridest.Location = New System.Drawing.Point(462, 15)
    Me.lbEt_caridest.Name = "lbEt_caridest"
    Me.lbEt_caridest.NTSDbField = ""
    Me.lbEt_caridest.Size = New System.Drawing.Size(93, 13)
    Me.lbEt_caridest.TabIndex = 607
    Me.lbEt_caridest.Text = "Cod. destinazione"
    Me.lbEt_caridest.Tooltip = ""
    Me.lbEt_caridest.UseMnemonic = False
    '
    'edEt_caridest
    '
    Me.edEt_caridest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caridest.Location = New System.Drawing.Point(569, 12)
    Me.edEt_caridest.Name = "edEt_caridest"
    Me.edEt_caridest.NTSDbField = ""
    Me.edEt_caridest.NTSFormat = "0"
    Me.edEt_caridest.NTSForzaVisZoom = False
    Me.edEt_caridest.NTSOldValue = ""
    Me.edEt_caridest.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_caridest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_caridest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caridest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caridest.Properties.AutoHeight = False
    Me.edEt_caridest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caridest.Properties.MaxLength = 65536
    Me.edEt_caridest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caridest.Size = New System.Drawing.Size(67, 20)
    Me.edEt_caridest.TabIndex = 606
    '
    'lbEt_caricod
    '
    Me.lbEt_caricod.AutoSize = True
    Me.lbEt_caricod.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caricod.Location = New System.Drawing.Point(259, 15)
    Me.lbEt_caricod.Name = "lbEt_caricod"
    Me.lbEt_caricod.NTSDbField = ""
    Me.lbEt_caricod.Size = New System.Drawing.Size(98, 13)
    Me.lbEt_caricod.TabIndex = 604
    Me.lbEt_caricod.Text = "Cod. soggetto car."
    Me.lbEt_caricod.Tooltip = ""
    Me.lbEt_caricod.UseMnemonic = False
    '
    'edEt_caricod
    '
    Me.edEt_caricod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_caricod.Location = New System.Drawing.Point(366, 12)
    Me.edEt_caricod.Name = "edEt_caricod"
    Me.edEt_caricod.NTSDbField = ""
    Me.edEt_caricod.NTSFormat = "0"
    Me.edEt_caricod.NTSForzaVisZoom = False
    Me.edEt_caricod.NTSOldValue = ""
    Me.edEt_caricod.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_caricod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_caricod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_caricod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_caricod.Properties.AutoHeight = False
    Me.edEt_caricod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_caricod.Properties.MaxLength = 65536
    Me.edEt_caricod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_caricod.Size = New System.Drawing.Size(90, 20)
    Me.edEt_caricod.TabIndex = 603
    '
    'lbEt_caritipo
    '
    Me.lbEt_caritipo.AutoSize = True
    Me.lbEt_caritipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_caritipo.Location = New System.Drawing.Point(8, 15)
    Me.lbEt_caritipo.Name = "lbEt_caritipo"
    Me.lbEt_caritipo.NTSDbField = ""
    Me.lbEt_caritipo.Size = New System.Drawing.Size(57, 13)
    Me.lbEt_caritipo.TabIndex = 601
    Me.lbEt_caritipo.Text = "Caricatore"
    Me.lbEt_caritipo.Tooltip = ""
    Me.lbEt_caritipo.UseMnemonic = False
    '
    'cbEt_caritipo
    '
    Me.cbEt_caritipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_caritipo.DataSource = Nothing
    Me.cbEt_caritipo.DisplayMember = ""
    Me.cbEt_caritipo.EditValue = ""
    Me.cbEt_caritipo.Location = New System.Drawing.Point(115, 12)
    Me.cbEt_caritipo.Name = "cbEt_caritipo"
    Me.cbEt_caritipo.NTSDbField = ""
    Me.cbEt_caritipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_caritipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_caritipo.Properties.AutoHeight = False
    Me.cbEt_caritipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_caritipo.Properties.DropDownRows = 30
    Me.cbEt_caritipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_caritipo.SelectedValue = ""
    Me.cbEt_caritipo.Size = New System.Drawing.Size(140, 20)
    Me.cbEt_caritipo.TabIndex = 600
    Me.cbEt_caritipo.ValueMember = ""
    '
    'tsSctrPage3
    '
    Me.tsSctrPage3.AllowDrop = True
    Me.tsSctrPage3.Controls.Add(Me.pnSctrPage3)
    Me.tsSctrPage3.Enable = True
    Me.tsSctrPage3.Name = "tsSctrPage3"
    Me.tsSctrPage3.Size = New System.Drawing.Size(652, 299)
    Me.tsSctrPage3.Text = "Proprietario"
    '
    'pnSctrPage3
    '
    Me.pnSctrPage3.AllowDrop = True
    Me.pnSctrPage3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctrPage3.Appearance.Options.UseBackColor = True
    Me.pnSctrPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctrPage3.Controls.Add(Me.lbEt_evendich)
    Me.pnSctrPage3.Controls.Add(Me.edEt_evendich)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propmail)
    Me.pnSctrPage3.Controls.Add(Me.edEt_proppi)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propmail)
    Me.pnSctrPage3.Controls.Add(Me.edEt_proptel)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_proppi)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propcf)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propcf)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_proptel)
    Me.pnSctrPage3.Controls.Add(Me.lbXx_propstat)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propcitt)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propcitt)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propind)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propstat)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propcap)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propstat)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propind)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propcap)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propprov)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propprov)
    Me.pnSctrPage3.Controls.Add(Me.edEt_proprs2)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_proprs1)
    Me.pnSctrPage3.Controls.Add(Me.edEt_proprs1)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propdest)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propdest)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_propcod)
    Me.pnSctrPage3.Controls.Add(Me.edEt_propcod)
    Me.pnSctrPage3.Controls.Add(Me.lbEt_proptipo)
    Me.pnSctrPage3.Controls.Add(Me.cbEt_proptipo)
    Me.pnSctrPage3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctrPage3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctrPage3.Location = New System.Drawing.Point(0, 0)
    Me.pnSctrPage3.Name = "pnSctrPage3"
    Me.pnSctrPage3.NTSActiveTrasparency = True
    Me.pnSctrPage3.Size = New System.Drawing.Size(652, 299)
    Me.pnSctrPage3.TabIndex = 1
    Me.pnSctrPage3.Text = "NtsPanel2"
    '
    'lbEt_evendich
    '
    Me.lbEt_evendich.AutoSize = True
    Me.lbEt_evendich.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_evendich.Location = New System.Drawing.Point(8, 222)
    Me.lbEt_evendich.Name = "lbEt_evendich"
    Me.lbEt_evendich.NTSDbField = ""
    Me.lbEt_evendich.Size = New System.Drawing.Size(77, 13)
    Me.lbEt_evendich.TabIndex = 661
    Me.lbEt_evendich.Text = "Eventuali dich."
    Me.lbEt_evendich.Tooltip = ""
    Me.lbEt_evendich.UseMnemonic = False
    '
    'edEt_evendich
    '
    Me.edEt_evendich.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_evendich.Location = New System.Drawing.Point(115, 220)
    Me.edEt_evendich.Name = "edEt_evendich"
    Me.edEt_evendich.NTSDbField = ""
    Me.edEt_evendich.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_evendich.Size = New System.Drawing.Size(521, 58)
    Me.edEt_evendich.TabIndex = 660
    '
    'edEt_propmail
    '
    Me.edEt_propmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propmail.Location = New System.Drawing.Point(442, 194)
    Me.edEt_propmail.Name = "edEt_propmail"
    Me.edEt_propmail.NTSDbField = ""
    Me.edEt_propmail.NTSForzaVisZoom = False
    Me.edEt_propmail.NTSOldValue = ""
    Me.edEt_propmail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propmail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propmail.Properties.AutoHeight = False
    Me.edEt_propmail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propmail.Properties.MaxLength = 65536
    Me.edEt_propmail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propmail.Size = New System.Drawing.Size(194, 20)
    Me.edEt_propmail.TabIndex = 659
    '
    'edEt_proppi
    '
    Me.edEt_proppi.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edEt_proppi.Location = New System.Drawing.Point(442, 168)
    Me.edEt_proppi.Name = "edEt_proppi"
    Me.edEt_proppi.NTSDbField = ""
    Me.edEt_proppi.NTSForzaVisZoom = False
    Me.edEt_proppi.NTSOldValue = ""
    Me.edEt_proppi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_proppi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_proppi.Properties.AutoHeight = False
    Me.edEt_proppi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_proppi.Properties.MaxLength = 65536
    Me.edEt_proppi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_proppi.Size = New System.Drawing.Size(194, 20)
    Me.edEt_proppi.TabIndex = 657
    '
    'lbEt_propmail
    '
    Me.lbEt_propmail.AutoSize = True
    Me.lbEt_propmail.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propmail.Location = New System.Drawing.Point(335, 197)
    Me.lbEt_propmail.Name = "lbEt_propmail"
    Me.lbEt_propmail.NTSDbField = ""
    Me.lbEt_propmail.Size = New System.Drawing.Size(35, 13)
    Me.lbEt_propmail.TabIndex = 655
    Me.lbEt_propmail.Text = "E-mail"
    Me.lbEt_propmail.Tooltip = ""
    Me.lbEt_propmail.UseMnemonic = False
    '
    'edEt_proptel
    '
    Me.edEt_proptel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_proptel.Location = New System.Drawing.Point(115, 194)
    Me.edEt_proptel.Name = "edEt_proptel"
    Me.edEt_proptel.NTSDbField = ""
    Me.edEt_proptel.NTSForzaVisZoom = False
    Me.edEt_proptel.NTSOldValue = ""
    Me.edEt_proptel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_proptel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_proptel.Properties.AutoHeight = False
    Me.edEt_proptel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_proptel.Properties.MaxLength = 65536
    Me.edEt_proptel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_proptel.Size = New System.Drawing.Size(194, 20)
    Me.edEt_proptel.TabIndex = 658
    '
    'lbEt_proppi
    '
    Me.lbEt_proppi.AutoSize = True
    Me.lbEt_proppi.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_proppi.Location = New System.Drawing.Point(335, 171)
    Me.lbEt_proppi.Name = "lbEt_proppi"
    Me.lbEt_proppi.NTSDbField = ""
    Me.lbEt_proppi.Size = New System.Drawing.Size(59, 13)
    Me.lbEt_proppi.TabIndex = 653
    Me.lbEt_proppi.Text = "Partita IVA"
    Me.lbEt_proppi.Tooltip = ""
    Me.lbEt_proppi.UseMnemonic = False
    '
    'edEt_propcf
    '
    Me.edEt_propcf.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edEt_propcf.Location = New System.Drawing.Point(115, 168)
    Me.edEt_propcf.Name = "edEt_propcf"
    Me.edEt_propcf.NTSDbField = ""
    Me.edEt_propcf.NTSForzaVisZoom = False
    Me.edEt_propcf.NTSOldValue = ""
    Me.edEt_propcf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propcf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propcf.Properties.AutoHeight = False
    Me.edEt_propcf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propcf.Properties.MaxLength = 65536
    Me.edEt_propcf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propcf.Size = New System.Drawing.Size(194, 20)
    Me.edEt_propcf.TabIndex = 656
    '
    'lbEt_propcf
    '
    Me.lbEt_propcf.AutoSize = True
    Me.lbEt_propcf.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propcf.Location = New System.Drawing.Point(8, 171)
    Me.lbEt_propcf.Name = "lbEt_propcf"
    Me.lbEt_propcf.NTSDbField = ""
    Me.lbEt_propcf.Size = New System.Drawing.Size(63, 13)
    Me.lbEt_propcf.TabIndex = 652
    Me.lbEt_propcf.Text = "Cod. fiscale"
    Me.lbEt_propcf.Tooltip = ""
    Me.lbEt_propcf.UseMnemonic = False
    '
    'lbEt_proptel
    '
    Me.lbEt_proptel.AutoSize = True
    Me.lbEt_proptel.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_proptel.Location = New System.Drawing.Point(8, 197)
    Me.lbEt_proptel.Name = "lbEt_proptel"
    Me.lbEt_proptel.NTSDbField = ""
    Me.lbEt_proptel.Size = New System.Drawing.Size(49, 13)
    Me.lbEt_proptel.TabIndex = 654
    Me.lbEt_proptel.Text = "Telefono"
    Me.lbEt_proptel.Tooltip = ""
    Me.lbEt_proptel.UseMnemonic = False
    '
    'lbXx_propstat
    '
    Me.lbXx_propstat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_propstat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_propstat.Location = New System.Drawing.Point(511, 142)
    Me.lbXx_propstat.Name = "lbXx_propstat"
    Me.lbXx_propstat.NTSDbField = ""
    Me.lbXx_propstat.Size = New System.Drawing.Size(125, 20)
    Me.lbXx_propstat.TabIndex = 651
    Me.lbXx_propstat.Tooltip = ""
    Me.lbXx_propstat.UseMnemonic = False
    '
    'lbEt_propcitt
    '
    Me.lbEt_propcitt.AutoSize = True
    Me.lbEt_propcitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propcitt.Location = New System.Drawing.Point(8, 119)
    Me.lbEt_propcitt.Name = "lbEt_propcitt"
    Me.lbEt_propcitt.NTSDbField = ""
    Me.lbEt_propcitt.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_propcitt.TabIndex = 643
    Me.lbEt_propcitt.Text = "Citta/località"
    Me.lbEt_propcitt.Tooltip = ""
    Me.lbEt_propcitt.UseMnemonic = False
    '
    'edEt_propcitt
    '
    Me.edEt_propcitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propcitt.Location = New System.Drawing.Point(115, 116)
    Me.edEt_propcitt.Name = "edEt_propcitt"
    Me.edEt_propcitt.NTSDbField = ""
    Me.edEt_propcitt.NTSForzaVisZoom = False
    Me.edEt_propcitt.NTSOldValue = ""
    Me.edEt_propcitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propcitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propcitt.Properties.AutoHeight = False
    Me.edEt_propcitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propcitt.Properties.MaxLength = 65536
    Me.edEt_propcitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propcitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_propcitt.TabIndex = 648
    '
    'edEt_propind
    '
    Me.edEt_propind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propind.Location = New System.Drawing.Point(115, 90)
    Me.edEt_propind.Name = "edEt_propind"
    Me.edEt_propind.NTSDbField = ""
    Me.edEt_propind.NTSForzaVisZoom = False
    Me.edEt_propind.NTSOldValue = ""
    Me.edEt_propind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propind.Properties.AutoHeight = False
    Me.edEt_propind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propind.Properties.MaxLength = 65536
    Me.edEt_propind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propind.Size = New System.Drawing.Size(521, 20)
    Me.edEt_propind.TabIndex = 646
    '
    'lbEt_propstat
    '
    Me.lbEt_propstat.AutoSize = True
    Me.lbEt_propstat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propstat.Location = New System.Drawing.Point(335, 143)
    Me.lbEt_propstat.Name = "lbEt_propstat"
    Me.lbEt_propstat.NTSDbField = ""
    Me.lbEt_propstat.Size = New System.Drawing.Size(58, 13)
    Me.lbEt_propstat.TabIndex = 645
    Me.lbEt_propstat.Text = "Cod. stato"
    Me.lbEt_propstat.Tooltip = ""
    Me.lbEt_propstat.UseMnemonic = False
    '
    'lbEt_propcap
    '
    Me.lbEt_propcap.AutoSize = True
    Me.lbEt_propcap.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propcap.Location = New System.Drawing.Point(8, 145)
    Me.lbEt_propcap.Name = "lbEt_propcap"
    Me.lbEt_propcap.NTSDbField = ""
    Me.lbEt_propcap.Size = New System.Drawing.Size(26, 13)
    Me.lbEt_propcap.TabIndex = 642
    Me.lbEt_propcap.Text = "Cap"
    Me.lbEt_propcap.Tooltip = ""
    Me.lbEt_propcap.UseMnemonic = False
    '
    'edEt_propstat
    '
    Me.edEt_propstat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propstat.Location = New System.Drawing.Point(442, 142)
    Me.edEt_propstat.Name = "edEt_propstat"
    Me.edEt_propstat.NTSDbField = ""
    Me.edEt_propstat.NTSForzaVisZoom = False
    Me.edEt_propstat.NTSOldValue = ""
    Me.edEt_propstat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propstat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propstat.Properties.AutoHeight = False
    Me.edEt_propstat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propstat.Properties.MaxLength = 65536
    Me.edEt_propstat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propstat.Size = New System.Drawing.Size(63, 20)
    Me.edEt_propstat.TabIndex = 650
    '
    'lbEt_propind
    '
    Me.lbEt_propind.AutoSize = True
    Me.lbEt_propind.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propind.Location = New System.Drawing.Point(8, 93)
    Me.lbEt_propind.Name = "lbEt_propind"
    Me.lbEt_propind.NTSDbField = ""
    Me.lbEt_propind.Size = New System.Drawing.Size(47, 13)
    Me.lbEt_propind.TabIndex = 641
    Me.lbEt_propind.Text = "Indirizzo"
    Me.lbEt_propind.Tooltip = ""
    Me.lbEt_propind.UseMnemonic = False
    '
    'edEt_propcap
    '
    Me.edEt_propcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propcap.Location = New System.Drawing.Point(115, 142)
    Me.edEt_propcap.Name = "edEt_propcap"
    Me.edEt_propcap.NTSDbField = ""
    Me.edEt_propcap.NTSForzaVisZoom = False
    Me.edEt_propcap.NTSOldValue = ""
    Me.edEt_propcap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propcap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propcap.Properties.AutoHeight = False
    Me.edEt_propcap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propcap.Properties.MaxLength = 65536
    Me.edEt_propcap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propcap.Size = New System.Drawing.Size(63, 20)
    Me.edEt_propcap.TabIndex = 647
    '
    'edEt_propprov
    '
    Me.edEt_propprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propprov.Location = New System.Drawing.Point(263, 142)
    Me.edEt_propprov.Name = "edEt_propprov"
    Me.edEt_propprov.NTSDbField = ""
    Me.edEt_propprov.NTSForzaVisZoom = False
    Me.edEt_propprov.NTSOldValue = ""
    Me.edEt_propprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propprov.Properties.AutoHeight = False
    Me.edEt_propprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propprov.Properties.MaxLength = 65536
    Me.edEt_propprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propprov.Size = New System.Drawing.Size(45, 20)
    Me.edEt_propprov.TabIndex = 649
    '
    'lbEt_propprov
    '
    Me.lbEt_propprov.AutoSize = True
    Me.lbEt_propprov.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propprov.Location = New System.Drawing.Point(184, 145)
    Me.lbEt_propprov.Name = "lbEt_propprov"
    Me.lbEt_propprov.NTSDbField = ""
    Me.lbEt_propprov.Size = New System.Drawing.Size(50, 13)
    Me.lbEt_propprov.TabIndex = 644
    Me.lbEt_propprov.Text = "Provincia"
    Me.lbEt_propprov.Tooltip = ""
    Me.lbEt_propprov.UseMnemonic = False
    '
    'edEt_proprs2
    '
    Me.edEt_proprs2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_proprs2.Location = New System.Drawing.Point(115, 64)
    Me.edEt_proprs2.Name = "edEt_proprs2"
    Me.edEt_proprs2.NTSDbField = ""
    Me.edEt_proprs2.NTSForzaVisZoom = False
    Me.edEt_proprs2.NTSOldValue = ""
    Me.edEt_proprs2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_proprs2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_proprs2.Properties.AutoHeight = False
    Me.edEt_proprs2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_proprs2.Properties.MaxLength = 65536
    Me.edEt_proprs2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_proprs2.Size = New System.Drawing.Size(521, 20)
    Me.edEt_proprs2.TabIndex = 640
    '
    'lbEt_proprs1
    '
    Me.lbEt_proprs1.AutoSize = True
    Me.lbEt_proprs1.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_proprs1.Location = New System.Drawing.Point(8, 41)
    Me.lbEt_proprs1.Name = "lbEt_proprs1"
    Me.lbEt_proprs1.NTSDbField = ""
    Me.lbEt_proprs1.Size = New System.Drawing.Size(65, 13)
    Me.lbEt_proprs1.TabIndex = 638
    Me.lbEt_proprs1.Text = "Rag. sociale"
    Me.lbEt_proprs1.Tooltip = ""
    Me.lbEt_proprs1.UseMnemonic = False
    '
    'edEt_proprs1
    '
    Me.edEt_proprs1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_proprs1.Location = New System.Drawing.Point(115, 38)
    Me.edEt_proprs1.Name = "edEt_proprs1"
    Me.edEt_proprs1.NTSDbField = ""
    Me.edEt_proprs1.NTSForzaVisZoom = False
    Me.edEt_proprs1.NTSOldValue = ""
    Me.edEt_proprs1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_proprs1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_proprs1.Properties.AutoHeight = False
    Me.edEt_proprs1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_proprs1.Properties.MaxLength = 65536
    Me.edEt_proprs1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_proprs1.Size = New System.Drawing.Size(521, 20)
    Me.edEt_proprs1.TabIndex = 639
    '
    'lbEt_propdest
    '
    Me.lbEt_propdest.AutoSize = True
    Me.lbEt_propdest.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propdest.Location = New System.Drawing.Point(462, 15)
    Me.lbEt_propdest.Name = "lbEt_propdest"
    Me.lbEt_propdest.NTSDbField = ""
    Me.lbEt_propdest.Size = New System.Drawing.Size(93, 13)
    Me.lbEt_propdest.TabIndex = 637
    Me.lbEt_propdest.Text = "Cod. destinazione"
    Me.lbEt_propdest.Tooltip = ""
    Me.lbEt_propdest.UseMnemonic = False
    '
    'edEt_propdest
    '
    Me.edEt_propdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propdest.Location = New System.Drawing.Point(569, 12)
    Me.edEt_propdest.Name = "edEt_propdest"
    Me.edEt_propdest.NTSDbField = ""
    Me.edEt_propdest.NTSFormat = "0"
    Me.edEt_propdest.NTSForzaVisZoom = False
    Me.edEt_propdest.NTSOldValue = ""
    Me.edEt_propdest.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_propdest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_propdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propdest.Properties.AutoHeight = False
    Me.edEt_propdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propdest.Properties.MaxLength = 65536
    Me.edEt_propdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propdest.Size = New System.Drawing.Size(67, 20)
    Me.edEt_propdest.TabIndex = 636
    '
    'lbEt_propcod
    '
    Me.lbEt_propcod.AutoSize = True
    Me.lbEt_propcod.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_propcod.Location = New System.Drawing.Point(259, 15)
    Me.lbEt_propcod.Name = "lbEt_propcod"
    Me.lbEt_propcod.NTSDbField = ""
    Me.lbEt_propcod.Size = New System.Drawing.Size(99, 13)
    Me.lbEt_propcod.TabIndex = 634
    Me.lbEt_propcod.Text = "Cod. soggetto pro."
    Me.lbEt_propcod.Tooltip = ""
    Me.lbEt_propcod.UseMnemonic = False
    '
    'edEt_propcod
    '
    Me.edEt_propcod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_propcod.Location = New System.Drawing.Point(366, 12)
    Me.edEt_propcod.Name = "edEt_propcod"
    Me.edEt_propcod.NTSDbField = ""
    Me.edEt_propcod.NTSFormat = "0"
    Me.edEt_propcod.NTSForzaVisZoom = False
    Me.edEt_propcod.NTSOldValue = ""
    Me.edEt_propcod.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_propcod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_propcod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_propcod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_propcod.Properties.AutoHeight = False
    Me.edEt_propcod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_propcod.Properties.MaxLength = 65536
    Me.edEt_propcod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_propcod.Size = New System.Drawing.Size(90, 20)
    Me.edEt_propcod.TabIndex = 633
    '
    'lbEt_proptipo
    '
    Me.lbEt_proptipo.AutoSize = True
    Me.lbEt_proptipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_proptipo.Location = New System.Drawing.Point(8, 15)
    Me.lbEt_proptipo.Name = "lbEt_proptipo"
    Me.lbEt_proptipo.NTSDbField = ""
    Me.lbEt_proptipo.Size = New System.Drawing.Size(63, 13)
    Me.lbEt_proptipo.TabIndex = 631
    Me.lbEt_proptipo.Text = "Proprietario"
    Me.lbEt_proptipo.Tooltip = ""
    Me.lbEt_proptipo.UseMnemonic = False
    '
    'cbEt_proptipo
    '
    Me.cbEt_proptipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_proptipo.DataSource = Nothing
    Me.cbEt_proptipo.DisplayMember = ""
    Me.cbEt_proptipo.EditValue = ""
    Me.cbEt_proptipo.Location = New System.Drawing.Point(115, 12)
    Me.cbEt_proptipo.Name = "cbEt_proptipo"
    Me.cbEt_proptipo.NTSDbField = ""
    Me.cbEt_proptipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_proptipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_proptipo.Properties.AutoHeight = False
    Me.cbEt_proptipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_proptipo.Properties.DropDownRows = 30
    Me.cbEt_proptipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_proptipo.SelectedValue = ""
    Me.cbEt_proptipo.Size = New System.Drawing.Size(140, 20)
    Me.cbEt_proptipo.TabIndex = 630
    Me.cbEt_proptipo.ValueMember = ""
    '
    'tsSctrPage4
    '
    Me.tsSctrPage4.AllowDrop = True
    Me.tsSctrPage4.Controls.Add(Me.pnSctrPage4)
    Me.tsSctrPage4.Enable = True
    Me.tsSctrPage4.Name = "tsSctrPage4"
    Me.tsSctrPage4.Size = New System.Drawing.Size(652, 299)
    Me.tsSctrPage4.Text = "Merci e luogo carico"
    '
    'pnSctrPage4
    '
    Me.pnSctrPage4.AllowDrop = True
    Me.pnSctrPage4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctrPage4.Appearance.Options.UseBackColor = True
    Me.pnSctrPage4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctrPage4.Controls.Add(Me.lbXx_lcstat)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lccitt)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lccitt)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lcind)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lcstat)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lccap)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lcstat)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lcind)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lccap)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lcprov)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lcprov)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lcdest)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lcdest)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lcconto)
    Me.pnSctrPage4.Controls.Add(Me.edEt_lcconto)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_lctipo)
    Me.pnSctrPage4.Controls.Add(Me.cbEt_lctipo)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_peso)
    Me.pnSctrPage4.Controls.Add(Me.edEt_peso)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_quant)
    Me.pnSctrPage4.Controls.Add(Me.edEt_quant)
    Me.pnSctrPage4.Controls.Add(Me.lbEt_tipmerce)
    Me.pnSctrPage4.Controls.Add(Me.edEt_tipmerce)
    Me.pnSctrPage4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctrPage4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctrPage4.Location = New System.Drawing.Point(0, 0)
    Me.pnSctrPage4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSctrPage4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSctrPage4.Name = "pnSctrPage4"
    Me.pnSctrPage4.NTSActiveTrasparency = True
    Me.pnSctrPage4.Size = New System.Drawing.Size(652, 299)
    Me.pnSctrPage4.TabIndex = 687
    Me.pnSctrPage4.Text = "NtsPanel3"
    '
    'lbXx_lcstat
    '
    Me.lbXx_lcstat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_lcstat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_lcstat.Location = New System.Drawing.Point(511, 180)
    Me.lbXx_lcstat.Name = "lbXx_lcstat"
    Me.lbXx_lcstat.NTSDbField = ""
    Me.lbXx_lcstat.Size = New System.Drawing.Size(125, 20)
    Me.lbXx_lcstat.TabIndex = 709
    Me.lbXx_lcstat.Tooltip = ""
    Me.lbXx_lcstat.UseMnemonic = False
    '
    'lbEt_lccitt
    '
    Me.lbEt_lccitt.AutoSize = True
    Me.lbEt_lccitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lccitt.Location = New System.Drawing.Point(8, 157)
    Me.lbEt_lccitt.Name = "lbEt_lccitt"
    Me.lbEt_lccitt.NTSDbField = ""
    Me.lbEt_lccitt.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_lccitt.TabIndex = 701
    Me.lbEt_lccitt.Text = "Citta/località"
    Me.lbEt_lccitt.Tooltip = ""
    Me.lbEt_lccitt.UseMnemonic = False
    '
    'edEt_lccitt
    '
    Me.edEt_lccitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lccitt.Location = New System.Drawing.Point(115, 154)
    Me.edEt_lccitt.Name = "edEt_lccitt"
    Me.edEt_lccitt.NTSDbField = ""
    Me.edEt_lccitt.NTSForzaVisZoom = False
    Me.edEt_lccitt.NTSOldValue = ""
    Me.edEt_lccitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lccitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lccitt.Properties.AutoHeight = False
    Me.edEt_lccitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lccitt.Properties.MaxLength = 65536
    Me.edEt_lccitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lccitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_lccitt.TabIndex = 706
    '
    'edEt_lcind
    '
    Me.edEt_lcind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lcind.Location = New System.Drawing.Point(115, 128)
    Me.edEt_lcind.Name = "edEt_lcind"
    Me.edEt_lcind.NTSDbField = ""
    Me.edEt_lcind.NTSForzaVisZoom = False
    Me.edEt_lcind.NTSOldValue = ""
    Me.edEt_lcind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lcind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lcind.Properties.AutoHeight = False
    Me.edEt_lcind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lcind.Properties.MaxLength = 65536
    Me.edEt_lcind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lcind.Size = New System.Drawing.Size(521, 20)
    Me.edEt_lcind.TabIndex = 704
    '
    'lbEt_lcstat
    '
    Me.lbEt_lcstat.AutoSize = True
    Me.lbEt_lcstat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lcstat.Location = New System.Drawing.Point(335, 181)
    Me.lbEt_lcstat.Name = "lbEt_lcstat"
    Me.lbEt_lcstat.NTSDbField = ""
    Me.lbEt_lcstat.Size = New System.Drawing.Size(58, 13)
    Me.lbEt_lcstat.TabIndex = 703
    Me.lbEt_lcstat.Text = "Cod. stato"
    Me.lbEt_lcstat.Tooltip = ""
    Me.lbEt_lcstat.UseMnemonic = False
    '
    'lbEt_lccap
    '
    Me.lbEt_lccap.AutoSize = True
    Me.lbEt_lccap.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lccap.Location = New System.Drawing.Point(8, 183)
    Me.lbEt_lccap.Name = "lbEt_lccap"
    Me.lbEt_lccap.NTSDbField = ""
    Me.lbEt_lccap.Size = New System.Drawing.Size(26, 13)
    Me.lbEt_lccap.TabIndex = 700
    Me.lbEt_lccap.Text = "Cap"
    Me.lbEt_lccap.Tooltip = ""
    Me.lbEt_lccap.UseMnemonic = False
    '
    'edEt_lcstat
    '
    Me.edEt_lcstat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lcstat.Location = New System.Drawing.Point(442, 180)
    Me.edEt_lcstat.Name = "edEt_lcstat"
    Me.edEt_lcstat.NTSDbField = ""
    Me.edEt_lcstat.NTSForzaVisZoom = False
    Me.edEt_lcstat.NTSOldValue = ""
    Me.edEt_lcstat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lcstat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lcstat.Properties.AutoHeight = False
    Me.edEt_lcstat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lcstat.Properties.MaxLength = 65536
    Me.edEt_lcstat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lcstat.Size = New System.Drawing.Size(63, 20)
    Me.edEt_lcstat.TabIndex = 708
    '
    'lbEt_lcind
    '
    Me.lbEt_lcind.AutoSize = True
    Me.lbEt_lcind.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lcind.Location = New System.Drawing.Point(8, 131)
    Me.lbEt_lcind.Name = "lbEt_lcind"
    Me.lbEt_lcind.NTSDbField = ""
    Me.lbEt_lcind.Size = New System.Drawing.Size(47, 13)
    Me.lbEt_lcind.TabIndex = 699
    Me.lbEt_lcind.Text = "Indirizzo"
    Me.lbEt_lcind.Tooltip = ""
    Me.lbEt_lcind.UseMnemonic = False
    '
    'edEt_lccap
    '
    Me.edEt_lccap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lccap.Location = New System.Drawing.Point(115, 180)
    Me.edEt_lccap.Name = "edEt_lccap"
    Me.edEt_lccap.NTSDbField = ""
    Me.edEt_lccap.NTSForzaVisZoom = False
    Me.edEt_lccap.NTSOldValue = ""
    Me.edEt_lccap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lccap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lccap.Properties.AutoHeight = False
    Me.edEt_lccap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lccap.Properties.MaxLength = 65536
    Me.edEt_lccap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lccap.Size = New System.Drawing.Size(63, 20)
    Me.edEt_lccap.TabIndex = 705
    '
    'edEt_lcprov
    '
    Me.edEt_lcprov.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edEt_lcprov.Location = New System.Drawing.Point(263, 180)
    Me.edEt_lcprov.Name = "edEt_lcprov"
    Me.edEt_lcprov.NTSDbField = ""
    Me.edEt_lcprov.NTSForzaVisZoom = False
    Me.edEt_lcprov.NTSOldValue = ""
    Me.edEt_lcprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lcprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lcprov.Properties.AutoHeight = False
    Me.edEt_lcprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lcprov.Properties.MaxLength = 65536
    Me.edEt_lcprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lcprov.Size = New System.Drawing.Size(45, 20)
    Me.edEt_lcprov.TabIndex = 707
    '
    'lbEt_lcprov
    '
    Me.lbEt_lcprov.AutoSize = True
    Me.lbEt_lcprov.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lcprov.Location = New System.Drawing.Point(184, 183)
    Me.lbEt_lcprov.Name = "lbEt_lcprov"
    Me.lbEt_lcprov.NTSDbField = ""
    Me.lbEt_lcprov.Size = New System.Drawing.Size(50, 13)
    Me.lbEt_lcprov.TabIndex = 702
    Me.lbEt_lcprov.Text = "Provincia"
    Me.lbEt_lcprov.Tooltip = ""
    Me.lbEt_lcprov.UseMnemonic = False
    '
    'lbEt_lcdest
    '
    Me.lbEt_lcdest.AutoSize = True
    Me.lbEt_lcdest.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lcdest.Location = New System.Drawing.Point(462, 105)
    Me.lbEt_lcdest.Name = "lbEt_lcdest"
    Me.lbEt_lcdest.NTSDbField = ""
    Me.lbEt_lcdest.Size = New System.Drawing.Size(93, 13)
    Me.lbEt_lcdest.TabIndex = 698
    Me.lbEt_lcdest.Text = "Cod. destinazione"
    Me.lbEt_lcdest.Tooltip = ""
    Me.lbEt_lcdest.UseMnemonic = False
    '
    'edEt_lcdest
    '
    Me.edEt_lcdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lcdest.Location = New System.Drawing.Point(569, 102)
    Me.edEt_lcdest.Name = "edEt_lcdest"
    Me.edEt_lcdest.NTSDbField = ""
    Me.edEt_lcdest.NTSFormat = "0"
    Me.edEt_lcdest.NTSForzaVisZoom = False
    Me.edEt_lcdest.NTSOldValue = ""
    Me.edEt_lcdest.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_lcdest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_lcdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lcdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lcdest.Properties.AutoHeight = False
    Me.edEt_lcdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lcdest.Properties.MaxLength = 65536
    Me.edEt_lcdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lcdest.Size = New System.Drawing.Size(67, 20)
    Me.edEt_lcdest.TabIndex = 697
    '
    'lbEt_lcconto
    '
    Me.lbEt_lcconto.AutoSize = True
    Me.lbEt_lcconto.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lcconto.Location = New System.Drawing.Point(259, 105)
    Me.lbEt_lcconto.Name = "lbEt_lcconto"
    Me.lbEt_lcconto.NTSDbField = ""
    Me.lbEt_lcconto.Size = New System.Drawing.Size(61, 13)
    Me.lbEt_lcconto.TabIndex = 696
    Me.lbEt_lcconto.Text = "Cod. carico"
    Me.lbEt_lcconto.Tooltip = ""
    Me.lbEt_lcconto.UseMnemonic = False
    '
    'edEt_lcconto
    '
    Me.edEt_lcconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lcconto.Location = New System.Drawing.Point(366, 102)
    Me.edEt_lcconto.Name = "edEt_lcconto"
    Me.edEt_lcconto.NTSDbField = ""
    Me.edEt_lcconto.NTSFormat = "0"
    Me.edEt_lcconto.NTSForzaVisZoom = False
    Me.edEt_lcconto.NTSOldValue = ""
    Me.edEt_lcconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_lcconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_lcconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lcconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lcconto.Properties.AutoHeight = False
    Me.edEt_lcconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lcconto.Properties.MaxLength = 65536
    Me.edEt_lcconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lcconto.Size = New System.Drawing.Size(90, 20)
    Me.edEt_lcconto.TabIndex = 695
    '
    'lbEt_lctipo
    '
    Me.lbEt_lctipo.AutoSize = True
    Me.lbEt_lctipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lctipo.Location = New System.Drawing.Point(8, 105)
    Me.lbEt_lctipo.Name = "lbEt_lctipo"
    Me.lbEt_lctipo.NTSDbField = ""
    Me.lbEt_lctipo.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_lctipo.TabIndex = 694
    Me.lbEt_lctipo.Text = "Luogo carico"
    Me.lbEt_lctipo.Tooltip = ""
    Me.lbEt_lctipo.UseMnemonic = False
    '
    'cbEt_lctipo
    '
    Me.cbEt_lctipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_lctipo.DataSource = Nothing
    Me.cbEt_lctipo.DisplayMember = ""
    Me.cbEt_lctipo.EditValue = ""
    Me.cbEt_lctipo.Location = New System.Drawing.Point(115, 102)
    Me.cbEt_lctipo.Name = "cbEt_lctipo"
    Me.cbEt_lctipo.NTSDbField = ""
    Me.cbEt_lctipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_lctipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_lctipo.Properties.AutoHeight = False
    Me.cbEt_lctipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_lctipo.Properties.DropDownRows = 30
    Me.cbEt_lctipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_lctipo.SelectedValue = ""
    Me.cbEt_lctipo.Size = New System.Drawing.Size(140, 20)
    Me.cbEt_lctipo.TabIndex = 693
    Me.cbEt_lctipo.ValueMember = ""
    '
    'lbEt_peso
    '
    Me.lbEt_peso.AutoSize = True
    Me.lbEt_peso.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_peso.Location = New System.Drawing.Point(371, 79)
    Me.lbEt_peso.Name = "lbEt_peso"
    Me.lbEt_peso.NTSDbField = ""
    Me.lbEt_peso.Size = New System.Drawing.Size(57, 13)
    Me.lbEt_peso.TabIndex = 692
    Me.lbEt_peso.Text = "Peso lordo"
    Me.lbEt_peso.Tooltip = ""
    Me.lbEt_peso.UseMnemonic = False
    '
    'edEt_peso
    '
    Me.edEt_peso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_peso.Location = New System.Drawing.Point(442, 76)
    Me.edEt_peso.Name = "edEt_peso"
    Me.edEt_peso.NTSDbField = ""
    Me.edEt_peso.NTSFormat = "0"
    Me.edEt_peso.NTSForzaVisZoom = False
    Me.edEt_peso.NTSOldValue = ""
    Me.edEt_peso.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_peso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_peso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_peso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_peso.Properties.AutoHeight = False
    Me.edEt_peso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_peso.Properties.MaxLength = 65536
    Me.edEt_peso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_peso.Size = New System.Drawing.Size(86, 20)
    Me.edEt_peso.TabIndex = 691
    '
    'lbEt_quant
    '
    Me.lbEt_quant.AutoSize = True
    Me.lbEt_quant.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_quant.Location = New System.Drawing.Point(8, 79)
    Me.lbEt_quant.Name = "lbEt_quant"
    Me.lbEt_quant.NTSDbField = ""
    Me.lbEt_quant.Size = New System.Drawing.Size(49, 13)
    Me.lbEt_quant.TabIndex = 690
    Me.lbEt_quant.Text = "Quantità"
    Me.lbEt_quant.Tooltip = ""
    Me.lbEt_quant.UseMnemonic = False
    '
    'edEt_quant
    '
    Me.edEt_quant.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edEt_quant.Location = New System.Drawing.Point(115, 76)
    Me.edEt_quant.Name = "edEt_quant"
    Me.edEt_quant.NTSDbField = ""
    Me.edEt_quant.NTSFormat = "0"
    Me.edEt_quant.NTSForzaVisZoom = False
    Me.edEt_quant.NTSOldValue = ""
    Me.edEt_quant.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_quant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_quant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_quant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_quant.Properties.AutoHeight = False
    Me.edEt_quant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_quant.Properties.MaxLength = 65536
    Me.edEt_quant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_quant.Size = New System.Drawing.Size(86, 20)
    Me.edEt_quant.TabIndex = 689
    '
    'lbEt_tipmerce
    '
    Me.lbEt_tipmerce.AutoSize = True
    Me.lbEt_tipmerce.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_tipmerce.Location = New System.Drawing.Point(9, 14)
    Me.lbEt_tipmerce.Name = "lbEt_tipmerce"
    Me.lbEt_tipmerce.NTSDbField = ""
    Me.lbEt_tipmerce.Size = New System.Drawing.Size(81, 13)
    Me.lbEt_tipmerce.TabIndex = 688
    Me.lbEt_tipmerce.Text = "Tipologia merce"
    Me.lbEt_tipmerce.Tooltip = ""
    Me.lbEt_tipmerce.UseMnemonic = False
    '
    'edEt_tipmerce
    '
    Me.edEt_tipmerce.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_tipmerce.Location = New System.Drawing.Point(115, 12)
    Me.edEt_tipmerce.Name = "edEt_tipmerce"
    Me.edEt_tipmerce.NTSDbField = ""
    Me.edEt_tipmerce.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_tipmerce.Size = New System.Drawing.Size(521, 58)
    Me.edEt_tipmerce.TabIndex = 687
    '
    'tsSctrPage5
    '
    Me.tsSctrPage5.AllowDrop = True
    Me.tsSctrPage5.Controls.Add(Me.pnSctrPage5)
    Me.tsSctrPage5.Enable = True
    Me.tsSctrPage5.Name = "tsSctrPage5"
    Me.tsSctrPage5.Size = New System.Drawing.Size(652, 299)
    Me.tsSctrPage5.Text = "Luogo scarico e altro"
    '
    'pnSctrPage5
    '
    Me.pnSctrPage5.AllowDrop = True
    Me.pnSctrPage5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSctrPage5.Appearance.Options.UseBackColor = True
    Me.pnSctrPage5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSctrPage5.Controls.Add(Me.lbXx_datcomp)
    Me.pnSctrPage5.Controls.Add(Me.edXx_datcomp)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_compilat)
    Me.pnSctrPage5.Controls.Add(Me.edEt_compilat)
    Me.pnSctrPage5.Controls.Add(Me.edEt_compcitt)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_compcitt)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_istruz)
    Me.pnSctrPage5.Controls.Add(Me.edEt_istruz)
    Me.pnSctrPage5.Controls.Add(Me.lbXx_lsstat)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lscitt)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lscitt)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lsind)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lsstat)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lscap)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lsstat)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lsind)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lscap)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lsprov)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lsprov)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lsdest)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lsdest)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lsconto)
    Me.pnSctrPage5.Controls.Add(Me.edEt_lsconto)
    Me.pnSctrPage5.Controls.Add(Me.lbEt_lstipo)
    Me.pnSctrPage5.Controls.Add(Me.cbEt_lstipo)
    Me.pnSctrPage5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSctrPage5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSctrPage5.Location = New System.Drawing.Point(0, 0)
    Me.pnSctrPage5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSctrPage5.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSctrPage5.Name = "pnSctrPage5"
    Me.pnSctrPage5.NTSActiveTrasparency = True
    Me.pnSctrPage5.Size = New System.Drawing.Size(652, 299)
    Me.pnSctrPage5.TabIndex = 714
    Me.pnSctrPage5.Text = "NtsPanel3"
    '
    'lbXx_datcomp
    '
    Me.lbXx_datcomp.AutoSize = True
    Me.lbXx_datcomp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_datcomp.Location = New System.Drawing.Point(8, 183)
    Me.lbXx_datcomp.Name = "lbXx_datcomp"
    Me.lbXx_datcomp.NTSDbField = ""
    Me.lbXx_datcomp.Size = New System.Drawing.Size(62, 13)
    Me.lbXx_datcomp.TabIndex = 738
    Me.lbXx_datcomp.Text = "Data comp."
    Me.lbXx_datcomp.Tooltip = ""
    Me.lbXx_datcomp.UseMnemonic = False
    '
    'edXx_datcomp
    '
    Me.edXx_datcomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_datcomp.Location = New System.Drawing.Point(115, 180)
    Me.edXx_datcomp.Name = "edXx_datcomp"
    Me.edXx_datcomp.NTSDbField = ""
    Me.edXx_datcomp.NTSForzaVisZoom = False
    Me.edXx_datcomp.NTSOldValue = ""
    Me.edXx_datcomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_datcomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_datcomp.Properties.AutoHeight = False
    Me.edXx_datcomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_datcomp.Properties.MaxLength = 65536
    Me.edXx_datcomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_datcomp.Size = New System.Drawing.Size(88, 20)
    Me.edXx_datcomp.TabIndex = 737
    '
    'lbEt_compilat
    '
    Me.lbEt_compilat.AutoSize = True
    Me.lbEt_compilat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_compilat.Location = New System.Drawing.Point(8, 235)
    Me.lbEt_compilat.Name = "lbEt_compilat"
    Me.lbEt_compilat.NTSDbField = ""
    Me.lbEt_compilat.Size = New System.Drawing.Size(97, 13)
    Me.lbEt_compilat.TabIndex = 734
    Me.lbEt_compilat.Text = "Nome/cogn. comp."
    Me.lbEt_compilat.Tooltip = ""
    Me.lbEt_compilat.UseMnemonic = False
    '
    'edEt_compilat
    '
    Me.edEt_compilat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_compilat.Location = New System.Drawing.Point(115, 232)
    Me.edEt_compilat.Name = "edEt_compilat"
    Me.edEt_compilat.NTSDbField = ""
    Me.edEt_compilat.NTSForzaVisZoom = False
    Me.edEt_compilat.NTSOldValue = ""
    Me.edEt_compilat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_compilat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_compilat.Properties.AutoHeight = False
    Me.edEt_compilat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_compilat.Properties.MaxLength = 65536
    Me.edEt_compilat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_compilat.Size = New System.Drawing.Size(521, 20)
    Me.edEt_compilat.TabIndex = 736
    '
    'edEt_compcitt
    '
    Me.edEt_compcitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_compcitt.Location = New System.Drawing.Point(115, 206)
    Me.edEt_compcitt.Name = "edEt_compcitt"
    Me.edEt_compcitt.NTSDbField = ""
    Me.edEt_compcitt.NTSForzaVisZoom = False
    Me.edEt_compcitt.NTSOldValue = ""
    Me.edEt_compcitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_compcitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_compcitt.Properties.AutoHeight = False
    Me.edEt_compcitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_compcitt.Properties.MaxLength = 65536
    Me.edEt_compcitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_compcitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_compcitt.TabIndex = 735
    '
    'lbEt_compcitt
    '
    Me.lbEt_compcitt.AutoSize = True
    Me.lbEt_compcitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_compcitt.Location = New System.Drawing.Point(8, 209)
    Me.lbEt_compcitt.Name = "lbEt_compcitt"
    Me.lbEt_compcitt.NTSDbField = ""
    Me.lbEt_compcitt.Size = New System.Drawing.Size(62, 13)
    Me.lbEt_compcitt.TabIndex = 733
    Me.lbEt_compcitt.Text = "Città comp."
    Me.lbEt_compcitt.Tooltip = ""
    Me.lbEt_compcitt.UseMnemonic = False
    '
    'lbEt_istruz
    '
    Me.lbEt_istruz.AutoSize = True
    Me.lbEt_istruz.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_istruz.Location = New System.Drawing.Point(8, 118)
    Me.lbEt_istruz.Name = "lbEt_istruz"
    Me.lbEt_istruz.NTSDbField = ""
    Me.lbEt_istruz.Size = New System.Drawing.Size(73, 13)
    Me.lbEt_istruz.TabIndex = 732
    Me.lbEt_istruz.Text = "Eventuali istr."
    Me.lbEt_istruz.Tooltip = ""
    Me.lbEt_istruz.UseMnemonic = False
    '
    'edEt_istruz
    '
    Me.edEt_istruz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_istruz.Location = New System.Drawing.Point(115, 118)
    Me.edEt_istruz.Name = "edEt_istruz"
    Me.edEt_istruz.NTSDbField = ""
    Me.edEt_istruz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_istruz.Size = New System.Drawing.Size(521, 58)
    Me.edEt_istruz.TabIndex = 731
    '
    'lbXx_lsstat
    '
    Me.lbXx_lsstat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_lsstat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_lsstat.Location = New System.Drawing.Point(511, 91)
    Me.lbXx_lsstat.Name = "lbXx_lsstat"
    Me.lbXx_lsstat.NTSDbField = ""
    Me.lbXx_lsstat.Size = New System.Drawing.Size(125, 20)
    Me.lbXx_lsstat.TabIndex = 730
    Me.lbXx_lsstat.Tooltip = ""
    Me.lbXx_lsstat.UseMnemonic = False
    '
    'lbEt_lscitt
    '
    Me.lbEt_lscitt.AutoSize = True
    Me.lbEt_lscitt.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lscitt.Location = New System.Drawing.Point(8, 67)
    Me.lbEt_lscitt.Name = "lbEt_lscitt"
    Me.lbEt_lscitt.NTSDbField = ""
    Me.lbEt_lscitt.Size = New System.Drawing.Size(67, 13)
    Me.lbEt_lscitt.TabIndex = 722
    Me.lbEt_lscitt.Text = "Citta/località"
    Me.lbEt_lscitt.Tooltip = ""
    Me.lbEt_lscitt.UseMnemonic = False
    '
    'edEt_lscitt
    '
    Me.edEt_lscitt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lscitt.Location = New System.Drawing.Point(115, 64)
    Me.edEt_lscitt.Name = "edEt_lscitt"
    Me.edEt_lscitt.NTSDbField = ""
    Me.edEt_lscitt.NTSForzaVisZoom = False
    Me.edEt_lscitt.NTSOldValue = ""
    Me.edEt_lscitt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lscitt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lscitt.Properties.AutoHeight = False
    Me.edEt_lscitt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lscitt.Properties.MaxLength = 65536
    Me.edEt_lscitt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lscitt.Size = New System.Drawing.Size(521, 20)
    Me.edEt_lscitt.TabIndex = 727
    '
    'edEt_lsind
    '
    Me.edEt_lsind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lsind.Location = New System.Drawing.Point(115, 38)
    Me.edEt_lsind.Name = "edEt_lsind"
    Me.edEt_lsind.NTSDbField = ""
    Me.edEt_lsind.NTSForzaVisZoom = False
    Me.edEt_lsind.NTSOldValue = ""
    Me.edEt_lsind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lsind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lsind.Properties.AutoHeight = False
    Me.edEt_lsind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lsind.Properties.MaxLength = 65536
    Me.edEt_lsind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lsind.Size = New System.Drawing.Size(521, 20)
    Me.edEt_lsind.TabIndex = 725
    '
    'lbEt_lsstat
    '
    Me.lbEt_lsstat.AutoSize = True
    Me.lbEt_lsstat.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lsstat.Location = New System.Drawing.Point(335, 91)
    Me.lbEt_lsstat.Name = "lbEt_lsstat"
    Me.lbEt_lsstat.NTSDbField = ""
    Me.lbEt_lsstat.Size = New System.Drawing.Size(58, 13)
    Me.lbEt_lsstat.TabIndex = 724
    Me.lbEt_lsstat.Text = "Cod. stato"
    Me.lbEt_lsstat.Tooltip = ""
    Me.lbEt_lsstat.UseMnemonic = False
    '
    'lbEt_lscap
    '
    Me.lbEt_lscap.AutoSize = True
    Me.lbEt_lscap.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lscap.Location = New System.Drawing.Point(8, 93)
    Me.lbEt_lscap.Name = "lbEt_lscap"
    Me.lbEt_lscap.NTSDbField = ""
    Me.lbEt_lscap.Size = New System.Drawing.Size(26, 13)
    Me.lbEt_lscap.TabIndex = 721
    Me.lbEt_lscap.Text = "Cap"
    Me.lbEt_lscap.Tooltip = ""
    Me.lbEt_lscap.UseMnemonic = False
    '
    'edEt_lsstat
    '
    Me.edEt_lsstat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lsstat.Location = New System.Drawing.Point(442, 90)
    Me.edEt_lsstat.Name = "edEt_lsstat"
    Me.edEt_lsstat.NTSDbField = ""
    Me.edEt_lsstat.NTSForzaVisZoom = False
    Me.edEt_lsstat.NTSOldValue = ""
    Me.edEt_lsstat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lsstat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lsstat.Properties.AutoHeight = False
    Me.edEt_lsstat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lsstat.Properties.MaxLength = 65536
    Me.edEt_lsstat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lsstat.Size = New System.Drawing.Size(63, 20)
    Me.edEt_lsstat.TabIndex = 729
    '
    'lbEt_lsind
    '
    Me.lbEt_lsind.AutoSize = True
    Me.lbEt_lsind.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lsind.Location = New System.Drawing.Point(8, 41)
    Me.lbEt_lsind.Name = "lbEt_lsind"
    Me.lbEt_lsind.NTSDbField = ""
    Me.lbEt_lsind.Size = New System.Drawing.Size(47, 13)
    Me.lbEt_lsind.TabIndex = 720
    Me.lbEt_lsind.Text = "Indirizzo"
    Me.lbEt_lsind.Tooltip = ""
    Me.lbEt_lsind.UseMnemonic = False
    '
    'edEt_lscap
    '
    Me.edEt_lscap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lscap.Location = New System.Drawing.Point(115, 90)
    Me.edEt_lscap.Name = "edEt_lscap"
    Me.edEt_lscap.NTSDbField = ""
    Me.edEt_lscap.NTSForzaVisZoom = False
    Me.edEt_lscap.NTSOldValue = ""
    Me.edEt_lscap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lscap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lscap.Properties.AutoHeight = False
    Me.edEt_lscap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lscap.Properties.MaxLength = 65536
    Me.edEt_lscap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lscap.Size = New System.Drawing.Size(63, 20)
    Me.edEt_lscap.TabIndex = 726
    '
    'edEt_lsprov
    '
    Me.edEt_lsprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lsprov.Location = New System.Drawing.Point(263, 90)
    Me.edEt_lsprov.Name = "edEt_lsprov"
    Me.edEt_lsprov.NTSDbField = ""
    Me.edEt_lsprov.NTSForzaVisZoom = False
    Me.edEt_lsprov.NTSOldValue = ""
    Me.edEt_lsprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lsprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lsprov.Properties.AutoHeight = False
    Me.edEt_lsprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lsprov.Properties.MaxLength = 65536
    Me.edEt_lsprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lsprov.Size = New System.Drawing.Size(45, 20)
    Me.edEt_lsprov.TabIndex = 728
    '
    'lbEt_lsprov
    '
    Me.lbEt_lsprov.AutoSize = True
    Me.lbEt_lsprov.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lsprov.Location = New System.Drawing.Point(184, 93)
    Me.lbEt_lsprov.Name = "lbEt_lsprov"
    Me.lbEt_lsprov.NTSDbField = ""
    Me.lbEt_lsprov.Size = New System.Drawing.Size(50, 13)
    Me.lbEt_lsprov.TabIndex = 723
    Me.lbEt_lsprov.Text = "Provincia"
    Me.lbEt_lsprov.Tooltip = ""
    Me.lbEt_lsprov.UseMnemonic = False
    '
    'lbEt_lsdest
    '
    Me.lbEt_lsdest.AutoSize = True
    Me.lbEt_lsdest.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lsdest.Location = New System.Drawing.Point(462, 15)
    Me.lbEt_lsdest.Name = "lbEt_lsdest"
    Me.lbEt_lsdest.NTSDbField = ""
    Me.lbEt_lsdest.Size = New System.Drawing.Size(93, 13)
    Me.lbEt_lsdest.TabIndex = 719
    Me.lbEt_lsdest.Text = "Cod. destinazione"
    Me.lbEt_lsdest.Tooltip = ""
    Me.lbEt_lsdest.UseMnemonic = False
    '
    'edEt_lsdest
    '
    Me.edEt_lsdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lsdest.Location = New System.Drawing.Point(569, 12)
    Me.edEt_lsdest.Name = "edEt_lsdest"
    Me.edEt_lsdest.NTSDbField = ""
    Me.edEt_lsdest.NTSFormat = "0"
    Me.edEt_lsdest.NTSForzaVisZoom = False
    Me.edEt_lsdest.NTSOldValue = ""
    Me.edEt_lsdest.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_lsdest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_lsdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lsdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lsdest.Properties.AutoHeight = False
    Me.edEt_lsdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lsdest.Properties.MaxLength = 65536
    Me.edEt_lsdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lsdest.Size = New System.Drawing.Size(67, 20)
    Me.edEt_lsdest.TabIndex = 718
    '
    'lbEt_lsconto
    '
    Me.lbEt_lsconto.AutoSize = True
    Me.lbEt_lsconto.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lsconto.Location = New System.Drawing.Point(259, 15)
    Me.lbEt_lsconto.Name = "lbEt_lsconto"
    Me.lbEt_lsconto.NTSDbField = ""
    Me.lbEt_lsconto.Size = New System.Drawing.Size(66, 13)
    Me.lbEt_lsconto.TabIndex = 717
    Me.lbEt_lsconto.Text = "Cod. scarico"
    Me.lbEt_lsconto.Tooltip = ""
    Me.lbEt_lsconto.UseMnemonic = False
    '
    'edEt_lsconto
    '
    Me.edEt_lsconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_lsconto.Location = New System.Drawing.Point(366, 12)
    Me.edEt_lsconto.Name = "edEt_lsconto"
    Me.edEt_lsconto.NTSDbField = ""
    Me.edEt_lsconto.NTSFormat = "0"
    Me.edEt_lsconto.NTSForzaVisZoom = False
    Me.edEt_lsconto.NTSOldValue = ""
    Me.edEt_lsconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_lsconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_lsconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_lsconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_lsconto.Properties.AutoHeight = False
    Me.edEt_lsconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_lsconto.Properties.MaxLength = 65536
    Me.edEt_lsconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_lsconto.Size = New System.Drawing.Size(90, 20)
    Me.edEt_lsconto.TabIndex = 716
    '
    'lbEt_lstipo
    '
    Me.lbEt_lstipo.AutoSize = True
    Me.lbEt_lstipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_lstipo.Location = New System.Drawing.Point(8, 15)
    Me.lbEt_lstipo.Name = "lbEt_lstipo"
    Me.lbEt_lstipo.NTSDbField = ""
    Me.lbEt_lstipo.Size = New System.Drawing.Size(72, 13)
    Me.lbEt_lstipo.TabIndex = 715
    Me.lbEt_lstipo.Text = "Luogo scarico"
    Me.lbEt_lstipo.Tooltip = ""
    Me.lbEt_lstipo.UseMnemonic = False
    '
    'cbEt_lstipo
    '
    Me.cbEt_lstipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_lstipo.DataSource = Nothing
    Me.cbEt_lstipo.DisplayMember = ""
    Me.cbEt_lstipo.EditValue = ""
    Me.cbEt_lstipo.Location = New System.Drawing.Point(115, 12)
    Me.cbEt_lstipo.Name = "cbEt_lstipo"
    Me.cbEt_lstipo.NTSDbField = ""
    Me.cbEt_lstipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_lstipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_lstipo.Properties.AutoHeight = False
    Me.cbEt_lstipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_lstipo.Properties.DropDownRows = 30
    Me.cbEt_lstipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_lstipo.SelectedValue = ""
    Me.cbEt_lstipo.Size = New System.Drawing.Size(140, 20)
    Me.cbEt_lstipo.TabIndex = 714
    Me.cbEt_lstipo.ValueMember = ""
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbXx_riferim)
    Me.pnTop.Controls.Add(Me.lbEt_vetttipo)
    Me.pnTop.Controls.Add(Me.edEt_vettalbo)
    Me.pnTop.Controls.Add(Me.lbriferim)
    Me.pnTop.Controls.Add(Me.lbEt_vettcod)
    Me.pnTop.Controls.Add(Me.cbEt_vetttipo)
    Me.pnTop.Controls.Add(Me.edEt_vettcod)
    Me.pnTop.Controls.Add(Me.lbXx_vettcod)
    Me.pnTop.Controls.Add(Me.lbEt_vettalbo)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(661, 113)
    Me.pnTop.TabIndex = 36
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbXx_riferim
    '
    Me.lbXx_riferim.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_riferim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_riferim.Location = New System.Drawing.Point(118, 11)
    Me.lbXx_riferim.Name = "lbXx_riferim"
    Me.lbXx_riferim.NTSDbField = ""
    Me.lbXx_riferim.Size = New System.Drawing.Size(224, 20)
    Me.lbXx_riferim.TabIndex = 28
    Me.lbXx_riferim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_riferim.Tooltip = ""
    Me.lbXx_riferim.UseMnemonic = False
    '
    'lbEt_vetttipo
    '
    Me.lbEt_vetttipo.AutoSize = True
    Me.lbEt_vetttipo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_vetttipo.Location = New System.Drawing.Point(11, 37)
    Me.lbEt_vetttipo.Name = "lbEt_vetttipo"
    Me.lbEt_vetttipo.NTSDbField = ""
    Me.lbEt_vetttipo.Size = New System.Drawing.Size(43, 13)
    Me.lbEt_vetttipo.TabIndex = 30
    Me.lbEt_vetttipo.Text = "Vettore"
    Me.lbEt_vetttipo.Tooltip = ""
    Me.lbEt_vetttipo.UseMnemonic = False
    '
    'edEt_vettalbo
    '
    Me.edEt_vettalbo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_vettalbo.Location = New System.Drawing.Point(118, 81)
    Me.edEt_vettalbo.Name = "edEt_vettalbo"
    Me.edEt_vettalbo.NTSDbField = ""
    Me.edEt_vettalbo.NTSForzaVisZoom = False
    Me.edEt_vettalbo.NTSOldValue = ""
    Me.edEt_vettalbo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_vettalbo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_vettalbo.Properties.AutoHeight = False
    Me.edEt_vettalbo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_vettalbo.Properties.MaxLength = 65536
    Me.edEt_vettalbo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_vettalbo.Size = New System.Drawing.Size(521, 20)
    Me.edEt_vettalbo.TabIndex = 35
    '
    'lbriferim
    '
    Me.lbriferim.AutoSize = True
    Me.lbriferim.BackColor = System.Drawing.Color.Transparent
    Me.lbriferim.Location = New System.Drawing.Point(11, 15)
    Me.lbriferim.Name = "lbriferim"
    Me.lbriferim.NTSDbField = ""
    Me.lbriferim.Size = New System.Drawing.Size(80, 13)
    Me.lbriferim.TabIndex = 27
    Me.lbriferim.Text = "Rif. documento"
    Me.lbriferim.Tooltip = ""
    Me.lbriferim.UseMnemonic = False
    '
    'lbEt_vettcod
    '
    Me.lbEt_vettcod.AutoSize = True
    Me.lbEt_vettcod.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_vettcod.Location = New System.Drawing.Point(11, 60)
    Me.lbEt_vettcod.Name = "lbEt_vettcod"
    Me.lbEt_vettcod.NTSDbField = ""
    Me.lbEt_vettcod.Size = New System.Drawing.Size(78, 13)
    Me.lbEt_vettcod.TabIndex = 34
    Me.lbEt_vettcod.Text = "Codice vettore"
    Me.lbEt_vettcod.Tooltip = ""
    Me.lbEt_vettcod.UseMnemonic = False
    '
    'cbEt_vetttipo
    '
    Me.cbEt_vetttipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_vetttipo.DataSource = Nothing
    Me.cbEt_vetttipo.DisplayMember = ""
    Me.cbEt_vetttipo.EditValue = ""
    Me.cbEt_vetttipo.Location = New System.Drawing.Point(118, 34)
    Me.cbEt_vetttipo.Name = "cbEt_vetttipo"
    Me.cbEt_vetttipo.NTSDbField = ""
    Me.cbEt_vetttipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_vetttipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_vetttipo.Properties.AutoHeight = False
    Me.cbEt_vetttipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_vetttipo.Properties.DropDownRows = 30
    Me.cbEt_vetttipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_vetttipo.SelectedValue = ""
    Me.cbEt_vetttipo.Size = New System.Drawing.Size(112, 20)
    Me.cbEt_vetttipo.TabIndex = 29
    Me.cbEt_vetttipo.ValueMember = ""
    '
    'edEt_vettcod
    '
    Me.edEt_vettcod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_vettcod.Location = New System.Drawing.Point(118, 57)
    Me.edEt_vettcod.Name = "edEt_vettcod"
    Me.edEt_vettcod.NTSDbField = ""
    Me.edEt_vettcod.NTSFormat = "0"
    Me.edEt_vettcod.NTSForzaVisZoom = False
    Me.edEt_vettcod.NTSOldValue = ""
    Me.edEt_vettcod.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_vettcod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_vettcod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_vettcod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_vettcod.Properties.AutoHeight = False
    Me.edEt_vettcod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_vettcod.Properties.MaxLength = 65536
    Me.edEt_vettcod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_vettcod.Size = New System.Drawing.Size(67, 20)
    Me.edEt_vettcod.TabIndex = 33
    '
    'lbXx_vettcod
    '
    Me.lbXx_vettcod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vettcod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vettcod.Location = New System.Drawing.Point(191, 57)
    Me.lbXx_vettcod.Name = "lbXx_vettcod"
    Me.lbXx_vettcod.NTSDbField = ""
    Me.lbXx_vettcod.Size = New System.Drawing.Size(448, 20)
    Me.lbXx_vettcod.TabIndex = 31
    Me.lbXx_vettcod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_vettcod.Tooltip = ""
    Me.lbXx_vettcod.UseMnemonic = False
    '
    'lbEt_vettalbo
    '
    Me.lbEt_vettalbo.AutoSize = True
    Me.lbEt_vettalbo.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_vettalbo.Location = New System.Drawing.Point(11, 84)
    Me.lbEt_vettalbo.Name = "lbEt_vettalbo"
    Me.lbEt_vettalbo.NTSDbField = ""
    Me.lbEt_vettalbo.Size = New System.Drawing.Size(106, 13)
    Me.lbEt_vettalbo.TabIndex = 32
    Me.lbEt_vettalbo.Text = "Iscr. albo autotrasp."
    Me.lbEt_vettalbo.Tooltip = ""
    Me.lbEt_vettalbo.UseMnemonic = False
    '
    'FRMMGSCTR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(661, 468)
    Me.Controls.Add(Me.pnSctr)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = "bsmgsctr.htm"
    Me.MaximizeBox = False
    Me.Name = "FRMMGSCTR"
    Me.Text = "SCHEDA DI TRASPORTO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSctr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctr.ResumeLayout(False)
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel2.ResumeLayout(False)
    CType(Me.tsSctr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSctr.ResumeLayout(False)
    Me.tsSctrPage1.ResumeLayout(False)
    CType(Me.pnSctrPage1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctrPage1.ResumeLayout(False)
    Me.pnSctrPage1.PerformLayout()
    CType(Me.edEt_commmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commpi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commtel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commcf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commcitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commstat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commcap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commrs2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commrs1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_commcod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_commtipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSctrPage2.ResumeLayout(False)
    CType(Me.pnSctrPage2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctrPage2.ResumeLayout(False)
    Me.pnSctrPage2.PerformLayout()
    CType(Me.edEt_carimail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caripi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caritel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caricf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caricitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_cariind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caristat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caricap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_cariprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_carirs2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_carirs1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caridest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_caricod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_caritipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSctrPage3.ResumeLayout(False)
    CType(Me.pnSctrPage3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctrPage3.ResumeLayout(False)
    Me.pnSctrPage3.PerformLayout()
    CType(Me.edEt_evendich.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_proppi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_proptel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propcf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propcitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propstat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propcap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_proprs2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_proprs1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_propcod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_proptipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSctrPage4.ResumeLayout(False)
    CType(Me.pnSctrPage4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctrPage4.ResumeLayout(False)
    Me.pnSctrPage4.PerformLayout()
    CType(Me.edEt_lccitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lcind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lcstat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lccap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lcprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lcdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lcconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_lctipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_peso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_quant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_tipmerce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSctrPage5.ResumeLayout(False)
    CType(Me.pnSctrPage5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSctrPage5.ResumeLayout(False)
    Me.pnSctrPage5.PerformLayout()
    CType(Me.edXx_datcomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_compilat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_compcitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_istruz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lscitt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lsind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lsstat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lscap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lsprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lsdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_lsconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_lstipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edEt_vettalbo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_vetttipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_vettcod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMMGSCTR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSctr.Dispose()
      dsSctr.Dispose()
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub FRMMGSCTR_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then
        e.Cancel = True
        Return
      End If
      oMenu.SaveSettingBus("BSVEBOLL", "RECENT", ".", "RecentCittàSchetrasp", edEt_compcitt.Text, " ", "NS.", "NS.", "...")
      oCallParams.ctlPar1 = dsSctr.Tables("SCHETRASP")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSCTR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      bOnLoading = True
      'Carico i combobox
      CaricaCombo()

      'Predispongo i controlli
      InitControls()

      oCleDocu.SctrApri(DittaCorrente, dsSctr, dsSctrTesta)

      dcSctr.DataSource = dsSctr.Tables("SCHETRASP")
      dsSctr.AcceptChanges()
      Bindcontrols()

      tsSctr.SelectedTabPageIndex = 0

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bOnLoading = True = False
    End Try
  End Sub

  Public Overridable Sub FRMMGSCTR_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Try
      If dsSctr.Tables("SCHETRASP").Rows.Count = 1 Then
        If dsSctr.Tables("SCHETRASP").Rows(0).RowState = DataRowState.Added Then
          'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
        End If
      Else
        If dsSctr.Tables("SCHETRASP").Rows.Count = 0 Then
          tlbNuovo_ItemClick(Me, Nothing)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCallParams.strNomProg.ToUpper
        Case "BSVEBOLL", "BSORGSOR"
          Select Case oMenu.GetSettingBusDitt(DittaCorrente, oCallParams.strNomProg.ToUpper, "OPZIONI", ".", "ScheTraspPropostaLuogoScarico", " ", " ", " ").ToUpper
            Case "D1"
              If NTSCInt(dsSctrTesta.Tables("TESTA").Rows(0)!et_coddest) <> 0 Then
                cbEt_lstipo.NTSSelectedValueDB = "X"
              Else
                cbEt_lstipo.NTSSelectedValueDB = "C"
              End If
            Case "D2"
              If NTSCInt(dsSctrTesta.Tables("TESTA").Rows(0)!et_coddest2) <> 0 Then
                cbEt_lstipo.NTSSelectedValueDB = "Y"
              ElseIf NTSCInt(dsSctrTesta.Tables("TESTA").Rows(0)!et_coddest) <> 0 Then
                cbEt_lstipo.NTSSelectedValueDB = "X"
              Else
                cbEt_lstipo.NTSSelectedValueDB = "C"
              End If
            Case "D" : cbEt_lstipo.NTSSelectedValueDB = "D"
          End Select
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleDocu.bSctrIsLoad = True
    End Try
  End Sub

#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttEt_vetttipo As New DataTable()
    Dim dttEt_commtipo As New DataTable()
    Dim dttEt_caritipo As New DataTable()
    Dim dttEt_proptipo As New DataTable()
    Dim dttEt_lctipo As New DataTable()
    Dim dttEt_lstipo As New DataTable()
    Try
      dttEt_vetttipo.Columns.Add("cod", GetType(String))
      dttEt_vetttipo.Columns.Add("val", GetType(String))
      dttEt_vetttipo.Rows.Add(New Object() {"1", "Vettore 1"})
      dttEt_vetttipo.Rows.Add(New Object() {"2", "Vettore 2"})
      'dttEt_vetttipo.Rows.Add(New Object() {"A", "Altro"})
      dttEt_vetttipo.AcceptChanges()
      cbEt_vetttipo.DataSource = dttEt_vetttipo
      cbEt_vetttipo.ValueMember = "cod"
      cbEt_vetttipo.DisplayMember = "val"

      dttEt_commtipo.Columns.Add("cod", GetType(String))
      dttEt_commtipo.Columns.Add("val", GetType(String))
      dttEt_commtipo.Rows.Add(New Object() {"D", "Ditta corrente"})
      dttEt_commtipo.Rows.Add(New Object() {"C", "Cliente/forn."})
      dttEt_commtipo.Rows.Add(New Object() {"X", "Cliente/forn. (dest. 1)"})
      dttEt_commtipo.Rows.Add(New Object() {"Y", "Cliente/forn. (dest. 2)"})
      dttEt_commtipo.Rows.Add(New Object() {"F", "Conto fatturazione"})
      dttEt_commtipo.Rows.Add(New Object() {"M", "(Altro manuale)"})
      dttEt_commtipo.AcceptChanges()
      cbEt_commtipo.DataSource = dttEt_commtipo
      cbEt_commtipo.ValueMember = "cod"
      cbEt_commtipo.DisplayMember = "val"

      dttEt_caritipo.Columns.Add("cod", GetType(String))
      dttEt_caritipo.Columns.Add("val", GetType(String))
      dttEt_caritipo.Rows.Add(New Object() {"D", "Ditta corrente"})
      dttEt_caritipo.Rows.Add(New Object() {"C", "Cliente/forn."})
      dttEt_caritipo.Rows.Add(New Object() {"X", "Cliente/forn. (dest. 1)"})
      dttEt_caritipo.Rows.Add(New Object() {"Y", "Cliente/forn. (dest. 2)"})
      dttEt_caritipo.Rows.Add(New Object() {"1", "Vettore 1"})
      dttEt_caritipo.Rows.Add(New Object() {"2", "Vettore 2"})
      dttEt_caritipo.Rows.Add(New Object() {"M", "(Altro manuale)"})
      dttEt_caritipo.AcceptChanges()
      cbEt_caritipo.DataSource = dttEt_caritipo
      cbEt_caritipo.ValueMember = "cod"
      cbEt_caritipo.DisplayMember = "val"

      dttEt_proptipo.Columns.Add("cod", GetType(String))
      dttEt_proptipo.Columns.Add("val", GetType(String))
      dttEt_proptipo.Rows.Add(New Object() {"D", "Ditta corrente"})
      dttEt_proptipo.Rows.Add(New Object() {"C", "Cliente/forn."})
      dttEt_proptipo.Rows.Add(New Object() {"X", "Cliente/forn. (dest. 1)"})
      dttEt_proptipo.Rows.Add(New Object() {"Y", "Cliente/forn. (dest. 2)"})
      dttEt_proptipo.Rows.Add(New Object() {"F", "Conto fatturazione"})
      dttEt_proptipo.Rows.Add(New Object() {"M", "(Altro manuale)"})
      dttEt_proptipo.Rows.Add(New Object() {"N", "(Non definito)"})
      dttEt_proptipo.AcceptChanges()
      cbEt_proptipo.DataSource = dttEt_proptipo
      cbEt_proptipo.ValueMember = "cod"
      cbEt_proptipo.DisplayMember = "val"

      dttEt_lctipo.Columns.Add("cod", GetType(String))
      dttEt_lctipo.Columns.Add("val", GetType(String))
      dttEt_lctipo.Rows.Add(New Object() {"D", "Ditta corrente"})
      dttEt_lctipo.Rows.Add(New Object() {"W", "Ditta corrente (mag. 1 dest.)"})
      dttEt_lctipo.Rows.Add(New Object() {"Z", "Ditta corrente (mag. 2 dest.)"})
      dttEt_lctipo.Rows.Add(New Object() {"C", "Cliente/forn."})
      dttEt_lctipo.Rows.Add(New Object() {"X", "Cliente/forn. (dest. 1)"})
      dttEt_lctipo.Rows.Add(New Object() {"Y", "Cliente/forn. (dest. 2)"})
      dttEt_lctipo.Rows.Add(New Object() {"M", "(Altro manuale)"})
      dttEt_lctipo.AcceptChanges()
      cbEt_lctipo.DataSource = dttEt_lctipo
      cbEt_lctipo.ValueMember = "cod"
      cbEt_lctipo.DisplayMember = "val"

      dttEt_lstipo.Columns.Add("cod", GetType(String))
      dttEt_lstipo.Columns.Add("val", GetType(String))
      dttEt_lstipo.Rows.Add(New Object() {"D", "Ditta corrente"})
      dttEt_lstipo.Rows.Add(New Object() {"W", "Ditta corrente (mag. 1 dest.)"})
      dttEt_lstipo.Rows.Add(New Object() {"Z", "Ditta corrente (mag. 2 dest.)"})
      dttEt_lstipo.Rows.Add(New Object() {"C", "Cliente/forn."})
      dttEt_lstipo.Rows.Add(New Object() {"X", "Cliente/forn. (dest. 1)"})
      dttEt_lstipo.Rows.Add(New Object() {"Y", "Cliente/forn. (dest. 2)"})
      dttEt_lstipo.Rows.Add(New Object() {"M", "(Altro manuale)"})
      dttEt_lstipo.AcceptChanges()
      cbEt_lstipo.DataSource = dttEt_lstipo
      cbEt_lstipo.ValueMember = "cod"
      cbEt_lstipo.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      tlbMain.NTSSetToolTip()

      edEt_commmail.NTSSetParam(oMenu, oApp.Tr(Me, 128988634523922771, "E-mail"), 50)
      edEt_commpi.NTSSetParam(oMenu, oApp.Tr(Me, 128988634524079020, "Partita IVA"), 11)
      edEt_commtel.NTSSetParam(oMenu, oApp.Tr(Me, 128988634524391518, "Telefono"), 18)
      edEt_commcf.NTSSetParam(oMenu, oApp.Tr(Me, 128988634524704016, "Cod. fiscale"), 16)
      edEt_commcitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634525485261, "Citta/località"), 50)
      edEt_commind.NTSSetParam(oMenu, oApp.Tr(Me, 128988634525641510, "Indirizzo"), 70)
      edEt_commstat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634526110257, "Cod. stato"), tabstat, True)
      edEt_commcap.NTSSetParam(oMenu, oApp.Tr(Me, 128988634526422755, "Cap"), 9)
      edEt_commprov.NTSSetParam(oMenu, oApp.Tr(Me, 128988634526579004, "Provincia"), 2)
      edEt_commrs2.NTSSetParam(oMenu, oApp.Tr(Me, 128988634526891502, "Rag. sociale"), 50)
      edEt_commrs1.NTSSetParam(oMenu, oApp.Tr(Me, 128988634527204000, "Rag. sociale"), 35)
      edEt_commdest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634527516498, "Cod. destinazione"), tabdestdiv)
      edEt_commdest.CliePerDestdiv = edEt_commcod
      edEt_commcod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634527828996, "Cod. soggetto com."), tabanagra)
      cbEt_commtipo.NTSSetParam(oApp.Tr(Me, 128988634528141494, "Committente"))
      edEt_carimail.NTSSetParam(oMenu, oApp.Tr(Me, 128993997107477926, "E-mail"), 50)
      edEt_caripi.NTSSetParam(oMenu, oApp.Tr(Me, 128988634528297743, "Partita IVA"), 11)
      edEt_caritel.NTSSetParam(oMenu, oApp.Tr(Me, 128988634528610241, "Telefono"), 18)
      edEt_caricf.NTSSetParam(oMenu, oApp.Tr(Me, 128988634528922739, "Cod. fiscale"), 16)
      edEt_caricitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634529703984, "Citta/località"), 50)
      edEt_cariind.NTSSetParam(oMenu, oApp.Tr(Me, 128988634529860233, "Indirizzo"), 70)
      edEt_caristat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634530328980, "Cod. stato"), tabstat, True)
      edEt_caricap.NTSSetParam(oMenu, oApp.Tr(Me, 128988634530641478, "Cap"), 9)
      edEt_cariprov.NTSSetParam(oMenu, oApp.Tr(Me, 128988634530797727, "Provincia"), 2)
      edEt_carirs2.NTSSetParam(oMenu, oApp.Tr(Me, 128988634531110225, "Rag. sociale"), 50)
      edEt_carirs1.NTSSetParam(oMenu, oApp.Tr(Me, 128988634531422723, "Rag. sociale"), 35)
      edEt_caridest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634531735221, "Cod. destinazione"), tabdestdiv)
      edEt_caridest.CliePerDestdiv = edEt_caricod
      edEt_caricod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634532047719, "Cod. soggetto car."), tabanagra)
      cbEt_caritipo.NTSSetParam(oApp.Tr(Me, 128988634532360217, "Caricatore"))
      edEt_evendich.NTSSetParam(oMenu, oApp.Tr(Me, 128988634532516466, "Eventuali dich."), 0)
      edEt_propmail.NTSSetParam(oMenu, oApp.Tr(Me, 128988634532985213, "E-mail"), 50)
      edEt_proppi.NTSSetParam(oMenu, oApp.Tr(Me, 128988634533141462, "Partita IVA"), 11)
      edEt_proptel.NTSSetParam(oMenu, oApp.Tr(Me, 128988634533453960, "Telefono"), 18)
      edEt_propcf.NTSSetParam(oMenu, oApp.Tr(Me, 128988634533766458, "Cod. fiscale"), 16)
      edEt_propcitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634534547703, "Citta/località"), 50)
      edEt_propind.NTSSetParam(oMenu, oApp.Tr(Me, 128988634534703952, "Indirizzo"), 70)
      edEt_propstat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634535172699, "Cod. stato"), tabstat, True)
      edEt_propcap.NTSSetParam(oMenu, oApp.Tr(Me, 128988634535485197, "Cap"), 9)
      edEt_propprov.NTSSetParam(oMenu, oApp.Tr(Me, 128988634535641446, "Provincia"), 2)
      edEt_proprs2.NTSSetParam(oMenu, oApp.Tr(Me, 128988634535953944, "Rag. sociale"), 50)
      edEt_proprs1.NTSSetParam(oMenu, oApp.Tr(Me, 128988634536578940, "Rag. sociale"), 35)
      edEt_propdest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634536891438, "Cod. destinazione"), tabdestdiv)
      edEt_propdest.CliePerDestdiv = edEt_propcod
      edEt_propcod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634537203936, "Cod. soggetto pro."), tabanagra)
      cbEt_proptipo.NTSSetParam(oApp.Tr(Me, 128988634537516434, "Proprietario"))
      edEt_lccitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634537828932, "Citta/località"), 50)
      edEt_lcind.NTSSetParam(oMenu, oApp.Tr(Me, 128988634537985181, "Indirizzo"), 70)
      edEt_lcstat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634538453928, "Cod. stato"), tabstat, True)
      edEt_lccap.NTSSetParam(oMenu, oApp.Tr(Me, 128988634538766426, "Cap"), 9)
      edEt_lcprov.NTSSetParam(oMenu, oApp.Tr(Me, 128988634538922675, "Provincia"), 2)
      edEt_lcdest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634539391422, "Cod. destinazione"), tabdestdiv)
      edEt_lcdest.CliePerDestdiv = edEt_lcconto
      edEt_lcconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634539703920, "Cod. carico"), tabanagra)
      cbEt_lctipo.NTSSetParam(oApp.Tr(Me, 128988634540016418, "Luogo carico"))
      edEt_peso.NTSSetParam(oMenu, oApp.Tr(Me, 128988634540328916, "Peso lordo"), oApp.FormatQta)
      edEt_quant.NTSSetParam(oMenu, oApp.Tr(Me, 128988634540641414, "Quantità"), oApp.FormatQta)
      edEt_tipmerce.NTSSetParam(oMenu, oApp.Tr(Me, 128988634540953912, "Tipologia merce"), 0)
      edXx_datcomp.NTSSetParam(oMenu, oApp.Tr(Me, 128988634541578908, "Data comp."), True)
      edEt_compilat.NTSSetParam(oMenu, oApp.Tr(Me, 128988634541891406, "Nome/cogn. comp."), 50)
      edEt_compcitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634542047655, "Città comp."), 50)
      edEt_istruz.NTSSetParam(oMenu, oApp.Tr(Me, 128988634542516402, "Eventuali istr."), 0)
      edEt_lscitt.NTSSetParam(oMenu, oApp.Tr(Me, 128988634543297647, "Citta/località"), 50)
      edEt_lsind.NTSSetParam(oMenu, oApp.Tr(Me, 128988634543453896, "Indirizzo"), 70)
      edEt_lsstat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634543922643, "Cod. stato"), tabstat, True)
      edEt_lscap.NTSSetParam(oMenu, oApp.Tr(Me, 128988634544235141, "Cap"), 9)
      edEt_lsprov.NTSSetParam(oMenu, oApp.Tr(Me, 128988634544391390, "Provincia"), 2)
      edEt_lsdest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634544860137, "Cod. destinazione"), tabdestdiv)
      edEt_lsdest.CliePerDestdiv = edEt_lsconto
      edEt_lsconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634545172635, "Cod. scarico"), tabanagra)
      cbEt_lstipo.NTSSetParam(oApp.Tr(Me, 128988634545485133, "Luogo scarico"))
      edEt_vettalbo.NTSSetParam(oMenu, oApp.Tr(Me, 128988634545797631, "Iscr. albo autotrasp."), 50)
      cbEt_vetttipo.NTSSetParam(oApp.Tr(Me, 128988634546266378, "Vettore"))
      edEt_vettcod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128988634546422627, "Codice vettore"), tabvett)

      edEt_vettcod.Enabled = False
      edEt_commcod.Enabled = False
      edEt_commdest.Enabled = False
      edEt_caricod.Enabled = False
      edEt_caridest.Enabled = False
      edEt_propcod.Enabled = False
      edEt_propdest.Enabled = False
      edXx_datcomp.Enabled = False
      edEt_lcconto.Enabled = False
      edEt_lcdest.Enabled = False
      edEt_lsconto.Enabled = False
      edEt_lsdest.Enabled = False

      '---------------------------------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'Se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'Collego il BindingSource ai vari controlli 

      edEt_commmail.NTSDbField = "schetrasp.et_commmail"
      edEt_commpi.NTSDbField = "schetrasp.et_commpi"
      edEt_commtel.NTSDbField = "schetrasp.et_commtel"
      edEt_commcf.NTSDbField = "schetrasp.et_commcf"
      edXx_commstat.NTSDbField = "schetrasp.xx_commstat"
      edEt_commcitt.NTSDbField = "schetrasp.et_commcitt"
      edEt_commind.NTSDbField = "schetrasp.et_commind"
      edEt_commstat.NTSDbField = "schetrasp.et_commstat"
      edEt_commcap.NTSDbField = "schetrasp.et_commcap"
      edEt_commprov.NTSDbField = "schetrasp.et_commprov"
      edEt_commrs2.NTSDbField = "schetrasp.et_commrs2"
      edEt_commrs1.NTSDbField = "schetrasp.et_commrs1"
      edEt_commdest.NTSDbField = "schetrasp.et_commdest"
      edEt_commcod.NTSDbField = "schetrasp.et_commcod"
      cbEt_commtipo.NTSDbField = "schetrasp.et_commtipo"
      edEt_carimail.NTSDbField = "schetrasp.et_carimail"
      edEt_caripi.NTSDbField = "schetrasp.et_caripi"
      edEt_caritel.NTSDbField = "schetrasp.et_caritel"
      edEt_caricf.NTSDbField = "schetrasp.et_caricf"
      lbXx_caristat.NTSDbField = "schetrasp.xx_caristat"
      edEt_caricitt.NTSDbField = "schetrasp.et_caricitt"
      edEt_cariind.NTSDbField = "schetrasp.et_cariind"
      edEt_caristat.NTSDbField = "schetrasp.et_caristat"
      edEt_caricap.NTSDbField = "schetrasp.et_caricap"
      edEt_cariprov.NTSDbField = "schetrasp.et_cariprov"
      edEt_carirs2.NTSDbField = "schetrasp.et_carirs2"
      edEt_carirs1.NTSDbField = "schetrasp.et_carirs1"
      edEt_caridest.NTSDbField = "schetrasp.et_caridest"
      edEt_caricod.NTSDbField = "schetrasp.et_caricod"
      cbEt_caritipo.NTSDbField = "schetrasp.et_caritipo"
      edEt_evendich.NTSDbField = "schetrasp.et_evendich"
      edEt_propmail.NTSDbField = "schetrasp.et_propmail"
      edEt_proppi.NTSDbField = "schetrasp.et_proppi"
      edEt_proptel.NTSDbField = "schetrasp.et_proptel"
      edEt_propcf.NTSDbField = "schetrasp.et_propcf"
      lbXx_propstat.NTSDbField = "schetrasp.xx_propstat"
      edEt_propcitt.NTSDbField = "schetrasp.et_propcitt"
      edEt_propind.NTSDbField = "schetrasp.et_propind"
      edEt_propstat.NTSDbField = "schetrasp.et_propstat"
      edEt_propcap.NTSDbField = "schetrasp.et_propcap"
      edEt_propprov.NTSDbField = "schetrasp.et_propprov"
      edEt_proprs2.NTSDbField = "schetrasp.et_proprs2"
      edEt_proprs1.NTSDbField = "schetrasp.et_proprs1"
      edEt_propdest.NTSDbField = "schetrasp.et_propdest"
      edEt_propcod.NTSDbField = "schetrasp.et_propcod"
      cbEt_proptipo.NTSDbField = "schetrasp.et_proptipo"
      lbXx_lcstat.NTSDbField = "schetrasp.xx_lcstat"
      edEt_lccitt.NTSDbField = "schetrasp.et_lccitt"
      edEt_lcind.NTSDbField = "schetrasp.et_lcind"
      edEt_lcstat.NTSDbField = "schetrasp.et_lcstat"
      edEt_lccap.NTSDbField = "schetrasp.et_lccap"
      edEt_lcprov.NTSDbField = "schetrasp.et_lcprov"
      edEt_lcdest.NTSDbField = "schetrasp.et_lcdest"
      edEt_lcconto.NTSDbField = "schetrasp.et_lcconto"
      cbEt_lctipo.NTSDbField = "schetrasp.et_lctipo"
      edEt_peso.NTSDbField = "schetrasp.et_peso"
      edEt_quant.NTSDbField = "schetrasp.et_quant"
      edEt_tipmerce.NTSDbField = "schetrasp.et_tipmerce"
      edXx_datcomp.NTSDbField = "schetrasp.xx_datcomp"
      edEt_compilat.NTSDbField = "schetrasp.et_compilat"
      edEt_compcitt.NTSDbField = "schetrasp.et_compcitt"
      edEt_istruz.NTSDbField = "schetrasp.et_istruz"
      lbXx_lsstat.NTSDbField = "schetrasp.xx_lsstat"
      edEt_lscitt.NTSDbField = "schetrasp.et_lscitt"
      edEt_lsind.NTSDbField = "schetrasp.et_lsind"
      edEt_lsstat.NTSDbField = "schetrasp.et_lsstat"
      edEt_lscap.NTSDbField = "schetrasp.et_lscap"
      edEt_lsprov.NTSDbField = "schetrasp.et_lsprov"
      edEt_lsdest.NTSDbField = "schetrasp.et_lsdest"
      edEt_lsconto.NTSDbField = "schetrasp.et_lsconto"
      cbEt_lstipo.NTSDbField = "schetrasp.et_lstipo"
      lbXx_riferim.NTSDbField = "schetrasp.xx_riferim"
      edEt_vettalbo.NTSDbField = "schetrasp.et_vettalbo"
      cbEt_vetttipo.NTSDbField = "schetrasp.et_vetttipo"
      edEt_vettcod.NTSDbField = "schetrasp.et_vettcod"
      lbXx_vettcod.NTSDbField = "schetrasp.xx_vettcod"

      '-------------------------------------------------
      'Per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcSctr, Me)

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
      'Chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      If oCleDocu.SctrRecordIsChanged Then

        'Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550313281250, "Salvare le modifiche apportate alla scheda di trasporto?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleDocu.SctrSalva(False) Then Return False
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

#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'Creo una nuova scheda di trasporto
      If Not Salva() Then Return
      If dsSctr.Tables("SCHETRASP").Rows.Count > 0 Then Return
      oCleDocu.SctrNuovo()
      dcSctr.MoveLast()

      SetStato(1)

      '-------------------------------------------------
      'Imposto i valori di default come impostato nella GCTL
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
      If dsSctr.Tables("SCHETRASP").Rows.Count = 0 Then Return
      '-------------------------------------------------
      'Cancello la scheda di trasporto
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550430781250, "Cancellare la scheda di trasporto?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsSctr.Tables("SCHETRASP").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcSctr.RemoveAt(dcSctr.Position)
          oCleDocu.SctrSalva(True)

          If bRemovBinding Then
            NTSFormAddDataBinding(dcSctr, Me)
            bRemovBinding = False
            SetStato(0)
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSctr, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()
      '-------------------------------------------------
      'Ripristino la scheda di trasporto
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550517812500, "Ripristinare le modifiche apportate alla scheda di trasporto?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsSctr.Tables("SCHETRASP").Rows.Count = 1 And dsSctr.Tables("SCHETRASP").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleDocu.SctrRipristina(dcSctr.Position, dcSctr.Filter)

          If bRemovBinding Then
            NTSFormAddDataBinding(dcSctr, Me)
            bRemovBinding = False
            SetStato(0)
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSctr, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5

      'Per eventuali altri controlli caricati al volo
      'Zoom standard di Textbox e Griglia
      NTSCallStandardZoom()

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

#End Region

  Public Overridable Sub cbEt_commtipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_commtipo.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      dsSctr.Tables("SCHETRASP").Rows(dcSctr.Position)!et_commtipo = cbEt_commtipo.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbEt_caritipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_caritipo.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      dsSctr.Tables("SCHETRASP").Rows(dcSctr.Position)!et_caritipo = cbEt_caritipo.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbEt_proptipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_proptipo.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      dsSctr.Tables("SCHETRASP").Rows(dcSctr.Position)!et_proptipo = cbEt_proptipo.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbEt_lctipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_lctipo.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      dsSctr.Tables("SCHETRASP").Rows(dcSctr.Position)!et_lctipo = cbEt_lctipo.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbEt_lstipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_lstipo.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      dsSctr.Tables("SCHETRASP").Rows(dcSctr.Position)!et_lstipo = cbEt_lstipo.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cbEt_proptipo_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEt_proptipo.SelectedValueChanged
    Try
      If cbEt_proptipo.SelectedValue = "N" Then
        edEt_proprs1.Enabled = False
        edEt_proprs2.Enabled = False
        edEt_propind.Enabled = False
        edEt_propcap.Enabled = False
        edEt_propcitt.Enabled = False
        edEt_propprov.Enabled = False
        edEt_propstat.Enabled = False
        edEt_propcf.Enabled = False
        edEt_proppi.Enabled = False
        edEt_proptel.Enabled = False
        edEt_propmail.Enabled = False
      Else
        GctlSetVisEnab(edEt_proprs1, False)
        GctlSetVisEnab(edEt_proprs2, False)
        GctlSetVisEnab(edEt_propind, False)
        GctlSetVisEnab(edEt_propcap, False)
        GctlSetVisEnab(edEt_propcitt, False)
        GctlSetVisEnab(edEt_propprov, False)
        GctlSetVisEnab(edEt_propstat, False)
        GctlSetVisEnab(edEt_propcf, False)
        GctlSetVisEnab(edEt_proppi, False)
        GctlSetVisEnab(edEt_proptel, False)
        GctlSetVisEnab(edEt_propmail, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SetStato(ByVal nSetStato As Integer) As Boolean
    Try
      If nSetStato = 1 Then
        GctlSetVisEnab(tsSctr, True)
        GctlSetVisEnab(pnTop, True)

        tsSctr.SelectedTabPageIndex = 0

      ElseIf nSetStato = 0 Then
        tsSctr.Visible = False
        pnTop.Visible = False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
