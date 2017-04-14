Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEBANC

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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
  Public oCleBanc As CLEVEBANC
  Public dsBanc As DataSet
  Public dcBanc As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public dttDitta As New DataTable
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnBanc As NTSInformatica.NTSPanel
  Public WithEvents lbTb_codbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_codbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_desbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_desbanc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_conbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_conbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_abi As NTSInformatica.NTSLabel
  Public WithEvents edTb_abi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_cab As NTSInformatica.NTSLabel
  Public WithEvents edTb_cab As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edTb_csbf As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_rifriba As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_csbf As NTSInformatica.NTSLabel
  Public WithEvents lbTb_rifriba As NTSInformatica.NTSLabel
  Public WithEvents edTb_fido As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tiprap As NTSInformatica.NTSLabel
  Public WithEvents cbTb_tiprap As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_iban As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desbanc3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desbanc2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_iban As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desbanc3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desbanc2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_ritfisc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_fido As NTSInformatica.NTSLabel
  Public WithEvents edTb_ritfisc As NTSInformatica.NTSTextBoxNum
  Public WithEvents tsBanc As NTSInformatica.NTSTabControl
  Public WithEvents tsBancPage1 As NTSInformatica.NTSTabPage
  Public WithEvents tsBancPage2 As NTSInformatica.NTSTabPage
  Public WithEvents tsBancPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnBancPage1 As NTSInformatica.NTSPanel
  Public WithEvents pnBancPage0 As NTSInformatica.NTSPanel
  Public WithEvents edTb_tenutcc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tenutcc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_opcompr As NTSInformatica.NTSLabel
  Public WithEvents edTb_opcompr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_impboll As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_impboll As NTSInformatica.NTSLabel
  Public WithEvents edTb_speseop As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_speseop As NTSInformatica.NTSLabel
  Public WithEvents edTb_spechiusann As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_spechiusann As NTSInformatica.NTSLabel
  Public WithEvents lbTb_maxscop2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_maxscop2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_maxscop As NTSInformatica.NTSLabel
  Public WithEvents edTb_maxscop As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnBancPage2 As NTSInformatica.NTSPanel
  Public WithEvents edTb_scaglconsm2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_scaglconsm2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_scaglconsm1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_scaglconsm1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_scaglcms As NTSInformatica.NTSLabel
  Public WithEvents edTb_scaglcms As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnBancPage3 As NTSInformatica.NTSPanel
  Public WithEvents lbTb_saldovalulad As NTSInformatica.NTSLabel
  Public WithEvents edTb_saldovalulad As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_saldovalulac As NTSInformatica.NTSLabel
  Public WithEvents edTb_saldovalulac As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_saldovalulch As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_saldoulchiu As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_addimpa As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_addimpa As NTSInformatica.NTSLabel
  Public WithEvents lbTb_accinat As NTSInformatica.NTSLabel
  Public WithEvents cbTb_accinat As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_dtulchiu As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dtulacint As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dtuladint As NTSInformatica.NTSLabel
  Public WithEvents lbTb_saldovalulch As NTSInformatica.NTSLabel
  Public WithEvents lbTb_saldoulchiu As NTSInformatica.NTSLabel
  Public WithEvents lbTb_numaulchiu As NTSInformatica.NTSLabel
  Public WithEvents edTb_numdulchiu As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_numdulchiu As NTSInformatica.NTSLabel
  Public WithEvents edTb_numaulchiu As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_dtulchiu As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_dtuladint As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_dtulacint As NTSInformatica.NTSTextBoxData
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCondizioni As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbTassi As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmIbanItalia As NTSInformatica.NTSGroupBox
  Public WithEvents edTb_cin As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_prefiban As NTSInformatica.NTSLabel
  Public WithEvents edTb_prefiban As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_tiporapdf As NTSInformatica.NTSLabel
  Public WithEvents edTb_tiporapdf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_codbdoc As NTSInformatica.NTSLabel
  Public WithEvents edTb_codbdoc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_saldoanticipaz As NTSInformatica.NTSLabel
  Public WithEvents edTb_saldoanticipaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_conanticip As NTSInformatica.NTSLabel
  Public WithEvents edTb_conanticip As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conanticip As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conbanc As NTSInformatica.NTSLabel
  Public WithEvents tsBancPage0 As NTSInformatica.NTSTabPage
  Public WithEvents edTb_codcuc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_codcuc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_swift As NTSInformatica.NTSLabel
  Public WithEvents edTb_swift As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_rididcred As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_rididcred As NTSInformatica.NTSLabel
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem

