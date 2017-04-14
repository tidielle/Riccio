Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSCHO
  Public oCleScho As CLEORSCHO
  Public oCallParams As CLE__CLDP

  Public dttCampi As New DataTable          'elenco campi filtrabili di MOVORD
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource
  Public strProntoMess As String = ""
  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri

#Region "Moduli"
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents ckSalto As NTSInformatica.NTSCheckBox
  Public WithEvents ckEvasi As NTSInformatica.NTSCheckBox
  Public WithEvents lbDadatord As NTSInformatica.NTSLabel
  Public WithEvents edDadatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edAagente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaagente As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDaagente As NTSInformatica.NTSLabel
  Public WithEvents edAmagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDamagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDamagaz As NTSInformatica.NTSLabel
  Public WithEvents edAcodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDacodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edFasefin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFaseini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommecafin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommecaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCommecaini As NTSInformatica.NTSLabel
  Public WithEvents edSottogr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edGruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbGruppo As NTSInformatica.NTSLabel
  Public WithEvents lbRilasciato As NTSInformatica.NTSLabel
  Public WithEvents cbRilasciato As NTSInformatica.NTSComboBox
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckTipork As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_Codcfam As NTSInformatica.NTSLabel
  Public WithEvents ckZzdispsca As NTSInformatica.NTSCheckBox
  Public WithEvents ckOrdlist As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovord1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovord As NTSInformatica.NTSCheckBox
  Public WithEvents ckSolotaskril As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovord4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTaskPM As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovord3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovord2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckRilasciato As NTSInformatica.NTSCheckBox
  Public WithEvents opTipoStampa1 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoStampa0 As NTSInformatica.NTSRadioButton
  Public WithEvents cbOrdin As NTSInformatica.NTSComboBox
  Public WithEvents fmTc As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_Codstag As NTSInformatica.NTSLabel
  Public WithEvents lbCodstag As NTSInformatica.NTSLabel
  Public WithEvents edAnnotco As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnotco As NTSInformatica.NTSLabel
  Public WithEvents ckSelAnnoStag As NTSInformatica.NTSCheckBox
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaWordRaggr As NTSInformatica.NTSBarMenuItem
  Public WithEvents edCodstag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDadatcons As NTSInformatica.NTSLabel
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORSCHO", "BEORSCHO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleScho = CType(oTmp, CLEORSCHO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORSCHO", strRemoteServer, strRemotePort)
    AddHandler oCleScho.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleScho.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORSCHO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbNoModal = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWordRaggr = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.ckEvasi = New NTSInformatica.NTSCheckBox
    Me.ckSalto = New NTSInformatica.NTSCheckBox
    Me.lbDamagaz = New NTSInformatica.NTSLabel
    Me.edDamagaz = New NTSInformatica.NTSTextBoxNum
    Me.edAmagaz = New NTSInformatica.NTSTextBoxNum
    Me.edDaagente = New NTSInformatica.NTSTextBoxNum
    Me.lbDaagente = New NTSInformatica.NTSLabel
    Me.edAagente = New NTSInformatica.NTSTextBoxNum
    Me.edDadatord = New NTSInformatica.NTSTextBoxData
    Me.lbDadatord = New NTSInformatica.NTSLabel
    Me.edAdatord = New NTSInformatica.NTSTextBoxData
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.edDacodart = New NTSInformatica.NTSTextBoxStr
    Me.edAcodart = New NTSInformatica.NTSTextBoxStr
    Me.edFasefin = New NTSInformatica.NTSTextBoxNum
    Me.edFaseini = New NTSInformatica.NTSTextBoxNum
    Me.edCommecafin = New NTSInformatica.NTSTextBoxNum
    Me.edCommecaini = New NTSInformatica.NTSTextBoxNum
    Me.lbCommecaini = New NTSInformatica.NTSLabel
    Me.edGruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbGruppo = New NTSInformatica.NTSLabel
    Me.edSottogr = New NTSInformatica.NTSTextBoxNum
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.cbRilasciato = New NTSInformatica.NTSComboBox
    Me.lbRilasciato = New NTSInformatica.NTSLabel
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.ckTipork = New NTSInformatica.NTSCheckBox
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_Codcfam = New NTSInformatica.NTSLabel
    Me.ckSolotaskril = New NTSInformatica.NTSCheckBox
    Me.ckMovord4 = New NTSInformatica.NTSCheckBox
    Me.ckTaskPM = New NTSInformatica.NTSCheckBox
    Me.ckMovord3 = New NTSInformatica.NTSCheckBox
    Me.ckMovord2 = New NTSInformatica.NTSCheckBox
    Me.ckZzdispsca = New NTSInformatica.NTSCheckBox
    Me.ckOrdlist = New NTSInformatica.NTSCheckBox
    Me.ckMovord1 = New NTSInformatica.NTSCheckBox
    Me.ckMovord = New NTSInformatica.NTSCheckBox
    Me.ckRilasciato = New NTSInformatica.NTSCheckBox
    Me.cbOrdin = New NTSInformatica.NTSComboBox
    Me.opTipoStampa1 = New NTSInformatica.NTSRadioButton
    Me.opTipoStampa0 = New NTSInformatica.NTSRadioButton
    Me.fmTc = New NTSInformatica.NTSGroupBox
    Me.edCodstag = New NTSInformatica.NTSTextBoxNum
    Me.ckSelAnnoStag = New NTSInformatica.NTSCheckBox
    Me.lbXx_Codstag = New NTSInformatica.NTSLabel
    Me.lbCodstag = New NTSInformatica.NTSLabel
    Me.edAnnotco = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnotco = New NTSInformatica.NTSLabel
    Me.lbDadatcons = New NTSInformatica.NTSLabel
    Me.tsScho = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.lbOrdinamento = New NTSInformatica.NTSLabel
    Me.pnFilterSx = New NTSInformatica.NTSPanel
    Me.lbDescodlsel = New NTSInformatica.NTSLabel
    Me.edCodlsel = New NTSInformatica.NTSTextBoxNum
    Me.cbConto = New NTSInformatica.NTSComboBox
    Me.lbDescodlsar = New NTSInformatica.NTSLabel
    Me.edCodlsar = New NTSInformatica.NTSTextBoxNum
    Me.cbCodart = New NTSInformatica.NTSComboBox
    Me.cmdClassificaDeleteFilter = New NTSInformatica.NTSButton
    Me.cmdClassifica = New NTSInformatica.NTSButton
    Me.lbClassifica = New NTSInformatica.NTSLabel
    Me.edClassificazioneLivello5 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello4 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello3 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello2 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello1 = New NTSInformatica.NTSTextBoxStr
    Me.lbVert = New NTSInformatica.NTSLabel
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.lbFaseini = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    Me.pnFiltri2 = New NTSInformatica.NTSPanel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPianificazione = New NTSInformatica.NTSPanel
    Me.NtsGridColumn1 = New NTSInformatica.NTSGridColumn
    Me.NtsGridColumn2 = New NTSInformatica.NTSGridColumn
    Me.NtsGridColumn3 = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnTipoStampa = New NTSInformatica.NTSPanel
    Me.lbTipoStampa = New NTSInformatica.NTSLabel
    Me.cmdApriFiltri = New NTSInformatica.NTSButton
    Me.cbFiltro = New NTSInformatica.NTSComboBox
    Me.lbFiltri = New NTSInformatica.NTSLabel
    Me.tlbAccorpa = New NTSInformatica.NTSBarMenuItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEvasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaagente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAagente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFasefin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFaseini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbRilasciato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSolotaskril.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovord4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTaskPM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovord3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovord2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckZzdispsca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOrdlist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovord1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRilasciato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOrdin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTc.SuspendLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsScho, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsScho.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.pnFilterSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFilterSx.SuspendLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri2.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPianificazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPianificazione.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTipoStampa.SuspendLayout()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbStampaWord, Me.tlbStampaWordRaggr, Me.tlbNoModal, Me.tlbAccorpa})
    Me.NtsBarManager1.MaxItemId = 23
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoModal), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAccorpa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWordRaggr)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbNoModal
    '
    Me.tlbNoModal.Caption = "Apri griglia in modalità NON modale"
    Me.tlbNoModal.GlyphPath = ""
    Me.tlbNoModal.Id = 21
    Me.tlbNoModal.Name = "tlbNoModal"
    Me.tlbNoModal.NTSIsCheckBox = True
    Me.tlbNoModal.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampaWordRaggr
    '
    Me.tlbStampaWordRaggr.Caption = "Stampa Word raggruppata"
    Me.tlbStampaWordRaggr.GlyphPath = ""
    Me.tlbStampaWordRaggr.Id = 20
    Me.tlbStampaWordRaggr.Name = "tlbStampaWordRaggr"
    Me.tlbStampaWordRaggr.NTSIsCheckBox = False
    Me.tlbStampaWordRaggr.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.GlyphPath = ""
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.GlyphPath = ""
    Me.tlbStampaWord.Id = 18
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
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
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(9, 378)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 34
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'ckEvasi
    '
    Me.ckEvasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEvasi.Location = New System.Drawing.Point(445, 34)
    Me.ckEvasi.Name = "ckEvasi"
    Me.ckEvasi.NTSCheckValue = "S"
    Me.ckEvasi.NTSUnCheckValue = "N"
    Me.ckEvasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEvasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckEvasi.Properties.AutoHeight = False
    Me.ckEvasi.Properties.Caption = "Consid. solo le righe non evase"
    Me.ckEvasi.Size = New System.Drawing.Size(233, 19)
    Me.ckEvasi.TabIndex = 35
    '
    'ckSalto
    '
    Me.ckSalto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalto.Location = New System.Drawing.Point(445, 60)
    Me.ckSalto.Name = "ckSalto"
    Me.ckSalto.NTSCheckValue = "S"
    Me.ckSalto.NTSUnCheckValue = "N"
    Me.ckSalto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalto.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalto.Properties.AutoHeight = False
    Me.ckSalto.Properties.Caption = "&Salto pagina per ogni articolo"
    Me.ckSalto.Size = New System.Drawing.Size(233, 19)
    Me.ckSalto.TabIndex = 36
    '
    'lbDamagaz
    '
    Me.lbDamagaz.AutoSize = True
    Me.lbDamagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDamagaz.Location = New System.Drawing.Point(3, 7)
    Me.lbDamagaz.Name = "lbDamagaz"
    Me.lbDamagaz.NTSDbField = ""
    Me.lbDamagaz.Size = New System.Drawing.Size(88, 13)
    Me.lbDamagaz.TabIndex = 37
    Me.lbDamagaz.Text = "Magazzino DA /A"
    Me.lbDamagaz.Tooltip = ""
    Me.lbDamagaz.UseMnemonic = False
    '
    'edDamagaz
    '
    Me.edDamagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDamagaz.EditValue = "0"
    Me.edDamagaz.Location = New System.Drawing.Point(128, 4)
    Me.edDamagaz.Name = "edDamagaz"
    Me.edDamagaz.NTSDbField = ""
    Me.edDamagaz.NTSFormat = "0"
    Me.edDamagaz.NTSForzaVisZoom = False
    Me.edDamagaz.NTSOldValue = ""
    Me.edDamagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edDamagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDamagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamagaz.Properties.AutoHeight = False
    Me.edDamagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamagaz.Properties.MaxLength = 65536
    Me.edDamagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamagaz.Size = New System.Drawing.Size(84, 20)
    Me.edDamagaz.TabIndex = 38
    '
    'edAmagaz
    '
    Me.edAmagaz.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAmagaz.EditValue = "0"
    Me.edAmagaz.Location = New System.Drawing.Point(280, 4)
    Me.edAmagaz.Name = "edAmagaz"
    Me.edAmagaz.NTSDbField = ""
    Me.edAmagaz.NTSFormat = "0"
    Me.edAmagaz.NTSForzaVisZoom = False
    Me.edAmagaz.NTSOldValue = ""
    Me.edAmagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edAmagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAmagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAmagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAmagaz.Properties.AutoHeight = False
    Me.edAmagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAmagaz.Properties.MaxLength = 65536
    Me.edAmagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAmagaz.Size = New System.Drawing.Size(84, 20)
    Me.edAmagaz.TabIndex = 39
    '
    'edDaagente
    '
    Me.edDaagente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaagente.EditValue = "0"
    Me.edDaagente.Location = New System.Drawing.Point(128, 30)
    Me.edDaagente.Name = "edDaagente"
    Me.edDaagente.NTSDbField = ""
    Me.edDaagente.NTSFormat = "0"
    Me.edDaagente.NTSForzaVisZoom = False
    Me.edDaagente.NTSOldValue = ""
    Me.edDaagente.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaagente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaagente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaagente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaagente.Properties.AutoHeight = False
    Me.edDaagente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaagente.Properties.MaxLength = 65536
    Me.edDaagente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaagente.Size = New System.Drawing.Size(84, 20)
    Me.edDaagente.TabIndex = 42
    '
    'lbDaagente
    '
    Me.lbDaagente.AutoSize = True
    Me.lbDaagente.BackColor = System.Drawing.Color.Transparent
    Me.lbDaagente.Location = New System.Drawing.Point(3, 33)
    Me.lbDaagente.Name = "lbDaagente"
    Me.lbDaagente.NTSDbField = ""
    Me.lbDaagente.Size = New System.Drawing.Size(76, 13)
    Me.lbDaagente.TabIndex = 41
    Me.lbDaagente.Text = "Agente DA / A"
    Me.lbDaagente.Tooltip = ""
    Me.lbDaagente.UseMnemonic = False
    '
    'edAagente
    '
    Me.edAagente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAagente.EditValue = "0"
    Me.edAagente.Location = New System.Drawing.Point(280, 30)
    Me.edAagente.Name = "edAagente"
    Me.edAagente.NTSDbField = ""
    Me.edAagente.NTSFormat = "0"
    Me.edAagente.NTSForzaVisZoom = False
    Me.edAagente.NTSOldValue = ""
    Me.edAagente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAagente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAagente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAagente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAagente.Properties.AutoHeight = False
    Me.edAagente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAagente.Properties.MaxLength = 65536
    Me.edAagente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAagente.Size = New System.Drawing.Size(84, 20)
    Me.edAagente.TabIndex = 44
    '
    'edDadatord
    '
    Me.edDadatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatord.Location = New System.Drawing.Point(128, 56)
    Me.edDadatord.Name = "edDadatord"
    Me.edDadatord.NTSDbField = ""
    Me.edDadatord.NTSForzaVisZoom = False
    Me.edDadatord.NTSOldValue = ""
    Me.edDadatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatord.Properties.AutoHeight = False
    Me.edDadatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatord.Properties.MaxLength = 65536
    Me.edDadatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatord.Size = New System.Drawing.Size(84, 20)
    Me.edDadatord.TabIndex = 45
    '
    'lbDadatord
    '
    Me.lbDadatord.AutoSize = True
    Me.lbDadatord.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatord.Location = New System.Drawing.Point(3, 59)
    Me.lbDadatord.Name = "lbDadatord"
    Me.lbDadatord.NTSDbField = ""
    Me.lbDadatord.Size = New System.Drawing.Size(97, 13)
    Me.lbDadatord.TabIndex = 46
    Me.lbDadatord.Text = "Data ordine DA / A"
    Me.lbDadatord.Tooltip = ""
    Me.lbDadatord.UseMnemonic = False
    '
    'edAdatord
    '
    Me.edAdatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatord.Location = New System.Drawing.Point(280, 56)
    Me.edAdatord.Name = "edAdatord"
    Me.edAdatord.NTSDbField = ""
    Me.edAdatord.NTSForzaVisZoom = False
    Me.edAdatord.NTSOldValue = ""
    Me.edAdatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatord.Properties.AutoHeight = False
    Me.edAdatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatord.Properties.MaxLength = 65536
    Me.edAdatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatord.Size = New System.Drawing.Size(84, 20)
    Me.edAdatord.TabIndex = 47
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatcons.Location = New System.Drawing.Point(280, 82)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.AutoHeight = False
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(84, 20)
    Me.edAdatcons.TabIndex = 51
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatcons.Location = New System.Drawing.Point(128, 82)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.AutoHeight = False
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(84, 20)
    Me.edDadatcons.TabIndex = 49
    '
    'edAconto
    '
    Me.edAconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAconto.EditValue = "0"
    Me.edAconto.Location = New System.Drawing.Point(280, 108)
    Me.edAconto.Name = "edAconto"
    Me.edAconto.NTSDbField = ""
    Me.edAconto.NTSFormat = "0"
    Me.edAconto.NTSForzaVisZoom = False
    Me.edAconto.NTSOldValue = ""
    Me.edAconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAconto.Properties.AutoHeight = False
    Me.edAconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAconto.Properties.MaxLength = 65536
    Me.edAconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAconto.Size = New System.Drawing.Size(84, 20)
    Me.edAconto.TabIndex = 55
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(129, 108)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.AutoHeight = False
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(83, 20)
    Me.edDaconto.TabIndex = 54
    '
    'edDacodart
    '
    Me.edDacodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacodart.Location = New System.Drawing.Point(128, 134)
    Me.edDacodart.Name = "edDacodart"
    Me.edDacodart.NTSDbField = ""
    Me.edDacodart.NTSForzaVisZoom = False
    Me.edDacodart.NTSOldValue = ""
    Me.edDacodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDacodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDacodart.Properties.AutoHeight = False
    Me.edDacodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDacodart.Properties.MaxLength = 65536
    Me.edDacodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDacodart.Size = New System.Drawing.Size(148, 20)
    Me.edDacodart.TabIndex = 57
    '
    'edAcodart
    '
    Me.edAcodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcodart.Location = New System.Drawing.Point(280, 134)
    Me.edAcodart.Name = "edAcodart"
    Me.edAcodart.NTSDbField = ""
    Me.edAcodart.NTSForzaVisZoom = False
    Me.edAcodart.NTSOldValue = ""
    Me.edAcodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAcodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAcodart.Properties.AutoHeight = False
    Me.edAcodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcodart.Properties.MaxLength = 65536
    Me.edAcodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcodart.Size = New System.Drawing.Size(148, 20)
    Me.edAcodart.TabIndex = 60
    '
    'edFasefin
    '
    Me.edFasefin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFasefin.EditValue = "0"
    Me.edFasefin.Location = New System.Drawing.Point(280, 160)
    Me.edFasefin.Name = "edFasefin"
    Me.edFasefin.NTSDbField = ""
    Me.edFasefin.NTSFormat = "0"
    Me.edFasefin.NTSForzaVisZoom = False
    Me.edFasefin.NTSOldValue = ""
    Me.edFasefin.Properties.Appearance.Options.UseTextOptions = True
    Me.edFasefin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFasefin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFasefin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFasefin.Properties.AutoHeight = False
    Me.edFasefin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFasefin.Properties.MaxLength = 65536
    Me.edFasefin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFasefin.Size = New System.Drawing.Size(84, 20)
    Me.edFasefin.TabIndex = 68
    '
    'edFaseini
    '
    Me.edFaseini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFaseini.EditValue = "0"
    Me.edFaseini.Location = New System.Drawing.Point(129, 160)
    Me.edFaseini.Name = "edFaseini"
    Me.edFaseini.NTSDbField = ""
    Me.edFaseini.NTSFormat = "0"
    Me.edFaseini.NTSForzaVisZoom = False
    Me.edFaseini.NTSOldValue = ""
    Me.edFaseini.Properties.Appearance.Options.UseTextOptions = True
    Me.edFaseini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFaseini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFaseini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFaseini.Properties.AutoHeight = False
    Me.edFaseini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFaseini.Properties.MaxLength = 65536
    Me.edFaseini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFaseini.Size = New System.Drawing.Size(83, 20)
    Me.edFaseini.TabIndex = 66
    '
    'edCommecafin
    '
    Me.edCommecafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecafin.EditValue = "0"
    Me.edCommecafin.Location = New System.Drawing.Point(280, 186)
    Me.edCommecafin.Name = "edCommecafin"
    Me.edCommecafin.NTSDbField = ""
    Me.edCommecafin.NTSFormat = "0"
    Me.edCommecafin.NTSForzaVisZoom = False
    Me.edCommecafin.NTSOldValue = ""
    Me.edCommecafin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecafin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecafin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecafin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecafin.Properties.AutoHeight = False
    Me.edCommecafin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecafin.Properties.MaxLength = 65536
    Me.edCommecafin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecafin.Size = New System.Drawing.Size(84, 20)
    Me.edCommecafin.TabIndex = 63
    '
    'edCommecaini
    '
    Me.edCommecaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecaini.EditValue = "0"
    Me.edCommecaini.Location = New System.Drawing.Point(129, 186)
    Me.edCommecaini.Name = "edCommecaini"
    Me.edCommecaini.NTSDbField = ""
    Me.edCommecaini.NTSFormat = "0"
    Me.edCommecaini.NTSForzaVisZoom = False
    Me.edCommecaini.NTSOldValue = ""
    Me.edCommecaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecaini.Properties.AutoHeight = False
    Me.edCommecaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecaini.Properties.MaxLength = 65536
    Me.edCommecaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecaini.Size = New System.Drawing.Size(83, 20)
    Me.edCommecaini.TabIndex = 62
    '
    'lbCommecaini
    '
    Me.lbCommecaini.AutoSize = True
    Me.lbCommecaini.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaini.Location = New System.Drawing.Point(3, 189)
    Me.lbCommecaini.Name = "lbCommecaini"
    Me.lbCommecaini.NTSDbField = ""
    Me.lbCommecaini.Size = New System.Drawing.Size(92, 13)
    Me.lbCommecaini.TabIndex = 61
    Me.lbCommecaini.Text = "Commessa DA / A"
    Me.lbCommecaini.Tooltip = ""
    Me.lbCommecaini.UseMnemonic = False
    '
    'edGruppo
    '
    Me.edGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edGruppo.EditValue = "0"
    Me.edGruppo.Location = New System.Drawing.Point(129, 212)
    Me.edGruppo.Name = "edGruppo"
    Me.edGruppo.NTSDbField = ""
    Me.edGruppo.NTSFormat = "0"
    Me.edGruppo.NTSForzaVisZoom = False
    Me.edGruppo.NTSOldValue = ""
    Me.edGruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edGruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edGruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppo.Properties.AutoHeight = False
    Me.edGruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edGruppo.Properties.MaxLength = 65536
    Me.edGruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppo.Size = New System.Drawing.Size(83, 20)
    Me.edGruppo.TabIndex = 70
    '
    'lbGruppo
    '
    Me.lbGruppo.AutoSize = True
    Me.lbGruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppo.Location = New System.Drawing.Point(3, 215)
    Me.lbGruppo.Name = "lbGruppo"
    Me.lbGruppo.NTSDbField = ""
    Me.lbGruppo.Size = New System.Drawing.Size(106, 13)
    Me.lbGruppo.TabIndex = 69
    Me.lbGruppo.Text = "Gruppo/Sottogruppo"
    Me.lbGruppo.Tooltip = ""
    Me.lbGruppo.UseMnemonic = False
    '
    'edSottogr
    '
    Me.edSottogr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSottogr.EditValue = "0"
    Me.edSottogr.Location = New System.Drawing.Point(280, 212)
    Me.edSottogr.Name = "edSottogr"
    Me.edSottogr.NTSDbField = ""
    Me.edSottogr.NTSFormat = "0"
    Me.edSottogr.NTSForzaVisZoom = False
    Me.edSottogr.NTSOldValue = ""
    Me.edSottogr.Properties.Appearance.Options.UseTextOptions = True
    Me.edSottogr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSottogr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSottogr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSottogr.Properties.AutoHeight = False
    Me.edSottogr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSottogr.Properties.MaxLength = 65536
    Me.edSottogr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSottogr.Size = New System.Drawing.Size(84, 20)
    Me.edSottogr.TabIndex = 71
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(332, 341)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 73
    '
    'cbRilasciato
    '
    Me.cbRilasciato.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbRilasciato.DataSource = Nothing
    Me.cbRilasciato.DisplayMember = ""
    Me.cbRilasciato.Location = New System.Drawing.Point(129, 289)
    Me.cbRilasciato.Name = "cbRilasciato"
    Me.cbRilasciato.NTSDbField = ""
    Me.cbRilasciato.Properties.AutoHeight = False
    Me.cbRilasciato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbRilasciato.Properties.DropDownRows = 30
    Me.cbRilasciato.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbRilasciato.SelectedValue = ""
    Me.cbRilasciato.Size = New System.Drawing.Size(147, 20)
    Me.cbRilasciato.TabIndex = 75
    Me.cbRilasciato.ValueMember = ""
    '
    'lbRilasciato
    '
    Me.lbRilasciato.AutoSize = True
    Me.lbRilasciato.BackColor = System.Drawing.Color.Transparent
    Me.lbRilasciato.Location = New System.Drawing.Point(3, 292)
    Me.lbRilasciato.Name = "lbRilasciato"
    Me.lbRilasciato.NTSDbField = ""
    Me.lbRilasciato.Size = New System.Drawing.Size(56, 13)
    Me.lbRilasciato.TabIndex = 76
    Me.lbRilasciato.Text = "Tipo ordini"
    Me.lbRilasciato.Tooltip = ""
    Me.lbRilasciato.UseMnemonic = False
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(128, 341)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.AutoHeight = False
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(142, 20)
    Me.cbTipork.TabIndex = 77
    Me.cbTipork.ValueMember = ""
    '
    'ckTipork
    '
    Me.ckTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipork.Location = New System.Drawing.Point(3, 342)
    Me.ckTipork.Name = "ckTipork"
    Me.ckTipork.NTSCheckValue = "S"
    Me.ckTipork.NTSUnCheckValue = "N"
    Me.ckTipork.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipork.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipork.Properties.AutoHeight = False
    Me.ckTipork.Properties.Caption = "Tipo &Documento"
    Me.ckTipork.Size = New System.Drawing.Size(119, 19)
    Me.ckTipork.TabIndex = 78
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(3, 318)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(110, 13)
    Me.lbCodcfam.TabIndex = 80
    Me.lbCodcfam.Text = "Linea/famiglia articolo"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(129, 315)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.AutoHeight = False
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(78, 20)
    Me.edCodcfam.TabIndex = 79
    '
    'lbXx_Codcfam
    '
    Me.lbXx_Codcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codcfam.Location = New System.Drawing.Point(213, 315)
    Me.lbXx_Codcfam.Name = "lbXx_Codcfam"
    Me.lbXx_Codcfam.NTSDbField = ""
    Me.lbXx_Codcfam.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_Codcfam.TabIndex = 81
    Me.lbXx_Codcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codcfam.Tooltip = ""
    Me.lbXx_Codcfam.UseMnemonic = False
    '
    'ckSolotaskril
    '
    Me.ckSolotaskril.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSolotaskril.Location = New System.Drawing.Point(9, 98)
    Me.ckSolotaskril.Name = "ckSolotaskril"
    Me.ckSolotaskril.NTSCheckValue = "S"
    Me.ckSolotaskril.NTSUnCheckValue = "N"
    Me.ckSolotaskril.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSolotaskril.Properties.Appearance.Options.UseBackColor = True
    Me.ckSolotaskril.Properties.AutoHeight = False
    Me.ckSolotaskril.Properties.Caption = "Solo tasks rilasciati"
    Me.ckSolotaskril.Size = New System.Drawing.Size(159, 19)
    Me.ckSolotaskril.TabIndex = 45
    '
    'ckMovord4
    '
    Me.ckMovord4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMovord4.Location = New System.Drawing.Point(174, 63)
    Me.ckMovord4.Name = "ckMovord4"
    Me.ckMovord4.NTSCheckValue = "S"
    Me.ckMovord4.NTSUnCheckValue = "N"
    Me.ckMovord4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovord4.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovord4.Properties.AutoHeight = False
    Me.ckMovord4.Properties.Caption = "Impegni di commessa"
    Me.ckMovord4.Size = New System.Drawing.Size(158, 19)
    Me.ckMovord4.TabIndex = 44
    '
    'ckTaskPM
    '
    Me.ckTaskPM.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTaskPM.Location = New System.Drawing.Point(174, 46)
    Me.ckTaskPM.Name = "ckTaskPM"
    Me.ckTaskPM.NTSCheckValue = "S"
    Me.ckTaskPM.NTSUnCheckValue = "N"
    Me.ckTaskPM.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTaskPM.Properties.Appearance.Options.UseBackColor = True
    Me.ckTaskPM.Properties.AutoHeight = False
    Me.ckTaskPM.Properties.Caption = "Tasks Commesse "
    Me.ckTaskPM.Size = New System.Drawing.Size(158, 19)
    Me.ckTaskPM.TabIndex = 43
    '
    'ckMovord3
    '
    Me.ckMovord3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMovord3.Location = New System.Drawing.Point(174, 28)
    Me.ckMovord3.Name = "ckMovord3"
    Me.ckMovord3.NTSCheckValue = "S"
    Me.ckMovord3.NTSUnCheckValue = "N"
    Me.ckMovord3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovord3.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovord3.Properties.AutoHeight = False
    Me.ckMovord3.Properties.Caption = "Impe&gni Clienti aperti"
    Me.ckMovord3.Size = New System.Drawing.Size(158, 19)
    Me.ckMovord3.TabIndex = 42
    '
    'ckMovord2
    '
    Me.ckMovord2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMovord2.Location = New System.Drawing.Point(174, 12)
    Me.ckMovord2.Name = "ckMovord2"
    Me.ckMovord2.NTSCheckValue = "S"
    Me.ckMovord2.NTSUnCheckValue = "N"
    Me.ckMovord2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovord2.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovord2.Properties.AutoHeight = False
    Me.ckMovord2.Properties.Caption = "Ordini Fornitori ap&erti"
    Me.ckMovord2.Size = New System.Drawing.Size(158, 19)
    Me.ckMovord2.TabIndex = 41
    '
    'ckZzdispsca
    '
    Me.ckZzdispsca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckZzdispsca.Location = New System.Drawing.Point(9, 63)
    Me.ckZzdispsca.Name = "ckZzdispsca"
    Me.ckZzdispsca.NTSCheckValue = "S"
    Me.ckZzdispsca.NTSUnCheckValue = "N"
    Me.ckZzdispsca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckZzdispsca.Properties.Appearance.Options.UseBackColor = True
    Me.ckZzdispsca.Properties.AutoHeight = False
    Me.ckZzdispsca.Properties.Caption = "Fabbisogni primari E&MRP"
    Me.ckZzdispsca.Size = New System.Drawing.Size(159, 19)
    Me.ckZzdispsca.TabIndex = 40
    '
    'ckOrdlist
    '
    Me.ckOrdlist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdlist.Location = New System.Drawing.Point(9, 46)
    Me.ckOrdlist.Name = "ckOrdlist"
    Me.ckOrdlist.NTSCheckValue = "S"
    Me.ckOrdlist.NTSUnCheckValue = "N"
    Me.ckOrdlist.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdlist.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdlist.Properties.AutoHeight = False
    Me.ckOrdlist.Properties.Caption = "Proposte &d'Ordine"
    Me.ckOrdlist.Size = New System.Drawing.Size(159, 19)
    Me.ckOrdlist.TabIndex = 39
    '
    'ckMovord1
    '
    Me.ckMovord1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMovord1.Location = New System.Drawing.Point(9, 29)
    Me.ckMovord1.Name = "ckMovord1"
    Me.ckMovord1.NTSCheckValue = "S"
    Me.ckMovord1.NTSUnCheckValue = "N"
    Me.ckMovord1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovord1.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovord1.Properties.AutoHeight = False
    Me.ckMovord1.Properties.Caption = "A&ltri Ordini Impegni"
    Me.ckMovord1.Size = New System.Drawing.Size(159, 19)
    Me.ckMovord1.TabIndex = 38
    '
    'ckMovord
    '
    Me.ckMovord.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckMovord.Location = New System.Drawing.Point(9, 12)
    Me.ckMovord.Name = "ckMovord"
    Me.ckMovord.NTSCheckValue = "S"
    Me.ckMovord.NTSUnCheckValue = "N"
    Me.ckMovord.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovord.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovord.Properties.AutoHeight = False
    Me.ckMovord.Properties.Caption = "&Impegni Clienti"
    Me.ckMovord.Size = New System.Drawing.Size(159, 19)
    Me.ckMovord.TabIndex = 37
    '
    'ckRilasciato
    '
    Me.ckRilasciato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRilasciato.Location = New System.Drawing.Point(445, 84)
    Me.ckRilasciato.Name = "ckRilasciato"
    Me.ckRilasciato.NTSCheckValue = "S"
    Me.ckRilasciato.NTSUnCheckValue = "N"
    Me.ckRilasciato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRilasciato.Properties.Appearance.Options.UseBackColor = True
    Me.ckRilasciato.Properties.AutoHeight = False
    Me.ckRilasciato.Properties.Caption = "Aggiorna Status 'Rilasciato' su righe"
    Me.ckRilasciato.Size = New System.Drawing.Size(233, 19)
    Me.ckRilasciato.TabIndex = 46
    '
    'cbOrdin
    '
    Me.cbOrdin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOrdin.DataSource = Nothing
    Me.cbOrdin.DisplayMember = ""
    Me.cbOrdin.Location = New System.Drawing.Point(524, 7)
    Me.cbOrdin.Name = "cbOrdin"
    Me.cbOrdin.NTSDbField = ""
    Me.cbOrdin.Properties.AutoHeight = False
    Me.cbOrdin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOrdin.Properties.DropDownRows = 30
    Me.cbOrdin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOrdin.SelectedValue = ""
    Me.cbOrdin.Size = New System.Drawing.Size(154, 20)
    Me.cbOrdin.TabIndex = 0
    Me.cbOrdin.ValueMember = ""
    '
    'opTipoStampa1
    '
    Me.opTipoStampa1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa1.Location = New System.Drawing.Point(87, 3)
    Me.opTipoStampa1.Name = "opTipoStampa1"
    Me.opTipoStampa1.NTSCheckValue = "S"
    Me.opTipoStampa1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa1.Properties.AutoHeight = False
    Me.opTipoStampa1.Properties.Caption = "Completa"
    Me.opTipoStampa1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa1.Size = New System.Drawing.Size(75, 19)
    Me.opTipoStampa1.TabIndex = 1
    '
    'opTipoStampa0
    '
    Me.opTipoStampa0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa0.EditValue = True
    Me.opTipoStampa0.Location = New System.Drawing.Point(1, 3)
    Me.opTipoStampa0.Name = "opTipoStampa0"
    Me.opTipoStampa0.NTSCheckValue = "S"
    Me.opTipoStampa0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa0.Properties.AutoHeight = False
    Me.opTipoStampa0.Properties.Caption = "Ridotta"
    Me.opTipoStampa0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa0.Size = New System.Drawing.Size(65, 19)
    Me.opTipoStampa0.TabIndex = 0
    '
    'fmTc
    '
    Me.fmTc.AllowDrop = True
    Me.fmTc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTc.Appearance.Options.UseBackColor = True
    Me.fmTc.Controls.Add(Me.edCodstag)
    Me.fmTc.Controls.Add(Me.ckSelAnnoStag)
    Me.fmTc.Controls.Add(Me.lbXx_Codstag)
    Me.fmTc.Controls.Add(Me.lbCodstag)
    Me.fmTc.Controls.Add(Me.edAnnotco)
    Me.fmTc.Controls.Add(Me.lbAnnotco)
    Me.fmTc.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTc.Location = New System.Drawing.Point(443, 300)
    Me.fmTc.Name = "fmTc"
    Me.fmTc.Size = New System.Drawing.Size(235, 70)
    Me.fmTc.TabIndex = 86
    '
    'edCodstag
    '
    Me.edCodstag.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodstag.EditValue = "0"
    Me.edCodstag.Location = New System.Drawing.Point(67, 45)
    Me.edCodstag.Name = "edCodstag"
    Me.edCodstag.NTSDbField = ""
    Me.edCodstag.NTSFormat = "0"
    Me.edCodstag.NTSForzaVisZoom = False
    Me.edCodstag.NTSOldValue = ""
    Me.edCodstag.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodstag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodstag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodstag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodstag.Properties.AutoHeight = False
    Me.edCodstag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodstag.Properties.MaxLength = 65536
    Me.edCodstag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodstag.Size = New System.Drawing.Size(56, 20)
    Me.edCodstag.TabIndex = 84
    '
    'ckSelAnnoStag
    '
    Me.ckSelAnnoStag.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSelAnnoStag.Location = New System.Drawing.Point(5, 0)
    Me.ckSelAnnoStag.Name = "ckSelAnnoStag"
    Me.ckSelAnnoStag.NTSCheckValue = "S"
    Me.ckSelAnnoStag.NTSUnCheckValue = "N"
    Me.ckSelAnnoStag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSelAnnoStag.Properties.Appearance.Options.UseBackColor = True
    Me.ckSelAnnoStag.Properties.AutoHeight = False
    Me.ckSelAnnoStag.Properties.Caption = "Seleziona Anno/Stagione articolo"
    Me.ckSelAnnoStag.Size = New System.Drawing.Size(204, 19)
    Me.ckSelAnnoStag.TabIndex = 83
    '
    'lbXx_Codstag
    '
    Me.lbXx_Codstag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codstag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codstag.Location = New System.Drawing.Point(129, 45)
    Me.lbXx_Codstag.Name = "lbXx_Codstag"
    Me.lbXx_Codstag.NTSDbField = ""
    Me.lbXx_Codstag.Size = New System.Drawing.Size(101, 20)
    Me.lbXx_Codstag.TabIndex = 82
    Me.lbXx_Codstag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codstag.Tooltip = ""
    Me.lbXx_Codstag.UseMnemonic = False
    '
    'lbCodstag
    '
    Me.lbCodstag.AutoSize = True
    Me.lbCodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodstag.Location = New System.Drawing.Point(9, 48)
    Me.lbCodstag.Name = "lbCodstag"
    Me.lbCodstag.NTSDbField = ""
    Me.lbCodstag.Size = New System.Drawing.Size(49, 13)
    Me.lbCodstag.TabIndex = 60
    Me.lbCodstag.Text = "Stagione"
    Me.lbCodstag.Tooltip = ""
    Me.lbCodstag.UseMnemonic = False
    '
    'edAnnotco
    '
    Me.edAnnotco.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnotco.EditValue = "0"
    Me.edAnnotco.Location = New System.Drawing.Point(67, 20)
    Me.edAnnotco.Name = "edAnnotco"
    Me.edAnnotco.NTSDbField = ""
    Me.edAnnotco.NTSFormat = "0"
    Me.edAnnotco.NTSForzaVisZoom = False
    Me.edAnnotco.NTSOldValue = ""
    Me.edAnnotco.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnotco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnotco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnotco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnotco.Properties.AutoHeight = False
    Me.edAnnotco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnotco.Properties.MaxLength = 65536
    Me.edAnnotco.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnotco.Size = New System.Drawing.Size(56, 20)
    Me.edAnnotco.TabIndex = 40
    '
    'lbAnnotco
    '
    Me.lbAnnotco.AutoSize = True
    Me.lbAnnotco.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnotco.Location = New System.Drawing.Point(8, 23)
    Me.lbAnnotco.Name = "lbAnnotco"
    Me.lbAnnotco.NTSDbField = ""
    Me.lbAnnotco.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnotco.TabIndex = 39
    Me.lbAnnotco.Text = "Anno"
    Me.lbAnnotco.Tooltip = ""
    Me.lbAnnotco.UseMnemonic = False
    '
    'lbDadatcons
    '
    Me.lbDadatcons.AutoSize = True
    Me.lbDadatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatcons.Location = New System.Drawing.Point(3, 85)
    Me.lbDadatcons.Name = "lbDadatcons"
    Me.lbDadatcons.NTSDbField = ""
    Me.lbDadatcons.Size = New System.Drawing.Size(113, 13)
    Me.lbDadatcons.TabIndex = 87
    Me.lbDadatcons.Text = "Data consegna DA / A"
    Me.lbDadatcons.Tooltip = ""
    Me.lbDadatcons.UseMnemonic = False
    '
    'tsScho
    '
    Me.tsScho.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsScho.Location = New System.Drawing.Point(0, 62)
    Me.tsScho.Name = "tsScho"
    Me.tsScho.SelectedTabPage = Me.NtsTabPage1
    Me.tsScho.Size = New System.Drawing.Size(696, 424)
    Me.tsScho.TabIndex = 89
    Me.tsScho.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3})
    Me.tsScho.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnLeft)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(687, 394)
    Me.NtsTabPage1.Text = "&1- Principale"
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.cbOrdin)
    Me.pnLeft.Controls.Add(Me.lbOrdinamento)
    Me.pnLeft.Controls.Add(Me.pnFilterSx)
    Me.pnLeft.Controls.Add(Me.ckRilasciato)
    Me.pnLeft.Controls.Add(Me.ckEvasi)
    Me.pnLeft.Controls.Add(Me.lbStatus)
    Me.pnLeft.Controls.Add(Me.ckSalto)
    Me.pnLeft.Controls.Add(Me.fmTc)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(687, 394)
    Me.pnLeft.TabIndex = 54
    Me.pnLeft.Text = "NtsPanel1"
    '
    'lbOrdinamento
    '
    Me.lbOrdinamento.AutoSize = True
    Me.lbOrdinamento.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdinamento.Location = New System.Drawing.Point(445, 10)
    Me.lbOrdinamento.Name = "lbOrdinamento"
    Me.lbOrdinamento.NTSDbField = ""
    Me.lbOrdinamento.Size = New System.Drawing.Size(73, 13)
    Me.lbOrdinamento.TabIndex = 92
    Me.lbOrdinamento.Text = "Ordinamento:"
    Me.lbOrdinamento.Tooltip = ""
    Me.lbOrdinamento.UseMnemonic = False
    '
    'pnFilterSx
    '
    Me.pnFilterSx.AllowDrop = True
    Me.pnFilterSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFilterSx.Appearance.Options.UseBackColor = True
    Me.pnFilterSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFilterSx.Controls.Add(Me.lbDescodlsel)
    Me.pnFilterSx.Controls.Add(Me.edCodlsel)
    Me.pnFilterSx.Controls.Add(Me.cbConto)
    Me.pnFilterSx.Controls.Add(Me.lbDescodlsar)
    Me.pnFilterSx.Controls.Add(Me.edCodlsar)
    Me.pnFilterSx.Controls.Add(Me.cbCodart)
    Me.pnFilterSx.Controls.Add(Me.cmdClassificaDeleteFilter)
    Me.pnFilterSx.Controls.Add(Me.cmdClassifica)
    Me.pnFilterSx.Controls.Add(Me.lbClassifica)
    Me.pnFilterSx.Controls.Add(Me.edClassificazioneLivello5)
    Me.pnFilterSx.Controls.Add(Me.edClassificazioneLivello4)
    Me.pnFilterSx.Controls.Add(Me.edClassificazioneLivello3)
    Me.pnFilterSx.Controls.Add(Me.edClassificazioneLivello2)
    Me.pnFilterSx.Controls.Add(Me.edClassificazioneLivello1)
    Me.pnFilterSx.Controls.Add(Me.edSerie)
    Me.pnFilterSx.Controls.Add(Me.lbVert)
    Me.pnFilterSx.Controls.Add(Me.ckTipork)
    Me.pnFilterSx.Controls.Add(Me.ckSerie)
    Me.pnFilterSx.Controls.Add(Me.lbDamagaz)
    Me.pnFilterSx.Controls.Add(Me.edCommecaini)
    Me.pnFilterSx.Controls.Add(Me.edDadatcons)
    Me.pnFilterSx.Controls.Add(Me.lbCommecaini)
    Me.pnFilterSx.Controls.Add(Me.lbFaseini)
    Me.pnFilterSx.Controls.Add(Me.edFaseini)
    Me.pnFilterSx.Controls.Add(Me.cbTipork)
    Me.pnFilterSx.Controls.Add(Me.lbDadatcons)
    Me.pnFilterSx.Controls.Add(Me.edAmagaz)
    Me.pnFilterSx.Controls.Add(Me.lbXx_Codcfam)
    Me.pnFilterSx.Controls.Add(Me.edDacodart)
    Me.pnFilterSx.Controls.Add(Me.lbCodcfam)
    Me.pnFilterSx.Controls.Add(Me.edDaconto)
    Me.pnFilterSx.Controls.Add(Me.edCodcfam)
    Me.pnFilterSx.Controls.Add(Me.lbDadatord)
    Me.pnFilterSx.Controls.Add(Me.lbRilasciato)
    Me.pnFilterSx.Controls.Add(Me.edAagente)
    Me.pnFilterSx.Controls.Add(Me.cbRilasciato)
    Me.pnFilterSx.Controls.Add(Me.edDamagaz)
    Me.pnFilterSx.Controls.Add(Me.edDadatord)
    Me.pnFilterSx.Controls.Add(Me.edDaagente)
    Me.pnFilterSx.Controls.Add(Me.edSottogr)
    Me.pnFilterSx.Controls.Add(Me.edAdatord)
    Me.pnFilterSx.Controls.Add(Me.edGruppo)
    Me.pnFilterSx.Controls.Add(Me.lbDaagente)
    Me.pnFilterSx.Controls.Add(Me.lbGruppo)
    Me.pnFilterSx.Controls.Add(Me.edCommecafin)
    Me.pnFilterSx.Controls.Add(Me.edAcodart)
    Me.pnFilterSx.Controls.Add(Me.edAdatcons)
    Me.pnFilterSx.Controls.Add(Me.edFasefin)
    Me.pnFilterSx.Controls.Add(Me.edAconto)
    Me.pnFilterSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFilterSx.Location = New System.Drawing.Point(6, 3)
    Me.pnFilterSx.Name = "pnFilterSx"
    Me.pnFilterSx.NTSActiveTrasparency = True
    Me.pnFilterSx.Size = New System.Drawing.Size(433, 367)
    Me.pnFilterSx.TabIndex = 91
    Me.pnFilterSx.Text = "NtsPanel1"
    '
    'lbDescodlsel
    '
    Me.lbDescodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsel.Location = New System.Drawing.Point(185, 108)
    Me.lbDescodlsel.Name = "lbDescodlsel"
    Me.lbDescodlsel.NTSDbField = ""
    Me.lbDescodlsel.Size = New System.Drawing.Size(243, 20)
    Me.lbDescodlsel.TabIndex = 703
    Me.lbDescodlsel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodlsel.Tooltip = ""
    Me.lbDescodlsel.UseMnemonic = False
    Me.lbDescodlsel.Visible = False
    '
    'edCodlsel
    '
    Me.edCodlsel.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodlsel.EditValue = "0"
    Me.edCodlsel.Enabled = False
    Me.edCodlsel.Location = New System.Drawing.Point(129, 108)
    Me.edCodlsel.Name = "edCodlsel"
    Me.edCodlsel.NTSDbField = ""
    Me.edCodlsel.NTSFormat = "0"
    Me.edCodlsel.NTSForzaVisZoom = False
    Me.edCodlsel.NTSOldValue = ""
    Me.edCodlsel.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlsel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlsel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlsel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlsel.Properties.AutoHeight = False
    Me.edCodlsel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlsel.Properties.MaxLength = 65536
    Me.edCodlsel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlsel.Size = New System.Drawing.Size(50, 20)
    Me.edCodlsel.TabIndex = 702
    Me.edCodlsel.Visible = False
    '
    'cbConto
    '
    Me.cbConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbConto.DataSource = Nothing
    Me.cbConto.DisplayMember = ""
    Me.cbConto.Location = New System.Drawing.Point(6, 108)
    Me.cbConto.Name = "cbConto"
    Me.cbConto.NTSDbField = ""
    Me.cbConto.Properties.AutoHeight = False
    Me.cbConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbConto.Properties.DropDownRows = 30
    Me.cbConto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbConto.SelectedValue = ""
    Me.cbConto.Size = New System.Drawing.Size(116, 20)
    Me.cbConto.TabIndex = 701
    Me.cbConto.ValueMember = ""
    '
    'lbDescodlsar
    '
    Me.lbDescodlsar.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsar.Location = New System.Drawing.Point(185, 134)
    Me.lbDescodlsar.Name = "lbDescodlsar"
    Me.lbDescodlsar.NTSDbField = ""
    Me.lbDescodlsar.Size = New System.Drawing.Size(243, 20)
    Me.lbDescodlsar.TabIndex = 700
    Me.lbDescodlsar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodlsar.Tooltip = ""
    Me.lbDescodlsar.UseMnemonic = False
    Me.lbDescodlsar.Visible = False
    '
    'edCodlsar
    '
    Me.edCodlsar.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodlsar.EditValue = "0"
    Me.edCodlsar.Enabled = False
    Me.edCodlsar.Location = New System.Drawing.Point(129, 135)
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
    Me.edCodlsar.Size = New System.Drawing.Size(50, 20)
    Me.edCodlsar.TabIndex = 699
    Me.edCodlsar.Visible = False
    '
    'cbCodart
    '
    Me.cbCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCodart.DataSource = Nothing
    Me.cbCodart.DisplayMember = ""
    Me.cbCodart.Location = New System.Drawing.Point(6, 134)
    Me.cbCodart.Name = "cbCodart"
    Me.cbCodart.NTSDbField = ""
    Me.cbCodart.Properties.AutoHeight = False
    Me.cbCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCodart.Properties.DropDownRows = 30
    Me.cbCodart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCodart.SelectedValue = ""
    Me.cbCodart.Size = New System.Drawing.Size(116, 20)
    Me.cbCodart.TabIndex = 698
    Me.cbCodart.ValueMember = ""
    '
    'cmdClassificaDeleteFilter
    '
    Me.cmdClassificaDeleteFilter.Image = CType(resources.GetObject("cmdClassificaDeleteFilter.Image"), System.Drawing.Image)
    Me.cmdClassificaDeleteFilter.ImagePath = ""
    Me.cmdClassificaDeleteFilter.ImageText = ""
    Me.cmdClassificaDeleteFilter.Location = New System.Drawing.Point(39, 261)
    Me.cmdClassificaDeleteFilter.Name = "cmdClassificaDeleteFilter"
    Me.cmdClassificaDeleteFilter.NTSContextMenu = Nothing
    Me.cmdClassificaDeleteFilter.Size = New System.Drawing.Size(28, 22)
    Me.cmdClassificaDeleteFilter.TabIndex = 696
    Me.cmdClassificaDeleteFilter.ToolTip = "Rimuovi il fitro relativo alla classificazione"
    '
    'cmdClassifica
    '
    Me.cmdClassifica.ImagePath = ""
    Me.cmdClassifica.ImageText = ""
    Me.cmdClassifica.Location = New System.Drawing.Point(9, 239)
    Me.cmdClassifica.Name = "cmdClassifica"
    Me.cmdClassifica.NTSContextMenu = Nothing
    Me.cmdClassifica.Size = New System.Drawing.Size(58, 22)
    Me.cmdClassifica.TabIndex = 697
    Me.cmdClassifica.Text = "Classifica"
    '
    'lbClassifica
    '
    Me.lbClassifica.BackColor = System.Drawing.Color.Transparent
    Me.lbClassifica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbClassifica.Location = New System.Drawing.Point(72, 239)
    Me.lbClassifica.Name = "lbClassifica"
    Me.lbClassifica.NTSDbField = ""
    Me.lbClassifica.Size = New System.Drawing.Size(292, 44)
    Me.lbClassifica.TabIndex = 695
    Me.lbClassifica.Tooltip = ""
    Me.lbClassifica.UseMnemonic = False
    '
    'edClassificazioneLivello5
    '
    Me.edClassificazioneLivello5.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello5.Location = New System.Drawing.Point(32, 261)
    Me.edClassificazioneLivello5.Name = "edClassificazioneLivello5"
    Me.edClassificazioneLivello5.NTSDbField = ""
    Me.edClassificazioneLivello5.NTSForzaVisZoom = False
    Me.edClassificazioneLivello5.NTSOldValue = ""
    Me.edClassificazioneLivello5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello5.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello5.Properties.AutoHeight = False
    Me.edClassificazioneLivello5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello5.Properties.MaxLength = 65536
    Me.edClassificazioneLivello5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello5.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello5.TabIndex = 691
    Me.edClassificazioneLivello5.Visible = False
    '
    'edClassificazioneLivello4
    '
    Me.edClassificazioneLivello4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClassificazioneLivello4.Location = New System.Drawing.Point(26, 261)
    Me.edClassificazioneLivello4.Name = "edClassificazioneLivello4"
    Me.edClassificazioneLivello4.NTSDbField = ""
    Me.edClassificazioneLivello4.NTSForzaVisZoom = False
    Me.edClassificazioneLivello4.NTSOldValue = ""
    Me.edClassificazioneLivello4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello4.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello4.Properties.AutoHeight = False
    Me.edClassificazioneLivello4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello4.Properties.MaxLength = 65536
    Me.edClassificazioneLivello4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello4.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello4.TabIndex = 690
    Me.edClassificazioneLivello4.Visible = False
    '
    'edClassificazioneLivello3
    '
    Me.edClassificazioneLivello3.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edClassificazioneLivello3.Location = New System.Drawing.Point(19, 261)
    Me.edClassificazioneLivello3.Name = "edClassificazioneLivello3"
    Me.edClassificazioneLivello3.NTSDbField = ""
    Me.edClassificazioneLivello3.NTSForzaVisZoom = False
    Me.edClassificazioneLivello3.NTSOldValue = ""
    Me.edClassificazioneLivello3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello3.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello3.Properties.AutoHeight = False
    Me.edClassificazioneLivello3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello3.Properties.MaxLength = 65536
    Me.edClassificazioneLivello3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello3.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello3.TabIndex = 692
    Me.edClassificazioneLivello3.Visible = False
    '
    'edClassificazioneLivello2
    '
    Me.edClassificazioneLivello2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello2.Location = New System.Drawing.Point(13, 261)
    Me.edClassificazioneLivello2.Name = "edClassificazioneLivello2"
    Me.edClassificazioneLivello2.NTSDbField = ""
    Me.edClassificazioneLivello2.NTSForzaVisZoom = False
    Me.edClassificazioneLivello2.NTSOldValue = ""
    Me.edClassificazioneLivello2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello2.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello2.Properties.AutoHeight = False
    Me.edClassificazioneLivello2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello2.Properties.MaxLength = 65536
    Me.edClassificazioneLivello2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello2.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello2.TabIndex = 694
    Me.edClassificazioneLivello2.Visible = False
    '
    'edClassificazioneLivello1
    '
    Me.edClassificazioneLivello1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello1.Location = New System.Drawing.Point(7, 261)
    Me.edClassificazioneLivello1.Name = "edClassificazioneLivello1"
    Me.edClassificazioneLivello1.NTSDbField = ""
    Me.edClassificazioneLivello1.NTSForzaVisZoom = False
    Me.edClassificazioneLivello1.NTSOldValue = ""
    Me.edClassificazioneLivello1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello1.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello1.Properties.AutoHeight = False
    Me.edClassificazioneLivello1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello1.Properties.MaxLength = 65536
    Me.edClassificazioneLivello1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello1.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello1.TabIndex = 693
    Me.edClassificazioneLivello1.Visible = False
    '
    'lbVert
    '
    Me.lbVert.BackColor = System.Drawing.Color.Transparent
    Me.lbVert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbVert.Dock = System.Windows.Forms.DockStyle.Right
    Me.lbVert.Location = New System.Drawing.Point(432, 0)
    Me.lbVert.Name = "lbVert"
    Me.lbVert.NTSDbField = ""
    Me.lbVert.Size = New System.Drawing.Size(1, 367)
    Me.lbVert.TabIndex = 93
    Me.lbVert.Tooltip = ""
    Me.lbVert.UseMnemonic = False
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(280, 342)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(55, 19)
    Me.ckSerie.TabIndex = 89
    '
    'lbFaseini
    '
    Me.lbFaseini.AutoSize = True
    Me.lbFaseini.BackColor = System.Drawing.Color.Transparent
    Me.lbFaseini.Location = New System.Drawing.Point(3, 163)
    Me.lbFaseini.Name = "lbFaseini"
    Me.lbFaseini.NTSDbField = ""
    Me.lbFaseini.Size = New System.Drawing.Size(64, 13)
    Me.lbFaseini.TabIndex = 65
    Me.lbFaseini.Text = "Fase DA / A"
    Me.lbFaseini.Tooltip = ""
    Me.lbFaseini.UseMnemonic = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnAll)
    Me.NtsTabPage2.Controls.Add(Me.pnFiltri2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(687, 394)
    Me.NtsTabPage2.Text = "&2 - Filtri Estesi"
    '
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.ceFiltriExt)
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 0)
    Me.pnAll.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAll.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(687, 394)
    Me.pnAll.TabIndex = 2
    Me.pnAll.Text = "NtsPanel1"
    '
    'ceFiltriExt
    '
    Me.ceFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.ceFiltriExt.MinimumSize = New System.Drawing.Size(399, 193)
    Me.ceFiltriExt.Name = "ceFiltriExt"
    Me.ceFiltriExt.oParent = Nothing
    Me.ceFiltriExt.Size = New System.Drawing.Size(687, 394)
    Me.ceFiltriExt.strNomeCampo = ""
    Me.ceFiltriExt.TabIndex = 1
    '
    'pnFiltri2
    '
    Me.pnFiltri2.AllowDrop = True
    Me.pnFiltri2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri2.Appearance.Options.UseBackColor = True
    Me.pnFiltri2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri2.Controls.Add(Me.cmdLock)
    Me.pnFiltri2.Controls.Add(Me.grFiltri1)
    Me.pnFiltri2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnFiltri2.Location = New System.Drawing.Point(567, 333)
    Me.pnFiltri2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnFiltri2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnFiltri2.Name = "pnFiltri2"
    Me.pnFiltri2.NTSActiveTrasparency = True
    Me.pnFiltri2.Size = New System.Drawing.Size(120, 61)
    Me.pnFiltri2.TabIndex = 0
    Me.pnFiltri2.Text = "NtsPanel1"
    Me.pnFiltri2.Visible = False
    '
    'cmdLock
    '
    Me.cmdLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdLock.ImagePath = ""
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(6, 35)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(111, 20)
    Me.cmdLock.TabIndex = 9
    Me.cmdLock.Text = "Blocca/sblocca filtri"
    '
    'grFiltri1
    '
    Me.grFiltri1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grFiltri1.EmbeddedNavigator.Name = ""
    Me.grFiltri1.Location = New System.Drawing.Point(0, 0)
    Me.grFiltri1.MainView = Me.grvFiltri1
    Me.grFiltri1.Name = "grFiltri1"
    Me.grFiltri1.Size = New System.Drawing.Size(120, 29)
    Me.grFiltri1.TabIndex = 6
    Me.grFiltri1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFiltri1})
    '
    'grvFiltri1
    '
    Me.grvFiltri1.ActiveFilterEnabled = False
    Me.grvFiltri1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_valoreda, Me.xx_valorea})
    Me.grvFiltri1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFiltri1.Enabled = True
    Me.grvFiltri1.GridControl = Me.grFiltri1
    Me.grvFiltri1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri1.MinRowHeight = 14
    Me.grvFiltri1.Name = "grvFiltri1"
    Me.grvFiltri1.NTSAllowDelete = True
    Me.grvFiltri1.NTSAllowInsert = True
    Me.grvFiltri1.NTSAllowUpdate = True
    Me.grvFiltri1.NTSMenuContext = Nothing
    Me.grvFiltri1.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri1.OptionsFilter.AllowFilterEditor = False
    Me.grvFiltri1.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri1.OptionsNavigation.UseTabKey = False
    Me.grvFiltri1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri1.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri1.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFiltri1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri1.OptionsView.ShowGroupPanel = False
    Me.grvFiltri1.RowHeight = 16
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Caption = "Filtro"
    Me.xx_nome.Enabled = False
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowEdit = False
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nome.OptionsColumn.ReadOnly = True
    Me.xx_nome.OptionsFilter.AllowFilter = False
    Me.xx_nome.Visible = True
    Me.xx_nome.VisibleIndex = 0
    Me.xx_nome.Width = 178
    '
    'xx_valoreda
    '
    Me.xx_valoreda.AppearanceCell.Options.UseBackColor = True
    Me.xx_valoreda.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valoreda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valoreda.Caption = "Valore DA"
    Me.xx_valoreda.Enabled = True
    Me.xx_valoreda.FieldName = "xx_valoreda"
    Me.xx_valoreda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valoreda.Name = "xx_valoreda"
    Me.xx_valoreda.NTSRepositoryComboBox = Nothing
    Me.xx_valoreda.NTSRepositoryItemCheck = Nothing
    Me.xx_valoreda.NTSRepositoryItemMemo = Nothing
    Me.xx_valoreda.NTSRepositoryItemText = Nothing
    Me.xx_valoreda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valoreda.OptionsFilter.AllowFilter = False
    Me.xx_valoreda.Visible = True
    Me.xx_valoreda.VisibleIndex = 1
    Me.xx_valoreda.Width = 300
    '
    'xx_valorea
    '
    Me.xx_valorea.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea.Caption = "Valore A"
    Me.xx_valorea.Enabled = True
    Me.xx_valorea.FieldName = "xx_valorea"
    Me.xx_valorea.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea.Name = "xx_valorea"
    Me.xx_valorea.NTSRepositoryComboBox = Nothing
    Me.xx_valorea.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea.NTSRepositoryItemText = Nothing
    Me.xx_valorea.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valorea.OptionsFilter.AllowFilter = False
    Me.xx_valorea.Visible = True
    Me.xx_valorea.VisibleIndex = 2
    Me.xx_valorea.Width = 300
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPianificazione)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(687, 394)
    Me.NtsTabPage3.Text = "Filtri stampa pianificazione"
    '
    'pnPianificazione
    '
    Me.pnPianificazione.AllowDrop = True
    Me.pnPianificazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPianificazione.Appearance.Options.UseBackColor = True
    Me.pnPianificazione.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPianificazione.Controls.Add(Me.ckSolotaskril)
    Me.pnPianificazione.Controls.Add(Me.ckMovord4)
    Me.pnPianificazione.Controls.Add(Me.ckMovord)
    Me.pnPianificazione.Controls.Add(Me.ckTaskPM)
    Me.pnPianificazione.Controls.Add(Me.ckMovord1)
    Me.pnPianificazione.Controls.Add(Me.ckMovord3)
    Me.pnPianificazione.Controls.Add(Me.ckOrdlist)
    Me.pnPianificazione.Controls.Add(Me.ckMovord2)
    Me.pnPianificazione.Controls.Add(Me.ckZzdispsca)
    Me.pnPianificazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPianificazione.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPianificazione.Location = New System.Drawing.Point(0, 0)
    Me.pnPianificazione.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPianificazione.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPianificazione.Name = "pnPianificazione"
    Me.pnPianificazione.NTSActiveTrasparency = True
    Me.pnPianificazione.Size = New System.Drawing.Size(687, 394)
    Me.pnPianificazione.TabIndex = 0
    Me.pnPianificazione.Text = "NtsPanel1"
    '
    'NtsGridColumn1
    '
    Me.NtsGridColumn1.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn1.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn1.Caption = "Filtro"
    Me.NtsGridColumn1.Enabled = False
    Me.NtsGridColumn1.FieldName = "xx_nome"
    Me.NtsGridColumn1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn1.Name = "NtsGridColumn1"
    Me.NtsGridColumn1.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn1.OptionsColumn.AllowEdit = False
    Me.NtsGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn1.OptionsColumn.ReadOnly = True
    Me.NtsGridColumn1.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn1.Visible = True
    Me.NtsGridColumn1.VisibleIndex = 0
    Me.NtsGridColumn1.Width = 178
    '
    'NtsGridColumn2
    '
    Me.NtsGridColumn2.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn2.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn2.Caption = "Valore DA"
    Me.NtsGridColumn2.Enabled = True
    Me.NtsGridColumn2.FieldName = "xx_valoreda"
    Me.NtsGridColumn2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn2.Name = "NtsGridColumn2"
    Me.NtsGridColumn2.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn2.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn2.Visible = True
    Me.NtsGridColumn2.VisibleIndex = 1
    Me.NtsGridColumn2.Width = 300
    '
    'NtsGridColumn3
    '
    Me.NtsGridColumn3.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn3.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn3.Caption = "Valore A"
    Me.NtsGridColumn3.Enabled = True
    Me.NtsGridColumn3.FieldName = "xx_valorea"
    Me.NtsGridColumn3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn3.Name = "NtsGridColumn3"
    Me.NtsGridColumn3.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn3.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn3.Visible = True
    Me.NtsGridColumn3.VisibleIndex = 2
    Me.NtsGridColumn3.Width = 300
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.pnTipoStampa)
    Me.pnTop.Controls.Add(Me.lbTipoStampa)
    Me.pnTop.Controls.Add(Me.cmdApriFiltri)
    Me.pnTop.Controls.Add(Me.cbFiltro)
    Me.pnTop.Controls.Add(Me.lbFiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(696, 32)
    Me.pnTop.TabIndex = 90
    Me.pnTop.Text = "NtsPanel2"
    '
    'pnTipoStampa
    '
    Me.pnTipoStampa.AllowDrop = True
    Me.pnTipoStampa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTipoStampa.Appearance.Options.UseBackColor = True
    Me.pnTipoStampa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa1)
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa0)
    Me.pnTipoStampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTipoStampa.Location = New System.Drawing.Point(526, 3)
    Me.pnTipoStampa.Name = "pnTipoStampa"
    Me.pnTipoStampa.NTSActiveTrasparency = True
    Me.pnTipoStampa.Size = New System.Drawing.Size(167, 28)
    Me.pnTipoStampa.TabIndex = 93
    Me.pnTipoStampa.Text = "NtsPanel1"
    '
    'lbTipoStampa
    '
    Me.lbTipoStampa.AutoSize = True
    Me.lbTipoStampa.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoStampa.Location = New System.Drawing.Point(445, 9)
    Me.lbTipoStampa.Name = "lbTipoStampa"
    Me.lbTipoStampa.NTSDbField = ""
    Me.lbTipoStampa.Size = New System.Drawing.Size(80, 13)
    Me.lbTipoStampa.TabIndex = 77
    Me.lbTipoStampa.Text = "Tipo di stampa:"
    Me.lbTipoStampa.Tooltip = ""
    Me.lbTipoStampa.UseMnemonic = False
    '
    'cmdApriFiltri
    '
    Me.cmdApriFiltri.Image = CType(resources.GetObject("cmdApriFiltri.Image"), System.Drawing.Image)
    Me.cmdApriFiltri.ImageAlignment = DevExpress.Utils.HorzAlignment.[Default]
    Me.cmdApriFiltri.ImagePath = ""
    Me.cmdApriFiltri.ImageText = ""
    Me.cmdApriFiltri.Location = New System.Drawing.Point(237, 3)
    Me.cmdApriFiltri.Name = "cmdApriFiltri"
    Me.cmdApriFiltri.NTSContextMenu = Nothing
    Me.cmdApriFiltri.Size = New System.Drawing.Size(28, 28)
    Me.cmdApriFiltri.TabIndex = 5
    '
    'cbFiltro
    '
    Me.cbFiltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbFiltro.DataSource = Nothing
    Me.cbFiltro.DisplayMember = ""
    Me.cbFiltro.Location = New System.Drawing.Point(88, 6)
    Me.cbFiltro.Name = "cbFiltro"
    Me.cbFiltro.NTSDbField = ""
    Me.cbFiltro.Properties.AutoHeight = False
    Me.cbFiltro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbFiltro.Properties.DropDownRows = 30
    Me.cbFiltro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbFiltro.SelectedValue = ""
    Me.cbFiltro.Size = New System.Drawing.Size(146, 20)
    Me.cbFiltro.TabIndex = 4
    Me.cbFiltro.ValueMember = ""
    '
    'lbFiltri
    '
    Me.lbFiltri.AutoSize = True
    Me.lbFiltri.BackColor = System.Drawing.Color.Transparent
    Me.lbFiltri.Location = New System.Drawing.Point(3, 9)
    Me.lbFiltri.Name = "lbFiltri"
    Me.lbFiltri.NTSDbField = ""
    Me.lbFiltri.Size = New System.Drawing.Size(79, 13)
    Me.lbFiltri.TabIndex = 3
    Me.lbFiltri.Text = "Configurazione"
    Me.lbFiltri.Tooltip = ""
    Me.lbFiltri.UseMnemonic = False
    '
    'tlbAccorpa
    '
    Me.tlbAccorpa.Caption = "Accorpa Mag. Assimilati"
    Me.tlbAccorpa.GlyphPath = ""
    Me.tlbAccorpa.Id = 22
    Me.tlbAccorpa.Name = "tlbAccorpa"
    Me.tlbAccorpa.NTSIsCheckBox = True
    Me.tlbAccorpa.Visible = True
    '
    'FRMORSCHO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(696, 486)
    Me.Controls.Add(Me.tsScho)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMORSCHO"
    Me.Text = "STAMPA / VISUALIZZAZIONE SCHEDE ORDINI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEvasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaagente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAagente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFasefin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFaseini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbRilasciato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSolotaskril.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovord4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTaskPM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovord3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovord2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckZzdispsca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOrdlist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovord1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRilasciato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOrdin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTc.ResumeLayout(False)
    Me.fmTc.PerformLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsScho, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsScho.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.pnFilterSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFilterSx.ResumeLayout(False)
    Me.pnFilterSx.PerformLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri2.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPianificazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPianificazione.ResumeLayout(False)
    Me.pnPianificazione.PerformLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTipoStampa.ResumeLayout(False)
    Me.pnTipoStampa.PerformLayout()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      ckSelAnnoStag.NTSSetParam(oMenu, oApp.Tr(Me, 128595426228437500, "Seleziona Anno/Stagione articolo"), "S", "N")
      edCodstag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426228906250, "Stagione"), tabstag)
      edAnnotco.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229062500, "Anno"), "0", 4, 0, 2099)
      opTipoStampa1.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229218750, "Completo"), "S")
      opTipoStampa0.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229375000, "Ridotto"), "S")
      cbOrdin.NTSSetParam(oApp.Tr(Me, 128756214849590060, "Ordinamento"))
      ckRilasciato.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229531250, "Aggiorna Status 'Rilasciato' su righe ordini"), "S", "N")
      ckSolotaskril.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229687500, "Solo tasks rilasciati"), "S", "N")
      ckMovord4.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229843750, "Impegni di commessa"), "S", "N")
      ckTaskPM.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230000000, "Tasks Commesse "), "S", "N")
      ckMovord3.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230156250, "Impe&gni Clienti aperti"), "S", "N")
      ckMovord2.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230312500, "Ordini Fornitori ap&erti"), "S", "N")
      ckZzdispsca.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230468750, "Fabbisogni primari E&MRP"), "S", "N")
      ckOrdlist.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230625000, "Proposte &d'Ordine"), "S", "N")
      ckMovord1.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230781250, "A&ltri Ordini Impegni"), "S", "N")
      ckMovord.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230937500, "&Impegni Clienti"), "S", "N")
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426231250000, "Linea/famiglia articolo"), tabcfam, True)
      ckTipork.NTSSetParam(oMenu, oApp.Tr(Me, 128595426231406250, "Tipo &Documento"), "S", "N")
      cbTipork.NTSSetParam(oApp.Tr(Me, 128595426231562500, "Tipo &Documento"))
      cbRilasciato.NTSSetParam(oApp.Tr(Me, 128595426231875000, "Tipo ordini"))
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426232187500, "Serie ordini"), CLN__STD.SerieMaxLen, True)
      edSottogr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232500000, "Sottogruppo"), tabsgme)
      edGruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232656250, "Gruppo"), tabgmer)
      edFasefin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232968750, "a fase"), tabartfasi)
      edFasefin.ArtiPerFase = edAcodart
      edFaseini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426233281250, "Da fase"), tabartfasi)
      edFaseini.ArtiPerFase = edDacodart
      edCommecafin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426233750000, "a commessa"), tabcommess)
      edCommecaini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426233906250, "Da commessa"), tabcommess)
      cbCodart.NTSSetParam(oApp.Tr(Me, 130378881630028762, "Articolo"))
      edCodlsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130378881142506099, "Lista selezionata articolo"), tablsar)
      edAcodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234218750, "a codice articolo"), tabartico, False)
      edDacodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234687500, "Da codice articolo"), tabartico, True)
      cbConto.NTSSetParam(oApp.Tr(Me, 130378892331840956, "Conto"))
      edCodlsel.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130378891845127165, "Lista selezionata clienti/fornitori"), tablsel)
      edAconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235000000, "a conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235156250, "Da conto"), tabanagrac)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128595426235625000, "alla data di consegna"), False)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128595426235937500, "Dalla data di consegna"), False)
      edAdatord.NTSSetParam(oMenu, oApp.Tr(Me, 128595426236250000, "alla data ordine"), False)
      edDadatord.NTSSetParam(oMenu, oApp.Tr(Me, 128595426236562500, "Dalla data ordine"), False)
      edAagente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426236718750, "ad agente"), tabcage)
      edDaagente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237031250, "Da agente"), tabcage)
      edAmagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237500000, "a magazzino"), tabmaga)
      edDamagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237656250, "Da Magazzino"), tabmaga)
      ckSalto.NTSSetParam(oMenu, oApp.Tr(Me, 128595426237968750, "&Salto pagina per ogni articolo"), "S", "N")
      ckEvasi.NTSSetParam(oMenu, oApp.Tr(Me, 128595426238125000, "C&onsidera solo le righe non evase"), "S", "N")
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvFiltri1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia filtri 1"))
      grvFiltri1.NTSAllowDelete = False
      grvFiltri1.NTSAllowInsert = False
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128491820088062000, "Nome filtro"), dttCampi, "xx_nome", "cb_nomcampo")
      xx_valoreda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128491820607854000, "Valore filtro DA"), 0)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128584498502187500, "Valore filtro A"), 0)
      xx_valoreda.NTSSetParamZoom("__")
      xx_valorea.NTSSetParamZoom("__")

      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 130421334824965792, "Filtri Estesi"), "BSORSCHO", New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTORD")
      ceFiltriExt.AggiungiTabella("MOVORD")
      ceFiltriExt.AggiungiTabella("ANAGRA")
      ceFiltriExt.AggiungiTabella("ARTICO")
      ceFiltriExt.InizializzaFiltri()

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
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMORSCHO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("movord", dttCampi) Then
        Me.Close()
        Return
      End If
      strProntoMess = oApp.Tr(Me, 128756215105211788, "Pronto.")
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()

      CreaDatatableFiltri()

      dcFiltri1.DataSource = dsFiltri.Tables("FILTRI1")
      dsFiltri.AcceptChanges()
      grFiltri1.DataSource = dcFiltri1

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      tsScho.SelectedTabPageIndex = 0

      oCleScho.bModTCO = CBool(oApp.ActKey.ModuliExtAzienda And bsModExtTCO)
      oCleScho.bModAS = CBool(oApp.ActKey.ModuliAzienda And bsModAS)
      If CBool(oApp.ActKey.ModuliExtAzienda And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleScho.bModuloCRM = True
      Else
        oCleScho.bModuloCRM = False
      End If

      oCleScho.bModuloCRM = False

      CaricaCombo()

      If Not oCleScho.LeggiDatiDitta(DittaCorrente) Then
        Me.Close()
        Return
      End If

      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          oCleScho.bSchoDagest = True
          oCleScho.lSchoDaconto = NTSCInt(Mid(oCallParams.strParam, 1, 9))
          oCleScho.lSchoAconto = NTSCInt(Mid(oCallParams.strParam, 11, 9))
          oCleScho.strSchoDacodart = Trim(Mid(oCallParams.strParam, 21, CLN__STD.CodartMaxLen))
          oCleScho.strSchoAcodart = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 22, CLN__STD.CodartMaxLen))
          oCleScho.strSchoTipork = Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 23, 1)
          oCleScho.bSchoFiltri = CBool(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 25, 1))
          oCleScho.strSchoDatordini = Trim(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 27, 10))
          oCleScho.strSchoDatordfin = Trim(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 38, 10))
        Else
          oCleScho.bSchoDagest = False
        End If
      End If

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleScho.bModuloCRM = True
      If oCleScho.bModuloCRM Then
        oCleScho.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleScho.bAmm, oCleScho.strAccvis, oCleScho.strAccmod, oCleScho.strRegvis, oCleScho.strRegmod)
        If oCleScho.bIsCRMUser Then
          oCleScho.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleScho.nCodcageoperat)
          If oCleScho.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If

      edDamagaz.Text = "0"
      edAmagaz.Text = "9999"
      edDaagente.Text = "0"
      edAagente.Text = "9999"
      edDadatord.Text = IntSetDate("01/01/1900")
      edAdatord.Text = IntSetDate("31/12/2099")
      If oCleScho.bSchoDagest = False Then
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        cbTipork.Enabled = False
      Else
        edDaconto.Text = NTSCStr(oCleScho.lSchoDaconto)
        edAconto.Text = NTSCStr(oCleScho.lSchoAconto)
        edDacodart.Text = oCleScho.strSchoDacodart
        edAcodart.Text = oCleScho.strSchoAcodart
        If oCleScho.strSchoTipork = "§" Then
          cbTipork.SelectedValue = "§"
          cbTipork.Enabled = False
        Else
          ckTipork.Checked = True
          cbTipork.SelectedValue = oCleScho.strSchoTipork
        End If
        If oCleScho.strSchoDatordini <> "" Then edDadatord.Text = oCleScho.strSchoDatordini
        If oCleScho.strSchoDatordfin <> "" Then edAdatord.Text = oCleScho.strSchoDatordfin
        cbRilasciato.SelectedValue = "E"
      End If
      edCommecaini.Text = "0"
      edCommecafin.Text = "999999999"
      edFaseini.Text = "0"
      edFasefin.Text = "9999"
      edDadatcons.Text = IntSetDate("01/01/1900")
      edAdatcons.Text = IntSetDate("31/12/2099")
      edGruppo.Text = "0"
      edSottogr.Text = "0"
      edCodcfam.Text = ""
      lbXx_Codcfam.Text = ""
      ckMovord.Checked = False
      ckMovord1.Checked = False
      ckOrdlist.Checked = False
      ckZzdispsca.Checked = False
      ckMovord2.Checked = False
      ckMovord3.Checked = False
      ckMovord4.Checked = False
      ckMovord.Enabled = False
      ckMovord1.Enabled = False
      ckOrdlist.Enabled = False
      ckZzdispsca.Enabled = False
      ckMovord2.Enabled = False
      ckMovord3.Enabled = False
      ckMovord4.Enabled = False
      ckTaskPM.Checked = False
      ckSolotaskril.Checked = False
      ckTaskPM.Enabled = False
      ckSolotaskril.Enabled = False
      pnPianificazione.Enabled = False
      tlbStampaWord.Enabled = False
      tlbStampaWordRaggr.Enabled = False
      '-----------------------------------------------------------------------------------------
      If oCleScho.bModTCO = False Then
        fmTc.Visible = False
        ckSelAnnoStag.Visible = False
      End If
      lbAnnotco.Enabled = False
      lbCodstag.Enabled = False
      edAnnotco.Enabled = False
      edCodstag.Enabled = False
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo CUSTOMER SERVICE rende non visibile il CheckBox
      '--- relativo al'Impegno di commessa
      '-----------------------------------------------------------------------------------------
      If oCleScho.bModAS = False Then
        ckMovord4.Visible = False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Legge opzioni
      '-----------------------------------------------------------------------------------------
      oCleScho.nMagazInternoTransito = NTSCInt(oMenu.GetSettingBus("BSDBEMRP", "OPZIONI", ".", "MagazInternoTransito", "1", " ", "1"))
      oCleScho.nAltezzaGif = NTSCInt(oMenu.GetSettingBus("BSORSCHO", "OPZIONI", ".", "AltezzaGif", "20", " ", "20"))

      If (oCleScho.bSchoDagest = True) And (oCleScho.bSchoFiltri = False) Then
        Me.Visible = False
        ReportSuGriglia()
      End If

      edSerie.Enabled = False
      ckSerie.Checked = False

      lbStatus.Text = strProntoMess

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()
      '---------------------------------------------------
      'Prende la struttura della tabella
      oCleScho.GetTableStructMovIfil(dttDefault)

      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")

      ComponiDatatable(dttDefault, Me)

      tlbAccorpa.Checked = CBool(oMenu.GetSettingBus("BSORSCHO", "RECENT", ".", "Accorpa", "0", " ", "0"))
      tlbNoModal.Checked = CBool(oMenu.GetSettingBus("BSORSCHO", "RECENT", ".", "ChildNoModal", "0", " ", "0"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORSCHO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      'Necessario per ovviare al problema che non caricava i dati se si forzava un valore del combo dalla configuratore user interface
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORSCHO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTSCHO", False, oCleScho.lIITTScho)
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSORSCHO", "RECENT", ".", "Accorpa", IIf(tlbAccorpa.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSORSCHO", "RECENT", ".", "ChildNoModal", IIf(tlbNoModal.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORSCHO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    Dim strTmp As String = ""
    Dim i As Integer = 0
    dsFiltri.Tables("FILTRI1").AcceptChanges()
    For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
    Next
    strTmp = strTmp.Substring(0, strTmp.Length - 1)
    oMenu.SaveSettingBus("BNORSCHO", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTmp As String = ""
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edFaseini.Focused Then
        If edDacodart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128596603345677879, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edFaseini.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edFaseini.Text)
        oParam.strTipo = edDacodart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edFaseini.Text Then edFaseini.NTSTextDB = NTSZOOM.strIn
      ElseIf edFasefin.Focused Then
        If edAcodart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128596603368815755, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edFasefin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edFasefin.Text)
        oParam.strTipo = edAcodart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edFasefin.Text Then edFasefin.NTSTextDB = NTSZOOM.strIn
      ElseIf grFiltri1.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        If NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome) = "." Then Return 'sono su una colonna N.A.
        If grvFiltri1.FocusedColumn.Name = "xx_valoreda" Then
          strTmp = NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valoreda)
          ApriZoomTabella(strTmp, NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome))
          'If strTmp <> NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valoreda) And strTmp <> "" Then
          '  grvFiltri1.SetFocusedValue(strTmp)
          'End If
        Else
          strTmp = NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valorea)
          ApriZoomTabella(strTmp, NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome))
          'If strTmp <> NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valorea) And strTmp <> "" Then
          '  grvFiltri1.SetFocusedValue(strTmp)
          'End If
        End If
      ElseIf ceFiltriExt.ContainsFocus Then
        ceFiltriExt.Zoom()
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
      ReportSuGriglia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = strProntoMess
    End Try
  End Sub

  Public Overridable Sub tlbStampaWordRaggr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWordRaggr.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strSQLRipartisciTaglieRagg As String = ""
    Dim strSQLTaglieRagg As String = ""
    Try
      Me.ValidaLastControl()
      '-----------------------
      'faccio comporre la query al dl

      oCleScho.bStampaWordRaggruppata = True

      strQueryWord = oCleScho.GetQueryStampaWord(ckEvasi.Checked, edDamagaz.Text, _
                                  edAmagaz.Text, edDaagente.Text, edAagente.Text, edDadatord.Text, _
                                  edAdatord.Text, edDadatcons.Text, edAdatcons.Text, edDaconto.Text, _
                                  edAconto.Text, edDacodart.Text, edAcodart.Text, edCommecaini.Text, _
                                  edCommecafin.Text, edFaseini.Text, edFasefin.Text, edGruppo.Text, _
                                  edSottogr.Text, edSerie.Text, cbRilasciato.SelectedValue, ckTipork.Checked, _
                                  edCodcfam.Text, ckSelAnnoStag.Checked, edAnnotco.Text, _
                                  edCodstag.Text, cbTipork.SelectedValue, ceFiltriExt.GeneraQuerySQL(), _
                                  edClassificazioneLivello1.Text, edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                                  edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                                  cbConto.SelectedValue, NTSCInt(edCodlsel.Text), _
                                  cbCodart.SelectedValue, NTSCInt(edCodlsar.Text))
      If strQueryWord = "" Then Return

      CaricaDataSetWord(dsTmp)

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORSCHO"
      oPar.strPar2 = strQueryWord
      oPar.bPar2 = oCleScho.bStampaWordRaggruppata
      oPar.ctlPar1 = dsTmp
      oCleScho.RipartisciTaglieQuantitaRagg(ckEvasi.Checked, edDamagaz.Text, edAmagaz.Text, _
                                            edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                                            edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                                            edDacodart.Text, edAcodart.Text, edCommecaini.Text, edCommecafin.Text, _
                                            edFaseini.Text, edFasefin.Text, edGruppo.Text, edSottogr.Text, _
                                            edSerie.Text, cbRilasciato.SelectedValue, ckTipork.Checked, cbTipork.SelectedValue, _
                                            edCodcfam.Text, ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                                            strSQLRipartisciTaglieRagg, _
                                            edClassificazioneLivello1.Text, edClassificazioneLivello2.Text, _
                                            edClassificazioneLivello3.Text, edClassificazioneLivello4.Text, _
                                            edClassificazioneLivello5.Text, _
                                            cbConto.SelectedValue, NTSCInt(edCodlsel.Text), _
                                            cbCodart.SelectedValue, NTSCInt(edCodlsar.Text))
      oCleScho.TaglieQuantitaRagg(ckEvasi.Checked, edDamagaz.Text, edAmagaz.Text, _
                                  edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                                  edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                                  edDacodart.Text, edAcodart.Text, edCommecaini.Text, edCommecafin.Text, _
                                  edFaseini.Text, edFasefin.Text, edGruppo.Text, edSottogr.Text, _
                                  edSerie.Text, cbRilasciato.SelectedValue, ckTipork.Checked, cbTipork.SelectedValue, _
                                  edCodcfam.Text, ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                                  strSQLTaglieRagg, _
                                  edClassificazioneLivello1.Text, edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                                  edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                                  cbConto.SelectedValue, NTSCInt(edCodlsel.Text), _
                                  cbCodart.SelectedValue, NTSCInt(edCodlsar.Text))
      oPar.strPar3 = strSQLRipartisciTaglieRagg
      oPar.strPar4 = strSQLTaglieRagg
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      Me.ValidaLastControl()
      '-----------------------
      'faccio comporre la query al dl

      oCleScho.bStampaWordRaggruppata = False

      strQueryWord = oCleScho.GetQueryStampaWord(ckEvasi.Checked, edDamagaz.Text, _
                                  edAmagaz.Text, edDaagente.Text, edAagente.Text, edDadatord.Text, _
                                  edAdatord.Text, edDadatcons.Text, edAdatcons.Text, edDaconto.Text, _
                                  edAconto.Text, edDacodart.Text, edAcodart.Text, edCommecaini.Text, _
                                  edCommecafin.Text, edFaseini.Text, edFasefin.Text, edGruppo.Text, _
                                  edSottogr.Text, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, cbRilasciato.SelectedValue, ckTipork.Checked, _
                                  edCodcfam.Text, ckSelAnnoStag.Checked, edAnnotco.Text, _
                                  edCodstag.Text, cbTipork.SelectedValue, ceFiltriExt.GeneraQuerySQL(), _
                                  edClassificazioneLivello1.Text, edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                                  edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                                  cbConto.SelectedValue, NTSCInt(edCodlsel.Text), _
                                  cbCodart.SelectedValue, NTSCInt(edCodlsar.Text))
      If strQueryWord = "" Then Return

      CaricaDataSetWord(dsTmp)

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORSCHO"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = cbTipork.SelectedValue
      oPar.bPar2 = oCleScho.bStampaWordRaggruppata
      oPar.ctlPar1 = dsTmp
      oPar.strPar3 = ""
      oPar.strPar4 = ""
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edCodstag_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodstag.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edCodstag_Validated(NTSCInt(edCodstag.Text), strTmp) Then
        edCodstag.Text = NTSCStr(edCodstag.OldEditValue)
      Else
        lbXx_Codstag.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edGruppo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edGruppo.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edGruppo_Validated(NTSCInt(edGruppo.Text), strTmp) Then
        edGruppo.Text = NTSCStr(edGruppo.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSottogr_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSottogr.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edSottogr_Validated(NTSCInt(edSottogr.Text), strTmp) Then
        edSottogr.Text = NTSCStr(edSottogr.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodcfam_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp) Then
        edCodcfam.Text = NTSCStr(edCodcfam.OldEditValue)
      Else
        lbXx_Codcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDacodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacodart.Validated
    Try
      If edDacodart.Text <> "".PadLeft(CLN__STD.CodartMaxLen) And edDacodart.Text <> "" Then
        edDacodart.Text = UCase(edDacodart.Text)
        edAcodart.Text = edDacodart.Text
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If NTSCInt(edDaconto.Text) <> 0 Then edAconto.Text = edDaconto.Text

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAcodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAcodart.Validated
    Try
      If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then edAcodart.Text = UCase(edAcodart.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodlsar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsar.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleScho Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleScho.edCodlsar_Validated(NTSCInt(edCodlsar.Text), strTmp) Then
        edCodlsar.Text = NTSCStr(edCodlsar.OldEditValue)
      Else
        lbDescodlsar.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edCodlsel_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsel.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleScho Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleScho.edCodlsel_Validated(NTSCInt(edCodlsel.Text), strTmp) Then
        edCodlsel.Text = NTSCStr(edCodlsel.OldEditValue)
      Else
        lbDescodlsel.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If ckSerie.Checked And edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbOrdin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdin.SelectedIndexChanged
    Try
      Select Case NTSCInt(cbOrdin.SelectedValue)
        Case 1
          If ckSalto.Visible = False Then
            GctlSetVisEnab(ckSalto, True)
          End If
          ckMovord.Checked = False
          ckMovord1.Checked = False
          ckOrdlist.Checked = False
          ckZzdispsca.Checked = False
          ckMovord2.Checked = False
          ckMovord3.Checked = False
          ckMovord4.Checked = False
          ckMovord.Enabled = False
          ckMovord1.Enabled = False
          ckOrdlist.Enabled = False
          ckZzdispsca.Enabled = False
          ckMovord2.Enabled = False
          ckMovord3.Enabled = False
          ckMovord4.Enabled = False
          ckTaskPM.Checked = False
          ckSolotaskril.Checked = False
          ckTaskPM.Enabled = False
          ckSolotaskril.Enabled = False
          pnPianificazione.Enabled = False
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          ckRilasciato.Enabled = True
          cbRilasciato.Enabled = True
          lbRilasciato.Enabled = True
          tlbAccorpa.Enabled = False
          tlbStampaWord.Enabled = False
          tlbStampaWordRaggr.Enabled = False
          If pnTipoStampa.Enabled = False Then
            GctlSetVisEnab(pnTipoStampa, False)
            GctlSetVisEnab(opTipoStampa0, False)
            GctlSetVisEnab(opTipoStampa1, False)
          End If
        Case 2
          ckSalto.Checked = False
          ckSalto.TabStop = False
          ckSalto.Visible = False
          ckMovord.Checked = False
          ckMovord1.Checked = False
          ckOrdlist.Checked = False
          ckZzdispsca.Checked = False
          ckMovord2.Checked = False
          ckMovord3.Checked = False
          ckMovord4.Checked = False
          ckMovord.Enabled = False
          ckMovord1.Enabled = False
          ckOrdlist.Enabled = False
          ckZzdispsca.Enabled = False
          ckMovord2.Enabled = False
          ckMovord3.Enabled = False
          ckMovord4.Enabled = False
          ckTaskPM.Checked = False
          ckSolotaskril.Checked = False
          ckTaskPM.Enabled = False
          ckSolotaskril.Enabled = False
          pnPianificazione.Enabled = False
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          ckRilasciato.Enabled = True
          cbRilasciato.Enabled = True
          lbRilasciato.Enabled = True
          tlbAccorpa.Enabled = False
          GctlSetVisEnab(tlbStampaWord, False)
          GctlSetVisEnab(tlbStampaWordRaggr, False)
          GctlSetVisEnab(ckTipork, False)
          If pnTipoStampa.Enabled = False Then
            GctlSetVisEnab(pnTipoStampa, False)
            GctlSetVisEnab(opTipoStampa0, False)
            GctlSetVisEnab(opTipoStampa1, False)
          End If
        Case 3
          If ckSalto.Visible = False Then
            GctlSetVisEnab(ckSalto, True)
            ckSalto.TabStop = True
          End If
          ckMovord.Checked = False
          ckMovord1.Checked = False
          ckOrdlist.Checked = False
          ckZzdispsca.Checked = False
          ckMovord2.Checked = False
          ckMovord3.Checked = False
          ckMovord4.Checked = False
          ckMovord.Enabled = False
          ckMovord1.Enabled = False
          ckOrdlist.Enabled = False
          ckZzdispsca.Enabled = False
          ckMovord2.Enabled = False
          ckMovord3.Enabled = False
          ckMovord4.Enabled = False
          ckTaskPM.Checked = False
          ckSolotaskril.Checked = False
          ckTaskPM.Enabled = False
          ckSolotaskril.Enabled = False
          pnPianificazione.Enabled = False
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          ckRilasciato.Enabled = True
          cbRilasciato.Enabled = True
          lbRilasciato.Enabled = True
          tlbAccorpa.Enabled = False
          tlbStampaWord.Enabled = False
          tlbStampaWordRaggr.Enabled = False
          If pnTipoStampa.Enabled = True Then
            opTipoStampa0.Checked = True
            opTipoStampa0.Enabled = False
            opTipoStampa1.Enabled = False
            pnTipoStampa.Enabled = False
          End If
        Case 4
          If pnPianificazione.Enabled = False Then
            GctlSetVisEnab(pnPianificazione, False)
            GctlSetVisEnab(ckMovord, False)
            GctlSetVisEnab(ckMovord1, False)
            GctlSetVisEnab(ckOrdlist, False)
            GctlSetVisEnab(ckZzdispsca, False)
            GctlSetVisEnab(ckMovord2, False)
            GctlSetVisEnab(ckMovord3, False)
            If oCleScho.bModAS = True Then
              GctlSetVisEnab(ckMovord4, False)
              ckMovord4.Checked = False
            End If
            ckMovord1.Checked = True
            ckOrdlist.Checked = True
            ckZzdispsca.Checked = True
            ckMovord2.Checked = False
            ckMovord3.Checked = False
            ckTaskPM.Checked = False
            ckSolotaskril.Checked = False
            GctlSetVisEnab(ckTaskPM, False)
            GctlSetVisEnab(ckSolotaskril, False)
            cbTipork.SelectedValue = "$"
            cbTipork.Enabled = False
            ckTipork.Checked = False
            ckTipork.Enabled = False
            ckEvasi.Checked = True
            ckEvasi.Enabled = False
            If ckSalto.Visible = False Then GctlSetVisEnab(ckSalto, True)
            ckRilasciato.Checked = False
            ckRilasciato.Enabled = False
            cbRilasciato.Enabled = False
            lbRilasciato.Enabled = False
          End If
          GctlSetVisEnab(tlbAccorpa, False)
          tlbStampaWord.Enabled = False
          tlbStampaWordRaggr.Enabled = False
          If pnTipoStampa.Enabled = True Then
            opTipoStampa0.Checked = True
            opTipoStampa0.Enabled = False
            opTipoStampa1.Enabled = False
            pnTipoStampa.Enabled = False
          End If
        Case 5
          ckSalto.Checked = False
          ckSalto.TabStop = False
          ckSalto.Visible = False
          ckMovord.Checked = False
          ckMovord1.Checked = False
          ckOrdlist.Checked = False
          ckZzdispsca.Checked = False
          ckMovord2.Checked = False
          ckMovord3.Checked = False
          ckMovord4.Checked = False
          ckMovord.Enabled = False
          ckMovord1.Enabled = False
          ckOrdlist.Enabled = False
          ckZzdispsca.Enabled = False
          ckMovord2.Enabled = False
          ckMovord3.Enabled = False
          ckMovord4.Enabled = False
          ckTaskPM.Checked = False
          ckSolotaskril.Checked = False
          ckTaskPM.Enabled = False
          ckSolotaskril.Enabled = False
          pnPianificazione.Enabled = False
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          ckRilasciato.Enabled = True
          cbRilasciato.Enabled = True
          lbRilasciato.Enabled = True
          tlbAccorpa.Enabled = False
          tlbStampaWord.Enabled = False
          tlbStampaWordRaggr.Enabled = False
          If pnTipoStampa.Enabled = True Then
            opTipoStampa0.Checked = True
            opTipoStampa0.Enabled = False
            opTipoStampa1.Enabled = False
            pnTipoStampa.Enabled = False
          End If
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbRilasciato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRilasciato.SelectedIndexChanged
    Try
      If cbRilasciato.SelectedValue = "N" Then
        ckRilasciato.Enabled = True
      Else
        ckRilasciato.Enabled = False
        ckRilasciato.Checked = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbCodart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCodart.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbCodart.SelectedValue)
        Case "A"
          edCodlsar.Text = "0"
          edCodlsar.Enabled = False
          edCodlsar.Visible = False
          lbDescodlsar.Text = ""
          lbDescodlsar.Visible = False
          GctlSetVisEnab(edDacodart, False)
          GctlSetVisEnab(edAcodart, False)
          GctlSetVisEnab(edDacodart, True)
          GctlSetVisEnab(edAcodart, True)
          GctlSetVisEnab(lbFaseini, False)
          GctlSetVisEnab(edFaseini, False)
          GctlSetVisEnab(edFasefin, False)
          edDacodart.Focus()
        Case "B"
          edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
          edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
          edDacodart.Enabled = False
          edAcodart.Enabled = False
          edDacodart.Visible = False
          edAcodart.Visible = False
          edFaseini.Text = "0"
          edFasefin.Text = "".PadLeft(4, "9"c)
          lbFaseini.Enabled = False
          edFaseini.Enabled = False
          edFasefin.Enabled = False
          GctlSetVisEnab(edCodlsar, False)
          GctlSetVisEnab(edCodlsar, True)
          GctlSetVisEnab(lbDescodlsar, True)
          edCodlsar.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbConto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbConto.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbConto.SelectedValue)
        Case "A"
          edCodlsel.Text = "0"
          edCodlsel.Enabled = False
          edCodlsel.Visible = False
          lbDescodlsel.Text = ""
          lbDescodlsel.Visible = False
          GctlSetVisEnab(edDaconto, False)
          GctlSetVisEnab(edAconto, False)
          GctlSetVisEnab(edDaconto, True)
          GctlSetVisEnab(edAconto, True)
          edDaconto.Focus()
        Case "B"
          edDaconto.Text = "0"
          edAconto.Text = "".PadLeft(9, "9"c)
          edDaconto.Enabled = False
          edAconto.Enabled = False
          edDaconto.Visible = False
          edAconto.Visible = False
          GctlSetVisEnab(edCodlsel, False)
          GctlSetVisEnab(edCodlsel, True)
          GctlSetVisEnab(lbDescodlsel, True)
          edCodlsel.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckSelAnnoStag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSelAnnoStag.CheckedChanged
    Try
      If ckSelAnnoStag.Checked = True Then
        GctlSetVisEnab(lbAnnotco, False)
        GctlSetVisEnab(lbCodstag, False)
        GctlSetVisEnab(edAnnotco, False)
        GctlSetVisEnab(edCodstag, False)
        edAnnotco.Text = NTSCStr(Year(Now))
        edCodstag.Text = "0"
      Else
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Text = NTSCStr(Year(Now))
        edCodstag.Text = "0"
        lbXx_Codstag.Text = ""
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTipork_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTipork.CheckedChanged
    Try
      If ckTipork.Checked = True Then
        GctlSetVisEnab(cbTipork, False)
        cbTipork.SelectedIndex = 0
      Else
        cbTipork.SelectedValue = "$"
        cbTipork.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        If edSerie.Text = "" Then edSerie.Text = " "
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdClassifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassifica.Click
    Dim oParam As New CLE__PATB
    Dim strClassificaFilter() As String = Nothing
    Try
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTCLAS", DittaCorrente, oParam)
      If NTSZOOM.strIn <> "" Then
        strClassificaFilter = NTSZOOM.strIn.Split("|"c)
        edClassificazioneLivello1.Text = strClassificaFilter(0)
        edClassificazioneLivello2.Text = strClassificaFilter(1)
        edClassificazioneLivello3.Text = strClassificaFilter(2)
        edClassificazioneLivello4.Text = strClassificaFilter(3)
        edClassificazioneLivello5.Text = strClassificaFilter(4)
        lbClassifica.Text = oMenu.GetArtclasDescr(DittaCorrente, strClassificaFilter(0), strClassificaFilter(1), _
                                                  strClassificaFilter(2), strClassificaFilter(3), strClassificaFilter(4))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdClassificaDeleteFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassificaDeleteFilter.Click
    Try
      edClassificazioneLivello1.Text = ""
      edClassificazioneLivello2.Text = ""
      edClassificazioneLivello3.Text = ""
      edClassificazioneLivello4.Text = ""
      edClassificazioneLivello5.Text = ""
      lbClassifica.Text = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function AggiornaRilasci() As Boolean
    Dim strDacodart As String = ""
    Dim strAcodart As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen) Then strDacodart = edDacodart.Text.ToUpper Else strDacodart = edDacodart.Text
      If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then strAcodart = edAcodart.Text.ToUpper Else strAcodart = edAcodart.Text
      '--------------------------------------------------------------------------------------------------------------
      oCleScho.AggiornaRilasci(edDacodart.Text, edAcodart.Text, edDamagaz.Text, edAmagaz.Text, _
                               edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                               edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                               edCommecaini.Text, edCommecafin.Text, edFaseini.Text, edFasefin.Text, _
                               ckEvasi.Checked, edGruppo.Text, edSottogr.Text, edSerie.Text, _
                               cbTipork.Enabled, cbTipork.SelectedValue, cbRilasciato.SelectedValue, edCodcfam.Text, _
                               ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                               ceFiltriExt.GeneraQuerySQL(), edClassificazioneLivello1.Text, _
                               edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                               edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                               cbConto.SelectedValue, NTSCInt(edCodlsel.Text), _
                               cbCodart.SelectedValue, NTSCInt(edCodlsar.Text))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttRilasciato As New DataTable()
      Dim dttTipork As New DataTable()
      Dim dttOrdin As New DataTable()
      Dim dttCodart As New DataTable()
      Dim dttConto As New DataTable()

      dttRilasciato.Columns.Add("cod", GetType(String))
      dttRilasciato.Columns.Add("val", GetType(String))
      dttRilasciato.Rows.Add(New Object() {"N", "Non rilasciati"})
      dttRilasciato.Rows.Add(New Object() {"S", "Rilasciati"})
      dttRilasciato.Rows.Add(New Object() {"E", "Entrambi"})
      dttRilasciato.AcceptChanges()
      cbRilasciato.DataSource = dttRilasciato
      cbRilasciato.ValueMember = "cod"
      cbRilasciato.DisplayMember = "val"

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"$", "Ord.for.aperto"})
      dttTipork.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.Rows.Add(New Object() {"V", "Imp.cli.aperto"})
      dttTipork.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipork.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      If oCleScho.bModAS = True Then
        dttTipork.Rows.Add(New Object() {"#", "Impegno di commessa"})
      End If
      dttTipork.AcceptChanges()
      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"

      dttOrdin.Columns.Add("cod", GetType(Integer))
      dttOrdin.Columns.Add("val", GetType(String))
      dttOrdin.Rows.Add(New Object() {1, "Per Articolo"})
      dttOrdin.Rows.Add(New Object() {2, "Per Conto"})
      dttOrdin.Rows.Add(New Object() {3, "Per Articolo/magazzino/commessa"})
      dttOrdin.Rows.Add(New Object() {4, "Per Articolo Pianificazione"})
      dttOrdin.Rows.Add(New Object() {5, "Per Commessa/articolo"})
      dttOrdin.AcceptChanges()
      cbOrdin.DataSource = dttOrdin
      cbOrdin.ValueMember = "cod"
      cbOrdin.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttCodart.Columns.Add("cod", GetType(String))
      dttCodart.Columns.Add("val", GetType(String))
      dttCodart.Rows.Add(New Object() {"A", "Articolo DA/A"})
      dttCodart.Rows.Add(New Object() {"B", "Lista selezionata"})
      dttCodart.AcceptChanges()
      cbCodart.DataSource = dttCodart
      cbCodart.ValueMember = "cod"
      cbCodart.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttConto.Columns.Add("cod", GetType(String))
      dttConto.Columns.Add("val", GetType(String))
      dttConto.Rows.Add(New Object() {"A", "Conto DA/A"})
      dttConto.Rows.Add(New Object() {"B", "Lista selezionata"})
      dttConto.AcceptChanges()
      cbConto.DataSource = dttConto
      cbConto.ValueMember = "cod"
      cbConto.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      CaricaFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaDataSetWord(ByRef ds As DataSet)
    Try
      ds = New DataSet()
      ds.Tables.Add("WORD")
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_dadatcons", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_adatcons", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_dadatord", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_adatord", GetType(Date)))
      ds.AcceptChanges()

      ds.Tables("WORD").Rows.Add(ds.Tables("WORD").NewRow)

      ds.Tables("WORD").Rows(0)!xx_dadatcons = NTSCStr(edDadatcons.Text)
      ds.Tables("WORD").Rows(0)!xx_adatcons = NTSCStr(edAdatcons.Text)
      ds.Tables("WORD").Rows(0)!xx_dadatord = NTSCStr(edDadatord.Text)
      ds.Tables("WORD").Rows(0)!xx_adatord = NTSCStr(edAdatord.Text)
      ds.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Try
      If NTSCInt(edDamagaz.Text) > NTSCInt(edAmagaz.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499815189918, "Il magazzino di partenza non può essere superiore a quello di arrivo."))
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
        Return False
      End If
      If NTSCInt(edDaagente.Text) > NTSCInt(edAagente.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499785346168, "L'agente di partenza non può essere superiore a quello di arrivo."))
        edDaagente.Text = "0"
        edAagente.Text = "9999"
        Return False
      End If
      If NTSCDate(edDadatord.Text) > NTSCDate(edAdatord.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499747689918, "La data ordine di partenza non può essere superiore a quella di arrivo."))
        edDadatord.Text = IntSetDate("01/01/1900")
        edAdatord.Text = IntSetDate("31/12/2099")
        Return False
      End If

      If NTSCDate(edDadatcons.Text) > NTSCDate(edAdatcons.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499730189918, "La data consegna di partenza non può essere superiore a quella di arrivo."))
        edDadatcons.Text = IntSetDate("01/01/1900")
        edAdatcons.Text = IntSetDate("31/12/2099")
        Return False
      End If
      If NTSCInt(edDaconto.Text) > NTSCInt(edAconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499709721168, "Il conto di partenza non può essere superiore a quello di arrivo."))
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        Return False
      End If
      If UCase(edDacodart.Text) > UCase(edAcodart.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499598002418, "L'articolo di partenza non può essere superiore a quello di arrivo."))
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Return False
      End If
      If NTSCInt(edCommecaini.Text) > NTSCInt(edCommecafin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499574408668, "La commessa di partenza non può essere superiore a quella di arrivo."))
        edCommecaini.Text = "0"
        edCommecafin.Text = "999999999"
        Return False
      End If
      If NTSCInt(edFaseini.Text) > NTSCInt(edFasefin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499553627418, "La fase di partenza non può essere superiore a quella di arrivo."))
        edFaseini.Text = "0"
        edFasefin.Text = "9999"
        Return False
      End If
      If NTSCInt(cbOrdin.SelectedValue) = 4 Then
        If oCleScho.bModAS = False Then
          If (ckMovord.Checked = False) And (ckMovord1.Checked = False) And _
             (ckOrdlist.Checked = False) And (ckZzdispsca.Checked = False) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128595499533783668, "Selezionare almeno un tipo di Pianificazione."))
            Return False
          End If
        Else
          If (ckMovord.Checked = False) And (ckMovord1.Checked = False) And _
             (ckMovord4.Checked = False) And _
             (ckOrdlist.Checked = False) And (ckZzdispsca.Checked = False) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128595499511908668, "Selezionare almeno un tipo di Pianificazione."))
            Return False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (cbConto.SelectedValue = "B") And (NTSCInt(edCodlsel.Text) = 0) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130383259056151689, "Attenzione!" & vbCrLf & _
          "Indicare un codice lista selezionata Clienti/Fornitore valido."))
        edCodlsel.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (cbCodart.SelectedValue = "B") And (NTSCInt(edCodlsar.Text) = 0) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130383259719691105, "Attenzione!" & vbCrLf & _
          "Indicare un codice lista selezionata Articoli valido."))
        edCodlsar.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiFormula() As String
    Dim strC As String = ""
    Dim strAggwhereCRM As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckSerie.Checked = False Then edSerie.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      edDacodart.Text = edDacodart.Text.ToUpper
      edAcodart.Text = edAcodart.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il tipo di stampa è 'Per Articolo Pianificazione', non serve filtrare
      '--- gli eventuali dati CRM dato che lo fa già riempiendo la tabella temporanea TTSCHO
      '--------------------------------------------------------------------------------------------------------------
      strAggwhereCRM = ""
      If NTSCInt(cbOrdin.SelectedValue) <> 4 Then
        If (oCleScho.bModuloCRM = True) And (oCleScho.bIsCRMUser = True) Then
          strAggwhereCRM = " And ("
          If oCleScho.strAccvis <> "T" Then
            strAggwhereCRM += "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
            Select Case oCleScho.strAccvis
              Case "P" : strAggwhereCRM += " And {leads.le_opinc} = " & oCleScho.lCodorgaOperat
              Case "C" : strAggwhereCRM += " And {leads.le_opinc} In [" & oCleScho.strRegvis & "]"
            End Select
            strAggwhereCRM += ")"
          Else '--- Tutti i clienti
            strAggwhereCRM += "  {anagra.an_tipo} = 'C'"
          End If
          If oCleScho.bAmm = False Then
            strAggwhereCRM += " And {anagra.an_tipo} <> 'F'"
          Else
            strAggwhereCRM += " Or {anagra.an_tipo} <> 'C'"
          End If
          strAggwhereCRM += ")"
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(cbOrdin.SelectedValue) = 4 Then 'Articolo Pianificazione
        strC = "{TTSCHO.codditt} = '" & DittaCorrente & "'" & _
          " And {TTSCHO.instid} = " & oCleScho.lIITTScho
      Else
        strC = "{keyord.codditt} = '" & DittaCorrente & "'" & _
          IIf(ckEvasi.Checked = True, " And {movord.mo_flevas} = 'C'", "").ToString & _
          IIf(NTSCInt(edGruppo.Text) <> 0, " And {artico.ar_gruppo} = " & edGruppo.Text, "").ToString & _
          IIf(NTSCInt(edSottogr.Text) <> 0, " And {artico.ar_sotgru} = " & edSottogr.Text, "").ToString & _
          IIf(edClassificazioneLivello1.Text.Trim <> "", " And {artico.ar_codcla1} = '" & edClassificazioneLivello1.Text & "'", "").ToString & _
          IIf(edClassificazioneLivello2.Text.Trim <> "", " And {artico.ar_codcla2} = '" & edClassificazioneLivello2.Text & "'", "").ToString & _
          IIf(edClassificazioneLivello3.Text.Trim <> "", " And {artico.ar_codcla3} = '" & edClassificazioneLivello3.Text & "'", "").ToString & _
          IIf(edClassificazioneLivello4.Text.Trim <> "", " And {artico.ar_codcla4} = '" & edClassificazioneLivello4.Text & "'", "").ToString & _
          IIf(edClassificazioneLivello5.Text.Trim <> "", " And {artico.ar_codcla5} = '" & edClassificazioneLivello5.Text & "'", "").ToString & _
          IIf(ckSerie.Checked = True, " And {keyord.ko_serie} = '" & edSerie.Text & "'", "").ToString & _
          IIf(cbTipork.Enabled, " And {keyord.ko_tipork} = '" & cbTipork.SelectedValue & "'", "").ToString
        If (NTSCInt(edDamagaz.Text) <> 0) Or (NTSCInt(edAmagaz.Text) <> 9999) Then
          strC += " And {keyord.ko_magaz} In " & edDamagaz.Text & " To " & edAmagaz.Text
        End If
        If (NTSCInt(edDaagente.Text) <> 0) Or (NTSCInt(edAagente.Text) <> 9999) Then
          strC += " And {testord.td_codagen} In " & edDaagente.Text & " To " & edAagente.Text
        End If
        If (NTSCDate(edDadatord.Text) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(edAdatord.Text) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {testord.td_datord} In " & ConvDataRpt(edDadatord.Text) & " To " & ConvDataRpt(edAdatord.Text)
        End If
        If (NTSCDate(edDadatcons.Text) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(edAdatcons.Text) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {keyord.ko_datcons} In " & ConvDataRpt(edDadatcons.Text) & " To " & ConvDataRpt(edAdatcons.Text)
        End If
        If (NTSCInt(edCommecaini.Text) <> 0) Or (NTSCInt(edCommecafin.Text) <> 999999999) Then
          strC += " And {movord.mo_commeca} In " & edCommecaini.Text & " To " & (edCommecafin.Text)
        End If
        Select Case cbRilasciato.SelectedValue
          Case "N" : strC += " And {movord.mo_rilasciato} = 'N'"
          Case "S" : strC += " And {movord.mo_rilasciato} = 'S'"
        End Select
        Select Case cbConto.SelectedValue
          Case "A"
            If (NTSCInt(edDaconto.Text) <> 0) Or (NTSCInt(edAconto.Text) <> 999999999) Then
              strC += " And {keyord.ko_conto} In " & edDaconto.Text & " To " & edAconto.Text
            End If
          Case "B"
            If NTSCInt(edCodlsel.Text) > 0 Then
              If oCleScho.RitornaLISTSEL(NTSCInt(edCodlsel.Text), dttTmp) = True Then
                strC += " And {keyord.ko_conto} In ["
                For i As Integer = 0 To (dttTmp.Rows.Count - 1)
                  If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                    strC += NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  Else
                    strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                      " Or {keyord.ko_conto} In [" & _
                      NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  End If
                Next
              End If
            End If
        End Select
        Select Case cbCodart.SelectedValue
          Case "A"
            If (edDacodart.Text <> "".PadLeft(18)) Or (edAcodart.Text <> "".PadLeft(18, "z"c)) Then
              strC += " And {keyord.ko_codart} In '" & edDacodart.Text & "' To '" & edAcodart.Text & "'"
            End If
            If (NTSCInt(edFaseini.Text) <> 0) Or (NTSCInt(edFasefin.Text) <> 9999) Then
              strC += " And {movord.mo_fase} In " & edFaseini.Text & " To " & edFasefin.Text
            End If
          Case "B"
            If NTSCInt(edCodlsar.Text) > 0 Then
              If oCleScho.RitornaLISTSAR(NTSCInt(edCodlsar.Text), dttTmp) = True Then
                strC += " And {keyord.ko_codart} + '.' + ToText({movord.mo_fase}, 0, '') In ["
                For i As Integer = 0 To (dttTmp.Rows.Count - 1)
                  If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                    strC += "'" & NTSCStr(dttTmp.Rows(i)!lsa_codart) & "." & NTSCStr(dttTmp.Rows(i)!lsa_fase) & "'" & _
                      IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  Else
                    strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                      " Or {keyord.ko_codart} + '.' + ToText({movord.mo_fase}, 0, '') In [" & _
                      "'" & NTSCStr(dttTmp.Rows(i)!lsa_codart) & "." & NTSCStr(dttTmp.Rows(i)!lsa_fase) & "'" & _
                      IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  End If
                Next
              End If
            End If
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbOrdin.SelectedValue
        Case "3" 'Articolo/magazzino/commessa
          strC += " And {artico.ar_gescomm} = 'S'"
        Case "5" 'Commessa/articolo
          strC += " And {artico.ar_gescomm} = 'S'"
      End Select
      If edCodcfam.Text.Trim <> "" Then strC += " And {artico.ar_famprod} = '" & edCodcfam.Text & "'"
      If (oCleScho.bModTCO = True) And (ckSelAnnoStag.Checked = True) Then
        If NTSCInt(edAnnotco.Text) > 0 Then strC += " And {artico.ar_anno} = " & edAnnotco.Text
        If NTSCInt(edCodstag.Text) > 0 Then strC += " And {artico.ar_codstag} = " & edCodstag.Text
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      strC += strAggwhereCRM
      '--------------------------------------------------------------------------------------------------------------
      Dim strFiltriExt As String = ceFiltriExt.GeneraQueryReport()
      If strFiltriExt <> "" Then strC += " AND " & strFiltriExt
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = strC
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function Elabora() As Boolean
    Dim nValoriz As Integer = 0
    Dim nMagaz As Integer = 0

    Try
      Me.ValidaLastControl()

      If Not CheckIntervalli() Then Return False

      Me.Cursor = Cursors.WaitCursor

      '-------------------------
      'eseguo l'elaborazione
      oCleScho.Elabora(edDacodart.Text, edAcodart.Text, edDamagaz.Text, edAmagaz.Text, _
                      edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                      edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                      edCommecaini.Text, edCommecafin.Text, edFaseini.Text, edFasefin.Text, _
                      ckEvasi.Checked, edGruppo.Text, edSottogr.Text, edSerie.Text, _
                      cbTipork.Enabled, edSerie.Text, cbRilasciato.SelectedValue, edCodcfam.Text, _
                      ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                      ckMovord.Checked, ckMovord4.Checked, ckMovord1.Checked, ckMovord2.Checked, _
                      ckMovord3.Checked, ckOrdlist.Checked, ckZzdispsca.Checked, ckTaskPM.Checked, _
                      ckSolotaskril.Checked, ceFiltriExt.GeneraQuerySQL(), _
                      edClassificazioneLivello1.Text, edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                      edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                      cbConto.SelectedValue, NTSCInt(edCodlsel.Text), cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), tlbAccorpa.Checked)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

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
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ReportSuGriglia()
    Dim strCodart As String
    Dim nMagaz As Integer
    Dim oParam As New CLE__CLDP
    Dim frmGrso As FRMORGRSO = Nothing
    Dim frmGrs1 As FRMORGRS1 = Nothing
    Dim frmGrs2 As FRMORGRS2 = Nothing
    Try
      Me.ValidaLastControl()
      If Not CheckIntervalli() Then Exit Sub
      oCleScho.strAltriFiltri = ceFiltriExt.GeneraQuerySQL()

      lbStatus.Text = oApp.Tr(Me, 130421107104306276, "Selezione dati in corso...")
      '--------------------------------------------------------------------------------------------------------------
      oCleScho.strSchoConto = cbConto.SelectedValue
      oCleScho.nSchoCodlsel = NTSCInt(edCodlsel.Text)
      oCleScho.strSchoCodart = cbCodart.SelectedValue
      oCleScho.nSchoCodlsar = NTSCInt(edCodlsar.Text)
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(cbOrdin.SelectedValue) = 4 Then
        strCodart = ""
        nMagaz = 0
RiapriGriglia:
        Elabora()
        If Not oCleScho.CheckTmpTable() Then Exit Sub
        oCleScho.lIITTScho = oCleScho.lIITTScho
        oCleScho.strSch1Dadatcons = edDadatcons.Text
        oCleScho.strSch1Adatcons = edAdatcons.Text
        oCleScho.bSch1AggiornaOrdine = False
        oCleScho.strSch1Codart = strCodart
        oCleScho.nSch1Magaz = nMagaz
        oCleScho.bGrs1ModTCO = oCleScho.bModTCO
        oCleScho.bGrs1Accorpa = tlbAccorpa.Checked

        frmGrs1 = CType(NTSNewFormModal("FRMORGRS1"), FRMORGRS1)
        frmGrs1.Init(oMenu, oParam, DittaCorrente)
        frmGrs1.InitEntity(oCleScho)
        If Not oCallParams Is Nothing Then
          frmGrs1.bNoModal = False
        Else
          frmGrs1.bNoModal = tlbNoModal.Checked
        End If
        If frmGrs1.bNoModal Then
          frmGrs1.Show()
        Else
          frmGrs1.ShowDialog()
        End If

        If frmGrs1.bClose = True Then oApp.MsgBoxErr(oApp.Tr(Me, 128756210954795578, "Non esistono dati con queste caratteristiche."))

        '-------------------------------------------------------------------
        ' Se è stato richiesto un refresh degli ordini appena visualizzati
        ' rielabora e riapre la finestra
        '-------------------------------------------------------------------
        If oCleScho.bSch1AggiornaOrdine Then
          strCodart = oCleScho.strSch1Codart
          nMagaz = oCleScho.nSch1Magaz
          GoTo RiapriGriglia
        End If
        '-------------------------------------------------------------------
      Else
        oCleScho.nSchoDamagaz = NTSCInt(edDamagaz.Text)
        oCleScho.nSchoAmagaz = NTSCInt(edAmagaz.Text)
        oCleScho.nSchoDaagente = NTSCInt(edDaagente.Text)
        oCleScho.nSchoAagente = NTSCInt(edAagente.Text)
        oCleScho.strSchoDadatord = edDadatord.Text
        oCleScho.strSchoAdatord = edAdatord.Text
        oCleScho.strSchoDadatcons = edDadatcons.Text
        oCleScho.strSchoAdatcons = edAdatcons.Text
        oCleScho.lSchoDaconto = NTSCInt(edDaconto.Text)
        oCleScho.lSchoAconto = NTSCInt(edAconto.Text)
        If Not edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen) Then
          oCleScho.strSchoDacodart = UCase$(edDacodart.Text)
        Else
          oCleScho.strSchoDacodart = edDacodart.Text
        End If
        If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then
          oCleScho.strSchoAcodart = UCase$(edAcodart.Text)
        Else
          oCleScho.strSchoAcodart = edAcodart.Text
        End If
        oCleScho.lSchoCommecaini = NTSCInt(edCommecaini.Text)
        oCleScho.lSchoCommecafin = NTSCInt(edCommecafin.Text)
        oCleScho.nSchoFaseini = NTSCInt(edFaseini.Text)
        oCleScho.nSchoFasefin = NTSCInt(edFasefin.Text)
        oCleScho.nSchoGruppo = NTSCInt(edGruppo.Text)
        oCleScho.nSchoSottogr = NTSCInt(edSottogr.Text)
        oCleScho.strSchoClassLivello1 = edClassificazioneLivello1.Text
        oCleScho.strSchoClassLivello2 = edClassificazioneLivello2.Text
        oCleScho.strSchoClassLivello3 = edClassificazioneLivello3.Text
        oCleScho.strSchoClassLivello4 = edClassificazioneLivello4.Text
        oCleScho.strSchoClassLivello5 = edClassificazioneLivello5.Text
        oCleScho.strSchoSerie = Trim(edSerie.Text)
        If ckEvasi.Checked = True Then oCleScho.bSchoEvasi = True Else oCleScho.bSchoEvasi = False
        If cbTipork.Enabled Then
          oCleScho.strSchoTipork = cbTipork.SelectedValue
        Else
          oCleScho.strSchoTipork = ""
        End If
        oCleScho.strSchoRilasciato = cbRilasciato.SelectedValue
        Select Case NTSCInt(cbOrdin.SelectedValue)
          Case 1 : oCleScho.strSchoOrdin = "A"
          Case 2 : oCleScho.strSchoOrdin = "C"
          Case 3 : oCleScho.strSchoOrdin = "X"
        End Select
        If ckRilasciato.Checked = True Then
          lbStatus.Text = oApp.Tr(Me, 130421107235557956, "Aggiornamento status 'Rilasciato' su righe ordini in corso...")
          AggiornaRilasci()
        End If
        lbStatus.Text = strProntoMess
        If NTSCInt(cbOrdin.SelectedValue) <> 5 Then 'Commessa/articolo
          oCleScho.strGrsoCodcfam = Trim(edCodcfam.Text)
          oCleScho.bGrsoModTCO = oCleScho.bModTCO
          If oCleScho.bModTCO = True Then
            If ckSelAnnoStag.Checked = True Then
              oCleScho.nGrsoAnnotco = NTSCInt(edAnnotco.Text)
              oCleScho.nGrsoCodstag = NTSCInt(edCodstag.Text)
            Else
              oCleScho.nGrsoAnnotco = 0
              oCleScho.nGrsoCodstag = 0
            End If
          Else
            oCleScho.nGrsoAnnotco = 0
            oCleScho.nGrsoCodstag = 0
          End If

          frmGrso = CType(NTSNewFormModal("FRMORGRSO"), FRMORGRSO)
          frmGrso.Init(oMenu, oParam, DittaCorrente)
          frmGrso.InitEntity(oCleScho)
          If Not oCallParams Is Nothing Then
            frmGrso.bNoModal = False
          Else
            frmGrso.bNoModal = tlbNoModal.Checked
          End If
          If frmGrso.bNoModal Then
            frmGrso.Show()
          Else
            frmGrso.ShowDialog()
          End If

          If frmGrso.bClose = True Then oApp.MsgBoxErr(oApp.Tr(Me, 128756212996332532, "Non esistono dati con queste caratteristiche."))
        Else
          oCleScho.strSchoOrdin = "S"
          oCleScho.strGrs2Codcfam = Trim(edCodcfam.Text)
          oCleScho.bGrs2ModTCO = oCleScho.bModTCO
          If oCleScho.bModTCO = True Then
            If ckSelAnnoStag.Checked = True Then
              oCleScho.nGrs2Annotco = NTSCInt(edAnnotco.Text)
              oCleScho.nGrs2Codstag = NTSCInt(edCodstag.Text)
            Else
              oCleScho.nGrs2Annotco = 0
              oCleScho.nGrs2Codstag = 0
            End If
          Else
            oCleScho.nGrs2Annotco = 0
            oCleScho.nGrs2Codstag = 0
          End If

          frmGrs2 = CType(NTSNewFormModal("FRMORGRS2"), FRMORGRS2)
          frmGrs2.Init(oMenu, oParam, DittaCorrente)
          frmGrs2.InitEntity(oCleScho)
          If Not oCallParams Is Nothing Then
            frmGrs2.bNoModal = False
          Else
            frmGrs2.bNoModal = tlbNoModal.Checked
          End If
          If frmGrs2.bNoModal Then
            frmGrs2.Show()
          Else
            frmGrs2.ShowDialog()
          End If

          If frmGrs2.bClose = True Then oApp.MsgBoxErr(oApp.Tr(Me, 128756213161174172, "Non esistono dati con queste caratteristiche."))
        End If
      End If
      If (oCleScho.bSchoDagest = True) And (oCleScho.bSchoFiltri = False) Then
        Me.Visible = True
        Me.Close()
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGrso Is Nothing Then
        If frmGrso.bNoModal = False Then
          frmGrso.Dispose()
          frmGrso = Nothing
        End If
      End If

      If Not frmGrs1 Is Nothing Then
        If frmGrs1.bNoModal = False Then
          frmGrs1.Dispose()
          frmGrs1 = Nothing
        End If
      End If

      If Not frmGrs2 Is Nothing Then
        If frmGrs2.bNoModal = False Then
          frmGrs2.Dispose()
          frmGrs2 = Nothing
        End If
      End If
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strNomRpt As String = ""
    Dim strKey As String = ""
    Dim sectionCode As Integer
    Dim nVisible As Integer
    Dim nNewPageBefore As Integer
    Dim nNewPageAfter As Integer
    Dim nKeepTogether As Integer
    Dim nSuppressBlankSection As Integer
    Dim nResetPageNAfter As Integer
    Dim nPrintAtBottomOfPage As Integer
    Dim lBackgroudColor As Integer
    Dim nUnderlaySection As Integer
    Dim nShowArea As Integer
    Dim nFreeFormPlacement As Integer
    Try

      '--------------------------------------------------
      Select Case NTSCInt(cbOrdin.SelectedValue)
        Case 1
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSORSCH6.RPT" : strKey = "Reports6"
          Else
            strNomRpt = "BSORSCH1.RPT" : strKey = "Reports1"
          End If
        Case 2
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSORSCH7.RPT" : strKey = "Reports7"
          Else
            strNomRpt = "BSORSCH2.RPT" : strKey = "Reports2"
          End If
        Case 3 : strNomRpt = "BSORSCH4.RPT" : strKey = "Reports4"
        Case 4
          If Not Elabora() Then Exit Sub
          If Not oCleScho.CheckTmpTable() Then Exit Sub
          strNomRpt = "BSORSCH3.RPT" : strKey = "Reports3"
        Case 5 : strNomRpt = "BSORSCH5.RPT" : strKey = "Reports5"
      End Select

      Me.Cursor = Cursors.WaitCursor

      strCrpe = ComponiFormula()

      '--------------------------------------------------
      'preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORSCHO", strKey, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede ordini", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DADATCONS", "'" & edDadatcons.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ADATCONS", "'" & edAdatcons.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DADATORD", "'" & edDadatord.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ADATORD", "'" & edDadatord.Text & "'")

        If i = 1 Then
          Select Case NTSCInt(cbOrdin.SelectedValue)
            Case 1, 3, 4
              If (ckSalto.Checked = True) Then
                sectionCode = oMenu.PE_GROUPHEADER

                nRis = oMenu.PEGetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
                nNewPageBefore = 1
                nRis = oMenu.PESetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
              End If
          End Select
        End If

        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      If ckRilasciato.Checked = True Then
        lbStatus.Text = oApp.Tr(Me, 128595632764171148, "Aggiornamento status 'Rilasciato' su righe ordini in corso...")
        AggiornaRilasci()
      End If

      lbStatus.Text = strProntoMess

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = strProntoMess
    End Try
  End Sub

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Select Case strIn.Substring(0, 1)
      Case "#"
        'Variabile esposte SPECIFICHE PER PROGRAMMA
        Select Case UCase$(Mid$(strIn, 2))
          Case "IITTSCHO"
            Return NTSCStr(oCleScho.lIITTScho)
          Case Else
            Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else
        Return MyBase.ResolveField(strIn)
    End Select
    Return ""
  End Function

