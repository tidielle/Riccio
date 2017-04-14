Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCONF
  Public oCleConf As CLEORCONF
  Public dsConf As DataSet
  Public oCallParams As CLE__CLDP
  Public dcConf As BindingSource = New BindingSource

  Public dttCampi As New DataTable          'elenco campi filtrabili di TESTORD
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource

  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri
  Public Const MAX_ROWS_COUNT As Integer = 1000

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
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPdf As NTSInformatica.NTSBarButtonItem
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents lbTipork As NTSInformatica.NTSLabel
  Public WithEvents frStagione As NTSInformatica.NTSGroupBox
  Public WithEvents lbZona As NTSInformatica.NTSLabel
  Public WithEvents edZona As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddestfin As NTSInformatica.NTSLabel
  Public WithEvents edCoddestfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddestini As NTSInformatica.NTSLabel
  Public WithEvents edCoddestini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRigafin As NTSInformatica.NTSLabel
  Public WithEvents lbRiga As NTSInformatica.NTSLabel
  Public WithEvents edRigafin As NTSInformatica.NTSTextBoxData
  Public WithEvents edRiga As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDataConsfin As NTSInformatica.NTSLabel
  Public WithEvents lbDataCons As NTSInformatica.NTSLabel
  Public WithEvents edDataConsfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataCons As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatordfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatordini As NTSInformatica.NTSLabel
  Public WithEvents edDatordfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatordini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbCommecafin As NTSInformatica.NTSLabel
  Public WithEvents edCommecafin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCommecaini As NTSInformatica.NTSLabel
  Public WithEvents lbContoini As NTSInformatica.NTSLabel
  Public WithEvents lbNumordini As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edCommecaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumordini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents ckSelAnnoStag As NTSInformatica.NTSCheckBox
  Public WithEvents lbNumordfin As NTSInformatica.NTSLabel
  Public WithEvents edNumordfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmStorico As NTSInformatica.NTSGroupBox
  Public WithEvents opStorico2 As NTSInformatica.NTSRadioButton
  Public WithEvents opStorico1 As NTSInformatica.NTSRadioButton
  Public WithEvents opStorico0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodstag As NTSInformatica.NTSLabel
  Public WithEvents edCodstag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnotco As NTSInformatica.NTSLabel
  Public WithEvents edAnnotco As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmCliForn As NTSInformatica.NTSGroupBox
  Public WithEvents ckCliForn As NTSInformatica.NTSCheckBox
  Public WithEvents fmRiga As NTSInformatica.NTSGroupBox
  Public WithEvents opRiga2 As NTSInformatica.NTSRadioButton
  Public WithEvents opRiga1 As NTSInformatica.NTSRadioButton
  Public WithEvents opRiga0 As NTSInformatica.NTSRadioButton
  Public WithEvents fmStampati As NTSInformatica.NTSGroupBox
  Public WithEvents opStampati2 As NTSInformatica.NTSRadioButton
  Public WithEvents opStampati1 As NTSInformatica.NTSRadioButton
  Public WithEvents opStampati0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbContofin As NTSInformatica.NTSLabel
  Public WithEvents edContofin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesagen As NTSInformatica.NTSLabel
  Public WithEvents lbDescontofin As NTSInformatica.NTSLabel
  Public WithEvents lbDescontoini As NTSInformatica.NTSLabel
  Public WithEvents lbDescodcfam As NTSInformatica.NTSLabel
  Public WithEvents lbDeszona As NTSInformatica.NTSLabel
  Public WithEvents lbDescodstag As NTSInformatica.NTSLabel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents tsConf As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnFiltri2 As NTSInformatica.NTSPanel
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents cmdLock As NTSInformatica.NTSButton
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
    'Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORCONF", "BEORCONF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128534975466065282, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleConf = CType(oTmp, CLEORCONF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORCONF", strRemoteServer, strRemotePort)
    AddHandler oCleConf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleConf.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCONF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.edNumordini = New NTSInformatica.NTSTextBoxNum
    Me.edContoini = New NTSInformatica.NTSTextBoxNum
    Me.edCommecaini = New NTSInformatica.NTSTextBoxNum
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbNumordini = New NTSInformatica.NTSLabel
    Me.lbContoini = New NTSInformatica.NTSLabel
    Me.lbCommecaini = New NTSInformatica.NTSLabel
    Me.lbCommecafin = New NTSInformatica.NTSLabel
    Me.edCommecafin = New NTSInformatica.NTSTextBoxNum
    Me.edDatordini = New NTSInformatica.NTSTextBoxData
    Me.edDatordfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatordini = New NTSInformatica.NTSLabel
    Me.lbDatordfin = New NTSInformatica.NTSLabel
    Me.lbDataConsfin = New NTSInformatica.NTSLabel
    Me.lbDataCons = New NTSInformatica.NTSLabel
    Me.edDataConsfin = New NTSInformatica.NTSTextBoxData
    Me.edDataCons = New NTSInformatica.NTSTextBoxData
    Me.lbRigafin = New NTSInformatica.NTSLabel
    Me.lbRiga = New NTSInformatica.NTSLabel
    Me.edRigafin = New NTSInformatica.NTSTextBoxData
    Me.edRiga = New NTSInformatica.NTSTextBoxData
    Me.lbCoddestfin = New NTSInformatica.NTSLabel
    Me.edCoddestfin = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddestini = New NTSInformatica.NTSLabel
    Me.edCoddestini = New NTSInformatica.NTSTextBoxNum
    Me.lbCodagen = New NTSInformatica.NTSLabel
    Me.edCodagen = New NTSInformatica.NTSTextBoxNum
    Me.lbZona = New NTSInformatica.NTSLabel
    Me.edZona = New NTSInformatica.NTSTextBoxNum
    Me.frStagione = New NTSInformatica.NTSGroupBox
    Me.lbDescodstag = New NTSInformatica.NTSLabel
    Me.lbCodstag = New NTSInformatica.NTSLabel
    Me.edCodstag = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnotco = New NTSInformatica.NTSLabel
    Me.edAnnotco = New NTSInformatica.NTSTextBoxNum
    Me.ckSelAnnoStag = New NTSInformatica.NTSCheckBox
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbNumordfin = New NTSInformatica.NTSLabel
    Me.edNumordfin = New NTSInformatica.NTSTextBoxNum
    Me.fmStorico = New NTSInformatica.NTSGroupBox
    Me.opStorico2 = New NTSInformatica.NTSRadioButton
    Me.opStorico1 = New NTSInformatica.NTSRadioButton
    Me.opStorico0 = New NTSInformatica.NTSRadioButton
    Me.fmStampati = New NTSInformatica.NTSGroupBox
    Me.opStampati2 = New NTSInformatica.NTSRadioButton
    Me.opStampati1 = New NTSInformatica.NTSRadioButton
    Me.opStampati0 = New NTSInformatica.NTSRadioButton
    Me.fmRiga = New NTSInformatica.NTSGroupBox
    Me.opRiga2 = New NTSInformatica.NTSRadioButton
    Me.opRiga1 = New NTSInformatica.NTSRadioButton
    Me.opRiga0 = New NTSInformatica.NTSRadioButton
    Me.fmCliForn = New NTSInformatica.NTSGroupBox
    Me.ckCliForn = New NTSInformatica.NTSCheckBox
    Me.lbContofin = New NTSInformatica.NTSLabel
    Me.edContofin = New NTSInformatica.NTSTextBoxNum
    Me.lbDesagen = New NTSInformatica.NTSLabel
    Me.lbDeszona = New NTSInformatica.NTSLabel
    Me.lbDescodcfam = New NTSInformatica.NTSLabel
    Me.lbDescontoini = New NTSInformatica.NTSLabel
    Me.lbDescontofin = New NTSInformatica.NTSLabel
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.lbDescodlsel = New NTSInformatica.NTSLabel
    Me.lbCodlsel = New NTSInformatica.NTSLabel
    Me.edCodlsel = New NTSInformatica.NTSTextBoxNum
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.fmConfermato = New NTSInformatica.NTSGroupBox
    Me.optConfermatoEntrambe = New NTSInformatica.NTSRadioButton
    Me.optConfermato = New NTSInformatica.NTSRadioButton
    Me.optNonConfermato = New NTSInformatica.NTSRadioButton
    Me.tsConf = New NTSInformatica.NTSTabControl
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    Me.pnFiltri2 = New NTSInformatica.NTSPanel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdApriFiltri = New NTSInformatica.NTSButton
    Me.cbFiltro = New NTSInformatica.NTSComboBox
    Me.lbFiltri = New NTSInformatica.NTSLabel
    Me.pnFiltriExt = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataConsfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataCons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRigafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRiga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddestfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddestini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frStagione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frStagione.SuspendLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStorico, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStorico.SuspendLayout()
    CType(Me.opStorico2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStorico1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStorico0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStampati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStampati.SuspendLayout()
    CType(Me.opStampati2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStampati1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStampati0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRiga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRiga.SuspendLayout()
    CType(Me.opRiga2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRiga1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRiga0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCliForn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCliForn.SuspendLayout()
    CType(Me.ckCliForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.fmConfermato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmConfermato.SuspendLayout()
    CType(Me.optConfermatoEntrambe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optConfermato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optNonConfermato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsConf, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsConf.SuspendLayout()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri2.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaWord, Me.tlbStampaPdf})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.Id = 26
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "StampaPDF"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.Id = 27
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = True
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Location = New System.Drawing.Point(12, 7)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(27, 13)
    Me.lbTipork.TabIndex = 4
    Me.lbTipork.Text = "Tipo"
    Me.lbTipork.Tooltip = ""
    Me.lbTipork.UseMnemonic = False
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(133, 4)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.AutoHeight = False
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(199, 20)
    Me.cbTipork.TabIndex = 5
    Me.cbTipork.ValueMember = ""
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(133, 26)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(64, 20)
    Me.edAnno.TabIndex = 6
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(133, 268)
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
    Me.edCodcfam.Size = New System.Drawing.Size(64, 20)
    Me.edCodcfam.TabIndex = 7
    '
    'edNumordini
    '
    Me.edNumordini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordini.Location = New System.Drawing.Point(133, 48)
    Me.edNumordini.Name = "edNumordini"
    Me.edNumordini.NTSDbField = ""
    Me.edNumordini.NTSFormat = "0"
    Me.edNumordini.NTSForzaVisZoom = False
    Me.edNumordini.NTSOldValue = ""
    Me.edNumordini.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordini.Properties.AutoHeight = False
    Me.edNumordini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordini.Properties.MaxLength = 65536
    Me.edNumordini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordini.Size = New System.Drawing.Size(85, 20)
    Me.edNumordini.TabIndex = 8
    '
    'edContoini
    '
    Me.edContoini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoini.Location = New System.Drawing.Point(133, 70)
    Me.edContoini.Name = "edContoini"
    Me.edContoini.NTSDbField = ""
    Me.edContoini.NTSFormat = "0"
    Me.edContoini.NTSForzaVisZoom = False
    Me.edContoini.NTSOldValue = ""
    Me.edContoini.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoini.Properties.AutoHeight = False
    Me.edContoini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoini.Properties.MaxLength = 65536
    Me.edContoini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoini.Size = New System.Drawing.Size(85, 20)
    Me.edContoini.TabIndex = 9
    '
    'edCommecaini
    '
    Me.edCommecaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecaini.Location = New System.Drawing.Point(133, 114)
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
    Me.edCommecaini.Size = New System.Drawing.Size(85, 20)
    Me.edCommecaini.TabIndex = 10
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 29)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(97, 13)
    Me.lbAnno.TabIndex = 11
    Me.lbAnno.Text = "Anno (0=qualsiasi)"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbNumordini
    '
    Me.lbNumordini.AutoSize = True
    Me.lbNumordini.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordini.Location = New System.Drawing.Point(12, 51)
    Me.lbNumordini.Name = "lbNumordini"
    Me.lbNumordini.NTSDbField = ""
    Me.lbNumordini.Size = New System.Drawing.Size(92, 13)
    Me.lbNumordini.TabIndex = 12
    Me.lbNumordini.Text = "Da numero ordine"
    Me.lbNumordini.Tooltip = ""
    Me.lbNumordini.UseMnemonic = False
    '
    'lbContoini
    '
    Me.lbContoini.AutoSize = True
    Me.lbContoini.BackColor = System.Drawing.Color.Transparent
    Me.lbContoini.Location = New System.Drawing.Point(12, 73)
    Me.lbContoini.Name = "lbContoini"
    Me.lbContoini.NTSDbField = ""
    Me.lbContoini.Size = New System.Drawing.Size(50, 13)
    Me.lbContoini.TabIndex = 13
    Me.lbContoini.Text = "Da conto"
    Me.lbContoini.Tooltip = ""
    Me.lbContoini.UseMnemonic = False
    '
    'lbCommecaini
    '
    Me.lbCommecaini.AutoSize = True
    Me.lbCommecaini.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaini.Location = New System.Drawing.Point(12, 117)
    Me.lbCommecaini.Name = "lbCommecaini"
    Me.lbCommecaini.NTSDbField = ""
    Me.lbCommecaini.Size = New System.Drawing.Size(72, 13)
    Me.lbCommecaini.TabIndex = 14
    Me.lbCommecaini.Text = "Da commessa"
    Me.lbCommecaini.Tooltip = ""
    Me.lbCommecaini.UseMnemonic = False
    '
    'lbCommecafin
    '
    Me.lbCommecafin.AutoSize = True
    Me.lbCommecafin.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecafin.Location = New System.Drawing.Point(231, 117)
    Me.lbCommecafin.Name = "lbCommecafin"
    Me.lbCommecafin.NTSDbField = ""
    Me.lbCommecafin.Size = New System.Drawing.Size(66, 13)
    Me.lbCommecafin.TabIndex = 16
    Me.lbCommecafin.Text = "A commessa"
    Me.lbCommecafin.Tooltip = ""
    Me.lbCommecafin.UseMnemonic = False
    '
    'edCommecafin
    '
    Me.edCommecafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecafin.Location = New System.Drawing.Point(370, 114)
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
    Me.edCommecafin.Size = New System.Drawing.Size(85, 20)
    Me.edCommecafin.TabIndex = 15
    '
    'edDatordini
    '
    Me.edDatordini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordini.Location = New System.Drawing.Point(133, 136)
    Me.edDatordini.Name = "edDatordini"
    Me.edDatordini.NTSDbField = ""
    Me.edDatordini.NTSForzaVisZoom = False
    Me.edDatordini.NTSOldValue = ""
    Me.edDatordini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordini.Properties.AutoHeight = False
    Me.edDatordini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordini.Properties.MaxLength = 65536
    Me.edDatordini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordini.Size = New System.Drawing.Size(85, 20)
    Me.edDatordini.TabIndex = 17
    '
    'edDatordfin
    '
    Me.edDatordfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordfin.Location = New System.Drawing.Point(370, 136)
    Me.edDatordfin.Name = "edDatordfin"
    Me.edDatordfin.NTSDbField = ""
    Me.edDatordfin.NTSForzaVisZoom = False
    Me.edDatordfin.NTSOldValue = ""
    Me.edDatordfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordfin.Properties.AutoHeight = False
    Me.edDatordfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordfin.Properties.MaxLength = 65536
    Me.edDatordfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordfin.Size = New System.Drawing.Size(85, 20)
    Me.edDatordfin.TabIndex = 18
    '
    'lbDatordini
    '
    Me.lbDatordini.AutoSize = True
    Me.lbDatordini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordini.Location = New System.Drawing.Point(12, 139)
    Me.lbDatordini.Name = "lbDatordini"
    Me.lbDatordini.NTSDbField = ""
    Me.lbDatordini.Size = New System.Drawing.Size(78, 13)
    Me.lbDatordini.TabIndex = 19
    Me.lbDatordini.Text = "Da data ordine"
    Me.lbDatordini.Tooltip = ""
    Me.lbDatordini.UseMnemonic = False
    '
    'lbDatordfin
    '
    Me.lbDatordfin.AutoSize = True
    Me.lbDatordfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordfin.Location = New System.Drawing.Point(230, 139)
    Me.lbDatordfin.Name = "lbDatordfin"
    Me.lbDatordfin.NTSDbField = ""
    Me.lbDatordfin.Size = New System.Drawing.Size(72, 13)
    Me.lbDatordfin.TabIndex = 20
    Me.lbDatordfin.Text = "A data ordine"
    Me.lbDatordfin.Tooltip = ""
    Me.lbDatordfin.UseMnemonic = False
    '
    'lbDataConsfin
    '
    Me.lbDataConsfin.AutoSize = True
    Me.lbDataConsfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDataConsfin.Location = New System.Drawing.Point(231, 161)
    Me.lbDataConsfin.Name = "lbDataConsfin"
    Me.lbDataConsfin.NTSDbField = ""
    Me.lbDataConsfin.Size = New System.Drawing.Size(101, 13)
    Me.lbDataConsfin.TabIndex = 24
    Me.lbDataConsfin.Text = "A data cons. ordine"
    Me.lbDataConsfin.Tooltip = ""
    Me.lbDataConsfin.UseMnemonic = False
    '
    'lbDataCons
    '
    Me.lbDataCons.AutoSize = True
    Me.lbDataCons.BackColor = System.Drawing.Color.Transparent
    Me.lbDataCons.Location = New System.Drawing.Point(12, 161)
    Me.lbDataCons.Name = "lbDataCons"
    Me.lbDataCons.NTSDbField = ""
    Me.lbDataCons.Size = New System.Drawing.Size(107, 13)
    Me.lbDataCons.TabIndex = 23
    Me.lbDataCons.Text = "Da data cons. ordine"
    Me.lbDataCons.Tooltip = ""
    Me.lbDataCons.UseMnemonic = False
    '
    'edDataConsfin
    '
    Me.edDataConsfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataConsfin.Location = New System.Drawing.Point(370, 158)
    Me.edDataConsfin.Name = "edDataConsfin"
    Me.edDataConsfin.NTSDbField = ""
    Me.edDataConsfin.NTSForzaVisZoom = False
    Me.edDataConsfin.NTSOldValue = ""
    Me.edDataConsfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataConsfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataConsfin.Properties.AutoHeight = False
    Me.edDataConsfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataConsfin.Properties.MaxLength = 65536
    Me.edDataConsfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataConsfin.Size = New System.Drawing.Size(85, 20)
    Me.edDataConsfin.TabIndex = 22
    '
    'edDataCons
    '
    Me.edDataCons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataCons.Location = New System.Drawing.Point(133, 158)
    Me.edDataCons.Name = "edDataCons"
    Me.edDataCons.NTSDbField = ""
    Me.edDataCons.NTSForzaVisZoom = False
    Me.edDataCons.NTSOldValue = ""
    Me.edDataCons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataCons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataCons.Properties.AutoHeight = False
    Me.edDataCons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataCons.Properties.MaxLength = 65536
    Me.edDataCons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataCons.Size = New System.Drawing.Size(85, 20)
    Me.edDataCons.TabIndex = 21
    '
    'lbRigafin
    '
    Me.lbRigafin.AutoSize = True
    Me.lbRigafin.BackColor = System.Drawing.Color.Transparent
    Me.lbRigafin.Location = New System.Drawing.Point(231, 183)
    Me.lbRigafin.Name = "lbRigafin"
    Me.lbRigafin.NTSDbField = ""
    Me.lbRigafin.Size = New System.Drawing.Size(89, 13)
    Me.lbRigafin.TabIndex = 28
    Me.lbRigafin.Text = "A data cons. riga"
    Me.lbRigafin.Tooltip = ""
    Me.lbRigafin.UseMnemonic = False
    '
    'lbRiga
    '
    Me.lbRiga.AutoSize = True
    Me.lbRiga.BackColor = System.Drawing.Color.Transparent
    Me.lbRiga.Location = New System.Drawing.Point(12, 183)
    Me.lbRiga.Name = "lbRiga"
    Me.lbRiga.NTSDbField = ""
    Me.lbRiga.Size = New System.Drawing.Size(95, 13)
    Me.lbRiga.TabIndex = 27
    Me.lbRiga.Text = "Da data cons. riga"
    Me.lbRiga.Tooltip = ""
    Me.lbRiga.UseMnemonic = False
    '
    'edRigafin
    '
    Me.edRigafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRigafin.Location = New System.Drawing.Point(370, 180)
    Me.edRigafin.Name = "edRigafin"
    Me.edRigafin.NTSDbField = ""
    Me.edRigafin.NTSForzaVisZoom = False
    Me.edRigafin.NTSOldValue = ""
    Me.edRigafin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRigafin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRigafin.Properties.AutoHeight = False
    Me.edRigafin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRigafin.Properties.MaxLength = 65536
    Me.edRigafin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRigafin.Size = New System.Drawing.Size(85, 20)
    Me.edRigafin.TabIndex = 26
    '
    'edRiga
    '
    Me.edRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRiga.Location = New System.Drawing.Point(133, 180)
    Me.edRiga.Name = "edRiga"
    Me.edRiga.NTSDbField = ""
    Me.edRiga.NTSForzaVisZoom = False
    Me.edRiga.NTSOldValue = ""
    Me.edRiga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRiga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRiga.Properties.AutoHeight = False
    Me.edRiga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRiga.Properties.MaxLength = 65536
    Me.edRiga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRiga.Size = New System.Drawing.Size(85, 20)
    Me.edRiga.TabIndex = 25
    '
    'lbCoddestfin
    '
    Me.lbCoddestfin.AutoSize = True
    Me.lbCoddestfin.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddestfin.Location = New System.Drawing.Point(231, 205)
    Me.lbCoddestfin.Name = "lbCoddestfin"
    Me.lbCoddestfin.NTSDbField = ""
    Me.lbCoddestfin.Size = New System.Drawing.Size(77, 13)
    Me.lbCoddestfin.TabIndex = 32
    Me.lbCoddestfin.Text = "A destinazione"
    Me.lbCoddestfin.Tooltip = ""
    Me.lbCoddestfin.UseMnemonic = False
    '
    'edCoddestfin
    '
    Me.edCoddestfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddestfin.Location = New System.Drawing.Point(370, 202)
    Me.edCoddestfin.Name = "edCoddestfin"
    Me.edCoddestfin.NTSDbField = ""
    Me.edCoddestfin.NTSFormat = "0"
    Me.edCoddestfin.NTSForzaVisZoom = False
    Me.edCoddestfin.NTSOldValue = ""
    Me.edCoddestfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddestfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddestfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddestfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddestfin.Properties.AutoHeight = False
    Me.edCoddestfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddestfin.Properties.MaxLength = 65536
    Me.edCoddestfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddestfin.Size = New System.Drawing.Size(85, 20)
    Me.edCoddestfin.TabIndex = 31
    '
    'lbCoddestini
    '
    Me.lbCoddestini.AutoSize = True
    Me.lbCoddestini.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddestini.Location = New System.Drawing.Point(12, 205)
    Me.lbCoddestini.Name = "lbCoddestini"
    Me.lbCoddestini.NTSDbField = ""
    Me.lbCoddestini.Size = New System.Drawing.Size(83, 13)
    Me.lbCoddestini.TabIndex = 30
    Me.lbCoddestini.Text = "Da destinazione"
    Me.lbCoddestini.Tooltip = ""
    Me.lbCoddestini.UseMnemonic = False
    '
    'edCoddestini
    '
    Me.edCoddestini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddestini.Location = New System.Drawing.Point(133, 202)
    Me.edCoddestini.Name = "edCoddestini"
    Me.edCoddestini.NTSDbField = ""
    Me.edCoddestini.NTSFormat = "0"
    Me.edCoddestini.NTSForzaVisZoom = False
    Me.edCoddestini.NTSOldValue = ""
    Me.edCoddestini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddestini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddestini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddestini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddestini.Properties.AutoHeight = False
    Me.edCoddestini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddestini.Properties.MaxLength = 65536
    Me.edCoddestini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddestini.Size = New System.Drawing.Size(85, 20)
    Me.edCoddestini.TabIndex = 29
    '
    'lbCodagen
    '
    Me.lbCodagen.AutoSize = True
    Me.lbCodagen.BackColor = System.Drawing.Color.Transparent
    Me.lbCodagen.Location = New System.Drawing.Point(12, 227)
    Me.lbCodagen.Name = "lbCodagen"
    Me.lbCodagen.NTSDbField = ""
    Me.lbCodagen.Size = New System.Drawing.Size(102, 13)
    Me.lbCodagen.TabIndex = 34
    Me.lbCodagen.Text = "Agente    (0 = tutti)"
    Me.lbCodagen.Tooltip = ""
    Me.lbCodagen.UseMnemonic = False
    '
    'edCodagen
    '
    Me.edCodagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagen.Location = New System.Drawing.Point(133, 224)
    Me.edCodagen.Name = "edCodagen"
    Me.edCodagen.NTSDbField = ""
    Me.edCodagen.NTSFormat = "0"
    Me.edCodagen.NTSForzaVisZoom = False
    Me.edCodagen.NTSOldValue = ""
    Me.edCodagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodagen.Properties.AutoHeight = False
    Me.edCodagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagen.Properties.MaxLength = 65536
    Me.edCodagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagen.Size = New System.Drawing.Size(64, 20)
    Me.edCodagen.TabIndex = 33
    '
    'lbZona
    '
    Me.lbZona.AutoSize = True
    Me.lbZona.BackColor = System.Drawing.Color.Transparent
    Me.lbZona.Location = New System.Drawing.Point(12, 249)
    Me.lbZona.Name = "lbZona"
    Me.lbZona.NTSDbField = ""
    Me.lbZona.Size = New System.Drawing.Size(104, 13)
    Me.lbZona.TabIndex = 36
    Me.lbZona.Text = "Zona       (0 = tutte)"
    Me.lbZona.Tooltip = ""
    Me.lbZona.UseMnemonic = False
    '
    'edZona
    '
    Me.edZona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZona.Location = New System.Drawing.Point(133, 246)
    Me.edZona.Name = "edZona"
    Me.edZona.NTSDbField = ""
    Me.edZona.NTSFormat = "0"
    Me.edZona.NTSForzaVisZoom = False
    Me.edZona.NTSOldValue = ""
    Me.edZona.Properties.Appearance.Options.UseTextOptions = True
    Me.edZona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZona.Properties.AutoHeight = False
    Me.edZona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZona.Properties.MaxLength = 65536
    Me.edZona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZona.Size = New System.Drawing.Size(64, 20)
    Me.edZona.TabIndex = 35
    '
    'frStagione
    '
    Me.frStagione.AllowDrop = True
    Me.frStagione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frStagione.Appearance.Options.UseBackColor = True
    Me.frStagione.Controls.Add(Me.lbDescodstag)
    Me.frStagione.Controls.Add(Me.lbCodstag)
    Me.frStagione.Controls.Add(Me.edCodstag)
    Me.frStagione.Controls.Add(Me.lbAnnotco)
    Me.frStagione.Controls.Add(Me.edAnnotco)
    Me.frStagione.Controls.Add(Me.ckSelAnnoStag)
    Me.frStagione.Cursor = System.Windows.Forms.Cursors.Default
    Me.frStagione.Location = New System.Drawing.Point(15, 318)
    Me.frStagione.Name = "frStagione"
    Me.frStagione.Size = New System.Drawing.Size(440, 76)
    Me.frStagione.TabIndex = 37
    '
    'lbDescodstag
    '
    Me.lbDescodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodstag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodstag.Location = New System.Drawing.Point(188, 49)
    Me.lbDescodstag.Name = "lbDescodstag"
    Me.lbDescodstag.NTSDbField = ""
    Me.lbDescodstag.Size = New System.Drawing.Size(247, 20)
    Me.lbDescodstag.TabIndex = 50
    Me.lbDescodstag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodstag.Tooltip = ""
    Me.lbDescodstag.UseMnemonic = False
    '
    'lbCodstag
    '
    Me.lbCodstag.AutoSize = True
    Me.lbCodstag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodstag.Location = New System.Drawing.Point(13, 53)
    Me.lbCodstag.Name = "lbCodstag"
    Me.lbCodstag.NTSDbField = ""
    Me.lbCodstag.Size = New System.Drawing.Size(49, 13)
    Me.lbCodstag.TabIndex = 42
    Me.lbCodstag.Text = "Stagione"
    Me.lbCodstag.Tooltip = ""
    Me.lbCodstag.UseMnemonic = False
    '
    'edCodstag
    '
    Me.edCodstag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodstag.Location = New System.Drawing.Point(118, 49)
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
    Me.edCodstag.Size = New System.Drawing.Size(64, 20)
    Me.edCodstag.TabIndex = 41
    '
    'lbAnnotco
    '
    Me.lbAnnotco.AutoSize = True
    Me.lbAnnotco.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnotco.Location = New System.Drawing.Point(13, 26)
    Me.lbAnnotco.Name = "lbAnnotco"
    Me.lbAnnotco.NTSDbField = ""
    Me.lbAnnotco.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnotco.TabIndex = 40
    Me.lbAnnotco.Text = "Anno"
    Me.lbAnnotco.Tooltip = ""
    Me.lbAnnotco.UseMnemonic = False
    '
    'edAnnotco
    '
    Me.edAnnotco.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnotco.Location = New System.Drawing.Point(118, 23)
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
    Me.edAnnotco.Size = New System.Drawing.Size(64, 20)
    Me.edAnnotco.TabIndex = 39
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
    Me.ckSelAnnoStag.Properties.Caption = "Seleziona Anno/Stagione"
    Me.ckSelAnnoStag.Size = New System.Drawing.Size(165, 19)
    Me.ckSelAnnoStag.TabIndex = 38
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(12, 271)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(72, 13)
    Me.lbCodcfam.TabIndex = 38
    Me.lbCodcfam.Text = "Linea/famiglia"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(370, 26)
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
    Me.edSerie.TabIndex = 39
    '
    'lbNumordfin
    '
    Me.lbNumordfin.AutoSize = True
    Me.lbNumordfin.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordfin.Location = New System.Drawing.Point(231, 51)
    Me.lbNumordfin.Name = "lbNumordfin"
    Me.lbNumordfin.NTSDbField = ""
    Me.lbNumordfin.Size = New System.Drawing.Size(86, 13)
    Me.lbNumordfin.TabIndex = 42
    Me.lbNumordfin.Text = "A numero ordine"
    Me.lbNumordfin.Tooltip = ""
    Me.lbNumordfin.UseMnemonic = False
    '
    'edNumordfin
    '
    Me.edNumordfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordfin.Location = New System.Drawing.Point(370, 48)
    Me.edNumordfin.Name = "edNumordfin"
    Me.edNumordfin.NTSDbField = ""
    Me.edNumordfin.NTSFormat = "0"
    Me.edNumordfin.NTSForzaVisZoom = False
    Me.edNumordfin.NTSOldValue = ""
    Me.edNumordfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordfin.Properties.AutoHeight = False
    Me.edNumordfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordfin.Properties.MaxLength = 65536
    Me.edNumordfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordfin.Size = New System.Drawing.Size(85, 20)
    Me.edNumordfin.TabIndex = 41
    '
    'fmStorico
    '
    Me.fmStorico.AllowDrop = True
    Me.fmStorico.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStorico.Appearance.Options.UseBackColor = True
    Me.fmStorico.Controls.Add(Me.opStorico2)
    Me.fmStorico.Controls.Add(Me.opStorico1)
    Me.fmStorico.Controls.Add(Me.opStorico0)
    Me.fmStorico.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStorico.Location = New System.Drawing.Point(7, 21)
    Me.fmStorico.Name = "fmStorico"
    Me.fmStorico.Size = New System.Drawing.Size(200, 83)
    Me.fmStorico.TabIndex = 43
    '
    'opStorico2
    '
    Me.opStorico2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico2.Location = New System.Drawing.Point(9, 57)
    Me.opStorico2.Name = "opStorico2"
    Me.opStorico2.NTSCheckValue = "S"
    Me.opStorico2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico2.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico2.Properties.AutoHeight = False
    Me.opStorico2.Properties.Caption = "&Entrambi"
    Me.opStorico2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico2.Size = New System.Drawing.Size(111, 19)
    Me.opStorico2.TabIndex = 2
    '
    'opStorico1
    '
    Me.opStorico1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico1.Location = New System.Drawing.Point(9, 40)
    Me.opStorico1.Name = "opStorico1"
    Me.opStorico1.NTSCheckValue = "S"
    Me.opStorico1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico1.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico1.Properties.AutoHeight = False
    Me.opStorico1.Properties.Caption = "Ordini in &storico"
    Me.opStorico1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico1.Size = New System.Drawing.Size(111, 19)
    Me.opStorico1.TabIndex = 1
    '
    'opStorico0
    '
    Me.opStorico0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStorico0.EditValue = True
    Me.opStorico0.Location = New System.Drawing.Point(9, 23)
    Me.opStorico0.Name = "opStorico0"
    Me.opStorico0.NTSCheckValue = "S"
    Me.opStorico0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStorico0.Properties.Appearance.Options.UseBackColor = True
    Me.opStorico0.Properties.AutoHeight = False
    Me.opStorico0.Properties.Caption = "&Ordini in essere"
    Me.opStorico0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStorico0.Size = New System.Drawing.Size(111, 19)
    Me.opStorico0.TabIndex = 0
    '
    'fmStampati
    '
    Me.fmStampati.AllowDrop = True
    Me.fmStampati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStampati.Appearance.Options.UseBackColor = True
    Me.fmStampati.Controls.Add(Me.opStampati2)
    Me.fmStampati.Controls.Add(Me.opStampati1)
    Me.fmStampati.Controls.Add(Me.opStampati0)
    Me.fmStampati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStampati.Location = New System.Drawing.Point(7, 109)
    Me.fmStampati.Name = "fmStampati"
    Me.fmStampati.Size = New System.Drawing.Size(200, 83)
    Me.fmStampati.TabIndex = 44
    '
    'opStampati2
    '
    Me.opStampati2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati2.Location = New System.Drawing.Point(9, 57)
    Me.opStampati2.Name = "opStampati2"
    Me.opStampati2.NTSCheckValue = "S"
    Me.opStampati2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati2.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati2.Properties.AutoHeight = False
    Me.opStampati2.Properties.Caption = "Entra&mbi"
    Me.opStampati2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati2.Size = New System.Drawing.Size(111, 19)
    Me.opStampati2.TabIndex = 2
    '
    'opStampati1
    '
    Me.opStampati1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati1.Location = New System.Drawing.Point(9, 40)
    Me.opStampati1.Name = "opStampati1"
    Me.opStampati1.NTSCheckValue = "S"
    Me.opStampati1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati1.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati1.Properties.AutoHeight = False
    Me.opStampati1.Properties.Caption = "Solo &già stampati"
    Me.opStampati1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati1.Size = New System.Drawing.Size(133, 19)
    Me.opStampati1.TabIndex = 1
    '
    'opStampati0
    '
    Me.opStampati0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStampati0.EditValue = True
    Me.opStampati0.Location = New System.Drawing.Point(9, 23)
    Me.opStampati0.Name = "opStampati0"
    Me.opStampati0.NTSCheckValue = "S"
    Me.opStampati0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStampati0.Properties.Appearance.Options.UseBackColor = True
    Me.opStampati0.Properties.AutoHeight = False
    Me.opStampati0.Properties.Caption = "So&lo non stampati"
    Me.opStampati0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStampati0.Size = New System.Drawing.Size(133, 19)
    Me.opStampati0.TabIndex = 0
    '
    'fmRiga
    '
    Me.fmRiga.AllowDrop = True
    Me.fmRiga.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRiga.Appearance.Options.UseBackColor = True
    Me.fmRiga.Controls.Add(Me.opRiga2)
    Me.fmRiga.Controls.Add(Me.opRiga1)
    Me.fmRiga.Controls.Add(Me.opRiga0)
    Me.fmRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRiga.Location = New System.Drawing.Point(7, 198)
    Me.fmRiga.Name = "fmRiga"
    Me.fmRiga.Size = New System.Drawing.Size(200, 83)
    Me.fmRiga.TabIndex = 45
    '
    'opRiga2
    '
    Me.opRiga2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRiga2.EditValue = True
    Me.opRiga2.Location = New System.Drawing.Point(9, 57)
    Me.opRiga2.Name = "opRiga2"
    Me.opRiga2.NTSCheckValue = "S"
    Me.opRiga2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga2.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga2.Properties.AutoHeight = False
    Me.opRiga2.Properties.Caption = "Entram&be"
    Me.opRiga2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga2.Size = New System.Drawing.Size(111, 19)
    Me.opRiga2.TabIndex = 2
    '
    'opRiga1
    '
    Me.opRiga1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRiga1.Location = New System.Drawing.Point(9, 40)
    Me.opRiga1.Name = "opRiga1"
    Me.opRiga1.NTSCheckValue = "S"
    Me.opRiga1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga1.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga1.Properties.AutoHeight = False
    Me.opRiga1.Properties.Caption = "Rig&he saldate"
    Me.opRiga1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga1.Size = New System.Drawing.Size(111, 19)
    Me.opRiga1.TabIndex = 1
    '
    'opRiga0
    '
    Me.opRiga0.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opRiga0.Location = New System.Drawing.Point(9, 23)
    Me.opRiga0.Name = "opRiga0"
    Me.opRiga0.NTSCheckValue = "S"
    Me.opRiga0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRiga0.Properties.Appearance.Options.UseBackColor = True
    Me.opRiga0.Properties.AutoHeight = False
    Me.opRiga0.Properties.Caption = "&Righe aperte"
    Me.opRiga0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRiga0.Size = New System.Drawing.Size(111, 19)
    Me.opRiga0.TabIndex = 0
    '
    'fmCliForn
    '
    Me.fmCliForn.AllowDrop = True
    Me.fmCliForn.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCliForn.Appearance.Options.UseBackColor = True
    Me.fmCliForn.Controls.Add(Me.ckCliForn)
    Me.fmCliForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCliForn.Location = New System.Drawing.Point(7, 374)
    Me.fmCliForn.Name = "fmCliForn"
    Me.fmCliForn.Size = New System.Drawing.Size(200, 52)
    Me.fmCliForn.TabIndex = 46
    '
    'ckCliForn
    '
    Me.ckCliForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCliForn.Location = New System.Drawing.Point(9, 23)
    Me.ckCliForn.Name = "ckCliForn"
    Me.ckCliForn.NTSCheckValue = "S"
    Me.ckCliForn.NTSUnCheckValue = "N"
    Me.ckCliForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCliForn.Properties.Appearance.Options.UseBackColor = True
    Me.ckCliForn.Properties.AutoHeight = False
    Me.ckCliForn.Properties.Caption = "Raggruppa per cliente/fornitore"
    Me.ckCliForn.Size = New System.Drawing.Size(191, 19)
    Me.ckCliForn.TabIndex = 0
    '
    'lbContofin
    '
    Me.lbContofin.AutoSize = True
    Me.lbContofin.BackColor = System.Drawing.Color.Transparent
    Me.lbContofin.Location = New System.Drawing.Point(12, 95)
    Me.lbContofin.Name = "lbContofin"
    Me.lbContofin.NTSDbField = ""
    Me.lbContofin.Size = New System.Drawing.Size(44, 13)
    Me.lbContofin.TabIndex = 48
    Me.lbContofin.Text = "A conto"
    Me.lbContofin.Tooltip = ""
    Me.lbContofin.UseMnemonic = False
    '
    'edContofin
    '
    Me.edContofin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContofin.Location = New System.Drawing.Point(133, 92)
    Me.edContofin.Name = "edContofin"
    Me.edContofin.NTSDbField = ""
    Me.edContofin.NTSFormat = "0"
    Me.edContofin.NTSForzaVisZoom = False
    Me.edContofin.NTSOldValue = ""
    Me.edContofin.Properties.Appearance.Options.UseTextOptions = True
    Me.edContofin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContofin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContofin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContofin.Properties.AutoHeight = False
    Me.edContofin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContofin.Properties.MaxLength = 65536
    Me.edContofin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContofin.Size = New System.Drawing.Size(85, 20)
    Me.edContofin.TabIndex = 47
    '
    'lbDesagen
    '
    Me.lbDesagen.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagen.Location = New System.Drawing.Point(203, 224)
    Me.lbDesagen.Name = "lbDesagen"
    Me.lbDesagen.NTSDbField = ""
    Me.lbDesagen.Size = New System.Drawing.Size(252, 20)
    Me.lbDesagen.TabIndex = 49
    Me.lbDesagen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesagen.Tooltip = ""
    Me.lbDesagen.UseMnemonic = False
    '
    'lbDeszona
    '
    Me.lbDeszona.BackColor = System.Drawing.Color.Transparent
    Me.lbDeszona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDeszona.Location = New System.Drawing.Point(203, 246)
    Me.lbDeszona.Name = "lbDeszona"
    Me.lbDeszona.NTSDbField = ""
    Me.lbDeszona.Size = New System.Drawing.Size(252, 20)
    Me.lbDeszona.TabIndex = 50
    Me.lbDeszona.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDeszona.Tooltip = ""
    Me.lbDeszona.UseMnemonic = False
    '
    'lbDescodcfam
    '
    Me.lbDescodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodcfam.Location = New System.Drawing.Point(203, 268)
    Me.lbDescodcfam.Name = "lbDescodcfam"
    Me.lbDescodcfam.NTSDbField = ""
    Me.lbDescodcfam.Size = New System.Drawing.Size(252, 20)
    Me.lbDescodcfam.TabIndex = 51
    Me.lbDescodcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodcfam.Tooltip = ""
    Me.lbDescodcfam.UseMnemonic = False
    '
    'lbDescontoini
    '
    Me.lbDescontoini.BackColor = System.Drawing.Color.Transparent
    Me.lbDescontoini.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescontoini.Location = New System.Drawing.Point(224, 70)
    Me.lbDescontoini.Name = "lbDescontoini"
    Me.lbDescontoini.NTSDbField = ""
    Me.lbDescontoini.Size = New System.Drawing.Size(231, 20)
    Me.lbDescontoini.TabIndex = 52
    Me.lbDescontoini.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescontoini.Tooltip = ""
    Me.lbDescontoini.UseMnemonic = False
    '
    'lbDescontofin
    '
    Me.lbDescontofin.BackColor = System.Drawing.Color.Transparent
    Me.lbDescontofin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescontofin.Location = New System.Drawing.Point(224, 92)
    Me.lbDescontofin.Name = "lbDescontofin"
    Me.lbDescontofin.NTSDbField = ""
    Me.lbDescontofin.Size = New System.Drawing.Size(231, 20)
    Me.lbDescontofin.TabIndex = 53
    Me.lbDescontofin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescontofin.Tooltip = ""
    Me.lbDescontofin.UseMnemonic = False
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.lbDescodlsel)
    Me.pnLeft.Controls.Add(Me.lbCodlsel)
    Me.pnLeft.Controls.Add(Me.edCodlsel)
    Me.pnLeft.Controls.Add(Me.ckSerie)
    Me.pnLeft.Controls.Add(Me.lbTipork)
    Me.pnLeft.Controls.Add(Me.lbDescontofin)
    Me.pnLeft.Controls.Add(Me.cbTipork)
    Me.pnLeft.Controls.Add(Me.lbDescontoini)
    Me.pnLeft.Controls.Add(Me.edAnno)
    Me.pnLeft.Controls.Add(Me.lbDescodcfam)
    Me.pnLeft.Controls.Add(Me.edCodcfam)
    Me.pnLeft.Controls.Add(Me.lbDeszona)
    Me.pnLeft.Controls.Add(Me.edNumordini)
    Me.pnLeft.Controls.Add(Me.lbDesagen)
    Me.pnLeft.Controls.Add(Me.edContoini)
    Me.pnLeft.Controls.Add(Me.lbContofin)
    Me.pnLeft.Controls.Add(Me.edCommecaini)
    Me.pnLeft.Controls.Add(Me.edContofin)
    Me.pnLeft.Controls.Add(Me.lbAnno)
    Me.pnLeft.Controls.Add(Me.lbNumordini)
    Me.pnLeft.Controls.Add(Me.lbContoini)
    Me.pnLeft.Controls.Add(Me.lbCommecaini)
    Me.pnLeft.Controls.Add(Me.edCommecafin)
    Me.pnLeft.Controls.Add(Me.lbNumordfin)
    Me.pnLeft.Controls.Add(Me.lbCommecafin)
    Me.pnLeft.Controls.Add(Me.edNumordfin)
    Me.pnLeft.Controls.Add(Me.edDatordini)
    Me.pnLeft.Controls.Add(Me.edDatordfin)
    Me.pnLeft.Controls.Add(Me.edSerie)
    Me.pnLeft.Controls.Add(Me.lbDatordini)
    Me.pnLeft.Controls.Add(Me.lbCodcfam)
    Me.pnLeft.Controls.Add(Me.lbDatordfin)
    Me.pnLeft.Controls.Add(Me.frStagione)
    Me.pnLeft.Controls.Add(Me.edDataCons)
    Me.pnLeft.Controls.Add(Me.lbZona)
    Me.pnLeft.Controls.Add(Me.edDataConsfin)
    Me.pnLeft.Controls.Add(Me.edZona)
    Me.pnLeft.Controls.Add(Me.lbDataCons)
    Me.pnLeft.Controls.Add(Me.lbCodagen)
    Me.pnLeft.Controls.Add(Me.lbDataConsfin)
    Me.pnLeft.Controls.Add(Me.edCodagen)
    Me.pnLeft.Controls.Add(Me.edRiga)
    Me.pnLeft.Controls.Add(Me.lbCoddestfin)
    Me.pnLeft.Controls.Add(Me.edRigafin)
    Me.pnLeft.Controls.Add(Me.edCoddestfin)
    Me.pnLeft.Controls.Add(Me.lbRiga)
    Me.pnLeft.Controls.Add(Me.lbCoddestini)
    Me.pnLeft.Controls.Add(Me.lbRigafin)
    Me.pnLeft.Controls.Add(Me.edCoddestini)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(467, 420)
    Me.pnLeft.TabIndex = 54
    Me.pnLeft.Text = "NtsPanel1"
    '
    'lbDescodlsel
    '
    Me.lbDescodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodlsel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodlsel.Location = New System.Drawing.Point(203, 290)
    Me.lbDescodlsel.Name = "lbDescodlsel"
    Me.lbDescodlsel.NTSDbField = ""
    Me.lbDescodlsel.Size = New System.Drawing.Size(252, 20)
    Me.lbDescodlsel.TabIndex = 57
    Me.lbDescodlsel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodlsel.Tooltip = ""
    Me.lbDescodlsel.UseMnemonic = False
    '
    'lbCodlsel
    '
    Me.lbCodlsel.AutoSize = True
    Me.lbCodlsel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodlsel.Location = New System.Drawing.Point(12, 293)
    Me.lbCodlsel.Name = "lbCodlsel"
    Me.lbCodlsel.NTSDbField = ""
    Me.lbCodlsel.Size = New System.Drawing.Size(86, 13)
    Me.lbCodlsel.TabIndex = 56
    Me.lbCodlsel.Text = "Lista selezionata"
    Me.lbCodlsel.Tooltip = ""
    Me.lbCodlsel.UseMnemonic = False
    '
    'edCodlsel
    '
    Me.edCodlsel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlsel.Location = New System.Drawing.Point(133, 290)
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
    Me.edCodlsel.Size = New System.Drawing.Size(64, 20)
    Me.edCodlsel.TabIndex = 55
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(234, 27)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(63, 19)
    Me.ckSerie.TabIndex = 54
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.fmConfermato)
    Me.pnRight.Controls.Add(Me.fmStorico)
    Me.pnRight.Controls.Add(Me.fmStampati)
    Me.pnRight.Controls.Add(Me.fmCliForn)
    Me.pnRight.Controls.Add(Me.fmRiga)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(476, 30)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(219, 482)
    Me.pnRight.TabIndex = 55
    Me.pnRight.Text = "NtsPanel1"
    '
    'fmConfermato
    '
    Me.fmConfermato.AllowDrop = True
    Me.fmConfermato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmConfermato.Appearance.Options.UseBackColor = True
    Me.fmConfermato.Controls.Add(Me.optConfermatoEntrambe)
    Me.fmConfermato.Controls.Add(Me.optConfermato)
    Me.fmConfermato.Controls.Add(Me.optNonConfermato)
    Me.fmConfermato.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmConfermato.Location = New System.Drawing.Point(7, 287)
    Me.fmConfermato.Name = "fmConfermato"
    Me.fmConfermato.Size = New System.Drawing.Size(200, 83)
    Me.fmConfermato.TabIndex = 47
    '
    'optConfermatoEntrambe
    '
    Me.optConfermatoEntrambe.Cursor = System.Windows.Forms.Cursors.Default
    Me.optConfermatoEntrambe.EditValue = True
    Me.optConfermatoEntrambe.Location = New System.Drawing.Point(9, 57)
    Me.optConfermatoEntrambe.Name = "optConfermatoEntrambe"
    Me.optConfermatoEntrambe.NTSCheckValue = "S"
    Me.optConfermatoEntrambe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optConfermatoEntrambe.Properties.Appearance.Options.UseBackColor = True
    Me.optConfermatoEntrambe.Properties.AutoHeight = False
    Me.optConfermatoEntrambe.Properties.Caption = "Entrambe"
    Me.optConfermatoEntrambe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optConfermatoEntrambe.Size = New System.Drawing.Size(111, 19)
    Me.optConfermatoEntrambe.TabIndex = 2
    '
    'optConfermato
    '
    Me.optConfermato.Cursor = System.Windows.Forms.Cursors.Default
    Me.optConfermato.Location = New System.Drawing.Point(9, 40)
    Me.optConfermato.Name = "optConfermato"
    Me.optConfermato.NTSCheckValue = "S"
    Me.optConfermato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optConfermato.Properties.Appearance.Options.UseBackColor = True
    Me.optConfermato.Properties.AutoHeight = False
    Me.optConfermato.Properties.Caption = "Confermato"
    Me.optConfermato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optConfermato.Size = New System.Drawing.Size(111, 19)
    Me.optConfermato.TabIndex = 1
    '
    'optNonConfermato
    '
    Me.optNonConfermato.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNonConfermato.Location = New System.Drawing.Point(9, 23)
    Me.optNonConfermato.Name = "optNonConfermato"
    Me.optNonConfermato.NTSCheckValue = "S"
    Me.optNonConfermato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNonConfermato.Properties.Appearance.Options.UseBackColor = True
    Me.optNonConfermato.Properties.AutoHeight = False
    Me.optNonConfermato.Properties.Caption = "Non confermato"
    Me.optNonConfermato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNonConfermato.Size = New System.Drawing.Size(111, 19)
    Me.optNonConfermato.TabIndex = 0
    '
    'tsConf
    '
    Me.tsConf.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsConf.Location = New System.Drawing.Point(0, 62)
    Me.tsConf.Name = "tsConf"
    Me.tsConf.SelectedTabPage = Me.NtsTabPage1
    Me.tsConf.Size = New System.Drawing.Size(476, 450)
    Me.tsConf.TabIndex = 56
    Me.tsConf.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsConf.Text = "NtsTabControl1"
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
    Me.NtsTabPage2.Size = New System.Drawing.Size(467, 420)
    Me.NtsTabPage2.Text = "&2 - Filtri Estesi"
    '
    'ceFiltriExt
    '
    Me.ceFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.ceFiltriExt.MinimumSize = New System.Drawing.Size(399, 193)
    Me.ceFiltriExt.Name = "ceFiltriExt"
    Me.ceFiltriExt.oParent = Nothing
    Me.ceFiltriExt.Size = New System.Drawing.Size(467, 420)
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
    Me.pnFiltri2.Location = New System.Drawing.Point(433, 389)
    Me.pnFiltri2.Name = "pnFiltri2"
    Me.pnFiltri2.NTSActiveTrasparency = True
    Me.pnFiltri2.Size = New System.Drawing.Size(34, 31)
    Me.pnFiltri2.TabIndex = 0
    Me.pnFiltri2.Text = "NtsPanel1"
    Me.pnFiltri2.Visible = False
    '
    'cmdLock
    '
    Me.cmdLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(-101, 5)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(132, 24)
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
    Me.grFiltri1.Size = New System.Drawing.Size(34, 0)
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
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnLeft)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(467, 420)
    Me.NtsTabPage1.Text = "&1- Principale"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdApriFiltri)
    Me.pnTop.Controls.Add(Me.cbFiltro)
    Me.pnTop.Controls.Add(Me.lbFiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(476, 32)
    Me.pnTop.TabIndex = 57
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdApriFiltri
    '
    Me.cmdApriFiltri.Image = CType(resources.GetObject("cmdApriFiltri.Image"), System.Drawing.Image)
    Me.cmdApriFiltri.ImageAlignment = DevExpress.Utils.HorzAlignment.[Default]
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
    Me.pnFiltriExt.Size = New System.Drawing.Size(467, 420)
    Me.pnFiltriExt.TabIndex = 2
    Me.pnFiltriExt.Text = "NtsPanel1"
    '
    'FRMORCONF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(695, 512)
    Me.Controls.Add(Me.tsConf)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMORCONF"
    Me.Text = "STAMPA ORDINI -- CONFERME D'ORDINE -- PREVENTIVI -- IMPEGNI DI TRASFERIMENTO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataConsfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataCons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRigafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRiga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddestfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddestini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frStagione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frStagione.ResumeLayout(False)
    Me.frStagione.PerformLayout()
    CType(Me.edCodstag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnotco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelAnnoStag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStorico, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStorico.ResumeLayout(False)
    Me.fmStorico.PerformLayout()
    CType(Me.opStorico2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStorico1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStorico0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStampati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStampati.ResumeLayout(False)
    Me.fmStampati.PerformLayout()
    CType(Me.opStampati2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStampati1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStampati0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRiga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRiga.ResumeLayout(False)
    Me.fmRiga.PerformLayout()
    CType(Me.opRiga2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRiga1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRiga0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCliForn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCliForn.ResumeLayout(False)
    Me.fmCliForn.PerformLayout()
    CType(Me.ckCliForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.edCodlsel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.fmConfermato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmConfermato.ResumeLayout(False)
    Me.fmConfermato.PerformLayout()
    CType(Me.optConfermatoEntrambe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optConfermato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optNonConfermato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsConf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsConf.ResumeLayout(False)
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri2.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.cbFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltriExt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltriExt.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli

      edContofin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054423785585, "A conto"), tabanagra)
      ckCliForn.NTSSetParam(oMenu, oApp.Tr(Me, 128535054423941796, "Raggruppa per cliente/fornitore"), "S", "N")
      opRiga2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424098007, "Entram&be"), "S")
      opRiga1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424254218, "Rig&he saldate"), "S")
      opRiga0.NTSSetParam(oMenu, oApp.Tr(Me, 128823856330773934, "&Righe aperte"), "S")
      opStampati2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424410429, "Entra&mbi"), "S")
      opStampati1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424566640, "Solo &già stampati"), "S")
      opStampati0.NTSSetParam(oMenu, oApp.Tr(Me, 128823856436712790, "So&lo non stampati"), "S")
      opStorico2.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424722851, "&Entrambi"), "S")
      opStorico1.NTSSetParam(oMenu, oApp.Tr(Me, 128535054424879062, "Ordini in &storico"), "S")
      opStorico0.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425035273, "&Ordini in essere"), "S")
      edNumordfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425191484, "A numero ordine"), "0", 9, 0, 999999999)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128535054425503906, "Serie"), CLN__STD.SerieMaxLen, True)
      edCodstag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054426128750, "Stagione"), tabstag)
      edAnnotco.NTSSetParam(oMenu, oApp.Tr(Me, 128535054426441172, "Anno"), "0", 4, 0, 2099) 'Per la gestione delle impostazioni filtro è meglio che accetti anche 0
      ckSelAnnoStag.NTSSetParam(oMenu, oApp.Tr(Me, 128535054426597383, "Seleziona Anno/Stagione"), "S", "N")
      edZona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054426753594, "Zona"), tabzone)
      edCodagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427066016, "Agente"), tabcage)
      edCoddestfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427378438, "A destinazione"), tabdestdiv)
      edCoddestfin.CliePerDestdiv = edContoini
      edCoddestini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054427690860, "Da destinazione"), tabdestdiv)
      edCoddestini.CliePerDestdiv = edContoini
      edRigafin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428159493, "A data cons. riga"), False)
      edRiga.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428315704, "Da data cons. riga"), False)
      edDataConsfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428784337, "A data cons. ordine"), False)
      edDataCons.NTSSetParam(oMenu, oApp.Tr(Me, 128535054428940548, "Da data cons. ordine"), False)
      edDatordfin.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429409181, "A data ordine"), False)
      edDatordini.NTSSetParam(oMenu, oApp.Tr(Me, 128535054429565392, "Da data ordine"), False)
      edCommecafin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054429877814, "A commessa"), tabcommess)
      edCommecaini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430658869, "Da commessa"), tabcommess)
      edContoini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430815080, "Da conto"), tabanagra)
      edNumordini.NTSSetParam(oMenu, oApp.Tr(Me, 128535054430971291, "Da numero ordine"), "0", 9, 0, 999999999)
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054431127502, "Linea/famiglia"), tabcfam, True)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128535054431283713, "Anno"), "0", 4, 0, 2099)
      cbTipork.NTSSetParam(oApp.Tr(Me, 128535054431439924, "Tipo"))
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")
      optNonConfermato.NTSSetParam(oMenu, oApp.Tr(Me, 129536562510599730, "Non confermato"), "S")
      optConfermato.NTSSetParam(oMenu, oApp.Tr(Me, 129536562653085670, "Confermato"), "S")
      optConfermatoEntrambe.NTSSetParam(oMenu, oApp.Tr(Me, 129536562710070280, "Entrambe"), "S")
      edCodlsel.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130378188922985088, "Lista selezionata"), tablsel)

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

      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 130421267561006920, "filtri Estesi"), "BSORCONF", New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTORD")
      ceFiltriExt.AggiungiTabella("MOVORD")
      ceFiltriExt.AggiungiTabella("ANAGRA")
      ceFiltriExt.InizializzaFiltri()

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

