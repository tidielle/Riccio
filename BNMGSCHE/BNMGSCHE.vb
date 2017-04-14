Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSCHE
  Public oCleSche As CLEMGSCHE
  Public oCallParams As CLE__CLDP

  Public bOnLoadingFromSettings As Boolean = False

  Public dttCampi As New DataTable          'elenco campi filtrabili di MOVMAG
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource

  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
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
  Public WithEvents fmPrelmovimenti As NTSInformatica.NTSGroupBox
  Public WithEvents ckPermatricole As NTSInformatica.NTSCheckBox
  Public WithEvents ckRigheInevase As NTSInformatica.NTSCheckBox
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents fmTc As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_Codstag As NTSInformatica.NTSLabel
  Public WithEvents lbCodstag As NTSInformatica.NTSLabel
  Public WithEvents edAnnotco As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnotco As NTSInformatica.NTSLabel
  Public WithEvents ckSelAnnoStag As NTSInformatica.NTSCheckBox
  Public WithEvents edCodstag As NTSInformatica.NTSTextBoxNum
  Public WithEvents tsSche As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnFiltri2 As NTSInformatica.NTSPanel
  Public WithEvents cmdLock As NTSInformatica.NTSButton
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents ckSalto As NTSInformatica.NTSCheckBox
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSCHE", "BEMGSCHE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSche = CType(oTmp, CLEMGSCHE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSCHE", strRemoteServer, strRemotePort)
    AddHandler oCleSche.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSche.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSCHE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbNoModal = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.fmPrelmovimenti = New NTSInformatica.NTSGroupBox
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.ckPermatricole = New NTSInformatica.NTSCheckBox
    Me.ckRigheInevase = New NTSInformatica.NTSCheckBox
    Me.fmTc = New NTSInformatica.NTSGroupBox
    Me.edCodstag = New NTSInformatica.NTSTextBoxNum
    Me.ckSelAnnoStag = New NTSInformatica.NTSCheckBox
    Me.lbXx_Codstag = New NTSInformatica.NTSLabel
    Me.lbCodstag = New NTSInformatica.NTSLabel
    Me.edAnnotco = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnotco = New NTSInformatica.NTSLabel
    Me.tsSche = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.pnPanel1Left = New NTSInformatica.NTSPanel
    Me.lbDescodlsel = New NTSInformatica.NTSLabel
    Me.edCodlsel = New NTSInformatica.NTSTextBoxNum
    Me.cbConto = New NTSInformatica.NTSComboBox
    Me.lbDescodlsar = New NTSInformatica.NTSLabel
    Me.edCodlsar = New NTSInformatica.NTSTextBoxNum
    Me.cbCodart = New NTSInformatica.NTSComboBox
    Me.fmSoloMovimentati = New NTSInformatica.NTSGroupBox
    Me.ckPerMagazzino = New NTSInformatica.NTSCheckBox
    Me.ckNonMovimentati = New NTSInformatica.NTSCheckBox
    Me.edNonMovimentati = New NTSInformatica.NTSTextBoxData
    Me.cmdClassificaDeleteFilter = New NTSInformatica.NTSButton
    Me.cmdClassifica = New NTSInformatica.NTSButton
    Me.lbClassifica = New NTSInformatica.NTSLabel
    Me.edClassificazioneLivello5 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello4 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello3 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello2 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello1 = New NTSInformatica.NTSTextBoxStr
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.edLottoDa = New NTSInformatica.NTSTextBoxStr
    Me.edLottoA = New NTSInformatica.NTSTextBoxStr
    Me.lbInizio = New NTSInformatica.NTSLabel
    Me.lbFine = New NTSInformatica.NTSLabel
    Me.lbFaseini = New NTSInformatica.NTSLabel
    Me.edFaseini = New NTSInformatica.NTSTextBoxNum
    Me.lbFasefin = New NTSInformatica.NTSLabel
    Me.edFasefin = New NTSInformatica.NTSTextBoxNum
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.edCodmarcfin = New NTSInformatica.NTSTextBoxNum
    Me.lbCodmarcfin = New NTSInformatica.NTSLabel
    Me.edCodmarcini = New NTSInformatica.NTSTextBoxNum
    Me.lbCodmarcini = New NTSInformatica.NTSLabel
    Me.lbAcomme = New NTSInformatica.NTSLabel
    Me.edDacomme = New NTSInformatica.NTSTextBoxNum
    Me.lbDacomme = New NTSInformatica.NTSLabel
    Me.edAcomme = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codcfam = New NTSInformatica.NTSLabel
    Me.edCausale = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_causale = New NTSInformatica.NTSLabel
    Me.lbCausale = New NTSInformatica.NTSLabel
    Me.edUbicazini = New NTSInformatica.NTSTextBoxStr
    Me.lbUbicazini = New NTSInformatica.NTSLabel
    Me.lbUbicazfin = New NTSInformatica.NTSLabel
    Me.edUbicazfin = New NTSInformatica.NTSTextBoxStr
    Me.edDamatr = New NTSInformatica.NTSTextBoxStr
    Me.lbDamatr = New NTSInformatica.NTSLabel
    Me.lbAmatr = New NTSInformatica.NTSLabel
    Me.edAmatr = New NTSInformatica.NTSTextBoxStr
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatfin = New NTSInformatica.NTSLabel
    Me.ckSaldiIniziali = New NTSInformatica.NTSCheckBox
    Me.ckStorico = New NTSInformatica.NTSCheckBox
    Me.lbDamagaz = New NTSInformatica.NTSLabel
    Me.edDamagaz = New NTSInformatica.NTSTextBoxNum
    Me.edAmagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbAmagaz = New NTSInformatica.NTSLabel
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.lbSep = New NTSInformatica.NTSLabel
    Me.edSottogr = New NTSInformatica.NTSTextBoxNum
    Me.edGruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbGruppo = New NTSInformatica.NTSLabel
    Me.edDacodart = New NTSInformatica.NTSTextBoxStr
    Me.lbAcodart = New NTSInformatica.NTSLabel
    Me.edAcodart = New NTSInformatica.NTSTextBoxStr
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.cbIncludi = New NTSInformatica.NTSComboBox
    Me.lbIncludi = New NTSInformatica.NTSLabel
    Me.ckStampaFiltri = New NTSInformatica.NTSCheckBox
    Me.ckSalto = New NTSInformatica.NTSCheckBox
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cbTipoStampa = New NTSInformatica.NTSComboBox
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    Me.pnFiltri2 = New NTSInformatica.NTSPanel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbTipoStampa = New NTSInformatica.NTSLabel
    Me.pnTipoStampa = New NTSInformatica.NTSPanel
    Me.opTipoStampa1 = New NTSInformatica.NTSRadioButton
    Me.opTipoStampa0 = New NTSInformatica.NTSRadioButton
    Me.cmdApriFiltri = New NTSInformatica.NTSButton
    Me.cbFiltro = New NTSInformatica.NTSComboBox
    Me.lbFiltri = New NTSInformatica.NTSLabel
    Me.pnFiltriExt = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPrelmovimenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrelmovimenti.SuspendLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPermatricole.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRigheInevase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTc.SuspendLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsSche, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSche.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.pnPanel1Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel1Left.SuspendLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSoloMovimentati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSoloMovimentati.SuspendLayout()
    CType(Me.ckPerMagazzino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNonMovimentati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNonMovimentati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLottoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLottoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFaseini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFasefin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmarcfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmarcini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacomme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcomme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCausale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUbicazini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUbicazfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAmatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSaldiIniziali.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStorico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.cbIncludi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStampaFiltri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoStampa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri2.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTipoStampa.SuspendLayout()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFiltriExt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltriExt.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbNoModal})
    Me.NtsBarManager1.MaxItemId = 22
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoModal), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbNoModal
    '
    Me.tlbNoModal.Caption = "Apri griglia in modalità NON modale"
    Me.tlbNoModal.Id = 21
    Me.tlbNoModal.Name = "tlbNoModal"
    Me.tlbNoModal.NTSIsCheckBox = True
    Me.tlbNoModal.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
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
    'lbStatus
    '
    Me.lbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(10, 419)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 34
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'fmPrelmovimenti
    '
    Me.fmPrelmovimenti.AllowDrop = True
    Me.fmPrelmovimenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrelmovimenti.Appearance.Options.UseBackColor = True
    Me.fmPrelmovimenti.Controls.Add(Me.ckSerie)
    Me.fmPrelmovimenti.Controls.Add(Me.edSerie)
    Me.fmPrelmovimenti.Controls.Add(Me.cbTipodoc)
    Me.fmPrelmovimenti.Controls.Add(Me.ckPermatricole)
    Me.fmPrelmovimenti.Controls.Add(Me.ckRigheInevase)
    Me.fmPrelmovimenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrelmovimenti.Location = New System.Drawing.Point(9, 342)
    Me.fmPrelmovimenti.Name = "fmPrelmovimenti"
    Me.fmPrelmovimenti.Size = New System.Drawing.Size(468, 73)
    Me.fmPrelmovimenti.TabIndex = 82
    Me.fmPrelmovimenti.Text = "Preleva movimenti:"
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(227, 28)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(47, 19)
    Me.ckSerie.TabIndex = 41
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Enabled = False
    Me.edSerie.Location = New System.Drawing.Point(277, 28)
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
    Me.edSerie.TabIndex = 61
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Location = New System.Drawing.Point(6, 28)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.AutoHeight = False
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(217, 20)
    Me.cbTipodoc.TabIndex = 0
    Me.cbTipodoc.ValueMember = ""
    '
    'ckPermatricole
    '
    Me.ckPermatricole.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPermatricole.Location = New System.Drawing.Point(323, 48)
    Me.ckPermatricole.Name = "ckPermatricole"
    Me.ckPermatricole.NTSCheckValue = "S"
    Me.ckPermatricole.NTSUnCheckValue = "N"
    Me.ckPermatricole.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPermatricole.Properties.Appearance.Options.UseBackColor = True
    Me.ckPermatricole.Properties.AutoHeight = False
    Me.ckPermatricole.Properties.Caption = "Matricole (solo in griglia)"
    Me.ckPermatricole.Size = New System.Drawing.Size(138, 19)
    Me.ckPermatricole.TabIndex = 44
    Me.ckPermatricole.Visible = False
    '
    'ckRigheInevase
    '
    Me.ckRigheInevase.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRigheInevase.Location = New System.Drawing.Point(323, 28)
    Me.ckRigheInevase.Name = "ckRigheInevase"
    Me.ckRigheInevase.NTSCheckValue = "S"
    Me.ckRigheInevase.NTSUnCheckValue = "N"
    Me.ckRigheInevase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRigheInevase.Properties.Appearance.Options.UseBackColor = True
    Me.ckRigheInevase.Properties.AutoHeight = False
    Me.ckRigheInevase.Properties.Caption = "Solo righe non evase"
    Me.ckRigheInevase.Size = New System.Drawing.Size(138, 19)
    Me.ckRigheInevase.TabIndex = 43
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
    Me.fmTc.Location = New System.Drawing.Point(483, 342)
    Me.fmTc.Name = "fmTc"
    Me.fmTc.Size = New System.Drawing.Size(276, 73)
    Me.fmTc.TabIndex = 86
    '
    'edCodstag
    '
    Me.edCodstag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodstag.EditValue = "0"
    Me.edCodstag.Location = New System.Drawing.Point(63, 47)
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
    Me.ckSelAnnoStag.Location = New System.Drawing.Point(3, 1)
    Me.ckSelAnnoStag.Name = "ckSelAnnoStag"
    Me.ckSelAnnoStag.NTSCheckValue = "S"
    Me.ckSelAnnoStag.NTSUnCheckValue = "N"
    Me.ckSelAnnoStag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSelAnnoStag.Properties.Appearance.Options.UseBackColor = True
    Me.ckSelAnnoStag.Properties.AutoHeight = False
    Me.ckSelAnnoStag.Properties.Caption = "Seleziona Anno/Stagione"
    Me.ckSelAnnoStag.Size = New System.Drawing.Size(145, 19)
    Me.ckSelAnnoStag.TabIndex = 83
    '
    'lbXx_Codstag
    '
    Me.lbXx_Codstag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codstag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codstag.Location = New System.Drawing.Point(125, 46)
    Me.lbXx_Codstag.Name = "lbXx_Codstag"
    Me.lbXx_Codstag.NTSDbField = ""
    Me.lbXx_Codstag.Size = New System.Drawing.Size(146, 20)
    Me.lbXx_Codstag.TabIndex = 82
    Me.lbXx_Codstag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codstag.Tooltip = ""
    Me.lbXx_Codstag.UseMnemonic = False
    '
    'lbCodstag
    '
    Me.lbCodstag.AutoSize = True
    Me.lbCodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodstag.Location = New System.Drawing.Point(3, 51)
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
    Me.edAnnotco.Location = New System.Drawing.Point(63, 25)
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
    Me.lbAnnotco.Location = New System.Drawing.Point(3, 28)
    Me.lbAnnotco.Name = "lbAnnotco"
    Me.lbAnnotco.NTSDbField = ""
    Me.lbAnnotco.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnotco.TabIndex = 39
    Me.lbAnnotco.Text = "Anno"
    Me.lbAnnotco.Tooltip = ""
    Me.lbAnnotco.UseMnemonic = False
    '
    'tsSche
    '
    Me.tsSche.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSche.Location = New System.Drawing.Point(0, 62)
    Me.tsSche.Name = "tsSche"
    Me.tsSche.SelectedTabPage = Me.NtsTabPage2
    Me.tsSche.Size = New System.Drawing.Size(774, 465)
    Me.tsSche.TabIndex = 89
    Me.tsSche.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsSche.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnLeft)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(765, 435)
    Me.NtsTabPage1.Text = "&1- Principale"
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.pnPanel1Left)
    Me.pnLeft.Controls.Add(Me.pnRight)
    Me.pnLeft.Controls.Add(Me.lbStatus)
    Me.pnLeft.Controls.Add(Me.fmTc)
    Me.pnLeft.Controls.Add(Me.fmPrelmovimenti)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(765, 435)
    Me.pnLeft.TabIndex = 54
    Me.pnLeft.Text = "NtsPanel1"
    '
    'pnPanel1Left
    '
    Me.pnPanel1Left.AllowDrop = True
    Me.pnPanel1Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel1Left.Appearance.Options.UseBackColor = True
    Me.pnPanel1Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel1Left.Controls.Add(Me.lbDescodlsel)
    Me.pnPanel1Left.Controls.Add(Me.edCodlsel)
    Me.pnPanel1Left.Controls.Add(Me.cbConto)
    Me.pnPanel1Left.Controls.Add(Me.lbDescodlsar)
    Me.pnPanel1Left.Controls.Add(Me.edCodlsar)
    Me.pnPanel1Left.Controls.Add(Me.cbCodart)
    Me.pnPanel1Left.Controls.Add(Me.fmSoloMovimentati)
    Me.pnPanel1Left.Controls.Add(Me.cmdClassificaDeleteFilter)
    Me.pnPanel1Left.Controls.Add(Me.cmdClassifica)
    Me.pnPanel1Left.Controls.Add(Me.lbClassifica)
    Me.pnPanel1Left.Controls.Add(Me.edClassificazioneLivello5)
    Me.pnPanel1Left.Controls.Add(Me.edClassificazioneLivello4)
    Me.pnPanel1Left.Controls.Add(Me.edClassificazioneLivello3)
    Me.pnPanel1Left.Controls.Add(Me.edClassificazioneLivello2)
    Me.pnPanel1Left.Controls.Add(Me.edClassificazioneLivello1)
    Me.pnPanel1Left.Controls.Add(Me.edDaconto)
    Me.pnPanel1Left.Controls.Add(Me.edLottoDa)
    Me.pnPanel1Left.Controls.Add(Me.edLottoA)
    Me.pnPanel1Left.Controls.Add(Me.lbInizio)
    Me.pnPanel1Left.Controls.Add(Me.lbFine)
    Me.pnPanel1Left.Controls.Add(Me.lbFaseini)
    Me.pnPanel1Left.Controls.Add(Me.edFaseini)
    Me.pnPanel1Left.Controls.Add(Me.lbFasefin)
    Me.pnPanel1Left.Controls.Add(Me.edFasefin)
    Me.pnPanel1Left.Controls.Add(Me.lbAconto)
    Me.pnPanel1Left.Controls.Add(Me.edAconto)
    Me.pnPanel1Left.Controls.Add(Me.edCodmarcfin)
    Me.pnPanel1Left.Controls.Add(Me.lbCodmarcfin)
    Me.pnPanel1Left.Controls.Add(Me.edCodmarcini)
    Me.pnPanel1Left.Controls.Add(Me.lbCodmarcini)
    Me.pnPanel1Left.Controls.Add(Me.lbAcomme)
    Me.pnPanel1Left.Controls.Add(Me.edDacomme)
    Me.pnPanel1Left.Controls.Add(Me.lbDacomme)
    Me.pnPanel1Left.Controls.Add(Me.edAcomme)
    Me.pnPanel1Left.Controls.Add(Me.lbXx_codcfam)
    Me.pnPanel1Left.Controls.Add(Me.edCausale)
    Me.pnPanel1Left.Controls.Add(Me.lbXx_causale)
    Me.pnPanel1Left.Controls.Add(Me.lbCausale)
    Me.pnPanel1Left.Controls.Add(Me.edUbicazini)
    Me.pnPanel1Left.Controls.Add(Me.lbUbicazini)
    Me.pnPanel1Left.Controls.Add(Me.lbUbicazfin)
    Me.pnPanel1Left.Controls.Add(Me.edUbicazfin)
    Me.pnPanel1Left.Controls.Add(Me.edDamatr)
    Me.pnPanel1Left.Controls.Add(Me.lbDamatr)
    Me.pnPanel1Left.Controls.Add(Me.lbAmatr)
    Me.pnPanel1Left.Controls.Add(Me.edAmatr)
    Me.pnPanel1Left.Controls.Add(Me.edDatini)
    Me.pnPanel1Left.Controls.Add(Me.lbDatini)
    Me.pnPanel1Left.Controls.Add(Me.edDatfin)
    Me.pnPanel1Left.Controls.Add(Me.lbDatfin)
    Me.pnPanel1Left.Controls.Add(Me.ckSaldiIniziali)
    Me.pnPanel1Left.Controls.Add(Me.ckStorico)
    Me.pnPanel1Left.Controls.Add(Me.lbDamagaz)
    Me.pnPanel1Left.Controls.Add(Me.edDamagaz)
    Me.pnPanel1Left.Controls.Add(Me.edAmagaz)
    Me.pnPanel1Left.Controls.Add(Me.lbAmagaz)
    Me.pnPanel1Left.Controls.Add(Me.lbCodcfam)
    Me.pnPanel1Left.Controls.Add(Me.edCodcfam)
    Me.pnPanel1Left.Controls.Add(Me.lbSep)
    Me.pnPanel1Left.Controls.Add(Me.edSottogr)
    Me.pnPanel1Left.Controls.Add(Me.edGruppo)
    Me.pnPanel1Left.Controls.Add(Me.lbGruppo)
    Me.pnPanel1Left.Controls.Add(Me.edDacodart)
    Me.pnPanel1Left.Controls.Add(Me.lbAcodart)
    Me.pnPanel1Left.Controls.Add(Me.edAcodart)
    Me.pnPanel1Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel1Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPanel1Left.Name = "pnPanel1Left"
    Me.pnPanel1Left.NTSActiveTrasparency = True
    Me.pnPanel1Left.Size = New System.Drawing.Size(545, 339)
    Me.pnPanel1Left.TabIndex = 117
    Me.pnPanel1Left.Text = "NtsPanel1"
    '
    'lbDescodlsel
    '
    Me.lbDescodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsel.Location = New System.Drawing.Point(215, 114)
    Me.lbDescodlsel.Name = "lbDescodlsel"
    Me.lbDescodlsel.NTSDbField = ""
    Me.lbDescodlsel.Size = New System.Drawing.Size(321, 20)
    Me.lbDescodlsel.TabIndex = 694
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
    Me.edCodlsel.Location = New System.Drawing.Point(126, 114)
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
    Me.edCodlsel.Size = New System.Drawing.Size(84, 20)
    Me.edCodlsel.TabIndex = 693
    Me.edCodlsel.Visible = False
    '
    'cbConto
    '
    Me.cbConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbConto.DataSource = Nothing
    Me.cbConto.DisplayMember = ""
    Me.cbConto.Location = New System.Drawing.Point(6, 114)
    Me.cbConto.Name = "cbConto"
    Me.cbConto.NTSDbField = ""
    Me.cbConto.Properties.AutoHeight = False
    Me.cbConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbConto.Properties.DropDownRows = 30
    Me.cbConto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbConto.SelectedValue = ""
    Me.cbConto.Size = New System.Drawing.Size(114, 20)
    Me.cbConto.TabIndex = 692
    Me.cbConto.ValueMember = ""
    '
    'lbDescodlsar
    '
    Me.lbDescodlsar.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsar.Location = New System.Drawing.Point(215, 48)
    Me.lbDescodlsar.Name = "lbDescodlsar"
    Me.lbDescodlsar.NTSDbField = ""
    Me.lbDescodlsar.Size = New System.Drawing.Size(321, 20)
    Me.lbDescodlsar.TabIndex = 691
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
    Me.edCodlsar.Location = New System.Drawing.Point(126, 48)
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
    Me.edCodlsar.Size = New System.Drawing.Size(84, 20)
    Me.edCodlsar.TabIndex = 690
    Me.edCodlsar.Visible = False
    '
    'cbCodart
    '
    Me.cbCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCodart.DataSource = Nothing
    Me.cbCodart.DisplayMember = ""
    Me.cbCodart.Location = New System.Drawing.Point(6, 48)
    Me.cbCodart.Name = "cbCodart"
    Me.cbCodart.NTSDbField = ""
    Me.cbCodart.Properties.AutoHeight = False
    Me.cbCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCodart.Properties.DropDownRows = 30
    Me.cbCodart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCodart.SelectedValue = ""
    Me.cbCodart.Size = New System.Drawing.Size(114, 20)
    Me.cbCodart.TabIndex = 130
    Me.cbCodart.ValueMember = ""
    '
    'fmSoloMovimentati
    '
    Me.fmSoloMovimentati.AllowDrop = True
    Me.fmSoloMovimentati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSoloMovimentati.Appearance.Options.UseBackColor = True
    Me.fmSoloMovimentati.Controls.Add(Me.ckPerMagazzino)
    Me.fmSoloMovimentati.Controls.Add(Me.ckNonMovimentati)
    Me.fmSoloMovimentati.Controls.Add(Me.edNonMovimentati)
    Me.fmSoloMovimentati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSoloMovimentati.Location = New System.Drawing.Point(340, 224)
    Me.fmSoloMovimentati.Name = "fmSoloMovimentati"
    Me.fmSoloMovimentati.Size = New System.Drawing.Size(196, 64)
    Me.fmSoloMovimentati.TabIndex = 130
    '
    'ckPerMagazzino
    '
    Me.ckPerMagazzino.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPerMagazzino.Enabled = False
    Me.ckPerMagazzino.Location = New System.Drawing.Point(16, 43)
    Me.ckPerMagazzino.Name = "ckPerMagazzino"
    Me.ckPerMagazzino.NTSCheckValue = "S"
    Me.ckPerMagazzino.NTSUnCheckValue = "N"
    Me.ckPerMagazzino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPerMagazzino.Properties.Appearance.Options.UseBackColor = True
    Me.ckPerMagazzino.Properties.AutoHeight = False
    Me.ckPerMagazzino.Properties.Caption = "Distingui per magazzino"
    Me.ckPerMagazzino.Size = New System.Drawing.Size(143, 19)
    Me.ckPerMagazzino.TabIndex = 692
    '
    'ckNonMovimentati
    '
    Me.ckNonMovimentati.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNonMovimentati.Location = New System.Drawing.Point(6, 0)
    Me.ckNonMovimentati.Name = "ckNonMovimentati"
    Me.ckNonMovimentati.NTSCheckValue = "S"
    Me.ckNonMovimentati.NTSUnCheckValue = "N"
    Me.ckNonMovimentati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNonMovimentati.Properties.Appearance.Options.UseBackColor = True
    Me.ckNonMovimentati.Properties.AutoHeight = False
    Me.ckNonMovimentati.Properties.Caption = "Solo non movimentati dal"
    Me.ckNonMovimentati.Size = New System.Drawing.Size(154, 19)
    Me.ckNonMovimentati.TabIndex = 690
    '
    'edNonMovimentati
    '
    Me.edNonMovimentati.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNonMovimentati.EditValue = ""
    Me.edNonMovimentati.Enabled = False
    Me.edNonMovimentati.Location = New System.Drawing.Point(18, 22)
    Me.edNonMovimentati.Name = "edNonMovimentati"
    Me.edNonMovimentati.NTSDbField = ""
    Me.edNonMovimentati.NTSForzaVisZoom = False
    Me.edNonMovimentati.NTSOldValue = ""
    Me.edNonMovimentati.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNonMovimentati.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNonMovimentati.Properties.AutoHeight = False
    Me.edNonMovimentati.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNonMovimentati.Properties.MaxLength = 65536
    Me.edNonMovimentati.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNonMovimentati.Size = New System.Drawing.Size(80, 20)
    Me.edNonMovimentati.TabIndex = 691
    '
    'cmdClassificaDeleteFilter
    '
    Me.cmdClassificaDeleteFilter.Image = CType(resources.GetObject("cmdClassificaDeleteFilter.Image"), System.Drawing.Image)
    Me.cmdClassificaDeleteFilter.ImageText = ""
    Me.cmdClassificaDeleteFilter.Location = New System.Drawing.Point(40, 269)
    Me.cmdClassificaDeleteFilter.Name = "cmdClassificaDeleteFilter"
    Me.cmdClassificaDeleteFilter.NTSContextMenu = Nothing
    Me.cmdClassificaDeleteFilter.Size = New System.Drawing.Size(28, 22)
    Me.cmdClassificaDeleteFilter.TabIndex = 688
    Me.cmdClassificaDeleteFilter.ToolTip = "Rimuovi il fitro relativo alla classificazione"
    '
    'cmdClassifica
    '
    Me.cmdClassifica.ImageText = ""
    Me.cmdClassifica.Location = New System.Drawing.Point(10, 247)
    Me.cmdClassifica.Name = "cmdClassifica"
    Me.cmdClassifica.NTSContextMenu = Nothing
    Me.cmdClassifica.Size = New System.Drawing.Size(58, 22)
    Me.cmdClassifica.TabIndex = 689
    Me.cmdClassifica.Text = "Classifica"
    '
    'lbClassifica
    '
    Me.lbClassifica.BackColor = System.Drawing.Color.Transparent
    Me.lbClassifica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbClassifica.Location = New System.Drawing.Point(73, 247)
    Me.lbClassifica.Name = "lbClassifica"
    Me.lbClassifica.NTSDbField = ""
    Me.lbClassifica.Size = New System.Drawing.Size(246, 44)
    Me.lbClassifica.TabIndex = 687
    Me.lbClassifica.Tooltip = ""
    Me.lbClassifica.UseMnemonic = False
    '
    'edClassificazioneLivello5
    '
    Me.edClassificazioneLivello5.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello5.Location = New System.Drawing.Point(33, 269)
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
    Me.edClassificazioneLivello5.TabIndex = 681
    Me.edClassificazioneLivello5.Visible = False
    '
    'edClassificazioneLivello4
    '
    Me.edClassificazioneLivello4.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello4.Location = New System.Drawing.Point(27, 269)
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
    Me.edClassificazioneLivello4.TabIndex = 680
    Me.edClassificazioneLivello4.Visible = False
    '
    'edClassificazioneLivello3
    '
    Me.edClassificazioneLivello3.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello3.Location = New System.Drawing.Point(20, 269)
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
    Me.edClassificazioneLivello3.TabIndex = 682
    Me.edClassificazioneLivello3.Visible = False
    '
    'edClassificazioneLivello2
    '
    Me.edClassificazioneLivello2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello2.Location = New System.Drawing.Point(14, 269)
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
    Me.edClassificazioneLivello2.TabIndex = 684
    Me.edClassificazioneLivello2.Visible = False
    '
    'edClassificazioneLivello1
    '
    Me.edClassificazioneLivello1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello1.Location = New System.Drawing.Point(8, 269)
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
    Me.edClassificazioneLivello1.TabIndex = 683
    Me.edClassificazioneLivello1.Visible = False
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(126, 114)
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
    Me.edDaconto.TabIndex = 152
    '
    'edLottoDa
    '
    Me.edLottoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLottoDa.EditValue = ""
    Me.edLottoDa.Location = New System.Drawing.Point(126, 136)
    Me.edLottoDa.Name = "edLottoDa"
    Me.edLottoDa.NTSDbField = ""
    Me.edLottoDa.NTSForzaVisZoom = False
    Me.edLottoDa.NTSOldValue = ""
    Me.edLottoDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edLottoDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLottoDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLottoDa.Properties.AutoHeight = False
    Me.edLottoDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLottoDa.Properties.MaxLength = 65536
    Me.edLottoDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLottoDa.Size = New System.Drawing.Size(193, 20)
    Me.edLottoDa.TabIndex = 156
    '
    'edLottoA
    '
    Me.edLottoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLottoA.EditValue = ""
    Me.edLottoA.Location = New System.Drawing.Point(342, 136)
    Me.edLottoA.Name = "edLottoA"
    Me.edLottoA.NTSDbField = ""
    Me.edLottoA.NTSForzaVisZoom = False
    Me.edLottoA.NTSOldValue = ""
    Me.edLottoA.Properties.Appearance.Options.UseTextOptions = True
    Me.edLottoA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLottoA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLottoA.Properties.AutoHeight = False
    Me.edLottoA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLottoA.Properties.MaxLength = 65536
    Me.edLottoA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLottoA.Size = New System.Drawing.Size(194, 20)
    Me.edLottoA.TabIndex = 157
    '
    'lbInizio
    '
    Me.lbInizio.AutoSize = True
    Me.lbInizio.BackColor = System.Drawing.Color.Transparent
    Me.lbInizio.Location = New System.Drawing.Point(7, 140)
    Me.lbInizio.Name = "lbInizio"
    Me.lbInizio.NTSDbField = ""
    Me.lbInizio.Size = New System.Drawing.Size(55, 13)
    Me.lbInizio.TabIndex = 155
    Me.lbInizio.Text = "Da/a lotto"
    Me.lbInizio.Tooltip = ""
    Me.lbInizio.UseMnemonic = False
    '
    'lbFine
    '
    Me.lbFine.AutoSize = True
    Me.lbFine.BackColor = System.Drawing.Color.Transparent
    Me.lbFine.Location = New System.Drawing.Point(325, 139)
    Me.lbFine.Name = "lbFine"
    Me.lbFine.NTSDbField = ""
    Me.lbFine.Size = New System.Drawing.Size(11, 13)
    Me.lbFine.TabIndex = 158
    Me.lbFine.Text = "/"
    Me.lbFine.Tooltip = ""
    Me.lbFine.UseMnemonic = False
    '
    'lbFaseini
    '
    Me.lbFaseini.AutoSize = True
    Me.lbFaseini.BackColor = System.Drawing.Color.Transparent
    Me.lbFaseini.Location = New System.Drawing.Point(7, 74)
    Me.lbFaseini.Name = "lbFaseini"
    Me.lbFaseini.NTSDbField = ""
    Me.lbFaseini.Size = New System.Drawing.Size(54, 13)
    Me.lbFaseini.TabIndex = 159
    Me.lbFaseini.Text = "Da/a fase"
    Me.lbFaseini.Tooltip = ""
    Me.lbFaseini.UseMnemonic = False
    '
    'edFaseini
    '
    Me.edFaseini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFaseini.EditValue = "0"
    Me.edFaseini.Location = New System.Drawing.Point(126, 70)
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
    Me.edFaseini.TabIndex = 160
    '
    'lbFasefin
    '
    Me.lbFasefin.AutoSize = True
    Me.lbFasefin.BackColor = System.Drawing.Color.Transparent
    Me.lbFasefin.Location = New System.Drawing.Point(325, 74)
    Me.lbFasefin.Name = "lbFasefin"
    Me.lbFasefin.NTSDbField = ""
    Me.lbFasefin.Size = New System.Drawing.Size(11, 13)
    Me.lbFasefin.TabIndex = 161
    Me.lbFasefin.Text = "/"
    Me.lbFasefin.Tooltip = ""
    Me.lbFasefin.UseMnemonic = False
    '
    'edFasefin
    '
    Me.edFasefin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFasefin.EditValue = "0"
    Me.edFasefin.Location = New System.Drawing.Point(342, 70)
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
    Me.edFasefin.TabIndex = 162
    '
    'lbAconto
    '
    Me.lbAconto.AutoSize = True
    Me.lbAconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAconto.Location = New System.Drawing.Point(325, 117)
    Me.lbAconto.Name = "lbAconto"
    Me.lbAconto.NTSDbField = ""
    Me.lbAconto.Size = New System.Drawing.Size(11, 13)
    Me.lbAconto.TabIndex = 154
    Me.lbAconto.Text = "/"
    Me.lbAconto.Tooltip = ""
    Me.lbAconto.UseMnemonic = False
    '
    'edAconto
    '
    Me.edAconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAconto.EditValue = "0"
    Me.edAconto.Location = New System.Drawing.Point(342, 114)
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
    Me.edAconto.TabIndex = 153
    '
    'edCodmarcfin
    '
    Me.edCodmarcfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmarcfin.EditValue = "0"
    Me.edCodmarcfin.Location = New System.Drawing.Point(342, 92)
    Me.edCodmarcfin.Name = "edCodmarcfin"
    Me.edCodmarcfin.NTSDbField = ""
    Me.edCodmarcfin.NTSFormat = "0"
    Me.edCodmarcfin.NTSForzaVisZoom = False
    Me.edCodmarcfin.NTSOldValue = ""
    Me.edCodmarcfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmarcfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmarcfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmarcfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmarcfin.Properties.AutoHeight = False
    Me.edCodmarcfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmarcfin.Properties.MaxLength = 65536
    Me.edCodmarcfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmarcfin.Size = New System.Drawing.Size(84, 20)
    Me.edCodmarcfin.TabIndex = 150
    '
    'lbCodmarcfin
    '
    Me.lbCodmarcfin.AutoSize = True
    Me.lbCodmarcfin.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmarcfin.Location = New System.Drawing.Point(325, 95)
    Me.lbCodmarcfin.Name = "lbCodmarcfin"
    Me.lbCodmarcfin.NTSDbField = ""
    Me.lbCodmarcfin.Size = New System.Drawing.Size(11, 13)
    Me.lbCodmarcfin.TabIndex = 149
    Me.lbCodmarcfin.Text = "/"
    Me.lbCodmarcfin.Tooltip = ""
    Me.lbCodmarcfin.UseMnemonic = False
    '
    'edCodmarcini
    '
    Me.edCodmarcini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmarcini.EditValue = "0"
    Me.edCodmarcini.Location = New System.Drawing.Point(126, 92)
    Me.edCodmarcini.Name = "edCodmarcini"
    Me.edCodmarcini.NTSDbField = ""
    Me.edCodmarcini.NTSFormat = "0"
    Me.edCodmarcini.NTSForzaVisZoom = False
    Me.edCodmarcini.NTSOldValue = ""
    Me.edCodmarcini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmarcini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmarcini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmarcini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmarcini.Properties.AutoHeight = False
    Me.edCodmarcini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmarcini.Properties.MaxLength = 65536
    Me.edCodmarcini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmarcini.Size = New System.Drawing.Size(83, 20)
    Me.edCodmarcini.TabIndex = 148
    '
    'lbCodmarcini
    '
    Me.lbCodmarcini.AutoSize = True
    Me.lbCodmarcini.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmarcini.Location = New System.Drawing.Point(7, 95)
    Me.lbCodmarcini.Name = "lbCodmarcini"
    Me.lbCodmarcini.NTSDbField = ""
    Me.lbCodmarcini.Size = New System.Drawing.Size(62, 13)
    Me.lbCodmarcini.TabIndex = 147
    Me.lbCodmarcini.Text = "Da/a marca"
    Me.lbCodmarcini.Tooltip = ""
    Me.lbCodmarcini.UseMnemonic = False
    '
    'lbAcomme
    '
    Me.lbAcomme.AutoSize = True
    Me.lbAcomme.BackColor = System.Drawing.Color.Transparent
    Me.lbAcomme.Location = New System.Drawing.Point(325, 162)
    Me.lbAcomme.Name = "lbAcomme"
    Me.lbAcomme.NTSDbField = ""
    Me.lbAcomme.Size = New System.Drawing.Size(11, 13)
    Me.lbAcomme.TabIndex = 166
    Me.lbAcomme.Text = "/"
    Me.lbAcomme.Tooltip = ""
    Me.lbAcomme.UseMnemonic = False
    '
    'edDacomme
    '
    Me.edDacomme.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacomme.EditValue = "0"
    Me.edDacomme.Location = New System.Drawing.Point(126, 158)
    Me.edDacomme.Name = "edDacomme"
    Me.edDacomme.NTSDbField = ""
    Me.edDacomme.NTSFormat = "0"
    Me.edDacomme.NTSForzaVisZoom = False
    Me.edDacomme.NTSOldValue = ""
    Me.edDacomme.Properties.Appearance.Options.UseTextOptions = True
    Me.edDacomme.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDacomme.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDacomme.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDacomme.Properties.AutoHeight = False
    Me.edDacomme.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDacomme.Properties.MaxLength = 65536
    Me.edDacomme.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDacomme.Size = New System.Drawing.Size(83, 20)
    Me.edDacomme.TabIndex = 164
    '
    'lbDacomme
    '
    Me.lbDacomme.AutoSize = True
    Me.lbDacomme.BackColor = System.Drawing.Color.Transparent
    Me.lbDacomme.Location = New System.Drawing.Point(7, 162)
    Me.lbDacomme.Name = "lbDacomme"
    Me.lbDacomme.NTSDbField = ""
    Me.lbDacomme.Size = New System.Drawing.Size(82, 13)
    Me.lbDacomme.TabIndex = 163
    Me.lbDacomme.Text = "Da/a commessa"
    Me.lbDacomme.Tooltip = ""
    Me.lbDacomme.UseMnemonic = False
    '
    'edAcomme
    '
    Me.edAcomme.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcomme.EditValue = "0"
    Me.edAcomme.Location = New System.Drawing.Point(342, 158)
    Me.edAcomme.Name = "edAcomme"
    Me.edAcomme.NTSDbField = ""
    Me.edAcomme.NTSFormat = "0"
    Me.edAcomme.NTSForzaVisZoom = False
    Me.edAcomme.NTSOldValue = ""
    Me.edAcomme.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcomme.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcomme.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAcomme.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAcomme.Properties.AutoHeight = False
    Me.edAcomme.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcomme.Properties.MaxLength = 65536
    Me.edAcomme.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcomme.Size = New System.Drawing.Size(84, 20)
    Me.edAcomme.TabIndex = 165
    '
    'lbXx_codcfam
    '
    Me.lbXx_codcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcfam.Location = New System.Drawing.Point(215, 316)
    Me.lbXx_codcfam.Name = "lbXx_codcfam"
    Me.lbXx_codcfam.NTSDbField = ""
    Me.lbXx_codcfam.Size = New System.Drawing.Size(321, 20)
    Me.lbXx_codcfam.TabIndex = 146
    Me.lbXx_codcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codcfam.Tooltip = ""
    Me.lbXx_codcfam.UseMnemonic = False
    '
    'edCausale
    '
    Me.edCausale.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCausale.EditValue = "0"
    Me.edCausale.Location = New System.Drawing.Point(126, 294)
    Me.edCausale.Name = "edCausale"
    Me.edCausale.NTSDbField = ""
    Me.edCausale.NTSFormat = "0"
    Me.edCausale.NTSForzaVisZoom = False
    Me.edCausale.NTSOldValue = ""
    Me.edCausale.Properties.Appearance.Options.UseTextOptions = True
    Me.edCausale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCausale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCausale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCausale.Properties.AutoHeight = False
    Me.edCausale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCausale.Properties.MaxLength = 65536
    Me.edCausale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCausale.Size = New System.Drawing.Size(83, 20)
    Me.edCausale.TabIndex = 145
    '
    'lbXx_causale
    '
    Me.lbXx_causale.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_causale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_causale.Location = New System.Drawing.Point(215, 293)
    Me.lbXx_causale.Name = "lbXx_causale"
    Me.lbXx_causale.NTSDbField = ""
    Me.lbXx_causale.Size = New System.Drawing.Size(321, 20)
    Me.lbXx_causale.TabIndex = 144
    Me.lbXx_causale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_causale.Tooltip = ""
    Me.lbXx_causale.UseMnemonic = False
    '
    'lbCausale
    '
    Me.lbCausale.AutoSize = True
    Me.lbCausale.BackColor = System.Drawing.Color.Transparent
    Me.lbCausale.Location = New System.Drawing.Point(7, 297)
    Me.lbCausale.Name = "lbCausale"
    Me.lbCausale.NTSDbField = ""
    Me.lbCausale.Size = New System.Drawing.Size(45, 13)
    Me.lbCausale.TabIndex = 143
    Me.lbCausale.Text = "Causale"
    Me.lbCausale.Tooltip = ""
    Me.lbCausale.UseMnemonic = False
    '
    'edUbicazini
    '
    Me.edUbicazini.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edUbicazini.Enabled = False
    Me.edUbicazini.Location = New System.Drawing.Point(126, 202)
    Me.edUbicazini.Name = "edUbicazini"
    Me.edUbicazini.NTSDbField = ""
    Me.edUbicazini.NTSForzaVisZoom = False
    Me.edUbicazini.NTSOldValue = ""
    Me.edUbicazini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUbicazini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUbicazini.Properties.AutoHeight = False
    Me.edUbicazini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUbicazini.Properties.MaxLength = 65536
    Me.edUbicazini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUbicazini.Size = New System.Drawing.Size(193, 20)
    Me.edUbicazini.TabIndex = 139
    '
    'lbUbicazini
    '
    Me.lbUbicazini.AutoSize = True
    Me.lbUbicazini.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicazini.Location = New System.Drawing.Point(7, 206)
    Me.lbUbicazini.Name = "lbUbicazini"
    Me.lbUbicazini.NTSDbField = ""
    Me.lbUbicazini.Size = New System.Drawing.Size(83, 13)
    Me.lbUbicazini.TabIndex = 140
    Me.lbUbicazini.Text = "Da/a ubicazione"
    Me.lbUbicazini.Tooltip = ""
    Me.lbUbicazini.UseMnemonic = False
    '
    'lbUbicazfin
    '
    Me.lbUbicazfin.AutoSize = True
    Me.lbUbicazfin.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicazfin.Location = New System.Drawing.Point(325, 205)
    Me.lbUbicazfin.Name = "lbUbicazfin"
    Me.lbUbicazfin.NTSDbField = ""
    Me.lbUbicazfin.Size = New System.Drawing.Size(11, 13)
    Me.lbUbicazfin.TabIndex = 141
    Me.lbUbicazfin.Text = "/"
    Me.lbUbicazfin.Tooltip = ""
    Me.lbUbicazfin.UseMnemonic = False
    '
    'edUbicazfin
    '
    Me.edUbicazfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUbicazfin.Enabled = False
    Me.edUbicazfin.Location = New System.Drawing.Point(342, 202)
    Me.edUbicazfin.Name = "edUbicazfin"
    Me.edUbicazfin.NTSDbField = ""
    Me.edUbicazfin.NTSForzaVisZoom = False
    Me.edUbicazfin.NTSOldValue = ""
    Me.edUbicazfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUbicazfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUbicazfin.Properties.AutoHeight = False
    Me.edUbicazfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUbicazfin.Properties.MaxLength = 65536
    Me.edUbicazfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUbicazfin.Size = New System.Drawing.Size(194, 20)
    Me.edUbicazfin.TabIndex = 142
    '
    'edDamatr
    '
    Me.edDamatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDamatr.Enabled = False
    Me.edDamatr.Location = New System.Drawing.Point(126, 180)
    Me.edDamatr.Name = "edDamatr"
    Me.edDamatr.NTSDbField = ""
    Me.edDamatr.NTSForzaVisZoom = False
    Me.edDamatr.NTSOldValue = ""
    Me.edDamatr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamatr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamatr.Properties.AutoHeight = False
    Me.edDamatr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamatr.Properties.MaxLength = 65536
    Me.edDamatr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamatr.Size = New System.Drawing.Size(193, 20)
    Me.edDamatr.TabIndex = 135
    '
    'lbDamatr
    '
    Me.lbDamatr.AutoSize = True
    Me.lbDamatr.BackColor = System.Drawing.Color.Transparent
    Me.lbDamatr.Location = New System.Drawing.Point(7, 184)
    Me.lbDamatr.Name = "lbDamatr"
    Me.lbDamatr.NTSDbField = ""
    Me.lbDamatr.Size = New System.Drawing.Size(76, 13)
    Me.lbDamatr.TabIndex = 136
    Me.lbDamatr.Text = "Da/a matricola"
    Me.lbDamatr.Tooltip = ""
    Me.lbDamatr.UseMnemonic = False
    '
    'lbAmatr
    '
    Me.lbAmatr.AutoSize = True
    Me.lbAmatr.BackColor = System.Drawing.Color.Transparent
    Me.lbAmatr.Location = New System.Drawing.Point(325, 183)
    Me.lbAmatr.Name = "lbAmatr"
    Me.lbAmatr.NTSDbField = ""
    Me.lbAmatr.Size = New System.Drawing.Size(11, 13)
    Me.lbAmatr.TabIndex = 137
    Me.lbAmatr.Text = "/"
    Me.lbAmatr.Tooltip = ""
    Me.lbAmatr.UseMnemonic = False
    '
    'edAmatr
    '
    Me.edAmatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAmatr.Enabled = False
    Me.edAmatr.Location = New System.Drawing.Point(342, 180)
    Me.edAmatr.Name = "edAmatr"
    Me.edAmatr.NTSDbField = ""
    Me.edAmatr.NTSForzaVisZoom = False
    Me.edAmatr.NTSOldValue = ""
    Me.edAmatr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAmatr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAmatr.Properties.AutoHeight = False
    Me.edAmatr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAmatr.Properties.MaxLength = 65536
    Me.edAmatr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAmatr.Size = New System.Drawing.Size(194, 20)
    Me.edAmatr.TabIndex = 138
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.EditValue = ""
    Me.edDatini.Location = New System.Drawing.Point(239, 4)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.AutoHeight = False
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(80, 20)
    Me.edDatini.TabIndex = 131
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(178, 7)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(55, 13)
    Me.lbDatini.TabIndex = 132
    Me.lbDatini.Text = "Da/a data"
    Me.lbDatini.Tooltip = ""
    Me.lbDatini.UseMnemonic = False
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.Location = New System.Drawing.Point(342, 4)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatfin.Properties.AutoHeight = False
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(84, 20)
    Me.edDatfin.TabIndex = 133
    '
    'lbDatfin
    '
    Me.lbDatfin.AutoSize = True
    Me.lbDatfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfin.Location = New System.Drawing.Point(325, 7)
    Me.lbDatfin.Name = "lbDatfin"
    Me.lbDatfin.NTSDbField = ""
    Me.lbDatfin.Size = New System.Drawing.Size(11, 13)
    Me.lbDatfin.TabIndex = 134
    Me.lbDatfin.Text = "/"
    Me.lbDatfin.Tooltip = ""
    Me.lbDatfin.UseMnemonic = False
    '
    'ckSaldiIniziali
    '
    Me.ckSaldiIniziali.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSaldiIniziali.EditValue = True
    Me.ckSaldiIniziali.Enabled = False
    Me.ckSaldiIniziali.Location = New System.Drawing.Point(428, 4)
    Me.ckSaldiIniziali.Name = "ckSaldiIniziali"
    Me.ckSaldiIniziali.NTSCheckValue = "S"
    Me.ckSaldiIniziali.NTSUnCheckValue = "N"
    Me.ckSaldiIniziali.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSaldiIniziali.Properties.Appearance.Options.UseBackColor = True
    Me.ckSaldiIniziali.Properties.AutoHeight = False
    Me.ckSaldiIniziali.Properties.Caption = "Calcola saldi iniziali"
    Me.ckSaldiIniziali.Size = New System.Drawing.Size(111, 19)
    Me.ckSaldiIniziali.TabIndex = 130
    '
    'ckStorico
    '
    Me.ckStorico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStorico.Location = New System.Drawing.Point(6, 3)
    Me.ckStorico.Name = "ckStorico"
    Me.ckStorico.NTSCheckValue = "S"
    Me.ckStorico.NTSUnCheckValue = "N"
    Me.ckStorico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStorico.Properties.Appearance.Options.UseBackColor = True
    Me.ckStorico.Properties.AutoHeight = False
    Me.ckStorico.Properties.Caption = "Considera lo storico"
    Me.ckStorico.Size = New System.Drawing.Size(123, 19)
    Me.ckStorico.TabIndex = 129
    '
    'lbDamagaz
    '
    Me.lbDamagaz.AutoSize = True
    Me.lbDamagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDamagaz.Location = New System.Drawing.Point(7, 29)
    Me.lbDamagaz.Name = "lbDamagaz"
    Me.lbDamagaz.NTSDbField = ""
    Me.lbDamagaz.Size = New System.Drawing.Size(83, 13)
    Me.lbDamagaz.TabIndex = 115
    Me.lbDamagaz.Text = "Da/a magazzino"
    Me.lbDamagaz.Tooltip = ""
    Me.lbDamagaz.UseMnemonic = False
    '
    'edDamagaz
    '
    Me.edDamagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDamagaz.EditValue = "0"
    Me.edDamagaz.Location = New System.Drawing.Point(126, 26)
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
    Me.edDamagaz.TabIndex = 116
    '
    'edAmagaz
    '
    Me.edAmagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAmagaz.EditValue = "0"
    Me.edAmagaz.Location = New System.Drawing.Point(342, 26)
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
    Me.edAmagaz.TabIndex = 117
    '
    'lbAmagaz
    '
    Me.lbAmagaz.AutoSize = True
    Me.lbAmagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAmagaz.Location = New System.Drawing.Point(325, 29)
    Me.lbAmagaz.Name = "lbAmagaz"
    Me.lbAmagaz.NTSDbField = ""
    Me.lbAmagaz.Size = New System.Drawing.Size(11, 13)
    Me.lbAmagaz.TabIndex = 118
    Me.lbAmagaz.Text = "/"
    Me.lbAmagaz.Tooltip = ""
    Me.lbAmagaz.UseMnemonic = False
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(7, 319)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(110, 13)
    Me.lbCodcfam.TabIndex = 128
    Me.lbCodcfam.Text = "Linea/famiglia articolo"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(126, 316)
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
    Me.edCodcfam.Size = New System.Drawing.Size(83, 20)
    Me.edCodcfam.TabIndex = 127
    '
    'lbSep
    '
    Me.lbSep.AutoSize = True
    Me.lbSep.BackColor = System.Drawing.Color.Transparent
    Me.lbSep.Location = New System.Drawing.Point(218, 228)
    Me.lbSep.Name = "lbSep"
    Me.lbSep.NTSDbField = ""
    Me.lbSep.Size = New System.Drawing.Size(11, 13)
    Me.lbSep.TabIndex = 126
    Me.lbSep.Text = "/"
    Me.lbSep.Tooltip = ""
    Me.lbSep.UseMnemonic = False
    '
    'edSottogr
    '
    Me.edSottogr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSottogr.EditValue = "0"
    Me.edSottogr.Location = New System.Drawing.Point(235, 224)
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
    Me.edSottogr.TabIndex = 125
    '
    'edGruppo
    '
    Me.edGruppo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edGruppo.EditValue = "0"
    Me.edGruppo.Location = New System.Drawing.Point(126, 224)
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
    Me.edGruppo.TabIndex = 124
    '
    'lbGruppo
    '
    Me.lbGruppo.AutoSize = True
    Me.lbGruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppo.Location = New System.Drawing.Point(7, 228)
    Me.lbGruppo.Name = "lbGruppo"
    Me.lbGruppo.NTSDbField = ""
    Me.lbGruppo.Size = New System.Drawing.Size(106, 13)
    Me.lbGruppo.TabIndex = 123
    Me.lbGruppo.Text = "Gruppo/Sottogruppo"
    Me.lbGruppo.Tooltip = ""
    Me.lbGruppo.UseMnemonic = False
    '
    'edDacodart
    '
    Me.edDacodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacodart.Location = New System.Drawing.Point(126, 48)
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
    Me.edDacodart.Size = New System.Drawing.Size(194, 20)
    Me.edDacodart.TabIndex = 119
    '
    'lbAcodart
    '
    Me.lbAcodart.AutoSize = True
    Me.lbAcodart.BackColor = System.Drawing.Color.Transparent
    Me.lbAcodart.Location = New System.Drawing.Point(325, 51)
    Me.lbAcodart.Name = "lbAcodart"
    Me.lbAcodart.NTSDbField = ""
    Me.lbAcodart.Size = New System.Drawing.Size(11, 13)
    Me.lbAcodart.TabIndex = 121
    Me.lbAcodart.Text = "/"
    Me.lbAcodart.Tooltip = ""
    Me.lbAcodart.UseMnemonic = False
    '
    'edAcodart
    '
    Me.edAcodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcodart.Location = New System.Drawing.Point(342, 48)
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
    Me.edAcodart.Size = New System.Drawing.Size(194, 20)
    Me.edAcodart.TabIndex = 122
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cbIncludi)
    Me.pnRight.Controls.Add(Me.lbIncludi)
    Me.pnRight.Controls.Add(Me.ckStampaFiltri)
    Me.pnRight.Controls.Add(Me.ckSalto)
    Me.pnRight.Controls.Add(Me.lbTipo)
    Me.pnRight.Controls.Add(Me.cbTipoStampa)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Location = New System.Drawing.Point(550, 3)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(209, 339)
    Me.pnRight.TabIndex = 116
    Me.pnRight.Text = "NtsPanel1"
    '
    'cbIncludi
    '
    Me.cbIncludi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbIncludi.DataSource = Nothing
    Me.cbIncludi.DisplayMember = ""
    Me.cbIncludi.Location = New System.Drawing.Point(4, 70)
    Me.cbIncludi.Name = "cbIncludi"
    Me.cbIncludi.NTSDbField = ""
    Me.cbIncludi.Properties.AutoHeight = False
    Me.cbIncludi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbIncludi.Properties.DropDownRows = 30
    Me.cbIncludi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbIncludi.SelectedValue = ""
    Me.cbIncludi.Size = New System.Drawing.Size(206, 20)
    Me.cbIncludi.TabIndex = 120
    Me.cbIncludi.ValueMember = ""
    '
    'lbIncludi
    '
    Me.lbIncludi.AutoSize = True
    Me.lbIncludi.BackColor = System.Drawing.Color.Transparent
    Me.lbIncludi.Location = New System.Drawing.Point(85, 55)
    Me.lbIncludi.Name = "lbIncludi"
    Me.lbIncludi.NTSDbField = ""
    Me.lbIncludi.Size = New System.Drawing.Size(38, 13)
    Me.lbIncludi.TabIndex = 129
    Me.lbIncludi.Text = "Includi"
    Me.lbIncludi.Tooltip = ""
    Me.lbIncludi.UseMnemonic = False
    '
    'ckStampaFiltri
    '
    Me.ckStampaFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStampaFiltri.Location = New System.Drawing.Point(14, 244)
    Me.ckStampaFiltri.Name = "ckStampaFiltri"
    Me.ckStampaFiltri.NTSCheckValue = "S"
    Me.ckStampaFiltri.NTSUnCheckValue = "N"
    Me.ckStampaFiltri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStampaFiltri.Properties.Appearance.Options.UseBackColor = True
    Me.ckStampaFiltri.Properties.AutoHeight = False
    Me.ckStampaFiltri.Properties.Caption = "Stampa i filtri impostati nel report"
    Me.ckStampaFiltri.Size = New System.Drawing.Size(190, 19)
    Me.ckStampaFiltri.TabIndex = 63
    '
    'ckSalto
    '
    Me.ckSalto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalto.Location = New System.Drawing.Point(14, 222)
    Me.ckSalto.Name = "ckSalto"
    Me.ckSalto.NTSCheckValue = "S"
    Me.ckSalto.NTSUnCheckValue = "N"
    Me.ckSalto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalto.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalto.Properties.AutoHeight = False
    Me.ckSalto.Properties.Caption = "Salto pagina per ogni articolo"
    Me.ckSalto.Size = New System.Drawing.Size(160, 19)
    Me.ckSalto.TabIndex = 46
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTipo.Location = New System.Drawing.Point(69, 2)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(84, 13)
    Me.lbTipo.TabIndex = 119
    Me.lbTipo.Text = "TIPO STAMPA"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'cbTipoStampa
    '
    Me.cbTipoStampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoStampa.DataSource = Nothing
    Me.cbTipoStampa.DisplayMember = ""
    Me.cbTipoStampa.Location = New System.Drawing.Point(3, 18)
    Me.cbTipoStampa.Name = "cbTipoStampa"
    Me.cbTipoStampa.NTSDbField = ""
    Me.cbTipoStampa.Properties.AutoHeight = False
    Me.cbTipoStampa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoStampa.Properties.DropDownRows = 30
    Me.cbTipoStampa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoStampa.SelectedValue = ""
    Me.cbTipoStampa.Size = New System.Drawing.Size(206, 20)
    Me.cbTipoStampa.TabIndex = 118
    Me.cbTipoStampa.ValueMember = ""
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage2.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage2.Controls.Add(Me.pnFiltriExt)
    Me.NtsTabPage2.Controls.Add(Me.pnFiltri2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(765, 435)
    Me.NtsTabPage2.Text = "&2 - Filtri Estesi"
    '
    'ceFiltriExt
    '
    Me.ceFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.ceFiltriExt.MinimumSize = New System.Drawing.Size(399, 193)
    Me.ceFiltriExt.Name = "ceFiltriExt"
    Me.ceFiltriExt.oParent = Nothing
    Me.ceFiltriExt.Size = New System.Drawing.Size(765, 435)
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
    Me.pnFiltri2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri2.Location = New System.Drawing.Point(688, 368)
    Me.pnFiltri2.Name = "pnFiltri2"
    Me.pnFiltri2.NTSActiveTrasparency = True
    Me.pnFiltri2.Size = New System.Drawing.Size(77, 67)
    Me.pnFiltri2.TabIndex = 0
    Me.pnFiltri2.Text = "NtsPanel1"
    Me.pnFiltri2.Visible = False
    '
    'cmdLock
    '
    Me.cmdLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(-127, 39)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(198, 24)
    Me.cmdLock.TabIndex = 9
    Me.cmdLock.Text = "Blocca/sblocca filtri"
    '
    'grFiltri1
    '
    Me.grFiltri1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grFiltri1.EmbeddedNavigator.Name = ""
    Me.grFiltri1.Location = New System.Drawing.Point(0, 0)
    Me.grFiltri1.MainView = Me.grvFiltri1
    Me.grFiltri1.Name = "grFiltri1"
    Me.grFiltri1.Size = New System.Drawing.Size(77, 35)
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbTipoStampa)
    Me.pnTop.Controls.Add(Me.pnTipoStampa)
    Me.pnTop.Controls.Add(Me.cmdApriFiltri)
    Me.pnTop.Controls.Add(Me.cbFiltro)
    Me.pnTop.Controls.Add(Me.lbFiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(774, 32)
    Me.pnTop.TabIndex = 90
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbTipoStampa
    '
    Me.lbTipoStampa.AutoSize = True
    Me.lbTipoStampa.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoStampa.Location = New System.Drawing.Point(547, 8)
    Me.lbTipoStampa.Name = "lbTipoStampa"
    Me.lbTipoStampa.NTSDbField = ""
    Me.lbTipoStampa.Size = New System.Drawing.Size(76, 13)
    Me.lbTipoStampa.TabIndex = 95
    Me.lbTipoStampa.Text = "Tipo di stampa"
    Me.lbTipoStampa.Tooltip = ""
    Me.lbTipoStampa.UseMnemonic = False
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
    Me.pnTipoStampa.Location = New System.Drawing.Point(626, 1)
    Me.pnTipoStampa.Name = "pnTipoStampa"
    Me.pnTipoStampa.NTSActiveTrasparency = True
    Me.pnTipoStampa.Size = New System.Drawing.Size(145, 28)
    Me.pnTipoStampa.TabIndex = 94
    Me.pnTipoStampa.Text = "NtsPanel1"
    '
    'opTipoStampa1
    '
    Me.opTipoStampa1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa1.EditValue = True
    Me.opTipoStampa1.Location = New System.Drawing.Point(65, 5)
    Me.opTipoStampa1.Name = "opTipoStampa1"
    Me.opTipoStampa1.NTSCheckValue = "S"
    Me.opTipoStampa1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa1.Properties.AutoHeight = False
    Me.opTipoStampa1.Properties.Caption = "Completo"
    Me.opTipoStampa1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa1.Size = New System.Drawing.Size(75, 19)
    Me.opTipoStampa1.TabIndex = 1
    '
    'opTipoStampa0
    '
    Me.opTipoStampa0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa0.Location = New System.Drawing.Point(3, 5)
    Me.opTipoStampa0.Name = "opTipoStampa0"
    Me.opTipoStampa0.NTSCheckValue = "S"
    Me.opTipoStampa0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa0.Properties.AutoHeight = False
    Me.opTipoStampa0.Properties.Caption = "Ridotto"
    Me.opTipoStampa0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa0.Size = New System.Drawing.Size(65, 19)
    Me.opTipoStampa0.TabIndex = 0
    '
    'cmdApriFiltri
    '
    Me.cmdApriFiltri.Image = CType(resources.GetObject("cmdApriFiltri.Image"), System.Drawing.Image)
    Me.cmdApriFiltri.ImageAlignment = DevExpress.Utils.HorzAlignment.[Default]
    Me.cmdApriFiltri.ImageText = ""
    Me.cmdApriFiltri.Location = New System.Drawing.Point(331, 3)
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
    Me.cbFiltro.Location = New System.Drawing.Point(131, 7)
    Me.cbFiltro.Name = "cbFiltro"
    Me.cbFiltro.NTSDbField = ""
    Me.cbFiltro.Properties.AutoHeight = False
    Me.cbFiltro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbFiltro.Properties.DropDownRows = 30
    Me.cbFiltro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbFiltro.SelectedValue = ""
    Me.cbFiltro.Size = New System.Drawing.Size(194, 20)
    Me.cbFiltro.TabIndex = 4
    Me.cbFiltro.ValueMember = ""
    '
    'lbFiltri
    '
    Me.lbFiltri.AutoSize = True
    Me.lbFiltri.BackColor = System.Drawing.Color.Transparent
    Me.lbFiltri.Location = New System.Drawing.Point(13, 10)
    Me.lbFiltri.Name = "lbFiltri"
    Me.lbFiltri.NTSDbField = ""
    Me.lbFiltri.Size = New System.Drawing.Size(79, 13)
    Me.lbFiltri.TabIndex = 3
    Me.lbFiltri.Text = "Configurazione"
    Me.lbFiltri.Tooltip = ""
    Me.lbFiltri.UseMnemonic = False
    '
    'pnFiltriExt
    '
    Me.pnFiltriExt.AllowDrop = True
    Me.pnFiltriExt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltriExt.Appearance.Options.UseBackColor = True
    Me.pnFiltriExt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltriExt.Controls.Add(Me.ceFiltriExt)
    Me.pnFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltriExt.Name = "pnFiltriExt"
    Me.pnFiltriExt.NTSActiveTrasparency = True
    Me.pnFiltriExt.Size = New System.Drawing.Size(765, 435)
    Me.pnFiltriExt.TabIndex = 2
    Me.pnFiltriExt.Text = "NtsPanel1"
    '
    'FRMMGSCHE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(774, 527)
    Me.Controls.Add(Me.tsSche)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSCHE"
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPrelmovimenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrelmovimenti.ResumeLayout(False)
    Me.fmPrelmovimenti.PerformLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPermatricole.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRigheInevase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTc.ResumeLayout(False)
    Me.fmTc.PerformLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsSche, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSche.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.pnPanel1Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1Left.ResumeLayout(False)
    Me.pnPanel1Left.PerformLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSoloMovimentati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSoloMovimentati.ResumeLayout(False)
    Me.fmSoloMovimentati.PerformLayout()
    CType(Me.ckPerMagazzino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNonMovimentati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNonMovimentati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLottoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLottoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFaseini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFasefin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmarcfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmarcini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacomme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcomme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCausale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUbicazini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUbicazfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAmatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSaldiIniziali.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStorico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    CType(Me.cbIncludi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStampaFiltri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoStampa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri2.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTipoStampa.ResumeLayout(False)
    Me.pnTipoStampa.PerformLayout()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltriExt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltriExt.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      cbFiltro.NTSSetParam(oApp.Tr(Me, 129195141008889394, "Configurazione"))
      opTipoStampa0.NTSSetParam(oMenu, oApp.Tr(Me, 128735523170922000, "Ridotto"), "S")
      opTipoStampa1.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229218750, "Completo"), "S")
      ckStorico.NTSSetParam(oMenu, oApp.Tr(Me, 128644905943641169, "Considera lo storico"), "S", "N")
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128644905942858714, "Dalla data"), False)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128644905943171696, "alla data"), False)
      ckSaldiIniziali.NTSSetParam(oMenu, oApp.Tr(Me, 128595426238125000, "C&onsidera solo le righe non evase"), "S", "N")
      edAmagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237500000, "a magazzino"), tabmaga)
      edDamagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237656250, "Da Magazzino"), tabmaga)
      cbCodart.NTSSetParam(oApp.Tr(Me, 130378881630028762, "Articolo"))
      edAcodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234218750, "a codice articolo"), tabartico, False)
      edDacodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234687500, "Da codice articolo"), tabartico, True)
      edCodlsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130378881142506099, "Lista selezionata articolo"), tablsar)
      cbConto.NTSSetParam(oApp.Tr(Me, 130378892331840956, "Conto"))
      edAconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235000000, "a conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235156250, "Da conto"), tabanagrac)
      edCodlsel.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130378891845127165, "Lista selezionata clienti/fornitori"), tablsel)
      edCodmarcfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426236718750, "a marca"), tabmarc)
      edCodmarcini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237031250, "Da marca"), tabmarc)
      edFasefin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232968750, "a fase"), tabartfasi)
      edFasefin.ArtiPerFase = edAcodart
      edFaseini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426233281250, "Da fase"), tabartfasi)
      edFaseini.ArtiPerFase = edDacodart
      If oCleSche.bLottoNew = False Then
        edLottoA.NTSSetParam(oMenu, oApp.Tr(Me, 129518379212535051, "A lotto"), 9, True)
        edLottoDa.NTSSetParam(oMenu, oApp.Tr(Me, 129518379226051195, "Da lotto"), 9, True)
      Else
        edLottoA.NTSSetParam(oMenu, oApp.Tr(Me, 128595426233750000, "A lotto"), 50, True)
        edLottoDa.NTSSetParam(oMenu, oApp.Tr(Me, 128595426233906250, "Da lotto"), 50, True)
      End If
      edAcomme.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644905942545732, "a commessa"), tabcommess)
      edDacomme.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644905942702223, "Da commessa"), tabcommess)
      edDamatr.NTSSetParam(oMenu, oApp.Tr(Me, 128644905941606786, "Da matricola"), 30, True)
      edAmatr.NTSSetParam(oMenu, oApp.Tr(Me, 128644905942076259, "a matricola"), 30, True)
      edUbicazini.NTSSetParam(oMenu, oApp.Tr(Me, 128644905940980822, "Da ubicazione"), 18, True)
      edUbicazfin.NTSSetParam(oMenu, oApp.Tr(Me, 128644905941450295, "a ubicazione"), 18, True)
      edGruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232656250, "Gruppo"), tabgmer)
      edSottogr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232500000, "Sottogruppo"), tabsgme)
      edCausale.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644905940511349, "Causale"), tabcaum)
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426231250000, "Linea/famiglia articolo"), tabcfam, True)
      cbTipoStampa.NTSSetParam(oApp.Tr(Me, 129640284364511718, "TIPO STAMPA"))
      cbIncludi.NTSSetParam(oApp.Tr(Me, 129640901980788242, "Includi"))
      ckSalto.NTSSetParam(oMenu, oApp.Tr(Me, 128644905940198367, "Salto pagina per ogni articolo"), "S", "N")
      ckStampaFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 129640929964742026, "Stampa i filtri impostati nel report"), "S", "N")
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 128595426229375000, "Preleva movimenti"))
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230312500, "Serie"), "S", "N")
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426232187500, "Serie"), CLN__STD.SerieMaxLen, True)
      ckRigheInevase.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230000000, "Solo righe non evase"), "S", "N")
      ckPermatricole.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229843750, "Matricole (solo in griglia)"), "S", "N")
      ckSelAnnoStag.NTSSetParam(oMenu, oApp.Tr(Me, 128595426228437500, "Seleziona Anno/Stagione"), "S", "N")
      edAnnotco.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229062500, "Anno"), "0", 4, 0, 2099)
      edCodstag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426228906250, "Stagione"), tabstag)
      ckNonMovimentati.NTSSetParam(oMenu, oApp.Tr(Me, 130289107687319416, "Solo non movimentati dal"), "S", "N")
      edNonMovimentati.NTSSetParam(oMenu, oApp.Tr(Me, 130289108032869794, "Solo non movimentati dal"), True)
      ckPerMagazzino.NTSSetParam(oMenu, oApp.Tr(Me, 130289121338373487, "Distingui per magazzino"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      grvFiltri1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia filtri 1"))
      grvFiltri1.NTSAllowDelete = False
      grvFiltri1.NTSAllowInsert = False
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128491820088062000, "Nome filtro"), dttCampi, "xx_nome", "cb_nomcampo")
      xx_valoreda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128491820607854000, "Valore filtro DA"), 0)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128584498502187500, "Valore filtro A"), 0)
      xx_valoreda.NTSSetParamZoom("__")
      xx_valorea.NTSSetParamZoom("__")
      '--------------------------------------------------------------------------------------------------------------
      edLottoDa.NTSForzaVisZoom = True
      edLottoA.NTSForzaVisZoom = True
      '--------------------------------------------------------------------------------------------------------------
      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 130422099822017126, "Filtri Estesi"), "BSMGSCHE", New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTMAG")
      ceFiltriExt.AggiungiTabella("MOVMAG")
      ceFiltriExt.AggiungiTabella("KEYMAG")
      ceFiltriExt.AggiungiTabella("ARTICO")
      ceFiltriExt.InizializzaFiltri()
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGSCHE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleSche.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("movmag", dttCampi) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      CreaDatatableFiltri()
      '--------------------------------------------------------------------------------------------------------------
      dcFiltri1.DataSource = dsFiltri.Tables("FILTRI1")
      dsFiltri.AcceptChanges()
      grFiltri1.DataSource = dcFiltri1
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      tsSche.SelectedTabPageIndex = 0
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.bModTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleSche.bModuloCRM = True
      Else
        oCleSche.bModuloCRM = False
      End If
      oCleSche.bModuloCRM = False
      oCleSche.bLogisticaEstesa = (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtLEX) Or CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP))
      '--------------------------------------------------------------------------------------------------------------
      edDacodart.Text = " " 'per SBS
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.LeggiDatiDitta(DittaCorrente) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Setta i parametri passati dal chiamante (arti, clie, ...)
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.nScheFase = 0
      oCleSche.bScheGestDaCons = False
      oCleSche.bScheStampaSuGriglia = False
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          oCleSche.bScheDagest = True
          Select Case Mid(oCallParams.strParam, 1, 4)
            Case "APRI"
              oCleSche.strScheCodart = Trim(Mid(oCallParams.strParam, 6, CLN__STD.CodartMaxLen))
              oCleSche.nScheMagaz = NTSCInt(Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 7, 4)))
              oCleSche.lScheConto = NTSCInt(Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 12, 9)))
              oCleSche.strScheOrdin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 22, 1))
              If Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 24, 4) <> "" Then
                oCleSche.nScheFase = NTSCInt(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 24, 4))
              Else
                oCleSche.nScheFase = 0
              End If
              If oCallParams.strParam.Trim <> "" Then
                If Microsoft.VisualBasic.Right(oCallParams.strParam, 2) = "-1" Then
                  oCleSche.bScheStampaSuGriglia = True
                End If
              End If
            Case "APRD"
              oCleSche.bScheGestDaCons = True
              oCleSche.lScheConto = NTSCInt(Trim(Mid(oCallParams.strParam, 6, 9)))
              oCleSche.strScheCodart = Trim(Mid(oCallParams.strParam, 16, CLN__STD.CodartMaxLen))
              oCleSche.strScheTipork = Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 17, 1)
              oCleSche.strScheDatini = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 19, 10))
              oCleSche.strScheDatfin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 30, 10))
              oCleSche.strScheOrdin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 41, 1))
              oCleSche.bScheFiltri = CBool(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 43, 1))
          End Select
        Else
          oCleSche.bScheDagest = False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleSche.bModuloCRM = False
      If oCleSche.bModuloCRM Then
        oCleSche.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleSche.bAmm, oCleSche.strAccvis, oCleSche.strAccmod, oCleSche.strRegvis, oCleSche.strRegmod)
        If oCleSche.bIsCRMUser Then
          oCleSche.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleSche.nCodcageoperat)
          If oCleSche.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.strScheCodart = "" Then
        edDacodart.Text = NTSCStr(oCleSche.strScheCodart)
        edAcodart.Text = NTSCStr(oCleSche.strScheCodart)
      Else
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, " "c)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
      End If
      If Not oCleSche.nScheMagaz = 0 Then
        edDamagaz.Text = NTSCStr(oCleSche.nScheMagaz)
        edAmagaz.Text = NTSCStr(oCleSche.nScheMagaz)
      Else
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
      End If
      If Not oCleSche.lScheConto = 0 Then
        edDaconto.Text = NTSCStr(oCleSche.lScheConto)
        edAconto.Text = NTSCStr(oCleSche.lScheConto)
      Else
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
      End If
      edCodmarcini.Text = "0"
      edCodmarcfin.Text = "9999"
      Select Case oCleSche.strScheOrdin
        Case "A" : cbTipoStampa.SelectedValue = "A"
        Case "C" : cbTipoStampa.SelectedValue = "B"
      End Select
      If oCleSche.bScheStampaSuGriglia = True And oCleSche.strScheOrdin = "K" Then
        cbTipoStampa.SelectedValue = "K"
      Else
        oCleSche.bScheStampaSuGriglia = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.strTTStloco = "TTSTLOCO"
      oCleSche.lIITTStloco = oMenu.GetTblInstId("TTSTLOCO", False)
      oCleSche.strTTStlocs = "TTSTLOCS"
      oCleSche.lIITTStlocs = oMenu.GetTblInstId("TTSTLOCS", False)
      oCleSche.strTTStschea = "TTSTSCHEA"
      oCleSche.lIITTStschea = oMenu.GetTblInstId("TTSTSCHEA", False)
      oCleSche.strTTStMatr = "TTSTMATR"
      oCleSche.lIITTStMatr = oMenu.GetTblInstId("TTSTMATR", False)
      oCleSche.strTTStMats = "TTSTMATS"
      oCleSche.lIITTStMats = oMenu.GetTblInstId("TTSTMATS", False)
      oCleSche.lIITTStlocu = oMenu.GetTblInstId("TTSTLOCU", False)
      '--------------------------------------------------------------------------------------------------------------
      If Not NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap) = "" Then
        edDatini.Text = NTSCStr(DateAdd("d", 1, NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)))
        oCleSche.strDtulap = NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)
      Else
        edDatini.Text = IntSetDate("01/01/1900")
        oCleSche.strDtulap = IntSetDate("01/01/1900")
      End If
      edDatini.Enabled = False
      edDatfin.Text = IntSetDate("31/12/2099")
      If oCleSche.nScheFase = 0 Then
        edFaseini.Text = "0"
        edFasefin.Text = "9999"
      Else
        edFaseini.Text = NTSCStr(oCleSche.nScheFase)
        edFasefin.Text = NTSCStr(oCleSche.nScheFase)
      End If
      edLottoDa.Text = ""
      If oCleSche.bLottoNew = False Then
        edLottoA.Text = "".PadLeft(9, "9"c)
      Else
        edLottoA.Text = "".PadLeft(50, "z"c)
      End If
      edDacomme.Text = "0"
      edAcomme.Text = "999999999"
      cbTipodoc.SelectedValue = "1"
      cbIncludi.SelectedValue = "A"
      edSerie.Text = ""
      edCausale.Text = "0"
      edGruppo.Text = "0"
      edSottogr.Text = "0"
      edCodcfam.Text = ""
      lbXx_codcfam.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      If oCleSche.bModTCO = False Then
        fmTc.Visible = False
        ckSelAnnoStag.Visible = False
      End If
      lbAnnotco.Enabled = False
      lbCodstag.Enabled = False
      edAnnotco.Enabled = False
      edCodstag.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipodoc.SelectedValue) = "1") And _
         (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (ckStorico.Checked = True) Then
        GctlSetVisEnab(ckSaldiIniziali, False)
      Else
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      End If
      If oCleSche.bScheGestDaCons = True Then
        If oCleSche.lScheConto = 0 Then
          edDaconto.Text = "0"
          edAconto.Text = "999999999"
        Else
          edDaconto.Text = NTSCStr(oCleSche.lScheConto)
          edAconto.Text = NTSCStr(oCleSche.lScheConto)
        End If
        If oCleSche.strScheCodart = "" Then
          edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, " "c)
          edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Else
          edDacodart.Text = oCleSche.strScheCodart
          edAcodart.Text = oCleSche.strScheCodart
        End If
        If oCleSche.strScheTipork <> "§" Then cbTipodoc.SelectedValue = oCleSche.strScheTipork
        If oCleSche.strScheDatini <> "" Then
          ckStorico.Checked = True
          If Microsoft.VisualBasic.Right(oCleSche.strScheDatini, 3) = "/00" Then
            oCleSche.strScheDatini = Microsoft.VisualBasic.Left(oCleSche.strScheDatini, 6) & "1900"
          End If
          edDatini.Text = oCleSche.strScheDatini
        End If
        If oCleSche.strScheDatfin <> "" Then edDatfin.Text = oCleSche.strScheDatfin
        If oCleSche.bScheFiltri = False Then
          ReportSuGriglia()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleSche.bScheStampaSuGriglia = True Then
        ReportSuGriglia()
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipodoc.SelectedValue) = "W" Then
        GctlSetVisEnab(ckPermatricole, False)
      Else
        ckPermatricole.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128735523680418000, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      '--- Prende la struttura della tabella
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.GetTableStructMovIfil(dttDefault)
      '--------------------------------------------------------------------------------------------------------------
      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")
      '--------------------------------------------------------------------------------------------------------------
      ComponiDatatable(dttDefault, Me)
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStampaFiltri.Enabled = True) Then
        ckStampaFiltri.Checked = CBool(oMenu.GetSettingBus("BSMGSCHE", "RECENT", ".", "StampaFiltriInReport", "0", " ", "0"))
      End If
      '--------------------------------------------------------------------------------------------------------------
      tlbNoModal.Checked = CBool(oMenu.GetSettingBus("BSMGSCHE", "RECENT", ".", "ChildNoModal", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHE_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      'Necessario per ovviare al problema che non caricava i dati se si forzava un valore del combo dalla configuratore user interface
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSMGSCHE", "RECENT", ".", "ChildNoModal", IIf(tlbNoModal.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    Dim strTmp As String = ""
    Dim i As Integer = 0
    dsFiltri.Tables("FILTRI1").AcceptChanges()
    For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
    Next
    strTmp = strTmp.Substring(0, strTmp.Length - 1)
    oMenu.SaveSettingBus("BNMGSCHE", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
  End Sub
#End Region

#Region "Eventi Toolbar"
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
          oApp.MsgBoxErr(oApp.Tr(Me, 128735523410694000, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edFasefin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edFasefin.Text)
        oParam.strTipo = edAcodart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edFasefin.Text Then edFasefin.NTSTextDB = NTSZOOM.strIn

      ElseIf edLottoDa.Focused Or edLottoA.Focused Then
        '------------------------------------
        'zoom lotti
        If edDacodart.Text.Trim = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128569627222968750, "Indicare prima il codice articolo"))
          Return
        End If
        '-------------------------------------------------------------------------------------
        '--- Controlla che l'articolo di partenza e quello di arrivo siano uguali
        '-------------------------------------------------------------------------------------
        If edDacodart.Text <> edAcodart.Text Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128596603368815755, "L'articolo di partenza deve essere uguale all'articolo di destinazione per lo zoom/ricerca sui lotti relativi."))
          Return
        End If

        If Not oCleSche.CheckArtico(edDacodart.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128644986141468040, "Articolo inesistente." & vbCrLf & "Indicare un articolo valido prima di passare alla selezione di un lotto relativo."))
          Return
        End If

        oParam.strTipo = edDacodart.Text
        'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
        'oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
        'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti

        If edLottoDa.Focused Then
          NTSZOOM.strIn = edLottoDa.Text
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> edLottoDa.Text Then edLottoDa.NTSTextDB = NTSZOOM.strIn
        ElseIf edLottoA.Focused Then
          NTSZOOM.strIn = edLottoA.Text
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> edLottoA.Text Then edLottoA.NTSTextDB = NTSZOOM.strIn
        End If

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
      lbStatus.Text = oApp.Tr(Me, 128498073391304728, "Pronto.")
      Me.Refresh()
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
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edCodstag_Validated(NTSCInt(edCodstag.Text), strTmp) Then
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
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edGruppo_Validated(NTSCInt(edGruppo.Text), strTmp) Then
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
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edSottogr_Validated(NTSCInt(edSottogr.Text), strTmp) Then
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
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp) Then
        edCodcfam.Text = NTSCStr(edCodcfam.OldEditValue)
      Else
        lbXx_codcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDacodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacodart.Validated
    Try
      If edDacodart.Text <> "".PadLeft(CLN__STD.CodartMaxLen) Then
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
  Public Overridable Sub edCausale_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCausale.Validated
    Dim strTmp As String = ""
    Try
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edCausale_Validated(NTSCInt(edCausale.Text), strTmp) Then
        edCausale.Text = NTSCStr(edCausale.OldEditValue)
      Else
        lbXx_causale.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDacomme_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacomme.Validated
    Try
      If NTSCInt(edDacomme.Text) <> 0 Then edAcomme.Text = edDacomme.Text
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edLottoDa_ValidatedAndChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edLottoDa.ValidatedAndChanged
    Try
      If oCleSche Is Nothing Then Return
      If oCleSche.bLottoNew = False Then
        If edLottoDa.Text <> "" Then edLottoDa.Text = NTSCInt(edLottoDa.Text).ToString("000000000")
        If NTSCDec(edLottoDa.Text) <> 0 Then edLottoA.Text = edLottoDa.Text
      Else
        If IsNumeric(edLottoDa.Text) Then
          If NTSCDec(edLottoDa.Text) <> 0 Then edLottoA.Text = edLottoDa.Text
        Else
          If edLottoDa.Text.Trim <> "" Then edLottoA.Text = edLottoDa.Text
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edLottoA_ValidatedAndChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edLottoA.ValidatedAndChanged
    Try
      If oCleSche Is Nothing Then Return
      If oCleSche.bLottoNew = False Then
        If edLottoA.Text <> "" Then edLottoA.Text = NTSCInt(edLottoA.Text).ToString("000000000")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edCodlsar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsar.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleSche Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.edCodlsar_Validated(NTSCInt(edCodlsar.Text), strTmp) Then
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
      If oCleSche Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.edCodlsel_Validated(NTSCInt(edCodlsel.Text), strTmp) Then
        edCodlsel.Text = NTSCStr(edCodlsel.OldEditValue)
      Else
        lbDescodlsel.Text = strTmp
      End If
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
  Public Overridable Sub ckPermatricole_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPermatricole.CheckedChanged
    Try
      If ckPermatricole.Checked = True Then
        GctlSetVisEnab(edDamatr, False)
        edDamatr.Text = "".PadLeft(30, " "c)
        GctlSetVisEnab(edAmatr, False)
        edAmatr.Text = "".PadLeft(30, "z"c)
      Else
        edDamatr.Enabled = False
        edDamatr.Text = ""
        edAmatr.Enabled = False
        edAmatr.Text = ""
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
        edSerie.Text = " "
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
  Public Overridable Sub ckStorico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckStorico.CheckedChanged
    Try
      If ckStorico.Checked = False Then
        If Not NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap) = "" Then
          edDatini.Text = NTSCStr(DateAdd("d", 1, NTSCDate(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)))
        Else
          edDatini.Text = IntSetDate("01/01/1900")
        End If
        edDatini.Enabled = False
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      Else
        If (NTSCStr(cbTipodoc.SelectedValue) = "1") And (NTSCStr(cbTipoStampa.SelectedValue) = "A") Then
          GctlSetVisEnab(ckSaldiIniziali, False)
        Else
          ckSaldiIniziali.Checked = True
          ckSaldiIniziali.Enabled = False
        End If
        GctlSetVisEnab(edDatini, False)
        If bOnLoadingFromSettings = False Then edDatini.Text = IntSetDate("01/01/1900")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckNonMovimentati_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNonMovimentati.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckNonMovimentati.Checked = True Then
        GctlSetVisEnab(edNonMovimentati, False)
        GctlSetVisEnab(ckPerMagazzino, False)
        edNonMovimentati.Text = Now.ToShortDateString
        edNonMovimentati.Focus()
      Else
        edNonMovimentati.Text = ""
        ckPerMagazzino.Checked = False
        edNonMovimentati.Enabled = False
        ckPerMagazzino.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbTipodoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipodoc.SelectedIndexChanged
    Try
      Select Case NTSCStr(cbTipodoc.SelectedValue)
        Case "1"
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStorico.Checked = True) Then
            GctlSetVisEnab(ckSaldiIniziali, False)
          Else
            ckSaldiIniziali.Checked = True
            ckSaldiIniziali.Enabled = False
          End If
          ckSerie.Checked = False
          ckSerie.Enabled = False
          If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
            cbIncludi.SelectedValue = "A"
            GctlSetVisEnab(cbIncludi, False)
          Else
            cbIncludi.SelectedValue = "A"
            cbIncludi.Enabled = False
          End If
          ckRigheInevase.Checked = False
          ckRigheInevase.Enabled = False
          ckPermatricole.Checked = False
          ckPermatricole.Enabled = False
          Select Case NTSCStr(cbTipoStampa.SelectedValue)
            Case "A", "B" : SettaTipoStampa(False)
            Case Else : SettaTipoStampa(True)
          End Select
        Case "W"
          ckSaldiIniziali.Checked = True
          ckSaldiIniziali.Enabled = False
          SettaTipoStampa(True)
          GctlSetVisEnab(cbTipodoc, False)
          GctlSetVisEnab(ckSerie, False)
          If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
            cbIncludi.SelectedValue = "A"
            GctlSetVisEnab(cbIncludi, False)
          Else
            cbIncludi.SelectedValue = "A"
            cbIncludi.Enabled = False
          End If
          GctlSetVisEnab(ckRigheInevase, False)
          GctlSetVisEnab(ckPermatricole, False)
          If (NTSCStr(cbTipoStampa.SelectedValue) <> "A") And (NTSCStr(cbTipoStampa.SelectedValue) <> "B") Then
            cbTipoStampa.SelectedValue = "A"
          End If
        Case Else
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStorico.Checked = True) Then
            GctlSetVisEnab(ckSaldiIniziali, False)
          Else
            ckSaldiIniziali.Checked = True
            ckSaldiIniziali.Enabled = False
          End If
          GctlSetVisEnab(ckSerie, False)
          If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
            cbIncludi.SelectedValue = "A"
            GctlSetVisEnab(cbIncludi, False)
          Else
            cbIncludi.SelectedValue = "A"
            cbIncludi.Enabled = False
          End If
          ckRigheInevase.Checked = False
          ckRigheInevase.Enabled = False
          ckPermatricole.Checked = False
          ckPermatricole.Enabled = False
          Select Case NTSCStr(cbTipoStampa.SelectedValue)
            Case "A", "B" : SettaTipoStampa(False)
            Case Else : SettaTipoStampa(True)
          End Select
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbTipoStampa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoStampa.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipodoc.SelectedValue) = "W") And _
         ((NTSCStr(cbTipoStampa.SelectedValue) <> "A") And (NTSCStr(cbTipoStampa.SelectedValue) <> "B")) Then
        cbTipoStampa.SelectedValue = "A"
      End If
      '--------------------------------------------------------------------------------------------------------------
      ScelteOptionButton()
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") Or (NTSCStr(cbTipoStampa.SelectedValue) = "B") Then
        If NTSCStr(cbTipodoc.SelectedValue) <> "W" Then SettaTipoStampa(False) Else SettaTipoStampa(True)
      Else
        SettaTipoStampa(True)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbTipoStampa.SelectedValue
        Case "B"
          If ckNonMovimentati.Enabled = True Then
            ckNonMovimentati.Checked = False
            ckNonMovimentati.Enabled = False
          End If
        Case Else
          If ckNonMovimentati.Enabled = False Then GctlSetVisEnab(ckNonMovimentati, False)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
          GctlSetVisEnab(lbAcodart, False)
          GctlSetVisEnab(lbFaseini, False)
          GctlSetVisEnab(lbFasefin, False)
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
          lbAcodart.Visible = False
          edFaseini.Text = "0"
          edFasefin.Text = "".PadLeft(4, "9"c)
          lbFaseini.Enabled = False
          lbFasefin.Enabled = False
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
          GctlSetVisEnab(lbAconto, False)
          edDaconto.Focus()
        Case "B"
          edDaconto.Text = "0"
          edAconto.Text = "".PadLeft(9, "9"c)
          edDaconto.Enabled = False
          edAconto.Enabled = False
          edDaconto.Visible = False
          edAconto.Visible = False
          lbAconto.Visible = False
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