#Region "Filtri Estesi"
  Public Overridable Function CheckFiltri1() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome) = "." Then
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda = ""
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea = ""
        Else
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584505571093750, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128756215234585132, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584506055937500, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503331406250, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503236718750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsFiltri.Tables("FILTRI1").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
    Try
      xx_nome.Enabled = Not xx_nome.Enabled
      grvFiltri1.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function CreaDatatableFiltri() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsFiltri.Tables.Add("FILTRI1")
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_nome", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valoreda", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valorea", GetType(String))
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dsFiltri.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNORSCHO", "RECENT", ".", "Filtri1", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsFiltri.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ComponiWhereFiltriEstesi(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQuery As String = ""
    Try

      If bCrystal Then
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda))
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea))
              Case 8
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).ToShortDateString)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).ToShortDateString)
              Case Else
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False)
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case 8
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case Else
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)
      End If


      Return strQuery

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      strOut = CStrSQL(strTesto & bFil)

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function
  Public Overridable Sub ApriZoomTabella(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try

      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = "MOVORD"
        oParam.strDescr = strCampo
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)

      Else

        SetFastZoom(NTSCStr(grvFiltri1.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Gestione filtri"
  Public Overridable Sub CaricaFiltri()
    Dim dttTmp As New DataTable
    Try
      oCleScho.CaricaFiltri(dttTmp)

      dttTmp.AcceptChanges()

      cbFiltro.DataSource = dttTmp
      cbFiltro.ValueMember = "cod"
      cbFiltro.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdApriFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriFiltri.Click
    Dim dttCampiForm As New DataTable
    Dim oPar As New CLE__CLDP
    Try
      'Prende la struttura della tabella
      oCleScho.GetTableStructMovIfil(dttCampiForm)

      dttCampiForm.Columns.Add("xx_descr")
      dttCampiForm.Columns.Add("xx_info")
      dttCampiForm.Columns.Add("xx_tipo")

      'Compongo il datatable con i campi da passare al programma per la gestione dei dati
      If Not ComponiDatatable(dttCampiForm, Me) Then Return

      'Riempie le colonne mancanti
      For z As Integer = 0 To dttCampiForm.Rows.Count - 1
        dttCampiForm.Rows(z)!mo_child = "BNORSCHO"
        dttCampiForm.Rows(z)!mo_form = "FRMORSCHO"
        dttCampiForm.Rows(z)!mo_locked = "N"
        dttCampiForm.Rows(z)!mo_codifil = NTSCInt(cbFiltro.SelectedValue)
      Next

      'Avvia il programma
      oPar.ctlPar1 = dttCampiForm
      oPar.strPar1 = "BNORSCHO"
      oPar.dPar1 = NTSCInt(cbFiltro.SelectedValue)

      oMenu.RunChild("NTSInformatica", "FRM__IFIL", oApp.Tr(Me, 129181266305000000, "Impostazione filtri"), DittaCorrente, "", "BN__IFIL", oPar, "", True, True)

      CaricaFiltri()
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbFiltro_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFiltro.SelectedValueChanged
    Try
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ComponiDatatable(ByRef dttCampiForm As DataTable, ByVal oControl As Control) As Boolean
    Dim dtrRow As DataRow
    Try
      'Verifico a quale controllo corrisponde e lo aggiungo al datatable dei campi.
      Select Case oControl.GetType().ToString
        Case "NTSInformatica.NTSButton"
          If oControl.Name = "cmdApriFiltri" Then Return True ' è un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = oControl.Text
            !mo_control = oControl.Name
            !mo_valore = ""
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxNum"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxNum).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxNum).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxNum"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxStr"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxStr).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxStr).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxStr"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxData"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxData).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxData"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSMemoBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSMemoBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSMemoBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSCheckBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSCheckBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSCheckBox).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSCheckBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSComboBox"
          If oControl.Name = "cbFiltro" Then Return True ' è un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSComboBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = CType(oControl, NTSComboBox).SelectedValue
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSComboBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSRadioButton"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSRadioButton).strNomeCampo
            !xx_info = CType(oControl, NTSRadioButton).Parent.Name
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSRadioButton).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSRadioButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSGrid"
          Select Case oControl.Name
            'Le griglie le gestisco a mano
            Case "grFilt"
              For z As Integer = 0 To ceFiltriExt.grvFilt.RowCount - 1
                dtrRow = ceFiltriExt.grvFilt.GetDataRow(z)
                If NTSCStr(dtrRow!xx_nome) = "-" Then Continue For

                dttCampiForm.Rows.Add()

                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_valore"
                  !xx_descr = oApp.Tr(Me, 129182143688273058, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo)
                  !mo_valore = dtrRow!xx_valore
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With

                dttCampiForm.Rows.Add()
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_valorea"
                  !xx_descr = oApp.Tr(Me, 129181522451445167, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo) & " (A)"
                  !mo_valore = dtrRow!xx_valorea
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With

                dttCampiForm.Rows.Add()
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFilt"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = dtrRow!xx_nome
                  !mo_colvalue = "xx_tipo"
                  !xx_descr = oApp.Tr(Me, 130606857219845115, "Filtri Estesi - ") & NTSCStr(dtrRow!xx_descampo) & " (Tipo operatore)"
                  !mo_valore = dtrRow!xx_tipo
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow!xx_tipocampo
                End With
              Next
            Case Else
              'Avviso se è una griglia non gestita
              'oApp.MsgBoxErr(oApp.Tr(Me, 129181211327812500, "Griglia '|" & oControl.Name & "|' non gestita"))
          End Select
          Return True
      End Select

      'Ricorsivamente scorro tutti i controlli
      For z As Integer = 0 To oControl.Controls.Count - 1
        If Not ComponiDatatable(dttCampiForm, oControl.Controls(z)) Then Return False
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApplicaFiltro(ByVal lCod As Integer) As Boolean
    Try
      If Me.NTSActiveFirstOccured = False Then Return True
      'Ripristina lo stato di default dei controlli
      GctlSetRoules()
      AggiornaForm(dttDefault, True)

      If lCod <> 0 Then
        'Carica il filtro e lo applica (la gctlSetRoules vince sempre)
        If Not oCleScho.LeggiFiltro(lCod, "BNORSCHO", "FRMORSCHO", dttPersForm) Then Return False
        AggiornaForm(dttPersForm, False)

        If Not grvFiltri1.NTSGetCurrentDataRow() Is Nothing Then grvFiltri1_NTSFocusedRowChanged(Me, Nothing)
      End If

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaForm(ByVal dttControl As DataTable, ByVal bDoEnable As Boolean) As Boolean
    Dim oControl As New Object
    Try
      'Ripristina la configurazione della griglia
      For z As Integer = 0 To dttControl.Rows.Count - 1
        oControl = NTSFindControlByName(Me, NTSCStr(dttControl.Rows(z)!mo_control))
        If oControl Is Nothing Then Continue For

        Select Case oControl.GetType().ToString
          Case "NTSInformatica.NTSButton"
            Dim cmdControl As NTSButton = CType(oControl, NTSButton)
            If cmdControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cmdControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxNum"
            Dim edControl As NTSTextBoxNum = CType(oControl, NTSTextBoxNum)
            edControl.Text = NTSCInt(dttControl.Rows(z)!mo_valore).ToString
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxStr"
            Dim edControl As NTSTextBoxStr = CType(oControl, NTSTextBoxStr)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxData"
            Dim edControl As NTSTextBoxData = CType(oControl, NTSTextBoxData)
            edControl.Text = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSMemoBox"
            Dim edControl As NTSMemoBox = CType(oControl, NTSMemoBox)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSCheckBox"
            Dim ckControl As NTSCheckBox = CType(oControl, NTSCheckBox)
            ckControl.Checked = CBool(dttControl.Rows(z)!mo_valore)
            If ckControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              ckControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSComboBox"
            Dim cbControl As NTSComboBox = CType(oControl, NTSComboBox)
            cbControl.SelectedValue = NTSCStr(dttControl.Rows(z)!mo_valore)
            If cbControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cbControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSRadioButton"
            Dim opControl As NTSRadioButton = CType(oControl, NTSRadioButton)
            If CBool(dttControl.Rows(z)!mo_valore) Then opControl.Checked = True
            If opControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              opControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSGrid"
            Dim grControl As NTSGrid = CType(oControl, NTSGrid)
            Dim dttTmp As DataTable = CType(CType(grControl.DataSource, BindingSource).DataSource, DataTable)
            Dim dtrRow() As DataRow

            dtrRow = dttTmp.Select(NTSCStr(dttControl.Rows(z)!mo_colkeyname) & " = '" & NTSCStr(dttControl.Rows(z)!mo_colkeyvalue) & "'")
            If dtrRow.Length > 0 Then
              'Se è di tipo data faccio la conversione, altrimenti no
              Dim dtrTmp() As DataRow = ceFiltriExt.dttFilt.Select("xx_nome = " & CStrSQL(dttControl.Rows(z)!mo_colkeyvalue))
              If NTSCInt(dtrTmp(0)!xx_tipocampo) = 8 Then ' 8 = data
                dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
              Else
                dtrRow(0)(NTSCStr(dttControl.Rows(z)!mo_colvalue)) = NTSCStr(dttControl.Rows(z)!mo_valore)
              End If
            End If
            ' L'enable\disable dei vari campi lo gestisco con lo spostamento nella griglia
        End Select
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvertiInData(ByVal strData As String) As String
    Dim strPart() As String
    Try
      strData = strData.Trim

      'Data odierna o oggi è quella del giorno di stampa
      If strData.ToLower = "data odierna" Or strData.ToLower = "oggi" Or _
         strData.ToLower = "today" Or strData.ToLower = "now" Or _
         strData.ToLower = oApp.Tr(Me, 129223786674775598, "data odierna").ToLower Or _
         strData.ToLower = oApp.Tr(Me, 129223786568213098, "oggi").ToLower Then Return Now.ToShortDateString

      'Non è di tipo "+1 Giorno", ecc... tutti con uno spazio in mezzo, quindi è una data classica
      strPart = strData.Split(" "c)

      If strPart.Length = 1 Then Return strData

      'dovrebbero essere supportati solo i nomi italiani, ma aggiungerne anche altri non costa niente.
      Select Case strPart(1).ToLower
        Case "giorni", "giorno", "g", "day", "days", "d", oApp.Tr(Me, 129223783990556848, "giorni").ToLower, oApp.Tr(Me, 129223784107275598, "giorno").ToLower
          Return Now.AddDays(NTSCInt(strPart(0))).ToShortDateString
        Case "mese", "mesi", "month", "months", "m", oApp.Tr(Me, 129223784378681848, "mese").ToLower, oApp.Tr(Me, 129223784395244348, "mesi").ToLower
          Return Now.AddMonths(NTSCInt(strPart(0))).ToShortDateString
        Case "anno", "anni", "a", "year", "years", "y", oApp.Tr(Me, 129223784387119348, "anno").ToLower, oApp.Tr(Me, 129223784402744348, "anni").ToLower
          Return Now.AddYears(NTSCInt(strPart(0))).ToShortDateString
      End Select

      'Se non riesce a codificarla in nulla ritorna la data di oggi e avvisa
      oApp.MsgBoxErr(oApp.Tr(Me, 129223785065556848, "Non è stato possibile tradurre la data '|" & strData & "|'"))
      Return Now.ToShortDateString
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub grvFiltri1_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFiltri1.NTSFocusedRowChanged
    Dim dtrRow() As DataRow
    Try
      ' se non c'è una impostazione attiva le colonne devono rimanere come sono
      If NTSCInt(cbFiltro.SelectedValue) = 0 Then Return

      dtrRow = dttPersForm.Select("mo_colkeyvalue = '" & NTSCStr(grvFiltri1.NTSGetCurrentDataRow()!xx_nome) & "'")

      For z As Integer = 0 To dtrRow.Length - 1
        CType(grvFiltri1.Columns(NTSCStr(dtrRow(z)!mo_colvalue)), NTSGridColumn).Enabled = CBool(IIf(NTSCStr(dtrRow(z)!mo_locked) = "S", False, True))
      Next

    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Sub
#End Region

End Class