#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non Ã¨ per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      Select Case e.TipoEvento
        Case "AggiornaColoreAbiCab" : ColoraCampoAbiCab()
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
    Dim strErr As String = ""
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEBANC", "BEVEBANC", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128171550173281250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleBanc = CType(objTmp, CLEVEBANC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEBANC", strRemoteServer, strRemotePort)
    AddHandler oCleBanc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleBanc.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEBANC))
    Me.edTb_codbanc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codbanc = New NTSInformatica.NTSLabel
    Me.edTb_desbanc = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_desbanc = New NTSInformatica.NTSLabel
    Me.lbTb_conbanc = New NTSInformatica.NTSLabel
    Me.edTb_conbanc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cab = New NTSInformatica.NTSLabel
    Me.edTb_cab = New NTSInformatica.NTSTextBoxNum
    Me.edTb_abi = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_abi = New NTSInformatica.NTSLabel
    Me.lbTb_rifriba = New NTSInformatica.NTSLabel
    Me.lbTb_csbf = New NTSInformatica.NTSLabel
    Me.edTb_rifriba = New NTSInformatica.NTSTextBoxStr
    Me.edTb_csbf = New NTSInformatica.NTSTextBoxStr
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
    Me.tlbCambioDitta = New NTSInformatica.NTSBarButtonItem
    Me.tlbCondizioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbTassi = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnBanc = New NTSInformatica.NTSPanel
    Me.lbXx_conanticip = New NTSInformatica.NTSLabel
    Me.lbXx_conbanc = New NTSInformatica.NTSLabel
    Me.lbTb_conanticip = New NTSInformatica.NTSLabel
    Me.edTb_conanticip = New NTSInformatica.NTSTextBoxNum
    Me.tsBanc = New NTSInformatica.NTSTabControl
    Me.tsBancPage0 = New NTSInformatica.NTSTabPage
    Me.pnBancPage0 = New NTSInformatica.NTSPanel
    Me.fmIbanItalia = New NTSInformatica.NTSGroupBox
    Me.edTb_rididcred = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_rididcred = New NTSInformatica.NTSLabel
    Me.edTb_codcuc = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_codcuc = New NTSInformatica.NTSLabel
    Me.lbTb_swift = New NTSInformatica.NTSLabel
    Me.edTb_swift = New NTSInformatica.NTSTextBoxStr
    Me.edTb_cin = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_prefiban = New NTSInformatica.NTSLabel
    Me.edTb_prefiban = New NTSInformatica.NTSTextBoxStr
    Me.edTb_fido = New NTSInformatica.NTSTextBoxNum
    Me.edTb_ritfisc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_fido = New NTSInformatica.NTSLabel
    Me.lbTb_ritfisc = New NTSInformatica.NTSLabel
    Me.edTb_desbanc2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_iban = New NTSInformatica.NTSTextBoxStr
    Me.edTb_desbanc3 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_desbanc2 = New NTSInformatica.NTSLabel
    Me.lbTb_tiprap = New NTSInformatica.NTSLabel
    Me.lbTb_desbanc3 = New NTSInformatica.NTSLabel
    Me.cbTb_tiprap = New NTSInformatica.NTSComboBox
    Me.lbTb_iban = New NTSInformatica.NTSLabel
    Me.tsBancPage1 = New NTSInformatica.NTSTabPage
    Me.pnBancPage1 = New NTSInformatica.NTSPanel
    Me.lbTb_maxscop2 = New NTSInformatica.NTSLabel
    Me.edTb_maxscop2 = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.lbTb_maxscop = New NTSInformatica.NTSLabel
    Me.edTb_maxscop = New NTSInformatica.NTSTextBoxNum
    Me.edTb_impboll = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codbdoc = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_impboll = New NTSInformatica.NTSLabel
    Me.edTb_speseop = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_speseop = New NTSInformatica.NTSLabel
    Me.edTb_spechiusann = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codbdoc = New NTSInformatica.NTSLabel
    Me.lbTb_spechiusann = New NTSInformatica.NTSLabel
    Me.edTb_tenutcc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_tiporapdf = New NTSInformatica.NTSLabel
    Me.lbTb_tenutcc = New NTSInformatica.NTSLabel
    Me.lbTb_opcompr = New NTSInformatica.NTSLabel
    Me.edTb_opcompr = New NTSInformatica.NTSTextBoxNum
    Me.edTb_tiporapdf = New NTSInformatica.NTSTextBoxStr
    Me.tsBancPage2 = New NTSInformatica.NTSTabPage
    Me.pnBancPage2 = New NTSInformatica.NTSPanel
    Me.cbTb_addimpa = New NTSInformatica.NTSComboBox
    Me.lbTb_addimpa = New NTSInformatica.NTSLabel
    Me.lbTb_accinat = New NTSInformatica.NTSLabel
    Me.cbTb_accinat = New NTSInformatica.NTSComboBox
    Me.edTb_scaglconsm2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_scaglconsm2 = New NTSInformatica.NTSLabel
    Me.edTb_scaglconsm1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_scaglconsm1 = New NTSInformatica.NTSLabel
    Me.lbTb_scaglcms = New NTSInformatica.NTSLabel
    Me.edTb_scaglcms = New NTSInformatica.NTSTextBoxNum
    Me.tsBancPage3 = New NTSInformatica.NTSTabPage
    Me.pnBancPage3 = New NTSInformatica.NTSPanel
    Me.lbTb_saldoanticipaz = New NTSInformatica.NTSLabel
    Me.edTb_saldoanticipaz = New NTSInformatica.NTSTextBoxNum
    Me.edTb_dtulchiu = New NTSInformatica.NTSTextBoxData
    Me.edTb_dtuladint = New NTSInformatica.NTSTextBoxData
    Me.edTb_dtulacint = New NTSInformatica.NTSTextBoxData
    Me.edTb_numdulchiu = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_numdulchiu = New NTSInformatica.NTSLabel
    Me.edTb_numaulchiu = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_numaulchiu = New NTSInformatica.NTSLabel
    Me.lbTb_dtuladint = New NTSInformatica.NTSLabel
    Me.lbTb_saldovalulch = New NTSInformatica.NTSLabel
    Me.lbTb_saldoulchiu = New NTSInformatica.NTSLabel
    Me.lbTb_dtulchiu = New NTSInformatica.NTSLabel
    Me.lbTb_dtulacint = New NTSInformatica.NTSLabel
    Me.lbTb_saldovalulad = New NTSInformatica.NTSLabel
    Me.edTb_saldovalulad = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_saldovalulac = New NTSInformatica.NTSLabel
    Me.edTb_saldovalulac = New NTSInformatica.NTSTextBoxNum
    Me.edTb_saldovalulch = New NTSInformatica.NTSTextBoxNum
    Me.edTb_saldoulchiu = New NTSInformatica.NTSTextBoxNum
    CType(Me.edTb_codbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_abi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rifriba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_csbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBanc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBanc.SuspendLayout()
    CType(Me.edTb_conanticip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsBanc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsBanc.SuspendLayout()
    Me.tsBancPage0.SuspendLayout()
    CType(Me.pnBancPage0, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBancPage0.SuspendLayout()
    CType(Me.fmIbanItalia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIbanItalia.SuspendLayout()
    CType(Me.edTb_rididcred.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcuc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_swift.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_prefiban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_fido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ritfisc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_iban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tiprap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsBancPage1.SuspendLayout()
    CType(Me.pnBancPage1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBancPage1.SuspendLayout()
    CType(Me.edTb_maxscop2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_maxscop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_impboll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codbdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_speseop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_spechiusann.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tenutcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_opcompr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tiporapdf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsBancPage2.SuspendLayout()
    CType(Me.pnBancPage2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBancPage2.SuspendLayout()
    CType(Me.cbTb_addimpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_accinat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_scaglconsm2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_scaglconsm1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_scaglcms.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsBancPage3.SuspendLayout()
    CType(Me.pnBancPage3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBancPage3.SuspendLayout()
    CType(Me.edTb_saldoanticipaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulchiu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtuladint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtulacint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_numdulchiu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_numaulchiu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_saldovalulad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_saldovalulac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_saldovalulch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_saldoulchiu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'edTb_codbanc
    '
    Me.edTb_codbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codbanc.EditValue = "0"
    Me.edTb_codbanc.Location = New System.Drawing.Point(91, 8)
    Me.edTb_codbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_codbanc.Name = "edTb_codbanc"
    Me.edTb_codbanc.NTSDbField = ""
    Me.edTb_codbanc.NTSFormat = "0"
    Me.edTb_codbanc.NTSForzaVisZoom = False
    Me.edTb_codbanc.NTSOldValue = ""
    Me.edTb_codbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codbanc.Properties.MaxLength = 65536
    Me.edTb_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codbanc.Size = New System.Drawing.Size(72, 20)
    Me.edTb_codbanc.TabIndex = 0
    '
    'lbTb_codbanc
    '
    Me.lbTb_codbanc.AutoSize = True
    Me.lbTb_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codbanc.Location = New System.Drawing.Point(13, 11)
    Me.lbTb_codbanc.Name = "lbTb_codbanc"
    Me.lbTb_codbanc.NTSDbField = ""
    Me.lbTb_codbanc.Size = New System.Drawing.Size(62, 13)
    Me.lbTb_codbanc.TabIndex = 1
    Me.lbTb_codbanc.Text = "Cod. Banca"
    Me.lbTb_codbanc.Tooltip = ""
    Me.lbTb_codbanc.UseMnemonic = False
    '
    'edTb_desbanc
    '
    Me.edTb_desbanc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_desbanc.Location = New System.Drawing.Point(273, 8)
    Me.edTb_desbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc.Name = "edTb_desbanc"
    Me.edTb_desbanc.NTSDbField = ""
    Me.edTb_desbanc.NTSForzaVisZoom = False
    Me.edTb_desbanc.NTSOldValue = ""
    Me.edTb_desbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc.Properties.MaxLength = 65536
    Me.edTb_desbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc.Size = New System.Drawing.Size(238, 20)
    Me.edTb_desbanc.TabIndex = 2
    '
    'lbTb_desbanc
    '
    Me.lbTb_desbanc.AutoSize = True
    Me.lbTb_desbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc.Location = New System.Drawing.Point(195, 11)
    Me.lbTb_desbanc.Name = "lbTb_desbanc"
    Me.lbTb_desbanc.NTSDbField = ""
    Me.lbTb_desbanc.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_desbanc.TabIndex = 3
    Me.lbTb_desbanc.Text = "Descr. Banca"
    Me.lbTb_desbanc.Tooltip = ""
    Me.lbTb_desbanc.UseMnemonic = False
    '
    'lbTb_conbanc
    '
    Me.lbTb_conbanc.AutoSize = True
    Me.lbTb_conbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conbanc.Location = New System.Drawing.Point(13, 40)
    Me.lbTb_conbanc.Name = "lbTb_conbanc"
    Me.lbTb_conbanc.NTSDbField = ""
    Me.lbTb_conbanc.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_conbanc.TabIndex = 5
    Me.lbTb_conbanc.Text = "Conto P.d.C."
    Me.lbTb_conbanc.Tooltip = ""
    Me.lbTb_conbanc.UseMnemonic = False
    '
    'edTb_conbanc
    '
    Me.edTb_conbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conbanc.EditValue = "0"
    Me.edTb_conbanc.Location = New System.Drawing.Point(165, 37)
    Me.edTb_conbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_conbanc.Name = "edTb_conbanc"
    Me.edTb_conbanc.NTSDbField = ""
    Me.edTb_conbanc.NTSFormat = "0"
    Me.edTb_conbanc.NTSForzaVisZoom = False
    Me.edTb_conbanc.NTSOldValue = ""
    Me.edTb_conbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conbanc.Properties.MaxLength = 65536
    Me.edTb_conbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conbanc.Size = New System.Drawing.Size(100, 20)
    Me.edTb_conbanc.TabIndex = 1
    '
    'lbTb_cab
    '
    Me.lbTb_cab.AutoSize = True
    Me.lbTb_cab.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cab.Location = New System.Drawing.Point(298, 23)
    Me.lbTb_cab.Name = "lbTb_cab"
    Me.lbTb_cab.NTSDbField = ""
    Me.lbTb_cab.Size = New System.Drawing.Size(53, 13)
    Me.lbTb_cab.TabIndex = 7
    Me.lbTb_cab.Text = "Cod. CAB"
    Me.lbTb_cab.Tooltip = ""
    Me.lbTb_cab.UseMnemonic = False
    '
    'edTb_cab
    '
    Me.edTb_cab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cab.EditValue = "0"
    Me.edTb_cab.Location = New System.Drawing.Point(399, 20)
    Me.edTb_cab.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_cab.Name = "edTb_cab"
    Me.edTb_cab.NTSDbField = ""
    Me.edTb_cab.NTSFormat = "0"
    Me.edTb_cab.NTSForzaVisZoom = False
    Me.edTb_cab.NTSOldValue = ""
    Me.edTb_cab.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cab.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cab.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cab.Properties.MaxLength = 65536
    Me.edTb_cab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cab.Size = New System.Drawing.Size(100, 20)
    Me.edTb_cab.TabIndex = 4
    '
    'edTb_abi
    '
    Me.edTb_abi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_abi.EditValue = "0"
    Me.edTb_abi.Location = New System.Drawing.Point(85, 20)
    Me.edTb_abi.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_abi.Name = "edTb_abi"
    Me.edTb_abi.NTSDbField = ""
    Me.edTb_abi.NTSFormat = "0"
    Me.edTb_abi.NTSForzaVisZoom = False
    Me.edTb_abi.NTSOldValue = ""
    Me.edTb_abi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_abi.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_abi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_abi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_abi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_abi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_abi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_abi.Properties.MaxLength = 65536
    Me.edTb_abi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_abi.Size = New System.Drawing.Size(99, 20)
    Me.edTb_abi.TabIndex = 3
    '
    'lbTb_abi
    '
    Me.lbTb_abi.AutoSize = True
    Me.lbTb_abi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_abi.Location = New System.Drawing.Point(7, 23)
    Me.lbTb_abi.Name = "lbTb_abi"
    Me.lbTb_abi.NTSDbField = ""
    Me.lbTb_abi.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_abi.TabIndex = 9
    Me.lbTb_abi.Text = "Cod. ABI"
    Me.lbTb_abi.Tooltip = ""
    Me.lbTb_abi.UseMnemonic = False
    '
    'lbTb_rifriba
    '
    Me.lbTb_rifriba.AutoSize = True
    Me.lbTb_rifriba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rifriba.Location = New System.Drawing.Point(304, 201)
    Me.lbTb_rifriba.Name = "lbTb_rifriba"
    Me.lbTb_rifriba.NTSDbField = ""
    Me.lbTb_rifriba.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_rifriba.TabIndex = 10
    Me.lbTb_rifriba.Text = "Cod. SIA"
    Me.lbTb_rifriba.Tooltip = ""
    Me.lbTb_rifriba.UseMnemonic = False
    '
    'lbTb_csbf
    '
    Me.lbTb_csbf.AutoSize = True
    Me.lbTb_csbf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_csbf.Location = New System.Drawing.Point(7, 46)
    Me.lbTb_csbf.Name = "lbTb_csbf"
    Me.lbTb_csbf.NTSDbField = ""
    Me.lbTb_csbf.Size = New System.Drawing.Size(68, 13)
    Me.lbTb_csbf.TabIndex = 11
    Me.lbTb_csbf.Text = "N° C/C / CIN"
    Me.lbTb_csbf.Tooltip = ""
    Me.lbTb_csbf.UseMnemonic = False
    '
    'edTb_rifriba
    '
    Me.edTb_rifriba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rifriba.Location = New System.Drawing.Point(405, 198)
    Me.edTb_rifriba.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_rifriba.Name = "edTb_rifriba"
    Me.edTb_rifriba.NTSDbField = ""
    Me.edTb_rifriba.NTSForzaVisZoom = False
    Me.edTb_rifriba.NTSOldValue = ""
    Me.edTb_rifriba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_rifriba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_rifriba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rifriba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rifriba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rifriba.Properties.MaxLength = 65536
    Me.edTb_rifriba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rifriba.Size = New System.Drawing.Size(100, 20)
    Me.edTb_rifriba.TabIndex = 12
    '
    'edTb_csbf
    '
    Me.edTb_csbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_csbf.Location = New System.Drawing.Point(85, 43)
    Me.edTb_csbf.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_csbf.Name = "edTb_csbf"
    Me.edTb_csbf.NTSDbField = ""
    Me.edTb_csbf.NTSForzaVisZoom = False
    Me.edTb_csbf.NTSOldValue = ""
    Me.edTb_csbf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_csbf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_csbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_csbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_csbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_csbf.Properties.MaxLength = 65536
    Me.edTb_csbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_csbf.Size = New System.Drawing.Size(100, 20)
    Me.edTb_csbf.TabIndex = 13
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbCambioDitta, Me.tlbCondizioni, Me.tlbTassi})
    Me.NtsBarManager1.MaxItemId = 18
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
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 9
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCondizioni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTassi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambio ditta"
    Me.tlbCambioDitta.Id = 15
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.Visible = True
    '
    'tlbCondizioni
    '
    Me.tlbCondizioni.Caption = "Condizioni per tipo di operazione"
    Me.tlbCondizioni.Id = 16
    Me.tlbCondizioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbCondizioni.Name = "tlbCondizioni"
    Me.tlbCondizioni.Visible = True
    '
    'tlbTassi
    '
    Me.tlbTassi.Caption = "Tassi applicati"
    Me.tlbTassi.Id = 17
    Me.tlbTassi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbTassi.Name = "tlbTassi"
    Me.tlbTassi.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 11
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 12
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnBanc
    '
    Me.pnBanc.AllowDrop = True
    Me.pnBanc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBanc.Appearance.Options.UseBackColor = True
    Me.pnBanc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBanc.Controls.Add(Me.lbXx_conanticip)
    Me.pnBanc.Controls.Add(Me.lbXx_conbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_conanticip)
    Me.pnBanc.Controls.Add(Me.edTb_conanticip)
    Me.pnBanc.Controls.Add(Me.tsBanc)
    Me.pnBanc.Controls.Add(Me.edTb_codbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_codbanc)
    Me.pnBanc.Controls.Add(Me.edTb_desbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_desbanc)
    Me.pnBanc.Controls.Add(Me.edTb_conbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_conbanc)
    Me.pnBanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBanc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBanc.Location = New System.Drawing.Point(0, 30)
    Me.pnBanc.Name = "pnBanc"
    Me.pnBanc.NTSActiveTrasparency = True
    Me.pnBanc.Size = New System.Drawing.Size(537, 407)
    Me.pnBanc.TabIndex = 10
    Me.pnBanc.Text = "pnBanc"
    '
    'lbXx_conanticip
    '
    Me.lbXx_conanticip.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conanticip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conanticip.Location = New System.Drawing.Point(273, 63)
    Me.lbXx_conanticip.Name = "lbXx_conanticip"
    Me.lbXx_conanticip.NTSDbField = ""
    Me.lbXx_conanticip.Size = New System.Drawing.Size(238, 20)
    Me.lbXx_conanticip.TabIndex = 37
    Me.lbXx_conanticip.Tooltip = ""
    Me.lbXx_conanticip.UseMnemonic = False
    '
    'lbXx_conbanc
    '
    Me.lbXx_conbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conbanc.Location = New System.Drawing.Point(273, 37)
    Me.lbXx_conbanc.Name = "lbXx_conbanc"
    Me.lbXx_conbanc.NTSDbField = ""
    Me.lbXx_conbanc.Size = New System.Drawing.Size(238, 20)
    Me.lbXx_conbanc.TabIndex = 36
    Me.lbXx_conbanc.Tooltip = ""
    Me.lbXx_conbanc.UseMnemonic = False
    '
    'lbTb_conanticip
    '
    Me.lbTb_conanticip.AutoSize = True
    Me.lbTb_conanticip.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conanticip.Location = New System.Drawing.Point(13, 66)
    Me.lbTb_conanticip.Name = "lbTb_conanticip"
    Me.lbTb_conanticip.NTSDbField = ""
    Me.lbTb_conanticip.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_conanticip.TabIndex = 34
    Me.lbTb_conanticip.Text = "Conto x anticipaz. su C/C"
    Me.lbTb_conanticip.Tooltip = ""
    Me.lbTb_conanticip.UseMnemonic = False
    '
    'edTb_conanticip
    '
    Me.edTb_conanticip.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conanticip.EditValue = "0"
    Me.edTb_conanticip.Location = New System.Drawing.Point(165, 63)
    Me.edTb_conanticip.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_conanticip.Name = "edTb_conanticip"
    Me.edTb_conanticip.NTSDbField = ""
    Me.edTb_conanticip.NTSFormat = "0"
    Me.edTb_conanticip.NTSForzaVisZoom = False
    Me.edTb_conanticip.NTSOldValue = ""
    Me.edTb_conanticip.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conanticip.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conanticip.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conanticip.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conanticip.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conanticip.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conanticip.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conanticip.Properties.MaxLength = 65536
    Me.edTb_conanticip.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conanticip.Size = New System.Drawing.Size(100, 20)
    Me.edTb_conanticip.TabIndex = 33
    '
    'tsBanc
    '
    Me.tsBanc.Location = New System.Drawing.Point(3, 96)
    Me.tsBanc.Name = "tsBanc"
    Me.tsBanc.SelectedTabPage = Me.tsBancPage0
    Me.tsBanc.Size = New System.Drawing.Size(534, 308)
    Me.tsBanc.TabIndex = 26
    Me.tsBanc.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tsBancPage0, Me.tsBancPage1, Me.tsBancPage2, Me.tsBancPage3})
    '
    'tsBancPage0
    '
    Me.tsBancPage0.AllowDrop = True
    Me.tsBancPage0.Controls.Add(Me.pnBancPage0)
    Me.tsBancPage0.Enable = True
    Me.tsBancPage0.Name = "tsBancPage0"
    Me.tsBancPage0.Size = New System.Drawing.Size(525, 278)
    Me.tsBancPage0.Text = "Dati generali"
    '
    'pnBancPage0
    '
    Me.pnBancPage0.AllowDrop = True
    Me.pnBancPage0.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBancPage0.Appearance.Options.UseBackColor = True
    Me.pnBancPage0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBancPage0.Controls.Add(Me.fmIbanItalia)
    Me.pnBancPage0.Controls.Add(Me.lbTb_rifriba)
    Me.pnBancPage0.Controls.Add(Me.edTb_rifriba)
    Me.pnBancPage0.Controls.Add(Me.edTb_fido)
    Me.pnBancPage0.Controls.Add(Me.edTb_ritfisc)
    Me.pnBancPage0.Controls.Add(Me.lbTb_fido)
    Me.pnBancPage0.Controls.Add(Me.lbTb_ritfisc)
    Me.pnBancPage0.Controls.Add(Me.edTb_desbanc2)
    Me.pnBancPage0.Controls.Add(Me.edTb_iban)
    Me.pnBancPage0.Controls.Add(Me.edTb_desbanc3)
    Me.pnBancPage0.Controls.Add(Me.lbTb_desbanc2)
    Me.pnBancPage0.Controls.Add(Me.lbTb_tiprap)
    Me.pnBancPage0.Controls.Add(Me.lbTb_desbanc3)
    Me.pnBancPage0.Controls.Add(Me.cbTb_tiprap)
    Me.pnBancPage0.Controls.Add(Me.lbTb_iban)
    Me.pnBancPage0.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBancPage0.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBancPage0.Location = New System.Drawing.Point(0, 0)
    Me.pnBancPage0.Name = "pnBancPage0"
    Me.pnBancPage0.NTSActiveTrasparency = True
    Me.pnBancPage0.Size = New System.Drawing.Size(525, 278)
    Me.pnBancPage0.TabIndex = 0
    Me.pnBancPage0.Text = "pnBancPage0"
    '
    'fmIbanItalia
    '
    Me.fmIbanItalia.AllowDrop = True
    Me.fmIbanItalia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIbanItalia.Appearance.Options.UseBackColor = True
    Me.fmIbanItalia.Controls.Add(Me.edTb_rididcred)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_rididcred)
    Me.fmIbanItalia.Controls.Add(Me.edTb_codcuc)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_codcuc)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_swift)
    Me.fmIbanItalia.Controls.Add(Me.edTb_swift)
    Me.fmIbanItalia.Controls.Add(Me.edTb_cin)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_prefiban)
    Me.fmIbanItalia.Controls.Add(Me.edTb_prefiban)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_csbf)
    Me.fmIbanItalia.Controls.Add(Me.edTb_cab)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_cab)
    Me.fmIbanItalia.Controls.Add(Me.edTb_abi)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_abi)
    Me.fmIbanItalia.Controls.Add(Me.edTb_csbf)
    Me.fmIbanItalia.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIbanItalia.Location = New System.Drawing.Point(6, 3)
    Me.fmIbanItalia.Name = "fmIbanItalia"
    Me.fmIbanItalia.Size = New System.Drawing.Size(513, 136)
    Me.fmIbanItalia.TabIndex = 31
    Me.fmIbanItalia.Text = "IBAN italia"
    '
    'edTb_rididcred
    '
    Me.edTb_rididcred.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_rididcred.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rididcred.Location = New System.Drawing.Point(85, 109)
    Me.edTb_rididcred.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_rididcred.Name = "edTb_rididcred"
    Me.edTb_rididcred.NTSDbField = ""
    Me.edTb_rididcred.NTSForzaVisZoom = False
    Me.edTb_rididcred.NTSOldValue = ""
    Me.edTb_rididcred.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_rididcred.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_rididcred.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rididcred.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rididcred.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rididcred.Properties.MaxLength = 65536
    Me.edTb_rididcred.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rididcred.Size = New System.Drawing.Size(414, 20)
    Me.edTb_rididcred.TabIndex = 677
    '
    'lbTb_rididcred
    '
    Me.lbTb_rididcred.AutoSize = True
    Me.lbTb_rididcred.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rididcred.Location = New System.Drawing.Point(7, 112)
    Me.lbTb_rididcred.Name = "lbTb_rididcred"
    Me.lbTb_rididcred.NTSDbField = ""
    Me.lbTb_rididcred.Size = New System.Drawing.Size(77, 13)
    Me.lbTb_rididcred.TabIndex = 678
    Me.lbTb_rididcred.Text = "RID: ID credit."
    Me.lbTb_rididcred.Tooltip = ""
    Me.lbTb_rididcred.UseMnemonic = False
    '
    'edTb_codcuc
    '
    Me.edTb_codcuc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_codcuc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcuc.Location = New System.Drawing.Point(85, 87)
    Me.edTb_codcuc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_codcuc.Name = "edTb_codcuc"
    Me.edTb_codcuc.NTSDbField = ""
    Me.edTb_codcuc.NTSForzaVisZoom = False
    Me.edTb_codcuc.NTSOldValue = ""
    Me.edTb_codcuc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codcuc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codcuc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcuc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcuc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcuc.Properties.MaxLength = 65536
    Me.edTb_codcuc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcuc.Size = New System.Drawing.Size(414, 20)
    Me.edTb_codcuc.TabIndex = 675
    '
    'lbTb_codcuc
    '
    Me.lbTb_codcuc.AutoSize = True
    Me.lbTb_codcuc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codcuc.Location = New System.Drawing.Point(7, 90)
    Me.lbTb_codcuc.Name = "lbTb_codcuc"
    Me.lbTb_codcuc.NTSDbField = ""
    Me.lbTb_codcuc.Size = New System.Drawing.Size(54, 13)
    Me.lbTb_codcuc.TabIndex = 676
    Me.lbTb_codcuc.Text = "Cod. CUC"
    Me.lbTb_codcuc.Tooltip = ""
    Me.lbTb_codcuc.UseMnemonic = False
    '
    'lbTb_swift
    '
    Me.lbTb_swift.AutoSize = True
    Me.lbTb_swift.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_swift.Location = New System.Drawing.Point(7, 68)
    Me.lbTb_swift.Name = "lbTb_swift"
    Me.lbTb_swift.NTSDbField = ""
    Me.lbTb_swift.Size = New System.Drawing.Size(48, 13)
    Me.lbTb_swift.TabIndex = 674
    Me.lbTb_swift.Text = "Bic/Swift"
    Me.lbTb_swift.Tooltip = ""
    Me.lbTb_swift.UseMnemonic = False
    '
    'edTb_swift
    '
    Me.edTb_swift.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_swift.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_swift.Location = New System.Drawing.Point(85, 65)
    Me.edTb_swift.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_swift.Name = "edTb_swift"
    Me.edTb_swift.NTSDbField = ""
    Me.edTb_swift.NTSForzaVisZoom = False
    Me.edTb_swift.NTSOldValue = ""
    Me.edTb_swift.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_swift.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_swift.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_swift.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_swift.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_swift.Properties.MaxLength = 65536
    Me.edTb_swift.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_swift.Size = New System.Drawing.Size(414, 20)
    Me.edTb_swift.TabIndex = 673
    '
    'edTb_cin
    '
    Me.edTb_cin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cin.Location = New System.Drawing.Point(191, 43)
    Me.edTb_cin.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_cin.Name = "edTb_cin"
    Me.edTb_cin.NTSDbField = ""
    Me.edTb_cin.NTSForzaVisZoom = False
    Me.edTb_cin.NTSOldValue = ""
    Me.edTb_cin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cin.Properties.MaxLength = 65536
    Me.edTb_cin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cin.Size = New System.Drawing.Size(24, 20)
    Me.edTb_cin.TabIndex = 21
    '
    'lbTb_prefiban
    '
    Me.lbTb_prefiban.AutoSize = True
    Me.lbTb_prefiban.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_prefiban.Location = New System.Drawing.Point(298, 46)
    Me.lbTb_prefiban.Name = "lbTb_prefiban"
    Me.lbTb_prefiban.NTSDbField = ""
    Me.lbTb_prefiban.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_prefiban.TabIndex = 20
    Me.lbTb_prefiban.Text = "Prefisso IBAN"
    Me.lbTb_prefiban.Tooltip = ""
    Me.lbTb_prefiban.UseMnemonic = False
    '
    'edTb_prefiban
    '
    Me.edTb_prefiban.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_prefiban.Location = New System.Drawing.Point(399, 43)
    Me.edTb_prefiban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_prefiban.Name = "edTb_prefiban"
    Me.edTb_prefiban.NTSDbField = ""
    Me.edTb_prefiban.NTSForzaVisZoom = False
    Me.edTb_prefiban.NTSOldValue = ""
    Me.edTb_prefiban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_prefiban.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_prefiban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_prefiban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_prefiban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_prefiban.Properties.MaxLength = 65536
    Me.edTb_prefiban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_prefiban.Size = New System.Drawing.Size(100, 20)
    Me.edTb_prefiban.TabIndex = 19
    '
    'edTb_fido
    '
    Me.edTb_fido.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_fido.EditValue = "0"
    Me.edTb_fido.Location = New System.Drawing.Point(91, 175)
    Me.edTb_fido.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_fido.Name = "edTb_fido"
    Me.edTb_fido.NTSDbField = ""
    Me.edTb_fido.NTSFormat = "0"
    Me.edTb_fido.NTSForzaVisZoom = False
    Me.edTb_fido.NTSOldValue = ""
    Me.edTb_fido.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_fido.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_fido.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_fido.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_fido.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_fido.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_fido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_fido.Properties.MaxLength = 65536
    Me.edTb_fido.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_fido.Size = New System.Drawing.Size(100, 20)
    Me.edTb_fido.TabIndex = 14
    '
    'edTb_ritfisc
    '
    Me.edTb_ritfisc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ritfisc.EditValue = "0"
    Me.edTb_ritfisc.Location = New System.Drawing.Point(406, 175)
    Me.edTb_ritfisc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_ritfisc.Name = "edTb_ritfisc"
    Me.edTb_ritfisc.NTSDbField = ""
    Me.edTb_ritfisc.NTSFormat = "0"
    Me.edTb_ritfisc.NTSForzaVisZoom = False
    Me.edTb_ritfisc.NTSOldValue = ""
    Me.edTb_ritfisc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ritfisc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ritfisc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ritfisc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ritfisc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ritfisc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ritfisc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ritfisc.Properties.MaxLength = 65536
    Me.edTb_ritfisc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ritfisc.Size = New System.Drawing.Size(99, 20)
    Me.edTb_ritfisc.TabIndex = 15
    '
    'lbTb_fido
    '
    Me.lbTb_fido.AutoSize = True
    Me.lbTb_fido.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_fido.Location = New System.Drawing.Point(7, 178)
    Me.lbTb_fido.Name = "lbTb_fido"
    Me.lbTb_fido.NTSDbField = ""
    Me.lbTb_fido.Size = New System.Drawing.Size(66, 13)
    Me.lbTb_fido.TabIndex = 16
    Me.lbTb_fido.Text = "Importo fido"
    Me.lbTb_fido.Tooltip = ""
    Me.lbTb_fido.UseMnemonic = False
    '
    'lbTb_ritfisc
    '
    Me.lbTb_ritfisc.AutoSize = True
    Me.lbTb_ritfisc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ritfisc.Location = New System.Drawing.Point(304, 178)
    Me.lbTb_ritfisc.Name = "lbTb_ritfisc"
    Me.lbTb_ritfisc.NTSDbField = ""
    Me.lbTb_ritfisc.Size = New System.Drawing.Size(71, 13)
    Me.lbTb_ritfisc.TabIndex = 17
    Me.lbTb_ritfisc.Text = "% Rit. fiscale"
    Me.lbTb_ritfisc.Tooltip = ""
    Me.lbTb_ritfisc.UseMnemonic = False
    '
    'edTb_desbanc2
    '
    Me.edTb_desbanc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desbanc2.Location = New System.Drawing.Point(91, 228)
    Me.edTb_desbanc2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc2.Name = "edTb_desbanc2"
    Me.edTb_desbanc2.NTSDbField = ""
    Me.edTb_desbanc2.NTSForzaVisZoom = False
    Me.edTb_desbanc2.NTSOldValue = ""
    Me.edTb_desbanc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc2.Properties.MaxLength = 65536
    Me.edTb_desbanc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc2.Size = New System.Drawing.Size(414, 20)
    Me.edTb_desbanc2.TabIndex = 18
    '
    'edTb_iban
    '
    Me.edTb_iban.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_iban.Location = New System.Drawing.Point(91, 152)
    Me.edTb_iban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_iban.Name = "edTb_iban"
    Me.edTb_iban.NTSDbField = ""
    Me.edTb_iban.NTSForzaVisZoom = False
    Me.edTb_iban.NTSOldValue = ""
    Me.edTb_iban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_iban.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_iban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_iban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_iban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_iban.Properties.MaxLength = 65536
    Me.edTb_iban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_iban.Size = New System.Drawing.Size(414, 20)
    Me.edTb_iban.TabIndex = 20
    '
    'edTb_desbanc3
    '
    Me.edTb_desbanc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desbanc3.Location = New System.Drawing.Point(91, 250)
    Me.edTb_desbanc3.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc3.Name = "edTb_desbanc3"
    Me.edTb_desbanc3.NTSDbField = ""
    Me.edTb_desbanc3.NTSForzaVisZoom = False
    Me.edTb_desbanc3.NTSOldValue = ""
    Me.edTb_desbanc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc3.Properties.MaxLength = 65536
    Me.edTb_desbanc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc3.Size = New System.Drawing.Size(414, 20)
    Me.edTb_desbanc3.TabIndex = 19
    '
    'lbTb_desbanc2
    '
    Me.lbTb_desbanc2.AutoSize = True
    Me.lbTb_desbanc2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc2.Location = New System.Drawing.Point(6, 231)
    Me.lbTb_desbanc2.Name = "lbTb_desbanc2"
    Me.lbTb_desbanc2.NTSDbField = ""
    Me.lbTb_desbanc2.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_desbanc2.TabIndex = 21
    Me.lbTb_desbanc2.Text = "Descr. agg. 1"
    Me.lbTb_desbanc2.Tooltip = ""
    Me.lbTb_desbanc2.UseMnemonic = False
    '
    'lbTb_tiprap
    '
    Me.lbTb_tiprap.AutoSize = True
    Me.lbTb_tiprap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tiprap.Location = New System.Drawing.Point(7, 201)
    Me.lbTb_tiprap.Name = "lbTb_tiprap"
    Me.lbTb_tiprap.NTSDbField = ""
    Me.lbTb_tiprap.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_tiprap.TabIndex = 25
    Me.lbTb_tiprap.Text = "Tipo rapporto"
    Me.lbTb_tiprap.Tooltip = ""
    Me.lbTb_tiprap.UseMnemonic = False
    '
    'lbTb_desbanc3
    '
    Me.lbTb_desbanc3.AutoSize = True
    Me.lbTb_desbanc3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc3.Location = New System.Drawing.Point(6, 253)
    Me.lbTb_desbanc3.Name = "lbTb_desbanc3"
    Me.lbTb_desbanc3.NTSDbField = ""
    Me.lbTb_desbanc3.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_desbanc3.TabIndex = 22
    Me.lbTb_desbanc3.Text = "Descr. agg. 2"
    Me.lbTb_desbanc3.Tooltip = ""
    Me.lbTb_desbanc3.UseMnemonic = False
    '
    'cbTb_tiprap
    '
    Me.cbTb_tiprap.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tiprap.DataSource = Nothing
    Me.cbTb_tiprap.DisplayMember = ""
    Me.cbTb_tiprap.Location = New System.Drawing.Point(91, 198)
    Me.cbTb_tiprap.Name = "cbTb_tiprap"
    Me.cbTb_tiprap.NTSDbField = ""
    Me.cbTb_tiprap.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tiprap.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tiprap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tiprap.Properties.DropDownRows = 30
    Me.cbTb_tiprap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tiprap.SelectedValue = ""
    Me.cbTb_tiprap.Size = New System.Drawing.Size(100, 20)
    Me.cbTb_tiprap.TabIndex = 24
    Me.cbTb_tiprap.ValueMember = ""
    '
    'lbTb_iban
    '
    Me.lbTb_iban.AutoSize = True
    Me.lbTb_iban.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_iban.Location = New System.Drawing.Point(7, 155)
    Me.lbTb_iban.Name = "lbTb_iban"
    Me.lbTb_iban.NTSDbField = ""
    Me.lbTb_iban.Size = New System.Drawing.Size(65, 13)
    Me.lbTb_iban.TabIndex = 23
    Me.lbTb_iban.Text = "IBAN estero"
    Me.lbTb_iban.Tooltip = ""
    Me.lbTb_iban.UseMnemonic = False
    '
    'tsBancPage1
    '
    Me.tsBancPage1.AllowDrop = True
    Me.tsBancPage1.Controls.Add(Me.pnBancPage1)
    Me.tsBancPage1.Enable = True
    Me.tsBancPage1.Name = "tsBancPage1"
    Me.tsBancPage1.Size = New System.Drawing.Size(525, 278)
    Me.tsBancPage1.Text = "Condizioni C/C 1"
    '
    'pnBancPage1
    '
    Me.pnBancPage1.AllowDrop = True
    Me.pnBancPage1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBancPage1.Appearance.Options.UseBackColor = True
    Me.pnBancPage1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBancPage1.Controls.Add(Me.lbTb_maxscop2)
    Me.pnBancPage1.Controls.Add(Me.edTb_maxscop2)
    Me.pnBancPage1.Controls.Add(Me.NtsLabel1)
    Me.pnBancPage1.Controls.Add(Me.lbTb_maxscop)
    Me.pnBancPage1.Controls.Add(Me.edTb_maxscop)
    Me.pnBancPage1.Controls.Add(Me.edTb_impboll)
    Me.pnBancPage1.Controls.Add(Me.edTb_codbdoc)
    Me.pnBancPage1.Controls.Add(Me.lbTb_impboll)
    Me.pnBancPage1.Controls.Add(Me.edTb_speseop)
    Me.pnBancPage1.Controls.Add(Me.lbTb_speseop)
    Me.pnBancPage1.Controls.Add(Me.edTb_spechiusann)
    Me.pnBancPage1.Controls.Add(Me.lbTb_codbdoc)
    Me.pnBancPage1.Controls.Add(Me.lbTb_spechiusann)
    Me.pnBancPage1.Controls.Add(Me.edTb_tenutcc)
    Me.pnBancPage1.Controls.Add(Me.lbTb_tiporapdf)
    Me.pnBancPage1.Controls.Add(Me.lbTb_tenutcc)
    Me.pnBancPage1.Controls.Add(Me.lbTb_opcompr)
    Me.pnBancPage1.Controls.Add(Me.edTb_opcompr)
    Me.pnBancPage1.Controls.Add(Me.edTb_tiporapdf)
    Me.pnBancPage1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBancPage1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBancPage1.Location = New System.Drawing.Point(0, 0)
    Me.pnBancPage1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnBancPage1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnBancPage1.Name = "pnBancPage1"
    Me.pnBancPage1.NTSActiveTrasparency = True
    Me.pnBancPage1.Size = New System.Drawing.Size(525, 278)
    Me.pnBancPage1.TabIndex = 0
    Me.pnBancPage1.Text = "pnBancPage1"
    '
    'lbTb_maxscop2
    '
    Me.lbTb_maxscop2.AutoSize = True
    Me.lbTb_maxscop2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_maxscop2.Location = New System.Drawing.Point(10, 138)
    Me.lbTb_maxscop2.Name = "lbTb_maxscop2"
    Me.lbTb_maxscop2.NTSDbField = ""
    Me.lbTb_maxscop2.Size = New System.Drawing.Size(142, 13)
    Me.lbTb_maxscop2.TabIndex = 30
    Me.lbTb_maxscop2.Text = "% Max scoperto oltre scagl."
    Me.lbTb_maxscop2.Tooltip = ""
    Me.lbTb_maxscop2.UseMnemonic = False
    '
    'edTb_maxscop2
    '
    Me.edTb_maxscop2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_maxscop2.EditValue = "0"
    Me.edTb_maxscop2.Location = New System.Drawing.Point(411, 135)
    Me.edTb_maxscop2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_maxscop2.Name = "edTb_maxscop2"
    Me.edTb_maxscop2.NTSDbField = ""
    Me.edTb_maxscop2.NTSFormat = "0"
    Me.edTb_maxscop2.NTSForzaVisZoom = False
    Me.edTb_maxscop2.NTSOldValue = ""
    Me.edTb_maxscop2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_maxscop2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_maxscop2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_maxscop2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_maxscop2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_maxscop2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_maxscop2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_maxscop2.Properties.MaxLength = 65536
    Me.edTb_maxscop2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_maxscop2.Size = New System.Drawing.Size(100, 20)
    Me.edTb_maxscop2.TabIndex = 29
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(13, 252)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(80, 13)
    Me.NtsLabel1.TabIndex = 35
    Me.NtsLabel1.Text = "DOC FINANCE:"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'lbTb_maxscop
    '
    Me.lbTb_maxscop.AutoSize = True
    Me.lbTb_maxscop.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_maxscop.Location = New System.Drawing.Point(10, 116)
    Me.lbTb_maxscop.Name = "lbTb_maxscop"
    Me.lbTb_maxscop.NTSDbField = ""
    Me.lbTb_maxscop.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_maxscop.TabIndex = 28
    Me.lbTb_maxscop.Text = "% Massimo scoperto"
    Me.lbTb_maxscop.Tooltip = ""
    Me.lbTb_maxscop.UseMnemonic = False
    '
    'edTb_maxscop
    '
    Me.edTb_maxscop.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_maxscop.EditValue = "0"
    Me.edTb_maxscop.Location = New System.Drawing.Point(411, 113)
    Me.edTb_maxscop.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_maxscop.Name = "edTb_maxscop"
    Me.edTb_maxscop.NTSDbField = ""
    Me.edTb_maxscop.NTSFormat = "0"
    Me.edTb_maxscop.NTSForzaVisZoom = False
    Me.edTb_maxscop.NTSOldValue = ""
    Me.edTb_maxscop.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_maxscop.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_maxscop.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_maxscop.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_maxscop.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_maxscop.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_maxscop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_maxscop.Properties.MaxLength = 65536
    Me.edTb_maxscop.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_maxscop.Size = New System.Drawing.Size(100, 20)
    Me.edTb_maxscop.TabIndex = 27
    '
    'edTb_impboll
    '
    Me.edTb_impboll.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_impboll.EditValue = "0"
    Me.edTb_impboll.Location = New System.Drawing.Point(411, 91)
    Me.edTb_impboll.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_impboll.Name = "edTb_impboll"
    Me.edTb_impboll.NTSDbField = ""
    Me.edTb_impboll.NTSFormat = "0"
    Me.edTb_impboll.NTSForzaVisZoom = False
    Me.edTb_impboll.NTSOldValue = ""
    Me.edTb_impboll.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_impboll.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_impboll.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_impboll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_impboll.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_impboll.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_impboll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_impboll.Properties.MaxLength = 65536
    Me.edTb_impboll.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_impboll.Size = New System.Drawing.Size(100, 20)
    Me.edTb_impboll.TabIndex = 26
    '
    'edTb_codbdoc
    '
    Me.edTb_codbdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codbdoc.Location = New System.Drawing.Point(181, 249)
    Me.edTb_codbdoc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_codbdoc.Name = "edTb_codbdoc"
    Me.edTb_codbdoc.NTSDbField = ""
    Me.edTb_codbdoc.NTSForzaVisZoom = False
    Me.edTb_codbdoc.NTSOldValue = ""
    Me.edTb_codbdoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codbdoc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codbdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codbdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codbdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codbdoc.Properties.MaxLength = 65536
    Me.edTb_codbdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codbdoc.Size = New System.Drawing.Size(100, 20)
    Me.edTb_codbdoc.TabIndex = 21
    '
    'lbTb_impboll
    '
    Me.lbTb_impboll.AutoSize = True
    Me.lbTb_impboll.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_impboll.Location = New System.Drawing.Point(10, 94)
    Me.lbTb_impboll.Name = "lbTb_impboll"
    Me.lbTb_impboll.NTSDbField = ""
    Me.lbTb_impboll.Size = New System.Drawing.Size(67, 13)
    Me.lbTb_impboll.TabIndex = 25
    Me.lbTb_impboll.Text = "Imposta bolli"
    Me.lbTb_impboll.Tooltip = ""
    Me.lbTb_impboll.UseMnemonic = False
    '
    'edTb_speseop
    '
    Me.edTb_speseop.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_speseop.EditValue = "0"
    Me.edTb_speseop.Location = New System.Drawing.Point(411, 69)
    Me.edTb_speseop.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_speseop.Name = "edTb_speseop"
    Me.edTb_speseop.NTSDbField = ""
    Me.edTb_speseop.NTSFormat = "0"
    Me.edTb_speseop.NTSForzaVisZoom = False
    Me.edTb_speseop.NTSOldValue = ""
    Me.edTb_speseop.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_speseop.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_speseop.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_speseop.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_speseop.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_speseop.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_speseop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_speseop.Properties.MaxLength = 65536
    Me.edTb_speseop.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_speseop.Size = New System.Drawing.Size(100, 20)
    Me.edTb_speseop.TabIndex = 24
    '
    'lbTb_speseop
    '
    Me.lbTb_speseop.AutoSize = True
    Me.lbTb_speseop.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_speseop.Location = New System.Drawing.Point(10, 72)
    Me.lbTb_speseop.Name = "lbTb_speseop"
    Me.lbTb_speseop.NTSDbField = ""
    Me.lbTb_speseop.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_speseop.TabIndex = 23
    Me.lbTb_speseop.Text = "Spese per operazione"
    Me.lbTb_speseop.Tooltip = ""
    Me.lbTb_speseop.UseMnemonic = False
    '
    'edTb_spechiusann
    '
    Me.edTb_spechiusann.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_spechiusann.EditValue = "0"
    Me.edTb_spechiusann.Location = New System.Drawing.Point(411, 47)
    Me.edTb_spechiusann.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_spechiusann.Name = "edTb_spechiusann"
    Me.edTb_spechiusann.NTSDbField = ""
    Me.edTb_spechiusann.NTSFormat = "0"
    Me.edTb_spechiusann.NTSForzaVisZoom = False
    Me.edTb_spechiusann.NTSOldValue = ""
    Me.edTb_spechiusann.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_spechiusann.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_spechiusann.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_spechiusann.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_spechiusann.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_spechiusann.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_spechiusann.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_spechiusann.Properties.MaxLength = 65536
    Me.edTb_spechiusann.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_spechiusann.Size = New System.Drawing.Size(100, 20)
    Me.edTb_spechiusann.TabIndex = 22
    '
    'lbTb_codbdoc
    '
    Me.lbTb_codbdoc.AutoSize = True
    Me.lbTb_codbdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codbdoc.Location = New System.Drawing.Point(99, 252)
    Me.lbTb_codbdoc.Name = "lbTb_codbdoc"
    Me.lbTb_codbdoc.NTSDbField = ""
    Me.lbTb_codbdoc.Size = New System.Drawing.Size(62, 13)
    Me.lbTb_codbdoc.TabIndex = 7
    Me.lbTb_codbdoc.Text = "Cod. banca"
    Me.lbTb_codbdoc.Tooltip = ""
    Me.lbTb_codbdoc.UseMnemonic = False
    '
    'lbTb_spechiusann
    '
    Me.lbTb_spechiusann.AutoSize = True
    Me.lbTb_spechiusann.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_spechiusann.Location = New System.Drawing.Point(10, 50)
    Me.lbTb_spechiusann.Name = "lbTb_spechiusann"
    Me.lbTb_spechiusann.NTSDbField = ""
    Me.lbTb_spechiusann.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_spechiusann.TabIndex = 21
    Me.lbTb_spechiusann.Text = "Spese tenuta C/C annuali"
    Me.lbTb_spechiusann.Tooltip = ""
    Me.lbTb_spechiusann.UseMnemonic = False
    '
    'edTb_tenutcc
    '
    Me.edTb_tenutcc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_tenutcc.EditValue = "0"
    Me.edTb_tenutcc.Location = New System.Drawing.Point(411, 25)
    Me.edTb_tenutcc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_tenutcc.Name = "edTb_tenutcc"
    Me.edTb_tenutcc.NTSDbField = ""
    Me.edTb_tenutcc.NTSFormat = "0"
    Me.edTb_tenutcc.NTSForzaVisZoom = False
    Me.edTb_tenutcc.NTSOldValue = ""
    Me.edTb_tenutcc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tenutcc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tenutcc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_tenutcc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_tenutcc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tenutcc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tenutcc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tenutcc.Properties.MaxLength = 65536
    Me.edTb_tenutcc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tenutcc.Size = New System.Drawing.Size(100, 20)
    Me.edTb_tenutcc.TabIndex = 20
    '
    'lbTb_tiporapdf
    '
    Me.lbTb_tiporapdf.AutoSize = True
    Me.lbTb_tiporapdf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tiporapdf.Location = New System.Drawing.Point(310, 252)
    Me.lbTb_tiporapdf.Name = "lbTb_tiporapdf"
    Me.lbTb_tiporapdf.NTSDbField = ""
    Me.lbTb_tiporapdf.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_tiporapdf.TabIndex = 20
    Me.lbTb_tiporapdf.Text = "Tipo rapporto"
    Me.lbTb_tiporapdf.Tooltip = ""
    Me.lbTb_tiporapdf.UseMnemonic = False
    '
    'lbTb_tenutcc
    '
    Me.lbTb_tenutcc.AutoSize = True
    Me.lbTb_tenutcc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tenutcc.Location = New System.Drawing.Point(10, 28)
    Me.lbTb_tenutcc.Name = "lbTb_tenutcc"
    Me.lbTb_tenutcc.NTSDbField = ""
    Me.lbTb_tenutcc.Size = New System.Drawing.Size(82, 13)
    Me.lbTb_tenutcc.TabIndex = 19
    Me.lbTb_tenutcc.Text = "Spese fisse C/C"
    Me.lbTb_tenutcc.Tooltip = ""
    Me.lbTb_tenutcc.UseMnemonic = False
    '
    'lbTb_opcompr
    '
    Me.lbTb_opcompr.AutoSize = True
    Me.lbTb_opcompr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_opcompr.Location = New System.Drawing.Point(10, 6)
    Me.lbTb_opcompr.Name = "lbTb_opcompr"
    Me.lbTb_opcompr.NTSDbField = ""
    Me.lbTb_opcompr.Size = New System.Drawing.Size(130, 13)
    Me.lbTb_opcompr.TabIndex = 18
    Me.lbTb_opcompr.Text = "Operazioni annuali incluse"
    Me.lbTb_opcompr.Tooltip = ""
    Me.lbTb_opcompr.UseMnemonic = False
    '
    'edTb_opcompr
    '
    Me.edTb_opcompr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_opcompr.EditValue = "0"
    Me.edTb_opcompr.Location = New System.Drawing.Point(411, 3)
    Me.edTb_opcompr.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_opcompr.Name = "edTb_opcompr"
    Me.edTb_opcompr.NTSDbField = ""
    Me.edTb_opcompr.NTSFormat = "0"
    Me.edTb_opcompr.NTSForzaVisZoom = False
    Me.edTb_opcompr.NTSOldValue = ""
    Me.edTb_opcompr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_opcompr.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_opcompr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_opcompr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_opcompr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_opcompr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_opcompr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_opcompr.Properties.MaxLength = 65536
    Me.edTb_opcompr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_opcompr.Size = New System.Drawing.Size(100, 20)
    Me.edTb_opcompr.TabIndex = 17
    '
    'edTb_tiporapdf
    '
    Me.edTb_tiporapdf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tiporapdf.Location = New System.Drawing.Point(411, 249)
    Me.edTb_tiporapdf.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_tiporapdf.Name = "edTb_tiporapdf"
    Me.edTb_tiporapdf.NTSDbField = ""
    Me.edTb_tiporapdf.NTSForzaVisZoom = False
    Me.edTb_tiporapdf.NTSOldValue = ""
    Me.edTb_tiporapdf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_tiporapdf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_tiporapdf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tiporapdf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tiporapdf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tiporapdf.Properties.MaxLength = 65536
    Me.edTb_tiporapdf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tiporapdf.Size = New System.Drawing.Size(100, 20)
    Me.edTb_tiporapdf.TabIndex = 19
    '
    'tsBancPage2
    '
    Me.tsBancPage2.AllowDrop = True
    Me.tsBancPage2.Controls.Add(Me.pnBancPage2)
    Me.tsBancPage2.Enable = True
    Me.tsBancPage2.Name = "tsBancPage2"
    Me.tsBancPage2.Size = New System.Drawing.Size(525, 278)
    Me.tsBancPage2.Text = "Condizioni C/C 2"
    '
    'pnBancPage2
    '
    Me.pnBancPage2.AllowDrop = True
    Me.pnBancPage2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBancPage2.Appearance.Options.UseBackColor = True
    Me.pnBancPage2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBancPage2.Controls.Add(Me.cbTb_addimpa)
    Me.pnBancPage2.Controls.Add(Me.lbTb_addimpa)
    Me.pnBancPage2.Controls.Add(Me.lbTb_accinat)
    Me.pnBancPage2.Controls.Add(Me.cbTb_accinat)
    Me.pnBancPage2.Controls.Add(Me.edTb_scaglconsm2)
    Me.pnBancPage2.Controls.Add(Me.lbTb_scaglconsm2)
    Me.pnBancPage2.Controls.Add(Me.edTb_scaglconsm1)
    Me.pnBancPage2.Controls.Add(Me.lbTb_scaglconsm1)
    Me.pnBancPage2.Controls.Add(Me.lbTb_scaglcms)
    Me.pnBancPage2.Controls.Add(Me.edTb_scaglcms)
    Me.pnBancPage2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBancPage2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBancPage2.Location = New System.Drawing.Point(0, 0)
    Me.pnBancPage2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnBancPage2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnBancPage2.Name = "pnBancPage2"
    Me.pnBancPage2.NTSActiveTrasparency = True
    Me.pnBancPage2.Size = New System.Drawing.Size(525, 278)
    Me.pnBancPage2.TabIndex = 1
    Me.pnBancPage2.Text = "NtsPanel1"
    '
    'cbTb_addimpa
    '
    Me.cbTb_addimpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_addimpa.DataSource = Nothing
    Me.cbTb_addimpa.DisplayMember = ""
    Me.cbTb_addimpa.Location = New System.Drawing.Point(411, 88)
    Me.cbTb_addimpa.Name = "cbTb_addimpa"
    Me.cbTb_addimpa.NTSDbField = ""
    Me.cbTb_addimpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_addimpa.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_addimpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_addimpa.Properties.DropDownRows = 30
    Me.cbTb_addimpa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_addimpa.SelectedValue = ""
    Me.cbTb_addimpa.Size = New System.Drawing.Size(100, 20)
    Me.cbTb_addimpa.TabIndex = 29
    Me.cbTb_addimpa.ValueMember = ""
    '
    'lbTb_addimpa
    '
    Me.lbTb_addimpa.AutoSize = True
    Me.lbTb_addimpa.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_addimpa.Location = New System.Drawing.Point(10, 94)
    Me.lbTb_addimpa.Name = "lbTb_addimpa"
    Me.lbTb_addimpa.NTSDbField = ""
    Me.lbTb_addimpa.Size = New System.Drawing.Size(144, 13)
    Me.lbTb_addimpa.TabIndex = 28
    Me.lbTb_addimpa.Text = "Periodo addeb. inter. passivi"
    Me.lbTb_addimpa.Tooltip = ""
    Me.lbTb_addimpa.UseMnemonic = False
    '
    'lbTb_accinat
    '
    Me.lbTb_accinat.AutoSize = True
    Me.lbTb_accinat.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_accinat.Location = New System.Drawing.Point(10, 72)
    Me.lbTb_accinat.Name = "lbTb_accinat"
    Me.lbTb_accinat.NTSDbField = ""
    Me.lbTb_accinat.Size = New System.Drawing.Size(138, 13)
    Me.lbTb_accinat.TabIndex = 27
    Me.lbTb_accinat.Text = "Periodo accred. inter. attivi"
    Me.lbTb_accinat.Tooltip = ""
    Me.lbTb_accinat.UseMnemonic = False
    '
    'cbTb_accinat
    '
    Me.cbTb_accinat.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_accinat.DataSource = Nothing
    Me.cbTb_accinat.DisplayMember = ""
    Me.cbTb_accinat.EditValue = ""
    Me.cbTb_accinat.Location = New System.Drawing.Point(411, 66)
    Me.cbTb_accinat.Name = "cbTb_accinat"
    Me.cbTb_accinat.NTSDbField = ""
    Me.cbTb_accinat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_accinat.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_accinat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_accinat.Properties.DropDownRows = 30
    Me.cbTb_accinat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_accinat.SelectedValue = ""
    Me.cbTb_accinat.Size = New System.Drawing.Size(100, 20)
    Me.cbTb_accinat.TabIndex = 26
    Me.cbTb_accinat.ValueMember = ""
    '
    'edTb_scaglconsm2
    '
    Me.edTb_scaglconsm2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_scaglconsm2.EditValue = "0"
    Me.edTb_scaglconsm2.Location = New System.Drawing.Point(411, 44)
    Me.edTb_scaglconsm2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_scaglconsm2.Name = "edTb_scaglconsm2"
    Me.edTb_scaglconsm2.NTSDbField = ""
    Me.edTb_scaglconsm2.NTSFormat = "0"
    Me.edTb_scaglconsm2.NTSForzaVisZoom = False
    Me.edTb_scaglconsm2.NTSOldValue = ""
    Me.edTb_scaglconsm2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_scaglconsm2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_scaglconsm2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_scaglconsm2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_scaglconsm2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_scaglconsm2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_scaglconsm2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_scaglconsm2.Properties.MaxLength = 65536
    Me.edTb_scaglconsm2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_scaglconsm2.Size = New System.Drawing.Size(100, 20)
    Me.edTb_scaglconsm2.TabIndex = 22
    '
    'lbTb_scaglconsm2
    '
    Me.lbTb_scaglconsm2.AutoSize = True
    Me.lbTb_scaglconsm2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scaglconsm2.Location = New System.Drawing.Point(10, 50)
    Me.lbTb_scaglconsm2.Name = "lbTb_scaglconsm2"
    Me.lbTb_scaglconsm2.NTSDbField = ""
    Me.lbTb_scaglconsm2.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_scaglconsm2.TabIndex = 21
    Me.lbTb_scaglconsm2.Text = "Imp. scagl. cons. media 2"
    Me.lbTb_scaglconsm2.Tooltip = ""
    Me.lbTb_scaglconsm2.UseMnemonic = False
    '
    'edTb_scaglconsm1
    '
    Me.edTb_scaglconsm1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_scaglconsm1.EditValue = "0"
    Me.edTb_scaglconsm1.Location = New System.Drawing.Point(411, 22)
    Me.edTb_scaglconsm1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_scaglconsm1.Name = "edTb_scaglconsm1"
    Me.edTb_scaglconsm1.NTSDbField = ""
    Me.edTb_scaglconsm1.NTSFormat = "0"
    Me.edTb_scaglconsm1.NTSForzaVisZoom = False
    Me.edTb_scaglconsm1.NTSOldValue = ""
    Me.edTb_scaglconsm1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_scaglconsm1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_scaglconsm1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_scaglconsm1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_scaglconsm1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_scaglconsm1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_scaglconsm1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_scaglconsm1.Properties.MaxLength = 65536
    Me.edTb_scaglconsm1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_scaglconsm1.Size = New System.Drawing.Size(100, 20)
    Me.edTb_scaglconsm1.TabIndex = 20
    '
    'lbTb_scaglconsm1
    '
    Me.lbTb_scaglconsm1.AutoSize = True
    Me.lbTb_scaglconsm1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scaglconsm1.Location = New System.Drawing.Point(10, 28)
    Me.lbTb_scaglconsm1.Name = "lbTb_scaglconsm1"
    Me.lbTb_scaglconsm1.NTSDbField = ""
    Me.lbTb_scaglconsm1.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_scaglconsm1.TabIndex = 19
    Me.lbTb_scaglconsm1.Text = "Imp. scagl. cons. media 1"
    Me.lbTb_scaglconsm1.Tooltip = ""
    Me.lbTb_scaglconsm1.UseMnemonic = False
    '
    'lbTb_scaglcms
    '
    Me.lbTb_scaglcms.AutoSize = True
    Me.lbTb_scaglcms.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scaglcms.Location = New System.Drawing.Point(10, 6)
    Me.lbTb_scaglcms.Name = "lbTb_scaglcms"
    Me.lbTb_scaglcms.NTSDbField = ""
    Me.lbTb_scaglcms.Size = New System.Drawing.Size(131, 13)
    Me.lbTb_scaglcms.TabIndex = 18
    Me.lbTb_scaglcms.Text = "Imp. scagl. per max scop."
    Me.lbTb_scaglcms.Tooltip = ""
    Me.lbTb_scaglcms.UseMnemonic = False
    '
    'edTb_scaglcms
    '
    Me.edTb_scaglcms.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_scaglcms.EditValue = "0"
    Me.edTb_scaglcms.Location = New System.Drawing.Point(411, 0)
    Me.edTb_scaglcms.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_scaglcms.Name = "edTb_scaglcms"
    Me.edTb_scaglcms.NTSDbField = ""
    Me.edTb_scaglcms.NTSFormat = "0"
    Me.edTb_scaglcms.NTSForzaVisZoom = False
    Me.edTb_scaglcms.NTSOldValue = ""
    Me.edTb_scaglcms.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_scaglcms.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_scaglcms.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_scaglcms.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_scaglcms.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_scaglcms.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_scaglcms.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_scaglcms.Properties.MaxLength = 65536
    Me.edTb_scaglcms.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_scaglcms.Size = New System.Drawing.Size(100, 20)
    Me.edTb_scaglcms.TabIndex = 17
    '
    'tsBancPage3
    '
    Me.tsBancPage3.AllowDrop = True
    Me.tsBancPage3.Controls.Add(Me.pnBancPage3)
    Me.tsBancPage3.Enable = True
    Me.tsBancPage3.Name = "tsBancPage3"
    Me.tsBancPage3.Size = New System.Drawing.Size(525, 278)
    Me.tsBancPage3.Text = "Condizioni C/C 3"
    '
    'pnBancPage3
    '
    Me.pnBancPage3.AllowDrop = True
    Me.pnBancPage3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBancPage3.Appearance.Options.UseBackColor = True
    Me.pnBancPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBancPage3.Controls.Add(Me.lbTb_saldoanticipaz)
    Me.pnBancPage3.Controls.Add(Me.edTb_saldoanticipaz)
    Me.pnBancPage3.Controls.Add(Me.edTb_dtulchiu)
    Me.pnBancPage3.Controls.Add(Me.edTb_dtuladint)
    Me.pnBancPage3.Controls.Add(Me.edTb_dtulacint)
    Me.pnBancPage3.Controls.Add(Me.edTb_numdulchiu)
    Me.pnBancPage3.Controls.Add(Me.lbTb_numdulchiu)
    Me.pnBancPage3.Controls.Add(Me.edTb_numaulchiu)
    Me.pnBancPage3.Controls.Add(Me.lbTb_numaulchiu)
    Me.pnBancPage3.Controls.Add(Me.lbTb_dtuladint)
    Me.pnBancPage3.Controls.Add(Me.lbTb_saldovalulch)
    Me.pnBancPage3.Controls.Add(Me.lbTb_saldoulchiu)
    Me.pnBancPage3.Controls.Add(Me.lbTb_dtulchiu)
    Me.pnBancPage3.Controls.Add(Me.lbTb_dtulacint)
    Me.pnBancPage3.Controls.Add(Me.lbTb_saldovalulad)
    Me.pnBancPage3.Controls.Add(Me.edTb_saldovalulad)
    Me.pnBancPage3.Controls.Add(Me.lbTb_saldovalulac)
    Me.pnBancPage3.Controls.Add(Me.edTb_saldovalulac)
    Me.pnBancPage3.Controls.Add(Me.edTb_saldovalulch)
    Me.pnBancPage3.Controls.Add(Me.edTb_saldoulchiu)
    Me.pnBancPage3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBancPage3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBancPage3.Location = New System.Drawing.Point(0, 0)
    Me.pnBancPage3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnBancPage3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnBancPage3.Name = "pnBancPage3"
    Me.pnBancPage3.NTSActiveTrasparency = True
    Me.pnBancPage3.Size = New System.Drawing.Size(525, 278)
    Me.pnBancPage3.TabIndex = 1
    Me.pnBancPage3.Text = "NtsPanel2"
    '
    'lbTb_saldoanticipaz
    '
    Me.lbTb_saldoanticipaz.AutoSize = True
    Me.lbTb_saldoanticipaz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_saldoanticipaz.Location = New System.Drawing.Point(10, 205)
    Me.lbTb_saldoanticipaz.Name = "lbTb_saldoanticipaz"
    Me.lbTb_saldoanticipaz.NTSDbField = ""
    Me.lbTb_saldoanticipaz.Size = New System.Drawing.Size(229, 13)
    Me.lbTb_saldoanticipaz.TabIndex = 47
    Me.lbTb_saldoanticipaz.Text = "Saldo anticipazioni su C/C da conto portafoglio"
    Me.lbTb_saldoanticipaz.Tooltip = ""
    Me.lbTb_saldoanticipaz.UseMnemonic = False
    '
    'edTb_saldoanticipaz
    '
    Me.edTb_saldoanticipaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_saldoanticipaz.EditValue = "0"
    Me.edTb_saldoanticipaz.Location = New System.Drawing.Point(411, 202)
    Me.edTb_saldoanticipaz.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_saldoanticipaz.Name = "edTb_saldoanticipaz"
    Me.edTb_saldoanticipaz.NTSDbField = ""
    Me.edTb_saldoanticipaz.NTSFormat = "0"
    Me.edTb_saldoanticipaz.NTSForzaVisZoom = False
    Me.edTb_saldoanticipaz.NTSOldValue = ""
    Me.edTb_saldoanticipaz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_saldoanticipaz.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_saldoanticipaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_saldoanticipaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_saldoanticipaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_saldoanticipaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_saldoanticipaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_saldoanticipaz.Properties.MaxLength = 65536
    Me.edTb_saldoanticipaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_saldoanticipaz.Size = New System.Drawing.Size(100, 20)
    Me.edTb_saldoanticipaz.TabIndex = 46
    '
    'edTb_dtulchiu
    '
    Me.edTb_dtulchiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulchiu.Location = New System.Drawing.Point(411, 47)
    Me.edTb_dtulchiu.Name = "edTb_dtulchiu"
    Me.edTb_dtulchiu.NTSDbField = ""
    Me.edTb_dtulchiu.NTSForzaVisZoom = False
    Me.edTb_dtulchiu.NTSOldValue = ""
    Me.edTb_dtulchiu.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_dtulchiu.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_dtulchiu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulchiu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulchiu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulchiu.Properties.MaxLength = 65536
    Me.edTb_dtulchiu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulchiu.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulchiu.TabIndex = 45
    '
    'edTb_dtuladint
    '
    Me.edTb_dtuladint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtuladint.Location = New System.Drawing.Point(411, 25)
    Me.edTb_dtuladint.Name = "edTb_dtuladint"
    Me.edTb_dtuladint.NTSDbField = ""
    Me.edTb_dtuladint.NTSForzaVisZoom = False
    Me.edTb_dtuladint.NTSOldValue = ""
    Me.edTb_dtuladint.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_dtuladint.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_dtuladint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtuladint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtuladint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtuladint.Properties.MaxLength = 65536
    Me.edTb_dtuladint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtuladint.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtuladint.TabIndex = 44
    '
    'edTb_dtulacint
    '
    Me.edTb_dtulacint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtulacint.Location = New System.Drawing.Point(411, 3)
    Me.edTb_dtulacint.Name = "edTb_dtulacint"
    Me.edTb_dtulacint.NTSDbField = ""
    Me.edTb_dtulacint.NTSForzaVisZoom = False
    Me.edTb_dtulacint.NTSOldValue = ""
    Me.edTb_dtulacint.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_dtulacint.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_dtulacint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtulacint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtulacint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtulacint.Properties.MaxLength = 65536
    Me.edTb_dtulacint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtulacint.Size = New System.Drawing.Size(100, 20)
    Me.edTb_dtulacint.TabIndex = 43
    '
    'edTb_numdulchiu
    '
    Me.edTb_numdulchiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_numdulchiu.EditValue = "0"
    Me.edTb_numdulchiu.Location = New System.Drawing.Point(411, 91)
    Me.edTb_numdulchiu.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_numdulchiu.Name = "edTb_numdulchiu"
    Me.edTb_numdulchiu.NTSDbField = ""
    Me.edTb_numdulchiu.NTSFormat = "0"
    Me.edTb_numdulchiu.NTSForzaVisZoom = False
    Me.edTb_numdulchiu.NTSOldValue = ""
    Me.edTb_numdulchiu.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_numdulchiu.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_numdulchiu.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_numdulchiu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_numdulchiu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_numdulchiu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_numdulchiu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_numdulchiu.Properties.MaxLength = 65536
    Me.edTb_numdulchiu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_numdulchiu.Size = New System.Drawing.Size(100, 20)
    Me.edTb_numdulchiu.TabIndex = 42
    '
    'lbTb_numdulchiu
    '
    Me.lbTb_numdulchiu.AutoSize = True
    Me.lbTb_numdulchiu.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_numdulchiu.Location = New System.Drawing.Point(10, 94)
    Me.lbTb_numdulchiu.Name = "lbTb_numdulchiu"
    Me.lbTb_numdulchiu.NTSDbField = ""
    Me.lbTb_numdulchiu.Size = New System.Drawing.Size(139, 13)
    Me.lbTb_numdulchiu.TabIndex = 41
    Me.lbTb_numdulchiu.Text = "Numeri dare ultima chiusura"
    Me.lbTb_numdulchiu.Tooltip = ""
    Me.lbTb_numdulchiu.UseMnemonic = False
    '
    'edTb_numaulchiu
    '
    Me.edTb_numaulchiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_numaulchiu.EditValue = "0"
    Me.edTb_numaulchiu.Location = New System.Drawing.Point(411, 69)
    Me.edTb_numaulchiu.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_numaulchiu.Name = "edTb_numaulchiu"
    Me.edTb_numaulchiu.NTSDbField = ""
    Me.edTb_numaulchiu.NTSFormat = "0"
    Me.edTb_numaulchiu.NTSForzaVisZoom = False
    Me.edTb_numaulchiu.NTSOldValue = ""
    Me.edTb_numaulchiu.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_numaulchiu.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_numaulchiu.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_numaulchiu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_numaulchiu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_numaulchiu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_numaulchiu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_numaulchiu.Properties.MaxLength = 65536
    Me.edTb_numaulchiu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_numaulchiu.Size = New System.Drawing.Size(100, 20)
    Me.edTb_numaulchiu.TabIndex = 40
    '
    'lbTb_numaulchiu
    '
    Me.lbTb_numaulchiu.AutoSize = True
    Me.lbTb_numaulchiu.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_numaulchiu.Location = New System.Drawing.Point(10, 72)
    Me.lbTb_numaulchiu.Name = "lbTb_numaulchiu"
    Me.lbTb_numaulchiu.NTSDbField = ""
    Me.lbTb_numaulchiu.Size = New System.Drawing.Size(145, 13)
    Me.lbTb_numaulchiu.TabIndex = 36
    Me.lbTb_numaulchiu.Text = "Numeri avere ultima chiusura"
    Me.lbTb_numaulchiu.Tooltip = ""
    Me.lbTb_numaulchiu.UseMnemonic = False
    '
    'lbTb_dtuladint
    '
    Me.lbTb_dtuladint.AutoSize = True
    Me.lbTb_dtuladint.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtuladint.Location = New System.Drawing.Point(10, 28)
    Me.lbTb_dtuladint.Name = "lbTb_dtuladint"
    Me.lbTb_dtuladint.NTSDbField = ""
    Me.lbTb_dtuladint.Size = New System.Drawing.Size(149, 13)
    Me.lbTb_dtuladint.TabIndex = 35
    Me.lbTb_dtuladint.Text = "Data ultimo addebito interessi"
    Me.lbTb_dtuladint.Tooltip = ""
    Me.lbTb_dtuladint.UseMnemonic = False
    '
    'lbTb_saldovalulch
    '
    Me.lbTb_saldovalulch.AutoSize = True
    Me.lbTb_saldovalulch.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_saldovalulch.Location = New System.Drawing.Point(10, 138)
    Me.lbTb_saldovalulch.Name = "lbTb_saldovalulch"
    Me.lbTb_saldovalulch.NTSDbField = ""
    Me.lbTb_saldovalulch.Size = New System.Drawing.Size(152, 13)
    Me.lbTb_saldovalulch.TabIndex = 34
    Me.lbTb_saldovalulch.Text = "Saldo valuta all'ultima chiusura"
    Me.lbTb_saldovalulch.Tooltip = ""
    Me.lbTb_saldovalulch.UseMnemonic = False
    '
    'lbTb_saldoulchiu
    '
    Me.lbTb_saldoulchiu.AutoSize = True
    Me.lbTb_saldoulchiu.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_saldoulchiu.Location = New System.Drawing.Point(10, 116)
    Me.lbTb_saldoulchiu.Name = "lbTb_saldoulchiu"
    Me.lbTb_saldoulchiu.NTSDbField = ""
    Me.lbTb_saldoulchiu.Size = New System.Drawing.Size(165, 13)
    Me.lbTb_saldoulchiu.TabIndex = 33
    Me.lbTb_saldoulchiu.Text = "Saldo contabile all'ultima chiusura"
    Me.lbTb_saldoulchiu.Tooltip = ""
    Me.lbTb_saldoulchiu.UseMnemonic = False
    '
    'lbTb_dtulchiu
    '
    Me.lbTb_dtulchiu.AutoSize = True
    Me.lbTb_dtulchiu.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulchiu.Location = New System.Drawing.Point(10, 50)
    Me.lbTb_dtulchiu.Name = "lbTb_dtulchiu"
    Me.lbTb_dtulchiu.NTSDbField = ""
    Me.lbTb_dtulchiu.Size = New System.Drawing.Size(104, 13)
    Me.lbTb_dtulchiu.TabIndex = 32
    Me.lbTb_dtulchiu.Text = "Data ultima chiusura"
    Me.lbTb_dtulchiu.Tooltip = ""
    Me.lbTb_dtulchiu.UseMnemonic = False
    '
    'lbTb_dtulacint
    '
    Me.lbTb_dtulacint.AutoSize = True
    Me.lbTb_dtulacint.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtulacint.Location = New System.Drawing.Point(10, 6)
    Me.lbTb_dtulacint.Name = "lbTb_dtulacint"
    Me.lbTb_dtulacint.NTSDbField = ""
    Me.lbTb_dtulacint.Size = New System.Drawing.Size(151, 13)
    Me.lbTb_dtulacint.TabIndex = 31
    Me.lbTb_dtulacint.Text = "Data ultimo accredito interessi"
    Me.lbTb_dtulacint.Tooltip = ""
    Me.lbTb_dtulacint.UseMnemonic = False
    '
    'lbTb_saldovalulad
    '
    Me.lbTb_saldovalulad.AutoSize = True
    Me.lbTb_saldovalulad.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_saldovalulad.Location = New System.Drawing.Point(10, 182)
    Me.lbTb_saldovalulad.Name = "lbTb_saldovalulad"
    Me.lbTb_saldovalulad.NTSDbField = ""
    Me.lbTb_saldovalulad.Size = New System.Drawing.Size(198, 13)
    Me.lbTb_saldovalulad.TabIndex = 30
    Me.lbTb_saldovalulad.Text = "Saldo valuta ultimo addeb. inter. passivi"
    Me.lbTb_saldovalulad.Tooltip = ""
    Me.lbTb_saldovalulad.UseMnemonic = False
    '
    'edTb_saldovalulad
    '
    Me.edTb_saldovalulad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_saldovalulad.EditValue = "0"
    Me.edTb_saldovalulad.Location = New System.Drawing.Point(411, 179)
    Me.edTb_saldovalulad.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_saldovalulad.Name = "edTb_saldovalulad"
    Me.edTb_saldovalulad.NTSDbField = ""
    Me.edTb_saldovalulad.NTSFormat = "0"
    Me.edTb_saldovalulad.NTSForzaVisZoom = False
    Me.edTb_saldovalulad.NTSOldValue = ""
    Me.edTb_saldovalulad.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_saldovalulad.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_saldovalulad.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_saldovalulad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_saldovalulad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_saldovalulad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_saldovalulad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_saldovalulad.Properties.MaxLength = 65536
    Me.edTb_saldovalulad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_saldovalulad.Size = New System.Drawing.Size(100, 20)
    Me.edTb_saldovalulad.TabIndex = 29
    '
    'lbTb_saldovalulac
    '
    Me.lbTb_saldovalulac.AutoSize = True
    Me.lbTb_saldovalulac.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_saldovalulac.Location = New System.Drawing.Point(10, 160)
    Me.lbTb_saldovalulac.Name = "lbTb_saldovalulac"
    Me.lbTb_saldovalulac.NTSDbField = ""
    Me.lbTb_saldovalulac.Size = New System.Drawing.Size(192, 13)
    Me.lbTb_saldovalulac.TabIndex = 28
    Me.lbTb_saldovalulac.Text = "Saldo valuta ultimo accred. inter. attivi"
    Me.lbTb_saldovalulac.Tooltip = ""
    Me.lbTb_saldovalulac.UseMnemonic = False
    '
    'edTb_saldovalulac
    '
    Me.edTb_saldovalulac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_saldovalulac.EditValue = "0"
    Me.edTb_saldovalulac.Location = New System.Drawing.Point(411, 157)
    Me.edTb_saldovalulac.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_saldovalulac.Name = "edTb_saldovalulac"
    Me.edTb_saldovalulac.NTSDbField = ""
    Me.edTb_saldovalulac.NTSFormat = "0"
    Me.edTb_saldovalulac.NTSForzaVisZoom = False
    Me.edTb_saldovalulac.NTSOldValue = ""
    Me.edTb_saldovalulac.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_saldovalulac.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_saldovalulac.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_saldovalulac.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_saldovalulac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_saldovalulac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_saldovalulac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_saldovalulac.Properties.MaxLength = 65536
    Me.edTb_saldovalulac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_saldovalulac.Size = New System.Drawing.Size(100, 20)
    Me.edTb_saldovalulac.TabIndex = 27
    '
    'edTb_saldovalulch
    '
    Me.edTb_saldovalulch.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_saldovalulch.EditValue = "0"
    Me.edTb_saldovalulch.Location = New System.Drawing.Point(411, 135)
    Me.edTb_saldovalulch.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_saldovalulch.Name = "edTb_saldovalulch"
    Me.edTb_saldovalulch.NTSDbField = ""
    Me.edTb_saldovalulch.NTSFormat = "0"
    Me.edTb_saldovalulch.NTSForzaVisZoom = False
    Me.edTb_saldovalulch.NTSOldValue = ""
    Me.edTb_saldovalulch.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_saldovalulch.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_saldovalulch.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_saldovalulch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_saldovalulch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_saldovalulch.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_saldovalulch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_saldovalulch.Properties.MaxLength = 65536
    Me.edTb_saldovalulch.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_saldovalulch.Size = New System.Drawing.Size(100, 20)
    Me.edTb_saldovalulch.TabIndex = 26
    '
    'edTb_saldoulchiu
    '
    Me.edTb_saldoulchiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_saldoulchiu.EditValue = "0"
    Me.edTb_saldoulchiu.Location = New System.Drawing.Point(411, 113)
    Me.edTb_saldoulchiu.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_saldoulchiu.Name = "edTb_saldoulchiu"
    Me.edTb_saldoulchiu.NTSDbField = ""
    Me.edTb_saldoulchiu.NTSFormat = "0"
    Me.edTb_saldoulchiu.NTSForzaVisZoom = False
    Me.edTb_saldoulchiu.NTSOldValue = ""
    Me.edTb_saldoulchiu.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_saldoulchiu.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_saldoulchiu.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_saldoulchiu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_saldoulchiu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_saldoulchiu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_saldoulchiu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_saldoulchiu.Properties.MaxLength = 65536
    Me.edTb_saldoulchiu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_saldoulchiu.Size = New System.Drawing.Size(100, 20)
    Me.edTb_saldoulchiu.TabIndex = 24
    '
    'FRMVEBANC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(537, 437)
    Me.Controls.Add(Me.pnBanc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVEBANC"
    Me.Text = "BANCHE"
    CType(Me.edTb_codbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_abi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rifriba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_csbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBanc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBanc.ResumeLayout(False)
    Me.pnBanc.PerformLayout()
    CType(Me.edTb_conanticip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsBanc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsBanc.ResumeLayout(False)
    Me.tsBancPage0.ResumeLayout(False)
    CType(Me.pnBancPage0, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBancPage0.ResumeLayout(False)
    Me.pnBancPage0.PerformLayout()
    CType(Me.fmIbanItalia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIbanItalia.ResumeLayout(False)
    Me.fmIbanItalia.PerformLayout()
    CType(Me.edTb_rididcred.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcuc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_swift.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_prefiban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_fido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ritfisc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_iban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tiprap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsBancPage1.ResumeLayout(False)
    CType(Me.pnBancPage1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBancPage1.ResumeLayout(False)
    Me.pnBancPage1.PerformLayout()
    CType(Me.edTb_maxscop2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_maxscop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_impboll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codbdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_speseop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_spechiusann.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tenutcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_opcompr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tiporapdf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsBancPage2.ResumeLayout(False)
    CType(Me.pnBancPage2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBancPage2.ResumeLayout(False)
    Me.pnBancPage2.PerformLayout()
    CType(Me.cbTb_addimpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_accinat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_scaglconsm2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_scaglconsm1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_scaglcms.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsBancPage3.ResumeLayout(False)
    CType(Me.pnBancPage3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBancPage3.ResumeLayout(False)
    Me.pnBancPage3.PerformLayout()
    CType(Me.edTb_saldoanticipaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulchiu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtuladint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtulacint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_numdulchiu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_numaulchiu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_saldovalulad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_saldovalulac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_saldovalulch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_saldoulchiu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMVEBANC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcBanc.Dispose()
      dsBanc.Dispose()
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'Carico i combobox
      CaricaCombo()

      'Predispongo i controlli
      InitControls()

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Try
      LeggiDatiDitta()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsBanc.Tables("TABBANC").Rows.Count = 1 And dsBanc.Tables("TABBANC").Rows(0).RowState = DataRowState.Added Then
            'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcBanc.List.Count - 1
            If CType(dcBanc.Item(i), DataRowView)!tb_codbanc.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcBanc.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTiprap As New DataTable()
    Dim dttAccinat As New DataTable()
    Dim dttAddimpa As New DataTable()
    Try
      dttTiprap.Columns.Add("cod", GetType(String))
      dttTiprap.Columns.Add("val", GetType(String))
      dttTiprap.Rows.Add(New Object() {"B", "C/C"})
      dttTiprap.Rows.Add(New Object() {"C", "C/SBF"})
      dttTiprap.Rows.Add(New Object() {"D", "Anticipi Estero"})
      dttTiprap.Rows.Add(New Object() {"E", "Anticipi Italia"})
      dttTiprap.Rows.Add(New Object() {"Z", "C/Antic. da SBF"})
      dttTiprap.AcceptChanges()
      cbTb_tiprap.DataSource = dttTiprap
      cbTb_tiprap.ValueMember = "cod"
      cbTb_tiprap.DisplayMember = "val"

      dttAccinat.Columns.Add("cod", GetType(Integer))
      dttAccinat.Columns.Add("val", GetType(String))
      dttAccinat.Rows.Add(New Object() {0, "Annuale"})
      dttAccinat.Rows.Add(New Object() {1, "Mensile"})
      dttAccinat.Rows.Add(New Object() {2, "Bimestrale"})
      dttAccinat.Rows.Add(New Object() {3, "Trimestrale"})
      dttAccinat.Rows.Add(New Object() {4, "Quadrimestrale"})
      dttAccinat.Rows.Add(New Object() {6, "Semestrale"})
      dttAccinat.AcceptChanges()
      cbTb_accinat.DataSource = dttAccinat
      cbTb_accinat.ValueMember = "cod"
      cbTb_accinat.DisplayMember = "val"

      dttAddimpa.Columns.Add("cod", GetType(Integer))
      dttAddimpa.Columns.Add("val", GetType(String))
      dttAddimpa.Rows.Add(New Object() {0, "Annuale"})
      dttAddimpa.Rows.Add(New Object() {1, "Mensile"})
      dttAddimpa.Rows.Add(New Object() {2, "Bimestrale"})
      dttAddimpa.Rows.Add(New Object() {3, "Trimestrale"})
      dttAddimpa.Rows.Add(New Object() {4, "Quadrimestrale"})
      dttAddimpa.Rows.Add(New Object() {6, "Semestrale"})
      dttAddimpa.AcceptChanges()
      cbTb_addimpa.DataSource = dttAddimpa
      cbTb_addimpa.ValueMember = "cod"
      cbTb_addimpa.DisplayMember = "val"


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
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
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------------------
      'Completo le informazioni dei controlli
      edTb_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322337308, "Cod. Banca"), tabbanc)
      edTb_desbanc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023322493481, "Descr. Banca"), 30, True)
      edTb_conbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322649654, "Cod. Conto P.d.C."), tabanagras)
      edTb_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322805827, "Cod. ABI"), tababi)
      edTb_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322962000, "Cod. CAB"), tababicab)
      edTb_swift.NTSSetParam(oMenu, oApp.Tr(Me, 130187110440982855, "Codice BIC/SWIFT"), 14, True)
      edTb_codcuc.NTSSetParam(oMenu, oApp.Tr(Me, 130187110465382524, "Codice CUC/Identificativo creditore"), 35, True)
      edTb_rididcred.NTSSetParam(oMenu, oApp.Tr(Me, 130198267672870756, "RID: Identificativo creditore"), 35, True)
      edTb_rifriba.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323118173, "Cod. SIA"), 12, True)
      edTb_csbf.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323274346, "N° Conto"), 12, True)
      edTb_fido.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323430519, "Importo Fido"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_ritfisc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323586692, "% Ritenuta Fiscale"), oApp.FormatSconti, 6, 0, 100)
      cbTb_tiprap.NTSSetParam(oApp.Tr(Me, 128230023323742865, "Tipo di rapporto"))
      edTb_desbanc2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323899038, "Descr. agg. 1"), 30, True)
      edTb_desbanc3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324055211, "Descr. agg. 2"), 30, True)
      edTb_iban.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324211384, "Cod. IBAN"), 50, True)
      edTb_opcompr.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324367557, "Operazioni annuali incluse"), "0", 4, 0, 9999)
      edTb_tenutcc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324523730, "Spese fisse conto corrente"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_spechiusann.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324679903, "Spese tenuta conto corrente annuali"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_speseop.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324836076, "Spese per operazione"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_impboll.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324992249, "Imposta bolli"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_maxscop.NTSSetParam(oMenu, oApp.Tr(Me, 128230023325148422, "% Massimo scoperto"), "0.000", 7, 0, 100)
      edTb_maxscop2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023325304595, "% Massimo scoperto oltre scaglione"), "0.000", 7, 0, 100)
      edTb_scaglcms.NTSSetParam(oMenu, oApp.Tr(Me, 128230023325460768, "Importo scaglione per massimo scoperto"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_scaglconsm1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023325616941, "Importo scaglione per cons. media 1"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_scaglconsm2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023325773114, "Importo scaglione per cons. media 2"), oApp.FormatImporti, 20, 0, 9999999999999)
      cbTb_accinat.NTSSetParam(oApp.Tr(Me, 128230023325929287, "Periodo accredito interessi attivi"))
      cbTb_addimpa.NTSSetParam(oApp.Tr(Me, 128230023326085460, "Periodo accredito interessi passivi"))
      edTb_dtulacint.NTSSetParam(oMenu, oApp.Tr(Me, 128230023326241633, "Data ultimo accredito interessi"), False)
      edTb_dtuladint.NTSSetParam(oMenu, oApp.Tr(Me, 128230023326397806, "Data ultimo addebito interessi"), False)
      edTb_dtulchiu.NTSSetParam(oMenu, oApp.Tr(Me, 128230023326553979, "Data ultima chiusura"), False)
      edTb_numaulchiu.NTSSetParam(oMenu, oApp.Tr(Me, 128230023326710152, "Numeri avere ultima chiusura"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_numdulchiu.NTSSetParam(oMenu, oApp.Tr(Me, 128230023326866325, "Numeri dare ultima chiusura"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_saldoulchiu.NTSSetParam(oMenu, oApp.Tr(Me, 128230023327022498, "Saldo contabile all'ultima chiusura"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_saldovalulch.NTSSetParam(oMenu, oApp.Tr(Me, 128230023327178671, "Saldo valuta all'ultima chiusura"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_saldovalulac.NTSSetParam(oMenu, oApp.Tr(Me, 128230023327334844, "Saldo valuta ultimo accredito interessi attivi"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_saldovalulad.NTSSetParam(oMenu, oApp.Tr(Me, 128230023327491017, "Saldo valuta ultimo addebito interessi passivi"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_cin.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751612000, "Cin"), 1, True)
      edTb_prefiban.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751924000, "Prefisso IBAN"), 4, True)
      edTb_codbdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129471855433835768, "Cod. banca Doc.Finance"), 8, True)
      edTb_tiporapdf.NTSSetParam(oMenu, oApp.Tr(Me, 129471855458363878, "Tipo Rapporto Doc.Finance"), 4, True)
      edTb_saldoanticipaz.NTSSetParam(oMenu, oApp.Tr(Me, 129555555576015625, "Saldo anticipazioni su C/C da conto portafoglio"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      edTb_conanticip.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129555559109316406, "Conto x giroconto anticipazioni su C/C"), tabanagras)

      edTb_codbanc.NTSForzaVisZoom = False 'Non permette lo ZOOM
      edTb_desbanc.NTSForzaVisZoom = True
      edTb_conbanc.NTSForzaVisZoom = True
      edTb_abi.NTSForzaVisZoom = True
      edTb_cab.NTSForzaVisZoom = True

      edTb_desbanc.NTSSetParamZoom("")

      edTb_codbanc.NTSSetRichiesto()

      '---------------------------------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'Se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'Collego il BindingSource ai vari controlli 
      edTb_codbanc.NTSDbField = "tabbanc.tb_codbanc"
      edTb_desbanc.NTSDbField = "tabbanc.tb_desbanc"
      edTb_conbanc.NTSDbField = "tabbanc.tb_conbanc"
      edTb_abi.NTSDbField = "tabbanc.tb_abi"
      edTb_cab.NTSDbField = "tabbanc.tb_cab"
      edTb_rifriba.NTSDbField = "tabbanc.tb_rifriba"
      edTb_swift.NTSDbField = "tabbanc.tb_swift"
      edTb_codcuc.NTSDbField = "tabbanc.tb_codcuc"
      edTb_rididcred.NTSDbField = "tabbanc.tb_rididcred"
      edTb_csbf.NTSDbField = "tabbanc.tb_csbf"
      edTb_fido.NTSDbField = "tabbanc.tb_fido"
      edTb_ritfisc.NTSDbField = "tabbanc.tb_ritfisc"
      cbTb_tiprap.NTSDbField = "tabbanc.tb_tiprap"
      edTb_desbanc2.NTSDbField = "tabbanc.tb_desbanc2"
      edTb_desbanc3.NTSDbField = "tabbanc.tb_desbanc3"
      edTb_iban.NTSDbField = "tabbanc.tb_iban"
      edTb_opcompr.NTSDbField = "tabbanc.tb_opcompr"
      edTb_tenutcc.NTSDbField = "tabbanc.tb_tenutcc"
      edTb_spechiusann.NTSDbField = "tabbanc.tb_spechiusann"
      edTb_speseop.NTSDbField = "tabbanc.tb_speseop"
      edTb_impboll.NTSDbField = "tabbanc.tb_impboll"
      edTb_maxscop.NTSDbField = "tabbanc.tb_maxscop"
      edTb_maxscop2.NTSDbField = "tabbanc.tb_maxscop2"
      edTb_scaglcms.NTSDbField = "tabbanc.tb_scaglcms"
      edTb_scaglconsm1.NTSDbField = "tabbanc.tb_scaglconsm1"
      edTb_scaglconsm2.NTSDbField = "tabbanc.tb_scaglconsm2"
      cbTb_accinat.NTSDbField = "tabbanc.tb_accinat"
      cbTb_addimpa.NTSDbField = "tabbanc.tb_addimpa"
      edTb_dtulacint.NTSDbField = "tabbanc.tb_dtulacint"
      edTb_dtuladint.NTSDbField = "tabbanc.tb_dtuladint"
      edTb_dtulchiu.NTSDbField = "tabbanc.tb_dtulchiu"
      edTb_numaulchiu.NTSDbField = "tabbanc.tb_numaulchiu"
      edTb_numdulchiu.NTSDbField = "tabbanc.tb_numdulchiu"
      edTb_saldoulchiu.NTSDbField = "tabbanc.tb_saldoulchiu"
      edTb_saldovalulch.NTSDbField = "tabbanc.tb_saldovalulch"
      edTb_saldovalulac.NTSDbField = "tabbanc.tb_saldovalulac"
      edTb_saldovalulad.NTSDbField = "tabbanc.tb_saldovalulad"
      edTb_cin.NTSDbField = "tabbanc.tb_cin"
      edTb_prefiban.NTSDbField = "tabbanc.tb_prefiban"
      edTb_codbdoc.NTSDbField = "tabbanc.tb_codbdoc"
      edTb_tiporapdf.NTSDbField = "tabbanc.tb_tiporapdf"
      edTb_saldoanticipaz.NTSDbField = "tabbanc.tb_saldoanticipaz"
      edTb_conanticip.NTSDbField = "tabbanc.tb_conanticip"
      lbXx_conbanc.NTSDbField = "tabbanc.xx_conbanc"
      lbXx_conanticip.NTSDbField = "tabbanc.xx_conanticip"

      'ckTb_shiftm.NTSText.NTSDbField = "tabpaga.tb_shiftm"
      'ckTb_flcondp.NTSText.NTSDbField = "tabpaga.tb_flcondp"

      'lbXx_concassp.NTSDbField = "tabpaga.xx_concassp"

      '-------------------------------------------------
      'Per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcBanc, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub LeggiDatiDitta()
    Dim bAnagen As Boolean = False
    Dim bDbMultiDitta As Boolean = False
    Try
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      'se oCallParams.strOpen <> "" non visualizzera lo zoom ditte e prednerà la ditta passata da lchild padre
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BNVEBANC", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Me.Close()

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      '-------------------------------------------------
      'Leggo le informazioni relative alla ditta corrente e le passo all'ENTITY
      oCleBanc.strDittaCorrente = DittaCorrente
      oCleBanc.bModCG = CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCG)            'Memorizzo nell'entity la presenza del modulo di CG
      oCleBanc.bModExtCGE = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCGE) 'Memorizzo nell'entity la presenza del modulo di CGE

      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)

      tsBanc.SelectedTabPageIndex = 0

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If Not oCleBanc.Apri(DittaCorrente, dsBanc) Then Me.Close()
      dcBanc.DataSource = dsBanc.Tables("TABBANC")
      dsBanc.AcceptChanges()
      If dsBanc.Tables("TABBANC").Rows.Count > 0 Then
        edTb_codbanc.Enabled = False
      End If

      '-------------------------------------------------
      'Collego il BindingSource ai vari controlli 
      Bindcontrols()
      '-------------------------------------------------

      'Refresh dei controlli
      If dsBanc.Tables("TABBANC").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcBanc.ResetBindings(False) 'False = non è cambiato il datatable sottostante (da noi è sempre così)
      dcBanc.MoveFirst()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'Chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      If oCleBanc.RecordIsChanged Then

        'Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550313281250, "Salvare le modifiche apportate alla Banca?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleBanc.Salva(False) Then Return False
          If dsBanc.Tables("TABBANC").Rows.Count > 0 Then
            edTb_codbanc.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      'Preparo il motore di stampa
      strCrpe = "{tabbanc.codditt} = " & CStrSQL(DittaCorrente)
      nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBANC", "Reports1", " ", 0, nDestin, "BSVEBANC.RPT", False, "Banche", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'Lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Toolbar"

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'Vado sul primo record
      If Not Salva() Then Return
      dcBanc.MoveFirst()
      ColoraCampoAbiCab()
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
      dcBanc.MovePrevious()
      ColoraCampoAbiCab()
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
      dcBanc.MoveNext()
      ColoraCampoAbiCab()
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
      dcBanc.MoveLast()
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'Creo una nuova Banca
      If Not Salva() Then Return
      oCleBanc.Nuovo()
      dcBanc.MoveLast()
      edTb_codbanc.Enabled = True
      edTb_codbanc.Focus()

      '-------------------------------------------------
      'Imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try

      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleBanc.dsShared.Tables("TABBANC").Rows(dcBanc.Position).RowState <> DataRowState.Added Then
        If oCleBanc.TestPreCancella(NTSCInt(edTb_codbanc.Text), strMsg) = False Then
          oApp.MsgBoxInfo(strMsg)
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'Cancello la Banca
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550430781250, "Cancellare la Banca?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsBanc.Tables("TABBANC").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcBanc.RemoveAt(dcBanc.Position)
          oCleBanc.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcBanc, Me)
            bRemovBinding = False
            edTb_codbanc.Enabled = True
          Else
            edTb_codbanc.Enabled = False
          End If
          Return
      End Select
      ColoraCampoAbiCab()
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcBanc, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'Ripristino la Banca
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550517812500, "Ripristinare le modifiche apportate alla Banca?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsBanc.Tables("TABBANC").Rows.Count = 1 And dsBanc.Tables("TABBANC").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleBanc.Ripristina(dcBanc.Position, dcBanc.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcBanc, Me)
            bRemovBinding = False
            edTb_codbanc.Enabled = True
          Else
            edTb_codbanc.Enabled = False
          End If
      End Select
      ColoraCampoAbiCab()
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcBanc, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim i As Integer
    Dim oParam As New CLE__PATB
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
      If edTb_desbanc.Focused Then

        Me.ValidaLastControl()

        'Lancio lo zoom solo per riposizionare il BindingSource
        If oCleBanc.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128777131353619988, "Salvare o Ripristinare le modifiche apportate alla Banca prima di selezionarne una nuova."))
          Return
        End If

        NTSZOOM.strIn = edTb_codbanc.Text
        NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codbanc.Text Then
          'Mi sposto sul record selezionato
          For i = 0 To dcBanc.List.Count - 1
            If CType(dcBanc.Item(i), DataRowView)!tb_codbanc.ToString = NTSZOOM.strIn Then
              dcBanc.Position = i
              Exit For
            End If
          Next
        End If
      ElseIf edTb_conbanc.Focused Then
        '--------------------------------------------
        'Zoom ANAGRAS
        SetFastZoom(edTb_conbanc.Text, oParam)    'abilito la gestione dello zoom veloce
        'oParam.bVisGriglia = True               'non serve è il default
        'oParam.strTipo = "S"                    'non serve, è scontato
        oParam.bTipoProposto = False
        NTSZOOM.strIn = edTb_conbanc.Text
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conbanc.Text Then
          edTb_conbanc.NTSTextDB = NTSZOOM.strIn
        End If
      ElseIf edTb_cab.Focused Then
        'Zoom CAB
        SetFastZoom(edTb_cab.Text, oParam)    'Abilito la gestione dello ZOOM veloce
        oParam.strDescr = edTb_abi.Text       'Passo il codice ABI
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_cab.Text Then
          edTb_cab.NTSTextDB = NTSZOOM.strIn
        End If
      Else
        'Per eventuali altri controlli caricati al volo
        'Zoom standard di Textbox e Griglia
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
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
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      If Not Salva() Then Return
      LeggiDatiDitta()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCondizioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCondizioni.ItemClick
    Dim frmCoba As FRMTECOBA = Nothing

    Try
      If Not Salva() Then Return

      oCleBanc.lCodBanc = NTSCInt(edTb_codbanc.Text)

      frmCoba = CType(NTSNewFormModal("FRMTECOBA"), FRMTECOBA)
      If Not frmCoba.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmCoba.InitEntity(oCleBanc)
      frmCoba.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoba Is Nothing Then frmCoba.Dispose()
      frmCoba = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbTassi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTassi.ItemClick
    Dim frmTass As FRMTETASS = Nothing

    Try
      If Not Salva() Then Return

      oCleBanc.lCodBanc = NTSCInt(edTb_codbanc.Text)

      frmTass = CType(NTSNewFormModal("FRMTETASS"), FRMTETASS)
      If Not frmTass.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmTass.InitEntity(oCleBanc)
      frmTass.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmTass Is Nothing Then frmTass.Dispose()
      frmTass = Nothing
    End Try
  End Sub

#End Region


  Public Overridable Sub ColoraCampoAbiCab()
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(edTb_abi.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edTb_abi.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edTb_abi.Text, DittaCorrente, "ABI", "N", , dttTmp)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abiabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edTb_abi.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edTb_abi.BackColor = Color.Transparent
        End If
      End If

      If NTSCInt(edTb_cab.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edTb_cab.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edTb_cab.Text, DittaCorrente, "CAB", "N", , dttTmp, edTb_abi.Text)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abcabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edTb_cab.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edTb_cab.BackColor = Color.Transparent
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
