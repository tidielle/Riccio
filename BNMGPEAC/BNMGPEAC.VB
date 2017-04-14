Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGPEAC
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
  Public oClePeac As CLEMGPEAC
  Public dsPeac As DataSet
  Public dcPeac As BindingSource = New BindingSource
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
  Public WithEvents tabPeac As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnPeac1 As NTSInformatica.NTSPanel
  Public WithEvents pnPeac2 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_atipobf As NTSInformatica.NTSLabel
  Public WithEvents edTb_atipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_atipobf As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontro As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_amagazz As NTSInformatica.NTSLabel
  Public WithEvents edTb_amagazz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_amagazz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acaumag As NTSInformatica.NTSLabel
  Public WithEvents edTb_acaumag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acaumag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_ccdaconv As NTSInformatica.NTSLabel
  Public WithEvents edTb_ccdaconv As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_ccdaconv As NTSInformatica.NTSLabel
  Public WithEvents lbXx_ccdfconv As NTSInformatica.NTSLabel
  Public WithEvents edTb_ccdfconv As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_ccdfconv As NTSInformatica.NTSLabel
  Public WithEvents edTb_alistin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_alistin As NTSInformatica.NTSLabel
  Public WithEvents edTb_listcstd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_listcstd As NTSInformatica.NTSLabel
  Public WithEvents pnPeac3 As NTSInformatica.NTSPanel
  Public WithEvents pnPeac4 As NTSInformatica.NTSPanel
  Public WithEvents fmScorporo As NTSInformatica.NTSGroupBox
  Public WithEvents ckTb_ascorpo As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_caurcnacee As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcnacee As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcnacee As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcftcee As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcftcee As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcftcee As NTSInformatica.NTSLabel
  Public WithEvents lbXx_causcapro As NTSInformatica.NTSLabel
  Public WithEvents edTb_causcapro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_causcapro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aiva15 As NTSInformatica.NTSLabel
  Public WithEvents edTb_aiva15 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aiva15 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aivainc As NTSInformatica.NTSLabel
  Public WithEvents edTb_aivainc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aivainc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caustomac As NTSInformatica.NTSLabel
  Public WithEvents edTb_caustomac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caustomac As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caupag As NTSInformatica.NTSLabel
  Public WithEvents edTb_caupag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caupag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcna As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcna As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcna As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcft As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcft As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcft As NTSInformatica.NTSLabel
  Public WithEvents lbTb_aconabat As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontimba As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontimba As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontimba As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconriac As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconriac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconriac As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconriin As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconriin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconriin As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconribo As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconribo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconribo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconomag As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconomag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconomag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconabpa As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconabpa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconabpa As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconabat As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconabat As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_aconcas As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconcas As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconcas As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aconcauz As NTSInformatica.NTSLabel
  Public WithEvents edTb_aconcauz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aconcauz As NTSInformatica.NTSLabel
  Public WithEvents lbTb_acontrabat As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrimba As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrimba As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrimba As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrriac As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrriac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrriac As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrriin As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrriin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrriin As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrribo As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrribo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrribo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontromag As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontromag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontromag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrabpa As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrabpa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrabpa As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrabat As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrabat As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_acontrcas As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrcas As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrcas As NTSInformatica.NTSLabel
  Public WithEvents lbXx_acontrcauz As NTSInformatica.NTSLabel
  Public WithEvents edTb_acontrcauz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_acontrcauz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcnextrc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcnextrc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcnextrc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcftextrc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcftextrc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcftextrc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcarc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcarc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcarc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_caurcfrc As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcfrc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcfrc As NTSInformatica.NTSLabel
  Private components As System.ComponentModel.IContainer
  Public WithEvents lbTb_aivainc3 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_aivainc3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_aivainc3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_adatfinivainc2 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbXx_aivainc2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_aivainc2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aivainc2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_adatfinivainc3 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbXx_caurcfs As NTSInformatica.NTSLabel
  Public WithEvents edTb_caurcfs As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_caurcfs As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_tipobfrko As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkh As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkh As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkh As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrko As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrko As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkt As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkt As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkt As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkm As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkm As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkm As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkl As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkl As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkl As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkj As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkj As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkj As NTSInformatica.NTSLabel
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbTipiBF As NTSInformatica.NTSBarMenuItem
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGPEAC", "BEMGPEAC", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128437380912758983, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePeac = CType(objTmp, CLEMGPEAC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGPEAC", strRemoteServer, strRemotePort)
    AddHandler oClePeac.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePeac.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGPEAC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tabPeac = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPeac1 = New NTSInformatica.NTSPanel
    Me.lbXx_caurcfs = New NTSInformatica.NTSLabel
    Me.edTb_caurcfs = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcfs = New NTSInformatica.NTSLabel
    Me.edTb_adatfinivainc3 = New NTSInformatica.NTSTextBoxData
    Me.lbTb_aivainc3 = New NTSInformatica.NTSLabel
    Me.lbXx_aivainc3 = New NTSInformatica.NTSLabel
    Me.edTb_aivainc3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_adatfinivainc2 = New NTSInformatica.NTSTextBoxData
    Me.lbXx_aivainc2 = New NTSInformatica.NTSLabel
    Me.edTb_aivainc2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aivainc2 = New NTSInformatica.NTSLabel
    Me.lbXx_caurcarc = New NTSInformatica.NTSLabel
    Me.edTb_caurcarc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcarc = New NTSInformatica.NTSLabel
    Me.lbXx_caurcfrc = New NTSInformatica.NTSLabel
    Me.edTb_caurcfrc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcfrc = New NTSInformatica.NTSLabel
    Me.lbXx_caurcnextrc = New NTSInformatica.NTSLabel
    Me.edTb_caurcnextrc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcnextrc = New NTSInformatica.NTSLabel
    Me.lbXx_caurcftextrc = New NTSInformatica.NTSLabel
    Me.edTb_caurcftextrc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcftextrc = New NTSInformatica.NTSLabel
    Me.edTb_aiva15 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_caurcnacee = New NTSInformatica.NTSLabel
    Me.edTb_caurcnacee = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcnacee = New NTSInformatica.NTSLabel
    Me.lbXx_caurcftcee = New NTSInformatica.NTSLabel
    Me.edTb_caurcftcee = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcftcee = New NTSInformatica.NTSLabel
    Me.lbXx_causcapro = New NTSInformatica.NTSLabel
    Me.edTb_causcapro = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_causcapro = New NTSInformatica.NTSLabel
    Me.lbXx_aiva15 = New NTSInformatica.NTSLabel
    Me.lbTb_aiva15 = New NTSInformatica.NTSLabel
    Me.lbXx_aivainc = New NTSInformatica.NTSLabel
    Me.edTb_aivainc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aivainc = New NTSInformatica.NTSLabel
    Me.lbXx_caustomac = New NTSInformatica.NTSLabel
    Me.edTb_caustomac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caustomac = New NTSInformatica.NTSLabel
    Me.lbXx_caupag = New NTSInformatica.NTSLabel
    Me.edTb_caupag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caupag = New NTSInformatica.NTSLabel
    Me.lbXx_caurcna = New NTSInformatica.NTSLabel
    Me.edTb_caurcna = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcna = New NTSInformatica.NTSLabel
    Me.lbXx_caurcft = New NTSInformatica.NTSLabel
    Me.edTb_caurcft = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_caurcft = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPeac2 = New NTSInformatica.NTSPanel
    Me.fmScorporo = New NTSInformatica.NTSGroupBox
    Me.ckTb_ascorpo = New NTSInformatica.NTSCheckBox
    Me.edTb_listcstd = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_listcstd = New NTSInformatica.NTSLabel
    Me.edTb_alistin = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_alistin = New NTSInformatica.NTSLabel
    Me.lbXx_ccdfconv = New NTSInformatica.NTSLabel
    Me.edTb_ccdfconv = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_ccdfconv = New NTSInformatica.NTSLabel
    Me.lbXx_ccdaconv = New NTSInformatica.NTSLabel
    Me.edTb_ccdaconv = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_ccdaconv = New NTSInformatica.NTSLabel
    Me.lbXx_acaumag = New NTSInformatica.NTSLabel
    Me.edTb_acaumag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acaumag = New NTSInformatica.NTSLabel
    Me.lbXx_amagazz = New NTSInformatica.NTSLabel
    Me.edTb_amagazz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_amagazz = New NTSInformatica.NTSLabel
    Me.lbXx_acontro = New NTSInformatica.NTSLabel
    Me.edTb_acontro = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontro = New NTSInformatica.NTSLabel
    Me.lbXx_atipobf = New NTSInformatica.NTSLabel
    Me.edTb_atipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_atipobf = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPeac3 = New NTSInformatica.NTSPanel
    Me.lbTb_aconabat = New NTSInformatica.NTSLabel
    Me.lbXx_acontimba = New NTSInformatica.NTSLabel
    Me.edTb_acontimba = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontimba = New NTSInformatica.NTSLabel
    Me.lbXx_aconriac = New NTSInformatica.NTSLabel
    Me.edTb_aconriac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconriac = New NTSInformatica.NTSLabel
    Me.lbXx_aconriin = New NTSInformatica.NTSLabel
    Me.edTb_aconriin = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconriin = New NTSInformatica.NTSLabel
    Me.lbXx_aconribo = New NTSInformatica.NTSLabel
    Me.edTb_aconribo = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconribo = New NTSInformatica.NTSLabel
    Me.lbXx_aconomag = New NTSInformatica.NTSLabel
    Me.edTb_aconomag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconomag = New NTSInformatica.NTSLabel
    Me.lbXx_aconabpa = New NTSInformatica.NTSLabel
    Me.edTb_aconabpa = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconabpa = New NTSInformatica.NTSLabel
    Me.lbXx_aconabat = New NTSInformatica.NTSLabel
    Me.edTb_aconabat = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_aconcas = New NTSInformatica.NTSLabel
    Me.edTb_aconcas = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconcas = New NTSInformatica.NTSLabel
    Me.lbXx_aconcauz = New NTSInformatica.NTSLabel
    Me.edTb_aconcauz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aconcauz = New NTSInformatica.NTSLabel
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPeac4 = New NTSInformatica.NTSPanel
    Me.lbTb_acontrabat = New NTSInformatica.NTSLabel
    Me.lbXx_acontrimba = New NTSInformatica.NTSLabel
    Me.edTb_acontrimba = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrimba = New NTSInformatica.NTSLabel
    Me.lbXx_acontrriac = New NTSInformatica.NTSLabel
    Me.edTb_acontrriac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrriac = New NTSInformatica.NTSLabel
    Me.lbXx_acontrriin = New NTSInformatica.NTSLabel
    Me.edTb_acontrriin = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrriin = New NTSInformatica.NTSLabel
    Me.lbXx_acontrribo = New NTSInformatica.NTSLabel
    Me.edTb_acontrribo = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrribo = New NTSInformatica.NTSLabel
    Me.lbXx_acontromag = New NTSInformatica.NTSLabel
    Me.edTb_acontromag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontromag = New NTSInformatica.NTSLabel
    Me.lbXx_acontrabpa = New NTSInformatica.NTSLabel
    Me.edTb_acontrabpa = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrabpa = New NTSInformatica.NTSLabel
    Me.lbXx_acontrabat = New NTSInformatica.NTSLabel
    Me.edTb_acontrabat = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_acontrcas = New NTSInformatica.NTSLabel
    Me.edTb_acontrcas = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrcas = New NTSInformatica.NTSLabel
    Me.lbXx_acontrcauz = New NTSInformatica.NTSLabel
    Me.edTb_acontrcauz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_acontrcauz = New NTSInformatica.NTSLabel
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbXx_tipobfrko = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkh = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkh = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkh = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrko = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrko = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkt = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkt = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkt = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkm = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkm = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkm = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkl = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkl = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkl = New NTSInformatica.NTSLabel
    Me.lbXx_tipobfrkj = New NTSInformatica.NTSLabel
    Me.edTb_tipobfrkj = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tipobfrkj = New NTSInformatica.NTSLabel
    Me.tlbTipiBF = New NTSInformatica.NTSBarMenuItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tabPeac, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabPeac.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPeac1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeac1.SuspendLayout()
    CType(Me.edTb_caurcfs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_adatfinivainc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aivainc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_adatfinivainc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aivainc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcfrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcnextrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcftextrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aiva15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcnacee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcftcee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_causcapro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aivainc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caustomac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caupag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcna.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_caurcft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPeac2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeac2.SuspendLayout()
    CType(Me.fmScorporo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmScorporo.SuspendLayout()
    CType(Me.ckTb_ascorpo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_listcstd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_alistin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ccdfconv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ccdaconv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acaumag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_amagazz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_atipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPeac3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeac3.SuspendLayout()
    CType(Me.edTb_acontimba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconriac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconriin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconribo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconomag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconabpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconabat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconcas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aconcauz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPeac4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeac4.SuspendLayout()
    CType(Me.edTb_acontrimba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrriac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrriin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrribo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontromag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrabpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrabat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrcas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_acontrcauz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edTb_tipobfrkh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipobfrkj.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbTipiBF})
    Me.NtsBarManager1.MaxItemId = 7
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTipiBF)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
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
    'tabPeac
    '
    Me.tabPeac.Location = New System.Drawing.Point(12, 37)
    Me.tabPeac.Name = "tabPeac"
    Me.tabPeac.SelectedTabPage = Me.NtsTabPage1
    Me.tabPeac.Size = New System.Drawing.Size(724, 403)
    Me.tabPeac.TabIndex = 1
    Me.tabPeac.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5})
    Me.tabPeac.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPeac1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(715, 373)
    Me.NtsTabPage1.Text = "Pagina &1"
    '
    'pnPeac1
    '
    Me.pnPeac1.AllowDrop = True
    Me.pnPeac1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeac1.Appearance.Options.UseBackColor = True
    Me.pnPeac1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcfs)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcfs)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcfs)
    Me.pnPeac1.Controls.Add(Me.edTb_adatfinivainc3)
    Me.pnPeac1.Controls.Add(Me.lbTb_aivainc3)
    Me.pnPeac1.Controls.Add(Me.lbXx_aivainc3)
    Me.pnPeac1.Controls.Add(Me.edTb_aivainc3)
    Me.pnPeac1.Controls.Add(Me.edTb_adatfinivainc2)
    Me.pnPeac1.Controls.Add(Me.lbXx_aivainc2)
    Me.pnPeac1.Controls.Add(Me.edTb_aivainc2)
    Me.pnPeac1.Controls.Add(Me.lbTb_aivainc2)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcarc)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcarc)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcarc)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcfrc)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcfrc)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcfrc)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcnextrc)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcnextrc)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcnextrc)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcftextrc)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcftextrc)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcftextrc)
    Me.pnPeac1.Controls.Add(Me.edTb_aiva15)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcnacee)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcnacee)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcnacee)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcftcee)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcftcee)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcftcee)
    Me.pnPeac1.Controls.Add(Me.lbXx_causcapro)
    Me.pnPeac1.Controls.Add(Me.edTb_causcapro)
    Me.pnPeac1.Controls.Add(Me.lbTb_causcapro)
    Me.pnPeac1.Controls.Add(Me.lbXx_aiva15)
    Me.pnPeac1.Controls.Add(Me.lbTb_aiva15)
    Me.pnPeac1.Controls.Add(Me.lbXx_aivainc)
    Me.pnPeac1.Controls.Add(Me.edTb_aivainc)
    Me.pnPeac1.Controls.Add(Me.lbTb_aivainc)
    Me.pnPeac1.Controls.Add(Me.lbXx_caustomac)
    Me.pnPeac1.Controls.Add(Me.edTb_caustomac)
    Me.pnPeac1.Controls.Add(Me.lbTb_caustomac)
    Me.pnPeac1.Controls.Add(Me.lbXx_caupag)
    Me.pnPeac1.Controls.Add(Me.edTb_caupag)
    Me.pnPeac1.Controls.Add(Me.lbTb_caupag)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcna)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcna)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcna)
    Me.pnPeac1.Controls.Add(Me.lbXx_caurcft)
    Me.pnPeac1.Controls.Add(Me.edTb_caurcft)
    Me.pnPeac1.Controls.Add(Me.lbTb_caurcft)
    Me.pnPeac1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeac1.Location = New System.Drawing.Point(3, 3)
    Me.pnPeac1.Name = "pnPeac1"
    Me.pnPeac1.NTSActiveTrasparency = True
    Me.pnPeac1.Size = New System.Drawing.Size(712, 367)
    Me.pnPeac1.TabIndex = 0
    Me.pnPeac1.Text = "NtsPanel1"
    '
    'lbXx_caurcfs
    '
    Me.lbXx_caurcfs.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcfs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcfs.Location = New System.Drawing.Point(311, 334)
    Me.lbXx_caurcfs.Name = "lbXx_caurcfs"
    Me.lbXx_caurcfs.NTSDbField = ""
    Me.lbXx_caurcfs.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcfs.TabIndex = 137
    Me.lbXx_caurcfs.Tooltip = ""
    Me.lbXx_caurcfs.UseMnemonic = False
    '
    'edTb_caurcfs
    '
    Me.edTb_caurcfs.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcfs.Location = New System.Drawing.Point(236, 334)
    Me.edTb_caurcfs.Name = "edTb_caurcfs"
    Me.edTb_caurcfs.NTSDbField = ""
    Me.edTb_caurcfs.NTSFormat = "0"
    Me.edTb_caurcfs.NTSForzaVisZoom = False
    Me.edTb_caurcfs.NTSOldValue = ""
    Me.edTb_caurcfs.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcfs.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcfs.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcfs.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcfs.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcfs.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcfs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcfs.Properties.MaxLength = 65536
    Me.edTb_caurcfs.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcfs.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcfs.TabIndex = 135
    '
    'lbTb_caurcfs
    '
    Me.lbTb_caurcfs.AutoSize = True
    Me.lbTb_caurcfs.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcfs.Location = New System.Drawing.Point(13, 339)
    Me.lbTb_caurcfs.Name = "lbTb_caurcfs"
    Me.lbTb_caurcfs.NTSDbField = ""
    Me.lbTb_caurcfs.Size = New System.Drawing.Size(157, 13)
    Me.lbTb_caurcfs.TabIndex = 136
    Me.lbTb_caurcfs.Text = "Caus.ricev.fattura IVA differita"
    Me.lbTb_caurcfs.Tooltip = ""
    Me.lbTb_caurcfs.UseMnemonic = False
    '
    'edTb_adatfinivainc3
    '
    Me.edTb_adatfinivainc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_adatfinivainc3.EditValue = "01/01/1900"
    Me.edTb_adatfinivainc3.Location = New System.Drawing.Point(607, 136)
    Me.edTb_adatfinivainc3.Name = "edTb_adatfinivainc3"
    Me.edTb_adatfinivainc3.NTSDbField = ""
    Me.edTb_adatfinivainc3.NTSForzaVisZoom = False
    Me.edTb_adatfinivainc3.NTSOldValue = ""
    Me.edTb_adatfinivainc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_adatfinivainc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_adatfinivainc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_adatfinivainc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_adatfinivainc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_adatfinivainc3.Properties.MaxLength = 65536
    Me.edTb_adatfinivainc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_adatfinivainc3.Size = New System.Drawing.Size(90, 20)
    Me.edTb_adatfinivainc3.TabIndex = 134
    '
    'lbTb_aivainc3
    '
    Me.lbTb_aivainc3.AutoSize = True
    Me.lbTb_aivainc3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aivainc3.Location = New System.Drawing.Point(13, 137)
    Me.lbTb_aivainc3.Name = "lbTb_aivainc3"
    Me.lbTb_aivainc3.NTSDbField = ""
    Me.lbTb_aivainc3.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_aivainc3.TabIndex = 133
    Me.lbTb_aivainc3.Text = "Codice Iva spese piede 3 / prima del"
    Me.lbTb_aivainc3.Tooltip = ""
    Me.lbTb_aivainc3.UseMnemonic = False
    '
    'lbXx_aivainc3
    '
    Me.lbXx_aivainc3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aivainc3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aivainc3.Location = New System.Drawing.Point(311, 136)
    Me.lbXx_aivainc3.Name = "lbXx_aivainc3"
    Me.lbXx_aivainc3.NTSDbField = ""
    Me.lbXx_aivainc3.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_aivainc3.TabIndex = 132
    Me.lbXx_aivainc3.Tooltip = ""
    Me.lbXx_aivainc3.UseMnemonic = False
    '
    'edTb_aivainc3
    '
    Me.edTb_aivainc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aivainc3.Location = New System.Drawing.Point(236, 136)
    Me.edTb_aivainc3.Name = "edTb_aivainc3"
    Me.edTb_aivainc3.NTSDbField = ""
    Me.edTb_aivainc3.NTSFormat = "0"
    Me.edTb_aivainc3.NTSForzaVisZoom = False
    Me.edTb_aivainc3.NTSOldValue = ""
    Me.edTb_aivainc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aivainc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aivainc3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aivainc3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aivainc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aivainc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aivainc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aivainc3.Properties.MaxLength = 65536
    Me.edTb_aivainc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aivainc3.Size = New System.Drawing.Size(69, 20)
    Me.edTb_aivainc3.TabIndex = 131
    '
    'edTb_adatfinivainc2
    '
    Me.edTb_adatfinivainc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_adatfinivainc2.EditValue = "17/09/2011"
    Me.edTb_adatfinivainc2.Location = New System.Drawing.Point(607, 113)
    Me.edTb_adatfinivainc2.Name = "edTb_adatfinivainc2"
    Me.edTb_adatfinivainc2.NTSDbField = ""
    Me.edTb_adatfinivainc2.NTSForzaVisZoom = False
    Me.edTb_adatfinivainc2.NTSOldValue = ""
    Me.edTb_adatfinivainc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_adatfinivainc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_adatfinivainc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_adatfinivainc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_adatfinivainc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_adatfinivainc2.Properties.MaxLength = 65536
    Me.edTb_adatfinivainc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_adatfinivainc2.Size = New System.Drawing.Size(90, 20)
    Me.edTb_adatfinivainc2.TabIndex = 130
    '
    'lbXx_aivainc2
    '
    Me.lbXx_aivainc2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aivainc2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aivainc2.Location = New System.Drawing.Point(311, 113)
    Me.lbXx_aivainc2.Name = "lbXx_aivainc2"
    Me.lbXx_aivainc2.NTSDbField = ""
    Me.lbXx_aivainc2.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_aivainc2.TabIndex = 129
    Me.lbXx_aivainc2.Tooltip = ""
    Me.lbXx_aivainc2.UseMnemonic = False
    '
    'edTb_aivainc2
    '
    Me.edTb_aivainc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aivainc2.Location = New System.Drawing.Point(236, 113)
    Me.edTb_aivainc2.Name = "edTb_aivainc2"
    Me.edTb_aivainc2.NTSDbField = ""
    Me.edTb_aivainc2.NTSFormat = "0"
    Me.edTb_aivainc2.NTSForzaVisZoom = False
    Me.edTb_aivainc2.NTSOldValue = ""
    Me.edTb_aivainc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aivainc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aivainc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aivainc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aivainc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aivainc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aivainc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aivainc2.Properties.MaxLength = 65536
    Me.edTb_aivainc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aivainc2.Size = New System.Drawing.Size(69, 20)
    Me.edTb_aivainc2.TabIndex = 127
    '
    'lbTb_aivainc2
    '
    Me.lbTb_aivainc2.AutoSize = True
    Me.lbTb_aivainc2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aivainc2.Location = New System.Drawing.Point(13, 116)
    Me.lbTb_aivainc2.Name = "lbTb_aivainc2"
    Me.lbTb_aivainc2.NTSDbField = ""
    Me.lbTb_aivainc2.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_aivainc2.TabIndex = 128
    Me.lbTb_aivainc2.Text = "Codice Iva spese piede 2 / prima del"
    Me.lbTb_aivainc2.Tooltip = ""
    Me.lbTb_aivainc2.UseMnemonic = False
    '
    'lbXx_caurcarc
    '
    Me.lbXx_caurcarc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcarc.Location = New System.Drawing.Point(311, 311)
    Me.lbXx_caurcarc.Name = "lbXx_caurcarc"
    Me.lbXx_caurcarc.NTSDbField = ""
    Me.lbXx_caurcarc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcarc.TabIndex = 126
    Me.lbXx_caurcarc.Tooltip = ""
    Me.lbXx_caurcarc.UseMnemonic = False
    '
    'edTb_caurcarc
    '
    Me.edTb_caurcarc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcarc.Location = New System.Drawing.Point(236, 311)
    Me.edTb_caurcarc.Name = "edTb_caurcarc"
    Me.edTb_caurcarc.NTSDbField = ""
    Me.edTb_caurcarc.NTSFormat = "0"
    Me.edTb_caurcarc.NTSForzaVisZoom = False
    Me.edTb_caurcarc.NTSOldValue = ""
    Me.edTb_caurcarc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcarc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcarc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcarc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcarc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcarc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcarc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcarc.Properties.MaxLength = 65536
    Me.edTb_caurcarc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcarc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcarc.TabIndex = 122
    '
    'lbTb_caurcarc
    '
    Me.lbTb_caurcarc.AutoSize = True
    Me.lbTb_caurcarc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcarc.Location = New System.Drawing.Point(13, 316)
    Me.lbTb_caurcarc.Name = "lbTb_caurcarc"
    Me.lbTb_caurcarc.NTSDbField = ""
    Me.lbTb_caurcarc.Size = New System.Drawing.Size(153, 13)
    Me.lbTb_caurcarc.TabIndex = 125
    Me.lbTb_caurcarc.Text = "Caus.ricev.NA.reverse charge"
    Me.lbTb_caurcarc.Tooltip = ""
    Me.lbTb_caurcarc.UseMnemonic = False
    '
    'lbXx_caurcfrc
    '
    Me.lbXx_caurcfrc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcfrc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcfrc.Location = New System.Drawing.Point(311, 289)
    Me.lbXx_caurcfrc.Name = "lbXx_caurcfrc"
    Me.lbXx_caurcfrc.NTSDbField = ""
    Me.lbXx_caurcfrc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcfrc.TabIndex = 124
    Me.lbXx_caurcfrc.Tooltip = ""
    Me.lbXx_caurcfrc.UseMnemonic = False
    '
    'edTb_caurcfrc
    '
    Me.edTb_caurcfrc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcfrc.Location = New System.Drawing.Point(236, 289)
    Me.edTb_caurcfrc.Name = "edTb_caurcfrc"
    Me.edTb_caurcfrc.NTSDbField = ""
    Me.edTb_caurcfrc.NTSFormat = "0"
    Me.edTb_caurcfrc.NTSForzaVisZoom = False
    Me.edTb_caurcfrc.NTSOldValue = ""
    Me.edTb_caurcfrc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcfrc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcfrc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcfrc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcfrc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcfrc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcfrc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcfrc.Properties.MaxLength = 65536
    Me.edTb_caurcfrc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcfrc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcfrc.TabIndex = 121
    '
    'lbTb_caurcfrc
    '
    Me.lbTb_caurcfrc.AutoSize = True
    Me.lbTb_caurcfrc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcfrc.Location = New System.Drawing.Point(13, 292)
    Me.lbTb_caurcfrc.Name = "lbTb_caurcfrc"
    Me.lbTb_caurcfrc.NTSDbField = ""
    Me.lbTb_caurcfrc.Size = New System.Drawing.Size(157, 13)
    Me.lbTb_caurcfrc.TabIndex = 123
    Me.lbTb_caurcfrc.Text = "Caus.ricev.fatt.reverse charge"
    Me.lbTb_caurcfrc.Tooltip = ""
    Me.lbTb_caurcfrc.UseMnemonic = False
    '
    'lbXx_caurcnextrc
    '
    Me.lbXx_caurcnextrc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcnextrc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcnextrc.Location = New System.Drawing.Point(311, 267)
    Me.lbXx_caurcnextrc.Name = "lbXx_caurcnextrc"
    Me.lbXx_caurcnextrc.NTSDbField = ""
    Me.lbXx_caurcnextrc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcnextrc.TabIndex = 120
    Me.lbXx_caurcnextrc.Tooltip = ""
    Me.lbXx_caurcnextrc.UseMnemonic = False
    '
    'edTb_caurcnextrc
    '
    Me.edTb_caurcnextrc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcnextrc.Location = New System.Drawing.Point(236, 267)
    Me.edTb_caurcnextrc.Name = "edTb_caurcnextrc"
    Me.edTb_caurcnextrc.NTSDbField = ""
    Me.edTb_caurcnextrc.NTSFormat = "0"
    Me.edTb_caurcnextrc.NTSForzaVisZoom = False
    Me.edTb_caurcnextrc.NTSOldValue = ""
    Me.edTb_caurcnextrc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcnextrc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcnextrc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcnextrc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcnextrc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcnextrc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcnextrc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcnextrc.Properties.MaxLength = 65536
    Me.edTb_caurcnextrc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcnextrc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcnextrc.TabIndex = 116
    '
    'lbTb_caurcnextrc
    '
    Me.lbTb_caurcnextrc.AutoSize = True
    Me.lbTb_caurcnextrc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcnextrc.Location = New System.Drawing.Point(13, 270)
    Me.lbTb_caurcnextrc.Name = "lbTb_caurcnextrc"
    Me.lbTb_caurcnextrc.NTSDbField = ""
    Me.lbTb_caurcnextrc.Size = New System.Drawing.Size(123, 13)
    Me.lbTb_caurcnextrc.TabIndex = 119
    Me.lbTb_caurcnextrc.Text = "Caus.ricev.NA.extracee"
    Me.lbTb_caurcnextrc.Tooltip = ""
    Me.lbTb_caurcnextrc.UseMnemonic = False
    '
    'lbXx_caurcftextrc
    '
    Me.lbXx_caurcftextrc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcftextrc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcftextrc.Location = New System.Drawing.Point(311, 245)
    Me.lbXx_caurcftextrc.Name = "lbXx_caurcftextrc"
    Me.lbXx_caurcftextrc.NTSDbField = ""
    Me.lbXx_caurcftextrc.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcftextrc.TabIndex = 118
    Me.lbXx_caurcftextrc.Tooltip = ""
    Me.lbXx_caurcftextrc.UseMnemonic = False
    '
    'edTb_caurcftextrc
    '
    Me.edTb_caurcftextrc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcftextrc.Location = New System.Drawing.Point(236, 245)
    Me.edTb_caurcftextrc.Name = "edTb_caurcftextrc"
    Me.edTb_caurcftextrc.NTSDbField = ""
    Me.edTb_caurcftextrc.NTSFormat = "0"
    Me.edTb_caurcftextrc.NTSForzaVisZoom = False
    Me.edTb_caurcftextrc.NTSOldValue = ""
    Me.edTb_caurcftextrc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcftextrc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcftextrc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcftextrc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcftextrc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcftextrc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcftextrc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcftextrc.Properties.MaxLength = 65536
    Me.edTb_caurcftextrc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcftextrc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcftextrc.TabIndex = 115
    '
    'lbTb_caurcftextrc
    '
    Me.lbTb_caurcftextrc.AutoSize = True
    Me.lbTb_caurcftextrc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcftextrc.Location = New System.Drawing.Point(13, 246)
    Me.lbTb_caurcftextrc.Name = "lbTb_caurcftextrc"
    Me.lbTb_caurcftextrc.NTSDbField = ""
    Me.lbTb_caurcftextrc.Size = New System.Drawing.Size(127, 13)
    Me.lbTb_caurcftextrc.TabIndex = 117
    Me.lbTb_caurcftextrc.Text = "Caus.ricev.fatt.extracee"
    Me.lbTb_caurcftextrc.Tooltip = ""
    Me.lbTb_caurcftextrc.UseMnemonic = False
    '
    'edTb_aiva15
    '
    Me.edTb_aiva15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aiva15.Location = New System.Drawing.Point(236, 157)
    Me.edTb_aiva15.Name = "edTb_aiva15"
    Me.edTb_aiva15.NTSDbField = ""
    Me.edTb_aiva15.NTSFormat = "0"
    Me.edTb_aiva15.NTSForzaVisZoom = False
    Me.edTb_aiva15.NTSOldValue = ""
    Me.edTb_aiva15.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aiva15.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aiva15.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aiva15.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aiva15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aiva15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aiva15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aiva15.Properties.MaxLength = 65536
    Me.edTb_aiva15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aiva15.Size = New System.Drawing.Size(69, 20)
    Me.edTb_aiva15.TabIndex = 7
    '
    'lbXx_caurcnacee
    '
    Me.lbXx_caurcnacee.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcnacee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcnacee.Location = New System.Drawing.Point(311, 223)
    Me.lbXx_caurcnacee.Name = "lbXx_caurcnacee"
    Me.lbXx_caurcnacee.NTSDbField = ""
    Me.lbXx_caurcnacee.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcnacee.TabIndex = 114
    Me.lbXx_caurcnacee.Tooltip = ""
    Me.lbXx_caurcnacee.UseMnemonic = False
    '
    'edTb_caurcnacee
    '
    Me.edTb_caurcnacee.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcnacee.Location = New System.Drawing.Point(236, 223)
    Me.edTb_caurcnacee.Name = "edTb_caurcnacee"
    Me.edTb_caurcnacee.NTSDbField = ""
    Me.edTb_caurcnacee.NTSFormat = "0"
    Me.edTb_caurcnacee.NTSForzaVisZoom = False
    Me.edTb_caurcnacee.NTSOldValue = ""
    Me.edTb_caurcnacee.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcnacee.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcnacee.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcnacee.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcnacee.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcnacee.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcnacee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcnacee.Properties.MaxLength = 65536
    Me.edTb_caurcnacee.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcnacee.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcnacee.TabIndex = 10
    '
    'lbTb_caurcnacee
    '
    Me.lbTb_caurcnacee.AutoSize = True
    Me.lbTb_caurcnacee.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcnacee.Location = New System.Drawing.Point(13, 226)
    Me.lbTb_caurcnacee.Name = "lbTb_caurcnacee"
    Me.lbTb_caurcnacee.NTSDbField = ""
    Me.lbTb_caurcnacee.Size = New System.Drawing.Size(157, 13)
    Me.lbTb_caurcnacee.TabIndex = 112
    Me.lbTb_caurcnacee.Text = "Caus.ricev.NA.intracomunitaria"
    Me.lbTb_caurcnacee.Tooltip = ""
    Me.lbTb_caurcnacee.UseMnemonic = False
    '
    'lbXx_caurcftcee
    '
    Me.lbXx_caurcftcee.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcftcee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcftcee.Location = New System.Drawing.Point(311, 201)
    Me.lbXx_caurcftcee.Name = "lbXx_caurcftcee"
    Me.lbXx_caurcftcee.NTSDbField = ""
    Me.lbXx_caurcftcee.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcftcee.TabIndex = 111
    Me.lbXx_caurcftcee.Tooltip = ""
    Me.lbXx_caurcftcee.UseMnemonic = False
    '
    'edTb_caurcftcee
    '
    Me.edTb_caurcftcee.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcftcee.Location = New System.Drawing.Point(236, 201)
    Me.edTb_caurcftcee.Name = "edTb_caurcftcee"
    Me.edTb_caurcftcee.NTSDbField = ""
    Me.edTb_caurcftcee.NTSFormat = "0"
    Me.edTb_caurcftcee.NTSForzaVisZoom = False
    Me.edTb_caurcftcee.NTSOldValue = ""
    Me.edTb_caurcftcee.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcftcee.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcftcee.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcftcee.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcftcee.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcftcee.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcftcee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcftcee.Properties.MaxLength = 65536
    Me.edTb_caurcftcee.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcftcee.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcftcee.TabIndex = 9
    '
    'lbTb_caurcftcee
    '
    Me.lbTb_caurcftcee.AutoSize = True
    Me.lbTb_caurcftcee.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcftcee.Location = New System.Drawing.Point(13, 204)
    Me.lbTb_caurcftcee.Name = "lbTb_caurcftcee"
    Me.lbTb_caurcftcee.NTSDbField = ""
    Me.lbTb_caurcftcee.Size = New System.Drawing.Size(161, 13)
    Me.lbTb_caurcftcee.TabIndex = 109
    Me.lbTb_caurcftcee.Text = "Caus.ricev.fatt.intracomunitaria"
    Me.lbTb_caurcftcee.Tooltip = ""
    Me.lbTb_caurcftcee.UseMnemonic = False
    '
    'lbXx_causcapro
    '
    Me.lbXx_causcapro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_causcapro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_causcapro.Location = New System.Drawing.Point(311, 179)
    Me.lbXx_causcapro.Name = "lbXx_causcapro"
    Me.lbXx_causcapro.NTSDbField = ""
    Me.lbXx_causcapro.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_causcapro.TabIndex = 108
    Me.lbXx_causcapro.Tooltip = ""
    Me.lbXx_causcapro.UseMnemonic = False
    '
    'edTb_causcapro
    '
    Me.edTb_causcapro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_causcapro.Location = New System.Drawing.Point(236, 179)
    Me.edTb_causcapro.Name = "edTb_causcapro"
    Me.edTb_causcapro.NTSDbField = ""
    Me.edTb_causcapro.NTSFormat = "0"
    Me.edTb_causcapro.NTSForzaVisZoom = False
    Me.edTb_causcapro.NTSOldValue = ""
    Me.edTb_causcapro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_causcapro.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_causcapro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_causcapro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_causcapro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_causcapro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_causcapro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_causcapro.Properties.MaxLength = 65536
    Me.edTb_causcapro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_causcapro.Size = New System.Drawing.Size(69, 20)
    Me.edTb_causcapro.TabIndex = 8
    '
    'lbTb_causcapro
    '
    Me.lbTb_causcapro.AutoSize = True
    Me.lbTb_causcapro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_causcapro.Location = New System.Drawing.Point(13, 182)
    Me.lbTb_causcapro.Name = "lbTb_causcapro"
    Me.lbTb_causcapro.NTSDbField = ""
    Me.lbTb_causcapro.Size = New System.Drawing.Size(165, 13)
    Me.lbTb_causcapro.TabIndex = 106
    Me.lbTb_causcapro.Text = "Causale Mag. Scarico Produzione"
    Me.lbTb_causcapro.Tooltip = ""
    Me.lbTb_causcapro.UseMnemonic = False
    '
    'lbXx_aiva15
    '
    Me.lbXx_aiva15.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aiva15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aiva15.Location = New System.Drawing.Point(311, 157)
    Me.lbXx_aiva15.Name = "lbXx_aiva15"
    Me.lbXx_aiva15.NTSDbField = ""
    Me.lbXx_aiva15.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_aiva15.TabIndex = 105
    Me.lbXx_aiva15.Tooltip = ""
    Me.lbXx_aiva15.UseMnemonic = False
    '
    'lbTb_aiva15
    '
    Me.lbTb_aiva15.AutoSize = True
    Me.lbTb_aiva15.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aiva15.Location = New System.Drawing.Point(13, 160)
    Me.lbTb_aiva15.Name = "lbTb_aiva15"
    Me.lbTb_aiva15.NTSDbField = ""
    Me.lbTb_aiva15.Size = New System.Drawing.Size(138, 13)
    Me.lbTb_aiva15.TabIndex = 103
    Me.lbTb_aiva15.Text = "Codice IVA Escl. Articolo 15"
    Me.lbTb_aiva15.Tooltip = ""
    Me.lbTb_aiva15.UseMnemonic = False
    '
    'lbXx_aivainc
    '
    Me.lbXx_aivainc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aivainc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aivainc.Location = New System.Drawing.Point(311, 92)
    Me.lbXx_aivainc.Name = "lbXx_aivainc"
    Me.lbXx_aivainc.NTSDbField = ""
    Me.lbXx_aivainc.Size = New System.Drawing.Size(290, 20)
    Me.lbXx_aivainc.TabIndex = 102
    Me.lbXx_aivainc.Tooltip = ""
    Me.lbXx_aivainc.UseMnemonic = False
    '
    'edTb_aivainc
    '
    Me.edTb_aivainc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aivainc.Location = New System.Drawing.Point(236, 92)
    Me.edTb_aivainc.Name = "edTb_aivainc"
    Me.edTb_aivainc.NTSDbField = ""
    Me.edTb_aivainc.NTSFormat = "0"
    Me.edTb_aivainc.NTSForzaVisZoom = False
    Me.edTb_aivainc.NTSOldValue = ""
    Me.edTb_aivainc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aivainc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aivainc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aivainc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aivainc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aivainc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aivainc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aivainc.Properties.MaxLength = 65536
    Me.edTb_aivainc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aivainc.Size = New System.Drawing.Size(69, 20)
    Me.edTb_aivainc.TabIndex = 6
    '
    'lbTb_aivainc
    '
    Me.lbTb_aivainc.AutoSize = True
    Me.lbTb_aivainc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aivainc.Location = New System.Drawing.Point(13, 95)
    Me.lbTb_aivainc.Name = "lbTb_aivainc"
    Me.lbTb_aivainc.NTSDbField = ""
    Me.lbTb_aivainc.Size = New System.Drawing.Size(173, 13)
    Me.lbTb_aivainc.TabIndex = 100
    Me.lbTb_aivainc.Text = "Codice Iva spese piede predefinito"
    Me.lbTb_aivainc.Tooltip = ""
    Me.lbTb_aivainc.UseMnemonic = False
    '
    'lbXx_caustomac
    '
    Me.lbXx_caustomac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caustomac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caustomac.Location = New System.Drawing.Point(311, 69)
    Me.lbXx_caustomac.Name = "lbXx_caustomac"
    Me.lbXx_caustomac.NTSDbField = ""
    Me.lbXx_caustomac.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caustomac.TabIndex = 99
    Me.lbXx_caustomac.Tooltip = ""
    Me.lbXx_caustomac.UseMnemonic = False
    '
    'edTb_caustomac
    '
    Me.edTb_caustomac.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_caustomac.Location = New System.Drawing.Point(236, 69)
    Me.edTb_caustomac.Name = "edTb_caustomac"
    Me.edTb_caustomac.NTSDbField = ""
    Me.edTb_caustomac.NTSFormat = "0"
    Me.edTb_caustomac.NTSForzaVisZoom = False
    Me.edTb_caustomac.NTSOldValue = ""
    Me.edTb_caustomac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caustomac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caustomac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caustomac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caustomac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caustomac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caustomac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caustomac.Properties.MaxLength = 65536
    Me.edTb_caustomac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caustomac.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caustomac.TabIndex = 5
    '
    'lbTb_caustomac
    '
    Me.lbTb_caustomac.AutoSize = True
    Me.lbTb_caustomac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caustomac.Location = New System.Drawing.Point(13, 72)
    Me.lbTb_caustomac.Name = "lbTb_caustomac"
    Me.lbTb_caustomac.NTSDbField = ""
    Me.lbTb_caustomac.Size = New System.Drawing.Size(119, 13)
    Me.lbTb_caustomac.TabIndex = 97
    Me.lbTb_caustomac.Text = "Causale Storno Omaggi"
    Me.lbTb_caustomac.Tooltip = ""
    Me.lbTb_caustomac.UseMnemonic = False
    '
    'lbXx_caupag
    '
    Me.lbXx_caupag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caupag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caupag.Location = New System.Drawing.Point(311, 47)
    Me.lbXx_caupag.Name = "lbXx_caupag"
    Me.lbXx_caupag.NTSDbField = ""
    Me.lbXx_caupag.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caupag.TabIndex = 96
    Me.lbXx_caupag.Tooltip = ""
    Me.lbXx_caupag.UseMnemonic = False
    '
    'edTb_caupag
    '
    Me.edTb_caupag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caupag.Location = New System.Drawing.Point(236, 47)
    Me.edTb_caupag.Name = "edTb_caupag"
    Me.edTb_caupag.NTSDbField = ""
    Me.edTb_caupag.NTSFormat = "0"
    Me.edTb_caupag.NTSForzaVisZoom = False
    Me.edTb_caupag.NTSOldValue = ""
    Me.edTb_caupag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caupag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caupag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caupag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caupag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caupag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caupag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caupag.Properties.MaxLength = 65536
    Me.edTb_caupag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caupag.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caupag.TabIndex = 4
    '
    'lbTb_caupag
    '
    Me.lbTb_caupag.AutoSize = True
    Me.lbTb_caupag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caupag.Location = New System.Drawing.Point(13, 50)
    Me.lbTb_caupag.Name = "lbTb_caupag"
    Me.lbTb_caupag.NTSDbField = ""
    Me.lbTb_caupag.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_caupag.TabIndex = 94
    Me.lbTb_caupag.Text = "Causale Pagamento"
    Me.lbTb_caupag.Tooltip = ""
    Me.lbTb_caupag.UseMnemonic = False
    '
    'lbXx_caurcna
    '
    Me.lbXx_caurcna.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcna.Location = New System.Drawing.Point(311, 25)
    Me.lbXx_caurcna.Name = "lbXx_caurcna"
    Me.lbXx_caurcna.NTSDbField = ""
    Me.lbXx_caurcna.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcna.TabIndex = 93
    Me.lbXx_caurcna.Tooltip = ""
    Me.lbXx_caurcna.UseMnemonic = False
    '
    'edTb_caurcna
    '
    Me.edTb_caurcna.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcna.Location = New System.Drawing.Point(236, 25)
    Me.edTb_caurcna.Name = "edTb_caurcna"
    Me.edTb_caurcna.NTSDbField = ""
    Me.edTb_caurcna.NTSFormat = "0"
    Me.edTb_caurcna.NTSForzaVisZoom = False
    Me.edTb_caurcna.NTSOldValue = ""
    Me.edTb_caurcna.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcna.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcna.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcna.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcna.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcna.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcna.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcna.Properties.MaxLength = 65536
    Me.edTb_caurcna.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcna.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcna.TabIndex = 3
    '
    'lbTb_caurcna
    '
    Me.lbTb_caurcna.AutoSize = True
    Me.lbTb_caurcna.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcna.Location = New System.Drawing.Point(13, 28)
    Me.lbTb_caurcna.Name = "lbTb_caurcna"
    Me.lbTb_caurcna.NTSDbField = ""
    Me.lbTb_caurcna.Size = New System.Drawing.Size(137, 13)
    Me.lbTb_caurcna.TabIndex = 91
    Me.lbTb_caurcna.Text = "Causale Ric.Note Accredito"
    Me.lbTb_caurcna.Tooltip = ""
    Me.lbTb_caurcna.UseMnemonic = False
    '
    'lbXx_caurcft
    '
    Me.lbXx_caurcft.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caurcft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caurcft.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_caurcft.Name = "lbXx_caurcft"
    Me.lbXx_caurcft.NTSDbField = ""
    Me.lbXx_caurcft.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_caurcft.TabIndex = 90
    Me.lbXx_caurcft.Tooltip = ""
    Me.lbXx_caurcft.UseMnemonic = False
    '
    'edTb_caurcft
    '
    Me.edTb_caurcft.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_caurcft.Location = New System.Drawing.Point(236, 3)
    Me.edTb_caurcft.Name = "edTb_caurcft"
    Me.edTb_caurcft.NTSDbField = ""
    Me.edTb_caurcft.NTSFormat = "0"
    Me.edTb_caurcft.NTSForzaVisZoom = False
    Me.edTb_caurcft.NTSOldValue = ""
    Me.edTb_caurcft.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_caurcft.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_caurcft.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_caurcft.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_caurcft.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_caurcft.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_caurcft.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_caurcft.Properties.MaxLength = 65536
    Me.edTb_caurcft.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_caurcft.Size = New System.Drawing.Size(69, 20)
    Me.edTb_caurcft.TabIndex = 2
    '
    'lbTb_caurcft
    '
    Me.lbTb_caurcft.AutoSize = True
    Me.lbTb_caurcft.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_caurcft.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_caurcft.Name = "lbTb_caurcft"
    Me.lbTb_caurcft.NTSDbField = ""
    Me.lbTb_caurcft.Size = New System.Drawing.Size(145, 13)
    Me.lbTb_caurcft.TabIndex = 81
    Me.lbTb_caurcft.Text = "Causale Ricevimento Fatture"
    Me.lbTb_caurcft.Tooltip = ""
    Me.lbTb_caurcft.UseMnemonic = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPeac2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(715, 373)
    Me.NtsTabPage2.Text = "Pagina &2"
    '
    'pnPeac2
    '
    Me.pnPeac2.AllowDrop = True
    Me.pnPeac2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeac2.Appearance.Options.UseBackColor = True
    Me.pnPeac2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeac2.Controls.Add(Me.fmScorporo)
    Me.pnPeac2.Controls.Add(Me.edTb_listcstd)
    Me.pnPeac2.Controls.Add(Me.lbTb_listcstd)
    Me.pnPeac2.Controls.Add(Me.edTb_alistin)
    Me.pnPeac2.Controls.Add(Me.lbTb_alistin)
    Me.pnPeac2.Controls.Add(Me.lbXx_ccdfconv)
    Me.pnPeac2.Controls.Add(Me.edTb_ccdfconv)
    Me.pnPeac2.Controls.Add(Me.lbTb_ccdfconv)
    Me.pnPeac2.Controls.Add(Me.lbXx_ccdaconv)
    Me.pnPeac2.Controls.Add(Me.edTb_ccdaconv)
    Me.pnPeac2.Controls.Add(Me.lbTb_ccdaconv)
    Me.pnPeac2.Controls.Add(Me.lbXx_acaumag)
    Me.pnPeac2.Controls.Add(Me.edTb_acaumag)
    Me.pnPeac2.Controls.Add(Me.lbTb_acaumag)
    Me.pnPeac2.Controls.Add(Me.lbXx_amagazz)
    Me.pnPeac2.Controls.Add(Me.edTb_amagazz)
    Me.pnPeac2.Controls.Add(Me.lbTb_amagazz)
    Me.pnPeac2.Controls.Add(Me.lbXx_acontro)
    Me.pnPeac2.Controls.Add(Me.edTb_acontro)
    Me.pnPeac2.Controls.Add(Me.lbTb_acontro)
    Me.pnPeac2.Controls.Add(Me.lbXx_atipobf)
    Me.pnPeac2.Controls.Add(Me.edTb_atipobf)
    Me.pnPeac2.Controls.Add(Me.lbTb_atipobf)
    Me.pnPeac2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeac2.Location = New System.Drawing.Point(3, 3)
    Me.pnPeac2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeac2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeac2.Name = "pnPeac2"
    Me.pnPeac2.NTSActiveTrasparency = True
    Me.pnPeac2.Size = New System.Drawing.Size(712, 341)
    Me.pnPeac2.TabIndex = 0
    Me.pnPeac2.Text = "NtsPanel1"
    '
    'fmScorporo
    '
    Me.fmScorporo.AllowDrop = True
    Me.fmScorporo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmScorporo.Appearance.Options.UseBackColor = True
    Me.fmScorporo.Controls.Add(Me.ckTb_ascorpo)
    Me.fmScorporo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmScorporo.Location = New System.Drawing.Point(373, 160)
    Me.fmScorporo.Name = "fmScorporo"
    Me.fmScorporo.Size = New System.Drawing.Size(186, 46)
    Me.fmScorporo.TabIndex = 117
    '
    'ckTb_ascorpo
    '
    Me.ckTb_ascorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_ascorpo.Location = New System.Drawing.Point(5, 23)
    Me.ckTb_ascorpo.Name = "ckTb_ascorpo"
    Me.ckTb_ascorpo.NTSCheckValue = "S"
    Me.ckTb_ascorpo.NTSUnCheckValue = "N"
    Me.ckTb_ascorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_ascorpo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_ascorpo.Properties.Caption = "&Scorporo in Testata Bolla"
    Me.ckTb_ascorpo.Size = New System.Drawing.Size(156, 19)
    Me.ckTb_ascorpo.TabIndex = 0
    '
    'edTb_listcstd
    '
    Me.edTb_listcstd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_listcstd.Location = New System.Drawing.Point(236, 183)
    Me.edTb_listcstd.Name = "edTb_listcstd"
    Me.edTb_listcstd.NTSDbField = ""
    Me.edTb_listcstd.NTSFormat = "0"
    Me.edTb_listcstd.NTSForzaVisZoom = False
    Me.edTb_listcstd.NTSOldValue = ""
    Me.edTb_listcstd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_listcstd.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_listcstd.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_listcstd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_listcstd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_listcstd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_listcstd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_listcstd.Properties.MaxLength = 65536
    Me.edTb_listcstd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_listcstd.Size = New System.Drawing.Size(69, 20)
    Me.edTb_listcstd.TabIndex = 9
    '
    'lbTb_listcstd
    '
    Me.lbTb_listcstd.AutoSize = True
    Me.lbTb_listcstd.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_listcstd.Location = New System.Drawing.Point(13, 186)
    Me.lbTb_listcstd.Name = "lbTb_listcstd"
    Me.lbTb_listcstd.NTSDbField = ""
    Me.lbTb_listcstd.Size = New System.Drawing.Size(109, 13)
    Me.lbTb_listcstd.TabIndex = 116
    Me.lbTb_listcstd.Text = "Listino costi Standard"
    Me.lbTb_listcstd.Tooltip = ""
    Me.lbTb_listcstd.UseMnemonic = False
    '
    'edTb_alistin
    '
    Me.edTb_alistin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_alistin.Location = New System.Drawing.Point(236, 157)
    Me.edTb_alistin.Name = "edTb_alistin"
    Me.edTb_alistin.NTSDbField = ""
    Me.edTb_alistin.NTSFormat = "0"
    Me.edTb_alistin.NTSForzaVisZoom = False
    Me.edTb_alistin.NTSOldValue = ""
    Me.edTb_alistin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_alistin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_alistin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_alistin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_alistin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_alistin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_alistin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_alistin.Properties.MaxLength = 65536
    Me.edTb_alistin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_alistin.Size = New System.Drawing.Size(69, 20)
    Me.edTb_alistin.TabIndex = 1
    '
    'lbTb_alistin
    '
    Me.lbTb_alistin.AutoSize = True
    Me.lbTb_alistin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_alistin.Location = New System.Drawing.Point(13, 160)
    Me.lbTb_alistin.Name = "lbTb_alistin"
    Me.lbTb_alistin.NTSDbField = ""
    Me.lbTb_alistin.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_alistin.TabIndex = 113
    Me.lbTb_alistin.Text = "Listino Testata Bolla"
    Me.lbTb_alistin.Tooltip = ""
    Me.lbTb_alistin.UseMnemonic = False
    '
    'lbXx_ccdfconv
    '
    Me.lbXx_ccdfconv.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ccdfconv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_ccdfconv.Location = New System.Drawing.Point(311, 131)
    Me.lbXx_ccdfconv.Name = "lbXx_ccdfconv"
    Me.lbXx_ccdfconv.NTSDbField = ""
    Me.lbXx_ccdfconv.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_ccdfconv.TabIndex = 111
    Me.lbXx_ccdfconv.Tooltip = ""
    Me.lbXx_ccdfconv.UseMnemonic = False
    '
    'edTb_ccdfconv
    '
    Me.edTb_ccdfconv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ccdfconv.Location = New System.Drawing.Point(236, 131)
    Me.edTb_ccdfconv.Name = "edTb_ccdfconv"
    Me.edTb_ccdfconv.NTSDbField = ""
    Me.edTb_ccdfconv.NTSFormat = "0"
    Me.edTb_ccdfconv.NTSForzaVisZoom = False
    Me.edTb_ccdfconv.NTSOldValue = ""
    Me.edTb_ccdfconv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ccdfconv.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ccdfconv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ccdfconv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ccdfconv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ccdfconv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ccdfconv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ccdfconv.Properties.MaxLength = 65536
    Me.edTb_ccdfconv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ccdfconv.Size = New System.Drawing.Size(69, 20)
    Me.edTb_ccdfconv.TabIndex = 17
    '
    'lbTb_ccdfconv
    '
    Me.lbTb_ccdfconv.AutoSize = True
    Me.lbTb_ccdfconv.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ccdfconv.Location = New System.Drawing.Point(13, 134)
    Me.lbTb_ccdfconv.Name = "lbTb_ccdfconv"
    Me.lbTb_ccdfconv.NTSDbField = ""
    Me.lbTb_ccdfconv.Size = New System.Drawing.Size(217, 13)
    Me.lbTb_ccdfconv.TabIndex = 110
    Me.lbTb_ccdfconv.Text = "Conto contropartita Ricavi spese accessorie"
    Me.lbTb_ccdfconv.Tooltip = ""
    Me.lbTb_ccdfconv.UseMnemonic = False
    '
    'lbXx_ccdaconv
    '
    Me.lbXx_ccdaconv.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_ccdaconv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_ccdaconv.Location = New System.Drawing.Point(311, 103)
    Me.lbXx_ccdaconv.Name = "lbXx_ccdaconv"
    Me.lbXx_ccdaconv.NTSDbField = ""
    Me.lbXx_ccdaconv.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_ccdaconv.TabIndex = 105
    Me.lbXx_ccdaconv.Tooltip = ""
    Me.lbXx_ccdaconv.UseMnemonic = False
    '
    'edTb_ccdaconv
    '
    Me.edTb_ccdaconv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ccdaconv.Location = New System.Drawing.Point(236, 105)
    Me.edTb_ccdaconv.Name = "edTb_ccdaconv"
    Me.edTb_ccdaconv.NTSDbField = ""
    Me.edTb_ccdaconv.NTSFormat = "0"
    Me.edTb_ccdaconv.NTSForzaVisZoom = False
    Me.edTb_ccdaconv.NTSOldValue = ""
    Me.edTb_ccdaconv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ccdaconv.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ccdaconv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ccdaconv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ccdaconv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ccdaconv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ccdaconv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ccdaconv.Properties.MaxLength = 65536
    Me.edTb_ccdaconv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ccdaconv.Size = New System.Drawing.Size(69, 20)
    Me.edTb_ccdaconv.TabIndex = 1
    '
    'lbTb_ccdaconv
    '
    Me.lbTb_ccdaconv.AutoSize = True
    Me.lbTb_ccdaconv.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ccdaconv.Location = New System.Drawing.Point(13, 108)
    Me.lbTb_ccdaconv.Name = "lbTb_ccdaconv"
    Me.lbTb_ccdaconv.NTSDbField = ""
    Me.lbTb_ccdaconv.Size = New System.Drawing.Size(154, 13)
    Me.lbTb_ccdaconv.TabIndex = 104
    Me.lbTb_ccdaconv.Text = "Controp.diff.attive di convers."
    Me.lbTb_ccdaconv.Tooltip = ""
    Me.lbTb_ccdaconv.UseMnemonic = False
    '
    'lbXx_acaumag
    '
    Me.lbXx_acaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acaumag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acaumag.Location = New System.Drawing.Point(311, 79)
    Me.lbXx_acaumag.Name = "lbXx_acaumag"
    Me.lbXx_acaumag.NTSDbField = ""
    Me.lbXx_acaumag.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_acaumag.TabIndex = 102
    Me.lbXx_acaumag.Tooltip = ""
    Me.lbXx_acaumag.UseMnemonic = False
    '
    'edTb_acaumag
    '
    Me.edTb_acaumag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acaumag.Location = New System.Drawing.Point(236, 79)
    Me.edTb_acaumag.Name = "edTb_acaumag"
    Me.edTb_acaumag.NTSDbField = ""
    Me.edTb_acaumag.NTSFormat = "0"
    Me.edTb_acaumag.NTSForzaVisZoom = False
    Me.edTb_acaumag.NTSOldValue = ""
    Me.edTb_acaumag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acaumag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acaumag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acaumag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acaumag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acaumag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acaumag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acaumag.Properties.MaxLength = 65536
    Me.edTb_acaumag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acaumag.Size = New System.Drawing.Size(69, 20)
    Me.edTb_acaumag.TabIndex = 14
    '
    'lbTb_acaumag
    '
    Me.lbTb_acaumag.AutoSize = True
    Me.lbTb_acaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acaumag.Location = New System.Drawing.Point(13, 82)
    Me.lbTb_acaumag.Name = "lbTb_acaumag"
    Me.lbTb_acaumag.NTSDbField = ""
    Me.lbTb_acaumag.Size = New System.Drawing.Size(145, 13)
    Me.lbTb_acaumag.TabIndex = 101
    Me.lbTb_acaumag.Text = "Cod.Caus.Magazz.Test.Bolla"
    Me.lbTb_acaumag.Tooltip = ""
    Me.lbTb_acaumag.UseMnemonic = False
    '
    'lbXx_amagazz
    '
    Me.lbXx_amagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_amagazz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_amagazz.Location = New System.Drawing.Point(311, 53)
    Me.lbXx_amagazz.Name = "lbXx_amagazz"
    Me.lbXx_amagazz.NTSDbField = ""
    Me.lbXx_amagazz.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_amagazz.TabIndex = 99
    Me.lbXx_amagazz.Tooltip = ""
    Me.lbXx_amagazz.UseMnemonic = False
    '
    'edTb_amagazz
    '
    Me.edTb_amagazz.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_amagazz.Location = New System.Drawing.Point(236, 53)
    Me.edTb_amagazz.Name = "edTb_amagazz"
    Me.edTb_amagazz.NTSDbField = ""
    Me.edTb_amagazz.NTSFormat = "0"
    Me.edTb_amagazz.NTSForzaVisZoom = False
    Me.edTb_amagazz.NTSOldValue = ""
    Me.edTb_amagazz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_amagazz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_amagazz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_amagazz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_amagazz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_amagazz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_amagazz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_amagazz.Properties.MaxLength = 65536
    Me.edTb_amagazz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_amagazz.Size = New System.Drawing.Size(69, 20)
    Me.edTb_amagazz.TabIndex = 13
    '
    'lbTb_amagazz
    '
    Me.lbTb_amagazz.AutoSize = True
    Me.lbTb_amagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_amagazz.Location = New System.Drawing.Point(13, 56)
    Me.lbTb_amagazz.Name = "lbTb_amagazz"
    Me.lbTb_amagazz.NTSDbField = ""
    Me.lbTb_amagazz.Size = New System.Drawing.Size(141, 13)
    Me.lbTb_amagazz.TabIndex = 98
    Me.lbTb_amagazz.Text = "Cod.Magazz.Car.Test. Bolla"
    Me.lbTb_amagazz.Tooltip = ""
    Me.lbTb_amagazz.UseMnemonic = False
    '
    'lbXx_acontro
    '
    Me.lbXx_acontro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontro.Location = New System.Drawing.Point(311, 27)
    Me.lbXx_acontro.Name = "lbXx_acontro"
    Me.lbXx_acontro.NTSDbField = ""
    Me.lbXx_acontro.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_acontro.TabIndex = 96
    Me.lbXx_acontro.Tooltip = ""
    Me.lbXx_acontro.UseMnemonic = False
    '
    'edTb_acontro
    '
    Me.edTb_acontro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontro.Location = New System.Drawing.Point(236, 27)
    Me.edTb_acontro.Name = "edTb_acontro"
    Me.edTb_acontro.NTSDbField = ""
    Me.edTb_acontro.NTSFormat = "0"
    Me.edTb_acontro.NTSForzaVisZoom = False
    Me.edTb_acontro.NTSOldValue = ""
    Me.edTb_acontro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontro.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontro.Properties.MaxLength = 65536
    Me.edTb_acontro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontro.Size = New System.Drawing.Size(69, 20)
    Me.edTb_acontro.TabIndex = 12
    '
    'lbTb_acontro
    '
    Me.lbTb_acontro.AutoSize = True
    Me.lbTb_acontro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontro.Location = New System.Drawing.Point(13, 30)
    Me.lbTb_acontro.Name = "lbTb_acontro"
    Me.lbTb_acontro.NTSDbField = ""
    Me.lbTb_acontro.Size = New System.Drawing.Size(147, 13)
    Me.lbTb_acontro.TabIndex = 95
    Me.lbTb_acontro.Text = "Codice Controp.Testata Bolla"
    Me.lbTb_acontro.Tooltip = ""
    Me.lbTb_acontro.UseMnemonic = False
    '
    'lbXx_atipobf
    '
    Me.lbXx_atipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_atipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_atipobf.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_atipobf.Name = "lbXx_atipobf"
    Me.lbXx_atipobf.NTSDbField = ""
    Me.lbXx_atipobf.Size = New System.Drawing.Size(388, 20)
    Me.lbXx_atipobf.TabIndex = 93
    Me.lbXx_atipobf.Tooltip = ""
    Me.lbXx_atipobf.UseMnemonic = False
    '
    'edTb_atipobf
    '
    Me.edTb_atipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_atipobf.Location = New System.Drawing.Point(236, 3)
    Me.edTb_atipobf.Name = "edTb_atipobf"
    Me.edTb_atipobf.NTSDbField = ""
    Me.edTb_atipobf.NTSFormat = "0"
    Me.edTb_atipobf.NTSForzaVisZoom = False
    Me.edTb_atipobf.NTSOldValue = ""
    Me.edTb_atipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_atipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_atipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_atipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_atipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_atipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_atipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_atipobf.Properties.MaxLength = 65536
    Me.edTb_atipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_atipobf.Size = New System.Drawing.Size(69, 20)
    Me.edTb_atipobf.TabIndex = 11
    '
    'lbTb_atipobf
    '
    Me.lbTb_atipobf.AutoSize = True
    Me.lbTb_atipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_atipobf.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_atipobf.Name = "lbTb_atipobf"
    Me.lbTb_atipobf.NTSDbField = ""
    Me.lbTb_atipobf.Size = New System.Drawing.Size(205, 13)
    Me.lbTb_atipobf.TabIndex = 92
    Me.lbTb_atipobf.Text = "Tipo Bolla/Fattura generico per Test.Bolla"
    Me.lbTb_atipobf.Tooltip = ""
    Me.lbTb_atipobf.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPeac3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(715, 373)
    Me.NtsTabPage3.Text = "Pagina &3"
    '
    'pnPeac3
    '
    Me.pnPeac3.AllowDrop = True
    Me.pnPeac3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeac3.Appearance.Options.UseBackColor = True
    Me.pnPeac3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeac3.Controls.Add(Me.lbTb_aconabat)
    Me.pnPeac3.Controls.Add(Me.lbXx_acontimba)
    Me.pnPeac3.Controls.Add(Me.edTb_acontimba)
    Me.pnPeac3.Controls.Add(Me.lbTb_acontimba)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconriac)
    Me.pnPeac3.Controls.Add(Me.edTb_aconriac)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconriac)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconriin)
    Me.pnPeac3.Controls.Add(Me.edTb_aconriin)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconriin)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconribo)
    Me.pnPeac3.Controls.Add(Me.edTb_aconribo)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconribo)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconomag)
    Me.pnPeac3.Controls.Add(Me.edTb_aconomag)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconomag)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconabpa)
    Me.pnPeac3.Controls.Add(Me.edTb_aconabpa)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconabpa)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconabat)
    Me.pnPeac3.Controls.Add(Me.edTb_aconabat)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconcas)
    Me.pnPeac3.Controls.Add(Me.edTb_aconcas)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconcas)
    Me.pnPeac3.Controls.Add(Me.lbXx_aconcauz)
    Me.pnPeac3.Controls.Add(Me.edTb_aconcauz)
    Me.pnPeac3.Controls.Add(Me.lbTb_aconcauz)
    Me.pnPeac3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeac3.Location = New System.Drawing.Point(3, 3)
    Me.pnPeac3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeac3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeac3.Name = "pnPeac3"
    Me.pnPeac3.NTSActiveTrasparency = True
    Me.pnPeac3.Size = New System.Drawing.Size(712, 341)
    Me.pnPeac3.TabIndex = 0
    Me.pnPeac3.Text = "NtsPanel1"
    '
    'lbTb_aconabat
    '
    Me.lbTb_aconabat.AutoSize = True
    Me.lbTb_aconabat.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconabat.Location = New System.Drawing.Point(13, 58)
    Me.lbTb_aconabat.Name = "lbTb_aconabat"
    Me.lbTb_aconabat.NTSDbField = ""
    Me.lbTb_aconabat.Size = New System.Drawing.Size(152, 13)
    Me.lbTb_aconabat.TabIndex = 141
    Me.lbTb_aconabat.Text = "Conto Controp. Abbuoni Attivi"
    Me.lbTb_aconabat.Tooltip = ""
    Me.lbTb_aconabat.UseMnemonic = False
    '
    'lbXx_acontimba
    '
    Me.lbXx_acontimba.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontimba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontimba.Location = New System.Drawing.Point(338, 209)
    Me.lbXx_acontimba.Name = "lbXx_acontimba"
    Me.lbXx_acontimba.NTSDbField = ""
    Me.lbXx_acontimba.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontimba.TabIndex = 140
    Me.lbXx_acontimba.Tooltip = ""
    Me.lbXx_acontimba.UseMnemonic = False
    '
    'edTb_acontimba
    '
    Me.edTb_acontimba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontimba.Location = New System.Drawing.Point(236, 209)
    Me.edTb_acontimba.Name = "edTb_acontimba"
    Me.edTb_acontimba.NTSDbField = ""
    Me.edTb_acontimba.NTSFormat = "0"
    Me.edTb_acontimba.NTSForzaVisZoom = False
    Me.edTb_acontimba.NTSOldValue = ""
    Me.edTb_acontimba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontimba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontimba.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontimba.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontimba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontimba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontimba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontimba.Properties.MaxLength = 65536
    Me.edTb_acontimba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontimba.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontimba.TabIndex = 123
    '
    'lbTb_acontimba
    '
    Me.lbTb_acontimba.AutoSize = True
    Me.lbTb_acontimba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontimba.Location = New System.Drawing.Point(13, 214)
    Me.lbTb_acontimba.Name = "lbTb_acontimba"
    Me.lbTb_acontimba.NTSDbField = ""
    Me.lbTb_acontimba.Size = New System.Drawing.Size(147, 13)
    Me.lbTb_acontimba.TabIndex = 139
    Me.lbTb_acontimba.Text = "Conto Controp. Spese Imballi"
    Me.lbTb_acontimba.Tooltip = ""
    Me.lbTb_acontimba.UseMnemonic = False
    '
    'lbXx_aconriac
    '
    Me.lbXx_aconriac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconriac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconriac.Location = New System.Drawing.Point(338, 183)
    Me.lbXx_aconriac.Name = "lbXx_aconriac"
    Me.lbXx_aconriac.NTSDbField = ""
    Me.lbXx_aconriac.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconriac.TabIndex = 138
    Me.lbXx_aconriac.Tooltip = ""
    Me.lbXx_aconriac.UseMnemonic = False
    '
    'edTb_aconriac
    '
    Me.edTb_aconriac.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_aconriac.Location = New System.Drawing.Point(236, 183)
    Me.edTb_aconriac.Name = "edTb_aconriac"
    Me.edTb_aconriac.NTSDbField = ""
    Me.edTb_aconriac.NTSFormat = "0"
    Me.edTb_aconriac.NTSForzaVisZoom = False
    Me.edTb_aconriac.NTSOldValue = ""
    Me.edTb_aconriac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconriac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconriac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconriac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconriac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconriac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconriac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconriac.Properties.MaxLength = 65536
    Me.edTb_aconriac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconriac.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconriac.TabIndex = 122
    '
    'lbTb_aconriac
    '
    Me.lbTb_aconriac.AutoSize = True
    Me.lbTb_aconriac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconriac.Location = New System.Drawing.Point(13, 188)
    Me.lbTb_aconriac.Name = "lbTb_aconriac"
    Me.lbTb_aconriac.NTSDbField = ""
    Me.lbTb_aconriac.Size = New System.Drawing.Size(172, 13)
    Me.lbTb_aconriac.TabIndex = 137
    Me.lbTb_aconriac.Text = "Conto Controp. Costo Sp. Access."
    Me.lbTb_aconriac.Tooltip = ""
    Me.lbTb_aconriac.UseMnemonic = False
    '
    'lbXx_aconriin
    '
    Me.lbXx_aconriin.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconriin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconriin.Location = New System.Drawing.Point(338, 157)
    Me.lbXx_aconriin.Name = "lbXx_aconriin"
    Me.lbXx_aconriin.NTSDbField = ""
    Me.lbXx_aconriin.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconriin.TabIndex = 136
    Me.lbXx_aconriin.Tooltip = ""
    Me.lbXx_aconriin.UseMnemonic = False
    '
    'edTb_aconriin
    '
    Me.edTb_aconriin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconriin.Location = New System.Drawing.Point(236, 157)
    Me.edTb_aconriin.Name = "edTb_aconriin"
    Me.edTb_aconriin.NTSDbField = ""
    Me.edTb_aconriin.NTSFormat = "0"
    Me.edTb_aconriin.NTSForzaVisZoom = False
    Me.edTb_aconriin.NTSOldValue = ""
    Me.edTb_aconriin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconriin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconriin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconriin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconriin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconriin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconriin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconriin.Properties.MaxLength = 65536
    Me.edTb_aconriin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconriin.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconriin.TabIndex = 121
    '
    'lbTb_aconriin
    '
    Me.lbTb_aconriin.AutoSize = True
    Me.lbTb_aconriin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconriin.Location = New System.Drawing.Point(13, 162)
    Me.lbTb_aconriin.Name = "lbTb_aconriin"
    Me.lbTb_aconriin.NTSDbField = ""
    Me.lbTb_aconriin.Size = New System.Drawing.Size(172, 13)
    Me.lbTb_aconriin.TabIndex = 135
    Me.lbTb_aconriin.Text = "Conto Controp. Costo Sp. Incasso"
    Me.lbTb_aconriin.Tooltip = ""
    Me.lbTb_aconriin.UseMnemonic = False
    '
    'lbXx_aconribo
    '
    Me.lbXx_aconribo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconribo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconribo.Location = New System.Drawing.Point(338, 131)
    Me.lbXx_aconribo.Name = "lbXx_aconribo"
    Me.lbXx_aconribo.NTSDbField = ""
    Me.lbXx_aconribo.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconribo.TabIndex = 134
    Me.lbXx_aconribo.Tooltip = ""
    Me.lbXx_aconribo.UseMnemonic = False
    '
    'edTb_aconribo
    '
    Me.edTb_aconribo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconribo.Location = New System.Drawing.Point(236, 131)
    Me.edTb_aconribo.Name = "edTb_aconribo"
    Me.edTb_aconribo.NTSDbField = ""
    Me.edTb_aconribo.NTSFormat = "0"
    Me.edTb_aconribo.NTSForzaVisZoom = False
    Me.edTb_aconribo.NTSOldValue = ""
    Me.edTb_aconribo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconribo.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconribo.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconribo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconribo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconribo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconribo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconribo.Properties.MaxLength = 65536
    Me.edTb_aconribo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconribo.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconribo.TabIndex = 120
    '
    'lbTb_aconribo
    '
    Me.lbTb_aconribo.AutoSize = True
    Me.lbTb_aconribo.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconribo.Location = New System.Drawing.Point(13, 136)
    Me.lbTb_aconribo.Name = "lbTb_aconribo"
    Me.lbTb_aconribo.NTSDbField = ""
    Me.lbTb_aconribo.Size = New System.Drawing.Size(153, 13)
    Me.lbTb_aconribo.TabIndex = 133
    Me.lbTb_aconribo.Text = "Conto Controp. Costo Sp. Bolli"
    Me.lbTb_aconribo.Tooltip = ""
    Me.lbTb_aconribo.UseMnemonic = False
    '
    'lbXx_aconomag
    '
    Me.lbXx_aconomag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconomag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconomag.Location = New System.Drawing.Point(338, 103)
    Me.lbXx_aconomag.Name = "lbXx_aconomag"
    Me.lbXx_aconomag.NTSDbField = ""
    Me.lbXx_aconomag.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconomag.TabIndex = 132
    Me.lbXx_aconomag.Tooltip = ""
    Me.lbXx_aconomag.UseMnemonic = False
    '
    'edTb_aconomag
    '
    Me.edTb_aconomag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconomag.Location = New System.Drawing.Point(236, 105)
    Me.edTb_aconomag.Name = "edTb_aconomag"
    Me.edTb_aconomag.NTSDbField = ""
    Me.edTb_aconomag.NTSFormat = "0"
    Me.edTb_aconomag.NTSForzaVisZoom = False
    Me.edTb_aconomag.NTSOldValue = ""
    Me.edTb_aconomag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconomag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconomag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconomag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconomag.Properties.MaxLength = 65536
    Me.edTb_aconomag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconomag.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconomag.TabIndex = 119
    '
    'lbTb_aconomag
    '
    Me.lbTb_aconomag.AutoSize = True
    Me.lbTb_aconomag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconomag.Location = New System.Drawing.Point(13, 110)
    Me.lbTb_aconomag.Name = "lbTb_aconomag"
    Me.lbTb_aconomag.NTSDbField = ""
    Me.lbTb_aconomag.Size = New System.Drawing.Size(157, 13)
    Me.lbTb_aconomag.TabIndex = 131
    Me.lbTb_aconomag.Text = "Conto Controp. Omaggi (ricavi)"
    Me.lbTb_aconomag.Tooltip = ""
    Me.lbTb_aconomag.UseMnemonic = False
    '
    'lbXx_aconabpa
    '
    Me.lbXx_aconabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconabpa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconabpa.Location = New System.Drawing.Point(338, 79)
    Me.lbXx_aconabpa.Name = "lbXx_aconabpa"
    Me.lbXx_aconabpa.NTSDbField = ""
    Me.lbXx_aconabpa.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconabpa.TabIndex = 130
    Me.lbXx_aconabpa.Tooltip = ""
    Me.lbXx_aconabpa.UseMnemonic = False
    '
    'edTb_aconabpa
    '
    Me.edTb_aconabpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconabpa.Location = New System.Drawing.Point(236, 79)
    Me.edTb_aconabpa.Name = "edTb_aconabpa"
    Me.edTb_aconabpa.NTSDbField = ""
    Me.edTb_aconabpa.NTSFormat = "0"
    Me.edTb_aconabpa.NTSForzaVisZoom = False
    Me.edTb_aconabpa.NTSOldValue = ""
    Me.edTb_aconabpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconabpa.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconabpa.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconabpa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconabpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconabpa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconabpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconabpa.Properties.MaxLength = 65536
    Me.edTb_aconabpa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconabpa.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconabpa.TabIndex = 118
    '
    'lbTb_aconabpa
    '
    Me.lbTb_aconabpa.AutoSize = True
    Me.lbTb_aconabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconabpa.Location = New System.Drawing.Point(13, 84)
    Me.lbTb_aconabpa.Name = "lbTb_aconabpa"
    Me.lbTb_aconabpa.NTSDbField = ""
    Me.lbTb_aconabpa.Size = New System.Drawing.Size(159, 13)
    Me.lbTb_aconabpa.TabIndex = 129
    Me.lbTb_aconabpa.Text = "Conto Controp. Abbuoni Passivi"
    Me.lbTb_aconabpa.Tooltip = ""
    Me.lbTb_aconabpa.UseMnemonic = False
    '
    'lbXx_aconabat
    '
    Me.lbXx_aconabat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconabat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconabat.Location = New System.Drawing.Point(338, 53)
    Me.lbXx_aconabat.Name = "lbXx_aconabat"
    Me.lbXx_aconabat.NTSDbField = ""
    Me.lbXx_aconabat.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconabat.TabIndex = 128
    Me.lbXx_aconabat.Tooltip = ""
    Me.lbXx_aconabat.UseMnemonic = False
    '
    'edTb_aconabat
    '
    Me.edTb_aconabat.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_aconabat.Location = New System.Drawing.Point(236, 53)
    Me.edTb_aconabat.Name = "edTb_aconabat"
    Me.edTb_aconabat.NTSDbField = ""
    Me.edTb_aconabat.NTSFormat = "0"
    Me.edTb_aconabat.NTSForzaVisZoom = False
    Me.edTb_aconabat.NTSOldValue = ""
    Me.edTb_aconabat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconabat.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconabat.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconabat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconabat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconabat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconabat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconabat.Properties.MaxLength = 65536
    Me.edTb_aconabat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconabat.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconabat.TabIndex = 117
    '
    'lbXx_aconcas
    '
    Me.lbXx_aconcas.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconcas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconcas.Location = New System.Drawing.Point(338, 27)
    Me.lbXx_aconcas.Name = "lbXx_aconcas"
    Me.lbXx_aconcas.NTSDbField = ""
    Me.lbXx_aconcas.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconcas.TabIndex = 127
    Me.lbXx_aconcas.Tooltip = ""
    Me.lbXx_aconcas.UseMnemonic = False
    '
    'edTb_aconcas
    '
    Me.edTb_aconcas.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconcas.Location = New System.Drawing.Point(236, 27)
    Me.edTb_aconcas.Name = "edTb_aconcas"
    Me.edTb_aconcas.NTSDbField = ""
    Me.edTb_aconcas.NTSFormat = "0"
    Me.edTb_aconcas.NTSForzaVisZoom = False
    Me.edTb_aconcas.NTSOldValue = ""
    Me.edTb_aconcas.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconcas.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconcas.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconcas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconcas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconcas.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconcas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconcas.Properties.MaxLength = 65536
    Me.edTb_aconcas.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconcas.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconcas.TabIndex = 116
    '
    'lbTb_aconcas
    '
    Me.lbTb_aconcas.AutoSize = True
    Me.lbTb_aconcas.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconcas.Location = New System.Drawing.Point(13, 32)
    Me.lbTb_aconcas.Name = "lbTb_aconcas"
    Me.lbTb_aconcas.NTSDbField = ""
    Me.lbTb_aconcas.Size = New System.Drawing.Size(114, 13)
    Me.lbTb_aconcas.TabIndex = 126
    Me.lbTb_aconcas.Text = "Conto Controp. Cassa"
    Me.lbTb_aconcas.Tooltip = ""
    Me.lbTb_aconcas.UseMnemonic = False
    '
    'lbXx_aconcauz
    '
    Me.lbXx_aconcauz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_aconcauz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_aconcauz.Location = New System.Drawing.Point(338, 3)
    Me.lbXx_aconcauz.Name = "lbXx_aconcauz"
    Me.lbXx_aconcauz.NTSDbField = ""
    Me.lbXx_aconcauz.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_aconcauz.TabIndex = 125
    Me.lbXx_aconcauz.Tooltip = ""
    Me.lbXx_aconcauz.UseMnemonic = False
    '
    'edTb_aconcauz
    '
    Me.edTb_aconcauz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aconcauz.Location = New System.Drawing.Point(236, 3)
    Me.edTb_aconcauz.Name = "edTb_aconcauz"
    Me.edTb_aconcauz.NTSDbField = ""
    Me.edTb_aconcauz.NTSFormat = "0"
    Me.edTb_aconcauz.NTSForzaVisZoom = False
    Me.edTb_aconcauz.NTSOldValue = ""
    Me.edTb_aconcauz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_aconcauz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_aconcauz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_aconcauz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_aconcauz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aconcauz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aconcauz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aconcauz.Properties.MaxLength = 65536
    Me.edTb_aconcauz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aconcauz.Size = New System.Drawing.Size(96, 20)
    Me.edTb_aconcauz.TabIndex = 115
    '
    'lbTb_aconcauz
    '
    Me.lbTb_aconcauz.AutoSize = True
    Me.lbTb_aconcauz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aconcauz.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_aconcauz.Name = "lbTb_aconcauz"
    Me.lbTb_aconcauz.NTSDbField = ""
    Me.lbTb_aconcauz.Size = New System.Drawing.Size(125, 13)
    Me.lbTb_aconcauz.TabIndex = 124
    Me.lbTb_aconcauz.Text = "Conto Controp. Cauzioni"
    Me.lbTb_aconcauz.Tooltip = ""
    Me.lbTb_aconcauz.UseMnemonic = False
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPeac4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(715, 373)
    Me.NtsTabPage4.Text = "Pagina &4"
    '
    'pnPeac4
    '
    Me.pnPeac4.AllowDrop = True
    Me.pnPeac4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeac4.Appearance.Options.UseBackColor = True
    Me.pnPeac4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrabat)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrimba)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrimba)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrimba)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrriac)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrriac)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrriac)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrriin)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrriin)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrriin)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrribo)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrribo)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrribo)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontromag)
    Me.pnPeac4.Controls.Add(Me.edTb_acontromag)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontromag)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrabpa)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrabpa)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrabpa)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrabat)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrabat)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrcas)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrcas)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrcas)
    Me.pnPeac4.Controls.Add(Me.lbXx_acontrcauz)
    Me.pnPeac4.Controls.Add(Me.edTb_acontrcauz)
    Me.pnPeac4.Controls.Add(Me.lbTb_acontrcauz)
    Me.pnPeac4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeac4.Location = New System.Drawing.Point(3, 3)
    Me.pnPeac4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeac4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeac4.Name = "pnPeac4"
    Me.pnPeac4.NTSActiveTrasparency = True
    Me.pnPeac4.Size = New System.Drawing.Size(712, 341)
    Me.pnPeac4.TabIndex = 0
    Me.pnPeac4.Text = "NtsPanel1"
    '
    'lbTb_acontrabat
    '
    Me.lbTb_acontrabat.AutoSize = True
    Me.lbTb_acontrabat.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrabat.Location = New System.Drawing.Point(13, 58)
    Me.lbTb_acontrabat.Name = "lbTb_acontrabat"
    Me.lbTb_acontrabat.NTSDbField = ""
    Me.lbTb_acontrabat.Size = New System.Drawing.Size(142, 13)
    Me.lbTb_acontrabat.TabIndex = 168
    Me.lbTb_acontrabat.Text = "Contropartita Abbuoni Attivi"
    Me.lbTb_acontrabat.Tooltip = ""
    Me.lbTb_acontrabat.UseMnemonic = False
    '
    'lbXx_acontrimba
    '
    Me.lbXx_acontrimba.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrimba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrimba.Location = New System.Drawing.Point(338, 209)
    Me.lbXx_acontrimba.Name = "lbXx_acontrimba"
    Me.lbXx_acontrimba.NTSDbField = ""
    Me.lbXx_acontrimba.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrimba.TabIndex = 167
    Me.lbXx_acontrimba.Tooltip = ""
    Me.lbXx_acontrimba.UseMnemonic = False
    '
    'edTb_acontrimba
    '
    Me.edTb_acontrimba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrimba.Location = New System.Drawing.Point(236, 209)
    Me.edTb_acontrimba.Name = "edTb_acontrimba"
    Me.edTb_acontrimba.NTSDbField = ""
    Me.edTb_acontrimba.NTSFormat = "0"
    Me.edTb_acontrimba.NTSForzaVisZoom = False
    Me.edTb_acontrimba.NTSOldValue = ""
    Me.edTb_acontrimba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrimba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrimba.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrimba.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrimba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrimba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrimba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrimba.Properties.MaxLength = 65536
    Me.edTb_acontrimba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrimba.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrimba.TabIndex = 150
    '
    'lbTb_acontrimba
    '
    Me.lbTb_acontrimba.AutoSize = True
    Me.lbTb_acontrimba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrimba.Location = New System.Drawing.Point(13, 214)
    Me.lbTb_acontrimba.Name = "lbTb_acontrimba"
    Me.lbTb_acontrimba.NTSDbField = ""
    Me.lbTb_acontrimba.Size = New System.Drawing.Size(155, 13)
    Me.lbTb_acontrimba.TabIndex = 166
    Me.lbTb_acontrimba.Text = "Contropartita Costo Sp. Imballi"
    Me.lbTb_acontrimba.Tooltip = ""
    Me.lbTb_acontrimba.UseMnemonic = False
    '
    'lbXx_acontrriac
    '
    Me.lbXx_acontrriac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrriac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrriac.Location = New System.Drawing.Point(338, 183)
    Me.lbXx_acontrriac.Name = "lbXx_acontrriac"
    Me.lbXx_acontrriac.NTSDbField = ""
    Me.lbXx_acontrriac.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrriac.TabIndex = 165
    Me.lbXx_acontrriac.Tooltip = ""
    Me.lbXx_acontrriac.UseMnemonic = False
    '
    'edTb_acontrriac
    '
    Me.edTb_acontrriac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrriac.Location = New System.Drawing.Point(236, 183)
    Me.edTb_acontrriac.Name = "edTb_acontrriac"
    Me.edTb_acontrriac.NTSDbField = ""
    Me.edTb_acontrriac.NTSFormat = "0"
    Me.edTb_acontrriac.NTSForzaVisZoom = False
    Me.edTb_acontrriac.NTSOldValue = ""
    Me.edTb_acontrriac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrriac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrriac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrriac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrriac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrriac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrriac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrriac.Properties.MaxLength = 65536
    Me.edTb_acontrriac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrriac.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrriac.TabIndex = 149
    '
    'lbTb_acontrriac
    '
    Me.lbTb_acontrriac.AutoSize = True
    Me.lbTb_acontrriac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrriac.Location = New System.Drawing.Point(13, 188)
    Me.lbTb_acontrriac.Name = "lbTb_acontrriac"
    Me.lbTb_acontrriac.NTSDbField = ""
    Me.lbTb_acontrriac.Size = New System.Drawing.Size(162, 13)
    Me.lbTb_acontrriac.TabIndex = 164
    Me.lbTb_acontrriac.Text = "Contropartita Costo Sp. Access."
    Me.lbTb_acontrriac.Tooltip = ""
    Me.lbTb_acontrriac.UseMnemonic = False
    '
    'lbXx_acontrriin
    '
    Me.lbXx_acontrriin.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrriin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrriin.Location = New System.Drawing.Point(338, 157)
    Me.lbXx_acontrriin.Name = "lbXx_acontrriin"
    Me.lbXx_acontrriin.NTSDbField = ""
    Me.lbXx_acontrriin.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrriin.TabIndex = 163
    Me.lbXx_acontrriin.Tooltip = ""
    Me.lbXx_acontrriin.UseMnemonic = False
    '
    'edTb_acontrriin
    '
    Me.edTb_acontrriin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrriin.Location = New System.Drawing.Point(236, 157)
    Me.edTb_acontrriin.Name = "edTb_acontrriin"
    Me.edTb_acontrriin.NTSDbField = ""
    Me.edTb_acontrriin.NTSFormat = "0"
    Me.edTb_acontrriin.NTSForzaVisZoom = False
    Me.edTb_acontrriin.NTSOldValue = ""
    Me.edTb_acontrriin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrriin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrriin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrriin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrriin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrriin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrriin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrriin.Properties.MaxLength = 65536
    Me.edTb_acontrriin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrriin.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrriin.TabIndex = 148
    '
    'lbTb_acontrriin
    '
    Me.lbTb_acontrriin.AutoSize = True
    Me.lbTb_acontrriin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrriin.Location = New System.Drawing.Point(13, 162)
    Me.lbTb_acontrriin.Name = "lbTb_acontrriin"
    Me.lbTb_acontrriin.NTSDbField = ""
    Me.lbTb_acontrriin.Size = New System.Drawing.Size(162, 13)
    Me.lbTb_acontrriin.TabIndex = 162
    Me.lbTb_acontrriin.Text = "Contropartita Costo Sp. Incasso"
    Me.lbTb_acontrriin.Tooltip = ""
    Me.lbTb_acontrriin.UseMnemonic = False
    '
    'lbXx_acontrribo
    '
    Me.lbXx_acontrribo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrribo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrribo.Location = New System.Drawing.Point(338, 131)
    Me.lbXx_acontrribo.Name = "lbXx_acontrribo"
    Me.lbXx_acontrribo.NTSDbField = ""
    Me.lbXx_acontrribo.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrribo.TabIndex = 161
    Me.lbXx_acontrribo.Tooltip = ""
    Me.lbXx_acontrribo.UseMnemonic = False
    '
    'edTb_acontrribo
    '
    Me.edTb_acontrribo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrribo.Location = New System.Drawing.Point(236, 131)
    Me.edTb_acontrribo.Name = "edTb_acontrribo"
    Me.edTb_acontrribo.NTSDbField = ""
    Me.edTb_acontrribo.NTSFormat = "0"
    Me.edTb_acontrribo.NTSForzaVisZoom = False
    Me.edTb_acontrribo.NTSOldValue = ""
    Me.edTb_acontrribo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrribo.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrribo.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrribo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrribo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrribo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrribo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrribo.Properties.MaxLength = 65536
    Me.edTb_acontrribo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrribo.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrribo.TabIndex = 147
    '
    'lbTb_acontrribo
    '
    Me.lbTb_acontrribo.AutoSize = True
    Me.lbTb_acontrribo.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrribo.Location = New System.Drawing.Point(13, 136)
    Me.lbTb_acontrribo.Name = "lbTb_acontrribo"
    Me.lbTb_acontrribo.NTSDbField = ""
    Me.lbTb_acontrribo.Size = New System.Drawing.Size(143, 13)
    Me.lbTb_acontrribo.TabIndex = 160
    Me.lbTb_acontrribo.Text = "Contropartita Costo Sp. Bolli"
    Me.lbTb_acontrribo.Tooltip = ""
    Me.lbTb_acontrribo.UseMnemonic = False
    '
    'lbXx_acontromag
    '
    Me.lbXx_acontromag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontromag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontromag.Location = New System.Drawing.Point(338, 103)
    Me.lbXx_acontromag.Name = "lbXx_acontromag"
    Me.lbXx_acontromag.NTSDbField = ""
    Me.lbXx_acontromag.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontromag.TabIndex = 159
    Me.lbXx_acontromag.Tooltip = ""
    Me.lbXx_acontromag.UseMnemonic = False
    '
    'edTb_acontromag
    '
    Me.edTb_acontromag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontromag.Location = New System.Drawing.Point(236, 105)
    Me.edTb_acontromag.Name = "edTb_acontromag"
    Me.edTb_acontromag.NTSDbField = ""
    Me.edTb_acontromag.NTSFormat = "0"
    Me.edTb_acontromag.NTSForzaVisZoom = False
    Me.edTb_acontromag.NTSOldValue = ""
    Me.edTb_acontromag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontromag.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontromag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontromag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontromag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontromag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontromag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontromag.Properties.MaxLength = 65536
    Me.edTb_acontromag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontromag.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontromag.TabIndex = 146
    '
    'lbTb_acontromag
    '
    Me.lbTb_acontromag.AutoSize = True
    Me.lbTb_acontromag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontromag.Location = New System.Drawing.Point(13, 110)
    Me.lbTb_acontromag.Name = "lbTb_acontromag"
    Me.lbTb_acontromag.NTSDbField = ""
    Me.lbTb_acontromag.Size = New System.Drawing.Size(147, 13)
    Me.lbTb_acontromag.TabIndex = 158
    Me.lbTb_acontromag.Text = "Contropartita Omaggi (ricavi)"
    Me.lbTb_acontromag.Tooltip = ""
    Me.lbTb_acontromag.UseMnemonic = False
    '
    'lbXx_acontrabpa
    '
    Me.lbXx_acontrabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrabpa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrabpa.Location = New System.Drawing.Point(338, 79)
    Me.lbXx_acontrabpa.Name = "lbXx_acontrabpa"
    Me.lbXx_acontrabpa.NTSDbField = ""
    Me.lbXx_acontrabpa.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrabpa.TabIndex = 157
    Me.lbXx_acontrabpa.Tooltip = ""
    Me.lbXx_acontrabpa.UseMnemonic = False
    '
    'edTb_acontrabpa
    '
    Me.edTb_acontrabpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrabpa.Location = New System.Drawing.Point(236, 79)
    Me.edTb_acontrabpa.Name = "edTb_acontrabpa"
    Me.edTb_acontrabpa.NTSDbField = ""
    Me.edTb_acontrabpa.NTSFormat = "0"
    Me.edTb_acontrabpa.NTSForzaVisZoom = False
    Me.edTb_acontrabpa.NTSOldValue = ""
    Me.edTb_acontrabpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrabpa.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrabpa.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrabpa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrabpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrabpa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrabpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrabpa.Properties.MaxLength = 65536
    Me.edTb_acontrabpa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrabpa.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrabpa.TabIndex = 145
    '
    'lbTb_acontrabpa
    '
    Me.lbTb_acontrabpa.AutoSize = True
    Me.lbTb_acontrabpa.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrabpa.Location = New System.Drawing.Point(13, 84)
    Me.lbTb_acontrabpa.Name = "lbTb_acontrabpa"
    Me.lbTb_acontrabpa.NTSDbField = ""
    Me.lbTb_acontrabpa.Size = New System.Drawing.Size(149, 13)
    Me.lbTb_acontrabpa.TabIndex = 156
    Me.lbTb_acontrabpa.Text = "Contropartita Abbuoni Passivi"
    Me.lbTb_acontrabpa.Tooltip = ""
    Me.lbTb_acontrabpa.UseMnemonic = False
    '
    'lbXx_acontrabat
    '
    Me.lbXx_acontrabat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrabat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrabat.Location = New System.Drawing.Point(338, 53)
    Me.lbXx_acontrabat.Name = "lbXx_acontrabat"
    Me.lbXx_acontrabat.NTSDbField = ""
    Me.lbXx_acontrabat.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrabat.TabIndex = 155
    Me.lbXx_acontrabat.Tooltip = ""
    Me.lbXx_acontrabat.UseMnemonic = False
    '
    'edTb_acontrabat
    '
    Me.edTb_acontrabat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrabat.Location = New System.Drawing.Point(236, 53)
    Me.edTb_acontrabat.Name = "edTb_acontrabat"
    Me.edTb_acontrabat.NTSDbField = ""
    Me.edTb_acontrabat.NTSFormat = "0"
    Me.edTb_acontrabat.NTSForzaVisZoom = False
    Me.edTb_acontrabat.NTSOldValue = ""
    Me.edTb_acontrabat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrabat.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrabat.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrabat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrabat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrabat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrabat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrabat.Properties.MaxLength = 65536
    Me.edTb_acontrabat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrabat.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrabat.TabIndex = 144
    '
    'lbXx_acontrcas
    '
    Me.lbXx_acontrcas.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrcas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrcas.Location = New System.Drawing.Point(338, 27)
    Me.lbXx_acontrcas.Name = "lbXx_acontrcas"
    Me.lbXx_acontrcas.NTSDbField = ""
    Me.lbXx_acontrcas.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrcas.TabIndex = 154
    Me.lbXx_acontrcas.Tooltip = ""
    Me.lbXx_acontrcas.UseMnemonic = False
    '
    'edTb_acontrcas
    '
    Me.edTb_acontrcas.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrcas.Location = New System.Drawing.Point(236, 27)
    Me.edTb_acontrcas.Name = "edTb_acontrcas"
    Me.edTb_acontrcas.NTSDbField = ""
    Me.edTb_acontrcas.NTSFormat = "0"
    Me.edTb_acontrcas.NTSForzaVisZoom = False
    Me.edTb_acontrcas.NTSOldValue = ""
    Me.edTb_acontrcas.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrcas.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrcas.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrcas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrcas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrcas.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrcas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrcas.Properties.MaxLength = 65536
    Me.edTb_acontrcas.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrcas.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrcas.TabIndex = 143
    '
    'lbTb_acontrcas
    '
    Me.lbTb_acontrcas.AutoSize = True
    Me.lbTb_acontrcas.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrcas.Location = New System.Drawing.Point(13, 32)
    Me.lbTb_acontrcas.Name = "lbTb_acontrcas"
    Me.lbTb_acontrcas.NTSDbField = ""
    Me.lbTb_acontrcas.Size = New System.Drawing.Size(104, 13)
    Me.lbTb_acontrcas.TabIndex = 153
    Me.lbTb_acontrcas.Text = "Contropartita Cassa"
    Me.lbTb_acontrcas.Tooltip = ""
    Me.lbTb_acontrcas.UseMnemonic = False
    '
    'lbXx_acontrcauz
    '
    Me.lbXx_acontrcauz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_acontrcauz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_acontrcauz.Location = New System.Drawing.Point(338, 3)
    Me.lbXx_acontrcauz.Name = "lbXx_acontrcauz"
    Me.lbXx_acontrcauz.NTSDbField = ""
    Me.lbXx_acontrcauz.Size = New System.Drawing.Size(359, 20)
    Me.lbXx_acontrcauz.TabIndex = 152
    Me.lbXx_acontrcauz.Tooltip = ""
    Me.lbXx_acontrcauz.UseMnemonic = False
    '
    'edTb_acontrcauz
    '
    Me.edTb_acontrcauz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_acontrcauz.Location = New System.Drawing.Point(236, 3)
    Me.edTb_acontrcauz.Name = "edTb_acontrcauz"
    Me.edTb_acontrcauz.NTSDbField = ""
    Me.edTb_acontrcauz.NTSFormat = "0"
    Me.edTb_acontrcauz.NTSForzaVisZoom = False
    Me.edTb_acontrcauz.NTSOldValue = ""
    Me.edTb_acontrcauz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_acontrcauz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_acontrcauz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_acontrcauz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_acontrcauz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_acontrcauz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_acontrcauz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_acontrcauz.Properties.MaxLength = 65536
    Me.edTb_acontrcauz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_acontrcauz.Size = New System.Drawing.Size(96, 20)
    Me.edTb_acontrcauz.TabIndex = 142
    '
    'lbTb_acontrcauz
    '
    Me.lbTb_acontrcauz.AutoSize = True
    Me.lbTb_acontrcauz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_acontrcauz.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_acontrcauz.Name = "lbTb_acontrcauz"
    Me.lbTb_acontrcauz.NTSDbField = ""
    Me.lbTb_acontrcauz.Size = New System.Drawing.Size(115, 13)
    Me.lbTb_acontrcauz.TabIndex = 151
    Me.lbTb_acontrcauz.Text = "Contropartita Cauzioni"
    Me.lbTb_acontrcauz.Tooltip = ""
    Me.lbTb_acontrcauz.UseMnemonic = False
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.NtsPanel1)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(715, 373)
    Me.NtsTabPage5.Text = "Pagina &5"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrko)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkh)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkh)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkh)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrko)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrko)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkt)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkt)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkt)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkm)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkm)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkm)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkl)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkl)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkl)
    Me.NtsPanel1.Controls.Add(Me.lbXx_tipobfrkj)
    Me.NtsPanel1.Controls.Add(Me.edTb_tipobfrkj)
    Me.NtsPanel1.Controls.Add(Me.lbTb_tipobfrkj)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(1, 3)
    Me.NtsPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(712, 367)
    Me.NtsPanel1.TabIndex = 2
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'lbXx_tipobfrko
    '
    Me.lbXx_tipobfrko.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrko.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrko.Location = New System.Drawing.Point(311, 107)
    Me.lbXx_tipobfrko.Name = "lbXx_tipobfrko"
    Me.lbXx_tipobfrko.NTSDbField = ""
    Me.lbXx_tipobfrko.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrko.TabIndex = 108
    Me.lbXx_tipobfrko.Tooltip = ""
    Me.lbXx_tipobfrko.UseMnemonic = False
    '
    'lbXx_tipobfrkh
    '
    Me.lbXx_tipobfrkh.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkh.Location = New System.Drawing.Point(311, 133)
    Me.lbXx_tipobfrkh.Name = "lbXx_tipobfrkh"
    Me.lbXx_tipobfrkh.NTSDbField = ""
    Me.lbXx_tipobfrkh.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkh.TabIndex = 107
    Me.lbXx_tipobfrkh.Tooltip = ""
    Me.lbXx_tipobfrkh.UseMnemonic = False
    '
    'edTb_tipobfrkh
    '
    Me.edTb_tipobfrkh.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkh.Location = New System.Drawing.Point(236, 133)
    Me.edTb_tipobfrkh.Name = "edTb_tipobfrkh"
    Me.edTb_tipobfrkh.NTSDbField = ""
    Me.edTb_tipobfrkh.NTSFormat = "0"
    Me.edTb_tipobfrkh.NTSForzaVisZoom = False
    Me.edTb_tipobfrkh.NTSOldValue = ""
    Me.edTb_tipobfrkh.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkh.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkh.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkh.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkh.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkh.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkh.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkh.Properties.MaxLength = 65536
    Me.edTb_tipobfrkh.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkh.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkh.TabIndex = 26
    '
    'lbTb_tipobfrkh
    '
    Me.lbTb_tipobfrkh.AutoSize = True
    Me.lbTb_tipobfrkh.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkh.Location = New System.Drawing.Point(13, 136)
    Me.lbTb_tipobfrkh.Name = "lbTb_tipobfrkh"
    Me.lbTb_tipobfrkh.NTSDbField = ""
    Me.lbTb_tipobfrkh.Size = New System.Drawing.Size(198, 13)
    Me.lbTb_tipobfrkh.TabIndex = 106
    Me.lbTb_tipobfrkh.Text = "Tipo bolla/fattura per ORDINI DI PROD."
    Me.lbTb_tipobfrkh.Tooltip = ""
    Me.lbTb_tipobfrkh.UseMnemonic = False
    '
    'edTb_tipobfrko
    '
    Me.edTb_tipobfrko.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrko.Location = New System.Drawing.Point(236, 107)
    Me.edTb_tipobfrko.Name = "edTb_tipobfrko"
    Me.edTb_tipobfrko.NTSDbField = ""
    Me.edTb_tipobfrko.NTSFormat = "0"
    Me.edTb_tipobfrko.NTSForzaVisZoom = False
    Me.edTb_tipobfrko.NTSOldValue = ""
    Me.edTb_tipobfrko.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrko.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrko.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrko.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrko.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrko.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrko.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrko.Properties.MaxLength = 65536
    Me.edTb_tipobfrko.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrko.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrko.TabIndex = 25
    '
    'lbTb_tipobfrko
    '
    Me.lbTb_tipobfrko.AutoSize = True
    Me.lbTb_tipobfrko.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrko.Location = New System.Drawing.Point(13, 110)
    Me.lbTb_tipobfrko.Name = "lbTb_tipobfrko"
    Me.lbTb_tipobfrko.NTSDbField = ""
    Me.lbTb_tipobfrko.Size = New System.Drawing.Size(214, 13)
    Me.lbTb_tipobfrko.TabIndex = 104
    Me.lbTb_tipobfrko.Text = "Tipo bolla/fattura per ORD. F./ORD. F. AP."
    Me.lbTb_tipobfrko.Tooltip = ""
    Me.lbTb_tipobfrko.UseMnemonic = False
    '
    'lbXx_tipobfrkt
    '
    Me.lbXx_tipobfrkt.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkt.Location = New System.Drawing.Point(311, 81)
    Me.lbXx_tipobfrkt.Name = "lbXx_tipobfrkt"
    Me.lbXx_tipobfrkt.NTSDbField = ""
    Me.lbXx_tipobfrkt.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkt.TabIndex = 102
    Me.lbXx_tipobfrkt.Tooltip = ""
    Me.lbXx_tipobfrkt.UseMnemonic = False
    '
    'edTb_tipobfrkt
    '
    Me.edTb_tipobfrkt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkt.Location = New System.Drawing.Point(236, 81)
    Me.edTb_tipobfrkt.Name = "edTb_tipobfrkt"
    Me.edTb_tipobfrkt.NTSDbField = ""
    Me.edTb_tipobfrkt.NTSFormat = "0"
    Me.edTb_tipobfrkt.NTSForzaVisZoom = False
    Me.edTb_tipobfrkt.NTSOldValue = ""
    Me.edTb_tipobfrkt.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkt.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkt.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkt.Properties.MaxLength = 65536
    Me.edTb_tipobfrkt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkt.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkt.TabIndex = 24
    '
    'lbTb_tipobfrkt
    '
    Me.lbTb_tipobfrkt.AutoSize = True
    Me.lbTb_tipobfrkt.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkt.Location = New System.Drawing.Point(13, 84)
    Me.lbTb_tipobfrkt.Name = "lbTb_tipobfrkt"
    Me.lbTb_tipobfrkt.NTSDbField = ""
    Me.lbTb_tipobfrkt.Size = New System.Drawing.Size(207, 13)
    Me.lbTb_tipobfrkt.TabIndex = 101
    Me.lbTb_tipobfrkt.Text = "Tipo bolla/fattura per CARICHI DA PROD."
    Me.lbTb_tipobfrkt.Tooltip = ""
    Me.lbTb_tipobfrkt.UseMnemonic = False
    '
    'lbXx_tipobfrkm
    '
    Me.lbXx_tipobfrkm.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkm.Location = New System.Drawing.Point(311, 55)
    Me.lbXx_tipobfrkm.Name = "lbXx_tipobfrkm"
    Me.lbXx_tipobfrkm.NTSDbField = ""
    Me.lbXx_tipobfrkm.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkm.TabIndex = 99
    Me.lbXx_tipobfrkm.Tooltip = ""
    Me.lbXx_tipobfrkm.UseMnemonic = False
    '
    'edTb_tipobfrkm
    '
    Me.edTb_tipobfrkm.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_tipobfrkm.Location = New System.Drawing.Point(236, 55)
    Me.edTb_tipobfrkm.Name = "edTb_tipobfrkm"
    Me.edTb_tipobfrkm.NTSDbField = ""
    Me.edTb_tipobfrkm.NTSFormat = "0"
    Me.edTb_tipobfrkm.NTSForzaVisZoom = False
    Me.edTb_tipobfrkm.NTSOldValue = ""
    Me.edTb_tipobfrkm.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkm.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkm.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkm.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkm.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkm.Properties.MaxLength = 65536
    Me.edTb_tipobfrkm.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkm.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkm.TabIndex = 23
    '
    'lbTb_tipobfrkm
    '
    Me.lbTb_tipobfrkm.AutoSize = True
    Me.lbTb_tipobfrkm.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkm.Location = New System.Drawing.Point(13, 58)
    Me.lbTb_tipobfrkm.Name = "lbTb_tipobfrkm"
    Me.lbTb_tipobfrkm.NTSDbField = ""
    Me.lbTb_tipobfrkm.Size = New System.Drawing.Size(182, 13)
    Me.lbTb_tipobfrkm.TabIndex = 98
    Me.lbTb_tipobfrkm.Text = "Tipo bolla/fattura per DDT RICEVUTI"
    Me.lbTb_tipobfrkm.Tooltip = ""
    Me.lbTb_tipobfrkm.UseMnemonic = False
    '
    'lbXx_tipobfrkl
    '
    Me.lbXx_tipobfrkl.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkl.Location = New System.Drawing.Point(311, 29)
    Me.lbXx_tipobfrkl.Name = "lbXx_tipobfrkl"
    Me.lbXx_tipobfrkl.NTSDbField = ""
    Me.lbXx_tipobfrkl.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkl.TabIndex = 96
    Me.lbXx_tipobfrkl.Tooltip = ""
    Me.lbXx_tipobfrkl.UseMnemonic = False
    '
    'edTb_tipobfrkl
    '
    Me.edTb_tipobfrkl.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_tipobfrkl.Location = New System.Drawing.Point(236, 29)
    Me.edTb_tipobfrkl.Name = "edTb_tipobfrkl"
    Me.edTb_tipobfrkl.NTSDbField = ""
    Me.edTb_tipobfrkl.NTSFormat = "0"
    Me.edTb_tipobfrkl.NTSForzaVisZoom = False
    Me.edTb_tipobfrkl.NTSOldValue = ""
    Me.edTb_tipobfrkl.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkl.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkl.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkl.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkl.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkl.Properties.MaxLength = 65536
    Me.edTb_tipobfrkl.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkl.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkl.TabIndex = 22
    '
    'lbTb_tipobfrkl
    '
    Me.lbTb_tipobfrkl.AutoSize = True
    Me.lbTb_tipobfrkl.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkl.Location = New System.Drawing.Point(13, 32)
    Me.lbTb_tipobfrkl.Name = "lbTb_tipobfrkl"
    Me.lbTb_tipobfrkl.NTSDbField = ""
    Me.lbTb_tipobfrkl.Size = New System.Drawing.Size(193, 13)
    Me.lbTb_tipobfrkl.TabIndex = 95
    Me.lbTb_tipobfrkl.Text = "Tipo bolla/fattura per FATT. IMM. RIC."
    Me.lbTb_tipobfrkl.Tooltip = ""
    Me.lbTb_tipobfrkl.UseMnemonic = False
    '
    'lbXx_tipobfrkj
    '
    Me.lbXx_tipobfrkj.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobfrkj.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobfrkj.Location = New System.Drawing.Point(311, 3)
    Me.lbXx_tipobfrkj.Name = "lbXx_tipobfrkj"
    Me.lbXx_tipobfrkj.NTSDbField = ""
    Me.lbXx_tipobfrkj.Size = New System.Drawing.Size(386, 20)
    Me.lbXx_tipobfrkj.TabIndex = 93
    Me.lbXx_tipobfrkj.Tooltip = ""
    Me.lbXx_tipobfrkj.UseMnemonic = False
    '
    'edTb_tipobfrkj
    '
    Me.edTb_tipobfrkj.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipobfrkj.Location = New System.Drawing.Point(236, 3)
    Me.edTb_tipobfrkj.Name = "edTb_tipobfrkj"
    Me.edTb_tipobfrkj.NTSDbField = ""
    Me.edTb_tipobfrkj.NTSFormat = "0"
    Me.edTb_tipobfrkj.NTSForzaVisZoom = False
    Me.edTb_tipobfrkj.NTSOldValue = ""
    Me.edTb_tipobfrkj.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tipobfrkj.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tipobfrkj.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tipobfrkj.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tipobfrkj.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipobfrkj.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipobfrkj.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipobfrkj.Properties.MaxLength = 65536
    Me.edTb_tipobfrkj.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipobfrkj.Size = New System.Drawing.Size(69, 20)
    Me.edTb_tipobfrkj.TabIndex = 21
    '
    'lbTb_tipobfrkj
    '
    Me.lbTb_tipobfrkj.AutoSize = True
    Me.lbTb_tipobfrkj.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipobfrkj.Location = New System.Drawing.Point(13, 6)
    Me.lbTb_tipobfrkj.Name = "lbTb_tipobfrkj"
    Me.lbTb_tipobfrkj.NTSDbField = ""
    Me.lbTb_tipobfrkj.Size = New System.Drawing.Size(226, 13)
    Me.lbTb_tipobfrkj.TabIndex = 92
    Me.lbTb_tipobfrkj.Text = "Tipo bolla/fattura per NOTE DI ACCRED. RIC."
    Me.lbTb_tipobfrkj.Tooltip = ""
    Me.lbTb_tipobfrkj.UseMnemonic = False
    '
    'tlbTipiBF
    '
    Me.tlbTipiBF.AccessibleName = ""
    Me.tlbTipiBF.Caption = "Tipi B/F per serie/tipo documento"
    Me.tlbTipiBF.Id = 6
    Me.tlbTipiBF.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbTipiBF.Name = "tlbTipiBF"
    Me.tlbTipiBF.NTSIsCheckBox = False
    Me.tlbTipiBF.Visible = True
    '
    'FRMMGPEAC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(748, 437)
    Me.Controls.Add(Me.tabPeac)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMMGPEAC"
    Me.Text = "PERSONALIZZAZIONE ACQUISTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tabPeac, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabPeac.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPeac1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeac1.ResumeLayout(False)
    Me.pnPeac1.PerformLayout()
    CType(Me.edTb_caurcfs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_adatfinivainc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aivainc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_adatfinivainc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aivainc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcfrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcnextrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcftextrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aiva15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcnacee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcftcee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_causcapro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aivainc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caustomac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caupag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcna.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_caurcft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPeac2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeac2.ResumeLayout(False)
    Me.pnPeac2.PerformLayout()
    CType(Me.fmScorporo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmScorporo.ResumeLayout(False)
    Me.fmScorporo.PerformLayout()
    CType(Me.ckTb_ascorpo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_listcstd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_alistin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ccdfconv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ccdaconv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acaumag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_amagazz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_atipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPeac3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeac3.ResumeLayout(False)
    Me.pnPeac3.PerformLayout()
    CType(Me.edTb_acontimba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconriac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconriin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconribo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconomag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconabpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconabat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconcas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aconcauz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPeac4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeac4.ResumeLayout(False)
    Me.pnPeac4.PerformLayout()
    CType(Me.edTb_acontrimba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrriac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrriin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrribo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontromag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrabpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrabat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrcas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_acontrcauz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edTb_tipobfrkh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipobfrkj.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      edTb_caurcft.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250351032416, "Causale Ricevimento Fatture"), tabcauc)
      edTb_caurcna.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250350563483, "Causale Ric.Note Accredito"), tabcauc)
      edTb_caupag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250350094550, "Causale Pagamento"), tabcauc)
      edTb_caustomac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250349625617, "Causale Storno Omaggi"), tabcauc)
      edTb_aivainc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250349156684, "Codice IVA Spese Incasso"), tabciva)
      edTb_aivainc2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129636658842314453, "Codice Iva spese incasso 2"), tabciva)
      edTb_aivainc3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129636658856796875, "Codice Iva spese incasso 3"), tabciva)
      edTb_adatfinivainc3.NTSSetParam(oMenu, oApp.Tr(Me, 129636659026552734, "Data fine validità Codice Iva spese incasso 3"), False)
      edTb_adatfinivainc2.NTSSetParam(oMenu, oApp.Tr(Me, 129636659253115234, "Data fine validità Codice Iva spese incasso 2"), False)
      edTb_aiva15.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250348687751, "Codice IVA Escl. Articolo 15"), tabciva)
      edTb_causcapro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250348218818, "Causale Mag. Scarico Produzione"), tabcaum)
      edTb_caurcftcee.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250347749885, "Caus.ricev.fatt.intracomunitaria"), tabcauc)
      edTb_caurcnacee.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250347280952, "Caus.ricev.NA.intracomunitaria"), tabcauc)
      edTb_caurcftextrc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129460394465683593, "Caus.ricev.fatt.extracee"), tabcauc)
      edTb_caurcnextrc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129460394477197265, "Caus.ricev.NA.extracee"), tabcauc)
      edTb_caurcfrc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129617559033339844, "Caus.ricev.fatt.reverse charge"), tabcauc)
      edTb_caurcarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129617559607656250, "Caus.ricev.NA.reverse charge"), tabcauc)
      edTb_caurcfs.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129767970138754215, "Caus.ricev.fattura IVA differita"), tabcauc)
      '-------------------------------------------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------------------------------------------
      edTb_atipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250354940191, "Tipo Bolla/Fattura generico per Test.Bolla"), tabtpbf)
      edTb_acontro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250354471258, "Codice Controp.Testata Bolla"), tabcove)
      edTb_amagazz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250354002325, "Cod.Magazz.Car.Test. Bolla"), tabmaga)
      edTb_acaumag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250353533392, "Cod.Caus.Magazz.Test.Bolla"), tabcaum)
      edTb_ccdaconv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250353064459, "Controp.diff.attive di convers."), tabcove)
      edTb_ccdfconv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250352126593, "Conto contropartita Ricavi spese accessorie"), tabcove)
      edTb_alistin.NTSSetParam(oMenu, oApp.Tr(Me, 128432250351657660, "Listino Testata Bolla"), "0", 4, 0, 9999)
      edTb_listcstd.NTSSetParam(oMenu, oApp.Tr(Me, 128432250351345038, "Listino costi Standard"), "0", 4, 0, 9999)
      ckTb_ascorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128432250351188727, "Scorporo in Testata Bolla"), "S", "N")
      '-------------------------------------------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------------------------------------------
      edTb_aconcauz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250359004277, "Conto Controp. Cauzioni"), tabanagras)
      edTb_aconcas.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250358535344, "Conto Controp. Cassa"), tabanagras)
      edTb_aconabat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250358222722, "Conto Controp. Abbuoni Attivi"), tabanagras)
      edTb_aconabpa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250357753789, "Conto Controp. Abbuoni Passivi"), tabanagras)
      edTb_aconomag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250357284856, "Conto Controp. Omaggi (ricavi)"), tabanagras)
      edTb_aconribo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250356815923, "Conto Controp. Costo Sp. Bolli"), tabanagras)
      edTb_aconriin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250356346990, "Conto Controp. Costo Sp. Incasso"), tabanagras)
      edTb_aconriac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250355878057, "Conto Controp. Costo Sp. Access."), tabanagras)
      edTb_acontimba.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250355409124, "Conto Controp. Spese Imballi"), tabanagras)
      '-------------------------------------------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------------------------------------------
      edTb_acontrcauz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250363068363, "Contropartita Cauzioni"), tabcovg)
      edTb_acontrcas.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250362599430, "Contropartita Cassa"), tabcovg)
      edTb_acontrabat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250362286808, "Contropartita Abbuoni Attivi"), tabcovg)
      edTb_acontrabpa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250361817875, "Contropartita Abbuoni Passivi"), tabcovg)
      edTb_acontromag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250361348942, "Contropartita Omaggi (ricavi)"), tabcovg)
      edTb_acontrribo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250360880009, "Contropartita Costo Sp. Bolli"), tabcovg)
      edTb_acontrriin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250360411076, "Contropartita Costo Sp. Incasso"), tabcovg)
      edTb_acontrriac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250359942143, "Contropartita Costo Sp. Incasso"), tabcovg)
      edTb_acontrimba.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128432250359473210, "Contropartita Costo Sp. Imballi"), tabcovg)
      '-------------------------------------------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------------------------------------------
      edTb_tipobfrkj.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185268128482681, "Tipo Bolla/Fattura per NOTE DI ACCRED RICEVUTE"), tabtpbf)
      edTb_tipobfrkl.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185268792241177, "Tipo Bolla/Fattura per FATTURE IMMEDIATE RICEVUTE"), tabtpbf)
      edTb_tipobfrkm.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185269058963341, "Tipo Bolla/Fattura per DDT RICEVUTI"), tabtpbf)
      edTb_tipobfrkt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185269346939569, "Tipo Bolla/Fattura per CARICHI DA PRODUZIONE"), tabtpbf)
      edTb_tipobfrko.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185269674143821, "Tipo Bolla/Fattura per ORDINI FORNITORE/ORDINI FORNITORE APERTI"), tabtpbf)
      edTb_tipobfrkh.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130185270229172237, "Tipo Bolla/Fattura per ORDINI DI PRODUZIONE"), tabtpbf)
      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      edTb_caurcft.NTSForzaVisZoom = True
      edTb_caurcna.NTSForzaVisZoom = True
      edTb_caupag.NTSForzaVisZoom = True
      edTb_caustomac.NTSForzaVisZoom = True
      edTb_aivainc.NTSForzaVisZoom = True
      edTb_aiva15.NTSForzaVisZoom = True
      edTb_causcapro.NTSForzaVisZoom = True
      edTb_caurcftcee.NTSForzaVisZoom = True
      edTb_caurcnacee.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------------------------------------------
      edTb_atipobf.NTSForzaVisZoom = True
      edTb_acontro.NTSForzaVisZoom = True
      edTb_amagazz.NTSForzaVisZoom = True
      edTb_acaumag.NTSForzaVisZoom = True
      edTb_ccdaconv.NTSForzaVisZoom = True
      edTb_ccdfconv.NTSForzaVisZoom = True
      edTb_alistin.NTSForzaVisZoom = True
      edTb_listcstd.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------------------------------------------
      edTb_aconcauz.NTSForzaVisZoom = True
      edTb_aconcas.NTSForzaVisZoom = True
      edTb_aconabat.NTSForzaVisZoom = True
      edTb_aconabpa.NTSForzaVisZoom = True
      edTb_aconomag.NTSForzaVisZoom = True
      edTb_aconribo.NTSForzaVisZoom = True
      edTb_aconriin.NTSForzaVisZoom = True
      edTb_aconriac.NTSForzaVisZoom = True
      edTb_acontimba.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------------------------------------------
      edTb_acontrcauz.NTSForzaVisZoom = True
      edTb_acontrcas.NTSForzaVisZoom = True
      edTb_acontrabat.NTSForzaVisZoom = True
      edTb_acontrabpa.NTSForzaVisZoom = True
      edTb_acontromag.NTSForzaVisZoom = True
      edTb_acontrribo.NTSForzaVisZoom = True
      edTb_acontrriin.NTSForzaVisZoom = True
      edTb_acontrriac.NTSForzaVisZoom = True
      edTb_acontrimba.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------------------------------------------
      edTb_tipobfrkj.NTSForzaVisZoom = True
      edTb_tipobfrkl.NTSForzaVisZoom = True
      edTb_tipobfrkm.NTSForzaVisZoom = True
      edTb_tipobfrkt.NTSForzaVisZoom = True
      edTb_tipobfrko.NTSForzaVisZoom = True
      edTb_tipobfrkh.NTSForzaVisZoom = True
      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      edTb_caurcft.NTSSetParamZoom("ZOOMTABCAUC")
      edTb_caurcna.NTSSetParamZoom("ZOOMTABCAUC")
      edTb_caupag.NTSSetParamZoom("ZOOMTABCAUC")
      edTb_caustomac.NTSSetParamZoom("ZOOMTABCAUC")
      edTb_aivainc.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_aiva15.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_causcapro.NTSSetParamZoom("ZOOMTABCAUM")
      edTb_caurcftcee.NTSSetParamZoom("ZOOMTABCAUC")
      edTb_caurcnacee.NTSSetParamZoom("ZOOMTABCAUC")
      '-------------------------------------------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------------------------------------------
      edTb_atipobf.NTSSetParamZoom("ZOOMTABTPBF")
      edTb_acontro.NTSSetParamZoom("ZOOMTABCOVE")
      edTb_amagazz.NTSSetParamZoom("ZOOMTABMAGA")
      edTb_acaumag.NTSSetParamZoom("ZOOMTABCAUM")
      edTb_ccdaconv.NTSSetParamZoom("ZOOMTABCOVE")
      edTb_ccdfconv.NTSSetParamZoom("ZOOMTABCOVE")
      '-------------------------------------------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------------------------------------------
      edTb_aconcauz.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconcas.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconabat.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconabpa.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconomag.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconribo.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconriin.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_aconriac.NTSSetParamZoom("ZOOMANAGRAS")
      edTb_acontimba.NTSSetParamZoom("ZOOMANAGRAS")
      '-------------------------------------------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------------------------------------------
      edTb_acontrcauz.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrcas.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrabat.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrabpa.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontromag.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrribo.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrriin.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrriac.NTSSetParamZoom("ZOOMTABCOVG")
      edTb_acontrimba.NTSSetParamZoom("ZOOMTABCOVG")

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
      lbXx_caurcft.NTSDbField = "tabpeac.xx_caurcft"
      edTb_caurcft.NTSDbField = "tabpeac.tb_caurcft"
      lbXx_caurcna.NTSDbField = "tabpeac.xx_caurcna"
      edTb_caurcna.NTSDbField = "tabpeac.tb_caurcna"
      lbXx_caupag.NTSDbField = "tabpeac.xx_caupag"
      edTb_caupag.NTSDbField = "tabpeac.tb_caupag"
      lbXx_caustomac.NTSDbField = "tabpeac.xx_caustomac"
      edTb_caustomac.NTSDbField = "tabpeac.tb_caustomac"
      lbXx_aivainc.NTSDbField = "tabpeac.xx_aivainc"
      edTb_aivainc.NTSDbField = "tabpeac.tb_aivainc"
      edTb_aivainc2.NTSDbField = "tabpeac.tb_aivainc2"
      lbXx_aivainc2.NTSDbField = "tabpeac.xx_aivainc2"
      edTb_aivainc3.NTSDbField = "tabpeac.tb_aivainc3"
      lbXx_aivainc3.NTSDbField = "tabpeac.xx_aivainc3"
      edTb_adatfinivainc3.NTSDbField = "tabpeac.tb_adatfinivainc3"
      edTb_adatfinivainc2.NTSDbField = "tabpeac.tb_adatfinivainc2"
      lbXx_aiva15.NTSDbField = "tabpeac.xx_aiva15"
      edTb_aiva15.NTSDbField = "tabpeac.tb_aiva15"
      lbXx_causcapro.NTSDbField = "tabpeac.xx_causcapro"
      edTb_causcapro.NTSDbField = "tabpeac.tb_causcapro"
      lbXx_caurcftcee.NTSDbField = "tabpeac.xx_caurcftcee"
      edTb_caurcftcee.NTSDbField = "tabpeac.tb_caurcftcee"
      lbXx_caurcnacee.NTSDbField = "tabpeac.xx_caurcnacee"
      edTb_caurcnacee.NTSDbField = "tabpeac.tb_caurcnacee"
      edTb_caurcftextrc.NTSDbField = "tabpeac.tb_caurcftextrc"
      lbXx_caurcftextrc.NTSDbField = "tabpeac.xx_caurcftextrc"
      edTb_caurcnextrc.NTSDbField = "tabpeac.tb_caurcnextrc"
      lbXx_caurcnextrc.NTSDbField = "tabpeac.xx_caurcnextrc"
      edTb_caurcfrc.NTSDbField = "tabpeac.tb_caurcfrc"
      lbXx_caurcfrc.NTSDbField = "tabpeac.xx_caurcfrc"
      edTb_caurcarc.NTSDbField = "tabpeac.tb_caurcarc"
      lbXx_caurcarc.NTSDbField = "tabpeac.xx_caurcarc"
      edTb_caurcfs.NTSDbField = "tabpeac.tb_caurcfs"
      lbXx_caurcfs.NTSDbField = "tabpeac.xx_caurcfs"
      '-------------------------------------------------
      '--- Secondo Tab
      '-------------------------------------------------
      lbXx_atipobf.NTSDbField = "tabpeac.xx_atipobf"
      edTb_atipobf.NTSDbField = "tabpeac.tb_atipobf"
      lbXx_acontro.NTSDbField = "tabpeac.xx_acontro"
      edTb_acontro.NTSDbField = "tabpeac.tb_acontro"
      lbXx_amagazz.NTSDbField = "tabpeac.xx_amagazz"
      edTb_amagazz.NTSDbField = "tabpeac.tb_amagazz"
      lbXx_acaumag.NTSDbField = "tabpeac.xx_acaumag"
      edTb_acaumag.NTSDbField = "tabpeac.tb_acaumag"
      lbXx_ccdaconv.NTSDbField = "tabpeac.xx_ccdaconv"
      edTb_ccdaconv.NTSDbField = "tabpeac.tb_ccdaconv"
      lbXx_ccdfconv.NTSDbField = "tabpeac.xx_ccdfconv"
      edTb_ccdfconv.NTSDbField = "tabpeac.tb_ccdfconv"
      edTb_alistin.NTSDbField = "tabpeac.tb_alistin"
      edTb_listcstd.NTSDbField = "tabpeac.tb_listcstd"
      ckTb_ascorpo.NTSText.NTSDbField = "tabpeac.tb_ascorpo"
      '-------------------------------------------------
      '--- Terzo Tab
      '-------------------------------------------------
      lbXx_aconcauz.NTSDbField = "tabpeac.xx_aconcauz"
      edTb_aconcauz.NTSDbField = "tabpeac.tb_aconcauz"
      lbXx_aconcas.NTSDbField = "tabpeac.xx_aconcas"
      edTb_aconcas.NTSDbField = "tabpeac.tb_aconcas"
      lbXx_aconabat.NTSDbField = "tabpeac.xx_aconabat"
      edTb_aconabat.NTSDbField = "tabpeac.tb_aconabat"
      lbXx_aconabpa.NTSDbField = "tabpeac.xx_aconabpa"
      edTb_aconabpa.NTSDbField = "tabpeac.tb_aconabpa"
      lbXx_aconomag.NTSDbField = "tabpeac.xx_aconomag"
      edTb_aconomag.NTSDbField = "tabpeac.tb_aconomag"
      lbXx_aconribo.NTSDbField = "tabpeac.xx_aconribo"
      edTb_aconribo.NTSDbField = "tabpeac.tb_aconribo"
      lbXx_aconriin.NTSDbField = "tabpeac.xx_aconriin"
      edTb_aconriin.NTSDbField = "tabpeac.tb_aconriin"
      lbXx_aconriac.NTSDbField = "tabpeac.xx_aconriac"
      edTb_aconriac.NTSDbField = "tabpeac.tb_aconriac"
      lbXx_acontimba.NTSDbField = "tabpeac.xx_acontimba"
      edTb_acontimba.NTSDbField = "tabpeac.tb_acontimba"
      '-------------------------------------------------
      '--- Quarto Tab
      '-------------------------------------------------
      lbXx_acontrcauz.NTSDbField = "tabpeac.xx_acontrcauz"
      edTb_acontrcauz.NTSDbField = "tabpeac.tb_acontrcauz"
      lbXx_acontrcas.NTSDbField = "tabpeac.xx_acontrcas"
      edTb_acontrcas.NTSDbField = "tabpeac.tb_acontrcas"
      lbXx_acontrabat.NTSDbField = "tabpeac.xx_acontrabat"
      edTb_acontrabat.NTSDbField = "tabpeac.tb_acontrabat"
      lbXx_acontrabpa.NTSDbField = "tabpeac.xx_acontrabpa"
      edTb_acontrabpa.NTSDbField = "tabpeac.tb_acontrabpa"
      lbXx_acontromag.NTSDbField = "tabpeac.xx_acontromag"
      edTb_acontromag.NTSDbField = "tabpeac.tb_acontromag"
      lbXx_acontrribo.NTSDbField = "tabpeac.xx_acontrribo"
      edTb_acontrribo.NTSDbField = "tabpeac.tb_acontrribo"
      lbXx_acontrriin.NTSDbField = "tabpeac.xx_acontrriin"
      edTb_acontrriin.NTSDbField = "tabpeac.tb_acontrriin"
      lbXx_acontrriac.NTSDbField = "tabpeac.xx_acontrriac"
      edTb_acontrriac.NTSDbField = "tabpeac.tb_acontrriac"
      lbXx_acontrimba.NTSDbField = "tabpeac.xx_acontrimba"
      edTb_acontrimba.NTSDbField = "tabpeac.tb_acontrimba"
      '-------------------------------------------------
      '--- Quinto Tab
      '-------------------------------------------------
      lbXx_tipobfrkj.NTSDbField = "tabpeac.xx_tipobfrkj"
      edTb_tipobfrkj.NTSDbField = "tabpeac.tb_tipobfrkj"
      lbXx_tipobfrkl.NTSDbField = "tabpeac.xx_tipobfrkl"
      edTb_tipobfrkl.NTSDbField = "tabpeac.tb_tipobfrkl"
      lbXx_tipobfrkm.NTSDbField = "tabpeac.xx_tipobfrkm"
      edTb_tipobfrkm.NTSDbField = "tabpeac.tb_tipobfrkm"
      lbXx_tipobfrkt.NTSDbField = "tabpeac.xx_tipobfrkt"
      edTb_tipobfrkt.NTSDbField = "tabpeac.tb_tipobfrkt"
      lbXx_tipobfrko.NTSDbField = "tabpeac.xx_tipobfrko"
      edTb_tipobfrko.NTSDbField = "tabpeac.tb_tipobfrko"
      lbXx_tipobfrkh.NTSDbField = "tabpeac.xx_tipobfrkh"
      edTb_tipobfrkh.NTSDbField = "tabpeac.tb_tipobfrkh"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcPeac, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRMMGPEAC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------------------------------------
      InitControls()
      '-------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBinding
      '-------------------------------------------------------------------------------------
      If Not oClePeac.File_Apri(DittaCorrente, dsPeac) Then Me.Close()
      dcPeac.DataSource = dsPeac.Tables("TABPEAC")
      dsPeac.AcceptChanges()
      '-------------------------------------------------------------------------------------
      '--- Collego il BindingSource ai vari controlli 
      '-------------------------------------------------------------------------------------
      Bindcontrols()
      '-------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--- e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      '-------------------------------------------------------------------------------------
      GctlSetRoules()

      If dsPeac.Tables("TABPEAC").Rows.Count = 0 Then
        oClePeac.RecordNuovo()
        dcPeac.MoveLast()
        Me.GctlApplicaDefaultValue()
      End If

      '-------------------------------------------------------------------------------------
      dcPeac.ResetBindings(False)
      dcPeac.MoveFirst()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGPEAC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMPEAC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcPeac.Dispose()
      dsPeac.Dispose()
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
      If oClePeac.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128437381259155971, "Salvo la personalizzazione?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oClePeac.File_Aggiorna(False) Then Return False
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then oClePeac.RecordRipristina(dcPeac.Position, dcPeac.Filter)
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
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128437381474949393, "Ripristinare le modifiche apportate alla personalizzazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oClePeac.RecordRipristina(dcPeac.Position, dcPeac.Filter)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTipiBF_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTipiBF.ItemClick
    Dim oPar As CLE__CLDP = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      oPar = New CLE__CLDP
      oMenu.RunChild("NTSInformatica", "FRMVERKBF", "", DittaCorrente, "", "BNVERKBF", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try

  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try

      oParam.strCodPdc = oClePeac.dttAnaz.Rows(0)!tb_azcodpcon.ToString
      oParam.nEscomp = NTSCInt(oClePeac.dttAnaz.Rows(0)!tb_escomp.ToString)

      '-------------------------------------------------------------------------------------
      '--- Primo Tab
      '-------------------------------------------------------------------------------------
      If edTb_caurcft.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurcft.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caurcft.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurcft.Text Then edTb_caurcft.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurcna.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurcna.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caurcna.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurcna.Text Then edTb_caurcna.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caupag.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caupag.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caupag.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caupag.Text Then edTb_caupag.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caustomac.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caustomac.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caustomac.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caustomac.Text Then edTb_caustomac.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_causcapro.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_causcapro.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_causcapro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_causcapro.Text Then edTb_causcapro.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurcftcee.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurcftcee.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caurcftcee.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurcftcee.Text Then edTb_caurcftcee.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_caurcnacee.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_caurcnacee.Text, oParam)
        oParam.bVisGriglia = True
        NTSZOOM.strIn = edTb_caurcnacee.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_caurcnacee.Text Then edTb_caurcnacee.NTSTextDB = NTSZOOM.strIn
        '-------------------------------------------------------------------------------------
        '--- Secondo Tab
        '-------------------------------------------------------------------------------------

        '-------------------------------------------------------------------------------------
        '--- Terzo Tab
        '-------------------------------------------------------------------------------------
      ElseIf edTb_aconcauz.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconcauz.Text, oParam)
        NTSZOOM.strIn = edTb_aconcauz.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconcauz.Text Then edTb_aconcauz.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconcas.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconcas.Text, oParam)
        NTSZOOM.strIn = edTb_aconcas.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconcas.Text Then edTb_aconcas.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconabat.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconabat.Text, oParam)
        NTSZOOM.strIn = edTb_aconabat.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconabat.Text Then edTb_aconabat.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconabpa.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconabpa.Text, oParam)
        NTSZOOM.strIn = edTb_aconabpa.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconabpa.Text Then edTb_aconabpa.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconomag.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconomag.Text, oParam)
        NTSZOOM.strIn = edTb_aconomag.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconomag.Text Then edTb_aconomag.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconribo.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconribo.Text, oParam)
        NTSZOOM.strIn = edTb_aconribo.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconribo.Text Then edTb_aconribo.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconriin.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconriin.Text, oParam)
        NTSZOOM.strIn = edTb_aconriin.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconriin.Text Then edTb_aconriin.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_aconriac.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_aconriac.Text, oParam)
        NTSZOOM.strIn = edTb_aconriac.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_aconriac.Text Then edTb_aconriac.NTSTextDB = NTSZOOM.strIn
      ElseIf edTb_acontimba.Focused Then
        '--------------------------------------------
        SetFastZoom(edTb_acontimba.Text, oParam)
        NTSZOOM.strIn = edTb_acontimba.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "S"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_acontimba.Text Then edTb_acontimba.NTSTextDB = NTSZOOM.strIn
        '-------------------------------------------------------------------------------------
        '--- Quarto Tab
        '-------------------------------------------------------------------------------------

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
