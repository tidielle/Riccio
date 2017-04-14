Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ISTP
  Public oCleIstp As CLE__ISTP
  Public oCallParams As CLE__CLDP

  Public dsIstp As DataSet
  Public dcIstp As BindingSource = New BindingSource

  Public bOnLoading As Boolean = True

#Region "Moduli"
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
#End Region

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Sqlsql As NTSInformatica.NTSLabel
  Public WithEvents edPz_Sqlsql As NTSInformatica.NTSMemoBox
  Public WithEvents edPz_maintab As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Titstam As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Nomrpt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Codform As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPz_maintab As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Nomrpt As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Titstam As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Codform As NTSInformatica.NTSLabel
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents cmdOpNot As NTSInformatica.NTSButton
  Public WithEvents cmdOpOr As NTSInformatica.NTSButton
  Public WithEvents cmdOpAnd As NTSInformatica.NTSButton
  Public WithEvents cmdChiudip As NTSInformatica.NTSButton
  Public WithEvents cmdAprip As NTSInformatica.NTSButton
  Public WithEvents tsFiltri As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Val1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Op1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Tipo1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Column1 As NTSInformatica.NTSLabel
  Public WithEvents lb5 As NTSInformatica.NTSLabel
  Public WithEvents lb4 As NTSInformatica.NTSLabel
  Public WithEvents lb3 As NTSInformatica.NTSLabel
  Public WithEvents lb2 As NTSInformatica.NTSLabel
  Public WithEvents lb1 As NTSInformatica.NTSLabel
  Public WithEvents cmdnFiltro5 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro4 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro3 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro2 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro1 As NTSInformatica.NTSButton
  Public WithEvents edPz_Val5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op5 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op4 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op3 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op2 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op1 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo5 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo4 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo3 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo2 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo1 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Column5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_Descam1 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descam5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag2 As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Val6 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Op6 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Tipo6 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Column6 As NTSInformatica.NTSLabel
  Public WithEvents lb10 As NTSInformatica.NTSLabel
  Public WithEvents lb9 As NTSInformatica.NTSLabel
  Public WithEvents lb8 As NTSInformatica.NTSLabel
  Public WithEvents lb7 As NTSInformatica.NTSLabel
  Public WithEvents lb6 As NTSInformatica.NTSLabel
  Public WithEvents cmdnFiltro10 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro9 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro8 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro7 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro6 As NTSInformatica.NTSButton
  Public WithEvents edPz_Val10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op10 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op9 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op8 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op7 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op6 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo10 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo9 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo8 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo7 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo6 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Column10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_Descam6 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descam10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnPage3 As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Val11 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Op11 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Tipo11 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Column11 As NTSInformatica.NTSLabel
  Public WithEvents lb15 As NTSInformatica.NTSLabel
  Public WithEvents lb14 As NTSInformatica.NTSLabel
  Public WithEvents lb13 As NTSInformatica.NTSLabel
  Public WithEvents lb12 As NTSInformatica.NTSLabel
  Public WithEvents lb11 As NTSInformatica.NTSLabel
  Public WithEvents cmdnFiltro15 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro14 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro13 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro12 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltro11 As NTSInformatica.NTSButton
  Public WithEvents edPz_Val15 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val14 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val13 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val12 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Val11 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Op15 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op14 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op13 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op12 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Op11 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo15 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo14 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo13 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo12 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipo11 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Column15 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column14 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column13 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column12 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Column11 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_Descam11 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descam15 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam14 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam13 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam12 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descam11 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_strOprel As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDisplayfiltri2 As NTSInformatica.NTSLabel
  Public WithEvents lbDisplayfiltri1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_strOprel2 As NTSInformatica.NTSLabel
  Public WithEvents edPz_strOprel2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbPz_Vals1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Ops1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Tipos1 As NTSInformatica.NTSLabel
  Public WithEvents lbPz_Columns1 As NTSInformatica.NTSLabel
  Public WithEvents lbSubQ5 As NTSInformatica.NTSLabel
  Public WithEvents lbSubQ4 As NTSInformatica.NTSLabel
  Public WithEvents lbSubQ3 As NTSInformatica.NTSLabel
  Public WithEvents lbSubQ2 As NTSInformatica.NTSLabel
  Public WithEvents lbSubQ1 As NTSInformatica.NTSLabel
  Public WithEvents cmdnFiltros5 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltros4 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltros3 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltros2 As NTSInformatica.NTSButton
  Public WithEvents cmdnFiltros1 As NTSInformatica.NTSButton
  Public WithEvents edPz_Vals5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Vals4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Vals3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Vals2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Vals1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbPz_Ops5 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Ops4 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Ops3 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Ops2 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Ops1 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipos5 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipos4 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipos3 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipos2 As NTSInformatica.NTSComboBox
  Public WithEvents cbPz_Tipos1 As NTSInformatica.NTSComboBox
  Public WithEvents edPz_Columns5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Columns4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Columns3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Columns2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Columns1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_Descams1 As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descams5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descams4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descams3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPz_Descams2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbAccessi As NTSInformatica.NTSBarButtonItem
  Public WithEvents edPz_xlsfoglio As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_xlsfoglio As NTSInformatica.NTSLabel
  Public WithEvents edPz_xlsfile As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPz_xlsfile As NTSInformatica.NTSLabel
  Public WithEvents edPz_Descams1 As NTSInformatica.NTSTextBoxStr
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ISTP", "BE__ISTP", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128306256286279549, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleIstp = CType(oTmp, CLE__ISTP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ISTP", strRemoteServer, strRemotePort)
    AddHandler oCleIstp.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleIstp.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ISTP))
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.lbPz_strOprel2 = New NTSInformatica.NTSLabel
    Me.edPz_strOprel2 = New NTSInformatica.NTSTextBoxStr
    Me.cmdOpNot = New NTSInformatica.NTSButton
    Me.cmdOpOr = New NTSInformatica.NTSButton
    Me.cmdOpAnd = New NTSInformatica.NTSButton
    Me.cmdChiudip = New NTSInformatica.NTSButton
    Me.cmdAprip = New NTSInformatica.NTSButton
    Me.tsFiltri = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.lbPz_Val1 = New NTSInformatica.NTSLabel
    Me.lbPz_Op1 = New NTSInformatica.NTSLabel
    Me.lbPz_Tipo1 = New NTSInformatica.NTSLabel
    Me.lbPz_Column1 = New NTSInformatica.NTSLabel
    Me.lb5 = New NTSInformatica.NTSLabel
    Me.lb4 = New NTSInformatica.NTSLabel
    Me.lb3 = New NTSInformatica.NTSLabel
    Me.lb2 = New NTSInformatica.NTSLabel
    Me.lb1 = New NTSInformatica.NTSLabel
    Me.cmdnFiltro5 = New NTSInformatica.NTSButton
    Me.cmdnFiltro4 = New NTSInformatica.NTSButton
    Me.cmdnFiltro3 = New NTSInformatica.NTSButton
    Me.cmdnFiltro2 = New NTSInformatica.NTSButton
    Me.cmdnFiltro1 = New NTSInformatica.NTSButton
    Me.edPz_Val5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val1 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op5 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op4 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op3 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op2 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op1 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo5 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo4 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo3 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo2 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo1 = New NTSInformatica.NTSComboBox
    Me.edPz_Column5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column1 = New NTSInformatica.NTSTextBoxStr
    Me.lbPz_Descam1 = New NTSInformatica.NTSLabel
    Me.edPz_Descam5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam1 = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPag2 = New NTSInformatica.NTSPanel
    Me.lbPz_Val6 = New NTSInformatica.NTSLabel
    Me.lbPz_Op6 = New NTSInformatica.NTSLabel
    Me.lbPz_Tipo6 = New NTSInformatica.NTSLabel
    Me.lbPz_Column6 = New NTSInformatica.NTSLabel
    Me.lb10 = New NTSInformatica.NTSLabel
    Me.lb9 = New NTSInformatica.NTSLabel
    Me.lb8 = New NTSInformatica.NTSLabel
    Me.lb7 = New NTSInformatica.NTSLabel
    Me.lb6 = New NTSInformatica.NTSLabel
    Me.cmdnFiltro10 = New NTSInformatica.NTSButton
    Me.cmdnFiltro9 = New NTSInformatica.NTSButton
    Me.cmdnFiltro8 = New NTSInformatica.NTSButton
    Me.cmdnFiltro7 = New NTSInformatica.NTSButton
    Me.cmdnFiltro6 = New NTSInformatica.NTSButton
    Me.edPz_Val10 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val9 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val8 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val7 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val6 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op10 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op9 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op8 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op7 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op6 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo10 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo9 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo8 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo7 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo6 = New NTSInformatica.NTSComboBox
    Me.edPz_Column10 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column9 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column8 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column7 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column6 = New NTSInformatica.NTSTextBoxStr
    Me.lbPz_Descam6 = New NTSInformatica.NTSLabel
    Me.edPz_Descam10 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam9 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam8 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam7 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam6 = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPage3 = New NTSInformatica.NTSPanel
    Me.lbPz_Val11 = New NTSInformatica.NTSLabel
    Me.lbPz_Op11 = New NTSInformatica.NTSLabel
    Me.lbPz_Tipo11 = New NTSInformatica.NTSLabel
    Me.lbPz_Column11 = New NTSInformatica.NTSLabel
    Me.lb15 = New NTSInformatica.NTSLabel
    Me.lb14 = New NTSInformatica.NTSLabel
    Me.lb13 = New NTSInformatica.NTSLabel
    Me.lb12 = New NTSInformatica.NTSLabel
    Me.lb11 = New NTSInformatica.NTSLabel
    Me.cmdnFiltro15 = New NTSInformatica.NTSButton
    Me.cmdnFiltro14 = New NTSInformatica.NTSButton
    Me.cmdnFiltro13 = New NTSInformatica.NTSButton
    Me.cmdnFiltro12 = New NTSInformatica.NTSButton
    Me.cmdnFiltro11 = New NTSInformatica.NTSButton
    Me.edPz_Val15 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val14 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val13 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val12 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Val11 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Op15 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op14 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op13 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op12 = New NTSInformatica.NTSComboBox
    Me.cbPz_Op11 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo15 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo14 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo13 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo12 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipo11 = New NTSInformatica.NTSComboBox
    Me.edPz_Column15 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column14 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column13 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column12 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Column11 = New NTSInformatica.NTSTextBoxStr
    Me.lbPz_Descam11 = New NTSInformatica.NTSLabel
    Me.edPz_Descam15 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam14 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam13 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam12 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descam11 = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbPz_Vals1 = New NTSInformatica.NTSLabel
    Me.lbPz_Ops1 = New NTSInformatica.NTSLabel
    Me.lbPz_Tipos1 = New NTSInformatica.NTSLabel
    Me.lbPz_Columns1 = New NTSInformatica.NTSLabel
    Me.lbSubQ5 = New NTSInformatica.NTSLabel
    Me.lbSubQ4 = New NTSInformatica.NTSLabel
    Me.lbSubQ3 = New NTSInformatica.NTSLabel
    Me.lbSubQ2 = New NTSInformatica.NTSLabel
    Me.lbSubQ1 = New NTSInformatica.NTSLabel
    Me.cmdnFiltros5 = New NTSInformatica.NTSButton
    Me.cmdnFiltros4 = New NTSInformatica.NTSButton
    Me.cmdnFiltros3 = New NTSInformatica.NTSButton
    Me.cmdnFiltros2 = New NTSInformatica.NTSButton
    Me.cmdnFiltros1 = New NTSInformatica.NTSButton
    Me.edPz_Vals5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Vals4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Vals3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Vals2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Vals1 = New NTSInformatica.NTSTextBoxStr
    Me.cbPz_Ops5 = New NTSInformatica.NTSComboBox
    Me.cbPz_Ops4 = New NTSInformatica.NTSComboBox
    Me.cbPz_Ops3 = New NTSInformatica.NTSComboBox
    Me.cbPz_Ops2 = New NTSInformatica.NTSComboBox
    Me.cbPz_Ops1 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipos5 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipos4 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipos3 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipos2 = New NTSInformatica.NTSComboBox
    Me.cbPz_Tipos1 = New NTSInformatica.NTSComboBox
    Me.edPz_Columns5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Columns4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Columns3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Columns2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Columns1 = New NTSInformatica.NTSTextBoxStr
    Me.lbPz_Descams1 = New NTSInformatica.NTSLabel
    Me.edPz_Descams5 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descams4 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descams3 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descams2 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Descams1 = New NTSInformatica.NTSTextBoxStr
    Me.edPz_strOprel = New NTSInformatica.NTSTextBoxStr
    Me.lbDisplayfiltri2 = New NTSInformatica.NTSLabel
    Me.lbDisplayfiltri1 = New NTSInformatica.NTSLabel
    Me.lbPz_Sqlsql = New NTSInformatica.NTSLabel
    Me.edPz_Sqlsql = New NTSInformatica.NTSMemoBox
    Me.edPz_maintab = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Titstam = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Nomrpt = New NTSInformatica.NTSTextBoxStr
    Me.edPz_Codform = New NTSInformatica.NTSTextBoxNum
    Me.lbPz_maintab = New NTSInformatica.NTSLabel
    Me.lbPz_Nomrpt = New NTSInformatica.NTSLabel
    Me.lbPz_Titstam = New NTSInformatica.NTSLabel
    Me.lbPz_Codform = New NTSInformatica.NTSLabel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbAccessi = New NTSInformatica.NTSBarButtonItem
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
    Me.lbPz_xlsfile = New NTSInformatica.NTSLabel
    Me.edPz_xlsfile = New NTSInformatica.NTSTextBoxStr
    Me.lbPz_xlsfoglio = New NTSInformatica.NTSLabel
    Me.edPz_xlsfoglio = New NTSInformatica.NTSTextBoxStr
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.edPz_strOprel2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsFiltri.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.edPz_Val5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2.SuspendLayout()
    CType(Me.edPz_Val10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPage3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage3.SuspendLayout()
    CType(Me.edPz_Val15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Val11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Op11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipo11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Column11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descam11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edPz_Vals5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Vals1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Ops1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipos5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipos4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipos3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipos2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPz_Tipos1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Columns5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Columns4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Columns3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Columns2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Columns1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Descams1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_strOprel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Sqlsql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_maintab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Titstam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Nomrpt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_Codform.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_xlsfile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPz_xlsfoglio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edPz_xlsfoglio)
    Me.pnTop.Controls.Add(Me.lbPz_xlsfoglio)
    Me.pnTop.Controls.Add(Me.edPz_xlsfile)
    Me.pnTop.Controls.Add(Me.lbPz_xlsfile)
    Me.pnTop.Controls.Add(Me.fmFiltri)
    Me.pnTop.Controls.Add(Me.lbPz_Sqlsql)
    Me.pnTop.Controls.Add(Me.edPz_Sqlsql)
    Me.pnTop.Controls.Add(Me.edPz_maintab)
    Me.pnTop.Controls.Add(Me.edPz_Titstam)
    Me.pnTop.Controls.Add(Me.edPz_Nomrpt)
    Me.pnTop.Controls.Add(Me.edPz_Codform)
    Me.pnTop.Controls.Add(Me.lbPz_maintab)
    Me.pnTop.Controls.Add(Me.lbPz_Nomrpt)
    Me.pnTop.Controls.Add(Me.lbPz_Titstam)
    Me.pnTop.Controls.Add(Me.lbPz_Codform)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(738, 428)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "NtsPanel2"
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.lbPz_strOprel2)
    Me.fmFiltri.Controls.Add(Me.edPz_strOprel2)
    Me.fmFiltri.Controls.Add(Me.cmdOpNot)
    Me.fmFiltri.Controls.Add(Me.cmdOpOr)
    Me.fmFiltri.Controls.Add(Me.cmdOpAnd)
    Me.fmFiltri.Controls.Add(Me.cmdChiudip)
    Me.fmFiltri.Controls.Add(Me.cmdAprip)
    Me.fmFiltri.Controls.Add(Me.tsFiltri)
    Me.fmFiltri.Controls.Add(Me.edPz_strOprel)
    Me.fmFiltri.Controls.Add(Me.lbDisplayfiltri2)
    Me.fmFiltri.Controls.Add(Me.lbDisplayfiltri1)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(8, 165)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(721, 257)
    Me.fmFiltri.TabIndex = 6
    Me.fmFiltri.Text = "Filtri Utente"
    '
    'lbPz_strOprel2
    '
    Me.lbPz_strOprel2.AutoSize = True
    Me.lbPz_strOprel2.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_strOprel2.Location = New System.Drawing.Point(12, 59)
    Me.lbPz_strOprel2.Name = "lbPz_strOprel2"
    Me.lbPz_strOprel2.NTSDbField = ""
    Me.lbPz_strOprel2.Size = New System.Drawing.Size(53, 13)
    Me.lbPz_strOprel2.TabIndex = 39
    Me.lbPz_strOprel2.Text = "Subquery"
    Me.lbPz_strOprel2.Tooltip = ""
    Me.lbPz_strOprel2.UseMnemonic = False
    '
    'edPz_strOprel2
    '
    Me.edPz_strOprel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_strOprel2.Location = New System.Drawing.Point(105, 56)
    Me.edPz_strOprel2.Name = "edPz_strOprel2"
    Me.edPz_strOprel2.NTSDbField = ""
    Me.edPz_strOprel2.NTSForzaVisZoom = False
    Me.edPz_strOprel2.NTSOldValue = ""
    Me.edPz_strOprel2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_strOprel2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_strOprel2.Properties.AutoHeight = False
    Me.edPz_strOprel2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_strOprel2.Properties.MaxLength = 65536
    Me.edPz_strOprel2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_strOprel2.Size = New System.Drawing.Size(604, 20)
    Me.edPz_strOprel2.TabIndex = 38
    '
    'cmdOpNot
    '
    Me.cmdOpNot.ImageText = ""
    Me.cmdOpNot.Location = New System.Drawing.Point(651, 219)
    Me.cmdOpNot.Name = "cmdOpNot"
    Me.cmdOpNot.NTSContextMenu = Nothing
    Me.cmdOpNot.Size = New System.Drawing.Size(58, 23)
    Me.cmdOpNot.TabIndex = 37
    Me.cmdOpNot.Text = "NOT"
    '
    'cmdOpOr
    '
    Me.cmdOpOr.ImageText = ""
    Me.cmdOpOr.Location = New System.Drawing.Point(651, 193)
    Me.cmdOpOr.Name = "cmdOpOr"
    Me.cmdOpOr.NTSContextMenu = Nothing
    Me.cmdOpOr.Size = New System.Drawing.Size(58, 23)
    Me.cmdOpOr.TabIndex = 36
    Me.cmdOpOr.Text = "OR"
    '
    'cmdOpAnd
    '
    Me.cmdOpAnd.ImageText = ""
    Me.cmdOpAnd.Location = New System.Drawing.Point(651, 167)
    Me.cmdOpAnd.Name = "cmdOpAnd"
    Me.cmdOpAnd.NTSContextMenu = Nothing
    Me.cmdOpAnd.Size = New System.Drawing.Size(58, 23)
    Me.cmdOpAnd.TabIndex = 35
    Me.cmdOpAnd.Text = "AND"
    '
    'cmdChiudip
    '
    Me.cmdChiudip.ImageText = ""
    Me.cmdChiudip.Location = New System.Drawing.Point(683, 141)
    Me.cmdChiudip.Name = "cmdChiudip"
    Me.cmdChiudip.NTSContextMenu = Nothing
    Me.cmdChiudip.Size = New System.Drawing.Size(26, 23)
    Me.cmdChiudip.TabIndex = 34
    Me.cmdChiudip.Text = ")"
    '
    'cmdAprip
    '
    Me.cmdAprip.ImageText = ""
    Me.cmdAprip.Location = New System.Drawing.Point(651, 141)
    Me.cmdAprip.Name = "cmdAprip"
    Me.cmdAprip.NTSContextMenu = Nothing
    Me.cmdAprip.Size = New System.Drawing.Size(26, 23)
    Me.cmdAprip.TabIndex = 33
    Me.cmdAprip.Text = "("
    '
    'tsFiltri
    '
    Me.tsFiltri.Location = New System.Drawing.Point(5, 83)
    Me.tsFiltri.Name = "tsFiltri"
    Me.tsFiltri.SelectedTabPage = Me.NtsTabPage1
    Me.tsFiltri.Size = New System.Drawing.Size(640, 170)
    Me.tsFiltri.TabIndex = 6
    Me.tsFiltri.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4})
    Me.tsFiltri.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(631, 140)
    Me.NtsTabPage1.Text = "&1° Gruppo"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.lbPz_Val1)
    Me.pnPag1.Controls.Add(Me.lbPz_Op1)
    Me.pnPag1.Controls.Add(Me.lbPz_Tipo1)
    Me.pnPag1.Controls.Add(Me.lbPz_Column1)
    Me.pnPag1.Controls.Add(Me.lb5)
    Me.pnPag1.Controls.Add(Me.lb4)
    Me.pnPag1.Controls.Add(Me.lb3)
    Me.pnPag1.Controls.Add(Me.lb2)
    Me.pnPag1.Controls.Add(Me.lb1)
    Me.pnPag1.Controls.Add(Me.cmdnFiltro5)
    Me.pnPag1.Controls.Add(Me.cmdnFiltro4)
    Me.pnPag1.Controls.Add(Me.cmdnFiltro3)
    Me.pnPag1.Controls.Add(Me.cmdnFiltro2)
    Me.pnPag1.Controls.Add(Me.cmdnFiltro1)
    Me.pnPag1.Controls.Add(Me.edPz_Val5)
    Me.pnPag1.Controls.Add(Me.edPz_Val4)
    Me.pnPag1.Controls.Add(Me.edPz_Val3)
    Me.pnPag1.Controls.Add(Me.edPz_Val2)
    Me.pnPag1.Controls.Add(Me.edPz_Val1)
    Me.pnPag1.Controls.Add(Me.cbPz_Op5)
    Me.pnPag1.Controls.Add(Me.cbPz_Op4)
    Me.pnPag1.Controls.Add(Me.cbPz_Op3)
    Me.pnPag1.Controls.Add(Me.cbPz_Op2)
    Me.pnPag1.Controls.Add(Me.cbPz_Op1)
    Me.pnPag1.Controls.Add(Me.cbPz_Tipo5)
    Me.pnPag1.Controls.Add(Me.cbPz_Tipo4)
    Me.pnPag1.Controls.Add(Me.cbPz_Tipo3)
    Me.pnPag1.Controls.Add(Me.cbPz_Tipo2)
    Me.pnPag1.Controls.Add(Me.cbPz_Tipo1)
    Me.pnPag1.Controls.Add(Me.edPz_Column5)
    Me.pnPag1.Controls.Add(Me.edPz_Column4)
    Me.pnPag1.Controls.Add(Me.edPz_Column3)
    Me.pnPag1.Controls.Add(Me.edPz_Column2)
    Me.pnPag1.Controls.Add(Me.edPz_Column1)
    Me.pnPag1.Controls.Add(Me.lbPz_Descam1)
    Me.pnPag1.Controls.Add(Me.edPz_Descam5)
    Me.pnPag1.Controls.Add(Me.edPz_Descam4)
    Me.pnPag1.Controls.Add(Me.edPz_Descam3)
    Me.pnPag1.Controls.Add(Me.edPz_Descam2)
    Me.pnPag1.Controls.Add(Me.edPz_Descam1)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(631, 140)
    Me.pnPag1.TabIndex = 6
    Me.pnPag1.Text = "NtsPanel1"
    '
    'lbPz_Val1
    '
    Me.lbPz_Val1.AutoSize = True
    Me.lbPz_Val1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Val1.Location = New System.Drawing.Point(495, 9)
    Me.lbPz_Val1.Name = "lbPz_Val1"
    Me.lbPz_Val1.NTSDbField = ""
    Me.lbPz_Val1.Size = New System.Drawing.Size(73, 13)
    Me.lbPz_Val1.TabIndex = 45
    Me.lbPz_Val1.Text = "Valore Campo"
    Me.lbPz_Val1.Tooltip = ""
    Me.lbPz_Val1.UseMnemonic = False
    '
    'lbPz_Op1
    '
    Me.lbPz_Op1.AutoSize = True
    Me.lbPz_Op1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Op1.Location = New System.Drawing.Point(415, 9)
    Me.lbPz_Op1.Name = "lbPz_Op1"
    Me.lbPz_Op1.NTSDbField = ""
    Me.lbPz_Op1.Size = New System.Drawing.Size(57, 13)
    Me.lbPz_Op1.TabIndex = 44
    Me.lbPz_Op1.Text = "Operatore"
    Me.lbPz_Op1.Tooltip = ""
    Me.lbPz_Op1.UseMnemonic = False
    '
    'lbPz_Tipo1
    '
    Me.lbPz_Tipo1.AutoSize = True
    Me.lbPz_Tipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Tipo1.Location = New System.Drawing.Point(329, 9)
    Me.lbPz_Tipo1.Name = "lbPz_Tipo1"
    Me.lbPz_Tipo1.NTSDbField = ""
    Me.lbPz_Tipo1.Size = New System.Drawing.Size(63, 13)
    Me.lbPz_Tipo1.TabIndex = 43
    Me.lbPz_Tipo1.Text = "Tipo Campo"
    Me.lbPz_Tipo1.Tooltip = ""
    Me.lbPz_Tipo1.UseMnemonic = False
    '
    'lbPz_Column1
    '
    Me.lbPz_Column1.AutoSize = True
    Me.lbPz_Column1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Column1.Location = New System.Drawing.Point(185, 9)
    Me.lbPz_Column1.Name = "lbPz_Column1"
    Me.lbPz_Column1.NTSDbField = ""
    Me.lbPz_Column1.Size = New System.Drawing.Size(70, 13)
    Me.lbPz_Column1.TabIndex = 42
    Me.lbPz_Column1.Text = "Nome Campo"
    Me.lbPz_Column1.Tooltip = ""
    Me.lbPz_Column1.UseMnemonic = False
    '
    'lb5
    '
    Me.lb5.AutoSize = True
    Me.lb5.BackColor = System.Drawing.Color.Transparent
    Me.lb5.Location = New System.Drawing.Point(10, 118)
    Me.lb5.Name = "lb5"
    Me.lb5.NTSDbField = ""
    Me.lb5.Size = New System.Drawing.Size(13, 13)
    Me.lb5.TabIndex = 41
    Me.lb5.Text = "5"
    Me.lb5.Tooltip = ""
    Me.lb5.UseMnemonic = False
    '
    'lb4
    '
    Me.lb4.AutoSize = True
    Me.lb4.BackColor = System.Drawing.Color.Transparent
    Me.lb4.Location = New System.Drawing.Point(10, 96)
    Me.lb4.Name = "lb4"
    Me.lb4.NTSDbField = ""
    Me.lb4.Size = New System.Drawing.Size(13, 13)
    Me.lb4.TabIndex = 40
    Me.lb4.Text = "4"
    Me.lb4.Tooltip = ""
    Me.lb4.UseMnemonic = False
    '
    'lb3
    '
    Me.lb3.AutoSize = True
    Me.lb3.BackColor = System.Drawing.Color.Transparent
    Me.lb3.Location = New System.Drawing.Point(10, 74)
    Me.lb3.Name = "lb3"
    Me.lb3.NTSDbField = ""
    Me.lb3.Size = New System.Drawing.Size(13, 13)
    Me.lb3.TabIndex = 39
    Me.lb3.Text = "3"
    Me.lb3.Tooltip = ""
    Me.lb3.UseMnemonic = False
    '
    'lb2
    '
    Me.lb2.AutoSize = True
    Me.lb2.BackColor = System.Drawing.Color.Transparent
    Me.lb2.Location = New System.Drawing.Point(10, 52)
    Me.lb2.Name = "lb2"
    Me.lb2.NTSDbField = ""
    Me.lb2.Size = New System.Drawing.Size(13, 13)
    Me.lb2.TabIndex = 38
    Me.lb2.Text = "2"
    Me.lb2.Tooltip = ""
    Me.lb2.UseMnemonic = False
    '
    'lb1
    '
    Me.lb1.AutoSize = True
    Me.lb1.BackColor = System.Drawing.Color.Transparent
    Me.lb1.Location = New System.Drawing.Point(10, 30)
    Me.lb1.Name = "lb1"
    Me.lb1.NTSDbField = ""
    Me.lb1.Size = New System.Drawing.Size(13, 13)
    Me.lb1.TabIndex = 37
    Me.lb1.Text = "1"
    Me.lb1.Tooltip = ""
    Me.lb1.UseMnemonic = False
    '
    'cmdnFiltro5
    '
    Me.cmdnFiltro5.ImageText = ""
    Me.cmdnFiltro5.Location = New System.Drawing.Point(596, 115)
    Me.cmdnFiltro5.Name = "cmdnFiltro5"
    Me.cmdnFiltro5.NTSContextMenu = Nothing
    Me.cmdnFiltro5.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro5.TabIndex = 36
    Me.cmdnFiltro5.Text = "5"
    '
    'cmdnFiltro4
    '
    Me.cmdnFiltro4.ImageText = ""
    Me.cmdnFiltro4.Location = New System.Drawing.Point(596, 93)
    Me.cmdnFiltro4.Name = "cmdnFiltro4"
    Me.cmdnFiltro4.NTSContextMenu = Nothing
    Me.cmdnFiltro4.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro4.TabIndex = 35
    Me.cmdnFiltro4.Text = "4"
    '
    'cmdnFiltro3
    '
    Me.cmdnFiltro3.ImageText = ""
    Me.cmdnFiltro3.Location = New System.Drawing.Point(596, 71)
    Me.cmdnFiltro3.Name = "cmdnFiltro3"
    Me.cmdnFiltro3.NTSContextMenu = Nothing
    Me.cmdnFiltro3.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro3.TabIndex = 34
    Me.cmdnFiltro3.Text = "3"
    '
    'cmdnFiltro2
    '
    Me.cmdnFiltro2.ImageText = ""
    Me.cmdnFiltro2.Location = New System.Drawing.Point(596, 49)
    Me.cmdnFiltro2.Name = "cmdnFiltro2"
    Me.cmdnFiltro2.NTSContextMenu = Nothing
    Me.cmdnFiltro2.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro2.TabIndex = 33
    Me.cmdnFiltro2.Text = "2"
    '
    'cmdnFiltro1
    '
    Me.cmdnFiltro1.ImageText = ""
    Me.cmdnFiltro1.Location = New System.Drawing.Point(596, 27)
    Me.cmdnFiltro1.Name = "cmdnFiltro1"
    Me.cmdnFiltro1.NTSContextMenu = Nothing
    Me.cmdnFiltro1.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro1.TabIndex = 32
    Me.cmdnFiltro1.Text = "1"
    '
    'edPz_Val5
    '
    Me.edPz_Val5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val5.Location = New System.Drawing.Point(498, 115)
    Me.edPz_Val5.Name = "edPz_Val5"
    Me.edPz_Val5.NTSDbField = ""
    Me.edPz_Val5.NTSForzaVisZoom = False
    Me.edPz_Val5.NTSOldValue = ""
    Me.edPz_Val5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val5.Properties.AutoHeight = False
    Me.edPz_Val5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val5.Properties.MaxLength = 65536
    Me.edPz_Val5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val5.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val5.TabIndex = 31
    '
    'edPz_Val4
    '
    Me.edPz_Val4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val4.Location = New System.Drawing.Point(498, 93)
    Me.edPz_Val4.Name = "edPz_Val4"
    Me.edPz_Val4.NTSDbField = ""
    Me.edPz_Val4.NTSForzaVisZoom = False
    Me.edPz_Val4.NTSOldValue = ""
    Me.edPz_Val4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val4.Properties.AutoHeight = False
    Me.edPz_Val4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val4.Properties.MaxLength = 65536
    Me.edPz_Val4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val4.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val4.TabIndex = 30
    '
    'edPz_Val3
    '
    Me.edPz_Val3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val3.Location = New System.Drawing.Point(498, 71)
    Me.edPz_Val3.Name = "edPz_Val3"
    Me.edPz_Val3.NTSDbField = ""
    Me.edPz_Val3.NTSForzaVisZoom = False
    Me.edPz_Val3.NTSOldValue = ""
    Me.edPz_Val3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val3.Properties.AutoHeight = False
    Me.edPz_Val3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val3.Properties.MaxLength = 65536
    Me.edPz_Val3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val3.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val3.TabIndex = 29
    '
    'edPz_Val2
    '
    Me.edPz_Val2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val2.Location = New System.Drawing.Point(498, 49)
    Me.edPz_Val2.Name = "edPz_Val2"
    Me.edPz_Val2.NTSDbField = ""
    Me.edPz_Val2.NTSForzaVisZoom = False
    Me.edPz_Val2.NTSOldValue = ""
    Me.edPz_Val2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val2.Properties.AutoHeight = False
    Me.edPz_Val2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val2.Properties.MaxLength = 65536
    Me.edPz_Val2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val2.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val2.TabIndex = 28
    '
    'edPz_Val1
    '
    Me.edPz_Val1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val1.Location = New System.Drawing.Point(498, 27)
    Me.edPz_Val1.Name = "edPz_Val1"
    Me.edPz_Val1.NTSDbField = ""
    Me.edPz_Val1.NTSForzaVisZoom = False
    Me.edPz_Val1.NTSOldValue = ""
    Me.edPz_Val1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val1.Properties.AutoHeight = False
    Me.edPz_Val1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val1.Properties.MaxLength = 65536
    Me.edPz_Val1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val1.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val1.TabIndex = 27
    '
    'cbPz_Op5
    '
    Me.cbPz_Op5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op5.DataSource = Nothing
    Me.cbPz_Op5.DisplayMember = ""
    Me.cbPz_Op5.Location = New System.Drawing.Point(418, 115)
    Me.cbPz_Op5.Name = "cbPz_Op5"
    Me.cbPz_Op5.NTSDbField = ""
    Me.cbPz_Op5.Properties.AutoHeight = False
    Me.cbPz_Op5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op5.Properties.DropDownRows = 30
    Me.cbPz_Op5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op5.SelectedValue = ""
    Me.cbPz_Op5.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op5.TabIndex = 26
    Me.cbPz_Op5.ValueMember = ""
    '
    'cbPz_Op4
    '
    Me.cbPz_Op4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op4.DataSource = Nothing
    Me.cbPz_Op4.DisplayMember = ""
    Me.cbPz_Op4.Location = New System.Drawing.Point(418, 93)
    Me.cbPz_Op4.Name = "cbPz_Op4"
    Me.cbPz_Op4.NTSDbField = ""
    Me.cbPz_Op4.Properties.AutoHeight = False
    Me.cbPz_Op4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op4.Properties.DropDownRows = 30
    Me.cbPz_Op4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op4.SelectedValue = ""
    Me.cbPz_Op4.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op4.TabIndex = 25
    Me.cbPz_Op4.ValueMember = ""
    '
    'cbPz_Op3
    '
    Me.cbPz_Op3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op3.DataSource = Nothing
    Me.cbPz_Op3.DisplayMember = ""
    Me.cbPz_Op3.Location = New System.Drawing.Point(418, 71)
    Me.cbPz_Op3.Name = "cbPz_Op3"
    Me.cbPz_Op3.NTSDbField = ""
    Me.cbPz_Op3.Properties.AutoHeight = False
    Me.cbPz_Op3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op3.Properties.DropDownRows = 30
    Me.cbPz_Op3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op3.SelectedValue = ""
    Me.cbPz_Op3.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op3.TabIndex = 24
    Me.cbPz_Op3.ValueMember = ""
    '
    'cbPz_Op2
    '
    Me.cbPz_Op2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op2.DataSource = Nothing
    Me.cbPz_Op2.DisplayMember = ""
    Me.cbPz_Op2.Location = New System.Drawing.Point(418, 49)
    Me.cbPz_Op2.Name = "cbPz_Op2"
    Me.cbPz_Op2.NTSDbField = ""
    Me.cbPz_Op2.Properties.AutoHeight = False
    Me.cbPz_Op2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op2.Properties.DropDownRows = 30
    Me.cbPz_Op2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op2.SelectedValue = ""
    Me.cbPz_Op2.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op2.TabIndex = 23
    Me.cbPz_Op2.ValueMember = ""
    '
    'cbPz_Op1
    '
    Me.cbPz_Op1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op1.DataSource = Nothing
    Me.cbPz_Op1.DisplayMember = ""
    Me.cbPz_Op1.Location = New System.Drawing.Point(418, 27)
    Me.cbPz_Op1.Name = "cbPz_Op1"
    Me.cbPz_Op1.NTSDbField = ""
    Me.cbPz_Op1.Properties.AutoHeight = False
    Me.cbPz_Op1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op1.Properties.DropDownRows = 30
    Me.cbPz_Op1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op1.SelectedValue = ""
    Me.cbPz_Op1.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op1.TabIndex = 22
    Me.cbPz_Op1.ValueMember = ""
    '
    'cbPz_Tipo5
    '
    Me.cbPz_Tipo5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo5.DataSource = Nothing
    Me.cbPz_Tipo5.DisplayMember = ""
    Me.cbPz_Tipo5.Location = New System.Drawing.Point(332, 115)
    Me.cbPz_Tipo5.Name = "cbPz_Tipo5"
    Me.cbPz_Tipo5.NTSDbField = ""
    Me.cbPz_Tipo5.Properties.AutoHeight = False
    Me.cbPz_Tipo5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo5.Properties.DropDownRows = 30
    Me.cbPz_Tipo5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo5.SelectedValue = ""
    Me.cbPz_Tipo5.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo5.TabIndex = 21
    Me.cbPz_Tipo5.ValueMember = ""
    '
    'cbPz_Tipo4
    '
    Me.cbPz_Tipo4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo4.DataSource = Nothing
    Me.cbPz_Tipo4.DisplayMember = ""
    Me.cbPz_Tipo4.Location = New System.Drawing.Point(332, 93)
    Me.cbPz_Tipo4.Name = "cbPz_Tipo4"
    Me.cbPz_Tipo4.NTSDbField = ""
    Me.cbPz_Tipo4.Properties.AutoHeight = False
    Me.cbPz_Tipo4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo4.Properties.DropDownRows = 30
    Me.cbPz_Tipo4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo4.SelectedValue = ""
    Me.cbPz_Tipo4.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo4.TabIndex = 20
    Me.cbPz_Tipo4.ValueMember = ""
    '
    'cbPz_Tipo3
    '
    Me.cbPz_Tipo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo3.DataSource = Nothing
    Me.cbPz_Tipo3.DisplayMember = ""
    Me.cbPz_Tipo3.Location = New System.Drawing.Point(332, 71)
    Me.cbPz_Tipo3.Name = "cbPz_Tipo3"
    Me.cbPz_Tipo3.NTSDbField = ""
    Me.cbPz_Tipo3.Properties.AutoHeight = False
    Me.cbPz_Tipo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo3.Properties.DropDownRows = 30
    Me.cbPz_Tipo3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo3.SelectedValue = ""
    Me.cbPz_Tipo3.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo3.TabIndex = 19
    Me.cbPz_Tipo3.ValueMember = ""
    '
    'cbPz_Tipo2
    '
    Me.cbPz_Tipo2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbPz_Tipo2.DataSource = Nothing
    Me.cbPz_Tipo2.DisplayMember = ""
    Me.cbPz_Tipo2.Location = New System.Drawing.Point(332, 49)
    Me.cbPz_Tipo2.Name = "cbPz_Tipo2"
    Me.cbPz_Tipo2.NTSDbField = ""
    Me.cbPz_Tipo2.Properties.AutoHeight = False
    Me.cbPz_Tipo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo2.Properties.DropDownRows = 30
    Me.cbPz_Tipo2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo2.SelectedValue = ""
    Me.cbPz_Tipo2.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo2.TabIndex = 18
    Me.cbPz_Tipo2.ValueMember = ""
    '
    'cbPz_Tipo1
    '
    Me.cbPz_Tipo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo1.DataSource = Nothing
    Me.cbPz_Tipo1.DisplayMember = ""
    Me.cbPz_Tipo1.Location = New System.Drawing.Point(332, 27)
    Me.cbPz_Tipo1.Name = "cbPz_Tipo1"
    Me.cbPz_Tipo1.NTSDbField = ""
    Me.cbPz_Tipo1.Properties.AutoHeight = False
    Me.cbPz_Tipo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo1.Properties.DropDownRows = 30
    Me.cbPz_Tipo1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo1.SelectedValue = ""
    Me.cbPz_Tipo1.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo1.TabIndex = 17
    Me.cbPz_Tipo1.ValueMember = ""
    '
    'edPz_Column5
    '
    Me.edPz_Column5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column5.Location = New System.Drawing.Point(188, 115)
    Me.edPz_Column5.Name = "edPz_Column5"
    Me.edPz_Column5.NTSDbField = ""
    Me.edPz_Column5.NTSForzaVisZoom = False
    Me.edPz_Column5.NTSOldValue = ""
    Me.edPz_Column5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column5.Properties.AutoHeight = False
    Me.edPz_Column5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column5.Properties.MaxLength = 65536
    Me.edPz_Column5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column5.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column5.TabIndex = 16
    '
    'edPz_Column4
    '
    Me.edPz_Column4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column4.Location = New System.Drawing.Point(188, 93)
    Me.edPz_Column4.Name = "edPz_Column4"
    Me.edPz_Column4.NTSDbField = ""
    Me.edPz_Column4.NTSForzaVisZoom = False
    Me.edPz_Column4.NTSOldValue = ""
    Me.edPz_Column4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column4.Properties.AutoHeight = False
    Me.edPz_Column4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column4.Properties.MaxLength = 65536
    Me.edPz_Column4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column4.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column4.TabIndex = 15
    '
    'edPz_Column3
    '
    Me.edPz_Column3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column3.Location = New System.Drawing.Point(188, 71)
    Me.edPz_Column3.Name = "edPz_Column3"
    Me.edPz_Column3.NTSDbField = ""
    Me.edPz_Column3.NTSForzaVisZoom = False
    Me.edPz_Column3.NTSOldValue = ""
    Me.edPz_Column3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column3.Properties.AutoHeight = False
    Me.edPz_Column3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column3.Properties.MaxLength = 65536
    Me.edPz_Column3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column3.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column3.TabIndex = 14
    '
    'edPz_Column2
    '
    Me.edPz_Column2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPz_Column2.Location = New System.Drawing.Point(188, 49)
    Me.edPz_Column2.Name = "edPz_Column2"
    Me.edPz_Column2.NTSDbField = ""
    Me.edPz_Column2.NTSForzaVisZoom = False
    Me.edPz_Column2.NTSOldValue = ""
    Me.edPz_Column2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column2.Properties.AutoHeight = False
    Me.edPz_Column2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column2.Properties.MaxLength = 65536
    Me.edPz_Column2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column2.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column2.TabIndex = 13
    '
    'edPz_Column1
    '
    Me.edPz_Column1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column1.Location = New System.Drawing.Point(188, 27)
    Me.edPz_Column1.Name = "edPz_Column1"
    Me.edPz_Column1.NTSDbField = ""
    Me.edPz_Column1.NTSForzaVisZoom = False
    Me.edPz_Column1.NTSOldValue = ""
    Me.edPz_Column1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column1.Properties.AutoHeight = False
    Me.edPz_Column1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column1.Properties.MaxLength = 65536
    Me.edPz_Column1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column1.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column1.TabIndex = 12
    '
    'lbPz_Descam1
    '
    Me.lbPz_Descam1.AutoSize = True
    Me.lbPz_Descam1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Descam1.Location = New System.Drawing.Point(20, 9)
    Me.lbPz_Descam1.Name = "lbPz_Descam1"
    Me.lbPz_Descam1.NTSDbField = ""
    Me.lbPz_Descam1.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Descam1.TabIndex = 11
    Me.lbPz_Descam1.Text = "Descrizione Campo"
    Me.lbPz_Descam1.Tooltip = ""
    Me.lbPz_Descam1.UseMnemonic = False
    '
    'edPz_Descam5
    '
    Me.edPz_Descam5.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Descam5.Location = New System.Drawing.Point(23, 115)
    Me.edPz_Descam5.Name = "edPz_Descam5"
    Me.edPz_Descam5.NTSDbField = ""
    Me.edPz_Descam5.NTSForzaVisZoom = False
    Me.edPz_Descam5.NTSOldValue = ""
    Me.edPz_Descam5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam5.Properties.AutoHeight = False
    Me.edPz_Descam5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam5.Properties.MaxLength = 65536
    Me.edPz_Descam5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam5.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam5.TabIndex = 10
    '
    'edPz_Descam4
    '
    Me.edPz_Descam4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam4.Location = New System.Drawing.Point(23, 93)
    Me.edPz_Descam4.Name = "edPz_Descam4"
    Me.edPz_Descam4.NTSDbField = ""
    Me.edPz_Descam4.NTSForzaVisZoom = False
    Me.edPz_Descam4.NTSOldValue = ""
    Me.edPz_Descam4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam4.Properties.AutoHeight = False
    Me.edPz_Descam4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam4.Properties.MaxLength = 65536
    Me.edPz_Descam4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam4.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam4.TabIndex = 9
    '
    'edPz_Descam3
    '
    Me.edPz_Descam3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam3.Location = New System.Drawing.Point(23, 71)
    Me.edPz_Descam3.Name = "edPz_Descam3"
    Me.edPz_Descam3.NTSDbField = ""
    Me.edPz_Descam3.NTSForzaVisZoom = False
    Me.edPz_Descam3.NTSOldValue = ""
    Me.edPz_Descam3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam3.Properties.AutoHeight = False
    Me.edPz_Descam3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam3.Properties.MaxLength = 65536
    Me.edPz_Descam3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam3.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam3.TabIndex = 8
    '
    'edPz_Descam2
    '
    Me.edPz_Descam2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam2.Location = New System.Drawing.Point(23, 49)
    Me.edPz_Descam2.Name = "edPz_Descam2"
    Me.edPz_Descam2.NTSDbField = ""
    Me.edPz_Descam2.NTSForzaVisZoom = False
    Me.edPz_Descam2.NTSOldValue = ""
    Me.edPz_Descam2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam2.Properties.AutoHeight = False
    Me.edPz_Descam2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam2.Properties.MaxLength = 65536
    Me.edPz_Descam2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam2.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam2.TabIndex = 7
    '
    'edPz_Descam1
    '
    Me.edPz_Descam1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam1.Location = New System.Drawing.Point(23, 27)
    Me.edPz_Descam1.Name = "edPz_Descam1"
    Me.edPz_Descam1.NTSDbField = ""
    Me.edPz_Descam1.NTSForzaVisZoom = False
    Me.edPz_Descam1.NTSOldValue = ""
    Me.edPz_Descam1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam1.Properties.AutoHeight = False
    Me.edPz_Descam1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam1.Properties.MaxLength = 65536
    Me.edPz_Descam1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam1.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam1.TabIndex = 6
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(631, 140)
    Me.NtsTabPage2.Text = "&2° Gruppo"
    '
    'pnPag2
    '
    Me.pnPag2.AllowDrop = True
    Me.pnPag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2.Appearance.Options.UseBackColor = True
    Me.pnPag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2.Controls.Add(Me.lbPz_Val6)
    Me.pnPag2.Controls.Add(Me.lbPz_Op6)
    Me.pnPag2.Controls.Add(Me.lbPz_Tipo6)
    Me.pnPag2.Controls.Add(Me.lbPz_Column6)
    Me.pnPag2.Controls.Add(Me.lb10)
    Me.pnPag2.Controls.Add(Me.lb9)
    Me.pnPag2.Controls.Add(Me.lb8)
    Me.pnPag2.Controls.Add(Me.lb7)
    Me.pnPag2.Controls.Add(Me.lb6)
    Me.pnPag2.Controls.Add(Me.cmdnFiltro10)
    Me.pnPag2.Controls.Add(Me.cmdnFiltro9)
    Me.pnPag2.Controls.Add(Me.cmdnFiltro8)
    Me.pnPag2.Controls.Add(Me.cmdnFiltro7)
    Me.pnPag2.Controls.Add(Me.cmdnFiltro6)
    Me.pnPag2.Controls.Add(Me.edPz_Val10)
    Me.pnPag2.Controls.Add(Me.edPz_Val9)
    Me.pnPag2.Controls.Add(Me.edPz_Val8)
    Me.pnPag2.Controls.Add(Me.edPz_Val7)
    Me.pnPag2.Controls.Add(Me.edPz_Val6)
    Me.pnPag2.Controls.Add(Me.cbPz_Op10)
    Me.pnPag2.Controls.Add(Me.cbPz_Op9)
    Me.pnPag2.Controls.Add(Me.cbPz_Op8)
    Me.pnPag2.Controls.Add(Me.cbPz_Op7)
    Me.pnPag2.Controls.Add(Me.cbPz_Op6)
    Me.pnPag2.Controls.Add(Me.cbPz_Tipo10)
    Me.pnPag2.Controls.Add(Me.cbPz_Tipo9)
    Me.pnPag2.Controls.Add(Me.cbPz_Tipo8)
    Me.pnPag2.Controls.Add(Me.cbPz_Tipo7)
    Me.pnPag2.Controls.Add(Me.cbPz_Tipo6)
    Me.pnPag2.Controls.Add(Me.edPz_Column10)
    Me.pnPag2.Controls.Add(Me.edPz_Column9)
    Me.pnPag2.Controls.Add(Me.edPz_Column8)
    Me.pnPag2.Controls.Add(Me.edPz_Column7)
    Me.pnPag2.Controls.Add(Me.edPz_Column6)
    Me.pnPag2.Controls.Add(Me.lbPz_Descam6)
    Me.pnPag2.Controls.Add(Me.edPz_Descam10)
    Me.pnPag2.Controls.Add(Me.edPz_Descam9)
    Me.pnPag2.Controls.Add(Me.edPz_Descam8)
    Me.pnPag2.Controls.Add(Me.edPz_Descam7)
    Me.pnPag2.Controls.Add(Me.edPz_Descam6)
    Me.pnPag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2.Location = New System.Drawing.Point(0, 0)
    Me.pnPag2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag2.Name = "pnPag2"
    Me.pnPag2.NTSActiveTrasparency = True
    Me.pnPag2.Size = New System.Drawing.Size(631, 140)
    Me.pnPag2.TabIndex = 7
    Me.pnPag2.Text = "NtsPanel1"
    '
    'lbPz_Val6
    '
    Me.lbPz_Val6.AutoSize = True
    Me.lbPz_Val6.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Val6.Location = New System.Drawing.Point(495, 9)
    Me.lbPz_Val6.Name = "lbPz_Val6"
    Me.lbPz_Val6.NTSDbField = ""
    Me.lbPz_Val6.Size = New System.Drawing.Size(73, 13)
    Me.lbPz_Val6.TabIndex = 45
    Me.lbPz_Val6.Text = "Valore Campo"
    Me.lbPz_Val6.Tooltip = ""
    Me.lbPz_Val6.UseMnemonic = False
    '
    'lbPz_Op6
    '
    Me.lbPz_Op6.AutoSize = True
    Me.lbPz_Op6.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Op6.Location = New System.Drawing.Point(415, 9)
    Me.lbPz_Op6.Name = "lbPz_Op6"
    Me.lbPz_Op6.NTSDbField = ""
    Me.lbPz_Op6.Size = New System.Drawing.Size(57, 13)
    Me.lbPz_Op6.TabIndex = 44
    Me.lbPz_Op6.Text = "Operatore"
    Me.lbPz_Op6.Tooltip = ""
    Me.lbPz_Op6.UseMnemonic = False
    '
    'lbPz_Tipo6
    '
    Me.lbPz_Tipo6.AutoSize = True
    Me.lbPz_Tipo6.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Tipo6.Location = New System.Drawing.Point(329, 9)
    Me.lbPz_Tipo6.Name = "lbPz_Tipo6"
    Me.lbPz_Tipo6.NTSDbField = ""
    Me.lbPz_Tipo6.Size = New System.Drawing.Size(63, 13)
    Me.lbPz_Tipo6.TabIndex = 43
    Me.lbPz_Tipo6.Text = "Tipo Campo"
    Me.lbPz_Tipo6.Tooltip = ""
    Me.lbPz_Tipo6.UseMnemonic = False
    '
    'lbPz_Column6
    '
    Me.lbPz_Column6.AutoSize = True
    Me.lbPz_Column6.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Column6.Location = New System.Drawing.Point(185, 9)
    Me.lbPz_Column6.Name = "lbPz_Column6"
    Me.lbPz_Column6.NTSDbField = ""
    Me.lbPz_Column6.Size = New System.Drawing.Size(70, 13)
    Me.lbPz_Column6.TabIndex = 42
    Me.lbPz_Column6.Text = "Nome Campo"
    Me.lbPz_Column6.Tooltip = ""
    Me.lbPz_Column6.UseMnemonic = False
    '
    'lb10
    '
    Me.lb10.AutoSize = True
    Me.lb10.BackColor = System.Drawing.Color.Transparent
    Me.lb10.Location = New System.Drawing.Point(4, 118)
    Me.lb10.Name = "lb10"
    Me.lb10.NTSDbField = ""
    Me.lb10.Size = New System.Drawing.Size(19, 13)
    Me.lb10.TabIndex = 41
    Me.lb10.Text = "10"
    Me.lb10.Tooltip = ""
    Me.lb10.UseMnemonic = False
    '
    'lb9
    '
    Me.lb9.AutoSize = True
    Me.lb9.BackColor = System.Drawing.Color.Transparent
    Me.lb9.Location = New System.Drawing.Point(10, 96)
    Me.lb9.Name = "lb9"
    Me.lb9.NTSDbField = ""
    Me.lb9.Size = New System.Drawing.Size(13, 13)
    Me.lb9.TabIndex = 40
    Me.lb9.Text = "9"
    Me.lb9.Tooltip = ""
    Me.lb9.UseMnemonic = False
    '
    'lb8
    '
    Me.lb8.AutoSize = True
    Me.lb8.BackColor = System.Drawing.Color.Transparent
    Me.lb8.Location = New System.Drawing.Point(10, 74)
    Me.lb8.Name = "lb8"
    Me.lb8.NTSDbField = ""
    Me.lb8.Size = New System.Drawing.Size(13, 13)
    Me.lb8.TabIndex = 39
    Me.lb8.Text = "8"
    Me.lb8.Tooltip = ""
    Me.lb8.UseMnemonic = False
    '
    'lb7
    '
    Me.lb7.AutoSize = True
    Me.lb7.BackColor = System.Drawing.Color.Transparent
    Me.lb7.Location = New System.Drawing.Point(10, 52)
    Me.lb7.Name = "lb7"
    Me.lb7.NTSDbField = ""
    Me.lb7.Size = New System.Drawing.Size(13, 13)
    Me.lb7.TabIndex = 38
    Me.lb7.Text = "7"
    Me.lb7.Tooltip = ""
    Me.lb7.UseMnemonic = False
    '
    'lb6
    '
    Me.lb6.AutoSize = True
    Me.lb6.BackColor = System.Drawing.Color.Transparent
    Me.lb6.Location = New System.Drawing.Point(10, 30)
    Me.lb6.Name = "lb6"
    Me.lb6.NTSDbField = ""
    Me.lb6.Size = New System.Drawing.Size(13, 13)
    Me.lb6.TabIndex = 37
    Me.lb6.Text = "6"
    Me.lb6.Tooltip = ""
    Me.lb6.UseMnemonic = False
    '
    'cmdnFiltro10
    '
    Me.cmdnFiltro10.ImageText = ""
    Me.cmdnFiltro10.Location = New System.Drawing.Point(596, 115)
    Me.cmdnFiltro10.Name = "cmdnFiltro10"
    Me.cmdnFiltro10.NTSContextMenu = Nothing
    Me.cmdnFiltro10.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro10.TabIndex = 36
    Me.cmdnFiltro10.Text = "10"
    '
    'cmdnFiltro9
    '
    Me.cmdnFiltro9.ImageText = ""
    Me.cmdnFiltro9.Location = New System.Drawing.Point(596, 93)
    Me.cmdnFiltro9.Name = "cmdnFiltro9"
    Me.cmdnFiltro9.NTSContextMenu = Nothing
    Me.cmdnFiltro9.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro9.TabIndex = 35
    Me.cmdnFiltro9.Text = "9"
    '
    'cmdnFiltro8
    '
    Me.cmdnFiltro8.ImageText = ""
    Me.cmdnFiltro8.Location = New System.Drawing.Point(596, 71)
    Me.cmdnFiltro8.Name = "cmdnFiltro8"
    Me.cmdnFiltro8.NTSContextMenu = Nothing
    Me.cmdnFiltro8.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro8.TabIndex = 34
    Me.cmdnFiltro8.Text = "8"
    '
    'cmdnFiltro7
    '
    Me.cmdnFiltro7.ImageText = ""
    Me.cmdnFiltro7.Location = New System.Drawing.Point(596, 49)
    Me.cmdnFiltro7.Name = "cmdnFiltro7"
    Me.cmdnFiltro7.NTSContextMenu = Nothing
    Me.cmdnFiltro7.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro7.TabIndex = 33
    Me.cmdnFiltro7.Text = "7"
    '
    'cmdnFiltro6
    '
    Me.cmdnFiltro6.ImageText = ""
    Me.cmdnFiltro6.Location = New System.Drawing.Point(596, 27)
    Me.cmdnFiltro6.Name = "cmdnFiltro6"
    Me.cmdnFiltro6.NTSContextMenu = Nothing
    Me.cmdnFiltro6.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro6.TabIndex = 32
    Me.cmdnFiltro6.Text = "6"
    '
    'edPz_Val10
    '
    Me.edPz_Val10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val10.Location = New System.Drawing.Point(498, 115)
    Me.edPz_Val10.Name = "edPz_Val10"
    Me.edPz_Val10.NTSDbField = ""
    Me.edPz_Val10.NTSForzaVisZoom = False
    Me.edPz_Val10.NTSOldValue = ""
    Me.edPz_Val10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val10.Properties.AutoHeight = False
    Me.edPz_Val10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val10.Properties.MaxLength = 65536
    Me.edPz_Val10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val10.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val10.TabIndex = 31
    '
    'edPz_Val9
    '
    Me.edPz_Val9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val9.Location = New System.Drawing.Point(498, 93)
    Me.edPz_Val9.Name = "edPz_Val9"
    Me.edPz_Val9.NTSDbField = ""
    Me.edPz_Val9.NTSForzaVisZoom = False
    Me.edPz_Val9.NTSOldValue = ""
    Me.edPz_Val9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val9.Properties.AutoHeight = False
    Me.edPz_Val9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val9.Properties.MaxLength = 65536
    Me.edPz_Val9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val9.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val9.TabIndex = 30
    '
    'edPz_Val8
    '
    Me.edPz_Val8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val8.Location = New System.Drawing.Point(498, 71)
    Me.edPz_Val8.Name = "edPz_Val8"
    Me.edPz_Val8.NTSDbField = ""
    Me.edPz_Val8.NTSForzaVisZoom = False
    Me.edPz_Val8.NTSOldValue = ""
    Me.edPz_Val8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val8.Properties.AutoHeight = False
    Me.edPz_Val8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val8.Properties.MaxLength = 65536
    Me.edPz_Val8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val8.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val8.TabIndex = 29
    '
    'edPz_Val7
    '
    Me.edPz_Val7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val7.Location = New System.Drawing.Point(498, 49)
    Me.edPz_Val7.Name = "edPz_Val7"
    Me.edPz_Val7.NTSDbField = ""
    Me.edPz_Val7.NTSForzaVisZoom = False
    Me.edPz_Val7.NTSOldValue = ""
    Me.edPz_Val7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val7.Properties.AutoHeight = False
    Me.edPz_Val7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val7.Properties.MaxLength = 65536
    Me.edPz_Val7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val7.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val7.TabIndex = 28
    '
    'edPz_Val6
    '
    Me.edPz_Val6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val6.Location = New System.Drawing.Point(498, 27)
    Me.edPz_Val6.Name = "edPz_Val6"
    Me.edPz_Val6.NTSDbField = ""
    Me.edPz_Val6.NTSForzaVisZoom = False
    Me.edPz_Val6.NTSOldValue = ""
    Me.edPz_Val6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val6.Properties.AutoHeight = False
    Me.edPz_Val6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val6.Properties.MaxLength = 65536
    Me.edPz_Val6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val6.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val6.TabIndex = 27
    '
    'cbPz_Op10
    '
    Me.cbPz_Op10.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op10.DataSource = Nothing
    Me.cbPz_Op10.DisplayMember = ""
    Me.cbPz_Op10.Location = New System.Drawing.Point(418, 115)
    Me.cbPz_Op10.Name = "cbPz_Op10"
    Me.cbPz_Op10.NTSDbField = ""
    Me.cbPz_Op10.Properties.AutoHeight = False
    Me.cbPz_Op10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op10.Properties.DropDownRows = 30
    Me.cbPz_Op10.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op10.SelectedValue = ""
    Me.cbPz_Op10.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op10.TabIndex = 26
    Me.cbPz_Op10.ValueMember = ""
    '
    'cbPz_Op9
    '
    Me.cbPz_Op9.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op9.DataSource = Nothing
    Me.cbPz_Op9.DisplayMember = ""
    Me.cbPz_Op9.Location = New System.Drawing.Point(418, 93)
    Me.cbPz_Op9.Name = "cbPz_Op9"
    Me.cbPz_Op9.NTSDbField = ""
    Me.cbPz_Op9.Properties.AutoHeight = False
    Me.cbPz_Op9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op9.Properties.DropDownRows = 30
    Me.cbPz_Op9.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op9.SelectedValue = ""
    Me.cbPz_Op9.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op9.TabIndex = 25
    Me.cbPz_Op9.ValueMember = ""
    '
    'cbPz_Op8
    '
    Me.cbPz_Op8.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op8.DataSource = Nothing
    Me.cbPz_Op8.DisplayMember = ""
    Me.cbPz_Op8.Location = New System.Drawing.Point(418, 71)
    Me.cbPz_Op8.Name = "cbPz_Op8"
    Me.cbPz_Op8.NTSDbField = ""
    Me.cbPz_Op8.Properties.AutoHeight = False
    Me.cbPz_Op8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op8.Properties.DropDownRows = 30
    Me.cbPz_Op8.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op8.SelectedValue = ""
    Me.cbPz_Op8.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op8.TabIndex = 24
    Me.cbPz_Op8.ValueMember = ""
    '
    'cbPz_Op7
    '
    Me.cbPz_Op7.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op7.DataSource = Nothing
    Me.cbPz_Op7.DisplayMember = ""
    Me.cbPz_Op7.Location = New System.Drawing.Point(418, 49)
    Me.cbPz_Op7.Name = "cbPz_Op7"
    Me.cbPz_Op7.NTSDbField = ""
    Me.cbPz_Op7.Properties.AutoHeight = False
    Me.cbPz_Op7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op7.Properties.DropDownRows = 30
    Me.cbPz_Op7.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op7.SelectedValue = ""
    Me.cbPz_Op7.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op7.TabIndex = 23
    Me.cbPz_Op7.ValueMember = ""
    '
    'cbPz_Op6
    '
    Me.cbPz_Op6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op6.DataSource = Nothing
    Me.cbPz_Op6.DisplayMember = ""
    Me.cbPz_Op6.Location = New System.Drawing.Point(418, 27)
    Me.cbPz_Op6.Name = "cbPz_Op6"
    Me.cbPz_Op6.NTSDbField = ""
    Me.cbPz_Op6.Properties.AutoHeight = False
    Me.cbPz_Op6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op6.Properties.DropDownRows = 30
    Me.cbPz_Op6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op6.SelectedValue = ""
    Me.cbPz_Op6.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op6.TabIndex = 22
    Me.cbPz_Op6.ValueMember = ""
    '
    'cbPz_Tipo10
    '
    Me.cbPz_Tipo10.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo10.DataSource = Nothing
    Me.cbPz_Tipo10.DisplayMember = ""
    Me.cbPz_Tipo10.Location = New System.Drawing.Point(332, 115)
    Me.cbPz_Tipo10.Name = "cbPz_Tipo10"
    Me.cbPz_Tipo10.NTSDbField = ""
    Me.cbPz_Tipo10.Properties.AutoHeight = False
    Me.cbPz_Tipo10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo10.Properties.DropDownRows = 30
    Me.cbPz_Tipo10.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo10.SelectedValue = ""
    Me.cbPz_Tipo10.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo10.TabIndex = 21
    Me.cbPz_Tipo10.ValueMember = ""
    '
    'cbPz_Tipo9
    '
    Me.cbPz_Tipo9.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo9.DataSource = Nothing
    Me.cbPz_Tipo9.DisplayMember = ""
    Me.cbPz_Tipo9.Location = New System.Drawing.Point(332, 93)
    Me.cbPz_Tipo9.Name = "cbPz_Tipo9"
    Me.cbPz_Tipo9.NTSDbField = ""
    Me.cbPz_Tipo9.Properties.AutoHeight = False
    Me.cbPz_Tipo9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo9.Properties.DropDownRows = 30
    Me.cbPz_Tipo9.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo9.SelectedValue = ""
    Me.cbPz_Tipo9.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo9.TabIndex = 20
    Me.cbPz_Tipo9.ValueMember = ""
    '
    'cbPz_Tipo8
    '
    Me.cbPz_Tipo8.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo8.DataSource = Nothing
    Me.cbPz_Tipo8.DisplayMember = ""
    Me.cbPz_Tipo8.Location = New System.Drawing.Point(332, 71)
    Me.cbPz_Tipo8.Name = "cbPz_Tipo8"
    Me.cbPz_Tipo8.NTSDbField = ""
    Me.cbPz_Tipo8.Properties.AutoHeight = False
    Me.cbPz_Tipo8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo8.Properties.DropDownRows = 30
    Me.cbPz_Tipo8.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo8.SelectedValue = ""
    Me.cbPz_Tipo8.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo8.TabIndex = 19
    Me.cbPz_Tipo8.ValueMember = ""
    '
    'cbPz_Tipo7
    '
    Me.cbPz_Tipo7.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo7.DataSource = Nothing
    Me.cbPz_Tipo7.DisplayMember = ""
    Me.cbPz_Tipo7.Location = New System.Drawing.Point(332, 49)
    Me.cbPz_Tipo7.Name = "cbPz_Tipo7"
    Me.cbPz_Tipo7.NTSDbField = ""
    Me.cbPz_Tipo7.Properties.AutoHeight = False
    Me.cbPz_Tipo7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo7.Properties.DropDownRows = 30
    Me.cbPz_Tipo7.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo7.SelectedValue = ""
    Me.cbPz_Tipo7.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo7.TabIndex = 18
    Me.cbPz_Tipo7.ValueMember = ""
    '
    'cbPz_Tipo6
    '
    Me.cbPz_Tipo6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo6.DataSource = Nothing
    Me.cbPz_Tipo6.DisplayMember = ""
    Me.cbPz_Tipo6.Location = New System.Drawing.Point(332, 27)
    Me.cbPz_Tipo6.Name = "cbPz_Tipo6"
    Me.cbPz_Tipo6.NTSDbField = ""
    Me.cbPz_Tipo6.Properties.AutoHeight = False
    Me.cbPz_Tipo6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo6.Properties.DropDownRows = 30
    Me.cbPz_Tipo6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo6.SelectedValue = ""
    Me.cbPz_Tipo6.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo6.TabIndex = 17
    Me.cbPz_Tipo6.ValueMember = ""
    '
    'edPz_Column10
    '
    Me.edPz_Column10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column10.Location = New System.Drawing.Point(188, 115)
    Me.edPz_Column10.Name = "edPz_Column10"
    Me.edPz_Column10.NTSDbField = ""
    Me.edPz_Column10.NTSForzaVisZoom = False
    Me.edPz_Column10.NTSOldValue = ""
    Me.edPz_Column10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column10.Properties.AutoHeight = False
    Me.edPz_Column10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column10.Properties.MaxLength = 65536
    Me.edPz_Column10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column10.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column10.TabIndex = 16
    '
    'edPz_Column9
    '
    Me.edPz_Column9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column9.Location = New System.Drawing.Point(188, 93)
    Me.edPz_Column9.Name = "edPz_Column9"
    Me.edPz_Column9.NTSDbField = ""
    Me.edPz_Column9.NTSForzaVisZoom = False
    Me.edPz_Column9.NTSOldValue = ""
    Me.edPz_Column9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column9.Properties.AutoHeight = False
    Me.edPz_Column9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column9.Properties.MaxLength = 65536
    Me.edPz_Column9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column9.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column9.TabIndex = 15
    '
    'edPz_Column8
    '
    Me.edPz_Column8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column8.Location = New System.Drawing.Point(188, 71)
    Me.edPz_Column8.Name = "edPz_Column8"
    Me.edPz_Column8.NTSDbField = ""
    Me.edPz_Column8.NTSForzaVisZoom = False
    Me.edPz_Column8.NTSOldValue = ""
    Me.edPz_Column8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column8.Properties.AutoHeight = False
    Me.edPz_Column8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column8.Properties.MaxLength = 65536
    Me.edPz_Column8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column8.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column8.TabIndex = 14
    '
    'edPz_Column7
    '
    Me.edPz_Column7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column7.Location = New System.Drawing.Point(188, 49)
    Me.edPz_Column7.Name = "edPz_Column7"
    Me.edPz_Column7.NTSDbField = ""
    Me.edPz_Column7.NTSForzaVisZoom = False
    Me.edPz_Column7.NTSOldValue = ""
    Me.edPz_Column7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column7.Properties.AutoHeight = False
    Me.edPz_Column7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column7.Properties.MaxLength = 65536
    Me.edPz_Column7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column7.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column7.TabIndex = 13
    '
    'edPz_Column6
    '
    Me.edPz_Column6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column6.Location = New System.Drawing.Point(188, 27)
    Me.edPz_Column6.Name = "edPz_Column6"
    Me.edPz_Column6.NTSDbField = ""
    Me.edPz_Column6.NTSForzaVisZoom = False
    Me.edPz_Column6.NTSOldValue = ""
    Me.edPz_Column6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column6.Properties.AutoHeight = False
    Me.edPz_Column6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column6.Properties.MaxLength = 65536
    Me.edPz_Column6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column6.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column6.TabIndex = 12
    '
    'lbPz_Descam6
    '
    Me.lbPz_Descam6.AutoSize = True
    Me.lbPz_Descam6.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Descam6.Location = New System.Drawing.Point(20, 9)
    Me.lbPz_Descam6.Name = "lbPz_Descam6"
    Me.lbPz_Descam6.NTSDbField = ""
    Me.lbPz_Descam6.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Descam6.TabIndex = 11
    Me.lbPz_Descam6.Text = "Descrizione Campo"
    Me.lbPz_Descam6.Tooltip = ""
    Me.lbPz_Descam6.UseMnemonic = False
    '
    'edPz_Descam10
    '
    Me.edPz_Descam10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam10.Location = New System.Drawing.Point(23, 115)
    Me.edPz_Descam10.Name = "edPz_Descam10"
    Me.edPz_Descam10.NTSDbField = ""
    Me.edPz_Descam10.NTSForzaVisZoom = False
    Me.edPz_Descam10.NTSOldValue = ""
    Me.edPz_Descam10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam10.Properties.AutoHeight = False
    Me.edPz_Descam10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam10.Properties.MaxLength = 65536
    Me.edPz_Descam10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam10.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam10.TabIndex = 10
    '
    'edPz_Descam9
    '
    Me.edPz_Descam9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam9.Location = New System.Drawing.Point(23, 93)
    Me.edPz_Descam9.Name = "edPz_Descam9"
    Me.edPz_Descam9.NTSDbField = ""
    Me.edPz_Descam9.NTSForzaVisZoom = False
    Me.edPz_Descam9.NTSOldValue = ""
    Me.edPz_Descam9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam9.Properties.AutoHeight = False
    Me.edPz_Descam9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam9.Properties.MaxLength = 65536
    Me.edPz_Descam9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam9.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam9.TabIndex = 9
    '
    'edPz_Descam8
    '
    Me.edPz_Descam8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam8.Location = New System.Drawing.Point(23, 71)
    Me.edPz_Descam8.Name = "edPz_Descam8"
    Me.edPz_Descam8.NTSDbField = ""
    Me.edPz_Descam8.NTSForzaVisZoom = False
    Me.edPz_Descam8.NTSOldValue = ""
    Me.edPz_Descam8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam8.Properties.AutoHeight = False
    Me.edPz_Descam8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam8.Properties.MaxLength = 65536
    Me.edPz_Descam8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam8.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam8.TabIndex = 8
    '
    'edPz_Descam7
    '
    Me.edPz_Descam7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam7.Location = New System.Drawing.Point(23, 49)
    Me.edPz_Descam7.Name = "edPz_Descam7"
    Me.edPz_Descam7.NTSDbField = ""
    Me.edPz_Descam7.NTSForzaVisZoom = False
    Me.edPz_Descam7.NTSOldValue = ""
    Me.edPz_Descam7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam7.Properties.AutoHeight = False
    Me.edPz_Descam7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam7.Properties.MaxLength = 65536
    Me.edPz_Descam7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam7.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam7.TabIndex = 7
    '
    'edPz_Descam6
    '
    Me.edPz_Descam6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam6.Location = New System.Drawing.Point(23, 27)
    Me.edPz_Descam6.Name = "edPz_Descam6"
    Me.edPz_Descam6.NTSDbField = ""
    Me.edPz_Descam6.NTSForzaVisZoom = False
    Me.edPz_Descam6.NTSOldValue = ""
    Me.edPz_Descam6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam6.Properties.AutoHeight = False
    Me.edPz_Descam6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam6.Properties.MaxLength = 65536
    Me.edPz_Descam6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam6.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam6.TabIndex = 6
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPage3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(631, 140)
    Me.NtsTabPage3.Text = "&3° Gruppo"
    '
    'pnPage3
    '
    Me.pnPage3.AllowDrop = True
    Me.pnPage3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage3.Appearance.Options.UseBackColor = True
    Me.pnPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage3.Controls.Add(Me.lbPz_Val11)
    Me.pnPage3.Controls.Add(Me.lbPz_Op11)
    Me.pnPage3.Controls.Add(Me.lbPz_Tipo11)
    Me.pnPage3.Controls.Add(Me.lbPz_Column11)
    Me.pnPage3.Controls.Add(Me.lb15)
    Me.pnPage3.Controls.Add(Me.lb14)
    Me.pnPage3.Controls.Add(Me.lb13)
    Me.pnPage3.Controls.Add(Me.lb12)
    Me.pnPage3.Controls.Add(Me.lb11)
    Me.pnPage3.Controls.Add(Me.cmdnFiltro15)
    Me.pnPage3.Controls.Add(Me.cmdnFiltro14)
    Me.pnPage3.Controls.Add(Me.cmdnFiltro13)
    Me.pnPage3.Controls.Add(Me.cmdnFiltro12)
    Me.pnPage3.Controls.Add(Me.cmdnFiltro11)
    Me.pnPage3.Controls.Add(Me.edPz_Val15)
    Me.pnPage3.Controls.Add(Me.edPz_Val14)
    Me.pnPage3.Controls.Add(Me.edPz_Val13)
    Me.pnPage3.Controls.Add(Me.edPz_Val12)
    Me.pnPage3.Controls.Add(Me.edPz_Val11)
    Me.pnPage3.Controls.Add(Me.cbPz_Op15)
    Me.pnPage3.Controls.Add(Me.cbPz_Op14)
    Me.pnPage3.Controls.Add(Me.cbPz_Op13)
    Me.pnPage3.Controls.Add(Me.cbPz_Op12)
    Me.pnPage3.Controls.Add(Me.cbPz_Op11)
    Me.pnPage3.Controls.Add(Me.cbPz_Tipo15)
    Me.pnPage3.Controls.Add(Me.cbPz_Tipo14)
    Me.pnPage3.Controls.Add(Me.cbPz_Tipo13)
    Me.pnPage3.Controls.Add(Me.cbPz_Tipo12)
    Me.pnPage3.Controls.Add(Me.cbPz_Tipo11)
    Me.pnPage3.Controls.Add(Me.edPz_Column15)
    Me.pnPage3.Controls.Add(Me.edPz_Column14)
    Me.pnPage3.Controls.Add(Me.edPz_Column13)
    Me.pnPage3.Controls.Add(Me.edPz_Column12)
    Me.pnPage3.Controls.Add(Me.edPz_Column11)
    Me.pnPage3.Controls.Add(Me.lbPz_Descam11)
    Me.pnPage3.Controls.Add(Me.edPz_Descam15)
    Me.pnPage3.Controls.Add(Me.edPz_Descam14)
    Me.pnPage3.Controls.Add(Me.edPz_Descam13)
    Me.pnPage3.Controls.Add(Me.edPz_Descam12)
    Me.pnPage3.Controls.Add(Me.edPz_Descam11)
    Me.pnPage3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage3.Location = New System.Drawing.Point(0, 0)
    Me.pnPage3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPage3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPage3.Name = "pnPage3"
    Me.pnPage3.NTSActiveTrasparency = True
    Me.pnPage3.Size = New System.Drawing.Size(631, 140)
    Me.pnPage3.TabIndex = 8
    Me.pnPage3.Text = "NtsPanel1"
    '
    'lbPz_Val11
    '
    Me.lbPz_Val11.AutoSize = True
    Me.lbPz_Val11.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Val11.Location = New System.Drawing.Point(495, 9)
    Me.lbPz_Val11.Name = "lbPz_Val11"
    Me.lbPz_Val11.NTSDbField = ""
    Me.lbPz_Val11.Size = New System.Drawing.Size(73, 13)
    Me.lbPz_Val11.TabIndex = 45
    Me.lbPz_Val11.Text = "Valore Campo"
    Me.lbPz_Val11.Tooltip = ""
    Me.lbPz_Val11.UseMnemonic = False
    '
    'lbPz_Op11
    '
    Me.lbPz_Op11.AutoSize = True
    Me.lbPz_Op11.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Op11.Location = New System.Drawing.Point(415, 9)
    Me.lbPz_Op11.Name = "lbPz_Op11"
    Me.lbPz_Op11.NTSDbField = ""
    Me.lbPz_Op11.Size = New System.Drawing.Size(57, 13)
    Me.lbPz_Op11.TabIndex = 44
    Me.lbPz_Op11.Text = "Operatore"
    Me.lbPz_Op11.Tooltip = ""
    Me.lbPz_Op11.UseMnemonic = False
    '
    'lbPz_Tipo11
    '
    Me.lbPz_Tipo11.AutoSize = True
    Me.lbPz_Tipo11.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Tipo11.Location = New System.Drawing.Point(329, 9)
    Me.lbPz_Tipo11.Name = "lbPz_Tipo11"
    Me.lbPz_Tipo11.NTSDbField = ""
    Me.lbPz_Tipo11.Size = New System.Drawing.Size(63, 13)
    Me.lbPz_Tipo11.TabIndex = 43
    Me.lbPz_Tipo11.Text = "Tipo Campo"
    Me.lbPz_Tipo11.Tooltip = ""
    Me.lbPz_Tipo11.UseMnemonic = False
    '
    'lbPz_Column11
    '
    Me.lbPz_Column11.AutoSize = True
    Me.lbPz_Column11.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Column11.Location = New System.Drawing.Point(185, 9)
    Me.lbPz_Column11.Name = "lbPz_Column11"
    Me.lbPz_Column11.NTSDbField = ""
    Me.lbPz_Column11.Size = New System.Drawing.Size(70, 13)
    Me.lbPz_Column11.TabIndex = 42
    Me.lbPz_Column11.Text = "Nome Campo"
    Me.lbPz_Column11.Tooltip = ""
    Me.lbPz_Column11.UseMnemonic = False
    '
    'lb15
    '
    Me.lb15.AutoSize = True
    Me.lb15.BackColor = System.Drawing.Color.Transparent
    Me.lb15.Location = New System.Drawing.Point(4, 118)
    Me.lb15.Name = "lb15"
    Me.lb15.NTSDbField = ""
    Me.lb15.Size = New System.Drawing.Size(19, 13)
    Me.lb15.TabIndex = 41
    Me.lb15.Text = "15"
    Me.lb15.Tooltip = ""
    Me.lb15.UseMnemonic = False
    '
    'lb14
    '
    Me.lb14.AutoSize = True
    Me.lb14.BackColor = System.Drawing.Color.Transparent
    Me.lb14.Location = New System.Drawing.Point(4, 96)
    Me.lb14.Name = "lb14"
    Me.lb14.NTSDbField = ""
    Me.lb14.Size = New System.Drawing.Size(19, 13)
    Me.lb14.TabIndex = 40
    Me.lb14.Text = "14"
    Me.lb14.Tooltip = ""
    Me.lb14.UseMnemonic = False
    '
    'lb13
    '
    Me.lb13.AutoSize = True
    Me.lb13.BackColor = System.Drawing.Color.Transparent
    Me.lb13.Location = New System.Drawing.Point(4, 74)
    Me.lb13.Name = "lb13"
    Me.lb13.NTSDbField = ""
    Me.lb13.Size = New System.Drawing.Size(19, 13)
    Me.lb13.TabIndex = 39
    Me.lb13.Text = "13"
    Me.lb13.Tooltip = ""
    Me.lb13.UseMnemonic = False
    '
    'lb12
    '
    Me.lb12.AutoSize = True
    Me.lb12.BackColor = System.Drawing.Color.Transparent
    Me.lb12.Location = New System.Drawing.Point(4, 52)
    Me.lb12.Name = "lb12"
    Me.lb12.NTSDbField = ""
    Me.lb12.Size = New System.Drawing.Size(19, 13)
    Me.lb12.TabIndex = 38
    Me.lb12.Text = "12"
    Me.lb12.Tooltip = ""
    Me.lb12.UseMnemonic = False
    '
    'lb11
    '
    Me.lb11.AutoSize = True
    Me.lb11.BackColor = System.Drawing.Color.Transparent
    Me.lb11.Location = New System.Drawing.Point(4, 30)
    Me.lb11.Name = "lb11"
    Me.lb11.NTSDbField = ""
    Me.lb11.Size = New System.Drawing.Size(19, 13)
    Me.lb11.TabIndex = 37
    Me.lb11.Text = "11"
    Me.lb11.Tooltip = ""
    Me.lb11.UseMnemonic = False
    '
    'cmdnFiltro15
    '
    Me.cmdnFiltro15.ImageText = ""
    Me.cmdnFiltro15.Location = New System.Drawing.Point(596, 115)
    Me.cmdnFiltro15.Name = "cmdnFiltro15"
    Me.cmdnFiltro15.NTSContextMenu = Nothing
    Me.cmdnFiltro15.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro15.TabIndex = 36
    Me.cmdnFiltro15.Text = "15"
    '
    'cmdnFiltro14
    '
    Me.cmdnFiltro14.ImageText = ""
    Me.cmdnFiltro14.Location = New System.Drawing.Point(596, 93)
    Me.cmdnFiltro14.Name = "cmdnFiltro14"
    Me.cmdnFiltro14.NTSContextMenu = Nothing
    Me.cmdnFiltro14.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro14.TabIndex = 35
    Me.cmdnFiltro14.Text = "14"
    '
    'cmdnFiltro13
    '
    Me.cmdnFiltro13.ImageText = ""
    Me.cmdnFiltro13.Location = New System.Drawing.Point(596, 71)
    Me.cmdnFiltro13.Name = "cmdnFiltro13"
    Me.cmdnFiltro13.NTSContextMenu = Nothing
    Me.cmdnFiltro13.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro13.TabIndex = 34
    Me.cmdnFiltro13.Text = "13"
    '
    'cmdnFiltro12
    '
    Me.cmdnFiltro12.ImageText = ""
    Me.cmdnFiltro12.Location = New System.Drawing.Point(596, 49)
    Me.cmdnFiltro12.Name = "cmdnFiltro12"
    Me.cmdnFiltro12.NTSContextMenu = Nothing
    Me.cmdnFiltro12.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro12.TabIndex = 33
    Me.cmdnFiltro12.Text = "12"
    '
    'cmdnFiltro11
    '
    Me.cmdnFiltro11.ImageText = ""
    Me.cmdnFiltro11.Location = New System.Drawing.Point(596, 27)
    Me.cmdnFiltro11.Name = "cmdnFiltro11"
    Me.cmdnFiltro11.NTSContextMenu = Nothing
    Me.cmdnFiltro11.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltro11.TabIndex = 32
    Me.cmdnFiltro11.Text = "11"
    '
    'edPz_Val15
    '
    Me.edPz_Val15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val15.Location = New System.Drawing.Point(498, 115)
    Me.edPz_Val15.Name = "edPz_Val15"
    Me.edPz_Val15.NTSDbField = ""
    Me.edPz_Val15.NTSForzaVisZoom = False
    Me.edPz_Val15.NTSOldValue = ""
    Me.edPz_Val15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val15.Properties.AutoHeight = False
    Me.edPz_Val15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val15.Properties.MaxLength = 65536
    Me.edPz_Val15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val15.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val15.TabIndex = 31
    '
    'edPz_Val14
    '
    Me.edPz_Val14.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val14.Location = New System.Drawing.Point(498, 93)
    Me.edPz_Val14.Name = "edPz_Val14"
    Me.edPz_Val14.NTSDbField = ""
    Me.edPz_Val14.NTSForzaVisZoom = False
    Me.edPz_Val14.NTSOldValue = ""
    Me.edPz_Val14.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val14.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val14.Properties.AutoHeight = False
    Me.edPz_Val14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val14.Properties.MaxLength = 65536
    Me.edPz_Val14.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val14.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val14.TabIndex = 30
    '
    'edPz_Val13
    '
    Me.edPz_Val13.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Val13.Location = New System.Drawing.Point(498, 71)
    Me.edPz_Val13.Name = "edPz_Val13"
    Me.edPz_Val13.NTSDbField = ""
    Me.edPz_Val13.NTSForzaVisZoom = False
    Me.edPz_Val13.NTSOldValue = ""
    Me.edPz_Val13.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val13.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val13.Properties.AutoHeight = False
    Me.edPz_Val13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val13.Properties.MaxLength = 65536
    Me.edPz_Val13.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val13.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val13.TabIndex = 29
    '
    'edPz_Val12
    '
    Me.edPz_Val12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val12.Location = New System.Drawing.Point(498, 49)
    Me.edPz_Val12.Name = "edPz_Val12"
    Me.edPz_Val12.NTSDbField = ""
    Me.edPz_Val12.NTSForzaVisZoom = False
    Me.edPz_Val12.NTSOldValue = ""
    Me.edPz_Val12.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val12.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val12.Properties.AutoHeight = False
    Me.edPz_Val12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val12.Properties.MaxLength = 65536
    Me.edPz_Val12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val12.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val12.TabIndex = 28
    '
    'edPz_Val11
    '
    Me.edPz_Val11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Val11.Location = New System.Drawing.Point(498, 27)
    Me.edPz_Val11.Name = "edPz_Val11"
    Me.edPz_Val11.NTSDbField = ""
    Me.edPz_Val11.NTSForzaVisZoom = False
    Me.edPz_Val11.NTSOldValue = ""
    Me.edPz_Val11.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Val11.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Val11.Properties.AutoHeight = False
    Me.edPz_Val11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Val11.Properties.MaxLength = 65536
    Me.edPz_Val11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Val11.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Val11.TabIndex = 27
    '
    'cbPz_Op15
    '
    Me.cbPz_Op15.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op15.DataSource = Nothing
    Me.cbPz_Op15.DisplayMember = ""
    Me.cbPz_Op15.Location = New System.Drawing.Point(418, 115)
    Me.cbPz_Op15.Name = "cbPz_Op15"
    Me.cbPz_Op15.NTSDbField = ""
    Me.cbPz_Op15.Properties.AutoHeight = False
    Me.cbPz_Op15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op15.Properties.DropDownRows = 30
    Me.cbPz_Op15.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op15.SelectedValue = ""
    Me.cbPz_Op15.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op15.TabIndex = 26
    Me.cbPz_Op15.ValueMember = ""
    '
    'cbPz_Op14
    '
    Me.cbPz_Op14.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op14.DataSource = Nothing
    Me.cbPz_Op14.DisplayMember = ""
    Me.cbPz_Op14.Location = New System.Drawing.Point(418, 93)
    Me.cbPz_Op14.Name = "cbPz_Op14"
    Me.cbPz_Op14.NTSDbField = ""
    Me.cbPz_Op14.Properties.AutoHeight = False
    Me.cbPz_Op14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op14.Properties.DropDownRows = 30
    Me.cbPz_Op14.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op14.SelectedValue = ""
    Me.cbPz_Op14.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op14.TabIndex = 25
    Me.cbPz_Op14.ValueMember = ""
    '
    'cbPz_Op13
    '
    Me.cbPz_Op13.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op13.DataSource = Nothing
    Me.cbPz_Op13.DisplayMember = ""
    Me.cbPz_Op13.Location = New System.Drawing.Point(418, 71)
    Me.cbPz_Op13.Name = "cbPz_Op13"
    Me.cbPz_Op13.NTSDbField = ""
    Me.cbPz_Op13.Properties.AutoHeight = False
    Me.cbPz_Op13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op13.Properties.DropDownRows = 30
    Me.cbPz_Op13.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op13.SelectedValue = ""
    Me.cbPz_Op13.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op13.TabIndex = 24
    Me.cbPz_Op13.ValueMember = ""
    '
    'cbPz_Op12
    '
    Me.cbPz_Op12.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op12.DataSource = Nothing
    Me.cbPz_Op12.DisplayMember = ""
    Me.cbPz_Op12.Location = New System.Drawing.Point(418, 49)
    Me.cbPz_Op12.Name = "cbPz_Op12"
    Me.cbPz_Op12.NTSDbField = ""
    Me.cbPz_Op12.Properties.AutoHeight = False
    Me.cbPz_Op12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op12.Properties.DropDownRows = 30
    Me.cbPz_Op12.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op12.SelectedValue = ""
    Me.cbPz_Op12.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op12.TabIndex = 23
    Me.cbPz_Op12.ValueMember = ""
    '
    'cbPz_Op11
    '
    Me.cbPz_Op11.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Op11.DataSource = Nothing
    Me.cbPz_Op11.DisplayMember = ""
    Me.cbPz_Op11.Location = New System.Drawing.Point(418, 27)
    Me.cbPz_Op11.Name = "cbPz_Op11"
    Me.cbPz_Op11.NTSDbField = ""
    Me.cbPz_Op11.Properties.AutoHeight = False
    Me.cbPz_Op11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Op11.Properties.DropDownRows = 30
    Me.cbPz_Op11.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Op11.SelectedValue = ""
    Me.cbPz_Op11.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Op11.TabIndex = 22
    Me.cbPz_Op11.ValueMember = ""
    '
    'cbPz_Tipo15
    '
    Me.cbPz_Tipo15.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo15.DataSource = Nothing
    Me.cbPz_Tipo15.DisplayMember = ""
    Me.cbPz_Tipo15.Location = New System.Drawing.Point(332, 115)
    Me.cbPz_Tipo15.Name = "cbPz_Tipo15"
    Me.cbPz_Tipo15.NTSDbField = ""
    Me.cbPz_Tipo15.Properties.AutoHeight = False
    Me.cbPz_Tipo15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo15.Properties.DropDownRows = 30
    Me.cbPz_Tipo15.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo15.SelectedValue = ""
    Me.cbPz_Tipo15.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo15.TabIndex = 21
    Me.cbPz_Tipo15.ValueMember = ""
    '
    'cbPz_Tipo14
    '
    Me.cbPz_Tipo14.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo14.DataSource = Nothing
    Me.cbPz_Tipo14.DisplayMember = ""
    Me.cbPz_Tipo14.Location = New System.Drawing.Point(332, 93)
    Me.cbPz_Tipo14.Name = "cbPz_Tipo14"
    Me.cbPz_Tipo14.NTSDbField = ""
    Me.cbPz_Tipo14.Properties.AutoHeight = False
    Me.cbPz_Tipo14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo14.Properties.DropDownRows = 30
    Me.cbPz_Tipo14.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo14.SelectedValue = ""
    Me.cbPz_Tipo14.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo14.TabIndex = 20
    Me.cbPz_Tipo14.ValueMember = ""
    '
    'cbPz_Tipo13
    '
    Me.cbPz_Tipo13.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo13.DataSource = Nothing
    Me.cbPz_Tipo13.DisplayMember = ""
    Me.cbPz_Tipo13.Location = New System.Drawing.Point(332, 71)
    Me.cbPz_Tipo13.Name = "cbPz_Tipo13"
    Me.cbPz_Tipo13.NTSDbField = ""
    Me.cbPz_Tipo13.Properties.AutoHeight = False
    Me.cbPz_Tipo13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo13.Properties.DropDownRows = 30
    Me.cbPz_Tipo13.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo13.SelectedValue = ""
    Me.cbPz_Tipo13.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo13.TabIndex = 19
    Me.cbPz_Tipo13.ValueMember = ""
    '
    'cbPz_Tipo12
    '
    Me.cbPz_Tipo12.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo12.DataSource = Nothing
    Me.cbPz_Tipo12.DisplayMember = ""
    Me.cbPz_Tipo12.Location = New System.Drawing.Point(332, 49)
    Me.cbPz_Tipo12.Name = "cbPz_Tipo12"
    Me.cbPz_Tipo12.NTSDbField = ""
    Me.cbPz_Tipo12.Properties.AutoHeight = False
    Me.cbPz_Tipo12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo12.Properties.DropDownRows = 30
    Me.cbPz_Tipo12.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo12.SelectedValue = ""
    Me.cbPz_Tipo12.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo12.TabIndex = 18
    Me.cbPz_Tipo12.ValueMember = ""
    '
    'cbPz_Tipo11
    '
    Me.cbPz_Tipo11.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipo11.DataSource = Nothing
    Me.cbPz_Tipo11.DisplayMember = ""
    Me.cbPz_Tipo11.Location = New System.Drawing.Point(332, 27)
    Me.cbPz_Tipo11.Name = "cbPz_Tipo11"
    Me.cbPz_Tipo11.NTSDbField = ""
    Me.cbPz_Tipo11.Properties.AutoHeight = False
    Me.cbPz_Tipo11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipo11.Properties.DropDownRows = 30
    Me.cbPz_Tipo11.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipo11.SelectedValue = ""
    Me.cbPz_Tipo11.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipo11.TabIndex = 17
    Me.cbPz_Tipo11.ValueMember = ""
    '
    'edPz_Column15
    '
    Me.edPz_Column15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column15.Location = New System.Drawing.Point(188, 115)
    Me.edPz_Column15.Name = "edPz_Column15"
    Me.edPz_Column15.NTSDbField = ""
    Me.edPz_Column15.NTSForzaVisZoom = False
    Me.edPz_Column15.NTSOldValue = ""
    Me.edPz_Column15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column15.Properties.AutoHeight = False
    Me.edPz_Column15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column15.Properties.MaxLength = 65536
    Me.edPz_Column15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column15.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column15.TabIndex = 16
    '
    'edPz_Column14
    '
    Me.edPz_Column14.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column14.Location = New System.Drawing.Point(188, 93)
    Me.edPz_Column14.Name = "edPz_Column14"
    Me.edPz_Column14.NTSDbField = ""
    Me.edPz_Column14.NTSForzaVisZoom = False
    Me.edPz_Column14.NTSOldValue = ""
    Me.edPz_Column14.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column14.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column14.Properties.AutoHeight = False
    Me.edPz_Column14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column14.Properties.MaxLength = 65536
    Me.edPz_Column14.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column14.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column14.TabIndex = 15
    '
    'edPz_Column13
    '
    Me.edPz_Column13.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPz_Column13.Location = New System.Drawing.Point(188, 71)
    Me.edPz_Column13.Name = "edPz_Column13"
    Me.edPz_Column13.NTSDbField = ""
    Me.edPz_Column13.NTSForzaVisZoom = False
    Me.edPz_Column13.NTSOldValue = ""
    Me.edPz_Column13.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column13.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column13.Properties.AutoHeight = False
    Me.edPz_Column13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column13.Properties.MaxLength = 65536
    Me.edPz_Column13.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column13.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column13.TabIndex = 14
    '
    'edPz_Column12
    '
    Me.edPz_Column12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column12.Location = New System.Drawing.Point(188, 49)
    Me.edPz_Column12.Name = "edPz_Column12"
    Me.edPz_Column12.NTSDbField = ""
    Me.edPz_Column12.NTSForzaVisZoom = False
    Me.edPz_Column12.NTSOldValue = ""
    Me.edPz_Column12.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column12.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column12.Properties.AutoHeight = False
    Me.edPz_Column12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column12.Properties.MaxLength = 65536
    Me.edPz_Column12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column12.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column12.TabIndex = 13
    '
    'edPz_Column11
    '
    Me.edPz_Column11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Column11.Location = New System.Drawing.Point(188, 27)
    Me.edPz_Column11.Name = "edPz_Column11"
    Me.edPz_Column11.NTSDbField = ""
    Me.edPz_Column11.NTSForzaVisZoom = False
    Me.edPz_Column11.NTSOldValue = ""
    Me.edPz_Column11.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Column11.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Column11.Properties.AutoHeight = False
    Me.edPz_Column11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Column11.Properties.MaxLength = 65536
    Me.edPz_Column11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Column11.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Column11.TabIndex = 12
    '
    'lbPz_Descam11
    '
    Me.lbPz_Descam11.AutoSize = True
    Me.lbPz_Descam11.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Descam11.Location = New System.Drawing.Point(20, 9)
    Me.lbPz_Descam11.Name = "lbPz_Descam11"
    Me.lbPz_Descam11.NTSDbField = ""
    Me.lbPz_Descam11.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Descam11.TabIndex = 11
    Me.lbPz_Descam11.Text = "Descrizione Campo"
    Me.lbPz_Descam11.Tooltip = ""
    Me.lbPz_Descam11.UseMnemonic = False
    '
    'edPz_Descam15
    '
    Me.edPz_Descam15.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam15.Location = New System.Drawing.Point(23, 115)
    Me.edPz_Descam15.Name = "edPz_Descam15"
    Me.edPz_Descam15.NTSDbField = ""
    Me.edPz_Descam15.NTSForzaVisZoom = False
    Me.edPz_Descam15.NTSOldValue = ""
    Me.edPz_Descam15.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam15.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam15.Properties.AutoHeight = False
    Me.edPz_Descam15.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam15.Properties.MaxLength = 65536
    Me.edPz_Descam15.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam15.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam15.TabIndex = 10
    '
    'edPz_Descam14
    '
    Me.edPz_Descam14.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam14.Location = New System.Drawing.Point(23, 93)
    Me.edPz_Descam14.Name = "edPz_Descam14"
    Me.edPz_Descam14.NTSDbField = ""
    Me.edPz_Descam14.NTSForzaVisZoom = False
    Me.edPz_Descam14.NTSOldValue = ""
    Me.edPz_Descam14.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam14.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam14.Properties.AutoHeight = False
    Me.edPz_Descam14.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam14.Properties.MaxLength = 65536
    Me.edPz_Descam14.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam14.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam14.TabIndex = 9
    '
    'edPz_Descam13
    '
    Me.edPz_Descam13.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam13.Location = New System.Drawing.Point(23, 71)
    Me.edPz_Descam13.Name = "edPz_Descam13"
    Me.edPz_Descam13.NTSDbField = ""
    Me.edPz_Descam13.NTSForzaVisZoom = False
    Me.edPz_Descam13.NTSOldValue = ""
    Me.edPz_Descam13.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam13.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam13.Properties.AutoHeight = False
    Me.edPz_Descam13.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam13.Properties.MaxLength = 65536
    Me.edPz_Descam13.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam13.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam13.TabIndex = 8
    '
    'edPz_Descam12
    '
    Me.edPz_Descam12.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam12.Location = New System.Drawing.Point(23, 49)
    Me.edPz_Descam12.Name = "edPz_Descam12"
    Me.edPz_Descam12.NTSDbField = ""
    Me.edPz_Descam12.NTSForzaVisZoom = False
    Me.edPz_Descam12.NTSOldValue = ""
    Me.edPz_Descam12.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam12.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam12.Properties.AutoHeight = False
    Me.edPz_Descam12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam12.Properties.MaxLength = 65536
    Me.edPz_Descam12.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam12.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam12.TabIndex = 7
    '
    'edPz_Descam11
    '
    Me.edPz_Descam11.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descam11.Location = New System.Drawing.Point(23, 27)
    Me.edPz_Descam11.Name = "edPz_Descam11"
    Me.edPz_Descam11.NTSDbField = ""
    Me.edPz_Descam11.NTSForzaVisZoom = False
    Me.edPz_Descam11.NTSOldValue = ""
    Me.edPz_Descam11.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descam11.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descam11.Properties.AutoHeight = False
    Me.edPz_Descam11.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descam11.Properties.MaxLength = 65536
    Me.edPz_Descam11.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descam11.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descam11.TabIndex = 6
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.NtsPanel1)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(631, 140)
    Me.NtsTabPage4.Text = "&4 Filtri Subquery"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbPz_Vals1)
    Me.NtsPanel1.Controls.Add(Me.lbPz_Ops1)
    Me.NtsPanel1.Controls.Add(Me.lbPz_Tipos1)
    Me.NtsPanel1.Controls.Add(Me.lbPz_Columns1)
    Me.NtsPanel1.Controls.Add(Me.lbSubQ5)
    Me.NtsPanel1.Controls.Add(Me.lbSubQ4)
    Me.NtsPanel1.Controls.Add(Me.lbSubQ3)
    Me.NtsPanel1.Controls.Add(Me.lbSubQ2)
    Me.NtsPanel1.Controls.Add(Me.lbSubQ1)
    Me.NtsPanel1.Controls.Add(Me.cmdnFiltros5)
    Me.NtsPanel1.Controls.Add(Me.cmdnFiltros4)
    Me.NtsPanel1.Controls.Add(Me.cmdnFiltros3)
    Me.NtsPanel1.Controls.Add(Me.cmdnFiltros2)
    Me.NtsPanel1.Controls.Add(Me.cmdnFiltros1)
    Me.NtsPanel1.Controls.Add(Me.edPz_Vals5)
    Me.NtsPanel1.Controls.Add(Me.edPz_Vals4)
    Me.NtsPanel1.Controls.Add(Me.edPz_Vals3)
    Me.NtsPanel1.Controls.Add(Me.edPz_Vals2)
    Me.NtsPanel1.Controls.Add(Me.edPz_Vals1)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Ops5)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Ops4)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Ops3)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Ops2)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Ops1)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Tipos5)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Tipos4)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Tipos3)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Tipos2)
    Me.NtsPanel1.Controls.Add(Me.cbPz_Tipos1)
    Me.NtsPanel1.Controls.Add(Me.edPz_Columns5)
    Me.NtsPanel1.Controls.Add(Me.edPz_Columns4)
    Me.NtsPanel1.Controls.Add(Me.edPz_Columns3)
    Me.NtsPanel1.Controls.Add(Me.edPz_Columns2)
    Me.NtsPanel1.Controls.Add(Me.edPz_Columns1)
    Me.NtsPanel1.Controls.Add(Me.lbPz_Descams1)
    Me.NtsPanel1.Controls.Add(Me.edPz_Descams5)
    Me.NtsPanel1.Controls.Add(Me.edPz_Descams4)
    Me.NtsPanel1.Controls.Add(Me.edPz_Descams3)
    Me.NtsPanel1.Controls.Add(Me.edPz_Descams2)
    Me.NtsPanel1.Controls.Add(Me.edPz_Descams1)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(631, 140)
    Me.NtsPanel1.TabIndex = 7
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'lbPz_Vals1
    '
    Me.lbPz_Vals1.AutoSize = True
    Me.lbPz_Vals1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Vals1.Location = New System.Drawing.Point(495, 9)
    Me.lbPz_Vals1.Name = "lbPz_Vals1"
    Me.lbPz_Vals1.NTSDbField = ""
    Me.lbPz_Vals1.Size = New System.Drawing.Size(73, 13)
    Me.lbPz_Vals1.TabIndex = 45
    Me.lbPz_Vals1.Text = "Valore Campo"
    Me.lbPz_Vals1.Tooltip = ""
    Me.lbPz_Vals1.UseMnemonic = False
    '
    'lbPz_Ops1
    '
    Me.lbPz_Ops1.AutoSize = True
    Me.lbPz_Ops1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Ops1.Location = New System.Drawing.Point(415, 9)
    Me.lbPz_Ops1.Name = "lbPz_Ops1"
    Me.lbPz_Ops1.NTSDbField = ""
    Me.lbPz_Ops1.Size = New System.Drawing.Size(57, 13)
    Me.lbPz_Ops1.TabIndex = 44
    Me.lbPz_Ops1.Text = "Operatore"
    Me.lbPz_Ops1.Tooltip = ""
    Me.lbPz_Ops1.UseMnemonic = False
    '
    'lbPz_Tipos1
    '
    Me.lbPz_Tipos1.AutoSize = True
    Me.lbPz_Tipos1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Tipos1.Location = New System.Drawing.Point(329, 9)
    Me.lbPz_Tipos1.Name = "lbPz_Tipos1"
    Me.lbPz_Tipos1.NTSDbField = ""
    Me.lbPz_Tipos1.Size = New System.Drawing.Size(63, 13)
    Me.lbPz_Tipos1.TabIndex = 43
    Me.lbPz_Tipos1.Text = "Tipo Campo"
    Me.lbPz_Tipos1.Tooltip = ""
    Me.lbPz_Tipos1.UseMnemonic = False
    '
    'lbPz_Columns1
    '
    Me.lbPz_Columns1.AutoSize = True
    Me.lbPz_Columns1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Columns1.Location = New System.Drawing.Point(185, 9)
    Me.lbPz_Columns1.Name = "lbPz_Columns1"
    Me.lbPz_Columns1.NTSDbField = ""
    Me.lbPz_Columns1.Size = New System.Drawing.Size(70, 13)
    Me.lbPz_Columns1.TabIndex = 42
    Me.lbPz_Columns1.Text = "Nome Campo"
    Me.lbPz_Columns1.Tooltip = ""
    Me.lbPz_Columns1.UseMnemonic = False
    '
    'lbSubQ5
    '
    Me.lbSubQ5.AutoSize = True
    Me.lbSubQ5.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQ5.Location = New System.Drawing.Point(10, 118)
    Me.lbSubQ5.Name = "lbSubQ5"
    Me.lbSubQ5.NTSDbField = ""
    Me.lbSubQ5.Size = New System.Drawing.Size(13, 13)
    Me.lbSubQ5.TabIndex = 41
    Me.lbSubQ5.Text = "5"
    Me.lbSubQ5.Tooltip = ""
    Me.lbSubQ5.UseMnemonic = False
    '
    'lbSubQ4
    '
    Me.lbSubQ4.AutoSize = True
    Me.lbSubQ4.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQ4.Location = New System.Drawing.Point(10, 96)
    Me.lbSubQ4.Name = "lbSubQ4"
    Me.lbSubQ4.NTSDbField = ""
    Me.lbSubQ4.Size = New System.Drawing.Size(13, 13)
    Me.lbSubQ4.TabIndex = 40
    Me.lbSubQ4.Text = "4"
    Me.lbSubQ4.Tooltip = ""
    Me.lbSubQ4.UseMnemonic = False
    '
    'lbSubQ3
    '
    Me.lbSubQ3.AutoSize = True
    Me.lbSubQ3.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQ3.Location = New System.Drawing.Point(10, 74)
    Me.lbSubQ3.Name = "lbSubQ3"
    Me.lbSubQ3.NTSDbField = ""
    Me.lbSubQ3.Size = New System.Drawing.Size(13, 13)
    Me.lbSubQ3.TabIndex = 39
    Me.lbSubQ3.Text = "3"
    Me.lbSubQ3.Tooltip = ""
    Me.lbSubQ3.UseMnemonic = False
    '
    'lbSubQ2
    '
    Me.lbSubQ2.AutoSize = True
    Me.lbSubQ2.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQ2.Location = New System.Drawing.Point(10, 52)
    Me.lbSubQ2.Name = "lbSubQ2"
    Me.lbSubQ2.NTSDbField = ""
    Me.lbSubQ2.Size = New System.Drawing.Size(13, 13)
    Me.lbSubQ2.TabIndex = 38
    Me.lbSubQ2.Text = "2"
    Me.lbSubQ2.Tooltip = ""
    Me.lbSubQ2.UseMnemonic = False
    '
    'lbSubQ1
    '
    Me.lbSubQ1.AutoSize = True
    Me.lbSubQ1.BackColor = System.Drawing.Color.Transparent
    Me.lbSubQ1.Location = New System.Drawing.Point(10, 30)
    Me.lbSubQ1.Name = "lbSubQ1"
    Me.lbSubQ1.NTSDbField = ""
    Me.lbSubQ1.Size = New System.Drawing.Size(13, 13)
    Me.lbSubQ1.TabIndex = 37
    Me.lbSubQ1.Text = "1"
    Me.lbSubQ1.Tooltip = ""
    Me.lbSubQ1.UseMnemonic = False
    '
    'cmdnFiltros5
    '
    Me.cmdnFiltros5.ImageText = ""
    Me.cmdnFiltros5.Location = New System.Drawing.Point(596, 115)
    Me.cmdnFiltros5.Name = "cmdnFiltros5"
    Me.cmdnFiltros5.NTSContextMenu = Nothing
    Me.cmdnFiltros5.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltros5.TabIndex = 36
    Me.cmdnFiltros5.Text = "5"
    '
    'cmdnFiltros4
    '
    Me.cmdnFiltros4.ImageText = ""
    Me.cmdnFiltros4.Location = New System.Drawing.Point(596, 93)
    Me.cmdnFiltros4.Name = "cmdnFiltros4"
    Me.cmdnFiltros4.NTSContextMenu = Nothing
    Me.cmdnFiltros4.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltros4.TabIndex = 35
    Me.cmdnFiltros4.Text = "4"
    '
    'cmdnFiltros3
    '
    Me.cmdnFiltros3.ImageText = ""
    Me.cmdnFiltros3.Location = New System.Drawing.Point(596, 71)
    Me.cmdnFiltros3.Name = "cmdnFiltros3"
    Me.cmdnFiltros3.NTSContextMenu = Nothing
    Me.cmdnFiltros3.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltros3.TabIndex = 34
    Me.cmdnFiltros3.Text = "3"
    '
    'cmdnFiltros2
    '
    Me.cmdnFiltros2.ImageText = ""
    Me.cmdnFiltros2.Location = New System.Drawing.Point(596, 49)
    Me.cmdnFiltros2.Name = "cmdnFiltros2"
    Me.cmdnFiltros2.NTSContextMenu = Nothing
    Me.cmdnFiltros2.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltros2.TabIndex = 33
    Me.cmdnFiltros2.Text = "2"
    '
    'cmdnFiltros1
    '
    Me.cmdnFiltros1.ImageText = ""
    Me.cmdnFiltros1.Location = New System.Drawing.Point(596, 27)
    Me.cmdnFiltros1.Name = "cmdnFiltros1"
    Me.cmdnFiltros1.NTSContextMenu = Nothing
    Me.cmdnFiltros1.Size = New System.Drawing.Size(26, 20)
    Me.cmdnFiltros1.TabIndex = 32
    Me.cmdnFiltros1.Text = "1"
    '
    'edPz_Vals5
    '
    Me.edPz_Vals5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals5.Location = New System.Drawing.Point(498, 115)
    Me.edPz_Vals5.Name = "edPz_Vals5"
    Me.edPz_Vals5.NTSDbField = ""
    Me.edPz_Vals5.NTSForzaVisZoom = False
    Me.edPz_Vals5.NTSOldValue = ""
    Me.edPz_Vals5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals5.Properties.AutoHeight = False
    Me.edPz_Vals5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals5.Properties.MaxLength = 65536
    Me.edPz_Vals5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals5.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Vals5.TabIndex = 31
    '
    'edPz_Vals4
    '
    Me.edPz_Vals4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals4.Location = New System.Drawing.Point(498, 93)
    Me.edPz_Vals4.Name = "edPz_Vals4"
    Me.edPz_Vals4.NTSDbField = ""
    Me.edPz_Vals4.NTSForzaVisZoom = False
    Me.edPz_Vals4.NTSOldValue = ""
    Me.edPz_Vals4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals4.Properties.AutoHeight = False
    Me.edPz_Vals4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals4.Properties.MaxLength = 65536
    Me.edPz_Vals4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals4.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Vals4.TabIndex = 30
    '
    'edPz_Vals3
    '
    Me.edPz_Vals3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals3.Location = New System.Drawing.Point(498, 71)
    Me.edPz_Vals3.Name = "edPz_Vals3"
    Me.edPz_Vals3.NTSDbField = ""
    Me.edPz_Vals3.NTSForzaVisZoom = False
    Me.edPz_Vals3.NTSOldValue = ""
    Me.edPz_Vals3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals3.Properties.AutoHeight = False
    Me.edPz_Vals3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals3.Properties.MaxLength = 65536
    Me.edPz_Vals3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals3.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Vals3.TabIndex = 29
    '
    'edPz_Vals2
    '
    Me.edPz_Vals2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals2.Location = New System.Drawing.Point(498, 49)
    Me.edPz_Vals2.Name = "edPz_Vals2"
    Me.edPz_Vals2.NTSDbField = ""
    Me.edPz_Vals2.NTSForzaVisZoom = False
    Me.edPz_Vals2.NTSOldValue = ""
    Me.edPz_Vals2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals2.Properties.AutoHeight = False
    Me.edPz_Vals2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals2.Properties.MaxLength = 65536
    Me.edPz_Vals2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals2.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Vals2.TabIndex = 28
    '
    'edPz_Vals1
    '
    Me.edPz_Vals1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Vals1.Location = New System.Drawing.Point(498, 27)
    Me.edPz_Vals1.Name = "edPz_Vals1"
    Me.edPz_Vals1.NTSDbField = ""
    Me.edPz_Vals1.NTSForzaVisZoom = False
    Me.edPz_Vals1.NTSOldValue = ""
    Me.edPz_Vals1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Vals1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Vals1.Properties.AutoHeight = False
    Me.edPz_Vals1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Vals1.Properties.MaxLength = 65536
    Me.edPz_Vals1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Vals1.Size = New System.Drawing.Size(92, 20)
    Me.edPz_Vals1.TabIndex = 27
    '
    'cbPz_Ops5
    '
    Me.cbPz_Ops5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops5.DataSource = Nothing
    Me.cbPz_Ops5.DisplayMember = ""
    Me.cbPz_Ops5.Location = New System.Drawing.Point(418, 115)
    Me.cbPz_Ops5.Name = "cbPz_Ops5"
    Me.cbPz_Ops5.NTSDbField = ""
    Me.cbPz_Ops5.Properties.AutoHeight = False
    Me.cbPz_Ops5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops5.Properties.DropDownRows = 30
    Me.cbPz_Ops5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops5.SelectedValue = ""
    Me.cbPz_Ops5.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Ops5.TabIndex = 26
    Me.cbPz_Ops5.ValueMember = ""
    '
    'cbPz_Ops4
    '
    Me.cbPz_Ops4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops4.DataSource = Nothing
    Me.cbPz_Ops4.DisplayMember = ""
    Me.cbPz_Ops4.Location = New System.Drawing.Point(418, 93)
    Me.cbPz_Ops4.Name = "cbPz_Ops4"
    Me.cbPz_Ops4.NTSDbField = ""
    Me.cbPz_Ops4.Properties.AutoHeight = False
    Me.cbPz_Ops4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops4.Properties.DropDownRows = 30
    Me.cbPz_Ops4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops4.SelectedValue = ""
    Me.cbPz_Ops4.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Ops4.TabIndex = 25
    Me.cbPz_Ops4.ValueMember = ""
    '
    'cbPz_Ops3
    '
    Me.cbPz_Ops3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops3.DataSource = Nothing
    Me.cbPz_Ops3.DisplayMember = ""
    Me.cbPz_Ops3.Location = New System.Drawing.Point(418, 71)
    Me.cbPz_Ops3.Name = "cbPz_Ops3"
    Me.cbPz_Ops3.NTSDbField = ""
    Me.cbPz_Ops3.Properties.AutoHeight = False
    Me.cbPz_Ops3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops3.Properties.DropDownRows = 30
    Me.cbPz_Ops3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops3.SelectedValue = ""
    Me.cbPz_Ops3.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Ops3.TabIndex = 24
    Me.cbPz_Ops3.ValueMember = ""
    '
    'cbPz_Ops2
    '
    Me.cbPz_Ops2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops2.DataSource = Nothing
    Me.cbPz_Ops2.DisplayMember = ""
    Me.cbPz_Ops2.Location = New System.Drawing.Point(418, 49)
    Me.cbPz_Ops2.Name = "cbPz_Ops2"
    Me.cbPz_Ops2.NTSDbField = ""
    Me.cbPz_Ops2.Properties.AutoHeight = False
    Me.cbPz_Ops2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops2.Properties.DropDownRows = 30
    Me.cbPz_Ops2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops2.SelectedValue = ""
    Me.cbPz_Ops2.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Ops2.TabIndex = 23
    Me.cbPz_Ops2.ValueMember = ""
    '
    'cbPz_Ops1
    '
    Me.cbPz_Ops1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Ops1.DataSource = Nothing
    Me.cbPz_Ops1.DisplayMember = ""
    Me.cbPz_Ops1.Location = New System.Drawing.Point(418, 27)
    Me.cbPz_Ops1.Name = "cbPz_Ops1"
    Me.cbPz_Ops1.NTSDbField = ""
    Me.cbPz_Ops1.Properties.AutoHeight = False
    Me.cbPz_Ops1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Ops1.Properties.DropDownRows = 30
    Me.cbPz_Ops1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Ops1.SelectedValue = ""
    Me.cbPz_Ops1.Size = New System.Drawing.Size(72, 20)
    Me.cbPz_Ops1.TabIndex = 22
    Me.cbPz_Ops1.ValueMember = ""
    '
    'cbPz_Tipos5
    '
    Me.cbPz_Tipos5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipos5.DataSource = Nothing
    Me.cbPz_Tipos5.DisplayMember = ""
    Me.cbPz_Tipos5.Location = New System.Drawing.Point(332, 115)
    Me.cbPz_Tipos5.Name = "cbPz_Tipos5"
    Me.cbPz_Tipos5.NTSDbField = ""
    Me.cbPz_Tipos5.Properties.AutoHeight = False
    Me.cbPz_Tipos5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipos5.Properties.DropDownRows = 30
    Me.cbPz_Tipos5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipos5.SelectedValue = ""
    Me.cbPz_Tipos5.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipos5.TabIndex = 21
    Me.cbPz_Tipos5.ValueMember = ""
    '
    'cbPz_Tipos4
    '
    Me.cbPz_Tipos4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipos4.DataSource = Nothing
    Me.cbPz_Tipos4.DisplayMember = ""
    Me.cbPz_Tipos4.Location = New System.Drawing.Point(332, 93)
    Me.cbPz_Tipos4.Name = "cbPz_Tipos4"
    Me.cbPz_Tipos4.NTSDbField = ""
    Me.cbPz_Tipos4.Properties.AutoHeight = False
    Me.cbPz_Tipos4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipos4.Properties.DropDownRows = 30
    Me.cbPz_Tipos4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipos4.SelectedValue = ""
    Me.cbPz_Tipos4.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipos4.TabIndex = 20
    Me.cbPz_Tipos4.ValueMember = ""
    '
    'cbPz_Tipos3
    '
    Me.cbPz_Tipos3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipos3.DataSource = Nothing
    Me.cbPz_Tipos3.DisplayMember = ""
    Me.cbPz_Tipos3.Location = New System.Drawing.Point(332, 71)
    Me.cbPz_Tipos3.Name = "cbPz_Tipos3"
    Me.cbPz_Tipos3.NTSDbField = ""
    Me.cbPz_Tipos3.Properties.AutoHeight = False
    Me.cbPz_Tipos3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipos3.Properties.DropDownRows = 30
    Me.cbPz_Tipos3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipos3.SelectedValue = ""
    Me.cbPz_Tipos3.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipos3.TabIndex = 19
    Me.cbPz_Tipos3.ValueMember = ""
    '
    'cbPz_Tipos2
    '
    Me.cbPz_Tipos2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbPz_Tipos2.DataSource = Nothing
    Me.cbPz_Tipos2.DisplayMember = ""
    Me.cbPz_Tipos2.Location = New System.Drawing.Point(332, 49)
    Me.cbPz_Tipos2.Name = "cbPz_Tipos2"
    Me.cbPz_Tipos2.NTSDbField = ""
    Me.cbPz_Tipos2.Properties.AutoHeight = False
    Me.cbPz_Tipos2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipos2.Properties.DropDownRows = 30
    Me.cbPz_Tipos2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipos2.SelectedValue = ""
    Me.cbPz_Tipos2.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipos2.TabIndex = 18
    Me.cbPz_Tipos2.ValueMember = ""
    '
    'cbPz_Tipos1
    '
    Me.cbPz_Tipos1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPz_Tipos1.DataSource = Nothing
    Me.cbPz_Tipos1.DisplayMember = ""
    Me.cbPz_Tipos1.Location = New System.Drawing.Point(332, 27)
    Me.cbPz_Tipos1.Name = "cbPz_Tipos1"
    Me.cbPz_Tipos1.NTSDbField = ""
    Me.cbPz_Tipos1.Properties.AutoHeight = False
    Me.cbPz_Tipos1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPz_Tipos1.Properties.DropDownRows = 30
    Me.cbPz_Tipos1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPz_Tipos1.SelectedValue = ""
    Me.cbPz_Tipos1.Size = New System.Drawing.Size(78, 20)
    Me.cbPz_Tipos1.TabIndex = 17
    Me.cbPz_Tipos1.ValueMember = ""
    '
    'edPz_Columns5
    '
    Me.edPz_Columns5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Columns5.Location = New System.Drawing.Point(188, 115)
    Me.edPz_Columns5.Name = "edPz_Columns5"
    Me.edPz_Columns5.NTSDbField = ""
    Me.edPz_Columns5.NTSForzaVisZoom = False
    Me.edPz_Columns5.NTSOldValue = ""
    Me.edPz_Columns5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Columns5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Columns5.Properties.AutoHeight = False
    Me.edPz_Columns5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Columns5.Properties.MaxLength = 65536
    Me.edPz_Columns5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Columns5.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Columns5.TabIndex = 16
    '
    'edPz_Columns4
    '
    Me.edPz_Columns4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Columns4.Location = New System.Drawing.Point(188, 93)
    Me.edPz_Columns4.Name = "edPz_Columns4"
    Me.edPz_Columns4.NTSDbField = ""
    Me.edPz_Columns4.NTSForzaVisZoom = False
    Me.edPz_Columns4.NTSOldValue = ""
    Me.edPz_Columns4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Columns4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Columns4.Properties.AutoHeight = False
    Me.edPz_Columns4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Columns4.Properties.MaxLength = 65536
    Me.edPz_Columns4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Columns4.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Columns4.TabIndex = 15
    '
    'edPz_Columns3
    '
    Me.edPz_Columns3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Columns3.Location = New System.Drawing.Point(188, 71)
    Me.edPz_Columns3.Name = "edPz_Columns3"
    Me.edPz_Columns3.NTSDbField = ""
    Me.edPz_Columns3.NTSForzaVisZoom = False
    Me.edPz_Columns3.NTSOldValue = ""
    Me.edPz_Columns3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Columns3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Columns3.Properties.AutoHeight = False
    Me.edPz_Columns3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Columns3.Properties.MaxLength = 65536
    Me.edPz_Columns3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Columns3.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Columns3.TabIndex = 14
    '
    'edPz_Columns2
    '
    Me.edPz_Columns2.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPz_Columns2.Location = New System.Drawing.Point(188, 49)
    Me.edPz_Columns2.Name = "edPz_Columns2"
    Me.edPz_Columns2.NTSDbField = ""
    Me.edPz_Columns2.NTSForzaVisZoom = False
    Me.edPz_Columns2.NTSOldValue = ""
    Me.edPz_Columns2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Columns2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Columns2.Properties.AutoHeight = False
    Me.edPz_Columns2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Columns2.Properties.MaxLength = 65536
    Me.edPz_Columns2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Columns2.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Columns2.TabIndex = 13
    '
    'edPz_Columns1
    '
    Me.edPz_Columns1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Columns1.Location = New System.Drawing.Point(188, 27)
    Me.edPz_Columns1.Name = "edPz_Columns1"
    Me.edPz_Columns1.NTSDbField = ""
    Me.edPz_Columns1.NTSForzaVisZoom = False
    Me.edPz_Columns1.NTSOldValue = ""
    Me.edPz_Columns1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Columns1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Columns1.Properties.AutoHeight = False
    Me.edPz_Columns1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Columns1.Properties.MaxLength = 65536
    Me.edPz_Columns1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Columns1.Size = New System.Drawing.Size(138, 20)
    Me.edPz_Columns1.TabIndex = 12
    '
    'lbPz_Descams1
    '
    Me.lbPz_Descams1.AutoSize = True
    Me.lbPz_Descams1.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Descams1.Location = New System.Drawing.Point(20, 9)
    Me.lbPz_Descams1.Name = "lbPz_Descams1"
    Me.lbPz_Descams1.NTSDbField = ""
    Me.lbPz_Descams1.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Descams1.TabIndex = 11
    Me.lbPz_Descams1.Text = "Descrizione Campo"
    Me.lbPz_Descams1.Tooltip = ""
    Me.lbPz_Descams1.UseMnemonic = False
    '
    'edPz_Descams5
    '
    Me.edPz_Descams5.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPz_Descams5.Location = New System.Drawing.Point(23, 115)
    Me.edPz_Descams5.Name = "edPz_Descams5"
    Me.edPz_Descams5.NTSDbField = ""
    Me.edPz_Descams5.NTSForzaVisZoom = False
    Me.edPz_Descams5.NTSOldValue = ""
    Me.edPz_Descams5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams5.Properties.AutoHeight = False
    Me.edPz_Descams5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams5.Properties.MaxLength = 65536
    Me.edPz_Descams5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams5.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descams5.TabIndex = 10
    '
    'edPz_Descams4
    '
    Me.edPz_Descams4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams4.Location = New System.Drawing.Point(23, 93)
    Me.edPz_Descams4.Name = "edPz_Descams4"
    Me.edPz_Descams4.NTSDbField = ""
    Me.edPz_Descams4.NTSForzaVisZoom = False
    Me.edPz_Descams4.NTSOldValue = ""
    Me.edPz_Descams4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams4.Properties.AutoHeight = False
    Me.edPz_Descams4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams4.Properties.MaxLength = 65536
    Me.edPz_Descams4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams4.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descams4.TabIndex = 9
    '
    'edPz_Descams3
    '
    Me.edPz_Descams3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams3.Location = New System.Drawing.Point(23, 71)
    Me.edPz_Descams3.Name = "edPz_Descams3"
    Me.edPz_Descams3.NTSDbField = ""
    Me.edPz_Descams3.NTSForzaVisZoom = False
    Me.edPz_Descams3.NTSOldValue = ""
    Me.edPz_Descams3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams3.Properties.AutoHeight = False
    Me.edPz_Descams3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams3.Properties.MaxLength = 65536
    Me.edPz_Descams3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams3.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descams3.TabIndex = 8
    '
    'edPz_Descams2
    '
    Me.edPz_Descams2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams2.Location = New System.Drawing.Point(23, 49)
    Me.edPz_Descams2.Name = "edPz_Descams2"
    Me.edPz_Descams2.NTSDbField = ""
    Me.edPz_Descams2.NTSForzaVisZoom = False
    Me.edPz_Descams2.NTSOldValue = ""
    Me.edPz_Descams2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams2.Properties.AutoHeight = False
    Me.edPz_Descams2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams2.Properties.MaxLength = 65536
    Me.edPz_Descams2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams2.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descams2.TabIndex = 7
    '
    'edPz_Descams1
    '
    Me.edPz_Descams1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Descams1.Location = New System.Drawing.Point(23, 27)
    Me.edPz_Descams1.Name = "edPz_Descams1"
    Me.edPz_Descams1.NTSDbField = ""
    Me.edPz_Descams1.NTSForzaVisZoom = False
    Me.edPz_Descams1.NTSOldValue = ""
    Me.edPz_Descams1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Descams1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Descams1.Properties.AutoHeight = False
    Me.edPz_Descams1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Descams1.Properties.MaxLength = 65536
    Me.edPz_Descams1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Descams1.Size = New System.Drawing.Size(159, 20)
    Me.edPz_Descams1.TabIndex = 6
    '
    'edPz_strOprel
    '
    Me.edPz_strOprel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_strOprel.Location = New System.Drawing.Point(105, 34)
    Me.edPz_strOprel.Name = "edPz_strOprel"
    Me.edPz_strOprel.NTSDbField = ""
    Me.edPz_strOprel.NTSForzaVisZoom = False
    Me.edPz_strOprel.NTSOldValue = ""
    Me.edPz_strOprel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_strOprel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_strOprel.Properties.AutoHeight = False
    Me.edPz_strOprel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_strOprel.Properties.MaxLength = 65536
    Me.edPz_strOprel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_strOprel.Size = New System.Drawing.Size(604, 20)
    Me.edPz_strOprel.TabIndex = 5
    '
    'lbDisplayfiltri2
    '
    Me.lbDisplayfiltri2.AutoSize = True
    Me.lbDisplayfiltri2.BackColor = System.Drawing.Color.Transparent
    Me.lbDisplayfiltri2.Location = New System.Drawing.Point(12, 41)
    Me.lbDisplayfiltri2.Name = "lbDisplayfiltri2"
    Me.lbDisplayfiltri2.NTSDbField = ""
    Me.lbDisplayfiltri2.Size = New System.Drawing.Size(79, 13)
    Me.lbDisplayfiltri2.TabIndex = 3
    Me.lbDisplayfiltri2.Text = "relazionale filtri"
    Me.lbDisplayfiltri2.Tooltip = ""
    Me.lbDisplayfiltri2.UseMnemonic = False
    '
    'lbDisplayfiltri1
    '
    Me.lbDisplayfiltri1.AutoSize = True
    Me.lbDisplayfiltri1.BackColor = System.Drawing.Color.Transparent
    Me.lbDisplayfiltri1.Location = New System.Drawing.Point(12, 25)
    Me.lbDisplayfiltri1.Name = "lbDisplayfiltri1"
    Me.lbDisplayfiltri1.NTSDbField = ""
    Me.lbDisplayfiltri1.Size = New System.Drawing.Size(72, 13)
    Me.lbDisplayfiltri1.TabIndex = 0
    Me.lbDisplayfiltri1.Text = "Composizione"
    Me.lbDisplayfiltri1.Tooltip = ""
    Me.lbDisplayfiltri1.UseMnemonic = False
    '
    'lbPz_Sqlsql
    '
    Me.lbPz_Sqlsql.AutoSize = True
    Me.lbPz_Sqlsql.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Sqlsql.Location = New System.Drawing.Point(5, 52)
    Me.lbPz_Sqlsql.Name = "lbPz_Sqlsql"
    Me.lbPz_Sqlsql.NTSDbField = ""
    Me.lbPz_Sqlsql.Size = New System.Drawing.Size(98, 13)
    Me.lbPz_Sqlsql.TabIndex = 25
    Me.lbPz_Sqlsql.Text = "Stringa SQL Server"
    Me.lbPz_Sqlsql.Tooltip = ""
    Me.lbPz_Sqlsql.UseMnemonic = False
    '
    'edPz_Sqlsql
    '
    Me.edPz_Sqlsql.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Sqlsql.Location = New System.Drawing.Point(108, 50)
    Me.edPz_Sqlsql.Name = "edPz_Sqlsql"
    Me.edPz_Sqlsql.NTSDbField = ""
    Me.edPz_Sqlsql.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Sqlsql.Size = New System.Drawing.Size(621, 82)
    Me.edPz_Sqlsql.TabIndex = 24
    '
    'edPz_maintab
    '
    Me.edPz_maintab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_maintab.Location = New System.Drawing.Point(613, 28)
    Me.edPz_maintab.Name = "edPz_maintab"
    Me.edPz_maintab.NTSDbField = ""
    Me.edPz_maintab.NTSForzaVisZoom = False
    Me.edPz_maintab.NTSOldValue = ""
    Me.edPz_maintab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_maintab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_maintab.Properties.AutoHeight = False
    Me.edPz_maintab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_maintab.Properties.MaxLength = 65536
    Me.edPz_maintab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_maintab.Size = New System.Drawing.Size(116, 20)
    Me.edPz_maintab.TabIndex = 22
    '
    'edPz_Titstam
    '
    Me.edPz_Titstam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Titstam.Location = New System.Drawing.Point(108, 28)
    Me.edPz_Titstam.Name = "edPz_Titstam"
    Me.edPz_Titstam.NTSDbField = ""
    Me.edPz_Titstam.NTSForzaVisZoom = False
    Me.edPz_Titstam.NTSOldValue = ""
    Me.edPz_Titstam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Titstam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Titstam.Properties.AutoHeight = False
    Me.edPz_Titstam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Titstam.Properties.MaxLength = 65536
    Me.edPz_Titstam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Titstam.Size = New System.Drawing.Size(304, 20)
    Me.edPz_Titstam.TabIndex = 21
    '
    'edPz_Nomrpt
    '
    Me.edPz_Nomrpt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Nomrpt.Location = New System.Drawing.Point(287, 6)
    Me.edPz_Nomrpt.Name = "edPz_Nomrpt"
    Me.edPz_Nomrpt.NTSDbField = ""
    Me.edPz_Nomrpt.NTSForzaVisZoom = False
    Me.edPz_Nomrpt.NTSOldValue = ""
    Me.edPz_Nomrpt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Nomrpt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Nomrpt.Properties.AutoHeight = False
    Me.edPz_Nomrpt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Nomrpt.Properties.MaxLength = 65536
    Me.edPz_Nomrpt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Nomrpt.Size = New System.Drawing.Size(125, 20)
    Me.edPz_Nomrpt.TabIndex = 20
    '
    'edPz_Codform
    '
    Me.edPz_Codform.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_Codform.Location = New System.Drawing.Point(108, 6)
    Me.edPz_Codform.Name = "edPz_Codform"
    Me.edPz_Codform.NTSDbField = ""
    Me.edPz_Codform.NTSFormat = "0"
    Me.edPz_Codform.NTSForzaVisZoom = False
    Me.edPz_Codform.NTSOldValue = ""
    Me.edPz_Codform.Properties.Appearance.Options.UseTextOptions = True
    Me.edPz_Codform.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPz_Codform.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_Codform.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_Codform.Properties.AutoHeight = False
    Me.edPz_Codform.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_Codform.Properties.MaxLength = 65536
    Me.edPz_Codform.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_Codform.Size = New System.Drawing.Size(85, 20)
    Me.edPz_Codform.TabIndex = 19
    '
    'lbPz_maintab
    '
    Me.lbPz_maintab.AutoSize = True
    Me.lbPz_maintab.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_maintab.Location = New System.Drawing.Point(435, 31)
    Me.lbPz_maintab.Name = "lbPz_maintab"
    Me.lbPz_maintab.NTSDbField = ""
    Me.lbPz_maintab.Size = New System.Drawing.Size(166, 13)
    Me.lbPz_maintab.TabIndex = 18
    Me.lbPz_maintab.Text = "Tabella principale per codice ditta"
    Me.lbPz_maintab.Tooltip = ""
    Me.lbPz_maintab.UseMnemonic = False
    '
    'lbPz_Nomrpt
    '
    Me.lbPz_Nomrpt.AutoSize = True
    Me.lbPz_Nomrpt.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Nomrpt.Location = New System.Drawing.Point(206, 10)
    Me.lbPz_Nomrpt.Name = "lbPz_Nomrpt"
    Me.lbPz_Nomrpt.NTSDbField = ""
    Me.lbPz_Nomrpt.Size = New System.Drawing.Size(75, 13)
    Me.lbPz_Nomrpt.TabIndex = 17
    Me.lbPz_Nomrpt.Text = "Nome File RPT"
    Me.lbPz_Nomrpt.Tooltip = ""
    Me.lbPz_Nomrpt.UseMnemonic = False
    '
    'lbPz_Titstam
    '
    Me.lbPz_Titstam.AutoSize = True
    Me.lbPz_Titstam.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Titstam.Location = New System.Drawing.Point(5, 31)
    Me.lbPz_Titstam.Name = "lbPz_Titstam"
    Me.lbPz_Titstam.NTSDbField = ""
    Me.lbPz_Titstam.Size = New System.Drawing.Size(36, 13)
    Me.lbPz_Titstam.TabIndex = 15
    Me.lbPz_Titstam.Text = "Titolo "
    Me.lbPz_Titstam.Tooltip = ""
    Me.lbPz_Titstam.UseMnemonic = False
    '
    'lbPz_Codform
    '
    Me.lbPz_Codform.AutoSize = True
    Me.lbPz_Codform.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_Codform.Location = New System.Drawing.Point(5, 10)
    Me.lbPz_Codform.Name = "lbPz_Codform"
    Me.lbPz_Codform.NTSDbField = ""
    Me.lbPz_Codform.Size = New System.Drawing.Size(97, 13)
    Me.lbPz_Codform.TabIndex = 14
    Me.lbPz_Codform.Text = "Formato di Stampa"
    Me.lbPz_Codform.Tooltip = ""
    Me.lbPz_Codform.UseMnemonic = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbDuplica, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci, Me.tlbAccessi})
    Me.NtsBarManager1.MaxItemId = 13
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAccessi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 1
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
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
    'tlbAccessi
    '
    Me.tlbAccessi.Caption = "Accessi per operatore"
    Me.tlbAccessi.Glyph = CType(resources.GetObject("tlbAccessi.Glyph"), System.Drawing.Image)
    Me.tlbAccessi.Id = 12
    Me.tlbAccessi.Name = "tlbAccessi"
    Me.tlbAccessi.Visible = True
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
    'lbPz_xlsfile
    '
    Me.lbPz_xlsfile.AutoSize = True
    Me.lbPz_xlsfile.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_xlsfile.Location = New System.Drawing.Point(5, 142)
    Me.lbPz_xlsfile.Name = "lbPz_xlsfile"
    Me.lbPz_xlsfile.NTSDbField = ""
    Me.lbPz_xlsfile.Size = New System.Drawing.Size(79, 13)
    Me.lbPz_xlsfile.TabIndex = 26
    Me.lbPz_xlsfile.Text = "Nome file Excel"
    Me.lbPz_xlsfile.Tooltip = ""
    Me.lbPz_xlsfile.UseMnemonic = False
    '
    'edPz_xlsfile
    '
    Me.edPz_xlsfile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_xlsfile.Location = New System.Drawing.Point(108, 139)
    Me.edPz_xlsfile.Name = "edPz_xlsfile"
    Me.edPz_xlsfile.NTSDbField = ""
    Me.edPz_xlsfile.NTSForzaVisZoom = False
    Me.edPz_xlsfile.NTSOldValue = ""
    Me.edPz_xlsfile.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_xlsfile.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_xlsfile.Properties.AutoHeight = False
    Me.edPz_xlsfile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_xlsfile.Properties.MaxLength = 65536
    Me.edPz_xlsfile.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_xlsfile.Size = New System.Drawing.Size(304, 20)
    Me.edPz_xlsfile.TabIndex = 27
    '
    'lbPz_xlsfoglio
    '
    Me.lbPz_xlsfoglio.AutoSize = True
    Me.lbPz_xlsfoglio.BackColor = System.Drawing.Color.Transparent
    Me.lbPz_xlsfoglio.Location = New System.Drawing.Point(435, 142)
    Me.lbPz_xlsfoglio.Name = "lbPz_xlsfoglio"
    Me.lbPz_xlsfoglio.NTSDbField = ""
    Me.lbPz_xlsfoglio.Size = New System.Drawing.Size(125, 13)
    Me.lbPz_xlsfoglio.TabIndex = 28
    Me.lbPz_xlsfoglio.Text = "Nome foglio nel file Excel"
    Me.lbPz_xlsfoglio.Tooltip = ""
    Me.lbPz_xlsfoglio.UseMnemonic = False
    '
    'edPz_xlsfoglio
    '
    Me.edPz_xlsfoglio.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPz_xlsfoglio.Location = New System.Drawing.Point(613, 139)
    Me.edPz_xlsfoglio.Name = "edPz_xlsfoglio"
    Me.edPz_xlsfoglio.NTSDbField = ""
    Me.edPz_xlsfoglio.NTSForzaVisZoom = False
    Me.edPz_xlsfoglio.NTSOldValue = ""
    Me.edPz_xlsfoglio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPz_xlsfoglio.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPz_xlsfoglio.Properties.AutoHeight = False
    Me.edPz_xlsfoglio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPz_xlsfoglio.Properties.MaxLength = 65536
    Me.edPz_xlsfoglio.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPz_xlsfoglio.Size = New System.Drawing.Size(116, 20)
    Me.edPz_xlsfoglio.TabIndex = 29
    '
    'FRM__ISTP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(738, 458)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "FRM__ISTP"
    Me.Text = "IMPOSTAZIONE STAMPE / VISUALIZZAZIONI PARAMETRICHE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.edPz_strOprel2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsFiltri.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    Me.pnPag1.PerformLayout()
    CType(Me.edPz_Val5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2.ResumeLayout(False)
    Me.pnPag2.PerformLayout()
    CType(Me.edPz_Val10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPage3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage3.ResumeLayout(False)
    Me.pnPage3.PerformLayout()
    CType(Me.edPz_Val15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Val11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Op11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipo11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Column11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descam11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edPz_Vals5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Vals1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Ops1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipos5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipos4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipos3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipos2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPz_Tipos1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Columns5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Columns4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Columns3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Columns2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Columns1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Descams1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_strOprel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Sqlsql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_maintab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Titstam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Nomrpt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_Codform.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_xlsfile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPz_xlsfoglio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbAccessi.GlyphPath = (oApp.ChildImageDir & "\selez_cli.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      edPz_Sqlsql.NTSSetParam(oMenu, oApp.Tr(Me, 128662848627812500, "Stringa SQL DB SQL Server"), 0)
      edPz_xlsfile.NTSSetParam(oMenu, oApp.Tr(Me, 130465200296587004, "File Excel"), 255)
      edPz_xlsfoglio.NTSSetParam(oMenu, oApp.Tr(Me, 130465200309535280, "Foglio di lavoro Excel"), 255)
      edPz_maintab.NTSSetParam(oMenu, oApp.Tr(Me, 128306284872883689, "Tabella principale per codice ditta"), 15)
      edPz_Titstam.NTSSetParam(oMenu, oApp.Tr(Me, 128306284873040011, "Titolo "), 40)
      edPz_Nomrpt.NTSSetParam(oMenu, oApp.Tr(Me, 128306284873196333, "Nome File RPT"), 12)
      edPz_Codform.NTSSetParam(oMenu, oApp.Tr(Me, 128306284873352655, "Formato di Stampa"), "0", 4, 1, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Val15.NTSSetParam(oMenu, oApp.Tr(Me, 129077780111376953, "Valore Campo15"), 13)
      edPz_Val14.NTSSetParam(oMenu, oApp.Tr(Me, 129077780123701172, "Valore Campo14"), 13)
      edPz_Val13.NTSSetParam(oMenu, oApp.Tr(Me, 129077780134423828, "Valore Campo13"), 13)
      edPz_Val12.NTSSetParam(oMenu, oApp.Tr(Me, 129077780146904297, "Valore Campo12"), 13)
      edPz_Val11.NTSSetParam(oMenu, oApp.Tr(Me, 129077780159462890, "Valore Campo11"), 13)
      edPz_Val10.NTSSetParam(oMenu, oApp.Tr(Me, 129077780170498047, "Valore Campo0"), 13)
      edPz_Val9.NTSSetParam(oMenu, oApp.Tr(Me, 128306284882731975, "Valore Campo9"), 13)
      edPz_Val8.NTSSetParam(oMenu, oApp.Tr(Me, 128306284882888297, "Valore Campo8"), 13)
      edPz_Val7.NTSSetParam(oMenu, oApp.Tr(Me, 128306284883044619, "Valore Campo7"), 13)
      edPz_Val6.NTSSetParam(oMenu, oApp.Tr(Me, 128306284883200941, "Valore Campo6"), 13)
      edPz_Val5.NTSSetParam(oMenu, oApp.Tr(Me, 128306284876479095, "Valore Campo5"), 13)
      edPz_Val4.NTSSetParam(oMenu, oApp.Tr(Me, 128306284876635417, "Valore Campo4"), 13)
      edPz_Val3.NTSSetParam(oMenu, oApp.Tr(Me, 128306284876791739, "Valore Campo3"), 13)
      edPz_Val2.NTSSetParam(oMenu, oApp.Tr(Me, 128306284876948061, "Valore Campo2"), 13)
      edPz_Val1.NTSSetParam(oMenu, oApp.Tr(Me, 128306284877104383, "Valore Campo1"), 13)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals1.NTSSetParam(oMenu, oApp.Tr(Me, 130449677553124815, "Valore Campo 1 (subqyery)"), 13)
      edPz_Vals2.NTSSetParam(oMenu, oApp.Tr(Me, 130449677825742705, "Valore Campo 2 (subqyery)"), 13)
      edPz_Vals3.NTSSetParam(oMenu, oApp.Tr(Me, 130449677842413267, "Valore Campo 3 (subqyery)"), 13)
      edPz_Vals4.NTSSetParam(oMenu, oApp.Tr(Me, 130449677858986169, "Valore Campo 4 (subqyery)"), 13)
      edPz_Vals5.NTSSetParam(oMenu, oApp.Tr(Me, 130449677875773923, "Valore Campo 5 (subqyery)"), 13)
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op15.NTSSetParam(oApp.Tr(Me, 129077780499697265, "Operatore15"))
      cbPz_Op14.NTSSetParam(oApp.Tr(Me, 129077780484658203, "Operatore14"))
      cbPz_Op13.NTSSetParam(oApp.Tr(Me, 129077780472177734, "Operatore13"))
      cbPz_Op12.NTSSetParam(oApp.Tr(Me, 129077780461777343, "Operatore12"))
      cbPz_Op11.NTSSetParam(oApp.Tr(Me, 129077780446660156, "Operatore11"))
      cbPz_Op10.NTSSetParam(oApp.Tr(Me, 128306284883357263, "Operatore0"))
      cbPz_Op9.NTSSetParam(oApp.Tr(Me, 128306284883513585, "Operatore9"))
      cbPz_Op8.NTSSetParam(oApp.Tr(Me, 128306284883669907, "Operatore8"))
      cbPz_Op7.NTSSetParam(oApp.Tr(Me, 128306284883826229, "Operatore7"))
      cbPz_Op6.NTSSetParam(oApp.Tr(Me, 128306284883982551, "Operatore6"))
      cbPz_Op5.NTSSetParam(oApp.Tr(Me, 128306284877260705, "Operatore5"))
      cbPz_Op4.NTSSetParam(oApp.Tr(Me, 128306284877417027, "Operatore4"))
      cbPz_Op3.NTSSetParam(oApp.Tr(Me, 128306284877573349, "Operatore3"))
      cbPz_Op2.NTSSetParam(oApp.Tr(Me, 128306284877729671, "Operatore2"))
      cbPz_Op1.NTSSetParam(oApp.Tr(Me, 128306284877885993, "Operatore1"))
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.NTSSetParam(oApp.Tr(Me, 130449678384963397, "Operatore 1 (subquery)"))
      cbPz_Ops2.NTSSetParam(oApp.Tr(Me, 130449678400120229, "Operatore 2 (subquery)"))
      cbPz_Ops3.NTSSetParam(oApp.Tr(Me, 130449678413978183, "Operatore 3 (subquery)"))
      cbPz_Ops4.NTSSetParam(oApp.Tr(Me, 130449678427982627, "Operatore 4 (subquery)"))
      cbPz_Ops5.NTSSetParam(oApp.Tr(Me, 130449678442153093, "Operatore 5 (subquery)"))
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipo15.NTSSetParam(oApp.Tr(Me, 129077780790576172, "Tipo Campo15"))
      cbPz_Tipo14.NTSSetParam(oApp.Tr(Me, 129077780781855468, "Tipo Campo14"))
      cbPz_Tipo13.NTSSetParam(oApp.Tr(Me, 129077780773056640, "Tipo Campo13"))
      cbPz_Tipo12.NTSSetParam(oApp.Tr(Me, 129077780762890625, "Tipo Campo12"))
      cbPz_Tipo11.NTSSetParam(oApp.Tr(Me, 129077780751455078, "Tipo Campo11"))
      cbPz_Tipo10.NTSSetParam(oApp.Tr(Me, 128306284884138873, "Tipo Campo0"))
      cbPz_Tipo9.NTSSetParam(oApp.Tr(Me, 128306284884295195, "Tipo Campo9"))
      cbPz_Tipo8.NTSSetParam(oApp.Tr(Me, 128306284884451517, "Tipo Campo8"))
      cbPz_Tipo7.NTSSetParam(oApp.Tr(Me, 128306284884607839, "Tipo Campo7"))
      cbPz_Tipo6.NTSSetParam(oApp.Tr(Me, 128306284884764161, "Tipo Campo6"))
      cbPz_Tipo5.NTSSetParam(oApp.Tr(Me, 128306284878042315, "Tipo Campo5"))
      cbPz_Tipo4.NTSSetParam(oApp.Tr(Me, 128306284878198637, "Tipo Campo4"))
      cbPz_Tipo3.NTSSetParam(oApp.Tr(Me, 128306284878354959, "Tipo Campo3"))
      cbPz_Tipo2.NTSSetParam(oApp.Tr(Me, 128306284878511281, "Tipo Campo2"))
      cbPz_Tipo1.NTSSetParam(oApp.Tr(Me, 128306284878667603, "Tipo Campo1"))
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipos1.NTSSetParam(oApp.Tr(Me, 130449678913333295, "Tipo Campo 1 (subquery)"))
      cbPz_Tipos2.NTSSetParam(oApp.Tr(Me, 130449678927777209, "Tipo Campo 2 (subquery)"))
      cbPz_Tipos3.NTSSetParam(oApp.Tr(Me, 130449678941117565, "Tipo Campo 3 (subquery)"))
      cbPz_Tipos4.NTSSetParam(oApp.Tr(Me, 130449678957963915, "Tipo Campo 4 (subquery)"))
      cbPz_Tipos5.NTSSetParam(oApp.Tr(Me, 130449678972759405, "Tipo Campo 5 (subquery)"))
      '--------------------------------------------------------------------------------------------------------------
      edPz_Column15.NTSSetParam(oMenu, oApp.Tr(Me, 129077780994892578, "Nome Campo15"), 30)
      edPz_Column14.NTSSetParam(oMenu, oApp.Tr(Me, 129077781012968750, "Nome Campo14"), 30)
      edPz_Column13.NTSSetParam(oMenu, oApp.Tr(Me, 129077781026650390, "Nome Campo13"), 30)
      edPz_Column12.NTSSetParam(oMenu, oApp.Tr(Me, 129077781041289062, "Nome Campo12"), 30)
      edPz_Column11.NTSSetParam(oMenu, oApp.Tr(Me, 129077781050332031, "Nome Campo11"), 30)
      edPz_Column10.NTSSetParam(oMenu, oApp.Tr(Me, 129077781059687500, "Nome Campo0"), 30)
      edPz_Column9.NTSSetParam(oMenu, oApp.Tr(Me, 128306284885076805, "Nome Campo9"), 30)
      edPz_Column8.NTSSetParam(oMenu, oApp.Tr(Me, 128306284885233127, "Nome Campo8"), 30)
      edPz_Column7.NTSSetParam(oMenu, oApp.Tr(Me, 128306284885389449, "Nome Campo7"), 30)
      edPz_Column6.NTSSetParam(oMenu, oApp.Tr(Me, 128306284885545771, "Nome Campo6"), 30)
      edPz_Column5.NTSSetParam(oMenu, oApp.Tr(Me, 128306284878823925, "Nome Campo5"), 30)
      edPz_Column4.NTSSetParam(oMenu, oApp.Tr(Me, 128306284878980247, "Nome Campo4"), 30)
      edPz_Column3.NTSSetParam(oMenu, oApp.Tr(Me, 128306284879136569, "Nome Campo3"), 30)
      edPz_Column2.NTSSetParam(oMenu, oApp.Tr(Me, 128306284879292891, "Nome Campo2"), 30)
      edPz_Column1.NTSSetParam(oMenu, oApp.Tr(Me, 128306284879449213, "Nome Campo1"), 30)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Columns1.NTSSetParam(oMenu, oApp.Tr(Me, 130449679385802609, "Nome Campo 1 (subquery)"), 30)
      edPz_Columns2.NTSSetParam(oMenu, oApp.Tr(Me, 130449679399484775, "Nome Campo 2 (subquery)"), 30)
      edPz_Columns3.NTSSetParam(oMenu, oApp.Tr(Me, 130449679413674773, "Nome Campo 3 (subquery)"), 30)
      edPz_Columns4.NTSSetParam(oMenu, oApp.Tr(Me, 130449679427464365, "Nome Campo 4 (subquery)"), 30)
      edPz_Columns5.NTSSetParam(oMenu, oApp.Tr(Me, 130449679441273489, "Nome Campo 5 (subquery)"), 30)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descam15.NTSSetParam(oMenu, oApp.Tr(Me, 129077781228007812, "Descrizione Campo15"), 40)
      edPz_Descam14.NTSSetParam(oMenu, oApp.Tr(Me, 129077781243291015, "Descrizione Campo14"), 40)
      edPz_Descam13.NTSSetParam(oMenu, oApp.Tr(Me, 129077781252812500, "Descrizione Campo13"), 40)
      edPz_Descam12.NTSSetParam(oMenu, oApp.Tr(Me, 129077781262734375, "Descrizione Campo12"), 40)
      edPz_Descam11.NTSSetParam(oMenu, oApp.Tr(Me, 129077781271933593, "Descrizione Campo11"), 40)
      edPz_Descam10.NTSSetParam(oMenu, oApp.Tr(Me, 129077781282167968, "Descrizione Campo0"), 40)
      edPz_Descam9.NTSSetParam(oMenu, oApp.Tr(Me, 128306284886014737, "Descrizione Campo9"), 40)
      edPz_Descam8.NTSSetParam(oMenu, oApp.Tr(Me, 128306284886171059, "Descrizione Campo8"), 40)
      edPz_Descam7.NTSSetParam(oMenu, oApp.Tr(Me, 128306284886327381, "Descrizione Campo7"), 40)
      edPz_Descam6.NTSSetParam(oMenu, oApp.Tr(Me, 128306284886483703, "Descrizione Campo6"), 40)
      edPz_Descam5.NTSSetParam(oMenu, oApp.Tr(Me, 128306284879761857, "Descrizione Campo5"), 40)
      edPz_Descam4.NTSSetParam(oMenu, oApp.Tr(Me, 128306284879918179, "Descrizione Campo4"), 40)
      edPz_Descam3.NTSSetParam(oMenu, oApp.Tr(Me, 128306284880074501, "Descrizione Campo3"), 40)
      edPz_Descam2.NTSSetParam(oMenu, oApp.Tr(Me, 128306284880230823, "Descrizione Campo2"), 40)
      edPz_Descam1.NTSSetParam(oMenu, oApp.Tr(Me, 128306284880387145, "Descrizione Campo1"), 40)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descams1.NTSSetParam(oMenu, oApp.Tr(Me, 130449679802176019, "Descrizione Campo 1 (subquery)"), 40)
      edPz_Descams2.NTSSetParam(oMenu, oApp.Tr(Me, 130449679816180463, "Descrizione Campo 2 (subquery)"), 40)
      edPz_Descams3.NTSSetParam(oMenu, oApp.Tr(Me, 130449679830751335, "Descrizione Campo 3 (subquery)"), 40)
      edPz_Descams4.NTSSetParam(oMenu, oApp.Tr(Me, 130449679847890665, "Descrizione Campo 4 (subquery)"), 40)
      edPz_Descams5.NTSSetParam(oMenu, oApp.Tr(Me, 130449679862627559, "Descrizione Campo 5 (subquery)"), 40)
      '--------------------------------------------------------------------------------------------------------------
      edPz_strOprel.NTSSetParam(oMenu, oApp.Tr(Me, 128898040302352575, "Composizione relazionale filtri ridotta"), 255)
      edPz_strOprel2.NTSSetParam(oMenu, oApp.Tr(Me, 130465186914735627, "Composizione relazionale filtri subquery"), 255)
      '--------------------------------------------------------------------------------------------------------------
      '--- N on faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo
      '--- ma riposizionare il dataset!!!
      '--------------------------------------------------------------------------------------------------------------
      edPz_Codform.NTSForzaVisZoom = False
      edPz_Codform.NTSSetRichiesto()
      edPz_Titstam.NTSSetParamZoom(".")
      edPz_xlsfile.NTSSetParamZoom(".")
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__ISTP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim bOut As Boolean
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOut = Apri()
      bOnLoading = False

      tsFiltri.SelectedTabPageIndex = 0

      GctlSetRoules()

      disabilitaPz_Column()

      EspandiFiltri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo un nuovo formato di stampa
      If Not Salva() Then Return
      oCleIstp.Nuovo()
      dcIstp.MoveLast()
      edPz_Codform.Enabled = True
      edPz_Codform.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim dtrTmp As DataRow
    Try
      If Not Salva() Then Return
      dtrTmp = dsIstp.Tables("PARSTAG").Rows(dcIstp.Position)
      If Not oCleIstp.Duplica(dtrTmp) Then Return
      dcIstp.MoveLast()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
      'cancello il formato di stampa
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128306256431033721, "Cancellare il formato di stampa?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsIstp.Tables("PARSTAG").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcIstp.RemoveAt(dcIstp.Position)
          oCleIstp.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcIstp, Me)
            bRemovBinding = False
            edPz_Codform.Enabled = True
          Else
            edPz_Codform.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcIstp, Me)
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
      'Ripristino il formato di stampa
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128306256614868393, "Ripristinare le modifiche apportate al formato di stampa?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsIstp.Tables("PARSTAG").Rows.Count = 1 And dsIstp.Tables("PARSTAG").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleIstp.Ripristina(dcIstp.Position, dcIstp.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcIstp, Me)
            bRemovBinding = False
            edPz_Codform.Enabled = True
          Else
            edPz_Codform.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcIstp, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmHlpz As FRM__HLPZ = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      frmHlpz = CType(NTSNewFormModal("FRM__HLPZ"), FRM__HLPZ)
      If edPz_Titstam.Focused = True Then
        frmHlpz.Init(oMenu, oParam, DittaCorrente)
        frmHlpz.InitEntity(oCleIstp)
        frmHlpz.ShowDialog()
        If oCleIstp.strHlpzTitstam <> "" Then posizionaAfterZoom(NTSCStr(oCleIstp.nHlpzCodform))
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edPz_xlsfile.Focused Or edPz_xlsfile.Focused Then
        'zoom file di excel
        Dim OpenFileDialog1 As New NTSOpenFileDialog
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.ShowReadOnly = False
        OpenFileDialog1.ShowHelp = False
        OpenFileDialog1.DefaultExt = "xlsx"
        OpenFileDialog1.Title = "Selezione file Excel"
        OpenFileDialog1.Filter = "File Excel (*.xls;*.xlsx)|*.XLS;*.XLSX"
        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.oMenu = oMenu
        If Not OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
          If OpenFileDialog1.FileName <> "" Then
            edPz_xlsfile.NTSTextDB = OpenFileDialog1.FileName
          End If
        End If
      ElseIf edPz_Descam1.Focused Or edPz_Column1.Focused Then
        ApriHlcp(1)
      ElseIf edPz_Descam2.Focused Or edPz_Column2.Focused Then
        ApriHlcp(2)
      ElseIf edPz_Descam3.Focused Or edPz_Column3.Focused Then
        ApriHlcp(3)
      ElseIf edPz_Descam4.Focused Or edPz_Column4.Focused Then
        ApriHlcp(4)
      ElseIf edPz_Descam5.Focused Or edPz_Column5.Focused Then
        ApriHlcp(5)
      ElseIf edPz_Descam6.Focused Or edPz_Column6.Focused Then
        ApriHlcp(6)
      ElseIf edPz_Descam7.Focused Or edPz_Column7.Focused Then
        ApriHlcp(7)
      ElseIf edPz_Descam8.Focused Or edPz_Column8.Focused Then
        ApriHlcp(8)
      ElseIf edPz_Descam9.Focused Or edPz_Column9.Focused Then
        ApriHlcp(9)
      ElseIf edPz_Descam10.Focused Or edPz_Column10.Focused Then
        ApriHlcp(10)
      ElseIf edPz_Descam11.Focused Or edPz_Column11.Focused Then
        ApriHlcp(11)
      ElseIf edPz_Descam12.Focused Or edPz_Column12.Focused Then
        ApriHlcp(12)
      ElseIf edPz_Descam13.Focused Or edPz_Column13.Focused Then
        ApriHlcp(13)
      ElseIf edPz_Descam14.Focused Or edPz_Column14.Focused Then
        ApriHlcp(14)
      ElseIf edPz_Descam15.Focused Or edPz_Column15.Focused Then
        ApriHlcp(15)
      ElseIf edPz_Descams1.Focused Or edPz_Columns1.Focused Then
        ApriHlcp(1)
      ElseIf edPz_Descams2.Focused Or edPz_Columns2.Focused Then
        ApriHlcp(2)
      ElseIf edPz_Descams3.Focused Or edPz_Columns3.Focused Then
        ApriHlcp(3)
      ElseIf edPz_Descams4.Focused Or edPz_Columns4.Focused Then
        ApriHlcp(4)
      ElseIf edPz_Descams5.Focused Or edPz_Columns5.Focused Then
        ApriHlcp(5)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edPz_Val1.Focused And edPz_Descam1.Text <> "" Then
        ApriZoom(1)
      ElseIf edPz_Val2.Focused And edPz_Descam2.Text <> "" Then
        ApriZoom(2)
      ElseIf edPz_Val3.Focused And edPz_Descam3.Text <> "" Then
        ApriZoom(3)
      ElseIf edPz_Val4.Focused And edPz_Descam4.Text <> "" Then
        ApriZoom(4)
      ElseIf edPz_Val5.Focused And edPz_Descam5.Text <> "" Then
        ApriZoom(5)
      ElseIf edPz_Val6.Focused And edPz_Descam6.Text <> "" Then
        ApriZoom(6)
      ElseIf edPz_Val7.Focused And edPz_Descam7.Text <> "" Then
        ApriZoom(7)
      ElseIf edPz_Val8.Focused And edPz_Descam8.Text <> "" Then
        ApriZoom(8)
      ElseIf edPz_Val9.Focused And edPz_Descam9.Text <> "" Then
        ApriZoom(9)
      ElseIf edPz_Val10.Focused And edPz_Descam10.Text <> "" Then
        ApriZoom(10)
      ElseIf edPz_Val11.Focused And edPz_Descam11.Text <> "" Then
        ApriZoom(11)
      ElseIf edPz_Val12.Focused And edPz_Descam12.Text <> "" Then
        ApriZoom(12)
      ElseIf edPz_Val13.Focused And edPz_Descam13.Text <> "" Then
        ApriZoom(13)
      ElseIf edPz_Val14.Focused And edPz_Descam14.Text <> "" Then
        ApriZoom(14)
      ElseIf edPz_Val15.Focused And edPz_Descam15.Text <> "" Then
        ApriZoom(15)
      ElseIf edPz_Vals1.Focused And edPz_Descams1.Text <> "" Then
        ApriZooms(1)
      ElseIf edPz_Vals2.Focused And edPz_Descams2.Text <> "" Then
        ApriZooms(2)
      ElseIf edPz_Vals3.Focused And edPz_Descams3.Text <> "" Then
        ApriZooms(3)
      ElseIf edPz_Vals4.Focused And edPz_Descams4.Text <> "" Then
        ApriZooms(4)
      ElseIf edPz_Vals5.Focused And edPz_Descams5.Text <> "" Then
        ApriZooms(5)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHlpz Is Nothing Then frmHlpz.Dispose()
      frmHlpz = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbAccessi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAccessi.ItemClick
    Dim frmGacc As FRM__GACC = Nothing
    Try
      If Not Salva() Then Return

      If oCleIstp.RecordIsChanged Then Return 'ho detto NO alla domanda di salvare 
      frmGacc = CType(NTSNewFormModal("FRM__GACC"), FRM__GACC)

      frmGacc.Init(oMenu, oCallParams, DittaCorrente)
      frmGacc.InitEntity(oCleIstp, NTSCInt(edPz_Codform.Text))
      frmGacc.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGacc Is Nothing Then frmGacc.Dispose()
      frmGacc = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcIstp.MoveFirst()
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
      dcIstp.MovePrevious()
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
      dcIstp.MoveNext()
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
      dcIstp.MoveLast()
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
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edPz_Nomrpt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Nomrpt.LostFocus
    Try
      If edPz_Nomrpt.Text <> "" Then
        edPz_Nomrpt.Text = edPz_Nomrpt.Text.ToUpper
        If (Len(oCleIstp.Estensione(edPz_Nomrpt.Text)) = 0) And (Len(oCleIstp.NomeFile(edPz_Nomrpt.Text)) <= 8) And (edPz_Nomrpt.Text <> "") Then
          edPz_Nomrpt.Text = edPz_Nomrpt.Text & ".RPT"
        Else
          If (Len(oCleIstp.NomeFile(edPz_Nomrpt.Text)) > 8) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128309554159677403, "Il nome del File non deve superare gli 8 caratteri"))
            If edPz_Nomrpt.Enabled Then
              edPz_Nomrpt.Focus()
            End If
          End If
        End If
        If (edPz_Nomrpt.Text = "*.RPT") Or (edPz_Nomrpt.Text = ".RPT") Then
          edPz_Nomrpt.Text = ""
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_strOprel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_strOprel.TextChanged
    Try
      If Len(edPz_strOprel.Text) > 0 Then EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edPz_Val1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val1.TextChanged
    Try
      edPz_Val1.Text = edPz_Val1.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val2.TextChanged
    Try
      edPz_Val2.Text = edPz_Val2.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val3.TextChanged
    Try
      edPz_Val3.Text = edPz_Val3.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val4.TextChanged
    Try
      edPz_Val4.Text = edPz_Val4.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val5.TextChanged
    Try
      edPz_Val5.Text = edPz_Val5.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val6.TextChanged
    Try
      edPz_Val6.Text = edPz_Val6.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val7.TextChanged
    Try
      edPz_Val7.Text = edPz_Val7.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val8.TextChanged
    Try
      edPz_Val8.Text = edPz_Val8.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val9.TextChanged
    Try
      edPz_Val9.Text = edPz_Val9.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val10.TextChanged
    Try
      edPz_Val10.Text = edPz_Val10.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val11.TextChanged
    Try
      edPz_Val11.Text = edPz_Val11.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val12.TextChanged
    Try
      edPz_Val12.Text = edPz_Val12.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val13.TextChanged
    Try
      edPz_Val13.Text = edPz_Val13.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val14.TextChanged
    Try
      edPz_Val14.Text = edPz_Val14.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Val15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Val15.TextChanged
    Try
      edPz_Val15.Text = edPz_Val15.Text.ToUpper
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edPz_Vals1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Vals1.TextChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals1.Text = edPz_Vals1.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edPz_Vals2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Vals2.TextChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals2.Text = edPz_Vals2.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edPz_Vals3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Vals3.TextChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals3.Text = edPz_Vals3.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edPz_Vals4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Vals4.TextChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals4.Text = edPz_Vals4.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edPz_Vals5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Vals5.TextChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals5.Text = edPz_Vals5.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edPz_Descam1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam1.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam2.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam3.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam4.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam5.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam6.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam7.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam8.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam9.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam10.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam11.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam12.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam13.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam14.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPz_Descam15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPz_Descam15.TextChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbPz_Op1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op1.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op2.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op3.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op4.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op5.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op6.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op7.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op8.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op9.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op10.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op11_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op11.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op12.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op13_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op13.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op14_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op14.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbPz_Op15_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPz_Op15.SelectedIndexChanged
    Try
      EspandiFiltri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdnFiltro1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro1.Click
    Try
      cmdnFiltro(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro2.Click
    Try
      cmdnFiltro(2)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro3.Click
    Try
      cmdnFiltro(3)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro4.Click
    Try
      cmdnFiltro(4)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro5.Click
    Try
      cmdnFiltro(5)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro6.Click
    Try
      cmdnFiltro(6)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro7.Click
    Try
      cmdnFiltro(7)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro8.Click
    Try
      cmdnFiltro(8)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro9.Click
    Try
      cmdnFiltro(9)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro10.Click
    Try
      cmdnFiltro(10)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro11.Click
    Try
      cmdnFiltro(11)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro12.Click
    Try
      cmdnFiltro(12)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro13.Click
    Try
      cmdnFiltro(13)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro14.Click
    Try
      cmdnFiltro(14)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdnFiltro15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltro15.Click
    Try
      cmdnFiltro(15)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdnFiltros1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltros1.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdnFiltros(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdnFiltros2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltros2.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdnFiltros(2)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdnFiltros3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltros3.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdnFiltros(3)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdnFiltros4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltros4.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdnFiltros(4)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdnFiltros5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdnFiltros5.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdnFiltros(5)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAprip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAprip.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        edPz_strOprel2.Text = edPz_strOprel2.Text & " ("
        If edPz_strOprel2.Enabled = True Then edPz_strOprel2.Focus()
      Else
        edPz_strOprel.Text = edPz_strOprel.Text & " ("
        If edPz_strOprel.Enabled = True Then edPz_strOprel.Focus()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdChiudip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChiudip.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        edPz_strOprel2.Text = edPz_strOprel2.Text & " )"
        If edPz_strOprel2.Enabled Then edPz_strOprel2.Focus()
      Else
        edPz_strOprel.Text = edPz_strOprel.Text & " )"
        If edPz_strOprel.Enabled Then edPz_strOprel.Focus()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdOpAnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpAnd.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        edPz_strOprel2.Text = edPz_strOprel2.Text & " AND "
      Else
        edPz_strOprel.Text = edPz_strOprel.Text & " AND "
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdOpOr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpOr.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        edPz_strOprel2.Text = edPz_strOprel2.Text & " OR "
      Else
        edPz_strOprel.Text = edPz_strOprel.Text & " OR "
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdOpNot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpNot.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        edPz_strOprel2.Text = edPz_strOprel2.Text & " NOT "
      Else
        edPz_strOprel.Text = edPz_strOprel.Text & " NOT "
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      NTSFormClearDataBinding(Me)
      If Not oCleIstp.Apri(DittaCorrente, dsIstp) Then Me.Close()
      dcIstp.DataSource = dsIstp.Tables("PARSTAG")
      dsIstp.AcceptChanges()
      If dsIstp.Tables("PARSTAG").Rows.Count > 0 Then
        edPz_Codform.Enabled = False
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      If dsIstp.Tables("PARSTAG").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcIstp.ResetBindings(False)
      dcIstp.MoveFirst()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ApriHlcp(ByVal i As Integer)
    Dim oParam As New CLE__CLDP
    Dim frmHlcp As FRM__HLCP = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      frmHlcp = CType(NTSNewFormModal("FRM__HLCP"), FRM__HLCP)
      oCleIstp.strParstagDescam = ""
      oCleIstp.strParstagColumn = ""
      oCleIstp.nParstagTipo = 0
      '--------------------------------------------------------------------------------------------------------------
      frmHlcp.Init(oMenu, oParam, DittaCorrente)
      frmHlcp.InitEntity(oCleIstp)
      frmHlcp.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If tsFiltri.SelectedTabPageIndex = 3 Then
        oCleIstp.SalvaHlcpS(i, edPz_Codform.Text)
      Else
        oCleIstp.SalvaHlcp(i, edPz_Codform.Text)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmHlcp Is Nothing Then frmHlcp.Dispose()
      frmHlcp = Nothing
    End Try
  End Sub
  Public Overridable Sub ApriZoom(ByVal i As Integer)
    Dim nPos As Integer
    Dim strTabella As String
    Dim strCampo As String
    Dim oParam As New CLE__PATB

    Try

      Select Case i
        Case 1
          If (edPz_Val1.Focused) And edPz_Descam1.Text <> "" Then
            nPos = InStr(edPz_Column1.Text, ".") - 1
            strTabella = Mid(edPz_Column1.Text, 1, nPos)
            strCampo = edPz_Column1.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(1, strTabella, strCampo)
            End If
          End If
        Case 2
          If (edPz_Val2.Focused) And edPz_Descam2.Text <> "" Then
            nPos = InStr(edPz_Column2.Text, ".") - 1
            strTabella = Mid(edPz_Column2.Text, 1, nPos)
            strCampo = edPz_Column2.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(2, strTabella, strCampo)
            End If
          End If
        Case 3
          If (edPz_Val3.Focused) And edPz_Descam3.Text <> "" Then
            nPos = InStr(edPz_Column3.Text, ".") - 1
            strTabella = Mid(edPz_Column3.Text, 1, nPos)
            strCampo = edPz_Column3.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(3, strTabella, strCampo)
            End If
          End If
        Case 4
          If (edPz_Val4.Focused) And edPz_Descam4.Text <> "" Then
            nPos = InStr(edPz_Column4.Text, ".") - 1
            strTabella = Mid(edPz_Column4.Text, 1, nPos)
            strCampo = edPz_Column4.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(4, strTabella, strCampo)
            End If
          End If
        Case 5
          If (edPz_Val5.Focused) And edPz_Descam5.Text <> "" Then
            nPos = InStr(edPz_Column5.Text, ".") - 1
            strTabella = Mid(edPz_Column5.Text, 1, nPos)
            strCampo = edPz_Column5.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(5, strTabella, strCampo)
            End If
          End If
        Case 6
          If (edPz_Val6.Focused) And edPz_Descam6.Text <> "" Then
            nPos = InStr(edPz_Column6.Text, ".") - 1
            strTabella = Mid(edPz_Column6.Text, 1, nPos)
            strCampo = edPz_Column6.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(6, strTabella, strCampo)
            End If
          End If
        Case 7
          If (edPz_Val7.Focused) And edPz_Descam7.Text <> "" Then
            nPos = InStr(edPz_Column7.Text, ".") - 1
            strTabella = Mid(edPz_Column7.Text, 1, nPos)
            strCampo = edPz_Column7.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(7, strTabella, strCampo)
            End If
          End If
        Case 8
          If (edPz_Val8.Focused) And edPz_Descam8.Text <> "" Then
            nPos = InStr(edPz_Column8.Text, ".") - 1
            strTabella = Mid(edPz_Column8.Text, 1, nPos)
            strCampo = edPz_Column8.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(8, strTabella, strCampo)
            End If
          End If
        Case 9
          If (edPz_Val9.Focused) And edPz_Descam9.Text <> "" Then
            nPos = InStr(edPz_Column9.Text, ".") - 1
            strTabella = Mid(edPz_Column9.Text, 1, nPos)
            strCampo = edPz_Column9.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(9, strTabella, strCampo)
            End If
          End If
        Case 10
          If (edPz_Val10.Focused) And edPz_Descam10.Text <> "" Then
            nPos = InStr(edPz_Column10.Text, ".") - 1
            strTabella = Mid(edPz_Column10.Text, 1, nPos)
            strCampo = edPz_Column10.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(10, strTabella, strCampo)
            End If
          End If
        Case 11
          If (edPz_Val11.Focused) And edPz_Descam11.Text <> "" Then
            nPos = InStr(edPz_Column11.Text, ".") - 1
            strTabella = Mid(edPz_Column11.Text, 1, nPos)
            strCampo = edPz_Column11.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(11, strTabella, strCampo)
            End If
          End If
        Case 12
          If (edPz_Val12.Focused) And edPz_Descam12.Text <> "" Then
            nPos = InStr(edPz_Column12.Text, ".") - 1
            strTabella = Mid(edPz_Column12.Text, 1, nPos)
            strCampo = edPz_Column12.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(12, strTabella, strCampo)
            End If
          End If
        Case 13
          If (edPz_Val13.Focused) And edPz_Descam13.Text <> "" Then
            nPos = InStr(edPz_Column13.Text, ".") - 1
            strTabella = Mid(edPz_Column13.Text, 1, nPos)
            strCampo = edPz_Column13.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(13, strTabella, strCampo)
            End If
          End If
        Case 14
          If (edPz_Val14.Focused) And edPz_Descam14.Text <> "" Then
            nPos = InStr(edPz_Column14.Text, ".") - 1
            strTabella = Mid(edPz_Column14.Text, 1, nPos)
            strCampo = edPz_Column14.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(14, strTabella, strCampo)
            End If
          End If
        Case 15
          If (edPz_Val15.Focused) And edPz_Descam15.Text <> "" Then
            nPos = InStr(edPz_Column15.Text, ".") - 1
            strTabella = Mid(edPz_Column15.Text, 1, nPos)
            strCampo = edPz_Column15.Text
            If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
              NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
              If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)
            Else
              ApriZoomTabella(15, strTabella, strCampo)
            End If
          End If
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ApriZooms(ByVal i As Integer)
    Dim nPos As Integer = 0
    Dim strTabella As String = ""
    Dim strCampo As String = ""
    Dim oParam As New CLE__PATB

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1
          If (edPz_Vals1.Focused = True) And (edPz_Descams1.Text <> "") Then
            nPos = InStr(edPz_Columns1.Text, ".") - 1
            strTabella = Mid(edPz_Columns1.Text, 1, nPos)
            strCampo = edPz_Columns1.Text
          End If
        Case 2
          If (edPz_Vals2.Focused = True) And (edPz_Descams2.Text <> "") Then
            nPos = InStr(edPz_Columns2.Text, ".") - 1
            strTabella = Mid(edPz_Columns2.Text, 1, nPos)
            strCampo = edPz_Columns2.Text
          End If
        Case 3
          If (edPz_Vals3.Focused = True) And (edPz_Descams3.Text <> "") Then
            nPos = InStr(edPz_Columns3.Text, ".") - 1
            strTabella = Mid(edPz_Columns3.Text, 1, nPos)
            strCampo = edPz_Columns3.Text
          End If
        Case 4
          If (edPz_Vals4.Focused = True) And (edPz_Descams4.Text <> "") Then
            nPos = InStr(edPz_Columns4.Text, ".") - 1
            strTabella = Mid(edPz_Columns4.Text, 1, nPos)
            strCampo = edPz_Columns4.Text
          End If
        Case 5
          If (edPz_Vals5.Focused = True) And (edPz_Descams5.Text <> "") Then
            nPos = InStr(edPz_Columns5.Text, ".") - 1
            strTabella = Mid(edPz_Columns5.Text, 1, nPos)
            strCampo = edPz_Columns5.Text
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      nPos = InStr(edPz_Columns1.Text, ".") - 1
      strTabella = Mid(edPz_Columns1.Text, 1, nPos)
      strCampo = edPz_Columns1.Text
      If Microsoft.VisualBasic.Left(strCampo, 3).ToUpper = "TAB" And Mid(strCampo, 8, 7).ToUpper = ".TB_COD" And Mid(strCampo, 4, 4).ToUpper = Mid(strCampo, 15, 4).ToUpper Then
        NTSZOOM.ZoomStrIn("ZOOMTAB" & Mid(strCampo, 4, 4).ToUpper, DittaCorrente, oParam)
        If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
      Else
        ApriZoomTabellaS(1, strTabella, strCampo)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub ApriZoomTabella(ByVal i As Integer, ByVal strTabella As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim oParamHlvl As New CLE__CLDP
    Dim frmHlvl As FRM__HLVL = Nothing
    Dim strValore As String
    Try
      frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
      Select Case strCampo.ToUpper

        Case "ANAGRA.AN_CONTO", "BUDGET.BU_CONTO", "CODARFO.CAF_CONTO", "COMMESS.CO_CONTO", "DESTDIV.DD_CONTO", _
             "KEYMAG.KM_CONTO", "KEYORD.KO_CONTO", "KEYPRB.KM_CONTO", "LISTINI.LC_CONTO", "LISTSEL.LSE_CONTO", _
             "MOVIVA.MI_CONTO", "ORDLIST.OL_CONTO", "PERPROV.PER_CONTO", "PRINOT.PN_CONTO", _
             "PROBUDGET.PB_CONTO", "PROVVIG.PV_CONTO", "SCADEN.SC_CONTO", "SCONTI.SO_CONTO", "TESTMAG.TM_CONTO", _
             "TESTORD.TD_CONTO", "TESTPRB.TM_CONTO", "ALLOLE.AO_CODICE", "MOVIVA.MI_CONTOCF", "PRINOT.PN_CONTOCF", _
             "TESTMAG.TM_CONTO2", "TESTPRB.TM_CONTO2"

          NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "PRIANA.PA_CONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGCA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_FORN", "ARTICO.AR_FORN2", "TABCAGE.TB_CODFORN", "TABMAGA.TB_MAGCONTO"

          NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODMARC"

          NTSZOOM.ZoomStrIn("ZOOMTABMARC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVIVA.MI_CONTROP", "PRINOT.PN_CONTROP", "SCADEN.SC_CONTROP", "TABBANC.TB_CONBANC", _
             "TABCAUC.TB_CONTIVA", "TABCAUC.TB_CD_1", "TABCAUC.TB_CD_2", "TABCAUC.TB_CD_3", "TABCAUC.TB_CD_4", _
             "TABCAUC.TB_CA_1", "TABCAUC.TB_CA_2", "TABCAUC.TB_CA_3", "TABCAUC.TB_CA_4", _
             "TABPEAC.TB_ACONCAS", "TABPEAC.TB_ACONABAT", "TABPEAC.TB_ACONABPA", "TABPEAC.TB_ACONOMAG", _
             "TABPEAC.TB_ACONRIBO", "TABPEAC.TB_ACONRIIN", "TABPEAC.TB_ACONRIAC", "TABPEAC.TB_ACONCAUZ", _
             "TABPECG.TB_CONEFF", "TABPECG.TB_CONABAT", "TABPECG.TB_CONABPA", "TABPECG.TB_CONDCAT", _
             "TABPECG.TB_CONDCPA", "TABPEVE.TB_CONCAS", "TABPEVE.TB_CONABAT", "TABPEVE.TB_CONABPA", _
             "TABPEVE.TB_CONOMAG", "TABPEVE.TB_CONRIBO", "TABPEVE.TB_CONRIIN", "TABPEVE.TB_CONRIAC", _
             "TABPEVE.TB_CONCAUZ"

          NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVMAG.MM_CONTOCONTR", "MOVORD.MO_CONTOCONTR", "MOVPRB.MM_CONTOCONTR"

          NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODART", "ARTDEF.AD_CODART", "ARTDEFX.ADX_CODART", "ARTLIF.AL_CODART", _
             "ARTPRO.AP_CODART", "ARTPROX.APX_CODART", "ARTVAL.AX_CODART", "BARCODE.BC_CODART", _
             "BUDGETD.BUD_CODART", "CODARFO.CAF_CODART", "KEYMAG.KM_CODART", "KEYORD.KO_CODART", _
             "KEYPRB.KM_CODART", "LISTINI.LC_CODART", "MOVMAG.MM_CODART", "MOVORD.MO_CODART", _
             "MOVPRB.MM_CODART", "ORDLIST.OL_CODART", "PERPROV.PER_CODART", "PRIANA.PA_CODART", _
             "PROBUDGETD.PBD_CODART", "SCONTI.SO_CODART", "ZZAGAD.ZZ_CODART", "ZZXAGAD.ZZX_CODART", _
             "MOVOFF.MO_CODART"

          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_AGENTE", "ANAGRA.AN_AGENTE2", "PERPROV.PER_CODCAGE", "PROVVIG.PV_CODAGE", _
             "SCADEN.SC_CODCAGE", "TESTMAG.TM_CODAGE", "TESTMAG.TM_CODAGE2", "TESTORD.TD_CODAGEN", _
             "TESTORD.TD_CODAGEN2", "TESTPRB.TM_CODAGEN", "TESTPRB.TM_CODAGEN2"

          NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CATEG"

          NTSZOOM.ZoomStrIn("ZOOMTABCATE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODESE", "ARTICO.AR_CODIVA", "MOVIVA.MI_CODIVA", "MOVMAG.MM_CODIVA", _
             "MOVORD.MO_CODIVA", "MOVPRB.MM_CODIVA", "ORDLIST.OL_CODIVA", "PRINOT.PN_CODIVA", _
             "TABENAS.TB_CODIVFT", "TABPRIV.TB_PCODIVA", _
             "TESTMAG.TM_CODESE", "TESTMAG.TM_CODIVA_1", "TESTMAG.TM_CODIVA_2", "TESTMAG.TM_CODIVA_3", _
             "TESTMAG.TM_CODIVA_4", "TESTMAG.TM_CODIVA_5", "TESTMAG.TM_CODIVA_6", "TESTMAG.TM_CODIVA_7", _
             "TESTMAG.TM_CODIVA_8", "TESTORD.TD_CODESE", _
             "TESTPRB.TM_CODESE", "TESTPRB.TM_CODIVA_1", "TESTPRB.TM_CODIVA_2", "TESTPRB.TM_CODIVA_3", _
             "TESTPRB.TM_CODIVA_4", "TESTPRB.TM_CODIVA_5", "TESTPRB.TM_CODIVA_6", "TESTPRB.TM_CODIVA_7", _
             "TESTPRB.TM_CODIVA_8"

          NTSZOOM.ZoomStrIn("ZOOMTABCIVA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODMAST", "PRIANA.PA_MASTRO", _
             "TABANAZ.TB_MASCLI_1", "TABANAZ.TB_MASCLI_2", "TABANAZ.TB_MASCLI_3", "TABANAZ.TB_MASCLI_4", _
             "TABANAZ.TB_MASCLI_5", "TABANAZ.TB_MASCLI_6", "TABANAZ.TB_MASCLI_7", "TABANAZ.TB_MASCLI_8", _
             "TABANAZ.TB_MASCLI_9", "TABANAZ.TB_MASCLI_10", "TABANAZ.TB_MASFOR_1", "TABANAZ.TB_MASFOR_2", _
             "TABANAZ.TB_MASFOR_3", "TABANAZ.TB_MASFOR_4", "TABANAZ.TB_MASFOR_5", "TABANAZ.TB_MASFOR_6", _
             "TABANAZ.TB_MASFOR_7", "TABANAZ.TB_MASFOR_8", "TABANAZ.TB_MASFOR_9", "TABANAZ.TB_MASFOR_10"

          NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
             "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA"

          NTSZOOM.ZoomStrIn("ZOOMTABPAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_PORTO", "TESTMAG.TM_PORTO", "TESTORD.TD_PORTO", "TESTPRB.TM_PORTO"

          NTSZOOM.ZoomStrIn("ZOOMTABPORT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_VALUTA", "ARTVAL.AX_CODVALU", "CAMBI.WXCODVALU", "LISTINI.LC_CODVALU", _
             "MOVIVA.MI_CODVALU", "ORDLIST.OL_CODVALU", "PRIANA.PA_CODVALU", "PRINOT.PNCODVALU", _
             "SCADEN.SC_CODVALU", "TESTMAG.TM_VALUTA", "TESTORD.TD_VALUTA", "TESTPRB.TM_VALUTA"

          NTSZOOM.ZoomStrIn("ZOOMTABVALU", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_VETT", "TESTMAG.TM_VETTOR", "TESTORD.TD_VETTOR", "TESTPRB.TM_VETTOR"

          NTSZOOM.ZoomStrIn("ZOOMTABVETT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ANAGRA.AN_ZONA", "DESTDIV.DD_CODZONA"

          NTSZOOM.ZoomStrIn("ZOOMTABZONE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "KEYMAG.KM_MAGAZ", "KEYORD.KO_MAGAZ", "KEYPRB.KM_MAGAZ", "ARTPRO.AP_MAGAZ", _
             "MOVMAG.MM_MAGAZ", "MOVMAG.MM_MAGAZ2", "MOVMAG.MM_MAGDISIMP", "MOVMAG.MM_MAGDISIMP2", _
             "MOVORD.MO_MAGAZ", "MOVORD.MO_MAGAZ2", _
             "MOVPRB.MM_MAGAZ", "MOVPRB.MM_MAGAZ2", "MOVPRB.MM_MAGDISIMP", "MOVPRB.MM_MAGDISIMP2", _
             "ORDLIST.OL_MAGAZ", "ORDLIST.OL_MAGAZ2", "TABTPBF.TB_TMAGAZZ", "TABTPBF.TB_TMAGAZZ2", _
             "TESTMAG.TM_MAGAZ", "TESTMAG.TM_MAGAZ2", "TESTORD.TD_MAGAZ", "TESTORD.TD_MAGAZ2", _
             "TESTPRB.TM_MAGAZ", "ARTDEF.AD_MAGAZ", "TESTPRB.TM_MAGAZ2"

          NTSZOOM.ZoomStrIn("ZOOMTABMAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CODPDON"

          NTSZOOM.ZoomStrIn("ZOOMTABPDON", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_CONTROA", "ARTICO.AR_CONTROP", "MOVMAG.MM_CONTROP", "MOVORD.MO_CONTROP", _
             "MOVPRB.MM_CONTROP", "ORDLIST.OL_CONTROP", "TABCOPE.TB_OCODCONTR", "TABTPBF.TB_TCONTRO", _
             "TESTMAG.TM_CONTROP", "TESTORD.TD_CONTROP", "TESTPRB.TM_CONTROP"

          NTSZOOM.ZoomStrIn("ZOOMTABCOVE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_FAMPROD", "BUDGET.BU_CODCFAM", "BUDGETD.BUD_CODCFAM", "BUDGETDMEN.BUDM_CODCFAM", _
             "BUDGETMEN.BUM_CODCFAM", "MOVMAG.MM_CODCFAM", "MOVORD.MO_CODCFAM", "MOVPRB.MM_CODCFAM", _
             "ORDLIST.OL_CODCFAM", "PRIANA.PA_CODCFAM"

          NTSZOOM.ZoomStrIn("ZOOMTABCFAM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_GRUPPO", "TABSGME.TB_CODGRUPM"

          NTSZOOM.ZoomStrIn("ZOOMTABGMER", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "ARTICO.AR_SOTGRU"

          NTSZOOM.ZoomStrIn("ZOOMTABSGME", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "BUDGET.BU_CODCENA", "BUDGETD.BUD_CODCENA", "BUDGETDMEN.BUDM_CODCENA", "BUDGETMEN.BUM_CODCENA", _
             "MOVMAG.MM_CODCENA", "MOVORD.MO_CODCENA", "MOVPRB.MM_CODCENA", "ORDLIST.OL_CODCENA", _
             "PRIANA.PA_CODCENA", "TABCOPE.TB_OCODCENA", "TABTPBF.TB_VCODCEN", "TESTMAG.TM_CODCENA", _
             "TESTORD.TD_CODCENA", "TESTPRB.TM_CODCENA"

          NTSZOOM.ZoomStrIn("ZOOMTABCENA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "PRIANA.PA_CODLAVO", "PROBUDGETD.PBD_CODLAVO", "TABCOPE.TB_OCODLAVO", "BUDGETD.BUD_CODLAVO"

          NTSZOOM.ZoomStrIn("ZOOMTABLAVO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "CONDBAN.CB_CAUSALE", "PRINOT.PN_CAUSALE", "SCADEN.SC_CAUSALE"

          NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "CONDBAN.CB_CODBANC", "SCADEN.SC_CODBANC", "TASSI.TX_CODBANC"

          NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "LISTINI.LC_CODTPRO", "PERPROV.PER_CODTPRO"

          NTSZOOM.ZoomStrIn("ZOOMTABTPRO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "MOVMAG.MM_CAUSALE", "MOVMAG.MM_CAUSALE2", "MOVPRB.MM_CAUSALE", "MOVPRB.MM_CAUSALE2", _
             "TABTPBF.TB_TCAUMAG", "TABTPBF.TB_TCAUTRA", "TESTMAG.TMCAUSALE", "TESTMAG.CAUSTRA", _
             "TESTORD.TD_CAMBIO", "TESTPRB.TM_CAUSALE", "TESTPRB.TM_CAUSTRA"

          oParam.strCodPdc = "*"
          oParam.nEscomp = -1
          NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "PRIANA.PA_CODCOPE"

          NTSZOOM.ZoomStrIn("ZOOMTABCOPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TABCAGE.TB_CODTBEN"

          NTSZOOM.ZoomStrIn("ZOOMTABENAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TABMAST.TB_CLASMAST"

          NTSZOOM.ZoomStrIn("ZOOMTABCLAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODASPE", "TESTORD.TD_CODASPE", "TESTPRB.TM_CODASPE"

          NTSZOOM.ZoomStrIn("ZOOMTABASPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODNTRA", "TESTPRB.TM_CODNTRA"

          NTSZOOM.ZoomStrIn("ZOOMTABNTRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODPORT", "TESTPRB.TM_CODPORT"

          NTSZOOM.ZoomStrIn("ZOOMTABMPOR", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_CODRSTA", "TESTPRB.TM_CODRSTA"

          NTSZOOM.ZoomStrIn("ZOOMTABRSTA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"

          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case "TESTOFF.TD_CODLEAD", "LEADS.LE_CODLEAD"

          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Val(i)

        Case Else

          oCleIstp.strHlvlTabella = strTabella
          oCleIstp.strHlvlCampo = strCampo

          frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
          frmHlvl.InitEntity(oCleIstp)
          frmHlvl.ShowDialog()

          strValore = oCleIstp.strHlvlValore
          If strValore <> "" Then
            oCleIstp.SalvaValore(i, edPz_Codform.Text, strValore)
          End If
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHlvl Is Nothing Then frmHlvl.Dispose()
      frmHlvl = Nothing
    End Try
  End Sub
  Public Overridable Sub ApriZoomTabellaS(ByVal i As Integer, ByVal strTabella As String, ByVal strCampo As String)
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim oParamHlvl As New CLE__CLDP
    Dim frmHlvl As FRM__HLVL = Nothing
    Dim strValore As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      frmHlvl = CType(NTSNewFormModal("FRM__HLVL"), FRM__HLVL)
      '--------------------------------------------------------------------------------------------------------------
      Select Case strCampo.ToUpper
        Case "ANAGRA.AN_CONTO", "BUDGET.BU_CONTO", "CODARFO.CAF_CONTO", "COMMESS.CO_CONTO", "DESTDIV.DD_CONTO", _
             "KEYMAG.KM_CONTO", "KEYORD.KO_CONTO", "KEYPRB.KM_CONTO", "LISTINI.LC_CONTO", "LISTSEL.LSE_CONTO", _
             "MOVIVA.MI_CONTO", "ORDLIST.OL_CONTO", "PERPROV.PER_CONTO", "PRINOT.PN_CONTO", _
             "PROBUDGET.PB_CONTO", "PROVVIG.PV_CONTO", "SCADEN.SC_CONTO", "SCONTI.SO_CONTO", "TESTMAG.TM_CONTO", _
             "TESTORD.TD_CONTO", "TESTPRB.TM_CONTO", "ALLOLE.AO_CODICE", "MOVIVA.MI_CONTOCF", "PRINOT.PN_CONTOCF", _
             "TESTMAG.TM_CONTO2", "TESTPRB.TM_CONTO2"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Val(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGCA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_FORN", "ARTICO.AR_FORN2", "TABCAGE.TB_CODFORN", "TABMAGA.TB_MAGCONTO"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODMARC"
          NTSZOOM.ZoomStrIn("ZOOMTABMARC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVIVA.MI_CONTROP", "PRINOT.PN_CONTROP", "SCADEN.SC_CONTROP", "TABBANC.TB_CONBANC", _
             "TABCAUC.TB_CONTIVA", "TABCAUC.TB_CD_1", "TABCAUC.TB_CD_2", "TABCAUC.TB_CD_3", "TABCAUC.TB_CD_4", _
             "TABCAUC.TB_CA_1", "TABCAUC.TB_CA_2", "TABCAUC.TB_CA_3", "TABCAUC.TB_CA_4", _
             "TABPEAC.TB_ACONCAS", "TABPEAC.TB_ACONABAT", "TABPEAC.TB_ACONABPA", "TABPEAC.TB_ACONOMAG", _
             "TABPEAC.TB_ACONRIBO", "TABPEAC.TB_ACONRIIN", "TABPEAC.TB_ACONRIAC", "TABPEAC.TB_ACONCAUZ", _
             "TABPECG.TB_CONEFF", "TABPECG.TB_CONABAT", "TABPECG.TB_CONABPA", "TABPECG.TB_CONDCAT", _
             "TABPECG.TB_CONDCPA", "TABPEVE.TB_CONCAS", "TABPEVE.TB_CONABAT", "TABPEVE.TB_CONABPA", _
             "TABPEVE.TB_CONOMAG", "TABPEVE.TB_CONRIBO", "TABPEVE.TB_CONRIIN", "TABPEVE.TB_CONRIAC", _
             "TABPEVE.TB_CONCAUZ"
          NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVMAG.MM_CONTOCONTR", "MOVORD.MO_CONTOCONTR", "MOVPRB.MM_CONTOCONTR"
          NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODART", "ARTDEF.AD_CODART", "ARTDEFX.ADX_CODART", "ARTLIF.AL_CODART", _
             "ARTPRO.AP_CODART", "ARTPROX.APX_CODART", "ARTVAL.AX_CODART", "BARCODE.BC_CODART", _
             "BUDGETD.BUD_CODART", "CODARFO.CAF_CODART", "KEYMAG.KM_CODART", "KEYORD.KO_CODART", _
             "KEYPRB.KM_CODART", "LISTINI.LC_CODART", "MOVMAG.MM_CODART", "MOVORD.MO_CODART", _
             "MOVPRB.MM_CODART", "ORDLIST.OL_CODART", "PERPROV.PER_CODART", "PRIANA.PA_CODART", _
             "PROBUDGETD.PBD_CODART", "SCONTI.SO_CODART", "ZZAGAD.ZZ_CODART", "ZZXAGAD.ZZX_CODART", _
             "MOVOFF.MO_CODART"
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_AGENTE", "ANAGRA.AN_AGENTE2", "PERPROV.PER_CODCAGE", "PROVVIG.PV_CODAGE", _
             "SCADEN.SC_CODCAGE", "TESTMAG.TM_CODAGE", "TESTMAG.TM_CODAGE2", "TESTORD.TD_CODAGEN", _
             "TESTORD.TD_CODAGEN2", "TESTPRB.TM_CODAGEN", "TESTPRB.TM_CODAGEN2"
          NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CATEG"
          NTSZOOM.ZoomStrIn("ZOOMTABCATE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODESE", "ARTICO.AR_CODIVA", "MOVIVA.MI_CODIVA", "MOVMAG.MM_CODIVA", _
             "MOVORD.MO_CODIVA", "MOVPRB.MM_CODIVA", "ORDLIST.OL_CODIVA", "PRINOT.PN_CODIVA", _
             "TABENAS.TB_CODIVFT", "TABPRIV.TB_PCODIVA", _
             "TESTMAG.TM_CODESE", "TESTMAG.TM_CODIVA_1", "TESTMAG.TM_CODIVA_2", "TESTMAG.TM_CODIVA_3", _
             "TESTMAG.TM_CODIVA_4", "TESTMAG.TM_CODIVA_5", "TESTMAG.TM_CODIVA_6", "TESTMAG.TM_CODIVA_7", _
             "TESTMAG.TM_CODIVA_8", "TESTORD.TD_CODESE", _
             "TESTPRB.TM_CODESE", "TESTPRB.TM_CODIVA_1", "TESTPRB.TM_CODIVA_2", "TESTPRB.TM_CODIVA_3", _
             "TESTPRB.TM_CODIVA_4", "TESTPRB.TM_CODIVA_5", "TESTPRB.TM_CODIVA_6", "TESTPRB.TM_CODIVA_7", _
             "TESTPRB.TM_CODIVA_8"
          NTSZOOM.ZoomStrIn("ZOOMTABCIVA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODMAST", "PRIANA.PA_MASTRO", _
             "TABANAZ.TB_MASCLI_1", "TABANAZ.TB_MASCLI_2", "TABANAZ.TB_MASCLI_3", "TABANAZ.TB_MASCLI_4", _
             "TABANAZ.TB_MASCLI_5", "TABANAZ.TB_MASCLI_6", "TABANAZ.TB_MASCLI_7", "TABANAZ.TB_MASCLI_8", _
             "TABANAZ.TB_MASCLI_9", "TABANAZ.TB_MASCLI_10", "TABANAZ.TB_MASFOR_1", "TABANAZ.TB_MASFOR_2", _
             "TABANAZ.TB_MASFOR_3", "TABANAZ.TB_MASFOR_4", "TABANAZ.TB_MASFOR_5", "TABANAZ.TB_MASFOR_6", _
             "TABANAZ.TB_MASFOR_7", "TABANAZ.TB_MASFOR_8", "TABANAZ.TB_MASFOR_9", "TABANAZ.TB_MASFOR_10"
          NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
             "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA"
          NTSZOOM.ZoomStrIn("ZOOMTABPAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_PORTO", "TESTMAG.TM_PORTO", "TESTORD.TD_PORTO", "TESTPRB.TM_PORTO"
          NTSZOOM.ZoomStrIn("ZOOMTABPORT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_VALUTA", "ARTVAL.AX_CODVALU", "CAMBI.WXCODVALU", "LISTINI.LC_CODVALU", _
             "MOVIVA.MI_CODVALU", "ORDLIST.OL_CODVALU", "PRIANA.PA_CODVALU", "PRINOT.PNCODVALU", _
             "SCADEN.SC_CODVALU", "TESTMAG.TM_VALUTA", "TESTORD.TD_VALUTA", "TESTPRB.TM_VALUTA"
          NTSZOOM.ZoomStrIn("ZOOMTABVALU", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_VETT", "TESTMAG.TM_VETTOR", "TESTORD.TD_VETTOR", "TESTPRB.TM_VETTOR"
          NTSZOOM.ZoomStrIn("ZOOMTABVETT", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ANAGRA.AN_ZONA", "DESTDIV.DD_CODZONA"
          NTSZOOM.ZoomStrIn("ZOOMTABZONE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "KEYMAG.KM_MAGAZ", "KEYORD.KO_MAGAZ", "KEYPRB.KM_MAGAZ", "ARTPRO.AP_MAGAZ", _
             "MOVMAG.MM_MAGAZ", "MOVMAG.MM_MAGAZ2", "MOVMAG.MM_MAGDISIMP", "MOVMAG.MM_MAGDISIMP2", _
             "MOVORD.MO_MAGAZ", "MOVORD.MO_MAGAZ2", _
             "MOVPRB.MM_MAGAZ", "MOVPRB.MM_MAGAZ2", "MOVPRB.MM_MAGDISIMP", "MOVPRB.MM_MAGDISIMP2", _
             "ORDLIST.OL_MAGAZ", "ORDLIST.OL_MAGAZ2", "TABTPBF.TB_TMAGAZZ", "TABTPBF.TB_TMAGAZZ2", _
             "TESTMAG.TM_MAGAZ", "TESTMAG.TM_MAGAZ2", "TESTORD.TD_MAGAZ", "TESTORD.TD_MAGAZ2", _
             "TESTPRB.TM_MAGAZ", "ARTDEF.AD_MAGAZ", "TESTPRB.TM_MAGAZ2"
          NTSZOOM.ZoomStrIn("ZOOMTABMAGA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CODPDON"
          NTSZOOM.ZoomStrIn("ZOOMTABPDON", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_CONTROA", "ARTICO.AR_CONTROP", "MOVMAG.MM_CONTROP", "MOVORD.MO_CONTROP", _
             "MOVPRB.MM_CONTROP", "ORDLIST.OL_CONTROP", "TABCOPE.TB_OCODCONTR", "TABTPBF.TB_TCONTRO", _
             "TESTMAG.TM_CONTROP", "TESTORD.TD_CONTROP", "TESTPRB.TM_CONTROP"
          NTSZOOM.ZoomStrIn("ZOOMTABCOVE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_FAMPROD", "BUDGET.BU_CODCFAM", "BUDGETD.BUD_CODCFAM", "BUDGETDMEN.BUDM_CODCFAM", _
             "BUDGETMEN.BUM_CODCFAM", "MOVMAG.MM_CODCFAM", "MOVORD.MO_CODCFAM", "MOVPRB.MM_CODCFAM", _
             "ORDLIST.OL_CODCFAM", "PRIANA.PA_CODCFAM"
          NTSZOOM.ZoomStrIn("ZOOMTABCFAM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_GRUPPO", "TABSGME.TB_CODGRUPM"
          NTSZOOM.ZoomStrIn("ZOOMTABGMER", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "ARTICO.AR_SOTGRU"
          NTSZOOM.ZoomStrIn("ZOOMTABSGME", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "BUDGET.BU_CODCENA", "BUDGETD.BUD_CODCENA", "BUDGETDMEN.BUDM_CODCENA", "BUDGETMEN.BUM_CODCENA", _
             "MOVMAG.MM_CODCENA", "MOVORD.MO_CODCENA", "MOVPRB.MM_CODCENA", "ORDLIST.OL_CODCENA", _
             "PRIANA.PA_CODCENA", "TABCOPE.TB_OCODCENA", "TABTPBF.TB_VCODCEN", "TESTMAG.TM_CODCENA", _
             "TESTORD.TD_CODCENA", "TESTPRB.TM_CODCENA"
          NTSZOOM.ZoomStrIn("ZOOMTABCENA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CODLAVO", "PROBUDGETD.PBD_CODLAVO", "TABCOPE.TB_OCODLAVO", "BUDGETD.BUD_CODLAVO"
          NTSZOOM.ZoomStrIn("ZOOMTABLAVO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "CONDBAN.CB_CAUSALE", "PRINOT.PN_CAUSALE", "SCADEN.SC_CAUSALE"
          NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "CONDBAN.CB_CODBANC", "SCADEN.SC_CODBANC", "TASSI.TX_CODBANC"
          NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "LISTINI.LC_CODTPRO", "PERPROV.PER_CODTPRO"
          NTSZOOM.ZoomStrIn("ZOOMTABTPRO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "MOVMAG.MM_CAUSALE", "MOVMAG.MM_CAUSALE2", "MOVPRB.MM_CAUSALE", "MOVPRB.MM_CAUSALE2", _
             "TABTPBF.TB_TCAUMAG", "TABTPBF.TB_TCAUTRA", "TESTMAG.TMCAUSALE", "TESTMAG.CAUSTRA", _
             "TESTORD.TD_CAMBIO", "TESTPRB.TM_CAUSALE", "TESTPRB.TM_CAUSTRA"
          oParam.strCodPdc = "*"
          oParam.nEscomp = -1
          NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "PRIANA.PA_CODCOPE"
          NTSZOOM.ZoomStrIn("ZOOMTABCOPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TABCAGE.TB_CODTBEN"
          NTSZOOM.ZoomStrIn("ZOOMTABENAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TABMAST.TB_CLASMAST"
          NTSZOOM.ZoomStrIn("ZOOMTABCLAS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODASPE", "TESTORD.TD_CODASPE", "TESTPRB.TM_CODASPE"
          NTSZOOM.ZoomStrIn("ZOOMTABASPE", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODNTRA", "TESTPRB.TM_CODNTRA"
          NTSZOOM.ZoomStrIn("ZOOMTABNTRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODPORT", "TESTPRB.TM_CODPORT"
          NTSZOOM.ZoomStrIn("ZOOMTABMPOR", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_CODRSTA", "TESTPRB.TM_CODRSTA"
          NTSZOOM.ZoomStrIn("ZOOMTABRSTA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF"
          NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case "TESTOFF.TD_CODLEAD", "LEADS.LE_CODLEAD"
          NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
          If NTSZOOM.strIn <> getPz_Vals(i) And NTSZOOM.strIn <> "" Then setPz_Vals(i)
        Case Else
          oCleIstp.strHlvlTabella = strTabella
          oCleIstp.strHlvlCampo = strCampo
          frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
          frmHlvl.InitEntity(oCleIstp)
          frmHlvl.ShowDialog()
          strValore = oCleIstp.strHlvlValore
          If strValore <> "" Then oCleIstp.SalvaValores(i, edPz_Codform.Text, strValore)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmHlvl Is Nothing Then frmHlvl.Dispose()
      frmHlvl = Nothing
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edPz_Sqlsql.NTSDbField = "parstag.pz_sqlsql"
      edPz_xlsfile.NTSDbField = "parstag.pz_xlsfile"
      edPz_xlsfoglio.NTSDbField = "parstag.pz_xlsfoglio"
      edPz_maintab.NTSDbField = "parstag.pz_maintab"
      edPz_Titstam.NTSDbField = "parstag.pz_titstam"
      edPz_Nomrpt.NTSDbField = "parstag.pz_nomrpt"
      edPz_Codform.NTSDbField = "parstag.pz_codform"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Val15.NTSDbField = "parstag.pz_val15"
      edPz_Val14.NTSDbField = "parstag.pz_val14"
      edPz_Val13.NTSDbField = "parstag.pz_val13"
      edPz_Val12.NTSDbField = "parstag.pz_val12"
      edPz_Val11.NTSDbField = "parstag.pz_val11"
      edPz_Val10.NTSDbField = "parstag.pz_val10"
      edPz_Val9.NTSDbField = "parstag.pz_val9"
      edPz_Val8.NTSDbField = "parstag.pz_val8"
      edPz_Val7.NTSDbField = "parstag.pz_val7"
      edPz_Val6.NTSDbField = "parstag.pz_val6"
      edPz_Val5.NTSDbField = "parstag.pz_val5"
      edPz_Val4.NTSDbField = "parstag.pz_val4"
      edPz_Val3.NTSDbField = "parstag.pz_val3"
      edPz_Val2.NTSDbField = "parstag.pz_val2"
      edPz_Val1.NTSDbField = "parstag.pz_val1"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Vals1.NTSDbField = "parstag.pz_vals1"
      edPz_Vals2.NTSDbField = "parstag.pz_vals2"
      edPz_Vals3.NTSDbField = "parstag.pz_vals3"
      edPz_Vals4.NTSDbField = "parstag.pz_vals4"
      edPz_Vals5.NTSDbField = "parstag.pz_vals5"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op15.NTSDbField = "parstag.pz_op15"
      cbPz_Op14.NTSDbField = "parstag.pz_op14"
      cbPz_Op13.NTSDbField = "parstag.pz_op13"
      cbPz_Op12.NTSDbField = "parstag.pz_op12"
      cbPz_Op11.NTSDbField = "parstag.pz_op11"
      cbPz_Op10.NTSDbField = "parstag.pz_op10"
      cbPz_Op9.NTSDbField = "parstag.pz_op9"
      cbPz_Op8.NTSDbField = "parstag.pz_op8"
      cbPz_Op7.NTSDbField = "parstag.pz_op7"
      cbPz_Op6.NTSDbField = "parstag.pz_op6"
      cbPz_Op5.NTSDbField = "parstag.pz_op5"
      cbPz_Op4.NTSDbField = "parstag.pz_op4"
      cbPz_Op3.NTSDbField = "parstag.pz_op3"
      cbPz_Op2.NTSDbField = "parstag.pz_op2"
      cbPz_Op1.NTSDbField = "parstag.pz_op1"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.NTSDbField = "parstag.pz_ops1"
      cbPz_Ops2.NTSDbField = "parstag.pz_ops2"
      cbPz_Ops3.NTSDbField = "parstag.pz_ops3"
      cbPz_Ops4.NTSDbField = "parstag.pz_ops4"
      cbPz_Ops5.NTSDbField = "parstag.pz_ops5"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipo15.NTSDbField = "parstag.pz_tipo15"
      cbPz_Tipo14.NTSDbField = "parstag.pz_tipo14"
      cbPz_Tipo13.NTSDbField = "parstag.pz_tipo13"
      cbPz_Tipo12.NTSDbField = "parstag.pz_tipo12"
      cbPz_Tipo11.NTSDbField = "parstag.pz_tipo11"
      cbPz_Tipo10.NTSDbField = "parstag.pz_tipo10"
      cbPz_Tipo9.NTSDbField = "parstag.pz_tipo9"
      cbPz_Tipo8.NTSDbField = "parstag.pz_tipo8"
      cbPz_Tipo7.NTSDbField = "parstag.pz_tipo7"
      cbPz_Tipo6.NTSDbField = "parstag.pz_tipo6"
      cbPz_Tipo5.NTSDbField = "parstag.pz_tipo5"
      cbPz_Tipo4.NTSDbField = "parstag.pz_tipo4"
      cbPz_Tipo3.NTSDbField = "parstag.pz_tipo3"
      cbPz_Tipo2.NTSDbField = "parstag.pz_tipo2"
      cbPz_Tipo1.NTSDbField = "parstag.pz_tipo1"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipos1.NTSDbField = "parstag.pz_tipos1"
      cbPz_Tipos2.NTSDbField = "parstag.pz_tipos2"
      cbPz_Tipos3.NTSDbField = "parstag.pz_tipos3"
      cbPz_Tipos4.NTSDbField = "parstag.pz_tipos4"
      cbPz_Tipos5.NTSDbField = "parstag.pz_tipos5"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Column15.NTSDbField = "parstag.pz_column15"
      edPz_Column14.NTSDbField = "parstag.pz_column14"
      edPz_Column13.NTSDbField = "parstag.pz_column13"
      edPz_Column12.NTSDbField = "parstag.pz_column12"
      edPz_Column11.NTSDbField = "parstag.pz_column11"
      edPz_Column10.NTSDbField = "parstag.pz_column10"
      edPz_Column9.NTSDbField = "parstag.pz_column9"
      edPz_Column8.NTSDbField = "parstag.pz_column8"
      edPz_Column7.NTSDbField = "parstag.pz_column7"
      edPz_Column6.NTSDbField = "parstag.pz_column6"
      edPz_Column5.NTSDbField = "parstag.pz_column5"
      edPz_Column4.NTSDbField = "parstag.pz_column4"
      edPz_Column3.NTSDbField = "parstag.pz_column3"
      edPz_Column2.NTSDbField = "parstag.pz_column2"
      edPz_Column1.NTSDbField = "parstag.pz_column1"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Columns1.NTSDbField = "parstag.pz_columns1"
      edPz_Columns2.NTSDbField = "parstag.pz_columns2"
      edPz_Columns3.NTSDbField = "parstag.pz_columns3"
      edPz_Columns4.NTSDbField = "parstag.pz_columns4"
      edPz_Columns5.NTSDbField = "parstag.pz_columns5"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descam15.NTSDbField = "parstag.pz_descam15"
      edPz_Descam14.NTSDbField = "parstag.pz_descam14"
      edPz_Descam13.NTSDbField = "parstag.pz_descam13"
      edPz_Descam12.NTSDbField = "parstag.pz_descam12"
      edPz_Descam11.NTSDbField = "parstag.pz_descam11"
      edPz_Descam10.NTSDbField = "parstag.pz_descam10"
      edPz_Descam9.NTSDbField = "parstag.pz_descam9"
      edPz_Descam8.NTSDbField = "parstag.pz_descam8"
      edPz_Descam7.NTSDbField = "parstag.pz_descam7"
      edPz_Descam6.NTSDbField = "parstag.pz_descam6"
      edPz_Descam5.NTSDbField = "parstag.pz_descam5"
      edPz_Descam4.NTSDbField = "parstag.pz_descam4"
      edPz_Descam3.NTSDbField = "parstag.pz_descam3"
      edPz_Descam2.NTSDbField = "parstag.pz_descam2"
      edPz_Descam1.NTSDbField = "parstag.pz_descam1"
      '--------------------------------------------------------------------------------------------------------------
      edPz_Descams1.NTSDbField = "parstag.pz_descams1"
      edPz_Descams2.NTSDbField = "parstag.pz_descams2"
      edPz_Descams3.NTSDbField = "parstag.pz_descams3"
      edPz_Descams4.NTSDbField = "parstag.pz_descams4"
      edPz_Descams5.NTSDbField = "parstag.pz_descams5"
      '--------------------------------------------------------------------------------------------------------------
      edPz_strOprel.NTSDbField = "parstag.pz_stroprel"
      edPz_strOprel2.NTSDbField = "parstag.pz_stroprel2"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcIstp, Me)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttOperatore As New DataTable()
    Dim dttTipcampi As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttOperatore.Columns.Add("cod", GetType(String))
      dttOperatore.Columns.Add("val", GetType(String))
      dttOperatore.Rows.Add(New Object() {"=", "="})
      dttOperatore.Rows.Add(New Object() {"<>", "<>"})
      dttOperatore.Rows.Add(New Object() {">", ">"})
      dttOperatore.Rows.Add(New Object() {">=", ">="})
      dttOperatore.Rows.Add(New Object() {"<", "<"})
      dttOperatore.Rows.Add(New Object() {"<=", "<="})
      dttOperatore.Rows.Add(New Object() {"Like", "Like"})
      dttOperatore.Rows.Add(New Object() {"IS NULL", "IS NULL"})
      dttOperatore.Rows.Add(New Object() {"IS NOT NULL", "IS NOT NULL"})
      dttOperatore.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Op1.DataSource = dttOperatore
      cbPz_Op1.ValueMember = "cod"
      cbPz_Op1.DisplayMember = "val"
      cbPz_Op2.DataSource = dttOperatore
      cbPz_Op2.ValueMember = "cod"
      cbPz_Op2.DisplayMember = "val"
      cbPz_Op3.DataSource = dttOperatore
      cbPz_Op3.ValueMember = "cod"
      cbPz_Op3.DisplayMember = "val"
      cbPz_Op4.DataSource = dttOperatore
      cbPz_Op4.ValueMember = "cod"
      cbPz_Op4.DisplayMember = "val"
      cbPz_Op5.DataSource = dttOperatore
      cbPz_Op5.ValueMember = "cod"
      cbPz_Op5.DisplayMember = "val"
      cbPz_Op6.DataSource = dttOperatore
      cbPz_Op6.ValueMember = "cod"
      cbPz_Op6.DisplayMember = "val"
      cbPz_Op7.DataSource = dttOperatore
      cbPz_Op7.ValueMember = "cod"
      cbPz_Op7.DisplayMember = "val"
      cbPz_Op8.DataSource = dttOperatore
      cbPz_Op8.ValueMember = "cod"
      cbPz_Op8.DisplayMember = "val"
      cbPz_Op9.DataSource = dttOperatore
      cbPz_Op9.ValueMember = "cod"
      cbPz_Op9.DisplayMember = "val"
      cbPz_Op10.DataSource = dttOperatore
      cbPz_Op10.ValueMember = "cod"
      cbPz_Op10.DisplayMember = "val"
      cbPz_Op11.DataSource = dttOperatore
      cbPz_Op11.ValueMember = "cod"
      cbPz_Op11.DisplayMember = "val"
      cbPz_Op12.DataSource = dttOperatore
      cbPz_Op12.ValueMember = "cod"
      cbPz_Op12.DisplayMember = "val"
      cbPz_Op13.DataSource = dttOperatore
      cbPz_Op13.ValueMember = "cod"
      cbPz_Op13.DisplayMember = "val"
      cbPz_Op14.DataSource = dttOperatore
      cbPz_Op14.ValueMember = "cod"
      cbPz_Op14.DisplayMember = "val"
      cbPz_Op15.DataSource = dttOperatore
      cbPz_Op15.ValueMember = "cod"
      cbPz_Op15.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Ops1.DataSource = dttOperatore
      cbPz_Ops1.ValueMember = "cod"
      cbPz_Ops1.DisplayMember = "val"
      cbPz_Ops2.DataSource = dttOperatore
      cbPz_Ops2.ValueMember = "cod"
      cbPz_Ops2.DisplayMember = "val"
      cbPz_Ops3.DataSource = dttOperatore
      cbPz_Ops3.ValueMember = "cod"
      cbPz_Ops3.DisplayMember = "val"
      cbPz_Ops4.DataSource = dttOperatore
      cbPz_Ops4.ValueMember = "cod"
      cbPz_Ops4.DisplayMember = "val"
      cbPz_Ops5.DataSource = dttOperatore
      cbPz_Ops5.ValueMember = "cod"
      cbPz_Ops5.DisplayMember = "val"
      dttTipcampi.Columns.Add("cod", GetType(Integer))
      dttTipcampi.Columns.Add("val", GetType(String))
      '--------------------------------------------------------------------------------------------------------------
      dttTipcampi.Rows.Add(New Object() {0, "(nessuno)"})
      dttTipcampi.Rows.Add(New Object() {1, "Si/No"})
      dttTipcampi.Rows.Add(New Object() {2, "Byte"})
      dttTipcampi.Rows.Add(New Object() {3, "Num.Intero"})
      dttTipcampi.Rows.Add(New Object() {4, "Num.Lungo"})
      dttTipcampi.Rows.Add(New Object() {5, "Valuta"})
      dttTipcampi.Rows.Add(New Object() {7, "Num.Doppia Pr."})
      dttTipcampi.Rows.Add(New Object() {8, "Data"})
      dttTipcampi.Rows.Add(New Object() {10, "Stringa"})
      dttTipcampi.Rows.Add(New Object() {11, "memo"})
      dttTipcampi.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipo1.DataSource = dttTipcampi
      cbPz_Tipo1.ValueMember = "cod"
      cbPz_Tipo1.DisplayMember = "val"
      cbPz_Tipo2.DataSource = dttTipcampi
      cbPz_Tipo2.ValueMember = "cod"
      cbPz_Tipo2.DisplayMember = "val"
      cbPz_Tipo3.DataSource = dttTipcampi
      cbPz_Tipo3.ValueMember = "cod"
      cbPz_Tipo3.DisplayMember = "val"
      cbPz_Tipo4.DataSource = dttTipcampi
      cbPz_Tipo4.ValueMember = "cod"
      cbPz_Tipo4.DisplayMember = "val"
      cbPz_Tipo5.DataSource = dttTipcampi
      cbPz_Tipo5.ValueMember = "cod"
      cbPz_Tipo5.DisplayMember = "val"
      cbPz_Tipo6.DataSource = dttTipcampi
      cbPz_Tipo6.ValueMember = "cod"
      cbPz_Tipo6.DisplayMember = "val"
      cbPz_Tipo7.DataSource = dttTipcampi
      cbPz_Tipo7.ValueMember = "cod"
      cbPz_Tipo7.DisplayMember = "val"
      cbPz_Tipo8.DataSource = dttTipcampi
      cbPz_Tipo8.ValueMember = "cod"
      cbPz_Tipo8.DisplayMember = "val"
      cbPz_Tipo9.DataSource = dttTipcampi
      cbPz_Tipo9.ValueMember = "cod"
      cbPz_Tipo9.DisplayMember = "val"
      cbPz_Tipo10.DataSource = dttTipcampi
      cbPz_Tipo10.ValueMember = "cod"
      cbPz_Tipo10.DisplayMember = "val"
      cbPz_Tipo11.DataSource = dttTipcampi
      cbPz_Tipo11.ValueMember = "cod"
      cbPz_Tipo11.DisplayMember = "val"
      cbPz_Tipo12.DataSource = dttTipcampi
      cbPz_Tipo12.ValueMember = "cod"
      cbPz_Tipo12.DisplayMember = "val"
      cbPz_Tipo13.DataSource = dttTipcampi
      cbPz_Tipo13.ValueMember = "cod"
      cbPz_Tipo13.DisplayMember = "val"
      cbPz_Tipo14.DataSource = dttTipcampi
      cbPz_Tipo14.ValueMember = "cod"
      cbPz_Tipo14.DisplayMember = "val"
      cbPz_Tipo15.DataSource = dttTipcampi
      cbPz_Tipo15.ValueMember = "cod"
      cbPz_Tipo15.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      cbPz_Tipos1.DataSource = dttTipcampi
      cbPz_Tipos1.ValueMember = "cod"
      cbPz_Tipos1.DisplayMember = "val"
      cbPz_Tipos2.DataSource = dttTipcampi
      cbPz_Tipos2.ValueMember = "cod"
      cbPz_Tipos2.DisplayMember = "val"
      cbPz_Tipos3.DataSource = dttTipcampi
      cbPz_Tipos3.ValueMember = "cod"
      cbPz_Tipos3.DisplayMember = "val"
      cbPz_Tipos4.DataSource = dttTipcampi
      cbPz_Tipos4.ValueMember = "cod"
      cbPz_Tipos4.DisplayMember = "val"
      cbPz_Tipos5.DataSource = dttTipcampi
      cbPz_Tipos5.ValueMember = "cod"
      cbPz_Tipos5.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult

    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleIstp.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128306257061636669, "Salvo il formato di stampa?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleIstp.Salva(False) Then Return False
          If dsIstp.Tables("PARSTAG").Rows.Count > 0 Then
            edPz_Codform.Enabled = False
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
  
  Public Overridable Sub cmdnFiltro(ByVal strIndex As Integer)
    Try
      edPz_strOprel.Text = edPz_strOprel.Text & NTSCStr(strIndex)
      If edPz_strOprel.Enabled Then edPz_strOprel.Focus()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdnFiltros(ByVal strIndex As Integer)
    Try
      '--------------------------------------------------------------------------------------------------------------
      edPz_strOprel2.Text = edPz_strOprel2.Text & NTSCStr(strIndex)
      If edPz_strOprel2.Enabled = True Then edPz_strOprel2.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub posizionaAfterZoom(ByVal strZoom As String)
    Dim i As Integer
    Try
      For i = 0 To dcIstp.List.Count - 1
        If CType(dcIstp.Item(i), DataRowView)!Pz_Codform.ToString = strZoom Then
          dcIstp.Position = i
          Exit For
        End If
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function getPz_Val(ByVal i As Integer) As String
    getPz_Val = ""
    Try
      Select Case i
        Case 1
          Return edPz_Val1.Text
        Case 2
          Return edPz_Val2.Text
        Case 3
          Return edPz_Val3.Text
        Case 4
          Return edPz_Val4.Text
        Case 5
          Return edPz_Val5.Text
        Case 6
          Return edPz_Val6.Text
        Case 7
          Return edPz_Val7.Text
        Case 8
          Return edPz_Val8.Text
        Case 9
          Return edPz_Val9.Text
        Case 10
          Return edPz_Val10.Text
        Case 11
          Return edPz_Val11.Text
        Case 12
          Return edPz_Val12.Text
        Case 13
          Return edPz_Val13.Text
        Case 14
          Return edPz_Val14.Text
        Case 15
          Return edPz_Val15.Text
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function getPz_Vals(ByVal i As Integer) As String
    getPz_Vals = ""
    Try
      Select Case i
        Case 1 : Return edPz_Vals1.Text
        Case 2 : Return edPz_Vals2.Text
        Case 3 : Return edPz_Vals3.Text
        Case 4 : Return edPz_Vals4.Text
        Case 5 : Return edPz_Vals5.Text
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub setPz_Val(ByVal i As Integer)
    Try
      Select Case i
        Case 1
          edPz_Val1.NTSTextDB = NTSZOOM.strIn
        Case 2
          edPz_Val2.NTSTextDB = NTSZOOM.strIn
        Case 3
          edPz_Val3.NTSTextDB = NTSZOOM.strIn
        Case 4
          edPz_Val4.NTSTextDB = NTSZOOM.strIn
        Case 5
          edPz_Val5.NTSTextDB = NTSZOOM.strIn
        Case 6
          edPz_Val6.NTSTextDB = NTSZOOM.strIn
        Case 7
          edPz_Val7.NTSTextDB = NTSZOOM.strIn
        Case 8
          edPz_Val8.NTSTextDB = NTSZOOM.strIn
        Case 9
          edPz_Val9.NTSTextDB = NTSZOOM.strIn
        Case 10
          edPz_Val10.NTSTextDB = NTSZOOM.strIn
        Case 11
          edPz_Val11.NTSTextDB = NTSZOOM.strIn
        Case 12
          edPz_Val12.NTSTextDB = NTSZOOM.strIn
        Case 13
          edPz_Val13.NTSTextDB = NTSZOOM.strIn
        Case 14
          edPz_Val14.NTSTextDB = NTSZOOM.strIn
        Case 15
          edPz_Val15.NTSTextDB = NTSZOOM.strIn
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub setPz_Vals(ByVal i As Integer)
    Try
      Select Case i
        Case 1 : edPz_Vals1.NTSTextDB = NTSZOOM.strIn
        Case 2 : edPz_Vals2.NTSTextDB = NTSZOOM.strIn
        Case 3 : edPz_Vals3.NTSTextDB = NTSZOOM.strIn
        Case 4 : edPz_Vals4.NTSTextDB = NTSZOOM.strIn
        Case 5 : edPz_Vals5.NTSTextDB = NTSZOOM.strIn
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub disabilitaPz_Column()
    Try
      'sempre abilitati: per poter gestire i campi di database personalizzati
      'edPz_Column1.Enabled = False
      'edPz_Column2.Enabled = False
      'edPz_Column3.Enabled = False
      'edPz_Column4.Enabled = False
      'edPz_Column5.Enabled = False
      'edPz_Column6.Enabled = False
      'edPz_Column7.Enabled = False
      'edPz_Column8.Enabled = False
      'edPz_Column9.Enabled = False
      'edPz_Column10.Enabled = False
      'edPz_Column11.Enabled = False
      'edPz_Column12.Enabled = False
      'edPz_Column13.Enabled = False
      'edPz_Column14.Enabled = False
      'edPz_Column15.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function EspandiFiltri() As Boolean
    Try
      'If bOnLoading = True Then Return True
      'edDisplayfiltri.Text = oCleIstp.EspandiFiltri(edPz_strOprel.Text, _
      '  NTSCInt(cbPz_Op1.SelectedIndex), NTSCInt(cbPz_Op2.SelectedIndex), NTSCInt(cbPz_Op3.SelectedIndex), _
      '  NTSCInt(cbPz_Op4.SelectedIndex), NTSCInt(cbPz_Op5.SelectedIndex), NTSCInt(cbPz_Op6.SelectedIndex), _
      '  NTSCInt(cbPz_Op7.SelectedIndex), NTSCInt(cbPz_Op8.SelectedIndex), NTSCInt(cbPz_Op9.SelectedIndex), _
      '  NTSCInt(cbPz_Op10.SelectedIndex), NTSCInt(cbPz_Op11.SelectedIndex), NTSCInt(cbPz_Op12.SelectedIndex), _
      '  NTSCInt(cbPz_Op13.SelectedIndex), NTSCInt(cbPz_Op14.SelectedIndex), NTSCInt(cbPz_Op15.SelectedIndex), _
      '  edPz_Descam1.Text, edPz_Descam2.Text, edPz_Descam3.Text, _
      '  edPz_Descam4.Text, edPz_Descam5.Text, edPz_Descam6.Text, _
      '  edPz_Descam7.Text, edPz_Descam8.Text, edPz_Descam9.Text, _
      '  edPz_Descam10.Text, edPz_Descam11.Text, edPz_Descam12.Text, _
      '  edPz_Descam13.Text, edPz_Descam14.Text, edPz_Descam15.Text, _
      '  edPz_Val1.Text, edPz_Val2.Text, edPz_Val3.Text, _
      '  edPz_Val4.Text, edPz_Val5.Text, edPz_Val6.Text, _
      '  edPz_Val7.Text, edPz_Val8.Text, edPz_Val9.Text, _
      '  edPz_Val10.Text, edPz_Val11.Text, edPz_Val12.Text, _
      '  edPz_Val13.Text, edPz_Val14.Text, edPz_Val15.Text)
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
