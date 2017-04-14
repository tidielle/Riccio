Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVETPBF
  Public oCleTpbf As CLEVETPBF
  Public dsTpbf As DataSet

  Public oCallParams As CLE__CLDP
  Public dcTpbf As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer

#Region "Moduli"
  Private Moduli_P As Integer = bsModVE + bsModMG + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtPAR + bsModExtCRM
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

#Region "Dichirazione Controlli"
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbTb_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents edTb_codtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_destpbf As NTSInformatica.NTSLabel
  Public WithEvents edTb_destpbf As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckTb_new506 As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_tcontro As NTSInformatica.NTSLabel
  Public WithEvents edTb_tcontro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tmagazz As NTSInformatica.NTSLabel
  Public WithEvents edTb_tmagazz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tcaumag As NTSInformatica.NTSLabel
  Public WithEvents edTb_tcaumag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tlistin As NTSInformatica.NTSLabel
  Public WithEvents edTb_tlistin As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckTb_tscorpo As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_tprofor As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_tcautra As NTSInformatica.NTSLabel
  Public WithEvents edTb_tcautra As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_vcodcen As NTSInformatica.NTSLabel
  Public WithEvents edTb_vcodcen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tmagazz2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_tmagazz2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_flresocl As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_tcauscap As NTSInformatica.NTSLabel
  Public WithEvents edTb_tcauscap As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tmagimp As NTSInformatica.NTSLabel
  Public WithEvents edTb_tmagimp As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_flacconto As NTSInformatica.NTSComboBox
  Public WithEvents ckTb_fattsosp As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_przbol As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_autotr As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_tcontro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tmagazz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tmagazz2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tcaumag As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tcautra As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vcodcen As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tcauscap As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tmagimp As NTSInformatica.NTSLabel
  Public WithEvents fmDocumento As NTSInformatica.NTSGroupBox
  Public WithEvents fmPrestazione As NTSInformatica.NTSGroupBox
  Public WithEvents fmSegue As NTSInformatica.NTSGroupBox
  Public WithEvents fmOpzioni As NTSInformatica.NTSGroupBox
  Public WithEvents cbTb_prestserv As NTSInformatica.NTSComboBox
  Public WithEvents lbXx_codcacadd As NTSInformatica.NTSLabel
  Public WithEvents lbTb_codcacadd As NTSInformatica.NTSLabel
  Public WithEvents edTb_codcacadd As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnRight As NTSInformatica.NTSGroupBox
  Public WithEvents fmLeft As NTSInformatica.NTSGroupBox
