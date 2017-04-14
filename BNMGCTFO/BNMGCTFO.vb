#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGCTFO

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG
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
  Public oCleCtfo As CLEMGCTFO
  Public dsCtfo As DataSet
  Public oCallParams As CLE__CLDP
  Public dcCtfo As BindingSource = New BindingSource
  Public dcCtfoDettaglio As BindingSource = New BindingSource
  Public strTabella As String = "ARTEST"

  Private components As System.ComponentModel.IContainer
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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsTabControl1 As NTSInformatica.NTSTabControl
  Public WithEvents tabpVenditeAcquisti As NTSInformatica.NTSTabPage
  Public WithEvents lbAe_codartf As NTSInformatica.NTSLabel
  Public WithEvents edAe_codartf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_forn As NTSInformatica.NTSLabel
  Public WithEvents edAe_forn As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_codart As NTSInformatica.NTSLabel
  Public WithEvents edAe_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_siglaforn As NTSInformatica.NTSLabel
  Public WithEvents edAe_siglaforn As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_codmarc As NTSInformatica.NTSLabel
  Public WithEvents edAe_codmarc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_status As NTSInformatica.NTSLabel
  Public WithEvents cbAe_status As NTSInformatica.NTSComboBox
  Public WithEvents lbAe_descr As NTSInformatica.NTSLabel
  Public WithEvents edAe_descr As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAe_desint As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_unmis As NTSInformatica.NTSLabel
  Public WithEvents edAe_unmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_confez2 As NTSInformatica.NTSLabel
  Public WithEvents edAe_confez2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_qtacon2 As NTSInformatica.NTSLabel
  Public WithEvents edAe_qtacon2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_codiva As NTSInformatica.NTSLabel
  Public WithEvents edAe_codiva As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_gruppo As NTSInformatica.NTSLabel
  Public WithEvents edAe_gruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_sotgru As NTSInformatica.NTSLabel
  Public WithEvents edAe_sotgru As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_clascon As NTSInformatica.NTSLabel
  Public WithEvents edAe_clascon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_sostit As NTSInformatica.NTSLabel
  Public WithEvents edAe_sostit As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUltagg As NTSInformatica.NTSLabel
  Public WithEvents lbAe_famprod As NTSInformatica.NTSLabel
  Public WithEvents edAe_famprod As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_codnomc As NTSInformatica.NTSLabel
  Public WithEvents edAe_codnomc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_pesolor As NTSInformatica.NTSLabel
  Public WithEvents edAe_pesolor As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_pesonet As NTSInformatica.NTSLabel
  Public WithEvents edAe_pesonet As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_sostituito As NTSInformatica.NTSLabel
  Public WithEvents edAe_sostituito As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_volume As NTSInformatica.NTSLabel
  Public WithEvents edAe_volume As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_prezzopu As NTSInformatica.NTSLabel
  Public WithEvents edAe_prezzopu As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_dataggpu As NTSInformatica.NTSLabel
  Public WithEvents edAe_dataggpu As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAe_prezzogr As NTSInformatica.NTSLabel
  Public WithEvents edAe_prezzogr As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_datagggr As NTSInformatica.NTSLabel
  Public WithEvents edAe_datagggr As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAe_codeump As NTSInformatica.NTSLabel
  Public WithEvents edAe_codeump As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_codeconf As NTSInformatica.NTSLabel
  Public WithEvents edAe_codeconf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_descrl1 As NTSInformatica.NTSLabel
  Public WithEvents edAe_descrl1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_desintl1 As NTSInformatica.NTSLabel
  Public WithEvents edAe_desintl1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_descrl2 As NTSInformatica.NTSLabel
  Public WithEvents edAe_descrl2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_desintl2 As NTSInformatica.NTSLabel
  Public WithEvents edAe_desintl2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_descrl3 As NTSInformatica.NTSLabel
  Public WithEvents edAe_descrl3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_desintl3 As NTSInformatica.NTSLabel
  Public WithEvents edAe_desintl3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_descr1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desmarc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desciva As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descsar As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desgmer As NTSInformatica.NTSLabel
  Public WithEvents lbTb_dessgme As NTSInformatica.NTSLabel
  Public WithEvents lbTb_descfam As NTSInformatica.NTSLabel
  Public WithEvents pnCtfo As NTSInformatica.NTSPanel
  Public WithEvents pnCtfoBottom As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo1 As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo2 As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo3 As NTSInformatica.NTSPanel
  Public WithEvents pnCtfoTopRight As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo6 As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo5 As NTSInformatica.NTSPanel
  Public WithEvents pnCtfo7 As NTSInformatica.NTSPanel
  Public WithEvents tabpAltriDati As NTSInformatica.NTSTabPage
  Public WithEvents tabpNote As NTSInformatica.NTSTabPage
  Public WithEvents tabpDescrLingua As NTSInformatica.NTSTabPage
  Public WithEvents fmUM As NTSInformatica.NTSGroupBox
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents edAe_note As NTSInformatica.NTSMemoBox
  Public WithEvents fmAcquisti As NTSInformatica.NTSGroupBox
  Public WithEvents edAe_minordin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAe_rrfence As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_minordin As NTSInformatica.NTSLabel
  Public WithEvents lbAe_sublotto As NTSInformatica.NTSLabel
  Public WithEvents edAe_sublotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_rrfence As NTSInformatica.NTSLabel
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCTFO))
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
    Me.NtsTabControl1 = New NTSInformatica.NTSTabControl
    Me.tabpVenditeAcquisti = New NTSInformatica.NTSTabPage
    Me.pnCtfo1 = New NTSInformatica.NTSPanel
    Me.fmAcquisti = New NTSInformatica.NTSGroupBox
    Me.edAe_minordin = New NTSInformatica.NTSTextBoxNum
    Me.edAe_rrfence = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_minordin = New NTSInformatica.NTSLabel
    Me.lbAe_sublotto = New NTSInformatica.NTSLabel
    Me.edAe_sublotto = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_rrfence = New NTSInformatica.NTSLabel
    Me.fmPrezzi = New NTSInformatica.NTSGroupBox
    Me.pnCtfo5 = New NTSInformatica.NTSPanel
    Me.lbAe_prezzopu = New NTSInformatica.NTSLabel
    Me.edAe_datagggr = New NTSInformatica.NTSTextBoxData
    Me.edAe_prezzopu = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_datagggr = New NTSInformatica.NTSLabel
    Me.edAe_dataggpu = New NTSInformatica.NTSTextBoxData
    Me.edAe_prezzogr = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_prezzogr = New NTSInformatica.NTSLabel
    Me.lbAe_dataggpu = New NTSInformatica.NTSLabel
    Me.pnCtfo7 = New NTSInformatica.NTSPanel
    Me.lbAe_sostit = New NTSInformatica.NTSLabel
    Me.edAe_clascon = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_clascon = New NTSInformatica.NTSLabel
    Me.edAe_sostituito = New NTSInformatica.NTSTextBoxStr
    Me.edAe_sostit = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_descsar = New NTSInformatica.NTSLabel
    Me.lbAe_sostituito = New NTSInformatica.NTSLabel
    Me.tabpAltriDati = New NTSInformatica.NTSTabPage
    Me.pnCtfo6 = New NTSInformatica.NTSPanel
    Me.lbAe_ultagg = New NTSInformatica.NTSLabel
    Me.lbAe_volume = New NTSInformatica.NTSLabel
    Me.lbAe_codnomc = New NTSInformatica.NTSLabel
    Me.edAe_pesolor = New NTSInformatica.NTSTextBoxNum
    Me.lbUltagg = New NTSInformatica.NTSLabel
    Me.edAe_codnomc = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_pesonet = New NTSInformatica.NTSLabel
    Me.edAe_pesonet = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_pesolor = New NTSInformatica.NTSLabel
    Me.edAe_volume = New NTSInformatica.NTSTextBoxNum
    Me.pnCtfo2 = New NTSInformatica.NTSPanel
    Me.lbAe_codiva = New NTSInformatica.NTSLabel
    Me.edAe_gruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_desgmer = New NTSInformatica.NTSLabel
    Me.lbTb_dessgme = New NTSInformatica.NTSLabel
    Me.lbAe_gruppo = New NTSInformatica.NTSLabel
    Me.edAe_codiva = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_sotgru = New NTSInformatica.NTSLabel
    Me.lbTb_descfam = New NTSInformatica.NTSLabel
    Me.edAe_famprod = New NTSInformatica.NTSTextBoxStr
    Me.edAe_codeconf = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_codeconf = New NTSInformatica.NTSLabel
    Me.lbAe_codeump = New NTSInformatica.NTSLabel
    Me.edAe_sotgru = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_desciva = New NTSInformatica.NTSLabel
    Me.lbAe_famprod = New NTSInformatica.NTSLabel
    Me.edAe_codeump = New NTSInformatica.NTSTextBoxStr
    Me.tabpDescrLingua = New NTSInformatica.NTSTabPage
    Me.pnCtfo3 = New NTSInformatica.NTSPanel
    Me.edAe_desintl2 = New NTSInformatica.NTSTextBoxStr
    Me.edAe_desintl3 = New NTSInformatica.NTSTextBoxStr
    Me.edAe_descrl2 = New NTSInformatica.NTSTextBoxStr
    Me.edAe_descrl1 = New NTSInformatica.NTSTextBoxStr
    Me.edAe_desintl1 = New NTSInformatica.NTSTextBoxStr
    Me.edAe_descrl3 = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_descrl1 = New NTSInformatica.NTSLabel
    Me.lbAe_desintl1 = New NTSInformatica.NTSLabel
    Me.lbAe_desintl2 = New NTSInformatica.NTSLabel
    Me.lbAe_descrl3 = New NTSInformatica.NTSLabel
    Me.lbAe_desintl3 = New NTSInformatica.NTSLabel
    Me.lbAe_descrl2 = New NTSInformatica.NTSLabel
    Me.tabpNote = New NTSInformatica.NTSTabPage
    Me.edAe_note = New NTSInformatica.NTSMemoBox
    Me.lbAe_codartf = New NTSInformatica.NTSLabel
    Me.edAe_codartf = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_forn = New NTSInformatica.NTSLabel
    Me.edAe_forn = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_codart = New NTSInformatica.NTSLabel
    Me.edAe_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_siglaforn = New NTSInformatica.NTSLabel
    Me.edAe_siglaforn = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_codmarc = New NTSInformatica.NTSLabel
    Me.edAe_codmarc = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_status = New NTSInformatica.NTSLabel
    Me.cbAe_status = New NTSInformatica.NTSComboBox
    Me.lbAe_descr = New NTSInformatica.NTSLabel
    Me.edAe_descr = New NTSInformatica.NTSTextBoxStr
    Me.edAe_desint = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_unmis = New NTSInformatica.NTSLabel
    Me.edAe_unmis = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_confez2 = New NTSInformatica.NTSLabel
    Me.edAe_confez2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_qtacon2 = New NTSInformatica.NTSLabel
    Me.edAe_qtacon2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAn_descr1 = New NTSInformatica.NTSLabel
    Me.lbTb_desmarc = New NTSInformatica.NTSLabel
    Me.pnCtfo = New NTSInformatica.NTSPanel
    Me.pnCtfoTopLeft = New NTSInformatica.NTSPanel
    Me.pnCtfoTopRight = New NTSInformatica.NTSPanel
    Me.fmUM = New NTSInformatica.NTSGroupBox
    Me.pnCtfoBottom = New NTSInformatica.NTSPanel
    Me.cmdCreaArticolo = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabControl1.SuspendLayout()
    Me.tabpVenditeAcquisti.SuspendLayout()
    CType(Me.pnCtfo1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo1.SuspendLayout()
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAcquisti.SuspendLayout()
    CType(Me.edAe_minordin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_rrfence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_sublotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzi.SuspendLayout()
    CType(Me.pnCtfo5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo5.SuspendLayout()
    CType(Me.edAe_datagggr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_prezzopu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_dataggpu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_prezzogr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCtfo7, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo7.SuspendLayout()
    CType(Me.edAe_clascon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_sostituito.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_sostit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabpAltriDati.SuspendLayout()
    CType(Me.pnCtfo6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo6.SuspendLayout()
    CType(Me.edAe_pesolor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codnomc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_pesonet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_volume.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCtfo2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo2.SuspendLayout()
    CType(Me.edAe_gruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_famprod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codeconf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_sotgru.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codeump.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabpDescrLingua.SuspendLayout()
    CType(Me.pnCtfo3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo3.SuspendLayout()
    CType(Me.edAe_desintl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_desintl3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_descrl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_descrl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_desintl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_descrl3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabpNote.SuspendLayout()
    CType(Me.edAe_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codartf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_forn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_siglaforn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codmarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAe_status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_descr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_desint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_unmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_confez2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_qtacon2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCtfo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfo.SuspendLayout()
    CType(Me.pnCtfoTopLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfoTopLeft.SuspendLayout()
    CType(Me.pnCtfoTopRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfoTopRight.SuspendLayout()
    CType(Me.fmUM, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUM.SuspendLayout()
    CType(Me.pnCtfoBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCtfoBottom.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci, Me.tlbApri})
    Me.NtsBarManager1.MaxItemId = 28
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 26
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
    'NtsTabControl1
    '
    Me.NtsTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.NtsTabControl1.Location = New System.Drawing.Point(0, 0)
    Me.NtsTabControl1.Name = "NtsTabControl1"
    Me.NtsTabControl1.SelectedTabPage = Me.tabpVenditeAcquisti
    Me.NtsTabControl1.Size = New System.Drawing.Size(738, 239)
    Me.NtsTabControl1.TabIndex = 4
    Me.NtsTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabpVenditeAcquisti, Me.tabpAltriDati, Me.tabpDescrLingua, Me.tabpNote})
    Me.NtsTabControl1.Text = "NtsTabControl1"
    '
    'tabpVenditeAcquisti
    '
    Me.tabpVenditeAcquisti.AllowDrop = True
    Me.tabpVenditeAcquisti.Controls.Add(Me.pnCtfo1)
    Me.tabpVenditeAcquisti.Enable = True
    Me.tabpVenditeAcquisti.Name = "tabpVenditeAcquisti"
    Me.tabpVenditeAcquisti.Size = New System.Drawing.Size(729, 209)
    Me.tabpVenditeAcquisti.Text = "&Dati Vendite/Acquisti"
    '
    'pnCtfo1
    '
    Me.pnCtfo1.AllowDrop = True
    Me.pnCtfo1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo1.Appearance.Options.UseBackColor = True
    Me.pnCtfo1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo1.Controls.Add(Me.fmAcquisti)
    Me.pnCtfo1.Controls.Add(Me.fmPrezzi)
    Me.pnCtfo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCtfo1.Location = New System.Drawing.Point(0, 0)
    Me.pnCtfo1.Name = "pnCtfo1"
    Me.pnCtfo1.NTSActiveTrasparency = True
    Me.pnCtfo1.Size = New System.Drawing.Size(729, 209)
    Me.pnCtfo1.TabIndex = 531
    Me.pnCtfo1.Text = "NtsPanel1"
    '
    'fmAcquisti
    '
    Me.fmAcquisti.AllowDrop = True
    Me.fmAcquisti.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmAcquisti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAcquisti.Appearance.Options.UseBackColor = True
    Me.fmAcquisti.Controls.Add(Me.edAe_minordin)
    Me.fmAcquisti.Controls.Add(Me.edAe_rrfence)
    Me.fmAcquisti.Controls.Add(Me.lbAe_minordin)
    Me.fmAcquisti.Controls.Add(Me.lbAe_sublotto)
    Me.fmAcquisti.Controls.Add(Me.edAe_sublotto)
    Me.fmAcquisti.Controls.Add(Me.lbAe_rrfence)
    Me.fmAcquisti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAcquisti.Location = New System.Drawing.Point(3, 132)
    Me.fmAcquisti.Name = "fmAcquisti"
    Me.fmAcquisti.Size = New System.Drawing.Size(723, 73)
    Me.fmAcquisti.TabIndex = 511
    Me.fmAcquisti.Text = "Acquisti"
    '
    'edAe_minordin
    '
    Me.edAe_minordin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_minordin.EditValue = "0"
    Me.edAe_minordin.Location = New System.Drawing.Point(85, 35)
    Me.edAe_minordin.Name = "edAe_minordin"
    Me.edAe_minordin.NTSDbField = ""
    Me.edAe_minordin.NTSFormat = "0"
    Me.edAe_minordin.NTSForzaVisZoom = False
    Me.edAe_minordin.NTSOldValue = ""
    Me.edAe_minordin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_minordin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_minordin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_minordin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_minordin.Properties.AutoHeight = False
    Me.edAe_minordin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_minordin.Properties.MaxLength = 65536
    Me.edAe_minordin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_minordin.Size = New System.Drawing.Size(100, 20)
    Me.edAe_minordin.TabIndex = 521
    '
    'edAe_rrfence
    '
    Me.edAe_rrfence.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_rrfence.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_rrfence.EditValue = "0"
    Me.edAe_rrfence.Location = New System.Drawing.Point(615, 35)
    Me.edAe_rrfence.Name = "edAe_rrfence"
    Me.edAe_rrfence.NTSDbField = ""
    Me.edAe_rrfence.NTSFormat = "0"
    Me.edAe_rrfence.NTSForzaVisZoom = False
    Me.edAe_rrfence.NTSOldValue = ""
    Me.edAe_rrfence.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_rrfence.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_rrfence.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_rrfence.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_rrfence.Properties.AutoHeight = False
    Me.edAe_rrfence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_rrfence.Properties.MaxLength = 65536
    Me.edAe_rrfence.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_rrfence.Size = New System.Drawing.Size(100, 20)
    Me.edAe_rrfence.TabIndex = 523
    '
    'lbAe_minordin
    '
    Me.lbAe_minordin.AutoSize = True
    Me.lbAe_minordin.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_minordin.Location = New System.Drawing.Point(5, 38)
    Me.lbAe_minordin.Name = "lbAe_minordin"
    Me.lbAe_minordin.NTSDbField = ""
    Me.lbAe_minordin.Size = New System.Drawing.Size(74, 13)
    Me.lbAe_minordin.TabIndex = 518
    Me.lbAe_minordin.Text = "Quantità lotto"
    Me.lbAe_minordin.Tooltip = ""
    Me.lbAe_minordin.UseMnemonic = False
    '
    'lbAe_sublotto
    '
    Me.lbAe_sublotto.AutoSize = True
    Me.lbAe_sublotto.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_sublotto.Location = New System.Drawing.Point(232, 38)
    Me.lbAe_sublotto.Name = "lbAe_sublotto"
    Me.lbAe_sublotto.NTSDbField = ""
    Me.lbAe_sublotto.Size = New System.Drawing.Size(55, 13)
    Me.lbAe_sublotto.TabIndex = 519
    Me.lbAe_sublotto.Text = "Sottolotto"
    Me.lbAe_sublotto.Tooltip = ""
    Me.lbAe_sublotto.UseMnemonic = False
    '
    'edAe_sublotto
    '
    Me.edAe_sublotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_sublotto.EditValue = "0"
    Me.edAe_sublotto.Location = New System.Drawing.Point(293, 35)
    Me.edAe_sublotto.Name = "edAe_sublotto"
    Me.edAe_sublotto.NTSDbField = ""
    Me.edAe_sublotto.NTSFormat = "0"
    Me.edAe_sublotto.NTSForzaVisZoom = False
    Me.edAe_sublotto.NTSOldValue = ""
    Me.edAe_sublotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_sublotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_sublotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_sublotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_sublotto.Properties.AutoHeight = False
    Me.edAe_sublotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_sublotto.Properties.MaxLength = 65536
    Me.edAe_sublotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_sublotto.Size = New System.Drawing.Size(100, 20)
    Me.edAe_sublotto.TabIndex = 522
    '
    'lbAe_rrfence
    '
    Me.lbAe_rrfence.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbAe_rrfence.AutoSize = True
    Me.lbAe_rrfence.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_rrfence.Location = New System.Drawing.Point(556, 38)
    Me.lbAe_rrfence.Name = "lbAe_rrfence"
    Me.lbAe_rrfence.NTSDbField = ""
    Me.lbAe_rrfence.Size = New System.Drawing.Size(53, 13)
    Me.lbAe_rrfence.TabIndex = 520
    Me.lbAe_rrfence.Text = "RR Fence"
    Me.lbAe_rrfence.Tooltip = ""
    Me.lbAe_rrfence.UseMnemonic = False
    '
    'fmPrezzi
    '
    Me.fmPrezzi.AllowDrop = True
    Me.fmPrezzi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzi.Appearance.Options.UseBackColor = True
    Me.fmPrezzi.Controls.Add(Me.pnCtfo5)
    Me.fmPrezzi.Controls.Add(Me.pnCtfo7)
    Me.fmPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzi.Location = New System.Drawing.Point(3, 4)
    Me.fmPrezzi.Name = "fmPrezzi"
    Me.fmPrezzi.Size = New System.Drawing.Size(723, 122)
    Me.fmPrezzi.TabIndex = 511
    Me.fmPrezzi.Text = "Vendite"
    '
    'pnCtfo5
    '
    Me.pnCtfo5.AllowDrop = True
    Me.pnCtfo5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnCtfo5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo5.Appearance.Options.UseBackColor = True
    Me.pnCtfo5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo5.Controls.Add(Me.lbAe_prezzopu)
    Me.pnCtfo5.Controls.Add(Me.edAe_datagggr)
    Me.pnCtfo5.Controls.Add(Me.edAe_prezzopu)
    Me.pnCtfo5.Controls.Add(Me.lbAe_datagggr)
    Me.pnCtfo5.Controls.Add(Me.edAe_dataggpu)
    Me.pnCtfo5.Controls.Add(Me.edAe_prezzogr)
    Me.pnCtfo5.Controls.Add(Me.lbAe_prezzogr)
    Me.pnCtfo5.Controls.Add(Me.lbAe_dataggpu)
    Me.pnCtfo5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo5.Location = New System.Drawing.Point(367, 22)
    Me.pnCtfo5.Name = "pnCtfo5"
    Me.pnCtfo5.NTSActiveTrasparency = True
    Me.pnCtfo5.Size = New System.Drawing.Size(351, 95)
    Me.pnCtfo5.TabIndex = 532
    Me.pnCtfo5.Text = "NtsPanel8"
    '
    'lbAe_prezzopu
    '
    Me.lbAe_prezzopu.AutoSize = True
    Me.lbAe_prezzopu.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_prezzopu.Location = New System.Drawing.Point(24, 40)
    Me.lbAe_prezzopu.Name = "lbAe_prezzopu"
    Me.lbAe_prezzopu.NTSDbField = ""
    Me.lbAe_prezzopu.Size = New System.Drawing.Size(96, 13)
    Me.lbAe_prezzopu.TabIndex = 37
    Me.lbAe_prezzopu.Text = "Vendita al pubblico"
    Me.lbAe_prezzopu.Tooltip = ""
    Me.lbAe_prezzopu.UseMnemonic = False
    '
    'edAe_datagggr
    '
    Me.edAe_datagggr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_datagggr.EditValue = "01/01/1900"
    Me.edAe_datagggr.Location = New System.Drawing.Point(243, 63)
    Me.edAe_datagggr.Name = "edAe_datagggr"
    Me.edAe_datagggr.NTSDbField = ""
    Me.edAe_datagggr.NTSForzaVisZoom = False
    Me.edAe_datagggr.NTSOldValue = ""
    Me.edAe_datagggr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_datagggr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_datagggr.Properties.AutoHeight = False
    Me.edAe_datagggr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_datagggr.Properties.MaxLength = 65536
    Me.edAe_datagggr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_datagggr.Size = New System.Drawing.Size(105, 20)
    Me.edAe_datagggr.TabIndex = 530
    '
    'edAe_prezzopu
    '
    Me.edAe_prezzopu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_prezzopu.EditValue = "0"
    Me.edAe_prezzopu.Location = New System.Drawing.Point(132, 37)
    Me.edAe_prezzopu.Name = "edAe_prezzopu"
    Me.edAe_prezzopu.NTSDbField = ""
    Me.edAe_prezzopu.NTSFormat = "0"
    Me.edAe_prezzopu.NTSForzaVisZoom = False
    Me.edAe_prezzopu.NTSOldValue = ""
    Me.edAe_prezzopu.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_prezzopu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_prezzopu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_prezzopu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_prezzopu.Properties.AutoHeight = False
    Me.edAe_prezzopu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_prezzopu.Properties.MaxLength = 65536
    Me.edAe_prezzopu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_prezzopu.Size = New System.Drawing.Size(105, 20)
    Me.edAe_prezzopu.TabIndex = 527
    '
    'lbAe_datagggr
    '
    Me.lbAe_datagggr.AutoSize = True
    Me.lbAe_datagggr.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_datagggr.Location = New System.Drawing.Point(265, 23)
    Me.lbAe_datagggr.Name = "lbAe_datagggr"
    Me.lbAe_datagggr.NTSDbField = ""
    Me.lbAe_datagggr.Size = New System.Drawing.Size(55, 13)
    Me.lbAe_datagggr.TabIndex = 40
    Me.lbAe_datagggr.Text = "Data agg."
    Me.lbAe_datagggr.Tooltip = ""
    Me.lbAe_datagggr.UseMnemonic = False
    '
    'edAe_dataggpu
    '
    Me.edAe_dataggpu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_dataggpu.EditValue = "01/01/1900"
    Me.edAe_dataggpu.Location = New System.Drawing.Point(243, 37)
    Me.edAe_dataggpu.Name = "edAe_dataggpu"
    Me.edAe_dataggpu.NTSDbField = ""
    Me.edAe_dataggpu.NTSForzaVisZoom = False
    Me.edAe_dataggpu.NTSOldValue = ""
    Me.edAe_dataggpu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_dataggpu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_dataggpu.Properties.AutoHeight = False
    Me.edAe_dataggpu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_dataggpu.Properties.MaxLength = 65536
    Me.edAe_dataggpu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_dataggpu.Size = New System.Drawing.Size(105, 20)
    Me.edAe_dataggpu.TabIndex = 528
    '
    'edAe_prezzogr
    '
    Me.edAe_prezzogr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_prezzogr.EditValue = "0"
    Me.edAe_prezzogr.Location = New System.Drawing.Point(132, 63)
    Me.edAe_prezzogr.Name = "edAe_prezzogr"
    Me.edAe_prezzogr.NTSDbField = ""
    Me.edAe_prezzogr.NTSFormat = "0"
    Me.edAe_prezzogr.NTSForzaVisZoom = False
    Me.edAe_prezzogr.NTSOldValue = ""
    Me.edAe_prezzogr.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_prezzogr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_prezzogr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_prezzogr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_prezzogr.Properties.AutoHeight = False
    Me.edAe_prezzogr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_prezzogr.Properties.MaxLength = 65536
    Me.edAe_prezzogr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_prezzogr.Size = New System.Drawing.Size(105, 20)
    Me.edAe_prezzogr.TabIndex = 529
    '
    'lbAe_prezzogr
    '
    Me.lbAe_prezzogr.AutoSize = True
    Me.lbAe_prezzogr.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_prezzogr.Location = New System.Drawing.Point(24, 66)
    Me.lbAe_prezzogr.Name = "lbAe_prezzogr"
    Me.lbAe_prezzogr.NTSDbField = ""
    Me.lbAe_prezzogr.Size = New System.Drawing.Size(102, 13)
    Me.lbAe_prezzogr.TabIndex = 39
    Me.lbAe_prezzogr.Text = "Vendital al grossista"
    Me.lbAe_prezzogr.Tooltip = ""
    Me.lbAe_prezzogr.UseMnemonic = False
    '
    'lbAe_dataggpu
    '
    Me.lbAe_dataggpu.AutoSize = True
    Me.lbAe_dataggpu.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_dataggpu.Location = New System.Drawing.Point(164, 23)
    Me.lbAe_dataggpu.Name = "lbAe_dataggpu"
    Me.lbAe_dataggpu.NTSDbField = ""
    Me.lbAe_dataggpu.Size = New System.Drawing.Size(39, 13)
    Me.lbAe_dataggpu.TabIndex = 38
    Me.lbAe_dataggpu.Text = "Prezzo"
    Me.lbAe_dataggpu.Tooltip = ""
    Me.lbAe_dataggpu.UseMnemonic = False
    '
    'pnCtfo7
    '
    Me.pnCtfo7.AllowDrop = True
    Me.pnCtfo7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnCtfo7.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo7.Appearance.Options.UseBackColor = True
    Me.pnCtfo7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo7.Controls.Add(Me.lbAe_sostit)
    Me.pnCtfo7.Controls.Add(Me.edAe_clascon)
    Me.pnCtfo7.Controls.Add(Me.lbAe_clascon)
    Me.pnCtfo7.Controls.Add(Me.edAe_sostituito)
    Me.pnCtfo7.Controls.Add(Me.edAe_sostit)
    Me.pnCtfo7.Controls.Add(Me.lbTb_descsar)
    Me.pnCtfo7.Controls.Add(Me.lbAe_sostituito)
    Me.pnCtfo7.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo7.Location = New System.Drawing.Point(3, 22)
    Me.pnCtfo7.Name = "pnCtfo7"
    Me.pnCtfo7.NTSActiveTrasparency = True
    Me.pnCtfo7.Size = New System.Drawing.Size(358, 95)
    Me.pnCtfo7.TabIndex = 531
    Me.pnCtfo7.Text = "NtsPanel7"
    '
    'lbAe_sostit
    '
    Me.lbAe_sostit.AutoSize = True
    Me.lbAe_sostit.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_sostit.Location = New System.Drawing.Point(3, 8)
    Me.lbAe_sostit.Name = "lbAe_sostit"
    Me.lbAe_sostit.NTSDbField = ""
    Me.lbAe_sostit.Size = New System.Drawing.Size(104, 13)
    Me.lbAe_sostit.TabIndex = 28
    Me.lbAe_sostit.Text = "Cod. art. sostitutivo"
    Me.lbAe_sostit.Tooltip = ""
    Me.lbAe_sostit.UseMnemonic = False
    '
    'edAe_clascon
    '
    Me.edAe_clascon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_clascon.EditValue = "0"
    Me.edAe_clascon.Location = New System.Drawing.Point(64, 66)
    Me.edAe_clascon.Name = "edAe_clascon"
    Me.edAe_clascon.NTSDbField = ""
    Me.edAe_clascon.NTSFormat = "0"
    Me.edAe_clascon.NTSForzaVisZoom = False
    Me.edAe_clascon.NTSOldValue = ""
    Me.edAe_clascon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_clascon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_clascon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_clascon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_clascon.Properties.AutoHeight = False
    Me.edAe_clascon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_clascon.Properties.MaxLength = 65536
    Me.edAe_clascon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_clascon.Size = New System.Drawing.Size(59, 20)
    Me.edAe_clascon.TabIndex = 514
    '
    'lbAe_clascon
    '
    Me.lbAe_clascon.AutoSize = True
    Me.lbAe_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_clascon.Location = New System.Drawing.Point(3, 69)
    Me.lbAe_clascon.Name = "lbAe_clascon"
    Me.lbAe_clascon.NTSDbField = ""
    Me.lbAe_clascon.Size = New System.Drawing.Size(55, 13)
    Me.lbAe_clascon.TabIndex = 24
    Me.lbAe_clascon.Text = "Cl. sconto"
    Me.lbAe_clascon.Tooltip = ""
    Me.lbAe_clascon.UseMnemonic = False
    '
    'edAe_sostituito
    '
    Me.edAe_sostituito.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_sostituito.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_sostituito.Location = New System.Drawing.Point(129, 31)
    Me.edAe_sostituito.Name = "edAe_sostituito"
    Me.edAe_sostituito.NTSDbField = ""
    Me.edAe_sostituito.NTSForzaVisZoom = False
    Me.edAe_sostituito.NTSOldValue = ""
    Me.edAe_sostituito.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_sostituito.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_sostituito.Properties.AutoHeight = False
    Me.edAe_sostituito.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_sostituito.Properties.MaxLength = 65536
    Me.edAe_sostituito.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_sostituito.Size = New System.Drawing.Size(216, 20)
    Me.edAe_sostituito.TabIndex = 525
    '
    'edAe_sostit
    '
    Me.edAe_sostit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_sostit.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_sostit.Location = New System.Drawing.Point(129, 5)
    Me.edAe_sostit.Name = "edAe_sostit"
    Me.edAe_sostit.NTSDbField = ""
    Me.edAe_sostit.NTSForzaVisZoom = False
    Me.edAe_sostit.NTSOldValue = ""
    Me.edAe_sostit.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_sostit.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_sostit.Properties.AutoHeight = False
    Me.edAe_sostit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_sostit.Properties.MaxLength = 65536
    Me.edAe_sostit.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_sostit.Size = New System.Drawing.Size(216, 20)
    Me.edAe_sostit.TabIndex = 518
    '
    'lbTb_descsar
    '
    Me.lbTb_descsar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_descsar.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descsar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_descsar.Location = New System.Drawing.Point(129, 66)
    Me.lbTb_descsar.Name = "lbTb_descsar"
    Me.lbTb_descsar.NTSDbField = ""
    Me.lbTb_descsar.Size = New System.Drawing.Size(216, 20)
    Me.lbTb_descsar.TabIndex = 52
    Me.lbTb_descsar.Text = "Descrizione classe di sconto"
    Me.lbTb_descsar.Tooltip = ""
    Me.lbTb_descsar.UseMnemonic = False
    '
    'lbAe_sostituito
    '
    Me.lbAe_sostituito.AutoSize = True
    Me.lbAe_sostituito.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_sostituito.Location = New System.Drawing.Point(3, 34)
    Me.lbAe_sostituito.Name = "lbAe_sostituito"
    Me.lbAe_sostituito.NTSDbField = ""
    Me.lbAe_sostituito.Size = New System.Drawing.Size(98, 13)
    Me.lbAe_sostituito.TabIndex = 35
    Me.lbAe_sostituito.Text = "Cod. art. sostituito"
    Me.lbAe_sostituito.Tooltip = ""
    Me.lbAe_sostituito.UseMnemonic = False
    '
    'tabpAltriDati
    '
    Me.tabpAltriDati.AllowDrop = True
    Me.tabpAltriDati.Controls.Add(Me.pnCtfo6)
    Me.tabpAltriDati.Controls.Add(Me.pnCtfo2)
    Me.tabpAltriDati.Enable = True
    Me.tabpAltriDati.Name = "tabpAltriDati"
    Me.tabpAltriDati.Size = New System.Drawing.Size(729, 209)
    Me.tabpAltriDati.Text = "&Altri Dati"
    '
    'pnCtfo6
    '
    Me.pnCtfo6.AllowDrop = True
    Me.pnCtfo6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo6.Appearance.Options.UseBackColor = True
    Me.pnCtfo6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo6.Controls.Add(Me.lbAe_ultagg)
    Me.pnCtfo6.Controls.Add(Me.lbAe_volume)
    Me.pnCtfo6.Controls.Add(Me.lbAe_codnomc)
    Me.pnCtfo6.Controls.Add(Me.edAe_pesolor)
    Me.pnCtfo6.Controls.Add(Me.lbUltagg)
    Me.pnCtfo6.Controls.Add(Me.edAe_codnomc)
    Me.pnCtfo6.Controls.Add(Me.lbAe_pesonet)
    Me.pnCtfo6.Controls.Add(Me.edAe_pesonet)
    Me.pnCtfo6.Controls.Add(Me.lbAe_pesolor)
    Me.pnCtfo6.Controls.Add(Me.edAe_volume)
    Me.pnCtfo6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo6.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnCtfo6.Location = New System.Drawing.Point(469, 0)
    Me.pnCtfo6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCtfo6.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCtfo6.Name = "pnCtfo6"
    Me.pnCtfo6.NTSActiveTrasparency = True
    Me.pnCtfo6.Size = New System.Drawing.Size(260, 209)
    Me.pnCtfo6.TabIndex = 534
    Me.pnCtfo6.Text = "NtsPanel6"
    '
    'lbAe_ultagg
    '
    Me.lbAe_ultagg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbAe_ultagg.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_ultagg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAe_ultagg.Location = New System.Drawing.Point(157, 164)
    Me.lbAe_ultagg.Name = "lbAe_ultagg"
    Me.lbAe_ultagg.NTSDbField = ""
    Me.lbAe_ultagg.Size = New System.Drawing.Size(100, 20)
    Me.lbAe_ultagg.TabIndex = 527
    Me.lbAe_ultagg.Tooltip = ""
    Me.lbAe_ultagg.UseMnemonic = False
    '
    'lbAe_volume
    '
    Me.lbAe_volume.AutoSize = True
    Me.lbAe_volume.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_volume.Location = New System.Drawing.Point(110, 90)
    Me.lbAe_volume.Name = "lbAe_volume"
    Me.lbAe_volume.NTSDbField = ""
    Me.lbAe_volume.Size = New System.Drawing.Size(41, 13)
    Me.lbAe_volume.TabIndex = 36
    Me.lbAe_volume.Text = "Volume"
    Me.lbAe_volume.Tooltip = ""
    Me.lbAe_volume.UseMnemonic = False
    '
    'lbAe_codnomc
    '
    Me.lbAe_codnomc.AutoSize = True
    Me.lbAe_codnomc.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codnomc.Location = New System.Drawing.Point(1, 12)
    Me.lbAe_codnomc.Name = "lbAe_codnomc"
    Me.lbAe_codnomc.NTSDbField = ""
    Me.lbAe_codnomc.Size = New System.Drawing.Size(150, 13)
    Me.lbAe_codnomc.TabIndex = 31
    Me.lbAe_codnomc.Text = "Cod. nomenclatura combinata"
    Me.lbAe_codnomc.Tooltip = ""
    Me.lbAe_codnomc.UseMnemonic = False
    '
    'edAe_pesolor
    '
    Me.edAe_pesolor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_pesolor.EditValue = "0"
    Me.edAe_pesolor.Location = New System.Drawing.Point(157, 35)
    Me.edAe_pesolor.Name = "edAe_pesolor"
    Me.edAe_pesolor.NTSDbField = ""
    Me.edAe_pesolor.NTSFormat = "0"
    Me.edAe_pesolor.NTSForzaVisZoom = False
    Me.edAe_pesolor.NTSOldValue = ""
    Me.edAe_pesolor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_pesolor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_pesolor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_pesolor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_pesolor.Properties.AutoHeight = False
    Me.edAe_pesolor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_pesolor.Properties.MaxLength = 65536
    Me.edAe_pesolor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_pesolor.Size = New System.Drawing.Size(100, 20)
    Me.edAe_pesolor.TabIndex = 522
    '
    'lbUltagg
    '
    Me.lbUltagg.AutoSize = True
    Me.lbUltagg.BackColor = System.Drawing.Color.Transparent
    Me.lbUltagg.Location = New System.Drawing.Point(14, 167)
    Me.lbUltagg.Name = "lbUltagg"
    Me.lbUltagg.NTSDbField = ""
    Me.lbUltagg.Size = New System.Drawing.Size(136, 13)
    Me.lbUltagg.TabIndex = 29
    Me.lbUltagg.Text = "Data ultimo aggiornamento"
    Me.lbUltagg.Tooltip = ""
    Me.lbUltagg.UseMnemonic = False
    '
    'edAe_codnomc
    '
    Me.edAe_codnomc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codnomc.Location = New System.Drawing.Point(157, 9)
    Me.edAe_codnomc.Name = "edAe_codnomc"
    Me.edAe_codnomc.NTSDbField = ""
    Me.edAe_codnomc.NTSForzaVisZoom = False
    Me.edAe_codnomc.NTSOldValue = ""
    Me.edAe_codnomc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codnomc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codnomc.Properties.AutoHeight = False
    Me.edAe_codnomc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codnomc.Properties.MaxLength = 65536
    Me.edAe_codnomc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codnomc.Size = New System.Drawing.Size(100, 20)
    Me.edAe_codnomc.TabIndex = 521
    '
    'lbAe_pesonet
    '
    Me.lbAe_pesonet.AutoSize = True
    Me.lbAe_pesonet.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_pesonet.Location = New System.Drawing.Point(94, 64)
    Me.lbAe_pesonet.Name = "lbAe_pesonet"
    Me.lbAe_pesonet.NTSDbField = ""
    Me.lbAe_pesonet.Size = New System.Drawing.Size(60, 13)
    Me.lbAe_pesonet.TabIndex = 33
    Me.lbAe_pesonet.Text = "Peso Netto"
    Me.lbAe_pesonet.Tooltip = ""
    Me.lbAe_pesonet.UseMnemonic = False
    '
    'edAe_pesonet
    '
    Me.edAe_pesonet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_pesonet.EditValue = "0"
    Me.edAe_pesonet.Location = New System.Drawing.Point(157, 61)
    Me.edAe_pesonet.Name = "edAe_pesonet"
    Me.edAe_pesonet.NTSDbField = ""
    Me.edAe_pesonet.NTSFormat = "0"
    Me.edAe_pesonet.NTSForzaVisZoom = False
    Me.edAe_pesonet.NTSOldValue = ""
    Me.edAe_pesonet.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_pesonet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_pesonet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_pesonet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_pesonet.Properties.AutoHeight = False
    Me.edAe_pesonet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_pesonet.Properties.MaxLength = 65536
    Me.edAe_pesonet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_pesonet.Size = New System.Drawing.Size(100, 20)
    Me.edAe_pesonet.TabIndex = 523
    '
    'lbAe_pesolor
    '
    Me.lbAe_pesolor.AutoSize = True
    Me.lbAe_pesolor.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_pesolor.Location = New System.Drawing.Point(91, 38)
    Me.lbAe_pesolor.Name = "lbAe_pesolor"
    Me.lbAe_pesolor.NTSDbField = ""
    Me.lbAe_pesolor.Size = New System.Drawing.Size(60, 13)
    Me.lbAe_pesolor.TabIndex = 32
    Me.lbAe_pesolor.Text = "Peso Lordo"
    Me.lbAe_pesolor.Tooltip = ""
    Me.lbAe_pesolor.UseMnemonic = False
    '
    'edAe_volume
    '
    Me.edAe_volume.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_volume.EditValue = "0"
    Me.edAe_volume.Location = New System.Drawing.Point(157, 87)
    Me.edAe_volume.Name = "edAe_volume"
    Me.edAe_volume.NTSDbField = ""
    Me.edAe_volume.NTSFormat = "0"
    Me.edAe_volume.NTSForzaVisZoom = False
    Me.edAe_volume.NTSOldValue = ""
    Me.edAe_volume.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_volume.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_volume.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_volume.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_volume.Properties.AutoHeight = False
    Me.edAe_volume.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_volume.Properties.MaxLength = 65536
    Me.edAe_volume.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_volume.Size = New System.Drawing.Size(100, 20)
    Me.edAe_volume.TabIndex = 526
    '
    'pnCtfo2
    '
    Me.pnCtfo2.AllowDrop = True
    Me.pnCtfo2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnCtfo2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo2.Appearance.Options.UseBackColor = True
    Me.pnCtfo2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo2.Controls.Add(Me.lbAe_codiva)
    Me.pnCtfo2.Controls.Add(Me.edAe_gruppo)
    Me.pnCtfo2.Controls.Add(Me.lbTb_desgmer)
    Me.pnCtfo2.Controls.Add(Me.lbTb_dessgme)
    Me.pnCtfo2.Controls.Add(Me.lbAe_gruppo)
    Me.pnCtfo2.Controls.Add(Me.edAe_codiva)
    Me.pnCtfo2.Controls.Add(Me.lbAe_sotgru)
    Me.pnCtfo2.Controls.Add(Me.lbTb_descfam)
    Me.pnCtfo2.Controls.Add(Me.edAe_famprod)
    Me.pnCtfo2.Controls.Add(Me.edAe_codeconf)
    Me.pnCtfo2.Controls.Add(Me.lbAe_codeconf)
    Me.pnCtfo2.Controls.Add(Me.lbAe_codeump)
    Me.pnCtfo2.Controls.Add(Me.edAe_sotgru)
    Me.pnCtfo2.Controls.Add(Me.lbTb_desciva)
    Me.pnCtfo2.Controls.Add(Me.lbAe_famprod)
    Me.pnCtfo2.Controls.Add(Me.edAe_codeump)
    Me.pnCtfo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo2.Location = New System.Drawing.Point(0, 0)
    Me.pnCtfo2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCtfo2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCtfo2.Name = "pnCtfo2"
    Me.pnCtfo2.NTSActiveTrasparency = True
    Me.pnCtfo2.Size = New System.Drawing.Size(466, 208)
    Me.pnCtfo2.TabIndex = 533
    Me.pnCtfo2.Text = "NtsPanel2"
    '
    'lbAe_codiva
    '
    Me.lbAe_codiva.AutoSize = True
    Me.lbAe_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codiva.Location = New System.Drawing.Point(9, 12)
    Me.lbAe_codiva.Name = "lbAe_codiva"
    Me.lbAe_codiva.NTSDbField = ""
    Me.lbAe_codiva.Size = New System.Drawing.Size(58, 13)
    Me.lbAe_codiva.TabIndex = 21
    Me.lbAe_codiva.Text = "Codice Iva"
    Me.lbAe_codiva.Tooltip = ""
    Me.lbAe_codiva.UseMnemonic = False
    '
    'edAe_gruppo
    '
    Me.edAe_gruppo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAe_gruppo.EditValue = "0"
    Me.edAe_gruppo.Location = New System.Drawing.Point(122, 35)
    Me.edAe_gruppo.Name = "edAe_gruppo"
    Me.edAe_gruppo.NTSDbField = ""
    Me.edAe_gruppo.NTSFormat = "0"
    Me.edAe_gruppo.NTSForzaVisZoom = False
    Me.edAe_gruppo.NTSOldValue = ""
    Me.edAe_gruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_gruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_gruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_gruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_gruppo.Properties.AutoHeight = False
    Me.edAe_gruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_gruppo.Properties.MaxLength = 65536
    Me.edAe_gruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_gruppo.Size = New System.Drawing.Size(78, 20)
    Me.edAe_gruppo.TabIndex = 512
    '
    'lbTb_desgmer
    '
    Me.lbTb_desgmer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_desgmer.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desgmer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_desgmer.Location = New System.Drawing.Point(206, 35)
    Me.lbTb_desgmer.Name = "lbTb_desgmer"
    Me.lbTb_desgmer.NTSDbField = ""
    Me.lbTb_desgmer.Size = New System.Drawing.Size(257, 20)
    Me.lbTb_desgmer.TabIndex = 53
    Me.lbTb_desgmer.Text = "Descrizione gruppo merceologico"
    Me.lbTb_desgmer.Tooltip = ""
    Me.lbTb_desgmer.UseMnemonic = False
    '
    'lbTb_dessgme
    '
    Me.lbTb_dessgme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_dessgme.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dessgme.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_dessgme.Location = New System.Drawing.Point(206, 61)
    Me.lbTb_dessgme.Name = "lbTb_dessgme"
    Me.lbTb_dessgme.NTSDbField = ""
    Me.lbTb_dessgme.Size = New System.Drawing.Size(257, 20)
    Me.lbTb_dessgme.TabIndex = 54
    Me.lbTb_dessgme.Text = "Descrizione sottogruppo merceologico"
    Me.lbTb_dessgme.Tooltip = ""
    Me.lbTb_dessgme.UseMnemonic = False
    '
    'lbAe_gruppo
    '
    Me.lbAe_gruppo.AutoSize = True
    Me.lbAe_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_gruppo.Location = New System.Drawing.Point(9, 38)
    Me.lbAe_gruppo.Name = "lbAe_gruppo"
    Me.lbAe_gruppo.NTSDbField = ""
    Me.lbAe_gruppo.Size = New System.Drawing.Size(107, 13)
    Me.lbAe_gruppo.TabIndex = 22
    Me.lbAe_gruppo.Text = "Gruppo Merceologico"
    Me.lbAe_gruppo.Tooltip = ""
    Me.lbAe_gruppo.UseMnemonic = False
    '
    'edAe_codiva
    '
    Me.edAe_codiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codiva.EditValue = "0"
    Me.edAe_codiva.Location = New System.Drawing.Point(122, 9)
    Me.edAe_codiva.Name = "edAe_codiva"
    Me.edAe_codiva.NTSDbField = ""
    Me.edAe_codiva.NTSFormat = "0"
    Me.edAe_codiva.NTSForzaVisZoom = False
    Me.edAe_codiva.NTSOldValue = ""
    Me.edAe_codiva.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_codiva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_codiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codiva.Properties.AutoHeight = False
    Me.edAe_codiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codiva.Properties.MaxLength = 65536
    Me.edAe_codiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codiva.Size = New System.Drawing.Size(78, 20)
    Me.edAe_codiva.TabIndex = 511
    '
    'lbAe_sotgru
    '
    Me.lbAe_sotgru.AutoSize = True
    Me.lbAe_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_sotgru.Location = New System.Drawing.Point(9, 64)
    Me.lbAe_sotgru.Name = "lbAe_sotgru"
    Me.lbAe_sotgru.NTSDbField = ""
    Me.lbAe_sotgru.Size = New System.Drawing.Size(67, 13)
    Me.lbAe_sotgru.TabIndex = 23
    Me.lbAe_sotgru.Text = "Sottogruppo"
    Me.lbAe_sotgru.Tooltip = ""
    Me.lbAe_sotgru.UseMnemonic = False
    '
    'lbTb_descfam
    '
    Me.lbTb_descfam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_descfam.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_descfam.Location = New System.Drawing.Point(206, 87)
    Me.lbTb_descfam.Name = "lbTb_descfam"
    Me.lbTb_descfam.NTSDbField = ""
    Me.lbTb_descfam.Size = New System.Drawing.Size(257, 20)
    Me.lbTb_descfam.TabIndex = 55
    Me.lbTb_descfam.Text = "Descrizione famiglia/linea"
    Me.lbTb_descfam.Tooltip = ""
    Me.lbTb_descfam.UseMnemonic = False
    '
    'edAe_famprod
    '
    Me.edAe_famprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_famprod.Location = New System.Drawing.Point(122, 87)
    Me.edAe_famprod.Name = "edAe_famprod"
    Me.edAe_famprod.NTSDbField = ""
    Me.edAe_famprod.NTSForzaVisZoom = False
    Me.edAe_famprod.NTSOldValue = ""
    Me.edAe_famprod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_famprod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_famprod.Properties.AutoHeight = False
    Me.edAe_famprod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_famprod.Properties.MaxLength = 65536
    Me.edAe_famprod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_famprod.Size = New System.Drawing.Size(78, 20)
    Me.edAe_famprod.TabIndex = 520
    '
    'edAe_codeconf
    '
    Me.edAe_codeconf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_codeconf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codeconf.Location = New System.Drawing.Point(206, 164)
    Me.edAe_codeconf.Name = "edAe_codeconf"
    Me.edAe_codeconf.NTSDbField = ""
    Me.edAe_codeconf.NTSForzaVisZoom = False
    Me.edAe_codeconf.NTSOldValue = ""
    Me.edAe_codeconf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codeconf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codeconf.Properties.AutoHeight = False
    Me.edAe_codeconf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codeconf.Properties.MaxLength = 65536
    Me.edAe_codeconf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codeconf.Size = New System.Drawing.Size(202, 20)
    Me.edAe_codeconf.TabIndex = 532
    '
    'lbAe_codeconf
    '
    Me.lbAe_codeconf.AutoSize = True
    Me.lbAe_codeconf.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codeconf.Location = New System.Drawing.Point(5, 167)
    Me.lbAe_codeconf.Name = "lbAe_codeconf"
    Me.lbAe_codeconf.NTSDbField = ""
    Me.lbAe_codeconf.Size = New System.Drawing.Size(195, 13)
    Me.lbAe_codeconf.TabIndex = 42
    Me.lbAe_codeconf.Text = "Codice a barre unita' misura secondaria"
    Me.lbAe_codeconf.Tooltip = ""
    Me.lbAe_codeconf.UseMnemonic = False
    '
    'lbAe_codeump
    '
    Me.lbAe_codeump.AutoSize = True
    Me.lbAe_codeump.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codeump.Location = New System.Drawing.Point(5, 141)
    Me.lbAe_codeump.Name = "lbAe_codeump"
    Me.lbAe_codeump.NTSDbField = ""
    Me.lbAe_codeump.Size = New System.Drawing.Size(188, 13)
    Me.lbAe_codeump.TabIndex = 41
    Me.lbAe_codeump.Text = "Codice a barre unita' misura principale"
    Me.lbAe_codeump.Tooltip = ""
    Me.lbAe_codeump.UseMnemonic = False
    '
    'edAe_sotgru
    '
    Me.edAe_sotgru.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_sotgru.EditValue = "0"
    Me.edAe_sotgru.Location = New System.Drawing.Point(122, 61)
    Me.edAe_sotgru.Name = "edAe_sotgru"
    Me.edAe_sotgru.NTSDbField = ""
    Me.edAe_sotgru.NTSFormat = "0"
    Me.edAe_sotgru.NTSForzaVisZoom = False
    Me.edAe_sotgru.NTSOldValue = ""
    Me.edAe_sotgru.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_sotgru.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_sotgru.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_sotgru.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_sotgru.Properties.AutoHeight = False
    Me.edAe_sotgru.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_sotgru.Properties.MaxLength = 65536
    Me.edAe_sotgru.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_sotgru.Size = New System.Drawing.Size(78, 20)
    Me.edAe_sotgru.TabIndex = 513
    '
    'lbTb_desciva
    '
    Me.lbTb_desciva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_desciva.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desciva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_desciva.Location = New System.Drawing.Point(206, 9)
    Me.lbTb_desciva.Name = "lbTb_desciva"
    Me.lbTb_desciva.NTSDbField = ""
    Me.lbTb_desciva.Size = New System.Drawing.Size(257, 20)
    Me.lbTb_desciva.TabIndex = 51
    Me.lbTb_desciva.Text = "Descrizione codice iva"
    Me.lbTb_desciva.Tooltip = ""
    Me.lbTb_desciva.UseMnemonic = False
    '
    'lbAe_famprod
    '
    Me.lbAe_famprod.AutoSize = True
    Me.lbAe_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_famprod.Location = New System.Drawing.Point(9, 90)
    Me.lbAe_famprod.Name = "lbAe_famprod"
    Me.lbAe_famprod.NTSDbField = ""
    Me.lbAe_famprod.Size = New System.Drawing.Size(71, 13)
    Me.lbAe_famprod.TabIndex = 30
    Me.lbAe_famprod.Text = "Famiglia/linea"
    Me.lbAe_famprod.Tooltip = ""
    Me.lbAe_famprod.UseMnemonic = False
    '
    'edAe_codeump
    '
    Me.edAe_codeump.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_codeump.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codeump.Location = New System.Drawing.Point(206, 138)
    Me.edAe_codeump.Name = "edAe_codeump"
    Me.edAe_codeump.NTSDbField = ""
    Me.edAe_codeump.NTSForzaVisZoom = False
    Me.edAe_codeump.NTSOldValue = ""
    Me.edAe_codeump.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codeump.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codeump.Properties.AutoHeight = False
    Me.edAe_codeump.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codeump.Properties.MaxLength = 65536
    Me.edAe_codeump.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codeump.Size = New System.Drawing.Size(202, 20)
    Me.edAe_codeump.TabIndex = 531
    '
    'tabpDescrLingua
    '
    Me.tabpDescrLingua.AllowDrop = True
    Me.tabpDescrLingua.Controls.Add(Me.pnCtfo3)
    Me.tabpDescrLingua.Enable = True
    Me.tabpDescrLingua.Name = "tabpDescrLingua"
    Me.tabpDescrLingua.Size = New System.Drawing.Size(729, 209)
    Me.tabpDescrLingua.Text = "Descrizioni in &Lingua"
    '
    'pnCtfo3
    '
    Me.pnCtfo3.AllowDrop = True
    Me.pnCtfo3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo3.Appearance.Options.UseBackColor = True
    Me.pnCtfo3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo3.Controls.Add(Me.edAe_desintl2)
    Me.pnCtfo3.Controls.Add(Me.edAe_desintl3)
    Me.pnCtfo3.Controls.Add(Me.edAe_descrl2)
    Me.pnCtfo3.Controls.Add(Me.edAe_descrl1)
    Me.pnCtfo3.Controls.Add(Me.edAe_desintl1)
    Me.pnCtfo3.Controls.Add(Me.edAe_descrl3)
    Me.pnCtfo3.Controls.Add(Me.lbAe_descrl1)
    Me.pnCtfo3.Controls.Add(Me.lbAe_desintl1)
    Me.pnCtfo3.Controls.Add(Me.lbAe_desintl2)
    Me.pnCtfo3.Controls.Add(Me.lbAe_descrl3)
    Me.pnCtfo3.Controls.Add(Me.lbAe_desintl3)
    Me.pnCtfo3.Controls.Add(Me.lbAe_descrl2)
    Me.pnCtfo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCtfo3.Location = New System.Drawing.Point(0, 0)
    Me.pnCtfo3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCtfo3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCtfo3.Name = "pnCtfo3"
    Me.pnCtfo3.NTSActiveTrasparency = True
    Me.pnCtfo3.Size = New System.Drawing.Size(729, 209)
    Me.pnCtfo3.TabIndex = 539
    Me.pnCtfo3.Text = "NtsPanel3"
    '
    'edAe_desintl2
    '
    Me.edAe_desintl2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_desintl2.Location = New System.Drawing.Point(169, 103)
    Me.edAe_desintl2.Name = "edAe_desintl2"
    Me.edAe_desintl2.NTSDbField = ""
    Me.edAe_desintl2.NTSForzaVisZoom = False
    Me.edAe_desintl2.NTSOldValue = ""
    Me.edAe_desintl2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_desintl2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_desintl2.Properties.AutoHeight = False
    Me.edAe_desintl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_desintl2.Properties.MaxLength = 65536
    Me.edAe_desintl2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_desintl2.Size = New System.Drawing.Size(373, 20)
    Me.edAe_desintl2.TabIndex = 536
    '
    'edAe_desintl3
    '
    Me.edAe_desintl3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_desintl3.Location = New System.Drawing.Point(169, 155)
    Me.edAe_desintl3.Name = "edAe_desintl3"
    Me.edAe_desintl3.NTSDbField = ""
    Me.edAe_desintl3.NTSForzaVisZoom = False
    Me.edAe_desintl3.NTSOldValue = ""
    Me.edAe_desintl3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_desintl3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_desintl3.Properties.AutoHeight = False
    Me.edAe_desintl3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_desintl3.Properties.MaxLength = 65536
    Me.edAe_desintl3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_desintl3.Size = New System.Drawing.Size(373, 20)
    Me.edAe_desintl3.TabIndex = 538
    '
    'edAe_descrl2
    '
    Me.edAe_descrl2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_descrl2.Location = New System.Drawing.Point(169, 77)
    Me.edAe_descrl2.Name = "edAe_descrl2"
    Me.edAe_descrl2.NTSDbField = ""
    Me.edAe_descrl2.NTSForzaVisZoom = False
    Me.edAe_descrl2.NTSOldValue = ""
    Me.edAe_descrl2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_descrl2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_descrl2.Properties.AutoHeight = False
    Me.edAe_descrl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_descrl2.Properties.MaxLength = 65536
    Me.edAe_descrl2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_descrl2.Size = New System.Drawing.Size(373, 20)
    Me.edAe_descrl2.TabIndex = 535
    '
    'edAe_descrl1
    '
    Me.edAe_descrl1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_descrl1.Location = New System.Drawing.Point(169, 25)
    Me.edAe_descrl1.Name = "edAe_descrl1"
    Me.edAe_descrl1.NTSDbField = ""
    Me.edAe_descrl1.NTSForzaVisZoom = False
    Me.edAe_descrl1.NTSOldValue = ""
    Me.edAe_descrl1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_descrl1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_descrl1.Properties.AutoHeight = False
    Me.edAe_descrl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_descrl1.Properties.MaxLength = 65536
    Me.edAe_descrl1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_descrl1.Size = New System.Drawing.Size(373, 20)
    Me.edAe_descrl1.TabIndex = 533
    '
    'edAe_desintl1
    '
    Me.edAe_desintl1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_desintl1.Location = New System.Drawing.Point(169, 51)
    Me.edAe_desintl1.Name = "edAe_desintl1"
    Me.edAe_desintl1.NTSDbField = ""
    Me.edAe_desintl1.NTSForzaVisZoom = False
    Me.edAe_desintl1.NTSOldValue = ""
    Me.edAe_desintl1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_desintl1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_desintl1.Properties.AutoHeight = False
    Me.edAe_desintl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_desintl1.Properties.MaxLength = 65536
    Me.edAe_desintl1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_desintl1.Size = New System.Drawing.Size(373, 20)
    Me.edAe_desintl1.TabIndex = 534
    '
    'edAe_descrl3
    '
    Me.edAe_descrl3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_descrl3.Location = New System.Drawing.Point(169, 129)
    Me.edAe_descrl3.Name = "edAe_descrl3"
    Me.edAe_descrl3.NTSDbField = ""
    Me.edAe_descrl3.NTSForzaVisZoom = False
    Me.edAe_descrl3.NTSOldValue = ""
    Me.edAe_descrl3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_descrl3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_descrl3.Properties.AutoHeight = False
    Me.edAe_descrl3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_descrl3.Properties.MaxLength = 65536
    Me.edAe_descrl3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_descrl3.Size = New System.Drawing.Size(373, 20)
    Me.edAe_descrl3.TabIndex = 537
    '
    'lbAe_descrl1
    '
    Me.lbAe_descrl1.AutoSize = True
    Me.lbAe_descrl1.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_descrl1.Location = New System.Drawing.Point(12, 28)
    Me.lbAe_descrl1.Name = "lbAe_descrl1"
    Me.lbAe_descrl1.NTSDbField = ""
    Me.lbAe_descrl1.Size = New System.Drawing.Size(61, 13)
    Me.lbAe_descrl1.TabIndex = 43
    Me.lbAe_descrl1.Text = "Descrizione"
    Me.lbAe_descrl1.Tooltip = ""
    Me.lbAe_descrl1.UseMnemonic = False
    '
    'lbAe_desintl1
    '
    Me.lbAe_desintl1.AutoSize = True
    Me.lbAe_desintl1.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_desintl1.Location = New System.Drawing.Point(12, 54)
    Me.lbAe_desintl1.Name = "lbAe_desintl1"
    Me.lbAe_desintl1.NTSDbField = ""
    Me.lbAe_desintl1.Size = New System.Drawing.Size(68, 13)
    Me.lbAe_desintl1.TabIndex = 44
    Me.lbAe_desintl1.Text = "Descr. supp."
    Me.lbAe_desintl1.Tooltip = ""
    Me.lbAe_desintl1.UseMnemonic = False
    '
    'lbAe_desintl2
    '
    Me.lbAe_desintl2.AutoSize = True
    Me.lbAe_desintl2.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_desintl2.Location = New System.Drawing.Point(12, 106)
    Me.lbAe_desintl2.Name = "lbAe_desintl2"
    Me.lbAe_desintl2.NTSDbField = ""
    Me.lbAe_desintl2.Size = New System.Drawing.Size(68, 13)
    Me.lbAe_desintl2.TabIndex = 46
    Me.lbAe_desintl2.Text = "Descr. supp."
    Me.lbAe_desintl2.Tooltip = ""
    Me.lbAe_desintl2.UseMnemonic = False
    '
    'lbAe_descrl3
    '
    Me.lbAe_descrl3.AutoSize = True
    Me.lbAe_descrl3.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_descrl3.Location = New System.Drawing.Point(12, 132)
    Me.lbAe_descrl3.Name = "lbAe_descrl3"
    Me.lbAe_descrl3.NTSDbField = ""
    Me.lbAe_descrl3.Size = New System.Drawing.Size(61, 13)
    Me.lbAe_descrl3.TabIndex = 47
    Me.lbAe_descrl3.Text = "Descrizione"
    Me.lbAe_descrl3.Tooltip = ""
    Me.lbAe_descrl3.UseMnemonic = False
    '
    'lbAe_desintl3
    '
    Me.lbAe_desintl3.AutoSize = True
    Me.lbAe_desintl3.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_desintl3.Location = New System.Drawing.Point(12, 158)
    Me.lbAe_desintl3.Name = "lbAe_desintl3"
    Me.lbAe_desintl3.NTSDbField = ""
    Me.lbAe_desintl3.Size = New System.Drawing.Size(68, 13)
    Me.lbAe_desintl3.TabIndex = 48
    Me.lbAe_desintl3.Text = "Descr. supp."
    Me.lbAe_desintl3.Tooltip = ""
    Me.lbAe_desintl3.UseMnemonic = False
    '
    'lbAe_descrl2
    '
    Me.lbAe_descrl2.AutoSize = True
    Me.lbAe_descrl2.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_descrl2.Location = New System.Drawing.Point(12, 80)
    Me.lbAe_descrl2.Name = "lbAe_descrl2"
    Me.lbAe_descrl2.NTSDbField = ""
    Me.lbAe_descrl2.Size = New System.Drawing.Size(61, 13)
    Me.lbAe_descrl2.TabIndex = 45
    Me.lbAe_descrl2.Text = "Descrizione"
    Me.lbAe_descrl2.Tooltip = ""
    Me.lbAe_descrl2.UseMnemonic = False
    '
    'tabpNote
    '
    Me.tabpNote.AllowDrop = True
    Me.tabpNote.Controls.Add(Me.edAe_note)
    Me.tabpNote.Enable = True
    Me.tabpNote.Name = "tabpNote"
    Me.tabpNote.Size = New System.Drawing.Size(729, 209)
    Me.tabpNote.Text = "N&ote"
    '
    'edAe_note
    '
    Me.edAe_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edAe_note.Location = New System.Drawing.Point(0, 0)
    Me.edAe_note.Name = "edAe_note"
    Me.edAe_note.NTSDbField = ""
    Me.edAe_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_note.Size = New System.Drawing.Size(729, 209)
    Me.edAe_note.TabIndex = 604
    '
    'lbAe_codartf
    '
    Me.lbAe_codartf.AutoSize = True
    Me.lbAe_codartf.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codartf.Location = New System.Drawing.Point(13, 38)
    Me.lbAe_codartf.Name = "lbAe_codartf"
    Me.lbAe_codartf.NTSDbField = ""
    Me.lbAe_codartf.Size = New System.Drawing.Size(78, 13)
    Me.lbAe_codartf.TabIndex = 10
    Me.lbAe_codartf.Text = "Cod. art. forn:"
    Me.lbAe_codartf.Tooltip = ""
    Me.lbAe_codartf.UseMnemonic = False
    '
    'edAe_codartf
    '
    Me.edAe_codartf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_codartf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codartf.Enabled = False
    Me.edAe_codartf.Location = New System.Drawing.Point(97, 35)
    Me.edAe_codartf.Name = "edAe_codartf"
    Me.edAe_codartf.NTSDbField = ""
    Me.edAe_codartf.NTSForzaVisZoom = False
    Me.edAe_codartf.NTSOldValue = ""
    Me.edAe_codartf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codartf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codartf.Properties.AutoHeight = False
    Me.edAe_codartf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codartf.Properties.MaxLength = 65536
    Me.edAe_codartf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codartf.Size = New System.Drawing.Size(372, 20)
    Me.edAe_codartf.TabIndex = 500
    '
    'lbAe_forn
    '
    Me.lbAe_forn.AutoSize = True
    Me.lbAe_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_forn.Location = New System.Drawing.Point(13, 9)
    Me.lbAe_forn.Name = "lbAe_forn"
    Me.lbAe_forn.NTSDbField = ""
    Me.lbAe_forn.Size = New System.Drawing.Size(51, 13)
    Me.lbAe_forn.TabIndex = 11
    Me.lbAe_forn.Text = "Fornitore"
    Me.lbAe_forn.Tooltip = ""
    Me.lbAe_forn.UseMnemonic = False
    '
    'edAe_forn
    '
    Me.edAe_forn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_forn.EditValue = "0"
    Me.edAe_forn.Enabled = False
    Me.edAe_forn.Location = New System.Drawing.Point(97, 9)
    Me.edAe_forn.Name = "edAe_forn"
    Me.edAe_forn.NTSDbField = ""
    Me.edAe_forn.NTSFormat = "0"
    Me.edAe_forn.NTSForzaVisZoom = False
    Me.edAe_forn.NTSOldValue = ""
    Me.edAe_forn.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_forn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_forn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_forn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_forn.Properties.AutoHeight = False
    Me.edAe_forn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_forn.Properties.MaxLength = 65536
    Me.edAe_forn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_forn.Size = New System.Drawing.Size(100, 20)
    Me.edAe_forn.TabIndex = 501
    '
    'lbAe_codart
    '
    Me.lbAe_codart.AutoSize = True
    Me.lbAe_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codart.Location = New System.Drawing.Point(206, 64)
    Me.lbAe_codart.Name = "lbAe_codart"
    Me.lbAe_codart.NTSDbField = ""
    Me.lbAe_codart.Size = New System.Drawing.Size(51, 13)
    Me.lbAe_codart.TabIndex = 12
    Me.lbAe_codart.Text = "Cod. art:"
    Me.lbAe_codart.Tooltip = ""
    Me.lbAe_codart.UseMnemonic = False
    '
    'edAe_codart
    '
    Me.edAe_codart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codart.Enabled = False
    Me.edAe_codart.Location = New System.Drawing.Point(263, 61)
    Me.edAe_codart.Name = "edAe_codart"
    Me.edAe_codart.NTSDbField = ""
    Me.edAe_codart.NTSForzaVisZoom = False
    Me.edAe_codart.NTSOldValue = ""
    Me.edAe_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codart.Properties.AutoHeight = False
    Me.edAe_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codart.Properties.MaxLength = 65536
    Me.edAe_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codart.Size = New System.Drawing.Size(206, 20)
    Me.edAe_codart.TabIndex = 502
    '
    'lbAe_siglaforn
    '
    Me.lbAe_siglaforn.AutoSize = True
    Me.lbAe_siglaforn.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_siglaforn.Location = New System.Drawing.Point(13, 64)
    Me.lbAe_siglaforn.Name = "lbAe_siglaforn"
    Me.lbAe_siglaforn.NTSDbField = ""
    Me.lbAe_siglaforn.Size = New System.Drawing.Size(74, 13)
    Me.lbAe_siglaforn.TabIndex = 13
    Me.lbAe_siglaforn.Text = "Sigla fornitore"
    Me.lbAe_siglaforn.Tooltip = ""
    Me.lbAe_siglaforn.UseMnemonic = False
    '
    'edAe_siglaforn
    '
    Me.edAe_siglaforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_siglaforn.Location = New System.Drawing.Point(97, 61)
    Me.edAe_siglaforn.Name = "edAe_siglaforn"
    Me.edAe_siglaforn.NTSDbField = ""
    Me.edAe_siglaforn.NTSForzaVisZoom = False
    Me.edAe_siglaforn.NTSOldValue = ""
    Me.edAe_siglaforn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_siglaforn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_siglaforn.Properties.AutoHeight = False
    Me.edAe_siglaforn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_siglaforn.Properties.MaxLength = 65536
    Me.edAe_siglaforn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_siglaforn.Size = New System.Drawing.Size(100, 20)
    Me.edAe_siglaforn.TabIndex = 503
    '
    'lbAe_codmarc
    '
    Me.lbAe_codmarc.AutoSize = True
    Me.lbAe_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codmarc.Location = New System.Drawing.Point(13, 90)
    Me.lbAe_codmarc.Name = "lbAe_codmarc"
    Me.lbAe_codmarc.NTSDbField = ""
    Me.lbAe_codmarc.Size = New System.Drawing.Size(36, 13)
    Me.lbAe_codmarc.TabIndex = 14
    Me.lbAe_codmarc.Text = "Marca"
    Me.lbAe_codmarc.Tooltip = ""
    Me.lbAe_codmarc.UseMnemonic = False
    '
    'edAe_codmarc
    '
    Me.edAe_codmarc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codmarc.EditValue = "0"
    Me.edAe_codmarc.Enabled = False
    Me.edAe_codmarc.Location = New System.Drawing.Point(97, 87)
    Me.edAe_codmarc.Name = "edAe_codmarc"
    Me.edAe_codmarc.NTSDbField = ""
    Me.edAe_codmarc.NTSFormat = "0"
    Me.edAe_codmarc.NTSForzaVisZoom = False
    Me.edAe_codmarc.NTSOldValue = ""
    Me.edAe_codmarc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_codmarc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_codmarc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_codmarc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_codmarc.Properties.AutoHeight = False
    Me.edAe_codmarc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codmarc.Properties.MaxLength = 65536
    Me.edAe_codmarc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codmarc.Size = New System.Drawing.Size(68, 20)
    Me.edAe_codmarc.TabIndex = 504
    '
    'lbAe_status
    '
    Me.lbAe_status.AutoSize = True
    Me.lbAe_status.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_status.Location = New System.Drawing.Point(35, 96)
    Me.lbAe_status.Name = "lbAe_status"
    Me.lbAe_status.NTSDbField = ""
    Me.lbAe_status.Size = New System.Drawing.Size(38, 13)
    Me.lbAe_status.TabIndex = 15
    Me.lbAe_status.Text = "Status"
    Me.lbAe_status.Tooltip = ""
    Me.lbAe_status.UseMnemonic = False
    '
    'cbAe_status
    '
    Me.cbAe_status.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAe_status.DataSource = Nothing
    Me.cbAe_status.DisplayMember = ""
    Me.cbAe_status.Location = New System.Drawing.Point(79, 93)
    Me.cbAe_status.Name = "cbAe_status"
    Me.cbAe_status.NTSDbField = ""
    Me.cbAe_status.Properties.AutoHeight = False
    Me.cbAe_status.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAe_status.Properties.DropDownRows = 30
    Me.cbAe_status.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAe_status.SelectedValue = ""
    Me.cbAe_status.Size = New System.Drawing.Size(170, 20)
    Me.cbAe_status.TabIndex = 505
    Me.cbAe_status.ValueMember = ""
    '
    'lbAe_descr
    '
    Me.lbAe_descr.AutoSize = True
    Me.lbAe_descr.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_descr.Location = New System.Drawing.Point(13, 116)
    Me.lbAe_descr.Name = "lbAe_descr"
    Me.lbAe_descr.NTSDbField = ""
    Me.lbAe_descr.Size = New System.Drawing.Size(61, 13)
    Me.lbAe_descr.TabIndex = 16
    Me.lbAe_descr.Text = "Descrizione"
    Me.lbAe_descr.Tooltip = ""
    Me.lbAe_descr.UseMnemonic = False
    '
    'edAe_descr
    '
    Me.edAe_descr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_descr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_descr.Location = New System.Drawing.Point(97, 113)
    Me.edAe_descr.Name = "edAe_descr"
    Me.edAe_descr.NTSDbField = ""
    Me.edAe_descr.NTSForzaVisZoom = False
    Me.edAe_descr.NTSOldValue = ""
    Me.edAe_descr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_descr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_descr.Properties.AutoHeight = False
    Me.edAe_descr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_descr.Properties.MaxLength = 65536
    Me.edAe_descr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_descr.Size = New System.Drawing.Size(372, 20)
    Me.edAe_descr.TabIndex = 506
    '
    'edAe_desint
    '
    Me.edAe_desint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAe_desint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_desint.Location = New System.Drawing.Point(97, 139)
    Me.edAe_desint.Name = "edAe_desint"
    Me.edAe_desint.NTSDbField = ""
    Me.edAe_desint.NTSForzaVisZoom = False
    Me.edAe_desint.NTSOldValue = ""
    Me.edAe_desint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_desint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_desint.Properties.AutoHeight = False
    Me.edAe_desint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_desint.Properties.MaxLength = 65536
    Me.edAe_desint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_desint.Size = New System.Drawing.Size(372, 20)
    Me.edAe_desint.TabIndex = 507
    '
    'lbAe_unmis
    '
    Me.lbAe_unmis.AutoSize = True
    Me.lbAe_unmis.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_unmis.Location = New System.Drawing.Point(8, 25)
    Me.lbAe_unmis.Name = "lbAe_unmis"
    Me.lbAe_unmis.NTSDbField = ""
    Me.lbAe_unmis.Size = New System.Drawing.Size(52, 13)
    Me.lbAe_unmis.TabIndex = 18
    Me.lbAe_unmis.Text = "Principale"
    Me.lbAe_unmis.Tooltip = ""
    Me.lbAe_unmis.UseMnemonic = False
    '
    'edAe_unmis
    '
    Me.edAe_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_unmis.Location = New System.Drawing.Point(75, 22)
    Me.edAe_unmis.Name = "edAe_unmis"
    Me.edAe_unmis.NTSDbField = ""
    Me.edAe_unmis.NTSForzaVisZoom = False
    Me.edAe_unmis.NTSOldValue = ""
    Me.edAe_unmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_unmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_unmis.Properties.AutoHeight = False
    Me.edAe_unmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_unmis.Properties.MaxLength = 65536
    Me.edAe_unmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_unmis.Size = New System.Drawing.Size(64, 20)
    Me.edAe_unmis.TabIndex = 508
    '
    'lbAe_confez2
    '
    Me.lbAe_confez2.AutoSize = True
    Me.lbAe_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_confez2.Location = New System.Drawing.Point(8, 51)
    Me.lbAe_confez2.Name = "lbAe_confez2"
    Me.lbAe_confez2.NTSDbField = ""
    Me.lbAe_confez2.Size = New System.Drawing.Size(61, 13)
    Me.lbAe_confez2.TabIndex = 19
    Me.lbAe_confez2.Text = "Confezione"
    Me.lbAe_confez2.Tooltip = ""
    Me.lbAe_confez2.UseMnemonic = False
    '
    'edAe_confez2
    '
    Me.edAe_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_confez2.Location = New System.Drawing.Point(75, 48)
    Me.edAe_confez2.Name = "edAe_confez2"
    Me.edAe_confez2.NTSDbField = ""
    Me.edAe_confez2.NTSForzaVisZoom = False
    Me.edAe_confez2.NTSOldValue = ""
    Me.edAe_confez2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_confez2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_confez2.Properties.AutoHeight = False
    Me.edAe_confez2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_confez2.Properties.MaxLength = 65536
    Me.edAe_confez2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_confez2.Size = New System.Drawing.Size(64, 20)
    Me.edAe_confez2.TabIndex = 509
    '
    'lbAe_qtacon2
    '
    Me.lbAe_qtacon2.AutoSize = True
    Me.lbAe_qtacon2.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_qtacon2.Location = New System.Drawing.Point(169, 25)
    Me.lbAe_qtacon2.Name = "lbAe_qtacon2"
    Me.lbAe_qtacon2.NTSDbField = ""
    Me.lbAe_qtacon2.Size = New System.Drawing.Size(49, 13)
    Me.lbAe_qtacon2.TabIndex = 20
    Me.lbAe_qtacon2.Text = "Quantità"
    Me.lbAe_qtacon2.Tooltip = ""
    Me.lbAe_qtacon2.UseMnemonic = False
    '
    'edAe_qtacon2
    '
    Me.edAe_qtacon2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_qtacon2.EditValue = "1"
    Me.edAe_qtacon2.Location = New System.Drawing.Point(145, 48)
    Me.edAe_qtacon2.Name = "edAe_qtacon2"
    Me.edAe_qtacon2.NTSDbField = ""
    Me.edAe_qtacon2.NTSFormat = "0"
    Me.edAe_qtacon2.NTSForzaVisZoom = False
    Me.edAe_qtacon2.NTSOldValue = "1"
    Me.edAe_qtacon2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_qtacon2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_qtacon2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAe_qtacon2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAe_qtacon2.Properties.AutoHeight = False
    Me.edAe_qtacon2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_qtacon2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_qtacon2.Size = New System.Drawing.Size(100, 20)
    Me.edAe_qtacon2.TabIndex = 510
    '
    'lbAn_descr1
    '
    Me.lbAn_descr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbAn_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAn_descr1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAn_descr1.Location = New System.Drawing.Point(203, 9)
    Me.lbAn_descr1.Name = "lbAn_descr1"
    Me.lbAn_descr1.NTSDbField = ""
    Me.lbAn_descr1.Size = New System.Drawing.Size(266, 20)
    Me.lbAn_descr1.TabIndex = 49
    Me.lbAn_descr1.Text = "Descrizione fornitore"
    Me.lbAn_descr1.Tooltip = ""
    Me.lbAn_descr1.UseMnemonic = False
    '
    'lbTb_desmarc
    '
    Me.lbTb_desmarc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTb_desmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desmarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_desmarc.Location = New System.Drawing.Point(171, 87)
    Me.lbTb_desmarc.Name = "lbTb_desmarc"
    Me.lbTb_desmarc.NTSDbField = ""
    Me.lbTb_desmarc.Size = New System.Drawing.Size(298, 20)
    Me.lbTb_desmarc.TabIndex = 50
    Me.lbTb_desmarc.Text = "Descrizione marca/marchio"
    Me.lbTb_desmarc.Tooltip = ""
    Me.lbTb_desmarc.UseMnemonic = False
    '
    'pnCtfo
    '
    Me.pnCtfo.AllowDrop = True
    Me.pnCtfo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfo.Appearance.Options.UseBackColor = True
    Me.pnCtfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfo.Controls.Add(Me.pnCtfoTopLeft)
    Me.pnCtfo.Controls.Add(Me.pnCtfoTopRight)
    Me.pnCtfo.Controls.Add(Me.pnCtfoBottom)
    Me.pnCtfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCtfo.Location = New System.Drawing.Point(0, 30)
    Me.pnCtfo.Name = "pnCtfo"
    Me.pnCtfo.NTSActiveTrasparency = True
    Me.pnCtfo.Size = New System.Drawing.Size(738, 403)
    Me.pnCtfo.TabIndex = 5
    Me.pnCtfo.Text = "NtsPanel1"
    '
    'pnCtfoTopLeft
    '
    Me.pnCtfoTopLeft.AllowDrop = True
    Me.pnCtfoTopLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfoTopLeft.Appearance.Options.UseBackColor = True
    Me.pnCtfoTopLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_forn)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_codartf)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_desint)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_descr)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbTb_desmarc)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_descr)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_codartf)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_codmarc)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_codmarc)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_forn)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_siglaforn)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAn_descr1)
    Me.pnCtfoTopLeft.Controls.Add(Me.edAe_codart)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_codart)
    Me.pnCtfoTopLeft.Controls.Add(Me.lbAe_siglaforn)
    Me.pnCtfoTopLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfoTopLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnCtfoTopLeft.Name = "pnCtfoTopLeft"
    Me.pnCtfoTopLeft.NTSActiveTrasparency = True
    Me.pnCtfoTopLeft.Size = New System.Drawing.Size(473, 162)
    Me.pnCtfoTopLeft.TabIndex = 0
    Me.pnCtfoTopLeft.Text = "NtsPanel1"
    '
    'pnCtfoTopRight
    '
    Me.pnCtfoTopRight.AllowDrop = True
    Me.pnCtfoTopRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfoTopRight.Appearance.Options.UseBackColor = True
    Me.pnCtfoTopRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfoTopRight.Controls.Add(Me.cmdCreaArticolo)
    Me.pnCtfoTopRight.Controls.Add(Me.fmUM)
    Me.pnCtfoTopRight.Controls.Add(Me.cbAe_status)
    Me.pnCtfoTopRight.Controls.Add(Me.lbAe_status)
    Me.pnCtfoTopRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfoTopRight.Location = New System.Drawing.Point(475, 0)
    Me.pnCtfoTopRight.Name = "pnCtfoTopRight"
    Me.pnCtfoTopRight.NTSActiveTrasparency = True
    Me.pnCtfoTopRight.Size = New System.Drawing.Size(263, 162)
    Me.pnCtfoTopRight.TabIndex = 512
    Me.pnCtfoTopRight.Text = "NtsPanel5"
    '
    'fmUM
    '
    Me.fmUM.AllowDrop = True
    Me.fmUM.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUM.Appearance.Options.UseBackColor = True
    Me.fmUM.Controls.Add(Me.edAe_confez2)
    Me.fmUM.Controls.Add(Me.lbAe_confez2)
    Me.fmUM.Controls.Add(Me.edAe_unmis)
    Me.fmUM.Controls.Add(Me.lbAe_unmis)
    Me.fmUM.Controls.Add(Me.lbAe_qtacon2)
    Me.fmUM.Controls.Add(Me.edAe_qtacon2)
    Me.fmUM.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUM.Location = New System.Drawing.Point(4, 9)
    Me.fmUM.Name = "fmUM"
    Me.fmUM.Size = New System.Drawing.Size(256, 72)
    Me.fmUM.TabIndex = 511
    Me.fmUM.Text = "Unità di misura"
    '
    'pnCtfoBottom
    '
    Me.pnCtfoBottom.AllowDrop = True
    Me.pnCtfoBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCtfoBottom.Appearance.Options.UseBackColor = True
    Me.pnCtfoBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCtfoBottom.Controls.Add(Me.NtsTabControl1)
    Me.pnCtfoBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCtfoBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnCtfoBottom.Location = New System.Drawing.Point(0, 165)
    Me.pnCtfoBottom.Name = "pnCtfoBottom"
    Me.pnCtfoBottom.NTSActiveTrasparency = True
    Me.pnCtfoBottom.Size = New System.Drawing.Size(738, 238)
    Me.pnCtfoBottom.TabIndex = 6
    Me.pnCtfoBottom.Text = "NtsPanel4"
    '
    'cmdCreaArticolo
    '
    Me.cmdCreaArticolo.ImagePath = ""
    Me.cmdCreaArticolo.ImageText = ""
    Me.cmdCreaArticolo.Location = New System.Drawing.Point(79, 136)
    Me.cmdCreaArticolo.Name = "cmdCreaArticolo"
    Me.cmdCreaArticolo.NTSContextMenu = Nothing
    Me.cmdCreaArticolo.Size = New System.Drawing.Size(170, 23)
    Me.cmdCreaArticolo.TabIndex = 512
    Me.cmdCreaArticolo.Text = "Crea Articolo"
    '
    'FRMMGCTFO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(738, 433)
    Me.Controls.Add(Me.pnCtfo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGCTFO"
    Me.Text = "CATALOGO FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabControl1.ResumeLayout(False)
    Me.tabpVenditeAcquisti.ResumeLayout(False)
    CType(Me.pnCtfo1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo1.ResumeLayout(False)
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAcquisti.ResumeLayout(False)
    Me.fmAcquisti.PerformLayout()
    CType(Me.edAe_minordin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_rrfence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_sublotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzi.ResumeLayout(False)
    CType(Me.pnCtfo5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo5.ResumeLayout(False)
    Me.pnCtfo5.PerformLayout()
    CType(Me.edAe_datagggr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_prezzopu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_dataggpu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_prezzogr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCtfo7, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo7.ResumeLayout(False)
    Me.pnCtfo7.PerformLayout()
    CType(Me.edAe_clascon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_sostituito.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_sostit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabpAltriDati.ResumeLayout(False)
    CType(Me.pnCtfo6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo6.ResumeLayout(False)
    Me.pnCtfo6.PerformLayout()
    CType(Me.edAe_pesolor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codnomc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_pesonet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_volume.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCtfo2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo2.ResumeLayout(False)
    Me.pnCtfo2.PerformLayout()
    CType(Me.edAe_gruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_famprod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codeconf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_sotgru.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codeump.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabpDescrLingua.ResumeLayout(False)
    CType(Me.pnCtfo3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo3.ResumeLayout(False)
    Me.pnCtfo3.PerformLayout()
    CType(Me.edAe_desintl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_desintl3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_descrl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_descrl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_desintl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_descrl3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabpNote.ResumeLayout(False)
    CType(Me.edAe_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codartf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_forn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_siglaforn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codmarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAe_status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_descr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_desint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_unmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_confez2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_qtacon2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCtfo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfo.ResumeLayout(False)
    CType(Me.pnCtfoTopLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfoTopLeft.ResumeLayout(False)
    Me.pnCtfoTopLeft.PerformLayout()
    CType(Me.pnCtfoTopRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfoTopRight.ResumeLayout(False)
    Me.pnCtfoTopRight.PerformLayout()
    CType(Me.fmUM, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUM.ResumeLayout(False)
    Me.fmUM.PerformLayout()
    CType(Me.pnCtfoBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCtfoBottom.ResumeLayout(False)
    Me.ResumeLayout(False)

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
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGCTFO", "BEMGCTFO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128674380440781250, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleCtfo = CType(oTmp, CLEMGCTFO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGCTFO", strRemoteServer, strRemotePort)
    AddHandler oCleCtfo.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCtfo.Init(oApp, oScript, oMenu.oCleComm, strTabella, bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub LoadImage()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
      Catch ex As Exception
      End Try
      Try
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
      Catch ex As Exception
      End Try
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
      Catch ex As Exception
      End Try
      Try
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
      Catch ex As Exception
      End Try
      Try
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
      Catch ex As Exception
      End Try
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
      Catch ex As Exception
      End Try
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
      Catch ex As Exception
      End Try
      Try
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
      Catch ex As Exception
      End Try
      Try
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
      Catch ex As Exception
      End Try
      Try
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
      Catch ex As Exception
      End Try
      Try
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
      Catch ex As Exception
      End Try
      Try
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAe_codartf.NTSSetParam(oMenu, oApp.Tr(Me, 128681206701562500, "Codice Articolo del fornitore"), 30, True)
      edAe_forn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681206728437500, "Codice Fornitore"), tabanagraf)
      edAe_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681206767343750, "Codice alticolo in anagrafica articoli se acquisito"), tabartico, True)
      edAe_siglaforn.NTSSetParam(oMenu, oApp.Tr(Me, 128681206840625000, "Sigla fornitore"), 10, True)
      edAe_codmarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681206872187500, "Marca/marchio"), tabmarc)
      cbAe_status.NTSSetParam(oApp.Tr(Me, 128681206960312500, "Status"))
      edAe_descr.NTSSetParam(oMenu, oApp.Tr(Me, 128681206995468750, "Descrizione Articolo"), 40, True)
      edAe_desint.NTSSetParam(oMenu, oApp.Tr(Me, 128681207027500000, "Descrizione Articolo Ulteriore"), 40, True)
      edAe_unmis.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207065156250, "Unità misura"), tabumis, True)
      edAe_confez2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207108281250, "Unita' misura confezione"), tabumis, True)
      edAe_qtacon2.NTSSetParam(oMenu, oApp.Tr(Me, 128681207176875000, "Quantita' contenuta in una confezione"), oApp.FormatQta, 14, NTSCDec(Val("0.001")), 99999999)
      edAe_codiva.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207213125000, "Codice Iva"), tabciva)
      edAe_gruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207256718750, "Codice Gruppo Merceologico"), tabgmer)
      edAe_sotgru.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207425781250, "Codice Sottogruppo Merceologico"), tabsgme)
      edAe_clascon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207645468750, "Classe di sconto"), tabcsar)
      edAe_minordin.NTSSetParam(oMenu, oApp.Tr(Me, 128681207670937500, "Lotto Minimo ordinabile"), oApp.FormatQta, 15, 0, 999999999)
      edAe_sublotto.NTSSetParam(oMenu, oApp.Tr(Me, 128681207694843750, "Sottolotto"), oApp.FormatQta, 15, 0, 999999999)
      edAe_rrfence.NTSSetParam(oMenu, oApp.Tr(Me, 128681207735312500, "RR fence: tempo di fornitura in giorni"), "0", 4, 0, 999)
      edAe_sostit.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207760312500, "Codice articolo sostitutivo"), tabartico, True)
      edAe_famprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681207820781250, "Codice Famiglia/linea"), tabcfam, True)
      edAe_codnomc.NTSSetParam(oMenu, oApp.Tr(Me, 128681207853906250, "Codice nomenclatura combinata"), 10, True)
      edAe_pesolor.NTSSetParam(oMenu, oApp.Tr(Me, 128681207885468750, "Peso lordo"), oApp.FormatQta, 14, 0, 99999999)
      edAe_pesonet.NTSSetParam(oMenu, oApp.Tr(Me, 128681207943906250, "Peso Netto"), oApp.FormatQta, 14, 0, 99999999)
      edAe_note.NTSSetParam(oMenu, oApp.Tr(Me, 128681207985312500, "Note"), 0, True)
      edAe_sostituito.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128681208068281250, "Cod. art. sostituito"), tabartico, True)
      edAe_volume.NTSSetParam(oMenu, oApp.Tr(Me, 128681208105000000, "Volume"), oApp.FormatQta, 19, 0, 999999999999)
      edAe_prezzopu.NTSSetParam(oMenu, oApp.Tr(Me, 128681208142968750, "Prezzo Listino pubblico"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      edAe_dataggpu.NTSSetParam(oMenu, oApp.Tr(Me, 128681208173593750, "Data aggiornamento prezzo listino al pubblico"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edAe_prezzogr.NTSSetParam(oMenu, oApp.Tr(Me, 128681208209531250, "Prezzo Listino grossista"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      edAe_datagggr.NTSSetParam(oMenu, oApp.Tr(Me, 128681208236718750, "Data aggiornamento prezzo listino grossista"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edAe_codeump.NTSSetParam(oMenu, oApp.Tr(Me, 128681208270468750, "Codice a barre unita' misura principale"), 255, True)
      edAe_codeconf.NTSSetParam(oMenu, oApp.Tr(Me, 128681208295156250, "Codice a barre unita' misura secondaria"), 255, True)
      edAe_descrl1.NTSSetParam(oMenu, oApp.Tr(Me, 128681208346093750, "Descrizione 1 Articolo lingua 1"), 40, True)
      edAe_desintl1.NTSSetParam(oMenu, oApp.Tr(Me, 128681208368750000, "Descrizione 2 Articolo lingua 1"), 40, True)
      edAe_descrl2.NTSSetParam(oMenu, oApp.Tr(Me, 128681208390312500, "Descrizione 1 Articolo lingua 2"), 40, True)
      edAe_desintl2.NTSSetParam(oMenu, oApp.Tr(Me, 128681208410937500, "Descrizione 2 Articolo lingua 2"), 40, True)
      edAe_descrl3.NTSSetParam(oMenu, oApp.Tr(Me, 128681208436250000, "Descrizione 1 Articolo lingua 3"), 40, True)
      edAe_desintl3.NTSSetParam(oMenu, oApp.Tr(Me, 128681208497656250, "Descrizione 2 Articolo lingua 3"), 40, True)

      edAe_codnomc.NTSSetParamZoom("ZOOMTARIC")

      edAe_descr.NTSSetRichiesto()
      edAe_unmis.NTSSetRichiesto()
      edAe_codiva.NTSSetRichiesto()
      edAe_siglaforn.NTSSetRichiesto()

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
  Public Overridable Sub CaricaCombo()
    Dim dttStatus As New DataTable()
    Try
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {" ", "Valido/attivo"})
      dttStatus.Rows.Add(New Object() {"D", "Cancellato"})
      dttStatus.Rows.Add(New Object() {"E", "In esaurimento"})
      dttStatus.Rows.Add(New Object() {"S", "Sostituito"})
      dttStatus.Rows.Add(New Object() {"V", "Vecchio codice"})
      dttStatus.Rows.Add(New Object() {"A", "Altro"})
      dttStatus.AcceptChanges()
      cbAe_status.DataSource = dttStatus
      cbAe_status.ValueMember = "cod"
      cbAe_status.DisplayMember = "val"
      cbAe_status.SelectedIndex = 1
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAe_codartf.NTSDbField = strTabella & ".ae_codartf"
      edAe_forn.NTSDbField = strTabella & ".ae_forn"
      edAe_codmarc.NTSDbField = strTabella & ".ae_codmarc"
      edAe_codart.NTSDbField = strTabella & "Dettaglio.ae_codart"
      edAe_siglaforn.NTSDbField = strTabella & "Dettaglio.ae_siglaforn"
      cbAe_status.NTSDbField = strTabella & "Dettaglio.ae_status"
      edAe_descr.NTSDbField = strTabella & "Dettaglio.ae_descr"
      edAe_desint.NTSDbField = strTabella & "Dettaglio.ae_desint"
      edAe_unmis.NTSDbField = strTabella & "Dettaglio.ae_unmis"
      edAe_confez2.NTSDbField = strTabella & "Dettaglio.ae_confez2"
      edAe_qtacon2.NTSDbField = strTabella & "Dettaglio.ae_qtacon2"
      edAe_codiva.NTSDbField = strTabella & "Dettaglio.ae_codiva"
      edAe_gruppo.NTSDbField = strTabella & "Dettaglio.ae_gruppo"
      edAe_sotgru.NTSDbField = strTabella & "Dettaglio.ae_sotgru"
      edAe_clascon.NTSDbField = strTabella & "Dettaglio.ae_clascon"
      edAe_minordin.NTSDbField = strTabella & "Dettaglio.ae_minordin"
      edAe_sublotto.NTSDbField = strTabella & "Dettaglio.ae_sublotto"
      edAe_rrfence.NTSDbField = strTabella & "Dettaglio.ae_rrfence"
      edAe_sostit.NTSDbField = strTabella & "Dettaglio.ae_sostit"
      edAe_famprod.NTSDbField = strTabella & "Dettaglio.ae_famprod"
      edAe_codnomc.NTSDbField = strTabella & "Dettaglio.ae_codnomc"
      edAe_pesolor.NTSDbField = strTabella & "Dettaglio.ae_pesolor"
      edAe_pesonet.NTSDbField = strTabella & "Dettaglio.ae_pesonet"
      edAe_note.NTSDbField = strTabella & "Dettaglio.ae_note"
      edAe_sostituito.NTSDbField = strTabella & "Dettaglio.ae_sostituito"
      edAe_volume.NTSDbField = strTabella & "Dettaglio.ae_volume"
      edAe_prezzopu.NTSDbField = strTabella & "Dettaglio.ae_prezzopu"
      edAe_dataggpu.NTSDbField = strTabella & "Dettaglio.ae_dataggpu"
      edAe_prezzogr.NTSDbField = strTabella & "Dettaglio.ae_prezzogr"
      edAe_datagggr.NTSDbField = strTabella & "Dettaglio.ae_datagggr"
      edAe_codeump.NTSDbField = strTabella & "Dettaglio.ae_codeump"
      edAe_codeconf.NTSDbField = strTabella & "Dettaglio.ae_codeconf"
      edAe_descrl1.NTSDbField = strTabella & "Dettaglio.ae_descrl1"
      edAe_desintl1.NTSDbField = strTabella & "Dettaglio.ae_desintl1"
      edAe_descrl2.NTSDbField = strTabella & "Dettaglio.ae_descrl2"
      edAe_desintl2.NTSDbField = strTabella & "Dettaglio.ae_desintl2"
      edAe_descrl3.NTSDbField = strTabella & "Dettaglio.ae_descrl3"
      edAe_desintl3.NTSDbField = strTabella & "Dettaglio.ae_desintl3"
      lbAn_descr1.NTSDbField = strTabella & "Dettaglio.an_descr1"
      lbTb_desmarc.NTSDbField = strTabella & "Dettaglio.tb_desmarc"
      lbTb_desciva.NTSDbField = strTabella & "Dettaglio.tb_desciva"
      lbTb_descsar.NTSDbField = strTabella & "Dettaglio.tb_descsar"
      lbTb_desgmer.NTSDbField = strTabella & "Dettaglio.tb_desgmer"
      lbTb_dessgme.NTSDbField = strTabella & "Dettaglio.tb_dessgme"
      lbTb_descfam.NTSDbField = strTabella & "Dettaglio.tb_descfam"
      lbAe_ultagg.NTSDbField = strTabella & "Dettaglio.ae_ultagg"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCtfo, Me)
      NTSFormAddDataBinding(dcCtfoDettaglio, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGCTFO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      CaricaCombo()
      InitControls()

      edAe_qtacon2.Text = "1"

      Dim strLing(2) As String
      strLing(0) = "...................."
      strLing(1) = "...................."
      strLing(2) = "...................."

      If Not oCleCtfo.CaricaLabelTabling(strLing) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128681250224375000, "Errore durante il caricamento delle lingue."))
      End If

      Dim strDescrizione As String = oApp.Tr(Me, 128681248024687500, "Descrizione ")
      Dim strDescrSup As String = oApp.Tr(Me, 128681248693906250, "Des. supp. ")

      lbAe_descrl1.Text = strDescrizione & strLing(0)
      lbAe_desintl1.Text = strDescrSup & strLing(0)
      lbAe_descrl2.Text = strDescrizione & strLing(1)
      lbAe_desintl2.Text = strDescrSup & strLing(1)
      lbAe_descrl3.Text = strDescrizione & strLing(2)
      lbAe_desintl3.Text = strDescrSup & strLing(2)

      If Not oCleCtfo.LeggiDatiDitta() Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128681268098125000, "Errore durante la lettura delle impostazioni da registro."))
        Me.Close()
        Return
      End If

      If Not oCleCtfo.bGestTabUnmis Then
        edAe_unmis.NTSSetParamZoom("")
        edAe_confez2.NTSSetParamZoom("")
      End If

      lbAe_prezzopu.Text = oApp.Tr(Me, 128681269136875000, "Pubblico (l. |" & oCleCtfo.nCodArtDaCatNListPubb & "|)")
      lbAe_prezzogr.Text = oApp.Tr(Me, 128681269110937500, "Ingrosso (l. |" & oCleCtfo.nCodArtDaCatNListIngr & "|)")

      SetForm()

      If Not oCallParams Is Nothing Then
        tlbApri_ItemClick(Me, Nothing)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGCTFO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGCTFO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      oCleCtfo.ResetTblInstId()

      dcCtfo.Dispose()
      dcCtfoDettaglio.Dispose()
      If Not dsCtfo Is Nothing Then dsCtfo.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      Nuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      Apri()
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
    Try
      If StatoNuovo() Then Return

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128563492479687500, "Confermi la cancellazione?")) = Windows.Forms.DialogResult.Yes Then
        NTSFormClearDataBinding(Me)

        dcCtfo.RemoveAt(dcCtfo.Position)
        dcCtfoDettaglio.RemoveAt(dcCtfoDettaglio.Position)
        oCleCtfo.Salva(True)

        If dsCtfo.Tables(strTabella).Rows.Count > 0 Then
          ApriDettaglio()
        Else
          SetForm()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim dlgRes As DialogResult
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        If StatoNuovo() Then
          dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128580791531406250, "Annullare l'inserimento del nuovo articolo nel catalogo fornitore?"))
        Else
          dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128563492479843750, "Ripristinare le modifiche apportate?"))
        End If
      Else
        'chiamato dalla salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      If dlgRes = Windows.Forms.DialogResult.Yes Then
        If dsCtfo.Tables(strTabella).Rows(dcCtfo.Position).RowState = DataRowState.Added Then NTSFormClearDataBinding(Me)

        oCleCtfo.Ripristina(dcCtfo.Position, dcCtfo.Filter, dcCtfoDettaglio.Position, dcCtfoDettaglio.Filter)

        If dsCtfo.Tables(strTabella).Rows.Count > 0 Then
          ApriDettaglio()
        Else
          SetForm()
        End If
      End If

    Catch ex As Exception
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

      'If (Not oCleCtfo.bGestTabUnmis) And (edAe_unmis.Focused Or edAe_confez2.Focused) Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCreaArticolo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaArticolo.Click
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleCtfo.RitornaArtest(edAe_codartf.Text, NTSCInt(edAe_forn.Text), NTSCInt(edAe_codmarc.Text), _
        dttTmp) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!ae_codart).Trim <> "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130728853325187105, "Attenzione!" & vbCrLf & _
          "Articolo |" & NTSCStr(dttTmp.Rows(0)!ae_codart).ToUpper & "| già esistente."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130728850769762789, "Attenzione!" & vbCrLf & _
        "Procedere con la creazione dell'articolo da catalogo?")) = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleCtfo.ImportaCatalogoFornitori(dttTmp) = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130728843852544853, "Creazione articolo effettuata."))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub tlbNavigazione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick, tlbPrecedente.ItemClick, tlbSuccessivo.ItemClick, tlbUltimo.ItemClick
    Try
      If Not StatoVuoto() Then If Not Salva() Then Return

      Dim nPosition As Integer = dcCtfo.Position

      Select Case e.Item.Name
        Case tlbPrimo.Name
          dcCtfo.MoveFirst()
        Case tlbPrecedente.Name
          dcCtfo.MovePrevious()
        Case tlbSuccessivo.Name
          dcCtfo.MoveNext()
        Case tlbUltimo.Name
          dcCtfo.MoveLast()
      End Select

      If dcCtfo.Position = nPosition Then Return

      ApriDettaglio()
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

  Public Overridable Function Salva() As Boolean
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleCtfo.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        Select Case oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128674380441250000, "Confermi il salvataggio?"))
          Case System.Windows.Forms.DialogResult.Cancel
            Return False
          Case System.Windows.Forms.DialogResult.Yes
            If Not oCleCtfo.Salva(False) Then Return False
            For i As Integer = 0 To (dsCtfo.Tables(strTabella & "Dettaglio").Rows.Count - 1)
              With dsCtfo.Tables(strTabella & "Dettaglio").Rows(i)
                If (NTSCStr(!ae_codartf) = NTSCStr(dsCtfo.Tables(strTabella).Rows(dcCtfo.Position)!ae_codartf)) And _
                   (NTSCInt(!ae_forn) = NTSCInt(dsCtfo.Tables(strTabella).Rows(dcCtfo.Position)!ae_forn)) And _
                   (NTSCInt(!ae_codmarc) = NTSCInt(dsCtfo.Tables(strTabella).Rows(dcCtfo.Position)!ae_codmarc)) Then
                  !ae_ultagg = oCleCtfo.GetTimeStampArtest(NTSCStr(!ae_codartf), NTSCInt(!ae_forn), NTSCInt(!ae_codmarc)).ToString
                End If
              End With
            Next
            dsCtfo.AcceptChanges()
            SetForm()
          Case System.Windows.Forms.DialogResult.No
            tlbRipristina_ItemClick(Nothing, Nothing)
        End Select
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Nuovo(Optional ByVal bAskUseEntityParameters As Boolean = True) As Boolean
    Dim frmCoae As FRMMGCOAE = Nothing

    Try
      If Not StatoVuoto() Then If Not Salva() Then Return False

      Dim oCallParamTmp As New CLE__CLDP
      oCallParamTmp.bAddNew = True
      frmCoae = CType(NTSNewFormModal("FRMMGCOAE"), FRMMGCOAE)
      frmCoae.Init(oMenu, oCallParamTmp, DittaCorrente)
      frmCoae.oCleCoae = oCleCtfo
      frmCoae.ShowDialog()

      If Not oCallParamTmp.bPar1 Then Return False

      If StatoVuoto() Then
        If dsCtfo Is Nothing Then dsCtfo = New DataSet
        oCleCtfo.ApriLayout(DittaCorrente, dsCtfo)
        dcCtfo.DataSource = dsCtfo.Tables(strTabella)
        dcCtfoDettaglio.DataSource = dsCtfo.Tables(strTabella & "Dettaglio")
      Else
        NTSFormClearDataBinding(Me)
        dsCtfo.Tables(strTabella & "Dettaglio").Rows.Clear()
        dsCtfo.Tables(strTabella & "Dettaglio").AcceptChanges()
      End If

      oCleCtfo.Nuovo(NTSCStr(CType(oCallParamTmp.ctlPar1, DataTable).Rows(0)!ae_forn), _
                     NTSCStr(CType(oCallParamTmp.ctlPar1, DataTable).Rows(0)!ae_codartf), _
                     NTSCStr(CType(oCallParamTmp.ctlPar1, DataTable).Rows(0)!ae_codmarc))
      Bindcontrols()
      dcCtfo.MoveLast()
      dcCtfoDettaglio.MoveLast()
      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

      dcCtfo.ResetBindings(False)
      dcCtfoDettaglio.ResetBindings(False)

      SetForm()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoae Is Nothing Then frmCoae.Dispose()
      frmCoae = Nothing
    End Try
  End Function
  Public Overridable Function Apri() As Boolean
    Dim frmCoae As FRMMGCOAE = Nothing
    Dim oCallParamTmp As New CLE__CLDP
    Dim dttArtest As New DataTable
    Try
      If Not StatoVuoto() Then
        'Salva
        If Not Salva() Then Return False
        'Rimuove il binding
        NTSFormClearDataBinding(Me)
        'Cncella il dataset
        dsCtfo.Tables.Clear()
        dsCtfo.AcceptChanges()
        'Pulisce la form
        SetForm()
      End If

      If Not oCallParams Is Nothing Then

        oCleCtfo.VerificaArticolo(dttArtest, NTSCStr(NTSCInt(Mid(oCallParams.strParam, 37, 9))), _
          Trim(Mid(oCallParams.strParam, 6, 30)), NTSCStr(NTSCInt(Mid(oCallParams.strParam, 47, 4))))

        oCallParamTmp.ctlPar1 = dttArtest.Copy
      Else
        'Apre la modale
        oCallParamTmp.bAddNew = False
        frmCoae = CType(NTSNewFormModal("FRMMGCOAE"), FRMMGCOAE)
        frmCoae.Init(oMenu, oCallParamTmp, DittaCorrente)
        frmCoae.oCleCoae = oCleCtfo
        frmCoae.ShowDialog()

        'Annullata modale
        If Not oCallParamTmp.bPar1 Then Return False
      End If

      'Nessun art. forn. selezionato nello zoom della multiselezione
      If Not CType(oCallParamTmp.ctlPar1, DataTable).Rows.Count > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128683754997959084, "Nessun articolo selezionato."))
        Return False
      End If

      'Creo il nuovo dataset
      If dsCtfo Is Nothing Then dsCtfo = New DataSet
      dsCtfo.Tables.Add(CType(oCallParamTmp.ctlPar1, DataTable).Copy)
      dsCtfo.AcceptChanges()

      'Aggancio il data control
      dcCtfo.DataSource = dsCtfo.Tables(strTabella)

      'Apro il dettaglio della riga corrente
      Return ApriDettaglio()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoae Is Nothing Then frmCoae.Dispose()
      frmCoae = Nothing
    End Try
  End Function
  Public Overridable Function ApriDettaglio() As Boolean
    Try
      NTSFormClearDataBinding(Me)

      'Leggo il dettaglio
      If Not oCleCtfo.ApriDettaglio(DittaCorrente, dsCtfo, dcCtfo.Position) Then Return False

      'Aggancio il data control
      dcCtfoDettaglio.DataSource = dsCtfo.Tables(strTabella & "Dettaglio")

      'Collego il binding source ai vari controlli 
      Bindcontrols()

      SetForm()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function StatoNuovo() As Boolean
    Try
      If StatoVuoto() Then Return False
      If Not dsCtfo.Tables(strTabella).Rows.Count > 0 Then Return False
      If dsCtfo.Tables(strTabella).Rows(dcCtfo.Position).RowState = DataRowState.Added Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoVuoto() As Boolean
    Try
      If dsCtfo Is Nothing Then Return True
      If dsCtfo.Tables(strTabella) Is Nothing Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoDataApri() As Boolean
    Try
      If Not StatoVuoto() Then
        If dsCtfo.Tables(strTabella).Rows.Count > 0 Then Return True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoDataLayout() As Boolean
    Try
      If Not StatoVuoto() Then
        If dsCtfo.Tables(strTabella).Rows.Count = 0 Then Return True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetForm()
    Try
      SetToolBar()
      SetControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SetToolBar()
    Try
      If StatoVuoto() Or StatoDataLayout() Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbZoom.Enabled = False
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbNuovo.Enabled = True
        tlbApri.Enabled = True
      Else
        tlbSalva.Enabled = True
        tlbRipristina.Enabled = True
        tlbCancella.Enabled = Not StatoNuovo()
        tlbZoom.Enabled = True
        tlbPrimo.Enabled = True
        tlbPrecedente.Enabled = True
        tlbSuccessivo.Enabled = True
        tlbUltimo.Enabled = True
        If dcCtfo.Position = 0 Then
          tlbPrimo.Enabled = False
          tlbPrecedente.Enabled = False
        End If
        If dcCtfo.Position = dsCtfo.Tables(strTabella).Rows.Count - 1 Then
          tlbSuccessivo.Enabled = False
          tlbUltimo.Enabled = False
        End If
        tlbNuovo.Enabled = True
        tlbApri.Enabled = True
        End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SetControls()
    Try
      If StatoVuoto() Or StatoDataLayout() Then
        pnCtfo.Visible = False
      Else
        If pnCtfo.Visible = False Then NtsTabControl1.SelectedTabPage = tabpVenditeAcquisti
        pnCtfo.Visible = True
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub MessaggioBusOffline()
  End Sub
End Class

