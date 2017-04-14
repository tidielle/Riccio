Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMPRCAGE
#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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
  Public oCleCage As CLEPRCAGE
  Public oCallParams As CLE__CLDP
  Public dsCage As DataSet
  Public dcCage As New BindingSource
  Public bNuovo As Boolean
  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents tsCage As NTSInformatica.NTSTabControl
  Public WithEvents tsProg As NTSInformatica.NTSTabPage
  Public WithEvents tsPeriodo As NTSInformatica.NTSTabPage
  Public WithEvents tsPerprov As NTSInformatica.NTSTabPage
  Public WithEvents pnAgente As NTSInformatica.NTSPanel
  Public WithEvents pnPeriodo As NTSInformatica.NTSPanel
  Public WithEvents pnProg As NTSInformatica.NTSPanel
  Public WithEvents pnPerprovv As NTSInformatica.NTSPanel
  Public WithEvents tsAltri As NTSInformatica.NTSTabPage
  Public WithEvents pnAltri As NTSInformatica.NTSPanel
  Public WithEvents lbDessup As NTSInformatica.NTSLabel
  Public WithEvents lbDesAnag As NTSInformatica.NTSLabel
  Public WithEvents cbTb_tippro As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tiptab As NTSInformatica.NTSComboBox
  Public WithEvents edTb_codsup As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_giorni As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_perprra As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_codforn As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_descage As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_codcage As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCalcolo As NTSInformatica.NTSLabel
  Public WithEvents lbDecor As NTSInformatica.NTSLabel
  Public WithEvents lbNominativo As NTSInformatica.NTSLabel
  Public WithEvents lbCodsup As NTSInformatica.NTSLabel
  Public WithEvents lbMod As NTSInformatica.NTSLabel
  Public WithEvents lbPerc As NTSInformatica.NTSLabel
  Public WithEvents lbCodanag As NTSInformatica.NTSLabel
  Public WithEvents lbCodcage As NTSInformatica.NTSLabel
  Public WithEvents lbImpon As NTSInformatica.NTSLabel
  Public WithEvents lbInca As NTSInformatica.NTSLabel
  Public WithEvents lbIntegra As NTSInformatica.NTSLabel
  Public WithEvents lbInden As NTSInformatica.NTSLabel
  Public WithEvents lbEnasaditt As NTSInformatica.NTSLabel
  Public WithEvents lbProvv As NTSInformatica.NTSLabel
  Public WithEvents lbEnasa As NTSInformatica.NTSLabel
  Public WithEvents lbUltaggpro As NTSInformatica.NTSLabel
  Public WithEvents edTb_minrecu As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_incapro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_casspro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_enagpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_endtpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_propro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_fatpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_datpro As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_incaper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_cassper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_enagper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_endtper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_proper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_fatper As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_datper As NTSInformatica.NTSTextBoxData
  Public WithEvents lbImpper As NTSInformatica.NTSLabel
  Public WithEvents lbIncaper As NTSInformatica.NTSLabel
  Public WithEvents lbIndeper As NTSInformatica.NTSLabel
  Public WithEvents lbEnasadittper As NTSInformatica.NTSLabel
  Public WithEvents lbProvper As NTSInformatica.NTSLabel
  Public WithEvents lbEnasaPer As NTSInformatica.NTSLabel
  Public WithEvents lbUltaggper As NTSInformatica.NTSLabel
  Public WithEvents lbDatfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatini As NTSInformatica.NTSLabel
  Public WithEvents lbInde As NTSInformatica.NTSLabel
  Public WithEvents lbCodtabena As NTSInformatica.NTSLabel
  Public WithEvents lbCodcauinca As NTSInformatica.NTSLabel
  Public WithEvents edTb_codcaui As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_codtben As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_perinca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_datiniz As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_datfin As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDestabena As NTSInformatica.NTSLabel
  Public WithEvents lbDescausa As NTSInformatica.NTSLabel
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAzzpro As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAzzper As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbContratti As NTSInformatica.NTSBarButtonItem
  Public WithEvents ckTb_webvis As NTSInformatica.NTSCheckBox
  Public WithEvents ceProvv As NTSInformatica.NTSXXPROV
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRCAGE", "BEPRCAGE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128626743371406250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCage = CType(oTmp, CLEPRCAGE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRCAGE", strRemoteServer, strRemotePort)
    AddHandler oCleCage.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCage.Init(oApp, oScript, oMenu.oCleComm, "TABCAGE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      cbTb_tippro.NTSSetParam(oApp.Tr(Me, 128726175105141790, "Mod. corresponsione"))
      cbTb_tiptab.NTSSetParam(oApp.Tr(Me, 128726175105297556, "Percentuali riferite a"))
      edTb_codsup.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726175105453322, "Cod. agente superiore"), tabcage)
      edTb_giorni.NTSSetParam(oMenu, oApp.Tr(Me, 128726175105609088, "Decorrenza matur. provv. (gg)"), "0", 3, 0, 999)
      edTb_perprra.NTSSetParam(oMenu, oApp.Tr(Me, 128726175105764854, "% provvigione calcolo R/A"), "0.00", 4, 0, 100)
      edTb_codforn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726175105920620, "Codice anagrafico"), tabanagraf)
      edTb_descage.NTSSetParam(oMenu, oApp.Tr(Me, 128726175106076386, "Nominativo"), 50)
      edTb_descage.NTSForzaVisZoom = True
      edTb_codcage.NTSSetParam(oMenu, oApp.Tr(Me, 128726175106232152, "Codice Agente"), "0", 4, 0, 9999)
      edTb_minrecu.NTSSetParam(oMenu, oApp.Tr(Me, 128726175107478280, "Integrazione al minimo"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_incapro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175107634046, "Incassato (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_casspro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175107789812, "Indennità di cassa (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_enagpro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175107945578, "Enasarco agente (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_endtpro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175108101344, "Enasarco ditta (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_propro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175108257110, "Provvigioni (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_fatpro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175108412876, "Imponibile (prog.)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_datpro.NTSSetParam(oMenu, oApp.Tr(Me, 128726175108568642, "Data ultimo agg. (prog.)"), False)
      edTb_incaper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175109814770, "Incassato (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_cassper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175109970536, "Indennità di cassa (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_enagper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175110126302, "Enasarco agente (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_endtper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175110282068, "Enasarco ditta (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_proper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175110437834, "Provvigioni (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_fatper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175110593600, "Imponibile (periodo)"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_datper.NTSSetParam(oMenu, oApp.Tr(Me, 128726175110749366, "Data ultimo agg. (periodo)"), False)
      edTb_codcaui.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726175114955048, "Cod. causale incassato"), tabcauc)
      edTb_codtben.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726175115110814, "Cod. tab. enasarco"), tabenas)
      edTb_perinca.NTSSetParam(oMenu, oApp.Tr(Me, 128726175115266580, "% indennità di cassa"), "0.00", 4, 0, 100)
      edTb_datiniz.NTSSetParam(oMenu, oApp.Tr(Me, 128726175115422346, "Data inizio mandato"), True)
      edTb_datfin.NTSSetParam(oMenu, oApp.Tr(Me, 128726175115578112, "Data fine mandato"), True)
      ckTb_webvis.NTSSetParam(oMenu, oApp.Tr(Me, 130415294122244969, "Agente visibile dall'applicazione esterna"), "S", "N")

      ceProvv.NTSSetParam(oMenu, "Provvigioni", "BNPRCAGE", Nothing)
      ceProvv.tlbProvEsci.Enabled = False
      ceProvv.TipoProvv = 0
      ceProvv.PerCodart = ""
      ceProvv.PerConto = 0
      ceProvv.PerClasseCli = 0
      ceProvv.PerClasseArt = 0

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
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRCAGE))
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
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbAzzpro = New NTSInformatica.NTSBarButtonItem
    Me.tlbAzzper = New NTSInformatica.NTSBarButtonItem
    Me.tlbContratti = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.ceProvv = New NTSInformatica.NTSXXPROV
    Me.tsCage = New NTSInformatica.NTSTabControl
    Me.tsAltri = New NTSInformatica.NTSTabPage
    Me.pnAltri = New NTSInformatica.NTSPanel
    Me.ckTb_webvis = New NTSInformatica.NTSCheckBox
    Me.lbDestabena = New NTSInformatica.NTSLabel
    Me.lbDescausa = New NTSInformatica.NTSLabel
    Me.edTb_codcaui = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codtben = New NTSInformatica.NTSTextBoxNum
    Me.edTb_perinca = New NTSInformatica.NTSTextBoxNum
    Me.edTb_datiniz = New NTSInformatica.NTSTextBoxData
    Me.edTb_datfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatfin = New NTSInformatica.NTSLabel
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.lbInde = New NTSInformatica.NTSLabel
    Me.lbCodtabena = New NTSInformatica.NTSLabel
    Me.lbCodcauinca = New NTSInformatica.NTSLabel
    Me.tsProg = New NTSInformatica.NTSTabPage
    Me.pnProg = New NTSInformatica.NTSPanel
    Me.edTb_minrecu = New NTSInformatica.NTSTextBoxNum
    Me.edTb_incapro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_casspro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_enagpro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_endtpro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_propro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_fatpro = New NTSInformatica.NTSTextBoxNum
    Me.edTb_datpro = New NTSInformatica.NTSTextBoxData
    Me.lbImpon = New NTSInformatica.NTSLabel
    Me.lbInca = New NTSInformatica.NTSLabel
    Me.lbIntegra = New NTSInformatica.NTSLabel
    Me.lbInden = New NTSInformatica.NTSLabel
    Me.lbEnasaditt = New NTSInformatica.NTSLabel
    Me.lbProvv = New NTSInformatica.NTSLabel
    Me.lbEnasa = New NTSInformatica.NTSLabel
    Me.lbUltaggpro = New NTSInformatica.NTSLabel
    Me.tsPeriodo = New NTSInformatica.NTSTabPage
    Me.pnPeriodo = New NTSInformatica.NTSPanel
    Me.edTb_incaper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_cassper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_enagper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_endtper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_proper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_fatper = New NTSInformatica.NTSTextBoxNum
    Me.edTb_datper = New NTSInformatica.NTSTextBoxData
    Me.lbImpper = New NTSInformatica.NTSLabel
    Me.lbIncaper = New NTSInformatica.NTSLabel
    Me.lbIndeper = New NTSInformatica.NTSLabel
    Me.lbEnasadittper = New NTSInformatica.NTSLabel
    Me.lbProvper = New NTSInformatica.NTSLabel
    Me.lbEnasaPer = New NTSInformatica.NTSLabel
    Me.lbUltaggper = New NTSInformatica.NTSLabel
    Me.tsPerprov = New NTSInformatica.NTSTabPage
    Me.pnPerprovv = New NTSInformatica.NTSPanel
    Me.pnAgente = New NTSInformatica.NTSPanel
    Me.lbDessup = New NTSInformatica.NTSLabel
    Me.lbDesAnag = New NTSInformatica.NTSLabel
    Me.cbTb_tippro = New NTSInformatica.NTSComboBox
    Me.cbTb_tiptab = New NTSInformatica.NTSComboBox
    Me.edTb_codsup = New NTSInformatica.NTSTextBoxNum
    Me.edTb_giorni = New NTSInformatica.NTSTextBoxNum
    Me.edTb_perprra = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codforn = New NTSInformatica.NTSTextBoxNum
    Me.edTb_descage = New NTSInformatica.NTSTextBoxStr
    Me.edTb_codcage = New NTSInformatica.NTSTextBoxNum
    Me.lbCalcolo = New NTSInformatica.NTSLabel
    Me.lbDecor = New NTSInformatica.NTSLabel
    Me.lbNominativo = New NTSInformatica.NTSLabel
    Me.lbCodsup = New NTSInformatica.NTSLabel
    Me.lbMod = New NTSInformatica.NTSLabel
    Me.lbPerc = New NTSInformatica.NTSLabel
    Me.lbCodanag = New NTSInformatica.NTSLabel
    Me.lbCodcage = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsCage, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsCage.SuspendLayout()
    Me.tsAltri.SuspendLayout()
    CType(Me.pnAltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAltri.SuspendLayout()
    CType(Me.ckTb_webvis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcaui.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codtben.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_perinca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datiniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsProg.SuspendLayout()
    CType(Me.pnProg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProg.SuspendLayout()
    CType(Me.edTb_minrecu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_incapro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_casspro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_enagpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_endtpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_propro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_fatpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsPeriodo.SuspendLayout()
    CType(Me.pnPeriodo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPeriodo.SuspendLayout()
    CType(Me.edTb_incaper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cassper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_enagper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_endtper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_proper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_fatper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_datper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsPerprov.SuspendLayout()
    CType(Me.pnPerprovv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPerprovv.SuspendLayout()
    CType(Me.pnAgente, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAgente.SuspendLayout()
    CType(Me.cbTb_tippro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tiptab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codsup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_perprra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codforn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbNuovo, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbAzzpro, Me.tlbAzzper, Me.tlbZoom, Me.tlbContratti})
    Me.NtsBarManager1.MaxItemId = 28
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 18
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
    Me.tlbZoom.Id = 26
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 20
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 21
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Sucessivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 22
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 23
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAzzpro, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAzzper), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbContratti, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbAzzpro
    '
    Me.tlbAzzpro.Caption = "Azzera progressivi"
    Me.tlbAzzpro.GlyphPath = ""
    Me.tlbAzzpro.Id = 24
    Me.tlbAzzpro.Name = "tlbAzzpro"
    Me.tlbAzzpro.Visible = True
    '
    'tlbAzzper
    '
    Me.tlbAzzper.Caption = "Azzera periodo"
    Me.tlbAzzper.GlyphPath = ""
    Me.tlbAzzper.Id = 25
    Me.tlbAzzper.Name = "tlbAzzper"
    Me.tlbAzzper.Visible = True
    '
    'tlbContratti
    '
    Me.tlbContratti.Caption = "Contratti"
    Me.tlbContratti.GlyphPath = ""
    Me.tlbContratti.Id = 27
    Me.tlbContratti.Name = "tlbContratti"
    Me.tlbContratti.Visible = True
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
    'ceProvv
    '
    Me.ceProvv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceProvv.GridColumn1_465_17 = Nothing
    Me.ceProvv.GridColumn1_715_16 = Nothing
    Me.ceProvv.Location = New System.Drawing.Point(0, 0)
    Me.ceProvv.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceProvv.Name = "ceProvv"
    Me.ceProvv.PerClasseArt = 0
    Me.ceProvv.PerClasseCli = 0
    Me.ceProvv.PerCodart = ""
    Me.ceProvv.PerCodartRoot = ""
    Me.ceProvv.PerCodcage = 0
    Me.ceProvv.PerConto = 0
    Me.ceProvv.Size = New System.Drawing.Size(547, 294)
    Me.ceProvv.strNomeCampo = ""
    Me.ceProvv.TabIndex = 1
    Me.ceProvv.TipoProvv = 0
    '
    'tsCage
    '
    Me.tsCage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsCage.Location = New System.Drawing.Point(0, 170)
    Me.tsCage.Name = "tsCage"
    Me.tsCage.SelectedTabPage = Me.tsProg
    Me.tsCage.Size = New System.Drawing.Size(556, 324)
    Me.tsCage.TabIndex = 4
    Me.tsCage.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tsProg, Me.tsPeriodo, Me.tsPerprov, Me.tsAltri})
    Me.tsCage.Text = "NtsTabControl1"
    '
    'tsAltri
    '
    Me.tsAltri.AllowDrop = True
    Me.tsAltri.Controls.Add(Me.pnAltri)
    Me.tsAltri.Enable = True
    Me.tsAltri.Name = "tsAltri"
    Me.tsAltri.Size = New System.Drawing.Size(547, 294)
    Me.tsAltri.Text = "Altri dati"
    '
    'pnAltri
    '
    Me.pnAltri.AllowDrop = True
    Me.pnAltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAltri.Appearance.Options.UseBackColor = True
    Me.pnAltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAltri.Controls.Add(Me.ckTb_webvis)
    Me.pnAltri.Controls.Add(Me.lbDestabena)
    Me.pnAltri.Controls.Add(Me.lbDescausa)
    Me.pnAltri.Controls.Add(Me.edTb_codcaui)
    Me.pnAltri.Controls.Add(Me.edTb_codtben)
    Me.pnAltri.Controls.Add(Me.edTb_perinca)
    Me.pnAltri.Controls.Add(Me.edTb_datiniz)
    Me.pnAltri.Controls.Add(Me.edTb_datfin)
    Me.pnAltri.Controls.Add(Me.lbDatfin)
    Me.pnAltri.Controls.Add(Me.lbDatini)
    Me.pnAltri.Controls.Add(Me.lbInde)
    Me.pnAltri.Controls.Add(Me.lbCodtabena)
    Me.pnAltri.Controls.Add(Me.lbCodcauinca)
    Me.pnAltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAltri.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAltri.Location = New System.Drawing.Point(0, 0)
    Me.pnAltri.Name = "pnAltri"
    Me.pnAltri.NTSActiveTrasparency = True
    Me.pnAltri.Size = New System.Drawing.Size(547, 294)
    Me.pnAltri.TabIndex = 0
    Me.pnAltri.Text = "NtsPanel1"
    '
    'ckTb_webvis
    '
    Me.ckTb_webvis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_webvis.Location = New System.Drawing.Point(12, 174)
    Me.ckTb_webvis.Name = "ckTb_webvis"
    Me.ckTb_webvis.NTSCheckValue = "S"
    Me.ckTb_webvis.NTSUnCheckValue = "N"
    Me.ckTb_webvis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_webvis.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_webvis.Properties.AutoHeight = False
    Me.ckTb_webvis.Properties.Caption = "Agente visibile dall'applicazione esterna"
    Me.ckTb_webvis.Size = New System.Drawing.Size(216, 19)
    Me.ckTb_webvis.TabIndex = 684
    '
    'lbDestabena
    '
    Me.lbDestabena.BackColor = System.Drawing.Color.Transparent
    Me.lbDestabena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDestabena.Location = New System.Drawing.Point(191, 70)
    Me.lbDestabena.Name = "lbDestabena"
    Me.lbDestabena.NTSDbField = ""
    Me.lbDestabena.Size = New System.Drawing.Size(342, 20)
    Me.lbDestabena.TabIndex = 19
    Me.lbDestabena.Tooltip = ""
    Me.lbDestabena.UseMnemonic = False
    '
    'lbDescausa
    '
    Me.lbDescausa.BackColor = System.Drawing.Color.Transparent
    Me.lbDescausa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescausa.Location = New System.Drawing.Point(191, 44)
    Me.lbDescausa.Name = "lbDescausa"
    Me.lbDescausa.NTSDbField = ""
    Me.lbDescausa.Size = New System.Drawing.Size(342, 20)
    Me.lbDescausa.TabIndex = 18
    Me.lbDescausa.Tooltip = ""
    Me.lbDescausa.UseMnemonic = False
    '
    'edTb_codcaui
    '
    Me.edTb_codcaui.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcaui.EditValue = "0"
    Me.edTb_codcaui.Location = New System.Drawing.Point(128, 44)
    Me.edTb_codcaui.Name = "edTb_codcaui"
    Me.edTb_codcaui.NTSDbField = ""
    Me.edTb_codcaui.NTSFormat = "0"
    Me.edTb_codcaui.NTSForzaVisZoom = False
    Me.edTb_codcaui.NTSOldValue = ""
    Me.edTb_codcaui.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcaui.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcaui.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcaui.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcaui.Properties.AutoHeight = False
    Me.edTb_codcaui.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcaui.Properties.MaxLength = 65536
    Me.edTb_codcaui.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcaui.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codcaui.TabIndex = 9
    '
    'edTb_codtben
    '
    Me.edTb_codtben.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_codtben.EditValue = "0"
    Me.edTb_codtben.Location = New System.Drawing.Point(128, 70)
    Me.edTb_codtben.Name = "edTb_codtben"
    Me.edTb_codtben.NTSDbField = ""
    Me.edTb_codtben.NTSFormat = "0"
    Me.edTb_codtben.NTSForzaVisZoom = False
    Me.edTb_codtben.NTSOldValue = ""
    Me.edTb_codtben.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codtben.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codtben.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codtben.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codtben.Properties.AutoHeight = False
    Me.edTb_codtben.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codtben.Properties.MaxLength = 65536
    Me.edTb_codtben.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codtben.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codtben.TabIndex = 8
    '
    'edTb_perinca
    '
    Me.edTb_perinca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_perinca.EditValue = "0"
    Me.edTb_perinca.Location = New System.Drawing.Point(128, 96)
    Me.edTb_perinca.Name = "edTb_perinca"
    Me.edTb_perinca.NTSDbField = ""
    Me.edTb_perinca.NTSFormat = "0"
    Me.edTb_perinca.NTSForzaVisZoom = False
    Me.edTb_perinca.NTSOldValue = ""
    Me.edTb_perinca.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_perinca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_perinca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_perinca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_perinca.Properties.AutoHeight = False
    Me.edTb_perinca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_perinca.Properties.MaxLength = 65536
    Me.edTb_perinca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_perinca.Size = New System.Drawing.Size(57, 20)
    Me.edTb_perinca.TabIndex = 7
    '
    'edTb_datiniz
    '
    Me.edTb_datiniz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datiniz.Location = New System.Drawing.Point(128, 122)
    Me.edTb_datiniz.Name = "edTb_datiniz"
    Me.edTb_datiniz.NTSDbField = ""
    Me.edTb_datiniz.NTSForzaVisZoom = False
    Me.edTb_datiniz.NTSOldValue = ""
    Me.edTb_datiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datiniz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datiniz.Properties.AutoHeight = False
    Me.edTb_datiniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datiniz.Properties.MaxLength = 65536
    Me.edTb_datiniz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datiniz.Size = New System.Drawing.Size(100, 20)
    Me.edTb_datiniz.TabIndex = 6
    '
    'edTb_datfin
    '
    Me.edTb_datfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datfin.Location = New System.Drawing.Point(128, 148)
    Me.edTb_datfin.Name = "edTb_datfin"
    Me.edTb_datfin.NTSDbField = ""
    Me.edTb_datfin.NTSForzaVisZoom = False
    Me.edTb_datfin.NTSOldValue = ""
    Me.edTb_datfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datfin.Properties.AutoHeight = False
    Me.edTb_datfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datfin.Properties.MaxLength = 65536
    Me.edTb_datfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datfin.Size = New System.Drawing.Size(100, 20)
    Me.edTb_datfin.TabIndex = 5
    '
    'lbDatfin
    '
    Me.lbDatfin.AutoSize = True
    Me.lbDatfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfin.Location = New System.Drawing.Point(9, 151)
    Me.lbDatfin.Name = "lbDatfin"
    Me.lbDatfin.NTSDbField = ""
    Me.lbDatfin.Size = New System.Drawing.Size(96, 13)
    Me.lbDatfin.TabIndex = 4
    Me.lbDatfin.Text = "Data fine mandato"
    Me.lbDatfin.Tooltip = ""
    Me.lbDatfin.UseMnemonic = False
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(9, 125)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(101, 13)
    Me.lbDatini.TabIndex = 3
    Me.lbDatini.Text = "Data inizio mandato"
    Me.lbDatini.Tooltip = ""
    Me.lbDatini.UseMnemonic = False
    '
    'lbInde
    '
    Me.lbInde.AutoSize = True
    Me.lbInde.BackColor = System.Drawing.Color.Transparent
    Me.lbInde.Location = New System.Drawing.Point(9, 99)
    Me.lbInde.Name = "lbInde"
    Me.lbInde.NTSDbField = ""
    Me.lbInde.Size = New System.Drawing.Size(108, 13)
    Me.lbInde.TabIndex = 2
    Me.lbInde.Text = "% Indennità di cassa"
    Me.lbInde.Tooltip = ""
    Me.lbInde.UseMnemonic = False
    '
    'lbCodtabena
    '
    Me.lbCodtabena.AutoSize = True
    Me.lbCodtabena.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtabena.Location = New System.Drawing.Point(9, 73)
    Me.lbCodtabena.Name = "lbCodtabena"
    Me.lbCodtabena.NTSDbField = ""
    Me.lbCodtabena.Size = New System.Drawing.Size(100, 13)
    Me.lbCodtabena.TabIndex = 1
    Me.lbCodtabena.Text = "Cod. tab. enasarco"
    Me.lbCodtabena.Tooltip = ""
    Me.lbCodtabena.UseMnemonic = False
    '
    'lbCodcauinca
    '
    Me.lbCodcauinca.AutoSize = True
    Me.lbCodcauinca.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcauinca.Location = New System.Drawing.Point(9, 47)
    Me.lbCodcauinca.Name = "lbCodcauinca"
    Me.lbCodcauinca.NTSDbField = ""
    Me.lbCodcauinca.Size = New System.Drawing.Size(107, 13)
    Me.lbCodcauinca.TabIndex = 0
    Me.lbCodcauinca.Text = "Cod. causale incasso"
    Me.lbCodcauinca.Tooltip = ""
    Me.lbCodcauinca.UseMnemonic = False
    '
    'tsProg
    '
    Me.tsProg.AllowDrop = True
    Me.tsProg.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.tsProg.Appearance.Header.Options.UseFont = True
    Me.tsProg.Controls.Add(Me.pnProg)
    Me.tsProg.Enable = True
    Me.tsProg.Name = "tsProg"
    Me.tsProg.Size = New System.Drawing.Size(547, 294)
    Me.tsProg.Text = "Progressivi"
    '
    'pnProg
    '
    Me.pnProg.AllowDrop = True
    Me.pnProg.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProg.Appearance.Options.UseBackColor = True
    Me.pnProg.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProg.Controls.Add(Me.edTb_minrecu)
    Me.pnProg.Controls.Add(Me.edTb_incapro)
    Me.pnProg.Controls.Add(Me.edTb_casspro)
    Me.pnProg.Controls.Add(Me.edTb_enagpro)
    Me.pnProg.Controls.Add(Me.edTb_endtpro)
    Me.pnProg.Controls.Add(Me.edTb_propro)
    Me.pnProg.Controls.Add(Me.edTb_fatpro)
    Me.pnProg.Controls.Add(Me.edTb_datpro)
    Me.pnProg.Controls.Add(Me.lbImpon)
    Me.pnProg.Controls.Add(Me.lbInca)
    Me.pnProg.Controls.Add(Me.lbIntegra)
    Me.pnProg.Controls.Add(Me.lbInden)
    Me.pnProg.Controls.Add(Me.lbEnasaditt)
    Me.pnProg.Controls.Add(Me.lbProvv)
    Me.pnProg.Controls.Add(Me.lbEnasa)
    Me.pnProg.Controls.Add(Me.lbUltaggpro)
    Me.pnProg.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnProg.Location = New System.Drawing.Point(0, 0)
    Me.pnProg.Name = "pnProg"
    Me.pnProg.NTSActiveTrasparency = True
    Me.pnProg.Size = New System.Drawing.Size(547, 294)
    Me.pnProg.TabIndex = 0
    Me.pnProg.Text = "NtsPanel1"
    '
    'edTb_minrecu
    '
    Me.edTb_minrecu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_minrecu.EditValue = "0"
    Me.edTb_minrecu.Location = New System.Drawing.Point(433, 122)
    Me.edTb_minrecu.Name = "edTb_minrecu"
    Me.edTb_minrecu.NTSDbField = ""
    Me.edTb_minrecu.NTSFormat = "0"
    Me.edTb_minrecu.NTSForzaVisZoom = False
    Me.edTb_minrecu.NTSOldValue = ""
    Me.edTb_minrecu.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_minrecu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_minrecu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_minrecu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_minrecu.Properties.AutoHeight = False
    Me.edTb_minrecu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_minrecu.Properties.MaxLength = 65536
    Me.edTb_minrecu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_minrecu.Size = New System.Drawing.Size(100, 20)
    Me.edTb_minrecu.TabIndex = 15
    '
    'edTb_incapro
    '
    Me.edTb_incapro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_incapro.EditValue = "0"
    Me.edTb_incapro.Location = New System.Drawing.Point(433, 70)
    Me.edTb_incapro.Name = "edTb_incapro"
    Me.edTb_incapro.NTSDbField = ""
    Me.edTb_incapro.NTSFormat = "0"
    Me.edTb_incapro.NTSForzaVisZoom = False
    Me.edTb_incapro.NTSOldValue = ""
    Me.edTb_incapro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_incapro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_incapro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_incapro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_incapro.Properties.AutoHeight = False
    Me.edTb_incapro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_incapro.Properties.MaxLength = 65536
    Me.edTb_incapro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_incapro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_incapro.TabIndex = 14
    '
    'edTb_casspro
    '
    Me.edTb_casspro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_casspro.EditValue = "0"
    Me.edTb_casspro.Location = New System.Drawing.Point(433, 96)
    Me.edTb_casspro.Name = "edTb_casspro"
    Me.edTb_casspro.NTSDbField = ""
    Me.edTb_casspro.NTSFormat = "0"
    Me.edTb_casspro.NTSForzaVisZoom = False
    Me.edTb_casspro.NTSOldValue = ""
    Me.edTb_casspro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_casspro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_casspro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_casspro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_casspro.Properties.AutoHeight = False
    Me.edTb_casspro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_casspro.Properties.MaxLength = 65536
    Me.edTb_casspro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_casspro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_casspro.TabIndex = 13
    '
    'edTb_enagpro
    '
    Me.edTb_enagpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_enagpro.EditValue = "0"
    Me.edTb_enagpro.Location = New System.Drawing.Point(433, 44)
    Me.edTb_enagpro.Name = "edTb_enagpro"
    Me.edTb_enagpro.NTSDbField = ""
    Me.edTb_enagpro.NTSFormat = "0"
    Me.edTb_enagpro.NTSForzaVisZoom = False
    Me.edTb_enagpro.NTSOldValue = ""
    Me.edTb_enagpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_enagpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_enagpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_enagpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_enagpro.Properties.AutoHeight = False
    Me.edTb_enagpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_enagpro.Properties.MaxLength = 65536
    Me.edTb_enagpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_enagpro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_enagpro.TabIndex = 12
    '
    'edTb_endtpro
    '
    Me.edTb_endtpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_endtpro.EditValue = "0"
    Me.edTb_endtpro.Location = New System.Drawing.Point(128, 122)
    Me.edTb_endtpro.Name = "edTb_endtpro"
    Me.edTb_endtpro.NTSDbField = ""
    Me.edTb_endtpro.NTSFormat = "0"
    Me.edTb_endtpro.NTSForzaVisZoom = False
    Me.edTb_endtpro.NTSOldValue = ""
    Me.edTb_endtpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_endtpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_endtpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_endtpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_endtpro.Properties.AutoHeight = False
    Me.edTb_endtpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_endtpro.Properties.MaxLength = 65536
    Me.edTb_endtpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_endtpro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_endtpro.TabIndex = 11
    '
    'edTb_propro
    '
    Me.edTb_propro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_propro.EditValue = "0"
    Me.edTb_propro.Location = New System.Drawing.Point(128, 96)
    Me.edTb_propro.Name = "edTb_propro"
    Me.edTb_propro.NTSDbField = ""
    Me.edTb_propro.NTSFormat = "0"
    Me.edTb_propro.NTSForzaVisZoom = False
    Me.edTb_propro.NTSOldValue = ""
    Me.edTb_propro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_propro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_propro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_propro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_propro.Properties.AutoHeight = False
    Me.edTb_propro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_propro.Properties.MaxLength = 65536
    Me.edTb_propro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_propro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_propro.TabIndex = 10
    '
    'edTb_fatpro
    '
    Me.edTb_fatpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_fatpro.EditValue = "0"
    Me.edTb_fatpro.Location = New System.Drawing.Point(128, 70)
    Me.edTb_fatpro.Name = "edTb_fatpro"
    Me.edTb_fatpro.NTSDbField = ""
    Me.edTb_fatpro.NTSFormat = "0"
    Me.edTb_fatpro.NTSForzaVisZoom = False
    Me.edTb_fatpro.NTSOldValue = ""
    Me.edTb_fatpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_fatpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_fatpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_fatpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_fatpro.Properties.AutoHeight = False
    Me.edTb_fatpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_fatpro.Properties.MaxLength = 65536
    Me.edTb_fatpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_fatpro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_fatpro.TabIndex = 9
    '
    'edTb_datpro
    '
    Me.edTb_datpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datpro.EditValue = "01/01/1900"
    Me.edTb_datpro.Location = New System.Drawing.Point(128, 44)
    Me.edTb_datpro.Name = "edTb_datpro"
    Me.edTb_datpro.NTSDbField = ""
    Me.edTb_datpro.NTSForzaVisZoom = False
    Me.edTb_datpro.NTSOldValue = ""
    Me.edTb_datpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datpro.Properties.AutoHeight = False
    Me.edTb_datpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datpro.Properties.MaxLength = 65536
    Me.edTb_datpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datpro.Size = New System.Drawing.Size(100, 20)
    Me.edTb_datpro.TabIndex = 8
    '
    'lbImpon
    '
    Me.lbImpon.AutoSize = True
    Me.lbImpon.BackColor = System.Drawing.Color.Transparent
    Me.lbImpon.Location = New System.Drawing.Point(9, 73)
    Me.lbImpon.Name = "lbImpon"
    Me.lbImpon.NTSDbField = ""
    Me.lbImpon.Size = New System.Drawing.Size(110, 13)
    Me.lbImpon.TabIndex = 7
    Me.lbImpon.Text = "Imponibile (fatturato)"
    Me.lbImpon.Tooltip = ""
    Me.lbImpon.UseMnemonic = False
    '
    'lbInca
    '
    Me.lbInca.AutoSize = True
    Me.lbInca.BackColor = System.Drawing.Color.Transparent
    Me.lbInca.Location = New System.Drawing.Point(313, 99)
    Me.lbInca.Name = "lbInca"
    Me.lbInca.NTSDbField = ""
    Me.lbInca.Size = New System.Drawing.Size(54, 13)
    Me.lbInca.TabIndex = 6
    Me.lbInca.Text = "Incassato"
    Me.lbInca.Tooltip = ""
    Me.lbInca.UseMnemonic = False
    '
    'lbIntegra
    '
    Me.lbIntegra.AutoSize = True
    Me.lbIntegra.BackColor = System.Drawing.Color.Transparent
    Me.lbIntegra.Location = New System.Drawing.Point(313, 125)
    Me.lbIntegra.Name = "lbIntegra"
    Me.lbIntegra.NTSDbField = ""
    Me.lbIntegra.Size = New System.Drawing.Size(114, 13)
    Me.lbIntegra.TabIndex = 5
    Me.lbIntegra.Text = "Integrazione al minimo"
    Me.lbIntegra.Tooltip = ""
    Me.lbIntegra.UseMnemonic = False
    '
    'lbInden
    '
    Me.lbInden.AutoSize = True
    Me.lbInden.BackColor = System.Drawing.Color.Transparent
    Me.lbInden.Location = New System.Drawing.Point(313, 73)
    Me.lbInden.Name = "lbInden"
    Me.lbInden.NTSDbField = ""
    Me.lbInden.Size = New System.Drawing.Size(94, 13)
    Me.lbInden.TabIndex = 4
    Me.lbInden.Text = "Indennità di cassa"
    Me.lbInden.Tooltip = ""
    Me.lbInden.UseMnemonic = False
    '
    'lbEnasaditt
    '
    Me.lbEnasaditt.AutoSize = True
    Me.lbEnasaditt.BackColor = System.Drawing.Color.Transparent
    Me.lbEnasaditt.Location = New System.Drawing.Point(313, 47)
    Me.lbEnasaditt.Name = "lbEnasaditt"
    Me.lbEnasaditt.NTSDbField = ""
    Me.lbEnasaditt.Size = New System.Drawing.Size(88, 13)
    Me.lbEnasaditt.TabIndex = 3
    Me.lbEnasaditt.Text = "Enasarco agente"
    Me.lbEnasaditt.Tooltip = ""
    Me.lbEnasaditt.UseMnemonic = False
    '
    'lbProvv
    '
    Me.lbProvv.AutoSize = True
    Me.lbProvv.BackColor = System.Drawing.Color.Transparent
    Me.lbProvv.Location = New System.Drawing.Point(9, 99)
    Me.lbProvv.Name = "lbProvv"
    Me.lbProvv.NTSDbField = ""
    Me.lbProvv.Size = New System.Drawing.Size(59, 13)
    Me.lbProvv.TabIndex = 2
    Me.lbProvv.Text = "Provvigioni"
    Me.lbProvv.Tooltip = ""
    Me.lbProvv.UseMnemonic = False
    '
    'lbEnasa
    '
    Me.lbEnasa.AutoSize = True
    Me.lbEnasa.BackColor = System.Drawing.Color.Transparent
    Me.lbEnasa.Location = New System.Drawing.Point(9, 125)
    Me.lbEnasa.Name = "lbEnasa"
    Me.lbEnasa.NTSDbField = ""
    Me.lbEnasa.Size = New System.Drawing.Size(76, 13)
    Me.lbEnasa.TabIndex = 1
    Me.lbEnasa.Text = "Enasarco ditta"
    Me.lbEnasa.Tooltip = ""
    Me.lbEnasa.UseMnemonic = False
    '
    'lbUltaggpro
    '
    Me.lbUltaggpro.AutoSize = True
    Me.lbUltaggpro.BackColor = System.Drawing.Color.Transparent
    Me.lbUltaggpro.Location = New System.Drawing.Point(9, 47)
    Me.lbUltaggpro.Name = "lbUltaggpro"
    Me.lbUltaggpro.NTSDbField = ""
    Me.lbUltaggpro.Size = New System.Drawing.Size(104, 13)
    Me.lbUltaggpro.TabIndex = 0
    Me.lbUltaggpro.Text = "Data ultimo aggiorn."
    Me.lbUltaggpro.Tooltip = ""
    Me.lbUltaggpro.UseMnemonic = False
    '
    'tsPeriodo
    '
    Me.tsPeriodo.AllowDrop = True
    Me.tsPeriodo.Controls.Add(Me.pnPeriodo)
    Me.tsPeriodo.Enable = True
    Me.tsPeriodo.Name = "tsPeriodo"
    Me.tsPeriodo.Size = New System.Drawing.Size(547, 294)
    Me.tsPeriodo.Text = "Periodo"
    '
    'pnPeriodo
    '
    Me.pnPeriodo.AllowDrop = True
    Me.pnPeriodo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPeriodo.Appearance.Options.UseBackColor = True
    Me.pnPeriodo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPeriodo.Controls.Add(Me.edTb_incaper)
    Me.pnPeriodo.Controls.Add(Me.edTb_cassper)
    Me.pnPeriodo.Controls.Add(Me.edTb_enagper)
    Me.pnPeriodo.Controls.Add(Me.edTb_endtper)
    Me.pnPeriodo.Controls.Add(Me.edTb_proper)
    Me.pnPeriodo.Controls.Add(Me.edTb_fatper)
    Me.pnPeriodo.Controls.Add(Me.edTb_datper)
    Me.pnPeriodo.Controls.Add(Me.lbImpper)
    Me.pnPeriodo.Controls.Add(Me.lbIncaper)
    Me.pnPeriodo.Controls.Add(Me.lbIndeper)
    Me.pnPeriodo.Controls.Add(Me.lbEnasadittper)
    Me.pnPeriodo.Controls.Add(Me.lbProvper)
    Me.pnPeriodo.Controls.Add(Me.lbEnasaPer)
    Me.pnPeriodo.Controls.Add(Me.lbUltaggper)
    Me.pnPeriodo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPeriodo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPeriodo.Location = New System.Drawing.Point(0, 0)
    Me.pnPeriodo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPeriodo.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPeriodo.Name = "pnPeriodo"
    Me.pnPeriodo.NTSActiveTrasparency = True
    Me.pnPeriodo.Size = New System.Drawing.Size(547, 294)
    Me.pnPeriodo.TabIndex = 0
    Me.pnPeriodo.Text = "NtsPanel1"
    '
    'edTb_incaper
    '
    Me.edTb_incaper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_incaper.EditValue = "0"
    Me.edTb_incaper.Location = New System.Drawing.Point(433, 70)
    Me.edTb_incaper.Name = "edTb_incaper"
    Me.edTb_incaper.NTSDbField = ""
    Me.edTb_incaper.NTSFormat = "0"
    Me.edTb_incaper.NTSForzaVisZoom = False
    Me.edTb_incaper.NTSOldValue = ""
    Me.edTb_incaper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_incaper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_incaper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_incaper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_incaper.Properties.AutoHeight = False
    Me.edTb_incaper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_incaper.Properties.MaxLength = 65536
    Me.edTb_incaper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_incaper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_incaper.TabIndex = 30
    '
    'edTb_cassper
    '
    Me.edTb_cassper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cassper.EditValue = "0"
    Me.edTb_cassper.Location = New System.Drawing.Point(433, 96)
    Me.edTb_cassper.Name = "edTb_cassper"
    Me.edTb_cassper.NTSDbField = ""
    Me.edTb_cassper.NTSFormat = "0"
    Me.edTb_cassper.NTSForzaVisZoom = False
    Me.edTb_cassper.NTSOldValue = ""
    Me.edTb_cassper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cassper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cassper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cassper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cassper.Properties.AutoHeight = False
    Me.edTb_cassper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cassper.Properties.MaxLength = 65536
    Me.edTb_cassper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cassper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_cassper.TabIndex = 29
    '
    'edTb_enagper
    '
    Me.edTb_enagper.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_enagper.EditValue = "0"
    Me.edTb_enagper.Location = New System.Drawing.Point(433, 44)
    Me.edTb_enagper.Name = "edTb_enagper"
    Me.edTb_enagper.NTSDbField = ""
    Me.edTb_enagper.NTSFormat = "0"
    Me.edTb_enagper.NTSForzaVisZoom = False
    Me.edTb_enagper.NTSOldValue = ""
    Me.edTb_enagper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_enagper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_enagper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_enagper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_enagper.Properties.AutoHeight = False
    Me.edTb_enagper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_enagper.Properties.MaxLength = 65536
    Me.edTb_enagper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_enagper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_enagper.TabIndex = 28
    '
    'edTb_endtper
    '
    Me.edTb_endtper.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_endtper.EditValue = "0"
    Me.edTb_endtper.Location = New System.Drawing.Point(128, 122)
    Me.edTb_endtper.Name = "edTb_endtper"
    Me.edTb_endtper.NTSDbField = ""
    Me.edTb_endtper.NTSFormat = "0"
    Me.edTb_endtper.NTSForzaVisZoom = False
    Me.edTb_endtper.NTSOldValue = ""
    Me.edTb_endtper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_endtper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_endtper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_endtper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_endtper.Properties.AutoHeight = False
    Me.edTb_endtper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_endtper.Properties.MaxLength = 65536
    Me.edTb_endtper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_endtper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_endtper.TabIndex = 27
    '
    'edTb_proper
    '
    Me.edTb_proper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_proper.EditValue = "0"
    Me.edTb_proper.Location = New System.Drawing.Point(128, 96)
    Me.edTb_proper.Name = "edTb_proper"
    Me.edTb_proper.NTSDbField = ""
    Me.edTb_proper.NTSFormat = "0"
    Me.edTb_proper.NTSForzaVisZoom = False
    Me.edTb_proper.NTSOldValue = ""
    Me.edTb_proper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_proper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_proper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_proper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_proper.Properties.AutoHeight = False
    Me.edTb_proper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_proper.Properties.MaxLength = 65536
    Me.edTb_proper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_proper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_proper.TabIndex = 26
    '
    'edTb_fatper
    '
    Me.edTb_fatper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_fatper.EditValue = "0"
    Me.edTb_fatper.Location = New System.Drawing.Point(128, 70)
    Me.edTb_fatper.Name = "edTb_fatper"
    Me.edTb_fatper.NTSDbField = ""
    Me.edTb_fatper.NTSFormat = "0"
    Me.edTb_fatper.NTSForzaVisZoom = False
    Me.edTb_fatper.NTSOldValue = ""
    Me.edTb_fatper.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_fatper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_fatper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_fatper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_fatper.Properties.AutoHeight = False
    Me.edTb_fatper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_fatper.Properties.MaxLength = 65536
    Me.edTb_fatper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_fatper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_fatper.TabIndex = 25
    '
    'edTb_datper
    '
    Me.edTb_datper.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datper.EditValue = "01/01/1900"
    Me.edTb_datper.Location = New System.Drawing.Point(128, 44)
    Me.edTb_datper.Name = "edTb_datper"
    Me.edTb_datper.NTSDbField = ""
    Me.edTb_datper.NTSForzaVisZoom = False
    Me.edTb_datper.NTSOldValue = ""
    Me.edTb_datper.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datper.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datper.Properties.AutoHeight = False
    Me.edTb_datper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datper.Properties.MaxLength = 65536
    Me.edTb_datper.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datper.Size = New System.Drawing.Size(100, 20)
    Me.edTb_datper.TabIndex = 24
    '
    'lbImpper
    '
    Me.lbImpper.AutoSize = True
    Me.lbImpper.BackColor = System.Drawing.Color.Transparent
    Me.lbImpper.Location = New System.Drawing.Point(9, 73)
    Me.lbImpper.Name = "lbImpper"
    Me.lbImpper.NTSDbField = ""
    Me.lbImpper.Size = New System.Drawing.Size(110, 13)
    Me.lbImpper.TabIndex = 23
    Me.lbImpper.Text = "Imponibile (fatturato)"
    Me.lbImpper.Tooltip = ""
    Me.lbImpper.UseMnemonic = False
    '
    'lbIncaper
    '
    Me.lbIncaper.AutoSize = True
    Me.lbIncaper.BackColor = System.Drawing.Color.Transparent
    Me.lbIncaper.Location = New System.Drawing.Point(313, 99)
    Me.lbIncaper.Name = "lbIncaper"
    Me.lbIncaper.NTSDbField = ""
    Me.lbIncaper.Size = New System.Drawing.Size(54, 13)
    Me.lbIncaper.TabIndex = 22
    Me.lbIncaper.Text = "Incassato"
    Me.lbIncaper.Tooltip = ""
    Me.lbIncaper.UseMnemonic = False
    '
    'lbIndeper
    '
    Me.lbIndeper.AutoSize = True
    Me.lbIndeper.BackColor = System.Drawing.Color.Transparent
    Me.lbIndeper.Location = New System.Drawing.Point(313, 73)
    Me.lbIndeper.Name = "lbIndeper"
    Me.lbIndeper.NTSDbField = ""
    Me.lbIndeper.Size = New System.Drawing.Size(94, 13)
    Me.lbIndeper.TabIndex = 20
    Me.lbIndeper.Text = "Indennità di cassa"
    Me.lbIndeper.Tooltip = ""
    Me.lbIndeper.UseMnemonic = False
    '
    'lbEnasadittper
    '
    Me.lbEnasadittper.AutoSize = True
    Me.lbEnasadittper.BackColor = System.Drawing.Color.Transparent
    Me.lbEnasadittper.Location = New System.Drawing.Point(313, 47)
    Me.lbEnasadittper.Name = "lbEnasadittper"
    Me.lbEnasadittper.NTSDbField = ""
    Me.lbEnasadittper.Size = New System.Drawing.Size(88, 13)
    Me.lbEnasadittper.TabIndex = 19
    Me.lbEnasadittper.Text = "Enasarco agente"
    Me.lbEnasadittper.Tooltip = ""
    Me.lbEnasadittper.UseMnemonic = False
    '
    'lbProvper
    '
    Me.lbProvper.AutoSize = True
    Me.lbProvper.BackColor = System.Drawing.Color.Transparent
    Me.lbProvper.Location = New System.Drawing.Point(9, 99)
    Me.lbProvper.Name = "lbProvper"
    Me.lbProvper.NTSDbField = ""
    Me.lbProvper.Size = New System.Drawing.Size(59, 13)
    Me.lbProvper.TabIndex = 18
    Me.lbProvper.Text = "Provvigioni"
    Me.lbProvper.Tooltip = ""
    Me.lbProvper.UseMnemonic = False
    '
    'lbEnasaPer
    '
    Me.lbEnasaPer.AutoSize = True
    Me.lbEnasaPer.BackColor = System.Drawing.Color.Transparent
    Me.lbEnasaPer.Location = New System.Drawing.Point(9, 125)
    Me.lbEnasaPer.Name = "lbEnasaPer"
    Me.lbEnasaPer.NTSDbField = ""
    Me.lbEnasaPer.Size = New System.Drawing.Size(76, 13)
    Me.lbEnasaPer.TabIndex = 17
    Me.lbEnasaPer.Text = "Enasarco ditta"
    Me.lbEnasaPer.Tooltip = ""
    Me.lbEnasaPer.UseMnemonic = False
    '
    'lbUltaggper
    '
    Me.lbUltaggper.AutoSize = True
    Me.lbUltaggper.BackColor = System.Drawing.Color.Transparent
    Me.lbUltaggper.Location = New System.Drawing.Point(9, 47)
    Me.lbUltaggper.Name = "lbUltaggper"
    Me.lbUltaggper.NTSDbField = ""
    Me.lbUltaggper.Size = New System.Drawing.Size(104, 13)
    Me.lbUltaggper.TabIndex = 16
    Me.lbUltaggper.Text = "Data ultimo aggiorn."
    Me.lbUltaggper.Tooltip = ""
    Me.lbUltaggper.UseMnemonic = False
    '
    'tsPerprov
    '
    Me.tsPerprov.AllowDrop = True
    Me.tsPerprov.Controls.Add(Me.pnPerprovv)
    Me.tsPerprov.Enable = True
    Me.tsPerprov.Name = "tsPerprov"
    Me.tsPerprov.Size = New System.Drawing.Size(547, 294)
    Me.tsPerprov.Text = "Percentuali provv."
    '
    'pnPerprovv
    '
    Me.pnPerprovv.AllowDrop = True
    Me.pnPerprovv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPerprovv.Appearance.Options.UseBackColor = True
    Me.pnPerprovv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPerprovv.Controls.Add(Me.ceProvv)
    Me.pnPerprovv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPerprovv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPerprovv.Location = New System.Drawing.Point(0, 0)
    Me.pnPerprovv.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPerprovv.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPerprovv.Name = "pnPerprovv"
    Me.pnPerprovv.NTSActiveTrasparency = True
    Me.pnPerprovv.Size = New System.Drawing.Size(547, 294)
    Me.pnPerprovv.TabIndex = 0
    Me.pnPerprovv.Text = "NtsPanel1"
    '
    'pnAgente
    '
    Me.pnAgente.AllowDrop = True
    Me.pnAgente.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAgente.Appearance.Options.UseBackColor = True
    Me.pnAgente.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAgente.Controls.Add(Me.lbDessup)
    Me.pnAgente.Controls.Add(Me.lbDesAnag)
    Me.pnAgente.Controls.Add(Me.cbTb_tippro)
    Me.pnAgente.Controls.Add(Me.cbTb_tiptab)
    Me.pnAgente.Controls.Add(Me.edTb_codsup)
    Me.pnAgente.Controls.Add(Me.edTb_giorni)
    Me.pnAgente.Controls.Add(Me.edTb_perprra)
    Me.pnAgente.Controls.Add(Me.edTb_codforn)
    Me.pnAgente.Controls.Add(Me.edTb_descage)
    Me.pnAgente.Controls.Add(Me.edTb_codcage)
    Me.pnAgente.Controls.Add(Me.lbCalcolo)
    Me.pnAgente.Controls.Add(Me.lbDecor)
    Me.pnAgente.Controls.Add(Me.lbNominativo)
    Me.pnAgente.Controls.Add(Me.lbCodsup)
    Me.pnAgente.Controls.Add(Me.lbMod)
    Me.pnAgente.Controls.Add(Me.lbPerc)
    Me.pnAgente.Controls.Add(Me.lbCodanag)
    Me.pnAgente.Controls.Add(Me.lbCodcage)
    Me.pnAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAgente.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnAgente.Location = New System.Drawing.Point(0, 30)
    Me.pnAgente.Name = "pnAgente"
    Me.pnAgente.NTSActiveTrasparency = True
    Me.pnAgente.Size = New System.Drawing.Size(556, 140)
    Me.pnAgente.TabIndex = 5
    Me.pnAgente.Text = "NtsPanel1"
    '
    'lbDessup
    '
    Me.lbDessup.BackColor = System.Drawing.Color.Transparent
    Me.lbDessup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessup.Location = New System.Drawing.Point(194, 110)
    Me.lbDessup.Name = "lbDessup"
    Me.lbDessup.NTSDbField = ""
    Me.lbDessup.Size = New System.Drawing.Size(342, 20)
    Me.lbDessup.TabIndex = 17
    Me.lbDessup.Tooltip = ""
    Me.lbDessup.UseMnemonic = False
    '
    'lbDesAnag
    '
    Me.lbDesAnag.BackColor = System.Drawing.Color.Transparent
    Me.lbDesAnag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesAnag.Location = New System.Drawing.Point(237, 32)
    Me.lbDesAnag.Name = "lbDesAnag"
    Me.lbDesAnag.NTSDbField = ""
    Me.lbDesAnag.Size = New System.Drawing.Size(299, 20)
    Me.lbDesAnag.TabIndex = 16
    Me.lbDesAnag.Tooltip = ""
    Me.lbDesAnag.UseMnemonic = False
    '
    'cbTb_tippro
    '
    Me.cbTb_tippro.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tippro.DataSource = Nothing
    Me.cbTb_tippro.DisplayMember = ""
    Me.cbTb_tippro.Location = New System.Drawing.Point(131, 84)
    Me.cbTb_tippro.Name = "cbTb_tippro"
    Me.cbTb_tippro.NTSDbField = ""
    Me.cbTb_tippro.Properties.AutoHeight = False
    Me.cbTb_tippro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tippro.Properties.DropDownRows = 30
    Me.cbTb_tippro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tippro.SelectedValue = ""
    Me.cbTb_tippro.Size = New System.Drawing.Size(144, 20)
    Me.cbTb_tippro.TabIndex = 15
    Me.cbTb_tippro.ValueMember = ""
    '
    'cbTb_tiptab
    '
    Me.cbTb_tiptab.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tiptab.DataSource = Nothing
    Me.cbTb_tiptab.DisplayMember = ""
    Me.cbTb_tiptab.Location = New System.Drawing.Point(131, 58)
    Me.cbTb_tiptab.Name = "cbTb_tiptab"
    Me.cbTb_tiptab.NTSDbField = ""
    Me.cbTb_tiptab.Properties.AutoHeight = False
    Me.cbTb_tiptab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tiptab.Properties.DropDownRows = 30
    Me.cbTb_tiptab.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tiptab.SelectedValue = ""
    Me.cbTb_tiptab.Size = New System.Drawing.Size(144, 20)
    Me.cbTb_tiptab.TabIndex = 14
    Me.cbTb_tiptab.ValueMember = ""
    '
    'edTb_codsup
    '
    Me.edTb_codsup.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_codsup.EditValue = "0"
    Me.edTb_codsup.Location = New System.Drawing.Point(131, 110)
    Me.edTb_codsup.Name = "edTb_codsup"
    Me.edTb_codsup.NTSDbField = ""
    Me.edTb_codsup.NTSFormat = "0"
    Me.edTb_codsup.NTSForzaVisZoom = False
    Me.edTb_codsup.NTSOldValue = ""
    Me.edTb_codsup.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codsup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codsup.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codsup.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codsup.Properties.AutoHeight = False
    Me.edTb_codsup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codsup.Properties.MaxLength = 65536
    Me.edTb_codsup.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codsup.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codsup.TabIndex = 13
    '
    'edTb_giorni
    '
    Me.edTb_giorni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_giorni.EditValue = "0"
    Me.edTb_giorni.Location = New System.Drawing.Point(471, 84)
    Me.edTb_giorni.Name = "edTb_giorni"
    Me.edTb_giorni.NTSDbField = ""
    Me.edTb_giorni.NTSFormat = "0"
    Me.edTb_giorni.NTSForzaVisZoom = False
    Me.edTb_giorni.NTSOldValue = ""
    Me.edTb_giorni.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni.Properties.AutoHeight = False
    Me.edTb_giorni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni.Properties.MaxLength = 65536
    Me.edTb_giorni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni.Size = New System.Drawing.Size(65, 20)
    Me.edTb_giorni.TabIndex = 12
    '
    'edTb_perprra
    '
    Me.edTb_perprra.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_perprra.EditValue = "0"
    Me.edTb_perprra.Location = New System.Drawing.Point(471, 58)
    Me.edTb_perprra.Name = "edTb_perprra"
    Me.edTb_perprra.NTSDbField = ""
    Me.edTb_perprra.NTSFormat = "0"
    Me.edTb_perprra.NTSForzaVisZoom = False
    Me.edTb_perprra.NTSOldValue = ""
    Me.edTb_perprra.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_perprra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_perprra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_perprra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_perprra.Properties.AutoHeight = False
    Me.edTb_perprra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_perprra.Properties.MaxLength = 65536
    Me.edTb_perprra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_perprra.Size = New System.Drawing.Size(65, 20)
    Me.edTb_perprra.TabIndex = 11
    '
    'edTb_codforn
    '
    Me.edTb_codforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codforn.EditValue = "0"
    Me.edTb_codforn.Location = New System.Drawing.Point(131, 32)
    Me.edTb_codforn.Name = "edTb_codforn"
    Me.edTb_codforn.NTSDbField = ""
    Me.edTb_codforn.NTSFormat = "0"
    Me.edTb_codforn.NTSForzaVisZoom = False
    Me.edTb_codforn.NTSOldValue = ""
    Me.edTb_codforn.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codforn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codforn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codforn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codforn.Properties.AutoHeight = False
    Me.edTb_codforn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codforn.Properties.MaxLength = 65536
    Me.edTb_codforn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codforn.Size = New System.Drawing.Size(100, 20)
    Me.edTb_codforn.TabIndex = 10
    '
    'edTb_descage
    '
    Me.edTb_descage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descage.Location = New System.Drawing.Point(300, 6)
    Me.edTb_descage.Name = "edTb_descage"
    Me.edTb_descage.NTSDbField = ""
    Me.edTb_descage.NTSForzaVisZoom = False
    Me.edTb_descage.NTSOldValue = ""
    Me.edTb_descage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descage.Properties.AutoHeight = False
    Me.edTb_descage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descage.Properties.MaxLength = 65536
    Me.edTb_descage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descage.Size = New System.Drawing.Size(236, 20)
    Me.edTb_descage.TabIndex = 9
    '
    'edTb_codcage
    '
    Me.edTb_codcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcage.EditValue = "0"
    Me.edTb_codcage.Enabled = False
    Me.edTb_codcage.Location = New System.Drawing.Point(131, 6)
    Me.edTb_codcage.Name = "edTb_codcage"
    Me.edTb_codcage.NTSDbField = ""
    Me.edTb_codcage.NTSFormat = "0"
    Me.edTb_codcage.NTSForzaVisZoom = False
    Me.edTb_codcage.NTSOldValue = ""
    Me.edTb_codcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcage.Properties.AutoHeight = False
    Me.edTb_codcage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcage.Properties.MaxLength = 65536
    Me.edTb_codcage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcage.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codcage.TabIndex = 8
    '
    'lbCalcolo
    '
    Me.lbCalcolo.AutoSize = True
    Me.lbCalcolo.BackColor = System.Drawing.Color.Transparent
    Me.lbCalcolo.Location = New System.Drawing.Point(316, 61)
    Me.lbCalcolo.Name = "lbCalcolo"
    Me.lbCalcolo.NTSDbField = ""
    Me.lbCalcolo.Size = New System.Drawing.Size(133, 13)
    Me.lbCalcolo.TabIndex = 7
    Me.lbCalcolo.Text = "% Provvigione calcolo R/A"
    Me.lbCalcolo.Tooltip = ""
    Me.lbCalcolo.UseMnemonic = False
    '
    'lbDecor
    '
    Me.lbDecor.AutoSize = True
    Me.lbDecor.BackColor = System.Drawing.Color.Transparent
    Me.lbDecor.Location = New System.Drawing.Point(316, 87)
    Me.lbDecor.Name = "lbDecor"
    Me.lbDecor.NTSDbField = ""
    Me.lbDecor.Size = New System.Drawing.Size(149, 13)
    Me.lbDecor.TabIndex = 6
    Me.lbDecor.Text = "Decorrenza matur.provv.(gg)"
    Me.lbDecor.Tooltip = ""
    Me.lbDecor.UseMnemonic = False
    '
    'lbNominativo
    '
    Me.lbNominativo.AutoSize = True
    Me.lbNominativo.BackColor = System.Drawing.Color.Transparent
    Me.lbNominativo.Location = New System.Drawing.Point(234, 9)
    Me.lbNominativo.Name = "lbNominativo"
    Me.lbNominativo.NTSDbField = ""
    Me.lbNominativo.Size = New System.Drawing.Size(60, 13)
    Me.lbNominativo.TabIndex = 5
    Me.lbNominativo.Text = "Nominativo"
    Me.lbNominativo.Tooltip = ""
    Me.lbNominativo.UseMnemonic = False
    '
    'lbCodsup
    '
    Me.lbCodsup.AutoSize = True
    Me.lbCodsup.BackColor = System.Drawing.Color.Transparent
    Me.lbCodsup.Location = New System.Drawing.Point(12, 113)
    Me.lbCodsup.Name = "lbCodsup"
    Me.lbCodsup.NTSDbField = ""
    Me.lbCodsup.Size = New System.Drawing.Size(115, 13)
    Me.lbCodsup.TabIndex = 4
    Me.lbCodsup.Text = "Cod. agente superiore"
    Me.lbCodsup.Tooltip = ""
    Me.lbCodsup.UseMnemonic = False
    '
    'lbMod
    '
    Me.lbMod.AutoSize = True
    Me.lbMod.BackColor = System.Drawing.Color.Transparent
    Me.lbMod.Location = New System.Drawing.Point(12, 87)
    Me.lbMod.Name = "lbMod"
    Me.lbMod.NTSDbField = ""
    Me.lbMod.Size = New System.Drawing.Size(107, 13)
    Me.lbMod.TabIndex = 3
    Me.lbMod.Text = "Mod. corresponsione"
    Me.lbMod.Tooltip = ""
    Me.lbMod.UseMnemonic = False
    '
    'lbPerc
    '
    Me.lbPerc.AutoSize = True
    Me.lbPerc.BackColor = System.Drawing.Color.Transparent
    Me.lbPerc.Location = New System.Drawing.Point(12, 61)
    Me.lbPerc.Name = "lbPerc"
    Me.lbPerc.NTSDbField = ""
    Me.lbPerc.Size = New System.Drawing.Size(108, 13)
    Me.lbPerc.TabIndex = 2
    Me.lbPerc.Text = "Percentuali riferite a:"
    Me.lbPerc.Tooltip = ""
    Me.lbPerc.UseMnemonic = False
    '
    'lbCodanag
    '
    Me.lbCodanag.AutoSize = True
    Me.lbCodanag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodanag.Location = New System.Drawing.Point(12, 35)
    Me.lbCodanag.Name = "lbCodanag"
    Me.lbCodanag.NTSDbField = ""
    Me.lbCodanag.Size = New System.Drawing.Size(93, 13)
    Me.lbCodanag.TabIndex = 1
    Me.lbCodanag.Text = "Codice anagrafico"
    Me.lbCodanag.Tooltip = ""
    Me.lbCodanag.UseMnemonic = False
    '
    'lbCodcage
    '
    Me.lbCodcage.AutoSize = True
    Me.lbCodcage.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcage.Location = New System.Drawing.Point(12, 9)
    Me.lbCodcage.Name = "lbCodcage"
    Me.lbCodcage.NTSDbField = ""
    Me.lbCodcage.Size = New System.Drawing.Size(76, 13)
    Me.lbCodcage.TabIndex = 0
    Me.lbCodcage.Text = "Codice agente"
    Me.lbCodcage.Tooltip = ""
    Me.lbCodcage.UseMnemonic = False
    '
    'FRMPRCAGE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(556, 494)
    Me.Controls.Add(Me.tsCage)
    Me.Controls.Add(Me.pnAgente)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMPRCAGE"
    Me.Text = "TABELLA CODICI AGENTE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsCage, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsCage.ResumeLayout(False)
    Me.tsAltri.ResumeLayout(False)
    CType(Me.pnAltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAltri.ResumeLayout(False)
    Me.pnAltri.PerformLayout()
    CType(Me.ckTb_webvis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcaui.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codtben.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_perinca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datiniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsProg.ResumeLayout(False)
    CType(Me.pnProg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProg.ResumeLayout(False)
    Me.pnProg.PerformLayout()
    CType(Me.edTb_minrecu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_incapro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_casspro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_enagpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_endtpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_propro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_fatpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsPeriodo.ResumeLayout(False)
    CType(Me.pnPeriodo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPeriodo.ResumeLayout(False)
    Me.pnPeriodo.PerformLayout()
    CType(Me.edTb_incaper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cassper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_enagper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_endtper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_proper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_fatper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_datper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsPerprov.ResumeLayout(False)
    CType(Me.pnPerprovv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPerprovv.ResumeLayout(False)
    CType(Me.pnAgente, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAgente.ResumeLayout(False)
    Me.pnAgente.PerformLayout()
    CType(Me.cbTb_tippro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tiptab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codsup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_perprra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codforn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      cbTb_tippro.NTSDbField = "tabcage.tb_tippro"
      cbTb_tiptab.NTSDbField = "tabcage.tb_tiptab"
      edTb_codsup.NTSDbField = "tabcage.tb_codsup"
      edTb_giorni.NTSDbField = "tabcage.tb_giorni"
      edTb_perprra.NTSDbField = "tabcage.tb_perprra"
      edTb_codforn.NTSDbField = "tabcage.tb_codforn"
      edTb_descage.NTSDbField = "tabcage.tb_descage"
      edTb_codcage.NTSDbField = "tabcage.tb_codcage"
      edTb_minrecu.NTSDbField = "tabcage.tb_minrecu"
      edTb_incapro.NTSDbField = "tabcage.tb_incapro"
      edTb_casspro.NTSDbField = "tabcage.tb_casspro"
      edTb_enagpro.NTSDbField = "tabcage.tb_enagpro"
      edTb_endtpro.NTSDbField = "tabcage.tb_endtpro"
      edTb_propro.NTSDbField = "tabcage.tb_propro"
      edTb_fatpro.NTSDbField = "tabcage.tb_fatpro"
      edTb_datpro.NTSDbField = "tabcage.tb_datpro"
      edTb_incaper.NTSDbField = "tabcage.tb_incaper"
      edTb_cassper.NTSDbField = "tabcage.tb_cassper"
      edTb_enagper.NTSDbField = "tabcage.tb_enagper"
      edTb_endtper.NTSDbField = "tabcage.tb_endtper"
      edTb_proper.NTSDbField = "tabcage.tb_proper"
      edTb_fatper.NTSDbField = "tabcage.tb_fatper"
      edTb_datper.NTSDbField = "tabcage.tb_datper"
      edTb_codcaui.NTSDbField = "tabcage.tb_codcaui"
      edTb_codtben.NTSDbField = "tabcage.tb_codtben"
      edTb_perinca.NTSDbField = "tabcage.tb_perinca"
      edTb_datiniz.NTSDbField = "tabcage.tb_datiniz"
      edTb_datfin.NTSDbField = "tabcage.tb_datfin"
      ckTb_webvis.NTSText.NTSDbField = "tabcage.tb_webvis"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCage, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMPRCAGE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleCage.Apri(DittaCorrente, dsCage) Then Return
      dcCage.DataSource = dsCage.Tables("TABCAGE")
      dsCage.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      If dsCage.Tables("TABCAGE").Rows.Count > 0 Then edTb_codcage.Enabled = False
      If dsCage.Tables("TABCAGE").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcCage.ResetBindings(False)
      dcCage.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      Aggiorna(False)
      '--------------------------------------------------------------------------------------------------------------
      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCCC) Then tlbContratti.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If Not Salva() Then Return
          If tlbNuovo.Enabled And tlbNuovo.Visible Then      'Se 'Nuovo' non è abilitato o visibile, non permetto di creare un nuovo agente
            oCleCage.Nuovo()
            dcCage.MoveLast()
            edTb_codcage.Enabled = True
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i As Integer = 0 To dcCage.List.Count - 1
            If CType(dcCage.Item(i), DataRowView)!tb_codcage.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcCage.Position = i
              DecodAfterPosition()
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then

      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMPRCAGE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If tlbSalva.Enabled And tlbSalva.Visible Then
      If Not Salva() Then e.Cancel = True
    Else
      tlbRipristina_ItemClick(Nothing, Nothing)
    End If
  End Sub

  Public Overridable Sub FRMPRCAGE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCage.Dispose()
      dsCage.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTiptab As New DataTable()
    Dim dttTippro As New DataTable()

    Try
      dttTiptab.Columns.Add("cod", GetType(String))
      dttTiptab.Columns.Add("val", GetType(String))
      dttTiptab.Rows.Add(New Object() {"A", "Articolo"})
      dttTiptab.Rows.Add(New Object() {"C", "Cliente"})
      dttTiptab.Rows.Add(New Object() {"T", "Sconti/provv."})
      dttTiptab.AcceptChanges()
      cbTb_tiptab.DataSource = dttTiptab
      cbTb_tiptab.ValueMember = "cod"
      cbTb_tiptab.DisplayMember = "val"

      dttTippro.Columns.Add("cod", GetType(String))
      dttTippro.Columns.Add("val", GetType(String))
      dttTippro.Rows.Add(New Object() {"F", "Fatturato"})
      dttTippro.Rows.Add(New Object() {"I", "Incassato"})
      dttTippro.Rows.Add(New Object() {"M", "Maturato"})
      dttTippro.Rows.Add(New Object() {"T", "Incassato totale"})
      dttTippro.AcceptChanges()
      cbTb_tippro.DataSource = dttTippro
      cbTb_tippro.ValueMember = "cod"
      cbTb_tippro.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo un nuovo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      oCleCage.Nuovo()
      dcCage.MoveLast()

      tsCage.SelectedTabPageIndex = 0

      edTb_codcage.Enabled = True
      Aggiorna(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Salva() Then
        edTb_codcage.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim i As Integer = 0
    Try
      Cancella()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

          If dsCage.Tables("TABCAGE").Rows.Count = 1 AndAlso dsCage.Tables("TABCAGE").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleCage.Ripristina(dcCage.Position, dcCage.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCage, Me)
            bRemovBinding = False
            edTb_codcage.Enabled = True
          Else
            edTb_codcage.Enabled = False
          End If

          ceProvv.Ripristina()

          Aggiorna(False)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
      Dim i As Integer = 0
      If edTb_descage.Focused Then
        'lancio lo zoom solo per riposizionare il BindingSource
        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!

        NTSZOOM.strIn = edTb_codcage.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codcage.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcCage.List.Count - 1
            If CType(dcCage.Item(i), DataRowView)!tb_codcage.ToString = NTSZOOM.strIn Then
              dcCage.Position = i
              Exit For
            End If
          Next
          Aggiorna(False)
        End If
      ElseIf edTb_codforn.Focused Then
        SetFastZoom(edTb_codforn.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edTb_codforn.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "F"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_codforn.Text Then edTb_codforn.NTSTextDB = NTSZOOM.strIn
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
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      Stampa(0)
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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      dcCage.MoveFirst()

      Aggiorna(False)
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
      End If
      dcCage.MovePrevious()

      Aggiorna(False)
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
      End If
      dcCage.MoveNext()

      Aggiorna(False)
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
      End If
      dcCage.MoveLast()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAzzpro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAzzpro.ItemClick
    Dim i As Integer
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128727709062296733, "Azzerare i progressivi di tutti gli agenti?")) = Windows.Forms.DialogResult.No Then Return

      For i = 0 To dsCage.Tables("TABCAGE").Rows.Count - 1
        If dsCage.Tables("TABCAGE").Rows(i)!tb_codcage.ToString = edTb_codcage.Text Then
          dcCage.Position = i
          Exit For
        End If
      Next
      For i = 0 To dsCage.Tables("TABCAGE").Rows.Count - 1
        dsCage.Tables("TABCAGE").Rows(i)!tb_fatpro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_propro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_endtpro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_enagpro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_incapro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_casspro = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_minrecu = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_datpro = NTSCDate(dsCage.Tables("TABCAGE").Rows(i)!tb_datpro).AddDays(1)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAzzper_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAzzper.ItemClick
    Dim i As Integer
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128727712760530500, "Azzerare i dati del periodo di tutti gli agenti?")) = Windows.Forms.DialogResult.No Then Return

      For i = 0 To dsCage.Tables("TABCAGE").Rows.Count - 1
        If dsCage.Tables("TABCAGE").Rows(i)!tb_codcage.ToString = edTb_codcage.Text Then
          dcCage.Position = i
          Exit For
        End If
      Next

      For i = 0 To dsCage.Tables("TABCAGE").Rows.Count - 1
        dsCage.Tables("TABCAGE").Rows(i)!tb_fatper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_proper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_endtper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_enagper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_incaper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_cassper = 0
        dsCage.Tables("TABCAGE").Rows(i)!tb_datper = NTSCDate(dsCage.Tables("TABCAGE").Rows(i)!tb_datper).AddDays(1)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbContratti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbContratti.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.dPar2 = NTSCInt(dsCage.Tables("TABCAGE").Rows(dcCage.Position)!tb_codcage)
      oPar.bPar1 = True 'Promozioni modificabili.

      oMenu.RunChild("NTSInformatica", "FRMREGTES", "", DittaCorrente, "", "BNREGTES", oPar, "", True, True)
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
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleCage.RecordIsChanged Then
        If GctlControllaOutNotEqual() = False Then Return False
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589412364687500, "Salvo la tabella codici agenti?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleCage.Salva(False) Then Return False
          If dsCage.Tables("TABCAGE").Rows.Count > 0 Then
            edTb_codcage.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
          Return True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ceProvv.Salva
      '--------------------------------------------------------------------------------------------------------------
      Aggiorna(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Cancella() As Boolean
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'cancello
      Select Case oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589410075156250, "Confermare la cancellazione del record?"))
        Case Windows.Forms.DialogResult.No
          Return False
        Case Windows.Forms.DialogResult.Yes
          If dsCage.Tables("TABCAGE").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleCage.CancellaProv(NTSCInt(edTb_codcage.Text))
          dcCage.RemoveAt(dcCage.Position)
          oCleCage.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCage, Me)
            bRemovBinding = False
            edTb_codcage.Enabled = True
          Else
            edTb_codcage.Enabled = False
          End If
      End Select

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      strCrpe = "{TABCAGE.codditt} = '" & DittaCorrente & "'"

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSPRCAGE", "Reports1", " ", 0, nDestin, "BSPRCAGE.RPT", False, "Codici agenti", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Validated/Changed"
  Public Overridable Sub edTb_codforn_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codforn.Validated
    Try
      If edTb_codforn.Text = "0" Then
        lbDesAnag.Text = ""
      End If

      If Not oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", lbDesAnag.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129472396764093093, "Codice anagrafico inesistente"))
        edTb_codforn.Text = "0"
        lbDesAnag.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTb_codsup_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codsup.Validated
    Try
      If edTb_codsup.Text = "0" Then
        lbDessup.Text = ""
      End If

      If edTb_codsup.Text = edTb_codcage.Text Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727716723448090, "Il codice agente superiore non può essere uguale al codice agente corrente."))
        Return
      End If

      If Not oCleCage.ValCodiceDB(edTb_codsup.Text, "N", "TABCAGE", lbDessup.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726803051669432, "Codice agente inesistente"))
        edTb_codsup.Text = "0"
        lbDessup.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTb_codcaui_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codcaui.Validated
    Try
      If edTb_codcaui.Text = "0" Then
        lbDescausa.Text = ""
      End If

      If Not oCleCage.ValCodiceDB(edTb_codcaui.Text, "N", "TABCAUC", lbDescausa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726803590888182, "Codice causale incasso"))
        edTb_codcaui.Text = "0"
        lbDescausa.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTb_codtben_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codtben.Validated
    Try
      If edTb_codtben.Text = "0" Then
        lbDestabena.Text = ""
      End If

      If Not oCleCage.ValCodiceDB(edTb_codtben.Text, "N", "TABENAS", lbDestabena.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129472396431536906, "Codice tab. enasarco"))
        edTb_codtben.Text = "0"
        lbDestabena.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbTb_tiptab_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_tiptab.SelectedValueChanged
    Try
      Select Case cbTb_tiptab.SelectedValue
        Case "A", "T" : ceProvv.strRifer = "A"
        Case "C" : ceProvv.strRifer = "C"
      End Select

      ceProvv.cbTipoProv_SelectedIndexChanged(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tsCage_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsCage.SelectedPageChanged
    Try
      If tsCage.SelectedTabPageIndex = 2 Then
        If edTb_codcage.Enabled = True Then
          tsCage.SelectedTabPageIndex = 0
          oApp.MsgBoxErr(oApp.Tr(Me, 128727741789695349, "Prima di accedere alle percentuali provvigioni è necessario salvare"))
          Return
        End If
        'apro la griglia
        ceProvv.TipoProvv = 6
        ceProvv.PerCodcage = NTSCInt(edTb_codcage.Text)
        ceProvv.per_codcage.Visible = False
        ceProvv.xx_codcage.Visible = False
        ceProvv.ApriProvvigioni()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Aggiorna(ByVal bNuovo As Boolean)
    Try
      If edTb_codforn.Text = "0" Then
        lbDesAnag.Text = ""
      End If
      If Not oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", lbDesAnag.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726803304325682, "Codice anagrafico inesistente"))
        edTb_codforn.Text = "0"
        lbDesAnag.Text = ""
      End If

      If edTb_codsup.Text = "0" Then
        lbDessup.Text = ""
      End If
      If Not oCleCage.ValCodiceDB(edTb_codsup.Text, "N", "TABCAGE", lbDessup.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129472396685991593, "Codice agente inesistente"))
        edTb_codsup.Text = "0"
        lbDessup.Text = ""
      End If

      If edTb_codcaui.Text = "0" Then
        lbDescausa.Text = ""
      End If
      If Not oCleCage.ValCodiceDB(edTb_codcaui.Text, "N", "TABCAUC", lbDescausa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129472396538535961, "Codice causale incasso"))
        edTb_codcaui.Text = "0"
        lbDescausa.Text = ""
      End If

      If edTb_codtben.Text = "0" Then
        lbDestabena.Text = ""
      End If
      If Not oCleCage.ValCodiceDB(edTb_codtben.Text, "N", "TABENAS", lbDestabena.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726803776825682, "Codice tab. enasarco"))
        edTb_codtben.Text = "0"
        lbDestabena.Text = ""
      End If

      If Not bNuovo Then
        'apro la griglia
        ceProvv.TipoProvv = 6
        ceProvv.PerCodcage = NTSCInt(edTb_codcage.Text)
        ceProvv.per_codcage.Visible = False
        ceProvv.xx_codcage.Visible = False
        ceProvv.ApriProvvigioni()
      Else
        'se nuovo carica l'agente 0 cioè svuota la griglia
        'per il bug corretto il xx/yy
        'apro la griglia
        ceProvv.TipoProvv = 6
        ceProvv.PerCodcage = 0
        ceProvv.per_codcage.Visible = False
        ceProvv.xx_codcage.Visible = False
        ceProvv.ApriProvvigioni()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub DecodAfterPosition()
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      lbDesAnag.Text = ""
      If edTb_codforn.Text <> "0" Then
        If oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", strTmp) = True Then lbDesAnag.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbDessup.Text = ""
      If edTb_codsup.Text <> "0" Then
        If oCleCage.ValCodiceDB(edTb_codsup.Text, "N", "TABCAGE", strTmp) = True Then lbDessup.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbDescausa.Text = ""
      If edTb_codcaui.Text <> "0" Then
        If oCleCage.ValCodiceDB(edTb_codcaui.Text, "N", "TABCAUC", strTmp) = True Then lbDescausa.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbDestabena.Text = ""
      If edTb_codtben.Text <> "0" Then
        If oCleCage.ValCodiceDB(edTb_codtben.Text, "N", "TABENAS", strTmp) = True Then lbDestabena.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class