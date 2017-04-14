Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGELAR
  Public oCleElar As CLEMGELAR
  Public dsElar As DataSet = Nothing
  Public dcElar As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public strProntoMess As String = ""

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents pnElar As NTSInformatica.NTSPanel
  Public WithEvents fmTipoElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents opGenericoDaFornitori As NTSInformatica.NTSRadioButton
  Public WithEvents opGenericoDaClienti As NTSInformatica.NTSRadioButton
  Public WithEvents opFornitori As NTSInformatica.NTSRadioButton
  Public WithEvents opClienti As NTSInformatica.NTSRadioButton
  Public WithEvents opGenerico As NTSInformatica.NTSRadioButton
  Public WithEvents cmdArticoliDaDoc As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Conto As NTSInformatica.NTSLabel
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
  Public WithEvents edDatagg As NTSInformatica.NTSTextBoxData
  Public WithEvents fmListinoElabora As NTSInformatica.NTSGroupBox
  Public WithEvents fmListinoPartenza As NTSInformatica.NTSGroupBox
  Public WithEvents edListpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListpar As NTSInformatica.NTSLabel
  Public WithEvents fmTipoVariazione As NTSInformatica.NTSGroupBox
  Public WithEvents opScorporaIva As NTSInformatica.NTSRadioButton
  Public WithEvents opRicarico As NTSInformatica.NTSRadioButton
  Public WithEvents opValore As NTSInformatica.NTSRadioButton
  Public WithEvents opPercentuale As NTSInformatica.NTSRadioButton
  Public WithEvents lbAppMis As NTSInformatica.NTSLabel
  Public WithEvents cbAppMis As NTSInformatica.NTSComboBox
  Public WithEvents edValore As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValore As NTSInformatica.NTSLabel
  Public WithEvents edRicar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRicar1 As NTSInformatica.NTSLabel
  Public WithEvents edArrotond As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArrotond As NTSInformatica.NTSLabel
  Public WithEvents lbRicar2 As NTSInformatica.NTSLabel
  Public WithEvents edRicar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents ckElaborazione As NTSInformatica.NTSCheckBox
  Public WithEvents ckAggiungiIVA As NTSInformatica.NTSCheckBox
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodlavo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodlavo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Codlavo As NTSInformatica.NTSLabel
  Public WithEvents edCambiopar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCambiopar As NTSInformatica.NTSLabel
  Public WithEvents edValpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValpar As NTSInformatica.NTSLabel
  Public WithEvents opUltimaFase As NTSInformatica.NTSRadioButton
  Public WithEvents lbListpar3 As NTSInformatica.NTSLabel
  Public WithEvents lbListpar2 As NTSInformatica.NTSLabel
  Public WithEvents lbListpar1 As NTSInformatica.NTSLabel
  Public WithEvents opQualsiasiFase As NTSInformatica.NTSRadioButton
  Public WithEvents opSoloFase As NTSInformatica.NTSRadioButton
  Public WithEvents edCambioelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCambioelab As NTSInformatica.NTSLabel
  Public WithEvents edValelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValelab As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Valelab As NTSInformatica.NTSLabel
  Public WithEvents edListelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListelab As NTSInformatica.NTSLabel
  Public WithEvents edCodlavOut As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodlavOut As NTSInformatica.NTSLabel
  Public WithEvents lbXx_CodlavOut As NTSInformatica.NTSLabel
  Public WithEvents lbScadprom As NTSInformatica.NTSLabel
  Public WithEvents edCodtpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodtpro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Codtpro As NTSInformatica.NTSLabel
  Public WithEvents ckAggiornalistini As NTSInformatica.NTSCheckBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents edScadprom As NTSInformatica.NTSTextBoxData
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents ckVisRisultati As NTSInformatica.NTSCheckBox
  Public WithEvents ckNetto As NTSInformatica.NTSCheckBox
  Public WithEvents fmPromo As NTSInformatica.NTSGroupBox
  Public WithEvents opPromoSpecifica As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttePromo As NTSInformatica.NTSRadioButton
  Public WithEvents opNoPromo As NTSInformatica.NTSRadioButton
  Public WithEvents lbDesPromo As NTSInformatica.NTSLabel
  Public WithEvents edPromo As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipoElaborazione As NTSInformatica.NTSComboBox
  Private components As System.ComponentModel.IContainer
  Public WithEvents edCodlsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodlsar As NTSInformatica.NTSLabel
  Public WithEvents lbCodlsar As NTSInformatica.NTSLabel
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents lbDescoddest As NTSInformatica.NTSLabel
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDataListinoRiferim As NTSInformatica.NTSLabel
  Public WithEvents edDataListinoRiferim As NTSInformatica.NTSTextBoxData
  Public WithEvents ckScontiDaCondComm As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_Valpar As NTSInformatica.NTSLabel
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
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGELAR", "BEMGELAR", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleElar = CType(objTmp, CLEMGELAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGELAR", strRemoteServer, strRemotePort)
    AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleElar.Init(oApp, NTSScript, oMenu.oCleComm, "tabanaz", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGELAR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnElar = New NTSInformatica.NTSPanel
    Me.ckScontiDaCondComm = New NTSInformatica.NTSCheckBox
    Me.lbDataListinoRiferim = New NTSInformatica.NTSLabel
    Me.edDataListinoRiferim = New NTSInformatica.NTSTextBoxData
    Me.lbDescoddest = New NTSInformatica.NTSLabel
    Me.edCodlsar = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddest = New NTSInformatica.NTSLabel
    Me.edCoddest = New NTSInformatica.NTSTextBoxNum
    Me.lbDescodlsar = New NTSInformatica.NTSLabel
    Me.lbCodlsar = New NTSInformatica.NTSLabel
    Me.ckNetto = New NTSInformatica.NTSCheckBox
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.ckVisRisultati = New NTSInformatica.NTSCheckBox
    Me.fmTipoVariazione = New NTSInformatica.NTSGroupBox
    Me.opScorporaIva = New NTSInformatica.NTSRadioButton
    Me.opRicarico = New NTSInformatica.NTSRadioButton
    Me.opValore = New NTSInformatica.NTSRadioButton
    Me.opPercentuale = New NTSInformatica.NTSRadioButton
    Me.lbArrotond = New NTSInformatica.NTSLabel
    Me.fmElaborazione = New NTSInformatica.NTSGroupBox
    Me.ckElaborazione = New NTSInformatica.NTSCheckBox
    Me.edArrotond = New NTSInformatica.NTSTextBoxNum
    Me.ckAggiungiIVA = New NTSInformatica.NTSCheckBox
    Me.lbRicar1 = New NTSInformatica.NTSLabel
    Me.lbRicar2 = New NTSInformatica.NTSLabel
    Me.edRicar1 = New NTSInformatica.NTSTextBoxNum
    Me.edRicar2 = New NTSInformatica.NTSTextBoxNum
    Me.lbValore = New NTSInformatica.NTSLabel
    Me.lbAppMis = New NTSInformatica.NTSLabel
    Me.edValore = New NTSInformatica.NTSTextBoxNum
    Me.cbAppMis = New NTSInformatica.NTSComboBox
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.fmListinoElabora = New NTSInformatica.NTSGroupBox
    Me.edScadprom = New NTSInformatica.NTSTextBoxData
    Me.ckAggiornalistini = New NTSInformatica.NTSCheckBox
    Me.edCodlavOut = New NTSInformatica.NTSTextBoxNum
    Me.lbCodlavOut = New NTSInformatica.NTSLabel
    Me.lbXx_CodlavOut = New NTSInformatica.NTSLabel
    Me.lbScadprom = New NTSInformatica.NTSLabel
    Me.edCodtpro = New NTSInformatica.NTSTextBoxNum
    Me.lbCodtpro = New NTSInformatica.NTSLabel
    Me.lbXx_Codtpro = New NTSInformatica.NTSLabel
    Me.edCambioelab = New NTSInformatica.NTSTextBoxNum
    Me.lbCambioelab = New NTSInformatica.NTSLabel
    Me.edValelab = New NTSInformatica.NTSTextBoxNum
    Me.lbValelab = New NTSInformatica.NTSLabel
    Me.lbXx_Valelab = New NTSInformatica.NTSLabel
    Me.edListelab = New NTSInformatica.NTSTextBoxNum
    Me.lbListelab = New NTSInformatica.NTSLabel
    Me.fmListinoPartenza = New NTSInformatica.NTSGroupBox
    Me.fmPromo = New NTSInformatica.NTSGroupBox
    Me.lbDesPromo = New NTSInformatica.NTSLabel
    Me.edPromo = New NTSInformatica.NTSTextBoxNum
    Me.opPromoSpecifica = New NTSInformatica.NTSRadioButton
    Me.opTuttePromo = New NTSInformatica.NTSRadioButton
    Me.opNoPromo = New NTSInformatica.NTSRadioButton
    Me.opQualsiasiFase = New NTSInformatica.NTSRadioButton
    Me.opSoloFase = New NTSInformatica.NTSRadioButton
    Me.opUltimaFase = New NTSInformatica.NTSRadioButton
    Me.lbListpar3 = New NTSInformatica.NTSLabel
    Me.lbListpar2 = New NTSInformatica.NTSLabel
    Me.lbListpar1 = New NTSInformatica.NTSLabel
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edCodlavo = New NTSInformatica.NTSTextBoxNum
    Me.lbCodlavo = New NTSInformatica.NTSLabel
    Me.lbXx_Codlavo = New NTSInformatica.NTSLabel
    Me.edCambiopar = New NTSInformatica.NTSTextBoxNum
    Me.lbCambiopar = New NTSInformatica.NTSLabel
    Me.edValpar = New NTSInformatica.NTSTextBoxNum
    Me.lbValpar = New NTSInformatica.NTSLabel
    Me.lbXx_Valpar = New NTSInformatica.NTSLabel
    Me.edListpar = New NTSInformatica.NTSTextBoxNum
    Me.lbListpar = New NTSInformatica.NTSLabel
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.edDatagg = New NTSInformatica.NTSTextBoxData
    Me.cmdArticoliDaDoc = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbXx_Conto = New NTSInformatica.NTSLabel
    Me.fmTipoElaborazione = New NTSInformatica.NTSGroupBox
    Me.cbTipoElaborazione = New NTSInformatica.NTSComboBox
    Me.opGenericoDaFornitori = New NTSInformatica.NTSRadioButton
    Me.opGenericoDaClienti = New NTSInformatica.NTSRadioButton
    Me.opFornitori = New NTSInformatica.NTSRadioButton
    Me.opClienti = New NTSInformatica.NTSRadioButton
    Me.opGenerico = New NTSInformatica.NTSRadioButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElar.SuspendLayout()
    CType(Me.ckScontiDaCondComm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataListinoRiferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.ckVisRisultati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoVariazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoVariazione.SuspendLayout()
    CType(Me.opScorporaIva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRicarico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPercentuale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmElaborazione.SuspendLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiungiIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRicar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRicar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAppMis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListinoElabora, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListinoElabora.SuspendLayout()
    CType(Me.edScadprom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiornalistini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlavOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCambioelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListinoPartenza, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListinoPartenza.SuspendLayout()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPromo.SuspendLayout()
    CType(Me.edPromo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPromoSpecifica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTuttePromo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opNoPromo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opQualsiasiFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSoloFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opUltimaFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlavo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCambiopar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoElaborazione.SuspendLayout()
    CType(Me.cbTipoElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opGenericoDaFornitori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opGenericoDaClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opGenerico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 6
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnElar
    '
    Me.pnElar.AllowDrop = True
    Me.pnElar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElar.Appearance.Options.UseBackColor = True
    Me.pnElar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElar.Controls.Add(Me.ckScontiDaCondComm)
    Me.pnElar.Controls.Add(Me.lbDataListinoRiferim)
    Me.pnElar.Controls.Add(Me.edDataListinoRiferim)
    Me.pnElar.Controls.Add(Me.lbDescoddest)
    Me.pnElar.Controls.Add(Me.edCodlsar)
    Me.pnElar.Controls.Add(Me.lbCoddest)
    Me.pnElar.Controls.Add(Me.edCoddest)
    Me.pnElar.Controls.Add(Me.lbDescodlsar)
    Me.pnElar.Controls.Add(Me.lbCodlsar)
    Me.pnElar.Controls.Add(Me.ckNetto)
    Me.pnElar.Controls.Add(Me.pnRight)
    Me.pnElar.Controls.Add(Me.lbStatus)
    Me.pnElar.Controls.Add(Me.fmListinoElabora)
    Me.pnElar.Controls.Add(Me.fmListinoPartenza)
    Me.pnElar.Controls.Add(Me.lbDatagg)
    Me.pnElar.Controls.Add(Me.edDatagg)
    Me.pnElar.Controls.Add(Me.cmdArticoliDaDoc)
    Me.pnElar.Controls.Add(Me.cmdSeleziona)
    Me.pnElar.Controls.Add(Me.edConto)
    Me.pnElar.Controls.Add(Me.lbConto)
    Me.pnElar.Controls.Add(Me.lbXx_Conto)
    Me.pnElar.Controls.Add(Me.fmTipoElaborazione)
    Me.pnElar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnElar.Location = New System.Drawing.Point(0, 30)
    Me.pnElar.Name = "pnElar"
    Me.pnElar.NTSActiveTrasparency = True
    Me.pnElar.Size = New System.Drawing.Size(748, 559)
    Me.pnElar.TabIndex = 0
    Me.pnElar.Text = "NtsPanel1"
    '
    'ckScontiDaCondComm
    '
    Me.ckScontiDaCondComm.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckScontiDaCondComm.Location = New System.Drawing.Point(227, 84)
    Me.ckScontiDaCondComm.Name = "ckScontiDaCondComm"
    Me.ckScontiDaCondComm.NTSCheckValue = "S"
    Me.ckScontiDaCondComm.NTSUnCheckValue = "N"
    Me.ckScontiDaCondComm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScontiDaCondComm.Properties.Appearance.Options.UseBackColor = True
    Me.ckScontiDaCondComm.Properties.AutoHeight = False
    Me.ckScontiDaCondComm.Properties.Caption = "Applico Priorità Sconti Clienti/Fornitori "
    Me.ckScontiDaCondComm.Size = New System.Drawing.Size(206, 19)
    Me.ckScontiDaCondComm.TabIndex = 630
    '
    'lbDataListinoRiferim
    '
    Me.lbDataListinoRiferim.AutoSize = True
    Me.lbDataListinoRiferim.BackColor = System.Drawing.Color.Transparent
    Me.lbDataListinoRiferim.Location = New System.Drawing.Point(466, 107)
    Me.lbDataListinoRiferim.Name = "lbDataListinoRiferim"
    Me.lbDataListinoRiferim.NTSDbField = ""
    Me.lbDataListinoRiferim.Size = New System.Drawing.Size(126, 13)
    Me.lbDataListinoRiferim.TabIndex = 629
    Me.lbDataListinoRiferim.Text = "Data listino di riferimento"
    Me.lbDataListinoRiferim.Tooltip = ""
    Me.lbDataListinoRiferim.UseMnemonic = False
    '
    'edDataListinoRiferim
    '
    Me.edDataListinoRiferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataListinoRiferim.Location = New System.Drawing.Point(654, 104)
    Me.edDataListinoRiferim.Name = "edDataListinoRiferim"
    Me.edDataListinoRiferim.NTSDbField = ""
    Me.edDataListinoRiferim.NTSForzaVisZoom = False
    Me.edDataListinoRiferim.NTSOldValue = ""
    Me.edDataListinoRiferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataListinoRiferim.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataListinoRiferim.Properties.AutoHeight = False
    Me.edDataListinoRiferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataListinoRiferim.Properties.MaxLength = 65536
    Me.edDataListinoRiferim.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataListinoRiferim.Size = New System.Drawing.Size(85, 20)
    Me.edDataListinoRiferim.TabIndex = 628
    '
    'lbDescoddest
    '
    Me.lbDescoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbDescoddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescoddest.Location = New System.Drawing.Point(610, 60)
    Me.lbDescoddest.Name = "lbDescoddest"
    Me.lbDescoddest.NTSDbField = ""
    Me.lbDescoddest.Size = New System.Drawing.Size(129, 20)
    Me.lbDescoddest.TabIndex = 627
    Me.lbDescoddest.Tooltip = ""
    Me.lbDescoddest.UseMnemonic = False
    '
    'edCodlsar
    '
    Me.edCodlsar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlsar.EditValue = "0"
    Me.edCodlsar.Location = New System.Drawing.Point(117, 106)
    Me.edCodlsar.Name = "edCodlsar"
    Me.edCodlsar.NTSDbField = ""
    Me.edCodlsar.NTSFormat = "0"
    Me.edCodlsar.NTSForzaVisZoom = False
    Me.edCodlsar.NTSOldValue = ""
    Me.edCodlsar.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlsar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlsar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlsar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlsar.Properties.AutoHeight = False
    Me.edCodlsar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlsar.Properties.MaxLength = 65536
    Me.edCodlsar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlsar.Size = New System.Drawing.Size(67, 20)
    Me.edCodlsar.TabIndex = 153
    '
    'lbCoddest
    '
    Me.lbCoddest.AutoSize = True
    Me.lbCoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddest.Location = New System.Drawing.Point(466, 63)
    Me.lbCoddest.Name = "lbCoddest"
    Me.lbCoddest.NTSDbField = ""
    Me.lbCoddest.Size = New System.Drawing.Size(87, 13)
    Me.lbCoddest.TabIndex = 626
    Me.lbCoddest.Text = "Destin.(-1:tutte)"
    Me.lbCoddest.Tooltip = ""
    Me.lbCoddest.UseMnemonic = False
    '
    'edCoddest
    '
    Me.edCoddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest.EditValue = "-1"
    Me.edCoddest.Location = New System.Drawing.Point(555, 60)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSFormat = "0"
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = "-1"
    Me.edCoddest.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCoddest.Properties.Appearance.Options.UseBackColor = True
    Me.edCoddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.AutoHeight = False
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(52, 20)
    Me.edCoddest.TabIndex = 625
    '
    'lbDescodlsar
    '
    Me.lbDescodlsar.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsar.Location = New System.Drawing.Point(189, 106)
    Me.lbDescodlsar.Name = "lbDescodlsar"
    Me.lbDescodlsar.NTSDbField = ""
    Me.lbDescodlsar.Size = New System.Drawing.Size(271, 20)
    Me.lbDescodlsar.TabIndex = 152
    Me.lbDescodlsar.Tooltip = ""
    Me.lbDescodlsar.UseMnemonic = False
    '
    'lbCodlsar
    '
    Me.lbCodlsar.AutoSize = True
    Me.lbCodlsar.BackColor = System.Drawing.Color.Transparent
    Me.lbCodlsar.Location = New System.Drawing.Point(15, 109)
    Me.lbCodlsar.Name = "lbCodlsar"
    Me.lbCodlsar.NTSDbField = ""
    Me.lbCodlsar.Size = New System.Drawing.Size(86, 13)
    Me.lbCodlsar.TabIndex = 151
    Me.lbCodlsar.Text = "Lista selezionata"
    Me.lbCodlsar.Tooltip = ""
    Me.lbCodlsar.UseMnemonic = False
    '
    'ckNetto
    '
    Me.ckNetto.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckNetto.Location = New System.Drawing.Point(15, 84)
    Me.ckNetto.Name = "ckNetto"
    Me.ckNetto.NTSCheckValue = "S"
    Me.ckNetto.NTSUnCheckValue = "N"
    Me.ckNetto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNetto.Properties.Appearance.Options.UseBackColor = True
    Me.ckNetto.Properties.AutoHeight = False
    Me.ckNetto.Properties.Caption = "Netto sconti specifici Clienti/Fornitori"
    Me.ckNetto.Size = New System.Drawing.Size(206, 19)
    Me.ckNetto.TabIndex = 150
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.ckVisRisultati)
    Me.pnRight.Controls.Add(Me.fmTipoVariazione)
    Me.pnRight.Controls.Add(Me.lbArrotond)
    Me.pnRight.Controls.Add(Me.fmElaborazione)
    Me.pnRight.Controls.Add(Me.edArrotond)
    Me.pnRight.Controls.Add(Me.ckAggiungiIVA)
    Me.pnRight.Controls.Add(Me.lbRicar1)
    Me.pnRight.Controls.Add(Me.lbRicar2)
    Me.pnRight.Controls.Add(Me.edRicar1)
    Me.pnRight.Controls.Add(Me.edRicar2)
    Me.pnRight.Controls.Add(Me.lbValore)
    Me.pnRight.Controls.Add(Me.lbAppMis)
    Me.pnRight.Controls.Add(Me.edValore)
    Me.pnRight.Controls.Add(Me.cbAppMis)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Location = New System.Drawing.Point(466, 131)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(281, 394)
    Me.pnRight.TabIndex = 149
    Me.pnRight.Text = "NtsPanel1"
    '
    'ckVisRisultati
    '
    Me.ckVisRisultati.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisRisultati.Location = New System.Drawing.Point(8, 217)
    Me.ckVisRisultati.Name = "ckVisRisultati"
    Me.ckVisRisultati.NTSCheckValue = "S"
    Me.ckVisRisultati.NTSUnCheckValue = "N"
    Me.ckVisRisultati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisRisultati.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisRisultati.Properties.AutoHeight = False
    Me.ckVisRisultati.Properties.Caption = "Visualizza risultati"
    Me.ckVisRisultati.Size = New System.Drawing.Size(108, 19)
    Me.ckVisRisultati.TabIndex = 148
    '
    'fmTipoVariazione
    '
    Me.fmTipoVariazione.AllowDrop = True
    Me.fmTipoVariazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoVariazione.Appearance.Options.UseBackColor = True
    Me.fmTipoVariazione.Controls.Add(Me.opScorporaIva)
    Me.fmTipoVariazione.Controls.Add(Me.opRicarico)
    Me.fmTipoVariazione.Controls.Add(Me.opValore)
    Me.fmTipoVariazione.Controls.Add(Me.opPercentuale)
    Me.fmTipoVariazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoVariazione.Location = New System.Drawing.Point(3, 0)
    Me.fmTipoVariazione.Name = "fmTipoVariazione"
    Me.fmTipoVariazione.Size = New System.Drawing.Size(179, 103)
    Me.fmTipoVariazione.TabIndex = 135
    Me.fmTipoVariazione.Text = "Tipo Variazione : "
    '
    'opScorporaIva
    '
    Me.opScorporaIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.opScorporaIva.Location = New System.Drawing.Point(5, 80)
    Me.opScorporaIva.Name = "opScorporaIva"
    Me.opScorporaIva.NTSCheckValue = "S"
    Me.opScorporaIva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opScorporaIva.Properties.Appearance.Options.UseBackColor = True
    Me.opScorporaIva.Properties.AutoHeight = False
    Me.opScorporaIva.Properties.Caption = "Scorpora Iva"
    Me.opScorporaIva.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opScorporaIva.Size = New System.Drawing.Size(86, 19)
    Me.opScorporaIva.TabIndex = 7
    '
    'opRicarico
    '
    Me.opRicarico.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opRicarico.Location = New System.Drawing.Point(5, 60)
    Me.opRicarico.Name = "opRicarico"
    Me.opRicarico.NTSCheckValue = "S"
    Me.opRicarico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRicarico.Properties.Appearance.Options.UseBackColor = True
    Me.opRicarico.Properties.AutoHeight = False
    Me.opRicarico.Properties.Caption = "Ri&carico/Margine"
    Me.opRicarico.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRicarico.Size = New System.Drawing.Size(101, 19)
    Me.opRicarico.TabIndex = 6
    '
    'opValore
    '
    Me.opValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValore.Location = New System.Drawing.Point(5, 40)
    Me.opValore.Name = "opValore"
    Me.opValore.NTSCheckValue = "S"
    Me.opValore.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValore.Properties.Appearance.Options.UseBackColor = True
    Me.opValore.Properties.AutoHeight = False
    Me.opValore.Properties.Caption = "&Valore"
    Me.opValore.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValore.Size = New System.Drawing.Size(56, 19)
    Me.opValore.TabIndex = 5
    '
    'opPercentuale
    '
    Me.opPercentuale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPercentuale.EditValue = True
    Me.opPercentuale.Location = New System.Drawing.Point(5, 20)
    Me.opPercentuale.Name = "opPercentuale"
    Me.opPercentuale.NTSCheckValue = "S"
    Me.opPercentuale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPercentuale.Properties.Appearance.Options.UseBackColor = True
    Me.opPercentuale.Properties.AutoHeight = False
    Me.opPercentuale.Properties.Caption = "&Percentuale"
    Me.opPercentuale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPercentuale.Size = New System.Drawing.Size(86, 19)
    Me.opPercentuale.TabIndex = 4
    '
    'lbArrotond
    '
    Me.lbArrotond.AutoSize = True
    Me.lbArrotond.BackColor = System.Drawing.Color.Transparent
    Me.lbArrotond.Location = New System.Drawing.Point(4, 110)
    Me.lbArrotond.Name = "lbArrotond"
    Me.lbArrotond.NTSDbField = ""
    Me.lbArrotond.Size = New System.Drawing.Size(134, 13)
    Me.lbArrotond.TabIndex = 136
    Me.lbArrotond.Text = "Arrotondamento di default"
    Me.lbArrotond.Tooltip = ""
    Me.lbArrotond.UseMnemonic = False
    '
    'fmElaborazione
    '
    Me.fmElaborazione.AllowDrop = True
    Me.fmElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmElaborazione.Appearance.Options.UseBackColor = True
    Me.fmElaborazione.Controls.Add(Me.ckElaborazione)
    Me.fmElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmElaborazione.Location = New System.Drawing.Point(3, 364)
    Me.fmElaborazione.Name = "fmElaborazione"
    Me.fmElaborazione.ShowCaption = False
    Me.fmElaborazione.Size = New System.Drawing.Size(179, 30)
    Me.fmElaborazione.TabIndex = 147
    '
    'ckElaborazione
    '
    Me.ckElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazione.Location = New System.Drawing.Point(5, 5)
    Me.ckElaborazione.Name = "ckElaborazione"
    Me.ckElaborazione.NTSCheckValue = "S"
    Me.ckElaborazione.NTSUnCheckValue = "N"
    Me.ckElaborazione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazione.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazione.Properties.AutoHeight = False
    Me.ckElaborazione.Properties.Caption = "&Elaborazione automatica"
    Me.ckElaborazione.Size = New System.Drawing.Size(143, 19)
    Me.ckElaborazione.TabIndex = 147
    '
    'edArrotond
    '
    Me.edArrotond.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArrotond.EditValue = "0"
    Me.edArrotond.Location = New System.Drawing.Point(144, 107)
    Me.edArrotond.Name = "edArrotond"
    Me.edArrotond.NTSDbField = ""
    Me.edArrotond.NTSFormat = "0"
    Me.edArrotond.NTSForzaVisZoom = False
    Me.edArrotond.NTSOldValue = ""
    Me.edArrotond.Properties.Appearance.Options.UseTextOptions = True
    Me.edArrotond.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArrotond.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArrotond.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArrotond.Properties.AutoHeight = False
    Me.edArrotond.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArrotond.Properties.MaxLength = 65536
    Me.edArrotond.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArrotond.Size = New System.Drawing.Size(131, 20)
    Me.edArrotond.TabIndex = 137
    '
    'ckAggiungiIVA
    '
    Me.ckAggiungiIVA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiungiIVA.Location = New System.Drawing.Point(8, 195)
    Me.ckAggiungiIVA.Name = "ckAggiungiIVA"
    Me.ckAggiungiIVA.NTSCheckValue = "S"
    Me.ckAggiungiIVA.NTSUnCheckValue = "N"
    Me.ckAggiungiIVA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiungiIVA.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiungiIVA.Properties.AutoHeight = False
    Me.ckAggiungiIVA.Properties.Caption = "Aggiungi l'IVA al valore del listino di partenza"
    Me.ckAggiungiIVA.Size = New System.Drawing.Size(242, 19)
    Me.ckAggiungiIVA.TabIndex = 146
    '
    'lbRicar1
    '
    Me.lbRicar1.AutoSize = True
    Me.lbRicar1.BackColor = System.Drawing.Color.Transparent
    Me.lbRicar1.Location = New System.Drawing.Point(3, 132)
    Me.lbRicar1.Name = "lbRicar1"
    Me.lbRicar1.NTSDbField = ""
    Me.lbRicar1.Size = New System.Drawing.Size(131, 13)
    Me.lbRicar1.TabIndex = 138
    Me.lbRicar1.Text = "Percentuale di incremento"
    Me.lbRicar1.Tooltip = ""
    Me.lbRicar1.UseMnemonic = False
    '
    'lbRicar2
    '
    Me.lbRicar2.AutoSize = True
    Me.lbRicar2.BackColor = System.Drawing.Color.Transparent
    Me.lbRicar2.Location = New System.Drawing.Point(202, 132)
    Me.lbRicar2.Name = "lbRicar2"
    Me.lbRicar2.NTSDbField = ""
    Me.lbRicar2.Size = New System.Drawing.Size(15, 13)
    Me.lbRicar2.TabIndex = 145
    Me.lbRicar2.Text = "+"
    Me.lbRicar2.Tooltip = ""
    Me.lbRicar2.UseMnemonic = False
    '
    'edRicar1
    '
    Me.edRicar1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRicar1.EditValue = "0"
    Me.edRicar1.Location = New System.Drawing.Point(145, 129)
    Me.edRicar1.Name = "edRicar1"
    Me.edRicar1.NTSDbField = ""
    Me.edRicar1.NTSFormat = "0"
    Me.edRicar1.NTSForzaVisZoom = False
    Me.edRicar1.NTSOldValue = ""
    Me.edRicar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edRicar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRicar1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicar1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicar1.Properties.AutoHeight = False
    Me.edRicar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicar1.Properties.MaxLength = 65536
    Me.edRicar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicar1.Size = New System.Drawing.Size(55, 20)
    Me.edRicar1.TabIndex = 139
    '
    'edRicar2
    '
    Me.edRicar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRicar2.EditValue = "0"
    Me.edRicar2.Location = New System.Drawing.Point(220, 129)
    Me.edRicar2.Name = "edRicar2"
    Me.edRicar2.NTSDbField = ""
    Me.edRicar2.NTSFormat = "0"
    Me.edRicar2.NTSForzaVisZoom = False
    Me.edRicar2.NTSOldValue = ""
    Me.edRicar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edRicar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRicar2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicar2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicar2.Properties.AutoHeight = False
    Me.edRicar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicar2.Properties.MaxLength = 65536
    Me.edRicar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicar2.Size = New System.Drawing.Size(55, 20)
    Me.edRicar2.TabIndex = 144
    '
    'lbValore
    '
    Me.lbValore.AutoSize = True
    Me.lbValore.BackColor = System.Drawing.Color.Transparent
    Me.lbValore.Location = New System.Drawing.Point(3, 154)
    Me.lbValore.Name = "lbValore"
    Me.lbValore.NTSDbField = ""
    Me.lbValore.Size = New System.Drawing.Size(104, 13)
    Me.lbValore.TabIndex = 140
    Me.lbValore.Text = "Valore di incremento"
    Me.lbValore.Tooltip = ""
    Me.lbValore.UseMnemonic = False
    '
    'lbAppMis
    '
    Me.lbAppMis.AutoSize = True
    Me.lbAppMis.BackColor = System.Drawing.Color.Transparent
    Me.lbAppMis.Location = New System.Drawing.Point(3, 176)
    Me.lbAppMis.Name = "lbAppMis"
    Me.lbAppMis.NTSDbField = ""
    Me.lbAppMis.Size = New System.Drawing.Size(133, 13)
    Me.lbAppMis.TabIndex = 143
    Me.lbAppMis.Text = "Applicata a Unità di Misura"
    Me.lbAppMis.Tooltip = ""
    Me.lbAppMis.UseMnemonic = False
    '
    'edValore
    '
    Me.edValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValore.EditValue = "0"
    Me.edValore.Location = New System.Drawing.Point(145, 151)
    Me.edValore.Name = "edValore"
    Me.edValore.NTSDbField = ""
    Me.edValore.NTSFormat = "0"
    Me.edValore.NTSForzaVisZoom = False
    Me.edValore.NTSOldValue = ""
    Me.edValore.Properties.Appearance.Options.UseTextOptions = True
    Me.edValore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValore.Properties.AutoHeight = False
    Me.edValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValore.Properties.MaxLength = 65536
    Me.edValore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValore.Size = New System.Drawing.Size(130, 20)
    Me.edValore.TabIndex = 141
    '
    'cbAppMis
    '
    Me.cbAppMis.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAppMis.DataSource = Nothing
    Me.cbAppMis.DisplayMember = ""
    Me.cbAppMis.Location = New System.Drawing.Point(145, 173)
    Me.cbAppMis.Name = "cbAppMis"
    Me.cbAppMis.NTSDbField = ""
    Me.cbAppMis.Properties.AutoHeight = False
    Me.cbAppMis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAppMis.Properties.DropDownRows = 30
    Me.cbAppMis.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAppMis.SelectedValue = ""
    Me.cbAppMis.Size = New System.Drawing.Size(130, 20)
    Me.cbAppMis.TabIndex = 142
    Me.cbAppMis.ValueMember = ""
    '
    'lbStatus
    '
    Me.lbStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbStatus.Location = New System.Drawing.Point(9, 538)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(730, 13)
    Me.lbStatus.TabIndex = 148
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'fmListinoElabora
    '
    Me.fmListinoElabora.AllowDrop = True
    Me.fmListinoElabora.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListinoElabora.Appearance.Options.UseBackColor = True
    Me.fmListinoElabora.Controls.Add(Me.edScadprom)
    Me.fmListinoElabora.Controls.Add(Me.ckAggiornalistini)
    Me.fmListinoElabora.Controls.Add(Me.edCodlavOut)
    Me.fmListinoElabora.Controls.Add(Me.lbCodlavOut)
    Me.fmListinoElabora.Controls.Add(Me.lbXx_CodlavOut)
    Me.fmListinoElabora.Controls.Add(Me.lbScadprom)
    Me.fmListinoElabora.Controls.Add(Me.edCodtpro)
    Me.fmListinoElabora.Controls.Add(Me.lbCodtpro)
    Me.fmListinoElabora.Controls.Add(Me.lbXx_Codtpro)
    Me.fmListinoElabora.Controls.Add(Me.edCambioelab)
    Me.fmListinoElabora.Controls.Add(Me.lbCambioelab)
    Me.fmListinoElabora.Controls.Add(Me.edValelab)
    Me.fmListinoElabora.Controls.Add(Me.lbValelab)
    Me.fmListinoElabora.Controls.Add(Me.lbXx_Valelab)
    Me.fmListinoElabora.Controls.Add(Me.edListelab)
    Me.fmListinoElabora.Controls.Add(Me.lbListelab)
    Me.fmListinoElabora.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListinoElabora.Location = New System.Drawing.Point(7, 384)
    Me.fmListinoElabora.Name = "fmListinoElabora"
    Me.fmListinoElabora.ShowCaption = False
    Me.fmListinoElabora.Size = New System.Drawing.Size(453, 141)
    Me.fmListinoElabora.TabIndex = 134
    '
    'edScadprom
    '
    Me.edScadprom.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScadprom.EditValue = "01/01/1900"
    Me.edScadprom.Location = New System.Drawing.Point(110, 93)
    Me.edScadprom.Name = "edScadprom"
    Me.edScadprom.NTSDbField = ""
    Me.edScadprom.NTSForzaVisZoom = False
    Me.edScadprom.NTSOldValue = ""
    Me.edScadprom.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScadprom.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScadprom.Properties.AutoHeight = False
    Me.edScadprom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScadprom.Properties.MaxLength = 65536
    Me.edScadprom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScadprom.Size = New System.Drawing.Size(85, 20)
    Me.edScadprom.TabIndex = 160
    '
    'ckAggiornalistini
    '
    Me.ckAggiornalistini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiornalistini.Location = New System.Drawing.Point(183, 6)
    Me.ckAggiornalistini.Name = "ckAggiornalistini"
    Me.ckAggiornalistini.NTSCheckValue = "S"
    Me.ckAggiornalistini.NTSUnCheckValue = "N"
    Me.ckAggiornalistini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiornalistini.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiornalistini.Properties.AutoHeight = False
    Me.ckAggiornalistini.Properties.Caption = "&Aggiorna listini dal 2 all' 8"
    Me.ckAggiornalistini.Size = New System.Drawing.Size(143, 19)
    Me.ckAggiornalistini.TabIndex = 159
    '
    'edCodlavOut
    '
    Me.edCodlavOut.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlavOut.EditValue = "0"
    Me.edCodlavOut.Location = New System.Drawing.Point(110, 115)
    Me.edCodlavOut.Name = "edCodlavOut"
    Me.edCodlavOut.NTSDbField = ""
    Me.edCodlavOut.NTSFormat = "0"
    Me.edCodlavOut.NTSForzaVisZoom = False
    Me.edCodlavOut.NTSOldValue = ""
    Me.edCodlavOut.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlavOut.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlavOut.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlavOut.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlavOut.Properties.AutoHeight = False
    Me.edCodlavOut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlavOut.Properties.MaxLength = 65536
    Me.edCodlavOut.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlavOut.Size = New System.Drawing.Size(67, 20)
    Me.edCodlavOut.TabIndex = 158
    '
    'lbCodlavOut
    '
    Me.lbCodlavOut.AutoSize = True
    Me.lbCodlavOut.BackColor = System.Drawing.Color.Transparent
    Me.lbCodlavOut.Location = New System.Drawing.Point(8, 118)
    Me.lbCodlavOut.Name = "lbCodlavOut"
    Me.lbCodlavOut.NTSDbField = ""
    Me.lbCodlavOut.Size = New System.Drawing.Size(65, 13)
    Me.lbCodlavOut.TabIndex = 157
    Me.lbCodlavOut.Text = "Lavorazione"
    Me.lbCodlavOut.Tooltip = ""
    Me.lbCodlavOut.UseMnemonic = False
    '
    'lbXx_CodlavOut
    '
    Me.lbXx_CodlavOut.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_CodlavOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_CodlavOut.Location = New System.Drawing.Point(182, 115)
    Me.lbXx_CodlavOut.Name = "lbXx_CodlavOut"
    Me.lbXx_CodlavOut.NTSDbField = ""
    Me.lbXx_CodlavOut.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_CodlavOut.TabIndex = 156
    Me.lbXx_CodlavOut.Tooltip = ""
    Me.lbXx_CodlavOut.UseMnemonic = False
    '
    'lbScadprom
    '
    Me.lbScadprom.AutoSize = True
    Me.lbScadprom.BackColor = System.Drawing.Color.Transparent
    Me.lbScadprom.Location = New System.Drawing.Point(8, 96)
    Me.lbScadprom.Name = "lbScadprom"
    Me.lbScadprom.NTSDbField = ""
    Me.lbScadprom.Size = New System.Drawing.Size(95, 13)
    Me.lbScadprom.TabIndex = 154
    Me.lbScadprom.Text = "Scadenza promoz."
    Me.lbScadprom.Tooltip = ""
    Me.lbScadprom.UseMnemonic = False
    '
    'edCodtpro
    '
    Me.edCodtpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpro.EditValue = "0"
    Me.edCodtpro.Location = New System.Drawing.Point(110, 71)
    Me.edCodtpro.Name = "edCodtpro"
    Me.edCodtpro.NTSDbField = ""
    Me.edCodtpro.NTSFormat = "0"
    Me.edCodtpro.NTSForzaVisZoom = False
    Me.edCodtpro.NTSOldValue = ""
    Me.edCodtpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpro.Properties.AutoHeight = False
    Me.edCodtpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpro.Properties.MaxLength = 65536
    Me.edCodtpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpro.Size = New System.Drawing.Size(67, 20)
    Me.edCodtpro.TabIndex = 152
    '
    'lbCodtpro
    '
    Me.lbCodtpro.AutoSize = True
    Me.lbCodtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpro.Location = New System.Drawing.Point(8, 74)
    Me.lbCodtpro.Name = "lbCodtpro"
    Me.lbCodtpro.NTSDbField = ""
    Me.lbCodtpro.Size = New System.Drawing.Size(62, 13)
    Me.lbCodtpro.TabIndex = 151
    Me.lbCodtpro.Text = "Promozione"
    Me.lbCodtpro.Tooltip = ""
    Me.lbCodtpro.UseMnemonic = False
    '
    'lbXx_Codtpro
    '
    Me.lbXx_Codtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codtpro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codtpro.Location = New System.Drawing.Point(182, 71)
    Me.lbXx_Codtpro.Name = "lbXx_Codtpro"
    Me.lbXx_Codtpro.NTSDbField = ""
    Me.lbXx_Codtpro.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Codtpro.TabIndex = 150
    Me.lbXx_Codtpro.Tooltip = ""
    Me.lbXx_Codtpro.UseMnemonic = False
    '
    'edCambioelab
    '
    Me.edCambioelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCambioelab.EditValue = "0"
    Me.edCambioelab.Location = New System.Drawing.Point(110, 49)
    Me.edCambioelab.Name = "edCambioelab"
    Me.edCambioelab.NTSDbField = ""
    Me.edCambioelab.NTSFormat = "0"
    Me.edCambioelab.NTSForzaVisZoom = False
    Me.edCambioelab.NTSOldValue = "0"
    Me.edCambioelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edCambioelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCambioelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCambioelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCambioelab.Properties.AutoHeight = False
    Me.edCambioelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCambioelab.Properties.MaxLength = 65536
    Me.edCambioelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCambioelab.Size = New System.Drawing.Size(208, 20)
    Me.edCambioelab.TabIndex = 149
    '
    'lbCambioelab
    '
    Me.lbCambioelab.AutoSize = True
    Me.lbCambioelab.BackColor = System.Drawing.Color.Transparent
    Me.lbCambioelab.Location = New System.Drawing.Point(8, 52)
    Me.lbCambioelab.Name = "lbCambioelab"
    Me.lbCambioelab.NTSDbField = ""
    Me.lbCambioelab.Size = New System.Drawing.Size(42, 13)
    Me.lbCambioelab.TabIndex = 148
    Me.lbCambioelab.Text = "Cambio"
    Me.lbCambioelab.Tooltip = ""
    Me.lbCambioelab.UseMnemonic = False
    '
    'edValelab
    '
    Me.edValelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValelab.EditValue = "0"
    Me.edValelab.Location = New System.Drawing.Point(110, 27)
    Me.edValelab.Name = "edValelab"
    Me.edValelab.NTSDbField = ""
    Me.edValelab.NTSFormat = "0"
    Me.edValelab.NTSForzaVisZoom = False
    Me.edValelab.NTSOldValue = ""
    Me.edValelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edValelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValelab.Properties.AutoHeight = False
    Me.edValelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValelab.Properties.MaxLength = 65536
    Me.edValelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValelab.Size = New System.Drawing.Size(67, 20)
    Me.edValelab.TabIndex = 146
    '
    'lbValelab
    '
    Me.lbValelab.AutoSize = True
    Me.lbValelab.BackColor = System.Drawing.Color.Transparent
    Me.lbValelab.Location = New System.Drawing.Point(8, 30)
    Me.lbValelab.Name = "lbValelab"
    Me.lbValelab.NTSDbField = ""
    Me.lbValelab.Size = New System.Drawing.Size(37, 13)
    Me.lbValelab.TabIndex = 145
    Me.lbValelab.Text = "Valuta"
    Me.lbValelab.Tooltip = ""
    Me.lbValelab.UseMnemonic = False
    '
    'lbXx_Valelab
    '
    Me.lbXx_Valelab.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Valelab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Valelab.Location = New System.Drawing.Point(182, 27)
    Me.lbXx_Valelab.Name = "lbXx_Valelab"
    Me.lbXx_Valelab.NTSDbField = ""
    Me.lbXx_Valelab.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Valelab.TabIndex = 144
    Me.lbXx_Valelab.Tooltip = ""
    Me.lbXx_Valelab.UseMnemonic = False
    '
    'edListelab
    '
    Me.edListelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListelab.EditValue = "0"
    Me.edListelab.Location = New System.Drawing.Point(110, 5)
    Me.edListelab.Name = "edListelab"
    Me.edListelab.NTSDbField = ""
    Me.edListelab.NTSFormat = "0"
    Me.edListelab.NTSForzaVisZoom = False
    Me.edListelab.NTSOldValue = ""
    Me.edListelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edListelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListelab.Properties.AutoHeight = False
    Me.edListelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListelab.Properties.MaxLength = 65536
    Me.edListelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListelab.Size = New System.Drawing.Size(67, 20)
    Me.edListelab.TabIndex = 143
    '
    'lbListelab
    '
    Me.lbListelab.AutoSize = True
    Me.lbListelab.BackColor = System.Drawing.Color.Transparent
    Me.lbListelab.Location = New System.Drawing.Point(8, 8)
    Me.lbListelab.Name = "lbListelab"
    Me.lbListelab.NTSDbField = ""
    Me.lbListelab.Size = New System.Drawing.Size(101, 13)
    Me.lbListelab.TabIndex = 142
    Me.lbListelab.Text = "Listino da elaborare"
    Me.lbListelab.Tooltip = ""
    Me.lbListelab.UseMnemonic = False
    '
    'fmListinoPartenza
    '
    Me.fmListinoPartenza.AllowDrop = True
    Me.fmListinoPartenza.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListinoPartenza.Appearance.Options.UseBackColor = True
    Me.fmListinoPartenza.Controls.Add(Me.fmPromo)
    Me.fmListinoPartenza.Controls.Add(Me.opQualsiasiFase)
    Me.fmListinoPartenza.Controls.Add(Me.opSoloFase)
    Me.fmListinoPartenza.Controls.Add(Me.opUltimaFase)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar3)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar2)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar1)
    Me.fmListinoPartenza.Controls.Add(Me.edFase)
    Me.fmListinoPartenza.Controls.Add(Me.edCodlavo)
    Me.fmListinoPartenza.Controls.Add(Me.lbCodlavo)
    Me.fmListinoPartenza.Controls.Add(Me.lbXx_Codlavo)
    Me.fmListinoPartenza.Controls.Add(Me.edCambiopar)
    Me.fmListinoPartenza.Controls.Add(Me.lbCambiopar)
    Me.fmListinoPartenza.Controls.Add(Me.edValpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbValpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbXx_Valpar)
    Me.fmListinoPartenza.Controls.Add(Me.edListpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar)
    Me.fmListinoPartenza.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListinoPartenza.Location = New System.Drawing.Point(7, 131)
    Me.fmListinoPartenza.Name = "fmListinoPartenza"
    Me.fmListinoPartenza.Size = New System.Drawing.Size(453, 246)
    Me.fmListinoPartenza.TabIndex = 133
    '
    'fmPromo
    '
    Me.fmPromo.AllowDrop = True
    Me.fmPromo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPromo.Appearance.Options.UseBackColor = True
    Me.fmPromo.Controls.Add(Me.lbDesPromo)
    Me.fmPromo.Controls.Add(Me.edPromo)
    Me.fmPromo.Controls.Add(Me.opPromoSpecifica)
    Me.fmPromo.Controls.Add(Me.opTuttePromo)
    Me.fmPromo.Controls.Add(Me.opNoPromo)
    Me.fmPromo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPromo.Location = New System.Drawing.Point(5, 157)
    Me.fmPromo.Name = "fmPromo"
    Me.fmPromo.Size = New System.Drawing.Size(443, 80)
    Me.fmPromo.TabIndex = 150
    Me.fmPromo.Text = "Promozioni"
    '
    'lbDesPromo
    '
    Me.lbDesPromo.BackColor = System.Drawing.Color.Transparent
    Me.lbDesPromo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesPromo.Location = New System.Drawing.Point(241, 39)
    Me.lbDesPromo.Name = "lbDesPromo"
    Me.lbDesPromo.NTSDbField = ""
    Me.lbDesPromo.Size = New System.Drawing.Size(194, 20)
    Me.lbDesPromo.TabIndex = 4
    Me.lbDesPromo.Tooltip = ""
    Me.lbDesPromo.UseMnemonic = False
    '
    'edPromo
    '
    Me.edPromo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPromo.EditValue = "0"
    Me.edPromo.Enabled = False
    Me.edPromo.Location = New System.Drawing.Point(178, 39)
    Me.edPromo.Name = "edPromo"
    Me.edPromo.NTSDbField = ""
    Me.edPromo.NTSFormat = "0"
    Me.edPromo.NTSForzaVisZoom = False
    Me.edPromo.NTSOldValue = ""
    Me.edPromo.Properties.Appearance.Options.UseTextOptions = True
    Me.edPromo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPromo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPromo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPromo.Properties.AutoHeight = False
    Me.edPromo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPromo.Properties.MaxLength = 65536
    Me.edPromo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPromo.Size = New System.Drawing.Size(57, 20)
    Me.edPromo.TabIndex = 3
    '
    'opPromoSpecifica
    '
    Me.opPromoSpecifica.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPromoSpecifica.Location = New System.Drawing.Point(5, 40)
    Me.opPromoSpecifica.Name = "opPromoSpecifica"
    Me.opPromoSpecifica.NTSCheckValue = "S"
    Me.opPromoSpecifica.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPromoSpecifica.Properties.Appearance.Options.UseBackColor = True
    Me.opPromoSpecifica.Properties.AutoHeight = False
    Me.opPromoSpecifica.Properties.Caption = "Listini con codice promozione"
    Me.opPromoSpecifica.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPromoSpecifica.Size = New System.Drawing.Size(167, 19)
    Me.opPromoSpecifica.TabIndex = 2
    '
    'opTuttePromo
    '
    Me.opTuttePromo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTuttePromo.Location = New System.Drawing.Point(5, 60)
    Me.opTuttePromo.Name = "opTuttePromo"
    Me.opTuttePromo.NTSCheckValue = "S"
    Me.opTuttePromo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTuttePromo.Properties.Appearance.Options.UseBackColor = True
    Me.opTuttePromo.Properties.AutoHeight = False
    Me.opTuttePromo.Properties.Caption = "Tutti i listini in promozione"
    Me.opTuttePromo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTuttePromo.Size = New System.Drawing.Size(158, 19)
    Me.opTuttePromo.TabIndex = 1
    '
    'opNoPromo
    '
    Me.opNoPromo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opNoPromo.EditValue = True
    Me.opNoPromo.Location = New System.Drawing.Point(5, 20)
    Me.opNoPromo.Name = "opNoPromo"
    Me.opNoPromo.NTSCheckValue = "S"
    Me.opNoPromo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opNoPromo.Properties.Appearance.Options.UseBackColor = True
    Me.opNoPromo.Properties.AutoHeight = False
    Me.opNoPromo.Properties.Caption = "Listini non in promozione"
    Me.opNoPromo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opNoPromo.Size = New System.Drawing.Size(144, 19)
    Me.opNoPromo.TabIndex = 0
    '
    'opQualsiasiFase
    '
    Me.opQualsiasiFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.opQualsiasiFase.Location = New System.Drawing.Point(266, 129)
    Me.opQualsiasiFase.Name = "opQualsiasiFase"
    Me.opQualsiasiFase.NTSCheckValue = "S"
    Me.opQualsiasiFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opQualsiasiFase.Properties.Appearance.Options.UseBackColor = True
    Me.opQualsiasiFase.Properties.AutoHeight = False
    Me.opQualsiasiFase.Properties.Caption = "Qualsiasi fase"
    Me.opQualsiasiFase.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opQualsiasiFase.Size = New System.Drawing.Size(88, 19)
    Me.opQualsiasiFase.TabIndex = 149
    '
    'opSoloFase
    '
    Me.opSoloFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSoloFase.Location = New System.Drawing.Point(108, 129)
    Me.opSoloFase.Name = "opSoloFase"
    Me.opSoloFase.NTSCheckValue = "S"
    Me.opSoloFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSoloFase.Properties.Appearance.Options.UseBackColor = True
    Me.opSoloFase.Properties.AutoHeight = False
    Me.opSoloFase.Properties.Caption = "Solo fase"
    Me.opSoloFase.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSoloFase.Size = New System.Drawing.Size(67, 19)
    Me.opSoloFase.TabIndex = 148
    '
    'opUltimaFase
    '
    Me.opUltimaFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.opUltimaFase.EditValue = True
    Me.opUltimaFase.Location = New System.Drawing.Point(5, 130)
    Me.opUltimaFase.Name = "opUltimaFase"
    Me.opUltimaFase.NTSCheckValue = "S"
    Me.opUltimaFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opUltimaFase.Properties.Appearance.Options.UseBackColor = True
    Me.opUltimaFase.Properties.AutoHeight = False
    Me.opUltimaFase.Properties.Caption = "Solo ultima fase"
    Me.opUltimaFase.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opUltimaFase.Size = New System.Drawing.Size(97, 19)
    Me.opUltimaFase.TabIndex = 147
    '
    'lbListpar3
    '
    Me.lbListpar3.AutoSize = True
    Me.lbListpar3.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar3.Location = New System.Drawing.Point(183, 46)
    Me.lbListpar3.Name = "lbListpar3"
    Me.lbListpar3.NTSDbField = ""
    Me.lbListpar3.Size = New System.Drawing.Size(98, 13)
    Me.lbListpar3.TabIndex = 146
    Me.lbListpar3.Text = "(-2 = Costo Medio)"
    Me.lbListpar3.Tooltip = ""
    Me.lbListpar3.UseMnemonic = False
    '
    'lbListpar2
    '
    Me.lbListpar2.AutoSize = True
    Me.lbListpar2.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar2.Location = New System.Drawing.Point(183, 33)
    Me.lbListpar2.Name = "lbListpar2"
    Me.lbListpar2.NTSDbField = ""
    Me.lbListpar2.Size = New System.Drawing.Size(195, 13)
    Me.lbListpar2.TabIndex = 145
    Me.lbListpar2.Text = "(-1 = Ultimo Costo con Oneri accessori)"
    Me.lbListpar2.Tooltip = ""
    Me.lbListpar2.UseMnemonic = False
    '
    'lbListpar1
    '
    Me.lbListpar1.AutoSize = True
    Me.lbListpar1.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar1.Location = New System.Drawing.Point(183, 20)
    Me.lbListpar1.Name = "lbListpar1"
    Me.lbListpar1.NTSDbField = ""
    Me.lbListpar1.Size = New System.Drawing.Size(143, 13)
    Me.lbListpar1.TabIndex = 144
    Me.lbListpar1.Text = "( 1 - 999) (0 = Ultimo Costo)"
    Me.lbListpar1.Tooltip = ""
    Me.lbListpar1.UseMnemonic = False
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.EditValue = "0"
    Me.edFase.Location = New System.Drawing.Point(183, 129)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(67, 20)
    Me.edFase.TabIndex = 143
    Me.edFase.Visible = False
    '
    'edCodlavo
    '
    Me.edCodlavo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlavo.EditValue = "0"
    Me.edCodlavo.Location = New System.Drawing.Point(110, 107)
    Me.edCodlavo.Name = "edCodlavo"
    Me.edCodlavo.NTSDbField = ""
    Me.edCodlavo.NTSFormat = "0"
    Me.edCodlavo.NTSForzaVisZoom = False
    Me.edCodlavo.NTSOldValue = ""
    Me.edCodlavo.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlavo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlavo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlavo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlavo.Properties.AutoHeight = False
    Me.edCodlavo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlavo.Properties.MaxLength = 65536
    Me.edCodlavo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlavo.Size = New System.Drawing.Size(67, 20)
    Me.edCodlavo.TabIndex = 140
    '
    'lbCodlavo
    '
    Me.lbCodlavo.AutoSize = True
    Me.lbCodlavo.BackColor = System.Drawing.Color.Transparent
    Me.lbCodlavo.Location = New System.Drawing.Point(8, 110)
    Me.lbCodlavo.Name = "lbCodlavo"
    Me.lbCodlavo.NTSDbField = ""
    Me.lbCodlavo.Size = New System.Drawing.Size(65, 13)
    Me.lbCodlavo.TabIndex = 139
    Me.lbCodlavo.Text = "Lavorazione"
    Me.lbCodlavo.Tooltip = ""
    Me.lbCodlavo.UseMnemonic = False
    '
    'lbXx_Codlavo
    '
    Me.lbXx_Codlavo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codlavo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codlavo.Location = New System.Drawing.Point(183, 107)
    Me.lbXx_Codlavo.Name = "lbXx_Codlavo"
    Me.lbXx_Codlavo.NTSDbField = ""
    Me.lbXx_Codlavo.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Codlavo.TabIndex = 138
    Me.lbXx_Codlavo.Tooltip = ""
    Me.lbXx_Codlavo.UseMnemonic = False
    '
    'edCambiopar
    '
    Me.edCambiopar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCambiopar.EditValue = "0"
    Me.edCambiopar.Location = New System.Drawing.Point(110, 85)
    Me.edCambiopar.Name = "edCambiopar"
    Me.edCambiopar.NTSDbField = ""
    Me.edCambiopar.NTSFormat = "0"
    Me.edCambiopar.NTSForzaVisZoom = False
    Me.edCambiopar.NTSOldValue = ""
    Me.edCambiopar.Properties.Appearance.Options.UseTextOptions = True
    Me.edCambiopar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCambiopar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCambiopar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCambiopar.Properties.AutoHeight = False
    Me.edCambiopar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCambiopar.Properties.MaxLength = 65536
    Me.edCambiopar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCambiopar.Size = New System.Drawing.Size(208, 20)
    Me.edCambiopar.TabIndex = 137
    '
    'lbCambiopar
    '
    Me.lbCambiopar.AutoSize = True
    Me.lbCambiopar.BackColor = System.Drawing.Color.Transparent
    Me.lbCambiopar.Location = New System.Drawing.Point(8, 88)
    Me.lbCambiopar.Name = "lbCambiopar"
    Me.lbCambiopar.NTSDbField = ""
    Me.lbCambiopar.Size = New System.Drawing.Size(42, 13)
    Me.lbCambiopar.TabIndex = 136
    Me.lbCambiopar.Text = "Cambio"
    Me.lbCambiopar.Tooltip = ""
    Me.lbCambiopar.UseMnemonic = False
    '
    'edValpar
    '
    Me.edValpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValpar.EditValue = "0"
    Me.edValpar.Location = New System.Drawing.Point(110, 63)
    Me.edValpar.Name = "edValpar"
    Me.edValpar.NTSDbField = ""
    Me.edValpar.NTSFormat = "0"
    Me.edValpar.NTSForzaVisZoom = False
    Me.edValpar.NTSOldValue = ""
    Me.edValpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edValpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValpar.Properties.AutoHeight = False
    Me.edValpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValpar.Properties.MaxLength = 65536
    Me.edValpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValpar.Size = New System.Drawing.Size(67, 20)
    Me.edValpar.TabIndex = 134
    '
    'lbValpar
    '
    Me.lbValpar.AutoSize = True
    Me.lbValpar.BackColor = System.Drawing.Color.Transparent
    Me.lbValpar.Location = New System.Drawing.Point(8, 66)
    Me.lbValpar.Name = "lbValpar"
    Me.lbValpar.NTSDbField = ""
    Me.lbValpar.Size = New System.Drawing.Size(37, 13)
    Me.lbValpar.TabIndex = 133
    Me.lbValpar.Text = "Valuta"
    Me.lbValpar.Tooltip = ""
    Me.lbValpar.UseMnemonic = False
    '
    'lbXx_Valpar
    '
    Me.lbXx_Valpar.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Valpar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Valpar.Location = New System.Drawing.Point(183, 63)
    Me.lbXx_Valpar.Name = "lbXx_Valpar"
    Me.lbXx_Valpar.NTSDbField = ""
    Me.lbXx_Valpar.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Valpar.TabIndex = 132
    Me.lbXx_Valpar.Tooltip = ""
    Me.lbXx_Valpar.UseMnemonic = False
    '
    'edListpar
    '
    Me.edListpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListpar.EditValue = "0"
    Me.edListpar.Location = New System.Drawing.Point(110, 30)
    Me.edListpar.Name = "edListpar"
    Me.edListpar.NTSDbField = ""
    Me.edListpar.NTSFormat = "0"
    Me.edListpar.NTSForzaVisZoom = False
    Me.edListpar.NTSOldValue = ""
    Me.edListpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edListpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListpar.Properties.AutoHeight = False
    Me.edListpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListpar.Properties.MaxLength = 65536
    Me.edListpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListpar.Size = New System.Drawing.Size(67, 20)
    Me.edListpar.TabIndex = 131
    '
    'lbListpar
    '
    Me.lbListpar.AutoSize = True
    Me.lbListpar.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar.Location = New System.Drawing.Point(8, 33)
    Me.lbListpar.Name = "lbListpar"
    Me.lbListpar.NTSDbField = ""
    Me.lbListpar.Size = New System.Drawing.Size(94, 13)
    Me.lbListpar.TabIndex = 130
    Me.lbListpar.Text = "Listino di partenza"
    Me.lbListpar.Tooltip = ""
    Me.lbListpar.UseMnemonic = False
    '
    'lbDatagg
    '
    Me.lbDatagg.AutoSize = True
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.Location = New System.Drawing.Point(466, 85)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(156, 13)
    Me.lbDatagg.TabIndex = 132
    Me.lbDatagg.Text = "Data inizio validità nuovo listino"
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'edDatagg
    '
    Me.edDatagg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatagg.EditValue = "01/01/1900"
    Me.edDatagg.Location = New System.Drawing.Point(654, 82)
    Me.edDatagg.Name = "edDatagg"
    Me.edDatagg.NTSDbField = ""
    Me.edDatagg.NTSForzaVisZoom = False
    Me.edDatagg.NTSOldValue = ""
    Me.edDatagg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatagg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatagg.Properties.AutoHeight = False
    Me.edDatagg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatagg.Properties.MaxLength = 65536
    Me.edDatagg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatagg.Size = New System.Drawing.Size(85, 20)
    Me.edDatagg.TabIndex = 131
    '
    'cmdArticoliDaDoc
    '
    Me.cmdArticoliDaDoc.ImagePath = ""
    Me.cmdArticoliDaDoc.ImageText = ""
    Me.cmdArticoliDaDoc.Location = New System.Drawing.Point(449, 34)
    Me.cmdArticoliDaDoc.Name = "cmdArticoliDaDoc"
    Me.cmdArticoliDaDoc.NTSContextMenu = Nothing
    Me.cmdArticoliDaDoc.Size = New System.Drawing.Size(290, 23)
    Me.cmdArticoliDaDoc.TabIndex = 130
    Me.cmdArticoliDaDoc.Text = "Seleziona Articoli da Documenti di Magazzino..."
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(449, 6)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(290, 23)
    Me.cmdSeleziona.TabIndex = 129
    Me.cmdSeleziona.Text = "&Seleziona Articoli..."
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(117, 60)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(93, 20)
    Me.edConto.TabIndex = 128
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(15, 65)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(11, 13)
    Me.lbConto.TabIndex = 127
    Me.lbConto.Text = "."
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbXx_Conto
    '
    Me.lbXx_Conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Conto.Location = New System.Drawing.Point(216, 60)
    Me.lbXx_Conto.Name = "lbXx_Conto"
    Me.lbXx_Conto.NTSDbField = ""
    Me.lbXx_Conto.Size = New System.Drawing.Size(244, 20)
    Me.lbXx_Conto.TabIndex = 126
    Me.lbXx_Conto.Tooltip = ""
    Me.lbXx_Conto.UseMnemonic = False
    '
    'fmTipoElaborazione
    '
    Me.fmTipoElaborazione.AllowDrop = True
    Me.fmTipoElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoElaborazione.Appearance.Options.UseBackColor = True
    Me.fmTipoElaborazione.Controls.Add(Me.cbTipoElaborazione)
    Me.fmTipoElaborazione.Controls.Add(Me.opGenericoDaFornitori)
    Me.fmTipoElaborazione.Controls.Add(Me.opGenericoDaClienti)
    Me.fmTipoElaborazione.Controls.Add(Me.opFornitori)
    Me.fmTipoElaborazione.Controls.Add(Me.opClienti)
    Me.fmTipoElaborazione.Controls.Add(Me.opGenerico)
    Me.fmTipoElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoElaborazione.Location = New System.Drawing.Point(7, 6)
    Me.fmTipoElaborazione.Name = "fmTipoElaborazione"
    Me.fmTipoElaborazione.Size = New System.Drawing.Size(318, 50)
    Me.fmTipoElaborazione.TabIndex = 121
    Me.fmTipoElaborazione.Text = "Aggiornamento/creazione Listino di tipo"
    '
    'cbTipoElaborazione
    '
    Me.cbTipoElaborazione.DataSource = Nothing
    Me.cbTipoElaborazione.DisplayMember = ""
    Me.cbTipoElaborazione.Location = New System.Drawing.Point(11, 24)
    Me.cbTipoElaborazione.Name = "cbTipoElaborazione"
    Me.cbTipoElaborazione.NTSDbField = ""
    Me.cbTipoElaborazione.Properties.AutoHeight = False
    Me.cbTipoElaborazione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoElaborazione.Properties.DropDownRows = 30
    Me.cbTipoElaborazione.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoElaborazione.SelectedValue = ""
    Me.cbTipoElaborazione.Size = New System.Drawing.Size(296, 20)
    Me.cbTipoElaborazione.TabIndex = 5
    Me.cbTipoElaborazione.ValueMember = ""
    '
    'opGenericoDaFornitori
    '
    Me.opGenericoDaFornitori.Cursor = System.Windows.Forms.Cursors.Default
    Me.opGenericoDaFornitori.Location = New System.Drawing.Point(-288, 105)
    Me.opGenericoDaFornitori.Name = "opGenericoDaFornitori"
    Me.opGenericoDaFornitori.NTSCheckValue = "S"
    Me.opGenericoDaFornitori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opGenericoDaFornitori.Properties.Appearance.Options.UseBackColor = True
    Me.opGenericoDaFornitori.Properties.AutoHeight = False
    Me.opGenericoDaFornitori.Properties.Caption = "Aggiorna/crea listino generico da listino Fornitori"
    Me.opGenericoDaFornitori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opGenericoDaFornitori.Size = New System.Drawing.Size(255, 19)
    Me.opGenericoDaFornitori.TabIndex = 4
    Me.opGenericoDaFornitori.Visible = False
    '
    'opGenericoDaClienti
    '
    Me.opGenericoDaClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opGenericoDaClienti.Location = New System.Drawing.Point(-278, 105)
    Me.opGenericoDaClienti.Name = "opGenericoDaClienti"
    Me.opGenericoDaClienti.NTSCheckValue = "S"
    Me.opGenericoDaClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opGenericoDaClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opGenericoDaClienti.Properties.AutoHeight = False
    Me.opGenericoDaClienti.Properties.Caption = "Aggiorna/crea listino generico da listino Clienti"
    Me.opGenericoDaClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opGenericoDaClienti.Size = New System.Drawing.Size(245, 19)
    Me.opGenericoDaClienti.TabIndex = 3
    Me.opGenericoDaClienti.Visible = False
    '
    'opFornitori
    '
    Me.opFornitori.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFornitori.Location = New System.Drawing.Point(-267, 105)
    Me.opFornitori.Name = "opFornitori"
    Me.opFornitori.NTSCheckValue = "S"
    Me.opFornitori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFornitori.Properties.Appearance.Options.UseBackColor = True
    Me.opFornitori.Properties.AutoHeight = False
    Me.opFornitori.Properties.Caption = "Aggiorna/crea listino speciale F&ornitori"
    Me.opFornitori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFornitori.Size = New System.Drawing.Size(211, 19)
    Me.opFornitori.TabIndex = 2
    Me.opFornitori.Visible = False
    '
    'opClienti
    '
    Me.opClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opClienti.Location = New System.Drawing.Point(-244, 105)
    Me.opClienti.Name = "opClienti"
    Me.opClienti.NTSCheckValue = "S"
    Me.opClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opClienti.Properties.AutoHeight = False
    Me.opClienti.Properties.Caption = "Aggiorna/crea listino speciale C&lienti"
    Me.opClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opClienti.Size = New System.Drawing.Size(200, 19)
    Me.opClienti.TabIndex = 1
    Me.opClienti.Visible = False
    '
    'opGenerico
    '
    Me.opGenerico.Cursor = System.Windows.Forms.Cursors.Default
    Me.opGenerico.EditValue = True
    Me.opGenerico.Location = New System.Drawing.Point(-267, 105)
    Me.opGenerico.Name = "opGenerico"
    Me.opGenerico.NTSCheckValue = "S"
    Me.opGenerico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opGenerico.Properties.Appearance.Options.UseBackColor = True
    Me.opGenerico.Properties.AutoHeight = False
    Me.opGenerico.Properties.Caption = "Ag&giorna/crea listino generico"
    Me.opGenerico.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opGenerico.Size = New System.Drawing.Size(168, 19)
    Me.opGenerico.TabIndex = 0
    Me.opGenerico.Visible = False
    '
    'FRMMGELAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(748, 589)
    Me.Controls.Add(Me.pnElar)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMMGELAR"
    Me.Text = "VARIAZIONE PREZZI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElar.ResumeLayout(False)
    Me.pnElar.PerformLayout()
    CType(Me.ckScontiDaCondComm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataListinoRiferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    CType(Me.ckVisRisultati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoVariazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoVariazione.ResumeLayout(False)
    Me.fmTipoVariazione.PerformLayout()
    CType(Me.opScorporaIva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRicarico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPercentuale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmElaborazione.ResumeLayout(False)
    Me.fmElaborazione.PerformLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiungiIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRicar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRicar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAppMis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListinoElabora, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListinoElabora.ResumeLayout(False)
    Me.fmListinoElabora.PerformLayout()
    CType(Me.edScadprom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiornalistini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlavOut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCambioelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListinoPartenza, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListinoPartenza.ResumeLayout(False)
    Me.fmListinoPartenza.PerformLayout()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPromo.ResumeLayout(False)
    Me.fmPromo.PerformLayout()
    CType(Me.edPromo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPromoSpecifica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTuttePromo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opNoPromo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opQualsiasiFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSoloFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opUltimaFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlavo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCambiopar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoElaborazione.ResumeLayout(False)
    Me.fmTipoElaborazione.PerformLayout()
    CType(Me.cbTipoElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opGenericoDaFornitori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opGenericoDaClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opGenerico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      tlbMain.NTSSetToolTip()

      ckElaborazione.NTSSetParam(oMenu, oApp.Tr(Me, 128685557583999205, "&Elaborazione automatica"), "S", "N")
      ckAggiungiIVA.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584155495, "Aggiungi l'IVA al valore del listino di partenza"), "S", "N")
      edRicar2.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584468075, "+"), oApp.FormatImporti, 5, -999, 999)
      cbAppMis.NTSSetParam(oApp.Tr(Me, 128685557584780655, "Valore di incremento"))
      edValore.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584936945, "Valore di incremento"), oCleElar.TrovaFmtPrz, 12, -999999999999, 999999999999)
      edRicar1.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585249525, "Percentuale di incremento"), oApp.FormatImporti, 5, -999, 999)
      edArrotond.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585562105, "Arrotondamento di default"), oApp.FormatTempi, 7, -1000000, 1000000)
      opScorporaIva.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585874685, "Scorpora Iva"), "S")
      opRicarico.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586030975, "Ri&carico/Margine"), "S")
      opValore.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586187265, "&Valore"), "S")
      opPercentuale.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586343555, "&Percentuale"), "S")
      ckAggiornalistini.NTSSetParam(oMenu, oApp.Tr(Me, 128770929105589315, "&Aggiorna listini dal 2 all' 8"), "S", "N")
      edCodlavOut.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557586499845, "Lavorazione"), tablavo)
      edScadprom.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586968715, "Data scad. promoz."), False)
      edCodtpro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557587281295, "Codice promozione"), tabtpro)
      edCambioelab.NTSSetParam(oMenu, oApp.Tr(Me, 128685557587750165, "Cambio"), "#,##0.000000000", 20, 0, 999999999)
      edValelab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557588062745, "Valuta"), tabvalu)
      edListelab.NTSSetParam(oMenu, oApp.Tr(Me, 128685557588531615, "Lista selezionata articoli"), "0", 4, 0, 9999)
      opQualsiasiFase.NTSSetParam(oMenu, oApp.Tr(Me, 128685557588687905, "Qualsiasi fase"), "S")
      opSoloFase.NTSSetParam(oMenu, oApp.Tr(Me, 128685557588844195, "Solo fase"), "S")
      opUltimaFase.NTSSetParam(oMenu, oApp.Tr(Me, 128685557589000485, "Solo ultima fase"), "S")
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 128685557589625645, "Fase"), "0", 4, 0, 9999)
      edCodlavo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557589781935, "Lavorazione"), tablavo)
      edCambiopar.NTSSetParam(oMenu, oApp.Tr(Me, 128685557590250805, "Cambio"), "#,##0.000000000", 20, 0, 999999999)
      edValpar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557590563385, "Valuta"), tabvalu)
      edListpar.NTSSetParam(oMenu, oApp.Tr(Me, 128685557591032255, "Listino di partenza"), "0", 4, -2, 9999)
      edDatagg.NTSSetParam(oMenu, oApp.Tr(Me, 128685557591344835, "Data inizio validità"), False)
      edDataListinoRiferim.NTSSetParam(oMenu, oApp.Tr(Me, 130687314245051152, "Data listini di riferimento"), False)
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557591813705, "Conto"), tabanagra)
      opGenericoDaFornitori.NTSSetParam(oMenu, oApp.Tr(Me, 128685557592282575, "Aggiorna/crea listino generico da listino Fornitori"), "S")
      opGenericoDaClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128685557592438865, "Aggiorna/crea listino generico da listino Clienti"), "S")
      opFornitori.NTSSetParam(oMenu, oApp.Tr(Me, 128685557592595155, "Aggiorna/crea listino speciale F&ornitori"), "S")
      opClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128685557592751445, "Aggiorna/crea listino speciale C&lienti"), "S")
      opGenerico.NTSSetParam(oMenu, oApp.Tr(Me, 128685557592907735, "Ag&giorna/crea listino generico"), "S")
      ckNetto.NTSSetParam(oMenu, oApp.Tr(Me, 129441353801675778, "Netto sconti spec. clienti/forn."), "S", "N")
      ckVisRisultati.NTSSetParam(oMenu, oApp.Tr(Me, 129441353801831988, "Visualizza risultati"), "S", "N")
      edPromo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129441353806830708, "Promozione"), tabtpro)
      opPromoSpecifica.NTSSetParam(oMenu, oApp.Tr(Me, 129441353806986918, "Listini con codice promozione"), "S")
      opTuttePromo.NTSSetParam(oMenu, oApp.Tr(Me, 129441353807143128, "Tutti i listini in promozione"), "T")
      opNoPromo.NTSSetParam(oMenu, oApp.Tr(Me, 129441353807299338, "Listini non in promozione"), "N")
      edCodlsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130384849288417725, "lista selezionata articoli"), tablsar)
      edCoddest.NTSSetParam(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), "0", 9, -1, 999999999)
      edCoddest.NTSSetParamZoom("ZOOMDESTDIV")
      edCoddest.CliePerDestdiv = edConto
      ckScontiDaCondComm.NTSSetParam(oMenu, oApp.Tr(Me, 130876565666111086, "Applico Priorità Sconti Clienti/Fornitori"), "S", "N")

      'edConto.NTSSetParamZoom("")
      edConto.NTSForzaVisZoom = True

      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGELAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim nDecPrx As Integer
    Try
      InitControls()

      CaricaCombo()
      strProntoMess = oApp.Tr(Me, 130421081986546402, "Pronto.")
      '------------------------------------------------------------------------------------
      oCleElar.bAggPrezzi0 = CBool(oMenu.GetSettingBus("BSMGELAR", "OPZIONI", ".", "AggiornaPrezzi0", "0", " ", "0"))
      oCleElar.strTipoArro = UCase(CStr(oMenu.GetSettingBus("BSMGELAR", "OPZIONI", ".", "TipoArrotondamento", "E", " ", "E")))
      '------------------------------------------------------------------------------------
      edConto.Text = "0"
      lbXx_Conto.Text = ""
      edCoddest.Text = "-1"
      lbDescoddest.Text = ""
      edCoddest.Enabled = False
      edDatagg.Text = NTSCDate(Now).ToShortDateString
      edDataListinoRiferim.Text = NTSCDate(Now).ToShortDateString
      edListpar.Text = "1"
      edValpar.Text = "0"
      lbXx_Valpar.Text = ""
      edCambiopar.Text = "1"
      edCambiopar.Enabled = False
      edListelab.Text = "1"
      ckAggiornalistini.Checked = False
      edValelab.Text = "0"
      lbXx_Valelab.Text = ""
      edCambioelab.Text = "1"
      edCambioelab.Enabled = False
      edCodtpro.Text = "0"
      lbXx_Codtpro.Text = ""
      edScadprom.Text = IntSetDate("31/12/2099")
      edScadprom.Enabled = False
      edCodlavo.Text = "0"
      edFase.Text = "0"
      edCodlavOut.Text = "0"
      lbXx_Codlavo.Text = ""

      'solo per azienda euro
      oCleElar.strArroPrx = "0,01"
      nDecPrx = oMenu.TrovaNdecSuPrzUn(0)
      Select Case nDecPrx
        Case 0
          oCleElar.strArroPrx = "1"
        Case 1
          oCleElar.strArroPrx = "0,1"
        Case 2
          oCleElar.strArroPrx = "0,01" 'default
        Case 3
          oCleElar.strArroPrx = "0,001"
        Case 4
          oCleElar.strArroPrx = "0,0001"
      End Select

      edArrotond.Text = NTSCStr(oCleElar.strArroPrx)
      edRicar1.Text = "0"
      edRicar2.Text = "0"
      edValore.Text = "0"
      edValore.Enabled = False
      edValore.TabStop = False
      lbValore.Enabled = False
      lbConto.Enabled = False
      lbAppMis.Enabled = False
      cbAppMis.Enabled = False

      cbAppMis.SelectedValue = "P"

      edConto.Enabled = False

      ckVisRisultati.Checked = CBool(oMenu.GetSettingBus("BSMGELAR", "RECENT", ".", "VisRisultati", "-1", " ", "-1"))

      ckNetto.Enabled = False
      ckNetto.Checked = False

      AbilitaDisabilitaScontiDaCondComm(False)
      '------------------------------------------------------------------------------
      oCleElar.bSeleziona = False

      lbStatus.Text = strProntoMess

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGELAR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleElar.bElabInCorso Then e.Cancel = True
      oMenu.SaveSettingBus("BSMGELAR", "RECENT", ".", "VisRisultati", NTSCStr(IIf((ckVisRisultati.Checked), "-1", "0")), " ", "NS.", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strSQL As String = ""
    Dim dRes As DialogResult
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleElar.bScontiDaCondComm = False
      oCleElar.lContoDaCondComm = 0
      oCleElar.nClasconContoDaCondComm = 0
      If ckScontiDaCondComm.Checked = True Then
        oMenu.SaveSettingBus("BSMGELAR", "RECENT", ".", "ScontiDaCondComm", _
          NTSCStr(IIf((ckScontiDaCondComm.Checked = True), "-1", "0")), " ", "NS.", "...", "...")
        If (ckScontiDaCondComm.Checked = True) And (NTSCInt(edConto.Text) = 0) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130873917288217764, "Attenzione!" & vbCrLf & _
            "Il conto |" & lbConto.Text & "| è obbligatorio, quando sono selezionati gli:" & vbCrLf & _
            " . '|" & ckScontiDaCondComm.Text & "|'"))
          edConto.Focus()
          Return
        End If
        oCleElar.bScontiDaCondComm = ckScontiDaCondComm.Checked
        oCleElar.lContoDaCondComm = NTSCInt(edConto.Text)
        If oMenu.ValCodiceDb(edConto.Text, DittaCorrente, "ANAGRA", "N", Nothing, dttTmp) = True Then
          If dttTmp.Rows.Count > 0 Then oCleElar.nClasconContoDaCondComm = NTSCInt(dttTmp.Rows(0)!an_clascon)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If (oCleElar.bSeleziona = False) And (NTSCInt(edCodlsar.Text) = 0) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128674290993239085, "Attenzione!" & vbCrLf & _
          "Non sono stati selezionati gli articoli e non è stata indicata una lista selezionata." & vbCrLf & _
          "Selezionarli ora?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          cmdSeleziona_Click(Me, Nothing)
          If oCleElar.bSeleziona = False Then Return
        Else
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodlsar.Text) <> 0 Then
        If oCleElar.bSeleziona = False Then
          oCleElar.strWhereFiar = ""
          oCleElar.strAdocQuery = ""
        End If
        If oCleElar.strWhereFiar = Nothing Then oCleElar.strWhereFiar = ""
        If oCleElar.strAdocQuery = Nothing Then oCleElar.strAdocQuery = ""
        strSQL = "CAST(lc_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lc_fase AS VARCHAR(4))" & _
          " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
          " WHERE codditt = " & CStrSQL(DittaCorrente) & _
          " AND lsa_codlsar = " & NTSCInt(edCodlsar.Text) & ")"
        If oCleElar.strWhereFiar.Trim <> "" And oCleElar.strAdocQuery.Trim = "" Then
          oCleElar.strWhereFiar += "§" & strSQL
        Else
          If oCleElar.strAdocQuery.Trim <> "" Then
            oCleElar.strAdocQuery += " AND " & strSQL
          Else
            oCleElar.strWhereFiar = " AND " & strSQL
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCInt(cbTipoElaborazione.SelectedValue) = 4 Or NTSCInt(cbTipoElaborazione.SelectedValue) = 5) And _
         (NTSCInt(edConto.Text) = 0) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 131074556605042087, "Attenzione!" & vbCrLf & _
          "Se selezionato l'aggiornamento/creazione di:" & vbCrLf & _
          ". |" & cbTipoElaborazione.Text & "|" & vbCrLf & _
          "il |" & lbConto.Text & "| è obbligatorio."))
        edConto.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleElar.ElaboraEx(NTSCInt(cbTipoElaborazione.SelectedValue), edConto.Text, edCodlavo.Text, edValpar.Text, _
                          edDatagg.Text, edFase.Text, edListpar.Text, opUltimaFase.Checked, _
                          opSoloFase.Checked, edListelab.Text, opQualsiasiFase.Checked, edValelab.Text, _
                          edCodtpro.Text, edCodlavOut.Text, edScadprom.Text, ckAggiornalistini.Checked, _
                          opValore.Checked, cbAppMis.SelectedValue, ckElaborazione.Checked, ckVisRisultati.Checked, _
                          ckAggiungiIVA.Checked, edValore.Text, edCambioelab.Text, edCambiopar.Text, _
                          edRicar1.Text, edRicar2.Text, opPercentuale.Checked, opRicarico.Checked, _
                          opScorporaIva.Checked, edArrotond.Text, dsElar, ckNetto.Checked, NTSCInt(edCoddest.Text), _
                          edDataListinoRiferim.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128551610678125000, "Elaborazione completata regolarmente."))
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128551610937343750, "Elaborazione non riuscita."))
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = strProntoMess
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      oCleElar.bSeleziona = False
      oCleElar.strWhereFiar = ""
      oCleElar.strAdocQuery = ""
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edConto.Text)

        Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
          Case 2, 4, 6 : oParam.strTipo = "C"
          Case 3, 5, 7 : oParam.strTipo = "F"
        End Select

        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then edConto.NTSTextDB = NTSZOOM.strIn
      ElseIf edCoddest.Focused Then
        SetFastZoom(edCoddest.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddest.Text
        oParam.lContoCF = NTSCInt(edConto.Text)  'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddest.Text Then edCoddest.NTSTextDB = NTSZOOM.strIn
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
    Me.Close()
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleElar.strWhereFiar = oPar.strOut.Trim
      oCleElar.strAdocQuery = ""

      oCleElar.bSeleziona = oCleElar.strWhereFiar <> ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdArticoliDaDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArticoliDaDoc.Click
    Dim oParam As New CLE__CLDP
    Dim fmAdoc As FRMMGADOC = Nothing
    Try
      oCleElar.strAdocQuery = ""

      fmAdoc = CType(NTSNewFormModal("FRMMGADOC"), FRMMGADOC)
      fmAdoc.Init(oMenu, oParam, DittaCorrente)
      fmAdoc.InitEntity(oCleElar)
      fmAdoc.ShowDialog()

      oCleElar.strWhereFiar = ""
      '-----------------------------------------------------------------------------------------
      If oCleElar.bAdocAnnullato = True Then
        oCleElar.bSeleziona = True
      Else
        oCleElar.bSeleziona = False
      End If
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmAdoc Is Nothing Then fmAdoc.Dispose()
      fmAdoc = Nothing
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edArrotond_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edArrotond.Validated
    Try
      If NTSCDec(edArrotond.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128695998129835058, "L'arrotondamento non può essere pari a zero."))
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodlavo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlavo.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodlavo_Validated(NTSCInt(edCodlavo.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodlavo.Text = "0"
        lbXx_Codlavo.Text = ""
      Else
        lbXx_Codlavo.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodlavOut_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlavOut.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodlavOut_Validated(NTSCInt(edCodlavOut.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodlavOut.Text = "0"
        lbXx_CodlavOut.Text = ""
      Else
        lbXx_CodlavOut.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodtpro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpro.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodtpro_Validated(NTSCInt(edCodtpro.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodtpro.Text = "0"
        lbXx_Codtpro.Text = ""
        edScadprom.Text = IntSetDate("31/12/2099")
        edScadprom.Enabled = False
      Else
        lbXx_Codtpro.Text = strTmp
        If edCodtpro.Text = "0" Then
          edScadprom.Text = IntSetDate("31/12/2099")
          edScadprom.Enabled = False
        Else
          GctlSetVisEnab(edScadprom, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edConto_Validated(NTSCInt(edConto.Text), strTmp, strError, _
                                       (NTSCInt(cbTipoElaborazione.SelectedValue) = 3 OrElse NTSCInt(cbTipoElaborazione.SelectedValue) = 7), _
                                       (NTSCInt(cbTipoElaborazione.SelectedValue) = 2 OrElse NTSCInt(cbTipoElaborazione.SelectedValue) = 6)) Then
        oApp.MsgBoxErr(strError)
        edConto.Text = "0"
        lbXx_Conto.Text = ""
      Else
        lbXx_Conto.Text = strTmp
      End If
      edCoddest.Text = "-1"
      lbDescoddest.Text = ""

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCoddest_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCoddest.Validated
    Dim strTmp As String = ""

    Try
      If oCleElar Is Nothing Then Return
      If NTSCInt(edCoddest.Text) <= 0 Then
        lbDescoddest.Text = ""
        Return
      End If

      If Not oMenu.ValCodiceDb(edCoddest.Text, DittaCorrente, "DESTDIV", "N", strTmp, Nothing, edConto.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130584436125707623, "Attenzione!" & vbCrLf & "Destinazione diversa inesistente."))
        edCoddest.Text = "-1"
        lbDescoddest.Text = ""
      Else
        lbDescoddest.Text = strTmp
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edDatagg_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDatagg.Validated
    Try
      If NTSCInt(edValpar.Text) <> 0 Then
        oCleElar.CercaCambioDiOggi(NTSCInt(edValpar.Text), edDatagg.Text, edCambiopar.Text)
      End If
      If NTSCInt(edValelab.Text) <> 0 Then
        oCleElar.CercaCambioDiOggi(NTSCInt(edValelab.Text), edDatagg.Text, edCambioelab.Text)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edListelab_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edListelab.Validated
    Try
      If NTSCInt(edListelab.Text) = 1 Then
        If Not ckAggiornalistini.Checked Then
          GctlSetVisEnab(ckAggiornalistini, True)
        End If
      Else
        ckAggiornalistini.Checked = False
        ckAggiornalistini.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValelab_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edValelab.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not edValelab.Text = "0" Then
        If Not oCleElar.edValelab_Validated(NTSCInt(edValelab.Text), strTmp, strError) Then
          oApp.MsgBoxErr(strError)
          edValelab.Text = "0"
          lbXx_Valelab.Text = ""
          edCambioelab.Text = "1"
          edCambioelab.Enabled = False
        Else
          GctlSetVisEnab(edCambioelab, False)
          oCleElar.CercaCambioDiOggi(NTSCInt(edValelab.Text), edDatagg.Text, edCambioelab.Text)
          lbXx_Valelab.Text = strTmp
        End If
      Else
        edValelab.Text = "0"
        lbXx_Valelab.Text = ""
        edCambioelab.Text = "1"
        edCambioelab.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValpar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edValpar.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not edValpar.Text = "0" Then
        If Not oCleElar.edValpar_Validated(NTSCInt(edValpar.Text), strTmp, strError) Then
          oApp.MsgBoxErr(strError)
          edValpar.Text = "0"
          lbXx_Valpar.Text = ""
          edCambiopar.Text = "1"
          edCambiopar.Enabled = False
        Else
          GctlSetVisEnab(edCambiopar, False)
          oCleElar.CercaCambioDiOggi(NTSCInt(edValpar.Text), edDatagg.Text, edCambiopar.Text)
          lbXx_Valpar.Text = strTmp
        End If
      Else
        edValpar.Text = "0"
        lbXx_Valpar.Text = ""
        edCambiopar.Text = "1"
        edCambiopar.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPromo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edPromo.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodtpro_Validated(NTSCInt(edPromo.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edPromo.Text = "0"
        lbDesPromo.Text = ""
      Else
        lbDesPromo.Text = strTmp
      End If

      oCleElar.lPromo = NTSCInt(edPromo.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodlsar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsar.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleElar.edCodlsar_Validated(NTSCInt(edCodlsar.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodlsar.Text = "0"
        lbDescodlsar.Text = ""
      Else
        lbDescodlsar.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbTipoElaborazione_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoElaborazione.SelectedValueChanged
    Try
      Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
        Case 1 : opGenerico_CheckedChanged(Nothing, Nothing)
        Case 2 : opClienti_CheckedChanged(Nothing, Nothing)
        Case 3 : opFornitori_CheckedChanged(Nothing, Nothing)
        Case 4 : opGenericoDaClienti_CheckedChanged(Nothing, Nothing)
        Case 5 : opGenericoDaFornitori_CheckedChanged(Nothing, Nothing)
        Case 6 : CampiSpecialeClienteDaGenerico()
        Case 7 : CampiSpecialeFornitoreDaGenerico()
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi OptionButton"
  Public Overridable Sub opClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      lbListpar.Enabled = False
      lbListelab.Enabled = False
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 128450577437473574, "Cliente")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      edListpar.Enabled = False
      edListelab.Text = "1"
      edListelab.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      If opRicarico.Checked = True Then opPercentuale.Checked = True
      opRicarico.Checked = False
      opRicarico.Enabled = False
      ckAggiornalistini.Checked = False
      ckAggiornalistini.Visible = False
      ckNetto.Enabled = False
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opFornitori_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      lbListpar.Enabled = False
      lbListelab.Enabled = False
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 128686435287556449, "Fornitore")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      edListpar.Enabled = False
      edListelab.Text = "1"
      edListelab.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      If opRicarico.Checked = True Then opPercentuale.Checked = True
      opRicarico.Checked = False
      opRicarico.Enabled = False
      ckAggiornalistini.Checked = False
      ckAggiornalistini.Visible = False
      ckNetto.Enabled = False
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opGenerico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      GctlSetVisEnab(lbListpar, False)
      GctlSetVisEnab(lbListelab, False)
      lbConto.Text = oApp.Tr(Me, 128686435412088589, "Conto")
      lbConto.Enabled = False
      GctlSetVisEnab(lbListpar1, False)
      GctlSetVisEnab(lbListpar2, False)
      GctlSetVisEnab(lbListpar3, False)
      GctlSetVisEnab(edListpar, False)
      GctlSetVisEnab(edListelab, False)
      edConto.Text = "0"
      edConto.Enabled = False
      lbXx_Conto.Text = ""
      edCoddest.Text = "-1"
      lbDescoddest.Text = ""
      edCoddest.Enabled = False
      GctlSetVisEnab(opRicarico, False)
      opPercentuale.Checked = True
      GctlSetVisEnab(ckAggiornalistini, True)
      ckNetto.Enabled = False
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opGenericoDaClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      lbListpar.Enabled = False
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 128686435797032049, "Cliente")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      edListpar.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      GctlSetVisEnab(lbListelab, False)
      GctlSetVisEnab(edListelab, False)
      GctlSetVisEnab(ckAggiornalistini, True)
      GctlSetVisEnab(opRicarico, False)
      GctlSetVisEnab(ckNetto, False)
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opGenericoDaFornitori_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      '---------------------------------------------------------------------------------------
      lbListpar.Enabled = True
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 130372807711481724, "Fornitore")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      edListpar.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      GctlSetVisEnab(lbListelab, False)
      GctlSetVisEnab(edListelab, False)
      GctlSetVisEnab(ckAggiornalistini, True)
      GctlSetVisEnab(opRicarico, False)
      GctlSetVisEnab(ckNetto, False)
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opPercentuale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opPercentuale.CheckedChanged
    Try
      If opPercentuale.Checked = True Then
        GctlSetVisEnab(lbArrotond, False)
        GctlSetVisEnab(lbRicar1, False)
        GctlSetVisEnab(lbRicar2, False)
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        GctlSetVisEnab(edArrotond, False)
        GctlSetVisEnab(edRicar1, False)
        GctlSetVisEnab(edRicar2, False)
        edValore.Text = "0"
        edValore.Enabled = False
        cbAppMis.Enabled = False
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opQualsiasiFase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opQualsiasiFase.CheckedChanged
    Try
      If opQualsiasiFase.Checked = True Then
        If edFase.Visible = True Then
          edFase.Text = "0"
          edFase.Enabled = False
          edFase.Visible = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opRicarico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opRicarico.CheckedChanged
    Try
      If opRicarico.Checked = True Then
        GctlSetVisEnab(lbArrotond, False)
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        GctlSetVisEnab(edArrotond, False)
        edRicar1.Text = "0"
        edRicar1.Enabled = False
        edRicar2.Text = "0"
        edRicar2.Enabled = False
        edValore.Text = "0"
        edValore.Enabled = False
        cbAppMis.Enabled = False
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opScorporaIva_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opScorporaIva.CheckedChanged
    Try
      If opScorporaIva.Checked = True Then
        edRicar1.Text = "0"
        edRicar2.Text = "0"
        edValore.Text = "0"
        cbAppMis.SelectedValue = "P"
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        edRicar1.Enabled = False
        edRicar2.Enabled = False
        edValore.Enabled = False
        cbAppMis.Enabled = False
        GctlSetVisEnab(lbArrotond, False)
        GctlSetVisEnab(edArrotond, False)
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        ckAggiungiIVA.Checked = False
        ckAggiungiIVA.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opSoloFase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opSoloFase.CheckedChanged
    Try
      If opSoloFase.Checked = True Then
        If edFase.Visible = False Then
          GctlSetVisEnab(edFase, True)
          GctlSetVisEnab(edFase, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opUltimaFase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opUltimaFase.CheckedChanged
    Try
      If opUltimaFase.Checked = True Then
        If edFase.Visible = True Then
          edFase.Text = "0"
          edFase.Enabled = False
          edFase.Visible = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opValore_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValore.CheckedChanged
    Try
      If opValore.Checked = True Then
        lbArrotond.Enabled = False
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbAppMis.Enabled = True
        GctlSetVisEnab(lbValore, False)
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        edArrotond.Enabled = False
        edRicar1.Text = "0"
        edRicar1.Enabled = False
        edRicar2.Text = "0"
        edRicar2.Enabled = False
        GctlSetVisEnab(edValore, False)
        GctlSetVisEnab(cbAppMis, False)
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opPromozioni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opNoPromo.CheckedChanged, opPromoSpecifica.CheckedChanged, opTuttePromo.CheckedChanged
    Try
      If opPromoSpecifica.Checked Then
        edPromo.Enabled = True
        oCleElar.strTipoPromo = "S"
        edCodtpro.Enabled = True
      ElseIf opNoPromo.Checked Then
        edPromo.Enabled = False
        oCleElar.strTipoPromo = "N"
        edCodtpro.Enabled = True
      Else
        edPromo.Enabled = False
        oCleElar.strTipoPromo = "T"
        edCodtpro.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckNetto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNetto.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (ckNetto.Enabled = True) And _
         (ckNetto.Checked = True) And _
         (ckScontiDaCondComm.Enabled = True) And _
         (ckScontiDaCondComm.Checked = True) Then ckScontiDaCondComm.Checked = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckScontiDaCondComm_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckScontiDaCondComm.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (ckScontiDaCondComm.Enabled = True) And _
         (ckScontiDaCondComm.Checked = True) And _
         (ckNetto.Enabled = True) And _
         (ckNetto.Checked = True) Then ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub CampiSpecialeClienteDaGenerico()
    Try
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 130372807679968794, "Cliente")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      lbListelab.Enabled = False
      edListelab.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      GctlSetVisEnab(lbListpar, False)
      GctlSetVisEnab(edListpar, False)
      ckAggiornalistini.Visible = False
      opRicarico.Enabled = False
      GctlSetVisEnab(ckNetto, False)
      ckNetto.Enabled = False
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CampiSpecialeFornitoreDaGenerico()
    Try
      '---------------------------------------------------------------------------------------
      GctlSetVisEnab(lbConto, False)
      lbConto.Text = oApp.Tr(Me, 130372807582684864, "Fornitore")
      lbListpar1.Enabled = False
      lbListpar2.Enabled = False
      lbListpar3.Enabled = False
      edListpar.Text = "1"
      lbListelab.Enabled = False
      edListelab.Enabled = False
      GctlSetVisEnab(edConto, False)
      GctlSetVisEnab(edCoddest, False)
      GctlSetVisEnab(lbListpar, False)
      GctlSetVisEnab(edListpar, False)
      ckAggiornalistini.Visible = False
      opRicarico.Enabled = False
      GctlSetVisEnab(ckNetto, False)
      ckNetto.Enabled = False
      ckNetto.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaScontiDaCondComm(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttAppMis As New DataTable()

      dttAppMis.Columns.Add("cod", GetType(String))
      dttAppMis.Columns.Add("val", GetType(String))
      dttAppMis.Rows.Add(New Object() {"P", "Principale"})
      dttAppMis.Rows.Add(New Object() {"C", "Confezione"})
      dttAppMis.Rows.Add(New Object() {"S", "Secondaria"})
      dttAppMis.AcceptChanges()
      cbAppMis.DataSource = dttAppMis
      cbAppMis.ValueMember = "cod"
      cbAppMis.DisplayMember = "val"


      Dim dttTipoElaborazione As New DataTable()
      dttTipoElaborazione.Columns.Add("cod", GetType(Integer))
      dttTipoElaborazione.Columns.Add("val", GetType(String))
      dttTipoElaborazione.Rows.Add(New Object() {1, "Listino Generico"})
      dttTipoElaborazione.Rows.Add(New Object() {2, "Speciale Clienti"})
      dttTipoElaborazione.Rows.Add(New Object() {3, "Speciale Fornitori"})
      dttTipoElaborazione.Rows.Add(New Object() {4, "Generico da Listino Clienti"})
      dttTipoElaborazione.Rows.Add(New Object() {5, "Generico da Listino Fornitori"})
      dttTipoElaborazione.Rows.Add(New Object() {6, "Speciale Clienti da Listino Generico"})
      dttTipoElaborazione.Rows.Add(New Object() {7, "Speciale Fornitori da Listino Generico"})
      dttTipoElaborazione.AcceptChanges()
      cbTipoElaborazione.DataSource = dttTipoElaborazione
      cbTipoElaborazione.ValueMember = "cod"
      cbTipoElaborazione.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "STATUSBAR:"
            lbStatus.Text = e.Message
            Me.Refresh()
          Case "SALTALIST:"
            Salta()
            Me.Refresh()
          Case "VISRISULT:"
            visRisult()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salta() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmInea As FRMMGINEA = Nothing
    Try

      frmInea = CType(NTSNewFormModal("FRMMGINEA"), FRMMGINEA)
      frmInea.nIneaRichiesta = 0
      frmInea.Init(oMenu, oParam, DittaCorrente)
      frmInea.InitEntity(oCleElar)
      frmInea.ShowDialog()

      oCleElar.nRichiesta = frmInea.nIneaRichiesta

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmInea Is Nothing Then frmInea.Dispose()
      frmInea = Nothing
    End Try
  End Function

  Public Overridable Function visRisult() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmVris As FRMMGVRIS = Nothing
    Try

      frmVris = CType(NTSNewFormModal("FRMMGVRIS"), FRMMGVRIS)
      frmVris.Init(oMenu, oParam, DittaCorrente)
      frmVris.InitEntity(oCleElar)
      frmVris.ShowDialog()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmVris Is Nothing Then frmVris.Dispose()
      frmVris = Nothing
    End Try
  End Function

  Public Overridable Sub AbilitaDisabilitaScontiDaCondComm(ByVal bAbilita As Boolean)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ckScontiDaCondComm.Enabled = bAbilita
      If bAbilita = False Then
        ckScontiDaCondComm.Checked = False
      Else
        ckScontiDaCondComm.Checked = CBool(oMenu.GetSettingBus("BSMGELAR", "RECENT", ".", "ScontiDaCondComm", "-1", " ", "-1"))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
