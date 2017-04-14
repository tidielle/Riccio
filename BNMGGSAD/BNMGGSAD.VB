Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGSAD
  Private Moduli_P As Integer = CLN__STD.bsModMG
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
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

#Region "VARIABILI"
  Public oCleGsad As CLEMGGSAD
  Public dsGsad As DataSet
  Public oCallParams As CLE__CLDP
  Public dcGsad As BindingSource = New BindingSource
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbXx_fase As NTSInformatica.NTSLabel
  Public WithEvents edAd_fase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edAd_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAd_fase As NTSInformatica.NTSLabel
  Public WithEvents lbAd_codart As NTSInformatica.NTSLabel
  Public WithEvents lbXx_magaz As NTSInformatica.NTSLabel
  Public WithEvents edAd_magaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAd_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbAd_sommat As NTSInformatica.NTSLabel
  Public WithEvents edAd_sommat As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents tsGsad As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pbIndex1 As NTSInformatica.NTSPanel
  Public WithEvents edAd_rescli As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_carvar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_carpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_carfor As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_esist As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAd_rescli As NTSInformatica.NTSLabel
  Public WithEvents lbAd_carvar As NTSInformatica.NTSLabel
  Public WithEvents lbAd_carpro As NTSInformatica.NTSLabel
  Public WithEvents lbAd_carfor As NTSInformatica.NTSLabel
  Public WithEvents lbAd_esist As NTSInformatica.NTSLabel
  Public WithEvents lbAd_giaini As NTSInformatica.NTSLabel
  Public WithEvents lbAd_resfor As NTSInformatica.NTSLabel
  Public WithEvents lbAd_scavar As NTSInformatica.NTSLabel
  Public WithEvents lbAd_scapro As NTSInformatica.NTSLabel
  Public WithEvents lbAd_scacli As NTSInformatica.NTSLabel
  Public WithEvents edAd_giaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_resfor As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_scavar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_scapro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_scacli As NTSInformatica.NTSTextBoxNum
  Public WithEvents pbIndex2 As NTSInformatica.NTSPanel
  Public WithEvents edAd_vgiaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vresfor As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vscavar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vscapro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vscacli As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAd_vgiaini As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vresfor As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vscavar As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vscapro As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vscacli As NTSInformatica.NTSLabel
  Public WithEvents edAd_vrescli As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vcarvar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vcarpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_vcarfor As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAd_valesi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAd_vrescli As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vcarvar As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vcarpro As NTSInformatica.NTSLabel
  Public WithEvents lbAd_vcarfor As NTSInformatica.NTSLabel
  Public WithEvents lbAd_valesi As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipval As NTSInformatica.NTSLabel
  Public WithEvents edAd_tipval As NTSInformatica.NTSTextBoxStr
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
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
    '------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGSAD", "BEMGGSAD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128548169577077855, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsad = CType(oTmp, CLEMGGSAD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGSAD", strRemoteServer, strRemotePort)
    AddHandler oCleGsad.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsad.Init(oApp, NTSScript, oMenu.oCleComm, "ARTDEF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edAd_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128548212960857134, "Articolo"), tabartico, False)
      edAd_fase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128548212992056309, "Fase"), tabartfasi)
      edAd_fase.ArtiPerFase = edAd_codart
      edAd_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128548213084159789, "Magazzino"), tabmaga)
      edAd_sommat.NTSSetParam(oMenu, oApp.Tr(Me, 128548213412205199, "Sommatoria"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_tipval.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128980161210357885, "Valorizzazione"), tablist, False)
      edAd_esist.NTSSetParam(oMenu, oApp.Tr(Me, 128548213640061709, "Esistenza (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_carfor.NTSSetParam(oMenu, oApp.Tr(Me, 128548213989687769, "Carichi fornitore (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_carpro.NTSSetParam(oMenu, oApp.Tr(Me, 128548214206382884, "Carichi produzione (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_carvar.NTSSetParam(oMenu, oApp.Tr(Me, 128548214374282294, "Carichi varie (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_rescli.NTSSetParam(oMenu, oApp.Tr(Me, 128548214601338074, "Resi da clienti (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_scacli.NTSSetParam(oMenu, oApp.Tr(Me, 128548214943728269, "Scarichi a clienti (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_scapro.NTSSetParam(oMenu, oApp.Tr(Me, 128548215098396104, "Scarichi produzione (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_scavar.NTSSetParam(oMenu, oApp.Tr(Me, 128548215256471924, "Scarichi varie (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_resfor.NTSSetParam(oMenu, oApp.Tr(Me, 128548215426871174, "Resi a fornitore (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAd_giaini.NTSSetParam(oMenu, oApp.Tr(Me, 128548215627893464, "Giacenza iniziale (quantità)"), oApp.FormatQta, 20, -999999999999, 999999999999)

      edAd_valesi.NTSSetParam(oMenu, oApp.Tr(Me, 128548216079417299, "Esistenza (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vcarfor.NTSSetParam(oMenu, oApp.Tr(Me, 128548216291923229, "Carichi fornitore (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vcarpro.NTSSetParam(oMenu, oApp.Tr(Me, 128548216510317454, "Carichi produzione (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vcarvar.NTSSetParam(oMenu, oApp.Tr(Me, 128548217711881174, "Carichi varie (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vrescli.NTSSetParam(oMenu, oApp.Tr(Me, 128548217888891329, "Resi da clienti (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vscacli.NTSSetParam(oMenu, oApp.Tr(Me, 128548218109619389, "Scarichi a clienti (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vscapro.NTSSetParam(oMenu, oApp.Tr(Me, 128548218388488259, "Scarichi produzione (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vscavar.NTSSetParam(oMenu, oApp.Tr(Me, 128548218569785249, "Scarichi varie (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vresfor.NTSSetParam(oMenu, oApp.Tr(Me, 128548218809701534, "Resi a fornitori (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAd_vgiaini.NTSSetParam(oMenu, oApp.Tr(Me, 128548219009835209, "Giacenza iniziale (valore)"), oApp.FormatImporti, 20, -999999999999, 999999999999)

      '-------------------------------------------------
      edAd_codart.NTSSetRichiesto()
      edAd_fase.NTSSetRichiesto()
      edAd_magaz.NTSSetRichiesto()
      edAd_tipval.NTSSetRichiesto()
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

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGSAD))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbXx_fase = New NTSInformatica.NTSLabel
    Me.edAd_fase = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edAd_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbAd_fase = New NTSInformatica.NTSLabel
    Me.lbAd_codart = New NTSInformatica.NTSLabel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.edAd_magaz = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_magaz = New NTSInformatica.NTSLabel
    Me.edAd_sommat = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_sommat = New NTSInformatica.NTSLabel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.tsGsad = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pbIndex1 = New NTSInformatica.NTSPanel
    Me.edAd_giaini = New NTSInformatica.NTSTextBoxNum
    Me.edAd_resfor = New NTSInformatica.NTSTextBoxNum
    Me.edAd_scavar = New NTSInformatica.NTSTextBoxNum
    Me.edAd_scapro = New NTSInformatica.NTSTextBoxNum
    Me.edAd_scacli = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_giaini = New NTSInformatica.NTSLabel
    Me.lbAd_resfor = New NTSInformatica.NTSLabel
    Me.lbAd_scavar = New NTSInformatica.NTSLabel
    Me.lbAd_scapro = New NTSInformatica.NTSLabel
    Me.lbAd_scacli = New NTSInformatica.NTSLabel
    Me.edAd_rescli = New NTSInformatica.NTSTextBoxNum
    Me.edAd_carvar = New NTSInformatica.NTSTextBoxNum
    Me.edAd_carpro = New NTSInformatica.NTSTextBoxNum
    Me.edAd_carfor = New NTSInformatica.NTSTextBoxNum
    Me.edAd_esist = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_rescli = New NTSInformatica.NTSLabel
    Me.lbAd_carvar = New NTSInformatica.NTSLabel
    Me.lbAd_carpro = New NTSInformatica.NTSLabel
    Me.lbAd_carfor = New NTSInformatica.NTSLabel
    Me.lbAd_esist = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pbIndex2 = New NTSInformatica.NTSPanel
    Me.edAd_vgiaini = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vresfor = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vscavar = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vscapro = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vscacli = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_vgiaini = New NTSInformatica.NTSLabel
    Me.lbAd_vresfor = New NTSInformatica.NTSLabel
    Me.lbAd_vscavar = New NTSInformatica.NTSLabel
    Me.lbAd_vscapro = New NTSInformatica.NTSLabel
    Me.lbAd_vscacli = New NTSInformatica.NTSLabel
    Me.edAd_vrescli = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vcarvar = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vcarpro = New NTSInformatica.NTSTextBoxNum
    Me.edAd_vcarfor = New NTSInformatica.NTSTextBoxNum
    Me.edAd_valesi = New NTSInformatica.NTSTextBoxNum
    Me.lbAd_vrescli = New NTSInformatica.NTSLabel
    Me.lbAd_vcarvar = New NTSInformatica.NTSLabel
    Me.lbAd_vcarpro = New NTSInformatica.NTSLabel
    Me.lbAd_vcarfor = New NTSInformatica.NTSLabel
    Me.lbAd_valesi = New NTSInformatica.NTSLabel
    Me.edAd_tipval = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_tipval = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_fase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_magaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_sommat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsGsad, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsGsad.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pbIndex1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbIndex1.SuspendLayout()
    CType(Me.edAd_giaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_resfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_scavar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_scapro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_scacli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_rescli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_carvar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_carpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_carfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_esist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pbIndex2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbIndex2.SuspendLayout()
    CType(Me.edAd_vgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vresfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vscavar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vscapro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vscacli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vrescli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vcarvar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vcarpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_vcarfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_valesi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAd_tipval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 12
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 11
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
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
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 8
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbXx_fase
    '
    Me.lbXx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_fase.Location = New System.Drawing.Point(164, 72)
    Me.lbXx_fase.Name = "lbXx_fase"
    Me.lbXx_fase.NTSDbField = ""
    Me.lbXx_fase.Size = New System.Drawing.Size(429, 20)
    Me.lbXx_fase.TabIndex = 15
    Me.lbXx_fase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'edAd_fase
    '
    Me.edAd_fase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_fase.EditValue = "0"
    Me.edAd_fase.Location = New System.Drawing.Point(95, 72)
    Me.edAd_fase.Name = "edAd_fase"
    Me.edAd_fase.NTSDbField = ""
    Me.edAd_fase.NTSFormat = "0"
    Me.edAd_fase.NTSForzaVisZoom = False
    Me.edAd_fase.NTSOldValue = ""
    Me.edAd_fase.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_fase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_fase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_fase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_fase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAd_fase.Properties.MaxLength = 65536
    Me.edAd_fase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_fase.Size = New System.Drawing.Size(63, 20)
    Me.edAd_fase.TabIndex = 11
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(287, 46)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(306, 20)
    Me.lbXx_codart.TabIndex = 14
    Me.lbXx_codart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'edAd_codart
    '
    Me.edAd_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_codart.Location = New System.Drawing.Point(95, 46)
    Me.edAd_codart.Name = "edAd_codart"
    Me.edAd_codart.NTSDbField = ""
    Me.edAd_codart.NTSForzaVisZoom = False
    Me.edAd_codart.NTSOldValue = ""
    Me.edAd_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAd_codart.Properties.MaxLength = 65536
    Me.edAd_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_codart.Size = New System.Drawing.Size(186, 20)
    Me.edAd_codart.TabIndex = 10
    '
    'lbAd_fase
    '
    Me.lbAd_fase.AutoSize = True
    Me.lbAd_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_fase.Location = New System.Drawing.Point(15, 74)
    Me.lbAd_fase.Name = "lbAd_fase"
    Me.lbAd_fase.NTSDbField = ""
    Me.lbAd_fase.Size = New System.Drawing.Size(30, 13)
    Me.lbAd_fase.TabIndex = 13
    Me.lbAd_fase.Text = "Fase"
    '
    'lbAd_codart
    '
    Me.lbAd_codart.AutoSize = True
    Me.lbAd_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_codart.Location = New System.Drawing.Point(15, 49)
    Me.lbAd_codart.Name = "lbAd_codart"
    Me.lbAd_codart.NTSDbField = ""
    Me.lbAd_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbAd_codart.TabIndex = 12
    Me.lbAd_codart.Text = "Articolo"
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(164, 98)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(429, 20)
    Me.lbXx_magaz.TabIndex = 18
    Me.lbXx_magaz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'edAd_magaz
    '
    Me.edAd_magaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_magaz.EditValue = "0"
    Me.edAd_magaz.Location = New System.Drawing.Point(95, 97)
    Me.edAd_magaz.Name = "edAd_magaz"
    Me.edAd_magaz.NTSDbField = ""
    Me.edAd_magaz.NTSFormat = "0"
    Me.edAd_magaz.NTSForzaVisZoom = False
    Me.edAd_magaz.NTSOldValue = ""
    Me.edAd_magaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_magaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_magaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_magaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_magaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAd_magaz.Properties.MaxLength = 65536
    Me.edAd_magaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_magaz.Size = New System.Drawing.Size(63, 20)
    Me.edAd_magaz.TabIndex = 16
    '
    'lbAd_magaz
    '
    Me.lbAd_magaz.AutoSize = True
    Me.lbAd_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_magaz.Location = New System.Drawing.Point(15, 100)
    Me.lbAd_magaz.Name = "lbAd_magaz"
    Me.lbAd_magaz.NTSDbField = ""
    Me.lbAd_magaz.Size = New System.Drawing.Size(57, 13)
    Me.lbAd_magaz.TabIndex = 17
    Me.lbAd_magaz.Text = "Magazzino"
    '
    'edAd_sommat
    '
    Me.edAd_sommat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_sommat.EditValue = "0"
    Me.edAd_sommat.Location = New System.Drawing.Point(95, 123)
    Me.edAd_sommat.Name = "edAd_sommat"
    Me.edAd_sommat.NTSDbField = ""
    Me.edAd_sommat.NTSFormat = "0"
    Me.edAd_sommat.NTSForzaVisZoom = False
    Me.edAd_sommat.NTSOldValue = ""
    Me.edAd_sommat.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_sommat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_sommat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_sommat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_sommat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_sommat.Properties.MaxLength = 65536
    Me.edAd_sommat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_sommat.Size = New System.Drawing.Size(86, 20)
    Me.edAd_sommat.TabIndex = 19
    '
    'lbAd_sommat
    '
    Me.lbAd_sommat.AutoSize = True
    Me.lbAd_sommat.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_sommat.Location = New System.Drawing.Point(15, 127)
    Me.lbAd_sommat.Name = "lbAd_sommat"
    Me.lbAd_sommat.NTSDbField = ""
    Me.lbAd_sommat.Size = New System.Drawing.Size(63, 13)
    Me.lbAd_sommat.TabIndex = 20
    Me.lbAd_sommat.Text = "Sommatoria"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(15, 152)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(74, 13)
    Me.NtsLabel1.TabIndex = 21
    Me.NtsLabel1.Text = "Valorizzazione"
    '
    'tsGsad
    '
    Me.tsGsad.Location = New System.Drawing.Point(15, 185)
    Me.tsGsad.Name = "tsGsad"
    Me.tsGsad.SelectedTabPage = Me.NtsTabPage1
    Me.tsGsad.Size = New System.Drawing.Size(578, 190)
    Me.tsGsad.TabIndex = 52
    Me.tsGsad.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsGsad.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pbIndex1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(569, 160)
    Me.NtsTabPage1.Text = "&1 - QUANTITA'"
    '
    'pbIndex1
    '
    Me.pbIndex1.AllowDrop = True
    Me.pbIndex1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbIndex1.Appearance.Options.UseBackColor = True
    Me.pbIndex1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbIndex1.Controls.Add(Me.edAd_giaini)
    Me.pbIndex1.Controls.Add(Me.edAd_resfor)
    Me.pbIndex1.Controls.Add(Me.edAd_scavar)
    Me.pbIndex1.Controls.Add(Me.edAd_scapro)
    Me.pbIndex1.Controls.Add(Me.edAd_scacli)
    Me.pbIndex1.Controls.Add(Me.lbAd_giaini)
    Me.pbIndex1.Controls.Add(Me.lbAd_resfor)
    Me.pbIndex1.Controls.Add(Me.lbAd_scavar)
    Me.pbIndex1.Controls.Add(Me.lbAd_scapro)
    Me.pbIndex1.Controls.Add(Me.lbAd_scacli)
    Me.pbIndex1.Controls.Add(Me.edAd_rescli)
    Me.pbIndex1.Controls.Add(Me.edAd_carvar)
    Me.pbIndex1.Controls.Add(Me.edAd_carpro)
    Me.pbIndex1.Controls.Add(Me.edAd_carfor)
    Me.pbIndex1.Controls.Add(Me.edAd_esist)
    Me.pbIndex1.Controls.Add(Me.lbAd_rescli)
    Me.pbIndex1.Controls.Add(Me.lbAd_carvar)
    Me.pbIndex1.Controls.Add(Me.lbAd_carpro)
    Me.pbIndex1.Controls.Add(Me.lbAd_carfor)
    Me.pbIndex1.Controls.Add(Me.lbAd_esist)
    Me.pbIndex1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbIndex1.Location = New System.Drawing.Point(0, 0)
    Me.pbIndex1.Name = "pbIndex1"
    Me.pbIndex1.Size = New System.Drawing.Size(566, 160)
    Me.pbIndex1.TabIndex = 53
    Me.pbIndex1.Text = "NtsPanel1"
    '
    'edAd_giaini
    '
    Me.edAd_giaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_giaini.EditValue = "0"
    Me.edAd_giaini.Location = New System.Drawing.Point(396, 121)
    Me.edAd_giaini.Name = "edAd_giaini"
    Me.edAd_giaini.NTSDbField = ""
    Me.edAd_giaini.NTSFormat = "0"
    Me.edAd_giaini.NTSForzaVisZoom = False
    Me.edAd_giaini.NTSOldValue = ""
    Me.edAd_giaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_giaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_giaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_giaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_giaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_giaini.Properties.MaxLength = 65536
    Me.edAd_giaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_giaini.Size = New System.Drawing.Size(73, 20)
    Me.edAd_giaini.TabIndex = 50
    '
    'edAd_resfor
    '
    Me.edAd_resfor.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAd_resfor.EditValue = "0"
    Me.edAd_resfor.Location = New System.Drawing.Point(396, 95)
    Me.edAd_resfor.Name = "edAd_resfor"
    Me.edAd_resfor.NTSDbField = ""
    Me.edAd_resfor.NTSFormat = "0"
    Me.edAd_resfor.NTSForzaVisZoom = False
    Me.edAd_resfor.NTSOldValue = ""
    Me.edAd_resfor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_resfor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_resfor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_resfor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_resfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_resfor.Properties.MaxLength = 65536
    Me.edAd_resfor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_resfor.Size = New System.Drawing.Size(73, 20)
    Me.edAd_resfor.TabIndex = 49
    '
    'edAd_scavar
    '
    Me.edAd_scavar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_scavar.EditValue = "0"
    Me.edAd_scavar.Location = New System.Drawing.Point(396, 69)
    Me.edAd_scavar.Name = "edAd_scavar"
    Me.edAd_scavar.NTSDbField = ""
    Me.edAd_scavar.NTSFormat = "0"
    Me.edAd_scavar.NTSForzaVisZoom = False
    Me.edAd_scavar.NTSOldValue = ""
    Me.edAd_scavar.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_scavar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_scavar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_scavar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_scavar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_scavar.Properties.MaxLength = 65536
    Me.edAd_scavar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_scavar.Size = New System.Drawing.Size(73, 20)
    Me.edAd_scavar.TabIndex = 48
    '
    'edAd_scapro
    '
    Me.edAd_scapro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_scapro.EditValue = "0"
    Me.edAd_scapro.Location = New System.Drawing.Point(396, 44)
    Me.edAd_scapro.Name = "edAd_scapro"
    Me.edAd_scapro.NTSDbField = ""
    Me.edAd_scapro.NTSFormat = "0"
    Me.edAd_scapro.NTSForzaVisZoom = False
    Me.edAd_scapro.NTSOldValue = ""
    Me.edAd_scapro.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_scapro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_scapro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_scapro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_scapro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_scapro.Properties.MaxLength = 65536
    Me.edAd_scapro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_scapro.Size = New System.Drawing.Size(73, 20)
    Me.edAd_scapro.TabIndex = 47
    '
    'edAd_scacli
    '
    Me.edAd_scacli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_scacli.EditValue = "0"
    Me.edAd_scacli.Location = New System.Drawing.Point(396, 18)
    Me.edAd_scacli.Name = "edAd_scacli"
    Me.edAd_scacli.NTSDbField = ""
    Me.edAd_scacli.NTSFormat = "0"
    Me.edAd_scacli.NTSForzaVisZoom = False
    Me.edAd_scacli.NTSOldValue = ""
    Me.edAd_scacli.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_scacli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_scacli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_scacli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_scacli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_scacli.Properties.MaxLength = 65536
    Me.edAd_scacli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_scacli.Size = New System.Drawing.Size(73, 20)
    Me.edAd_scacli.TabIndex = 46
    '
    'lbAd_giaini
    '
    Me.lbAd_giaini.AutoSize = True
    Me.lbAd_giaini.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_giaini.Location = New System.Drawing.Point(284, 124)
    Me.lbAd_giaini.Name = "lbAd_giaini"
    Me.lbAd_giaini.NTSDbField = ""
    Me.lbAd_giaini.Size = New System.Drawing.Size(84, 13)
    Me.lbAd_giaini.TabIndex = 45
    Me.lbAd_giaini.Text = "Giacenza iniziale"
    '
    'lbAd_resfor
    '
    Me.lbAd_resfor.AutoSize = True
    Me.lbAd_resfor.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_resfor.Location = New System.Drawing.Point(284, 98)
    Me.lbAd_resfor.Name = "lbAd_resfor"
    Me.lbAd_resfor.NTSDbField = ""
    Me.lbAd_resfor.Size = New System.Drawing.Size(81, 13)
    Me.lbAd_resfor.TabIndex = 44
    Me.lbAd_resfor.Text = "Resi a fornitore"
    '
    'lbAd_scavar
    '
    Me.lbAd_scavar.AutoSize = True
    Me.lbAd_scavar.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_scavar.Location = New System.Drawing.Point(284, 72)
    Me.lbAd_scavar.Name = "lbAd_scavar"
    Me.lbAd_scavar.NTSDbField = ""
    Me.lbAd_scavar.Size = New System.Drawing.Size(70, 13)
    Me.lbAd_scavar.TabIndex = 43
    Me.lbAd_scavar.Text = "Scarichi varie"
    '
    'lbAd_scapro
    '
    Me.lbAd_scapro.AutoSize = True
    Me.lbAd_scapro.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_scapro.Location = New System.Drawing.Point(284, 47)
    Me.lbAd_scapro.Name = "lbAd_scapro"
    Me.lbAd_scapro.NTSDbField = ""
    Me.lbAd_scapro.Size = New System.Drawing.Size(99, 13)
    Me.lbAd_scapro.TabIndex = 42
    Me.lbAd_scapro.Text = "Scarichi produzione"
    '
    'lbAd_scacli
    '
    Me.lbAd_scacli.AutoSize = True
    Me.lbAd_scacli.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_scacli.Location = New System.Drawing.Point(284, 18)
    Me.lbAd_scacli.Name = "lbAd_scacli"
    Me.lbAd_scacli.NTSDbField = ""
    Me.lbAd_scacli.Size = New System.Drawing.Size(82, 13)
    Me.lbAd_scacli.TabIndex = 41
    Me.lbAd_scacli.Text = "Scarichi a clienti"
    '
    'edAd_rescli
    '
    Me.edAd_rescli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_rescli.EditValue = "0"
    Me.edAd_rescli.Location = New System.Drawing.Point(143, 121)
    Me.edAd_rescli.Name = "edAd_rescli"
    Me.edAd_rescli.NTSDbField = ""
    Me.edAd_rescli.NTSFormat = "0"
    Me.edAd_rescli.NTSForzaVisZoom = False
    Me.edAd_rescli.NTSOldValue = ""
    Me.edAd_rescli.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_rescli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_rescli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_rescli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_rescli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_rescli.Properties.MaxLength = 65536
    Me.edAd_rescli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_rescli.Size = New System.Drawing.Size(73, 20)
    Me.edAd_rescli.TabIndex = 40
    '
    'edAd_carvar
    '
    Me.edAd_carvar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_carvar.EditValue = "0"
    Me.edAd_carvar.Location = New System.Drawing.Point(143, 95)
    Me.edAd_carvar.Name = "edAd_carvar"
    Me.edAd_carvar.NTSDbField = ""
    Me.edAd_carvar.NTSFormat = "0"
    Me.edAd_carvar.NTSForzaVisZoom = False
    Me.edAd_carvar.NTSOldValue = ""
    Me.edAd_carvar.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_carvar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_carvar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_carvar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_carvar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_carvar.Properties.MaxLength = 65536
    Me.edAd_carvar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_carvar.Size = New System.Drawing.Size(73, 20)
    Me.edAd_carvar.TabIndex = 39
    '
    'edAd_carpro
    '
    Me.edAd_carpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_carpro.EditValue = "0"
    Me.edAd_carpro.Location = New System.Drawing.Point(143, 69)
    Me.edAd_carpro.Name = "edAd_carpro"
    Me.edAd_carpro.NTSDbField = ""
    Me.edAd_carpro.NTSFormat = "0"
    Me.edAd_carpro.NTSForzaVisZoom = False
    Me.edAd_carpro.NTSOldValue = ""
    Me.edAd_carpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_carpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_carpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_carpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_carpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_carpro.Properties.MaxLength = 65536
    Me.edAd_carpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_carpro.Size = New System.Drawing.Size(73, 20)
    Me.edAd_carpro.TabIndex = 38
    '
    'edAd_carfor
    '
    Me.edAd_carfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_carfor.EditValue = "0"
    Me.edAd_carfor.Location = New System.Drawing.Point(143, 44)
    Me.edAd_carfor.Name = "edAd_carfor"
    Me.edAd_carfor.NTSDbField = ""
    Me.edAd_carfor.NTSFormat = "0"
    Me.edAd_carfor.NTSForzaVisZoom = False
    Me.edAd_carfor.NTSOldValue = ""
    Me.edAd_carfor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_carfor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_carfor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_carfor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_carfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_carfor.Properties.MaxLength = 65536
    Me.edAd_carfor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_carfor.Size = New System.Drawing.Size(73, 20)
    Me.edAd_carfor.TabIndex = 37
    '
    'edAd_esist
    '
    Me.edAd_esist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_esist.EditValue = "0"
    Me.edAd_esist.Location = New System.Drawing.Point(143, 18)
    Me.edAd_esist.Name = "edAd_esist"
    Me.edAd_esist.NTSDbField = ""
    Me.edAd_esist.NTSFormat = "0"
    Me.edAd_esist.NTSForzaVisZoom = False
    Me.edAd_esist.NTSOldValue = ""
    Me.edAd_esist.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_esist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_esist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_esist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_esist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_esist.Properties.MaxLength = 65536
    Me.edAd_esist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_esist.Size = New System.Drawing.Size(73, 20)
    Me.edAd_esist.TabIndex = 36
    '
    'lbAd_rescli
    '
    Me.lbAd_rescli.AutoSize = True
    Me.lbAd_rescli.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_rescli.Location = New System.Drawing.Point(24, 124)
    Me.lbAd_rescli.Name = "lbAd_rescli"
    Me.lbAd_rescli.NTSDbField = ""
    Me.lbAd_rescli.Size = New System.Drawing.Size(72, 13)
    Me.lbAd_rescli.TabIndex = 35
    Me.lbAd_rescli.Text = "Resi da clienti"
    '
    'lbAd_carvar
    '
    Me.lbAd_carvar.AutoSize = True
    Me.lbAd_carvar.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_carvar.Location = New System.Drawing.Point(24, 98)
    Me.lbAd_carvar.Name = "lbAd_carvar"
    Me.lbAd_carvar.NTSDbField = ""
    Me.lbAd_carvar.Size = New System.Drawing.Size(66, 13)
    Me.lbAd_carvar.TabIndex = 34
    Me.lbAd_carvar.Text = "Carichi varie"
    '
    'lbAd_carpro
    '
    Me.lbAd_carpro.AutoSize = True
    Me.lbAd_carpro.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_carpro.Location = New System.Drawing.Point(24, 72)
    Me.lbAd_carpro.Name = "lbAd_carpro"
    Me.lbAd_carpro.NTSDbField = ""
    Me.lbAd_carpro.Size = New System.Drawing.Size(95, 13)
    Me.lbAd_carpro.TabIndex = 33
    Me.lbAd_carpro.Text = "Carichi produzione"
    '
    'lbAd_carfor
    '
    Me.lbAd_carfor.AutoSize = True
    Me.lbAd_carfor.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_carfor.Location = New System.Drawing.Point(24, 47)
    Me.lbAd_carfor.Name = "lbAd_carfor"
    Me.lbAd_carfor.NTSDbField = ""
    Me.lbAd_carfor.Size = New System.Drawing.Size(84, 13)
    Me.lbAd_carfor.TabIndex = 32
    Me.lbAd_carfor.Text = "Carichi fornitore"
    '
    'lbAd_esist
    '
    Me.lbAd_esist.AutoSize = True
    Me.lbAd_esist.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_esist.Location = New System.Drawing.Point(24, 21)
    Me.lbAd_esist.Name = "lbAd_esist"
    Me.lbAd_esist.NTSDbField = ""
    Me.lbAd_esist.Size = New System.Drawing.Size(52, 13)
    Me.lbAd_esist.TabIndex = 31
    Me.lbAd_esist.Text = "Esistenza"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pbIndex2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(569, 160)
    Me.NtsTabPage2.Text = "&2 - VALORI"
    '
    'pbIndex2
    '
    Me.pbIndex2.AllowDrop = True
    Me.pbIndex2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbIndex2.Appearance.Options.UseBackColor = True
    Me.pbIndex2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbIndex2.Controls.Add(Me.edAd_vgiaini)
    Me.pbIndex2.Controls.Add(Me.edAd_vresfor)
    Me.pbIndex2.Controls.Add(Me.edAd_vscavar)
    Me.pbIndex2.Controls.Add(Me.edAd_vscapro)
    Me.pbIndex2.Controls.Add(Me.edAd_vscacli)
    Me.pbIndex2.Controls.Add(Me.lbAd_vgiaini)
    Me.pbIndex2.Controls.Add(Me.lbAd_vresfor)
    Me.pbIndex2.Controls.Add(Me.lbAd_vscavar)
    Me.pbIndex2.Controls.Add(Me.lbAd_vscapro)
    Me.pbIndex2.Controls.Add(Me.lbAd_vscacli)
    Me.pbIndex2.Controls.Add(Me.edAd_vrescli)
    Me.pbIndex2.Controls.Add(Me.edAd_vcarvar)
    Me.pbIndex2.Controls.Add(Me.edAd_vcarpro)
    Me.pbIndex2.Controls.Add(Me.edAd_vcarfor)
    Me.pbIndex2.Controls.Add(Me.edAd_valesi)
    Me.pbIndex2.Controls.Add(Me.lbAd_vrescli)
    Me.pbIndex2.Controls.Add(Me.lbAd_vcarvar)
    Me.pbIndex2.Controls.Add(Me.lbAd_vcarpro)
    Me.pbIndex2.Controls.Add(Me.lbAd_vcarfor)
    Me.pbIndex2.Controls.Add(Me.lbAd_valesi)
    Me.pbIndex2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbIndex2.Location = New System.Drawing.Point(0, 0)
    Me.pbIndex2.Name = "pbIndex2"
    Me.pbIndex2.Size = New System.Drawing.Size(566, 160)
    Me.pbIndex2.TabIndex = 54
    Me.pbIndex2.Text = "NtsPanel1"
    '
    'edAd_vgiaini
    '
    Me.edAd_vgiaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vgiaini.EditValue = "0"
    Me.edAd_vgiaini.Location = New System.Drawing.Point(396, 121)
    Me.edAd_vgiaini.Name = "edAd_vgiaini"
    Me.edAd_vgiaini.NTSDbField = ""
    Me.edAd_vgiaini.NTSFormat = "0"
    Me.edAd_vgiaini.NTSForzaVisZoom = False
    Me.edAd_vgiaini.NTSOldValue = ""
    Me.edAd_vgiaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vgiaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vgiaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vgiaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vgiaini.Properties.MaxLength = 65536
    Me.edAd_vgiaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vgiaini.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vgiaini.TabIndex = 50
    '
    'edAd_vresfor
    '
    Me.edAd_vresfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vresfor.EditValue = "0"
    Me.edAd_vresfor.Location = New System.Drawing.Point(396, 95)
    Me.edAd_vresfor.Name = "edAd_vresfor"
    Me.edAd_vresfor.NTSDbField = ""
    Me.edAd_vresfor.NTSFormat = "0"
    Me.edAd_vresfor.NTSForzaVisZoom = False
    Me.edAd_vresfor.NTSOldValue = ""
    Me.edAd_vresfor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vresfor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vresfor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vresfor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vresfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vresfor.Properties.MaxLength = 65536
    Me.edAd_vresfor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vresfor.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vresfor.TabIndex = 49
    '
    'edAd_vscavar
    '
    Me.edAd_vscavar.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAd_vscavar.EditValue = "0"
    Me.edAd_vscavar.Location = New System.Drawing.Point(396, 69)
    Me.edAd_vscavar.Name = "edAd_vscavar"
    Me.edAd_vscavar.NTSDbField = ""
    Me.edAd_vscavar.NTSFormat = "0"
    Me.edAd_vscavar.NTSForzaVisZoom = False
    Me.edAd_vscavar.NTSOldValue = ""
    Me.edAd_vscavar.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vscavar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vscavar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vscavar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vscavar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vscavar.Properties.MaxLength = 65536
    Me.edAd_vscavar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vscavar.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vscavar.TabIndex = 48
    '
    'edAd_vscapro
    '
    Me.edAd_vscapro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vscapro.EditValue = "0"
    Me.edAd_vscapro.Location = New System.Drawing.Point(396, 44)
    Me.edAd_vscapro.Name = "edAd_vscapro"
    Me.edAd_vscapro.NTSDbField = ""
    Me.edAd_vscapro.NTSFormat = "0"
    Me.edAd_vscapro.NTSForzaVisZoom = False
    Me.edAd_vscapro.NTSOldValue = ""
    Me.edAd_vscapro.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vscapro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vscapro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vscapro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vscapro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vscapro.Properties.MaxLength = 65536
    Me.edAd_vscapro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vscapro.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vscapro.TabIndex = 47
    '
    'edAd_vscacli
    '
    Me.edAd_vscacli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vscacli.EditValue = "0"
    Me.edAd_vscacli.Location = New System.Drawing.Point(396, 18)
    Me.edAd_vscacli.Name = "edAd_vscacli"
    Me.edAd_vscacli.NTSDbField = ""
    Me.edAd_vscacli.NTSFormat = "0"
    Me.edAd_vscacli.NTSForzaVisZoom = False
    Me.edAd_vscacli.NTSOldValue = ""
    Me.edAd_vscacli.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vscacli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vscacli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vscacli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vscacli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vscacli.Properties.MaxLength = 65536
    Me.edAd_vscacli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vscacli.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vscacli.TabIndex = 46
    '
    'lbAd_vgiaini
    '
    Me.lbAd_vgiaini.AutoSize = True
    Me.lbAd_vgiaini.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vgiaini.Location = New System.Drawing.Point(284, 124)
    Me.lbAd_vgiaini.Name = "lbAd_vgiaini"
    Me.lbAd_vgiaini.NTSDbField = ""
    Me.lbAd_vgiaini.Size = New System.Drawing.Size(84, 13)
    Me.lbAd_vgiaini.TabIndex = 45
    Me.lbAd_vgiaini.Text = "Giacenza iniziale"
    '
    'lbAd_vresfor
    '
    Me.lbAd_vresfor.AutoSize = True
    Me.lbAd_vresfor.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vresfor.Location = New System.Drawing.Point(284, 98)
    Me.lbAd_vresfor.Name = "lbAd_vresfor"
    Me.lbAd_vresfor.NTSDbField = ""
    Me.lbAd_vresfor.Size = New System.Drawing.Size(81, 13)
    Me.lbAd_vresfor.TabIndex = 44
    Me.lbAd_vresfor.Text = "Resi a fornitore"
    '
    'lbAd_vscavar
    '
    Me.lbAd_vscavar.AutoSize = True
    Me.lbAd_vscavar.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vscavar.Location = New System.Drawing.Point(284, 72)
    Me.lbAd_vscavar.Name = "lbAd_vscavar"
    Me.lbAd_vscavar.NTSDbField = ""
    Me.lbAd_vscavar.Size = New System.Drawing.Size(70, 13)
    Me.lbAd_vscavar.TabIndex = 43
    Me.lbAd_vscavar.Text = "Scarichi varie"
    '
    'lbAd_vscapro
    '
    Me.lbAd_vscapro.AutoSize = True
    Me.lbAd_vscapro.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vscapro.Location = New System.Drawing.Point(284, 47)
    Me.lbAd_vscapro.Name = "lbAd_vscapro"
    Me.lbAd_vscapro.NTSDbField = ""
    Me.lbAd_vscapro.Size = New System.Drawing.Size(99, 13)
    Me.lbAd_vscapro.TabIndex = 42
    Me.lbAd_vscapro.Text = "Scarichi produzione"
    '
    'lbAd_vscacli
    '
    Me.lbAd_vscacli.AutoSize = True
    Me.lbAd_vscacli.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vscacli.Location = New System.Drawing.Point(284, 18)
    Me.lbAd_vscacli.Name = "lbAd_vscacli"
    Me.lbAd_vscacli.NTSDbField = ""
    Me.lbAd_vscacli.Size = New System.Drawing.Size(82, 13)
    Me.lbAd_vscacli.TabIndex = 41
    Me.lbAd_vscacli.Text = "Scarichi a clienti"
    '
    'edAd_vrescli
    '
    Me.edAd_vrescli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vrescli.EditValue = "0"
    Me.edAd_vrescli.Location = New System.Drawing.Point(143, 121)
    Me.edAd_vrescli.Name = "edAd_vrescli"
    Me.edAd_vrescli.NTSDbField = ""
    Me.edAd_vrescli.NTSFormat = "0"
    Me.edAd_vrescli.NTSForzaVisZoom = False
    Me.edAd_vrescli.NTSOldValue = ""
    Me.edAd_vrescli.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vrescli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vrescli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vrescli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vrescli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vrescli.Properties.MaxLength = 65536
    Me.edAd_vrescli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vrescli.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vrescli.TabIndex = 40
    '
    'edAd_vcarvar
    '
    Me.edAd_vcarvar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_vcarvar.EditValue = "0"
    Me.edAd_vcarvar.Location = New System.Drawing.Point(143, 95)
    Me.edAd_vcarvar.Name = "edAd_vcarvar"
    Me.edAd_vcarvar.NTSDbField = ""
    Me.edAd_vcarvar.NTSFormat = "0"
    Me.edAd_vcarvar.NTSForzaVisZoom = False
    Me.edAd_vcarvar.NTSOldValue = ""
    Me.edAd_vcarvar.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vcarvar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vcarvar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vcarvar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vcarvar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vcarvar.Properties.MaxLength = 65536
    Me.edAd_vcarvar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vcarvar.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vcarvar.TabIndex = 39
    '
    'edAd_vcarpro
    '
    Me.edAd_vcarpro.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAd_vcarpro.EditValue = "0"
    Me.edAd_vcarpro.Location = New System.Drawing.Point(143, 69)
    Me.edAd_vcarpro.Name = "edAd_vcarpro"
    Me.edAd_vcarpro.NTSDbField = ""
    Me.edAd_vcarpro.NTSFormat = "0"
    Me.edAd_vcarpro.NTSForzaVisZoom = False
    Me.edAd_vcarpro.NTSOldValue = ""
    Me.edAd_vcarpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vcarpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vcarpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vcarpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vcarpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vcarpro.Properties.MaxLength = 65536
    Me.edAd_vcarpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vcarpro.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vcarpro.TabIndex = 38
    '
    'edAd_vcarfor
    '
    Me.edAd_vcarfor.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAd_vcarfor.EditValue = "0"
    Me.edAd_vcarfor.Location = New System.Drawing.Point(143, 44)
    Me.edAd_vcarfor.Name = "edAd_vcarfor"
    Me.edAd_vcarfor.NTSDbField = ""
    Me.edAd_vcarfor.NTSFormat = "0"
    Me.edAd_vcarfor.NTSForzaVisZoom = False
    Me.edAd_vcarfor.NTSOldValue = ""
    Me.edAd_vcarfor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_vcarfor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_vcarfor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_vcarfor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_vcarfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_vcarfor.Properties.MaxLength = 65536
    Me.edAd_vcarfor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_vcarfor.Size = New System.Drawing.Size(73, 20)
    Me.edAd_vcarfor.TabIndex = 37
    '
    'edAd_valesi
    '
    Me.edAd_valesi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_valesi.EditValue = "0"
    Me.edAd_valesi.Location = New System.Drawing.Point(143, 18)
    Me.edAd_valesi.Name = "edAd_valesi"
    Me.edAd_valesi.NTSDbField = ""
    Me.edAd_valesi.NTSFormat = "0"
    Me.edAd_valesi.NTSForzaVisZoom = False
    Me.edAd_valesi.NTSOldValue = ""
    Me.edAd_valesi.Properties.Appearance.Options.UseTextOptions = True
    Me.edAd_valesi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAd_valesi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_valesi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_valesi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAd_valesi.Properties.MaxLength = 65536
    Me.edAd_valesi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_valesi.Size = New System.Drawing.Size(73, 20)
    Me.edAd_valesi.TabIndex = 36
    '
    'lbAd_vrescli
    '
    Me.lbAd_vrescli.AutoSize = True
    Me.lbAd_vrescli.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vrescli.Location = New System.Drawing.Point(24, 124)
    Me.lbAd_vrescli.Name = "lbAd_vrescli"
    Me.lbAd_vrescli.NTSDbField = ""
    Me.lbAd_vrescli.Size = New System.Drawing.Size(72, 13)
    Me.lbAd_vrescli.TabIndex = 35
    Me.lbAd_vrescli.Text = "Resi da clienti"
    '
    'lbAd_vcarvar
    '
    Me.lbAd_vcarvar.AutoSize = True
    Me.lbAd_vcarvar.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vcarvar.Location = New System.Drawing.Point(24, 98)
    Me.lbAd_vcarvar.Name = "lbAd_vcarvar"
    Me.lbAd_vcarvar.NTSDbField = ""
    Me.lbAd_vcarvar.Size = New System.Drawing.Size(66, 13)
    Me.lbAd_vcarvar.TabIndex = 34
    Me.lbAd_vcarvar.Text = "Carichi varie"
    '
    'lbAd_vcarpro
    '
    Me.lbAd_vcarpro.AutoSize = True
    Me.lbAd_vcarpro.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vcarpro.Location = New System.Drawing.Point(24, 72)
    Me.lbAd_vcarpro.Name = "lbAd_vcarpro"
    Me.lbAd_vcarpro.NTSDbField = ""
    Me.lbAd_vcarpro.Size = New System.Drawing.Size(95, 13)
    Me.lbAd_vcarpro.TabIndex = 33
    Me.lbAd_vcarpro.Text = "Carichi produzione"
    '
    'lbAd_vcarfor
    '
    Me.lbAd_vcarfor.AutoSize = True
    Me.lbAd_vcarfor.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_vcarfor.Location = New System.Drawing.Point(24, 47)
    Me.lbAd_vcarfor.Name = "lbAd_vcarfor"
    Me.lbAd_vcarfor.NTSDbField = ""
    Me.lbAd_vcarfor.Size = New System.Drawing.Size(84, 13)
    Me.lbAd_vcarfor.TabIndex = 32
    Me.lbAd_vcarfor.Text = "Carichi fornitore"
    '
    'lbAd_valesi
    '
    Me.lbAd_valesi.AutoSize = True
    Me.lbAd_valesi.BackColor = System.Drawing.Color.Transparent
    Me.lbAd_valesi.Location = New System.Drawing.Point(24, 21)
    Me.lbAd_valesi.Name = "lbAd_valesi"
    Me.lbAd_valesi.NTSDbField = ""
    Me.lbAd_valesi.Size = New System.Drawing.Size(52, 13)
    Me.lbAd_valesi.TabIndex = 31
    Me.lbAd_valesi.Text = "Esistenza"
    '
    'edAd_tipval
    '
    Me.edAd_tipval.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAd_tipval.Location = New System.Drawing.Point(95, 149)
    Me.edAd_tipval.Name = "edAd_tipval"
    Me.edAd_tipval.NTSDbField = ""
    Me.edAd_tipval.NTSForzaVisZoom = False
    Me.edAd_tipval.NTSOldValue = ""
    Me.edAd_tipval.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAd_tipval.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAd_tipval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAd_tipval.Properties.MaxLength = 65536
    Me.edAd_tipval.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAd_tipval.Size = New System.Drawing.Size(63, 20)
    Me.edAd_tipval.TabIndex = 53
    '
    'lbXx_tipval
    '
    Me.lbXx_tipval.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipval.Location = New System.Drawing.Point(164, 149)
    Me.lbXx_tipval.Name = "lbXx_tipval"
    Me.lbXx_tipval.NTSDbField = ""
    Me.lbXx_tipval.Size = New System.Drawing.Size(429, 20)
    Me.lbXx_tipval.TabIndex = 54
    Me.lbXx_tipval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'FRMMGGSAD
    '
    Me.ClientSize = New System.Drawing.Size(605, 388)
    Me.Controls.Add(Me.lbXx_tipval)
    Me.Controls.Add(Me.edAd_tipval)
    Me.Controls.Add(Me.tsGsad)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Controls.Add(Me.lbAd_sommat)
    Me.Controls.Add(Me.edAd_sommat)
    Me.Controls.Add(Me.lbXx_magaz)
    Me.Controls.Add(Me.edAd_magaz)
    Me.Controls.Add(Me.lbAd_magaz)
    Me.Controls.Add(Me.lbXx_fase)
    Me.Controls.Add(Me.edAd_fase)
    Me.Controls.Add(Me.lbXx_codart)
    Me.Controls.Add(Me.edAd_codart)
    Me.Controls.Add(Me.lbAd_fase)
    Me.Controls.Add(Me.lbAd_codart)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.Name = "FRMMGGSAD"
    Me.Text = "PROGRESSIVI DEFINITIVI ARTICOLI/MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_fase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_magaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_sommat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsGsad, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsGsad.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pbIndex1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbIndex1.ResumeLayout(False)
    Me.pbIndex1.PerformLayout()
    CType(Me.edAd_giaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_resfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_scavar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_scapro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_scacli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_rescli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_carvar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_carpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_carfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_esist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pbIndex2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbIndex2.ResumeLayout(False)
    Me.pbIndex2.PerformLayout()
    CType(Me.edAd_vgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vresfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vscavar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vscapro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vscacli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vrescli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vcarvar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vcarpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_vcarfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_valesi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAd_tipval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAd_codart.NTSDbField = "artdef.ad_codart"
      lbXx_codart.NTSDbField = "artdef.xx_codart"
      edAd_fase.NTSDbField = "artdef.ad_fase"
      lbXx_fase.NTSDbField = "artdef.xx_fase"
      edAd_magaz.NTSDbField = "artdef.ad_magaz"
      lbXx_magaz.NTSDbField = "artdef.xx_magaz"
      edAd_sommat.NTSDbField = "artdef.ad_sommat"
      edAd_tipval.NTSDbField = "artdef.ad_tipval"
      lbXx_tipval.NTSDbField = "artdef.xx_tipval"
      edAd_esist.NTSDbField = "artdef.ad_esist"
      edAd_carfor.NTSDbField = "artdef.ad_carfor"
      edAd_carpro.NTSDbField = "artdef.ad_carpro"
      edAd_carvar.NTSDbField = "artdef.ad_carvar"
      edAd_rescli.NTSDbField = "artdef.ad_rescli"
      edAd_scacli.NTSDbField = "artdef.ad_scacli"
      edAd_scapro.NTSDbField = "artdef.ad_scapro"
      edAd_scavar.NTSDbField = "artdef.ad_scavar"
      edAd_resfor.NTSDbField = "artdef.ad_resfor"
      edAd_giaini.NTSDbField = "artdef.ad_giaini"
      edAd_valesi.NTSDbField = "artdef.ad_valesi"
      edAd_vcarfor.NTSDbField = "artdef.ad_vcarfor"
      edAd_vcarpro.NTSDbField = "artdef.ad_vcarpro"
      edAd_vcarvar.NTSDbField = "artdef.ad_vcarvar"
      edAd_vrescli.NTSDbField = "artdef.ad_vrescli"
      edAd_vscacli.NTSDbField = "artdef.ad_vscacli"
      edAd_vscapro.NTSDbField = "artdef.ad_vscapro"
      edAd_vscavar.NTSDbField = "artdef.ad_vscavar"
      edAd_vresfor.NTSDbField = "artdef.ad_vresfor"
      edAd_vgiaini.NTSDbField = "artdef.ad_vgiaini"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGsad, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSAD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleGsad.Apri(DittaCorrente, dsGsad) Then
        Me.Close()
        Return
      End If

      dcGsad.DataSource = dsGsad.Tables("ARTDEF")
      dsGsad.AcceptChanges()
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      '------------------------------------------------
      If dsGsad.Tables("ARTDEF").Rows.Count > 0 Then
        edAd_codart.Enabled = False
        edAd_fase.Enabled = False
        edAd_magaz.Enabled = False
      End If
      '-------------------------------------------------
      If dsGsad.Tables("ARTDEF").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcGsad.ResetBindings(False)
      '-------------------------------------------------
      dcGsad.MoveFirst()
      '-------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSAD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSAD_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGsad.Dispose()
      dsGsad.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "TOOLBAR"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      oCleGsad.Nuovo()
      dcGsad.MoveLast()
      edAd_codart.Enabled = True
      edAd_fase.Enabled = True
      edAd_magaz.Enabled = True
      edAd_codart.Focus()
      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB
    Try
      '----------------------------------------------------------------------------------
      If Not Salva() Then Return
      '----------------------------------------------------------------------------------
      SetFastZoom(edAd_codart.Text, oParam)    'abilito la gestione dello zoom veloce
      NTSZOOM.strIn = ""
      oParam.nMagaz = 0
      oParam.nListino = 1
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      '----------------------------------------------------------------------------------
      If NTSZOOM.strIn.ToString = "" Then Return
      '----------------------------------------------------------------------------------
      If NTSZOOM.strIn <> edAd_codart.Text Then
        If dsGsad.Tables("ARTDEF").Select("ad_codart = " & CStrSQL(NTSZOOM.strIn.ToString)).Length = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128565233960370508, "Codice articolo inesistente"))
          Return
        Else
          'mi posiziono sul codice inserito
          For i = 0 To dcGsad.List.Count - 1
            If CType(dcGsad.Item(i), DataRowView)!ad_codart.ToString = NTSZOOM.strIn.ToString Then
              dcGsad.Position = i
              edAd_sommat.Focus()
              Exit For
            End If
          Next
        End If
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '-------------------------------------------------
      'prima di salvare simulo una lostfocus del campo su cui mi trovo, altrimenti potrei salvare un dato non corretto
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
      '-------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128547959003989217, "Cancellare il progressivo definitivo articolo?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsGsad.Tables("ARTDEF").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          dcGsad.RemoveAt(dcGsad.Position)
          oCleGsad.Salva(True)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcGsad, Me)
            bRemovBinding = False
            edAd_codart.Enabled = True
            edAd_fase.Enabled = True
            edAd_magaz.Enabled = True
          Else
            edAd_codart.Enabled = False
            edAd_fase.Enabled = False
            edAd_magaz.Enabled = False
          End If
          Return
      End Select
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGsad, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco
      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128548226121174428, "Ripristinare le modifiche apportate al progressivo definitivo articolo/magazzino?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsGsad.Tables("ARTDEF").Rows.Count = 1 And dsGsad.Tables("ARTDEF").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          oCleGsad.Ripristina(dcGsad.Position, dcGsad.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcGsad, Me)
            bRemovBinding = False
            edAd_codart.Enabled = True
            edAd_fase.Enabled = True
            edAd_magaz.Enabled = True
          Else
            edAd_codart.Enabled = False
            edAd_fase.Enabled = False
            edAd_magaz.Enabled = False
          End If
      End Select
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGsad, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edAd_fase.Focused Then
        If edAd_codart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128548227428255868, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edAd_fase.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edAd_fase.Text)
        oParam.strTipo = edAd_codart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAd_fase.Text Then edAd_fase.NTSTextDB = NTSZOOM.strIn
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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcGsad.MoveFirst()
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
      If Not Salva() Then Return
      dcGsad.MovePrevious()
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
      If Not Salva() Then Return
      dcGsad.MoveNext()
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
      If Not Salva() Then Return
      dcGsad.MoveLast()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim i As Integer = 0
    Dim strMsg As String = ""
    Dim dRes As DialogResult
    Dim dttArtdefx As New DataTable
    Dim dttLotcpro As New DataTable

    Try
      '----------------------------------------------------------------------------------
      '--- Chiedo conferma e, se necessario, salvo
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      '----------------------------------------------------------------------------------
      If oCleGsad.RecordIsChanged Then
        '--------------------------------------------------------------------------------
        '--- Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        '--------------------------------------------------------------------------------
        If GctlControllaOutNotEqual() = False Then Return False
        '--------------------------------------------------------------------------------
        '--- Se non esistono dati in ARTDEFX e/o LOTCDEF, chiede conferma prima di
        '--- aggiornare
        '--------------------------------------------------------------------------------
        oCleGsad.RitornaArtdefxLotcpro(edAd_codart.Text, NTSCInt(edAd_fase.Text), _
          NTSCInt(edAd_magaz.Text), dttArtdefx, dttLotcpro)
        If (dttArtdefx.Rows.Count = 0) Or (dttLotcpro.Rows.Count = 0) Then
          strMsg = oApp.Tr(Me, 129073287588066989, "Attenzione!" & vbCrLf & _
            "Per l'articolo '|" & edAd_codart.Text & "|' - |" & lbXx_codart.Text & "|" & vbCrLf & _
            "Fase '|" & edAd_fase.Text & "|'|" & IIf(NTSCInt(edAd_fase.Text) <> 0, " - " & lbXx_fase.Text, "").ToString & "|" & vbCrLf & _
            "Magazzino '|" & edAd_magaz.Text & "|'|" & IIf(NTSCInt(edAd_magaz.Text) <> 0, " - " & lbXx_magaz.Text, "").ToString & "|" & vbCrLf & _
            "NON esistono dati relativi in: " & vbCrLf)
          If dttArtdefx.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129073287632443977, " --> PROGRESSIVI DEFINITIVI ARTICOLI" & vbCrLf)
          End If
          If dttLotcpro.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129073287651819845, " --> PROGRESSIVI CORRENTI ARTICOLO/LOTTO/COMMESSA/FASE/UBICAZIONE" & vbCrLf)
          End If
          strMsg += oApp.Tr(Me, 129073287677445993, "Confermare ugualmente il salvataggio del progressivo definitivo articolo?")
          dRes = oApp.MsgBoxInfoYesNo_DefNo(strMsg)
        Else
          strMsg = oApp.Tr(Me, 129073289005012035, "Attenzione!" & vbCrLf & _
            "Ricordarsi di sistemare le quantità e i valori esistenza in:" & vbCrLf & _
            " --> PROGRESSIVI DEFINITIVI ARTICOLI:" & vbCrLf & _
            "      articolo: |" & edAd_codart.Text & "| / fase: |" & edAd_fase.Text & "|" & vbCrLf & _
            " --> PROGRESSIVI CORRENTI ARTICOLO/LOTTO/COMMESSA/FASE/UBICAZIONE" & vbCrLf)
          For i = 0 To (dttLotcpro.Rows.Count - 1)
            With dttLotcpro.Rows(i)
              strMsg += oApp.Tr(Me, 129073289030013315, "      mag.: |" & edAd_magaz.Text & "| / " & _
                "fase: |" & edAd_fase.Text & "| / " & _
                "comm.: |" & !ld_commeca.ToString & "| / " & _
                "lotto: |" & !xx_lottox.ToString & "| / " & _
                "ubicaz.: |" & !ld_ubicaz.ToString & "|" & vbCrLf)
            End With
          Next
          Me.NTSBigBox(strMsg, oApp.Tr(Me, 130681191122899777, "Attenzione"), 0, False, True, False)
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129073289050326855, "Confermare il salvataggio del progressivo definitivo articolo?"))
        End If
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Cancel : Return False
          Case System.Windows.Forms.DialogResult.Yes
            If Not oCleGsad.Salva(False) Then Return False
            If dsGsad.Tables("ARTDEF").Rows.Count > 0 Then
              edAd_codart.Enabled = False
              edAd_fase.Enabled = False
              edAd_magaz.Enabled = False
            End If
          Case System.Windows.Forms.DialogResult.No : tlbRipristina_ItemClick(Nothing, Nothing)
        End Select
      End If
      '----------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  
End Class
