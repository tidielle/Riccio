Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGSAY
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
  Public oCleGsay As CLEMGGSAY
  Public dsGsay As DataSet
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer
  Public WithEvents lbAdx_fase As NTSInformatica.NTSLabel
  Public WithEvents lbAdx_codart As NTSInformatica.NTSLabel
  Public WithEvents edAdx_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edAdx_fase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_fase As NTSInformatica.NTSLabel
  Public WithEvents tsGsay As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
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
  Public WithEvents pbIndex1 As NTSInformatica.NTSPanel
  Public WithEvents edAdx_dtulsca As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdx_dtulcar As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAdx_dtulsca As NTSInformatica.NTSLabel
  Public WithEvents lbAdx_dtulcar As NTSInformatica.NTSLabel
  Public WithEvents edAdx_ultpre As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_ultpre As NTSInformatica.NTSLabel
  Public WithEvents edAdx_peucos As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_peucos As NTSInformatica.NTSLabel
  Public WithEvents edAdx_ultcos As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_ultcos As NTSInformatica.NTSLabel
  Public WithEvents fmFrame1 As NTSInformatica.NTSGroupBox
  Public WithEvents edAdx_vqtalif As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_qtalif As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_qtalif As NTSInformatica.NTSLabel
  Public WithEvents edAdx_vgiaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_giaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_giaini As NTSInformatica.NTSLabel
  Public WithEvents edAdx_valesi As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_esist As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_esist As NTSInformatica.NTSLabel
  Public WithEvents lbValore1 As NTSInformatica.NTSLabel
  Public WithEvents lbQuantità1 As NTSInformatica.NTSLabel
  Public WithEvents pbIndex2 As NTSInformatica.NTSPanel
  Public WithEvents lbAdx_tipval As NTSInformatica.NTSLabel
  Public WithEvents edAdx_ldtulsca As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdx_ldtulcar As NTSInformatica.NTSTextBoxData
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents edAdx_lprezzo As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents edAdx_lpeucos As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents edAdx_lultcos As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents edAdx_lvqtalif As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_lqtalif As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_lqtalif As NTSInformatica.NTSLabel
  Public WithEvents edAdx_lvgiaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_lgiaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_lgiaini As NTSInformatica.NTSLabel
  Public WithEvents edAdx_lvalesi As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdx_lesist As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAdx_lesist As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel9 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel10 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipval As NTSInformatica.NTSLabel
  Public WithEvents edAdx_tipval As NTSInformatica.NTSTextBoxStr
  Public dcGsay As BindingSource = New BindingSource
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGSAY", "BEMGGSAY", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128539325490238232, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsay = CType(oTmp, CLEMGGSAY)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGSAY", strRemoteServer, strRemotePort)
    AddHandler oCleGsay.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsay.Init(oApp, NTSScript, oMenu.oCleComm, "ARTDEFX", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
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
      edAdx_codart.NTSSetParam(oMenu, oApp.Tr(Me, 128539380149421545, "Articolo"), CLN__STD.CodartMaxLen)
      edAdx_fase.NTSSetParam(oMenu, oApp.Tr(Me, 128539380182390084, "Fase"), "0", 4, 0, 9999)
      edAdx_esist.NTSSetParam(oMenu, oApp.Tr(Me, 128539380594887444, "Quantità Esistenza (tutti i magazzini)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_valesi.NTSSetParam(oMenu, oApp.Tr(Me, 128539381197383588, "Valore Esistenza (tutti i magazzini)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_giaini.NTSSetParam(oMenu, oApp.Tr(Me, 128539382810498264, "Quantità Giacenza iniziale (tutti i magazzini)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_vgiaini.NTSSetParam(oMenu, oApp.Tr(Me, 128539382833154369, "Valore Giacenza iniziale (tutti i magazzini)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_qtalif.NTSSetParam(oMenu, oApp.Tr(Me, 128539383287213963, "Quantità Carichi per valorizzazione (tutti i magazzini)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_vqtalif.NTSSetParam(oMenu, oApp.Tr(Me, 128539383309401321, "Valore Carichi per valorizzazione (tutti i magazzini)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_ultcos.NTSSetParam(oMenu, oApp.Tr(Me, 128548016901452703, "ultimo costo (tutti i magazzini)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_peucos.NTSSetParam(oMenu, oApp.Tr(Me, 128548017907227516, "ultimo costo + spese accessorie (tutti i magazzini)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_ultpre.NTSSetParam(oMenu, oApp.Tr(Me, 128548018110351216, "ultimo prezzo (tutti i magazzini)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_dtulcar.NTSSetParam(oMenu, oApp.Tr(Me, 128548021231424991, "Data ultimo carico (tutti i magazzini)"), False)
      edAdx_dtulsca.NTSSetParam(oMenu, oApp.Tr(Me, 128548022477474047, "Data ultimo scarico (tutti i magazzini)"), False)

      edAdx_tipval.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128980220553789485, "Valorizzazione"), tablist, False)
      edAdx_lesist.NTSSetParam(oMenu, oApp.Tr(Me, 128546200653673671, "Quantità Esistenza (magazzini merce propria)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_lvalesi.NTSSetParam(oMenu, oApp.Tr(Me, 128546200712111545, "Valore Esistenza (magazzini merce propria)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_lgiaini.NTSSetParam(oMenu, oApp.Tr(Me, 128546200741174231, "Quantità Giacenza iniziale (magazzini merce propria)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_lvgiaini.NTSSetParam(oMenu, oApp.Tr(Me, 128546200770080666, "Valore Giacenza iniziale (magazzini merce propria)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_lqtalif.NTSSetParam(oMenu, oApp.Tr(Me, 128546200792580810, "Quantità Carichi per valorizzazione (magazzini merce propria)"), oApp.FormatQta, 20, -999999999999, 999999999999)
      edAdx_lvqtalif.NTSSetParam(oMenu, oApp.Tr(Me, 128546200819143480, "Valore Carichi per valorizzazione (magazzini merce propria)"), oApp.FormatImporti, 20, -999999999999, 999999999999)
      edAdx_lultcos.NTSSetParam(oMenu, oApp.Tr(Me, 128548018447067811, "ultimo costo (magazzini merce propria)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_lpeucos.NTSSetParam(oMenu, oApp.Tr(Me, 128548018467848928, "ultimo costo + spese accessorie (magazzini merce propria)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_lprezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128548018488630045, "ultimo prezzo (magazzini merce propria)"), oApp.FormatPrzUn, 20, -999999999999, 999999999999)
      edAdx_ldtulcar.NTSSetParam(oMenu, oApp.Tr(Me, 128548022625764787, "Data ultimo carico (magazzini merce propria)"), False)
      edAdx_ldtulsca.NTSSetParam(oMenu, oApp.Tr(Me, 128548022647328667, "Data ultimo scarico (magazzini merce propria)"), False)

      '-------------------------------------------------
      edAdx_codart.NTSForzaVisZoom = True
      edAdx_fase.NTSForzaVisZoom = True
      edAdx_codart.NTSSetRichiesto()
      edAdx_fase.NTSSetRichiesto()
      edAdx_tipval.NTSSetRichiesto()
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGSAY))
    Me.lbAdx_codart = New NTSInformatica.NTSLabel
    Me.lbAdx_fase = New NTSInformatica.NTSLabel
    Me.edAdx_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edAdx_fase = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_fase = New NTSInformatica.NTSLabel
    Me.tsGsay = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pbIndex1 = New NTSInformatica.NTSPanel
    Me.edAdx_dtulsca = New NTSInformatica.NTSTextBoxData
    Me.edAdx_dtulcar = New NTSInformatica.NTSTextBoxData
    Me.lbAdx_dtulsca = New NTSInformatica.NTSLabel
    Me.lbAdx_dtulcar = New NTSInformatica.NTSLabel
    Me.edAdx_ultpre = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_ultpre = New NTSInformatica.NTSLabel
    Me.edAdx_peucos = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_peucos = New NTSInformatica.NTSLabel
    Me.edAdx_ultcos = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_ultcos = New NTSInformatica.NTSLabel
    Me.fmFrame1 = New NTSInformatica.NTSGroupBox
    Me.edAdx_vqtalif = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_qtalif = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_qtalif = New NTSInformatica.NTSLabel
    Me.edAdx_vgiaini = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_giaini = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_giaini = New NTSInformatica.NTSLabel
    Me.edAdx_valesi = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_esist = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_esist = New NTSInformatica.NTSLabel
    Me.lbValore1 = New NTSInformatica.NTSLabel
    Me.lbQuantità1 = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pbIndex2 = New NTSInformatica.NTSPanel
    Me.lbAdx_tipval = New NTSInformatica.NTSLabel
    Me.edAdx_ldtulsca = New NTSInformatica.NTSTextBoxData
    Me.edAdx_ldtulcar = New NTSInformatica.NTSTextBoxData
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.edAdx_lprezzo = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.edAdx_lpeucos = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.edAdx_lultcos = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.edAdx_lvqtalif = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_lqtalif = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_lqtalif = New NTSInformatica.NTSLabel
    Me.edAdx_lvgiaini = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_lgiaini = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_lgiaini = New NTSInformatica.NTSLabel
    Me.edAdx_lvalesi = New NTSInformatica.NTSTextBoxNum
    Me.edAdx_lesist = New NTSInformatica.NTSTextBoxNum
    Me.lbAdx_lesist = New NTSInformatica.NTSLabel
    Me.NtsLabel9 = New NTSInformatica.NTSLabel
    Me.NtsLabel10 = New NTSInformatica.NTSLabel
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
    Me.lbXx_tipval = New NTSInformatica.NTSLabel
    Me.edAdx_tipval = New NTSInformatica.NTSTextBoxStr
    CType(Me.edAdx_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_fase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsGsay, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsGsay.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pbIndex1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbIndex1.SuspendLayout()
    CType(Me.edAdx_dtulsca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_dtulcar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_ultpre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_peucos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_ultcos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame1.SuspendLayout()
    CType(Me.edAdx_vqtalif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_qtalif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_vgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_giaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_valesi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_esist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pbIndex2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbIndex2.SuspendLayout()
    CType(Me.edAdx_ldtulsca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_ldtulcar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lprezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lpeucos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lultcos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.edAdx_lvqtalif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lqtalif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lvgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lvalesi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_lesist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdx_tipval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'lbAdx_codart
    '
    Me.lbAdx_codart.AutoSize = True
    Me.lbAdx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_codart.Location = New System.Drawing.Point(12, 41)
    Me.lbAdx_codart.Name = "lbAdx_codart"
    Me.lbAdx_codart.NTSDbField = ""
    Me.lbAdx_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbAdx_codart.TabIndex = 4
    Me.lbAdx_codart.Text = "Articolo"
    '
    'lbAdx_fase
    '
    Me.lbAdx_fase.AutoSize = True
    Me.lbAdx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_fase.Location = New System.Drawing.Point(12, 67)
    Me.lbAdx_fase.Name = "lbAdx_fase"
    Me.lbAdx_fase.NTSDbField = ""
    Me.lbAdx_fase.Size = New System.Drawing.Size(30, 13)
    Me.lbAdx_fase.TabIndex = 5
    Me.lbAdx_fase.Text = "Fase"
    '
    'edAdx_codart
    '
    Me.edAdx_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_codart.Location = New System.Drawing.Point(70, 38)
    Me.edAdx_codart.Name = "edAdx_codart"
    Me.edAdx_codart.NTSDbField = ""
    Me.edAdx_codart.NTSForzaVisZoom = False
    Me.edAdx_codart.NTSOldValue = ""
    Me.edAdx_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_codart.Properties.MaxLength = 65536
    Me.edAdx_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_codart.Size = New System.Drawing.Size(198, 20)
    Me.edAdx_codart.TabIndex = 0
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(274, 38)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(385, 20)
    Me.lbXx_codart.TabIndex = 7
    Me.lbXx_codart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'edAdx_fase
    '
    Me.edAdx_fase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_fase.EditValue = "0"
    Me.edAdx_fase.Location = New System.Drawing.Point(70, 64)
    Me.edAdx_fase.Name = "edAdx_fase"
    Me.edAdx_fase.NTSDbField = ""
    Me.edAdx_fase.NTSFormat = "0"
    Me.edAdx_fase.NTSForzaVisZoom = False
    Me.edAdx_fase.NTSOldValue = ""
    Me.edAdx_fase.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_fase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_fase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_fase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_fase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_fase.Properties.MaxLength = 65536
    Me.edAdx_fase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_fase.Size = New System.Drawing.Size(63, 20)
    Me.edAdx_fase.TabIndex = 1
    '
    'lbXx_fase
    '
    Me.lbXx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_fase.Location = New System.Drawing.Point(139, 64)
    Me.lbXx_fase.Name = "lbXx_fase"
    Me.lbXx_fase.NTSDbField = ""
    Me.lbXx_fase.Size = New System.Drawing.Size(323, 20)
    Me.lbXx_fase.TabIndex = 9
    Me.lbXx_fase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'tsGsay
    '
    Me.tsGsay.Location = New System.Drawing.Point(15, 99)
    Me.tsGsay.Name = "tsGsay"
    Me.tsGsay.SelectedTabPage = Me.NtsTabPage1
    Me.tsGsay.Size = New System.Drawing.Size(644, 190)
    Me.tsGsay.TabIndex = 10
    Me.tsGsay.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsGsay.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.pbIndex1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(635, 160)
    Me.NtsTabPage1.Text = "&1 - Tutti i magazzini"
    '
    'pbIndex1
    '
    Me.pbIndex1.AllowDrop = True
    Me.pbIndex1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbIndex1.Appearance.Options.UseBackColor = True
    Me.pbIndex1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbIndex1.Controls.Add(Me.edAdx_dtulsca)
    Me.pbIndex1.Controls.Add(Me.edAdx_dtulcar)
    Me.pbIndex1.Controls.Add(Me.lbAdx_dtulsca)
    Me.pbIndex1.Controls.Add(Me.lbAdx_dtulcar)
    Me.pbIndex1.Controls.Add(Me.edAdx_ultpre)
    Me.pbIndex1.Controls.Add(Me.lbAdx_ultpre)
    Me.pbIndex1.Controls.Add(Me.edAdx_peucos)
    Me.pbIndex1.Controls.Add(Me.lbAdx_peucos)
    Me.pbIndex1.Controls.Add(Me.edAdx_ultcos)
    Me.pbIndex1.Controls.Add(Me.lbAdx_ultcos)
    Me.pbIndex1.Controls.Add(Me.fmFrame1)
    Me.pbIndex1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbIndex1.Location = New System.Drawing.Point(0, 0)
    Me.pbIndex1.Name = "pbIndex1"
    Me.pbIndex1.Size = New System.Drawing.Size(632, 160)
    Me.pbIndex1.TabIndex = 11
    Me.pbIndex1.Text = "NtsPanel1"
    '
    'edAdx_dtulsca
    '
    Me.edAdx_dtulsca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_dtulsca.Location = New System.Drawing.Point(527, 127)
    Me.edAdx_dtulsca.Name = "edAdx_dtulsca"
    Me.edAdx_dtulsca.NTSDbField = ""
    Me.edAdx_dtulsca.NTSForzaVisZoom = False
    Me.edAdx_dtulsca.NTSOldValue = ""
    Me.edAdx_dtulsca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_dtulsca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_dtulsca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_dtulsca.Properties.MaxLength = 65536
    Me.edAdx_dtulsca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_dtulsca.Size = New System.Drawing.Size(100, 20)
    Me.edAdx_dtulsca.TabIndex = 28
    '
    'edAdx_dtulcar
    '
    Me.edAdx_dtulcar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_dtulcar.Location = New System.Drawing.Point(527, 105)
    Me.edAdx_dtulcar.Name = "edAdx_dtulcar"
    Me.edAdx_dtulcar.NTSDbField = ""
    Me.edAdx_dtulcar.NTSForzaVisZoom = False
    Me.edAdx_dtulcar.NTSOldValue = ""
    Me.edAdx_dtulcar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_dtulcar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_dtulcar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_dtulcar.Properties.MaxLength = 65536
    Me.edAdx_dtulcar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_dtulcar.Size = New System.Drawing.Size(100, 20)
    Me.edAdx_dtulcar.TabIndex = 27
    '
    'lbAdx_dtulsca
    '
    Me.lbAdx_dtulsca.AutoSize = True
    Me.lbAdx_dtulsca.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_dtulsca.Location = New System.Drawing.Point(358, 130)
    Me.lbAdx_dtulsca.Name = "lbAdx_dtulsca"
    Me.lbAdx_dtulsca.NTSDbField = ""
    Me.lbAdx_dtulsca.Size = New System.Drawing.Size(97, 13)
    Me.lbAdx_dtulsca.TabIndex = 26
    Me.lbAdx_dtulsca.Text = "Data ultimo scarico"
    '
    'lbAdx_dtulcar
    '
    Me.lbAdx_dtulcar.AutoSize = True
    Me.lbAdx_dtulcar.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_dtulcar.Location = New System.Drawing.Point(358, 108)
    Me.lbAdx_dtulcar.Name = "lbAdx_dtulcar"
    Me.lbAdx_dtulcar.NTSDbField = ""
    Me.lbAdx_dtulcar.Size = New System.Drawing.Size(92, 13)
    Me.lbAdx_dtulcar.TabIndex = 25
    Me.lbAdx_dtulcar.Text = "Data ultimo carico"
    '
    'edAdx_ultpre
    '
    Me.edAdx_ultpre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_ultpre.EditValue = "0"
    Me.edAdx_ultpre.Location = New System.Drawing.Point(527, 83)
    Me.edAdx_ultpre.Name = "edAdx_ultpre"
    Me.edAdx_ultpre.NTSDbField = ""
    Me.edAdx_ultpre.NTSFormat = "0"
    Me.edAdx_ultpre.NTSForzaVisZoom = False
    Me.edAdx_ultpre.NTSOldValue = ""
    Me.edAdx_ultpre.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_ultpre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_ultpre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_ultpre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_ultpre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_ultpre.Properties.MaxLength = 65536
    Me.edAdx_ultpre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_ultpre.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_ultpre.TabIndex = 24
    '
    'lbAdx_ultpre
    '
    Me.lbAdx_ultpre.AutoSize = True
    Me.lbAdx_ultpre.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_ultpre.Location = New System.Drawing.Point(358, 86)
    Me.lbAdx_ultpre.Name = "lbAdx_ultpre"
    Me.lbAdx_ultpre.NTSDbField = ""
    Me.lbAdx_ultpre.Size = New System.Drawing.Size(71, 13)
    Me.lbAdx_ultpre.TabIndex = 23
    Me.lbAdx_ultpre.Text = "Ultimo prezzo"
    '
    'edAdx_peucos
    '
    Me.edAdx_peucos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_peucos.EditValue = "0"
    Me.edAdx_peucos.Location = New System.Drawing.Point(527, 61)
    Me.edAdx_peucos.Name = "edAdx_peucos"
    Me.edAdx_peucos.NTSDbField = ""
    Me.edAdx_peucos.NTSFormat = "0"
    Me.edAdx_peucos.NTSForzaVisZoom = False
    Me.edAdx_peucos.NTSOldValue = ""
    Me.edAdx_peucos.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_peucos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_peucos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_peucos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_peucos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_peucos.Properties.MaxLength = 65536
    Me.edAdx_peucos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_peucos.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_peucos.TabIndex = 22
    '
    'lbAdx_peucos
    '
    Me.lbAdx_peucos.AutoSize = True
    Me.lbAdx_peucos.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_peucos.Location = New System.Drawing.Point(358, 64)
    Me.lbAdx_peucos.Name = "lbAdx_peucos"
    Me.lbAdx_peucos.NTSDbField = ""
    Me.lbAdx_peucos.Size = New System.Drawing.Size(160, 13)
    Me.lbAdx_peucos.TabIndex = 21
    Me.lbAdx_peucos.Text = "Ultimo costo + spese accessorie"
    '
    'edAdx_ultcos
    '
    Me.edAdx_ultcos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_ultcos.EditValue = "0"
    Me.edAdx_ultcos.Location = New System.Drawing.Point(527, 38)
    Me.edAdx_ultcos.Name = "edAdx_ultcos"
    Me.edAdx_ultcos.NTSDbField = ""
    Me.edAdx_ultcos.NTSFormat = "0"
    Me.edAdx_ultcos.NTSForzaVisZoom = False
    Me.edAdx_ultcos.NTSOldValue = ""
    Me.edAdx_ultcos.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_ultcos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_ultcos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_ultcos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_ultcos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_ultcos.Properties.MaxLength = 65536
    Me.edAdx_ultcos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_ultcos.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_ultcos.TabIndex = 20
    '
    'lbAdx_ultcos
    '
    Me.lbAdx_ultcos.AutoSize = True
    Me.lbAdx_ultcos.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_ultcos.Location = New System.Drawing.Point(358, 41)
    Me.lbAdx_ultcos.Name = "lbAdx_ultcos"
    Me.lbAdx_ultcos.NTSDbField = ""
    Me.lbAdx_ultcos.Size = New System.Drawing.Size(65, 13)
    Me.lbAdx_ultcos.TabIndex = 19
    Me.lbAdx_ultcos.Text = "Ultimo costo"
    '
    'fmFrame1
    '
    Me.fmFrame1.AllowDrop = True
    Me.fmFrame1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame1.Appearance.Options.UseBackColor = True
    Me.fmFrame1.Controls.Add(Me.edAdx_vqtalif)
    Me.fmFrame1.Controls.Add(Me.edAdx_qtalif)
    Me.fmFrame1.Controls.Add(Me.lbAdx_qtalif)
    Me.fmFrame1.Controls.Add(Me.edAdx_vgiaini)
    Me.fmFrame1.Controls.Add(Me.edAdx_giaini)
    Me.fmFrame1.Controls.Add(Me.lbAdx_giaini)
    Me.fmFrame1.Controls.Add(Me.edAdx_valesi)
    Me.fmFrame1.Controls.Add(Me.edAdx_esist)
    Me.fmFrame1.Controls.Add(Me.lbAdx_esist)
    Me.fmFrame1.Controls.Add(Me.lbValore1)
    Me.fmFrame1.Controls.Add(Me.lbQuantità1)
    Me.fmFrame1.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame1.Location = New System.Drawing.Point(13, 45)
    Me.fmFrame1.Name = "fmFrame1"
    Me.fmFrame1.ShowCaption = False
    Me.fmFrame1.Size = New System.Drawing.Size(328, 102)
    Me.fmFrame1.TabIndex = 18
    Me.fmFrame1.Text = "NtsGroupBox1"
    '
    'edAdx_vqtalif
    '
    Me.edAdx_vqtalif.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_vqtalif.EditValue = "0"
    Me.edAdx_vqtalif.Location = New System.Drawing.Point(235, 72)
    Me.edAdx_vqtalif.Name = "edAdx_vqtalif"
    Me.edAdx_vqtalif.NTSDbField = ""
    Me.edAdx_vqtalif.NTSFormat = "0"
    Me.edAdx_vqtalif.NTSForzaVisZoom = False
    Me.edAdx_vqtalif.NTSOldValue = ""
    Me.edAdx_vqtalif.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_vqtalif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_vqtalif.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_vqtalif.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_vqtalif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_vqtalif.Properties.MaxLength = 65536
    Me.edAdx_vqtalif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_vqtalif.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_vqtalif.TabIndex = 14
    '
    'edAdx_qtalif
    '
    Me.edAdx_qtalif.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_qtalif.EditValue = "0"
    Me.edAdx_qtalif.Location = New System.Drawing.Point(148, 72)
    Me.edAdx_qtalif.Name = "edAdx_qtalif"
    Me.edAdx_qtalif.NTSDbField = ""
    Me.edAdx_qtalif.NTSFormat = "0"
    Me.edAdx_qtalif.NTSForzaVisZoom = False
    Me.edAdx_qtalif.NTSOldValue = ""
    Me.edAdx_qtalif.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_qtalif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_qtalif.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_qtalif.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_qtalif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_qtalif.Properties.MaxLength = 65536
    Me.edAdx_qtalif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_qtalif.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_qtalif.TabIndex = 13
    '
    'lbAdx_qtalif
    '
    Me.lbAdx_qtalif.AutoSize = True
    Me.lbAdx_qtalif.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_qtalif.Location = New System.Drawing.Point(15, 74)
    Me.lbAdx_qtalif.Name = "lbAdx_qtalif"
    Me.lbAdx_qtalif.NTSDbField = ""
    Me.lbAdx_qtalif.Size = New System.Drawing.Size(128, 13)
    Me.lbAdx_qtalif.TabIndex = 12
    Me.lbAdx_qtalif.Text = "Carichi per valorizzazione"
    '
    'edAdx_vgiaini
    '
    Me.edAdx_vgiaini.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAdx_vgiaini.EditValue = "0"
    Me.edAdx_vgiaini.Location = New System.Drawing.Point(235, 50)
    Me.edAdx_vgiaini.Name = "edAdx_vgiaini"
    Me.edAdx_vgiaini.NTSDbField = ""
    Me.edAdx_vgiaini.NTSFormat = "0"
    Me.edAdx_vgiaini.NTSForzaVisZoom = False
    Me.edAdx_vgiaini.NTSOldValue = ""
    Me.edAdx_vgiaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_vgiaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_vgiaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_vgiaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_vgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_vgiaini.Properties.MaxLength = 65536
    Me.edAdx_vgiaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_vgiaini.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_vgiaini.TabIndex = 11
    '
    'edAdx_giaini
    '
    Me.edAdx_giaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_giaini.EditValue = "0"
    Me.edAdx_giaini.Location = New System.Drawing.Point(148, 50)
    Me.edAdx_giaini.Name = "edAdx_giaini"
    Me.edAdx_giaini.NTSDbField = ""
    Me.edAdx_giaini.NTSFormat = "0"
    Me.edAdx_giaini.NTSForzaVisZoom = False
    Me.edAdx_giaini.NTSOldValue = ""
    Me.edAdx_giaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_giaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_giaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_giaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_giaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_giaini.Properties.MaxLength = 65536
    Me.edAdx_giaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_giaini.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_giaini.TabIndex = 10
    '
    'lbAdx_giaini
    '
    Me.lbAdx_giaini.AutoSize = True
    Me.lbAdx_giaini.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_giaini.Location = New System.Drawing.Point(15, 53)
    Me.lbAdx_giaini.Name = "lbAdx_giaini"
    Me.lbAdx_giaini.NTSDbField = ""
    Me.lbAdx_giaini.Size = New System.Drawing.Size(84, 13)
    Me.lbAdx_giaini.TabIndex = 9
    Me.lbAdx_giaini.Text = "Giacenza iniziale"
    '
    'edAdx_valesi
    '
    Me.edAdx_valesi.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAdx_valesi.EditValue = "0"
    Me.edAdx_valesi.Location = New System.Drawing.Point(235, 28)
    Me.edAdx_valesi.Name = "edAdx_valesi"
    Me.edAdx_valesi.NTSDbField = ""
    Me.edAdx_valesi.NTSFormat = "0"
    Me.edAdx_valesi.NTSForzaVisZoom = False
    Me.edAdx_valesi.NTSOldValue = ""
    Me.edAdx_valesi.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_valesi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_valesi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_valesi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_valesi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_valesi.Properties.MaxLength = 65536
    Me.edAdx_valesi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_valesi.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_valesi.TabIndex = 8
    '
    'edAdx_esist
    '
    Me.edAdx_esist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_esist.EditValue = "5"
    Me.edAdx_esist.Location = New System.Drawing.Point(148, 28)
    Me.edAdx_esist.Name = "edAdx_esist"
    Me.edAdx_esist.NTSDbField = ""
    Me.edAdx_esist.NTSFormat = "0"
    Me.edAdx_esist.NTSForzaVisZoom = False
    Me.edAdx_esist.NTSOldValue = "5"
    Me.edAdx_esist.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_esist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_esist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_esist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_esist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_esist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_esist.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_esist.TabIndex = 7
    '
    'lbAdx_esist
    '
    Me.lbAdx_esist.AutoSize = True
    Me.lbAdx_esist.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_esist.Location = New System.Drawing.Point(15, 31)
    Me.lbAdx_esist.Name = "lbAdx_esist"
    Me.lbAdx_esist.NTSDbField = ""
    Me.lbAdx_esist.Size = New System.Drawing.Size(52, 13)
    Me.lbAdx_esist.TabIndex = 6
    Me.lbAdx_esist.Text = "Esistenza"
    '
    'lbValore1
    '
    Me.lbValore1.AutoSize = True
    Me.lbValore1.BackColor = System.Drawing.Color.Transparent
    Me.lbValore1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbValore1.Location = New System.Drawing.Point(247, 12)
    Me.lbValore1.Name = "lbValore1"
    Me.lbValore1.NTSDbField = ""
    Me.lbValore1.Size = New System.Drawing.Size(50, 13)
    Me.lbValore1.TabIndex = 1
    Me.lbValore1.Text = "VALORE"
    '
    'lbQuantità1
    '
    Me.lbQuantità1.AutoSize = True
    Me.lbQuantità1.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantità1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbQuantità1.Location = New System.Drawing.Point(153, 12)
    Me.lbQuantità1.Name = "lbQuantità1"
    Me.lbQuantità1.NTSDbField = ""
    Me.lbQuantità1.Size = New System.Drawing.Size(68, 13)
    Me.lbQuantità1.TabIndex = 0
    Me.lbQuantità1.Text = "QUANTITA'"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pbIndex2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(635, 160)
    Me.NtsTabPage2.Text = "&2 - Magazzini merce propria"
    '
    'pbIndex2
    '
    Me.pbIndex2.AllowDrop = True
    Me.pbIndex2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbIndex2.Appearance.Options.UseBackColor = True
    Me.pbIndex2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbIndex2.Controls.Add(Me.lbXx_tipval)
    Me.pbIndex2.Controls.Add(Me.edAdx_tipval)
    Me.pbIndex2.Controls.Add(Me.lbAdx_tipval)
    Me.pbIndex2.Controls.Add(Me.edAdx_ldtulsca)
    Me.pbIndex2.Controls.Add(Me.edAdx_ldtulcar)
    Me.pbIndex2.Controls.Add(Me.NtsLabel1)
    Me.pbIndex2.Controls.Add(Me.NtsLabel2)
    Me.pbIndex2.Controls.Add(Me.edAdx_lprezzo)
    Me.pbIndex2.Controls.Add(Me.NtsLabel3)
    Me.pbIndex2.Controls.Add(Me.edAdx_lpeucos)
    Me.pbIndex2.Controls.Add(Me.NtsLabel4)
    Me.pbIndex2.Controls.Add(Me.edAdx_lultcos)
    Me.pbIndex2.Controls.Add(Me.NtsLabel5)
    Me.pbIndex2.Controls.Add(Me.NtsGroupBox1)
    Me.pbIndex2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbIndex2.Location = New System.Drawing.Point(0, 0)
    Me.pbIndex2.Name = "pbIndex2"
    Me.pbIndex2.Size = New System.Drawing.Size(632, 160)
    Me.pbIndex2.TabIndex = 11
    Me.pbIndex2.Text = "NtsPanel1"
    '
    'lbAdx_tipval
    '
    Me.lbAdx_tipval.AutoSize = True
    Me.lbAdx_tipval.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_tipval.Location = New System.Drawing.Point(28, 18)
    Me.lbAdx_tipval.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
    Me.lbAdx_tipval.Name = "lbAdx_tipval"
    Me.lbAdx_tipval.NTSDbField = ""
    Me.lbAdx_tipval.Size = New System.Drawing.Size(74, 13)
    Me.lbAdx_tipval.TabIndex = 51
    Me.lbAdx_tipval.Text = "Valorizzazione"
    '
    'edAdx_ldtulsca
    '
    Me.edAdx_ldtulsca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_ldtulsca.Location = New System.Drawing.Point(527, 127)
    Me.edAdx_ldtulsca.Name = "edAdx_ldtulsca"
    Me.edAdx_ldtulsca.NTSDbField = ""
    Me.edAdx_ldtulsca.NTSForzaVisZoom = False
    Me.edAdx_ldtulsca.NTSOldValue = ""
    Me.edAdx_ldtulsca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_ldtulsca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_ldtulsca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_ldtulsca.Properties.MaxLength = 65536
    Me.edAdx_ldtulsca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_ldtulsca.Size = New System.Drawing.Size(100, 20)
    Me.edAdx_ldtulsca.TabIndex = 49
    '
    'edAdx_ldtulcar
    '
    Me.edAdx_ldtulcar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_ldtulcar.Location = New System.Drawing.Point(527, 105)
    Me.edAdx_ldtulcar.Name = "edAdx_ldtulcar"
    Me.edAdx_ldtulcar.NTSDbField = ""
    Me.edAdx_ldtulcar.NTSForzaVisZoom = False
    Me.edAdx_ldtulcar.NTSOldValue = ""
    Me.edAdx_ldtulcar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_ldtulcar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_ldtulcar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_ldtulcar.Properties.MaxLength = 65536
    Me.edAdx_ldtulcar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_ldtulcar.Size = New System.Drawing.Size(100, 20)
    Me.edAdx_ldtulcar.TabIndex = 48
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(358, 130)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(97, 13)
    Me.NtsLabel1.TabIndex = 47
    Me.NtsLabel1.Text = "Data ultimo scarico"
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(358, 108)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(92, 13)
    Me.NtsLabel2.TabIndex = 46
    Me.NtsLabel2.Text = "Data ultimo carico"
    '
    'edAdx_lprezzo
    '
    Me.edAdx_lprezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lprezzo.EditValue = "0"
    Me.edAdx_lprezzo.Location = New System.Drawing.Point(527, 83)
    Me.edAdx_lprezzo.Name = "edAdx_lprezzo"
    Me.edAdx_lprezzo.NTSDbField = ""
    Me.edAdx_lprezzo.NTSFormat = "0"
    Me.edAdx_lprezzo.NTSForzaVisZoom = False
    Me.edAdx_lprezzo.NTSOldValue = ""
    Me.edAdx_lprezzo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lprezzo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lprezzo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lprezzo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lprezzo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lprezzo.Properties.MaxLength = 65536
    Me.edAdx_lprezzo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lprezzo.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lprezzo.TabIndex = 45
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(358, 86)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(71, 13)
    Me.NtsLabel3.TabIndex = 44
    Me.NtsLabel3.Text = "Ultimo prezzo"
    '
    'edAdx_lpeucos
    '
    Me.edAdx_lpeucos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lpeucos.EditValue = "0"
    Me.edAdx_lpeucos.Location = New System.Drawing.Point(527, 61)
    Me.edAdx_lpeucos.Name = "edAdx_lpeucos"
    Me.edAdx_lpeucos.NTSDbField = ""
    Me.edAdx_lpeucos.NTSFormat = "0"
    Me.edAdx_lpeucos.NTSForzaVisZoom = False
    Me.edAdx_lpeucos.NTSOldValue = ""
    Me.edAdx_lpeucos.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lpeucos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lpeucos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lpeucos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lpeucos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lpeucos.Properties.MaxLength = 65536
    Me.edAdx_lpeucos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lpeucos.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lpeucos.TabIndex = 43
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(358, 64)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(160, 13)
    Me.NtsLabel4.TabIndex = 42
    Me.NtsLabel4.Text = "Ultimo costo + spese accessorie"
    '
    'edAdx_lultcos
    '
    Me.edAdx_lultcos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lultcos.EditValue = "0"
    Me.edAdx_lultcos.Location = New System.Drawing.Point(527, 38)
    Me.edAdx_lultcos.Name = "edAdx_lultcos"
    Me.edAdx_lultcos.NTSDbField = ""
    Me.edAdx_lultcos.NTSFormat = "0"
    Me.edAdx_lultcos.NTSForzaVisZoom = False
    Me.edAdx_lultcos.NTSOldValue = ""
    Me.edAdx_lultcos.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lultcos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lultcos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lultcos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lultcos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lultcos.Properties.MaxLength = 65536
    Me.edAdx_lultcos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lultcos.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lultcos.TabIndex = 41
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(358, 41)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(65, 13)
    Me.NtsLabel5.TabIndex = 40
    Me.NtsLabel5.Text = "Ultimo costo"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lvqtalif)
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lqtalif)
    Me.NtsGroupBox1.Controls.Add(Me.lbAdx_lqtalif)
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lvgiaini)
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lgiaini)
    Me.NtsGroupBox1.Controls.Add(Me.lbAdx_lgiaini)
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lvalesi)
    Me.NtsGroupBox1.Controls.Add(Me.edAdx_lesist)
    Me.NtsGroupBox1.Controls.Add(Me.lbAdx_lesist)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel9)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel10)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(13, 45)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.ShowCaption = False
    Me.NtsGroupBox1.Size = New System.Drawing.Size(328, 102)
    Me.NtsGroupBox1.TabIndex = 39
    Me.NtsGroupBox1.Text = "NtsGroupBox1"
    '
    'edAdx_lvqtalif
    '
    Me.edAdx_lvqtalif.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAdx_lvqtalif.EditValue = "0"
    Me.edAdx_lvqtalif.Location = New System.Drawing.Point(235, 72)
    Me.edAdx_lvqtalif.Name = "edAdx_lvqtalif"
    Me.edAdx_lvqtalif.NTSDbField = ""
    Me.edAdx_lvqtalif.NTSFormat = "0"
    Me.edAdx_lvqtalif.NTSForzaVisZoom = False
    Me.edAdx_lvqtalif.NTSOldValue = ""
    Me.edAdx_lvqtalif.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lvqtalif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lvqtalif.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lvqtalif.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lvqtalif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lvqtalif.Properties.MaxLength = 65536
    Me.edAdx_lvqtalif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lvqtalif.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lvqtalif.TabIndex = 14
    '
    'edAdx_lqtalif
    '
    Me.edAdx_lqtalif.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lqtalif.EditValue = "0"
    Me.edAdx_lqtalif.Location = New System.Drawing.Point(148, 72)
    Me.edAdx_lqtalif.Name = "edAdx_lqtalif"
    Me.edAdx_lqtalif.NTSDbField = ""
    Me.edAdx_lqtalif.NTSFormat = "0"
    Me.edAdx_lqtalif.NTSForzaVisZoom = False
    Me.edAdx_lqtalif.NTSOldValue = ""
    Me.edAdx_lqtalif.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lqtalif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lqtalif.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lqtalif.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lqtalif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lqtalif.Properties.MaxLength = 65536
    Me.edAdx_lqtalif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lqtalif.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lqtalif.TabIndex = 13
    '
    'lbAdx_lqtalif
    '
    Me.lbAdx_lqtalif.AutoSize = True
    Me.lbAdx_lqtalif.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_lqtalif.Location = New System.Drawing.Point(15, 74)
    Me.lbAdx_lqtalif.Name = "lbAdx_lqtalif"
    Me.lbAdx_lqtalif.NTSDbField = ""
    Me.lbAdx_lqtalif.Size = New System.Drawing.Size(128, 13)
    Me.lbAdx_lqtalif.TabIndex = 12
    Me.lbAdx_lqtalif.Text = "Carichi per valorizzazione"
    '
    'edAdx_lvgiaini
    '
    Me.edAdx_lvgiaini.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAdx_lvgiaini.EditValue = "0"
    Me.edAdx_lvgiaini.Location = New System.Drawing.Point(235, 50)
    Me.edAdx_lvgiaini.Name = "edAdx_lvgiaini"
    Me.edAdx_lvgiaini.NTSDbField = ""
    Me.edAdx_lvgiaini.NTSFormat = "0"
    Me.edAdx_lvgiaini.NTSForzaVisZoom = False
    Me.edAdx_lvgiaini.NTSOldValue = ""
    Me.edAdx_lvgiaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lvgiaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lvgiaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lvgiaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lvgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lvgiaini.Properties.MaxLength = 65536
    Me.edAdx_lvgiaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lvgiaini.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lvgiaini.TabIndex = 11
    '
    'edAdx_lgiaini
    '
    Me.edAdx_lgiaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lgiaini.EditValue = "0"
    Me.edAdx_lgiaini.Location = New System.Drawing.Point(148, 50)
    Me.edAdx_lgiaini.Name = "edAdx_lgiaini"
    Me.edAdx_lgiaini.NTSDbField = ""
    Me.edAdx_lgiaini.NTSFormat = "0"
    Me.edAdx_lgiaini.NTSForzaVisZoom = False
    Me.edAdx_lgiaini.NTSOldValue = ""
    Me.edAdx_lgiaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lgiaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lgiaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lgiaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lgiaini.Properties.MaxLength = 65536
    Me.edAdx_lgiaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lgiaini.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lgiaini.TabIndex = 10
    '
    'lbAdx_lgiaini
    '
    Me.lbAdx_lgiaini.AutoSize = True
    Me.lbAdx_lgiaini.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_lgiaini.Location = New System.Drawing.Point(15, 53)
    Me.lbAdx_lgiaini.Name = "lbAdx_lgiaini"
    Me.lbAdx_lgiaini.NTSDbField = ""
    Me.lbAdx_lgiaini.Size = New System.Drawing.Size(84, 13)
    Me.lbAdx_lgiaini.TabIndex = 9
    Me.lbAdx_lgiaini.Text = "Giacenza iniziale"
    '
    'edAdx_lvalesi
    '
    Me.edAdx_lvalesi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAdx_lvalesi.EditValue = "0"
    Me.edAdx_lvalesi.Location = New System.Drawing.Point(235, 28)
    Me.edAdx_lvalesi.Name = "edAdx_lvalesi"
    Me.edAdx_lvalesi.NTSDbField = ""
    Me.edAdx_lvalesi.NTSFormat = "0"
    Me.edAdx_lvalesi.NTSForzaVisZoom = False
    Me.edAdx_lvalesi.NTSOldValue = ""
    Me.edAdx_lvalesi.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lvalesi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lvalesi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lvalesi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lvalesi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lvalesi.Properties.MaxLength = 65536
    Me.edAdx_lvalesi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lvalesi.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lvalesi.TabIndex = 8
    '
    'edAdx_lesist
    '
    Me.edAdx_lesist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_lesist.EditValue = "0"
    Me.edAdx_lesist.Location = New System.Drawing.Point(148, 28)
    Me.edAdx_lesist.Name = "edAdx_lesist"
    Me.edAdx_lesist.NTSDbField = ""
    Me.edAdx_lesist.NTSFormat = "0"
    Me.edAdx_lesist.NTSForzaVisZoom = False
    Me.edAdx_lesist.NTSOldValue = ""
    Me.edAdx_lesist.Properties.Appearance.Options.UseTextOptions = True
    Me.edAdx_lesist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAdx_lesist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_lesist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_lesist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.Utils.HorzAlignment.Center, Nothing)})
    Me.edAdx_lesist.Properties.MaxLength = 65536
    Me.edAdx_lesist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_lesist.Size = New System.Drawing.Size(73, 20)
    Me.edAdx_lesist.TabIndex = 7
    '
    'lbAdx_lesist
    '
    Me.lbAdx_lesist.AutoSize = True
    Me.lbAdx_lesist.BackColor = System.Drawing.Color.Transparent
    Me.lbAdx_lesist.Location = New System.Drawing.Point(15, 31)
    Me.lbAdx_lesist.Name = "lbAdx_lesist"
    Me.lbAdx_lesist.NTSDbField = ""
    Me.lbAdx_lesist.Size = New System.Drawing.Size(52, 13)
    Me.lbAdx_lesist.TabIndex = 6
    Me.lbAdx_lesist.Text = "Esistenza"
    '
    'NtsLabel9
    '
    Me.NtsLabel9.AutoSize = True
    Me.NtsLabel9.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel9.Location = New System.Drawing.Point(247, 12)
    Me.NtsLabel9.Name = "NtsLabel9"
    Me.NtsLabel9.NTSDbField = ""
    Me.NtsLabel9.Size = New System.Drawing.Size(50, 13)
    Me.NtsLabel9.TabIndex = 1
    Me.NtsLabel9.Text = "VALORE"
    '
    'NtsLabel10
    '
    Me.NtsLabel10.AutoSize = True
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel10.Location = New System.Drawing.Point(153, 12)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.NTSDbField = ""
    Me.NtsLabel10.Size = New System.Drawing.Size(68, 13)
    Me.NtsLabel10.TabIndex = 0
    Me.NtsLabel10.Text = "QUANTITA'"
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
    Me.NtsBarManager1.MaxItemId = 13
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
    Me.tlbNuovo.Id = 1
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 12
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 2
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
    Me.tlbCancella.Id = 4
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 6
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 7
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 8
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 9
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 10
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 11
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbXx_tipval
    '
    Me.lbXx_tipval.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipval.Location = New System.Drawing.Point(177, 15)
    Me.lbXx_tipval.Name = "lbXx_tipval"
    Me.lbXx_tipval.NTSDbField = ""
    Me.lbXx_tipval.Size = New System.Drawing.Size(423, 20)
    Me.lbXx_tipval.TabIndex = 56
    Me.lbXx_tipval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'edAdx_tipval
    '
    Me.edAdx_tipval.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdx_tipval.Location = New System.Drawing.Point(108, 15)
    Me.edAdx_tipval.Name = "edAdx_tipval"
    Me.edAdx_tipval.NTSDbField = ""
    Me.edAdx_tipval.NTSForzaVisZoom = False
    Me.edAdx_tipval.NTSOldValue = ""
    Me.edAdx_tipval.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdx_tipval.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdx_tipval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdx_tipval.Properties.MaxLength = 65536
    Me.edAdx_tipval.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdx_tipval.Size = New System.Drawing.Size(63, 20)
    Me.edAdx_tipval.TabIndex = 55
    '
    'FRMMGGSAY
    '
    Me.ClientSize = New System.Drawing.Size(671, 301)
    Me.Controls.Add(Me.tsGsay)
    Me.Controls.Add(Me.lbXx_fase)
    Me.Controls.Add(Me.edAdx_fase)
    Me.Controls.Add(Me.lbXx_codart)
    Me.Controls.Add(Me.edAdx_codart)
    Me.Controls.Add(Me.lbAdx_fase)
    Me.Controls.Add(Me.lbAdx_codart)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.MaximizeBox = False
    Me.Name = "FRMMGGSAY"
    Me.Text = "PROGRESSIVI DEFINITIVI ARTICOLI"
    CType(Me.edAdx_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_fase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsGsay, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsGsay.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pbIndex1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbIndex1.ResumeLayout(False)
    Me.pbIndex1.PerformLayout()
    CType(Me.edAdx_dtulsca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_dtulcar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_ultpre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_peucos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_ultcos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame1.ResumeLayout(False)
    Me.fmFrame1.PerformLayout()
    CType(Me.edAdx_vqtalif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_qtalif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_vgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_giaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_valesi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_esist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pbIndex2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbIndex2.ResumeLayout(False)
    Me.pbIndex2.PerformLayout()
    CType(Me.edAdx_ldtulsca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_ldtulcar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lprezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lpeucos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lultcos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.edAdx_lvqtalif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lqtalif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lvgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lvalesi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_lesist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdx_tipval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      edAdx_codart.NTSDbField = "artdefx.adx_codart"
      lbXx_codart.NTSDbField = "artdefx.xx_codart"
      edAdx_fase.NTSDbField = "artdefx.adx_fase"
      lbXx_fase.NTSDbField = "artdefx.xx_fase"
      edAdx_esist.NTSDbField = "artdefx.adx_esist"
      edAdx_valesi.NTSDbField = "artdefx.adx_valesi"
      edAdx_giaini.NTSDbField = "artdefx.adx_giaini"
      edAdx_vgiaini.NTSDbField = "artdefx.adx_vgiaini"
      edAdx_qtalif.NTSDbField = "artdefx.adx_qtalif"
      edAdx_vqtalif.NTSDbField = "artdefx.adx_vqtalif"
      edAdx_ultcos.NTSDbField = "artdefx.adx_ultcos"
      edAdx_peucos.NTSDbField = "artdefx.adx_peucos"
      edAdx_ultpre.NTSDbField = "artdefx.adx_ultpre"
      edAdx_dtulcar.NTSDbField = "artdefx.adx_dtulcar"
      edAdx_dtulsca.NTSDbField = "artdefx.adx_dtulsca"
      edAdx_lesist.NTSDbField = "artdefx.adx_lesist"
      edAdx_lvalesi.NTSDbField = "artdefx.adx_lvalesi"
      edAdx_lgiaini.NTSDbField = "artdefx.adx_lgiaini"
      edAdx_lvgiaini.NTSDbField = "artdefx.adx_lvgiaini"
      edAdx_lqtalif.NTSDbField = "artdefx.adx_lqtalif"
      edAdx_lvqtalif.NTSDbField = "artdefx.adx_lvqtalif"
      edAdx_lultcos.NTSDbField = "artdefx.adx_lultcos"
      edAdx_lpeucos.NTSDbField = "artdefx.adx_lpeucos"
      edAdx_lprezzo.NTSDbField = "artdefx.adx_lprezzo"
      edAdx_ldtulcar.NTSDbField = "artdefx.adx_ldtulcar"
      edAdx_ldtulsca.NTSDbField = "artdefx.adx_ldtulsca"
      edAdx_tipval.NTSDbField = "artdefx.adx_tipval"
      lbXx_tipval.NTSDbField = "artdefx.xx_tipval"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGsay, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSAX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleGsay.Apri(DittaCorrente, dsGsay) Then Me.Close()
      dcGsay.DataSource = dsGsay.Tables("ARTDEFX")
      dsGsay.AcceptChanges()
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      '------------------------------------------------
      If dsGsay.Tables("ARTDEFX").Rows.Count > 0 Then
        edAdx_codart.Enabled = False
        edAdx_fase.Enabled = False
      End If
      '-------------------------------------------------
      If dsGsay.Tables("ARTDEFX").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcGsay.ResetBindings(False)
      '-------------------------------------------------
      dcGsay.MoveFirst()
      '-------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSAX_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSAX_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGsay.Dispose()
      dsGsay.Dispose()
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
      oCleGsay.Nuovo()
      dcGsay.MoveLast()
      edAdx_codart.Enabled = True
      edAdx_fase.Enabled = True
      edAdx_codart.Focus()
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
      SetFastZoom(edAdx_codart.Text, oParam)    'abilito la gestione dello zoom veloce
      NTSZOOM.strIn = ""
      oParam.nMagaz = 0
      oParam.nListino = 1
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      '----------------------------------------------------------------------------------
      If NTSZOOM.strIn.ToString = "" Then Return
      '----------------------------------------------------------------------------------
      If NTSZOOM.strIn <> edAdx_codart.Text Then
        If dsGsay.Tables("ARTDEFX").Select("adx_codart = " & CStrSQL(NTSZOOM.strIn.ToString)).Length = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128565222686775424, "Codice articolo inesistente"))
          Return
        Else
          'mi posiziono sul codice inserito
          For i = 0 To dcGsay.List.Count - 1
            If CType(dcGsay.Item(i), DataRowView)!adx_codart.ToString = NTSZOOM.strIn.ToString Then
              dcGsay.Position = i
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
          If dsGsay.Tables("ARTDEFX").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          dcGsay.RemoveAt(dcGsay.Position)
          oCleGsay.Salva(True)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcGsay, Me)
            bRemovBinding = False
            edAdx_codart.Enabled = True
            edAdx_fase.Enabled = True
          Else
            edAdx_codart.Enabled = False
            edAdx_fase.Enabled = False
          End If
          Return
      End Select
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGsay, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128547959516801717, "Ripristinare le modifiche apportate al progressivo definitivo articolo?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsGsay.Tables("ARTDEFX").Rows.Count = 1 And dsGsay.Tables("ARTDEFX").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          oCleGsay.Ripristina(dcGsay.Position, dcGsay.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcGsay, Me)
            bRemovBinding = False
            edAdx_codart.Enabled = True
            edAdx_fase.Enabled = True
          Else
            edAdx_codart.Enabled = False
            edAdx_fase.Enabled = False
          End If
      End Select
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGsay, Me)
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

      If edAdx_codart.Focused Then
        SetFastZoom(edAdx_codart.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edAdx_codart.Text)
        oParam.nMagaz = 0
        oParam.nListino = 1
        NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAdx_codart.Text Then edAdx_codart.NTSTextDB = NTSZOOM.strIn
      ElseIf edAdx_fase.Focused Then
        If edAdx_codart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128547960324296557, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edAdx_fase.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edAdx_fase.Text)
        oParam.strTipo = edAdx_codart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAdx_fase.Text Then edAdx_fase.NTSTextDB = NTSZOOM.strIn
      Else
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
      dcGsay.MoveFirst()
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
      dcGsay.MovePrevious()
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
      dcGsay.MoveNext()
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
      dcGsay.MoveLast()
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
    Dim dttArtdef As New DataTable
    Dim dttLotcpro As New DataTable

    Try
      '----------------------------------------------------------------------------------
      '--- Chiedo conferma e, se necessario, salvo
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      '----------------------------------------------------------------------------------
      If oCleGsay.RecordIsChanged Then
        '--------------------------------------------------------------------------------
        '--- Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        '--------------------------------------------------------------------------------
        If GctlControllaOutNotEqual() = False Then Return False
        '--------------------------------------------------------------------------------
        '--- Se non esistono dati in ARTDEF e/o LOTCDEF, chiede conferma prima di
        '--- aggiornare
        '--------------------------------------------------------------------------------
        oCleGsay.RitornaArtdefLotcpro(edAdx_codart.Text, NTSCInt(edAdx_fase.Text), _
          dttArtdef, dttLotcpro)
        If (dttArtdef.Rows.Count = 0) Or (dttLotcpro.Rows.Count = 0) Then
          strMsg = oApp.Tr(Me, 129071818708979159, "Attenzione!" & vbCrLf & _
            "Per l'articolo '|" & edAdx_codart.Text & "|' - |" & lbXx_codart.Text & "|" & vbCrLf & _
            "Fase '|" & edAdx_fase.Text & "|'" & "|" & IIf(NTSCInt(edAdx_fase.Text) <> 0, " - " & lbXx_fase.Text, "").ToString & "|" & vbCrLf & _
            "NON esistono dati relativi in: " & vbCrLf)
          If dttArtdef.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129071820141349374, " --> PROGRESSIVI DEFINITIVI ARTICOLO/MAGAZZINO" & vbCrLf)
          End If
          If dttLotcpro.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129071820161193759, " --> PROGRESSIVI CORRENTI ARTICOLO/LOTTO/COMMESSA/FASE/UBICAZIONE" & vbCrLf)
          End If
          strMsg += oApp.Tr(Me, 129071820179788104, "Confermare ugualmente il salvataggio del progressivo definitivo articolo?")
          dRes = oApp.MsgBoxInfoYesNo_DefNo(strMsg)
        Else
          strMsg = oApp.Tr(Me, 129071820794651529, "Attenzione!" & vbCrLf & _
            "Ricordarsi di sistemare le quantità e i valori esistenza in: " & vbCrLf)
          strMsg += oApp.Tr(Me, 129071820820277349, " --> PROGRESSIVI DEFINITIVI ARTICOLO/MAGAZZINO:" & vbCrLf)
          For i = 0 To (dttArtdef.Rows.Count - 1)
            strMsg += oApp.Tr(Me, 129071820845121894, "      magazzino: |" & dttArtdef.Rows(i)!ad_magaz.ToString & "|" & vbCrLf)
          Next
          strMsg += oApp.Tr(Me, 129071821017158649, " --> PROGRESSIVI CORRENTI ARTICOLO/LOTTO/COMMESSA/FASE/UBICAZIONE" & vbCrLf)
          For i = 0 To (dttLotcpro.Rows.Count - 1)
            With dttLotcpro.Rows(i)
              strMsg += oApp.Tr(Me, 129071821051065984, "      mag.: |" & !ld_magaz.ToString & "| / " & _
                "comm.: |" & !ld_commeca.ToString & "| / " & _
                "lotto: |" & !xx_lottox.ToString & "| / " & _
                "ubicaz.: |" & !ld_ubicaz.ToString & "|" & vbCrLf)
            End With
          Next
          Me.NTSBigBox(strMsg, oApp.Tr(Me, 130681191122899777, "Attenzione"), 0, False, True, False)
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129071821082942004, "Confermare il salvataggio del progressivo definitivo articolo?"))
        End If
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Cancel : Return False
          Case System.Windows.Forms.DialogResult.Yes
            If Not oCleGsay.Salva(False) Then Return False
            If dsGsay.Tables("ARTDEFX").Rows.Count > 0 Then
              edAdx_codart.Enabled = False
              edAdx_fase.Enabled = False
            End If
          Case System.Windows.Forms.DialogResult.No : tlbRipristina_ItemClick(Nothing, Nothing)
        End Select
      End If
      '----------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttArtdef.Clear()
      dttArtdef.Dispose()
      dttLotcpro.Clear()
      dttLotcpro.Dispose()
    End Try
  End Function

End Class
