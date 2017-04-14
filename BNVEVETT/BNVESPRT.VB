Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESPRT

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public oCleVett As CLEVEVETT
  Public dsSprt As New DataSet
  Public dcSprt As New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnSprt As NTSInformatica.NTSPanel
  Public WithEvents edSt_codvett As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSt_codport As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSt_codportLabel As NTSInformatica.NTSLabel
  Public WithEvents lbSt_codvettLabel As NTSInformatica.NTSLabel
  Public WithEvents lbSt_codport As NTSInformatica.NTSLabel
  Public WithEvents lbSt_codvett As NTSInformatica.NTSLabel
  Public WithEvents tsSptr As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents fmAddebito As NTSInformatica.NTSGroupBox
  Public WithEvents lb1 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base1Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSt_fisso As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSt_minimo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb4 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base4Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb3 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base3Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb2 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base2Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmCalcolo As NTSInformatica.NTSGroupBox
  Public WithEvents lbSt_val9Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val8Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val7Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val6Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val5Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val4Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val3Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val2Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_val1Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_val1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb8 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base8Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb7 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base7Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb6 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base6Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lb5 As NTSInformatica.NTSLabel
  Public WithEvents lbSt_base5Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_base5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_fissoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbSt_minimoLabel As NTSInformatica.NTSLabel
  Public WithEvents cbSt_percval As NTSInformatica.NTSComboBox
  Public WithEvents cbSt_addclie As NTSInformatica.NTSComboBox
  Public WithEvents lbSt_fissocsLabel As NTSInformatica.NTSLabel
  Public WithEvents lbSt_minimocsLabel As NTSInformatica.NTSLabel
  Public WithEvents fmModalita As NTSInformatica.NTSGroupBox
  Public WithEvents cbSt_percvalcs As NTSInformatica.NTSComboBox
  Public WithEvents lbSt_valcs9Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs8Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs7Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs6Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs5Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs4Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs3Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs2Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_valcs1Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_valcs1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmValori As NTSInformatica.NTSGroupBox
  Public WithEvents lbSt_basecs8Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs7Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs6Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs5Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs4Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs3Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs2Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSt_basecs1Label As NTSInformatica.NTSLabel
  Public WithEvents edSt_basecs1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSt_fissocs As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSt_minimocs As NTSInformatica.NTSTextBoxNum

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVESPRT))
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
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnSprt = New NTSInformatica.NTSPanel
    Me.tsSptr = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.lbSt_fissoLabel = New NTSInformatica.NTSLabel
    Me.lbSt_minimoLabel = New NTSInformatica.NTSLabel
    Me.fmCalcolo = New NTSInformatica.NTSGroupBox
    Me.cbSt_percval = New NTSInformatica.NTSComboBox
    Me.lbSt_val9Label = New NTSInformatica.NTSLabel
    Me.edSt_val9 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val8Label = New NTSInformatica.NTSLabel
    Me.edSt_val8 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val7Label = New NTSInformatica.NTSLabel
    Me.edSt_val7 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val6Label = New NTSInformatica.NTSLabel
    Me.edSt_val6 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val5Label = New NTSInformatica.NTSLabel
    Me.edSt_val5 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val4Label = New NTSInformatica.NTSLabel
    Me.edSt_val4 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val3Label = New NTSInformatica.NTSLabel
    Me.edSt_val3 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val2Label = New NTSInformatica.NTSLabel
    Me.edSt_val2 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_val1Label = New NTSInformatica.NTSLabel
    Me.edSt_val1 = New NTSInformatica.NTSTextBoxNum
    Me.fmAddebito = New NTSInformatica.NTSGroupBox
    Me.cbSt_addclie = New NTSInformatica.NTSComboBox
    Me.lb8 = New NTSInformatica.NTSLabel
    Me.lbSt_base8Label = New NTSInformatica.NTSLabel
    Me.edSt_base8 = New NTSInformatica.NTSTextBoxNum
    Me.lb7 = New NTSInformatica.NTSLabel
    Me.lbSt_base7Label = New NTSInformatica.NTSLabel
    Me.edSt_base7 = New NTSInformatica.NTSTextBoxNum
    Me.lb6 = New NTSInformatica.NTSLabel
    Me.lbSt_base6Label = New NTSInformatica.NTSLabel
    Me.edSt_base6 = New NTSInformatica.NTSTextBoxNum
    Me.lb5 = New NTSInformatica.NTSLabel
    Me.lbSt_base5Label = New NTSInformatica.NTSLabel
    Me.edSt_base5 = New NTSInformatica.NTSTextBoxNum
    Me.lb4 = New NTSInformatica.NTSLabel
    Me.lbSt_base4Label = New NTSInformatica.NTSLabel
    Me.edSt_base4 = New NTSInformatica.NTSTextBoxNum
    Me.lb3 = New NTSInformatica.NTSLabel
    Me.lbSt_base3Label = New NTSInformatica.NTSLabel
    Me.edSt_base3 = New NTSInformatica.NTSTextBoxNum
    Me.lb2 = New NTSInformatica.NTSLabel
    Me.lbSt_base2Label = New NTSInformatica.NTSLabel
    Me.edSt_base2 = New NTSInformatica.NTSTextBoxNum
    Me.lb1 = New NTSInformatica.NTSLabel
    Me.lbSt_base1Label = New NTSInformatica.NTSLabel
    Me.edSt_base1 = New NTSInformatica.NTSTextBoxNum
    Me.edSt_fisso = New NTSInformatica.NTSTextBoxNum
    Me.edSt_minimo = New NTSInformatica.NTSTextBoxNum
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.lbSt_fissocsLabel = New NTSInformatica.NTSLabel
    Me.lbSt_minimocsLabel = New NTSInformatica.NTSLabel
    Me.fmModalita = New NTSInformatica.NTSGroupBox
    Me.cbSt_percvalcs = New NTSInformatica.NTSComboBox
    Me.lbSt_valcs9Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs9 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs8Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs8 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs7Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs7 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs6Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs6 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs5Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs5 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs4Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs4 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs3Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs3 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs2Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs2 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_valcs1Label = New NTSInformatica.NTSLabel
    Me.edSt_valcs1 = New NTSInformatica.NTSTextBoxNum
    Me.fmValori = New NTSInformatica.NTSGroupBox
    Me.lbSt_basecs8Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs8 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs7Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs7 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs6Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs6 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs5Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs5 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs4Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs4 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs3Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs3 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs2Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs2 = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_basecs1Label = New NTSInformatica.NTSLabel
    Me.edSt_basecs1 = New NTSInformatica.NTSTextBoxNum
    Me.edSt_fissocs = New NTSInformatica.NTSTextBoxNum
    Me.edSt_minimocs = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_codport = New NTSInformatica.NTSLabel
    Me.lbSt_codvett = New NTSInformatica.NTSLabel
    Me.edSt_codport = New NTSInformatica.NTSTextBoxStr
    Me.edSt_codvett = New NTSInformatica.NTSTextBoxNum
    Me.lbSt_codportLabel = New NTSInformatica.NTSLabel
    Me.lbSt_codvettLabel = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSprt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSprt.SuspendLayout()
    CType(Me.tsSptr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSptr.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.fmCalcolo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCalcolo.SuspendLayout()
    CType(Me.cbSt_percval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_val1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAddebito, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAddebito.SuspendLayout()
    CType(Me.cbSt_addclie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_base1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_fisso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_minimo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.fmModalita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmModalita.SuspendLayout()
    CType(Me.cbSt_percvalcs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_valcs1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValori, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValori.SuspendLayout()
    CType(Me.edSt_basecs8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_basecs1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_fissocs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_minimocs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_codport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSt_codvett.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'pnSprt
    '
    Me.pnSprt.AllowDrop = True
    Me.pnSprt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSprt.Appearance.Options.UseBackColor = True
    Me.pnSprt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSprt.Controls.Add(Me.tsSptr)
    Me.pnSprt.Controls.Add(Me.lbSt_codport)
    Me.pnSprt.Controls.Add(Me.lbSt_codvett)
    Me.pnSprt.Controls.Add(Me.edSt_codport)
    Me.pnSprt.Controls.Add(Me.edSt_codvett)
    Me.pnSprt.Controls.Add(Me.lbSt_codportLabel)
    Me.pnSprt.Controls.Add(Me.lbSt_codvettLabel)
    Me.pnSprt.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSprt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSprt.Location = New System.Drawing.Point(0, 30)
    Me.pnSprt.Name = "pnSprt"
    Me.pnSprt.NTSActiveTrasparency = True
    Me.pnSprt.Size = New System.Drawing.Size(627, 425)
    Me.pnSprt.TabIndex = 4
    Me.pnSprt.Text = "NtsPanel1"
    '
    'tsSptr
    '
    Me.tsSptr.Location = New System.Drawing.Point(11, 69)
    Me.tsSptr.Name = "tsSptr"
    Me.tsSptr.SelectedTabPage = Me.NtsTabPage1
    Me.tsSptr.Size = New System.Drawing.Size(604, 341)
    Me.tsSptr.TabIndex = 6
    Me.tsSptr.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsSptr.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.lbSt_fissoLabel)
    Me.NtsTabPage1.Controls.Add(Me.lbSt_minimoLabel)
    Me.NtsTabPage1.Controls.Add(Me.fmCalcolo)
    Me.NtsTabPage1.Controls.Add(Me.fmAddebito)
    Me.NtsTabPage1.Controls.Add(Me.edSt_fisso)
    Me.NtsTabPage1.Controls.Add(Me.edSt_minimo)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(595, 311)
    Me.NtsTabPage1.Text = "Spese &trasporto"
    Me.NtsTabPage1.Tooltip = "Spese riguardanti il trasporto"
    '
    'lbSt_fissoLabel
    '
    Me.lbSt_fissoLabel.AutoSize = True
    Me.lbSt_fissoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_fissoLabel.Location = New System.Drawing.Point(308, 15)
    Me.lbSt_fissoLabel.Name = "lbSt_fissoLabel"
    Me.lbSt_fissoLabel.NTSDbField = ""
    Me.lbSt_fissoLabel.Size = New System.Drawing.Size(70, 13)
    Me.lbSt_fissoLabel.TabIndex = 5
    Me.lbSt_fissoLabel.Text = "Importo fisso"
    Me.lbSt_fissoLabel.Tooltip = ""
    Me.lbSt_fissoLabel.UseMnemonic = False
    '
    'lbSt_minimoLabel
    '
    Me.lbSt_minimoLabel.AutoSize = True
    Me.lbSt_minimoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_minimoLabel.Location = New System.Drawing.Point(19, 15)
    Me.lbSt_minimoLabel.Name = "lbSt_minimoLabel"
    Me.lbSt_minimoLabel.NTSDbField = ""
    Me.lbSt_minimoLabel.Size = New System.Drawing.Size(80, 13)
    Me.lbSt_minimoLabel.TabIndex = 4
    Me.lbSt_minimoLabel.Text = "Importo minimo"
    Me.lbSt_minimoLabel.Tooltip = ""
    Me.lbSt_minimoLabel.UseMnemonic = False
    '
    'fmCalcolo
    '
    Me.fmCalcolo.AllowDrop = True
    Me.fmCalcolo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCalcolo.Appearance.Options.UseBackColor = True
    Me.fmCalcolo.Controls.Add(Me.cbSt_percval)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val9Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val9)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val8Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val8)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val7Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val7)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val6Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val6)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val5Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val5)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val4Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val4)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val3Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val3)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val2Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val2)
    Me.fmCalcolo.Controls.Add(Me.lbSt_val1Label)
    Me.fmCalcolo.Controls.Add(Me.edSt_val1)
    Me.fmCalcolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCalcolo.Location = New System.Drawing.Point(298, 44)
    Me.fmCalcolo.Name = "fmCalcolo"
    Me.fmCalcolo.Size = New System.Drawing.Size(283, 263)
    Me.fmCalcolo.TabIndex = 3
    Me.fmCalcolo.Text = "Modalità di calcolo: "
    '
    'cbSt_percval
    '
    Me.cbSt_percval.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSt_percval.DataSource = Nothing
    Me.cbSt_percval.DisplayMember = ""
    Me.cbSt_percval.Location = New System.Drawing.Point(152, 0)
    Me.cbSt_percval.Name = "cbSt_percval"
    Me.cbSt_percval.NTSDbField = ""
    Me.cbSt_percval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSt_percval.Properties.DropDownRows = 30
    Me.cbSt_percval.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSt_percval.SelectedValue = ""
    Me.cbSt_percval.Size = New System.Drawing.Size(122, 20)
    Me.cbSt_percval.TabIndex = 29
    Me.cbSt_percval.ValueMember = ""
    '
    'lbSt_val9Label
    '
    Me.lbSt_val9Label.AutoSize = True
    Me.lbSt_val9Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val9Label.Location = New System.Drawing.Point(10, 234)
    Me.lbSt_val9Label.Name = "lbSt_val9Label"
    Me.lbSt_val9Label.NTSDbField = ""
    Me.lbSt_val9Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val9Label.TabIndex = 26
    Me.lbSt_val9Label.Text = "Valore scaglione 9"
    Me.lbSt_val9Label.Tooltip = ""
    Me.lbSt_val9Label.UseMnemonic = False
    '
    'edSt_val9
    '
    Me.edSt_val9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val9.Location = New System.Drawing.Point(152, 231)
    Me.edSt_val9.Name = "edSt_val9"
    Me.edSt_val9.NTSDbField = ""
    Me.edSt_val9.NTSFormat = "0"
    Me.edSt_val9.NTSForzaVisZoom = False
    Me.edSt_val9.NTSOldValue = ""
    Me.edSt_val9.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val9.Properties.MaxLength = 65536
    Me.edSt_val9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val9.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val9.TabIndex = 25
    '
    'lbSt_val8Label
    '
    Me.lbSt_val8Label.AutoSize = True
    Me.lbSt_val8Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val8Label.Location = New System.Drawing.Point(10, 208)
    Me.lbSt_val8Label.Name = "lbSt_val8Label"
    Me.lbSt_val8Label.NTSDbField = ""
    Me.lbSt_val8Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val8Label.TabIndex = 23
    Me.lbSt_val8Label.Text = "Valore scaglione 8"
    Me.lbSt_val8Label.Tooltip = ""
    Me.lbSt_val8Label.UseMnemonic = False
    '
    'edSt_val8
    '
    Me.edSt_val8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val8.Location = New System.Drawing.Point(152, 205)
    Me.edSt_val8.Name = "edSt_val8"
    Me.edSt_val8.NTSDbField = ""
    Me.edSt_val8.NTSFormat = "0"
    Me.edSt_val8.NTSForzaVisZoom = False
    Me.edSt_val8.NTSOldValue = ""
    Me.edSt_val8.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val8.Properties.MaxLength = 65536
    Me.edSt_val8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val8.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val8.TabIndex = 22
    '
    'lbSt_val7Label
    '
    Me.lbSt_val7Label.AutoSize = True
    Me.lbSt_val7Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val7Label.Location = New System.Drawing.Point(10, 182)
    Me.lbSt_val7Label.Name = "lbSt_val7Label"
    Me.lbSt_val7Label.NTSDbField = ""
    Me.lbSt_val7Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val7Label.TabIndex = 20
    Me.lbSt_val7Label.Text = "Valore scaglione 7"
    Me.lbSt_val7Label.Tooltip = ""
    Me.lbSt_val7Label.UseMnemonic = False
    '
    'edSt_val7
    '
    Me.edSt_val7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val7.Location = New System.Drawing.Point(152, 179)
    Me.edSt_val7.Name = "edSt_val7"
    Me.edSt_val7.NTSDbField = ""
    Me.edSt_val7.NTSFormat = "0"
    Me.edSt_val7.NTSForzaVisZoom = False
    Me.edSt_val7.NTSOldValue = ""
    Me.edSt_val7.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val7.Properties.MaxLength = 65536
    Me.edSt_val7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val7.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val7.TabIndex = 19
    '
    'lbSt_val6Label
    '
    Me.lbSt_val6Label.AutoSize = True
    Me.lbSt_val6Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val6Label.Location = New System.Drawing.Point(10, 156)
    Me.lbSt_val6Label.Name = "lbSt_val6Label"
    Me.lbSt_val6Label.NTSDbField = ""
    Me.lbSt_val6Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val6Label.TabIndex = 17
    Me.lbSt_val6Label.Text = "Valore scaglione 6"
    Me.lbSt_val6Label.Tooltip = ""
    Me.lbSt_val6Label.UseMnemonic = False
    '
    'edSt_val6
    '
    Me.edSt_val6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val6.Location = New System.Drawing.Point(152, 153)
    Me.edSt_val6.Name = "edSt_val6"
    Me.edSt_val6.NTSDbField = ""
    Me.edSt_val6.NTSFormat = "0"
    Me.edSt_val6.NTSForzaVisZoom = False
    Me.edSt_val6.NTSOldValue = ""
    Me.edSt_val6.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val6.Properties.MaxLength = 65536
    Me.edSt_val6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val6.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val6.TabIndex = 16
    '
    'lbSt_val5Label
    '
    Me.lbSt_val5Label.AutoSize = True
    Me.lbSt_val5Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val5Label.Location = New System.Drawing.Point(10, 130)
    Me.lbSt_val5Label.Name = "lbSt_val5Label"
    Me.lbSt_val5Label.NTSDbField = ""
    Me.lbSt_val5Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val5Label.TabIndex = 14
    Me.lbSt_val5Label.Text = "Valore scaglione 5"
    Me.lbSt_val5Label.Tooltip = ""
    Me.lbSt_val5Label.UseMnemonic = False
    '
    'edSt_val5
    '
    Me.edSt_val5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val5.Location = New System.Drawing.Point(152, 127)
    Me.edSt_val5.Name = "edSt_val5"
    Me.edSt_val5.NTSDbField = ""
    Me.edSt_val5.NTSFormat = "0"
    Me.edSt_val5.NTSForzaVisZoom = False
    Me.edSt_val5.NTSOldValue = ""
    Me.edSt_val5.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val5.Properties.MaxLength = 65536
    Me.edSt_val5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val5.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val5.TabIndex = 13
    '
    'lbSt_val4Label
    '
    Me.lbSt_val4Label.AutoSize = True
    Me.lbSt_val4Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val4Label.Location = New System.Drawing.Point(10, 104)
    Me.lbSt_val4Label.Name = "lbSt_val4Label"
    Me.lbSt_val4Label.NTSDbField = ""
    Me.lbSt_val4Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val4Label.TabIndex = 11
    Me.lbSt_val4Label.Text = "Valore scaglione 4"
    Me.lbSt_val4Label.Tooltip = ""
    Me.lbSt_val4Label.UseMnemonic = False
    '
    'edSt_val4
    '
    Me.edSt_val4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val4.Location = New System.Drawing.Point(152, 101)
    Me.edSt_val4.Name = "edSt_val4"
    Me.edSt_val4.NTSDbField = ""
    Me.edSt_val4.NTSFormat = "0"
    Me.edSt_val4.NTSForzaVisZoom = False
    Me.edSt_val4.NTSOldValue = ""
    Me.edSt_val4.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val4.Properties.MaxLength = 65536
    Me.edSt_val4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val4.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val4.TabIndex = 10
    '
    'lbSt_val3Label
    '
    Me.lbSt_val3Label.AutoSize = True
    Me.lbSt_val3Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val3Label.Location = New System.Drawing.Point(10, 78)
    Me.lbSt_val3Label.Name = "lbSt_val3Label"
    Me.lbSt_val3Label.NTSDbField = ""
    Me.lbSt_val3Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val3Label.TabIndex = 8
    Me.lbSt_val3Label.Text = "Valore scaglione 3"
    Me.lbSt_val3Label.Tooltip = ""
    Me.lbSt_val3Label.UseMnemonic = False
    '
    'edSt_val3
    '
    Me.edSt_val3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val3.Location = New System.Drawing.Point(152, 75)
    Me.edSt_val3.Name = "edSt_val3"
    Me.edSt_val3.NTSDbField = ""
    Me.edSt_val3.NTSFormat = "0"
    Me.edSt_val3.NTSForzaVisZoom = False
    Me.edSt_val3.NTSOldValue = ""
    Me.edSt_val3.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val3.Properties.MaxLength = 65536
    Me.edSt_val3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val3.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val3.TabIndex = 7
    '
    'lbSt_val2Label
    '
    Me.lbSt_val2Label.AutoSize = True
    Me.lbSt_val2Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val2Label.Location = New System.Drawing.Point(10, 52)
    Me.lbSt_val2Label.Name = "lbSt_val2Label"
    Me.lbSt_val2Label.NTSDbField = ""
    Me.lbSt_val2Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val2Label.TabIndex = 5
    Me.lbSt_val2Label.Text = "Valore scaglione 2"
    Me.lbSt_val2Label.Tooltip = ""
    Me.lbSt_val2Label.UseMnemonic = False
    '
    'edSt_val2
    '
    Me.edSt_val2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val2.Location = New System.Drawing.Point(152, 49)
    Me.edSt_val2.Name = "edSt_val2"
    Me.edSt_val2.NTSDbField = ""
    Me.edSt_val2.NTSFormat = "0"
    Me.edSt_val2.NTSForzaVisZoom = False
    Me.edSt_val2.NTSOldValue = ""
    Me.edSt_val2.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val2.Properties.MaxLength = 65536
    Me.edSt_val2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val2.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val2.TabIndex = 4
    '
    'lbSt_val1Label
    '
    Me.lbSt_val1Label.AutoSize = True
    Me.lbSt_val1Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_val1Label.Location = New System.Drawing.Point(10, 26)
    Me.lbSt_val1Label.Name = "lbSt_val1Label"
    Me.lbSt_val1Label.NTSDbField = ""
    Me.lbSt_val1Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_val1Label.TabIndex = 2
    Me.lbSt_val1Label.Text = "Valore scaglione 1"
    Me.lbSt_val1Label.Tooltip = ""
    Me.lbSt_val1Label.UseMnemonic = False
    '
    'edSt_val1
    '
    Me.edSt_val1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_val1.Location = New System.Drawing.Point(152, 23)
    Me.edSt_val1.Name = "edSt_val1"
    Me.edSt_val1.NTSDbField = ""
    Me.edSt_val1.NTSFormat = "0"
    Me.edSt_val1.NTSForzaVisZoom = False
    Me.edSt_val1.NTSOldValue = ""
    Me.edSt_val1.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_val1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_val1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_val1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_val1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_val1.Properties.MaxLength = 65536
    Me.edSt_val1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_val1.Size = New System.Drawing.Size(122, 20)
    Me.edSt_val1.TabIndex = 1
    '
    'fmAddebito
    '
    Me.fmAddebito.AllowDrop = True
    Me.fmAddebito.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAddebito.Appearance.Options.UseBackColor = True
    Me.fmAddebito.Controls.Add(Me.cbSt_addclie)
    Me.fmAddebito.Controls.Add(Me.lb8)
    Me.fmAddebito.Controls.Add(Me.lbSt_base8Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base8)
    Me.fmAddebito.Controls.Add(Me.lb7)
    Me.fmAddebito.Controls.Add(Me.lbSt_base7Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base7)
    Me.fmAddebito.Controls.Add(Me.lb6)
    Me.fmAddebito.Controls.Add(Me.lbSt_base6Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base6)
    Me.fmAddebito.Controls.Add(Me.lb5)
    Me.fmAddebito.Controls.Add(Me.lbSt_base5Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base5)
    Me.fmAddebito.Controls.Add(Me.lb4)
    Me.fmAddebito.Controls.Add(Me.lbSt_base4Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base4)
    Me.fmAddebito.Controls.Add(Me.lb3)
    Me.fmAddebito.Controls.Add(Me.lbSt_base3Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base3)
    Me.fmAddebito.Controls.Add(Me.lb2)
    Me.fmAddebito.Controls.Add(Me.lbSt_base2Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base2)
    Me.fmAddebito.Controls.Add(Me.lb1)
    Me.fmAddebito.Controls.Add(Me.lbSt_base1Label)
    Me.fmAddebito.Controls.Add(Me.edSt_base1)
    Me.fmAddebito.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAddebito.Location = New System.Drawing.Point(9, 44)
    Me.fmAddebito.Name = "fmAddebito"
    Me.fmAddebito.Size = New System.Drawing.Size(283, 263)
    Me.fmAddebito.TabIndex = 2
    Me.fmAddebito.Text = "Modalità di addebito: "
    '
    'cbSt_addclie
    '
    Me.cbSt_addclie.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSt_addclie.DataSource = Nothing
    Me.cbSt_addclie.DisplayMember = ""
    Me.cbSt_addclie.Location = New System.Drawing.Point(152, 0)
    Me.cbSt_addclie.Name = "cbSt_addclie"
    Me.cbSt_addclie.NTSDbField = ""
    Me.cbSt_addclie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSt_addclie.Properties.DropDownRows = 30
    Me.cbSt_addclie.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSt_addclie.SelectedValue = ""
    Me.cbSt_addclie.Size = New System.Drawing.Size(122, 20)
    Me.cbSt_addclie.TabIndex = 28
    Me.cbSt_addclie.ValueMember = ""
    '
    'lb8
    '
    Me.lb8.AutoSize = True
    Me.lb8.BackColor = System.Drawing.Color.Transparent
    Me.lb8.Location = New System.Drawing.Point(122, 208)
    Me.lb8.Name = "lb8"
    Me.lb8.NTSDbField = ""
    Me.lb8.Size = New System.Drawing.Size(27, 13)
    Me.lb8.TabIndex = 24
    Me.lb8.Text = "EUR"
    Me.lb8.Tooltip = ""
    Me.lb8.UseMnemonic = False
    '
    'lbSt_base8Label
    '
    Me.lbSt_base8Label.AutoSize = True
    Me.lbSt_base8Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base8Label.Location = New System.Drawing.Point(10, 208)
    Me.lbSt_base8Label.Name = "lbSt_base8Label"
    Me.lbSt_base8Label.NTSDbField = ""
    Me.lbSt_base8Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base8Label.TabIndex = 23
    Me.lbSt_base8Label.Text = "Importo max scag. 8"
    Me.lbSt_base8Label.Tooltip = ""
    Me.lbSt_base8Label.UseMnemonic = False
    '
    'edSt_base8
    '
    Me.edSt_base8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base8.Location = New System.Drawing.Point(152, 205)
    Me.edSt_base8.Name = "edSt_base8"
    Me.edSt_base8.NTSDbField = ""
    Me.edSt_base8.NTSFormat = "0"
    Me.edSt_base8.NTSForzaVisZoom = False
    Me.edSt_base8.NTSOldValue = ""
    Me.edSt_base8.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base8.Properties.MaxLength = 65536
    Me.edSt_base8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base8.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base8.TabIndex = 22
    '
    'lb7
    '
    Me.lb7.AutoSize = True
    Me.lb7.BackColor = System.Drawing.Color.Transparent
    Me.lb7.Location = New System.Drawing.Point(122, 182)
    Me.lb7.Name = "lb7"
    Me.lb7.NTSDbField = ""
    Me.lb7.Size = New System.Drawing.Size(27, 13)
    Me.lb7.TabIndex = 21
    Me.lb7.Text = "EUR"
    Me.lb7.Tooltip = ""
    Me.lb7.UseMnemonic = False
    '
    'lbSt_base7Label
    '
    Me.lbSt_base7Label.AutoSize = True
    Me.lbSt_base7Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base7Label.Location = New System.Drawing.Point(10, 182)
    Me.lbSt_base7Label.Name = "lbSt_base7Label"
    Me.lbSt_base7Label.NTSDbField = ""
    Me.lbSt_base7Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base7Label.TabIndex = 20
    Me.lbSt_base7Label.Text = "Importo max scag. 7"
    Me.lbSt_base7Label.Tooltip = ""
    Me.lbSt_base7Label.UseMnemonic = False
    '
    'edSt_base7
    '
    Me.edSt_base7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base7.Location = New System.Drawing.Point(152, 179)
    Me.edSt_base7.Name = "edSt_base7"
    Me.edSt_base7.NTSDbField = ""
    Me.edSt_base7.NTSFormat = "0"
    Me.edSt_base7.NTSForzaVisZoom = False
    Me.edSt_base7.NTSOldValue = ""
    Me.edSt_base7.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base7.Properties.MaxLength = 65536
    Me.edSt_base7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base7.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base7.TabIndex = 19
    '
    'lb6
    '
    Me.lb6.AutoSize = True
    Me.lb6.BackColor = System.Drawing.Color.Transparent
    Me.lb6.Location = New System.Drawing.Point(122, 156)
    Me.lb6.Name = "lb6"
    Me.lb6.NTSDbField = ""
    Me.lb6.Size = New System.Drawing.Size(27, 13)
    Me.lb6.TabIndex = 18
    Me.lb6.Text = "EUR"
    Me.lb6.Tooltip = ""
    Me.lb6.UseMnemonic = False
    '
    'lbSt_base6Label
    '
    Me.lbSt_base6Label.AutoSize = True
    Me.lbSt_base6Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base6Label.Location = New System.Drawing.Point(10, 156)
    Me.lbSt_base6Label.Name = "lbSt_base6Label"
    Me.lbSt_base6Label.NTSDbField = ""
    Me.lbSt_base6Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base6Label.TabIndex = 17
    Me.lbSt_base6Label.Text = "Importo max scag. 6"
    Me.lbSt_base6Label.Tooltip = ""
    Me.lbSt_base6Label.UseMnemonic = False
    '
    'edSt_base6
    '
    Me.edSt_base6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base6.Location = New System.Drawing.Point(152, 153)
    Me.edSt_base6.Name = "edSt_base6"
    Me.edSt_base6.NTSDbField = ""
    Me.edSt_base6.NTSFormat = "0"
    Me.edSt_base6.NTSForzaVisZoom = False
    Me.edSt_base6.NTSOldValue = ""
    Me.edSt_base6.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base6.Properties.MaxLength = 65536
    Me.edSt_base6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base6.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base6.TabIndex = 16
    '
    'lb5
    '
    Me.lb5.AutoSize = True
    Me.lb5.BackColor = System.Drawing.Color.Transparent
    Me.lb5.Location = New System.Drawing.Point(122, 130)
    Me.lb5.Name = "lb5"
    Me.lb5.NTSDbField = ""
    Me.lb5.Size = New System.Drawing.Size(27, 13)
    Me.lb5.TabIndex = 15
    Me.lb5.Text = "EUR"
    Me.lb5.Tooltip = ""
    Me.lb5.UseMnemonic = False
    '
    'lbSt_base5Label
    '
    Me.lbSt_base5Label.AutoSize = True
    Me.lbSt_base5Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base5Label.Location = New System.Drawing.Point(10, 130)
    Me.lbSt_base5Label.Name = "lbSt_base5Label"
    Me.lbSt_base5Label.NTSDbField = ""
    Me.lbSt_base5Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base5Label.TabIndex = 14
    Me.lbSt_base5Label.Text = "Importo max scag. 5"
    Me.lbSt_base5Label.Tooltip = ""
    Me.lbSt_base5Label.UseMnemonic = False
    '
    'edSt_base5
    '
    Me.edSt_base5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base5.Location = New System.Drawing.Point(152, 127)
    Me.edSt_base5.Name = "edSt_base5"
    Me.edSt_base5.NTSDbField = ""
    Me.edSt_base5.NTSFormat = "0"
    Me.edSt_base5.NTSForzaVisZoom = False
    Me.edSt_base5.NTSOldValue = ""
    Me.edSt_base5.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base5.Properties.MaxLength = 65536
    Me.edSt_base5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base5.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base5.TabIndex = 13
    '
    'lb4
    '
    Me.lb4.AutoSize = True
    Me.lb4.BackColor = System.Drawing.Color.Transparent
    Me.lb4.Location = New System.Drawing.Point(122, 104)
    Me.lb4.Name = "lb4"
    Me.lb4.NTSDbField = ""
    Me.lb4.Size = New System.Drawing.Size(27, 13)
    Me.lb4.TabIndex = 12
    Me.lb4.Text = "EUR"
    Me.lb4.Tooltip = ""
    Me.lb4.UseMnemonic = False
    '
    'lbSt_base4Label
    '
    Me.lbSt_base4Label.AutoSize = True
    Me.lbSt_base4Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base4Label.Location = New System.Drawing.Point(10, 104)
    Me.lbSt_base4Label.Name = "lbSt_base4Label"
    Me.lbSt_base4Label.NTSDbField = ""
    Me.lbSt_base4Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base4Label.TabIndex = 11
    Me.lbSt_base4Label.Text = "Importo max scag. 4"
    Me.lbSt_base4Label.Tooltip = ""
    Me.lbSt_base4Label.UseMnemonic = False
    '
    'edSt_base4
    '
    Me.edSt_base4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base4.Location = New System.Drawing.Point(152, 101)
    Me.edSt_base4.Name = "edSt_base4"
    Me.edSt_base4.NTSDbField = ""
    Me.edSt_base4.NTSFormat = "0"
    Me.edSt_base4.NTSForzaVisZoom = False
    Me.edSt_base4.NTSOldValue = ""
    Me.edSt_base4.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base4.Properties.MaxLength = 65536
    Me.edSt_base4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base4.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base4.TabIndex = 10
    '
    'lb3
    '
    Me.lb3.AutoSize = True
    Me.lb3.BackColor = System.Drawing.Color.Transparent
    Me.lb3.Location = New System.Drawing.Point(122, 78)
    Me.lb3.Name = "lb3"
    Me.lb3.NTSDbField = ""
    Me.lb3.Size = New System.Drawing.Size(27, 13)
    Me.lb3.TabIndex = 9
    Me.lb3.Text = "EUR"
    Me.lb3.Tooltip = ""
    Me.lb3.UseMnemonic = False
    '
    'lbSt_base3Label
    '
    Me.lbSt_base3Label.AutoSize = True
    Me.lbSt_base3Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base3Label.Location = New System.Drawing.Point(10, 78)
    Me.lbSt_base3Label.Name = "lbSt_base3Label"
    Me.lbSt_base3Label.NTSDbField = ""
    Me.lbSt_base3Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base3Label.TabIndex = 8
    Me.lbSt_base3Label.Text = "Importo max scag. 3"
    Me.lbSt_base3Label.Tooltip = ""
    Me.lbSt_base3Label.UseMnemonic = False
    '
    'edSt_base3
    '
    Me.edSt_base3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_base3.Location = New System.Drawing.Point(152, 75)
    Me.edSt_base3.Name = "edSt_base3"
    Me.edSt_base3.NTSDbField = ""
    Me.edSt_base3.NTSFormat = "0"
    Me.edSt_base3.NTSForzaVisZoom = False
    Me.edSt_base3.NTSOldValue = ""
    Me.edSt_base3.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base3.Properties.MaxLength = 65536
    Me.edSt_base3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base3.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base3.TabIndex = 7
    '
    'lb2
    '
    Me.lb2.AutoSize = True
    Me.lb2.BackColor = System.Drawing.Color.Transparent
    Me.lb2.Location = New System.Drawing.Point(122, 52)
    Me.lb2.Name = "lb2"
    Me.lb2.NTSDbField = ""
    Me.lb2.Size = New System.Drawing.Size(27, 13)
    Me.lb2.TabIndex = 6
    Me.lb2.Text = "EUR"
    Me.lb2.Tooltip = ""
    Me.lb2.UseMnemonic = False
    '
    'lbSt_base2Label
    '
    Me.lbSt_base2Label.AutoSize = True
    Me.lbSt_base2Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base2Label.Location = New System.Drawing.Point(10, 52)
    Me.lbSt_base2Label.Name = "lbSt_base2Label"
    Me.lbSt_base2Label.NTSDbField = ""
    Me.lbSt_base2Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base2Label.TabIndex = 5
    Me.lbSt_base2Label.Text = "Importo max scag. 2"
    Me.lbSt_base2Label.Tooltip = ""
    Me.lbSt_base2Label.UseMnemonic = False
    '
    'edSt_base2
    '
    Me.edSt_base2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edSt_base2.Location = New System.Drawing.Point(152, 49)
    Me.edSt_base2.Name = "edSt_base2"
    Me.edSt_base2.NTSDbField = ""
    Me.edSt_base2.NTSFormat = "0"
    Me.edSt_base2.NTSForzaVisZoom = False
    Me.edSt_base2.NTSOldValue = ""
    Me.edSt_base2.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base2.Properties.MaxLength = 65536
    Me.edSt_base2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base2.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base2.TabIndex = 4
    '
    'lb1
    '
    Me.lb1.AutoSize = True
    Me.lb1.BackColor = System.Drawing.Color.Transparent
    Me.lb1.Location = New System.Drawing.Point(122, 26)
    Me.lb1.Name = "lb1"
    Me.lb1.NTSDbField = ""
    Me.lb1.Size = New System.Drawing.Size(27, 13)
    Me.lb1.TabIndex = 3
    Me.lb1.Text = "EUR"
    Me.lb1.Tooltip = ""
    Me.lb1.UseMnemonic = False
    '
    'lbSt_base1Label
    '
    Me.lbSt_base1Label.AutoSize = True
    Me.lbSt_base1Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_base1Label.Location = New System.Drawing.Point(10, 26)
    Me.lbSt_base1Label.Name = "lbSt_base1Label"
    Me.lbSt_base1Label.NTSDbField = ""
    Me.lbSt_base1Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_base1Label.TabIndex = 2
    Me.lbSt_base1Label.Text = "Importo max scag. 1"
    Me.lbSt_base1Label.Tooltip = ""
    Me.lbSt_base1Label.UseMnemonic = False
    '
    'edSt_base1
    '
    Me.edSt_base1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSt_base1.Location = New System.Drawing.Point(152, 23)
    Me.edSt_base1.Name = "edSt_base1"
    Me.edSt_base1.NTSDbField = ""
    Me.edSt_base1.NTSFormat = "0"
    Me.edSt_base1.NTSForzaVisZoom = False
    Me.edSt_base1.NTSOldValue = ""
    Me.edSt_base1.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_base1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_base1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_base1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_base1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_base1.Properties.MaxLength = 65536
    Me.edSt_base1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_base1.Size = New System.Drawing.Size(122, 20)
    Me.edSt_base1.TabIndex = 1
    '
    'edSt_fisso
    '
    Me.edSt_fisso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_fisso.Location = New System.Drawing.Point(450, 15)
    Me.edSt_fisso.Name = "edSt_fisso"
    Me.edSt_fisso.NTSDbField = ""
    Me.edSt_fisso.NTSFormat = "0"
    Me.edSt_fisso.NTSForzaVisZoom = False
    Me.edSt_fisso.NTSOldValue = ""
    Me.edSt_fisso.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_fisso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_fisso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_fisso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_fisso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_fisso.Properties.MaxLength = 65536
    Me.edSt_fisso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_fisso.Size = New System.Drawing.Size(122, 20)
    Me.edSt_fisso.TabIndex = 1
    '
    'edSt_minimo
    '
    Me.edSt_minimo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_minimo.Location = New System.Drawing.Point(161, 15)
    Me.edSt_minimo.Name = "edSt_minimo"
    Me.edSt_minimo.NTSDbField = ""
    Me.edSt_minimo.NTSFormat = "0"
    Me.edSt_minimo.NTSForzaVisZoom = False
    Me.edSt_minimo.NTSOldValue = ""
    Me.edSt_minimo.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_minimo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_minimo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_minimo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_minimo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_minimo.Properties.MaxLength = 65536
    Me.edSt_minimo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_minimo.Size = New System.Drawing.Size(122, 20)
    Me.edSt_minimo.TabIndex = 0
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.lbSt_fissocsLabel)
    Me.NtsTabPage2.Controls.Add(Me.lbSt_minimocsLabel)
    Me.NtsTabPage2.Controls.Add(Me.fmModalita)
    Me.NtsTabPage2.Controls.Add(Me.fmValori)
    Me.NtsTabPage2.Controls.Add(Me.edSt_fissocs)
    Me.NtsTabPage2.Controls.Add(Me.edSt_minimocs)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(595, 311)
    Me.NtsTabPage2.Text = "Spese &contrassegno"
    Me.NtsTabPage2.Tooltip = "Spese riguardanti il contrassegno"
    '
    'lbSt_fissocsLabel
    '
    Me.lbSt_fissocsLabel.AutoSize = True
    Me.lbSt_fissocsLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_fissocsLabel.Location = New System.Drawing.Point(308, 15)
    Me.lbSt_fissocsLabel.Name = "lbSt_fissocsLabel"
    Me.lbSt_fissocsLabel.NTSDbField = ""
    Me.lbSt_fissocsLabel.Size = New System.Drawing.Size(70, 13)
    Me.lbSt_fissocsLabel.TabIndex = 11
    Me.lbSt_fissocsLabel.Text = "Importo fisso"
    Me.lbSt_fissocsLabel.Tooltip = ""
    Me.lbSt_fissocsLabel.UseMnemonic = False
    '
    'lbSt_minimocsLabel
    '
    Me.lbSt_minimocsLabel.AutoSize = True
    Me.lbSt_minimocsLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_minimocsLabel.Location = New System.Drawing.Point(19, 15)
    Me.lbSt_minimocsLabel.Name = "lbSt_minimocsLabel"
    Me.lbSt_minimocsLabel.NTSDbField = ""
    Me.lbSt_minimocsLabel.Size = New System.Drawing.Size(80, 13)
    Me.lbSt_minimocsLabel.TabIndex = 10
    Me.lbSt_minimocsLabel.Text = "Importo minimo"
    Me.lbSt_minimocsLabel.Tooltip = ""
    Me.lbSt_minimocsLabel.UseMnemonic = False
    '
    'fmModalita
    '
    Me.fmModalita.AllowDrop = True
    Me.fmModalita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmModalita.Appearance.Options.UseBackColor = True
    Me.fmModalita.Controls.Add(Me.cbSt_percvalcs)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs9Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs9)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs8Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs8)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs7Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs7)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs6Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs6)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs5Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs5)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs4Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs4)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs3Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs3)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs2Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs2)
    Me.fmModalita.Controls.Add(Me.lbSt_valcs1Label)
    Me.fmModalita.Controls.Add(Me.edSt_valcs1)
    Me.fmModalita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmModalita.Location = New System.Drawing.Point(298, 44)
    Me.fmModalita.Name = "fmModalita"
    Me.fmModalita.Size = New System.Drawing.Size(283, 263)
    Me.fmModalita.TabIndex = 9
    Me.fmModalita.Text = "Modalità di calcolo: "
    '
    'cbSt_percvalcs
    '
    Me.cbSt_percvalcs.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSt_percvalcs.DataSource = Nothing
    Me.cbSt_percvalcs.DisplayMember = ""
    Me.cbSt_percvalcs.Location = New System.Drawing.Point(152, 0)
    Me.cbSt_percvalcs.Name = "cbSt_percvalcs"
    Me.cbSt_percvalcs.NTSDbField = ""
    Me.cbSt_percvalcs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSt_percvalcs.Properties.DropDownRows = 30
    Me.cbSt_percvalcs.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSt_percvalcs.SelectedValue = ""
    Me.cbSt_percvalcs.Size = New System.Drawing.Size(122, 20)
    Me.cbSt_percvalcs.TabIndex = 29
    Me.cbSt_percvalcs.ValueMember = ""
    '
    'lbSt_valcs9Label
    '
    Me.lbSt_valcs9Label.AutoSize = True
    Me.lbSt_valcs9Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs9Label.Location = New System.Drawing.Point(10, 234)
    Me.lbSt_valcs9Label.Name = "lbSt_valcs9Label"
    Me.lbSt_valcs9Label.NTSDbField = ""
    Me.lbSt_valcs9Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs9Label.TabIndex = 26
    Me.lbSt_valcs9Label.Text = "Valore scaglione 9"
    Me.lbSt_valcs9Label.Tooltip = ""
    Me.lbSt_valcs9Label.UseMnemonic = False
    '
    'edSt_valcs9
    '
    Me.edSt_valcs9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs9.Location = New System.Drawing.Point(152, 231)
    Me.edSt_valcs9.Name = "edSt_valcs9"
    Me.edSt_valcs9.NTSDbField = ""
    Me.edSt_valcs9.NTSFormat = "0"
    Me.edSt_valcs9.NTSForzaVisZoom = False
    Me.edSt_valcs9.NTSOldValue = ""
    Me.edSt_valcs9.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs9.Properties.MaxLength = 65536
    Me.edSt_valcs9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs9.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs9.TabIndex = 25
    '
    'lbSt_valcs8Label
    '
    Me.lbSt_valcs8Label.AutoSize = True
    Me.lbSt_valcs8Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs8Label.Location = New System.Drawing.Point(10, 208)
    Me.lbSt_valcs8Label.Name = "lbSt_valcs8Label"
    Me.lbSt_valcs8Label.NTSDbField = ""
    Me.lbSt_valcs8Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs8Label.TabIndex = 23
    Me.lbSt_valcs8Label.Text = "Valore scaglione 8"
    Me.lbSt_valcs8Label.Tooltip = ""
    Me.lbSt_valcs8Label.UseMnemonic = False
    '
    'edSt_valcs8
    '
    Me.edSt_valcs8.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edSt_valcs8.Location = New System.Drawing.Point(152, 205)
    Me.edSt_valcs8.Name = "edSt_valcs8"
    Me.edSt_valcs8.NTSDbField = ""
    Me.edSt_valcs8.NTSFormat = "0"
    Me.edSt_valcs8.NTSForzaVisZoom = False
    Me.edSt_valcs8.NTSOldValue = ""
    Me.edSt_valcs8.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs8.Properties.MaxLength = 65536
    Me.edSt_valcs8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs8.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs8.TabIndex = 22
    '
    'lbSt_valcs7Label
    '
    Me.lbSt_valcs7Label.AutoSize = True
    Me.lbSt_valcs7Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs7Label.Location = New System.Drawing.Point(10, 182)
    Me.lbSt_valcs7Label.Name = "lbSt_valcs7Label"
    Me.lbSt_valcs7Label.NTSDbField = ""
    Me.lbSt_valcs7Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs7Label.TabIndex = 20
    Me.lbSt_valcs7Label.Text = "Valore scaglione 7"
    Me.lbSt_valcs7Label.Tooltip = ""
    Me.lbSt_valcs7Label.UseMnemonic = False
    '
    'edSt_valcs7
    '
    Me.edSt_valcs7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs7.Location = New System.Drawing.Point(152, 179)
    Me.edSt_valcs7.Name = "edSt_valcs7"
    Me.edSt_valcs7.NTSDbField = ""
    Me.edSt_valcs7.NTSFormat = "0"
    Me.edSt_valcs7.NTSForzaVisZoom = False
    Me.edSt_valcs7.NTSOldValue = ""
    Me.edSt_valcs7.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs7.Properties.MaxLength = 65536
    Me.edSt_valcs7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs7.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs7.TabIndex = 19
    '
    'lbSt_valcs6Label
    '
    Me.lbSt_valcs6Label.AutoSize = True
    Me.lbSt_valcs6Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs6Label.Location = New System.Drawing.Point(10, 156)
    Me.lbSt_valcs6Label.Name = "lbSt_valcs6Label"
    Me.lbSt_valcs6Label.NTSDbField = ""
    Me.lbSt_valcs6Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs6Label.TabIndex = 17
    Me.lbSt_valcs6Label.Text = "Valore scaglione 6"
    Me.lbSt_valcs6Label.Tooltip = ""
    Me.lbSt_valcs6Label.UseMnemonic = False
    '
    'edSt_valcs6
    '
    Me.edSt_valcs6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs6.Location = New System.Drawing.Point(152, 153)
    Me.edSt_valcs6.Name = "edSt_valcs6"
    Me.edSt_valcs6.NTSDbField = ""
    Me.edSt_valcs6.NTSFormat = "0"
    Me.edSt_valcs6.NTSForzaVisZoom = False
    Me.edSt_valcs6.NTSOldValue = ""
    Me.edSt_valcs6.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs6.Properties.MaxLength = 65536
    Me.edSt_valcs6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs6.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs6.TabIndex = 16
    '
    'lbSt_valcs5Label
    '
    Me.lbSt_valcs5Label.AutoSize = True
    Me.lbSt_valcs5Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs5Label.Location = New System.Drawing.Point(10, 130)
    Me.lbSt_valcs5Label.Name = "lbSt_valcs5Label"
    Me.lbSt_valcs5Label.NTSDbField = ""
    Me.lbSt_valcs5Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs5Label.TabIndex = 14
    Me.lbSt_valcs5Label.Text = "Valore scaglione 5"
    Me.lbSt_valcs5Label.Tooltip = ""
    Me.lbSt_valcs5Label.UseMnemonic = False
    '
    'edSt_valcs5
    '
    Me.edSt_valcs5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs5.Location = New System.Drawing.Point(152, 127)
    Me.edSt_valcs5.Name = "edSt_valcs5"
    Me.edSt_valcs5.NTSDbField = ""
    Me.edSt_valcs5.NTSFormat = "0"
    Me.edSt_valcs5.NTSForzaVisZoom = False
    Me.edSt_valcs5.NTSOldValue = ""
    Me.edSt_valcs5.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs5.Properties.MaxLength = 65536
    Me.edSt_valcs5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs5.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs5.TabIndex = 13
    '
    'lbSt_valcs4Label
    '
    Me.lbSt_valcs4Label.AutoSize = True
    Me.lbSt_valcs4Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs4Label.Location = New System.Drawing.Point(10, 104)
    Me.lbSt_valcs4Label.Name = "lbSt_valcs4Label"
    Me.lbSt_valcs4Label.NTSDbField = ""
    Me.lbSt_valcs4Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs4Label.TabIndex = 11
    Me.lbSt_valcs4Label.Text = "Valore scaglione 4"
    Me.lbSt_valcs4Label.Tooltip = ""
    Me.lbSt_valcs4Label.UseMnemonic = False
    '
    'edSt_valcs4
    '
    Me.edSt_valcs4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs4.Location = New System.Drawing.Point(152, 101)
    Me.edSt_valcs4.Name = "edSt_valcs4"
    Me.edSt_valcs4.NTSDbField = ""
    Me.edSt_valcs4.NTSFormat = "0"
    Me.edSt_valcs4.NTSForzaVisZoom = False
    Me.edSt_valcs4.NTSOldValue = ""
    Me.edSt_valcs4.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs4.Properties.MaxLength = 65536
    Me.edSt_valcs4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs4.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs4.TabIndex = 10
    '
    'lbSt_valcs3Label
    '
    Me.lbSt_valcs3Label.AutoSize = True
    Me.lbSt_valcs3Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs3Label.Location = New System.Drawing.Point(10, 78)
    Me.lbSt_valcs3Label.Name = "lbSt_valcs3Label"
    Me.lbSt_valcs3Label.NTSDbField = ""
    Me.lbSt_valcs3Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs3Label.TabIndex = 8
    Me.lbSt_valcs3Label.Text = "Valore scaglione 3"
    Me.lbSt_valcs3Label.Tooltip = ""
    Me.lbSt_valcs3Label.UseMnemonic = False
    '
    'edSt_valcs3
    '
    Me.edSt_valcs3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs3.Location = New System.Drawing.Point(152, 75)
    Me.edSt_valcs3.Name = "edSt_valcs3"
    Me.edSt_valcs3.NTSDbField = ""
    Me.edSt_valcs3.NTSFormat = "0"
    Me.edSt_valcs3.NTSForzaVisZoom = False
    Me.edSt_valcs3.NTSOldValue = ""
    Me.edSt_valcs3.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs3.Properties.MaxLength = 65536
    Me.edSt_valcs3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs3.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs3.TabIndex = 7
    '
    'lbSt_valcs2Label
    '
    Me.lbSt_valcs2Label.AutoSize = True
    Me.lbSt_valcs2Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs2Label.Location = New System.Drawing.Point(10, 52)
    Me.lbSt_valcs2Label.Name = "lbSt_valcs2Label"
    Me.lbSt_valcs2Label.NTSDbField = ""
    Me.lbSt_valcs2Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs2Label.TabIndex = 5
    Me.lbSt_valcs2Label.Text = "Valore scaglione 2"
    Me.lbSt_valcs2Label.Tooltip = ""
    Me.lbSt_valcs2Label.UseMnemonic = False
    '
    'edSt_valcs2
    '
    Me.edSt_valcs2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs2.Location = New System.Drawing.Point(152, 49)
    Me.edSt_valcs2.Name = "edSt_valcs2"
    Me.edSt_valcs2.NTSDbField = ""
    Me.edSt_valcs2.NTSFormat = "0"
    Me.edSt_valcs2.NTSForzaVisZoom = False
    Me.edSt_valcs2.NTSOldValue = ""
    Me.edSt_valcs2.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs2.Properties.MaxLength = 65536
    Me.edSt_valcs2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs2.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs2.TabIndex = 4
    '
    'lbSt_valcs1Label
    '
    Me.lbSt_valcs1Label.AutoSize = True
    Me.lbSt_valcs1Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_valcs1Label.Location = New System.Drawing.Point(10, 26)
    Me.lbSt_valcs1Label.Name = "lbSt_valcs1Label"
    Me.lbSt_valcs1Label.NTSDbField = ""
    Me.lbSt_valcs1Label.Size = New System.Drawing.Size(93, 13)
    Me.lbSt_valcs1Label.TabIndex = 2
    Me.lbSt_valcs1Label.Text = "Valore scaglione 1"
    Me.lbSt_valcs1Label.Tooltip = ""
    Me.lbSt_valcs1Label.UseMnemonic = False
    '
    'edSt_valcs1
    '
    Me.edSt_valcs1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_valcs1.Location = New System.Drawing.Point(152, 23)
    Me.edSt_valcs1.Name = "edSt_valcs1"
    Me.edSt_valcs1.NTSDbField = ""
    Me.edSt_valcs1.NTSFormat = "0"
    Me.edSt_valcs1.NTSForzaVisZoom = False
    Me.edSt_valcs1.NTSOldValue = ""
    Me.edSt_valcs1.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_valcs1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_valcs1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_valcs1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_valcs1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_valcs1.Properties.MaxLength = 65536
    Me.edSt_valcs1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_valcs1.Size = New System.Drawing.Size(122, 20)
    Me.edSt_valcs1.TabIndex = 1
    '
    'fmValori
    '
    Me.fmValori.AllowDrop = True
    Me.fmValori.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValori.Appearance.Options.UseBackColor = True
    Me.fmValori.Controls.Add(Me.lbSt_basecs8Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs8)
    Me.fmValori.Controls.Add(Me.lbSt_basecs7Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs7)
    Me.fmValori.Controls.Add(Me.lbSt_basecs6Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs6)
    Me.fmValori.Controls.Add(Me.lbSt_basecs5Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs5)
    Me.fmValori.Controls.Add(Me.lbSt_basecs4Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs4)
    Me.fmValori.Controls.Add(Me.lbSt_basecs3Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs3)
    Me.fmValori.Controls.Add(Me.lbSt_basecs2Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs2)
    Me.fmValori.Controls.Add(Me.lbSt_basecs1Label)
    Me.fmValori.Controls.Add(Me.edSt_basecs1)
    Me.fmValori.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValori.Location = New System.Drawing.Point(9, 44)
    Me.fmValori.Name = "fmValori"
    Me.fmValori.Size = New System.Drawing.Size(283, 263)
    Me.fmValori.TabIndex = 8
    Me.fmValori.Text = "Valori: "
    '
    'lbSt_basecs8Label
    '
    Me.lbSt_basecs8Label.AutoSize = True
    Me.lbSt_basecs8Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs8Label.Location = New System.Drawing.Point(10, 208)
    Me.lbSt_basecs8Label.Name = "lbSt_basecs8Label"
    Me.lbSt_basecs8Label.NTSDbField = ""
    Me.lbSt_basecs8Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs8Label.TabIndex = 23
    Me.lbSt_basecs8Label.Text = "Importo max scag. 8"
    Me.lbSt_basecs8Label.Tooltip = ""
    Me.lbSt_basecs8Label.UseMnemonic = False
    '
    'edSt_basecs8
    '
    Me.edSt_basecs8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs8.Location = New System.Drawing.Point(152, 205)
    Me.edSt_basecs8.Name = "edSt_basecs8"
    Me.edSt_basecs8.NTSDbField = ""
    Me.edSt_basecs8.NTSFormat = "0"
    Me.edSt_basecs8.NTSForzaVisZoom = False
    Me.edSt_basecs8.NTSOldValue = ""
    Me.edSt_basecs8.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs8.Properties.MaxLength = 65536
    Me.edSt_basecs8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs8.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs8.TabIndex = 22
    '
    'lbSt_basecs7Label
    '
    Me.lbSt_basecs7Label.AutoSize = True
    Me.lbSt_basecs7Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs7Label.Location = New System.Drawing.Point(10, 182)
    Me.lbSt_basecs7Label.Name = "lbSt_basecs7Label"
    Me.lbSt_basecs7Label.NTSDbField = ""
    Me.lbSt_basecs7Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs7Label.TabIndex = 20
    Me.lbSt_basecs7Label.Text = "Importo max scag. 7"
    Me.lbSt_basecs7Label.Tooltip = ""
    Me.lbSt_basecs7Label.UseMnemonic = False
    '
    'edSt_basecs7
    '
    Me.edSt_basecs7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs7.Location = New System.Drawing.Point(152, 179)
    Me.edSt_basecs7.Name = "edSt_basecs7"
    Me.edSt_basecs7.NTSDbField = ""
    Me.edSt_basecs7.NTSFormat = "0"
    Me.edSt_basecs7.NTSForzaVisZoom = False
    Me.edSt_basecs7.NTSOldValue = ""
    Me.edSt_basecs7.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs7.Properties.MaxLength = 65536
    Me.edSt_basecs7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs7.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs7.TabIndex = 19
    '
    'lbSt_basecs6Label
    '
    Me.lbSt_basecs6Label.AutoSize = True
    Me.lbSt_basecs6Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs6Label.Location = New System.Drawing.Point(10, 156)
    Me.lbSt_basecs6Label.Name = "lbSt_basecs6Label"
    Me.lbSt_basecs6Label.NTSDbField = ""
    Me.lbSt_basecs6Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs6Label.TabIndex = 17
    Me.lbSt_basecs6Label.Text = "Importo max scag. 6"
    Me.lbSt_basecs6Label.Tooltip = ""
    Me.lbSt_basecs6Label.UseMnemonic = False
    '
    'edSt_basecs6
    '
    Me.edSt_basecs6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs6.Location = New System.Drawing.Point(152, 153)
    Me.edSt_basecs6.Name = "edSt_basecs6"
    Me.edSt_basecs6.NTSDbField = ""
    Me.edSt_basecs6.NTSFormat = "0"
    Me.edSt_basecs6.NTSForzaVisZoom = False
    Me.edSt_basecs6.NTSOldValue = ""
    Me.edSt_basecs6.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs6.Properties.MaxLength = 65536
    Me.edSt_basecs6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs6.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs6.TabIndex = 16
    '
    'lbSt_basecs5Label
    '
    Me.lbSt_basecs5Label.AutoSize = True
    Me.lbSt_basecs5Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs5Label.Location = New System.Drawing.Point(10, 130)
    Me.lbSt_basecs5Label.Name = "lbSt_basecs5Label"
    Me.lbSt_basecs5Label.NTSDbField = ""
    Me.lbSt_basecs5Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs5Label.TabIndex = 14
    Me.lbSt_basecs5Label.Text = "Importo max scag. 5"
    Me.lbSt_basecs5Label.Tooltip = ""
    Me.lbSt_basecs5Label.UseMnemonic = False
    '
    'edSt_basecs5
    '
    Me.edSt_basecs5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs5.Location = New System.Drawing.Point(152, 127)
    Me.edSt_basecs5.Name = "edSt_basecs5"
    Me.edSt_basecs5.NTSDbField = ""
    Me.edSt_basecs5.NTSFormat = "0"
    Me.edSt_basecs5.NTSForzaVisZoom = False
    Me.edSt_basecs5.NTSOldValue = ""
    Me.edSt_basecs5.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs5.Properties.MaxLength = 65536
    Me.edSt_basecs5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs5.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs5.TabIndex = 13
    '
    'lbSt_basecs4Label
    '
    Me.lbSt_basecs4Label.AutoSize = True
    Me.lbSt_basecs4Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs4Label.Location = New System.Drawing.Point(10, 104)
    Me.lbSt_basecs4Label.Name = "lbSt_basecs4Label"
    Me.lbSt_basecs4Label.NTSDbField = ""
    Me.lbSt_basecs4Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs4Label.TabIndex = 11
    Me.lbSt_basecs4Label.Text = "Importo max scag. 4"
    Me.lbSt_basecs4Label.Tooltip = ""
    Me.lbSt_basecs4Label.UseMnemonic = False
    '
    'edSt_basecs4
    '
    Me.edSt_basecs4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs4.Location = New System.Drawing.Point(152, 101)
    Me.edSt_basecs4.Name = "edSt_basecs4"
    Me.edSt_basecs4.NTSDbField = ""
    Me.edSt_basecs4.NTSFormat = "0"
    Me.edSt_basecs4.NTSForzaVisZoom = False
    Me.edSt_basecs4.NTSOldValue = ""
    Me.edSt_basecs4.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs4.Properties.MaxLength = 65536
    Me.edSt_basecs4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs4.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs4.TabIndex = 10
    '
    'lbSt_basecs3Label
    '
    Me.lbSt_basecs3Label.AutoSize = True
    Me.lbSt_basecs3Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs3Label.Location = New System.Drawing.Point(10, 78)
    Me.lbSt_basecs3Label.Name = "lbSt_basecs3Label"
    Me.lbSt_basecs3Label.NTSDbField = ""
    Me.lbSt_basecs3Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs3Label.TabIndex = 8
    Me.lbSt_basecs3Label.Text = "Importo max scag. 3"
    Me.lbSt_basecs3Label.Tooltip = ""
    Me.lbSt_basecs3Label.UseMnemonic = False
    '
    'edSt_basecs3
    '
    Me.edSt_basecs3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs3.Location = New System.Drawing.Point(152, 75)
    Me.edSt_basecs3.Name = "edSt_basecs3"
    Me.edSt_basecs3.NTSDbField = ""
    Me.edSt_basecs3.NTSFormat = "0"
    Me.edSt_basecs3.NTSForzaVisZoom = False
    Me.edSt_basecs3.NTSOldValue = ""
    Me.edSt_basecs3.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs3.Properties.MaxLength = 65536
    Me.edSt_basecs3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs3.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs3.TabIndex = 7
    '
    'lbSt_basecs2Label
    '
    Me.lbSt_basecs2Label.AutoSize = True
    Me.lbSt_basecs2Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs2Label.Location = New System.Drawing.Point(10, 52)
    Me.lbSt_basecs2Label.Name = "lbSt_basecs2Label"
    Me.lbSt_basecs2Label.NTSDbField = ""
    Me.lbSt_basecs2Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs2Label.TabIndex = 5
    Me.lbSt_basecs2Label.Text = "Importo max scag. 2"
    Me.lbSt_basecs2Label.Tooltip = ""
    Me.lbSt_basecs2Label.UseMnemonic = False
    '
    'edSt_basecs2
    '
    Me.edSt_basecs2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs2.Location = New System.Drawing.Point(152, 49)
    Me.edSt_basecs2.Name = "edSt_basecs2"
    Me.edSt_basecs2.NTSDbField = ""
    Me.edSt_basecs2.NTSFormat = "0"
    Me.edSt_basecs2.NTSForzaVisZoom = False
    Me.edSt_basecs2.NTSOldValue = ""
    Me.edSt_basecs2.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs2.Properties.MaxLength = 65536
    Me.edSt_basecs2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs2.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs2.TabIndex = 4
    '
    'lbSt_basecs1Label
    '
    Me.lbSt_basecs1Label.AutoSize = True
    Me.lbSt_basecs1Label.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_basecs1Label.Location = New System.Drawing.Point(10, 26)
    Me.lbSt_basecs1Label.Name = "lbSt_basecs1Label"
    Me.lbSt_basecs1Label.NTSDbField = ""
    Me.lbSt_basecs1Label.Size = New System.Drawing.Size(106, 13)
    Me.lbSt_basecs1Label.TabIndex = 2
    Me.lbSt_basecs1Label.Text = "Importo max scag. 1"
    Me.lbSt_basecs1Label.Tooltip = ""
    Me.lbSt_basecs1Label.UseMnemonic = False
    '
    'edSt_basecs1
    '
    Me.edSt_basecs1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_basecs1.Location = New System.Drawing.Point(152, 23)
    Me.edSt_basecs1.Name = "edSt_basecs1"
    Me.edSt_basecs1.NTSDbField = ""
    Me.edSt_basecs1.NTSFormat = "0"
    Me.edSt_basecs1.NTSForzaVisZoom = False
    Me.edSt_basecs1.NTSOldValue = ""
    Me.edSt_basecs1.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_basecs1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_basecs1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_basecs1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_basecs1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_basecs1.Properties.MaxLength = 65536
    Me.edSt_basecs1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_basecs1.Size = New System.Drawing.Size(122, 20)
    Me.edSt_basecs1.TabIndex = 1
    '
    'edSt_fissocs
    '
    Me.edSt_fissocs.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_fissocs.Location = New System.Drawing.Point(450, 15)
    Me.edSt_fissocs.Name = "edSt_fissocs"
    Me.edSt_fissocs.NTSDbField = ""
    Me.edSt_fissocs.NTSFormat = "0"
    Me.edSt_fissocs.NTSForzaVisZoom = False
    Me.edSt_fissocs.NTSOldValue = ""
    Me.edSt_fissocs.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_fissocs.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_fissocs.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_fissocs.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_fissocs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_fissocs.Properties.MaxLength = 65536
    Me.edSt_fissocs.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_fissocs.Size = New System.Drawing.Size(122, 20)
    Me.edSt_fissocs.TabIndex = 7
    '
    'edSt_minimocs
    '
    Me.edSt_minimocs.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edSt_minimocs.Location = New System.Drawing.Point(161, 15)
    Me.edSt_minimocs.Name = "edSt_minimocs"
    Me.edSt_minimocs.NTSDbField = ""
    Me.edSt_minimocs.NTSFormat = "0"
    Me.edSt_minimocs.NTSForzaVisZoom = False
    Me.edSt_minimocs.NTSOldValue = ""
    Me.edSt_minimocs.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_minimocs.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_minimocs.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_minimocs.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_minimocs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_minimocs.Properties.MaxLength = 65536
    Me.edSt_minimocs.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_minimocs.Size = New System.Drawing.Size(122, 20)
    Me.edSt_minimocs.TabIndex = 6
    '
    'lbSt_codport
    '
    Me.lbSt_codport.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_codport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbSt_codport.Location = New System.Drawing.Point(223, 43)
    Me.lbSt_codport.Name = "lbSt_codport"
    Me.lbSt_codport.NTSDbField = ""
    Me.lbSt_codport.Size = New System.Drawing.Size(366, 20)
    Me.lbSt_codport.TabIndex = 5
    Me.lbSt_codport.Tooltip = ""
    Me.lbSt_codport.UseMnemonic = False
    '
    'lbSt_codvett
    '
    Me.lbSt_codvett.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_codvett.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbSt_codvett.Location = New System.Drawing.Point(223, 15)
    Me.lbSt_codvett.Name = "lbSt_codvett"
    Me.lbSt_codvett.NTSDbField = ""
    Me.lbSt_codvett.Size = New System.Drawing.Size(366, 20)
    Me.lbSt_codvett.TabIndex = 4
    Me.lbSt_codvett.Tooltip = ""
    Me.lbSt_codvett.UseMnemonic = False
    '
    'edSt_codport
    '
    Me.edSt_codport.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSt_codport.Location = New System.Drawing.Point(153, 43)
    Me.edSt_codport.Name = "edSt_codport"
    Me.edSt_codport.NTSDbField = ""
    Me.edSt_codport.NTSForzaVisZoom = False
    Me.edSt_codport.NTSOldValue = ""
    Me.edSt_codport.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_codport.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_codport.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_codport.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_codport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_codport.Properties.MaxLength = 65536
    Me.edSt_codport.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_codport.Size = New System.Drawing.Size(64, 20)
    Me.edSt_codport.TabIndex = 3
    '
    'edSt_codvett
    '
    Me.edSt_codvett.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSt_codvett.Location = New System.Drawing.Point(153, 15)
    Me.edSt_codvett.Name = "edSt_codvett"
    Me.edSt_codvett.NTSDbField = ""
    Me.edSt_codvett.NTSFormat = "0"
    Me.edSt_codvett.NTSForzaVisZoom = False
    Me.edSt_codvett.NTSOldValue = ""
    Me.edSt_codvett.Properties.Appearance.Options.UseTextOptions = True
    Me.edSt_codvett.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSt_codvett.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSt_codvett.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSt_codvett.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSt_codvett.Properties.MaxLength = 65536
    Me.edSt_codvett.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSt_codvett.Size = New System.Drawing.Size(64, 20)
    Me.edSt_codvett.TabIndex = 2
    '
    'lbSt_codportLabel
    '
    Me.lbSt_codportLabel.AutoSize = True
    Me.lbSt_codportLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_codportLabel.Location = New System.Drawing.Point(12, 46)
    Me.lbSt_codportLabel.Name = "lbSt_codportLabel"
    Me.lbSt_codportLabel.NTSDbField = ""
    Me.lbSt_codportLabel.Size = New System.Drawing.Size(130, 13)
    Me.lbSt_codportLabel.TabIndex = 1
    Me.lbSt_codportLabel.Text = "Codice modalità trasporto"
    Me.lbSt_codportLabel.Tooltip = ""
    Me.lbSt_codportLabel.UseMnemonic = False
    '
    'lbSt_codvettLabel
    '
    Me.lbSt_codvettLabel.AutoSize = True
    Me.lbSt_codvettLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSt_codvettLabel.Location = New System.Drawing.Point(12, 18)
    Me.lbSt_codvettLabel.Name = "lbSt_codvettLabel"
    Me.lbSt_codvettLabel.NTSDbField = ""
    Me.lbSt_codvettLabel.Size = New System.Drawing.Size(78, 13)
    Me.lbSt_codvettLabel.TabIndex = 0
    Me.lbSt_codvettLabel.Text = "Codice vettore"
    Me.lbSt_codvettLabel.Tooltip = ""
    Me.lbSt_codvettLabel.UseMnemonic = False
    '
    'FRMVESPRT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(627, 455)
    Me.Controls.Add(Me.pnSprt)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.HelpContext = ""
    Me.Name = "FRMVESPRT"
    Me.Text = "SPESE DI TRASPORTO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSprt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSprt.ResumeLayout(False)
    Me.pnSprt.PerformLayout()
    CType(Me.tsSptr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSptr.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    Me.NtsTabPage1.PerformLayout()
    CType(Me.fmCalcolo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCalcolo.ResumeLayout(False)
    Me.fmCalcolo.PerformLayout()
    CType(Me.cbSt_percval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_val1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAddebito, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAddebito.ResumeLayout(False)
    Me.fmAddebito.PerformLayout()
    CType(Me.cbSt_addclie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_base1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_fisso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_minimo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    Me.NtsTabPage2.PerformLayout()
    CType(Me.fmModalita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmModalita.ResumeLayout(False)
    Me.fmModalita.PerformLayout()
    CType(Me.cbSt_percvalcs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_valcs1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValori, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValori.ResumeLayout(False)
    Me.fmValori.PerformLayout()
    CType(Me.edSt_basecs8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_basecs1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_fissocs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_minimocs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_codport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSt_codvett.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function
  Public Overridable Sub InitEntity(ByRef cleVett As CLEVEVETT)
    oCleVett = cleVett
    AddHandler oCleVett.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAddclie As New DataTable()
    Dim dttPercval As New DataTable()
    Dim dttPercvalcs As New DataTable()
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
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttAddclie.Columns.Add("cod", GetType(String))
      dttAddclie.Columns.Add("val", GetType(String))
      dttAddclie.Rows.Add(New Object() {"P", "Peso"})
      dttAddclie.Rows.Add(New Object() {"V", "Valore"})
      dttAddclie.AcceptChanges()
      cbSt_addclie.DataSource = dttAddclie
      cbSt_addclie.ValueMember = "cod"
      cbSt_addclie.DisplayMember = "val"

      dttPercval.Columns.Add("cod", GetType(String))
      dttPercval.Columns.Add("val", GetType(String))
      dttPercval.Rows.Add(New Object() {"P", "Percentuali"})
      dttPercval.Rows.Add(New Object() {"V", "Valori"})
      dttPercval.AcceptChanges()
      cbSt_percval.DataSource = dttPercval
      cbSt_percval.ValueMember = "cod"
      cbSt_percval.DisplayMember = "val"

      dttPercvalcs.Columns.Add("cod", GetType(String))
      dttPercvalcs.Columns.Add("val", GetType(String))
      dttPercvalcs.Rows.Add(New Object() {"P", "Percentuali"})
      dttPercvalcs.Rows.Add(New Object() {"V", "Valori"})
      dttPercvalcs.AcceptChanges()
      cbSt_percvalcs.DataSource = dttPercvalcs
      cbSt_percvalcs.ValueMember = "cod"
      cbSt_percvalcs.DisplayMember = "val"

      cbSt_percval.NTSSetParam(oApp.Tr(Me, 128405677646596485, "Modalità di calcolo"))
      edSt_val9.NTSSetParam(oMenu, oApp.Tr(Me, 128405677646908993, "Valore scaglione 9"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val8.NTSSetParam(oMenu, oApp.Tr(Me, 128405677647221501, "Valore scaglione 8"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val7.NTSSetParam(oMenu, oApp.Tr(Me, 128405677647534009, "Valore scaglione 7"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val6.NTSSetParam(oMenu, oApp.Tr(Me, 128405677647846517, "Valore scaglione 6"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val5.NTSSetParam(oMenu, oApp.Tr(Me, 128405677648159025, "Valore scaglione 5"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val4.NTSSetParam(oMenu, oApp.Tr(Me, 128405677648471533, "Valore scaglione 4"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val3.NTSSetParam(oMenu, oApp.Tr(Me, 128405677648784041, "Valore scaglione 3"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val2.NTSSetParam(oMenu, oApp.Tr(Me, 128405677649096549, "Valore scaglione 2"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_val1.NTSSetParam(oMenu, oApp.Tr(Me, 128974943815735326, "Valore scaglione 1"), oApp.FormatImporti, 0, 0, 999999999999)
      cbSt_addclie.NTSSetParam(oApp.Tr(Me, 128974943830110326, "Modalità di addebito"))
      edSt_base8.NTSSetParam(oMenu, oApp.Tr(Me, 128405677649877819, "Importo max scag. 8"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base7.NTSSetParam(oMenu, oApp.Tr(Me, 128405677650346581, "Importo max scag. 7"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base6.NTSSetParam(oMenu, oApp.Tr(Me, 128405677650815343, "Importo max scag. 6"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base5.NTSSetParam(oMenu, oApp.Tr(Me, 128405677651284105, "Importo max scag. 5"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base4.NTSSetParam(oMenu, oApp.Tr(Me, 128405677651752867, "Importo max scag. 4"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base3.NTSSetParam(oMenu, oApp.Tr(Me, 128405677652221629, "Importo max scag. 3"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base2.NTSSetParam(oMenu, oApp.Tr(Me, 128405677652690391, "Importo max scag. 2"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_base1.NTSSetParam(oMenu, oApp.Tr(Me, 128974944003860326, "Importo max scag. 1"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_fisso.NTSSetParam(oMenu, oApp.Tr(Me, 128974944018079076, "Importo fisso"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_minimo.NTSSetParam(oMenu, oApp.Tr(Me, 128405677653315407, "Importo minimo"), oApp.FormatImporti, 0, 0, 999999999999)
      cbSt_percvalcs.NTSSetParam(oApp.Tr(Me, 128405677653627915, "Modalità di calcolo"))
      edSt_valcs9.NTSSetParam(oMenu, oApp.Tr(Me, 128405677653940423, "Valore scaglione 9"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs8.NTSSetParam(oMenu, oApp.Tr(Me, 128405677654252931, "Valore scaglione 8"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs7.NTSSetParam(oMenu, oApp.Tr(Me, 128405677654565439, "Valore scaglione 7"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs6.NTSSetParam(oMenu, oApp.Tr(Me, 128405677654877947, "Valore scaglione 6"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs5.NTSSetParam(oMenu, oApp.Tr(Me, 128405677655190455, "Valore scaglione 5"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs4.NTSSetParam(oMenu, oApp.Tr(Me, 128405677655502963, "Valore scaglione 4"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs3.NTSSetParam(oMenu, oApp.Tr(Me, 128405677655815471, "Valore scaglione 3"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs2.NTSSetParam(oMenu, oApp.Tr(Me, 128405677656127979, "Valore scaglione 2"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_valcs1.NTSSetParam(oMenu, oApp.Tr(Me, 128405677656440487, "Valore scaglione 1"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs8.NTSSetParam(oMenu, oApp.Tr(Me, 128405677656596741, "Importo max scag. 8"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs7.NTSSetParam(oMenu, oApp.Tr(Me, 128405677656909249, "Importo max scag. 7"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs6.NTSSetParam(oMenu, oApp.Tr(Me, 128405677657221757, "Importo max scag. 6"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs5.NTSSetParam(oMenu, oApp.Tr(Me, 128405677657534265, "Importo max scag. 5"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs4.NTSSetParam(oMenu, oApp.Tr(Me, 128405677657846773, "Importo max scag. 4"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs3.NTSSetParam(oMenu, oApp.Tr(Me, 128405677658159281, "Importo max scag. 3"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs2.NTSSetParam(oMenu, oApp.Tr(Me, 128405677658471789, "Importo max scag. 2"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_basecs1.NTSSetParam(oMenu, oApp.Tr(Me, 128974943722766576, "Importo max scag. 1"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_fissocs.NTSSetParam(oMenu, oApp.Tr(Me, 128974943747141576, "Importo fisso"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_minimocs.NTSSetParam(oMenu, oApp.Tr(Me, 128405677658940551, "Importo minimo"), oApp.FormatImporti, 0, 0, 999999999999)
      edSt_codport.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128405677659253059, "Codice modalità trasporto"), tabport, False)
      edSt_codvett.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128405677659409313, "Codice vettore"), tabvett)

      edSt_codvett.Enabled = False

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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 

      cbSt_percval.NTSDbField = "spetrasp.st_percval"
      edSt_val9.NTSDbField = "spetrasp.st_val9"
      edSt_val8.NTSDbField = "spetrasp.st_val8"
      edSt_val7.NTSDbField = "spetrasp.st_val7"
      edSt_val6.NTSDbField = "spetrasp.st_val6"
      edSt_val5.NTSDbField = "spetrasp.st_val5"
      edSt_val4.NTSDbField = "spetrasp.st_val4"
      edSt_val3.NTSDbField = "spetrasp.st_val3"
      edSt_val2.NTSDbField = "spetrasp.st_val2"
      edSt_val1.NTSDbField = "spetrasp.st_val1"
      cbSt_addclie.NTSDbField = "spetrasp.st_addclie"
      edSt_base8.NTSDbField = "spetrasp.st_base8"
      edSt_base7.NTSDbField = "spetrasp.st_base7"
      edSt_base6.NTSDbField = "spetrasp.st_base6"
      edSt_base5.NTSDbField = "spetrasp.st_base5"
      edSt_base4.NTSDbField = "spetrasp.st_base4"
      edSt_base3.NTSDbField = "spetrasp.st_base3"
      edSt_base2.NTSDbField = "spetrasp.st_base2"
      edSt_base1.NTSDbField = "spetrasp.st_base1"
      edSt_fisso.NTSDbField = "spetrasp.st_fisso"
      edSt_minimo.NTSDbField = "spetrasp.st_minimo"
      cbSt_percvalcs.NTSDbField = "spetrasp.st_percvalcs"
      edSt_valcs9.NTSDbField = "spetrasp.st_valcs9"
      edSt_valcs8.NTSDbField = "spetrasp.st_valcs8"
      edSt_valcs7.NTSDbField = "spetrasp.st_valcs7"
      edSt_valcs6.NTSDbField = "spetrasp.st_valcs6"
      edSt_valcs5.NTSDbField = "spetrasp.st_valcs5"
      edSt_valcs4.NTSDbField = "spetrasp.st_valcs4"
      edSt_valcs3.NTSDbField = "spetrasp.st_valcs3"
      edSt_valcs2.NTSDbField = "spetrasp.st_valcs2"
      edSt_valcs1.NTSDbField = "spetrasp.st_valcs1"
      edSt_basecs8.NTSDbField = "spetrasp.st_basecs8"
      edSt_basecs7.NTSDbField = "spetrasp.st_basecs7"
      edSt_basecs6.NTSDbField = "spetrasp.st_basecs6"
      edSt_basecs5.NTSDbField = "spetrasp.st_basecs5"
      edSt_basecs4.NTSDbField = "spetrasp.st_basecs4"
      edSt_basecs3.NTSDbField = "spetrasp.st_basecs3"
      edSt_basecs2.NTSDbField = "spetrasp.st_basecs2"
      edSt_basecs1.NTSDbField = "spetrasp.st_basecs1"
      edSt_fissocs.NTSDbField = "spetrasp.st_fissocs"
      edSt_minimocs.NTSDbField = "spetrasp.st_minimocs"
      lbSt_codport.NTSDbField = "spetrasp.xx_desport"
      lbSt_codvett.NTSDbField = "spetrasp.xx_desvett"
      edSt_codport.NTSDbField = "spetrasp.st_codport"
      edSt_codvett.NTSDbField = "spetrasp.st_codvett"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcSprt, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMTESPRT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()
      '--------------------------------------------------------------------------------------------------------------
      SettaLabelScaglioni()
      '--------------------------------------------------------------------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "EventiToolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova spesa di trasporto
      If Not Salva() Then Return
      oCleVett.SprtNuovo()
      dcSprt.MoveLast()
      edSt_codport.Enabled = True
      edSt_codport.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

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
      'cancello la spesa di trasporto
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128256093191691883, "Cancellare la spesa di trasporto?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsSprt.Tables("SPETRASP").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcSprt.RemoveAt(dcSprt.Position)
          oCleVett.SprtSalva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcSprt, Me)
            bRemovBinding = False
            edSt_codport.Enabled = True
          Else
            edSt_codport.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSprt, Me)
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
      'Ripristino il spesa di trasporto
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128257699390236031, "Ripristinare le modifiche apportate alla spesa di trasporto?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsSprt.Tables("SPETRASP").Rows.Count = 1 And dsSprt.Tables("SPETRASP").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleVett.SprtRipristina(dcSprt.Position, dcSprt.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcSprt, Me)
            bRemovBinding = False
            edSt_codport.Enabled = True
          Else
            edSt_codport.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcSprt, Me)
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

      If edSt_codport.Focused Then
        '--------------------------------------------
        'Zoom TABPORT
        SetFastZoom(edSt_codport.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edSt_codport.Text
        NTSZOOM.ZoomStrIn("ZOOMTABPORT", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edSt_codport.Text Then
          edSt_codport.NTSTextDB = NTSZOOM.strIn
        End If
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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcSprt.MoveFirst()
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
      dcSprt.MovePrevious()
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
      dcSprt.MoveNext()
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
      dcSprt.MoveLast()
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

  Public Overridable Function Apri() As Boolean
    Try

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      NTSFormClearDataBinding(Me)
      If Not oCleVett.SprtApri(DittaCorrente, dsSprt) Then Me.Close()
      dcSprt.DataSource = dsSprt.Tables("SPETRASP")
      dsSprt.AcceptChanges()
      If dsSprt.Tables("SPETRASP").Rows.Count > 0 Then
        edSt_codport.Enabled = False
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      If dsSprt.Tables("SPETRASP").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcSprt.ResetBindings(False)
      dcSprt.MoveFirst()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult

    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleVett.SprtRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128306257061636669, "Salvo la spesa di trasporto?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleVett.SprtSalva(False) Then Return False
          If dsSprt.Tables("SPETRASP").Rows.Count > 0 Then
            edSt_codport.Enabled = False
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

  Public Overridable Sub cbSt_addclie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSt_addclie.SelectedIndexChanged
    Try
      Select Case cbSt_addclie.SelectedValue
        Case "P"   'Peso
          lb1.Text = "Kg"
          lb2.Text = "Kg"
          lb3.Text = "Kg"
          lb4.Text = "Kg"
          lb5.Text = "Kg"
          lb6.Text = "Kg"
          lb7.Text = "Kg"
          lb8.Text = "Kg"
        Case "V"   'Valore
          lb1.Text = oApp.ValutaCorrente
          lb2.Text = oApp.ValutaCorrente
          lb3.Text = oApp.ValutaCorrente
          lb4.Text = oApp.ValutaCorrente
          lb5.Text = oApp.ValutaCorrente
          lb6.Text = oApp.ValutaCorrente
          lb7.Text = oApp.ValutaCorrente
          lb8.Text = oApp.ValutaCorrente
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub
  Public Overridable Sub cbSt_percval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSt_percval.SelectedIndexChanged
    Try
      Select Case cbSt_percval.SelectedValue
        Case "P"   'Percentuali
          lbSt_val1Label.Text = oApp.Tr(Me, 130420504708519346, "Percentuale scaglione 1")
          lbSt_val2Label.Text = oApp.Tr(Me, 130420505296648110, "Percentuale scaglione 2")
          lbSt_val3Label.Text = oApp.Tr(Me, 130420505532743371, "Percentuale scaglione 3")
          lbSt_val4Label.Text = oApp.Tr(Me, 130420505604775082, "Percentuale scaglione 4")
          lbSt_val5Label.Text = oApp.Tr(Me, 130420505680400566, "Percentuale scaglione 5")
          lbSt_val6Label.Text = oApp.Tr(Me, 130420505756182301, "Percentuale scaglione 6")
          lbSt_val7Label.Text = oApp.Tr(Me, 130420505892901926, "Percentuale scaglione 7")
          lbSt_val8Label.Text = oApp.Tr(Me, 130420505968214908, "Percentuale scaglione 8")
          lbSt_val9Label.Text = oApp.Tr(Me, 130420506051652942, "Percentuale scaglione 9")
        Case "V"   'Valori
          lbSt_val1Label.Text = oApp.Tr(Me, 130420506134622223, "Valore scaglione 1")
          lbSt_val2Label.Text = oApp.Tr(Me, 130420506202278906, "Valore scaglione 2")
          lbSt_val3Label.Text = oApp.Tr(Me, 130420506294623247, "Valore scaglione 3")
          lbSt_val4Label.Text = oApp.Tr(Me, 130420506364311193, "Valore scaglione 4")
          lbSt_val5Label.Text = oApp.Tr(Me, 130420506439155422, "Valore scaglione 5")
          lbSt_val6Label.Text = oApp.Tr(Me, 130420506508843368, "Valore scaglione 6")
          lbSt_val7Label.Text = oApp.Tr(Me, 130420506580562577, "Valore scaglione 7")
          lbSt_val8Label.Text = oApp.Tr(Me, 130420506652281786, "Valore scaglione 8")
          lbSt_val9Label.Text = oApp.Tr(Me, 130420506722750987, "Valore scaglione 9")
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub
  Public Overridable Sub cbSt_percvalcs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSt_percvalcs.SelectedIndexChanged
    Try
      Select Case cbSt_percvalcs.SelectedValue
        Case "P"   'Percentuali
          lbSt_valcs1Label.Text = "Percentuale scaglione 1" : lbSt_valcs2Label.Text = "Percentuale scaglione 2"
          lbSt_valcs3Label.Text = "Percentuale scaglione 3" : lbSt_valcs4Label.Text = "Percentuale scaglione 4"
          lbSt_valcs5Label.Text = "Percentuale scaglione 5" : lbSt_valcs6Label.Text = "Percentuale scaglione 6"
          lbSt_valcs7Label.Text = "Percentuale scaglione 7" : lbSt_valcs8Label.Text = "Percentuale scaglione 8"
          lbSt_valcs9Label.Text = "Percentuale scaglione 9"
        Case "V"   'Valori
          lbSt_valcs1Label.Text = "Valore scaglione 1" : lbSt_valcs2Label.Text = "Valore scaglione 2"
          lbSt_valcs3Label.Text = "Valore scaglione 3" : lbSt_valcs4Label.Text = "Valore scaglione 4"
          lbSt_valcs5Label.Text = "Valore scaglione 5" : lbSt_valcs6Label.Text = "Valore scaglione 6"
          lbSt_valcs7Label.Text = "Valore scaglione 7" : lbSt_valcs8Label.Text = "Valore scaglione 8"
          lbSt_valcs9Label.Text = "Valore scaglione 9"
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub SettaLabelScaglioni()
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbSt_addclie.SelectedValue
        Case "P"   'Peso
          lb1.Text = "Kg"
          lb2.Text = "Kg"
          lb3.Text = "Kg"
          lb4.Text = "Kg"
          lb5.Text = "Kg"
          lb6.Text = "Kg"
          lb7.Text = "Kg"
          lb8.Text = "Kg"
        Case "V"   'Valore
          lb1.Text = oApp.ValutaCorrente
          lb2.Text = oApp.ValutaCorrente
          lb3.Text = oApp.ValutaCorrente
          lb4.Text = oApp.ValutaCorrente
          lb5.Text = oApp.ValutaCorrente
          lb6.Text = oApp.ValutaCorrente
          lb7.Text = oApp.ValutaCorrente
          lb8.Text = oApp.ValutaCorrente
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbSt_percval.SelectedValue
        Case "P"   'Percentuali
          lbSt_val1Label.Text = oApp.Tr(Me, 130420504708519346, "Percentuale scaglione 1")
          lbSt_val2Label.Text = oApp.Tr(Me, 130420505296648110, "Percentuale scaglione 2")
          lbSt_val3Label.Text = oApp.Tr(Me, 130420505532743371, "Percentuale scaglione 3")
          lbSt_val4Label.Text = oApp.Tr(Me, 130420505604775082, "Percentuale scaglione 4")
          lbSt_val5Label.Text = oApp.Tr(Me, 130420505680400566, "Percentuale scaglione 5")
          lbSt_val6Label.Text = oApp.Tr(Me, 130420505756182301, "Percentuale scaglione 6")
          lbSt_val7Label.Text = oApp.Tr(Me, 130420505892901926, "Percentuale scaglione 7")
          lbSt_val8Label.Text = oApp.Tr(Me, 130420505968214908, "Percentuale scaglione 8")
          lbSt_val9Label.Text = oApp.Tr(Me, 130420506051652942, "Percentuale scaglione 9")
        Case "V"   'Valori
          lbSt_val1Label.Text = oApp.Tr(Me, 130420506134622223, "Valore scaglione 1")
          lbSt_val2Label.Text = oApp.Tr(Me, 130420506202278906, "Valore scaglione 2")
          lbSt_val3Label.Text = oApp.Tr(Me, 130420506294623247, "Valore scaglione 3")
          lbSt_val4Label.Text = oApp.Tr(Me, 130420506364311193, "Valore scaglione 4")
          lbSt_val5Label.Text = oApp.Tr(Me, 130420506439155422, "Valore scaglione 5")
          lbSt_val6Label.Text = oApp.Tr(Me, 130420506508843368, "Valore scaglione 6")
          lbSt_val7Label.Text = oApp.Tr(Me, 130420506580562577, "Valore scaglione 7")
          lbSt_val8Label.Text = oApp.Tr(Me, 130420506652281786, "Valore scaglione 8")
          lbSt_val9Label.Text = oApp.Tr(Me, 130420506722750987, "Valore scaglione 9")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbSt_percvalcs.SelectedValue
        Case "P"   'Percentuali
          lbSt_valcs1Label.Text = "Percentuale scaglione 1" : lbSt_valcs2Label.Text = "Percentuale scaglione 2"
          lbSt_valcs3Label.Text = "Percentuale scaglione 3" : lbSt_valcs4Label.Text = "Percentuale scaglione 4"
          lbSt_valcs5Label.Text = "Percentuale scaglione 5" : lbSt_valcs6Label.Text = "Percentuale scaglione 6"
          lbSt_valcs7Label.Text = "Percentuale scaglione 7" : lbSt_valcs8Label.Text = "Percentuale scaglione 8"
          lbSt_valcs9Label.Text = "Percentuale scaglione 9"
        Case "V"   'Valori
          lbSt_valcs1Label.Text = "Valore scaglione 1" : lbSt_valcs2Label.Text = "Valore scaglione 2"
          lbSt_valcs3Label.Text = "Valore scaglione 3" : lbSt_valcs4Label.Text = "Valore scaglione 4"
          lbSt_valcs5Label.Text = "Valore scaglione 5" : lbSt_valcs6Label.Text = "Valore scaglione 6"
          lbSt_valcs7Label.Text = "Valore scaglione 7" : lbSt_valcs8Label.Text = "Valore scaglione 8"
          lbSt_valcs9Label.Text = "Valore scaglione 9"
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class