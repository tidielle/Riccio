Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__AEXT

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = 0
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
  Public oCleAext As CLE__AEXT
  Public dsAext As DataSet
  Public dcAext As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public nEditMode As Integer '0 nessuna 1 apri 2 nuovo
  Public charTipork As Char

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnAext1 As NTSInformatica.NTSPanel
  Public WithEvents tabAext As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnAext2 As NTSInformatica.NTSPanel
  Public WithEvents pnAext3 As NTSInformatica.NTSPanel
  Public WithEvents pnAext4 As NTSInformatica.NTSPanel
  Public WithEvents pnAext5 As NTSInformatica.NTSPanel
  Public WithEvents lbTb_helptipo1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_tipo1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destab1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipo1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTipork As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipo3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helptipo3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_tipo3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipo2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helptipo2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_tipo2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_descr10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_descr6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_destab2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_desext3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desext2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_desext3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desext2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_desext1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_desext1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_data3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_data5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_data2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_data4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_data1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr7 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descr6 As NTSInformatica.NTSLabel
  Public WithEvents edTb_data3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_data5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_data2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_data4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_data1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_check10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check7 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_check1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_check10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_check6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_destab6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num10 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num9 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num7 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num6 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_num1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_num10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_num6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_destab5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_memo2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_memo1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_memo2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_memo1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_destab4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_destab3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destab4 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_combo2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_combo2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_combo1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_combo1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helpcom1_L As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_E As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_I As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_D As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_H As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_C As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_G As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_B As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_F As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom1_A As NTSInformatica.NTSLabel
  Public WithEvents edTb_helpcom1_L As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_E As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_D As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_H As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_B As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_I As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_C As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_G As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_A As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom1_F As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_combo3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_combo3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helpcom3_V As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_L As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_Q As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_E As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_U As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_I As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_P As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_D As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_T As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_H As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_O As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_C As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_S As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_G As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_N As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_B As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_R As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_F As NTSInformatica.NTSLabel
  Public WithEvents edTb_helpcom3_V As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helpcom3_M As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom3_A As NTSInformatica.NTSLabel
  Public WithEvents edTb_helpcom3_Q As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_L As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_E As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_P As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_T As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_D As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_H As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_N As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_U As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_B As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_I As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_O As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_S As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_C As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_G As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_M As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_R As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_A As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom3_F As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_helpcom2_E As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom2_D As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom2_C As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom2_B As NTSInformatica.NTSLabel
  Public WithEvents lbTb_helpcom2_A As NTSInformatica.NTSLabel
  Public WithEvents edTb_helpcom2_E As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom2_D As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom2_B As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom2_C As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_helpcom2_A As NTSInformatica.NTSTextBoxStr
  Public WithEvents mbTb_helptipo3 As NTSInformatica.NTSMemoBox
  Public WithEvents mbTb_helptipo2 As NTSInformatica.NTSMemoBox
  Public WithEvents mbTb_helptipo1 As NTSInformatica.NTSMemoBox
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnAext As NTSInformatica.NTSPanel
  Public WithEvents pnInfo As NTSInformatica.NTSPanel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
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
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__AEXT", "BE__AEXT", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleAext = CType(objTmp, CLE__AEXT)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__AEXT", strRemoteServer, strRemotePort)
    AddHandler oCleAext.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAext.Init(oApp, NTSScript, oMenu.oCleComm, "TABAEXT", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

#Region "Inizializzazione controlli"
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__AEXT))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnAext1 = New NTSInformatica.NTSPanel
    Me.mbTb_helptipo3 = New NTSInformatica.NTSMemoBox
    Me.mbTb_helptipo2 = New NTSInformatica.NTSMemoBox
    Me.mbTb_helptipo1 = New NTSInformatica.NTSMemoBox
    Me.edTb_tipo3 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helptipo3 = New NTSInformatica.NTSLabel
    Me.lbTb_tipo3 = New NTSInformatica.NTSLabel
    Me.edTb_tipo2 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helptipo2 = New NTSInformatica.NTSLabel
    Me.lbTb_tipo2 = New NTSInformatica.NTSLabel
    Me.edTb_tipo1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab1 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helptipo1 = New NTSInformatica.NTSLabel
    Me.lbTb_tipo1 = New NTSInformatica.NTSLabel
    Me.lbTb_destab1 = New NTSInformatica.NTSLabel
    Me.tabAext = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnAext2 = New NTSInformatica.NTSPanel
    Me.lbTb_descr10 = New NTSInformatica.NTSLabel
    Me.lbTb_data3 = New NTSInformatica.NTSLabel
    Me.lbTb_descr5 = New NTSInformatica.NTSLabel
    Me.lbTb_descr9 = New NTSInformatica.NTSLabel
    Me.lbTb_data5 = New NTSInformatica.NTSLabel
    Me.lbTb_data2 = New NTSInformatica.NTSLabel
    Me.lbTb_descr4 = New NTSInformatica.NTSLabel
    Me.lbTb_descr8 = New NTSInformatica.NTSLabel
    Me.lbTb_data4 = New NTSInformatica.NTSLabel
    Me.lbTb_data1 = New NTSInformatica.NTSLabel
    Me.lbTb_descr3 = New NTSInformatica.NTSLabel
    Me.lbTb_descr7 = New NTSInformatica.NTSLabel
    Me.lbTb_descr2 = New NTSInformatica.NTSLabel
    Me.lbTb_descr6 = New NTSInformatica.NTSLabel
    Me.lbTb_descr1 = New NTSInformatica.NTSLabel
    Me.edTb_data3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_data5 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr10 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_data2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr5 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr8 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_data4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_data1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr9 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr7 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_descr6 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab2 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_destab2 = New NTSInformatica.NTSLabel
    Me.edTb_desext3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_desext2 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_desext3 = New NTSInformatica.NTSLabel
    Me.lbTb_desext2 = New NTSInformatica.NTSLabel
    Me.edTb_desext1 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_desext1 = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnAext3 = New NTSInformatica.NTSPanel
    Me.lbTb_check10 = New NTSInformatica.NTSLabel
    Me.lbTb_check5 = New NTSInformatica.NTSLabel
    Me.lbTb_check9 = New NTSInformatica.NTSLabel
    Me.lbTb_check4 = New NTSInformatica.NTSLabel
    Me.lbTb_check8 = New NTSInformatica.NTSLabel
    Me.lbTb_check3 = New NTSInformatica.NTSLabel
    Me.lbTb_check7 = New NTSInformatica.NTSLabel
    Me.lbTb_check2 = New NTSInformatica.NTSLabel
    Me.lbTb_check6 = New NTSInformatica.NTSLabel
    Me.lbTb_check1 = New NTSInformatica.NTSLabel
    Me.edTb_check10 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check5 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check8 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check9 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check7 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_check6 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab6 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_destab6 = New NTSInformatica.NTSLabel
    Me.lbTb_num10 = New NTSInformatica.NTSLabel
    Me.lbTb_num5 = New NTSInformatica.NTSLabel
    Me.lbTb_num9 = New NTSInformatica.NTSLabel
    Me.lbTb_num4 = New NTSInformatica.NTSLabel
    Me.lbTb_num8 = New NTSInformatica.NTSLabel
    Me.lbTb_num3 = New NTSInformatica.NTSLabel
    Me.lbTb_memo2 = New NTSInformatica.NTSLabel
    Me.lbTb_num7 = New NTSInformatica.NTSLabel
    Me.lbTb_num2 = New NTSInformatica.NTSLabel
    Me.lbTb_memo1 = New NTSInformatica.NTSLabel
    Me.lbTb_num6 = New NTSInformatica.NTSLabel
    Me.lbTb_num1 = New NTSInformatica.NTSLabel
    Me.edTb_num10 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num5 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num8 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num9 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_memo2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num7 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_memo1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num1 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_num6 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab4 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_destab5 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_destab3 = New NTSInformatica.NTSLabel
    Me.lbTb_destab4 = New NTSInformatica.NTSLabel
    Me.lbTb_destab5 = New NTSInformatica.NTSLabel
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnAext4 = New NTSInformatica.NTSPanel
    Me.lbTb_combo2 = New NTSInformatica.NTSLabel
    Me.edTb_combo2 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_combo1 = New NTSInformatica.NTSLabel
    Me.edTb_combo1 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helpcom1_L = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom2_E = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_E = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_I = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom2_D = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_D = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_H = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom2_C = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_C = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_G = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom2_B = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_B = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_F = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom2_A = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom1_A = New NTSInformatica.NTSLabel
    Me.edTb_helpcom2_E = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_L = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom2_D = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_E = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_D = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom2_B = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_H = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_B = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom2_C = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_I = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_C = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom2_A = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_G = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_A = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom1_F = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnAext5 = New NTSInformatica.NTSPanel
    Me.lbTb_combo3 = New NTSInformatica.NTSLabel
    Me.edTb_combo3 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helpcom3_V = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_L = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_Q = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_E = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_U = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_I = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_P = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_D = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_T = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_H = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_O = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_C = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_S = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_G = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_N = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_B = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_R = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_F = New NTSInformatica.NTSLabel
    Me.edTb_helpcom3_V = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_helpcom3_M = New NTSInformatica.NTSLabel
    Me.lbTb_helpcom3_A = New NTSInformatica.NTSLabel
    Me.edTb_helpcom3_Q = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_L = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_E = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_P = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_T = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_D = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_H = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_N = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_U = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_B = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_I = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_O = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_S = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_C = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_G = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_M = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_R = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_A = New NTSInformatica.NTSTextBoxStr
    Me.edTb_helpcom3_F = New NTSInformatica.NTSTextBoxStr
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.pnAext = New NTSInformatica.NTSPanel
    Me.pnInfo = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAext1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext1.SuspendLayout()
    CType(Me.mbTb_helptipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.mbTb_helptipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.mbTb_helptipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_tipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tabAext, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabAext.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnAext2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext2.SuspendLayout()
    CType(Me.edTb_data3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_data5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_data2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_data4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_data1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descr6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desext3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desext2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desext1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnAext3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext3.SuspendLayout()
    CType(Me.edTb_check10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_check6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_memo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_memo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_num6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destab5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnAext4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext4.SuspendLayout()
    CType(Me.edTb_combo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_combo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom2_E.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_L.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom2_D.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_E.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_D.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom2_B.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_B.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom2_C.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_I.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_C.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom2_A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_G.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom1_F.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnAext5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext5.SuspendLayout()
    CType(Me.edTb_combo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_V.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_Q.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_L.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_E.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_P.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_T.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_D.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_N.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_U.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_B.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_I.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_O.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_S.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_C.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_G.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_M.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_R.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_helpcom3_F.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAext, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAext.SuspendLayout()
    CType(Me.pnInfo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnInfo.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(30, 56)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbSalva, Me.tlbApri, Me.tlbCancella})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 6
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 9
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 8
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 10
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnAext1
    '
    Me.pnAext1.AllowDrop = True
    Me.pnAext1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext1.Appearance.Options.UseBackColor = True
    Me.pnAext1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext1.Controls.Add(Me.mbTb_helptipo3)
    Me.pnAext1.Controls.Add(Me.mbTb_helptipo2)
    Me.pnAext1.Controls.Add(Me.mbTb_helptipo1)
    Me.pnAext1.Controls.Add(Me.edTb_tipo3)
    Me.pnAext1.Controls.Add(Me.lbTb_helptipo3)
    Me.pnAext1.Controls.Add(Me.lbTb_tipo3)
    Me.pnAext1.Controls.Add(Me.edTb_tipo2)
    Me.pnAext1.Controls.Add(Me.lbTb_helptipo2)
    Me.pnAext1.Controls.Add(Me.lbTb_tipo2)
    Me.pnAext1.Controls.Add(Me.edTb_tipo1)
    Me.pnAext1.Controls.Add(Me.edTb_destab1)
    Me.pnAext1.Controls.Add(Me.lbTb_helptipo1)
    Me.pnAext1.Controls.Add(Me.lbTb_tipo1)
    Me.pnAext1.Controls.Add(Me.lbTb_destab1)
    Me.pnAext1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext1.Location = New System.Drawing.Point(0, 0)
    Me.pnAext1.Name = "pnAext1"
    Me.pnAext1.NTSActiveTrasparency = True
    Me.pnAext1.Size = New System.Drawing.Size(685, 352)
    Me.pnAext1.TabIndex = 0
    Me.pnAext1.Text = "NtsPanel1"
    '
    'mbTb_helptipo3
    '
    Me.mbTb_helptipo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.mbTb_helptipo3.Location = New System.Drawing.Point(151, 260)
    Me.mbTb_helptipo3.Name = "mbTb_helptipo3"
    Me.mbTb_helptipo3.NTSDbField = ""
    Me.mbTb_helptipo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.mbTb_helptipo3.Size = New System.Drawing.Size(528, 69)
    Me.mbTb_helptipo3.TabIndex = 10
    '
    'mbTb_helptipo2
    '
    Me.mbTb_helptipo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.mbTb_helptipo2.Location = New System.Drawing.Point(151, 160)
    Me.mbTb_helptipo2.Name = "mbTb_helptipo2"
    Me.mbTb_helptipo2.NTSDbField = ""
    Me.mbTb_helptipo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.mbTb_helptipo2.Size = New System.Drawing.Size(528, 69)
    Me.mbTb_helptipo2.TabIndex = 10
    '
    'mbTb_helptipo1
    '
    Me.mbTb_helptipo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.mbTb_helptipo1.Location = New System.Drawing.Point(151, 60)
    Me.mbTb_helptipo1.Name = "mbTb_helptipo1"
    Me.mbTb_helptipo1.NTSDbField = ""
    Me.mbTb_helptipo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.mbTb_helptipo1.Size = New System.Drawing.Size(528, 69)
    Me.mbTb_helptipo1.TabIndex = 10
    '
    'edTb_tipo3
    '
    Me.edTb_tipo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipo3.Location = New System.Drawing.Point(151, 236)
    Me.edTb_tipo3.Name = "edTb_tipo3"
    Me.edTb_tipo3.NTSDbField = ""
    Me.edTb_tipo3.NTSForzaVisZoom = False
    Me.edTb_tipo3.NTSOldValue = ""
    Me.edTb_tipo3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipo3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipo3.Properties.AutoHeight = False
    Me.edTb_tipo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipo3.Properties.MaxLength = 65536
    Me.edTb_tipo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipo3.Size = New System.Drawing.Size(250, 20)
    Me.edTb_tipo3.TabIndex = 9
    '
    'lbTb_helptipo3
    '
    Me.lbTb_helptipo3.AutoSize = True
    Me.lbTb_helptipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helptipo3.Location = New System.Drawing.Point(9, 262)
    Me.lbTb_helptipo3.Name = "lbTb_helptipo3"
    Me.lbTb_helptipo3.NTSDbField = ""
    Me.lbTb_helptipo3.Size = New System.Drawing.Size(136, 13)
    Me.lbTb_helptipo3.TabIndex = 7
    Me.lbTb_helptipo3.Text = "Help tipo da un carattere 3"
    Me.lbTb_helptipo3.Tooltip = ""
    Me.lbTb_helptipo3.UseMnemonic = False
    '
    'lbTb_tipo3
    '
    Me.lbTb_tipo3.AutoSize = True
    Me.lbTb_tipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipo3.Location = New System.Drawing.Point(9, 239)
    Me.lbTb_tipo3.Name = "lbTb_tipo3"
    Me.lbTb_tipo3.NTSDbField = ""
    Me.lbTb_tipo3.Size = New System.Drawing.Size(114, 13)
    Me.lbTb_tipo3.TabIndex = 8
    Me.lbTb_tipo3.Text = "Tipo da un carattere 3"
    Me.lbTb_tipo3.Tooltip = ""
    Me.lbTb_tipo3.UseMnemonic = False
    '
    'edTb_tipo2
    '
    Me.edTb_tipo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipo2.Location = New System.Drawing.Point(151, 136)
    Me.edTb_tipo2.Name = "edTb_tipo2"
    Me.edTb_tipo2.NTSDbField = ""
    Me.edTb_tipo2.NTSForzaVisZoom = False
    Me.edTb_tipo2.NTSOldValue = ""
    Me.edTb_tipo2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipo2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipo2.Properties.AutoHeight = False
    Me.edTb_tipo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipo2.Properties.MaxLength = 65536
    Me.edTb_tipo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipo2.Size = New System.Drawing.Size(250, 20)
    Me.edTb_tipo2.TabIndex = 5
    '
    'lbTb_helptipo2
    '
    Me.lbTb_helptipo2.AutoSize = True
    Me.lbTb_helptipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helptipo2.Location = New System.Drawing.Point(9, 162)
    Me.lbTb_helptipo2.Name = "lbTb_helptipo2"
    Me.lbTb_helptipo2.NTSDbField = ""
    Me.lbTb_helptipo2.Size = New System.Drawing.Size(136, 13)
    Me.lbTb_helptipo2.TabIndex = 3
    Me.lbTb_helptipo2.Text = "Help tipo da un carattere 2"
    Me.lbTb_helptipo2.Tooltip = ""
    Me.lbTb_helptipo2.UseMnemonic = False
    '
    'lbTb_tipo2
    '
    Me.lbTb_tipo2.AutoSize = True
    Me.lbTb_tipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipo2.Location = New System.Drawing.Point(9, 139)
    Me.lbTb_tipo2.Name = "lbTb_tipo2"
    Me.lbTb_tipo2.NTSDbField = ""
    Me.lbTb_tipo2.Size = New System.Drawing.Size(114, 13)
    Me.lbTb_tipo2.TabIndex = 4
    Me.lbTb_tipo2.Text = "Tipo da un carattere 2"
    Me.lbTb_tipo2.Tooltip = ""
    Me.lbTb_tipo2.UseMnemonic = False
    '
    'edTb_tipo1
    '
    Me.edTb_tipo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_tipo1.Location = New System.Drawing.Point(151, 36)
    Me.edTb_tipo1.Name = "edTb_tipo1"
    Me.edTb_tipo1.NTSDbField = ""
    Me.edTb_tipo1.NTSForzaVisZoom = False
    Me.edTb_tipo1.NTSOldValue = ""
    Me.edTb_tipo1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_tipo1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_tipo1.Properties.AutoHeight = False
    Me.edTb_tipo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_tipo1.Properties.MaxLength = 65536
    Me.edTb_tipo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_tipo1.Size = New System.Drawing.Size(250, 20)
    Me.edTb_tipo1.TabIndex = 1
    '
    'edTb_destab1
    '
    Me.edTb_destab1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab1.Location = New System.Drawing.Point(151, 5)
    Me.edTb_destab1.Name = "edTb_destab1"
    Me.edTb_destab1.NTSDbField = ""
    Me.edTb_destab1.NTSForzaVisZoom = False
    Me.edTb_destab1.NTSOldValue = ""
    Me.edTb_destab1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab1.Properties.AutoHeight = False
    Me.edTb_destab1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab1.Properties.MaxLength = 65536
    Me.edTb_destab1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab1.TabIndex = 1
    '
    'lbTb_helptipo1
    '
    Me.lbTb_helptipo1.AutoSize = True
    Me.lbTb_helptipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helptipo1.Location = New System.Drawing.Point(9, 62)
    Me.lbTb_helptipo1.Name = "lbTb_helptipo1"
    Me.lbTb_helptipo1.NTSDbField = ""
    Me.lbTb_helptipo1.Size = New System.Drawing.Size(136, 13)
    Me.lbTb_helptipo1.TabIndex = 0
    Me.lbTb_helptipo1.Text = "Help tipo da un carattere 1"
    Me.lbTb_helptipo1.Tooltip = ""
    Me.lbTb_helptipo1.UseMnemonic = False
    '
    'lbTb_tipo1
    '
    Me.lbTb_tipo1.AutoSize = True
    Me.lbTb_tipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tipo1.Location = New System.Drawing.Point(9, 39)
    Me.lbTb_tipo1.Name = "lbTb_tipo1"
    Me.lbTb_tipo1.NTSDbField = ""
    Me.lbTb_tipo1.Size = New System.Drawing.Size(114, 13)
    Me.lbTb_tipo1.TabIndex = 0
    Me.lbTb_tipo1.Text = "Tipo da un carattere 1"
    Me.lbTb_tipo1.Tooltip = ""
    Me.lbTb_tipo1.UseMnemonic = False
    '
    'lbTb_destab1
    '
    Me.lbTb_destab1.AutoSize = True
    Me.lbTb_destab1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab1.Location = New System.Drawing.Point(9, 8)
    Me.lbTb_destab1.Name = "lbTb_destab1"
    Me.lbTb_destab1.NTSDbField = ""
    Me.lbTb_destab1.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab1.TabIndex = 0
    Me.lbTb_destab1.Text = "Descrizione Tab 1"
    Me.lbTb_destab1.Tooltip = ""
    Me.lbTb_destab1.UseMnemonic = False
    '
    'tabAext
    '
    Me.tabAext.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tabAext.Location = New System.Drawing.Point(0, 20)
    Me.tabAext.Name = "tabAext"
    Me.tabAext.SelectedTabPage = Me.NtsTabPage1
    Me.tabAext.Size = New System.Drawing.Size(694, 382)
    Me.tabAext.TabIndex = 0
    Me.tabAext.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5})
    Me.tabAext.Text = "tabAext"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.pnAext1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(685, 352)
    Me.NtsTabPage1.Text = "Pagina 1"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnAext2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(685, 352)
    Me.NtsTabPage2.Text = "Pagina 2"
    '
    'pnAext2
    '
    Me.pnAext2.AllowDrop = True
    Me.pnAext2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext2.Appearance.Options.UseBackColor = True
    Me.pnAext2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext2.Controls.Add(Me.lbTb_descr10)
    Me.pnAext2.Controls.Add(Me.lbTb_data3)
    Me.pnAext2.Controls.Add(Me.lbTb_descr5)
    Me.pnAext2.Controls.Add(Me.lbTb_descr9)
    Me.pnAext2.Controls.Add(Me.lbTb_data5)
    Me.pnAext2.Controls.Add(Me.lbTb_data2)
    Me.pnAext2.Controls.Add(Me.lbTb_descr4)
    Me.pnAext2.Controls.Add(Me.lbTb_descr8)
    Me.pnAext2.Controls.Add(Me.lbTb_data4)
    Me.pnAext2.Controls.Add(Me.lbTb_data1)
    Me.pnAext2.Controls.Add(Me.lbTb_descr3)
    Me.pnAext2.Controls.Add(Me.lbTb_descr7)
    Me.pnAext2.Controls.Add(Me.lbTb_descr2)
    Me.pnAext2.Controls.Add(Me.lbTb_descr6)
    Me.pnAext2.Controls.Add(Me.lbTb_descr1)
    Me.pnAext2.Controls.Add(Me.edTb_data3)
    Me.pnAext2.Controls.Add(Me.edTb_data5)
    Me.pnAext2.Controls.Add(Me.edTb_descr10)
    Me.pnAext2.Controls.Add(Me.edTb_data2)
    Me.pnAext2.Controls.Add(Me.edTb_descr5)
    Me.pnAext2.Controls.Add(Me.edTb_descr4)
    Me.pnAext2.Controls.Add(Me.edTb_descr8)
    Me.pnAext2.Controls.Add(Me.edTb_data4)
    Me.pnAext2.Controls.Add(Me.edTb_descr2)
    Me.pnAext2.Controls.Add(Me.edTb_data1)
    Me.pnAext2.Controls.Add(Me.edTb_descr9)
    Me.pnAext2.Controls.Add(Me.edTb_descr3)
    Me.pnAext2.Controls.Add(Me.edTb_descr7)
    Me.pnAext2.Controls.Add(Me.edTb_descr1)
    Me.pnAext2.Controls.Add(Me.edTb_descr6)
    Me.pnAext2.Controls.Add(Me.edTb_destab2)
    Me.pnAext2.Controls.Add(Me.lbTb_destab2)
    Me.pnAext2.Controls.Add(Me.edTb_desext3)
    Me.pnAext2.Controls.Add(Me.edTb_desext2)
    Me.pnAext2.Controls.Add(Me.lbTb_desext3)
    Me.pnAext2.Controls.Add(Me.lbTb_desext2)
    Me.pnAext2.Controls.Add(Me.edTb_desext1)
    Me.pnAext2.Controls.Add(Me.lbTb_desext1)
    Me.pnAext2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext2.Location = New System.Drawing.Point(0, 0)
    Me.pnAext2.Name = "pnAext2"
    Me.pnAext2.NTSActiveTrasparency = True
    Me.pnAext2.Size = New System.Drawing.Size(685, 352)
    Me.pnAext2.TabIndex = 5
    Me.pnAext2.Text = "NtsPanel1"
    '
    'lbTb_descr10
    '
    Me.lbTb_descr10.AutoSize = True
    Me.lbTb_descr10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr10.Location = New System.Drawing.Point(358, 209)
    Me.lbTb_descr10.Name = "lbTb_descr10"
    Me.lbTb_descr10.NTSDbField = ""
    Me.lbTb_descr10.Size = New System.Drawing.Size(108, 13)
    Me.lbTb_descr10.TabIndex = 6
    Me.lbTb_descr10.Text = "Campo descrittivo 10"
    Me.lbTb_descr10.Tooltip = ""
    Me.lbTb_descr10.UseMnemonic = False
    '
    'lbTb_data3
    '
    Me.lbTb_data3.AutoSize = True
    Me.lbTb_data3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_data3.Location = New System.Drawing.Point(9, 290)
    Me.lbTb_data3.Name = "lbTb_data3"
    Me.lbTb_data3.NTSDbField = ""
    Me.lbTb_data3.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_data3.TabIndex = 6
    Me.lbTb_data3.Text = "Campo data 3"
    Me.lbTb_data3.Tooltip = ""
    Me.lbTb_data3.UseMnemonic = False
    '
    'lbTb_descr5
    '
    Me.lbTb_descr5.AutoSize = True
    Me.lbTb_descr5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr5.Location = New System.Drawing.Point(9, 209)
    Me.lbTb_descr5.Name = "lbTb_descr5"
    Me.lbTb_descr5.NTSDbField = ""
    Me.lbTb_descr5.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr5.TabIndex = 6
    Me.lbTb_descr5.Text = "Campo descrittivo 5"
    Me.lbTb_descr5.Tooltip = ""
    Me.lbTb_descr5.UseMnemonic = False
    '
    'lbTb_descr9
    '
    Me.lbTb_descr9.AutoSize = True
    Me.lbTb_descr9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr9.Location = New System.Drawing.Point(358, 185)
    Me.lbTb_descr9.Name = "lbTb_descr9"
    Me.lbTb_descr9.NTSDbField = ""
    Me.lbTb_descr9.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr9.TabIndex = 6
    Me.lbTb_descr9.Text = "Campo descrittivo 9"
    Me.lbTb_descr9.Tooltip = ""
    Me.lbTb_descr9.UseMnemonic = False
    '
    'lbTb_data5
    '
    Me.lbTb_data5.AutoSize = True
    Me.lbTb_data5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_data5.Location = New System.Drawing.Point(358, 266)
    Me.lbTb_data5.Name = "lbTb_data5"
    Me.lbTb_data5.NTSDbField = ""
    Me.lbTb_data5.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_data5.TabIndex = 6
    Me.lbTb_data5.Text = "Campo data 5"
    Me.lbTb_data5.Tooltip = ""
    Me.lbTb_data5.UseMnemonic = False
    '
    'lbTb_data2
    '
    Me.lbTb_data2.AutoSize = True
    Me.lbTb_data2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_data2.Location = New System.Drawing.Point(9, 266)
    Me.lbTb_data2.Name = "lbTb_data2"
    Me.lbTb_data2.NTSDbField = ""
    Me.lbTb_data2.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_data2.TabIndex = 6
    Me.lbTb_data2.Text = "Campo data 2"
    Me.lbTb_data2.Tooltip = ""
    Me.lbTb_data2.UseMnemonic = False
    '
    'lbTb_descr4
    '
    Me.lbTb_descr4.AutoSize = True
    Me.lbTb_descr4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr4.Location = New System.Drawing.Point(9, 185)
    Me.lbTb_descr4.Name = "lbTb_descr4"
    Me.lbTb_descr4.NTSDbField = ""
    Me.lbTb_descr4.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr4.TabIndex = 6
    Me.lbTb_descr4.Text = "Campo descrittivo 4"
    Me.lbTb_descr4.Tooltip = ""
    Me.lbTb_descr4.UseMnemonic = False
    '
    'lbTb_descr8
    '
    Me.lbTb_descr8.AutoSize = True
    Me.lbTb_descr8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr8.Location = New System.Drawing.Point(358, 161)
    Me.lbTb_descr8.Name = "lbTb_descr8"
    Me.lbTb_descr8.NTSDbField = ""
    Me.lbTb_descr8.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr8.TabIndex = 6
    Me.lbTb_descr8.Text = "Campo descrittivo 8"
    Me.lbTb_descr8.Tooltip = ""
    Me.lbTb_descr8.UseMnemonic = False
    '
    'lbTb_data4
    '
    Me.lbTb_data4.AutoSize = True
    Me.lbTb_data4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_data4.Location = New System.Drawing.Point(358, 242)
    Me.lbTb_data4.Name = "lbTb_data4"
    Me.lbTb_data4.NTSDbField = ""
    Me.lbTb_data4.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_data4.TabIndex = 6
    Me.lbTb_data4.Text = "Campo data 4"
    Me.lbTb_data4.Tooltip = ""
    Me.lbTb_data4.UseMnemonic = False
    '
    'lbTb_data1
    '
    Me.lbTb_data1.AutoSize = True
    Me.lbTb_data1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_data1.Location = New System.Drawing.Point(9, 242)
    Me.lbTb_data1.Name = "lbTb_data1"
    Me.lbTb_data1.NTSDbField = ""
    Me.lbTb_data1.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_data1.TabIndex = 6
    Me.lbTb_data1.Text = "Campo data 1"
    Me.lbTb_data1.Tooltip = ""
    Me.lbTb_data1.UseMnemonic = False
    '
    'lbTb_descr3
    '
    Me.lbTb_descr3.AutoSize = True
    Me.lbTb_descr3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr3.Location = New System.Drawing.Point(9, 161)
    Me.lbTb_descr3.Name = "lbTb_descr3"
    Me.lbTb_descr3.NTSDbField = ""
    Me.lbTb_descr3.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr3.TabIndex = 6
    Me.lbTb_descr3.Text = "Campo descrittivo 3"
    Me.lbTb_descr3.Tooltip = ""
    Me.lbTb_descr3.UseMnemonic = False
    '
    'lbTb_descr7
    '
    Me.lbTb_descr7.AutoSize = True
    Me.lbTb_descr7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr7.Location = New System.Drawing.Point(358, 137)
    Me.lbTb_descr7.Name = "lbTb_descr7"
    Me.lbTb_descr7.NTSDbField = ""
    Me.lbTb_descr7.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr7.TabIndex = 6
    Me.lbTb_descr7.Text = "Campo descrittivo 7"
    Me.lbTb_descr7.Tooltip = ""
    Me.lbTb_descr7.UseMnemonic = False
    '
    'lbTb_descr2
    '
    Me.lbTb_descr2.AutoSize = True
    Me.lbTb_descr2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr2.Location = New System.Drawing.Point(9, 137)
    Me.lbTb_descr2.Name = "lbTb_descr2"
    Me.lbTb_descr2.NTSDbField = ""
    Me.lbTb_descr2.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr2.TabIndex = 6
    Me.lbTb_descr2.Text = "Campo descrittivo 2"
    Me.lbTb_descr2.Tooltip = ""
    Me.lbTb_descr2.UseMnemonic = False
    '
    'lbTb_descr6
    '
    Me.lbTb_descr6.AutoSize = True
    Me.lbTb_descr6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr6.Location = New System.Drawing.Point(358, 113)
    Me.lbTb_descr6.Name = "lbTb_descr6"
    Me.lbTb_descr6.NTSDbField = ""
    Me.lbTb_descr6.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr6.TabIndex = 6
    Me.lbTb_descr6.Text = "Campo descrittivo 6"
    Me.lbTb_descr6.Tooltip = ""
    Me.lbTb_descr6.UseMnemonic = False
    '
    'lbTb_descr1
    '
    Me.lbTb_descr1.AutoSize = True
    Me.lbTb_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descr1.Location = New System.Drawing.Point(9, 113)
    Me.lbTb_descr1.Name = "lbTb_descr1"
    Me.lbTb_descr1.NTSDbField = ""
    Me.lbTb_descr1.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_descr1.TabIndex = 6
    Me.lbTb_descr1.Text = "Campo descrittivo 1"
    Me.lbTb_descr1.Tooltip = ""
    Me.lbTb_descr1.UseMnemonic = False
    '
    'edTb_data3
    '
    Me.edTb_data3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_data3.Location = New System.Drawing.Point(120, 287)
    Me.edTb_data3.Name = "edTb_data3"
    Me.edTb_data3.NTSDbField = ""
    Me.edTb_data3.NTSForzaVisZoom = False
    Me.edTb_data3.NTSOldValue = ""
    Me.edTb_data3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_data3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_data3.Properties.AutoHeight = False
    Me.edTb_data3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_data3.Properties.MaxLength = 65536
    Me.edTb_data3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_data3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_data3.TabIndex = 5
    '
    'edTb_data5
    '
    Me.edTb_data5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_data5.Location = New System.Drawing.Point(472, 263)
    Me.edTb_data5.Name = "edTb_data5"
    Me.edTb_data5.NTSDbField = ""
    Me.edTb_data5.NTSForzaVisZoom = False
    Me.edTb_data5.NTSOldValue = ""
    Me.edTb_data5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_data5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_data5.Properties.AutoHeight = False
    Me.edTb_data5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_data5.Properties.MaxLength = 65536
    Me.edTb_data5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_data5.Size = New System.Drawing.Size(200, 20)
    Me.edTb_data5.TabIndex = 5
    '
    'edTb_descr10
    '
    Me.edTb_descr10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr10.Location = New System.Drawing.Point(472, 206)
    Me.edTb_descr10.Name = "edTb_descr10"
    Me.edTb_descr10.NTSDbField = ""
    Me.edTb_descr10.NTSForzaVisZoom = False
    Me.edTb_descr10.NTSOldValue = ""
    Me.edTb_descr10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr10.Properties.AutoHeight = False
    Me.edTb_descr10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr10.Properties.MaxLength = 65536
    Me.edTb_descr10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr10.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr10.TabIndex = 5
    '
    'edTb_data2
    '
    Me.edTb_data2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_data2.Location = New System.Drawing.Point(120, 263)
    Me.edTb_data2.Name = "edTb_data2"
    Me.edTb_data2.NTSDbField = ""
    Me.edTb_data2.NTSForzaVisZoom = False
    Me.edTb_data2.NTSOldValue = ""
    Me.edTb_data2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_data2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_data2.Properties.AutoHeight = False
    Me.edTb_data2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_data2.Properties.MaxLength = 65536
    Me.edTb_data2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_data2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_data2.TabIndex = 5
    '
    'edTb_descr5
    '
    Me.edTb_descr5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr5.Location = New System.Drawing.Point(120, 206)
    Me.edTb_descr5.Name = "edTb_descr5"
    Me.edTb_descr5.NTSDbField = ""
    Me.edTb_descr5.NTSForzaVisZoom = False
    Me.edTb_descr5.NTSOldValue = ""
    Me.edTb_descr5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr5.Properties.AutoHeight = False
    Me.edTb_descr5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr5.Properties.MaxLength = 65536
    Me.edTb_descr5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr5.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr5.TabIndex = 5
    '
    'edTb_descr4
    '
    Me.edTb_descr4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr4.Location = New System.Drawing.Point(120, 182)
    Me.edTb_descr4.Name = "edTb_descr4"
    Me.edTb_descr4.NTSDbField = ""
    Me.edTb_descr4.NTSForzaVisZoom = False
    Me.edTb_descr4.NTSOldValue = ""
    Me.edTb_descr4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr4.Properties.AutoHeight = False
    Me.edTb_descr4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr4.Properties.MaxLength = 65536
    Me.edTb_descr4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr4.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr4.TabIndex = 5
    '
    'edTb_descr8
    '
    Me.edTb_descr8.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_descr8.Location = New System.Drawing.Point(472, 158)
    Me.edTb_descr8.Name = "edTb_descr8"
    Me.edTb_descr8.NTSDbField = ""
    Me.edTb_descr8.NTSForzaVisZoom = False
    Me.edTb_descr8.NTSOldValue = ""
    Me.edTb_descr8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr8.Properties.AutoHeight = False
    Me.edTb_descr8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr8.Properties.MaxLength = 65536
    Me.edTb_descr8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr8.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr8.TabIndex = 5
    '
    'edTb_data4
    '
    Me.edTb_data4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_data4.Location = New System.Drawing.Point(472, 239)
    Me.edTb_data4.Name = "edTb_data4"
    Me.edTb_data4.NTSDbField = ""
    Me.edTb_data4.NTSForzaVisZoom = False
    Me.edTb_data4.NTSOldValue = ""
    Me.edTb_data4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_data4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_data4.Properties.AutoHeight = False
    Me.edTb_data4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_data4.Properties.MaxLength = 65536
    Me.edTb_data4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_data4.Size = New System.Drawing.Size(200, 20)
    Me.edTb_data4.TabIndex = 5
    '
    'edTb_descr2
    '
    Me.edTb_descr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr2.Location = New System.Drawing.Point(120, 134)
    Me.edTb_descr2.Name = "edTb_descr2"
    Me.edTb_descr2.NTSDbField = ""
    Me.edTb_descr2.NTSForzaVisZoom = False
    Me.edTb_descr2.NTSOldValue = ""
    Me.edTb_descr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr2.Properties.AutoHeight = False
    Me.edTb_descr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr2.Properties.MaxLength = 65536
    Me.edTb_descr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr2.TabIndex = 5
    '
    'edTb_data1
    '
    Me.edTb_data1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_data1.Location = New System.Drawing.Point(120, 239)
    Me.edTb_data1.Name = "edTb_data1"
    Me.edTb_data1.NTSDbField = ""
    Me.edTb_data1.NTSForzaVisZoom = False
    Me.edTb_data1.NTSOldValue = ""
    Me.edTb_data1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_data1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_data1.Properties.AutoHeight = False
    Me.edTb_data1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_data1.Properties.MaxLength = 65536
    Me.edTb_data1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_data1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_data1.TabIndex = 5
    '
    'edTb_descr9
    '
    Me.edTb_descr9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr9.Location = New System.Drawing.Point(472, 182)
    Me.edTb_descr9.Name = "edTb_descr9"
    Me.edTb_descr9.NTSDbField = ""
    Me.edTb_descr9.NTSForzaVisZoom = False
    Me.edTb_descr9.NTSOldValue = ""
    Me.edTb_descr9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr9.Properties.AutoHeight = False
    Me.edTb_descr9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr9.Properties.MaxLength = 65536
    Me.edTb_descr9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr9.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr9.TabIndex = 5
    '
    'edTb_descr3
    '
    Me.edTb_descr3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr3.Location = New System.Drawing.Point(120, 158)
    Me.edTb_descr3.Name = "edTb_descr3"
    Me.edTb_descr3.NTSDbField = ""
    Me.edTb_descr3.NTSForzaVisZoom = False
    Me.edTb_descr3.NTSOldValue = ""
    Me.edTb_descr3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr3.Properties.AutoHeight = False
    Me.edTb_descr3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr3.Properties.MaxLength = 65536
    Me.edTb_descr3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr3.TabIndex = 5
    '
    'edTb_descr7
    '
    Me.edTb_descr7.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_descr7.Location = New System.Drawing.Point(472, 134)
    Me.edTb_descr7.Name = "edTb_descr7"
    Me.edTb_descr7.NTSDbField = ""
    Me.edTb_descr7.NTSForzaVisZoom = False
    Me.edTb_descr7.NTSOldValue = ""
    Me.edTb_descr7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr7.Properties.AutoHeight = False
    Me.edTb_descr7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr7.Properties.MaxLength = 65536
    Me.edTb_descr7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr7.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr7.TabIndex = 5
    '
    'edTb_descr1
    '
    Me.edTb_descr1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_descr1.Location = New System.Drawing.Point(120, 110)
    Me.edTb_descr1.Name = "edTb_descr1"
    Me.edTb_descr1.NTSDbField = ""
    Me.edTb_descr1.NTSForzaVisZoom = False
    Me.edTb_descr1.NTSOldValue = ""
    Me.edTb_descr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr1.Properties.AutoHeight = False
    Me.edTb_descr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr1.Properties.MaxLength = 65536
    Me.edTb_descr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr1.TabIndex = 5
    '
    'edTb_descr6
    '
    Me.edTb_descr6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descr6.Location = New System.Drawing.Point(472, 110)
    Me.edTb_descr6.Name = "edTb_descr6"
    Me.edTb_descr6.NTSDbField = ""
    Me.edTb_descr6.NTSForzaVisZoom = False
    Me.edTb_descr6.NTSOldValue = ""
    Me.edTb_descr6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descr6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descr6.Properties.AutoHeight = False
    Me.edTb_descr6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descr6.Properties.MaxLength = 65536
    Me.edTb_descr6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descr6.Size = New System.Drawing.Size(200, 20)
    Me.edTb_descr6.TabIndex = 5
    '
    'edTb_destab2
    '
    Me.edTb_destab2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab2.Location = New System.Drawing.Point(120, 86)
    Me.edTb_destab2.Name = "edTb_destab2"
    Me.edTb_destab2.NTSDbField = ""
    Me.edTb_destab2.NTSForzaVisZoom = False
    Me.edTb_destab2.NTSOldValue = ""
    Me.edTb_destab2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab2.Properties.AutoHeight = False
    Me.edTb_destab2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab2.Properties.MaxLength = 65536
    Me.edTb_destab2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab2.TabIndex = 5
    '
    'lbTb_destab2
    '
    Me.lbTb_destab2.AutoSize = True
    Me.lbTb_destab2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab2.Location = New System.Drawing.Point(9, 89)
    Me.lbTb_destab2.Name = "lbTb_destab2"
    Me.lbTb_destab2.NTSDbField = ""
    Me.lbTb_destab2.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab2.TabIndex = 4
    Me.lbTb_destab2.Text = "Descrizione Tab 2"
    Me.lbTb_destab2.Tooltip = ""
    Me.lbTb_destab2.UseMnemonic = False
    '
    'edTb_desext3
    '
    Me.edTb_desext3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desext3.Location = New System.Drawing.Point(120, 53)
    Me.edTb_desext3.Name = "edTb_desext3"
    Me.edTb_desext3.NTSDbField = ""
    Me.edTb_desext3.NTSForzaVisZoom = False
    Me.edTb_desext3.NTSOldValue = ""
    Me.edTb_desext3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desext3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desext3.Properties.AutoHeight = False
    Me.edTb_desext3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desext3.Properties.MaxLength = 65536
    Me.edTb_desext3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desext3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_desext3.TabIndex = 3
    '
    'edTb_desext2
    '
    Me.edTb_desext2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desext2.Location = New System.Drawing.Point(120, 29)
    Me.edTb_desext2.Name = "edTb_desext2"
    Me.edTb_desext2.NTSDbField = ""
    Me.edTb_desext2.NTSForzaVisZoom = False
    Me.edTb_desext2.NTSOldValue = ""
    Me.edTb_desext2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desext2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desext2.Properties.AutoHeight = False
    Me.edTb_desext2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desext2.Properties.MaxLength = 65536
    Me.edTb_desext2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desext2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_desext2.TabIndex = 3
    '
    'lbTb_desext3
    '
    Me.lbTb_desext3.AutoSize = True
    Me.lbTb_desext3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desext3.Location = New System.Drawing.Point(9, 56)
    Me.lbTb_desext3.Name = "lbTb_desext3"
    Me.lbTb_desext3.NTSDbField = ""
    Me.lbTb_desext3.Size = New System.Drawing.Size(105, 13)
    Me.lbTb_desext3.TabIndex = 2
    Me.lbTb_desext3.Text = "Descrizione estesa 3"
    Me.lbTb_desext3.Tooltip = ""
    Me.lbTb_desext3.UseMnemonic = False
    '
    'lbTb_desext2
    '
    Me.lbTb_desext2.AutoSize = True
    Me.lbTb_desext2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desext2.Location = New System.Drawing.Point(9, 32)
    Me.lbTb_desext2.Name = "lbTb_desext2"
    Me.lbTb_desext2.NTSDbField = ""
    Me.lbTb_desext2.Size = New System.Drawing.Size(105, 13)
    Me.lbTb_desext2.TabIndex = 2
    Me.lbTb_desext2.Text = "Descrizione estesa 2"
    Me.lbTb_desext2.Tooltip = ""
    Me.lbTb_desext2.UseMnemonic = False
    '
    'edTb_desext1
    '
    Me.edTb_desext1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desext1.Location = New System.Drawing.Point(120, 5)
    Me.edTb_desext1.Name = "edTb_desext1"
    Me.edTb_desext1.NTSDbField = ""
    Me.edTb_desext1.NTSForzaVisZoom = False
    Me.edTb_desext1.NTSOldValue = ""
    Me.edTb_desext1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desext1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desext1.Properties.AutoHeight = False
    Me.edTb_desext1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desext1.Properties.MaxLength = 65536
    Me.edTb_desext1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desext1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_desext1.TabIndex = 1
    '
    'lbTb_desext1
    '
    Me.lbTb_desext1.AutoSize = True
    Me.lbTb_desext1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desext1.Location = New System.Drawing.Point(9, 8)
    Me.lbTb_desext1.Name = "lbTb_desext1"
    Me.lbTb_desext1.NTSDbField = ""
    Me.lbTb_desext1.Size = New System.Drawing.Size(105, 13)
    Me.lbTb_desext1.TabIndex = 0
    Me.lbTb_desext1.Text = "Descrizione estesa 1"
    Me.lbTb_desext1.Tooltip = ""
    Me.lbTb_desext1.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage3.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage3.Controls.Add(Me.pnAext3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(685, 352)
    Me.NtsTabPage3.Text = "Pagina 3"
    '
    'pnAext3
    '
    Me.pnAext3.AllowDrop = True
    Me.pnAext3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext3.Appearance.Options.UseBackColor = True
    Me.pnAext3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext3.Controls.Add(Me.lbTb_check10)
    Me.pnAext3.Controls.Add(Me.lbTb_check5)
    Me.pnAext3.Controls.Add(Me.lbTb_check9)
    Me.pnAext3.Controls.Add(Me.lbTb_check4)
    Me.pnAext3.Controls.Add(Me.lbTb_check8)
    Me.pnAext3.Controls.Add(Me.lbTb_check3)
    Me.pnAext3.Controls.Add(Me.lbTb_check7)
    Me.pnAext3.Controls.Add(Me.lbTb_check2)
    Me.pnAext3.Controls.Add(Me.lbTb_check6)
    Me.pnAext3.Controls.Add(Me.lbTb_check1)
    Me.pnAext3.Controls.Add(Me.edTb_check10)
    Me.pnAext3.Controls.Add(Me.edTb_check5)
    Me.pnAext3.Controls.Add(Me.edTb_check4)
    Me.pnAext3.Controls.Add(Me.edTb_check8)
    Me.pnAext3.Controls.Add(Me.edTb_check2)
    Me.pnAext3.Controls.Add(Me.edTb_check9)
    Me.pnAext3.Controls.Add(Me.edTb_check3)
    Me.pnAext3.Controls.Add(Me.edTb_check7)
    Me.pnAext3.Controls.Add(Me.edTb_check1)
    Me.pnAext3.Controls.Add(Me.edTb_check6)
    Me.pnAext3.Controls.Add(Me.edTb_destab6)
    Me.pnAext3.Controls.Add(Me.lbTb_destab6)
    Me.pnAext3.Controls.Add(Me.lbTb_num10)
    Me.pnAext3.Controls.Add(Me.lbTb_num5)
    Me.pnAext3.Controls.Add(Me.lbTb_num9)
    Me.pnAext3.Controls.Add(Me.lbTb_num4)
    Me.pnAext3.Controls.Add(Me.lbTb_num8)
    Me.pnAext3.Controls.Add(Me.lbTb_num3)
    Me.pnAext3.Controls.Add(Me.lbTb_memo2)
    Me.pnAext3.Controls.Add(Me.lbTb_num7)
    Me.pnAext3.Controls.Add(Me.lbTb_num2)
    Me.pnAext3.Controls.Add(Me.lbTb_memo1)
    Me.pnAext3.Controls.Add(Me.lbTb_num6)
    Me.pnAext3.Controls.Add(Me.lbTb_num1)
    Me.pnAext3.Controls.Add(Me.edTb_num10)
    Me.pnAext3.Controls.Add(Me.edTb_num5)
    Me.pnAext3.Controls.Add(Me.edTb_num4)
    Me.pnAext3.Controls.Add(Me.edTb_num8)
    Me.pnAext3.Controls.Add(Me.edTb_num2)
    Me.pnAext3.Controls.Add(Me.edTb_num9)
    Me.pnAext3.Controls.Add(Me.edTb_memo2)
    Me.pnAext3.Controls.Add(Me.edTb_num3)
    Me.pnAext3.Controls.Add(Me.edTb_num7)
    Me.pnAext3.Controls.Add(Me.edTb_memo1)
    Me.pnAext3.Controls.Add(Me.edTb_num1)
    Me.pnAext3.Controls.Add(Me.edTb_num6)
    Me.pnAext3.Controls.Add(Me.edTb_destab3)
    Me.pnAext3.Controls.Add(Me.edTb_destab4)
    Me.pnAext3.Controls.Add(Me.edTb_destab5)
    Me.pnAext3.Controls.Add(Me.lbTb_destab3)
    Me.pnAext3.Controls.Add(Me.lbTb_destab4)
    Me.pnAext3.Controls.Add(Me.lbTb_destab5)
    Me.pnAext3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext3.Location = New System.Drawing.Point(0, 0)
    Me.pnAext3.Name = "pnAext3"
    Me.pnAext3.NTSActiveTrasparency = True
    Me.pnAext3.Size = New System.Drawing.Size(685, 352)
    Me.pnAext3.TabIndex = 5
    Me.pnAext3.Text = "NtsPanel1"
    '
    'lbTb_check10
    '
    Me.lbTb_check10.AutoSize = True
    Me.lbTb_check10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check10.Location = New System.Drawing.Point(359, 329)
    Me.lbTb_check10.Name = "lbTb_check10"
    Me.lbTb_check10.NTSDbField = ""
    Me.lbTb_check10.Size = New System.Drawing.Size(85, 13)
    Me.lbTb_check10.TabIndex = 45
    Me.lbTb_check10.Text = "Campo check 10"
    Me.lbTb_check10.Tooltip = ""
    Me.lbTb_check10.UseMnemonic = False
    '
    'lbTb_check5
    '
    Me.lbTb_check5.AutoSize = True
    Me.lbTb_check5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check5.Location = New System.Drawing.Point(9, 329)
    Me.lbTb_check5.Name = "lbTb_check5"
    Me.lbTb_check5.NTSDbField = ""
    Me.lbTb_check5.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check5.TabIndex = 46
    Me.lbTb_check5.Text = "Campo check 5"
    Me.lbTb_check5.Tooltip = ""
    Me.lbTb_check5.UseMnemonic = False
    '
    'lbTb_check9
    '
    Me.lbTb_check9.AutoSize = True
    Me.lbTb_check9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check9.Location = New System.Drawing.Point(359, 305)
    Me.lbTb_check9.Name = "lbTb_check9"
    Me.lbTb_check9.NTSDbField = ""
    Me.lbTb_check9.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check9.TabIndex = 44
    Me.lbTb_check9.Text = "Campo check 9"
    Me.lbTb_check9.Tooltip = ""
    Me.lbTb_check9.UseMnemonic = False
    '
    'lbTb_check4
    '
    Me.lbTb_check4.AutoSize = True
    Me.lbTb_check4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check4.Location = New System.Drawing.Point(9, 305)
    Me.lbTb_check4.Name = "lbTb_check4"
    Me.lbTb_check4.NTSDbField = ""
    Me.lbTb_check4.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check4.TabIndex = 42
    Me.lbTb_check4.Text = "Campo check 4"
    Me.lbTb_check4.Tooltip = ""
    Me.lbTb_check4.UseMnemonic = False
    '
    'lbTb_check8
    '
    Me.lbTb_check8.AutoSize = True
    Me.lbTb_check8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check8.Location = New System.Drawing.Point(360, 281)
    Me.lbTb_check8.Name = "lbTb_check8"
    Me.lbTb_check8.NTSDbField = ""
    Me.lbTb_check8.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check8.TabIndex = 43
    Me.lbTb_check8.Text = "Campo check 8"
    Me.lbTb_check8.Tooltip = ""
    Me.lbTb_check8.UseMnemonic = False
    '
    'lbTb_check3
    '
    Me.lbTb_check3.AutoSize = True
    Me.lbTb_check3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check3.Location = New System.Drawing.Point(9, 281)
    Me.lbTb_check3.Name = "lbTb_check3"
    Me.lbTb_check3.NTSDbField = ""
    Me.lbTb_check3.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check3.TabIndex = 50
    Me.lbTb_check3.Text = "Campo check 3"
    Me.lbTb_check3.Tooltip = ""
    Me.lbTb_check3.UseMnemonic = False
    '
    'lbTb_check7
    '
    Me.lbTb_check7.AutoSize = True
    Me.lbTb_check7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check7.Location = New System.Drawing.Point(360, 257)
    Me.lbTb_check7.Name = "lbTb_check7"
    Me.lbTb_check7.NTSDbField = ""
    Me.lbTb_check7.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check7.TabIndex = 51
    Me.lbTb_check7.Text = "Campo check 7"
    Me.lbTb_check7.Tooltip = ""
    Me.lbTb_check7.UseMnemonic = False
    '
    'lbTb_check2
    '
    Me.lbTb_check2.AutoSize = True
    Me.lbTb_check2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check2.Location = New System.Drawing.Point(9, 257)
    Me.lbTb_check2.Name = "lbTb_check2"
    Me.lbTb_check2.NTSDbField = ""
    Me.lbTb_check2.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check2.TabIndex = 49
    Me.lbTb_check2.Text = "Campo check 2"
    Me.lbTb_check2.Tooltip = ""
    Me.lbTb_check2.UseMnemonic = False
    '
    'lbTb_check6
    '
    Me.lbTb_check6.AutoSize = True
    Me.lbTb_check6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check6.Location = New System.Drawing.Point(360, 233)
    Me.lbTb_check6.Name = "lbTb_check6"
    Me.lbTb_check6.NTSDbField = ""
    Me.lbTb_check6.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check6.TabIndex = 47
    Me.lbTb_check6.Text = "Campo check 6"
    Me.lbTb_check6.Tooltip = ""
    Me.lbTb_check6.UseMnemonic = False
    '
    'lbTb_check1
    '
    Me.lbTb_check1.AutoSize = True
    Me.lbTb_check1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_check1.Location = New System.Drawing.Point(9, 233)
    Me.lbTb_check1.Name = "lbTb_check1"
    Me.lbTb_check1.NTSDbField = ""
    Me.lbTb_check1.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_check1.TabIndex = 48
    Me.lbTb_check1.Text = "Campo check 1"
    Me.lbTb_check1.Tooltip = ""
    Me.lbTb_check1.UseMnemonic = False
    '
    'edTb_check10
    '
    Me.edTb_check10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check10.Location = New System.Drawing.Point(473, 326)
    Me.edTb_check10.Name = "edTb_check10"
    Me.edTb_check10.NTSDbField = ""
    Me.edTb_check10.NTSForzaVisZoom = False
    Me.edTb_check10.NTSOldValue = ""
    Me.edTb_check10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check10.Properties.AutoHeight = False
    Me.edTb_check10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check10.Properties.MaxLength = 65536
    Me.edTb_check10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check10.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check10.TabIndex = 41
    '
    'edTb_check5
    '
    Me.edTb_check5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check5.Location = New System.Drawing.Point(120, 326)
    Me.edTb_check5.Name = "edTb_check5"
    Me.edTb_check5.NTSDbField = ""
    Me.edTb_check5.NTSForzaVisZoom = False
    Me.edTb_check5.NTSOldValue = ""
    Me.edTb_check5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check5.Properties.AutoHeight = False
    Me.edTb_check5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check5.Properties.MaxLength = 65536
    Me.edTb_check5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check5.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check5.TabIndex = 40
    '
    'edTb_check4
    '
    Me.edTb_check4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check4.Location = New System.Drawing.Point(120, 302)
    Me.edTb_check4.Name = "edTb_check4"
    Me.edTb_check4.NTSDbField = ""
    Me.edTb_check4.NTSForzaVisZoom = False
    Me.edTb_check4.NTSOldValue = ""
    Me.edTb_check4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check4.Properties.AutoHeight = False
    Me.edTb_check4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check4.Properties.MaxLength = 65536
    Me.edTb_check4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check4.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check4.TabIndex = 33
    '
    'edTb_check8
    '
    Me.edTb_check8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check8.Location = New System.Drawing.Point(473, 278)
    Me.edTb_check8.Name = "edTb_check8"
    Me.edTb_check8.NTSDbField = ""
    Me.edTb_check8.NTSForzaVisZoom = False
    Me.edTb_check8.NTSOldValue = ""
    Me.edTb_check8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check8.Properties.AutoHeight = False
    Me.edTb_check8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check8.Properties.MaxLength = 65536
    Me.edTb_check8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check8.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check8.TabIndex = 34
    '
    'edTb_check2
    '
    Me.edTb_check2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check2.Location = New System.Drawing.Point(120, 254)
    Me.edTb_check2.Name = "edTb_check2"
    Me.edTb_check2.NTSDbField = ""
    Me.edTb_check2.NTSForzaVisZoom = False
    Me.edTb_check2.NTSOldValue = ""
    Me.edTb_check2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check2.Properties.AutoHeight = False
    Me.edTb_check2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check2.Properties.MaxLength = 65536
    Me.edTb_check2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check2.TabIndex = 31
    '
    'edTb_check9
    '
    Me.edTb_check9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check9.Location = New System.Drawing.Point(473, 302)
    Me.edTb_check9.Name = "edTb_check9"
    Me.edTb_check9.NTSDbField = ""
    Me.edTb_check9.NTSForzaVisZoom = False
    Me.edTb_check9.NTSOldValue = ""
    Me.edTb_check9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check9.Properties.AutoHeight = False
    Me.edTb_check9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check9.Properties.MaxLength = 65536
    Me.edTb_check9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check9.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check9.TabIndex = 32
    '
    'edTb_check3
    '
    Me.edTb_check3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check3.Location = New System.Drawing.Point(120, 278)
    Me.edTb_check3.Name = "edTb_check3"
    Me.edTb_check3.NTSDbField = ""
    Me.edTb_check3.NTSForzaVisZoom = False
    Me.edTb_check3.NTSOldValue = ""
    Me.edTb_check3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check3.Properties.AutoHeight = False
    Me.edTb_check3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check3.Properties.MaxLength = 65536
    Me.edTb_check3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check3.TabIndex = 35
    '
    'edTb_check7
    '
    Me.edTb_check7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check7.Location = New System.Drawing.Point(473, 254)
    Me.edTb_check7.Name = "edTb_check7"
    Me.edTb_check7.NTSDbField = ""
    Me.edTb_check7.NTSForzaVisZoom = False
    Me.edTb_check7.NTSOldValue = ""
    Me.edTb_check7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check7.Properties.AutoHeight = False
    Me.edTb_check7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check7.Properties.MaxLength = 65536
    Me.edTb_check7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check7.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check7.TabIndex = 38
    '
    'edTb_check1
    '
    Me.edTb_check1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_check1.Location = New System.Drawing.Point(120, 230)
    Me.edTb_check1.Name = "edTb_check1"
    Me.edTb_check1.NTSDbField = ""
    Me.edTb_check1.NTSForzaVisZoom = False
    Me.edTb_check1.NTSOldValue = ""
    Me.edTb_check1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check1.Properties.AutoHeight = False
    Me.edTb_check1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check1.Properties.MaxLength = 65536
    Me.edTb_check1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check1.TabIndex = 39
    '
    'edTb_check6
    '
    Me.edTb_check6.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_check6.Location = New System.Drawing.Point(473, 230)
    Me.edTb_check6.Name = "edTb_check6"
    Me.edTb_check6.NTSDbField = ""
    Me.edTb_check6.NTSForzaVisZoom = False
    Me.edTb_check6.NTSOldValue = ""
    Me.edTb_check6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_check6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_check6.Properties.AutoHeight = False
    Me.edTb_check6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_check6.Properties.MaxLength = 65536
    Me.edTb_check6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_check6.Size = New System.Drawing.Size(200, 20)
    Me.edTb_check6.TabIndex = 36
    '
    'edTb_destab6
    '
    Me.edTb_destab6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab6.Location = New System.Drawing.Point(120, 206)
    Me.edTb_destab6.Name = "edTb_destab6"
    Me.edTb_destab6.NTSDbField = ""
    Me.edTb_destab6.NTSForzaVisZoom = False
    Me.edTb_destab6.NTSOldValue = ""
    Me.edTb_destab6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab6.Properties.AutoHeight = False
    Me.edTb_destab6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab6.Properties.MaxLength = 65536
    Me.edTb_destab6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab6.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab6.TabIndex = 37
    '
    'lbTb_destab6
    '
    Me.lbTb_destab6.AutoSize = True
    Me.lbTb_destab6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab6.Location = New System.Drawing.Point(9, 209)
    Me.lbTb_destab6.Name = "lbTb_destab6"
    Me.lbTb_destab6.NTSDbField = ""
    Me.lbTb_destab6.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab6.TabIndex = 30
    Me.lbTb_destab6.Text = "Descrizione Tab 6"
    Me.lbTb_destab6.Tooltip = ""
    Me.lbTb_destab6.UseMnemonic = False
    '
    'lbTb_num10
    '
    Me.lbTb_num10.AutoSize = True
    Me.lbTb_num10.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num10.Location = New System.Drawing.Point(359, 176)
    Me.lbTb_num10.Name = "lbTb_num10"
    Me.lbTb_num10.NTSDbField = ""
    Me.lbTb_num10.Size = New System.Drawing.Size(101, 13)
    Me.lbTb_num10.TabIndex = 23
    Me.lbTb_num10.Text = "Campo numerico 10"
    Me.lbTb_num10.Tooltip = ""
    Me.lbTb_num10.UseMnemonic = False
    '
    'lbTb_num5
    '
    Me.lbTb_num5.AutoSize = True
    Me.lbTb_num5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num5.Location = New System.Drawing.Point(9, 176)
    Me.lbTb_num5.Name = "lbTb_num5"
    Me.lbTb_num5.NTSDbField = ""
    Me.lbTb_num5.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num5.TabIndex = 24
    Me.lbTb_num5.Text = "Campo numerico 5"
    Me.lbTb_num5.Tooltip = ""
    Me.lbTb_num5.UseMnemonic = False
    '
    'lbTb_num9
    '
    Me.lbTb_num9.AutoSize = True
    Me.lbTb_num9.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num9.Location = New System.Drawing.Point(359, 152)
    Me.lbTb_num9.Name = "lbTb_num9"
    Me.lbTb_num9.NTSDbField = ""
    Me.lbTb_num9.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num9.TabIndex = 22
    Me.lbTb_num9.Text = "Campo numerico 9"
    Me.lbTb_num9.Tooltip = ""
    Me.lbTb_num9.UseMnemonic = False
    '
    'lbTb_num4
    '
    Me.lbTb_num4.AutoSize = True
    Me.lbTb_num4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num4.Location = New System.Drawing.Point(9, 152)
    Me.lbTb_num4.Name = "lbTb_num4"
    Me.lbTb_num4.NTSDbField = ""
    Me.lbTb_num4.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num4.TabIndex = 20
    Me.lbTb_num4.Text = "Campo numerico 4"
    Me.lbTb_num4.Tooltip = ""
    Me.lbTb_num4.UseMnemonic = False
    '
    'lbTb_num8
    '
    Me.lbTb_num8.AutoSize = True
    Me.lbTb_num8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num8.Location = New System.Drawing.Point(359, 128)
    Me.lbTb_num8.Name = "lbTb_num8"
    Me.lbTb_num8.NTSDbField = ""
    Me.lbTb_num8.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num8.TabIndex = 21
    Me.lbTb_num8.Text = "Campo numerico 8"
    Me.lbTb_num8.Tooltip = ""
    Me.lbTb_num8.UseMnemonic = False
    '
    'lbTb_num3
    '
    Me.lbTb_num3.AutoSize = True
    Me.lbTb_num3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num3.Location = New System.Drawing.Point(9, 128)
    Me.lbTb_num3.Name = "lbTb_num3"
    Me.lbTb_num3.NTSDbField = ""
    Me.lbTb_num3.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num3.TabIndex = 28
    Me.lbTb_num3.Text = "Campo numerico 3"
    Me.lbTb_num3.Tooltip = ""
    Me.lbTb_num3.UseMnemonic = False
    '
    'lbTb_memo2
    '
    Me.lbTb_memo2.AutoSize = True
    Me.lbTb_memo2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_memo2.Location = New System.Drawing.Point(359, 32)
    Me.lbTb_memo2.Name = "lbTb_memo2"
    Me.lbTb_memo2.NTSDbField = ""
    Me.lbTb_memo2.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_memo2.TabIndex = 29
    Me.lbTb_memo2.Text = "Campo memo 2"
    Me.lbTb_memo2.Tooltip = ""
    Me.lbTb_memo2.UseMnemonic = False
    '
    'lbTb_num7
    '
    Me.lbTb_num7.AutoSize = True
    Me.lbTb_num7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num7.Location = New System.Drawing.Point(359, 104)
    Me.lbTb_num7.Name = "lbTb_num7"
    Me.lbTb_num7.NTSDbField = ""
    Me.lbTb_num7.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num7.TabIndex = 29
    Me.lbTb_num7.Text = "Campo numerico 7"
    Me.lbTb_num7.Tooltip = ""
    Me.lbTb_num7.UseMnemonic = False
    '
    'lbTb_num2
    '
    Me.lbTb_num2.AutoSize = True
    Me.lbTb_num2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num2.Location = New System.Drawing.Point(9, 104)
    Me.lbTb_num2.Name = "lbTb_num2"
    Me.lbTb_num2.NTSDbField = ""
    Me.lbTb_num2.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num2.TabIndex = 27
    Me.lbTb_num2.Text = "Campo numerico 2"
    Me.lbTb_num2.Tooltip = ""
    Me.lbTb_num2.UseMnemonic = False
    '
    'lbTb_memo1
    '
    Me.lbTb_memo1.AutoSize = True
    Me.lbTb_memo1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_memo1.Location = New System.Drawing.Point(359, 8)
    Me.lbTb_memo1.Name = "lbTb_memo1"
    Me.lbTb_memo1.NTSDbField = ""
    Me.lbTb_memo1.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_memo1.TabIndex = 25
    Me.lbTb_memo1.Text = "Campo memo 1"
    Me.lbTb_memo1.Tooltip = ""
    Me.lbTb_memo1.UseMnemonic = False
    '
    'lbTb_num6
    '
    Me.lbTb_num6.AutoSize = True
    Me.lbTb_num6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num6.Location = New System.Drawing.Point(359, 80)
    Me.lbTb_num6.Name = "lbTb_num6"
    Me.lbTb_num6.NTSDbField = ""
    Me.lbTb_num6.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num6.TabIndex = 25
    Me.lbTb_num6.Text = "Campo numerico 6"
    Me.lbTb_num6.Tooltip = ""
    Me.lbTb_num6.UseMnemonic = False
    '
    'lbTb_num1
    '
    Me.lbTb_num1.AutoSize = True
    Me.lbTb_num1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_num1.Location = New System.Drawing.Point(9, 80)
    Me.lbTb_num1.Name = "lbTb_num1"
    Me.lbTb_num1.NTSDbField = ""
    Me.lbTb_num1.Size = New System.Drawing.Size(95, 13)
    Me.lbTb_num1.TabIndex = 26
    Me.lbTb_num1.Text = "Campo numerico 1"
    Me.lbTb_num1.Tooltip = ""
    Me.lbTb_num1.UseMnemonic = False
    '
    'edTb_num10
    '
    Me.edTb_num10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num10.Location = New System.Drawing.Point(473, 173)
    Me.edTb_num10.Name = "edTb_num10"
    Me.edTb_num10.NTSDbField = ""
    Me.edTb_num10.NTSForzaVisZoom = False
    Me.edTb_num10.NTSOldValue = ""
    Me.edTb_num10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num10.Properties.AutoHeight = False
    Me.edTb_num10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num10.Properties.MaxLength = 65536
    Me.edTb_num10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num10.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num10.TabIndex = 19
    '
    'edTb_num5
    '
    Me.edTb_num5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num5.Location = New System.Drawing.Point(120, 173)
    Me.edTb_num5.Name = "edTb_num5"
    Me.edTb_num5.NTSDbField = ""
    Me.edTb_num5.NTSForzaVisZoom = False
    Me.edTb_num5.NTSOldValue = ""
    Me.edTb_num5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num5.Properties.AutoHeight = False
    Me.edTb_num5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num5.Properties.MaxLength = 65536
    Me.edTb_num5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num5.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num5.TabIndex = 18
    '
    'edTb_num4
    '
    Me.edTb_num4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num4.Location = New System.Drawing.Point(120, 149)
    Me.edTb_num4.Name = "edTb_num4"
    Me.edTb_num4.NTSDbField = ""
    Me.edTb_num4.NTSForzaVisZoom = False
    Me.edTb_num4.NTSOldValue = ""
    Me.edTb_num4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num4.Properties.AutoHeight = False
    Me.edTb_num4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num4.Properties.MaxLength = 65536
    Me.edTb_num4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num4.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num4.TabIndex = 11
    '
    'edTb_num8
    '
    Me.edTb_num8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num8.Location = New System.Drawing.Point(473, 125)
    Me.edTb_num8.Name = "edTb_num8"
    Me.edTb_num8.NTSDbField = ""
    Me.edTb_num8.NTSForzaVisZoom = False
    Me.edTb_num8.NTSOldValue = ""
    Me.edTb_num8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num8.Properties.AutoHeight = False
    Me.edTb_num8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num8.Properties.MaxLength = 65536
    Me.edTb_num8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num8.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num8.TabIndex = 12
    '
    'edTb_num2
    '
    Me.edTb_num2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num2.Location = New System.Drawing.Point(120, 101)
    Me.edTb_num2.Name = "edTb_num2"
    Me.edTb_num2.NTSDbField = ""
    Me.edTb_num2.NTSForzaVisZoom = False
    Me.edTb_num2.NTSOldValue = ""
    Me.edTb_num2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num2.Properties.AutoHeight = False
    Me.edTb_num2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num2.Properties.MaxLength = 65536
    Me.edTb_num2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num2.TabIndex = 9
    '
    'edTb_num9
    '
    Me.edTb_num9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num9.Location = New System.Drawing.Point(473, 149)
    Me.edTb_num9.Name = "edTb_num9"
    Me.edTb_num9.NTSDbField = ""
    Me.edTb_num9.NTSForzaVisZoom = False
    Me.edTb_num9.NTSOldValue = ""
    Me.edTb_num9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num9.Properties.AutoHeight = False
    Me.edTb_num9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num9.Properties.MaxLength = 65536
    Me.edTb_num9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num9.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num9.TabIndex = 10
    '
    'edTb_memo2
    '
    Me.edTb_memo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_memo2.Location = New System.Drawing.Point(473, 29)
    Me.edTb_memo2.Name = "edTb_memo2"
    Me.edTb_memo2.NTSDbField = ""
    Me.edTb_memo2.NTSForzaVisZoom = False
    Me.edTb_memo2.NTSOldValue = ""
    Me.edTb_memo2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_memo2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_memo2.Properties.AutoHeight = False
    Me.edTb_memo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_memo2.Properties.MaxLength = 65536
    Me.edTb_memo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_memo2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_memo2.TabIndex = 16
    '
    'edTb_num3
    '
    Me.edTb_num3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num3.Location = New System.Drawing.Point(120, 125)
    Me.edTb_num3.Name = "edTb_num3"
    Me.edTb_num3.NTSDbField = ""
    Me.edTb_num3.NTSForzaVisZoom = False
    Me.edTb_num3.NTSOldValue = ""
    Me.edTb_num3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num3.Properties.AutoHeight = False
    Me.edTb_num3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num3.Properties.MaxLength = 65536
    Me.edTb_num3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num3.TabIndex = 13
    '
    'edTb_num7
    '
    Me.edTb_num7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num7.Location = New System.Drawing.Point(473, 101)
    Me.edTb_num7.Name = "edTb_num7"
    Me.edTb_num7.NTSDbField = ""
    Me.edTb_num7.NTSForzaVisZoom = False
    Me.edTb_num7.NTSOldValue = ""
    Me.edTb_num7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num7.Properties.AutoHeight = False
    Me.edTb_num7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num7.Properties.MaxLength = 65536
    Me.edTb_num7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num7.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num7.TabIndex = 16
    '
    'edTb_memo1
    '
    Me.edTb_memo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_memo1.Location = New System.Drawing.Point(473, 5)
    Me.edTb_memo1.Name = "edTb_memo1"
    Me.edTb_memo1.NTSDbField = ""
    Me.edTb_memo1.NTSForzaVisZoom = False
    Me.edTb_memo1.NTSOldValue = ""
    Me.edTb_memo1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_memo1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_memo1.Properties.AutoHeight = False
    Me.edTb_memo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_memo1.Properties.MaxLength = 65536
    Me.edTb_memo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_memo1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_memo1.TabIndex = 14
    '
    'edTb_num1
    '
    Me.edTb_num1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num1.Location = New System.Drawing.Point(120, 77)
    Me.edTb_num1.Name = "edTb_num1"
    Me.edTb_num1.NTSDbField = ""
    Me.edTb_num1.NTSForzaVisZoom = False
    Me.edTb_num1.NTSOldValue = ""
    Me.edTb_num1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num1.Properties.AutoHeight = False
    Me.edTb_num1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num1.Properties.MaxLength = 65536
    Me.edTb_num1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num1.TabIndex = 17
    '
    'edTb_num6
    '
    Me.edTb_num6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_num6.Location = New System.Drawing.Point(473, 77)
    Me.edTb_num6.Name = "edTb_num6"
    Me.edTb_num6.NTSDbField = ""
    Me.edTb_num6.NTSForzaVisZoom = False
    Me.edTb_num6.NTSOldValue = ""
    Me.edTb_num6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_num6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_num6.Properties.AutoHeight = False
    Me.edTb_num6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_num6.Properties.MaxLength = 65536
    Me.edTb_num6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_num6.Size = New System.Drawing.Size(200, 20)
    Me.edTb_num6.TabIndex = 14
    '
    'edTb_destab3
    '
    Me.edTb_destab3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab3.Location = New System.Drawing.Point(120, 5)
    Me.edTb_destab3.Name = "edTb_destab3"
    Me.edTb_destab3.NTSDbField = ""
    Me.edTb_destab3.NTSForzaVisZoom = False
    Me.edTb_destab3.NTSOldValue = ""
    Me.edTb_destab3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab3.Properties.AutoHeight = False
    Me.edTb_destab3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab3.Properties.MaxLength = 65536
    Me.edTb_destab3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab3.TabIndex = 15
    '
    'edTb_destab4
    '
    Me.edTb_destab4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab4.Location = New System.Drawing.Point(120, 29)
    Me.edTb_destab4.Name = "edTb_destab4"
    Me.edTb_destab4.NTSDbField = ""
    Me.edTb_destab4.NTSForzaVisZoom = False
    Me.edTb_destab4.NTSOldValue = ""
    Me.edTb_destab4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab4.Properties.AutoHeight = False
    Me.edTb_destab4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab4.Properties.MaxLength = 65536
    Me.edTb_destab4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab4.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab4.TabIndex = 15
    '
    'edTb_destab5
    '
    Me.edTb_destab5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destab5.Location = New System.Drawing.Point(120, 53)
    Me.edTb_destab5.Name = "edTb_destab5"
    Me.edTb_destab5.NTSDbField = ""
    Me.edTb_destab5.NTSForzaVisZoom = False
    Me.edTb_destab5.NTSOldValue = ""
    Me.edTb_destab5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destab5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destab5.Properties.AutoHeight = False
    Me.edTb_destab5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destab5.Properties.MaxLength = 65536
    Me.edTb_destab5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destab5.Size = New System.Drawing.Size(200, 20)
    Me.edTb_destab5.TabIndex = 15
    '
    'lbTb_destab3
    '
    Me.lbTb_destab3.AutoSize = True
    Me.lbTb_destab3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab3.Location = New System.Drawing.Point(9, 8)
    Me.lbTb_destab3.Name = "lbTb_destab3"
    Me.lbTb_destab3.NTSDbField = ""
    Me.lbTb_destab3.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab3.TabIndex = 8
    Me.lbTb_destab3.Text = "Descrizione Tab 3"
    Me.lbTb_destab3.Tooltip = ""
    Me.lbTb_destab3.UseMnemonic = False
    '
    'lbTb_destab4
    '
    Me.lbTb_destab4.AutoSize = True
    Me.lbTb_destab4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab4.Location = New System.Drawing.Point(9, 32)
    Me.lbTb_destab4.Name = "lbTb_destab4"
    Me.lbTb_destab4.NTSDbField = ""
    Me.lbTb_destab4.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab4.TabIndex = 8
    Me.lbTb_destab4.Text = "Descrizione Tab 4"
    Me.lbTb_destab4.Tooltip = ""
    Me.lbTb_destab4.UseMnemonic = False
    '
    'lbTb_destab5
    '
    Me.lbTb_destab5.AutoSize = True
    Me.lbTb_destab5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destab5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_destab5.Location = New System.Drawing.Point(9, 56)
    Me.lbTb_destab5.Name = "lbTb_destab5"
    Me.lbTb_destab5.NTSDbField = ""
    Me.lbTb_destab5.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_destab5.TabIndex = 8
    Me.lbTb_destab5.Text = "Descrizione Tab 5"
    Me.lbTb_destab5.Tooltip = ""
    Me.lbTb_destab5.UseMnemonic = False
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnAext4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(685, 352)
    Me.NtsTabPage4.Text = "Pagina 4"
    '
    'pnAext4
    '
    Me.pnAext4.AllowDrop = True
    Me.pnAext4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext4.Appearance.Options.UseBackColor = True
    Me.pnAext4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext4.Controls.Add(Me.lbTb_combo2)
    Me.pnAext4.Controls.Add(Me.edTb_combo2)
    Me.pnAext4.Controls.Add(Me.lbTb_combo1)
    Me.pnAext4.Controls.Add(Me.edTb_combo1)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_L)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom2_E)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_E)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_I)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom2_D)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_D)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_H)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom2_C)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_C)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_G)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom2_B)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_B)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_F)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom2_A)
    Me.pnAext4.Controls.Add(Me.lbTb_helpcom1_A)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom2_E)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_L)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom2_D)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_E)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_D)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom2_B)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_H)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_B)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom2_C)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_I)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_C)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom2_A)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_G)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_A)
    Me.pnAext4.Controls.Add(Me.edTb_helpcom1_F)
    Me.pnAext4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext4.Location = New System.Drawing.Point(0, 0)
    Me.pnAext4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAext4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAext4.Name = "pnAext4"
    Me.pnAext4.NTSActiveTrasparency = True
    Me.pnAext4.Size = New System.Drawing.Size(685, 352)
    Me.pnAext4.TabIndex = 1
    Me.pnAext4.Text = "NtsPanel1"
    '
    'lbTb_combo2
    '
    Me.lbTb_combo2.AutoSize = True
    Me.lbTb_combo2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_combo2.Location = New System.Drawing.Point(9, 161)
    Me.lbTb_combo2.Name = "lbTb_combo2"
    Me.lbTb_combo2.NTSDbField = ""
    Me.lbTb_combo2.Size = New System.Drawing.Size(83, 13)
    Me.lbTb_combo2.TabIndex = 95
    Me.lbTb_combo2.Text = "Campo combo 2"
    Me.lbTb_combo2.Tooltip = ""
    Me.lbTb_combo2.UseMnemonic = False
    '
    'edTb_combo2
    '
    Me.edTb_combo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_combo2.Location = New System.Drawing.Point(94, 158)
    Me.edTb_combo2.Name = "edTb_combo2"
    Me.edTb_combo2.NTSDbField = ""
    Me.edTb_combo2.NTSForzaVisZoom = False
    Me.edTb_combo2.NTSOldValue = ""
    Me.edTb_combo2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_combo2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_combo2.Properties.AutoHeight = False
    Me.edTb_combo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_combo2.Properties.MaxLength = 65536
    Me.edTb_combo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_combo2.Size = New System.Drawing.Size(200, 20)
    Me.edTb_combo2.TabIndex = 94
    '
    'lbTb_combo1
    '
    Me.lbTb_combo1.AutoSize = True
    Me.lbTb_combo1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_combo1.Location = New System.Drawing.Point(9, 8)
    Me.lbTb_combo1.Name = "lbTb_combo1"
    Me.lbTb_combo1.NTSDbField = ""
    Me.lbTb_combo1.Size = New System.Drawing.Size(83, 13)
    Me.lbTb_combo1.TabIndex = 73
    Me.lbTb_combo1.Text = "Campo combo 1"
    Me.lbTb_combo1.Tooltip = ""
    Me.lbTb_combo1.UseMnemonic = False
    '
    'edTb_combo1
    '
    Me.edTb_combo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_combo1.Location = New System.Drawing.Point(94, 5)
    Me.edTb_combo1.Name = "edTb_combo1"
    Me.edTb_combo1.NTSDbField = ""
    Me.edTb_combo1.NTSForzaVisZoom = False
    Me.edTb_combo1.NTSOldValue = ""
    Me.edTb_combo1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_combo1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_combo1.Properties.AutoHeight = False
    Me.edTb_combo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_combo1.Properties.MaxLength = 65536
    Me.edTb_combo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_combo1.Size = New System.Drawing.Size(200, 20)
    Me.edTb_combo1.TabIndex = 72
    '
    'lbTb_helpcom1_L
    '
    Me.lbTb_helpcom1_L.AutoSize = True
    Me.lbTb_helpcom1_L.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_L.Location = New System.Drawing.Point(359, 128)
    Me.lbTb_helpcom1_L.Name = "lbTb_helpcom1_L"
    Me.lbTb_helpcom1_L.NTSDbField = ""
    Me.lbTb_helpcom1_L.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom1_L.TabIndex = 65
    Me.lbTb_helpcom1_L.Text = "Descr. valore 10"
    Me.lbTb_helpcom1_L.Tooltip = ""
    Me.lbTb_helpcom1_L.UseMnemonic = False
    '
    'lbTb_helpcom2_E
    '
    Me.lbTb_helpcom2_E.AutoSize = True
    Me.lbTb_helpcom2_E.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom2_E.Location = New System.Drawing.Point(34, 281)
    Me.lbTb_helpcom2_E.Name = "lbTb_helpcom2_E"
    Me.lbTb_helpcom2_E.NTSDbField = ""
    Me.lbTb_helpcom2_E.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom2_E.TabIndex = 66
    Me.lbTb_helpcom2_E.Text = "Descr. valore 5"
    Me.lbTb_helpcom2_E.Tooltip = ""
    Me.lbTb_helpcom2_E.UseMnemonic = False
    '
    'lbTb_helpcom1_E
    '
    Me.lbTb_helpcom1_E.AutoSize = True
    Me.lbTb_helpcom1_E.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_E.Location = New System.Drawing.Point(34, 128)
    Me.lbTb_helpcom1_E.Name = "lbTb_helpcom1_E"
    Me.lbTb_helpcom1_E.NTSDbField = ""
    Me.lbTb_helpcom1_E.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_E.TabIndex = 66
    Me.lbTb_helpcom1_E.Text = "Descr. valore 5"
    Me.lbTb_helpcom1_E.Tooltip = ""
    Me.lbTb_helpcom1_E.UseMnemonic = False
    '
    'lbTb_helpcom1_I
    '
    Me.lbTb_helpcom1_I.AutoSize = True
    Me.lbTb_helpcom1_I.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_I.Location = New System.Drawing.Point(359, 104)
    Me.lbTb_helpcom1_I.Name = "lbTb_helpcom1_I"
    Me.lbTb_helpcom1_I.NTSDbField = ""
    Me.lbTb_helpcom1_I.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_I.TabIndex = 64
    Me.lbTb_helpcom1_I.Text = "Descr. valore 9"
    Me.lbTb_helpcom1_I.Tooltip = ""
    Me.lbTb_helpcom1_I.UseMnemonic = False
    '
    'lbTb_helpcom2_D
    '
    Me.lbTb_helpcom2_D.AutoSize = True
    Me.lbTb_helpcom2_D.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom2_D.Location = New System.Drawing.Point(34, 257)
    Me.lbTb_helpcom2_D.Name = "lbTb_helpcom2_D"
    Me.lbTb_helpcom2_D.NTSDbField = ""
    Me.lbTb_helpcom2_D.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom2_D.TabIndex = 62
    Me.lbTb_helpcom2_D.Text = "Descr. valore 4"
    Me.lbTb_helpcom2_D.Tooltip = ""
    Me.lbTb_helpcom2_D.UseMnemonic = False
    '
    'lbTb_helpcom1_D
    '
    Me.lbTb_helpcom1_D.AutoSize = True
    Me.lbTb_helpcom1_D.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_D.Location = New System.Drawing.Point(34, 104)
    Me.lbTb_helpcom1_D.Name = "lbTb_helpcom1_D"
    Me.lbTb_helpcom1_D.NTSDbField = ""
    Me.lbTb_helpcom1_D.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_D.TabIndex = 62
    Me.lbTb_helpcom1_D.Text = "Descr. valore 4"
    Me.lbTb_helpcom1_D.Tooltip = ""
    Me.lbTb_helpcom1_D.UseMnemonic = False
    '
    'lbTb_helpcom1_H
    '
    Me.lbTb_helpcom1_H.AutoSize = True
    Me.lbTb_helpcom1_H.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_H.Location = New System.Drawing.Point(359, 80)
    Me.lbTb_helpcom1_H.Name = "lbTb_helpcom1_H"
    Me.lbTb_helpcom1_H.NTSDbField = ""
    Me.lbTb_helpcom1_H.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_H.TabIndex = 63
    Me.lbTb_helpcom1_H.Text = "Descr. valore 8"
    Me.lbTb_helpcom1_H.Tooltip = ""
    Me.lbTb_helpcom1_H.UseMnemonic = False
    '
    'lbTb_helpcom2_C
    '
    Me.lbTb_helpcom2_C.AutoSize = True
    Me.lbTb_helpcom2_C.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom2_C.Location = New System.Drawing.Point(34, 233)
    Me.lbTb_helpcom2_C.Name = "lbTb_helpcom2_C"
    Me.lbTb_helpcom2_C.NTSDbField = ""
    Me.lbTb_helpcom2_C.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom2_C.TabIndex = 70
    Me.lbTb_helpcom2_C.Text = "Descr. valore 3"
    Me.lbTb_helpcom2_C.Tooltip = ""
    Me.lbTb_helpcom2_C.UseMnemonic = False
    '
    'lbTb_helpcom1_C
    '
    Me.lbTb_helpcom1_C.AutoSize = True
    Me.lbTb_helpcom1_C.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_C.Location = New System.Drawing.Point(34, 80)
    Me.lbTb_helpcom1_C.Name = "lbTb_helpcom1_C"
    Me.lbTb_helpcom1_C.NTSDbField = ""
    Me.lbTb_helpcom1_C.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_C.TabIndex = 70
    Me.lbTb_helpcom1_C.Text = "Descr. valore 3"
    Me.lbTb_helpcom1_C.Tooltip = ""
    Me.lbTb_helpcom1_C.UseMnemonic = False
    '
    'lbTb_helpcom1_G
    '
    Me.lbTb_helpcom1_G.AutoSize = True
    Me.lbTb_helpcom1_G.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_G.Location = New System.Drawing.Point(359, 56)
    Me.lbTb_helpcom1_G.Name = "lbTb_helpcom1_G"
    Me.lbTb_helpcom1_G.NTSDbField = ""
    Me.lbTb_helpcom1_G.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_G.TabIndex = 71
    Me.lbTb_helpcom1_G.Text = "Descr. valore 7"
    Me.lbTb_helpcom1_G.Tooltip = ""
    Me.lbTb_helpcom1_G.UseMnemonic = False
    '
    'lbTb_helpcom2_B
    '
    Me.lbTb_helpcom2_B.AutoSize = True
    Me.lbTb_helpcom2_B.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom2_B.Location = New System.Drawing.Point(34, 209)
    Me.lbTb_helpcom2_B.Name = "lbTb_helpcom2_B"
    Me.lbTb_helpcom2_B.NTSDbField = ""
    Me.lbTb_helpcom2_B.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom2_B.TabIndex = 69
    Me.lbTb_helpcom2_B.Text = "Descr. valore 2"
    Me.lbTb_helpcom2_B.Tooltip = ""
    Me.lbTb_helpcom2_B.UseMnemonic = False
    '
    'lbTb_helpcom1_B
    '
    Me.lbTb_helpcom1_B.AutoSize = True
    Me.lbTb_helpcom1_B.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_B.Location = New System.Drawing.Point(34, 56)
    Me.lbTb_helpcom1_B.Name = "lbTb_helpcom1_B"
    Me.lbTb_helpcom1_B.NTSDbField = ""
    Me.lbTb_helpcom1_B.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_B.TabIndex = 69
    Me.lbTb_helpcom1_B.Text = "Descr. valore 2"
    Me.lbTb_helpcom1_B.Tooltip = ""
    Me.lbTb_helpcom1_B.UseMnemonic = False
    '
    'lbTb_helpcom1_F
    '
    Me.lbTb_helpcom1_F.AutoSize = True
    Me.lbTb_helpcom1_F.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_F.Location = New System.Drawing.Point(359, 32)
    Me.lbTb_helpcom1_F.Name = "lbTb_helpcom1_F"
    Me.lbTb_helpcom1_F.NTSDbField = ""
    Me.lbTb_helpcom1_F.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_F.TabIndex = 67
    Me.lbTb_helpcom1_F.Text = "Descr. valore 6"
    Me.lbTb_helpcom1_F.Tooltip = ""
    Me.lbTb_helpcom1_F.UseMnemonic = False
    '
    'lbTb_helpcom2_A
    '
    Me.lbTb_helpcom2_A.AutoSize = True
    Me.lbTb_helpcom2_A.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom2_A.Location = New System.Drawing.Point(34, 185)
    Me.lbTb_helpcom2_A.Name = "lbTb_helpcom2_A"
    Me.lbTb_helpcom2_A.NTSDbField = ""
    Me.lbTb_helpcom2_A.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom2_A.TabIndex = 68
    Me.lbTb_helpcom2_A.Text = "Descr. valore 1"
    Me.lbTb_helpcom2_A.Tooltip = ""
    Me.lbTb_helpcom2_A.UseMnemonic = False
    '
    'lbTb_helpcom1_A
    '
    Me.lbTb_helpcom1_A.AutoSize = True
    Me.lbTb_helpcom1_A.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom1_A.Location = New System.Drawing.Point(34, 32)
    Me.lbTb_helpcom1_A.Name = "lbTb_helpcom1_A"
    Me.lbTb_helpcom1_A.NTSDbField = ""
    Me.lbTb_helpcom1_A.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom1_A.TabIndex = 68
    Me.lbTb_helpcom1_A.Text = "Descr. valore 1"
    Me.lbTb_helpcom1_A.Tooltip = ""
    Me.lbTb_helpcom1_A.UseMnemonic = False
    '
    'edTb_helpcom2_E
    '
    Me.edTb_helpcom2_E.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom2_E.Location = New System.Drawing.Point(120, 278)
    Me.edTb_helpcom2_E.Name = "edTb_helpcom2_E"
    Me.edTb_helpcom2_E.NTSDbField = ""
    Me.edTb_helpcom2_E.NTSForzaVisZoom = False
    Me.edTb_helpcom2_E.NTSOldValue = ""
    Me.edTb_helpcom2_E.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom2_E.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom2_E.Properties.AutoHeight = False
    Me.edTb_helpcom2_E.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom2_E.Properties.MaxLength = 65536
    Me.edTb_helpcom2_E.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom2_E.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom2_E.TabIndex = 60
    '
    'edTb_helpcom1_L
    '
    Me.edTb_helpcom1_L.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_L.Location = New System.Drawing.Point(451, 125)
    Me.edTb_helpcom1_L.Name = "edTb_helpcom1_L"
    Me.edTb_helpcom1_L.NTSDbField = ""
    Me.edTb_helpcom1_L.NTSForzaVisZoom = False
    Me.edTb_helpcom1_L.NTSOldValue = ""
    Me.edTb_helpcom1_L.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_L.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_L.Properties.AutoHeight = False
    Me.edTb_helpcom1_L.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_L.Properties.MaxLength = 65536
    Me.edTb_helpcom1_L.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_L.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_L.TabIndex = 61
    '
    'edTb_helpcom2_D
    '
    Me.edTb_helpcom2_D.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom2_D.Location = New System.Drawing.Point(120, 254)
    Me.edTb_helpcom2_D.Name = "edTb_helpcom2_D"
    Me.edTb_helpcom2_D.NTSDbField = ""
    Me.edTb_helpcom2_D.NTSForzaVisZoom = False
    Me.edTb_helpcom2_D.NTSOldValue = ""
    Me.edTb_helpcom2_D.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom2_D.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom2_D.Properties.AutoHeight = False
    Me.edTb_helpcom2_D.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom2_D.Properties.MaxLength = 65536
    Me.edTb_helpcom2_D.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom2_D.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom2_D.TabIndex = 54
    '
    'edTb_helpcom1_E
    '
    Me.edTb_helpcom1_E.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_E.Location = New System.Drawing.Point(120, 125)
    Me.edTb_helpcom1_E.Name = "edTb_helpcom1_E"
    Me.edTb_helpcom1_E.NTSDbField = ""
    Me.edTb_helpcom1_E.NTSForzaVisZoom = False
    Me.edTb_helpcom1_E.NTSOldValue = ""
    Me.edTb_helpcom1_E.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_E.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_E.Properties.AutoHeight = False
    Me.edTb_helpcom1_E.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_E.Properties.MaxLength = 65536
    Me.edTb_helpcom1_E.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_E.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_E.TabIndex = 60
    '
    'edTb_helpcom1_D
    '
    Me.edTb_helpcom1_D.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_D.Location = New System.Drawing.Point(120, 101)
    Me.edTb_helpcom1_D.Name = "edTb_helpcom1_D"
    Me.edTb_helpcom1_D.NTSDbField = ""
    Me.edTb_helpcom1_D.NTSForzaVisZoom = False
    Me.edTb_helpcom1_D.NTSOldValue = ""
    Me.edTb_helpcom1_D.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_D.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_D.Properties.AutoHeight = False
    Me.edTb_helpcom1_D.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_D.Properties.MaxLength = 65536
    Me.edTb_helpcom1_D.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_D.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_D.TabIndex = 54
    '
    'edTb_helpcom2_B
    '
    Me.edTb_helpcom2_B.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom2_B.Location = New System.Drawing.Point(120, 206)
    Me.edTb_helpcom2_B.Name = "edTb_helpcom2_B"
    Me.edTb_helpcom2_B.NTSDbField = ""
    Me.edTb_helpcom2_B.NTSForzaVisZoom = False
    Me.edTb_helpcom2_B.NTSOldValue = ""
    Me.edTb_helpcom2_B.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom2_B.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom2_B.Properties.AutoHeight = False
    Me.edTb_helpcom2_B.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom2_B.Properties.MaxLength = 65536
    Me.edTb_helpcom2_B.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom2_B.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom2_B.TabIndex = 52
    '
    'edTb_helpcom1_H
    '
    Me.edTb_helpcom1_H.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_H.Location = New System.Drawing.Point(451, 77)
    Me.edTb_helpcom1_H.Name = "edTb_helpcom1_H"
    Me.edTb_helpcom1_H.NTSDbField = ""
    Me.edTb_helpcom1_H.NTSForzaVisZoom = False
    Me.edTb_helpcom1_H.NTSOldValue = ""
    Me.edTb_helpcom1_H.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_H.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_H.Properties.AutoHeight = False
    Me.edTb_helpcom1_H.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_H.Properties.MaxLength = 65536
    Me.edTb_helpcom1_H.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_H.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_H.TabIndex = 55
    '
    'edTb_helpcom1_B
    '
    Me.edTb_helpcom1_B.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_B.Location = New System.Drawing.Point(120, 53)
    Me.edTb_helpcom1_B.Name = "edTb_helpcom1_B"
    Me.edTb_helpcom1_B.NTSDbField = ""
    Me.edTb_helpcom1_B.NTSForzaVisZoom = False
    Me.edTb_helpcom1_B.NTSOldValue = ""
    Me.edTb_helpcom1_B.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_B.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_B.Properties.AutoHeight = False
    Me.edTb_helpcom1_B.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_B.Properties.MaxLength = 65536
    Me.edTb_helpcom1_B.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_B.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_B.TabIndex = 52
    '
    'edTb_helpcom2_C
    '
    Me.edTb_helpcom2_C.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom2_C.Location = New System.Drawing.Point(120, 230)
    Me.edTb_helpcom2_C.Name = "edTb_helpcom2_C"
    Me.edTb_helpcom2_C.NTSDbField = ""
    Me.edTb_helpcom2_C.NTSForzaVisZoom = False
    Me.edTb_helpcom2_C.NTSOldValue = ""
    Me.edTb_helpcom2_C.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom2_C.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom2_C.Properties.AutoHeight = False
    Me.edTb_helpcom2_C.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom2_C.Properties.MaxLength = 65536
    Me.edTb_helpcom2_C.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom2_C.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom2_C.TabIndex = 56
    '
    'edTb_helpcom1_I
    '
    Me.edTb_helpcom1_I.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_I.Location = New System.Drawing.Point(451, 101)
    Me.edTb_helpcom1_I.Name = "edTb_helpcom1_I"
    Me.edTb_helpcom1_I.NTSDbField = ""
    Me.edTb_helpcom1_I.NTSForzaVisZoom = False
    Me.edTb_helpcom1_I.NTSOldValue = ""
    Me.edTb_helpcom1_I.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_I.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_I.Properties.AutoHeight = False
    Me.edTb_helpcom1_I.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_I.Properties.MaxLength = 65536
    Me.edTb_helpcom1_I.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_I.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_I.TabIndex = 53
    '
    'edTb_helpcom1_C
    '
    Me.edTb_helpcom1_C.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_C.Location = New System.Drawing.Point(120, 77)
    Me.edTb_helpcom1_C.Name = "edTb_helpcom1_C"
    Me.edTb_helpcom1_C.NTSDbField = ""
    Me.edTb_helpcom1_C.NTSForzaVisZoom = False
    Me.edTb_helpcom1_C.NTSOldValue = ""
    Me.edTb_helpcom1_C.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_C.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_C.Properties.AutoHeight = False
    Me.edTb_helpcom1_C.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_C.Properties.MaxLength = 65536
    Me.edTb_helpcom1_C.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_C.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_C.TabIndex = 56
    '
    'edTb_helpcom2_A
    '
    Me.edTb_helpcom2_A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom2_A.Location = New System.Drawing.Point(120, 182)
    Me.edTb_helpcom2_A.Name = "edTb_helpcom2_A"
    Me.edTb_helpcom2_A.NTSDbField = ""
    Me.edTb_helpcom2_A.NTSForzaVisZoom = False
    Me.edTb_helpcom2_A.NTSOldValue = ""
    Me.edTb_helpcom2_A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom2_A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom2_A.Properties.AutoHeight = False
    Me.edTb_helpcom2_A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom2_A.Properties.MaxLength = 65536
    Me.edTb_helpcom2_A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom2_A.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom2_A.TabIndex = 59
    '
    'edTb_helpcom1_G
    '
    Me.edTb_helpcom1_G.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_helpcom1_G.Location = New System.Drawing.Point(451, 53)
    Me.edTb_helpcom1_G.Name = "edTb_helpcom1_G"
    Me.edTb_helpcom1_G.NTSDbField = ""
    Me.edTb_helpcom1_G.NTSForzaVisZoom = False
    Me.edTb_helpcom1_G.NTSOldValue = ""
    Me.edTb_helpcom1_G.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_G.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_G.Properties.AutoHeight = False
    Me.edTb_helpcom1_G.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_G.Properties.MaxLength = 65536
    Me.edTb_helpcom1_G.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_G.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_G.TabIndex = 58
    '
    'edTb_helpcom1_A
    '
    Me.edTb_helpcom1_A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_A.Location = New System.Drawing.Point(120, 29)
    Me.edTb_helpcom1_A.Name = "edTb_helpcom1_A"
    Me.edTb_helpcom1_A.NTSDbField = ""
    Me.edTb_helpcom1_A.NTSForzaVisZoom = False
    Me.edTb_helpcom1_A.NTSOldValue = ""
    Me.edTb_helpcom1_A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_A.Properties.AutoHeight = False
    Me.edTb_helpcom1_A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_A.Properties.MaxLength = 65536
    Me.edTb_helpcom1_A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_A.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_A.TabIndex = 59
    '
    'edTb_helpcom1_F
    '
    Me.edTb_helpcom1_F.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom1_F.Location = New System.Drawing.Point(451, 29)
    Me.edTb_helpcom1_F.Name = "edTb_helpcom1_F"
    Me.edTb_helpcom1_F.NTSDbField = ""
    Me.edTb_helpcom1_F.NTSForzaVisZoom = False
    Me.edTb_helpcom1_F.NTSOldValue = ""
    Me.edTb_helpcom1_F.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom1_F.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom1_F.Properties.AutoHeight = False
    Me.edTb_helpcom1_F.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom1_F.Properties.MaxLength = 65536
    Me.edTb_helpcom1_F.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom1_F.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom1_F.TabIndex = 57
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnAext5)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(685, 352)
    Me.NtsTabPage5.Text = "Pagina 5"
    '
    'pnAext5
    '
    Me.pnAext5.AllowDrop = True
    Me.pnAext5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext5.Appearance.Options.UseBackColor = True
    Me.pnAext5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext5.Controls.Add(Me.lbTb_combo3)
    Me.pnAext5.Controls.Add(Me.edTb_combo3)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_V)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_L)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_Q)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_E)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_U)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_I)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_P)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_D)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_T)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_H)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_O)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_C)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_S)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_G)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_N)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_B)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_R)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_F)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_V)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_M)
    Me.pnAext5.Controls.Add(Me.lbTb_helpcom3_A)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_Q)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_L)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_E)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_P)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_T)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_D)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_H)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_N)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_U)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_B)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_I)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_O)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_S)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_C)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_G)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_M)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_R)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_A)
    Me.pnAext5.Controls.Add(Me.edTb_helpcom3_F)
    Me.pnAext5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext5.Location = New System.Drawing.Point(0, 0)
    Me.pnAext5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAext5.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAext5.Name = "pnAext5"
    Me.pnAext5.NTSActiveTrasparency = True
    Me.pnAext5.Size = New System.Drawing.Size(685, 352)
    Me.pnAext5.TabIndex = 1
    Me.pnAext5.Text = "NtsPanel1"
    '
    'lbTb_combo3
    '
    Me.lbTb_combo3.AutoSize = True
    Me.lbTb_combo3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_combo3.Location = New System.Drawing.Point(9, 8)
    Me.lbTb_combo3.Name = "lbTb_combo3"
    Me.lbTb_combo3.NTSDbField = ""
    Me.lbTb_combo3.Size = New System.Drawing.Size(83, 13)
    Me.lbTb_combo3.TabIndex = 95
    Me.lbTb_combo3.Text = "Campo combo 3"
    Me.lbTb_combo3.Tooltip = ""
    Me.lbTb_combo3.UseMnemonic = False
    '
    'edTb_combo3
    '
    Me.edTb_combo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_combo3.Location = New System.Drawing.Point(94, 5)
    Me.edTb_combo3.Name = "edTb_combo3"
    Me.edTb_combo3.NTSDbField = ""
    Me.edTb_combo3.NTSForzaVisZoom = False
    Me.edTb_combo3.NTSOldValue = ""
    Me.edTb_combo3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_combo3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_combo3.Properties.AutoHeight = False
    Me.edTb_combo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_combo3.Properties.MaxLength = 65536
    Me.edTb_combo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_combo3.Size = New System.Drawing.Size(200, 20)
    Me.edTb_combo3.TabIndex = 94
    '
    'lbTb_helpcom3_V
    '
    Me.lbTb_helpcom3_V.AutoSize = True
    Me.lbTb_helpcom3_V.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_V.Location = New System.Drawing.Point(371, 248)
    Me.lbTb_helpcom3_V.Name = "lbTb_helpcom3_V"
    Me.lbTb_helpcom3_V.NTSDbField = ""
    Me.lbTb_helpcom3_V.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_V.TabIndex = 87
    Me.lbTb_helpcom3_V.Text = "Descr. valore 20"
    Me.lbTb_helpcom3_V.Tooltip = ""
    Me.lbTb_helpcom3_V.UseMnemonic = False
    '
    'lbTb_helpcom3_L
    '
    Me.lbTb_helpcom3_L.AutoSize = True
    Me.lbTb_helpcom3_L.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_L.Location = New System.Drawing.Point(28, 248)
    Me.lbTb_helpcom3_L.Name = "lbTb_helpcom3_L"
    Me.lbTb_helpcom3_L.NTSDbField = ""
    Me.lbTb_helpcom3_L.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_L.TabIndex = 87
    Me.lbTb_helpcom3_L.Text = "Descr. valore 10"
    Me.lbTb_helpcom3_L.Tooltip = ""
    Me.lbTb_helpcom3_L.UseMnemonic = False
    '
    'lbTb_helpcom3_Q
    '
    Me.lbTb_helpcom3_Q.AutoSize = True
    Me.lbTb_helpcom3_Q.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_Q.Location = New System.Drawing.Point(371, 128)
    Me.lbTb_helpcom3_Q.Name = "lbTb_helpcom3_Q"
    Me.lbTb_helpcom3_Q.NTSDbField = ""
    Me.lbTb_helpcom3_Q.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_Q.TabIndex = 88
    Me.lbTb_helpcom3_Q.Text = "Descr. valore 15"
    Me.lbTb_helpcom3_Q.Tooltip = ""
    Me.lbTb_helpcom3_Q.UseMnemonic = False
    '
    'lbTb_helpcom3_E
    '
    Me.lbTb_helpcom3_E.AutoSize = True
    Me.lbTb_helpcom3_E.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_E.Location = New System.Drawing.Point(28, 128)
    Me.lbTb_helpcom3_E.Name = "lbTb_helpcom3_E"
    Me.lbTb_helpcom3_E.NTSDbField = ""
    Me.lbTb_helpcom3_E.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_E.TabIndex = 88
    Me.lbTb_helpcom3_E.Text = "Descr. valore 5"
    Me.lbTb_helpcom3_E.Tooltip = ""
    Me.lbTb_helpcom3_E.UseMnemonic = False
    '
    'lbTb_helpcom3_U
    '
    Me.lbTb_helpcom3_U.AutoSize = True
    Me.lbTb_helpcom3_U.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_U.Location = New System.Drawing.Point(371, 224)
    Me.lbTb_helpcom3_U.Name = "lbTb_helpcom3_U"
    Me.lbTb_helpcom3_U.NTSDbField = ""
    Me.lbTb_helpcom3_U.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_U.TabIndex = 86
    Me.lbTb_helpcom3_U.Text = "Descr. valore 19"
    Me.lbTb_helpcom3_U.Tooltip = ""
    Me.lbTb_helpcom3_U.UseMnemonic = False
    '
    'lbTb_helpcom3_I
    '
    Me.lbTb_helpcom3_I.AutoSize = True
    Me.lbTb_helpcom3_I.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_I.Location = New System.Drawing.Point(28, 224)
    Me.lbTb_helpcom3_I.Name = "lbTb_helpcom3_I"
    Me.lbTb_helpcom3_I.NTSDbField = ""
    Me.lbTb_helpcom3_I.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_I.TabIndex = 86
    Me.lbTb_helpcom3_I.Text = "Descr. valore 9"
    Me.lbTb_helpcom3_I.Tooltip = ""
    Me.lbTb_helpcom3_I.UseMnemonic = False
    '
    'lbTb_helpcom3_P
    '
    Me.lbTb_helpcom3_P.AutoSize = True
    Me.lbTb_helpcom3_P.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_P.Location = New System.Drawing.Point(371, 104)
    Me.lbTb_helpcom3_P.Name = "lbTb_helpcom3_P"
    Me.lbTb_helpcom3_P.NTSDbField = ""
    Me.lbTb_helpcom3_P.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_P.TabIndex = 84
    Me.lbTb_helpcom3_P.Text = "Descr. valore 14"
    Me.lbTb_helpcom3_P.Tooltip = ""
    Me.lbTb_helpcom3_P.UseMnemonic = False
    '
    'lbTb_helpcom3_D
    '
    Me.lbTb_helpcom3_D.AutoSize = True
    Me.lbTb_helpcom3_D.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_D.Location = New System.Drawing.Point(28, 104)
    Me.lbTb_helpcom3_D.Name = "lbTb_helpcom3_D"
    Me.lbTb_helpcom3_D.NTSDbField = ""
    Me.lbTb_helpcom3_D.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_D.TabIndex = 84
    Me.lbTb_helpcom3_D.Text = "Descr. valore 4"
    Me.lbTb_helpcom3_D.Tooltip = ""
    Me.lbTb_helpcom3_D.UseMnemonic = False
    '
    'lbTb_helpcom3_T
    '
    Me.lbTb_helpcom3_T.AutoSize = True
    Me.lbTb_helpcom3_T.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_T.Location = New System.Drawing.Point(371, 200)
    Me.lbTb_helpcom3_T.Name = "lbTb_helpcom3_T"
    Me.lbTb_helpcom3_T.NTSDbField = ""
    Me.lbTb_helpcom3_T.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_T.TabIndex = 85
    Me.lbTb_helpcom3_T.Text = "Descr. valore 18"
    Me.lbTb_helpcom3_T.Tooltip = ""
    Me.lbTb_helpcom3_T.UseMnemonic = False
    '
    'lbTb_helpcom3_H
    '
    Me.lbTb_helpcom3_H.AutoSize = True
    Me.lbTb_helpcom3_H.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_H.Location = New System.Drawing.Point(28, 200)
    Me.lbTb_helpcom3_H.Name = "lbTb_helpcom3_H"
    Me.lbTb_helpcom3_H.NTSDbField = ""
    Me.lbTb_helpcom3_H.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_H.TabIndex = 85
    Me.lbTb_helpcom3_H.Text = "Descr. valore 8"
    Me.lbTb_helpcom3_H.Tooltip = ""
    Me.lbTb_helpcom3_H.UseMnemonic = False
    '
    'lbTb_helpcom3_O
    '
    Me.lbTb_helpcom3_O.AutoSize = True
    Me.lbTb_helpcom3_O.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_O.Location = New System.Drawing.Point(371, 80)
    Me.lbTb_helpcom3_O.Name = "lbTb_helpcom3_O"
    Me.lbTb_helpcom3_O.NTSDbField = ""
    Me.lbTb_helpcom3_O.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_O.TabIndex = 92
    Me.lbTb_helpcom3_O.Text = "Descr. valore 13"
    Me.lbTb_helpcom3_O.Tooltip = ""
    Me.lbTb_helpcom3_O.UseMnemonic = False
    '
    'lbTb_helpcom3_C
    '
    Me.lbTb_helpcom3_C.AutoSize = True
    Me.lbTb_helpcom3_C.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_C.Location = New System.Drawing.Point(28, 80)
    Me.lbTb_helpcom3_C.Name = "lbTb_helpcom3_C"
    Me.lbTb_helpcom3_C.NTSDbField = ""
    Me.lbTb_helpcom3_C.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_C.TabIndex = 92
    Me.lbTb_helpcom3_C.Text = "Descr. valore 3"
    Me.lbTb_helpcom3_C.Tooltip = ""
    Me.lbTb_helpcom3_C.UseMnemonic = False
    '
    'lbTb_helpcom3_S
    '
    Me.lbTb_helpcom3_S.AutoSize = True
    Me.lbTb_helpcom3_S.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_S.Location = New System.Drawing.Point(371, 176)
    Me.lbTb_helpcom3_S.Name = "lbTb_helpcom3_S"
    Me.lbTb_helpcom3_S.NTSDbField = ""
    Me.lbTb_helpcom3_S.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_S.TabIndex = 93
    Me.lbTb_helpcom3_S.Text = "Descr. valore 17"
    Me.lbTb_helpcom3_S.Tooltip = ""
    Me.lbTb_helpcom3_S.UseMnemonic = False
    '
    'lbTb_helpcom3_G
    '
    Me.lbTb_helpcom3_G.AutoSize = True
    Me.lbTb_helpcom3_G.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_G.Location = New System.Drawing.Point(28, 176)
    Me.lbTb_helpcom3_G.Name = "lbTb_helpcom3_G"
    Me.lbTb_helpcom3_G.NTSDbField = ""
    Me.lbTb_helpcom3_G.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_G.TabIndex = 93
    Me.lbTb_helpcom3_G.Text = "Descr. valore 7"
    Me.lbTb_helpcom3_G.Tooltip = ""
    Me.lbTb_helpcom3_G.UseMnemonic = False
    '
    'lbTb_helpcom3_N
    '
    Me.lbTb_helpcom3_N.AutoSize = True
    Me.lbTb_helpcom3_N.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_N.Location = New System.Drawing.Point(371, 56)
    Me.lbTb_helpcom3_N.Name = "lbTb_helpcom3_N"
    Me.lbTb_helpcom3_N.NTSDbField = ""
    Me.lbTb_helpcom3_N.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_N.TabIndex = 91
    Me.lbTb_helpcom3_N.Text = "Descr. valore 12"
    Me.lbTb_helpcom3_N.Tooltip = ""
    Me.lbTb_helpcom3_N.UseMnemonic = False
    '
    'lbTb_helpcom3_B
    '
    Me.lbTb_helpcom3_B.AutoSize = True
    Me.lbTb_helpcom3_B.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_B.Location = New System.Drawing.Point(28, 56)
    Me.lbTb_helpcom3_B.Name = "lbTb_helpcom3_B"
    Me.lbTb_helpcom3_B.NTSDbField = ""
    Me.lbTb_helpcom3_B.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_B.TabIndex = 91
    Me.lbTb_helpcom3_B.Text = "Descr. valore 2"
    Me.lbTb_helpcom3_B.Tooltip = ""
    Me.lbTb_helpcom3_B.UseMnemonic = False
    '
    'lbTb_helpcom3_R
    '
    Me.lbTb_helpcom3_R.AutoSize = True
    Me.lbTb_helpcom3_R.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_R.Location = New System.Drawing.Point(371, 152)
    Me.lbTb_helpcom3_R.Name = "lbTb_helpcom3_R"
    Me.lbTb_helpcom3_R.NTSDbField = ""
    Me.lbTb_helpcom3_R.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_R.TabIndex = 89
    Me.lbTb_helpcom3_R.Text = "Descr. valore 16"
    Me.lbTb_helpcom3_R.Tooltip = ""
    Me.lbTb_helpcom3_R.UseMnemonic = False
    '
    'lbTb_helpcom3_F
    '
    Me.lbTb_helpcom3_F.AutoSize = True
    Me.lbTb_helpcom3_F.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_F.Location = New System.Drawing.Point(28, 152)
    Me.lbTb_helpcom3_F.Name = "lbTb_helpcom3_F"
    Me.lbTb_helpcom3_F.NTSDbField = ""
    Me.lbTb_helpcom3_F.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_F.TabIndex = 89
    Me.lbTb_helpcom3_F.Text = "Descr. valore 6"
    Me.lbTb_helpcom3_F.Tooltip = ""
    Me.lbTb_helpcom3_F.UseMnemonic = False
    '
    'edTb_helpcom3_V
    '
    Me.edTb_helpcom3_V.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_V.Location = New System.Drawing.Point(463, 245)
    Me.edTb_helpcom3_V.Name = "edTb_helpcom3_V"
    Me.edTb_helpcom3_V.NTSDbField = ""
    Me.edTb_helpcom3_V.NTSForzaVisZoom = False
    Me.edTb_helpcom3_V.NTSOldValue = ""
    Me.edTb_helpcom3_V.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_V.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_V.Properties.AutoHeight = False
    Me.edTb_helpcom3_V.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_V.Properties.MaxLength = 65536
    Me.edTb_helpcom3_V.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_V.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_V.TabIndex = 83
    '
    'lbTb_helpcom3_M
    '
    Me.lbTb_helpcom3_M.AutoSize = True
    Me.lbTb_helpcom3_M.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_M.Location = New System.Drawing.Point(371, 32)
    Me.lbTb_helpcom3_M.Name = "lbTb_helpcom3_M"
    Me.lbTb_helpcom3_M.NTSDbField = ""
    Me.lbTb_helpcom3_M.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_helpcom3_M.TabIndex = 90
    Me.lbTb_helpcom3_M.Text = "Descr. valore 11"
    Me.lbTb_helpcom3_M.Tooltip = ""
    Me.lbTb_helpcom3_M.UseMnemonic = False
    '
    'lbTb_helpcom3_A
    '
    Me.lbTb_helpcom3_A.AutoSize = True
    Me.lbTb_helpcom3_A.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_helpcom3_A.Location = New System.Drawing.Point(28, 32)
    Me.lbTb_helpcom3_A.Name = "lbTb_helpcom3_A"
    Me.lbTb_helpcom3_A.NTSDbField = ""
    Me.lbTb_helpcom3_A.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_helpcom3_A.TabIndex = 90
    Me.lbTb_helpcom3_A.Text = "Descr. valore 1"
    Me.lbTb_helpcom3_A.Tooltip = ""
    Me.lbTb_helpcom3_A.UseMnemonic = False
    '
    'edTb_helpcom3_Q
    '
    Me.edTb_helpcom3_Q.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_Q.Location = New System.Drawing.Point(463, 125)
    Me.edTb_helpcom3_Q.Name = "edTb_helpcom3_Q"
    Me.edTb_helpcom3_Q.NTSDbField = ""
    Me.edTb_helpcom3_Q.NTSForzaVisZoom = False
    Me.edTb_helpcom3_Q.NTSOldValue = ""
    Me.edTb_helpcom3_Q.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_Q.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_Q.Properties.AutoHeight = False
    Me.edTb_helpcom3_Q.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_Q.Properties.MaxLength = 65536
    Me.edTb_helpcom3_Q.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_Q.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_Q.TabIndex = 82
    '
    'edTb_helpcom3_L
    '
    Me.edTb_helpcom3_L.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_L.Location = New System.Drawing.Point(120, 245)
    Me.edTb_helpcom3_L.Name = "edTb_helpcom3_L"
    Me.edTb_helpcom3_L.NTSDbField = ""
    Me.edTb_helpcom3_L.NTSForzaVisZoom = False
    Me.edTb_helpcom3_L.NTSOldValue = ""
    Me.edTb_helpcom3_L.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_L.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_L.Properties.AutoHeight = False
    Me.edTb_helpcom3_L.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_L.Properties.MaxLength = 65536
    Me.edTb_helpcom3_L.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_L.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_L.TabIndex = 83
    '
    'edTb_helpcom3_E
    '
    Me.edTb_helpcom3_E.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_E.Location = New System.Drawing.Point(120, 125)
    Me.edTb_helpcom3_E.Name = "edTb_helpcom3_E"
    Me.edTb_helpcom3_E.NTSDbField = ""
    Me.edTb_helpcom3_E.NTSForzaVisZoom = False
    Me.edTb_helpcom3_E.NTSOldValue = ""
    Me.edTb_helpcom3_E.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_E.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_E.Properties.AutoHeight = False
    Me.edTb_helpcom3_E.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_E.Properties.MaxLength = 65536
    Me.edTb_helpcom3_E.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_E.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_E.TabIndex = 82
    '
    'edTb_helpcom3_P
    '
    Me.edTb_helpcom3_P.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_P.Location = New System.Drawing.Point(463, 101)
    Me.edTb_helpcom3_P.Name = "edTb_helpcom3_P"
    Me.edTb_helpcom3_P.NTSDbField = ""
    Me.edTb_helpcom3_P.NTSForzaVisZoom = False
    Me.edTb_helpcom3_P.NTSOldValue = ""
    Me.edTb_helpcom3_P.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_P.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_P.Properties.AutoHeight = False
    Me.edTb_helpcom3_P.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_P.Properties.MaxLength = 65536
    Me.edTb_helpcom3_P.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_P.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_P.TabIndex = 76
    '
    'edTb_helpcom3_T
    '
    Me.edTb_helpcom3_T.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_T.Location = New System.Drawing.Point(463, 197)
    Me.edTb_helpcom3_T.Name = "edTb_helpcom3_T"
    Me.edTb_helpcom3_T.NTSDbField = ""
    Me.edTb_helpcom3_T.NTSForzaVisZoom = False
    Me.edTb_helpcom3_T.NTSOldValue = ""
    Me.edTb_helpcom3_T.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_T.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_T.Properties.AutoHeight = False
    Me.edTb_helpcom3_T.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_T.Properties.MaxLength = 65536
    Me.edTb_helpcom3_T.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_T.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_T.TabIndex = 77
    '
    'edTb_helpcom3_D
    '
    Me.edTb_helpcom3_D.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_D.Location = New System.Drawing.Point(120, 101)
    Me.edTb_helpcom3_D.Name = "edTb_helpcom3_D"
    Me.edTb_helpcom3_D.NTSDbField = ""
    Me.edTb_helpcom3_D.NTSForzaVisZoom = False
    Me.edTb_helpcom3_D.NTSOldValue = ""
    Me.edTb_helpcom3_D.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_D.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_D.Properties.AutoHeight = False
    Me.edTb_helpcom3_D.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_D.Properties.MaxLength = 65536
    Me.edTb_helpcom3_D.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_D.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_D.TabIndex = 76
    '
    'edTb_helpcom3_H
    '
    Me.edTb_helpcom3_H.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_H.Location = New System.Drawing.Point(120, 197)
    Me.edTb_helpcom3_H.Name = "edTb_helpcom3_H"
    Me.edTb_helpcom3_H.NTSDbField = ""
    Me.edTb_helpcom3_H.NTSForzaVisZoom = False
    Me.edTb_helpcom3_H.NTSOldValue = ""
    Me.edTb_helpcom3_H.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_H.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_H.Properties.AutoHeight = False
    Me.edTb_helpcom3_H.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_H.Properties.MaxLength = 65536
    Me.edTb_helpcom3_H.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_H.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_H.TabIndex = 77
    '
    'edTb_helpcom3_N
    '
    Me.edTb_helpcom3_N.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_N.Location = New System.Drawing.Point(463, 53)
    Me.edTb_helpcom3_N.Name = "edTb_helpcom3_N"
    Me.edTb_helpcom3_N.NTSDbField = ""
    Me.edTb_helpcom3_N.NTSForzaVisZoom = False
    Me.edTb_helpcom3_N.NTSOldValue = ""
    Me.edTb_helpcom3_N.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_N.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_N.Properties.AutoHeight = False
    Me.edTb_helpcom3_N.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_N.Properties.MaxLength = 65536
    Me.edTb_helpcom3_N.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_N.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_N.TabIndex = 74
    '
    'edTb_helpcom3_U
    '
    Me.edTb_helpcom3_U.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_U.Location = New System.Drawing.Point(463, 221)
    Me.edTb_helpcom3_U.Name = "edTb_helpcom3_U"
    Me.edTb_helpcom3_U.NTSDbField = ""
    Me.edTb_helpcom3_U.NTSForzaVisZoom = False
    Me.edTb_helpcom3_U.NTSOldValue = ""
    Me.edTb_helpcom3_U.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_U.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_U.Properties.AutoHeight = False
    Me.edTb_helpcom3_U.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_U.Properties.MaxLength = 65536
    Me.edTb_helpcom3_U.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_U.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_U.TabIndex = 75
    '
    'edTb_helpcom3_B
    '
    Me.edTb_helpcom3_B.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_B.Location = New System.Drawing.Point(120, 53)
    Me.edTb_helpcom3_B.Name = "edTb_helpcom3_B"
    Me.edTb_helpcom3_B.NTSDbField = ""
    Me.edTb_helpcom3_B.NTSForzaVisZoom = False
    Me.edTb_helpcom3_B.NTSOldValue = ""
    Me.edTb_helpcom3_B.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_B.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_B.Properties.AutoHeight = False
    Me.edTb_helpcom3_B.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_B.Properties.MaxLength = 65536
    Me.edTb_helpcom3_B.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_B.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_B.TabIndex = 74
    '
    'edTb_helpcom3_I
    '
    Me.edTb_helpcom3_I.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_I.Location = New System.Drawing.Point(120, 221)
    Me.edTb_helpcom3_I.Name = "edTb_helpcom3_I"
    Me.edTb_helpcom3_I.NTSDbField = ""
    Me.edTb_helpcom3_I.NTSForzaVisZoom = False
    Me.edTb_helpcom3_I.NTSOldValue = ""
    Me.edTb_helpcom3_I.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_I.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_I.Properties.AutoHeight = False
    Me.edTb_helpcom3_I.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_I.Properties.MaxLength = 65536
    Me.edTb_helpcom3_I.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_I.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_I.TabIndex = 75
    '
    'edTb_helpcom3_O
    '
    Me.edTb_helpcom3_O.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_O.Location = New System.Drawing.Point(463, 77)
    Me.edTb_helpcom3_O.Name = "edTb_helpcom3_O"
    Me.edTb_helpcom3_O.NTSDbField = ""
    Me.edTb_helpcom3_O.NTSForzaVisZoom = False
    Me.edTb_helpcom3_O.NTSOldValue = ""
    Me.edTb_helpcom3_O.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_O.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_O.Properties.AutoHeight = False
    Me.edTb_helpcom3_O.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_O.Properties.MaxLength = 65536
    Me.edTb_helpcom3_O.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_O.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_O.TabIndex = 78
    '
    'edTb_helpcom3_S
    '
    Me.edTb_helpcom3_S.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_S.Location = New System.Drawing.Point(463, 173)
    Me.edTb_helpcom3_S.Name = "edTb_helpcom3_S"
    Me.edTb_helpcom3_S.NTSDbField = ""
    Me.edTb_helpcom3_S.NTSForzaVisZoom = False
    Me.edTb_helpcom3_S.NTSOldValue = ""
    Me.edTb_helpcom3_S.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_S.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_S.Properties.AutoHeight = False
    Me.edTb_helpcom3_S.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_S.Properties.MaxLength = 65536
    Me.edTb_helpcom3_S.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_S.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_S.TabIndex = 80
    '
    'edTb_helpcom3_C
    '
    Me.edTb_helpcom3_C.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_C.Location = New System.Drawing.Point(120, 77)
    Me.edTb_helpcom3_C.Name = "edTb_helpcom3_C"
    Me.edTb_helpcom3_C.NTSDbField = ""
    Me.edTb_helpcom3_C.NTSForzaVisZoom = False
    Me.edTb_helpcom3_C.NTSOldValue = ""
    Me.edTb_helpcom3_C.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_C.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_C.Properties.AutoHeight = False
    Me.edTb_helpcom3_C.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_C.Properties.MaxLength = 65536
    Me.edTb_helpcom3_C.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_C.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_C.TabIndex = 78
    '
    'edTb_helpcom3_G
    '
    Me.edTb_helpcom3_G.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_G.Location = New System.Drawing.Point(120, 173)
    Me.edTb_helpcom3_G.Name = "edTb_helpcom3_G"
    Me.edTb_helpcom3_G.NTSDbField = ""
    Me.edTb_helpcom3_G.NTSForzaVisZoom = False
    Me.edTb_helpcom3_G.NTSOldValue = ""
    Me.edTb_helpcom3_G.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_G.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_G.Properties.AutoHeight = False
    Me.edTb_helpcom3_G.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_G.Properties.MaxLength = 65536
    Me.edTb_helpcom3_G.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_G.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_G.TabIndex = 80
    '
    'edTb_helpcom3_M
    '
    Me.edTb_helpcom3_M.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_M.Location = New System.Drawing.Point(463, 29)
    Me.edTb_helpcom3_M.Name = "edTb_helpcom3_M"
    Me.edTb_helpcom3_M.NTSDbField = ""
    Me.edTb_helpcom3_M.NTSForzaVisZoom = False
    Me.edTb_helpcom3_M.NTSOldValue = ""
    Me.edTb_helpcom3_M.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_M.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_M.Properties.AutoHeight = False
    Me.edTb_helpcom3_M.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_M.Properties.MaxLength = 65536
    Me.edTb_helpcom3_M.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_M.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_M.TabIndex = 81
    '
    'edTb_helpcom3_R
    '
    Me.edTb_helpcom3_R.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_R.Location = New System.Drawing.Point(463, 149)
    Me.edTb_helpcom3_R.Name = "edTb_helpcom3_R"
    Me.edTb_helpcom3_R.NTSDbField = ""
    Me.edTb_helpcom3_R.NTSForzaVisZoom = False
    Me.edTb_helpcom3_R.NTSOldValue = ""
    Me.edTb_helpcom3_R.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_R.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_R.Properties.AutoHeight = False
    Me.edTb_helpcom3_R.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_R.Properties.MaxLength = 65536
    Me.edTb_helpcom3_R.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_R.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_R.TabIndex = 79
    '
    'edTb_helpcom3_A
    '
    Me.edTb_helpcom3_A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_A.Location = New System.Drawing.Point(120, 29)
    Me.edTb_helpcom3_A.Name = "edTb_helpcom3_A"
    Me.edTb_helpcom3_A.NTSDbField = ""
    Me.edTb_helpcom3_A.NTSForzaVisZoom = False
    Me.edTb_helpcom3_A.NTSOldValue = ""
    Me.edTb_helpcom3_A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_A.Properties.AutoHeight = False
    Me.edTb_helpcom3_A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_A.Properties.MaxLength = 65536
    Me.edTb_helpcom3_A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_A.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_A.TabIndex = 81
    '
    'edTb_helpcom3_F
    '
    Me.edTb_helpcom3_F.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_helpcom3_F.Location = New System.Drawing.Point(120, 149)
    Me.edTb_helpcom3_F.Name = "edTb_helpcom3_F"
    Me.edTb_helpcom3_F.NTSDbField = ""
    Me.edTb_helpcom3_F.NTSForzaVisZoom = False
    Me.edTb_helpcom3_F.NTSOldValue = ""
    Me.edTb_helpcom3_F.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_helpcom3_F.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_helpcom3_F.Properties.AutoHeight = False
    Me.edTb_helpcom3_F.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_helpcom3_F.Properties.MaxLength = 65536
    Me.edTb_helpcom3_F.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_helpcom3_F.Size = New System.Drawing.Size(200, 20)
    Me.edTb_helpcom3_F.TabIndex = 79
    '
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTipork.Location = New System.Drawing.Point(3, 2)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(42, 13)
    Me.lbTipork.TabIndex = 0
    Me.lbTipork.Text = "Clienti"
    Me.lbTipork.Tooltip = ""
    Me.lbTipork.UseMnemonic = False
    '
    'pnAext
    '
    Me.pnAext.AllowDrop = True
    Me.pnAext.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAext.Appearance.Options.UseBackColor = True
    Me.pnAext.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAext.Controls.Add(Me.tabAext)
    Me.pnAext.Controls.Add(Me.pnInfo)
    Me.pnAext.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAext.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAext.Location = New System.Drawing.Point(0, 30)
    Me.pnAext.Name = "pnAext"
    Me.pnAext.NTSActiveTrasparency = True
    Me.pnAext.Size = New System.Drawing.Size(694, 402)
    Me.pnAext.TabIndex = 4
    Me.pnAext.Text = "NtsPanel1"
    '
    'pnInfo
    '
    Me.pnInfo.AllowDrop = True
    Me.pnInfo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnInfo.Appearance.Options.UseBackColor = True
    Me.pnInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnInfo.Controls.Add(Me.lbTipork)
    Me.pnInfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnInfo.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnInfo.Location = New System.Drawing.Point(0, 0)
    Me.pnInfo.Name = "pnInfo"
    Me.pnInfo.NTSActiveTrasparency = True
    Me.pnInfo.Size = New System.Drawing.Size(694, 20)
    Me.pnInfo.TabIndex = 11
    Me.pnInfo.Text = "NtsPanel1"
    '
    'FRM__AEXT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(694, 432)
    Me.Controls.Add(Me.pnAext)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRM__AEXT"
    Me.Text = "DEFINIZIONE ESTENSIONI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAext1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext1.ResumeLayout(False)
    Me.pnAext1.PerformLayout()
    CType(Me.mbTb_helptipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.mbTb_helptipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.mbTb_helptipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_tipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tabAext, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabAext.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnAext2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext2.ResumeLayout(False)
    Me.pnAext2.PerformLayout()
    CType(Me.edTb_data3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_data5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_data2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_data4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_data1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descr6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desext3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desext2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desext1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnAext3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext3.ResumeLayout(False)
    Me.pnAext3.PerformLayout()
    CType(Me.edTb_check10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_check6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_memo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_memo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_num6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destab5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnAext4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext4.ResumeLayout(False)
    Me.pnAext4.PerformLayout()
    CType(Me.edTb_combo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_combo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom2_E.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_L.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom2_D.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_E.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_D.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom2_B.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_B.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom2_C.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_I.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_C.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom2_A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_G.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom1_F.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnAext5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext5.ResumeLayout(False)
    Me.pnAext5.PerformLayout()
    CType(Me.edTb_combo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_V.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_Q.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_L.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_E.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_P.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_T.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_D.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_N.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_U.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_B.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_I.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_O.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_S.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_C.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_G.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_M.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_R.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_helpcom3_F.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAext, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAext.ResumeLayout(False)
    CType(Me.pnInfo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnInfo.ResumeLayout(False)
    Me.pnInfo.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub LoadImage()
    Try
      tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()
      tlbMain.NTSSetToolTip()

      edTb_check1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751059062500, "Campo check 1"), 30)
      edTb_check10.NTSSetParam(oMenu, oApp.Tr(Me, 128492751057812500, "Campo check 10"), 30)
      edTb_check2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058437500, "Campo check 2"), 30)
      edTb_check3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058750000, "Campo check 3"), 30)
      edTb_check4.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058125000, "Campo check 4"), 30)
      edTb_check5.NTSSetParam(oMenu, oApp.Tr(Me, 128492751057968750, "Campo check 5"), 30)
      edTb_check6.NTSSetParam(oMenu, oApp.Tr(Me, 128492751059218750, "Campo check 6"), 30)
      edTb_check7.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058906250, "Campo check 7"), 30)
      edTb_check8.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058281250, "Campo check 8"), 30)
      edTb_check9.NTSSetParam(oMenu, oApp.Tr(Me, 128492751058593750, "Campo check 9"), 30)
      edTb_combo1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751064843750, "Campo combo 1"), 30)
      edTb_combo2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751064531250, "Campo combo 2"), 30)
      edTb_combo3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751069843750, "Campo combo 3"), 30)
      edTb_data1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054218750, "Campo data 1"), 30)
      edTb_data2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053281250, "Campo data 2"), 30)
      edTb_data3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751052812500, "Campo data 3"), 30)
      edTb_data4.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053906250, "Campo data 4"), 30)
      edTb_data5.NTSSetParam(oMenu, oApp.Tr(Me, 128492751052968750, "Campo data 5"), 30)
      edTb_descr1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054843750, "Campo descrittivo 1"), 30)
      edTb_descr10.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053125000, "Campo descrittivo 10"), 30)
      edTb_descr2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054062500, "Campo descrittivo 2"), 30)
      edTb_descr3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054531250, "Campo descrittivo 3"), 30)
      edTb_descr4.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053593750, "Campo descrittivo 4"), 30)
      edTb_descr5.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053437500, "Campo descrittivo 5"), 30)
      edTb_descr6.NTSSetParam(oMenu, oApp.Tr(Me, 128492751055000000, "Campo descrittivo 6"), 30)
      edTb_descr7.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054687500, "Campo descrittivo 7"), 30)
      edTb_descr8.NTSSetParam(oMenu, oApp.Tr(Me, 128492751053750000, "Campo descrittivo 8"), 30)
      edTb_descr9.NTSSetParam(oMenu, oApp.Tr(Me, 128492751054375000, "Campo descrittivo 9"), 30)
      edTb_desext1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751056093750, "Descrizione estesa 1"), 30)
      edTb_desext2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751055625000, "Descrizione estesa 2"), 30)
      edTb_desext3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751055468750, "Descrizione estesa 3"), 30)
      edTb_destab1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751050000000, "Descrizione Tab 1"), 15)
      edTb_destab2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751055156250, "Descrizione Tab 2"), 15)
      edTb_destab3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063593750, "Descrizione Tab 3"), 15)
      edTb_destab4.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063750000, "Descrizione Tab 4"), 15)
      edTb_destab5.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063906250, "Descrizione Tab 5"), 15)
      edTb_destab6.NTSSetParam(oMenu, oApp.Tr(Me, 128492751059531250, "Descrizione Tab 6"), 15)
      edTb_helpcom1_A.NTSSetParam(oMenu, oApp.Tr(Me, 128492751069375000, "Descr. valore 1"), 30)
      edTb_helpcom1_B.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068437500, "Descr. valore 2"), 30)
      edTb_helpcom1_C.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068906250, "Descr. valore 3"), 30)
      edTb_helpcom1_D.NTSSetParam(oMenu, oApp.Tr(Me, 128492751067968750, "Descr. valore 4"), 30)
      edTb_helpcom1_E.NTSSetParam(oMenu, oApp.Tr(Me, 128492751067812500, "Descr. valore 5"), 30)
      edTb_helpcom1_F.NTSSetParam(oMenu, oApp.Tr(Me, 128492751069687500, "Descr. valore 6"), 30)
      edTb_helpcom1_G.NTSSetParam(oMenu, oApp.Tr(Me, 128492751069218750, "Descr. valore 7"), 30)
      edTb_helpcom1_H.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068281250, "Descr. valore 8"), 30)
      edTb_helpcom1_I.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068750000, "Descr. valore 9"), 30)
      edTb_helpcom1_L.NTSSetParam(oMenu, oApp.Tr(Me, 128492751067500000, "Descr. valore 10"), 30)
      edTb_helpcom2_A.NTSSetParam(oMenu, oApp.Tr(Me, 128492751069062500, "Descr. valore 1"), 30)
      edTb_helpcom2_B.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068125000, "Descr. valore 2"), 30)
      edTb_helpcom2_C.NTSSetParam(oMenu, oApp.Tr(Me, 128492751068593750, "Descr. valore 3"), 30)
      edTb_helpcom2_D.NTSSetParam(oMenu, oApp.Tr(Me, 128492751067656250, "Descr. valore 4"), 30)
      edTb_helpcom2_E.NTSSetParam(oMenu, oApp.Tr(Me, 128492751067343750, "Descr. valore 5"), 30)
      edTb_helpcom3_A.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075937500, "Descr. valore 1"), 30)
      edTb_helpcom3_B.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074687500, "Descr. valore 2"), 30)
      edTb_helpcom3_C.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075312500, "Descr. valore 3"), 30)
      edTb_helpcom3_D.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074062500, "Descr. valore 4"), 30)
      edTb_helpcom3_E.NTSSetParam(oMenu, oApp.Tr(Me, 128492751073593750, "Descr. valore 5"), 30)
      edTb_helpcom3_F.NTSSetParam(oMenu, oApp.Tr(Me, 128492751076093750, "Descr. valore 6"), 30)
      edTb_helpcom3_G.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075468750, "Descr. valore 7"), 30)
      edTb_helpcom3_H.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074218750, "Descr. valore 8"), 30)
      edTb_helpcom3_I.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074843750, "Descr. valore 9"), 30)
      edTb_helpcom3_L.NTSSetParam(oMenu, oApp.Tr(Me, 128492751073437500, "Descr. valore 10"), 30)
      edTb_helpcom3_M.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075625000, "Descr. valore 11"), 30)
      edTb_helpcom3_N.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074375000, "Descr. valore 12"), 30)
      edTb_helpcom3_O.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075000000, "Descr. valore 13"), 30)
      edTb_helpcom3_P.NTSSetParam(oMenu, oApp.Tr(Me, 128492751073750000, "Descr. valore 14"), 30)
      edTb_helpcom3_Q.NTSSetParam(oMenu, oApp.Tr(Me, 128492751073281250, "Descr. valore 15"), 30)
      edTb_helpcom3_R.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075781250, "Descr. valore 16"), 30)
      edTb_helpcom3_S.NTSSetParam(oMenu, oApp.Tr(Me, 128492751075156250, "Descr. valore 17"), 30)
      edTb_helpcom3_T.NTSSetParam(oMenu, oApp.Tr(Me, 128492751073906250, "Descr. valore 18"), 30)
      edTb_helpcom3_U.NTSSetParam(oMenu, oApp.Tr(Me, 128492751074531250, "Descr. valore 19"), 30)
      edTb_helpcom3_V.NTSSetParam(oMenu, oApp.Tr(Me, 128492751072812500, "Descr. valore 20"), 30)
      edTb_memo1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063125000, "Campo memo 1"), 30)
      edTb_memo2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062656250, "Campo memo 2"), 30)
      edTb_num1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063281250, "Campo numerico 1"), 30)
      edTb_num10.NTSSetParam(oMenu, oApp.Tr(Me, 128492751061718750, "Campo numerico 10"), 30)
      edTb_num2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062343750, "Campo numerico 2"), 30)
      edTb_num3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062812500, "Campo numerico 3"), 30)
      edTb_num4.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062031250, "Campo numerico 4"), 30)
      edTb_num5.NTSSetParam(oMenu, oApp.Tr(Me, 128492751061875000, "Campo numerico 5"), 30)
      edTb_num6.NTSSetParam(oMenu, oApp.Tr(Me, 128492751063437500, "Campo numerico 6"), 30)
      edTb_num7.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062968750, "Campo numerico 7"), 30)
      edTb_num8.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062187500, "Campo numerico 8"), 30)
      edTb_num9.NTSSetParam(oMenu, oApp.Tr(Me, 128492751062500000, "Campo numerico 9"), 30)
      edTb_tipo1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751049843750, "Tipo da un carattere 1"), 30)
      edTb_tipo2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751049375000, "Tipo da un carattere 2"), 30)
      edTb_tipo3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751048750000, "Tipo da un carattere 3"), 30)
      mbTb_helptipo1.NTSSetParam(oMenu, oApp.Tr(Me, 128492751048281250, "Help tipo da un carattere 1"), 255)
      mbTb_helptipo2.NTSSetParam(oMenu, oApp.Tr(Me, 128492751047812500, "Help tipo da un carattere 2"), 255)
      mbTb_helptipo3.NTSSetParam(oMenu, oApp.Tr(Me, 128492751047343750, "Help tipo da un carattere 3"), 255)

      ''''edTb_codesb_1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128485132438750000, "Codice Iva Esente (1)"), tabciva)

      ''''edTb_codesb_1.NTSForzaVisZoom = True

      ''''edTb_codesb_1.NTSSetParamZoom("ZOOMTABCIVA")

      InitEnviroment()
      '-------------------------------------------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con source ext
      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub InitEnviroment()
    SetStato(0)
    charTipork = "C"c
  End Sub
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------------------------------------------
      'Se i controlli erano già stati precedentemente collegati, li scollego
      '-------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------------------------------------------
      'Collego il BindingSource ai vari controlli 
      '-------------------------------------------------------------------------------------
      edTb_check1.NTSDbField = "tabaext.tb_check1"
      edTb_check2.NTSDbField = "tabaext.tb_check2"
      edTb_check3.NTSDbField = "tabaext.tb_check3"
      edTb_check4.NTSDbField = "tabaext.tb_check4"
      edTb_check5.NTSDbField = "tabaext.tb_check5"
      edTb_check6.NTSDbField = "tabaext.tb_check6"
      edTb_check7.NTSDbField = "tabaext.tb_check7"
      edTb_check8.NTSDbField = "tabaext.tb_check8"
      edTb_check9.NTSDbField = "tabaext.tb_check9"
      edTb_check10.NTSDbField = "tabaext.tb_check10"
      edTb_combo1.NTSDbField = "tabaext.tb_combo1"
      edTb_combo2.NTSDbField = "tabaext.tb_combo2"
      edTb_combo3.NTSDbField = "tabaext.tb_combo3"
      edTb_data1.NTSDbField = "tabaext.tb_data1"
      edTb_data2.NTSDbField = "tabaext.tb_data2"
      edTb_data3.NTSDbField = "tabaext.tb_data3"
      edTb_data4.NTSDbField = "tabaext.tb_data4"
      edTb_data5.NTSDbField = "tabaext.tb_data5"
      edTb_descr1.NTSDbField = "tabaext.tb_descr1"
      edTb_descr2.NTSDbField = "tabaext.tb_descr2"
      edTb_descr3.NTSDbField = "tabaext.tb_descr3"
      edTb_descr4.NTSDbField = "tabaext.tb_descr4"
      edTb_descr5.NTSDbField = "tabaext.tb_descr5"
      edTb_descr6.NTSDbField = "tabaext.tb_descr6"
      edTb_descr7.NTSDbField = "tabaext.tb_descr7"
      edTb_descr8.NTSDbField = "tabaext.tb_descr8"
      edTb_descr9.NTSDbField = "tabaext.tb_descr9"
      edTb_descr10.NTSDbField = "tabaext.tb_descr10"
      edTb_desext1.NTSDbField = "tabaext.tb_desext1"
      edTb_desext2.NTSDbField = "tabaext.tb_desext2"
      edTb_desext3.NTSDbField = "tabaext.tb_desext3"
      edTb_destab1.NTSDbField = "tabaext.tb_destab1"
      edTb_destab2.NTSDbField = "tabaext.tb_destab2"
      edTb_destab3.NTSDbField = "tabaext.tb_destab3"
      edTb_destab4.NTSDbField = "tabaext.tb_destab4"
      edTb_destab5.NTSDbField = "tabaext.tb_destab5"
      edTb_destab6.NTSDbField = "tabaext.tb_destab6"
      edTb_helpcom1_A.NTSDbField = "tabaext.tb_helpcom1_A"
      edTb_helpcom1_B.NTSDbField = "tabaext.tb_helpcom1_B"
      edTb_helpcom1_C.NTSDbField = "tabaext.tb_helpcom1_C"
      edTb_helpcom1_D.NTSDbField = "tabaext.tb_helpcom1_D"
      edTb_helpcom1_E.NTSDbField = "tabaext.tb_helpcom1_E"
      edTb_helpcom1_F.NTSDbField = "tabaext.tb_helpcom1_F"
      edTb_helpcom1_G.NTSDbField = "tabaext.tb_helpcom1_G"
      edTb_helpcom1_H.NTSDbField = "tabaext.tb_helpcom1_H"
      edTb_helpcom1_I.NTSDbField = "tabaext.tb_helpcom1_I"
      edTb_helpcom1_L.NTSDbField = "tabaext.tb_helpcom1_L"
      edTb_helpcom2_A.NTSDbField = "tabaext.tb_helpcom2_A"
      edTb_helpcom2_B.NTSDbField = "tabaext.tb_helpcom2_B"
      edTb_helpcom2_C.NTSDbField = "tabaext.tb_helpcom2_C"
      edTb_helpcom2_D.NTSDbField = "tabaext.tb_helpcom2_D"
      edTb_helpcom2_E.NTSDbField = "tabaext.tb_helpcom2_E"
      edTb_helpcom3_A.NTSDbField = "tabaext.tb_helpcom3_A"
      edTb_helpcom3_B.NTSDbField = "tabaext.tb_helpcom3_B"
      edTb_helpcom3_C.NTSDbField = "tabaext.tb_helpcom3_C"
      edTb_helpcom3_D.NTSDbField = "tabaext.tb_helpcom3_D"
      edTb_helpcom3_E.NTSDbField = "tabaext.tb_helpcom3_E"
      edTb_helpcom3_F.NTSDbField = "tabaext.tb_helpcom3_F"
      edTb_helpcom3_G.NTSDbField = "tabaext.tb_helpcom3_G"
      edTb_helpcom3_H.NTSDbField = "tabaext.tb_helpcom3_H"
      edTb_helpcom3_I.NTSDbField = "tabaext.tb_helpcom3_I"
      edTb_helpcom3_L.NTSDbField = "tabaext.tb_helpcom3_L"
      edTb_helpcom3_M.NTSDbField = "tabaext.tb_helpcom3_M"
      edTb_helpcom3_N.NTSDbField = "tabaext.tb_helpcom3_N"
      edTb_helpcom3_O.NTSDbField = "tabaext.tb_helpcom3_O"
      edTb_helpcom3_P.NTSDbField = "tabaext.tb_helpcom3_P"
      edTb_helpcom3_Q.NTSDbField = "tabaext.tb_helpcom3_Q"
      edTb_helpcom3_R.NTSDbField = "tabaext.tb_helpcom3_R"
      edTb_helpcom3_S.NTSDbField = "tabaext.tb_helpcom3_S"
      edTb_helpcom3_T.NTSDbField = "tabaext.tb_helpcom3_T"
      edTb_helpcom3_U.NTSDbField = "tabaext.tb_helpcom3_U"
      edTb_helpcom3_V.NTSDbField = "tabaext.tb_helpcom3_V"
      edTb_memo1.NTSDbField = "tabaext.tb_memo1"
      edTb_memo2.NTSDbField = "tabaext.tb_memo2"
      edTb_num1.NTSDbField = "tabaext.tb_num1"
      edTb_num2.NTSDbField = "tabaext.tb_num2"
      edTb_num3.NTSDbField = "tabaext.tb_num3"
      edTb_num4.NTSDbField = "tabaext.tb_num4"
      edTb_num5.NTSDbField = "tabaext.tb_num5"
      edTb_num6.NTSDbField = "tabaext.tb_num6"
      edTb_num7.NTSDbField = "tabaext.tb_num7"
      edTb_num8.NTSDbField = "tabaext.tb_num8"
      edTb_num9.NTSDbField = "tabaext.tb_num9"
      edTb_num10.NTSDbField = "tabaext.tb_num10"
      edTb_tipo1.NTSDbField = "tabaext.tb_tipo1"
      edTb_tipo2.NTSDbField = "tabaext.tb_tipo2"
      edTb_tipo3.NTSDbField = "tabaext.tb_tipo3"
      mbTb_helptipo1.NTSDbField = "tabaext.tb_helptipo1"
      mbTb_helptipo2.NTSDbField = "tabaext.tb_helptipo2"
      mbTb_helptipo3.NTSDbField = "tabaext.tb_helptipo3"

      '-------------------------------------------------------------------------------------
      'Per agganciare al dataset i vari controlli
      '-------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcAext, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRM__AEXT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------------------------------------
      InitControls()

      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__AEXT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva(True) Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__AEXT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAext.Dispose()
      dsAext.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Function Apri(ByVal nModalita As Integer) As Boolean
    Dim frmAemd As FRM__AEMD = Nothing
    Dim bNuovo As Boolean
    Try
      frmAemd = CType(NTSNewFormModal("FRM__AEMD"), FRM__AEMD)

      NTSFormClearDataBinding(Me)

      frmAemd.Init(oMenu, oCallParams, DittaCorrente)
      If nModalita = 2 Then
        bNuovo = True
      Else
        bNuovo = False
      End If
      frmAemd.InitEnviroment(oCleAext, bNuovo)
      frmAemd.ShowDialog()

      If frmAemd.bConfermato Then
        charTipork = frmAemd.charTipork
        SetStato(nModalita)
        '-------------------------------------------------------------------------------------
        '--- Leggo dal database i dati e collego il NTSBinding
        '-------------------------------------------------------------------------------------
        If Not oCleAext.RecordApri(dsAext, charTipork) Then Me.Close()

        dcAext.DataSource = dsAext.Tables("TABAEXT")
        dsAext.AcceptChanges()


        If dsAext.Tables("TABAEXT").Rows.Count = 0 Then
          oCleAext.RecordNuovo(charTipork)
          'dcAext.MoveLast()
        Else

        End If
        '-------------------------------------------------------------------------------------
        '--- Collego il BindingSource ai vari controlli 
        '-------------------------------------------------------------------------------------
        Bindcontrols()
        '-------------------------------------------------------------------------------------
        '--- Sempre alla fine di questa funzione: applico le regole della gctl
        '--- e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
        '-------------------------------------------------------------------------------------
        GctlSetRoules()
        '-------------------------------------------------------------------------------------
        dcAext.ResetBindings(False)
        dcAext.MoveFirst()

        Me.GctlApplicaDefaultValue()

        Return True

      End If

      '''' Non serve gia' di default restituisce una false 
      '''' Return False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmAemd Is Nothing Then frmAemd.Dispose()
      frmAemd = Nothing
    End Try
  End Function
  Public Overridable Function Salva(ByVal bEsci As Boolean) As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      If oCleAext.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False
        If bEsci Then
          'chiamato da esci
          If nEditMode = 1 Then
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128497185151718750, "Salvare le modifiche apportate al record prima di uscire?"))
          Else
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128497185211093750, "Salvare il nuovo record inserito prima di uscire?"))
          End If
        Else
          'chiamato da tasto aggiorna
          If nEditMode = 1 Then
            '1=apri
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128497185238281250, "Salvare le modifiche apportate al record?"))
          Else
            '2 nuovo
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128497185271406250, "Salvare il nuovo record inserito?"))
          End If
        End If
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Cancel
            Return False
          Case Windows.Forms.DialogResult.Yes
            If Not oCleAext.RecordSalva(False) Then Return False
            If nEditMode = 2 Then

              'salvare i record anche in anaext
              SalvaRecordInAnaext()

            End If
          Case Windows.Forms.DialogResult.No
            tlbRipristina_ItemClick(Nothing, Nothing)
        End Select
      End If
      If Not bEsci Then SetStato(0)
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      Apri(2) '2=apri
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      Apri(1) '1=apri
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlb_Aggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If oCleAext.bHasChanges Then
        Dim dlgRes As DialogResult

        If sender Is Nothing Then
          dlgRes = Windows.Forms.DialogResult.Yes
        Else
          If nEditMode = 1 Then
            '1=apri
            dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497270625156250, "Ripristinare le modifiche apportate al record aperto?"))
          Else
            '2 nuovo
            dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497270666406250, "Annulare l'inserimento del nuovo record?"))
          End If
        End If
        Select Case dlgRes
          Case Windows.Forms.DialogResult.No
            Return
          Case Windows.Forms.DialogResult.Yes
            oCleAext.RecordRipristina(dcAext.Position, dcAext.Filter)
        End Select
      End If
      SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim dlgRes As DialogResult
    Try
      If oCleAext.TestPreCancella() Then
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497216941250000, "Confermare la cancellazione del record?"))
      Else
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497216973437500, "Attenzione!" & vbCrLf & _
        "Esistono dei records relativi alla tipologia di estensioni." & vbCrLf & _
        "Cancellando la tipologia non sara' piu' possibile visualizzare gli attributi inseriti." & vbCrLf & _
        "Procedere con la cancellazione dei record?"))
      End If
      If dlgRes = Windows.Forms.DialogResult.Yes Then
        NTSFormClearDataBinding(Me)
        dcAext.RemoveAt(dcAext.Position)
        If Not oCleAext.RecordSalva(True) Then
          dcAext.ResetItem(dcAext.Position)
          Bindcontrols()
        Else
          SetStato(0)
        End If
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
#End Region

  Public Overridable Function SetStato(ByVal nEditMode As Integer) As Boolean
    Try
      Me.nEditMode = nEditMode

      If nEditMode = 0 Then
        'Nessuno
        pnAext.Visible = False
        tlbNuovo.Enabled = True
        tlbApri.Enabled = True
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False
        tlbCancella.Enabled = False
      Else
        pnAext.Visible = True
        tabAext.SelectedTabPage = NtsTabPage1
        tlbNuovo.Enabled = False
        tlbApri.Enabled = False
        tlbSalva.Enabled = True
        tlbRipristina.Enabled = True
        Select Case charTipork
          Case "C"c
            lbTipork.Text = oApp.Tr(Me, 128499024772031250, "Clienti")
          Case "F"c
            lbTipork.Text = oApp.Tr(Me, 128499024815781250, "Fornitori")
          Case "D"c
            lbTipork.Text = oApp.Tr(Me, 128499024851718750, "Destinazioni diverse clienti")
          Case "E"c
            lbTipork.Text = oApp.Tr(Me, 128499024878593750, "Destinazioni diverse fornitori")
          Case "L"c
            lbTipork.Text = oApp.Tr(Me, 128499024905156250, "Leads")
          Case "M"c
            lbTipork.Text = oApp.Tr(Me, 128499024941093750, "Matricole")
          Case "U"c
            lbTipork.Text = oApp.Tr(Me, 128794187903480867, "Ubicazioni")
          Case "A"c
            lbTipork.Text = oApp.Tr(Me, 130440261753285935, "Articoli")
        End Select
        If nEditMode = 1 Then
          'Apri
          tlbCancella.Enabled = True
        ElseIf nEditMode = 2 Then
          'Nuovo
          tlbCancella.Enabled = False
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Function

  Public Overridable Function SalvaRecordInAnaext() As Boolean
    Try
      Dim strAppendTiporkDesc As String = ""
      Dim strAppendTiporkDesc2 As String = ""

      'stato nuovo
      Select Case charTipork
        Case "C"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295655156250, "Cliente?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 12849729565515625, "clienti")
        Case "F"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295702968750, "Fornitore?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128497295655156252, "fornitori")
        Case "D"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295739375000, "Destinazione diversa cliente?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128497295655156253, "destinazioni diverse clienti")
        Case "E"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295779375000, "Destinazione diversa fornitore?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128497295655156254, "destinazioni diverse fornitori")
        Case "L"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295818906250, "Lead?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128497295655156255, "leads")
        Case "M"c
          strAppendTiporkDesc = oApp.Tr(Me, 128794204364413505, "Matricola?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128794204401757972, "matricole")
        Case "U"c
          strAppendTiporkDesc = oApp.Tr(Me, 128497295863906250, "Ubicazione?")
          strAppendTiporkDesc2 = oApp.Tr(Me, 128497295655156256, "ubicazioni")
        Case "A"c : Return True
      End Select
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497296109062500, "Si desidera procedere con la generazione in automatico" & vbCrLf & "degli attributi appena definiti per ogni ") & strAppendTiporkDesc) = Windows.Forms.DialogResult.Yes Then
        If Not oCleAext.SalvaRecordInAnaext(charTipork) Then oApp.MsgBoxInfo(oApp.Tr(Me, 128498781440468750, "Non esistono |" & strAppendTiporkDesc2 & "|. Impossibile creare gli attributi."))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