#Region "Eventi Form"
  Public Overridable Sub FRMORCONF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim lPunto As Integer
    Dim strPath As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Test per controllare l'esistenza o meno del modulo TCO 'Taglie e colori'
      '-----------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then oCleConf.bModTCO = True Else oCleConf.bModTCO = False
      '-----------------------------------------------------------------------------------------
      '--- Controlla se esiste il modulo CUSTOMER SERVICE
      '-----------------------------------------------------------------------------------------
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModAS)) Then oCleConf.bModuloAS = True Else oCleConf.bModuloAS = False
      '-----------------------------------------------------------------------------------------

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("testord", dttCampi) Then
        Me.Close()
        Return
      End If
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

      tsConf.SelectedTabPageIndex = 0
      Me.MinimumSize = Me.Size

      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo CUSTOMER SERVICE, non mette 'Impegno di commessa'
      '--- nell'elenco dei tipi ordini/impegni
      '-------------------------------------------------------
      edAnno.Text = Format(Now, "yyyy")
      edSerie.Text = ""
      edNumordini.Text = "0"
      edNumordfin.Text = "99999999"
      edContoini.Text = "0"
      edContofin.Text = "999999999"
      edDatordini.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edDatordfin.Text = IntSetDate("31/12/" & Format(Now, "yyyy"))
      edDataCons.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edDataConsfin.Text = IntSetDate("31/12/2099")
      lbCoddestini.Enabled = False : lbCoddestfin.Enabled = False
      edCoddestini.Text = "0" : edCoddestfin.Text = "999999999"
      edCoddestini.Enabled = False : edCoddestfin.Enabled = False
      edRiga.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
      edRigafin.Text = IntSetDate("31/12/2099")
      edCodagen.Text = "0"
      edZona.Text = "0"
      lbDescontoini.Text = ""
      lbDescontofin.Text = ""
      lbDesagen.Text = ""
      lbDeszona.Text = ""
      edCommecaini.Text = "0"
      edCommecafin.Text = "999999999"
      edCodcfam.Text = ""
      lbDescodcfam.Text = ""
      edCodlsel.Text = "0"
      '-----------------------------------------------------------------------------------------
      If oCleConf.bModTCO = False Then
        frStagione.Enabled = False
        ckSelAnnoStag.Enabled = False
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
        frStagione.Visible = False
        ckSelAnnoStag.Visible = False
      Else
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
      End If
      '-----------------------------------------------------------------------------------------
      oCleConf.bUsaKeyOrdWord = CBool(oMenu.GetSettingBus("Bsorgsor", "Opzioni", ".", "UsaKeyordWord", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      oCleConf.bNomeDocWordNumero = CBool(oMenu.GetSettingBus("BSORCONF", "OPZIONI", ".", "NomeDocWordNumero", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      oCleConf.nAltezzaGif = NTSCInt(oMenu.GetSettingBus("BSORCONF", "OPZIONI", ".", "AltezzaGif", "20", " ", "20"))
      lPunto = 0
      While InStr((lPunto + 1), oApp.RptDir, "\") > 0
        lPunto = InStr((lPunto + 1), oApp.RptDir, "\")
      End While
      strPath = Microsoft.VisualBasic.Left(oApp.RptDir, lPunto) & "Images\"
      '-----------------------------------------------------------------------------------------

      edSerie.Enabled = False
      ckSerie.Checked = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      Me.GctlApplicaDefaultValue()

      'Prende la struttura della tabella
      oCleConf.GetTableStructMovIfil(dttDefault)

      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")

      ComponiDatatable(dttDefault, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMORCONF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      GctlApplicaDefaultValue()
      'Necessario per ovviare al problema che non caricava i dati se si forzava un valore del combo dalla configuratore user interface
      ApplicaFiltro(NTSCInt(cbFiltro.SelectedValue))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORCONF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    Dim strTmp As String = ""
    Dim i As Integer = 0
    dsFiltri.Tables("FILTRI1").AcceptChanges()
    For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
    Next
    strTmp = strTmp.Substring(0, strTmp.Length - 1)
    oMenu.SaveSettingBus("BNORCONF", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
  End Sub

  Public Overridable Sub FRMORCONF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not TestPreStampa() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not TestPreStampa() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Try
      If Not TestPreStampa() Then Return
      '-----------------------
      'faccio comporre la query al dl
      strQueryWord = oCleConf.GetQueryStampaWord(cbTipork.SelectedValue, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, _
                                       edNumordfin.Text, edContoini.Text, edContofin.Text, edDatordini.Text, _
                                       edDatordfin.Text, edDataCons.Text, edDataConsfin.Text, edCommecaini.Text, _
                                       edCommecafin.Text, edRiga.Text, edRigafin.Text, edAnno.Text, _
                                       edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, edCodagen.Text, _
                                       edZona.Text, opStorico0.Checked, opStorico1.Checked, opStampati0.Checked, _
                                       opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, edCodcfam.Text, _
                                       oCleConf.bModTCO, ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                                       oCleConf.bUsaKeyOrdWord, ckCliForn.Checked, ceFiltriExt.GeneraQuerySQL(), _
                                       optNonConfermato.Checked, optConfermato.Checked, NTSCInt(edCodlsel.Text))
      If strQueryWord = "" Then Return

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORCONF"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = cbTipork.SelectedValue
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar1 = ckCliForn.Checked
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

      If oPar.bPar5 Then AggiornaTestord(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPdf.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante
    Dim strMessComponiFormula As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreStampa() Then Return

      'Carica i dati in memoria per gestire i filtri su Movord
      If Not oCleConf.ReportExist(edDatordini.Text, edDatordfin.Text, edDataCons.Text, _
                           edDataConsfin.Text, edRiga.Text, edRigafin.Text, cbTipork.SelectedValue, _
                           IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, edNumordfin.Text, edContoini.Text, _
                           edContofin.Text, edCommecaini.Text, edCommecafin.Text, edAnno.Text, _
                           edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, -1, _
                           edCodagen.Text, edZona.Text, opStorico0.Checked, opStorico1.Checked, _
                           opStampati0.Checked, opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, _
                           edCodcfam.Text, oCleConf.bModTCO, ckSelAnnoStag.Checked, edAnnotco.Text, _
                           edCodstag.Text, ceFiltriExt.GeneraQuerySQL(), _
                           optNonConfermato.Checked, optConfermato.Checked, NTSCInt(edCodlsel.Text)) Then Return

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleConf.GetQueryStampaPdf(cbTipork.SelectedValue, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, _
                                       edNumordfin.Text, edContoini.Text, edContofin.Text, edDatordini.Text, _
                                       edDatordfin.Text, edDataCons.Text, edDataConsfin.Text, edCommecaini.Text, _
                                       edCommecafin.Text, edRiga.Text, edRigafin.Text, edAnno.Text, _
                                       edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, edCodagen.Text, _
                                       edZona.Text, opStorico0.Checked, opStorico1.Checked, opStampati0.Checked, _
                                       opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, edCodcfam.Text, _
                                       oCleConf.bModTCO, ckSelAnnoStag.Checked, edAnnotco.Text, edCodstag.Text, _
                                       oCleConf.bUsaKeyOrdWord, ckCliForn.Checked, strQueryGetDocUnico, _
                                       ceFiltriExt.GeneraQuerySQL, _
                                       optNonConfermato.Checked, optConfermato.Checked, NTSCInt(edCodlsel.Text))
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

      'prima parte di PeSetSelectionFormula
      strQueryCrw32FileMultipli = ComponiFormula(0, strMessComponiFormula)
      If strMessComponiFormula.Length <> 0 Then
        oApp.MsgBoxInfo(strMessComponiFormula)
      Else
        strQueryCrw32FileUnico = strQueryCrw32FileMultipli & _
                                " AND {TESTORD.td_valuta} = |valuta|" & _
                                " AND {TESTORD.td_scorpo} = |scorpo|"
        strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & _
                                   " AND {MOVORD.mo_anno} = |anno|" & _
                                   " AND {MOVORD.mo_numord} = |numero|" & _
                                   " AND {MOVORD.mo_serie} = |serie|" & _
                                   " AND {MOVORD.mo_tipork} = |tipork|"
        oPar = New CLE__CLDP
        oPar.Ditta = DittaCorrente
        oPar.strPar1 = "BSORCONF"
        oPar.strPar2 = strQueryCrw32FileMultipli
        oPar.strPar3 = strQueryCrw32FileUnico
        oPar.strPar4 = strQueryGetDocMultipli
        oPar.strPar5 = strQueryGetDocUnico
        oPar.strParam = "Stampa ordini"
        oPar.ctlPar1 = Me
        oPar.ctlPar2 = dttFormule
        oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
        oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

        oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)


        '-------------------------------
        'da bnpdgenp è stato scelto di stampare anche su carta
        If oPar.bPar4 Then
          Stampa(1)
        ElseIf oPar.bPar5 Then
          '-------------------------------
          'Se faccio la stampa sopra ci pensa lui ad aggiornare la testata
          'aggiorno il flag di 'Stampato' su testord
          AggiornaTestord(0)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTipork As String = ""
    Dim strTmp As String = ""

    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edContoini.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "Q", "R", "V" : strTipork = "C"
          Case "$", "H", "O", "X", "Y" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edContoini.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edContoini.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edContoini.Text Then edContoini.NTSTextDB = NTSZOOM.strIn

      ElseIf edContofin.Focused Then
        Select Case NTSCStr(cbTipork.SelectedValue)
          Case "Q", "R", "V" : strTipork = "C"
          Case "$", "H", "O", "X", "Y" : strTipork = "F"
          Case Else : strTipork = "C"
        End Select
        SetFastZoom(edContofin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edContofin.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipork
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edContofin.Text Then edContofin.NTSTextDB = NTSZOOM.strIn

      ElseIf edCoddestini.Focused Then
        SetFastZoom(edCoddestini.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddestini.Text
        oParam.lContoCF = NTSCInt(edContoini.Text)
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddestini.Text Then edCoddestini.Text = NTSZOOM.strIn

      ElseIf edCoddestfin.Focused Then
        SetFastZoom(edCoddestfin.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddestfin.Text
        oParam.lContoCF = NTSCInt(edContoini.Text)
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddestfin.Text Then edCoddestfin.Text = NTSZOOM.strIn

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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If NTSCInt(edAnno.Text) <> 0 Then
        If Not (NTSCInt(edAnno.Text) >= 1900 And NTSCInt(edAnno.Text) <= 2099) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128599196327187500, "Se l'anno e' diverso da zero il numero deve essere compreso fa 1900 e 2099."))
          edAnno.Text = "0"
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodagen_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodagen.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodagen_Validated(NTSCInt(edCodagen.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodagen.Text = "0"
        lbDesagen.Text = ""
      Else
        lbDesagen.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodcfam_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodcfam.Text = ""
        lbDescodcfam.Text = ""
      Else
        lbDescodcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodstag_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodstag.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodstag_Validated(NTSCInt(edCodstag.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodstag.Text = "0"
        lbDescodstag.Text = ""
      Else
        lbDescodstag.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edContoini_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edContoini.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edContoini_Validated(NTSCInt(edContoini.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edContoini.Text = "0"
        lbDescontoini.Text = ""
      Else
        lbDescontoini.Text = strTmp
        If edContoini.Text <> "0" Then edContofin.Text = edContoini.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edContofin_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edContofin.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edContofin_Validated(NTSCInt(edContofin.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edContofin.Text = "999999999"
        lbDescontofin.Text = ""
      Else
        lbDescontofin.Text = strTmp
      End If

      If (NTSCInt(edContoini.Text) = NTSCInt(edContofin.Text)) And (NTSCInt(edContoini.Text) > 0) Then
        If edCoddestini.Enabled = False Then
          GctlSetVisEnab(lbCoddestini, False)
          GctlSetVisEnab(lbCoddestfin, False)
          GctlSetVisEnab(edCoddestini, False)
          GctlSetVisEnab(edCoddestfin, False)
          edCoddestini.Text = "0"
          edCoddestfin.Text = "999999999"
        End If
      Else
        edCoddestini.Text = "0"
        edCoddestfin.Text = "999999999"
        edCoddestini.Enabled = False
        edCoddestfin.Enabled = False
        lbCoddestini.Enabled = False
        lbCoddestfin.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edZona_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edZona.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edZona_Validated(NTSCInt(edZona.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edZona.Text = "0"
        lbDeszona.Text = ""
      Else
        lbDeszona.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodlsel_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodlsel.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleConf Is Nothing Then Return
      If Not oCleConf.edCodlsel_Validated(NTSCInt(edCodlsel.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodlsel.Text = "0"
        lbDescodlsel.Text = ""
      Else
        lbDescodlsel.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

#Region "Eventi CheckBox"
  Public Overridable Sub ckSelAnnoStag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSelAnnoStag.CheckedChanged
    Try
      If ckSelAnnoStag.Checked Then
        GctlSetVisEnab(lbAnnotco, False)
        GctlSetVisEnab(lbCodstag, False)
        GctlSetVisEnab(edAnnotco, False)
        GctlSetVisEnab(edCodstag, False)
        edAnnotco.Text = NTSCStr(Year(Now))
        edCodstag.Text = "0"
      Else
        lbAnnotco.Enabled = False
        lbCodstag.Enabled = False
        edAnnotco.Enabled = False
        edCodstag.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
#End Region

#Region "Eventi Pulsanti"
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
#End Region

  Public Overridable Function AggiornaTestord(ByVal nTipostampa As Integer) As Boolean
    Try
      '--------------------------------------------------------------------
      '---Se i record sono già stati stampati non fa' nessun aggiornamento
      If opStampati1.Checked = True Then Return True

      Return oCleConf.AggiornaTestord(edDatordini.Text, edDatordfin.Text, edDataCons.Text, _
                                 edDataConsfin.Text, edRiga.Text, edRigafin.Text, cbTipork.SelectedValue, _
                                 edSerie.Text, edNumordini.Text, edNumordfin.Text, edContoini.Text, _
                                 edContofin.Text, edCommecaini.Text, edCommecafin.Text, edAnno.Text, _
                                 edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, nTipostampa, _
                                 edCodagen.Text, edZona.Text, opStorico0.Checked, opStorico1.Checked, _
                                 opStampati0.Checked, opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, _
                                 edCodcfam.Text, oCleConf.bModTCO, ckSelAnnoStag.Checked, edAnnotco.Text, _
                                 edCodstag.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
        oParam.strTipo = "TESTORD"
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
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttTipork As New DataTable()
    Try
      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))


      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipork.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      'dttTipork.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipork.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipork.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipork.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      If oCleConf.bModuloAS = True Then
        dttTipork.Rows.Add(New Object() {"#", "Impegno di commessa"})
      End If
      dttTipork.AcceptChanges()

      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"
      cbTipork.SelectedValue = "Q"

      CaricaFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

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
                  oApp.MsgBoxErr(oApp.Tr(Me, 128823856513901278, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
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

  Public Overridable Function ComponiFormula(ByVal nTipostampa As Integer) As String
    Try
      Return ComponiFormula(nTipostampa, "")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiFormula(ByVal nTipostampa As Integer, ByRef strMess As String) As String
    '------------------------------------------------------------
    '
    '---Compone la stringa per la selezione dei record nei report
    '
    '---Case nTipostampa:
    '---0: Tutti i 3 tipi di record
    '---1: In Lire senza scorporo
    '---2: In Lire con scorporo
    '---3: In valuta (senza scorporo)
    '-------------------------------------------------------------
    Dim i As Integer = 0
    Dim strC As String = "", strTipork As String
    Dim strDatordini As String, strDatordfin As String
    Dim strDataCons As String, strDataConsfin As String
    Dim strRiga As String, strRigafin As String
    Dim strFiltriExt As String
    Dim dttTmp As New DataTable

    Try
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nTipostampa, strMess})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMess = NTSCStr(oIn(0))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If ckSerie.Checked = False Then edSerie.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      strFiltriExt = ceFiltriExt.GeneraQueryReport()
      '--------------------------------------------------------------------------------------------------------------
      If strFiltriExt = "" OrElse strFiltriExt.IndexOf("movord", StringComparison.CurrentCultureIgnoreCase) = -1 Then
        strDatordini = ConvDataRpt(edDatordini.Text)
        strDatordfin = ConvDataRpt(edDatordfin.Text)
        strDataCons = ConvDataRpt(edDataCons.Text)
        strDataConsfin = ConvDataRpt(edDataConsfin.Text)
        strRiga = ConvDataRpt(edRiga.Text)
        strRigafin = ConvDataRpt(edRigafin.Text)
        strTipork = NTSCStr(cbTipork.SelectedValue)
        '--------------------------------------------------------------------------------------------------------------
        strC = "{testord.codditt} = '" & DittaCorrente & "'" & _
               " And {TESTORD.td_tipork} = '" & strTipork & "'" & _
               " And {TESTORD.td_magaz2} <> {KEYORD.ko_magaz}" & _
               " And {MOVORD.mo_stasino} <> 'N'" & _
               IIf(ckSerie.Checked = True, " And {TESTORD.td_serie} = " & CStrSQL(edSerie.Text), "").ToString & _
               IIf(NTSCInt(edAnno.Text) <> 0, " And {TESTORD.td_anno} = " & edAnno.Text, "").ToString & _
               IIf(NTSCInt(edCodagen.Text) > 0, " And {TESTORD.td_codagen} = " & edCodagen.Text, "").ToString & _
               IIf(NTSCInt(edZona.Text) > 0, " And {ANAGRA.an_zona} = " & edZona.Text, "").ToString & _
               IIf(opStorico0.Checked = True, " And {TESTORD.td_flevas} = 'N'", "").ToString & _
               IIf(opStorico1.Checked = True, " And {TESTORD.td_flevas} = 'S'", "").ToString & _
               IIf(opStampati0.Checked = True, " And {TESTORD.td_flstam} = 'N'", "").ToString & _
               IIf(opStampati1.Checked = True, " And {TESTORD.td_flstam} = 'S'", "").ToString & _
               IIf(opRiga0.Checked = True, " And {MOVORD.mo_flevas} = 'C'", "").ToString & _
               IIf(opRiga1.Checked = True, " And {MOVORD.mo_flevas} = 'S'", "").ToString & _
               IIf(optNonConfermato.Checked = True, " And {TESTORD.td_confermato} = 'N'", "").ToString & _
               IIf(optConfermato.Checked = True, " And {TESTORD.td_confermato} = 'S'", "").ToString & _
               IIf(edCodcfam.Text <> "", " And {testord.td_codcfam} = " & CStrSQL(edCodcfam.Text), "").ToString
        If (NTSCInt(edContoini.Text) <> 0) Or (NTSCInt(edContofin.Text) <> 999999999) Then
          strC += " And {TESTORD.td_conto} In " & edContoini.Text & " To " & edContofin.Text
        End If
        If (NTSCInt(edNumordini.Text) <> 0) Or (NTSCInt(edNumordfin.Text) <> 999999999) Then
          strC += " And {TESTORD.td_numord} In " & edNumordini.Text & " To " & edNumordfin.Text
        End If
        If (NTSCInt(edCommecaini.Text) <> 0) Or (NTSCInt(edCommecafin.Text) <> 999999999) Then
          strC += " And {TESTORD.td_commeca} In " & edCommecaini.Text & " To " & edCommecafin.Text
        End If
        If (NTSCDate(strDatordini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(strDatordfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {TESTORD.td_datord} In " & strDatordini & " To " & strDatordfin
        End If
        If (NTSCDate(strRiga) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(strRigafin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {MOVORD.mo_datcons} In " & strRiga & " To " & strRigafin
        End If
        If (NTSCDate(strDataCons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(strDataConsfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {TESTORD.td_datcons} In " & strDataCons & " To " & strDataConsfin
        End If
        If edCoddestini.Enabled = True Then
          If NTSCInt(edCoddestini.Text) <> 0 Or (NTSCInt(edCoddestfin.Text) <> 999999999) Then
            strC += " And {TESTORD.td_coddest} In " & edCoddestini.Text & " To " & edCoddestfin.Text
          End If
        End If
        Select Case nTipostampa
          Case 1 : strC += " And {TESTORD.td_valuta} = 0 And {TESTORD.td_scorpo} = 'N'"
          Case 2 : strC += " And {TESTORD.td_valuta} = 0 And {TESTORD.td_scorpo} = 'S'"
          Case 3 : strC += " And {TESTORD.td_valuta} <> 0 And {TESTORD.td_scorpo} = 'N'"
        End Select
        '--------------------------------------------------------------------------------------------------------------
        '--- Se è attivo il modulo 'Taglie e colori' controlla se considerare i filtri specifici
        '--- su anno e/o codice stagione
        '--------------------------------------------------------------------------------------------------------------
        If (oCleConf.bModTCO = True) And (ckSelAnnoStag.Checked = True) Then
          If NTSCInt(edAnnotco.Text) > 0 Then strC += " And {testord.td_annotco} = " & edAnnotco.Text
          If NTSCInt(edCodstag.Text) > 0 Then strC += " And {testord.td_codstag} = " & edCodstag.Text
        End If
        '--------------------------------------------------------------------------------------------------------------
        If NTSCInt(edCodlsel.Text) > 0 Then
          If oCleConf.RitornaLISTSEL(NTSCInt(edCodlsel.Text), dttTmp) = True Then
            strC += " And {TESTORD.td_conto} In ["
            For i = 0 To (dttTmp.Rows.Count - 1)
              If NTSCInt(dttTmp.Rows(i)!progressivo) Mod 1000 <> 0 Then
                strC += NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
              Else
                strC = Mid(strC, 1, strC.Length - 1) & "]" & _
                  " Or {TESTORD.td_conto} In [" & _
                  NTSCStr(dttTmp.Rows(i)!lse_conto) & IIf(i < dttTmp.Rows.Count - 1, ",", "]").ToString
              End If
            Next
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------
        If strFiltriExt <> "" Then strC &= " AND " & strFiltriExt
        '--------------------------------------------------------------------------------------------------------------
      Else
        Dim nRowsCount As Integer = oCleConf.dsShared.Tables("ANAGRA").Rows.Count
        If nRowsCount <= MAX_ROWS_COUNT Then
          For Each dtrRow As DataRow In oCleConf.dsShared.Tables("ANAGRA").Rows
            strC &= "({testord.codditt} = " & ConvStrRpt(NTSCStr(dtrRow!codditt)) & _
                    " AND {testord.td_tipork} = " & ConvStrRpt(NTSCStr(dtrRow!td_tipork)) & _
                    " AND {testord.td_anno} = " & NTSCStr(dtrRow!td_anno) & _
                    " AND {testord.td_serie} = " & ConvStrRpt(NTSCStr(dtrRow!td_serie)) & _
                    " AND {testord.td_numord} = " & NTSCStr(dtrRow!td_numord) & ") OR "
          Next
          If strC.Length = 0 Then
            strC = "{testord.td_anno} = 1800" 'Così non carica nulla
          Else
            strC = strC.Remove(strC.Length - 3)
          End If
        Else
          strMess = oApp.Tr(Me, 130997626161123989, _
          "Attenzione: utilizzando filtri sul corpo del documento non è possibile prelevare più di |" & _
          MAX_ROWS_COUNT & "| documenti. La selezione attuale ne ha trovati |" & nRowsCount & "|.")
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
    Return strC
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
      strTmp = NTSCStr(oMenu.GetSettingBus("BNORCONF", "RECENT", ".", "Filtri1", "", " ", ""))
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

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
        ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------            
      If edContoini.ContainsFocus Or edContofin.ContainsFocus Then strTabName = "ANAGRACF"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Dim strTdflevas As String = ""
    Dim strFlstam As String = ""
    Dim strMoflevas As String = ""
    '----------------------------------------------------------------------------------------------------------------
    If opStorico0.Checked = True Then strTdflevas = "N"
    If opStorico1.Checked = True Then strTdflevas = "S"
    '----------------------------------------------------------------------------------------------------------------
    If opStampati0.Checked = True Then strFlstam = "N"
    If opStampati1.Checked = True Then strFlstam = "S"
    '----------------------------------------------------------------------------------------------------------------
    If opRiga0.Checked = True Then strMoflevas = "C"
    If opRiga1.Checked = True Then strMoflevas = "S"
    '----------------------------------------------------------------------------------------------------------------
    Select Case strIn.Substring(0, 1)
      Case "#"
        '------------------------------------------------------------------------------------------------------------
        '--- Variabile esposte SPECIFICHE PER PROGRAMMA
        '------------------------------------------------------------------------------------------------------------
        Select Case UCase$(Mid$(strIn, 2))
          Case "TIPORK" : Return cbTipork.SelectedValue
          Case "STORICO" : Return strTdflevas
          Case "STAMPATI" : Return strFlstam
          Case "RIGA" : Return strMoflevas
          Case Else : Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else : Return MyBase.ResolveField(strIn)
    End Select
    '----------------------------------------------------------------------------------------------------------------
    Return ""
    '----------------------------------------------------------------------------------------------------------------
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim n As Integer = 0
    Dim strTipodoc As String
    Dim bExist(3) As Boolean
    Dim bStampato As Boolean
    Dim strCaption As String
    Dim bAggiorna As Boolean
    Dim strNotatotali As String = "N"
    Dim strMessComponiFormula As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se impostato almeno uno dei filtri sulle righe, setta a "S" il flag da passare alla formula
      '--- NOTATOTALI, in modo da visualizzare nota e asterischi vicino ai totali di report
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edRiga.Text) <> New Date(1900, 1, 1) OrElse _
         NTSCDate(edRigafin.Text) <> New Date(2099, 12, 31) OrElse _
         opRiga0.Checked OrElse opRiga1.Checked Then strNotatotali = "S"
      '--------------------------------------------------------------------------------------------------------------
      strTipodoc = NTSCStr(cbTipork.SelectedValue())
      For j = 1 To 3
        If oCleConf.ReportExist(edDatordini.Text, edDatordfin.Text, edDataCons.Text, _
                                 edDataConsfin.Text, edRiga.Text, edRigafin.Text, cbTipork.SelectedValue, _
                                 IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, edNumordini.Text, edNumordfin.Text, edContoini.Text, _
                                 edContofin.Text, edCommecaini.Text, edCommecafin.Text, edAnno.Text, _
                                 edCoddestini.Enabled, edCoddestini.Text, edCoddestfin.Text, j, _
                                 edCodagen.Text, edZona.Text, opStorico0.Checked, opStorico1.Checked, _
                                 opStampati0.Checked, opStampati1.Checked, opRiga0.Checked, opRiga1.Checked, _
                                 edCodcfam.Text, oCleConf.bModTCO, ckSelAnnoStag.Checked, edAnnotco.Text, _
                                 edCodstag.Text, ceFiltriExt.GeneraQuerySQL(), _
                                 optNonConfermato.Checked, optConfermato.Checked, NTSCInt(edCodlsel.Text)) = True Then
          bExist(j) = True
          bAggiorna = False
          Select Case j
            Case 1 : strCaption = oApp.Tr(Me, 129640274285250112, "Stampa Ordini in Euro senza scorporo")
            Case 2 : strCaption = oApp.Tr(Me, 129640274355712999, "Stampa Ordini in Euro con scorporo")
            Case 3 : strCaption = oApp.Tr(Me, 129640274442424534, "Stampa Ordini in Valuta")
            Case Else : strCaption = "STAMPA ORDINI -- CONFERME D'ORDINE -- PREVENTIVI -- IMPEGNI DI TRASFERIMENTO"
          End Select
          '--------------------------------------------------
          'preparo il motore di stampa
          strCrpe = ComponiFormula(j, strMessComponiFormula)
          If strMessComponiFormula.Length <> 0 Then
            oApp.MsgBoxInfo(strMessComponiFormula)
          Else
            nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORCONF", "Reports" & j, strTipodoc, 0, nDestin, "BSORGSOR.RPT", False, strCaption, False)
            If nPjob Is Nothing Then Return Else bAggiorna = True

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (gestisce già  il multireport)
            For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NOTATOTALI", ConvStrRpt(strNotatotali))
              'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
              For n = 3 To 12
                If Trim(CStr(CType(nPjob, Array).GetValue(n, i))) <> "" Then
                  nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(n, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(n + 10, i))))
                End If
              Next n
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next
            If bAggiorna = True Then AggiornaTestord(j)
          End If
        End If
      Next

      For j = 1 To 3
        If bExist(j) = True Then bStampato = True
      Next
      If bStampato = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128538443219687405, "Non esistono dati con queste caratteristiche."))
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function TestPreStampa() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla gli intervalli
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edNumordini.Text) > NTSCInt(edNumordfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457464773159, "Il numero ordine iniziale non può essere superiore a quello finale."))
        edNumordini.Text = "0"
        edNumordfin.Text = "".PadLeft(9, "9"c)
        edNumordini.Focus()
        Return False
      End If
      If NTSCInt(edContoini.Text) > NTSCInt(edContofin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457487684579, "Il conto iniziale non può essere superiore a quello finale."))
        edContoini.Text = "0"
        edContofin.Text = "".PadLeft(9, "9"c)
        edContoini.Focus()
        Return False
      End If
      If NTSCInt(edCommecaini.Text) > NTSCInt(edCommecafin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457508881539, "La commessa iniziale non può essere superiore a quella finale."))
        edCommecaini.Text = "0"
        edCommecafin.Text = "".PadLeft(9, "9"c)
        edCommecaini.Focus()
        Return False
      End If
      If NTSCDate(edDatordini.Text) > NTSCDate(edDatordfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457527584739, "La data ordine iniziale non può essere superiore a quella finale."))
        edDatordini.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edDatordfin.Text = IntSetDate("31/12/" & Format(Now, "yyyy"))
        edDatordini.Focus()
        Return False
      End If
      If NTSCDate(edDataCons.Text) > NTSCDate(edDataConsfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457544885199, "La data consegna ordine iniziale non può essere superiore a quella finale."))
        edDataCons.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edDataConsfin.Text = IntSetDate("31/12/2099")
        edDataCons.Focus()
        Return False
      End If
      If NTSCDate(edRiga.Text) > NTSCDate(edRigafin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128538457582915039, "La data consegna riga iniziale non può essere superiore a quella finale."))
        edRiga.Text = IntSetDate("01/01/" & Format(Now, "yyyy"))
        edRigafin.Text = IntSetDate("31/12/2099")
        edRiga.Focus()
        Return False
      End If
      If edCoddestini.Enabled Then
        If NTSCInt(edCoddestini.Text) > NTSCInt(edCoddestfin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128538457600527219, "Il codice destinazione iniziale non può essere superiore a quello finale."))
          edCoddestini.Text = "0"
          edCoddestfin.Text = "999999999"
          edCoddestini.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckFiltri1() Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "Gestione filtri"
  Public Overridable Sub CaricaFiltri()
    Dim dttTmp As New DataTable
    Try
      oCleConf.CaricaFiltri(dttTmp)

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
      oCleConf.GetTableStructMovIfil(dttCampiForm)

      dttCampiForm.Columns.Add("xx_descr")
      dttCampiForm.Columns.Add("xx_info")
      dttCampiForm.Columns.Add("xx_tipo")

      'Compongo il datatable con i campi da passare al programma per la gestione dei dati
      If Not ComponiDatatable(dttCampiForm, Me) Then Return

      'Riempie le colonne mancanti
      For z As Integer = 0 To dttCampiForm.Rows.Count - 1
        dttCampiForm.Rows(z)!mo_child = "BNORConf"
        dttCampiForm.Rows(z)!mo_form = "FRMORConf"
        dttCampiForm.Rows(z)!mo_locked = "N"
        dttCampiForm.Rows(z)!mo_codifil = NTSCInt(cbFiltro.SelectedValue)
      Next

      'Avvia il programma
      oPar.ctlPar1 = dttCampiForm
      oPar.strPar1 = "BNORConf"
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
    Dim dtrRow() As DataRow
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
            Case "grFiltri1"
              For z As Integer = 0 To grvFiltri1.RowCount - 1
                If NTSCStr(grvFiltri1.GetDataRow(z)!xx_nome) = "." Then Continue For

                dttCampiForm.Rows.Add()

                dtrRow = dttCampi.Select("cb_nomcampo = " & CStrSQL(grvFiltri1.GetDataRow(z)!xx_nome))
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFiltri1"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = grvFiltri1.GetRowCellValue(z, "xx_nome")
                  !mo_colvalue = "xx_valoreda"
                  !xx_descr = oApp.Tr(Me, 129182143688273058, "Altri filtri testata - ") & grvFiltri1.GetRowCellDisplayText(z, "xx_nome") & " (Da)"
                  !mo_valore = grvFiltri1.GetRowCellValue(z, "xx_valoreda")
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow(0)!cb_tipocampo
                End With

                dttCampiForm.Rows.Add()
                With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
                  !mo_control = "grFiltri1"
                  !mo_colkeyname = "xx_nome"
                  !mo_colkeyvalue = grvFiltri1.GetRowCellValue(z, "xx_nome")
                  !mo_colvalue = "xx_valorea"
                  !xx_descr = oApp.Tr(Me, 129181522451445167, "Altri filtri testata - ") & grvFiltri1.GetRowCellDisplayText(z, "xx_nome") & " (A)"
                  !mo_valore = grvFiltri1.GetRowCellValue(z, "xx_valorea")
                  !xx_tipo = "NTSGridColumn"
                  !mo_ordin = dttCampiForm.Rows.Count
                  !xx_info = dtrRow(0)!cb_tipocampo
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
        If Not oCleConf.LeggiFiltro(lCod, "BNORCONF", "FRMORCONF", dttPersForm) Then Return False
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
              Dim dtrTmp() As DataRow = dttCampi.Select("cb_nomcampo = " & CStrSQL(dttControl.Rows(z)!mo_colkeyvalue))
              If NTSCInt(dtrTmp(0)!cb_tipocampo) = 8 Then ' 8 = data
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

