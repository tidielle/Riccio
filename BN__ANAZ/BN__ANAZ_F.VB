Imports System.Data
Imports System.IO
Imports NTSInformatica.CLN__STD

Public Class FRM__ANAZ_F
  Public oCleAnaz As CLE__ANAZ
  Public dsAnaz As DataSet
  Public oCallParams As CLE__CLDP
  Public dcAnaz As BindingSource = New BindingSource

  Public strPWDCancellazione As String = "nts"
  Public strCallParamsDestDiv As String = ""

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = CLN__STD.bsModSupCAE
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
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents lbXx_codditt As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azrags1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_azrags1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azpersfg As NTSInformatica.NTSLabel
  Public WithEvents cbTb_azpersfg As NTSInformatica.NTSComboBox
  Public WithEvents tlbCalcolaCodFisc As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRitornaCodFisc As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents tlbEscomp As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbIva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDatiAggCont As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAggRiclassif As NTSInformatica.NTSBarMenuItem
  Public WithEvents tsAnaz As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag3 As NTSInformatica.NTSPanel
  Public WithEvents fmWeb As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_nflogo As NTSInformatica.NTSLabel
  Public WithEvents edXx_nflogo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azwebpwd As NTSInformatica.NTSLabel
  Public WithEvents edTb_azwebpwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azwebsite As NTSInformatica.NTSLabel
  Public WithEvents edTb_azwebsite As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azwebuid As NTSInformatica.NTSLabel
  Public WithEvents edTb_azwebuid As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azcodrtac As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodrtac As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_azcodrtac As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents pnPag1Dx As NTSInformatica.NTSPanel
  Public WithEvents lbTb_azcell As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcell As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckTb_azomocodice As NTSInformatica.NTSCheckBox
  Public WithEvents cbTb_azusaem As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azusaem As NTSInformatica.NTSLabel
  Public WithEvents edTb_azemail As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azpiva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azfaxtlx As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azemail As NTSInformatica.NTSLabel
  Public WithEvents edTb_azfaxtlx As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_aztelef As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azpiva As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azcodf As NTSInformatica.NTSLabel
  Public WithEvents lbTb_aztelef As NTSInformatica.NTSLabel
  Public WithEvents pnPag1Sx As NTSInformatica.NTSPanel
  Public WithEvents lbTb_azstatofed As NTSInformatica.NTSLabel
  Public WithEvents edTb_azstatofed As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_azstato As NTSInformatica.NTSLabel
  Public WithEvents lbXx_azcodcomu As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcodcomu As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodcomu As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azcitta As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azindir As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azstato As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcap As NTSInformatica.NTSLabel
  Public WithEvents edTb_azstato As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azindir As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azprov As NTSInformatica.NTSLabel
  Public WithEvents cmdAltriIndir As NTSInformatica.NTSButton
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag2 As NTSInformatica.NTSPanel
  Public WithEvents fmPersfisica As NTSInformatica.NTSGroupBox
  Public WithEvents edTb_aztitolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_aztitolo As NTSInformatica.NTSLabel
  Public WithEvents lbTb_sesso As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcognome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azcognome As NTSInformatica.NTSLabel
  Public WithEvents edTb_aznome As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbTb_sesso As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_aznome As NTSInformatica.NTSLabel
  Public WithEvents fmNascita As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_azstanasc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_azcodcomn As NTSInformatica.NTSLabel
  Public WithEvents lbTb_datnasc As NTSInformatica.NTSLabel
  Public WithEvents edTb_datnasc As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_pronasc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_locnasc As NTSInformatica.NTSLabel
  Public WithEvents edTb_pronasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_locnasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azcodcomn As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodcomn As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azstanasc As NTSInformatica.NTSLabel
  Public WithEvents edTb_azstanasc As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckTb_azcondom As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_azsoggresi As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_azprofes As NTSInformatica.NTSCheckBox
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnDatiContabili As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnFornitura As NTSInformatica.NTSPanel
  Public WithEvents edXx_codditt As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnCondFornsx As NTSInformatica.NTSPanel
  Public WithEvents fmBanca As NTSInformatica.NTSGroupBox
  Public WithEvents lbTb_azcodabi As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcodcc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azrifriba As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodcc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azrifriba As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azcodcab As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_azcodabi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_azcodcab As NTSInformatica.NTSLabel
  Public WithEvents pnAltriDatiSx As NTSInformatica.NTSPanel
  Public WithEvents lbTb_dtulst As NTSInformatica.NTSLabel
  Public WithEvents edTb_dtulst As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_dtulap As NTSInformatica.NTSLabel
  Public WithEvents edTb_dtulap As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_pravlgp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_prdalgp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_riullgp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rgullgp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dtullgp As NTSInformatica.NTSTextBoxData
  Public WithEvents lbEscompp As NTSInformatica.NTSLabel
  Public WithEvents edTb_pravlg As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_prdalg As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_riullg As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rgulel As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rgullg As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dtulel As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_dtullg As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_rgulel As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dtulel As NTSInformatica.NTSLabel
  Public WithEvents lbTb_riullg As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pravlg As NTSInformatica.NTSLabel
  Public WithEvents lbTb_pgullg As NTSInformatica.NTSLabel
  Public WithEvents lbTb_prdalg As NTSInformatica.NTSLabel
  Public WithEvents lbTb_rgullg As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dtullg As NTSInformatica.NTSLabel
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents edTb_pgullgp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_pgullg As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_azgestscad As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_ventil As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azgestscad As NTSInformatica.NTSLabel
  Public WithEvents lbTb_ventil As NTSInformatica.NTSLabel
  Public WithEvents lbTb_escompp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_escompp As NTSInformatica.NTSLabel
  Public WithEvents lbTb_escomp As NTSInformatica.NTSLabel
  Public WithEvents edTb_escomp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_escomp As NTSInformatica.NTSLabel
  Public WithEvents edTb_escompp As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_azdoppes As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azdoppes As NTSInformatica.NTSLabel
  Public WithEvents lbXx_azcodcab As NTSInformatica.NTSLabel
  Public WithEvents lbXx_azcodabi As NTSInformatica.NTSLabel
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ANAZ", "BE__ANAZ", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128641291410937500, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleAnaz = CType(oTmp, CLE__ANAZ)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ANAZ", strRemoteServer, strRemotePort)
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAnaz.Init(oApp, oScript, oMenu.oCleComm, "TABANAZ", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
      oCleAnaz.IsNuovaAnalitica = True
    End If
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ANAZ_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbEscomp = New NTSInformatica.NTSBarButtonItem
    Me.tlbIva = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbDatiAggCont = New NTSInformatica.NTSBarMenuItem
    Me.tlbCalcolaCodFisc = New NTSInformatica.NTSBarMenuItem
    Me.tlbRitornaCodFisc = New NTSInformatica.NTSBarMenuItem
    Me.tlbAggRiclassif = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbXx_codditt = New NTSInformatica.NTSLabel
    Me.lbTb_azrags1 = New NTSInformatica.NTSLabel
    Me.edTb_azrags1 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azpersfg = New NTSInformatica.NTSLabel
    Me.cbTb_azpersfg = New NTSInformatica.NTSComboBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.edTb_imagename = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_imagename = New NTSInformatica.NTSLabel
    Me.cmdAltriIndir = New NTSInformatica.NTSButton
    Me.cmdSelImmagine = New NTSInformatica.NTSButton
    Me.edTb_azrags2 = New NTSInformatica.NTSTextBoxStr
    Me.edXx_codditt = New NTSInformatica.NTSTextBoxStr
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tsAnaz = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.pnPag1Dx = New NTSInformatica.NTSPanel
    Me.lbTb_azcell = New NTSInformatica.NTSLabel
    Me.edTb_azcell = New NTSInformatica.NTSTextBoxStr
    Me.ckTb_azomocodice = New NTSInformatica.NTSCheckBox
    Me.cbTb_azusaem = New NTSInformatica.NTSComboBox
    Me.lbTb_azusaem = New NTSInformatica.NTSLabel
    Me.edTb_azemail = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azpiva = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azfaxtlx = New NTSInformatica.NTSLabel
    Me.lbTb_azemail = New NTSInformatica.NTSLabel
    Me.edTb_azfaxtlx = New NTSInformatica.NTSTextBoxStr
    Me.edTb_aztelef = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azpiva = New NTSInformatica.NTSLabel
    Me.edTb_azcodf = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azcodf = New NTSInformatica.NTSLabel
    Me.lbTb_aztelef = New NTSInformatica.NTSLabel
    Me.pnPag1Sx = New NTSInformatica.NTSPanel
    Me.lbTb_azstatofed = New NTSInformatica.NTSLabel
    Me.edTb_azstatofed = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_azstato = New NTSInformatica.NTSLabel
    Me.lbXx_azcodcomu = New NTSInformatica.NTSLabel
    Me.lbTb_azcodcomu = New NTSInformatica.NTSLabel
    Me.edTb_azcodcomu = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azcitta = New NTSInformatica.NTSLabel
    Me.edTb_azcitta = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azindir = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azstato = New NTSInformatica.NTSLabel
    Me.lbTb_azcap = New NTSInformatica.NTSLabel
    Me.edTb_azstato = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azindir = New NTSInformatica.NTSLabel
    Me.edTb_azcap = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azprov = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azprov = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPag2 = New NTSInformatica.NTSPanel
    Me.fmPersfisica = New NTSInformatica.NTSGroupBox
    Me.edTb_aztitolo = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_aztitolo = New NTSInformatica.NTSLabel
    Me.lbTb_sesso = New NTSInformatica.NTSLabel
    Me.edTb_azcognome = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azcognome = New NTSInformatica.NTSLabel
    Me.edTb_aznome = New NTSInformatica.NTSTextBoxStr
    Me.cbTb_sesso = New NTSInformatica.NTSComboBox
    Me.lbTb_aznome = New NTSInformatica.NTSLabel
    Me.fmNascita = New NTSInformatica.NTSGroupBox
    Me.lbXx_azstanasc = New NTSInformatica.NTSLabel
    Me.lbXx_azcodcomn = New NTSInformatica.NTSLabel
    Me.lbTb_datnasc = New NTSInformatica.NTSLabel
    Me.edTb_datnasc = New NTSInformatica.NTSTextBoxData
    Me.lbTb_pronasc = New NTSInformatica.NTSLabel
    Me.lbTb_locnasc = New NTSInformatica.NTSLabel
    Me.edTb_pronasc = New NTSInformatica.NTSTextBoxStr
    Me.edTb_locnasc = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azcodcomn = New NTSInformatica.NTSLabel
    Me.edTb_azcodcomn = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azstanasc = New NTSInformatica.NTSLabel
    Me.edTb_azstanasc = New NTSInformatica.NTSTextBoxStr
    Me.ckTb_azcondom = New NTSInformatica.NTSCheckBox
    Me.ckTb_azsoggresi = New NTSInformatica.NTSCheckBox
    Me.ckTb_azprofes = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPag3 = New NTSInformatica.NTSPanel
    Me.fmWeb = New NTSInformatica.NTSGroupBox
    Me.lbTb_latitud = New NTSInformatica.NTSLabel
    Me.edTb_latitud = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_longitud = New NTSInformatica.NTSLabel
    Me.edTb_longitud = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_nflogo = New NTSInformatica.NTSLabel
    Me.edXx_nflogo = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azwebpwd = New NTSInformatica.NTSLabel
    Me.edTb_azwebpwd = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azwebsite = New NTSInformatica.NTSLabel
    Me.edTb_azwebsite = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azwebuid = New NTSInformatica.NTSLabel
    Me.edTb_azwebuid = New NTSInformatica.NTSTextBoxStr
    Me.pnAltriDatiSx = New NTSInformatica.NTSPanel
    Me.lbTb_dtulst = New NTSInformatica.NTSLabel
    Me.fmBanca = New NTSInformatica.NTSGroupBox
    Me.lbXx_azcodcaf = New NTSInformatica.NTSLabel
    Me.lbTb_azcodcaf = New NTSInformatica.NTSLabel
    Me.edTb_azcodcaf = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_azcodcab = New NTSInformatica.NTSLabel
    Me.lbXx_azcodabi = New NTSInformatica.NTSLabel
    Me.lbTb_azcodabi = New NTSInformatica.NTSLabel
    Me.lbTb_azcodcc = New NTSInformatica.NTSLabel
    Me.lbTb_azrifriba = New NTSInformatica.NTSLabel
    Me.edTb_azcodcc = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azrifriba = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azcodcab = New NTSInformatica.NTSTextBoxNum
    Me.edTb_azcodabi = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_azcodcab = New NTSInformatica.NTSLabel
    Me.lbXx_azcodrtac = New NTSInformatica.NTSLabel
    Me.edTb_dtulst = New NTSInformatica.NTSTextBoxData
    Me.edTb_azcodrtac = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_dtulap = New NTSInformatica.NTSLabel
    Me.lbTb_azcodrtac = New NTSInformatica.NTSLabel
    Me.edTb_dtulap = New NTSInformatica.NTSTextBoxData
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnDatiContabili = New NTSInformatica.NTSPanel
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.edTb_dtulliqtri = New NTSInformatica.NTSTextBoxData
    Me.edTb_dtulliq = New NTSInformatica.NTSTextBoxData
    Me.lbTb_dtulliq = New NTSInformatica.NTSLabel
    Me.edTb_uffiva = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_uffiva = New NTSInformatica.NTSLabel
    Me.fmDatiContabili = New NTSInformatica.NTSGroupBox
    Me.edTb_escomp = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_azdoppes = New NTSInformatica.NTSComboBox
    Me.edTb_escompp = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_azdoppes = New NTSInformatica.NTSLabel
    Me.lbXx_escomp = New NTSInformatica.NTSLabel
    Me.cbTb_azgestscad = New NTSInformatica.NTSComboBox
    Me.lbTb_escomp = New NTSInformatica.NTSLabel
    Me.cbTb_ventil = New NTSInformatica.NTSComboBox
    Me.lbXx_escompp = New NTSInformatica.NTSLabel
    Me.lbTb_azgestscad = New NTSInformatica.NTSLabel
    Me.lbTb_escompp = New NTSInformatica.NTSLabel
    Me.lbTb_ventil = New NTSInformatica.NTSLabel
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnFornitura = New NTSInformatica.NTSPanel
    Me.lbNota = New NTSInformatica.NTSLabel
    Me.edTb_pgullgp = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pravlgp = New NTSInformatica.NTSTextBoxNum
    Me.edTb_prdalgp = New NTSInformatica.NTSTextBoxNum
    Me.edTb_riullgp = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rgullgp = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dtullgp = New NTSInformatica.NTSTextBoxData
    Me.lbEscompp = New NTSInformatica.NTSLabel
    Me.pnCondFornsx = New NTSInformatica.NTSPanel
    Me.edTb_pgullg = New NTSInformatica.NTSTextBoxNum
    Me.edTb_pravlg = New NTSInformatica.NTSTextBoxNum
    Me.edTb_prdalg = New NTSInformatica.NTSTextBoxNum
    Me.edTb_riullg = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rgulel = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rgullg = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dtulel = New NTSInformatica.NTSTextBoxData
    Me.edTb_dtullg = New NTSInformatica.NTSTextBoxData
    Me.lbTb_rgulel = New NTSInformatica.NTSLabel
    Me.lbTb_dtulel = New NTSInformatica.NTSLabel
    Me.lbTb_riullg = New NTSInformatica.NTSLabel
    Me.lbTb_pravlg = New NTSInformatica.NTSLabel
    Me.lbTb_pgullg = New NTSInformatica.NTSLabel
    Me.lbTb_prdalg = New NTSInformatica.NTSLabel
    Me.lbTb_rgullg = New NTSInformatica.NTSLabel
    Me.lbTb_dtullg = New NTSInformatica.NTSLabel
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.cmdDelImmagine = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azrags1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azpersfg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edTb_imagename.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azrags2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_codditt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tsAnaz, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsAnaz.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.pnPag1Dx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Dx.SuspendLayout()
    CType(Me.edTb_azcell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_azomocodice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azusaem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azemail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azpiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azfaxtlx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aztelef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag1Sx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Sx.SuspendLayout()
    CType(Me.edTb_azstatofed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodcomu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azindir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azstato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2.SuspendLayout()
    CType(Me.fmPersfisica, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPersfisica.SuspendLayout()
    CType(Me.edTb_aztitolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcognome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_aznome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_sesso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmNascita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNascita.SuspendLayout()
    CType(Me.edTb_datnasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pronasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_locnasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodcomn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azstanasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_azcondom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_azsoggresi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_azprofes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag3.SuspendLayout()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmWeb.SuspendLayout()
    CType(Me.edTb_latitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_longitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edXx_nflogo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azwebpwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azwebsite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azwebuid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAltriDatiSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAltriDatiSx.SuspendLayout()
    CType(Me.fmBanca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmBanca.SuspendLayout()
    CType(Me.edTb_azcodcaf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azrifriba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodcab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodabi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulst.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodrtac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnDatiContabili, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDatiContabili.SuspendLayout()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.edTb_dtulliqtri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulliq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_uffiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatiContabili, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatiContabili.SuspendLayout()
    CType(Me.edTb_escomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azdoppes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_escompp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azgestscad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_ventil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnFornitura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFornitura.SuspendLayout()
    CType(Me.edTb_pgullgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pravlgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_prdalgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_riullgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rgullgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtullgp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCondFornsx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCondFornsx.SuspendLayout()
    CType(Me.edTb_pgullg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_pravlg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_prdalg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_riullg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rgulel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rgullg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtullg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbApri, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbCalcolaCodFisc, Me.tlbRitornaCodFisc, Me.tlbEscomp, Me.tlbIva, Me.tlbDatiAggCont, Me.tlbAggRiclassif})
    Me.NtsBarManager1.MaxItemId = 51
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEscomp, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 5
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    'tlbEscomp
    '
    Me.tlbEscomp.Caption = "Dati esercizi contabili"
    Me.tlbEscomp.Glyph = CType(resources.GetObject("tlbEscomp.Glyph"), System.Drawing.Image)
    Me.tlbEscomp.GlyphPath = ""
    Me.tlbEscomp.Id = 28
    Me.tlbEscomp.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbEscomp.Name = "tlbEscomp"
    Me.tlbEscomp.Visible = True
    '
    'tlbIva
    '
    Me.tlbIva.Caption = "Dati IVA per anno"
    Me.tlbIva.Glyph = CType(resources.GetObject("tlbIva.Glyph"), System.Drawing.Image)
    Me.tlbIva.GlyphPath = ""
    Me.tlbIva.Id = 29
    Me.tlbIva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbIva.Name = "tlbIva"
    Me.tlbIva.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDatiAggCont, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCalcolaCodFisc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRitornaCodFisc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggRiclassif, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbDatiAggCont
    '
    Me.tlbDatiAggCont.Caption = "Dati aggiuntivi contabilità"
    Me.tlbDatiAggCont.GlyphPath = ""
    Me.tlbDatiAggCont.Id = 41
    Me.tlbDatiAggCont.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbDatiAggCont.Name = "tlbDatiAggCont"
    Me.tlbDatiAggCont.NTSIsCheckBox = False
    Me.tlbDatiAggCont.Visible = True
    '
    'tlbCalcolaCodFisc
    '
    Me.tlbCalcolaCodFisc.Caption = "Calcola codice fiscale"
    Me.tlbCalcolaCodFisc.GlyphPath = ""
    Me.tlbCalcolaCodFisc.Id = 26
    Me.tlbCalcolaCodFisc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbCalcolaCodFisc.Name = "tlbCalcolaCodFisc"
    Me.tlbCalcolaCodFisc.NTSIsCheckBox = False
    Me.tlbCalcolaCodFisc.Visible = True
    '
    'tlbRitornaCodFisc
    '
    Me.tlbRitornaCodFisc.Caption = "Ritorna codice fiscale"
    Me.tlbRitornaCodFisc.GlyphPath = ""
    Me.tlbRitornaCodFisc.Id = 27
    Me.tlbRitornaCodFisc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbRitornaCodFisc.Name = "tlbRitornaCodFisc"
    Me.tlbRitornaCodFisc.NTSIsCheckBox = False
    Me.tlbRitornaCodFisc.Visible = True
    '
    'tlbAggRiclassif
    '
    Me.tlbAggRiclassif.Caption = "Riaggiorna collegamenti riclassificazione"
    Me.tlbAggRiclassif.GlyphPath = ""
    Me.tlbAggRiclassif.Id = 50
    Me.tlbAggRiclassif.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbAggRiclassif.Name = "tlbAggRiclassif"
    Me.tlbAggRiclassif.NTSIsCheckBox = False
    Me.tlbAggRiclassif.Visible = True
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
    'lbXx_codditt
    '
    Me.lbXx_codditt.AutoSize = True
    Me.lbXx_codditt.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codditt.Location = New System.Drawing.Point(5, 6)
    Me.lbXx_codditt.Name = "lbXx_codditt"
    Me.lbXx_codditt.NTSDbField = ""
    Me.lbXx_codditt.Size = New System.Drawing.Size(39, 13)
    Me.lbXx_codditt.TabIndex = 10
    Me.lbXx_codditt.Text = "Codice"
    Me.lbXx_codditt.Tooltip = ""
    Me.lbXx_codditt.UseMnemonic = False
    '
    'lbTb_azrags1
    '
    Me.lbTb_azrags1.AutoSize = True
    Me.lbTb_azrags1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azrags1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_azrags1.Location = New System.Drawing.Point(5, 28)
    Me.lbTb_azrags1.Name = "lbTb_azrags1"
    Me.lbTb_azrags1.NTSDbField = ""
    Me.lbTb_azrags1.Size = New System.Drawing.Size(96, 13)
    Me.lbTb_azrags1.TabIndex = 11
    Me.lbTb_azrags1.Text = "Ragione Sociale"
    Me.lbTb_azrags1.Tooltip = ""
    Me.lbTb_azrags1.UseMnemonic = False
    '
    'edTb_azrags1
    '
    Me.edTb_azrags1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azrags1.Location = New System.Drawing.Point(103, 25)
    Me.edTb_azrags1.Name = "edTb_azrags1"
    Me.edTb_azrags1.NTSDbField = ""
    Me.edTb_azrags1.NTSForzaVisZoom = False
    Me.edTb_azrags1.NTSOldValue = ""
    Me.edTb_azrags1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azrags1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azrags1.Properties.AutoHeight = False
    Me.edTb_azrags1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azrags1.Properties.MaxLength = 65536
    Me.edTb_azrags1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azrags1.Size = New System.Drawing.Size(276, 20)
    Me.edTb_azrags1.TabIndex = 501
    '
    'lbTb_azpersfg
    '
    Me.lbTb_azpersfg.AutoSize = True
    Me.lbTb_azpersfg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azpersfg.Location = New System.Drawing.Point(389, 9)
    Me.lbTb_azpersfg.Name = "lbTb_azpersfg"
    Me.lbTb_azpersfg.NTSDbField = ""
    Me.lbTb_azpersfg.Size = New System.Drawing.Size(73, 13)
    Me.lbTb_azpersfg.TabIndex = 41
    Me.lbTb_azpersfg.Text = "Tipo soggetto"
    Me.lbTb_azpersfg.Tooltip = ""
    Me.lbTb_azpersfg.UseMnemonic = False
    '
    'cbTb_azpersfg
    '
    Me.cbTb_azpersfg.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azpersfg.DataSource = Nothing
    Me.cbTb_azpersfg.DisplayMember = ""
    Me.cbTb_azpersfg.Location = New System.Drawing.Point(494, 6)
    Me.cbTb_azpersfg.Name = "cbTb_azpersfg"
    Me.cbTb_azpersfg.NTSDbField = ""
    Me.cbTb_azpersfg.Properties.AutoHeight = False
    Me.cbTb_azpersfg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azpersfg.Properties.DropDownRows = 30
    Me.cbTb_azpersfg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azpersfg.SelectedValue = ""
    Me.cbTb_azpersfg.Size = New System.Drawing.Size(134, 20)
    Me.cbTb_azpersfg.TabIndex = 531
    Me.cbTb_azpersfg.ValueMember = ""
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdDelImmagine)
    Me.pnTop.Controls.Add(Me.lbInfo)
    Me.pnTop.Controls.Add(Me.edTb_imagename)
    Me.pnTop.Controls.Add(Me.lbTb_imagename)
    Me.pnTop.Controls.Add(Me.cmdAltriIndir)
    Me.pnTop.Controls.Add(Me.cmdSelImmagine)
    Me.pnTop.Controls.Add(Me.edTb_azrags2)
    Me.pnTop.Controls.Add(Me.edTb_azrags1)
    Me.pnTop.Controls.Add(Me.edXx_codditt)
    Me.pnTop.Controls.Add(Me.lbTb_azrags1)
    Me.pnTop.Controls.Add(Me.lbXx_codditt)
    Me.pnTop.Controls.Add(Me.cbTb_azpersfg)
    Me.pnTop.Controls.Add(Me.lbTb_azpersfg)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(774, 73)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbInfo
    '
    Me.lbInfo.AutoSize = True
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Location = New System.Drawing.Point(391, 50)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(72, 13)
    Me.lbInfo.TabIndex = 686
    Me.lbInfo.Text = "info immagine"
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'edTb_imagename
    '
    Me.edTb_imagename.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_imagename.Enabled = False
    Me.edTb_imagename.Location = New System.Drawing.Point(494, 27)
    Me.edTb_imagename.Name = "edTb_imagename"
    Me.edTb_imagename.NTSDbField = ""
    Me.edTb_imagename.NTSForzaVisZoom = False
    Me.edTb_imagename.NTSOldValue = ""
    Me.edTb_imagename.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_imagename.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_imagename.Properties.AutoHeight = False
    Me.edTb_imagename.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_imagename.Properties.MaxLength = 65536
    Me.edTb_imagename.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_imagename.Size = New System.Drawing.Size(210, 20)
    Me.edTb_imagename.TabIndex = 575
    '
    'lbTb_imagename
    '
    Me.lbTb_imagename.AutoSize = True
    Me.lbTb_imagename.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_imagename.Location = New System.Drawing.Point(390, 30)
    Me.lbTb_imagename.Name = "lbTb_imagename"
    Me.lbTb_imagename.NTSDbField = ""
    Me.lbTb_imagename.Size = New System.Drawing.Size(100, 13)
    Me.lbTb_imagename.TabIndex = 685
    Me.lbTb_imagename.Text = "Logo az. per report"
    Me.lbTb_imagename.Tooltip = ""
    Me.lbTb_imagename.UseMnemonic = False
    '
    'cmdAltriIndir
    '
    Me.cmdAltriIndir.ImagePath = ""
    Me.cmdAltriIndir.ImageText = ""
    Me.cmdAltriIndir.Location = New System.Drawing.Point(647, 3)
    Me.cmdAltriIndir.Name = "cmdAltriIndir"
    Me.cmdAltriIndir.NTSContextMenu = Nothing
    Me.cmdAltriIndir.Size = New System.Drawing.Size(114, 23)
    Me.cmdAltriIndir.TabIndex = 550
    Me.cmdAltriIndir.Text = "Altri &indirizzi"
    '
    'cmdSelImmagine
    '
    Me.cmdSelImmagine.ImagePath = ""
    Me.cmdSelImmagine.ImageText = ""
    Me.cmdSelImmagine.Location = New System.Drawing.Point(708, 27)
    Me.cmdSelImmagine.Name = "cmdSelImmagine"
    Me.cmdSelImmagine.NTSContextMenu = Nothing
    Me.cmdSelImmagine.Size = New System.Drawing.Size(25, 23)
    Me.cmdSelImmagine.TabIndex = 684
    Me.cmdSelImmagine.Text = "..."
    '
    'edTb_azrags2
    '
    Me.edTb_azrags2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azrags2.Location = New System.Drawing.Point(103, 47)
    Me.edTb_azrags2.Name = "edTb_azrags2"
    Me.edTb_azrags2.NTSDbField = ""
    Me.edTb_azrags2.NTSForzaVisZoom = False
    Me.edTb_azrags2.NTSOldValue = ""
    Me.edTb_azrags2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azrags2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azrags2.Properties.AutoHeight = False
    Me.edTb_azrags2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azrags2.Properties.MaxLength = 65536
    Me.edTb_azrags2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azrags2.Size = New System.Drawing.Size(276, 20)
    Me.edTb_azrags2.TabIndex = 668
    '
    'edXx_codditt
    '
    Me.edXx_codditt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_codditt.EditValue = ""
    Me.edXx_codditt.Enabled = False
    Me.edXx_codditt.Location = New System.Drawing.Point(103, 3)
    Me.edXx_codditt.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edXx_codditt.Name = "edXx_codditt"
    Me.edXx_codditt.NTSDbField = ""
    Me.edXx_codditt.NTSForzaVisZoom = False
    Me.edXx_codditt.NTSOldValue = ""
    Me.edXx_codditt.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edXx_codditt.Properties.Appearance.Options.UseBackColor = True
    Me.edXx_codditt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_codditt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_codditt.Properties.AutoHeight = False
    Me.edXx_codditt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_codditt.Properties.MaxLength = 65536
    Me.edXx_codditt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_codditt.Size = New System.Drawing.Size(228, 20)
    Me.edXx_codditt.TabIndex = 667
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsAnaz)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 103)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(774, 386)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel1"
    '
    'tsAnaz
    '
    Me.tsAnaz.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsAnaz.Location = New System.Drawing.Point(0, 0)
    Me.tsAnaz.Name = "tsAnaz"
    Me.tsAnaz.SelectedTabPage = Me.NtsTabPage1
    Me.tsAnaz.Size = New System.Drawing.Size(774, 386)
    Me.tsAnaz.TabIndex = 4
    Me.tsAnaz.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5})
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(765, 356)
    Me.NtsTabPage1.Text = "&1 - Generale"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.pnPag1Dx)
    Me.pnPag1.Controls.Add(Me.pnPag1Sx)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(765, 356)
    Me.pnPag1.TabIndex = 0
    Me.pnPag1.Text = "NtsPanel1"
    '
    'pnPag1Dx
    '
    Me.pnPag1Dx.AllowDrop = True
    Me.pnPag1Dx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Dx.Appearance.Options.UseBackColor = True
    Me.pnPag1Dx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azcell)
    Me.pnPag1Dx.Controls.Add(Me.edTb_azcell)
    Me.pnPag1Dx.Controls.Add(Me.ckTb_azomocodice)
    Me.pnPag1Dx.Controls.Add(Me.cbTb_azusaem)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azusaem)
    Me.pnPag1Dx.Controls.Add(Me.edTb_azemail)
    Me.pnPag1Dx.Controls.Add(Me.edTb_azpiva)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azfaxtlx)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azemail)
    Me.pnPag1Dx.Controls.Add(Me.edTb_azfaxtlx)
    Me.pnPag1Dx.Controls.Add(Me.edTb_aztelef)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azpiva)
    Me.pnPag1Dx.Controls.Add(Me.edTb_azcodf)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_azcodf)
    Me.pnPag1Dx.Controls.Add(Me.lbTb_aztelef)
    Me.pnPag1Dx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Dx.Location = New System.Drawing.Point(376, 0)
    Me.pnPag1Dx.Name = "pnPag1Dx"
    Me.pnPag1Dx.NTSActiveTrasparency = True
    Me.pnPag1Dx.Size = New System.Drawing.Size(403, 160)
    Me.pnPag1Dx.TabIndex = 574
    Me.pnPag1Dx.Text = "NtsPanel1"
    '
    'lbTb_azcell
    '
    Me.lbTb_azcell.AutoSize = True
    Me.lbTb_azcell.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcell.Location = New System.Drawing.Point(6, 72)
    Me.lbTb_azcell.Name = "lbTb_azcell"
    Me.lbTb_azcell.NTSDbField = ""
    Me.lbTb_azcell.Size = New System.Drawing.Size(48, 13)
    Me.lbTb_azcell.TabIndex = 589
    Me.lbTb_azcell.Text = "Cellulare"
    Me.lbTb_azcell.Tooltip = ""
    Me.lbTb_azcell.UseMnemonic = False
    '
    'edTb_azcell
    '
    Me.edTb_azcell.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcell.Location = New System.Drawing.Point(119, 69)
    Me.edTb_azcell.Name = "edTb_azcell"
    Me.edTb_azcell.NTSDbField = ""
    Me.edTb_azcell.NTSForzaVisZoom = False
    Me.edTb_azcell.NTSOldValue = ""
    Me.edTb_azcell.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcell.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcell.Properties.AutoHeight = False
    Me.edTb_azcell.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcell.Properties.MaxLength = 65536
    Me.edTb_azcell.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcell.Size = New System.Drawing.Size(184, 20)
    Me.edTb_azcell.TabIndex = 590
    '
    'ckTb_azomocodice
    '
    Me.ckTb_azomocodice.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_azomocodice.Location = New System.Drawing.Point(307, 4)
    Me.ckTb_azomocodice.Name = "ckTb_azomocodice"
    Me.ckTb_azomocodice.NTSCheckValue = "S"
    Me.ckTb_azomocodice.NTSUnCheckValue = "N"
    Me.ckTb_azomocodice.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_azomocodice.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_azomocodice.Properties.AutoHeight = False
    Me.ckTb_azomocodice.Properties.Caption = "Omocodice"
    Me.ckTb_azomocodice.Size = New System.Drawing.Size(75, 19)
    Me.ckTb_azomocodice.TabIndex = 588
    '
    'cbTb_azusaem
    '
    Me.cbTb_azusaem.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azusaem.DataSource = Nothing
    Me.cbTb_azusaem.DisplayMember = ""
    Me.cbTb_azusaem.Location = New System.Drawing.Point(119, 135)
    Me.cbTb_azusaem.Name = "cbTb_azusaem"
    Me.cbTb_azusaem.NTSDbField = ""
    Me.cbTb_azusaem.Properties.AutoHeight = False
    Me.cbTb_azusaem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azusaem.Properties.DropDownRows = 30
    Me.cbTb_azusaem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azusaem.SelectedValue = ""
    Me.cbTb_azusaem.Size = New System.Drawing.Size(184, 20)
    Me.cbTb_azusaem.TabIndex = 566
    Me.cbTb_azusaem.ValueMember = ""
    '
    'lbTb_azusaem
    '
    Me.lbTb_azusaem.AutoSize = True
    Me.lbTb_azusaem.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azusaem.Location = New System.Drawing.Point(3, 138)
    Me.lbTb_azusaem.Name = "lbTb_azusaem"
    Me.lbTb_azusaem.NTSDbField = ""
    Me.lbTb_azusaem.Size = New System.Drawing.Size(115, 13)
    Me.lbTb_azusaem.TabIndex = 543
    Me.lbTb_azusaem.Text = "Modalità di corrispond."
    Me.lbTb_azusaem.Tooltip = ""
    Me.lbTb_azusaem.UseMnemonic = False
    '
    'edTb_azemail
    '
    Me.edTb_azemail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azemail.Location = New System.Drawing.Point(119, 113)
    Me.edTb_azemail.Name = "edTb_azemail"
    Me.edTb_azemail.NTSDbField = ""
    Me.edTb_azemail.NTSForzaVisZoom = False
    Me.edTb_azemail.NTSOldValue = ""
    Me.edTb_azemail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azemail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azemail.Properties.AutoHeight = False
    Me.edTb_azemail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azemail.Properties.MaxLength = 65536
    Me.edTb_azemail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azemail.Size = New System.Drawing.Size(184, 20)
    Me.edTb_azemail.TabIndex = 564
    '
    'edTb_azpiva
    '
    Me.edTb_azpiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azpiva.Location = New System.Drawing.Point(119, 25)
    Me.edTb_azpiva.Name = "edTb_azpiva"
    Me.edTb_azpiva.NTSDbField = ""
    Me.edTb_azpiva.NTSForzaVisZoom = False
    Me.edTb_azpiva.NTSOldValue = ""
    Me.edTb_azpiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azpiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azpiva.Properties.AutoHeight = False
    Me.edTb_azpiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azpiva.Properties.MaxLength = 65536
    Me.edTb_azpiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azpiva.Size = New System.Drawing.Size(184, 20)
    Me.edTb_azpiva.TabIndex = 555
    '
    'lbTb_azfaxtlx
    '
    Me.lbTb_azfaxtlx.AutoSize = True
    Me.lbTb_azfaxtlx.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azfaxtlx.Location = New System.Drawing.Point(6, 94)
    Me.lbTb_azfaxtlx.Name = "lbTb_azfaxtlx"
    Me.lbTb_azfaxtlx.NTSDbField = ""
    Me.lbTb_azfaxtlx.Size = New System.Drawing.Size(25, 13)
    Me.lbTb_azfaxtlx.TabIndex = 534
    Me.lbTb_azfaxtlx.Text = "Fax"
    Me.lbTb_azfaxtlx.Tooltip = ""
    Me.lbTb_azfaxtlx.UseMnemonic = False
    '
    'lbTb_azemail
    '
    Me.lbTb_azemail.AutoSize = True
    Me.lbTb_azemail.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azemail.Location = New System.Drawing.Point(6, 116)
    Me.lbTb_azemail.Name = "lbTb_azemail"
    Me.lbTb_azemail.NTSDbField = ""
    Me.lbTb_azemail.Size = New System.Drawing.Size(35, 13)
    Me.lbTb_azemail.TabIndex = 541
    Me.lbTb_azemail.Text = "E-mail"
    Me.lbTb_azemail.Tooltip = ""
    Me.lbTb_azemail.UseMnemonic = False
    '
    'edTb_azfaxtlx
    '
    Me.edTb_azfaxtlx.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azfaxtlx.Location = New System.Drawing.Point(119, 91)
    Me.edTb_azfaxtlx.Name = "edTb_azfaxtlx"
    Me.edTb_azfaxtlx.NTSDbField = ""
    Me.edTb_azfaxtlx.NTSForzaVisZoom = False
    Me.edTb_azfaxtlx.NTSOldValue = ""
    Me.edTb_azfaxtlx.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azfaxtlx.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azfaxtlx.Properties.AutoHeight = False
    Me.edTb_azfaxtlx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azfaxtlx.Properties.MaxLength = 65536
    Me.edTb_azfaxtlx.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azfaxtlx.Size = New System.Drawing.Size(184, 20)
    Me.edTb_azfaxtlx.TabIndex = 557
    '
    'edTb_aztelef
    '
    Me.edTb_aztelef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aztelef.Location = New System.Drawing.Point(119, 47)
    Me.edTb_aztelef.Name = "edTb_aztelef"
    Me.edTb_aztelef.NTSDbField = ""
    Me.edTb_aztelef.NTSForzaVisZoom = False
    Me.edTb_aztelef.NTSOldValue = ""
    Me.edTb_aztelef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aztelef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aztelef.Properties.AutoHeight = False
    Me.edTb_aztelef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aztelef.Properties.MaxLength = 65536
    Me.edTb_aztelef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aztelef.Size = New System.Drawing.Size(184, 20)
    Me.edTb_aztelef.TabIndex = 556
    '
    'lbTb_azpiva
    '
    Me.lbTb_azpiva.AutoSize = True
    Me.lbTb_azpiva.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azpiva.Location = New System.Drawing.Point(6, 28)
    Me.lbTb_azpiva.Name = "lbTb_azpiva"
    Me.lbTb_azpiva.NTSDbField = ""
    Me.lbTb_azpiva.Size = New System.Drawing.Size(58, 13)
    Me.lbTb_azpiva.TabIndex = 532
    Me.lbTb_azpiva.Text = "Partita Iva"
    Me.lbTb_azpiva.Tooltip = ""
    Me.lbTb_azpiva.UseMnemonic = False
    '
    'edTb_azcodf
    '
    Me.edTb_azcodf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodf.Location = New System.Drawing.Point(119, 3)
    Me.edTb_azcodf.Name = "edTb_azcodf"
    Me.edTb_azcodf.NTSDbField = ""
    Me.edTb_azcodf.NTSForzaVisZoom = False
    Me.edTb_azcodf.NTSOldValue = ""
    Me.edTb_azcodf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodf.Properties.AutoHeight = False
    Me.edTb_azcodf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodf.Properties.MaxLength = 65536
    Me.edTb_azcodf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodf.Size = New System.Drawing.Size(184, 20)
    Me.edTb_azcodf.TabIndex = 554
    '
    'lbTb_azcodf
    '
    Me.lbTb_azcodf.AutoSize = True
    Me.lbTb_azcodf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodf.Location = New System.Drawing.Point(6, 6)
    Me.lbTb_azcodf.Name = "lbTb_azcodf"
    Me.lbTb_azcodf.NTSDbField = ""
    Me.lbTb_azcodf.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_azcodf.TabIndex = 531
    Me.lbTb_azcodf.Text = "Codice Fiscale"
    Me.lbTb_azcodf.Tooltip = ""
    Me.lbTb_azcodf.UseMnemonic = False
    '
    'lbTb_aztelef
    '
    Me.lbTb_aztelef.AutoSize = True
    Me.lbTb_aztelef.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aztelef.Location = New System.Drawing.Point(6, 50)
    Me.lbTb_aztelef.Name = "lbTb_aztelef"
    Me.lbTb_aztelef.NTSDbField = ""
    Me.lbTb_aztelef.Size = New System.Drawing.Size(49, 13)
    Me.lbTb_aztelef.TabIndex = 533
    Me.lbTb_aztelef.Text = "Telefono"
    Me.lbTb_aztelef.Tooltip = ""
    Me.lbTb_aztelef.UseMnemonic = False
    '
    'pnPag1Sx
    '
    Me.pnPag1Sx.AllowDrop = True
    Me.pnPag1Sx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Sx.Appearance.Options.UseBackColor = True
    Me.pnPag1Sx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azstatofed)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azstatofed)
    Me.pnPag1Sx.Controls.Add(Me.lbXx_azstato)
    Me.pnPag1Sx.Controls.Add(Me.lbXx_azcodcomu)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azcodcomu)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azcodcomu)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azcitta)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azcitta)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azindir)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azstato)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azcap)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azstato)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azindir)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azcap)
    Me.pnPag1Sx.Controls.Add(Me.edTb_azprov)
    Me.pnPag1Sx.Controls.Add(Me.lbTb_azprov)
    Me.pnPag1Sx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Sx.Location = New System.Drawing.Point(3, 0)
    Me.pnPag1Sx.Name = "pnPag1Sx"
    Me.pnPag1Sx.NTSActiveTrasparency = True
    Me.pnPag1Sx.Size = New System.Drawing.Size(373, 160)
    Me.pnPag1Sx.TabIndex = 573
    Me.pnPag1Sx.Text = "NtsPanel1"
    '
    'lbTb_azstatofed
    '
    Me.lbTb_azstatofed.AutoSize = True
    Me.lbTb_azstatofed.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azstatofed.Location = New System.Drawing.Point(6, 116)
    Me.lbTb_azstatofed.Name = "lbTb_azstatofed"
    Me.lbTb_azstatofed.NTSDbField = ""
    Me.lbTb_azstatofed.Size = New System.Drawing.Size(93, 13)
    Me.lbTb_azstatofed.TabIndex = 587
    Me.lbTb_azstatofed.Text = "Stato fed./contea"
    Me.lbTb_azstatofed.Tooltip = ""
    Me.lbTb_azstatofed.UseMnemonic = False
    '
    'edTb_azstatofed
    '
    Me.edTb_azstatofed.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azstatofed.Location = New System.Drawing.Point(114, 113)
    Me.edTb_azstatofed.Name = "edTb_azstatofed"
    Me.edTb_azstatofed.NTSDbField = ""
    Me.edTb_azstatofed.NTSForzaVisZoom = False
    Me.edTb_azstatofed.NTSOldValue = ""
    Me.edTb_azstatofed.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azstatofed.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azstatofed.Properties.AutoHeight = False
    Me.edTb_azstatofed.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azstatofed.Properties.MaxLength = 65536
    Me.edTb_azstatofed.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azstatofed.Size = New System.Drawing.Size(253, 20)
    Me.edTb_azstatofed.TabIndex = 588
    '
    'lbXx_azstato
    '
    Me.lbXx_azstato.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azstato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azstato.Location = New System.Drawing.Point(183, 91)
    Me.lbXx_azstato.Name = "lbXx_azstato"
    Me.lbXx_azstato.NTSDbField = ""
    Me.lbXx_azstato.Size = New System.Drawing.Size(184, 20)
    Me.lbXx_azstato.TabIndex = 580
    Me.lbXx_azstato.Tooltip = ""
    Me.lbXx_azstato.UseMnemonic = False
    '
    'lbXx_azcodcomu
    '
    Me.lbXx_azcodcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodcomu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodcomu.Location = New System.Drawing.Point(183, 25)
    Me.lbXx_azcodcomu.Name = "lbXx_azcodcomu"
    Me.lbXx_azcodcomu.NTSDbField = ""
    Me.lbXx_azcodcomu.Size = New System.Drawing.Size(184, 20)
    Me.lbXx_azcodcomu.TabIndex = 579
    Me.lbXx_azcodcomu.Tooltip = ""
    Me.lbXx_azcodcomu.UseMnemonic = False
    '
    'lbTb_azcodcomu
    '
    Me.lbTb_azcodcomu.AutoSize = True
    Me.lbTb_azcodcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodcomu.Location = New System.Drawing.Point(6, 28)
    Me.lbTb_azcodcomu.Name = "lbTb_azcodcomu"
    Me.lbTb_azcodcomu.NTSDbField = ""
    Me.lbTb_azcodcomu.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_azcodcomu.TabIndex = 577
    Me.lbTb_azcodcomu.Text = "Codice comune"
    Me.lbTb_azcodcomu.Tooltip = ""
    Me.lbTb_azcodcomu.UseMnemonic = False
    '
    'edTb_azcodcomu
    '
    Me.edTb_azcodcomu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodcomu.Location = New System.Drawing.Point(114, 25)
    Me.edTb_azcodcomu.Name = "edTb_azcodcomu"
    Me.edTb_azcodcomu.NTSDbField = ""
    Me.edTb_azcodcomu.NTSForzaVisZoom = False
    Me.edTb_azcodcomu.NTSOldValue = ""
    Me.edTb_azcodcomu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodcomu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodcomu.Properties.AutoHeight = False
    Me.edTb_azcodcomu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodcomu.Properties.MaxLength = 65536
    Me.edTb_azcodcomu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodcomu.Size = New System.Drawing.Size(63, 20)
    Me.edTb_azcodcomu.TabIndex = 578
    '
    'lbTb_azcitta
    '
    Me.lbTb_azcitta.AutoSize = True
    Me.lbTb_azcitta.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcitta.Location = New System.Drawing.Point(6, 50)
    Me.lbTb_azcitta.Name = "lbTb_azcitta"
    Me.lbTb_azcitta.NTSDbField = ""
    Me.lbTb_azcitta.Size = New System.Drawing.Size(67, 13)
    Me.lbTb_azcitta.TabIndex = 528
    Me.lbTb_azcitta.Text = "Citta/località"
    Me.lbTb_azcitta.Tooltip = ""
    Me.lbTb_azcitta.UseMnemonic = False
    '
    'edTb_azcitta
    '
    Me.edTb_azcitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcitta.Location = New System.Drawing.Point(114, 47)
    Me.edTb_azcitta.Name = "edTb_azcitta"
    Me.edTb_azcitta.NTSDbField = ""
    Me.edTb_azcitta.NTSForzaVisZoom = False
    Me.edTb_azcitta.NTSOldValue = ""
    Me.edTb_azcitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcitta.Properties.AutoHeight = False
    Me.edTb_azcitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcitta.Properties.MaxLength = 65536
    Me.edTb_azcitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcitta.Size = New System.Drawing.Size(253, 20)
    Me.edTb_azcitta.TabIndex = 551
    '
    'edTb_azindir
    '
    Me.edTb_azindir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azindir.Location = New System.Drawing.Point(114, 3)
    Me.edTb_azindir.Name = "edTb_azindir"
    Me.edTb_azindir.NTSDbField = ""
    Me.edTb_azindir.NTSForzaVisZoom = False
    Me.edTb_azindir.NTSOldValue = ""
    Me.edTb_azindir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azindir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azindir.Properties.AutoHeight = False
    Me.edTb_azindir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azindir.Properties.MaxLength = 65536
    Me.edTb_azindir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azindir.Size = New System.Drawing.Size(253, 20)
    Me.edTb_azindir.TabIndex = 549
    '
    'lbTb_azstato
    '
    Me.lbTb_azstato.AutoSize = True
    Me.lbTb_azstato.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azstato.Location = New System.Drawing.Point(6, 94)
    Me.lbTb_azstato.Name = "lbTb_azstato"
    Me.lbTb_azstato.NTSDbField = ""
    Me.lbTb_azstato.Size = New System.Drawing.Size(101, 13)
    Me.lbTb_azstato.TabIndex = 530
    Me.lbTb_azstato.Text = "Codice stato estero"
    Me.lbTb_azstato.Tooltip = ""
    Me.lbTb_azstato.UseMnemonic = False
    '
    'lbTb_azcap
    '
    Me.lbTb_azcap.AutoSize = True
    Me.lbTb_azcap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcap.Location = New System.Drawing.Point(6, 71)
    Me.lbTb_azcap.Name = "lbTb_azcap"
    Me.lbTb_azcap.NTSDbField = ""
    Me.lbTb_azcap.Size = New System.Drawing.Size(39, 13)
    Me.lbTb_azcap.TabIndex = 527
    Me.lbTb_azcap.Text = "C.A.P."
    Me.lbTb_azcap.Tooltip = ""
    Me.lbTb_azcap.UseMnemonic = False
    '
    'edTb_azstato
    '
    Me.edTb_azstato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azstato.Location = New System.Drawing.Point(114, 91)
    Me.edTb_azstato.Name = "edTb_azstato"
    Me.edTb_azstato.NTSDbField = ""
    Me.edTb_azstato.NTSForzaVisZoom = False
    Me.edTb_azstato.NTSOldValue = ""
    Me.edTb_azstato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azstato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azstato.Properties.AutoHeight = False
    Me.edTb_azstato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azstato.Properties.MaxLength = 65536
    Me.edTb_azstato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azstato.Size = New System.Drawing.Size(63, 20)
    Me.edTb_azstato.TabIndex = 553
    '
    'lbTb_azindir
    '
    Me.lbTb_azindir.AutoSize = True
    Me.lbTb_azindir.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azindir.Location = New System.Drawing.Point(6, 6)
    Me.lbTb_azindir.Name = "lbTb_azindir"
    Me.lbTb_azindir.NTSDbField = ""
    Me.lbTb_azindir.Size = New System.Drawing.Size(47, 13)
    Me.lbTb_azindir.TabIndex = 526
    Me.lbTb_azindir.Text = "Indirizzo"
    Me.lbTb_azindir.Tooltip = ""
    Me.lbTb_azindir.UseMnemonic = False
    '
    'edTb_azcap
    '
    Me.edTb_azcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcap.Location = New System.Drawing.Point(114, 69)
    Me.edTb_azcap.Name = "edTb_azcap"
    Me.edTb_azcap.NTSDbField = ""
    Me.edTb_azcap.NTSForzaVisZoom = False
    Me.edTb_azcap.NTSOldValue = ""
    Me.edTb_azcap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcap.Properties.AutoHeight = False
    Me.edTb_azcap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcap.Properties.MaxLength = 65536
    Me.edTb_azcap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcap.Size = New System.Drawing.Size(63, 20)
    Me.edTb_azcap.TabIndex = 550
    '
    'edTb_azprov
    '
    Me.edTb_azprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azprov.Location = New System.Drawing.Point(322, 69)
    Me.edTb_azprov.Name = "edTb_azprov"
    Me.edTb_azprov.NTSDbField = ""
    Me.edTb_azprov.NTSForzaVisZoom = False
    Me.edTb_azprov.NTSOldValue = ""
    Me.edTb_azprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azprov.Properties.AutoHeight = False
    Me.edTb_azprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azprov.Properties.MaxLength = 65536
    Me.edTb_azprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azprov.Size = New System.Drawing.Size(45, 20)
    Me.edTb_azprov.TabIndex = 552
    '
    'lbTb_azprov
    '
    Me.lbTb_azprov.AutoSize = True
    Me.lbTb_azprov.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azprov.Location = New System.Drawing.Point(254, 72)
    Me.lbTb_azprov.Name = "lbTb_azprov"
    Me.lbTb_azprov.NTSDbField = ""
    Me.lbTb_azprov.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_azprov.TabIndex = 529
    Me.lbTb_azprov.Text = "Provincia"
    Me.lbTb_azprov.Tooltip = ""
    Me.lbTb_azprov.UseMnemonic = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(765, 356)
    Me.NtsTabPage2.Text = "&2 - Persona Fisica/Giuridica"
    '
    'pnPag2
    '
    Me.pnPag2.AllowDrop = True
    Me.pnPag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2.Appearance.Options.UseBackColor = True
    Me.pnPag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2.Controls.Add(Me.fmPersfisica)
    Me.pnPag2.Controls.Add(Me.fmNascita)
    Me.pnPag2.Controls.Add(Me.ckTb_azcondom)
    Me.pnPag2.Controls.Add(Me.ckTb_azsoggresi)
    Me.pnPag2.Controls.Add(Me.ckTb_azprofes)
    Me.pnPag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag2.Location = New System.Drawing.Point(0, 0)
    Me.pnPag2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag2.Name = "pnPag2"
    Me.pnPag2.NTSActiveTrasparency = True
    Me.pnPag2.Size = New System.Drawing.Size(765, 356)
    Me.pnPag2.TabIndex = 0
    Me.pnPag2.Text = "NtsPanel1"
    '
    'fmPersfisica
    '
    Me.fmPersfisica.AllowDrop = True
    Me.fmPersfisica.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPersfisica.Appearance.Options.UseBackColor = True
    Me.fmPersfisica.Controls.Add(Me.edTb_aztitolo)
    Me.fmPersfisica.Controls.Add(Me.lbTb_aztitolo)
    Me.fmPersfisica.Controls.Add(Me.lbTb_sesso)
    Me.fmPersfisica.Controls.Add(Me.edTb_azcognome)
    Me.fmPersfisica.Controls.Add(Me.lbTb_azcognome)
    Me.fmPersfisica.Controls.Add(Me.edTb_aznome)
    Me.fmPersfisica.Controls.Add(Me.cbTb_sesso)
    Me.fmPersfisica.Controls.Add(Me.lbTb_aznome)
    Me.fmPersfisica.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPersfisica.Location = New System.Drawing.Point(3, 3)
    Me.fmPersfisica.Name = "fmPersfisica"
    Me.fmPersfisica.Size = New System.Drawing.Size(365, 94)
    Me.fmPersfisica.TabIndex = 600
    Me.fmPersfisica.Text = "Persona fisica"
    '
    'edTb_aztitolo
    '
    Me.edTb_aztitolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aztitolo.Location = New System.Drawing.Point(238, 66)
    Me.edTb_aztitolo.Name = "edTb_aztitolo"
    Me.edTb_aztitolo.NTSDbField = ""
    Me.edTb_aztitolo.NTSForzaVisZoom = False
    Me.edTb_aztitolo.NTSOldValue = ""
    Me.edTb_aztitolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aztitolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aztitolo.Properties.AutoHeight = False
    Me.edTb_aztitolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aztitolo.Properties.MaxLength = 65536
    Me.edTb_aztitolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aztitolo.Size = New System.Drawing.Size(120, 20)
    Me.edTb_aztitolo.TabIndex = 596
    '
    'lbTb_aztitolo
    '
    Me.lbTb_aztitolo.AutoSize = True
    Me.lbTb_aztitolo.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aztitolo.Location = New System.Drawing.Point(189, 69)
    Me.lbTb_aztitolo.Name = "lbTb_aztitolo"
    Me.lbTb_aztitolo.NTSDbField = ""
    Me.lbTb_aztitolo.Size = New System.Drawing.Size(33, 13)
    Me.lbTb_aztitolo.TabIndex = 595
    Me.lbTb_aztitolo.Text = "Titolo"
    Me.lbTb_aztitolo.Tooltip = ""
    Me.lbTb_aztitolo.UseMnemonic = False
    '
    'lbTb_sesso
    '
    Me.lbTb_sesso.AutoSize = True
    Me.lbTb_sesso.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_sesso.Location = New System.Drawing.Point(6, 69)
    Me.lbTb_sesso.Name = "lbTb_sesso"
    Me.lbTb_sesso.NTSDbField = ""
    Me.lbTb_sesso.Size = New System.Drawing.Size(35, 13)
    Me.lbTb_sesso.TabIndex = 594
    Me.lbTb_sesso.Text = "Sesso"
    Me.lbTb_sesso.Tooltip = ""
    Me.lbTb_sesso.UseMnemonic = False
    '
    'edTb_azcognome
    '
    Me.edTb_azcognome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcognome.Location = New System.Drawing.Point(69, 22)
    Me.edTb_azcognome.Name = "edTb_azcognome"
    Me.edTb_azcognome.NTSDbField = ""
    Me.edTb_azcognome.NTSForzaVisZoom = False
    Me.edTb_azcognome.NTSOldValue = ""
    Me.edTb_azcognome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcognome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcognome.Properties.AutoHeight = False
    Me.edTb_azcognome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcognome.Properties.MaxLength = 65536
    Me.edTb_azcognome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcognome.Size = New System.Drawing.Size(289, 20)
    Me.edTb_azcognome.TabIndex = 581
    '
    'lbTb_azcognome
    '
    Me.lbTb_azcognome.AutoSize = True
    Me.lbTb_azcognome.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcognome.Location = New System.Drawing.Point(6, 25)
    Me.lbTb_azcognome.Name = "lbTb_azcognome"
    Me.lbTb_azcognome.NTSDbField = ""
    Me.lbTb_azcognome.Size = New System.Drawing.Size(52, 13)
    Me.lbTb_azcognome.TabIndex = 562
    Me.lbTb_azcognome.Text = "Cognome"
    Me.lbTb_azcognome.Tooltip = ""
    Me.lbTb_azcognome.UseMnemonic = False
    '
    'edTb_aznome
    '
    Me.edTb_aznome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_aznome.Location = New System.Drawing.Point(69, 44)
    Me.edTb_aznome.Name = "edTb_aznome"
    Me.edTb_aznome.NTSDbField = ""
    Me.edTb_aznome.NTSForzaVisZoom = False
    Me.edTb_aznome.NTSOldValue = ""
    Me.edTb_aznome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_aznome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_aznome.Properties.AutoHeight = False
    Me.edTb_aznome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_aznome.Properties.MaxLength = 65536
    Me.edTb_aznome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_aznome.Size = New System.Drawing.Size(289, 20)
    Me.edTb_aznome.TabIndex = 582
    '
    'cbTb_sesso
    '
    Me.cbTb_sesso.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_sesso.DataSource = Nothing
    Me.cbTb_sesso.DisplayMember = ""
    Me.cbTb_sesso.Location = New System.Drawing.Point(69, 66)
    Me.cbTb_sesso.Name = "cbTb_sesso"
    Me.cbTb_sesso.NTSDbField = ""
    Me.cbTb_sesso.Properties.AutoHeight = False
    Me.cbTb_sesso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_sesso.Properties.DropDownRows = 30
    Me.cbTb_sesso.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_sesso.SelectedValue = ""
    Me.cbTb_sesso.Size = New System.Drawing.Size(100, 20)
    Me.cbTb_sesso.TabIndex = 593
    Me.cbTb_sesso.ValueMember = ""
    '
    'lbTb_aznome
    '
    Me.lbTb_aznome.AutoSize = True
    Me.lbTb_aznome.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aznome.Location = New System.Drawing.Point(6, 47)
    Me.lbTb_aznome.Name = "lbTb_aznome"
    Me.lbTb_aznome.NTSDbField = ""
    Me.lbTb_aznome.Size = New System.Drawing.Size(34, 13)
    Me.lbTb_aznome.TabIndex = 563
    Me.lbTb_aznome.Text = "Nome"
    Me.lbTb_aznome.Tooltip = ""
    Me.lbTb_aznome.UseMnemonic = False
    '
    'fmNascita
    '
    Me.fmNascita.AllowDrop = True
    Me.fmNascita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNascita.Appearance.Options.UseBackColor = True
    Me.fmNascita.Controls.Add(Me.lbXx_azstanasc)
    Me.fmNascita.Controls.Add(Me.lbXx_azcodcomn)
    Me.fmNascita.Controls.Add(Me.lbTb_datnasc)
    Me.fmNascita.Controls.Add(Me.edTb_datnasc)
    Me.fmNascita.Controls.Add(Me.lbTb_pronasc)
    Me.fmNascita.Controls.Add(Me.lbTb_locnasc)
    Me.fmNascita.Controls.Add(Me.edTb_pronasc)
    Me.fmNascita.Controls.Add(Me.edTb_locnasc)
    Me.fmNascita.Controls.Add(Me.lbTb_azcodcomn)
    Me.fmNascita.Controls.Add(Me.edTb_azcodcomn)
    Me.fmNascita.Controls.Add(Me.lbTb_azstanasc)
    Me.fmNascita.Controls.Add(Me.edTb_azstanasc)
    Me.fmNascita.Cursor = System.Windows.Forms.Cursors.Hand
    Me.fmNascita.Location = New System.Drawing.Point(3, 103)
    Me.fmNascita.Name = "fmNascita"
    Me.fmNascita.Size = New System.Drawing.Size(456, 135)
    Me.fmNascita.TabIndex = 596
    Me.fmNascita.Text = "Estremi nascita/costituzione"
    '
    'lbXx_azstanasc
    '
    Me.lbXx_azstanasc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azstanasc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azstanasc.Location = New System.Drawing.Point(171, 66)
    Me.lbXx_azstanasc.Name = "lbXx_azstanasc"
    Me.lbXx_azstanasc.NTSDbField = ""
    Me.lbXx_azstanasc.Size = New System.Drawing.Size(269, 20)
    Me.lbXx_azstanasc.TabIndex = 596
    Me.lbXx_azstanasc.Tooltip = ""
    Me.lbXx_azstanasc.UseMnemonic = False
    '
    'lbXx_azcodcomn
    '
    Me.lbXx_azcodcomn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodcomn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodcomn.Location = New System.Drawing.Point(171, 44)
    Me.lbXx_azcodcomn.Name = "lbXx_azcodcomn"
    Me.lbXx_azcodcomn.NTSDbField = ""
    Me.lbXx_azcodcomn.Size = New System.Drawing.Size(269, 20)
    Me.lbXx_azcodcomn.TabIndex = 595
    Me.lbXx_azcodcomn.Tooltip = ""
    Me.lbXx_azcodcomn.UseMnemonic = False
    '
    'lbTb_datnasc
    '
    Me.lbTb_datnasc.AutoSize = True
    Me.lbTb_datnasc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_datnasc.Location = New System.Drawing.Point(5, 25)
    Me.lbTb_datnasc.Name = "lbTb_datnasc"
    Me.lbTb_datnasc.NTSDbField = ""
    Me.lbTb_datnasc.Size = New System.Drawing.Size(30, 13)
    Me.lbTb_datnasc.TabIndex = 591
    Me.lbTb_datnasc.Text = "Data"
    Me.lbTb_datnasc.Tooltip = ""
    Me.lbTb_datnasc.UseMnemonic = False
    '
    'edTb_datnasc
    '
    Me.edTb_datnasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_datnasc.EditValue = "01/01/1900"
    Me.edTb_datnasc.Location = New System.Drawing.Point(102, 22)
    Me.edTb_datnasc.Name = "edTb_datnasc"
    Me.edTb_datnasc.NTSDbField = ""
    Me.edTb_datnasc.NTSForzaVisZoom = False
    Me.edTb_datnasc.NTSOldValue = ""
    Me.edTb_datnasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_datnasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_datnasc.Properties.AutoHeight = False
    Me.edTb_datnasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_datnasc.Properties.MaxLength = 65536
    Me.edTb_datnasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_datnasc.Size = New System.Drawing.Size(100, 20)
    Me.edTb_datnasc.TabIndex = 594
    '
    'lbTb_pronasc
    '
    Me.lbTb_pronasc.AutoSize = True
    Me.lbTb_pronasc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pronasc.Location = New System.Drawing.Point(5, 113)
    Me.lbTb_pronasc.Name = "lbTb_pronasc"
    Me.lbTb_pronasc.NTSDbField = ""
    Me.lbTb_pronasc.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_pronasc.TabIndex = 552
    Me.lbTb_pronasc.Text = "Provincia"
    Me.lbTb_pronasc.Tooltip = ""
    Me.lbTb_pronasc.UseMnemonic = False
    '
    'lbTb_locnasc
    '
    Me.lbTb_locnasc.AutoSize = True
    Me.lbTb_locnasc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_locnasc.Location = New System.Drawing.Point(5, 91)
    Me.lbTb_locnasc.Name = "lbTb_locnasc"
    Me.lbTb_locnasc.NTSDbField = ""
    Me.lbTb_locnasc.Size = New System.Drawing.Size(91, 13)
    Me.lbTb_locnasc.TabIndex = 592
    Me.lbTb_locnasc.Text = "Descr. città/stato"
    Me.lbTb_locnasc.Tooltip = ""
    Me.lbTb_locnasc.UseMnemonic = False
    '
    'edTb_pronasc
    '
    Me.edTb_pronasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pronasc.Location = New System.Drawing.Point(102, 110)
    Me.edTb_pronasc.Name = "edTb_pronasc"
    Me.edTb_pronasc.NTSDbField = ""
    Me.edTb_pronasc.NTSForzaVisZoom = False
    Me.edTb_pronasc.NTSOldValue = ""
    Me.edTb_pronasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_pronasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_pronasc.Properties.AutoHeight = False
    Me.edTb_pronasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pronasc.Properties.MaxLength = 65536
    Me.edTb_pronasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pronasc.Size = New System.Drawing.Size(63, 20)
    Me.edTb_pronasc.TabIndex = 571
    '
    'edTb_locnasc
    '
    Me.edTb_locnasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_locnasc.Location = New System.Drawing.Point(102, 88)
    Me.edTb_locnasc.Name = "edTb_locnasc"
    Me.edTb_locnasc.NTSDbField = ""
    Me.edTb_locnasc.NTSForzaVisZoom = False
    Me.edTb_locnasc.NTSOldValue = ""
    Me.edTb_locnasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_locnasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_locnasc.Properties.AutoHeight = False
    Me.edTb_locnasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_locnasc.Properties.MaxLength = 65536
    Me.edTb_locnasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_locnasc.Size = New System.Drawing.Size(338, 20)
    Me.edTb_locnasc.TabIndex = 595
    '
    'lbTb_azcodcomn
    '
    Me.lbTb_azcodcomn.AutoSize = True
    Me.lbTb_azcodcomn.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodcomn.Location = New System.Drawing.Point(5, 47)
    Me.lbTb_azcodcomn.Name = "lbTb_azcodcomn"
    Me.lbTb_azcodcomn.NTSDbField = ""
    Me.lbTb_azcodcomn.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_azcodcomn.TabIndex = 564
    Me.lbTb_azcodcomn.Text = "Codice comune"
    Me.lbTb_azcodcomn.Tooltip = ""
    Me.lbTb_azcodcomn.UseMnemonic = False
    '
    'edTb_azcodcomn
    '
    Me.edTb_azcodcomn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodcomn.Location = New System.Drawing.Point(102, 44)
    Me.edTb_azcodcomn.Name = "edTb_azcodcomn"
    Me.edTb_azcodcomn.NTSDbField = ""
    Me.edTb_azcodcomn.NTSForzaVisZoom = False
    Me.edTb_azcodcomn.NTSOldValue = ""
    Me.edTb_azcodcomn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodcomn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodcomn.Properties.AutoHeight = False
    Me.edTb_azcodcomn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodcomn.Properties.MaxLength = 65536
    Me.edTb_azcodcomn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodcomn.Size = New System.Drawing.Size(63, 20)
    Me.edTb_azcodcomn.TabIndex = 583
    '
    'lbTb_azstanasc
    '
    Me.lbTb_azstanasc.AutoSize = True
    Me.lbTb_azstanasc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azstanasc.Location = New System.Drawing.Point(5, 69)
    Me.lbTb_azstanasc.Name = "lbTb_azstanasc"
    Me.lbTb_azstanasc.NTSDbField = ""
    Me.lbTb_azstanasc.Size = New System.Drawing.Size(92, 13)
    Me.lbTb_azstanasc.TabIndex = 553
    Me.lbTb_azstanasc.Text = "Cod. stato estero"
    Me.lbTb_azstanasc.Tooltip = ""
    Me.lbTb_azstanasc.UseMnemonic = False
    '
    'edTb_azstanasc
    '
    Me.edTb_azstanasc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azstanasc.Location = New System.Drawing.Point(102, 66)
    Me.edTb_azstanasc.Name = "edTb_azstanasc"
    Me.edTb_azstanasc.NTSDbField = ""
    Me.edTb_azstanasc.NTSForzaVisZoom = False
    Me.edTb_azstanasc.NTSOldValue = ""
    Me.edTb_azstanasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azstanasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azstanasc.Properties.AutoHeight = False
    Me.edTb_azstanasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azstanasc.Properties.MaxLength = 65536
    Me.edTb_azstanasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azstanasc.Size = New System.Drawing.Size(63, 20)
    Me.edTb_azstanasc.TabIndex = 572
    '
    'ckTb_azcondom
    '
    Me.ckTb_azcondom.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_azcondom.Location = New System.Drawing.Point(211, 244)
    Me.ckTb_azcondom.Name = "ckTb_azcondom"
    Me.ckTb_azcondom.NTSCheckValue = "S"
    Me.ckTb_azcondom.NTSUnCheckValue = "N"
    Me.ckTb_azcondom.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_azcondom.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_azcondom.Properties.AutoHeight = False
    Me.ckTb_azcondom.Properties.Caption = "Condominio"
    Me.ckTb_azcondom.Size = New System.Drawing.Size(88, 19)
    Me.ckTb_azcondom.TabIndex = 590
    '
    'ckTb_azsoggresi
    '
    Me.ckTb_azsoggresi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_azsoggresi.Location = New System.Drawing.Point(12, 244)
    Me.ckTb_azsoggresi.Name = "ckTb_azsoggresi"
    Me.ckTb_azsoggresi.NTSCheckValue = "S"
    Me.ckTb_azsoggresi.NTSUnCheckValue = "N"
    Me.ckTb_azsoggresi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_azsoggresi.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_azsoggresi.Properties.AutoHeight = False
    Me.ckTb_azsoggresi.Properties.Caption = "Residente"
    Me.ckTb_azsoggresi.Size = New System.Drawing.Size(78, 19)
    Me.ckTb_azsoggresi.TabIndex = 548
    '
    'ckTb_azprofes
    '
    Me.ckTb_azprofes.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_azprofes.Location = New System.Drawing.Point(105, 244)
    Me.ckTb_azprofes.Name = "ckTb_azprofes"
    Me.ckTb_azprofes.NTSCheckValue = "S"
    Me.ckTb_azprofes.NTSUnCheckValue = "N"
    Me.ckTb_azprofes.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_azprofes.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_azprofes.Properties.AutoHeight = False
    Me.ckTb_azprofes.Properties.Caption = "Professionista"
    Me.ckTb_azprofes.Size = New System.Drawing.Size(100, 19)
    Me.ckTb_azprofes.TabIndex = 532
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(765, 356)
    Me.NtsTabPage3.Text = "&3 - Altri dati"
    '
    'pnPag3
    '
    Me.pnPag3.AllowDrop = True
    Me.pnPag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag3.Appearance.Options.UseBackColor = True
    Me.pnPag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag3.Controls.Add(Me.fmWeb)
    Me.pnPag3.Controls.Add(Me.pnAltriDatiSx)
    Me.pnPag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPag3.Location = New System.Drawing.Point(0, 0)
    Me.pnPag3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag3.Name = "pnPag3"
    Me.pnPag3.NTSActiveTrasparency = True
    Me.pnPag3.Size = New System.Drawing.Size(765, 356)
    Me.pnPag3.TabIndex = 566
    Me.pnPag3.Text = "NtsPanel1"
    '
    'fmWeb
    '
    Me.fmWeb.AllowDrop = True
    Me.fmWeb.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmWeb.Appearance.Options.UseBackColor = True
    Me.fmWeb.Controls.Add(Me.lbTb_latitud)
    Me.fmWeb.Controls.Add(Me.edTb_latitud)
    Me.fmWeb.Controls.Add(Me.lbTb_longitud)
    Me.fmWeb.Controls.Add(Me.edTb_longitud)
    Me.fmWeb.Controls.Add(Me.lbXx_nflogo)
    Me.fmWeb.Controls.Add(Me.edXx_nflogo)
    Me.fmWeb.Controls.Add(Me.lbTb_azwebpwd)
    Me.fmWeb.Controls.Add(Me.edTb_azwebpwd)
    Me.fmWeb.Controls.Add(Me.lbTb_azwebsite)
    Me.fmWeb.Controls.Add(Me.edTb_azwebsite)
    Me.fmWeb.Controls.Add(Me.lbTb_azwebuid)
    Me.fmWeb.Controls.Add(Me.edTb_azwebuid)
    Me.fmWeb.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmWeb.Location = New System.Drawing.Point(401, 3)
    Me.fmWeb.Name = "fmWeb"
    Me.fmWeb.Size = New System.Drawing.Size(360, 157)
    Me.fmWeb.TabIndex = 596
    Me.fmWeb.Text = "Sito Web / coordinate geografiche"
    '
    'lbTb_latitud
    '
    Me.lbTb_latitud.AutoSize = True
    Me.lbTb_latitud.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_latitud.Location = New System.Drawing.Point(6, 92)
    Me.lbTb_latitud.Name = "lbTb_latitud"
    Me.lbTb_latitud.NTSDbField = ""
    Me.lbTb_latitud.Size = New System.Drawing.Size(54, 13)
    Me.lbTb_latitud.TabIndex = 576
    Me.lbTb_latitud.Text = "Latitudine"
    Me.lbTb_latitud.Tooltip = ""
    Me.lbTb_latitud.UseMnemonic = False
    '
    'edTb_latitud
    '
    Me.edTb_latitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_latitud.Location = New System.Drawing.Point(114, 89)
    Me.edTb_latitud.Name = "edTb_latitud"
    Me.edTb_latitud.NTSDbField = ""
    Me.edTb_latitud.NTSForzaVisZoom = False
    Me.edTb_latitud.NTSOldValue = ""
    Me.edTb_latitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_latitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_latitud.Properties.AutoHeight = False
    Me.edTb_latitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_latitud.Properties.MaxLength = 65536
    Me.edTb_latitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_latitud.Size = New System.Drawing.Size(141, 20)
    Me.edTb_latitud.TabIndex = 578
    '
    'lbTb_longitud
    '
    Me.lbTb_longitud.AutoSize = True
    Me.lbTb_longitud.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_longitud.Location = New System.Drawing.Point(6, 113)
    Me.lbTb_longitud.Name = "lbTb_longitud"
    Me.lbTb_longitud.NTSDbField = ""
    Me.lbTb_longitud.Size = New System.Drawing.Size(62, 13)
    Me.lbTb_longitud.TabIndex = 577
    Me.lbTb_longitud.Text = "Longitudine"
    Me.lbTb_longitud.Tooltip = ""
    Me.lbTb_longitud.UseMnemonic = False
    '
    'edTb_longitud
    '
    Me.edTb_longitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_longitud.Location = New System.Drawing.Point(114, 111)
    Me.edTb_longitud.Name = "edTb_longitud"
    Me.edTb_longitud.NTSDbField = ""
    Me.edTb_longitud.NTSForzaVisZoom = False
    Me.edTb_longitud.NTSOldValue = ""
    Me.edTb_longitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_longitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_longitud.Properties.AutoHeight = False
    Me.edTb_longitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_longitud.Properties.MaxLength = 65536
    Me.edTb_longitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_longitud.Size = New System.Drawing.Size(141, 20)
    Me.edTb_longitud.TabIndex = 579
    '
    'lbXx_nflogo
    '
    Me.lbXx_nflogo.AutoSize = True
    Me.lbXx_nflogo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_nflogo.Location = New System.Drawing.Point(6, 135)
    Me.lbXx_nflogo.Name = "lbXx_nflogo"
    Me.lbXx_nflogo.NTSDbField = ""
    Me.lbXx_nflogo.Size = New System.Drawing.Size(102, 13)
    Me.lbXx_nflogo.TabIndex = 574
    Me.lbXx_nflogo.Text = "Logo Azienda (bmp)"
    Me.lbXx_nflogo.Tooltip = ""
    Me.lbXx_nflogo.UseMnemonic = False
    '
    'edXx_nflogo
    '
    Me.edXx_nflogo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edXx_nflogo.Location = New System.Drawing.Point(114, 133)
    Me.edXx_nflogo.Name = "edXx_nflogo"
    Me.edXx_nflogo.NTSDbField = ""
    Me.edXx_nflogo.NTSForzaVisZoom = False
    Me.edXx_nflogo.NTSOldValue = ""
    Me.edXx_nflogo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edXx_nflogo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edXx_nflogo.Properties.AutoHeight = False
    Me.edXx_nflogo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edXx_nflogo.Properties.MaxLength = 65536
    Me.edXx_nflogo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edXx_nflogo.Size = New System.Drawing.Size(240, 20)
    Me.edXx_nflogo.TabIndex = 575
    '
    'lbTb_azwebpwd
    '
    Me.lbTb_azwebpwd.AutoSize = True
    Me.lbTb_azwebpwd.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azwebpwd.Location = New System.Drawing.Point(6, 69)
    Me.lbTb_azwebpwd.Name = "lbTb_azwebpwd"
    Me.lbTb_azwebpwd.NTSDbField = ""
    Me.lbTb_azwebpwd.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_azwebpwd.TabIndex = 572
    Me.lbTb_azwebpwd.Text = "Pwd sito Web"
    Me.lbTb_azwebpwd.Tooltip = ""
    Me.lbTb_azwebpwd.UseMnemonic = False
    '
    'edTb_azwebpwd
    '
    Me.edTb_azwebpwd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azwebpwd.Location = New System.Drawing.Point(114, 67)
    Me.edTb_azwebpwd.Name = "edTb_azwebpwd"
    Me.edTb_azwebpwd.NTSDbField = ""
    Me.edTb_azwebpwd.NTSForzaVisZoom = False
    Me.edTb_azwebpwd.NTSOldValue = ""
    Me.edTb_azwebpwd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azwebpwd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azwebpwd.Properties.AutoHeight = False
    Me.edTb_azwebpwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azwebpwd.Properties.MaxLength = 65536
    Me.edTb_azwebpwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azwebpwd.Size = New System.Drawing.Size(240, 20)
    Me.edTb_azwebpwd.TabIndex = 573
    '
    'lbTb_azwebsite
    '
    Me.lbTb_azwebsite.AutoSize = True
    Me.lbTb_azwebsite.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azwebsite.Location = New System.Drawing.Point(6, 26)
    Me.lbTb_azwebsite.Name = "lbTb_azwebsite"
    Me.lbTb_azwebsite.NTSDbField = ""
    Me.lbTb_azwebsite.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_azwebsite.TabIndex = 568
    Me.lbTb_azwebsite.Text = "Sito Web"
    Me.lbTb_azwebsite.Tooltip = ""
    Me.lbTb_azwebsite.UseMnemonic = False
    '
    'edTb_azwebsite
    '
    Me.edTb_azwebsite.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azwebsite.Location = New System.Drawing.Point(114, 23)
    Me.edTb_azwebsite.Name = "edTb_azwebsite"
    Me.edTb_azwebsite.NTSDbField = ""
    Me.edTb_azwebsite.NTSForzaVisZoom = False
    Me.edTb_azwebsite.NTSOldValue = ""
    Me.edTb_azwebsite.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azwebsite.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azwebsite.Properties.AutoHeight = False
    Me.edTb_azwebsite.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azwebsite.Properties.MaxLength = 65536
    Me.edTb_azwebsite.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azwebsite.Size = New System.Drawing.Size(240, 20)
    Me.edTb_azwebsite.TabIndex = 570
    '
    'lbTb_azwebuid
    '
    Me.lbTb_azwebuid.AutoSize = True
    Me.lbTb_azwebuid.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azwebuid.Location = New System.Drawing.Point(6, 47)
    Me.lbTb_azwebuid.Name = "lbTb_azwebuid"
    Me.lbTb_azwebuid.NTSDbField = ""
    Me.lbTb_azwebuid.Size = New System.Drawing.Size(85, 13)
    Me.lbTb_azwebuid.TabIndex = 569
    Me.lbTb_azwebuid.Text = "UserID sito Web"
    Me.lbTb_azwebuid.Tooltip = ""
    Me.lbTb_azwebuid.UseMnemonic = False
    '
    'edTb_azwebuid
    '
    Me.edTb_azwebuid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azwebuid.Location = New System.Drawing.Point(114, 45)
    Me.edTb_azwebuid.Name = "edTb_azwebuid"
    Me.edTb_azwebuid.NTSDbField = ""
    Me.edTb_azwebuid.NTSForzaVisZoom = False
    Me.edTb_azwebuid.NTSOldValue = ""
    Me.edTb_azwebuid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azwebuid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azwebuid.Properties.AutoHeight = False
    Me.edTb_azwebuid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azwebuid.Properties.MaxLength = 65536
    Me.edTb_azwebuid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azwebuid.Size = New System.Drawing.Size(240, 20)
    Me.edTb_azwebuid.TabIndex = 571
    '
    'pnAltriDatiSx
    '
    Me.pnAltriDatiSx.AllowDrop = True
    Me.pnAltriDatiSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAltriDatiSx.Appearance.Options.UseBackColor = True
    Me.pnAltriDatiSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAltriDatiSx.Controls.Add(Me.lbTb_dtulst)
    Me.pnAltriDatiSx.Controls.Add(Me.fmBanca)
    Me.pnAltriDatiSx.Controls.Add(Me.lbXx_azcodrtac)
    Me.pnAltriDatiSx.Controls.Add(Me.edTb_dtulst)
    Me.pnAltriDatiSx.Controls.Add(Me.edTb_azcodrtac)
    Me.pnAltriDatiSx.Controls.Add(Me.lbTb_dtulap)
    Me.pnAltriDatiSx.Controls.Add(Me.lbTb_azcodrtac)
    Me.pnAltriDatiSx.Controls.Add(Me.edTb_dtulap)
    Me.pnAltriDatiSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAltriDatiSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnAltriDatiSx.Location = New System.Drawing.Point(0, 0)
    Me.pnAltriDatiSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAltriDatiSx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAltriDatiSx.Name = "pnAltriDatiSx"
    Me.pnAltriDatiSx.NTSActiveTrasparency = True
    Me.pnAltriDatiSx.Size = New System.Drawing.Size(399, 356)
    Me.pnAltriDatiSx.TabIndex = 678
    Me.pnAltriDatiSx.Text = "NtsPanel1"
    '
    'lbTb_dtulst
    '
    Me.lbTb_dtulst.AutoSize = True
    Me.lbTb_dtulst.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulst.Location = New System.Drawing.Point(9, 191)
    Me.lbTb_dtulst.Name = "lbTb_dtulst"
    Me.lbTb_dtulst.NTSDbField = ""
    Me.lbTb_dtulst.Size = New System.Drawing.Size(242, 13)
    Me.lbTb_dtulst.TabIndex = 681
    Me.lbTb_dtulst.Text = "Data ultima cancellazione movimenti di magazzino"
    Me.lbTb_dtulst.Tooltip = ""
    Me.lbTb_dtulst.UseMnemonic = False
    '
    'fmBanca
    '
    Me.fmBanca.AllowDrop = True
    Me.fmBanca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmBanca.Appearance.Options.UseBackColor = True
    Me.fmBanca.Controls.Add(Me.lbXx_azcodcaf)
    Me.fmBanca.Controls.Add(Me.lbTb_azcodcaf)
    Me.fmBanca.Controls.Add(Me.edTb_azcodcaf)
    Me.fmBanca.Controls.Add(Me.lbXx_azcodcab)
    Me.fmBanca.Controls.Add(Me.lbXx_azcodabi)
    Me.fmBanca.Controls.Add(Me.lbTb_azcodabi)
    Me.fmBanca.Controls.Add(Me.lbTb_azcodcc)
    Me.fmBanca.Controls.Add(Me.lbTb_azrifriba)
    Me.fmBanca.Controls.Add(Me.edTb_azcodcc)
    Me.fmBanca.Controls.Add(Me.edTb_azrifriba)
    Me.fmBanca.Controls.Add(Me.edTb_azcodcab)
    Me.fmBanca.Controls.Add(Me.edTb_azcodabi)
    Me.fmBanca.Controls.Add(Me.lbTb_azcodcab)
    Me.fmBanca.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmBanca.Location = New System.Drawing.Point(3, 3)
    Me.fmBanca.Name = "fmBanca"
    Me.fmBanca.Size = New System.Drawing.Size(392, 137)
    Me.fmBanca.TabIndex = 677
    Me.fmBanca.Text = "Dati banca ricorrente"
    '
    'lbXx_azcodcaf
    '
    Me.lbXx_azcodcaf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodcaf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodcaf.Location = New System.Drawing.Point(163, 110)
    Me.lbXx_azcodcaf.Name = "lbXx_azcodcaf"
    Me.lbXx_azcodcaf.NTSDbField = ""
    Me.lbXx_azcodcaf.Size = New System.Drawing.Size(225, 20)
    Me.lbXx_azcodcaf.TabIndex = 674
    Me.lbXx_azcodcaf.Tooltip = ""
    Me.lbXx_azcodcaf.UseMnemonic = False
    '
    'lbTb_azcodcaf
    '
    Me.lbTb_azcodcaf.AutoSize = True
    Me.lbTb_azcodcaf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodcaf.Location = New System.Drawing.Point(5, 113)
    Me.lbTb_azcodcaf.Name = "lbTb_azcodcaf"
    Me.lbTb_azcodcaf.NTSDbField = ""
    Me.lbTb_azcodcaf.Size = New System.Drawing.Size(71, 13)
    Me.lbTb_azcodcaf.TabIndex = 672
    Me.lbTb_azcodcaf.Text = "Codice Banca"
    Me.lbTb_azcodcaf.Tooltip = ""
    Me.lbTb_azcodcaf.UseMnemonic = False
    '
    'edTb_azcodcaf
    '
    Me.edTb_azcodcaf.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_azcodcaf.Location = New System.Drawing.Point(95, 110)
    Me.edTb_azcodcaf.Name = "edTb_azcodcaf"
    Me.edTb_azcodcaf.NTSDbField = ""
    Me.edTb_azcodcaf.NTSFormat = "0"
    Me.edTb_azcodcaf.NTSForzaVisZoom = False
    Me.edTb_azcodcaf.NTSOldValue = ""
    Me.edTb_azcodcaf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_azcodcaf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_azcodcaf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_azcodcaf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_azcodcaf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodcaf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodcaf.Properties.AutoHeight = False
    Me.edTb_azcodcaf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodcaf.Properties.MaxLength = 65536
    Me.edTb_azcodcaf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodcaf.Size = New System.Drawing.Size(62, 20)
    Me.edTb_azcodcaf.TabIndex = 673
    '
    'lbXx_azcodcab
    '
    Me.lbXx_azcodcab.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodcab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodcab.Location = New System.Drawing.Point(163, 44)
    Me.lbXx_azcodcab.Name = "lbXx_azcodcab"
    Me.lbXx_azcodcab.NTSDbField = ""
    Me.lbXx_azcodcab.Size = New System.Drawing.Size(226, 20)
    Me.lbXx_azcodcab.TabIndex = 668
    Me.lbXx_azcodcab.Tooltip = ""
    Me.lbXx_azcodcab.UseMnemonic = False
    '
    'lbXx_azcodabi
    '
    Me.lbXx_azcodabi.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodabi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodabi.Location = New System.Drawing.Point(163, 22)
    Me.lbXx_azcodabi.Name = "lbXx_azcodabi"
    Me.lbXx_azcodabi.NTSDbField = ""
    Me.lbXx_azcodabi.Size = New System.Drawing.Size(225, 20)
    Me.lbXx_azcodabi.TabIndex = 667
    Me.lbXx_azcodabi.Tooltip = ""
    Me.lbXx_azcodabi.UseMnemonic = False
    '
    'lbTb_azcodabi
    '
    Me.lbTb_azcodabi.AutoSize = True
    Me.lbTb_azcodabi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodabi.Location = New System.Drawing.Point(6, 25)
    Me.lbTb_azcodabi.Name = "lbTb_azcodabi"
    Me.lbTb_azcodabi.NTSDbField = ""
    Me.lbTb_azcodabi.Size = New System.Drawing.Size(54, 13)
    Me.lbTb_azcodabi.TabIndex = 5
    Me.lbTb_azcodabi.Text = "Abi Banca"
    Me.lbTb_azcodabi.Tooltip = ""
    Me.lbTb_azcodabi.UseMnemonic = False
    '
    'lbTb_azcodcc
    '
    Me.lbTb_azcodcc.AutoSize = True
    Me.lbTb_azcodcc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodcc.Location = New System.Drawing.Point(5, 69)
    Me.lbTb_azcodcc.Name = "lbTb_azcodcc"
    Me.lbTb_azcodcc.NTSDbField = ""
    Me.lbTb_azcodcc.Size = New System.Drawing.Size(58, 13)
    Me.lbTb_azcodcc.TabIndex = 649
    Me.lbTb_azcodcc.Text = "N° C/Corr."
    Me.lbTb_azcodcc.Tooltip = ""
    Me.lbTb_azcodcc.UseMnemonic = False
    '
    'lbTb_azrifriba
    '
    Me.lbTb_azrifriba.AutoSize = True
    Me.lbTb_azrifriba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azrifriba.Location = New System.Drawing.Point(5, 91)
    Me.lbTb_azrifriba.Name = "lbTb_azrifriba"
    Me.lbTb_azrifriba.NTSDbField = ""
    Me.lbTb_azrifriba.Size = New System.Drawing.Size(59, 13)
    Me.lbTb_azrifriba.TabIndex = 666
    Me.lbTb_azrifriba.Text = "Codice SIA"
    Me.lbTb_azrifriba.Tooltip = ""
    Me.lbTb_azrifriba.UseMnemonic = False
    '
    'edTb_azcodcc
    '
    Me.edTb_azcodcc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodcc.Location = New System.Drawing.Point(95, 66)
    Me.edTb_azcodcc.Name = "edTb_azcodcc"
    Me.edTb_azcodcc.NTSDbField = ""
    Me.edTb_azcodcc.NTSForzaVisZoom = False
    Me.edTb_azcodcc.NTSOldValue = ""
    Me.edTb_azcodcc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodcc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodcc.Properties.AutoHeight = False
    Me.edTb_azcodcc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodcc.Properties.MaxLength = 65536
    Me.edTb_azcodcc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodcc.Size = New System.Drawing.Size(294, 20)
    Me.edTb_azcodcc.TabIndex = 648
    '
    'edTb_azrifriba
    '
    Me.edTb_azrifriba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azrifriba.Location = New System.Drawing.Point(95, 88)
    Me.edTb_azrifriba.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_azrifriba.Name = "edTb_azrifriba"
    Me.edTb_azrifriba.NTSDbField = ""
    Me.edTb_azrifriba.NTSForzaVisZoom = False
    Me.edTb_azrifriba.NTSOldValue = ""
    Me.edTb_azrifriba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_azrifriba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_azrifriba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azrifriba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azrifriba.Properties.AutoHeight = False
    Me.edTb_azrifriba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azrifriba.Properties.MaxLength = 65536
    Me.edTb_azrifriba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azrifriba.Size = New System.Drawing.Size(294, 20)
    Me.edTb_azrifriba.TabIndex = 665
    '
    'edTb_azcodcab
    '
    Me.edTb_azcodcab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodcab.Location = New System.Drawing.Point(95, 44)
    Me.edTb_azcodcab.Name = "edTb_azcodcab"
    Me.edTb_azcodcab.NTSDbField = ""
    Me.edTb_azcodcab.NTSFormat = "0"
    Me.edTb_azcodcab.NTSForzaVisZoom = False
    Me.edTb_azcodcab.NTSOldValue = ""
    Me.edTb_azcodcab.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_azcodcab.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_azcodcab.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_azcodcab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_azcodcab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodcab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodcab.Properties.AutoHeight = False
    Me.edTb_azcodcab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodcab.Properties.MaxLength = 65536
    Me.edTb_azcodcab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodcab.Size = New System.Drawing.Size(62, 20)
    Me.edTb_azcodcab.TabIndex = 48
    '
    'edTb_azcodabi
    '
    Me.edTb_azcodabi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodabi.Location = New System.Drawing.Point(95, 22)
    Me.edTb_azcodabi.Name = "edTb_azcodabi"
    Me.edTb_azcodabi.NTSDbField = ""
    Me.edTb_azcodabi.NTSFormat = "0"
    Me.edTb_azcodabi.NTSForzaVisZoom = False
    Me.edTb_azcodabi.NTSOldValue = ""
    Me.edTb_azcodabi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_azcodabi.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_azcodabi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_azcodabi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_azcodabi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodabi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodabi.Properties.AutoHeight = False
    Me.edTb_azcodabi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodabi.Properties.MaxLength = 65536
    Me.edTb_azcodabi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodabi.Size = New System.Drawing.Size(62, 20)
    Me.edTb_azcodabi.TabIndex = 47
    '
    'lbTb_azcodcab
    '
    Me.lbTb_azcodcab.AutoSize = True
    Me.lbTb_azcodcab.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodcab.Location = New System.Drawing.Point(5, 47)
    Me.lbTb_azcodcab.Name = "lbTb_azcodcab"
    Me.lbTb_azcodcab.NTSDbField = ""
    Me.lbTb_azcodcab.Size = New System.Drawing.Size(55, 13)
    Me.lbTb_azcodcab.TabIndex = 6
    Me.lbTb_azcodcab.Text = "Cab Filiale"
    Me.lbTb_azcodcab.Tooltip = ""
    Me.lbTb_azcodcab.UseMnemonic = False
    '
    'lbXx_azcodrtac
    '
    Me.lbXx_azcodrtac.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_azcodrtac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_azcodrtac.Location = New System.Drawing.Point(165, 143)
    Me.lbXx_azcodrtac.Name = "lbXx_azcodrtac"
    Me.lbXx_azcodrtac.NTSDbField = ""
    Me.lbXx_azcodrtac.Size = New System.Drawing.Size(226, 20)
    Me.lbXx_azcodrtac.TabIndex = 649
    Me.lbXx_azcodrtac.Tooltip = ""
    Me.lbXx_azcodrtac.UseMnemonic = False
    '
    'edTb_dtulst
    '
    Me.edTb_dtulst.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulst.EditValue = "01/01/1900"
    Me.edTb_dtulst.Location = New System.Drawing.Point(291, 188)
    Me.edTb_dtulst.Name = "edTb_dtulst"
    Me.edTb_dtulst.NTSDbField = ""
    Me.edTb_dtulst.NTSForzaVisZoom = False
    Me.edTb_dtulst.NTSOldValue = ""
    Me.edTb_dtulst.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulst.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulst.Properties.AutoHeight = False
    Me.edTb_dtulst.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulst.Properties.MaxLength = 65536
    Me.edTb_dtulst.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulst.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulst.TabIndex = 682
    '
    'edTb_azcodrtac
    '
    Me.edTb_azcodrtac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodrtac.EditValue = "0"
    Me.edTb_azcodrtac.Location = New System.Drawing.Point(98, 143)
    Me.edTb_azcodrtac.Name = "edTb_azcodrtac"
    Me.edTb_azcodrtac.NTSDbField = ""
    Me.edTb_azcodrtac.NTSFormat = "0"
    Me.edTb_azcodrtac.NTSForzaVisZoom = False
    Me.edTb_azcodrtac.NTSOldValue = ""
    Me.edTb_azcodrtac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_azcodrtac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_azcodrtac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodrtac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodrtac.Properties.AutoHeight = False
    Me.edTb_azcodrtac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodrtac.Properties.MaxLength = 65536
    Me.edTb_azcodrtac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodrtac.Size = New System.Drawing.Size(53, 20)
    Me.edTb_azcodrtac.TabIndex = 650
    '
    'lbTb_dtulap
    '
    Me.lbTb_dtulap.AutoSize = True
    Me.lbTb_dtulap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulap.Location = New System.Drawing.Point(8, 169)
    Me.lbTb_dtulap.Name = "lbTb_dtulap"
    Me.lbTb_dtulap.NTSDbField = ""
    Me.lbTb_dtulap.Size = New System.Drawing.Size(255, 13)
    Me.lbTb_dtulap.TabIndex = 679
    Me.lbTb_dtulap.Text = "Data ultimo aggiornamento progressivi di magazzino"
    Me.lbTb_dtulap.Tooltip = ""
    Me.lbTb_dtulap.UseMnemonic = False
    '
    'lbTb_azcodrtac
    '
    Me.lbTb_azcodrtac.AutoSize = True
    Me.lbTb_azcodrtac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodrtac.Location = New System.Drawing.Point(8, 146)
    Me.lbTb_azcodrtac.Name = "lbTb_azcodrtac"
    Me.lbTb_azcodrtac.NTSDbField = ""
    Me.lbTb_azcodrtac.Size = New System.Drawing.Size(82, 13)
    Me.lbTb_azcodrtac.TabIndex = 647
    Me.lbTb_azcodrtac.Text = "TP ass. rit. acc."
    Me.lbTb_azcodrtac.Tooltip = ""
    Me.lbTb_azcodrtac.UseMnemonic = False
    '
    'edTb_dtulap
    '
    Me.edTb_dtulap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulap.EditValue = "01/01/1900"
    Me.edTb_dtulap.Location = New System.Drawing.Point(291, 166)
    Me.edTb_dtulap.Name = "edTb_dtulap"
    Me.edTb_dtulap.NTSDbField = ""
    Me.edTb_dtulap.NTSForzaVisZoom = False
    Me.edTb_dtulap.NTSOldValue = ""
    Me.edTb_dtulap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulap.Properties.AutoHeight = False
    Me.edTb_dtulap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulap.Properties.MaxLength = 65536
    Me.edTb_dtulap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulap.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulap.TabIndex = 680
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnDatiContabili)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(765, 356)
    Me.NtsTabPage4.Text = "&4 - Dati Contabili/Iva"
    '
    'pnDatiContabili
    '
    Me.pnDatiContabili.AllowDrop = True
    Me.pnDatiContabili.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDatiContabili.Appearance.Options.UseBackColor = True
    Me.pnDatiContabili.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDatiContabili.Controls.Add(Me.NtsGroupBox1)
    Me.pnDatiContabili.Controls.Add(Me.fmDatiContabili)
    Me.pnDatiContabili.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDatiContabili.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDatiContabili.Location = New System.Drawing.Point(0, 0)
    Me.pnDatiContabili.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnDatiContabili.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnDatiContabili.Name = "pnDatiContabili"
    Me.pnDatiContabili.NTSActiveTrasparency = True
    Me.pnDatiContabili.Size = New System.Drawing.Size(765, 356)
    Me.pnDatiContabili.TabIndex = 1
    Me.pnDatiContabili.Text = "NtsPanel4"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.edTb_dtulliqtri)
    Me.NtsGroupBox1.Controls.Add(Me.edTb_dtulliq)
    Me.NtsGroupBox1.Controls.Add(Me.lbTb_dtulliq)
    Me.NtsGroupBox1.Controls.Add(Me.edTb_uffiva)
    Me.NtsGroupBox1.Controls.Add(Me.lbTb_uffiva)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(3, 144)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(466, 69)
    Me.NtsGroupBox1.TabIndex = 679
    Me.NtsGroupBox1.Text = "DATI IVA"
    '
    'edTb_dtulliqtri
    '
    Me.edTb_dtulliqtri.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulliqtri.EditValue = "01/01/1900"
    Me.edTb_dtulliqtri.Location = New System.Drawing.Point(344, 45)
    Me.edTb_dtulliqtri.Name = "edTb_dtulliqtri"
    Me.edTb_dtulliqtri.NTSDbField = ""
    Me.edTb_dtulliqtri.NTSForzaVisZoom = False
    Me.edTb_dtulliqtri.NTSOldValue = ""
    Me.edTb_dtulliqtri.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulliqtri.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulliqtri.Properties.AutoHeight = False
    Me.edTb_dtulliqtri.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulliqtri.Properties.MaxLength = 65536
    Me.edTb_dtulliqtri.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulliqtri.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulliqtri.TabIndex = 688
    '
    'edTb_dtulliq
    '
    Me.edTb_dtulliq.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulliq.EditValue = "01/01/1900"
    Me.edTb_dtulliq.Location = New System.Drawing.Point(235, 44)
    Me.edTb_dtulliq.Name = "edTb_dtulliq"
    Me.edTb_dtulliq.NTSDbField = ""
    Me.edTb_dtulliq.NTSForzaVisZoom = False
    Me.edTb_dtulliq.NTSOldValue = ""
    Me.edTb_dtulliq.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulliq.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulliq.Properties.AutoHeight = False
    Me.edTb_dtulliq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulliq.Properties.MaxLength = 65536
    Me.edTb_dtulliq.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulliq.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulliq.TabIndex = 687
    '
    'lbTb_dtulliq
    '
    Me.lbTb_dtulliq.AutoSize = True
    Me.lbTb_dtulliq.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulliq.Location = New System.Drawing.Point(9, 47)
    Me.lbTb_dtulliq.Name = "lbTb_dtulliq"
    Me.lbTb_dtulliq.NTSDbField = ""
    Me.lbTb_dtulliq.Size = New System.Drawing.Size(220, 13)
    Me.lbTb_dtulliq.TabIndex = 686
    Me.lbTb_dtulliq.Text = "Data ultima liquidazione (mensile/trimestrale)"
    Me.lbTb_dtulliq.Tooltip = ""
    Me.lbTb_dtulliq.UseMnemonic = False
    '
    'edTb_uffiva
    '
    Me.edTb_uffiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_uffiva.Location = New System.Drawing.Point(235, 22)
    Me.edTb_uffiva.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_uffiva.Name = "edTb_uffiva"
    Me.edTb_uffiva.NTSDbField = ""
    Me.edTb_uffiva.NTSForzaVisZoom = False
    Me.edTb_uffiva.NTSOldValue = ""
    Me.edTb_uffiva.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_uffiva.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_uffiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_uffiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_uffiva.Properties.AutoHeight = False
    Me.edTb_uffiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_uffiva.Properties.MaxLength = 65536
    Me.edTb_uffiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_uffiva.Size = New System.Drawing.Size(209, 20)
    Me.edTb_uffiva.TabIndex = 685
    '
    'lbTb_uffiva
    '
    Me.lbTb_uffiva.AutoSize = True
    Me.lbTb_uffiva.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_uffiva.Location = New System.Drawing.Point(9, 25)
    Me.lbTb_uffiva.Name = "lbTb_uffiva"
    Me.lbTb_uffiva.NTSDbField = ""
    Me.lbTb_uffiva.Size = New System.Drawing.Size(116, 13)
    Me.lbTb_uffiva.TabIndex = 684
    Me.lbTb_uffiva.Text = "Ufficio Iva competente"
    Me.lbTb_uffiva.Tooltip = ""
    Me.lbTb_uffiva.UseMnemonic = False
    '
    'fmDatiContabili
    '
    Me.fmDatiContabili.AllowDrop = True
    Me.fmDatiContabili.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatiContabili.Appearance.Options.UseBackColor = True
    Me.fmDatiContabili.Controls.Add(Me.edTb_escomp)
    Me.fmDatiContabili.Controls.Add(Me.cbTb_azdoppes)
    Me.fmDatiContabili.Controls.Add(Me.edTb_escompp)
    Me.fmDatiContabili.Controls.Add(Me.lbTb_azdoppes)
    Me.fmDatiContabili.Controls.Add(Me.lbXx_escomp)
    Me.fmDatiContabili.Controls.Add(Me.cbTb_azgestscad)
    Me.fmDatiContabili.Controls.Add(Me.lbTb_escomp)
    Me.fmDatiContabili.Controls.Add(Me.cbTb_ventil)
    Me.fmDatiContabili.Controls.Add(Me.lbXx_escompp)
    Me.fmDatiContabili.Controls.Add(Me.lbTb_azgestscad)
    Me.fmDatiContabili.Controls.Add(Me.lbTb_escompp)
    Me.fmDatiContabili.Controls.Add(Me.lbTb_ventil)
    Me.fmDatiContabili.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatiContabili.Location = New System.Drawing.Point(3, 3)
    Me.fmDatiContabili.Name = "fmDatiContabili"
    Me.fmDatiContabili.Size = New System.Drawing.Size(698, 135)
    Me.fmDatiContabili.TabIndex = 678
    Me.fmDatiContabili.Text = "DATI CONTABILI"
    '
    'edTb_escomp
    '
    Me.edTb_escomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_escomp.EditValue = "0"
    Me.edTb_escomp.Location = New System.Drawing.Point(235, 23)
    Me.edTb_escomp.Name = "edTb_escomp"
    Me.edTb_escomp.NTSDbField = ""
    Me.edTb_escomp.NTSFormat = "0"
    Me.edTb_escomp.NTSForzaVisZoom = False
    Me.edTb_escomp.NTSOldValue = ""
    Me.edTb_escomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_escomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_escomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_escomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_escomp.Properties.AutoHeight = False
    Me.edTb_escomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_escomp.Properties.MaxLength = 65536
    Me.edTb_escomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_escomp.Size = New System.Drawing.Size(53, 20)
    Me.edTb_escomp.TabIndex = 666
    '
    'cbTb_azdoppes
    '
    Me.cbTb_azdoppes.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azdoppes.DataSource = Nothing
    Me.cbTb_azdoppes.DisplayMember = ""
    Me.cbTb_azdoppes.Location = New System.Drawing.Point(235, 67)
    Me.cbTb_azdoppes.Name = "cbTb_azdoppes"
    Me.cbTb_azdoppes.NTSDbField = ""
    Me.cbTb_azdoppes.Properties.AutoHeight = False
    Me.cbTb_azdoppes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azdoppes.Properties.DropDownRows = 30
    Me.cbTb_azdoppes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azdoppes.SelectedValue = ""
    Me.cbTb_azdoppes.Size = New System.Drawing.Size(137, 20)
    Me.cbTb_azdoppes.TabIndex = 675
    Me.cbTb_azdoppes.ValueMember = ""
    '
    'edTb_escompp
    '
    Me.edTb_escompp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_escompp.EditValue = "0"
    Me.edTb_escompp.Location = New System.Drawing.Point(235, 45)
    Me.edTb_escompp.Name = "edTb_escompp"
    Me.edTb_escompp.NTSDbField = ""
    Me.edTb_escompp.NTSFormat = "0"
    Me.edTb_escompp.NTSForzaVisZoom = False
    Me.edTb_escompp.NTSOldValue = ""
    Me.edTb_escompp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_escompp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_escompp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_escompp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_escompp.Properties.AutoHeight = False
    Me.edTb_escompp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_escompp.Properties.MaxLength = 65536
    Me.edTb_escompp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_escompp.Size = New System.Drawing.Size(53, 20)
    Me.edTb_escompp.TabIndex = 663
    '
    'lbTb_azdoppes
    '
    Me.lbTb_azdoppes.AutoSize = True
    Me.lbTb_azdoppes.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azdoppes.Location = New System.Drawing.Point(9, 70)
    Me.lbTb_azdoppes.Name = "lbTb_azdoppes"
    Me.lbTb_azdoppes.NTSDbField = ""
    Me.lbTb_azdoppes.Size = New System.Drawing.Size(82, 13)
    Me.lbTb_azdoppes.TabIndex = 672
    Me.lbTb_azdoppes.Text = "Status esercizio"
    Me.lbTb_azdoppes.Tooltip = ""
    Me.lbTb_azdoppes.UseMnemonic = False
    '
    'lbXx_escomp
    '
    Me.lbXx_escomp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_escomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_escomp.Location = New System.Drawing.Point(294, 24)
    Me.lbXx_escomp.Name = "lbXx_escomp"
    Me.lbXx_escomp.NTSDbField = ""
    Me.lbXx_escomp.Size = New System.Drawing.Size(395, 20)
    Me.lbXx_escomp.TabIndex = 665
    Me.lbXx_escomp.Tooltip = ""
    Me.lbXx_escomp.UseMnemonic = False
    '
    'cbTb_azgestscad
    '
    Me.cbTb_azgestscad.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azgestscad.DataSource = Nothing
    Me.cbTb_azgestscad.DisplayMember = ""
    Me.cbTb_azgestscad.Location = New System.Drawing.Point(235, 111)
    Me.cbTb_azgestscad.Name = "cbTb_azgestscad"
    Me.cbTb_azgestscad.NTSDbField = ""
    Me.cbTb_azgestscad.Properties.AutoHeight = False
    Me.cbTb_azgestscad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azgestscad.Properties.DropDownRows = 30
    Me.cbTb_azgestscad.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azgestscad.SelectedValue = ""
    Me.cbTb_azgestscad.Size = New System.Drawing.Size(137, 20)
    Me.cbTb_azgestscad.TabIndex = 670
    Me.cbTb_azgestscad.ValueMember = ""
    '
    'lbTb_escomp
    '
    Me.lbTb_escomp.AutoSize = True
    Me.lbTb_escomp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_escomp.Location = New System.Drawing.Point(9, 26)
    Me.lbTb_escomp.Name = "lbTb_escomp"
    Me.lbTb_escomp.NTSDbField = ""
    Me.lbTb_escomp.Size = New System.Drawing.Size(138, 13)
    Me.lbTb_escomp.TabIndex = 664
    Me.lbTb_escomp.Text = "Esercizio contabile corrente"
    Me.lbTb_escomp.Tooltip = ""
    Me.lbTb_escomp.UseMnemonic = False
    '
    'cbTb_ventil
    '
    Me.cbTb_ventil.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_ventil.DataSource = Nothing
    Me.cbTb_ventil.DisplayMember = ""
    Me.cbTb_ventil.Location = New System.Drawing.Point(235, 89)
    Me.cbTb_ventil.Name = "cbTb_ventil"
    Me.cbTb_ventil.NTSDbField = ""
    Me.cbTb_ventil.Properties.AutoHeight = False
    Me.cbTb_ventil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_ventil.Properties.DropDownRows = 30
    Me.cbTb_ventil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_ventil.SelectedValue = ""
    Me.cbTb_ventil.Size = New System.Drawing.Size(137, 20)
    Me.cbTb_ventil.TabIndex = 669
    Me.cbTb_ventil.ValueMember = ""
    '
    'lbXx_escompp
    '
    Me.lbXx_escompp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_escompp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_escompp.Location = New System.Drawing.Point(294, 46)
    Me.lbXx_escompp.Name = "lbXx_escompp"
    Me.lbXx_escompp.NTSDbField = ""
    Me.lbXx_escompp.Size = New System.Drawing.Size(395, 20)
    Me.lbXx_escompp.TabIndex = 662
    Me.lbXx_escompp.Tooltip = ""
    Me.lbXx_escompp.UseMnemonic = False
    '
    'lbTb_azgestscad
    '
    Me.lbTb_azgestscad.AutoSize = True
    Me.lbTb_azgestscad.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azgestscad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_azgestscad.Location = New System.Drawing.Point(9, 114)
    Me.lbTb_azgestscad.Name = "lbTb_azgestscad"
    Me.lbTb_azgestscad.NTSDbField = ""
    Me.lbTb_azgestscad.Size = New System.Drawing.Size(131, 13)
    Me.lbTb_azgestscad.TabIndex = 668
    Me.lbTb_azgestscad.Text = "Gestione scadenziario"
    Me.lbTb_azgestscad.Tooltip = ""
    Me.lbTb_azgestscad.UseMnemonic = False
    '
    'lbTb_escompp
    '
    Me.lbTb_escompp.AutoSize = True
    Me.lbTb_escompp.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_escompp.Location = New System.Drawing.Point(9, 48)
    Me.lbTb_escompp.Name = "lbTb_escompp"
    Me.lbTb_escompp.NTSDbField = ""
    Me.lbTb_escompp.Size = New System.Drawing.Size(152, 13)
    Me.lbTb_escompp.TabIndex = 661
    Me.lbTb_escompp.Text = "Esercizio contabile precedente"
    Me.lbTb_escompp.Tooltip = ""
    Me.lbTb_escompp.UseMnemonic = False
    '
    'lbTb_ventil
    '
    Me.lbTb_ventil.AutoSize = True
    Me.lbTb_ventil.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ventil.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_ventil.Location = New System.Drawing.Point(9, 92)
    Me.lbTb_ventil.Name = "lbTb_ventil"
    Me.lbTb_ventil.NTSDbField = ""
    Me.lbTb_ventil.Size = New System.Drawing.Size(99, 13)
    Me.lbTb_ventil.TabIndex = 667
    Me.lbTb_ventil.Text = "Gestione partite"
    Me.lbTb_ventil.Tooltip = ""
    Me.lbTb_ventil.UseMnemonic = False
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnFornitura)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(765, 356)
    Me.NtsTabPage5.Text = "&5 - Libro Giornale"
    '
    'pnFornitura
    '
    Me.pnFornitura.AllowDrop = True
    Me.pnFornitura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFornitura.Appearance.Options.UseBackColor = True
    Me.pnFornitura.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFornitura.Controls.Add(Me.lbNota)
    Me.pnFornitura.Controls.Add(Me.edTb_pgullgp)
    Me.pnFornitura.Controls.Add(Me.edTb_pravlgp)
    Me.pnFornitura.Controls.Add(Me.edTb_prdalgp)
    Me.pnFornitura.Controls.Add(Me.edTb_riullgp)
    Me.pnFornitura.Controls.Add(Me.edTb_rgullgp)
    Me.pnFornitura.Controls.Add(Me.edTb_dtullgp)
    Me.pnFornitura.Controls.Add(Me.lbEscompp)
    Me.pnFornitura.Controls.Add(Me.pnCondFornsx)
    Me.pnFornitura.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFornitura.Location = New System.Drawing.Point(0, 0)
    Me.pnFornitura.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnFornitura.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnFornitura.Name = "pnFornitura"
    Me.pnFornitura.NTSActiveTrasparency = True
    Me.pnFornitura.Size = New System.Drawing.Size(779, 373)
    Me.pnFornitura.TabIndex = 1
    Me.pnFornitura.Text = "NtsPanel3"
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNota.Location = New System.Drawing.Point(9, 232)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(532, 13)
    Me.lbNota.TabIndex = 695
    Me.lbNota.Text = "I dati in 'Esercizio precedente' vengono ignorati quando 'Chiusure post-fine eser" & _
        "cizio e Libro Giornale continuo'"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'edTb_pgullgp
    '
    Me.edTb_pgullgp.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_pgullgp.EditValue = "0"
    Me.edTb_pgullgp.Location = New System.Drawing.Point(548, 130)
    Me.edTb_pgullgp.Name = "edTb_pgullgp"
    Me.edTb_pgullgp.NTSDbField = ""
    Me.edTb_pgullgp.NTSFormat = "0"
    Me.edTb_pgullgp.NTSForzaVisZoom = False
    Me.edTb_pgullgp.NTSOldValue = ""
    Me.edTb_pgullgp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pgullgp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pgullgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_pgullgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_pgullgp.Properties.AutoHeight = False
    Me.edTb_pgullgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pgullgp.Properties.MaxLength = 65536
    Me.edTb_pgullgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pgullgp.Size = New System.Drawing.Size(100, 20)
    Me.edTb_pgullgp.TabIndex = 694
    '
    'edTb_pravlgp
    '
    Me.edTb_pravlgp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pravlgp.EditValue = "0"
    Me.edTb_pravlgp.Location = New System.Drawing.Point(473, 108)
    Me.edTb_pravlgp.Name = "edTb_pravlgp"
    Me.edTb_pravlgp.NTSDbField = ""
    Me.edTb_pravlgp.NTSFormat = "0"
    Me.edTb_pravlgp.NTSForzaVisZoom = False
    Me.edTb_pravlgp.NTSOldValue = ""
    Me.edTb_pravlgp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pravlgp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pravlgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_pravlgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_pravlgp.Properties.AutoHeight = False
    Me.edTb_pravlgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pravlgp.Properties.MaxLength = 65536
    Me.edTb_pravlgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pravlgp.Size = New System.Drawing.Size(175, 20)
    Me.edTb_pravlgp.TabIndex = 693
    '
    'edTb_prdalgp
    '
    Me.edTb_prdalgp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_prdalgp.EditValue = "0"
    Me.edTb_prdalgp.Location = New System.Drawing.Point(473, 86)
    Me.edTb_prdalgp.Name = "edTb_prdalgp"
    Me.edTb_prdalgp.NTSDbField = ""
    Me.edTb_prdalgp.NTSFormat = "0"
    Me.edTb_prdalgp.NTSForzaVisZoom = False
    Me.edTb_prdalgp.NTSOldValue = ""
    Me.edTb_prdalgp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_prdalgp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_prdalgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_prdalgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_prdalgp.Properties.AutoHeight = False
    Me.edTb_prdalgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_prdalgp.Properties.MaxLength = 65536
    Me.edTb_prdalgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_prdalgp.Size = New System.Drawing.Size(175, 20)
    Me.edTb_prdalgp.TabIndex = 692
    '
    'edTb_riullgp
    '
    Me.edTb_riullgp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_riullgp.EditValue = "0"
    Me.edTb_riullgp.Location = New System.Drawing.Point(548, 64)
    Me.edTb_riullgp.Name = "edTb_riullgp"
    Me.edTb_riullgp.NTSDbField = ""
    Me.edTb_riullgp.NTSFormat = "0"
    Me.edTb_riullgp.NTSForzaVisZoom = False
    Me.edTb_riullgp.NTSOldValue = ""
    Me.edTb_riullgp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_riullgp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_riullgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_riullgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_riullgp.Properties.AutoHeight = False
    Me.edTb_riullgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_riullgp.Properties.MaxLength = 65536
    Me.edTb_riullgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_riullgp.Size = New System.Drawing.Size(100, 20)
    Me.edTb_riullgp.TabIndex = 690
    '
    'edTb_rgullgp
    '
    Me.edTb_rgullgp.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_rgullgp.EditValue = "0"
    Me.edTb_rgullgp.Location = New System.Drawing.Point(548, 42)
    Me.edTb_rgullgp.Name = "edTb_rgullgp"
    Me.edTb_rgullgp.NTSDbField = ""
    Me.edTb_rgullgp.NTSFormat = "0"
    Me.edTb_rgullgp.NTSForzaVisZoom = False
    Me.edTb_rgullgp.NTSOldValue = ""
    Me.edTb_rgullgp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rgullgp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rgullgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rgullgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rgullgp.Properties.AutoHeight = False
    Me.edTb_rgullgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rgullgp.Properties.MaxLength = 65536
    Me.edTb_rgullgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rgullgp.Size = New System.Drawing.Size(100, 20)
    Me.edTb_rgullgp.TabIndex = 689
    '
    'edTb_dtullgp
    '
    Me.edTb_dtullgp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtullgp.EditValue = "01/01/1900"
    Me.edTb_dtullgp.Location = New System.Drawing.Point(548, 20)
    Me.edTb_dtullgp.Name = "edTb_dtullgp"
    Me.edTb_dtullgp.NTSDbField = ""
    Me.edTb_dtullgp.NTSForzaVisZoom = False
    Me.edTb_dtullgp.NTSOldValue = ""
    Me.edTb_dtullgp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtullgp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtullgp.Properties.AutoHeight = False
    Me.edTb_dtullgp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtullgp.Properties.MaxLength = 65536
    Me.edTb_dtullgp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtullgp.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtullgp.TabIndex = 681
    '
    'lbEscompp
    '
    Me.lbEscompp.AutoSize = True
    Me.lbEscompp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscompp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbEscompp.Location = New System.Drawing.Point(496, 3)
    Me.lbEscompp.Name = "lbEscompp"
    Me.lbEscompp.NTSDbField = ""
    Me.lbEscompp.Size = New System.Drawing.Size(124, 13)
    Me.lbEscompp.TabIndex = 665
    Me.lbEscompp.Text = "Esercizio precedente"
    Me.lbEscompp.Tooltip = ""
    Me.lbEscompp.UseMnemonic = False
    '
    'pnCondFornsx
    '
    Me.pnCondFornsx.AllowDrop = True
    Me.pnCondFornsx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCondFornsx.Appearance.Options.UseBackColor = True
    Me.pnCondFornsx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCondFornsx.Controls.Add(Me.edTb_pgullg)
    Me.pnCondFornsx.Controls.Add(Me.edTb_pravlg)
    Me.pnCondFornsx.Controls.Add(Me.edTb_prdalg)
    Me.pnCondFornsx.Controls.Add(Me.edTb_riullg)
    Me.pnCondFornsx.Controls.Add(Me.edTb_rgulel)
    Me.pnCondFornsx.Controls.Add(Me.edTb_rgullg)
    Me.pnCondFornsx.Controls.Add(Me.edTb_dtulel)
    Me.pnCondFornsx.Controls.Add(Me.edTb_dtullg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_rgulel)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_dtulel)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_riullg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_pravlg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_pgullg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_prdalg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_rgullg)
    Me.pnCondFornsx.Controls.Add(Me.lbTb_dtullg)
    Me.pnCondFornsx.Controls.Add(Me.lbEscomp)
    Me.pnCondFornsx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCondFornsx.Location = New System.Drawing.Point(0, 0)
    Me.pnCondFornsx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCondFornsx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCondFornsx.Name = "pnCondFornsx"
    Me.pnCondFornsx.NTSActiveTrasparency = True
    Me.pnCondFornsx.Size = New System.Drawing.Size(472, 220)
    Me.pnCondFornsx.TabIndex = 664
    Me.pnCondFornsx.Text = "NtsPanel1"
    '
    'edTb_pgullg
    '
    Me.edTb_pgullg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pgullg.EditValue = "0"
    Me.edTb_pgullg.Location = New System.Drawing.Point(361, 130)
    Me.edTb_pgullg.Name = "edTb_pgullg"
    Me.edTb_pgullg.NTSDbField = ""
    Me.edTb_pgullg.NTSFormat = "0"
    Me.edTb_pgullg.NTSForzaVisZoom = False
    Me.edTb_pgullg.NTSOldValue = ""
    Me.edTb_pgullg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pgullg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pgullg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_pgullg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_pgullg.Properties.AutoHeight = False
    Me.edTb_pgullg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pgullg.Properties.MaxLength = 65536
    Me.edTb_pgullg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pgullg.Size = New System.Drawing.Size(100, 20)
    Me.edTb_pgullg.TabIndex = 693
    '
    'edTb_pravlg
    '
    Me.edTb_pravlg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_pravlg.EditValue = "0"
    Me.edTb_pravlg.Location = New System.Drawing.Point(286, 108)
    Me.edTb_pravlg.Name = "edTb_pravlg"
    Me.edTb_pravlg.NTSDbField = ""
    Me.edTb_pravlg.NTSFormat = "0"
    Me.edTb_pravlg.NTSForzaVisZoom = False
    Me.edTb_pravlg.NTSOldValue = ""
    Me.edTb_pravlg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_pravlg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_pravlg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_pravlg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_pravlg.Properties.AutoHeight = False
    Me.edTb_pravlg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_pravlg.Properties.MaxLength = 65536
    Me.edTb_pravlg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_pravlg.Size = New System.Drawing.Size(175, 20)
    Me.edTb_pravlg.TabIndex = 692
    '
    'edTb_prdalg
    '
    Me.edTb_prdalg.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_prdalg.EditValue = "0"
    Me.edTb_prdalg.Location = New System.Drawing.Point(286, 86)
    Me.edTb_prdalg.Name = "edTb_prdalg"
    Me.edTb_prdalg.NTSDbField = ""
    Me.edTb_prdalg.NTSFormat = "0"
    Me.edTb_prdalg.NTSForzaVisZoom = False
    Me.edTb_prdalg.NTSOldValue = ""
    Me.edTb_prdalg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_prdalg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_prdalg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_prdalg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_prdalg.Properties.AutoHeight = False
    Me.edTb_prdalg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_prdalg.Properties.MaxLength = 65536
    Me.edTb_prdalg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_prdalg.Size = New System.Drawing.Size(175, 20)
    Me.edTb_prdalg.TabIndex = 691
    '
    'edTb_riullg
    '
    Me.edTb_riullg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_riullg.EditValue = "0"
    Me.edTb_riullg.Location = New System.Drawing.Point(361, 64)
    Me.edTb_riullg.Name = "edTb_riullg"
    Me.edTb_riullg.NTSDbField = ""
    Me.edTb_riullg.NTSFormat = "0"
    Me.edTb_riullg.NTSForzaVisZoom = False
    Me.edTb_riullg.NTSOldValue = ""
    Me.edTb_riullg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_riullg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_riullg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_riullg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_riullg.Properties.AutoHeight = False
    Me.edTb_riullg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_riullg.Properties.MaxLength = 65536
    Me.edTb_riullg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_riullg.Size = New System.Drawing.Size(100, 20)
    Me.edTb_riullg.TabIndex = 690
    '
    'edTb_rgulel
    '
    Me.edTb_rgulel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rgulel.EditValue = "0"
    Me.edTb_rgulel.Location = New System.Drawing.Point(361, 190)
    Me.edTb_rgulel.Name = "edTb_rgulel"
    Me.edTb_rgulel.NTSDbField = ""
    Me.edTb_rgulel.NTSFormat = "0"
    Me.edTb_rgulel.NTSForzaVisZoom = False
    Me.edTb_rgulel.NTSOldValue = ""
    Me.edTb_rgulel.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rgulel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rgulel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rgulel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rgulel.Properties.AutoHeight = False
    Me.edTb_rgulel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rgulel.Properties.MaxLength = 65536
    Me.edTb_rgulel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rgulel.Size = New System.Drawing.Size(100, 20)
    Me.edTb_rgulel.TabIndex = 689
    '
    'edTb_rgullg
    '
    Me.edTb_rgullg.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_rgullg.EditValue = "0"
    Me.edTb_rgullg.Location = New System.Drawing.Point(361, 42)
    Me.edTb_rgullg.Name = "edTb_rgullg"
    Me.edTb_rgullg.NTSDbField = ""
    Me.edTb_rgullg.NTSFormat = "0"
    Me.edTb_rgullg.NTSForzaVisZoom = False
    Me.edTb_rgullg.NTSOldValue = ""
    Me.edTb_rgullg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rgullg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rgullg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rgullg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rgullg.Properties.AutoHeight = False
    Me.edTb_rgullg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rgullg.Properties.MaxLength = 65536
    Me.edTb_rgullg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rgullg.Size = New System.Drawing.Size(100, 20)
    Me.edTb_rgullg.TabIndex = 688
    '
    'edTb_dtulel
    '
    Me.edTb_dtulel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulel.EditValue = "01/01/1900"
    Me.edTb_dtulel.Location = New System.Drawing.Point(361, 168)
    Me.edTb_dtulel.Name = "edTb_dtulel"
    Me.edTb_dtulel.NTSDbField = ""
    Me.edTb_dtulel.NTSForzaVisZoom = False
    Me.edTb_dtulel.NTSOldValue = ""
    Me.edTb_dtulel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulel.Properties.AutoHeight = False
    Me.edTb_dtulel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulel.Properties.MaxLength = 65536
    Me.edTb_dtulel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulel.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulel.TabIndex = 682
    '
    'edTb_dtullg
    '
    Me.edTb_dtullg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtullg.EditValue = "01/01/1900"
    Me.edTb_dtullg.Location = New System.Drawing.Point(361, 20)
    Me.edTb_dtullg.Name = "edTb_dtullg"
    Me.edTb_dtullg.NTSDbField = ""
    Me.edTb_dtullg.NTSForzaVisZoom = False
    Me.edTb_dtullg.NTSOldValue = ""
    Me.edTb_dtullg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtullg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtullg.Properties.AutoHeight = False
    Me.edTb_dtullg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtullg.Properties.MaxLength = 65536
    Me.edTb_dtullg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtullg.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtullg.TabIndex = 681
    '
    'lbTb_rgulel
    '
    Me.lbTb_rgulel.AutoSize = True
    Me.lbTb_rgulel.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rgulel.Location = New System.Drawing.Point(9, 193)
    Me.lbTb_rgulel.Name = "lbTb_rgulel"
    Me.lbTb_rgulel.NTSDbField = ""
    Me.lbTb_rgulel.Size = New System.Drawing.Size(180, 13)
    Me.lbTb_rgulel.TabIndex = 20
    Me.lbTb_rgulel.Text = "Ultima registrazione stampa partitari"
    Me.lbTb_rgulel.Tooltip = ""
    Me.lbTb_rgulel.UseMnemonic = False
    '
    'lbTb_dtulel
    '
    Me.lbTb_dtulel.AutoSize = True
    Me.lbTb_dtulel.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulel.Location = New System.Drawing.Point(9, 171)
    Me.lbTb_dtulel.Name = "lbTb_dtulel"
    Me.lbTb_dtulel.NTSDbField = ""
    Me.lbTb_dtulel.Size = New System.Drawing.Size(205, 13)
    Me.lbTb_dtulel.TabIndex = 19
    Me.lbTb_dtulel.Text = "Data registrazione ultima stampa partitari"
    Me.lbTb_dtulel.Tooltip = ""
    Me.lbTb_dtulel.UseMnemonic = False
    '
    'lbTb_riullg
    '
    Me.lbTb_riullg.AutoSize = True
    Me.lbTb_riullg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_riullg.Location = New System.Drawing.Point(9, 67)
    Me.lbTb_riullg.Name = "lbTb_riullg"
    Me.lbTb_riullg.NTSDbField = ""
    Me.lbTb_riullg.Size = New System.Drawing.Size(238, 13)
    Me.lbTb_riullg.TabIndex = 18
    Me.lbTb_riullg.Text = "Progressivo riga ultima stampa del Libro Giornale"
    Me.lbTb_riullg.Tooltip = ""
    Me.lbTb_riullg.UseMnemonic = False
    '
    'lbTb_pravlg
    '
    Me.lbTb_pravlg.AutoSize = True
    Me.lbTb_pravlg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pravlg.Location = New System.Drawing.Point(9, 112)
    Me.lbTb_pravlg.Name = "lbTb_pravlg"
    Me.lbTb_pravlg.NTSDbField = ""
    Me.lbTb_pravlg.Size = New System.Drawing.Size(249, 13)
    Me.lbTb_pravlg.TabIndex = 17
    Me.lbTb_pravlg.Text = "Progressivo Avere ultima stampa del Libro Giornale"
    Me.lbTb_pravlg.Tooltip = ""
    Me.lbTb_pravlg.UseMnemonic = False
    '
    'lbTb_pgullg
    '
    Me.lbTb_pgullg.AutoSize = True
    Me.lbTb_pgullg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_pgullg.Location = New System.Drawing.Point(9, 133)
    Me.lbTb_pgullg.Name = "lbTb_pgullg"
    Me.lbTb_pgullg.NTSDbField = ""
    Me.lbTb_pgullg.Size = New System.Drawing.Size(226, 13)
    Me.lbTb_pgullg.TabIndex = 16
    Me.lbTb_pgullg.Text = "Numero ultima pagina stampata Libro Giornale"
    Me.lbTb_pgullg.Tooltip = ""
    Me.lbTb_pgullg.UseMnemonic = False
    '
    'lbTb_prdalg
    '
    Me.lbTb_prdalg.AutoSize = True
    Me.lbTb_prdalg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_prdalg.Location = New System.Drawing.Point(9, 89)
    Me.lbTb_prdalg.Name = "lbTb_prdalg"
    Me.lbTb_prdalg.NTSDbField = ""
    Me.lbTb_prdalg.Size = New System.Drawing.Size(243, 13)
    Me.lbTb_prdalg.TabIndex = 15
    Me.lbTb_prdalg.Text = "Progressivo Dare ultima stampa del Libro Giornale"
    Me.lbTb_prdalg.Tooltip = ""
    Me.lbTb_prdalg.UseMnemonic = False
    '
    'lbTb_rgullg
    '
    Me.lbTb_rgullg.AutoSize = True
    Me.lbTb_rgullg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rgullg.Location = New System.Drawing.Point(9, 45)
    Me.lbTb_rgullg.Name = "lbTb_rgullg"
    Me.lbTb_rgullg.NTSDbField = ""
    Me.lbTb_rgullg.Size = New System.Drawing.Size(263, 13)
    Me.lbTb_rgullg.TabIndex = 14
    Me.lbTb_rgullg.Text = "Numero registrazione ultima stampa del Libro Giornale"
    Me.lbTb_rgullg.Tooltip = ""
    Me.lbTb_rgullg.UseMnemonic = False
    '
    'lbTb_dtullg
    '
    Me.lbTb_dtullg.AutoSize = True
    Me.lbTb_dtullg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtullg.Location = New System.Drawing.Point(9, 23)
    Me.lbTb_dtullg.Name = "lbTb_dtullg"
    Me.lbTb_dtullg.NTSDbField = ""
    Me.lbTb_dtullg.Size = New System.Drawing.Size(249, 13)
    Me.lbTb_dtullg.TabIndex = 13
    Me.lbTb_dtullg.Text = "Data registrazione ultima stampa del Libro Giornale"
    Me.lbTb_dtullg.Tooltip = ""
    Me.lbTb_dtullg.UseMnemonic = False
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbEscomp.Location = New System.Drawing.Point(316, 3)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(103, 13)
    Me.lbEscomp.TabIndex = 12
    Me.lbEscomp.Text = "Esercizio in corso"
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'cmdDelImmagine
    '
    Me.cmdDelImmagine.Image = CType(resources.GetObject("cmdDelImmagine.Image"), System.Drawing.Image)
    Me.cmdDelImmagine.ImagePath = ""
    Me.cmdDelImmagine.ImageText = ""
    Me.cmdDelImmagine.Location = New System.Drawing.Point(736, 27)
    Me.cmdDelImmagine.Name = "cmdDelImmagine"
    Me.cmdDelImmagine.NTSContextMenu = Nothing
    Me.cmdDelImmagine.Size = New System.Drawing.Size(25, 23)
    Me.cmdDelImmagine.TabIndex = 687
    '
    'FRM__ANAZ_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(774, 489)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__ANAZ_F"
    Me.Text = "ANAGRAFICA DITTA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azrags1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azpersfg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edTb_imagename.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azrags2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_codditt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tsAnaz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsAnaz.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    CType(Me.pnPag1Dx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Dx.ResumeLayout(False)
    Me.pnPag1Dx.PerformLayout()
    CType(Me.edTb_azcell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_azomocodice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azusaem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azemail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azpiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azfaxtlx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aztelef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag1Sx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Sx.ResumeLayout(False)
    Me.pnPag1Sx.PerformLayout()
    CType(Me.edTb_azstatofed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodcomu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azindir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azstato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2.ResumeLayout(False)
    Me.pnPag2.PerformLayout()
    CType(Me.fmPersfisica, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPersfisica.ResumeLayout(False)
    Me.fmPersfisica.PerformLayout()
    CType(Me.edTb_aztitolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcognome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_aznome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_sesso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmNascita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNascita.ResumeLayout(False)
    Me.fmNascita.PerformLayout()
    CType(Me.edTb_datnasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pronasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_locnasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodcomn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azstanasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_azcondom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_azsoggresi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_azprofes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag3.ResumeLayout(False)
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmWeb.ResumeLayout(False)
    Me.fmWeb.PerformLayout()
    CType(Me.edTb_latitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_longitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edXx_nflogo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azwebpwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azwebsite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azwebuid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAltriDatiSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAltriDatiSx.ResumeLayout(False)
    Me.pnAltriDatiSx.PerformLayout()
    CType(Me.fmBanca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmBanca.ResumeLayout(False)
    Me.fmBanca.PerformLayout()
    CType(Me.edTb_azcodcaf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azrifriba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodcab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodabi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulst.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodrtac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnDatiContabili, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDatiContabili.ResumeLayout(False)
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.edTb_dtulliqtri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulliq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_uffiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatiContabili, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatiContabili.ResumeLayout(False)
    Me.fmDatiContabili.PerformLayout()
    CType(Me.edTb_escomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azdoppes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_escompp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azgestscad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_ventil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnFornitura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFornitura.ResumeLayout(False)
    Me.pnFornitura.PerformLayout()
    CType(Me.edTb_pgullgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pravlgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_prdalgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_riullgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rgullgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtullgp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCondFornsx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCondFornsx.ResumeLayout(False)
    Me.pnCondFornsx.PerformLayout()
    CType(Me.edTb_pgullg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_pravlg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_prdalg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_riullg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rgulel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rgullg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtullg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEscomp.GlyphPath = (oApp.ChildImageDir & "\ordini_1.gif")
        tlbIva.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edTb_azrags1.NTSSetParam(oMenu, oApp.Tr(Me, 128765041470872000, "Rag. sociale"), 50, False)
      edTb_azrags2.NTSSetParam(oMenu, oApp.Tr(Me, 128765041489436000, "Rag. sociale 2"), 50, True)
      edTb_imagename.NTSSetParam(oMenu, oApp.Tr(Me, 130143726166381999, "Logo azienda per report"), 50, True)
      edTb_azindir.NTSSetParam(oMenu, oApp.Tr(Me, 128765041500512000, "Indirizzo"), 70, True)
      edTb_azcap.NTSSetParam(oMenu, oApp.Tr(Me, 128765041512836000, "Cap"), 9, True)
      edTb_azcitta.NTSSetParam(oMenu, oApp.Tr(Me, 128765041523288000, "Citta/località"), 50, True)
      edTb_azprov.NTSSetParam(oMenu, oApp.Tr(Me, 128765041533584000, "Prov"), 2, True)
      edTb_azstato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041543880000, "Cod. stato estero"), tabstat, True)
      edTb_azcodf.NTSSetParam(oMenu, oApp.Tr(Me, 128765041553708000, "Cod. fiscale"), 16, True)
      edTb_azpiva.NTSSetParam(oMenu, oApp.Tr(Me, 128765041562600000, "Partita IVA"), 11, True)
      edTb_aztelef.NTSSetParam(oMenu, oApp.Tr(Me, 128765041571804000, "Telefono"), 18, True)
      edTb_azfaxtlx.NTSSetParam(oMenu, oApp.Tr(Me, 128765041581164000, "Fax"), 18, True)
      edTb_azemail.NTSSetParam(oMenu, oApp.Tr(Me, 128765042249994000, "E-mail"), 50, True)
      edTb_azwebsite.NTSSetParam(oMenu, oApp.Tr(Me, 128765041605500000, "Sito Web"), 50, True)
      cbTb_azusaem.NTSSetParam(oApp.Tr(Me, 128765041635296000, "Modalità di corrispondenza"))
      edTb_azwebuid.NTSSetParam(oMenu, oApp.Tr(Me, 128765041650740000, "UserID sito Web"), 20, True)
      edTb_azwebpwd.NTSSetParam(oMenu, oApp.Tr(Me, 128765041661660000, "Pwd sito Web"), 20, True)
      cbTb_sesso.NTSSetParam(oApp.Tr(Me, 128765041684748000, "Sesso"))
      edTb_datnasc.NTSSetParam(oMenu, oApp.Tr(Me, 128765041696760000, "Data nasc./costituz."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_locnasc.NTSSetParam(oMenu, oApp.Tr(Me, 128765041707992000, "Città nasc."), 50, True)
      edTb_pronasc.NTSSetParam(oMenu, oApp.Tr(Me, 128765041719536000, "Prov. nasc."), 2, True)
      edTb_azstanasc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041731704000, "Stato nasc."), tabstat, True)
      edTb_azcell.NTSSetParam(oMenu, oApp.Tr(Me, 128765041752452000, "Cellulare"), 18, True)
      edTb_aztitolo.NTSSetParam(oMenu, oApp.Tr(Me, 128765041762748000, "Titolo"), 8, True)
      cbTb_azpersfg.NTSSetParam(oApp.Tr(Me, 128765041772888000, "Tipo sogg."))
      ckTb_azprofes.NTSSetParam(oMenu, oApp.Tr(Me, 128765041785212000, "Professionista"), "S", "N")
      ckTb_azcondom.NTSSetParam(oMenu, oApp.Tr(Me, 128765041797068000, "Condominio"), "S", "N")
      edTb_azcodcomu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041808612000, "Cod. comune"), tabcomuni, True)
      edTb_azcognome.NTSSetParam(oMenu, oApp.Tr(Me, 128765041867424000, "Cognome"), 30, True)
      edTb_aznome.NTSSetParam(oMenu, oApp.Tr(Me, 128765041919372000, "Nome"), 30, True)
      edTb_azcodcomn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041940120000, "Cod. comune nasc."), tabcomuni, True)
      edTb_azstatofed.NTSSetParam(oMenu, oApp.Tr(Me, 128765041968200000, "Stato fed./contea"), 30, True)
      ckTb_azsoggresi.NTSSetParam(oMenu, oApp.Tr(Me, 128765041977716000, "Residente"), "S", "N")
      ckTb_azomocodice.NTSSetParam(oMenu, oApp.Tr(Me, 128765041986920000, "Omocodice"), "S", "N")
      edTb_azcodrtac.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765042232366000, "Tipo di assoggettamento a ritenuta d'acconto"), tabrtac)

      edTb_dtulst.NTSSetParam(oMenu, oApp.Tr(Me, 128642157425312500, "Data ultima cancellazione movimenti di magazzino"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_dtulap.NTSSetParam(oMenu, oApp.Tr(Me, 128642157425625000, "Data ultimo aggiornamento progressivi di magazzino"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_azcodcc.NTSSetParam(oMenu, oApp.Tr(Me, 128642157426718750, "Numero C/C"), 12, True)
      edTb_azrifriba.NTSSetParam(oMenu, oApp.Tr(Me, 128642157426875000, "Codice SIA"), 12, True)
      edTb_azcodcab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642157427031250, "Codice CAB"), tababicab)
      edTb_azcodabi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642157427187500, "Codice ABI"), tababi)
      edXx_nflogo.NTSSetParam(oMenu, oApp.Tr(Me, 128642157427500000, "Logo azienda"), 12, True)
      cbTb_azdoppes.NTSSetParam(oApp.Tr(Me, 128642157429687500, "Status esercizio"))
      cbTb_azgestscad.NTSSetParam(oApp.Tr(Me, 128642157430468750, "Gestione scadenze"))
      cbTb_ventil.NTSSetParam(oApp.Tr(Me, 128642157430625000, "Gestione partite"))
      edTb_escomp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642157431562500, "Esercizio corrente"), tabesco)
      edTb_escompp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642157431875000, "Esercizio precedente"), tabesco)
      edTb_pgullgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157433593750, "Numero ultima pagina libro giornale es. precedente"), "0")
      edTb_pravlgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157433750000, "Progressivo AVERE ultima pagina libro giornale es. precedente"), oApp.FormatImporti)
      edTb_prdalgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157433906250, "Progressivo DARE ultima pagina libro giornale es. precedente"), oApp.FormatImporti)
      edTb_riullgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157434062500, "Progressivo riga ultima pagina libro giornale es. precedente"), "0")
      edTb_rgullgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157434218750, "Numero registrazione ultima pagina libro giornale es. precedente"), "0")
      edTb_dtullgp.NTSSetParam(oMenu, oApp.Tr(Me, 128642157434375000, "Data registrazione ultima pagina libro giornale es. precedente"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_pgullg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157434687500, "Numero ultima pagina libro giornale es. corrente"), "0")
      edTb_pravlg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157434843750, "Progressivo AVERE ultima pagina libro giornale es. corrente"), oApp.FormatImporti)
      edTb_prdalg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435000000, "Progressivo DARE ultima pagina libro giornale es. corrente"), oApp.FormatImporti)
      edTb_riullg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435156250, "Progressivo riga ultima pagina libro giornale es. corrente"), "0")
      edTb_rgullg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435468750, "Numero registrazione ultima pagina libro giornale es. corrente"), "0")
      edTb_dtullg.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435781250, "Data registrazione ultima pagina libro giornale es. corrente"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_rgulel.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435312500, "Ultima registrazione stampa partitari"), "0")
      edTb_dtulel.NTSSetParam(oMenu, oApp.Tr(Me, 128642157435625000, "Data registrazione ultima stampa partitari"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_dtulliqtri.NTSSetParam(oMenu, oApp.Tr(Me, 128642157438906250, "Data ultima liquidazione IVA trimestrale"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_dtulliq.NTSSetParam(oMenu, oApp.Tr(Me, 128642157439062500, "Data ultima liquidazione IVA mensile"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_uffiva.NTSSetParam(oMenu, oApp.Tr(Me, 128642157439375000, "Ufficio IVA"), 20, True)
      edXx_codditt.NTSSetParam(oMenu, oApp.Tr(Me, 128642157440000000, "Codice ditta"), 12, True)
      edTb_latitud.NTSSetParam(oMenu, oApp.Tr(Me, 129762730794124958, "Latitudine"), 15, True)
      edTb_longitud.NTSSetParam(oMenu, oApp.Tr(Me, 129762730807760999, "Longitudine"), 15, True)
      edTb_azcodcaf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129791437298012576, "Codice banca predefinito"), tabbanc)

      edTb_azcitta.NTSForzaVisZoom = True
      edTb_azcap.NTSForzaVisZoom = True

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
  Public Overridable Sub FRM__ANAZ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()

      CaricaCombo()

      '--------------------------------------------------------------------------------------------------------------
      oCleAnaz.bCreaCAREVIS = False
      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupBUD) Then
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then oCleAnaz.bCreaCAREVIS = True
      End If
      '--------------------------------------------------------------------------------------------------------------
      strPWDCancellazione = oMenu.GetSettingBus("BS--ANAZ", "OPZIONI", ".", "PWDCancellazione", "nts", " ", "nts")
      oCleAnaz.bGesttabcont = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "GestTabcont", "0", " ", "0"))

      If Not oCleAnaz.Apri(".", False, dsAnaz) Then
        Me.Close()
        Return
      End If
      dcAnaz.DataSource = dsAnaz.Tables("TABANAZ")
      dsAnaz.AcceptChanges()
      Bindcontrols()

      '-------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = ""
      GctlSetRoules()

      SetStato(0)

      lbInfo.Text = oApp.Tr(Me, 130143776502811425, "N.B.: L'immagine sarà sempre prelevata dalla cartella '|" & oApp.ImgDir & "|'")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ANAZ_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim lPos As Integer

    Try
      If Not Apri(False) Then Return


      If Not oCallParams Is Nothing Then
        '---
        'Impostazioni per stringa di connessione dello zoom veloce di gestione delle destinazioni diverse
        strCallParamsDestDiv = oCallParams.strParam
        If Microsoft.VisualBasic.Left(strCallParamsDestDiv, 13) = "ZOOMDESTDIVA;" Then
          lPos = InStr(1, strCallParamsDestDiv, ";")
          strCallParamsDestDiv = Microsoft.VisualBasic.Mid(strCallParamsDestDiv, lPos + 1)
          If DittaCorrente = Microsoft.VisualBasic.Mid(strCallParamsDestDiv, 1, NTSCInt(IIf(InStr(1, strCallParamsDestDiv, ";") > 0, InStr(1, strCallParamsDestDiv, ";") - 1, strCallParamsDestDiv.Length))) Then
            lPos = InStr(1, strCallParamsDestDiv, ";")
            If (lPos > 0) Then
              strCallParamsDestDiv = Microsoft.VisualBasic.Mid(strCallParamsDestDiv, lPos + 1)
              Select Case Microsoft.VisualBasic.Left(strCallParamsDestDiv, 5)
                Case "APRI;"
                  cmdAltriIndir_Click(Nothing, Nothing)
                Case "NUOV;"
                  cmdAltriIndir_Click(Nothing, Nothing)
              End Select
            End If
          End If
        End If
        strCallParamsDestDiv = ""
        '---
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ANAZ_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__ANAZ_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAnaz.Dispose()
      dsAnaz.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      If pnTop.Visible Then
        If Not Salva() Then Return
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
      End If

      If Not LeggiDatiDitta() Then
        Me.Close()
        Return
      End If

      If Not Apri(False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Salva() Then
        CaricaColonneUnbound(CType(dcAnaz.Current, DataRowView).Row)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      If oCleAnaz.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128380294388916000, "La ditta non è mai stato salvata. Eventualmente ripristinare"))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128643872255468750, "Attenzione!" & vbCrLf & _
          "L'eliminazione dell'Anagrafica Ditta comporta la perdita di tutti i dati relativi." & vbCrLf & _
          "Proseguire con la cancellazione?")) = Windows.Forms.DialogResult.No Then Return

      If oApp.InputBoxNew(oApp.Tr(Me, 128643869013125000, "Inserire la Password per la cancellazione della ditta"), "", True) <> strPWDCancellazione Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128643869148906250, "Password non corretta: cancellazione ditta non possibile."))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor
      If Not oCleAnaz.Salva(True) Then Return
      tlbRipristina_ItemClick(Nothing, Nothing)

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAnaz, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'ripristino la forma di pagamento
      If oCleAnaz.bHasChanges = False Then
        oCleAnaz.Ripristina()
        SetStato(0)
        Return
      End If

      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128271029890194656, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleAnaz.Ripristina()

          SetStato(0)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAnaz, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB

    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return

      If edTb_azcitta.ContainsFocus Or edTb_azcap.ContainsFocus Then
        '------------------------------------
        'zoom comune
        NTSZOOM.strIn = NTSCStr(edTb_azcodcomu.Text)
        NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_azcodcomu.Text Then edTb_azcodcomu.Text = NTSZOOM.strIn
        edTb_azcodcomu.Focus()

      ElseIf edTb_azcodcab.ContainsFocus Then
        '------------------------------------
        'zoom cab
        SetFastZoom(edTb_azcodcab.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = edTb_azcodabi.Text   'passo il codice abi
        NTSZOOM.strIn = edTb_azcodcab.Text
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_azcodcab.Text Then edTb_azcodcab.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

      ctrlTmp.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub tlbEscomp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEscomp.ItemClick
    Dim frmEsci As FRM__ESCI_F = Nothing
    Try
      frmEsci = CType(NTSNewFormModal("FRM__ESCI_F"), FRM__ESCI_F)
      frmEsci.Init(oMenu, Nothing, DittaCorrente)
      frmEsci.InitEntity(oCleAnaz)
      frmEsci.ShowDialog()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmEsci Is Nothing Then frmEsci.Dispose()
      frmEsci = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbIva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIva.ItemClick
    Try
      RecordAttivita(0, 0)
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
    If Not Salva() Then Return
    Me.Close()
  End Sub


  Public Overridable Sub tlbWizardDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Dim nYear As Integer = 0        'anno dell'esercizio contabile corrente
    Dim bCG As Boolean = False
    Dim bCesp As Boolean = False
    Dim bParc As Boolean = False
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing

    Try
      Me.ValidaLastControl()

      '--------------------
      'se c'è il modulo cunsultant prima faccio apparire la form di moduli abilitati, ed in base a quelli visualizzo le maschere
      'se non c'è il modulo consultant faccio il tutto da bs--insg e non faccio apparire i moduli abilitati

      If oCleAnaz.dttInsg.Rows(0)!tb_mod3_9.ToString = "S" Then
        For i = 0 To dsAnaz.Tables("ANAZMOD").Rows.Count - 1
          Select Case NTSCInt(dsAnaz.Tables("ANAZMOD").Rows(i)!am_modulo)
            Case 1 : If dsAnaz.Tables("ANAZMOD").Rows(i)!am_abilit.ToString = "S" Then bCG = True 'contabilità generale
            Case 9 : If dsAnaz.Tables("ANAZMOD").Rows(i)!am_abilit.ToString = "S" Then bCesp = True 'cespiti
            Case 38 : If dsAnaz.Tables("ANAZMOD").Rows(i)!am_abilit.ToString = "S" Then bParc = True 'parcellazione
            Case 41 : If dsAnaz.Tables("ANAZMOD").Rows(i)!am_abilit.ToString = "S" Then bCG = True 'contabilità easy
          End Select
        Next
      Else
        If oCleAnaz.dttInsg.Rows(0)!tb_mod_1.ToString = "S" Then bCG = True
        If oCleAnaz.dttInsg.Rows(0)!tb_mod4_1.ToString = "S" Then bCG = True
        If oCleAnaz.dttInsg.Rows(0)!tb_mod_9.ToString = "S" Then bCesp = True
        If oCleAnaz.dttInsg.Rows(0)!tb_mod3_8.ToString = "S" Then bParc = True
      End If

      '--------------------
      'comunque faccio compilare dati esrcizi contabili e dati aggiuntivi CG
      tlbEscomp_ItemClick(tlbEscomp, Nothing)
      If dsAnaz.Tables("TABESCO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128647577409687500, "Wizard interrotto. Non è stato indicato nessun esercizio contabile."))
        Return
      Else
        i = 0
        dtrT = dsAnaz.Tables("TABESCO").Select("", "tb_codesco DESC")
        If NTSCInt(edTb_escomp.Text) = 0 Then
          dsAnaz.Tables("TABANAZ").Rows(0)!tb_escomp = dtrT(0)!tb_codesco.ToString
          nYear = Year(NTSCDate(dtrT(0)!tb_dtfieser))
          If dtrT.Length > 1 Then
            dsAnaz.Tables("TABANAZ").Rows(0)!tb_escompp = dtrT(1)!tb_codesco.ToString
          End If
        Else
          'determino l'anno in base all'esercizio precedentemente impostato
          Do While NTSCInt(edTb_escomp.Text) <> NTSCInt(dtrT(i)!tb_codesco)
            i = i + 1
          Loop
          nYear = Year(NTSCDate(dtrT(0)!tb_dtfieser))
        End If
      End If    'If dsAnaz.Tables("TABESCO").Rows.Count = 0 Then

      '--------------------
      If bCG Then
        RecordAttivita(0, nYear)
        dtrT = dsAnaz.Tables("ANAZIVA").Select("ai_aanno = " & nYear.ToString)
        If dtrT.Length = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128647584611250000, "Wizard interrotto. La maschera dei dati aggiuntivi IVA non è stata compilata e salvata per l'anno |" & nYear.ToString & "|"))
          Return
        Else
          dtrT1 = dsAnaz.Tables("TABATTI").Select("tb_anno = " & nYear.ToString)
          If dtrT1.Length = 0 Then
            RecordAttivita(1, nYear)

            'se non è stato inserito nulla esco
            If dsAnaz.Tables("TABATTI").Select("tb_anno = " & nYear.ToString).Length = 0 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128647586950468750, "Wizard interrotto. Non è stato indicato nessuna attività IVA per l'anno |" & nYear.ToString & "|"))
              Return
            End If
          End If

          '-------------------------------
          dtrT1 = dsAnaz.Tables("TABDURI").Select("tb_anno = " & nYear.ToString)
          If dtrT1.Length = 0 Then
            RecordAttivita(2, nYear)

            'se non è stato inserito nulla esco
            If dsAnaz.Tables("TABDURI").Select("tb_anno = " & nYear.ToString).Length = 0 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128647586934531250, "Wizard interrotto. Non è stato indicato nessun registro IVA per l'anno |" & nYear.ToString & "|"))
              Return
            End If
          End If

        End If    'If dtrT.Length = 0 Then
      End If    'If bCG Then

      '--------------------
      tlbDatiAggCont_ItemClick(tlbDatiAggCont, Nothing)

      oApp.MsgBoxInfo(oApp.Tr(Me, 128647581322031250, "Procedura completata. Le impostazioni possono essere ritrattate utilizzando gli appositi comandi del menu."))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDatiAggCont_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDatiAggCont.ItemClick
    Dim frmDita As FRM__DITA_F = Nothing
    Try
      frmDita = CType(NTSNewFormModal("FRM__DITA_F"), FRM__DITA_F)
      frmDita.Init(oMenu, Nothing, DittaCorrente)
      frmDita.InitEntity(oCleAnaz)
      frmDita.ShowDialog()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDita Is Nothing Then frmDita.Dispose()
      frmDita = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCalcolaCodFisc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCalcolaCodFisc.ItemClick
    Try
      edTb_azcodf.NTSTextDB = oMenu.CalcolaCodFiscEx(edTb_azcognome.Text, edTb_aznome.Text, cbTb_sesso.SelectedValue, _
                                                  edTb_datnasc.Text, edTb_azcodcomn.Text, lbXx_azcodcomn.Text, _
                                                  edTb_pronasc.Text, edTb_azstanasc.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRitornaCodFisc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRitornaCodFisc.ItemClick
    Dim i As Integer = 0
    Dim strCodcomu As String = ""
    Dim strComune As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strSesso As String = ""
    Dim strDatnasc As String = ""
    Try

      If oMenu.GetDatiFromCodFiscEx(edTb_azcodf.Text, strCodcomu, strComune, _
                                    strProv, strStato, strSesso, strDatnasc) Then
        edTb_azcodcomn.NTSTextDB = strCodcomu
        lbXx_azcodcomn.Text = strComune
        edTb_locnasc.NTSTextDB = strComune
        edTb_pronasc.NTSTextDB = strProv
        edTb_azstanasc.NTSTextDB = strStato
        cbTb_sesso.SelectedValue = strSesso
        edTb_datnasc.NTSTextDB = strDatnasc
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggRiclassif_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggRiclassif.ItemClick
    Try
      If oCleAnaz.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128765041390532000, "Funzione utilizzabile solo in modifica di anagrafica ditta"))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128647246110312500, "Confermare l'aggiornamento dei collegamenti Riclassificazione?")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor

      oCleAnaz.AggRiclassif()
      oApp.MsgBoxInfo(oApp.Tr(Me, 128647247616718750, "Aggiornamento collegamenti Riclassificazione terminato."))

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edTb_escomp_NTSZoomGest(ByVal sender As Object, ByRef e As NTSEventArgs) Handles edTb_escomp.NTSZoomGest
    e.ZoomHandled = True
  End Sub

  Public Overridable Sub edTb_escompp_NTSZoomGest(ByVal sender As Object, ByRef e As NTSEventArgs) Handles edTb_escompp.NTSZoomGest
    e.ZoomHandled = True
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbAn_persfg_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_azpersfg.SelectedValueChanged
    Try

      If cbTb_azpersfg.SelectedValue.ToString = "G" Then
        edTb_azcognome.Text = ""
        edTb_aznome.Text = ""
        cbTb_sesso.SelectedValue = "S"

        edTb_azcognome.Enabled = False
        edTb_aznome.Enabled = False
        cbTb_sesso.Enabled = False
      Else
        If cbTb_sesso.SelectedValue = "S" Then cbTb_sesso.SelectedValue = "M"
        GctlSetVisEnab(edTb_azcognome, False)
        GctlSetVisEnab(edTb_aznome, False)
        GctlSetVisEnab(cbTb_sesso, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdAltriIndir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAltriIndir.Click
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim frmdesg As FRM__DESD = Nothing
    Dim oCallParamsTmp As New CLE__CLDP
    Try
      frmdesg = CType(NTSNewFormModal("FRM__DESD"), FRM__DESD)
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(dsAnaz.Tables("ANAZUL").Clone())
      For i = 0 To dsAnaz.Tables("ANAZUL").Rows.Count - 1
        If NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestdomf And _
            NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestsedel And _
            NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestresan And _
            NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestcorr Then
          ds.Tables("ANAZUL").ImportRow(dsAnaz.Tables("ANAZUL").Rows(i))
          dsAnaz.Tables("ANAZUL").Rows(i).Delete()
        End If
      Next
      dsAnaz.Tables("ANAZUL").AcceptChanges()

      oCallParamsTmp.strParam = strCallParamsDestDiv
      oCallParamsTmp.strPar1 = cmdAltriIndir.Text
      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleAnaz, ds, 0)
      If NTSCInt(dsAnaz.Tables("TABANAZ").Rows(0)!tb_azcodanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("ANAZUL").Rows.Count - 1
        If ds.Tables("ANAZUL").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("ANAZUL").Rows(i)!ul_coddest) > 0 Then
            dsAnaz.Tables("ANAZUL").ImportRow(ds.Tables("ANAZUL").Rows(i))
          Else
            ds.Tables("ANAZUL").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsAnaz.Tables("ANAZUL").AcceptChanges()
      oCleAnaz.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If dsAnaz.Tables("TABANAZ").Rows(0).RowState = DataRowState.Unchanged Then dsAnaz.Tables("TABANAZ").Rows(0).SetModified()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdSelImmagine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelImmagine.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      ApriFile()
      '--------------------------------------------------------------------------------------------------------------
      If edTb_imagename.Text.Trim <> "" Then oCleAnaz.bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri(ByVal bNew As Boolean) As Boolean
    Dim frmNew As FRM__NUOD = Nothing
    Try

      If bNew Then
        'NUOVO
        frmNew = CType(NTSNewFormModal("FRM__NUOD"), FRM__NUOD)
        frmNew.Init(oMenu, Nothing, DittaCorrente)
        frmNew.oCleAnaz = oCleAnaz
        frmNew.ShowDialog()
        If frmNew.bOk = False Then Return False 'annullato
        DittaCorrente = frmNew.edCodditt.Text.ToUpper.Trim
      End If     'If bNew Then

      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      Me.Cursor = Cursors.WaitCursor
      If Not oCleAnaz.Apri(DittaCorrente, bNew, dsAnaz) Then
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        Return False
      End If

      If bNew Then
        'NUOVO DA LEAD
        If Not oCleAnaz.NuovaDitta(DittaCorrente, NTSCInt(frmNew.edAnagen.Text), dsAnaz.Tables("TABANAZ").Rows(0)) Then
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
          Return False
        End If
      End If

      dcAnaz.DataSource = dsAnaz.Tables("TABANAZ")
      dsAnaz.AcceptChanges()

      dcAnaz.ResetBindings(False)
      dcAnaz.MoveFirst()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      If bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128642330015312500, "Attenzione: per avviare la procedura guidata di creazione ditta occorre:" & vbCrLf & "- verificare l'impostazione del flag 'Professionista' in 'Persona fisica/giuridica'"))
        SetStato(1)
      Else
        SetStato(2)
      End If

      CaricaColonneUnbound(CType(dcAnaz.Current, DataRowView).Row)

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      If bNew Then
        oCleAnaz.bHasChanges = True
        Me.GctlApplicaDefaultValue()
      End If

      edTb_azrags1.Focus()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmNew Is Nothing Then frmNew.Dispose()
      frmNew = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function ApriFile() As Boolean
    Dim nPosSep As Integer = 0
    Dim strNomeFile As String = ""
    Dim strPathFile As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la Common Dialog sulla cartella delle immagini
      '--------------------------------------------------------------------------------------------------------------
      OpenFileDialog1.CheckFileExists = True
      OpenFileDialog1.ShowReadOnly = False
      OpenFileDialog1.ShowHelp = False
      OpenFileDialog1.DefaultExt = "jpg"
      OpenFileDialog1.Title = "Selezione immagine"
      OpenFileDialog1.Filter = "Images (*.BMP;*.JPG;*.JPEG;*.PNG)|*.BMP;*.JPG;*.JPEG;*.PNG|All files (*.*)|*.*"
      OpenFileDialog1.InitialDirectory = oApp.ImgDir
      OpenFileDialog1.FileName = ""
      OpenFileDialog1.oMenu = oMenu
      OpenFileDialog1.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If OpenFileDialog1.FileName.Trim = "" Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Dim info As New FileInfo(OpenFileDialog1.FileName)
      If NTSCDec(info.Length) > 500000 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130143825578709539, "Attenzione!" & vbCrLf & _
          "Il file selezionato supera i 500 KB." & vbCrLf & _
          "Selezionare un file con minori dimensioni."))
        Return False
      End If
      Dim b As Image = System.Drawing.Bitmap.FromFile(OpenFileDialog1.FileName)
      If (NTSCInt(b.Width) <> 320) Or (NTSCInt(b.Height) <> 200) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130143828133651395, "Attenzione!" & vbCrLf & _
          "Le dimensioni del file selezionato NON sono:" & vbCrLf & _
          " . Altezza: 200" & vbCrLf & _
          " . Larghezza: 320" & vbCrLf & _
          "L'immagine, nei reports, potrebbe essere visualizzata non correttamente."))
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Cerca l'ultimo simbolo \ per dividere il path dal nome del file
      '--- ATTENZIONE il nome del file non deve contenere il simbolo "\"
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To Len(OpenFileDialog1.FileName)
        If Mid(OpenFileDialog1.FileName, i, 1) = "\" Then nPosSep = i
      Next
      If nPosSep > 0 Then
        strNomeFile = Microsoft.VisualBasic.Mid(OpenFileDialog1.FileName, nPosSep + 1)
        strPathFile = Microsoft.VisualBasic.Left(OpenFileDialog1.FileName, nPosSep)
      Else
        strNomeFile = OpenFileDialog1.FileName
      End If
      '--------------------------------------------------------------------------------------------------------------

      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'immagine selezionata non è nella cartella delle immagini
      '--- allora la copio nella cartella delle immagini
      '--------------------------------------------------------------------------------------------------------------
      If Not Directory.Exists(oApp.ImgDir) Then Directory.CreateDirectory(oApp.ImgDir)
      If strPathFile.ToUpper <> oApp.ImgDir.ToUpper Then
        If Not File.Exists(oApp.ImgDir & "\" & strNomeFile) Then
          Try
            FileCopy(OpenFileDialog1.FileName, oApp.ImgDir & "\" & strNomeFile)
          Catch ex As Exception
            Exit Function
          End Try
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Inserisce nel TextBox relativo il file immagine selezionato
      '--- Lo inserisco dopo aver ricopiato l'immagine se no da msg la BeforeColUpdate
      '--------------------------------------------------------------------------------------------------------------
      edTb_imagename.NTSTextDB = strNomeFile
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edTb_azcell.NTSDbField = "TABANAZ.tb_azcell"
      ckTb_azomocodice.NTSText.NTSDbField = "TABANAZ.tb_azomocodice"
      cbTb_azusaem.NTSDbField = "TABANAZ.tb_azusaem"
      edTb_azemail.NTSDbField = "TABANAZ.tb_azemail"
      edTb_azpiva.NTSDbField = "TABANAZ.tb_azpiva"
      edTb_azfaxtlx.NTSDbField = "TABANAZ.tb_azfaxtlx"
      edTb_aztelef.NTSDbField = "TABANAZ.tb_aztelef"
      edTb_azcodf.NTSDbField = "TABANAZ.tb_azcodf"
      edTb_azstatofed.NTSDbField = "TABANAZ.tb_azstatofed"
      edTb_azcodcomu.NTSDbField = "TABANAZ.tb_azcodcomu"
      edTb_azcitta.NTSDbField = "TABANAZ.tb_azcitta"
      edTb_azindir.NTSDbField = "TABANAZ.tb_azindir"
      edTb_azstato.NTSDbField = "TABANAZ.tb_azstato"
      edTb_azcap.NTSDbField = "TABANAZ.tb_azcap"
      edTb_azprov.NTSDbField = "TABANAZ.tb_azprov"
      edTb_aztitolo.NTSDbField = "TABANAZ.tb_aztitolo"
      edTb_azcognome.NTSDbField = "TABANAZ.tb_azcognome"
      edTb_aznome.NTSDbField = "TABANAZ.tb_aznome"
      cbTb_sesso.NTSDbField = "TABANAZ.tb_sesso"
      edTb_datnasc.NTSDbField = "TABANAZ.tb_datnasc"
      edTb_pronasc.NTSDbField = "TABANAZ.tb_pronasc"
      edTb_locnasc.NTSDbField = "TABANAZ.tb_locnasc"
      edTb_azcodcomn.NTSDbField = "TABANAZ.tb_azcodcomn"
      edTb_azstanasc.NTSDbField = "TABANAZ.tb_azstanasc"
      ckTb_azcondom.NTSText.NTSDbField = "TABANAZ.tb_azcondom"
      ckTb_azsoggresi.NTSText.NTSDbField = "TABANAZ.tb_azsoggresi"
      ckTb_azprofes.NTSText.NTSDbField = "TABANAZ.tb_azprofes"
      edTb_dtulst.NTSDbField = "TABANAZ.tb_dtulst"
      edTb_dtulap.NTSDbField = "TABANAZ.tb_dtulap"
      edTb_azcodcc.NTSDbField = "TABANAZ.tb_azcodcc"
      edTb_azrifriba.NTSDbField = "TABANAZ.tb_azrifriba"
      edTb_azcodcab.NTSDbField = "TABANAZ.tb_azcodcab"
      edTb_azcodabi.NTSDbField = "TABANAZ.tb_azcodabi"
      edXx_nflogo.NTSDbField = "TABANAZ.xx_nflogo"
      edTb_azwebpwd.NTSDbField = "TABANAZ.tb_azwebpwd"
      edTb_azwebsite.NTSDbField = "TABANAZ.tb_azwebsite"
      edTb_azwebuid.NTSDbField = "TABANAZ.tb_azwebuid"
      edTb_latitud.NTSDbField = "TABANAZ.tb_latitud"
      edTb_longitud.NTSDbField = "TABANAZ.tb_longitud"
      edTb_azcodrtac.NTSDbField = "TABANAZ.tb_azcodrtac"
      cbTb_azdoppes.NTSDbField = "TABANAZ.tb_azdoppes"
      cbTb_azgestscad.NTSDbField = "TABANAZ.tb_azgestscad"
      cbTb_ventil.NTSDbField = "TABANAZ.tb_ventil"
      edTb_escomp.NTSDbField = "TABANAZ.tb_escomp"
      edTb_escompp.NTSDbField = "TABANAZ.tb_escompp"
      edTb_pgullgp.NTSDbField = "TABANAZ.tb_pgullgp"
      edTb_pravlgp.NTSDbField = "TABANAZ.tb_pravlgp"
      edTb_prdalgp.NTSDbField = "TABANAZ.tb_prdalgp"
      edTb_riullgp.NTSDbField = "TABANAZ.tb_riullgp"
      edTb_rgullgp.NTSDbField = "TABANAZ.tb_rgullgp"
      edTb_dtullgp.NTSDbField = "TABANAZ.tb_dtullgp"
      edTb_pgullg.NTSDbField = "TABANAZ.tb_pgullg"
      edTb_pravlg.NTSDbField = "TABANAZ.tb_pravlg"
      edTb_prdalg.NTSDbField = "TABANAZ.tb_prdalg"
      edTb_riullg.NTSDbField = "TABANAZ.tb_riullg"
      edTb_rgulel.NTSDbField = "TABANAZ.tb_rgulel"
      edTb_rgullg.NTSDbField = "TABANAZ.tb_rgullg"
      edTb_dtulel.NTSDbField = "TABANAZ.tb_dtulel"
      edTb_dtullg.NTSDbField = "TABANAZ.tb_dtullg"
      edTb_dtulliqtri.NTSDbField = "TABANAZ.tb_dtulliqtri"
      edTb_dtulliq.NTSDbField = "TABANAZ.tb_dtulliq"
      edTb_uffiva.NTSDbField = "TABANAZ.tb_uffiva"
      edTb_azrags1.NTSDbField = "TABANAZ.tb_azrags1"
      edTb_azrags2.NTSDbField = "TABANAZ.tb_azrags2"
      cbTb_azpersfg.NTSDbField = "TABANAZ.tb_azpersfg"

      lbXx_azstato.NTSDbField = "TABANAZ.xx_azstato"
      lbXx_azcodcomu.NTSDbField = "TABANAZ.xx_azcodcomu"
      lbXx_azcodcomn.NTSDbField = "TABANAZ.xx_azcodcomn"
      lbXx_azstanasc.NTSDbField = "TABANAZ.xx_azstanasc"
      edXx_nflogo.NTSDbField = "TABANAZ.xx_nflogo"
      lbXx_escomp.NTSDbField = "TABANAZ.xx_escomp"
      lbXx_escompp.NTSDbField = "TABANAZ.xx_escompp"

      lbXx_azcodabi.NTSDbField = "TABANAZ.xx_azcodabi"
      lbXx_azcodcab.NTSDbField = "TABANAZ.xx_azcodcab"
      lbXx_azcodrtac.NTSDbField = "TABANAZ.xx_azcodrtac"
      edTb_azcodcaf.NTSDbField = "TABANAZ.tb_azcodcaf"
      lbXx_azcodcaf.NTSDbField = "TABANAZ.xx_azcodcaf"


      edTb_imagename.NTSDbField = "TABANAZ.tb_imagename"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcAnaz, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaColonneUnbound(ByRef dtrIn As DataRow)
    Try
      Me.Cursor = Cursors.WaitCursor

      If Not oCleAnaz.CaricaColonneUnbound(dtrIn) Then
        Me.Close()
        Return
      End If
      If dtrIn.RowState <> DataRowState.Added Then dtrIn.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttPersfg As New DataTable()
      dttPersfg.Columns.Add("cod", GetType(String))
      dttPersfg.Columns.Add("val", GetType(String))
      dttPersfg.Rows.Add(New Object() {"F", "Persona fisica"})
      dttPersfg.Rows.Add(New Object() {"G", "Persona giuridica"})
      dttPersfg.AcceptChanges()
      cbTb_azpersfg.DataSource = dttPersfg
      cbTb_azpersfg.ValueMember = "cod"
      cbTb_azpersfg.DisplayMember = "val"

      Dim dttSesso As New DataTable()
      dttSesso.Columns.Add("cod", GetType(String))
      dttSesso.Columns.Add("val", GetType(String))
      dttSesso.Rows.Add(New Object() {"M", "Maschio"})
      dttSesso.Rows.Add(New Object() {"F", "Femmina"})
      dttSesso.Rows.Add(New Object() {"S", "Pers.Giuridica"})
      dttSesso.AcceptChanges()
      cbTb_sesso.DataSource = dttSesso
      cbTb_sesso.ValueMember = "cod"
      cbTb_sesso.DisplayMember = "val"

      Dim dttTipoSend As New DataTable()
      dttTipoSend.Columns.Add("cod", GetType(String))
      dttTipoSend.Columns.Add("val", GetType(String))
      dttTipoSend.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipoSend.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      'dttTipoSend.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"})
      dttTipoSend.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipoSend.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipoSend.Rows.Add(New Object() {"H", "HylaFAX"})
      dttTipoSend.AcceptChanges()
      cbTb_azusaem.DataSource = dttTipoSend
      cbTb_azusaem.ValueMember = "cod"
      cbTb_azusaem.DisplayMember = "val"

      Dim dttStatus As New DataTable()
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {"N", "Corrente"})
      dttStatus.Rows.Add(New Object() {"S", "Doppio esercizio"})
      dttStatus.Rows.Add(New Object() {"C", "Effettuata chiusura"})
      dttStatus.AcceptChanges()
      cbTb_azdoppes.DataSource = dttStatus
      cbTb_azdoppes.ValueMember = "cod"
      cbTb_azdoppes.DisplayMember = "val"

      Dim dttGestpart As New DataTable()
      dttGestpart.Columns.Add("cod", GetType(String))
      dttGestpart.Columns.Add("val", GetType(String))
      dttGestpart.Rows.Add(New Object() {"S", "Sia clienti che fornitori"})
      dttGestpart.Rows.Add(New Object() {"C", "Solo clienti"})
      dttGestpart.Rows.Add(New Object() {"F", "Solo fornitori"})
      dttGestpart.Rows.Add(New Object() {"N", "No"})
      dttGestpart.AcceptChanges()
      cbTb_ventil.DataSource = dttGestpart
      cbTb_ventil.ValueMember = "cod"
      cbTb_ventil.DisplayMember = "val"

      Dim dttGestscad As New DataTable()
      dttGestscad.Columns.Add("cod", GetType(String))
      dttGestscad.Columns.Add("val", GetType(String))
      dttGestscad.Rows.Add(New Object() {"S", "Sia clienti che fornitori"})
      dttGestscad.Rows.Add(New Object() {"C", "Solo clienti"})
      dttGestscad.Rows.Add(New Object() {"F", "Solo fornitori"})
      dttGestscad.Rows.Add(New Object() {"N", "No"})
      dttGestscad.AcceptChanges()
      cbTb_azgestscad.DataSource = dttGestscad
      cbTb_azgestscad.ValueMember = "cod"
      cbTb_azgestscad.DisplayMember = "val"

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
          'Case "ALFPROT_1:" : edAlfpro1.Text = IIf(strTmp(i).Substring(10) = "§", edAlfdoc.Text, strTmp(i).Substring(10)).ToString

        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiDatiDitta() As Boolean
    Dim bDbMultiDitta As Boolean = False
    Dim strTmp As String = ""
    Dim strDitta As String = ""
    Try
      oCleAnaz.bAnagen = CBool((oApp.ActKey.ModuliExtAzienda And CLN__STD.bsModExtANG))
      If oCleAnaz.IsDbNoDitte() Then

      Else
        '-------------------------------------------------
        'zoom ditte

        'non posso usare la riga sotto: farebbe cambiare la ditta in uso per i nuovi programmi aperti ...
        'DittaCorrente = oMenu.CambioDitta(Nothing, DittaCorrente, "BN__ANAZ", "", 0, 0, 0, 0, 0, 0, False, False)
        Dim oParam As New CLE__CLDP
        oParam.strNomProg = "BN__ANAZ"
        oParam.dPar1 = 0
        oParam.dPar2 = 0
        oParam.dPar3 = 0
        oParam.dPar4 = 0
        oParam.dPar5 = 0
        oParam.strPar1 = "0"
        oMenu.RunZoomNet("NTSInformatica", "FRM__HLDI", "", "BN__HLDI", "Zoom ditte", DittaCorrente, CObj(oParam))
        DittaCorrente = oParam.Ditta
        oParam = Nothing

        If DittaCorrente = "" Then Return False
      End If

      Me.Text = oApp.Tr(Me, 128642120117500000, "ANAGRAFICA DITTA")

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function RecordAttivita(ByVal nModale As Integer, ByVal nAnnoIva As Integer) As Boolean
    'se nModale è diverso da 0, apre direttamente o la modale attività (1) iva o la modale regsitri iva (2)
    'anno iva (se 0 viene ricalcolato)
    Dim frmAniv As FRM__ANIV_F = Nothing
    Dim dtrT() As DataRow = Nothing
    Try
      frmAniv = CType(NTSNewFormModal("FRM__ANIV_F"), FRM__ANIV_F)
      frmAniv.Init(oMenu, Nothing, DittaCorrente)
      frmAniv.InitEntity(oCleAnaz)

      '--------------------
      'Controlla se è stato indicato l'esercizio contabile corrente
      'perché nella modale relativa alla proposta dell'anno va a prendere
      'l'anno della data finale di TTTABESCO con tb_codesco = edEscomp.Text

      If nAnnoIva = 0 Then

        If NTSCInt(edTb_escomp.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128647593686406250, "Indicare l'esercizio contabile corrente prima di passare alla gestione dei 'Dati Iva per anno'."))
          Return False
        End If

        dtrT = dsAnaz.Tables("TABESCO").Select("tb_codesco = " & NTSCInt(edTb_escomp.Text).ToString)
        If dtrT.Length = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128647594774687500, "Inserire i dati negli 'Esercizi contabili' il codice esercizio |" & edTb_escomp.Text & "| prima di passare alla gestione dei 'Dati Iva per anno'."))
          Return False
        Else
          frmAniv.nAnivEscomp = NTSCDate(dtrT(0)!tb_dtfieser).Year
        End If
        frmAniv.bSceglianno = True
      Else
        frmAniv.nAnivEscomp = nAnnoIva
        frmAniv.bSceglianno = False
      End If

      frmAniv.nModale = nModale
      frmAniv.ShowDialog()
      oCleAnaz.bHasChanges = True

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmAniv Is Nothing Then frmAniv.Dispose()
      frmAniv = Nothing
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      If tsAnaz.Visible = False Then Return True

      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnaz.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128271029890350656, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.No Then
          oCleAnaz.Ripristina()
        End If
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Me.Cursor = Cursors.WaitCursor
          If Not oCleAnaz.Salva(False) Then Return False
          oCleAnaz.bNew = False
          SetStato(2)
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nStato As Integer)
    Try
      If nStato = 0 Then
        pnTop.Visible = False
        tsAnaz.Visible = False
        GctlSetVisEnab(tlbApri, False)
        tlbZoom.Enabled = False
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbCalcolaCodFisc.Enabled = False
        tlbRitornaCodFisc.Enabled = False
        tlbDatiAggCont.Enabled = False
        tlbEscomp.Enabled = False
        tlbIva.Enabled = False
        tlbAggRiclassif.Enabled = False

        ckTb_azprofes.Enabled = False
      Else
        tsAnaz.SelectedTabPageIndex = 0
        GctlSetVisEnab(pnTop, True)
        GctlSetVisEnab(tsAnaz, True)
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbCalcolaCodFisc, False)
        GctlSetVisEnab(tlbRitornaCodFisc, False)
        GctlSetVisEnab(tlbDatiAggCont, False)
        GctlSetVisEnab(tlbEscomp, False)
        GctlSetVisEnab(tlbIva, False)

        If nStato = 1 Then
          '----------------------------------
          'Se si è in inserimento di una nuova ditta
          'ed è stato indicato un codice Anagrafica Generale
          'e non esistono ditte collegate a tale codice
          'abilita il CheckBox "Professionista" (TABANAZ.tb_azprofes)
          'altrimenti lo lascia disabilitato
          If NTSCInt(dsAnaz.Tables("TABANAZ").Rows(0)!tb_azcodanag) = 0 Then
            GctlSetVisEnab(ckTb_azprofes, False)
          Else
            If Not oCleAnaz.CheckDitteAnagen(NTSCInt(dsAnaz.Tables("TABANAZ").Rows(0)!tb_azcodanag)) Then
              GctlSetVisEnab(ckTb_azprofes, False)
            End If
          End If
        Else
          'nStato = 2
          GctlSetVisEnab(tlbAggRiclassif, False)
          GctlSetVisEnab(tlbCancella, False)
        End If

      End If

      edXx_codditt.Text = DittaCorrente

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function VariazioneDati(ByVal strTipovar As String) As Boolean
    Dim strDataTmp As String = ""
    Dim strTmp As String = ""
    Try
      '--------------------
      '"N" = Data variazione natura giuridica          tabanaz.tb_dtulvng
      '"A" = Data ultima variazione attività           tabanaz.tb_dtulvat
      '"C" = Data ultima variazione capitale sociale   tabanaz.tb_dtulvcs

      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128644103911250000, "Indicare la nuova data di variazione"), "")
      If strTmp = "" Then Return False
      If Not IsDate(strTmp) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644104456250000, "La data inserita non è corretta"))
        Return False
      End If

      If Not oCleAnaz.VariazioneDate(strTipovar, strTmp) Then Return False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Sub cmdDelImmagine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelImmagine.Click
    Try
      edTb_imagename.NTSTextDB = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class