#End Region

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
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
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVETPBF", "BEVETPBF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128491173731875580, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleTpbf = CType(oTmp, CLEVETPBF)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BNVETPBF", strRemoteServer, strRemotePort)
    AddHandler oCleTpbf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleTpbf.Init(oApp, oScript, oMenu.oCleComm, "TABTPBF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttRevc As New DataTable()
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'à una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()

      dttRevc.Columns.Add("cod", GetType(String))
      dttRevc.Columns.Add("val", GetType(String))
      dttRevc.Rows.Add(New Object() {"N", "No"})
      dttRevc.Rows.Add(New Object() {"S", "Si"})
      dttRevc.Rows.Add(New Object() {"M", "Misto"})
      dttRevc.AcceptChanges()
      cbTb_fattrevch.DataSource = dttRevc
      cbTb_fattrevch.ValueMember = "cod"
      cbTb_fattrevch.DisplayMember = "val"

      '--------------------------------------------------------------------------------------------------------------
      edTb_codtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950725808722, "Tipo Bolla/Fattura"), tabtpbf)
      edTb_destpbf.NTSSetParam(oMenu, oApp.Tr(Me, 128759950738288722, "Descrizione"), 30, True)
      ckTb_new506.NTSSetParam(oMenu, oApp.Tr(Me, 128759950748896722, "Deroga alla tabella personalizzata"), "S", "N")
      edTb_tcontro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950759192722, "Contropartita"), tabcove)
      edTb_tmagazz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950770112722, "Magazzino 1"), tabmaga)
      edTb_tcaumag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950782748722, "Causale Magazzino"), tabcaum)
      edTb_tlistin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950795696722, "Listino Testata"), tablist)
      ckTb_tscorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128759950805368722, "Scorporo in testata bolla"), "S", "N")
      ckTb_tprofor.NTSSetParam(oMenu, oApp.Tr(Me, 128759950815976722, "Proforma"), "S", "N")
      edTb_tcautra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950827364722, "Causale Trasporto"), tabcaum)
      edTb_vcodcen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950838128722, "Centro C/A"), tabcena)
      edTb_tmagazz2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950849516722, "Magazzino 2"), tabmaga)
      cbTb_flresocl.NTSSetParam(oApp.Tr(Me, 128759950861372722, "Segue"))
      edTb_tcauscap.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950871668722, "Causale scarico produzione"), tabcaum)
      edTb_tmagimp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759950883056722, "Magazzino imp."), tabmaga)
      cbTb_flacconto.NTSSetParam(oApp.Tr(Me, 128759950892728722, "Documento d'acconto :"))
      ckTb_fattsosp.NTSSetParam(oMenu, oApp.Tr(Me, 128759950902556722, "IVA ad esig.differita"), "S", "N")
      ckTb_przbol.NTSSetParam(oMenu, oApp.Tr(Me, 128759950913944722, "Prezzo e sconti in bolla"), "S", "N")
      ckTb_autotr.NTSSetParam(oMenu, oApp.Tr(Me, 129127802386330395, "Fatt./nota di accr. autotrasportatori"), "S", "N")
      cbTb_prestserv.NTSSetParam(oApp.Tr(Me, 128491814464519070, "Tipo prestazione"))
      edTb_codcacadd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129127795204153365, "Causale di CA per spese di piede"), tabcaca)
      edTb_coddivi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129270253533751797, "Divisione di CA"), tabdivi)
      ckTb_fattextrc.NTSSetParam(oMenu, oApp.Tr(Me, 129460388795732421, "Fatt./nota di accr. acquisto extracee"), "S", "N")
      edTb_codcauc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129491394461552735, "Causale di CG"), tabcauc)
      ckTb_fattrevch.NTSSetParam(oMenu, "vecchio non più usato", "S", "N")
      cbTb_fattrevch.NTSSetParam(oApp.Tr(Me, 129775665834337671, "Documento acquisto Reverse charge"))
      ckTb_resoclfor.NTSSetParam(oMenu, oApp.Tr(Me, 129739637243723635, "Forza listino (solo GPV)"), "S", "N")
      edTb_tiporkok.NTSSetParam(oMenu, oApp.Tr(Me, 130184581352300710, "Utilizzabile con"), 50, True)
      '--------------------------------------------------------------------------------------------------------------
      edTb_codtpbf.NTSSetRichiesto()
      edTb_destpbf.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      edTb_codtpbf.NTSSetParamZoom("")
      edTb_destpbf.NTSForzaVisZoom = True
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVETPBF))
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
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbTb_codtpbf = New NTSInformatica.NTSLabel
    Me.edTb_codtpbf = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_destpbf = New NTSInformatica.NTSLabel
    Me.edTb_destpbf = New NTSInformatica.NTSTextBoxStr
    Me.ckTb_new506 = New NTSInformatica.NTSCheckBox
    Me.lbTb_tcontro = New NTSInformatica.NTSLabel
    Me.edTb_tcontro = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tmagazz = New NTSInformatica.NTSLabel
    Me.edTb_tmagazz = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tcaumag = New NTSInformatica.NTSLabel
    Me.edTb_tcaumag = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tlistin = New NTSInformatica.NTSLabel
    Me.edTb_tlistin = New NTSInformatica.NTSTextBoxNum
    Me.ckTb_tscorpo = New NTSInformatica.NTSCheckBox
    Me.ckTb_tprofor = New NTSInformatica.NTSCheckBox
    Me.lbTb_tcautra = New NTSInformatica.NTSLabel
    Me.edTb_tcautra = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_vcodcen = New NTSInformatica.NTSLabel
    Me.edTb_vcodcen = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tmagazz2 = New NTSInformatica.NTSLabel
    Me.edTb_tmagazz2 = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_flresocl = New NTSInformatica.NTSComboBox
    Me.lbTb_tcauscap = New NTSInformatica.NTSLabel
    Me.edTb_tcauscap = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tmagimp = New NTSInformatica.NTSLabel
    Me.edTb_tmagimp = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_flacconto = New NTSInformatica.NTSComboBox
    Me.ckTb_fattsosp = New NTSInformatica.NTSCheckBox
    Me.ckTb_przbol = New NTSInformatica.NTSCheckBox
    Me.ckTb_autotr = New NTSInformatica.NTSCheckBox
    Me.lbXx_tcontro = New NTSInformatica.NTSLabel
    Me.lbXx_tmagazz = New NTSInformatica.NTSLabel
    Me.lbXx_tmagazz2 = New NTSInformatica.NTSLabel
    Me.lbXx_tcaumag = New NTSInformatica.NTSLabel
    Me.lbXx_tcautra = New NTSInformatica.NTSLabel
    Me.lbXx_vcodcen = New NTSInformatica.NTSLabel
    Me.lbXx_tcauscap = New NTSInformatica.NTSLabel
    Me.lbXx_tmagimp = New NTSInformatica.NTSLabel
    Me.fmOpzioni = New NTSInformatica.NTSGroupBox
    Me.ckTb_fattrevch = New NTSInformatica.NTSCheckBox
    Me.lbTb_fattrevch = New NTSInformatica.NTSLabel
    Me.cbTb_fattrevch = New NTSInformatica.NTSComboBox
    Me.ckTb_fattextrc = New NTSInformatica.NTSCheckBox
    Me.fmSegue = New NTSInformatica.NTSGroupBox
    Me.fmPrestazione = New NTSInformatica.NTSGroupBox
    Me.cbTb_prestserv = New NTSInformatica.NTSComboBox
    Me.fmDocumento = New NTSInformatica.NTSGroupBox
    Me.lbXx_codcacadd = New NTSInformatica.NTSLabel
    Me.lbTb_codcacadd = New NTSInformatica.NTSLabel
    Me.edTb_codcacadd = New NTSInformatica.NTSTextBoxNum
    Me.pnRight = New NTSInformatica.NTSGroupBox
    Me.fmLeft = New NTSInformatica.NTSGroupBox
    Me.lbXx_tlistin = New NTSInformatica.NTSLabel
    Me.cmdTb_tiporkok = New NTSInformatica.NTSButton
    Me.edTb_tiporkok = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_tiporkok = New NTSInformatica.NTSLabel
    Me.ckTb_resoclfor = New NTSInformatica.NTSCheckBox
    Me.lbXx_codcauc = New NTSInformatica.NTSLabel
    Me.edTb_codcauc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codcauc = New NTSInformatica.NTSLabel
    Me.lbXx_coddivi = New NTSInformatica.NTSLabel
    Me.edTb_coddivi = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_coddivi = New NTSInformatica.NTSLabel
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_new506.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tcontro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tmagazz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tcaumag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tlistin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_tscorpo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_tprofor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tcautra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_vcodcen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tmagazz2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_flresocl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tcauscap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tmagimp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_flacconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_fattsosp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_przbol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_autotr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOpzioni.SuspendLayout()
    CType(Me.ckTb_fattrevch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_fattrevch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_fattextrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSegue, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSegue.SuspendLayout()
    CType(Me.fmPrestazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrestazione.SuspendLayout()
    CType(Me.cbTb_prestserv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDocumento.SuspendLayout()
    CType(Me.edTb_codcacadd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.fmLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmLeft.SuspendLayout()
    CType(Me.edTb_tiporkok.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_resoclfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcauc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_coddivi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbTb_codtpbf
    '
    Me.lbTb_codtpbf.AutoSize = True
    Me.lbTb_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codtpbf.Location = New System.Drawing.Point(15, 16)
    Me.lbTb_codtpbf.Name = "lbTb_codtpbf"
    Me.lbTb_codtpbf.NTSDbField = ""
    Me.lbTb_codtpbf.Size = New System.Drawing.Size(92, 13)
    Me.lbTb_codtpbf.TabIndex = 10
    Me.lbTb_codtpbf.Text = "Tipo Bolla/Fattura"
    Me.lbTb_codtpbf.Tooltip = ""
    Me.lbTb_codtpbf.UseMnemonic = False
    '
    'edTb_codtpbf
    '
    Me.edTb_codtpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codtpbf.EditValue = "0"
    Me.edTb_codtpbf.Location = New System.Drawing.Point(155, 13)
    Me.edTb_codtpbf.Name = "edTb_codtpbf"
    Me.edTb_codtpbf.NTSDbField = ""
    Me.edTb_codtpbf.NTSFormat = "0"
    Me.edTb_codtpbf.NTSForzaVisZoom = False
    Me.edTb_codtpbf.NTSOldValue = ""
    Me.edTb_codtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codtpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codtpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codtpbf.Properties.AutoHeight = False
    Me.edTb_codtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codtpbf.Properties.MaxLength = 65536
    Me.edTb_codtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codtpbf.Size = New System.Drawing.Size(64, 20)
    Me.edTb_codtpbf.TabIndex = 500
    '
    'lbTb_destpbf
    '
    Me.lbTb_destpbf.AutoSize = True
    Me.lbTb_destpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destpbf.Location = New System.Drawing.Point(15, 41)
    Me.lbTb_destpbf.Name = "lbTb_destpbf"
    Me.lbTb_destpbf.NTSDbField = ""
    Me.lbTb_destpbf.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_destpbf.TabIndex = 11
    Me.lbTb_destpbf.Text = "Descrizione"
    Me.lbTb_destpbf.Tooltip = ""
    Me.lbTb_destpbf.UseMnemonic = False
    '
    'edTb_destpbf
    '
    Me.edTb_destpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destpbf.EditValue = ""
    Me.edTb_destpbf.Location = New System.Drawing.Point(155, 38)
    Me.edTb_destpbf.Name = "edTb_destpbf"
    Me.edTb_destpbf.NTSDbField = ""
    Me.edTb_destpbf.NTSForzaVisZoom = False
    Me.edTb_destpbf.NTSOldValue = ""
    Me.edTb_destpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destpbf.Properties.AutoHeight = False
    Me.edTb_destpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destpbf.Properties.MaxLength = 65536
    Me.edTb_destpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destpbf.Size = New System.Drawing.Size(245, 20)
    Me.edTb_destpbf.TabIndex = 501
    '
    'ckTb_new506
    '
    Me.ckTb_new506.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_new506.Location = New System.Drawing.Point(5, 25)
    Me.ckTb_new506.Name = "ckTb_new506"
    Me.ckTb_new506.NTSCheckValue = "S"
    Me.ckTb_new506.NTSUnCheckValue = "N"
    Me.ckTb_new506.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_new506.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_new506.Properties.AutoHeight = False
    Me.ckTb_new506.Properties.Caption = "Deroga alla tabella personalizzata"
    Me.ckTb_new506.Size = New System.Drawing.Size(188, 19)
    Me.ckTb_new506.TabIndex = 502
    '
    'lbTb_tcontro
    '
    Me.lbTb_tcontro.AutoSize = True
    Me.lbTb_tcontro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tcontro.Location = New System.Drawing.Point(15, 67)
    Me.lbTb_tcontro.Name = "lbTb_tcontro"
    Me.lbTb_tcontro.NTSDbField = ""
    Me.lbTb_tcontro.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_tcontro.TabIndex = 13
    Me.lbTb_tcontro.Text = "Contropartita"
    Me.lbTb_tcontro.Tooltip = ""
    Me.lbTb_tcontro.UseMnemonic = False
    '
    'edTb_tcontro
    '
    Me.edTb_tcontro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tcontro.EditValue = "0"
    Me.edTb_tcontro.Location = New System.Drawing.Point(155, 64)
    Me.edTb_tcontro.Name = "edTb_tcontro"
    Me.edTb_tcontro.NTSDbField = ""
    Me.edTb_tcontro.NTSFormat = "0"
    Me.edTb_tcontro.NTSForzaVisZoom = False
    Me.edTb_tcontro.NTSOldValue = ""
    Me.edTb_tcontro.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tcontro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tcontro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tcontro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tcontro.Properties.AutoHeight = False
    Me.edTb_tcontro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tcontro.Properties.MaxLength = 65536
    Me.edTb_tcontro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tcontro.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tcontro.TabIndex = 503
    '
    'lbTb_tmagazz
    '
    Me.lbTb_tmagazz.AutoSize = True
    Me.lbTb_tmagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tmagazz.Location = New System.Drawing.Point(15, 93)
    Me.lbTb_tmagazz.Name = "lbTb_tmagazz"
    Me.lbTb_tmagazz.NTSDbField = ""
    Me.lbTb_tmagazz.Size = New System.Drawing.Size(66, 13)
    Me.lbTb_tmagazz.TabIndex = 14
    Me.lbTb_tmagazz.Text = "Magazzino 1"
    Me.lbTb_tmagazz.Tooltip = ""
    Me.lbTb_tmagazz.UseMnemonic = False
    '
    'edTb_tmagazz
    '
    Me.edTb_tmagazz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tmagazz.EditValue = "0"
    Me.edTb_tmagazz.Location = New System.Drawing.Point(155, 90)
    Me.edTb_tmagazz.Name = "edTb_tmagazz"
    Me.edTb_tmagazz.NTSDbField = ""
    Me.edTb_tmagazz.NTSFormat = "0"
    Me.edTb_tmagazz.NTSForzaVisZoom = False
    Me.edTb_tmagazz.NTSOldValue = ""
    Me.edTb_tmagazz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tmagazz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tmagazz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tmagazz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tmagazz.Properties.AutoHeight = False
    Me.edTb_tmagazz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tmagazz.Properties.MaxLength = 65536
    Me.edTb_tmagazz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tmagazz.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tmagazz.TabIndex = 504
    '
    'lbTb_tcaumag
    '
    Me.lbTb_tcaumag.AutoSize = True
    Me.lbTb_tcaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tcaumag.Location = New System.Drawing.Point(15, 145)
    Me.lbTb_tcaumag.Name = "lbTb_tcaumag"
    Me.lbTb_tcaumag.NTSDbField = ""
    Me.lbTb_tcaumag.Size = New System.Drawing.Size(98, 13)
    Me.lbTb_tcaumag.TabIndex = 15
    Me.lbTb_tcaumag.Text = "Causale Magazzino"
    Me.lbTb_tcaumag.Tooltip = ""
    Me.lbTb_tcaumag.UseMnemonic = False
    '
    'edTb_tcaumag
    '
    Me.edTb_tcaumag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tcaumag.EditValue = "0"
    Me.edTb_tcaumag.Location = New System.Drawing.Point(155, 142)
    Me.edTb_tcaumag.Name = "edTb_tcaumag"
    Me.edTb_tcaumag.NTSDbField = ""
    Me.edTb_tcaumag.NTSFormat = "0"
    Me.edTb_tcaumag.NTSForzaVisZoom = False
    Me.edTb_tcaumag.NTSOldValue = ""
    Me.edTb_tcaumag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tcaumag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tcaumag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tcaumag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tcaumag.Properties.AutoHeight = False
    Me.edTb_tcaumag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tcaumag.Properties.MaxLength = 65536
    Me.edTb_tcaumag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tcaumag.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tcaumag.TabIndex = 505
    '
    'lbTb_tlistin
    '
    Me.lbTb_tlistin.AutoSize = True
    Me.lbTb_tlistin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tlistin.Location = New System.Drawing.Point(15, 223)
    Me.lbTb_tlistin.Name = "lbTb_tlistin"
    Me.lbTb_tlistin.NTSDbField = ""
    Me.lbTb_tlistin.Size = New System.Drawing.Size(77, 13)
    Me.lbTb_tlistin.TabIndex = 16
    Me.lbTb_tlistin.Text = "Listino Testata"
    Me.lbTb_tlistin.Tooltip = ""
    Me.lbTb_tlistin.UseMnemonic = False
    '
    'edTb_tlistin
    '
    Me.edTb_tlistin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tlistin.EditValue = "0"
    Me.edTb_tlistin.Location = New System.Drawing.Point(155, 220)
    Me.edTb_tlistin.Name = "edTb_tlistin"
    Me.edTb_tlistin.NTSDbField = ""
    Me.edTb_tlistin.NTSFormat = "0"
    Me.edTb_tlistin.NTSForzaVisZoom = False
    Me.edTb_tlistin.NTSOldValue = ""
    Me.edTb_tlistin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tlistin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tlistin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tlistin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tlistin.Properties.AutoHeight = False
    Me.edTb_tlistin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tlistin.Properties.MaxLength = 65536
    Me.edTb_tlistin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tlistin.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tlistin.TabIndex = 506
    '
    'ckTb_tscorpo
    '
    Me.ckTb_tscorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_tscorpo.Location = New System.Drawing.Point(5, 45)
    Me.ckTb_tscorpo.Name = "ckTb_tscorpo"
    Me.ckTb_tscorpo.NTSCheckValue = "S"
    Me.ckTb_tscorpo.NTSUnCheckValue = "N"
    Me.ckTb_tscorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_tscorpo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_tscorpo.Properties.AutoHeight = False
    Me.ckTb_tscorpo.Properties.Caption = "Scorporo in testata bolla"
    Me.ckTb_tscorpo.Size = New System.Drawing.Size(142, 19)
    Me.ckTb_tscorpo.TabIndex = 507
    '
    'ckTb_tprofor
    '
    Me.ckTb_tprofor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_tprofor.Location = New System.Drawing.Point(5, 65)
    Me.ckTb_tprofor.Name = "ckTb_tprofor"
    Me.ckTb_tprofor.NTSCheckValue = "S"
    Me.ckTb_tprofor.NTSUnCheckValue = "N"
    Me.ckTb_tprofor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_tprofor.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_tprofor.Properties.AutoHeight = False
    Me.ckTb_tprofor.Properties.Caption = "Proforma"
    Me.ckTb_tprofor.Size = New System.Drawing.Size(88, 19)
    Me.ckTb_tprofor.TabIndex = 508
    '
    'lbTb_tcautra
    '
    Me.lbTb_tcautra.AutoSize = True
    Me.lbTb_tcautra.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tcautra.Location = New System.Drawing.Point(15, 249)
    Me.lbTb_tcautra.Name = "lbTb_tcautra"
    Me.lbTb_tcautra.NTSDbField = ""
    Me.lbTb_tcautra.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_tcautra.TabIndex = 19
    Me.lbTb_tcautra.Text = "Causale Trasporto"
    Me.lbTb_tcautra.Tooltip = ""
    Me.lbTb_tcautra.UseMnemonic = False
    '
    'edTb_tcautra
    '
    Me.edTb_tcautra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tcautra.EditValue = "0"
    Me.edTb_tcautra.Location = New System.Drawing.Point(155, 246)
    Me.edTb_tcautra.Name = "edTb_tcautra"
    Me.edTb_tcautra.NTSDbField = ""
    Me.edTb_tcautra.NTSFormat = "0"
    Me.edTb_tcautra.NTSForzaVisZoom = False
    Me.edTb_tcautra.NTSOldValue = ""
    Me.edTb_tcautra.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tcautra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tcautra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tcautra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tcautra.Properties.AutoHeight = False
    Me.edTb_tcautra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tcautra.Properties.MaxLength = 65536
    Me.edTb_tcautra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tcautra.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tcautra.TabIndex = 509
    '
    'lbTb_vcodcen
    '
    Me.lbTb_vcodcen.AutoSize = True
    Me.lbTb_vcodcen.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_vcodcen.Location = New System.Drawing.Point(15, 275)
    Me.lbTb_vcodcen.Name = "lbTb_vcodcen"
    Me.lbTb_vcodcen.NTSDbField = ""
    Me.lbTb_vcodcen.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_vcodcen.TabIndex = 20
    Me.lbTb_vcodcen.Text = "Centro C/A"
    Me.lbTb_vcodcen.Tooltip = ""
    Me.lbTb_vcodcen.UseMnemonic = False
    '
    'edTb_vcodcen
    '
    Me.edTb_vcodcen.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_vcodcen.EditValue = "0"
    Me.edTb_vcodcen.Location = New System.Drawing.Point(155, 272)
    Me.edTb_vcodcen.Name = "edTb_vcodcen"
    Me.edTb_vcodcen.NTSDbField = ""
    Me.edTb_vcodcen.NTSFormat = "0"
    Me.edTb_vcodcen.NTSForzaVisZoom = False
    Me.edTb_vcodcen.NTSOldValue = ""
    Me.edTb_vcodcen.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_vcodcen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_vcodcen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_vcodcen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_vcodcen.Properties.AutoHeight = False
    Me.edTb_vcodcen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_vcodcen.Properties.MaxLength = 65536
    Me.edTb_vcodcen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_vcodcen.Size = New System.Drawing.Size(64, 20)
    Me.edTb_vcodcen.TabIndex = 510
    '
    'lbTb_tmagazz2
    '
    Me.lbTb_tmagazz2.AutoSize = True
    Me.lbTb_tmagazz2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tmagazz2.Location = New System.Drawing.Point(15, 119)
    Me.lbTb_tmagazz2.Name = "lbTb_tmagazz2"
    Me.lbTb_tmagazz2.NTSDbField = ""
    Me.lbTb_tmagazz2.Size = New System.Drawing.Size(66, 13)
    Me.lbTb_tmagazz2.TabIndex = 21
    Me.lbTb_tmagazz2.Text = "Magazzino 2"
    Me.lbTb_tmagazz2.Tooltip = ""
    Me.lbTb_tmagazz2.UseMnemonic = False
    '
    'edTb_tmagazz2
    '
    Me.edTb_tmagazz2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tmagazz2.EditValue = "0"
    Me.edTb_tmagazz2.Location = New System.Drawing.Point(155, 116)
    Me.edTb_tmagazz2.Name = "edTb_tmagazz2"
    Me.edTb_tmagazz2.NTSDbField = ""
    Me.edTb_tmagazz2.NTSFormat = "0"
    Me.edTb_tmagazz2.NTSForzaVisZoom = False
    Me.edTb_tmagazz2.NTSOldValue = ""
    Me.edTb_tmagazz2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tmagazz2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tmagazz2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tmagazz2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tmagazz2.Properties.AutoHeight = False
    Me.edTb_tmagazz2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tmagazz2.Properties.MaxLength = 65536
    Me.edTb_tmagazz2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tmagazz2.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tmagazz2.TabIndex = 511
    '
    'cbTb_flresocl
    '
    Me.cbTb_flresocl.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_flresocl.DataSource = Nothing
    Me.cbTb_flresocl.DisplayMember = ""
    Me.cbTb_flresocl.Location = New System.Drawing.Point(5, 23)
    Me.cbTb_flresocl.Name = "cbTb_flresocl"
    Me.cbTb_flresocl.NTSDbField = ""
    Me.cbTb_flresocl.Properties.AutoHeight = False
    Me.cbTb_flresocl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_flresocl.Properties.DropDownRows = 30
    Me.cbTb_flresocl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_flresocl.SelectedValue = ""
    Me.cbTb_flresocl.Size = New System.Drawing.Size(249, 20)
    Me.cbTb_flresocl.TabIndex = 512
    Me.cbTb_flresocl.ValueMember = ""
    '
    'lbTb_tcauscap
    '
    Me.lbTb_tcauscap.AutoSize = True
    Me.lbTb_tcauscap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tcauscap.Location = New System.Drawing.Point(15, 171)
    Me.lbTb_tcauscap.Name = "lbTb_tcauscap"
    Me.lbTb_tcauscap.NTSDbField = ""
    Me.lbTb_tcauscap.Size = New System.Drawing.Size(137, 13)
    Me.lbTb_tcauscap.TabIndex = 23
    Me.lbTb_tcauscap.Text = "Causale scarico produzione"
    Me.lbTb_tcauscap.Tooltip = ""
    Me.lbTb_tcauscap.UseMnemonic = False
    '
    'edTb_tcauscap
    '
    Me.edTb_tcauscap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tcauscap.EditValue = "0"
    Me.edTb_tcauscap.Location = New System.Drawing.Point(155, 168)
    Me.edTb_tcauscap.Name = "edTb_tcauscap"
    Me.edTb_tcauscap.NTSDbField = ""
    Me.edTb_tcauscap.NTSFormat = "0"
    Me.edTb_tcauscap.NTSForzaVisZoom = False
    Me.edTb_tcauscap.NTSOldValue = ""
    Me.edTb_tcauscap.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tcauscap.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tcauscap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tcauscap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tcauscap.Properties.AutoHeight = False
    Me.edTb_tcauscap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tcauscap.Properties.MaxLength = 65536
    Me.edTb_tcauscap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tcauscap.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tcauscap.TabIndex = 513
    '
    'lbTb_tmagimp
    '
    Me.lbTb_tmagimp.AutoSize = True
    Me.lbTb_tmagimp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tmagimp.Location = New System.Drawing.Point(15, 197)
    Me.lbTb_tmagimp.Name = "lbTb_tmagimp"
    Me.lbTb_tmagimp.NTSDbField = ""
    Me.lbTb_tmagimp.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_tmagimp.TabIndex = 24
    Me.lbTb_tmagimp.Text = "Magazzino imp."
    Me.lbTb_tmagimp.Tooltip = ""
    Me.lbTb_tmagimp.UseMnemonic = False
    '
    'edTb_tmagimp
    '
    Me.edTb_tmagimp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tmagimp.EditValue = "0"
    Me.edTb_tmagimp.Location = New System.Drawing.Point(155, 194)
    Me.edTb_tmagimp.Name = "edTb_tmagimp"
    Me.edTb_tmagimp.NTSDbField = ""
    Me.edTb_tmagimp.NTSFormat = "0"
    Me.edTb_tmagimp.NTSForzaVisZoom = False
    Me.edTb_tmagimp.NTSOldValue = ""
    Me.edTb_tmagimp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tmagimp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tmagimp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tmagimp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tmagimp.Properties.AutoHeight = False
    Me.edTb_tmagimp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tmagimp.Properties.MaxLength = 65536
    Me.edTb_tmagimp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tmagimp.Size = New System.Drawing.Size(64, 20)
    Me.edTb_tmagimp.TabIndex = 514
    '
    'cbTb_flacconto
    '
    Me.cbTb_flacconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_flacconto.DataSource = Nothing
    Me.cbTb_flacconto.DisplayMember = ""
    Me.cbTb_flacconto.Location = New System.Drawing.Point(5, 23)
    Me.cbTb_flacconto.Name = "cbTb_flacconto"
    Me.cbTb_flacconto.NTSDbField = ""
    Me.cbTb_flacconto.Properties.AutoHeight = False
    Me.cbTb_flacconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_flacconto.Properties.DropDownRows = 30
    Me.cbTb_flacconto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_flacconto.SelectedValue = ""
    Me.cbTb_flacconto.Size = New System.Drawing.Size(249, 20)
    Me.cbTb_flacconto.TabIndex = 515
    Me.cbTb_flacconto.ValueMember = ""
    '
    'ckTb_fattsosp
    '
    Me.ckTb_fattsosp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_fattsosp.Location = New System.Drawing.Point(5, 85)
    Me.ckTb_fattsosp.Name = "ckTb_fattsosp"
    Me.ckTb_fattsosp.NTSCheckValue = "S"
    Me.ckTb_fattsosp.NTSUnCheckValue = "N"
    Me.ckTb_fattsosp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_fattsosp.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_fattsosp.Properties.AutoHeight = False
    Me.ckTb_fattsosp.Properties.Caption = "IVA ad esig.differita"
    Me.ckTb_fattsosp.Size = New System.Drawing.Size(125, 19)
    Me.ckTb_fattsosp.TabIndex = 516
    '
    'ckTb_przbol
    '
    Me.ckTb_przbol.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_przbol.Location = New System.Drawing.Point(5, 105)
    Me.ckTb_przbol.Name = "ckTb_przbol"
    Me.ckTb_przbol.NTSCheckValue = "S"
    Me.ckTb_przbol.NTSUnCheckValue = "N"
    Me.ckTb_przbol.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_przbol.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_przbol.Properties.AutoHeight = False
    Me.ckTb_przbol.Properties.Caption = "Prezzo e sconti in bolla"
    Me.ckTb_przbol.Size = New System.Drawing.Size(139, 19)
    Me.ckTb_przbol.TabIndex = 517
    '
    'ckTb_autotr
    '
    Me.ckTb_autotr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_autotr.Location = New System.Drawing.Point(5, 126)
    Me.ckTb_autotr.Name = "ckTb_autotr"
    Me.ckTb_autotr.NTSCheckValue = "S"
    Me.ckTb_autotr.NTSUnCheckValue = "N"
    Me.ckTb_autotr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_autotr.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_autotr.Properties.AutoHeight = False
    Me.ckTb_autotr.Properties.Caption = "Fatt./nota di accr. autotrasportatori"
    Me.ckTb_autotr.Size = New System.Drawing.Size(206, 19)
    Me.ckTb_autotr.TabIndex = 518
    '
    'lbXx_tcontro
    '
    Me.lbXx_tcontro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tcontro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tcontro.Location = New System.Drawing.Point(225, 64)
    Me.lbXx_tcontro.Name = "lbXx_tcontro"
    Me.lbXx_tcontro.NTSDbField = ""
    Me.lbXx_tcontro.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tcontro.TabIndex = 519
    Me.lbXx_tcontro.Tooltip = ""
    Me.lbXx_tcontro.UseMnemonic = False
    '
    'lbXx_tmagazz
    '
    Me.lbXx_tmagazz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tmagazz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tmagazz.Location = New System.Drawing.Point(225, 90)
    Me.lbXx_tmagazz.Name = "lbXx_tmagazz"
    Me.lbXx_tmagazz.NTSDbField = ""
    Me.lbXx_tmagazz.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tmagazz.TabIndex = 520
    Me.lbXx_tmagazz.Tooltip = ""
    Me.lbXx_tmagazz.UseMnemonic = False
    '
    'lbXx_tmagazz2
    '
    Me.lbXx_tmagazz2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tmagazz2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tmagazz2.Location = New System.Drawing.Point(225, 116)
    Me.lbXx_tmagazz2.Name = "lbXx_tmagazz2"
    Me.lbXx_tmagazz2.NTSDbField = ""
    Me.lbXx_tmagazz2.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tmagazz2.TabIndex = 521
    Me.lbXx_tmagazz2.Tooltip = ""
    Me.lbXx_tmagazz2.UseMnemonic = False
    '
    'lbXx_tcaumag
    '
    Me.lbXx_tcaumag.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tcaumag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tcaumag.Location = New System.Drawing.Point(225, 142)
    Me.lbXx_tcaumag.Name = "lbXx_tcaumag"
    Me.lbXx_tcaumag.NTSDbField = ""
    Me.lbXx_tcaumag.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tcaumag.TabIndex = 522
    Me.lbXx_tcaumag.Tooltip = ""
    Me.lbXx_tcaumag.UseMnemonic = False
    '
    'lbXx_tcautra
    '
    Me.lbXx_tcautra.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tcautra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tcautra.Location = New System.Drawing.Point(225, 246)
    Me.lbXx_tcautra.Name = "lbXx_tcautra"
    Me.lbXx_tcautra.NTSDbField = ""
    Me.lbXx_tcautra.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tcautra.TabIndex = 523
    Me.lbXx_tcautra.Tooltip = ""
    Me.lbXx_tcautra.UseMnemonic = False
    '
    'lbXx_vcodcen
    '
    Me.lbXx_vcodcen.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vcodcen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vcodcen.Location = New System.Drawing.Point(225, 272)
    Me.lbXx_vcodcen.Name = "lbXx_vcodcen"
    Me.lbXx_vcodcen.NTSDbField = ""
    Me.lbXx_vcodcen.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_vcodcen.TabIndex = 524
    Me.lbXx_vcodcen.Tooltip = ""
    Me.lbXx_vcodcen.UseMnemonic = False
    '
    'lbXx_tcauscap
    '
    Me.lbXx_tcauscap.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tcauscap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tcauscap.Location = New System.Drawing.Point(225, 168)
    Me.lbXx_tcauscap.Name = "lbXx_tcauscap"
    Me.lbXx_tcauscap.NTSDbField = ""
    Me.lbXx_tcauscap.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tcauscap.TabIndex = 525
    Me.lbXx_tcauscap.Tooltip = ""
    Me.lbXx_tcauscap.UseMnemonic = False
    '
    'lbXx_tmagimp
    '
    Me.lbXx_tmagimp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tmagimp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tmagimp.Location = New System.Drawing.Point(225, 194)
    Me.lbXx_tmagimp.Name = "lbXx_tmagimp"
    Me.lbXx_tmagimp.NTSDbField = ""
    Me.lbXx_tmagimp.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_tmagimp.TabIndex = 526
    Me.lbXx_tmagimp.Tooltip = ""
    Me.lbXx_tmagimp.UseMnemonic = False
    '
    'fmOpzioni
    '
    Me.fmOpzioni.AllowDrop = True
    Me.fmOpzioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOpzioni.Appearance.Options.UseBackColor = True
    Me.fmOpzioni.Controls.Add(Me.ckTb_fattrevch)
    Me.fmOpzioni.Controls.Add(Me.lbTb_fattrevch)
    Me.fmOpzioni.Controls.Add(Me.cbTb_fattrevch)
    Me.fmOpzioni.Controls.Add(Me.ckTb_fattextrc)
    Me.fmOpzioni.Controls.Add(Me.ckTb_new506)
    Me.fmOpzioni.Controls.Add(Me.ckTb_autotr)
    Me.fmOpzioni.Controls.Add(Me.ckTb_przbol)
    Me.fmOpzioni.Controls.Add(Me.ckTb_fattsosp)
    Me.fmOpzioni.Controls.Add(Me.ckTb_tscorpo)
    Me.fmOpzioni.Controls.Add(Me.ckTb_tprofor)
    Me.fmOpzioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOpzioni.Location = New System.Drawing.Point(3, 3)
    Me.fmOpzioni.Name = "fmOpzioni"
    Me.fmOpzioni.Size = New System.Drawing.Size(259, 196)
    Me.fmOpzioni.TabIndex = 527
    Me.fmOpzioni.Text = "Opzioni"
    '
    'ckTb_fattrevch
    '
    Me.ckTb_fattrevch.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_fattrevch.Location = New System.Drawing.Point(177, 101)
    Me.ckTb_fattrevch.Name = "ckTb_fattrevch"
    Me.ckTb_fattrevch.NTSCheckValue = "S"
    Me.ckTb_fattrevch.NTSUnCheckValue = "N"
    Me.ckTb_fattrevch.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_fattrevch.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_fattrevch.Properties.AutoHeight = False
    Me.ckTb_fattrevch.Properties.Caption = "non usato"
    Me.ckTb_fattrevch.Size = New System.Drawing.Size(66, 19)
    Me.ckTb_fattrevch.TabIndex = 520
    Me.ckTb_fattrevch.Visible = False
    '
    'lbTb_fattrevch
    '
    Me.lbTb_fattrevch.AutoSize = True
    Me.lbTb_fattrevch.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_fattrevch.Location = New System.Drawing.Point(2, 172)
    Me.lbTb_fattrevch.Name = "lbTb_fattrevch"
    Me.lbTb_fattrevch.NTSDbField = ""
    Me.lbTb_fattrevch.Size = New System.Drawing.Size(195, 13)
    Me.lbTb_fattrevch.TabIndex = 521
    Me.lbTb_fattrevch.Text = "Fatt./nota di accr. acq. reverse charge"
    Me.lbTb_fattrevch.Tooltip = ""
    Me.lbTb_fattrevch.UseMnemonic = False
    '
    'cbTb_fattrevch
    '
    Me.cbTb_fattrevch.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_fattrevch.DataSource = Nothing
    Me.cbTb_fattrevch.DisplayMember = ""
    Me.cbTb_fattrevch.Location = New System.Drawing.Point(203, 168)
    Me.cbTb_fattrevch.Name = "cbTb_fattrevch"
    Me.cbTb_fattrevch.NTSDbField = ""
    Me.cbTb_fattrevch.Properties.AutoHeight = False
    Me.cbTb_fattrevch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_fattrevch.Properties.DropDownRows = 30
    Me.cbTb_fattrevch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_fattrevch.SelectedValue = ""
    Me.cbTb_fattrevch.Size = New System.Drawing.Size(51, 20)
    Me.cbTb_fattrevch.TabIndex = 520
    Me.cbTb_fattrevch.ValueMember = ""
    '
    'ckTb_fattextrc
    '
    Me.ckTb_fattextrc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_fattextrc.Location = New System.Drawing.Point(5, 145)
    Me.ckTb_fattextrc.Name = "ckTb_fattextrc"
    Me.ckTb_fattextrc.NTSCheckValue = "S"
    Me.ckTb_fattextrc.NTSUnCheckValue = "N"
    Me.ckTb_fattextrc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_fattextrc.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_fattextrc.Properties.AutoHeight = False
    Me.ckTb_fattextrc.Properties.Caption = "Fatt./nota di accr. acquisto extracee"
    Me.ckTb_fattextrc.Size = New System.Drawing.Size(206, 19)
    Me.ckTb_fattextrc.TabIndex = 519
    '
    'fmSegue
    '
    Me.fmSegue.AllowDrop = True
    Me.fmSegue.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSegue.Appearance.Options.UseBackColor = True
    Me.fmSegue.Controls.Add(Me.cbTb_flresocl)
    Me.fmSegue.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSegue.Location = New System.Drawing.Point(3, 205)
    Me.fmSegue.Name = "fmSegue"
    Me.fmSegue.Size = New System.Drawing.Size(259, 52)
    Me.fmSegue.TabIndex = 528
    Me.fmSegue.Text = "Segue"
    '
    'fmPrestazione
    '
    Me.fmPrestazione.AllowDrop = True
    Me.fmPrestazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrestazione.Appearance.Options.UseBackColor = True
    Me.fmPrestazione.Controls.Add(Me.cbTb_prestserv)
    Me.fmPrestazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrestazione.Location = New System.Drawing.Point(3, 263)
    Me.fmPrestazione.Name = "fmPrestazione"
    Me.fmPrestazione.Size = New System.Drawing.Size(259, 54)
    Me.fmPrestazione.TabIndex = 529
    Me.fmPrestazione.Text = "Tipo prestazione"
    '
    'cbTb_prestserv
    '
    Me.cbTb_prestserv.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_prestserv.DataSource = Nothing
    Me.cbTb_prestserv.DisplayMember = ""
    Me.cbTb_prestserv.Location = New System.Drawing.Point(5, 24)
    Me.cbTb_prestserv.Name = "cbTb_prestserv"
    Me.cbTb_prestserv.NTSDbField = ""
    Me.cbTb_prestserv.Properties.AutoHeight = False
    Me.cbTb_prestserv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_prestserv.Properties.DropDownRows = 30
    Me.cbTb_prestserv.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_prestserv.SelectedValue = ""
    Me.cbTb_prestserv.Size = New System.Drawing.Size(249, 20)
    Me.cbTb_prestserv.TabIndex = 531
    Me.cbTb_prestserv.ValueMember = ""
    '
    'fmDocumento
    '
    Me.fmDocumento.AllowDrop = True
    Me.fmDocumento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDocumento.Appearance.Options.UseBackColor = True
    Me.fmDocumento.Controls.Add(Me.cbTb_flacconto)
    Me.fmDocumento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDocumento.Location = New System.Drawing.Point(3, 323)
    Me.fmDocumento.Name = "fmDocumento"
    Me.fmDocumento.Size = New System.Drawing.Size(259, 51)
    Me.fmDocumento.TabIndex = 530
    Me.fmDocumento.Text = "Documento d'acconto"
    '
    'lbXx_codcacadd
    '
    Me.lbXx_codcacadd.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcacadd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcacadd.Location = New System.Drawing.Point(225, 323)
    Me.lbXx_codcacadd.Name = "lbXx_codcacadd"
    Me.lbXx_codcacadd.NTSDbField = ""
    Me.lbXx_codcacadd.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_codcacadd.TabIndex = 542
    Me.lbXx_codcacadd.Tooltip = ""
    Me.lbXx_codcacadd.UseMnemonic = False
    '
    'lbTb_codcacadd
    '
    Me.lbTb_codcacadd.AutoSize = True
    Me.lbTb_codcacadd.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codcacadd.Location = New System.Drawing.Point(15, 327)
    Me.lbTb_codcacadd.Name = "lbTb_codcacadd"
    Me.lbTb_codcacadd.NTSDbField = ""
    Me.lbTb_codcacadd.Size = New System.Drawing.Size(133, 13)
    Me.lbTb_codcacadd.TabIndex = 540
    Me.lbTb_codcacadd.Text = "Causale CA spese di piede"
    Me.lbTb_codcacadd.Tooltip = ""
    Me.lbTb_codcacadd.UseMnemonic = False
    '
    'edTb_codcacadd
    '
    Me.edTb_codcacadd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcacadd.EditValue = "0"
    Me.edTb_codcacadd.Location = New System.Drawing.Point(155, 323)
    Me.edTb_codcacadd.Name = "edTb_codcacadd"
    Me.edTb_codcacadd.NTSDbField = ""
    Me.edTb_codcacadd.NTSFormat = "0"
    Me.edTb_codcacadd.NTSForzaVisZoom = False
    Me.edTb_codcacadd.NTSOldValue = ""
    Me.edTb_codcacadd.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcacadd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcacadd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcacadd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcacadd.Properties.AutoHeight = False
    Me.edTb_codcacadd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcacadd.Properties.MaxLength = 65536
    Me.edTb_codcacadd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcacadd.Size = New System.Drawing.Size(64, 20)
    Me.edTb_codcacadd.TabIndex = 541
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.fmOpzioni)
    Me.pnRight.Controls.Add(Me.fmSegue)
    Me.pnRight.Controls.Add(Me.fmDocumento)
    Me.pnRight.Controls.Add(Me.fmPrestazione)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(622, 30)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.ShowCaption = False
    Me.pnRight.Size = New System.Drawing.Size(283, 384)
    Me.pnRight.TabIndex = 544
    '
    'fmLeft
    '
    Me.fmLeft.AllowDrop = True
    Me.fmLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmLeft.Appearance.Options.UseBackColor = True
    Me.fmLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.fmLeft.Controls.Add(Me.lbXx_tlistin)
    Me.fmLeft.Controls.Add(Me.cmdTb_tiporkok)
    Me.fmLeft.Controls.Add(Me.edTb_tiporkok)
    Me.fmLeft.Controls.Add(Me.lbTb_tiporkok)
    Me.fmLeft.Controls.Add(Me.ckTb_resoclfor)
    Me.fmLeft.Controls.Add(Me.lbXx_codcauc)
    Me.fmLeft.Controls.Add(Me.edTb_codcauc)
    Me.fmLeft.Controls.Add(Me.lbTb_codcauc)
    Me.fmLeft.Controls.Add(Me.lbXx_codcacadd)
    Me.fmLeft.Controls.Add(Me.lbXx_coddivi)
    Me.fmLeft.Controls.Add(Me.edTb_codcacadd)
    Me.fmLeft.Controls.Add(Me.lbTb_codcacadd)
    Me.fmLeft.Controls.Add(Me.edTb_coddivi)
    Me.fmLeft.Controls.Add(Me.lbTb_coddivi)
    Me.fmLeft.Controls.Add(Me.lbTb_codtpbf)
    Me.fmLeft.Controls.Add(Me.edTb_tmagimp)
    Me.fmLeft.Controls.Add(Me.lbTb_tcauscap)
    Me.fmLeft.Controls.Add(Me.lbXx_tmagimp)
    Me.fmLeft.Controls.Add(Me.lbTb_tmagimp)
    Me.fmLeft.Controls.Add(Me.lbXx_tcontro)
    Me.fmLeft.Controls.Add(Me.edTb_tcauscap)
    Me.fmLeft.Controls.Add(Me.lbXx_tmagazz2)
    Me.fmLeft.Controls.Add(Me.lbTb_tmagazz2)
    Me.fmLeft.Controls.Add(Me.lbTb_destpbf)
    Me.fmLeft.Controls.Add(Me.edTb_tmagazz2)
    Me.fmLeft.Controls.Add(Me.lbXx_tcautra)
    Me.fmLeft.Controls.Add(Me.edTb_codtpbf)
    Me.fmLeft.Controls.Add(Me.lbXx_tmagazz)
    Me.fmLeft.Controls.Add(Me.edTb_tcontro)
    Me.fmLeft.Controls.Add(Me.lbXx_tcauscap)
    Me.fmLeft.Controls.Add(Me.edTb_tmagazz)
    Me.fmLeft.Controls.Add(Me.lbXx_tcaumag)
    Me.fmLeft.Controls.Add(Me.lbTb_tcontro)
    Me.fmLeft.Controls.Add(Me.lbXx_vcodcen)
    Me.fmLeft.Controls.Add(Me.edTb_tcautra)
    Me.fmLeft.Controls.Add(Me.edTb_destpbf)
    Me.fmLeft.Controls.Add(Me.edTb_tcaumag)
    Me.fmLeft.Controls.Add(Me.lbTb_tlistin)
    Me.fmLeft.Controls.Add(Me.edTb_vcodcen)
    Me.fmLeft.Controls.Add(Me.lbTb_tcaumag)
    Me.fmLeft.Controls.Add(Me.lbTb_tcautra)
    Me.fmLeft.Controls.Add(Me.edTb_tlistin)
    Me.fmLeft.Controls.Add(Me.lbTb_vcodcen)
    Me.fmLeft.Controls.Add(Me.lbTb_tmagazz)
    Me.fmLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.fmLeft.Location = New System.Drawing.Point(0, 30)
    Me.fmLeft.Name = "fmLeft"
    Me.fmLeft.ShowCaption = False
    Me.fmLeft.Size = New System.Drawing.Size(616, 384)
    Me.fmLeft.TabIndex = 545
    '
    'lbXx_tlistin
    '
    Me.lbXx_tlistin.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tlistin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tlistin.Location = New System.Drawing.Point(225, 220)
    Me.lbXx_tlistin.Name = "lbXx_tlistin"
    Me.lbXx_tlistin.NTSDbField = ""
    Me.lbXx_tlistin.Size = New System.Drawing.Size(235, 20)
    Me.lbXx_tlistin.TabIndex = 594
    Me.lbXx_tlistin.Tooltip = ""
    Me.lbXx_tlistin.UseMnemonic = False
    '
    'cmdTb_tiporkok
    '
    Me.cmdTb_tiporkok.ImagePath = ""
    Me.cmdTb_tiporkok.ImageText = ""
    Me.cmdTb_tiporkok.Location = New System.Drawing.Point(573, 13)
    Me.cmdTb_tiporkok.Name = "cmdTb_tiporkok"
    Me.cmdTb_tiporkok.NTSContextMenu = Nothing
    Me.cmdTb_tiporkok.Size = New System.Drawing.Size(29, 20)
    Me.cmdTb_tiporkok.TabIndex = 592
    Me.cmdTb_tiporkok.Text = "..."
    '
    'edTb_tiporkok
    '
    Me.edTb_tiporkok.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tiporkok.EditValue = ""
    Me.edTb_tiporkok.Enabled = False
    Me.edTb_tiporkok.Location = New System.Drawing.Point(490, 13)
    Me.edTb_tiporkok.Name = "edTb_tiporkok"
    Me.edTb_tiporkok.NTSDbField = ""
    Me.edTb_tiporkok.NTSForzaVisZoom = False
    Me.edTb_tiporkok.NTSOldValue = ""
    Me.edTb_tiporkok.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tiporkok.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tiporkok.Properties.AutoHeight = False
    Me.edTb_tiporkok.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tiporkok.Properties.MaxLength = 65536
    Me.edTb_tiporkok.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tiporkok.Size = New System.Drawing.Size(83, 20)
    Me.edTb_tiporkok.TabIndex = 552
    '
    'lbTb_tiporkok
    '
    Me.lbTb_tiporkok.AutoSize = True
    Me.lbTb_tiporkok.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tiporkok.Location = New System.Drawing.Point(321, 16)
    Me.lbTb_tiporkok.Name = "lbTb_tiporkok"
    Me.lbTb_tiporkok.NTSDbField = ""
    Me.lbTb_tiporkok.Size = New System.Drawing.Size(163, 13)
    Me.lbTb_tiporkok.TabIndex = 551
    Me.lbTb_tiporkok.Text = "Mostra in zoom documenti di tipo"
    Me.lbTb_tiporkok.Tooltip = ""
    Me.lbTb_tiporkok.UseMnemonic = False
    '
    'ckTb_resoclfor
    '
    Me.ckTb_resoclfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_resoclfor.Location = New System.Drawing.Point(466, 221)
    Me.ckTb_resoclfor.Name = "ckTb_resoclfor"
    Me.ckTb_resoclfor.NTSCheckValue = "S"
    Me.ckTb_resoclfor.NTSUnCheckValue = "N"
    Me.ckTb_resoclfor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_resoclfor.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_resoclfor.Properties.AutoHeight = False
    Me.ckTb_resoclfor.Properties.Caption = "Forza listino (solo GPV)"
    Me.ckTb_resoclfor.Size = New System.Drawing.Size(136, 19)
    Me.ckTb_resoclfor.TabIndex = 550
    '
    'lbXx_codcauc
    '
    Me.lbXx_codcauc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcauc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcauc.Location = New System.Drawing.Point(225, 349)
    Me.lbXx_codcauc.Name = "lbXx_codcauc"
    Me.lbXx_codcauc.NTSDbField = ""
    Me.lbXx_codcauc.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_codcauc.TabIndex = 549
    Me.lbXx_codcauc.Tooltip = ""
    Me.lbXx_codcauc.UseMnemonic = False
    '
    'edTb_codcauc
    '
    Me.edTb_codcauc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcauc.EditValue = "0"
    Me.edTb_codcauc.Location = New System.Drawing.Point(155, 349)
    Me.edTb_codcauc.Name = "edTb_codcauc"
    Me.edTb_codcauc.NTSDbField = ""
    Me.edTb_codcauc.NTSFormat = "0"
    Me.edTb_codcauc.NTSForzaVisZoom = False
    Me.edTb_codcauc.NTSOldValue = ""
    Me.edTb_codcauc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcauc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcauc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcauc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcauc.Properties.AutoHeight = False
    Me.edTb_codcauc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcauc.Properties.MaxLength = 65536
    Me.edTb_codcauc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcauc.Size = New System.Drawing.Size(64, 20)
    Me.edTb_codcauc.TabIndex = 548
    '
    'lbTb_codcauc
    '
    Me.lbTb_codcauc.AutoSize = True
    Me.lbTb_codcauc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codcauc.Location = New System.Drawing.Point(15, 353)
    Me.lbTb_codcauc.Name = "lbTb_codcauc"
    Me.lbTb_codcauc.NTSDbField = ""
    Me.lbTb_codcauc.Size = New System.Drawing.Size(137, 13)
    Me.lbTb_codcauc.TabIndex = 547
    Me.lbTb_codcauc.Text = "Causale CG per contabilizz."
    Me.lbTb_codcauc.Tooltip = ""
    Me.lbTb_codcauc.UseMnemonic = False
    '
    'lbXx_coddivi
    '
    Me.lbXx_coddivi.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddivi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddivi.Location = New System.Drawing.Point(225, 297)
    Me.lbXx_coddivi.Name = "lbXx_coddivi"
    Me.lbXx_coddivi.NTSDbField = ""
    Me.lbXx_coddivi.Size = New System.Drawing.Size(377, 20)
    Me.lbXx_coddivi.TabIndex = 546
    Me.lbXx_coddivi.Tooltip = ""
    Me.lbXx_coddivi.UseMnemonic = False
    '
    'edTb_coddivi
    '
    Me.edTb_coddivi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_coddivi.EditValue = "0"
    Me.edTb_coddivi.Location = New System.Drawing.Point(155, 297)
    Me.edTb_coddivi.Name = "edTb_coddivi"
    Me.edTb_coddivi.NTSDbField = ""
    Me.edTb_coddivi.NTSFormat = "0"
    Me.edTb_coddivi.NTSForzaVisZoom = False
    Me.edTb_coddivi.NTSOldValue = ""
    Me.edTb_coddivi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_coddivi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_coddivi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_coddivi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_coddivi.Properties.AutoHeight = False
    Me.edTb_coddivi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_coddivi.Properties.MaxLength = 65536
    Me.edTb_coddivi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_coddivi.Size = New System.Drawing.Size(64, 20)
    Me.edTb_coddivi.TabIndex = 545
    '
    'lbTb_coddivi
    '
    Me.lbTb_coddivi.AutoSize = True
    Me.lbTb_coddivi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_coddivi.Location = New System.Drawing.Point(15, 300)
    Me.lbTb_coddivi.Name = "lbTb_coddivi"
    Me.lbTb_coddivi.NTSDbField = ""
    Me.lbTb_coddivi.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_coddivi.TabIndex = 544
    Me.lbTb_coddivi.Text = "Divisione C/A"
    Me.lbTb_coddivi.Tooltip = ""
    Me.lbTb_coddivi.UseMnemonic = False
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 26
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'FRMVETPBF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(905, 414)
    Me.Controls.Add(Me.fmLeft)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVETPBF"
    Me.Text = "TIPI BOLLE E FATTURE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_new506.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tcontro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tmagazz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tcaumag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tlistin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_tscorpo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_tprofor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tcautra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_vcodcen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tmagazz2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_flresocl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tcauscap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tmagimp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_flacconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_fattsosp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_przbol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_autotr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOpzioni.ResumeLayout(False)
    Me.fmOpzioni.PerformLayout()
    CType(Me.ckTb_fattrevch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_fattrevch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_fattextrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSegue, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSegue.ResumeLayout(False)
    CType(Me.fmPrestazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrestazione.ResumeLayout(False)
    CType(Me.cbTb_prestserv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDocumento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDocumento.ResumeLayout(False)
    CType(Me.edTb_codcacadd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.fmLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmLeft.ResumeLayout(False)
    Me.fmLeft.PerformLayout()
    CType(Me.edTb_tiporkok.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_resoclfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcauc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_coddivi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRMVETPBF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleTpbf.Apri(DittaCorrente, dsTpbf) Then Me.Close()
      dcTpbf.DataSource = dsTpbf.Tables("TABTPBF")
      dsTpbf.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      If dsTpbf.Tables("TABTPBF").Rows.Count > 0 Then edTb_codtpbf.Enabled = False
      If dsTpbf.Tables("TABTPBF").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcTpbf.ResetBindings(False)
      dcTpbf.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsTpbf.Tables("TABTPBF").Rows.Count = 1 And dsTpbf.Tables("TABTPBF").Rows(0).RowState = DataRowState.Added Then
            'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcTpbf.List.Count - 1
            If CType(dcTpbf.Item(i), DataRowView)!tb_codtpbf.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcTpbf.Position = i
              Exit For
            End If
          Next
        End If
      End If

      ckTb_resoclfor.Visible = CBool(bsModSupGPV And oCleTpbf.ModuliSupDittaDitt(DittaCorrente))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVETPBF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMVETPBF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTpbf.Dispose()
      dsTpbf.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva() Then Return
      oCleTpbf.Nuovo()
      dcTpbf.MoveLast()
      edTb_codtpbf.Enabled = True
      edTb_codtpbf.Focus()
      '--------------------------------------------------------------------------------------------------------------
      Me.GctlApplicaDefaultValue()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim frmDuar As FRM__DUAR = Nothing
    Dim strCodOld As String = ""
    Try
      If Not Salva() Then Return

      'test su riga valida
      If edTb_codtpbf.Text = "0" Or edTb_codtpbf.Text = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130723482390952496, "Posizionarsi su un record valido"))
        Return
      Else
        strCodOld = edTb_codtpbf.Text
      End If

      frmDuar = CType(NTSNewFormModal("FRM__DUAR"), FRM__DUAR)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oCleTpbf = oCleTpbf
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or frmDuar.strCodDupl = "" Then
        'annullato
        Return
      End If

      If Not oCleTpbf.Duplica(frmDuar.strCodDupl, strCodOld) Then
        Return
      End If

      dcTpbf.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDuar Is Nothing Then frmDuar.Dispose()
      frmDuar = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491173732031440, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No : Return
        Case Windows.Forms.DialogResult.Yes
          If dsTpbf.Tables("TABTPBF").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          dcTpbf.RemoveAt(dcTpbf.Position)
          oCleTpbf.Salva(True)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcTpbf, Me)
            bRemovBinding = False
            edTb_codtpbf.Enabled = True
          Else
            edTb_codtpbf.Enabled = False
          End If
          Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcTpbf, Me)
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491173732187300, "Ripristinare le modifiche apportate?"))
      Else
        dlgRes = Windows.Forms.DialogResult.Yes
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsTpbf.Tables("TABTPBF").Rows.Count = 1 And dsTpbf.Tables("TABTPBF").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          oCleTpbf.Ripristina(dcTpbf.Position, dcTpbf.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcTpbf, Me)
            bRemovBinding = False
            edTb_codtpbf.Enabled = True
          Else
            edTb_codtpbf.Enabled = False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcTpbf, Me)
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB
      If edTb_destpbf.Focused Then
        Salva()
        SetFastZoom(edTb_codtpbf.Text, oParam)
        NTSZOOM.strIn = edTb_codtpbf.Text
        NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
        PosizionaAfterZoom(NTSZOOM.strIn)
      Else
        NTSCallStandardZoom()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    If Not Salva() Then Return
    dcTpbf.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    If Not Salva() Then Return
    dcTpbf.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    If Not Salva() Then Return
    dcTpbf.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    If Not Salva() Then Return
    dcTpbf.MoveLast()
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edTb_codtpbf.NTSDbField = "TABTPBF.tb_codtpbf"
      edTb_destpbf.NTSDbField = "TABTPBF.tb_destpbf"
      ckTb_new506.NTSText.NTSDbField = "TABTPBF.tb_new506"
      edTb_tcontro.NTSDbField = "TABTPBF.tb_tcontro"
      edTb_tmagazz.NTSDbField = "TABTPBF.tb_tmagazz"
      edTb_tcaumag.NTSDbField = "TABTPBF.tb_tcaumag"
      edTb_tlistin.NTSDbField = "TABTPBF.tb_tlistin"
      lbXx_tlistin.NTSDbField = "TABTPBF.xx_tlistin"
      ckTb_tscorpo.NTSText.NTSDbField = "TABTPBF.tb_tscorpo"
      ckTb_tprofor.NTSText.NTSDbField = "TABTPBF.tb_tprofor"
      edTb_tcautra.NTSDbField = "TABTPBF.tb_tcautra"
      edTb_vcodcen.NTSDbField = "TABTPBF.tb_vcodcen"
      edTb_tmagazz2.NTSDbField = "TABTPBF.tb_tmagazz2"
      cbTb_flresocl.NTSDbField = "TABTPBF.tb_flresocl"
      edTb_tcauscap.NTSDbField = "TABTPBF.tb_tcauscap"
      edTb_tmagimp.NTSDbField = "TABTPBF.tb_tmagimp"
      cbTb_flacconto.NTSDbField = "TABTPBF.tb_flacconto"
      ckTb_fattsosp.NTSText.NTSDbField = "TABTPBF.tb_fattsosp"
      ckTb_przbol.NTSText.NTSDbField = "TABTPBF.tb_przbol"
      ckTb_autotr.NTSText.NTSDbField = "TABTPBF.tb_autotr"
      cbTb_fattrevch.NTSDbField = "TABTPBF.tb_fattrevch"
      lbXx_tcontro.NTSDbField = "TABTPBF.xx_tcontro"
      lbXx_tmagazz.NTSDbField = "TABTPBF.xx_tmagazz"
      lbXx_tmagazz2.NTSDbField = "TABTPBF.xx_tmagazz2"
      lbXx_tcaumag.NTSDbField = "TABTPBF.xx_tcaumag"
      lbXx_tcautra.NTSDbField = "TABTPBF.xx_tcautra"
      lbXx_vcodcen.NTSDbField = "TABTPBF.xx_vcodcen"
      lbXx_tcauscap.NTSDbField = "TABTPBF.xx_tcauscap"
      lbXx_tmagimp.NTSDbField = "TABTPBF.xx_tmagimp"
      cbTb_prestserv.NTSDbField = "TABTPBF.tb_prestserv"
      edTb_codcacadd.NTSDbField = "TABTPBF.tb_codcacadd"
      lbXx_codcacadd.NTSDbField = "TABTPBF.xx_codcacadd"
      edTb_coddivi.NTSDbField = "TABTPBF.tb_coddivi"
      lbXx_coddivi.NTSDbField = "TABTPBF.xx_coddivi"
      ckTb_fattextrc.NTSText.NTSDbField = "TABTPBF.tb_fattextrc"
      edTb_codcauc.NTSDbField = "TABTPBF.tb_codcauc"
      lbXx_codcauc.NTSDbField = "TABTPBF.xx_codcauc"
      ckTb_resoclfor.NTSText.NTSDbField = "TABTPBF.tb_resoclfor"
      edTb_tiporkok.NTSDbField = "TABTPBF.tb_tiporkok"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcTpbf, Me)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttTb_flresocl As New DataTable()
    Dim dttTb_prestserv As New DataTable()
    Dim dttTb_flacconto As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTb_flresocl.Columns.Add("cod", GetType(String))
      dttTb_flresocl.Columns.Add("val", GetType(String))
      dttTb_flresocl.Rows.Add(New Object() {"N", "(Nessun documento)"})
      dttTb_flresocl.Rows.Add(New Object() {"S", "Fattura"})
      dttTb_flresocl.Rows.Add(New Object() {"F", "Fattura ricevuta fiscale"})
      dttTb_flresocl.AcceptChanges()
      cbTb_flresocl.DataSource = dttTb_flresocl
      cbTb_flresocl.ValueMember = "cod"
      cbTb_flresocl.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttTb_prestserv.Columns.Add("cod", GetType(String))
      dttTb_prestserv.Columns.Add("val", GetType(String))
      dttTb_prestserv.Rows.Add(New Object() {"N", "Cessione di beni"})
      dttTb_prestserv.Rows.Add(New Object() {"S", "Prestazione di servizi"})
      dttTb_prestserv.Rows.Add(New Object() {"A", "Noleggio/Leasing Autovettura"})
      dttTb_prestserv.Rows.Add(New Object() {"B", "Noleggio/Leasing Caravan"})
      dttTb_prestserv.Rows.Add(New Object() {"C", "Noleggio/Leasing Altri veicoli"})
      dttTb_prestserv.Rows.Add(New Object() {"D", "Noleggio/Leasing Unità da diporto"})
      dttTb_prestserv.Rows.Add(New Object() {"E", "Noleggio/Leasing Aeromobili"})
      dttTb_prestserv.AcceptChanges()
      cbTb_prestserv.DataSource = dttTb_prestserv
      cbTb_prestserv.ValueMember = "cod"
      cbTb_prestserv.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttTb_flacconto.Columns.Add("cod", GetType(String))
      dttTb_flacconto.Columns.Add("val", GetType(String))
      dttTb_flacconto.Rows.Add(New Object() {"N", "No"})
      dttTb_flacconto.Rows.Add(New Object() {"S", "Fatt./Ricevute fiscali d'acconto"})
      dttTb_flacconto.Rows.Add(New Object() {"E", "Chiude documenti d'acconto"})
      dttTb_flacconto.AcceptChanges()
      cbTb_flacconto.DataSource = dttTb_flacconto
      cbTb_flacconto.ValueMember = "cod"
      cbTb_flacconto.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub PosizionaAfterZoom(ByVal strZoom As String)
    Dim i As Integer
    Dim lPos As Integer

    Try
      lPos = dcTpbf.Position
      dcTpbf.MoveFirst()
      For i = 0 To dcTpbf.Count - 1
        If dsTpbf.Tables("TABTPBF").Rows(dcTpbf.Position)("tb_codtpbf").ToString = strZoom Then
          Exit Sub
        End If
        dcTpbf.MoveNext()
      Next
      dcTpbf.Position = lPos
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleTpbf.RecordIsChanged Then
        If GctlControllaOutNotEqual() = False Then Return False
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491173732343160, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleTpbf.Salva(False) Then Return False
          If dsTpbf.Tables("TABTPBF").Rows.Count > 0 Then
            edTb_codtpbf.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{tabtpbf.codditt} = " & CStrSQL(DittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVETPBF", "Reports1", " ", 0, nDestin, "BSVETPBF.RPT", False, "TIPI BOLLE E FATTURE", False)
      '--------------------------------------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdTb_tiporkok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTb_tiporkok.Click
    Dim frmTprk As FRM__TPRK = Nothing
    Dim strT() As String = Nothing
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try
      frmTprk = CType(NTSNewFormModal("FRM__TPRK"), FRM__TPRK)
      frmTprk.Init(oMenu, Nothing, DittaCorrente, Nothing)

      If edTb_tiporkok.Text.Trim = "" Then
        frmTprk.cmdSelAll_Click(Nothing, Nothing)
      Else
        frmTprk.cmdDeselAll_Click(Nothing, Nothing)
        strT = edTb_tiporkok.Text.Trim.Split(";"c)
        For Each strTmp In strT
          If strTmp.Trim = "" Then Continue For
          Select Case strTmp
            Case "H" : frmTprk.ckH.Checked = True
            Case "$" : frmTprk.ckO1.Checked = True
            Case "O" : frmTprk.ckO.Checked = True
            Case "Z" : frmTprk.ckZ.Checked = True
            Case "W" : frmTprk.ckW.Checked = True
            Case "T" : frmTprk.ckT.Checked = True
            Case "M" : frmTprk.ckM.Checked = True
            Case "J" : frmTprk.ckJ.Checked = True
            Case "L" : frmTprk.ckL.Checked = True
            Case "#" : frmTprk.CkR1.Checked = True
            Case "X" : frmTprk.ckX.Checked = True
            Case "V" : frmTprk.ckV.Checked = True
            Case "R" : frmTprk.ckR.Checked = True
            Case "Q" : frmTprk.ckQ.Checked = True
            Case "S" : frmTprk.ckS.Checked = True
            Case "N" : frmTprk.ckN.Checked = True
            Case "I" : frmTprk.ckI.Checked = True
            Case "F" : frmTprk.ckF.Checked = True
            Case "E" : frmTprk.ckE.Checked = True
            Case "C" : frmTprk.ckC.Checked = True
            Case "B" : frmTprk.ckB.Checked = True
            Case "A" : frmTprk.ckA.Checked = True
          End Select
        Next
      End If

      frmTprk.ShowDialog()
      If frmTprk.bOk = False Then
        Return
      Else
        strTmp = ""
        bOk = True
        If frmTprk.ckH.Checked Then strTmp += "H;" Else bOk = False
        If frmTprk.ckO1.Checked Then strTmp += "$;" Else bOk = False
        If frmTprk.ckO.Checked Then strTmp += "O;" Else bOk = False
        If frmTprk.ckZ.Checked Then strTmp += "Z;" Else bOk = False
        If frmTprk.ckW.Checked Then strTmp += "W;" Else bOk = False
        If frmTprk.ckT.Checked Then strTmp += "T;" Else bOk = False
        If frmTprk.ckM.Checked Then strTmp += "M;" Else bOk = False
        If frmTprk.ckJ.Checked Then strTmp += "J;" Else bOk = False
        If frmTprk.ckL.Checked Then strTmp += "L;" Else bOk = False
        If frmTprk.CkR1.Checked Then strTmp += "#;" Else bOk = False
        If frmTprk.ckX.Checked Then strTmp += "X;" Else bOk = False
        If frmTprk.ckV.Checked Then strTmp += "V;" Else bOk = False
        If frmTprk.ckR.Checked Then strTmp += "R;" Else bOk = False
        If frmTprk.ckQ.Checked Then strTmp += "Q;" Else bOk = False
        If frmTprk.ckS.Checked Then strTmp += "S;" Else bOk = False
        If frmTprk.ckN.Checked Then strTmp += "N;" Else bOk = False
        If frmTprk.ckI.Checked Then strTmp += "I;" Else bOk = False
        If frmTprk.ckF.Checked Then strTmp += "F;" Else bOk = False
        If frmTprk.ckE.Checked Then strTmp += "E;" Else bOk = False
        If frmTprk.ckC.Checked Then strTmp += "C;" Else bOk = False
        If frmTprk.ckB.Checked Then strTmp += "B;" Else bOk = False
        If frmTprk.ckA.Checked Then strTmp += "A;" Else bOk = False
        If bOk Then
          'ho scelto tutti i tipirk
          strTmp = ""
        Else
          If strTmp = "" Then
            'nessun tipork
            strTmp = "-"
          Else
            'solo alcuni tipirk
            strTmp = strTmp.Substring(0, strTmp.Length - 1)
          End If

        End If
        edTb_tiporkok.NTSTextDB = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmTprk Is Nothing Then frmTprk.Dispose()
      frmTprk = Nothing
    End Try
  End Sub
End Class