#Region "Eventi CommandButton"
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
                  oApp.MsgBoxErr(oApp.Tr(Me, 128735523533622000, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
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
      strTmp = NTSCStr(oMenu.GetSettingBus("BNMGSCHE", "RECENT", ".", "Filtri1", "", " ", ""))
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
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
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
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
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
        oParam.strTipo = "MOVMAG"
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
  Public Overridable Function CampiMemoInSelezione() As Boolean
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsFiltri.Tables("FILTRI1").Rows.Count - 1)
        With dsFiltri.Tables("FILTRI1").Rows(i)
          If (NTSCStr(!xx_nome).Trim <> ".") And (NTSCStr(!xx_valoreda) <> "") And (NTSCStr(!xx_valorea) <> "") Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(!xx_nome))
            If NTSCInt(dtrT(0)!cb_tipocampo.ToString) = 12 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 130056573971850323, "Attenzione!" & vbCrLf & _
                "Non Ã¨ possibile indicare un campo Note (memo), in 'Altri filtri corpo'." & vbCrLf & _
                "Modificare la selezione dei dati."))
              Return False
            End If
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

#Region "Gestione filtri"
  Public Overridable Sub CaricaFiltri()
    Dim dttTmp As New DataTable
    Try
      oCleSche.CaricaFiltri(dttTmp)

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
      oCleSche.GetTableStructMovIfil(dttCampiForm)

      dttCampiForm.Columns.Add("xx_descr")
      dttCampiForm.Columns.Add("xx_info")
      dttCampiForm.Columns.Add("xx_tipo")

      'Compongo il datatable con i campi da passare al programma per la gestione dei dati
      If Not ComponiDatatable(dttCampiForm, Me) Then Return

      'Riempie le colonne mancanti
      For z As Integer = 0 To dttCampiForm.Rows.Count - 1
        dttCampiForm.Rows(z)!mo_child = "BNMGSCHE"
        dttCampiForm.Rows(z)!mo_form = "FRMMGSCHE"
        dttCampiForm.Rows(z)!mo_locked = "N"
        dttCampiForm.Rows(z)!mo_codifil = NTSCInt(cbFiltro.SelectedValue)
      Next

      'Avvia il programma
      oPar.ctlPar1 = dttCampiForm
      oPar.strPar1 = "BNMGSCHE"
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
      '--------------------------------------------------------------------------------------------------------------
      bOnLoadingFromSettings = True
      '--------------------------------------------------------------------------------------------------------------
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bOnLoadingFromSettings = False
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
        If Not oCleSche.LeggiFiltro(lCod, "BNMGSCHE", "FRMMGSCHE", dttPersForm) Then Return False
        AggiornaForm(dttPersForm, False)

        If Not grvFiltri1.NTSGetCurrentDataRow() Is Nothing Then grvFiltri1_NTSFocusedRowChanged(Me, Nothing)
        '------------------------------------------------------------------------------------------------------------
        If (edDacodart.Text.Trim = "") And (edAcodart.Text.ToLower = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
          If (Not oCallParams Is Nothing) Then
            If (oCallParams.strParam <> "") And (oCleSche.bScheDagest = True) Then
              Select Case Mid(oCallParams.strParam, 1, 4)
                Case "APRI"
                  edDacodart.Text = oCleSche.strScheCodart
                  edAcodart.Text = oCleSche.strScheCodart
              End Select
            End If
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
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

#Region "Azioni su Tabelle Temporanee"
  Public Overridable Function RiempiTTStloco(ByVal bSuTTlocu As Boolean) As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim bLapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "C", True, False))
    Dim bLaperti As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "D", True, False))
    Dim bCapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "F", True, False))
    Dim bCaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "G", True, False))
    Dim bUapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "L", True, False))
    Dim bUaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "M", True, False))
    Dim strError As String = ""

    Try
      If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        bPrelevaSolo = True
      End If

      lbStatus.Text = oApp.Tr(Me, 129080485027141294, "Elaborazioni su lotti/commesse/ubicazioni/fase")
      Me.Refresh()

      If Not oCleSche.RiempiTTStloco(bSuTTlocu, edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                                     edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                                     edAconto.Text, edFaseini.Text, edFasefin.Text, ckStorico.Checked, _
                                     bLapchiu, bLaperti, edLottoDa.Text, edLottoA.Text, _
                                     bCapchiu, bCaperte, edDacomme.Text, edAcomme.Text, _
                                     bUapchiu, bUaperte, edUbicazini.Text, edUbicazfin.Text, _
                                     bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                                     edCausale.Text, edGruppo.Text, edSottogr.Text, edCodmarcini.Text, _
                                     edCodmarcfin.Text, edCodcfam.Text, ckSelAnnoStag.Checked, _
                                     edAnnotco.Text, edCodstag.Text, strError, ceFiltriExt.GeneraQuerySQL(), _
                                     edClassificazioneLivello1.Text, _
                                     edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                                     edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                                     cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), _
                                     cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      lbStatus.Text = ""
      Me.Refresh()
    End Try
  End Function

  Public Overridable Function RiempiTTStlocs() As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim bLapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "C", True, False))
    Dim bLaperti As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "D", True, False))
    Dim bLsaldo As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "E", True, False))
    Dim bCapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "F", True, False))
    Dim bCaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "G", True, False))
    Dim bCsaldo As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "H", True, False))
    Dim bUapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "L", True, False))
    Dim bUaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "M", True, False))
    Dim bUsaldo As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "N", True, False))
    Dim strError As String = ""

    Try
      If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        bPrelevaSolo = True
      End If

      lbStatus.Text = oApp.Tr(Me, 129080485163865294, "Elaborazioni su saldi lotti/commesse/ubicazioni/fase")
      Me.Refresh()

      If Not oCleSche.RiempiTTStlocs(edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                               edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                               edAconto.Text, edFaseini.Text, edFasefin.Text, ckStorico.Checked, _
                               bLapchiu, bLaperti, edLottoDa.Text, edLottoA.Text, _
                               bCapchiu, bCaperte, edDacomme.Text, edAcomme.Text, _
                               bUapchiu, bUaperte, edUbicazini.Text, edUbicazfin.Text, _
                               bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                               edCausale.Text, edGruppo.Text, edSottogr.Text, edCodmarcini.Text, _
                               edCodmarcfin.Text, edCodcfam.Text, ckSelAnnoStag.Checked, _
                               edAnnotco.Text, edCodstag.Text, bLsaldo, bCsaldo, _
                               bUsaldo, strError, ceFiltriExt.GeneraQuerySQL(), _
                               edClassificazioneLivello1.Text, _
                               edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                               edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                               cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), _
                               cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaldoStorico() As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim bLapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "C", True, False))
    Dim bLaperti As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "D", True, False))
    Dim bCapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "F", True, False))
    Dim bCaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "G", True, False))
    Dim bUapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "L", True, False))
    Dim bUaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "M", True, False))
    Dim strError As String = ""

    Try
      If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        bPrelevaSolo = True
      End If

      lbStatus.Text = oApp.Tr(Me, 129080485546996984, "Determinazione saldo storico articoli movimentati ...")
      Me.Refresh()

      If Not oCleSche.SaldoStorico(edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                         edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                         edAconto.Text, edFaseini.Text, edFasefin.Text, ckStorico.Checked, _
                         bLapchiu, bLaperti, edLottoDa.Text, edLottoA.Text, _
                         bCapchiu, bCaperte, edDacomme.Text, edAcomme.Text, _
                         bUapchiu, bUaperte, edUbicazini.Text, edUbicazfin.Text, _
                         bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                         edCausale.Text, edGruppo.Text, edSottogr.Text, edCodmarcini.Text, _
                         edCodmarcfin.Text, edCodcfam.Text, ckSelAnnoStag.Checked, _
                         edAnnotco.Text, edCodstag.Text, ckSaldiIniziali.Checked, strError, _
                         ceFiltriExt.GeneraQuerySQL(), edClassificazioneLivello1.Text, _
                         edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                         edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                         cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), _
                         cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaldoStoricoNonMov() As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim bLapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "C", True, False))
    Dim bLaperti As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "D", True, False))
    Dim bCapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "F", True, False))
    Dim bCaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "G", True, False))
    Dim bUapchiu As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "L", True, False))
    Dim bUaperte As Boolean = CBool(IIf(NTSCStr(cbTipoStampa.SelectedValue) = "M", True, False))
    Dim bEsist As Boolean = CBool(IIf(NTSCStr(cbIncludi.SelectedValue) = "C", True, False))
    Dim strError As String = ""

    Try
      If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        bPrelevaSolo = True
      End If

      lbStatus.Text = oApp.Tr(Me, 129080485680746128, "Determinazione saldo storico articoli non movimentati ...")
      Me.Refresh()

      If Not oCleSche.SaldoStoricoNonMov(edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                   edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                   edAconto.Text, edFaseini.Text, edFasefin.Text, ckStorico.Checked, _
                   bLapchiu, bLaperti, edLottoDa.Text, edLottoA.Text, _
                   bCapchiu, bCaperte, edDacomme.Text, edAcomme.Text, _
                   bUapchiu, bUaperte, edUbicazini.Text, edUbicazfin.Text, _
                   bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                   edCausale.Text, edGruppo.Text, edSottogr.Text, edCodmarcini.Text, _
                   edCodmarcfin.Text, edCodcfam.Text, ckSelAnnoStag.Checked, _
                   edAnnotco.Text, edCodstag.Text, ckSaldiIniziali.Checked, bEsist, _
                   strError, ceFiltriExt.GeneraQuerySQL(), edClassificazioneLivello1.Text, _
                   edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                   edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                   cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), _
                   cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      oMenu.ResetTblInstId("TTSTSCHEA", False, oCleSche.lIITTStschea)
      oMenu.ResetTblInstId("TTSTLOCO", False, oCleSche.lIITTStloco)
      oMenu.ResetTblInstId("TTSTLOCS", False, oCleSche.lIITTStlocs)
      oMenu.ResetTblInstId("TTSTMATR", False, oCleSche.lIITTStMatr)
      oMenu.ResetTblInstId("TTSTMATS", False, oCleSche.lIITTStMats)
      oMenu.ResetTblInstId("TTSTLOCU", False, oCleSche.lIITTStlocu)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Sub Avvertimento()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckStorico.Checked = True Then
        Select Case NTSCStr(cbTipoStampa.SelectedValue)
          Case "C", "D", "E", "F", "G", "H", "L", "M", "N"
            oApp.MsgBoxErr(oApp.Tr(Me, 128644089103006347, "ATTENZIONE!!!" & vbCrLf & _
              "Questo tipo elaborazione (con 'Considera lo storico' attivato)" & vbCrLf & _
              "non prevede la ricostruzione di progressivi a data anteriore all'ultimo aggiornamento magazzino." & vbCrLf & _
              "Pertanto i progressivi/rimanenze eventualmente presenti in questa visualizzazione o stampa sono corretti solo se non si è mai proceduto con cancellazione di  documenti di magazzino " & _
              "e se la data iniziale impostata (Dalla Data:) è anteriore a qualsiasi movimento di magazzino presente in archivio!"))
          Case "I", "J", "K"
            oApp.MsgBoxErr(oApp.Tr(Me, 128644089124718286, "ATTENZIONE!!!" & vbCrLf & _
              "Questo tipo di elaborazione (sulle matricole) parte sempre, per ciascun articolo gestito a matricole, dal primo movimento " & _
              "presente in archivio, indipendentemente dalle scelte 'Considera lo storico' e 'Calcola saldi iniziali'!!!"))
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttTipodoc As New DataTable()
    Dim dttTipoStampa As New DataTable()
    Dim dttIncludi As New DataTable()
    Dim dttCodart As New DataTable()
    Dim dttConto As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTipodoc.Columns.Add("cod", GetType(String))
      dttTipodoc.Columns.Add("val", GetType(String))
      dttTipodoc.Rows.Add(New Object() {"1", "Tutti"})
      dttTipodoc.Rows.Add(New Object() {"A", "Fatture Immediate emesse"})
      dttTipodoc.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipodoc.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipodoc.Rows.Add(New Object() {"E", "Note di Addebito emesse"})
      dttTipodoc.Rows.Add(New Object() {"F", "Ricevute Fiscali emesse"})
      dttTipodoc.Rows.Add(New Object() {"I", "Riemissione Ricevute Fiscali"})
      dttTipodoc.Rows.Add(New Object() {"J", "Note di Accredito ricevute"})
      dttTipodoc.Rows.Add(New Object() {"L", "Fatture Immediate ricevute"})
      dttTipodoc.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipodoc.Rows.Add(New Object() {"N", "Note di Accredito emesse"})
      dttTipodoc.Rows.Add(New Object() {"S", "Fatture/Ricevute Fiscali emesse"})
      dttTipodoc.Rows.Add(New Object() {"T", "Carichi da Produzione"})
      dttTipodoc.Rows.Add(New Object() {"U", "Scarichi a Produzione"})
      dttTipodoc.Rows.Add(New Object() {"Z", "Bolle di Movimentazione Interna"})
      dttTipodoc.Rows.Add(New Object() {"W", "Note di Prelievo"})
      dttTipodoc.AcceptChanges()
      cbTipodoc.DataSource = dttTipodoc
      cbTipodoc.ValueMember = "cod"
      cbTipodoc.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttTipoStampa.Columns.Add("cod", GetType(String))
      dttTipoStampa.Columns.Add("val", GetType(String))
      dttTipoStampa.Rows.Add(New Object() {"A", "Per Articolo"})
      dttTipoStampa.Rows.Add(New Object() {"B", "Per Conto"})
      dttTipoStampa.Rows.Add(New Object() {"C", "A Lotti aperti e chiusi"})
      dttTipoStampa.Rows.Add(New Object() {"D", "A Lotti aperti"})
      dttTipoStampa.Rows.Add(New Object() {"E", "Saldo Lotti aperti"})
      dttTipoStampa.Rows.Add(New Object() {"F", "A Commesse aperte e chiuse"})
      dttTipoStampa.Rows.Add(New Object() {"G", "A Commesse aperte"})
      dttTipoStampa.Rows.Add(New Object() {"H", "Saldo Commesse aperte"})
      dttTipoStampa.Rows.Add(New Object() {"I", "A Matricole aperte e chiuse"})
      dttTipoStampa.Rows.Add(New Object() {"J", "A Matricole aperte"})
      dttTipoStampa.Rows.Add(New Object() {"K", "Saldo Matricole aperte"})
      If oCleSche.bLogisticaEstesa = True Then
        dttTipoStampa.Rows.Add(New Object() {"L", "A Ubicazioni aperte e chiuse"})
        dttTipoStampa.Rows.Add(New Object() {"M", "A Ubicazioni aperte"})
        dttTipoStampa.Rows.Add(New Object() {"N", "Saldo Ubicazioni aperte"})
      End If
      dttTipoStampa.AcceptChanges()
      cbTipoStampa.DataSource = dttTipoStampa
      cbTipoStampa.ValueMember = "cod"
      cbTipoStampa.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttIncludi.Columns.Add("cod", GetType(String))
      dttIncludi.Columns.Add("val", GetType(String))
      dttIncludi.Rows.Add(New Object() {"A", "Solo articoli movimentati"})
      dttIncludi.Rows.Add(New Object() {"B", "Tutti gli articoli"})
      dttIncludi.Rows.Add(New Object() {"C", "Articoli in esistenza alla data inizio"})
      dttIncludi.AcceptChanges()
      cbIncludi.DataSource = dttIncludi
      cbIncludi.ValueMember = "cod"
      cbIncludi.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttCodart.Columns.Add("cod", GetType(String))
      dttCodart.Columns.Add("val", GetType(String))
      dttCodart.Rows.Add(New Object() {"A", "Da/a articolo"})
      dttCodart.Rows.Add(New Object() {"B", "Lista selezionata"})
      dttCodart.AcceptChanges()
      cbCodart.DataSource = dttCodart
      cbCodart.ValueMember = "cod"
      cbCodart.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttConto.Columns.Add("cod", GetType(String))
      dttConto.Columns.Add("val", GetType(String))
      dttConto.Rows.Add(New Object() {"A", "Da/a conto"})
      dttConto.Rows.Add(New Object() {"B", "Lista selezionata"})
      dttConto.AcceptChanges()
      cbConto.DataSource = dttConto
      cbConto.ValueMember = "cod"
      cbConto.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      CaricaFiltri()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edDatini.Text) > NTSCDate(edDatfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499747689918, "La data iniziale non può essere superiore a quella finale."))
        edDatfin.Text = IntSetDate("31/12/2099")
        Return False
      End If
      If NTSCInt(edDamagaz.Text) > NTSCInt(edAmagaz.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499815189918, "Il magazzino di partenza non può essere superiore a quello di arrivo."))
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
        Return False
      End If
      If UCase(edDacodart.Text) > UCase(edAcodart.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499598002418, "L'articolo di partenza non può essere superiore a quello di arrivo."))
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Return False
      End If
      If NTSCInt(edDaconto.Text) > NTSCInt(edAconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499709721168, "Il conto di partenza non può essere superiore a quello di arrivo."))
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        Return False
      End If
      If NTSCInt(edCodmarcini.Text) > NTSCInt(edCodmarcfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499785346168, "La marca iniziale non può essere superiore a quella finale."))
        edCodmarcini.Text = "0"
        edCodmarcfin.Text = "9999"
        Return False
      End If
      If NTSCInt(edFaseini.Text) > NTSCInt(edFasefin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499553627418, "La fase di partenza non può essere superiore a quella di arrivo."))
        edFaseini.Text = "0"
        edFasefin.Text = "9999"
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A", "B", "I", "J", "K"
        Case Else
          If edLottoDa.Text > edLottoA.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128735523472158000, "Il lotto iniziale non può essere superiore a quello finale."))
            edLottoDa.Text = ""
            If oCleSche.bLottoNew = False Then
              edLottoA.Text = "".PadLeft(9, "9"c)
            Else
              edLottoA.Text = "".PadLeft(50, "z"c)
            End If
            Return False
          End If
          If NTSCInt(edDacomme.Text) > NTSCInt(edAcomme.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128595499574408668, "La commessa di partenza non può essere superiore a quella di arrivo."))
            edDacomme.Text = "0"
            edAcomme.Text = "999999999"
            Return False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Controllo intervalli matricole
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "I", "J", "K"
          If NTSCDec(edDamatr.Text) > NTSCDec(edAmatr.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128644033638600222, "La matricola iniziale non può essere superiore a quella finale."))
            edDamatr.Text = "                              "
            edAmatr.Text = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Return False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Controllo intervalli ubicazioni
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "L", "M", "N"
          If NTSCDec(edUbicazini.Text) > NTSCDec(edUbicazfin.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128644033778501252, "La ubicazione iniziale non può essere superiore a quella finale."))
            edUbicazini.Text = "                              "
            edUbicazfin.Text = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Return False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If (cbCodart.SelectedValue = "B") And (NTSCInt(edCodlsar.Text) = 0) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130379632920134028, "Attenzione!" & vbCrLf & _
          "Se si è scelto di filtrare i dati per 'Lista selezionata Articoli'" & vbCrLf & _
          "il codice relativo non può essere a zero."))
        edCodlsar.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (cbConto.SelectedValue = "B") And (NTSCInt(edCodlsel.Text) = 0) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130379633907585178, "Attenzione!" & vbCrLf & _
          "Se si è scelto di filtrare i dati per 'Lista selezionata Clienti/Fornitori'" & vbCrLf & _
          "il codice relativo non può essere a zero."))
        edCodlsel.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      'If FiltriEstesiNonCorretti() = True Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiFormula(ByVal strTab As String) As String
    Dim strC As String = ""
    Dim strTipork As String = ""
    Dim strAggwhereCRM As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
      If (oCleSche.bModuloCRM = True) And (oCleSche.bIsCRMUser = True) Then
        strAggwhereCRM = " And ("
        If oCleSche.strAccvis <> "T" Then
          strAggwhereCRM += "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
          Select Case oCleSche.strAccvis
            Case "P" : strAggwhereCRM += " And {leads.le_opinc} = " & oCleSche.lCodorgaOperat
            Case "C" : strAggwhereCRM += " And {leads.le_opinc} In [" & oCleSche.strRegvis & "]"
          End Select
          strAggwhereCRM += ")"
        Else '--- Tutti i clienti
          strAggwhereCRM += " {anagra.an_tipo} = 'C'"
        End If
        If oCleSche.bAmm = False Then
          strAggwhereCRM += " And {anagra.an_tipo} <> 'F'"
        Else
          strAggwhereCRM += " Or {anagra.an_tipo} <> 'C'"
        End If
        strAggwhereCRM += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbIncludi.SelectedValue) = "A" Then
        strC = "{" & strTab & ".codditt} = '" & DittaCorrente & "'"
        If (NTSCDate(edDatini.Text) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(edDatfin.Text) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {" & strTab & ".km_aammgg} In " & ConvDataRpt(edDatini.Text) & " To " & ConvDataRpt(edDatfin.Text)
        End If
        If (NTSCInt(edDamagaz.Text) <> 0) Or (NTSCInt(edAmagaz.Text) <> 9999) Then
          strC += " And {" & strTab & ".km_magaz} In " & NTSCInt(edDamagaz.Text) & " To " & NTSCInt(edAmagaz.Text)
        End If
        If (NTSCInt(edDacomme.Text) <> 0) Or (NTSCInt(edAcomme.Text) <> 999999999) Then
          strC += " And {" & strTab & ".km_commeca} In " & NTSCInt(edDacomme.Text) & " To " & NTSCInt(edAcomme.Text)
        End If
        If oCleSche.bLottoNew = False Then
          If (edLottoDa.Text.Trim <> "") Or (edLottoA.Text.Trim <> "".PadLeft(9, "9"c)) Then
            strC += " And {" & strTab & ".km_lotto} In " & NTSCInt(edLottoDa.Text) & " To " & NTSCInt(edLottoA.Text)
          End If
        Else
          If (edLottoDa.Text.Trim <> "") Or (edLottoA.Text.Trim <> "".PadLeft(50, "z"c)) Then
            strC += " And {ANALOTTI.alo_lottox} In " & ConvStrRpt(edLottoDa.Text) & " To " & ConvStrRpt(edLottoA.Text)
          End If
        End If
        If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
          strTipork = UCase(NTSCStr(cbTipodoc.SelectedValue))
          strC += " and {" & strTab & ".km_tipork} = '" & strTipork & "'"
          If ckSerie.Checked = True Then
            If edSerie.Text.Length > 0 Then strC += " and {" & strTab & ".km_serie} = '" & edSerie.Text & "'"
          End If
        End If
        If ckRigheInevase.Checked = True Then
          strC += " and {" & NTSCStr(IIf(strTab = "KEYPRB", "MOVPRB", "MOVMAG")) & ".mm_nprflevas} = 'C'"
        End If
        If NTSCInt(edCausale.Text) <> 0 Then strC += " and {" & strTab & ".km_causale} = " & NTSCInt(edCausale.Text)
        '------------------------------------------------------------------------------------------------------------
        Select Case cbCodart.SelectedValue
          Case "A"
            If (edDacodart.Text <> "".PadLeft(18)) Or (edAcodart.Text <> "".PadLeft(18, "z"c)) Then
              strC += " And {" & strTab & ".km_codart} In '" & edDacodart.Text & "' To '" & edAcodart.Text & "'"
            End If
            If (NTSCInt(edFaseini.Text) <> 0) Or (NTSCInt(edFasefin.Text) <> 9999) Then
              strC += " And {" & strTab & ".km_fase} In " & NTSCInt(edFaseini.Text) & " To " & NTSCInt(edFasefin.Text)
            End If
          Case "B"
            If NTSCInt(edCodlsar.Text) > 0 Then
              If oCleSche.RitornaLISTSAR(NTSCInt(edCodlsar.Text), dttTmp) = True Then
                strC += " And {" & strTab & ".km_codart} + '.' + ToText({" & strTab & ".km_fase}, 0, '') In ["
                For i As Integer = 0 To (dttTmp.Rows.Count - 1)
                  If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                    strC += "'" & NTSCStr(dttTmp.Rows(i)!lsa_codart) & "." & NTSCStr(dttTmp.Rows(i)!lsa_fase) & "'" & _
                      IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  Else
                    strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                      " Or {" & strTab & ".km_codart} + '.' + ToText({" & strTab & ".km_fase}, 0, '') In [" & _
                      "'" & NTSCStr(dttTmp.Rows(i)!lsa_codart) & "." & NTSCStr(dttTmp.Rows(i)!lsa_fase) & "'" & _
                      IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  End If
                Next
              End If
            End If
        End Select
        Select Case cbConto.SelectedValue
          Case "A"
            If (NTSCInt(edDaconto.Text) <> 0) Or (NTSCInt(edAconto.Text) <> 999999999) Then
              strC += " And {" & strTab & ".km_conto} In " & NTSCInt(edDaconto.Text) & " To " & NTSCInt(edAconto.Text)
            End If
          Case "B"
            If NTSCInt(edCodlsel.Text) > 0 Then
              If oCleSche.RitornaLISTSEL(NTSCInt(edCodlsel.Text), dttTmp) = True Then
                strC += " And {" & strTab & ".km_conto} In ["
                For i As Integer = 0 To (dttTmp.Rows.Count - 1)
                  If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                    strC += NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  Else
                    strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                      " Or {" & strTab & ".km_conto} In [" & _
                      NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
                  End If
                Next
              End If
            End If
        End Select
        '------------------------------------------------------------------------------------------------------------
        strC += " and "
      End If '--- If NTSCStr(cbIncludi.SelectedValue) = "A" Then
      '--------------------------------------------------------------------------------------------------------------
      strC += " {ARTICO.ar_stasche} = 'S'"
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edGruppo.Text) <> 0 Then strC += " and {ARTICO.ar_gruppo} = " & NTSCInt(edGruppo.Text)
      If NTSCInt(edSottogr.Text) <> 0 Then strC += " and {ARTICO.ar_sotgru} = " & NTSCInt(edSottogr.Text)
      If edClassificazioneLivello1.Text.Trim <> "" Then strC += " AND {artico.ar_codcla1} = '" & edClassificazioneLivello1.Text & "'"
      If edClassificazioneLivello2.Text.Trim <> "" Then strC += " AND {artico.ar_codcla2} = '" & edClassificazioneLivello2.Text & "'"
      If edClassificazioneLivello3.Text.Trim <> "" Then strC += " AND {artico.ar_codcla3} = '" & edClassificazioneLivello3.Text & "'"
      If edClassificazioneLivello4.Text.Trim <> "" Then strC += " AND {artico.ar_codcla4} = '" & edClassificazioneLivello4.Text & "'"
      If edClassificazioneLivello5.Text.Trim <> "" Then strC += " AND {artico.ar_codcla5} = '" & edClassificazioneLivello5.Text & "'"
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "I", "J", "K"
          strC += " and {MOVMATR.mma_matric} in " & ConvStrRpt(edDamatr.Text) & " to " & ConvStrRpt(edAmatr.Text)
      End Select
      If (NTSCInt(edCodmarcini.Text) <> 0) Or (NTSCInt(edCodmarcfin.Text) <> 999) Then
        strC += " And {ARTICO.ar_codmarc} in " & NTSCInt(edCodmarcini.Text) & " To " & NTSCInt(edCodmarcfin.Text)
      End If
      If edCodcfam.Text.Trim <> "" Then strC += " And {artico.ar_famprod} = '" & edCodcfam.Text & "'"
      If oCleSche.bModTCO = True Then
        If ckSelAnnoStag.Checked = True Then
          If NTSCInt(edAnnotco.Text) <> 0 Then strC += " AND {artico.ar_anno} = " & NTSCInt(edAnnotco.Text)
          If NTSCInt(edCodstag.Text) <> 0 Then strC += " AND {artico.ar_codstag} = " & NTSCInt(edCodstag.Text)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      strC += strAggwhereCRM
      '--------------------------------------------------------------------------------------------------------------
      Dim strFiltriAgg As String = ceFiltriExt.GeneraQueryReport()
      If strFiltriAgg <> "" Then
        If strTab = "KEYPRB" Then
          strC += " AND " & strFiltriAgg.Replace("movmag", "movprb").Replace("testmag", "testprb")
        Else
          strC += " AND " & ceFiltriExt.GeneraQueryReport()
        End If
      End If

      If strTab = "KEYMAG" Then
        If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then strC += " and {TTSTSCHEA.instid} = " & oCleSche.lIITTStschea
      End If

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
            lbStatus.Refresh() 'Me.Refresh ridisegna tutto il programma. Le prestazioni sono decisamente peggiori.
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ReplaceSQLApice(ByVal strIn As String) As String
    Try
      Dim i As Integer
      i = InStr(1, strIn, "'")
      While i > 0
        strIn = Microsoft.VisualBasic.Left(strIn, i - 1) + "' + ""'"" + '" + Mid(strIn, i + 1)
        i = i + 11
        i = InStr(i, strIn, "'")
      End While

      ReplaceSQLApice = strIn

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub ReportSuGriglia()
    Dim oParam As New CLE__CLDP
    Dim frmGrsc As FRMMGGRSC = Nothing
    Dim frmGrlo As FRMMGGRLO = Nothing
    Dim frmGrma As FRMMGGRMA = Nothing
    Dim frmGrnp As FRMMGGRNP = Nothing
    Dim oPar As New CLE__PATB
    Dim strParam As String = ""
    Dim strSTMSerie As String = ""
    Dim strSTMTipork As String = ""
    Dim strTipoMatr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (NTSCStr(cbIncludi.SelectedValue) = "C" Or NTSCStr(cbIncludi.SelectedValue) = "B") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644786523647457, "Stampa su gliglia non diponibile con le scelte che includono in stampa 'Tutti gli articoli' o 'Articoli in esistenza alla data inizio'."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If Not SvuotaTmpTable() Then Return
      If Not CheckIntervalli() Then Return
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 129080484846821870, "Selezione dati in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      '--- Avverimento
      '--------------------------------------------------------------------------------------------------------------
      Avvertimento()
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'eventuale presenza di campi di tipo 'Memo', nel tab relativo a 'Altri filtri corpo'
      '--- impedendone la stampa
      '--------------------------------------------------------------------------------------------------------------
      If CampiMemoInSelezione() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.strScarDalotto = edLottoDa.Text
      oCleSche.strScarAlotto = edLottoA.Text
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "I", "J", "K" '--- Valido solo nel caso di gestione a matricole
          '----------------------------------------------------------------------------------------------------------
          strParam = "BNMGSCHE;"
          '----------------------------------------------------------------------------------------------------------
          Select Case NTSCStr(cbTipodoc.SelectedValue)
            Case "1", "W" : strSTMTipork = ""
            Case Else
              strSTMTipork = NTSCStr(cbTipodoc.SelectedValue()).ToUpper
              If ckSerie.Checked = True Then
                If edSerie.Text.Length > 0 Then strSTMSerie = edSerie.Text
              Else
                strSTMSerie = ""
              End If
          End Select
          '----------------------------------------------------------------------------------------------------------
          strParam += strSTMTipork & ";"
          strParam += strSTMSerie & ";"
          strParam += IntSetDate("01/01/1900") & ";" ' edDatini ? sempre dall'inizio ....
          strParam += edDatfin.Text & ";"
          strParam += edDamagaz.Text & ";"
          strParam += edAmagaz.Text & ";"
          strParam += edDacodart.Text & ";"
          strParam += edAcodart.Text & ";"
          strParam += edDaconto.Text & ";"
          strParam += edAconto.Text & ";"
          strParam += ReplaceSQLApice(edDamatr.Text) & ";"
          strParam += ReplaceSQLApice(edAmatr.Text) & ";"
          strParam += edCausale.Text & ";"
          strParam += edGruppo.Text & ";"
          strParam += edSottogr.Text & ";"
          If NTSCStr(cbTipoStampa.SelectedValue) = "I" Then
            strTipoMatr = "0"
          Else
            If NTSCStr(cbTipoStampa.SelectedValue) = "J" Then strTipoMatr = "1" Else strTipoMatr = "2"
          End If
          strParam += strTipoMatr & ";"
          strParam += oCleSche.strTTStMatr & ";"
          strParam += oCleSche.strTTStMats & ";"
          strParam += edFaseini.Text & ";"
          strParam += edFasefin.Text & ";"
          strParam += edCodmarcini.Text & ";"
          strParam += edCodmarcfin.Text & ";"
          strParam += edCodcfam.Text.Trim & ";"
          strParam += edClassificazioneLivello1.Text & ";"
          strParam += edClassificazioneLivello2.Text & ";"
          strParam += edClassificazioneLivello3.Text & ";"
          strParam += edClassificazioneLivello4.Text & ";"
          strParam += edClassificazioneLivello5.Text & ";"
          strParam += cbCodart.SelectedValue & ";"
          strParam += edCodlsar.Text & ";"
          strParam += cbConto.SelectedValue & ";"
          strParam += edCodlsel.Text & ";"
          '----------------------------------------------------------------------------------------------------------
          oPar.strTipo = strParam
          oPar.strOut = ceFiltriExt.GeneraQuerySQL()
          NTSZOOM.ZoomStrIn("ZOOMSCHEDEARTMATR", DittaCorrente, oPar)
          '----------------------------------------------------------------------------------------------------------
          Return
        Case "C", "D", "F", "G" : If RiempiTTStloco(False) = False Then Return
        Case "E", "H", "N" : If RiempiTTStlocs() = False Then Return
        Case "L", "M" : If RiempiTTStloco(True) = False Then Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If ckStorico.Checked = True Then oCleSche.bScarStorico = True Else oCleSche.bScarStorico = False
      oCleSche.strScarDatini = edDatini.Text
      oCleSche.strScarDatfin = edDatfin.Text
      oCleSche.nScarDamagaz = NTSCInt(edDamagaz.Text)
      oCleSche.nScarAmagaz = NTSCInt(edAmagaz.Text)
      oCleSche.strScarCodart = cbCodart.SelectedValue
      oCleSche.strScarDacodart = edDacodart.Text
      oCleSche.strScarAcodart = edAcodart.Text
      oCleSche.nScarCodlsar = NTSCInt(edCodlsar.Text)
      oCleSche.strScarConto = cbConto.SelectedValue
      oCleSche.lScarDaconto = NTSCInt(edDaconto.Text)
      oCleSche.lScarAconto = NTSCInt(edAconto.Text)
      oCleSche.nScarCodlsel = NTSCInt(edCodlsel.Text)
      oCleSche.nScarCodmarcini = NTSCInt(edCodmarcini.Text)
      oCleSche.nScarCodmarcfin = NTSCInt(edCodmarcfin.Text)
      oCleSche.nScarFaseini = NTSCInt(edFaseini.Text)
      oCleSche.nScarFasefin = NTSCInt(edFasefin.Text)
      oCleSche.lScarDacomme = NTSCInt(edDacomme.Text)
      oCleSche.lScarAcomme = NTSCInt(edAcomme.Text)
      oCleSche.bRigheInevase = ckRigheInevase.Checked
      If (NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        oCleSche.strScarTipodoc = UCase(NTSCStr(cbTipodoc.SelectedValue))
      Else
        oCleSche.strScarTipodoc = ""
      End If
      oCleSche.nScarCausale = NTSCInt(edCausale.Text)
      oCleSche.nScarGruppo = NTSCInt(edGruppo.Text)
      oCleSche.nScarSottogr = NTSCInt(edSottogr.Text)
      oCleSche.strScarClassLivello1 = edClassificazioneLivello1.Text
      oCleSche.strScarClassLivello2 = edClassificazioneLivello2.Text
      oCleSche.strScarClassLivello3 = edClassificazioneLivello3.Text
      oCleSche.strScarClassLivello4 = edClassificazioneLivello4.Text
      oCleSche.strScarClassLivello5 = edClassificazioneLivello5.Text
      oCleSche.strScarSerie = IIf(ckSerie.Checked = True, edSerie.Text, "").ToString
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A" : oCleSche.strScarOrdin = "A"
        Case "B" : oCleSche.strScarOrdin = "C"
        Case "C" : oCleSche.nScarOrdin = 3
        Case "D" : oCleSche.nScarOrdin = 4
        Case "E" : oCleSche.nScarOrdin = 5
        Case "F" : oCleSche.nScarOrdin = 6
        Case "G" : oCleSche.nScarOrdin = 7
        Case "H" : oCleSche.nScarOrdin = 8
        Case "L" : oCleSche.nScarOrdin = 12
        Case "M" : oCleSche.nScarOrdin = 13
        Case "N" : oCleSche.nScarOrdin = 14
      End Select
      If NTSCStr(cbTipodoc.SelectedValue) <> "W" Then
        If (NTSCStr(cbTipoStampa.SelectedValue) = "A") Or (NTSCStr(cbTipoStampa.SelectedValue) = "B") Then
          oCleSche.bScarDaveboll = False
          If (ckStorico.Checked = True) And _
             (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
             (ckSaldiIniziali.Checked = False) Then
            oCleSche.bGrscSaldiIniziali = False
          Else
            oCleSche.bGrscSaldiIniziali = True
          End If
          oCleSche.strGrscCodcfam = Trim(edCodcfam.Text)
          If oCleSche.bModTCO = True Then
            If ckSelAnnoStag.Checked = True Then
              oCleSche.nGrscAnnotco = NTSCInt(edAnnotco.Text)
              oCleSche.nGrscCodstag = NTSCInt(edCodstag.Text)
            Else
              oCleSche.nGrscAnnotco = 0
              oCleSche.nGrscCodstag = 0
            End If
          Else
            oCleSche.nGrscAnnotco = 0
            oCleSche.nGrscCodstag = 0
          End If
          oCleSche.strAltriFiltri = ceFiltriExt.GeneraQuerySQL()
          frmGrsc = CType(NTSNewFormModal("FRMMGGRSC"), FRMMGGRSC)
          frmGrsc.Init(oMenu, oParam, DittaCorrente)
          frmGrsc.InitEntity(oCleSche)
          If Not oCallParams Is Nothing Then
            frmGrsc.bNoModal = False
          Else
            frmGrsc.bNoModal = tlbNoModal.Checked
          End If
          If frmGrsc.bNoModal Then
            frmGrsc.Show()
          Else
            frmGrsc.ShowDialog()
          End If
        Else
          frmGrlo = CType(NTSNewFormModal("FRMMGGRLO"), FRMMGGRLO)
          frmGrlo.Init(oMenu, oParam, DittaCorrente)
          frmGrlo.InitEntity(oCleSche)
          If Not oCallParams Is Nothing Then
            frmGrlo.bNoModal = False
          Else
            frmGrlo.bNoModal = tlbNoModal.Checked
          End If
          If frmGrlo.bNoModal Then
            frmGrlo.Show()
          Else
            frmGrlo.ShowDialog()
          End If
        End If
      Else
        If ckPermatricole.Checked = True Then 'sempre invisibile defleggato se non abilitato da gest sicurezza
          oCleSche.strGrmaCodcfam = Trim(edCodcfam.Text)
          If oCleSche.bModTCO = True Then
            If ckSelAnnoStag.Checked = True Then
              oCleSche.nGrmaAnnotco = NTSCInt(edAnnotco.Text)
              oCleSche.nGrmaCodstag = NTSCInt(edCodstag.Text)
            Else
              oCleSche.nGrmaAnnotco = 0
              oCleSche.nGrmaCodstag = 0
            End If
          Else
            oCleSche.nGrmaAnnotco = 0
            oCleSche.nGrmaCodstag = 0
          End If
          oCleSche.strAltriFiltri = ceFiltriExt.GeneraQuerySQL()
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") Or (NTSCStr(cbTipoStampa.SelectedValue) = "B") Then
            oCleSche.bScarDaveboll = False
            oCleSche.strGrmaDamatric = NTSCStr(edDamatr.Text)
            oCleSche.strGrmaAmatric = NTSCStr(edAmatr.Text)
            frmGrma = CType(NTSNewFormModal("FRMMGGRMA"), FRMMGGRMA)
            frmGrma.Init(oMenu, oParam, DittaCorrente)
            frmGrma.InitEntity(oCleSche)
            If Not oCallParams Is Nothing Then
              frmGrma.bNoModal = False
            Else
              frmGrma.bNoModal = tlbNoModal.Checked
            End If
            If frmGrma.bNoModal Then
              frmGrma.Show()
            Else
              frmGrma.ShowDialog()
            End If
          Else
            oCleSche.strGrmaDamatric = NTSCStr(edDamatr.Text)
            oCleSche.strGrmaAmatric = NTSCStr(edAmatr.Text)
            frmGrma = CType(NTSNewFormModal("FRMMGGRMA"), FRMMGGRMA)
            frmGrma.Init(oMenu, oParam, DittaCorrente)
            frmGrma.InitEntity(oCleSche)
            If Not oCallParams Is Nothing Then
              frmGrma.bNoModal = False
            Else
              frmGrma.bNoModal = tlbNoModal.Checked
            End If
            If frmGrma.bNoModal Then
              frmGrma.Show()
            Else
              frmGrma.ShowDialog()
            End If
          End If
        Else
          oCleSche.strGrnpCodcfam = Trim(edCodcfam.Text)
          If oCleSche.bModTCO = True Then
            If ckSelAnnoStag.Checked = True Then
              oCleSche.nGrnpAnnotco = NTSCInt(edAnnotco.Text)
              oCleSche.nGrnpCodstag = NTSCInt(edCodstag.Text)
            Else
              oCleSche.nGrnpAnnotco = 0
              oCleSche.nGrnpCodstag = 0
            End If
          Else
            oCleSche.nGrnpAnnotco = 0
            oCleSche.nGrnpCodstag = 0
          End If
          oCleSche.strAltriFiltri = ceFiltriExt.GeneraQuerySQL()
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") Or (NTSCStr(cbTipoStampa.SelectedValue) = "B") Then
            oCleSche.bScarDaveboll = False
            frmGrnp = CType(NTSNewFormModal("FRMMGGRNP"), FRMMGGRNP)
            frmGrnp.Init(oMenu, oParam, DittaCorrente)
            frmGrnp.InitEntity(oCleSche)
            If Not oCallParams Is Nothing Then
              frmGrnp.bNoModal = False
            Else
              frmGrnp.bNoModal = tlbNoModal.Checked
            End If
            If frmGrnp.bNoModal Then
              frmGrnp.Show()
            Else
              frmGrnp.ShowDialog()
            End If
          Else
            frmGrnp = CType(NTSNewFormModal("FRMMGGRNP"), FRMMGGRNP)
            frmGrnp.Init(oMenu, oParam, DittaCorrente)
            frmGrnp.InitEntity(oCleSche)
            If Not oCallParams Is Nothing Then
              frmGrnp.bNoModal = False
            Else
              frmGrnp.bNoModal = tlbNoModal.Checked
            End If
            If frmGrnp.bNoModal Then
              frmGrnp.Show()
            Else
              frmGrnp.ShowDialog()
            End If
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleSche.bScheGestDaCons = True Then
        Me.Visible = True
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrsc Is Nothing Then
        If frmGrsc.bNoModal = False Then
          frmGrsc.Dispose()
          frmGrsc = Nothing
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrlo Is Nothing Then
        If frmGrlo.bNoModal = False Then
          frmGrlo.Dispose()
          frmGrlo = Nothing
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrma Is Nothing Then
        If frmGrma.bNoModal = False Then
          frmGrma.Dispose()
          frmGrma = Nothing
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrnp Is Nothing Then
        If frmGrnp.bNoModal = False Then
          frmGrnp.Dispose()
          frmGrnp = Nothing
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ScelteOptionButton()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipodoc.SelectedValue) = "1") And (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (ckStorico.Checked = True) Then
        GctlSetVisEnab(ckSaldiIniziali, False)
      Else
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") Or (NTSCStr(cbTipoStampa.SelectedValue) = "B") Then
        If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
          GctlSetVisEnab(ckSalto, False)
          cbIncludi.SelectedValue = "A"
          GctlSetVisEnab(cbIncludi, False)
        Else
          ckSalto.Enabled = False
          cbIncludi.SelectedValue = "A"
          cbIncludi.Enabled = False
        End If
        GctlSetVisEnab(ckStorico, False)
        edDamatr.Text = ""
        edDamatr.Enabled = False
        edAmatr.Text = ""
        edAmatr.Enabled = False
        edUbicazini.Text = ""
        edUbicazini.Enabled = False
        edUbicazfin.Text = ""
        edUbicazfin.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "I", "J", "K"
          ckSalto.Checked = False
          ckSalto.Enabled = False
          ckStorico.Checked = False
          edDamatr.Text = "".PadLeft(30, " "c)
          GctlSetVisEnab(edDamatr, False)
          edAmatr.Text = "".PadLeft(30, "z"c)
          GctlSetVisEnab(edAmatr, False)
          edUbicazini.Text = ""
          edUbicazini.Enabled = False
          edUbicazfin.Text = ""
          edUbicazfin.Enabled = False
          cbIncludi.SelectedValue = "A"
          cbIncludi.Enabled = False
        Case "L", "M", "N"
          ckSalto.Checked = False
          ckSalto.Enabled = False
          ckStorico.Checked = False
          GctlSetVisEnab(edUbicazini, False)
          GctlSetVisEnab(edUbicazfin, False)
          edUbicazfin.Text = "".PadLeft(30, "z"c)
          edUbicazini.Text = "".PadLeft(30, " "c)
          edDamatr.Text = ""
          edDamatr.Enabled = False
          edAmatr.Text = ""
          edAmatr.Enabled = False
          cbIncludi.SelectedValue = "A"
          cbIncludi.Enabled = False
        Case "C", "D", "E", "F", "G", "H"
          ckSalto.Checked = False
          ckSalto.Enabled = False
          ckStorico.Checked = False
          edUbicazini.Text = ""
          edUbicazini.Enabled = False
          edUbicazfin.Text = ""
          edUbicazfin.Enabled = False
          edDamatr.Text = ""
          edDamatr.Enabled = False
          edAmatr.Text = ""
          edAmatr.Enabled = False
          cbIncludi.SelectedValue = "A"
          cbIncludi.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A", "B", "F", "G", "H"
          GctlSetVisEnab(edDacomme, False)
          GctlSetVisEnab(edAcomme, False)
        Case Else
          edDacomme.Text = "0"
          edAcomme.Text = "999999999"
          edDacomme.Enabled = False
          edAcomme.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A", "B", "C", "D", "E"
          GctlSetVisEnab(edLottoDa, False)
          GctlSetVisEnab(edLottoA, False)
        Case Else
          edLottoDa.Text = ""
          If oCleSche.bLottoNew = False Then
            edLottoA.Text = "".PadLeft(9, "9"c)
          Else
            edLottoA.Text = "".PadLeft(50, "z"c)
          End If
          edLottoDa.Enabled = False
          edLottoA.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A"
          GctlSetVisEnab(ckStampaFiltri, False)
        Case Else
          ckStampaFiltri.Checked = False
          ckStampaFiltri.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub SettaTipoStampa(ByVal bNoRidotto As Boolean)
    Try
      If bNoRidotto = True Then
        If (opTipoStampa0.Enabled = True) Or (opTipoStampa1.Enabled = True) Then
          opTipoStampa1.Checked = True
          opTipoStampa0.Enabled = False
          opTipoStampa1.Enabled = False
        End If
      Else
        If (opTipoStampa0.Enabled = False) Or (opTipoStampa1.Enabled = False) Then
          GctlSetVisEnab(opTipoStampa0, False)
          GctlSetVisEnab(opTipoStampa1, False)
          opTipoStampa0.Checked = True
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SoloNonMovimentati() As Boolean
    Dim bTTSTSCHEA As Boolean = False
    Dim bTTSTMATR As Boolean = False
    Dim bTTSTLOCO As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il CheckBox "Solo non movimentati dal", NON è spuntato, esce
      '--------------------------------------------------------------------------------------------------------------
      If ckNonMovimentati.Checked = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A" : bTTSTSCHEA = True
        Case "I", "J", "K" : bTTSTMATR = True
        Case "C", "D", "F", "G", "E", "H", "N", "L", "M" : bTTSTLOCO = True
      End Select
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 130289166451632195, _
        "Selezione dei soli articoli non movimentati al |" & edNonMovimentati.Text & "| in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      Return oCleSche.SoloNonMovimentati(CBool(IIf(cbTipodoc.SelectedValue = "W", True, False)), _
        edNonMovimentati.Text, ckPerMagazzino.Checked, bTTSTSCHEA, bTTSTMATR, bTTSTLOCO)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strNomRpt As String = ""
    Dim strKey As String = ""
    Dim strKey1 As String = ""
    Dim strData As String = ""
    Dim strDataIn As String = ""
    Dim strDataFin As String = ""
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
    Dim strSTMTipork As String
    Dim strError As String = ""
    Dim strTmp As String = ""
    Dim strLottoAlfanumerico As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckStampaFiltri.Enabled = True Then
        oMenu.SaveSettingBusDitt(DittaCorrente, "BSMGSCHE", "RECENT", ".", "StampaFiltriInReport", IIf(ckStampaFiltri.Checked = True, "-1", "0").ToString, " ", ".N.", ".N.", ".N.")
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipodoc.SelectedValue) = "W" Then
        If ckPermatricole.Checked = True Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128644089072390951, "Attenzione!" & vbCrLf & _
            "Funzionalità non abilitata nel caso di Stampa/Visualizzazione schede articoli da Note di prelievo per matricole."))
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If (ckNonMovimentati.Checked = True) And (edNonMovimentati.Text.Trim = "") Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130289131090065466, "Attenzione!" & vbCrLf & _
          "Se selezionata la stampa dei soli articoli non movimentati, deve essere indicata una data valida."))
        edNonMovimentati.Text = Now.ToShortDateString
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not SvuotaTmpTable() Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckIntervalli() Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Avverimento
      '--------------------------------------------------------------------------------------------------------------
      Avvertimento()
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'eventuale presenza di campi di tipo 'Memo', nel tab relativo a 'Altri filtri corpo'
      '--- impedendone la stampa
      '--------------------------------------------------------------------------------------------------------------
      If CampiMemoInSelezione() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Valido solo nel caso di gestione a matricole
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "I", "J", "K"
          '----------------------------------------------------------------------------------------------------------
          Select Case NTSCStr(cbTipodoc.SelectedValue)
            Case "1", "W" : strSTMTipork = ""
            Case Else : strSTMTipork = UCase(NTSCStr(cbTipodoc.SelectedValue))
          End Select
          '----------------------------------------------------------------------------------------------------------
          '--- Riempie le tabelle temporanee relative alle matricole
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(cbTipoStampa.SelectedValue) = "I" Then
            If oCleSche.RiempiTTStMatr(ReplaceSQLApice(edDamatr.Text), ReplaceSQLApice(edAmatr.Text), _
                 IntSetDate("01/01/1900"), edDatfin.Text, NTSCInt(edDamagaz.Text), NTSCInt(edAmagaz.Text), _
                 edDacodart.Text, edAcodart.Text, NTSCInt(edFaseini.Text), NTSCInt(edFasefin.Text), _
                 NTSCInt(edDaconto.Text), NTSCInt(edAconto.Text), strSTMTipork, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                 NTSCInt(edCausale.Text), NTSCInt(edGruppo.Text), NTSCInt(edSottogr.Text), _
                 NTSCInt(edCodmarcini.Text), NTSCInt(edCodmarcfin.Text), Trim(edCodcfam.Text), 0, strError, _
                 ceFiltriExt.GeneraQuerySQL(), _
                 edClassificazioneLivello1.Text, _
                 edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                 edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                 cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) = False Then
              If strError <> "" Then oApp.MsgBoxErr(strError)
              Return
            End If
          End If
          If NTSCStr(cbTipoStampa.SelectedValue) = "J" Then
            If oCleSche.RiempiTTStMatr(ReplaceSQLApice(edDamatr.Text), ReplaceSQLApice(edAmatr.Text), _
                 IntSetDate("01/01/1900"), edDatfin.Text, NTSCInt(edDamagaz.Text), NTSCInt(edAmagaz.Text), _
                 edDacodart.Text, edAcodart.Text, NTSCInt(edFaseini.Text), NTSCInt(edFasefin.Text), _
                 NTSCInt(edDaconto.Text), NTSCInt(edAconto.Text), strSTMTipork, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                 NTSCInt(edCausale.Text), NTSCInt(edGruppo.Text), NTSCInt(edSottogr.Text), _
                 NTSCInt(edCodmarcini.Text), NTSCInt(edCodmarcfin.Text), Trim(edCodcfam.Text), 1, strError, _
                 ceFiltriExt.GeneraQuerySQL(), _
                 edClassificazioneLivello1.Text, _
                 edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                 edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                 cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) = False Then
              If strError <> "" Then oApp.MsgBoxErr(strError)
              Return
            End If
          End If
          If NTSCStr(cbTipoStampa.SelectedValue) = "K" Then
            If oCleSche.RiempiTTStMats(ReplaceSQLApice(edDamatr.Text), ReplaceSQLApice(edAmatr.Text), _
                 IntSetDate("01/01/1900"), edDatfin.Text, NTSCInt(edDamagaz.Text), NTSCInt(edAmagaz.Text), _
                 edDacodart.Text, edAcodart.Text, NTSCInt(edFaseini.Text), NTSCInt(edFasefin.Text), _
                 NTSCInt(edDaconto.Text), NTSCInt(edAconto.Text), strSTMTipork, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                 NTSCInt(edCausale.Text), NTSCInt(edGruppo.Text), NTSCInt(edSottogr.Text), _
                 NTSCInt(edCodmarcini.Text), NTSCInt(edCodmarcfin.Text), Trim(edCodcfam.Text), 2, strError, _
                 ceFiltriExt.GeneraQuerySQL(), _
                 edClassificazioneLivello1.Text, _
                 edClassificazioneLivello2.Text, edClassificazioneLivello3.Text, _
                 edClassificazioneLivello4.Text, edClassificazioneLivello5.Text, _
                 cbCodart.SelectedValue, NTSCInt(edCodlsar.Text), cbConto.SelectedValue, NTSCInt(edCodlsel.Text)) = False Then
              If strError <> "" Then oApp.MsgBoxErr(strError)
              Return
            End If
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.strScarDalotto = edLottoDa.Text
      oCleSche.strScarAlotto = edLottoA.Text
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie le tabelle temporanee se serve
      '--------------------------------------------------------------------------------------------------------------      
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "C", "D", "F", "G" : If RiempiTTStloco(False) = False Then Return
        Case "E", "H", "N" : If RiempiTTStlocs() = False Then Return
        Case "L", "M" : If RiempiTTStloco(True) = False Then Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") Then
        If NTSCStr(cbIncludi.SelectedValue) = "A" Then
          If Not SaldoStorico() Then Return
        Else
          If Not SaldoStoricoNonMov() Then Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If SoloNonMovimentati() = False Then
        Me.Cursor = Cursors.Default
        lbStatus.Text = oApp.Tr(Me, 130289165867646135, "Pronto.")
        Me.Refresh()
        oApp.MsgBoxInfo(oApp.Tr(Me, 130289165369465130, "Attenzione!" & vbCrLf & _
          "Non esistono dati da stampare con le caratteristiche richieste."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 129080484660564718, "Stampa in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      '--- Imposta i nomi dei report e delle chiavi di registro...
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipodoc.SelectedValue) = "W" Then
        If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then strNomRpt = "BSMGSCH0.RPT" : strKey1 = "Reports10"
        If NTSCStr(cbTipoStampa.SelectedValue) = "B" Then strNomRpt = "BSMGSCH5.RPT" : strKey1 = "Reports5"
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
        GoTo Salta
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A"
          If opTipoStampa0.Checked = True Then
            If NTSCStr(cbIncludi.SelectedValue) = "A" Then
              strNomRpt = "BSMGSCH11.RPT" : strKey1 = "Reports11"
            Else
              strNomRpt = "BSMGSCH13.RPT" : strKey1 = "Reports13"
            End If
          Else
            If NTSCStr(cbIncludi.SelectedValue) = "A" Then
              strNomRpt = "BSMGSCHE.RPT" : strKey1 = "Reports1"
            Else
              strNomRpt = "BSMGSCH14.RPT" : strKey1 = "Reports14"
            End If
          End If
        Case "B"
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSMGSCH12.RPT" : strKey1 = "Reports12"
          Else
            strNomRpt = "BSMGSCH1.RPT" : strKey1 = "Reports2"
          End If
        Case "C", "D", "F", "G"
          strNomRpt = "BSMGSCH2.RPT" : strKey1 = "Reports3"
        Case "E", "H"
          strNomRpt = "BSMGSCH3.RPT" : strKey1 = "Reports4"
        Case "I", "J"
          strNomRpt = "BSMGSCH7.RPT" : strKey1 = "Reports6"
        Case "K"
          strNomRpt = "BSMGSCH8.RPT" : strKey1 = "Reports7"
        Case "L", "M"
          strNomRpt = "BSMGSCH9.RPT" : strKey1 = "Reports8"
        Case "N"
          strNomRpt = "BSMGSCH10.RPT" : strKey1 = "Reports9"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      strData = "'Dal " & edDatini.Text & " al " & edDatfin.Text & "'"
      strDataIn = "'" & (edDatini.Text) & "'"
      strDataFin = "'" & edDatfin.Text & "'"
      '--------------------------------------------------------------------------------------------------------------
      '--- Si comporta diversamente se per articolo oppure no...
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipoStampa.SelectedValue) <> "A" Then
        If NTSCStr(cbTipoStampa.SelectedValue) <> "B" Then
          Select Case NTSCStr(cbTipoStampa.SelectedValue)
            Case "C", "D", "F", "G"
              nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
            Case Else '--- Saldo lotti aperti
              nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
          End Select
        Else ' per conto
          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
        End If
      Else ' per articolo
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
      End If
Salta:
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A", "B"
          Select Case NTSCStr(cbTipodoc.SelectedValue)
            Case "W" : strCrpe = ComponiFormula("KEYPRB")
            Case Else
              strCrpe = ComponiFormula("KEYMAG")
          End Select
        Case Else
          strCrpe = ComponiFormula2()
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Lancio tutti gli eventuali reports (gestisce già il multireport)
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To UBound(CType(nPjob, Array), 2)
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        '------------------------------------------------------------------------------------------------------------
        If i = 1 Then
          '----------------------------------------------------------------------------------------------------------
          '--- Il primo report (quello standard)
          '----------------------------------------------------------------------------------------------------------
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckSalto.Checked = True) Then
            sectionCode = oMenu.PE_GROUPHEADER
            nRis = oMenu.PEGetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
            nNewPageBefore = 1
            nRis = oMenu.PESetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATA", strData)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAIN", strDataIn)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAFIN", strDataFin)
        '------------------------------------------------------------------------------------------------------------
        '--- Passa alcune formule per i filtri sui report
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(cbTipoStampa.SelectedValue) = "A" And (NTSCStr(cbTipodoc.SelectedValue) <> "W") And (NTSCStr(cbIncludi.SelectedValue) = "B" Or NTSCStr(cbIncludi.SelectedValue) = "C") Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDATAIN", ConvDataRpt(edDatini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDATAFIN", ConvDataRpt(edDatfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDACONTO", edDaconto.Text)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTACONTO", edAconto.Text)
          If cbIncludi.SelectedValue <> "A" Then
            'solo per bsmgsch13.rpt e bsmgsch14.rpt
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDALOTTO", ConvStrRpt(edLottoDa.Text))
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTALOTTO", ConvStrRpt(edLottoA.Text))
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDACOMME", edDacomme.Text)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTACOMME", edAcomme.Text)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTCAUM", NTSCStr(IIf(NTSCInt(edCausale.Text) > 0, edCausale.Text, "-1")))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTTIPO", NTSCStr(IIf((NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W"), ConvStrRpt(UCase(NTSCStr(cbTipodoc.SelectedValue))), "'ALL'")))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTSERIE", NTSCStr(IIf((NTSCStr(cbTipodoc.SelectedValue) <> "1") And (NTSCStr(cbTipodoc.SelectedValue) <> "W") And ckSerie.Checked = True And Len(edSerie.Text) > 0, Trim(edSerie.Text), "'ALL'")))
        End If
        '------------------------------------------------------------------------------------------------------------
        If ckStampaFiltri.Checked = True Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OMETTISEZIONEFILTRI", ConvStrRpt("N"))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CKSTORICO", ConvStrRpt(IIf(ckStorico.Checked = True, "Sì", "No").ToString))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDATINI", ConvStrRpt(edDatini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDATFIN", ConvStrRpt(edDatfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDAMAGAZ", ConvStrRpt(edDamagaz.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDAMAGAZ", ConvStrRpt(edAmagaz.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDACODART", ConvStrRpt(edDacodart.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDACODART", ConvStrRpt(edAcodart.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDACONTO", ConvStrRpt(edDaconto.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDACONTO", ConvStrRpt(edAconto.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODMARCINI", ConvStrRpt(edCodmarcini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODMARCFIN", ConvStrRpt(edCodmarcfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDFASEINI", ConvStrRpt(edFaseini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDFASEFIN", ConvStrRpt(edFasefin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDINIZIO", ConvStrRpt(edLottoDa.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDFINE", ConvStrRpt(edLottoA.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDACOMME", ConvStrRpt(edDacomme.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDACOMME", ConvStrRpt(edAcomme.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDAMATR", ConvStrRpt(edDamatr.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDAMATR", ConvStrRpt(edAmatr.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDUBICAZINI", ConvStrRpt(edUbicazini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDUBICAZFIN", ConvStrRpt(edUbicazfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDGRUPPO", ConvStrRpt(edGruppo.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDSOTTOGR", ConvStrRpt(edSottogr.Text))
          strTmp = ""
          If NTSCInt(edCausale.Text) <> 0 Then
            strTmp = edCausale.Text & IIf(lbXx_causale.Text.Trim <> "", " - ", "").ToString & lbXx_causale.Text.Trim
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCAUSALE", ConvStrRpt(strTmp))
          strTmp = ""
          If edCodcfam.Text.Trim <> "" Then
            strTmp = edCodcfam.Text & IIf(lbXx_codcfam.Text.Trim <> "", " - ", "").ToString & lbXx_codcfam.Text.Trim
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODCFAM", ConvStrRpt(strTmp))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CKSELANNOSTAG", ConvStrRpt(IIf(ckSelAnnoStag.Checked = True, "Sì", "No").ToString))
          strTmp = ""
          If ckSelAnnoStag.Checked = True Then
            If NTSCInt(edAnnotco.Text) <> 0 Then strTmp = edAnnotco.Text
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDANNOTCO", ConvStrRpt(strTmp))
          strTmp = ""
          If ckSelAnnoStag.Checked = True Then
            If NTSCInt(edCodstag.Text) <> 0 Then
              strTmp = edCodstag.Text & IIf(lbXx_Codstag.Text.Trim <> "", " - ", "").ToString & lbXx_Codstag.Text.Trim
            End If
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODSTAG", ConvStrRpt(strTmp))
          strTmp = ""
          If NTSCStr(cbTipodoc.SelectedValue) = "A" Then strTmp = "Da tutti i documenti di magazzino"
          If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then
            strTmp = "Solo da " & cbTipodoc.SelectedItem.ToString
            If ckSerie.Checked = True Then strTmp += " (serie: " & IIf(edSerie.Text.Trim <> "", edSerie.Text, "nulla").ToString & ")"
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OPPRELEVA", ConvStrRpt(strTmp))
          If NTSCStr(cbIncludi.SelectedValue) = "A" Then strTmp = "Solo articoli movimentati"
          If NTSCStr(cbIncludi.SelectedValue) = "B" Then strTmp = "Tutti gli articoli"
          If NTSCStr(cbIncludi.SelectedValue) = "C" Then strTmp = "Articoli in esistenza alla data inizio"
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OPINCLUDI", ConvStrRpt(strTmp))
        Else
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OMETTISEZIONEFILTRI", ConvStrRpt("S"))
        End If
        Select Case NTSCStr(cbTipoStampa.SelectedValue)
          Case "A", "B", "C", "D", "E" : strLottoAlfanumerico = "'S'"
          Case Else : strLottoAlfanumerico = "'N'"
        End Select
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STPLOTTI", strLottoAlfanumerico)
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128735523639234000, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128735523664038000, "Pronto.")
      Me.Refresh()
    End Try
  End Sub

  Public Overridable Function ComponiFormula2() As String
    Dim strC As String = ""
    Try
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "E", "H", "N"
          strC = "{TTSTLOCS.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTLOCS.instid} = " & oCleSche.lIITTStlocs
        Case "J"
          strC = "{TTSTMATR.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTMATR.instid} = " & oCleSche.lIITTStMatr
        Case "K"
          strC = "{TTSTMATS.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTMATS.instid} = " & oCleSche.lIITTStMats
        Case "I"
          strC = "{TTSTMATR.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTMATR.instid} = " & oCleSche.lIITTStMatr
        Case "L", "M"
          strC = "{TTSTLOCU.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTLOCU.instid} = " & oCleSche.lIITTStlocu
        Case Else
          strC = "{TTSTLOCO.codditt} = '" & DittaCorrente & "'" & _
            " And {TTSTLOCO.instid} = " & oCleSche.lIITTStloco
      End Select

      Return strC
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function FiltriEstesiNonCorretti() As Boolean
    Try
      'I controlli devono essere fatti da BNXXFILT non dai singoli programmi
      '--------------------------------------------------------------------------------------------------------------
      'If ceFiltriExt.dttFilt.Select("xx_tipocampo IN (3, 4, 5) AND xx_valore = ''").Length > 0 Then
      '  oApp.MsgBoxInfo(oApp.Tr(Me, 130671039210903273, "Attenzione!" & vbCrLf & _
      '    "Nei filtri estesi sono stati selezionati campi numerici ma non sono stati indicati i valori." & vbCrLf & _
      '    "Deselezionare i filtri o indicare valori numerici corretti."))
      '  Return True
      'End If
      '--------------------------------------------------------------------------------------------------------------
      'Return False
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
